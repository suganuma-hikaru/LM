' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM040H : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    'Select文
    Private Const SELECT_DELETE_DATA_001 As String = "SYS_DEL_FLG = '0' "

#End Region

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(DEST.DEST_CD)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TARIFF_SET_COUNT As String = " SELECT COUNT(UNCHIN_TARIFF_SET.CUST_CD_L)                AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                  " & vbNewLine

    'START YANAI 要望番号881
    '''' <summary>
    '''' DEST_M、UNCHIN_TARIFF_SET_Mデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                                        " & vbNewLine _
    '                                        & "	      DEST.NRS_BR_CD                                          AS NRS_BR_CD                    " & vbNewLine _
    '                                        & "	     ,NRSBR.NRS_BR_NM                                         AS NRS_BR_NM                    " & vbNewLine _
    '                                        & "	     ,DEST.CUST_CD_L                                          AS CUST_CD_L                    " & vbNewLine _
    '                                        & "	     ,CUST.CUST_NM_L                                          AS CUST_NM_L                    " & vbNewLine _
    '                                        & "	     ,DEST.DEST_CD                                            AS DEST_CD                      " & vbNewLine _
    '                                        & "	     ,DEST.EDI_CD                                             AS EDI_CD                       " & vbNewLine _
    '                                        & "	     ,DEST.DEST_NM                                            AS DEST_NM                      " & vbNewLine _
    '                                        & "	     ,DEST.ZIP                                                AS ZIP                          " & vbNewLine _
    '                                        & "	     ,DEST.AD_1                                               AS AD_1                         " & vbNewLine _
    '                                        & "	     ,DEST.AD_2                                               AS AD_2                         " & vbNewLine _
    '                                        & "	     ,DEST.AD_3                                               AS AD_3                         " & vbNewLine _
    '                                        & "	     ,DEST.CUST_DEST_CD                                       AS CUST_DEST_CD                 " & vbNewLine _
    '                                        & "	     ,DEST.TEL                                                AS TEL                          " & vbNewLine _
    '                                        & "	     ,DEST.JIS                                                AS JIS                          " & vbNewLine _
    '                                        & "	     ,DEST.SP_NHS_KB                                          AS SP_NHS_KB                    " & vbNewLine _
    '                                        & "	     ,KBN1.KBN_NM1                                            AS SP_NHS_KB_NM                 " & vbNewLine _
    '                                        & "	     ,DEST.FAX                                                AS FAX                          " & vbNewLine _
    '                                        & "	     ,DEST.KYORI                                              AS KYORI                        " & vbNewLine _
    '                                        & "	     ,DEST.COA_YN                                             AS COA_YN                       " & vbNewLine _
    '                                        & "	     ,KBN2.KBN_NM1                                            AS COA_YN_NM                    " & vbNewLine _
    '                                        & "	     ,DEST.SP_UNSO_CD                                         AS SP_UNSO_CD                   " & vbNewLine _
    '                                        & "	     ,DEST.SP_UNSO_BR_CD                                      AS SP_UNSO_BR_CD                " & vbNewLine _
    '                                        & "	     ,UNSOCO.UNSOCO_NM +' ' + UNSOCO.UNSOCO_BR_NM             AS SP_UNSO_NM                   " & vbNewLine _
    '                                        & "	     ,DEST.PICK_KB                                            AS PICK_KB                      " & vbNewLine _
    '                                        & "	     ,KBN3.KBN_NM1                                            AS PICK_KB_NM                   " & vbNewLine _
    '                                        & "	     ,DEST.BIN_KB                                             AS BIN_KB                       " & vbNewLine _
    '                                        & "	     ,KBN4.KBN_NM1                                            AS BIN_KB_NM                    " & vbNewLine _
    '                                        & "	     ,DEST.MOTO_CHAKU_KB                                      AS MOTO_CHAKU_KB                " & vbNewLine _
    '                                        & "	     ,KBN5.KBN_NM1                                            AS MOTO_CHAKU_KB_NM             " & vbNewLine _
    '                                        & "	     ,DEST.CARGO_TIME_LIMIT                                   AS CARGO_TIME_LIMIT             " & vbNewLine _
    '                                        & "	     ,DEST.LARGE_CAR_YN                                       AS LARGE_CAR_YN                 " & vbNewLine _
    '                                        & "	     ,KBN6.KBN_NM1                                            AS LARGE_CAR_YN_NM              " & vbNewLine _
    '                                        & "	     ,DEST.DELI_ATT                                           AS DELI_ATT                     " & vbNewLine _
    '                                        & "	     ,DEST.SALES_CD                                           AS SALES_CD                     " & vbNewLine _
    '                                        & "	     ,CUST2.CUST_NM_L                                         AS SALES_NM                     " & vbNewLine _
    '                                        & "	     ,DEST.URIAGE_CD                                          AS URIAGE_CD                    " & vbNewLine _
    '                                        & "	     ,DEST2.DEST_NM                                           AS URIAGE_NM                    " & vbNewLine _
    '                                        & "	     ,DEST.UNCHIN_SEIQTO_CD                                   AS UNCHIN_SEIQTO_CD             " & vbNewLine _
    '                                        & "	     ,SEIQTO.SEIQTO_NM +' ' + SEIQTO.SEIQTO_BUSYO_NM          AS UNCHIN_SEIQTO_NM             " & vbNewLine _
    '                                        & "	     ,DEST.SYS_ENT_DATE                                       AS SYS_ENT_DATE                 " & vbNewLine _
    '                                        & "	     ,USER1.USER_NM                                           AS SYS_ENT_USER_NM              " & vbNewLine _
    '                                        & "	     ,DEST.SYS_UPD_DATE                                       AS SYS_UPD_DATE                 " & vbNewLine _
    '                                        & "	     ,DEST.SYS_UPD_TIME                                       AS SYS_UPD_TIME                 " & vbNewLine _
    '                                        & "	     ,USER2.USER_NM                                           AS SYS_UPD_USER_NM              " & vbNewLine _
    '                                        & "	     ,DEST.SYS_DEL_FLG                                        AS SYS_DEL_FLG                  " & vbNewLine _
    '                                        & "	     ,KBN7.KBN_NM1                                            AS SYS_DEL_NM                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB                      AS TARIFF_BUNRUI_KB             " & vbNewLine _
    '                                        & "	     ,KBN8.KBN_NM1                                            AS TARIFF_BUNRUI_KB_NM          " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1                     AS UNCHIN_TARIFF_CD1            " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2                     AS UNCHIN_TARIFF_CD2            " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.EXTC_TARIFF_CD                        AS EXTC_TARIFF_CD               " & vbNewLine _
    '                                        & "	     ,EXTC_UNCHIN.EXTC_TARIFF_REM                             AS EXTC_TARIFF_NM               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.YOKO_TARIFF_CD                        AS YOKO_TARIFF_CD               " & vbNewLine _
    '                                        & "	     ,YOKO_TARIFF_HD.YOKO_REM                                 AS YOKO_TARIFF_NM               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.CUST_CD_M                             AS CUST_CD_M                    " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.SET_MST_CD                            AS SET_MST_CD                   " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.SET_KB                                AS SET_KB                       " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.SYS_UPD_DATE                          AS SYS_UPD_DATE_T               " & vbNewLine _
    '                                        & "	     ,UNCHIN_TARIFF_SET.SYS_UPD_TIME                          AS SYS_UPD_TIME_T               " & vbNewLine _
    '                                        & "	     ,UNCHIN.UNCHIN_TARIFF_REM                                AS UNCHIN_TARIFF_NM1            " & vbNewLine _
    '                                        & "	     ,UNCHIN2.UNCHIN_TARIFF_REM                               AS UNCHIN_TARIFF_NM2            " & vbNewLine

    '要望番号:1330 terakawa 2012.08.09 KANA_NM追加 Start
    ''' <summary>
    ''' DEST_M、UNCHIN_TARIFF_SET_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                        " & vbNewLine _
                                            & "	      DEST.NRS_BR_CD                                          AS NRS_BR_CD                    " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                                         AS NRS_BR_NM                    " & vbNewLine _
                                            & "	     ,DEST.CUST_CD_L                                          AS CUST_CD_L                    " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_L                                          AS CUST_NM_L                    " & vbNewLine _
                                            & "	     ,DEST.DEST_CD                                            AS DEST_CD                      " & vbNewLine _
                                            & "	     ,DEST.EDI_CD                                             AS EDI_CD                       " & vbNewLine _
                                            & "	     ,DEST.DEST_NM                                            AS DEST_NM                      " & vbNewLine _
                                            & "	     ,DEST.KANA_NM                                            AS KANA_NM                      " & vbNewLine _
                                            & "	     ,DEST.ZIP                                                AS ZIP                          " & vbNewLine _
                                            & "	     ,DEST.AD_1                                               AS AD_1                         " & vbNewLine _
                                            & "	     ,DEST.AD_2                                               AS AD_2                         " & vbNewLine _
                                            & "	     ,DEST.AD_3                                               AS AD_3                         " & vbNewLine _
                                            & "	     ,DEST.CUST_DEST_CD                                       AS CUST_DEST_CD                 " & vbNewLine _
                                            & "	     ,DEST.TEL                                                AS TEL                          " & vbNewLine _
                                            & "	     ,DEST.JIS                                                AS JIS                          " & vbNewLine _
                                            & "	     ,DEST.SP_NHS_KB                                          AS SP_NHS_KB                    " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                            AS SP_NHS_KB_NM                 " & vbNewLine _
                                            & "	     ,DEST.FAX                                                AS FAX                          " & vbNewLine _
                                            & "	     ,DEST.KYORI                                              AS KYORI                        " & vbNewLine _
                                            & "	     ,DEST.COA_YN                                             AS COA_YN                       " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                            AS COA_YN_NM                    " & vbNewLine _
                                            & "	     ,DEST.SP_UNSO_CD                                         AS SP_UNSO_CD                   " & vbNewLine _
                                            & "	     ,DEST.SP_UNSO_BR_CD                                      AS SP_UNSO_BR_CD                " & vbNewLine _
                                            & "	     ,UNSOCO.UNSOCO_NM +' ' + UNSOCO.UNSOCO_BR_NM             AS SP_UNSO_NM                   " & vbNewLine _
                                            & "	     ,DEST.PICK_KB                                            AS PICK_KB                      " & vbNewLine _
                                            & "	     ,KBN3.KBN_NM1                                            AS PICK_KB_NM                   " & vbNewLine _
                                            & "	     ,DEST.BIN_KB                                             AS BIN_KB                       " & vbNewLine _
                                            & "	     ,KBN4.KBN_NM1                                            AS BIN_KB_NM                    " & vbNewLine _
                                            & "	     ,DEST.MOTO_CHAKU_KB                                      AS MOTO_CHAKU_KB                " & vbNewLine _
                                            & "	     ,KBN5.KBN_NM1                                            AS MOTO_CHAKU_KB_NM             " & vbNewLine _
                                            & "	     ,DEST.CARGO_TIME_LIMIT                                   AS CARGO_TIME_LIMIT             " & vbNewLine _
                                            & "	     ,DEST.LARGE_CAR_YN                                       AS LARGE_CAR_YN                 " & vbNewLine _
                                            & "	     ,KBN6.KBN_NM1                                            AS LARGE_CAR_YN_NM              " & vbNewLine _
                                            & "	     ,DEST.DELI_ATT                                           AS DELI_ATT                     " & vbNewLine _
                                            & "	     ,DEST.SALES_CD                                           AS SALES_CD                     " & vbNewLine _
                                            & "	     ,CUST2.CUST_NM_L                                         AS SALES_NM                     " & vbNewLine _
                                            & "	     ,DEST.URIAGE_CD                                          AS URIAGE_CD                    " & vbNewLine _
                                            & "	     ,DEST2.DEST_NM                                           AS URIAGE_NM                    " & vbNewLine _
                                            & "	     ,DEST.UNCHIN_SEIQTO_CD                                   AS UNCHIN_SEIQTO_CD             " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQTO_NM +' ' + SEIQTO.SEIQTO_BUSYO_NM          AS UNCHIN_SEIQTO_NM             " & vbNewLine _
                                            & "	     ,DEST.SYS_ENT_DATE                                       AS SYS_ENT_DATE                 " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                           AS SYS_ENT_USER_NM              " & vbNewLine _
                                            & "	     ,DEST.SYS_UPD_DATE                                       AS SYS_UPD_DATE                 " & vbNewLine _
                                            & "	     ,DEST.SYS_UPD_TIME                                       AS SYS_UPD_TIME                 " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                           AS SYS_UPD_USER_NM              " & vbNewLine _
                                            & "	     ,DEST.SYS_DEL_FLG                                        AS SYS_DEL_FLG                  " & vbNewLine _
                                            & "	     ,KBN7.KBN_NM1                                            AS SYS_DEL_NM                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB                      AS TARIFF_BUNRUI_KB             " & vbNewLine _
                                            & "	     ,KBN8.KBN_NM1                                            AS TARIFF_BUNRUI_KB_NM          " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1                     AS UNCHIN_TARIFF_CD1            " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2                     AS UNCHIN_TARIFF_CD2            " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.EXTC_TARIFF_CD                        AS EXTC_TARIFF_CD               " & vbNewLine _
                                            & "	     ,EXTC_UNCHIN.EXTC_TARIFF_REM                             AS EXTC_TARIFF_NM               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.YOKO_TARIFF_CD                        AS YOKO_TARIFF_CD               " & vbNewLine _
                                            & "	     ,YOKO_TARIFF_HD.YOKO_REM                                 AS YOKO_TARIFF_NM               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.CUST_CD_M                             AS CUST_CD_M                    " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.SET_MST_CD                            AS SET_MST_CD                   " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.SET_KB                                AS SET_KB                       " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.SYS_UPD_DATE                          AS SYS_UPD_DATE_T               " & vbNewLine _
                                            & "	     ,UNCHIN_TARIFF_SET.SYS_UPD_TIME                          AS SYS_UPD_TIME_T               " & vbNewLine _
                                            & "	     ,UNCHIN.UNCHIN_TARIFF_REM                                AS UNCHIN_TARIFF_NM1            " & vbNewLine _
                                            & "	     ,UNCHIN2.UNCHIN_TARIFF_REM                               AS UNCHIN_TARIFF_NM2            " & vbNewLine _
                                            & "	     ,DEST.REMARK                                             AS REMARK                       " & vbNewLine _
                                            & "	     ,DEST.SHIHARAI_AD                                        AS SHIHARAI_AD                  " & vbNewLine
    '要望番号:1330 terakawa 2012.08.09 KANA_NM追加 End
    'END YANAI 要望番号881

    ''' <summary>
    ''' DEST_DETAILS_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = " SELECT                                                                           " & vbNewLine _
                                            & "	      DEST_DETAILS.NRS_BR_CD                        AS NRS_BR_CD                  " & vbNewLine _
                                            & "	     ,DEST_DETAILS.CUST_CD_L                        AS CUST_CD_L                  " & vbNewLine _
                                            & "	     ,DEST_DETAILS.DEST_CD                          AS DEST_CD                    " & vbNewLine _
                                            & "	     ,DEST_DETAILS.DEST_CD_EDA                      AS DEST_CD_EDA                " & vbNewLine _
                                            & "	     ,DEST_DETAILS.SUB_KB                           AS SUB_KB                     " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                  AS SUB_KB_NM                  " & vbNewLine _
                                            & "	     ,DEST_DETAILS.SET_NAIYO                        AS SET_NAIYO                  " & vbNewLine _
                                            & "	     ,DEST_DETAILS.REMARK                           AS REMARK                     " & vbNewLine _
                                            & "	     ,'1'                                           AS UPD_FLG                    " & vbNewLine _
                                            & "	     ,DEST_DETAILS.SYS_DEL_FLG                      AS SYS_DEL_FLG                " & vbNewLine

    ''' <summary>
    ''' DEST_DETAILS_M(MAX届先コード枝番)データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAX As String = " SELECT                                                                             " & vbNewLine _
                                            & "	      MAX(DEST_DETAILS.DEST_CD_EDA)                 AS DEST_MAXCD_EDA             " & vbNewLine

    ''' <summary>
    ''' UNCHIN_TARIFF_SET_M(MAXセットマスタコード)データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TARIFF_SET_MAX As String = " SELECT                                                                  " & vbNewLine _
                                            & "	      MAX(UNCHIN_TARIFF_SET.SET_MST_CD)             AS SET_MST_MAXCD              " & vbNewLine

    ''' <summary>
    ''' ZIP_M(JISコード)データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ZIP_JIS As String = " SELECT                                                                         " & vbNewLine _
                                            & "	      ZIP.JIS_CD                                    AS JIS                        " & vbNewLine _
                                            & "	     ,ZIP.KEN_N + ZIP.CITY_N                        AS AD_1                       " & vbNewLine _
                                            & "	     ,ZIP.TOWN_N                                    AS AD_2                       " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                                 " & vbNewLine _
                                          & "                      $LM_MST$..M_DEST AS DEST                                       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER AS USER1                                      " & vbNewLine _
                                          & "        ON DEST.SYS_ENT_USER                       = USER1.USER_CD                   " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER2                                     " & vbNewLine _
                                          & "       ON DEST.SYS_UPD_USER                        = USER2.USER_CD                   " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                                   " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = NRSBR.NRS_BR_CD                 " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST  AS CUST                                      " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = CUST.NRS_BR_CD                  " & vbNewLine _
                                          & "       AND DEST.CUST_CD_L                          = CUST.CUST_CD_L                  " & vbNewLine _
                                          & "       AND CUST.CUST_CD_M                          = '00'                            " & vbNewLine _
                                          & "       AND CUST.CUST_CD_S                          = '00'                            " & vbNewLine _
                                          & "       AND CUST.CUST_CD_SS                         = '00'                            " & vbNewLine _
                                          & "       AND CUST.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                                    " & vbNewLine _
                                          & "        ON DEST.SP_NHS_KB                          = KBN1.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD                       = 'S013'                          " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                                    " & vbNewLine _
                                          & "        ON DEST.COA_YN                             = KBN2.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD                       = 'B005'                          " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO  AS UNSOCO                                  " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = UNSOCO.NRS_BR_CD                " & vbNewLine _
                                          & "       AND DEST.SP_UNSO_CD                         = UNSOCO.UNSOCO_CD                " & vbNewLine _
                                          & "       AND DEST.SP_UNSO_BR_CD                      = UNSOCO.UNSOCO_BR_CD             " & vbNewLine _
                                          & "       AND UNSOCO.SYS_DEL_FLG                      = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3                                    " & vbNewLine _
                                          & "        ON DEST.PICK_KB                            = KBN3.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD                       = 'P001'                          " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4                                    " & vbNewLine _
                                          & "        ON DEST.BIN_KB                             = KBN4.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD                       = 'U001'                          " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN5                                    " & vbNewLine _
                                          & "        ON DEST.MOTO_CHAKU_KB                      = KBN5.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD                       = 'M001'                          " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN6                                    " & vbNewLine _
                                          & "        ON DEST.LARGE_CAR_YN                       = KBN6.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN6.KBN_GROUP_CD                       = 'K017'                          " & vbNewLine _
                                          & "       AND KBN6.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST  AS CUST2                                     " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = CUST2.NRS_BR_CD                 " & vbNewLine _
                                          & "       AND DEST.SALES_CD                           = CUST2.CUST_CD_L                 " & vbNewLine _
                                          & "       AND CUST2.CUST_CD_M                         = '00'                            " & vbNewLine _
                                          & "       AND CUST2.CUST_CD_S                         = '00'                            " & vbNewLine _
                                          & "       AND CUST2.CUST_CD_SS                        = '00'                            " & vbNewLine _
                                          & "       AND CUST2.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST  AS DEST2                                     " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = DEST2.NRS_BR_CD                 " & vbNewLine _
                                          & "       AND DEST.CUST_CD_L                          = DEST2.CUST_CD_L                 " & vbNewLine _
                                          & "       AND DEST.URIAGE_CD                          = DEST2.DEST_CD                   " & vbNewLine _
                                          & "       AND DEST2.SYS_DEL_FLG                       = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SEIQTO  AS SEIQTO                                  " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = SEIQTO.NRS_BR_CD                " & vbNewLine _
                                          & "       AND DEST.UNCHIN_SEIQTO_CD                   = SEIQTO.SEIQTO_CD                " & vbNewLine _
                                          & "       AND SEIQTO.SYS_DEL_FLG                      = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN7                                    " & vbNewLine _
                                          & "        ON DEST.SYS_DEL_FLG                        = KBN7.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN7.KBN_GROUP_CD                       = 'S051'                          " & vbNewLine _
                                          & "       AND KBN7.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_UNCHIN_TARIFF_SET  AS UNCHIN_TARIFF_SET            " & vbNewLine _
                                          & "        ON DEST.NRS_BR_CD                          = UNCHIN_TARIFF_SET.NRS_BR_CD     " & vbNewLine _
                                          & "       AND DEST.CUST_CD_L                          = UNCHIN_TARIFF_SET.CUST_CD_L     " & vbNewLine _
                                          & "       AND UNCHIN_TARIFF_SET.CUST_CD_M             = '00'                            " & vbNewLine _
                                          & "       AND DEST.DEST_CD                            = UNCHIN_TARIFF_SET.DEST_CD       " & vbNewLine _
                                          & "       AND UNCHIN_TARIFF_SET.SET_KB                = '01'                            " & vbNewLine _
                                          & "       AND UNCHIN_TARIFF_SET.SYS_DEL_FLG           = '0'                             " & vbNewLine _
                                          & "                      LEFT OUTER JOIN                                                                        " & vbNewLine _
                                          & "                         (SELECT                                                                             " & vbNewLine _
                                          & "                                       UT.NRS_BR_CD                                                          " & vbNewLine _
                                          & "                                      ,UT.UNCHIN_TARIFF_CD                                                   " & vbNewLine _
                                          & "                                      ,UT.UNCHIN_TARIFF_REM                                                  " & vbNewLine _
                                          & "                                      ,MAX(UT.STR_DATE) AS STR_DATE	                                      " & vbNewLine _
                                          & "                                      ,MAX(UT.DATA_TP) AS DATA_TP                                            " & vbNewLine _
                                          & "                                      ,MAX(UT.TABLE_TP) AS TABLE_TP                                          " & vbNewLine _
                                          & "                          FROM                                                                               " & vbNewLine _
                                          & "                                      $LM_MST$..M_UNCHIN_TARIFF     AS UT                                    " & vbNewLine _
                                          & "            INNER JOIN (                                                                                     " & vbNewLine _
                                          & "                        SELECT                                                                               " & vbNewLine _
                                          & "                 UT.NRS_BR_CD                                                                                " & vbNewLine _
                                          & "                ,UT.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
                                          & "                ,UT2.STR_DATE                                                                                " & vbNewLine _
                                          & "                ,MIN(UT.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                        " & vbNewLine _
                                          & "            FROM $LM_MST$..M_UNCHIN_TARIFF          AS UT                                                    " & vbNewLine _
                                          & "           INNER JOIN (                                                                                      " & vbNewLine _
                                          & "                          SELECT                                                                             " & vbNewLine _
                                          & "                                 MAX(STR_DATE) AS STR_DATE                                                   " & vbNewLine _
                                          & "                                ,NRS_BR_CD                                                                   " & vbNewLine _
                                          & "                                ,UNCHIN_TARIFF_CD                                                            " & vbNewLine _
                                          & "                           FROM $LM_MST$..M_UNCHIN_TARIFF     AS UT2                                         " & vbNewLine _
                                          & "                          WHERE SYS_DEL_FLG = '0'                                                            " & vbNewLine _
                                          & "                            AND   UT2.UNCHIN_TARIFF_REM   IS NOT NULL                                        " & vbNewLine _
                                          & "                            AND   UT2.UNCHIN_TARIFF_REM   <>  ''                                             " & vbNewLine _
                                          & "--DEL 2019/08/08 006992                            AND   UT2.STR_DATE            <= '20110819'                                      " & vbNewLine _
                                          & "                          GROUP BY                                                                           " & vbNewLine _
                                          & "                                   UT2.NRS_BR_CD                                                             " & vbNewLine _
                                          & "                                  ,UT2.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
                                          & "                             ) AS UT2                                                                        " & vbNewLine _
                                          & "                ON UT.NRS_BR_CD            = UT2.NRS_BR_CD                                                   " & vbNewLine _
                                          & "               AND UT.UNCHIN_TARIFF_CD     = UT2.UNCHIN_TARIFF_CD                                            " & vbNewLine _
                                          & "               AND UT.STR_DATE             = UT2.STR_DATE                                                    " & vbNewLine _
                                          & "             WHERE UT.SYS_DEL_FLG          = '0'                                                             " & vbNewLine _
                                          & "             GROUP BY                                                                                        " & vbNewLine _
                                          & "                     UT.NRS_BR_CD                                                                            " & vbNewLine _
                                          & "                    ,UT.UNCHIN_TARIFF_CD                                                                     " & vbNewLine _
                                          & "                    ,UT2.STR_DATE                                                                            " & vbNewLine _
                                          & "                            )  AS UT2                                                                        " & vbNewLine _
                                          & "                ON UT.NRS_BR_CD               = UT2.NRS_BR_CD                                                " & vbNewLine _
                                          & "               AND UT.UNCHIN_TARIFF_CD        = UT2.UNCHIN_TARIFF_CD                                         " & vbNewLine _
                                          & "               AND UT.UNCHIN_TARIFF_CD_EDA    = UT2.UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
                                          & "               AND UT.STR_DATE                = UT2.STR_DATE                                                 " & vbNewLine _
                                          & "            WHERE UT.SYS_DEL_FLG          = '0'                                                              " & vbNewLine _
                                          & "                   GROUP BY                                                                                  " & vbNewLine _
                                          & "                         UT.NRS_BR_CD                                                                        " & vbNewLine _
                                          & "                        ,UT.UNCHIN_TARIFF_CD                                                                 " & vbNewLine _
                                          & "                        ,UT.UNCHIN_TARIFF_REM                                                                " & vbNewLine _
                                          & "                        ,UT.STR_DATE               ) AS UNCHIN                                               " & vbNewLine _
                                          & "                             ON UNCHIN_TARIFF_SET.NRS_BR_CD                     = UNCHIN.NRS_BR_CD           " & vbNewLine _
                                          & "                            AND UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD1             = UNCHIN.UNCHIN_TARIFF_CD    " & vbNewLine _
                                          & "                      LEFT OUTER JOIN                                                                        " & vbNewLine _
                                          & "                         (SELECT                                                                             " & vbNewLine _
                                          & "                                       UT.NRS_BR_CD                                                          " & vbNewLine _
                                          & "                                      ,UT.UNCHIN_TARIFF_CD                                                   " & vbNewLine _
                                          & "                                      ,UT.UNCHIN_TARIFF_REM                                                  " & vbNewLine _
                                          & "                                      ,MAX(UT.STR_DATE) AS STR_DATE                                          " & vbNewLine _
                                          & "                                      ,MAX(UT.DATA_TP) AS DATA_TP                                            " & vbNewLine _
                                          & "                                      ,MAX(UT.TABLE_TP) AS TABLE_TP                                          " & vbNewLine _
                                          & "                          FROM                                                                               " & vbNewLine _
                                          & "                                      $LM_MST$..M_UNCHIN_TARIFF     AS UT                                  " & vbNewLine _
                                          & "          INNER JOIN (                                                                                     " & vbNewLine _
                                          & "                        SELECT                                                                             " & vbNewLine _
                                          & "                 UT.NRS_BR_CD                                                                              " & vbNewLine _
                                          & "                ,UT.UNCHIN_TARIFF_CD                                                                       " & vbNewLine _
                                          & "                ,UT2.STR_DATE                                                                              " & vbNewLine _
                                          & "                ,MIN(UT.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                      " & vbNewLine _
                                          & "            FROM $LM_MST$..M_UNCHIN_TARIFF          AS UT                                                  " & vbNewLine _
                                          & "           INNER JOIN (                                                                                    " & vbNewLine _
                                          & "                          SELECT                                                                           " & vbNewLine _
                                          & "                                 MAX(STR_DATE) AS STR_DATE                                                 " & vbNewLine _
                                          & "                                ,NRS_BR_CD                                                                 " & vbNewLine _
                                          & "                                ,UNCHIN_TARIFF_CD                                                          " & vbNewLine _
                                          & "                           FROM $LM_MST$..M_UNCHIN_TARIFF     AS UT2                                       " & vbNewLine _
                                          & "                          WHERE SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                          & "                            AND   UT2.UNCHIN_TARIFF_REM   IS NOT NULL                                      " & vbNewLine _
                                          & "                            AND   UT2.UNCHIN_TARIFF_REM   <>  ''                                           " & vbNewLine _
                                          & "--DEL 2019/08/08 006992                            AND   UT2.STR_DATE            <= '20110819'                                    " & vbNewLine _
                                          & "                          GROUP BY                                                                         " & vbNewLine _
                                          & "                                   UT2.NRS_BR_CD                                                           " & vbNewLine _
                                          & "                                  ,UT2.UNCHIN_TARIFF_CD                                                    " & vbNewLine _
                                          & "                             ) AS UT2                                                                      " & vbNewLine _
                                          & "                ON UT.NRS_BR_CD            = UT2.NRS_BR_CD                                                 " & vbNewLine _
                                          & "               AND UT.UNCHIN_TARIFF_CD     = UT2.UNCHIN_TARIFF_CD                                          " & vbNewLine _
                                          & "               AND UT.STR_DATE             = UT2.STR_DATE                                                  " & vbNewLine _
                                          & "             WHERE UT.SYS_DEL_FLG          = '0'                                                           " & vbNewLine _
                                          & "             GROUP BY                                                                                      " & vbNewLine _
                                          & "                     UT.NRS_BR_CD                                                                          " & vbNewLine _
                                          & "                    ,UT.UNCHIN_TARIFF_CD                                                                   " & vbNewLine _
                                          & "                    ,UT2.STR_DATE                                                                          " & vbNewLine _
                                          & "                            )  AS UT2                                                                      " & vbNewLine _
                                          & "           ON UT.NRS_BR_CD               = UT2.NRS_BR_CD                                                   " & vbNewLine _
                                          & "               AND UT.UNCHIN_TARIFF_CD        = UT2.UNCHIN_TARIFF_CD                                       " & vbNewLine _
                                          & "               AND UT.UNCHIN_TARIFF_CD_EDA    = UT2.UNCHIN_TARIFF_CD_EDA                                   " & vbNewLine _
                                          & "               AND UT.STR_DATE                = UT2.STR_DATE                                               " & vbNewLine _
                                          & "             WHERE UT.SYS_DEL_FLG          = '0'                                                           " & vbNewLine _
                                          & "                   GROUP BY                                                                                " & vbNewLine _
                                          & "                         UT.NRS_BR_CD                                                                      " & vbNewLine _
                                          & "                        ,UT.UNCHIN_TARIFF_CD                                                               " & vbNewLine _
                                          & "                        ,UT.UNCHIN_TARIFF_REM      ) AS UNCHIN2                                            " & vbNewLine _
                                          & "                             ON UNCHIN_TARIFF_SET.NRS_BR_CD                     = UNCHIN2.NRS_BR_CD                                      " & vbNewLine _
                                          & "                            AND UNCHIN_TARIFF_SET.UNCHIN_TARIFF_CD2             = UNCHIN2.UNCHIN_TARIFF_CD                               " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN8                                    " & vbNewLine _
                                          & "        ON UNCHIN_TARIFF_SET.TARIFF_BUNRUI_KB      = KBN8.KBN_CD                     " & vbNewLine _
                                          & "       AND KBN8.KBN_GROUP_CD                       = 'T015'                          " & vbNewLine _
                                          & "       AND KBN8.SYS_DEL_FLG                        = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_EXTC_UNCHIN  AS EXTC_UNCHIN                        " & vbNewLine _
                                          & "        ON UNCHIN_TARIFF_SET.NRS_BR_CD             = EXTC_UNCHIN.NRS_BR_CD           " & vbNewLine _
                                          & "       AND UNCHIN_TARIFF_SET.EXTC_TARIFF_CD        = EXTC_UNCHIN.EXTC_TARIFF_CD      " & vbNewLine _
                                          & "       AND EXTC_UNCHIN.JIS_CD                      = '0000000'                       " & vbNewLine _
                                          & "       AND EXTC_UNCHIN.SYS_DEL_FLG                 = '0'                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_YOKO_TARIFF_HD  AS YOKO_TARIFF_HD                  " & vbNewLine _
                                          & "        ON UNCHIN_TARIFF_SET.NRS_BR_CD             = YOKO_TARIFF_HD.NRS_BR_CD        " & vbNewLine _
                                          & "       AND UNCHIN_TARIFF_SET.YOKO_TARIFF_CD        = YOKO_TARIFF_HD.YOKO_TARIFF_CD   " & vbNewLine _
                                          & "       AND YOKO_TARIFF_HD.SYS_DEL_FLG              = '0'                             " & vbNewLine


    Private Const SQL_FROM_DATA2 As String = "FROM                                                           " & vbNewLine _
                                          & "                      $LM_MST$..M_DEST_DETAILS AS DEST_DETAILS  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST AS DEST                  " & vbNewLine _
                                          & "       ON DEST_DETAILS.NRS_BR_CD         = DEST.NRS_BR_CD       " & vbNewLine _
                                          & "       AND DEST_DETAILS.CUST_CD_L        = DEST.CUST_CD_L       " & vbNewLine _
                                          & "       AND DEST_DETAILS.DEST_CD          = DEST.DEST_CD         " & vbNewLine _
                                          & "       AND DEST.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1               " & vbNewLine _
                                          & "        ON DEST_DETAILS.SUB_KB           = KBN1.KBN_CD          " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD             = 'Y006'               " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG              = '0'                  " & vbNewLine



    Private Const SQL_FROM_MAX As String = "FROM                                                                " & vbNewLine _
                                          & "                       $LM_MST$..M_DEST_DETAILS  DEST_DETAILS      " & vbNewLine

    Private Const SQL_FROM_TARIFF_SET_MAX As String = "FROM                                                                " & vbNewLine _
                                          & "                       $LM_MST$..M_UNCHIN_TARIFF_SET  UNCHIN_TARIFF_SET       " & vbNewLine

    Private Const SQL_FROM_ZIP_JIS As String = "FROM                                                            " & vbNewLine _
                                          & "                       $LM_MST$..M_ZIP  ZIP                        " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY(DEST_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     DEST.NRS_BR_CD                                    " & vbNewLine _
                                         & "    ,DEST.DEST_CD                                      " & vbNewLine _
                                         & "    ,DEST.CUST_CD_L                                    " & vbNewLine

    ''' <summary>
    ''' ORDER BY(DEST_DETAILS_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY2 As String = "ORDER BY                                              " & vbNewLine _
                                         & "     DEST_DETAILS.DEST_CD_EDA                          " & vbNewLine _
                                         & "    ,DEST_DETAILS.SUB_KB                               " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 届先マスタ存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_DEST As String = "SELECT                              " & vbNewLine _
                                            & "   COUNT(DEST_CD)  AS REC_CNT     " & vbNewLine _
                                            & "FROM $LM_MST$..M_DEST             " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                            & "  AND CUST_CD_L    = @CUST_CD_L   " & vbNewLine _
                                            & "  AND DEST_CD      = @DEST_CD     " & vbNewLine

    ''' <summary>
    ''' 運賃タリフセットマスタ存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_UNCHIN_TARIFF_SET As String = "SELECT                   " & vbNewLine _
                                            & "   COUNT(CUST_CD_L)  AS REC_CNT     " & vbNewLine _
                                            & "FROM $LM_MST$..M_UNCHIN_TARIFF_SET  " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD     " & vbNewLine _
                                            & "  AND CUST_CD_L    = @CUST_CD_L     " & vbNewLine _
                                            & "  AND CUST_CD_M    = @CUST_CD_M     " & vbNewLine _
                                            & "  AND SET_MST_CD   = @SET_MST_CD    " & vbNewLine

    ''' <summary>
    ''' 郵便番号存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_ZIP As String = "SELECT                      " & vbNewLine _
                                         & "   COUNT(ZIP_NO)  AS REC_CNT" & vbNewLine _
                                         & "FROM $LM_MST$..M_ZIP        " & vbNewLine _
                                         & "WHERE ZIP_NO  = @ZIP        " & vbNewLine

    ''' <summary>
    ''' 郵便番号/JISコード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_ZIP_JIS As String = "SELECT                  " & vbNewLine _
                                         & "   COUNT(ZIP_NO)  AS REC_CNT" & vbNewLine _
                                         & "FROM $LM_MST$..M_ZIP        " & vbNewLine _
                                         & "WHERE ZIP_NO    = @ZIP      " & vbNewLine _
                                         & "  AND JIS_CD    = @JIS_CD   " & vbNewLine _
                                         & "  AND SYS_DEL_FLG   = '0'   " & vbNewLine

    ''' <summary>
    ''' 一括登録時の郵便番号/JISチェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_IMPORT_CHK As String = "" _
                                         & "SELECT                          " & vbNewLine _
                                         & "   ZIP.ZIP_NO AS ZIP            " & vbNewLine _
                                         & "  ,ZIP.JIS_CD AS ZIP_JIS        " & vbNewLine _
                                         & "  ,JIS.JIS_CD AS JIS            " & vbNewLine _
                                         & "FROM                            " & vbNewLine _
                                         & "  $LM_MST$..M_ZIP AS ZIP        " & vbNewLine _
                                         & "LEFT JOIN                       " & vbNewLine _
                                         & "  $LM_MST$..M_JIS AS JIS        " & vbNewLine _
                                         & "  ON  JIS.JIS_CD = ZIP.JIS_CD   " & vbNewLine _
                                         & "  AND JIS.SYS_DEL_FLG = '0'     " & vbNewLine _
                                         & "WHERE                           " & vbNewLine _
                                         & "  ZIP.ZIP_NO = @ZIP             " & vbNewLine _
                                         & "  AND ZIP.SYS_DEL_FLG = '0'     " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

#Region "INSERT"

    'START YANAI 要望番号881
    '''' <summary>
    '''' 新規登録SQL（M_DEST）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_DEST        " & vbNewLine _
    '                                   & "(                                   " & vbNewLine _
    '                                   & "      NRS_BR_CD                     " & vbNewLine _
    '                                   & "      ,CUST_CD_L                    " & vbNewLine _
    '                                   & "      ,DEST_CD                      " & vbNewLine _
    '                                   & "      ,EDI_CD                       " & vbNewLine _
    '                                   & "      ,DEST_NM                      " & vbNewLine _
    '                                   & "      ,ZIP                          " & vbNewLine _
    '                                   & "      ,AD_1                         " & vbNewLine _
    '                                   & "      ,AD_2                         " & vbNewLine _
    '                                   & "      ,AD_3                         " & vbNewLine _
    '                                   & "      ,CUST_DEST_CD                 " & vbNewLine _
    '                                   & "      ,SALES_CD                     " & vbNewLine _
    '                                   & "      ,SP_NHS_KB                    " & vbNewLine _
    '                                   & "      ,COA_YN                       " & vbNewLine _
    '                                   & "      ,SP_UNSO_CD                   " & vbNewLine _
    '                                   & "      ,SP_UNSO_BR_CD                " & vbNewLine _
    '                                   & "      ,DELI_ATT                     " & vbNewLine _
    '                                   & "      ,CARGO_TIME_LIMIT             " & vbNewLine _
    '                                   & "      ,LARGE_CAR_YN                 " & vbNewLine _
    '                                   & "      ,TEL                          " & vbNewLine _
    '                                   & "      ,FAX                          " & vbNewLine _
    '                                   & "      ,UNCHIN_SEIQTO_CD             " & vbNewLine _
    '                                   & "      ,JIS                          " & vbNewLine _
    '                                   & "      ,KYORI                        " & vbNewLine _
    '                                   & "      ,PICK_KB                      " & vbNewLine _
    '                                   & "      ,BIN_KB                       " & vbNewLine _
    '                                   & "      ,MOTO_CHAKU_KB                " & vbNewLine _
    '                                   & "      ,URIAGE_CD                    " & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE                 " & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME                 " & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID                 " & vbNewLine _
    '                                   & "      ,SYS_ENT_USER                 " & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE                 " & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME                 " & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID                 " & vbNewLine _
    '                                   & "      ,SYS_UPD_USER                 " & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG                  " & vbNewLine _
    '                                   & "      ) VALUES (                    " & vbNewLine _
    '                                   & "      @NRS_BR_CD                    " & vbNewLine _
    '                                   & "      ,@CUST_CD_L                   " & vbNewLine _
    '                                   & "      ,@DEST_CD                     " & vbNewLine _
    '                                   & "      ,@EDI_CD                      " & vbNewLine _
    '                                   & "      ,@DEST_NM                     " & vbNewLine _
    '                                   & "      ,@ZIP                         " & vbNewLine _
    '                                   & "      ,@AD_1                        " & vbNewLine _
    '                                   & "      ,@AD_2                        " & vbNewLine _
    '                                   & "      ,@AD_3                        " & vbNewLine _
    '                                   & "      ,@CUST_DEST_CD                " & vbNewLine _
    '                                   & "      ,@SALES_CD                    " & vbNewLine _
    '                                   & "      ,@SP_NHS_KB                   " & vbNewLine _
    '                                   & "      ,@COA_YN                      " & vbNewLine _
    '                                   & "      ,@SP_UNSO_CD                  " & vbNewLine _
    '                                   & "      ,@SP_UNSO_BR_CD               " & vbNewLine _
    '                                   & "      ,@DELI_ATT                    " & vbNewLine _
    '                                   & "      ,@CARGO_TIME_LIMIT            " & vbNewLine _
    '                                   & "      ,@LARGE_CAR_YN                " & vbNewLine _
    '                                   & "      ,@TEL                         " & vbNewLine _
    '                                   & "      ,@FAX                         " & vbNewLine _
    '                                   & "      ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
    '                                   & "      ,@JIS                         " & vbNewLine _
    '                                   & "      ,@KYORI                       " & vbNewLine _
    '                                   & "      ,@PICK_KB                     " & vbNewLine _
    '                                   & "      ,@BIN_KB                      " & vbNewLine _
    '                                   & "      ,@MOTO_CHAKU_KB               " & vbNewLine _
    '                                   & "      ,@URIAGE_CD                   " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE                " & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME                " & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID                " & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER                " & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE                " & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME                " & vbNewLine _
    '                                   & "      ,@SYS_UPD_PGID                " & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER                " & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG                 " & vbNewLine _
    '                                   & ")                                   " & vbNewLine
    '要望番号:1330 terakawa 2012.08.09 KANA_NM追加 Start
    ''' <summary>
    ''' 新規登録SQL（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_DEST        " & vbNewLine _
                                       & "(                                   " & vbNewLine _
                                       & "      NRS_BR_CD                     " & vbNewLine _
                                       & "      ,CUST_CD_L                    " & vbNewLine _
                                       & "      ,DEST_CD                      " & vbNewLine _
                                       & "      ,EDI_CD                       " & vbNewLine _
                                       & "      ,DEST_NM                      " & vbNewLine _
                                       & "      ,KANA_NM                      " & vbNewLine _
                                       & "      ,ZIP                          " & vbNewLine _
                                       & "      ,AD_1                         " & vbNewLine _
                                       & "      ,AD_2                         " & vbNewLine _
                                       & "      ,AD_3                         " & vbNewLine _
                                       & "      ,CUST_DEST_CD                 " & vbNewLine _
                                       & "      ,SALES_CD                     " & vbNewLine _
                                       & "      ,SP_NHS_KB                    " & vbNewLine _
                                       & "      ,COA_YN                       " & vbNewLine _
                                       & "      ,SP_UNSO_CD                   " & vbNewLine _
                                       & "      ,SP_UNSO_BR_CD                " & vbNewLine _
                                       & "      ,DELI_ATT                     " & vbNewLine _
                                       & "      ,CARGO_TIME_LIMIT             " & vbNewLine _
                                       & "      ,LARGE_CAR_YN                 " & vbNewLine _
                                       & "      ,TEL                          " & vbNewLine _
                                       & "      ,FAX                          " & vbNewLine _
                                       & "      ,UNCHIN_SEIQTO_CD             " & vbNewLine _
                                       & "      ,JIS                          " & vbNewLine _
                                       & "      ,KYORI                        " & vbNewLine _
                                       & "      ,PICK_KB                      " & vbNewLine _
                                       & "      ,BIN_KB                       " & vbNewLine _
                                       & "      ,MOTO_CHAKU_KB                " & vbNewLine _
                                       & "      ,URIAGE_CD                    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                 " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                 " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                 " & vbNewLine _
                                       & "      ,SYS_ENT_USER                 " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                 " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                 " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                 " & vbNewLine _
                                       & "      ,SYS_UPD_USER                 " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                  " & vbNewLine _
                                       & "      ,REMARK                       " & vbNewLine _
                                       & "      ,SHIHARAI_AD                  " & vbNewLine _
                                       & "      ) VALUES (                    " & vbNewLine _
                                       & "      @NRS_BR_CD                    " & vbNewLine _
                                       & "      ,@CUST_CD_L                   " & vbNewLine _
                                       & "      ,@DEST_CD                     " & vbNewLine _
                                       & "      ,@EDI_CD                      " & vbNewLine _
                                       & "      ,@DEST_NM                     " & vbNewLine _
                                       & "      ,@KANA_NM                     " & vbNewLine _
                                       & "      ,@ZIP                         " & vbNewLine _
                                       & "      ,@AD_1                        " & vbNewLine _
                                       & "      ,@AD_2                        " & vbNewLine _
                                       & "      ,@AD_3                        " & vbNewLine _
                                       & "      ,@CUST_DEST_CD                " & vbNewLine _
                                       & "      ,@SALES_CD                    " & vbNewLine _
                                       & "      ,@SP_NHS_KB                   " & vbNewLine _
                                       & "      ,@COA_YN                      " & vbNewLine _
                                       & "      ,@SP_UNSO_CD                  " & vbNewLine _
                                       & "      ,@SP_UNSO_BR_CD               " & vbNewLine _
                                       & "      ,@DELI_ATT                    " & vbNewLine _
                                       & "      ,@CARGO_TIME_LIMIT            " & vbNewLine _
                                       & "      ,@LARGE_CAR_YN                " & vbNewLine _
                                       & "      ,@TEL                         " & vbNewLine _
                                       & "      ,@FAX                         " & vbNewLine _
                                       & "      ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
                                       & "      ,@JIS                         " & vbNewLine _
                                       & "      ,@KYORI                       " & vbNewLine _
                                       & "      ,@PICK_KB                     " & vbNewLine _
                                       & "      ,@BIN_KB                      " & vbNewLine _
                                       & "      ,@MOTO_CHAKU_KB               " & vbNewLine _
                                       & "      ,@URIAGE_CD                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                 " & vbNewLine _
                                       & "      ,@REMARK                      " & vbNewLine _
                                       & "      ,@SHIHARAI_AD                 " & vbNewLine _
                                       & ")                                   " & vbNewLine
    '要望番号:1330 terakawa 2012.08.09 KANA_NM追加 End
    'END YANAI 要望番号881

    ''' <summary>
    ''' 新規登録SQL（M_UNCHIN_TARIFF_SET）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_UNCHIN_TARIFF_SET As String = "INSERT INTO $LM_MST$..M_UNCHIN_TARIFF_SET        " & vbNewLine _
                                   & "(                                                                      " & vbNewLine _
                                   & "      NRS_BR_CD                                                        " & vbNewLine _
                                   & "      ,CUST_CD_L                                                       " & vbNewLine _
                                   & "      ,CUST_CD_M                                                       " & vbNewLine _
                                   & "      ,SET_MST_CD                                                      " & vbNewLine _
                                   & "      ,DEST_CD                                                         " & vbNewLine _
                                   & "      ,SET_KB                                                          " & vbNewLine _
                                   & "      ,TARIFF_BUNRUI_KB                                                " & vbNewLine _
                                   & "      ,UNCHIN_TARIFF_CD1                                               " & vbNewLine _
                                   & "      ,UNCHIN_TARIFF_CD2                                               " & vbNewLine _
                                   & "      ,EXTC_TARIFF_CD                                                  " & vbNewLine _
                                   & "      ,YOKO_TARIFF_CD                                                  " & vbNewLine _
                                   & "      ,SYS_ENT_DATE                                                    " & vbNewLine _
                                   & "      ,SYS_ENT_TIME                                                    " & vbNewLine _
                                   & "      ,SYS_ENT_PGID                                                    " & vbNewLine _
                                   & "      ,SYS_ENT_USER                                                    " & vbNewLine _
                                   & "      ,SYS_UPD_DATE                                                    " & vbNewLine _
                                   & "      ,SYS_UPD_TIME                                                    " & vbNewLine _
                                   & "      ,SYS_UPD_PGID                                                    " & vbNewLine _
                                   & "      ,SYS_UPD_USER                                                    " & vbNewLine _
                                   & "      ,SYS_DEL_FLG                                                     " & vbNewLine _
                                   & "      ) VALUES (                                                       " & vbNewLine _
                                   & "      @NRS_BR_CD                                                       " & vbNewLine _
                                   & "      ,@CUST_CD_L                                                      " & vbNewLine _
                                   & "      ,@CUST_CD_M                                                      " & vbNewLine _
                                   & "      ,@SET_MST_CD                                                     " & vbNewLine _
                                   & "      ,@DEST_CD                                                        " & vbNewLine _
                                   & "      ,@SET_KB                                                         " & vbNewLine _
                                   & "      ,@TARIFF_BUNRUI_KB                                               " & vbNewLine _
                                   & "      ,@UNCHIN_TARIFF_CD1                                              " & vbNewLine _
                                   & "      ,@UNCHIN_TARIFF_CD2                                              " & vbNewLine _
                                   & "      ,@EXTC_TARIFF_CD                                                 " & vbNewLine _
                                   & "      ,@YOKO_TARIFF_CD                                                 " & vbNewLine _
                                   & "      ,@SYS_ENT_DATE                                                   " & vbNewLine _
                                   & "      ,@SYS_ENT_TIME                                                   " & vbNewLine _
                                   & "      ,@SYS_ENT_PGID                                                   " & vbNewLine _
                                   & "      ,@SYS_ENT_USER                                                   " & vbNewLine _
                                   & "      ,@SYS_UPD_DATE                                                   " & vbNewLine _
                                   & "      ,@SYS_UPD_TIME                                                   " & vbNewLine _
                                   & "      ,@SYS_UPD_PGID                                                   " & vbNewLine _
                                   & "      ,@SYS_UPD_USER                                                   " & vbNewLine _
                                   & "      ,@SYS_DEL_FLG                                                    " & vbNewLine _
                                   & ")                                                                      " & vbNewLine

    ''' <summary>
    ''' 新規登録SQL（M_DEST_DETAILS）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DEST_DETAILS As String = "INSERT INTO $LM_MST$..M_DEST_DETAILS     " & vbNewLine _
                                       & "(                                              " & vbNewLine _
                                       & "      NRS_BR_CD                                " & vbNewLine _
                                       & "      ,CUST_CD_L                               " & vbNewLine _
                                       & "      ,DEST_CD                                 " & vbNewLine _
                                       & "      ,DEST_CD_EDA                             " & vbNewLine _
                                       & "      ,SUB_KB                                  " & vbNewLine _
                                       & "      ,SET_NAIYO                               " & vbNewLine _
                                       & "      ,REMARK                                  " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                            " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                            " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                            " & vbNewLine _
                                       & "      ,SYS_ENT_USER                            " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                            " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                            " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                            " & vbNewLine _
                                       & "      ,SYS_UPD_USER                            " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                             " & vbNewLine _
                                       & "      ) VALUES (                               " & vbNewLine _
                                       & "      @NRS_BR_CD                               " & vbNewLine _
                                       & "      ,@CUST_CD_L                              " & vbNewLine _
                                       & "      ,@DEST_CD                                " & vbNewLine _
                                       & "      ,@DEST_CD_EDA                            " & vbNewLine _
                                       & "      ,@SUB_KB                                 " & vbNewLine _
                                       & "      ,@SET_NAIYO                              " & vbNewLine _
                                       & "      ,@REMARK                                 " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                           " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                           " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                           " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                           " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                           " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                           " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                           " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                           " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                            " & vbNewLine _
                                       & ")                                              " & vbNewLine

#End Region

#Region "Delete"

    ''' <summary>
    ''' 物理削除SQL（M_DEST_DETAILS）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_DEST_DETAILS As String = "DELETE FROM $LM_MST$..M_DEST_DETAILS               " & vbNewLine _
                                              & "WHERE   NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                              & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                              & "    AND DEST_CD               = @DEST_CD              " & vbNewLine

    ''' <summary>
    ''' 物理削除SQL（M_UNCHIN_TARIFF_SET）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_UNCHIN_TARIFF_SET As String = "DELETE FROM $LM_MST$..M_UNCHIN_TARIFF_SET     " & vbNewLine _
                                              & "WHERE   NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                              & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                              & "    AND CUST_CD_M             = @CUST_CD_M            " & vbNewLine _
                                              & "    AND SET_MST_CD            = @SET_MST_CD           " & vbNewLine

#End Region

#Region "UPDATE"

    'START YANAI 要望番号881
    '''' <summary>
    '''' 更新SQL（M_DEST）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_DEST SET                           " & vbNewLine _
    '                                   & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
    '                                   & "       ,CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
    '                                   & "       ,DEST_CD               = @DEST_CD              " & vbNewLine _
    '                                   & "       ,EDI_CD                = @EDI_CD               " & vbNewLine _
    '                                   & "       ,DEST_NM               = @DEST_NM              " & vbNewLine _
    '                                   & "       ,ZIP                   = @ZIP                  " & vbNewLine _
    '                                   & "       ,AD_1                  = @AD_1                 " & vbNewLine _
    '                                   & "       ,AD_2                  = @AD_2                 " & vbNewLine _
    '                                   & "       ,AD_3                  = @AD_3                 " & vbNewLine _
    '                                   & "       ,CUST_DEST_CD          = @CUST_DEST_CD         " & vbNewLine _
    '                                   & "       ,SALES_CD              = @SALES_CD             " & vbNewLine _
    '                                   & "       ,SP_NHS_KB             = @SP_NHS_KB            " & vbNewLine _
    '                                   & "       ,COA_YN                = @COA_YN               " & vbNewLine _
    '                                   & "       ,SP_UNSO_CD            = @SP_UNSO_CD           " & vbNewLine _
    '                                   & "       ,SP_UNSO_BR_CD         = @SP_UNSO_BR_CD        " & vbNewLine _
    '                                   & "       ,DELI_ATT              = @DELI_ATT             " & vbNewLine _
    '                                   & "       ,CARGO_TIME_LIMIT      = @CARGO_TIME_LIMIT     " & vbNewLine _
    '                                   & "       ,LARGE_CAR_YN          = @LARGE_CAR_YN         " & vbNewLine _
    '                                   & "       ,TEL                   = @TEL                  " & vbNewLine _
    '                                   & "       ,FAX                   = @FAX                  " & vbNewLine _
    '                                   & "       ,UNCHIN_SEIQTO_CD      = @UNCHIN_SEIQTO_CD     " & vbNewLine _
    '                                   & "       ,JIS                   = @JIS                  " & vbNewLine _
    '                                   & "       ,KYORI                 = @KYORI                " & vbNewLine _
    '                                   & "       ,PICK_KB               = @PICK_KB              " & vbNewLine _
    '                                   & "       ,BIN_KB                = @BIN_KB               " & vbNewLine _
    '                                   & "       ,MOTO_CHAKU_KB         = @MOTO_CHAKU_KB        " & vbNewLine _
    '                                   & "       ,URIAGE_CD             = @URIAGE_CD            " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
    '                                   & " WHERE                                                " & vbNewLine _
    '                                   & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
    '                                   & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
    '                                   & "    AND DEST_CD               = @DEST_CD              " & vbNewLine

    '要望番号:1330 terakawa 2012.08.09 KANA_NM追加 Start
    ''' <summary>
    ''' 更新SQL（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_DEST SET                           " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                       & "       ,CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                       & "       ,DEST_CD               = @DEST_CD              " & vbNewLine _
                                       & "       ,EDI_CD                = @EDI_CD               " & vbNewLine _
                                       & "       ,DEST_NM               = @DEST_NM              " & vbNewLine _
                                       & "       ,KANA_NM               = @KANA_NM              " & vbNewLine _
                                       & "       ,ZIP                   = @ZIP                  " & vbNewLine _
                                       & "       ,AD_1                  = @AD_1                 " & vbNewLine _
                                       & "       ,AD_2                  = @AD_2                 " & vbNewLine _
                                       & "       ,AD_3                  = @AD_3                 " & vbNewLine _
                                       & "       ,CUST_DEST_CD          = @CUST_DEST_CD         " & vbNewLine _
                                       & "       ,SALES_CD              = @SALES_CD             " & vbNewLine _
                                       & "       ,SP_NHS_KB             = @SP_NHS_KB            " & vbNewLine _
                                       & "       ,COA_YN                = @COA_YN               " & vbNewLine _
                                       & "       ,SP_UNSO_CD            = @SP_UNSO_CD           " & vbNewLine _
                                       & "       ,SP_UNSO_BR_CD         = @SP_UNSO_BR_CD        " & vbNewLine _
                                       & "       ,DELI_ATT              = @DELI_ATT             " & vbNewLine _
                                       & "       ,CARGO_TIME_LIMIT      = @CARGO_TIME_LIMIT     " & vbNewLine _
                                       & "       ,LARGE_CAR_YN          = @LARGE_CAR_YN         " & vbNewLine _
                                       & "       ,TEL                   = @TEL                  " & vbNewLine _
                                       & "       ,FAX                   = @FAX                  " & vbNewLine _
                                       & "       ,UNCHIN_SEIQTO_CD      = @UNCHIN_SEIQTO_CD     " & vbNewLine _
                                       & "       ,JIS                   = @JIS                  " & vbNewLine _
                                       & "       ,KYORI                 = @KYORI                " & vbNewLine _
                                       & "       ,PICK_KB               = @PICK_KB              " & vbNewLine _
                                       & "       ,BIN_KB                = @BIN_KB               " & vbNewLine _
                                       & "       ,MOTO_CHAKU_KB         = @MOTO_CHAKU_KB        " & vbNewLine _
                                       & "       ,URIAGE_CD             = @URIAGE_CD            " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,REMARK                = @REMARK               " & vbNewLine _
                                       & "       ,SHIHARAI_AD           = @SHIHARAI_AD          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                       & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                       & "    AND DEST_CD               = @DEST_CD              " & vbNewLine _
                                       & "    AND SYS_UPD_DATE          = @SYS_UPD_DATE_OLD  --ADD 2018/09/11 排他制御            " & vbNewLine _
                                       & "    AND SYS_UPD_TIME          = @SYS_UPD_TIME_OLD  --ADD 2018/09/11 排他制御            " & vbNewLine
    '要望番号:1330 terakawa 2012.08.09 KANA_NM追加 End
    'END YANAI 要望番号881

    ''' <summary>
    ''' 更新SQL（M_UNCHIN_TARIFF_SET）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_UNCHIN_TARIFF_SET As String = "UPDATE $LM_MST$..M_UNCHIN_TARIFF_SET SET           " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD                           " & vbNewLine _
                                       & "       ,CUST_CD_L             = @CUST_CD_L                           " & vbNewLine _
                                       & "       ,CUST_CD_M             = @CUST_CD_M                           " & vbNewLine _
                                       & "       ,SET_MST_CD            = @SET_MST_CD                          " & vbNewLine _
                                       & "       ,DEST_CD               = @DEST_CD                             " & vbNewLine _
                                       & "       ,SET_KB                = @SET_KB                              " & vbNewLine _
                                       & "       ,TARIFF_BUNRUI_KB      = @TARIFF_BUNRUI_KB                    " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_CD1     = @UNCHIN_TARIFF_CD1                   " & vbNewLine _
                                       & "       ,UNCHIN_TARIFF_CD2     = @UNCHIN_TARIFF_CD2                   " & vbNewLine _
                                       & "       ,EXTC_TARIFF_CD        = @EXTC_TARIFF_CD                      " & vbNewLine _
                                       & "       ,YOKO_TARIFF_CD        = @YOKO_TARIFF_CD                      " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE                        " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME                        " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID                        " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER                        " & vbNewLine _
                                       & " WHERE                                                               " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD                           " & vbNewLine _
                                       & "    AND CUST_CD_L             = @CUST_CD_L                           " & vbNewLine _
                                       & "    AND CUST_CD_M             = @CUST_CD_M                           " & vbNewLine _
                                       & "    AND SET_MST_CD            = @SET_MST_CD                          " & vbNewLine _
                                       & "    AND SYS_UPD_DATE          = @SYS_UPD_DATE_OLD  --ADD 2022/08/23 排他制御 03185            " & vbNewLine _
                                       & "    AND SYS_UPD_TIME          = @SYS_UPD_TIME_OLD  --ADD 2022/08/23 排他制御 031850           " & vbNewLine


    ''' <summary>
    ''' 更新SQL（M_DEST） 一括登録用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_IMPORT As String = "UPDATE $LM_MST$..M_DEST SET                    " & vbNewLine _
                                       & "        DEST_NM               = @DEST_NM              " & vbNewLine _
                                       & "       ,ZIP                   = @ZIP                  " & vbNewLine _
                                       & "       ,AD_1                  = @AD_1                 " & vbNewLine _
                                       & "       ,AD_2                  = @AD_2                 " & vbNewLine _
                                       & "       ,AD_3                  = @AD_3                 " & vbNewLine _
                                       & "       ,TEL                   = @TEL                  " & vbNewLine _
                                       & "       ,JIS                   = @JIS                  " & vbNewLine _
                                       & "       ,SHIHARAI_AD           = @SHIHARAI_AD          " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                       & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                       & "    AND DEST_CD               = @DEST_CD              " & vbNewLine

#End Region

#Region "UPDATE_DEL_FLG"

    ''' <summary>
    ''' 削除・復活SQL（M_DEST）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_DEST SET                           " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                       & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                       & "    AND DEST_CD               = @DEST_CD              " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_DEST_DETAILS）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_DEST_DETAILS As String = "UPDATE $LM_MST$..M_DEST_DETAILS SET                   " & vbNewLine _
                                                    & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                                    & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                                    & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                                    & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                                    & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                                    & " WHERE                                                " & vbNewLine _
                                                    & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                                    & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                                    & "    AND DEST_CD               = @DEST_CD              " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL（M_UNCHIN_TARIFF_SET）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UNCHIN_TARIFF_SET As String = "UPDATE $LM_MST$..M_UNCHIN_TARIFF_SET SET         " & vbNewLine _
                                                    & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                                    & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                                    & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                                    & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                                    & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                                    & " WHERE                                                " & vbNewLine _
                                                    & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                                    & "    AND CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                                    & "    AND CUST_CD_M             = @CUST_CD_M            " & vbNewLine _
                                                    & "    AND DEST_CD               = @DEST_CD              " & vbNewLine _
                                                    & "    AND SET_KB                = @SET_KB               " & vbNewLine


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
    ''' サーバ日付
    ''' </summary>
    ''' <remarks></remarks>
    Private _ServerDate As String = String.Empty

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 届先マスタ・運賃タリフセットマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ・運賃タリフセットマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタデータ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectUnchinTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_TARIFF_SET_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_TARIFF_SET_MAX)         'SQL構築(カウント用from句)
        Call Me.SetConditionTariffSetMasterSQL()                     '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectUnchinTariffSetM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ・運賃タリフセットマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ・運賃タリフセットマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM040DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("DEST_NM", "DEST_NM")
        '要望番号:1330 terakawa 2012.08.09 Start
        map.Add("KANA_NM", "KANA_NM")
        '要望番号:1330 terakawa 2012.08.09 End
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CUST_DEST_CD", "CUST_DEST_CD")
        map.Add("TEL", "TEL")
        map.Add("JIS", "JIS")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("SP_NHS_KB_NM", "SP_NHS_KB_NM")
        map.Add("FAX", "FAX")
        map.Add("KYORI", "KYORI")
        map.Add("COA_YN", "COA_YN")
        map.Add("COA_YN_NM", "COA_YN_NM")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("SP_UNSO_NM", "SP_UNSO_NM")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("PICK_KB_NM", "PICK_KB_NM")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_KB_NM", "BIN_KB_NM")
        map.Add("MOTO_CHAKU_KB", "MOTO_CHAKU_KB")
        map.Add("MOTO_CHAKU_KB_NM", "MOTO_CHAKU_KB_NM")
        map.Add("CARGO_TIME_LIMIT", "CARGO_TIME_LIMIT")
        map.Add("LARGE_CAR_YN", "LARGE_CAR_YN")
        map.Add("LARGE_CAR_YN_NM", "LARGE_CAR_YN_NM")
        map.Add("DELI_ATT", "DELI_ATT")
        map.Add("SALES_CD", "SALES_CD")
        map.Add("SALES_NM", "SALES_NM")
        map.Add("URIAGE_CD", "URIAGE_CD")
        map.Add("URIAGE_NM", "URIAGE_NM")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("UNCHIN_SEIQTO_NM", "UNCHIN_SEIQTO_NM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("TARIFF_BUNRUI_KB_NM", "TARIFF_BUNRUI_KB_NM")
        map.Add("UNCHIN_TARIFF_CD1", "UNCHIN_TARIFF_CD1")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_NM", "EXTC_TARIFF_NM")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_TARIFF_NM", "YOKO_TARIFF_NM")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("SET_MST_CD", "SET_MST_CD")
        map.Add("SET_KB", "SET_KB")
        map.Add("SYS_UPD_DATE_T", "SYS_UPD_DATE_T")
        map.Add("SYS_UPD_TIME_T", "SYS_UPD_TIME_T")
        map.Add("UNCHIN_TARIFF_NM1", "UNCHIN_TARIFF_NM1")
        map.Add("UNCHIN_TARIFF_NM2", "UNCHIN_TARIFF_NM2")
        'START YANAI 要望番号881
        map.Add("REMARK", "REMARK")
        'END YANAI 要望番号881
        '要望番号:1424② yamanaka 2012.09.21 Start
        map.Add("SHIHARAI_AD", "SHIHARAI_AD")
        '要望番号:1424② yamanaka 2012.09.21 End

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM040OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_UNCHIN_TARIFF_SET(運賃タリフセットM存在チェック用))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionTariffSetMasterSQL()

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
                andstr.Append(" (UNCHIN_TARIFF_SET.NRS_BR_CD = @NRS_BR_CD OR UNCHIN_TARIFF_SET.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 -- START --

                andstr.Append(" UNCHIN_TARIFF_SET.CUST_CD_L = @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
                'andstr.Append(" UNCHIN_TARIFF_SET.CUST_CD_L LIKE @CUST_CD_L")
                'andstr.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 --  END  --

            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 -- START --

                andstr.Append(" UNCHIN_TARIFF_SET.CUST_CD_M = @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                'andstr.Append(" UNCHIN_TARIFF_SET.CUST_CD_M LIKE @CUST_CD_M")
                'andstr.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 --  END  --

            End If

            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 -- START --

                andstr.Append(" UNCHIN_TARIFF_SET.DEST_CD = @DEST_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", whereStr, DBDataType.NVARCHAR))
                'andstr.Append(" UNCHIN_TARIFF_SET.DEST_CD LIKE @DEST_CD")
                'andstr.Append(vbNewLine)
                ''START YANAI 要望番号1317 届け先M：届け先CD・電話番号の検索条件を中間一致にする
                ''Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                ''END YANAI 要望番号1317 届け先M：届け先CD・電話番号の検索条件を中間一致にする

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 --  END  --

            End If

            whereStr = .Item("SET_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 -- START --

                andstr.Append(" UNCHIN_TARIFF_SET.SET_KB = @SET_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KB", whereStr, DBDataType.CHAR))
                'andstr.Append(" UNCHIN_TARIFF_SET.SET_KB LIKE @SET_KB")
                'andstr.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KB", String.Concat(whereStr, "%"), DBDataType.CHAR))

                '(2012.12.26)要望番号1707 LIKE条件→=条件に変更 --  END  --

            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_DEST)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        _ServerDate = String.Concat(MyBase.GetSystemDate)

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", _ServerDate, DBDataType.CHAR))

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (DEST.SYS_DEL_FLG = @SYS_DEL_FLG  OR DEST.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (DEST.NRS_BR_CD = @NRS_BR_CD OR DEST.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_CD LIKE @DEST_CD")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号1317 届け先M：届け先CD・電話番号の検索条件を中間一致にする
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号1317 届け先M：届け先CD・電話番号の検索条件を中間一致にする
            End If

            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @DEST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("AD_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.AD_1 LIKE @AD_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("TEL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.TEL LIKE @TEL")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号1317 届け先M：届け先CD・電話番号の検索条件を中間一致にする
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                'END YANAI 要望番号1317 届け先M：届け先CD・電話番号の検索条件を中間一致にする
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 届先明細マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先明細マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_DATA2)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定
        Me._StrSql.Append(LMM040DAC.SQL_ORDER_BY2)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectListData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_CD_EDA", "DEST_CD_EDA")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SUB_KB_NM", "SUB_KB_NM")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("REMARK", "REMARK")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM040_DEST_DETAILS")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_DEST_DETAILS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

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
                andstr.Append(" (DEST_DETAILS.NRS_BR_CD = @NRS_BR_CD OR DEST_DETAILS.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST_DETAILS.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST_DETAILS.DEST_CD LIKE @DEST_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_UNCHIN_TARIFF_SET(MAXセットマスタコード取得用))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionTariffSetMaxMasterSQL()

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
                andstr.Append(" (UNCHIN_TARIFF_SET.NRS_BR_CD = @NRS_BR_CD OR UNCHIN_TARIFF_SET.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_SET.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNCHIN_TARIFF_SET.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(M_ZIP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQLZip()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("ZIP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (ZIP.ZIP_NO = @ZIP OR ZIP.ZIP_NO IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", whereStr, DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' MAX届先コード枝番取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>MAX届先コード枝番取得SQLの構築・発行</remarks>
    Private Function SelectMaxDestCdEdaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_MAX)        'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_MAX)          'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL2()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectMaxDestCdEdaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEST_MAXCD_EDA", "DEST_MAXCD_EDA")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM040_DEST_DETAILS_MAXEDA")

        Return ds

    End Function

    ''' <summary>
    ''' MAXセットマスタコード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>MAXセットマスタコード取得SQLの構築・発行</remarks>
    Private Function SelectMaxTariffSetMaxCdData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_TARIFF_SET_MAX)        'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_TARIFF_SET_MAX)          'SQL構築(データ抽出用from句)
        Call Me.SetConditionTariffSetMaxMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectMaxTariffSetMaxCdData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_MST_MAXCD", "SET_MST_MAXCD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM040_UNCHIN_TARIFF_SET_MAXCD")

        Return ds

    End Function

    ''' <summary>
    ''' JISコード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JISコード取得SQLの構築・発行</remarks>
    Private Function SelectZipJisData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM040DAC.SQL_SELECT_ZIP_JIS)        'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM040DAC.SQL_FROM_ZIP_JIS)          'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQLZip()                     '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectZipJisData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JIS", "JIS")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM040OUT")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 届先マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectDestM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM040DAC.SQL_EXIT_DEST)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk("SelectDestM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "SelectDestM", cmd)

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
    ''' 運賃タリフセットマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function HaitaUnchinTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM040DAC.SQL_EXIT_UNCHIN_TARIFF_SET)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk("HaitaUnchinTariffSetM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "HaitaUnchinTariffSetM", cmd)

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
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistDestM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_EXIT_DEST, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "CheckExistDestM", cmd)

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

    ''2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>郵便番号マスタ件数取得SQLの構築・発行</remarks>
    'Private Function CheckExistZipM(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMM040IN")

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_EXIT_ZIP, Me._Row.Item("USER_BR_CD").ToString()))

    '    Dim reader As SqlDataReader = Nothing

    '    'SQLパラメータ初期化/設定
    '    Call Me.SetParamZipExistChk()

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM040DAC", "CheckExistZipM", cmd)

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
    ''' 郵便番号マスタ存在チェック(郵便番号・JISコードで検索)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistZipJisM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_EXIT_ZIP_JIS, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamZipExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "CheckExistZipJisM", cmd)

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
    ''' 一括登録時の郵便番号/JISチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ImportChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dr As DataRow = ds.Tables("LMM040_IMPORT_CHK").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMM040DAC.SQL_IMPORT_CHK, dr.Item("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", dr.Item("ZIP").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "ImportChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ds.Tables("LMM040_IMPORT_CHK").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ZIP", "ZIP")
        map.Add("ZIP_JIS", "ZIP_JIS")
        map.Add("JIS", "JIS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM040_IMPORT_CHK")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMM040_IMPORT_CHK").Rows.Count())
        reader.Close()

        Return ds

    End Function

#Region "Insert"

    ''' <summary>
    ''' 届先マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertDestM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "InsertDestM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_INSERT_UNCHIN_TARIFF_SET, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "InsertUnchinTariffSetM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ物理削除SQLの構築・発行</remarks>
    Private Function DelUnchinTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_DEL_UNCHIN_TARIFF_SET, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete("DelUnchinTariffSetM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "DelUnchinTariffSetM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 届先明細マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先明細マスタ物理削除SQLの構築・発行</remarks>
    Private Function DelDestDetailsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_DEL_DEST_DETAILS, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete("DelDestDetailsM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "DelDestDetailsM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 届先明細マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先明細マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertDestDetailsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040_DEST_DETAILS")

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_INSERT_DEST_DETAILS, Me._Row.Item("USER_BR_CD").ToString()))
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化/設定
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetDestDetailsParam(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM040DAC", "InsertDestDetailsM", cmd)

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
    ''' 届先マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateDestM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_UPDATE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        '排他用追加 ADD 2018/09/11
        Call Me.SetComParamHaita()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "UpdateDestM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
#If fals Then   'UPD 2022/08/24 03101
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM040DAC.SQL_UPDATE_UNCHIN_TARIFF_SET _
                                                                                     , LMM040DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))
#Else
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM040DAC.SQL_UPDATE_UNCHIN_TARIFF_SET) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))
#End If


        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

#If True Then   'ADD 2022/08/23 031010 '排他用追加 運賃タリフセットマスタ
        Call Me.SetComParamHaitaSET()

#End If

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "UpdateUnchinTariffSetM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ更新（一括登録）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateDestMImport(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_UPDATE_IMPORT, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "UpdateDestMImport", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "Delete"

    ''' <summary>
    ''' 届先マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteDestM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete("DeleteDestM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "DeleteDestM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 届先明細マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先明細マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteDestDetailsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_DELETE_DEST_DETAILS, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete("DeleteDestDetailsM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "DeleteDestDetailsM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteUnchinTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM040IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM040DAC.SQL_DELETE_UNCHIN_TARIFF_SET, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete("DeleteUnchinTariffSetM")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM040DAC", "DeleteUnchinTariffSetM", cmd)

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
    ''' パラメータ設定モジュール(届先マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTariffExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_MST_CD", .Item("SET_MST_CD").ToString(), DBDataType.CHAR))
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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS_CD", .Item("JIS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk(ByVal methodNm As String)

        '①届先マスタの排他チェック
        If ("SelectDestM").Equals(methodNm) = True Then
            Call Me.SetParamExistChk()
            With Me._Row
                'パラメータ設定
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
            End With
            '②運賃タリフセットマスタの排他チェック
        ElseIf ("HaitaUnchinTariffSetM").Equals(methodNm) = True Then
            Call Me.SetParamTariffExistChk()
            With Me._Row
                'パラメータ設定
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE_T").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME_T").ToString(), DBDataType.CHAR))
            End With
        End If

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録_届先Ｍ)
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
    Private Sub SetParamDelete(ByVal methodNm As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目        
        '①運賃タリフセットマスタの物理削除
        If ("DelUnchinTariffSetM").Equals(methodNm) = True Then
            Call Me.SetParamCommonSystemTariffDel()
            '②届先明細マスタの物理削除
        ElseIf ("DelDestDetailsM").Equals(methodNm) = True Then
            Call Me.SetParamCommonSystemDestDetailsDel()
            '③届先マスタ/届先明細マスタの論理削除
        ElseIf ("DeleteDestM").Equals(methodNm) = True OrElse _
               ("DeleteDestDetailsM").Equals(methodNm) = True Then
            Call Me.SetParamCommonSystemDelete()
            Call Me.SetParamCommonSystemUpd()
            '④運賃タリフセットマスタの論理削除
        ElseIf ("DeleteUnchinTariffSetM").Equals(methodNm) = True Then
            Call Me.SetParamCommonSystemUnchinTariffDelete()
            Call Me.SetParamCommonSystemUpd()
        End If

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_届先Ｍ,運賃タリフセットＭ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CD", .Item("EDI_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            '要望番号:1330 terakawa 2012.08.09 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANA_NM", .Item("KANA_NM").ToString(), DBDataType.NVARCHAR))
            '要望番号:1330 terakawa 2012.08.09 End
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", .Item("AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_2", .Item("AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_DEST_CD", .Item("CUST_DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SALES_CD", .Item("SALES_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_UNSO_CD", .Item("SP_UNSO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_UNSO_BR_CD", .Item("SP_UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELI_ATT", .Item("DELI_ATT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CARGO_TIME_LIMIT", .Item("CARGO_TIME_LIMIT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LARGE_CAR_YN", .Item("LARGE_CAR_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", .Item("TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", .Item("FAX").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_SEIQTO_CD", .Item("UNCHIN_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))      '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS", .Item("JIS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI", If(String.IsNullOrEmpty(.Item("KYORI").ToString), "0", .Item("KYORI").ToString), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Item("PICK_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_CHAKU_KB", .Item("MOTO_CHAKU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@URIAGE_CD", .Item("URIAGE_CD").ToString(), DBDataType.NVARCHAR))

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_MST_CD", .Item("SET_MST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD1", .Item("UNCHIN_TARIFF_CD1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD2", .Item("UNCHIN_TARIFF_CD2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("YOKO_TARIFF_CD").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号881
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号881
            '要望番号:1424② yamanaka 2012.09.21 Start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_AD", .Item("SHIHARAI_AD").ToString(), DBDataType.NVARCHAR))
            '要望番号:1424② yamanaka 2012.09.21 End

        End With

    End Sub


#If True Then

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_届先Ｍ排他用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParamHaita()

        With Me._Row
            'パラメータ設定
            '排他用追加 Start ADD 2018/09/11
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_OLD", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_OLD", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub
#End If

#If True Then   'ADD 2022/08/23 031010

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_運賃タリフセットマスタ排他用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParamHaitaSET()

        With Me._Row
            'パラメータ設定
            '排他用追加 Start ADD 2018/09/11
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_OLD", .Item("SYS_UPD_DATE_T").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_OLD", .Item("SYS_UPD_TIME_T").ToString(), DBDataType.CHAR))

        End With

    End Sub
#End If
    ''' <summary>
    ''' パラメータ設定モジュール(更新登録_届先明細Ｍ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDestDetailsParam(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_EDA", .Item("DEST_CD_EDA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SUB_KB", .Item("SUB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SET_NAIYO", .Item("SET_NAIYO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
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
    ''' パラメータ設定モジュール(システム共通項目(届先明細Ｍ登録時))
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
    ''' パラメータ設定モジュール(システム共通項目(保存時_運賃タリフセットＭ物理削除))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemTariffDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_MST_CD", Me._Row.Item("SET_MST_CD").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(保存時_届先明細Ｍ物理削除))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDestDetailsDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時_論理削除))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDelete()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時_論理削除))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUnchinTariffDelete()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KB", Me._Row.Item("SET_KB").ToString(), DBDataType.CHAR))
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
