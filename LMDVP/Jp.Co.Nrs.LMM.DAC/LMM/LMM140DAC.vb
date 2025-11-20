' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM140DAC : ZONEマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM140DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM140DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(ZONE.NRS_BR_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' M_ZONEデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                              " & vbNewLine _
                                            & "  ZONE.NRS_BR_CD                                           AS NRS_BR_CD              " & vbNewLine _
                                            & " ,NRSBR.NRS_BR_NM                                          AS NRS_BR_NM              " & vbNewLine _
                                            & " ,ZONE.WH_CD                                               AS WH_CD                  " & vbNewLine _
                                            & " ,SOKO.WH_NM                                               AS WH_NM                  " & vbNewLine _
                                            & " ,ZONE.TOU_NO                                              AS TOU_NO                 " & vbNewLine _
                                            & " ,ZONE.SITU_NO                                             AS SITU_NO                " & vbNewLine _
                                            & " ,TOUSITU.TOU_SITU_NM                                      AS TOU_SITU_NM            " & vbNewLine _
                                            & " ,ZONE.ZONE_CD                                             AS ZONE_CD                " & vbNewLine _
                                            & " ,ZONE.ZONE_NM                                             AS ZONE_NM                " & vbNewLine _
                                            & " ,ZONE.ONDO_CTL_KB                                         AS ONDO_CTL_KB            " & vbNewLine _
                                            & " ,KBN1.KBN_NM1                                             AS ONDO_CTL_KB_NM         " & vbNewLine _
                                            & " ,ZONE.ONDO                                                AS ONDO                   " & vbNewLine _
                                            & " ,ZONE.MAX_ONDO_UP                                         AS MAX_ONDO_UP            " & vbNewLine _
                                            & " ,ZONE.MINI_ONDO_DOWN                                      AS MINI_ONDO_DOWN         " & vbNewLine _
                                            & " ,ZONE.ONDO_CTL_FLG                                        AS ONDO_CTL_FLG           " & vbNewLine _
                                            & " ,KBN2.KBN_NM1                                             AS ONDO_CTL_FLG_NM        " & vbNewLine _
                                            & " ,ZONE.HOZEI_KB                                            AS HOZEI_KB               " & vbNewLine _
                                            & " ,KBN3.KBN_NM1                                             AS HOZEI_KB_NM            " & vbNewLine _
                                            & " ,ZONE.YAKUJI_YN                                           AS YAKUJI_YN              " & vbNewLine _
                                            & " ,KBN4.KBN_NM2                                             AS YAKUJI_YN_NM           " & vbNewLine _
                                            & " ,ZONE.DOKU_YN                                             AS DOKU_YN                " & vbNewLine _
                                            & " ,KBN5.KBN_NM2                                             AS DOKU_YN_NM             " & vbNewLine _
                                            & " ,ZONE.GASS_YN                                             AS GASS_YN                " & vbNewLine _
                                            & " ,KBN6.KBN_NM2                                             AS GASS_YN_NM             " & vbNewLine _
                                            & " ,ZONE.ZONE_KB                                             AS ZONE_KB                " & vbNewLine _
                                            & " ,ZONE.SEIQTO_CD                                           AS SEIQTO_CD              " & vbNewLine _
                                            & " ,SEIQTO.SEIQTO_NM + '' + SEIQTO.SEIQTO_BUSYO_NM           AS SEIQTO_NM              " & vbNewLine _
                                            & " ,ZONE.TSUBO_AM                                            AS TSUBO_AM               " & vbNewLine _
                                            & " ,ZONE.SYS_ENT_DATE                                        AS SYS_ENT_DATE           " & vbNewLine _
                                            & " ,USER1.USER_NM                                            AS SYS_ENT_USER_NM        " & vbNewLine _
                                            & " ,ZONE.SYS_UPD_DATE                                        AS SYS_UPD_DATE           " & vbNewLine _
                                            & " ,ZONE.SYS_UPD_TIME                                        AS SYS_UPD_TIME           " & vbNewLine _
                                            & " ,USER2.USER_NM                                            AS SYS_UPD_USER_NM        " & vbNewLine _
                                            & " ,ZONE.SYS_DEL_FLG                                         AS SYS_DEL_FLG            " & vbNewLine _
                                            & " ,KBN7.KBN_NM1                                             AS SYS_DEL_NM             " & vbNewLine

    ''' <summary>
    ''' M_ZONE_SHOBOデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                          " & vbNewLine _
                                            & "	      ZONE_SHOBO.NRS_BR_CD                      AS NRS_BR_CD                 " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.WH_CD                          AS WH_CD                     " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.TOU_NO                         AS TOU_NO                    " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.SITU_NO                        AS SITU_NO                   " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.ZONE_CD                        AS ZONE_CD                  " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.SHOBO_CD                       AS SHOBO_CD                  " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.WH_KYOKA_DATE                  AS WH_KYOKA_DATE             " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.BAISU                          AS BAISU                     " & vbNewLine _
                                            & "	     ,SHOBO.HINMEI                              AS HINMEI                    " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                              AS TOKYU                     " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                              AS SHUBETU                   " & vbNewLine _
                                            & "	     ,'1'                                       AS UPD_FLG                   " & vbNewLine _
                                            & "	     ,ZONE_SHOBO.SYS_DEL_FLG                    AS SYS_DEL_FLG               " & vbNewLine

    ''' <summary>
    ''' M_TOU_SITU_ZONE_CHKデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA3 As String = " SELECT                                                 " & vbNewLine _
                                             & "  TOU_SITU_ZONE_CHK.NRS_BR_CD       AS NRS_BR_CD        " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.WH_CD           AS WH_CD            " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.TOU_NO          AS TOU_NO           " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.SITU_NO         AS SITU_NO          " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.ZONE_CD         AS ZONE_CD          " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.KBN_GROUP_CD    AS KBN_GROUP_CD     " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.KBN_CD          AS KBN_CD           " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.KBN_NM1         AS KBN_NM1          " & vbNewLine _
                                             & " ,'1'                               AS UPD_FLG          " & vbNewLine _
                                             & " ,TOU_SITU_ZONE_CHK.SYS_DEL_FLG     AS SYS_DEL_FLG      " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                             " & vbNewLine _
                                          & "                $LM_MST$..M_ZONE           AS ZONE               " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_NRS_BR         AS NRSBR              " & vbNewLine _
                                          & "             ON ZONE.NRS_BR_CD      = NRSBR.NRS_BR_CD            " & vbNewLine _
                                          & "            AND NRSBR.SYS_DEL_FLG   = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_SOKO           AS SOKO               " & vbNewLine _
                                          & "             ON ZONE.WH_CD          = SOKO.WH_CD                 " & vbNewLine _
                                          & "            AND SOKO.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_TOU_SITU       AS TOUSITU            " & vbNewLine _
                                          & "             ON ZONE.NRS_BR_CD      = TOUSITU.NRS_BR_CD          " & vbNewLine _
                                          & "            AND ZONE.WH_CD          = TOUSITU.WH_CD              " & vbNewLine _
                                          & "            AND ZONE.TOU_NO         = TOUSITU.TOU_NO             " & vbNewLine _
                                          & "            AND ZONE.SITU_NO        = TOUSITU.SITU_NO            " & vbNewLine _
                                          & "            AND TOUSITU.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN1               " & vbNewLine _
                                          & "             ON ZONE.ONDO_CTL_KB    = KBN1.KBN_CD                " & vbNewLine _
                                          & "            AND KBN1.KBN_GROUP_CD   = 'O002'                     " & vbNewLine _
                                          & "            AND KBN1.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN2               " & vbNewLine _
                                          & "             ON ZONE.ONDO_CTL_FLG   = KBN2.KBN_CD                " & vbNewLine _
                                          & "            AND KBN2.KBN_GROUP_CD   = 'O004'                     " & vbNewLine _
                                          & "            AND KBN2.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN3               " & vbNewLine _
                                          & "             ON ZONE.HOZEI_KB       = KBN3.KBN_CD                " & vbNewLine _
                                          & "            AND KBN3.KBN_GROUP_CD   = 'H001'                     " & vbNewLine _
                                          & "            AND KBN3.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN4               " & vbNewLine _
                                          & "             ON ZONE.YAKUJI_YN      = KBN4.KBN_CD                " & vbNewLine _
                                          & "            AND KBN4.KBN_GROUP_CD   = 'U009'                     " & vbNewLine _
                                          & "            AND KBN4.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN5               " & vbNewLine _
                                          & "             ON ZONE.DOKU_YN        = KBN5.KBN_CD                " & vbNewLine _
                                          & "            AND KBN5.KBN_GROUP_CD   = 'U009'                     " & vbNewLine _
                                          & "            AND KBN5.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN6               " & vbNewLine _
                                          & "             ON ZONE.GASS_YN        = KBN6.KBN_CD                " & vbNewLine _
                                          & "            AND KBN6.KBN_GROUP_CD   = 'U009'                     " & vbNewLine _
                                          & "            AND KBN6.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..M_SEIQTO         AS SEIQTO             " & vbNewLine _
                                          & "             ON ZONE.NRS_BR_CD      = SEIQTO.NRS_BR_CD           " & vbNewLine _
                                          & "            AND ZONE.SEIQTO_CD      = SEIQTO.SEIQTO_CD           " & vbNewLine _
                                          & "            AND SEIQTO.SYS_DEL_FLG  = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..S_USER           AS USER1              " & vbNewLine _
                                          & "             ON ZONE.SYS_ENT_USER   = USER1.USER_CD              " & vbNewLine _
                                          & "            AND USER1.SYS_DEL_FLG   = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..S_USER           AS USER2              " & vbNewLine _
                                          & "             ON ZONE.SYS_UPD_USER   = USER2.USER_CD              " & vbNewLine _
                                          & "            AND USER1.SYS_DEL_FLG   = '0'                        " & vbNewLine _
                                          & "LEFT OUTER JOIN $LM_MST$..Z_KBN            AS KBN7               " & vbNewLine _
                                          & "             ON ZONE.SYS_DEL_FLG    = KBN7.KBN_CD                " & vbNewLine _
                                          & "            AND KBN7.KBN_GROUP_CD   = 'S051'                     " & vbNewLine _
                                          & "            AND KBN7.SYS_DEL_FLG    = '0'                        " & vbNewLine

    Private Const SQL_FROM_DATA2 As String = "FROM                                                            " & vbNewLine _
                                          & "                      $LM_MST$..M_ZONE_SHOBO AS ZONE_SHOBO       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SHOBO AS SHOBO                 " & vbNewLine _
                                          & "       ON ZONE_SHOBO.SHOBO_CD     = SHOBO.SHOBO_CD               " & vbNewLine _
                                          & "       AND SHOBO.SYS_DEL_FLG          = '0'                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                " & vbNewLine _
                                          & "        ON SHOBO.KIKEN_TOKYU          = KBN1.KBN_CD              " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD          = 'S002'                   " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG           = '0'                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                " & vbNewLine _
                                          & "        ON SHOBO.SYU = KBN2.KBN_CD                               " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD          = 'S022'                   " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG           = '0'                      " & vbNewLine

    Private Const SQL_FROM_DATA3 As String = "FROM                                                                  " & vbNewLine _
                                           & "                $LM_MST$..M_TOU_SITU_ZONE_CHK AS TOU_SITU_ZONE_CHK    " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                   " & vbNewLine _
                                         & "     ZONE.WH_CD, ZONE.TOU_NO, ZONE.SITU_NO, ZONE.ZONE_CD   " & vbNewLine

    ''' <summary>
    ''' ORDER BY(M_ZONE_SHOBO)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                          " & vbNewLine _
                                         & "     ZONE_SHOBO.NRS_BR_CD                          " & vbNewLine _
                                         & "    ,ZONE_SHOBO.WH_CD                              " & vbNewLine _
                                         & "    ,ZONE_SHOBO.TOU_NO                             " & vbNewLine _
                                         & "    ,ZONE_SHOBO.SITU_NO                            " & vbNewLine _
                                         & "    ,ZONE_SHOBO.SHOBO_CD                           " & vbNewLine _
                                         & "    ,ZONE_SHOBO.ZONE_CD                            " & vbNewLine
    ''' <summary>
    ''' ORDER BY(M_TOU_SITU_ZONE_CHK)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY3 As String = "ORDER BY                               " & vbNewLine _
                                          & "     TOU_SITU_ZONE_CHK.NRS_BR_CD       " & vbNewLine _
                                          & "    ,TOU_SITU_ZONE_CHK.WH_CD           " & vbNewLine _
                                          & "    ,TOU_SITU_ZONE_CHK.TOU_NO          " & vbNewLine _
                                          & "    ,TOU_SITU_ZONE_CHK.SITU_NO         " & vbNewLine _
                                          & "    ,TOU_SITU_ZONE_CHK.ZONE_CD         " & vbNewLine _
                                          & "    ,TOU_SITU_ZONE_CHK.KBN_GROUP_CD    " & vbNewLine _
                                          & "    ,TOU_SITU_ZONE_CHK.KBN_CD          " & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' ZONEコード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_ZONE As String = "SELECT                              " & vbNewLine _
                                            & "   COUNT(WH_CD)  AS REC_CNT       " & vbNewLine _
                                            & "FROM $LM_MST$..M_ZONE             " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                            & "  AND WH_CD        = @WH_CD       " & vbNewLine _
                                            & "  AND TOU_NO       = @TOU_NO      " & vbNewLine _
                                            & "  AND SITU_NO      = @SITU_NO     " & vbNewLine _
                                            & "  AND ZONE_CD      = @ZONE_CD     " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_ZONE      " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "      NRS_BR_CD                   " & vbNewLine _
                                       & "      ,WH_CD                      " & vbNewLine _
                                       & "      ,TOU_NO                     " & vbNewLine _
                                       & "      ,SITU_NO                    " & vbNewLine _
                                       & "      ,ZONE_CD                    " & vbNewLine _
                                       & "      ,ZONE_NM                    " & vbNewLine _
                                       & "      ,ZONE_KB                    " & vbNewLine _
                                       & "      ,ONDO_CTL_KB                " & vbNewLine _
                                       & "      ,ONDO                       " & vbNewLine _
                                       & "      ,MAX_ONDO_UP                " & vbNewLine _
                                       & "      ,MINI_ONDO_DOWN             " & vbNewLine _
                                       & "      ,ONDO_CTL_FLG               " & vbNewLine _
                                       & "      ,HOZEI_KB                   " & vbNewLine _
                                       & "      ,YAKUJI_YN                  " & vbNewLine _
                                       & "      ,DOKU_YN                    " & vbNewLine _
                                       & "      ,GASS_YN                    " & vbNewLine _
                                       & "      ,SEIQTO_CD                  " & vbNewLine _
                                       & "      ,TSUBO_AM                   " & vbNewLine _
                                       & "      ,SYS_ENT_DATE               " & vbNewLine _
                                       & "      ,SYS_ENT_TIME               " & vbNewLine _
                                       & "      ,SYS_ENT_PGID               " & vbNewLine _
                                       & "      ,SYS_ENT_USER               " & vbNewLine _
                                       & "      ,SYS_UPD_DATE               " & vbNewLine _
                                       & "      ,SYS_UPD_TIME               " & vbNewLine _
                                       & "      ,SYS_UPD_PGID               " & vbNewLine _
                                       & "      ,SYS_UPD_USER               " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                " & vbNewLine _
                                       & "       ) VALUES (                 " & vbNewLine _
                                       & "      @NRS_BR_CD                  " & vbNewLine _
                                       & "      ,@WH_CD                     " & vbNewLine _
                                       & "      ,@TOU_NO                    " & vbNewLine _
                                       & "      ,@SITU_NO                   " & vbNewLine _
                                       & "      ,@ZONE_CD                   " & vbNewLine _
                                       & "      ,@ZONE_NM                   " & vbNewLine _
                                       & "      ,@ZONE_KB                   " & vbNewLine _
                                       & "      ,@ONDO_CTL_KB               " & vbNewLine _
                                       & "      ,@ONDO                      " & vbNewLine _
                                       & "      ,@MAX_ONDO_UP               " & vbNewLine _
                                       & "      ,@MINI_ONDO_DOWN            " & vbNewLine _
                                       & "      ,@ONDO_CTL_FLG              " & vbNewLine _
                                       & "      ,@HOZEI_KB                  " & vbNewLine _
                                       & "      ,@YAKUJI_YN                 " & vbNewLine _
                                       & "      ,@DOKU_YN                   " & vbNewLine _
                                       & "      ,@GASS_YN                   " & vbNewLine _
                                       & "      ,@SEIQTO_CD                 " & vbNewLine _
                                       & "      ,@TSUBO_AM                  " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE              " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME              " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID              " & vbNewLine _
                                       & "      ,@SYS_ENT_USER              " & vbNewLine _
                                       & "      ,@DAC_SYS_UPD_DATE          " & vbNewLine _
                                       & "      ,@DAC_SYS_UPD_TIME          " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID              " & vbNewLine _
                                       & "      ,@SYS_UPD_USER              " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG               " & vbNewLine _
                                       & "      )                           " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_ZONE SET                       " & vbNewLine _
                                       & "         ZONE_NM           = @ZONE_NM             " & vbNewLine _
                                       & "        ,ZONE_KB           = @ZONE_KB             " & vbNewLine _
                                       & "        ,ONDO_CTL_KB       = @ONDO_CTL_KB         " & vbNewLine _
                                       & "        ,ONDO              = @ONDO                " & vbNewLine _
                                       & "        ,MAX_ONDO_UP       = @MAX_ONDO_UP         " & vbNewLine _
                                       & "        ,MINI_ONDO_DOWN    = @MINI_ONDO_DOWN      " & vbNewLine _
                                       & "        ,ONDO_CTL_FLG      = @ONDO_CTL_FLG        " & vbNewLine _
                                       & "        ,HOZEI_KB          = @HOZEI_KB            " & vbNewLine _
                                       & "        ,YAKUJI_YN         = @YAKUJI_YN           " & vbNewLine _
                                       & "        ,DOKU_YN           = @DOKU_YN             " & vbNewLine _
                                       & "        ,GASS_YN           = @GASS_YN             " & vbNewLine _
                                       & "        ,SEIQTO_CD         = @SEIQTO_CD           " & vbNewLine _
                                       & "        ,TSUBO_AM          = @TSUBO_AM            " & vbNewLine _
                                       & "        ,SYS_UPD_DATE      = @DAC_SYS_UPD_DATE    " & vbNewLine _
                                       & "        ,SYS_UPD_TIME      = @DAC_SYS_UPD_TIME    " & vbNewLine _
                                       & "        ,SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                       & "        ,SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                       & "        WHERE                                     " & vbNewLine _
                                       & "              NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
                                       & "        AND   WH_CD        = @WH_CD               " & vbNewLine _
                                       & "        AND   TOU_NO       = @TOU_NO              " & vbNewLine _
                                       & "        AND   SITU_NO      = @SITU_NO             " & vbNewLine _
                                       & "        AND   ZONE_CD      = @ZONE_CD             " & vbNewLine _
                                       & "        AND   SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
                                       & "        AND   SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_ZONE SET                           " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @DAC_SYS_UPD_DATE     " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @DAC_SYS_UPD_TIME     " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     WH_CD                = @WH_CD                " & vbNewLine _
                                       & " AND     TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & " AND     SITU_NO              = @SITU_NO              " & vbNewLine _
                                       & " AND     ZONE_CD              = @ZONE_CD              " & vbNewLine _
                                       & " AND     SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                       & " AND     SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine

    ''' <summary>
    ''' 新規登録SQL（M_ZONE_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_ZONE_SHOBO As String = "INSERT INTO $LM_MST$..M_ZONE_SHOBO     " & vbNewLine _
                                       & "(                                                 " & vbNewLine _
                                       & "       NRS_BR_CD                                  " & vbNewLine _
                                       & "      ,WH_CD                                      " & vbNewLine _
                                       & "      ,TOU_NO                                     " & vbNewLine _
                                       & "      ,SITU_NO                                    " & vbNewLine _
                                       & "      ,ZONE_CD                                    " & vbNewLine _
                                       & "      ,SHOBO_CD                                   " & vbNewLine _
                                       & "      ,WH_KYOKA_DATE                              " & vbNewLine _
                                       & "      ,BAISU                                      " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                               " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                               " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                               " & vbNewLine _
                                       & "      ,SYS_ENT_USER                               " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                               " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                               " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                               " & vbNewLine _
                                       & "      ,SYS_UPD_USER                               " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                                " & vbNewLine _
                                       & "      ) VALUES (                                  " & vbNewLine _
                                       & "       @NRS_BR_CD                                 " & vbNewLine _
                                       & "      ,@WH_CD                                     " & vbNewLine _
                                       & "      ,@TOU_NO                                    " & vbNewLine _
                                       & "      ,@SITU_NO                                   " & vbNewLine _
                                       & "      ,@ZONE_CD                                   " & vbNewLine _
                                       & "      ,@SHOBO_CD                                  " & vbNewLine _
                                       & "      ,@WH_KYOKA_DATE                             " & vbNewLine _
                                       & "      ,@BAISU                                     " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                              " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                              " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                              " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                              " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                              " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                              " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                              " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                              " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                               " & vbNewLine _
                                       & ")                                                 " & vbNewLine

    ''' <summary>
    ''' 新規登録SQL（M_TOU_SITU_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TOU_SITU_ZONE_CHK As String = "INSERT INTO $LM_MST$..M_TOU_SITU_ZONE_CHK   " & vbNewLine _
                                        & "(                                                             " & vbNewLine _
                                        & "       NRS_BR_CD                                              " & vbNewLine _
                                        & "      ,WH_CD                                                  " & vbNewLine _
                                        & "      ,TOU_NO                                                 " & vbNewLine _
                                        & "      ,SITU_NO                                                " & vbNewLine _
                                        & "      ,ZONE_CD                                                " & vbNewLine _
                                        & "      ,KBN_GROUP_CD                                           " & vbNewLine _
                                        & "      ,KBN_CD                                                 " & vbNewLine _
                                        & "      ,KBN_NM1                                                " & vbNewLine _
                                        & "      ,SYS_ENT_DATE                                           " & vbNewLine _
                                        & "      ,SYS_ENT_TIME                                           " & vbNewLine _
                                        & "      ,SYS_ENT_PGID                                           " & vbNewLine _
                                        & "      ,SYS_ENT_USER                                           " & vbNewLine _
                                        & "      ,SYS_UPD_DATE                                           " & vbNewLine _
                                        & "      ,SYS_UPD_TIME                                           " & vbNewLine _
                                        & "      ,SYS_UPD_PGID                                           " & vbNewLine _
                                        & "      ,SYS_UPD_USER                                           " & vbNewLine _
                                        & "      ,SYS_DEL_FLG                                            " & vbNewLine _
                                        & "      ) VALUES (                                              " & vbNewLine _
                                        & "       @NRS_BR_CD                                             " & vbNewLine _
                                        & "      ,@WH_CD                                                 " & vbNewLine _
                                        & "      ,@TOU_NO                                                " & vbNewLine _
                                        & "      ,@SITU_NO                                               " & vbNewLine _
                                        & "      ,@ZONE_CD                                               " & vbNewLine _
                                        & "      ,@KBN_GROUP_CD                                          " & vbNewLine _
                                        & "      ,@KBN_CD                                                " & vbNewLine _
                                        & "      ,@KBN_NM1                                               " & vbNewLine _
                                        & "      ,@SYS_ENT_DATE                                          " & vbNewLine _
                                        & "      ,@SYS_ENT_TIME                                          " & vbNewLine _
                                        & "      ,@SYS_ENT_PGID                                          " & vbNewLine _
                                        & "      ,@SYS_ENT_USER                                          " & vbNewLine _
                                        & "      ,@SYS_UPD_DATE                                          " & vbNewLine _
                                        & "      ,@SYS_UPD_TIME                                          " & vbNewLine _
                                        & "      ,@SYS_UPD_PGID                                          " & vbNewLine _
                                        & "      ,@SYS_UPD_USER                                          " & vbNewLine _
                                        & "      ,@SYS_DEL_FLG                                           " & vbNewLine _
                                        & ")                                                             " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_ZONE_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_ZONE_SHOBO As String = "DELETE FROM $LM_MST$..M_ZONE_SHOBO    " & vbNewLine _
                                       & "WHERE   WH_CD                 = @WH_CD        " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO       " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO      " & vbNewLine _
                                       & "     AND ZONE_CD              = @ZONE_CD      " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_TOU_SITU_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_SITU_ZONE_CHK As String = "DELETE FROM $LM_MST$..M_TOU_SITU_ZONE_CHK    " & vbNewLine _
                                    & " WHERE WH_CD                = @WH_CD         " & vbNewLine _
                                    & "   AND TOU_NO               = @TOU_NO        " & vbNewLine _
                                    & "   AND SITU_NO              = @SITU_NO       " & vbNewLine _
                                    & "   AND ZONE_CD              = @ZONE_CD       " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_ZONE_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZONE_SHOBO As String = "UPDATE $LM_MST$..M_ZONE_SHOBO SET      " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @DAC_SYS_UPD_DATE     " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @DAC_SYS_UPD_TIME     " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO              " & vbNewLine _
                                       & "     AND ZONE_CD              = @ZONE_CD              " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_TOU_SITU_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TOU_SITU_ZONE_CHK As String = "UPDATE $LM_MST$..M_TOU_SITU_ZONE_CHK SET             " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @DAC_SYS_UPD_DATE    " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @DAC_SYS_UPD_TIME    " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID        " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER        " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG         " & vbNewLine _
                                       & " WHERE                                               " & vbNewLine _
                                       & "         WH_CD                = @WH_CD               " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO              " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO             " & vbNewLine _
                                       & "     AND ZONE_CD              = @ZONE_CD             " & vbNewLine


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
    ''' ZONEマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM140DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM140DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM140DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM140DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM140DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("TOU_SITU_NM", "TOU_SITU_NM")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("ZONE_NM", "ZONE_NM")
        map.Add("ONDO_CTL_KB", "ONDO_CTL_KB")
        map.Add("ONDO_CTL_KB_NM", "ONDO_CTL_KB_NM")
        map.Add("ONDO", "ONDO")
        map.Add("MAX_ONDO_UP", "MAX_ONDO_UP")
        map.Add("MINI_ONDO_DOWN", "MINI_ONDO_DOWN")
        map.Add("ONDO_CTL_FLG", "ONDO_CTL_FLG")
        map.Add("ONDO_CTL_FLG_NM", "ONDO_CTL_FLG_NM")
        map.Add("HOZEI_KB", "HOZEI_KB")
        map.Add("HOZEI_KB_NM", "HOZEI_KB_NM")
        map.Add("YAKUJI_YN", "YAKUJI_YN")
        map.Add("YAKUJI_YN_NM", "YAKUJI_YN_NM")
        map.Add("DOKU_YN", "DOKU_YN")
        map.Add("DOKU_YN_NM", "DOKU_YN_NM")
        map.Add("GASS_YN", "GASS_YN")
        map.Add("GASS_YN_NM", "GASS_YN_NM")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("ZONE_KB", "ZONE_KB")
        map.Add("TSUBO_AM", "TSUBO_AM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM140OUT")

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
                andstr.Append(" (ZONE.SYS_DEL_FLG = @SYS_DEL_FLG  OR ZONE.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (ZONE.NRS_BR_CD = @NRS_BR_CD OR ZONE.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.WH_CD = @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                'START YANAI 要望番号705
                'andstr.Append(" ZONE.SITU_NO = @SITU_NO")
                'andstr.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", whereStr, DBDataType.CHAR))
                andstr.Append(" ZONE.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
                'END YANAI 要望番号705
            End If

            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                'START YANAI 要望番号705
                'andstr.Append(" ZONE.ZONE_CD = @ZONE_CD")
                'andstr.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", whereStr, DBDataType.CHAR))
                andstr.Append(" ZONE.ZONE_CD LIKE @ZONE_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
                'END YANAI 要望番号705
            End If

            whereStr = .Item("ZONE_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.ZONE_NM LIKE @ZONE_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("ONDO_CTL_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.ONDO_CTL_KB = @ONDO_CTL_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ONDO_CTL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.ONDO_CTL_FLG = @ONDO_CTL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HOZEI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.HOZEI_KB = @HOZEI_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOZEI_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("YAKUJI_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.YAKUJI_YN = @YAKUJI_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YAKUJI_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("DOKU_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.DOKU_YN = @DOKU_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("GASS_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE.GASS_YN = @GASS_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GASS_YN", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' ZONEマスタ消防更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ消防更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM140DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM140DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM140DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("WH_KYOKA_DATE", "WH_KYOKA_DATE")
        map.Add("BAISU", "BAISU")
        map.Add("HINMEI", "HINMEI")
        map.Add("TOKYU", "TOKYU")
        map.Add("SHUBETU", "SHUBETU")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM140_ZONE_SHOBO")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_ZONE_SHOBO)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE_SHOBO.WH_CD LIKE @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE_SHOBO.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE_SHOBO.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZONE_SHOBO.ZONE_CD LIKE @ZONE_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData3(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM140DAC.SQL_SELECT_DATA3)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM140DAC.SQL_FROM_DATA3)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL3()                   '条件設定
        Me._StrSql.Append(LMM140DAC.SQL_ORDER_BY3)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM140_TOU_SITU_ZONE_CHK")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU_ZONE_CHK)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL3()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_ZONE_CHK.WH_CD LIKE @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_ZONE_CHK.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_ZONE_CHK.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_ZONE_CHK.ZONE_CD LIKE @ZONE_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'ZONEコードは固定
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" TOU_SITU_ZONE_CHK.ZONE_CD <> '' ")
            andstr.Append(vbNewLine)

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' Zoneマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectZoneM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM140DAC.SQL_EXIT_ZONE)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "SelectZoneM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistZoneM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_EXIT_ZONE _
                                                                        , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "CheckExistZoneM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader("REC_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertZoneM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "InsertZoneM", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ消防情報新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ消防情報新規登録SQLの構築・発行</remarks>
    Private Function InsertZoneShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140_ZONE_SHOBO")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_INSERT_ZONE_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetZoneShoboParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM140DAC", "InsertZoneShoboM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateZoneM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_UPDATE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "UpdateZoneM", cmd)

        '削除出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' Zoneマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>Zoneマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteZoneM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "DeleteZoneM", cmd)

        '削除出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ消防情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ消防情報新規登録SQLの構築・発行</remarks>
    Private Function DelZoneShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_DEL_ZONE_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "DelZoneShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ情報削除SQLの構築・発行</remarks>
    Private Function DelTouSituZoneChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_DEL_TOU_SITU_ZONE_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "DelTouSituZoneChkM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ情報新規登録SQLの構築・発行</remarks>
    Private Function InsertTouSituZoneChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140_TOU_SITU_ZONE_CHK")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_INSERT_TOU_SITU_ZONE_CHK, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTouSituZoneChkParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM140DAC", "InsertTouSituZoneChkM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ消防情報削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ZONEマスタ消防情報削除・復活SQLの構築・発行</remarks>
    Private Function DeleteZoneShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_DELETE_ZONE_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "DeleteZoneShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ消防情報削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ情報削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTouSituZoneChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM140IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM140DAC.SQL_DELETE_TOU_SITU_ZONE_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM140DAC", "DeleteTouSituZoneChkM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

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
    ''' パラメータ設定モジュール(ZONEマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))

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

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_NM", .Item("ZONE_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_KB", .Item("ZONE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_KB", .Item("ONDO_CTL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO", .Item("ONDO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAX_ONDO_UP", .Item("MAX_ONDO_UP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MINI_ONDO_DOWN", .Item("MINI_ONDO_DOWN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_FLG", .Item("ONDO_CTL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOZEI_KB", .Item("HOZEI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YAKUJI_YN", .Item("YAKUJI_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_YN", .Item("DOKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GASS_YN", .Item("GASS_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))        '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TSUBO_AM", .Item("TSUBO_AM").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DAC_SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DAC_SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(ZONE消防情報Ｍ登録時))
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(ZONE消防情報Ｍ登録時))
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row.Item("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_ZONEマスタ消防情報用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZoneShoboParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", .Item("SHOBO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KYOKA_DATE", .Item("WH_KYOKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BAISU", Me.FormatNumValue(.Item("BAISU").ToString()), DBDataType.NUMERIC))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_棟室ゾーンチェックマスタ情報用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouSituZoneChkParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", .Item("KBN_GROUP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_CD", .Item("KBN_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_NM1", .Item("KBN_NM1").ToString(), DBDataType.NVARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region

#End Region

#End Region

End Class
