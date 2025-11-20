' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM080DAC : 運送会社マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM080DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM080DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "テーブル名"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMM080IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMM080OUT"

    ''' <summary>
    ''' CUSTRPTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_CUSTRPT As String = "LMM080_UNSO_CUST_RPT"

#End Region

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(UNSOCO.UNSOCO_CD)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' M_UNSOCOデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                               " & vbNewLine _
                                            & "     UNSOCO.NRS_BR_CD                 AS NRS_BR_CD                    " & vbNewLine _
                                            & "    ,NRSBR.NRS_BR_NM                  AS NRS_BR_NM                    " & vbNewLine _
                                            & "    ,UNSOCO.UNSOCO_CD                 AS UNSOCO_CD                    " & vbNewLine _
                                            & "    ,UNSOCO.UNSOCO_BR_CD              AS UNSOCO_BR_CD                 " & vbNewLine _
                                            & "    ,UNSOCO.UNSOCO_NM                 AS UNSOCO_NM                    " & vbNewLine _
                                            & "    ,UNSOCO.UNSOCO_BR_NM              AS UNSOCO_BR_NM                 " & vbNewLine _
                                            & "    ,UNSOCO.UNSOCO_KB                 AS UNSOCO_KB                    " & vbNewLine _
                                            & "    ,UNSOCO.ZIP                       AS ZIP                          " & vbNewLine _
                                            & "    ,UNSOCO.AD_1                      AS AD_1                         " & vbNewLine _
                                            & "    ,UNSOCO.AD_2                      AS AD_2                         " & vbNewLine _
                                            & "    ,UNSOCO.AD_3                      AS AD_3                         " & vbNewLine _
                                            & "    ,UNSOCO.TEL                       AS TEL                          " & vbNewLine _
                                            & "    ,UNSOCO.FAX                       AS FAX                          " & vbNewLine _
                                            & "    ,UNSOCO.URL                       AS URL                          " & vbNewLine _
                                            & "    ,UNSOCO.PIC                       AS PIC                          " & vbNewLine _
                                            & "    ,UNSOCO.MOTOUKE_KB                AS MOTOUKE_KB                   " & vbNewLine _
                                            & "    ,KBN2.KBN_NM1                     AS MOTOUKE_KB_NM                " & vbNewLine _
                                            & "    ,UNSOCO.NRS_SBETU_CD              AS NRS_SBETU_CD                 " & vbNewLine _
                                            & "    ,UNSOCO.NIHUDA_YN                 AS NIHUDA_YN                    " & vbNewLine _
                                            & "    ,UNSOCO.TARE_YN                   AS TARE_YN                      " & vbNewLine _
                                            & "--(2012.08.17)支払サブ機能対応 --- START ---                          " & vbNewLine _
                                            & "    ,UNSOCO.SHIHARAITO_CD             AS SHIHARAITO_CD                " & vbNewLine _
                                            & "    ,SHIHARAITO.SHIHARAITO_NM         AS SHIHARAITO_NM                " & vbNewLine _
                                            & "--(2012.08.17)支払サブ機能対応 ---  END  ---                          " & vbNewLine _
                                            & "    ,UNSOCO.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD             " & vbNewLine _
                                            & "    ,TRF1.UNCHIN_TARIFF_REM           AS UNCHIN_TARIFF_REM            " & vbNewLine _
                                            & "    ,UNSOCO.EXTC_TARIFF_CD            AS EXTC_TARIFF_CD               " & vbNewLine _
                                            & "    ,EXTC.EXTC_TARIFF_REM             AS EXTC_TARIFF_REM              " & vbNewLine _
                                            & "    ,UNSOCO.BETU_KYORI_CD             AS BETU_KYORI_CD                " & vbNewLine _
                                            & "    ,KYORI.KYORI_REM                  AS BETU_KYORI_REM               " & vbNewLine _
                                            & "    ,UNSOCO.LAST_PU_TIME              AS LAST_PU_TIME                 " & vbNewLine _
                                            & "    ,UNSOCO.SYS_ENT_DATE              AS SYS_ENT_DATE                 " & vbNewLine _
                                            & "    ,USER1.USER_NM                    AS SYS_ENT_USER_NM              " & vbNewLine _
                                            & "    ,UNSOCO.SYS_UPD_DATE              AS SYS_UPD_DATE                 " & vbNewLine _
                                            & "    ,UNSOCO.SYS_UPD_TIME              AS SYS_UPD_TIME                 " & vbNewLine _
                                            & "    ,USER2.USER_NM                    AS SYS_UPD_USER_NM              " & vbNewLine _
                                            & "    ,UNSOCO.SYS_DEL_FLG               AS SYS_DEL_FLG                  " & vbNewLine _
                                            & "    ,KBN3.KBN_NM1                     AS SYS_DEL_NM                   " & vbNewLine _
                                            & "--要望番号:1275 yamanaka 2012.07.13 Start                             " & vbNewLine _
                                            & "    ,UNSOCO.CUST_UNSO_RYAKU_NM        AS CUST_UNSO_RYAKU_NM           " & vbNewLine _
                                            & "--要望番号:1275 yamanaka 2012.07.13 End                               " & vbNewLine _
                                            & "--要望番号:2140 kobayashi 2013.12.24 Start                            " & vbNewLine _
                                            & "    ,UNSOCO.PICKLIST_GRP_KBN          AS PICKLIST_GRP_KBN             " & vbNewLine _
                                            & "    ,UNSOCO.EDI_USED_KBN              AS EDI_USED_KBN                 " & vbNewLine _
                                            & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 ---      " & vbNewLine _
                                            & "    ,UNSOCO.NIFUDA_SCAN_YN            AS NIFUDA_SCAN_YN               " & vbNewLine _
                                            & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 ---      " & vbNewLine _
                                            & "--(2015.09.17)要望番号2408対応START   --- START ---                   " & vbNewLine _
                                            & "    ,UNSOCO.AUTO_DENP_NO_FLG          AS AUTO_DENP_NO_FLG             " & vbNewLine _
                                            & "    ,UNSOCO.AUTO_DENP_NO_KBN          AS AUTO_DENP_NO_KBN             " & vbNewLine _
                                            & "--(2015.09.17)要望番号2408対応END   --- END ---                       " & vbNewLine _
                                            & "-- FFEM荷札検品対応 20160610                                          " & vbNewLine _
                                            & "    ,UNSOCO.TAG_BARCD_KBN                                             " & vbNewLine _
                                            & "    ,UNSOCO.WH_NIFUDA_SCAN_YN         AS WH_NIFUDA_SCAN_YN            " & vbNewLine

    ''' <summary>
    ''' UNSOCUST_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                              " & vbNewLine _
                                             & "    UNSO_CUST_RPT.NRS_BR_CD               AS NRS_BR_CD               " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.UNSOCO_CD               AS UNSOCO_CD               " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.UNSOCO_BR_CD            AS UNSOCO_BR_CD            " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.PTN_ID                  AS PTN_ID                  " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.PTN_CD                  AS PTN_CD                  " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.MOTO_TYAKU_KB           AS MOTO_TYAKU_KB           " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.EDABAN                  AS EDABAN                  " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.CUST_CD_L               AS CUST_CD_L               " & vbNewLine _
                                             & "   ,UNSO_CUST_RPT.CUST_CD_M               AS CUST_CD_M               " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' 運送会社マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA As String = "FROM                                                           " & vbNewLine _
                                          & "                      $LM_MST$..M_UNSOCO         AS UNSOCO     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR         AS NRSBR      " & vbNewLine _
                                          & "        ON UNSOCO.NRS_BR_CD         = NRSBR.NRS_BR_CD          " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG        = '0'                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN2       " & vbNewLine _
                                          & "        ON UNSOCO.MOTOUKE_KB        = KBN2.KBN_CD              " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD        = 'M006'                   " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG         = '0'                      " & vbNewLine _
                                          & "      LEFT JOIN (                                                    " & vbNewLine _
                                          & "                      SELECT                                         " & vbNewLine _
                                          & "                             TARIFF1.NRS_BR_CD                       " & vbNewLine _
                                          & "                            ,TARIFF1.UNCHIN_TARIFF_CD                " & vbNewLine _
                                          & "                            ,TARIFF1.UNCHIN_TARIFF_REM               " & vbNewLine _
                                          & "                            ,TARIFF1.STR_DATE                        " & vbNewLine _
                                          & "                        FROM $LM_MST$..M_UNCHIN_TARIFF AS TARIFF1    " & vbNewLine _
                                          & "                       INNER JOIN (                                  " & vbNewLine _
                                          & "                                      SELECT                                                             " & vbNewLine _
                                          & "                                             TARIFF1.NRS_BR_CD                                           " & vbNewLine _
                                          & "                                            ,TARIFF1.UNCHIN_TARIFF_CD                                    " & vbNewLine _
                                          & "                                            ,TARIFF2.STR_DATE                                            " & vbNewLine _
                                          & "                                            ,MIN(TARIFF1.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA   " & vbNewLine _
                                          & "                                        FROM $LM_MST$..M_UNCHIN_TARIFF          AS TARIFF1               " & vbNewLine _
                                          & "                                       INNER JOIN (                                                      " & vbNewLine _
                                          & "                                                      SELECT                                             " & vbNewLine _
                                          & "                                                             MAX(STR_DATE) AS STR_DATE                   " & vbNewLine _
                                          & "                                                            ,NRS_BR_CD                                   " & vbNewLine _
                                          & "                                                            ,UNCHIN_TARIFF_CD                            " & vbNewLine _
                                          & "                                                       FROM $LM_MST$..M_UNCHIN_TARIFF     AS TARIFF2     " & vbNewLine _
                                          & "                                                      WHERE SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "                                                        AND   TARIFF2.UNCHIN_TARIFF_REM   IS NOT NULL    " & vbNewLine _
                                          & "                                                        AND   TARIFF2.UNCHIN_TARIFF_REM   <>  ''         " & vbNewLine _
                                          & "                                                        AND   TARIFF2.STR_DATE            <= @SYS_DATE   " & vbNewLine _
                                          & "                                                      GROUP BY                                           " & vbNewLine _
                                          & "                                                               TARIFF2.NRS_BR_CD                         " & vbNewLine _
                                          & "                                                              ,TARIFF2.UNCHIN_TARIFF_CD                  " & vbNewLine _
                                          & "                                                         ) AS TARIFF2                                    " & vbNewLine _
                                          & "                                            ON TARIFF1.NRS_BR_CD            = TARIFF2.NRS_BR_CD          " & vbNewLine _
                                          & "                                           AND TARIFF1.UNCHIN_TARIFF_CD     = TARIFF2.UNCHIN_TARIFF_CD   " & vbNewLine _
                                          & "                                           AND TARIFF1.STR_DATE             = TARIFF2.STR_DATE           " & vbNewLine _
                                          & "                                         WHERE TARIFF1.SYS_DEL_FLG          = '0'                        " & vbNewLine _
                                          & "                                         GROUP BY                                                        " & vbNewLine _
                                          & "                                                 TARIFF1.NRS_BR_CD                                       " & vbNewLine _
                                          & "                                                ,TARIFF1.UNCHIN_TARIFF_CD                                " & vbNewLine _
                                          & "                                                ,TARIFF2.STR_DATE                                        " & vbNewLine _
                                          & "                                   )  AS TARIFF2                                                         " & vbNewLine _
                                          & "                          ON TARIFF1.NRS_BR_CD            = TARIFF2.NRS_BR_CD                            " & vbNewLine _
                                          & "                         AND TARIFF1.UNCHIN_TARIFF_CD     = TARIFF2.UNCHIN_TARIFF_CD                     " & vbNewLine _
                                          & "                         AND TARIFF1.UNCHIN_TARIFF_CD_EDA = TARIFF2.UNCHIN_TARIFF_CD_EDA                 " & vbNewLine _
                                          & "                         AND TARIFF1.STR_DATE             = TARIFF2.STR_DATE                             " & vbNewLine _
                                          & "                       WHERE TARIFF1.SYS_DEL_FLG          = '0'                                          " & vbNewLine _
                                          & "                       GROUP BY                                                                          " & vbNewLine _
                                          & "                             TARIFF1.NRS_BR_CD                                                           " & vbNewLine _
                                          & "                            ,TARIFF1.UNCHIN_TARIFF_CD                                                    " & vbNewLine _
                                          & "                            ,TARIFF1.UNCHIN_TARIFF_REM                                                   " & vbNewLine _
                                          & "                            ,TARIFF1.STR_DATE                                                            " & vbNewLine _
                                          & "                  ) AS TRF1                                                                              " & vbNewLine _
                                          & "        ON   TRF1.NRS_BR_CD               =  UNSOCO.NRS_BR_CD                                            " & vbNewLine _
                                          & "        AND  TRF1.UNCHIN_TARIFF_CD        =  UNSOCO.UNCHIN_TARIFF_CD                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_EXTC_UNCHIN    AS EXTC       " & vbNewLine _
                                          & "        ON UNSOCO.NRS_BR_CD         = EXTC.NRS_BR_CD           " & vbNewLine _
                                          & "       AND UNSOCO.EXTC_TARIFF_CD    = EXTC.EXTC_TARIFF_CD      " & vbNewLine _
                                          & "       AND EXTC.JIS_CD              = '0000000'                " & vbNewLine _
                                          & "       AND EXTC.SYS_DEL_FLG         = '0'                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                          " & vbNewLine _
                                          & "           (SELECT                                             " & vbNewLine _
                                          & "                  NRS_BR_CD                                    " & vbNewLine _
                                          & "                 ,KYORI_CD                                     " & vbNewLine _
                                          & "                 ,MIN(ORIG_JIS_CD) AS  ORIG_JIS_CD             " & vbNewLine _
                                          & "                 ,MIN(DEST_JIS_CD) AS  DEST_JIS_CD             " & vbNewLine _
                                          & "                 ,MAX(KYORI_REM)   AS  KYORI_REM               " & vbNewLine _
                                          & "            FROM $LM_MST$..M_KYORI   AS  KYORI                 " & vbNewLine _
                                          & "            WHERE                                              " & vbNewLine _
                                          & "                  SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "            GROUP BY                                           " & vbNewLine _
                                          & "                    NRS_BR_CD                                  " & vbNewLine _
                                          & "                   ,KYORI_CD                                   " & vbNewLine _
                                          & "                           )      AS KYORI                     " & vbNewLine _
                                          & "        ON UNSOCO.NRS_BR_CD         = KYORI.NRS_BR_CD          " & vbNewLine _
                                          & "       AND UNSOCO.BETU_KYORI_CD     = KYORI.KYORI_CD           " & vbNewLine _
                                          & "--(2012.08.17)支払サブ機能対応 --- START ---                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SHIHARAITO     AS SHIHARAITO " & vbNewLine _
                                          & "        ON SHIHARAITO.NRS_BR_CD     = UNSOCO.NRS_BR_CD         " & vbNewLine _
                                          & "       AND SHIHARAITO.SHIHARAITO_CD = UNSOCO.SHIHARAITO_CD     " & vbNewLine _
                                          & "--(2012.08.17)支払サブ機能対応 ---  END  ---                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER           AS USER1      " & vbNewLine _
                                          & "       ON UNSOCO.SYS_ENT_USER       = USER1.USER_CD            " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG        = '0'                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER           AS USER2      " & vbNewLine _
                                          & "       ON UNSOCO.SYS_UPD_USER       = USER2.USER_CD            " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG        = '0'                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN3       " & vbNewLine _
                                          & "        ON UNSOCO.SYS_DEL_FLG       = KBN3.KBN_CD              " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD        = 'S051'                   " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG         = '0'                      " & vbNewLine


    ''' <summary>
    ''' 運送会社荷主別送り状マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA2 As String = "FROM                                                      " & vbNewLine _
                                        & " $LM_MST$..M_UNSO_CUST_RPT               AS UNSO_CUST_RPT    " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(UNSOCO_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                    " & vbNewLine _
                                         & "     UNSOCO.NRS_BR_CD                                       " & vbNewLine _
                                         & "    ,UNSOCO.UNSOCO_CD                                       " & vbNewLine _
                                         & "    ,UNSOCO.UNSOCO_BR_CD                                    " & vbNewLine _
                                         & "    ,UNSOCO.MOTOUKE_KB                                      " & vbNewLine

    ''' <summary>
    ''' ORDER BY(UNSO_CUST_RPT_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                                   " & vbNewLine _
                                         & "     UNSO_CUST_RPT.CUST_CD_L                                " & vbNewLine _
                                         & "    ,UNSO_CUST_RPT.CUST_CD_M                                " & vbNewLine _
                                         & " -- 名鉄対応(2499)                                          " & vbNewLine _
                                         & "    ,UNSO_CUST_RPT.PTN_ID                                   " & vbNewLine _
                                         & "    ,UNSO_CUST_RPT.MOTO_TYAKU_KB                            " & vbNewLine _
                                         & "    ,UNSO_CUST_RPT.PTN_CD                                   " & vbNewLine


#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 運送会社マスタ存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_UNSOCO As String = "SELECT                                      " & vbNewLine _
                                            & "   COUNT(UNSOCO_CD)  AS REC_CNT             " & vbNewLine _
                                            & "FROM  $LM_MST$..M_UNSOCO                     " & vbNewLine _
                                            & "WHERE NRS_BR_CD       = @NRS_BR_CD          " & vbNewLine _
                                            & "  AND UNSOCO_CD       = @UNSOCO_CD          " & vbNewLine _
                                            & "  AND UNSOCO_BR_CD    = @UNSOCO_BR_CD       " & vbNewLine

    ''' <summary>
    ''' 郵便番号コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_ZIP As String = "SELECT                               " & vbNewLine _
                                         & "   COUNT(ZIP_NO)  AS REC_CNT         " & vbNewLine _
                                         & " FROM $LM_MST$..M_ZIP                " & vbNewLine _
                                         & "WHERE ZIP_NO  = @ZIP                 " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    ''' <summary>
    ''' 新規登録SQL（M_UNSOCO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_UNSOCO                    " & vbNewLine _
                                       & "(                                                 " & vbNewLine _
                                       & "       NRS_BR_CD                                  " & vbNewLine _
                                       & "      ,UNSOCO_CD                                  " & vbNewLine _
                                       & "      ,UNSOCO_BR_CD                               " & vbNewLine _
                                       & "      ,UNSOCO_NM                                  " & vbNewLine _
                                       & "      ,UNSOCO_BR_NM                               " & vbNewLine _
                                       & "      ,MOTOUKE_KB                                 " & vbNewLine _
                                       & "      ,UNSOCO_KB                                  " & vbNewLine _
                                       & "      ,ZIP                                        " & vbNewLine _
                                       & "      ,AD_1                                       " & vbNewLine _
                                       & "      ,AD_2                                       " & vbNewLine _
                                       & "      ,AD_3                                       " & vbNewLine _
                                       & "      ,TEL                                        " & vbNewLine _
                                       & "      ,FAX                                        " & vbNewLine _
                                       & "      ,URL                                        " & vbNewLine _
                                       & "      ,PIC                                        " & vbNewLine _
                                       & "      ,NRS_SBETU_CD                               " & vbNewLine _
                                       & "      ,NIHUDA_YN                                  " & vbNewLine _
                                       & "      ,TARE_YN                                    " & vbNewLine _
                                       & "--(2012.08.17)支払サブ機能対応 --- START ---      " & vbNewLine _
                                       & "      ,SHIHARAITO_CD                              " & vbNewLine _
                                       & "--(2012.08.17)支払サブ機能対応 ---  END  ---      " & vbNewLine _
                                       & "      ,UNCHIN_TARIFF_CD                           " & vbNewLine _
                                       & "      ,EXTC_TARIFF_CD                             " & vbNewLine _
                                       & "      ,BETU_KYORI_CD                              " & vbNewLine _
                                       & "      ,LAST_PU_TIME                               " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                               " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                               " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                               " & vbNewLine _
                                       & "      ,SYS_ENT_USER                               " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                               " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                               " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                               " & vbNewLine _
                                       & "      ,SYS_UPD_USER                               " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                                " & vbNewLine _
                                       & "--要望番号:1275 yamanaka 2012.07.13 Start         " & vbNewLine _
                                       & "      ,CUST_UNSO_RYAKU_NM                         " & vbNewLine _
                                       & "--要望番号:1275 yamanaka 2012.07.13 End           " & vbNewLine _
                                       & "--要望番号:2140 kobayashi 2013.12.24 Start        " & vbNewLine _
                                       & "      ,PICKLIST_GRP_KBN                           " & vbNewLine _
                                       & "      ,EDI_USED_KBN                               " & vbNewLine _
                                       & "--要望番号:2140 kobayashi 2013.12.24 End          " & vbNewLine _
                                       & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- " & vbNewLine _
                                       & "      ,NIFUDA_SCAN_YN                                       " & vbNewLine _
                                       & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- " & vbNewLine _
                                       & "--(2015.09.17)要望番号2408対応START               " & vbNewLine _
                                       & "      ,AUTO_DENP_NO_FLG                           " & vbNewLine _
                                       & "      ,AUTO_DENP_NO_KBN                           " & vbNewLine _
                                       & "--(2015.09.17)要望番号2408対応END                 " & vbNewLine _
                                       & "-- FFEM荷札検品対応 20160610                      " & vbNewLine _
                                       & "      ,TAG_BARCD_KBN                              " & vbNewLine _
                                       & "      ,WH_NIFUDA_SCAN_YN                          " & vbNewLine _
                                       & "      ) VALUES (                                  " & vbNewLine _
                                       & "       @NRS_BR_CD                                 " & vbNewLine _
                                       & "      ,@UNSOCO_CD                                 " & vbNewLine _
                                       & "      ,@UNSOCO_BR_CD                              " & vbNewLine _
                                       & "      ,@UNSOCO_NM                                 " & vbNewLine _
                                       & "      ,@UNSOCO_BR_NM                              " & vbNewLine _
                                       & "      ,@MOTOUKE_KB                                " & vbNewLine _
                                       & "      ,@UNSOCO_KB                                 " & vbNewLine _
                                       & "      ,@ZIP                                       " & vbNewLine _
                                       & "      ,@AD_1                                      " & vbNewLine _
                                       & "      ,@AD_2                                      " & vbNewLine _
                                       & "      ,@AD_3                                      " & vbNewLine _
                                       & "      ,@TEL                                       " & vbNewLine _
                                       & "      ,@FAX                                       " & vbNewLine _
                                       & "      ,@URL                                       " & vbNewLine _
                                       & "      ,@PIC                                       " & vbNewLine _
                                       & "      ,@NRS_SBETU_CD                              " & vbNewLine _
                                       & "      ,@NIHUDA_YN                                 " & vbNewLine _
                                       & "      ,@TARE_YN                                   " & vbNewLine _
                                       & "--(2012.08.17)支払サブ機能対応 --- START ---      " & vbNewLine _
                                       & "      ,@SHIHARAITO_CD                             " & vbNewLine _
                                       & "--(2012.08.17)支払サブ機能対応 ---  END  ---      " & vbNewLine _
                                       & "      ,@UNCHIN_TARIFF_CD                          " & vbNewLine _
                                       & "      ,@EXTC_TARIFF_CD                            " & vbNewLine _
                                       & "      ,@BETU_KYORI_CD                             " & vbNewLine _
                                       & "      ,@LAST_PU_TIME                              " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                              " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                              " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                              " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                              " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                              " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                              " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                              " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                              " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                               " & vbNewLine _
                                       & "--要望番号:1275 yamanaka 2012.07.13 Start         " & vbNewLine _
                                       & "      ,@CUST_UNSO_RYAKU_NM                        " & vbNewLine _
                                       & "--要望番号:1275 yamanaka 2012.07.13 End           " & vbNewLine _
                                       & "--要望番号:2140 kobayashi 2013.12.24 Start        " & vbNewLine _
                                       & "      ,@PICKLIST_GRP_KBN                          " & vbNewLine _
                                       & "      ,@EDI_USED_KBN                              " & vbNewLine _
                                       & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- " & vbNewLine _
                                       & "      ,@NIFUDA_SCAN_YN                                    " & vbNewLine _
                                       & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- " & vbNewLine _
                                       & "--(2015.09.17)要望番号2408対応START               " & vbNewLine _
                                       & "      ,@AUTO_DENP_NO_FLG                          " & vbNewLine _
                                       & "      ,@AUTO_DENP_NO_KBN                          " & vbNewLine _
                                       & "--(2015.09.17)要望番号2408対応END                 " & vbNewLine _
                                       & "-- FFEM荷札検品対応 20160610                      " & vbNewLine _
                                       & "      ,@TAG_BARCD_KBN                             " & vbNewLine _
                                       & "      ,@WH_NIFUDA_SCAN_YN                         " & vbNewLine _
                                       & ")                                                 " & vbNewLine

    ''' <summary>
    ''' 新規登録SQL（M_UNSO_CUST_RPT）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_UNSORPT As String = "INSERT INTO              " & vbNewLine _
                                       & "   $LM_MST$..M_UNSO_CUST_RPT     " & vbNewLine _
                                       & "(                                " & vbNewLine _
                                       & "       NRS_BR_CD                 " & vbNewLine _
                                       & "      ,UNSOCO_CD                 " & vbNewLine _
                                       & "      ,UNSOCO_BR_CD              " & vbNewLine _
                                       & "      ,PTN_ID                    " & vbNewLine _
                                       & "      ,PTN_CD                    " & vbNewLine _
                                       & "      ,MOTO_TYAKU_KB             " & vbNewLine _
                                       & "      ,EDABAN                    " & vbNewLine _
                                       & "      ,CUST_CD_L                 " & vbNewLine _
                                       & "      ,CUST_CD_M                 " & vbNewLine _
                                       & "      ,SYS_ENT_DATE              " & vbNewLine _
                                       & "      ,SYS_ENT_TIME              " & vbNewLine _
                                       & "      ,SYS_ENT_PGID              " & vbNewLine _
                                       & "      ,SYS_ENT_USER              " & vbNewLine _
                                       & "      ,SYS_UPD_DATE              " & vbNewLine _
                                       & "      ,SYS_UPD_TIME              " & vbNewLine _
                                       & "      ,SYS_UPD_PGID              " & vbNewLine _
                                       & "      ,SYS_UPD_USER              " & vbNewLine _
                                       & "      ,SYS_DEL_FLG               " & vbNewLine _
                                       & "      ) VALUES (                 " & vbNewLine _
                                       & "       @NRS_BR_CD                " & vbNewLine _
                                       & "      ,@UNSOCO_CD                " & vbNewLine _
                                       & "      ,@UNSOCO_BR_CD             " & vbNewLine _
                                       & "      ,@PTN_ID                   " & vbNewLine _
                                       & "      ,@PTN_CD                   " & vbNewLine _
                                       & "      ,@MOTO_TYAKU_KB            " & vbNewLine _
                                       & "      ,@EDABAN                   " & vbNewLine _
                                       & "      ,@CUST_CD_L                " & vbNewLine _
                                       & "      ,@CUST_CD_M                " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE             " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME             " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID             " & vbNewLine _
                                       & "      ,@SYS_ENT_USER             " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE             " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME             " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID             " & vbNewLine _
                                       & "      ,@SYS_UPD_USER             " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG              " & vbNewLine _
                                       & ")                                " & vbNewLine

#End Region

#Region "Delete"

    ''' <summary>
    ''' 物理削除SQL（M_UNSO_CUST_RPT）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_UNSO_CUST_RPT As String = "DELETE FROM $LM_MST$..M_UNSO_CUST_RPT       " & vbNewLine _
                                                     & " WHERE                                   " & vbNewLine _
                                                     & "         NRS_BR_CD     = @NRS_BR_CD      " & vbNewLine _
                                                     & "   AND   UNSOCO_CD     = @UNSOCO_CD      " & vbNewLine _
                                                     & "   AND   UNSOCO_BR_CD  = @UNSOCO_BR_CD   " & vbNewLine _
                                                     & "-- 名鉄対応(要望番号2499)                " & vbNewLine _
                                                     & "--   AND   PTN_ID        = '11'          " & vbNewLine _
                                                     & "     AND   PTN_ID IN                     " & vbNewLine _
                                                     & "           (                             " & vbNewLine _
                                                     & "             SELECT KBN_CD               " & vbNewLine _
                                                     & "             FROM $LM_MST$..Z_KBN        " & vbNewLine _
                                                     & "             WHERE SYS_DEL_FLG = '0'     " & vbNewLine _
                                                     & "               AND KBN_GROUP_CD = 'T007' " & vbNewLine _
                                                     & "               AND KBN_NM4 = '1'         " & vbNewLine _
                                                     & "           )                             " & vbNewLine



#End Region

#Region "UPDATE"

    ''' <summary>
    ''' 更新SQL（M_UNSOCO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_UNSOCO SET                    " & vbNewLine _
                                       & "        UNSOCO_NM           = @UNSOCO_NM         " & vbNewLine _
                                       & "       ,UNSOCO_BR_NM        = @UNSOCO_BR_NM      " & vbNewLine _
                                       & "       ,UNSOCO_KB           = @UNSOCO_KB         " & vbNewLine _
                                       & "       ,ZIP                 = @ZIP               " & vbNewLine _
                                       & "       ,AD_1                = @AD_1              " & vbNewLine _
                                       & "       ,AD_2                = @AD_2              " & vbNewLine _
                                       & "       ,AD_3                = @AD_3              " & vbNewLine _
                                       & "       ,TEL                 = @TEL               " & vbNewLine _
                                       & "       ,FAX                 = @FAX               " & vbNewLine _
                                       & "       ,URL                 = @URL               " & vbNewLine _
                                       & "       ,PIC                 = @PIC               " & vbNewLine _
                                       & "       ,MOTOUKE_KB          = @MOTOUKE_KB        " & vbNewLine _
                                       & "       ,NRS_SBETU_CD        = @NRS_SBETU_CD      " & vbNewLine _
                                       & "       ,NIHUDA_YN           = @NIHUDA_YN         " & vbNewLine _
                                       & "       ,TARE_YN             = @TARE_YN           " & vbNewLine _
                                       & "--(2012.08.17)支払サブ機能対応 --- START ---     " & vbNewLine _
                                       & "       ,SHIHARAITO_CD       = @SHIHARAITO_CD     " & vbNewLine _
                                       & "--(2012.08.17)支払サブ機能対応 ---  END  ---     " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_CD    = @UNCHIN_TARIFF_CD  " & vbNewLine _
                                       & "       ,EXTC_TARIFF_CD      = @EXTC_TARIFF_CD    " & vbNewLine _
                                       & "       ,BETU_KYORI_CD       = @BETU_KYORI_CD     " & vbNewLine _
                                       & "       ,LAST_PU_TIME        = @LAST_PU_TIME      " & vbNewLine _
                                       & "--要望番号:1275 yamanaka 2012.07.13 Start        " & vbNewLine _
                                       & "       ,CUST_UNSO_RYAKU_NM  = @CUST_UNSO_RYAKU_NM" & vbNewLine _
                                       & "--要望番号:1275 yamanaka 2012.07.13 End          " & vbNewLine _
                                       & "--要望番号:2140 kobayashi 2013.12.24 Start       " & vbNewLine _
                                       & "       ,PICKLIST_GRP_KBN    = @PICKLIST_GRP_KBN  " & vbNewLine _
                                       & "       ,EDI_USED_KBN        = @EDI_USED_KBN      " & vbNewLine _
                                       & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- " & vbNewLine _
                                       & "       ,NIFUDA_SCAN_YN      = @NIFUDA_SCAN_YN                    " & vbNewLine _
                                       & "--(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- " & vbNewLine _
                                       & "--(2015.09.17)要望番号2408対応START              " & vbNewLine _
                                       & "       ,AUTO_DENP_NO_FLG    = @AUTO_DENP_NO_FLG  " & vbNewLine _
                                       & "       ,AUTO_DENP_NO_KBN    = @AUTO_DENP_NO_KBN  " & vbNewLine _
                                       & "--(2015.09.17)要望番号2408対応END                " & vbNewLine _
                                       & "-- FFEM荷札検品対応 20160610                     " & vbNewLine _
                                       & "       ,TAG_BARCD_KBN       = @TAG_BARCD_KBN     " & vbNewLine _
                                       & "       ,WH_NIFUDA_SCAN_YN   = @WH_NIFUDA_SCAN_YN " & vbNewLine _
                                       & "       ,SYS_UPD_DATE        = @SYS_UPD_DATE      " & vbNewLine _
                                       & "       ,SYS_UPD_TIME        = @SYS_UPD_TIME      " & vbNewLine _
                                       & "       ,SYS_UPD_PGID        = @SYS_UPD_PGID      " & vbNewLine _
                                       & "       ,SYS_UPD_USER        = @SYS_UPD_USER      " & vbNewLine _
                                       & " WHERE                                           " & vbNewLine _
                                       & "         NRS_BR_CD          = @NRS_BR_CD         " & vbNewLine _
                                       & "   AND   UNSOCO_CD          = @UNSOCO_CD         " & vbNewLine _
                                       & "   AND   UNSOCO_BR_CD       = @UNSOCO_BR_CD      " & vbNewLine
#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL（M_UNSOCO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_UNSOCO SET           " & vbNewLine _
                                       & "        SYS_UPD_DATE   = @SYS_UPD_DATE  " & vbNewLine _
                                       & "       ,SYS_UPD_TIME   = @SYS_UPD_TIME  " & vbNewLine _
                                       & "       ,SYS_UPD_PGID   = @SYS_UPD_PGID  " & vbNewLine _
                                       & "       ,SYS_UPD_USER   = @SYS_UPD_USER  " & vbNewLine _
                                       & "       ,SYS_DEL_FLG    = @SYS_DEL_FLG   " & vbNewLine _
                                       & " WHERE                                  " & vbNewLine _
                                       & "         NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                       & "   AND   UNSOCO_CD     = @UNSOCO_CD     " & vbNewLine _
                                       & "   AND   UNSOCO_BR_CD  = @UNSOCO_BR_CD  " & vbNewLine



    ''' <summary>
    ''' 削除・復活SQL（M_UNSO_CUST_RPT）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UNSO_CUST_RPT As String = "UPDATE                                  " & vbNewLine _
                                                     & "   $LM_MST$..M_UNSO_CUST_RPT            " & vbNewLine _
                                                     & "SET                                     " & vbNewLine _
                                                     & "   SYS_UPD_DATE  = @SYS_UPD_DATE        " & vbNewLine _
                                                     & "  ,SYS_UPD_TIME  = @SYS_UPD_TIME        " & vbNewLine _
                                                     & "  ,SYS_UPD_PGID  = @SYS_UPD_PGID        " & vbNewLine _
                                                     & "  ,SYS_UPD_USER  = @SYS_UPD_USER        " & vbNewLine _
                                                     & "  ,SYS_DEL_FLG   = @SYS_DEL_FLG         " & vbNewLine _
                                                     & " WHERE                                  " & vbNewLine _
                                                     & "         NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                                     & "   AND   UNSOCO_CD     = @UNSOCO_CD     " & vbNewLine _
                                                     & "   AND   UNSOCO_BR_CD  = @UNSOCO_BR_CD  " & vbNewLine _
                                                     & "-- 名鉄対応(2499) 荷札追加              " & vbNewLine _
                                                     & "--  AND   PTN_ID        = '11'          " & vbNewLine _
                                                     & "    AND   PTN_ID IN                     " & vbNewLine _
                                                     & "          (                             " & vbNewLine _
                                                     & "            SELECT KBN_CD               " & vbNewLine _
                                                     & "            FROM $LM_MST$..Z_KBN        " & vbNewLine _
                                                     & "            WHERE SYS_DEL_FLG = '0'     " & vbNewLine _
                                                     & "              AND KBN_GROUP_CD = 'T007' " & vbNewLine _
                                                     & "              AND KBN_NM4 = '1'         " & vbNewLine _
                                                     & "          )                             " & vbNewLine


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
    ''' 運送会社マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM080DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM080DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetFromDate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運送会社マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM080DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM080DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM080DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetFromDate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("UNSOCO_KB", "UNSOCO_KB")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("URL", "URL")
        map.Add("PIC", "PIC")
        map.Add("MOTOUKE_KB", "MOTOUKE_KB")
        map.Add("MOTOUKE_KB_NM", "MOTOUKE_KB_NM")
        map.Add("NRS_SBETU_CD", "NRS_SBETU_CD")
        map.Add("NIHUDA_YN", "NIHUDA_YN")
        map.Add("TARE_YN", "TARE_YN")
        '(2012.08.17)支払サブ機能対応 --- START ---
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        map.Add("SHIHARAITO_NM", "SHIHARAITO_NM")
        '(2012.08.17)支払サブ機能対応 ---  END  ---
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        map.Add("BETU_KYORI_CD", "BETU_KYORI_CD")
        map.Add("BETU_KYORI_REM", "BETU_KYORI_REM")
        map.Add("LAST_PU_TIME", "LAST_PU_TIME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        '要望番号:1275 yamanaka 2012.07.13 Start
        map.Add("CUST_UNSO_RYAKU_NM", "CUST_UNSO_RYAKU_NM")
        '要望番号:1275 yamanaka 2012.07.13 End
        '要望番号:2140 kobayashi 2013.12.24 Start
        map.Add("PICKLIST_GRP_KBN", "PICKLIST_GRP_KBN")
        '要望番号:2140 kobayashi 2013.12.24 End
        map.Add("EDI_USED_KBN", "EDI_USED_KBN")
        '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
        map.Add("NIFUDA_SCAN_YN", "NIFUDA_SCAN_YN")
        '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 
        '要望番号:2408 2015.09.17 追加START
        map.Add("AUTO_DENP_NO_FLG", "AUTO_DENP_NO_FLG")
        map.Add("AUTO_DENP_NO_KBN", "AUTO_DENP_NO_KBN")
        '要望番号:2408 2015.09.17 追加END

#If True Then ' FFEM荷札検品対応 20160610 added inoue
        map.Add("TAG_BARCD_KBN", "TAG_BARCD_KBN")
#End If
        map.Add("WH_NIFUDA_SCAN_YN", "WH_NIFUDA_SCAN_YN")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_OUT)

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(S_USER)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (UNSOCO.SYS_DEL_FLG = @SYS_DEL_FLG  OR UNSOCO.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (UNSOCO.NRS_BR_CD = @NRS_BR_CD OR UNSOCO.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_CD LIKE @UNSOCO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_NM LIKE @UNSOCO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_BR_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_BR_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("MOTOUKE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.MOTOUKE_KB = @MOTOUKE_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTOUKE_KB", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 運送会社荷主別送り状マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社荷主別送り状マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM080DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM080DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM080DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("MOTO_TYAKU_KB", "MOTO_TYAKU_KB")
        map.Add("EDABAN", "EDABAN")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_CUSTRPT)

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_UNSOCUSTRPT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (UNSO_CUST_RPT.NRS_BR_CD = @NRS_BR_CD OR UNSO_CUST_RPT.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO_CUST_RPT.UNSOCO_CD LIKE @UNSOCO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO_CUST_RPT.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("PTN_ID").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSO_CUST_RPT.PTN_ID = @PTN_ID")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(郵便番号マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamZipExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'ハイフン入力があれば除去
            If IsNumeric(.Item("ZIP").ToString()) = False Then

                Dim prmZip As String = String.Empty
                prmZip = System.Text.RegularExpressions.Regex.Replace(.Item("ZIP").ToString(), "[^0-9]", "")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", prmZip, DBDataType.NVARCHAR))
                Exit Sub

            End If

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub


#End Region

#Region "設定処理"

    '2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>郵便番号マスタ件数取得SQLの構築・発行</remarks>
    'Private Function CheckExistZipM(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM080DAC.SQL_EXIT_ZIP, Me._Row.Item("USER_BR_CD").ToString()))

    '    Dim reader As SqlDataReader = Nothing

    '    'SQLパラメータ初期化/設定
    '    Call Me.SetParamZipExistChk()

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM080DAC", "CheckExistZipM", cmd)

    '    'SQLの発行
    '    reader = MyBase.GetSelectResult(cmd)

    '    cmd.Parameters.Clear()

    '    '処理件数の設定
    '    reader.Read()
    '    MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
    '    reader.Close()

    '    Return ds

    'End Function
    '2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' 運送会社マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectUnsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM080DAC.SQL_EXIT_UNSOCO)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "SelectUnsocoM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistUnsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM080DAC.SQL_EXIT_UNSOCO, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "CheckExistUnsocoM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#Region "Insert"

    ''' <summary>
    ''' 運送会社マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM080DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "InsertUnsocoM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 運送会社荷主別送り状マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社荷主別送り状マスタ新規登録SQLの構築・発行</remarks>
    Private Function DelCustRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM080DAC.SQL_DEL_UNSO_CUST_RPT _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "DelCustRptM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 運送会社荷主別送り状マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社荷主別送り状マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertCustRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CUSTRPT)

        Dim max As Integer = inTbl.Rows.Count - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM080DAC.SQL_INSERT_UNSORPT _
                                                                           , inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

            For i As Integer = 0 To max

                'SQLパラメータ初期化/設定
                Me._SqlPrmList = New ArrayList()

                'INTableの条件rowの格納
                Me._Row = inTbl.Rows(i)

                'パラメータ設定
                Call Me.SetDataInsertParameter(Me._SqlPrmList)
                Call Me.SetUnsoRptParam(Me._Row, Me._SqlPrmList)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM080DAC", "InsertUnsoCustRptM", cmd)

                'SQLの発行
                MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

                'パラメータの初期化
                cmd.Parameters.Clear()

            Next

        End If

        Return ds

    End Function

#End Region

#Region "Update"

    ''' <summary>
    ''' 運送会社マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateUnsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM080DAC.SQL_UPDATE _
                                                                                     , LMM080DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "UpdateUnsocoM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "Delete"

    ''' <summary>
    ''' 運送会社マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteUnsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM080DAC.SQL_DELETE _
                                                                                           , LMM080DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                           , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "DeleteUnsocoM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 運送会社荷主別送り状マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別荷主マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteCustRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM080DAC.SQL_DELETE_UNSO_CUST_RPT , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM080DAC", "DeleteCustRptM", cmd)

        Return ds

    End Function

#End Region

#Region "スキーマ名"

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(運送会社マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(From句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFromDate()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録_運送会社Ｍ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        '更新項目
        Call Me.SetParamDelUpdate()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)(荷主送り状マスタ)
    ''' </summary>
    ''' <remarks>排他なし</remarks>
    Private Sub SetParamDelUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

    End Sub



    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_運送会社Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", .Item("UNSOCO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_NM", .Item("UNSOCO_BR_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTOUKE_KB", .Item("MOTOUKE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_KB", .Item("UNSOCO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", .Item("AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_2", .Item("AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", .Item("TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", .Item("FAX").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@URL", .Item("URL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PIC", .Item("PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SBETU_CD", .Item("NRS_SBETU_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_YN", .Item("NIHUDA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARE_YN", .Item("TARE_YN").ToString(), DBDataType.CHAR))
            '(2012.08.17)支払サブ機能対応 --- START ---
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.NVARCHAR))
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_KYORI_CD", .Item("BETU_KYORI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PU_TIME", .Item("LAST_PU_TIME").ToString(), DBDataType.CHAR))
            '要望番号:1275 yamanaka 2012.07.13 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_UNSO_RYAKU_NM", .Item("CUST_UNSO_RYAKU_NM").ToString(), DBDataType.NVARCHAR))
            '要望番号:1275 yamanaka 2012.07.13 End
            '要望番号:2140 kobayashi 2013.12.24 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICKLIST_GRP_KBN", .Item("PICKLIST_GRP_KBN").ToString(), DBDataType.NVARCHAR))
            '要望番号:2140 kobayashi 2013.12.24 End
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_USED_KBN", .Item("EDI_USED_KBN").ToString(), DBDataType.NVARCHAR))
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIFUDA_SCAN_YN", .Item("NIFUDA_SCAN_YN").ToString(), DBDataType.CHAR))
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 
            '要望番号:2408 2015.09.17 追加START
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO_FLG", .Item("AUTO_DENP_NO_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO_KBN", .Item("AUTO_DENP_NO_KBN").ToString(), DBDataType.CHAR))
            '要望番号:2408 2015.09.17 追加END

#If True Then ' FFEM荷札検品対応 20160610 added inoue
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAG_BARCD_KBN", .Item("TAG_BARCD_KBN").ToString(), DBDataType.CHAR))
#End If
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NIFUDA_SCAN_YN", .Item("WH_NIFUDA_SCAN_YN").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_運送会社荷主別送り状Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnsoRptParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PTN_ID", .Item("PTN_ID").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PTN_CD", .Item("PTN_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_TYAKU_KB", .Item("MOTO_TYAKU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDABAN", .Item("EDABAN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        End With

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(運送会社荷主別送り状Ｍ登録時))
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(運送会社荷主別送り状Ｍ更新時))
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", Me._Row.Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", Me._Row.Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
