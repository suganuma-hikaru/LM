' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF050DAC : 運賃編集
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"


    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(UNCHIN.UNSO_NO_L)		   AS SELECT_CNT   " & vbNewLine

    'START YANAI 要望番号376
    '''' <summary>
    '''' F_UNCHIN_TRSデータ抽出用
    '''' </summary>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
    '                                        & "  UNCHIN.NRS_BR_CD             AS NRS_BR_CD                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_L             AS UNSO_NO_L                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_M             AS UNSO_NO_M                    " & vbNewLine _
    '                                        & " ,DEST.JIS                     AS DEST_JIS_CD                  " & vbNewLine _
    '                                        & " ,UNSO.DEST_CD                 AS DEST_CD                      " & vbNewLine _
    '                                        & " ,DEST.DEST_NM                 AS DEST_NM                      " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_NM             AS UNSOCO_NM                    " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD               " & vbNewLine _
    '                                        & " ,CASE WHEN UNCHIN.SEIQ_TARIFF_BUNRUI_KB = '40'                " & vbNewLine _
    '                                        & "       THEN M49_01.YOKO_REM                                    " & vbNewLine _
    '                                        & "       ELSE M47_01.UNCHIN_TARIFF_REM                           " & vbNewLine _
    '                                        & "       END                     AS SEIQ_TARIFF_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD              " & vbNewLine _
    '                                        & " ,M44_01.EXTC_TARIFF_REM       AS SEIQ_ETARIFF_NM              " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_WT               AS SEIQ_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_NG_NB            AS DECI_NG_NB                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_KYORI            AS DECI_KYORI                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WT               AS DECI_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_UNCHIN           AS DECI_UNCHIN                  " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_CITY_EXTC        AS DECI_CITY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WINT_EXTC        AS DECI_WINT_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_TOLL             AS DECI_TOLL                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_INSU             AS DECI_INSU                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_RELY_EXTC        AS DECI_RELY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_UNCHIN          AS KANRI_UNCHIN                 " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_TOLL            AS KANRI_TOLL                   " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_INSU            AS KANRI_INSU                   " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO                " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M              " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNSO.TRIP_NO                 AS TRIP_NO                      " & vbNewLine _
    '                                        & " ,KBN2.KBN_NM1                 AS UNSO_ONDO_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_PKG_UT           AS SEIQ_PKG_UT_KB               " & vbNewLine _
    '                                        & " ,KBN3.KBN_NM1                 AS SEIQ_PKG_UT_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB               " & vbNewLine _
    '                                        & " ,KBN4.KBN_NM1                 AS SEIQ_SYARYO_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB               " & vbNewLine _
    '                                        & " ,KBN5.KBN_NM1                 AS SEIQ_DANGER_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG              " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_DATE            AS SYS_UPD_DATE                 " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_TIME            AS SYS_UPD_TIME                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB        " & vbNewLine _
    '                                        & " ,KBN1.KBN_NM1                 AS SEIQ_TARIFF_BUNRUI_NM        " & vbNewLine _
    '                                        & " ,UNSO.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE              " & vbNewLine _
    '                                        & " ,UNSO.ARR_PLAN_DATE           AS ARR_PLAN_DATE                " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_L               AS CUST_CD_L                    " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_M               AS CUST_CD_M                    " & vbNewLine _
    '                                        & " ,UNCHIN.TAX_KB                AS TAX_KB                       " & vbNewLine _
    '                                        & " ,UNSO.MOTO_DATA_KB            AS MOTO_DATA_KB                 " & vbNewLine _
    '                                        & " ,UNSO.UNSO_ONDO_KB            AS UNSO_ONDO_KB                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQTO_CD             AS SEIQTO_CD                    " & vbNewLine _
    '                                        & " ,KBN6.KBN_NM1                 AS TAX_NM                       " & vbNewLine _
    '                                        & " ,UNCHIN.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB         " & vbNewLine _
    '                                        & " ,0                            AS UNSO_TTL_QT                  " & vbNewLine _
    '                                        & " ,UNCHIN.SIZE_KB               AS SIZE_KB                      " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_L               AS CUST_NM_L                    " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_M               AS CUST_NM_M                    " & vbNewLine
    'START YANAI 要望番号891
    '''' <summary>
    '''' F_UNCHIN_TRSデータ抽出用
    '''' </summary>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
    '                                        & "  UNCHIN.NRS_BR_CD             AS NRS_BR_CD                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_L             AS UNSO_NO_L                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_M             AS UNSO_NO_M                    " & vbNewLine _
    '                                        & " ,ISNULL(DEST.JIS,DEST2.JIS)   AS DEST_JIS_CD                  " & vbNewLine _
    '                                        & " ,UNSO.DEST_CD                 AS DEST_CD                      " & vbNewLine _
    '                                        & " ,ISNULL(DEST.DEST_NM,DEST2.DEST_NM) AS DEST_NM                " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_NM             AS UNSOCO_NM                    " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD               " & vbNewLine _
    '                                        & " ,CASE WHEN UNCHIN.SEIQ_TARIFF_BUNRUI_KB = '40'                " & vbNewLine _
    '                                        & "       THEN M49_01.YOKO_REM                                    " & vbNewLine _
    '                                        & "       ELSE M47_01.UNCHIN_TARIFF_REM                           " & vbNewLine _
    '                                        & "       END                     AS SEIQ_TARIFF_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD              " & vbNewLine _
    '                                        & " ,M44_01.EXTC_TARIFF_REM       AS SEIQ_ETARIFF_NM              " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_WT               AS SEIQ_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_NG_NB            AS DECI_NG_NB                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_KYORI            AS DECI_KYORI                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WT               AS DECI_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_UNCHIN           AS DECI_UNCHIN                  " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_CITY_EXTC        AS DECI_CITY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WINT_EXTC        AS DECI_WINT_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_TOLL             AS DECI_TOLL                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_INSU             AS DECI_INSU                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_RELY_EXTC        AS DECI_RELY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_UNCHIN          AS KANRI_UNCHIN                 " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_TOLL            AS KANRI_TOLL                   " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_INSU            AS KANRI_INSU                   " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO                " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M              " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNSO.TRIP_NO                 AS TRIP_NO                      " & vbNewLine _
    '                                        & " ,KBN2.KBN_NM1                 AS UNSO_ONDO_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_PKG_UT           AS SEIQ_PKG_UT_KB               " & vbNewLine _
    '                                        & " ,KBN3.KBN_NM1                 AS SEIQ_PKG_UT_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB               " & vbNewLine _
    '                                        & " ,KBN4.KBN_NM1                 AS SEIQ_SYARYO_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB               " & vbNewLine _
    '                                        & " ,KBN5.KBN_NM1                 AS SEIQ_DANGER_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG              " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_DATE            AS SYS_UPD_DATE                 " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_TIME            AS SYS_UPD_TIME                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB        " & vbNewLine _
    '                                        & " ,KBN1.KBN_NM1                 AS SEIQ_TARIFF_BUNRUI_NM        " & vbNewLine _
    '                                        & " ,UNSO.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE              " & vbNewLine _
    '                                        & " ,UNSO.ARR_PLAN_DATE           AS ARR_PLAN_DATE                " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_L               AS CUST_CD_L                    " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_M               AS CUST_CD_M                    " & vbNewLine _
    '                                        & " ,UNCHIN.TAX_KB                AS TAX_KB                       " & vbNewLine _
    '                                        & " ,UNSO.MOTO_DATA_KB            AS MOTO_DATA_KB                 " & vbNewLine _
    '                                        & " ,UNSO.UNSO_ONDO_KB            AS UNSO_ONDO_KB                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQTO_CD             AS SEIQTO_CD                    " & vbNewLine _
    '                                        & " ,KBN6.KBN_NM1                 AS TAX_NM                       " & vbNewLine _
    '                                        & " ,UNCHIN.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB         " & vbNewLine _
    '                                        & " ,0                            AS UNSO_TTL_QT                  " & vbNewLine _
    '                                        & " ,UNCHIN.SIZE_KB               AS SIZE_KB                      " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_L               AS CUST_NM_L                    " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_M               AS CUST_NM_M                    " & vbNewLine
    'START KAI 要望番号707
    '''' <summary>
    '''' F_UNCHIN_TRSデータ抽出用
    '''' </summary>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
    '                                        & "  UNCHIN.NRS_BR_CD             AS NRS_BR_CD                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_L             AS UNSO_NO_L                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_M             AS UNSO_NO_M                    " & vbNewLine _
    '                                        & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
    '                                        & "      THEN UNSO.ORIG_CD                                        " & vbNewLine _
    '                                        & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
    '                                        & "      THEN UNSO.DEST_CD                                        " & vbNewLine _
    '                                        & "      ELSE UNSO.DEST_CD                                        " & vbNewLine _
    '                                        & " END                             AS DEST_CD                    " & vbNewLine _
    '                                        & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST3.DEST_NM,DEST4.DEST_NM)                 " & vbNewLine _
    '                                        & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                  " & vbNewLine _
    '                                        & "      ELSE ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                  " & vbNewLine _
    '                                        & " END                             AS DEST_NM                    " & vbNewLine _
    '                                        & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST3.JIS,DEST4.JIS)                         " & vbNewLine _
    '                                        & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST.JIS,DEST2.JIS)                          " & vbNewLine _
    '                                        & "      ELSE ISNULL(DEST.JIS,DEST2.JIS)                          " & vbNewLine _
    '                                        & " END                             AS DEST_JIS_CD                " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_NM             AS UNSOCO_NM                    " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD               " & vbNewLine _
    '                                        & " ,CASE WHEN UNCHIN.SEIQ_TARIFF_BUNRUI_KB = '40'                " & vbNewLine _
    '                                        & "       THEN M49_01.YOKO_REM                                    " & vbNewLine _
    '                                        & "       ELSE M47_01.UNCHIN_TARIFF_REM                           " & vbNewLine _
    '                                        & "       END                     AS SEIQ_TARIFF_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD              " & vbNewLine _
    '                                        & " ,M44_01.EXTC_TARIFF_REM       AS SEIQ_ETARIFF_NM              " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_WT               AS SEIQ_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_NG_NB            AS DECI_NG_NB                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_KYORI            AS DECI_KYORI                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WT               AS DECI_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_UNCHIN           AS DECI_UNCHIN                  " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_CITY_EXTC        AS DECI_CITY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WINT_EXTC        AS DECI_WINT_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_TOLL             AS DECI_TOLL                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_INSU             AS DECI_INSU                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_RELY_EXTC        AS DECI_RELY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_UNCHIN          AS KANRI_UNCHIN                 " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_TOLL            AS KANRI_TOLL                   " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_INSU            AS KANRI_INSU                   " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO                " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M              " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNSO.TRIP_NO                 AS TRIP_NO                      " & vbNewLine _
    '                                        & " ,KBN2.KBN_NM1                 AS UNSO_ONDO_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_PKG_UT           AS SEIQ_PKG_UT_KB               " & vbNewLine _
    '                                        & " ,KBN3.KBN_NM1                 AS SEIQ_PKG_UT_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB               " & vbNewLine _
    '                                        & " ,KBN4.KBN_NM1                 AS SEIQ_SYARYO_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB               " & vbNewLine _
    '                                        & " ,KBN5.KBN_NM1                 AS SEIQ_DANGER_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG              " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_DATE            AS SYS_UPD_DATE                 " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_TIME            AS SYS_UPD_TIME                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB        " & vbNewLine _
    '                                        & " ,KBN1.KBN_NM1                 AS SEIQ_TARIFF_BUNRUI_NM        " & vbNewLine _
    '                                        & " ,UNSO.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE              " & vbNewLine _
    '                                        & " ,UNSO.ARR_PLAN_DATE           AS ARR_PLAN_DATE                " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_L               AS CUST_CD_L                    " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_M               AS CUST_CD_M                    " & vbNewLine _
    '                                        & " ,UNCHIN.TAX_KB                AS TAX_KB                       " & vbNewLine _
    '                                        & " ,UNSO.MOTO_DATA_KB            AS MOTO_DATA_KB                 " & vbNewLine _
    '                                        & " ,UNSO.UNSO_ONDO_KB            AS UNSO_ONDO_KB                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQTO_CD             AS SEIQTO_CD                    " & vbNewLine _
    '                                        & " ,KBN6.KBN_NM1                 AS TAX_NM                       " & vbNewLine _
    '                                        & " ,UNCHIN.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB         " & vbNewLine _
    '                                        & " ,0                            AS UNSO_TTL_QT                  " & vbNewLine _
    '                                        & " ,UNCHIN.SIZE_KB               AS SIZE_KB                      " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_L               AS CUST_NM_L                    " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_M               AS CUST_NM_M                    " & vbNewLine
    'START YANAI 要望番号974 運賃が0円で、他の割増運賃関係が0円以外の場合エラーとする
    '''' <summary>
    '''' F_UNCHIN_TRSデータ抽出用
    '''' </summary>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
    '                                        & "  UNCHIN.NRS_BR_CD             AS NRS_BR_CD                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_L             AS UNSO_NO_L                    " & vbNewLine _
    '                                        & " ,UNCHIN.UNSO_NO_M             AS UNSO_NO_M                    " & vbNewLine _
    '                                        & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
    '                                        & "      THEN UNSO.ORIG_CD                                        " & vbNewLine _
    '                                        & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
    '                                        & "      THEN UNSO.DEST_CD                                        " & vbNewLine _
    '                                        & "      ELSE UNSO.DEST_CD                                        " & vbNewLine _
    '                                        & " END                             AS DEST_CD                    " & vbNewLine _
    '                                        & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST3.DEST_NM,DEST4.DEST_NM)                 " & vbNewLine _
    '                                        & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                  " & vbNewLine _
    '                                        & "      ELSE ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                  " & vbNewLine _
    '                                        & " END                             AS DEST_NM                    " & vbNewLine _
    '                                        & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST3.JIS,DEST4.JIS)                         " & vbNewLine _
    '                                        & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
    '                                        & "      THEN ISNULL(DEST.JIS,DEST2.JIS)                          " & vbNewLine _
    '                                        & "      ELSE ISNULL(DEST.JIS,DEST2.JIS)                          " & vbNewLine _
    '                                        & " END                             AS DEST_JIS_CD                " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_NM             AS UNSOCO_NM                    " & vbNewLine _
    '                                        & " ,UNSOCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD               " & vbNewLine _
    '                                        & " ,CASE WHEN UNCHIN.SEIQ_TARIFF_BUNRUI_KB = '40'                " & vbNewLine _
    '                                        & "       THEN M49_01.YOKO_REM                                    " & vbNewLine _
    '                                        & "       ELSE M47_01.UNCHIN_TARIFF_REM                           " & vbNewLine _
    '                                        & "       END                     AS SEIQ_TARIFF_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD              " & vbNewLine _
    '                                        & " ,M44_01.EXTC_TARIFF_REM       AS SEIQ_ETARIFF_NM              " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_WT               AS SEIQ_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_NG_NB            AS DECI_NG_NB                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_KYORI            AS DECI_KYORI                   " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WT               AS DECI_WT                      " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_UNCHIN           AS DECI_UNCHIN                  " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_CITY_EXTC        AS DECI_CITY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_WINT_EXTC        AS DECI_WINT_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_TOLL             AS DECI_TOLL                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_INSU             AS DECI_INSU                    " & vbNewLine _
    '                                        & " ,UNCHIN.DECI_RELY_EXTC        AS DECI_RELY_EXTC               " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_UNCHIN          AS KANRI_UNCHIN                 " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC              " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_TOLL            AS KANRI_TOLL                   " & vbNewLine _
    '                                        & " ,UNCHIN.KANRI_INSU            AS KANRI_INSU                   " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO                " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M              " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNSO.TRIP_NO                 AS TRIP_NO                      " & vbNewLine _
    '                                        & " ,KBN2.KBN_NM1                 AS UNSO_ONDO_NM                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_PKG_UT           AS SEIQ_PKG_UT_KB               " & vbNewLine _
    '                                        & " ,KBN3.KBN_NM1                 AS SEIQ_PKG_UT_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB               " & vbNewLine _
    '                                        & " ,KBN4.KBN_NM1                 AS SEIQ_SYARYO_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB               " & vbNewLine _
    '                                        & " ,KBN5.KBN_NM1                 AS SEIQ_DANGER_NM               " & vbNewLine _
    '                                        & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG              " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_DATE            AS SYS_UPD_DATE                 " & vbNewLine _
    '                                        & " ,UNSO.SYS_UPD_TIME            AS SYS_UPD_TIME                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB        " & vbNewLine _
    '                                        & " ,KBN1.KBN_NM1                 AS SEIQ_TARIFF_BUNRUI_NM        " & vbNewLine _
    '                                        & " ,UNSO.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE              " & vbNewLine _
    '                                        & " ,UNSO.ARR_PLAN_DATE           AS ARR_PLAN_DATE                " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_L               AS CUST_CD_L                    " & vbNewLine _
    '                                        & " ,UNSO.CUST_CD_M               AS CUST_CD_M                    " & vbNewLine _
    '                                        & " ,UNCHIN.TAX_KB                AS TAX_KB                       " & vbNewLine _
    '                                        & " ,UNSO.MOTO_DATA_KB            AS MOTO_DATA_KB                 " & vbNewLine _
    '                                        & " ,UNSO.UNSO_ONDO_KB            AS UNSO_ONDO_KB                 " & vbNewLine _
    '                                        & " ,UNCHIN.SEIQTO_CD             AS SEIQTO_CD                    " & vbNewLine _
    '                                        & " ,KBN6.KBN_NM1                 AS TAX_NM                       " & vbNewLine _
    '                                        & " ,UNCHIN.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB         " & vbNewLine _
    '                                        & " ,0                            AS UNSO_TTL_QT                  " & vbNewLine _
    '                                        & " ,UNCHIN.SIZE_KB               AS SIZE_KB                      " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_L               AS CUST_NM_L                    " & vbNewLine _
    '                                        & " ,CUST.CUST_NM_M               AS CUST_NM_M                    " & vbNewLine _
    '                                        & " ,UNSO.INOUTKA_NO_L            AS INOUTKA_NO_L                 " & vbNewLine
    ''' <summary>
    ''' F_UNCHIN_TRSデータ抽出用
    ''' </summary>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                        " & vbNewLine _
                                            & "  UNCHIN.NRS_BR_CD             AS NRS_BR_CD                    " & vbNewLine _
                                            & " ,UNCHIN.UNSO_NO_L             AS UNSO_NO_L                    " & vbNewLine _
                                            & " ,UNCHIN.UNSO_NO_M             AS UNSO_NO_M                    " & vbNewLine _
                                            & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
                                            & "      THEN UNSO.ORIG_CD                                        " & vbNewLine _
                                            & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                            & "      THEN UNSO.DEST_CD                                        " & vbNewLine _
                                            & "      ELSE UNSO.DEST_CD                                        " & vbNewLine _
                                            & " END                             AS DEST_CD                    " & vbNewLine _
                                            & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
                                            & "      THEN ISNULL(DEST3.DEST_NM,DEST4.DEST_NM)                 " & vbNewLine _
                                            & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                            & "      THEN ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                  " & vbNewLine _
                                            & "      ELSE ISNULL(DEST.DEST_NM,DEST2.DEST_NM)                  " & vbNewLine _
                                            & " END                             AS DEST_NM                    " & vbNewLine _
                                            & ",CASE WHEN UNSO.MOTO_DATA_KB = '10'                            " & vbNewLine _
                                            & "      THEN ISNULL(DEST3.JIS,DEST4.JIS)                         " & vbNewLine _
                                            & "      WHEN UNSO.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                            & "      THEN ISNULL(DEST.JIS,DEST2.JIS)                          " & vbNewLine _
                                            & "      ELSE ISNULL(DEST.JIS,DEST2.JIS)                          " & vbNewLine _
                                            & " END                             AS DEST_JIS_CD                " & vbNewLine _
                                            & " ,UNSOCO.UNSOCO_NM             AS UNSOCO_NM                    " & vbNewLine _
                                            & " ,UNSOCO.UNSOCO_BR_NM          AS UNSOCO_BR_NM                 " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD               " & vbNewLine _
                                            & " ,CASE WHEN UNCHIN.SEIQ_TARIFF_BUNRUI_KB = '40'                " & vbNewLine _
                                            & "       THEN M49_01.YOKO_REM                                    " & vbNewLine _
                                            & "       ELSE M47_01.UNCHIN_TARIFF_REM                           " & vbNewLine _
                                            & "       END                     AS SEIQ_TARIFF_NM               " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD              " & vbNewLine _
                                            & " ,M44_01.EXTC_TARIFF_REM       AS SEIQ_ETARIFF_NM              " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_WT               AS SEIQ_WT                      " & vbNewLine _
                                            & " ,UNCHIN.DECI_NG_NB            AS DECI_NG_NB                   " & vbNewLine _
                                            & " ,UNCHIN.DECI_KYORI            AS DECI_KYORI                   " & vbNewLine _
                                            & " ,UNCHIN.DECI_WT               AS DECI_WT                      " & vbNewLine _
                                            & " ,UNCHIN.DECI_UNCHIN           AS DECI_UNCHIN                  " & vbNewLine _
                                            & " ,UNCHIN.DECI_CITY_EXTC        AS DECI_CITY_EXTC               " & vbNewLine _
                                            & " ,UNCHIN.DECI_WINT_EXTC        AS DECI_WINT_EXTC               " & vbNewLine _
                                            & " ,UNCHIN.DECI_TOLL             AS DECI_TOLL                    " & vbNewLine _
                                            & " ,UNCHIN.DECI_INSU             AS DECI_INSU                    " & vbNewLine _
                                            & " ,UNCHIN.DECI_RELY_EXTC        AS DECI_RELY_EXTC               " & vbNewLine _
                                            & " ,UNCHIN.KANRI_UNCHIN          AS KANRI_UNCHIN                 " & vbNewLine _
                                            & " ,UNCHIN.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC              " & vbNewLine _
                                            & " ,UNCHIN.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC              " & vbNewLine _
                                            & " ,UNCHIN.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC              " & vbNewLine _
                                            & " ,UNCHIN.KANRI_TOLL            AS KANRI_TOLL                   " & vbNewLine _
                                            & " ,UNCHIN.KANRI_INSU            AS KANRI_INSU                   " & vbNewLine _
                                            & " ,CUST.ITEM_CURR_CD            AS ITEM_CURR_CD                 " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO                " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M              " & vbNewLine _
                                            & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
                                            & " ,UNSO.TRIP_NO                 AS TRIP_NO                      " & vbNewLine _
                                            & " ,KBN2.KBN_NM1                 AS UNSO_ONDO_NM                 " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_PKG_UT           AS SEIQ_PKG_UT_KB               " & vbNewLine _
                                            & " ,KBN3.KBN_NM1                 AS SEIQ_PKG_UT_NM               " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB               " & vbNewLine _
                                            & " ,KBN4.KBN_NM1                 AS SEIQ_SYARYO_NM               " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB               " & vbNewLine _
                                            & " ,KBN5.KBN_NM1                 AS SEIQ_DANGER_NM               " & vbNewLine _
                                            & " ,UNCHIN.REMARK                AS REMARK                       " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG              " & vbNewLine _
                                            & " ,UNSO.SYS_UPD_DATE            AS SYS_UPD_DATE                 " & vbNewLine _
                                            & " ,UNSO.SYS_UPD_TIME            AS SYS_UPD_TIME                 " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB        " & vbNewLine _
                                            & " ,KBN1.KBN_NM1                 AS SEIQ_TARIFF_BUNRUI_NM        " & vbNewLine _
                                            & " ,UNSO.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE              " & vbNewLine _
                                            & " ,UNSO.ARR_PLAN_DATE           AS ARR_PLAN_DATE                " & vbNewLine _
                                            & " ,UNSO.CUST_CD_L               AS CUST_CD_L                    " & vbNewLine _
                                            & " ,UNSO.CUST_CD_M               AS CUST_CD_M                    " & vbNewLine _
                                            & " ,UNCHIN.TAX_KB                AS TAX_KB                       " & vbNewLine _
                                            & " ,UNSO.MOTO_DATA_KB            AS MOTO_DATA_KB                 " & vbNewLine _
                                            & " ,UNSO.UNSO_ONDO_KB            AS UNSO_ONDO_KB                 " & vbNewLine _
                                            & " ,UNCHIN.SEIQTO_CD             AS SEIQTO_CD                    " & vbNewLine _
                                            & " ,KBN6.KBN_NM1                 AS TAX_NM                       " & vbNewLine _
                                            & " ,UNCHIN.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB         " & vbNewLine _
                                            & " ,0                            AS UNSO_TTL_QT                  " & vbNewLine _
                                            & " ,UNCHIN.SIZE_KB               AS SIZE_KB                      " & vbNewLine _
                                            & " ,CUST.CUST_NM_L               AS CUST_NM_L                    " & vbNewLine _
                                            & " ,CUST.CUST_NM_M               AS CUST_NM_M                    " & vbNewLine _
                                            & " ,UNSO.INOUTKA_NO_L            AS INOUTKA_NO_L                 " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_UNCHIN           AS SEIQ_UNCHIN                  " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_CITY_EXTC        AS SEIQ_CITY_EXTC               " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_WINT_EXTC        AS SEIQ_WINT_EXTC               " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_RELY_EXTC        AS SEIQ_RELY_EXTC               " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_TOLL             AS SEIQ_TOLL                    " & vbNewLine _
                                            & " ,UNCHIN.SEIQ_INSU             AS SEIQ_INSU                    " & vbNewLine _
    'END YANAI 要望番号974 運賃が0円で、他の割増運賃関係が0円以外の場合エラーとする
    'END KAI 要望番号707
    'END YANAI 要望番号891
    'END YANAI 要望番号376

#End Region

#Region "FROM句"

    'START YANAI 要望番号376
    'Private Const SQL_FROM_DATA As String = "FROM                                                                                                                                    " & vbNewLine _
    '                                      & "                      $LM_TRN$..F_UNCHIN_TRS AS UNCHIN                                                                                  " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L     AS UNSO                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.UNSO_NO_L                   = UNSO.UNSO_NO_L                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST                                                                                    " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = DEST.NRS_BR_CD                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.CUST_CD_L                     = DEST.CUST_CD_L                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.DEST_CD                       = DEST.DEST_CD                                                                            " & vbNewLine _
    '                                      & "       AND DEST.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO     AS UNSOCO                                                                                  " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = UNSOCO.NRS_BR_CD                                                                        " & vbNewLine _
    '                                      & "       AND UNSO.UNSO_CD                       = UNSOCO.UNSOCO_CD                                                                        " & vbNewLine _
    '                                      & "       AND UNSO.UNSO_BR_CD                    = UNSOCO.UNSOCO_BR_CD                                                                     " & vbNewLine _
    '                                      & "       AND UNSOCO.SYS_DEL_FLG                 = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN2                                                                                    " & vbNewLine _
    '                                      & "        ON UNSO.UNSO_ONDO_KB                  = KBN2.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN2.KBN_GROUP_CD                  = 'U006'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN2.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN3                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_PKG_UT                 = KBN3.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN3.KBN_GROUP_CD                  = 'N001'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN3.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN4                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_SYARYO_KB              = KBN4.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN4.KBN_GROUP_CD                  = 'S012'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN4.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN5                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_DANGER_KB              = KBN5.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN5.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "       AND KBN5.KBN_GROUP_CD                  = 'K008'                                                                                  " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN1                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_TARIFF_BUNRUI_KB       = KBN1.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN1.KBN_GROUP_CD                  = 'T015'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN1.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN6                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.TAX_KB                      = KBN6.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN6.KBN_GROUP_CD                  = 'Z001'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN6.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_CUST       AS CUST                                                                                    " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = CUST.NRS_BR_CD                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.CUST_CD_L                     = CUST.CUST_CD_L                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.CUST_CD_M                     = CUST.CUST_CD_M                                                                          " & vbNewLine _
    '                                      & "       AND CUST.CUST_CD_S                     = '00'                                                                                    " & vbNewLine _
    '                                      & "       AND CUST.CUST_CD_SS                    = '00'                                                                                    " & vbNewLine _
    '                                      & "       AND CUST.SYS_DEL_FLG                   ='0'                                                                                      " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
    '                                      & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
    '                                      & "                      ,M47_02.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
    '                                      & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                                " & vbNewLine _
    '                                      & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                                 " & vbNewLine _
    '                                      & "                 INNER JOIN (                                                                                                           " & vbNewLine _
    '                                      & "                                    SELECT M47_02.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
    '                                      & "                                          ,M47_02.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
    '                                      & "                                          ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD                                         " & vbNewLine _
    '                                      & "                                          ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
    '                                      & "                                          ,M47_02.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
    '                                      & "                                      FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                             " & vbNewLine _
    '                                      & "                                INNER JOIN (                                                                                            " & vbNewLine _
    '                                      & "                                                    SELECT F02_11.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                          ,MAX(M47_01.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
    '                                      & "                                                      FROM $LM_TRN$..F_UNSO_L F02_11                                                    " & vbNewLine _
    '                                      & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_01                                                 " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD   = M07_01.NRS_BR_CD                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_L   = M07_01.CUST_CD_L                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_M   = M07_01.CUST_CD_M                                        " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_S   = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_SS  = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                      & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
    '                                      & "                                                                             SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
    '                                      & "                                                                               FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
    '                                      & "                                                                              WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                      & "                                                                           GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                                )                   M47_01                                              " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD       = M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                       AND F02_11.SEIQ_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                       AND CASE WHEN M07_01.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
    '                                      & "                                                                THEN F02_11.OUTKA_PLAN_DATE                                             " & vbNewLine _
    '                                      & "                                                                ELSE F02_11.ARR_PLAN_DATE                                               " & vbNewLine _
    '                                      & "                                                            END                  >= M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                     WHERE F02_11.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
    '                                      & "                                                  GROUP BY F02_11.NRS_BR_CD                                                             " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L                                                             " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
    '                                      & "                                           )                      M47_02                                                                " & vbNewLine _
    '                                      & "                                     ON M47_01.NRS_BR_CD        = M47_02.NRS_BR_CD                                                      " & vbNewLine _
    '                                      & "                                    AND M47_01.UNCHIN_TARIFF_CD = M47_02.UNCHIN_TARIFF_CD                                               " & vbNewLine _
    '                                      & "                                    AND M47_01.STR_DATE         = M47_02.STR_DATE                                                       " & vbNewLine _
    '                                      & "                                  WHERE M47_01.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
    '                                      & "                               GROUP BY M47_02.NRS_BR_CD                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_02.UNSO_NO_L                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_01.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
    '                                      & "                                       ,M47_02.STR_DATE                                                                                 " & vbNewLine _
    '                                      & "                            )                          M47_02                                                                           " & vbNewLine _
    '                                      & "                      ON M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                                                 " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
    '                                      & "                     AND M47_01.STR_DATE             = M47_02.STR_DATE                                                                  " & vbNewLine _
    '                                      & "                   WHERE M47_01.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
    '                                      & "          )                               M47_01                                                                                        " & vbNewLine _
    '                                      & "  ON  UNCHIN.NRS_BR_CD                  = M47_01.NRS_BR_CD                                                                              " & vbNewLine _
    '                                      & " AND  UNCHIN.UNSO_NO_L                  = M47_01.UNSO_NO_L                                                                              " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD     M49_01                                                                                        " & vbNewLine _
    '                                      & "  ON  UNCHIN.NRS_BR_CD                  = M49_01.NRS_BR_CD                                                                              " & vbNewLine _
    '                                      & " AND  UNCHIN.SEIQ_TARIFF_CD             = M49_01.YOKO_TARIFF_CD                                                                         " & vbNewLine _
    '                                      & " AND  M49_01.SYS_DEL_FLG                = '0'                                                                                           " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
    '                                      & "              SELECT NRS_BR_CD       AS NRS_BR_CD                                                                                       " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD                                                                                  " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM                                                                                 " & vbNewLine _
    '                                      & "                FROM $LM_MST$..M_EXTC_UNCHIN                                                                                            " & vbNewLine _
    '                                      & "               WHERE JIS_CD      = '0000000'                                                                                            " & vbNewLine _
    '                                      & "                 AND SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
    '                                      & "            GROUP BY NRS_BR_CD                                                                                                          " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD                                                                                                     " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM                                                                                                    " & vbNewLine _
    '                                      & "           )                              M44_01                                                                                        " & vbNewLine _
    '                                      & "  ON  UNCHIN.NRS_BR_CD                  = M44_01.NRS_BR_CD                                                                              " & vbNewLine _
    '                                      & " AND  UNCHIN.SEIQ_ETARIFF_CD            = M44_01.EXTC_TARIFF_CD                                                                         " & vbNewLine
    'START YANAI 要望番号891
    'Private Const SQL_FROM_DATA As String = "FROM                                                                                                                                    " & vbNewLine _
    '                                      & "                      $LM_TRN$..F_UNCHIN_TRS AS UNCHIN                                                                                  " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L     AS UNSO                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.UNSO_NO_L                   = UNSO.UNSO_NO_L                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST                                                                                    " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = DEST.NRS_BR_CD                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.CUST_CD_L                     = DEST.CUST_CD_L                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.DEST_CD                       = DEST.DEST_CD                                                                            " & vbNewLine _
    '                                      & "       AND DEST.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST2                                                                                   " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = DEST2.NRS_BR_CD                                                                         " & vbNewLine _
    '                                      & "       AND 'ZZZZZ'                            = DEST2.CUST_CD_L                                                                         " & vbNewLine _
    '                                      & "       AND UNSO.DEST_CD                       = DEST2.DEST_CD                                                                           " & vbNewLine _
    '                                      & "       AND DEST2.SYS_DEL_FLG                   = '0'                                                                                    " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO     AS UNSOCO                                                                                  " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = UNSOCO.NRS_BR_CD                                                                        " & vbNewLine _
    '                                      & "       AND UNSO.UNSO_CD                       = UNSOCO.UNSOCO_CD                                                                        " & vbNewLine _
    '                                      & "       AND UNSO.UNSO_BR_CD                    = UNSOCO.UNSOCO_BR_CD                                                                     " & vbNewLine _
    '                                      & "       AND UNSOCO.SYS_DEL_FLG                 = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN2                                                                                    " & vbNewLine _
    '                                      & "        ON UNSO.UNSO_ONDO_KB                  = KBN2.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN2.KBN_GROUP_CD                  = 'U006'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN2.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN3                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_PKG_UT                 = KBN3.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN3.KBN_GROUP_CD                  = 'N001'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN3.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN4                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_SYARYO_KB              = KBN4.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN4.KBN_GROUP_CD                  = 'S012'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN4.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN5                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_DANGER_KB              = KBN5.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN5.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "       AND KBN5.KBN_GROUP_CD                  = 'K008'                                                                                  " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN1                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.SEIQ_TARIFF_BUNRUI_KB       = KBN1.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN1.KBN_GROUP_CD                  = 'T015'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN1.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN6                                                                                    " & vbNewLine _
    '                                      & "        ON UNCHIN.TAX_KB                      = KBN6.KBN_CD                                                                             " & vbNewLine _
    '                                      & "       AND KBN6.KBN_GROUP_CD                  = 'Z001'                                                                                  " & vbNewLine _
    '                                      & "       AND KBN6.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_CUST       AS CUST                                                                                    " & vbNewLine _
    '                                      & "        ON UNSO.NRS_BR_CD                     = CUST.NRS_BR_CD                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.CUST_CD_L                     = CUST.CUST_CD_L                                                                          " & vbNewLine _
    '                                      & "       AND UNSO.CUST_CD_M                     = CUST.CUST_CD_M                                                                          " & vbNewLine _
    '                                      & "       AND CUST.CUST_CD_S                     = '00'                                                                                    " & vbNewLine _
    '                                      & "       AND CUST.CUST_CD_SS                    = '00'                                                                                    " & vbNewLine _
    '                                      & "       AND CUST.SYS_DEL_FLG                   ='0'                                                                                      " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
    '                                      & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
    '                                      & "                      ,M47_02.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
    '                                      & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                                " & vbNewLine _
    '                                      & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                                 " & vbNewLine _
    '                                      & "                 INNER JOIN (                                                                                                           " & vbNewLine _
    '                                      & "                                    SELECT M47_02.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
    '                                      & "                                          ,M47_02.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
    '                                      & "                                          ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD                                         " & vbNewLine _
    '                                      & "                                          ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
    '                                      & "                                          ,M47_02.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
    '                                      & "                                      FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                             " & vbNewLine _
    '                                      & "                                INNER JOIN (                                                                                            " & vbNewLine _
    '                                      & "                                                    SELECT F02_11.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                          ,MAX(M47_01.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
    '                                      & "                                                      FROM $LM_TRN$..F_UNSO_L F02_11                                                    " & vbNewLine _
    '                                      & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_01                                                 " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD   = M07_01.NRS_BR_CD                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_L   = M07_01.CUST_CD_L                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_M   = M07_01.CUST_CD_M                                        " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_S   = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_SS  = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                      & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
    '                                      & "                                                                             SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
    '                                      & "                                                                               FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
    '                                      & "                                                                              WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                      & "                                                                           GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                                )                   M47_01                                              " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD       = M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                       AND F02_11.SEIQ_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                       AND CASE WHEN M07_01.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
    '                                      & "                                                                THEN F02_11.OUTKA_PLAN_DATE                                             " & vbNewLine _
    '                                      & "                                                                ELSE F02_11.ARR_PLAN_DATE                                               " & vbNewLine _
    '                                      & "                                                            END                  >= M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                     WHERE F02_11.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
    '                                      & "                                                  GROUP BY F02_11.NRS_BR_CD                                                             " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L                                                             " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
    '                                      & "                                           )                      M47_02                                                                " & vbNewLine _
    '                                      & "                                     ON M47_01.NRS_BR_CD        = M47_02.NRS_BR_CD                                                      " & vbNewLine _
    '                                      & "                                    AND M47_01.UNCHIN_TARIFF_CD = M47_02.UNCHIN_TARIFF_CD                                               " & vbNewLine _
    '                                      & "                                    AND M47_01.STR_DATE         = M47_02.STR_DATE                                                       " & vbNewLine _
    '                                      & "                                  WHERE M47_01.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
    '                                      & "                               GROUP BY M47_02.NRS_BR_CD                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_02.UNSO_NO_L                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_01.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
    '                                      & "                                       ,M47_02.STR_DATE                                                                                 " & vbNewLine _
    '                                      & "                            )                          M47_02                                                                           " & vbNewLine _
    '                                      & "                      ON M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                                                 " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
    '                                      & "                     AND M47_01.STR_DATE             = M47_02.STR_DATE                                                                  " & vbNewLine _
    '                                      & "                   WHERE M47_01.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
    '                                      & "          )                               M47_01                                                                                        " & vbNewLine _
    '                                      & "  ON  UNCHIN.NRS_BR_CD                  = M47_01.NRS_BR_CD                                                                              " & vbNewLine _
    '                                      & " AND  UNCHIN.UNSO_NO_L                  = M47_01.UNSO_NO_L                                                                              " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD     M49_01                                                                                        " & vbNewLine _
    '                                      & "  ON  UNCHIN.NRS_BR_CD                  = M49_01.NRS_BR_CD                                                                              " & vbNewLine _
    '                                      & " AND  UNCHIN.SEIQ_TARIFF_CD             = M49_01.YOKO_TARIFF_CD                                                                         " & vbNewLine _
    '                                      & " AND  M49_01.SYS_DEL_FLG                = '0'                                                                                           " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
    '                                      & "              SELECT NRS_BR_CD       AS NRS_BR_CD                                                                                       " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD                                                                                  " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM                                                                                 " & vbNewLine _
    '                                      & "                FROM $LM_MST$..M_EXTC_UNCHIN                                                                                            " & vbNewLine _
    '                                      & "               WHERE JIS_CD      = '0000000'                                                                                            " & vbNewLine _
    '                                      & "                 AND SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
    '                                      & "            GROUP BY NRS_BR_CD                                                                                                          " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD                                                                                                     " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM                                                                                                    " & vbNewLine _
    '                                      & "           )                              M44_01                                                                                        " & vbNewLine _
    '                                      & "  ON  UNCHIN.NRS_BR_CD                  = M44_01.NRS_BR_CD                                                                              " & vbNewLine _
    '                                      & " AND  UNCHIN.SEIQ_ETARIFF_CD            = M44_01.EXTC_TARIFF_CD                                                                         " & vbNewLine
    Private Const SQL_FROM_DATA As String = "FROM                                                                                                                                    " & vbNewLine _
                                          & "                      $LM_TRN$..F_UNCHIN_TRS AS UNCHIN                                                                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L     AS UNSO                                                                                    " & vbNewLine _
                                          & "        ON UNCHIN.UNSO_NO_L                   = UNSO.UNSO_NO_L                                                                          " & vbNewLine _
                                          & "       AND UNSO.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST                                                                                    " & vbNewLine _
                                          & "        ON UNSO.NRS_BR_CD                     = DEST.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "       AND UNSO.CUST_CD_L                     = DEST.CUST_CD_L                                                                          " & vbNewLine _
                                          & "       AND UNSO.DEST_CD                       = DEST.DEST_CD                                                                            " & vbNewLine _
                                          & "       AND DEST.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST2                                                                                   " & vbNewLine _
                                          & "        ON UNSO.NRS_BR_CD                     = DEST2.NRS_BR_CD                                                                         " & vbNewLine _
                                          & "       AND 'ZZZZZ'                            = DEST2.CUST_CD_L                                                                         " & vbNewLine _
                                          & "       AND UNSO.DEST_CD                       = DEST2.DEST_CD                                                                           " & vbNewLine _
                                          & "       AND DEST2.SYS_DEL_FLG                   = '0'                                                                                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST3                                                                                    " & vbNewLine _
                                          & "        ON UNSO.NRS_BR_CD                     = DEST3.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "       AND UNSO.CUST_CD_L                     = DEST3.CUST_CD_L                                                                          " & vbNewLine _
                                          & "       AND UNSO.ORIG_CD                       = DEST3.DEST_CD                                                                            " & vbNewLine _
                                          & "       AND DEST3.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST       AS DEST4                                                                                   " & vbNewLine _
                                          & "        ON UNSO.NRS_BR_CD                     = DEST4.NRS_BR_CD                                                                         " & vbNewLine _
                                          & "       AND 'ZZZZZ'                            = DEST4.CUST_CD_L                                                                         " & vbNewLine _
                                          & "       AND UNSO.ORIG_CD                       = DEST4.DEST_CD                                                                           " & vbNewLine _
                                          & "       AND DEST4.SYS_DEL_FLG                   = '0'                                                                                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO     AS UNSOCO                                                                                  " & vbNewLine _
                                          & "        ON UNSO.NRS_BR_CD                     = UNSOCO.NRS_BR_CD                                                                        " & vbNewLine _
                                          & "       AND UNSO.UNSO_CD                       = UNSOCO.UNSOCO_CD                                                                        " & vbNewLine _
                                          & "       AND UNSO.UNSO_BR_CD                    = UNSOCO.UNSOCO_BR_CD                                                                     " & vbNewLine _
                                          & "       AND UNSOCO.SYS_DEL_FLG                 = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN2                                                                                    " & vbNewLine _
                                          & "        ON UNSO.UNSO_ONDO_KB                  = KBN2.KBN_CD                                                                             " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD                  = 'U006'                                                                                  " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN3                                                                                    " & vbNewLine _
                                          & "        ON UNCHIN.SEIQ_PKG_UT                 = KBN3.KBN_CD                                                                             " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD                  = 'N001'                                                                                  " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN4                                                                                    " & vbNewLine _
                                          & "        ON UNCHIN.SEIQ_SYARYO_KB              = KBN4.KBN_CD                                                                             " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD                  = 'S012'                                                                                  " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN5                                                                                    " & vbNewLine _
                                          & "        ON UNCHIN.SEIQ_DANGER_KB              = KBN5.KBN_CD                                                                             " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD                  = 'K008'                                                                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN1                                                                                    " & vbNewLine _
                                          & "        ON UNCHIN.SEIQ_TARIFF_BUNRUI_KB       = KBN1.KBN_CD                                                                             " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD                  = 'T015'                                                                                  " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN6                                                                                    " & vbNewLine _
                                          & "        ON UNCHIN.TAX_KB                      = KBN6.KBN_CD                                                                             " & vbNewLine _
                                          & "       AND KBN6.KBN_GROUP_CD                  = 'Z001'                                                                                  " & vbNewLine _
                                          & "       AND KBN6.SYS_DEL_FLG                   = '0'                                                                                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST       AS CUST                                                                                    " & vbNewLine _
                                          & "        ON UNSO.NRS_BR_CD                     = CUST.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "       AND UNSO.CUST_CD_L                     = CUST.CUST_CD_L                                                                          " & vbNewLine _
                                          & "       AND UNSO.CUST_CD_M                     = CUST.CUST_CD_M                                                                          " & vbNewLine _
                                          & "       AND CUST.CUST_CD_S                     = '00'                                                                                    " & vbNewLine _
                                          & "       AND CUST.CUST_CD_SS                    = '00'                                                                                    " & vbNewLine _
                                          & "       AND CUST.SYS_DEL_FLG                   ='0'                                                                                      " & vbNewLine _
                                          & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
                                          & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
                                          & "                      ,M47_02.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
                                          & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                                " & vbNewLine _
                                          & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                                 " & vbNewLine _
                                          & "                 INNER JOIN (                                                                                                           " & vbNewLine _
                                          & "                                    SELECT M47_02.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
                                          & "                                          ,M47_02.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
                                          & "                                          ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD                                         " & vbNewLine _
                                          & "                                          ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
                                          & "                                          ,M47_02.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
                                          & "                                      FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                             " & vbNewLine _
                                          & "                                INNER JOIN (                                                                                            " & vbNewLine _
                                          & "                                                    SELECT F02_11.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                          ,F02_11.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
                                          & "                                                          ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                             " & vbNewLine _
                                          & "                                                          ,MAX(M47_01.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
                                          & "                                                      FROM $LM_TRN$..F_UNSO_L F02_11                                                    " & vbNewLine _
                                          & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_01                                                 " & vbNewLine _
                                          & "                                                        ON F02_11.NRS_BR_CD   = M07_01.NRS_BR_CD                                        " & vbNewLine _
                                          & "                                                       AND F02_11.CUST_CD_L   = M07_01.CUST_CD_L                                        " & vbNewLine _
                                          & "                                                       AND F02_11.CUST_CD_M   = M07_01.CUST_CD_M                                        " & vbNewLine _
                                          & "                                                       AND M07_01.CUST_CD_S   = '00'                                                    " & vbNewLine _
                                          & "                                                       AND M07_01.CUST_CD_SS  = '00'                                                    " & vbNewLine _
                                          & "                                                       AND M07_01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                          & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
                                          & "                                                                             SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
                                          & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
                                          & "                                                                                   ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
                                          & "                                                                               FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
                                          & "                                                                              WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "                                                                           GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
                                          & "                                                                                   ,M47_01.STR_DATE                                     " & vbNewLine _
                                          & "                                                                )                   M47_01                                              " & vbNewLine _
                                          & "                                                        ON F02_11.NRS_BR_CD       = M47_01.NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                       AND F02_11.SEIQ_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
                                          & "                                                       AND CASE WHEN M07_01.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
                                          & "                                                                THEN F02_11.OUTKA_PLAN_DATE                                             " & vbNewLine _
                                          & "                                                                ELSE F02_11.ARR_PLAN_DATE                                               " & vbNewLine _
                                          & "                                                            END                  >= M47_01.STR_DATE                                     " & vbNewLine _
                                          & "                                                     WHERE F02_11.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
                                          & "                                                  GROUP BY F02_11.NRS_BR_CD                                                             " & vbNewLine _
                                          & "                                                          ,F02_11.UNSO_NO_L                                                             " & vbNewLine _
                                          & "                                                          ,M47_01.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
                                          & "                                           )                      M47_02                                                                " & vbNewLine _
                                          & "                                     ON M47_01.NRS_BR_CD        = M47_02.NRS_BR_CD                                                      " & vbNewLine _
                                          & "                                    AND M47_01.UNCHIN_TARIFF_CD = M47_02.UNCHIN_TARIFF_CD                                               " & vbNewLine _
                                          & "                                    AND M47_01.STR_DATE         = M47_02.STR_DATE                                                       " & vbNewLine _
                                          & "                                  WHERE M47_01.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
                                          & "                               GROUP BY M47_02.NRS_BR_CD                                                                                " & vbNewLine _
                                          & "                                       ,M47_02.UNSO_NO_L                                                                                " & vbNewLine _
                                          & "                                       ,M47_01.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
                                          & "                                       ,M47_02.STR_DATE                                                                                 " & vbNewLine _
                                          & "                            )                          M47_02                                                                           " & vbNewLine _
                                          & "                      ON M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                                                 " & vbNewLine _
                                          & "                     AND M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
                                          & "                     AND M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
                                          & "                     AND M47_01.STR_DATE             = M47_02.STR_DATE                                                                  " & vbNewLine _
                                          & "                   WHERE M47_01.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
                                          & "          )                               M47_01                                                                                        " & vbNewLine _
                                          & "  ON  UNCHIN.NRS_BR_CD                  = M47_01.NRS_BR_CD                                                                              " & vbNewLine _
                                          & " AND  UNCHIN.UNSO_NO_L                  = M47_01.UNSO_NO_L                                                                              " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD     M49_01                                                                                        " & vbNewLine _
                                          & "  ON  UNCHIN.NRS_BR_CD                  = M49_01.NRS_BR_CD                                                                              " & vbNewLine _
                                          & " AND  UNCHIN.SEIQ_TARIFF_CD             = M49_01.YOKO_TARIFF_CD                                                                         " & vbNewLine _
                                          & " AND  M49_01.SYS_DEL_FLG                = '0'                                                                                           " & vbNewLine _
                                          & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
                                          & "              SELECT NRS_BR_CD       AS NRS_BR_CD                                                                                       " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD                                                                                  " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM                                                                                 " & vbNewLine _
                                          & "                FROM $LM_MST$..M_EXTC_UNCHIN                                                                                            " & vbNewLine _
                                          & "               WHERE JIS_CD      = '0000000'                                                                                            " & vbNewLine _
                                          & "                 AND SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
                                          & "            GROUP BY NRS_BR_CD                                                                                                          " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_CD                                                                                                     " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_REM                                                                                                    " & vbNewLine _
                                          & "           )                              M44_01                                                                                        " & vbNewLine _
                                          & "  ON  UNCHIN.NRS_BR_CD                  = M44_01.NRS_BR_CD                                                                              " & vbNewLine _
                                          & " AND  UNCHIN.SEIQ_ETARIFF_CD            = M44_01.EXTC_TARIFF_CD                                                                         " & vbNewLine
    'END YANAI 要望番号891
    'END YANAI 要望番号376

    'START YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない
    '''' <summary>
    '''' 更新SQL(保存)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_TRN$..F_UNCHIN_TRS SET                          " & vbNewLine _
    '                                   & "        SEIQ_SYARYO_KB             = @SEIQ_SYARYO_KB       " & vbNewLine _
    '                                   & "       ,SEIQ_PKG_UT                = @SEIQ_PKG_UT          " & vbNewLine _
    '                                   & "       ,SEIQ_DANGER_KB             = @SEIQ_DANGER_KB       " & vbNewLine _
    '                                   & "       ,SEIQ_TARIFF_BUNRUI_KB      = @SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                   & "       ,SEIQ_TARIFF_CD             = @SEIQ_TARIFF_CD       " & vbNewLine _
    '                                   & "       ,SEIQ_ETARIFF_CD            = @SEIQ_ETARIFF_CD      " & vbNewLine _
    '                                   & "       ,DECI_NG_NB                 = @DECI_NG_NB           " & vbNewLine _
    '                                   & "       ,DECI_KYORI                 = @DECI_KYORI           " & vbNewLine _
    '                                   & "       ,DECI_WT                    = @DECI_WT              " & vbNewLine _
    '                                   & "       ,DECI_UNCHIN                = @DECI_UNCHIN          " & vbNewLine _
    '                                   & "       ,DECI_CITY_EXTC             = @DECI_CITY_EXTC       " & vbNewLine _
    '                                   & "       ,DECI_WINT_EXTC             = @DECI_WINT_EXTC       " & vbNewLine _
    '                                   & "       ,DECI_RELY_EXTC             = @DECI_RELY_EXTC       " & vbNewLine _
    '                                   & "       ,DECI_TOLL                  = @DECI_TOLL            " & vbNewLine _
    '                                   & "       ,DECI_INSU                  = @DECI_INSU            " & vbNewLine _
    '                                   & "       ,TAX_KB                     = @TAX_KB               " & vbNewLine _
    '                                   & "       ,REMARK                     = @REMARK               " & vbNewLine _
    '                                   & "       ,KANRI_UNCHIN               = @KANRI_UNCHIN         " & vbNewLine _
    '                                   & "       ,KANRI_CITY_EXTC            = @KANRI_CITY_EXTC      " & vbNewLine _
    '                                   & "       ,KANRI_WINT_EXTC            = @KANRI_WINT_EXTC      " & vbNewLine _
    '                                   & "       ,KANRI_RELY_EXTC            = @KANRI_RELY_EXTC      " & vbNewLine _
    '                                   & "       ,KANRI_TOLL                 = @KANRI_TOLL           " & vbNewLine _
    '                                   & "       ,KANRI_INSU                 = @KANRI_INSU           " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE               = @SYS_UPD_DATE         " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME               = @SYS_UPD_TIME         " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID               = @SYS_UPD_PGID         " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER               = @SYS_UPD_USER         " & vbNewLine _
    '                                   & " WHERE                                                     " & vbNewLine _
    '                                   & "         NRS_BR_CD                 = @NRS_BR_CD            " & vbNewLine _
    '                                   & " AND     UNSO_NO_L                 = @UNSO_NO_L            " & vbNewLine _
    '                                   & " AND     UNSO_NO_M                 = @UNSO_NO_M            " & vbNewLine
    ''' <summary>
    ''' 更新SQL(保存)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_TRN$..F_UNCHIN_TRS SET                          " & vbNewLine _
                                       & "        SEIQ_SYARYO_KB             = @SEIQ_SYARYO_KB       " & vbNewLine _
                                       & "       ,SEIQ_PKG_UT                = @SEIQ_PKG_UT          " & vbNewLine _
                                       & "       ,SEIQ_DANGER_KB             = @SEIQ_DANGER_KB       " & vbNewLine _
                                       & "       ,SEIQ_TARIFF_BUNRUI_KB      = @SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
                                       & "       ,SEIQ_TARIFF_CD             = @SEIQ_TARIFF_CD       " & vbNewLine _
                                       & "       ,SEIQ_ETARIFF_CD            = @SEIQ_ETARIFF_CD      " & vbNewLine _
                                       & "       ,SEIQ_WT                    = @SEIQ_WT              " & vbNewLine _
                                       & "       ,DECI_NG_NB                 = @DECI_NG_NB           " & vbNewLine _
                                       & "       ,DECI_KYORI                 = @DECI_KYORI           " & vbNewLine _
                                       & "       ,DECI_WT                    = @DECI_WT              " & vbNewLine _
                                       & "       ,DECI_UNCHIN                = @DECI_UNCHIN          " & vbNewLine _
                                       & "       ,DECI_CITY_EXTC             = @DECI_CITY_EXTC       " & vbNewLine _
                                       & "       ,DECI_WINT_EXTC             = @DECI_WINT_EXTC       " & vbNewLine _
                                       & "       ,DECI_RELY_EXTC             = @DECI_RELY_EXTC       " & vbNewLine _
                                       & "       ,DECI_TOLL                  = @DECI_TOLL            " & vbNewLine _
                                       & "       ,DECI_INSU                  = @DECI_INSU            " & vbNewLine _
                                       & "       ,TAX_KB                     = @TAX_KB               " & vbNewLine _
                                       & "       ,REMARK                     = @REMARK               " & vbNewLine _
                                       & "       ,KANRI_UNCHIN               = @KANRI_UNCHIN         " & vbNewLine _
                                       & "       ,KANRI_CITY_EXTC            = @KANRI_CITY_EXTC      " & vbNewLine _
                                       & "       ,KANRI_WINT_EXTC            = @KANRI_WINT_EXTC      " & vbNewLine _
                                       & "       ,KANRI_RELY_EXTC            = @KANRI_RELY_EXTC      " & vbNewLine _
                                       & "       ,KANRI_TOLL                 = @KANRI_TOLL           " & vbNewLine _
                                       & "       ,KANRI_INSU                 = @KANRI_INSU           " & vbNewLine _
                                       & "       ,SYS_UPD_DATE               = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME               = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID               = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER               = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                     " & vbNewLine _
                                       & "         NRS_BR_CD                 = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     UNSO_NO_L                 = @UNSO_NO_L            " & vbNewLine _
                                       & " AND     UNSO_NO_M                 = @UNSO_NO_M            " & vbNewLine
    'END YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない

    ''' <summary>
    ''' 更新SQL(確定)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_KAKUTEI As String = "UPDATE $LM_TRN$..F_UNCHIN_TRS SET                  " & vbNewLine _
                                               & "        SYS_UPD_DATE               = @SYS_UPD_DATE " & vbNewLine _
                                               & "       ,SYS_UPD_TIME               = @SYS_UPD_TIME " & vbNewLine _
                                               & "       ,SYS_UPD_PGID               = @SYS_UPD_PGID " & vbNewLine _
                                               & "       ,SYS_UPD_USER               = @SYS_UPD_USER " & vbNewLine _
                                               & "       ,SEIQ_FIXED_FLAG            = '01'          " & vbNewLine _
                                               & " WHERE                                             " & vbNewLine _
                                               & "         UNSO_NO_L                 = @UNSO_NO_L    " & vbNewLine _
                                               & " AND     UNSO_NO_M                 = @UNSO_NO_M    " & vbNewLine


    ''' <summary>
    ''' 更新SQL(確定解除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_KAKUTEIKAIJO As String = "UPDATE $LM_TRN$..F_UNCHIN_TRS SET                 " & vbNewLine _
                                                    & "        SYS_UPD_DATE               = @SYS_UPD_DATE" & vbNewLine _
                                                    & "       ,SYS_UPD_TIME               = @SYS_UPD_TIME" & vbNewLine _
                                                    & "       ,SYS_UPD_PGID               = @SYS_UPD_PGID" & vbNewLine _
                                                    & "       ,SYS_UPD_USER               = @SYS_UPD_USER" & vbNewLine _
                                                    & "       ,SEIQ_FIXED_FLAG            = '00'         " & vbNewLine _
                                                    & " WHERE                                            " & vbNewLine _
                                                    & "         UNSO_NO_L                 = @UNSO_NO_L   " & vbNewLine _
                                                    & " AND     UNSO_NO_M                 = @UNSO_NO_M   " & vbNewLine



    ''' <summary>
    ''' 更新SQL(共通)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_KYOUTU As String = "UPDATE $LM_TRN$..F_UNSO_L SET                         " & vbNewLine _
                                              & "       SYS_UPD_DATE                = @SYS_UPD_DATE    " & vbNewLine _
                                              & "       ,SYS_UPD_TIME               = @SYS_UPD_TIME    " & vbNewLine _
                                              & "       ,SYS_UPD_PGID               = @SYS_UPD_PGID    " & vbNewLine _
                                              & " WHERE                                                " & vbNewLine _
                                              & "         NRS_BR_CD                 = @NRS_BR_CD       " & vbNewLine _
                                              & " AND     UNSO_NO_L                 = @UNSO_NO_L       " & vbNewLine _
                                              & " AND     SYS_UPD_DATE              = @GUI_SYS_UPD_DATE" & vbNewLine _
                                              & " AND     SYS_UPD_TIME              = @GUI_SYS_UPD_TIME" & vbNewLine



#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                               " & vbNewLine _
                                         & "    UNCHIN.UNSO_NO_L,UNCHIN.UNSO_NO_M                   " & vbNewLine

#End Region

#Region "入力チェック"
    ''' <summary>
    ''' 運送番号L存在チェック用
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_EXIT_SEIQTO As String = "SELECT                            " & vbNewLine _
                                            & "   COUNT(UNSO.UNSO_NO_L)  AS REC_CNT   " & vbNewLine _
                                       & "      FROM  $LM_TRN$..F_UNSO_L AS UNSO       " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         UNSO.NRS_BR_CD                 = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     UNSO.UNSO_NO_L                 = @UNSO_NO_L            " & vbNewLine _
                                       & " AND     UNSO.SYS_UPD_DATE      = @SYS_UPD_DATE            " & vbNewLine _
                                       & " AND     UNSO.SYS_UPD_TIME      = @SYS_UPD_TIME            " & vbNewLine



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
    ''' 運賃マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050IN")

        'INTableの条件rowの格納
        'START YANAI 要望番号561
        'Me._Row = inTbl.Rows(0)
        Dim rowCnt As Integer = Convert.ToInt32(inTbl.Rows(0).Item("RENZOKU_CNT").ToString)
        Me._Row = inTbl.Rows(rowCnt)
        'END YANAI 要望番号561

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF050DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMF050DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF050DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050IN")

        'INTableの条件rowの格納
        'START YANAI 要望番号561
        'Me._Row = inTbl.Rows(0)
        Dim rowCnt As Integer = Convert.ToInt32(inTbl.Rows(0).Item("RENZOKU_CNT").ToString)
        Me._Row = inTbl.Rows(rowCnt)
        'END YANAI 要望番号561

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF050DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF050DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMF050DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF050DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SEIQ_TARIFF_BUNRUI_KB", "SEIQ_TARIFF_BUNRUI_KB")
        map.Add("SEIQ_TARIFF_BUNRUI_NM", "SEIQ_TARIFF_BUNRUI_NM")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_TARIFF_NM", "SEIQ_TARIFF_NM")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("SEIQ_ETARIFF_NM", "SEIQ_ETARIFF_NM")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("KANRI_UNCHIN", "KANRI_UNCHIN")
        map.Add("KANRI_CITY_EXTC", "KANRI_CITY_EXTC")
        map.Add("KANRI_WINT_EXTC", "KANRI_WINT_EXTC")
        map.Add("KANRI_TOLL", "KANRI_TOLL")
        map.Add("KANRI_INSU", "KANRI_INSU")
        map.Add("KANRI_RELY_EXTC", "KANRI_RELY_EXTC")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQ_GROUP_NO_M", "SEIQ_GROUP_NO_M")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("UNSO_ONDO_NM", "UNSO_ONDO_NM")
        map.Add("SEIQ_PKG_UT_KB", "SEIQ_PKG_UT_KB")
        map.Add("SEIQ_PKG_UT_NM", "SEIQ_PKG_UT_NM")
        map.Add("SEIQ_SYARYO_KB", "SEIQ_SYARYO_KB")
        map.Add("SEIQ_SYARYO_NM", "SEIQ_SYARYO_NM")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        map.Add("SEIQ_DANGER_NM", "SEIQ_DANGER_NM")
        map.Add("REMARK", "REMARK")
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_NM", "TAX_NM")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("UNSO_TTL_QT", "UNSO_TTL_QT")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        'START KAI 要望番号707
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        'END KAI 要望番号707
        'START YANAI 要望番号974 運賃が0円で、他の割増運賃関係が0円以外の場合エラーとする
        map.Add("SEIQ_UNCHIN", "SEIQ_UNCHIN")
        map.Add("SEIQ_CITY_EXTC", "SEIQ_CITY_EXTC")
        map.Add("SEIQ_WINT_EXTC", "SEIQ_WINT_EXTC")
        map.Add("SEIQ_RELY_EXTC", "SEIQ_RELY_EXTC")
        map.Add("SEIQ_TOLL", "SEIQ_TOLL")
        map.Add("SEIQ_INSU", "SEIQ_INSU")
        'END YANAI 要望番号974 運賃が0円で、他の割増運賃関係が0円以外の場合エラーとする

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF050OUT")

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

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.NRS_BR_CD = @NRS_BR_CD  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.SEIQ_GROUP_NO = @SEIQ_GROUP_NO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQ_GROUP_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN.SEIQ_GROUP_NO_M = @SEIQ_GROUP_NO_M ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(.Item("SEIQ_GROUP_NO").ToString()) = True AndAlso _
                String.IsNullOrEmpty(.Item("SEIQ_GROUP_NO_M").ToString()) = True Then
                If String.IsNullOrEmpty(whereStr) = False Then
                    If andstr.Length <> 0 Then
                        andstr.Append("AND")
                    End If
                    andstr.Append(" UNCHIN.UNSO_NO_L = @UNSO_NO_L ")
                    andstr.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
                End If
            End If

            whereStr = .Item("UNSO_NO_M").ToString()
            If String.IsNullOrEmpty(.Item("SEIQ_GROUP_NO").ToString()) = True AndAlso _
                String.IsNullOrEmpty(.Item("SEIQ_GROUP_NO_M").ToString()) = True Then
                If String.IsNullOrEmpty(whereStr) = False Then
                    If andstr.Length <> 0 Then
                        andstr.Append("AND")
                    End If
                    andstr.Append(" UNCHIN.UNSO_NO_M = @UNSO_NO_M ")
                    andstr.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", whereStr, DBDataType.CHAR))
                End If
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

#Region "SQL"

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
    Private Const SQL_SELECT_NEW_KURO_KAGAMI_UNCHIN_YOKO As String = " SELECT                           " & vbNewLine _
                                              & "    COUNT(SKYU_DATE)  AS SKYU_DATE_COUNT               " & vbNewLine _
                                              & "  FROM $LM_TRN$..G_KAGAMI_HED K_HED                    " & vbNewLine _
                                              & "  LEFT JOIN $LM_TRN$..G_KAGAMI_DTL K_DTL               " & vbNewLine _
                                              & "  ON K_HED.SKYU_NO = K_DTL.SKYU_NO                     " & vbNewLine _
                                              & "  WHERE K_HED.STATE_KB        IN ('00','01','02')      " & vbNewLine _
                                              & "    AND K_HED.RB_FLG          =    '00'                " & vbNewLine _
                                              & "    AND K_HED.SKYU_NO_RELATED <>   ''                  " & vbNewLine _
                                              & "    AND K_HED.SEIQTO_CD       =    @SEIQTO_CD          " & vbNewLine _
                                              & "    AND K_HED.SKYU_DATE       like @SKYU_MONTH         " & vbNewLine _
                                              & "    AND K_HED.SYS_DEL_FLG     =    '0'                 " & vbNewLine _
                                              & "    AND K_HED.YOKOMOCHI_IMP_FROM_DATE  <=   @SKYU_DATE " & vbNewLine _
                                              & "    AND K_HED.SKYU_DATE       >=   @SKYU_DATE          " & vbNewLine _
                                              & "    AND K_DTL.GROUP_KB = '03'                          " & vbNewLine _
                                              & "    AND K_DTL.SYS_DEL_FLG = '0'                        " & vbNewLine


    Private Const SQL_SELECT_NEW_KURO_KAGAMI_UNCHIN As String = " SELECT                                 " & vbNewLine _
                                              & "    COUNT(SKYU_DATE)  AS SKYU_DATE_COUNT               " & vbNewLine _
                                              & "  FROM $LM_TRN$..G_KAGAMI_HED K_HED                    " & vbNewLine _
                                              & "  LEFT JOIN $LM_TRN$..G_KAGAMI_DTL K_DTL               " & vbNewLine _
                                              & "  ON K_HED.SKYU_NO = K_DTL.SKYU_NO                     " & vbNewLine _
                                              & "  WHERE K_HED.STATE_KB        IN ('00','01','02')      " & vbNewLine _
                                              & "    AND K_HED.RB_FLG          =    '00'                " & vbNewLine _
                                              & "    AND K_HED.SKYU_NO_RELATED <>   ''                  " & vbNewLine _
                                              & "    AND K_HED.SEIQTO_CD       =    @SEIQTO_CD          " & vbNewLine _
                                              & "    AND K_HED.SKYU_DATE       like @SKYU_MONTH         " & vbNewLine _
                                              & "    AND K_HED.SYS_DEL_FLG     =    '0'                 " & vbNewLine _
                                              & "    AND K_HED.UNCHIN_IMP_FROM_DATE  <=   @SKYU_DATE    " & vbNewLine _
                                              & "    AND K_HED.SKYU_DATE       >=   @SKYU_DATE          " & vbNewLine _
                                              & "    AND K_DTL.GROUP_KB = '03'                          " & vbNewLine _
                                              & "    AND K_DTL.SYS_DEL_FLG = '0'                        " & vbNewLine

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end

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

    ''' <summary>
    ''' 運賃マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル検索結果取得SQLの構築・発行</remarks>
    Private Function SelectUnchinM(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMF050DAC.SQL_EXIT_SEIQTO)
        Me._StrSql.Append(vbNewLine)

        Dim max As Integer = inTbl.Rows.Count - 1

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        Dim cnt As Integer = 0
        For i As Integer = 0 To max

            Dim reader As SqlDataReader = Nothing

            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamHaitaChk()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF050DAC", "SelectUnchinM", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            cnt = Convert.ToInt32(reader("REC_CNT"))
            reader.Close()

            If Me.UpdateResultChk(cnt) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運賃テーブル更新(保存)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '更新対象共通の呼び出し
        Call Me.UpdateUnsoM(ds)


        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Dim max As Integer = inTbl.Rows.Count - 1

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF050DAC.SQL_UPDATE, Me._Row.Item("NRS_BR_CD").ToString()))

        'START YANAI 要望番号561
        Dim updDate As String = String.Empty
        Dim updTime As String = String.Empty
        'END YANAI 要望番号561

        For i As Integer = 0 To max

            Me._Row = inTbl.Rows(i)

            'START YANAI 要望番号561
            updDate = MyBase.GetSystemDate()
            updTime = MyBase.GetSystemTime()
            'END YANAI 要望番号561

            'SQLパラメータ初期化/設定
            'START YANAI 要望番号561
            'Call Me.SetParamUpdate()
            Call Me.SetParamUpdate(updDate, updTime)

            Me._Row.Item("SYS_UPD_DATE") = updDate
            Me._Row.Item("SYS_UPD_TIME") = updTime
            'END YANAI 要望番号561

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF050DAC", "UpdateUnchinM", cmd)

           MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()


        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運賃テーブル更新(確定)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinKakuteiM(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050OUT")

        ''INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '更新対象共通の呼び出し
        Call Me.UpdateUnsoM(ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Dim max As Integer = inTbl.Rows.Count - 1

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF050DAC.SQL_UPDATE_KAKUTEI, Me._Row.Item("NRS_BR_CD").ToString()))

        'START YANAI 要望番号561
        Dim updDate As String = String.Empty
        Dim updTime As String = String.Empty
        'END YANAI 要望番号561

        For i As Integer = 0 To max

            'OUTTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'START YANAI 要望番号561
            updDate = MyBase.GetSystemDate()
            updTime = MyBase.GetSystemTime()
            'END YANAI 要望番号561

            'SQLパラメータ初期化/設定
            'START YANAI 要望番号561
            'Call Me.SetParamUpdate()
            Call Me.SetParamUpdate(updDate, updTime)

            Me._Row.Item("SYS_UPD_DATE") = updDate
            Me._Row.Item("SYS_UPD_TIME") = updTime
            'END YANAI 要望番号561

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF050DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

           MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()


        Next


        Return ds


    End Function

    ''' <summary>
    ''' 運賃テーブル更新(確定解除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinKakuteiKijoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050OUT")

        ''INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '更新対象共通の呼び出し
        Call Me.UpdateUnsoM(ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Dim max As Integer = inTbl.Rows.Count - 1

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF050DAC.SQL_UPDATE_KAKUTEIKAIJO, Me._Row.Item("NRS_BR_CD").ToString()))

        'START YANAI 要望番号561
        Dim updDate As String = String.Empty
        Dim updTime As String = String.Empty
        'END YANAI 要望番号561

        For i As Integer = 0 To max

            'OUTTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'START YANAI 要望番号561
            updDate = MyBase.GetSystemDate()
            updTime = MyBase.GetSystemTime()
            'END YANAI 要望番号561

            'SQLパラメータ初期化/設定
            'START YANAI 要望番号561
            'Call Me.SetParamUpdate()
            Call Me.SetParamUpdate(updDate, updTime)

            Me._Row.Item("SYS_UPD_DATE") = updDate
            Me._Row.Item("SYS_UPD_TIME") = updTime
            'END YANAI 要望番号561

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF050DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送テーブル更新(共通)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoM(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050OUT")

        ''INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim max As Integer = inTbl.Rows.Count - 1

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF050DAC.SQL_UPDATE_KYOUTU, Me._Row.Item("NRS_BR_CD").ToString()))

        'START YANAI 要望番号561
        Dim updDate As String = String.Empty
        Dim updTime As String = String.Empty
        'END YANAI 要望番号561

        For i As Integer = 0 To max

            'OUTTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'START YANAI 要望番号561
            updDate = MyBase.GetSystemDate()
            updTime = MyBase.GetSystemTime()
            'END YANAI 要望番号561

            'SQLパラメータ初期化/設定
            'START YANAI 要望番号561
            'Call Me.SetParamUpdate()
            Call Me.SetParamUpdate(updDate, updTime)

            Me._Row.Item("SYS_UPD_DATE") = updDate
            Me._Row.Item("SYS_UPD_TIME") = updTime
            'END YANAI 要望番号561

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF050DAC", "UpdateUnsoM", cmd)

            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 請求鏡ヘッダチェック用
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeiChek(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF050OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMG000DAC.SQL_SELECT_KEIRI_CHK_DATE)      'SQL構築(請求鏡ヘッダのチェック用)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetSeiParam()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF050DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "G_KAGAMI_HED")


        Return ds

    End Function

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 新黒存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function NewKuroExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("G_HED_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMG000DAC.SQL_SELECT_NEW_KURO_COUNT, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF050DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' '請求期間内チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InSkyuDateChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("G_HED_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", Me._Row.Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        Dim motoSql As String = String.Empty

        If Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString() = "40" Then
            motoSql = LMG000DAC.SQL_SELECT_IN_SKYU_DATE_YOKO
        Else
            motoSql = LMG000DAC.SQL_SELECT_IN_SKYU_DATE
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(motoSql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF050DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function
    '要望番号:1045 terakawa 2013.03.28 End

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
    ''' <summary>
    ''' 新黒鑑情報に運賃情報が紐づいているかのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExistNewKuroKagamiUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("G_HED_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", Me._Row.Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        Dim motoSql As String = String.Empty

        If Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString() = "40" Then
            motoSql = LMF050DAC.SQL_SELECT_NEW_KURO_KAGAMI_UNCHIN_YOKO
        Else
            motoSql = LMF050DAC.SQL_SELECT_NEW_KURO_KAGAMI_UNCHIN
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(motoSql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF050DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function
    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))


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

    'START YANAI 要望番号561
    '''' <summary>
    '''' パラメータ設定モジュール(システム共通項目(更新時))
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetParamCommonSystemUpd()
    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd(ByVal updDate As String, ByVal updTime As String)
        'END YANAI 要望番号561


        'パラメータ設定
        'START YANAI 要望番号561
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", updDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", updTime, DBDataType.CHAR))
        'END YANAI 要望番号561
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub
    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            'END YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", .Item("DECI_NG_NB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", .Item("DECI_KYORI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_WT", .Item("DECI_WT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", .Item("KANRI_UNCHIN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", .Item("KANRI_CITY_EXTC").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", .Item("KANRI_WINT_EXTC").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", .Item("KANRI_RELY_EXTC").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", .Item("KANRI_TOLL").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", .Item("KANRI_INSU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))



            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))


        End With

    End Sub

    'START YANAI 要望番号561
    '''' <summary>
    '''' パラメータ設定モジュール(更新登録用)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetParamUpdate()
    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate(ByVal updDate As String, ByVal updTime As String)
        'END YANAI 要望番号561

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetComParam()

        '共通項目
        'START YANAI 要望番号561
        'Call Me.SetParamCommonSystemUpd()
        Call Me.SetParamCommonSystemUpd(updDate, updTime)
        'END YANAI 要望番号561

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求鏡ヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSeiParam()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()


        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            'START YANAI 要望番号827
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号827

        End With
    End Sub


#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class
