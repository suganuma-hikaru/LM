' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH040    : EDI出荷データ編集
'  作  成  者       :  [kim]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    'パラメータSettingパターン（検索）
    Private Enum SelectCondition As Integer
        PTN1  '受信テーブル名取得
        PTN2  '排他日付取得（受信HED）
        PTN3  '排他チェック
        PTN4  '検索（EDI出荷L）
        PTN5  '検索（EDI出荷M）
        PTN6  '検索（FREE）
        PTN7  '取込日付
        PTN8  '検索（FFEM入出荷EDIデータ(ヘッダ)）
    End Enum

    'パラメータSettingパターン（保存）
    Private Enum UpdateCondition As Integer
        UP_PTN1  'EDI出荷L
        UP_PTN2  'EDI出荷M
        UP_PTN3  '受信テーブル
        UP_PTN4  '
    End Enum


    '検索INテーブル
    Private Const TABLE_NM_IN As String = "LMH040IN_FIX"

    '受信テーブル情報格納テーブル
    Private Const TABLE_NM_RCV As String = "LMH040_RCV"

    '受信テーブル名格納テーブル
    Private Const TABLE_NM_RCV_NM As String = "LMH040_RCV_NM"

    '検索OUTテーブル(EDI出荷L)
    Private Const TABLE_NM_EDI_L As String = "LMH040_OUTKAEDI_L"

    '検索OUTテーブル(EDI出荷M)
    Private Const TABLE_NM_EDI_M As String = "LMH040_OUTKAEDI_M"

    '検索OUTテーブル(FREE項目L)
    Private Const TABLE_NM_OUT_FREE_L As String = "LMH040_M_FREE_STATE_L"

    '検索OUTテーブル(FREE項目M)
    Private Const TABLE_NM_OUT_FREE_M As String = "LMH040_M_FREE_STATE_M"

    '取込日付チェック用テーブル
    Private Const TABLE_NM_TORIKOMI_DATE As String = "LMH040_TORIKOMI_DATE"

    '検索OUTテーブル(FFEM入出荷EDIデータ(ヘッダ))
    Private Const TABLE_NM_INOUTKAEDI_HED_FJF As String = "LMH040_INOUTKAEDI_HED_FJF"

    'DAC名
    Private Const CLASS_NM As String = "LMH040DAC"

#End Region '制御用

#Region "受信テーブル名取得"

    '受信テーブル名取得
    Private Const SQL_GET_RCV As String = " SELECT                        " & vbNewLine _
                                        & "   RCV_NM_HED    AS RCV_NM_HED " & vbNewLine _
                                        & " , RCV_NM_DTL    AS RCV_NM_DTL " & vbNewLine _
                                        & " FROM                          " & vbNewLine _
                                        & "   $LM_MST$..M_EDI_CUST        " & vbNewLine _
                                        & " WHERE                         " & vbNewLine _
                                        & "   NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
                                        & "  AND                          " & vbNewLine _
                                        & "   WH_CD     = @WH_CD          " & vbNewLine _
                                        & "  AND                          " & vbNewLine _
                                        & "   CUST_CD_L = @CUST_CD_L      " & vbNewLine _
                                        & "  AND                          " & vbNewLine _
                                        & "   CUST_CD_M = @CUST_CD_M      " & vbNewLine _
                                        & "  AND                          " & vbNewLine _
                                        & "   INOUT_KB = '0'              " & vbNewLine

#End Region '受信テーブル名取得

#Region "排他チェック用 SQL"

    '排他日付取得（受信HED）
    Private Const SQL_GET_HAITA_HED As String = " SELECT " & vbNewLine _
                                              & "    HED.NRS_BR_CD                                                AS NRS_BR_CD    " & vbNewLine _
                                              & "  , HED.EDI_CTL_NO                                               AS EDI_CTL_NO   " & vbNewLine _
                                              & "  , SUBSTRING(MAX(HED.SYS_UPD_DATE+HED.SYS_UPD_TIME),0,9)        AS SYS_UPD_DATE " & vbNewLine _
                                              & "  , SUBSTRING(MAX(HED.SYS_UPD_DATE+HED.SYS_UPD_TIME),9,9)        AS SYS_UPD_TIME " & vbNewLine _
                                              & " FROM                                                                            " & vbNewLine _
                                              & "    $LM_TRN$..$TABLE_NM$   HED                                                   " & vbNewLine _
                                              & " WHERE                                                                           " & vbNewLine _
                                              & "    HED.NRS_BR_CD  = @NRS_BR_CD                                                  " & vbNewLine _
                                              & "  AND                                                                            " & vbNewLine _
                                              & "    HED.EDI_CTL_NO = @EDI_CTL_NO                                                 " & vbNewLine _
                                              & " GROUP BY                                                                        " & vbNewLine _
                                              & "    HED.NRS_BR_CD                                                                " & vbNewLine _
                                              & "  , HED.EDI_CTL_NO                                                               " & vbNewLine

    '排他チェックSQL（受信HED）
    Private Const SQL_HAITA_CHECK_HED As String = "  SELECT COUNT(MAIN.NRS_BR_CD)   AS REC_CNT                                " & vbNewLine _
                                               & " FROM                                                                       " & vbNewLine _
                                               & "  (                                                                         " & vbNewLine _
                                               & "     SELECT                                                                 " & vbNewLine _
                                               & "        HED.NRS_BR_CD                                AS NRS_BR_CD           " & vbNewLine _
                                               & "      , HED.EDI_CTL_NO                               AS EDI_CTL_NO          " & vbNewLine _
                                               & "      , MAX(HED.SYS_UPD_DATE+HED.SYS_UPD_TIME)       AS SYS_UPD_DATE_TIME   " & vbNewLine _
                                               & "     FROM                                                                   " & vbNewLine _
                                               & "      $LM_TRN$..$TABLE_NM$        HED                                       " & vbNewLine _
                                               & "     WHERE                                                                  " & vbNewLine _
                                               & "     HED.NRS_BR_CD  = @NRS_BR_CD                                            " & vbNewLine _
                                               & "     AND                                                                    " & vbNewLine _
                                               & "     HED.EDI_CTL_NO = @EDI_CTL_NO                                           " & vbNewLine _
                                               & "     GROUP BY                                                               " & vbNewLine _
                                               & "        HED.NRS_BR_CD                                                       " & vbNewLine _
                                               & "      , HED.EDI_CTL_NO                                                      " & vbNewLine _
                                               & "  ) MAIN                                                                    " & vbNewLine _
                                               & " WHERE                                                                      " & vbNewLine _
                                               & "    MAIN.SYS_UPD_DATE_TIME = @SYS_UPD_DATE + @SYS_UPD_TIME                  " & vbNewLine

    '排他チェックSQL（EDI出荷L）
    Private Const SQL_HAITA_CHECK_L As String = " SELECT                              " & vbNewLine _
                                              & "   COUNT(NRS_BR_CD) AS REC_CNT       " & vbNewLine _
                                              & " FROM                                " & vbNewLine _
                                              & "   $LM_TRN$..H_OUTKAEDI_L  H03       " & vbNewLine _
                                              & " WHERE                               " & vbNewLine _
                                              & "    H03.NRS_BR_CD  = @NRS_BR_CD      " & vbNewLine _
                                              & "  AND                                " & vbNewLine _
                                              & "    H03.EDI_CTL_NO = @EDI_CTL_NO     " & vbNewLine _
                                              & "  AND                                " & vbNewLine _
                                              & "    H03.SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
                                              & "  AND                                " & vbNewLine _
                                              & "    H03.SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine



#End Region '排他チェック用 SQL

#Region "検索処理 SQL"

    '検索SQLメイン（EDI出荷大データ取得用）
    Private Const SQL_SELECT_EDI_L As String = " SELECT  " & vbNewLine _
                                             & "   H03.NRS_BR_CD                                          AS NRS_BR_CD         " & vbNewLine _
                                             & " , H03.EDI_CTL_NO                                         AS EDI_CTL_NO        " & vbNewLine _
                                             & " , H03.OUTKA_CTL_NO                                       AS OUTKA_CTL_NO      " & vbNewLine _
                                             & " , H03.OUTKA_KB                                           AS OUTKA_KB          " & vbNewLine _
                                             & " , H03.SYUBETU_KB                                         AS SYUBETU_KB        " & vbNewLine _
                                             & " , H03.NAIGAI_KB                                          AS NAIGAI_KB         " & vbNewLine _
                                             & " , H03.OUTKA_STATE_KB                                     AS OUTKA_STATE_KB    " & vbNewLine _
                                             & " , H03.OUTKAHOKOKU_YN                                     AS OUTKAHOKOKU_YN    " & vbNewLine _
                                             & " , H03.PICK_KB                                            AS PICK_KB           " & vbNewLine _
                                             & " , M01.NRS_BR_NM                                          AS NRS_BR_NM         " & vbNewLine _
                                             & " , H03.WH_CD                                              AS WH_CD             " & vbNewLine _
                                             & " , M03.WH_NM                                              AS WH_NM             " & vbNewLine _
                                             & " , H03.OUTKA_PLAN_DATE                                    AS OUTKA_PLAN_DATE   " & vbNewLine _
                                             & " , H03.OUTKO_DATE                                         AS OUTKO_DATE        " & vbNewLine _
                                             & " , H03.ARR_PLAN_DATE                                      AS ARR_PLAN_DATE     " & vbNewLine _
                                             & " , H03.ARR_PLAN_TIME                                      AS ARR_PLAN_TIME     " & vbNewLine _
                                             & " , H03.HOKOKU_DATE                                        AS HOKOKU_DATE       " & vbNewLine _
                                             & " , H03.TOUKI_HOKAN_YN                                     AS TOUKI_HOKAN_YN    " & vbNewLine _
                                             & " , H03.CUST_CD_L                                          AS CUST_CD_L         " & vbNewLine _
                                             & " , H03.CUST_CD_M                                          AS CUST_CD_M         " & vbNewLine _
                                             & " , M04.CUST_NM_L                                          AS CUST_NM_L         " & vbNewLine _
                                             & " , M04.CUST_NM_M                                          AS CUST_NM_M         " & vbNewLine _
                                             & " , H03.SHIP_CD_L                                          AS SHIP_CD_L         " & vbNewLine _
                                             & " , H03.SHIP_CD_M                                          AS SHIP_CD_M         " & vbNewLine _
                                             & " , H03.SHIP_NM_L                                          AS SHIP_NM_L         " & vbNewLine _
                                             & " , H03.SHIP_NM_M                                          AS SHIP_NM_M         " & vbNewLine _
                                             & " , H03.EDI_DEST_CD                                        AS EDI_DEST_CD       " & vbNewLine _
                                             & " , H03.DEST_CD                                            AS DEST_CD           " & vbNewLine _
                                             & " , H03.DEST_NM                                            AS DEST_NM           " & vbNewLine _
                                             & " , H03.DEST_ZIP                                           AS DEST_ZIP          " & vbNewLine _
                                             & " , H03  .DEST_AD_1                                          AS DEST_AD_1         " & vbNewLine _
                                             & " , H03.DEST_AD_2                                          AS DEST_AD_2         " & vbNewLine _
                                             & " , H03.DEST_AD_3                                          AS DEST_AD_3         " & vbNewLine _
                                             & " , H03.DEST_AD_4                                          AS DEST_AD_4         " & vbNewLine _
                                             & " , H03.DEST_AD_5                                          AS DEST_AD_5         " & vbNewLine _
                                             & " , H03.DEST_TEL                                           AS DEST_TEL          " & vbNewLine _
                                             & " , H03.DEST_FAX                                           AS DEST_FAX          " & vbNewLine _
                                             & " , H03.DEST_MAIL                                          AS DEST_MAIL         " & vbNewLine _
                                             & " , H03.DEST_JIS_CD                                        AS DEST_JIS_CD       " & vbNewLine _
                                             & " , H03.SP_NHS_KB                                          AS SP_NHS_KB         " & vbNewLine _
                                             & " , H03.COA_YN                                             AS COA_YN            " & vbNewLine _
                                             & " , H03.CUST_ORD_NO                                        AS CUST_ORD_NO       " & vbNewLine _
                                             & " , H03.BUYER_ORD_NO                                       AS BUYER_ORD_NO      " & vbNewLine _
                                             & " , H03.UNSO_MOTO_KB                                       AS UNSO_MOTO_KB      " & vbNewLine _
                                             & " , H03.UNSO_TEHAI_KB                                      AS UNSO_TEHAI_KB     " & vbNewLine _
                                             & " , H03.SYARYO_KB                                          AS SYARYO_KB         " & vbNewLine _
                                             & " , H03.BIN_KB                                             AS BIN_KB            " & vbNewLine _
                                             & " , H03.UNSO_CD                                            AS UNSO_CD           " & vbNewLine _
                                             & " , H03.UNSO_NM                                            AS UNSO_NM           " & vbNewLine _
                                             & " , H03.UNSO_BR_CD                                         AS UNSO_BR_CD        " & vbNewLine _
                                             & " , H03.UNSO_BR_NM                                         AS UNSO_BR_NM        " & vbNewLine _
                                             & " , H03.UNCHIN_TARIFF_CD                                   AS UNCHIN_TARIFF_CD  " & vbNewLine _
                                             & " , H03.EXTC_TARIFF_CD                                     AS EXTC_TARIFF_CD    " & vbNewLine _
                                             & " , H03.REMARK                                             AS REMARK            " & vbNewLine _
                                             & " , H03.UNSO_ATT                                           AS UNSO_ATT          " & vbNewLine _
                                             & " , H03.DENP_YN                                            AS DENP_YN           " & vbNewLine _
                                             & " , H03.PC_KB                                              AS PC_KB             " & vbNewLine _
                                             & " , H03.UNCHIN_YN                                          AS UNCHIN_YN         " & vbNewLine _
                                             & " , H03.NIYAKU_YN                                          AS NIYAKU_YN         " & vbNewLine _
                                             & " , H03.OUT_FLAG                                           AS OUT_FLAG          " & vbNewLine _
                                             & " , H03.AKAKURO_KB                                         AS AKAKURO_KB        " & vbNewLine _
                                             & " , H03.JISSEKI_FLAG                                       AS JISSEKI_FLAG      " & vbNewLine _
                                             & " , H03.JISSEKI_USER                                       AS JISSEKI_USER      " & vbNewLine _
                                             & " , H03.JISSEKI_DATE                                       AS JISSEKI_DATE      " & vbNewLine _
                                             & " , H03.JISSEKI_TIME                                       AS JISSEKI_TIME      " & vbNewLine _
                                             & " , H03.FREE_N01                                           AS FREE_N01          " & vbNewLine _
                                             & " , H03.FREE_N02                                           AS FREE_N02          " & vbNewLine _
                                             & " , H03.FREE_N03                                           AS FREE_N03          " & vbNewLine _
                                             & " , H03.FREE_N04                                           AS FREE_N04          " & vbNewLine _
                                             & " , H03.FREE_N05                                           AS FREE_N05          " & vbNewLine _
                                             & " , H03.FREE_N06                                           AS FREE_N06          " & vbNewLine _
                                             & " , H03.FREE_N07                                           AS FREE_N07          " & vbNewLine _
                                             & " , H03.FREE_N08                                           AS FREE_N08          " & vbNewLine _
                                             & " , H03.FREE_N09                                           AS FREE_N09          " & vbNewLine _
                                             & " , H03.FREE_N10                                           AS FREE_N10          " & vbNewLine _
                                             & " , H03.FREE_C01                                           AS FREE_C01          " & vbNewLine _
                                             & " , H03.FREE_C02                                           AS FREE_C02          " & vbNewLine _
                                             & " , H03.FREE_C03                                           AS FREE_C03          " & vbNewLine _
                                             & " , H03.FREE_C04                                           AS FREE_C04          " & vbNewLine _
                                             & " , H03.FREE_C05                                           AS FREE_C05          " & vbNewLine _
                                             & " , H03.FREE_C06                                           AS FREE_C06          " & vbNewLine _
                                             & " , H03.FREE_C07                                           AS FREE_C07          " & vbNewLine _
                                             & " , H03.FREE_C08                                           AS FREE_C08          " & vbNewLine _
                                             & " , H03.FREE_C09                                           AS FREE_C09          " & vbNewLine _
                                             & " , H03.FREE_C10                                           AS FREE_C10          " & vbNewLine _
                                             & " , H03.FREE_C11                                           AS FREE_C11          " & vbNewLine _
                                             & " , H03.FREE_C12                                           AS FREE_C12          " & vbNewLine _
                                             & " , H03.FREE_C13                                           AS FREE_C13          " & vbNewLine _
                                             & " , H03.FREE_C14                                           AS FREE_C14          " & vbNewLine _
                                             & " , H03.FREE_C15                                           AS FREE_C15          " & vbNewLine _
                                             & " , H03.FREE_C16                                           AS FREE_C16          " & vbNewLine _
                                             & " , H03.FREE_C17                                           AS FREE_C17          " & vbNewLine _
                                             & " , H03.FREE_C18                                           AS FREE_C18          " & vbNewLine _
                                             & " , H03.FREE_C19                                           AS FREE_C19          " & vbNewLine _
                                             & " , H03.FREE_C20                                           AS FREE_C20          " & vbNewLine _
                                             & " , H03.FREE_C21                                           AS FREE_C21          " & vbNewLine _
                                             & " , H03.FREE_C22                                           AS FREE_C22          " & vbNewLine _
                                             & " , H03.FREE_C23                                           AS FREE_C23          " & vbNewLine _
                                             & " , H03.FREE_C24                                           AS FREE_C24          " & vbNewLine _
                                             & " , H03.FREE_C25                                           AS FREE_C25          " & vbNewLine _
                                             & " , H03.FREE_C26                                           AS FREE_C26          " & vbNewLine _
                                             & " , H03.FREE_C27                                           AS FREE_C27          " & vbNewLine _
                                             & " , H03.FREE_C28                                           AS FREE_C28          " & vbNewLine _
                                             & " , H03.FREE_C29                                           AS FREE_C29          " & vbNewLine _
                                             & " , H03.FREE_C30                                           AS FREE_C30          " & vbNewLine _
                                             & " , H03.CRT_USER                                           AS CRT_USER          " & vbNewLine _
                                             & " , H03.CRT_DATE                                           AS CRT_DATE          " & vbNewLine _
                                             & " , H03.CRT_TIME                                           AS CRT_TIME          " & vbNewLine _
                                             & " , H03.UPD_USER                                           AS UPD_USER          " & vbNewLine _
                                             & " , H03.UPD_DATE                                           AS UPD_DATE          " & vbNewLine _
                                             & " , H03.UPD_TIME                                           AS UPD_TIME          " & vbNewLine _
                                             & " , H03.SCM_CTL_NO_L                                       AS SCM_CTL_NO_L      " & vbNewLine _
                                             & " , H03.EDIT_FLAG                                          AS EDIT_FLAG         " & vbNewLine _
                                             & " ,CASE WHEN H03.DEL_KB = '1'  THEN        '01'                                 " & vbNewLine _
                                             & "       WHEN H03.DEL_KB = '2'  THEN        '02'                                 " & vbNewLine _
                                             & "       WHEN H03.DEL_KB = '3'  THEN        '05'                                 " & vbNewLine _
                                             & "       WHEN H03.DEL_KB = '0' AND @MATOME_FLG <> '0' THEN  '04'                 " & vbNewLine _
                                             & "       WHEN H03.DEL_KB = '0' AND H03.OUT_FLAG = '1' THEN  '03'                 " & vbNewLine _
                                             & "       ELSE  '00'                               END       AS EDI_STATE_KB      " & vbNewLine _
                                             & " , ''                                                     AS UNCHIN_TARIFF_REM " & vbNewLine _
                                             & " , ''                                                     AS YOKO_REM          " & vbNewLine _
                                             & " , H03.SYS_UPD_DATE                                       AS SYS_UPD_DATE      " & vbNewLine _
                                             & " , H03.SYS_UPD_TIME                                       AS SYS_UPD_TIME      " & vbNewLine _
                                             & " FROM                                                                          " & vbNewLine _
                                             & "   $LM_TRN$..H_OUTKAEDI_L   H03                                                " & vbNewLine _
                                             & " LEFT JOIN                                                                     " & vbNewLine _
                                             & "   $LM_MST$..M_NRS_BR       M01                                                " & vbNewLine _
                                             & " ON                                                                            " & vbNewLine _
                                             & "   M01.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                             & "  --AND                                                                        " & vbNewLine _
                                             & "   --M01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                             & " LEFT JOIN                                                                     " & vbNewLine _
                                             & "   $LM_MST$..M_SOKO         M03                                                " & vbNewLine _
                                             & " ON                                                                            " & vbNewLine _
                                             & "   M03.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                             & "  AND                                                                          " & vbNewLine _
                                             & "   M03.WH_CD = @WH_CD                                                          " & vbNewLine _
                                             & "  --AND                                                                        " & vbNewLine _
                                             & "  -- M03.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                             & " LEFT JOIN                                                                     " & vbNewLine _
                                             & "   $LM_MST$..M_CUST         M04                                                " & vbNewLine _
                                             & " ON                                                                            " & vbNewLine _
                                             & "   M04.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                             & "  AND                                                                          " & vbNewLine _
                                             & "  H03.CUST_CD_L = M04.CUST_CD_L                                                " & vbNewLine _
                                             & "  AND                                                                          " & vbNewLine _
                                             & "  H03.CUST_CD_M = M04.CUST_CD_M                                                " & vbNewLine _
                                             & "  AND                                                                          " & vbNewLine _
                                             & "  M04.CUST_CD_S = '00'                                                         " & vbNewLine _
                                             & "  AND                                                                          " & vbNewLine _
                                             & "  M04.CUST_CD_SS = '00'                                                        " & vbNewLine _
                                             & " WHERE                                                                         " & vbNewLine _
                                             & "   H03.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                             & "  AND                                                                          " & vbNewLine _
                                             & "   H03.EDI_CTL_NO = @EDI_CTL_NO                                                " & vbNewLine _
                                             & " ORDER BY                                                                      " & vbNewLine _
                                             & "   H03.EDI_CTL_NO                                                              " & vbNewLine
    '& " LEFT JOIN                                                                     " & vbNewLine _
    '& "   $LM_MST$..Z_KBN   Z01_1                                                     " & vbNewLine _
    '& " ON                                                                            " & vbNewLine _
    '& "   Z01_1.KBN_GROUP_CD  = 'E009'                                                " & vbNewLine _
    '& "  AND                                                                          " & vbNewLine _
    '& "   Z01_1.KBN_CD = '01'                                                         " & vbNewLine _
    '& " LEFT JOIN                                                                     " & vbNewLine _
    '& "   $LM_MST$..Z_KBN   Z01_2                                                     " & vbNewLine _
    '& " ON                                                                            " & vbNewLine _
    '& "   Z01_2.KBN_GROUP_CD  = 'E009'                                                " & vbNewLine _
    '& "  AND                                                                          " & vbNewLine _
    '& "   Z01_2.KBN_CD = '02'                                                         " & vbNewLine _
    '& " LEFT JOIN                                                                     " & vbNewLine _
    '& "   $LM_MST$..Z_KBN   Z01_3                                                     " & vbNewLine _
    '& " ON                                                                            " & vbNewLine _
    '& "   Z01_3.KBN_GROUP_CD  = 'E009'                                                " & vbNewLine _
    '& "  AND                                                                          " & vbNewLine _
    '& "   Z01_3.KBN_CD = '03'                                                         " & vbNewLine _
    '& " LEFT JOIN                                                                     " & vbNewLine _
    '& "   $LM_MST$..Z_KBN   Z01_4                                                     " & vbNewLine _
    '& " ON                                                                            " & vbNewLine _
    '& "   Z01_4.KBN_GROUP_CD  = 'E009'                                                " & vbNewLine _
    '& "  AND                                                                          " & vbNewLine _
    '& "   Z01_4.KBN_CD = '04'                                                         " & vbNewLine _
    '& " LEFT JOIN                                                                     " & vbNewLine _
    '& "   $LM_MST$..Z_KBN   Z01_5                                                     " & vbNewLine _
    '& " ON                                                                            " & vbNewLine _
    '& "   Z01_5.KBN_GROUP_CD  = 'E009'                                                " & vbNewLine _
    '& "  AND                                                                          " & vbNewLine _
    '& "   Z01_5.KBN_CD = '00'                                                         " & vbNewLine _

    '検索SQLメイン（EDI出荷中データ取得用）
    Private Const SQL_SELECT_EDI_M As String = " SELECT  " & vbNewLine _
                                              & "    H04.DEL_KB                                          AS DEL_KB            " & vbNewLine _
                                              & "  , H04.NRS_BR_CD                                       AS NRS_BR_CD         " & vbNewLine _
                                              & "  , H04.EDI_CTL_NO                                      AS EDI_CTL_NO        " & vbNewLine _
                                              & "  , H04.EDI_CTL_NO_CHU                                  AS EDI_CTL_NO_CHU    " & vbNewLine _
                                              & "  , H04.OUTKA_CTL_NO                                    AS OUTKA_CTL_NO      " & vbNewLine _
                                              & "  , H04.OUTKA_CTL_NO_CHU                                AS OUTKA_CTL_NO_CHU  " & vbNewLine _
                                              & "  , H04.COA_YN                                          AS COA_YN            " & vbNewLine _
                                              & "  , H04.CUST_ORD_NO_DTL                                 AS CUST_ORD_NO_DTL   " & vbNewLine _
                                              & "  , H04.BUYER_ORD_NO_DTL                                AS BUYER_ORD_NO_DTL  " & vbNewLine _
                                              & "  , H04.CUST_GOODS_CD                                   AS CUST_GOODS_CD     " & vbNewLine _
                                              & "  , H04.NRS_GOODS_CD                                    AS NRS_GOODS_CD      " & vbNewLine _
                                              & "  , H04.GOODS_NM                                        AS GOODS_NM          " & vbNewLine _
                                              & "  , H04.RSV_NO                                          AS RSV_NO            " & vbNewLine _
                                              & "  , H04.LOT_NO                                          AS LOT_NO            " & vbNewLine _
                                              & "  , H04.SERIAL_NO                                       AS SERIAL_NO         " & vbNewLine _
                                              & "  , H04.ALCTD_KB                                        AS ALCTD_KB          " & vbNewLine _
                                              & "  , Z01_1.KBN_NM1                                       AS ALCTD_KB_NM       " & vbNewLine _
                                              & "  , H04.OUTKA_PKG_NB                                    AS OUTKA_PKG_NB      " & vbNewLine _
                                              & "  , H04.OUTKA_HASU                                      AS OUTKA_HASU        " & vbNewLine _
                                              & "  , H04.OUTKA_QT                                        AS OUTKA_QT          " & vbNewLine _
                                              & "  , H04.OUTKA_TTL_NB                                    AS OUTKA_TTL_NB      " & vbNewLine _
                                              & "  , H04.OUTKA_TTL_QT                                    AS OUTKA_TTL_QT      " & vbNewLine _
                                              & "  , H04.KB_UT                                           AS KB_UT             " & vbNewLine _
                                              & "  , H04.QT_UT                                           AS QT_UT             " & vbNewLine _
                                              & "  , H04.PKG_NB                                          AS PKG_NB            " & vbNewLine _
                                              & "  , H04.PKG_UT                                          AS PKG_UT            " & vbNewLine _
                                              & "  , H04.ONDO_KB                                         AS ONDO_KB           " & vbNewLine _
                                              & "  , H04.UNSO_ONDO_KB                                    AS UNSO_ONDO_KB      " & vbNewLine _
                                              & "  , H04.IRIME                                           AS IRIME             " & vbNewLine _
                                              & "  , H04.IRIME_UT                                        AS IRIME_UT          " & vbNewLine _
                                              & "  , H04.BETU_WT                                         AS BETU_WT           " & vbNewLine _
                                              & "  , H04.REMARK                                          AS REMARK            " & vbNewLine _
                                              & "  , H04.OUT_KB                                          AS OUT_KB            " & vbNewLine _
                                              & "  , H04.AKAKURO_KB                                      AS AKAKURO_KB        " & vbNewLine _
                                              & "  , H04.JISSEKI_FLAG                                    AS JISSEKI_FLAG      " & vbNewLine _
                                              & "  , H04.JISSEKI_USER                                    AS JISSEKI_USER      " & vbNewLine _
                                              & "  , H04.JISSEKI_DATE                                    AS JISSEKI_DATE      " & vbNewLine _
                                              & "  , H04.JISSEKI_TIME                                    AS JISSEKI_TIME      " & vbNewLine _
                                              & "  , H04.SET_KB                                          AS SET_KB            " & vbNewLine _
                                              & "  , H04.FREE_N01                                        AS FREE_N01          " & vbNewLine _
                                              & "  , H04.FREE_N02                                        AS FREE_N02          " & vbNewLine _
                                              & "  , H04.FREE_N03                                        AS FREE_N03          " & vbNewLine _
                                              & "  , H04.FREE_N04                                        AS FREE_N04          " & vbNewLine _
                                              & "  , H04.FREE_N05                                        AS FREE_N05          " & vbNewLine _
                                              & "  , H04.FREE_N06                                        AS FREE_N06          " & vbNewLine _
                                              & "  , H04.FREE_N07                                        AS FREE_N07          " & vbNewLine _
                                              & "  , H04.FREE_N08                                        AS FREE_N08          " & vbNewLine _
                                              & "  , H04.FREE_N09                                        AS FREE_N09          " & vbNewLine _
                                              & "  , H04.FREE_N10                                        AS FREE_N10          " & vbNewLine _
                                              & "  , H04.FREE_C01                                        AS FREE_C01          " & vbNewLine _
                                              & "  , H04.FREE_C02                                        AS FREE_C02          " & vbNewLine _
                                              & "  , H04.FREE_C03                                        AS FREE_C03          " & vbNewLine _
                                              & "  , H04.FREE_C04                                        AS FREE_C04          " & vbNewLine _
                                              & "  , H04.FREE_C05                                        AS FREE_C05          " & vbNewLine _
                                              & "  , H04.FREE_C06                                        AS FREE_C06          " & vbNewLine _
                                              & "  , H04.FREE_C07                                        AS FREE_C07          " & vbNewLine _
                                              & "  , H04.FREE_C08                                        AS FREE_C08          " & vbNewLine _
                                              & "  , H04.FREE_C09                                        AS FREE_C09          " & vbNewLine _
                                              & "  , H04.FREE_C10                                        AS FREE_C10          " & vbNewLine _
                                              & "  , H04.FREE_C11                                        AS FREE_C11          " & vbNewLine _
                                              & "  , H04.FREE_C12                                        AS FREE_C12          " & vbNewLine _
                                              & "  , H04.FREE_C13                                        AS FREE_C13          " & vbNewLine _
                                              & "  , H04.FREE_C14                                        AS FREE_C14          " & vbNewLine _
                                              & "  , H04.FREE_C15                                        AS FREE_C15          " & vbNewLine _
                                              & "  , H04.FREE_C16                                        AS FREE_C16          " & vbNewLine _
                                              & "  , H04.FREE_C17                                        AS FREE_C17          " & vbNewLine _
                                              & "  , H04.FREE_C18                                        AS FREE_C18          " & vbNewLine _
                                              & "  , H04.FREE_C19                                        AS FREE_C19          " & vbNewLine _
                                              & "  , H04.FREE_C20                                        AS FREE_C20          " & vbNewLine _
                                              & "  , H04.FREE_C21                                        AS FREE_C21          " & vbNewLine _
                                              & "  , H04.FREE_C22                                        AS FREE_C22          " & vbNewLine _
                                              & "  , H04.FREE_C23                                        AS FREE_C23          " & vbNewLine _
                                              & "  , H04.FREE_C24                                        AS FREE_C24          " & vbNewLine _
                                              & "  , H04.FREE_C25                                        AS FREE_C25          " & vbNewLine _
                                              & "  , H04.FREE_C26                                        AS FREE_C26          " & vbNewLine _
                                              & "  , H04.FREE_C27                                        AS FREE_C27          " & vbNewLine _
                                              & "  , H04.FREE_C28                                        AS FREE_C28          " & vbNewLine _
                                              & "  , H04.FREE_C29                                        AS FREE_C29          " & vbNewLine _
                                              & "  , H04.FREE_C30                                        AS FREE_C30          " & vbNewLine _
                                              & "  , H04.CRT_USER                                        AS CRT_USER          " & vbNewLine _
                                              & "  , H04.CRT_DATE                                        AS CRT_DATE          " & vbNewLine _
                                              & "  , H04.CRT_TIME                                        AS CRT_TIME          " & vbNewLine _
                                              & "  , H04.UPD_USER                                        AS UPD_USER          " & vbNewLine _
                                              & "  , H04.UPD_DATE                                        AS UPD_DATE          " & vbNewLine _
                                              & "  , H04.UPD_TIME                                        AS UPD_TIME          " & vbNewLine _
                                              & "  , H04.SCM_CTL_NO_L                                    AS SCM_CTL_NO_L      " & vbNewLine _
                                              & "  , H04.SCM_CTL_NO_M                                    AS SCM_CTL_NO_M      " & vbNewLine _
                                              & "  , H04.SYS_DEL_FLG                                     AS SYS_DEL_FLG       " & vbNewLine _
                                              & "  , H04.SYS_DEL_FLG                                     AS JYOTAI            " & vbNewLine _
                                              & "  --, Z01_2.KBN_NM1                                     AS IRIME_UT_NM       " & vbNewLine _
                                              & "  --, Z01_3.KBN_NM1                                     AS PKG_UT_NM         " & vbNewLine _
                                              & "  --, Z01_4.KBN_NM1                                     AS KB_UT_NM          " & vbNewLine _
                                              & "  --, Z01_5.KBN_NM1                                     AS QT_UT_NM          " & vbNewLine _
                                              & " FROM                                                                        " & vbNewLine _
                                              & "    $LM_TRN$..H_OUTKAEDI_M H04                                               " & vbNewLine _
                                              & " LEFT JOIN                                                                   " & vbNewLine _
                                              & "   $LM_MST$..Z_KBN   Z01_1                                                   " & vbNewLine _
                                              & " ON                                                                          " & vbNewLine _
                                              & "   Z01_1.KBN_GROUP_CD  = 'S041'                                              " & vbNewLine _
                                              & "  AND                                                                        " & vbNewLine _
                                              & "   Z01_1.KBN_CD = H04.ALCTD_KB                                               " & vbNewLine _
                                              & " --LEFT JOIN                                                                   " & vbNewLine _
                                              & "   --$LM_MST$..Z_KBN   Z01_2                                                   " & vbNewLine _
                                              & " --ON                                                                          " & vbNewLine _
                                              & "   --Z01_2.KBN_GROUP_CD  = 'I001'                                              " & vbNewLine _
                                              & "  --AND                                                                        " & vbNewLine _
                                              & "   --Z01_2.KBN_CD = H04.IRIME_UT                                               " & vbNewLine _
                                              & " --LEFT JOIN                                                                   " & vbNewLine _
                                              & "   --$LM_MST$..Z_KBN   Z01_3                                                   " & vbNewLine _
                                              & " --ON                                                                          " & vbNewLine _
                                              & "   --Z01_3.KBN_GROUP_CD  = 'N001'                                              " & vbNewLine _
                                              & "  --AND                                                                        " & vbNewLine _
                                              & "   --Z01_3.KBN_CD = H04.PKG_UT                                                 " & vbNewLine _
                                              & " --LEFT JOIN                                                                   " & vbNewLine _
                                              & "   --$LM_MST$..Z_KBN   Z01_4                                                   " & vbNewLine _
                                              & " --ON                                                                          " & vbNewLine _
                                              & "   --Z01_4.KBN_GROUP_CD  = 'K002'                                              " & vbNewLine _
                                              & "  --AND                                                                        " & vbNewLine _
                                              & "   --Z01_4.KBN_CD = H04.KB_UT                                                  " & vbNewLine _
                                              & " --LEFT JOIN                                                                   " & vbNewLine _
                                              & "   --$LM_MST$..Z_KBN   Z01_5                                                   " & vbNewLine _
                                              & " --ON                                                                          " & vbNewLine _
                                              & "   --Z01_5.KBN_GROUP_CD  = 'I001'                                              " & vbNewLine _
                                              & "  --AND                                                                        " & vbNewLine _
                                              & "   --Z01_5.KBN_CD = H04.QT_UT                                                  " & vbNewLine _
                                              & " WHERE                                                                       " & vbNewLine _
                                              & "    H04.NRS_BR_CD  = @NRS_BR_CD                                              " & vbNewLine _
                                              & "  AND                                                                        " & vbNewLine _
                                              & "    H04.EDI_CTL_NO = @EDI_CTL_NO                                             " & vbNewLine _
                                              & " ORDER BY                                                                    " & vbNewLine _
                                              & "    H04.EDI_CTL_NO                                                           " & vbNewLine _
                                              & "  , H04.EDI_CTL_NO_CHU                                                       " & vbNewLine _
                                              & "  , H04.SYS_DEL_FLG                                                          " & vbNewLine

    'Free項目マスタ取得SQL（LM共通）
    Private Const SQL_SELECT_FREE_HD As String = " SELECT                                                   " & vbNewLine _
                                               & "    M67.NRS_BR_CD                     AS NRS_BR_CD        " & vbNewLine _
                                               & "  , M67.CUST_CD_L                     AS CUST_CD_L        " & vbNewLine _
                                               & "  , M67.CUST_CD_M                     AS CUST_CD_M        " & vbNewLine _
                                               & "  , M67.INOUT_KB                      AS INOUT_KB         " & vbNewLine _
                                               & "  , M67.DATA_KB                       AS DATA_KB          " & vbNewLine _
                                               & "  , M67.SEQ_NO                        AS SEQ_NO           " & vbNewLine _
                                               & "  , M67.DB_COL_NM                     AS DB_COL_NM        " & vbNewLine _
                                               & "  , M67.FIELD_NM                      AS FIELD_NM         " & vbNewLine _
                                               & "  , M67.NUM_DIGITS_INT                AS NUM_DIGITS_INT   " & vbNewLine _
                                               & "  , M67.NUM_DIGITS_DEC                AS NUM_DIGITS_DEC   " & vbNewLine _
                                               & "  , M67.INPUT_MANAGE_KB               AS INPUT_MANAGE_KB  " & vbNewLine _
                                               & "  , M67.ROW_VISIBLE_FLAG              AS ROW_VISIBLE_FLAG " & vbNewLine _
                                               & "  , M67.EDIT_ABLE_FLAG                AS EDIT_ABLE_FLAG   " & vbNewLine _
                                               & "  , M67.SORT_NO                       AS SORT_NO          " & vbNewLine _
                                               & "  , M67.DB_COL_NM+FIELD_NM            AS FREE_STATE       " & vbNewLine _
                                               & " FROM                                                     " & vbNewLine _
                                               & "    $LM_MST$..M_FREE_STATE M67                            " & vbNewLine _
                                               & " WHERE                                                    " & vbNewLine _
                                               & "    M67.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                               & "  AND                                                     " & vbNewLine _
                                               & "    M67.CUST_CD_L = @CUST_CD_L                            " & vbNewLine _
                                               & "  AND                                                     " & vbNewLine _
                                               & "    M67.CUST_CD_M = @CUST_CD_M                            " & vbNewLine _
                                               & "  AND                                                     " & vbNewLine _
                                               & "    M67.INOUT_KB = '20'                                   " & vbNewLine

    'Free項目マスタ取得SQL（L限定）
    Private Const SQL_SELECT_FREE_L As String = "  AND                   " & vbNewLine _
                                              & "    M67.DATA_KB  = '10' " & vbNewLine _
                                              & " ORDER BY               " & vbNewLine _
                                              & "    M67.SORT_NO         " & vbNewLine

    'Free項目マスタ取得SQL（M限定）
    Private Const SQL_SELECT_FREE_M As String = "  AND                   " & vbNewLine _
                                              & "    M67.DATA_KB  = '20' " & vbNewLine _
                                              & " ORDER BY               " & vbNewLine _
                                              & "    M67.SORT_NO         " & vbNewLine

    ' FFEM入出荷EDIデータ(ヘッダ) 取得
    Private Const SQL_SELECT_INOUTKAEDI_HED_FJF As String = "" _
        & "SELECT                                              " & vbNewLine _
        & "      H_INOUTKAEDI_HED_FJF.ZFVYHKKBN  AS ZFVYHKKBN  " & vbNewLine _
        & "    , H_INOUTKAEDI_HED_FJF.ZFVYDENTYP AS ZFVYDENTYP " & vbNewLine _
        & "FROM                                                " & vbNewLine _
        & "    $LM_TRN$..H_INOUTKAEDI_HED_FJF                  " & vbNewLine _
        & "WHERE                                               " & vbNewLine _
        & "    H_INOUTKAEDI_HED_FJF.NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
        & "AND H_INOUTKAEDI_HED_FJF.EDI_CTL_NO = @EDI_CTL_NO   " & vbNewLine _
        & "AND H_INOUTKAEDI_HED_FJF.INOUT_KB = '0'             " & vbNewLine _
        & "AND H_INOUTKAEDI_HED_FJF.DEL_KB IN('0','2')         " & vbNewLine _
        & ""

    ' 特定の荷主固有のテーブルが存在するか否かの判定SQL
    Private Const SQL_GET_TRN_TBL_EXISTS As String = "" _
        & "SELECT                                                       " & vbNewLine _
        & "    CASE WHEN OBJECT_ID('$LM_TRN$..' + @TBL_NM, 'U') IS NULL " & vbNewLine _
        & "        THEN '0'                                             " & vbNewLine _
        & "        ELSE '1'                                             " & vbNewLine _
        & "    END AS TBL_EXISTS                                        " & vbNewLine _
        & ""

#End Region '検索処理SQL

#Region "保存処理SQL"

    '保存処理 SQL（EDI出荷大）
    Private Const SQL_UPDATE_EDI_L As String = " UPDATE                                           " & vbNewLine _
                                             & " $LM_TRN$..H_OUTKAEDI_L                           " & vbNewLine _
                                             & " SET                                              " & vbNewLine _
                                             & "    DEL_KB                  = @DEL_KB             " & vbNewLine _
                                             & "  , OUTKA_CTL_NO            = @OUTKA_CTL_NO       " & vbNewLine _
                                             & "  , OUTKA_KB                = @OUTKA_KB           " & vbNewLine _
                                             & "  , SYUBETU_KB              = @SYUBETU_KB         " & vbNewLine _
                                             & "  , NAIGAI_KB               = @NAIGAI_KB          " & vbNewLine _
                                             & "  , OUTKA_STATE_KB          = @OUTKA_STATE_KB     " & vbNewLine _
                                             & "  , OUTKAHOKOKU_YN          = @OUTKAHOKOKU_YN     " & vbNewLine _
                                             & "  , PICK_KB                 = @PICK_KB            " & vbNewLine _
                                             & "  , OUTKA_PLAN_DATE         = @OUTKA_PLAN_DATE    " & vbNewLine _
                                             & "  , OUTKO_DATE              = @OUTKO_DATE         " & vbNewLine _
                                             & "  , ARR_PLAN_DATE           = @ARR_PLAN_DATE      " & vbNewLine _
                                             & "  , ARR_PLAN_TIME           = @ARR_PLAN_TIME      " & vbNewLine _
                                             & "  , HOKOKU_DATE             = @HOKOKU_DATE        " & vbNewLine _
                                             & "  , TOUKI_HOKAN_YN          = @TOUKI_HOKAN_YN     " & vbNewLine _
                                             & "  , SHIP_CD_L               = @SHIP_CD_L          " & vbNewLine _
                                             & "  , SHIP_NM_L               = @SHIP_NM_L          " & vbNewLine _
                                             & "  , EDI_DEST_CD             = @EDI_DEST_CD        " & vbNewLine _
                                             & "  , DEST_CD                 = @DEST_CD            " & vbNewLine _
                                             & "  , DEST_NM                 = @DEST_NM            " & vbNewLine _
                                             & "  , DEST_ZIP                = @DEST_ZIP           " & vbNewLine _
                                             & "  , DEST_AD_1               = @DEST_AD_1          " & vbNewLine _
                                             & "  , DEST_AD_2               = @DEST_AD_2          " & vbNewLine _
                                             & "  , DEST_AD_3               = @DEST_AD_3          " & vbNewLine _
                                             & "  , DEST_AD_4               = @DEST_AD_4          " & vbNewLine _
                                             & "  , DEST_AD_5               = @DEST_AD_5          " & vbNewLine _
                                             & "  , DEST_TEL                = @DEST_TEL           " & vbNewLine _
                                             & "  , DEST_FAX                = @DEST_FAX           " & vbNewLine _
                                             & "  , DEST_MAIL               = @DEST_MAIL          " & vbNewLine _
                                             & "  , DEST_JIS_CD             = @DEST_JIS_CD        " & vbNewLine _
                                             & "  , SP_NHS_KB               = @SP_NHS_KB          " & vbNewLine _
                                             & "  , COA_YN                  = @COA_YN             " & vbNewLine _
                                             & "  , CUST_ORD_NO             = @CUST_ORD_NO        " & vbNewLine _
                                             & "  , BUYER_ORD_NO            = @BUYER_ORD_NO       " & vbNewLine _
                                             & "  , UNSO_MOTO_KB            = @UNSO_MOTO_KB       " & vbNewLine _
                                             & "  , UNSO_TEHAI_KB           = @UNSO_TEHAI_KB      " & vbNewLine _
                                             & "  , SYARYO_KB               = @SYARYO_KB          " & vbNewLine _
                                             & "  , BIN_KB                  = @BIN_KB             " & vbNewLine _
                                             & "  , UNSO_CD                 = @UNSO_CD            " & vbNewLine _
                                             & "  , UNSO_NM                 = @UNSO_NM            " & vbNewLine _
                                             & "  , UNSO_BR_CD              = @UNSO_BR_CD         " & vbNewLine _
                                             & "  , UNSO_BR_NM              = @UNSO_BR_NM         " & vbNewLine _
                                             & "  , UNCHIN_TARIFF_CD        = @UNCHIN_TARIFF_CD   " & vbNewLine _
                                             & "  , EXTC_TARIFF_CD          = @EXTC_TARIFF_CD     " & vbNewLine _
                                             & "  , REMARK                  = @REMARK             " & vbNewLine _
                                             & "  , UNSO_ATT                = @UNSO_ATT           " & vbNewLine _
                                             & "  , DENP_YN                 = @DENP_YN            " & vbNewLine _
                                             & "  , PC_KB                   = @PC_KB              " & vbNewLine _
                                             & "  , UNCHIN_YN               = @UNCHIN_YN          " & vbNewLine _
                                             & "  , NIYAKU_YN               = @NIYAKU_YN          " & vbNewLine _
                                             & "  , OUT_FLAG                = @OUT_FLAG           " & vbNewLine _
                                             & "  , AKAKURO_KB              = @AKAKURO_KB         " & vbNewLine _
                                             & "  , FREE_N01                = @FREE_N01           " & vbNewLine _
                                             & "  , FREE_N02                = @FREE_N02           " & vbNewLine _
                                             & "  , FREE_N03                = @FREE_N03           " & vbNewLine _
                                             & "  , FREE_N04                = @FREE_N04           " & vbNewLine _
                                             & "  , FREE_N05                = @FREE_N05           " & vbNewLine _
                                             & "  , FREE_N06                = @FREE_N06           " & vbNewLine _
                                             & "  , FREE_N07                = @FREE_N07           " & vbNewLine _
                                             & "  , FREE_N08                = @FREE_N08           " & vbNewLine _
                                             & "  , FREE_N09                = @FREE_N09           " & vbNewLine _
                                             & "  , FREE_N10                = @FREE_N10           " & vbNewLine _
                                             & "  , FREE_C01                = @FREE_C01           " & vbNewLine _
                                             & "  , FREE_C02                = @FREE_C02           " & vbNewLine _
                                             & "  , FREE_C03                = @FREE_C03           " & vbNewLine _
                                             & "  , FREE_C04                = @FREE_C04           " & vbNewLine _
                                             & "  , FREE_C05                = @FREE_C05           " & vbNewLine _
                                             & "  , FREE_C06                = @FREE_C06           " & vbNewLine _
                                             & "  , FREE_C07                = @FREE_C07           " & vbNewLine _
                                             & "  , FREE_C08                = @FREE_C08           " & vbNewLine _
                                             & "  , FREE_C09                = @FREE_C09           " & vbNewLine _
                                             & "  , FREE_C10                = @FREE_C10           " & vbNewLine _
                                             & "  , FREE_C11                = @FREE_C11           " & vbNewLine _
                                             & "  , FREE_C12                = @FREE_C12           " & vbNewLine _
                                             & "  , FREE_C13                = @FREE_C13           " & vbNewLine _
                                             & "  , FREE_C14                = @FREE_C14           " & vbNewLine _
                                             & "  , FREE_C15                = @FREE_C15           " & vbNewLine _
                                             & "  , FREE_C16                = @FREE_C16           " & vbNewLine _
                                             & "  , FREE_C17                = @FREE_C17           " & vbNewLine _
                                             & "  , FREE_C18                = @FREE_C18           " & vbNewLine _
                                             & "  , FREE_C19                = @FREE_C19           " & vbNewLine _
                                             & "  , FREE_C20                = @FREE_C20           " & vbNewLine _
                                             & "  , FREE_C21                = @FREE_C21           " & vbNewLine _
                                             & "  , FREE_C22                = @FREE_C22           " & vbNewLine _
                                             & "  , FREE_C23                = @FREE_C23           " & vbNewLine _
                                             & "  , FREE_C24                = @FREE_C24           " & vbNewLine _
                                             & "  , FREE_C25                = @FREE_C25           " & vbNewLine _
                                             & "  , FREE_C26                = @FREE_C26           " & vbNewLine _
                                             & "  , FREE_C27                = @FREE_C27           " & vbNewLine _
                                             & "  , FREE_C28                = @FREE_C28           " & vbNewLine _
                                             & "  , FREE_C29                = @FREE_C29           " & vbNewLine _
                                             & "  , FREE_C30                = @FREE_C30           " & vbNewLine _
                                             & "  , UPD_USER                = @UPD_USER           " & vbNewLine _
                                             & "  , UPD_DATE                = @UPD_DATE           " & vbNewLine _
                                             & "  , UPD_TIME                = @UPD_TIME           " & vbNewLine _
                                             & "  , EDIT_FLAG               = @EDIT_FLAG          " & vbNewLine _
                                             & "  , SYS_UPD_DATE            = @SYS_UPD_DATE       " & vbNewLine _
                                             & "  , SYS_UPD_TIME            = @SYS_UPD_TIME       " & vbNewLine _
                                             & "  , SYS_UPD_PGID            = @SYS_UPD_PGID       " & vbNewLine _
                                             & "  , SYS_UPD_USER            = @SYS_UPD_USER       " & vbNewLine _
                                             & " WHERE                                            " & vbNewLine _
                                             & "   NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
                                             & " AND                                              " & vbNewLine _
                                             & "   EDI_CTL_NO = @EDI_CTL_NO                       " & vbNewLine


    '保存処理 SQL（EDI出荷中）
    Private Const SQL_UPDATE_EDI_M As String = " UPDATE                                                " & vbNewLine _
                                             & " $LM_TRN$..H_OUTKAEDI_M                                " & vbNewLine _
                                             & " SET                                                   " & vbNewLine _
                                             & "    DEL_KB                     = @DEL_KB               " & vbNewLine _
                                             & "  , NRS_BR_CD                  = @NRS_BR_CD            " & vbNewLine _
                                             & "  , EDI_CTL_NO                 = @EDI_CTL_NO           " & vbNewLine _
                                             & "  , EDI_CTL_NO_CHU             = @EDI_CTL_NO_CHU       " & vbNewLine _
                                             & "  , COA_YN                     = @COA_YN               " & vbNewLine _
                                             & "  , CUST_ORD_NO_DTL            = @CUST_ORD_NO_DTL      " & vbNewLine _
                                             & "  , BUYER_ORD_NO_DTL           = @BUYER_ORD_NO_DTL     " & vbNewLine _
                                             & "  , CUST_GOODS_CD              = @CUST_GOODS_CD        " & vbNewLine _
                                             & "  , NRS_GOODS_CD               = @NRS_GOODS_CD         " & vbNewLine _
                                             & "  , GOODS_NM                   = @GOODS_NM             " & vbNewLine _
                                             & "  , RSV_NO                     = @RSV_NO               " & vbNewLine _
                                             & "  , LOT_NO                     = @LOT_NO               " & vbNewLine _
                                             & "  , SERIAL_NO                  = @SERIAL_NO            " & vbNewLine _
                                             & "  , ALCTD_KB                   = @ALCTD_KB             " & vbNewLine _
                                             & "  , OUTKA_PKG_NB               = @OUTKA_PKG_NB         " & vbNewLine _
                                             & "  , OUTKA_HASU                 = @OUTKA_HASU           " & vbNewLine _
                                             & "  , OUTKA_QT                   = @OUTKA_QT             " & vbNewLine _
                                             & "  , OUTKA_TTL_NB               = @OUTKA_TTL_NB         " & vbNewLine _
                                             & "  , OUTKA_TTL_QT               = @OUTKA_TTL_QT         " & vbNewLine _
                                             & "  , PKG_NB                     = @PKG_NB               " & vbNewLine _
                                             & "  , IRIME                      = @IRIME                " & vbNewLine _
                                             & "  , REMARK                     = @REMARK               " & vbNewLine _
                                             & "  , FREE_N01                   = @FREE_N01             " & vbNewLine _
                                             & "  , FREE_N02                   = @FREE_N02             " & vbNewLine _
                                             & "  , FREE_N03                   = @FREE_N03             " & vbNewLine _
                                             & "  , FREE_N04                   = @FREE_N04             " & vbNewLine _
                                             & "  , FREE_N05                   = @FREE_N05             " & vbNewLine _
                                             & "  , FREE_N06                   = @FREE_N06             " & vbNewLine _
                                             & "  , FREE_N07                   = @FREE_N07             " & vbNewLine _
                                             & "  , FREE_N08                   = @FREE_N08             " & vbNewLine _
                                             & "  , FREE_N09                   = @FREE_N09             " & vbNewLine _
                                             & "  , FREE_N10                   = @FREE_N10             " & vbNewLine _
                                             & "  , FREE_C01                   = @FREE_C01             " & vbNewLine _
                                             & "  , FREE_C02                   = @FREE_C02             " & vbNewLine _
                                             & "  , FREE_C03                   = @FREE_C03             " & vbNewLine _
                                             & "  , FREE_C04                   = @FREE_C04             " & vbNewLine _
                                             & "  , FREE_C05                   = @FREE_C05             " & vbNewLine _
                                             & "  , FREE_C06                   = @FREE_C06             " & vbNewLine _
                                             & "  , FREE_C07                   = @FREE_C07             " & vbNewLine _
                                             & "  , FREE_C08                   = @FREE_C08             " & vbNewLine _
                                             & "  , FREE_C09                   = @FREE_C09             " & vbNewLine _
                                             & "  , FREE_C10                   = @FREE_C10             " & vbNewLine _
                                             & "  , FREE_C11                   = @FREE_C11             " & vbNewLine _
                                             & "  , FREE_C12                   = @FREE_C12             " & vbNewLine _
                                             & "  , FREE_C13                   = @FREE_C13             " & vbNewLine _
                                             & "  , FREE_C14                   = @FREE_C14             " & vbNewLine _
                                             & "  , FREE_C15                   = @FREE_C15             " & vbNewLine _
                                             & "  , FREE_C16                   = @FREE_C16             " & vbNewLine _
                                             & "  , FREE_C17                   = @FREE_C17             " & vbNewLine _
                                             & "  , FREE_C18                   = @FREE_C18             " & vbNewLine _
                                             & "  , FREE_C19                   = @FREE_C19             " & vbNewLine _
                                             & "  , FREE_C20                   = @FREE_C20             " & vbNewLine _
                                             & "  , FREE_C21                   = @FREE_C21             " & vbNewLine _
                                             & "  , FREE_C22                   = @FREE_C22             " & vbNewLine _
                                             & "  , FREE_C23                   = @FREE_C23             " & vbNewLine _
                                             & "  , FREE_C24                   = @FREE_C24             " & vbNewLine _
                                             & "  , FREE_C25                   = @FREE_C25             " & vbNewLine _
                                             & "  , FREE_C26                   = @FREE_C26             " & vbNewLine _
                                             & "  , FREE_C27                   = @FREE_C27             " & vbNewLine _
                                             & "  , FREE_C28                   = @FREE_C28             " & vbNewLine _
                                             & "  , FREE_C29                   = @FREE_C29             " & vbNewLine _
                                             & "  , FREE_C30                   = @FREE_C30             " & vbNewLine _
                                             & "  , UPD_USER                   = @UPD_USER             " & vbNewLine _
                                             & "  , UPD_DATE                   = @UPD_DATE             " & vbNewLine _
                                             & "  , UPD_TIME                   = @UPD_TIME             " & vbNewLine _
                                             & "  , SYS_UPD_DATE               = @SYS_UPD_DATE         " & vbNewLine _
                                             & "  , SYS_UPD_TIME               = @SYS_UPD_TIME         " & vbNewLine _
                                             & "  , SYS_UPD_PGID               = @SYS_UPD_PGID         " & vbNewLine _
                                             & "  , SYS_UPD_USER               = @SYS_UPD_USER         " & vbNewLine _
                                             & "  , SYS_DEL_FLG                = @SYS_DEL_FLG          " & vbNewLine _
                                             & "  , KB_UT                      = @KB_UT                " & vbNewLine _
                                             & "  , QT_UT                      = @QT_UT                " & vbNewLine _
                                             & "  , PKG_UT                     = @PKG_UT               " & vbNewLine _
                                             & "  , IRIME_UT                   = @IRIME_UT             " & vbNewLine _
                                             & " WHERE                                                 " & vbNewLine _
                                             & "   NRS_BR_CD  = @NRS_BR_CD                             " & vbNewLine _
                                             & "  AND                                                  " & vbNewLine _
                                             & "   EDI_CTL_NO = @EDI_CTL_NO                            " & vbNewLine _
                                             & "  AND                                                  " & vbNewLine _
                                             & "   EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU                    " & vbNewLine

    '受信テーブル更新SQL
    Private Const SQL_UPDATE_JYUSIN_L As String = " UPDATE                                                           " & vbNewLine _
                                              & "  $LM_TRN$..$TABLE_NM$                                            " & vbNewLine _
                                              & " SET                                                              " & vbNewLine _
                                              & "    UPD_USER         = @UPD_USER                                  " & vbNewLine _
                                              & "  , UPD_DATE         = @UPD_DATE                                  " & vbNewLine _
                                              & "  , UPD_TIME         = @UPD_TIME                                  " & vbNewLine _
                                              & "  , SYS_UPD_DATE     = @SYS_UPD_DATE                              " & vbNewLine _
                                              & "  , SYS_UPD_TIME     = @SYS_UPD_TIME                              " & vbNewLine _
                                              & "  , SYS_UPD_PGID     = @SYS_UPD_PGID                              " & vbNewLine _
                                              & "  , SYS_UPD_USER     = @SYS_UPD_USER                              " & vbNewLine _
                                              & " WHERE                                                            " & vbNewLine _
                                              & "   NRS_BR_CD  = @NRS_BR_CD                                        " & vbNewLine _
                                              & "  AND                                                             " & vbNewLine _
                                              & "   EDI_CTL_NO = @EDI_CTL_NO                                       " & vbNewLine

    '受信テーブル更新SQL
    Private Const SQL_UPDATE_JYUSIN_M As String = " UPDATE                                                           " & vbNewLine _
                                              & "  $LM_TRN$..$TABLE_NM$                                            " & vbNewLine _
                                              & " SET                                                              " & vbNewLine _
                                              & "    UPD_USER         = @UPD_USER                                  " & vbNewLine _
                                              & "  , UPD_DATE         = @UPD_DATE                                  " & vbNewLine _
                                              & "  , UPD_TIME         = @UPD_TIME                                  " & vbNewLine _
                                              & "  , SYS_UPD_DATE     = @SYS_UPD_DATE                              " & vbNewLine _
                                              & "  , SYS_UPD_TIME     = @SYS_UPD_TIME                              " & vbNewLine _
                                              & "  , SYS_UPD_PGID     = @SYS_UPD_PGID                              " & vbNewLine _
                                              & "  , SYS_UPD_USER     = @SYS_UPD_USER                              " & vbNewLine _
                                              & "  , DEL_KB           = @DEL_KB                                    " & vbNewLine _
                                              & "  , SYS_DEL_FLG      = @SYS_DEL_FLG                               " & vbNewLine _
                                              & "  , DELETE_USER      = @DELETE_USER                               " & vbNewLine _
                                              & "  , DELETE_DATE      = @DELETE_DATE                               " & vbNewLine _
                                              & "  , DELETE_TIME      = @DELETE_TIME                               " & vbNewLine _
                                              & " WHERE                                                            " & vbNewLine _
                                              & "   NRS_BR_CD  = @NRS_BR_CD                                        " & vbNewLine _
                                              & "  AND                                                             " & vbNewLine _
                                              & "   EDI_CTL_NO = @EDI_CTL_NO                                       " & vbNewLine _
                                              & "  AND                                                             " & vbNewLine _
                                              & "   EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU                               " & vbNewLine



#End Region '保存処理SQL

#Region "取込日付取得SQL"

    Private Const SQL_SELECT_TORIKOMI_DATE As String = "   SELECT                                                             " & vbNewLine _
                                                     & "     MAX(MAIN.UNTIN_CALCULATION_KB) AS UNTIN_CALCULATION_KB           " & vbNewLine _
                                                     & "   , MAX(MAIN.HOKAN_SKYU_DATE)      AS HOKAN_SKYU_DATE                " & vbNewLine _
                                                     & "   , MAX(MAIN.NIYAKU_SKYU_DATE)     AS NIYAKU_SKYU_DATE               " & vbNewLine _
                                                     & "   , MAX(MAIN.UNCHIN_SKYU_DATE)     AS UNCHIN_SKYU_DATE               " & vbNewLine _
                                                     & "   , MAX(MAIN.YOKOMOCHI_SKYU_DATE)  AS YOKOMOCHI_SKYU_DATE            " & vbNewLine _
                                                     & "   , MAX(MAIN.SAGYO_SKYU_DATE)      AS SAGYO_SKYU_DATE                " & vbNewLine _
                                                     & " FROM                                                                 " & vbNewLine _
                                                     & "  (                                                                   " & vbNewLine _
                                                     & "       SELECT                                                         " & vbNewLine _
                                                     & "           M07.UNTIN_CALCULATION_KB AS UNTIN_CALCULATION_KB           " & vbNewLine _
                                                     & "         , G01.SKYU_DATE            AS HOKAN_SKYU_DATE                " & vbNewLine _
                                                     & "         , ''                       AS NIYAKU_SKYU_DATE               " & vbNewLine _
                                                     & "         , ''                       AS UNCHIN_SKYU_DATE               " & vbNewLine _
                                                     & "         , ''                       AS YOKOMOCHI_SKYU_DATE            " & vbNewLine _
                                                     & "         , ''                       AS SAGYO_SKYU_DATE                " & vbNewLine _
                                                     & "       FROM                                                           " & vbNewLine _
                                                     & "        (                                                             " & vbNewLine _
                                                     & "          SELECT                                                      " & vbNewLine _
                                                     & "             UNTIN_CALCULATION_KB                                     " & vbNewLine _
                                                     & "           , HOKAN_SEIQTO_CD                                          " & vbNewLine _
                                                     & "          FROM                                                        " & vbNewLine _
                                                     & "            $LM_MST$..M_CUST M07                                      " & vbNewLine _
                                                     & "          WHERE                                                       " & vbNewLine _
                                                     & "            M07.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_M = '00'                                      " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_S = '00'                                      " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_SS = '00'                                     " & vbNewLine _
                                                     & "        ) M07                                                         " & vbNewLine _
                                                     & "       LEFT JOIN                                                      " & vbNewLine _
                                                     & "        (                                                             " & vbNewLine _
                                                     & "          SELECT                                                      " & vbNewLine _
                                                     & "              SEIQTO_CD         AS SEIQTO_CD                          " & vbNewLine _
                                                     & "            , MAX(SKYU_DATE)    AS SKYU_DATE                          " & vbNewLine _
                                                     & "          FROM                                                        " & vbNewLine _
                                                     & "            $LM_TRN$..G_KAGAMI_HED G01                                " & vbNewLine _
                                                     & "          WHERE                                                       " & vbNewLine _
                                                     & "            G01.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            G01.RB_FLG = '00'                                         " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "           (                                                          " & vbNewLine _
                                                     & "             (                                                        " & vbNewLine _
                                                     & "               G01.CRT_KB = '00'                                      " & vbNewLine _
                                                     & "               AND                                                    " & vbNewLine _
                                                     & "                 ( G01.STATE_KB in ('01', '02')                       " & vbNewLine _
                                                     & "                   OR                                                 " & vbNewLine _
                                                     & "                    (                                                 " & vbNewLine _
                                                     & "                      G01.STATE_KB = '00' AND G01.STORAGE_KB = '01'   " & vbNewLine _
                                                     & "                    )                                                 " & vbNewLine _
                                                     & "                 )                                                    " & vbNewLine _
                                                     & "              )                                                       " & vbNewLine _
                                                     & "             OR                                                       " & vbNewLine _
                                                     & "             (                                                        " & vbNewLine _
                                                     & "               STATE_KB in ('03', '04')                               " & vbNewLine _
                                                     & "             )                                                        " & vbNewLine _
                                                     & "           )                                                          " & vbNewLine _
                                                     & "          GROUP BY                                                    " & vbNewLine _
                                                     & "          G01.SEIQTO_CD                                               " & vbNewLine _
                                                     & "         ) G01                                                        " & vbNewLine _
                                                     & "       ON                                                             " & vbNewLine _
                                                     & "         M07.HOKAN_SEIQTO_CD = G01.SEIQTO_CD                          " & vbNewLine _
                                                     & "                                                                      " & vbNewLine _
                                                     & "       UNION                                                          " & vbNewLine _
                                                     & "                                                                      " & vbNewLine _
                                                     & "       SELECT                                                         " & vbNewLine _
                                                     & "           M07.UNTIN_CALCULATION_KB AS UNTIN_CALCULATION_KB           " & vbNewLine _
                                                     & "         , ''                       AS HOKAN_SKYU_DATE                " & vbNewLine _
                                                     & "         , G01.SKYU_DATE            AS NIYAKU_SKYU_DATE               " & vbNewLine _
                                                     & "         , ''                       AS UNCHIN_SKYU_DATE               " & vbNewLine _
                                                     & "         , ''                       AS YOKOMOCHI_SKYU_DATE            " & vbNewLine _
                                                     & "         , ''                       AS SAGYO_SKYU_DATE                " & vbNewLine _
                                                     & "       FROM                                                           " & vbNewLine _
                                                     & "        (                                                             " & vbNewLine _
                                                     & "          SELECT                                                      " & vbNewLine _
                                                     & "             UNTIN_CALCULATION_KB                                     " & vbNewLine _
                                                     & "           , NIYAKU_SEIQTO_CD                                         " & vbNewLine _
                                                     & "          FROM                                                        " & vbNewLine _
                                                     & "            $LM_MST$..M_CUST M07                                      " & vbNewLine _
                                                     & "          WHERE                                                       " & vbNewLine _
                                                     & "            M07.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_M = '00'                                      " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_S = '00'                                      " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            M07.CUST_CD_SS = '00'                                     " & vbNewLine _
                                                     & "        ) M07                                                         " & vbNewLine _
                                                     & "       LEFT JOIN                                                      " & vbNewLine _
                                                     & "        (                                                             " & vbNewLine _
                                                     & "          SELECT                                                      " & vbNewLine _
                                                     & "              SEIQTO_CD         AS SEIQTO_CD                          " & vbNewLine _
                                                     & "            , MAX(SKYU_DATE)    AS SKYU_DATE                          " & vbNewLine _
                                                     & "          FROM                                                        " & vbNewLine _
                                                     & "            $LM_TRN$..G_KAGAMI_HED G01                                " & vbNewLine _
                                                     & "          WHERE                                                       " & vbNewLine _
                                                     & "            G01.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "            G01.RB_FLG = '00'                                         " & vbNewLine _
                                                     & "           AND                                                        " & vbNewLine _
                                                     & "           (                                                          " & vbNewLine _
                                                     & "             (                                                        " & vbNewLine _
                                                     & "               G01.CRT_KB = '00'                                      " & vbNewLine _
                                                     & "               AND                                                    " & vbNewLine _
                                                     & "                 ( G01.STATE_KB in ('01', '02')                       " & vbNewLine _
                                                     & "                   OR                                                 " & vbNewLine _
                                                     & "                    (                                                 " & vbNewLine _
                                                     & "                      G01.STATE_KB = '00' AND G01.HANDLING_KB = '01'   " & vbNewLine _
                                                     & "                    )                                                  " & vbNewLine _
                                                     & "                 )                                                     " & vbNewLine _
                                                     & "              )                                                        " & vbNewLine _
                                                     & "             OR                                                        " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               STATE_KB in ('03', '04')                                " & vbNewLine _
                                                     & "             )                                                         " & vbNewLine _
                                                     & "           )                                                           " & vbNewLine _
                                                     & "          GROUP BY                                                     " & vbNewLine _
                                                     & "          G01.SEIQTO_CD                                                " & vbNewLine _
                                                     & "         ) G01                                                         " & vbNewLine _
                                                     & "       ON                                                              " & vbNewLine _
                                                     & "         M07.NIYAKU_SEIQTO_CD = G01.SEIQTO_CD                          " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "       UNION                                                           " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "       SELECT                                                          " & vbNewLine _
                                                     & "           M07.UNTIN_CALCULATION_KB AS UNTIN_CALCULATION_KB            " & vbNewLine _
                                                     & "         , ''                       AS HOKAN_SKYU_DATE                 " & vbNewLine _
                                                     & "         , ''                       AS NIYAKU_SKYU_DATE                " & vbNewLine _
                                                     & "         , G01.SKYU_DATE            AS UNCHIN_SKYU_DATE                " & vbNewLine _
                                                     & "         , ''                       AS YOKOMOCHI_SKYU_DATE             " & vbNewLine _
                                                     & "         , ''                       AS SAGYO_SKYU_DATE                 " & vbNewLine _
                                                     & "       FROM                                                            " & vbNewLine _
                                                     & "        (                                                              " & vbNewLine _
                                                     & "          SELECT                                                       " & vbNewLine _
                                                     & "             UNTIN_CALCULATION_KB                                      " & vbNewLine _
                                                     & "           , UNCHIN_SEIQTO_CD                                          " & vbNewLine _
                                                     & "          FROM                                                         " & vbNewLine _
                                                     & "            $LM_MST$..M_CUST M07                                       " & vbNewLine _
                                                     & "          WHERE                                                        " & vbNewLine _
                                                     & "            M07.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_M = '00'                                       " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_S = '00'                                       " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_SS = '00'                                      " & vbNewLine _
                                                     & "        ) M07                                                          " & vbNewLine _
                                                     & "       LEFT JOIN                                                       " & vbNewLine _
                                                     & "        (                                                              " & vbNewLine _
                                                     & "          SELECT                                                       " & vbNewLine _
                                                     & "              SEIQTO_CD         AS SEIQTO_CD                           " & vbNewLine _
                                                     & "            , MAX(SKYU_DATE)    AS SKYU_DATE                           " & vbNewLine _
                                                     & "          FROM                                                         " & vbNewLine _
                                                     & "            $LM_TRN$..G_KAGAMI_HED G01                                 " & vbNewLine _
                                                     & "          WHERE                                                        " & vbNewLine _
                                                     & "            G01.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            G01.RB_FLG = '00'                                          " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "           (                                                           " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               G01.CRT_KB = '00'                                       " & vbNewLine _
                                                     & "               AND                                                     " & vbNewLine _
                                                     & "                 ( G01.STATE_KB in ('01', '02')                        " & vbNewLine _
                                                     & "                   OR                                                  " & vbNewLine _
                                                     & "                    (                                                  " & vbNewLine _
                                                     & "                      G01.STATE_KB = '00' AND G01.UNCHIN_KB = '01'     " & vbNewLine _
                                                     & "                    )                                                  " & vbNewLine _
                                                     & "                 )                                                     " & vbNewLine _
                                                     & "              )                                                        " & vbNewLine _
                                                     & "             OR                                                        " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               STATE_KB in ('03', '04')                                " & vbNewLine _
                                                     & "             )                                                         " & vbNewLine _
                                                     & "           )                                                           " & vbNewLine _
                                                     & "          GROUP BY                                                     " & vbNewLine _
                                                     & "          G01.SEIQTO_CD                                                " & vbNewLine _
                                                     & "         ) G01                                                         " & vbNewLine _
                                                     & "       ON                                                              " & vbNewLine _
                                                     & "         M07.UNCHIN_SEIQTO_CD = G01.SEIQTO_CD                          " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "       UNION                                                           " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "       SELECT                                                          " & vbNewLine _
                                                     & "           M07.UNTIN_CALCULATION_KB AS UNTIN_CALCULATION_KB            " & vbNewLine _
                                                     & "         , ''                       AS HOKAN_SKYU_DATE                 " & vbNewLine _
                                                     & "         , ''                       AS NIYAKU_SKYU_DATE                " & vbNewLine _
                                                     & "         , ''                       AS UNCHIN_SKYU_DATE                " & vbNewLine _
                                                     & "         , G01.SKYU_DATE            AS YOKOMOCHI_SKYU_DATE             " & vbNewLine _
                                                     & "         , ''                       AS SAGYO_SKYU_DATE                 " & vbNewLine _
                                                     & "       FROM                                                            " & vbNewLine _
                                                     & "        (                                                              " & vbNewLine _
                                                     & "          SELECT                                                       " & vbNewLine _
                                                     & "             UNTIN_CALCULATION_KB                                      " & vbNewLine _
                                                     & "           , UNCHIN_SEIQTO_CD                                          " & vbNewLine _
                                                     & "          FROM                                                         " & vbNewLine _
                                                     & "            $LM_MST$..M_CUST M07                                       " & vbNewLine _
                                                     & "          WHERE                                                        " & vbNewLine _
                                                     & "            M07.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_M = '00'                                       " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_S = '00'                                       " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_SS = '00'                                      " & vbNewLine _
                                                     & "        ) M07                                                          " & vbNewLine _
                                                     & "       LEFT JOIN                                                       " & vbNewLine _
                                                     & "        (                                                              " & vbNewLine _
                                                     & "          SELECT                                                       " & vbNewLine _
                                                     & "              SEIQTO_CD         AS SEIQTO_CD                           " & vbNewLine _
                                                     & "            , MAX(SKYU_DATE)    AS SKYU_DATE                           " & vbNewLine _
                                                     & "          FROM                                                         " & vbNewLine _
                                                     & "            $LM_TRN$..G_KAGAMI_HED G01                                 " & vbNewLine _
                                                     & "          WHERE                                                        " & vbNewLine _
                                                     & "            G01.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            G01.RB_FLG = '00'                                          " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "           (                                                           " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               G01.CRT_KB = '00'                                       " & vbNewLine _
                                                     & "               AND                                                     " & vbNewLine _
                                                     & "                 ( G01.STATE_KB in ('01', '02')                        " & vbNewLine _
                                                     & "                   OR                                                  " & vbNewLine _
                                                     & "                    (                                                  " & vbNewLine _
                                                     & "                      G01.STATE_KB = '00' AND G01.YOKOMOCHI_KB = '01'  " & vbNewLine _
                                                     & "                    )                                                  " & vbNewLine _
                                                     & "                 )                                                     " & vbNewLine _
                                                     & "              )                                                        " & vbNewLine _
                                                     & "             OR                                                        " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               STATE_KB in ('03', '04')                                " & vbNewLine _
                                                     & "             )                                                         " & vbNewLine _
                                                     & "           )                                                           " & vbNewLine _
                                                     & "          GROUP BY                                                     " & vbNewLine _
                                                     & "          G01.SEIQTO_CD                                                " & vbNewLine _
                                                     & "         ) G01                                                         " & vbNewLine _
                                                     & "       ON                                                              " & vbNewLine _
                                                     & "         M07.UNCHIN_SEIQTO_CD = G01.SEIQTO_CD                          " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "       UNION                                                           " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "       SELECT                                                          " & vbNewLine _
                                                     & "           M07.UNTIN_CALCULATION_KB AS UNTIN_CALCULATION_KB            " & vbNewLine _
                                                     & "         , ''                       AS HOKAN_SKYU_DATE                 " & vbNewLine _
                                                     & "         , ''                       AS NIYAKU_SKYU_DATE                " & vbNewLine _
                                                     & "         , ''                       AS UNCHIN_SKYU_DATE                " & vbNewLine _
                                                     & "         , G01.SKYU_DATE            AS YOKOMOCHI_SKYU_DATE             " & vbNewLine _
                                                     & "         , ''                       AS SAGYO_SKYU_DATE                 " & vbNewLine _
                                                     & "       FROM                                                            " & vbNewLine _
                                                     & "        (                                                              " & vbNewLine _
                                                     & "          SELECT                                                       " & vbNewLine _
                                                     & "             UNTIN_CALCULATION_KB                                      " & vbNewLine _
                                                     & "           , SAGYO_SEIQTO_CD                                           " & vbNewLine _
                                                     & "          FROM                                                         " & vbNewLine _
                                                     & "            $LM_MST$..M_CUST M07                                       " & vbNewLine _
                                                     & "          WHERE                                                        " & vbNewLine _
                                                     & "            M07.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_M = '00'                                       " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_S = '00'                                       " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            M07.CUST_CD_SS = '00'                                      " & vbNewLine _
                                                     & "        ) M07                                                          " & vbNewLine _
                                                     & "       LEFT JOIN                                                       " & vbNewLine _
                                                     & "        (                                                              " & vbNewLine _
                                                     & "          SELECT                                                       " & vbNewLine _
                                                     & "              SEIQTO_CD         AS SEIQTO_CD                           " & vbNewLine _
                                                     & "            , MAX(SKYU_DATE)    AS SKYU_DATE                           " & vbNewLine _
                                                     & "          FROM                                                         " & vbNewLine _
                                                     & "            $LM_TRN$..G_KAGAMI_HED G01                                 " & vbNewLine _
                                                     & "          WHERE                                                        " & vbNewLine _
                                                     & "            G01.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "            G01.RB_FLG = '00'                                          " & vbNewLine _
                                                     & "           AND                                                         " & vbNewLine _
                                                     & "           (                                                           " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               G01.CRT_KB = '00'                                       " & vbNewLine _
                                                     & "               AND                                                     " & vbNewLine _
                                                     & "                 ( G01.STATE_KB in ('01', '02')                        " & vbNewLine _
                                                     & "                   OR                                                  " & vbNewLine _
                                                     & "                    (                                                  " & vbNewLine _
                                                     & "                      G01.STATE_KB = '00' AND G01.SAGYO_KB = '01'      " & vbNewLine _
                                                     & "                    )                                                  " & vbNewLine _
                                                     & "                 )                                                     " & vbNewLine _
                                                     & "              )                                                        " & vbNewLine _
                                                     & "             OR                                                        " & vbNewLine _
                                                     & "             (                                                         " & vbNewLine _
                                                     & "               STATE_KB in ('03', '04')                                " & vbNewLine _
                                                     & "             )                                                         " & vbNewLine _
                                                     & "           )                                                           " & vbNewLine _
                                                     & "          GROUP BY                                                     " & vbNewLine _
                                                     & "          G01.SEIQTO_CD                                                " & vbNewLine _
                                                     & "         ) G01                                                         " & vbNewLine _
                                                     & "       ON                                                              " & vbNewLine _
                                                     & "         M07.SAGYO_SEIQTO_CD = G01.SEIQTO_CD                           " & vbNewLine _
                                                     & "                                                                       " & vbNewLine _
                                                     & "   ) MAIN                                                              " & vbNewLine



#End Region '取込日付取得SQL

#End Region 'Const

#Region "Field"

    '検索条件設定用
    Private _Row As DataRow

    '発行SQL作成用
    Private _StrSql As StringBuilder

    'パラメータ設定用
    Private _SqlPrmList As ArrayList

    '受信テーブル名（ヘッダ）
    Private _RcvNmHed As String

    '受信テーブル名（明細）
    Private _RcvNmDtl As String

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function SelectAction(ByVal ds As DataSet) As DataSet

        '受信テーブル名取得
        ds = Me.SelectRcvNm(ds)

        '取得失敗時、排他日付取得処理スキップ
        If String.IsNullOrEmpty(Me._RcvNmHed) = False Then
            '受信テーブル排他日付取得
            ds = Me.GetRcvHaitaDate(ds, LMH040DAC.SQL_GET_HAITA_HED, SelectCondition.PTN2)
        End If

        'EDI出荷Lデータ取得
        ds = Me.GetEdiOutkaLData(ds, LMH040DAC.SQL_SELECT_EDI_L, SelectCondition.PTN4)

        'EDI出荷Mデータ取得
        ds = Me.GetEdiOutkaMData(ds, LMH040DAC.SQL_SELECT_EDI_M, SelectCondition.PTN5)

        '自由項目Lデータ取得
        ds = Me.GetFreeLData(ds, LMH040DAC.SQL_SELECT_FREE_HD + LMH040DAC.SQL_SELECT_FREE_L, SelectCondition.PTN6)

        '自由項目Mデータ取得
        ds = Me.GetFreeMData(ds, LMH040DAC.SQL_SELECT_FREE_HD + LMH040DAC.SQL_SELECT_FREE_M, SelectCondition.PTN6)

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(FFEM入出荷EDIデータ(ヘッダ))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectActionInoutkaEdiHedFjf(ByVal ds As DataSet) As DataSet

        ' FFEM入出荷EDIデータ(ヘッダ) 取得
        ds = Me.GetInoutkaEdiHedFjfData(ds, LMH040DAC.SQL_SELECT_INOUTKAEDI_HED_FJF, SelectCondition.PTN8)

        Return ds

    End Function

    ''' <summary>
    ''' 受信テーブル名取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="sql"></param>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRcvTableNM(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"RCV_NM_HED" _
                                          , "RCV_NM_DTL"
                                            }
        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_RCV_NM, sql, ptn, str)
    End Function

    ''' <summary>
    ''' 受信テーブル排他日付取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="sql"></param>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRcvHaitaDate(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"NRS_BR_CD" _
                                          , "EDI_CTL_NO" _
                                          , "SYS_UPD_DATE" _
                                          , "SYS_UPD_TIME"
                                            }
        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        sql = Me.SetRcvNm(sql, Me._RcvNmHed)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_RCV, sql, ptn, str)
    End Function

    ''' <summary>
    ''' EDI出荷Lデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetEdiOutkaLData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "EDI_CTL_NO" _
                                            , "OUTKA_CTL_NO" _
                                            , "OUTKA_KB" _
                                            , "SYUBETU_KB" _
                                            , "NAIGAI_KB" _
                                            , "OUTKA_STATE_KB" _
                                            , "OUTKAHOKOKU_YN" _
                                            , "PICK_KB" _
                                            , "NRS_BR_NM" _
                                            , "WH_CD" _
                                            , "WH_NM" _
                                            , "OUTKA_PLAN_DATE" _
                                            , "OUTKO_DATE" _
                                            , "ARR_PLAN_DATE" _
                                            , "ARR_PLAN_TIME" _
                                            , "HOKOKU_DATE" _
                                            , "TOUKI_HOKAN_YN" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "SHIP_CD_L" _
                                            , "SHIP_CD_M" _
                                            , "SHIP_NM_L" _
                                            , "SHIP_NM_M" _
                                            , "EDI_DEST_CD" _
                                            , "DEST_CD" _
                                            , "DEST_NM" _
                                            , "DEST_ZIP" _
                                            , "DEST_AD_1" _
                                            , "DEST_AD_2" _
                                            , "DEST_AD_3" _
                                            , "DEST_AD_4" _
                                            , "DEST_AD_5" _
                                            , "DEST_TEL" _
                                            , "DEST_FAX" _
                                            , "DEST_MAIL" _
                                            , "DEST_JIS_CD" _
                                            , "SP_NHS_KB" _
                                            , "COA_YN" _
                                            , "CUST_ORD_NO" _
                                            , "BUYER_ORD_NO" _
                                            , "UNSO_MOTO_KB" _
                                            , "UNSO_TEHAI_KB" _
                                            , "SYARYO_KB" _
                                            , "BIN_KB" _
                                            , "UNSO_CD" _
                                            , "UNSO_NM" _
                                            , "UNSO_BR_CD" _
                                            , "UNSO_BR_NM" _
                                            , "UNCHIN_TARIFF_CD" _
                                            , "EXTC_TARIFF_CD" _
                                            , "REMARK" _
                                            , "UNSO_ATT" _
                                            , "DENP_YN" _
                                            , "PC_KB" _
                                            , "UNCHIN_YN" _
                                            , "NIYAKU_YN" _
                                            , "OUT_FLAG" _
                                            , "AKAKURO_KB" _
                                            , "JISSEKI_FLAG" _
                                            , "JISSEKI_USER" _
                                            , "JISSEKI_DATE" _
                                            , "JISSEKI_TIME" _
                                            , "FREE_N01" _
                                            , "FREE_N02" _
                                            , "FREE_N03" _
                                            , "FREE_N04" _
                                            , "FREE_N05" _
                                            , "FREE_N06" _
                                            , "FREE_N07" _
                                            , "FREE_N08" _
                                            , "FREE_N09" _
                                            , "FREE_N10" _
                                            , "FREE_C01" _
                                            , "FREE_C02" _
                                            , "FREE_C03" _
                                            , "FREE_C04" _
                                            , "FREE_C05" _
                                            , "FREE_C06" _
                                            , "FREE_C07" _
                                            , "FREE_C08" _
                                            , "FREE_C09" _
                                            , "FREE_C10" _
                                            , "FREE_C11" _
                                            , "FREE_C12" _
                                            , "FREE_C13" _
                                            , "FREE_C14" _
                                            , "FREE_C15" _
                                            , "FREE_C16" _
                                            , "FREE_C17" _
                                            , "FREE_C18" _
                                            , "FREE_C19" _
                                            , "FREE_C20" _
                                            , "FREE_C21" _
                                            , "FREE_C22" _
                                            , "FREE_C23" _
                                            , "FREE_C24" _
                                            , "FREE_C25" _
                                            , "FREE_C26" _
                                            , "FREE_C27" _
                                            , "FREE_C28" _
                                            , "FREE_C29" _
                                            , "FREE_C30" _
                                            , "CRT_USER" _
                                            , "CRT_DATE" _
                                            , "CRT_TIME" _
                                            , "UPD_USER" _
                                            , "UPD_DATE" _
                                            , "UPD_TIME" _
                                            , "SCM_CTL_NO_L" _
                                            , "EDIT_FLAG" _
                                            , "EDI_STATE_KB" _
                                            , "UNCHIN_TARIFF_REM" _
                                            , "YOKO_REM" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME"
                                            }

        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_EDI_L, sql, ptn, str)

    End Function

    ''' <summary>
    ''' EDI出荷Mデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetEdiOutkaMData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"DEL_KB" _
                                            , "NRS_BR_CD" _
                                            , "EDI_CTL_NO" _
                                            , "EDI_CTL_NO_CHU" _
                                            , "OUTKA_CTL_NO" _
                                            , "OUTKA_CTL_NO_CHU" _
                                            , "COA_YN" _
                                            , "CUST_ORD_NO_DTL" _
                                            , "BUYER_ORD_NO_DTL" _
                                            , "CUST_GOODS_CD" _
                                            , "NRS_GOODS_CD" _
                                            , "GOODS_NM" _
                                            , "RSV_NO" _
                                            , "LOT_NO" _
                                            , "SERIAL_NO" _
                                            , "ALCTD_KB" _
                                            , "ALCTD_KB_NM" _
                                            , "OUTKA_PKG_NB" _
                                            , "OUTKA_HASU" _
                                            , "OUTKA_QT" _
                                            , "OUTKA_TTL_NB" _
                                            , "OUTKA_TTL_QT" _
                                            , "KB_UT" _
                                            , "QT_UT" _
                                            , "PKG_NB" _
                                            , "PKG_UT" _
                                            , "ONDO_KB" _
                                            , "UNSO_ONDO_KB" _
                                            , "IRIME" _
                                            , "IRIME_UT" _
                                            , "BETU_WT" _
                                            , "REMARK" _
                                            , "OUT_KB" _
                                            , "AKAKURO_KB" _
                                            , "JISSEKI_FLAG" _
                                            , "JISSEKI_USER" _
                                            , "JISSEKI_DATE" _
                                            , "JISSEKI_TIME" _
                                            , "SET_KB" _
                                            , "FREE_N01" _
                                            , "FREE_N02" _
                                            , "FREE_N03" _
                                            , "FREE_N04" _
                                            , "FREE_N05" _
                                            , "FREE_N06" _
                                            , "FREE_N07" _
                                            , "FREE_N08" _
                                            , "FREE_N09" _
                                            , "FREE_N10" _
                                            , "FREE_C01" _
                                            , "FREE_C02" _
                                            , "FREE_C03" _
                                            , "FREE_C04" _
                                            , "FREE_C05" _
                                            , "FREE_C06" _
                                            , "FREE_C07" _
                                            , "FREE_C08" _
                                            , "FREE_C09" _
                                            , "FREE_C10" _
                                            , "FREE_C11" _
                                            , "FREE_C12" _
                                            , "FREE_C13" _
                                            , "FREE_C14" _
                                            , "FREE_C15" _
                                            , "FREE_C16" _
                                            , "FREE_C17" _
                                            , "FREE_C18" _
                                            , "FREE_C19" _
                                            , "FREE_C20" _
                                            , "FREE_C21" _
                                            , "FREE_C22" _
                                            , "FREE_C23" _
                                            , "FREE_C24" _
                                            , "FREE_C25" _
                                            , "FREE_C26" _
                                            , "FREE_C27" _
                                            , "FREE_C28" _
                                            , "FREE_C29" _
                                            , "FREE_C30" _
                                            , "CRT_USER" _
                                            , "CRT_DATE" _
                                            , "CRT_TIME" _
                                            , "UPD_USER" _
                                            , "UPD_DATE" _
                                            , "UPD_TIME" _
                                            , "SCM_CTL_NO_L" _
                                            , "SCM_CTL_NO_M" _
                                            , "SYS_DEL_FLG" _
                                            , "JYOTAI"
                                            }
        ', "IRIME_UT_NM" _
        ', "PKG_UT_NM" _
        ', "KB_UT_NM" _
        ', "QT_UT_NM" _
        '}

        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_EDI_M, sql, ptn, str)

    End Function

    ''' <summary>
    ''' 自由項目Lデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetFreeLData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"NRS_BR_CD" _
                                           , "CUST_CD_L" _
                                           , "CUST_CD_M" _
                                           , "INOUT_KB" _
                                           , "DATA_KB" _
                                           , "SEQ_NO" _
                                           , "DB_COL_NM" _
                                           , "FIELD_NM" _
                                           , "NUM_DIGITS_INT" _
                                           , "NUM_DIGITS_DEC" _
                                           , "INPUT_MANAGE_KB" _
                                           , "ROW_VISIBLE_FLAG" _
                                           , "EDIT_ABLE_FLAG" _
                                           , "SORT_NO" _
                                           , "FREE_STATE"
                                           }

        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_OUT_FREE_L, sql, ptn, str)

    End Function

    ''' <summary>
    ''' 自由項目Mデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetFreeMData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"NRS_BR_CD" _
                                           , "CUST_CD_L" _
                                           , "CUST_CD_M" _
                                           , "INOUT_KB" _
                                           , "DATA_KB" _
                                           , "SEQ_NO" _
                                           , "DB_COL_NM" _
                                           , "FIELD_NM" _
                                           , "NUM_DIGITS_INT" _
                                           , "NUM_DIGITS_DEC" _
                                           , "INPUT_MANAGE_KB" _
                                           , "ROW_VISIBLE_FLAG" _
                                           , "EDIT_ABLE_FLAG" _
                                           , "SORT_NO" _
                                           , "FREE_STATE"
                                           }

        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_OUT_FREE_M, sql, ptn, str)

    End Function

    ''' <summary>
    ''' FFEM入出荷EDIデータ(ヘッダ) 取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索イベント結果データ取得SQLの構築・発行</remarks>
    Private Function GetInoutkaEdiHedFjfData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition) As DataSet
        Dim str As String() = New String() {"ZFVYHKKBN" _
                                            , "ZFVYDENTYP"
                                            }

        '検索条件データ設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_INOUTKAEDI_HED_FJF, sql, ptn, str)

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH040_TBL_EXISTS")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH040DAC.SQL_GET_TRN_TBL_EXISTS)

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TBL_NM", Me._Row.Item("TBL_NM").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Me._Row.Item("TBL_EXISTS") = "0"

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

            ' パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    Me._Row.Item("TBL_EXISTS") = Convert.ToString(reader("TBL_EXISTS"))
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region '検索処理

#Region "データチェック"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function HaitaChekuAction(ByVal ds As DataSet) As DataSet

        '受信テーブル名取得
        ds = Me.SelectRcvNm(ds)

        '取得失敗時、受信テーブル排他チェック処理スキップ
        If String.IsNullOrEmpty(Me._RcvNmHed) = False Then
            '受信HED排他チェック
            Me._Row = ds.Tables(LMH040DAC.TABLE_NM_RCV).Rows(0)
            If Me.SelectHaitaDataSub(Me.SetRcvNm(LMH040DAC.SQL_HAITA_CHECK_HED, Me._RcvNmHed), SelectCondition.PTN3) = False Then
                Return ds
            End If
        End If

        'EDI出荷（大）排他チェック
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_EDI_L).Rows(0)
        If Me.SelectHaitaDataSub(LMH040DAC.SQL_HAITA_CHECK_L, SelectCondition.PTN3) = False Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック（編集時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HaitaCheckForEdit(ByVal ds As DataSet) As DataSet

        'EDI出荷（大）排他チェック
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_EDI_L).Rows(0)
        If Me.SelectHaitaDataSub(LMH040DAC.SQL_HAITA_CHECK_L, SelectCondition.PTN3) = False Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック（詳細）
    ''' </summary>
    ''' <param name="sql">排他チェックSQL</param>
    ''' <param name="pnt">パラメータパターン</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaDataSub(ByVal sql As String, ByVal pnt As LMH040DAC.SelectCondition) As Boolean

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, pnt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理結果格納変数
        Dim result As Boolean = False

        '排他チェック
        reader.Read()
        result = Me.HaitaResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return result

    End Function

    ''' <summary>
    ''' 取込日付チェック用データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TorikomiCheckAction(ByVal ds As DataSet) As DataSet

        '抽出条件設定
        Me._Row = ds.Tables(LMH040DAC.TABLE_NM_IN).Rows(0)

        'mapping項目設定
        Dim str As String() = New String() {"UNTIN_CALCULATION_KB" _
                                            , "HOKAN_SKYU_DATE" _
                                            , "NIYAKU_SKYU_DATE" _
                                            , "UNCHIN_SKYU_DATE" _
                                            , "YOKOMOCHI_SKYU_DATE" _
                                            , "SAGYO_SKYU_DATE" _
                                              }

        Return Me.SelectListData(ds, LMH040DAC.TABLE_NM_TORIKOMI_DATE, LMH040DAC.SQL_SELECT_TORIKOMI_DATE, LMH040DAC.SelectCondition.PTN7, str)

    End Function


#End Region 'データチェック

#Region "保存処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveAction(ByVal ds As DataSet) As DataSet

        '更新カウント変数初期化
        Dim cnt As Integer = 0

        '受信テーブル名取得
        ds = Me.SelectRcvNm(ds)

        Dim updateFlg As Integer = 0

        '取得失敗時、受信テーブル更新処理スキップ
        If String.IsNullOrEmpty(Me._RcvNmHed) = False Then
            updateFlg = 1
        End If

        'EDI出荷（大）更新
        Dim inTbl As DataTable = ds.Tables(LMH040DAC.TABLE_NM_EDI_L)
        For i As Integer = 0 To inTbl.Rows.Count() - 1
            Me._Row = inTbl.Rows(i)
            cnt = cnt + Me.UpdateSubAction(LMH040DAC.SQL_UPDATE_EDI_L, UpdateCondition.UP_PTN1)
        Next

        'EDI出荷（中）更新
        inTbl = ds.Tables(LMH040DAC.TABLE_NM_EDI_M)
        If inTbl.Rows.Count() > 0 Then
            For i As Integer = 0 To inTbl.Rows.Count() - 1
                Me._Row = inTbl.Rows(i)
                cnt = cnt + Me.UpdateSubAction(LMH040DAC.SQL_UPDATE_EDI_M, UpdateCondition.UP_PTN2)

                '状態項目が更新されるデータのみ受信テーブルを更新
                If updateFlg <> 0 AndAlso Me._Row.Item("SYS_DEL_FLG").Equals(Me._Row.Item("JYOTAI")) = False Then

                    '受信（明細）テーブル更新
                    'Me._Row = ds.Tables(LMH040DAC.TABLE_NM_RCV).Rows(0)
                    cnt = cnt + Me.UpdateSubAction(Me.SetRcvNm(LMH040DAC.SQL_UPDATE_JYUSIN_M, Me._RcvNmDtl), UpdateCondition.UP_PTN4)
                    updateFlg = 2

                End If

            Next

            '受信（明細）テーブルが１件以上更新された場合のみ、受信（ヘッダ）テーブル更新
            If updateFlg = 2 Then
                '受信（ヘッダ）テーブル更新
                Me._Row = ds.Tables(LMH040DAC.TABLE_NM_RCV).Rows(0)
                cnt = cnt + Me.UpdateSubAction(Me.SetRcvNm(LMH040DAC.SQL_UPDATE_JYUSIN_L, Me._RcvNmHed), UpdateCondition.UP_PTN3)
            End If

        End If

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理（詳細）
    ''' </summary>
    ''' <param name="sql">更新SQL</param>
    ''' <param name="ptn">パラメータ設定パターン</param>
    ''' <returns>更新件数</returns>
    ''' <remarks>該当テーブルUpdate</remarks>
    Private Function UpdateSubAction(ByVal sql As String, ByVal ptn As LMH040DAC.UpdateCondition) As Integer

        'SQL文格納変数
        Dim cmd As SqlCommand = New SqlCommand

        'SQL文のコンパイル
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateParam(Me._SqlPrmList, Me._Row, ptn)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'SQLログ作成
        MyBase.Logger.WriteSQLLog(LMH040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Return MyBase.GetUpdateResult(cmd)

    End Function

#End Region '保存処理

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' 受信テーブル名設定
    ''' </summary>
    ''' <param name="sql">SQL文</param>
    ''' <param name="rcvNm">置換するテーブル名</param>
    ''' <returns></returns>
    ''' <remarks>HED、DTL両方とも利用可能</remarks>
    Private Function SetRcvNm(ByVal sql As String, ByVal rcvNm As String) As String

        sql = sql.Replace("$TABLE_NM$", rcvNm)
        Return sql

    End Function

    ''' <summary>
    ''' 受信テーブル名設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectRcvNm(ByVal ds As DataSet) As DataSet

        '受信テーブル名取得
        ds = Me.GetRcvTableNM(ds, LMH040DAC.SQL_GET_RCV, SelectCondition.PTN1)

        '受信テーブル名設定
        If Me.ResultCountChk(MyBase.GetResultCount()) = True Then

            '受信テーブル名セット
            Me._RcvNmHed = ds.Tables(LMH040DAC.TABLE_NM_RCV_NM).Rows(0).Item("RCV_NM_HED").ToString()
            Me._RcvNmDtl = ds.Tables(LMH040DAC.TABLE_NM_RCV_NM).Rows(0).Item("RCV_NM_DTL").ToString()

        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNmOut">OUTデータテーブル名</param>
    ''' <param name="sql">発行SQL文</param>
    ''' <param name="ptn">パラメータ種別</param>
    ''' <param name="str">マッピングCOLデータ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNmOut As String, ByVal sql As String, ByVal ptn As LMH040DAC.SelectCondition, ByVal str As String()) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        'OUTデータセットの初期化
        ds.Tables(tblNmOut).Clear()

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNmOut)

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.HaitaResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' Insert文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function InsertResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.HaitaResultChk(MyBase.GetInsertResult(cmd))

    End Function

    ''' <summary>
    ''' 検索結果チェック（0件チェック）
    ''' </summary>
    ''' <param name="cnt">検索件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("G001")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 排他チェック（エラーメッセージ設定あり）
    ''' </summary>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function HaitaResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索結果チェック（0件チェック）エラーメッセージ設定なし
    ''' </summary>
    ''' <param name="cnt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResultCountChk(ByVal cnt As Integer) As Boolean
        '判定
        If cnt < 1 Then
            Return False
        End If
        Return True
    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール（検索）
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByRef prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMH040DAC.SelectCondition)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            'パラメータ設定
            Select Case ptn

                Case LMH040DAC.SelectCondition.PTN1   '受信テーブル名取得

                    prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN2   '排他日付取得

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
                    'prmList.Add(MyBase.GetSqlParameter("@RCV_NM_HED", Me._RcvNmHed, DBDataType.CHAR))
                    'prmList.Add(MyBase.GetSqlParameter("@RCV_NM_DTL", Me._RcvNmDtl, DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN3   '排他チェック

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN4   '検索（EDI出荷L）

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@MATOME_FLG", .Item("MATOME_FLG").ToString(), DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN5   '検索（EDI出荷M）

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN6   '検索（FREE）

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN7   '取込日付チェック

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    'prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

                Case LMH040DAC.SelectCondition.PTN8   '検索（FFEM入出荷EDIデータ(ヘッダ)）

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

            End Select

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール（保存）
    ''' </summary>
    ''' <param name="prmList"></param>
    ''' <param name="dr"></param>
    ''' <param name="ptn"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateParam(ByRef prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMH040DAC.UpdateCondition)

        With dr

            '共通パラメータ
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

            '▼▼▼
            'prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserName(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            '▲▲▲
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Call Me.SetTimeParameter(prmList)
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

            'パラメータ設定
            Select Case ptn

                Case LMH040DAC.UpdateCondition.UP_PTN1   'EDI出荷（大）

                    prmList.Add(MyBase.GetSqlParameter("@DEL_KB", "0"))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", .Item("OUTKA_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", .Item("NAIGAI_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", .Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Item("PICK_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", .Item("SHIP_NM_L").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", .Item("EDI_DEST_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", .Item("DEST_ZIP").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", .Item("DEST_AD_4").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", .Item("DEST_AD_5").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", .Item("DEST_FAX").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", .Item("DEST_MAIL").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", .Item("DEST_JIS_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", .Item("UNSO_MOTO_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", .Item("SYARYO_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", .Item("UNSO_NM").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", .Item("UNSO_BR_NM").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", .Item("UNSO_ATT").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Item("DENP_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", .Item("UNCHIN_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", .Item("OUT_FLAG").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", "01"))

                    'フリー項目パラメータセット
                    prmList = Me.SetFreeParam(prmList, dr)

                Case LMH040DAC.UpdateCondition.UP_PTN2   'EDI出荷（中）

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
                    '2012.03.15 要望番号895 大阪対応START
                    If (.Item("DEL_KB").ToString()).Equals("3") = True Then
                        prmList.Add(MyBase.GetSqlParameter("@DEL_KB", "0"))
                    Else
                        prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
                    End If
                    '2012.03.15 要望番号895 大阪対応END
                    prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Item("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", .Item("OUTKA_HASU").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", .Item("OUTKA_QT").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

                    '単位項目
                    prmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))

                    'フリー項目パラメータセット
                    prmList = Me.SetFreeParam(prmList, dr)

                Case LMH040DAC.UpdateCondition.UP_PTN4   'EDI受信（明細）
                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))

                    '▼▼▼
                    Dim sysTime As String = MyBase.GetSystemTime()

                    prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

                    If .Item("SYS_DEL_FLG").ToString() = "0" Then

                        prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.NVARCHAR))
                        prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.CHAR))
                        prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.CHAR))

                    Else

                        prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                        prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                        prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", Me.GetColonEditTime(sysTime), DBDataType.CHAR))

                    End If
                    '▲▲▲

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フリー項目パラメータセット（EDI出荷共通）
    ''' </summary>
    ''' <param name="prmList"></param>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFreeParam(ByRef prmList As ArrayList, ByVal dr As DataRow) As ArrayList

        With dr

            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", .Item("FREE_N01").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", .Item("FREE_N02").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", .Item("FREE_N03").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", .Item("FREE_N04").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", .Item("FREE_N05").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", .Item("FREE_N06").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", .Item("FREE_N07").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", .Item("FREE_N08").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", .Item("FREE_N09").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", .Item("FREE_N10").ToString(), DBDataType.NUMERIC))

            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.NVARCHAR))

        End With

        Return prmList
    End Function

    ''' <summary>
    ''' 更新時刻(hh:mm:ss)の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetTimeParameter(ByRef prmList As ArrayList) As String

        Dim sysTime As String = MyBase.GetSystemTime()

        '更新時刻(hh:mm:ss)
        prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(sysTime), DBDataType.CHAR))

        Return sysTime

    End Function

    ''' <summary>
    ''' コロン編集した時刻を取得
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String
        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))
    End Function

#End Region

End Class
