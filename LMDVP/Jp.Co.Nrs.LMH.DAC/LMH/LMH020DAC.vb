' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブ
'  プログラムID     :  LMH020    : EDI入荷編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1
        PTN2
        PTN3
        PTN4
    End Enum

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMH020IN"

    ''' <summary>
    ''' RCV_NMテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_RCV_NM As String = "RCV_NM"

    ''' <summary>
    ''' M_FREE_STATEテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_FREE As String = "M_FREE_STATE"

    ''' <summary>
    ''' INKAEDI_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_L As String = "INKAEDI_L"

    ''' <summary>
    ''' INKAEDI_Mテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M As String = "INKAEDI_M"

    ''' <summary>
    ''' CUSTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_CUST As String = "CUST"

    ''' <summary>
    ''' G_HEDテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMH020DAC"

    ''' <summary>
    ''' EDI受信テーブル更新条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JUSHIN_DATA As String = " SYS_DEL_FLG <> JYOTAI "

#End Region

#Region "検索処理 SQL"

#Region "M_EDI_CUST"

    Private Const SQL_SELECT_HED_TBL As String = "SELECT                                " & vbNewLine _
                                               & " M66_01.RCV_NM_HED AS RCV_NM_HED      " & vbNewLine _
                                               & ",M66_01.RCV_NM_DTL AS RCV_NM_DTL      " & vbNewLine _
                                               & " FROM $LM_MST$..M_EDI_CUST M66_01     " & vbNewLine _
                                               & "WHERE M66_01.NRS_BR_CD = @NRS_BR_CD   " & vbNewLine _
                                               & "  AND M66_01.WH_CD     = @WH_CD       " & vbNewLine _
                                               & "  AND M66_01.CUST_CD_L = @CUST_CD_L   " & vbNewLine _
                                               & "  AND M66_01.CUST_CD_M = @CUST_CD_M   " & vbNewLine _
                                               & "  AND M66_01.INOUT_KB  = '1'          " & vbNewLine


#End Region

#Region "M_FREE_STATE"

    Private Const SQL_SELECT_FREE As String = "SELECT                                                                           " & vbNewLine _
                                            & " M67_01.DB_COL_NM                                        AS      DB_COL_NM       " & vbNewLine _
                                            & ",M67_01.FIELD_NM                                         AS      FIELD_NM        " & vbNewLine _
                                            & ",M67_01.NUM_DIGITS_INT                                   AS      NUM_DIGITS_INT  " & vbNewLine _
                                            & ",M67_01.NUM_DIGITS_DEC                                   AS      NUM_DIGITS_DEC  " & vbNewLine _
                                            & ",M67_01.INPUT_MANAGE_KB                                  AS      INPUT_MANAGE_KB " & vbNewLine _
                                            & ",M67_01.ROW_VISIBLE_FLAG                                 AS      ROW_VISIBLE_FLAG" & vbNewLine _
                                            & ",M67_01.EDIT_ABLE_FLAG                                   AS      EDIT_ABLE_FLAG  " & vbNewLine _
                                            & ",RIGHT('00' + CONVERT(VARCHAR(2),M67_01.SORT_NO),2)      AS      SORT_NO         " & vbNewLine _
                                            & ",M67_01.DATA_KB                                          AS      DATA_KB         " & vbNewLine _
                                            & " FROM $LM_MST$..M_FREE_STATE M67_01                                              " & vbNewLine _
                                            & "WHERE M67_01.NRS_BR_CD   = @NRS_BR_CD                                            " & vbNewLine _
                                            & "  AND M67_01.CUST_CD_L   = @CUST_CD_L                                            " & vbNewLine _
                                            & "  AND M67_01.CUST_CD_M   = @CUST_CD_M                                            " & vbNewLine _
                                            & "  AND M67_01.DATA_KB    IN ( '10' , '20' )                                       " & vbNewLine _
                                            & "  AND M67_01.INOUT_KB    = '10'                                                  " & vbNewLine _
                                            & "  --AND M67_01.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                            & "ORDER BY M67_01.DATA_KB                                                          " & vbNewLine _
                                            & "        ,M67_01.SORT_NO                                                          " & vbNewLine


#End Region

#Region "H_INKAEDI_L"

    Private Const SQL_SELECT_L As String = "SELECT                                                                                                                    " & vbNewLine _
                                         & " H01_01.DEL_KB              AS      DEL_KB                                                                                " & vbNewLine _
                                         & ",H01_01.NRS_BR_CD           AS      NRS_BR_CD                                                                             " & vbNewLine _
                                         & ",H01_01.EDI_CTL_NO          AS      EDI_CTL_NO                                                                            " & vbNewLine _
                                         & ",H01_01.INKA_CTL_NO_L       AS      INKA_CTL_NO_L                                                                         " & vbNewLine _
                                         & ",H01_01.INKA_TP             AS      INKA_TP                                                                               " & vbNewLine _
                                         & ",H01_01.INKA_KB             AS      INKA_KB                                                                               " & vbNewLine _
                                         & ",H01_01.INKA_STATE_KB       AS      INKA_STATE_KB                                                                         " & vbNewLine _
                                         & ",H01_01.INKA_DATE           AS      INKA_DATE                                                                             " & vbNewLine _
                                         & ",''                         AS      INKA_TIME                                                                             " & vbNewLine _
                                         & ",H01_01.NRS_WH_CD           AS      NRS_WH_CD                                                                             " & vbNewLine _
                                         & ",H01_01.CUST_CD_L           AS      CUST_CD_L                                                                             " & vbNewLine _
                                         & ",H01_01.CUST_CD_M           AS      CUST_CD_M                                                                             " & vbNewLine _
                                         & ",CASE WHEN     RTRIM(H01_01.CUST_NM_L) = ''                                                                               " & vbNewLine _
                                         & "           AND RTRIM(H01_01.CUST_NM_M) = ''                                                                               " & vbNewLine _
                                         & "      THEN     M07_01.CUST_NM_L                                                                                           " & vbNewLine _
                                         & "      ELSE     H01_01.CUST_NM_L                                                                                           " & vbNewLine _
                                         & " END                        AS      CUST_NM_L                                                                             " & vbNewLine _
                                         & ",CASE WHEN     RTRIM(H01_01.CUST_NM_L) = ''                                                                               " & vbNewLine _
                                         & "           AND RTRIM(H01_01.CUST_NM_M) = ''                                                                               " & vbNewLine _
                                         & "      THEN     M07_01.CUST_NM_M                                                                                           " & vbNewLine _
                                         & "      ELSE     H01_01.CUST_NM_M                                                                                           " & vbNewLine _
                                         & " END                        AS      CUST_NM_M                                                                             " & vbNewLine _
                                         & ",H01_01.INKA_PLAN_QT        AS      INKA_PLAN_QT                                                                          " & vbNewLine _
                                         & ",H01_01.INKA_PLAN_QT_UT     AS      INKA_PLAN_QT_UT                                                                       " & vbNewLine _
                                         & ",H01_01.INKA_TTL_NB         AS      INKA_TTL_NB                                                                           " & vbNewLine _
                                         & ",''                         AS      NAIGAI_KB                                                                             " & vbNewLine _
                                         & ",H01_01.BUYER_ORD_NO        AS      BUYER_ORD_NO                                                                          " & vbNewLine _
                                         & ",H01_01.OUTKA_FROM_ORD_NO   AS      OUTKA_FROM_ORD_NO                                                                     " & vbNewLine _
                                         & ",H01_01.TOUKI_HOKAN_YN      AS      TOUKI_HOKAN_YN                                                                        " & vbNewLine _
                                         & ",H01_01.HOKAN_YN            AS      HOKAN_YN                                                                              " & vbNewLine _
                                         & ",H01_01.HOKAN_FREE_KIKAN    AS      HOKAN_FREE_KIKAN                                                                      " & vbNewLine _
                                         & ",H01_01.HOKAN_STR_DATE      AS      HOKAN_STR_DATE                                                                        " & vbNewLine _
                                         & ",H01_01.NIYAKU_YN           AS      NIYAKU_YN                                                                             " & vbNewLine _
                                         & "--2018/12/11 MOD START 要望管理003739（別の方法で実現するため000927の修正を元に戻す）                                     " & vbNewLine _
                                         & "-- --2018/04/11 000927【LMS】入荷登録_入荷データの保管料課税区分=免税設定 Annen upd start                                 " & vbNewLine _
                                         & "-- ,CASE WHEN (     H01_01.NRS_BR_CD = '40'                                                                               " & vbNewLine _
                                         & "--              AND H01_01.CUST_CD_L = '00003'                                                                            " & vbNewLine _
                                         & "--              AND H01_01.CUST_CD_M = '00'                                                                               " & vbNewLine _
                                         & "-- 			 AND H01_01.SYS_ENT_DATE = H01_01.SYS_UPD_DATE                                                                " & vbNewLine _
                                         & "-- 			 AND H01_01.SYS_ENT_TIME = H01_01.SYS_UPD_TIME)                                                               " & vbNewLine _
                                         & "-- 	  THEN   M07_01.TAX_KB                                                                                                " & vbNewLine _
                                         & "-- 	  ELSE   H01_01.TAX_KB                                                                                                " & vbNewLine _
                                         & "--       END                   AS      TAX_KB                                                                             " & vbNewLine _
                                         & ",H01_01.TAX_KB              AS      TAX_KB                                                                                " & vbNewLine _
                                         & "-- --2018/04/11 000927【LMS】入荷登録_入荷データの保管料課税区分=免税設定 Annen upd end                                   " & vbNewLine _
                                         & "--2018/12/11 MOD END   要望管理003739（別の方法で実現するため000927の修正を元に戻す）                                     " & vbNewLine _
                                         & ",H01_01.REMARK              AS      REMARK                                                                                " & vbNewLine _
                                         & ",H01_01.NYUBAN_L            AS      NYUBAN_L                                                                              " & vbNewLine _
                                         & ",H01_01.UNCHIN_TP           AS      UNCHIN_TP                                                                             " & vbNewLine _
                                         & ",H01_01.UNCHIN_KB           AS      UNCHIN_KB                                                                             " & vbNewLine _
                                         & ",H01_01.OUTKA_MOTO          AS      OUTKA_MOTO                                                                            " & vbNewLine _
                                         & ",M10_01.DEST_NM             AS      OUTKA_MOTO_NM                                                                         " & vbNewLine _
                                         & ",H01_01.SYARYO_KB           AS      SYARYO_KB                                                                             " & vbNewLine _
                                         & ",H01_01.UNSO_ONDO_KB        AS      UNSO_ONDO_KB                                                                          " & vbNewLine _
                                         & ",H01_01.UNSO_CD             AS      UNSO_CD                                                                               " & vbNewLine _
                                         & ",H01_01.UNSO_BR_CD          AS      UNSO_BR_CD                                                                            " & vbNewLine _
                                         & ",M38_01.UNSOCO_NM           AS      UNSOCO_NM                                                                             " & vbNewLine _
                                         & ",M38_01.UNSOCO_BR_NM        AS      UNSOCO_BR_NM                                                                          " & vbNewLine _
                                         & ",H01_01.UNCHIN              AS      UNCHIN                                                                                " & vbNewLine _
                                         & ",H01_01.YOKO_TARIFF_CD      AS      YOKO_TARIFF_CD                                                                        " & vbNewLine _
                                         & ",CASE WHEN H01_01.UNCHIN_KB = '40'                                                                                        " & vbNewLine _
                                         & "      THEN M49_01.YOKO_REM                                                                                                " & vbNewLine _
                                         & "      ELSE M47_01.UNCHIN_TARIFF_REM                                                                                       " & vbNewLine _
                                         & " END                        AS      TARIFF_REM                                                                            " & vbNewLine _
                                         & ",H01_01.OUT_FLAG            AS      OUT_FLAG                                                                              " & vbNewLine _
                                         & ",H01_01.AKAKURO_KB          AS      AKAKURO_KB                                                                            " & vbNewLine _
                                         & ",''                         AS      JISSEKI_FLAG                                                                          " & vbNewLine _
                                         & ",''                         AS      JISSEKI_USER                                                                          " & vbNewLine _
                                         & ",''                         AS      JISSEKI_DATE                                                                          " & vbNewLine _
                                         & ",''                         AS      JISSEKI_TIME                                                                          " & vbNewLine _
                                         & ",H01_01.FREE_N01            AS      FREE_N01                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N02            AS      FREE_N02                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N03            AS      FREE_N03                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N04            AS      FREE_N04                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N05            AS      FREE_N05                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N06            AS      FREE_N06                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N07            AS      FREE_N07                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N08            AS      FREE_N08                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N09            AS      FREE_N09                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_N10            AS      FREE_N10                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C01            AS      FREE_C01                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C02            AS      FREE_C02                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C03            AS      FREE_C03                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C04            AS      FREE_C04                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C05            AS      FREE_C05                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C06            AS      FREE_C06                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C07            AS      FREE_C07                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C08            AS      FREE_C08                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C09            AS      FREE_C09                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C10            AS      FREE_C10                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C11            AS      FREE_C11                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C12            AS      FREE_C12                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C13            AS      FREE_C13                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C14            AS      FREE_C14                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C15            AS      FREE_C15                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C16            AS      FREE_C16                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C17            AS      FREE_C17                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C18            AS      FREE_C18                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C19            AS      FREE_C19                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C20            AS      FREE_C20                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C21            AS      FREE_C21                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C22            AS      FREE_C22                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C23            AS      FREE_C23                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C24            AS      FREE_C24                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C25            AS      FREE_C25                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C26            AS      FREE_C26                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C27            AS      FREE_C27                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C28            AS      FREE_C28                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C29            AS      FREE_C29                                                                              " & vbNewLine _
                                         & ",H01_01.FREE_C30            AS      FREE_C30                                                                              " & vbNewLine _
                                         & ",''                         AS      CRT_USER                                                                              " & vbNewLine _
                                         & ",''                         AS      CRT_DATE                                                                              " & vbNewLine _
                                         & ",''                         AS      CRT_TIME                                                                              " & vbNewLine _
                                         & ",''                         AS      UPD_USER                                                                              " & vbNewLine _
                                         & ",''                         AS      UPD_DATE                                                                              " & vbNewLine _
                                         & ",''                         AS      UPD_TIME                                                                              " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_DATE                                                                          " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_TIME                                                                          " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_PGID                                                                          " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_USER                                                                          " & vbNewLine _
                                         & ",H01_01.SYS_UPD_DATE        AS      SYS_UPD_DATE                                                                          " & vbNewLine _
                                         & ",H01_01.SYS_UPD_TIME        AS      SYS_UPD_TIME                                                                          " & vbNewLine _
                                         & ",''                         AS      SYS_UPD_PGID                                                                          " & vbNewLine _
                                         & ",''                         AS      SYS_UPD_USER                                                                          " & vbNewLine _
                                         & ",''                         AS      SYS_DEL_FLG                                                                           " & vbNewLine _
                                         & ",CASE WHEN     H01_01.DEL_KB    = '1'                                                                                     " & vbNewLine _
                                         & "      THEN     '01'                                                                                                       " & vbNewLine _
                                         & "      WHEN     H01_01.DEL_KB    = '2'                                                                                     " & vbNewLine _
                                         & "      THEN     '02'                                                                                                       " & vbNewLine _
                                         & "      WHEN     H01_01.DEL_KB    = '3'                                                                                     " & vbNewLine _
                                         & "      THEN     '05'                                                                                                       " & vbNewLine _
                                         & "      WHEN     H01_01.DEL_KB    = '0'                                                                                     " & vbNewLine _
                                         & "           AND @MATOME_FLG     <> '0'                                                                                     " & vbNewLine _
                                         & "      THEN     '04'                                                                                                       " & vbNewLine _
                                         & "      WHEN     H01_01.DEL_KB    = '0'                                                                                     " & vbNewLine _
                                         & "           AND H01_01.OUT_FLAG  = '1'                                                                                     " & vbNewLine _
                                         & "      THEN     '03'                                                                                                       " & vbNewLine _
                                         & "      WHEN     H01_01.DEL_KB    = '0'                                                                                     " & vbNewLine _
                                         & "           AND H01_01.OUT_FLAG <> '1'                                                                                     " & vbNewLine _
                                         & "           AND @MATOME_FLG      = '0'                                                                                     " & vbNewLine _
                                         & "      THEN     '00'                                                                                                       " & vbNewLine _
                                         & " END                        AS      EDI_STATE_KB                                                                          " & vbNewLine _
                                         & ",HED_01.SYS_UPD_DATE        AS      RCV_UPD_DATE                                                                          " & vbNewLine _
                                         & ",HED_01.SYS_UPD_TIME        AS      RCV_UPD_TIME                                                                          " & vbNewLine _
                                         & ",@MATOME_FLG                AS      MATOME_FLG                                                                            " & vbNewLine _
                                         & " FROM       $LM_TRN$..H_INKAEDI_L      H01_01                                                                             " & vbNewLine _
                                         & " LEFT JOIN (                                                                                                              " & vbNewLine _
                                         & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                          " & vbNewLine _
                                         & "                      ,M47_02.EDI_CTL_NO            AS EDI_CTL_NO                                                         " & vbNewLine _
                                         & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                  " & vbNewLine _
                                         & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                   " & vbNewLine _
                                         & "                 INNER JOIN (                                                                                             " & vbNewLine _
                                         & "                                  SELECT H01_01.NRS_BR_CD             AS NRS_BR_CD                                        " & vbNewLine _
                                         & "                                        ,H01_01.EDI_CTL_NO            AS EDI_CTL_NO                                       " & vbNewLine _
                                         & "                                        ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                                 " & vbNewLine _
                                         & "                                        ,M47_01.UNCHIN_TARIFF_CD_EDA  AS UNCHIN_TARIFF_CD_EDA                             " & vbNewLine _
                                         & "                                        ,MAX(M47_01.STR_DATE)         AS STR_DATE                                         " & vbNewLine _
                                         & "                                    FROM      $LM_TRN$..H_INKAEDI_L H01_01                                                " & vbNewLine _
                                         & "                                    LEFT JOIN (                                                                           " & vbNewLine _
                                         & "                                                           SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD           " & vbNewLine _
                                         & "                                                                 ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD    " & vbNewLine _
                                         & "                                                                 ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA" & vbNewLine _
                                         & "                                                                 ,M47_01.STR_DATE                  AS STR_DATE            " & vbNewLine _
                                         & "                                                             FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                        " & vbNewLine _
                                         & "                                                            --WHERE M47_01.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                         & "                                                         GROUP BY M47_01.NRS_BR_CD                                        " & vbNewLine _
                                         & "                                                                 ,M47_01.UNCHIN_TARIFF_CD                                 " & vbNewLine _
                                         & "                                                                 ,M47_01.STR_DATE                                         " & vbNewLine _
                                         & "                                              )                   M47_01                                                  " & vbNewLine _
                                         & "                                      ON H01_01.NRS_BR_CD       = M47_01.NRS_BR_CD                                        " & vbNewLine _
                                         & "                                     AND H01_01.YOKO_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                                 " & vbNewLine _
                                         & "                                     AND H01_01.INKA_DATE      >= M47_01.STR_DATE                                         " & vbNewLine _
                                         & "                                   WHERE H01_01.SYS_DEL_FLG     = '0'                                                     " & vbNewLine _
                                         & "                                GROUP BY H01_01.NRS_BR_CD                                                                 " & vbNewLine _
                                         & "                                        ,H01_01.EDI_CTL_NO                                                                " & vbNewLine _
                                         & "                                        ,M47_01.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
                                         & "                                        ,M47_01.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
                                         & "                            ) M47_02                                                                                      " & vbNewLine _
                                         & "                         ON   M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                              " & vbNewLine _
                                         & "                        AND   M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                       " & vbNewLine _
                                         & "                        AND   M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                   " & vbNewLine _
                                         & "                        AND   M47_01.STR_DATE             = M47_02.STR_DATE                                               " & vbNewLine _
                                         & "          )                           M47_01                                                                              " & vbNewLine _
                                         & "   ON H01_01.NRS_BR_CD              = M47_01.NRS_BR_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.EDI_CTL_NO             = M47_01.EDI_CTL_NO                                                                   " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_CUST           M07_01                                                                              " & vbNewLine _
                                         & "   ON H01_01.NRS_BR_CD              = M07_01.NRS_BR_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.CUST_CD_L              = M07_01.CUST_CD_L                                                                    " & vbNewLine _
                                         & "  AND H01_01.CUST_CD_M              = M07_01.CUST_CD_M                                                                    " & vbNewLine _
                                         & "  AND M07_01.CUST_CD_S              = '00'                                                                                " & vbNewLine _
                                         & "  AND M07_01.CUST_CD_SS             = '00'                                                                                " & vbNewLine _
                                         & "  --AND M07_01.SYS_DEL_FLG            = '0'                                                                                 " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_DEST           M10_01                                                                              " & vbNewLine _
                                         & "   ON H01_01.NRS_BR_CD              = M10_01.NRS_BR_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.CUST_CD_L              = M10_01.CUST_CD_L                                                                    " & vbNewLine _
                                         & "  AND H01_01.OUTKA_MOTO             = M10_01.DEST_CD                                                                      " & vbNewLine _
                                         & "  --AND M10_01.SYS_DEL_FLG            = '0'                                                                                 " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_UNSOCO         M38_01                                                                              " & vbNewLine _
                                         & "   ON H01_01.NRS_BR_CD              = M38_01.NRS_BR_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.UNSO_CD                = M38_01.UNSOCO_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.UNSO_BR_CD             = M38_01.UNSOCO_BR_CD                                                                 " & vbNewLine _
                                         & "  --AND M38_01.SYS_DEL_FLG            = '0'                                                                                 " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_YOKO_TARIFF_HD M49_01                                                                              " & vbNewLine _
                                         & "   ON H01_01.NRS_BR_CD              = M49_01.NRS_BR_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.YOKO_TARIFF_CD         = M49_01.YOKO_TARIFF_CD                                                               " & vbNewLine _
                                         & "  --AND M49_01.SYS_DEL_FLG            = '0'                                                                                 " & vbNewLine



    Private Const SQL_FROM_RCV_HED As String = " LEFT JOIN $LM_TRN$..$HED_TBL$        HED_01                                                                              " & vbNewLine _
                                         & "   ON H01_01.NRS_BR_CD              = HED_01.NRS_BR_CD                                                                    " & vbNewLine _
                                         & "  AND H01_01.EDI_CTL_NO             = HED_01.EDI_CTL_NO                                                                   " & vbNewLine _
                                         & "  --AND HED_01.SYS_DEL_FLG            = '0'                                                                                 " & vbNewLine

    Private Const SQL_FROM_RCV_HED_NULL As String = "LEFT JOIN                                                                                                        " & vbNewLine _
                                          & "(                                                                                                                        " & vbNewLine _
                                          & "SELECT                                                                                                                   " & vbNewLine _
                                          & " H_INKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                                                                                    " & vbNewLine _
                                          & ",'' AS SYS_UPD_DATE                                                                                                      " & vbNewLine _
                                          & ",'' AS SYS_UPD_TIME                                                                                                      " & vbNewLine _
                                          & "FROM                                                                                                                     " & vbNewLine _
                                          & "$LM_TRN$..H_INKAEDI_L                 H_INKAEDI_L                                                                        " & vbNewLine _
                                          & "WHERE                                                                                                                    " & vbNewLine _
                                          & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                          & ")                                     HED_01                                                                      " & vbNewLine _
                                          & "ON                                                                                                                       " & vbNewLine _
                                          & "H01_01.EDI_CTL_NO = HED_01.EDI_CTL_NO                                                                        " & vbNewLine


    Private Const SQL_SELECT_L_WHERE As String = "WHERE H01_01.NRS_BR_CD              = @NRS_BR_CD                                                                          " & vbNewLine _
                                         & "  AND H01_01.EDI_CTL_NO             = @EDI_CTL_NO                                                                         " & vbNewLine _
                                         & "  --AND H01_01.SYS_DEL_FLG            = '0'                                                                                 " & vbNewLine


#End Region

#Region "H_INKAEDI_M"

    Private Const SQL_SELECT_M As String = "SELECT                                                                                                 " & vbNewLine _
                                         & " H02_01.DEL_KB              AS      DEL_KB                  " & vbNewLine _
                                         & ",H02_01.NRS_BR_CD           AS      NRS_BR_CD               " & vbNewLine _
                                         & ",H02_01.EDI_CTL_NO          AS      EDI_CTL_NO              " & vbNewLine _
                                         & ",H02_01.EDI_CTL_NO_CHU      AS      EDI_CTL_NO_CHU          " & vbNewLine _
                                         & ",H02_01.INKA_CTL_NO_L       AS      INKA_CTL_NO_L           " & vbNewLine _
                                         & ",H02_01.INKA_CTL_NO_M       AS      INKA_CTL_NO_M           " & vbNewLine _
                                         & ",H02_01.NRS_GOODS_CD        AS      NRS_GOODS_CD            " & vbNewLine _
                                         & ",H02_01.CUST_GOODS_CD       AS      CUST_GOODS_CD           " & vbNewLine _
                                         & ",H02_01.GOODS_NM            AS      GOODS_NM                " & vbNewLine _
                                         & ",H02_01.NB                  AS      NB                      " & vbNewLine _
                                         & ",H02_01.NB_UT               AS      NB_UT                   " & vbNewLine _
                                         & "--,KBN_01.KBN_NM1             AS      PKG_UT_NM               " & vbNewLine _
                                         & ",H02_01.PKG_NB              AS      PKG_NB                  " & vbNewLine _
                                         & "--,KBN_02.KBN_NM1             AS      NB_UT_NM                " & vbNewLine _
                                         & ",H02_01.PKG_UT              AS      PKG_UT                  " & vbNewLine _
                                         & ",H02_01.INKA_PKG_NB         AS      INKA_PKG_NB             " & vbNewLine _
                                         & ",H02_01.HASU                AS      HASU                    " & vbNewLine _
                                         & ",H02_01.STD_IRIME           AS      STD_IRIME               " & vbNewLine _
                                         & ",H02_01.STD_IRIME_UT        AS      STD_IRIME_UT            " & vbNewLine _
                                         & "--,KBN_03.KBN_NM1             AS      STD_IRIME_UT_NM         " & vbNewLine _
                                         & ",H02_01.BETU_WT             AS      BETU_WT                 " & vbNewLine _
                                         & ",H02_01.CBM                 AS      CBM                     " & vbNewLine _
                                         & ",H02_01.ONDO_KB             AS      ONDO_KB                 " & vbNewLine _
                                         & ",H02_01.OUTKA_FROM_ORD_NO   AS      OUTKA_FROM_ORD_NO       " & vbNewLine _
                                         & ",H02_01.BUYER_ORD_NO        AS      BUYER_ORD_NO            " & vbNewLine _
                                         & ",H02_01.REMARK              AS      REMARK                  " & vbNewLine _
                                         & ",H02_01.LOT_NO              AS      LOT_NO                  " & vbNewLine _
                                         & ",H02_01.SERIAL_NO           AS      SERIAL_NO               " & vbNewLine _
                                         & ",H02_01.IRIME               AS      IRIME                   " & vbNewLine _
                                         & ",H02_01.IRIME_UT            AS      IRIME_UT                " & vbNewLine _
                                         & "--,KBN_04.KBN_NM1             AS      IRIME_UT_NM             " & vbNewLine _
                                         & ",H02_01.OUT_KB              AS      OUT_KB                  " & vbNewLine _
                                         & ",H02_01.AKAKURO_KB          AS      AKAKURO_KB              " & vbNewLine _
                                         & ",''                         AS      JISSEKI_FLAG            " & vbNewLine _
                                         & ",''                         AS      JISSEKI_USER            " & vbNewLine _
                                         & ",''                         AS      JISSEKI_DATE            " & vbNewLine _
                                         & ",''                         AS      JISSEKI_TIME            " & vbNewLine _
                                         & ",H02_01.FREE_N01            AS      FREE_N01                " & vbNewLine _
                                         & ",H02_01.FREE_N02            AS      FREE_N02                " & vbNewLine _
                                         & ",H02_01.FREE_N03            AS      FREE_N03                " & vbNewLine _
                                         & ",H02_01.FREE_N04            AS      FREE_N04                " & vbNewLine _
                                         & ",H02_01.FREE_N05            AS      FREE_N05                " & vbNewLine _
                                         & ",H02_01.FREE_N06            AS      FREE_N06                " & vbNewLine _
                                         & ",H02_01.FREE_N07            AS      FREE_N07                " & vbNewLine _
                                         & ",H02_01.FREE_N08            AS      FREE_N08                " & vbNewLine _
                                         & ",H02_01.FREE_N09            AS      FREE_N09                " & vbNewLine _
                                         & ",H02_01.FREE_N10            AS      FREE_N10                " & vbNewLine _
                                         & ",H02_01.FREE_C01            AS      FREE_C01                " & vbNewLine _
                                         & ",H02_01.FREE_C02            AS      FREE_C02                " & vbNewLine _
                                         & ",H02_01.FREE_C03            AS      FREE_C03                " & vbNewLine _
                                         & ",H02_01.FREE_C04            AS      FREE_C04                " & vbNewLine _
                                         & ",H02_01.FREE_C05            AS      FREE_C05                " & vbNewLine _
                                         & ",H02_01.FREE_C06            AS      FREE_C06                " & vbNewLine _
                                         & ",H02_01.FREE_C07            AS      FREE_C07                " & vbNewLine _
                                         & ",H02_01.FREE_C08            AS      FREE_C08                " & vbNewLine _
                                         & ",H02_01.FREE_C09            AS      FREE_C09                " & vbNewLine _
                                         & ",H02_01.FREE_C10            AS      FREE_C10                " & vbNewLine _
                                         & ",H02_01.FREE_C11            AS      FREE_C11                " & vbNewLine _
                                         & ",H02_01.FREE_C12            AS      FREE_C12                " & vbNewLine _
                                         & ",H02_01.FREE_C13            AS      FREE_C13                " & vbNewLine _
                                         & ",H02_01.FREE_C14            AS      FREE_C14                " & vbNewLine _
                                         & ",H02_01.FREE_C15            AS      FREE_C15                " & vbNewLine _
                                         & ",H02_01.FREE_C16            AS      FREE_C16                " & vbNewLine _
                                         & ",H02_01.FREE_C17            AS      FREE_C17                " & vbNewLine _
                                         & ",H02_01.FREE_C18            AS      FREE_C18                " & vbNewLine _
                                         & ",H02_01.FREE_C19            AS      FREE_C19                " & vbNewLine _
                                         & ",H02_01.FREE_C20            AS      FREE_C20                " & vbNewLine _
                                         & ",H02_01.FREE_C21            AS      FREE_C21                " & vbNewLine _
                                         & ",H02_01.FREE_C22            AS      FREE_C22                " & vbNewLine _
                                         & ",H02_01.FREE_C23            AS      FREE_C23                " & vbNewLine _
                                         & ",H02_01.FREE_C24            AS      FREE_C24                " & vbNewLine _
                                         & ",H02_01.FREE_C25            AS      FREE_C25                " & vbNewLine _
                                         & ",H02_01.FREE_C26            AS      FREE_C26                " & vbNewLine _
                                         & ",H02_01.FREE_C27            AS      FREE_C27                " & vbNewLine _
                                         & ",H02_01.FREE_C28            AS      FREE_C28                " & vbNewLine _
                                         & ",H02_01.FREE_C29            AS      FREE_C29                " & vbNewLine _
                                         & ",H02_01.FREE_C30            AS      FREE_C30                " & vbNewLine _
                                         & ",H02_01.CRT_USER            AS      CRT_USER                " & vbNewLine _
                                         & ",H02_01.CRT_DATE            AS      CRT_DATE                " & vbNewLine _
                                         & ",''                         AS      CRT_TIME                " & vbNewLine _
                                         & ",''                         AS      UPD_USER                " & vbNewLine _
                                         & ",''                         AS      UPD_DATE                " & vbNewLine _
                                         & ",''                         AS      UPD_TIME                " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_DATE            " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_TIME            " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_PGID            " & vbNewLine _
                                         & ",''                         AS      SYS_ENT_USER            " & vbNewLine _
                                         & ",''                         AS      SYS_UPD_DATE            " & vbNewLine _
                                         & ",''                         AS      SYS_UPD_TIME            " & vbNewLine _
                                         & ",''                         AS      SYS_UPD_PGID            " & vbNewLine _
                                         & ",''                         AS      SYS_UPD_USER            " & vbNewLine _
                                         & ",H02_01.SYS_DEL_FLG         AS      SYS_DEL_FLG             " & vbNewLine _
                                         & ",H02_01.NB * H02_01.IRIME   AS      SURYO                   " & vbNewLine _
                                         & ",H02_01.SYS_DEL_FLG         AS      JYOTAI                  " & vbNewLine _
                                         & ",KBN_05.KBN_NM1             AS      JYOTAI_NM               " & vbNewLine _
                                         & " FROM      $LM_TRN$..H_INKAEDI_M H02_01                     " & vbNewLine _
                                         & " --LEFT JOIN $LM_MST$..Z_KBN       KBN_01                   " & vbNewLine _
                                         & " --ON H02_01.NB_UT             = KBN_01.KBN_CD              " & vbNewLine _
                                         & " --AND KBN_01.KBN_GROUP_CD      = 'K002'                    " & vbNewLine _
                                         & "  --AND KBN_01.SYS_DEL_FLG       = '0'                      " & vbNewLine _
                                         & " --LEFT JOIN $LM_MST$..Z_KBN       KBN_02                   " & vbNewLine _
                                         & "   --ON H02_01.PKG_UT            = KBN_02.KBN_CD            " & vbNewLine _
                                         & "  --AND KBN_02.KBN_GROUP_CD      = 'N001'                   " & vbNewLine _
                                         & "  --AND KBN_02.SYS_DEL_FLG       = '0'                      " & vbNewLine _
                                         & " --LEFT JOIN $LM_MST$..Z_KBN       KBN_03                   " & vbNewLine _
                                         & "   --ON H02_01.STD_IRIME_UT      = KBN_03.KBN_CD            " & vbNewLine _
                                         & "  --AND KBN_03.KBN_GROUP_CD      = 'I001'                   " & vbNewLine _
                                         & "  --AND KBN_03.SYS_DEL_FLG       = '0'                      " & vbNewLine _
                                         & " --LEFT JOIN $LM_MST$..Z_KBN       KBN_04                   " & vbNewLine _
                                         & "   --ON H02_01.IRIME_UT          = KBN_04.KBN_CD            " & vbNewLine _
                                         & "  --AND KBN_04.KBN_GROUP_CD      = 'I001'                   " & vbNewLine _
                                         & "  --AND KBN_04.SYS_DEL_FLG       = '0'                      " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..Z_KBN       KBN_05                     " & vbNewLine _
                                         & "   ON H02_01.SYS_DEL_FLG       = KBN_05.KBN_CD              " & vbNewLine _
                                         & "  AND KBN_05.KBN_GROUP_CD      = 'S051'                     " & vbNewLine _
                                         & "  --AND KBN_05.SYS_DEL_FLG       = '0'                        " & vbNewLine _
                                         & "WHERE H02_01.NRS_BR_CD         = @NRS_BR_CD                 " & vbNewLine _
                                         & "  AND H02_01.EDI_CTL_NO        = @EDI_CTL_NO                " & vbNewLine


#End Region

#Region "M_CUST"

    Private Const SQL_SELECT_CUST As String = "SELECT CUST.HOKAN_SEIQTO_CD  AS HOKAN_SEIQTO_CD " & vbNewLine _
                                            & "      ,CUST.NIYAKU_SEIQTO_CD AS NIYAKU_SEIQTO_CD" & vbNewLine _
                                            & "      ,CUST.UNCHIN_SEIQTO_CD AS UNCHIN_SEIQTO_CD" & vbNewLine _
                                            & "      ,CUST.SAGYO_SEIQTO_CD  AS SAGYO_SEIQTO_CD " & vbNewLine _
                                            & "FROM       $LM_MST$..M_CUST CUST                " & vbNewLine _
                                            & "WHERE  CUST.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_L   = @CUST_CD_L            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_M   = @CUST_CD_M            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_S   = '00'                  " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_SS  = '00'                  " & vbNewLine _
                                            & "  --AND  CUST.SYS_DEL_FLG = '0'                   " & vbNewLine


#End Region

#Region "チェック"

#Region "COUNT_L"

    Private Const SQL_SELECT_COUNT As String = "SELECT COUNT(NRS_BR_CD) AS REC_CNT " & vbNewLine _
                                             & "FROM $LM_TRN$..H_INKAEDI_L        " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                             & "  AND EDI_CTL_NO   = @EDI_CTL_NO  " & vbNewLine _
                                             & "  AND SYS_DEL_FLG  = '0'          " & vbNewLine _
                                             & "  AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                             & "  AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

#End Region

#End Region

#End Region

#Region "設定処理 SQL"

    '2012.03.15 要望番号895 大阪対応START
#Region "H_INKAEDI_L"

    Private Const SQL_UPDATE_L As String = "UPDATE $LM_TRN$..H_INKAEDI_L SET                    " & vbNewLine _
                                         & " INKA_TP                 = @INKA_TP                 " & vbNewLine _
                                         & ",INKA_KB                 = @INKA_KB                 " & vbNewLine _
                                         & ",INKA_DATE               = @INKA_DATE               " & vbNewLine _
                                         & ",INKA_PLAN_QT            = @INKA_PLAN_QT            " & vbNewLine _
                                         & ",INKA_PLAN_QT_UT         = @INKA_PLAN_QT_UT         " & vbNewLine _
                                         & ",INKA_TTL_NB             = @INKA_TTL_NB             " & vbNewLine _
                                         & ",BUYER_ORD_NO            = @BUYER_ORD_NO            " & vbNewLine _
                                         & ",OUTKA_FROM_ORD_NO       = @OUTKA_FROM_ORD_NO       " & vbNewLine _
                                         & ",TOUKI_HOKAN_YN          = @TOUKI_HOKAN_YN          " & vbNewLine _
                                         & ",HOKAN_YN                = @HOKAN_YN                " & vbNewLine _
                                         & ",HOKAN_FREE_KIKAN        = @HOKAN_FREE_KIKAN        " & vbNewLine _
                                         & ",HOKAN_STR_DATE          = @HOKAN_STR_DATE          " & vbNewLine _
                                         & ",NIYAKU_YN               = @NIYAKU_YN               " & vbNewLine _
                                         & ",TAX_KB                  = @TAX_KB                  " & vbNewLine _
                                         & ",REMARK                  = @REMARK                  " & vbNewLine _
                                         & ",NYUBAN_L                = @NYUBAN_L                " & vbNewLine _
                                         & ",UNCHIN_TP               = @UNCHIN_TP               " & vbNewLine _
                                         & ",UNCHIN_KB               = @UNCHIN_KB               " & vbNewLine _
                                         & ",OUTKA_MOTO              = @OUTKA_MOTO              " & vbNewLine _
                                         & ",SYARYO_KB               = @SYARYO_KB               " & vbNewLine _
                                         & ",UNSO_ONDO_KB            = @UNSO_ONDO_KB            " & vbNewLine _
                                         & ",UNSO_CD                 = @UNSO_CD                 " & vbNewLine _
                                         & ",UNSO_BR_CD              = @UNSO_BR_CD              " & vbNewLine _
                                         & ",UNCHIN                  = @UNCHIN                  " & vbNewLine _
                                         & ",YOKO_TARIFF_CD          = @YOKO_TARIFF_CD          " & vbNewLine _
                                         & ",OUT_FLAG                = @OUT_FLAG                " & vbNewLine _
                                         & ",AKAKURO_KB              = @AKAKURO_KB              " & vbNewLine _
                                         & ",FREE_N01                = @FREE_N01                " & vbNewLine _
                                         & ",FREE_N02                = @FREE_N02                " & vbNewLine _
                                         & ",FREE_N03                = @FREE_N03                " & vbNewLine _
                                         & ",FREE_N04                = @FREE_N04                " & vbNewLine _
                                         & ",FREE_N05                = @FREE_N05                " & vbNewLine _
                                         & ",FREE_N06                = @FREE_N06                " & vbNewLine _
                                         & ",FREE_N07                = @FREE_N07                " & vbNewLine _
                                         & ",FREE_N08                = @FREE_N08                " & vbNewLine _
                                         & ",FREE_N09                = @FREE_N09                " & vbNewLine _
                                         & ",FREE_N10                = @FREE_N10                " & vbNewLine _
                                         & ",FREE_C01                = @FREE_C01                " & vbNewLine _
                                         & ",FREE_C02                = @FREE_C02                " & vbNewLine _
                                         & ",FREE_C03                = @FREE_C03                " & vbNewLine _
                                         & ",FREE_C04                = @FREE_C04                " & vbNewLine _
                                         & ",FREE_C05                = @FREE_C05                " & vbNewLine _
                                         & ",FREE_C06                = @FREE_C06                " & vbNewLine _
                                         & ",FREE_C07                = @FREE_C07                " & vbNewLine _
                                         & ",FREE_C08                = @FREE_C08                " & vbNewLine _
                                         & ",FREE_C09                = @FREE_C09                " & vbNewLine _
                                         & ",FREE_C10                = @FREE_C10                " & vbNewLine _
                                         & ",FREE_C11                = @FREE_C11                " & vbNewLine _
                                         & ",FREE_C12                = @FREE_C12                " & vbNewLine _
                                         & ",FREE_C13                = @FREE_C13                " & vbNewLine _
                                         & ",FREE_C14                = @FREE_C14                " & vbNewLine _
                                         & ",FREE_C15                = @FREE_C15                " & vbNewLine _
                                         & ",FREE_C16                = @FREE_C16                " & vbNewLine _
                                         & ",FREE_C17                = @FREE_C17                " & vbNewLine _
                                         & ",FREE_C18                = @FREE_C18                " & vbNewLine _
                                         & ",FREE_C19                = @FREE_C19                " & vbNewLine _
                                         & ",FREE_C20                = @FREE_C20                " & vbNewLine _
                                         & ",FREE_C21                = @FREE_C21                " & vbNewLine _
                                         & ",FREE_C22                = @FREE_C22                " & vbNewLine _
                                         & ",FREE_C23                = @FREE_C23                " & vbNewLine _
                                         & ",FREE_C24                = @FREE_C24                " & vbNewLine _
                                         & ",FREE_C25                = @FREE_C25                " & vbNewLine _
                                         & ",FREE_C26                = @FREE_C26                " & vbNewLine _
                                         & ",FREE_C27                = @FREE_C27                " & vbNewLine _
                                         & ",FREE_C28                = @FREE_C28                " & vbNewLine _
                                         & ",FREE_C29                = @FREE_C29                " & vbNewLine _
                                         & ",FREE_C30                = @FREE_C30                " & vbNewLine _
                                         & ",UPD_USER                = @SYS_UPD_USER            " & vbNewLine _
                                         & ",UPD_DATE                = @SYS_UPD_DATE            " & vbNewLine _
                                         & ",UPD_TIME                = @UPD_TIME                " & vbNewLine _
                                         & ",SYS_UPD_DATE            = @SYS_UPD_DATE            " & vbNewLine _
                                         & ",SYS_UPD_TIME            = @SYS_UPD_TIME            " & vbNewLine _
                                         & ",SYS_UPD_PGID            = @SYS_UPD_PGID            " & vbNewLine _
                                         & ",SYS_UPD_USER            = @SYS_UPD_USER            " & vbNewLine _
                                         & ",DEL_KB                  = @DEL_KB                  " & vbNewLine _
                                         & "WHERE NRS_BR_CD          = @NRS_BR_CD               " & vbNewLine _
                                         & "  AND EDI_CTL_NO         = @EDI_CTL_NO              " & vbNewLine _
                                         & "  AND SYS_UPD_DATE       = @GUI_UPD_DATE            " & vbNewLine _
                                         & "  AND SYS_UPD_TIME       = @GUI_UPD_TIME            " & vbNewLine

#End Region
    '2012.03.15 要望番号895 大阪対応END

    '2012.03.15 要望番号895 大阪対応START
#Region "H_INKAEDI_M"

    Private Const SQL_UPDATE_M As String = "UPDATE $LM_TRN$..H_INKAEDI_M SET         " & vbNewLine _
                                         & " NRS_GOODS_CD        = @NRS_GOODS_CD     " & vbNewLine _
                                         & ",CUST_GOODS_CD       = @CUST_GOODS_CD    " & vbNewLine _
                                         & ",GOODS_NM            = @GOODS_NM         " & vbNewLine _
                                         & ",NB                  = @NB               " & vbNewLine _
                                         & ",NB_UT               = @NB_UT            " & vbNewLine _
                                         & ",PKG_NB              = @PKG_NB           " & vbNewLine _
                                         & ",PKG_UT              = @PKG_UT           " & vbNewLine _
                                         & ",INKA_PKG_NB         = @INKA_PKG_NB      " & vbNewLine _
                                         & ",HASU                = @HASU             " & vbNewLine _
                                         & ",STD_IRIME           = @STD_IRIME        " & vbNewLine _
                                         & ",STD_IRIME_UT        = @STD_IRIME_UT     " & vbNewLine _
                                         & ",BETU_WT             = @BETU_WT          " & vbNewLine _
                                         & ",ONDO_KB             = @ONDO_KB          " & vbNewLine _
                                         & ",OUTKA_FROM_ORD_NO   = @OUTKA_FROM_ORD_NO" & vbNewLine _
                                         & ",BUYER_ORD_NO        = @BUYER_ORD_NO     " & vbNewLine _
                                         & ",REMARK              = @REMARK           " & vbNewLine _
                                         & ",LOT_NO              = @LOT_NO           " & vbNewLine _
                                         & ",SERIAL_NO           = @SERIAL_NO        " & vbNewLine _
                                         & ",IRIME               = @IRIME            " & vbNewLine _
                                         & ",IRIME_UT            = @IRIME_UT         " & vbNewLine _
                                         & ",FREE_N01            = @FREE_N01         " & vbNewLine _
                                         & ",FREE_N02            = @FREE_N02         " & vbNewLine _
                                         & ",FREE_N03            = @FREE_N03         " & vbNewLine _
                                         & ",FREE_N04            = @FREE_N04         " & vbNewLine _
                                         & ",FREE_N05            = @FREE_N05         " & vbNewLine _
                                         & ",FREE_N06            = @FREE_N06         " & vbNewLine _
                                         & ",FREE_N07            = @FREE_N07         " & vbNewLine _
                                         & ",FREE_N08            = @FREE_N08         " & vbNewLine _
                                         & ",FREE_N09            = @FREE_N09         " & vbNewLine _
                                         & ",FREE_N10            = @FREE_N10         " & vbNewLine _
                                         & ",FREE_C01            = @FREE_C01         " & vbNewLine _
                                         & ",FREE_C02            = @FREE_C02         " & vbNewLine _
                                         & ",FREE_C03            = @FREE_C03         " & vbNewLine _
                                         & ",FREE_C04            = @FREE_C04         " & vbNewLine _
                                         & ",FREE_C05            = @FREE_C05         " & vbNewLine _
                                         & ",FREE_C06            = @FREE_C06         " & vbNewLine _
                                         & ",FREE_C07            = @FREE_C07         " & vbNewLine _
                                         & ",FREE_C08            = @FREE_C08         " & vbNewLine _
                                         & ",FREE_C09            = @FREE_C09         " & vbNewLine _
                                         & ",FREE_C10            = @FREE_C10         " & vbNewLine _
                                         & ",FREE_C11            = @FREE_C11         " & vbNewLine _
                                         & ",FREE_C12            = @FREE_C12         " & vbNewLine _
                                         & ",FREE_C13            = @FREE_C13         " & vbNewLine _
                                         & ",FREE_C14            = @FREE_C14         " & vbNewLine _
                                         & ",FREE_C15            = @FREE_C15         " & vbNewLine _
                                         & ",FREE_C16            = @FREE_C16         " & vbNewLine _
                                         & ",FREE_C17            = @FREE_C17         " & vbNewLine _
                                         & ",FREE_C18            = @FREE_C18         " & vbNewLine _
                                         & ",FREE_C19            = @FREE_C19         " & vbNewLine _
                                         & ",FREE_C20            = @FREE_C20         " & vbNewLine _
                                         & ",FREE_C21            = @FREE_C21         " & vbNewLine _
                                         & ",FREE_C22            = @FREE_C22         " & vbNewLine _
                                         & ",FREE_C23            = @FREE_C23         " & vbNewLine _
                                         & ",FREE_C24            = @FREE_C24         " & vbNewLine _
                                         & ",FREE_C25            = @FREE_C25         " & vbNewLine _
                                         & ",FREE_C26            = @FREE_C26         " & vbNewLine _
                                         & ",FREE_C27            = @FREE_C27         " & vbNewLine _
                                         & ",FREE_C28            = @FREE_C28         " & vbNewLine _
                                         & ",FREE_C29            = @FREE_C29         " & vbNewLine _
                                         & ",FREE_C30            = @FREE_C30         " & vbNewLine _
                                         & ",UPD_USER            = @SYS_UPD_USER     " & vbNewLine _
                                         & ",UPD_DATE            = @SYS_UPD_DATE     " & vbNewLine _
                                         & ",UPD_TIME            = @UPD_TIME         " & vbNewLine _
                                         & ",SYS_UPD_DATE        = @SYS_UPD_DATE     " & vbNewLine _
                                         & ",SYS_UPD_TIME        = @SYS_UPD_TIME     " & vbNewLine _
                                         & ",SYS_UPD_PGID        = @SYS_UPD_PGID     " & vbNewLine _
                                         & ",SYS_UPD_USER        = @SYS_UPD_USER     " & vbNewLine _
                                         & ",SYS_DEL_FLG         = @SYS_DEL_FLG      " & vbNewLine _
                                         & ",DEL_KB              = @DEL_KB           " & vbNewLine _
                                         & "WHERE NRS_BR_CD      = @NRS_BR_CD        " & vbNewLine _
                                         & "  AND EDI_CTL_NO     = @EDI_CTL_NO       " & vbNewLine _
                                         & "  AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU   " & vbNewLine


#End Region
    '2012.03.15 要望番号895 大阪対応END

#Region "共通"

    Private Const SQL_UPDATE_COM As String = " UPD_USER          = @SYS_UPD_USER    " & vbNewLine _
                                           & ",UPD_DATE          = @SYS_UPD_DATE    " & vbNewLine _
                                           & ",UPD_TIME          = @UPD_TIME        " & vbNewLine _
                                           & ",SYS_UPD_DATE      = @SYS_UPD_DATE    " & vbNewLine _
                                           & ",SYS_UPD_TIME      = @SYS_UPD_TIME    " & vbNewLine _
                                           & ",SYS_UPD_PGID      = @SYS_UPD_PGID    " & vbNewLine _
                                           & ",SYS_UPD_USER      = @SYS_UPD_USER    " & vbNewLine _
                                           & "WHERE NRS_BR_CD    = @NRS_BR_CD       " & vbNewLine _
                                           & "  AND EDI_CTL_NO   = @EDI_CTL_NO      " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow

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

#Region "M_EDI_CUST"

    ''' <summary>
    ''' EDI受信テーブル名を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHedTbl(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"RCV_NM_HED", "RCV_NM_DTL"}

        Return Me.SelectListData(ds, LMH020DAC.TABLE_NM_RCV_NM, LMH020DAC.SQL_SELECT_HED_TBL, LMH020DAC.SelectCondition.PTN2, str)

    End Function

#End Region

#Region "M_FREE_STATE"

    ''' <summary>
    ''' フリー項目名を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectFree(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"DB_COL_NM" _
                                            , "FIELD_NM" _
                                            , "NUM_DIGITS_INT" _
                                            , "NUM_DIGITS_DEC" _
                                            , "INPUT_MANAGE_KB" _
                                            , "ROW_VISIBLE_FLAG" _
                                            , "EDIT_ABLE_FLAG" _
                                            , "SORT_NO" _
                                            , "DATA_KB" _
                                            }

        Return Me.SelectListData(ds, LMH020DAC.TABLE_NM_FREE, LMH020DAC.SQL_SELECT_FREE, LMH020DAC.SelectCondition.PTN3, str)

    End Function

#End Region

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' EDI入荷(大)を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectLData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"DEL_KB" _
                                            , "NRS_BR_CD" _
                                            , "EDI_CTL_NO" _
                                            , "INKA_CTL_NO_L" _
                                            , "INKA_TP" _
                                            , "INKA_KB" _
                                            , "INKA_STATE_KB" _
                                            , "INKA_DATE" _
                                            , "INKA_TIME" _
                                            , "NRS_WH_CD" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "INKA_PLAN_QT" _
                                            , "INKA_PLAN_QT_UT" _
                                            , "INKA_TTL_NB" _
                                            , "NAIGAI_KB" _
                                            , "BUYER_ORD_NO" _
                                            , "OUTKA_FROM_ORD_NO" _
                                            , "TOUKI_HOKAN_YN" _
                                            , "HOKAN_YN" _
                                            , "HOKAN_FREE_KIKAN" _
                                            , "HOKAN_STR_DATE" _
                                            , "NIYAKU_YN" _
                                            , "TAX_KB" _
                                            , "REMARK" _
                                            , "NYUBAN_L" _
                                            , "UNCHIN_TP" _
                                            , "UNCHIN_KB" _
                                            , "OUTKA_MOTO" _
                                            , "OUTKA_MOTO_NM" _
                                            , "SYARYO_KB" _
                                            , "UNSO_ONDO_KB" _
                                            , "UNSO_CD" _
                                            , "UNSO_BR_CD" _
                                            , "UNSOCO_NM" _
                                            , "UNSOCO_BR_NM" _
                                            , "UNCHIN" _
                                            , "YOKO_TARIFF_CD" _
                                            , "TARIFF_REM" _
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
                                            , "SYS_ENT_DATE" _
                                            , "SYS_ENT_TIME" _
                                            , "SYS_ENT_PGID" _
                                            , "SYS_ENT_USER" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_UPD_PGID" _
                                            , "SYS_UPD_USER" _
                                            , "SYS_DEL_FLG" _
                                            , "EDI_STATE_KB" _
                                            , "RCV_UPD_DATE" _
                                            , "RCV_UPD_TIME" _
                                            , "MATOME_FLG" _
                                            }
        '大阪対応　20120322 Start
        Dim sql As String = String.Empty
        Dim rcvHedTbl As String = ds.Tables("RCV_NM").Rows(0)("RCV_NM_HED").ToString() '受信HEDテーブル
        sql = LMH020DAC.SQL_SELECT_L

        '受信Hテーブルの有無でSQLのJOIN条件を替える
        If String.IsNullOrEmpty(rcvHedTbl) = True Then
            sql = String.Concat(sql, LMH020DAC.SQL_FROM_RCV_HED_NULL, LMH020DAC.SQL_SELECT_L_WHERE)
        Else
            sql = String.Concat(sql, LMH020DAC.SQL_FROM_RCV_HED, LMH020DAC.SQL_SELECT_L_WHERE)
        End If

        Return Me.SelectListData(ds, LMH020DAC.TABLE_NM_L, sql, LMH020DAC.SelectCondition.PTN4, str)
        '大阪対応　20120322 End
    End Function

#End Region

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' EDI入荷(中)を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMData(ByVal ds As DataSet) As DataSet

        'Dim str As String() = New String() {"DEL_KB" _
        '                                    , "NRS_BR_CD" _
        '                                    , "EDI_CTL_NO" _
        '                                    , "EDI_CTL_NO_CHU" _
        '                                    , "INKA_CTL_NO_L" _
        '                                    , "INKA_CTL_NO_M" _
        '                                    , "NRS_GOODS_CD" _
        '                                    , "CUST_GOODS_CD" _
        '                                    , "GOODS_NM" _
        '                                    , "NB" _
        '                                    , "NB_UT" _
        '                                    , "PKG_UT_NM" _
        '                                    , "PKG_NB" _
        '                                    , "NB_UT_NM" _
        '                                    , "PKG_UT" _
        '                                    , "INKA_PKG_NB" _
        '                                    , "HASU" _
        '                                    , "STD_IRIME" _
        '                                    , "STD_IRIME_UT" _
        '                                    , "STD_IRIME_UT_NM" _
        '                                    , "BETU_WT" _
        '                                    , "CBM" _
        '                                    , "ONDO_KB" _
        '                                    , "OUTKA_FROM_ORD_NO" _
        '                                    , "BUYER_ORD_NO" _
        '                                    , "REMARK" _
        '                                    , "LOT_NO" _
        '                                    , "SERIAL_NO" _
        '                                    , "IRIME" _
        '                                    , "IRIME_UT" _
        '                                    , "IRIME_UT_NM" _
        '                                    , "OUT_KB" _
        '                                    , "AKAKURO_KB" _
        '                                    , "JISSEKI_FLAG" _
        '                                    , "JISSEKI_USER" _
        '                                    , "JISSEKI_DATE" _
        '                                    , "JISSEKI_TIME" _
        '                                    , "FREE_N01" _
        '                                    , "FREE_N02" _
        '                                    , "FREE_N03" _
        '                                    , "FREE_N04" _
        '                                    , "FREE_N05" _
        '                                    , "FREE_N06" _
        '                                    , "FREE_N07" _
        '                                    , "FREE_N08" _
        '                                    , "FREE_N09" _
        '                                    , "FREE_N10" _
        '                                    , "FREE_C01" _
        '                                    , "FREE_C02" _
        '                                    , "FREE_C03" _
        '                                    , "FREE_C04" _
        '                                    , "FREE_C05" _
        '                                    , "FREE_C06" _
        '                                    , "FREE_C07" _
        '                                    , "FREE_C08" _
        '                                    , "FREE_C09" _
        '                                    , "FREE_C10" _
        '                                    , "FREE_C11" _
        '                                    , "FREE_C12" _
        '                                    , "FREE_C13" _
        '                                    , "FREE_C14" _
        '                                    , "FREE_C15" _
        '                                    , "FREE_C16" _
        '                                    , "FREE_C17" _
        '                                    , "FREE_C18" _
        '                                    , "FREE_C19" _
        '                                    , "FREE_C20" _
        '                                    , "FREE_C21" _
        '                                    , "FREE_C22" _
        '                                    , "FREE_C23" _
        '                                    , "FREE_C24" _
        '                                    , "FREE_C25" _
        '                                    , "FREE_C26" _
        '                                    , "FREE_C27" _
        '                                    , "FREE_C28" _
        '                                    , "FREE_C29" _
        '                                    , "FREE_C30" _
        '                                    , "CRT_USER" _
        '                                    , "CRT_DATE" _
        '                                    , "CRT_TIME" _
        '                                    , "UPD_USER" _
        '                                    , "UPD_DATE" _
        '                                    , "UPD_TIME" _
        '                                    , "SYS_ENT_DATE" _
        '                                    , "SYS_ENT_TIME" _
        '                                    , "SYS_ENT_PGID" _
        '                                    , "SYS_ENT_USER" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_UPD_PGID" _
        '                                    , "SYS_UPD_USER" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "SURYO" _
        '                                    , "JYOTAI" _
        '                                    , "JYOTAI_NM" _
        '                                    }

        Dim str As String() = New String() {"DEL_KB" _
                                    , "NRS_BR_CD" _
                                    , "EDI_CTL_NO" _
                                    , "EDI_CTL_NO_CHU" _
                                    , "INKA_CTL_NO_L" _
                                    , "INKA_CTL_NO_M" _
                                    , "NRS_GOODS_CD" _
                                    , "CUST_GOODS_CD" _
                                    , "GOODS_NM" _
                                    , "NB" _
                                    , "NB_UT" _
                                    , "PKG_NB" _
                                    , "PKG_UT" _
                                    , "INKA_PKG_NB" _
                                    , "HASU" _
                                    , "STD_IRIME" _
                                    , "STD_IRIME_UT" _
                                    , "BETU_WT" _
                                    , "CBM" _
                                    , "ONDO_KB" _
                                    , "OUTKA_FROM_ORD_NO" _
                                    , "BUYER_ORD_NO" _
                                    , "REMARK" _
                                    , "LOT_NO" _
                                    , "SERIAL_NO" _
                                    , "IRIME" _
                                    , "IRIME_UT" _
                                    , "OUT_KB" _
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
                                    , "SYS_ENT_DATE" _
                                    , "SYS_ENT_TIME" _
                                    , "SYS_ENT_PGID" _
                                    , "SYS_ENT_USER" _
                                    , "SYS_UPD_DATE" _
                                    , "SYS_UPD_TIME" _
                                    , "SYS_UPD_PGID" _
                                    , "SYS_UPD_USER" _
                                    , "SYS_DEL_FLG" _
                                    , "SURYO" _
                                    , "JYOTAI" _
                                    , "JYOTAI_NM" _
                                    }

        Return Me.SelectListData(ds, LMH020DAC.TABLE_NM_M, LMH020DAC.SQL_SELECT_M, LMH020DAC.SelectCondition.PTN1, str)

    End Function

#End Region

#Region "M_CUST"

    ''' <summary>
    ''' 荷主取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主取得SQLの構築・発行</remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH020DAC.TABLE_NM_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH020DAC.SQL_SELECT_CUST, br, ds))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("HOKAN_SEIQTO_CD", "HOKAN_SEIQTO_CD")
        map.Add("NIYAKU_SEIQTO_CD", "NIYAKU_SEIQTO_CD")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, LMH020DAC.TABLE_NM_CUST)

    End Function

#End Region

#Region "G_HED"

    ''' <summary>
    ''' 請求ヘッダ取得(保管料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderDataHokan(ByVal ds As DataSet) As DataSet
        Return Me.SelectGheaderData(ds, LMH020DAC.TABLE_NM_CUST, LMG000DAC.SQL_SELECT_HOKAN_CHK_DATE, "HOKAN_SEIQTO_CD")
    End Function

    ''' <summary>
    ''' 請求ヘッダ取得(荷役料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderDataNiyaku(ByVal ds As DataSet) As DataSet
        Return Me.SelectGheaderData(ds, LMH020DAC.TABLE_NM_CUST, LMG000DAC.SQL_SELECT_NIYAKU_CHK_DATE, "NIYAKU_SEIQTO_CD")
    End Function

    ''' <summary>
    ''' 請求ヘッダ取得(運賃)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderDataUnchin(ByVal ds As DataSet) As DataSet
        Return Me.SelectGheaderData(ds, LMH020DAC.TABLE_NM_CUST, LMG000DAC.SQL_SELECT_KEIRI_CHK_DATE, "UNCHIN_SEIQTO_CD", "UNCHIN_KB")
    End Function

    ''' <summary>
    ''' 請求ヘッダ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm1">列名1</param>
    ''' <param name="colNm2">列名2　初期値 = ''</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求ヘッダ取得SQLの構築・発行</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal colNm1 As String, Optional ByVal colNm2 As String = "") As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(tblNm)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item(colNm1).ToString(), DBDataType.CHAR))
        Dim inkaDr As DataRow = ds.Tables(LMH020DAC.TABLE_NM_L).Rows(0)
        If String.IsNullOrEmpty(colNm2) = False Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", inkaDr.Item(colNm2).ToString(), DBDataType.CHAR))
        End If
        Dim brCd As String = inkaDr.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, brCd, ds))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LMH020DAC.TABLE_NM_G_HED))

    End Function

#End Region

#End Region

#Region "設定処理"

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' EDI入荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiInkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH020DAC.TABLE_NM_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH020DAC.SQL_UPDATE_L, Me._Row.Item("NRS_BR_CD").ToString(), ds))

        'パラメータ設定
        Call Me.SetEdiInkaLComParameter(Me._SqlPrmList, Me._Row)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' EDI入荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiInkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH020DAC.TABLE_NM_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH020DAC.SQL_UPDATE_M, Me._Row.Item("NRS_BR_CD").ToString(), ds))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            cmd.Parameters.Clear()
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetEdiInkaMComParameter(Me._SqlPrmList, Me._Row)
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

#End Region

#Region "HED_TBL"

    ''' <summary>
    ''' EDI受信(ヘッダ)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHedTblData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH020DAC.TABLE_NM_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append("UPDATE $LM_TRN$..$HED_TBL$ SET        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMH020DAC.SQL_UPDATE_COM)
        Me._StrSql.Append("  AND SYS_UPD_DATE = @GUI_UPD_DATE    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  AND SYS_UPD_TIME = @GUI_UPD_TIME    ")
        Me._StrSql.Append(vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString(), ds))

        'パラメータ設定
        Call Me.SetPkParameter(Me._SqlPrmList, Me._Row)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row, "RCV_UPD_DATE", "RCV_UPD_TIME")

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "DTL_TBL"

    ''' <summary>
    ''' EDI受信(明細)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateDtlTblData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim drs As DataRow() = ds.Tables(LMH020DAC.TABLE_NM_M).Select(LMH020DAC.SQL_JUSHIN_DATA)
        Dim max As Integer = drs.Length - 1

        'SQL固定情報の設定
        Dim sql1 As String = String.Concat("UPDATE $LM_TRN$..$DTL_TBL$ SET ", vbNewLine)
        Dim sql2 As String = String.Concat(",SYS_DEL_FLG = @JYOTAI ", vbNewLine, ",", LMH020DAC.SQL_UPDATE_COM, "  AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU   ", vbNewLine)

        'SQL文のコンパイル(SQL文が変わるためForの中でインスタンス生成)
        Dim cmd As SqlCommand = Nothing

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = drs(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()
            Me._StrSql.Append(sql1)
            If LMConst.FLG.OFF.Equals(Me._Row.Item("JYOTAI").ToString()) = True Then

                Me._StrSql.Append(" DELETE_USER = '' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(",DELETE_DATE = '' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(",DELETE_TIME = '' ")
                Me._StrSql.Append(vbNewLine)

            Else

                Me._StrSql.Append(" DELETE_USER = @SYS_UPD_USER ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(",DELETE_DATE = @SYS_UPD_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(",DELETE_TIME = @UPD_TIME     ")
                Me._StrSql.Append(vbNewLine)

            End If
            Me._StrSql.Append(sql2)

            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString(), ds))

            'SQLパラメータ初期化
            cmd.Parameters.Clear()
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetPkParameter(Me._SqlPrmList, Me._Row)
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me._Row.Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYOTAI", Me._Row.Item("JYOTAI").ToString(), DBDataType.CHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "排他チェック"

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectHaitaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH020DAC.TABLE_NM_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, LMH020DAC.SelectCondition.PTN1)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH020DAC.SQL_SELECT_COUNT, Me._Row.Item("NRS_BR_CD").ToString(), ds))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String, ByVal ds As DataSet) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        'ヘッダテーブル名を設定
        Dim dt As DataTable = ds.Tables(LMH020DAC.TABLE_NM_RCV_NM)
        If 0 < dt.Rows.Count Then
            Dim dr As DataRow = dt.Rows(0)
            sql = sql.Replace("$HED_TBL$", dr.Item("RCV_NM_HED").ToString())
            sql = sql.Replace("$DTL_TBL$", dr.Item("RCV_NM_DTL").ToString())
        End If

        Return sql

    End Function

    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMH020DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH020DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString(), ds))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMH020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

    End Function

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMH020DAC.SelectCondition)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            Select Case ptn

                Case LMH020DAC.SelectCondition.PTN1

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

                Case LMH020DAC.SelectCondition.PTN2

                    prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("NRS_WH_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

                Case LMH020DAC.SelectCondition.PTN3

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

                Case LMH020DAC.SelectCondition.PTN4

                    prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@MATOME_FLG", .Item("MATOME_FLG").ToString(), DBDataType.CHAR))

            End Select


        End With

    End Sub

    ''' <summary>
    ''' PKのパラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <remarks></remarks>
    Private Sub SetPkParameter(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            '更新日時
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 排他の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <param name="update">更新日の列名</param>
    ''' <param name="uptime">更新時間の列名</param>
    ''' <remarks></remarks>
    Private Sub SetGuiSysdataTimeParameter(ByVal prmList As ArrayList _
                                           , ByVal dr As DataRow _
                                           , Optional ByVal update As String = "SYS_UPD_DATE" _
                                           , Optional ByVal uptime As String = "SYS_UPD_TIME" _
                                           )

        With dr

            '更新日時
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item(update).ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item(uptime).ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.SetTimeParameter(prmList), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時刻(hh:mm:ss)の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetTimeParameter(ByVal prmList As ArrayList) As String

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

    ''' <summary>
    ''' EDI入荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetEdiInkaLComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        With conditionRow

            '2012.03.15 要望番号895 大阪対応START
            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", "0"))
            '2012.03.15 要望番号895 大阪対応END
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TP", .Item("INKA_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_KB", .Item("INKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", .Item("INKA_PLAN_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", .Item("INKA_PLAN_QT_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", .Item("INKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", .Item("OUTKA_FROM_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NYUBAN_L", .Item("NYUBAN_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", .Item("UNCHIN_TP").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO", .Item("OUTKA_MOTO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", .Item("SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN", .Item("UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("YOKO_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", .Item("OUT_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
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

    End Sub

    ''' <summary>
    ''' EDI入荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetEdiInkaMComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        With conditionRow

            '2012.03.15 要望番号895 大阪対応START
            If (.Item("JYOTAI").ToString()).Equals("1") = True Then
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("JYOTAI").ToString(), DBDataType.CHAR))
            ElseIf (.Item("DEL_KB").ToString()).Equals("3") = True Then
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", "0"))
            Else
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("JYOTAI").ToString(), DBDataType.CHAR))
            End If
            '2012.03.15 要望番号895 大阪対応END
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NB", .Item("NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PKG_NB", .Item("INKA_PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HASU", .Item("HASU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", .Item("OUTKA_FROM_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STD_IRIME", .Item("STD_IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", .Item("STD_IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.NVARCHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("JYOTAI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

End Class
