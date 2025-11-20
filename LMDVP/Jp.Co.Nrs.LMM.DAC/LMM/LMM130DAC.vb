' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM130H : 棟室マスタメンテナンス
'  作  成  者       :  [kishi]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM130DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM130DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(TOU_SITU.WH_CD)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                  " & vbNewLine

    ''' <summary>
    ''' M_TOU_SITUデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                    " & vbNewLine _
                                            & "	      TOU_SITU.NRS_BR_CD                 AS NRS_BR_CD                     " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                    AS NRS_BR_NM                     " & vbNewLine _
                                            & "	     ,TOU_SITU.WH_CD                     AS WH_CD                         " & vbNewLine _
                                            & "	     ,SOKO.WH_NM                         AS WH_NM                         " & vbNewLine _
                                            & "	     ,SOKO.WH_KB                         AS WH_KB                         " & vbNewLine _
                                            & "	     ,TOU_SITU.TOU_NO                    AS TOU_NO                        " & vbNewLine _
                                            & "	     ,TOU_SITU.SITU_NO                   AS SITU_NO                       " & vbNewLine _
                                            & "	     ,TOU_SITU.TOU_SITU_NM               AS TOU_SITU_NM                   " & vbNewLine _
                                            & "	     ,TOU_SITU.SOKO_KB                   AS SOKO_KB                       " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                       AS SOKO_NM                       " & vbNewLine _
                                            & "	     ,TOU_SITU.HOZEI_KB                  AS HOZEI_KB                      " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                       AS HOZEI_NM                      " & vbNewLine _
                                            & "	     ,TOU_SITU.CHOZO_MAX_QTY             AS CHOZO_MAX_QTY                 " & vbNewLine _
                                            & "	     ,TOU_SITU.CHOZO_MAX_BAISU           AS CHOZO_MAX_BAISU               " & vbNewLine _
                                            & "	     ,TOU_SITU.ONDO_CTL_KB               AS ONDO_CTL_KB                   " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                       AS ONDO_CTL_NM                   " & vbNewLine _
                                            & "	     ,TOU_SITU.ONDO                      AS ONDO                          " & vbNewLine _
                                            & "	     ,TOU_SITU.MAX_ONDO_UP               AS MAX_ONDO_UP                   " & vbNewLine _
                                            & "	     ,TOU_SITU.MINI_ONDO_DOWN            AS MINI_ONDO_DOWN                " & vbNewLine _
                                            & "	     ,TOU_SITU.ONDO_CTL_FLG              AS ONDO_CTL_FLG                  " & vbNewLine _
                                            & "	     ,KBN4.KBN_NM2                       AS ONDO_CTL_FLG_NM               " & vbNewLine _
                                            & "	     ,TOU_SITU.HAN                       AS HAN                           " & vbNewLine _
                                            & "	     ,TOU_SITU.CBM                       AS CBM                           " & vbNewLine _
                                            & "	     ,TOU_SITU.AREA                      AS AREA                          " & vbNewLine _
                                            & "	     ,TOU_SITU.MX_PLT_QT                 AS MX_PLT_QT                     " & vbNewLine _
                                            & "	     ,TOU_SITU.RACK_YN                   AS RACK_YN                       " & vbNewLine _
                                            & "	     ,KBN5.KBN_NM1                       AS RACK_YN_NM                    " & vbNewLine _
                                            & "	     ,TOU_SITU.FCT_MGR                   AS FCT_MGR                       " & vbNewLine _
                                            & "	     ,USER4.USER_NM                      AS FCT_MGR_NM                    " & vbNewLine _
                                            & "	     ,TOU_SITU.SHOKA_KB                  AS SHOKA_KB                      " & vbNewLine _
                                            & "	     ,KBN6.KBN_NM1                       AS SHOKA_KB_NM                   " & vbNewLine _
                                            & "--要望番号：674 yamanaka 2012.7.5 Start                                    " & vbNewLine _
                                            & "	     ,TOU_SITU.JISYATASYA_KB             AS JISYATASYA_KB                 " & vbNewLine _
                                            & "	     ,KBN8.KBN_NM1                       AS JISYATASYA_KB_NM              " & vbNewLine _
                                            & "--要望番号：674 yamanaka 2012.7.5  End                                     " & vbNewLine _
                                            & "	     ,TOU_SITU.SYS_ENT_DATE              AS SYS_ENT_DATE                  " & vbNewLine _
                                            & "	     ,USER1.USER_NM                      AS SYS_ENT_USER_NM               " & vbNewLine _
                                            & "	     ,TOU_SITU.SYS_UPD_DATE              AS SYS_UPD_DATE                  " & vbNewLine _
                                            & "	     ,TOU_SITU.SYS_UPD_TIME              AS SYS_UPD_TIME                  " & vbNewLine _
                                            & "	     ,USER2.USER_NM                      AS SYS_UPD_USER_NM               " & vbNewLine _
                                            & "	     ,TOU_SITU.SYS_DEL_FLG               AS SYS_DEL_FLG                   " & vbNewLine _
                                            & "	     ,KBN7.KBN_NM1                       AS SYS_DEL_NM                    " & vbNewLine _
                                            & "	     ,TOU_SITU.DOKU_KB                   AS DOKU_KB                       " & vbNewLine _
                                            & "	     ,TOU_SITU.GAS_KANRI_KB              AS GAS_KANRI_KB                  " & vbNewLine _
                                            & "	     ,TOU_SITU.MAX_CAPA_KG_QTY           AS MAX_CAPA_KG_QTY               " & vbNewLine _
                                            & "	     ,TOU_SITU.USER_CD                   AS USER_CD                       " & vbNewLine _
                                            & "	     ,USER3.USER_NM                      AS USER_NM                       " & vbNewLine _
                                            & "	     ,TOU_SITU.USER_CD_SUB               AS USER_CD_SUB                   " & vbNewLine _
                                            & "	     ,USER5.USER_NM                      AS USER_NM_SUB                   " & vbNewLine _
                                            & "	     ,TOU_SITU.TASYA_WH_NM               AS TASYA_WH_NM                   " & vbNewLine _
                                            & "	     ,TOU_SITU.TASYA_ZIP                 AS TASYA_ZIP                     " & vbNewLine _
                                            & "	     ,TOU_SITU.TASYA_AD_1                AS TASYA_AD_1                    " & vbNewLine _
                                            & "	     ,TOU_SITU.TASYA_AD_2                AS TASYA_AD_2                    " & vbNewLine _
                                            & "	     ,TOU_SITU.TASYA_AD_3                AS TASYA_AD_3                    " & vbNewLine _
                                            & "	     ,TOU_SITU.AREA_RENT_HOKAN_AMO       AS AREA_RENT_HOKAN_AMO           " & vbNewLine

    ''' <summary>
    ''' M_TOU_SITU_SHOBOデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                          " & vbNewLine _
                                            & "	      TOU_SITU_SHOBO.NRS_BR_CD                      AS NRS_BR_CD                 " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.WH_CD                          AS WH_CD                     " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.TOU_NO                         AS TOU_NO                    " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.SITU_NO                        AS SITU_NO                   " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.SHOBO_CD                       AS SHOBO_CD                  " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.WH_KYOKA_DATE                  AS WH_KYOKA_DATE             " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.BAISU                          AS BAISU                     " & vbNewLine _
                                            & "	     ,SHOBO.HINMEI                                  AS HINMEI                    " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                  AS TOKYU                     " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                  AS SHUBETU                   " & vbNewLine _
                                            & "	     ,'1'                                           AS UPD_FLG                   " & vbNewLine _
                                            & "	     ,TOU_SITU_SHOBO.SYS_DEL_FLG                    AS SYS_DEL_FLG               " & vbNewLine


    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 棟室マスタ申請外の商品保管ルール一覧抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA3 As String = " SELECT                                                                          " & vbNewLine _
                                            & "	      TOU_SITU_EXP.NRS_BR_CD                      AS NRS_BR_CD                   " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.WH_CD                          AS WH_CD                       " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.TOU_NO                         AS TOU_NO                      " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.SITU_NO                        AS SITU_NO                     " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.SERIAL_NO                      AS SERIAL_NO                   " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.NO_APL_GOODS_STR_RULE_APL_DATE_FROM                         AS APL_DATE_FROM                  " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.NO_APL_GOODS_STR_RULE_APL_DATE_TO                           AS APL_DATE_TO                    " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.CUST_CD_L                      AS CUST_CD_L                   " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_L                              AS CUST_NM                     " & vbNewLine _
                                            & "	     ,'1'                                         AS UPD_FLG                     " & vbNewLine _
                                            & "	     ,TOU_SITU_EXP.SYS_DEL_FLG                    AS SYS_DEL_FLG                 " & vbNewLine

    Private Const SQL_SELECT_GET_SERIAL_NO As String = " SELECT                                                                 " & vbNewLine _
                                            & " CASE WHEN MAX(SERIAL_NO) IS NULL                                                " & vbNewLine _
                                            & " THEN 0                                                                          " & vbNewLine _
                                            & " ELSE MAX(SERIAL_NO)                                                             " & vbNewLine _
                                            & " END AS SERIAL_NO                                                                " & vbNewLine _
                                            & "FROM $LM_MST$..M_TOU_SITU_EXP                                                    " & vbNewLine _
                                            & "WHERE                                                                            " & vbNewLine _
                                            & "     NRS_BR_CD = @NRS_BR_CD                                                      " & vbNewLine _
                                            & " AND WH_CD = @WH_CD                                                              " & vbNewLine _
                                            & " AND TOU_NO = @TOU_NO                                                            " & vbNewLine _
                                            & " AND SITU_NO = @SITU_NO                                                          " & vbNewLine


    Private Const SQL_SELECT_DUPLICATE_EXP As String = " SELECT                                                                 " & vbNewLine _
                                            & " COUNT(0) AS TARGET_COUNT                                                        " & vbNewLine

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' M_TOU_SITU_ZONE_CHKデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA4 As String = " SELECT                                                     " & vbNewLine _
                                            & "       TOU_SITU_ZONE_CHK.NRS_BR_CD       AS NRS_BR_CD        " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.WH_CD           AS WH_CD            " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.TOU_NO          AS TOU_NO           " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.SITU_NO         AS SITU_NO          " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.ZONE_CD         AS ZONE_CD          " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.KBN_GROUP_CD    AS KBN_GROUP_CD     " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.KBN_CD          AS KBN_CD           " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.KBN_NM1         AS KBN_NM1          " & vbNewLine _
                                            & "      ,'1'                               AS UPD_FLG          " & vbNewLine _
                                            & "      ,TOU_SITU_ZONE_CHK.SYS_DEL_FLG     AS SYS_DEL_FLG      " & vbNewLine

    '''' <summary>
    '''' TCUST_M(MAXユーザーコード枝番)データ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_MAX As String = " SELECT                                                                              " & vbNewLine _
    '                                        & "	      MAX(TCUST.USER_CD_EDA)                      AS USER_CD_MAXEDA                " & vbNewLine

    ''' <summary>
    ''' 倉庫自社他社判定用データ取得
    ''' </summary>
    Private Const SQL_SELECT_SOKO_JT As String _
            = " SELECT              " & vbNewLine _
            & "    NRS_BR_CD        " & vbNewLine _
            & "   ,WH_CD            " & vbNewLine _
            & "   ,WH_KB            " & vbNewLine _
            & " FROM                " & vbNewLine _
            & "   $LM_MST$..M_SOKO  " & vbNewLine _
            & " WHERE               " & vbNewLine _
            & "   SYS_DEL_FLG = '0' " & vbNewLine _
            & " ORDER BY            " & vbNewLine _
            & "     WH_CD           " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_SITU AS TOU_SITU       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER AS USER1              " & vbNewLine _
                                          & "        ON TOU_SITU.SYS_ENT_USER    = USER1.USER_CD          " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER2             " & vbNewLine _
                                          & "       ON TOU_SITU.SYS_UPD_USER     = USER2.USER_CD          " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER3             " & vbNewLine _
                                          & "       ON TOU_SITU.USER_CD          = USER3.USER_CD          " & vbNewLine _
                                          & "       AND USER3.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR           " & vbNewLine _
                                          & "        ON TOU_SITU.NRS_BR_CD       = NRSBR.NRS_BR_CD        " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SOKO  AS SOKO              " & vbNewLine _
                                          & "        ON TOU_SITU.WH_CD           = SOKO.WH_CD             " & vbNewLine _
                                          & "       AND SOKO.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1            " & vbNewLine _
                                          & "        ON TOU_SITU.SOKO_KB         = KBN1.KBN_CD            " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD        = 'S015'                 " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2            " & vbNewLine _
                                          & "        ON TOU_SITU.HOZEI_KB        = KBN2.KBN_CD            " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD        = 'H001'                 " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3            " & vbNewLine _
                                          & "        ON TOU_SITU.ONDO_CTL_KB     = KBN3.KBN_CD            " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD        = 'O002'                 " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4            " & vbNewLine _
                                          & "        ON TOU_SITU.ONDO_CTL_FLG    = KBN4.KBN_CD            " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD        = 'O004'                 " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN5            " & vbNewLine _
                                          & "        ON TOU_SITU.RACK_YN = KBN5.KBN_CD                    " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD        = 'U009'                 " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN6            " & vbNewLine _
                                          & "        ON TOU_SITU.SHOKA_KB = KBN6.KBN_CD                   " & vbNewLine _
                                          & "       AND KBN6.KBN_GROUP_CD        = 'S016'                 " & vbNewLine _
                                          & "       AND KBN6.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN7            " & vbNewLine _
                                          & "        ON TOU_SITU.SYS_DEL_FLG     = KBN7.KBN_CD            " & vbNewLine _
                                          & "       AND KBN7.KBN_GROUP_CD        = 'S051'                 " & vbNewLine _
                                          & "       AND KBN7.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "--要望番号：674 yamanaka 2012.7.5 Start                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN8            " & vbNewLine _
                                          & "        ON TOU_SITU.SYS_DEL_FLG     = KBN8.KBN_CD            " & vbNewLine _
                                          & "       AND KBN7.KBN_GROUP_CD        = 'J004'                 " & vbNewLine _
                                          & "       AND KBN7.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                          & "--要望番号：674 yamanaka 2012.7.5 End                        " & vbNewLine _
                                          & "      LEFT JOIN $LM_MST$..S_USER          AS USER4           " & vbNewLine _
                                          & "        ON TOU_SITU.FCT_MGR         = USER4.USER_CD          " & vbNewLine _
                                          & "       AND USER4.SYS_DEL_FLG        = '0'                    " & vbNewLine _
                                          & "      LEFT JOIN $LM_MST$..S_USER          AS USER5           " & vbNewLine _
                                          & "       ON TOU_SITU.USER_CD_SUB      = USER5.USER_CD          " & vbNewLine _
                                          & "       AND USER5.SYS_DEL_FLG        = '0'                    " & vbNewLine

    Private Const SQL_FROM_DATA2 As String = "FROM                                                                      " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_SITU_SHOBO AS TOU_SITU_SHOBO         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SHOBO AS SHOBO                           " & vbNewLine _
                                          & "       ON TOU_SITU_SHOBO.SHOBO_CD     = SHOBO.SHOBO_CD                     " & vbNewLine _
                                          & "       AND SHOBO.SYS_DEL_FLG          = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                          " & vbNewLine _
                                          & "        ON SHOBO.KIKEN_TOKYU          = KBN1.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD          = 'S002'                             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG           = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                          " & vbNewLine _
                                          & "        ON SHOBO.SYU = KBN2.KBN_CD                                         " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD          = 'S022'                             " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG           = '0'                                " & vbNewLine


    'Private Const SQL_FROM_MAX As String = "FROM                                                          " & vbNewLine _
    '                                      & "                       $LM_MST$..M_TCUST  TCUST              " & vbNewLine

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    Private Const SQL_FROM_DATA3 As String = "FROM                                                                      " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_SITU_EXP TOU_SITU_EXP                " & vbNewLine _
                                          & "      LEFT OUTER JOIN (                                                    " & vbNewLine _
                                          & "                        SELECT  NRS_BR_CD                                  " & vbNewLine _
                                          & "                               ,CUST_CD_L                                  " & vbNewLine _
                                          & "                               ,CUST_NM_L                                  " & vbNewLine _
                                          & "                        FROM $LM_MST$..M_CUST                              " & vbNewLine _
                                          & "                        GROUP BY NRS_BR_CD,CUST_CD_L,CUST_NM_L             " & vbNewLine _
                                          & "                      ) CUST                                               " & vbNewLine _
                                          & "        ON  TOU_SITU_EXP.NRS_BR_CD =CUST.NRS_BR_CD                         " & vbNewLine _
                                          & "        AND  TOU_SITU_EXP.CUST_CD_L =CUST.CUST_CD_L                        " & vbNewLine

    Private Const SQL_FROM_DUPLICATE_EXP As String = "FROM                                                              " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_SITU_EXP TOU_SITU_EXP                " & vbNewLine
    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    Private Const SQL_FROM_DATA4 As String = "FROM                                                                      " & vbNewLine _
                                          & "                      $LM_MST$..M_TOU_SITU_ZONE_CHK AS TOU_SITU_ZONE_CHK   " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(M_TOU_SITU)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                              " & vbNewLine _
                                         & "     TOU_SITU.NRS_BR_CD                               " & vbNewLine _
                                         & "    ,TOU_SITU.WH_CD                                   " & vbNewLine _
                                         & "    ,TOU_SITU.TOU_NO                                  " & vbNewLine _
                                         & "    ,TOU_SITU.SITU_NO                                 " & vbNewLine

    ''' <summary>
    ''' ORDER BY(M_TOU_SITU_SHOBO)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                              " & vbNewLine _
                                         & "     TOU_SITU_SHOBO.NRS_BR_CD                          " & vbNewLine _
                                         & "    ,TOU_SITU_SHOBO.WH_CD                              " & vbNewLine _
                                         & "    ,TOU_SITU_SHOBO.TOU_NO                             " & vbNewLine _
                                         & "    ,TOU_SITU_SHOBO.SITU_NO                            " & vbNewLine _
                                         & "    ,TOU_SITU_SHOBO.SHOBO_CD                           " & vbNewLine

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

    Private Const SQL_ORDER_BY3 As String = "ORDER BY                                              " & vbNewLine _
                                     & "     TOU_SITU_EXP.CUST_CD_L                                " & vbNewLine _
                                     & "    ,TOU_SITU_EXP.NO_APL_GOODS_STR_RULE_APL_DATE_FROM      " & vbNewLine

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' ORDER BY(M_TOU_SITU_ZONE_CHK)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY4 As String = "ORDER BY                               " & vbNewLine _
                                         & "     TOU_SITU_ZONE_CHK.NRS_BR_CD        " & vbNewLine _
                                         & "    ,TOU_SITU_ZONE_CHK.WH_CD            " & vbNewLine _
                                         & "    ,TOU_SITU_ZONE_CHK.TOU_NO           " & vbNewLine _
                                         & "    ,TOU_SITU_ZONE_CHK.SITU_NO          " & vbNewLine _
                                         & "    ,TOU_SITU_ZONE_CHK.ZONE_CD          " & vbNewLine _
                                         & "    ,TOU_SITU_ZONE_CHK.KBN_GROUP_CD     " & vbNewLine _
                                         & "    ,TOU_SITU_ZONE_CHK.KBN_CD           " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 棟室マスタ存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_TOUSITU As String = "SELECT                         " & vbNewLine _
                                            & "   COUNT(WH_CD)  AS REC_CNT     " & vbNewLine _
                                            & "FROM $LM_MST$..M_TOU_SITU       " & vbNewLine _
                                            & "WHERE WH_CD    = @WH_CD         " & vbNewLine _
                                            & "  AND TOU_NO   = @TOU_NO        " & vbNewLine _
                                            & "  AND SITU_NO  = @SITU_NO       " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    ''' <summary>
    ''' 新規登録SQL（M_TOU_SITU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_TOU_SITU        " & vbNewLine _
                                       & "(                                       " & vbNewLine _
                                       & "       NRS_BR_CD                        " & vbNewLine _
                                       & "      ,WH_CD                            " & vbNewLine _
                                       & "      ,TOU_NO                           " & vbNewLine _
                                       & "      ,SITU_NO                          " & vbNewLine _
                                       & "      ,TOU_SITU_NM                      " & vbNewLine _
                                       & "      ,SOKO_KB                          " & vbNewLine _
                                       & "      ,HOZEI_KB                         " & vbNewLine _
                                       & "      ,CHOZO_MAX_QTY                    " & vbNewLine _
                                       & "      ,CHOZO_MAX_BAISU                  " & vbNewLine _
                                       & "      ,ONDO_CTL_KB                      " & vbNewLine _
                                       & "      ,ONDO                             " & vbNewLine _
                                       & "      ,MAX_ONDO_UP                      " & vbNewLine _
                                       & "      ,MINI_ONDO_DOWN                   " & vbNewLine _
                                       & "      ,ONDO_CTL_FLG                     " & vbNewLine _
                                       & "      ,HAN                              " & vbNewLine _
                                       & "      ,CBM                              " & vbNewLine _
                                       & "      ,AREA                             " & vbNewLine _
                                       & "      ,MX_PLT_QT                        " & vbNewLine _
                                       & "      ,RACK_YN                          " & vbNewLine _
                                       & "      ,FCT_MGR                          " & vbNewLine _
                                       & "      ,SHOKA_KB                         " & vbNewLine _
                                       & "--要望番号：674 yamanaka 2012.7.5 Start " & vbNewLine _
                                       & "      ,JISYATASYA_KB                    " & vbNewLine _
                                       & "--要望番号：674 yamanaka 2012.7.5 End   " & vbNewLine _
                                       & "      ,DOKU_KB                          " & vbNewLine _
                                       & "      ,GAS_KANRI_KB                     " & vbNewLine _
                                       & "      ,MAX_CAPA_KG_QTY                  " & vbNewLine _
                                       & "      ,USER_CD                          " & vbNewLine _
                                       & "      ,USER_CD_SUB                      " & vbNewLine _
                                       & "      ,TASYA_WH_NM                      " & vbNewLine _
                                       & "      ,TASYA_ZIP                        " & vbNewLine _
                                       & "      ,TASYA_AD_1                       " & vbNewLine _
                                       & "      ,TASYA_AD_2                       " & vbNewLine _
                                       & "      ,TASYA_AD_3                       " & vbNewLine _
                                       & "      ,AREA_RENT_HOKAN_AMO              " & vbNewLine _
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
                                       & "      ,@SITU_NO                         " & vbNewLine _
                                       & "      ,@TOU_SITU_NM                     " & vbNewLine _
                                       & "      ,@SOKO_KB                         " & vbNewLine _
                                       & "      ,@HOZEI_KB                        " & vbNewLine _
                                       & "      ,@CHOZO_MAX_QTY                   " & vbNewLine _
                                       & "      ,@CHOZO_MAX_BAISU                 " & vbNewLine _
                                       & "      ,@ONDO_CTL_KB                     " & vbNewLine _
                                       & "      ,@ONDO                            " & vbNewLine _
                                       & "      ,@MAX_ONDO_UP                     " & vbNewLine _
                                       & "      ,@MINI_ONDO_DOWN                  " & vbNewLine _
                                       & "      ,@ONDO_CTL_FLG                    " & vbNewLine _
                                       & "      ,@HAN                             " & vbNewLine _
                                       & "      ,@CBM                             " & vbNewLine _
                                       & "      ,@AREA                            " & vbNewLine _
                                       & "      ,@MX_PLT_QT                       " & vbNewLine _
                                       & "      ,@RACK_YN                         " & vbNewLine _
                                       & "      ,@FCT_MGR                         " & vbNewLine _
                                       & "      ,@SHOKA_KB                        " & vbNewLine _
                                       & "--要望番号：674 yamanaka 2012.7.5 Start " & vbNewLine _
                                       & "      ,@JISYATASYA_KB                   " & vbNewLine _
                                       & "--要望番号：674 yamanaka 2012.7.5 End   " & vbNewLine _
                                       & "      ,@DOKU_KB                         " & vbNewLine _
                                       & "      ,@GAS_KANRI_KB                    " & vbNewLine _
                                       & "      ,@MAX_CAPA_KG_QTY                 " & vbNewLine _
                                       & "      ,@USER_CD                         " & vbNewLine _
                                       & "      ,@USER_CD_SUB                     " & vbNewLine _
                                       & "      ,@TASYA_WH_NM                     " & vbNewLine _
                                       & "      ,@TASYA_ZIP                       " & vbNewLine _
                                       & "      ,@TASYA_AD_1                      " & vbNewLine _
                                       & "      ,@TASYA_AD_2                      " & vbNewLine _
                                       & "      ,@TASYA_AD_3                      " & vbNewLine _
                                       & "      ,@AREA_RENT_HOKAN_AMO             " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                    " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                    " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                    " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                    " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                    " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                    " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                    " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                     " & vbNewLine _
                                       & ")                                       " & vbNewLine

    ''' <summary>
    ''' 新規登録SQL（M_TOU_SITU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TOU_SITU_SHOBO As String = "INSERT INTO $LM_MST$..M_TOU_SITU_SHOBO     " & vbNewLine _
                                       & "(                                                         " & vbNewLine _
                                       & "       NRS_BR_CD                                          " & vbNewLine _
                                       & "      ,WH_CD                                              " & vbNewLine _
                                       & "      ,TOU_NO                                             " & vbNewLine _
                                       & "      ,SITU_NO                                            " & vbNewLine _
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
                                       & "      ,@SITU_NO                                           " & vbNewLine _
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

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    Private Const SQL_INSERT_TOU_SITU_EXP As String = "INSERT INTO $LM_MST$..M_TOU_SITU_EXP         " & vbNewLine _
                                       & "(                                                         " & vbNewLine _
                                       & "       NRS_BR_CD                                          " & vbNewLine _
                                       & "      ,WH_CD                                              " & vbNewLine _
                                       & "      ,TOU_NO                                             " & vbNewLine _
                                       & "      ,SITU_NO                                            " & vbNewLine _
                                       & "      ,SERIAL_NO                                          " & vbNewLine _
                                       & "      ,NO_APL_GOODS_STR_RULE_APL_DATE_FROM                " & vbNewLine _
                                       & "      ,NO_APL_GOODS_STR_RULE_APL_DATE_TO                  " & vbNewLine _
                                       & "      ,CUST_CD_L                                          " & vbNewLine _
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
                                       & "      ,@SITU_NO                                           " & vbNewLine _
                                       & "      ,@SERIAL_NO                                         " & vbNewLine _
                                       & "      ,@NO_APL_GOODS_STR_RULE_APL_DATE_FROM               " & vbNewLine _
                                       & "      ,@NO_APL_GOODS_STR_RULE_APL_DATE_TO                 " & vbNewLine _
                                       & "      ,@CUST_CD_L                                         " & vbNewLine _
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

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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

#End Region

#Region "Delete"

    ''' <summary>
    ''' 物理削除SQL（M_TOU_SITU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_SITU_SHOBO As String = "DELETE FROM $LM_MST$..M_TOU_SITU_SHOBO    " & vbNewLine _
                                       & "WHERE   WH_CD                 = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO              " & vbNewLine

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 物理削除SQL (M_TOU_SITU_EXP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_SITU_EXP As String = "DELETE FROM $LM_MST$..M_TOU_SITU_EXP        " & vbNewLine _
                                       & "WHERE   NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                       & "     AND WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO              " & vbNewLine
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 物理削除SQL（M_TOU_SITU_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TOU_SITU_ZONE_CHK As String = "DELETE FROM $LM_MST$..M_TOU_SITU_ZONE_CHK   " & vbNewLine _
                                            & "WHERE   WH_CD                 = @WH_CD                " & vbNewLine _
                                            & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                            & "     AND SITU_NO              = @SITU_NO              " & vbNewLine _
                                            & "     AND ZONE_CD              = ''                    " & vbNewLine

#End Region

#Region "UPDATE"

    ''' <summary>
    ''' 更新SQL（M_TOU_SITU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_TOU_SITU SET                     " & vbNewLine _
                                       & "        NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
                                       & "      , WH_CD               = @WH_CD                " & vbNewLine _
                                       & "      , TOU_NO              = @TOU_NO               " & vbNewLine _
                                       & "      , SITU_NO             = @SITU_NO              " & vbNewLine _
                                       & "      , TOU_SITU_NM         = @TOU_SITU_NM          " & vbNewLine _
                                       & "      , SOKO_KB             = @SOKO_KB              " & vbNewLine _
                                       & "      , HOZEI_KB            = @HOZEI_KB             " & vbNewLine _
                                       & "      , CHOZO_MAX_QTY       = @CHOZO_MAX_QTY        " & vbNewLine _
                                       & "      , CHOZO_MAX_BAISU     = @CHOZO_MAX_BAISU      " & vbNewLine _
                                       & "      , ONDO_CTL_KB         = @ONDO_CTL_KB          " & vbNewLine _
                                       & "      , ONDO                = @ONDO                 " & vbNewLine _
                                       & "      , MAX_ONDO_UP         = @MAX_ONDO_UP          " & vbNewLine _
                                       & "      , MINI_ONDO_DOWN      = @MINI_ONDO_DOWN       " & vbNewLine _
                                       & "      , ONDO_CTL_FLG        = @ONDO_CTL_FLG         " & vbNewLine _
                                       & "      , HAN                 = @HAN                  " & vbNewLine _
                                       & "      , CBM                 = @CBM                  " & vbNewLine _
                                       & "      , AREA                = @AREA                 " & vbNewLine _
                                       & "      , MX_PLT_QT           = @MX_PLT_QT            " & vbNewLine _
                                       & "      , RACK_YN             = @RACK_YN              " & vbNewLine _
                                       & "      , FCT_MGR             = @FCT_MGR              " & vbNewLine _
                                       & "      , SHOKA_KB            = @SHOKA_KB             " & vbNewLine _
                                       & "--要望番号：674 yamanaka 2012.7.5 Start             " & vbNewLine _
                                       & "      , JISYATASYA_KB       = @JISYATASYA_KB        " & vbNewLine _
                                       & "--要望番号：674 yamanaka 2012.7.5 End               " & vbNewLine _
                                       & "      , DOKU_KB             = @DOKU_KB              " & vbNewLine _
                                       & "      , GAS_KANRI_KB        = @GAS_KANRI_KB         " & vbNewLine _
                                       & "      , MAX_CAPA_KG_QTY     = @MAX_CAPA_KG_QTY      " & vbNewLine _
                                       & "      , USER_CD             = @USER_CD              " & vbNewLine _
                                       & "      , USER_CD_SUB         = @USER_CD_SUB          " & vbNewLine _
                                       & "      , TASYA_WH_NM         = @TASYA_WH_NM          " & vbNewLine _
                                       & "      , TASYA_ZIP           = @TASYA_ZIP            " & vbNewLine _
                                       & "      , TASYA_AD_1          = @TASYA_AD_1           " & vbNewLine _
                                       & "      , TASYA_AD_2          = @TASYA_AD_2           " & vbNewLine _
                                       & "      , TASYA_AD_3          = @TASYA_AD_3           " & vbNewLine _
                                       & "      , AREA_RENT_HOKAN_AMO = @AREA_RENT_HOKAN_AMO  " & vbNewLine _
                                       & "      , SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                       & "      , SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                       & "      , SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                       & "      , SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                       & "WHERE WH_CD                 = @WH_CD                " & vbNewLine _
                                       & "  AND TOU_NO                = @TOU_NO               " & vbNewLine _
                                       & "  AND SITU_NO               = @SITU_NO              " & vbNewLine

#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL（M_TOU_SITU）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_TOU_SITU SET                       " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO              " & vbNewLine


    ''' <summary>
    ''' 削除・復活SQL（M_TOU_SITU_SHOBO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TOU_SITU_SHOBO As String = "UPDATE $LM_MST$..M_TOU_SITU_SHOBO SET                    " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO              " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_TOU_SITU_ZONE_CHK）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TOU_SITU_ZONE_CHK As String = "UPDATE $LM_MST$..M_TOU_SITU_ZONE_CHK SET                    " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         WH_CD                = @WH_CD                " & vbNewLine _
                                       & "     AND TOU_NO               = @TOU_NO               " & vbNewLine _
                                       & "     AND SITU_NO              = @SITU_NO              " & vbNewLine _
                                       & "     AND ZONE_CD              = ''                    " & vbNewLine

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
    ''' 棟室マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM130DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM130DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM130DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM130DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM130DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_KB", "WH_KB")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("TOU_SITU_NM", "TOU_SITU_NM")
        map.Add("SOKO_KB", "SOKO_KB")
        map.Add("SOKO_NM", "SOKO_NM")
        map.Add("HOZEI_KB", "HOZEI_KB")
        map.Add("HOZEI_NM", "HOZEI_NM")
        map.Add("CHOZO_MAX_QTY", "CHOZO_MAX_QTY")
        map.Add("CHOZO_MAX_BAISU", "CHOZO_MAX_BAISU")
        map.Add("ONDO_CTL_KB", "ONDO_CTL_KB")
        map.Add("ONDO_CTL_NM", "ONDO_CTL_NM")
        map.Add("ONDO", "ONDO")
        map.Add("MAX_ONDO_UP", "MAX_ONDO_UP")
        map.Add("MINI_ONDO_DOWN", "MINI_ONDO_DOWN")
        map.Add("ONDO_CTL_FLG", "ONDO_CTL_FLG")
        map.Add("ONDO_CTL_FLG_NM", "ONDO_CTL_FLG_NM")
        map.Add("HAN", "HAN")
        map.Add("CBM", "CBM")
        map.Add("AREA", "AREA")
        map.Add("MX_PLT_QT", "MX_PLT_QT")
        map.Add("RACK_YN", "RACK_YN")
        map.Add("RACK_YN_NM", "RACK_YN_NM")
        map.Add("FCT_MGR", "FCT_MGR")
        map.Add("FCT_MGR_NM", "FCT_MGR_NM")
        map.Add("SHOKA_KB", "SHOKA_KB")
        map.Add("SHOKA_KB_NM", "SHOKA_KB_NM")
        '要望番号：674 yamanaka 2012.7.5 Start
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("JISYATASYA_KB_NM", "JISYATASYA_KB_NM")
        '要望番号：674 yamanaka 2012.7.5 End
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("GAS_KANRI_KB", "GAS_KANRI_KB")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("MAX_CAPA_KG_QTY", "MAX_CAPA_KG_QTY")
        map.Add("USER_CD", "USER_CD")
        map.Add("USER_NM", "USER_NM")
        map.Add("USER_CD_SUB", "USER_CD_SUB")
        map.Add("USER_NM_SUB", "USER_NM_SUB")
        map.Add("TASYA_WH_NM", "TASYA_WH_NM")
        map.Add("TASYA_ZIP", "TASYA_ZIP")
        map.Add("TASYA_AD_1", "TASYA_AD_1")
        map.Add("TASYA_AD_2", "TASYA_AD_2")
        map.Add("TASYA_AD_3", "TASYA_AD_3")
        map.Add("AREA_RENT_HOKAN_AMO", "AREA_RENT_HOKAN_AMO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM130OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU)
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
                andstr.Append(" (TOU_SITU.SYS_DEL_FLG = @SYS_DEL_FLG  OR TOU_SITU.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (TOU_SITU.NRS_BR_CD = @NRS_BR_CD OR TOU_SITU.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.WH_CD = @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_SITU_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.TOU_SITU_NM LIKE @TOU_SITU_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_SITU_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HOZEI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.HOZEI_KB = @HOZEI_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOZEI_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ONDO_CTL_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.ONDO_CTL_KB = @ONDO_CTL_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("ONDO_CTL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU.ONDO_CTL_FLG = @ONDO_CTL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 棟室マスタ消防更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ消防更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM130DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM130DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM130DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("WH_KYOKA_DATE", "WH_KYOKA_DATE")
        map.Add("BAISU", "BAISU")
        map.Add("HINMEI", "HINMEI")
        map.Add("TOKYU", "TOKYU")
        map.Add("SHUBETU", "SHUBETU")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM130_TOU_SITU_SHOBO")

        Return ds

    End Function

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

    ''' <summary>
    ''' 棟室マスタ申請外の商品保管ルール更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ申請外の商品保管ルール一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData3(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM130DAC.SQL_SELECT_DATA3)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM130DAC.SQL_FROM_DATA3)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL3()                   '条件設定
        Me._StrSql.Append(LMM130DAC.SQL_ORDER_BY3)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectListData3", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("APL_DATE_FROM", "APL_DATE_FROM")
        map.Add("APL_DATE_TO", "APL_DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SERIAL_NO", "SERIAL_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM130_TOU_SITU_EXP")

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫自社他社判定用データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSokoJT(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130_SOKO_JT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_SELECT_SOKO_JT, Me._Row.Item("NRS_BR_CD").ToString))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectSokoJT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        inTbl.Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_KB", "WH_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM130_SOKO_JT")

        Return ds

    End Function

    ''' <summary>
    ''' 申請外の商品管理ルール一括登録処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IkkatuTourokuExpAction(ByVal ds As DataSet) As DataSet

        Dim inTable As DataTable = ds.Tables("LMM130IN")
        Dim sourceTable As DataTable = ds.Tables("LMM130_TOU_SITU_EXP")
        Dim inRow As DataRow = inTable.Rows(0)

        '既に同じキーのデータが存在しないか確認
        For Each row As DataRow In sourceTable.Rows

            Me._Row = row

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(LMM130DAC.SQL_SELECT_DUPLICATE_EXP)      'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMM130DAC.SQL_FROM_DUPLICATE_EXP)        'SQL構築(データ抽出用from句)
            Call Me.SetConditionMasterSQLDuplicateExp()                '条件設定

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), inRow.Item("USER_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM130DAC", "IkkatuTourokuExpAction", cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            reader.Read()

            'エラーメッセージの設定
            If Convert.ToInt32(reader("TARGET_COUNT")) > 0 Then
                reader.Close()
                Dim touNo As String = _Row.Item("TOU_NO").ToString
                Dim situNo As String = _Row.Item("SITU_NO").ToString
                Dim aplDateFrom As String = Com.Utility.DateFormatUtility.EditSlash(_Row.Item("APL_DATE_FROM").ToString)
                Dim custCdL As String = _Row.Item("CUST_CD_L").ToString

                '以下の申請外の商品保管ルールデータは既に存在しています。[棟 = ～][室 = ～][適用日FROM = ～][荷主コード = ～]
                MyBase.SetMessageStore("00", "E965", New String() {touNo, situNo, aplDateFrom, custCdL})

            Else
                reader.Close()
            End If

        Next

        '既に同じキーのデータが登録していれば処理を終了する。
        If MyBase.IsMessageStoreExist() Then
            Return ds
        End If

        Me._Row = inRow

        '登録を行う。
        InsertTouSituExpM(ds)

        Return ds

    End Function

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData4(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM130DAC.SQL_SELECT_DATA4)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM130DAC.SQL_FROM_DATA4)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL4()                   '条件設定
        Me._StrSql.Append(LMM130DAC.SQL_ORDER_BY4)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectListData4", cmd)

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

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM130_TOU_SITU_ZONE_CHK")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU_SHOBO)
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
                andstr.Append(" TOU_SITU_SHOBO.WH_CD LIKE @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_SHOBO.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_SHOBO.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU_EXP)
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
                andstr.Append(" TOU_SITU_EXP.WH_CD LIKE @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_EXP.TOU_NO LIKE @TOU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TOU_SITU_EXP.SITU_NO LIKE @SITU_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU_EXP重複チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQLDuplicateExp()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As New StringBuilder

        With Me._Row
            'SQLのWHERE区作成
            whereStr.Append("WHERE" & vbNewLine)
            whereStr.Append("NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
            whereStr.Append("AND WH_CD = @WH_CD" & vbNewLine)
            whereStr.Append("AND TOU_NO = @TOU_NO" & vbNewLine)
            whereStr.Append("AND SITU_NO = @SITU_NO" & vbNewLine)
            whereStr.Append("AND NO_APL_GOODS_STR_RULE_APL_DATE_FROM = @APL_DATE_FROM" & vbNewLine)
            whereStr.Append("AND CUST_CD_L = @CUST_CD_L" & vbNewLine)
            whereStr.Append("AND SYS_DEL_FLG = 0")
            'SQLのパラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APL_DATE_FROM", .Item("APL_DATE_FROM"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L"), DBDataType.CHAR))
        End With

        Me._StrSql.Append(whereStr.ToString)

    End Sub

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TOU_SITU_ZONE_CHK)
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

            'ZONEコードは固定
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" TOU_SITU_ZONE_CHK.ZONE_CD = '' ")
            andstr.Append(vbNewLine)

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

    '''' <summary>
    '''' MAXユーザーコード枝番取得
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>MAXユーザーコード枝番取得SQLの構築・発行</remarks>
    'Private Function SelectMaxUserCdEdaData(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMM130IN")

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL格納変数の初期化
    '    Me._StrSql = New StringBuilder()

    '    'SQL作成
    '    Me._StrSql.Append(LMM130DAC.SQL_SELECT_MAX)        'SQL構築(データ抽出用Select句)
    '    Me._StrSql.Append(LMM130DAC.SQL_FROM_MAX)          'SQL構築(データ抽出用from句)
    '    Call Me.SetConditionMasterSQL2()                   '条件設定

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectMaxUserCdEdaData", cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    'DataReader→DataTableへの転記
    '    Dim map As Hashtable = New Hashtable()

    '    '取得データの格納先をマッピング
    '    map.Add("USER_CD_MAXEDA", "USER_CD_MAXEDA")

    '    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM130_TCUST_MAXEDA")

    '    Return ds

    'End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 棟室マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectTouSituM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM130DAC.SQL_EXIT_TOUSITU)
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

        MyBase.Logger.WriteSQLLog("LMM130DAC", "SelectTouSituM", cmd)

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
    ''' 棟室マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistTouSituM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_EXIT_TOUSITU, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "CheckExistTouSituM", cmd)

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
    ''' 棟室マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTouSituM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "InsertTouSituM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ消防情報新規登録SQLの構築・発行</remarks>
    Private Function DelTouSituShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_DEL_TOU_SITU_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "DelTouSituShoboM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 申請外の商品保管許可ルールマスタ情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>申請外の商品保管許可ルールマスタ情報新規登録SQLの構築・発行</remarks>
    Private Function DelTouSituExpM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_DEL_TOU_SITU_EXP, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteExp()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "DelTouSituExpM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室ゾーンチェックマスタ情報削除SQLの構築・発行</remarks>
    Private Function DelTouSituZoneChkM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_DEL_TOU_SITU_ZONE_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "DelTouSituZoneChkM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ消防情報新規登録SQLの構築・発行</remarks>
    Private Function InsertTouSituShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130_TOU_SITU_SHOBO")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_INSERT_TOU_SITU_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))
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

            MyBase.Logger.WriteSQLLog("LMM130DAC", "InsertTouSituShoboM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 申請外の商品保管許可ルールマスタ情報新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>申請外の商品保管許可ルールマスタ情報新規登録SQLの構築・発行</remarks>
    Private Function InsertTouSituExpM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130_TOU_SITU_EXP")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_INSERT_TOU_SITU_EXP, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1
        Dim userBrCode As String = Me._Row.Item("USER_BR_CD").ToString()

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            '登録連番設定
            _Row.Item("SERIAL_NO") = GetSerialNo(userBrCode)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTouSituExpParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM130DAC", "InsertTouSituExpM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

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
        Dim inTbl As DataTable = ds.Tables("LMM130_TOU_SITU_ZONE_CHK")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_INSERT_TOU_SITU_ZONE_CHK, Me._Row.Item("USER_BR_CD").ToString()))
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

            MyBase.Logger.WriteSQLLog("LMM130DAC", "InsertTouSituZoneChkM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 登録連番取得
    ''' </summary>
    ''' <param name="userBrCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSerialNo(ByVal userBrCode As String) As String

        Dim prmList As New ArrayList()
        Dim serialNo As String = String.Empty

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(SQL_SELECT_GET_SERIAL_NO, userBrCode))
        'パラメータ設定
        With Me._Row
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In prmList
            cmd.Parameters.Add(obj)
        Next

        'SQLのログ出力
        MyBase.Logger.WriteSQLLog("LMM130DAC", "GetSerialNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        reader.Read()

        serialNo = Convert.ToString(Convert.ToInt32(reader("SERIAL_NO")) + 1)
        reader.Close()
        Return serialNo

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

#End Region

#Region "Update"

    ''' <summary>
    ''' 棟室マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateTouSituM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM130DAC.SQL_UPDATE _
                                                                                     , LMM130DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "UpdateTouSituM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "Delete"

    ''' <summary>
    ''' 棟室マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTouSituM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "DeleteTouSituM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>棟室マスタ消防情報削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTouSituShoboM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_DELETE_TOU_SITU_SHOBO, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "DeleteTouSituShoboM", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMM130IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM130DAC.SQL_DELETE_TOU_SITU_ZONE_CHK, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM130DAC", "DeleteTouSituZoneChkM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

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
    ''' パラメータ設定モジュール(棟室マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))

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
    ''' パラメータ設定モジュール(新規登録_棟室Ｍ)
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

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)(申請外の商品保管許可ルール用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteExp()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDelExp()

        Call Me.SetParamCommonSystemUpd()

    End Sub
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add END 

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_棟室Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_SITU_NM", .Item("TOU_SITU_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_KB", .Item("SOKO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOZEI_KB", .Item("HOZEI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHOZO_MAX_QTY", .Item("CHOZO_MAX_QTY").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHOZO_MAX_BAISU", .Item("CHOZO_MAX_BAISU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_KB", .Item("ONDO_CTL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO", .Item("ONDO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAX_ONDO_UP", .Item("MAX_ONDO_UP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MINI_ONDO_DOWN", .Item("MINI_ONDO_DOWN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_CTL_FLG", .Item("ONDO_CTL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAN", .Item("HAN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CBM", .Item("CBM").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA", .Item("AREA").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MX_PLT_QT", .Item("MX_PLT_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RACK_YN", .Item("RACK_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FCT_MGR", .Item("FCT_MGR").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOKA_KB", .Item("SHOKA_KB").ToString(), DBDataType.CHAR))
            '要望番号：674 yamanaka 2012.7.5 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", .Item("JISYATASYA_KB").ToString(), DBDataType.CHAR))
            '要望番号：674 yamanaka 2012.7.5 End
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", .Item("DOKU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GAS_KANRI_KB", .Item("GAS_KANRI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAX_CAPA_KG_QTY", .Item("MAX_CAPA_KG_QTY").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD_SUB", .Item("USER_CD_SUB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TASYA_WH_NM", .Item("TASYA_WH_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TASYA_ZIP", .Item("TASYA_ZIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TASYA_AD_1", .Item("TASYA_AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TASYA_AD_2", .Item("TASYA_AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TASYA_AD_3", .Item("TASYA_AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_RENT_HOKAN_AMO", .Item("AREA_RENT_HOKAN_AMO").ToString(), DBDataType.NUMERIC))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_棟室マスタ消防情報用)
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
            prmList.Add(MyBase.GetSqlParameter("@BAISU", Me.FormatNumValue(.Item("BAISU").ToString()), DBDataType.NUMERIC))
        End With

    End Sub

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_棟室マスタ消防情報用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTouSituExpParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NO_APL_GOODS_STR_RULE_APL_DATE_FROM", .Item("APL_DATE_FROM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NO_APL_GOODS_STR_RULE_APL_DATE_TO", .Item("APL_DATE_TO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        End With

    End Sub
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add END 

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDelExp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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
