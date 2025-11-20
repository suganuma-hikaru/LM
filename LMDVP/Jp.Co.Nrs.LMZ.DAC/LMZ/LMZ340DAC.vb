' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ340DAC : 入荷棟室ZONEチェック処理
'                                  ・貯蔵最大数
'                                  ・毒劇区分
'                                  ・高圧ガス区分
'                                  ・薬事法区分
'                                  ・消防危険区分AND消防コード
'  作  成  者       :  asatsuma
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMZ340DACクラス
''' </summary>
Public Class LMZ340DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' チェック処理不要フラグ検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_FLG As String = "" _
            & " SELECT                                  " & vbNewLine _
            & "   SET_NAIYO AS FLG_A2                   " & vbNewLine _
            & " FROM                                    " & vbNewLine _
            & "   $LM_MST$..M_CUST_DETAILS CDT          " & vbNewLine _
            & " WHERE                                   " & vbNewLine _
            & "   CDT.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
            & "   AND CDT.CUST_CD = @CUST_CD            " & vbNewLine _
            & "   AND CDT.SUB_KB = 'A2'                 " & vbNewLine _
            & "   AND CDT.SYS_DEL_FLG = '0'             " & vbNewLine

    ''' <summary>
    ''' 貯蔵最大数チェック検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_CAPA As String = "" _
            & " SELECT                                                                                  " & vbNewLine _
            & "   CASE WHEN ISNULL(TSM.CHOZO_MAX_QTY, 0) > 0                                            " & vbNewLine _
            & "        THEN TSM.CHOZO_MAX_QTY                                                           " & vbNewLine _
            & "        ELSE TOM.CHOZO_MAX_QTY END AS MAX_QTY,                                           " & vbNewLine _
            & "   CASE WHEN ISNULL(TSM.CHOZO_MAX_QTY, 0) > 0                                            " & vbNewLine _
            & "        THEN ISNULL(TSZ.ZAI_QTY, 0)                                                      " & vbNewLine _
            & "        ELSE ISNULL(TOZ.ZAI_QTY, 0) END AS ZAI_QTY                                       " & vbNewLine _
            & " FROM                                                                                    " & vbNewLine _
            & "   $LM_MST$..M_TOU TOM                                                                   " & vbNewLine _
            & " LEFT JOIN                                                                               " & vbNewLine _
            & "   $LM_MST$..M_TOU_SITU TSM                                                              " & vbNewLine _
            & "   ON                                                                                    " & vbNewLine _
            & "     TSM.WH_CD = TOM.WH_CD                                                               " & vbNewLine _
            & "     AND TSM.TOU_NO = TOM.TOU_NO                                                         " & vbNewLine _
            & "     AND TSM.SITU_NO = @SITU_NO                                                          " & vbNewLine _
            & "     AND TSM.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
            & " LEFT JOIN (                                                                             " & vbNewLine _
            & "   SELECT                                                                                " & vbNewLine _
            & "     ZAI.NRS_BR_CD,                                                                      " & vbNewLine _
            & "     ZAI.WH_CD,                                                                          " & vbNewLine _
            & "     ZAI.TOU_NO,                                                                         " & vbNewLine _
            & "     SUM(CASE SUBSTRING(MGS.SHOBO_CD, 1, 1)                                              " & vbNewLine _
            & "         WHEN '4' THEN                                                                   " & vbNewLine _
            & "           CASE MGS.STD_IRIME_UT                                                         " & vbNewLine _
            & "             WHEN 'CC' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.001                   " & vbNewLine _
            & "             WHEN 'GL' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 3.79                    " & vbNewLine _
            & "             WHEN 'GR' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.001 / MGS.HIZYU       " & vbNewLine _
            & "             WHEN 'KG' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB / MGS.HIZYU               " & vbNewLine _
            & "             WHEN 'L ' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB                           " & vbNewLine _
            & "             WHEN 'LB' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.45 / MGS.HIZYU        " & vbNewLine _
            & "             WHEN 'm3' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB                           " & vbNewLine _
            & "             WHEN 'MG' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.000001 / MGS.HIZYU    " & vbNewLine _
            & "             WHEN 'ML' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.001 / MGS.HIZYU       " & vbNewLine _
            & "             WHEN 'OZ' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.03 / MGS.HIZYU        " & vbNewLine _
            & "             WHEN 'QT' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.95                    " & vbNewLine _
            & "             ELSE           ZAI.PORA_ZAI_NB * MGS.STD_WT_KGS                             " & vbNewLine _
            & "           END                                                                           " & vbNewLine _
            & "         ELSE                                                                            " & vbNewLine _
            & "           ZAI.PORA_ZAI_NB * MGS.STD_WT_KGS                                              " & vbNewLine _
            & "       END                                                                               " & vbNewLine _
            & "     ) AS ZAI_QTY                                                                        " & vbNewLine _
            & "   FROM                                                                                  " & vbNewLine _
            & "     $LM_TRN$..D_ZAI_TRS ZAI                                                             " & vbNewLine _
            & "   INNER JOIN                                                                            " & vbNewLine _
            & "     $LM_MST$..M_GOODS MGS                                                               " & vbNewLine _
            & "     ON                                                                                  " & vbNewLine _
            & "       MGS.NRS_BR_CD = ZAI.NRS_BR_CD                                                     " & vbNewLine _
            & "       AND MGS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                           " & vbNewLine _
            & "       AND MGS.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
            & "   WHERE                                                                                 " & vbNewLine _
            & "     ZAI.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
            & "     AND ZAI.WH_CD = @WH_CD                                                              " & vbNewLine _
            & "     AND ZAI.TOU_NO = @TOU_NO                                                            " & vbNewLine _
            & "     AND MGS.SHOBOKIKEN_KB = '01'　--危険品のみ   --ADD 2021/10/01 024123                " & vbNewLine _
            & "     AND ZAI.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
            & "   GROUP BY                                                                              " & vbNewLine _
            & "     ZAI.NRS_BR_CD,                                                                      " & vbNewLine _
            & "     ZAI.WH_CD,                                                                          " & vbNewLine _
            & "     ZAI.TOU_NO                                                                          " & vbNewLine _
            & "   ) AS TOZ                                                                              " & vbNewLine _
            & "   ON                                                                                    " & vbNewLine _
            & "     TOZ.WH_CD = TOM.WH_CD                                                               " & vbNewLine _
            & "     AND TOZ.TOU_NO = TOM.TOU_NO                                                         " & vbNewLine _
            & " LEFT JOIN (                                                                             " & vbNewLine _
            & "   SELECT                                                                                " & vbNewLine _
            & "     ZAI.NRS_BR_CD,                                                                      " & vbNewLine _
            & "     ZAI.WH_CD,                                                                          " & vbNewLine _
            & "     ZAI.TOU_NO,                                                                         " & vbNewLine _
            & "     ZAI.SITU_NO,                                                                        " & vbNewLine _
            & "     SUM(CASE SUBSTRING(MGS.SHOBO_CD, 1, 1)                                              " & vbNewLine _
            & "         WHEN '4' THEN                                                                   " & vbNewLine _
            & "           CASE MGS.STD_IRIME_UT                                                         " & vbNewLine _
            & "             WHEN 'CC' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.001                   " & vbNewLine _
            & "             WHEN 'GL' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 3.79                    " & vbNewLine _
            & "             WHEN 'GR' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.001 / MGS.HIZYU       " & vbNewLine _
            & "             WHEN 'KG' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB / MGS.HIZYU               " & vbNewLine _
            & "             WHEN 'L ' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB                           " & vbNewLine _
            & "             WHEN 'LB' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.45 / MGS.HIZYU        " & vbNewLine _
            & "             WHEN 'm3' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB                           " & vbNewLine _
            & "             WHEN 'MG' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.000001 / MGS.HIZYU    " & vbNewLine _
            & "             WHEN 'ML' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.001 / MGS.HIZYU       " & vbNewLine _
            & "             WHEN 'OZ' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.03 / MGS.HIZYU        " & vbNewLine _
            & "             WHEN 'QT' THEN ZAI.PORA_ZAI_NB * MGS.STD_IRIME_NB * 0.95                    " & vbNewLine _
            & "             ELSE           ZAI.PORA_ZAI_NB * MGS.STD_WT_KGS                             " & vbNewLine _
            & "           END                                                                           " & vbNewLine _
            & "         ELSE                                                                            " & vbNewLine _
            & "           ZAI.PORA_ZAI_NB * MGS.STD_WT_KGS                                              " & vbNewLine _
            & "       END                                                                               " & vbNewLine _
            & "     ) AS ZAI_QTY                                                                        " & vbNewLine _
            & "   FROM                                                                                  " & vbNewLine _
            & "     $LM_TRN$..D_ZAI_TRS ZAI                                                             " & vbNewLine _
            & "   INNER JOIN                                                                            " & vbNewLine _
            & "     $LM_MST$..M_GOODS MGS                                                               " & vbNewLine _
            & "     ON                                                                                  " & vbNewLine _
            & "       MGS.NRS_BR_CD = ZAI.NRS_BR_CD                                                     " & vbNewLine _
            & "       AND MGS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                           " & vbNewLine _
            & "       AND MGS.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
            & "   WHERE                                                                                 " & vbNewLine _
            & "     ZAI.NRS_BR_CD = @NRS_BR_CD                                                          " & vbNewLine _
            & "     AND ZAI.WH_CD = @WH_CD                                                              " & vbNewLine _
            & "     AND ZAI.TOU_NO = @TOU_NO                                                            " & vbNewLine _
            & "     AND ZAI.SITU_NO = @SITU_NO                                                          " & vbNewLine _
            & "     AND MGS.SHOBOKIKEN_KB = '01'　--危険品のみ  --ADD 2021/10/01 024123                 " & vbNewLine _
            & "     AND ZAI.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
            & "   GROUP BY                                                                              " & vbNewLine _
            & "     ZAI.NRS_BR_CD,                                                                      " & vbNewLine _
            & "     ZAI.WH_CD,                                                                          " & vbNewLine _
            & "     ZAI.TOU_NO,                                                                         " & vbNewLine _
            & "     ZAI.SITU_NO                                                                         " & vbNewLine _
            & "   ) AS TSZ                                                                              " & vbNewLine _
            & "   ON                                                                                    " & vbNewLine _
            & "     TSZ.WH_CD = TSM.WH_CD                                                               " & vbNewLine _
            & "     AND TSZ.TOU_NO = TSM.TOU_NO                                                         " & vbNewLine _
            & "     AND TSZ.SITU_NO = TSM.SITU_NO                                                       " & vbNewLine _
            & " WHERE                                                                                   " & vbNewLine _
            & "   TOM.WH_CD = @WH_CD                                                                    " & vbNewLine _
            & "   AND TOM.TOU_NO = @TOU_NO                                                              " & vbNewLine _
            & "   AND TOM.SYS_DEL_FLG = '0'                                                             " & vbNewLine

    ''' <summary>
    ''' 商品数量計算
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CALC_QTY As String = "" _
            & " SELECT                                                                          " & vbNewLine _
            & "   CASE SUBSTRING(MGS.SHOBO_CD, 1, 1)                                            " & vbNewLine _
            & "     WHEN '4' THEN                                                               " & vbNewLine _
            & "       CASE MGS.STD_IRIME_UT                                                     " & vbNewLine _
            & "         WHEN 'CC' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.001                      " & vbNewLine _
            & "         WHEN 'GL' THEN @INKA_NB * MGS.STD_IRIME_NB * 3.79                       " & vbNewLine _
            & "         WHEN 'GR' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.001 / MGS.HIZYU          " & vbNewLine _
            & "         WHEN 'KG' THEN @INKA_NB * MGS.STD_IRIME_NB / MGS.HIZYU                  " & vbNewLine _
            & "         WHEN 'L ' THEN @INKA_NB * MGS.STD_IRIME_NB                              " & vbNewLine _
            & "         WHEN 'LB' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.45 / MGS.HIZYU           " & vbNewLine _
            & "         WHEN 'm3' THEN @INKA_NB * MGS.STD_IRIME_NB                              " & vbNewLine _
            & "         WHEN 'MG' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.000001 / MGS.HIZYU       " & vbNewLine _
            & "         WHEN 'ML' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.001 / MGS.HIZYU          " & vbNewLine _
            & "         WHEN 'OZ' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.03 / MGS.HIZYU           " & vbNewLine _
            & "         WHEN 'QT' THEN @INKA_NB * MGS.STD_IRIME_NB * 0.95                       " & vbNewLine _
            & "         ELSE           @INKA_NB * MGS.STD_WT_KGS                                " & vbNewLine _
            & "       END                                                                       " & vbNewLine _
            & "     ELSE                                                                        " & vbNewLine _
            & "       @INKA_NB * MGS.STD_WT_KGS                                                 " & vbNewLine _
            & "   END AS INK_QTY                                                                " & vbNewLine _
            & " FROM                                                                            " & vbNewLine _
            & "   $LM_MST$..M_GOODS MGS                                                         " & vbNewLine _
            & " WHERE                                                                           " & vbNewLine _
            & "   MGS.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
            & "   AND MGS.GOODS_CD_NRS = @GOODS_CD_NRS                                          " & vbNewLine _
            & "   AND MGS.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
            & "   AND MGS.SHOBOKIKEN_KB = '01'　--危険品のみ  --ADD 2021/10/01 024123           " & vbNewLine

    ''' <summary>
    ''' 属性系チェック検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHECK_ATTR As String = "" _
            & " SELECT                                                                                                  " & vbNewLine _
            & "   CASE WHEN CH1.NRS_BR_CD IS NULL THEN '1' ELSE '0' END AS DOKU_KB_ERR,                                 " & vbNewLine _
            & "   CASE WHEN CH2.NRS_BR_CD IS NULL THEN '1' ELSE '0' END AS KOUATHUGAS_KB_ERR,                           " & vbNewLine _
            & "   CASE WHEN CH3.NRS_BR_CD IS NULL THEN '1' ELSE '0' END AS YAKUZIHO_KB_ERR,                             " & vbNewLine _
            & "   CASE WHEN TSB.NRS_BR_CD IS NULL                                                                       " & vbNewLine _
            & "             AND ZSB.NRS_BR_CD IS NULL                                                                   " & vbNewLine _
            & "             AND MGS.SHOBOKIKEN_KB <> '03' THEN '1' ELSE '0' END AS SHOBO_CD_ERR                         " & vbNewLine _
            & " FROM                                                                                                    " & vbNewLine _
            & "   $LM_MST$..M_GOODS MGS                                                                                 " & vbNewLine _
            & " LEFT JOIN                                                                                               " & vbNewLine _
            & "   $LM_MST$..M_TOU_SITU_ZONE_CHK CH1                                                                     " & vbNewLine _
            & "   ON                                                                                                    " & vbNewLine _
            & "     CH1.WH_CD = @WH_CD                                                                                  " & vbNewLine _
            & "     AND CH1.TOU_NO = @TOU_NO                                                                            " & vbNewLine _
            & "     AND CH1.SITU_NO = @SITU_NO                                                                          " & vbNewLine _
            & "     AND CH1.ZONE_CD = @ZONE_CD                                                                          " & vbNewLine _
            & "     AND CH1.KBN_GROUP_CD = 'G001'                                                                       " & vbNewLine _
            & "     AND CH1.KBN_CD = MGS.DOKU_KB                                                                        " & vbNewLine _
            & "     AND CH1.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & " LEFT JOIN                                                                                               " & vbNewLine _
            & "   $LM_MST$..M_TOU_SITU_ZONE_CHK CH2                                                                     " & vbNewLine _
            & "   ON                                                                                                    " & vbNewLine _
            & "     CH2.WH_CD = @WH_CD                                                                                  " & vbNewLine _
            & "     AND CH2.TOU_NO = @TOU_NO                                                                            " & vbNewLine _
            & "     AND CH2.SITU_NO = @SITU_NO                                                                          " & vbNewLine _
            & "     AND CH2.ZONE_CD = @ZONE_CD                                                                          " & vbNewLine _
            & "     AND CH2.KBN_GROUP_CD = 'G012'                                                                       " & vbNewLine _
            & "     AND CH2.KBN_CD = MGS.KOUATHUGAS_KB                                                                  " & vbNewLine _
            & "     AND CH2.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & " LEFT JOIN                                                                                               " & vbNewLine _
            & "   $LM_MST$..M_TOU_SITU_ZONE_CHK CH3                                                                     " & vbNewLine _
            & "   ON                                                                                                    " & vbNewLine _
            & "     CH3.WH_CD = @WH_CD                                                                                  " & vbNewLine _
            & "     AND CH3.TOU_NO = @TOU_NO                                                                            " & vbNewLine _
            & "     AND CH3.SITU_NO = @SITU_NO                                                                          " & vbNewLine _
            & "     AND CH3.ZONE_CD = @ZONE_CD                                                                          " & vbNewLine _
            & "     AND CH3.KBN_GROUP_CD = 'G201'                                                                       " & vbNewLine _
            & "     AND CH3.KBN_CD = MGS.YAKUZIHO_KB                                                                    " & vbNewLine _
            & "     AND CH3.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & " LEFT JOIN                                                                                               " & vbNewLine _
            & "   $LM_MST$..M_TOU_SITU_SHOBO TSB                                                                        " & vbNewLine _
            & "   ON                                                                                                    " & vbNewLine _
            & "     TSB.WH_CD = @WH_CD                                                                                  " & vbNewLine _
            & "     AND TSB.TOU_NO = @TOU_NO                                                                            " & vbNewLine _
            & "     AND TSB.SITU_NO = @SITU_NO                                                                          " & vbNewLine _
            & "     AND '' = @ZONE_CD                                                                                   " & vbNewLine _
            & "     AND TSB.SHOBO_CD = MGS.SHOBO_CD                                                                     " & vbNewLine _
            & "     AND TSB.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & " LEFT JOIN                                                                                               " & vbNewLine _
            & "   $LM_MST$..M_ZONE_SHOBO ZSB                                                                            " & vbNewLine _
            & "   ON                                                                                                    " & vbNewLine _
            & "     ZSB.WH_CD = @WH_CD                                                                                  " & vbNewLine _
            & "     AND ZSB.TOU_NO = @TOU_NO                                                                            " & vbNewLine _
            & "     AND ZSB.SITU_NO = @SITU_NO                                                                          " & vbNewLine _
            & "     AND ZSB.ZONE_CD = @ZONE_CD                                                                          " & vbNewLine _
            & "     AND ZSB.SHOBO_CD = MGS.SHOBO_CD                                                                     " & vbNewLine _
            & "     AND ZSB.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & " WHERE                                                                                                   " & vbNewLine _
            & "   MGS.NRS_BR_CD = @NRS_BR_CD                                                                            " & vbNewLine _
            & "   AND MGS.GOODS_CD_NRS = @GOODS_CD_NRS                                                                  " & vbNewLine _
            & "   AND MGS.SYS_DEL_FLG = '0'                                                                             " & vbNewLine

#End Region 'SQL

#End Region 'Const

#Region "Method"

#Region "共通"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">変換元SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>変換後SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region '共通

#Region "検索"

    ''' <summary>
    ''' チェック処理不要フラグ検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectCheckFlg(ByVal ds As DataSet) As DataSet

        'テーブル名
        Const IN_TBL_NM As String = "LMZ340IN"
        Const OUT_TBL_NM As String = "LMZ340OUT_CHECK_FLG"

        '件数の初期化
        Dim cnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        'SQLの編集
        Dim sql As String = Me.SetSchemaNm(SQL_SELECT_CHECK_FLG, inRow("NRS_BR_CD").ToString)

        'SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'SQLパラメータを設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@CUST_CD", inRow("CUST_CD").ToString, DBDataType.CHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            'ログ出力
            MyBase.Logger.WriteSQLLog("LMZ340DAC", "SelectCheckFlg", cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)

                    '件数の設定
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count
                End If

            End Using

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

    ''' <summary>
    ''' 貯蔵最大数チェック検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectCheckCapa(ByVal ds As DataSet) As DataSet

        'テーブル名
        Const IN_TBL_NM As String = "LMZ340IN"
        Const OUT_TBL_NM As String = "LMZ340OUT_CHECK_CAPA"

        '件数の初期化
        Dim cnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        'SQLの編集
        Dim sql As String = Me.SetSchemaNm(SQL_SELECT_CHECK_CAPA, inRow("NRS_BR_CD").ToString)

        'SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'SQLパラメータを設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@WH_CD", inRow("WH_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@TOU_NO", inRow("TOU_NO").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SITU_NO", inRow("SITU_NO").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString, DBDataType.CHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            'ログ出力
            MyBase.Logger.WriteSQLLog("LMZ340DAC", "SelectCheckCapa", cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)

                    '件数の設定
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count
                End If

            End Using

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

    ''' <summary>
    ''' 商品数量計算
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectCalcQty(ByVal ds As DataSet) As DataSet

        'テーブル名
        Const IN_TBL_NM As String = "LMZ340IN"
        Const OUT_TBL_NM As String = "LMZ340OUT_CALC_QTY"

        '件数の初期化
        Dim cnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        'SQLの編集
        Dim sql As String = Me.SetSchemaNm(SQL_SELECT_CALC_QTY, inRow("NRS_BR_CD").ToString)

        'SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'SQLパラメータを設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", inRow("GOODS_CD_NRS").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@INKA_NB", inRow("INKA_NB").ToString, DBDataType.NUMERIC))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            'ログ出力
            MyBase.Logger.WriteSQLLog("LMZ340DAC", "SelectCalcQty", cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)

                    '件数の設定
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count
                End If

            End Using

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

    ''' <summary>
    ''' 属性系チェック検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectCheckAttr(ByVal ds As DataSet) As DataSet

        'テーブル名
        Const IN_TBL_NM As String = "LMZ340IN"
        Const OUT_TBL_NM As String = "LMZ340OUT_CHECK_ATTR"

        '件数の初期化
        Dim cnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        'SQLの編集
        Dim sql As String = Me.SetSchemaNm(SQL_SELECT_CHECK_ATTR, inRow("NRS_BR_CD").ToString)

        'SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'SQLパラメータを設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@WH_CD", inRow("WH_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@TOU_NO", inRow("TOU_NO").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SITU_NO", inRow("SITU_NO").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@ZONE_CD", inRow("ZONE_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", inRow("GOODS_CD_NRS").ToString, DBDataType.CHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            'ログ出力
            MyBase.Logger.WriteSQLLog("LMZ340DAC", "SelectCheckAttr", cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)

                    '件数の設定
                    cnt = ds.Tables(OUT_TBL_NM).Rows.Count
                End If

            End Using

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region '検索

#End Region 'Method

End Class
