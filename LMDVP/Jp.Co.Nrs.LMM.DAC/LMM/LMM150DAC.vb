' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM150DAC : 倉庫マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM150DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM150DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
#Region "言語区分"
    '言語区分_日本語
    Private Const LANG_KBN_JA As String = "0"
    '言語区分_英語
    Private Const LANG_KBN_EN As String = "1"
    '言語区分_韓国語
    Private Const LANG_KBN_KO As String = "2"
    '言語区分_中国語
    Private Const LANG_KBN_CN As String = "3"
#End Region
    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end    

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(SOKO.WH_CD)      AS SELECT_CNT   " & vbNewLine


    'START KIM 2012/09/12 要望番号1404 

    'START YANAI 要望番号394
    '''' <summary>
    '''' 倉庫マスタデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                 " & vbNewLine _
    '                                        & "       SOKO.NRS_BR_CD                       AS NRS_BR_CD                " & vbNewLine _
    '                                        & "      ,NRSBR.NRS_BR_NM                      AS NRS_BR_NM                " & vbNewLine _
    '                                        & "      ,SOKO.WH_CD                           AS WH_CD                    " & vbNewLine _
    '                                        & "      ,SOKO.WH_NM                           AS WH_NM                    " & vbNewLine _
    '                                        & "      ,SOKO.TEL                             AS TEL                      " & vbNewLine _
    '                                        & "      ,SOKO.FAX                             AS FAX                      " & vbNewLine _
    '                                        & "      ,SOKO.ZIP                             AS ZIP                      " & vbNewLine _
    '                                        & "      ,SOKO.AD_1                            AS AD_1                     " & vbNewLine _
    '                                        & "      ,SOKO.AD_2                            AS AD_2                     " & vbNewLine _
    '                                        & "      ,SOKO.AD_3                            AS AD_3                     " & vbNewLine _
    '                                        & "      ,SOKO.WH_KB                           AS WH_KB                    " & vbNewLine _
    '                                        & "      ,SOKO.JIS_CD                          AS JIS_CD                   " & vbNewLine _
    '                                        & "      ,JIS.KEN                              AS KEN                      " & vbNewLine _
    '                                        & "      ,JIS.SHI                              AS SHI                      " & vbNewLine _
    '                                        & "      ,SOKO.NIHUDA_MX_CNT                   AS NIHUDA_MX_CNT            " & vbNewLine _
    '                                        & "      ,SOKO.INKA_YOTEI_YN                   AS INKA_YOTEI_YN            " & vbNewLine _
    '                                        & "      ,SOKO.INKA_UKE_PRT_YN                 AS INKA_UKE_PRT_YN          " & vbNewLine _
    '                                        & "      ,SOKO.INKA_KENPIN_YN                  AS INKA_KENPIN_YN           " & vbNewLine _
    '                                        & "      ,SOKO.INKA_KAKUNIN_YN                 AS INKA_KAKUNIN_YN          " & vbNewLine _
    '                                        & "      ,SOKO.INKA_INFO_YN                    AS INKA_INFO_YN             " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_SASHIZU_PRT_YN            AS OUTKA_SASHIZU_PRT_YN     " & vbNewLine _
    '                                        & "      ,SOKO.OUTOKA_KANRYO_YN                AS OUTOKA_KANRYO_YN         " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_KENPIN_YN                 AS OUTKA_KENPIN_YN          " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_INFO_YN                   AS OUTKA_INFO_YN            " & vbNewLine _
    '                                        & "      ,SOKO.LOC_MANAGER_YN                  AS LOC_MANAGER_YN           " & vbNewLine _
    '                                        & "      ,SOKO.TOU_KANRI_YN                    AS TOU_KANRI_YN             " & vbNewLine _
    '                                        & "      ,SOKO.TOUHAN_SASHIZU_YN               AS TOUHAN_SASHIZU_YN        " & vbNewLine _
    '                                        & "      ,SOKO.SYS_ENT_DATE                    AS SYS_ENT_DATE             " & vbNewLine _
    '                                        & "      ,USER1.USER_NM                        AS SYS_ENT_USER_NM          " & vbNewLine _
    '                                        & "      ,SOKO.SYS_UPD_DATE                    AS SYS_UPD_DATE             " & vbNewLine _
    '                                        & "      ,SOKO.SYS_UPD_TIME                    AS SYS_UPD_TIME             " & vbNewLine _
    '                                        & "      ,USER2.USER_NM                        AS SYS_UPD_USER_NM          " & vbNewLine _
    '                                        & "      ,SOKO.SYS_DEL_FLG                     AS SYS_DEL_FLG              " & vbNewLine _
    '                                        & "      ,KBN1.KBN_NM1                         AS SYS_DEL_NM               " & vbNewLine
    '''' <summary>
    '''' 倉庫マスタデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                 " & vbNewLine _
    '                                        & "       SOKO.NRS_BR_CD                       AS NRS_BR_CD                " & vbNewLine _
    '                                        & "      ,NRSBR.NRS_BR_NM                      AS NRS_BR_NM                " & vbNewLine _
    '                                        & "      ,SOKO.WH_CD                           AS WH_CD                    " & vbNewLine _
    '                                        & "      ,SOKO.WH_NM                           AS WH_NM                    " & vbNewLine _
    '                                        & "      ,SOKO.TEL                             AS TEL                      " & vbNewLine _
    '                                        & "      ,SOKO.FAX                             AS FAX                      " & vbNewLine _
    '                                        & "      ,SOKO.ZIP                             AS ZIP                      " & vbNewLine _
    '                                        & "      ,SOKO.AD_1                            AS AD_1                     " & vbNewLine _
    '                                        & "      ,SOKO.AD_2                            AS AD_2                     " & vbNewLine _
    '                                        & "      ,SOKO.AD_3                            AS AD_3                     " & vbNewLine _
    '                                        & "      ,SOKO.WH_KB                           AS WH_KB                    " & vbNewLine _
    '                                        & "      ,SOKO.JIS_CD                          AS JIS_CD                   " & vbNewLine _
    '                                        & "      ,JIS.KEN                              AS KEN                      " & vbNewLine _
    '                                        & "      ,JIS.SHI                              AS SHI                      " & vbNewLine _
    '                                        & "      ,SOKO.NIHUDA_MX_CNT                   AS NIHUDA_MX_CNT            " & vbNewLine _
    '                                        & "      ,SOKO.INKA_YOTEI_YN                   AS INKA_YOTEI_YN            " & vbNewLine _
    '                                        & "      ,SOKO.INKA_UKE_PRT_YN                 AS INKA_UKE_PRT_YN          " & vbNewLine _
    '                                        & "      ,SOKO.INKA_KENPIN_YN                  AS INKA_KENPIN_YN           " & vbNewLine _
    '                                        & "      ,SOKO.INKA_KAKUNIN_YN                 AS INKA_KAKUNIN_YN          " & vbNewLine _
    '                                        & "      ,SOKO.INKA_INFO_YN                    AS INKA_INFO_YN             " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_SASHIZU_PRT_YN            AS OUTKA_SASHIZU_PRT_YN     " & vbNewLine _
    '                                        & "      ,SOKO.OUTOKA_KANRYO_YN                AS OUTOKA_KANRYO_YN         " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_KENPIN_YN                 AS OUTKA_KENPIN_YN          " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_INFO_YN                   AS OUTKA_INFO_YN            " & vbNewLine _
    '                                        & "      ,SOKO.LOC_MANAGER_YN                  AS LOC_MANAGER_YN           " & vbNewLine _
    '                                        & "      ,SOKO.TOU_KANRI_YN                    AS TOU_KANRI_YN             " & vbNewLine _
    '                                        & "      ,SOKO.TOUHAN_SASHIZU_YN               AS TOUHAN_SASHIZU_YN        " & vbNewLine _
    '                                        & "      ,SOKO.SYS_ENT_DATE                    AS SYS_ENT_DATE             " & vbNewLine _
    '                                        & "      ,USER1.USER_NM                        AS SYS_ENT_USER_NM          " & vbNewLine _
    '                                        & "      ,SOKO.SYS_UPD_DATE                    AS SYS_UPD_DATE             " & vbNewLine _
    '                                        & "      ,SOKO.SYS_UPD_TIME                    AS SYS_UPD_TIME             " & vbNewLine _
    '                                        & "      ,USER2.USER_NM                        AS SYS_UPD_USER_NM          " & vbNewLine _
    '                                        & "      ,SOKO.SYS_DEL_FLG                     AS SYS_DEL_FLG              " & vbNewLine _
    '                                        & "      ,KBN1.KBN_NM1                         AS SYS_DEL_NM               " & vbNewLine _
    '                                        & "      ,SOKO.OUTKA_YOTEI_YN                  AS OUTKA_YOTEI_YN           " & vbNewLine
    'END YANAI 要望番号394

    ''' <summary>
    ''' 倉庫マスタデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                 " & vbNewLine _
                                            & "       SOKO.NRS_BR_CD                       AS NRS_BR_CD                " & vbNewLine _
                                            & "      ,NRSBR.NRS_BR_NM                      AS NRS_BR_NM                " & vbNewLine _
                                            & "      ,SOKO.WH_CD                           AS WH_CD                    " & vbNewLine _
                                            & "      ,SOKO.WH_NM                           AS WH_NM                    " & vbNewLine _
                                            & "      ,SOKO.TEL                             AS TEL                      " & vbNewLine _
                                            & "      ,SOKO.FAX                             AS FAX                      " & vbNewLine _
                                            & "      ,SOKO.ZIP                             AS ZIP                      " & vbNewLine _
                                            & "      ,SOKO.AD_1                            AS AD_1                     " & vbNewLine _
                                            & "      ,SOKO.AD_2                            AS AD_2                     " & vbNewLine _
                                            & "      ,SOKO.AD_3                            AS AD_3                     " & vbNewLine _
                                            & "      ,SOKO.WH_KB                           AS WH_KB                    " & vbNewLine _
                                            & "      ,SOKO.JIS_CD                          AS JIS_CD                   " & vbNewLine _
                                            & "      ,JIS.KEN                              AS KEN                      " & vbNewLine _
                                            & "      ,JIS.SHI                              AS SHI                      " & vbNewLine _
                                            & "      ,SOKO.NIHUDA_MX_CNT                   AS NIHUDA_MX_CNT            " & vbNewLine _
                                            & "      ,SOKO.INKA_YOTEI_YN                   AS INKA_YOTEI_YN            " & vbNewLine _
                                            & "      ,SOKO.INKA_UKE_PRT_YN                 AS INKA_UKE_PRT_YN          " & vbNewLine _
                                            & "      ,SOKO.INKA_KENPIN_YN                  AS INKA_KENPIN_YN           " & vbNewLine _
                                            & "      ,SOKO.INKA_KAKUNIN_YN                 AS INKA_KAKUNIN_YN          " & vbNewLine _
                                            & "      ,SOKO.INKA_INFO_YN                    AS INKA_INFO_YN             " & vbNewLine _
                                            & "      ,SOKO.OUTKA_SASHIZU_PRT_YN            AS OUTKA_SASHIZU_PRT_YN     " & vbNewLine _
                                            & "      ,SOKO.OUTOKA_KANRYO_YN                AS OUTOKA_KANRYO_YN         " & vbNewLine _
                                            & "      ,SOKO.OUTKA_KENPIN_YN                 AS OUTKA_KENPIN_YN          " & vbNewLine _
                                            & "      ,SOKO.OUTKA_INFO_YN                   AS OUTKA_INFO_YN            " & vbNewLine _
                                            & "      ,SOKO.LOC_MANAGER_YN                  AS LOC_MANAGER_YN           " & vbNewLine _
                                            & "      ,SOKO.TOU_KANRI_YN                    AS TOU_KANRI_YN             " & vbNewLine _
                                            & "      ,SOKO.TOUHAN_SASHIZU_YN               AS TOUHAN_SASHIZU_YN        " & vbNewLine _
                                            & "      ,SOKO.SYS_ENT_DATE                    AS SYS_ENT_DATE             " & vbNewLine _
                                            & "      ,USER1.USER_NM                        AS SYS_ENT_USER_NM          " & vbNewLine _
                                            & "      ,SOKO.SYS_UPD_DATE                    AS SYS_UPD_DATE             " & vbNewLine _
                                            & "      ,SOKO.SYS_UPD_TIME                    AS SYS_UPD_TIME             " & vbNewLine _
                                            & "      ,USER2.USER_NM                        AS SYS_UPD_USER_NM          " & vbNewLine _
                                            & "      ,SOKO.SYS_DEL_FLG                     AS SYS_DEL_FLG              " & vbNewLine _
                                            & "      ,KBN1.KBN_NM1                         AS SYS_DEL_NM               " & vbNewLine _
                                            & "      ,SOKO.OUTKA_YOTEI_YN                  AS OUTKA_YOTEI_YN           " & vbNewLine _
                                            & "      ,SOKO.GOODSLOT_CHECK_YN               AS GOODSLOT_CHECK_YN        " & vbNewLine

    'END KIM 2012/09/12 要望番号1404 

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                 " & vbNewLine _
                                          & "                      $LM_MST$..M_SOKO AS SOKO                       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                   " & vbNewLine _
                                          & "        ON SOKO.NRS_BR_CD       = NRSBR.NRS_BR_CD                    " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_JIS     AS JIS                     " & vbNewLine _
                                          & "        ON SOKO.JIS_CD = JIS.JIS_CD                                  " & vbNewLine _
                                          & "       AND JIS.SYS_DEL_FLG      = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1                   " & vbNewLine _
                                          & "        ON SOKO.SYS_ENT_USER = USER1.USER_CD                         " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2                   " & vbNewLine _
                                          & "       ON  SOKO.SYS_UPD_USER  = USER2.USER_CD                        " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                    " & vbNewLine _
                                          & "        ON SOKO.SYS_DEL_FLG  = KBN1.KBN_CD                           " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD    = 'S051'                             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG     = '0'                                " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                            " & vbNewLine _
                                         & "      SOKO.WH_CD                                    " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_SOKO As String = "SELECT                                       " & vbNewLine _
                                            & "      COUNT(SOKO.WH_CD)  AS REC_CNT        " & vbNewLine _
                                            & " FROM $LM_MST$..M_SOKO AS SOKO             " & vbNewLine _
                                            & "WHERE WH_CD    = @WH_CD                    " & vbNewLine

    ''' <summary>
    ''' 郵便番号コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_ZIP As String = "SELECT                                        " & vbNewLine _
                                         & "   COUNT(ZIP_NO)  AS REC_CNT                  " & vbNewLine _
                                         & "FROM $LM_MST$..M_ZIP                          " & vbNewLine _
                                         & "WHERE      ZIP_NO  = @ZIP                     " & vbNewLine _
                                         & "AND   SYS_DEL_FLG  = '0'                      " & vbNewLine
    ''' <summary>
    ''' JISコード整合性チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JIS_CHK As String = "SELECT                                        " & vbNewLine _
                                        & "   JIS_CD  AS JIS_CD                          " & vbNewLine _
                                        & "FROM $LM_MST$..M_ZIP                          " & vbNewLine _
                                        & "WHERE      ZIP_NO  = @ZIP                     " & vbNewLine


#End Region

#End Region

#Region "設定処理 SQL"

    'START KIM 2012/09/12 要望番号1404 

    'START YANAI 要望番号394
    '''' <summary>
    '''' 新規登録SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SOKO                 " & vbNewLine _
    '                                   & "(                                            " & vbNewLine _
    '                                   & "       NRS_BR_CD                             " & vbNewLine _
    '                                   & "      ,WH_CD                                 " & vbNewLine _
    '                                   & "      ,WH_NM                                 " & vbNewLine _
    '                                   & "      ,ZIP                                   " & vbNewLine _
    '                                   & "      ,AD_1                                  " & vbNewLine _
    '                                   & "      ,AD_2                                  " & vbNewLine _
    '                                   & "      ,AD_3                                  " & vbNewLine _
    '                                   & "      ,WH_KB                                 " & vbNewLine _
    '                                   & "      ,TEL                                   " & vbNewLine _
    '                                   & "      ,FAX                                   " & vbNewLine _
    '                                   & "      ,JIS_CD                                " & vbNewLine _
    '                                   & "      ,NIHUDA_MX_CNT                         " & vbNewLine _
    '                                   & "      ,INKA_YOTEI_YN                         " & vbNewLine _
    '                                   & "      ,INKA_UKE_PRT_YN                       " & vbNewLine _
    '                                   & "      ,INKA_KENPIN_YN                        " & vbNewLine _
    '                                   & "      ,INKA_KAKUNIN_YN                       " & vbNewLine _
    '                                   & "      ,INKA_INFO_YN                          " & vbNewLine _
    '                                   & "      ,LOC_MANAGER_YN                        " & vbNewLine _
    '                                   & "      ,OUTKA_SASHIZU_PRT_YN                  " & vbNewLine _
    '                                   & "      ,OUTOKA_KANRYO_YN                      " & vbNewLine _
    '                                   & "      ,OUTKA_KENPIN_YN                       " & vbNewLine _
    '                                   & "      ,OUTKA_INFO_YN                         " & vbNewLine _
    '                                   & "      ,TOUHAN_SASHIZU_YN                     " & vbNewLine _
    '                                   & "      ,TOU_KANRI_YN                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_USER                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_USER                          " & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG                           " & vbNewLine _
    '                                   & "      ) VALUES (                             " & vbNewLine _
    '                                   & "       @NRS_BR_CD                            " & vbNewLine _
    '                                   & "      ,@WH_CD                                " & vbNewLine _
    '                                   & "      ,@WH_NM                                " & vbNewLine _
    '                                   & "      ,@ZIP                                  " & vbNewLine _
    '                                   & "      ,@AD_1                                 " & vbNewLine _
    '                                   & "      ,@AD_2                                 " & vbNewLine _
    '                                   & "      ,@AD_3                                 " & vbNewLine _
    '                                   & "      ,@WH_KB                                " & vbNewLine _
    '                                   & "      ,@TEL                                  " & vbNewLine _
    '                                   & "      ,@FAX                                  " & vbNewLine _
    '                                   & "      ,@JIS_CD                               " & vbNewLine _
    '                                   & "      ,@NIHUDA_MX_CNT                        " & vbNewLine _
    '                                   & "      ,@INKA_YOTEI_YN                        " & vbNewLine _
    '                                   & "      ,@INKA_UKE_PRT_YN                      " & vbNewLine _
    '                                   & "      ,@INKA_KENPIN_YN                       " & vbNewLine _
    '                                   & "      ,@INKA_KAKUNIN_YN                      " & vbNewLine _
    '                                   & "      ,@INKA_INFO_YN                         " & vbNewLine _
    '                                   & "      ,@LOC_MANAGER_YN                       " & vbNewLine _
    '                                   & "      ,@OUTKA_SASHIZU_PRT_YN                 " & vbNewLine _
    '                                   & "      ,@OUTOKA_KANRYO_YN                     " & vbNewLine _
    '                                   & "      ,@OUTKA_KENPIN_YN                      " & vbNewLine _
    '                                   & "      ,@OUTKA_INFO_YN                        " & vbNewLine _
    '                                   & "      ,@TOUHAN_SASHIZU_YN                    " & vbNewLine _
    '                                   & "      ,@TOU_KANRI_YN                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_PGID                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER                         " & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG                          " & vbNewLine _
    '                                   & ")                                            " & vbNewLine
    '''' <summary>
    '''' 新規登録SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SOKO                 " & vbNewLine _
    '                                   & "(                                            " & vbNewLine _
    '                                   & "       NRS_BR_CD                             " & vbNewLine _
    '                                   & "      ,WH_CD                                 " & vbNewLine _
    '                                   & "      ,WH_NM                                 " & vbNewLine _
    '                                   & "      ,ZIP                                   " & vbNewLine _
    '                                   & "      ,AD_1                                  " & vbNewLine _
    '                                   & "      ,AD_2                                  " & vbNewLine _
    '                                   & "      ,AD_3                                  " & vbNewLine _
    '                                   & "      ,WH_KB                                 " & vbNewLine _
    '                                   & "      ,TEL                                   " & vbNewLine _
    '                                   & "      ,FAX                                   " & vbNewLine _
    '                                   & "      ,JIS_CD                                " & vbNewLine _
    '                                   & "      ,NIHUDA_MX_CNT                         " & vbNewLine _
    '                                   & "      ,INKA_YOTEI_YN                         " & vbNewLine _
    '                                   & "      ,INKA_UKE_PRT_YN                       " & vbNewLine _
    '                                   & "      ,INKA_KENPIN_YN                        " & vbNewLine _
    '                                   & "      ,INKA_KAKUNIN_YN                       " & vbNewLine _
    '                                   & "      ,INKA_INFO_YN                          " & vbNewLine _
    '                                   & "      ,LOC_MANAGER_YN                        " & vbNewLine _
    '                                   & "      ,OUTKA_SASHIZU_PRT_YN                  " & vbNewLine _
    '                                   & "      ,OUTOKA_KANRYO_YN                      " & vbNewLine _
    '                                   & "      ,OUTKA_KENPIN_YN                       " & vbNewLine _
    '                                   & "      ,OUTKA_INFO_YN                         " & vbNewLine _
    '                                   & "      ,TOUHAN_SASHIZU_YN                     " & vbNewLine _
    '                                   & "      ,TOU_KANRI_YN                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID                          " & vbNewLine _
    '                                   & "      ,SYS_ENT_USER                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID                          " & vbNewLine _
    '                                   & "      ,SYS_UPD_USER                          " & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG                           " & vbNewLine _
    '                                   & "      ,OUTKA_YOTEI_YN                        " & vbNewLine _
    '                                   & "      ) VALUES (                             " & vbNewLine _
    '                                   & "       @NRS_BR_CD                            " & vbNewLine _
    '                                   & "      ,@WH_CD                                " & vbNewLine _
    '                                   & "      ,@WH_NM                                " & vbNewLine _
    '                                   & "      ,@ZIP                                  " & vbNewLine _
    '                                   & "      ,@AD_1                                 " & vbNewLine _
    '                                   & "      ,@AD_2                                 " & vbNewLine _
    '                                   & "      ,@AD_3                                 " & vbNewLine _
    '                                   & "      ,@WH_KB                                " & vbNewLine _
    '                                   & "      ,@TEL                                  " & vbNewLine _
    '                                   & "      ,@FAX                                  " & vbNewLine _
    '                                   & "      ,@JIS_CD                               " & vbNewLine _
    '                                   & "      ,@NIHUDA_MX_CNT                        " & vbNewLine _
    '                                   & "      ,@INKA_YOTEI_YN                        " & vbNewLine _
    '                                   & "      ,@INKA_UKE_PRT_YN                      " & vbNewLine _
    '                                   & "      ,@INKA_KENPIN_YN                       " & vbNewLine _
    '                                   & "      ,@INKA_KAKUNIN_YN                      " & vbNewLine _
    '                                   & "      ,@INKA_INFO_YN                         " & vbNewLine _
    '                                   & "      ,@LOC_MANAGER_YN                       " & vbNewLine _
    '                                   & "      ,@OUTKA_SASHIZU_PRT_YN                 " & vbNewLine _
    '                                   & "      ,@OUTOKA_KANRYO_YN                     " & vbNewLine _
    '                                   & "      ,@OUTKA_KENPIN_YN                      " & vbNewLine _
    '                                   & "      ,@OUTKA_INFO_YN                        " & vbNewLine _
    '                                   & "      ,@TOUHAN_SASHIZU_YN                    " & vbNewLine _
    '                                   & "      ,@TOU_KANRI_YN                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID                         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_PGID                         " & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER                         " & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG                          " & vbNewLine _
    '                                   & "      ,@OUTKA_YOTEI_YN                       " & vbNewLine _
    '                                   & ")                                            " & vbNewLine
    'END YANAI 要望番号394

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SOKO                 " & vbNewLine _
                                       & "(                                            " & vbNewLine _
                                       & "       NRS_BR_CD                             " & vbNewLine _
                                       & "      ,WH_CD                                 " & vbNewLine _
                                       & "      ,WH_NM                                 " & vbNewLine _
                                       & "      --2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start " & vbNewLine _
                                       & "      $LANG$                                 " & vbNewLine _
                                       & "      --2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end   " & vbNewLine _
                                       & "      ,ZIP                                   " & vbNewLine _
                                       & "      ,AD_1                                  " & vbNewLine _
                                       & "      ,AD_2                                  " & vbNewLine _
                                       & "      ,AD_3                                  " & vbNewLine _
                                       & "      ,WH_KB                                 " & vbNewLine _
                                       & "      ,TEL                                   " & vbNewLine _
                                       & "      ,FAX                                   " & vbNewLine _
                                       & "      ,JIS_CD                                " & vbNewLine _
                                       & "      ,NIHUDA_MX_CNT                         " & vbNewLine _
                                       & "      ,INKA_YOTEI_YN                         " & vbNewLine _
                                       & "      ,INKA_UKE_PRT_YN                       " & vbNewLine _
                                       & "      ,INKA_KENPIN_YN                        " & vbNewLine _
                                       & "      ,INKA_KAKUNIN_YN                       " & vbNewLine _
                                       & "      ,INKA_INFO_YN                          " & vbNewLine _
                                       & "      ,LOC_MANAGER_YN                        " & vbNewLine _
                                       & "      ,OUTKA_SASHIZU_PRT_YN                  " & vbNewLine _
                                       & "      ,OUTOKA_KANRYO_YN                      " & vbNewLine _
                                       & "      ,OUTKA_KENPIN_YN                       " & vbNewLine _
                                       & "      ,OUTKA_INFO_YN                         " & vbNewLine _
                                       & "      ,TOUHAN_SASHIZU_YN                     " & vbNewLine _
                                       & "      ,TOU_KANRI_YN                          " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                          " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                          " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                          " & vbNewLine _
                                       & "      ,SYS_ENT_USER                          " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                          " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                          " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                          " & vbNewLine _
                                       & "      ,SYS_UPD_USER                          " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                           " & vbNewLine _
                                       & "      ,OUTKA_YOTEI_YN                        " & vbNewLine _
                                       & "      ,GOODSLOT_CHECK_YN                     " & vbNewLine _
                                       & "      ) VALUES (                             " & vbNewLine _
                                       & "       @NRS_BR_CD                            " & vbNewLine _
                                       & "      ,@WH_CD                                " & vbNewLine _
                                       & "      ,@WH_NM                                " & vbNewLine _
                                       & "      --2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start " & vbNewLine _
                                       & "      $LANG_PARA$                            " & vbNewLine _
                                       & "      --2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end   " & vbNewLine _
                                       & "      ,@ZIP                                  " & vbNewLine _
                                       & "      ,@AD_1                                 " & vbNewLine _
                                       & "      ,@AD_2                                 " & vbNewLine _
                                       & "      ,@AD_3                                 " & vbNewLine _
                                       & "      ,@WH_KB                                " & vbNewLine _
                                       & "      ,@TEL                                  " & vbNewLine _
                                       & "      ,@FAX                                  " & vbNewLine _
                                       & "      ,@JIS_CD                               " & vbNewLine _
                                       & "      ,@NIHUDA_MX_CNT                        " & vbNewLine _
                                       & "      ,@INKA_YOTEI_YN                        " & vbNewLine _
                                       & "      ,@INKA_UKE_PRT_YN                      " & vbNewLine _
                                       & "      ,@INKA_KENPIN_YN                       " & vbNewLine _
                                       & "      ,@INKA_KAKUNIN_YN                      " & vbNewLine _
                                       & "      ,@INKA_INFO_YN                         " & vbNewLine _
                                       & "      ,@LOC_MANAGER_YN                       " & vbNewLine _
                                       & "      ,@OUTKA_SASHIZU_PRT_YN                 " & vbNewLine _
                                       & "      ,@OUTOKA_KANRYO_YN                     " & vbNewLine _
                                       & "      ,@OUTKA_KENPIN_YN                      " & vbNewLine _
                                       & "      ,@OUTKA_INFO_YN                        " & vbNewLine _
                                       & "      ,@TOUHAN_SASHIZU_YN                    " & vbNewLine _
                                       & "      ,@TOU_KANRI_YN                         " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                         " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                         " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                         " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                         " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                         " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                         " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                         " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                         " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                          " & vbNewLine _
                                       & "      ,@OUTKA_YOTEI_YN                       " & vbNewLine _
                                       & "      ,@GOODSLOT_CHECK_YN                    " & vbNewLine _
                                       & ")                                            " & vbNewLine

    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
    '日本語の場合のInsert追加項目
    Dim SQL_INSERT_LANG_PARTS_JP As String = String.Empty
    '日本語の場合の追加項目パラメータ
    Dim SQL_INSERT_LANG_PARTS_JP_PARA As String = String.Empty
    '英語の場合の追加項目
    Dim SQL_INSERT_LANG_PARTS_EN As String = ",WH_NM_EN"
    '英語の場合の追加項目パラメータ
    Dim SQL_INSERT_LANG_PARTS_EN_PARA As String = ",@WH_NM_EN"
    '韓国語の場合の追加項目
    Dim SQL_INSERT_LANG_PARTS_KO As String = ",WH_NM_KO"
    '韓国語の場合の追加項目パラメータ
    Dim SQL_INSERT_LANG_PARTS_KO_PARA As String = ",@WH_NM_KO"
    '中国語の場合の追加項目
    Dim SQL_INSERT_LANG_PARTS_CN As String = ",WH_NM_CN"
    '中国語の場合の追加項目パラメータ
    Dim SQL_INSERT_LANG_PARTS_CN_PARA As String = ",@WH_NM_CN"
    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end

    'END KIM 2012/09/12 要望番号1404 

    'START KIM 2012/09/12 要望番号1404 

    'START YANAI 要望番号394
    '''' <summary>
    '''' 更新SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SOKO SET                                 " & vbNewLine _
    '                                   & "        NRS_BR_CD              = @NRS_BR_CD                 " & vbNewLine _
    '                                   & "       ,WH_NM                  = @WH_NM                     " & vbNewLine _
    '                                   & "       ,ZIP                    = @ZIP                       " & vbNewLine _
    '                                   & "       ,AD_1                   = @AD_1                      " & vbNewLine _
    '                                   & "       ,AD_2                   = @AD_2                      " & vbNewLine _
    '                                   & "       ,AD_3                   = @AD_3                      " & vbNewLine _
    '                                   & "       ,WH_KB                  = @WH_KB                     " & vbNewLine _
    '                                   & "       ,TEL                    = @TEL                       " & vbNewLine _
    '                                   & "       ,FAX                    = @FAX                       " & vbNewLine _
    '                                   & "       ,JIS_CD                 = @JIS_CD                    " & vbNewLine _
    '                                   & "       ,NIHUDA_MX_CNT          = @NIHUDA_MX_CNT             " & vbNewLine _
    '                                   & "       ,INKA_YOTEI_YN          = @INKA_YOTEI_YN             " & vbNewLine _
    '                                   & "       ,INKA_UKE_PRT_YN        = @INKA_UKE_PRT_YN           " & vbNewLine _
    '                                   & "       ,INKA_KENPIN_YN         = @INKA_KENPIN_YN            " & vbNewLine _
    '                                   & "       ,INKA_KAKUNIN_YN        = @INKA_KAKUNIN_YN           " & vbNewLine _
    '                                   & "       ,INKA_INFO_YN           = @INKA_INFO_YN              " & vbNewLine _
    '                                   & "       ,LOC_MANAGER_YN         = @LOC_MANAGER_YN            " & vbNewLine _
    '                                   & "       ,OUTKA_SASHIZU_PRT_YN   = @OUTKA_SASHIZU_PRT_YN      " & vbNewLine _
    '                                   & "       ,OUTOKA_KANRYO_YN       = @OUTOKA_KANRYO_YN          " & vbNewLine _
    '                                   & "       ,OUTKA_KENPIN_YN        = @OUTKA_KENPIN_YN           " & vbNewLine _
    '                                   & "       ,OUTKA_INFO_YN          = @OUTKA_INFO_YN             " & vbNewLine _
    '                                   & "       ,TOUHAN_SASHIZU_YN      = @TOUHAN_SASHIZU_YN         " & vbNewLine _
    '                                   & "       ,TOU_KANRI_YN           = @TOU_KANRI_YN              " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE           = @SYS_UPD_DATE              " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME           = @SYS_UPD_TIME              " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID           = @SYS_UPD_PGID              " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER           = @SYS_UPD_USER              " & vbNewLine _
    '                                   & " WHERE                                                      " & vbNewLine _
    '                                   & "         WH_CD                 = @WH_CD                     " & vbNewLine
    '''' <summary>
    '''' 更新SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SOKO SET                                 " & vbNewLine _
    '                                   & "        NRS_BR_CD              = @NRS_BR_CD                 " & vbNewLine _
    '                                   & "       ,WH_NM                  = @WH_NM                     " & vbNewLine _
    '                                   & "       ,ZIP                    = @ZIP                       " & vbNewLine _
    '                                   & "       ,AD_1                   = @AD_1                      " & vbNewLine _
    '                                   & "       ,AD_2                   = @AD_2                      " & vbNewLine _
    '                                   & "       ,AD_3                   = @AD_3                      " & vbNewLine _
    '                                   & "       ,WH_KB                  = @WH_KB                     " & vbNewLine _
    '                                   & "       ,TEL                    = @TEL                       " & vbNewLine _
    '                                   & "       ,FAX                    = @FAX                       " & vbNewLine _
    '                                   & "       ,JIS_CD                 = @JIS_CD                    " & vbNewLine _
    '                                   & "       ,NIHUDA_MX_CNT          = @NIHUDA_MX_CNT             " & vbNewLine _
    '                                   & "       ,INKA_YOTEI_YN          = @INKA_YOTEI_YN             " & vbNewLine _
    '                                   & "       ,INKA_UKE_PRT_YN        = @INKA_UKE_PRT_YN           " & vbNewLine _
    '                                   & "       ,INKA_KENPIN_YN         = @INKA_KENPIN_YN            " & vbNewLine _
    '                                   & "       ,INKA_KAKUNIN_YN        = @INKA_KAKUNIN_YN           " & vbNewLine _
    '                                   & "       ,INKA_INFO_YN           = @INKA_INFO_YN              " & vbNewLine _
    '                                   & "       ,LOC_MANAGER_YN         = @LOC_MANAGER_YN            " & vbNewLine _
    '                                   & "       ,OUTKA_SASHIZU_PRT_YN   = @OUTKA_SASHIZU_PRT_YN      " & vbNewLine _
    '                                   & "       ,OUTOKA_KANRYO_YN       = @OUTOKA_KANRYO_YN          " & vbNewLine _
    '                                   & "       ,OUTKA_KENPIN_YN        = @OUTKA_KENPIN_YN           " & vbNewLine _
    '                                   & "       ,OUTKA_INFO_YN          = @OUTKA_INFO_YN             " & vbNewLine _
    '                                   & "       ,TOUHAN_SASHIZU_YN      = @TOUHAN_SASHIZU_YN         " & vbNewLine _
    '                                   & "       ,TOU_KANRI_YN           = @TOU_KANRI_YN              " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE           = @SYS_UPD_DATE              " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME           = @SYS_UPD_TIME              " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID           = @SYS_UPD_PGID              " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER           = @SYS_UPD_USER              " & vbNewLine _
    '                                   & "       ,OUTKA_YOTEI_YN         = @OUTKA_YOTEI_YN            " & vbNewLine _
    '                                   & " WHERE                                                      " & vbNewLine _
    '                                   & "         WH_CD                 = @WH_CD                     " & vbNewLine
    'END YANAI 要望番号394

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SOKO SET                                 " & vbNewLine _
                                       & "        NRS_BR_CD              = @NRS_BR_CD                 " & vbNewLine _
                                       & "       ,WH_NM                  = @WH_NM                     " & vbNewLine _
                                       & "      --2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start " & vbNewLine _
                                       & "      $LANG$                                                " & vbNewLine _
                                       & "      --2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end   " & vbNewLine _
                                       & "       ,ZIP                    = @ZIP                       " & vbNewLine _
                                       & "       ,AD_1                   = @AD_1                      " & vbNewLine _
                                       & "       ,AD_2                   = @AD_2                      " & vbNewLine _
                                       & "       ,AD_3                   = @AD_3                      " & vbNewLine _
                                       & "       ,WH_KB                  = @WH_KB                     " & vbNewLine _
                                       & "       ,TEL                    = @TEL                       " & vbNewLine _
                                       & "       ,FAX                    = @FAX                       " & vbNewLine _
                                       & "       ,JIS_CD                 = @JIS_CD                    " & vbNewLine _
                                       & "       ,NIHUDA_MX_CNT          = @NIHUDA_MX_CNT             " & vbNewLine _
                                       & "       ,INKA_YOTEI_YN          = @INKA_YOTEI_YN             " & vbNewLine _
                                       & "       ,INKA_UKE_PRT_YN        = @INKA_UKE_PRT_YN           " & vbNewLine _
                                       & "       ,INKA_KENPIN_YN         = @INKA_KENPIN_YN            " & vbNewLine _
                                       & "       ,INKA_KAKUNIN_YN        = @INKA_KAKUNIN_YN           " & vbNewLine _
                                       & "       ,INKA_INFO_YN           = @INKA_INFO_YN              " & vbNewLine _
                                       & "       ,LOC_MANAGER_YN         = @LOC_MANAGER_YN            " & vbNewLine _
                                       & "       ,OUTKA_SASHIZU_PRT_YN   = @OUTKA_SASHIZU_PRT_YN      " & vbNewLine _
                                       & "       ,OUTOKA_KANRYO_YN       = @OUTOKA_KANRYO_YN          " & vbNewLine _
                                       & "       ,OUTKA_KENPIN_YN        = @OUTKA_KENPIN_YN           " & vbNewLine _
                                       & "       ,OUTKA_INFO_YN          = @OUTKA_INFO_YN             " & vbNewLine _
                                       & "       ,TOUHAN_SASHIZU_YN      = @TOUHAN_SASHIZU_YN         " & vbNewLine _
                                       & "       ,TOU_KANRI_YN           = @TOU_KANRI_YN              " & vbNewLine _
                                       & "       ,SYS_UPD_DATE           = @SYS_UPD_DATE              " & vbNewLine _
                                       & "       ,SYS_UPD_TIME           = @SYS_UPD_TIME              " & vbNewLine _
                                       & "       ,SYS_UPD_PGID           = @SYS_UPD_PGID              " & vbNewLine _
                                       & "       ,SYS_UPD_USER           = @SYS_UPD_USER              " & vbNewLine _
                                       & "       ,OUTKA_YOTEI_YN         = @OUTKA_YOTEI_YN            " & vbNewLine _
                                       & "       ,GOODSLOT_CHECK_YN      = @GOODSLOT_CHECK_YN         " & vbNewLine _
                                       & " WHERE                                                      " & vbNewLine _
                                       & "         WH_CD                 = @WH_CD                     " & vbNewLine

    'END KIM 2012/09/12 要望番号1404 

    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
    '日本語の場合のInsert追加項目
    Dim SQL_UPDATE_LANG_PARTS_JP As String = String.Empty
    '英語の場合の追加項目
    Dim SQL_UPDATE_LANG_PARTS_EN As String = ",WH_NM_EN                    = @WH_NM_EN"
    '韓国語の場合の追加項目
    Dim SQL_UPDATE_LANG_PARTS_KO As String = ",WH_NM_KO                    = @WH_NM_KO"
    '中国語の場合の追加項目
    Dim SQL_UPDATE_LANG_PARTS_CN As String = ",WH_NM_CN                    = @WH_NM_CN"
    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_SOKO SET                           " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & "WHERE                WH_CD    = @WH_CD                " & vbNewLine

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
    ''' 倉庫マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM150DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM150DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 倉庫マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM150DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM150DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM150DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("WH_KB", "WH_KB")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("KEN", "KEN")
        map.Add("SHI", "SHI")
        map.Add("NIHUDA_MX_CNT", "NIHUDA_MX_CNT")
        map.Add("INKA_YOTEI_YN", "INKA_YOTEI_YN")
        map.Add("INKA_UKE_PRT_YN", "INKA_UKE_PRT_YN")
        map.Add("INKA_KENPIN_YN", "INKA_KENPIN_YN")
        map.Add("INKA_KAKUNIN_YN", "INKA_KAKUNIN_YN")
        map.Add("INKA_INFO_YN", "INKA_INFO_YN")
        map.Add("OUTKA_SASHIZU_PRT_YN", "OUTKA_SASHIZU_PRT_YN")
        map.Add("OUTOKA_KANRYO_YN", "OUTOKA_KANRYO_YN")
        map.Add("OUTKA_KENPIN_YN", "OUTKA_KENPIN_YN")
        map.Add("OUTKA_INFO_YN", "OUTKA_INFO_YN")
        map.Add("LOC_MANAGER_YN", "LOC_MANAGER_YN")
        map.Add("TOU_KANRI_YN", "TOU_KANRI_YN")
        map.Add("TOUHAN_SASHIZU_YN", "TOUHAN_SASHIZU_YN")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        'START YANAI 要望番号394
        map.Add("OUTKA_YOTEI_YN", "OUTKA_YOTEI_YN")
        'END YANAI 要望番号394
        'START KIM 2012/09/12 要望番号1404 
        map.Add("GOODSLOT_CHECK_YN", "GOODSLOT_CHECK_YN")
        'END KIM 2012/09/12 要望番号1404 

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM150OUT")

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
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SOKO.SYS_DEL_FLG = @SYS_DEL_FLG  OR SOKO.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SOKO.NRS_BR_CD = @NRS_BR_CD OR SOKO.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SOKO.WH_CD = @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TEL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SOKO.TEL LIKE @TEL")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("FAX").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SOKO.FAX LIKE @FAX")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 倉庫マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSokoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM150DAC.SQL_EXIT_SOKO)
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

        MyBase.Logger.WriteSQLLog("LMM150DAC", "SelectSokoM", cmd)

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
    ''' 倉庫マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistSokoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM150DAC.SQL_EXIT_SOKO, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "CheckExistSokoM", cmd)

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
    ''' 倉庫マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSokoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen upd start
        '実行するSQL文
        Dim sql As String = Me.SetSchemaNm(LMM150DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString())
        '言語区分によって追加する項目の置換設定
        Dim langConvertItem As String = String.Empty
        Dim langConvertItemParameter As String = String.Empty
        Call GetInsertLangSqlParts(langConvertItem, langConvertItemParameter)
        sql = sql.Replace("$LANG$", langConvertItem).Replace("$LANG_PARA$", langConvertItemParameter)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM150DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))
        '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen upd end

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "InsertSokoM", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSokoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen upd start
        '実行するSQL文
        Dim sql As String = Me.SetSchemaNm(String.Concat(LMM150DAC.SQL_UPDATE, _
                                                         LMM150DAC.SQL_COM_UPDATE_CONDITION), _
                                           Me._Row.Item("USER_BR_CD").ToString())
        '言語区分によって追加する項目の置換設定
        Dim langConvertItem As String = String.Empty
        Call GetUpdateLangSqlParts(langConvertItem)
        sql = sql.Replace("$LANG$", langConvertItem)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM150DAC.SQL_UPDATE _
        '                                                                             , LMM150DAC.SQL_COM_UPDATE_CONDITION) _
        '                                                                             , Me._Row.Item("USER_BR_CD").ToString()))
        '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen upd end

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "UpdateSokoM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
    ''' <summary>
    ''' 言語区分によるInsert用SQLパーツの取得
    ''' </summary>
    ''' <param name="sqlParts">SQLパーツ</param>
    ''' <param name="sqlPartsParameter">SQLパラメータパーツ</param>
    ''' <remarks></remarks>
    Private Sub GetInsertLangSqlParts(ByRef sqlParts As String, ByRef sqlPartsParameter As String)
        Dim langKbn As String = _Row.Item("LANG_KBN").ToString
        Select Case langKbn
            Case LANG_KBN_JA
                '言語区分が日本の場合
                sqlParts = SQL_INSERT_LANG_PARTS_JP
                sqlPartsParameter = SQL_INSERT_LANG_PARTS_JP_PARA
            Case LANG_KBN_EN
                '言語区分が英語の場合
                sqlParts = SQL_INSERT_LANG_PARTS_EN
                sqlPartsParameter = SQL_INSERT_LANG_PARTS_EN_PARA
            Case LANG_KBN_KO
                '言語区分が韓国語の場合
                sqlParts = SQL_INSERT_LANG_PARTS_KO
                sqlPartsParameter = SQL_INSERT_LANG_PARTS_KO_PARA
            Case LANG_KBN_CN
                '言語区分が中国語の場合
                sqlParts = SQL_INSERT_LANG_PARTS_CN
                sqlPartsParameter = SQL_INSERT_LANG_PARTS_CN_PARA
            Case Else
        End Select
    End Sub

    ''' <summary>
    ''' 言語区分によるUpdate用SQLパーツの取得
    ''' </summary>
    ''' <param name="sqlParts">SQLパーツ</param>
    ''' <remarks></remarks>
    Private Sub GetUpdateLangSqlParts(ByRef sqlParts As String)
        Dim langKbn As String = _Row.Item("LANG_KBN").ToString
        Select Case langKbn
            Case LANG_KBN_JA
                '言語区分が日本の場合
                sqlParts = SQL_UPDATE_LANG_PARTS_JP
            Case LANG_KBN_EN
                '言語区分が英語の場合
                sqlParts = SQL_UPDATE_LANG_PARTS_EN
            Case LANG_KBN_KO
                '言語区分が韓国語の場合
                sqlParts = SQL_UPDATE_LANG_PARTS_KO
            Case LANG_KBN_CN
                '言語区分が中国語の場合
                sqlParts = SQL_UPDATE_LANG_PARTS_CN
            Case Else
        End Select

    End Sub
    '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end

    ''' <summary>
    ''' 倉庫マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSokoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM150DAC.SQL_DELETE _
                                                                                     , LMM150DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "DeleteSokoM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

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

#Region "入力チェック"

    ''2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>郵便番号マスタ件数取得SQLの構築・発行</remarks>
    'Private Function CheckExistZipM(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMM150IN")

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM150DAC.SQL_EXIT_ZIP, Me._Row.Item("USER_BR_CD").ToString()))

    '    Dim reader As SqlDataReader = Nothing

    '    'SQLパラメータ初期化/設定
    '    Call Me.SetParamZipExistChk()

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM150DAC", "CheckExistZipM", cmd)

    '    'SQLの発行
    '    reader = MyBase.GetSelectResult(cmd)

    '    cmd.Parameters.Clear()

    '    '処理件数の設定
    '    reader.Read()
    '    MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
    '    reader.Close()

    '    Return ds

    'End Function
    ''2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' JISコード整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckJisWng(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM150IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM150DAC.SQL_JIS_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamZipExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM150DAC", "CheckJisWng", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JIS_CD", "JIS_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM150JIS")

        Return ds

    End Function

#End Region

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(倉庫マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))

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
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

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
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
            '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
            Dim languageKbun As String = .Item("LANG_KBN").ToString()
            Select Case languageKbun
                Case LANG_KBN_EN
                    '言語区分が英語なら倉庫名称英語に倉庫名称と同じ値を入れる
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_EN", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
                Case LANG_KBN_KO
                    '言語区分が韓国語なら倉庫名称韓国語に倉庫名称と同じ値を入れる
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_KO", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
                Case LANG_KBN_CN
                    '言語区分が中国語なら倉庫名称中国語語に倉庫名称と同じ値を入れる
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_CN", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
                Case Else
                    '日本語の場合、特に設定しない
            End Select
            ''言語区分が英語なら倉庫名称英語に倉庫名称と同じ値を、そうでなければ空白を設定する
            'If languageKbun.Equals(LANG_KBN_EN) Then
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_EN", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
            'Else
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_EN", String.Empty, DBDataType.NVARCHAR))
            'End If
            ''言語区分が韓国語なら倉庫名称韓国語に倉庫名称と同じ値を、そうでなければ空白を設定する
            'If languageKbun.Equals(LANG_KBN_KO) Then
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_KO", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
            'Else
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_KO", String.Empty, DBDataType.NVARCHAR))
            'End If
            ''言語区分が中国語なら倉庫名称中国語に倉庫名称と同じ値を、そうでなければ空白を設定する
            'If languageKbun.Equals(LANG_KBN_CN) Then
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_CN", .Item("WH_NM").ToString(), DBDataType.NVARCHAR))
            'Else
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM_CN", String.Empty, DBDataType.NVARCHAR))
            'End If
            '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", .Item("AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_2", .Item("AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_KB", .Item("WH_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", .Item("TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", .Item("FAX").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_MX_CNT", .Item("NIHUDA_MX_CNT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_YOTEI_YN", .Item("INKA_YOTEI_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_UKE_PRT_YN", .Item("INKA_UKE_PRT_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KENPIN_YN", .Item("INKA_KENPIN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KAKUNIN_YN", .Item("INKA_KAKUNIN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_INFO_YN", .Item("INKA_INFO_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOC_MANAGER_YN", .Item("LOC_MANAGER_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SASHIZU_PRT_YN", .Item("OUTKA_SASHIZU_PRT_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTOKA_KANRYO_YN", .Item("OUTOKA_KANRYO_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KENPIN_YN", .Item("OUTKA_KENPIN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_INFO_YN", .Item("OUTKA_INFO_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUHAN_SASHIZU_YN", .Item("TOUHAN_SASHIZU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_KANRI_YN", .Item("TOU_KANRI_YN").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号394
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_YOTEI_YN", .Item("OUTKA_YOTEI_YN").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号394
            'START KIM 2012/09/12 要望番号1404 
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODSLOT_CHECK_YN", .Item("GOODSLOT_CHECK_YN").ToString(), DBDataType.CHAR))
            'END KIM 2012/09/12 要望番号1404 

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
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
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
