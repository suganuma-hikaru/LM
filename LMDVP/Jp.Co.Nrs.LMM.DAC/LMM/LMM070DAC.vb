' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM070DAC : 割増運賃マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM070DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM070DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TYPE_DAIHYO As String = "00"

    ''' <summary>
    ''' データタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TYPE_MEISAI As String = "01"

#End Region

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' 都道府県名データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KEN As String = " SELECT                                                               " & vbNewLine _
                                           & "	         KEN                     AS KEN                             " & vbNewLine _
                                           & " FROM                                                                 " & vbNewLine _
                                           & "	(                                                                   " & vbNewLine _
                                           & "		SELECT MIN(JIS_CD) AS JIS_CD                                    " & vbNewLine _
                                           & "	     ,KEN                                                           " & vbNewLine _
                                           & "	     FROM $LM_MST$..M_JIS                                           " & vbNewLine _
                                           & "		 WHERE SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & "	     GROUP BY  KEN                                                  " & vbNewLine _
                                           & "	)                      AS JIS                                       " & vbNewLine _
                                           & " ORDER BY JIS_CD                                                      " & vbNewLine


    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(EXTCUNCHIN.EXTC_TARIFF_CD)		   AS SELECT_CNT   " & vbNewLine

    'START YANAI 要望番号377
    '''' <summary>
    '''' M_EXTC_UNCHINデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                                               " & vbNewLine _
    '                                        & "	      EXTCUNCHIN.NRS_BR_CD                         AS NRS_BR_CD                        " & vbNewLine _
    '                                        & "	     ,NRSBR.NRS_BR_NM                              AS NRS_BR_NM                        " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.EXTC_TARIFF_CD                    AS EXTC_TARIFF_CD                   " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.EXTC_TARIFF_REM                   AS EXTC_TARIFF_REM                  " & vbNewLine _
    '                                        & "	     ,JIS.KEN                                      AS KEN                              " & vbNewLine _
    '                                        & "	     ,JIS.SHI                                      AS SHI                              " & vbNewLine _
    '                                        & "      ,CASE WHEN EXTCUNCHIN.JIS_CD = '0000000' THEN KBN01.KBN_CD                        " & vbNewLine _
    '                                        & "            ELSE KBN02.KBN_CD                                                           " & vbNewLine _
    '                                        & "	     END                                           AS DATA_TYPE                        " & vbNewLine _
    '                                        & "      ,CASE WHEN EXTCUNCHIN.JIS_CD = '0000000' THEN KBN01.KBN_NM1                       " & vbNewLine _
    '                                        & "            ELSE KBN02.KBN_NM1                                                          " & vbNewLine _
    '                                        & "	     END                                           AS DATA_TYPE_NM                     " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.JIS_CD                            AS JIS_CD                           " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.WINT_KIKAN_FROM                   AS WINT_KIKAN_FROM                  " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.WINT_KIKAN_TO                     AS WINT_KIKAN_TO                    " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.WINT_EXTC_YN                      AS WINT_EXTC_YN                     " & vbNewLine _
    '                                        & "	     ,KBN1.KBN_NM1                                 AS WINT_EXTC_YN_NM                  " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.CITY_EXTC_YN                      AS CITY_EXTC_YN                     " & vbNewLine _
    '                                        & "	     ,KBN2.KBN_NM1                                 AS CITY_EXTC_YN_NM                  " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.RELY_EXTC_YN                      AS RELY_EXTC_YN                     " & vbNewLine _
    '                                        & "	     ,KBN3.KBN_NM1                                 AS RELY_EXTC_YN_NM                  " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.FRRY_EXTC_YN                      AS FRRY_EXTC_YN                     " & vbNewLine _
    '                                        & "	     ,KBN4.KBN_NM1                                 AS FRRY_EXTC_YN_NM                  " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.SYS_ENT_DATE                      AS SYS_ENT_DATE                     " & vbNewLine _
    '                                        & "	     ,USER1.USER_NM                                AS SYS_ENT_USER_NM	               " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.SYS_UPD_DATE                      AS SYS_UPD_DATE                     " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.SYS_UPD_TIME                      AS SYS_UPD_TIME                     " & vbNewLine _
    '                                        & "	     ,USER2.USER_NM                                AS SYS_UPD_USER_NM                  " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN.SYS_DEL_FLG                       AS SYS_DEL_FLG                      " & vbNewLine _
    '                                        & "	     ,KBN5.KBN_NM1                                 AS SYS_DEL_NM                       " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN2.SYS_UPD_DATE                     AS OYA_DATE                         " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN2.SYS_UPD_TIME                     AS OYA_TIME                         " & vbNewLine _
    '                                        & "	     ,EXTCUNCHIN2.SYS_DEL_FLG                      AS OYA_SYS_DEL_FLG                  " & vbNewLine
    ''' <summary>
    ''' M_EXTC_UNCHINデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                                               " & vbNewLine _
                                            & "	      EXTCUNCHIN.NRS_BR_CD                         AS NRS_BR_CD                        " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                              AS NRS_BR_NM                        " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.EXTC_TARIFF_CD                    AS EXTC_TARIFF_CD                   " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.EXTC_TARIFF_REM                   AS EXTC_TARIFF_REM                  " & vbNewLine _
                                            & "	     ,JIS.KEN                                      AS KEN                              " & vbNewLine _
                                            & "	     ,JIS.SHI                                      AS SHI                              " & vbNewLine _
                                            & "      ,CASE WHEN EXTCUNCHIN.JIS_CD = '0000000' THEN KBN01.KBN_CD                        " & vbNewLine _
                                            & "            ELSE KBN02.KBN_CD                                                           " & vbNewLine _
                                            & "	     END                                           AS DATA_TYPE                        " & vbNewLine _
                                            & "      ,CASE WHEN EXTCUNCHIN.JIS_CD = '0000000' THEN KBN01.KBN_NM1                       " & vbNewLine _
                                            & "            ELSE KBN02.KBN_NM1                                                          " & vbNewLine _
                                            & "	     END                                           AS DATA_TYPE_NM                     " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.JIS_CD                            AS JIS_CD                           " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.WINT_KIKAN_FROM                   AS WINT_KIKAN_FROM                  " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.WINT_KIKAN_TO                     AS WINT_KIKAN_TO                    " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.WINT_EXTC_YN                      AS WINT_EXTC_YN                     " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                 AS WINT_EXTC_YN_NM                  " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.CITY_EXTC_YN                      AS CITY_EXTC_YN                     " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                 AS CITY_EXTC_YN_NM                  " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.RELY_EXTC_YN                      AS RELY_EXTC_YN                     " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                                 AS RELY_EXTC_YN_NM                  " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.FRRY_EXTC_YN                      AS FRRY_EXTC_YN                     " & vbNewLine _
                                            & "	     ,KBN4.KBN_NM1                                 AS FRRY_EXTC_YN_NM                  " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.FRRY_EXTC_10KG                    AS FRRY_EXTC_10KG                   " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.SYS_ENT_DATE                      AS SYS_ENT_DATE                     " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                AS SYS_ENT_USER_NM	               " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.SYS_UPD_DATE                      AS SYS_UPD_DATE                     " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.SYS_UPD_TIME                      AS SYS_UPD_TIME                     " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                AS SYS_UPD_USER_NM                  " & vbNewLine _
                                            & "	     ,EXTCUNCHIN.SYS_DEL_FLG                       AS SYS_DEL_FLG                      " & vbNewLine _
                                            & "	     ,KBN5.KBN_NM1                                 AS SYS_DEL_NM                       " & vbNewLine _
                                            & "	     ,EXTCUNCHIN2.SYS_UPD_DATE                     AS OYA_DATE                         " & vbNewLine _
                                            & "	     ,EXTCUNCHIN2.SYS_UPD_TIME                     AS OYA_TIME                         " & vbNewLine _
                                            & "	     ,EXTCUNCHIN2.SYS_DEL_FLG                      AS OYA_SYS_DEL_FLG                  " & vbNewLine
    'END YANAI 要望番号377

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                                                                             " & vbNewLine _
                                          & "                      $LM_MST$..M_EXTC_UNCHIN AS EXTCUNCHIN                          " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR          AS NRSBR                           " & vbNewLine _
                                          & "        ON EXTCUNCHIN.NRS_BR_CD                    = NRSBR.NRS_BR_CD                 " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN01                           " & vbNewLine _
                                          & "        ON KBN01.KBN_CD                            = '00'                            " & vbNewLine _
                                          & "       AND KBN01.KBN_GROUP_CD                      = 'D008'                          " & vbNewLine _
                                          & "       AND KBN01.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN02                           " & vbNewLine _
                                          & "        ON KBN02.KBN_CD                            = '01'                            " & vbNewLine _
                                          & "       AND KBN02.KBN_GROUP_CD                      = 'D008'                          " & vbNewLine _
                                          & "       AND KBN02.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_JIS             AS JIS                             " & vbNewLine _
                                          & "        ON EXTCUNCHIN.JIS_CD                       = JIS.JIS_CD                      " & vbNewLine _
                                          & "       AND JIS.SYS_DEL_FLG                         = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN1                            " & vbNewLine _
                                          & "        ON EXTCUNCHIN.WINT_EXTC_YN                 = KBN1.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD                       = 'W002'                          " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN2                            " & vbNewLine _
                                          & "        ON EXTCUNCHIN.CITY_EXTC_YN                 = KBN2.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD                       = 'W003'                          " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN3                            " & vbNewLine _
                                          & "        ON EXTCUNCHIN.RELY_EXTC_YN                 = KBN3.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD                       = 'W004'                          " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN4                            " & vbNewLine _
                                          & "        ON EXTCUNCHIN.FRRY_EXTC_YN                 = KBN4.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD                       = 'W005'                          " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER            AS USER1                           " & vbNewLine _
                                          & "        ON EXTCUNCHIN.SYS_ENT_USER                 = USER1.USER_CD                   " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER            AS USER2                           " & vbNewLine _
                                          & "        ON EXTCUNCHIN.SYS_UPD_USER                 = USER2.USER_CD                   " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN             AS KBN5                            " & vbNewLine _
                                          & "        ON EXTCUNCHIN.SYS_DEL_FLG                  = KBN5.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD                       = 'S051'                          " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_EXTC_UNCHIN     AS EXTCUNCHIN2                     " & vbNewLine _
                                          & "        ON EXTCUNCHIN.NRS_BR_CD                    = EXTCUNCHIN2.NRS_BR_CD           " & vbNewLine _
                                          & "       AND EXTCUNCHIN.EXTC_TARIFF_CD               = EXTCUNCHIN2.EXTC_TARIFF_CD      " & vbNewLine _
                                          & "       AND EXTCUNCHIN2.JIS_CD                      = '0000000'                       " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                                " & vbNewLine _
                                         & "       EXTCUNCHIN.JIS_CD                                                " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 割増運賃コード存在チェック/排他用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_WARIMASHI As String = "SELECT                                                        " & vbNewLine _
                                            & "   COUNT(EXTC_TARIFF_CD)            AS REC_CNT                    " & vbNewLine _
                                            & "  FROM $LM_MST$..M_EXTC_UNCHIN                                    " & vbNewLine _
                                            & "  WHERE NRS_BR_CD              =    @NRS_BR_CD                    " & vbNewLine _
                                            & "  AND   EXTC_TARIFF_CD         =    @EXTC_TARIFF_CD               " & vbNewLine _
                                            & "  AND   JIS_CD                 =    @JIS_CD                       " & vbNewLine


    ''' <summary>
    ''' 割増運賃コード存在チェック(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_DELETE As String = "SELECT                                                           " & vbNewLine _
                                            & "   COUNT(EXTC_TARIFF_CD)            AS REC_CNT                    " & vbNewLine _
                                            & "  FROM $LM_MST$..M_EXTC_UNCHIN                                    " & vbNewLine _
                                            & "  WHERE NRS_BR_CD              =    @NRS_BR_CD                    " & vbNewLine _
                                            & "  AND   EXTC_TARIFF_CD         =    @EXTC_TARIFF_CD               " & vbNewLine _
                                            & "  AND   JIS_CD                 <>   @JIS_CD                       " & vbNewLine _
                                            & "  AND   SYS_DEL_FLG            =    '0'                           " & vbNewLine




#End Region

#End Region

#Region "設定処理 SQL"

    'START YANAI 要望番号377
    '''' <summary>
    '''' 新規登録SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_EXTC_UNCHIN       " & vbNewLine _
    '                                   & "(                                         " & vbNewLine _
    '                                   & "       NRS_BR_CD                          " & vbNewLine _
    '                                   & "      ,EXTC_TARIFF_CD                     " & vbNewLine _
    '                                   & "      ,EXTC_TARIFF_REM                    " & vbNewLine _
    '                                   & "      ,JIS_CD                             " & vbNewLine _
    '                                   & "      ,WINT_KIKAN_FROM                    " & vbNewLine _
    '                                   & "      ,WINT_KIKAN_TO                      " & vbNewLine _
    '                                   & "      ,WINT_EXTC_YN                       " & vbNewLine _
    '                                   & "      ,CITY_EXTC_YN                       " & vbNewLine _
    '                                   & "      ,RELY_EXTC_YN                       " & vbNewLine _
    '                                   & "      ,FRRY_EXTC_YN                       " & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE                       " & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME                       " & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID                       " & vbNewLine _
    '                                   & "      ,SYS_ENT_USER                       " & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE         	            " & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME                       " & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID                       " & vbNewLine _
    '                                   & "      ,SYS_UPD_USER                       " & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG                        " & vbNewLine _
    '                                   & "      ) VALUES (                          " & vbNewLine _
    '                                   & "       @NRS_BR_CD                         " & vbNewLine _
    '                                   & "      ,@EXTC_TARIFF_CD                    " & vbNewLine _
    '                                   & "      ,@EXTC_TARIFF_REM                   " & vbNewLine _
    '                                   & "      ,@JIS_CD                            " & vbNewLine _
    '                                   & "      ,@WINT_KIKAN_FROM                   " & vbNewLine _
    '                                   & "      ,@WINT_KIKAN_TO                     " & vbNewLine _
    '                                   & "      ,@WINT_EXTC_YN                      " & vbNewLine _
    '                                   & "      ,@CITY_EXTC_YN                      " & vbNewLine _
    '                                   & "      ,@RELY_EXTC_YN                      " & vbNewLine _
    '                                   & "      ,@FRRY_EXTC_YN                      " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE         	            " & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME                 	    " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID                	    " & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER                      " & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE                 	    " & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME                      " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID         	            " & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER                      " & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG                 	    " & vbNewLine _
    '                                   & ")                                         " & vbNewLine
    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_EXTC_UNCHIN       " & vbNewLine _
                                       & "(                                         " & vbNewLine _
                                       & "       NRS_BR_CD                          " & vbNewLine _
                                       & "      ,EXTC_TARIFF_CD                     " & vbNewLine _
                                       & "      ,EXTC_TARIFF_REM                    " & vbNewLine _
                                       & "      ,JIS_CD                             " & vbNewLine _
                                       & "      ,WINT_KIKAN_FROM                    " & vbNewLine _
                                       & "      ,WINT_KIKAN_TO                      " & vbNewLine _
                                       & "      ,WINT_EXTC_YN                       " & vbNewLine _
                                       & "      ,CITY_EXTC_YN                       " & vbNewLine _
                                       & "      ,RELY_EXTC_YN                       " & vbNewLine _
                                       & "      ,FRRY_EXTC_YN                       " & vbNewLine _
                                       & "      ,FRRY_EXTC_10KG                     " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                       " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                       " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                       " & vbNewLine _
                                       & "      ,SYS_ENT_USER                       " & vbNewLine _
                                       & "      ,SYS_UPD_DATE         	            " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                       " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                       " & vbNewLine _
                                       & "      ,SYS_UPD_USER                       " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                        " & vbNewLine _
                                       & "      ) VALUES (                          " & vbNewLine _
                                       & "       @NRS_BR_CD                         " & vbNewLine _
                                       & "      ,@EXTC_TARIFF_CD                    " & vbNewLine _
                                       & "      ,@EXTC_TARIFF_REM                   " & vbNewLine _
                                       & "      ,@JIS_CD                            " & vbNewLine _
                                       & "      ,@WINT_KIKAN_FROM                   " & vbNewLine _
                                       & "      ,@WINT_KIKAN_TO                     " & vbNewLine _
                                       & "      ,@WINT_EXTC_YN                      " & vbNewLine _
                                       & "      ,@CITY_EXTC_YN                      " & vbNewLine _
                                       & "      ,@RELY_EXTC_YN                      " & vbNewLine _
                                       & "      ,@FRRY_EXTC_YN                      " & vbNewLine _
                                       & "      ,@FRRY_EXTC_10KG                    " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         	            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                 	    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                	    " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                      " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                 	    " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                      " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         	            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                      " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                 	    " & vbNewLine _
                                       & ")                                         " & vbNewLine
    'END YANAI 要望番号377

    'START YANAI 要望番号377
    '''' <summary>
    '''' 更新SQL
    '''' </summary>
    '''' <remarks></remarks>     
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_EXTC_UNCHIN SET                          " & vbNewLine _
    '                                   & "        EXTC_TARIFF_REM       = @EXTC_TARIFF_REM            " & vbNewLine _
    '                                   & "       ,WINT_KIKAN_FROM       = @WINT_KIKAN_FROM            " & vbNewLine _
    '                                   & "       ,WINT_KIKAN_TO         = @WINT_KIKAN_TO              " & vbNewLine _
    '                                   & "       ,WINT_EXTC_YN          = @WINT_EXTC_YN               " & vbNewLine _
    '                                   & "       ,CITY_EXTC_YN          = @CITY_EXTC_YN               " & vbNewLine _
    '                                   & "       ,RELY_EXTC_YN          = @RELY_EXTC_YN               " & vbNewLine _
    '                                   & "       ,FRRY_EXTC_YN          = @FRRY_EXTC_YN               " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE               " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME               " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID               " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER          = @SYS_UPD_USER               " & vbNewLine _
    '                                   & " WHERE                                                      " & vbNewLine _
    '                                   & "        NRS_BR_CD             = @NRS_BR_CD                  " & vbNewLine _
    '                                   & " AND    EXTC_TARIFF_CD        = @EXTC_TARIFF_CD             " & vbNewLine _
    '                                   & " AND    JIS_CD                = @JIS_CD                     " & vbNewLine
    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>     
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_EXTC_UNCHIN SET                          " & vbNewLine _
                                       & "        EXTC_TARIFF_REM       = @EXTC_TARIFF_REM            " & vbNewLine _
                                       & "       ,WINT_KIKAN_FROM       = @WINT_KIKAN_FROM            " & vbNewLine _
                                       & "       ,WINT_KIKAN_TO         = @WINT_KIKAN_TO              " & vbNewLine _
                                       & "       ,WINT_EXTC_YN          = @WINT_EXTC_YN               " & vbNewLine _
                                       & "       ,CITY_EXTC_YN          = @CITY_EXTC_YN               " & vbNewLine _
                                       & "       ,RELY_EXTC_YN          = @RELY_EXTC_YN               " & vbNewLine _
                                       & "       ,FRRY_EXTC_YN          = @FRRY_EXTC_YN               " & vbNewLine _
                                       & "       ,FRRY_EXTC_10KG        = @FRRY_EXTC_10KG             " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE               " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME               " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID               " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER               " & vbNewLine _
                                       & " WHERE                                                      " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD                  " & vbNewLine _
                                       & " AND    EXTC_TARIFF_CD        = @EXTC_TARIFF_CD             " & vbNewLine _
                                       & " AND    JIS_CD                = @JIS_CD                     " & vbNewLine
    'END YANAI 要望番号377
    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_EXTC_UNCHIN SET              " & vbNewLine _
                                       & "        SYS_UPD_DATE    = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME    = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID    = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER    = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG     = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                          " & vbNewLine _
                                       & "        NRS_BR_CD       = @NRS_BR_CD            " & vbNewLine _
                                       & " AND    EXTC_TARIFF_CD  = @EXTC_TARIFF_CD       " & vbNewLine _
                                       & " AND    JIS_CD          = @JIS_CD               " & vbNewLine

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
    ''' 都道府県名データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>都道府県名データ検索取得SQLの構築・発行</remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM070DAC.SQL_SELECT_KEN)      'SQL構築(都道府県名抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectKenData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("KEN", "KEN")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM070KEN")

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM070DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM070DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function


    ''' <summary>
    ''' 割増運賃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM070DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM070DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM070DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        map.Add("KEN", "KEN")
        map.Add("SHI", "SHI")
        map.Add("DATA_TYPE", "DATA_TYPE")
        map.Add("DATA_TYPE_NM", "DATA_TYPE_NM")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("WINT_KIKAN_FROM", "WINT_KIKAN_FROM")
        map.Add("WINT_KIKAN_TO", "WINT_KIKAN_TO")
        map.Add("WINT_EXTC_YN", "WINT_EXTC_YN")
        map.Add("WINT_EXTC_YN_NM", "WINT_EXTC_YN_NM")
        map.Add("CITY_EXTC_YN", "CITY_EXTC_YN")
        map.Add("CITY_EXTC_YN_NM", "CITY_EXTC_YN_NM")
        map.Add("RELY_EXTC_YN", "RELY_EXTC_YN")
        map.Add("RELY_EXTC_YN_NM", "RELY_EXTC_YN_NM")
        map.Add("FRRY_EXTC_YN", "FRRY_EXTC_YN")
        map.Add("FRRY_EXTC_YN_NM", "FRRY_EXTC_YN_NM")
        'START YANAI 要望番号377
        map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")
        'END YANAI 要望番号377
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("OYA_DATE", "OYA_DATE")
        map.Add("OYA_TIME", "OYA_TIME")
        map.Add("OYA_SYS_DEL_FLG", "OYA_SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM070OUT")

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
                andstr.Append(" (EXTCUNCHIN.SYS_DEL_FLG = @SYS_DEL_FLG  OR EXTCUNCHIN.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (EXTCUNCHIN.NRS_BR_CD = @NRS_BR_CD  OR EXTCUNCHIN.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("EXTC_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" EXTCUNCHIN.EXTC_TARIFF_CD LIKE @EXTC_TARIFF_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("EXTC_TARIFF_REM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" EXTCUNCHIN.EXTC_TARIFF_REM LIKE @EXTC_TARIFF_REM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_REM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("KEN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (JIS.KEN = @KEN OR JIS.KEN IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SHI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" JIS.SHI LIKE @SHI")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHI", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If


            whereStr = .Item("DATA_TYPE_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append("AND")
                If LMM070DAC.TYPE_DAIHYO.Equals(whereStr) = True Then
                    andstr.Append(" EXTCUNCHIN.JIS_CD = 0000000")
                    andstr.Append(vbNewLine)
                ElseIf LMM070DAC.TYPE_MEISAI.Equals(whereStr) = True Then
                    andstr.Append(" EXTCUNCHIN.JIS_CD <> 0000000")
                    andstr.Append(vbNewLine)
                Else
                End If
            End If

            whereStr = .Item("JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" EXTCUNCHIN.JIS_CD LIKE @JIS_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("WINT_EXTC_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (EXTCUNCHIN.WINT_EXTC_YN = @WINT_EXTC_YN  OR EXTCUNCHIN.WINT_EXTC_YN IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WINT_EXTC_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CITY_EXTC_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (EXTCUNCHIN.CITY_EXTC_YN = @CITY_EXTC_YN  OR EXTCUNCHIN.CITY_EXTC_YN IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_EXTC_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("RELY_EXTC_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (EXTCUNCHIN.RELY_EXTC_YN = @RELY_EXTC_YN  OR EXTCUNCHIN.RELY_EXTC_YN IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RELY_EXTC_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("FRRY_EXTC_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (EXTCUNCHIN.FRRY_EXTC_YN = @FRRY_EXTC_YN  OR EXTCUNCHIN.FRRY_EXTC_YN IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_YN", whereStr, DBDataType.CHAR))
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
    ''' 割増運賃マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistwarimashiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM070DAC.SQL_EXIST_WARIMASHI, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "CheckExistwarimashiM", cmd)

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
    ''' 割増運賃マスタ存在チェック(削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistDeleteM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM070DAC.SQL_EXIST_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "CheckExistDeleteM", cmd)

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
    ''' 割増運賃マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectwarimashiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM070DAC.SQL_EXIST_WARIMASHI)
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

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectwarimashiM", cmd)

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
    ''' 割増運賃マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertwarimashiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM070DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            cmd.Parameters.Clear()
            Call Me.SetParamInsert()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM070DAC", "InsertwarimashiM", cmd)

            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ更新SQLの構築・発行</remarks>
    Private Function UpdatewarimashiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM070DAC.SQL_UPDATE _
                                                                                     , LMM070DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "UpdatewarimashiM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeletewarimashiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM070DAC.SQL_DELETE _
                                                                                      , LMM070DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                      , Me._Row.Item("USER_BR_CD").ToString()))


        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            cmd.Parameters.Clear()

            'SQLパラメータ初期化/設定
            Call Me.SetParamDelete()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM070DAC", "DeletewarimashiM", cmd)

            '更新時排他チェック
            Call Me.UpdateResultChk(cmd)


        Next



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
    ''' パラメータ設定モジュール(新規登録)
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
    ''' パラメータ設定モジュール(更新登録)
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
    ''' パラメータ設定モジュール(削除・復活)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録用)
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
    ''' パラメータ設定モジュール(登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()
        '(篠原　要望番号377)
        '代表/明細識別用JISコード
        Dim jisCd As String = String.Empty
        '代表用JISコード
        Dim daihyo As String = "0000000"
        '(篠原　要望番号377　終了)
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_REM", .Item("EXTC_TARIFF_REM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WINT_KIKAN_FROM", .Item("WINT_KIKAN_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WINT_KIKAN_TO", .Item("WINT_KIKAN_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WINT_EXTC_YN", .Item("WINT_EXTC_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CITY_EXTC_YN", .Item("CITY_EXTC_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RELY_EXTC_YN", .Item("RELY_EXTC_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_YN", .Item("FRRY_EXTC_YN").ToString(), DBDataType.CHAR))
            '(篠原　要望番号377)
            'パラメータのJISコードを取得
            jisCd = .Item("JIS_CD").ToString()
            '代表/明細判明のためのIF文
            If jisCd = daihyo Then
                '10KGあたりの金額をゼロとする(篠原　要望番号377)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_10KG", "0", DBDataType.NUMERIC))
            Else
                '10KGあたりの金額を加える。
                '(篠原　要望番号377　終了)
                'START YANAI 要望番号377
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRRY_EXTC_10KG", .Item("FRRY_EXTC_10KG").ToString(), DBDataType.NUMERIC))
                'END YANAI 要望番号377
            End If

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活時用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me._Row.Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", Me._Row.Item("JIS_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

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
    ''' パラメータ設定モジュール(割増運賃マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定(PK)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me._Row.Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", Me._Row.Item("JIS_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
