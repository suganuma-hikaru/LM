' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM540H : 棟マスタメンテナンス
'  作  成  者       :  [narita]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM540DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM540DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(TOU.WH_CD)                AS SELECT_CNT                " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                                    " & vbNewLine

    ''' <summary>
    ''' M_TOUデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                " & vbNewLine _
                                            & "	      TOU.NRS_BR_CD                  AS NRS_BR_CD                     " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                AS NRS_BR_NM                     " & vbNewLine _
                                            & "	     ,TOU.WH_CD                      AS WH_CD                         " & vbNewLine _
                                            & "	     ,SOKO.WH_NM                     AS WH_NM                         " & vbNewLine _
                                            & "	     ,TOU.TOU_NO                     AS TOU_NO                        " & vbNewLine _
                                            & "	     ,TOU.TOU_NM                     AS TOU_NM                        " & vbNewLine _
                                            & "	     ,TOU.SOKO_KB                    AS SOKO_KB                       " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                   AS SOKO_NM                       " & vbNewLine _
                                            & "	     ,TOU.HOZEI_KB                   AS HOZEI_KB                      " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                   AS HOZEI_NM                      " & vbNewLine _
                                            & "	     ,TOU.HOKAN_KANO_M3              AS HOKAN_KANO_M3                 " & vbNewLine _
                                            & "	     ,TOU.HOKAN_KANO_KG              AS HOKAN_KANO_KG                 " & vbNewLine _
                                            & "	     ,TOU.CHOZO_MAX_QTY              AS CHOZO_MAX_QTY                 " & vbNewLine _
                                            & "	     ,TOU.CHOZO_MAX_BAISU            AS CHOZO_MAX_BAISU               " & vbNewLine _
                                            & "	     ,TOU.ONDO_CTL_KB                AS ONDO_CTL_KB                   " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                   AS ONDO_CTL_NM                   " & vbNewLine _
                                            & "	     ,TOU.AREA                       AS AREA                          " & vbNewLine _
                                            & "	     ,TOU.FCT_MGR                    AS FCT_MGR                       " & vbNewLine _
                                            & "	     ,USER4.USER_NM                  AS FCT_MGR_NM                    " & vbNewLine _
                                            & "	     ,TOU.JISYATASYA_KB              AS JISYATASYA_KB                 " & vbNewLine _
                                            & "	     ,KBN8.KBN_NM1                   AS JISYATASYA_KB_NM              " & vbNewLine _
                                            & "	     ,TOU.SYS_ENT_DATE               AS SYS_ENT_DATE                  " & vbNewLine _
                                            & "	     ,USER1.USER_NM                  AS SYS_ENT_USER_NM               " & vbNewLine _
                                            & "	     ,TOU.SYS_UPD_DATE               AS SYS_UPD_DATE                  " & vbNewLine _
                                            & "	     ,TOU.SYS_UPD_TIME               AS SYS_UPD_TIME                  " & vbNewLine _
                                            & "	     ,USER2.USER_NM                  AS SYS_UPD_USER_NM               " & vbNewLine _
                                            & "	     ,TOU.SYS_DEL_FLG                AS SYS_DEL_FLG                   " & vbNewLine _
                                            & "	     ,KBN7.KBN_NM1                   AS SYS_DEL_NM                    " & vbNewLine

    ''' <summary>
    ''' M_TOUデータ抽出用(SUM)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SUM_DATA As String = "SELECT                                                           " & vbNewLine _
                                         & "          SUM(CBM)                 AS HOKAN_KANO_M3              " & vbNewLine _
                                         & "	     ,SUM(MAX_CAPA_KG_QTY)     AS HOKAN_KANO_KG              " & vbNewLine _
                                         & "FROM $LM_MST$..M_TOU_SITU                                        " & vbNewLine _
                                         & "WHERE WH_CD    = @WH_CD                                          " & vbNewLine _
                                         & "  AND TOU_NO   = @TOU_NO                                         " & vbNewLine
    ''' <summary>
    ''' M_TOU_SHOBOデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                     " & vbNewLine _
                                            & "	      TOU_SHOBO.NRS_BR_CD                      AS NRS_BR_CD                 " & vbNewLine _
                                            & "	     ,TOU_SHOBO.WH_CD                          AS WH_CD                     " & vbNewLine _
                                            & "	     ,TOU_SHOBO.TOU_NO                         AS TOU_NO                    " & vbNewLine _
                                            & "	     ,TOU_SHOBO.SHOBO_CD                       AS SHOBO_CD                  " & vbNewLine _
                                            & "	     ,TOU_SHOBO.WH_KYOKA_DATE                  AS WH_KYOKA_DATE             " & vbNewLine _
                                            & "	     ,TOU_SHOBO.BAISU                          AS BAISU                     " & vbNewLine _
                                            & "	     ,SHOBO.HINMEI                             AS HINMEI                    " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                             AS TOKYU                     " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                             AS SHUBETU                   " & vbNewLine _
                                            & "	     ,'1'                                      AS UPD_FLG                   " & vbNewLine _
                                            & "	     ,TOU_SHOBO.SYS_DEL_FLG                    AS SYS_DEL_FLG               " & vbNewLine


    ''' <summary>
    ''' M_TOU_CHKデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA4 As String = " SELECT                                           " & vbNewLine _
                                            & "       TOU_CHK.NRS_BR_CD       AS NRS_BR_CD        " & vbNewLine _
                                            & "      ,TOU_CHK.WH_CD           AS WH_CD            " & vbNewLine _
                                            & "      ,TOU_CHK.TOU_NO          AS TOU_NO           " & vbNewLine _
                                            & "      ,TOU_CHK.KBN_GROUP_CD    AS KBN_GROUP_CD     " & vbNewLine _
                                            & "      ,TOU_CHK.KBN_CD          AS KBN_CD           " & vbNewLine _
                                            & "      ,TOU_CHK.KBN_NM1         AS KBN_NM1          " & vbNewLine _
                                            & "      ,'1'                     AS UPD_FLG          " & vbNewLine _
                                            & "      ,TOU_CHK.SYS_DEL_FLG     AS SYS_DEL_FLG      " & vbNewLine

    ''' <summary>
    ''' M_TOU_SITU(配下に反映時)データ抽出用Select,from,where,Order by句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA5 As String = " SELECT                                                                     " & vbNewLine _
                                            & "	      TOU_SITU.NRS_BR_CD                 AS NRS_BR_CD                       " & vbNewLine _
                                            & "	     ,TOU_SITU.WH_CD                     AS WH_CD                           " & vbNewLine _
                                            & "	     ,TOU_SITU.TOU_NO                    AS TOU_NO                          " & vbNewLine _
                                            & "	     ,TOU_SITU.SITU_NO                   AS SITU_NO                         " & vbNewLine _
                                            & "  FROM                                                                       " & vbNewLine _
                                            & "       $LM_MST$..M_TOU_SITU AS TOU_SITU                                      " & vbNewLine _
                                            & "  WHERE                                                                      " & vbNewLine _
                                            & "       TOU_SITU.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                            & "  AND  TOU_SITU.WH_CD = @WH_CD                                               " & vbNewLine _
                                            & "  AND  TOU_SITU.TOU_NO = @TOU_NO                                             " & vbNewLine _
                                            & "  ORDER BY                                                                   " & vbNewLine _
                                            & "       TOU_SITU.SITU_NO                                                      " & vbNewLine

    ''' <summary>
    ''' M_ZONE(配下に反映時)データ抽出用Select,from,where,Order by句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA6 As String = " SELECT                    	                                                   " & vbNewLine _
                                            & "	      ZONE.NRS_BR_CD                                 AS NRS_BR_CD              " & vbNewLine _
                                            & "	     ,ZONE.WH_CD                                     AS WH_CD                  " & vbNewLine _
                                            & "	     ,ZONE.TOU_NO                                    AS TOU_NO                 " & vbNewLine _
                                            & "	     ,ZONE.SITU_NO                                   AS SITU_NO                " & vbNewLine _
                                            & "	     ,ZONE.ZONE_CD                                   AS ZONE_CD                " & vbNewLine _
                                            & "  FROM                                                                          " & vbNewLine _
                                            & "       $LM_MST$..M_ZONE AS ZONE                                                 " & vbNewLine _
                                            & "  WHERE                                                                         " & vbNewLine _
                                            & "       ZONE.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                            & "  AND  ZONE.WH_CD = @WH_CD                                                      " & vbNewLine _
                                            & "  AND  ZONE.TOU_NO = @TOU_NO                                                    " & vbNewLine _
                                            & "  ORDER BY                                                                      " & vbNewLine _
                                            & "       ZONE.SITU_NO                                                             " & vbNewLine _
                                            & "      ,ZONE.ZONE_CD                                                             " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU AS TOU                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR           " & vbNewLine _
                                          & "        ON TOU.NRS_BR_CD       = NRSBR.NRS_BR_CD             " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SOKO  AS SOKO              " & vbNewLine _
                                          & "        ON TOU.WH_CD           = SOKO.WH_CD                  " & vbNewLine _
                                          & "       AND SOKO.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1            " & vbNewLine _
                                          & "        ON TOU.SOKO_KB         = KBN1.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD        = 'S015'                 " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2            " & vbNewLine _
                                          & "        ON TOU.HOZEI_KB        = KBN2.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD        = 'H001'                 " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3            " & vbNewLine _
                                          & "        ON TOU.ONDO_CTL_KB     = KBN3.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD        = 'O002'                 " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN7            " & vbNewLine _
                                          & "        ON TOU.SYS_DEL_FLG     = KBN7.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN7.KBN_GROUP_CD        = 'S051'                 " & vbNewLine _
                                          & "       AND KBN7.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN8            " & vbNewLine _
                                          & "        ON TOU.SYS_DEL_FLG     = KBN8.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN7.KBN_GROUP_CD        = 'J004'                 " & vbNewLine _
                                          & "       AND KBN7.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER AS USER1              " & vbNewLine _
                                          & "        ON TOU.SYS_ENT_USER    = USER1.USER_CD               " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER2             " & vbNewLine _
                                          & "       ON TOU.SYS_UPD_USER     = USER2.USER_CD               " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT JOIN $LM_MST$..S_USER          AS USER4           " & vbNewLine _
                                          & "        ON TOU.FCT_MGR         = USER4.USER_CD               " & vbNewLine _
                                          & "       AND USER4.SYS_DEL_FLG        = '0'                    " & vbNewLine

    Private Const SQL_FROM_DATA2 As String = "FROM                                                                      " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_SHOBO AS TOU_SHOBO                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SHOBO AS SHOBO                           " & vbNewLine _
                                          & "       ON TOU_SHOBO.SHOBO_CD     = SHOBO.SHOBO_CD                          " & vbNewLine _
                                          & "       AND SHOBO.SYS_DEL_FLG          = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                          " & vbNewLine _
                                          & "        ON SHOBO.KIKEN_TOKYU          = KBN1.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD          = 'S002'                             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG           = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                          " & vbNewLine _
                                          & "        ON SHOBO.SYU = KBN2.KBN_CD                                         " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD          = 'S022'                             " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG           = '0'                                " & vbNewLine



    Private Const SQL_FROM_DATA4 As String = "FROM                                                  " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_CHK AS TOU_CHK   " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(M_TOU)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                         " & vbNewLine _
                                         & "     TOU.NRS_BR_CD                               " & vbNewLine _
                                         & "    ,TOU.WH_CD                                   " & vbNewLine _
                                         & "    ,TOU.TOU_NO                                  " & vbNewLine _

    ''' <summary>
    ''' ORDER BY(M_TOU_SHOBO)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                         " & vbNewLine _
                                         & "     TOU_SHOBO.NRS_BR_CD                          " & vbNewLine _
                                         & "    ,TOU_SHOBO.WH_CD                              " & vbNewLine _
                                         & "    ,TOU_SHOBO.TOU_NO                             " & vbNewLine _
                                         & "    ,TOU_SHOBO.SHOBO_CD                           " & vbNewLine


    ''' <summary>
    ''' ORDER BY(M_TOU_CHK)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY4 As String = "ORDER BY                     " & vbNewLine _
                                         & "     TOU_CHK.NRS_BR_CD        " & vbNewLine _
                                         & "    ,TOU_CHK.WH_CD            " & vbNewLine _
                                         & "    ,TOU_CHK.TOU_NO           " & vbNewLine _
                                         & "    ,TOU_CHK.KBN_GROUP_CD     " & vbNewLine _
                                         & "    ,TOU_CHK.KBN_CD           " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 棟マスタ存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_Tou As String = "SELECT                             " & vbNewLine _
                                            & "   COUNT(WH_CD)  AS REC_CNT     " & vbNewLine _
                                            & "FROM $LM_MST$..M_TOU            " & vbNewLine _
                                            & "WHERE WH_CD    = @WH_CD         " & vbNewLine _
                                            & "  AND TOU_NO   = @TOU_NO        " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    ''' <summary>
    ''' 新規登録SQL（M_TOU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_TOU             " & vbNewLine _
                                       & "(                                       " & vbNewLine _
                                       & "       NRS_BR_CD                        " & vbNewLine _
                                       & "      ,WH_CD                            " & vbNewLine _
                                       & "      ,TOU_NO                           " & vbNewLine _
                                       & "      ,TOU_NM                           " & vbNewLine _
                                       & "      ,SOKO_KB                          " & vbNewLine _
                                       & "      ,HOZEI_KB                         " & vbNewLine _
                                       & "      ,CHOZO_MAX_QTY                    " & vbNewLine _
                                       & "      ,CHOZO_MAX_BAISU                  " & vbNewLine _
                                       & "      ,ONDO_CTL_KB                      " & vbNewLine _
                                       & "      ,AREA                             " & vbNewLine _
                                       & "      ,FCT_MGR                          " & vbNewLine _
                                       & "      ,HOKAN_KANO_M3                    " & vbNewLine _
                                       & "      ,HOKAN_KANO_KG                    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                     " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                     " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                     " & vbNewLine _
                                       & "      ,SYS_ENT_USER                     " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                     " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                     " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                     " & vbNewLine _
                                       & "      ,SYS_UPD_USER                     " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                      " & vbNewLine _
                                       & "      ) VALUES (                        " & vbNewLine _
                                       & "      @NRS_BR_CD                        " & vbNewLine _
                                       & "      ,@WH_CD                           " & vbNewLine _
                                       & "      ,@TOU_NO                          " & vbNewLine _
                                       & "      ,@TOU_NM                          " & vbNewLine _
                                       & "      ,@SOKO_KB                         " & vbNewLine _
                                       & "      ,@HOZEI_KB                        " & vbNewLine _
                                       & "      ,@CHOZO_MAX_QTY                   " & vbNewLine _
                                       & "      ,@CHOZO_MAX_BAISU                 " & vbNewLine _
                                       & "      ,@ONDO_CTL_KB                     " & vbNewLine _
                                       & "      ,@AREA                            " & vbNewLine _
                                       & "      ,@FCT_MGR                         " & vbNewLine _
                                       & "      ,@HOKAN_KANO_M3                   " & vbNewLine _
                                       & "      ,@HOKAN_KANO_KG                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         	          " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         	          " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         	          " & vbNewLine _
                                       & "      ,@SYS_ENT_USER         	          " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         	          " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME         	          " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         	          " & vbNewLine _
                                       & "      ,@SYS_UPD_USER         	          " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         	          " & vbNewLine _
                                       & ")                                       " & vbNewLine

    ''' <summary>
    ''' 新規登録SQL（M_TOU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TOU_SHOBO As String = "INSERT INTO $LM_MST$..M_TOU_SHOBO               " & vbNewLine _
                                       & "(                                                         " & vbNewLine _
                                       & "       NRS_BR_CD                                          " & vbNewLine _
                                       & "      ,WH_CD                                              " & vbNewLine _
                                       & "      ,TOU_NO                                             " & vbNewLine _
                                       & "      ,SHOBO_CD                                           " & vbNewLine _
                                       & "      ,WH_KYOKA_DATE                                      " & vbNewLine _
                                       & "      ,BAISU                                              " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                                       " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                                       " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                                       " & vbNewLine _
                                       & "      ,SYS_ENT_USER                                       " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                                       " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                                       " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                                       " & vbNewLine _
                                       & "      ,SYS_UPD_USER                                       " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                                        " & vbNewLine _
                                       & "      ) VALUES (                                          " & vbNewLine _
                                       & "       @NRS_BR_CD                                         " & vbNewLine _
                                       & "      ,@WH_CD                                             " & vbNewLine _
                                       & "      ,@TOU_NO                                            " & vbNewLine _
                                       & "      ,@SHOBO_CD                                          " & vbNewLine _
                                       & "      ,@WH_KYOKA_DATE                                     " & vbNewLine _
                                       & "      ,@BAISU                                             " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                                      " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                                      " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                                      " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                                      " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                                      " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                                      " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                                      " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                                      " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                                       " & vbNewLine _
                                       & ")                                                         " & vbNewLine


    ''' <summary>
    ''' 新規登録SQL（M_TOU_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TOU_CHK As String = "INSERT INTO $LM_MST$..M_TOU_CHK                       " & vbNewLine _
                                       & "(                                                             " & vbNewLine _
                                       & "       NRS_BR_CD                                              " & vbNewLine _
                                       & "      ,WH_CD                                                  " & vbNewLine _
                                       & "      ,TOU_NO                                                 " & vbNewLine _
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

#End Region

#Region "Delete"

    ''' <summary>
    ''' 物理削除SQL（M_TOU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_SHOBO As String = "DELETE FROM $LM_MST$..M_TOU_SHOBO              " & vbNewLine _
                                       & "WHERE   WH_CD                 = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine


    ''' <summary>
    ''' 物理削除SQL（M_TOU_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_CHK As String = "DELETE FROM $LM_MST$..M_TOU_CHK   " & vbNewLine _
                                            & "WHERE   WH_CD                 = @WH_CD                " & vbNewLine _
                                            & "     AND TOU_NO               = @TOU_NO               " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_TOU_SITU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_SITU_SHOBO As String = "DELETE FROM $LM_MST$..M_TOU_SITU_SHOBO          " & vbNewLine _
                                            & "WHERE    WH_CD                 = @WH_CD                " & vbNewLine _
                                            & "     AND TOU_NO               = @TOU_NO                " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_ZONE_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_M_ZONE_SHOBO As String = "DELETE FROM $LM_MST$..M_ZONE_SHOBO                " & vbNewLine _
                                            & "WHERE    WH_CD                 = @WH_CD                " & vbNewLine _
                                            & "     AND TOU_NO               = @TOU_NO                " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_TOU_SITU_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_M_TOU_SITU_ZONE_CHK As String = "DELETE FROM $LM_MST$..M_TOU_SITU_ZONE_CHK   " & vbNewLine _
                                            & "WHERE    WH_CD                = @WH_CD                  " & vbNewLine _
                                            & "     AND TOU_NO               = @TOU_NO                 " & vbNewLine _
                                            & "     AND KBN_GROUP_CD         = @KBN_GROUP_CD           " & vbNewLine

#End Region

#Region "UPDATE"

    ''' <summary>
    ''' 更新SQL（M_TOU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_TOU SET                          " & vbNewLine _
                                       & "        NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
                                       & "      , WH_CD               = @WH_CD                " & vbNewLine _
                                       & "      , TOU_NO              = @TOU_NO               " & vbNewLine _
                                       & "      , TOU_NM              = @TOU_NM               " & vbNewLine _
                                       & "      , SOKO_KB             = @SOKO_KB              " & vbNewLine _
                                       & "      , HOZEI_KB            = @HOZEI_KB             " & vbNewLine _
                                       & "      , CHOZO_MAX_QTY       = @CHOZO_MAX_QTY        " & vbNewLine _
                                       & "      , CHOZO_MAX_BAISU     = @CHOZO_MAX_BAISU      " & vbNewLine _
                                       & "      , ONDO_CTL_KB         = @ONDO_CTL_KB          " & vbNewLine _
                                       & "      , AREA                = @AREA                 " & vbNewLine _
                                       & "      , FCT_MGR             = @FCT_MGR              " & vbNewLine _
                                       & "      , HOKAN_KANO_M3       = @HOKAN_KANO_M3        " & vbNewLine _
                                       & "      , HOKAN_KANO_KG       = @HOKAN_KANO_KG        " & vbNewLine _
                                       & "      , SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                       & "      , SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                       & "      , SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                       & "      , SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                       & "WHERE WH_CD                 = @WH_CD                " & vbNewLine _
                                       & "  AND TOU_NO                = @TOU_NO               " & vbNewLine

#End Region

#Region "HAIKA_INSERT"

    ''' <summary>
    ''' 配下に反映SQL（M_TOU_SITU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TOU_SITU_SHOBO As String = "INSERT INTO $LM_MST$..M_TOU_SITU_SHOBO     " & vbNewLine _
                                       & "(                                               " & vbNewLine _
                                       & "        NRS_BR_CD                               " & vbNewLine _
                                       & "      , WH_CD                                   " & vbNewLine _
                                       & "      , TOU_NO                                  " & vbNewLine _
                                       & "      , SITU_NO                                 " & vbNewLine _
                                       & "      , SHOBO_CD                                " & vbNewLine _
                                       & "      , WH_KYOKA_DATE                           " & vbNewLine _
                                       & "      , BAISU                                   " & vbNewLine _
                                       & "      , SYS_ENT_DATE                            " & vbNewLine _
                                       & "      , SYS_ENT_TIME                            " & vbNewLine _
                                       & "      , SYS_ENT_PGID                            " & vbNewLine _
                                       & "      , SYS_ENT_USER                            " & vbNewLine _
                                       & "      , SYS_UPD_DATE                            " & vbNewLine _
                                       & "      , SYS_UPD_TIME                            " & vbNewLine _
                                       & "      , SYS_UPD_PGID                            " & vbNewLine _
                                       & "      , SYS_UPD_USER                            " & vbNewLine _
                                       & "      , SYS_DEL_FLG                             " & vbNewLine _
                                       & "      ) VALUES (                                " & vbNewLine _
                                       & "       @NRS_BR_CD                               " & vbNewLine _
                                       & "      ,@WH_CD                                   " & vbNewLine _
                                       & "      ,@TOU_NO                                  " & vbNewLine _
                                       & "      ,@SITU_NO                                 " & vbNewLine _
                                       & "      ,@SHOBO_CD                                " & vbNewLine _
                                       & "      ,@WH_KYOKA_DATE                           " & vbNewLine _
                                       & "      ,@BAISU                                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                            " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                            " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                            " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                            " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                            " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                            " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                             " & vbNewLine _
                                       & ")                                               " & vbNewLine

    ''' <summary>
    ''' 配下に反映SQL（M_ZONE_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_ZONE_SHOBO As String = "INSERT INTO $LM_MST$..M_ZONE_SHOBO   " & vbNewLine _
                                       & "(                                               " & vbNewLine _
                                       & "        NRS_BR_CD                               " & vbNewLine _
                                       & "      , WH_CD                                   " & vbNewLine _
                                       & "      , TOU_NO                                  " & vbNewLine _
                                       & "      , SITU_NO                                 " & vbNewLine _
                                       & "      , ZONE_CD                                 " & vbNewLine _
                                       & "      , SHOBO_CD                                " & vbNewLine _
                                       & "      , WH_KYOKA_DATE                           " & vbNewLine _
                                       & "      , BAISU                                  " & vbNewLine _
                                       & "      , SYS_ENT_DATE                            " & vbNewLine _
                                       & "      , SYS_ENT_TIME                            " & vbNewLine _
                                       & "      , SYS_ENT_PGID                            " & vbNewLine _
                                       & "      , SYS_ENT_USER                            " & vbNewLine _
                                       & "      , SYS_UPD_DATE                            " & vbNewLine _
                                       & "      , SYS_UPD_TIME                            " & vbNewLine _
                                       & "      , SYS_UPD_PGID                            " & vbNewLine _
                                       & "      , SYS_UPD_USER                            " & vbNewLine _
                                       & "      , SYS_DEL_FLG                             " & vbNewLine _
                                       & "      ) VALUES (                                " & vbNewLine _
                                       & "       @NRS_BR_CD                               " & vbNewLine _
                                       & "      ,@WH_CD                                   " & vbNewLine _
                                       & "      ,@TOU_NO                                  " & vbNewLine _
                                       & "      ,@SITU_NO                                 " & vbNewLine _
                                       & "      ,@ZONE_CD                                 " & vbNewLine _
                                       & "      ,@SHOBO_CD                                " & vbNewLine _
                                       & "      ,@WH_KYOKA_DATE                           " & vbNewLine _
                                       & "      ,@BAISU                                  " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                            " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                            " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                            " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                            " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                            " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                            " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                             " & vbNewLine _
                                       & ")                                               " & vbNewLine
    ''' <summary>
    ''' 配下に反映SQL（M_TOU_SITU_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TOU_SITU_CHK As String = "INSERT INTO $LM_MST$..M_TOU_SITU_ZONE_CHK   " & vbNewLine _
                                       & "(                                               " & vbNewLine _
                                       & "        NRS_BR_CD                               " & vbNewLine _
                                       & "      , WH_CD                                   " & vbNewLine _
                                       & "      , TOU_NO                                  " & vbNewLine _
                                       & "      , SITU_NO                                 " & vbNewLine _
                                       & "      , KBN_CD                                  " & vbNewLine _
                                       & "      , KBN_GROUP_CD                            " & vbNewLine _
                                       & "      , KBN_NM1                                 " & vbNewLine _
                                       & "      , SYS_ENT_DATE                            " & vbNewLine _
                                       & "      , SYS_ENT_TIME                            " & vbNewLine _
                                       & "      , SYS_ENT_PGID                            " & vbNewLine _
                                       & "      , SYS_ENT_USER                            " & vbNewLine _
                                       & "      , SYS_UPD_DATE                            " & vbNewLine _
                                       & "      , SYS_UPD_TIME                            " & vbNewLine _
                                       & "      , SYS_UPD_PGID                            " & vbNewLine _
                                       & "      , SYS_UPD_USER                            " & vbNewLine _
                                       & "      , SYS_DEL_FLG                             " & vbNewLine _
                                       & "      ) VALUES (                                " & vbNewLine _
                                       & "       @NRS_BR_CD                               " & vbNewLine _
                                       & "      ,@WH_CD                                   " & vbNewLine _
                                       & "      ,@TOU_NO                                  " & vbNewLine _
                                       & "      ,@SITU_NO                                 " & vbNewLine _
                                       & "      ,@KBN_CD                                  " & vbNewLine _
                                       & "      ,@KBN_GROUP_CD                            " & vbNewLine _
                                       & "      ,@KBN_NM1                                 " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                            " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                            " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                            " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                            " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                            " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                            " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                             " & vbNewLine _
                                       & ")                                               " & vbNewLine

    ''' <summary>
    ''' 配下に反映SQL（M_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_ZONE_CHK As String = "INSERT INTO $LM_MST$..M_TOU_SITU_ZONE_CHK   " & vbNewLine _
                                       & "(                                               " & vbNewLine _
                                       & "        NRS_BR_CD                               " & vbNewLine _
                                       & "      , WH_CD                                   " & vbNewLine _
                                       & "      , TOU_NO                                  " & vbNewLine _
                                       & "      , SITU_NO                                 " & vbNewLine _
                                       & "      , ZONE_CD                                 " & vbNewLine _
                                       & "      , KBN_CD                                  " & vbNewLine _
                                       & "      , KBN_GROUP_CD                            " & vbNewLine _
                                       & "      , KBN_NM1                                 " & vbNewLine _
                                       & "      , SYS_ENT_DATE                            " & vbNewLine _
                                       & "      , SYS_ENT_TIME                            " & vbNewLine _
                                       & "      , SYS_ENT_PGID                            " & vbNewLine _
                                       & "      , SYS_ENT_USER                            " & vbNewLine _
                                       & "      , SYS_UPD_DATE                            " & vbNewLine _
                                       & "      , SYS_UPD_TIME                            " & vbNewLine _
                                       & "      , SYS_UPD_PGID                            " & vbNewLine _
                                       & "      , SYS_UPD_USER                            " & vbNewLine _
                                       & "      , SYS_DEL_FLG                             " & vbNewLine _
                                       & "      ) VALUES (                                " & vbNewLine _
                                       & "       @NRS_BR_CD                               " & vbNewLine _
                                       & "      ,@WH_CD                                   " & vbNewLine _
                                       & "      ,@TOU_NO                                  " & vbNewLine _
                                       & "      ,@SITU_NO                                 " & vbNewLine _
                                       & "      ,@ZONE_CD                                 " & vbNewLine _
                                       & "      ,@KBN_CD                                  " & vbNewLine _
                                       & "      ,@KBN_GROUP_CD                            " & vbNewLine _
                                       & "      ,@KBN_NM1                                 " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                            " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                            " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                            " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                            " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                            " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                            " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                             " & vbNewLine _
                                       & ")                                               " & vbNewLine
#End Region

#Region "HAIKA_UPDATE"

    ''' <summary>
    ''' 更新SQL（M_TOU_SITU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOU_SITU As String = "UPDATE $LM_MST$..M_TOU_SITU SET                     " & vbNewLine _
                                                & "        SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                                & "      , SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                                & "      , SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                                & "      , SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                                & "      , DOKU_KB             = CASE WHEN @DOKU_KB = 'XX' THEN  DOKU_KB  " & vbNewLine _
                                                & "                                   ELSE @DOKU_KB  END                  " & vbNewLine _
                                                & "WHERE WH_CD                 = @WH_CD                " & vbNewLine _
                                                & "  AND TOU_NO                = @TOU_NO               " & vbNewLine _
                                                & "  AND SYS_DEL_FLG           = '0'                   " & vbNewLine

    ''' <summary>
    ''' 更新SQL（M_ZONE）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_ZONE As String = "UPDATE $LM_MST$..M_ZONE SET                         " & vbNewLine _
                                            & "        SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                            & "      , SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                            & "      , SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                            & "      , SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                            & "WHERE WH_CD                 = @WH_CD                " & vbNewLine _
                                            & "  AND TOU_NO                = @TOU_NO               " & vbNewLine _
                                            & "  AND SYS_DEL_FLG           = '0'                   " & vbNewLine

#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL（M_TOU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_TOU SET                            " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_TOU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TOU_SHOBO As String = "UPDATE $LM_MST$..M_TOU_SHOBO SET            " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_TOU_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TOU_CHK As String = "UPDATE $LM_MST$..M_TOU_CHK SET                    " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine

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

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList2 As ArrayList

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList3 As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 棟マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM540DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 棟マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM540DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM540DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectListData", cmd)

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
        map.Add("TOU_NM", "TOU_NM")
        map.Add("SOKO_KB", "SOKO_KB")
        map.Add("SOKO_NM", "SOKO_NM")
        map.Add("HOZEI_KB", "HOZEI_KB")
        map.Add("HOZEI_NM", "HOZEI_NM")
        map.Add("CHOZO_MAX_QTY", "CHOZO_MAX_QTY")
        map.Add("CHOZO_MAX_BAISU", "CHOZO_MAX_BAISU")
        map.Add("ONDO_CTL_KB", "ONDO_CTL_KB")
        map.Add("ONDO_CTL_NM", "ONDO_CTL_NM")
        map.Add("AREA", "AREA")
        map.Add("FCT_MGR", "FCT_MGR")
        map.Add("FCT_MGR_NM", "FCT_MGR_NM")
        map.Add("HOKAN_KANO_M3", "HOKAN_KANO_M3")
        map.Add("HOKAN_KANO_KG", "HOKAN_KANO_KG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM540OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU)
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
                andstr.Append(" (TOU.SYS_DEL_FLG = @SYS_DEL_FLG  OR TOU.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (TOU.NRS_BR_CD = @NRS_BR_CD OR TOU.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU.WH_CD = @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU.TOU_NM LIKE @TOU_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SOKO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU.SOKO_KB = @SOKO_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HOZEI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU.HOZEI_KB = @HOZEI_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOZEI_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ONDO_CTL_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU.ONDO_CTL_KB = @ONDO_CTL_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_KB", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' M_TOUデータ抽出用(SUM)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>更新時 M_TOUデータ抽出用(SUM)SQLの構築・発行</remarks>
    Private Function SelectListDataSum(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.CHAR))


        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SUM_DATA)      'SQL構築(データ抽出用Select,from,where,Order by句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectListDataSum", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("HOKAN_KANO_M3", "HOKAN_KANO_M3")
        map.Add("HOKAN_KANO_KG", "HOKAN_KANO_KG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM540_SUM")

        Return ds

    End Function

    ''' <summary>
    ''' 棟マスタ消防更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM540DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM540DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("WH_KYOKA_DATE", "WH_KYOKA_DATE")
        map.Add("BAISU", "BAISU")
        map.Add("HINMEI", "HINMEI")
        map.Add("TOKYU", "TOKYU")
        map.Add("SHUBETU", "SHUBETU")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM540_TOU_SHOBO")

        Return ds

    End Function



    ''' <summary>
    ''' 棟チェックマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟チェックマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData4(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SELECT_DATA4)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM540DAC.SQL_FROM_DATA4)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL4()                   '条件設定
        Me._StrSql.Append(LMM540DAC.SQL_ORDER_BY4)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectListData4", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM540_TOU_CHK")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SHOBO)
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
                andstr.Append(" TOU_SHOBO.WH_CD LIKE @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SHOBO.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_CHK)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL4()

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
                andstr.Append(" TOU_CHK.WH_CD LIKE @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_CHK.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If



        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SelectListData5(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SELECT_DATA5)      'SQL構築(データ抽出用Select,from,where,Order by句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetTouSituParam()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectListData5", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM540_HAIKA_TOU_SITU")

        Return ds

    End Function
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_ZONE)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SelectListData6(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM540DAC.SQL_SELECT_DATA6)      'SQL構築(データ抽出用Select,from,where,Order by句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetZoneParam()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectListData6", cmd)

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

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM540_HAIKA_ZONE")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 棟マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectTouM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM540DAC.SQL_EXIT_Tou)
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

        MyBase.Logger.WriteSQLLog("LMM540DAC", "SelectTouM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            reader.Close()
            MyBase.SetMessage("E011")
        Else
            reader.Close()
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 棟マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistTouM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_EXIT_Tou, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "CheckExistTouM", cmd)

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
    ''' 棟マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTouM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertTouM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟マスタ消防情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防情報新規登録SQLの構築・発行</remarks>
    Private Function DelTouShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DEL_TOU_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DelTouShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function


    ''' <summary>
    ''' 棟チェックマスタ情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟チェックマスタ情報削除SQLの構築・発行</remarks>
    Private Function DelTouChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DEL_TOU_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DelTouChkM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟マスタ消防情報新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防情報新規登録SQLの構築・発行</remarks>
    Private Function InsertTouShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540_TOU_SHOBO")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT_TOU_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTouShoboParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertTouShoboM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 棟チェックマスタ情報新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟チェックマスタ情報新規登録SQLの構築・発行</remarks>
    Private Function InsertTouChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540_TOU_CHK")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT_TOU_CHK, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTouChkParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertTouChkM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "Update"

    ''' <summary>
    ''' 棟マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateTouM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM540DAC.SQL_UPDATE _
                                                                                     , LMM540DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "UpdateTouM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateTouSituM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_UPDATE_TOU_SITU _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "UpdateTouSituM", cmd)

        '更新
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateZoneM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_UPDATE_ZONE _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "UpdateZoneM", cmd)

        '更新
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "Delete"

    ''' <summary>
    ''' 棟マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTouM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DeleteTouM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟マスタ消防情報削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防情報削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTouShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DELETE_TOU_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DeleteTouShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟チェックマスタ消防情報削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟チェックマスタ情報削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTouChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DELETE_TOU_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DeleteTouChkM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防情報削除SQLの構築・発行</remarks>
    Private Function DeleteTouSituShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DEL_TOU_SITU_SHOBO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete2()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DeleteTouSituShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' ゾーンマスタ消防情報削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防情報削除SQLの構築・発行</remarks>
    Private Function DeleteZoneShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DEL_M_ZONE_SHOBO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete3()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DeleteZoneShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟マスタ消防情報削除SQLの構築・発行</remarks>
    Private Function DeleteTouSituZoneChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_DEL_M_TOU_SITU_ZONE_CHK, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete4()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", ds.Tables("LMM540_KBN_CD").Rows(0).Item("KBN_GROUP_CD").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM540DAC", "DeleteZoneShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#End Region

#Region "HAIKA_INS"

    ''' <summary>
    ''' 棟室マスタ消防情報(配下に反映処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ消防情報(配下に反映処理)SQLの構築・発行</remarks>
    Private Function InsertTouSituShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540_TOU_SITU_SHOBO")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT_TOU_SITU_SHOBO, Me._Row.Item("NRS_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTouSituShoboParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertTouSituShoboM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報(配下に反映処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ消防情報(配下に反映処理)SQLの構築・発行</remarks>
    Private Function InsertZoneShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540_ZONE_SHOBO")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT_ZONE_SHOBO, Me._Row.Item("NRS_BR_CD").ToString()))
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

            MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertZoneShoboM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ(配下に反映処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ(配下に反映処理)SQLの構築・発行</remarks>
    Private Function InsertTouSituChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540_TOU_SITU_CHK")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT_TOU_SITU_CHK, Me._Row.Item("NRS_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTouSituChkParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertTouSituChkM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ(配下に反映処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ(配下に反映処理)SQLの構築・発行</remarks>
    Private Function InsertZoneChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM540_ZONE_CHK")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM540DAC.SQL_INSERT_ZONE_CHK, Me._Row.Item("NRS_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetZoneChkParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM540DAC", "InsertZoneChkM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

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
    ''' パラメータ設定モジュール(棟マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))

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
    ''' パラメータ設定モジュール(配下に反映処理_TouSituM)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouSituParam()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
        End With

        'システム項目
        Call Me.SetParamCommonSystemUpd()



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(配下に反映処理_ZoneM)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZoneParam()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))

        End With
        'システム項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録_棟Ｍ)
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

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel2()

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete3()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel3()

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete4()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel3()

        Call Me.SetParamCommonSystemUpd()

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_棟Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NM", .Item("TOU_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_KB", .Item("SOKO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOZEI_KB", .Item("HOZEI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHOZO_MAX_QTY", .Item("CHOZO_MAX_QTY").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHOZO_MAX_BAISU", .Item("CHOZO_MAX_BAISU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_KB", .Item("ONDO_CTL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA", .Item("AREA").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KANO_M3", .Item("HOKAN_KANO_M3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KANO_KG", .Item("HOKAN_KANO_KG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FCT_MGR", .Item("FCT_MGR").ToString(), DBDataType.NVARCHAR))

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", .Item("DOKU_KB").ToString(), DBDataType.CHAR))
        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(配下に反映処理_棟室マスタ消防情報用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouSituShoboParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", .Item("SHOBO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KYOKA_DATE", .Item("WH_KYOKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BAISU", 0, DBDataType.NUMERIC))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(配下に反映処理_ゾーンマスタ消防情報用)
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
            prmList.Add(MyBase.GetSqlParameter("@BAISU", 0, DBDataType.NUMERIC))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(配下に反映処理_棟室ゾーンチェックマスタ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouSituChkParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", .Item("KBN_GROUP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_CD", .Item("KBN_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_NM1", .Item("KBN_NM1").ToString(), DBDataType.NVARCHAR))
        End With

    End Sub



    ''' <summary>
    ''' パラメータ設定モジュール(配下に反映処理_棟室ゾーンチェックマスタ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZoneChkParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

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
    ''' パラメータ設定モジュール(更新_棟マスタ消防情報用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouShoboParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", .Item("SHOBO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KYOKA_DATE", .Item("WH_KYOKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BAISU", Me.FormatNumValue(.Item("BAISU").ToString()), DBDataType.NUMERIC))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_棟チェックマスタ情報用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouChkParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", .Item("KBN_GROUP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_CD", .Item("KBN_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KBN_NM1", .Item("KBN_NM1").ToString(), DBDataType.NVARCHAR))
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
    ''' パラメータ設定モジュール(システム共通項目(配下に反映時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns2()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(担当者別荷主Ｍ登録時))
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
    ''' パラメータ設定モジュール(システム共通項目(担当者別荷主Ｍ更新時))
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目棟室(削除時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel2()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目ゾーン(削除時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel3()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row.Item("ZONE_CD").ToString(), DBDataType.CHAR))
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
