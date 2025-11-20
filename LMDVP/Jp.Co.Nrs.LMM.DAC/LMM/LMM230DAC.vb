' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM230DAC : エリアマスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM230DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM230DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"


#Region "制御用"

    '更新前Select文
    Private Const SELECT_INSERT_DATA As String = "SYS_DEL_FLG = '0' AND UPD_FLG = '0' "
    Private Const SELECT_UPDATE_DATA As String = "SYS_DEL_FLG = '0' AND UPD_FLG = '1' "
    Private Const SELECT_DELETE_DATA As String = "SYS_DEL_FLG = '1' AND UPD_FLG = '1' "

#End Region

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(AREA.AREA_CD)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                  " & vbNewLine

    ''' <summary>
    ''' データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                   " & vbNewLine _
                                            & "       AREA.NRS_BR_CD                    AS NRS_BR_CD                     " & vbNewLine _
                                            & "      ,NRSBR.NRS_BR_NM                   AS NRS_BR_NM                     " & vbNewLine _
                                            & "      ,AREA.AREA_CD                      AS AREA_CD                       " & vbNewLine _
                                            & "      ,AREA.AREA_NM                      AS AREA_NM                       " & vbNewLine _
                                            & "      ,AREA.BIN_KB                       AS BIN_KB                        " & vbNewLine _
                                            & "      ,KBN1.KBN_NM1                      AS BIN_KB_NM                     " & vbNewLine _
                                            & "      ,AREA.AREA_INFO                    AS AREA_INFO                     " & vbNewLine _
                                            & "      ,AREA.SYS_ENT_DATE                 AS SYS_ENT_DATE                  " & vbNewLine _
                                            & "      ,USER1.USER_NM                     AS SYS_ENT_USER_NM               " & vbNewLine _
                                            & "      ,AREA.SYS_UPD_DATE                 AS SYS_UPD_DATE                  " & vbNewLine _
                                            & "      ,AREA.SYS_UPD_TIME                 AS SYS_UPD_TIME                  " & vbNewLine _
                                            & "      ,USER2.USER_NM                     AS SYS_UPD_USER_NM               " & vbNewLine _
                                            & "      ,AREA.SYS_DEL_FLG                  AS SYS_DEL_FLG                   " & vbNewLine _
                                            & "      ,KBN2.KBN_NM1                      AS SYS_DEL_NM                    " & vbNewLine

    ''' <summary>
    ''' データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_JIS As String = " SELECT                                                              " & vbNewLine _
                                            & "       AREA.NRS_BR_CD                     AS NRS_BR_CD                   " & vbNewLine _
                                            & "      ,NRSBR.NRS_BR_NM                    AS NRS_BR_NM                   " & vbNewLine _
                                            & "      ,AREA.AREA_CD                       AS AREA_CD                     " & vbNewLine _
                                            & "      ,AREA.AREA_NM                       AS AREA_NM                     " & vbNewLine _
                                            & "      ,AREA.BIN_KB                        AS BIN_KB                      " & vbNewLine _
                                            & "      ,KBN1.KBN_NM1                       AS BIN_KB_NM                   " & vbNewLine _
                                            & "      ,AREA.AREA_INFO                     AS AREA_INFO                   " & vbNewLine _
                                            & "      ,AREA.SYS_ENT_DATE                  AS SYS_ENT_DATE                " & vbNewLine _
                                            & "      ,USER1.USER_NM                      AS SYS_ENT_USER_NM             " & vbNewLine _
                                            & "      ,AREA.SYS_UPD_DATE                  AS SYS_UPD_DATE                " & vbNewLine _
                                            & "      ,AREA.SYS_UPD_TIME                  AS SYS_UPD_TIME                " & vbNewLine _
                                            & "      ,USER2.USER_NM                      AS SYS_UPD_USER_NM             " & vbNewLine _
                                            & "      ,KBN2.KBN_NM1                       AS SYS_DEL_NM                  " & vbNewLine _
                                            & "      ,AREA.JIS_CD                        AS JIS_CD                      " & vbNewLine _
                                            & "      ,JIS.KEN                            AS KEN                         " & vbNewLine _
                                            & "      ,JIS.SHI                            AS SHI                         " & vbNewLine _
                                            & "      ,'1'                                AS UPD_FLG                     " & vbNewLine _
                                            & "      ,AREA.SYS_DEL_FLG                   AS SYS_DEL_FLG                 " & vbNewLine


#End Region

#Region "FROM句"

    ''' <summary>
    ''' エリア情報
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA As String = "FROM                                          " & vbNewLine _
                      & "            $LM_MST$..M_AREA         AS AREA                      " & vbNewLine _
                      & "     INNER JOIN                                                   " & vbNewLine _
                      & "             (                                                    " & vbNewLine _
                      & "              SELECT                                              " & vbNewLine _
                      & "                    AREA1.NRS_BR_CD            AS NRS_BR_CD       " & vbNewLine _
                      & "                   ,AREA1.AREA_CD              AS AREA_CD         " & vbNewLine _
                      & "                    -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start   " & vbNewLine _
                      & "                   ,AREA1.BIN_KB               AS BIN_KB                                       " & vbNewLine _
                      & "                    -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　   " & vbNewLine _
                      & "                   ,MIN(AREA1.JIS_CD)          AS JIS_CD          " & vbNewLine _
                      & "              FROM                                                " & vbNewLine _
                      & "                    $LM_MST$..M_AREA   AS AREA1                     " & vbNewLine _
                      & "     INNER JOIN                                                   " & vbNewLine _
                      & "             (                                                    " & vbNewLine _
                      & "              SELECT                                              " & vbNewLine _
                      & "                   AREA2.NRS_BR_CD             AS NRS_BR_CD       " & vbNewLine _
                      & "                  ,AREA2.AREA_CD               AS AREA_CD         " & vbNewLine _
                      & "                   -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start    " & vbNewLine _
                      & "                  ,AREA2.BIN_KB                AS BIN_KB                                       " & vbNewLine _
                      & "                   -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　    " & vbNewLine _
                      & "                  ,MAX(AREA2.SYS_ENT_DATE)     AS SYS_ENT_DATE    " & vbNewLine _
                      & "                  ,MAX(AREA2.SYS_UPD_DATE)     AS SYS_UPD_DATE    " & vbNewLine _
                      & "                  ,MAX(AREA2.SYS_UPD_TIME)     AS SYS_UPD_TIME    " & vbNewLine _
                      & "              FROM                                                " & vbNewLine _
                      & "                   $LM_MST$..M_AREA   AS AREA2                      " & vbNewLine _
                      & "              GROUP BY                                            " & vbNewLine _
                      & "                   AREA2.NRS_BR_CD                                " & vbNewLine _
                      & "                  ,AREA2.AREA_CD                                  " & vbNewLine _
                      & "                   -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start    " & vbNewLine _
                      & "                  ,AREA2.BIN_KB                                                                " & vbNewLine _
                      & "                   -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　    " & vbNewLine _
                      & "              )                     AS AREA2                      " & vbNewLine _
                      & "      ON   AREA1.NRS_BR_CD    = AREA2.NRS_BR_CD                   " & vbNewLine _
                      & "     AND   AREA1.AREA_CD      = AREA2.AREA_CD                     " & vbNewLine _
                      & "     -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start                  " & vbNewLine _
                      & "     AND   AREA1.BIN_KB       = AREA2.BIN_KB                                                    " & vbNewLine _
                      & "     -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End                    " & vbNewLine _
                      & "     AND   AREA1.SYS_ENT_DATE = AREA2.SYS_ENT_DATE                " & vbNewLine _
                      & "     AND   AREA1.SYS_UPD_DATE = AREA2.SYS_UPD_DATE                " & vbNewLine _
                      & "     AND   AREA1.SYS_UPD_TIME = AREA2.SYS_UPD_TIME                " & vbNewLine _
                      & "     GROUP BY                                                     " & vbNewLine _
                      & "          AREA1.NRS_BR_CD                                         " & vbNewLine _
                      & "         ,AREA1.AREA_CD                                           " & vbNewLine _
                      & "          -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start             " & vbNewLine _
                      & "         ,AREA1.BIN_KB                                                                         " & vbNewLine _
                      & "          -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End               " & vbNewLine _
                      & "               )                    AS SUB                        " & vbNewLine _
                      & "      ON   AREA.NRS_BR_CD     = SUB.NRS_BR_CD                     " & vbNewLine _
                      & "     AND   AREA.AREA_CD       = SUB.AREA_CD                       " & vbNewLine _
                      & "     AND   AREA.JIS_CD        = SUB.JIS_CD                        " & vbNewLine _
                      & "     -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start                  " & vbNewLine _
                      & "     AND   AREA.BIN_KB        = SUB.BIN_KB                                                     " & vbNewLine _
                      & "     -- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End                    " & vbNewLine _
                      & "  LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                    " & vbNewLine _
                      & "    ON AREA.NRS_BR_CD       = NRSBR.NRS_BR_CD                     " & vbNewLine _
                      & "   AND NRSBR.SYS_DEL_FLG    = '0'                                 " & vbNewLine _
                      & "  LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                     " & vbNewLine _
                      & "    ON AREA.BIN_KB        = KBN1.KBN_CD                           " & vbNewLine _
                      & "   AND KBN1.KBN_GROUP_CD    = 'U001'                              " & vbNewLine _
                      & "   AND KBN1.SYS_DEL_FLG     = '0'                                 " & vbNewLine _
                      & "  LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1                    " & vbNewLine _
                      & "    ON AREA.SYS_ENT_USER    = USER1.USER_CD                       " & vbNewLine _
                      & "   AND USER1.SYS_DEL_FLG    = '0'                                 " & vbNewLine _
                      & "  LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2                    " & vbNewLine _
                      & "    ON AREA.SYS_UPD_USER    = USER2.USER_CD                       " & vbNewLine _
                      & "   AND USER2.SYS_DEL_FLG    = '0'                                 " & vbNewLine _
                      & "  LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                     " & vbNewLine _
                      & "    ON AREA.SYS_DEL_FLG     = KBN2.KBN_CD                         " & vbNewLine _
                      & "   AND KBN2.KBN_GROUP_CD    = 'S051'                              " & vbNewLine _
                      & "   AND KBN2.SYS_DEL_FLG     = '0'                                 " & vbNewLine


    ''' <summary>
    ''' JIS情報あり
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA_JIS As String = "FROM                                                     " & vbNewLine _
                                          & "                      $LM_MST$..M_AREA AS AREA               " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR           " & vbNewLine _
                                          & "        ON AREA.NRS_BR_CD       = NRSBR.NRS_BR_CD            " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1            " & vbNewLine _
                                          & "        ON AREA.BIN_KB        = KBN1.KBN_CD                  " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD    = 'U001'                     " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1           " & vbNewLine _
                                          & "       ON AREA.SYS_ENT_USER     = USER1.USER_CD              " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2           " & vbNewLine _
                                          & "       ON AREA.SYS_UPD_USER     = USER2.USER_CD              " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2            " & vbNewLine _
                                          & "        ON AREA.SYS_DEL_FLG     = KBN2.KBN_CD                " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD    = 'S051'                     " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_JIS     AS JIS             " & vbNewLine _
                                          & "        ON AREA.JIS_CD          = JIS.JIS_CD                 " & vbNewLine _
                                          & "       AND JIS.SYS_DEL_FLG      = '0'                        " & vbNewLine

    ''' <summary>
    ''' カウント用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_COUNT As String = "FROM                                                        " & vbNewLine _
                                              & "                      $LM_MST$..M_AREA    AS AREA        " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                              " & vbNewLine _
                                         & "     AREA.AREA_CD                                     " & vbNewLine _
                                         & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start    " & vbNewLine _
                                         & "    ,AREA.BIN_KB                                                            " & vbNewLine _
                                         & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　    " & vbNewLine

    ''' <summary>
    ''' ORDER BY2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "    ,AREA.JIS_CD                                     " & vbNewLine

#End Region


#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' エリアコード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_AREA As String = "SELECT                                                  " & vbNewLine _
                                            & "   COUNT(AREA.AREA_CD)  AS REC_CNT                    " & vbNewLine

    Private Const SQL_EXIT_WHERE As String = " WHERE                                                     " & vbNewLine _
                                            & "        AREA.NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                            & "   AND  AREA.AREA_CD              = @AREA_CD              " & vbNewLine _
                                            & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start   " & vbNewLine _
                                            & "   AND  AREA.BIN_KB               = @BIN_KB                                " & vbNewLine _
                                            & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　   " & vbNewLine

    Private Const SQL_EXIT_JIS As String = "   AND   AREA.JIS_CD                  = @JIS_CD               " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    ''' <summary>
    ''' 新規登録SQL(M_AREA)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_AREA      " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                        & "      NRS_BR_CD                  " & vbNewLine _
                                       & "      ,AREA_CD                    " & vbNewLine _
                                       & "      ,JIS_CD                     " & vbNewLine _
                                       & "      ,AREA_NM                    " & vbNewLine _
                                       & "      ,BIN_KB                     " & vbNewLine _
                                       & "      ,AREA_INFO                  " & vbNewLine _
                                       & "      ,SYS_ENT_DATE               " & vbNewLine _
                                       & "      ,SYS_ENT_TIME               " & vbNewLine _
                                       & "      ,SYS_ENT_PGID               " & vbNewLine _
                                       & "      ,SYS_ENT_USER               " & vbNewLine _
                                       & "      ,SYS_UPD_DATE               " & vbNewLine _
                                       & "      ,SYS_UPD_TIME               " & vbNewLine _
                                       & "      ,SYS_UPD_PGID               " & vbNewLine _
                                       & "      ,SYS_UPD_USER               " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                " & vbNewLine _
                                       & "      ) VALUES (                  " & vbNewLine _
                                       & "       @NRS_BR_CD                 " & vbNewLine _
                                       & "      ,@AREA_CD                   " & vbNewLine _
                                       & "      ,@JIS_CD                    " & vbNewLine _
                                       & "      ,@AREA_NM                   " & vbNewLine _
                                       & "      ,@BIN_KB                    " & vbNewLine _
                                       & "      ,@AREA_INFO                 " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE              " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME              " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID              " & vbNewLine _
                                       & "      ,@SYS_ENT_USER              " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE              " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME              " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID              " & vbNewLine _
                                       & "      ,@SYS_UPD_USER              " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG               " & vbNewLine _
                                       & ")                                 " & vbNewLine
    
#End Region

#Region "DELETE"

    ''' <summary>
    ''' 物理削除SQL(M_AREA)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_JIS As String = "DELETE FROM $LM_MST$..M_AREA                      " & vbNewLine _
                                          & " WHERE   NRS_BR_CD     = @NRS_BR_CD               " & vbNewLine _
                                          & "   AND   AREA_CD       = @AREA_CD                 " & vbNewLine _
                                          & "   AND   JIS_CD        = @JIS_CD                  " & vbNewLine _
                                          & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start   " & vbNewLine _
                                          & "   AND   BIN_KB        = @BIN_KB                                           " & vbNewLine _
                                          & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　   " & vbNewLine

#End Region

#Region "UPDATE"

    ''' <summary>
    ''' 更新SQL(M_AREA)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_AREA SET                         " & vbNewLine _
                                       & "        AREA_NM             = @AREA_NM              " & vbNewLine _
                                       & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start   " & vbNewLine _
                                       & "--       ,BIN_KB              = @BIN_KB               " & vbNewLine _
                                       & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End     " & vbNewLine _
                                       & "       ,AREA_INFO           = @AREA_INFO            " & vbNewLine _
                                       & "       ,SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                              " & vbNewLine _
                                       & "         NRS_BR_CD          = @NRS_BR_CD            " & vbNewLine _
                                       & "   AND   AREA_CD            = @AREA_CD              " & vbNewLine _
                                       & "   AND   JIS_CD             = @JIS_CD               " & vbNewLine _
                                       & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start   " & vbNewLine _
                                       & "   AND   BIN_KB             = @BIN_KB                                      " & vbNewLine _
                                       & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　   " & vbNewLine


#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL(M_AREA)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_AREA SET                           " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & "   AND   AREA_CD              = @AREA_CD              " & vbNewLine _
                                       & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start  " & vbNewLine _
                                       & "   AND   BIN_KB               = @BIN_KB                                   " & vbNewLine _
                                       & "-- 要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End　  " & vbNewLine


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
    ''' エリアマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM230DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM230DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM230DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' エリアマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM230DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM230DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM230DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM230DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("AREA_CD", "AREA_CD")
        map.Add("AREA_NM", "AREA_NM")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_KB_NM", "BIN_KB_NM")
        map.Add("AREA_INFO", "AREA_INFO")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM230OUT")

        Return ds

    End Function

    ''' <summary>
    ''' エリアマスタ(JIS情報)更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ(JIS情報)更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM230DAC.SQL_SELECT_DATA_JIS)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM230DAC.SQL_FROM_DATA_JIS)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM230DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        Me._StrSql.Append(LMM230DAC.SQL_ORDER_BY2)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM230DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("AREA_CD", "AREA_CD")
        map.Add("AREA_NM", "AREA_NM")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_KB_NM", "BIN_KB_NM")
        map.Add("AREA_INFO", "AREA_INFO")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("KEN", "KEN")
        map.Add("SHI", "SHI")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM230JIS")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_AREA)
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
                andstr.Append(" (AREA.SYS_DEL_FLG = @SYS_DEL_FLG  OR AREA.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (AREA.NRS_BR_CD = @NRS_BR_CD OR AREA.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("AREA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" AREA.AREA_CD LIKE @AREA_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("AREA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" AREA.AREA_NM LIKE @AREA_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" AREA.BIN_KB = @BIN_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("AREA_INFO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" AREA.AREA_INFO LIKE @AREA_INFO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_INFO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("JIS_CD").ToString()
            Dim whereStr2 As String = whereStr
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" AREA.NRS_BR_CD IN ( SELECT NRS_BR_CD FROM  M_AREA  WHERE JIS_CD LIKE @JIS_CD1 GROUP BY NRS_BR_CD)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD1", String.Concat(whereStr, "%"), DBDataType.CHAR))
                andstr.Append(" AND AREA.AREA_CD IN ( SELECT AREA_CD FROM  M_AREA  WHERE JIS_CD LIKE @JIS_CD2 GROUP BY AREA_CD)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD2", String.Concat(whereStr2, "%"), DBDataType.CHAR))
                '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
                andstr.Append(" AND AREA.BIN_KB  IN ( SELECT BIN_KB FROM  M_AREA  WHERE JIS_CD LIKE @JIS_CD3 GROUP BY BIN_KB)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD3", String.Concat(whereStr, "%"), DBDataType.CHAR))
                '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End
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
    ''' エリアマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230JIS")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append(LMM230DAC.SQL_EXIT_AREA)
        Me._StrSql.Append(LMM230DAC.SQL_FROM_DATA)
        Me._StrSql.Append(LMM230DAC.SQL_EXIT_WHERE)
        Me._StrSql.Append("AND AREA.SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND AREA.SYS_UPD_TIME = @SYS_UPD_TIME")

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

        MyBase.Logger.WriteSQLLog("LMM230DAC", "SelectAreaM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'パラメータ初期化
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
    ''' エリアマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230JIS")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (String.Concat(LMM230DAC.SQL_EXIT_AREA _
                                                                       , LMM230DAC.SQL_FROM_COUNT _
                                                                       , LMM230DAC.SQL_EXIT_WHERE) _
                                                        , Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM230DAC", "CheckExistAreaM", cmd)

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
    ''' エリアマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230JIS")

        Dim inRow As DataRow() = inTbl.Select(SELECT_INSERT_DATA)

        Dim max As Integer = inRow.Length - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM230DAC.SQL_INSERT, inRow(0).Item("USER_BR_CD").ToString()))

            For i As Integer = 0 To max

                'INTableの条件rowの格納
                Me._Row = inRow(i)

                'SQL格納変数の初期化
                Me._StrSql = New StringBuilder()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                '新規登録処理件数設定用
                Dim insCnt As Integer = 0

                'SQLパラメータ初期化/設定
                Call Me.SetParamInsert()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM230DAC", "InsertAreaM", cmd)

                'SQLの発行
                MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

                'パラメータ初期化
                cmd.Parameters.Clear()

            Next

        End If

        Return ds

    End Function

   #End Region

#Region "Update"

    ''' <summary>
    ''' エリアマスタ存在チェック(新規JISコード登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistJisAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230JIS")

        Dim inRow As DataRow() = inTbl.Select(SELECT_INSERT_DATA)

        Dim max As Integer = inRow.Length - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                (String.Concat(LMM230DAC.SQL_EXIT_AREA _
                                                               , LMM230DAC.SQL_FROM_COUNT _
                                                               , LMM230DAC.SQL_EXIT_WHERE _
                                                               , LMM230DAC.SQL_EXIT_JIS) _
                                                , inRow(0).Item("USER_BR_CD").ToString()))

            Dim reader As SqlDataReader = Nothing


            For i As Integer = 0 To max

                'INTableの条件rowの格納
                Me._Row = inRow(i)

                'SQL格納変数の初期化
                Me._StrSql = New StringBuilder()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ初期化/設定
                Call Me.SetParamExistChk()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM230DAC", "CheckExistJisAreaM", cmd)

                'SQLの発行
                reader = MyBase.GetSelectResult(cmd)

                'パラメータの初期化
                cmd.Parameters.Clear()

                '処理件数の設定
                reader.Read()
                MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
                reader.Close()

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' エリアマスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ物理削除SQLの構築・発行</remarks>
    Private Function PhysicDeleteAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230JIS")

        Dim inRow As DataRow() = inTbl.Select(SELECT_DELETE_DATA)

        Dim max As Integer = inRow.Length - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM230DAC.SQL_DEL_JIS _
                                                                                         , LMM230DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                         , inRow(0).Item("USER_BR_CD").ToString()))


            For i As Integer = 0 To max

                'INTableの条件rowの格納
                Me._Row = inRow(i)

                Me._SqlPrmList = New ArrayList()


                'SQLパラメータ初期化/設定
                Call Me.SetParamPhysicDeleteChk()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM230DAC", "PhysicDeleteAreaM", cmd)


                '更新時排他チェック
                If Me.UpdateResultChk(cmd) = False Then
                    Return ds
                End If

                'パラメータの初期化
                cmd.Parameters.Clear()

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' エリアマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230JIS")

        Dim inRow As DataRow() = inTbl.Select(SELECT_UPDATE_DATA)

        Dim max As Integer = inRow.Length - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM230DAC.SQL_UPDATE _
                                                                                         , LMM230DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                         , inRow(0).Item("USER_BR_CD").ToString()))

            For i As Integer = 0 To max

                'INTableの条件rowの格納
                Me._Row = inRow(i)

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ設定
                Call Me.SetParamUpdate()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMM230DAC", "UpdateAreaM", cmd)

                '更新時排他チェック
                If Me.UpdateResultChk(cmd) = False Then
                    Return ds
                End If

                'パラメータの初期化
                cmd.Parameters.Clear()

            Next

        End If

        Return ds

    End Function

#End Region

#Region "Delete"

    ''' <summary>
    '''  エリアマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エリアマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteAreaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM230IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM230DAC.SQL_DELETE _
                                                                                     , LMM230DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM230DAC", "DeleteAreaM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

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
    ''' パラメータ設定モジュール(エリアマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.NVARCHAR))

            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

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
    ''' パラメータ設定モジュール(物理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamPhysicDeleteChk()

        'キー項目
        Call Me.SetParamExistChk()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

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
    ''' パラメータ設定モジュール(更新登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_NM", .Item("AREA_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_INFO", .Item("AREA_INFO").ToString(), DBDataType.NVARCHAR))

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

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        End With

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        With Me._Row
            '画面パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With
        
    End Sub

#End Region

#End Region

#End Region

End Class
