' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM010DAC : ユーザーマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    '更新前Select文
    Private Const SELECT_INSERT_DATA As String = "SYS_DEL_FLG = '0' "
    Private Const SELECT_UPDATE_DATA As String = "SYS_DEL_FLG = '0' AND UPD_FLG = '1' "
    Private Const SELECT_DELETE_DATA_001 As String = "SYS_DEL_FLG = '1' "
    Private Const SELECT_DELETE_DATA_002 As String = "UPD_FLG = '2' "

#End Region

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(USER0.USER_CD)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                  " & vbNewLine

    'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
    '''' <summary>
    '''' USER_Mデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                           " & vbNewLine _
    '                                        & "	      USER0.USER_CD                               AS USER_CD                      " & vbNewLine _
    '                                        & "	     ,USER0.USER_NM                               AS USER_NM                      " & vbNewLine _
    '                                        & "	     ,USER0.PW                                    AS PW                           " & vbNewLine _
    '                                        & "	     ,USER0.AUTHO_LV                              AS AUTHO_LV                     " & vbNewLine _
    '                                        & "	     ,KBN1.KBN_NM1                                AS AUTHO_LV_NM                  " & vbNewLine _
    '                                        & "	     ,USER0.NRS_BR_CD                             AS NRS_BR_CD                    " & vbNewLine _
    '                                        & "	     ,NRSBR.NRS_BR_NM                             AS NRS_BR_NM                    " & vbNewLine _
    '                                        & "	     ,USER0.WH_CD                                 AS WH_CD                        " & vbNewLine _
    '                                        & "	     ,SOKO.WH_NM                                  AS WH_NM                        " & vbNewLine _
    '                                        & "	     ,USER0.BUSYO_CD                              AS BUSYO_CD                     " & vbNewLine _
    '                                        & "	     ,KBN2.KBN_NM1                                AS BUSYO_NM                     " & vbNewLine _
    '                                        & "	     ,USER0.INKA_DATE_INIT                        AS INKA_DATE_INIT               " & vbNewLine _
    '                                        & "	     ,KBN3.KBN_NM1                                AS INKA_DATE_INIT_NM            " & vbNewLine _
    '                                        & "	     ,USER0.OUTKA_DATE_INIT                       AS OUTKA_DATE_INIT              " & vbNewLine _
    '                                        & "	     ,KBN4.KBN_NM1                                AS OUTKA_DATE_INIT_NM           " & vbNewLine _
    '                                        & "	     ,USER0.DEF_PRT1                              AS DEF_PRT1                     " & vbNewLine _
    '                                        & "	     ,USER0.DEF_PRT2                              AS DEF_PRT2                     " & vbNewLine _
    '                                        & "	     ,USER0.COA_PRT                               AS COA_PRT                      " & vbNewLine _
    '                                        & "	     ,USER0.SYS_ENT_DATE                          AS SYS_ENT_DATE                 " & vbNewLine _
    '                                        & "	     ,USER1.USER_NM                               AS SYS_ENT_USER_NM              " & vbNewLine _
    '                                        & "	     ,USER0.SYS_UPD_DATE                          AS SYS_UPD_DATE                 " & vbNewLine _
    '                                        & "	     ,USER0.SYS_UPD_TIME                          AS SYS_UPD_TIME                 " & vbNewLine _
    '                                        & "	     ,USER2.USER_NM                               AS SYS_UPD_USER_NM              " & vbNewLine _
    '                                        & "	     ,USER0.SYS_DEL_FLG                           AS SYS_DEL_FLG                  " & vbNewLine _
    '                                        & "	     ,KBN5.KBN_NM1                                AS SYS_DEL_NM                   " & vbNewLine
    ''' <summary>
    ''' USER_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                           " & vbNewLine _
                                            & "	      USER0.USER_CD                               AS USER_CD                      " & vbNewLine _
                                            & "	     ,USER0.USER_NM                               AS USER_NM                      " & vbNewLine _
                                            & "	     ,USER0.PW                                    AS PW                           " & vbNewLine _
                                            & "	     ,USER0.AUTHO_LV                              AS AUTHO_LV                     " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                AS AUTHO_LV_NM                  " & vbNewLine _
                                            & "	     ,USER0.RIYOUSHA_KBN                          AS RIYOUSHA_KBN                 " & vbNewLine _
                                            & "	     ,KBN6.KBN_NM1                                AS RIYOUSHA_KBN_NM              " & vbNewLine _
                                            & "	     ,USER0.NRS_BR_CD                             AS NRS_BR_CD                    " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                             AS NRS_BR_NM                    " & vbNewLine _
                                            & "	     ,USER0.WH_CD                                 AS WH_CD                        " & vbNewLine _
                                            & "	     ,SOKO.WH_NM                                  AS WH_NM                        " & vbNewLine _
                                            & "	     ,USER0.BUSYO_CD                              AS BUSYO_CD                     " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                AS BUSYO_NM                     " & vbNewLine _
                                            & "	     ,USER0.INKA_DATE_INIT                        AS INKA_DATE_INIT               " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                                AS INKA_DATE_INIT_NM            " & vbNewLine _
                                            & "	     ,USER0.OUTKA_DATE_INIT                       AS OUTKA_DATE_INIT              " & vbNewLine _
                                            & "	     ,KBN4.KBN_NM1                                AS OUTKA_DATE_INIT_NM           " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT1                              AS DEF_PRT1                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT2                              AS DEF_PRT2                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT3                              AS DEF_PRT3                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT4                              AS DEF_PRT4                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT5                              AS DEF_PRT5                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT6                              AS DEF_PRT6                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT7                              AS DEF_PRT7                     " & vbNewLine _
                                            & "	     ,USER0.DEF_PRT8                              AS DEF_PRT8                     " & vbNewLine _
                                            & "	     ,USER0.COA_PRT                               AS COA_PRT                      " & vbNewLine _
                                            & "	     ,USER0.EMAIL                                 AS EMAIL                        " & vbNewLine _
                                            & "	     ,USER0.NOTICE_YN                             AS NOTICE_YN                    " & vbNewLine _
                                            & "	     ,USER0.SYS_ENT_DATE                          AS SYS_ENT_DATE                 " & vbNewLine _
                                            & "	     ,USER1.USER_NM                               AS SYS_ENT_USER_NM              " & vbNewLine _
                                            & "	     ,USER0.SYS_UPD_DATE                          AS SYS_UPD_DATE                 " & vbNewLine _
                                            & "	     ,USER0.SYS_UPD_TIME                          AS SYS_UPD_TIME                 " & vbNewLine _
                                            & "	     ,USER2.USER_NM                               AS SYS_UPD_USER_NM              " & vbNewLine _
                                            & "	     ,USER0.SYS_DEL_FLG                           AS SYS_DEL_FLG                  " & vbNewLine _
                                            & "	     ,KBN5.KBN_NM1                                AS SYS_DEL_NM                   " & vbNewLine _
                                            & "      ,USER0.YELLOW_CARD_PRT                       AS YELLOW_CARD_PRT              " & vbNewLine _
                                            & "      ,USER0.SAP_LINK_AUTHO                        AS SAP_LINK_AUTHO               " & vbNewLine
    'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

    ''' <summary>
    ''' TCUST_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                           " & vbNewLine _
                                            & "	      TCUST.USER_CD                               AS USER_CD                      " & vbNewLine _
                                            & "	     ,TCUST.USER_CD_EDA                           AS USER_CD_EDA                  " & vbNewLine _
                                            & "	     ,TCUST.CUST_CD_L                             AS CUST_CD_L                    " & vbNewLine _
                                            & "	     ,TCUST.CUST_CD_M                             AS CUST_CD_M                    " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_L                              AS CUST_NM_L                    " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_M                              AS CUST_NM_M                    " & vbNewLine _
                                            & "	     ,TCUST.DEFAULT_CUST_YN                       AS DEFAULT_CUST_YN              " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM2                                AS DEFAULT_CUST_YN_NM           " & vbNewLine _
                                            & "	     ,'1'                                         AS UPD_FLG                      " & vbNewLine _
                                            & "	     ,TCUST.SYS_DEL_FLG                           AS SYS_DEL_FLG                  " & vbNewLine

    ''' <summary>
    ''' TCUST_M(MAXユーザーコード枝番)データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAX As String = " SELECT                                                                              " & vbNewLine _
                                            & "	      MAX(TCUST.USER_CD_EDA)                      AS USER_CD_MAXEDA                " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    ''' TUNSOCO_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TUNSOCO As String = "SELECT                                                 " & vbNewLine _
                                               & "  TUNSOCO.USER_CD      AS USER_CD                      " & vbNewLine _
                                               & ", TUNSOCO.USER_CD_EDA  AS USER_CD_EDA                  " & vbNewLine _
                                               & ", TUNSOCO.UNSOCO_CD    AS UNSOCO_CD                    " & vbNewLine _
                                               & ", TUNSOCO.UNSOCO_BR_CD AS UNSOCO_BR_CD                 " & vbNewLine _
                                               & ", UNSOCO.UNSOCO_NM     AS UNSOCO_NM                    " & vbNewLine _
                                               & ", UNSOCO.UNSOCO_BR_NM  AS UNSOCO_BR_NM                 " & vbNewLine _
                                               & ", '1'                  AS UPD_FLG                      " & vbNewLine _
                                               & ", TUNSOCO.SYS_DEL_FLG  AS SYS_DEL_FLG                  " & vbNewLine

    ''' <summary>
    ''' TUNSOCO_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TTARIFF As String = "SELECT                                             " & vbNewLine _
                                               & "  TTARIFF.USER_CD            AS USER_CD            " & vbNewLine _
                                               & ", TTARIFF.USER_CD_EDA        AS USER_CD_EDA        " & vbNewLine _
                                               & ", TTARIFF.UNCHIN_TARIFF_CD   AS UNCHIN_TARIFF_CD   " & vbNewLine _
                                               & ", TARIFF.UNCHIN_TARIFF_REM   AS UNCHIN_TARIFF_REM  " & vbNewLine _
                                               & ", '1'                        AS UPD_FLG            " & vbNewLine _
                                               & ", TTARIFF.SYS_DEL_FLG        AS SYS_DEL_FLG        " & vbNewLine
    '要望番号:1248 yamanaka 2013.03.21 End


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..S_USER AS USER0              " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER AS USER1              " & vbNewLine _
                                          & "        ON USER0.SYS_ENT_USER    = USER1.USER_CD             " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER2             " & vbNewLine _
                                          & "       ON USER0.SYS_UPD_USER     = USER2.USER_CD             " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1            " & vbNewLine _
                                          & "        ON USER0.AUTHO_LV        = KBN1.KBN_CD               " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD    = 'K010'                     " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN6            " & vbNewLine _
                                          & "        ON USER0.RIYOUSHA_KBN   = KBN6.KBN_CD                " & vbNewLine _
                                          & "       AND KBN6.KBN_GROUP_CD    = 'R015'                     " & vbNewLine _
                                          & "       AND KBN6.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR           " & vbNewLine _
                                          & "        ON USER0.NRS_BR_CD       = NRSBR.NRS_BR_CD           " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG    = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SOKO  AS SOKO              " & vbNewLine _
                                          & "        ON USER0.WH_CD           = SOKO.WH_CD                " & vbNewLine _
                                          & "       AND SOKO.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2            " & vbNewLine _
                                          & "        ON USER0.BUSYO_CD        = KBN2.KBN_CD               " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD    = 'B007'                     " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3            " & vbNewLine _
                                          & "        ON USER0.INKA_DATE_INIT  = KBN3.KBN_CD               " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD    = 'H008'                     " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4            " & vbNewLine _
                                          & "        ON USER0.OUTKA_DATE_INIT = KBN4.KBN_CD               " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD    = 'H008'                     " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG     = '0'                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN5            " & vbNewLine _
                                          & "        ON USER0.SYS_DEL_FLG     = KBN5.KBN_CD               " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD    = 'S051'                     " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG     = '0'                        " & vbNewLine

    Private Const SQL_FROM_DATA2 As String = "FROM                                                        " & vbNewLine _
                                          & "                      $LM_MST$..M_TCUST AS TCUST             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER AS USER0              " & vbNewLine _
                                          & "       ON TCUST.USER_CD          = USER0.USER_CD             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST AS CUST               " & vbNewLine _
                                          & "       ON  TCUST.CUST_CD_L       = CUST.CUST_CD_L            " & vbNewLine _
                                          & "       AND TCUST.CUST_CD_M       = CUST.CUST_CD_M            " & vbNewLine _
                                          & "       AND CUST.CUST_CD_S        = '00'                      " & vbNewLine _
                                          & "       AND CUST.CUST_CD_SS       = '00'                      " & vbNewLine _
                                          & "       AND CUST.SYS_DEL_FLG      = '0'                       " & vbNewLine _
                                          & "       AND CUST.NRS_BR_CD        = USER0.NRS_BR_CD           " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1            " & vbNewLine _
                                          & "        ON TCUST.DEFAULT_CUST_YN = KBN1.KBN_CD               " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD     = 'U009'                    " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG      = '0'                       " & vbNewLine

    Private Const SQL_FROM_MAX As String = "FROM                                                          " & vbNewLine _
                                          & "                       $LM_MST$..M_TCUST  TCUST              " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.21 Start
    Private Const SQL_FROM_TUNSOCO As String = "FROM                                           " & vbNewLine _
                                             & "    $LM_MST$..M_TUNSOCO TUNSOCO                " & vbNewLine _
                                             & "LEFT JOIN                                      " & vbNewLine _
                                             & "    $LM_MST$..S_USER S_USER                    " & vbNewLine _
                                             & " ON TUNSOCO.USER_CD = S_USER.USER_CD           " & vbNewLine _
                                             & "LEFT JOIN                                      " & vbNewLine _
                                             & "    $LM_MST$..M_UNSOCO UNSOCO                  " & vbNewLine _
                                             & " ON S_USER.NRS_BR_CD = UNSOCO.NRS_BR_CD        " & vbNewLine _
                                             & "AND TUNSOCO.UNSOCO_CD = UNSOCO.UNSOCO_CD       " & vbNewLine _
                                             & "AND TUNSOCO.UNSOCO_BR_CD = UNSOCO.UNSOCO_BR_CD " & vbNewLine _
                                             & "AND UNSOCO.SYS_DEL_FLG = '0'                   " & vbNewLine

    Private Const SQL_FROM_TTARIFF As String = "FROM                                                    " & vbNewLine _
                                             & "    $LM_MST$..M_TUNCHIN_TARIFF TTARIFF                  " & vbNewLine _
                                             & "LEFT JOIN                                               " & vbNewLine _
                                             & "    $LM_MST$..S_USER S_USER                             " & vbNewLine _
                                             & " ON TTARIFF.USER_CD = S_USER.USER_CD                    " & vbNewLine _
                                             & "LEFT JOIN                                               " & vbNewLine _
                                             & "    $LM_MST$..M_UNCHIN_TARIFF TARIFF                    " & vbNewLine _
                                             & " ON S_USER.NRS_BR_CD = TARIFF.NRS_BR_CD                 " & vbNewLine _
                                             & "AND TTARIFF.UNCHIN_TARIFF_CD = TARIFF.UNCHIN_TARIFF_CD  " & vbNewLine _
                                             & "AND TARIFF.UNCHIN_TARIFF_CD_EDA = '000'                 " & vbNewLine _
                                             & "AND TARIFF.SYS_DEL_FLG = '0'                            " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.21 End


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(USER_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     USER0.USER_CD                                     " & vbNewLine

    ''' <summary>
    ''' ORDER BY(TCUST_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                              " & vbNewLine _
                                         & "     TCUST.CUST_CD_L                                   " & vbNewLine _
                                         & "    ,TCUST.CUST_CD_M                                   " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    ''' ORDER BY(TUNSOCO_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_TUNSOCO As String = "ORDER BY               " & vbNewLine _
                                                 & "  TUNSOCO.UNSOCO_CD    " & vbNewLine _
                                                 & ", TUNSOCO.UNSOCO_BR_CD " & vbNewLine

    ''' <summary>
    ''' ORDER BY(TUNCHIN_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_TUNCHIN As String = "ORDER BY                    " & vbNewLine _
                                                 & "  TTARIFF.UNCHIN_TARIFF_CD  " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.21 End

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' ユーザーコード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_USER As String = "SELECT                            " & vbNewLine _
                                            & "   COUNT(USER_CD)  AS REC_CNT     " & vbNewLine _
                                            & "FROM $LM_MST$..S_USER             " & vbNewLine _
                                            & "WHERE USER_CD    = @USER_CD       " & vbNewLine


#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
    '''' <summary>
    '''' 新規登録SQL（S_USER）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..S_USER      " & vbNewLine _
    '                                   & "(                                 " & vbNewLine _
    '                                   & "      USER_CD                     " & vbNewLine _
    '                                   & "      ,USER_NM                    " & vbNewLine _
    '                                   & "      ,PW        		            " & vbNewLine _
    '                                   & "      ,AUTHO_LV                   " & vbNewLine _
    '                                   & "      ,NRS_BR_CD        	        " & vbNewLine _
    '                                   & "      ,WH_CD        		        " & vbNewLine _
    '                                   & "      ,BUSYO_CD        		    " & vbNewLine _
    '                                   & "      ,INKA_DATE_INIT        	    " & vbNewLine _
    '                                   & "      ,OUTKA_DATE_INIT            " & vbNewLine _
    '                                   & "      ,DEF_PRT1        	        " & vbNewLine _
    '                                   & "      ,DEF_PRT2        	        " & vbNewLine _
    '                                   & "      ,COA_PRT         	        " & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE               " & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME               " & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID               " & vbNewLine _
    '                                   & "      ,SYS_ENT_USER               " & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE               " & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME               " & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID               " & vbNewLine _
    '                                   & "      ,SYS_UPD_USER               " & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG                " & vbNewLine _
    '                                   & "      ) VALUES (                  " & vbNewLine _
    '                                   & "      @USER_CD                    " & vbNewLine _
    '                                   & "      ,@USER_NM         	        " & vbNewLine _
    '                                   & "      ,@PW         		        " & vbNewLine _
    '                                   & "      ,@AUTHO_LV         	        " & vbNewLine _
    '                                   & "      ,@NRS_BR_CD         	    " & vbNewLine _
    '                                   & "      ,@WH_CD         	        " & vbNewLine _
    '                                   & "      ,@BUSYO_CD         	        " & vbNewLine _
    '                                   & "      ,@INKA_DATE_INIT            " & vbNewLine _
    '                                   & "      ,@OUTKA_DATE_INIT           " & vbNewLine _
    '                                   & "      ,@DEF_PRT1         	        " & vbNewLine _
    '                                   & "      ,@DEF_PRT2         	        " & vbNewLine _
    '                                   & "      ,@COA_PRT         	        " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE         	    " & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME         	    " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID         	    " & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER         	    " & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE         	    " & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME         	    " & vbNewLine _
    '                                   & "      ,@SYS_UPD_PGID         	    " & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER         	    " & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG         	    " & vbNewLine _
    '                                   & ")                                 " & vbNewLine
    ''' <summary>
    ''' 新規登録SQL（S_USER）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..S_USER      " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "      USER_CD                     " & vbNewLine _
                                       & "      ,USER_NM                    " & vbNewLine _
                                       & "      ,PW        		            " & vbNewLine _
                                       & "      ,AUTHO_LV                   " & vbNewLine _
                                       & "      ,RIYOUSHA_KBN               " & vbNewLine _
                                       & "      ,NRS_BR_CD        	        " & vbNewLine _
                                       & "      ,WH_CD        		        " & vbNewLine _
                                       & "      ,BUSYO_CD        		    " & vbNewLine _
                                       & "      ,INKA_DATE_INIT        	    " & vbNewLine _
                                       & "      ,OUTKA_DATE_INIT            " & vbNewLine _
                                       & "      ,DEF_PRT1        	        " & vbNewLine _
                                       & "      ,DEF_PRT2        	        " & vbNewLine _
                                       & "      ,DEF_PRT3        	        " & vbNewLine _
                                       & "      ,DEF_PRT4        	        " & vbNewLine _
                                       & "      ,DEF_PRT5        	        " & vbNewLine _
                                       & "      ,DEF_PRT6        	        " & vbNewLine _
                                       & "      ,DEF_PRT7        	        " & vbNewLine _
                                       & "      ,DEF_PRT8        	        " & vbNewLine _
                                       & "      ,COA_PRT         	        " & vbNewLine _
                                       & "      ,EMAIL         	            " & vbNewLine _
                                       & "      ,NOTICE_YN         	        " & vbNewLine _
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
                                       & "      @USER_CD                    " & vbNewLine _
                                       & "      ,@USER_NM         	        " & vbNewLine _
                                       & "      ,@PW         		        " & vbNewLine _
                                       & "      ,@AUTHO_LV         	        " & vbNewLine _
                                       & "      ,@RIYOUSHA_KBN    	        " & vbNewLine _
                                       & "      ,@NRS_BR_CD         	    " & vbNewLine _
                                       & "      ,@WH_CD         	        " & vbNewLine _
                                       & "      ,@BUSYO_CD         	        " & vbNewLine _
                                       & "      ,@INKA_DATE_INIT            " & vbNewLine _
                                       & "      ,@OUTKA_DATE_INIT           " & vbNewLine _
                                       & "      ,@DEF_PRT1         	        " & vbNewLine _
                                       & "      ,@DEF_PRT2         	        " & vbNewLine _
                                       & "      ,@DEF_PRT3         	        " & vbNewLine _
                                       & "      ,@DEF_PRT4         	        " & vbNewLine _
                                       & "      ,@DEF_PRT5         	        " & vbNewLine _
                                       & "      ,@DEF_PRT6         	        " & vbNewLine _
                                       & "      ,@DEF_PRT7         	        " & vbNewLine _
                                       & "      ,@DEF_PRT8         	        " & vbNewLine _
                                       & "      ,@COA_PRT         	        " & vbNewLine _
                                       & "      ,@EMAIL         	        " & vbNewLine _
                                       & "      ,@NOTICE_YN         	    " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         	    " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         	    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         	    " & vbNewLine _
                                       & "      ,@SYS_ENT_USER         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         	    " & vbNewLine _
                                       & "      ,@SYS_UPD_USER         	    " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         	    " & vbNewLine _
                                       & ")                                 " & vbNewLine
    'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

    ''' <summary>
    ''' 新規登録SQL（M_TCUST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TCUST As String = "INSERT INTO $LM_MST$..M_TCUST     " & vbNewLine _
                                       & "(                                       " & vbNewLine _
                                       & "      USER_CD                           " & vbNewLine _
                                       & "      ,USER_CD_EDA                      " & vbNewLine _
                                       & "      ,CUST_CD_L                        " & vbNewLine _
                                       & "      ,CUST_CD_M                        " & vbNewLine _
                                       & "      ,DEFAULT_CUST_YN                  " & vbNewLine _
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
                                       & "      @USER_CD                          " & vbNewLine _
                                       & "      ,@USER_CD_EDA                     " & vbNewLine _
                                       & "      ,@CUST_CD_L                       " & vbNewLine _
                                       & "      ,@CUST_CD_M                       " & vbNewLine _
                                       & "      ,@DEFAULT_CUST_YN                 " & vbNewLine _
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

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 新規登録SQL（M_TUNSOCO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TUNSOCO As String = "INSERT INTO $LM_MST$..M_TUNSOCO         " & vbNewLine _
                                               & "(                                       " & vbNewLine _
                                               & "       USER_CD                          " & vbNewLine _
                                               & "      ,USER_CD_EDA                      " & vbNewLine _
                                               & "      ,UNSOCO_CD                        " & vbNewLine _
                                               & "      ,UNSOCO_BR_CD                     " & vbNewLine _
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
                                               & "       @USER_CD                         " & vbNewLine _
                                               & "      ,@USER_CD_EDA                     " & vbNewLine _
                                               & "      ,@UNSOCO_CD                       " & vbNewLine _
                                               & "      ,@UNSOCO_BR_CD                    " & vbNewLine _
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
    ''' 新規登録SQL（M_TUNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TTARIFF As String = "INSERT INTO $LM_MST$..M_TUNCHIN_TARIFF  " & vbNewLine _
                                               & "(                                       " & vbNewLine _
                                               & "       USER_CD                          " & vbNewLine _
                                               & "      ,USER_CD_EDA                      " & vbNewLine _
                                               & "      ,UNCHIN_TARIFF_CD                 " & vbNewLine _
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
                                               & "       @USER_CD                         " & vbNewLine _
                                               & "      ,@USER_CD_EDA                     " & vbNewLine _
                                               & "      ,@UNCHIN_TARIFF_CD                " & vbNewLine _
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
    '要望番号:1248 yamanaka 2013.03.22 End

#End Region

#Region "Delete"

    ''' <summary>
    ''' 物理削除SQL（M_TCUST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TCUST As String = "DELETE FROM $LM_MST$..M_TCUST    " & vbNewLine _
                                              & "WHERE   USER_CD   = @USER_CD  " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 物理削除SQL（M_TUNSOCO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TUNSOCO As String = "DELETE FROM $LM_MST$..M_TUNSOCO  " & vbNewLine _
                                              & "WHERE USER_CD   = @USER_CD     " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_TUNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_TTARIFF As String = "DELETE FROM $LM_MST$..M_TUNCHIN_TARIFF   " & vbNewLine _
                                              & "WHERE USER_CD   = @USER_CD             " & vbNewLine
    '要望番号:1248 yamanaka 2013.03.22 End


#End Region

#Region "UPDATE"

    'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
    '''' <summary>
    '''' 更新SQL（S_USER）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..S_USER SET                         " & vbNewLine _
    '                                   & "        USER_CD             = @USER_CD              " & vbNewLine _
    '                                   & "       ,USER_NM             = @USER_NM              " & vbNewLine _
    '                                   & "       ,PW                  = @PW                   " & vbNewLine _
    '                                   & "       ,AUTHO_LV            = @AUTHO_LV             " & vbNewLine _
    '                                   & "       ,NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
    '                                   & "       ,WH_CD               = @WH_CD                " & vbNewLine _
    '                                   & "       ,BUSYO_CD            = @BUSYO_CD             " & vbNewLine _
    '                                   & "       ,INKA_DATE_INIT      = @INKA_DATE_INIT       " & vbNewLine _
    '                                   & "       ,OUTKA_DATE_INIT     = @OUTKA_DATE_INIT      " & vbNewLine _
    '                                   & "       ,DEF_PRT1            = @DEF_PRT1             " & vbNewLine _
    '                                   & "       ,DEF_PRT2            = @DEF_PRT2             " & vbNewLine _
    '                                   & "       ,COA_PRT             = @COA_PRT              " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
    '                                   & " WHERE                                              " & vbNewLine _
    '                                   & "         USER_CD            = @USER_CD              " & vbNewLine
    ''' <summary>
    ''' 更新SQL（S_USER）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..S_USER SET                         " & vbNewLine _
                                       & "        USER_CD             = @USER_CD              " & vbNewLine _
                                       & "       ,USER_NM             = @USER_NM              " & vbNewLine _
                                       & "       ,PW                  = @PW                   " & vbNewLine _
                                       & "       ,AUTHO_LV            = @AUTHO_LV             " & vbNewLine _
                                       & "       ,RIYOUSHA_KBN        = @RIYOUSHA_KBN         " & vbNewLine _
                                       & "       ,NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
                                       & "       ,WH_CD               = @WH_CD                " & vbNewLine _
                                       & "       ,BUSYO_CD            = @BUSYO_CD             " & vbNewLine _
                                       & "       ,INKA_DATE_INIT      = @INKA_DATE_INIT       " & vbNewLine _
                                       & "       ,OUTKA_DATE_INIT     = @OUTKA_DATE_INIT      " & vbNewLine _
                                       & "       ,DEF_PRT1            = @DEF_PRT1             " & vbNewLine _
                                       & "       ,DEF_PRT2            = @DEF_PRT2             " & vbNewLine _
                                       & "       ,DEF_PRT3            = @DEF_PRT3             " & vbNewLine _
                                       & "       ,DEF_PRT4            = @DEF_PRT4             " & vbNewLine _
                                       & "       ,DEF_PRT5            = @DEF_PRT5             " & vbNewLine _
                                       & "       ,DEF_PRT6            = @DEF_PRT6             " & vbNewLine _
                                       & "       ,DEF_PRT7            = @DEF_PRT7             " & vbNewLine _
                                       & "       ,DEF_PRT8            = @DEF_PRT8             " & vbNewLine _
                                       & "       ,COA_PRT             = @COA_PRT              " & vbNewLine _
                                       & "       ,YELLOW_CARD_PRT     = @YELLOW_CARD_PRT      " & vbNewLine _
                                       & "       ,EMAIL               = @EMAIL                " & vbNewLine _
                                       & "       ,NOTICE_YN           = @NOTICE_YN            " & vbNewLine _
                                       & "       ,SAP_LINK_AUTHO      = @SAP_LINK_AUTHO       " & vbNewLine _
                                       & "       ,SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                              " & vbNewLine _
                                       & "         USER_CD            = @USER_CD              " & vbNewLine
    'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL（S_USER）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..S_USER SET                           " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         USER_CD              = @USER_CD              " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_TCUST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TCUST As String = "UPDATE $LM_MST$..M_TCUST SET                    " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         USER_CD              = @USER_CD              " & vbNewLine

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 削除・復活SQL（M_TUNSOCO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TUNSOCO As String = "UPDATE $LM_MST$..M_TUNSOCO SET                        " & vbNewLine _
                                               & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                               & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                               & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                               & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                               & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                               & " WHERE                                                " & vbNewLine _
                                               & "         USER_CD              = @USER_CD              " & vbNewLine
    ''' <summary>
    ''' 削除・復活SQL（M_TUNCHIN_TARIFF）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_TTARIFF As String = "UPDATE $LM_MST$..M_TUNCHIN_TARIFF SET                 " & vbNewLine _
                                               & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                               & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                               & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                               & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                               & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                               & " WHERE                                                " & vbNewLine _
                                               & "         USER_CD              = @USER_CD              " & vbNewLine
    '要望番号:1248 yamanaka 2013.03.22 End



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
    ''' ユーザーマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM010DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM010DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' ユーザーマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM010DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM010DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM010DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("USER_CD", "USER_CD")
        map.Add("USER_NM", "USER_NM")
        map.Add("PW", "PW")
        map.Add("AUTHO_LV", "AUTHO_LV")
        map.Add("AUTHO_LV_NM", "AUTHO_LV_NM")
        map.Add("RIYOUSHA_KBN", "RIYOUSHA_KBN")
        map.Add("RIYOUSHA_KBN_NM", "RIYOUSHA_KBN_NM")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("BUSYO_NM", "BUSYO_NM")
        map.Add("INKA_DATE_INIT", "INKA_DATE_INIT")
        map.Add("INKA_DATE_INIT_NM", "INKA_DATE_INIT_NM")
        map.Add("OUTKA_DATE_INIT", "OUTKA_DATE_INIT")
        map.Add("OUTKA_DATE_INIT_NM", "OUTKA_DATE_INIT_NM")
        map.Add("DEF_PRT1", "DEF_PRT1")
        map.Add("DEF_PRT2", "DEF_PRT2")
        'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
        map.Add("DEF_PRT3", "DEF_PRT3")
        map.Add("DEF_PRT4", "DEF_PRT4")
        map.Add("DEF_PRT5", "DEF_PRT5")
        map.Add("DEF_PRT6", "DEF_PRT6")
        map.Add("DEF_PRT7", "DEF_PRT7")
        'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
        map.Add("DEF_PRT8", "DEF_PRT8")
        map.Add("COA_PRT", "COA_PRT")
        map.Add("EMAIL", "EMAIL")
        map.Add("NOTICE_YN", "NOTICE_YN")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("YELLOW_CARD_PRT", "YELLOW_CARD_PRT")
        map.Add("SAP_LINK_AUTHO", "SAP_LINK_AUTHO")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM010OUT")

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
                andstr.Append(" (USER0.SYS_DEL_FLG = @SYS_DEL_FLG  OR USER0.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (USER0.NRS_BR_CD = @NRS_BR_CD OR USER0.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" USER0.USER_CD LIKE @USER_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" USER0.USER_NM LIKE @USER_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("AUTHO_LV").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" USER0.AUTHO_LV = @AUTHO_LV")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AUTHO_LV", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 担当者別荷主マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別荷主マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM010DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM010DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM010DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("USER_CD", "USER_CD")
        map.Add("USER_CD_EDA", "USER_CD_EDA")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("DEFAULT_CUST_YN", "DEFAULT_CUST_YN")
        map.Add("DEFAULT_CUST_YN_NM", "DEFAULT_CUST_YN_NM")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM010_TCUST")

        Return ds

    End Function

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    ''' 担当者別運送会社マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運送会社マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataTunsoco(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM010DAC.SQL_SELECT_TUNSOCO)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM010DAC.SQL_FROM_TUNSOCO)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL3()                     '条件設定
        Me._StrSql.Append(LMM010DAC.SQL_ORDER_BY_TUNSOCO)    'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectListDataTunsoco", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("USER_CD", "USER_CD")
        map.Add("USER_CD_EDA", "USER_CD_EDA")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM010_TUNSOCO")

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別運賃タリフマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運賃タリフマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListDataTtariff(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM010DAC.SQL_SELECT_TTARIFF)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM010DAC.SQL_FROM_TTARIFF)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL4()                     '条件設定
        Me._StrSql.Append(LMM010DAC.SQL_ORDER_BY_TUNCHIN)    'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectListDataTtariff", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("USER_CD", "USER_CD")
        map.Add("USER_CD_EDA", "USER_CD_EDA")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM010_TUNCHIN_TARIFF")

        Return ds

    End Function
    '要望番号:1248 yamanaka 2013.03.21 End

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TCUST)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TCUST.USER_CD LIKE @USER_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    '要望番号:1248 yamanaka 2013.03.29 Start
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TUNSOCO)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL3()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TUNSOCO.USER_CD LIKE @USER_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_TUNCHIN_TARIFF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL4()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" TTARIFF.USER_CD LIKE @USER_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub
    '要望番号:1248 yamanaka 2013.03.29 End

    ''' <summary>
    ''' MAXユーザーコード枝番取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>MAXユーザーコード枝番取得SQLの構築・発行</remarks>
    Private Function SelectMaxUserCdEdaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM010DAC.SQL_SELECT_MAX)        'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM010DAC.SQL_FROM_MAX)          'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectMaxUserCdEdaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("USER_CD_MAXEDA", "USER_CD_MAXEDA")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM010_TCUST_MAXEDA")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' ユーザーマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectUserM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM010DAC.SQL_EXIT_USER)
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

        MyBase.Logger.WriteSQLLog("LMM010DAC", "SelectUserM", cmd)

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
    ''' ユーザーマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistUserM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_EXIT_USER, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "CheckExistUserM", cmd)

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
    ''' ユーザーマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertUserM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "InsertUserM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別荷主マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別荷主マスタ新規登録SQLの構築・発行</remarks>
    Private Function DelTcustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DEL_TCUST, ""))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DelTcustM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別荷主マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別荷主マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTcustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010_TCUST")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_INSERT_TCUST, ""))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            'Call Me.SetParamTcustInsert()
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTcustParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM010DAC", "InsertTcustM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 担当者別運送会社マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運送会社マスタ新規登録SQLの構築・発行</remarks>
    Private Function DelTunsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DEL_TUNSOCO, ""))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DelTunsocoM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別運送会社マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運送会社マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTunsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010_TUNSOCO")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_INSERT_TUNSOCO, ""))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            'Call Me.SetParamTcustInsert()
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTunsocoParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM010DAC", "InsertTunsocoM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別運賃タリフマスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運賃タリフマスタ新規登録SQLの構築・発行</remarks>
    Private Function DelTtariffM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DEL_TTARIFF, ""))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DelTtariffM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別運賃タリフマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運賃タリフマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTtariffM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010_TUNCHIN_TARIFF")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_INSERT_TTARIFF, ""))
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            'Call Me.SetParamTcustInsert()
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetTtariffParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM010DAC", "InsertTtariffM", cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    '要望番号:1248 yamanaka 2013.03.22 End

#End Region

#Region "Update"

    ''' <summary>
    ''' ユーザーマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateUserM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM010DAC.SQL_UPDATE _
                                                                                     , LMM010DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "UpdateUserM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "Delete"

    ''' <summary>
    ''' ユーザーマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ユーザーマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteUserM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DeleteUserM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別荷主マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別荷主マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTcustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DELETE_TCUST, ""))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DeleteUserM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 担当者別運送会社マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運送会社マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTunsocoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DELETE_TUNSOCO, ""))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DeleteTunsocoM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 担当者別運賃タリフマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>担当者別運賃タリフマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteTtariffM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM010DAC.SQL_DELETE_TTARIFF, ""))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM010DAC", "DeleteTtariffM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function
    '要望番号:1248 yamanaka 2013.03.22 End

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
    ''' パラメータ設定モジュール(ユーザーマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD").ToString(), DBDataType.CHAR))

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
    ''' パラメータ設定モジュール(新規登録_ユーザーＭ)
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
    ''' パラメータ設定モジュール(更新登録_ユーザーＭ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_NM", .Item("USER_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PW", .Item("PW").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AUTHO_LV", .Item("AUTHO_LV").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RIYOUSHA_KBN", .Item("RIYOUSHA_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_INIT", .Item("INKA_DATE_INIT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_INIT", .Item("OUTKA_DATE_INIT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT1", .Item("DEF_PRT1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT2", .Item("DEF_PRT2").ToString(), DBDataType.NVARCHAR))
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT3", .Item("DEF_PRT3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT4", .Item("DEF_PRT4").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT5", .Item("DEF_PRT5").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT6", .Item("DEF_PRT6").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT7", .Item("DEF_PRT7").ToString(), DBDataType.NVARCHAR))
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEF_PRT8", .Item("DEF_PRT8").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_PRT", .Item("COA_PRT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD_PRT", .Item("YELLOW_CARD_PRT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EMAIL", .Item("EMAIL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOTICE_YN", .Item("NOTICE_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_LINK_AUTHO", .Item("SAP_LINK_AUTHO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_担当者別荷主Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTcustParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@USER_CD_EDA", .Item("USER_CD_EDA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEFAULT_CUST_YN", .Item("DEFAULT_CUST_YN").ToString(), DBDataType.CHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", Me._Row.Item("USER_CD").ToString(), DBDataType.CHAR))
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

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_担当者別運送会社Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTunsocoParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@USER_CD_EDA", .Item("USER_CD_EDA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_担当者別運賃タリフＭ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTtariffParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@USER_CD", .Item("USER_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@USER_CD_EDA", .Item("USER_CD_EDA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub
    '要望番号:1248 yamanaka 2013.03.22 End

#End Region

#End Region

#End Region

End Class
