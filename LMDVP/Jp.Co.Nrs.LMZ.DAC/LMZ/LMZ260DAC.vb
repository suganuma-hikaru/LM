' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ260DAC : 荷主マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ260DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ260DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(CUST.CUST_CD_L)		   AS SELECT_CNT   " & vbNewLine

    'START YANAI 要望番号558
    '''' <summary>
    '''' CUST_Mデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
    '                                    & "      CUST.NRS_BR_CD                               AS NRS_BR_CD                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_L                               AS CUST_NM_L                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_M                               AS CUST_NM_M                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_S                               AS CUST_NM_S                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_SS                              AS CUST_NM_SS                    " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_L                               AS CUST_CD_L                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_M                               AS CUST_CD_M                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_S                               AS CUST_CD_S                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_SS                              AS CUST_CD_SS                    " & vbNewLine _
    '                                    & "     ,CUST.CUST_OYA_CD                             AS CUST_OYA_CD                   " & vbNewLine _
    '                                    & "     ,CUST.ZIP                                     AS ZIP                           " & vbNewLine _
    '                                    & "     ,CUST.AD_1                                    AS AD_1                          " & vbNewLine _
    '                                    & "     ,CUST.AD_2                                    AS AD_2                          " & vbNewLine _
    '                                    & "     ,CUST.AD_3                                    AS AD_3                          " & vbNewLine _
    '                                    & "     ,CUST.PIC                                     AS PIC                           " & vbNewLine _
    '                                    & "     ,CUST.FUKU_PIC                                AS FUKU_PIC                      " & vbNewLine _
    '                                    & "     ,CUST.TEL                                     AS TEL                           " & vbNewLine _
    '                                    & "     ,CUST.FAX                                     AS FAX                           " & vbNewLine _
    '                                    & "     ,CUST.MAIL                                    AS MAIL                          " & vbNewLine _
    '                                    & "     ,CUST.SAITEI_HAN_KB                           AS SAITEI_HAN_KB                 " & vbNewLine _
    '                                    & "     ,CUST.OYA_SEIQTO_CD                           AS OYA_SEIQTO_CD                 " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_SEIQTO_CD                         AS HOKAN_SEIQTO_CD               " & vbNewLine _
    '                                    & "     ,CUST.NIYAKU_SEIQTO_CD                        AS NIYAKU_SEIQTO_CD              " & vbNewLine _
    '                                    & "     ,CUST.UNCHIN_SEIQTO_CD                        AS UNCHIN_SEIQTO_CD              " & vbNewLine _
    '                                    & "     ,CUST.SAGYO_SEIQTO_CD                         AS SAGYO_SEIQTO_CD               " & vbNewLine _
    '                                    & "     ,CUST.INKA_RPT_YN                             AS INKA_RPT_YN                   " & vbNewLine _
    '                                    & "     ,CUST.OUTKA_RPT_YN                            AS OUTKA_RPT_YN                  " & vbNewLine _
    '                                    & "     ,CUST.ZAI_RPT_YN                              AS ZAI_RPT_YN                    " & vbNewLine _
    '                                    & "     ,CUST.UNSO_TEHAI_KB                            AS UNSO_TEHAI_KB                  " & vbNewLine _
    '                                    & "     ,CUST.SP_UNSO_CD                              AS SP_UNSO_CD                    " & vbNewLine _
    '                                    & "     ,CUST.SP_UNSO_BR_CD                           AS SP_UNSO_BR_CD                 " & vbNewLine _
    '                                    & "     ,CUST.BETU_KYORI_CD                           AS BETU_KYORI_CD                 " & vbNewLine _
    '                                    & "     ,CUST.TAX_KB                                  AS TAX_KB                        " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_FREE_KIKAN                        AS HOKAN_FREE_KIKAN              " & vbNewLine _
    '                                    & "     ,CUST.SMPL_SAGYO                              AS SMPL_SAGYO                    " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_NIYAKU_CALCULATION                AS HOKAN_NIYAKU_CALCULATION      " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_NIYAKU_CALCULATION_OLD            AS HOKAN_NIYAKU_CALCULATION_OLD  " & vbNewLine _
    '                                    & "     ,CUST.NEW_JOB_NO                              AS NEW_JOB_NO                    " & vbNewLine _
    '                                    & "     ,CUST.OLD_JOB_NO                              AS OLD_JOB_NO                    " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_NIYAKU_KEISAN_YN                  AS HOKAN_NIYAKU_KEISAN_YN        " & vbNewLine _
    '                                    & "     ,CUST.UNTIN_CALCULATION_KB                    AS UNTIN_CALCULATION_KB          " & vbNewLine _
    '                                    & "     ,CUST.DENPYO_NM                               AS DENPYO_NM                     " & vbNewLine _
    '                                    & "     ,CUST.SOKO_CHANGE_KB                          AS SOKO_CHANGE_KB                " & vbNewLine _
    '                                    & "     ,CUST.DEFAULT_SOKO_CD                         AS DEFAULT_SOKO_CD               " & vbNewLine _
    '                                    & "     ,CUST.PICK_LIST_KB                            AS PICK_LIST_KB                  " & vbNewLine
    'START YANAI 要望番号836
    '''' <summary>
    '''' CUST_Mデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
    '                                    & "      CUST.NRS_BR_CD                               AS NRS_BR_CD                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_L                               AS CUST_NM_L                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_M                               AS CUST_NM_M                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_S                               AS CUST_NM_S                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_NM_SS                              AS CUST_NM_SS                    " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_L                               AS CUST_CD_L                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_M                               AS CUST_CD_M                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_S                               AS CUST_CD_S                     " & vbNewLine _
    '                                    & "     ,CUST.CUST_CD_SS                              AS CUST_CD_SS                    " & vbNewLine _
    '                                    & "     ,CUST.CUST_OYA_CD                             AS CUST_OYA_CD                   " & vbNewLine _
    '                                    & "     ,CUST.ZIP                                     AS ZIP                           " & vbNewLine _
    '                                    & "     ,CUST.AD_1                                    AS AD_1                          " & vbNewLine _
    '                                    & "     ,CUST.AD_2                                    AS AD_2                          " & vbNewLine _
    '                                    & "     ,CUST.AD_3                                    AS AD_3                          " & vbNewLine _
    '                                    & "     ,CUST.PIC                                     AS PIC                           " & vbNewLine _
    '                                    & "     ,CUST.FUKU_PIC                                AS FUKU_PIC                      " & vbNewLine _
    '                                    & "     ,CUST.TEL                                     AS TEL                           " & vbNewLine _
    '                                    & "     ,CUST.FAX                                     AS FAX                           " & vbNewLine _
    '                                    & "     ,CUST.MAIL                                    AS MAIL                          " & vbNewLine _
    '                                    & "     ,CUST.SAITEI_HAN_KB                           AS SAITEI_HAN_KB                 " & vbNewLine _
    '                                    & "     ,CUST.OYA_SEIQTO_CD                           AS OYA_SEIQTO_CD                 " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_SEIQTO_CD                         AS HOKAN_SEIQTO_CD               " & vbNewLine _
    '                                    & "     ,CUST.NIYAKU_SEIQTO_CD                        AS NIYAKU_SEIQTO_CD              " & vbNewLine _
    '                                    & "     ,CUST.UNCHIN_SEIQTO_CD                        AS UNCHIN_SEIQTO_CD              " & vbNewLine _
    '                                    & "     ,CUST.SAGYO_SEIQTO_CD                         AS SAGYO_SEIQTO_CD               " & vbNewLine _
    '                                    & "     ,CUST.INKA_RPT_YN                             AS INKA_RPT_YN                   " & vbNewLine _
    '                                    & "     ,CUST.OUTKA_RPT_YN                            AS OUTKA_RPT_YN                  " & vbNewLine _
    '                                    & "     ,CUST.ZAI_RPT_YN                              AS ZAI_RPT_YN                    " & vbNewLine _
    '                                    & "     ,CUST.UNSO_TEHAI_KB                            AS UNSO_TEHAI_KB                " & vbNewLine _
    '                                    & "     ,CUST.SP_UNSO_CD                              AS SP_UNSO_CD                    " & vbNewLine _
    '                                    & "     ,CUST.SP_UNSO_BR_CD                           AS SP_UNSO_BR_CD                 " & vbNewLine _
    '                                    & "     ,CUST.BETU_KYORI_CD                           AS BETU_KYORI_CD                 " & vbNewLine _
    '                                    & "     ,CUST.TAX_KB                                  AS TAX_KB                        " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_FREE_KIKAN                        AS HOKAN_FREE_KIKAN              " & vbNewLine _
    '                                    & "     ,CUST.SMPL_SAGYO                              AS SMPL_SAGYO                    " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_NIYAKU_CALCULATION                AS HOKAN_NIYAKU_CALCULATION      " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_NIYAKU_CALCULATION_OLD            AS HOKAN_NIYAKU_CALCULATION_OLD  " & vbNewLine _
    '                                    & "     ,CUST.NEW_JOB_NO                              AS NEW_JOB_NO                    " & vbNewLine _
    '                                    & "     ,CUST.OLD_JOB_NO                              AS OLD_JOB_NO                    " & vbNewLine _
    '                                    & "     ,CUST.HOKAN_NIYAKU_KEISAN_YN                  AS HOKAN_NIYAKU_KEISAN_YN        " & vbNewLine _
    '                                    & "     ,CUST.UNTIN_CALCULATION_KB                    AS UNTIN_CALCULATION_KB          " & vbNewLine _
    '                                    & "     ,CUST.DENPYO_NM                               AS DENPYO_NM                     " & vbNewLine _
    '                                    & "     ,CUST.SOKO_CHANGE_KB                          AS SOKO_CHANGE_KB                " & vbNewLine _
    '                                    & "     ,CUST.DEFAULT_SOKO_CD                         AS DEFAULT_SOKO_CD               " & vbNewLine _
    '                                    & "     ,CUST.PICK_LIST_KB                            AS PICK_LIST_KB                  " & vbNewLine _
    '                                    & "     ,Z1.KBN_NM1                                   AS CLOSE_KB_NM                   " & vbNewLine _
    '                                    & "     ,SEIQTO.CLOSE_KB                              AS CLOSE_KB                      " & vbNewLine
    ''' <summary>
    ''' CUST_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
                                        & "      CUST.NRS_BR_CD                               AS NRS_BR_CD                     " & vbNewLine _
                                        & "     ,CUST.CUST_NM_L                               AS CUST_NM_L                     " & vbNewLine _
                                        & "     ,CUST.CUST_NM_M                               AS CUST_NM_M                     " & vbNewLine _
                                        & "     ,CUST.CUST_NM_S                               AS CUST_NM_S                     " & vbNewLine _
                                        & "     ,CUST.CUST_NM_SS                              AS CUST_NM_SS                    " & vbNewLine _
                                        & "     ,CUST.CUST_CD_L                               AS CUST_CD_L                     " & vbNewLine _
                                        & "     ,CUST.CUST_CD_M                               AS CUST_CD_M                     " & vbNewLine _
                                        & "     ,CUST.CUST_CD_S                               AS CUST_CD_S                     " & vbNewLine _
                                        & "     ,CUST.CUST_CD_SS                              AS CUST_CD_SS                    " & vbNewLine _
                                        & "     ,CUST.CUST_OYA_CD                             AS CUST_OYA_CD                   " & vbNewLine _
                                        & "     ,CUST.ZIP                                     AS ZIP                           " & vbNewLine _
                                        & "     ,CUST.AD_1                                    AS AD_1                          " & vbNewLine _
                                        & "     ,CUST.AD_2                                    AS AD_2                          " & vbNewLine _
                                        & "     ,CUST.AD_3                                    AS AD_3                          " & vbNewLine _
                                        & "     ,CUST.PIC                                     AS PIC                           " & vbNewLine _
                                        & "     ,CUST.FUKU_PIC                                AS FUKU_PIC                      " & vbNewLine _
                                        & "     ,CUST.TEL                                     AS TEL                           " & vbNewLine _
                                        & "     ,CUST.FAX                                     AS FAX                           " & vbNewLine _
                                        & "     ,CUST.MAIL                                    AS MAIL                          " & vbNewLine _
                                        & "     ,CUST.SAITEI_HAN_KB                           AS SAITEI_HAN_KB                 " & vbNewLine _
                                        & "     ,CUST.OYA_SEIQTO_CD                           AS OYA_SEIQTO_CD                 " & vbNewLine _
                                        & "     ,CUST.HOKAN_SEIQTO_CD                         AS HOKAN_SEIQTO_CD               " & vbNewLine _
                                        & "     ,CUST.NIYAKU_SEIQTO_CD                        AS NIYAKU_SEIQTO_CD              " & vbNewLine _
                                        & "     ,CUST.UNCHIN_SEIQTO_CD                        AS UNCHIN_SEIQTO_CD              " & vbNewLine _
                                        & "     ,CUST.SAGYO_SEIQTO_CD                         AS SAGYO_SEIQTO_CD               " & vbNewLine _
                                        & "     ,CUST.INKA_RPT_YN                             AS INKA_RPT_YN                   " & vbNewLine _
                                        & "     ,CUST.OUTKA_RPT_YN                            AS OUTKA_RPT_YN                  " & vbNewLine _
                                        & "     ,CUST.ZAI_RPT_YN                              AS ZAI_RPT_YN                    " & vbNewLine _
                                        & "     ,CUST.UNSO_TEHAI_KB                            AS UNSO_TEHAI_KB                " & vbNewLine _
                                        & "     ,CUST.SP_UNSO_CD                              AS SP_UNSO_CD                    " & vbNewLine _
                                        & "     ,CUST.SP_UNSO_BR_CD                           AS SP_UNSO_BR_CD                 " & vbNewLine _
                                        & "     ,CUST.BETU_KYORI_CD                           AS BETU_KYORI_CD                 " & vbNewLine _
                                        & "     ,CUST.TAX_KB                                  AS TAX_KB                        " & vbNewLine _
                                        & "     ,CUST.HOKAN_FREE_KIKAN                        AS HOKAN_FREE_KIKAN              " & vbNewLine _
                                        & "     ,CUST.SMPL_SAGYO                              AS SMPL_SAGYO                    " & vbNewLine _
                                        & "     ,CUST.HOKAN_NIYAKU_CALCULATION                AS HOKAN_NIYAKU_CALCULATION      " & vbNewLine _
                                        & "     ,CUST.HOKAN_NIYAKU_CALCULATION_OLD            AS HOKAN_NIYAKU_CALCULATION_OLD  " & vbNewLine _
                                        & "     ,CUST.NEW_JOB_NO                              AS NEW_JOB_NO                    " & vbNewLine _
                                        & "     ,CUST.OLD_JOB_NO                              AS OLD_JOB_NO                    " & vbNewLine _
                                        & "     ,CUST.HOKAN_NIYAKU_KEISAN_YN                  AS HOKAN_NIYAKU_KEISAN_YN        " & vbNewLine _
                                        & "     ,CUST.UNTIN_CALCULATION_KB                    AS UNTIN_CALCULATION_KB          " & vbNewLine _
                                        & "     ,CUST.DENPYO_NM                               AS DENPYO_NM                     " & vbNewLine _
                                        & "     ,CUST.SOKO_CHANGE_KB                          AS SOKO_CHANGE_KB                " & vbNewLine _
                                        & "     ,CUST.DEFAULT_SOKO_CD                         AS DEFAULT_SOKO_CD               " & vbNewLine _
                                        & "     ,CUST.PICK_LIST_KB                            AS PICK_LIST_KB                  " & vbNewLine _
                                        & "     ,Z1.KBN_NM1                                   AS CLOSE_KB_NM                   " & vbNewLine _
                                        & "     ,SEIQTO.CLOSE_KB                              AS CLOSE_KB                      " & vbNewLine _
                                        & "     ,TARIFFSET.UNCHIN_TARIFF_CD1                  AS UNCHIN_TARIFF_CD1             " & vbNewLine _
                                        & "     ,TARIFF.UNCHIN_TARIFF_REM                     AS UNCHIN_TARIFF_REM1            " & vbNewLine _
                                        & "     ,TARIFFSET.UNCHIN_TARIFF_CD2                  AS UNCHIN_TARIFF_CD2             " & vbNewLine _
                                        & "     ,TARIFF2.UNCHIN_TARIFF_REM                    AS UNCHIN_TARIFF_REM2            " & vbNewLine _
                                        & "     ,TARIFFSET.EXTC_TARIFF_CD                     AS EXTC_TARIFF_CD                " & vbNewLine _
                                        & "     ,EXTC.EXTC_TARIFF_REM                         AS EXTC_TARIFF_REM               " & vbNewLine _
    'END YANAI 要望番号836
    'END YANAI 要望番号558

#End Region

#Region "FROM句"

    'START YANAI 要望番号558
    'Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
    '                                      & "                      $LM_MST$..M_CUST    AS CUST            " & vbNewLine _
    '                                      & "WHERE                 CUST.SYS_DEL_FLG    = '0'              " & vbNewLine
    'START YANAI 要望番号836
    'Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
    '                                      & "                      $LM_MST$..M_CUST    AS CUST            " & vbNewLine _
    '                                      & "LEFT JOIN $LM_MST$..M_SEIQTO    SEIQTO                    " & vbNewLine _
    '                                      & "ON     SEIQTO.NRS_BR_CD = CUST.NRS_BR_CD                  " & vbNewLine _
    '                                      & "AND    SEIQTO.SEIQTO_CD = CUST.OYA_SEIQTO_CD              " & vbNewLine _
    '                                      & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
    '                                      & "ON     Z1.KBN_CD = SEIQTO.CLOSE_KB                        " & vbNewLine _
    '                                      & "AND    Z1.KBN_GROUP_CD = 'S008'                           " & vbNewLine _
    '                                      & "WHERE                 CUST.SYS_DEL_FLG    = '0'           " & vbNewLine
    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_CUST    AS CUST            " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_SEIQTO    SEIQTO                    " & vbNewLine _
                                          & "ON     SEIQTO.NRS_BR_CD = CUST.NRS_BR_CD                  " & vbNewLine _
                                          & "AND    SEIQTO.SEIQTO_CD = CUST.OYA_SEIQTO_CD              " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN    Z1                           " & vbNewLine _
                                          & "ON     Z1.KBN_CD = SEIQTO.CLOSE_KB                        " & vbNewLine _
                                          & "AND    Z1.KBN_GROUP_CD = 'S008'                           " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_UNCHIN_TARIFF_SET TARIFFSET         " & vbNewLine _
                                          & "ON     TARIFFSET.NRS_BR_CD = CUST.NRS_BR_CD               " & vbNewLine _
                                          & "AND    TARIFFSET.CUST_CD_L = CUST.CUST_CD_L               " & vbNewLine _
                                          & "AND    TARIFFSET.CUST_CD_M = CUST.CUST_CD_M               " & vbNewLine _
                                          & "AND    TARIFFSET.SET_KB = '00'                            " & vbNewLine _
                                          & "LEFT JOIN                                                 " & vbNewLine _
                                          & "(SELECT                                                   " & vbNewLine _
                                          & "  UT.NRS_BR_CD                                            " & vbNewLine _
                                          & " ,UT.UNCHIN_TARIFF_CD                                     " & vbNewLine _
                                          & " ,UT.UNCHIN_TARIFF_REM                                    " & vbNewLine _
                                          & " ,UT.STR_DATE                                             " & vbNewLine _
                                          & " ,UT.TABLE_TP                                             " & vbNewLine _
                                          & " FROM $LM_MST$..M_UNCHIN_TARIFF AS UT                     " & vbNewLine _
                                          & " WHERE UT.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                          & " AND UT.UNCHIN_TARIFF_REM Is Not NULL                     " & vbNewLine _
                                          & " AND UT.UNCHIN_TARIFF_REM <> ''                           " & vbNewLine _
                                          & " GROUP BY                                                 " & vbNewLine _
                                          & "   UT.NRS_BR_CD                                           " & vbNewLine _
                                          & "  ,UT.UNCHIN_TARIFF_CD                                    " & vbNewLine _
                                          & "  ,UT.UNCHIN_TARIFF_REM                                   " & vbNewLine _
                                          & "  ,UT.STR_DATE                                            " & vbNewLine _
                                          & "  ,UT.TABLE_TP)  AS TARIFF                                " & vbNewLine _
                                          & "ON  TARIFFSET.NRS_BR_CD = TARIFF.NRS_BR_CD                " & vbNewLine _
                                          & "AND TARIFFSET.UNCHIN_TARIFF_CD1 = TARIFF.UNCHIN_TARIFF_CD " & vbNewLine _
                                          & "LEFT JOIN                                                 " & vbNewLine _
                                          & "(SELECT                                                   " & vbNewLine _
                                          & "  UT.NRS_BR_CD                                            " & vbNewLine _
                                          & " ,UT.UNCHIN_TARIFF_CD                                     " & vbNewLine _
                                          & " ,UT.UNCHIN_TARIFF_REM                                    " & vbNewLine _
                                          & " ,UT.STR_DATE                                             " & vbNewLine _
                                          & " ,UT.TABLE_TP                                             " & vbNewLine _
                                          & " FROM $LM_MST$..M_UNCHIN_TARIFF AS UT                     " & vbNewLine _
                                          & " WHERE UT.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                          & " AND UT.UNCHIN_TARIFF_REM Is Not NULL                     " & vbNewLine _
                                          & " AND UT.UNCHIN_TARIFF_REM <> ''                           " & vbNewLine _
                                          & " GROUP BY                                                 " & vbNewLine _
                                          & "   UT.NRS_BR_CD                                           " & vbNewLine _
                                          & "  ,UT.UNCHIN_TARIFF_CD                                    " & vbNewLine _
                                          & "  ,UT.UNCHIN_TARIFF_REM                                   " & vbNewLine _
                                          & "  ,UT.STR_DATE                                            " & vbNewLine _
                                          & "  ,UT.TABLE_TP)  AS TARIFF2                               " & vbNewLine _
                                          & "ON  TARIFFSET.NRS_BR_CD = TARIFF2.NRS_BR_CD               " & vbNewLine _
                                          & "AND TARIFFSET.UNCHIN_TARIFF_CD2 = TARIFF2.UNCHIN_TARIFF_CD " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_EXTC_UNCHIN EXTC                    " & vbNewLine _
                                          & "ON     EXTC.NRS_BR_CD = TARIFFSET.NRS_BR_CD               " & vbNewLine _
                                          & "AND    EXTC.EXTC_TARIFF_CD = TARIFFSET.EXTC_TARIFF_CD     " & vbNewLine _
                                          & "AND    EXTC.JIS_CD = '0000000'                            " & vbNewLine _
                                          & "WHERE                 CUST.SYS_DEL_FLG    = '0'           " & vbNewLine
    'END YANAI 要望番号836
    'END YANAI 要望番号558

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                               " & vbNewLine _
                                         & "     CUST.CUST_CD_L,CUST.CUST_CD_M,CUST.CUST_CD_S,CUST.CUST_CD_SS      " & vbNewLine

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
    ''' 荷主マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ260DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMZ260DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ260DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ260DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ260DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMZ260DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ260DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_OYA_CD", "CUST_OYA_CD")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("PIC", "PIC")
        map.Add("FUKU_PIC", "FUKU_PIC")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("MAIL", "MAIL")
        map.Add("SAITEI_HAN_KB", "SAITEI_HAN_KB")
        map.Add("OYA_SEIQTO_CD", "OYA_SEIQTO_CD")
        map.Add("HOKAN_SEIQTO_CD", "HOKAN_SEIQTO_CD")
        map.Add("NIYAKU_SEIQTO_CD", "NIYAKU_SEIQTO_CD")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")
        map.Add("INKA_RPT_YN", "INKA_RPT_YN")
        map.Add("OUTKA_RPT_YN", "OUTKA_RPT_YN")
        map.Add("ZAI_RPT_YN", "ZAI_RPT_YN")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("BETU_KYORI_CD", "BETU_KYORI_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("SMPL_SAGYO", "SMPL_SAGYO")
        map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")
        map.Add("HOKAN_NIYAKU_CALCULATION_OLD", "HOKAN_NIYAKU_CALCULATION_OLD")
        map.Add("NEW_JOB_NO", "NEW_JOB_NO")
        map.Add("OLD_JOB_NO", "OLD_JOB_NO")
        map.Add("HOKAN_NIYAKU_KEISAN_YN", "HOKAN_NIYAKU_KEISAN_YN")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("DENPYO_NM", "DENPYO_NM")
        map.Add("SOKO_CHANGE_KB", "SOKO_CHANGE_KB")
        map.Add("DEFAULT_SOKO_CD", "DEFAULT_SOKO_CD")
        map.Add("PICK_LIST_KB", "PICK_LIST_KB")
        'START YANAI 要望番号558
        map.Add("CLOSE_KB_NM", "CLOSE_KB_NM")
        map.Add("CLOSE_KB", "CLOSE_KB")
        'END YANAI 要望番号558
        'START YANAI 要望番号836
        map.Add("UNCHIN_TARIFF_CD1", "UNCHIN_TARIFF_CD1")
        map.Add("UNCHIN_TARIFF_REM1", "UNCHIN_TARIFF_REM1")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("UNCHIN_TARIFF_REM2", "UNCHIN_TARIFF_REM2")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        'END YANAI 要望番号836

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ260OUT")

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
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_NM_L LIKE @CUST_NM_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_NM_M LIKE @CUST_NM_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_NM_S LIKE @CUST_NM_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_S", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_NM_SS LIKE @CUST_NM_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_SS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_CD_S LIKE @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND CUST.CUST_CD_SS LIKE @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


        End With

    End Sub

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

#End Region

End Class
