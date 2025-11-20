' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブ
'  プログラムID     :  LMG000    : 共通SQL(請求鑑ヘッダテーブル参照)
'  作  成  者       :  [ohagi]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMG000DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG000DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "CONST"

#Region "保管料取込日チェック用"

    ''' <summary>
    ''' 最新の請求日を取得（保管料取込用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_SELECT_HOKAN_CHK_DATE As String = "SELECT G_HED.SKYU_DATE                                                  " & vbNewLine _
                                       & "  FROM (                                                                              " & vbNewLine _
                                       & "        SELECT                                                                        " & vbNewLine _
                                       & "               MAX(SKYU_DATE) AS SKYU_DATE                                            " & vbNewLine _
                                       & "              ,SEIQTO_CD      AS SEIQTO_CD                                            " & vbNewLine _
                                       & "              ,NRS_BR_CD      AS NRS_BR_CD                                            " & vbNewLine _
                                       & "              ,RB_FLG         AS RB_FLG                                               " & vbNewLine _
                                       & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                          " & vbNewLine _
                                       & "          FROM $LM_TRN$..G_KAGAMI_HED                                                 " & vbNewLine _
                                       & "         WHERE (                                                                      " & vbNewLine _
                                       & "                                         CRT_KB  = '00'                               " & vbNewLine _
                                       & "                        AND (          STATE_KB  = '01'  OR  STATE_KB    = '02' )     " & vbNewLine _
                                       & "                         OR (          STATE_KB  = '00' AND STORAGE_KB    = '01'  )   " & vbNewLine _
                                       & "                )                                                                     " & vbNewLine _
                                       & "            OR ( STATE_KB = '03' OR STATE_KB = '04' )                                 " & vbNewLine _
                                       & "      GROUP BY   SEIQTO_CD                                                            " & vbNewLine _
                                       & "               , NRS_BR_CD                                                            " & vbNewLine _
                                       & "               , RB_FLG                                                               " & vbNewLine _
                                       & "               , SYS_DEL_FLG                                                          " & vbNewLine _
                                       & "        ) G_HED                                                                       " & vbNewLine _
                                       & " WHERE SEIQTO_CD              = @SEIQTO_CD                                            " & vbNewLine _
                                       & "   AND NRS_BR_CD              = @NRS_BR_CD                                            " & vbNewLine _
                                       & "   AND RB_FLG                 = '00'                                                  " & vbNewLine _
                                       & "   AND SYS_DEL_FLG            = '0'                                                   "

#End Region

#Region "荷役料取込チェック用"

    ''' <summary>
    ''' 最新の請求日を取得（荷役料取込チェック用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_SELECT_NIYAKU_CHK_DATE As String = "SELECT G_HED.SKYU_DATE                                                 " & vbNewLine _
                                       & "  FROM (                                                                              " & vbNewLine _
                                       & "        SELECT                                                                        " & vbNewLine _
                                       & "               MAX(SKYU_DATE) AS SKYU_DATE                                            " & vbNewLine _
                                       & "              ,SEIQTO_CD      AS SEIQTO_CD                                            " & vbNewLine _
                                       & "              ,NRS_BR_CD      AS NRS_BR_CD                                            " & vbNewLine _
                                       & "              ,RB_FLG         AS RB_FLG                                               " & vbNewLine _
                                       & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                          " & vbNewLine _
                                       & "          FROM $LM_TRN$..G_KAGAMI_HED                                                 " & vbNewLine _
                                       & "         WHERE (                                                                      " & vbNewLine _
                                       & "                                         CRT_KB  = '00'                               " & vbNewLine _
                                       & "                        AND (          STATE_KB  = '01'  OR  STATE_KB    = '02' )     " & vbNewLine _
                                       & "                         OR (          STATE_KB  = '00' AND HANDLING_KB    = '01'  )  " & vbNewLine _
                                       & "                )                                                                     " & vbNewLine _
                                       & "            OR ( STATE_KB = '03' OR STATE_KB = '04' )                                 " & vbNewLine _
                                       & "      GROUP BY   SEIQTO_CD                                                            " & vbNewLine _
                                       & "               , NRS_BR_CD                                                            " & vbNewLine _
                                       & "               , RB_FLG                                                               " & vbNewLine _
                                       & "               , SYS_DEL_FLG                                                          " & vbNewLine _
                                       & "        ) G_HED                                                                       " & vbNewLine _
                                       & " WHERE SEIQTO_CD              = @SEIQTO_CD                                            " & vbNewLine _
                                       & "   AND NRS_BR_CD              = @NRS_BR_CD                                            " & vbNewLine _
                                       & "   AND RB_FLG                 = '00'                                                  " & vbNewLine _
                                       & "   AND SYS_DEL_FLG            = '0'                                                   "


#End Region

#Region "運賃取込チェック用"

    ''' <summary>
    ''' 最新の請求日を取得（運賃取込チェック用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_SELECT_UNCHIN_CHK_DATE As String = "SELECT G_HED.SKYU_DATE                                                 " & vbNewLine _
                                       & "  FROM (                                                                              " & vbNewLine _
                                       & "        SELECT                                                                        " & vbNewLine _
                                       & "               MAX(SKYU_DATE) AS SKYU_DATE                                            " & vbNewLine _
                                       & "              ,SEIQTO_CD      AS SEIQTO_CD                                            " & vbNewLine _
                                       & "              ,NRS_BR_CD      AS NRS_BR_CD                                            " & vbNewLine _
                                       & "              ,RB_FLG         AS RB_FLG                                               " & vbNewLine _
                                       & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                          " & vbNewLine _
                                       & "          FROM $LM_TRN$..G_KAGAMI_HED                                                 " & vbNewLine _
                                       & "         WHERE (                                                                      " & vbNewLine _
                                       & "                                         CRT_KB  = '00'                               " & vbNewLine _
                                       & "                        AND (          STATE_KB  = '01'  OR  STATE_KB    = '02' )     " & vbNewLine _
                                       & "                         OR (          STATE_KB  = '00' AND UNCHIN_KB    = '01'  )    " & vbNewLine _
                                       & "                )                                                                     " & vbNewLine _
                                       & "            OR ( STATE_KB = '03' OR STATE_KB = '04' )                                 " & vbNewLine _
                                       & "      GROUP BY   SEIQTO_CD                                                            " & vbNewLine _
                                       & "               , NRS_BR_CD                                                            " & vbNewLine _
                                       & "               , RB_FLG                                                               " & vbNewLine _
                                       & "               , SYS_DEL_FLG                                                          " & vbNewLine _
                                       & "        ) G_HED                                                                       " & vbNewLine _
                                       & " WHERE SEIQTO_CD              = @SEIQTO_CD                                            " & vbNewLine _
                                       & "   AND NRS_BR_CD              = @NRS_BR_CD                                            " & vbNewLine _
                                       & "   AND RB_FLG                 = '00'                                                  " & vbNewLine _
                                       & "   AND SYS_DEL_FLG            = '0'                                                   "


#End Region

#Region "作業料取込チェック用"

    ''' <summary>
    ''' 最新の請求日を取得（作業料取込チェック用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_SELECT_SAGYO_CHK_DATE As String = "SELECT G_HED.SKYU_DATE,G_HED.STATE_KB                                   " & vbNewLine _
                                       & "  FROM (                                                                              " & vbNewLine _
                                       & "        SELECT                                                                        " & vbNewLine _
                                       & "               MAX(SKYU_DATE) AS SKYU_DATE                                            " & vbNewLine _
                                       & "              ,SEIQTO_CD      AS SEIQTO_CD                                            " & vbNewLine _
                                       & "              ,NRS_BR_CD      AS NRS_BR_CD                                            " & vbNewLine _
                                       & "              ,RB_FLG         AS RB_FLG                                               " & vbNewLine _
                                       & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                          " & vbNewLine _
                                       & "              ,STATE_KB       AS STATE_KB                                             " & vbNewLine _
                                       & "          FROM $LM_TRN$..G_KAGAMI_HED                                                 " & vbNewLine _
                                       & "         WHERE (                                                                      " & vbNewLine _
                                       & "                                         CRT_KB  = '00'                               " & vbNewLine _
                                       & "                        AND (          STATE_KB  = '01'  OR  STATE_KB    = '02' )     " & vbNewLine _
                                       & "                         OR (          STATE_KB  = '00' AND SAGYO_KB    = '01'  )     " & vbNewLine _
                                       & "                )                                                                     " & vbNewLine _
                                       & "            OR ( STATE_KB = '03' OR STATE_KB = '04' )                                 " & vbNewLine _
                                       & "      GROUP BY   SEIQTO_CD                                                            " & vbNewLine _
                                       & "               , NRS_BR_CD                                                            " & vbNewLine _
                                       & "               , RB_FLG                                                               " & vbNewLine _
                                       & "               , SYS_DEL_FLG                                                          " & vbNewLine _
                                       & "               , STATE_KB                                                             " & vbNewLine _
                                       & "        ) G_HED                                                                       " & vbNewLine _
                                       & " WHERE SEIQTO_CD              = @SEIQTO_CD                                            " & vbNewLine _
                                       & "   AND NRS_BR_CD              = @NRS_BR_CD                                            " & vbNewLine _
                                       & "   AND RB_FLG                 = '00'                                                  " & vbNewLine _
                                       & "   AND SYS_DEL_FLG            = '0'                                                   "


#End Region

#Region "横持料取込チェック用"

    ''' <summary>
    ''' 最新の請求日を取得（横持料取込チェック用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SQL_SELECT_YOKOMOCHI_CHK_DATE As String = "SELECT G_HED.SKYU_DATE                                              " & vbNewLine _
                                       & "  FROM (                                                                              " & vbNewLine _
                                       & "        SELECT                                                                        " & vbNewLine _
                                       & "               MAX(SKYU_DATE) AS SKYU_DATE                                            " & vbNewLine _
                                       & "              ,SEIQTO_CD      AS SEIQTO_CD                                            " & vbNewLine _
                                       & "              ,NRS_BR_CD      AS NRS_BR_CD                                            " & vbNewLine _
                                       & "              ,RB_FLG         AS RB_FLG                                               " & vbNewLine _
                                       & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                          " & vbNewLine _
                                       & "          FROM $LM_TRN$..G_KAGAMI_HED                                                 " & vbNewLine _
                                       & "         WHERE (                                                                      " & vbNewLine _
                                       & "                                         CRT_KB  = '00'                               " & vbNewLine _
                                       & "                        AND (          STATE_KB  = '01'  OR  STATE_KB    = '02' )     " & vbNewLine _
                                       & "                         OR (          STATE_KB  = '00' AND YOKOMOCHI_KB    = '01'  ) " & vbNewLine _
                                       & "                )                                                                     " & vbNewLine _
                                       & "            OR ( STATE_KB = '03' OR STATE_KB = '04' )                                 " & vbNewLine _
                                       & "      GROUP BY   SEIQTO_CD                                                            " & vbNewLine _
                                       & "               , NRS_BR_CD                                                            " & vbNewLine _
                                       & "               , RB_FLG                                                               " & vbNewLine _
                                       & "               , SYS_DEL_FLG                                                          " & vbNewLine _
                                       & "        ) G_HED                                                                       " & vbNewLine _
                                       & " WHERE SEIQTO_CD              = @SEIQTO_CD                                            " & vbNewLine _
                                       & "   AND NRS_BR_CD              = @NRS_BR_CD                                            " & vbNewLine _
                                       & "   AND RB_FLG                 = '00'                                                  " & vbNewLine _
                                       & "   AND SYS_DEL_FLG            = '0'                                                   "


#End Region

#Region "経理取込日チェック用"

    'START YANAI 要望番号827
    '''' <summary>
    '''' 請求ヘッダ作成済みデータの取得
    '''' </summary>
    '''' <remarks>
    '''' パラメータ
    '''' 　@TARIFF_BUNRUI_KB：タリフ分類区分
    '''' 　       @SEIQTO_CD：請求先コード
    '''' </remarks>
    'Public Const SQL_SELECT_KEIRI_CHK_DATE As String = "SELECT G_HED.SKYU_DATE                                                                             " & vbNewLine _
    '                                           & "  FROM (                                                                                           " & vbNewLine _
    '                                           & "        SELECT                                                                                     " & vbNewLine _
    '                                           & "               MAX(SKYU_DATE) AS SKYU_DATE                                                         " & vbNewLine _
    '                                           & "              ,SEIQTO_CD      AS SEIQTO_CD                                                         " & vbNewLine _
    '                                           & "              ,RB_FLG         AS RB_FLG                                                            " & vbNewLine _
    '                                           & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                                       " & vbNewLine _
    '                                           & "          FROM $LM_TRN$..G_KAGAMI_HED                                                              " & vbNewLine _
    '                                           & "         WHERE @TARIFF_BUNRUI_KB <> '40'                                                           " & vbNewLine _
    '                                           & "           AND (                                                                                   " & vbNewLine _
    '                                           & "                              (                                                                    " & vbNewLine _
    '                                           & "                                                        CRT_KB  = '00'                             " & vbNewLine _
    '                                           & "                                       AND (          STATE_KB  = '01'  OR  STATE_KB       = '02' )" & vbNewLine _
    '                                           & "                                        OR (          STATE_KB  = '00' AND UNCHIN_KB       = '01' )" & vbNewLine _
    '                                           & "                               )                                                                   " & vbNewLine _
    '                                           & "                      OR ( STATE_KB = '03' OR STATE_KB = '04' )                                    " & vbNewLine _
    '                                           & "               )                                                                                   " & vbNewLine _
    '                                           & "      GROUP BY   SEIQTO_CD                                                                         " & vbNewLine _
    '                                           & "               , RB_FLG                                                                            " & vbNewLine _
    '                                           & "               , SYS_DEL_FLG                                                                       " & vbNewLine _
    '                                           & "        UNION ALL                                                                                  " & vbNewLine _
    '                                           & "        SELECT                                                                                     " & vbNewLine _
    '                                           & "               MAX(SKYU_DATE) AS SKYU_DATE                                                         " & vbNewLine _
    '                                           & "              ,SEIQTO_CD      AS SEIQTO_CD                                                         " & vbNewLine _
    '                                           & "              ,RB_FLG         AS RB_FLG                                                            " & vbNewLine _
    '                                           & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                                       " & vbNewLine _
    '                                           & "          FROM $LM_TRN$..G_KAGAMI_HED                                                              " & vbNewLine _
    '                                           & "         WHERE @TARIFF_BUNRUI_KB  = '40'                                                           " & vbNewLine _
    '                                           & "           AND (                                                                                   " & vbNewLine _
    '                                           & "                              (                                                                    " & vbNewLine _
    '                                           & "                                                        CRT_KB  = '00'                             " & vbNewLine _
    '                                           & "                                       AND (          STATE_KB  = '01'  OR     STATE_KB    = '02' )" & vbNewLine _
    '                                           & "                                        OR (          STATE_KB  = '00' AND YOKOMOCHI_KB    = '01' )" & vbNewLine _
    '                                           & "                               )                                                                   " & vbNewLine _
    '                                           & "                      OR ( STATE_KB = '03' OR STATE_KB = '04' )                                    " & vbNewLine _
    '                                           & "               )                                                                                   " & vbNewLine _
    '                                           & "      GROUP BY   SEIQTO_CD                                                                         " & vbNewLine _
    '                                           & "               , RB_FLG                                                                            " & vbNewLine _
    '                                           & "               , SYS_DEL_FLG                                                                       " & vbNewLine _
    '                                           & "        ) G_HED                                                                                    " & vbNewLine _
    '                                           & " WHERE SEIQTO_CD              = @SEIQTO_CD                                                         " & vbNewLine _
    '                                           & "   AND RB_FLG                 = '00'                                                               " & vbNewLine _
    '                                           & "   AND SYS_DEL_FLG            = '0'    "
    ''' <summary>
    ''' 請求ヘッダ作成済みデータの取得
    ''' </summary>
    ''' <remarks>
    ''' パラメータ
    ''' 　@TARIFF_BUNRUI_KB：タリフ分類区分
    ''' 　       @SEIQTO_CD：請求先コード
    ''' </remarks>
    Public Const SQL_SELECT_KEIRI_CHK_DATE As String = "SELECT G_HED.SKYU_DATE                                                                             " & vbNewLine _
                                               & "  FROM (                                                                                           " & vbNewLine _
                                               & "        SELECT                                                                                     " & vbNewLine _
                                               & "               MAX(SKYU_DATE) AS SKYU_DATE                                                         " & vbNewLine _
                                               & "              ,SEIQTO_CD      AS SEIQTO_CD                                                         " & vbNewLine _
                                               & "              ,RB_FLG         AS RB_FLG                                                            " & vbNewLine _
                                               & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                                       " & vbNewLine _
                                               & "              ,NRS_BR_CD      AS NRS_BR_CD                                                         " & vbNewLine _
                                               & "          FROM $LM_TRN$..G_KAGAMI_HED                                                              " & vbNewLine _
                                               & "         WHERE @TARIFF_BUNRUI_KB <> '40'                                                           " & vbNewLine _
                                               & "           AND (                                                                                   " & vbNewLine _
                                               & "                              (                                                                    " & vbNewLine _
                                               & "                                                        CRT_KB  = '00'                             " & vbNewLine _
                                               & "                                       AND (          STATE_KB  = '01'  OR  STATE_KB       = '02' )" & vbNewLine _
                                               & "                                        OR (          STATE_KB  = '00' AND UNCHIN_KB       = '01' )" & vbNewLine _
                                               & "                               )                                                                   " & vbNewLine _
                                               & "                      OR ( STATE_KB = '03' OR STATE_KB = '04' )                                    " & vbNewLine _
                                               & "               )                                                                                   " & vbNewLine _
                                               & "      GROUP BY   SEIQTO_CD                                                                         " & vbNewLine _
                                               & "               , RB_FLG                                                                            " & vbNewLine _
                                               & "               , SYS_DEL_FLG                                                                       " & vbNewLine _
                                               & "               , NRS_BR_CD                                                                         " & vbNewLine _
                                               & "        UNION ALL                                                                                  " & vbNewLine _
                                               & "        SELECT                                                                                     " & vbNewLine _
                                               & "               MAX(SKYU_DATE) AS SKYU_DATE                                                         " & vbNewLine _
                                               & "              ,SEIQTO_CD      AS SEIQTO_CD                                                         " & vbNewLine _
                                               & "              ,RB_FLG         AS RB_FLG                                                            " & vbNewLine _
                                               & "              ,SYS_DEL_FLG    AS SYS_DEL_FLG                                                       " & vbNewLine _
                                               & "              ,NRS_BR_CD      AS NRS_BR_CD                                                         " & vbNewLine _
                                               & "          FROM $LM_TRN$..G_KAGAMI_HED                                                              " & vbNewLine _
                                               & "         WHERE @TARIFF_BUNRUI_KB  = '40'                                                           " & vbNewLine _
                                               & "           AND (                                                                                   " & vbNewLine _
                                               & "                              (                                                                    " & vbNewLine _
                                               & "                                                        CRT_KB  = '00'                             " & vbNewLine _
                                               & "                                       AND (          STATE_KB  = '01'  OR     STATE_KB    = '02' )" & vbNewLine _
                                               & "                                        OR (          STATE_KB  = '00' AND YOKOMOCHI_KB    = '01' )" & vbNewLine _
                                               & "                               )                                                                   " & vbNewLine _
                                               & "                      OR ( STATE_KB = '03' OR STATE_KB = '04' )                                    " & vbNewLine _
                                               & "               )                                                                                   " & vbNewLine _
                                               & "      GROUP BY   SEIQTO_CD                                                                         " & vbNewLine _
                                               & "               , RB_FLG                                                                            " & vbNewLine _
                                               & "               , SYS_DEL_FLG                                                                       " & vbNewLine _
                                               & "               , NRS_BR_CD                                                                         " & vbNewLine _
                                               & "        ) G_HED                                                                                    " & vbNewLine _
                                               & " WHERE SEIQTO_CD              = @SEIQTO_CD                                                         " & vbNewLine _
                                               & "   AND NRS_BR_CD              = @NRS_BR_CD                                                         " & vbNewLine _
                                               & "   AND RB_FLG                 = '00'                                                               " & vbNewLine _
                                               & "   AND SYS_DEL_FLG            = '0'    "
    'END YANAI 要望番号827

    '要望番号:1045 terakawa 2013.03.28 Start
    Public Const SQL_SELECT_NEW_KURO_COUNT As String = "    SELECT                                                                                   " & vbNewLine _
                                               & "       COUNT(SKYU_DATE) AS SKYU_DATE_COUNT                                                         " & vbNewLine _
                                               & "  FROM $LM_TRN$..G_KAGAMI_HED                                                                     " & vbNewLine _
                                               & " WHERE STATE_KB        IN ('00','01','02')                                                         " & vbNewLine _
                                               & "   AND RB_FLG          =    '00'                                                                   " & vbNewLine _
                                               & "   AND SKYU_NO_RELATED <>   ''                                                                     " & vbNewLine _
                                               & "   AND SEIQTO_CD       =    @SEIQTO_CD                                                             " & vbNewLine _
                                               & "   AND SKYU_DATE       like @SKYU_MONTH                                                            " & vbNewLine _
                                               & "   AND SYS_DEL_FLG     =    '0'                                                                    " & vbNewLine


    Public Const SQL_SELECT_IN_SKYU_DATE As String = "    SELECT                                                                                     " & vbNewLine _
                                               & "       COUNT(SKYU_DATE)  AS SKYU_DATE_COUNT                                                        " & vbNewLine _
                                               & "  FROM $LM_TRN$..G_KAGAMI_HED                                                                      " & vbNewLine _
                                               & " WHERE STATE_KB        IN ('00','01','02')                                                         " & vbNewLine _
                                               & "   AND RB_FLG          =    '00'                                                                   " & vbNewLine _
                                               & "   AND SKYU_NO_RELATED <>   ''                                                                     " & vbNewLine _
                                               & "   AND SEIQTO_CD       =    @SEIQTO_CD                                                             " & vbNewLine _
                                               & "   AND SKYU_DATE       like @SKYU_MONTH                                                            " & vbNewLine _
                                               & "   AND SYS_DEL_FLG     =    '0'                                                                    " & vbNewLine _
                                               & "   AND UNCHIN_IMP_FROM_DATE  <=   @SKYU_DATE                                                       " & vbNewLine _
                                               & "   AND SKYU_DATE       >=   @SKYU_DATE                                                             " & vbNewLine


    Public Const SQL_SELECT_IN_SKYU_DATE_YOKO As String = "    SELECT                                                                                " & vbNewLine _
                                              & "       COUNT(SKYU_DATE)  AS SKYU_DATE_COUNT                                                         " & vbNewLine _
                                              & "  FROM $LM_TRN$..G_KAGAMI_HED                                                                       " & vbNewLine _
                                              & " WHERE STATE_KB        IN ('00','01','02')                                                          " & vbNewLine _
                                              & "   AND RB_FLG          =    '00'                                                                    " & vbNewLine _
                                              & "   AND SKYU_NO_RELATED <>   ''                                                                      " & vbNewLine _
                                              & "   AND SEIQTO_CD       =    @SEIQTO_CD                                                              " & vbNewLine _
                                              & "   AND SKYU_DATE       like @SKYU_MONTH                                                             " & vbNewLine _
                                              & "   AND SYS_DEL_FLG     =    '0'                                                                     " & vbNewLine _
                                              & "   AND YOKOMOCHI_IMP_FROM_DATE  <=   @SKYU_DATE                                                     " & vbNewLine _
                                              & "   AND SKYU_DATE       >=   @SKYU_DATE                                                              " & vbNewLine

    Public Const SQL_SELECT_IN_SKYU_DATE_SAGYO As String = "    SELECT                                                                               " & vbNewLine _
                                              & "       COUNT(SKYU_DATE)  AS SKYU_DATE_COUNT                                                         " & vbNewLine _
                                              & "  FROM $LM_TRN$..G_KAGAMI_HED                                                                       " & vbNewLine _
                                              & " WHERE STATE_KB        IN ('00','01','02')                                                          " & vbNewLine _
                                              & "   AND RB_FLG          =    '00'                                                                    " & vbNewLine _
                                              & "   AND SKYU_NO_RELATED <>   ''                                                                      " & vbNewLine _
                                              & "   AND SEIQTO_CD       =    @SEIQTO_CD                                                              " & vbNewLine _
                                              & "   AND SKYU_DATE       like @SKYU_MONTH                                                             " & vbNewLine _
                                              & "   AND SYS_DEL_FLG     =    '0'                                                                     " & vbNewLine _
                                              & "   AND SAGYO_IMP_FROM_DATE  <=   @SKYU_DATE                                                         " & vbNewLine _
                                              & "   AND SKYU_DATE       >=   @SKYU_DATE                                                              " & vbNewLine

    '要望番号:1045 terakawa 2013.03.28 End

#End Region

#Region "請求開始日取得 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_START_DATE As String = "SELECT                                              " & vbNewLine _
                                                 & "      MAX(HED.SKYU_DATE)   AS    SKYU_DATE_FROM      " & vbNewLine _
                                                 & "FROM                                                 " & vbNewLine _
                                                 & "    $LM_TRN$..G_KAGAMI_HED      HED                  " & vbNewLine _
                                                 & "WHERE                                                " & vbNewLine _
                                                 & "       HED.NRS_BR_CD       =    @NRS_BR_CD           " & vbNewLine _
                                                 & "AND    HED.SEIQTO_CD       =    @SEIQTO_CD           " & vbNewLine _
                                                 & "AND    HED.SKYU_DATE       <    @SKYU_DATE           " & vbNewLine _
                                                 & "AND    HED.STATE_KB        IN   ('03','04')          " & vbNewLine _
                                                 & "AND    HED.SYS_DEL_FLG     =    '0'                  " & vbNewLine

#End Region

#Region "確定処理 SQL"

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_UP_HED_KAKUTEI As String = "UPDATE                                                   " & vbNewLine _
                                            & "	   $LM_TRN$..G_KAGAMI_HED                                  " & vbNewLine _
                                            & "SET                                                         " & vbNewLine _
                                            & "         STATE_KB                = @STATE_KB                " & vbNewLine _
                                            & "        ,UNCHIN_IMP_FROM_DATE    = @UNCHIN_IMP_FROM_DATE    " & vbNewLine _
                                            & "        ,SAGYO_IMP_FROM_DATE     = @SAGYO_IMP_FROM_DATE     " & vbNewLine _
                                            & "        ,YOKOMOCHI_IMP_FROM_DATE = @YOKOMOCHI_IMP_FROM_DATE " & vbNewLine _
                                            & "        ,SYS_UPD_DATE            = @SYS_UPD_DATE            " & vbNewLine _
                                            & "        ,SYS_UPD_TIME            = @SYS_UPD_TIME            " & vbNewLine _
                                            & "        ,SYS_UPD_PGID            = @SYS_UPD_PGID            " & vbNewLine _
                                            & "        ,SYS_UPD_USER            = @SYS_UPD_USER            " & vbNewLine _
                                            & "WHERE                                                       " & vbNewLine _
                                            & "        SKYU_NO       = @SKYU_NO                            " & vbNewLine _
                                            & "AND     SYS_UPD_DATE  = @HAITA_DATE                         " & vbNewLine _
                                            & "AND     SYS_UPD_TIME  = @HAITA_TIME                         " & vbNewLine

#End Region

#Region "新規登録,確定,編集時"

    ''' <summary>
    ''' マスタ存在チェック(請求先マスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_CHK_SEIQTO_M As String = "SELECT                                 " & vbNewLine _
                                             & "    COUNT(SEIQTO_CD)	AS	SELECT_CNT " & vbNewLine _
                                             & "FROM                                   " & vbNewLine _
                                             & "    $LM_MST$..M_SEIQTO                 " & vbNewLine _
                                             & "WHERE                                  " & vbNewLine _
                                             & "    NRS_BR_CD   =    @NRS_BR_CD        " & vbNewLine _
                                             & "AND SEIQTO_CD   =    @SEIQTO_CD        " & vbNewLine _
                                             & "AND SYS_DEL_FLG =    '0'               " & vbNewLine

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

#Region "請求開始日取得処理"

    ''' <summary>
    ''' 請求開始日取得処理"
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求開始日取得処理"SQLの構築・発行</remarks>
    Friend Function GetInvFrom(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("START_DATE_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG000DAC.SQL_SELECT_START_DATE)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamGetInvFrom()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG000DAC", "GetInvFrom", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE_FROM", "SKYU_DATE_FROM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "START_DATE_OUT")

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(保管/荷役/請求開始日　共通)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetInvFrom()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

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

End Class
