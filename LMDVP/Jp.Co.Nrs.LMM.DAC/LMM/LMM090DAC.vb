' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM090DAC : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM090DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM090DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "編集処理 SQL"

    ''' <summary>
    ''' 荷主マスタ排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CUSTM_HAITA_CHK As String = "SELECT                                                " & vbNewLine _
                                                & "       COUNT(CST.CUST_CD_L)     AS    SELECT_CNT      " & vbNewLine _
                                                & "FROM                                                  " & vbNewLine _
                                                & "     $LM_MST$..M_CUST    CST                          " & vbNewLine _
                                                & "WHERE                                                 " & vbNewLine _
                                                & "       CST.NRS_BR_CD          =    @NRS_BR_CD         " & vbNewLine _
                                                & "AND    CST.CUST_CD_L          =    @CUST_CD_L         " & vbNewLine _
                                                & "AND    CST.CUST_CD_M          =    @CUST_CD_M         " & vbNewLine _
                                                & "AND    CST.CUST_CD_S          =    @CUST_CD_S         " & vbNewLine _
                                                & "AND    CST.CUST_CD_SS         =    @CUST_CD_SS        " & vbNewLine _
                                                & "AND    CST.SYS_UPD_DATE       =    @HAITA_DATE        " & vbNewLine _
                                                & "AND    CST.SYS_UPD_TIME       =    @HAITA_TIME        " & vbNewLine

    'START YANAI 要望番号457
    '''' <summary>
    '''' 運賃タリフセットマスタ排他チェック処理(件数取得)用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_TARIFFM_HAITA_CHK As String = "SELECT                                                " & vbNewLine _
    '                                              & "       COUNT(TRS.CUST_CD_L)     AS    SELECT_CNT      " & vbNewLine _
    '                                              & "FROM                                                  " & vbNewLine _
    '                                              & "     $LM_MST$..M_UNCHIN_TARIFF_SET    TRS             " & vbNewLine _
    '                                              & "WHERE                                                 " & vbNewLine _
    '                                              & "       TRS.NRS_BR_CD          =    @NRS_BR_CD         " & vbNewLine _
    '                                              & "AND    TRS.CUST_CD_L          =    @CUST_CD_L         " & vbNewLine _
    '                                              & "AND    TRS.CUST_CD_M          =    @CUST_CD_M         " & vbNewLine _
    '                                              & "AND    TRS.SET_MST_CD         =    @SET_MST_CD        " & vbNewLine _
    '                                              & "AND    TRS.DEST_CD            =    ''                 " & vbNewLine
    ''' <summary>
    ''' 運賃タリフセットマスタ排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_TARIFFM_HAITA_CHK As String = "SELECT                                                " & vbNewLine _
                                                  & "       COUNT(TRS.CUST_CD_L)     AS    SELECT_CNT      " & vbNewLine _
                                                  & "FROM                                                  " & vbNewLine _
                                                  & "     $LM_MST$..M_UNCHIN_TARIFF_SET    TRS             " & vbNewLine _
                                                  & "WHERE                                                 " & vbNewLine _
                                                  & "       TRS.NRS_BR_CD          =    @NRS_BR_CD         " & vbNewLine _
                                                  & "AND    TRS.CUST_CD_L          =    @CUST_CD_L         " & vbNewLine _
                                                  & "AND    TRS.CUST_CD_M          =    @CUST_CD_M         " & vbNewLine _
                                                  & "AND    TRS.SET_MST_CD         =    @SET_MST_CD        " & vbNewLine _
    'END YANAI 要望番号457
    '& "AND    TRS.SYS_UPD_DATE       =    @HAITA_DATE        " & vbNewLine _
    '& "AND    TRS.SYS_UPD_TIME       =    @HAITA_TIME        " & vbNewLine

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_ZAIKO As String = "SELECT                                                  " & vbNewLine _
                                            & "    ISNULL(SUM(ZAI.PORA_ZAI_NB),0)   AS SELECT_CNT      " & vbNewLine _
                                            & "   ,count(*)                         AS REC_CNT         " & vbNewLine _
                                            & "FROM                                                    " & vbNewLine _
                                            & "    $LM_TRN$..D_ZAI_TRS   ZAI                           " & vbNewLine _
                                            & "WHERE ZAI.NRS_BR_CD    = @NRS_BR_CD                     " & vbNewLine _
                                            & "AND   ZAI.CUST_CD_L = @CUST_CD_L                        " & vbNewLine _
                                            & "AND   ZAI.CUST_CD_M = @CUST_CD_M                        " & vbNewLine _
                                            & "AND   ZAI.SYS_DEL_FLG  = '0'                            " & vbNewLine

#End Region

#Region "検索処理 SQL"

#Region "荷主マスタ"

#If False Then  'UPD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
        ''' <summary>
    ''' 荷主マスタ検索処理(件数取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
        Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(CST.NRS_BR_CD)	   AS SELECT_CNT   " & vbNewLine

#Else
    ''' <summary>
    ''' 荷主マスタ検索処理(件数取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(NRS_BR_CD)	   AS SELECT_CNT                                                                           " & vbNewLine _
                                                     & "   FROM (                                                                                                          " & vbNewLine _
                                                     & "         SELECT                                                                                                    " & vbNewLine _
                                                     & "         CST.NRS_BR_CD   AS  NRS_BR_CD                                                                             " & vbNewLine _
                                                     & "         --S_WEB_USER_COMP_DETAILS L                                                                               " & vbNewLine _
                                                     & "         ,ISNULL((SELECT      TOP 1                                                                                " & vbNewLine _
                                                     & "                    L.COMP_CD          AS COMP_CD_L                                                                " & vbNewLine _
                                                     & "                  FROM COM_DB..S_WEB_USER_COMP_DETAILS L                                                           " & vbNewLine _
                                                     & "                  WHERE                                                                                            " & vbNewLine _
                                                     & "                        L.COMP_CD     = CST.CUST_CD_L                                                              " & vbNewLine _
                                                     & "                    AND L.NRS_BR_CD   = CST.NRS_BR_CD                                                              " & vbNewLine _
                                                     & "                    AND L.SYS_DEL_FLG = '0'),'')   AS       WEB_COMP_CD_L                                          " & vbNewLine _
                                                     & "         --S_WEB_USER_COMP_DETAILS L+M                                                                             " & vbNewLine _
                                                     & "         ,ISNULL((SELECT      TOP 1                                                                                " & vbNewLine _
                                                     & "                    LM.COMP_CD         AS COMP_CD_L                                                                " & vbNewLine _
                                                     & "                  FROM COM_DB..S_WEB_USER_COMP_DETAILS LM                                                          " & vbNewLine _
                                                     & "                  WHERE                                                                                            " & vbNewLine _
                                                     & "                        LM.COMP_CD      = CST.CUST_CD_L + CST.CUST_CD_M                                            " & vbNewLine _
                                                     & "                    AND LM.NRS_BR_CD    = CST.NRS_BR_CD                                                            " & vbNewLine _
                                                     & "                    AND LM.SYS_DEL_FLG  = '0'),'') AS       WEB_COMP_CD_LM                                         " & vbNewLine _
                                                     & "         --S_WEB_USER_COMP_DETAILS L+M+S                                                                           " & vbNewLine _
                                                     & "         ,ISNULL((SELECT      TOP 1                                                                                " & vbNewLine _
                                                     & "                    LMS.COMP_CD        AS COMP_CD_L                                                                " & vbNewLine _
                                                     & "                  FROM COM_DB..S_WEB_USER_COMP_DETAILS LMS                                                         " & vbNewLine _
                                                     & "                  WHERE                                                                                            " & vbNewLine _
                                                     & "                       LMS.COMP_CD     = CST.CUST_CD_L + CST.CUST_CD_M + CST.CUST_CD_S                             " & vbNewLine _
                                                     & "                   AND LMS.NRS_BR_CD   = CST.NRS_BR_CD                                                             " & vbNewLine _
                                                     & "                   AND LMS.SYS_DEL_FLG = '0'),'')   AS       WEB_COMP_CD_LMS                                       " & vbNewLine _
                                                     & "         --S_WEB_USER_COMP_DETAILS L+M+S+SS                                                                        " & vbNewLine _
                                                     & "         ,ISNULL((SELECT      TOP 1                                                                                " & vbNewLine _
                                                     & "                    LMSSS.COMP_CD      AS COMP_CD_L                                                                " & vbNewLine _
                                                     & "                  FROM COM_DB..S_WEB_USER_COMP_DETAILS LMSSS                                                       " & vbNewLine _
                                                     & "                  WHERE                                                                                            " & vbNewLine _
                                                     & "                        LMSSS.COMP_CD     = CST.CUST_CD_L + CST.CUST_CD_M + CST.CUST_CD_S + CST.CUST_CD_SS         " & vbNewLine _
                                                     & "                    AND LMSSS.NRS_BR_CD   = CST.NRS_BR_CD                                                          " & vbNewLine _
                                                     & "                    AND LMSSS.SYS_DEL_FLG = '0'),'')   AS     WEB_COMP_CD_LMSSS                                    " & vbNewLine
#End If

    'START YANAI 要望番号824
    '''' <summary>
    '''' 荷主マスタ検索処理(データ取得(SELECT句))用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                                                                                                  " & vbNewLine _
    '                                               & "     CST.NRS_BR_CD                                                                    AS    NRS_BR_CD                    " & vbNewLine _
    '                                               & "    ,BRM.NRS_BR_NM                                                                    AS    NRS_BR_NM                    " & vbNewLine _
    '                                               & "    ,CST.CUST_CD_L                                                                    AS    CUST_CD_L                    " & vbNewLine _
    '                                               & "    ,CST.CUST_CD_M                                                                    AS    CUST_CD_M                    " & vbNewLine _
    '                                               & "    ,CST.CUST_CD_S                                                                    AS    CUST_CD_S                    " & vbNewLine _
    '                                               & "    ,CST.CUST_CD_SS                                                                   AS    CUST_CD_SS                   " & vbNewLine _
    '                                               & "    ,CST.CUST_CD_L + '-' + CST.CUST_CD_M + '-' + CST.CUST_CD_S + '-' + CST.CUST_CD_SS AS    CUST_CD                      " & vbNewLine _
    '                                               & "    ,CST.CUST_OYA_CD                                                                  AS    CUST_OYA_CD                  " & vbNewLine _
    '                                               & "    ,(SELECT MAX(CST2.CUST_NM_L) FROM   $LM_MST$..M_CUST      CST2                                                       " & vbNewLine _
    '                                               & "      WHERE  CST2.CUST_CD_L        =    CST.CUST_OYA_CD                                                                  " & vbNewLine _
    '                                               & "      AND CST2.NRS_BR_CD           =    CST.NRS_BR_CD                                                                    " & vbNewLine _
    '                                               & "      AND CST2.SYS_DEL_FLG         =    '0')                                          AS    CUST_OYA_NM                  " & vbNewLine _
    '                                               & "    ,CST.CUST_NM_L                                                                    AS    CUST_NM_L                    " & vbNewLine _
    '                                               & "    ,CST.CUST_NM_M                                                                    AS    CUST_NM_M                    " & vbNewLine _
    '                                               & "    ,CST.CUST_NM_S                                                                    AS    CUST_NM_S                    " & vbNewLine _
    '                                               & "    ,CST.CUST_NM_SS                                                                   AS    CUST_NM_SS                   " & vbNewLine _
    '                                               & "    ,CST.ZIP                                                                          AS    ZIP                          " & vbNewLine _
    '                                               & "    ,CST.AD_1                                                                         AS    AD_1                         " & vbNewLine _
    '                                               & "    ,CST.AD_2                                                                         AS    AD_2                         " & vbNewLine _
    '                                               & "    ,CST.AD_3                                                                         AS    AD_3                         " & vbNewLine _
    '                                               & "    ,CST.PIC                                                                          AS    PIC                          " & vbNewLine _
    '                                               & "    ,CST.FUKU_PIC                                                                     AS    FUKU_PIC                     " & vbNewLine _
    '                                               & "    ,CST.TEL                                                                          AS    TEL                          " & vbNewLine _
    '                                               & "    ,CST.FAX                                                                          AS    FAX                          " & vbNewLine _
    '                                               & "    ,CST.MAIL                                                                         AS    MAIL                         " & vbNewLine _
    '                                               & "    ,CST.SAITEI_HAN_KB                                                                AS    SAITEI_HAN_KB                " & vbNewLine _
    '                                               & "    ,CST.OYA_SEIQTO_CD                                                                AS    OYA_SEIQTO_CD                " & vbNewLine _
    '                                               & "    ,SQT1.SEIQTO_NM + ' '+ SQT1.SEIQTO_BUSYO_NM                                       AS    OYA_SEIQTO_NM                " & vbNewLine _
    '                                               & "    ,CST.HOKAN_SEIQTO_CD                                                              AS    HOKAN_SEIQTO_CD              " & vbNewLine _
    '                                               & "    ,SQT2.SEIQTO_NM + ' '+ SQT2.SEIQTO_BUSYO_NM                                       AS    HOKAN_SEIQTO_NM              " & vbNewLine _
    '                                               & "    ,CST.NIYAKU_SEIQTO_CD                                                             AS    NIYAKU_SEIQTO_CD             " & vbNewLine _
    '                                               & "    ,SQT3.SEIQTO_NM + ' '+ SQT3.SEIQTO_BUSYO_NM                                       AS    NIYAKU_SEIQTO_NM             " & vbNewLine _
    '                                               & "    ,CST.UNCHIN_SEIQTO_CD                                                             AS    UNCHIN_SEIQTO_CD             " & vbNewLine _
    '                                               & "    ,SQT4.SEIQTO_NM + ' '+ SQT4.SEIQTO_BUSYO_NM                                       AS    UNCHIN_SEIQTO_NM             " & vbNewLine _
    '                                               & "    ,CST.SAGYO_SEIQTO_CD                                                              AS    SAGYO_SEIQTO_CD              " & vbNewLine _
    '                                               & "    ,SQT5.SEIQTO_NM + ' '+ SQT5.SEIQTO_BUSYO_NM                                       AS    SAGYO_SEIQTO_NM              " & vbNewLine _
    '                                               & "    ,SQT1.CLOSE_KB                                                                    AS    CLOSE_KB                     " & vbNewLine _
    '                                               & "    ,CST.INKA_RPT_YN                                                                  AS    INKA_RPT_YN                  " & vbNewLine _
    '                                               & "    ,CST.OUTKA_RPT_YN                                                                 AS    OUTKA_RPT_YN                 " & vbNewLine _
    '                                               & "    ,CST.ZAI_RPT_YN                                                                   AS    ZAI_RPT_YN                   " & vbNewLine _
    '                                               & "    ,CST.UNSO_TEHAI_KB                                                                AS    UNSO_TEHAI_KB                " & vbNewLine _
    '                                               & "    ,CST.SP_UNSO_CD                                                                   AS    SP_UNSO_CD                   " & vbNewLine _
    '                                               & "    ,CST.SP_UNSO_BR_CD                                                                AS    SP_UNSO_BR_CD                " & vbNewLine _
    '                                               & "    ,UNS.UNSOCO_NM    + ' ' + UNS.UNSOCO_BR_NM                                        AS    SP_UNSO_NM                   " & vbNewLine _
    '                                               & "    ,CST.BETU_KYORI_CD                                                                AS    BETU_KYORI_CD                " & vbNewLine _
    '                                               & "    ,(SELECT                                                                                                             " & vbNewLine _
    '                                               & "         MAX(KYORI_REM)                                                                                                  " & vbNewLine _
    '                                               & "     FROM                                                                                                                " & vbNewLine _
    '                                               & "         $LM_MST$..M_KYORI KYO                                                                                           " & vbNewLine _
    '                                               & "     WHERE                                                                                                               " & vbNewLine _
    '                                               & "         KYO.NRS_BR_CD   =  CST.NRS_BR_CD                                                                                " & vbNewLine _
    '                                               & "     AND KYO.KYORI_CD    =  CST.BETU_KYORI_CD                                                                            " & vbNewLine _
    '                                               & "     AND KYO.SYS_DEL_FLG =  '0')                                                      AS    BETU_KYORI_REM               " & vbNewLine _
    '                                               & "    ,CST.TAX_KB                                                                       AS    TAX_KB                       " & vbNewLine _
    '                                               & "    ,CST.HOKAN_FREE_KIKAN                                                             AS    HOKAN_FREE_KIKAN             " & vbNewLine _
    '                                               & "    ,CST.SMPL_SAGYO                                                                   AS    SMPL_SAGYO                   " & vbNewLine _
    '                                               & "    ,SGY.SAGYO_NM                                                                     AS    SMPL_SAGYO_NM                " & vbNewLine _
    '                                               & "    ,CST.HOKAN_NIYAKU_CALCULATION                                                     AS    HOKAN_NIYAKU_CALCULATION     " & vbNewLine _
    '                                               & "    ,CST.HOKAN_NIYAKU_CALCULATION_OLD                                                 AS    HOKAN_NIYAKU_CALCULATION_OLD " & vbNewLine _
    '                                               & "    ,CST.NEW_JOB_NO                                                                   AS    NEW_JOB_NO                   " & vbNewLine _
    '                                               & "    ,CST.OLD_JOB_NO                                                                   AS    OLD_JOB_NO                   " & vbNewLine _
    '                                               & "    ,CST.HOKAN_NIYAKU_KEISAN_YN                                                       AS    HOKAN_NIYAKU_KEISAN_YN       " & vbNewLine _
    '                                               & "    ,CST.UNTIN_CALCULATION_KB                                                         AS    UNTIN_CALCULATION_KB         " & vbNewLine _
    '                                               & "    ,CST.DENPYO_NM                                                                    AS    DENPYO_NM                    " & vbNewLine _
    '                                               & "    ,CST.SOKO_CHANGE_KB                                                               AS    SOKO_CHANGE_KB               " & vbNewLine _
    '                                               & "    ,CST.DEFAULT_SOKO_CD                                                              AS    DEFAULT_SOKO_CD              " & vbNewLine _
    '                                               & "    ,CST.PICK_LIST_KB                                                                 AS    PICK_LIST_KB                 " & vbNewLine _
    '                                               & "    ,CST.SEKY_OFB_KB                                                                  AS    SEKY_OFB_KB                  " & vbNewLine _
    '                                               & "    ,CST.SYS_ENT_DATE                                                                 AS    SYS_ENT_DATE                 " & vbNewLine _
    '                                               & "    ,USE1.USER_NM                                                                     AS    SYS_ENT_USER_NM              " & vbNewLine _
    '                                               & "    ,CST.SYS_UPD_DATE                                                                 AS    SYS_UPD_DATE                 " & vbNewLine _
    '                                               & "    ,CST.SYS_UPD_TIME                                                                 AS    SYS_UPD_TIME                 " & vbNewLine _
    '                                               & "    ,USE2.USER_NM                                                                     AS    SYS_UPD_USER_NM              " & vbNewLine _
    '                                               & "    ,CST.SYS_DEL_FLG                                                                  AS    SYS_DEL_FLG                  " & vbNewLine _
    '                                               & "    ,KBN1.KBN_NM1                                                                     AS    SYS_DEL_NM                   " & vbNewLine

#If False Then  'UPD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
        ''' <summary>
    ''' 荷主マスタ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                                                                                                  " & vbNewLine _
                                                   & "     CST.NRS_BR_CD                                                                    AS    NRS_BR_CD                    " & vbNewLine _
                                                   & "    ,BRM.NRS_BR_NM                                                                    AS    NRS_BR_NM                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_L                                                                    AS    CUST_CD_L                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_M                                                                    AS    CUST_CD_M                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_S                                                                    AS    CUST_CD_S                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_SS                                                                   AS    CUST_CD_SS                   " & vbNewLine _
                                                   & "    ,CST.CUST_CD_L + '-' + CST.CUST_CD_M + '-' + CST.CUST_CD_S + '-' + CST.CUST_CD_SS AS    CUST_CD                      " & vbNewLine _
                                                   & "    ,CST.CUST_OYA_CD                                                                  AS    CUST_OYA_CD                  " & vbNewLine _
                                                   & "    ,(SELECT MAX(CST2.CUST_NM_L) FROM   $LM_MST$..M_CUST      CST2                                                       " & vbNewLine _
                                                   & "      WHERE  CST2.CUST_CD_L        =    CST.CUST_OYA_CD                                                                  " & vbNewLine _
                                                   & "      AND CST2.NRS_BR_CD           =    CST.NRS_BR_CD                                                                    " & vbNewLine _
                                                   & "      AND CST2.SYS_DEL_FLG         =    '0')                                          AS    CUST_OYA_NM                  " & vbNewLine _
                                                   & "    ,CST.CUST_NM_L                                                                    AS    CUST_NM_L                    " & vbNewLine _
                                                   & "    ,CST.CUST_NM_M                                                                    AS    CUST_NM_M                    " & vbNewLine _
                                                   & "    ,CST.CUST_NM_S                                                                    AS    CUST_NM_S                    " & vbNewLine _
                                                   & "    ,CST.CUST_NM_SS                                                                   AS    CUST_NM_SS                   " & vbNewLine _
                                                   & "    ,CST.ZIP                                                                          AS    ZIP                          " & vbNewLine _
                                                   & "    ,CST.AD_1                                                                         AS    AD_1                         " & vbNewLine _
                                                   & "    ,CST.AD_2                                                                         AS    AD_2                         " & vbNewLine _
                                                   & "    ,CST.AD_3                                                                         AS    AD_3                         " & vbNewLine _
                                                   & "    ,CST.PIC                                                                          AS    PIC                          " & vbNewLine _
                                                   & "    ,CST.FUKU_PIC                                                                     AS    FUKU_PIC                     " & vbNewLine _
                                                   & "    ,CST.TEL                                                                          AS    TEL                          " & vbNewLine _
                                                   & "    ,CST.FAX                                                                          AS    FAX                          " & vbNewLine _
                                                   & "    ,CST.MAIL                                                                         AS    MAIL                         " & vbNewLine _
                                                   & "    ,CST.SAITEI_HAN_KB                                                                AS    SAITEI_HAN_KB                " & vbNewLine _
                                                   & "    ,CST.OYA_SEIQTO_CD                                                                AS    OYA_SEIQTO_CD                " & vbNewLine _
                                                   & "    ,SQT1.SEIQTO_NM + ' '+ SQT1.SEIQTO_BUSYO_NM                                       AS    OYA_SEIQTO_NM                " & vbNewLine _
                                                   & "    ,CST.HOKAN_SEIQTO_CD                                                              AS    HOKAN_SEIQTO_CD              " & vbNewLine _
                                                   & "    ,SQT2.SEIQTO_NM + ' '+ SQT2.SEIQTO_BUSYO_NM                                       AS    HOKAN_SEIQTO_NM              " & vbNewLine _
                                                   & "    ,CST.NIYAKU_SEIQTO_CD                                                             AS    NIYAKU_SEIQTO_CD             " & vbNewLine _
                                                   & "    ,SQT3.SEIQTO_NM + ' '+ SQT3.SEIQTO_BUSYO_NM                                       AS    NIYAKU_SEIQTO_NM             " & vbNewLine _
                                                   & "    ,CST.UNCHIN_SEIQTO_CD                                                             AS    UNCHIN_SEIQTO_CD             " & vbNewLine _
                                                   & "    ,SQT4.SEIQTO_NM + ' '+ SQT4.SEIQTO_BUSYO_NM                                       AS    UNCHIN_SEIQTO_NM             " & vbNewLine _
                                                   & "    ,CST.SAGYO_SEIQTO_CD                                                              AS    SAGYO_SEIQTO_CD              " & vbNewLine _
                                                   & "    ,SQT5.SEIQTO_NM + ' '+ SQT5.SEIQTO_BUSYO_NM                                       AS    SAGYO_SEIQTO_NM              " & vbNewLine _
                                                   & "    ,SQT1.CLOSE_KB                                                                    AS    CLOSE_KB                     " & vbNewLine _
                                                   & "    ,CST.INKA_RPT_YN                                                                  AS    INKA_RPT_YN                  " & vbNewLine _
                                                   & "    ,CST.OUTKA_RPT_YN                                                                 AS    OUTKA_RPT_YN                 " & vbNewLine _
                                                   & "    ,CST.ZAI_RPT_YN                                                                   AS    ZAI_RPT_YN                   " & vbNewLine _
                                                   & "    ,CST.UNSO_TEHAI_KB                                                                AS    UNSO_TEHAI_KB                " & vbNewLine _
                                                   & "    ,CST.SP_UNSO_CD                                                                   AS    SP_UNSO_CD                   " & vbNewLine _
                                                   & "    ,CST.SP_UNSO_BR_CD                                                                AS    SP_UNSO_BR_CD                " & vbNewLine _
                                                   & "    ,UNS.UNSOCO_NM    + ' ' + UNS.UNSOCO_BR_NM                                        AS    SP_UNSO_NM                   " & vbNewLine _
                                                   & "    ,CST.BETU_KYORI_CD                                                                AS    BETU_KYORI_CD                " & vbNewLine _
                                                   & "    ,(SELECT                                                                                                             " & vbNewLine _
                                                   & "         MAX(KYORI_REM)                                                                                                  " & vbNewLine _
                                                   & "     FROM                                                                                                                " & vbNewLine _
                                                   & "         $LM_MST$..M_KYORI KYO                                                                                           " & vbNewLine _
                                                   & "     WHERE                                                                                                               " & vbNewLine _
                                                   & "         KYO.NRS_BR_CD   =  CST.NRS_BR_CD                                                                                " & vbNewLine _
                                                   & "     AND KYO.KYORI_CD    =  CST.BETU_KYORI_CD                                                                            " & vbNewLine _
                                                   & "     AND KYO.SYS_DEL_FLG =  '0')                                                      AS    BETU_KYORI_REM               " & vbNewLine _
                                                   & "    ,CST.TAX_KB                                                                       AS    TAX_KB                       " & vbNewLine _
                                                   & "    ,CST.HOKAN_FREE_KIKAN                                                             AS    HOKAN_FREE_KIKAN             " & vbNewLine _
                                                   & "    ,CST.SMPL_SAGYO                                                                   AS    SMPL_SAGYO                   " & vbNewLine _
                                                   & "    ,SGY.SAGYO_NM                                                                     AS    SMPL_SAGYO_NM                " & vbNewLine _
                                                   & "    ,CST.HOKAN_NIYAKU_CALCULATION                                                     AS    HOKAN_NIYAKU_CALCULATION     " & vbNewLine _
                                                   & "    ,CST.HOKAN_NIYAKU_CALCULATION_OLD                                                 AS    HOKAN_NIYAKU_CALCULATION_OLD " & vbNewLine _
                                                   & "    ,CST.NEW_JOB_NO                                                                   AS    NEW_JOB_NO                   " & vbNewLine _
                                                   & "    ,CST.OLD_JOB_NO                                                                   AS    OLD_JOB_NO                   " & vbNewLine _
                                                   & "    ,CST.HOKAN_NIYAKU_KEISAN_YN                                                       AS    HOKAN_NIYAKU_KEISAN_YN       " & vbNewLine _
                                                   & "    ,CST.UNTIN_CALCULATION_KB                                                         AS    UNTIN_CALCULATION_KB         " & vbNewLine _
                                                   & "    ,CST.DENPYO_NM                                                                    AS    DENPYO_NM                    " & vbNewLine _
                                                   & "    ,CST.SOKO_CHANGE_KB                                                               AS    SOKO_CHANGE_KB               " & vbNewLine _
                                                   & "    ,CST.DEFAULT_SOKO_CD                                                              AS    DEFAULT_SOKO_CD              " & vbNewLine _
                                                   & "    ,CST.PICK_LIST_KB                                                                 AS    PICK_LIST_KB                 " & vbNewLine _
                                                   & "    ,CST.SEKY_OFB_KB                                                                  AS    SEKY_OFB_KB                  " & vbNewLine _
                                                   & "    ,CST.SYS_ENT_DATE                                                                 AS    SYS_ENT_DATE                 " & vbNewLine _
                                                   & "    ,USE1.USER_NM                                                                     AS    SYS_ENT_USER_NM              " & vbNewLine _
                                                   & "    ,CST.SYS_UPD_DATE                                                                 AS    SYS_UPD_DATE                 " & vbNewLine _
                                                   & "    ,CST.SYS_UPD_TIME                                                                 AS    SYS_UPD_TIME                 " & vbNewLine _
                                                   & "    ,USE2.USER_NM                                                                     AS    SYS_UPD_USER_NM              " & vbNewLine _
                                                   & "    ,CST.SYS_DEL_FLG                                                                  AS    SYS_DEL_FLG                  " & vbNewLine _
                                                   & "    ,KBN1.KBN_NM1                                                                     AS    SYS_DEL_NM                   " & vbNewLine _
                                                   & "    ,CST.TANTO_CD                                                                     AS    TANTO_CD                     " & vbNewLine _
                                                   & "    ,USE3.USER_NM                                                                     AS    TANTO_NM                     " & vbNewLine _
                                                   & "    ,CST.ITEM_CURR_CD                                                                 AS    ITEM_CURR_CD                 " & vbNewLine _
                                                   & "    ,CST.UNSO_HOKEN_AUTO_YN                                                           AS    UNSO_HOKEN_AUTO_YN   --ADD 2018/10/22 002400対応" & vbNewLine _
                                                   & "    ,CST.INKA_ORIG_CD                                                                 AS    INKA_ORIG_CD         --ADD 2018/10/25 001820対応" & vbNewLine _
                                                   & "    ,DST.DEST_NM                                                                      AS    INKA_ORIG_NM         --ADD 2018/10/25 001820対応" & vbNewLine _
                                                   & "    ,CST.INIT_OUTKA_PLAN_DATE_KB                                                      AS    INIT_OUTKA_PLAN_DATE_KB  --ADD 2018/10/30 002192対応" & vbNewLine _
                                                   & "    ,CST.INIT_INKA_PLAN_DATE_KB                                                       AS    INIT_INKA_PLAN_DATE_KB   --ADD 2018/10/30 002192対応" & vbNewLine _
                                                   & "    ,CST.COA_INKA_DATE_FLG                                                            AS    COA_INKA_DATE_FLG    --ADD 2018/11/14 001939対応" & vbNewLine 

    'END YANAI 要望番号824
#Else
    ''' <summary>
    ''' 荷主マスタ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT BASE.*                                                                 " & vbNewLine _
                                                   & "      ,CASE WHEN BASE.WEB_COMP_CD_L <> ''                                      " & vbNewLine _
                                                   & "		   OR  BASE.WEB_COMP_CD_LM <> ''                                         " & vbNewLine _
                                                   & "		   OR  BASE.WEB_COMP_CD_LMS <> ''                                        " & vbNewLine _
                                                   & "		   OR  BASE.WEB_COMP_CD_LMSSS <> ''  THEN '〇'                           " & vbNewLine _
                                                   & "		                                     ELSE '-'  END AS INTEG_WEB_FLG 　   " & vbNewLine _
                                                   & "  FROM (                                                                       " & vbNewLine _
                                                   & "    SELECT                                                                     " & vbNewLine _
                                                   & "     CST.NRS_BR_CD                                                                    AS    NRS_BR_CD                    " & vbNewLine _
                                                   & "    ,BRM.NRS_BR_NM                                                                    AS    NRS_BR_NM                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_L                                                                    AS    CUST_CD_L                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_M                                                                    AS    CUST_CD_M                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_S                                                                    AS    CUST_CD_S                    " & vbNewLine _
                                                   & "    ,CST.CUST_CD_SS                                                                   AS    CUST_CD_SS                   " & vbNewLine _
                                                   & "    ,CST.CUST_CD_L + '-' + CST.CUST_CD_M + '-' + CST.CUST_CD_S + '-' + CST.CUST_CD_SS AS    CUST_CD                      " & vbNewLine _
                                                   & "    ,CST.CUST_OYA_CD                                                                  AS    CUST_OYA_CD                  " & vbNewLine _
                                                   & "    ,(SELECT MAX(CST2.CUST_NM_L) FROM   $LM_MST$..M_CUST      CST2                                                       " & vbNewLine _
                                                   & "      WHERE  CST2.CUST_CD_L        =    CST.CUST_OYA_CD                                                                  " & vbNewLine _
                                                   & "      AND CST2.NRS_BR_CD           =    CST.NRS_BR_CD                                                                    " & vbNewLine _
                                                   & "      AND CST2.SYS_DEL_FLG         =    '0')                                          AS    CUST_OYA_NM                  " & vbNewLine _
                                                   & "    ,CST.CUST_NM_L                                                                    AS    CUST_NM_L                    " & vbNewLine _
                                                   & "    ,CST.CUST_NM_M                                                                    AS    CUST_NM_M                    " & vbNewLine _
                                                   & "    ,CST.CUST_NM_S                                                                    AS    CUST_NM_S                    " & vbNewLine _
                                                   & "    ,CST.CUST_NM_SS                                                                   AS    CUST_NM_SS                   " & vbNewLine _
                                                   & "    ,CST.ZIP                                                                          AS    ZIP                          " & vbNewLine _
                                                   & "    ,CST.AD_1                                                                         AS    AD_1                         " & vbNewLine _
                                                   & "    ,CST.AD_2                                                                         AS    AD_2                         " & vbNewLine _
                                                   & "    ,CST.AD_3                                                                         AS    AD_3                         " & vbNewLine _
                                                   & "    ,CST.PIC                                                                          AS    PIC                          " & vbNewLine _
                                                   & "    ,CST.FUKU_PIC                                                                     AS    FUKU_PIC                     " & vbNewLine _
                                                   & "    ,CST.TEL                                                                          AS    TEL                          " & vbNewLine _
                                                   & "    ,CST.FAX                                                                          AS    FAX                          " & vbNewLine _
                                                   & "    ,CST.MAIL                                                                         AS    MAIL                         " & vbNewLine _
                                                   & "    ,CST.SAITEI_HAN_KB                                                                AS    SAITEI_HAN_KB                " & vbNewLine _
                                                   & "    ,CST.OYA_SEIQTO_CD                                                                AS    OYA_SEIQTO_CD                " & vbNewLine _
                                                   & "    ,SQT1.SEIQTO_NM + ' '+ SQT1.SEIQTO_BUSYO_NM                                       AS    OYA_SEIQTO_NM                " & vbNewLine _
                                                   & "    ,CST.HOKAN_SEIQTO_CD                                                              AS    HOKAN_SEIQTO_CD              " & vbNewLine _
                                                   & "    ,SQT2.SEIQTO_NM + ' '+ SQT2.SEIQTO_BUSYO_NM                                       AS    HOKAN_SEIQTO_NM              " & vbNewLine _
                                                   & "    ,CST.NIYAKU_SEIQTO_CD                                                             AS    NIYAKU_SEIQTO_CD             " & vbNewLine _
                                                   & "    ,SQT3.SEIQTO_NM + ' '+ SQT3.SEIQTO_BUSYO_NM                                       AS    NIYAKU_SEIQTO_NM             " & vbNewLine _
                                                   & "    ,CST.UNCHIN_SEIQTO_CD                                                             AS    UNCHIN_SEIQTO_CD             " & vbNewLine _
                                                   & "    ,SQT4.SEIQTO_NM + ' '+ SQT4.SEIQTO_BUSYO_NM                                       AS    UNCHIN_SEIQTO_NM             " & vbNewLine _
                                                   & "    ,CST.SAGYO_SEIQTO_CD                                                              AS    SAGYO_SEIQTO_CD              " & vbNewLine _
                                                   & "    ,SQT5.SEIQTO_NM + ' '+ SQT5.SEIQTO_BUSYO_NM                                       AS    SAGYO_SEIQTO_NM              " & vbNewLine _
                                                   & "    ,SQT1.CLOSE_KB                                                                    AS    CLOSE_KB                     " & vbNewLine _
                                                   & "    ,CST.INKA_RPT_YN                                                                  AS    INKA_RPT_YN                  " & vbNewLine _
                                                   & "    ,CST.OUTKA_RPT_YN                                                                 AS    OUTKA_RPT_YN                 " & vbNewLine _
                                                   & "    ,CST.ZAI_RPT_YN                                                                   AS    ZAI_RPT_YN                   " & vbNewLine _
                                                   & "    ,CST.UNSO_TEHAI_KB                                                                AS    UNSO_TEHAI_KB                " & vbNewLine _
                                                   & "    ,CST.SP_UNSO_CD                                                                   AS    SP_UNSO_CD                   " & vbNewLine _
                                                   & "    ,CST.SP_UNSO_BR_CD                                                                AS    SP_UNSO_BR_CD                " & vbNewLine _
                                                   & "    ,UNS.UNSOCO_NM    + ' ' + UNS.UNSOCO_BR_NM                                        AS    SP_UNSO_NM                   " & vbNewLine _
                                                   & "    ,CST.BETU_KYORI_CD                                                                AS    BETU_KYORI_CD                " & vbNewLine _
                                                   & "    ,CST.REMARK                                                                       AS    REMARK     --ADD 2019/07/10 002520 " & vbNewLine _
                                                   & "    ,(SELECT                                                                                                             " & vbNewLine _
                                                   & "         MAX(KYORI_REM)                                                                                                  " & vbNewLine _
                                                   & "     FROM                                                                                                                " & vbNewLine _
                                                   & "         $LM_MST$..M_KYORI KYO                                                                                           " & vbNewLine _
                                                   & "     WHERE                                                                                                               " & vbNewLine _
                                                   & "         KYO.NRS_BR_CD   =  CST.NRS_BR_CD                                                                                " & vbNewLine _
                                                   & "     AND KYO.KYORI_CD    =  CST.BETU_KYORI_CD                                                                            " & vbNewLine _
                                                   & "     AND KYO.SYS_DEL_FLG =  '0')                                                      AS    BETU_KYORI_REM               " & vbNewLine _
                                                   & "    ,CST.TAX_KB                                                                       AS    TAX_KB                       " & vbNewLine _
                                                   & "    ,CST.HOKAN_FREE_KIKAN                                                             AS    HOKAN_FREE_KIKAN             " & vbNewLine _
                                                   & "    ,CST.SMPL_SAGYO                                                                   AS    SMPL_SAGYO                   " & vbNewLine _
                                                   & "    ,SGY.SAGYO_NM                                                                     AS    SMPL_SAGYO_NM                " & vbNewLine _
                                                   & "    ,CST.PRODUCT_SEG_CD                                                               AS    PRODUCT_SEG_CD               " & vbNewLine _
                                                   & "    ,SEG.SGMT_L_NM                                                                    AS    PRODUCT_SEG_NM_L             " & vbNewLine _
                                                   & "    ,SEG.SGMT_M_NM                                                                    AS    PRODUCT_SEG_NM_M             " & vbNewLine _
                                                   & "    ,CST.TCUST_BPCD                                                                   AS    TCUST_BPCD                   " & vbNewLine _
                                                   & "    ,MBP.BP_NM1                                                                       AS    TCUST_BPNM                   " & vbNewLine _
                                                   & "    ,CST.HOKAN_NIYAKU_CALCULATION                                                     AS    HOKAN_NIYAKU_CALCULATION     " & vbNewLine _
                                                   & "    ,CST.HOKAN_NIYAKU_CALCULATION_OLD                                                 AS    HOKAN_NIYAKU_CALCULATION_OLD " & vbNewLine _
                                                   & "    ,CST.NEW_JOB_NO                                                                   AS    NEW_JOB_NO                   " & vbNewLine _
                                                   & "    ,CST.OLD_JOB_NO                                                                   AS    OLD_JOB_NO                   " & vbNewLine _
                                                   & "    ,CST.HOKAN_NIYAKU_KEISAN_YN                                                       AS    HOKAN_NIYAKU_KEISAN_YN       " & vbNewLine _
                                                   & "    ,CST.UNTIN_CALCULATION_KB                                                         AS    UNTIN_CALCULATION_KB         " & vbNewLine _
                                                   & "    ,CST.DENPYO_NM                                                                    AS    DENPYO_NM                    " & vbNewLine _
                                                   & "    ,CST.SOKO_CHANGE_KB                                                               AS    SOKO_CHANGE_KB               " & vbNewLine _
                                                   & "    ,CST.DEFAULT_SOKO_CD                                                              AS    DEFAULT_SOKO_CD              " & vbNewLine _
                                                   & "    ,CST.PICK_LIST_KB                                                                 AS    PICK_LIST_KB                 " & vbNewLine _
                                                   & "    ,CST.SEKY_OFB_KB                                                                  AS    SEKY_OFB_KB                  " & vbNewLine _
                                                   & "    ,CST.SYS_ENT_DATE                                                                 AS    SYS_ENT_DATE                 " & vbNewLine _
                                                   & "    ,USE1.USER_NM                                                                     AS    SYS_ENT_USER_NM              " & vbNewLine _
                                                   & "    ,CST.SYS_UPD_DATE                                                                 AS    SYS_UPD_DATE                 " & vbNewLine _
                                                   & "    ,CST.SYS_UPD_TIME                                                                 AS    SYS_UPD_TIME                 " & vbNewLine _
                                                   & "    ,USE2.USER_NM                                                                     AS    SYS_UPD_USER_NM              " & vbNewLine _
                                                   & "    ,CST.SYS_DEL_FLG                                                                  AS    SYS_DEL_FLG                  " & vbNewLine _
                                                   & "    ,KBN1.KBN_NM1                                                                     AS    SYS_DEL_NM                   " & vbNewLine _
                                                   & "    ,CST.TANTO_CD                                                                     AS    TANTO_CD                     " & vbNewLine _
                                                   & "    ,USE3.USER_NM                                                                     AS    TANTO_NM                     " & vbNewLine _
                                                   & "    ,CST.ITEM_CURR_CD                                                                 AS    ITEM_CURR_CD                 " & vbNewLine _
                                                   & "    ,CST.UNSO_HOKEN_AUTO_YN                                                           AS    UNSO_HOKEN_AUTO_YN   --ADD 2018/10/22 002400対応" & vbNewLine _
                                                   & "    ,CST.INKA_ORIG_CD                                                                 AS    INKA_ORIG_CD         --ADD 2018/10/25 001820対応" & vbNewLine _
                                                   & "    ,DST.DEST_NM                                                                      AS    INKA_ORIG_NM         --ADD 2018/10/25 001820対応" & vbNewLine _
                                                   & "    ,CST.INIT_OUTKA_PLAN_DATE_KB                                                      AS    INIT_OUTKA_PLAN_DATE_KB  --ADD 2018/10/30 002192対応" & vbNewLine _
                                                   & "    ,CST.INIT_INKA_PLAN_DATE_KB                                                       AS    INIT_INKA_PLAN_DATE_KB   --ADD 2018/10/30 002192対応" & vbNewLine _
                                                   & "    ,CST.COA_INKA_DATE_FLG                                                            AS    COA_INKA_DATE_FLG    --ADD 2018/11/14 001939対応" & vbNewLine _
                                                   & "-- ADD Start 2018/12/28  依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能                  " & vbNewLine _
                                                    & "--S_WEB_USER_COMP_DETAILS L                                                                                               " & vbNewLine _
                                                    & "      ,ISNULL((SELECT      TOP 1                                                                                           " & vbNewLine _
                                                    & "                 L.COMP_CD          AS COMP_CD_L                                                                          " & vbNewLine _
                                                    & "              FROM COM_DB..S_WEB_USER_COMP_DETAILS L                                                                      " & vbNewLine _
                                                    & "              WHERE                                                                                                       " & vbNewLine _
                                                    & "                  L.COMP_CD     = CST.CUST_CD_L                                                                           " & vbNewLine _
                                                    & "              AND L.NRS_BR_CD   = CST.NRS_BR_CD                                                                           " & vbNewLine _
                                                    & "              AND L.SYS_DEL_FLG = '0'),'')   AS       WEB_COMP_CD_L                                                       " & vbNewLine _
                                                    & "--S_WEB_USER_COMP_DETAILS L+M                                                                                             " & vbNewLine _
                                                    & "       ,ISNULL((SELECT      TOP 1                                                                                         " & vbNewLine _
                                                    & "                  LM.COMP_CD         AS COMP_CD_L                                                                         " & vbNewLine _
                                                    & "                FROM COM_DB..S_WEB_USER_COMP_DETAILS LM                                                                   " & vbNewLine _
                                                    & "                WHERE                                                                                                     " & vbNewLine _
                                                    & "                    LM.COMP_CD      = CST.CUST_CD_L + CST.CUST_CD_M                                                       " & vbNewLine _
                                                    & "                AND LM.NRS_BR_CD    = CST.NRS_BR_CD                                                                       " & vbNewLine _
                                                    & "                AND LM.SYS_DEL_FLG  = '0'),'') AS       WEB_COMP_CD_LM                                                    " & vbNewLine _
                                                    & "--S_WEB_USER_COMP_DETAILS L+M+S                                                                                           " & vbNewLine _
                                                    & "     ,ISNULL((SELECT      TOP 1                                                                                           " & vbNewLine _
                                                    & "                LMS.COMP_CD        AS COMP_CD_L                                                                           " & vbNewLine _
                                                    & "              FROM COM_DB..S_WEB_USER_COMP_DETAILS LMS                                                                    " & vbNewLine _
                                                    & "              WHERE                                                                                                       " & vbNewLine _
                                                    & "                  LMS.COMP_CD     = CST.CUST_CD_L + CST.CUST_CD_M + CST.CUST_CD_S                                         " & vbNewLine _
                                                    & "              AND LMS.NRS_BR_CD   = CST.NRS_BR_CD                                                                         " & vbNewLine _
                                                    & "              AND LMS.SYS_DEL_FLG = '0'),'')   AS       WEB_COMP_CD_LMS                                                   " & vbNewLine _
                                                    & "--S_WEB_USER_COMP_DETAILS L+M+S+SS                                                                                        " & vbNewLine _
                                                    & "     ,ISNULL((SELECT      TOP 1                                                                                           " & vbNewLine _
                                                    & "                LMSSS.COMP_CD      AS COMP_CD_L                                                                           " & vbNewLine _
                                                    & "              FROM COM_DB..S_WEB_USER_COMP_DETAILS LMSSS                                                                  " & vbNewLine _
                                                    & "               WHERE                                                                                                      " & vbNewLine _
                                                    & "                   LMSSS.COMP_CD     = CST.CUST_CD_L + CST.CUST_CD_M + CST.CUST_CD_S + CST.CUST_CD_SS                     " & vbNewLine _
                                                    & "               AND LMSSS.NRS_BR_CD   = CST.NRS_BR_CD                                                                      " & vbNewLine _
                                                    & "               AND LMSSS.SYS_DEL_FLG = '0'),'')   AS     WEB_COMP_CD_LMSSS                                                " & vbNewLine _
                                                    & "-- ADD END   2018/12/28   依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能                 " & vbNewLine
    'END YANAI 要望番号824
#End If

    'END YANAI 要望番号824

    'START YANAI 要望番号824
    '''' <summary>
    '''' 荷主マスタ検索処理(データ取得)用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA_FROM As String = " FROM                                                          " & vbNewLine _
    '                                        & "    $LM_MST$..M_CUST    CST                                         " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_NRS_BR    BRM                                 " & vbNewLine _
    '                                        & "ON  BRM.NRS_BR_CD         =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND BRM.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT1                                " & vbNewLine _
    '                                        & "ON  SQT1.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND SQT1.SEIQTO_CD        =    CST.OYA_SEIQTO_CD                    " & vbNewLine _
    '                                        & "AND SQT1.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT2                                " & vbNewLine _
    '                                        & "ON  SQT2.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND SQT2.SEIQTO_CD        =    CST.HOKAN_SEIQTO_CD                  " & vbNewLine _
    '                                        & "AND SQT2.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT3                                " & vbNewLine _
    '                                        & "ON  SQT3.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND SQT3.SEIQTO_CD        =    CST.NIYAKU_SEIQTO_CD                 " & vbNewLine _
    '                                        & "AND SQT3.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT4                                " & vbNewLine _
    '                                        & "ON  SQT4.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND SQT4.SEIQTO_CD        =    CST.UNCHIN_SEIQTO_CD                 " & vbNewLine _
    '                                        & "AND SQT4.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT5                                " & vbNewLine _
    '                                        & "ON  SQT5.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND SQT5.SEIQTO_CD        =    CST.SAGYO_SEIQTO_CD                  " & vbNewLine _
    '                                        & "AND SQT5.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_UNSOCO    UNS                                 " & vbNewLine _
    '                                        & "ON  UNS.NRS_BR_CD         =    CST.NRS_BR_CD                        " & vbNewLine _
    '                                        & "AND UNS.UNSOCO_CD         =    CST.SP_UNSO_CD                       " & vbNewLine _
    '                                        & "AND UNS.UNSOCO_BR_CD      =    CST.SP_UNSO_BR_CD                    " & vbNewLine _
    '                                        & "AND UNS.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..M_SAGYO    SGY                                  " & vbNewLine _
    '                                        & "ON  SGY.SAGYO_CD          =    CST.SMPL_SAGYO                       " & vbNewLine _
    '                                        & "AND SGY.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..S_USER      USE1                                " & vbNewLine _
    '                                        & "ON  USE1.USER_CD          =    CST.SYS_ENT_USER                     " & vbNewLine _
    '                                        & "AND USE1.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..S_USER      USE2                                " & vbNewLine _
    '                                        & "ON  USE2.USER_CD          =    CST.SYS_UPD_USER                     " & vbNewLine _
    '                                        & "AND USE2.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
    '                                        & "LEFT JOIN $LM_MST$..Z_KBN      KBN1                                 " & vbNewLine _
    '                                        & "ON  KBN1.KBN_GROUP_CD     =    'S051'                               " & vbNewLine _
    '                                        & "AND KBN1.KBN_CD           =    CST.SYS_DEL_FLG                      " & vbNewLine _
    '                                        & "AND KBN1.SYS_DEL_FLG      =    '0'                                  " & vbNewLine
    ''' <summary>
    ''' 荷主マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM As String = " FROM                                                          " & vbNewLine _
                                            & "    $LM_MST$..M_CUST    CST                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_NRS_BR    BRM                                 " & vbNewLine _
                                            & "ON  BRM.NRS_BR_CD         =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND BRM.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT1                                " & vbNewLine _
                                            & "ON  SQT1.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND SQT1.SEIQTO_CD        =    CST.OYA_SEIQTO_CD                    " & vbNewLine _
                                            & "AND SQT1.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT2                                " & vbNewLine _
                                            & "ON  SQT2.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND SQT2.SEIQTO_CD        =    CST.HOKAN_SEIQTO_CD                  " & vbNewLine _
                                            & "AND SQT2.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT3                                " & vbNewLine _
                                            & "ON  SQT3.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND SQT3.SEIQTO_CD        =    CST.NIYAKU_SEIQTO_CD                 " & vbNewLine _
                                            & "AND SQT3.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT4                                " & vbNewLine _
                                            & "ON  SQT4.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND SQT4.SEIQTO_CD        =    CST.UNCHIN_SEIQTO_CD                 " & vbNewLine _
                                            & "AND SQT4.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO    SQT5                                " & vbNewLine _
                                            & "ON  SQT5.NRS_BR_CD        =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND SQT5.SEIQTO_CD        =    CST.SAGYO_SEIQTO_CD                  " & vbNewLine _
                                            & "AND SQT5.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_UNSOCO    UNS                                 " & vbNewLine _
                                            & "ON  UNS.NRS_BR_CD         =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND UNS.UNSOCO_CD         =    CST.SP_UNSO_CD                       " & vbNewLine _
                                            & "AND UNS.UNSOCO_BR_CD      =    CST.SP_UNSO_BR_CD                    " & vbNewLine _
                                            & "AND UNS.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SAGYO    SGY                                  " & vbNewLine _
                                            & "ON  SGY.SAGYO_CD          =    CST.SMPL_SAGYO                       " & vbNewLine _
                                            & "AND SGY.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER      USE1                                " & vbNewLine _
                                            & "ON  USE1.USER_CD          =    CST.SYS_ENT_USER                     " & vbNewLine _
                                            & "AND USE1.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER      USE2                                " & vbNewLine _
                                            & "ON  USE2.USER_CD          =    CST.SYS_UPD_USER                     " & vbNewLine _
                                            & "AND USE2.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER      USE3                                " & vbNewLine _
                                            & "ON  USE3.USER_CD          =    CST.TANTO_CD                         " & vbNewLine _
                                            & "AND USE3.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN      KBN1                                 " & vbNewLine _
                                            & "ON  KBN1.KBN_GROUP_CD     =    'S051'                               " & vbNewLine _
                                            & "AND KBN1.KBN_CD           =    CST.SYS_DEL_FLG                      " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG      =    '0'                                  " & vbNewLine _
                                            & "-- ADD Start 2018/10/25 要望番号001820                              " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST     DST                                  " & vbNewLine _
                                            & "ON  DST.NRS_BR_CD         =    CST.NRS_BR_CD                        " & vbNewLine _
                                            & "AND DST.CUST_CD_L         =    CST.CUST_CD_L                        " & vbNewLine _
                                            & "AND DST.DEST_CD           =    CST.INKA_ORIG_CD                     " & vbNewLine _
                                            & "AND DST.SYS_DEL_FLG       =    '0'                                  " & vbNewLine _
                                            & "-- ADD End   2018/10/25 要望番号001820                              " & vbNewLine _
                                            & "LEFT JOIN ABM_DB..M_SEGMENT    SEG                                  " & vbNewLine _
                                            & "ON  SEG.DATA_TYPE_CD      =    '00002'                              " & vbNewLine _
                                            & "AND SEG.CNCT_SEG_CD       =    CST.PRODUCT_SEG_CD                   " & vbNewLine _
                                            & "AND SEG.KBN_LANG          =    @KBN_LANG                            " & vbNewLine _
                                            & "LEFT JOIN ABM_DB..M_BP         MBP                                  " & vbNewLine _
                                            & "ON  MBP.BP_CD             =    CST.TCUST_BPCD                       " & vbNewLine
    'END YANAI 要望番号824

#If True Then  'UPD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
    ''' <summary>
    ''' 荷主マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM_BASE As String = " ) BASE                                           " & vbNewLine _
                                                        & "WHERE                                             " & vbNewLine _
                                                        & " ((@INTEG_WEB_FLG = '')                            " & vbNewLine _
                                                        & "       OR                                         " & vbNewLine _
                                                        & "  (@INTEG_WEB_FLG = '01'                           " & vbNewLine _
                                                        & "   AND (WEB_COMP_CD_L <> ''                       " & vbNewLine _
                                                        & "    OR  WEB_COMP_CD_LM <> ''                      " & vbNewLine _
                                                        & "    OR  WEB_COMP_CD_LMS <> ''                     " & vbNewLine _
                                                        & "    OR  WEB_COMP_CD_LMSSS <> ''))                 " & vbNewLine _
                                                        & "      OR                                          " & vbNewLine _
                                                        & "  (@INTEG_WEB_FLG = '00'                           " & vbNewLine _
                                                        & "   AND (WEB_COMP_CD_L =  ''                       " & vbNewLine _
                                                        & "   AND  WEB_COMP_CD_LM = ''                       " & vbNewLine _
                                                        & "   AND  WEB_COMP_CD_LMS = ''                      " & vbNewLine _
                                                        & "   AND  WEB_COMP_CD_LMSSS = '')) 	             " & vbNewLine _
                                                        & " )	                                             " & vbNewLine
#End If


    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                   " & vbNewLine _
                                         & "    CST.NRS_BR_CD          " & vbNewLine _
                                         & "   ,CST.CUST_CD_L          " & vbNewLine _
                                         & "   ,CST.CUST_CD_M          " & vbNewLine _
                                         & "   ,CST.CUST_CD_S          " & vbNewLine _
                                         & "   ,CST.CUST_CD_SS         " & vbNewLine


    ''' <summary>
    ''' 並び順BASE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_BASE As String = "ORDER BY                   " & vbNewLine _
                                         & "    BASE.NRS_BR_CD          " & vbNewLine _
                                         & "   ,BASE.CUST_CD_L          " & vbNewLine _
                                         & "   ,BASE.CUST_CD_M          " & vbNewLine _
                                         & "   ,BASE.CUST_CD_S          " & vbNewLine _
                                         & "   ,BASE.CUST_CD_SS         " & vbNewLine


    ''' <summary>
    ''' 契約通貨コンボ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COMBO_ITEM_CURR_CD As String = "SELECT CURR_CD AS ITEM_CURR_CD      " & vbNewLine _
                                         & "    FROM COM_DB..M_CURR                       " & vbNewLine _
                                         & "   WHERE SYS_DEL_FLG = '0'                    " & vbNewLine _
                                         & "     AND      UP_FLG = '00000'                " & vbNewLine _
                                         & "   AND  BASE_CURR_CD = (SELECT                " & vbNewLine _
                                         & "   CASE WHEN BASE_CURR_CD IS NULL             " & vbNewLine _
                                         & "          OR BASE_CURR_CD = '' THEN 'JPY'     " & vbNewLine _
                                         & "   ELSE BASE_CURR_CD END CURR_CD              " & vbNewLine _
                                         & "   FROM LM_MST..M_NRS_BR MNRS                 " & vbNewLine _
                                         & "   LEFT JOIN LM_MST..S_USER SU                " & vbNewLine _
                                         & "   ON MNRS.NRS_BR_CD = SU.NRS_BR_CD           " & vbNewLine _
                                         & "   WHERE SU.USER_CD = @USER_CD)               " & vbNewLine
#End Region

#Region "荷主別帳票マスタ"

    ''' <summary>
    ''' 荷主別帳票マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRT_DATA As String = " SELECT                                          " & vbNewLine _
                                                & "     PRT.NRS_BR_CD             AS    NRS_BR_CD   " & vbNewLine _
                                                & "    ,PRT.CUST_CD_L             AS    CUST_CD_L   " & vbNewLine _
                                                & "    ,PRT.CUST_CD_M             AS    CUST_CD_M   " & vbNewLine _
                                                & "    ,PRT.CUST_CD_S             AS    CUST_CD_S   " & vbNewLine _
                                                & "    ,PRT.PTN_ID                AS    PTN_ID      " & vbNewLine _
                                                & "    ,PRT.PTN_CD                AS    PTN_CD      " & vbNewLine _
                                                & "FROM                                             " & vbNewLine _
                                                & "    $LM_MST$..M_CUST_RPT    PRT                  " & vbNewLine

#End Region

    '要望番号:349 yamanaka 2012.07.11 Start
#Region "荷主明細マスタ"

    ''' <summary>
    ''' 荷主明細マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DETAIL_DATA As String = "SELECT                                                                                           " & vbNewLine _
                                                   & "     DETAIL.NRS_BR_CD    AS NRS_BR_CD                                                            " & vbNewLine _
                                                   & "    ,DETAIL.CUST_CD      AS CUST_CD                                                              " & vbNewLine _
                                                   & "    ,DETAIL.CUST_CD_EDA  AS CUST_CD_EDA                                                          " & vbNewLine _
                                                   & "    ,DETAIL.CUST_CLASS   AS CUST_CLASS                                                           " & vbNewLine _
                                                   & "    ,KBN1.KBN_NM1        AS CUST_CLASS_NM                                                        " & vbNewLine _
                                                   & "    ,DETAIL.SUB_KB       AS SUB_KB                                                               " & vbNewLine _
                                                   & "    ,KBN2.KBN_NM1        AS SUB_KB_NM                                                            " & vbNewLine _
                                                   & "    ,DETAIL.SET_NAIYO    AS SET_NAIYO                                                            " & vbNewLine _
                                                   & "    ,DETAIL.SET_NAIYO_2  AS SET_NAIYO_2                                                          " & vbNewLine _
                                                   & "    ,DETAIL.SET_NAIYO_3  AS SET_NAIYO_3                                                          " & vbNewLine _
                                                   & "    ,DETAIL.REMARK       AS REMARK                                                               " & vbNewLine _
                                                   & "    ,'1'                 AS UPD_FLG                                                              " & vbNewLine _
                                                   & "    ,DETAIL.SYS_DEL_FLG  AS SYS_DEL_FLG                                                          " & vbNewLine _
                                                   & "FROM                                                                                             " & vbNewLine _
                                                   & "          $LM_MST$..M_CUST_DETAILS DETAIL                                                        " & vbNewLine _
                                                   & "LEFT JOIN                                                                                        " & vbNewLine _
                                                   & "           $LM_MST$..M_CUST CUST                                                                 " & vbNewLine _
                                                   & "       ON  DETAIL.NRS_BR_CD = CUST.NRS_BR_CD                                                     " & vbNewLine _
                                                   & "      AND (DETAIL.CUST_CD  = CUST.CUST_CD_L                                                      " & vbNewLine _
                                                   & "       OR  DETAIL.CUST_CD  = CUST.CUST_CD_L + CUST.CUST_CD_M                                     " & vbNewLine _
                                                   & "       OR  DETAIL.CUST_CD  = CUST.CUST_CD_L + CUST.CUST_CD_M + CUST.CUST_CD_S                    " & vbNewLine _
                                                   & "       OR  DETAIL.CUST_CD  = CUST.CUST_CD_L + CUST.CUST_CD_M + CUST.CUST_CD_S + CUST.CUST_CD_SS) " & vbNewLine _
                                                   & "LEFT JOIN                                                                                        " & vbNewLine _
                                                   & "           $LM_MST$..Z_KBN KBN1                                                                  " & vbNewLine _
                                                   & "       ON  KBN1.KBN_CD        = DETAIL.CUST_CLASS                                                " & vbNewLine _
                                                   & "      AND  KBN1.KBN_GROUP_CD  ='C002'                                                            " & vbNewLine _
                                                   & "      AND  KBN1.SYS_DEL_FLG   = '0'                                                              " & vbNewLine _
                                                   & "LEFT JOIN                                                                                        " & vbNewLine _
                                                   & "           $LM_MST$..Z_KBN KBN2                                                                  " & vbNewLine _
                                                   & "       ON  KBN2.KBN_CD        = DETAIL.SUB_KB                                                    " & vbNewLine _
                                                   & "      AND  KBN2.KBN_GROUP_CD  ='Y008'                                                            " & vbNewLine _
                                                   & "      AND  KBN2.SYS_DEL_FLG   = '0'                                                              " & vbNewLine

    Private Const SQL_SELECT_DETAIL_GROUP_BY As String = "GROUP BY                                                                                     " & vbNewLine _
                                                       & "           DETAIL.NRS_BR_CD                                                                  " & vbNewLine _
                                                       & "          ,DETAIL.CUST_CD                                                                    " & vbNewLine _
                                                       & "          ,DETAIL.CUST_CD_EDA                                                                " & vbNewLine _
                                                       & "          ,DETAIL.CUST_CLASS                                                                 " & vbNewLine _
                                                       & "          ,KBN1.KBN_NM1                                                                      " & vbNewLine _
                                                       & "          ,DETAIL.SUB_KB                                                                     " & vbNewLine _
                                                       & "          ,KBN2.KBN_NM1                                                                      " & vbNewLine _
                                                       & "          ,DETAIL.SET_NAIYO                                                                  " & vbNewLine _
                                                       & "          ,DETAIL.SET_NAIYO_2                                                                " & vbNewLine _
                                                       & "          ,DETAIL.SET_NAIYO_3                                                                " & vbNewLine _
                                                       & "          ,DETAIL.REMARK                                                                     " & vbNewLine _
                                                       & "          ,DETAIL.SYS_DEL_FLG                                                                " & vbNewLine _
                                                       & "ORDER BY                                                                                     " & vbNewLine _
                                                       & "           DETAIL.NRS_BR_CD                                                                  " & vbNewLine _
                                                       & "          ,DETAIL.CUST_CD                                                                    " & vbNewLine _
                                                       & "          ,DETAIL.CUST_CD_EDA                                                                " & vbNewLine _
                                                       & "          ,DETAIL.SUB_KB                                                                     " & vbNewLine

#End Region
    '要望番号:349 yamanaka 2012.07.11 End

#Region "運賃タリフセットマスタ"

    ''' <summary>
    ''' 運賃タリフセットマスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TARIFF_DATA As String = " SELECT                                                                                  " & vbNewLine _
                                                   & "     TRS.NRS_BR_CD            AS    NRS_BR_CD                                            " & vbNewLine _
                                                   & "    ,TRS.CUST_CD_L            AS    CUST_CD_L                                            " & vbNewLine _
                                                   & "    ,TRS.CUST_CD_M            AS    CUST_CD_M                                            " & vbNewLine _
                                                   & "    ,TRS.SET_MST_CD           AS    SET_MST_CD                                           " & vbNewLine _
                                                   & "    ,TRS.DEST_CD              AS    DEST_CD                                              " & vbNewLine _
                                                   & "    ,TRS.SET_KB               AS    SET_KB                                               " & vbNewLine _
                                                   & "    ,TRS.TARIFF_BUNRUI_KB     AS    TARIFF_BUNRUI_KB                                     " & vbNewLine _
                                                   & "    ,TRS.UNCHIN_TARIFF_CD1    AS    UNCHIN_TARIFF_CD1                                    " & vbNewLine _
                                                   & "    ,TRF1.UNCHIN_TARIFF_REM   AS    UNCHIN_TARIFF_NM1                                    " & vbNewLine _
                                                   & "    ,TRS.UNCHIN_TARIFF_CD2    AS    UNCHIN_TARIFF_CD2                                    " & vbNewLine _
                                                   & "    ,TRF2.UNCHIN_TARIFF_REM   AS    UNCHIN_TARIFF_NM2                                    " & vbNewLine _
                                                   & "    ,TRS.EXTC_TARIFF_CD       AS    EXTC_TARIFF_CD                                       " & vbNewLine _
                                                   & "    ,EXT.EXTC_TARIFF_REM      AS    EXTC_TARIFF_NM                                       " & vbNewLine _
                                                   & "    ,TRS.YOKO_TARIFF_CD       AS    YOKO_TARIFF_CD                                       " & vbNewLine _
                                                   & "    ,YKO.YOKO_REM             AS    YOKO_TARIFF_NM                                       " & vbNewLine _
                                                   & "    ,TRS.SYS_UPD_DATE         AS    SYS_UPD_DATE                                         " & vbNewLine _
                                                   & "    ,TRS.SYS_UPD_TIME         AS    SYS_UPD_TIME                                         " & vbNewLine _
                                                   & "FROM                                                                                     " & vbNewLine _
                                                   & "    $LM_MST$..M_UNCHIN_TARIFF_SET    TRS                                                 " & vbNewLine _
                                                   & "LEFT JOIN $LM_MST$..M_UNCHIN_TARIFF   TRF1                                               " & vbNewLine _
                                                   & "ON   TRF1.NRS_BR_CD          =  TRS.NRS_BR_CD                                            " & vbNewLine _
                                                   & "AND  TRF1.UNCHIN_TARIFF_CD   =  TRS.UNCHIN_TARIFF_CD1                                    " & vbNewLine _
                                                   & "AND  TRF1.SYS_DEL_FLG        =  '0'                                                      " & vbNewLine _
                                                   & "AND  TRF1.UNCHIN_TARIFF_REM  IS NOT NULL                                                 " & vbNewLine _
                                                   & "AND  TRF1.UNCHIN_TARIFF_REM  <>  ''                                                      " & vbNewLine _
                                                   & "AND  TRF1.STR_DATE           =  (SELECT                                                  " & vbNewLine _
                                                   & "                                    MAX(DAYTBL.STR_DATE)                                 " & vbNewLine _
                                                   & "                                 FROM $LM_MST$..M_UNCHIN_TARIFF  DAYTBL                  " & vbNewLine _
                                                   & "                                 WHERE                                                   " & vbNewLine _
                                                   & "                                      DAYTBL.NRS_BR_CD          =  TRS.NRS_BR_CD         " & vbNewLine _
                                                   & "                                 AND  DAYTBL.UNCHIN_TARIFF_CD   =  TRS.UNCHIN_TARIFF_CD1 " & vbNewLine _
                                                   & "                                 AND  DAYTBL.SYS_DEL_FLG        =  '0'                   " & vbNewLine _
                                                   & "                                 AND  DAYTBL.STR_DATE           <= @SYSTEM_DATE          " & vbNewLine _
                                                   & "                                 AND  DAYTBL.UNCHIN_TARIFF_REM  IS  NOT NULL             " & vbNewLine _
                                                   & "                                 AND  DAYTBL.UNCHIN_TARIFF_REM  <>  '')                  " & vbNewLine _
                                                   & "LEFT JOIN $LM_MST$..M_UNCHIN_TARIFF   TRF2                                               " & vbNewLine _
                                                   & "ON   TRF2.NRS_BR_CD          =  TRS.NRS_BR_CD                                            " & vbNewLine _
                                                   & "AND  TRF2.UNCHIN_TARIFF_CD   =  TRS.UNCHIN_TARIFF_CD2                                    " & vbNewLine _
                                                   & "AND  TRF2.SYS_DEL_FLG        =  '0'                                                      " & vbNewLine _
                                                   & "AND  TRF2.UNCHIN_TARIFF_REM  IS NOT NULL                                                 " & vbNewLine _
                                                   & "AND  TRF2.UNCHIN_TARIFF_REM  <>  ''                                                      " & vbNewLine _
                                                   & "AND  TRF2.STR_DATE           =  (SELECT                                                  " & vbNewLine _
                                                   & "                                    MAX(DAYTBL.STR_DATE)                                 " & vbNewLine _
                                                   & "                                 FROM $LM_MST$..M_UNCHIN_TARIFF  DAYTBL                  " & vbNewLine _
                                                   & "                                 WHERE                                                   " & vbNewLine _
                                                   & "                                      DAYTBL.NRS_BR_CD          =  TRS.NRS_BR_CD         " & vbNewLine _
                                                   & "                                 AND  DAYTBL.UNCHIN_TARIFF_CD   =  TRS.UNCHIN_TARIFF_CD2 " & vbNewLine _
                                                   & "                                 AND  DAYTBL.SYS_DEL_FLG        =  '0'                   " & vbNewLine _
                                                   & "                                 AND  DAYTBL.STR_DATE           <= @SYSTEM_DATE          " & vbNewLine _
                                                   & "                                 AND  DAYTBL.UNCHIN_TARIFF_REM  IS  NOT NULL             " & vbNewLine _
                                                   & "                                 AND  DAYTBL.UNCHIN_TARIFF_REM  <>  '')                  " & vbNewLine _
                                                   & "LEFT JOIN $LM_MST$..M_EXTC_UNCHIN      EXT                                               " & vbNewLine _
                                                   & "ON  EXT.NRS_BR_CD            =    TRS.NRS_BR_CD                                          " & vbNewLine _
                                                   & "AND EXT.EXTC_TARIFF_CD       =    TRS.EXTC_TARIFF_CD                                     " & vbNewLine _
                                                   & "AND EXT.JIS_CD               =    '0000000'                                              " & vbNewLine _
                                                   & "AND EXT.SYS_DEL_FLG          =    '0'                                                    " & vbNewLine _
                                                   & "LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD      YKO                                            " & vbNewLine _
                                                   & "ON  YKO.NRS_BR_CD            =    TRS.NRS_BR_CD                                          " & vbNewLine _
                                                   & "AND YKO.YOKO_TARIFF_CD       =    TRS.YOKO_TARIFF_CD                                     " & vbNewLine _
                                                   & "AND YKO.SYS_DEL_FLG          =    '0'                                                    " & vbNewLine


#End Region

#End Region

#Region "削除/復活処理 SQL"

#Region "チェック SQL"

    ''' <summary>
    ''' 子供データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_KODOMO_CUST As String = "SELECT                                                               " & vbNewLine _
                                                  & "     COUNT(CUST_CD_L)     AS    SELECT_CNT                           " & vbNewLine _
                                                  & "FROM                                                                 " & vbNewLine _
                                                  & "     $LM_MST$..M_CUST                                                " & vbNewLine _
                                                  & "WHERE                                                                " & vbNewLine _
                                                  & "       NRS_BR_CD          =    @NRS_BR_CD                            " & vbNewLine _
                                                  & "AND    CUST_CD_L          =    @CUST_CD_L                            " & vbNewLine _
                                                  & "AND    SYS_DEL_FLG        =    '0'                                   " & vbNewLine _
                                                  & "AND    (CUST_CD_M <> '00' OR CUST_CD_S <> '00' OR CUST_CD_SS <> '00')" & vbNewLine

    ''' <summary>
    ''' 親データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_OYA_CUST As String = "SELECT                                          " & vbNewLine _
                                                & "     COUNT(CUST_CD_L)     AS    SELECT_CNT     " & vbNewLine _
                                                & "FROM                                           " & vbNewLine _
                                                & "     $LM_MST$..M_CUST                          " & vbNewLine _
                                                & "WHERE                                          " & vbNewLine _
                                                & "       NRS_BR_CD          =    @NRS_BR_CD      " & vbNewLine _
                                                & "AND    CUST_CD_L          =    @CUST_CD_L      " & vbNewLine _
                                                & "AND    CUST_CD_M          =    '00'            " & vbNewLine _
                                                & "AND    CUST_CD_S          =    '00'            " & vbNewLine _
                                                & "AND    CUST_CD_SS         =    '00'            " & vbNewLine _
                                                & "AND    SYS_DEL_FLG        =    '0'             " & vbNewLine

    ''' <summary>
    ''' 運賃タリフセットマスタ更新可否チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_TARIFFM_UPDDEL_CHK As String = "SELECT                                                         " & vbNewLine _
                                                   & "       COUNT(CUST_CD_L) AS SELECT_CNT                          " & vbNewLine _
                                                   & "FROM                                                           " & vbNewLine _
                                                   & "     $LM_MST$..M_CUST                                          " & vbNewLine _
                                                   & "WHERE                                                          " & vbNewLine _
                                                   & "       NRS_BR_CD          =    @NRS_BR_CD                      " & vbNewLine _
                                                   & "AND    CUST_CD_L          =    @CUST_CD_L                      " & vbNewLine _
                                                   & "AND    CUST_CD_M          =    @CUST_CD_M                      " & vbNewLine _
                                                   & "AND    SYS_DEL_FLG        =    '0'                             " & vbNewLine _
                                                   & "AND    (CUST_CD_S <> @CUST_CD_S OR CUST_CD_SS <> @CUST_CD_SS) " & vbNewLine

    ''' <summary>
    ''' 荷主別帳票マスタ更新可否チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CUST_PRT_UPDDEL_CHK As String = "SELECT                                                         " & vbNewLine _
                                                    & "       COUNT(CUST_CD_L) AS SELECT_CNT                          " & vbNewLine _
                                                    & "FROM                                                           " & vbNewLine _
                                                    & "     $LM_MST$..M_CUST                                          " & vbNewLine _
                                                    & "WHERE                                                          " & vbNewLine _
                                                    & "       NRS_BR_CD          =    @NRS_BR_CD                      " & vbNewLine _
                                                    & "AND    CUST_CD_L          =    @CUST_CD_L                      " & vbNewLine _
                                                    & "AND    CUST_CD_M          =    @CUST_CD_M                      " & vbNewLine _
                                                    & "AND    CUST_CD_S          =    @CUST_CD_S                      " & vbNewLine _
                                                    & "AND    SYS_DEL_FLG        =    '0'                             " & vbNewLine _
                                                    & "AND    CUST_CD_SS         <>   @CUST_CD_SS                     " & vbNewLine

    '要望番号:349 yamanaka 2012.07.19 Start
    ''' <summary>
    ''' 荷主明細マスタ更新可否チェック(大)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CUST_DETAIL_UPDDEL_CHK_L As String = "SELECT                                    " & vbNewLine _
                                                         & "      COUNT(CUST_CD_L) AS SELECT_CNT      " & vbNewLine _
                                                         & "FROM                                      " & vbNewLine _
                                                         & "      $LM_MST$..M_CUST                    " & vbNewLine _
                                                         & "WHERE                                     " & vbNewLine _
                                                         & "      NRS_BR_CD          =    @NRS_BR_CD  " & vbNewLine _
                                                         & " AND  CUST_CD_L          =    @CUST_CD_L  " & vbNewLine _
                                                         & " AND  SYS_DEL_FLG        =    '0'         " & vbNewLine

    ''' <summary>
    ''' 荷主明細マスタ更新可否チェック(中)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CUST_DETAIL_UPDDEL_CHK_M As String = "SELECT                                    " & vbNewLine _
                                                         & "      COUNT(CUST_CD_L) AS SELECT_CNT      " & vbNewLine _
                                                         & "FROM                                      " & vbNewLine _
                                                         & "      $LM_MST$..M_CUST                    " & vbNewLine _
                                                         & "WHERE                                     " & vbNewLine _
                                                         & "      NRS_BR_CD          =    @NRS_BR_CD  " & vbNewLine _
                                                         & " AND  CUST_CD_L          =    @CUST_CD_L  " & vbNewLine _
                                                         & " AND  CUST_CD_M          =    @CUST_CD_M  " & vbNewLine _
                                                         & " AND  SYS_DEL_FLG        =    '0'         " & vbNewLine

    ''' <summary>
    ''' 荷主明細マスタ更新可否チェック(小)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CUST_DETAIL_UPDDEL_CHK_S As String = "SELECT                                    " & vbNewLine _
                                                         & "      COUNT(CUST_CD_L) AS SELECT_CNT      " & vbNewLine _
                                                         & "FROM                                      " & vbNewLine _
                                                         & "      $LM_MST$..M_CUST                    " & vbNewLine _
                                                         & "WHERE                                     " & vbNewLine _
                                                         & "      NRS_BR_CD          =    @NRS_BR_CD  " & vbNewLine _
                                                         & " AND  CUST_CD_L          =    @CUST_CD_L  " & vbNewLine _
                                                         & " AND  CUST_CD_M          =    @CUST_CD_M  " & vbNewLine _
                                                         & " AND  CUST_CD_S          =    @CUST_CD_S  " & vbNewLine _
                                                         & " AND  SYS_DEL_FLG        =    '0'         " & vbNewLine

    '要望番号:349 yamanaka 2012.07.19 End

    'START YANAI 要望番号739
    '''' <summary>
    '''' 在庫データ存在チェック
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_EXIST_ZAIT As String = "SELECT COUNT(GOODS.NRS_BR_CD) AS SELECT_CNT                                                         " & vbNewLine _
    '                                       & "FROM $LM_MST$..M_GOODS GOODS                                                                          " & vbNewLine _
    '                                       & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                                                  " & vbNewLine _
    '                                       & "ON  ZAITRS.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
    '                                       & "AND ZAITRS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                        " & vbNewLine _
    '                                       & "AND ZAITRS.CUST_CD_L = GOODS.CUST_CD_L                                                              " & vbNewLine _
    '                                       & "AND ZAITRS.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
    '                                       & "WHERE                                                                                               " & vbNewLine _
    '                                       & "GOODS.NRS_BR_CD = @NRS_BR_CD                                                                        " & vbNewLine _
    '                                       & "AND GOODS.CUST_CD_L = @CUST_CD_L                                                                    " & vbNewLine _
    '                                       & "AND GOODS.CUST_CD_M = @CUST_CD_M                                                                    " & vbNewLine _
    '                                       & "AND GOODS.CUST_CD_S = @CUST_CD_S                                                                    " & vbNewLine _
    '                                       & "AND GOODS.CUST_CD_SS = @CUST_CD_SS                                                                  " & vbNewLine
    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_ZAIT As String = "SELECT COUNT(GOODS.NRS_BR_CD) AS SELECT_CNT                                                         " & vbNewLine _
                                           & "FROM $LM_MST$..M_GOODS GOODS                                                                        " & vbNewLine _
                                           & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                                                " & vbNewLine _
                                           & "ON  ZAITRS.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
                                           & "AND ZAITRS.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                        " & vbNewLine _
                                           & "AND ZAITRS.CUST_CD_L = GOODS.CUST_CD_L                                                              " & vbNewLine _
                                           & "AND ZAITRS.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                           & "WHERE                                                                                               " & vbNewLine _
                                           & "GOODS.NRS_BR_CD = @NRS_BR_CD                                                                        " & vbNewLine _
                                           & "AND GOODS.CUST_CD_L = @CUST_CD_L                                                                    " & vbNewLine _
                                           & "AND GOODS.CUST_CD_M = @CUST_CD_M                                                                    " & vbNewLine _
                                           & "AND GOODS.CUST_CD_S = @CUST_CD_S                                                                    " & vbNewLine _
                                           & "AND GOODS.CUST_CD_SS = @CUST_CD_SS                                                                  " & vbNewLine
    'END YANAI 要望番号739

    'Start kobayashi 要望番号2034
    ''' <summary>
    ''' 退避データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_IS_ESCAPED_CUST As String = "SELECT                                                               " & vbNewLine _
                                                  & "     COUNT(CUST_CD_L)     AS    SELECT_CNT                           " & vbNewLine _
                                                  & "FROM                                                                 " & vbNewLine _
                                                  & "     $LM_MST$..M_CUST                                                " & vbNewLine _
                                                  & "WHERE                                                                " & vbNewLine _
                                                  & "       NRS_BR_CD          =    @NRS_BR_CD                            " & vbNewLine _
                                                  & "AND    CUST_CD_L          =    @CUST_CD_L                            " & vbNewLine _
                                                  & "AND    SYS_DEL_FLG        =    '1'                                   " & vbNewLine _
                                                  & "AND    BACKUP_FLG        IN    ('01','02')                           " & vbNewLine
    'End kobayashi 要望番号2034

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_VAR_STRAGE As String = "" _
                              & " SELECT                                    " & vbNewLine _
                              & "    SEI.SEIQTO_CD                          " & vbNewLine _
                              & "   ,SEI.SEIQTO_NM                          " & vbNewLine _
                              & " FROM                                      " & vbNewLine _
                              & "   LM_MST..M_CUST AS CST                   " & vbNewLine _
                              & " LEFT JOIN                                 " & vbNewLine _
                              & "   LM_MST..M_SEIQTO AS SEI                 " & vbNewLine _
                              & "   ON  SEI.NRS_BR_CD = CST.NRS_BR_CD       " & vbNewLine _
                              & "   AND SEI.SEIQTO_CD = CST.HOKAN_SEIQTO_CD " & vbNewLine _
                              & "   AND SEI.SYS_DEL_FLG = '0'               " & vbNewLine _
                              & " WHERE                                     " & vbNewLine _
                              & "   CST.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                              & "   AND CST.CUST_CD_L = @CUST_CD_L          " & vbNewLine _
                              & "   AND CST.CUST_CD_M = @CUST_CD_M          " & vbNewLine _
                              & "   AND CST.SYS_DEL_FLG = '0'               " & vbNewLine _
                              & "   AND SEI.VAR_STRAGE_FLG = '1'            " & vbNewLine



#End Region

    ''' <summary>
    ''' 荷主マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_CUST_M As String = "UPDATE                                                " & vbNewLine _
                                               & "    $LM_MST$..M_CUST                                  " & vbNewLine _
                                               & "SET                                                   " & vbNewLine _
                                               & "      SYS_UPD_DATE            =    @SYS_UPD_DATE      " & vbNewLine _
                                               & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME      " & vbNewLine _
                                               & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID      " & vbNewLine _
                                               & "     ,SYS_UPD_USER            =    @SYS_UPD_USER      " & vbNewLine _
                                               & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG       " & vbNewLine _
                                               & "WHERE                                                 " & vbNewLine _
                                               & "      NRS_BR_CD               =    @NRS_BR_CD         " & vbNewLine _
                                               & "AND  CUST_CD_L               =    @CUST_CD_L          " & vbNewLine _
                                               & "AND  CUST_CD_M               =    @CUST_CD_M          " & vbNewLine _
                                               & "AND  CUST_CD_S               =    @CUST_CD_S          " & vbNewLine _
                                               & "AND  CUST_CD_SS              =    @CUST_CD_SS         " & vbNewLine

    'START YANAI 要望番号457
    '''' <summary>
    '''' 運賃タリフセットマスタ更新SQL(論理削除)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPD_DEL_TARIFFSET_M As String = "UPDATE                                                " & vbNewLine _
    '                                                & "    $LM_MST$..M_UNCHIN_TARIFF_SET                     " & vbNewLine _
    '                                                & "SET                                                   " & vbNewLine _
    '                                                & "      SYS_UPD_DATE            =    @SYS_UPD_DATE      " & vbNewLine _
    '                                                & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME      " & vbNewLine _
    '                                                & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID      " & vbNewLine _
    '                                                & "     ,SYS_UPD_USER            =    @SYS_UPD_USER      " & vbNewLine _
    '                                                & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG       " & vbNewLine _
    '                                                & "WHERE                                                 " & vbNewLine _
    '                                                & "      NRS_BR_CD               =    @NRS_BR_CD         " & vbNewLine _
    '                                                & "AND  CUST_CD_L                =    @CUST_CD_L         " & vbNewLine _
    '                                                & "AND  CUST_CD_M                =    @CUST_CD_M         " & vbNewLine _
    '                                                & "AND  SET_MST_CD               =    @SET_MST_CD        " & vbNewLine _
    '                                                & "AND  DEST_CD                  =    ''                 " & vbNewLine
    ''' <summary>
    ''' 運賃タリフセットマスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_TARIFFSET_M As String = "UPDATE                                                " & vbNewLine _
                                                    & "    $LM_MST$..M_UNCHIN_TARIFF_SET                     " & vbNewLine _
                                                    & "SET                                                   " & vbNewLine _
                                                    & "      SYS_UPD_DATE            =    @SYS_UPD_DATE      " & vbNewLine _
                                                    & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME      " & vbNewLine _
                                                    & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID      " & vbNewLine _
                                                    & "     ,SYS_UPD_USER            =    @SYS_UPD_USER      " & vbNewLine _
                                                    & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG       " & vbNewLine _
                                                    & "WHERE                                                 " & vbNewLine _
                                                    & "      NRS_BR_CD               =    @NRS_BR_CD         " & vbNewLine _
                                                    & "AND  CUST_CD_L                =    @CUST_CD_L         " & vbNewLine _
                                                    & "AND  CUST_CD_M                =    @CUST_CD_M         " & vbNewLine _
                                                    & "AND  SET_MST_CD               =    @SET_MST_CD        " & vbNewLine _
    'END YANAI 要望番号457

    ''' <summary>
    ''' 荷主別帳票マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_CUSTPRT_M As String = "UPDATE                                                " & vbNewLine _
                                                   & "    $LM_MST$..M_CUST_RPT                              " & vbNewLine _
                                                   & "SET                                                   " & vbNewLine _
                                                   & "       SYS_UPD_DATE            =    @SYS_UPD_DATE      " & vbNewLine _
                                                   & "      ,SYS_UPD_TIME            =    @SYS_UPD_TIME      " & vbNewLine _
                                                   & "      ,SYS_UPD_PGID            =    @SYS_UPD_PGID      " & vbNewLine _
                                                   & "      ,SYS_UPD_USER            =    @SYS_UPD_USER      " & vbNewLine _
                                                   & "      ,SYS_DEL_FLG             =    @SYS_DEL_FLG       " & vbNewLine _
                                                   & "WHERE                                                 " & vbNewLine _
                                                   & "      NRS_BR_CD               =    @NRS_BR_CD         " & vbNewLine _
                                                   & "AND   CUST_CD_L                =    @CUST_CD_L         " & vbNewLine _
                                                   & "AND   CUST_CD_M                =    @CUST_CD_M         " & vbNewLine _
                                                   & "AND   CUST_CD_S                =    @CUST_CD_S        " & vbNewLine

    '要望番号:349 yamanaka 2012.07.19 Start
    ''' <summary>
    ''' 荷主明細マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_CUSTDETAIL_M As String = "UPDATE                                        " & vbNewLine _
                                                     & "      $LM_MST$..M_CUST_DETAILS                " & vbNewLine _
                                                     & "SET                                           " & vbNewLine _
                                                     & "       SYS_UPD_DATE        =    @SYS_UPD_DATE " & vbNewLine _
                                                     & "      ,SYS_UPD_TIME        =    @SYS_UPD_TIME " & vbNewLine _
                                                     & "      ,SYS_UPD_PGID        =    @SYS_UPD_PGID " & vbNewLine _
                                                     & "      ,SYS_UPD_USER        =    @SYS_UPD_USER " & vbNewLine _
                                                     & "      ,SYS_DEL_FLG         =    @SYS_DEL_FLG  " & vbNewLine _
                                                     & "WHERE                                         " & vbNewLine _
                                                     & "       NRS_BR_CD           =    @NRS_BR_CD    " & vbNewLine _
                                                     & " AND   CUST_CD             =    @CUST_CD      " & vbNewLine

    '要望番号:349 yamanaka 2012.07.19 End

#End Region

#Region "保存処理 SQL"

#Region "チェック"

    ''' <summary>
    ''' 郵便番号マスタ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIST_ZIPM As String = "SELECT                                             " & vbNewLine _
                                             & "       COUNT(ZIP_NO)      AS    SELECT_CNT       " & vbNewLine _
                                             & "FROM                                             " & vbNewLine _
                                             & "     $LM_MST$..M_ZIP                             " & vbNewLine _
                                             & "WHERE                                            " & vbNewLine _
                                             & "       ZIP_NO       =    @ZIP_NO                 " & vbNewLine _
                                             & "AND    SYS_DEL_FLG  =    '0'                     " & vbNewLine


    ''' <summary>
    ''' 荷主マスタ重複チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_REPEAT_CUSTM As String = "SELECT                                            " & vbNewLine _
                                              & "       COUNT(CUST_CD_L)     AS    SELECT_CNT     " & vbNewLine _
                                              & "FROM                                             " & vbNewLine _
                                              & "     $LM_MST$..M_CUST                            " & vbNewLine _
                                              & "WHERE                                            " & vbNewLine _
                                              & "       NRS_BR_CD          =    @NRS_BR_CD        " & vbNewLine _
                                              & "AND    CUST_CD_L          =    @CUST_CD_L        " & vbNewLine _
                                              & "AND    CUST_CD_M          =    @CUST_CD_M        " & vbNewLine _
                                              & "AND    CUST_CD_S          =    @CUST_CD_S        " & vbNewLine _
                                              & "AND    CUST_CD_SS         =    @CUST_CD_SS       " & vbNewLine


    ''' <summary>
    ''' 荷主コード(中)の採番用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SET_CUST_CD_M As String = "SELECT                                           " & vbNewLine _
                                              & "       MAX(CUST_CD_M)   AS    SAIBAN             " & vbNewLine _
                                              & "FROM                                             " & vbNewLine _
                                              & "     $LM_MST$..M_CUST                            " & vbNewLine _
                                              & "WHERE                                            " & vbNewLine _
                                              & "       NRS_BR_CD          =    @NRS_BR_CD        " & vbNewLine _
                                              & "AND    CUST_CD_L          =    @CUST_CD_L        " & vbNewLine


    ''' <summary>
    ''' 荷主コード(小)の採番用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SET_CUST_CD_S As String = "SELECT                                           " & vbNewLine _
                                              & "       MAX(CUST_CD_S)   AS    SAIBAN             " & vbNewLine _
                                              & "FROM                                             " & vbNewLine _
                                              & "     $LM_MST$..M_CUST                            " & vbNewLine _
                                              & "WHERE                                            " & vbNewLine _
                                              & "       NRS_BR_CD          =    @NRS_BR_CD        " & vbNewLine _
                                              & "AND    CUST_CD_L          =    @CUST_CD_L        " & vbNewLine _
                                              & "AND    CUST_CD_M          =    @CUST_CD_M        " & vbNewLine

    ''' <summary>
    ''' 荷主コード(極小)の採番用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SET_CUST_CD_SS As String = "SELECT                                           " & vbNewLine _
                                               & "       MAX(CUST_CD_SS)   AS    SAIBAN            " & vbNewLine _
                                               & "FROM                                             " & vbNewLine _
                                               & "     $LM_MST$..M_CUST                            " & vbNewLine _
                                               & "WHERE                                            " & vbNewLine _
                                               & "       NRS_BR_CD          =    @NRS_BR_CD        " & vbNewLine _
                                               & "AND    CUST_CD_L          =    @CUST_CD_L        " & vbNewLine _
                                               & "AND    CUST_CD_M          =    @CUST_CD_M        " & vbNewLine _
                                               & "AND    CUST_CD_S          =    @CUST_CD_S        " & vbNewLine


#End Region

#Region "新規登録 SQL"

    'START YANAI 要望番号824
    '''' <summary>
    '''' 荷主マスタ新規登録SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT_CUST_M As String = "INSERT INTO                        " & vbNewLine _
    '                                          & "    $LM_MST$..M_CUST               " & vbNewLine _
    '                                          & "    (                              " & vbNewLine _
    '                                          & "      NRS_BR_CD                    " & vbNewLine _
    '                                          & "     ,CUST_CD_L                    " & vbNewLine _
    '                                          & "     ,CUST_CD_M                    " & vbNewLine _
    '                                          & "     ,CUST_CD_S                    " & vbNewLine _
    '                                          & "     ,CUST_CD_SS                   " & vbNewLine _
    '                                          & "     ,CUST_OYA_CD                  " & vbNewLine _
    '                                          & "     ,CUST_NM_L                    " & vbNewLine _
    '                                          & "     ,CUST_NM_M                    " & vbNewLine _
    '                                          & "     ,CUST_NM_S                    " & vbNewLine _
    '                                          & "     ,CUST_NM_SS                   " & vbNewLine _
    '                                          & "     ,ZIP                          " & vbNewLine _
    '                                          & "     ,AD_1                         " & vbNewLine _
    '                                          & "     ,AD_2                         " & vbNewLine _
    '                                          & "     ,AD_3                         " & vbNewLine _
    '                                          & "     ,PIC                          " & vbNewLine _
    '                                          & "     ,FUKU_PIC                     " & vbNewLine _
    '                                          & "     ,TEL                          " & vbNewLine _
    '                                          & "     ,FAX                          " & vbNewLine _
    '                                          & "     ,MAIL                         " & vbNewLine _
    '                                          & "     ,SAITEI_HAN_KB                " & vbNewLine _
    '                                          & "     ,OYA_SEIQTO_CD                " & vbNewLine _
    '                                          & "     ,HOKAN_SEIQTO_CD              " & vbNewLine _
    '                                          & "     ,NIYAKU_SEIQTO_CD             " & vbNewLine _
    '                                          & "     ,UNCHIN_SEIQTO_CD             " & vbNewLine _
    '                                          & "     ,SAGYO_SEIQTO_CD              " & vbNewLine _
    '                                          & "     ,INKA_RPT_YN                  " & vbNewLine _
    '                                          & "     ,OUTKA_RPT_YN                 " & vbNewLine _
    '                                          & "     ,ZAI_RPT_YN                   " & vbNewLine _
    '                                          & "     ,UNSO_TEHAI_KB                " & vbNewLine _
    '                                          & "     ,SP_UNSO_CD                   " & vbNewLine _
    '                                          & "     ,SP_UNSO_BR_CD                " & vbNewLine _
    '                                          & "     ,BETU_KYORI_CD                " & vbNewLine _
    '                                          & "     ,TAX_KB                       " & vbNewLine _
    '                                          & "     ,HOKAN_FREE_KIKAN             " & vbNewLine _
    '                                          & "     ,SMPL_SAGYO                   " & vbNewLine _
    '                                          & "     ,HOKAN_NIYAKU_CALCULATION     " & vbNewLine _
    '                                          & "     ,HOKAN_NIYAKU_CALCULATION_OLD " & vbNewLine _
    '                                          & "     ,NEW_JOB_NO                   " & vbNewLine _
    '                                          & "     ,OLD_JOB_NO                   " & vbNewLine _
    '                                          & "     ,HOKAN_NIYAKU_KEISAN_YN       " & vbNewLine _
    '                                          & "     ,UNTIN_CALCULATION_KB         " & vbNewLine _
    '                                          & "     ,DENPYO_NM                    " & vbNewLine _
    '                                          & "     ,SOKO_CHANGE_KB               " & vbNewLine _
    '                                          & "     ,DEFAULT_SOKO_CD              " & vbNewLine _
    '                                          & "     ,PICK_LIST_KB                 " & vbNewLine _
    '                                          & "     ,SEKY_OFB_KB                  " & vbNewLine _
    '                                          & "     ,SYS_ENT_DATE                 " & vbNewLine _
    '                                          & "     ,SYS_ENT_TIME                 " & vbNewLine _
    '                                          & "     ,SYS_ENT_PGID                 " & vbNewLine _
    '                                          & "     ,SYS_ENT_USER                 " & vbNewLine _
    '                                          & "     ,SYS_UPD_DATE                 " & vbNewLine _
    '                                          & "     ,SYS_UPD_TIME                 " & vbNewLine _
    '                                          & "     ,SYS_UPD_PGID                 " & vbNewLine _
    '                                          & "     ,SYS_UPD_USER                 " & vbNewLine _
    '                                          & "     ,SYS_DEL_FLG                  " & vbNewLine _
    '                                          & "    )                              " & vbNewLine _
    '                                          & "VALUES                             " & vbNewLine _
    '                                          & "    (                              " & vbNewLine _
    '                                          & "      @NRS_BR_CD                   " & vbNewLine _
    '                                          & "     ,@CUST_CD_L                   " & vbNewLine _
    '                                          & "     ,@CUST_CD_M                   " & vbNewLine _
    '                                          & "     ,@CUST_CD_S                   " & vbNewLine _
    '                                          & "     ,@CUST_CD_SS                  " & vbNewLine _
    '                                          & "     ,@CUST_OYA_CD                 " & vbNewLine _
    '                                          & "     ,@CUST_NM_L                   " & vbNewLine _
    '                                          & "     ,@CUST_NM_M                   " & vbNewLine _
    '                                          & "     ,@CUST_NM_S                   " & vbNewLine _
    '                                          & "     ,@CUST_NM_SS                  " & vbNewLine _
    '                                          & "     ,@ZIP                         " & vbNewLine _
    '                                          & "     ,@AD_1                        " & vbNewLine _
    '                                          & "     ,@AD_2                        " & vbNewLine _
    '                                          & "     ,@AD_3                        " & vbNewLine _
    '                                          & "     ,@PIC                         " & vbNewLine _
    '                                          & "     ,@FUKU_PIC                    " & vbNewLine _
    '                                          & "     ,@TEL                         " & vbNewLine _
    '                                          & "     ,@FAX                         " & vbNewLine _
    '                                          & "     ,@MAIL                        " & vbNewLine _
    '                                          & "     ,@SAITEI_HAN_KB               " & vbNewLine _
    '                                          & "     ,@OYA_SEIQTO_CD               " & vbNewLine _
    '                                          & "     ,@HOKAN_SEIQTO_CD             " & vbNewLine _
    '                                          & "     ,@NIYAKU_SEIQTO_CD            " & vbNewLine _
    '                                          & "     ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
    '                                          & "     ,@SAGYO_SEIQTO_CD             " & vbNewLine _
    '                                          & "     ,@INKA_RPT_YN                 " & vbNewLine _
    '                                          & "     ,@OUTKA_RPT_YN                " & vbNewLine _
    '                                          & "     ,@ZAI_RPT_YN                  " & vbNewLine _
    '                                          & "     ,@UNSO_TEHAI_KB               " & vbNewLine _
    '                                          & "     ,@SP_UNSO_CD                  " & vbNewLine _
    '                                          & "     ,@SP_UNSO_BR_CD               " & vbNewLine _
    '                                          & "     ,@BETU_KYORI_CD               " & vbNewLine _
    '                                          & "     ,@TAX_KB                      " & vbNewLine _
    '                                          & "     ,@HOKAN_FREE_KIKAN            " & vbNewLine _
    '                                          & "     ,@SMPL_SAGYO                  " & vbNewLine _
    '                                          & "     ,@HOKAN_NIYAKU_CALCULATION    " & vbNewLine _
    '                                          & "     ,''                           " & vbNewLine _
    '                                          & "     ,''                           " & vbNewLine _
    '                                          & "     ,''                           " & vbNewLine _
    '                                          & "     ,@HOKAN_NIYAKU_KEISAN_YN      " & vbNewLine _
    '                                          & "     ,@UNTIN_CALCULATION_KB        " & vbNewLine _
    '                                          & "     ,@DENPYO_NM                   " & vbNewLine _
    '                                          & "     ,@SOKO_CHANGE_KB              " & vbNewLine _
    '                                          & "     ,@DEFAULT_SOKO_CD             " & vbNewLine _
    '                                          & "     ,@PICK_LIST_KB                " & vbNewLine _
    '                                          & "     ,@SEKY_OFB_KB                 " & vbNewLine _
    '                                          & "     ,@SYS_ENT_DATE                " & vbNewLine _
    '                                          & "     ,@SYS_ENT_TIME                " & vbNewLine _
    '                                          & "     ,@SYS_ENT_PGID                " & vbNewLine _
    '                                          & "     ,@SYS_ENT_USER                " & vbNewLine _
    '                                          & "     ,@SYS_UPD_DATE                " & vbNewLine _
    '                                          & "     ,@SYS_UPD_TIME                " & vbNewLine _
    '                                          & "     ,@SYS_UPD_PGID                " & vbNewLine _
    '                                          & "     ,@SYS_UPD_USER                " & vbNewLine _
    '                                          & "     ,@SYS_DEL_FLG                 " & vbNewLine _
    '                                          & "    )                              " & vbNewLine
    ''' <summary>
    ''' 荷主マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_CUST_M As String = "INSERT INTO                        " & vbNewLine _
                                              & "    $LM_MST$..M_CUST               " & vbNewLine _
                                              & "    (                              " & vbNewLine _
                                              & "      NRS_BR_CD                    " & vbNewLine _
                                              & "     ,CUST_CD_L                    " & vbNewLine _
                                              & "     ,CUST_CD_M                    " & vbNewLine _
                                              & "     ,CUST_CD_S                    " & vbNewLine _
                                              & "     ,CUST_CD_SS                   " & vbNewLine _
                                              & "     ,CUST_OYA_CD                  " & vbNewLine _
                                              & "     ,CUST_NM_L                    " & vbNewLine _
                                              & "     ,CUST_NM_M                    " & vbNewLine _
                                              & "     ,CUST_NM_S                    " & vbNewLine _
                                              & "     ,CUST_NM_SS                   " & vbNewLine _
                                              & "     ,ZIP                          " & vbNewLine _
                                              & "     ,AD_1                         " & vbNewLine _
                                              & "     ,AD_2                         " & vbNewLine _
                                              & "     ,AD_3                         " & vbNewLine _
                                              & "     ,REMARK                         " & vbNewLine _
                                              & "     ,PIC                          " & vbNewLine _
                                              & "     ,FUKU_PIC                     " & vbNewLine _
                                              & "     ,TEL                          " & vbNewLine _
                                              & "     ,FAX                          " & vbNewLine _
                                              & "     ,MAIL                         " & vbNewLine _
                                              & "     ,SAITEI_HAN_KB                " & vbNewLine _
                                              & "     ,OYA_SEIQTO_CD                " & vbNewLine _
                                              & "     ,HOKAN_SEIQTO_CD              " & vbNewLine _
                                              & "     ,NIYAKU_SEIQTO_CD             " & vbNewLine _
                                              & "     ,UNCHIN_SEIQTO_CD             " & vbNewLine _
                                              & "     ,SAGYO_SEIQTO_CD              " & vbNewLine _
                                              & "     ,INKA_RPT_YN                  " & vbNewLine _
                                              & "     ,OUTKA_RPT_YN                 " & vbNewLine _
                                              & "     ,ZAI_RPT_YN                   " & vbNewLine _
                                              & "     ,UNSO_TEHAI_KB                " & vbNewLine _
                                              & "     ,SP_UNSO_CD                   " & vbNewLine _
                                              & "     ,SP_UNSO_BR_CD                " & vbNewLine _
                                              & "     ,BETU_KYORI_CD                " & vbNewLine _
                                              & "     ,TAX_KB                       " & vbNewLine _
                                              & "     ,HOKAN_FREE_KIKAN             " & vbNewLine _
                                              & "     ,SMPL_SAGYO                   " & vbNewLine _
                                              & "     ,HOKAN_NIYAKU_CALCULATION     " & vbNewLine _
                                              & "     ,HOKAN_NIYAKU_CALCULATION_OLD " & vbNewLine _
                                              & "     ,NEW_JOB_NO                   " & vbNewLine _
                                              & "     ,OLD_JOB_NO                   " & vbNewLine _
                                              & "     ,HOKAN_NIYAKU_KEISAN_YN       " & vbNewLine _
                                              & "     ,UNTIN_CALCULATION_KB         " & vbNewLine _
                                              & "     ,DENPYO_NM                    " & vbNewLine _
                                              & "     ,SOKO_CHANGE_KB               " & vbNewLine _
                                              & "     ,DEFAULT_SOKO_CD              " & vbNewLine _
                                              & "     ,PICK_LIST_KB                 " & vbNewLine _
                                              & "     ,SEKY_OFB_KB                  " & vbNewLine _
                                              & "     ,SYS_ENT_DATE                 " & vbNewLine _
                                              & "     ,SYS_ENT_TIME                 " & vbNewLine _
                                              & "     ,SYS_ENT_PGID                 " & vbNewLine _
                                              & "     ,SYS_ENT_USER                 " & vbNewLine _
                                              & "     ,SYS_UPD_DATE                 " & vbNewLine _
                                              & "     ,SYS_UPD_TIME                 " & vbNewLine _
                                              & "     ,SYS_UPD_PGID                 " & vbNewLine _
                                              & "     ,SYS_UPD_USER                 " & vbNewLine _
                                              & "     ,SYS_DEL_FLG                  " & vbNewLine _
                                              & "     ,TANTO_CD                     " & vbNewLine _
                                              & "     ,ITEM_CURR_CD                 " & vbNewLine _
                                              & "     ,UNSO_HOKEN_AUTO_YN   --2018/10/22 002400                 " & vbNewLine _
                                              & "     ,INKA_ORIG_CD         --2018/10/25 要望番号001820         " & vbNewLine _
                                              & "     ,INIT_OUTKA_PLAN_DATE_KB   --2018/10/30 002192            " & vbNewLine _
                                              & "     ,INIT_INKA_PLAN_DATE_KB    --2018/10/30 002192            " & vbNewLine _
                                              & "     ,COA_INKA_DATE_FLG    --2018/11/14 要望番号001939         " & vbNewLine _
                                              & "     ,PRODUCT_SEG_CD               " & vbNewLine _
                                              & "     ,TCUST_BPCD                   " & vbNewLine _
                                              & "    )                              " & vbNewLine _
                                              & "VALUES                             " & vbNewLine _
                                              & "    (                              " & vbNewLine _
                                              & "      @NRS_BR_CD                   " & vbNewLine _
                                              & "     ,@CUST_CD_L                   " & vbNewLine _
                                              & "     ,@CUST_CD_M                   " & vbNewLine _
                                              & "     ,@CUST_CD_S                   " & vbNewLine _
                                              & "     ,@CUST_CD_SS                  " & vbNewLine _
                                              & "     ,@CUST_OYA_CD                 " & vbNewLine _
                                              & "     ,@CUST_NM_L                   " & vbNewLine _
                                              & "     ,@CUST_NM_M                   " & vbNewLine _
                                              & "     ,@CUST_NM_S                   " & vbNewLine _
                                              & "     ,@CUST_NM_SS                  " & vbNewLine _
                                              & "     ,@ZIP                         " & vbNewLine _
                                              & "     ,@AD_1                        " & vbNewLine _
                                              & "     ,@AD_2                        " & vbNewLine _
                                              & "     ,@AD_3                        " & vbNewLine _
                                              & "     ,@REMARK                      " & vbNewLine _
                                              & "     ,@PIC                         " & vbNewLine _
                                              & "     ,@FUKU_PIC                    " & vbNewLine _
                                              & "     ,@TEL                         " & vbNewLine _
                                              & "     ,@FAX                         " & vbNewLine _
                                              & "     ,@MAIL                        " & vbNewLine _
                                              & "     ,@SAITEI_HAN_KB               " & vbNewLine _
                                              & "     ,@OYA_SEIQTO_CD               " & vbNewLine _
                                              & "     ,@HOKAN_SEIQTO_CD             " & vbNewLine _
                                              & "     ,@NIYAKU_SEIQTO_CD            " & vbNewLine _
                                              & "     ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
                                              & "     ,@SAGYO_SEIQTO_CD             " & vbNewLine _
                                              & "     ,@INKA_RPT_YN                 " & vbNewLine _
                                              & "     ,@OUTKA_RPT_YN                " & vbNewLine _
                                              & "     ,@ZAI_RPT_YN                  " & vbNewLine _
                                              & "     ,@UNSO_TEHAI_KB               " & vbNewLine _
                                              & "     ,@SP_UNSO_CD                  " & vbNewLine _
                                              & "     ,@SP_UNSO_BR_CD               " & vbNewLine _
                                              & "     ,@BETU_KYORI_CD               " & vbNewLine _
                                              & "     ,@TAX_KB                      " & vbNewLine _
                                              & "     ,@HOKAN_FREE_KIKAN            " & vbNewLine _
                                              & "     ,@SMPL_SAGYO                  " & vbNewLine _
                                              & "     ,@HOKAN_NIYAKU_CALCULATION    " & vbNewLine _
                                              & "     ,''                           " & vbNewLine _
                                              & "     ,''                           " & vbNewLine _
                                              & "     ,''                           " & vbNewLine _
                                              & "     ,@HOKAN_NIYAKU_KEISAN_YN      " & vbNewLine _
                                              & "     ,@UNTIN_CALCULATION_KB        " & vbNewLine _
                                              & "     ,@DENPYO_NM                   " & vbNewLine _
                                              & "     ,@SOKO_CHANGE_KB              " & vbNewLine _
                                              & "     ,@DEFAULT_SOKO_CD             " & vbNewLine _
                                              & "     ,@PICK_LIST_KB                " & vbNewLine _
                                              & "     ,@SEKY_OFB_KB                 " & vbNewLine _
                                              & "     ,@SYS_ENT_DATE                " & vbNewLine _
                                              & "     ,@SYS_ENT_TIME                " & vbNewLine _
                                              & "     ,@SYS_ENT_PGID                " & vbNewLine _
                                              & "     ,@SYS_ENT_USER                " & vbNewLine _
                                              & "     ,@SYS_UPD_DATE                " & vbNewLine _
                                              & "     ,@SYS_UPD_TIME                " & vbNewLine _
                                              & "     ,@SYS_UPD_PGID                " & vbNewLine _
                                              & "     ,@SYS_UPD_USER                " & vbNewLine _
                                              & "     ,@SYS_DEL_FLG                 " & vbNewLine _
                                              & "     ,@TANTO_CD                    " & vbNewLine _
                                              & "     ,@ITEM_CURR_CD                " & vbNewLine _
                                              & "     ,@UNSO_HOKEN_AUTO_YN   --ADD 2018/10/22 002400    " & vbNewLine _
                                              & "     ,@INKA_ORIG_CD         --ADD 2018/10/25 要望番号001820 " & vbNewLine _
                                              & "     ,@INIT_OUTKA_PLAN_DATE_KB   --2018/10/30 002192        " & vbNewLine _
                                              & "     ,@INIT_INKA_PLAN_DATE_KB    --2018/10/30 002192        " & vbNewLine _
                                              & "     ,@COA_INKA_DATE_FLG    --ADD 2018/11/14 要望番号001939 " & vbNewLine _
                                              & "     ,@PRODUCT_SEG_CD              " & vbNewLine _
                                              & "     ,@TCUST_BPCD                  " & vbNewLine _
                                              & "    )                              " & vbNewLine
    'END YANAI 要望番号824

    ''' <summary>
    ''' 運賃タリフセットマスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TARIFF_SET_M As String = "INSERT INTO                 " & vbNewLine _
                                              & "    $LM_MST$..M_UNCHIN_TARIFF_SET " & vbNewLine _
                                              & "    (                             " & vbNewLine _
                                              & "      NRS_BR_CD                   " & vbNewLine _
                                              & "     ,CUST_CD_L                   " & vbNewLine _
                                              & "     ,CUST_CD_M                   " & vbNewLine _
                                              & "     ,SET_MST_CD                  " & vbNewLine _
                                              & "     ,DEST_CD                     " & vbNewLine _
                                              & "     ,SET_KB                      " & vbNewLine _
                                              & "     ,TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & "     ,UNCHIN_TARIFF_CD1           " & vbNewLine _
                                              & "     ,UNCHIN_TARIFF_CD2           " & vbNewLine _
                                              & "     ,EXTC_TARIFF_CD              " & vbNewLine _
                                              & "     ,YOKO_TARIFF_CD              " & vbNewLine _
                                              & "     ,SYS_ENT_DATE                " & vbNewLine _
                                              & "     ,SYS_ENT_TIME                " & vbNewLine _
                                              & "     ,SYS_ENT_PGID                " & vbNewLine _
                                              & "     ,SYS_ENT_USER                " & vbNewLine _
                                              & "     ,SYS_UPD_DATE                " & vbNewLine _
                                              & "     ,SYS_UPD_TIME                " & vbNewLine _
                                              & "     ,SYS_UPD_PGID                " & vbNewLine _
                                              & "     ,SYS_UPD_USER                " & vbNewLine _
                                              & "     ,SYS_DEL_FLG                 " & vbNewLine _
                                              & "    )                             " & vbNewLine _
                                              & "VALUES                            " & vbNewLine _
                                              & "    (                             " & vbNewLine _
                                              & "      @NRS_BR_CD                  " & vbNewLine _
                                              & "     ,@CUST_CD_L                  " & vbNewLine _
                                              & "     ,@CUST_CD_M                  " & vbNewLine _
                                              & "     ,@SET_MST_CD                 " & vbNewLine _
                                              & "     ,@DEST_CD                    " & vbNewLine _
                                              & "     ,@SET_KB                     " & vbNewLine _
                                              & "     ,@TARIFF_BUNRUI_KB           " & vbNewLine _
                                              & "     ,@UNCHIN_TARIFF_CD1          " & vbNewLine _
                                              & "     ,@UNCHIN_TARIFF_CD2          " & vbNewLine _
                                              & "     ,@EXTC_TARIFF_CD             " & vbNewLine _
                                              & "     ,@YOKO_TARIFF_CD             " & vbNewLine _
                                              & "     ,@SYS_ENT_DATE               " & vbNewLine _
                                              & "     ,@SYS_ENT_TIME               " & vbNewLine _
                                              & "     ,@SYS_ENT_PGID               " & vbNewLine _
                                              & "     ,@SYS_ENT_USER               " & vbNewLine _
                                              & "     ,@SYS_UPD_DATE               " & vbNewLine _
                                              & "     ,@SYS_UPD_TIME               " & vbNewLine _
                                              & "     ,@SYS_UPD_PGID               " & vbNewLine _
                                              & "     ,@SYS_UPD_USER               " & vbNewLine _
                                              & "     ,@SYS_DEL_FLG                " & vbNewLine _
                                              & "    )                             " & vbNewLine

    ''' <summary>
    ''' 荷主別帳票マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_PRT_M As String = "INSERT INTO                       " & vbNewLine _
                                             & "    $LM_MST$..M_CUST_RPT          " & vbNewLine _
                                             & "    (                             " & vbNewLine _
                                             & "      NRS_BR_CD                   " & vbNewLine _
                                             & "     ,CUST_CD_L                   " & vbNewLine _
                                             & "     ,CUST_CD_M                   " & vbNewLine _
                                             & "     ,CUST_CD_S                   " & vbNewLine _
                                             & "     ,PTN_ID                      " & vbNewLine _
                                             & "     ,PTN_CD                      " & vbNewLine _
                                             & "     ,SYS_ENT_DATE                " & vbNewLine _
                                             & "     ,SYS_ENT_TIME                " & vbNewLine _
                                             & "     ,SYS_ENT_PGID                " & vbNewLine _
                                             & "     ,SYS_ENT_USER                " & vbNewLine _
                                             & "     ,SYS_UPD_DATE                " & vbNewLine _
                                             & "     ,SYS_UPD_TIME                " & vbNewLine _
                                             & "     ,SYS_UPD_PGID                " & vbNewLine _
                                             & "     ,SYS_UPD_USER                " & vbNewLine _
                                             & "     ,SYS_DEL_FLG                 " & vbNewLine _
                                             & "    )                             " & vbNewLine _
                                             & "VALUES                            " & vbNewLine _
                                             & "    (                             " & vbNewLine _
                                             & "      @NRS_BR_CD                  " & vbNewLine _
                                             & "     ,@CUST_CD_L                  " & vbNewLine _
                                             & "     ,@CUST_CD_M                  " & vbNewLine _
                                             & "     ,@CUST_CD_S                  " & vbNewLine _
                                             & "     ,@PTN_ID                     " & vbNewLine _
                                             & "     ,@PTN_CD                     " & vbNewLine _
                                             & "     ,@SYS_ENT_DATE               " & vbNewLine _
                                             & "     ,@SYS_ENT_TIME               " & vbNewLine _
                                             & "     ,@SYS_ENT_PGID               " & vbNewLine _
                                             & "     ,@SYS_ENT_USER               " & vbNewLine _
                                             & "     ,@SYS_UPD_DATE               " & vbNewLine _
                                             & "     ,@SYS_UPD_TIME               " & vbNewLine _
                                             & "     ,@SYS_UPD_PGID               " & vbNewLine _
                                             & "     ,@SYS_UPD_USER               " & vbNewLine _
                                             & "     ,@SYS_DEL_FLG                " & vbNewLine _
                                             & "    )                             " & vbNewLine

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 荷主明細マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DETAIL_M As String = "INSERT INTO                    " & vbNewLine _
                                             & "    $LM_MST$..M_CUST_DETAILS      " & vbNewLine _
                                             & "    (                             " & vbNewLine _
                                             & "      NRS_BR_CD                   " & vbNewLine _
                                             & "     ,CUST_CD                     " & vbNewLine _
                                             & "     ,CUST_CD_EDA                 " & vbNewLine _
                                             & "     ,CUST_CLASS                  " & vbNewLine _
                                             & "     ,SUB_KB                      " & vbNewLine _
                                             & "     ,SET_NAIYO                   " & vbNewLine _
                                             & "     ,SET_NAIYO_2                 " & vbNewLine _
                                             & "     ,SET_NAIYO_3                 " & vbNewLine _
                                             & "     ,REMARK                      " & vbNewLine _
                                             & "     ,SYS_ENT_DATE                " & vbNewLine _
                                             & "     ,SYS_ENT_TIME                " & vbNewLine _
                                             & "     ,SYS_ENT_PGID                " & vbNewLine _
                                             & "     ,SYS_ENT_USER                " & vbNewLine _
                                             & "     ,SYS_UPD_DATE                " & vbNewLine _
                                             & "     ,SYS_UPD_TIME                " & vbNewLine _
                                             & "     ,SYS_UPD_PGID                " & vbNewLine _
                                             & "     ,SYS_UPD_USER                " & vbNewLine _
                                             & "     ,SYS_DEL_FLG                 " & vbNewLine _
                                             & "    )                             " & vbNewLine _
                                             & "VALUES                            " & vbNewLine _
                                             & "    (                             " & vbNewLine _
                                             & "      @NRS_BR_CD                  " & vbNewLine _
                                             & "     ,@CUST_CD                    " & vbNewLine _
                                             & "     ,@CUST_CD_EDA                " & vbNewLine _
                                             & "     ,@CUST_CLASS                 " & vbNewLine _
                                             & "     ,@SUB_KB                     " & vbNewLine _
                                             & "     ,@SET_NAIYO                  " & vbNewLine _
                                             & "     ,@SET_NAIYO_2                " & vbNewLine _
                                             & "     ,@SET_NAIYO_3                " & vbNewLine _
                                             & "     ,@REMARK                     " & vbNewLine _
                                             & "     ,@SYS_ENT_DATE               " & vbNewLine _
                                             & "     ,@SYS_ENT_TIME               " & vbNewLine _
                                             & "     ,@SYS_ENT_PGID               " & vbNewLine _
                                             & "     ,@SYS_ENT_USER               " & vbNewLine _
                                             & "     ,@SYS_UPD_DATE               " & vbNewLine _
                                             & "     ,@SYS_UPD_TIME               " & vbNewLine _
                                             & "     ,@SYS_UPD_PGID               " & vbNewLine _
                                             & "     ,@SYS_UPD_USER               " & vbNewLine _
                                             & "     ,@SYS_DEL_FLG                " & vbNewLine _
                                             & "    )                             " & vbNewLine
    '要望番号:349 yamanaka 2012.07.12 End

#End Region

#Region "更新処理 SQL"

    ''' <summary>
    ''' 荷主マスタ(大)更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CUST_L As String = "UPDATE                                                      " & vbNewLine _
                                               & "    $LM_MST$..M_CUST                                       " & vbNewLine _
                                               & "SET                                                        " & vbNewLine _
                                               & "      CUST_OYA_CD             =    @CUST_OYA_CD            " & vbNewLine _
                                               & "     ,CUST_NM_L               =    @CUST_NM_L              " & vbNewLine _
                                               & "     ,UNSO_TEHAI_KB           =    @UNSO_TEHAI_KB          " & vbNewLine _
                                               & "     ,SMPL_SAGYO              =    @SMPL_SAGYO             " & vbNewLine _
                                               & "--ADD START 2018/11/14 要望番号001939                      " & vbNewLine _
                                               & "     ,COA_INKA_DATE_FLG       =    @COA_INKA_DATE_FLG      " & vbNewLine _
                                               & "--ADD END   2018/11/14 要望番号001939                      " & vbNewLine _
                                               & "     ,PRODUCT_SEG_CD          =    @PRODUCT_SEG_CD         " & vbNewLine _
                                               & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE           " & vbNewLine _
                                               & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME           " & vbNewLine _
                                               & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID           " & vbNewLine _
                                               & "     ,SYS_UPD_USER            =    @SYS_UPD_USER           " & vbNewLine _
                                               & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                               & "AND   CUST_CD_L               =    @CUST_CD_L              " & vbNewLine

    'START YANAI 要望番号824
    '''' <summary>
    '''' 荷主マスタ(中)更新SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_CUST_M As String = "UPDATE                                                     " & vbNewLine _
    '                                          & "    $LM_MST$..M_CUST                                       " & vbNewLine _
    '                                          & "SET                                                        " & vbNewLine _
    '                                          & "       CUST_NM_M              =    @CUST_NM_M              " & vbNewLine _
    '                                          & "      ,ZIP                    =    @ZIP                    " & vbNewLine _
    '                                          & "      ,AD_1                   =    @AD_1                   " & vbNewLine _
    '                                          & "      ,AD_2                   =    @AD_2                   " & vbNewLine _
    '                                          & "      ,AD_3                   =    @AD_3                   " & vbNewLine _
    '                                          & "      ,SAITEI_HAN_KB          =    @SAITEI_HAN_KB          " & vbNewLine _
    '                                          & "      ,INKA_RPT_YN            =    @INKA_RPT_YN            " & vbNewLine _
    '                                          & "      ,OUTKA_RPT_YN           =    @OUTKA_RPT_YN           " & vbNewLine _
    '                                          & "      ,ZAI_RPT_YN             =    @ZAI_RPT_YN             " & vbNewLine _
    '                                          & "      ,SP_UNSO_CD             =    @SP_UNSO_CD             " & vbNewLine _
    '                                          & "      ,SP_UNSO_BR_CD          =    @SP_UNSO_BR_CD          " & vbNewLine _
    '                                          & "      ,BETU_KYORI_CD          =    @BETU_KYORI_CD          " & vbNewLine _
    '                                          & "      ,TAX_KB                 =    @TAX_KB                 " & vbNewLine _
    '                                          & "      ,HOKAN_FREE_KIKAN       =    @HOKAN_FREE_KIKAN       " & vbNewLine _
    '                                          & "      ,DEFAULT_SOKO_CD        =    @DEFAULT_SOKO_CD        " & vbNewLine _
    '                                          & "      ,SYS_UPD_DATE           =    @SYS_UPD_DATE           " & vbNewLine _
    '                                          & "      ,SYS_UPD_TIME           =    @SYS_UPD_TIME           " & vbNewLine _
    '                                          & "      ,SYS_UPD_PGID           =    @SYS_UPD_PGID           " & vbNewLine _
    '                                          & "      ,SYS_UPD_USER           =    @SYS_UPD_USER           " & vbNewLine _
    '                                          & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
    '                                          & "AND   CUST_CD_L               =    @CUST_CD_L              " & vbNewLine _
    '                                          & "AND   CUST_CD_M               =    @CUST_CD_M              " & vbNewLine
    ''' <summary>
    ''' 荷主マスタ(中)更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CUST_M As String = "UPDATE                                                     " & vbNewLine _
                                              & "    $LM_MST$..M_CUST                                       " & vbNewLine _
                                              & "SET                                                        " & vbNewLine _
                                              & "       CUST_NM_M              =    @CUST_NM_M              " & vbNewLine _
                                              & "      ,ZIP                    =    @ZIP                    " & vbNewLine _
                                              & "      ,AD_1                   =    @AD_1                   " & vbNewLine _
                                              & "      ,AD_2                   =    @AD_2                   " & vbNewLine _
                                              & "      ,AD_3                   =    @AD_3                   " & vbNewLine _
                                              & "      ,SAITEI_HAN_KB          =    @SAITEI_HAN_KB          " & vbNewLine _
                                              & "      ,INKA_RPT_YN            =    @INKA_RPT_YN            " & vbNewLine _
                                              & "      ,OUTKA_RPT_YN           =    @OUTKA_RPT_YN           " & vbNewLine _
                                              & "      ,ZAI_RPT_YN             =    @ZAI_RPT_YN             " & vbNewLine _
                                              & "      ,SP_UNSO_CD             =    @SP_UNSO_CD             " & vbNewLine _
                                              & "      ,SP_UNSO_BR_CD          =    @SP_UNSO_BR_CD          " & vbNewLine _
                                              & "      ,BETU_KYORI_CD          =    @BETU_KYORI_CD          " & vbNewLine _
                                              & "      ,TAX_KB                 =    @TAX_KB                 " & vbNewLine _
                                              & "      ,HOKAN_FREE_KIKAN       =    @HOKAN_FREE_KIKAN       " & vbNewLine _
                                              & "      ,DEFAULT_SOKO_CD        =    @DEFAULT_SOKO_CD        " & vbNewLine _
                                              & "      ,TANTO_CD               =    @TANTO_CD               " & vbNewLine _
                                              & "      ,ITEM_CURR_CD           =    @ITEM_CURR_CD           " & vbNewLine _
                                              & "      ,UNSO_HOKEN_AUTO_YN     =    @UNSO_HOKEN_AUTO_YN  --ADD 2018/10/22 002400          " & vbNewLine _
                                              & "      ,INKA_ORIG_CD           =    @INKA_ORIG_CD        --ADD 2018/10/25 要望管理001820  " & vbNewLine _
                                              & "      ,INIT_OUTKA_PLAN_DATE_KB =   @INIT_OUTKA_PLAN_DATE_KB  --ADD 2018/10/30 002192     " & vbNewLine _
                                              & "      ,INIT_INKA_PLAN_DATE_KB  =   @INIT_INKA_PLAN_DATE_KB   --ADD 2018/10/30 002192     " & vbNewLine _
                                              & "      ,REMARK                 =    @REMARK   --ADD 2019/07/10 002520     " & vbNewLine _
                                              & "      ,SYS_UPD_DATE           =    @SYS_UPD_DATE           " & vbNewLine _
                                              & "      ,SYS_UPD_TIME           =    @SYS_UPD_TIME           " & vbNewLine _
                                              & "      ,SYS_UPD_PGID           =    @SYS_UPD_PGID           " & vbNewLine _
                                              & "      ,SYS_UPD_USER           =    @SYS_UPD_USER           " & vbNewLine _
                                              & "WHERE NRS_BR_CD               =    @NRS_BR_CD              " & vbNewLine _
                                              & "AND   CUST_CD_L               =    @CUST_CD_L              " & vbNewLine _
                                              & "AND   CUST_CD_M               =    @CUST_CD_M              " & vbNewLine
    'END YANAI 要望番号824

    ''' <summary>
    ''' 荷主マスタ(小)更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CUST_S As String = "UPDATE                                                      " & vbNewLine _
                                              & "    $LM_MST$..M_CUST                                        " & vbNewLine _
                                              & "SET                                                         " & vbNewLine _
                                              & "       CUST_NM_S              =    @CUST_NM_S               " & vbNewLine _
                                              & "      ,TCUST_BPCD             =    @TCUST_BPCD              " & vbNewLine _
                                              & "      ,OYA_SEIQTO_CD          =    @OYA_SEIQTO_CD           " & vbNewLine _
                                              & "      ,HOKAN_SEIQTO_CD        =    @HOKAN_SEIQTO_CD         " & vbNewLine _
                                              & "      ,NIYAKU_SEIQTO_CD       =    @NIYAKU_SEIQTO_CD        " & vbNewLine _
                                              & "      ,UNCHIN_SEIQTO_CD       =    @UNCHIN_SEIQTO_CD        " & vbNewLine _
                                              & "      ,SAGYO_SEIQTO_CD        =    @SAGYO_SEIQTO_CD         " & vbNewLine _
                                              & "      ,UNTIN_CALCULATION_KB   =    @UNTIN_CALCULATION_KB    " & vbNewLine _
                                              & "      ,DENPYO_NM              =    @DENPYO_NM               " & vbNewLine _
                                              & "      ,PICK_LIST_KB           =    @PICK_LIST_KB            " & vbNewLine _
                                              & "      ,SYS_UPD_DATE           =    @SYS_UPD_DATE            " & vbNewLine _
                                              & "      ,SYS_UPD_TIME           =    @SYS_UPD_TIME            " & vbNewLine _
                                              & "      ,SYS_UPD_PGID           =    @SYS_UPD_PGID            " & vbNewLine _
                                              & "      ,SYS_UPD_USER           =    @SYS_UPD_USER            " & vbNewLine _
                                              & "WHERE NRS_BR_CD               =    @NRS_BR_CD               " & vbNewLine _
                                              & "AND   CUST_CD_L               =    @CUST_CD_L               " & vbNewLine _
                                              & "AND   CUST_CD_M               =    @CUST_CD_M               " & vbNewLine _
                                              & "AND   CUST_CD_S               =    @CUST_CD_S               " & vbNewLine

    ''' <summary>
    ''' 荷主マスタ(極小)更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CUST_SS As String = "UPDATE                                                            " & vbNewLine _
                                               & "    $LM_MST$..M_CUST                                              " & vbNewLine _
                                               & "SET                                                               " & vbNewLine _
                                               & "       CUST_NM_SS               =    @CUST_NM_SS                  " & vbNewLine _
                                               & "      ,PIC                      =    @PIC                         " & vbNewLine _
                                               & "      ,FUKU_PIC                 =    @FUKU_PIC                    " & vbNewLine _
                                               & "      ,TEL                      =    @TEL                         " & vbNewLine _
                                               & "      ,FAX                      =    @FAX                         " & vbNewLine _
                                               & "      ,MAIL                     =    @MAIL                        " & vbNewLine _
                                               & "      ,HOKAN_NIYAKU_CALCULATION =    @HOKAN_NIYAKU_CALCULATION    " & vbNewLine _
                                               & "      ,HOKAN_NIYAKU_KEISAN_YN   =    @HOKAN_NIYAKU_KEISAN_YN      " & vbNewLine _
                                               & "      ,SOKO_CHANGE_KB           =    @SOKO_CHANGE_KB              " & vbNewLine _
                                               & "      ,SEKY_OFB_KB              =    @SEKY_OFB_KB                 " & vbNewLine _
                                               & "      ,SYS_UPD_DATE             =    @SYS_UPD_DATE                " & vbNewLine _
                                               & "      ,SYS_UPD_TIME             =    @SYS_UPD_TIME                " & vbNewLine _
                                               & "      ,SYS_UPD_PGID             =    @SYS_UPD_PGID                " & vbNewLine _
                                               & "      ,SYS_UPD_USER             =    @SYS_UPD_USER                " & vbNewLine _
                                               & "WHERE NRS_BR_CD                 =    @NRS_BR_CD                   " & vbNewLine _
                                               & "AND   CUST_CD_L                 =    @CUST_CD_L                   " & vbNewLine _
                                               & "AND   CUST_CD_M                 =    @CUST_CD_M                   " & vbNewLine _
                                               & "AND   CUST_CD_S                 =    @CUST_CD_S                   " & vbNewLine _
                                               & "AND   CUST_CD_SS                =    @CUST_CD_SS                  " & vbNewLine

    ''' <summary>
    ''' 運賃タリフセットマスタ更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TARIFF_SET As String = "UPDATE                                                   " & vbNewLine _
                                                  & "    $LM_MST$..M_UNCHIN_TARIFF_SET                        " & vbNewLine _
                                                  & "SET                                                      " & vbNewLine _
                                                  & "      DEST_CD                 =    @DEST_CD              " & vbNewLine _
                                                  & "     ,SET_KB                  =    @SET_KB               " & vbNewLine _
                                                  & "     ,TARIFF_BUNRUI_KB        =    @TARIFF_BUNRUI_KB     " & vbNewLine _
                                                  & "     ,UNCHIN_TARIFF_CD1       =    @UNCHIN_TARIFF_CD1    " & vbNewLine _
                                                  & "     ,UNCHIN_TARIFF_CD2       =    @UNCHIN_TARIFF_CD2    " & vbNewLine _
                                                  & "     ,EXTC_TARIFF_CD          =    @EXTC_TARIFF_CD       " & vbNewLine _
                                                  & "     ,YOKO_TARIFF_CD          =    @YOKO_TARIFF_CD       " & vbNewLine _
                                                  & "     ,SYS_UPD_DATE            =    @SYS_UPD_DATE         " & vbNewLine _
                                                  & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME         " & vbNewLine _
                                                  & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID         " & vbNewLine _
                                                  & "     ,SYS_UPD_USER            =    @SYS_UPD_USER         " & vbNewLine _
                                                  & "WHERE NRS_BR_CD               =    @NRS_BR_CD            " & vbNewLine _
                                                  & "AND   CUST_CD_L               =    @CUST_CD_L            " & vbNewLine _
                                                  & "AND   CUST_CD_M               =    @CUST_CD_M            " & vbNewLine _
                                                  & "AND   SET_MST_CD              =    @SET_MST_CD           " & vbNewLine


    ''' <summary>
    ''' 荷主別帳票マスタ物理削除SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_CSUT_PRT As String = "DELETE FROM $LM_MST$..M_CUST_RPT       " & vbNewLine _
                                                 & "WHERE   NRS_BR_CD    = @NRS_BR_CD     " & vbNewLine _
                                                 & "AND     CUST_CD_L    = @CUST_CD_L     " & vbNewLine _
                                                 & "AND     CUST_CD_M    = @CUST_CD_M     " & vbNewLine _
                                                 & "AND     CUST_CD_S    = @CUST_CD_S     " & vbNewLine

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 荷主明細マスタ物理削除SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_CSUT_DETAIL As String = "DELETE FROM $LM_MST$..M_CUST_DETAILS  " & vbNewLine _
                                                   & "WHERE   NRS_BR_CD    = @NRS_BR_CD     " & vbNewLine _
                                                   & "AND     CUST_CD      = @CUST_CD       " & vbNewLine
    '要望番号:349 yamanaka 2012.07.12 End

#End Region

#Region "ComboBox SQL"

    ''' <summary>
    ''' 製品セグメント取得用
    ''' </summary>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Const SQL_SELECT_COMBO_SEIHIN As String _
            = " SELECT                                          " & vbNewLine _
            & "    CNCT_SEG_CD AS SEG_CD                        " & vbNewLine _
            & "   ,CONCAT(SGMT_L_NM,'：',SGMT_M_NM) AS SEG_NM   " & vbNewLine _
            & " FROM                                            " & vbNewLine _
            & "   ABM_DB..M_SEGMENT                             " & vbNewLine _
            & " WHERE                                           " & vbNewLine _
            & "   DATA_TYPE_CD = '00002'                        " & vbNewLine _
            & "   AND KBN_LANG = @KBN_LANG                      " & vbNewLine _
            & "   AND SYS_DEL_FLG = '0'                         " & vbNewLine _
            & " ORDER BY                                        " & vbNewLine _
            & "     CNCT_SEG_CD                                 " & vbNewLine

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

#Region "編集処理"

    ''' <summary>
    ''' 荷主マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function CustMHaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_CUSTM_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamCustMHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "CustMHaitaChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function TariffMHaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_TARIFF_SET")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_TARIFFM_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )


        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max


            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamTariffMHaitaChk()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "TariffMHaitaChk", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))

            reader.Close()

            'エラーメッセージの設定
            '更新の場合
            If MyBase.GetResultCount() < 1 Then
                MyBase.SetMessage("E011")
                Exit For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ存在チェック
    ''' </summary>
    ''' <returns>True : データあり、　False：データなしt</returns>
    ''' <remarks>荷主マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function TariffMExistChk() As Boolean

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_TARIFFM_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )


        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamTariffMHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "TariffMHaitaChk", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))

        reader.Close()

        'データなしの場合
        If MyBase.GetResultCount() < 1 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データテーブル検索結果取得SQLの構築・発行</remarks>
    Private Function ZaikoExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_EXIST_ZAIKO)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定（荷主マスタ排他チェック用を利用）
        Call Me.SetParamCustMHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "ZaikoExistChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        Dim dtZaiko As DataTable = ds.Tables("LMM090ZAIKO")
        dtZaiko.Clear()
        Dim drZaiko As DataRow = dtZaiko.NewRow
        drZaiko.Item("SUM_PORA_ZAI_NB") = CStr(reader("SELECT_CNT"))
        drZaiko.Item("REC_CNT") = CStr(reader("REC_CNT"))
        dtZaiko.Rows.Add(drZaiko)

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCustMHaitaChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '荷主マスタ主キー
        Call Me.SetParamPrimaryKeyCustM()

        '排他項目
        Call Me.SetParamHaita()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTariffMHaitaChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '運賃タリフセットマスタ主キー
        Call Me.SetParamPrimaryKeyTariffM()

        '排他項目
        'Call Me.SetParamHaita()

    End Sub

#End Region

#Region "削除/復活処理"

#Region "チェック"

    ''' <summary>
    ''' 削除可否チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ChkDeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_EXIST_KODOMO_CUST)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamOyaKoChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "ChkDeleteData", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount() > 0 Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E028", New String() {"親荷主", "削除"})
            MyBase.SetMessage("E372")
            '2016.01.06 UMANO 英語化対応END
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 親データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ChkOyaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_EXIST_OYA_CUST)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamOyaKoChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "ChkOyaData", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 更新可否チェック(運賃タリフセットマスタ)
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function CheckUpdDelTariff(ByVal ds As DataSet, ByVal i As Integer) As Boolean

        If Me._Row.Item("SYS_DEL_FLG").Equals(LMConst.FLG.OFF) Then
            Return True
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(i)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_TARIFFM_UPDDEL_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "CheckUpdDelTariff", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        If MyBase.GetResultCount() > 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 更新可否チェック(荷主別帳票マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主別帳票マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function CheckUpdDelCustPrt(ByVal ds As DataSet) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_CUST_PRT_UPDDEL_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "CheckUpdDelCustPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount() > 0 Then
            Return False
        End If

        Return True

    End Function

    '要望番号:349 yamanaka 2012.07.19 Start
    ''' <summary>
    ''' 削除更新可否チェック(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:削除可, False:削除不可</returns>
    ''' <remarks>荷主明細マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function CheckUpdDelCustDetail(ByVal ds As DataSet, ByVal custLevel As String) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化

        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成

        Select Case custLevel
            Case "S"
                Me._StrSql.Append(LMM090DAC.SQL_CUST_DETAIL_UPDDEL_CHK_S)
            Case "M"
                Me._StrSql.Append(LMM090DAC.SQL_CUST_DETAIL_UPDDEL_CHK_M)
            Case "L"
                Me._StrSql.Append(LMM090DAC.SQL_CUST_DETAIL_UPDDEL_CHK_L)
            Case Else
                Return False
        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "CheckUpdDelCustDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        '削除の場合
        If _Row("SYS_DEL_FLG").ToString.Equals("1") = True Then
            If MyBase.GetResultCount() > 0 Then
                Return False
            End If
            '復活の場合
        ElseIf _Row("SYS_DEL_FLG").ToString.Equals("0") = True Then
            If MyBase.GetResultCount() = 0 Then
                Return False
            End If
        End If

        Return True

    End Function
    '要望番号:349 yamanaka 2012.07.19 End

    '要望番号:2034 kobayashi 2013.06.17 Start
    ''' <summary>
    ''' 退避データチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ChkTaihiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_IS_ESCAPED_CUST)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamTaihiChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "ChkTaihiData", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount() > 0 Then
            MyBase.SetMessage("E548")
        End If

        Return ds

    End Function
    '要望番号:2034 kobayashi 2013.06.17 End

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistZaiT(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_EXIST_ZAIT)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamZaiTrsChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "ExistZaiT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        If MyBase.GetResultCount > 0 Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E260", New String() {"在庫データ"})
            MyBase.SetMessage("E890")
            '2016.01.06 UMANO 英語化対応END
        End If

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(親、子供存在チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamOyaKoChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(退避データ存在チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTaihiChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフマスタ更新可否チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '荷主マスタ主キー
        Call Me.SetParamPrimaryKeyCustM()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫データ存在チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamZaiTrsChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        Dim cmd As SqlCommand = New SqlCommand()

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL構築
            Me._StrSql.Append(LMM090DAC.SQL_UPD_DEL_CUST_M)
            Call Me.SetConditionMasterUpdDelCustSQL()  '排他時使用

            'SQL文のコンパイル
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                         , Me._Row.Item("USER_BR_CD").ToString())
                                          )

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "DeleteCustData", cmd)

            '処理件数の設定
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            'エラーメッセージの設定
            If MyBase.GetResultCount() < 1 Then
                MyBase.SetMessage("E011")
                Exit For
            End If

            cmd = New SqlCommand()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ更新SQLの構築・発行</remarks>
    Private Function DeleteTariffSetData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_TARIFF_SET")

        Dim cmd As SqlCommand = New SqlCommand()

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '削除復活可能レコードかどうか判断する
            If i Mod 2 = 0 Then
                If Me.CheckUpdDelTariff(ds, i \ 2) = False Then
                    '何もしない
                Else

                    For j As Integer = i To i + 1
                        'INTableの条件rowの格納
                        Me._Row = inTbl.Rows(j)

                        'SQL格納変数の初期化
                        Me._StrSql = New StringBuilder()

                        'SQL構築
                        Me._StrSql.Append(LMM090DAC.SQL_UPD_DEL_TARIFFSET_M)
                        Call Me.SetConditionMasterUpdDelTariffSQL()  '排他時使用

                        'SQL文のコンパイル
                        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                     , Me._Row.Item("USER_BR_CD").ToString())
                                                     )

                        'パラメータの反映
                        For Each obj As Object In Me._SqlPrmList
                            cmd.Parameters.Add(obj)
                        Next

                        MyBase.Logger.WriteSQLLog("LMM090DAC", "DeleteTariffSetData", cmd)

                        '処理件数の設定
                        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

                        'エラーメッセージの設定
                        If MyBase.GetResultCount() < 1 Then
                            MyBase.SetMessage("E011")
                            Return ds
                        End If
                        cmd = New SqlCommand()
                    Next
                End If
            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 荷主別帳票マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主別帳票マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteCustPrtData(ByVal ds As DataSet) As DataSet

        '削除復活可能レコードかどうか判断する
        If Me.CheckUpdDelCustPrt(ds) = False Then
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_UPD_DEL_CUSTPRT_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                     , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdDelCustPrt()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "DeleteCustPrtData", cmd)

            '処理件数の設定
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    '要望番号:349 yamanaka 2012.07.19 Start
    ''' <summary>
    ''' 荷主明細マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主明細マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteCustDetailData(ByVal ds As DataSet) As DataSet

        Dim dtIn As DataTable = ds.Tables("LMM090IN")
        Dim dtDtl As DataTable = ds.Tables("LMM090_CUST_DETAILS")
        Dim drIn As DataRow = dtIn.Rows(0)
        Dim rowCnt As Integer = 0

        '極小レコード入れる
        With dtDtl
            .Rows.Add(.NewRow())
            .Rows(rowCnt).Item("CUST_CD") = String.Concat(drIn.Item("CUST_CD_L").ToString() _
                                                          , drIn.Item("CUST_CD_M").ToString() _
                                                          , drIn.Item("CUST_CD_S").ToString() _
                                                          , drIn.Item("CUST_CD_SS").ToString())
            .Rows(rowCnt).Item("NRS_BR_CD") = drIn.Item("NRS_BR_CD")
            .Rows(rowCnt).Item("SYS_DEL_FLG") = drIn.Item("SYS_DEL_FLG")
            .Rows(rowCnt).Item("USER_BR_CD") = drIn.Item("USER_BR_CD")
        End With
        rowCnt += 1

        '削除復活可能レコードかどうか判断する(小)
        If Me.CheckUpdDelCustDetail(ds, "S") = True Then
            With dtDtl
                .Rows.Add(.NewRow())
                .Rows(rowCnt).Item("CUST_CD") = String.Concat(drIn.Item("CUST_CD_L").ToString() _
                                                              , drIn.Item("CUST_CD_M").ToString() _
                                                              , drIn.Item("CUST_CD_S").ToString())
                .Rows(rowCnt).Item("NRS_BR_CD") = drIn.Item("NRS_BR_CD")
                .Rows(rowCnt).Item("SYS_DEL_FLG") = drIn.Item("SYS_DEL_FLG")
                .Rows(rowCnt).Item("USER_BR_CD") = drIn.Item("USER_BR_CD")
            End With
            rowCnt += 1
        End If

        '削除復活可能レコードかどうか判断する(中)
        If Me.CheckUpdDelCustDetail(ds, "M") = True Then
            With dtDtl
                .Rows.Add(.NewRow())
                .Rows(rowCnt).Item("CUST_CD") = String.Concat(drIn.Item("CUST_CD_L").ToString() _
                                                              , drIn.Item("CUST_CD_M").ToString())
                .Rows(rowCnt).Item("NRS_BR_CD") = drIn.Item("NRS_BR_CD")
                .Rows(rowCnt).Item("SYS_DEL_FLG") = drIn.Item("SYS_DEL_FLG")
                .Rows(rowCnt).Item("USER_BR_CD") = drIn.Item("USER_BR_CD")
            End With
            rowCnt += 1

        End If

        '削除復活可能レコードかどうか判断する(大)
        If Me.CheckUpdDelCustDetail(ds, "L") = True Then
            With dtDtl
                dtDtl.Rows.Add(.NewRow())
                .Rows(rowCnt).Item("CUST_CD") = drIn.Item("CUST_CD_L").ToString()
                .Rows(rowCnt).Item("NRS_BR_CD") = drIn.Item("NRS_BR_CD")
                .Rows(rowCnt).Item("SYS_DEL_FLG") = drIn.Item("SYS_DEL_FLG")
                .Rows(rowCnt).Item("USER_BR_CD") = drIn.Item("USER_BR_CD")
            End With
            rowCnt += 1

        End If

        'INTableの条件rowの格納
        Me._Row = dtDtl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_UPD_DEL_CUSTDETAIL_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                     , Me._Row.Item("USER_BR_CD").ToString())
                                                       )

        Dim max As Integer = dtDtl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = dtDtl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdDelCustDetail()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "DeleteCustDetailData", cmd)

            '処理件数の設定
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    '要望番号:349 yamanaka 2012.07.19 End

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterUpdDelCustSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '削除/復活フラグ
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        '荷主マスタ主キー
        Call Me.SetParamPrimaryKeyCustM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

        With Me._Row

            If String.IsNullOrEmpty(.Item("SYS_UPD_DATE").ToString) = False Then

                '排他処理を行う
                Me._StrSql.Append("AND  SYS_UPD_DATE            =    @HAITA_DATE         ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("AND  SYS_UPD_TIME            =    @HAITA_TIME         ")

                '排他項目
                Call Me.SetParamHaita()

            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterUpdDelTariffSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '削除/復活フラグ
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        '運賃タリフセットマスタ主キー
        Call Me.SetParamPrimaryKeyTariffM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

        With Me._Row

            If String.IsNullOrEmpty(.Item("SYS_UPD_DATE").ToString) = False Then

                '排他処理を行う
                Me._StrSql.Append("AND  SYS_UPD_DATE            =    @HAITA_DATE         ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("AND  SYS_UPD_TIME            =    @HAITA_TIME         ")

                '排他項目
                Call Me.SetParamHaita()

            End If

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelCustPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))

        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    '要望番号:349 yamanaka 2012.07.19 Start
    ''' <summary>
    ''' パラメータ設定モジュール(荷主明細マスタチェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelCustDetail()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", .Item("CUST_CD").ToString(), DBDataType.NVARCHAR))

        End With

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub
    '要望番号:349 yamanaka 2012.07.19 End

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JDE非必須ユーザ確認</remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        'ログインユーザの営業所のIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_VAR_STRAGE")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_VAR_STRAGE)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectVarStrage", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'レコードをクリア
        ds.Tables("LMM090_VAR_STRAGE").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090_VAR_STRAGE")

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 荷主マスタ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_COUNT_SELECT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DATA_FROM)        'SQL構築(カウント用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DATA_FROM_BASE)    'ADD 2019/01/07 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DATA_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DATA_FROM)        'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                          '条件設定
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DATA_FROM_BASE)    'ADD 2019/01/07 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
        Me._StrSql.Append(LMM090DAC.SQL_ORDER_BY_BASE)                'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_OYA_CD", "CUST_OYA_CD")
        map.Add("CUST_OYA_NM", "CUST_OYA_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
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
        map.Add("OYA_SEIQTO_NM", "OYA_SEIQTO_NM")
        map.Add("HOKAN_SEIQTO_CD", "HOKAN_SEIQTO_CD")
        map.Add("HOKAN_SEIQTO_NM", "HOKAN_SEIQTO_NM")
        map.Add("NIYAKU_SEIQTO_CD", "NIYAKU_SEIQTO_CD")
        map.Add("NIYAKU_SEIQTO_NM", "NIYAKU_SEIQTO_NM")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("UNCHIN_SEIQTO_NM", "UNCHIN_SEIQTO_NM")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")
        map.Add("SAGYO_SEIQTO_NM", "SAGYO_SEIQTO_NM")
        map.Add("CLOSE_KB", "CLOSE_KB")
        map.Add("INKA_RPT_YN", "INKA_RPT_YN")
        map.Add("OUTKA_RPT_YN", "OUTKA_RPT_YN")
        map.Add("ZAI_RPT_YN", "ZAI_RPT_YN")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("SP_UNSO_NM", "SP_UNSO_NM")
        map.Add("BETU_KYORI_CD", "BETU_KYORI_CD")
        map.Add("BETU_KYORI_REM", "BETU_KYORI_REM")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("SMPL_SAGYO", "SMPL_SAGYO")
        map.Add("SMPL_SAGYO_NM", "SMPL_SAGYO_NM")
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("PRODUCT_SEG_NM_L", "PRODUCT_SEG_NM_L")
        map.Add("PRODUCT_SEG_NM_M", "PRODUCT_SEG_NM_M")
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
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
        map.Add("SEKY_OFB_KB", "SEKY_OFB_KB")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        'START YANAI 要望番号824
        map.Add("TANTO_CD", "TANTO_CD")
        map.Add("TANTO_NM", "TANTO_NM")
        'END YANAI 要望番号824
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("UNSO_HOKEN_AUTO_YN", "UNSO_HOKEN_AUTO_YN")     'ADD 2018/10/22 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる
        'ADD Start 2018/10/25 要望番号001820
        map.Add("INKA_ORIG_CD", "INKA_ORIG_CD")
        map.Add("INKA_ORIG_NM", "INKA_ORIG_NM")
        'ADD End   2018/10/25 要望番号001820
        map.Add("INIT_OUTKA_PLAN_DATE_KB", "INIT_OUTKA_PLAN_DATE_KB")    'ADD 2018/10/30　002192   荷主ごと_入庫日・出荷日の初期値設定
        map.Add("INIT_INKA_PLAN_DATE_KB", "INIT_INKA_PLAN_DATE_KB")      'ADD 2018/10/30　002192   荷主ごと_入庫日・出荷日の初期値設定
        map.Add("COA_INKA_DATE_FLG", "COA_INKA_DATE_FLG")       'ADD 2018/11/14 要望番号001939

#If True Then   'ADD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
        map.Add("WEB_COMP_CD_L", "WEB_COMP_CD_L")
        map.Add("WEB_COMP_CD_LM", "WEB_COMP_CD_LM")
        map.Add("WEB_COMP_CD_LMS", "WEB_COMP_CD_LMS")
        map.Add("WEB_COMP_CD_LMSSS", "WEB_COMP_CD_LMSSS")
        map.Add("INTEG_WEB_FLG", "INTEG_WEB_FLG")

#End If
        map.Add("REMARK", "REMARK")     'ADD 2019/07/10 002520

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 荷主別帳票マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主別帳票マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectPrtListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_PRT_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectPrtListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090_CUST_RPT")

        Return ds

    End Function

    ''' <summary>
    '''運賃タリフセットマスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectTariffListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_TARIFF_DATA)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSelectTariff()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectTariffListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("SET_MST_CD", "SET_MST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SET_KB", "SET_KB")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("UNCHIN_TARIFF_CD1", "UNCHIN_TARIFF_CD1")
        map.Add("UNCHIN_TARIFF_NM1", "UNCHIN_TARIFF_NM1")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("UNCHIN_TARIFF_NM2", "UNCHIN_TARIFF_NM2")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("EXTC_TARIFF_NM", "EXTC_TARIFF_NM")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_TARIFF_NM", "YOKO_TARIFF_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090_TARIFF_SET")

        Return ds

    End Function

    '要望番号:349 yamanaka 2012.07.11 Start
    ''' <summary>
    ''' 荷主明細マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主明細マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectDetailListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DETAIL_DATA)
        Call Me.SetParamSelectDetail()   '条件設定
        Me._StrSql.Append(LMM090DAC.SQL_SELECT_DETAIL_GROUP_BY)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectDetailListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_CD_EDA", "CUST_CD_EDA")
        map.Add("CUST_CLASS", "CUST_CLASS")
        map.Add("CUST_CLASS_NM", "CUST_CLASS_NM")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SUB_KB_NM", "SUB_KB_NM")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")
        map.Add("REMARK", "REMARK")
        map.Add("UPD_FLG", "UPD_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090_CUST_DETAILS")

        Return ds

    End Function
    '要望番号:349 yamanaka 2012.07.11 End

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

            '【営業所コード：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '【荷主コード(大)：LIKE 値%】
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(中)：LIKE 値%】
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(小)：LIKE 値%】
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_CD_S LIKE @CUST_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(極小)：LIKE 値%】
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_CD_SS LIKE @CUST_CD_SS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主名(大)：LIKE %値%】
            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【荷主名(中)：LIKE %値%】
            whereStr = .Item("CUST_NM_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_M LIKE @CUST_NM_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【荷主名(小)：LIKE %値%】
            whereStr = .Item("CUST_NM_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_S LIKE @CUST_NM_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_S", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【荷主名(極小)：LIKE %値%】
            whereStr = .Item("CUST_NM_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_SS LIKE @CUST_NM_SS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_SS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【削除フラグ：=】
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            '【ログイン言語】
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

            'ADD 2019/01/07
            '【((INTEG_WEB_FLG 】
            whereStr = .Item("INTEG_WEB_FLG").ToString()

            'andstr.Append("  CST.INTEG_WEB_FLG = @INTEG_WEB_FLG")
            'andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INTEG_WEB_FLG", whereStr, DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ検索)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectTariff()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim whereClause As New StringBuilder()

        With Me._Row
            Dim searchItem As String = TryCast(.Item("NRS_BR_CD"), String)

            If String.IsNullOrEmpty(searchItem) = False Then
                whereClause.AppendLine("       TRS.NRS_BR_CD = @NRS_BR_CD")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", searchItem, DBDataType.CHAR))
            End If

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYSTEM_DATE", Me.GetSystemDate(), DBDataType.CHAR))

        End With

        If whereClause.Length > 0 Then
            Me._StrSql.AppendLine(" WHERE ")
            Me._StrSql.AppendLine(whereClause.ToString)
        End If

    End Sub

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(荷主明細マスタ検索)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectDetail()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '【営業所コード：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  DETAIL.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '【荷主コード：LIKE 値%】
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  DETAIL.CUST_CD LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【削除フラグ：=】
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  DETAIL.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With
    End Sub
    '要望番号:349 yamanaka 2012.07.12 End

#End Region

#Region "契約通貨コンボ取得"

    ''' <summary>
    ''' 契約通貨コンボ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>契約通貨コンボ取得</remarks>
    Private Function SelectComboItemCurrCd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_COMBO_ITEM_CURR_CD)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", Me.GetUserID(), DBDataType.NVARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectComboItemCurrCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090OUT")

        Return ds

    End Function
#End Region

#Region "保存処理"

#Region "チェック"

    ''2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>郵便番号マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    'Private Function ExistZipM(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMM090IN")

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL格納変数の初期化
    '    Me._StrSql = New StringBuilder()

    '    'SQL作成
    '    Me._StrSql.Append(LMM090DAC.SQL_EXIST_ZIPM)

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
    '                                                                   , Me._Row.Item("USER_BR_CD").ToString()) _
    '                                                                    )

    '    'SQLパラメータ初期化/設定
    '    Call Me.SetParamZipMChk()

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM090DAC", "ExistZipM", cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    '処理件数の設定
    '    reader.Read()

    '    'エラーメッセージの設定
    '    MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
    '    If MyBase.GetResultCount = 0 Then
    '        MyBase.SetMessage("E079", New String() {"郵便番号マスタ", "郵便番号"})
    '    End If

    '    reader.Close()

    '    Return ds

    'End Function
    ''2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' 新規採番コード取得/荷主コード上限チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function GetCustCd(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.SetSqlSaiban()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamCustMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "GetCustCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim newCd As String = reader("SAIBAN").ToString()
        reader.Close()

        '取得データを元に新荷主コードを設定する
        Call Me.SetCustCd(ds, newCd)

        Return ds

    End Function

    ''' <summary>
    ''' 新規採番コード取得用SQL作成
    ''' </summary>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Sub SetSqlSaiban()

        If String.IsNullOrEmpty(Me._Row.Item("CUST_CD_M").ToString()) Then
            '荷主コード(中)の新規採番を行う
            Me._StrSql.Append(LMM090DAC.SQL_SET_CUST_CD_M)
        ElseIf String.IsNullOrEmpty(Me._Row.Item("CUST_CD_S").ToString()) Then
            '荷主コード(小)の新規採番を行う
            Me._StrSql.Append(LMM090DAC.SQL_SET_CUST_CD_S)
        ElseIf String.IsNullOrEmpty(Me._Row.Item("CUST_CD_SS").ToString()) Then
            '荷主コード(極小)の新規採番を行う
            Me._StrSql.Append(LMM090DAC.SQL_SET_CUST_CD_SS)

        End If

    End Sub

    ''' <summary>
    ''' 新規荷主コード設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>新規採番値設定</remarks>
    Private Sub SetCustCd(ByVal ds As DataSet, ByVal newCd As String)

        '取得データを元に新荷主コードを決定する
        If String.IsNullOrEmpty(newCd) Then
            newCd = "00"
        Else
            newCd = (Convert.ToInt32(newCd) + 1).ToString().PadLeft(2, Convert.ToChar("0"))
        End If

        Dim drIn As DataRow = ds.Tables("LMM090IN").Rows(0)

        '返却DataSetに荷主コード設定
        Dim outTbl As DataTable = ds.Tables("LMM090OUT")
        Dim outDr As DataRow = outTbl.NewRow()
        Dim msg As String = String.Empty
        If Me._Row.Item("CLICK_M_FLG").ToString().Equals(LMConst.FLG.ON) Then
            outDr.Item("CUST_CD_M") = newCd
            msg = "荷主コード(中)"
        Else
            outDr.Item("CUST_CD_M") = Me._Row.Item("CUST_CD_M").ToString()
        End If
        If Me._Row.Item("CLICK_S_FLG").ToString().Equals(LMConst.FLG.ON) Then
            outDr.Item("CUST_CD_S") = newCd
            msg = "荷主コード(小)"
        Else
            outDr.Item("CUST_CD_S") = Me._Row.Item("CUST_CD_S").ToString()
        End If
        If Me._Row.Item("CLICK_SS_FLG").ToString().Equals(LMConst.FLG.ON) Then
            outDr.Item("CUST_CD_SS") = newCd
            msg = "荷主コード(極小)"
        Else
            outDr.Item("CUST_CD_SS") = Me._Row.Item("CUST_CD_SS").ToString()
        End If
        outTbl.Rows.Add(outDr)

        If Convert.ToInt32(newCd) > 99 Then
            MyBase.SetMessage("E062", New String() {msg})
        End If

    End Sub

    ''' <summary>
    ''' 荷主マスタ重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistCustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM090DAC.SQL_REPEAT_CUSTM)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamCustMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "ExistCustM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(郵便番号マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamZipMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            '主キー
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP_NO", .Item("ZIP").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCustMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '荷主マスタ主キー設定
        Call Me.SetParamPrimaryKeyCustM()

    End Sub

#End Region

#Region "新規登録/更新"

    ''' <summary>
    ''' 荷主マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertCustM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_INSERT_CUST_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertCust()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "InsertCustM", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_TARIFF_SET")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_INSERT_TARIFF_SET_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertTariff()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "InsertTariffSetM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 荷主別帳票マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主別帳票マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertCustPrtM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_CUST_RPT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_INSERT_PRT_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertCustPrt()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "InsertCustPrtM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 荷主明細マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主明細マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertCustDtlM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_CUST_DETAILS")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_INSERT_DETAIL_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertCustDtl()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM090DAC", "InsertCustDtlM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    '要望番号:349 yamanaka 2012.07.12 End

    ''' <summary>
    ''' 荷主マスタL更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateCustM_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_UPDATE_CUST_L)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateCust()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "UpdateCustM_L", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタM更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateCustM_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_UPDATE_CUST_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateCust()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "UpdateCustM_M", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタS更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateCustM_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_UPDATE_CUST_S)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateCust()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "UpdateCustM_S", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタS更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateCustM_SS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_UPDATE_CUST_SS)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateCust()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "UpdateCustM_SS", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateTariffSetM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090_TARIFF_SET")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル(UPDATE)
        Dim cmdUp As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM090DAC.SQL_UPDATE_TARIFF_SET _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQL文のコンパイル(INSERT)
        Dim cmdIn As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM090DAC.SQL_INSERT_TARIFF_SET_M _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )
        Dim haitaDate As String = String.Empty

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'データ存在チェック
            Dim result As Boolean = Me.TariffMExistChk()
            If result = True Then  'データあり（更新）

                Call Me.SetParamUpdateTariffSet()          'SQLパラメータ初期化/設定
                For Each obj As Object In Me._SqlPrmList   'パラメータの反映
                    cmdUp.Parameters.Add(obj)
                Next
                MyBase.GetUpdateResult(cmdUp)              'SQLの発行
                MyBase.Logger.WriteSQLLog("LMM090DAC", "UpdateTariffSetM", cmdUp)
                cmdUp.Parameters.Clear()

            Else   'データなし（新規）

                Call Me.SetParamInsertTariff()             'SQLパラメータ初期化/設定
                For Each obj As Object In Me._SqlPrmList   'パラメータの反映
                    cmdIn.Parameters.Add(obj)
                Next
                MyBase.GetInsertResult(cmdIn)              'SQLの発行
                MyBase.Logger.WriteSQLLog("LMM090DAC", "InsertTariffSetM", cmdUp)
                cmdIn.Parameters.Clear()

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 荷主別帳票マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主別帳票マスタ物理削除SQLの構築・発行</remarks>
    Private Function DeleteCustPrtM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_DELETE_CSUT_PRT)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteCustPrt()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "DeleteCustPrtM", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' 荷主明細マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主明細マスタ物理削除SQLの構築・発行</remarks>
    Private Function DeleteCustDtlM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM090DAC.SQL_DELETE_CSUT_DETAIL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteCustDtl()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "DeleteCustDtlM", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function
    '要望番号:349 yamanaka 2012.07.12 End

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertCust()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '荷主マスタ全項目
        Call Me.SetParamCustM()

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertTariff()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '運賃タリフセットマスタ全項目
        Call Me.SetParamTariffM()

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(荷主別帳票マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertCustPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", .Item("PTN_ID").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", .Item("PTN_CD").ToString(), DBDataType.CHAR))
        End With

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' パラメータ設定モジュール(荷主明細マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertCustDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", .Item("CUST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_EDA", .Item("CUST_CD_EDA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CLASS", .Item("CUST_CLASS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_KB", .Item("SUB_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_NAIYO", .Item("SET_NAIYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_NAIYO_2", .Item("SET_NAIYO_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_NAIYO_3", .Item("SET_NAIYO_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub
    '要望番号:349 yamanaka 2012.07.12 End

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateCust()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '荷主マスタ全項目
        Call Me.SetParamCustM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateTariffSet()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '運賃タリフセットマスタ全項目
        Call Me.SetParamTariffM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(荷主別帳票マスタ物理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteCustPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))

        End With

    End Sub

    '要望番号:349 yamanaka 2012.07.12 Start
    ''' <summary>
    ''' パラメータ設定モジュール(荷主明細マスタ物理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteCustDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '大タブ編集時
            If LMConst.FLG.ON.Equals(.Item("CLICK_L_FLG").ToString()) = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                '中タブ編集時
            ElseIf LMConst.FLG.ON.Equals(.Item("CLICK_M_FLG").ToString()) = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(.Item("CUST_CD_L").ToString() _
                                                                                    , .Item("CUST_CD_M").ToString()) _
                                                                                    , DBDataType.CHAR))
                '小タブ編集時
            ElseIf LMConst.FLG.ON.Equals(.Item("CLICK_S_FLG").ToString()) = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(.Item("CUST_CD_L").ToString() _
                                                                                    , .Item("CUST_CD_M").ToString() _
                                                                                    , .Item("CUST_CD_S").ToString()) _
                                                                                    , DBDataType.CHAR))
                '極小タブ編集時
            ElseIf LMConst.FLG.ON.Equals(.Item("CLICK_SS_FLG").ToString()) = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(.Item("CUST_CD_L").ToString() _
                                                                                    , .Item("CUST_CD_M").ToString() _
                                                                                    , .Item("CUST_CD_S").ToString() _
                                                                                    , .Item("CUST_CD_SS").ToString()) _
                                                                                    , DBDataType.CHAR))
            End If

        End With

    End Sub
    '要望番号:349 yamanaka 2012.07.12 End

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCustM()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_OYA_CD", .Item("CUST_OYA_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", .Item("CUST_NM_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", .Item("CUST_NM_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_S", .Item("CUST_NM_S").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_SS", .Item("CUST_NM_SS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", .Item("AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_2", .Item("AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PIC", .Item("PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FUKU_PIC", .Item("FUKU_PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", .Item("TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", .Item("FAX").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAIL", .Item("MAIL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAITEI_HAN_KB", .Item("SAITEI_HAN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_SEIQTO_CD", .Item("OYA_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SEIQTO_CD", .Item("HOKAN_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_SEIQTO_CD", .Item("NIYAKU_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_SEIQTO_CD", .Item("UNCHIN_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SEIQTO_CD", .Item("SAGYO_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_RPT_YN", .Item("INKA_RPT_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_RPT_YN", .Item("OUTKA_RPT_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_RPT_YN", .Item("ZAI_RPT_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_UNSO_CD", .Item("SP_UNSO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_UNSO_BR_CD", .Item("SP_UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_KYORI_CD", .Item("BETU_KYORI_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SMPL_SAGYO", .Item("SMPL_SAGYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION", .Item("HOKAN_NIYAKU_CALCULATION").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_KEISAN_YN", .Item("HOKAN_NIYAKU_KEISAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NM", .Item("DENPYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CHANGE_KB", .Item("SOKO_CHANGE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEFAULT_SOKO_CD", .Item("DEFAULT_SOKO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_LIST_KB", .Item("PICK_LIST_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_OFB_KB", .Item("SEKY_OFB_KB").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号824
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_CD", .Item("TANTO_CD").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号824
            'START OU 要望番号2229
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_CURR_CD", .Item("ITEM_CURR_CD").ToString(), DBDataType.CHAR))
            'END OU 要望番号2229
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_HOKEN_AUTO_YN", .Item("UNSO_HOKEN_AUTO_YN").ToString(), DBDataType.CHAR))      'ADD 2018/10/22 002400
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_ORIG_CD", .Item("INKA_ORIG_CD").ToString(), DBDataType.CHAR))      'ADD 2018/10/25 要望番号001820
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INIT_OUTKA_PLAN_DATE_KB", .Item("INIT_OUTKA_PLAN_DATE_KB").ToString(), DBDataType.CHAR))    'ADD 2018/10/30 002192
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INIT_INKA_PLAN_DATE_KB", .Item("INIT_INKA_PLAN_DATE_KB").ToString(), DBDataType.CHAR))      'ADD 2018/10/30 002192
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_INKA_DATE_FLG", .Item("COA_INKA_DATE_FLG").ToString(), DBDataType.CHAR))    'ADD 2018/11/14 要望番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))    'ADD 2019/07/10 002520
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRODUCT_SEG_CD", .Item("PRODUCT_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TCUST_BPCD", .Item("TCUST_BPCD").ToString(), DBDataType.NVARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTariffM()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_MST_CD", .Item("SET_MST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD1", .Item("UNCHIN_TARIFF_CD1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD2", .Item("UNCHIN_TARIFF_CD2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("YOKO_TARIFF_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM090COMBO_SEIHINA")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMM090DAC.SQL_SELECT_COMBO_SEIHIN)

        ' SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", Me._Row.Item("KBN_LANG").ToString(), DBDataType.CHAR))

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM090DAC", "SelectComboSeihin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        'レコードをクリア
        ds.Tables("LMM090COMBO_SEIHINA").Rows.Clear()

        '取得データの格納先をマッピング
        map.Add("SEG_CD", "SEG_CD")
        map.Add("SEG_NM", "SEG_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM090COMBO_SEIHINA")

        Return ds

    End Function

#End Region

#Region "共通項目"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(新規時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(荷主マスタ主キー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamPrimaryKeyCustM()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃タリフセットマスタ主キー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamPrimaryKeyTariffM()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_MST_CD", .Item("SET_MST_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita()

        With Me._Row
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String _
                                 , ByVal brCd As String _
                                 ) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

End Class
