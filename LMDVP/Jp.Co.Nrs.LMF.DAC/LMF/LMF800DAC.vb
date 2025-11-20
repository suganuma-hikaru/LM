' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF800DAC : 運賃データ生成メイン
'  作  成  者       :  SBS)M.kobayashi
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF800DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF800DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    '2013.07.04 追加START
    Private Const INKA As String = "10"
    Private Const OUTKA As String = "20"
    '2013.07.04 追加END

    'データセットテーブル名
    Private Const TABLE_NM_IN As String = "LMF800IN"
    Private Const TABLE_NM_OUT As String = "LMF800OUT"
    Private Const TABLE_NM_UNCHIN As String = "F_UNCHIN_TRS"
    Private Const TABLE_NM_UNSO_L As String = "PRE_UNSO_L"
    Private Const TABLE_NM_UNSO_M As String = "PRE_UNSO_M"
    Private Const TABLE_NM_YOKO_HD As String = "M_YOKO_TARIFF_HD"
    Private Const TABLE_NM_TARIFF_LATEST As String = "UNCHIN_TARIFF_LATEST_REC"
    Private Const TABLE_NM_REV_UNIT As String = "GOODS_INFO_REV_UNIT"

    Private Const TABLE_NM_CALC_IN As String = "UNCHIN_CALC_IN"
    Private Const TABLE_NM_CALC_OUT As String = "UNCHIN_CALC_OUT"
    Private Const TABLE_NM_CALC_E_UNCHIN As String = "M_EXTC_UNCHIN"
    Private Const TABLE_NM_CALC_SYUHAI As String = "M_SYUHAI_SET"
    Private Const TABLE_NM_CALC_KYORI As String = "M_UNCHIN_KYORI"
    Private Const TABLE_NM_CALC_WEIGHT As String = "M_UNCHIN_WT"
    Private Const TABLE_NM_CALC_YOKO_HD As String = "M_YOKO_TARIFF_HD_CALC"
    Private Const TABLE_NM_CALC_YOKO_DTL As String = "M_YOKO_TARIFF_DTL_CALC"
    Private Const TABLE_NM_UNSO_HOKENRYO As String = "UNSO_HOKENRYO"        'ADD 2018/10/22 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる

    '2013.07.04 追加START
    Private Const TABLE_NM_RCV_INOUT_BP As String = "RCV_INOUT_BP"
    '2013.07.04 追加END

#End Region '制御用

#Region "SQL"

#Region "SQL"

    ''' <summary>
    ''' [F_UNSO_L]運送Ｌ 情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_UNSOL As String = "    SELECT                                              " & vbNewLine _
                                          & "            F_UNSO_L.*                                  " & vbNewLine _
                                          & " --★2013.03.18 / NOTES 1911 対応 開始                  " & vbNewLine _
                                          & "           ,M_UNSOCO.TARE_YN       AS UNSO_TARE_YN      " & vbNewLine _
                                          & " --★2013.03.18 / NOTES 1911 対応 終了                  " & vbNewLine _
                                          & "    FROM                                                " & vbNewLine _
                                          & "           $LM_TRN$..F_UNSO_L                           " & vbNewLine _
                                          & "    LEFT OUTER JOIN                                     " & vbNewLine _
                                          & "           $LM_MST$..M_UNSOCO                           " & vbNewLine _
                                          & "        ON F_UNSO_L.NRS_BR_CD   = M_UNSOCO.NRS_BR_CD    " & vbNewLine _
                                          & "       AND F_UNSO_L.UNSO_CD     = M_UNSOCO.UNSOCO_CD    " & vbNewLine _
                                          & "       AND F_UNSO_L.UNSO_BR_CD  = M_UNSOCO.UNSOCO_BR_CD " & vbNewLine _
                                          & "       AND M_UNSOCO.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                          & "    WHERE  F_UNSO_L.UNSO_NO_L   = @UNSO_NO_L            " & vbNewLine _
                                          & "       AND F_UNSO_L.SYS_DEL_FLG = '0'                   " & vbNewLine
    'NOTES 1911 対応前(バックログ)
    '" SELECT                        " & vbNewLine _
    '                                          & "     *                         " & vbNewLine _
    '                                          & " FROM                          " & vbNewLine _
    '                                          & "     $LM_TRN$..F_UNSO_L        " & vbNewLine _
    '                                          & " WHERE                         " & vbNewLine _
    '                                          & "     UNSO_NO_L   = @UNSO_NO_L  " & vbNewLine _
    '                                          & " AND SYS_DEL_FLG = '0'         " & vbNewLine

    '2013.07.04 追加START

    Private Const SQL_GET_BP_EDI_OUT As String = "SELECT  HBP.OUTKA_CTL_NO AS INOUTKA_NO_L " & vbNewLine _
                                    & "--(2014.01.22)要望番号2149 修正START      " & vbNewLine _
                                    & "      , SUM(TOTAL_QT) AS BP_UNSO_WT       " & vbNewLine _
                                    & "--      , SUM(TOTAL_WT) AS BP_UNSO_WT       " & vbNewLine _
                                    & "--(2014.01.22)要望番号2149 修正END        " & vbNewLine _
                                    & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_BP HBP   " & vbNewLine _
                                    & "LEFT JOIN                                 " & vbNewLine _
                                    & "$LM_TRN$..C_OUTKA_L CL                   " & vbNewLine _
                                    & "ON                                        " & vbNewLine _
                                    & "HBP.NRS_BR_CD = CL.NRS_BR_CD              " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "HBP.OUTKA_CTL_NO = CL.OUTKA_NO_L          " & vbNewLine _
                                    & "LEFT JOIN                                 " & vbNewLine _
                                    & "$LM_TRN$..F_UNSO_L FL                    " & vbNewLine _
                                    & "ON                                        " & vbNewLine _
                                    & "CL.NRS_BR_CD = FL.NRS_BR_CD               " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "CL.OUTKA_NO_L = FL.INOUTKA_NO_L           " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "FL.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                    & "LEFT JOIN                                 " & vbNewLine _
                                    & "$LM_MST$..M_CUST_DETAILS MCD              " & vbNewLine _
                                    & "ON                                        " & vbNewLine _
                                    & "MCD.NRS_BR_CD = HBP.NRS_BR_CD             " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "MCD.CUST_CD = HBP.CUST_CD_L               " & vbNewLine _
                                    & "WHERE                                     " & vbNewLine _
                                    & "FL.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "FL.UNSO_NO_L = @UNSO_NO_L                 " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "FL.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "CL.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "HBP.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                    & "AND                                       " & vbNewLine _
                                    & "MCD.SUB_KB = '58'                         " & vbNewLine _
                                    & "GROUP BY                                  " & vbNewLine _
                                    & "HBP.OUTKA_CTL_NO                          " & vbNewLine

    '2013.07.04 追加END

    ''2013.07.04 追加START
    'Private Const SQL_GET_BP_EDI_IN As String = "SELECT  HBP.OUTKA_CTL_NO AS INOUTKA_NO_L " & vbNewLine _
    '                                    & "      , SUM(TOTAL_WT) AS BP_UNSO_WT       " & vbNewLine _
    '                                    & "  FROM $LM_TRN$..H_INKAEDI_DTL_BP HBP    " & vbNewLine _
    '                                    & "LEFT JOIN                                 " & vbNewLine _
    '                                    & "$LM_TRN$..B_INKA_L BL                    " & vbNewLine _
    '                                    & "ON                                        " & vbNewLine _
    '                                    & "HBP.NRS_BR_CD = BL.NRS_BR_CD              " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "HBP.INKA_CTL_NO_L = BL.INKA_NO_L          " & vbNewLine _
    '                                    & "LEFT JOIN                                 " & vbNewLine _
    '                                    & "$LM_TRN$..F_UNSO_L FL                    " & vbNewLine _
    '                                    & "ON                                        " & vbNewLine _
    '                                    & "BL.NRS_BR_CD = FL.NRS_BR_CD               " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "BL.INKA_NO_L = FL.INOUTKA_NO_L            " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "FL.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                    & "LEFT JOIN                                 " & vbNewLine _
    '                                    & "$LM_MST$..M_CUST_DETAILS MCD              " & vbNewLine _
    '                                    & "ON                                        " & vbNewLine _
    '                                    & "MCD.NRS_BR_CD = HBP.NRS_BR_CD             " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "MCD.CUST_CD = HBP.CUST_CD_L               " & vbNewLine _
    '                                    & "WHERE                                     " & vbNewLine _
    '                                    & "FL.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "FL.UNSO_NO_L = @UNSO_NO_L                 " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "FL.SYS_DEL_FLG = '0'                      " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "BL.SYS_DEL_FLG = '0'                      " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "HBP.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                    & "AND                                       " & vbNewLine _
    '                                    & "MCD.SUB_KB = '58'                         " & vbNewLine _
    '                                    & "GROUP BY                                  " & vbNewLine _
    '                                    & "HBP.INKA_CTL_NO_L                         " & vbNewLine
    ''2013.07.04 追加END

    ''' <summary>
    ''' [F_UNSO_M]運送Ｍ 情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_UNSOM_ALL As String = " SELECT                                                                 " & vbNewLine _
                                              & "     UNSO_M.*                                                           " & vbNewLine _
                                              & "    ,(CASE WHEN GDS.NRS_BR_CD IS NULL THEN '00'                         " & vbNewLine _
                                              & "           ELSE                            '01' END ) AS GOODS_MST_FLG  " & vbNewLine _
                                              & "    ,GDS.STD_IRIME_NB     AS STD_IRIME_NB                               " & vbNewLine _
                                              & "    ,GDS.STD_WT_KGS       AS STD_WT_KGS                                 " & vbNewLine _
                                              & "    ,ISNULL(KBN.VALUE1,0) AS TARE_WT                                    " & vbNewLine _
                                              & " --★2013.03.18 / NOTES 1911 対応 開始                                  " & vbNewLine _
                                              & "    ,GDS.TARE_YN          AS GOODS_TARE_YN                              " & vbNewLine _
                                              & "    ,''                   AS ALCTD_KB                                   " & vbNewLine _
                                              & " --★2013.03.18 / NOTES 1911 対応 終了                                  " & vbNewLine _
                                              & " FROM                                                                   " & vbNewLine _
                                              & "  $LM_TRN$..F_UNSO_M  AS UNSO_M                                         " & vbNewLine _
                                              & "  LEFT OUTER JOIN                                                       " & vbNewLine _
                                              & "     $LM_MST$..M_GOODS  AS GDS                                          " & vbNewLine _
                                              & "     ON  UNSO_M.NRS_BR_CD    = GDS.NRS_BR_CD                            " & vbNewLine _
                                              & "     AND UNSO_M.GOODS_CD_NRS = GDS.GOODS_CD_NRS                         " & vbNewLine _
                                              & "     AND GDS.SYS_DEL_FLG     = '0'                                      " & vbNewLine _
                                              & "  LEFT OUTER JOIN                                                       " & vbNewLine _
                                              & "     $LM_MST$..Z_KBN    AS KBN                                          " & vbNewLine _
                                              & "     ON  KBN.KBN_GROUP_CD    = 'N001'                                   " & vbNewLine _
                                              & "     AND KBN.KBN_CD          = UNSO_M.NB_UT                             " & vbNewLine _
                                              & "     AND KBN.SYS_DEL_FLG     = '0'                                      " & vbNewLine _
                                              & " WHERE                                                                  " & vbNewLine _
                                              & "     UNSO_M.UNSO_NO_L   = @UNSO_NO_L                                    " & vbNewLine _
                                              & " AND UNSO_M.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                              & " ORDER BY                                                               " & vbNewLine _
                                              & "     UNSO_NO_M                                                          " & vbNewLine

    'NOTES 1911 対応前(バックログ)
    '" SELECT                                                                 " & vbNewLine _
    '                                              & "     UNSO_M.*                                                           " & vbNewLine _
    '                                              & "    ,(CASE WHEN GDS.NRS_BR_CD IS NULL THEN '00'                         " & vbNewLine _
    '                                              & "           ELSE                            '01' END ) AS GOODS_MST_FLG  " & vbNewLine _
    '                                              & "    ,GDS.STD_IRIME_NB     AS STD_IRIME_NB                               " & vbNewLine _
    '                                              & "    ,GDS.STD_WT_KGS       AS STD_WT_KGS                                 " & vbNewLine _
    '                                              & "    ,ISNULL(KBN.VALUE1,0) AS TARE_WT                                    " & vbNewLine _
    '                                              & " FROM                                                                   " & vbNewLine _
    '                                              & "  $LM_TRN$..F_UNSO_M  AS UNSO_M                                         " & vbNewLine _
    '                                              & "  LEFT OUTER JOIN                                                       " & vbNewLine _
    '                                              & "     $LM_MST$..M_GOODS  AS GDS                                          " & vbNewLine _
    '                                              & "     ON  UNSO_M.NRS_BR_CD    = GDS.NRS_BR_CD                            " & vbNewLine _
    '                                              & "     AND UNSO_M.GOODS_CD_NRS = GDS.GOODS_CD_NRS                         " & vbNewLine _
    '                                              & "     AND GDS.SYS_DEL_FLG     = '0'                                      " & vbNewLine _
    '                                              & "  LEFT OUTER JOIN                                                       " & vbNewLine _
    '                                              & "     $LM_MST$..Z_KBN    AS KBN                                          " & vbNewLine _
    '                                              & "     ON  KBN.KBN_GROUP_CD    = 'N001'                                   " & vbNewLine _
    '                                              & "     AND KBN.KBN_CD          = UNSO_M.NB_UT                             " & vbNewLine _
    '                                              & "     AND KBN.SYS_DEL_FLG     = '0'                                      " & vbNewLine _
    '                                              & " WHERE                                                                  " & vbNewLine _
    '                                              & "     UNSO_M.UNSO_NO_L   = @UNSO_NO_L                                    " & vbNewLine _
    '                                              & " AND UNSO_M.SYS_DEL_FLG = '0'                                           " & vbNewLine _
    '                                              & " ORDER BY                                                               " & vbNewLine _
    '                                              & "     UNSO_NO_M                                                          " & vbNewLine

    ''' <summary>
    ''' [F_UNSO_M]運送Ｍ 集約情報取得用(運送単位)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_UNSOM_GROUP As String = " SELECT                                             " & vbNewLine _
                                                & "     ISNULL(SUM(UNSO_TTL_QT),0) AS UNSO_TTL_QT_SUM  " & vbNewLine _
                                                & " FROM                                               " & vbNewLine _
                                                & "     $LM_TRN$..F_UNSO_M                             " & vbNewLine _
                                                & " WHERE                                              " & vbNewLine _
                                                & "     UNSO_NO_L   = @UNSO_NO_L                       " & vbNewLine _
                                                & " AND SYS_DEL_FLG = '0'                              " & vbNewLine

    ''' <summary>
    ''' [F_UNSO_M]運送Ｍ 集約情報取得用(宅急便サイズ単位)
    ''' </summary>
    ''' <remarks>宅急便サイズの昇順でソートを行う。後続処理において、この順序で運賃テーブルを生成する。</remarks>
    Private Const SQL_GET_UNSOM_TAKKYU_GROUP As String = " SELECT                                                               " & vbNewLine _
                                                       & "    MAIN.NRS_BR_CD                                                    " & vbNewLine _
                                                       & "   ,MAIN.UNSO_NO_L                                                    " & vbNewLine _
                                                       & "   ,MAIN.UNSO_NO_M                                                    " & vbNewLine _
                                                       & "   ,MAIN.GOODS_CD_NRS                                                 " & vbNewLine _
                                                       & "   ,MAIN.GOODS_NM                                                     " & vbNewLine _
                                                       & "   ,SUB.UNSO_TTL_NB                                                   " & vbNewLine _
                                                       & "   ,SUB.NB_UT                                                         " & vbNewLine _
                                                       & "   ,MAIN.UNSO_TTL_QT                                                  " & vbNewLine _
                                                       & "   ,SUB.QT_UT                                                         " & vbNewLine _
                                                       & "   ,MAIN.HASU                                                         " & vbNewLine _
                                                       & "   ,MAIN.ZAI_REC_NO                                                   " & vbNewLine _
                                                       & "   ,MAIN.UNSO_ONDO_KB                                                 " & vbNewLine _
                                                       & "   ,MAIN.IRIME                                                        " & vbNewLine _
                                                       & "   ,MAIN.IRIME_UT                                                     " & vbNewLine _
                                                       & "   ,SUB.BETU_WT                                                       " & vbNewLine _
                                                       & "   ,SUB.SIZE_KB                                                       " & vbNewLine _
                                                       & "   ,MAIN.ZBUKA_CD                                                     " & vbNewLine _
                                                       & "   ,MAIN.ABUKA_CD                                                     " & vbNewLine _
                                                       & "   ,MAIN.PKG_NB                                                       " & vbNewLine _
                                                       & "   ,MAIN.LOT_NO                                                       " & vbNewLine _
                                                       & "   ,MAIN.REMARK                                                       " & vbNewLine _
                                                       & "   ,(CASE WHEN GDS.NRS_BR_CD IS NULL THEN '00'                        " & vbNewLine _
                                                       & "          ELSE                            '01' END ) AS GOODS_MST_FLG " & vbNewLine _
                                                       & "   ,GDS.STD_IRIME_NB     AS STD_IRIME_NB                              " & vbNewLine _
                                                       & "   ,GDS.STD_WT_KGS       AS STD_WT_KGS                                " & vbNewLine _
                                                       & "   ,ISNULL(KBN.VALUE1,0) AS TARE_WT                                   " & vbNewLine _
                                                       & " --★2013.03.28　アベンド修正　開始　　　                             " & vbNewLine _
                                                       & "   ,GDS.TARE_YN          AS GOODS_TARE_YN                             " & vbNewLine _
                                                       & "   ,''                   AS ALCTD_KB                                  " & vbNewLine _
                                                       & " --★2013.03.28　アベンド修正　終了　　　                             " & vbNewLine _
                                                       & " FROM                                                                 " & vbNewLine _
                                                       & "     $LM_TRN$..F_UNSO_M  MAIN                                         " & vbNewLine _
                                                       & "     INNER JOIN                                                       " & vbNewLine _
                                                       & "     (   SELECT                                                       " & vbNewLine _
                                                       & "             UNSO_NO_L                                                " & vbNewLine _
                                                       & "            ,SIZE_KB                                                  " & vbNewLine _
                                                       & "            ,ISNULL(MIN(UNSO_NO_M),000) AS UNSO_NO_M                  " & vbNewLine _
                                                       & "            ,ISNULL(SUM(UNSO_TTL_NB),0) AS UNSO_TTL_NB                " & vbNewLine _
                                                       & "            ,ISNULL(MIN(BETU_WT),0)     AS BETU_WT                    " & vbNewLine _
                                                       & "            ,ISNULL(MIN(NB_UT),0)       AS NB_UT                      " & vbNewLine _
                                                       & "            ,ISNULL(MIN(QT_UT),0)       AS QT_UT                      " & vbNewLine _
                                                       & "         FROM                                                         " & vbNewLine _
                                                       & "             $LM_TRN$..F_UNSO_M                                       " & vbNewLine _
                                                       & "         WHERE                                                        " & vbNewLine _
                                                       & "             UNSO_NO_L   = @UNSO_NO_L                                 " & vbNewLine _
                                                       & "         AND SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                                       & "         GROUP BY                                                     " & vbNewLine _
                                                       & "             UNSO_NO_L                                                " & vbNewLine _
                                                       & "            ,SIZE_KB                                                  " & vbNewLine _
                                                       & "     )  SUB                                                           " & vbNewLine _
                                                       & "     ON  MAIN.UNSO_NO_L   = SUB.UNSO_NO_L                             " & vbNewLine _
                                                       & "     AND MAIN.UNSO_NO_M   = SUB.UNSO_NO_M                             " & vbNewLine _
                                                       & "     AND MAIN.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                                       & "     LEFT OUTER JOIN                                                  " & vbNewLine _
                                                       & "        $LM_MST$..M_GOODS  AS GDS                                     " & vbNewLine _
                                                       & "        ON  MAIN.NRS_BR_CD    = GDS.NRS_BR_CD                         " & vbNewLine _
                                                       & "        AND MAIN.GOODS_CD_NRS = GDS.GOODS_CD_NRS                      " & vbNewLine _
                                                       & "        AND GDS.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
                                                       & "     LEFT OUTER JOIN                                                  " & vbNewLine _
                                                       & "        $LM_MST$..Z_KBN    AS KBN                                     " & vbNewLine _
                                                       & "        ON  KBN.KBN_GROUP_CD    = 'N001'                              " & vbNewLine _
                                                       & "        AND KBN.KBN_CD          = MAIN.NB_UT                          " & vbNewLine _
                                                       & "        AND KBN.SYS_DEL_FLG     = '0'                                 " & vbNewLine _
                                                       & " ORDER BY                                                             " & vbNewLine _
                                                       & "     SUB.SIZE_KB                                                      " & vbNewLine

    ''' <summary>
    ''' [F_UNCHIN_TRS]請求グループ番号設定レコード件数取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_GROUP_REC_COUNT As String = " SELECT                                                " & vbNewLine _
                                                    & "    GRUP.SEIQ_GROUP_REC_COUNT  AS SEIQ_GROUP_REC_COUNT " & vbNewLine _
                                                    & "   ,DECI.DECI_REC_COUNT        AS DECI_REC_COUNT       " & vbNewLine _
                                                    & " FROM                                                  " & vbNewLine _
                                                    & " (                                                     " & vbNewLine _
                                                    & "     SELECT                                            " & vbNewLine _
                                                    & "         COUNT(*) AS SEIQ_GROUP_REC_COUNT              " & vbNewLine _
                                                    & "     FROM                                              " & vbNewLine _
                                                    & "         $LM_TRN$..F_UNCHIN_TRS                        " & vbNewLine _
                                                    & "     WHERE                                             " & vbNewLine _
                                                    & "         UNSO_NO_L      = @UNSO_NO_L                   " & vbNewLine _
                                                    & "     AND SEIQ_GROUP_NO <> ''                           " & vbNewLine _
                                                    & "     AND SYS_DEL_FLG    = '0'                          " & vbNewLine _
                                                    & " ) GRUP                                                " & vbNewLine _
                                                    & " ,                                                     " & vbNewLine _
                                                    & " (                                                     " & vbNewLine _
                                                    & "     SELECT                                            " & vbNewLine _
                                                    & "         COUNT(*) AS DECI_REC_COUNT                    " & vbNewLine _
                                                    & "     FROM                                              " & vbNewLine _
                                                    & "         $LM_TRN$..F_UNCHIN_TRS                        " & vbNewLine _
                                                    & "     WHERE                                             " & vbNewLine _
                                                    & "         UNSO_NO_L       = @UNSO_NO_L                  " & vbNewLine _
                                                    & "     AND SEIQ_FIXED_FLAG = '01'                        " & vbNewLine _
                                                    & "     AND SYS_DEL_FLG     = '0'                         " & vbNewLine _
                                                    & " ) DECI                                                " & vbNewLine

    ''' <summary>
    ''' [M_GOODS, M_CUST]商品・荷主関連 情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_GOODS_CUST As String = " SELECT                                                 " & vbNewLine _
                                               & "     GDS.CUST_CD_S              AS CUST_CD_S            " & vbNewLine _
                                               & "    ,GDS.CUST_CD_SS             AS CUST_CD_SS           " & vbNewLine _
                                               & "    ,GDS.KIKEN_KB               AS KIKEN_KB             " & vbNewLine _
                                               & "--2014/5/16Start s.kobayashi NotesNo.2183               " & vbNewLine _
                                               & "--    ,CST.BETU_KYORI_CD          AS BETU_KYORI_CD        " & vbNewLine _
                                               & "    ,CASE WHEN (ISNULL(KBN.KBN_NM2,'') <> '') THEN KBN.KBN_NM2" & vbNewLine _
                                               & "     ELSE CST.BETU_KYORI_CD END         AS BETU_KYORI_CD " & vbNewLine _
                                               & "--2014/5/16End s.kobayashi NotesNo.2183                 " & vbNewLine _
                                               & "    ,CST.UNCHIN_SEIQTO_CD       AS UNCHIN_SEIQTO_CD     " & vbNewLine _
                                               & "    ,CST.UNTIN_CALCULATION_KB   AS UNTIN_CALCULATION_KB " & vbNewLine _
                                               & " FROM                                                   " & vbNewLine _
                                               & "     $LM_TRN$..F_UNSO_M     AS UNS                      " & vbNewLine _
                                               & "--2014/5/16Start s.kobayashi NotesNo.2183               " & vbNewLine _
                                               & "     INNER JOIN                                         " & vbNewLine _
                                               & "         $LM_TRN$..F_UNSO_L  AS UNL                     " & vbNewLine _
                                               & "     ON  UNS.UNSO_NO_L    = UNL.UNSO_NO_L               " & vbNewLine _
                                               & "     AND UNL.SYS_DEL_FLG  = '0'                         " & vbNewLine _
                                               & "--2014/5/16End s.kobayashi NotesNo.2183                 " & vbNewLine _
                                               & "     INNER JOIN                                         " & vbNewLine _
                                               & "         $LM_MST$..M_GOODS  AS GDS                      " & vbNewLine _
                                               & "     ON  UNS.NRS_BR_CD    = GDS.NRS_BR_CD               " & vbNewLine _
                                               & "     AND UNS.GOODS_CD_NRS = GDS.GOODS_CD_NRS            " & vbNewLine _
                                               & "     AND GDS.SYS_DEL_FLG  = '0'                         " & vbNewLine _
                                               & "     INNER JOIN                                         " & vbNewLine _
                                               & "         $LM_MST$..M_CUST   AS CST                      " & vbNewLine _
                                               & "     ON  GDS.NRS_BR_CD    = CST.NRS_BR_CD               " & vbNewLine _
                                               & "     AND GDS.CUST_CD_L    = CST.CUST_CD_L               " & vbNewLine _
                                               & "     AND GDS.CUST_CD_M    = CST.CUST_CD_M               " & vbNewLine _
                                               & "     AND GDS.CUST_CD_S    = CST.CUST_CD_S               " & vbNewLine _
                                               & "     AND GDS.CUST_CD_SS   = CST.CUST_CD_SS              " & vbNewLine _
                                               & "     AND CST.SYS_DEL_FLG  = '0'                         " & vbNewLine _
                                               & "--2014/5/16Start s.kobayashi NotesNo.2183               " & vbNewLine _
                                               & "     LEFT JOIN                                          " & vbNewLine _
                                               & "         $LM_MST$..Z_KBN   AS KBN                       " & vbNewLine _
                                               & "     ON  KBN.KBN_GROUP_CD   = 'U028'                     " & vbNewLine _
                                               & "     AND UNL.SEIQ_TARIFF_CD = KBN.KBN_NM1              " & vbNewLine _
                                               & "     AND KBN.SYS_DEL_FLG    = '0'                       " & vbNewLine _
                                               & "--2014/5/16End s.kobayashi NotesNo.2183                 " & vbNewLine _
                                               & " WHERE                                                  " & vbNewLine _
                                               & "     UNS.UNSO_NO_L    = @UNSO_NO_L                      " & vbNewLine _
                                               & " AND UNS.UNSO_NO_M    = @UNSO_NO_M                      " & vbNewLine _
                                               & " AND UNS.SYS_DEL_FLG  = '0'                             " & vbNewLine

    ''' <summary>
    ''' [M_CUST]商品・荷主関連 情報取得用②
    ''' </summary>
    ''' <remarks>
    ''' 2014/5/16s.kobayashi NotesNo.2183 検索条件を荷主コード→運送番号に変更。よってBaseTableをM_CUSTからF_UNSO_Lに変更
    ''' </remarks>
    Private Const SQL_GET_GOODS_CUST2 As String = "SELECT                                                                                                        " & vbNewLine _
                                                & "     '00'                   AS CUST_CD_S                                                                      " & vbNewLine _
                                                & "    ,'00'                   AS CUST_CD_SS                                                                     " & vbNewLine _
                                                & "    ,'01'                   AS KIKEN_KB                                                                       " & vbNewLine _
                                                & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                     " & vbNewLine _
                                                & "    ,CASE WHEN (ISNULL(KBN.KBN_NM2,'') <> '') THEN KBN.KBN_NM2                                                " & vbNewLine _
                                                & "     ELSE CST.BETU_KYORI_CD END         AS BETU_KYORI_CD                                                      " & vbNewLine _
                                                & "--2014/5/16End s.kobayashi NotesNo.2183                                                                       " & vbNewLine _
                                                & "    ,CST.UNCHIN_SEIQTO_CD       AS UNCHIN_SEIQTO_CD                                                           " & vbNewLine _
                                                & "    ,CST.UNTIN_CALCULATION_KB   AS UNTIN_CALCULATION_KB                                                       " & vbNewLine _
                                                & " FROM                                                                                                         " & vbNewLine _
                                                & "--------------------------------------------                                                                  " & vbNewLine _
                                                & "     $LM_TRN$..F_UNSO_L     AS UNL                                                                            " & vbNewLine _
                                                & "     INNER JOIN                                                                                               " & vbNewLine _
                                                & "     $LM_MST$..M_CUST         AS CST                                                                          " & vbNewLine _
                                                & "	 ON  UNL.NRS_BR_CD = CST.NRS_BR_CD                                                                           " & vbNewLine _
                                                & "	 AND UNL.CUST_CD_L = CST.CUST_CD_L                                                                           " & vbNewLine _
                                                & "	 AND UNL.CUST_CD_M = CST.CUST_CD_M                                                                           " & vbNewLine _
                                                & "	 AND CST.CUST_CD_S    = '00'                                                                                 " & vbNewLine _
                                                & "	 AND CST.CUST_CD_SS   = '00'                                                                                 " & vbNewLine _
                                                & "	 AND CST.SYS_DEL_FLG  = '0'                                                                                  " & vbNewLine _
                                                & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                     " & vbNewLine _
                                                & "     LEFT JOIN                                                                                                " & vbNewLine _
                                                & "         $LM_MST$..Z_KBN   AS KBN                                                                             " & vbNewLine _
                                                & "     ON  KBN.KBN_GROUP_CD   = 'U028'                                                                           " & vbNewLine _
                                                & "     AND UNL.SEIQ_TARIFF_CD = KBN.KBN_NM1                                                                    " & vbNewLine _
                                                & "     AND KBN.SYS_DEL_FLG    = '0'                                                                             " & vbNewLine _
                                                & "--2014/5/16End s.kobayashi NotesNo.2183                                                                       " & vbNewLine _
                                                & " WHERE                                                                                                        " & vbNewLine _
                                                & "     UNL.UNSO_NO_L    = @UNSO_NO_L                                                                            " & vbNewLine _
                                                & " AND UNL.SYS_DEL_FLG  = '0'                                                                                   " & vbNewLine

    'Private Const SQL_GET_GOODS_CUST2 As String = " SELECT                                                 " & vbNewLine _
    '                                            & "     '00'                   AS CUST_CD_S                " & vbNewLine _
    '                                            & "    ,'00'                   AS CUST_CD_SS               " & vbNewLine _
    '                                            & "    ,'01'                   AS KIKEN_KB                 " & vbNewLine _
    '                                            & "    ,BETU_KYORI_CD          AS BETU_KYORI_CD            " & vbNewLine _
    '                                            & "    ,UNCHIN_SEIQTO_CD       AS UNCHIN_SEIQTO_CD         " & vbNewLine _
    '                                            & "    ,UNTIN_CALCULATION_KB   AS UNTIN_CALCULATION_KB     " & vbNewLine _
    '                                            & " FROM                                                   " & vbNewLine _
    '                                            & "     $LM_MST$..M_CUST                                   " & vbNewLine _
    '                                            & " WHERE                                                  " & vbNewLine _
    '                                            & "     NRS_BR_CD    = @NRS_BR_CD                          " & vbNewLine _
    '                                            & " AND CUST_CD_L    = @CUST_CD_L                          " & vbNewLine _
    '                                            & " AND CUST_CD_M    = @CUST_CD_M                          " & vbNewLine _
    '                                            & " AND CUST_CD_S    = '00'                                " & vbNewLine _
    '                                            & " AND CUST_CD_SS   = '00'                                " & vbNewLine _
    '                                            & " AND SYS_DEL_FLG  = '0'                                 " & vbNewLine

    ''' <summary>
    ''' [M_DEST]距離 情報取得用（From 届先Ｍ）
    ''' </summary>
    ''' <remarks>必ず１行抽出するため "GET_FLG" を追加している。</remarks>
    Private Const SQL_GET_KYORI_DEST_MST As String = " SELECT                        " & vbNewLine _
                                                   & "     '01' AS GET_FLG           " & vbNewLine _
                                                   & "    ,KYORI                     " & vbNewLine _
                                                   & "    ,UNCHIN_SEIQTO_CD          " & vbNewLine _
                                                   & "    ,JIS                       " & vbNewLine _
                                                   & " FROM                          " & vbNewLine _
                                                   & "     $LM_MST$..M_DEST          " & vbNewLine _
                                                   & " WHERE                         " & vbNewLine _
                                                   & "     NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                                   & " AND CUST_CD_L   = @CUST_CD_L  " & vbNewLine _
                                                   & " AND DEST_CD     = @DEST_CD    " & vbNewLine _
                                                   & " AND SYS_DEL_FLG = '0'         " & vbNewLine

    ''' <summary>
    ''' [M_DEST, M_SOKO]ＪＩＳ 情報取得用（入荷）
    ''' </summary>
    ''' <remarks>届先は倉庫</remarks>
    Private Const SQL_GET_NYUKA_JIS As String = " SELECT                                          " & vbNewLine _
                                              & "     ORIG.JIS                      AS ORIG_JIS   " & vbNewLine _
                                              & "   ,(SELECT JIS_CD                               " & vbNewLine _
                                              & "     FROM   $LM_MST$..M_SOKO                     " & vbNewLine _
                                              & "     WHERE  NRS_BR_CD   = @NRS_BR_CD             " & vbNewLine _
                                              & "     AND    WH_CD       = @WH_CD                 " & vbNewLine _
                                              & "     AND    SYS_DEL_FLG = '0'   )  AS DEST_JIS   " & vbNewLine _
                                              & " FROM                                            " & vbNewLine _
                                              & "     $LM_MST$..M_DEST  AS  ORIG                  " & vbNewLine _
                                              & " WHERE                                           " & vbNewLine _
                                              & "     ORIG.NRS_BR_CD   = @NRS_BR_CD               " & vbNewLine _
                                              & " AND ORIG.CUST_CD_L   = @CUST_CD_L               " & vbNewLine _
                                              & " AND ORIG.DEST_CD     = @ORIG_CD                 " & vbNewLine _
                                              & " AND ORIG.SYS_DEL_FLG = '0'                      " & vbNewLine

    ''' <summary>
    ''' [M_DEST, M_SOKO]ＪＩＳ 情報取得用（出荷）
    ''' </summary>
    ''' <remarks>発地は倉庫</remarks>
    Private Const SQL_GET_SHUKA_JIS As String = " SELECT                                          " & vbNewLine _
                                              & "    (SELECT JIS_CD                               " & vbNewLine _
                                              & "     FROM   $LM_MST$..M_SOKO                     " & vbNewLine _
                                              & "     WHERE  NRS_BR_CD   = @NRS_BR_CD             " & vbNewLine _
                                              & "     AND    WH_CD       = @WH_CD                 " & vbNewLine _
                                              & "     AND    SYS_DEL_FLG = '0'   )  AS ORIG_JIS   " & vbNewLine _
                                              & "    ,DEST.JIS                      AS DEST_JIS   " & vbNewLine _
                                              & " FROM                                            " & vbNewLine _
                                              & "     $LM_MST$..M_DEST  AS  DEST                  " & vbNewLine _
                                              & " WHERE                                           " & vbNewLine _
                                              & "     DEST.NRS_BR_CD   = @NRS_BR_CD               " & vbNewLine _
                                              & " AND DEST.CUST_CD_L   = @CUST_CD_L               " & vbNewLine _
                                              & " AND DEST.DEST_CD     = @DEST_CD                 " & vbNewLine _
                                              & " AND DEST.SYS_DEL_FLG = '0'                      " & vbNewLine

    ''' <summary>
    ''' [M_DEST]ＪＩＳ 情報取得用（運送）
    ''' </summary>
    ''' <remarks>2013/06/10 要望管理2052 ZZZZZ対応により複数件取得可能性があるので、'ZZZZZ'を条件に追加</remarks>
    Private Const SQL_GET_UNSOU_JIS As String = " SELECT                                                                                                          " & vbNewLine _
                                            & "   CASE                                                                                                            " & vbNewLine _
                                            & "     WHEN NOT ORIG.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017')                  " & vbNewLine _
                                            & "          AND NOT DEST.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017') THEN 0       " & vbNewLine _
                                            & "     WHEN NOT ORIG.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017')                  " & vbNewLine _
                                            & "          AND DEST.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017') THEN 1           " & vbNewLine _
                                            & "     WHEN ORIG.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017')                      " & vbNewLine _
                                            & "          AND NOT DEST.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017') THEN 2       " & vbNewLine _
                                            & "     ELSE 3 END SORTNO                                                                                             " & vbNewLine _
                                            & "    ,ORIG.JIS      AS  ORIG_JIS                                                                                    " & vbNewLine _
                                            & "    ,DEST.JIS      AS  DEST_JIS                                                                                    " & vbNewLine _
                                            & " FROM                                                                                                              " & vbNewLine _
                                            & "     $LM_MST$..M_DEST  AS  ORIG                                                                                  " & vbNewLine _
                                            & "    ,$LM_MST$..M_DEST  AS  DEST                                                                                  " & vbNewLine _
                                            & " WHERE                                                                                                             " & vbNewLine _
                                            & "     ORIG.NRS_BR_CD   = @NRS_BR_CD                                                                                 " & vbNewLine _
                                            & " AND (ORIG.CUST_CD_L   = @CUST_CD_L                                                                                " & vbNewLine _
                                            & "     OR ORIG.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017'))                       " & vbNewLine _
                                            & " AND ORIG.DEST_CD     = @ORIG_CD                                                                                   " & vbNewLine _
                                            & " AND ORIG.SYS_DEL_FLG = '0'                                                                                        " & vbNewLine _
                                            & " AND DEST.NRS_BR_CD   = @NRS_BR_CD                                                                                 " & vbNewLine _
                                            & " AND (DEST.CUST_CD_L  = @CUST_CD_L                                                                                 " & vbNewLine _
                                            & "     OR DEST.CUST_CD_L IN (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'T017'))                       " & vbNewLine _
                                            & " AND DEST.DEST_CD     = @DEST_CD                                                                                   " & vbNewLine _
                                            & " AND DEST.SYS_DEL_FLG = '0'                                                                                        " & vbNewLine


    ''' <summary>
    ''' [M_DEST]距離 情報取得用－ヘッダ（From 距離Ｍ）
    ''' </summary>
    ''' <remarks>2013/06/10 要望管理2052 ZZZZZ対応により複数件取得可能性があるので、TOP1を追加</remarks>
    Private Const SQL_GET_KYORI_MST_HDR As String = " SELECT TOP 1                           " & vbNewLine _
                                                  & "     KYORI                         " & vbNewLine _
                                                  & " FROM                              " & vbNewLine _
                                                  & "     $LM_MST$..M_KYORI             " & vbNewLine _
                                                  & "    ,(                             " & vbNewLine

    ''' <summary>
    ''' [M_DEST]距離 情報取得用－明細　（From 距離Ｍ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_KYORI_MST_DTL As String = "     ) JIS                         " & vbNewLine _
                                                  & " WHERE                             " & vbNewLine _
                                                  & "     NRS_BR_CD    = @NRS_BR_CD     " & vbNewLine _
                                                  & " AND KYORI_CD     = @KYORI_CD      " & vbNewLine _
                                                  & " AND SYS_DEL_FLG  = '0'            " & vbNewLine _
                                                  & " AND ORIG_JIS_CD  = ( CASE WHEN JIS.ORIG_JIS <= JIS.DEST_JIS THEN JIS.ORIG_JIS ELSE JIS.DEST_JIS END) " & vbNewLine _
                                                  & " AND DEST_JIS_CD  = ( CASE WHEN JIS.ORIG_JIS <= JIS.DEST_JIS THEN JIS.DEST_JIS ELSE JIS.ORIG_JIS END) " & vbNewLine

    ''' <summary>
    ''' [M_DEST]距離 ORDER BY(元データが運送の場合のみ）
    ''' </summary>
    ''' <remarks>2013/06/10 要望管理2052 ZZZZZ対応により複数件取得可能性があるので、ソートを設定</remarks>
    Private Const SQL_GET_KYORI_MST_ORDER As String = " ORDER BY SORTNO                           " & vbNewLine

    ''' <summary>
    ''' [M_UNCHIN_TARIFF]テーブルタイプ 取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_TABLE_TYPE As String = " SELECT DISTINCT                          " & vbNewLine _
                                               & "     TABLE_TP                             " & vbNewLine _
                                               & " FROM                                     " & vbNewLine _
                                               & "     $LM_MST$..M_UNCHIN_TARIFF            " & vbNewLine _
                                               & " WHERE                                    " & vbNewLine _
                                               & "     NRS_BR_CD        = @NRS_BR_CD        " & vbNewLine _
                                               & " AND UNCHIN_TARIFF_CD = @UNCHIN_TARIFF_CD " & vbNewLine _
                                               & " AND DATA_TP          = @DATA_TP          " & vbNewLine _
                                               & " AND SYS_DEL_FLG      = '0'               " & vbNewLine

    ''' <summary>
    ''' [M_YOKO_TARIFF_HD]横持ち運賃タリフヘッダ 取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_YOKO_HD As String = " SELECT                                 " & vbNewLine _
                                            & "     *                                  " & vbNewLine _
                                            & " FROM                                   " & vbNewLine _
                                            & "     $LM_MST$..M_YOKO_TARIFF_HD         " & vbNewLine _
                                            & " WHERE                                  " & vbNewLine _
                                            & "     NRS_BR_CD      = @NRS_BR_CD        " & vbNewLine _
                                            & " AND YOKO_TARIFF_CD = @YOKO_TARIFF_CD   " & vbNewLine _
                                            & " AND SYS_DEL_FLG    = '0'               " & vbNewLine

    ''' <summary>
    ''' [Z_KBN]特定荷主判断
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_SPECIFIC_CUST As String = " SELECT                           " & vbNewLine _
                                                  & "     VALUE1                       " & vbNewLine _
                                                  & " FROM                             " & vbNewLine _
                                                  & "     $LM_MST$..Z_KBN              " & vbNewLine _
                                                  & " WHERE                            " & vbNewLine _
                                                  & "     KBN_GROUP_CD = 'U014'        " & vbNewLine _
                                                  & " AND KBN_NM1      = @NRS_BR_CD    " & vbNewLine _
                                                  & " AND KBN_NM2      = @MOTO_DATA_KB " & vbNewLine _
                                                  & " AND KBN_NM3      = @CUST_CD_L    " & vbNewLine _
                                                  & " AND SYS_DEL_FLG  = '0'           " & vbNewLine

    ''' <summary>
    ''' [F_UNSO_M]貨物情報集約（請求先単位）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_REV_UNIT As String = " SELECT                                                                                           " & vbNewLine _
                                             & "     MAIN.UNSO_NO_L              AS UNSO_NO_L                                                     " & vbNewLine _
                                             & "    ,MAIN.UNSO_NO_M              AS UNSO_NO_M                                                     " & vbNewLine _
                                             & "    ,MAIN.SEIQTO_CD              AS SEIQTO_CD                                                     " & vbNewLine _
                                             & "    ,MAIN.SIZE_KB                AS SIZE_KB                                                       " & vbNewLine _
                                             & "    ,MAIN.UNSO_TTL_NB            AS UNSO_TTL_NB                                                   " & vbNewLine _
                                             & "    ,MAIN.BETU_WT                AS BETU_WT                                                       " & vbNewLine _
                                             & "    ,CUST.UNTIN_CALCULATION_KB   AS UNTIN_CALCULATION_KB                                          " & vbNewLine _
                                             & "    ,CUST.CUST_CD_S              AS CUST_CD_S                                                     " & vbNewLine _
                                             & "    ,CUST.CUST_CD_SS             AS CUST_CD_SS                                                    " & vbNewLine _
                                             & " FROM                                                                                             " & vbNewLine _
                                             & "    (                                                                                             " & vbNewLine _
                                             & "     SELECT                                                                                       " & vbNewLine _
                                             & "         UNSO_NO_L       AS UNSO_NO_L                                                             " & vbNewLine _
                                             & "        ,MIN(UNSO_NO_M)     AS UNSO_NO_M                                                          " & vbNewLine _
                                             & "        ,SEIQTO_CD          AS SEIQTO_CD                                                          " & vbNewLine _
                                             & "        ,SIZE_KB            AS SIZE_KB                                                            " & vbNewLine _
                                             & "        ,SUM(UNSO_TTL_NB)   AS UNSO_TTL_NB                                                        " & vbNewLine _
                                             & "        ,SUM(BETU_WT)       AS BETU_WT                                                            " & vbNewLine _
                                             & "     FROM                                                                                         " & vbNewLine _
                                             & "         (                                                                                        " & vbNewLine _
                                             & "         SELECT                                                                                   " & vbNewLine _
                                             & "             UNSO_M.UNSO_NO_L        AS UNSO_NO_L                                                 " & vbNewLine _
                                             & "            ,UNSO_M.UNSO_NO_M        AS UNSO_NO_M                                                 " & vbNewLine _
                                             & "            ,CUST.UNCHIN_SEIQTO_CD   AS SEIQTO_CD                                                 " & vbNewLine _
                                             & "            ,UNSO_M.UNSO_TTL_NB      AS UNSO_TTL_NB                                               " & vbNewLine _
                                             & "            ,UNSO_M.SIZE_KB          AS SIZE_KB                                                   " & vbNewLine _
                                             & "            ,CASE GOODS.TARE_YN                                                                   " & vbNewLine _
                                             & "             WHEN '00' THEN                                                                       " & vbNewLine _
                                             & "                (   GOODS.STD_WT_KGS * UNSO_M.IRIME / GOODS.STD_IRIME_NB )                        " & vbNewLine _
                                             & "                 * (UNSO_M.PKG_NB * UNSO_M.UNSO_TTL_NB + UNSO_M.HASU )                            " & vbNewLine _
                                             & "             WHEN '01' THEN                                                                       " & vbNewLine _
                                             & "                (   GOODS.STD_WT_KGS * UNSO_M.IRIME / GOODS.STD_IRIME_NB + ISNULL(KBN.KBN_NM1,0)) " & vbNewLine _
                                             & "                 * (UNSO_M.PKG_NB * UNSO_M.UNSO_TTL_NB + UNSO_M.HASU )                            " & vbNewLine _
                                             & "             END                     AS BETU_WT                                                   " & vbNewLine _
                                             & "         FROM                                                                                     " & vbNewLine _
                                             & "             $LM_MST$..M_CUST   CUST                                                              " & vbNewLine _
                                             & "             INNER JOIN  $LM_MST$..M_GOODS  GOODS                                                 " & vbNewLine _
                                             & "             ON  CUST.NRS_BR_CD      = GOODS.NRS_BR_CD                                            " & vbNewLine _
                                             & "             AND CUST.CUST_CD_L      = GOODS.CUST_CD_L                                            " & vbNewLine _
                                             & "             AND CUST.CUST_CD_M      = GOODS.CUST_CD_M                                            " & vbNewLine _
                                             & "             AND CUST.CUST_CD_S      = GOODS.CUST_CD_S                                            " & vbNewLine _
                                             & "             AND CUST.CUST_CD_SS     = GOODS.CUST_CD_SS                                           " & vbNewLine _
                                             & "             AND GOODS.SYS_DEL_FLG   = '0'                                                        " & vbNewLine _
                                             & "             INNER JOIN  $LM_TRN$..F_UNSO_M  UNSO_M                                               " & vbNewLine _
                                             & "             ON  GOODS.NRS_BR_CD     = UNSO_M.NRS_BR_CD                                           " & vbNewLine _
                                             & "             AND GOODS.GOODS_CD_NRS  = UNSO_M.GOODS_CD_NRS                                        " & vbNewLine _
                                             & "             AND UNSO_M.SYS_DEL_FLG  = '0'                                                        " & vbNewLine _
                                             & "             INNER JOIN  $LM_TRN$..F_UNSO_L  UNSO_L                                               " & vbNewLine _
                                             & "             ON  UNSO_M.UNSO_NO_L    = UNSO_L.UNSO_NO_L                                           " & vbNewLine _
                                             & "             AND UNSO_L.SYS_DEL_FLG  = '0'                                                        " & vbNewLine _
                                             & "             LEFT OUTER JOIN  $LM_MST$..Z_KBN  KBN                                                " & vbNewLine _
                                             & "             ON  KBN.KBN_GROUP_CD    = 'N001'                                                     " & vbNewLine _
                                             & "             AND KBN.KBN_CD          = UNSO_M.IRIME_UT                                            " & vbNewLine _
                                             & "             AND KBN.SYS_DEL_FLG     = '0'                                                        " & vbNewLine _
                                             & "         WHERE                                                                                    " & vbNewLine _
                                             & "             CUST.SYS_DEL_FLG        = '0'                                                        " & vbNewLine _
                                             & "         AND UNSO_L.UNSO_NO_L        = @UNSO_NO_L                                                 " & vbNewLine _
                                             & "         ) MAIN_PRE                                                                               " & vbNewLine _
                                             & "     GROUP BY                                                                                     " & vbNewLine _
                                             & "         UNSO_NO_L                                                                                " & vbNewLine _
                                             & "        ,SEIQTO_CD                                                                                " & vbNewLine _
                                             & "        ,SIZE_KB                                                                                  " & vbNewLine _
                                             & "    ) MAIN                                                                                        " & vbNewLine _
                                             & "     INNER JOIN  $LM_TRN$..F_UNSO_M SUB                                                           " & vbNewLine _
                                             & "     ON  SUB.UNSO_NO_L      = MAIN.UNSO_NO_L                                                      " & vbNewLine _
                                             & "     AND SUB.UNSO_NO_M      = MAIN.UNSO_NO_M                                                      " & vbNewLine _
                                             & "     AND SUB.SYS_DEL_FLG    = '0'                                                                 " & vbNewLine _
                                             & "     INNER JOIN  $LM_MST$..M_GOODS  GOODS                                                         " & vbNewLine _
                                             & "     ON  GOODS.NRS_BR_CD    = SUB.NRS_BR_CD                                                       " & vbNewLine _
                                             & "     AND GOODS.GOODS_CD_NRS = SUB.GOODS_CD_NRS                                                    " & vbNewLine _
                                             & "     AND GOODS.SYS_DEL_FLG  = '0'                                                                 " & vbNewLine _
                                             & "     INNER JOIN  $LM_MST$..M_CUST   CUST                                                          " & vbNewLine _
                                             & "     ON  CUST.NRS_BR_CD     = GOODS.NRS_BR_CD                                                     " & vbNewLine _
                                             & "     AND CUST.CUST_CD_L     = GOODS.CUST_CD_L                                                     " & vbNewLine _
                                             & "     AND CUST.CUST_CD_M     = GOODS.CUST_CD_M                                                     " & vbNewLine _
                                             & "     AND CUST.CUST_CD_S     = GOODS.CUST_CD_S                                                     " & vbNewLine _
                                             & "     AND CUST.CUST_CD_SS    = GOODS.CUST_CD_SS                                                    " & vbNewLine _
                                             & "     AND CUST.SYS_DEL_FLG   = '0'                                                                 " & vbNewLine _
                                             & " ORDER BY                                                                                         " & vbNewLine _
                                             & "     MAIN.SIZE_KB                                                                                 " & vbNewLine _
                                             & "    ,MAIN.SEIQTO_CD                                                                               " & vbNewLine

    '2013.03.26 / NOTES 1911 追加 開始
    Private Const SQL_GET_ALCTD As String = "   SELECT                                             " & vbNewLine _
                                            & " OUTKA_M.ALCTD_KB               AS ALCTD_KB            " & vbNewLine _
                                            & " FROM      $LM_TRN$..F_UNSO_L   AS UNSO_L              " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..F_UNSO_M   AS UNSO_M              " & vbNewLine _
                                            & "        ON                                             " & vbNewLine _
                                            & "           UNSO_M.NRS_BR_CD    = UNSO_L.NRS_BR_CD      " & vbNewLine _
                                            & "       AND UNSO_M.UNSO_NO_L    = UNSO_L.UNSO_NO_L      " & vbNewLine _
                                            & "       AND UNSO_M.SYS_DEL_FLG  = '0'                   " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..C_OUTKA_M  AS OUTKA_M             " & vbNewLine _
                                            & "        ON                                             " & vbNewLine _
                                            & "           UNSO_L.NRS_BR_CD    = OUTKA_M.NRS_BR_CD     " & vbNewLine _
                                            & "       AND UNSO_L.INOUTKA_NO_L = OUTKA_M.OUTKA_NO_L    " & vbNewLine _
                                            & "       AND UNSO_M.UNSO_NO_M    = OUTKA_M.OUTKA_NO_M    " & vbNewLine _
                                            & "       AND OUTKA_M.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                            & " WHERE                                                 " & vbNewLine _
                                            & "     UNSO_L.NRS_BR_CD          = @NRS_BR_CD            " & vbNewLine _
                                            & " AND UNSO_L.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                            & " AND UNSO_L.UNSO_NO_L          = @UNSO_NO_L            " & vbNewLine _
                                            & " AND UNSO_M.UNSO_NO_M          = @UNSO_NO_M            " & vbNewLine _
                                            & " AND UNSO_L.MOTO_DATA_KB       = '20'                  " & vbNewLine
    '2013.03.26 / NOTES 1911 追加 開始

    'ADD 2018/10/22 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる
    Private Const SQL_GET_HOKENYO As String = " SELECT                                             " & vbNewLine _
                                             & "    UNSOL.NRS_BR_CD                                    " & vbNewLine _
                                             & "   ,UNSOL.UNSO_NO_L                                    " & vbNewLine _
                                             & "   ,SUM(ROUND(((COM.OUTKA_TTL_QT * GOODS.KITAKU_GOODS_UP ) * KBN1.KBN_NM2),0)) AS HOKENRYO          --保険料   " & vbNewLine _
                                             & "   ,MIN(ISNULL(MCD1.SET_NAIYO,''))     AS SET1_NAIYO1  " & vbNewLine _
                                             & "   ,MIN(ISNULL(MCD1.SET_NAIYO_2,''))   AS SET1_NAIYO2  " & vbNewLine _
                                             & "   ,MIN(ISNULL(MCD1.SET_NAIYO_3,''))   AS SET1_NAIYO3  " & vbNewLine _
                                             & " FROM $LM_TRN$..F_UNSO_L UNSOL                         " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..C_OUTKA_L COL                    " & vbNewLine _
                                             & "    ON COL.NRS_BR_CD   = UNSOL.NRS_BR_CD               " & vbNewLine _
                                             & "   AND COL.OUTKA_NO_L  = UNSOL.INOUTKA_NO_L            " & vbNewLine _
                                             & "   AND COL.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_CUST  CUST                     " & vbNewLine _
                                             & "    ON CUST.NRS_BR_CD = COL.NRS_BR_CD                  " & vbNewLine _
                                             & "   AND CUST.CUST_CD_L   = COL.CUST_CD_L                " & vbNewLine _
                                             & "   AND CUST.CUST_CD_M   = COL.CUST_CD_M                " & vbNewLine _
                                             & "   AND CUST.CUST_CD_S   = '00'                         " & vbNewLine _
                                             & "   AND CUST.CUST_CD_SS  = '00'                         " & vbNewLine _
                                             & "  LEFT JOIN $LM_TRN$..C_OUTKA_M COM                    " & vbNewLine _
                                             & "    ON COM.NRS_BR_CD   = COL.NRS_BR_CD                 " & vbNewLine _
                                             & "   AND COM.OUTKA_NO_L  = COL.OUTKA_NO_L                " & vbNewLine _
                                             & "   AND COM.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..M_GOODS GOODS                    " & vbNewLine _
                                             & "    ON GOODS.NRS_BR_CD     = COM.NRS_BR_CD             " & vbNewLine _
                                             & "   AND GOODS.GOODS_CD_NRS  = COM.GOODS_CD_NRS          " & vbNewLine _
                                             & "   AND GOODS.SYS_DEL_FLG   = '0'                       " & vbNewLine _
                                             & "   AND GOODS.UNSO_HOKEN_YN = '01'   --運送保険有り時   " & vbNewLine _
                                             & "  LEFT JOIN $LM_MST$..Z_KBN  KBN1                      " & vbNewLine _
                                             & "    ON KBN1.KBN_GROUP_CD  = 'H027'                     " & vbNewLine _
                                             & "   AND KBN1.KBN_NM1       = COL.NRS_BR_CD              " & vbNewLine _
                                             & "   AND KBN1.SYS_DEL_FLG   = '0'                        " & vbNewLine _
                                             & "   LEFT JOIN $LM_MST$..M_CUST_DETAILS  MCD1            " & vbNewLine _
                                             & "    ON MCD1.NRS_BR_CD    = COL.NRS_BR_CD               " & vbNewLine _
                                             & "   AND MCD1.CUST_CD      = COL.CUST_CD_L + COL.CUST_CD_M  " & vbNewLine _
                                             & "   AND MCD1.SUB_KB       = '9J'                       " & vbNewLine _
                                             & "   AND MCD1.SYS_DEL_FLG  = '0'                        " & vbNewLine _
                                             & "  WHERE UNSOL.SYS_DEL_FLG       = '0'                 " & vbNewLine _
                                             & "    AND UNSOL.NRS_BR_CD         = @NRS_BR_CD          " & vbNewLine _
                                             & "    AND UNSOL.UNSO_NO_L         = @UNSO_NO_L          " & vbNewLine _
                                             & "    AND UNSOL.MOTO_DATA_KB      = '20'   --出荷                     " & vbNewLine _
                                             & "    AND GOODS.UNSO_HOKEN_YN     = '01'   --商品M 運送保険対象       " & vbNewLine _
                                             & "    AND CUST.UNSO_HOKEN_AUTO_YN = '01'   --荷主M 運送保険料自動設定 " & vbNewLine _
                                             & " GROUP BY                                             " & vbNewLine _
                                             & "       UNSOL.NRS_BR_CD                                " & vbNewLine _
                                             & "      ,UNSOL.UNSO_NO_L                                " & vbNewLine


#End Region '運賃データ生成

#Region "SQL"

    ''' <summary>
    ''' [M_DEST]届先JIS 情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MST_DEST_JIS As String = " SELECT                         " & vbNewLine _
                                             & "     JIS                        " & vbNewLine _
                                             & " FROM                           " & vbNewLine _
                                             & "     $LM_MST$..M_DEST           " & vbNewLine _
                                             & " WHERE                          " & vbNewLine _
                                             & "     NRS_BR_CD    = @NRS_BR_CD  " & vbNewLine _
                                             & " AND CUST_CD_L    = @CUST_CD_L  " & vbNewLine _
                                             & " AND DEST_CD      = @DEST_CD    " & vbNewLine _
                                             & " AND SYS_DEL_FLG  = '0'         " & vbNewLine

    ''' <summary>
    ''' [M_UNCHIN_TARIFF]運賃タリフ 適用・距離情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_TARIFF_BASE As String = " SELECT                                               " & vbNewLine _
                                            & "     MAIN.*                                           " & vbNewLine _
                                            & " FROM                                                 " & vbNewLine _
                                            & "     $LM_MST$..M_UNCHIN_TARIFF AS MAIN                " & vbNewLine _
                                            & "     INNER JOIN                                       " & vbNewLine _
                                            & "        (                                             " & vbNewLine _
                                            & "         SELECT                                       " & vbNewLine _
                                            & "            NRS_BR_CD                                 " & vbNewLine _
                                            & "            ,UNCHIN_TARIFF_CD                         " & vbNewLine _
                                            & "            ,DATA_TP                                  " & vbNewLine _
                                            & "            ,TABLE_TP                                 " & vbNewLine _
                                            & "            ,MAX(STR_DATE) AS MAX_DATE                " & vbNewLine _
                                            & "         FROM                                         " & vbNewLine _
                                            & "             $LM_MST$..M_UNCHIN_TARIFF                " & vbNewLine _
                                            & "         WHERE                                        " & vbNewLine _
                                            & "     　　　　NRS_BR_CD    = @NRS_BR_CD  　　　　　　　" & vbNewLine _
                                            & "         AND UNCHIN_TARIFF_CD  = @UNCHIN_TARIFF_CD    " & vbNewLine _
                                            & "         AND DATA_TP           = '00'                 " & vbNewLine _
                                            & "         AND STR_DATE         <= @STR_DATE            " & vbNewLine _
                                            & "         AND SYS_DEL_FLG       = '0'                  " & vbNewLine _
                                            & "         GROUP BY                                     " & vbNewLine _
                                            & "             NRS_BR_CD                                " & vbNewLine _
                                            & "            ,UNCHIN_TARIFF_CD                         " & vbNewLine _
                                            & "            ,DATA_TP                                  " & vbNewLine _
                                            & "            ,TABLE_TP                                 " & vbNewLine _
                                            & "        ) AS SUB                                      " & vbNewLine _
                                            & "     ON  MAIN.UNCHIN_TARIFF_CD = SUB.UNCHIN_TARIFF_CD " & vbNewLine _
                                            & "     AND MAIN.NRS_BR_CD        = SUB.NRS_BR_CD        " & vbNewLine _
                                            & "     AND MAIN.DATA_TP          = SUB.DATA_TP          " & vbNewLine _
                                            & "     AND MAIN.TABLE_TP         = SUB.TABLE_TP         " & vbNewLine _
                                            & "     AND MAIN.STR_DATE         = SUB.MAX_DATE         " & vbNewLine _
                                            & "     AND MAIN.SYS_DEL_FLG      = '0'                  " & vbNewLine

    ''' <summary>
    ''' [M_UNCHIN_TARIFF]運賃タリフ 重量情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_TARIFF_WEIGHT As String = " SELECT                                     " & vbNewLine _
                                              & "     *                                      " & vbNewLine _
                                              & " FROM                                       " & vbNewLine _
                                              & "     $LM_MST$..M_UNCHIN_TARIFF              " & vbNewLine _
                                              & " WHERE                                      " & vbNewLine _
                                              & " 　　NRS_BR_CD    = @NRS_BR_CD  　　　　　　" & vbNewLine _
                                              & " AND UNCHIN_TARIFF_CD  = @UNCHIN_TARIFF_CD  " & vbNewLine _
                                              & " AND DATA_TP          = @DATA_TP            " & vbNewLine _
                                              & " AND CAR_TP           = @CAR_TP             " & vbNewLine _
                                              & " AND STR_DATE         = @STR_DATE           " & vbNewLine _
                                              & " AND SYS_DEL_FLG      = '0'                 " & vbNewLine _
                                              & " ORDER BY                                   " & vbNewLine _
                                              & "     WT_LV                                  " & vbNewLine

    ''' <summary>
    ''' [M_SYUHAI_SET]集配料設定ファイル 情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXTC_SHUHAI As String = " SELECT                                    " & vbNewLine _
                                            & "     *                                     " & vbNewLine _
                                            & " FROM                                      " & vbNewLine _
                                            & "     $LM_MST$..M_SYUHAI_SET                " & vbNewLine _
                                            & " WHERE                                     " & vbNewLine _
                                            & "     NRS_BR_CD        = @NRS_BR_CD         " & vbNewLine _
                                            & " AND CUST_CD_L        = @CUST_CD_L         " & vbNewLine _
                                            & " AND UNCHIN_TARIFF_CD = @UNCHIN_TARIFF_CD  " & vbNewLine _
                                            & " AND JIS_CD           = @JIS_CD            " & vbNewLine _
                                            & " AND SYS_DEL_FLG      = '0'                " & vbNewLine

    ''' <summary>
    ''' [M_EXTC_UNCHIN]割増運賃マスタ 情報取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXTC_UNCHIN As String = " SELECT                                    " & vbNewLine _
                                            & "     *                                     " & vbNewLine _
                                            & " FROM                                      " & vbNewLine _
                                            & "     $LM_MST$..M_EXTC_UNCHIN               " & vbNewLine _
                                            & " WHERE                                     " & vbNewLine _
                                            & "     NRS_BR_CD        = @NRS_BR_CD         " & vbNewLine _
                                            & " AND EXTC_TARIFF_CD   = @EXTC_TARIFF_CD    " & vbNewLine _
                                            & " AND JIS_CD           = @JIS_CD            " & vbNewLine _
                                            & " AND SYS_DEL_FLG      = '0'                " & vbNewLine

    ''' <summary>
    ''' [M_YOKO_TARIFF_HD]横持ちタリフヘッダ 取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_YOKO_HD As String = " SELECT                                   " & vbNewLine _
                                        & "     NRS_BR_CD                            " & vbNewLine _
                                        & "    ,YOKO_TARIFF_CD                       " & vbNewLine _
                                        & "    ,YOKO_REM                             " & vbNewLine _
                                        & "    ,CALC_KB                              " & vbNewLine _
                                        & "    ,SPLIT_FLG                            " & vbNewLine _
                                        & " FROM                                     " & vbNewLine _
                                        & "     $LM_MST$..M_YOKO_TARIFF_HD           " & vbNewLine _
                                        & " WHERE                                    " & vbNewLine _
                                        & "     NRS_BR_CD      = @NRS_BR_CD          " & vbNewLine _
                                        & " AND YOKO_TARIFF_CD = @YOKO_TARIFF_CD     " & vbNewLine _
                                        & " AND SYS_DEL_FLG    = '0'                 " & vbNewLine

    ''' <summary>
    ''' [M_YOKO_TARIFF_DTL]横持ちタリフ明細 取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_YOKO_DTL As String = " SELECT                                  " & vbNewLine _
                                        & "     NRS_BR_CD                            " & vbNewLine _
                                        & "    ,YOKO_TARIFF_CD                       " & vbNewLine _
                                        & "    ,YOKO_TARIFF_CD_EDA                   " & vbNewLine _
                                        & "    ,CARGO_KB                             " & vbNewLine _
                                        & "    ,CAR_KB                               " & vbNewLine _
                                        & "    ,WT_LV                                " & vbNewLine _
                                        & "    ,DANGER_KB                            " & vbNewLine _
                                        & "    ,UT_PRICE                             " & vbNewLine _
                                        & "    ,KGS_PRICE                            " & vbNewLine _
                                        & " FROM                                     " & vbNewLine _
                                        & "     $LM_MST$..M_YOKO_TARIFF_DTL          " & vbNewLine _
                                        & " WHERE                                    " & vbNewLine _
                                        & "     NRS_BR_CD        = @NRS_BR_CD        " & vbNewLine _
                                        & " AND YOKO_TARIFF_CD   = @YOKO_TARIFF_CD   " & vbNewLine _
                                        & " AND CARGO_KB         = @CARGO_KB         " & vbNewLine _
                                        & " AND CAR_KB           = @CAR_KB           " & vbNewLine _
                                        & " AND WT_LV            = @WT_LV            " & vbNewLine _
                                        & " AND DANGER_KB        = @DANGER_KB        " & vbNewLine _
                                        & " AND SYS_DEL_FLG      = '0'               " & vbNewLine _
                                        & " ORDER BY                                 " & vbNewLine _
                                        & "     WT_LV                                " & vbNewLine

    ''' <summary>
    ''' [Z_KBN]端数調整特別荷主判断
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GET_ADJUST_CUST As String = " SELECT                         " & vbNewLine _
                                                & "     COUNT(*) AS REC_CNT        " & vbNewLine _
                                                & " FROM                           " & vbNewLine _
                                                & "     $LM_MST$..Z_KBN            " & vbNewLine _
                                                & " WHERE                          " & vbNewLine _
                                                & "     KBN_GROUP_CD = 'U015'      " & vbNewLine _
                                                & " AND KBN_NM1      = @NRS_BR_CD  " & vbNewLine _
                                                & " AND KBN_NM3      = @CUST_CD_L  " & vbNewLine _
                                                & " AND SYS_DEL_FLG  = '0'         " & vbNewLine

#End Region '運賃計算

#End Region '統合

#Region "定数"

    '運送Ｍ取得単位
    Private Const LVL_UNSOU As String = "01"
    Private Const LVL_TAKYU As String = "02"

    '元データ区分
    Private Const MOTO_NYUKA As String = "10"
    Private Const MOTO_SHUKA As String = "20"
    Private Const MOTO_UNSOU As String = "40"

    '運送温度区分
    Private Const ONDO_TEION As String = "10"
    Private Const ONDO_HOREI As String = "20"
    Private Const ONDO_OTHER As String = "90"

    'データタイプ[M_UNCHIN_TARIFF]
    Private Const DATA_TYPE_KYORI As String = "00"
    Private Const DATA_TYPE_UNCHN As String = "01"
    Private Const DATA_TYPE_HOREI As String = "02"

#End Region '定数

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow

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

#Region "Method [共通]"

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region 'ユーティリティ

#End Region '共通処理

#Region "Method [個別処理]"

#Region "Method [運賃データ生成]"

#Region "検索処理"

    ''' <summary>
    ''' 運送Ｌ 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送Ｌ情報を取得する</remarks>
    Private Function GetUnsoL(ByVal ds As DataSet) As DataSet

        '参照用のDataTableを取得する
        Me._Row = ds.Tables(TABLE_NM_IN).Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOL)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetUnsoL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("JIYU_KB", "JIYU_KB")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKA_PLAN_TIME", "OUTKA_PLAN_TIME")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("ARR_ACT_TIME", "ARR_ACT_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("SHIP_CD", "SHIP_CD")
        map.Add("ORIG_CD", "ORIG_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("PC_KB", "PC_KB")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("REMARK", "REMARK")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("AD_3", "AD_3")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("BUY_CHU_NO", "BUY_CHU_NO")
        map.Add("AREA_CD", "AREA_CD")
        map.Add("TYUKEI_HAISO_FLG", "TYUKEI_HAISO_FLG")
        map.Add("SYUKA_TYUKEI_CD", "SYUKA_TYUKEI_CD")
        map.Add("HAIKA_TYUKEI_CD", "HAIKA_TYUKEI_CD")
        map.Add("TRIP_NO_SYUKA", "TRIP_NO_SYUKA")
        map.Add("TRIP_NO_TYUKEI", "TRIP_NO_TYUKEI")
        map.Add("TRIP_NO_HAIKA", "TRIP_NO_HAIKA")
        '2013.03.18 / NOTES 1911 追加　開始
        map.Add("UNSO_TARE_YN", "UNSO_TARE_YN")
        '2013.03.18 / NOTES 1911 追加　終了
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_UNSO_L)

        Return ds

    End Function

    ''' <summary>
    ''' 運送Ｍ 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送Ｍ情報を取得する</remarks>
    Private Function GetUnsoM(ByVal ds As DataSet) As DataSet

        'ＤＢ参照キー取得
        Dim unsoLNo As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成 (運賃生成レベルにより利用するＳＱＬを切替え)
        Select Case ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_M_LV_KBN").ToString
            Case LVL_UNSOU
                Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOM_ALL)
            Case LVL_TAKYU
                Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOM_TAKKYU_GROUP)
        End Select

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", unsoLNo, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetUnsoM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("UNSO_TTL_QT", "UNSO_TTL_QT")
        map.Add("QT_UT", "QT_UT")
        map.Add("HASU", "HASU")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("ZBUKA_CD", "ZBUKA_CD")
        map.Add("ABUKA_CD", "ABUKA_CD")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("REMARK", "REMARK")
        map.Add("GOODS_MST_FLG", "GOODS_MST_FLG")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("TARE_WT", "TARE_WT")
        '2013.03.18 / NOTES 1911 追加　開始
        map.Add("GOODS_TARE_YN", "GOODS_TARE_YN")
        map.Add("ALCTD_KB", "ALCTD_KB")
        '2013.03.18 / NOTES 1911 追加　終了

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_UNSO_M)

        Return ds

    End Function

    '2013.07.04 追加START
    ''' <summary>
    ''' 運送重量の再取得(BPのみ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>受信TBLの重量合計をセット</remarks>
    Private Function GetBpUNsoWt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成

        Select Case Me._Row.Item("MOTO_DATA_KB").ToString()

            Case LMF800DAC.INKA

                Return ds
                'Me._StrSql.Append(LMF800DAC.SQL_GET_BP_EDI_IN)

            Case LMF800DAC.OUTKA

                Me._StrSql.Append(LMF800DAC.SQL_GET_BP_EDI_OUT)

            Case Else

                Return ds

        End Select

        'パラメータ設定
        Call Me.SetParamRcvBp(ds)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetBpUNsoWt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("BP_UNSO_WT", "BP_UNSO_WT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_RCV_INOUT_BP)

        Return ds

    End Function
    '2013.07.04 追加END

    '2013.03.27 / NOTES 1911 ADD ATART
    ''' <summary>
    ''' 引当個数単位区分 情報取得(NOTES1911追加)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷個数単位区分値を取得する</remarks>
    Private Function GetAlctd_Kb(ByVal ds As DataSet) As DataSet

        'ＤＢ参照キー取得
        Dim unsoLNo As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString
        Dim unsoMBr As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString
        Dim unsoMNo As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_M").ToString

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成 (運賃生成レベルにより利用するＳＱＬを切替え)'ウィラポン
        'Select Case ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_M_LV_KBN").ToString
        '    Case LVL_UNSOU
        '        Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOM_ALL)
        '    Case LVL_TAKYU
        '        Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOM_TAKKYU_GROUP)
        'End Select

        Me._StrSql.Append(LMF800DAC.SQL_GET_ALCTD)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", unsoLNo, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", unsoMBr, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", unsoMNo, DBDataType.CHAR))
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetALCTD", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'map.Add("NRS_BR_CD", "NRS_BR_CD")
        'map.Add("UNSO_NO_L", "UNSO_NO_L")
        'map.Add("UNSO_NO_M", "UNSO_NO_M")
        'map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        'map.Add("GOODS_NM", "GOODS_NM")
        'map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        'map.Add("NB_UT", "NB_UT")
        'map.Add("UNSO_TTL_QT", "UNSO_TTL_QT")
        'map.Add("QT_UT", "QT_UT")
        'map.Add("HASU", "HASU")
        'map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        'map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        'map.Add("IRIME", "IRIME")
        'map.Add("IRIME_UT", "IRIME_UT")
        'map.Add("BETU_WT", "BETU_WT")
        'map.Add("SIZE_KB", "SIZE_KB")
        'map.Add("ZBUKA_CD", "ZBUKA_CD")
        'map.Add("ABUKA_CD", "ABUKA_CD")
        'map.Add("PKG_NB", "PKG_NB")
        'map.Add("LOT_NO", "LOT_NO")
        'map.Add("REMARK", "REMARK")
        'map.Add("GOODS_MST_FLG", "GOODS_MST_FLG")
        'map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        'map.Add("STD_WT_KGS", "STD_WT_KGS")
        'map.Add("TARE_WT", "TARE_WT")
        '2013.03.18 / NOTES 1911 追加　開始
        'map.Add("GOODS_TARE_YN", "GOODS_TARE_YN")
        map.Add("ALCTD_KB", "ALCTD_KB")
        '2013.03.18 / NOTES 1911 追加　終了

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_UNSO_M)

        Return ds

    End Function
    '2013.03.27 / NOTES 1911 ADD END

    'ADD 2018/10/22 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる
    ''' <summary>
    ''' 運送保険料 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送保険料を取得する</remarks>
    Private Function GET_HOKENRYO(ByVal ds As DataSet) As DataSet

        'ＤＢ参照キー取得
        Dim unsoLNo As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString
        Dim unsoMBr As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMF800DAC.SQL_GET_HOKENYO)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", unsoLNo, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", unsoMBr, DBDataType.CHAR))
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetUnsoHokenryo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("HOKENRYO", "HOKENRYO")
        map.Add("SET1_NAIYO1", "SET1_NAIYO1")
        map.Add("SET1_NAIYO2", "SET1_NAIYO2")
        map.Add("SET1_NAIYO3", "SET1_NAIYO3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_UNSO_HOKENRYO)

        Return ds

    End Function

    ''' <summary>
    ''' 運送Ｍ 集約情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送Ｍ集約情報を取得する（運送番号Ｌで集約）</remarks>
    Private Function GetUnsoMGroup(ByVal ds As DataSet) As DataSet

        'ＤＢ参照キー取得
        Dim unsoLNo As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOM_GROUP)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", unsoLNo, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetUnsoMGroup", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("UNSO_TTL_QT_SUM") = Convert.ToInt32(reader("UNSO_TTL_QT_SUM"))
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' まとめ運賃レコード数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>まとめ運賃レコード件数を取得する（運送番号Ｌで集約）</remarks>
    Private Function GetSeiqGroupCount(ByVal ds As DataSet) As DataSet

        'ＤＢ参照キー取得
        Dim unsoLNo As String = ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_GROUP_REC_COUNT)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", unsoLNo, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetSeiqGroupCount", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("SEIQ_GROUP_REC_COUNT") = Convert.ToInt32(reader("SEIQ_GROUP_REC_COUNT"))
            outTbl.Rows(0).Item("DECI_REC_COUNT") = Convert.ToInt32(reader("DECI_REC_COUNT"))
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 商品／荷主関連 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品／荷主関連情報を取得する</remarks>
    Private Function GetGoodsCustInfo(ByVal ds As DataSet) As DataSet

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_GOODS_CUST)

        'パラメータ設定
        Call Me.SetParamGoodsCust(ds)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetGoodsCustInfo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("CUST_CD_S") = reader("CUST_CD_S").ToString
            outTbl.Rows(0).Item("CUST_CD_SS") = reader("CUST_CD_SS").ToString
            outTbl.Rows(0).Item("KIKEN_KB") = reader("KIKEN_KB").ToString
            outTbl.Rows(0).Item("BETU_KYORI_CD") = reader("BETU_KYORI_CD").ToString
            outTbl.Rows(0).Item("UNCHIN_SEIQTO_CD") = reader("UNCHIN_SEIQTO_CD").ToString
            outTbl.Rows(0).Item("UNTIN_CALCULATION_KB") = reader("UNTIN_CALCULATION_KB").ToString
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 商品／荷主関連 情報取得②
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品／荷主関連情報を取得する</remarks>
    Private Function GetGoodsCustInfoSecond(ByVal ds As DataSet) As DataSet

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        '参照用のDataTableを取得する
        Me._Row = ds.Tables(TABLE_NM_UNSO_L).Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_GOODS_CUST2)

        'パラメータ設定
        '2014/5/16 Start s.kobayashi NotesNo.2183
        Call Me.SetParamGoodsCustSecond(ds)
        'Call Me.SetParamGoodsCustSecond()
        '2014/5/16 End s.kobayashi NotesNo.2183

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetGoodsCustInfoSecond", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("CUST_CD_S") = reader("CUST_CD_S").ToString
            outTbl.Rows(0).Item("CUST_CD_SS") = reader("CUST_CD_SS").ToString
            outTbl.Rows(0).Item("KIKEN_KB") = reader("KIKEN_KB").ToString
            outTbl.Rows(0).Item("BETU_KYORI_CD") = reader("BETU_KYORI_CD").ToString
            outTbl.Rows(0).Item("UNCHIN_SEIQTO_CD") = reader("UNCHIN_SEIQTO_CD").ToString
            outTbl.Rows(0).Item("UNTIN_CALCULATION_KB") = reader("UNTIN_CALCULATION_KB").ToString
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 距離（届先Ｍ） 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先Ｍ情報を取得する</remarks>
    Private Function GetKyoriDestMst(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_KYORI_DEST_MST)

        'パラメータ設定
        Call Me.SetParamDestMst()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetKyoriDestMst", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("DEST_MST_EXIST_FLG") = reader("GET_FLG").ToString
            outTbl.Rows(0).Item("KYORI") = Convert.ToDouble(reader("KYORI"))
            outTbl.Rows(0).Item("DEST_JIS_CD") = reader("JIS").ToString

            '運賃請求先適用判断
            If reader("UNCHIN_SEIQTO_CD").ToString.Equals("") = False Then
                outTbl.Rows(0).Item("UNCHIN_SEIQTO_CD") = reader("UNCHIN_SEIQTO_CD").ToString
                outTbl.Rows(0).Item("DEST_MST_SEIQTO_EXIST_FLG") = "01"
            End If

        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 距離（距離Ｍ） 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>距離Ｍ情報を取得する</remarks>
    Private Function GetKyoriKyoriMst(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        CreateSqlForRefKyoriMst()

        'パラメータ設定
        Call Me.SetParamKyoriMst(ds)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetKyoriKyoriMst", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("KYORI") = Convert.ToDouble(reader("KYORI"))
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' テーブルタイプ 情報取得（最新タリフ参照）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>距離Ｍ情報を取得する</remarks>
    Private Function GetTableType(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_TABLE_TYPE)

        'パラメータ設定
        Call Me.SetParamTableType()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetTableType", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("TABLE_TP", "TABLE_TP")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_TARIFF_LATEST)

        Return ds

    End Function

    ''' <summary>
    ''' 横持ち運賃タリフヘッダ 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ち運賃タリフヘッダ情報を取得する</remarks>
    Private Function GetYokoTariffHd(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_YOKO_HD)

        'パラメータ設定
        Call Me.SetParamYokoHd()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetYokoTariffHd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_REM", "YOKO_REM")
        map.Add("CALC_KB", "CALC_KB")
        map.Add("SPLIT_FLG", "SPLIT_FLG")
        map.Add("YOKOMOCHI_MIN", "YOKOMOCHI_MIN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_YOKO_HD)

        Return ds

    End Function

    ''' <summary>
    ''' 特定荷主判断
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>対象顧客が運賃レコード複数作成用の "特定荷主" であるか判断</remarks>
    Private Function GetSpecificCustCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        '結果情報格納用 DataTable を取得
        Dim outTbl As DataTable = ds.Tables(TABLE_NM_OUT)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_SPECIFIC_CUST)

        'パラメータ設定
        Call Me.SetParamSpecificCust()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetSpecificCustCount", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            outTbl.Rows(0).Item("REV_UNCHIN_LV_FLG") = reader("VALUE1")
            MyBase.SetResultCount(1)
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 貨物情報集約（請求先単位）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>貨物情報を参照し、請求先単位に情報（重量・荷姿個数）を集約する</remarks>
    Private Function GetUnsoMRevUnit(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_REV_UNIT)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetUnsoMRevUnit", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_REV_UNIT)

        Return ds

    End Function

#End Region

#Region "SQL文構築"

    ''' <summary>
    ''' SQL文構築 [距離情報取得 M_KYORI]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlForRefKyoriMst()

        Me._StrSql.Append(LMF800DAC.SQL_GET_KYORI_MST_HDR)      '上部

        With Me._Row
            '元データ区分により発地・届先の参照先を切替える
            Select Case .Item("MOTO_DATA_KB").ToString
                Case LMF800DAC.MOTO_NYUKA
                    Me._StrSql.Append(LMF800DAC.SQL_GET_NYUKA_JIS)      'JIS取得（入荷）
                Case LMF800DAC.MOTO_SHUKA
                    Me._StrSql.Append(LMF800DAC.SQL_GET_SHUKA_JIS)      'JIS取得（出荷）
                Case LMF800DAC.MOTO_UNSOU
                    Me._StrSql.Append(LMF800DAC.SQL_GET_UNSOU_JIS)      'JIS取得（運送）
            End Select

            Me._StrSql.Append(LMF800DAC.SQL_GET_KYORI_MST_DTL)      '下部

            If LMF800DAC.MOTO_UNSOU.Equals(.Item("MOTO_DATA_KB").ToString) = True Then
                Me._StrSql.Append(LMF800DAC.SQL_GET_KYORI_MST_ORDER)      '順序（運送のときのみ）
            End If

        End With

    End Sub

#End Region 'SQL文構築

#Region "パラメータ設定"

    '2013.07.04 追加START
    ''' <summary>
    ''' パラメータ設定 [H_INKAEDI_DTL_BP, H_OUTKAEDI_DTL_BP]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamRcvBp(ByVal ds As DataSet)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

    End Sub
    '2013.07.04 追加END

    ''' <summary>
    ''' パラメータ設定 [M_GOODS, M_CUST]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsCust(ByVal ds As DataSet)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_M").ToString(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    ''' パラメータ設定② [M_GOODS, M_CUST]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGoodsCustSecond(ByVal ds As DataSet)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            '2014/5/16 Start s.kobayashi NotesNo.2183
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            '2014/5/16 End s.kobayashi NotesNo.2183

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [距離情報取得 M_DEST]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDestMst()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            Select Case .Item("MOTO_DATA_KB").ToString
                Case LMF800DAC.MOTO_NYUKA
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("ORIG_CD").ToString(), DBDataType.CHAR))
                Case LMF800DAC.MOTO_SHUKA
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
                Case LMF800DAC.MOTO_UNSOU
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            End Select

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [距離情報取得 M_KYORI]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamKyoriMst(ByVal ds As DataSet)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", ds.Tables(TABLE_NM_IN).Rows(0).Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_CD", ds.Tables(TABLE_NM_OUT).Rows(0).Item("BETU_KYORI_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [テーブルタイプ取得 M_UNCHIN_TARIFF]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTableType()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))

            Select Case .Item("UNSO_ONDO_KB").ToString
                Case LMF800DAC.ONDO_TEION, LMF800DAC.ONDO_HOREI
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TP", LMF800DAC.DATA_TYPE_HOREI, DBDataType.CHAR))
                Case Else
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TP", LMF800DAC.DATA_TYPE_UNCHN, DBDataType.CHAR))
            End Select

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [横持ちタリフヘッダ取得 M_YOKO_TARIFF_HD]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokoHd()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [特定荷主判断]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSpecificCust()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region 'パラメータ設定

#Region "業務処理"



#End Region '業務処理

#End Region '● 運賃データ生成

#Region "Method [運賃計算]"

#Region "検索処理"

    ''' <summary>
    ''' 届先JIS 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先JIS情報を取得する</remarks>
    Private Function GetDestJis(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_MST_DEST_JIS)

        'パラメータ設定
        Call Me.SetParamMstDestJis()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetDestJis", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得結果の設定
        If reader.Read() = True Then
            inTbl.Rows(0).Item("DEST_JIS") = reader("JIS").ToString
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフ 基本情報取得（適用運賃特定処理）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>適用運賃レコードを特定する</remarks>
    Private Function GetTariffBase(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_TARIFF_BASE)

        'パラメータ設定
        Call Me.SetParamTariffBase()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetTariffBase", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        ds = MyBase.SetSelectResultToDataSet(SetTariffResult(), ds, reader, TABLE_NM_CALC_KYORI)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフ 重量情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃格納タリフ情報を取得する</remarks>
    Private Function GetTariffWeight(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_TARIFF_WEIGHT)

        'パラメータ設定
        Call Me.SetParamTariffWeight(ds)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetTariffWeight", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        ds = MyBase.SetSelectResultToDataSet(SetTariffResult(), ds, reader, TABLE_NM_CALC_WEIGHT)

        Return ds

    End Function

    ''' <summary>
    ''' 集配料設定ファイル 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>集配料設定ファイル情報を取得する</remarks>
    Private Function GetExtcShuhai(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_EXTC_SHUHAI)

        'パラメータ設定
        Call Me.SetParamExtcShuhai()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetExtcShuhai", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("FIELD_NO", "FIELD_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_CALC_SYUHAI)

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ 情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>割増運賃マスタ情報を取得する</remarks>
    Private Function GetExtcUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_EXTC_UNCHIN)

        'パラメータ設定
        Call Me.SetParamExtcUnchin()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetExtcUnchin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        map.Add("WINT_KIKAN_FROM", "WINT_KIKAN_FROM")
        map.Add("WINT_KIKAN_TO", "WINT_KIKAN_TO")
        map.Add("WINT_EXTC_YN", "WINT_EXTC_YN")
        map.Add("CITY_EXTC_YN", "CITY_EXTC_YN")
        map.Add("RELY_EXTC_YN", "RELY_EXTC_YN")
        map.Add("FRRY_EXTC_YN", "FRRY_EXTC_YN")
        'START YANAI 要望番号377
        map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")
        'END YANAI 要望番号377

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_CALC_E_UNCHIN)

        Return ds

    End Function

    ''' <summary>
    ''' 横持ち運賃タリフヘッダデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ち運賃タリフヘッダのデータ取得SQLの構築・発行</remarks>
    Private Function GetYokoTariffHead(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_YOKO_HD)

        'パラメータ設定
        Call Me.SetParamYokoTariffHD()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetYokoTariffHead", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_REM", "YOKO_REM")
        map.Add("CALC_KB", "CALC_KB")
        map.Add("SPLIT_FLG", "SPLIT_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_CALC_YOKO_HD)

        Return ds

    End Function

    ''' <summary>
    ''' 横持ち運賃タリフ明細データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ち運賃タリフ明細のデータ取得SQLの構築・発行</remarks>
    Private Function GetYokoTariffDetail(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_YOKO_DTL)

        'パラメータ設定
        Call Me.SetParamYokoTariffDTL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetYokoTariffDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_TARIFF_CD_EDA", "YOKO_TARIFF_CD_EDA")
        map.Add("CARGO_KB", "CARGO_KB")
        map.Add("CAR_KB", "CAR_KB")
        map.Add("WT_LV", "WT_LV")
        map.Add("DANGER_KB", "DANGER_KB")
        map.Add("UT_PRICE", "UT_PRICE")
        map.Add("KGS_PRICE", "KGS_PRICE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_CALC_YOKO_DTL)

        Return ds

    End Function

    ''' <summary>
    ''' 端数調整特別荷主判断
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>対象顧客が運賃レコード複数作成用の "特定荷主" であるか判断</remarks>
    Private Function GetAdjustCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'INTableの条件rowの格納
        Me._Row = unsoLDt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF800DAC.SQL_GET_ADJUST_CUST)

        'パラメータ設定
        Call Me.SetParamAdjustCust()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF800DAC", "GetAdjustCust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定 [M_DEST]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamMstDestJis()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [M_UNCHIN_TARIFF] to Kyori
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTariffBase()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("UNSO_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [M_UNCHIN_TARIFF] To Weight
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetParamTariffWeight(ByVal ds As DataSet)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '[データタイプ用]設定パラメータ判定
        Dim dataType As String = ""

        Select Case ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("UNSO_ONDO_KB").ToString
            Case ONDO_TEION, ONDO_HOREI
                dataType = DATA_TYPE_HOREI

            Case Else
                dataType = DATA_TYPE_UNCHN

        End Select

        'パラメータ設定（前回取得した距離レコードを元に参照する）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TP", dataType, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_TP", ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("CAR_TP").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("STR_DATE").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定 [M_SYUHAI_SET]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExtcShuhai()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("DEST_JIS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [M_EXTC_UNCHIN]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExtcUnchin()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("DEST_JIS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [M_YOKO_TARIFF_HD]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokoTariffHD()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [M_YOKO_TARIFF_DTL]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokoTariffDTL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CARGO_KB", .Item("CARGO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KB", .Item("CAR_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WT_LV", .Item("WT_LV").ToString, DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DANGER_KB", .Item("DANGER_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 [Z_KBN]
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamAdjustCust()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region 'パラメータ設定

#Region "業務処理"

    ''' <summary>
    ''' タリフ情報の結果を格納
    ''' </summary>
    ''' <returns>HashTable</returns>
    ''' <remarks></remarks>
    Private Function SetTariffResult() As Hashtable

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_CD_EDA", "UNCHIN_TARIFF_CD_EDA")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("DATA_TP", "DATA_TP")
        map.Add("TABLE_TP", "TABLE_TP")
        map.Add("CAR_TP", "CAR_TP")
        map.Add("WT_LV", "WT_LV")
        map.Add("KYORI_1", "KYORI_1")
        map.Add("KYORI_2", "KYORI_2")
        map.Add("KYORI_3", "KYORI_3")
        map.Add("KYORI_4", "KYORI_4")
        map.Add("KYORI_5", "KYORI_5")
        map.Add("KYORI_6", "KYORI_6")
        map.Add("KYORI_7", "KYORI_7")
        map.Add("KYORI_8", "KYORI_8")
        map.Add("KYORI_9", "KYORI_9")
        map.Add("KYORI_10", "KYORI_10")
        map.Add("KYORI_11", "KYORI_11")
        map.Add("KYORI_12", "KYORI_12")
        map.Add("KYORI_13", "KYORI_13")
        map.Add("KYORI_14", "KYORI_14")
        map.Add("KYORI_15", "KYORI_15")
        map.Add("KYORI_16", "KYORI_16")
        map.Add("KYORI_17", "KYORI_17")
        map.Add("KYORI_18", "KYORI_18")
        map.Add("KYORI_19", "KYORI_19")
        map.Add("KYORI_20", "KYORI_20")
        map.Add("KYORI_21", "KYORI_21")
        map.Add("KYORI_22", "KYORI_22")
        map.Add("KYORI_23", "KYORI_23")
        map.Add("KYORI_24", "KYORI_24")
        map.Add("KYORI_25", "KYORI_25")
        map.Add("KYORI_26", "KYORI_26")
        map.Add("KYORI_27", "KYORI_27")
        map.Add("KYORI_28", "KYORI_28")
        map.Add("KYORI_29", "KYORI_29")
        map.Add("KYORI_30", "KYORI_30")
        map.Add("KYORI_31", "KYORI_31")
        map.Add("KYORI_32", "KYORI_32")
        map.Add("KYORI_33", "KYORI_33")
        map.Add("KYORI_34", "KYORI_34")
        map.Add("KYORI_35", "KYORI_35")
        map.Add("KYORI_36", "KYORI_36")
        map.Add("KYORI_37", "KYORI_37")
        map.Add("KYORI_38", "KYORI_38")
        map.Add("KYORI_39", "KYORI_39")
        map.Add("KYORI_40", "KYORI_40")
        map.Add("KYORI_41", "KYORI_41")
        map.Add("KYORI_42", "KYORI_42")
        map.Add("KYORI_43", "KYORI_43")
        map.Add("KYORI_44", "KYORI_44")
        map.Add("KYORI_45", "KYORI_45")
        map.Add("KYORI_46", "KYORI_46")
        map.Add("KYORI_47", "KYORI_47")
        map.Add("KYORI_48", "KYORI_48")
        map.Add("KYORI_49", "KYORI_49")
        map.Add("KYORI_50", "KYORI_50")
        map.Add("KYORI_51", "KYORI_51")
        map.Add("KYORI_52", "KYORI_52")
        map.Add("KYORI_53", "KYORI_53")
        map.Add("KYORI_54", "KYORI_54")
        map.Add("KYORI_55", "KYORI_55")
        map.Add("KYORI_56", "KYORI_56")
        map.Add("KYORI_57", "KYORI_57")
        map.Add("KYORI_58", "KYORI_58")
        map.Add("KYORI_59", "KYORI_59")
        map.Add("KYORI_60", "KYORI_60")
        map.Add("KYORI_61", "KYORI_61")
        map.Add("KYORI_62", "KYORI_62")
        map.Add("KYORI_63", "KYORI_63")
        map.Add("KYORI_64", "KYORI_64")
        map.Add("KYORI_65", "KYORI_65")
        map.Add("KYORI_66", "KYORI_66")
        map.Add("KYORI_67", "KYORI_67")
        map.Add("KYORI_68", "KYORI_68")
        map.Add("KYORI_69", "KYORI_69")
        map.Add("KYORI_70", "KYORI_70")
        map.Add("CITY_EXTC_A", "CITY_EXTC_A")
        map.Add("CITY_EXTC_B", "CITY_EXTC_B")
        map.Add("WINT_EXTC_A", "WINT_EXTC_A")
        map.Add("WINT_EXTC_B", "WINT_EXTC_B")
        map.Add("RELY_EXTC", "RELY_EXTC")
        map.Add("INSU", "INSU")
        'START YANAI 要望番号377
        'map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")
        'END YANAI 要望番号377
        map.Add("FRRY_EXTC_PART", "FRRY_EXTC_PART")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")

        Return map

    End Function

#End Region '業務処理

#End Region '● 運賃計算

#End Region '業務処理

End Class
