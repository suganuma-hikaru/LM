' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG050DAC : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "初期処理 SQL"

    ''' <summary>
    ''' 通貨マスタ通貨コード取得処理用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CURR_LIST As String = "SELECT                                       " & vbNewLine _
                                                & " MCR.CURR_CD     AS    CURR_CD         " & vbNewLine _
                                                & "FROM                                   " & vbNewLine _
                                                & "     COM_DB..M_CURR    MCR             " & vbNewLine _
                                                & "WHERE                                  " & vbNewLine _
                                                & "       MCR.SYS_DEL_FLG  =  '0'         " & vbNewLine _
                                                & "AND    MCR.UP_FLG       =  '00000'     " & vbNewLine _
                                                & "AND    MCR.BASE_CURR_CD = CASE WHEN (SELECT SEIQ_CURR_CD FROM $LM_MST$..M_SEIQTO WHERE NRS_BR_CD = @NRS_BR_CD AND SEIQTO_CD = @SEIQTO_CD) = '' THEN 'JPY'  " & vbNewLine _
                                                & "                               ELSE (SELECT SEIQ_CURR_CD FROM $LM_MST$..M_SEIQTO WHERE NRS_BR_CD = @NRS_BR_CD AND SEIQTO_CD = @SEIQTO_CD) END              " & vbNewLine

#End Region

#Region "検索処理 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HED As String = "SELECT                                                                             " & vbNewLine _
                                           & "          HED.SKYU_NO                      AS    SKYU_NO                           " & vbNewLine _
                                           & "          ,HED.STATE_KB                    AS    STATE_KB                          " & vbNewLine _
                                           & "          ,HED.SKYU_DATE                   AS    SKYU_DATE                         " & vbNewLine _
                                           & "          ,HED.NRS_BR_CD                   AS    NRS_BR_CD                         " & vbNewLine _
                                           & "          ,HED.SEIQTO_CD                   AS    SEIQTO_CD                         " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応START)                                           " & vbNewLine _
                                           & "          ,CASE WHEN HED.INV_CURR_CD = '' AND SQT.SEIQ_CURR_CD <> '' THEN SQT.SEIQ_CURR_CD      " & vbNewLine _
                                           & "                WHEN HED.INV_CURR_CD = '' AND SQT.SEIQ_CURR_CD =  '' THEN 'JPY'    " & vbNewLine _
                                           & "                ELSE HED.INV_CURR_CD       END    INV_CURR_CD                      " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応END)                                             " & vbNewLine _
                                           & "          ,HED.SEIQTO_PIC                  AS    SEIQTO_PIC                        " & vbNewLine _
                                           & "          ,HED.SEIQTO_NM                   AS    SEIQTO_NM                         " & vbNewLine _
                                           & "          ,CASE WHEN SUBSTRING(HED.SKYU_DATE, 1, 6) < ISNULL(RPT_CHG_START_YM.KBN_NM1, '202210')" & vbNewLine _
                                           & "               THEN ISNULL(OLD_BANK_MEIGI_NM.KBN_NM1, BNK.MEIGI_NM)                " & vbNewLine _
                                           & "               ELSE BNK.MEIGI_NM                                                   " & vbNewLine _
                                           & "               END                         AS    MEIGI_NM                          " & vbNewLine _
                                           & "          ,HED.NEBIKI_RT1                  AS    NEBIKI_RT1                        " & vbNewLine _
                                           & "          ,HED.NEBIKI_GK1                  AS    NEBIKI_GK1                        " & vbNewLine _
                                           & "          ,HED.TAX_GK1                     AS    TAX_GK1                           " & vbNewLine _
                                           & "          ,HED.TAX_HASU_GK1                AS    TAX_HASU_GK1                      " & vbNewLine _
                                           & "          ,HED.NEBIKI_RT2                  AS    NEBIKI_RT2                        " & vbNewLine _
                                           & "          ,HED.NEBIKI_GK2                  AS    NEBIKI_GK2                        " & vbNewLine _
                                           & "          ,HED.STORAGE_KB                  AS    STORAGE_KB                        " & vbNewLine _
                                           & "          ,HED.HANDLING_KB                 AS    HANDLING_KB                       " & vbNewLine _
                                           & "          ,HED.UNCHIN_KB                   AS    UNCHIN_KB                         " & vbNewLine _
                                           & "          ,HED.SAGYO_KB                    AS    SAGYO_KB                          " & vbNewLine _
                                           & "          ,HED.YOKOMOCHI_KB                AS    YOKOMOCHI_KB                      " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応START)                                           " & vbNewLine _
                                           & "          ,CASE WHEN HED.EX_MOTO_CURR_CD = '' THEN 'JPY'                           " & vbNewLine _
                                           & "                ELSE HED.EX_MOTO_CURR_CD   END   EX_MOTO_CURR_CD                   " & vbNewLine _
                                           & "          ,CASE WHEN HED.EX_RATE = 0.0 THEN 1.0                                    " & vbNewLine _
                                           & "                ELSE HED.EX_RATE           END   EX_RATE                           " & vbNewLine _
                                           & "          ,CASE WHEN HED.EX_SAKI_CURR_CD = '' THEN 'JPY'                           " & vbNewLine _
                                           & "                ELSE HED.EX_SAKI_CURR_CD   END   EX_SAKI_CURR_CD                   " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応START)                                           " & vbNewLine _
                                           & "          ,HED.CRT_KB                      AS    CRT_KB                            " & vbNewLine _
                                           & "          ,HED.UNCHIN_IMP_FROM_DATE        AS    UNCHIN_IMP_FROM_DATE              " & vbNewLine _
                                           & "          ,HED.SAGYO_IMP_FROM_DATE         AS    SAGYO_IMP_FROM_DATE               " & vbNewLine _
                                           & "          ,HED.YOKOMOCHI_IMP_FROM_DATE     AS    YOKOMOCHI_IMP_FROM_DATE           " & vbNewLine _
                                           & "          ,HED.REMARK                      AS    REMARK                            " & vbNewLine _
                                           & "          ,HED.RB_FLG                      AS    RB_FLG                            " & vbNewLine _
                                           & "          --(2015.04.10 要望番号2286対応)                                          " & vbNewLine _
                                           & "          ,HED.UNSO_WT                     AS    UNSO_WT                           " & vbNewLine _
                                           & "          ,HED.SAP_NO                      AS    SAP_NO                            " & vbNewLine _
                                           & "          --(2015.04.10 要望番号2286対応)                                          " & vbNewLine _
                                           & "          ,USR.USER_NM                     AS    SYS_ENT_USER_NM                   " & vbNewLine _
                                           & "          ,HED.SYS_UPD_DATE                AS    SYS_UPD_DATE                      " & vbNewLine _
                                           & "          ,HED.SYS_UPD_TIME                AS    SYS_UPD_TIME                      " & vbNewLine _
                                           & "          ,HED.SKYU_NO_RELATED             AS    SKYU_NO_RELATED                   " & vbNewLine _
                                           & "          ,SQT.DOC_SEI_YN                  AS    DOC_SEI_YN                        " & vbNewLine _
                                           & "          ,SQT.DOC_HUKU_YN                 AS    DOC_HUKU_YN                       " & vbNewLine _
                                           & "          ,SQT.DOC_HIKAE_YN                AS    DOC_HIKAE_YN                      " & vbNewLine _
                                           & "          ,SQT.DOC_KEIRI_YN                AS    DOC_KEIRI_YN                      " & vbNewLine _
                                           & "          ,(SELECT ISNULL(MAX(SKYU_SUB_NO),0)                                      " & vbNewLine _
                                           & "            FROM   $LM_TRN$..G_KAGAMI_DTL                                          " & vbNewLine _
                                           & "            WHERE    SKYU_NO       =    @SKYU_NO                                   " & vbNewLine _
                                           & "           )                               AS    MAX_SKYU_SUB_NO                   " & vbNewLine _
                                           & "          ,(SELECT                                                                 " & vbNewLine _
                                           & "                     ISNULL(SKYU_NO,'')                                            " & vbNewLine _
                                           & "            FROM   $LM_TRN$..G_KAGAMI_HED                                            " & vbNewLine _
                                           & "            WHERE  SKYU_NO = (SELECT SKYU_NO FROM $LM_TRN$..G_KAGAMI_HED             " & vbNewLine _
                                           & "                              WHERE SKYU_NO_RELATED = @SKYU_NO AND RB_FLG = '01')  " & vbNewLine _
                                           & "            ) AS AKA_SKYU_NO                                                       " & vbNewLine _
                                           & "FROM                                                                               " & vbNewLine _
                                           & "                $LM_TRN$..G_KAGAMI_HED    HED                                      " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..M_SEIQTO        SQT                                      " & vbNewLine _
                                           & "ON     SQT.NRS_BR_CD        =    HED.NRS_BR_CD                                     " & vbNewLine _
                                           & "AND    SQT.SEIQTO_CD        =    HED.SEIQTO_CD                                     " & vbNewLine _
                                           & "AND    SQT.SYS_DEL_FLG      =    '0'                                               " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..M_NRSBANK       BNK                                      " & vbNewLine _
                                           & "ON     BNK.MEIGI_CD         =    SQT.KOUZA_KB                                      " & vbNewLine _
                                           & "AND    BNK.SYS_DEL_FLG      =    '0'                                               " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..S_USER          USR                                      " & vbNewLine _
                                           & "ON     USR.USER_CD          =    HED.SYS_ENT_USER                                  " & vbNewLine _
                                           & "AND    USR.SYS_DEL_FLG      =    '0'                                               " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..Z_KBN AS RPT_CHG_START_YM                                " & vbNewLine _
                                           & "ON     RPT_CHG_START_YM.KBN_GROUP_CD = 'B043'                                      " & vbNewLine _
                                           & "AND    RPT_CHG_START_YM.KBN_CD       =  '01'                                       " & vbNewLine _
                                           & "AND    RPT_CHG_START_YM.SYS_DEL_FLG  =  '0'                                        " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..Z_KBN AS OLD_BANK_MEIGI_NM                               " & vbNewLine _
                                           & "ON     OLD_BANK_MEIGI_NM.KBN_GROUP_CD = 'B045'                                     " & vbNewLine _
                                           & "AND    OLD_BANK_MEIGI_NM.KBN_CD       =  SQT.KOUZA_KB                              " & vbNewLine _
                                           & "AND    OLD_BANK_MEIGI_NM.SYS_DEL_FLG  =  '0'                                       " & vbNewLine _
                                           & "WHERE                                                                              " & vbNewLine _
                                           & "--UPD20180810 依頼番号 : 002136       HED.SYS_DEL_FLG      =    '0'                                               " & vbNewLine _
                                           & "       HED.SYS_DEL_FLG      =    @SYS_DEL_FLG                                      " & vbNewLine _
                                           & "   AND HED.SKYU_NO          =    @SKYU_NO                                          " & vbNewLine


    ''' <summary>
    ''' 請求鑑詳細検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DTL As String = "SELECT                                                         " & vbNewLine _
                                           & "       DTL.SKYU_NO             AS    SKYU_NO                   " & vbNewLine _
                                           & "      ,DTL.SKYU_SUB_NO         AS    SKYU_SUB_NO               " & vbNewLine _
                                           & "      ,DTL.GROUP_KB            AS    GROUP_KB                  " & vbNewLine _
                                           & "      ,DTL.SEIQKMK_CD          AS    SEIQKMK_CD                " & vbNewLine _
                                           & "      ,KMK.SEIQKMK_NM          AS    SEIQKMK_NM                " & vbNewLine _
                                           & "      ,KMK.KEIRI_KB            AS    KEIRI_KB                  " & vbNewLine _
                                           & "      ,KMK.TAX_KB              AS    TAX_KB                    " & vbNewLine _
                                           & "      ,DTL.MAKE_SYU_KB         AS    MAKE_SYU_KB               " & vbNewLine _
                                           & "      ,KBN1.#KBN#              AS    MAKE_SYU_KB_NM            " & vbNewLine _
                                           & "      ,DTL.BUSYO_CD            AS    BUSYO_CD                  " & vbNewLine _
                                           & "--UPD 2016/09/06      ,KBN2.KBN_NM3            AS    KANJO_KAMOKU_CD           " & vbNewLine _
                                           & "      ,CASE WHEN DTL.JISYATASYA_KB = '02' THEN KBN2.KBN_NM7    " & vbNewLine _
                                           & "                                          ELSE KBN2.KBN_NM3    " & vbNewLine _
                                           & "       END  AS    KANJO_KAMOKU_CD                              " & vbNewLine _
                                           & "      ,KBN2.KBN_NM6            AS    KEIRI_BUMON_CD            " & vbNewLine _
                                           & "      ,KBN3.#KBN#              AS    TAX_KB_NM                 " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応START)                       " & vbNewLine _
                                           & "      ,CASE WHEN DTL.EX_RATE = 0.0 THEN 1.0                    " & vbNewLine _
                                           & "            ELSE DTL.EX_RATE           END   EX_RATE           " & vbNewLine _
                                           & "      ,CASE WHEN DTL.ITEM_CURR_CD = '' THEN 'JPY'              " & vbNewLine _
                                           & "            ELSE DTL.ITEM_CURR_CD   END   ITEM_CURR_CD         " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応END)                         " & vbNewLine _
                                           & "      ,DTL.KEISAN_TLGK         AS    KEISAN_TLGK               " & vbNewLine _
                                           & "      ,CAST(DTL.NEBIKI_RT AS DECIMAL(5,2))  AS    NEBIKI_RT    " & vbNewLine _
                                           & "      ,DTL.NEBIKI_RTGK         AS    NEBIKI_RTGK               " & vbNewLine _
                                           & "      ,DTL.NEBIKI_GK           AS    NEBIKI_GK                 " & vbNewLine _
                                           & "      ,DTL.TEKIYO              AS    TEKIYO                    " & vbNewLine _
                                           & "      ,DTL.PRINT_SORT          AS    PRINT_SORT                " & vbNewLine _
                                           & "      ,DTL.TEMPLATE_IMP_FLG    AS    TEMPLATE_IMP_FLG          " & vbNewLine _
                                           & "      ,DTL.TAX_CD_JDE          AS    TAX_CD_JDE                " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応START)                       " & vbNewLine _
                                           & "      ,ISNULL(CURR.CUR_RATE,1.0)         AS    BASE_EX_RATE    " & vbNewLine _
                                           & "      ,ISNULL(CURR.ROUND_POS,0)          AS    ROUND_POS       " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応END)                         " & vbNewLine _
                                           & "      ,DTL.JISYATASYA_KB       AS    JISYATASYA_KB   --ADD 2016/09/06   " & vbNewLine _
                                           & "      ,DTL.PRODUCT_SEG_CD      AS    PRODUCT_SEG_CD            " & vbNewLine _
                                           & "      ,DTL.ORIG_SEG_CD         AS    ORIG_SEG_CD               " & vbNewLine _
                                           & "      ,DTL.DEST_SEG_CD         AS    DEST_SEG_CD               " & vbNewLine _
                                           & "      ,DTL.TCUST_BPCD          AS    TCUST_BPCD                " & vbNewLine _
                                           & "      ,MBP.BP_NM1              AS    TCUST_BPNM                " & vbNewLine _
                                           & "      ,DTL.SEIQKMK_CD_S        AS    SEIQKMK_CD_S              " & vbNewLine _
                                           & "FROM                                                           " & vbNewLine _
                                           & "                $LM_TRN$..G_KAGAMI_DTL    DTL                  " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..M_SEIQKMK       KMK                  " & vbNewLine _
                                           & "ON     KMK.GROUP_KB            =    DTL.GROUP_KB               " & vbNewLine _
                                           & "AND    KMK.SEIQKMK_CD          =    DTL.SEIQKMK_CD             " & vbNewLine _
                                           & "AND    KMK.SEIQKMK_CD_S        =    DTL.SEIQKMK_CD_S           " & vbNewLine _
                                           & "AND    KMK.SYS_DEL_FLG         =    '0'                        " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..Z_KBN           KBN1                 " & vbNewLine _
                                           & "ON     KBN1.KBN_GROUP_CD       =   'K021'                      " & vbNewLine _
                                           & "AND    KBN1.KBN_CD             =    DTL.MAKE_SYU_KB            " & vbNewLine _
                                           & "AND    KBN1.SYS_DEL_FLG        =    '0'                        " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..Z_KBN           KBN2                 " & vbNewLine _
                                           & "ON     KBN2.KBN_GROUP_CD       =   'B006'                      " & vbNewLine _
                                           & "AND    KBN2.KBN_NM1            =    KMK.KEIRI_KB               " & vbNewLine _
                                           & "AND    KBN2.KBN_NM4            =    DTL.BUSYO_CD               " & vbNewLine _
                                           & "AND    KBN2.SYS_DEL_FLG        =    '0'                        " & vbNewLine _
                                           & "LEFT JOIN       $LM_MST$..Z_KBN           KBN3                 " & vbNewLine _
                                           & "ON     KBN3.KBN_GROUP_CD       =   'Z001'                      " & vbNewLine _
                                           & "AND    KBN3.KBN_CD             =    KMK.TAX_KB                 " & vbNewLine _
                                           & "AND    KBN3.SYS_DEL_FLG        =    '0'                        " & vbNewLine _
                                           & "          --(2014.08.21 多通貨対応START)                       " & vbNewLine _
                                           & "LEFT JOIN       COM_DB..M_CURR           CURR                  " & vbNewLine _
                                           & "ON     CURR.BASE_CURR_CD       =   ISNULL((SELECT BASE_CURR_CD FROM $LM_MST$..M_NRS_BR WHERE NRS_BR_CD = @NRS_BR_CD),'')  " & vbNewLine _
                                           & "AND    CURR.CURR_CD            =   DTL.ITEM_CURR_CD            " & vbNewLine _
                                           & "AND    CURR.SYS_DEL_FLG        =    '0'                        " & vbNewLine _
                                           & "AND    CURR.UP_FLG             =    '00000'                    " & vbNewLine _
                                           & "LEFT JOIN       $LM_TRN$..G_KAGAMI_HED    HED                  " & vbNewLine _
                                           & "ON     HED.SKYU_NO       =   @SKYU_NO                          " & vbNewLine _
                                           & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG1             " & vbNewLine _
                                           & "ON     SSG1.CNCT_SEG_CD     =   DTL.PRODUCT_SEG_CD         " & vbNewLine _
                                           & "AND    SSG1.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                           & "AND    SSG1.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                           & "AND    SSG1.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                           & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG2             " & vbNewLine _
                                           & "ON     SSG2.CNCT_SEG_CD     =   DTL.ORIG_SEG_CD            " & vbNewLine _
                                           & "AND    SSG2.DATA_TYPE_CD    =   '00001'                    " & vbNewLine _
                                           & "AND    SSG2.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                           & "AND    SSG2.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                           & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG3             " & vbNewLine _
                                           & "ON     SSG3.CNCT_SEG_CD     =   DTL.DEST_SEG_CD            " & vbNewLine _
                                           & "AND    SSG3.DATA_TYPE_CD    =   '00001'                    " & vbNewLine _
                                           & "AND    SSG3.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                           & "AND    SSG3.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                           & "LEFT JOIN       ABM_DB..M_BP          AS  MBP              " & vbNewLine _
                                           & "ON     MBP.BP_CD            =   DTL.TCUST_BPCD             " & vbNewLine _
                                           & "AND    MBP.SYS_DEL_FLG      =   '0'                        " & vbNewLine _
                                           & "WHERE                                                          " & vbNewLine _
                                           & "--UPD20180810 依頼番号 : 002136         DTL.SYS_DEL_FLG         =    '0'                        " & vbNewLine _
                                           & "       DTL.SYS_DEL_FLG   =    @SYS_DEL_FLG                     " & vbNewLine _
                                           & "  AND  DTL.SKYU_NO       =    @SKYU_NO                         " & vbNewLine _
                                           & "ORDER BY                                                       " & vbNewLine _
                                           & "       DTL.PRINT_SORT                                          " & vbNewLine _
                                           & "      ,DTL.SKYU_SUB_NO                                         " & vbNewLine


    ''' <summary>
    ''' セット料金の単価マスタが登録された荷主の主請求先(であるか否か) の検索(件数取得) 用
    ''' </summary>
    Private Const SELECT_SEIQTO_CUST_SET_PRICE As String = "" _
        & "SELECT                                                                                         " & vbNewLine _
        & "    COUNT(*) AS CNT                                                                            " & vbNewLine _
        & "FROM                                                                                           " & vbNewLine _
        & "    $LM_MST$..M_SEIQTO                                                                         " & vbNewLine _
        & "LEFT JOIN                                                                                      " & vbNewLine _
        & "   (SELECT                                                                                     " & vbNewLine _
        & "          NRS_BR_CD                                                                            " & vbNewLine _
        & "        , OYA_SEIQTO_CD                                                                        " & vbNewLine _
        & "        , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD, OYA_SEIQTO_CD                             " & vbNewLine _
        & "                            ORDER BY NEW_JOB_NO DESC, HOKAN_NIYAKU_CALCULATION DESC) AS ROW_NO " & vbNewLine _
        & "        , HOKAN_NIYAKU_CALCULATION                                                             " & vbNewLine _
        & "        , CUST_CD_L                                                                            " & vbNewLine _
        & "        , CUST_CD_M                                                                            " & vbNewLine _
        & "    FROM                                                                                       " & vbNewLine _
        & "        $LM_MST$..M_CUST                                                                       " & vbNewLine _
        & "    WHERE                                                                                      " & vbNewLine _
        & "        SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
        & "    ) AS CUST                                                                                  " & vbNewLine _
        & "        ON  CUST.NRS_BR_CD = M_SEIQTO.NRS_BR_CD                                                " & vbNewLine _
        & "        AND CUST.OYA_SEIQTO_CD = M_SEIQTO.SEIQTO_CD                                            " & vbNewLine _
        & "        AND CUST.ROW_NO = 1                                                                    " & vbNewLine _
        & "WHERE                                                                                          " & vbNewLine _
        & "    M_SEIQTO.NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
        & "AND M_SEIQTO.SEIQTO_CD = @SEIQTO_CD                                                            " & vbNewLine _
        & "AND EXISTS (                                                                                   " & vbNewLine _
        & "        SELECT                                                                                 " & vbNewLine _
        & "            'X'                                                                                " & vbNewLine _
        & "        FROM                                                                                   " & vbNewLine _
        & "            $LM_MST$..M_TANKA                                                                  " & vbNewLine _
        & "        WHERE                                                                                  " & vbNewLine _
        & "            M_TANKA.NRS_BR_CD = CUST.NRS_BR_CD                                                 " & vbNewLine _
        & "        AND M_TANKA.CUST_CD_L = CUST.CUST_CD_L                                                 " & vbNewLine _
        & "        AND M_TANKA.CUST_CD_M = CUST.CUST_CD_M                                                 " & vbNewLine _
        & "        AND M_TANKA.KIWARI_KB = '05'                                                           " & vbNewLine _
        & "        AND M_TANKA.SYS_DEL_FLG = '0')                                                         " & vbNewLine _
        & "AND M_SEIQTO.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
        & ""


    ''' <summary>
    ''' TSMC請求明細よりの部署コードとセット料金計算日数区分の組み合わせの取得 用
    ''' </summary>
    Private Const SELECT_TORIKOMI_DATA_BUSYO_CD_UNIT_KB_TSMC As String = "" _
        & "SELECT DISTINCT                                             " & vbNewLine _
        & "      ISNULL(KBN_B008.KBN_NM1, KBN_B007.KBN_CD) AS BUSYO_CD " & vbNewLine _
        & "    , I_SEKY_MEISAI_TSMC.UNIT_KB                            " & vbNewLine _
        & "FROM                                                        " & vbNewLine _
        & "    $LM_TRN$..I_SEKY_MEISAI_TSMC                            " & vbNewLine _
        & "LEFT JOIN                                                   " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_B008                             " & vbNewLine _
        & "        ON  KBN_B008.KBN_GROUP_CD = 'B008'                  " & vbNewLine _
        & "        AND KBN_B008.KBN_NM4 = I_SEKY_MEISAI_TSMC.NRS_BR_CD " & vbNewLine _
        & "        AND KBN_B008.KBN_NM2 = I_SEKY_MEISAI_TSMC.NRS_WH_CD " & vbNewLine _
        & "        AND KBN_B008.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "LEFT JOIN                                                   " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_B007                             " & vbNewLine _
        & "        ON  KBN_B007.KBN_GROUP_CD = 'B007'                  " & vbNewLine _
        & "        AND KBN_B007.KBN_NM2 = I_SEKY_MEISAI_TSMC.NRS_BR_CD " & vbNewLine _
        & "        AND KBN_B007.KBN_NM3 = '1'                          " & vbNewLine _
        & "        AND KBN_B007.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "WHERE                                                       " & vbNewLine _
        & "    I_SEKY_MEISAI_TSMC.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.SEIQTO_CD = @SEIQTO_CD               " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.INV_DATE_TO = @SKYU_DATE             " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.SYS_DEL_FLG = '0'                    " & vbNewLine _
        & ""


    ''' <summary>
    ''' TSMC請求明細よりの取込データの取得 用
    ''' </summary>
    Private Const SELECT_TORIKOMI_DATA_TSMC As String = "" _
        & "SELECT TOP 1                                                                                                  " & vbNewLine _
        & "      '' AS SKYU_SUB_NO                                                                                       " & vbNewLine _
        & "    , @GROUP_KB AS GROUP_KB                                                                                   " & vbNewLine _
        & "    , KBN_S064.KBN_NM2 AS SEIQKMK_CD                                                                          " & vbNewLine _
        & "    , M_SEIQKMK.SEIQKMK_NM                                                                                    " & vbNewLine _
        & "    , M_SEIQKMK.KEIRI_KB                                                                                      " & vbNewLine _
        & "    , @TAX_KB AS TAX_KB                                                                                       " & vbNewLine _
        & "    , @MAKE_SYU_KB AS MAKE_SYU_KB                                                                             " & vbNewLine _
        & "    , KBN_K021.KBN_NM1 AS MAKE_SYU_KB_NM                                                                      " & vbNewLine _
        & "    , ISNULL(KBN_B008.KBN_NM1, KBN_B007.KBN_CD) AS BUSYO_CD                                                   " & vbNewLine _
        & "    , CASE WHEN M_SOKO.WH_KB = '02'                                                                           " & vbNewLine _
        & "        THEN                                                                                                  " & vbNewLine _
        & "            CASE WHEN ISNULL(RTRIM(KBN_B006.KBN_NM7), '') <> ''                                               " & vbNewLine _
        & "                THEN KBN_B006.KBN_NM7                                                                         " & vbNewLine _
        & "                ELSE KBN_B006.KBN_NM3                                                                         " & vbNewLine _
        & "            END                                                                                               " & vbNewLine _
        & "        ELSE KBN_B006.KBN_NM3                                                                                 " & vbNewLine _
        & "      END AS KANJO_KAMOKU_CD                                                                                  " & vbNewLine _
        & "    , KBN_B006.KBN_NM6 AS KEIRI_BUMON_CD                                                                      " & vbNewLine _
        & "    , KBN_Z001.KBN_NM1 AS TAX_KB_NM                                                                           " & vbNewLine _
        & "    , ISNULL((                                                                                                " & vbNewLine _
        & "        SELECT                                                                                                " & vbNewLine _
        & "            SUM(TTLGK.SET_AMO)                                                                                " & vbNewLine _
        & "        FROM                                                                                                  " & vbNewLine _
        & "            $LM_TRN$..I_SEKY_MEISAI_TSMC AS TTLGK                                                             " & vbNewLine _
        & "        LEFT JOIN                                                                                             " & vbNewLine _
        & "            $LM_MST$..Z_KBN AS TTLGK_KBN_B008                                                                 " & vbNewLine _
        & "                ON  TTLGK_KBN_B008.KBN_GROUP_CD = 'B008'                                                      " & vbNewLine _
        & "                AND TTLGK_KBN_B008.KBN_NM4 = TTLGK.NRS_BR_CD                                                  " & vbNewLine _
        & "                AND TTLGK_KBN_B008.KBN_NM2 = TTLGK.NRS_WH_CD                                                  " & vbNewLine _
        & "                AND TTLGK_KBN_B008.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "        LEFT JOIN                                                                                             " & vbNewLine _
        & "            $LM_MST$..Z_KBN AS TTLGK_KBN_B007                                                                 " & vbNewLine _
        & "                ON  TTLGK_KBN_B007.KBN_GROUP_CD = 'B007'                                                      " & vbNewLine _
        & "                AND TTLGK_KBN_B007.KBN_NM2 = TTLGK.NRS_BR_CD                                                  " & vbNewLine _
        & "                AND TTLGK_KBN_B007.KBN_NM3 = '1'                                                              " & vbNewLine _
        & "                AND TTLGK_KBN_B007.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "        WHERE                                                                                                 " & vbNewLine _
        & "            TTLGK.NRS_BR_CD = I_SEKY_MEISAI_TSMC.NRS_BR_CD                                                    " & vbNewLine _
        & "        AND TTLGK.JOB_NO = I_SEKY_MEISAI_TSMC.JOB_NO                                                          " & vbNewLine _
        & "        AND TTLGK.SEIQTO_CD = I_SEKY_MEISAI_TSMC.SEIQTO_CD                                                    " & vbNewLine _
        & "        AND TTLGK.INV_DATE_TO = I_SEKY_MEISAI_TSMC.INV_DATE_TO                                                " & vbNewLine _
        & "        AND TTLGK.UNIT_KB = I_SEKY_MEISAI_TSMC.UNIT_KB                                                        " & vbNewLine _
        & "        AND TTLGK.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
        & "        AND ISNULL(TTLGK_KBN_B008.KBN_NM1, TTLGK_KBN_B007.KBN_CD) = ISNULL(KBN_B008.KBN_NM1, KBN_B007.KBN_CD) " & vbNewLine _
        & "      ), 0) AS KEISAN_TLGK                                                                                    " & vbNewLine _
        & "    , ISNULL((                                                                                                " & vbNewLine _
        & "        SELECT                                                                                                " & vbNewLine _
        & "            SUM(TTLGK.SET_OVER_AMO)                                                                           " & vbNewLine _
        & "        FROM                                                                                                  " & vbNewLine _
        & "            $LM_TRN$..I_SEKY_MEISAI_TSMC AS TTLGK                                                             " & vbNewLine _
        & "        LEFT JOIN                                                                                             " & vbNewLine _
        & "            $LM_MST$..Z_KBN AS TTLGK_KBN_B008                                                                 " & vbNewLine _
        & "                ON  TTLGK_KBN_B008.KBN_GROUP_CD = 'B008'                                                      " & vbNewLine _
        & "                AND TTLGK_KBN_B008.KBN_NM4 = TTLGK.NRS_BR_CD                                                  " & vbNewLine _
        & "                AND TTLGK_KBN_B008.KBN_NM2 = TTLGK.NRS_WH_CD                                                  " & vbNewLine _
        & "                AND TTLGK_KBN_B008.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "        LEFT JOIN                                                                                             " & vbNewLine _
        & "            $LM_MST$..Z_KBN AS TTLGK_KBN_B007                                                                 " & vbNewLine _
        & "                ON  TTLGK_KBN_B007.KBN_GROUP_CD = 'B007'                                                      " & vbNewLine _
        & "                AND TTLGK_KBN_B007.KBN_NM2 = TTLGK.NRS_BR_CD                                                  " & vbNewLine _
        & "                AND TTLGK_KBN_B007.KBN_NM3 = '1'                                                              " & vbNewLine _
        & "                AND TTLGK_KBN_B007.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "        WHERE                                                                                                 " & vbNewLine _
        & "            TTLGK.NRS_BR_CD = I_SEKY_MEISAI_TSMC.NRS_BR_CD                                                    " & vbNewLine _
        & "        AND TTLGK.JOB_NO = I_SEKY_MEISAI_TSMC.JOB_NO                                                          " & vbNewLine _
        & "        AND TTLGK.SEIQTO_CD = I_SEKY_MEISAI_TSMC.SEIQTO_CD                                                    " & vbNewLine _
        & "        AND TTLGK.INV_DATE_TO = I_SEKY_MEISAI_TSMC.INV_DATE_TO                                                " & vbNewLine _
        & "        AND TTLGK.UNIT_KB = I_SEKY_MEISAI_TSMC.UNIT_KB                                                        " & vbNewLine _
        & "        AND TTLGK.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
        & "        AND ISNULL(TTLGK_KBN_B008.KBN_NM1, TTLGK_KBN_B007.KBN_CD) = ISNULL(KBN_B008.KBN_NM1, KBN_B007.KBN_CD) " & vbNewLine _
        & "      ), 0) AS SET_OVER_AMO                                                                                   " & vbNewLine _
        & "    , M_SEIQTO.STORAGE_NR AS NEBIKI_RT                                                                        " & vbNewLine _
        & "    , M_SEIQTO.STORAGE_NG AS NEBIKI_GK                                                                        " & vbNewLine _
        & "    , CASE WHEN I_SEKY_MEISAI_TSMC.UNIT_KB = '90'                                                             " & vbNewLine _
        & "        THEN '９０日セット（倉庫）'                                                                           " & vbNewLine _
        & "        ELSE '４５日セット（コンテナ）'                                                                       " & vbNewLine _
        & "      END AS TEKIYO                                                                                           " & vbNewLine _
        & "    , '99' AS PRINT_SORT                                                                                      " & vbNewLine _
        & "    , '00' AS TEMPLATE_IMP_FLG  -- 未取込                                                                     " & vbNewLine _
        & "    , '0' AS SYS_DEL_FLG                                                                                      " & vbNewLine _
        & "    , '1' AS INS_FLG                                                                                          " & vbNewLine _
        & "    , 0 AS NEBIKI_RTGK                                                                                        " & vbNewLine _
        & "    , M_SOKO.WH_KB AS JISYATASYA_KB                                                                           " & vbNewLine _
        & "    , SEGMENT1.CNCT_SEG_CD AS PRODUCT_SEG_CD                                                                  " & vbNewLine _
        & "    , SEGMENT2.CNCT_SEG_CD AS ORIG_SEG_CD                                                                     " & vbNewLine _
        & "    , SEGMENT2.CNCT_SEG_CD AS DEST_SEG_CD                                                                     " & vbNewLine _
        & "    , CUST.TCUST_BPCD                                                                                         " & vbNewLine _
        & "    , M_BP.BP_NM1 AS TCUST_BPNM                                                                               " & vbNewLine _
        & "    , '' AS SEIQKMK_CD_S                                                                                      " & vbNewLine _
        & "    , I_SEKY_MEISAI_TSMC.UNIT_KB                                                                              " & vbNewLine _
        & "FROM                                                                                                          " & vbNewLine _
        & "    $LM_TRN$..I_SEKY_MEISAI_TSMC                                                                              " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..M_SEIQTO                                                                                        " & vbNewLine _
        & "        ON  M_SEIQTO.NRS_BR_CD = I_SEKY_MEISAI_TSMC.NRS_BR_CD                                                 " & vbNewLine _
        & "        AND M_SEIQTO.SEIQTO_CD = I_SEKY_MEISAI_TSMC.SEIQTO_CD                                                 " & vbNewLine _
        & "        AND M_SEIQTO.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "   (SELECT                                                                                                    " & vbNewLine _
        & "          NRS_BR_CD                                                                                           " & vbNewLine _
        & "        , OYA_SEIQTO_CD                                                                                       " & vbNewLine _
        & "        , HOKAN_NIYAKU_CALCULATION                                                                            " & vbNewLine _
        & "        , PRODUCT_SEG_CD                                                                                      " & vbNewLine _
        & "        , TCUST_BPCD                                                                                          " & vbNewLine _
        & "        , ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD, OYA_SEIQTO_CD                                            " & vbNewLine _
        & "                            ORDER BY NEW_JOB_NO DESC, HOKAN_NIYAKU_CALCULATION DESC) AS ROW_NO                " & vbNewLine _
        & "    FROM                                                                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST                                                                                      " & vbNewLine _
        & "    WHERE                                                                                                     " & vbNewLine _
        & "        SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
        & "    ) AS CUST                                                                                                 " & vbNewLine _
        & "        ON  CUST.NRS_BR_CD = M_SEIQTO.NRS_BR_CD                                                               " & vbNewLine _
        & "        AND CUST.OYA_SEIQTO_CD = M_SEIQTO.SEIQTO_CD                                                           " & vbNewLine _
        & "        AND CUST.ROW_NO = 1                                                                                   " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    ABM_DB..M_SEGMENT AS SEGMENT1                                                                             " & vbNewLine _
        & "        ON  SEGMENT1.CNCT_SEG_CD = CUST.PRODUCT_SEG_CD                                                        " & vbNewLine _
        & "        AND SEGMENT1.DATA_TYPE_CD = '00002'                                                                   " & vbNewLine _
        & "        AND SEGMENT1.KBN_LANG = @KBN_LANG                                                                     " & vbNewLine _
        & "        AND SEGMENT1.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_S064                                                                               " & vbNewLine _
        & "        ON  KBN_S064.KBN_GROUP_CD = 'S064'                                                                    " & vbNewLine _
        & "        AND KBN_S064.KBN_NM1 = @GROUP_KB                                                                      " & vbNewLine _
        & "        AND KBN_S064.KBN_NM3 = @TAX_KB                                                                        " & vbNewLine _
        & "        AND KBN_S064.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..M_SEIQKMK                                                                                       " & vbNewLine _
        & "        ON  M_SEIQKMK.GROUP_KB = @GROUP_KB                                                                    " & vbNewLine _
        & "        AND M_SEIQKMK.SEIQKMK_CD = KBN_S064.KBN_NM2                                                           " & vbNewLine _
        & "        AND M_SEIQKMK.SEIQKMK_CD_S = '  '                                                                     " & vbNewLine _
        & "        AND M_SEIQKMK.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_B008                                                                               " & vbNewLine _
        & "        ON  KBN_B008.KBN_GROUP_CD = 'B008'                                                                    " & vbNewLine _
        & "        AND KBN_B008.KBN_NM4 = I_SEKY_MEISAI_TSMC.NRS_BR_CD                                                   " & vbNewLine _
        & "        AND KBN_B008.KBN_NM2 = I_SEKY_MEISAI_TSMC.NRS_WH_CD                                                   " & vbNewLine _
        & "        AND KBN_B008.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_B007                                                                               " & vbNewLine _
        & "        ON  KBN_B007.KBN_GROUP_CD = 'B007'                                                                    " & vbNewLine _
        & "        AND KBN_B007.KBN_NM2 = I_SEKY_MEISAI_TSMC.NRS_BR_CD                                                   " & vbNewLine _
        & "        AND KBN_B007.KBN_NM3 = '1'                                                                            " & vbNewLine _
        & "        AND KBN_B007.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..M_SOKO                                                                                          " & vbNewLine _
        & "        ON  M_SOKO.WH_CD = I_SEKY_MEISAI_TSMC.NRS_WH_CD                                                       " & vbNewLine _
        & "        AND M_SOKO.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_B006                                                                               " & vbNewLine _
        & "        ON  KBN_B006.KBN_GROUP_CD = 'B006'                                                                    " & vbNewLine _
        & "        AND KBN_B006.KBN_NM4 = ISNULL(KBN_B008.KBN_NM1, KBN_B007.KBN_CD)                                      " & vbNewLine _
        & "        AND KBN_B006.KBN_NM1 = M_SEIQKMK.KEIRI_KB                                                             " & vbNewLine _
        & "        AND KBN_B006.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_K021                                                                               " & vbNewLine _
        & "        ON  KBN_K021.KBN_GROUP_CD = 'K021'                                                                    " & vbNewLine _
        & "        AND KBN_K021.KBN_CD = @MAKE_SYU_KB                                                                    " & vbNewLine _
        & "        AND KBN_K021.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KBN_Z001                                                                               " & vbNewLine _
        & "        ON  KBN_Z001.KBN_GROUP_CD = 'Z001'                                                                    " & vbNewLine _
        & "        AND KBN_Z001.KBN_CD = @TAX_KB                                                                         " & vbNewLine _
        & "        AND KBN_Z001.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    ABM_DB..Z_KBN AS KBN_TODOFUKEN                                                                            " & vbNewLine _
        & "        ON  KBN_TODOFUKEN.KBN_GROUP_CD = @ABM_DB_TODOFUKEN                                                    " & vbNewLine _
        & "        AND KBN_TODOFUKEN.KBN_LANG = @KBN_LANG                                                                " & vbNewLine _
        & "        AND KBN_TODOFUKEN.KBN_NM3 = LEFT(M_SOKO.JIS_CD, 2)                                                    " & vbNewLine _
        & "        AND KBN_TODOFUKEN.SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    ABM_DB..M_SEGMENT AS SEGMENT2                                                                             " & vbNewLine _
        & "        ON  SEGMENT2.DATA_TYPE_CD = '00001'                                                                   " & vbNewLine _
        & "        AND SEGMENT2.KBN_LANG = @KBN_LANG                                                                     " & vbNewLine _
        & "        AND SEGMENT2.KBN_GROUP_CD = KBN_TODOFUKEN.KBN_GRP_REF1                                                " & vbNewLine _
        & "        AND SEGMENT2.KBN_CD = KBN_TODOFUKEN.KBN_CD_REF1                                                       " & vbNewLine _
        & "        AND SEGMENT2.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
        & "LEFT JOIN                                                                                                     " & vbNewLine _
        & "    ABM_DB..M_BP                                                                                              " & vbNewLine _
        & "        ON  M_BP.BP_CD = CUST.TCUST_BPCD                                                                      " & vbNewLine _
        & "        AND M_BP.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
        & "WHERE                                                                                                         " & vbNewLine _
        & "    I_SEKY_MEISAI_TSMC.NRS_BR_CD = @NRS_BR_CD                                                                 " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.SEIQTO_CD = @SEIQTO_CD                                                                 " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.INV_DATE_TO = @SKYU_DATE                                                               " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.UNIT_KB = @UNIT_KB                                                                     " & vbNewLine _
        & "AND I_SEKY_MEISAI_TSMC.SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
        & "AND ISNULL(KBN_B008.KBN_NM1, KBN_B007.KBN_CD) = @BUSYO_CD                                                     " & vbNewLine _
        & ""


#End Region

#Region "排他チェック処理 SQL"

    ''' <summary>
    ''' 排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHK As String = "SELECT                                                         " & vbNewLine _
                                          & "       COUNT(HED.SKYU_NO)             AS    SELECT_CNT         " & vbNewLine _
                                          & "FROM                                                           " & vbNewLine _
                                          & "     $LM_TRN$..G_KAGAMI_HED    HED                             " & vbNewLine _
                                          & "WHERE                                                          " & vbNewLine _
                                          & "    HED.SKYU_NO            =    @SKYU_NO                       " & vbNewLine _
                                          & "AND    HED.SYS_UPD_DATE    =    @HAITA_DATE                    " & vbNewLine _
                                          & "AND    HED.SYS_UPD_TIME    =    @HAITA_TIME                    " & vbNewLine

#End Region

#If True Then   'ADD 2018/08/21 依頼番号 : 002136
#Region "復活処理 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPFUKKATSU_HED As String = "UPDATE                                   " & vbNewLine _
                                             & "	   $LM_TRN$..G_KAGAMI_HED            " & vbNewLine _
                                             & "SET                                      " & vbNewLine _
                                             & "         SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                             & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                             & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                             & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                             & "        ,SYS_DEL_FLG  = '0'              " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                             & "AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                             & "AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine

    ''' <summary>
    ''' 請求鑑詳細復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPFUKKATSU_DTL As String = "UPDATE                                   " & vbNewLine _
                                             & "	   $LM_TRN$..G_KAGAMI_DTL            " & vbNewLine _
                                             & "SET                                      " & vbNewLine _
                                             & "         SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                             & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                             & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                             & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                             & "        ,SYS_DEL_FLG  = '0'              " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                             & "--AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                             & "--AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine



#End Region
#End If

#Region "削除処理 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ論理削除SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDELETE_HED As String = "UPDATE                                   " & vbNewLine _
                                             & "	   $LM_TRN$..G_KAGAMI_HED            " & vbNewLine _
                                             & "SET                                      " & vbNewLine _
                                             & "         SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                             & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                             & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                             & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                             & "        ,SYS_DEL_FLG  = '1'              " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                             & "AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                             & "AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine

    ''' <summary>
    ''' 請求鑑詳細論理削除SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDELETE_DTL As String = "UPDATE                                   " & vbNewLine _
                                             & "	   $LM_TRN$..G_KAGAMI_DTL            " & vbNewLine _
                                             & "SET                                      " & vbNewLine _
                                             & "         SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                             & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                             & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                             & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                             & "        ,SYS_DEL_FLG  = '1'              " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                             & "    AND SYS_DEL_FLG   = '0'   --ADD 2018/08/23 依頼番号 : 002136 " & vbNewLine


#End Region

#Region "ステージアップ処理 SQL"

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPSTAGE_HED As String = "UPDATE                                   " & vbNewLine _
                                            & "	   $LM_TRN$..G_KAGAMI_HED               " & vbNewLine _
                                            & "SET                                      " & vbNewLine _
                                            & "         STATE_KB     = @STATE_KB        " & vbNewLine _
                                            & "        ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                            & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                            & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                            & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                            & "WHERE                                    " & vbNewLine _
                                            & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                            & "AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                            & "AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine

    ''' <summary>
    ''' 更新SQL（SAP連携情報）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPSAPNO_HED As String = "UPDATE                             " & vbNewLine _
                                            & "	   $LM_TRN$..G_KAGAMI_HED               " & vbNewLine _
                                            & "SET                                      " & vbNewLine _
                                            & "         SAP_NO       = @SAP_NO          " & vbNewLine _
                                            & "        ,SAP_OUT_USER = @SAP_OUT_USER    " & vbNewLine _
                                            & "        ,SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                            & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                            & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                            & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                            & "WHERE                                    " & vbNewLine _
                                            & "        SKYU_NO       = @SKYU_NO         " & vbNewLine

#End Region

#Region "システム共通項目の更新処理 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダシステム共通項目更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SYSDATA_HED As String = "UPDATE                                   " & vbNewLine _
                                             & "	   $LM_TRN$..G_KAGAMI_HED            " & vbNewLine _
                                             & "SET                                      " & vbNewLine _
                                             & "         SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                             & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                             & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                             & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "        SKYU_NO       = @SKYU_NO         " & vbNewLine _
                                             & "AND     SYS_UPD_DATE  = @HAITA_DATE      " & vbNewLine _
                                             & "AND     SYS_UPD_TIME  = @HAITA_TIME      " & vbNewLine

    ''' <summary>
    ''' 請求鑑詳細システム共通項目更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SYSDATA_DTL As String = "UPDATE                                   " & vbNewLine _
                                             & "	   $LM_TRN$..G_KAGAMI_DTL            " & vbNewLine _
                                             & "SET                                      " & vbNewLine _
                                             & "         SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                             & "        ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                             & "        ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                             & "        ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                             & "WHERE                                    " & vbNewLine _
                                             & "        SKYU_NO       = @SKYU_NO         " & vbNewLine


#End Region

#Region "新規登録 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_HED As String = "INSERT INTO                      " & vbNewLine _
                                                 & "    $LM_TRN$..G_KAGAMI_HED       " & vbNewLine _
                                                 & "    (                            " & vbNewLine _
                                                 & "     SKYU_NO                     " & vbNewLine _
                                                 & "    ,STATE_KB                    " & vbNewLine _
                                                 & "    ,SKYU_DATE                   " & vbNewLine _
                                                 & "    ,NRS_BR_CD                   " & vbNewLine _
                                                 & "    ,SEIQTO_CD                   " & vbNewLine _
                                                 & "    ,SEIQTO_PIC                  " & vbNewLine _
                                                 & "    ,SEIQTO_NM                   " & vbNewLine _
                                                 & "    ,NEBIKI_RT1                  " & vbNewLine _
                                                 & "    ,NEBIKI_GK1                  " & vbNewLine _
                                                 & "    ,TAX_GK1                     " & vbNewLine _
                                                 & "    ,TAX_HASU_GK1                " & vbNewLine _
                                                 & "    ,NEBIKI_RT2                  " & vbNewLine _
                                                 & "    ,NEBIKI_GK2                  " & vbNewLine _
                                                 & "    ,STORAGE_KB                  " & vbNewLine _
                                                 & "    ,HANDLING_KB                 " & vbNewLine _
                                                 & "    ,UNCHIN_KB                   " & vbNewLine _
                                                 & "    ,SAGYO_KB                    " & vbNewLine _
                                                 & "    ,YOKOMOCHI_KB                " & vbNewLine _
                                                 & "  --(2014.08.21 多通貨対応START) " & vbNewLine _
                                                 & "    ,INV_CURR_CD                 " & vbNewLine _
                                                 & "    ,EX_RATE                     " & vbNewLine _
                                                 & "    ,EX_MOTO_CURR_CD             " & vbNewLine _
                                                 & "    ,EX_SAKI_CURR_CD             " & vbNewLine _
                                                 & "  --(2014.08.21 多通貨対応END)   " & vbNewLine _
                                                 & "    ,CRT_KB                      " & vbNewLine _
                                                 & "    ,UNCHIN_IMP_FROM_DATE        " & vbNewLine _
                                                 & "    ,SAGYO_IMP_FROM_DATE         " & vbNewLine _
                                                 & "    ,YOKOMOCHI_IMP_FROM_DATE     " & vbNewLine _
                                                 & "    ,REMARK                      " & vbNewLine _
                                                 & "    ,SKYU_NO_RELATED             " & vbNewLine _
                                                 & "    ,RB_FLG                      " & vbNewLine _
                                                 & "  --(2015.04.10 要望番号2286対応)" & vbNewLine _
                                                 & "    ,UNSO_WT                     " & vbNewLine _
                                                 & "  --(2015.04.10 要望番号2286対応)" & vbNewLine _
                                                 & "    ,SYS_ENT_DATE                " & vbNewLine _
                                                 & "    ,SYS_ENT_TIME                " & vbNewLine _
                                                 & "    ,SYS_ENT_PGID                " & vbNewLine _
                                                 & "    ,SYS_ENT_USER                " & vbNewLine _
                                                 & "    ,SYS_UPD_DATE                " & vbNewLine _
                                                 & "    ,SYS_UPD_TIME                " & vbNewLine _
                                                 & "    ,SYS_UPD_PGID                " & vbNewLine _
                                                 & "    ,SYS_UPD_USER                " & vbNewLine _
                                                 & "    ,SYS_DEL_FLG                 " & vbNewLine _
                                                 & "    )                            " & vbNewLine _
                                                 & "VALUES                           " & vbNewLine _
                                                 & "    (                            " & vbNewLine _
                                                 & "     @SKYU_NO                    " & vbNewLine _
                                                 & "    ,@STATE_KB                   " & vbNewLine _
                                                 & "    ,@SKYU_DATE                  " & vbNewLine _
                                                 & "    ,@NRS_BR_CD                  " & vbNewLine _
                                                 & "    ,@SEIQTO_CD                  " & vbNewLine _
                                                 & "    ,@SEIQTO_PIC                 " & vbNewLine _
                                                 & "    ,@SEIQTO_NM                  " & vbNewLine _
                                                 & "    ,@NEBIKI_RT1                 " & vbNewLine _
                                                 & "    ,@NEBIKI_GK1                 " & vbNewLine _
                                                 & "    ,@TAX_GK1                    " & vbNewLine _
                                                 & "    ,@TAX_HASU_GK1               " & vbNewLine _
                                                 & "    ,@NEBIKI_RT2                 " & vbNewLine _
                                                 & "    ,@NEBIKI_GK2                 " & vbNewLine _
                                                 & "    ,@STORAGE_KB                 " & vbNewLine _
                                                 & "    ,@HANDLING_KB                " & vbNewLine _
                                                 & "    ,@UNCHIN_KB                  " & vbNewLine _
                                                 & "    ,@SAGYO_KB                   " & vbNewLine _
                                                 & "    ,@YOKOMOCHI_KB               " & vbNewLine _
                                                 & "  --(2014.08.21 多通貨対応START) " & vbNewLine _
                                                 & "    ,@INV_CURR_CD                " & vbNewLine _
                                                 & "    ,@EX_RATE                    " & vbNewLine _
                                                 & "    ,@EX_MOTO_CURR_CD            " & vbNewLine _
                                                 & "    ,@EX_SAKI_CURR_CD            " & vbNewLine _
                                                 & "  --(2014.08.21 多通貨対応END)   " & vbNewLine _
                                                 & "    ,@CRT_KB                     " & vbNewLine _
                                                 & "    ,@UNCHIN_IMP_FROM_DATE       " & vbNewLine _
                                                 & "    ,@SAGYO_IMP_FROM_DATE        " & vbNewLine _
                                                 & "    ,@YOKOMOCHI_IMP_FROM_DATE    " & vbNewLine _
                                                 & "    ,@REMARK                     " & vbNewLine _
                                                 & "    ,@SKYU_NO_RELATED            " & vbNewLine _
                                                 & "    ,@RB_FLG                     " & vbNewLine _
                                                 & "  --(2015.04.10 要望番号2286対応)" & vbNewLine _
                                                 & "    ,@UNSO_WT                    " & vbNewLine _
                                                 & "  --(2015.04.10 要望番号2286対応)" & vbNewLine _
                                                 & "    ,@SYS_ENT_DATE               " & vbNewLine _
                                                 & "    ,@SYS_ENT_TIME               " & vbNewLine _
                                                 & "    ,@SYS_ENT_PGID               " & vbNewLine _
                                                 & "    ,@SYS_ENT_USER               " & vbNewLine _
                                                 & "    ,@SYS_UPD_DATE               " & vbNewLine _
                                                 & "    ,@SYS_UPD_TIME               " & vbNewLine _
                                                 & "    ,@SYS_UPD_PGID               " & vbNewLine _
                                                 & "    ,@SYS_UPD_USER               " & vbNewLine _
                                                 & "    ,@SYS_DEL_FLG                " & vbNewLine _
                                                 & "    )                            " & vbNewLine


    ''' <summary>
    ''' 請求鑑詳細新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DTL As String = "INSERT INTO                    " & vbNewLine _
                                                & "    $LM_TRN$..G_KAGAMI_DTL     " & vbNewLine _
                                                & "    (                          " & vbNewLine _
                                                & "     SKYU_NO                   " & vbNewLine _
                                                & "    ,SKYU_SUB_NO               " & vbNewLine _
                                                & "    ,GROUP_KB                  " & vbNewLine _
                                                & "    ,SEIQKMK_CD                " & vbNewLine _
                                                & "    ,MAKE_SYU_KB               " & vbNewLine _
                                                & "    ,BUSYO_CD                  " & vbNewLine _
                                                & "    ,KEISAN_TLGK               " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応START)" & vbNewLine _
                                                & "    ,EX_RATE                   " & vbNewLine _
                                                & "    ,ITEM_CURR_CD              " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応END) " & vbNewLine _
                                                & "    ,NEBIKI_RT                 " & vbNewLine _
                                                & "    ,NEBIKI_RTGK               " & vbNewLine _
                                                & "    ,NEBIKI_GK                 " & vbNewLine _
                                                & "    ,TEKIYO                    " & vbNewLine _
                                                & "    ,PRINT_SORT                " & vbNewLine _
                                                & "    ,TEMPLATE_IMP_FLG          " & vbNewLine _
                                                & "     --2014/02/24 消費税対応 S " & vbNewLine _
                                                & "    ,TAX_CD_JDE                " & vbNewLine _
                                                & "     --2014/02/24 消費税対応 E " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応START)" & vbNewLine _
                                                & "    ,BASE_EX_RATE              " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応END) " & vbNewLine _
                                                & "    ,JISYATASYA_KB   --ADD 2016/09/06           " & vbNewLine _
                                                & "    ,PRODUCT_SEG_CD            " & vbNewLine _
                                                & "    ,ORIG_SEG_CD               " & vbNewLine _
                                                & "    ,DEST_SEG_CD               " & vbNewLine _
                                                & "    ,TCUST_BPCD                " & vbNewLine _
                                                & "    ,SEIQKMK_CD_S              " & vbNewLine _
                                                & "    ,SYS_ENT_DATE              " & vbNewLine _
                                                & "    ,SYS_ENT_TIME              " & vbNewLine _
                                                & "    ,SYS_ENT_PGID              " & vbNewLine _
                                                & "    ,SYS_ENT_USER              " & vbNewLine _
                                                & "    ,SYS_UPD_DATE              " & vbNewLine _
                                                & "    ,SYS_UPD_TIME              " & vbNewLine _
                                                & "    ,SYS_UPD_PGID              " & vbNewLine _
                                                & "    ,SYS_UPD_USER              " & vbNewLine _
                                                & "    ,SYS_DEL_FLG               " & vbNewLine _
                                                & "    )                          " & vbNewLine _
                                                & "VALUES                         " & vbNewLine _
                                                & "    (                          " & vbNewLine _
                                                & "     @SKYU_NO                  " & vbNewLine _
                                                & "    ,@SKYU_SUB_NO              " & vbNewLine _
                                                & "    ,@GROUP_KB                 " & vbNewLine _
                                                & "    ,@SEIQKMK_CD               " & vbNewLine _
                                                & "    ,@MAKE_SYU_KB              " & vbNewLine _
                                                & "    ,@BUSYO_CD                 " & vbNewLine _
                                                & "    ,@KEISAN_TLGK              " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応START)" & vbNewLine _
                                                & "    ,@EX_RATE                  " & vbNewLine _
                                                & "    ,@ITEM_CURR_CD             " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応END) " & vbNewLine _
                                                & "    ,@NEBIKI_RT                " & vbNewLine _
                                                & "    ,@NEBIKI_RTGK              " & vbNewLine _
                                                & "    ,@NEBIKI_GK                " & vbNewLine _
                                                & "    ,@TEKIYO                   " & vbNewLine _
                                                & "    ,@PRINT_SORT               " & vbNewLine _
                                                & "    ,@TEMPLATE_IMP_FLG         " & vbNewLine _
                                                & "    ,@TAX_CD_JDE               " & vbNewLine _
                                                & ",(SELECT CURR.CUR_RATE FROM COM_DB..M_CURR CURR                                                                                 " & vbNewLine _
                                                & "WHERE                                                                                                                          " & vbNewLine _
                                                & "BASE_CURR_CD = CASE WHEN (SELECT BR.BASE_CURR_CD FROM $LM_MST$..M_NRS_BR BR WHERE NRS_BR_CD = @NRS_BR_CD) = '' THEN 'JPY'      " & vbNewLine _
                                                & "                    ELSE (SELECT BR.BASE_CURR_CD FROM $LM_MST$..M_NRS_BR BR WHERE NRS_BR_CD = @NRS_BR_CD)       END            " & vbNewLine _
                                                & "AND                                                                                                                            " & vbNewLine _
                                                & "CURR_CD = CASE WHEN @ITEM_CURR_CD = '' THEN 'JPY' ELSE @ITEM_CURR_CD  END                                                         " & vbNewLine _
                                                & "AND                                                                                                                            " & vbNewLine _
                                                & "UP_FLG = '00000'                                                                                                               " & vbNewLine _
                                                & "AND                                                                                                                            " & vbNewLine _
                                                & "SYS_DEL_FLG = '0')                                                                                                             " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応START)" & vbNewLine _
                                                & "  --  ,@BASE_EX_RATE             " & vbNewLine _
                                                & "  --(2014.08.21 多通貨対応END) " & vbNewLine _
                                                & "    ,@JISYATASYA_KB     --ADD 2016/09/06             " & vbNewLine _
                                                & "    ,@PRODUCT_SEG_CD           " & vbNewLine _
                                                & "    ,@ORIG_SEG_CD              " & vbNewLine _
                                                & "    ,@DEST_SEG_CD              " & vbNewLine _
                                                & "    ,@TCUST_BPCD               " & vbNewLine _
                                                & "    ,@SEIQKMK_CD_S             " & vbNewLine _
                                                & "    ,@SYS_ENT_DATE             " & vbNewLine _
                                                & "    ,@SYS_ENT_TIME             " & vbNewLine _
                                                & "    ,@SYS_ENT_PGID             " & vbNewLine _
                                                & "    ,@SYS_ENT_USER             " & vbNewLine _
                                                & "    ,@SYS_UPD_DATE             " & vbNewLine _
                                                & "    ,@SYS_UPD_TIME             " & vbNewLine _
                                                & "    ,@SYS_UPD_PGID             " & vbNewLine _
                                                & "    ,@SYS_UPD_USER             " & vbNewLine _
                                                & "    ,@SYS_DEL_FLG              " & vbNewLine _
                                                & "    )                          " & vbNewLine

#End Region

#Region "未来データ取得 SQL"

    ''' <summary>
    ''' 未来データ取得(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_NEXT_DATA As String = "SELECT                                                         " & vbNewLine _
                                          & "       COUNT(HED.SKYU_NO)             AS    SELECT_CNT         " & vbNewLine _
                                          & "FROM                                                           " & vbNewLine _
                                          & "     $LM_TRN$..G_KAGAMI_HED    HED                             " & vbNewLine _
                                          & "WHERE                                                          " & vbNewLine _
                                          & "       HED.SKYU_DATE          >=  @SKYU_DATE                   " & vbNewLine _
                                          & "AND    HED.SEIQTO_CD          =   @SEIQTO_CD                   " & vbNewLine _
                                          & "AND    HED.SKYU_NO            <>  @SKYU_NO                     " & vbNewLine _
                                          & "AND    HED.STATE_KB           >=   '03'                        " & vbNewLine _
                                          & "AND    HED.SYS_DEL_FLG        =    '0'                         " & vbNewLine

#End Region

#Region "更新 SQL"

    ''' <summary>
    ''' 請求鑑ヘッダ更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED As String = "UPDATE                                                                 " & vbNewLine _
                                           & "    $LM_TRN$..G_KAGAMI_HED                                             " & vbNewLine _
                                           & "SET                                                                    " & vbNewLine _
                                           & "         STATE_KB                    =     @STATE_KB --ADD 2023/05/02 035323 " & vbNewLine _
                                           & "        ,SKYU_DATE                   =     @SKYU_DATE                  " & vbNewLine _
                                           & "        ,NRS_BR_CD                   =     @NRS_BR_CD                  " & vbNewLine _
                                           & "        ,SEIQTO_CD                   =     @SEIQTO_CD                  " & vbNewLine _
                                           & "        ,SEIQTO_PIC                  =     @SEIQTO_PIC                 " & vbNewLine _
                                           & "        ,SEIQTO_NM                   =     @SEIQTO_NM                  " & vbNewLine _
                                           & "        ,NEBIKI_RT1                  =     @NEBIKI_RT1                 " & vbNewLine _
                                           & "        ,NEBIKI_GK1                  =     @NEBIKI_GK1                 " & vbNewLine _
                                           & "        ,TAX_GK1                     =     @TAX_GK1                    " & vbNewLine _
                                           & "        ,TAX_HASU_GK1                =     @TAX_HASU_GK1               " & vbNewLine _
                                           & "        ,NEBIKI_RT2                  =     @NEBIKI_RT2                 " & vbNewLine _
                                           & "        ,NEBIKI_GK2                  =     @NEBIKI_GK2                 " & vbNewLine _
                                           & "        ,STORAGE_KB                  =     @STORAGE_KB                 " & vbNewLine _
                                           & "        ,HANDLING_KB                 =     @HANDLING_KB                " & vbNewLine _
                                           & "        ,UNCHIN_KB                   =     @UNCHIN_KB                  " & vbNewLine _
                                           & "        ,SAGYO_KB                    =     @SAGYO_KB                   " & vbNewLine _
                                           & "        ,YOKOMOCHI_KB                =     @YOKOMOCHI_KB               " & vbNewLine _
                                           & "        --(2014.09.01)追加START                                        " & vbNewLine _
                                           & "        ,INV_CURR_CD                 =     @INV_CURR_CD                " & vbNewLine _
                                           & "        ,EX_RATE                     =     @EX_RATE                    " & vbNewLine _
                                           & "        ,EX_MOTO_CURR_CD             =     @EX_MOTO_CURR_CD            " & vbNewLine _
                                           & "        ,EX_SAKI_CURR_CD             =     @EX_SAKI_CURR_CD            " & vbNewLine _
                                           & "        --(2014.09.01)追加END                                          " & vbNewLine _
                                           & "        ,UNCHIN_IMP_FROM_DATE        =     @UNCHIN_IMP_FROM_DATE       " & vbNewLine _
                                           & "        ,SAGYO_IMP_FROM_DATE         =     @SAGYO_IMP_FROM_DATE        " & vbNewLine _
                                           & "        ,YOKOMOCHI_IMP_FROM_DATE     =     @YOKOMOCHI_IMP_FROM_DATE    " & vbNewLine _
                                           & "        ,REMARK                      =     @REMARK                     " & vbNewLine _
                                           & "        ,UNSO_WT                     =     @UNSO_WT                    " & vbNewLine _
                                           & "        ,SYS_UPD_DATE                =     @SYS_UPD_DATE               " & vbNewLine _
                                           & "        ,SYS_UPD_TIME                =     @SYS_UPD_TIME               " & vbNewLine _
                                           & "        ,SYS_UPD_PGID                =     @SYS_UPD_PGID               " & vbNewLine _
                                           & "        ,SYS_UPD_USER                =     @SYS_UPD_USER               " & vbNewLine _
                                           & "WHERE                                                                  " & vbNewLine _
                                           & "        SKYU_NO                      =     @SKYU_NO                    " & vbNewLine _
                                           & "AND     SYS_UPD_DATE                 =     @HAITA_DATE                 " & vbNewLine _
                                           & "AND     SYS_UPD_TIME                 =     @HAITA_TIME                 " & vbNewLine
    ''' <summary>
    ''' 請求鑑詳細更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DTL As String = "UPDATE                                                " & vbNewLine _
                                           & "    $LM_TRN$..G_KAGAMI_DTL                            " & vbNewLine _
                                           & "SET                                                   " & vbNewLine _
                                           & "     GROUP_KB            =      @GROUP_KB             " & vbNewLine _
                                           & "    ,SEIQKMK_CD          =      @SEIQKMK_CD           " & vbNewLine _
                                           & "    ,MAKE_SYU_KB         =      @MAKE_SYU_KB          " & vbNewLine _
                                           & "    ,BUSYO_CD            =      @BUSYO_CD             " & vbNewLine _
                                           & "    ,KEISAN_TLGK         =      @KEISAN_TLGK          " & vbNewLine _
                                           & "    --(2014.09.01)追加START                           " & vbNewLine _
                                           & "    ,EX_RATE             =      @EX_RATE              " & vbNewLine _
                                           & "    ,ITEM_CURR_CD        =      @ITEM_CURR_CD         " & vbNewLine _
                                           & "    --(2014.09.01)追加END                             " & vbNewLine _
                                           & "    ,NEBIKI_RT           =      @NEBIKI_RT            " & vbNewLine _
                                           & "    ,NEBIKI_RTGK         =      @NEBIKI_RTGK          " & vbNewLine _
                                           & "    ,NEBIKI_GK           =      @NEBIKI_GK            " & vbNewLine _
                                           & "    ,TEKIYO              =      @TEKIYO               " & vbNewLine _
                                           & "    ,PRINT_SORT          =      @PRINT_SORT           " & vbNewLine _
                                           & "    ,TEMPLATE_IMP_FLG    =      @TEMPLATE_IMP_FLG     " & vbNewLine _
                                           & "    ,TAX_CD_JDE          =      @TAX_CD_JDE           " & vbNewLine _
                                           & "    --(2014.09.01)追加START                           " & vbNewLine _
                                           & "    ,BASE_EX_RATE        =      @BASE_EX_RATE         " & vbNewLine _
                                           & "    --(2014.09.01)追加END                             " & vbNewLine _
                                           & "    ,PRODUCT_SEG_CD      =      @PRODUCT_SEG_CD       " & vbNewLine _
                                           & "    ,ORIG_SEG_CD         =      @ORIG_SEG_CD          " & vbNewLine _
                                           & "    ,DEST_SEG_CD         =      @DEST_SEG_CD          " & vbNewLine _
                                           & "    ,TCUST_BPCD          =      @TCUST_BPCD           " & vbNewLine _
                                           & "    ,SEIQKMK_CD_S        =      @SEIQKMK_CD_S         " & vbNewLine _
                                           & "    ,SYS_UPD_DATE        =      @SYS_UPD_DATE         " & vbNewLine _
                                           & "    ,SYS_UPD_TIME        =      @SYS_UPD_TIME         " & vbNewLine _
                                           & "    ,SYS_UPD_PGID        =      @SYS_UPD_PGID         " & vbNewLine _
                                           & "    ,SYS_UPD_USER        =      @SYS_UPD_USER         " & vbNewLine _
                                           & "    ,SYS_DEL_FLG         =      @SYS_DEL_FLG          " & vbNewLine _
                                           & "WHERE   SKYU_NO          =      @SKYU_NO              " & vbNewLine _
                                           & "AND     SKYU_SUB_NO      =      @SKYU_SUB_NO          " & vbNewLine


#End Region

#Region "新規登録時チェック SQL"

    ''' <summary>
    ''' 請求日関連チェック(請求マスタ検索処理)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_CHK_SEIQTO_DATE As String = "SELECT                               " & vbNewLine _
                                             & "    SQT.CLOSE_KB	AS	CLOSE_KB       " & vbNewLine _
                                             & "   ,KBN.KBN_NM1	    AS	CLOSE_KB_NM    " & vbNewLine _
                                             & "FROM                                   " & vbNewLine _
                                             & "    $LM_MST$..M_SEIQTO SQT             " & vbNewLine _
                                             & "LEFT JOIN       $LM_MST$..Z_KBN    KBN " & vbNewLine _
                                             & "ON  KBN.KBN_GROUP_CD = 'S008'          " & vbNewLine _
                                             & "AND KBN.KBN_CD       =  SQT.CLOSE_KB   " & vbNewLine _
                                             & "AND KBN.SYS_DEL_FLG  =  '0'            " & vbNewLine _
                                             & "WHERE                                  " & vbNewLine _
                                             & "    SQT.NRS_BR_CD   =    @NRS_BR_CD    " & vbNewLine _
                                             & "AND SQT.SEIQTO_CD   =    @SEIQTO_CD    " & vbNewLine _
                                             & "AND SQT.SYS_DEL_FLG =    '0'           " & vbNewLine

    ''' <summary>
    ''' 鑑最低保証関連チェック(請求マスタ検索処理)
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Const SQL_CHK_SEIQTO_MIN As String = "SELECT                                                                        " & vbNewLine _
                                             & "    SQT.NRS_BR_CD                                  AS NRS_BR_CD                 " & vbNewLine _
                                             & "   ,SQT.SEIQTO_CD                                  AS SEIQTO_CD                 " & vbNewLine _
                                             & "   ,SQT.TOTAL_MIN_SEIQ_AMT                         AS TOTAL_MIN_SEIQ_AMT        " & vbNewLine _
                                             & "   ,SQT.STORAGE_TOTAL_FLG                          AS STORAGE_TOTAL_FLG         " & vbNewLine _
                                             & "   ,SQT.HANDLING_TOTAL_FLG                         AS HANDLING_TOTAL_FLG        " & vbNewLine _
                                             & "   ,SQT.UNCHIN_TOTAL_FLG                           AS UNCHIN_TOTAL_FLG          " & vbNewLine _
                                             & "   ,SQT.SAGYO_TOTAL_FLG                            AS SAGYO_TOTAL_FLG           " & vbNewLine _
                                             & "   ,SQT.SEIQ_CURR_CD                               AS SEIQ_CURR_CD              " & vbNewLine _
                                             & "   ,SQT.STORAGE_MIN_AMT                            AS STORAGE_MIN_AMT           " & vbNewLine _
                                             & "   ,SQT.STORAGE_OTHER_MIN_AMT                      AS STORAGE_OTHER_MIN_AMT     " & vbNewLine _
                                             & "   ,SQT.HANDLING_MIN_AMT                           AS HANDLING_MIN_AMT          " & vbNewLine _
                                             & "   ,SQT.HANDLING_OTHER_MIN_AMT                     AS HANDLING_OTHER_MIN_AMT    " & vbNewLine _
                                             & "   ,SQT.UNCHIN_MIN_AMT                             AS UNCHIN_MIN_AMT            " & vbNewLine _
                                             & "   ,SQT.SAGYO_MIN_AMT                              AS SAGYO_MIN_AMT             " & vbNewLine _
                                             & "FROM                                                                            " & vbNewLine _
                                             & "    $LM_MST$..M_SEIQTO SQT                                                      " & vbNewLine _
                                             & "WHERE                                                                           " & vbNewLine _
                                             & "    SQT.NRS_BR_CD   =    @NRS_BR_CD                                             " & vbNewLine _
                                             & "AND SQT.SEIQTO_CD   =    @SEIQTO_CD                                             " & vbNewLine _
                                             & "AND SQT.SYS_DEL_FLG =    '0'                                                    " & vbNewLine



    ''' <summary>
    ''' 通貨マスタ(COM_DB)重複チェック＋契約通貨,小数点桁数取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_CHK_CURR_INFO As String = "SELECT                                " & vbNewLine _
                              & "(SELECT                                              " & vbNewLine _
                              & "          BA_SEIQ.SEIQ_CURR_CD                       " & vbNewLine _
                              & "	 FROM $LM_MST$..M_SEIQTO BA_SEIQ                  " & vbNewLine _
                              & "	 WHERE BA_SEIQ.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                              & "    AND   BA_SEIQ.SEIQTO_CD = @SEIQTO_CD              " & vbNewLine _
                              & "    AND   BA_SEIQ.SYS_DEL_FLG = '0'                   " & vbNewLine _
                              & "	 )                     AS SEIQ_CURR_CD            " & vbNewLine _
                              & "   ,BASIS.ITEM_CURR_CD     AS ITEM_CURR_CD           " & vbNewLine _
                              & "   ,CURR.ROUND_POS         AS ROUND_POS              " & vbNewLine _
                              & "   ,CURR.CUR_RATE          AS CUR_RATE               " & vbNewLine _
                              & "  FROM                                               " & vbNewLine _
                              & "     (SELECT                                         " & vbNewLine _
                              & "	        ITEM_CURR_CD  AS ITEM_CURR_CD             " & vbNewLine _
                              & "	  FROM $LM_MST$..M_CUST AS HOKAN                  " & vbNewLine _
                              & "	  WHERE HOKAN.NRS_BR_CD= @NRS_BR_CD               " & vbNewLine _
                              & "	  AND HOKAN.HOKAN_SEIQTO_CD= @SEIQTO_CD           " & vbNewLine _
                              & "	  AND HOKAN.SYS_DEL_FLG = '0'                     " & vbNewLine _
                              & "      UNION ALL                                      " & vbNewLine _
                              & "      SELECT                                         " & vbNewLine _
                              & "	        ITEM_CURR_CD AS ITEM_CURR_CD              " & vbNewLine _
                              & "	  FROM $LM_MST$..M_CUST AS NIYAKU                 " & vbNewLine _
                              & "	  WHERE NIYAKU.NRS_BR_CD= @NRS_BR_CD              " & vbNewLine _
                              & "	  AND NIYAKU.NIYAKU_SEIQTO_CD= @SEIQTO_CD         " & vbNewLine _
                              & "	  AND NIYAKU.SYS_DEL_FLG = '0'                    " & vbNewLine _
                              & "      UNION ALL                                      " & vbNewLine _
                              & "      SELECT                                         " & vbNewLine _
                              & "	        ITEM_CURR_CD AS ITEM_CURR_CD              " & vbNewLine _
                              & "	  FROM $LM_MST$..M_CUST AS UNCHIN                 " & vbNewLine _
                              & "	  WHERE UNCHIN.NRS_BR_CD= @NRS_BR_CD              " & vbNewLine _
                              & "	  AND UNCHIN.UNCHIN_SEIQTO_CD= @SEIQTO_CD         " & vbNewLine _
                              & "	  AND UNCHIN.SYS_DEL_FLG = '0'                    " & vbNewLine _
                              & "      UNION ALL                                      " & vbNewLine _
                              & "      SELECT                                         " & vbNewLine _
                              & "	        ITEM_CURR_CD AS ITEM_CURR_CD              " & vbNewLine _
                              & "	  FROM $LM_MST$..M_CUST AS SAGYO                  " & vbNewLine _
                              & "	  WHERE SAGYO.NRS_BR_CD= @NRS_BR_CD               " & vbNewLine _
                              & "	  AND SAGYO.SAGYO_SEIQTO_CD= @SEIQTO_CD           " & vbNewLine _
                              & "	  AND SAGYO.SYS_DEL_FLG = '0'                     " & vbNewLine _
                              & "     ) AS BASIS                                      " & vbNewLine _
                              & "  LEFT OUTER JOIN COM_DB..M_CURR AS CURR             " & vbNewLine _
                              & "  ON                                                 " & vbNewLine _
                              & "      CURR.BASE_CURR_CD = (SELECT SEIQ.SEIQ_CURR_CD  FROM $LM_MST$..M_SEIQTO AS SEIQ  WHERE SEIQ.NRS_BR_CD = @NRS_BR_CD AND SEIQ.SEIQTO_CD = @SEIQTO_CD AND   SEIQ.SYS_DEL_FLG = '0')     " & vbNewLine _
                              & "  AND CURR.CURR_CD = BASIS.ITEM_CURR_CD              " & vbNewLine _
                              & "  AND CURR.UP_FLG = '00000'                          " & vbNewLine _
                              & "  AND CURR.SYS_DEL_FLG = '0'                         " & vbNewLine _
                              & "  GROUP BY                                           " & vbNewLine _
                              & "     BASIS.ITEM_CURR_CD                              " & vbNewLine _
                              & "    ,CURR.ROUND_POS                                  " & vbNewLine _
                              & "    ,CURR.CUR_RATE                                   " & vbNewLine _
                              & "  HAVING BASIS.ITEM_CURR_CD <> ''                    " & vbNewLine



    ''' <summary>
    ''' データ重複チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_REPEAT_CHK As String = "SELECT                                                " & vbNewLine _
                                         & "       COUNT(HED.SKYU_NO)      AS    SELECT_CNT         " & vbNewLine _
                                         & "FROM                                                    " & vbNewLine _
                                         & "     $LM_TRN$..G_KAGAMI_HED    HED                      " & vbNewLine _
                                         & "WHERE                                                   " & vbNewLine _
                                         & "       HED.NRS_BR_CD    =    @NRS_BR_CD                 " & vbNewLine _
                                         & "AND    HED.SEIQTO_CD    =    @SEIQTO_CD                 " & vbNewLine _
                                         & "AND    HED.SKYU_DATE    =    @SKYU_DATE                 " & vbNewLine _
                                         & "AND    HED.CRT_KB       =    @CRT_KB                    " & vbNewLine _
                                         & "AND    HED.SYS_DEL_FLG  =    '0'                        " & vbNewLine

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
    Private Const SQL_KEIRI_MODOSHI_CHK As String = "SELECT                                         " & vbNewLine _
                                         & "       COUNT(HED.SKYU_NO)      AS    SELECT_CNT         " & vbNewLine _
                                         & "FROM                                                    " & vbNewLine _
                                         & "     $LM_TRN$..G_KAGAMI_HED    HED                      " & vbNewLine _
                                         & "WHERE                                                   " & vbNewLine _
                                         & "       HED.SKYU_NO    =    @SKYU_NO                     " & vbNewLine _
                                         & "AND    HED.SKYU_NO_RELATED    <>    ''                  " & vbNewLine _
                                         & "AND    HED.SYS_DEL_FLG  =    '0'                        " & vbNewLine
    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end

    ''' <summary>
    ''' 経理取込チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_KEIRI_TORIKOMI_CHK As String = "SELECT                                        " & vbNewLine _
                                         & "       COUNT(HED.SKYU_NO)      AS    SELECT_CNT         " & vbNewLine _
                                         & "FROM                                                    " & vbNewLine _
                                         & "     $LM_TRN$..G_KAGAMI_HED    HED                      " & vbNewLine _
                                         & "WHERE                                                   " & vbNewLine _
                                         & "       HED.NRS_BR_CD    =    @NRS_BR_CD                 " & vbNewLine _
                                         & "AND    HED.SEIQTO_CD    =    @SEIQTO_CD                 " & vbNewLine _
                                         & "--  追加  要望番号 Notes1729 2012.12.27                 " & vbNewLine _
                                         & "AND    HED.SKYU_DATE    =    @SKYU_DATE                 " & vbNewLine _
                                         & "--  追加  要望番号 Notes1729 2012.12.27                 " & vbNewLine _
                                         & "AND    HED.CRT_KB       =    @CRT_KB                    " & vbNewLine _
                                         & "AND    HED.STATE_KB     <=   '02'                       " & vbNewLine _
                                         & "AND    HED.RB_FLG       =    '00'                       " & vbNewLine _
                                         & "AND    HED.SYS_DEL_FLG  =    '0'                        " & vbNewLine

    ''' <summary>
    ''' 未来データ取得(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_NEXT_DATA_CHK As String = "SELECT                                                     " & vbNewLine _
                                          & "       COUNT(HED.SKYU_NO)             AS    SELECT_CNT         " & vbNewLine _
                                          & "FROM                                                           " & vbNewLine _
                                          & "     $LM_TRN$..G_KAGAMI_HED    HED                             " & vbNewLine _
                                          & "WHERE                                                          " & vbNewLine _
                                          & "       HED.NRS_BR_CD    =    @NRS_BR_CD                        " & vbNewLine _
                                          & "AND    HED.SEIQTO_CD    =    @SEIQTO_CD                        " & vbNewLine _
                                          & "AND    HED.CRT_KB       =    @CRT_KB                           " & vbNewLine _
                                          & "AND    HED.SKYU_DATE    >=   @SKYU_DATE                        " & vbNewLine _
                                          & "AND    HED.SYS_DEL_FLG  =    '0'                               " & vbNewLine

#End Region

#Region "SAP処理 SQL"

    ''' <summary>
    ''' SAP出力処理対象取得用
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_SAP_OUT_SELECT As String = "SELECT                                                                                        " & vbNewLine _
                                              & "      HED.SKYU_NO                                                                             " & vbNewLine _
                                              & "--  , HED.STATE_KB                                                                            " & vbNewLine _
                                              & "    , HED.SKYU_DATE                                                                           " & vbNewLine _
                                              & "--  , HED.NRS_BR_CD                                                                           " & vbNewLine _
                                              & "--  , HED.SEIQTO_CD                                                                           " & vbNewLine _
                                              & "    , HED.TAX_GK1                                                                             " & vbNewLine _
                                              & "    , HED.TAX_HASU_GK1                                                                        " & vbNewLine _
                                              & "    , CASE                                                                                    " & vbNewLine _
                                              & "        WHEN HED.INV_CURR_CD = '' AND ISNULL(SEI.SEIQ_CURR_CD, '') <> '' THEN SEI.SEIQ_CURR_CD" & vbNewLine _
                                              & "        WHEN HED.INV_CURR_CD = '' AND ISNULL(SEI.SEIQ_CURR_CD, '')  = '' THEN 'JPY'           " & vbNewLine _
                                              & "        ELSE                                                                  HED.INV_CURR_CD " & vbNewLine _
                                              & "      END AS INV_CURR_CD                                                                      " & vbNewLine _
                                              & "    , HED.REMARK                                                                              " & vbNewLine _
                                              & "    , HED.RB_FLG                                                                              " & vbNewLine _
                                              & "--  , HED.SYS_ENT_USER                                                                        " & vbNewLine _
                                              & "    , ISNULL(USR.USER_NM, '') AS USER_NM                                                      " & vbNewLine _
                                              & "    , ISNULL(SEI.NRS_KEIRI_CD2, '') AS NRS_KEIRI_CD2                                          " & vbNewLine _
                                              & "    , '1000' AS COMP_CD                                                                       " & vbNewLine _
                                              & "--  , DTL.SKYU_SUB_NO                                                                         " & vbNewLine _
                                              & "--  , DTL.GROUP_KB                                                                            " & vbNewLine _
                                              & "--  , DTL.SEIQKMK_CD                                                                          " & vbNewLine _
                                              & "--  , DTL.BUSYO_CD                                                                            " & vbNewLine _
                                              & "    , DTL.KEISAN_TLGK                                                                         " & vbNewLine _
                                              & "    , CASE                                                                                    " & vbNewLine _
                                              & "        WHEN DTL.ITEM_CURR_CD = '' THEN 'JPY'                                                 " & vbNewLine _
                                              & "        ELSE                            DTL.ITEM_CURR_CD                                      " & vbNewLine _
                                              & "      END AS ITEM_CURR_CD                                                                     " & vbNewLine _
                                              & "    , DTL.NEBIKI_RTGK                                                                         " & vbNewLine _
                                              & "    , DTL.NEBIKI_GK                                                                           " & vbNewLine _
                                              & "    , DTL.TEKIYO                                                                              " & vbNewLine _
                                              & "    , DTL.TAX_CD_JDE                                                                          " & vbNewLine _
                                              & "--  , DTL.JISYATASYA_KB                                                                       " & vbNewLine _
                                              & "    , DTL.PRODUCT_SEG_CD                                                                      " & vbNewLine _
                                              & "    , DTL.ORIG_SEG_CD                                                                         " & vbNewLine _
                                              & "    , DTL.DEST_SEG_CD                                                                         " & vbNewLine _
                                              & "    , DTL.TCUST_BPCD                                                                          " & vbNewLine _
                                              & "    , ISNULL(                                                                                 " & vbNewLine _
                                              & "        CASE DTL.JISYATASYA_KB                                                                " & vbNewLine _
                                              & "            WHEN '02' THEN                                                                    " & vbNewLine _
                                              & "                CASE WHEN (NOT (KB2.KBN_NM7 IS NULL OR RTRIM(KB2.KBN_NM7) = ''))              " & vbNewLine _
                                              & "                    THEN KB2.KBN_NM7                                                          " & vbNewLine _
                                              & "                    ELSE KB2.KBN_NM3                                                          " & vbNewLine _
                                              & "                END                                                                           " & vbNewLine _
                                              & "            ELSE KB2.KBN_NM3                                                                  " & vbNewLine _
                                              & "        END, '') AS KANJO_KAMOKU_CD                                                           " & vbNewLine _
                                              & "    , ISNULL(KB2.KBN_NM6, '') AS KEIRI_BUMON_CD                                               " & vbNewLine _
                                              & "--    , ISNULL(KMK.TAX_KB, '') AS TAX_KB                                                      " & vbNewLine _
                                              & "    , ISNULL(KB3.KBN_NM8, '') AS TAX_KB                                                       " & vbNewLine _
                                              & "    , ISNULL(                                                                                 " & vbNewLine _
                                              & "       (SELECT                                                                                " & vbNewLine _
                                              & "            TAX_RATE                                                                          " & vbNewLine _
                                              & "        FROM                                                                                  " & vbNewLine _
                                              & "            $LM_MST$..M_TAX                                                                   " & vbNewLine _
                                              & "        WHERE                                                                                 " & vbNewLine _
                                              & "            TAX_CD = KB3.KBN_NM3                                                              " & vbNewLine _
                                              & "        AND START_DATE IN                                                                     " & vbNewLine _
                                              & "           (SELECT                                                                            " & vbNewLine _
                                              & "                MAX(START_DATE) AS START_DATE                                                 " & vbNewLine _
                                              & "            FROM                                                                              " & vbNewLine _
                                              & "                $LM_MST$..M_TAX                                                               " & vbNewLine _
                                              & "            WHERE                                                                             " & vbNewLine _
                                              & "                TAX_CD = KB3.KBN_NM3                                                          " & vbNewLine _
                                              & "            AND START_DATE <= HED.SKYU_DATE                                                   " & vbNewLine _
                                              & "            AND SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                              & "            GROUP BY                                                                          " & vbNewLine _
                                              & "                TAX_CD)                                                                       " & vbNewLine _
                                              & "        AND SYS_DEL_FLG = '0'), 0) AS TAX_RATE                                                " & vbNewLine _
                                              & "    , ISNULL(NBR.INV_DEPT_NM, '') AS INV_DEPT_NM                                              " & vbNewLine _
                                              & "FROM                                                                                          " & vbNewLine _
                                              & "    $LM_TRN$..G_KAGAMI_HED HED                                                                " & vbNewLine _
                                              & "INNER JOIN                                                                                    " & vbNewLine _
                                              & "    $LM_TRN$..G_KAGAMI_DTL DTL                                                                " & vbNewLine _
                                              & "ON  DTL.SKYU_NO = HED.SKYU_NO                                                                 " & vbNewLine _
                                              & "LEFT JOIN                                                                                     " & vbNewLine _
                                              & "    $LM_MST$..S_USER USR                                                                      " & vbNewLine _
                                              & "ON  USR.USER_CD = HED.SYS_ENT_USER                                                            " & vbNewLine _
                                              & "AND USR.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "LEFT JOIN                                                                                     " & vbNewLine _
                                              & "    $LM_MST$..M_SEIQTO SEI                                                                    " & vbNewLine _
                                              & "ON  SEI.NRS_BR_CD = HED.NRS_BR_CD                                                             " & vbNewLine _
                                              & "AND SEI.SEIQTO_CD = HED.SEIQTO_CD                                                             " & vbNewLine _
                                              & "AND SEI.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "LEFT JOIN                                                                                     " & vbNewLine _
                                              & "    $LM_MST$..M_SEIQKMK KMK                                                                   " & vbNewLine _
                                              & "ON  KMK.GROUP_KB = DTL.GROUP_KB                                                               " & vbNewLine _
                                              & "AND KMK.SEIQKMK_CD = DTL.SEIQKMK_CD                                                           " & vbNewLine _
                                              & "AND KMK.SEIQKMK_CD_S = DTL.SEIQKMK_CD_S                                                       " & vbNewLine _
                                              & "AND KMK.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "LEFT JOIN                                                                                     " & vbNewLine _
                                              & "    $LM_MST$..Z_KBN KB2                                                                       " & vbNewLine _
                                              & "ON  KB2.KBN_GROUP_CD = 'B006'                                                                 " & vbNewLine _
                                              & "AND KB2.KBN_NM1 = KMK.KEIRI_KB                                                                " & vbNewLine _
                                              & "AND KB2.KBN_NM4 = DTL.BUSYO_CD                                                                " & vbNewLine _
                                              & "AND KB2.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "LEFT JOIN                                                                                     " & vbNewLine _
                                              & "    $LM_MST$..Z_KBN KB3                                                                       " & vbNewLine _
                                              & "ON  KB3.KBN_GROUP_CD = 'Z001'                                                                 " & vbNewLine _
                                              & "AND KB3.KBN_CD = KMK.TAX_KB                                                                   " & vbNewLine _
                                              & "AND KB3.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "LEFT JOIN                                                                                     " & vbNewLine _
                                              & "    $LM_MST$..M_NRS_BR NBR                                                                    " & vbNewLine _
                                              & "ON  NBR.NRS_BR_CD = HED.NRS_BR_CD                                                             " & vbNewLine _
                                              & "AND NBR.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "WHERE                                                                                         " & vbNewLine _
                                              & "    HED.SKYU_NO = @SKYU_NO                                                                    " & vbNewLine _
                                              & "AND HED.SYS_UPD_DATE = @HAITA_DATE                                                            " & vbNewLine _
                                              & "AND HED.SYS_UPD_TIME = @HAITA_TIME                                                            " & vbNewLine _
                                              & "AND HED.SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
                                              & "AND DTL.SYS_DEL_FLG = '0'                                                                     " & vbNewLine

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

    ''' <summary>
    ''' 地域セグメント取得用
    ''' </summary>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Const SQL_SELECT_COMBO_CHIIKI As String _
            = " SELECT                                                          " & vbNewLine _
            & "    CNCT_SEG_CD AS SEG_CD                                        " & vbNewLine _
            & "   ,CONCAT(SGMT_L_NM,'：',SGMT_M_NM,'：',SGMT_S_NM) AS SEG_NM    " & vbNewLine _
            & " FROM                                                            " & vbNewLine _
            & "   ABM_DB..M_SEGMENT                                             " & vbNewLine _
            & " WHERE                                                           " & vbNewLine _
            & "   DATA_TYPE_CD = '00001'                                        " & vbNewLine _
            & "   AND KBN_LANG = @KBN_LANG                                      " & vbNewLine _
            & "   AND SYS_DEL_FLG = '0'                                         " & vbNewLine _
            & " ORDER BY                                                        " & vbNewLine _
            & "     CNCT_SEG_CD                                                 " & vbNewLine

    ''' <summary>
    ''' セグメント初期値取得
    ''' </summary>
    ''' <remarks>行追加時の初期値として利用</remarks>
    Private Const SQL_SELECT_DEF_SEG As String _
            = " SELECT TOP 1                                " & vbNewLine _
            & "    SSG.CNCT_SEG_CD AS DEF_SEG_SEIHIN        " & vbNewLine _
            & "   ,CSG.CNCT_SEG_CD AS DEF_SEG_CHIIKI        " & vbNewLine _
            & "   ,MCT.TCUST_BPCD  AS TCUST_BPCD            " & vbNewLine _
            & "   ,MBP.BP_NM1      AS TCUST_BPNM            " & vbNewLine _
            & " FROM                                        " & vbNewLine _
            & "   $LM_MST$..M_CUST MCT                      " & vbNewLine _
            & " LEFT JOIN                                   " & vbNewLine _
            & "   ABM_DB..M_SEGMENT SSG                     " & vbNewLine _
            & " ON                                          " & vbNewLine _
            & "   SSG.CNCT_SEG_CD = MCT.PRODUCT_SEG_CD      " & vbNewLine _
            & "   AND SSG.DATA_TYPE_CD = '00002'            " & vbNewLine _
            & "   AND SSG.KBN_LANG = @KBN_LANG              " & vbNewLine _
            & "   AND SSG.SYS_DEL_FLG = '0'                 " & vbNewLine _
            & " LEFT JOIN                                   " & vbNewLine _
            & "   $LM_MST$..M_SOKO SOKO                     " & vbNewLine _
            & " ON                                          " & vbNewLine _
            & "   SOKO.WH_CD = MCT.DEFAULT_SOKO_CD          " & vbNewLine _
            & "   AND SOKO.SYS_DEL_FLG = '0'                " & vbNewLine _
            & " LEFT JOIN                                   " & vbNewLine _
            & "   ABM_DB..Z_KBN AKB                         " & vbNewLine _
            & " ON                                          " & vbNewLine _
            & "   AKB.KBN_GROUP_CD = '" & LMG900DAC.ABM_DB_TODOFUKEN & "'               " & vbNewLine _
            & "   AND AKB.KBN_LANG = @KBN_LANG              " & vbNewLine _
            & "   AND AKB.KBN_NM3 = LEFT(SOKO.JIS_CD,2)     " & vbNewLine _
            & "   AND AKB.SYS_DEL_FLG = '0'                 " & vbNewLine _
            & " LEFT JOIN                                   " & vbNewLine _
            & "   ABM_DB..M_SEGMENT CSG                     " & vbNewLine _
            & " ON                                          " & vbNewLine _
            & "   CSG.DATA_TYPE_CD = '00001'                " & vbNewLine _
            & "   AND CSG.KBN_LANG = @KBN_LANG              " & vbNewLine _
            & "   AND CSG.KBN_GROUP_CD = AKB.KBN_GRP_REF1   " & vbNewLine _
            & "   AND CSG.KBN_CD = AKB.KBN_CD_REF1          " & vbNewLine _
            & "   AND CSG.SYS_DEL_FLG = '0'                 " & vbNewLine _
            & " LEFT JOIN                                   " & vbNewLine _
            & "   ABM_DB..M_BP MBP                          " & vbNewLine _
            & " ON                                          " & vbNewLine _
            & "   MBP.BP_CD = MCT.TCUST_BPCD                " & vbNewLine _
            & " AND MBP.SYS_DEL_FLG      =   '0'            " & vbNewLine _
            & " WHERE                                       " & vbNewLine _
            & "   MCT.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
            & "   AND MCT.OYA_SEIQTO_CD = @SEIQTO_CD        " & vbNewLine _
            & "   AND MCT.SYS_DEL_FLG = '0'                 " & vbNewLine _
            & " ORDER BY                                    " & vbNewLine _
            & "    MCT.CUST_CD_L                            " & vbNewLine _
            & "   ,MCT.CUST_CD_M                            " & vbNewLine

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

    ''' <summary>
    ''' 通貨マスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>通貨マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectContCurrCd(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_CURR_LIST)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'SQLパラメータ初期化/設定
        Call Me.SetParamCurr()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectContCurrCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CURR_CD", "CURR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_CURR_OUT")

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_SELECT_HED)      'SQL構築(データ抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSelect()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectHed", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("STATE_KB", "STATE_KB")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        '2014.08.21 多通貨対応START
        map.Add("INV_CURR_CD", "INV_CURR_CD")
        '2014.08.21 多通貨対応END
        map.Add("SEIQTO_PIC", "SEIQTO_PIC")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("MEIGI_NM", "MEIGI_NM")
        map.Add("NEBIKI_RT1", "NEBIKI_RT1")
        map.Add("NEBIKI_GK1", "NEBIKI_GK1")
        map.Add("TAX_GK1", "TAX_GK1")
        map.Add("TAX_HASU_GK1", "TAX_HASU_GK1")
        map.Add("NEBIKI_RT2", "NEBIKI_RT2")
        map.Add("NEBIKI_GK2", "NEBIKI_GK2")
        map.Add("STORAGE_KB", "STORAGE_KB")
        map.Add("HANDLING_KB", "HANDLING_KB")
        map.Add("UNCHIN_KB", "UNCHIN_KB")
        map.Add("SAGYO_KB", "SAGYO_KB")
        map.Add("YOKOMOCHI_KB", "YOKOMOCHI_KB")
        '2014.08.21 多通貨対応START
        map.Add("EX_MOTO_CURR_CD", "EX_MOTO_CURR_CD")
        map.Add("EX_RATE", "EX_RATE")
        map.Add("EX_SAKI_CURR_CD", "EX_SAKI_CURR_CD")
        '2014.08.21 多通貨対応END
        map.Add("CRT_KB", "CRT_KB")
        map.Add("UNCHIN_IMP_FROM_DATE", "UNCHIN_IMP_FROM_DATE")
        map.Add("SAGYO_IMP_FROM_DATE", "SAGYO_IMP_FROM_DATE")
        map.Add("YOKOMOCHI_IMP_FROM_DATE", "YOKOMOCHI_IMP_FROM_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("RB_FLG", "RB_FLG")
        '2015.04.10 修正開始 (要望番号:2286)
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("SAP_NO", "SAP_NO")
        '2015.04.10 修正終了 (要望番号:2286)
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SKYU_NO_RELATED", "SKYU_NO_RELATED")
        map.Add("DOC_SEI_YN", "DOC_SEI_YN")
        map.Add("DOC_HUKU_YN", "DOC_HUKU_YN")
        map.Add("DOC_HIKAE_YN", "DOC_HIKAE_YN")
        map.Add("DOC_KEIRI_YN", "DOC_KEIRI_YN")
        map.Add("MAX_SKYU_SUB_NO", "MAX_SKYU_SUB_NO")
        map.Add("AKA_SKYU_NO", "AKA_SKYU_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050HED")

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑詳細検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑詳細検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectDetail(ByVal ds As DataSet) As DataSet

        '20210628 ベトナム対応Add
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20210628 ベトナム対応Add

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_SELECT_DTL)      'SQL構築(データ抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSelect()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SKYU_SUB_NO", "SKYU_SUB_NO")
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("MAKE_SYU_KB", "MAKE_SYU_KB")
        map.Add("MAKE_SYU_KB_NM", "MAKE_SYU_KB_NM")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("KANJO_KAMOKU_CD", "KANJO_KAMOKU_CD")
        map.Add("KEIRI_BUMON_CD", "KEIRI_BUMON_CD")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        '2014.08.21 追加START 多通貨対応
        map.Add("EX_RATE", "EX_RATE")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        '2014.08.21 追加END 多通貨対応
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        '★ ADD START 2011/09/06 SUGA
        map.Add("NEBIKI_RTGK", "NEBIKI_RTGK")
        '★ ADD E N D 2011/09/06 SUGA
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")
        map.Add("TAX_CD_JDE", "TAX_CD_JDE")
        '2014.08.21 追加START 多通貨対応
        map.Add("BASE_EX_RATE", "BASE_EX_RATE")
        map.Add("ROUND_POS", "ROUND_POS")
        '2014.08.21 追加END 多通貨対応
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")       'ADD 2016/09/06 
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
        map.Add("DEST_SEG_CD", "DEST_SEG_CD")
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
        map.Add("SEIQKMK_CD_S", "SEIQKMK_CD_S")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050DTL")

        Return ds

    End Function

    ''' <summary>
    ''' セット料金の単価マスタが登録された荷主の主請求先(であるか否か) の検索(件数取得)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSeiqtoCustSetPrice(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050IN_TSMC")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMG050DAC.SELECT_SEIQTO_CUST_SET_PRICE)   ' SQL構築(データ抽出用Select句)

        Dim cnt As Integer = 0

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' SQLパラメータ初期化/設定
            Call Me.SetParamSelectSeiqtoCustSetPrice()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 処理件数の設定
                If reader.Read() = True Then
                    cnt = Convert.ToInt32(reader("CNT").ToString())
                End If

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

    ''' <summary>
    ''' TSMC請求明細よりの部署コードとセット料金計算日数区分の組み合わせの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectBusyoCdUnitKbInTorikomiDataTsmc(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050IN_TSMC")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMG050DAC.SELECT_TORIKOMI_DATA_BUSYO_CD_UNIT_KB_TSMC) ' SQL構築(データ抽出用Select句)

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' SQLパラメータ初期化/設定
            Call Me.SetParamSelectBusyoCdUnitKbInTorikomiDataTsmc()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("BUSYO_CD", "BUSYO_CD")
                map.Add("UNIT_KB", "UNIT_KB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050DTL")

            End Using

            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' TSMC請求明細よりの取込データの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectTorikomiDataTsmc(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050IN_TSMC")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMG050DAC.SELECT_TORIKOMI_DATA_TSMC)  ' SQL構築(データ抽出用Select句)

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' SQLパラメータ初期化/設定
            Call Me.SetParamSelectTorikomiDataTsmc()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("SKYU_SUB_NO", "SKYU_SUB_NO")
                map.Add("GROUP_KB", "GROUP_KB")
                map.Add("SEIQKMK_CD", "SEIQKMK_CD")
                map.Add("SEIQKMK_NM", "SEIQKMK_NM")
                map.Add("KEIRI_KB", "KEIRI_KB")
                map.Add("TAX_KB", "TAX_KB")
                map.Add("MAKE_SYU_KB", "MAKE_SYU_KB")
                map.Add("MAKE_SYU_KB_NM", "MAKE_SYU_KB_NM")
                map.Add("BUSYO_CD", "BUSYO_CD")
                map.Add("KANJO_KAMOKU_CD", "KANJO_KAMOKU_CD")
                map.Add("KEIRI_BUMON_CD", "KEIRI_BUMON_CD")
                map.Add("TAX_KB_NM", "TAX_KB_NM")
                map.Add("KEISAN_TLGK", "KEISAN_TLGK")
                map.Add("NEBIKI_RT", "NEBIKI_RT")
                map.Add("NEBIKI_GK", "NEBIKI_GK")
                map.Add("TEKIYO", "TEKIYO")
                map.Add("PRINT_SORT", "PRINT_SORT")
                map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")
                map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
                map.Add("INS_FLG", "INS_FLG")
                map.Add("NEBIKI_RTGK", "NEBIKI_RTGK")
                map.Add("JISYATASYA_KB", "JISYATASYA_KB")
                map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
                map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
                map.Add("DEST_SEG_CD", "DEST_SEG_CD")
                map.Add("TCUST_BPCD", "TCUST_BPCD")
                map.Add("TCUST_BPNM", "TCUST_BPNM")
                map.Add("SEIQKMK_CD_S", "SEIQKMK_CD_S")
                map.Add("UNIT_KB", "UNIT_KB")
                map.Add("SET_OVER_AMO", "SET_OVER_AMO")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050DTL")

            End Using

            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '2014.08.21 追加END 多通貨対応

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))    'ADD 2018/08/24　依頼番号 : 002136   【LMS】経理戻しによって発生した新黒データを削除・復活できるよう変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(通貨マスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCurr()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 セット料金の単価マスタが登録された荷主の主請求先(であるか否か) の検索(件数取得)
    ''' </summary>
    Private Sub SetParamSelectSeiqtoCustSetPrice()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 TSMC請求明細よりの部署コードとセット料金計算日数区分の組み合わせの取得
    ''' </summary>
    Private Sub SetParamSelectBusyoCdUnitKbInTorikomiDataTsmc()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定 TSMC請求明細よりの取込データの取得
    ''' </summary>
    Private Sub SetParamSelectTorikomiDataTsmc()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAKE_SYU_KB", .Item("MAKE_SYU_KB").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ABM_DB_TODOFUKEN", LMG900DAC.ABM_DB_TODOFUKEN, DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNIT_KB", .Item("UNIT_KB").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

#End Region

#Region "排他処理"

    ''' <summary>
    ''' 請求鑑ヘッダ排他チェック処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_HAITA_CHK)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaita()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "HaitaChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()
        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#If True Then   'ADD 2018/08/21 依頼番号 : 002136

#Region "復活処理"

    ''' <summary>
    ''' 復活処理(請求鑑ヘッダ復活)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpFukkatsuKagamiHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPFUKKATSU_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpFukkatsuKagamiHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 復活処理(請求鑑詳細復活)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑詳細更新SQLの構築・発行</remarks>
    Private Function UpFukkatsuKagamiDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPFUKKATSU_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateDtlFukkatsu()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpFukkatsuKagamiDtl", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region
#End If

#Region "削除処理"

    ''' <summary>
    ''' 削除処理(請求鑑ヘッダ論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpDeleteKagamiHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPDELETE_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpDeleteKagamiHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理(請求鑑詳細論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑詳細更新SQLの構築・発行</remarks>
    Private Function UpDeleteKagamiDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPDELETE_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateDtl()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpDeleteKagamiDtl", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#Region "ステージアップ処理"

    ''' <summary>
    ''' ステージアップ処理(確定時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpKakuteiHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG000DAC.SQL_UP_HED_KAKUTEI)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpHedKakutei()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpKakuteiHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' ステージアップ処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpStageKagamiHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPSTAGE_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpStageHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpStageKagamiHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' SAP伝票番号を更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpSapNoKagamiHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPSAPNO_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpSapnoHed(
                ds.Tables("RESULT_SAP").Rows(0).Item("SAP_NO").ToString(),
                ds.Tables("RESULT_SAP").Rows(0).Item("SAP_OUT_USER").ToString()
                )

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpSapNoKagamiHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpStageHed()

        'ヘッダ部更新時共通
        Call Me.SetParamUpdateHed()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KB", Me._Row.Item("STATE_KB").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <param name="sapNo">SAP伝票番号</param>
    ''' <param name="sapOutUser">SAP連携ユーザID</param>
    Private Sub SetParamUpSapnoHed(ByVal sapNo As String, ByVal sapOutUser As String)

        'ヘッダ部更新時共通
        Call Me.SetParamUpdateHed()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_NO", sapNo, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_OUT_USER", sapOutUser, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(確定処理用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpHedKakutei()

        'ヘッダ部更新時共通
        Call Me.SetParamUpdateHed()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KB", .Item("STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_IMP_FROM_DATE", .Item("UNCHIN_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_IMP_FROM_DATE", .Item("SAGYO_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_IMP_FROM_DATE", .Item("YOKOMOCHI_IMP_FROM_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "システム共通項目の更新処理"

    ''' <summary>
    ''' 更新処理(請求鑑ヘッダ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpdateSysDataHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPD_SYSDATA_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpdateSysDataHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理(請求鑑詳細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑詳細更新SQLの構築・発行</remarks>
    Private Function UpdateSysDataDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPD_SYSDATA_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateDtl()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpdateSysDataDtl", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region

#Region "新規登録処理"

    ''' <summary>
    ''' 請求鑑ヘッダ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ新規登録SQLの構築・発行</remarks>
    Private Function InsertHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_INSERT_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "InsertHed", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑詳細新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑詳細新規登録SQLの構築・発行</remarks>
    Private Function InsertDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050DTL")

        Dim inTblHed As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_INSERT_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertDtl()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG050DAC", "InsertDtl", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "未来データ取得"

    ''' <summary>
    ''' 請求鑑ヘッダ未来データ存在チェック処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectNextData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_NEXT_DATA)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamNextData()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectNextData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("C007")
        End If
        reader.Close()
        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamNextData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        End With

    End Sub

#End Region

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 請求鑑ヘッダ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ更新SQLの構築・発行</remarks>
    Private Function UpdateHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPDATE_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSaveUpdHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "UpdateHed", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑詳細更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑詳細更新SQLの構築・発行</remarks>
    Private Function UpdateDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050DTL")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMG050DAC.SQL_UPDATE_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamSaveUpdDtl()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMG050DAC", "UpdateDtl", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "新規登録時チェック"

    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistSeiqtoMChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG000DAC.SQL_CHK_SEIQTO_M)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSeiqtoMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "ExistSeiqtoMChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) = 0 Then
            MyBase.SetMessage("E078", New String() {"請求先マスタ"})
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 請求先マスタ請求日関連チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SeiqtoDateChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_CHK_SEIQTO_DATE)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSeiqtoMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SeiqtoDateChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CLOSE_KB", "CLOSE_KB")
        map.Add("CLOSE_KB_NM", "CLOSE_KB_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050_SEIQTOM")

        Return ds

    End Function

    ''' <summary>
    ''' 通貨マスタ(COM_DB)重複チェック＋契約通貨,小数点桁数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>通貨マスタ(COM_DB)検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function RepCurrChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_CHK_CURR_INFO)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSeiqtoMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "RepCurrChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQ_CURR_CD", "SEIQ_CURR_CD")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("ROUND_POS", "ROUND_POS")
        map.Add("CUR_RATE", "CUR_RATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050_CURRINFO")

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダデータ重複チェック処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function RepeatDataChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_REPEAT_CHK)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDataChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "RepeatDataChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E269", New String() {"請求先コード、請求日が同一"})
        End If
        reader.Close()
        Return ds

    End Function

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
    ''' <summary>
    ''' 元請求番号が設定されているか否かをチェックするSQL
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistMotoSeiqNoChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_KEIRI_MODOSHI_CHK)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ初期化/設定
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "ExistMotoSeiqNoChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E028", New String() {"新黒データ", "削除"})
        End If
        reader.Close()
        Return ds

    End Function

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end

    ''' <summary>
    ''' 経理取込済チェックを行う処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function KeiriTorikomiChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_KEIRI_TORIKOMI_CHK)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetKeiriTorikomiChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "KeiriTorikomiChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E282")
        End If
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 未来データ存在チェックを行う処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求鑑ヘッダ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function NextDataChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_NEXT_DATA_CHK)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDataChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "NextDataChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E269", New String() {"請求日以降の自動取込請求書"})
        End If
        reader.Close()
        Return ds

    End Function
    ''' <summary>
    ''' 請求先マスタ取得(鑑最低保証チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectSeiqtoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_CHK_SEIQTO_MIN)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSeiqtoMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectSeiqtoData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("TOTAL_MIN_SEIQ_AMT", "TOTAL_MIN_SEIQ_AMT")
        map.Add("STORAGE_TOTAL_FLG", "STORAGE_TOTAL_FLG")
        map.Add("HANDLING_TOTAL_FLG", "HANDLING_TOTAL_FLG")
        map.Add("UNCHIN_TOTAL_FLG", "UNCHIN_TOTAL_FLG")
        map.Add("SAGYO_TOTAL_FLG", "SAGYO_TOTAL_FLG")
        map.Add("SEIQ_CURR_CD", "SEIQ_CURR_CD")
        map.Add("STORAGE_MIN_AMT", "STORAGE_MIN_AMT")
        map.Add("STORAGE_OTHER_MIN_AMT", "STORAGE_OTHER_MIN_AMT")
        map.Add("HANDLING_MIN_AMT", "HANDLING_MIN_AMT")
        map.Add("HANDLING_OTHER_MIN_AMT", "HANDLING_OTHER_MIN_AMT")
        map.Add("UNCHIN_MIN_AMT", "UNCHIN_MIN_AMT")
        map.Add("SAGYO_MIN_AMT", "SAGYO_MIN_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050_SEIQTOM")

        Return ds

    End Function
#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(請求マスタ存在チェック、請求日関連チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSeiqtoMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(データ重複チェック,未来データ存在チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDataChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_KB", .Item("CRT_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(データ重複チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKeiriTorikomiChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))        '追加 Notes 要望番号 1729 2012.12.27
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_KB", .Item("CRT_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "SAP処理"

    ''' <summary>
    ''' SAP出力処理対象取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SAP出力処理対象得用SQLの構築・発行</remarks>
    Private Function SelectSapOut(ByVal ds As DataSet) As DataSet

        ' DataSet の取得
        Dim inTbl As DataTable = ds.Tables("LMG050HED")

        ' IN Table の条件 row の格納
        Me._Row = inTbl.Rows(0)

        ' SQL 格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMG050DAC.SQL_SAP_OUT_SELECT)

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        ' SQLパラメータ初期化/設定
        Call Me.SetParamSapOut()

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectSapOut", cmd)

        ' SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ' DataReader → DataTable への転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("TAX_GK1", "TAX_GK1")
        map.Add("TAX_HASU_GK1", "TAX_HASU_GK1")
        map.Add("INV_CURR_CD", "INV_CURR_CD")
        map.Add("REMARK", "REMARK")
        map.Add("RB_FLG", "RB_FLG")
        map.Add("USER_NM", "USER_NM")
        map.Add("NRS_KEIRI_CD2", "NRS_KEIRI_CD2")
        map.Add("COMP_CD", "COMP_CD")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("NEBIKI_RTGK", "NEBIKI_RTGK")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("TAX_CD_JDE", "TAX_CD_JDE")
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
        map.Add("DEST_SEG_CD", "DEST_SEG_CD")
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("KANJO_KAMOKU_CD", "KANJO_KAMOKU_CD")
        map.Add("KEIRI_BUMON_CD", "KEIRI_BUMON_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_RATE", "TAX_RATE")
        map.Add("INV_DEPT_NM", "INV_DEPT_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050SAPUPDOUT")

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(SAP出力処理対象取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSapOut()
        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
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
        Dim inTbl As DataTable = ds.Tables("LMG050COMBO_SEIHINA")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMG050DAC.SQL_SELECT_COMBO_SEIHIN)

        ' SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", Me._Row.Item("KBN_LANG").ToString(), DBDataType.CHAR))

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectComboSeihin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        'レコードをクリア
        ds.Tables("LMG050COMBO_SEIHINA").Rows.Clear()

        '取得データの格納先をマッピング
        map.Add("SEG_CD", "SEG_CD")
        map.Add("SEG_NM", "SEG_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050COMBO_SEIHINA")

        Return ds

    End Function

    ''' <summary>
    ''' 地域セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboChiiki(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050COMBO_CHIIKI")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMG050DAC.SQL_SELECT_COMBO_CHIIKI)

        ' SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", Me._Row.Item("KBN_LANG").ToString(), DBDataType.CHAR))

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectComboChiiki", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        'レコードをクリア
        ds.Tables("LMG050COMBO_CHIIKI").Rows.Clear()

        '取得データの格納先をマッピング
        map.Add("SEG_CD", "SEG_CD")
        map.Add("SEG_NM", "SEG_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050COMBO_CHIIKI")

        Return ds

    End Function

    ''' <summary>
    ''' セグメント初期値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>行追加時の初期値として利用</remarks>
    Private Function SelectDefSeg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG050DEF_SEG")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG050DAC.SQL_SELECT_DEF_SEG, Me._Row.Item("NRS_BR_CD").ToString()))

        ' SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", Me._Row.Item("KBN_LANG").ToString(), DBDataType.CHAR))

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM070DAC", "SelectDefSeg", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        'レコードをクリア
        ds.Tables("LMG050DEF_SEG").Rows.Clear()

        '取得データの格納先をマッピング
        map.Add("DEF_SEG_SEIHIN", "DEF_SEG_SEIHIN")
        map.Add("DEF_SEG_CHIIKI", "DEF_SEG_CHIIKI")
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG050DEF_SEG")

        Return ds

    End Function

#End Region

#Region "パラメータ設定(共通)"

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑ヘッダ更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateHed()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        '更新共通項目           
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑詳細更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))

        End With

        '更新共通項目           
        Call Me.SetParamCommonSystemUpd()

    End Sub

#If True Then       'ADD　2018/08/23 依頼番号 : 002136
    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑詳細更新復活登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateDtlFukkatsu()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

        '更新共通項目           
        Call Me.SetParamCommonSystemUpd()

    End Sub

#End If

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑ヘッダ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertHed()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KB", .Item("STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_PIC", .Item("SEIQTO_PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", .Item("SEIQTO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT1", .Item("NEBIKI_RT1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK1", .Item("NEBIKI_GK1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_GK1", .Item("TAX_GK1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_HASU_GK1", .Item("TAX_HASU_GK1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT2", .Item("NEBIKI_RT2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK2", .Item("NEBIKI_GK2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_KB", .Item("STORAGE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_KB", .Item("HANDLING_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB", .Item("SAGYO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_KB", .Item("YOKOMOCHI_KB").ToString(), DBDataType.CHAR))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_CURR_CD", .Item("INV_CURR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_RATE", .Item("EX_RATE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_MOTO_CURR_CD", .Item("EX_MOTO_CURR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_SAKI_CURR_CD", .Item("EX_SAKI_CURR_CD").ToString(), DBDataType.CHAR))
            '2014.09.01 追加END   多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_KB", .Item("CRT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_IMP_FROM_DATE", .Item("UNCHIN_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_IMP_FROM_DATE", .Item("SAGYO_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_IMP_FROM_DATE", .Item("YOKOMOCHI_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO_RELATED", .Item("SKYU_NO_RELATED").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_FLG", .Item("RB_FLG").ToString(), DBDataType.CHAR))
            '2015.04.10 追加開始 要望番号:2286 対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
            '2015.04.10 追加終了 要望番号:2286 対応

        End With

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑詳細新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", .Item("SKYU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_SUB_NO", .Item("SKYU_SUB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", .Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAKE_SYU_KB", .Item("MAKE_SYU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEISAN_TLGK", .Item("KEISAN_TLGK").ToString(), DBDataType.NUMERIC))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_RATE", .Item("EX_RATE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_CURR_CD", .Item("ITEM_CURR_CD").ToString(), DBDataType.CHAR))
            '2014.08.21 追加END   多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT", .Item("NEBIKI_RT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RTGK", .Item("NEBIKI_RTGK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK", .Item("NEBIKI_GK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEKIYO", .Item("TEKIYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEMPLATE_IMP_FLG", .Item("TEMPLATE_IMP_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_CD_JDE", .Item("TAX_CD_JDE").ToString(), DBDataType.NVARCHAR))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BASE_EX_RATE", .Item("BASE_EX_RATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '2014.08.21 追加END   多通貨対応

            'ADD 2016/09/06 Start 最保管対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", .Item("JISYATASYA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRODUCT_SEG_CD", .Item("PRODUCT_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_SEG_CD", .Item("ORIG_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SEG_CD", .Item("DEST_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TCUST_BPCD", .Item("TCUST_BPCD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", .Item("SEIQKMK_CD_S").ToString(), DBDataType.CHAR))
            'ADD 2016/09/06 End
        End With

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑ヘッダ更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSaveUpdHed()

        '主キー、排他用データ、更新共通項目主得
        Call Me.SetParamUpdateHed()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STATE_KB", .Item("STATE_KB").ToString(), DBDataType.CHAR))  'ADD 2023/05/02 035323【LMS】経理取込み対象外後の鑑編集
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_PIC", .Item("SEIQTO_PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", .Item("SEIQTO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT1", .Item("NEBIKI_RT1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK1", .Item("NEBIKI_GK1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_GK1", .Item("TAX_GK1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_HASU_GK1", .Item("TAX_HASU_GK1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT2", .Item("NEBIKI_RT2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK2", .Item("NEBIKI_GK2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_KB", .Item("STORAGE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_KB", .Item("HANDLING_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_KB", .Item("SAGYO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_KB", .Item("YOKOMOCHI_KB").ToString(), DBDataType.CHAR))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_CURR_CD", .Item("INV_CURR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_RATE", .Item("EX_RATE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_MOTO_CURR_CD", .Item("EX_MOTO_CURR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_SAKI_CURR_CD", .Item("EX_SAKI_CURR_CD").ToString(), DBDataType.CHAR))
            '2014.09.01 追加END   多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_IMP_FROM_DATE", .Item("UNCHIN_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_IMP_FROM_DATE", .Item("SAGYO_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_IMP_FROM_DATE", .Item("YOKOMOCHI_IMP_FROM_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            '2015.04.10 追加開始 要望番号:2286 対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
            '2015.04.10 追加終了 要望番号:2286 対応

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(請求鑑詳細更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSaveUpdDtl()

        '主キー、排他用データ、更新共通項目主得
        Call Me.SetParamUpdateDtl()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_SUB_NO", .Item("SKYU_SUB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", .Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MAKE_SYU_KB", .Item("MAKE_SYU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEISAN_TLGK", .Item("KEISAN_TLGK").ToString(), DBDataType.NUMERIC))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EX_RATE", .Item("EX_RATE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_CURR_CD", .Item("ITEM_CURR_CD").ToString(), DBDataType.CHAR))
            '2014.08.21 追加END   多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT", .Item("NEBIKI_RT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RTGK", .Item("NEBIKI_RTGK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK", .Item("NEBIKI_GK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEKIYO", .Item("TEKIYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEMPLATE_IMP_FLG", .Item("TEMPLATE_IMP_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_CD_JDE", .Item("TAX_CD_JDE").ToString(), DBDataType.NVARCHAR))
            '2014.08.21 追加START 多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BASE_EX_RATE", .Item("BASE_EX_RATE").ToString(), DBDataType.CHAR))
            '2014.08.21 追加END   多通貨対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRODUCT_SEG_CD", .Item("PRODUCT_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_SEG_CD", .Item("ORIG_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SEG_CD", .Item("DEST_SEG_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TCUST_BPCD", .Item("TCUST_BPCD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", .Item("SEIQKMK_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

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

#End Region

#Region "スキーマ名称設定"

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

#Region "言語取得"
    '20210628 ベトナム対応 add start
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable
        inTbl = ds.Tables("LMG050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG050DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function

    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '20210628 ベトナム対応 add End

#End Region

End Class
