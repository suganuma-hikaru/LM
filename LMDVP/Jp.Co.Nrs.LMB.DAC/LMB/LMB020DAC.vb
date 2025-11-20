' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB020    : 入荷データ編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB020DAC
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
    End Enum

    '更新前Select文
    Private Const SELECT_INSERT_DATA As String = "SYS_DEL_FLG = '0' AND UP_KBN = '0' "
    Private Const SELECT_UPDATE_DATA As String = "SYS_DEL_FLG = '0' AND UP_KBN = '1' "
    Private Const SELECT_DELETE_DATA_001 As String = "SYS_DEL_FLG = '1' "
    Private Const SELECT_DELETE_DATA_002 As String = "UP_KBN = '2' "

    '印刷種別
    Private Const PRINT_HOUKOKUSHO As String = "01"
    Private Const PRINT_CHECKLIST As String = "02"
    Private Const PRINT_UKETSUKEHYOU As String = "03"
    '2012/12/06入荷報告チェックリスト追加
    Private Const PRINT_HOUKOKU_CHECKLIST As String = "04"
    '2012/12/10入荷確定入力モニター表
    Private Const PRINT_DECI_MONITER As String = "05"
    Private Const PRINT_HOUKOKUSHO_KAKUIN As String = "07"      'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能

    '引当(済)
    Private Const HIKIATE_ZUMI As String = "済"

#End Region

#Region "検索処理 SQL"

#Region "データ抽出"

#Region "INKA_L"

    Private Const SQL_SELECT_INKA_L As String = "SELECT                                                 " & vbNewLine _
                                              & " INKL.NRS_BR_CD                  AS NRS_BR_CD          " & vbNewLine _
                                              & ",INKL.INKA_NO_L                  AS INKA_NO_L          " & vbNewLine _
                                              & ",INKL.FURI_NO                    AS FURI_NO            " & vbNewLine _
                                              & ",INKL.INKA_TP                    AS INKA_TP            " & vbNewLine _
                                              & ",INKL.INKA_KB                    AS INKA_KB            " & vbNewLine _
                                              & ",INKL.INKA_STATE_KB              AS INKA_STATE_KB      " & vbNewLine _
                                              & ",INKL.INKA_DATE                  AS INKA_DATE          " & vbNewLine _
                                              & ",INKL.STORAGE_DUE_DATE           AS STORAGE_DUE_DATE  --ADD 20170710" & vbNewLine _
                                              & ",INKL.WH_CD                      AS WH_CD              " & vbNewLine _
                                              & ",INKL.CUST_CD_L                  AS CUST_CD_L          " & vbNewLine _
                                              & ",INKL.CUST_CD_M                  AS CUST_CD_M          " & vbNewLine _
                                              & ",INKL.INKA_PLAN_QT               AS INKA_PLAN_QT       " & vbNewLine _
                                              & ",INKL.INKA_PLAN_QT_UT            AS INKA_PLAN_QT_UT    " & vbNewLine _
                                              & ",INKL.INKA_TTL_NB                AS INKA_TTL_NB        " & vbNewLine _
                                              & ",INKL.BUYER_ORD_NO_L             AS BUYER_ORD_NO_L     " & vbNewLine _
                                              & "--* UPD 2018/10/02 依頼番号 : 002498 --2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen upd start " & vbNewLine _
                                              & "--* ,CASE WHEN INKEDIL.INKA_CTL_NO_L <> '' THEN           " & vbNewLine _
                                              & "--*     CASE WHEN INKEDIL.EDI_COUNT >= 2                  " & vbNewLine _
                                              & "--*	           AND INKL.SYS_ENT_DATE = INKL.SYS_UPD_DATE " & vbNewLine _
                                              & "--*			   AND INKL.SYS_ENT_TIME = INKL.SYS_UPD_TIME THEN " & vbNewLine _
                                              & "--*    ''                                                 " & vbNewLine _
                                              & "--*	 ELSE                                               " & vbNewLine _
                                              & "--*	     INKL.OUTKA_FROM_ORD_NO_L                       " & vbNewLine _
                                              & "--*	  END                                               " & vbNewLine _
                                              & "--* ELSE                                                  " & vbNewLine _
                                              & "--*     INKL.OUTKA_FROM_ORD_NO_L                          " & vbNewLine _
                                              & "--* END                             AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
                                              & ",INKL.OUTKA_FROM_ORD_NO_L        AS OUTKA_FROM_ORD_NO_L" & vbNewLine _
                                              & "--2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen upd end " & vbNewLine _
                                              & ",INKL.TOUKI_HOKAN_YN             AS TOUKI_HOKAN_YN     " & vbNewLine _
                                              & ",INKL.HOKAN_YN                   AS HOKAN_YN           " & vbNewLine _
                                              & ",INKL.HOKAN_FREE_KIKAN           AS HOKAN_FREE_KIKAN   " & vbNewLine _
                                              & ",INKL.HOKAN_STR_DATE             AS HOKAN_STR_DATE     " & vbNewLine _
                                              & ",INKL.NIYAKU_YN                  AS NIYAKU_YN          " & vbNewLine _
                                              & ",INKL.TAX_KB                     AS TAX_KB             " & vbNewLine _
                                              & ",INKL.REMARK                     AS REMARK             " & vbNewLine _
                                              & ",INKL.REMARK_OUT                 AS REMARK_OUT         " & vbNewLine _
                                              & ",''                              AS CHECKLIST_PRT_DATE " & vbNewLine _
                                              & ",''                              AS CHECKLIST_PRT_USER " & vbNewLine _
                                              & ",INKL.UNCHIN_TP                  AS UNCHIN_TP          " & vbNewLine _
                                              & ",INKL.UNCHIN_KB                  AS UNCHIN_KB          " & vbNewLine _
                                              & ",CUST.CUST_NM_L + CUST.CUST_NM_M AS CUST_NM            " & vbNewLine _
                                              & ",KBN1.KBN_NM1                    AS INKA_PLAN_QT_UT_NM " & vbNewLine _
                                              & ",SOKO.JIS_CD                     AS JIS_CD             " & vbNewLine _
                                              & ",CUST.CUST_NM_L                  AS CUST_NM_L          " & vbNewLine _
                                              & ",CUST.CUST_NM_M                  AS CUST_NM_M          " & vbNewLine _
                                              & ",SOKO.WH_NM                      AS SOKO_NM            " & vbNewLine _
                                              & ",MNRS.NRS_BR_NM                  AS NRS_BR_NM          " & vbNewLine _
                                              & ",CUST.PIC                        AS TANTO_USER         " & vbNewLine _
                                              & ",USR1.USER_NM                    AS SYS_ENT_USER       " & vbNewLine _
                                              & ",USR2.USER_NM                    AS SYS_UPD_USER       " & vbNewLine _
                                              & ",KBN2.KBN_NM1                    AS INKA_STATE_KB_NM   " & vbNewLine _
                                              & ",KBN3.KBN_NM1                    AS INKA_KB_NM         " & vbNewLine _
                                              & ",OUTL.OUTKA_NO_L                 AS OUTKA_NO_L         " & vbNewLine _
                                              & ",'0'                             AS PRINT_FLG          " & vbNewLine _
                                              & ",INKL.SYS_UPD_DATE               AS SYS_UPD_DATE       " & vbNewLine _
                                              & ",INKL.SYS_UPD_TIME               AS SYS_UPD_TIME       " & vbNewLine _
                                              & ",OUTL.SYS_UPD_DATE               AS OUTKA_SYS_UPD_DATE " & vbNewLine _
                                              & ",OUTL.SYS_UPD_TIME               AS OUTKA_SYS_UPD_TIME " & vbNewLine _
                                              & ",INKL.WH_KENPIN_WK_STATUS        AS WH_KENPIN_WK_STATUS" & vbNewLine _
                                              & ",INKL.WH_TAB_STATUS              AS WH_TAB_SAGYO_SIJI_STATUS              " & vbNewLine _
                                              & ",INKL.WH_TAB_YN                  AS WH_TAB_YN                             " & vbNewLine _
                                              & ",INKL.WH_TAB_IMP_YN              AS WH_TAB_IMP_YN                         " & vbNewLine _
                                              & ",CASE WHEN TABH.IN_KENPIN_LOC_STATE_KB IN ('00','01','02','03','04')      " & vbNewLine _
                                              & "	   AND  TABH.CANCEL_FLG = '01'                               THEN '99' " & vbNewLine _
                                              & "      WHEN TABH.IN_KENPIN_LOC_STATE_KB = '00'                   THEN '00' " & vbNewLine _
                                              & "      WHEN TABH.IN_KENPIN_LOC_STATE_KB = '01'                   THEN '01' " & vbNewLine _
                                              & "      WHEN TABH.IN_KENPIN_LOC_STATE_KB = '02'                   THEN '02' " & vbNewLine _
                                              & "      WHEN TABH.IN_KENPIN_LOC_STATE_KB = '03'                   THEN '03' " & vbNewLine _
                                              & "      WHEN TABH.IN_KENPIN_LOC_STATE_KB = '04'                             " & vbNewLine _
                                              & "	    AND INKL.WH_TAB_IMP_YN          = '00'                   THEN '04' " & vbNewLine _
                                              & "	  WHEN TABH.IN_KENPIN_LOC_STATE_KB  = '04'                             " & vbNewLine _
                                              & "	    AND INKL.WH_TAB_IMP_YN          = '01'                   THEN '05' " & vbNewLine _
                                              & "	  ELSE ''                                                              " & vbNewLine _
                                              & " END                             AS WH_TAB_SAGYO_STATUS                   " & vbNewLine _
                                              & "--DEL 2019/10/10 要望管理007373  ,INKL.STOP_ALLOC                 AS STOP_ALLOC         --ADD 2019/08/01 要望管理005237" & vbNewLine _
                                              & ",INKL.WH_TAB_NO_SIJI_FLG         AS WH_TAB_NO_SIJI_FLG " & vbNewLine _
                                              & "FROM       $LM_TRN$..B_INKA_L  INKL                    " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..C_OUTKA_L OUTL                    " & vbNewLine _
                                              & "ON    INKL.NRS_BR_CD         = OUTL.NRS_BR_CD          " & vbNewLine _
                                              & "AND   INKL.FURI_NO           = OUTL.FURI_NO            " & vbNewLine _
                                              & "AND   RTRIM(OUTL.FURI_NO)   <>  ''                     " & vbNewLine _
                                              & "AND   OUTL.FURI_NO IS NOT NULL                         " & vbNewLine _
                                              & "AND   OUTL.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_CUST    CUST                    " & vbNewLine _
                                              & "ON    INKL.NRS_BR_CD         = CUST.NRS_BR_CD          " & vbNewLine _
                                              & "AND   INKL.CUST_CD_L         = CUST.CUST_CD_L          " & vbNewLine _
                                              & "AND   INKL.CUST_CD_M         = CUST.CUST_CD_M          " & vbNewLine _
                                              & "AND   CUST.CUST_CD_S         = '00'                    " & vbNewLine _
                                              & "AND   CUST.CUST_CD_SS        = '00'                    " & vbNewLine _
                                              & "AND   CUST.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_NRS_BR  MNRS                    " & vbNewLine _
                                              & "ON    INKL.NRS_BR_CD         = MNRS.NRS_BR_CD          " & vbNewLine _
                                              & "AND   MNRS.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_SOKO    SOKO                    " & vbNewLine _
                                              & "ON    INKL.NRS_BR_CD         = SOKO.NRS_BR_CD          " & vbNewLine _
                                              & "AND   INKL.WH_CD             = SOKO.WH_CD              " & vbNewLine _
                                              & "AND   SOKO.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..S_USER    USR1                    " & vbNewLine _
                                              & "ON    INKL.SYS_ENT_USER      = USR1.NRS_BR_CD          " & vbNewLine _
                                              & "AND   USR1.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..S_USER    USR2                    " & vbNewLine _
                                              & "ON    INKL.SYS_UPD_USER      = USR2.NRS_BR_CD          " & vbNewLine _
                                              & "AND   USR2.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..Z_KBN     KBN1                    " & vbNewLine _
                                              & "ON    INKL.INKA_PLAN_QT_UT   = KBN1.KBN_CD             " & vbNewLine _
                                              & "AND   KBN1.KBN_GROUP_CD      = 'I001'                  " & vbNewLine _
                                              & "AND   KBN1.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..Z_KBN     KBN2                    " & vbNewLine _
                                              & "ON    INKL.INKA_STATE_KB     = KBN2.KBN_CD             " & vbNewLine _
                                              & "AND   KBN2.KBN_GROUP_CD      = 'N004'                  " & vbNewLine _
                                              & "AND   KBN2.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..Z_KBN     KBN3                    " & vbNewLine _
                                              & "ON    INKL.INKA_KB           = KBN3.KBN_CD             " & vbNewLine _
                                              & "AND   KBN3.KBN_GROUP_CD      = 'N006'                  " & vbNewLine _
                                              & "AND   KBN3.SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "--2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen upd start " & vbNewLine _
                                              & "LEFT JOIN(                                             " & vbNewLine _
                                              & "	SELECT                                              " & vbNewLine _
                                              & "		 NRS_BR_CD                                      " & vbNewLine _
                                              & "	    ,INKA_CTL_NO_L             AS INKA_CTL_NO_L     " & vbNewLine _
                                              & "		,COUNT(INKA_CTL_NO_L)      AS EDI_COUNT         " & vbNewLine _
                                              & "	FROM                                                " & vbNewLine _
                                              & "	     $LM_TRN$..H_INKAEDI_L      -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                              & "	WHERE                                               " & vbNewLine _
                                              & "	      SYS_DEL_FLG = '0'                             " & vbNewLine _
                                              & "	  AND NRS_BR_CD = @NRS_BR_CD  --UPD 2020/06/04 '10'固定対応　013118 " & vbNewLine _
                                              & "	  AND CUST_CD_L = '00010'                           " & vbNewLine _
                                              & "	GROUP BY NRS_BR_CD,INKA_CTL_NO_L) INKEDIL           " & vbNewLine _
                                              & "ON INKL.NRS_BR_CD = INKEDIL.NRS_BR_CD                  " & vbNewLine _
                                              & "AND INKL.INKA_NO_L = INKEDIL.INKA_CTL_NO_L             " & vbNewLine _
                                              & "--2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen upd end " & vbNewLine _
                                              & "LEFT  JOIN                                             " & vbNewLine _
                                              & "    (                                                  " & vbNewLine _
                                              & "	 SELECT NRS_BR_CD                                   " & vbNewLine _
                                              & "	       ,INKA_NO_L                                   " & vbNewLine _
                                              & "	       ,MAX(IN_KENPIN_LOC_SEQ)AS IN_KENPIN_LOC_SEQ  " & vbNewLine _
                                              & "	 FROM   $LM_TRN$..TB_KENPIN_HEAD                    " & vbNewLine _
                                              & "    WHERE  NRS_BR_CD         = @NRS_BR_CD              " & vbNewLine _
                                              & "    AND    INKA_NO_L         = @INKA_NO_L              " & vbNewLine _
                                              & "    AND    SYS_DEL_FLG       = '0'                     " & vbNewLine _
                                              & "    GROUP BY NRS_BR_CD                                 " & vbNewLine _
                                              & "	         ,INKA_NO_L                                 " & vbNewLine _
                                              & "	) TABM                                              " & vbNewLine _
                                              & "ON    TABM.NRS_BR_CD = INKL.NRS_BR_CD                  " & vbNewLine _
                                              & "AND   TABM.INKA_NO_L = INKL.INKA_NO_L                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..TB_KENPIN_HEAD TABH               " & vbNewLine _
                                              & "ON    TABH.NRS_BR_CD         = TABM.NRS_BR_CD          " & vbNewLine _
                                              & "AND   TABH.INKA_NO_L         = TABM.INKA_NO_L          " & vbNewLine _
                                              & "AND   TABH.IN_KENPIN_LOC_SEQ = TABM.IN_KENPIN_LOC_SEQ  " & vbNewLine _
                                              & "WHERE INKL.NRS_BR_CD         = @NRS_BR_CD              " & vbNewLine _
                                              & "AND   INKL.INKA_NO_L         = @INKA_NO_L              " & vbNewLine _
                                              & "AND   INKL.SYS_DEL_FLG       = '0'                     " & vbNewLine

#End Region

#Region "INKA_M"

    'START YANAI メモ②No.20
    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                          & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                          & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                          & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                          & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                          & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                          & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                          & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                          & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                          & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                          & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                          & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                          & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                          & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.STD_IRIME_NB , 0)                             " & vbNewLine _
    '                                          & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                          & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                          & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                          & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                          & "      * INKS.IRIME                                                " & vbNewLine _
    '                                          & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                          & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                          & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                          & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                          & ",CASE WHEN ZAIT.ALLOC_CAN_NB <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "        OR ZAIT.ALLOC_CAN_QT <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.STD_IRIME_NB , 0)    " & vbNewLine _
    '                                          & "      THEN '済'                                                   " & vbNewLine _
    '                                          & "      ELSE '未'                                                   " & vbNewLine _
    '                                          & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                          & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                          & "      THEN '無'                                                   " & vbNewLine _
    '                                          & "      ELSE '有'                                                   " & vbNewLine _
    '                                          & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                          & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                          & "     * ISNULL(GOOD.PKG_NB , 0)         AS      EDI_SURYO          " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                          & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                          & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                          & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   IDO.N_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                          & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                          & "         ,IDO.O_IRIME                                             " & vbNewLine _
    '                                          & ") ZAIT                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                          & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                          & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                          & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                          & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                          & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & ")  SAGY                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                          & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                          & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                          & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                          & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                          & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                          & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                          & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                          & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                          & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                          & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                          & "ORDER BY                                                          " & vbNewLine _
    '                                          & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine
    'START YANAI 要望番号496
    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                      & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                      & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                      & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                      & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                      & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                      & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                      & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                      & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                      & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                      & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                      & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                      & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                      & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                      & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                      & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                      & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                      & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                      & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                      & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                      & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                      & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                      & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                      & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                      & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                      & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                      & "      * ISNULL(INKS.IRIME , 0)                                    " & vbNewLine _
    '                                      & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                      & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                      & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                      & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                      & "      * INKS.IRIME                                                " & vbNewLine _
    '                                      & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                      & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                      & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                      & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                      & ",CASE WHEN ZAIT.ALLOC_CAN_NB <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                      & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                      & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                      & "        OR ZAIT.ALLOC_CAN_QT <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                      & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                      & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                      & "                               * ISNULL(INKS.IRIME , 0)           " & vbNewLine _
    '                                      & "      THEN '済'                                                   " & vbNewLine _
    '                                      & "      ELSE '未'                                                   " & vbNewLine _
    '                                      & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                      & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                      & "      THEN '無'                                                   " & vbNewLine _
    '                                      & "      ELSE '有'                                                   " & vbNewLine _
    '                                      & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                      & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                      & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                      & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                      & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
    '                                      & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                      & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                      & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                      & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                      & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                      & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                      & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                      & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                      & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                      & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                      & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
    '                                      & "                                                     ELSE '1'     " & vbNewLine _
    '                                      & " END                                                 AS EDI_FLG   " & vbNewLine _
    '                                      & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                      & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                      & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                      & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                      & "LEFT  JOIN                                                        " & vbNewLine _
    '                                      & "(SELECT                                                           " & vbNewLine _
    '                                      & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                      & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                      & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                      & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                      & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                      & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                      & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                      & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                      & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                      & "FROM                                                              " & vbNewLine _
    '                                      & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                      & "LEFT  JOIN                                                        " & vbNewLine _
    '                                      & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                      & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                      & "AND   IDO.N_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                      & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                      & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                      & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                      & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                      & "GROUP BY                                                          " & vbNewLine _
    '                                      & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                      & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                      & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                      & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                      & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                      & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                      & "         ,IDO.O_IRIME                                             " & vbNewLine _
    '                                      & ") ZAIT                                                            " & vbNewLine _
    '                                      & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                      & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                      & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                      & "LEFT  JOIN                                                        " & vbNewLine _
    '                                      & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                      & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                      & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                      & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                      & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                      & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                      & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                      & ")  SAGY                                                           " & vbNewLine _
    '                                      & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                      & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                      & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                      & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                      & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                      & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                      & "AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                      & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                      & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                      & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                      & "GROUP BY                                                          " & vbNewLine _
    '                                      & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                      & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                      & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                      & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                      & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                      & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                      & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                      & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                      & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                      & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                      & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                      & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                      & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                      & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                      & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                      & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                      & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                      & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                      & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                      & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                      & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                      & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                      & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                      & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                      & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                      & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                      & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                      & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                      & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                      & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                      & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                      & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                      & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                      & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                      & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                      & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
    '                                      & "         ,EDIM.IRIME                                              " & vbNewLine _
    '                                      & "         ,INKS.IRIME                                              " & vbNewLine _
    '                                      & "ORDER BY                                                          " & vbNewLine _
    '                                      & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                      & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                      & "         ,INKM.INKA_NO_M                                          " & vbNewLine
    'START YANAI 要望番号573
    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                          & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                          & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                          & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                          & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                          & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                          & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                          & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                          & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                          & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                          & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                          & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                          & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                          & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                          & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                          & "      * ISNULL(INKS.IRIME , 0))                                   " & vbNewLine _
    '                                          & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                          & ",SUM(((ISNULL(INKS.KONSU , 0)                                     " & vbNewLine _
    '                                          & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                          & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                          & "      *  ISNULL(INKS.IRIME  , 0))                                 " & vbNewLine _
    '                                          & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                          & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                          & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                          & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                          & ",CASE WHEN ZAIT.ALLOC_CAN_NB <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "        OR ZAIT.ALLOC_CAN_QT <> SUM((ISNULL(INKS.KONSU , 0)       " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "                               * ISNULL(INKS.IRIME , 0))          " & vbNewLine _
    '                                          & "      THEN '済'                                                   " & vbNewLine _
    '                                          & "      ELSE '未'                                                   " & vbNewLine _
    '                                          & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                          & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                          & "      THEN '無'                                                   " & vbNewLine _
    '                                          & "      ELSE '有'                                                   " & vbNewLine _
    '                                          & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                          & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                          & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                          & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
    '                                          & "                                                     ELSE '1'     " & vbNewLine _
    '                                          & " END                                                 AS EDI_FLG   " & vbNewLine _
    '                                          & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                          & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   IDO.N_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                          & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                          & "         ,IDO.O_IRIME                                             " & vbNewLine _
    '                                          & ") ZAIT                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                          & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                          & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                          & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                          & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                          & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & ")  SAGY                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                          & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                          & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                          & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                          & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                          & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                          & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                          & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                          & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                          & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                          & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                          & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
    '                                          & "         ,EDIM.IRIME                                              " & vbNewLine _
    '                                          & "ORDER BY                                                          " & vbNewLine _
    '                                          & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine
    'START YANAI 要望番号788
    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                          & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                          & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                          & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                          & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                          & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                          & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                          & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                          & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                          & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                          & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                          & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                          & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                          & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                          & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                          & "      * ISNULL(INKS.IRIME , 0))                                   " & vbNewLine _
    '                                          & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                          & ",SUM(((ISNULL(INKS.KONSU , 0)                                     " & vbNewLine _
    '                                          & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                          & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                          & "      *  ISNULL(INKS.IRIME  , 0))                                 " & vbNewLine _
    '                                          & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                          & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                          & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                          & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                          & ",CASE WHEN ZAIT.ALLOC_CAN_NB <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "        OR ZAIT.ALLOC_CAN_QT <> SUM((ISNULL(INKS.KONSU , 0)       " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "                               * ISNULL(INKS.IRIME , 0))          " & vbNewLine _
    '                                          & "      THEN '済'                                                   " & vbNewLine _
    '                                          & "      ELSE '未'                                                   " & vbNewLine _
    '                                          & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                          & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                          & "      THEN '無'                                                   " & vbNewLine _
    '                                          & "      ELSE '有'                                                   " & vbNewLine _
    '                                          & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                          & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                          & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                          & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
    '                                          & "                                                     ELSE '1'     " & vbNewLine _
    '                                          & " END                                                 AS EDI_FLG   " & vbNewLine _
    '                                          & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                          & ",ISNULL(EDIM.JISSEKI_FLAG,'')          AS      JISSEKI_FLAG       " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                          & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   IDO.N_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                          & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                          & "         ,IDO.O_IRIME                                             " & vbNewLine _
    '                                          & ") ZAIT                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                          & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                          & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                          & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                          & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                          & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & ")  SAGY                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                          & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                          & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                          & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                          & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                          & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                          & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                          & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                          & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                          & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                          & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                          & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
    '                                          & "         ,EDIM.IRIME                                              " & vbNewLine _
    '                                          & "         ,EDIM.JISSEKI_FLAG                                       " & vbNewLine _
    '                                          & "ORDER BY                                                          " & vbNewLine _
    '                                          & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine
    'START YANAI 要望番号899
    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                          & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                          & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                          & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                          & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                          & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                          & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                          & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                          & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                          & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                          & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                          & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                          & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                          & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                          & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                          & "      * ISNULL(INKS.IRIME , 0))                                   " & vbNewLine _
    '                                          & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                          & ",SUM(((ISNULL(INKS.KONSU , 0)                                     " & vbNewLine _
    '                                          & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                          & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                          & "      *  ISNULL(INKS.IRIME  , 0))                                 " & vbNewLine _
    '                                          & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                          & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                          & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                          & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                          & ",CASE WHEN ZAIT.ALLOC_CAN_NB <> SUM(ISNULL(INKS.KONSU , 0)        " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "        OR ZAIT.ALLOC_CAN_QT <> SUM((ISNULL(INKS.KONSU , 0)       " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "                               * ISNULL(INKS.IRIME , 0))          " & vbNewLine _
    '                                          & "      THEN '済'                                                   " & vbNewLine _
    '                                          & "      ELSE '未'                                                   " & vbNewLine _
    '                                          & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                          & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                          & "      THEN '無'                                                   " & vbNewLine _
    '                                          & "      ELSE '有'                                                   " & vbNewLine _
    '                                          & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                          & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                          & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                          & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
    '                                          & "                                                     ELSE '1'     " & vbNewLine _
    '                                          & " END                                                 AS EDI_FLG   " & vbNewLine _
    '                                          & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                          & ",ISNULL(EDIM.JISSEKI_FLAG,'')          AS      JISSEKI_FLAG       " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "INNER  JOIN                                                       " & vbNewLine _
    '                                          & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                          & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   IDO.O_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                          & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                          & "         ,IDO.O_IRIME                                             " & vbNewLine _
    '                                          & ") ZAIT                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                          & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                          & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                          & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                          & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                          & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & ")  SAGY                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                          & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                          & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                          & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                          & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                          & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                          & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                          & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                          & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                          & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                          & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                          & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
    '                                          & "         ,EDIM.IRIME                                              " & vbNewLine _
    '                                          & "         ,EDIM.JISSEKI_FLAG                                       " & vbNewLine _
    '                                          & "ORDER BY                                                          " & vbNewLine _
    '                                          & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine
    'START YANAI 要望番号1039 入荷で、作業を後から削除した際に、表示した直後は『有』と表示され、Spreadをダブルクリックすると、無になる
    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                          & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                          & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                          & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                          & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                          & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                          & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                          & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                          & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                          & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                          & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                          & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                          & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                          & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                          & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                          & "      * ISNULL(INKS.IRIME , 0))                                   " & vbNewLine _
    '                                          & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                          & ",SUM(((ISNULL(INKS.KONSU , 0)                                     " & vbNewLine _
    '                                          & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                          & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                          & "      *  ISNULL(INKS.IRIME  , 0))                                 " & vbNewLine _
    '                                          & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                          & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                          & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                          & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(ZAIT.ALLOC_CAN_NB,ZAIT2.ALLOC_CAN_NB) <> SUM(ISNULL(INKS.KONSU , 0) " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "        OR ISNULL(ZAIT.ALLOC_CAN_QT,ZAIT2.ALLOC_CAN_QT) <> SUM((ISNULL(INKS.KONSU , 0) " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "                               * ISNULL(INKS.IRIME , 0))          " & vbNewLine _
    '                                          & "      THEN '済'                                                   " & vbNewLine _
    '                                          & "      ELSE '未'                                                   " & vbNewLine _
    '                                          & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                          & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                          & "      THEN '無'                                                   " & vbNewLine _
    '                                          & "      ELSE '有'                                                   " & vbNewLine _
    '                                          & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                          & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                          & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                          & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
    '                                          & "                                                     ELSE '1'     " & vbNewLine _
    '                                          & " END                                                 AS EDI_FLG   " & vbNewLine _
    '                                          & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                          & ",ISNULL(EDIM.JISSEKI_FLAG,'')          AS      JISSEKI_FLAG       " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.O_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "INNER  JOIN                                                       " & vbNewLine _
    '                                          & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                          & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   IDO.O_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                          & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                          & "         ,IDO.O_IRIME                                             " & vbNewLine _
    '                                          & ") ZAIT                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB)  AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & ") ZAIT2                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT2.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT2.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT2.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                          & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                          & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                          & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                          & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                          & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & ")  SAGY                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                          & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                          & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                          & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                          & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                          & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                          & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                          & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                          & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                          & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                          & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                          & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
    '                                          & "         ,EDIM.IRIME                                              " & vbNewLine _
    '                                          & "         ,EDIM.JISSEKI_FLAG                                       " & vbNewLine _
    '                                          & "         ,ZAIT2.ALLOC_CAN_NB                                     " & vbNewLine _
    '                                          & "         ,ZAIT2.ALLOC_CAN_QT                                     " & vbNewLine _
    '                                          & "ORDER BY                                                          " & vbNewLine _
    '                                          & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine

    '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 upd start
    Private Const SQL_SELECT_INKA_M_PARTS1 As String = "SELECT DISTINCT                                                   " & vbNewLine _
                                              & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
                                              & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
                                              & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
                                              & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
                                              & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
                                              & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
                                              & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
                                              & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
                                              & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
                                              & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
                                              & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
                                              & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
                                              & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
                                              & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
                                              & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
                                              & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
                                              & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
                                              & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
                                              & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
                                              & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
                                              & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
                                              & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
                                              & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
                                              & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
                                              & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
                                              & "      * ISNULL(INKS.IRIME , 0))                                   " & vbNewLine _
                                              & "                                       AS      SUM_SURYO_M        " & vbNewLine _
                                              & ",SUM(((ISNULL(INKS.KONSU , 0)                                     " & vbNewLine _
                                              & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
                                              & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
                                              & "      *  ISNULL(INKS.IRIME  , 0))                                 " & vbNewLine _
                                              & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
                                              & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
                                              & "                                       AS      SUM_JURYO_M        " & vbNewLine _
                                              & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
                                              & ",CASE WHEN ISNULL(ZAIT.ALLOC_CAN_NB,ZAIT2.ALLOC_CAN_NB) <> SUM(ISNULL(INKS.KONSU , 0) " & vbNewLine _
                                              & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
                                              & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
                                              & "        OR ISNULL(ZAIT.ALLOC_CAN_QT,ZAIT2.ALLOC_CAN_QT) <> SUM((ISNULL(INKS.KONSU , 0) " & vbNewLine _
                                              & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
                                              & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
                                              & "                               * ISNULL(INKS.IRIME , 0))          " & vbNewLine _
                                              & "      THEN '済'                                                   " & vbNewLine _
                                              & "      ELSE 　　                                                   " & vbNewLine _
                                              & "           CASE WHEN OUTS.MAX_OUTKA_NO_L is NULL THEN '未'        " & vbNewLine _
                                              & "                ELSE '済'                                         " & vbNewLine _
                                              & " END  END                              AS      HIKIATE            " & vbNewLine _
                                              & "--依頼番号 : 020059 UPD S                                         " & vbNewLine _
                                              & "--,CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
                                              & "--      THEN '無'                                                   " & vbNewLine _
                                              & "--      ELSE '有'                                                   " & vbNewLine _
                                              & "-- END                                   AS      SAGYO_UMU          " & vbNewLine _
                                              & ",CASE WHEN EXISTS (                                               " & vbNewLine _
                                              & "    SELECT E_SAGYO.NRS_BR_CD                                      " & vbNewLine _
                                              & "    FROM                                                          " & vbNewLine _
                                              & "        $LM_TRN$..E_SAGYO                                         " & vbNewLine _
                                              & "    WHERE                                                         " & vbNewLine _
                                              & "        E_SAGYO.NRS_BR_CD = INKM.NRS_BR_CD                        " & vbNewLine _
                                              & "    AND E_SAGYO.INOUTKA_NO_LM = INKM.INKA_NO_L + INKM.INKA_NO_M   " & vbNewLine _
                                              & "    AND E_SAGYO.IOZS_KB = '11'                                    " & vbNewLine _
                                              & "    AND E_SAGYO.SYS_DEL_FLG = '0')                                " & vbNewLine _
                                              & "      THEN '有'                                                   " & vbNewLine _
                                              & "      ELSE '無'                                                   " & vbNewLine _
                                              & " END                                   AS      SAGYO_UMU          " & vbNewLine _
                                              & "--依頼番号 : 020059 UPD E                                         " & vbNewLine _
                                              & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
                                              & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
                                              & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
                                              & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
                                              & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
                                              & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
                                              & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
                                              & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
                                              & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
                                              & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
                                              & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
                                              & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
                                              & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
                                              & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
                                              & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
                                              & "                                                     ELSE '1'     " & vbNewLine _
                                              & " END                                                 AS EDI_FLG   " & vbNewLine _
                                              & ",'1'                                   AS      UP_KBN             " & vbNewLine _
                                              & ",ISNULL(EDIM.JISSEKI_FLAG,'')          AS      JISSEKI_FLAG       " & vbNewLine _
                                              & ",ISNULL(EDIM.NB_UT , '')               AS      EDI_NB_UT          " & vbNewLine _
                                              & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
                                              & "LEFT  JOIN                                                        " & vbNewLine _
                                              & "(SELECT                                                           " & vbNewLine _
                                              & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
                                              & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
                                              & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
                                              & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                              & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
                                              & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB " & vbNewLine _
                                              & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.ZAIK_IRIME,0) AS ALLOC_CAN_QT " & vbNewLine _
                                              & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB " & vbNewLine _
                                              & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.ZAIK_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
                                              & "FROM                                                              " & vbNewLine _
                                              & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
                                              & "INNER  JOIN                                                       " & vbNewLine _
                                              & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
                                              & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
                                              & "AND   IDO.O_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
                                              & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
                                              & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
                                              & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
                                              & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
                                              & "GROUP BY                                                          " & vbNewLine _
                                              & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
                                              & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
                                              & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
                                              & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
                                              & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
                                              & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
                                              & "         ,IDO.ZAIK_IRIME                                             " & vbNewLine _
                                              & ") ZAIT                                                            " & vbNewLine _
                                              & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
                                              & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
                                              & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
                                              & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
                                              & "LEFT  JOIN                                                        " & vbNewLine _
                                              & "(SELECT                                                           " & vbNewLine _
                                              & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
                                              & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
                                              & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
                                              & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                              & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
                                              & ",SUM(ZAIT2.ALLOC_CAN_NB)  AS ALLOC_CAN_NB                         " & vbNewLine _
                                              & ",SUM(ZAIT2.ALLOC_CAN_QT) AS ALLOC_CAN_QT                          " & vbNewLine _
                                              & ",SUM(ZAIT2.PORA_ZAI_NB) AS PORA_ZAI_NB                            " & vbNewLine _
                                              & ",SUM(ZAIT2.PORA_ZAI_QT) AS PORA_ZAI_QT                            " & vbNewLine _
                                              & "FROM                                                              " & vbNewLine _
                                              & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
                                              & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
                                              & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
                                              & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
                                              & "GROUP BY                                                          " & vbNewLine _
                                              & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
                                              & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
                                              & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
                                              & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
                                              & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
                                              & ") ZAIT2                                                           " & vbNewLine _
                                              & "ON    INKM.NRS_BR_CD                  = ZAIT2.NRS_BR_CD           " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L                  = ZAIT2.INKA_NO_L           " & vbNewLine _
                                              & "AND   INKM.INKA_NO_M                  = ZAIT2.INKA_NO_M           " & vbNewLine _
                                              & "AND   INKM.GOODS_CD_NRS               = ZAIT2.GOODS_CD_NRS        " & vbNewLine _
                                              & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
                                              & "--依頼番号 : 020059 DEL (末尾まで)                                " & vbNewLine _
                                              & "--LEFT  JOIN                                                        " & vbNewLine _
                                              & "--(SELECT DISTINCT                                                  " & vbNewLine _
                                              & "--SAGY2.NRS_BR_CD                                                   " & vbNewLine _
                                              & "--,SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
                                              & "--,SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
                                              & "--FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
                                              & "--WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
                                              & "--AND   SAGY2.IOZS_KB                    = '11'                     " & vbNewLine _
                                              & "--AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
                                              & "--)  SAGY                                                           " & vbNewLine _
                                              & "--ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
                                              & "--AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
                                              & "--AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine

    Private Const SQL_SELECT_INKA_M_PARTS2 As String = "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                " & vbNewLine

    Private Const SQL_SELECT_INKA_M_PARTS3 As String = "LEFT JOIN                                                   " & vbNewLine _
                                              & "(SELECT NRS_BR_CD                                                  " & vbNewLine _
                                              & "       ,INKA_CTL_NO_L                                              " & vbNewLine _
                                              & "       ,INKA_CTL_NO_M                                              " & vbNewLine _
                                              & "       ,SUM(NB) AS NB                                              " & vbNewLine _
                                              & "       ,IRIME                                                      " & vbNewLine _
                                              & "	   ,EDI_CTL_NO                                                  " & vbNewLine _
                                              & "	   ,JISSEKI_FLAG                                                " & vbNewLine _
                                              & "	   ,'' AS NB_UT                                                 " & vbNewLine _
                                              & "	   ,SYS_DEL_FLG                                                 " & vbNewLine _
                                              & " FROM   $LM_TRN$..H_INKAEDI_M                                      " & vbNewLine _
                                              & " GROUP BY NRS_BR_CD,INKA_CTL_NO_L,INKA_CTL_NO_M,IRIME,EDI_CTL_NO,JISSEKI_FLAG,SYS_DEL_FLG) EDIM " & vbNewLine

    Private Const SQL_SELECT_INKA_M_PARTS4 As String = "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
                                              & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
                                              & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
                                              & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
                                              & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
                                              & "--(2016.03.17) コメントアウトSTART                                " & vbNewLine _
                                              & "--AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
                                              & "--(2016.03.17) コメントアウトEND                                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
                                              & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
                                              & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
                                              & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
                                              & "LEFT  JOIN                                                        " & vbNewLine _
                                              & "(SELECT                                                           " & vbNewLine _
                                              & " NRS_BR_CD                                                        " & vbNewLine _
                                              & ",INKA_NO_L                                                        " & vbNewLine _
                                              & ",INKA_NO_M                                                        " & vbNewLine _
                                              & ",SYS_DEL_FLG                                                      " & vbNewLine _
                                              & ",MAX(OUTKA_NO_L) as MAX_OUTKA_NO_L                                " & vbNewLine _
                                              & "FROM $LM_TRN$..C_OUTKA_S                                          " & vbNewLine _
                                              & "GROUP BY                                                          " & vbNewLine _
                                              & "NRS_BR_CD                                                         " & vbNewLine _
                                              & ",INKA_NO_L                                                        " & vbNewLine _
                                              & ",INKA_NO_M                                                        " & vbNewLine _
                                              & ",SYS_DEL_FLG) OUTS                                                " & vbNewLine _
                                              & "ON    INKS.NRS_BR_CD				    = OUTS.NRS_BR_CD           " & vbNewLine _
                                              & "AND   INKS.INKA_NO_L				    = OUTS.INKA_NO_L           " & vbNewLine _
                                              & "AND   INKS.INKA_NO_M				    = OUTS.INKA_NO_M           " & vbNewLine _
                                              & "AND   OUTS.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
                                              & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
                                              & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
                                              & "GROUP BY                                                          " & vbNewLine _
                                              & "          INKM.NRS_BR_CD                                          " & vbNewLine _
                                              & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
                                              & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
                                              & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
                                              & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
                                              & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
                                              & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
                                              & "         ,INKM.REMARK                                             " & vbNewLine _
                                              & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
                                              & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
                                              & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
                                              & "         ,GOOD.NB_UT                                              " & vbNewLine _
                                              & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
                                              & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
                                              & "         ,GOOD.PKG_NB                                             " & vbNewLine _
                                              & "         ,GOOD.NB_UT                                              " & vbNewLine _
                                              & "         ,GOOD.PKG_UT                                             " & vbNewLine _
                                              & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
                                              & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
                                              & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
                                              & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
                                              & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
                                              & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
                                              & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
                                              & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
                                              & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
                                              & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
                                              & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
                                              & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
                                              & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
                                              & "         ,GOOD.TARE_YN                                            " & vbNewLine _
                                              & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
                                              & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
                                              & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
                                              & "         ,EDIM.NB                                                 " & vbNewLine _
                                              & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
                                              & "         ,EDIM.IRIME                                              " & vbNewLine _
                                              & "         ,EDIM.JISSEKI_FLAG                                       " & vbNewLine _
                                              & "         ,EDIM.NB_UT                                              " & vbNewLine _
                                              & "         ,ZAIT2.ALLOC_CAN_NB                                      " & vbNewLine _
                                              & "         ,ZAIT2.ALLOC_CAN_QT                                      " & vbNewLine _
                                              & "         ,OUTS.MAX_OUTKA_NO_L                                     " & vbNewLine _
                                              & "ORDER BY                                                          " & vbNewLine _
                                              & "          INKM.INKA_NO_L                                          " & vbNewLine _
                                              & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
                                              & "         ,INKM.INKA_NO_M                                          " & vbNewLine

    'Private Const SQL_SELECT_INKA_M As String = "SELECT DISTINCT                                                   " & vbNewLine _
    '                                          & " INKM.NRS_BR_CD                        AS      NRS_BR_CD          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_L                        AS      INKA_NO_L          " & vbNewLine _
    '                                          & ",INKM.INKA_NO_M                        AS      INKA_NO_M          " & vbNewLine _
    '                                          & ",INKM.GOODS_CD_NRS                     AS      GOODS_CD_NRS       " & vbNewLine _
    '                                          & ",GOOD.GOODS_CD_CUST                    AS      GOODS_CD_CUST      " & vbNewLine _
    '                                          & ",INKM.OUTKA_FROM_ORD_NO_M              AS      OUTKA_FROM_ORD_NO_M" & vbNewLine _
    '                                          & ",INKM.BUYER_ORD_NO_M                   AS      BUYER_ORD_NO_M     " & vbNewLine _
    '                                          & ",INKM.REMARK                           AS      REMARK             " & vbNewLine _
    '                                          & ",INKM.PRINT_SORT                       AS      PRINT_SORT         " & vbNewLine _
    '                                          & ",GOOD.GOODS_NM_1                       AS      GOODS_NM           " & vbNewLine _
    '                                          & ",GOOD.ONDO_KB                          AS      ONDO_KB            " & vbNewLine _
    '                                          & ",SUM(ISNULL(INKS.KONSU , 0)                                       " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB , 0)                                   " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU   , 0))       AS      SUM_KOSU           " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      NB_UT              " & vbNewLine _
    '                                          & ",GOOD.ONDO_STR_DATE                    AS      ONDO_STR_DATE      " & vbNewLine _
    '                                          & ",GOOD.ONDO_END_DATE                    AS      ONDO_END_DATE      " & vbNewLine _
    '                                          & ",GOOD.PKG_NB                           AS      PKG_NB             " & vbNewLine _
    '                                          & ",GOOD.NB_UT                            AS      PKG_NB_UT1         " & vbNewLine _
    '                                          & ",GOOD.PKG_UT                           AS      PKG_NB_UT2         " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB_M     " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT                     AS      STD_IRIME_UT       " & vbNewLine _
    '                                          & ",SUM((ISNULL(INKS.KONSU , 0)                                      " & vbNewLine _
    '                                          & "      * ISNULL(GOOD.PKG_NB       , 0)                             " & vbNewLine _
    '                                          & "      + ISNULL(INKS.HASU         , 0))                            " & vbNewLine _
    '                                          & "      * ISNULL(INKS.IRIME , 0))                                   " & vbNewLine _
    '                                          & "                                       AS      SUM_SURYO_M        " & vbNewLine _
    '                                          & ",SUM(((ISNULL(INKS.KONSU , 0)                                     " & vbNewLine _
    '                                          & "      *  ISNULL(GOOD.PKG_NB , 0)                                  " & vbNewLine _
    '                                          & "      +  ISNULL(INKS.HASU   , 0))                                 " & vbNewLine _
    '                                          & "      *  ISNULL(INKS.IRIME  , 0))                                 " & vbNewLine _
    '                                          & "      * GOOD.STD_WT_KGS                                           " & vbNewLine _
    '                                          & "      / GOOD.STD_IRIME_NB)                                        " & vbNewLine _
    '                                          & "                                       AS      SUM_JURYO_M        " & vbNewLine _
    '                                          & ",GOOD.SHOBO_CD                         AS      SHOBO_CD           " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(ZAIT.ALLOC_CAN_NB,ZAIT2.ALLOC_CAN_NB) <> SUM(ISNULL(INKS.KONSU , 0) " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "        OR ISNULL(ZAIT.ALLOC_CAN_QT,ZAIT2.ALLOC_CAN_QT) <> SUM((ISNULL(INKS.KONSU , 0) " & vbNewLine _
    '                                          & "                               * ISNULL(GOOD.PKG_NB , 0)          " & vbNewLine _
    '                                          & "                               + ISNULL(INKS.HASU , 0))           " & vbNewLine _
    '                                          & "                               * ISNULL(INKS.IRIME , 0))          " & vbNewLine _
    '                                          & "      THEN '済'                                                   " & vbNewLine _
    '                                          & "      ELSE '未'                                                   " & vbNewLine _
    '                                          & " END                                   AS      HIKIATE            " & vbNewLine _
    '                                          & ",CASE WHEN COUNT(SAGY.NRS_BR_CD) = 0                              " & vbNewLine _
    '                                          & "      THEN '無'                                                   " & vbNewLine _
    '                                          & "      ELSE '有'                                                   " & vbNewLine _
    '                                          & " END                                   AS      SAGYO_UMU          " & vbNewLine _
    '                                          & ",INKM.SYS_DEL_FLG                      AS      SYS_DEL_FLG        " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                   AS      EDI_KOSU           " & vbNewLine _
    '                                          & ",ISNULL(EDIM.NB , 0)                                              " & vbNewLine _
    '                                          & "     * ISNULL(EDIM.IRIME , 0)          AS      EDI_SURYO          " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_NB                     AS      STD_IRIME_NB       " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS                       AS      STD_WT_KGS         " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_L                        AS      CUST_CD_L          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_M                        AS      CUST_CD_M          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_S                        AS      CUST_CD_S          " & vbNewLine _
    '                                          & ",GOOD.CUST_CD_SS                       AS      CUST_CD_SS         " & vbNewLine _
    '                                          & ",GOOD.TARE_YN                          AS      TARE_YN            " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB                       AS      LOT_CTL_KB         " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB                   AS      LT_DATE_CTL_KB     " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB                  AS      CRT_DATE_CTL_KB    " & vbNewLine _
    '                                          & ",CASE WHEN ISNULL(EDIM.EDI_CTL_NO,'')  = ''      THEN '0'         " & vbNewLine _
    '                                          & "                                                     ELSE '1'     " & vbNewLine _
    '                                          & " END                                                 AS EDI_FLG   " & vbNewLine _
    '                                          & ",'1'                                   AS      UP_KBN             " & vbNewLine _
    '                                          & ",ISNULL(EDIM.JISSEKI_FLAG,'')          AS      JISSEKI_FLAG       " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M           INKM                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S           INKS                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = INKS.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = INKS.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = INKS.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS ALLOC_CAN_NB " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.ZAIK_IRIME,0) AS ALLOC_CAN_QT " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) - ISNULL(IDO.N_ALCTD_NB,0) AS PORA_ZAI_NB " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) - ISNULL(IDO.N_ALCTD_NB,0) * ISNULL(IDO.ZAIK_IRIME,0) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "INNER  JOIN                                                       " & vbNewLine _
    '                                          & "$LM_TRN$..D_IDO_TRS          IDO                                  " & vbNewLine _
    '                                          & "ON    IDO.NRS_BR_CD                  = ZAIT2.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   IDO.O_ZAI_REC_NO               = ZAIT2.ZAI_REC_NO           " & vbNewLine _
    '                                          & "AND   IDO.SYS_DEL_FLG                = '0'                        " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & "         ,IDO.N_ALCTD_NB                                          " & vbNewLine _
    '                                          & "         ,IDO.ZAIK_IRIME                                             " & vbNewLine _
    '                                          & ") ZAIT                                                            " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT.INKA_NO_L            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT.INKA_NO_M            " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT.GOODS_CD_NRS         " & vbNewLine _
    '                                          & "AND   ZAIT.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT                                                           " & vbNewLine _
    '                                          & " ZAIT2.NRS_BR_CD AS NRS_BR_CD                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_L AS INKA_NO_L                                     " & vbNewLine _
    '                                          & ",ZAIT2.INKA_NO_M AS INKA_NO_M                                     " & vbNewLine _
    '                                          & ",ZAIT2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
    '                                          & ",ZAIT2.SYS_DEL_FLG AS SYS_DEL_FLG                                 " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_NB)  AS ALLOC_CAN_NB                         " & vbNewLine _
    '                                          & ",SUM(ZAIT2.ALLOC_CAN_QT) AS ALLOC_CAN_QT                          " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_NB) AS PORA_ZAI_NB                            " & vbNewLine _
    '                                          & ",SUM(ZAIT2.PORA_ZAI_QT) AS PORA_ZAI_QT                            " & vbNewLine _
    '                                          & "FROM                                                              " & vbNewLine _
    '                                          & "$LM_TRN$..D_ZAI_TRS          ZAIT2                                " & vbNewLine _
    '                                          & "WHERE ZAIT2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   ZAIT2.INKA_NO_L                  = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          ZAIT2.NRS_BR_CD                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_L                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.INKA_NO_M                                         " & vbNewLine _
    '                                          & "         ,ZAIT2.GOODS_CD_NRS                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.SYS_DEL_FLG                                       " & vbNewLine _
    '                                          & ") ZAIT2                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = ZAIT2.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                  = ZAIT2.INKA_NO_L           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                  = ZAIT2.INKA_NO_M           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS               = ZAIT2.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "AND   ZAIT2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN                                                        " & vbNewLine _
    '                                          & "(SELECT DISTINCT                                                  " & vbNewLine _
    '                                          & "SAGY2.NRS_BR_CD                                                   " & vbNewLine _
    '                                          & ",SAGY2.INOUTKA_NO_LM                                              " & vbNewLine _
    '                                          & ",SAGY2.SYS_DEL_FLG                                                " & vbNewLine _
    '                                          & "FROM  $LM_TRN$..E_SAGYO            SAGY2                          " & vbNewLine _
    '                                          & "WHERE SAGY2.NRS_BR_CD                  = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   SAGY2.IOZS_KB                    = '11'                     " & vbNewLine _
    '                                          & "AND   SAGY2.SYS_DEL_FLG                = '0'                      " & vbNewLine _
    '                                          & ")  SAGY                                                           " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                  = SAGY.NRS_BR_CD            " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L + INKM.INKA_NO_M = SAGY.INOUTKA_NO_LM        " & vbNewLine _
    '                                          & "AND   SAGY.SYS_DEL_FLG                = '0'                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..H_INKAEDI_M        EDIM                      " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = EDIM.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = EDIM.INKA_CTL_NO_L       " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M                   = EDIM.INKA_CTL_NO_M       " & vbNewLine _
    '                                          & "AND   EDIM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS             GOOD                     " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD                   = GOOD.NRS_BR_CD           " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS                = GOOD.GOODS_CD_NRS        " & vbNewLine _
    '                                          & "--(2016.03.17) コメントアウトSTART                                " & vbNewLine _
    '                                          & "--AND   GOOD.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "--(2016.03.17) コメントアウトEND                                  " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD                   = @NRS_BR_CD               " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L                   = @INKA_NO_L               " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG                 = '0'                      " & vbNewLine _
    '                                          & "GROUP BY                                                          " & vbNewLine _
    '                                          & "          INKM.NRS_BR_CD                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine _
    '                                          & "         ,INKM.GOODS_CD_NRS                                       " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_CD_CUST                                      " & vbNewLine _
    '                                          & "         ,INKM.OUTKA_FROM_ORD_NO_M                                " & vbNewLine _
    '                                          & "         ,INKM.BUYER_ORD_NO_M                                     " & vbNewLine _
    '                                          & "         ,INKM.REMARK                                             " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,GOOD.GOODS_NM_1                                         " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_KB                                            " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_STR_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.ONDO_END_DATE                                      " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                                             " & vbNewLine _
    '                                          & "         ,GOOD.NB_UT                                              " & vbNewLine _
    '                                          & "         ,GOOD.PKG_UT                                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                                       " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                                       " & vbNewLine _
    '                                          & "         ,GOOD.SHOBO_CD                                           " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                                         " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_NB                                       " & vbNewLine _
    '                                          & "         ,ZAIT.ALLOC_CAN_QT                                       " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_NB                                        " & vbNewLine _
    '                                          & "         ,ZAIT.PORA_ZAI_QT                                        " & vbNewLine _
    '                                          & "         ,INKM.SYS_DEL_FLG                                        " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_L                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_M                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_S                                          " & vbNewLine _
    '                                          & "         ,GOOD.CUST_CD_SS                                         " & vbNewLine _
    '                                          & "         ,GOOD.TARE_YN                                            " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                                         " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                                     " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                                    " & vbNewLine _
    '                                          & "         ,EDIM.NB                                                 " & vbNewLine _
    '                                          & "         ,EDIM.EDI_CTL_NO                                         " & vbNewLine _
    '                                          & "         ,EDIM.IRIME                                              " & vbNewLine _
    '                                          & "         ,EDIM.JISSEKI_FLAG                                       " & vbNewLine _
    '                                          & "         ,ZAIT2.ALLOC_CAN_NB                                      " & vbNewLine _
    '                                          & "         ,ZAIT2.ALLOC_CAN_QT                                      " & vbNewLine _
    '                                          & "ORDER BY                                                          " & vbNewLine _
    '                                          & "          INKM.INKA_NO_L                                          " & vbNewLine _
    '                                          & "         ,INKM.PRINT_SORT                                         " & vbNewLine _
    '                                          & "         ,INKM.INKA_NO_M                                          " & vbNewLine
    '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 end start
    'END YANAI 要望番号1039 入荷で、作業を後から削除した際に、表示した直後は『有』と表示され、Spreadをダブルクリックすると、無になる
    'END YANAI 要望番号899
    'END YANAI 要望番号788
    'END YANAI 要望番号573
    'END YANAI 要望番号496
    'END YANAI メモ②No.20

    Private Const SQL_SELECT_CUST_DTL As String = "SELECT                                       " & vbNewLine _
                                                & "     SET_NAIYO                               " & vbNewLine _
                                                & "    ,SET_NAIYO_2                             " & vbNewLine _
                                                & "    ,SET_NAIYO_2                             " & vbNewLine _
                                                & "    ,REMARK                                  " & vbNewLine _
                                                & "FROM                                         " & vbNewLine _
                                                & "    $LM_MST$..M_CUST_DETAILS                 " & vbNewLine _
                                                & "WHERE NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                                & "  AND CUST_CD = @CUST_CD_L + @CUST_CD_M      " & vbNewLine _
                                                & "  AND SUB_KB = '9D'                          " & vbNewLine

#End Region

#Region "INKA_S"

    'UPD 2022/11/07 倉庫写真アプリ対応 START
    'Private Const SQL_SELECT_INKA_S As String = "SELECT                                          " & vbNewLine _
    '                                          & " INKS.NRS_BR_CD             AS NRS_BR_CD        " & vbNewLine _
    '                                          & ",INKS.INKA_NO_L             AS INKA_NO_L        " & vbNewLine _
    '                                          & ",INKS.INKA_NO_M             AS INKA_NO_M        " & vbNewLine _
    '                                          & ",INKS.INKA_NO_S             AS INKA_NO_S        " & vbNewLine _
    '                                          & ",INKS.ZAI_REC_NO            AS ZAI_REC_NO       " & vbNewLine _
    '                                          & ",INKS.LOT_NO                AS LOT_NO           " & vbNewLine _
    '                                          & ",INKS.LOCA                  AS LOCA             " & vbNewLine _
    '                                          & ",INKS.TOU_NO                AS TOU_NO           " & vbNewLine _
    '                                          & ",INKS.SITU_NO               AS SITU_NO          " & vbNewLine _
    '                                          & ",INKS.ZONE_CD               AS ZONE_CD          " & vbNewLine _
    '                                          & ",INKS.KONSU                 AS KONSU            " & vbNewLine _
    '                                          & ",INKS.HASU                  AS HASU             " & vbNewLine _
    '                                          & ",INKS.IRIME                 AS IRIME            " & vbNewLine _
    '                                          & ",INKS.BETU_WT               AS BETU_WT          " & vbNewLine _
    '                                          & ",INKS.SERIAL_NO             AS SERIAL_NO        " & vbNewLine _
    '                                          & ",INKS.GOODS_COND_KB_1       AS GOODS_COND_KB_1  " & vbNewLine _
    '                                          & ",INKS.GOODS_COND_KB_2       AS GOODS_COND_KB_2  " & vbNewLine _
    '                                          & ",INKS.GOODS_COND_KB_3       AS GOODS_COND_KB_3  " & vbNewLine _
    '                                          & ",INKS.GOODS_CRT_DATE        AS GOODS_CRT_DATE   " & vbNewLine _
    '                                          & ",INKS.LT_DATE               AS LT_DATE          " & vbNewLine _
    '                                          & ",INKS.SPD_KB                AS SPD_KB           " & vbNewLine _
    '                                          & ",INKS.OFB_KB                AS OFB_KB           " & vbNewLine _
    '                                          & ",INKS.DEST_CD               AS DEST_CD          " & vbNewLine _
    '                                          & ",INKS.REMARK                AS REMARK           " & vbNewLine _
    '                                          & ",INKS.ALLOC_PRIORITY        AS ALLOC_PRIORITY   " & vbNewLine _
    '                                          & ",INKS.REMARK_OUT            AS REMARK_OUT       " & vbNewLine _
    '                                          & ",INKS.SYS_DEL_FLG           AS SYS_DEL_FLG      " & vbNewLine _
    '                                          & ",GOOD.STD_IRIME_UT          AS STD_IRIME_UT     " & vbNewLine _
    '                                          & ",KBN1.KBN_NM1               AS STD_IRIME_NM     " & vbNewLine _
    '                                          & ",INKS.KONSU                                     " & vbNewLine _
    '                                          & "     * GOOD.PKG_NB                              " & vbNewLine _
    '                                          & "     + INKS.HASU            AS KOSU_S           " & vbNewLine _
    '                                          & ",(INKS.KONSU                                    " & vbNewLine _
    '                                          & "     * GOOD.PKG_NB                              " & vbNewLine _
    '                                          & "     + INKS.HASU)                               " & vbNewLine _
    '                                          & "     * INKS.IRIME           AS SURYO_S          " & vbNewLine _
    '                                          & ",(INKS.KONSU                                    " & vbNewLine _
    '                                          & "     * GOOD.PKG_NB                              " & vbNewLine _
    '                                          & "     + INKS.HASU)                               " & vbNewLine _
    '                                          & "     * INKS.IRIME                               " & vbNewLine _
    '                                          & "     * GOOD.STD_WT_KGS                          " & vbNewLine _
    '                                          & "     / GOOD.STD_IRIME_NB    AS JURYO_S          " & vbNewLine _
    '                                          & ",GOOD.STD_WT_KGS            AS STD_WT_KGS       " & vbNewLine _
    '                                          & ",DEST.DEST_NM               AS DEST_NM          " & vbNewLine _
    '                                          & ",SUM(INKS.KONSU)            AS SUM_KONSU_S      " & vbNewLine _
    '                                          & ",GOOD.LOT_CTL_KB            AS LOT_CTL_KB       " & vbNewLine _
    '                                          & ",GOOD.LT_DATE_CTL_KB        AS LT_DATE_CTL_KB   " & vbNewLine _
    '                                          & ",GOOD.CRT_DATE_CTL_KB       AS CRT_DATE_CTL_KB  " & vbNewLine _
    '                                          & ",(SELECT                                        " & vbNewLine _
    '                                          & "        COUNT(REC_NO)       AS REC_CNT          " & vbNewLine _
    '                                          & "  FROM  $LM_TRN$..D_IDO_TRS                     " & vbNewLine _
    '                                          & "  WHERE NRS_BR_CD    = INKS.NRS_BR_CD           " & vbNewLine _
    '                                          & "     AND (O_ZAI_REC_NO = INKS.ZAI_REC_NO        " & vbNewLine _
    '                                          & "     OR N_ZAI_REC_NO = INKS.ZAI_REC_NO)         " & vbNewLine _
    '                                          & "    AND SYS_DEL_FLG  = '0'                      " & vbNewLine _
    '                                          & " )                          AS ZAI_REC_CNT      " & vbNewLine _
    '                                          & ",'1'                        AS UP_KBN           " & vbNewLine _
    '                                          & ", 0                         AS EXISTS_REMARK    " & vbNewLine _
    '                                          & ", INKS.BUG_YN               AS BUG_YN           " & vbNewLine _
    '                                          & ", CASE WHEN ISNULL(MFL.CNT,0) = 0 THEN  '00'    " & vbNewLine _
    '                                          & "       ELSE '01'                                " & vbNewLine _
    '                                          & "  END                       AS IMG_YN           " & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_L INKL              " & vbNewLine _
    '                                          & "INNER JOIN $LM_TRN$..B_INKA_M INKM              " & vbNewLine _
    '                                          & "ON    INKL.NRS_BR_CD        = INKM.NRS_BR_CD    " & vbNewLine _
    '                                          & "AND   INKL.INKA_NO_L        = INKM.INKA_NO_L    " & vbNewLine _
    '                                          & "AND   INKM.SYS_DEL_FLG      = '0'               " & vbNewLine _
    '                                          & "INNER JOIN $LM_TRN$..B_INKA_S INKS              " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD        = INKS.NRS_BR_CD    " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L        = INKS.INKA_NO_L    " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_M        = INKS.INKA_NO_M    " & vbNewLine _
    '                                          & "AND   INKS.SYS_DEL_FLG      = '0'               " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_GOODS  GOOD              " & vbNewLine _
    '                                          & "ON    INKS.NRS_BR_CD        = GOOD.NRS_BR_CD    " & vbNewLine _
    '                                          & "AND   INKM.GOODS_CD_NRS     = GOOD.GOODS_CD_NRS " & vbNewLine _
    '                                          & "--(2016.03.17) コメントアウトSTART              " & vbNewLine _
    '                                          & "--AND   GOOD.SYS_DEL_FLG      = '0'             " & vbNewLine _
    '                                          & "--(2016.03.17) コメントアウトEND                " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..M_DEST   DEST              " & vbNewLine _
    '                                          & "ON    INKS.NRS_BR_CD        = DEST.NRS_BR_CD    " & vbNewLine _
    '                                          & "AND   INKL.CUST_CD_L        = DEST.CUST_CD_L    " & vbNewLine _
    '                                          & "AND   INKS.DEST_CD          = DEST.DEST_CD      " & vbNewLine _
    '                                          & "AND   DEST.SYS_DEL_FLG      = '0'               " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_MST$..Z_KBN    KBN1              " & vbNewLine _
    '                                          & "ON    GOOD.STD_IRIME_UT     = KBN1.KBN_CD       " & vbNewLine _
    '                                          & "AND   KBN1.KBN_GROUP_CD     = 'I001'            " & vbNewLine _
    '                                          & "AND   KBN1.SYS_DEL_FLG      = '0'               " & vbNewLine _
    '                                          & "LEFT  JOIN (                                    " & vbNewLine _
    '                                          & "    SELECT KEY_NO,COUNT(*)AS CNT                " & vbNewLine _
    '                                          & "    FROM   LM_MST..M_FILE                       " & vbNewLine _
    '                                          & "    WHERE                                       " & vbNewLine _
    '                                          & "        ENT_SYSID_KBN = '06'                    " & vbNewLine _
    '                                          & "    AND SYS_DEL_FLG = '0'                       " & vbNewLine _
    '                                          & "	GROUP BY KEY_NO                              " & vbNewLine _
    '                                          & ")AS MFL                                         " & vbNewLine _
    '                                          & "ON MFL.KEY_NO = INKS.INKA_NO_L                  " & vbNewLine _
    '                                          & "              + INKS.INKA_NO_M                  " & vbNewLine _
    '                                          & "              + INKS.INKA_NO_S                  " & vbNewLine _
    '                                          & "WHERE INKL.NRS_BR_CD        = @NRS_BR_CD        " & vbNewLine _
    '                                          & "AND   INKL.INKA_NO_L        = @INKA_NO_L        " & vbNewLine _
    '                                          & "AND   INKL.SYS_DEL_FLG      = '0'               " & vbNewLine _
    '                                          & "GROUP BY                                        " & vbNewLine _
    '                                          & "          INKS.NRS_BR_CD                        " & vbNewLine _
    '                                          & "         ,INKS.INKA_NO_L                        " & vbNewLine _
    '                                          & "         ,INKS.INKA_NO_M                        " & vbNewLine _
    '                                          & "         ,INKS.INKA_NO_S                        " & vbNewLine _
    '                                          & "         ,INKS.ZAI_REC_NO                       " & vbNewLine _
    '                                          & "         ,INKS.LOT_NO                           " & vbNewLine _
    '                                          & "         ,INKS.LOCA                             " & vbNewLine _
    '                                          & "         ,INKS.TOU_NO                           " & vbNewLine _
    '                                          & "         ,INKS.SITU_NO                          " & vbNewLine _
    '                                          & "         ,INKS.ZONE_CD                          " & vbNewLine _
    '                                          & "         ,INKS.KONSU                            " & vbNewLine _
    '                                          & "         ,INKS.HASU                             " & vbNewLine _
    '                                          & "         ,INKS.IRIME                            " & vbNewLine _
    '                                          & "         ,INKS.BETU_WT                          " & vbNewLine _
    '                                          & "         ,INKS.SERIAL_NO                        " & vbNewLine _
    '                                          & "         ,INKS.GOODS_COND_KB_1                  " & vbNewLine _
    '                                          & "         ,INKS.GOODS_COND_KB_2                  " & vbNewLine _
    '                                          & "         ,INKS.GOODS_COND_KB_3                  " & vbNewLine _
    '                                          & "         ,INKS.GOODS_CRT_DATE                   " & vbNewLine _
    '                                          & "         ,INKS.LT_DATE                          " & vbNewLine _
    '                                          & "         ,INKS.SPD_KB                           " & vbNewLine _
    '                                          & "         ,INKS.OFB_KB                           " & vbNewLine _
    '                                          & "         ,INKS.DEST_CD                          " & vbNewLine _
    '                                          & "         ,INKS.REMARK                           " & vbNewLine _
    '                                          & "         ,INKS.ALLOC_PRIORITY                   " & vbNewLine _
    '                                          & "         ,INKS.REMARK_OUT                       " & vbNewLine _
    '                                          & "         ,INKS.SYS_DEL_FLG                      " & vbNewLine _
    '                                          & "         ,INKS.KONSU                            " & vbNewLine _
    '                                          & "         ,GOOD.PKG_NB                           " & vbNewLine _
    '                                          & "         ,INKS.HASU                             " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_NB                     " & vbNewLine _
    '                                          & "         ,GOOD.STD_IRIME_UT                     " & vbNewLine _
    '                                          & "         ,KBN1.KBN_NM1                          " & vbNewLine _
    '                                          & "         ,GOOD.STD_WT_KGS                       " & vbNewLine _
    '                                          & "         ,DEST.DEST_NM                          " & vbNewLine _
    '                                          & "         ,GOOD.LOT_CTL_KB                       " & vbNewLine _
    '                                          & "         ,GOOD.LT_DATE_CTL_KB                   " & vbNewLine _
    '                                          & "         ,GOOD.CRT_DATE_CTL_KB                  " & vbNewLine _
    '                                          & "         ,INKS.BUG_YN                           " & vbNewLine _
    '                                          & "         ,MFL.CNT                               " & vbNewLine _
    '                                          & "ORDER BY                                        " & vbNewLine _
    '                                          & "          INKS.INKA_NO_L                        " & vbNewLine _
    '                                          & "         ,INKS.INKA_NO_M                        " & vbNewLine _
    '                                          & "         ,INKS.INKA_NO_S                        " & vbNewLine
    Private Const SQL_SELECT_INKA_S As String = "SELECT                                          " & vbNewLine _
                                              & " INKS.NRS_BR_CD             AS NRS_BR_CD        " & vbNewLine _
                                              & ",INKS.INKA_NO_L             AS INKA_NO_L        " & vbNewLine _
                                              & ",INKS.INKA_NO_M             AS INKA_NO_M        " & vbNewLine _
                                              & ",INKS.INKA_NO_S             AS INKA_NO_S        " & vbNewLine _
                                              & ",INKS.ZAI_REC_NO            AS ZAI_REC_NO       " & vbNewLine _
                                              & ",INKS.LOT_NO                AS LOT_NO           " & vbNewLine _
                                              & ",INKS.LOCA                  AS LOCA             " & vbNewLine _
                                              & ",INKS.TOU_NO                AS TOU_NO           " & vbNewLine _
                                              & ",INKS.SITU_NO               AS SITU_NO          " & vbNewLine _
                                              & ",INKS.ZONE_CD               AS ZONE_CD          " & vbNewLine _
                                              & ",INKS.KONSU                 AS KONSU            " & vbNewLine _
                                              & ",INKS.HASU                  AS HASU             " & vbNewLine _
                                              & ",INKS.IRIME                 AS IRIME            " & vbNewLine _
                                              & ",INKS.BETU_WT               AS BETU_WT          " & vbNewLine _
                                              & ",INKS.SERIAL_NO             AS SERIAL_NO        " & vbNewLine _
                                              & ",INKS.GOODS_COND_KB_1       AS GOODS_COND_KB_1  " & vbNewLine _
                                              & ",INKS.GOODS_COND_KB_2       AS GOODS_COND_KB_2  " & vbNewLine _
                                              & ",INKS.GOODS_COND_KB_3       AS GOODS_COND_KB_3  " & vbNewLine _
                                              & ",INKS.GOODS_CRT_DATE        AS GOODS_CRT_DATE   " & vbNewLine _
                                              & ",INKS.LT_DATE               AS LT_DATE          " & vbNewLine _
                                              & ",INKS.SPD_KB                AS SPD_KB           " & vbNewLine _
                                              & ",INKS.OFB_KB                AS OFB_KB           " & vbNewLine _
                                              & ",INKS.DEST_CD               AS DEST_CD          " & vbNewLine _
                                              & ",INKS.REMARK                AS REMARK           " & vbNewLine _
                                              & ",INKS.ALLOC_PRIORITY        AS ALLOC_PRIORITY   " & vbNewLine _
                                              & ",INKS.REMARK_OUT            AS REMARK_OUT       " & vbNewLine _
                                              & ",INKS.SYS_DEL_FLG           AS SYS_DEL_FLG      " & vbNewLine _
                                              & ",GOOD.STD_IRIME_UT          AS STD_IRIME_UT     " & vbNewLine _
                                              & ",KBN1.KBN_NM1               AS STD_IRIME_NM     " & vbNewLine _
                                              & ",INKS.KONSU                                     " & vbNewLine _
                                              & "     * GOOD.PKG_NB                              " & vbNewLine _
                                              & "     + INKS.HASU            AS KOSU_S           " & vbNewLine _
                                              & ",(INKS.KONSU                                    " & vbNewLine _
                                              & "     * GOOD.PKG_NB                              " & vbNewLine _
                                              & "     + INKS.HASU)                               " & vbNewLine _
                                              & "     * INKS.IRIME           AS SURYO_S          " & vbNewLine _
                                              & ",(INKS.KONSU                                    " & vbNewLine _
                                              & "     * GOOD.PKG_NB                              " & vbNewLine _
                                              & "     + INKS.HASU)                               " & vbNewLine _
                                              & "     * INKS.IRIME                               " & vbNewLine _
                                              & "     * GOOD.STD_WT_KGS                          " & vbNewLine _
                                              & "     / GOOD.STD_IRIME_NB    AS JURYO_S          " & vbNewLine _
                                              & ",GOOD.STD_WT_KGS            AS STD_WT_KGS       " & vbNewLine _
                                              & ",DEST.DEST_NM               AS DEST_NM          " & vbNewLine _
                                              & ",SUM(INKS.KONSU)            AS SUM_KONSU_S      " & vbNewLine _
                                              & ",GOOD.LOT_CTL_KB            AS LOT_CTL_KB       " & vbNewLine _
                                              & ",GOOD.LT_DATE_CTL_KB        AS LT_DATE_CTL_KB   " & vbNewLine _
                                              & ",GOOD.CRT_DATE_CTL_KB       AS CRT_DATE_CTL_KB  " & vbNewLine _
                                              & ",(SELECT                                        " & vbNewLine _
                                              & "        COUNT(REC_NO)       AS REC_CNT          " & vbNewLine _
                                              & "  FROM  $LM_TRN$..D_IDO_TRS                     " & vbNewLine _
                                              & "  WHERE NRS_BR_CD    = INKS.NRS_BR_CD           " & vbNewLine _
                                              & "     AND (O_ZAI_REC_NO = INKS.ZAI_REC_NO        " & vbNewLine _
                                              & "     OR N_ZAI_REC_NO = INKS.ZAI_REC_NO)         " & vbNewLine _
                                              & "    AND SYS_DEL_FLG  = '0'                      " & vbNewLine _
                                              & " )                          AS ZAI_REC_CNT      " & vbNewLine _
                                              & ",'1'                        AS UP_KBN           " & vbNewLine _
                                              & ", 0                         AS EXISTS_REMARK    " & vbNewLine _
                                              & ", INKS.BUG_YN               AS BUG_YN           " & vbNewLine _
                                              & ", CASE WHEN ISNULL(MFL.CNT,0) = 0 THEN  '00'    " & vbNewLine _
                                              & "       ELSE '01'                                " & vbNewLine _
                                              & "  END                       AS IMG_YN           " & vbNewLine _
                                              & ", CASE WHEN PHOTO.INKA_NO_L IS NULL THEN  '00'  " & vbNewLine _
                                              & "       ELSE '01'                                " & vbNewLine _
                                              & "  END                       AS PHOTO_YN         " & vbNewLine _
                                              & "FROM       $LM_TRN$..B_INKA_L INKL              " & vbNewLine _
                                              & "INNER JOIN $LM_TRN$..B_INKA_M INKM              " & vbNewLine _
                                              & "ON    INKL.NRS_BR_CD        = INKM.NRS_BR_CD    " & vbNewLine _
                                              & "AND   INKL.INKA_NO_L        = INKM.INKA_NO_L    " & vbNewLine _
                                              & "AND   INKM.SYS_DEL_FLG      = '0'               " & vbNewLine _
                                              & "INNER JOIN $LM_TRN$..B_INKA_S INKS              " & vbNewLine _
                                              & "ON    INKM.NRS_BR_CD        = INKS.NRS_BR_CD    " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L        = INKS.INKA_NO_L    " & vbNewLine _
                                              & "AND   INKM.INKA_NO_M        = INKS.INKA_NO_M    " & vbNewLine _
                                              & "AND   INKS.SYS_DEL_FLG      = '0'               " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_GOODS  GOOD              " & vbNewLine _
                                              & "ON    INKS.NRS_BR_CD        = GOOD.NRS_BR_CD    " & vbNewLine _
                                              & "AND   INKM.GOODS_CD_NRS     = GOOD.GOODS_CD_NRS " & vbNewLine _
                                              & "--(2016.03.17) コメントアウトSTART              " & vbNewLine _
                                              & "--AND   GOOD.SYS_DEL_FLG      = '0'             " & vbNewLine _
                                              & "--(2016.03.17) コメントアウトEND                " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_DEST   DEST              " & vbNewLine _
                                              & "ON    INKS.NRS_BR_CD        = DEST.NRS_BR_CD    " & vbNewLine _
                                              & "AND   INKL.CUST_CD_L        = DEST.CUST_CD_L    " & vbNewLine _
                                              & "AND   INKS.DEST_CD          = DEST.DEST_CD      " & vbNewLine _
                                              & "AND   DEST.SYS_DEL_FLG      = '0'               " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..Z_KBN    KBN1              " & vbNewLine _
                                              & "ON    GOOD.STD_IRIME_UT     = KBN1.KBN_CD       " & vbNewLine _
                                              & "AND   KBN1.KBN_GROUP_CD     = 'I001'            " & vbNewLine _
                                              & "AND   KBN1.SYS_DEL_FLG      = '0'               " & vbNewLine _
                                              & "LEFT  JOIN (                                    " & vbNewLine _
                                              & "    SELECT KEY_NO,COUNT(*)AS CNT                " & vbNewLine _
                                              & "    FROM   LM_MST..M_FILE                       " & vbNewLine _
                                              & "    WHERE                                       " & vbNewLine _
                                              & "        ENT_SYSID_KBN = '06'                    " & vbNewLine _
                                              & "    AND SYS_DEL_FLG = '0'                       " & vbNewLine _
                                              & "	GROUP BY KEY_NO                              " & vbNewLine _
                                              & ")AS MFL                                         " & vbNewLine _
                                              & "ON MFL.KEY_NO = INKS.INKA_NO_L                  " & vbNewLine _
                                              & "              + INKS.INKA_NO_M                  " & vbNewLine _
                                              & "              + INKS.INKA_NO_S                  " & vbNewLine _
                                              & "LEFT  JOIN (                                    " & vbNewLine _
                                              & "    SELECT NRS_BR_CD                            " & vbNewLine _
                                              & "          ,INKA_NO_L                            " & vbNewLine _
                                              & "          ,INKA_NO_M                            " & vbNewLine _
                                              & "          ,INKA_NO_S                            " & vbNewLine _
                                              & "    FROM   $LM_TRN$..B_INKA_PHOTO               " & vbNewLine _
                                              & "    WHERE                                       " & vbNewLine _
                                              & "        NRS_BR_CD   = @NRS_BR_CD                " & vbNewLine _
                                              & "    AND INKA_NO_L   = @INKA_NO_L                " & vbNewLine _
                                              & "    AND SYS_DEL_FLG = '0'                       " & vbNewLine _
                                              & "   GROUP BY NRS_BR_CD                           " & vbNewLine _
                                              & "           ,INKA_NO_L                           " & vbNewLine _
                                              & "           ,INKA_NO_M                           " & vbNewLine _
                                              & "           ,INKA_NO_S                           " & vbNewLine _
                                              & ")AS PHOTO                                       " & vbNewLine _
                                              & "ON  PHOTO.NRS_BR_CD = INKS.NRS_BR_CD            " & vbNewLine _
                                              & "AND PHOTO.INKA_NO_L = INKS.INKA_NO_L            " & vbNewLine _
                                              & "AND PHOTO.INKA_NO_M = INKS.INKA_NO_M            " & vbNewLine _
                                              & "AND PHOTO.INKA_NO_S = INKS.INKA_NO_S            " & vbNewLine _
                                              & "WHERE INKL.NRS_BR_CD        = @NRS_BR_CD        " & vbNewLine _
                                              & "AND   INKL.INKA_NO_L        = @INKA_NO_L        " & vbNewLine _
                                              & "AND   INKL.SYS_DEL_FLG      = '0'               " & vbNewLine _
                                              & "GROUP BY                                        " & vbNewLine _
                                              & "          INKS.NRS_BR_CD                        " & vbNewLine _
                                              & "         ,INKS.INKA_NO_L                        " & vbNewLine _
                                              & "         ,INKS.INKA_NO_M                        " & vbNewLine _
                                              & "         ,INKS.INKA_NO_S                        " & vbNewLine _
                                              & "         ,INKS.ZAI_REC_NO                       " & vbNewLine _
                                              & "         ,INKS.LOT_NO                           " & vbNewLine _
                                              & "         ,INKS.LOCA                             " & vbNewLine _
                                              & "         ,INKS.TOU_NO                           " & vbNewLine _
                                              & "         ,INKS.SITU_NO                          " & vbNewLine _
                                              & "         ,INKS.ZONE_CD                          " & vbNewLine _
                                              & "         ,INKS.KONSU                            " & vbNewLine _
                                              & "         ,INKS.HASU                             " & vbNewLine _
                                              & "         ,INKS.IRIME                            " & vbNewLine _
                                              & "         ,INKS.BETU_WT                          " & vbNewLine _
                                              & "         ,INKS.SERIAL_NO                        " & vbNewLine _
                                              & "         ,INKS.GOODS_COND_KB_1                  " & vbNewLine _
                                              & "         ,INKS.GOODS_COND_KB_2                  " & vbNewLine _
                                              & "         ,INKS.GOODS_COND_KB_3                  " & vbNewLine _
                                              & "         ,INKS.GOODS_CRT_DATE                   " & vbNewLine _
                                              & "         ,INKS.LT_DATE                          " & vbNewLine _
                                              & "         ,INKS.SPD_KB                           " & vbNewLine _
                                              & "         ,INKS.OFB_KB                           " & vbNewLine _
                                              & "         ,INKS.DEST_CD                          " & vbNewLine _
                                              & "         ,INKS.REMARK                           " & vbNewLine _
                                              & "         ,INKS.ALLOC_PRIORITY                   " & vbNewLine _
                                              & "         ,INKS.REMARK_OUT                       " & vbNewLine _
                                              & "         ,INKS.SYS_DEL_FLG                      " & vbNewLine _
                                              & "         ,INKS.KONSU                            " & vbNewLine _
                                              & "         ,GOOD.PKG_NB                           " & vbNewLine _
                                              & "         ,INKS.HASU                             " & vbNewLine _
                                              & "         ,GOOD.STD_IRIME_NB                     " & vbNewLine _
                                              & "         ,GOOD.STD_IRIME_UT                     " & vbNewLine _
                                              & "         ,KBN1.KBN_NM1                          " & vbNewLine _
                                              & "         ,GOOD.STD_WT_KGS                       " & vbNewLine _
                                              & "         ,DEST.DEST_NM                          " & vbNewLine _
                                              & "         ,GOOD.LOT_CTL_KB                       " & vbNewLine _
                                              & "         ,GOOD.LT_DATE_CTL_KB                   " & vbNewLine _
                                              & "         ,GOOD.CRT_DATE_CTL_KB                  " & vbNewLine _
                                              & "         ,INKS.BUG_YN                           " & vbNewLine _
                                              & "         ,MFL.CNT                               " & vbNewLine _
                                              & "         ,PHOTO.INKA_NO_L                       " & vbNewLine _
                                              & "ORDER BY                                        " & vbNewLine _
                                              & "          INKS.INKA_NO_L                        " & vbNewLine _
                                              & "         ,INKS.INKA_NO_M                        " & vbNewLine _
                                              & "         ,INKS.INKA_NO_S                        " & vbNewLine
    'UPD 2022/11/07 倉庫写真アプリ対応 END

#End Region

#Region "UNSO_L"

    Private Const SQL_SELECT_UNSO_L As String = "SELECT TOP 1                                               " & vbNewLine _
                                              & " FUNL.NRS_BR_CD                    AS NRS_BR_CD            " & vbNewLine _
                                              & ",FUNL.UNSO_NO_L                    AS UNSO_NO_L            " & vbNewLine _
                                              & ",FUNL.YUSO_BR_CD                   AS YUSO_BR_CD           " & vbNewLine _
                                              & ",FUNL.INOUTKA_NO_L                 AS INOUTKA_NO_L         " & vbNewLine _
                                              & ",FUNL.TRIP_NO                      AS TRIP_NO              " & vbNewLine _
                                              & ",FUNL.UNSO_CD                      AS UNSO_CD              " & vbNewLine _
                                              & ",FUNL.UNSO_BR_CD                   AS UNSO_BR_CD           " & vbNewLine _
                                              & ",MUSC.TARE_YN                      AS TARE_YN              " & vbNewLine _
                                              & ",FUNL.BIN_KB                       AS BIN_KB               " & vbNewLine _
                                              & ",FUNL.JIYU_KB                      AS JIYU_KB              " & vbNewLine _
                                              & ",FUNL.DENP_NO                      AS DENP_NO              " & vbNewLine _
                                              & ",FUNL.OUTKA_PLAN_DATE              AS OUTKA_PLAN_DATE      " & vbNewLine _
                                              & ",FUNL.OUTKA_PLAN_TIME              AS OUTKA_PLAN_TIME      " & vbNewLine _
                                              & ",FUNL.ARR_PLAN_DATE                AS ARR_PLAN_DATE        " & vbNewLine _
                                              & ",FUNL.ARR_PLAN_TIME                AS ARR_PLAN_TIME        " & vbNewLine _
                                              & ",FUNL.ARR_ACT_TIME                 AS ARR_ACT_TIME         " & vbNewLine _
                                              & ",FUNL.CUST_CD_L                    AS CUST_CD_L            " & vbNewLine _
                                              & ",FUNL.CUST_CD_M                    AS CUST_CD_M            " & vbNewLine _
                                              & ",FUNL.CUST_REF_NO                  AS CUST_REF_NO          " & vbNewLine _
                                              & ",FUNL.SHIP_CD                      AS SHIP_CD              " & vbNewLine _
                                              & ",FUNL.ORIG_CD                      AS ORIG_CD              " & vbNewLine _
                                              & ",FUNL.DEST_CD                      AS DEST_CD              " & vbNewLine _
                                              & ",FUNL.UNSO_PKG_NB                  AS UNSO_PKG_NB          " & vbNewLine _
                                              & ",FUNL.NB_UT                        AS NB_UT                " & vbNewLine _
                                              & ",FUNL.UNSO_WT                      AS UNSO_WT              " & vbNewLine _
                                              & ",FUNL.UNSO_ONDO_KB                 AS UNSO_ONDO_KB         " & vbNewLine _
                                              & ",FUNL.PC_KB                        AS PC_KB                " & vbNewLine _
                                              & ",FUNL.VCLE_KB                      AS VCLE_KB              " & vbNewLine _
                                              & ",FUNL.MOTO_DATA_KB                 AS MOTO_DATA_KB         " & vbNewLine _
                                              & ",FUNL.REMARK                       AS REMARK               " & vbNewLine _
                                              & ",FUNL.SEIQ_TARIFF_CD               AS SEIQ_TARIFF_CD       " & vbNewLine _
                                              & ",FUNL.SEIQ_ETARIFF_CD              AS SEIQ_ETARIFF_CD      " & vbNewLine _
                                              & ",FUNL.AD_3                         AS AD_3                 " & vbNewLine _
                                              & ",FUNL.UNSO_TEHAI_KB                AS UNSO_TEHAI_KB        " & vbNewLine _
                                              & ",FUNL.BUY_CHU_NO                   AS BUY_CHU_NO           " & vbNewLine _
                                              & ",FUNL.AREA_CD                      AS AREA_CD              " & vbNewLine _
                                              & ",FUNL.TYUKEI_HAISO_FLG             AS TYUKEI_HAISO_FLG     " & vbNewLine _
                                              & ",FUNL.SYUKA_TYUKEI_CD              AS SYUKA_TYUKEI_CD      " & vbNewLine _
                                              & ",FUNL.HAIKA_TYUKEI_CD              AS HAIKA_TYUKEI_CD      " & vbNewLine _
                                              & ",FUNL.TRIP_NO_SYUKA                AS TRIP_NO_SYUKA        " & vbNewLine _
                                              & ",FUNL.TRIP_NO_TYUKEI               AS TRIP_NO_TYUKEI       " & vbNewLine _
                                              & ",FUNL.TRIP_NO_HAIKA                AS TRIP_NO_HAIKA        " & vbNewLine _
                                              & ",MUSC.BETU_KYORI_CD                AS BETU_KYORI_CD        " & vbNewLine _
                                              & ",MUSC.UNCHIN_TARIFF_CD             AS UNCHIN_TARIFF_CD     " & vbNewLine _
                                              & ",MUSC.EXTC_TARIFF_CD               AS EXTC_TARIFF_CD       " & vbNewLine _
                                              & ",SOKO.JIS_CD                       AS JIS_CD               " & vbNewLine _
                                              & ",FUNH.SEIQ_KYORI                   AS KYORI                " & vbNewLine _
                                              & ",MUSC.UNSOCO_NM                    AS UNSOCO_NM            " & vbNewLine _
                                              & ",MUSC.UNSOCO_BR_NM                 AS UNSOCO_BR_NM         " & vbNewLine _
                                              & ",DEST.DEST_NM                      AS ORIG_CD_NM           " & vbNewLine _
                                              & ",UTRF.UNCHIN_TARIFF_REM            AS UNCHIN_TARIFF_REM    " & vbNewLine _
                                              & ",YTRF.YOKO_REM                     AS YOKO_REM             " & vbNewLine _
                                              & ",'1'                               AS UMU_FLG              " & vbNewLine _
                                              & ",FUNL.SYS_UPD_DATE                 AS SYS_UPD_DATE         " & vbNewLine _
                                              & ",FUNL.SYS_UPD_TIME                 AS SYS_UPD_TIME         " & vbNewLine _
                                              & ",FUNL.SYS_DEL_FLG                  AS SYS_DEL_FLG          " & vbNewLine _
                                              & ",FUNL.TARIFF_BUNRUI_KB             AS TARIFF_BUNRUI_KB     " & vbNewLine _
                                              & ",SUM(FUNH.SEIQ_UNCHIN)             AS SEIQ_UNCHIN          " & vbNewLine _
                                              & ",FUNL.TAX_KB                       AS TAX_KB               " & vbNewLine _
                                              & ",'1'                               AS UP_KBN               " & vbNewLine _
                                              & ",COUNT(FXED.UNSO_NO_L)             AS FIXED_CHK            " & vbNewLine _
                                              & ",COUNT(GRUP.UNSO_NO_L)             AS GROUP_CHK            " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start " & vbNewLine _
                                              & ",FUNL.SHIHARAI_TARIFF_CD           AS SHIHARAI_TARIFF_CD   " & vbNewLine _
                                              & ",FUNL.SHIHARAI_ETARIFF_CD          AS SHIHARAI_ETARIFF_CD  " & vbNewLine _
                                              & ",STRF.SHIHARAI_TARIFF_REM          AS SHIHARAI_TARIFF_REM  " & vbNewLine _
                                              & ",STRY.YOKO_REM                     AS SHIHARAI_YOKO_REM    " & vbNewLine _
                                              & ",COUNT(FXES.UNSO_NO_L)             AS SHIHARAI_FIXED_CHK   " & vbNewLine _
                                              & ",COUNT(GRUS.UNSO_NO_L)             AS SHIHARAI_GROUP_CHK   " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End   " & vbNewLine _
                                              & "FROM       $LM_TRN$..F_UNSO_L         FUNL                 " & vbNewLine _
                                              & "INNER JOIN $LM_TRN$..B_INKA_L         INKL                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = INKL.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.INOUTKA_NO_L             = INKL.INKA_NO_L       " & vbNewLine _
                                              & "AND   INKL.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..F_UNCHIN_TRS     FUNH                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = FUNH.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_NO_L                = FUNH.UNSO_NO_L       " & vbNewLine _
                                              & "AND   FUNH.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..F_UNCHIN_TRS     FXED                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = FXED.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_NO_L                = FXED.UNSO_NO_L       " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start " & vbNewLine _
                                              & "--AND   FUNH.SEIQ_FIXED_FLAG          = '1'                  " & vbNewLine _
                                              & "AND   FUNH.SEIQ_FIXED_FLAG          = '01'                 " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End   " & vbNewLine _
                                              & "AND   FUNH.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..F_UNCHIN_TRS     GRUP                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = GRUP.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_NO_L                = GRUP.UNSO_NO_L       " & vbNewLine _
                                              & "AND   RTRIM(GRUP.SEIQ_GROUP_NO)    <> ''                   " & vbNewLine _
                                              & "AND   GRUP.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..F_SHIHARAI_TRS   FUNS                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = FUNS.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_NO_L                = FUNS.UNSO_NO_L       " & vbNewLine _
                                              & "AND   FUNS.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..F_SHIHARAI_TRS   FXES                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = FXES.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_NO_L                = FXES.UNSO_NO_L       " & vbNewLine _
                                              & "AND   FUNS.SHIHARAI_FIXED_FLAG      = '01'                 " & vbNewLine _
                                              & "AND   FUNS.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..F_SHIHARAI_TRS   GRUS                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = GRUS.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_NO_L                = GRUS.UNSO_NO_L       " & vbNewLine _
                                              & "AND   RTRIM(GRUS.SHIHARAI_GROUP_NO)    <> ''               " & vbNewLine _
                                              & "AND   GRUS.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN                                                 " & vbNewLine _
                                              & "(SELECT                                                    " & vbNewLine _
                                              & "STRF2.NRS_BR_CD                                            " & vbNewLine _
                                              & ",STRF2.SHIHARAI_TARIFF_CD                                  " & vbNewLine _
                                              & ",STRF2.SHIHARAI_TARIFF_CD_EDA                              " & vbNewLine _
                                              & ",STRF2.STR_DATE                                            " & vbNewLine _
                                              & ",STRF2.SHIHARAI_TARIFF_REM                                 " & vbNewLine _
                                              & ",STRF2.SYS_DEL_FLG                                         " & vbNewLine _
                                              & "FROM  $LM_MST$..M_SHIHARAI_TARIFF  STRF2                   " & vbNewLine _
                                              & "WHERE STRF2.NRS_BR_CD               = @NRS_BR_CD           " & vbNewLine _
                                              & "AND   STRF2.SYS_DEL_FLG             = '0'                  " & vbNewLine _
                                              & ")  STRF                                                    " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = STRF.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.SHIHARAI_TARIFF_CD       = STRF.SHIHARAI_TARIFF_CD  " & vbNewLine _
                                              & "AND   INKL.INKA_DATE                >= STRF.STR_DATE       " & vbNewLine _
                                              & "AND   STRF.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD_SHIHARAI STRY        " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = STRY.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.SHIHARAI_TARIFF_CD       = STRY.YOKO_TARIFF_CD  " & vbNewLine _
                                              & "AND   STRY.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End   " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_DEST           DEST                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = DEST.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.CUST_CD_L                = DEST.CUST_CD_L       " & vbNewLine _
                                              & "AND   FUNL.ORIG_CD                  = DEST.DEST_CD         " & vbNewLine _
                                              & "AND   DEST.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_UNSOCO         MUSC                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = MUSC.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_CD                  = MUSC.UNSOCO_CD       " & vbNewLine _
                                              & "AND   FUNL.UNSO_BR_CD               = MUSC.UNSOCO_BR_CD    " & vbNewLine _
                                              & "AND   MUSC.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_SOKO           SOKO                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = SOKO.NRS_BR_CD       " & vbNewLine _
                                              & "AND   INKL.WH_CD                    = SOKO.WH_CD           " & vbNewLine _
                                              & "AND   SOKO.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN                                                 " & vbNewLine _
                                              & "(SELECT                                                    " & vbNewLine _
                                              & "UTRF2.NRS_BR_CD                                            " & vbNewLine _
                                              & ",UTRF2.UNCHIN_TARIFF_CD                                    " & vbNewLine _
                                              & ",UTRF2.UNCHIN_TARIFF_CD_EDA                                " & vbNewLine _
                                              & ",UTRF2.STR_DATE                                            " & vbNewLine _
                                              & ",UTRF2.UNCHIN_TARIFF_REM                                   " & vbNewLine _
                                              & ",UTRF2.SYS_DEL_FLG                                         " & vbNewLine _
                                              & "FROM  $LM_MST$..M_UNCHIN_TARIFF  UTRF2                     " & vbNewLine _
                                              & "WHERE UTRF2.NRS_BR_CD               = @NRS_BR_CD           " & vbNewLine _
                                              & "AND   UTRF2.SYS_DEL_FLG             = '0'                  " & vbNewLine _
                                              & ")  UTRF                                                    " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = UTRF.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.SEIQ_TARIFF_CD           = UTRF.UNCHIN_TARIFF_CD" & vbNewLine _
                                              & "AND   INKL.INKA_DATE                >= UTRF.STR_DATE       " & vbNewLine _
                                              & "AND   UTRF.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD YTRF                 " & vbNewLine _
                                              & "ON    FUNL.NRS_BR_CD                = YTRF.NRS_BR_CD       " & vbNewLine _
                                              & "AND   FUNL.SEIQ_TARIFF_CD           = YTRF.YOKO_TARIFF_CD  " & vbNewLine _
                                              & "AND   YTRF.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "WHERE FUNL.NRS_BR_CD                = @NRS_BR_CD           " & vbNewLine _
                                              & "AND   FUNL.INOUTKA_NO_L             = @INKA_NO_L           " & vbNewLine _
                                              & "AND   FUNL.MOTO_DATA_KB             = '10'                 " & vbNewLine _
                                              & "AND   FUNL.SYS_DEL_FLG              = '0'                  " & vbNewLine _
                                              & "GROUP BY FUNL.NRS_BR_CD                                    " & vbNewLine _
                                              & "        ,FUNL.UNSO_NO_L                                    " & vbNewLine _
                                              & "        ,FUNL.YUSO_BR_CD                                   " & vbNewLine _
                                              & "        ,FUNL.INOUTKA_NO_L                                 " & vbNewLine _
                                              & "        ,FUNL.TRIP_NO                                      " & vbNewLine _
                                              & "        ,FUNL.UNSO_CD                                      " & vbNewLine _
                                              & "        ,FUNL.UNSO_BR_CD                                   " & vbNewLine _
                                              & "        ,MUSC.TARE_YN                                      " & vbNewLine _
                                              & "        ,FUNL.BIN_KB                                       " & vbNewLine _
                                              & "        ,FUNL.JIYU_KB                                      " & vbNewLine _
                                              & "        ,FUNL.DENP_NO                                      " & vbNewLine _
                                              & "        ,FUNL.OUTKA_PLAN_DATE                              " & vbNewLine _
                                              & "        ,FUNL.OUTKA_PLAN_TIME                              " & vbNewLine _
                                              & "        ,FUNL.ARR_PLAN_DATE                                " & vbNewLine _
                                              & "        ,FUNL.ARR_PLAN_TIME                                " & vbNewLine _
                                              & "        ,FUNL.ARR_ACT_TIME                                 " & vbNewLine _
                                              & "        ,FUNL.CUST_CD_L                                    " & vbNewLine _
                                              & "        ,FUNL.CUST_CD_M                                    " & vbNewLine _
                                              & "        ,FUNL.CUST_REF_NO                                  " & vbNewLine _
                                              & "        ,FUNL.SHIP_CD                                      " & vbNewLine _
                                              & "        ,FUNL.ORIG_CD                                      " & vbNewLine _
                                              & "        ,FUNL.DEST_CD                                      " & vbNewLine _
                                              & "        ,FUNL.UNSO_PKG_NB                                  " & vbNewLine _
                                              & "        ,FUNL.NB_UT                                        " & vbNewLine _
                                              & "        ,FUNL.UNSO_WT                                      " & vbNewLine _
                                              & "        ,FUNL.UNSO_ONDO_KB                                 " & vbNewLine _
                                              & "        ,FUNL.PC_KB                                        " & vbNewLine _
                                              & "        ,FUNL.VCLE_KB                                      " & vbNewLine _
                                              & "        ,FUNL.MOTO_DATA_KB                                 " & vbNewLine _
                                              & "        ,FUNL.REMARK                                       " & vbNewLine _
                                              & "        ,FUNL.SEIQ_TARIFF_CD                               " & vbNewLine _
                                              & "        ,FUNL.SEIQ_ETARIFF_CD                              " & vbNewLine _
                                              & "        ,FUNL.AD_3                                         " & vbNewLine _
                                              & "        ,FUNL.UNSO_TEHAI_KB                                " & vbNewLine _
                                              & "        ,FUNL.BUY_CHU_NO                                   " & vbNewLine _
                                              & "        ,FUNL.AREA_CD                                      " & vbNewLine _
                                              & "        ,FUNL.TYUKEI_HAISO_FLG                             " & vbNewLine _
                                              & "        ,FUNL.SYUKA_TYUKEI_CD                              " & vbNewLine _
                                              & "        ,FUNL.HAIKA_TYUKEI_CD                              " & vbNewLine _
                                              & "        ,FUNL.TRIP_NO_SYUKA                                " & vbNewLine _
                                              & "        ,FUNL.TRIP_NO_TYUKEI                               " & vbNewLine _
                                              & "        ,FUNL.TRIP_NO_HAIKA                                " & vbNewLine _
                                              & "        ,MUSC.BETU_KYORI_CD                                " & vbNewLine _
                                              & "        ,MUSC.UNCHIN_TARIFF_CD                             " & vbNewLine _
                                              & "        ,MUSC.EXTC_TARIFF_CD                               " & vbNewLine _
                                              & "        ,SOKO.JIS_CD                                       " & vbNewLine _
                                              & "        ,FUNH.SEIQ_KYORI                                   " & vbNewLine _
                                              & "        ,MUSC.UNSOCO_NM                                    " & vbNewLine _
                                              & "        ,MUSC.UNSOCO_BR_NM                                 " & vbNewLine _
                                              & "        ,UTRF.STR_DATE                                     " & vbNewLine _
                                              & "        ,UTRF.UNCHIN_TARIFF_CD_EDA                         " & vbNewLine _
                                              & "        ,DEST.DEST_NM                                      " & vbNewLine _
                                              & "        ,UTRF.UNCHIN_TARIFF_REM                            " & vbNewLine _
                                              & "        ,YTRF.YOKO_REM                                     " & vbNewLine _
                                              & "        ,FUNL.SYS_UPD_DATE                                 " & vbNewLine _
                                              & "        ,FUNL.SYS_UPD_TIME                                 " & vbNewLine _
                                              & "        ,FUNL.SYS_DEL_FLG                                  " & vbNewLine _
                                              & "        ,FUNL.TARIFF_BUNRUI_KB                             " & vbNewLine _
                                              & "        ,FUNL.TAX_KB                                       " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start " & vbNewLine _
                                              & "        ,FUNL.SHIHARAI_TARIFF_CD                           " & vbNewLine _
                                              & "        ,FUNL.SHIHARAI_ETARIFF_CD                          " & vbNewLine _
                                              & "        ,STRF.SHIHARAI_TARIFF_CD_EDA                       " & vbNewLine _
                                              & "        ,STRF.SHIHARAI_TARIFF_REM                          " & vbNewLine _
                                              & "        ,STRY.YOKO_REM                                     " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End   " & vbNewLine _
                                              & "ORDER BY FUNL.UNSO_NO_L                                    " & vbNewLine _
                                              & "        ,UTRF.STR_DATE                                     " & vbNewLine _
                                              & "        ,UTRF.UNCHIN_TARIFF_CD_EDA                         " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start " & vbNewLine _
                                              & "        ,STRF.SHIHARAI_TARIFF_CD_EDA                       " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End   " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                " & vbNewLine _
                                              & " F04_01.YUSO_BR_CD            AS YUSO_BR_CD           " & vbNewLine _
                                              & ",F04_01.NRS_BR_CD             AS NRS_BR_CD            " & vbNewLine _
                                              & ",F04_01.UNSO_NO_L             AS UNSO_NO_L            " & vbNewLine _
                                              & ",F04_01.UNSO_NO_M             AS UNSO_NO_M            " & vbNewLine _
                                              & ",F04_01.CUST_CD_L             AS CUST_CD_L            " & vbNewLine _
                                              & ",F04_01.CUST_CD_M             AS CUST_CD_M            " & vbNewLine _
                                              & ",F04_01.CUST_CD_S             AS CUST_CD_S            " & vbNewLine _
                                              & ",F04_01.CUST_CD_SS            AS CUST_CD_SS           " & vbNewLine _
                                              & ",F04_01.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO        " & vbNewLine _
                                              & ",F04_01.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M      " & vbNewLine _
                                              & ",F04_01.SEIQTO_CD             AS SEIQTO_CD            " & vbNewLine _
                                              & ",F04_01.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB " & vbNewLine _
                                              & ",F04_01.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB       " & vbNewLine _
                                              & ",F04_01.SEIQ_PKG_UT           AS SEIQ_PKG_UT          " & vbNewLine _
                                              & ",F04_01.SEIQ_NG_NB            AS SEIQ_NG_NB           " & vbNewLine _
                                              & ",F04_01.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB       " & vbNewLine _
                                              & ",F04_01.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
                                              & ",F04_01.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD       " & vbNewLine _
                                              & ",F04_01.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD      " & vbNewLine _
                                              & ",F04_01.SEIQ_KYORI            AS SEIQ_KYORI           " & vbNewLine _
                                              & ",F04_01.SEIQ_WT               AS SEIQ_WT              " & vbNewLine _
                                              & ",F04_01.SEIQ_UNCHIN           AS SEIQ_UNCHIN          " & vbNewLine _
                                              & ",F04_01.SEIQ_CITY_EXTC        AS SEIQ_CITY_EXTC       " & vbNewLine _
                                              & ",F04_01.SEIQ_WINT_EXTC        AS SEIQ_WINT_EXTC       " & vbNewLine _
                                              & ",F04_01.SEIQ_RELY_EXTC        AS SEIQ_RELY_EXTC       " & vbNewLine _
                                              & ",F04_01.SEIQ_TOLL             AS SEIQ_TOLL            " & vbNewLine _
                                              & ",F04_01.SEIQ_INSU             AS SEIQ_INSU            " & vbNewLine _
                                              & ",F04_01.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG      " & vbNewLine _
                                              & ",F04_01.DECI_NG_NB            AS DECI_NG_NB           " & vbNewLine _
                                              & ",F04_01.DECI_KYORI            AS DECI_KYORI           " & vbNewLine _
                                              & ",F04_01.DECI_WT               AS DECI_WT              " & vbNewLine _
                                              & ",F04_01.DECI_UNCHIN           AS DECI_UNCHIN          " & vbNewLine _
                                              & ",F04_01.DECI_CITY_EXTC        AS DECI_CITY_EXTC       " & vbNewLine _
                                              & ",F04_01.DECI_WINT_EXTC        AS DECI_WINT_EXTC       " & vbNewLine _
                                              & ",F04_01.DECI_RELY_EXTC        AS DECI_RELY_EXTC       " & vbNewLine _
                                              & ",F04_01.DECI_TOLL             AS DECI_TOLL            " & vbNewLine _
                                              & ",F04_01.DECI_INSU             AS DECI_INSU            " & vbNewLine _
                                              & ",F04_01.KANRI_UNCHIN          AS KANRI_UNCHIN         " & vbNewLine _
                                              & ",F04_01.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC      " & vbNewLine _
                                              & ",F04_01.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC      " & vbNewLine _
                                              & ",F04_01.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC      " & vbNewLine _
                                              & ",F04_01.KANRI_TOLL            AS KANRI_TOLL           " & vbNewLine _
                                              & ",F04_01.KANRI_INSU            AS KANRI_INSU           " & vbNewLine _
                                              & ",F04_01.REMARK                AS REMARK               " & vbNewLine _
                                              & ",F04_01.SIZE_KB               AS SIZE_KB              " & vbNewLine _
                                              & ",F04_01.TAX_KB                AS TAX_KB               " & vbNewLine _
                                              & ",F04_01.SAGYO_KANRI           AS SAGYO_KANRI          " & vbNewLine _
                                              & "FROM  $LM_TRN$..F_UNCHIN_TRS F04_01                   " & vbNewLine _
                                              & "WHERE F04_01.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                              & "  AND F04_01.UNSO_NO_L   = @UNSO_NO_L                 " & vbNewLine _
                                              & "  AND F04_01.SYS_DEL_FLG = '0'                        " & vbNewLine


#End Region

#Region "SAGYO"

    'START YANAI No.7
    'Private Const SQL_SELECT_SAGYO As String = "SELECT                                         " & vbNewLine _
    '                                         & " SAGY.SAGYO_COMP        AS SAGYO_COMP          " & vbNewLine _
    '                                         & ",KBN0.KBN_NM1           AS SAGYO_COMP_NM       " & vbNewLine _
    '                                         & ",''                     AS SKYU_CHK            " & vbNewLine _
    '                                         & ",SAGY.SAGYO_REC_NO      AS SAGYO_REC_NO        " & vbNewLine _
    '                                         & ",''                     AS SAGYO_SIJI_NO       " & vbNewLine _
    '                                         & ",SAGY.INOUTKA_NO_LM     AS INOUTKA_NO_LM       " & vbNewLine _
    '                                         & ",SAGY.NRS_BR_CD         AS NRS_BR_CD           " & vbNewLine _
    '                                         & ",SAGY.IOZS_KB           AS IOZS_KB             " & vbNewLine _
    '                                         & ",SAGY.SAGYO_CD          AS SAGYO_CD            " & vbNewLine _
    '                                         & ",''                     AS SAGYO_NM            " & vbNewLine _
    '                                         & ",SAGY.DEST_SAGYO_FLG    AS DEST_SAGYO_FLG      " & vbNewLine _
    '                                         & ",''                     AS DEST_CD             " & vbNewLine _
    '                                         & ",''                     AS DEST_NM             " & vbNewLine _
    '                                         & ",''                     AS LOT_NO              " & vbNewLine _
    '                                         & ",''                     AS INV_TANI            " & vbNewLine _
    '                                         & ",''                     AS SAGYO_NB            " & vbNewLine _
    '                                         & ",''                     AS SAGYO_UP            " & vbNewLine _
    '                                         & ",''                     AS SAGYO_GK            " & vbNewLine _
    '                                         & ",''                     AS TAX_KB              " & vbNewLine _
    '                                         & ",''                     AS REMARK_ZAI          " & vbNewLine _
    '                                         & ",''                     AS SAGYO_COMP_NM       " & vbNewLine _
    '                                         & ",MSGY.SAGYO_RYAK        AS SAGYO_RYAK          " & vbNewLine _
    '                                         & ",SAGY.SYS_DEL_FLG       AS SYS_DEL_FLG         " & vbNewLine _
    '                                         & ",SAGY.SYS_UPD_DATE      AS SYS_UPD_DATE        " & vbNewLine _
    '                                         & ",SAGY.SYS_UPD_TIME      AS SYS_UPD_TIME        " & vbNewLine _
    '                                         & ",'1'                    AS UP_KBN              " & vbNewLine _
    '                                         & "FROM       $LM_TRN$..E_SAGYO     SAGY          " & vbNewLine _
    '                                         & "LEFT  JOIN $LM_MST$..M_CUST      CUST          " & vbNewLine _
    '                                         & "ON    SAGY.NRS_BR_CD           = CUST.NRS_BR_CD" & vbNewLine _
    '                                         & "AND   SAGY.CUST_CD_L           = CUST.CUST_CD_L" & vbNewLine _
    '                                         & "AND   SAGY.CUST_CD_M           = CUST.CUST_CD_M" & vbNewLine _
    '                                         & "AND   CUST.CUST_CD_S           = '00'          " & vbNewLine _
    '                                         & "AND   CUST.CUST_CD_SS          = '00'          " & vbNewLine _
    '                                         & "AND   (SAGY.IOZS_KB            = '10'          " & vbNewLine _
    '                                         & "OR     SAGY.IOZS_KB            = '11')         " & vbNewLine _
    '                                         & "AND   CUST.SYS_DEL_FLG         = '0'           " & vbNewLine _
    '                                         & "LEFT  JOIN $LM_MST$..M_SAGYO     MSGY          " & vbNewLine _
    '                                         & "ON    SAGY.NRS_BR_CD           = MSGY.NRS_BR_CD" & vbNewLine _
    '                                         & "AND   SAGY.SAGYO_CD            = MSGY.SAGYO_CD " & vbNewLine _
    '                                         & "AND   MSGY.SYS_DEL_FLG         = '0'           " & vbNewLine _
    '                                         & "LEFT  JOIN $LM_MST$..Z_KBN       KBN0          " & vbNewLine _
    '                                         & "ON    SAGY.SAGYO_COMP          = KBN0.KBN_CD   " & vbNewLine _
    '                                         & "AND   KBN0.KBN_GROUP_CD        = 'S052'        " & vbNewLine _
    '                                         & "AND   KBN0.SYS_DEL_FLG         = '0'           " & vbNewLine _
    '                                         & "WHERE SAGY.INOUTKA_NO_LM    LIKE @INKA_NO_L    " & vbNewLine _
    '                                         & "AND   SAGY.SYS_DEL_FLG         = '0'           " & vbNewLine
    Private Const SQL_SELECT_SAGYO As String = "SELECT                                         " & vbNewLine _
                                             & " SAGY.SAGYO_COMP        AS SAGYO_COMP          " & vbNewLine _
                                             & ",KBN0.KBN_NM1           AS SAGYO_COMP_NM       " & vbNewLine _
                                             & ",SAGY.SKYU_CHK          AS SKYU_CHK            " & vbNewLine _
                                             & ",SAGY.SAGYO_REC_NO      AS SAGYO_REC_NO        " & vbNewLine _
                                             & ",''                     AS SAGYO_SIJI_NO       " & vbNewLine _
                                             & ",SAGY.INOUTKA_NO_LM     AS INOUTKA_NO_LM       " & vbNewLine _
                                             & ",SAGY.NRS_BR_CD         AS NRS_BR_CD           " & vbNewLine _
                                             & ",SAGY.IOZS_KB           AS IOZS_KB             " & vbNewLine _
                                             & ",SAGY.SAGYO_CD          AS SAGYO_CD            " & vbNewLine _
                                             & ",SAGY.SAGYO_NM          AS SAGYO_NM            " & vbNewLine _
                                             & ",SAGY.CUST_CD_L         AS CUST_CD_L           " & vbNewLine _
                                             & ",SAGY.CUST_CD_M         AS CUST_CD_M           " & vbNewLine _
                                             & ",SAGY.DEST_SAGYO_FLG    AS DEST_SAGYO_FLG      " & vbNewLine _
                                             & ",''                     AS DEST_CD             " & vbNewLine _
                                             & ",''                     AS DEST_NM             " & vbNewLine _
                                             & "--,''                     AS LOT_NO              " & vbNewLine _
                                             & ",SAGY.LOT_NO            AS LOT_NO              " & vbNewLine _
                                             & ",SAGY.INV_TANI          AS INV_TANI            " & vbNewLine _
                                             & "--,''                     AS SAGYO_NB            " & vbNewLine _
                                             & ",SAGY.SAGYO_NB          AS SAGYO_NB            " & vbNewLine _
                                             & "--,''                     AS SAGYO_UP            " & vbNewLine _
                                             & ",SAGY.SAGYO_UP          AS SAGYO_UP            " & vbNewLine _
                                             & "--,''                     AS SAGYO_GK            " & vbNewLine _
                                             & ",ROUND(SAGY.SAGYO_GK,0) AS SAGYO_GK            " & vbNewLine _
                                             & "--,''                     AS TAX_KB              " & vbNewLine _
                                             & ",SAGY.TAX_KB            AS TAX_KB              " & vbNewLine _
                                             & ",''                     AS REMARK_ZAI          " & vbNewLine _
                                             & ",MSGY.SAGYO_RYAK        AS SAGYO_RYAK          " & vbNewLine _
                                             & ",SAGY.SYS_DEL_FLG       AS SYS_DEL_FLG         " & vbNewLine _
                                             & ",SAGY.SYS_UPD_DATE      AS SYS_UPD_DATE        " & vbNewLine _
                                             & ",SAGY.SYS_UPD_TIME      AS SYS_UPD_TIME        " & vbNewLine _
                                             & ",SAGY.REMARK_SIJI       AS REMARK_SIJI         " & vbNewLine _
                                             & ",'1'                    AS UP_KBN              " & vbNewLine _
                                             & "FROM       $LM_TRN$..E_SAGYO     SAGY          " & vbNewLine _
                                             & "LEFT  JOIN $LM_MST$..M_CUST      CUST          " & vbNewLine _
                                             & "ON    SAGY.NRS_BR_CD           = CUST.NRS_BR_CD" & vbNewLine _
                                             & "AND   SAGY.CUST_CD_L           = CUST.CUST_CD_L" & vbNewLine _
                                             & "AND   SAGY.CUST_CD_M           = CUST.CUST_CD_M" & vbNewLine _
                                             & "AND   CUST.CUST_CD_S           = '00'          " & vbNewLine _
                                             & "AND   CUST.CUST_CD_SS          = '00'          " & vbNewLine _
                                             & "AND   CUST.SYS_DEL_FLG         = '0'           " & vbNewLine _
                                             & "LEFT  JOIN $LM_MST$..M_SAGYO     MSGY          " & vbNewLine _
                                             & "ON    SAGY.NRS_BR_CD           = MSGY.NRS_BR_CD" & vbNewLine _
                                             & "AND   SAGY.SAGYO_CD            = MSGY.SAGYO_CD " & vbNewLine _
                                             & "AND   MSGY.SYS_DEL_FLG         = '0'           " & vbNewLine _
                                             & "LEFT  JOIN $LM_MST$..Z_KBN       KBN0          " & vbNewLine _
                                             & "ON    SAGY.SAGYO_COMP          = KBN0.KBN_CD   " & vbNewLine _
                                             & "AND   KBN0.KBN_GROUP_CD        = 'S052'        " & vbNewLine _
                                             & "AND   KBN0.SYS_DEL_FLG         = '0'           " & vbNewLine _
                                             & "WHERE SAGY.INOUTKA_NO_LM    LIKE @INKA_NO_L    " & vbNewLine _
                                             & "AND   SAGY.SYS_DEL_FLG         = '0'           " & vbNewLine _
                                             & "AND   (SAGY.IOZS_KB            = '10'          " & vbNewLine _
                                             & "OR     SAGY.IOZS_KB            = '11')         " & vbNewLine _
    'END YANAI No.7


#End Region

#Region "ZAIKO"

    Private Const SQL_SELECT_ZAIKO As String = "SELECT                                     " & vbNewLine _
                                             & " ZAIK.ZAI_REC_NO        AS ZAI_REC_NO      " & vbNewLine _
                                             & ",ZAIK.INKA_NO_L         AS INKA_NO_L       " & vbNewLine _
                                             & ",ZAIK.INKA_NO_M         AS INKA_NO_M       " & vbNewLine _
                                             & ",ZAIK.INKA_NO_S         AS INKA_NO_S       " & vbNewLine _
                                             & ",''                     AS RSV_NO          " & vbNewLine _
                                             & ",ZAIK.ALCTD_NB          AS ALCTD_NB        " & vbNewLine _
                                             & ",ZAIK.ALLOC_CAN_NB      AS ALLOC_CAN_NB    " & vbNewLine _
                                             & ",ZAIK.PORA_ZAI_QT       AS PORA_ZAI_QT     " & vbNewLine _
                                             & ",ZAIK.ALCTD_QT          AS ALCTD_QT        " & vbNewLine _
                                             & ",ZAIK.ALLOC_CAN_QT      AS ALLOC_CAN_QT    " & vbNewLine _
                                             & ",ZAIK.INKO_DATE         AS INKO_DATE       " & vbNewLine _
                                             & ",''                     AS INKO_PLAN_DATE  " & vbNewLine _
                                             & ",ZAIK.ZERO_FLAG         AS ZERO_FLAG       " & vbNewLine _
                                             & ",ZAIK.SMPL_FLAG         AS SMPL_FLAG       " & vbNewLine _
                                             & ",ZAIK.SYS_DEL_FLG       AS SYS_DEL_FLG     " & vbNewLine _
                                             & ",ZAIK.SYS_UPD_DATE      AS SYS_UPD_DATE    " & vbNewLine _
                                             & ",ZAIK.SYS_UPD_TIME      AS SYS_UPD_TIME    " & vbNewLine _
                                             & ",'1'                    AS UP_KBN          " & vbNewLine _
                                             & "FROM  $LM_TRN$..D_ZAI_TRS ZAIK             " & vbNewLine _
                                             & "WHERE ZAIK.NRS_BR_CD   = @NRS_BR_CD        " & vbNewLine _
                                             & "AND   ZAIK.INKA_NO_L   = @INKA_NO_L        " & vbNewLine _
                                             & "AND   ZAIK.SYS_DEL_FLG = '0'               " & vbNewLine


#End Region

#Region "MAX_NO"

    'START YANAI 要望番号493
    'Private Const SQL_SELECT_MAX_NO As String = "SELECT INKM.INKA_NO_M                    AS INKA_NO_M    " & vbNewLine _
    '                                          & "      ,ISNULL(MAX(INKS.INKA_NO_S),'000') AS MAX_INKA_NO_S" & vbNewLine _
    '                                          & "FROM       $LM_TRN$..B_INKA_M INKM                       " & vbNewLine _
    '                                          & "LEFT  JOIN $LM_TRN$..B_INKA_S INKS                       " & vbNewLine _
    '                                          & "ON    INKM.NRS_BR_CD = INKS.NRS_BR_CD                    " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L = INKS.INKA_NO_L                    " & vbNewLine _
    '                                          & "WHERE INKM.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
    '                                          & "AND   INKM.INKA_NO_L = @INKA_NO_L                        " & vbNewLine _
    '                                          & "GROUP BY INKM.INKA_NO_M                                  " & vbNewLine _
    '                                          & "ORDER BY INKM.INKA_NO_M                                  " & vbNewLine
    Private Const SQL_SELECT_MAX_NO As String = "SELECT INKM.INKA_NO_M                    AS INKA_NO_M    " & vbNewLine _
                                              & "      ,(SELECT ISNULL(MAX(INKS.INKA_NO_S),'000') AS MAX_S " & vbNewLine _
                                              & "       FROM $LM_TRN$..B_INKA_S INKS                      " & vbNewLine _
                                              & "       WHERE                                             " & vbNewLine _
                                              & "           INKS.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                              & "       AND INKS.INKA_NO_L = @INKA_NO_L                   " & vbNewLine _
                                              & "       AND INKS.INKA_NO_M = INKM.INKA_NO_M               " & vbNewLine _
                                              & "       ) AS MAX_INKA_NO_S                                " & vbNewLine _
                                              & "FROM       $LM_TRN$..B_INKA_M INKM                       " & vbNewLine _
                                              & "LEFT  JOIN $LM_TRN$..B_INKA_S INKS                       " & vbNewLine _
                                              & "ON    INKM.NRS_BR_CD = INKS.NRS_BR_CD                    " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L = INKS.INKA_NO_L                    " & vbNewLine _
                                              & "WHERE INKM.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                              & "AND   INKM.INKA_NO_L = @INKA_NO_L                        " & vbNewLine _
                                              & "GROUP BY INKM.INKA_NO_M                                  " & vbNewLine _
                                              & "ORDER BY INKM.INKA_NO_M                                  " & vbNewLine
    'END YANAI 要望番号493

#End Region

#Region "M_CUST"

    Private Const SQL_SELECT_CUST As String = "SELECT CUST.HOKAN_SEIQTO_CD  AS HOKAN_SEIQTO_CD " & vbNewLine _
                                            & "      ,CUST.NIYAKU_SEIQTO_CD AS NIYAKU_SEIQTO_CD" & vbNewLine _
                                            & "      ,CUST.SAGYO_SEIQTO_CD  AS SAGYO_SEIQTO_CD " & vbNewLine _
                                            & "      ,CUST.HOKAN_NIYAKU_CALCULATION  AS HOKAN_NIYAKU_CALCULATION " & vbNewLine _
                                            & "FROM       $LM_MST$..M_CUST CUST                " & vbNewLine _
                                            & "WHERE  CUST.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_L   = @CUST_CD_L            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_M   = @CUST_CD_M            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_S   = @CUST_CD_S            " & vbNewLine _
                                            & "  AND  CUST.CUST_CD_SS  = @CUST_CD_SS           " & vbNewLine _
                                            & "  AND  CUST.SYS_DEL_FLG = '0'                   " & vbNewLine


#End Region

    '要望番号:1350 terakawa 2012.08.24 Start
#Region "M_CUST_DETAILS"

    Private Const SQL_SELECT_CUST_DETAILS As String = "SELECT                                    " & vbNewLine _
                                            & "  CUST_D.SET_NAIYO  AS SET_NAIYO                  " & vbNewLine _
                                            & " ,CUST_D.SET_NAIYO_2  AS SET_NAIYO_2              " & vbNewLine _
                                            & "FROM       $LM_MST$..M_CUST_DETAILS CUST_D        " & vbNewLine _
                                            & "WHERE  CUST_D.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  CUST_D.CUST_CD   = @CUST_CD_L              " & vbNewLine _
                                            & "  AND  CUST_D.SUB_KB    = '41'                    " & vbNewLine _
                                            & "  AND  CUST_D.SYS_DEL_FLG = '0'                   " & vbNewLine


#End Region
    '要望番号:1350 terakawa 2012.08.24 End

#Region "M_SOKO"

    Private Const SQL_SELECT_SOKO As String = "SELECT SOKO.INKA_UKE_PRT_YN  AS INKA_UKE_PRT_YN " & vbNewLine _
                                            & "      ,SOKO.INKA_KENPIN_YN   AS INKA_KENPIN_YN  " & vbNewLine _
                                            & "FROM       $LM_MST$..M_SOKO SOKO                " & vbNewLine _
                                            & "WHERE  SOKO.NRS_BR_CD   = @NRS_BR_CD            " & vbNewLine _
                                            & "  AND  SOKO.WH_CD       = @WH_CD                " & vbNewLine _
                                            & "  AND  SOKO.SYS_DEL_FLG = '0'                   " & vbNewLine

#End Region

#Region "INKA_QR"

    Private Const SQL_SELECT_INKA_QR As String _
        = " SELECT                                                    " & vbNewLine _
        & "        WK.NRS_BR_CD                AS NRS_BR_CD           " & vbNewLine _
        & "      , WK.INKA_NO_L                AS INKA_NO_L           " & vbNewLine _
        & "      , WK.INKA_NO_M                AS INKA_NO_M           " & vbNewLine _
        & "      , WK.INKA_NO_S                AS INKA_NO_S           " & vbNewLine _
        & "      , WK.SEQ                      AS SEQ                 " & vbNewLine _
        & "      , QR.GOODS_CD_NRS             AS GOODS_CD_NRS        " & vbNewLine _
        & "      , MG.GOODS_CD_CUST            AS GOODS_CD_CUST       " & vbNewLine _
        & "      , MG.GOODS_NM_1               AS GOODS_NM_1          " & vbNewLine _
        & "      , MG.GOODS_NM_2               AS GOODS_NM_2          " & vbNewLine _
        & "      , MG.GOODS_NM_3               AS GOODS_NM_3          " & vbNewLine _
        & "      , MG.PKG_NB                   AS PKG_NB              " & vbNewLine _
        & "      , QR.IRIME                    AS IRIME               " & vbNewLine _
        & "      , MG.STD_IRIME_NB             AS STD_IRIME_NB        " & vbNewLine _
        & "      , QR.LOT_NO                   AS LOT_NO              " & vbNewLine _
        & "      , QR.SERIAL_NO                AS SERIAL_NO           " & vbNewLine _
        & "      , QR.GOODS_CRT_DATE           AS GOODS_CRT_DATE      " & vbNewLine _
        & "      , QR.LT_DATE                  AS LT_DATE             " & vbNewLine _
        & "      , WK.KENPIN_NB                AS KENPIN_NB           " & vbNewLine _
        & "      , WK.EXISTS_REMARK            AS EXISTS_REMARK       " & vbNewLine _
        & "      , WK.WH_CD                    AS WH_CD               " & vbNewLine _
        & "      , WK.TOU_NO                   AS TOU_NO              " & vbNewLine _
        & "      , WK.SITU_NO                  AS SITU_NO             " & vbNewLine _
        & "      , WK.ZONE_CD                  AS ZONE_CD             " & vbNewLine _
        & "      , WK.LOCA                     AS LOCA                " & vbNewLine _
        & "      , WK.LOAD_DATE                AS LOAD_DATE           " & vbNewLine _
        & "      , WK.LOAD_TIME                AS LOAD_TIME           " & vbNewLine _
        & "      , ''                          AS IS_LOADING          " & vbNewLine _
        & "      , QR.NRS_SEQ_QR_NO            AS NRS_SEQ_QR_NO       " & vbNewLine _
        & "      , WK.INKA_LAST_UPD_DATE       AS INKA_SYS_UPD_DATE   " & vbNewLine _
        & "      , WK.INKA_LAST_UPD_TIME       AS INKA_SYS_UPD_TIME   " & vbNewLine _
        & "      , WK.SYS_ENT_DATE             AS KEPIN_DATE          " & vbNewLine _
        & "      , WK.SYS_ENT_TIME             AS KENPIN_TIME         " & vbNewLine _
        & "      , WK.SYS_UPD_DATE             AS SYS_UPD_DATE        " & vbNewLine _
        & "      , WK.SYS_UPD_TIME             AS SYS_UPD_TIME        " & vbNewLine _
        & "   FROM                                                    " & vbNewLine _
        & "        $LM_TRN$..B_INKA_SEQ_QR AS WK                      " & vbNewLine _
        & "   LEFT JOIN                                               " & vbNewLine _
        & "        $LM_MST$..M_NRS_SEQ_QR AS QR                       " & vbNewLine _
        & "     ON QR.NRS_SEQ_QR_NO = WK.NRS_SEQ_QR_NO                " & vbNewLine _
        & "    AND QR.QR_NO_REVISION = WK.QR_NO_REVISION              " & vbNewLine _
        & "   LEFT JOIN                                               " & vbNewLine _
        & "        $LM_MST$..M_GOODS AS MG                            " & vbNewLine _
        & "     ON MG.NRS_BR_CD    = QR.NRS_BR_CD                     " & vbNewLine _
        & "    AND MG.GOODS_CD_NRS = QR.GOODS_CD_NRS                  " & vbNewLine _
        & "  WHERE WK.INKA_NO_L   = @INKA_NO_L                        " & vbNewLine _
        & "    AND WK.NRS_BR_CD   = @NRS_BR_CD                        " & vbNewLine _
        & "    AND WK.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "  ORDER BY                                                 " & vbNewLine _
        & "        WK.NRS_BR_CD                                       " & vbNewLine _
        & "      , WK.INKA_NO_L                                       " & vbNewLine _
        & "      , WK.INKA_NO_M                                       " & vbNewLine _
        & "      , WK.INKA_NO_S                                       " & vbNewLine

#End Region

#Region "TB_KENPIN_HEAD"
    ''' <summary>
    ''' 入荷検品検品ヘッダ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TB_KENPIN_HEAD As String _
    = " SELECT                                                                  " & vbNewLine _
    & "     MAIN.NRS_BR_CD                     AS NRS_BR_CD                     " & vbNewLine _
    & "   , MAIN.INKA_NO_L                     AS INKA_NO_L                     " & vbNewLine _
    & "   , MAIN.IN_KENPIN_LOC_SEQ             AS IN_KENPIN_LOC_SEQ             " & vbNewLine _
    & "   , MAIN.IN_KENPIN_LOC_STATE_KB        AS IN_KENPIN_LOC_STATE_KB        " & vbNewLine _
    & "   , MAIN.WORK_STATE_KB                 AS WORK_STATE_KB                 " & vbNewLine _
    & "   , MAIN.CANCEL_FLG                    AS CANCEL_FLG                    " & vbNewLine _
    & "   , MAIN.WH_CD                         AS WH_CD                         " & vbNewLine _
    & "   , MAIN.CUST_CD_L                     AS CUST_CD_L                     " & vbNewLine _
    & "   , MAIN.CUST_CD_M                     AS CUST_CD_M                     " & vbNewLine _
    & "   , MAIN.CUST_NM_L                     AS CUST_NM_L                     " & vbNewLine _
    & "   , MAIN.CUST_NM_M                     AS CUST_NM_M                     " & vbNewLine _
    & "   , MAIN.BUYER_ORD_NO_L                AS BUYER_ORD_NO_L                " & vbNewLine _
    & "   , MAIN.OUTKA_FROM_ORD_NO_L           AS OUTKA_FROM_ORD_NO_L           " & vbNewLine _
    & "   , MAIN.UNSO_CD                       AS UNSO_CD                       " & vbNewLine _
    & "   , MAIN.UNSO_NM                       AS UNSO_NM                       " & vbNewLine _
    & "   , MAIN.UNSO_BR_CD                    AS UNSO_BR_CD                    " & vbNewLine _
    & "   , MAIN.UNSO_BR_NM                    AS UNSO_BR_NM                    " & vbNewLine _
    & "   , MAIN.INKA_DATE                     AS INKA_DATE                     " & vbNewLine _
    & "   , MAIN.REMARK                        AS REMARK                        " & vbNewLine _
    & "   , MAIN.REMARK_OUT                    AS REMARK_OUT                    " & vbNewLine _
    & "   , MAIN.REMARK_KENPIN_CHK_FLG         AS REMARK_KENPIN_CHK_FLG         " & vbNewLine _
    & "   , MAIN.REMARK_LOCA_CHK_FLG           AS REMARK_LOCA_CHK_FLG           " & vbNewLine _
    & "   , MAIN.SYS_ENT_DATE                  AS SYS_ENT_DATE                  " & vbNewLine _
    & "   , MAIN.SYS_ENT_TIME                  AS SYS_ENT_TIME                  " & vbNewLine _
    & "   , MAIN.SYS_ENT_PGID                  AS SYS_ENT_PGID                  " & vbNewLine _
    & "   , MAIN.SYS_ENT_USER                  AS SYS_ENT_USER                  " & vbNewLine _
    & "   , MAIN.SYS_UPD_DATE                  AS SYS_UPD_DATE                  " & vbNewLine _
    & "   , MAIN.SYS_UPD_TIME                  AS SYS_UPD_TIME                  " & vbNewLine _
    & "   , MAIN.SYS_UPD_PGID                  AS SYS_UPD_PGID                  " & vbNewLine _
    & "   , MAIN.SYS_UPD_USER                  AS SYS_UPD_USER                  " & vbNewLine _
    & "   , MAIN.SYS_DEL_FLG                   AS SYS_DEL_FLG                   " & vbNewLine _
    & " FROM $LM_TRN$..TB_KENPIN_HEAD MAIN                                      " & vbNewLine _
    & " INNER JOIN (                                                            " & vbNewLine _
    & "     SELECT                                                              " & vbNewLine _
    & "         NRS_BR_CD                                                       " & vbNewLine _
    & "       , INKA_NO_L                                                       " & vbNewLine _
    & "       , MAX(IN_KENPIN_LOC_SEQ) AS IN_KENPIN_LOC_SEQ                     " & vbNewLine _
    & "     FROM $LM_TRN$..TB_KENPIN_HEAD                                       " & vbNewLine _
    & "     WHERE                                                               " & vbNewLine _
    & "           NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
    & "       AND INKA_NO_L = @INKA_NO_L                                        " & vbNewLine _
    & "       AND SYS_DEL_FLG = '0'                                             " & vbNewLine _
    & "     GROUP BY                                                            " & vbNewLine _
    & "         NRS_BR_CD                                                       " & vbNewLine _
    & "       , INKA_NO_L                                                       " & vbNewLine _
    & " ) SUB                                                                   " & vbNewLine _
    & "   ON  SUB.NRS_BR_CD          = MAIN.NRS_BR_CD                           " & vbNewLine _
    & "   AND SUB.INKA_NO_L          = MAIN.INKA_NO_L                           " & vbNewLine _
    & "   AND SUB.IN_KENPIN_LOC_SEQ  = MAIN.IN_KENPIN_LOC_SEQ                   " & vbNewLine _
    & " WHERE                                                                   " & vbNewLine _
    & "       MAIN.NRS_BR_CD  = @NRS_BR_CD                                      " & vbNewLine _
    & "   AND MAIN.INKA_NO_L  = @INKA_NO_L                                      " & vbNewLine _
    & "   AND MAIN.SYS_DEL_FLG = '0'                                            " & vbNewLine

#End Region

#Region "TB_KENPIN_DTL"
    Private Const SQL_SELECT_TB_KENPIN_DTL As String = " " _
& " SELECT                                                                  " & vbNewLine _
& "     MAIN.NRS_BR_CD                       AS NRS_BR_CD                   " & vbNewLine _
& "   , MAIN.INKA_NO_L                       AS INKA_NO_L                   " & vbNewLine _
& "   , MAIN.IN_KENPIN_LOC_SEQ               AS IN_KENPIN_LOC_SEQ           " & vbNewLine _
& "   , MAIN.INKA_NO_M                       AS INKA_NO_M                   " & vbNewLine _
& "   , MAIN.INKA_NO_S                       AS INKA_NO_S                   " & vbNewLine _
& "   , MAIN.KENPIN_STATE_KB                 AS KENPIN_STATE_KB             " & vbNewLine _
& "   , MAIN.LOCATION_STATE_KB               AS LOCATION_STATE_KB           " & vbNewLine _
& "   , MAIN.GOODS_CD_NRS                    AS GOODS_CD_NRS                " & vbNewLine _
& "   , MAIN.GOODS_CD_CUST                   AS GOODS_CD_CUST               " & vbNewLine _
& "   , MAIN.GOODS_NM_NRS                    AS GOODS_NM_NRS                " & vbNewLine _
& "   , MAIN.STD_IRIME_NB                    AS STD_IRIME_NB                " & vbNewLine _
& "   , MAIN.PKG_NB                          AS PKG_NB                      " & vbNewLine _
& "   , MAIN.STD_WT_KGS                      AS STD_WT_KGS                  " & vbNewLine _
& "   , MAIN.SHOBO_CD                        AS SHOBO_CD                    " & vbNewLine _
& "   , MAIN.RUI                             AS RUI                         " & vbNewLine _
& "   , MAIN.HINMEI                          AS HINMEI                      " & vbNewLine _
& "   , MAIN.NB_UT                           AS NB_UT                       " & vbNewLine _
& "   , MAIN.PKG_UT                          AS PKG_UT                      " & vbNewLine _
& "   , MAIN.TOU_NO                          AS TOU_NO                      " & vbNewLine _
& "   , MAIN.SITU_NO                         AS SITU_NO                     " & vbNewLine _
& "   , MAIN.ZONE_CD                         AS ZONE_CD                     " & vbNewLine _
& "   , MAIN.LOCA                            AS LOCA                        " & vbNewLine _
& "   , MAIN.LOT_NO                          AS LOT_NO                      " & vbNewLine _
& "   , MAIN.KONSU                           AS KONSU                       " & vbNewLine _
& "   , MAIN.HASU                            AS HASU                        " & vbNewLine _
& "   , MAIN.BETU_WT                         AS BETU_WT                     " & vbNewLine _
& "   , MAIN.IRIME                           AS IRIME                       " & vbNewLine _
& "   , MAIN.IRIME_UT                        AS IRIME_UT                    " & vbNewLine _
& "   , MAIN.SERIAL_NO                       AS SERIAL_NO                   " & vbNewLine _
& "   , MAIN.GOODS_COND_KB_1                 AS GOODS_COND_KB_1             " & vbNewLine _
& "   , MAIN.GOODS_COND_KB_2                 AS GOODS_COND_KB_2             " & vbNewLine _
& "   , MAIN.GOODS_COND_KB_3                 AS GOODS_COND_KB_3             " & vbNewLine _
& "   , MAIN.LT_DATE                         AS LT_DATE                     " & vbNewLine _
& "   , MAIN.GOODS_CRT_DATE                  AS GOODS_CRT_DATE              " & vbNewLine _
& "   , MAIN.SPD_KB                          AS SPD_KB                      " & vbNewLine _
& "   , MAIN.OFB_KB                          AS OFB_KB                      " & vbNewLine _
& "   , MAIN.DEST_CD                         AS DEST_CD                     " & vbNewLine _
& "   , MAIN.DEST_NM                         AS DEST_NM                     " & vbNewLine _
& "   , MAIN.ALLOC_PRIORITY                  AS ALLOC_PRIORITY              " & vbNewLine _
& "   , MAIN.BUG_FLG                         AS BUG_FLG                     " & vbNewLine _
& "   , MAIN.REMARK                          AS REMARK                      " & vbNewLine _
& "   , MAIN.REMARK_OUT                      AS REMARK_OUT                  " & vbNewLine _
& "   , MAIN.SYS_ENT_DATE                    AS SYS_ENT_DATE                " & vbNewLine _
& "   , MAIN.SYS_ENT_TIME                    AS SYS_ENT_TIME                " & vbNewLine _
& "   , MAIN.SYS_ENT_PGID                    AS SYS_ENT_PGID                " & vbNewLine _
& "   , MAIN.SYS_ENT_USER                    AS SYS_ENT_USER                " & vbNewLine _
& "   , MAIN.SYS_UPD_DATE                    AS SYS_UPD_DATE                " & vbNewLine _
& "   , MAIN.SYS_UPD_TIME                    AS SYS_UPD_TIME                " & vbNewLine _
& "   , MAIN.SYS_UPD_PGID                    AS SYS_UPD_PGID                " & vbNewLine _
& "   , MAIN.SYS_UPD_USER                    AS SYS_UPD_USER                " & vbNewLine _
& "   , MAIN.SYS_DEL_FLG                     AS SYS_DEL_FLG                 " & vbNewLine _
& "   , ''                                   AS IS_LOADING                  " & vbNewLine _
& "   , KBN1.KBN_NM1                         AS STD_IRIME_NM                " & vbNewLine _
& "   , CASE WHEN ISNULL(MFL.CNT,0) = 0 THEN  '00'                          " & vbNewLine _
& "          ELSE '01'                                                      " & vbNewLine _
& "     END                                  AS IMG_YN                      " & vbNewLine _
& " FROM                                                                    " & vbNewLine _
& "   $LM_TRN$..TB_KENPIN_DTL MAIN                                          " & vbNewLine _
& " INNER JOIN (                                                            " & vbNewLine _
& "     SELECT                                                              " & vbNewLine _
& "       NRS_BR_CD                                                         " & vbNewLine _
& "      ,INKA_NO_L                                                         " & vbNewLine _
& "      ,MAX(IN_KENPIN_LOC_SEQ) AS IN_KENPIN_LOC_SEQ                       " & vbNewLine _
& " 	FROM $LM_TRN$..TB_KENPIN_DTL                                        " & vbNewLine _
& "     WHERE                                                               " & vbNewLine _
& "         NRS_BR_CD   = @NRS_BR_CD                                        " & vbNewLine _
& "     AND INKA_NO_L   = @INKA_NO_L                                        " & vbNewLine _
& "     AND SYS_DEL_FLG = '0'                                               " & vbNewLine _
& "     GROUP BY                                                            " & vbNewLine _
& "       NRS_BR_CD                                                         " & vbNewLine _
& "      ,INKA_NO_L                                                         " & vbNewLine _
& " )SUB                                                                    " & vbNewLine _
& "   ON  SUB.NRS_BR_CD = MAIN.NRS_BR_CD                                    " & vbNewLine _
& "   AND SUB.INKA_NO_L = MAIN.INKA_NO_L                                    " & vbNewLine _
& "   AND SUB.IN_KENPIN_LOC_SEQ = MAIN.IN_KENPIN_LOC_SEQ                    " & vbNewLine _
& " LEFT JOIN $LM_MST$..Z_KBN KBN1                                          " & vbNewLine _
& " ON   KBN1.KBN_GROUP_CD = 'I001'                                         " & vbNewLine _
& " AND  KBN1.KBN_CD       = MAIN.IRIME_UT                                  " & vbNewLine _
& " LEFT  JOIN (                                                            " & vbNewLine _
& "     SELECT KEY_NO,COUNT(*)AS CNT                                        " & vbNewLine _
& "     FROM   LM_MST..M_FILE                                               " & vbNewLine _
& "     WHERE                                                               " & vbNewLine _
& "         ENT_SYSID_KBN = '06'                                            " & vbNewLine _
& "     AND SYS_DEL_FLG = '0'                                               " & vbNewLine _
& " 	GROUP BY KEY_NO                                                     " & vbNewLine _
& " )AS MFL                                                                 " & vbNewLine _
& " ON MFL.KEY_NO = MAIN.INKA_NO_L                                          " & vbNewLine _
& "               + MAIN.INKA_NO_M                                          " & vbNewLine _
& "               + MAIN.INKA_NO_S                                          " & vbNewLine _
& " WHERE                                                                   " & vbNewLine _
& "       MAIN.NRS_BR_CD = @NRS_BR_CD                                       " & vbNewLine _
& "   AND MAIN.INKA_NO_L = @INKA_NO_L                                       " & vbNewLine _
& " ORDER BY                                                                " & vbNewLine _
& "   MAIN.NRS_BR_CD                                                        " & vbNewLine _
& "   , MAIN.INKA_NO_L                                                      " & vbNewLine _
& "   , MAIN.IN_KENPIN_LOC_SEQ                                              " & vbNewLine _
& "   , MAIN.INKA_NO_M                                                      " & vbNewLine _
& "   , MAIN.INKA_NO_S                                                      " & vbNewLine
#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "INKA_PHOTO"

    Private Const SQL_SELECT_INKA_PHOTO As String =
          "SELECT                                        " & vbNewLine _
        & " INKA_PHOTO.NRS_BR_CD    AS NRS_BR_CD         " & vbNewLine _
        & ",INKA_PHOTO.INKA_NO_L    AS INKA_NO_L         " & vbNewLine _
        & ",INKA_PHOTO.INKA_NO_M    AS INKA_NO_M         " & vbNewLine _
        & ",INKA_PHOTO.INKA_NO_S    AS INKA_NO_S         " & vbNewLine _
        & ",INKA_PHOTO.NO           AS NO                " & vbNewLine _
        & ",PHOTO.SHOHIN_NM         AS SHOHIN_NM         " & vbNewLine _
        & ",PHOTO.SATSUEI_DATE      AS SATSUEI_DATE      " & vbNewLine _
        & ",USERHDR.USER_LNM        AS USER_LNM          " & vbNewLine _
        & ",PHOTO.SYS_UPD_DATE      AS SYS_UPD_DATE      " & vbNewLine _
        & ",PHOTO.SYS_UPD_TIME      AS SYS_UPD_TIME      " & vbNewLine _
        & ",INKA_PHOTO.FILE_PATH    AS FILE_PATH         " & vbNewLine _
        & "FROM      $LM_TRN$..B_INKA_PHOTO INKA_PHOTO   " & vbNewLine _
        & "LEFT JOIN AB_DB..NP_PHOTO PHOTO               " & vbNewLine _
        & "ON   PHOTO.NO          = INKA_PHOTO.NO        " & vbNewLine _
        & "AND  PHOTO.SYS_DEL_FLG = '0'                  " & vbNewLine _
        & "LEFT JOIN ABM_DB..M_USER_HDR USERHDR          " & vbNewLine _
        & "ON   USERHDR.USER_CD     = PHOTO.SYS_ENT_USER " & vbNewLine _
        & "AND  USERHDR.SYS_DEL_FLG = '0'                " & vbNewLine _
        & "WHERE INKA_PHOTO.NRS_BR_CD   = @NRS_BR_CD     " & vbNewLine _
        & "AND   INKA_PHOTO.INKA_NO_L   = @INKA_NO_L     " & vbNewLine _
        & "AND   INKA_PHOTO.SYS_DEL_FLG = '0'            " & vbNewLine _
        & "ORDER BY                                      " & vbNewLine _
        & " INKA_PHOTO.INKA_NO_L                         " & vbNewLine _
        & ",INKA_PHOTO.INKA_NO_M                         " & vbNewLine _
        & ",INKA_PHOTO.INKA_NO_S                         " & vbNewLine _
        & ",INKA_PHOTO.NO                                " & vbNewLine _
        & ",INKA_PHOTO.FILE_PATH                         " & vbNewLine _

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#Region "保管・荷役料最終計算日 検索処理 SQL"

    Private Const SQL_SELECT_HOKAN_NIYAKU_CALCULATION As String = "" _
        & "SELECT                                                   " & vbNewLine _
        & "      B_INKA_L.INKA_DATE                                 " & vbNewLine _
        & "    , B_INKA_L.HOKAN_STR_DATE                            " & vbNewLine _
        & "    , M_CUST.HOKAN_NIYAKU_CALCULATION                    " & vbNewLine _
        & "    , '0' AS INKA_M_EXISTS                               " & vbNewLine _
        & "FROM                                                     " & vbNewLine _
        & "    $LM_TRN$..B_INKA_L                                   " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "    $LM_TRN$..B_INKA_M                                   " & vbNewLine _
        & "        ON  B_INKA_M.NRS_BR_CD = B_INKA_L.NRS_BR_CD      " & vbNewLine _
        & "        AND B_INKA_M.INKA_NO_L = B_INKA_L.INKA_NO_L      " & vbNewLine _
        & "        AND B_INKA_M.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "    $LM_MST$..M_CUST                                     " & vbNewLine _
        & "        ON  M_CUST.NRS_BR_CD = B_INKA_L.NRS_BR_CD        " & vbNewLine _
        & "        AND M_CUST.CUST_CD_L = B_INKA_L.CUST_CD_L        " & vbNewLine _
        & "        AND M_CUST.CUST_CD_M = B_INKA_L.CUST_CD_M        " & vbNewLine _
        & "        AND M_CUST.CUST_CD_S = '00'                      " & vbNewLine _
        & "        AND M_CUST.CUST_CD_SS = '00'                     " & vbNewLine _
        & "WHERE                                                    " & vbNewLine _
        & "    B_INKA_L.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
        & "AND B_INKA_L.INKA_NO_L = @INKA_NO_L                      " & vbNewLine _
        & "AND B_INKA_M.NRS_BR_CD IS NULL                           " & vbNewLine _
        & "UNION                                                    " & vbNewLine _
        & "SELECT                                                   " & vbNewLine _
        & "      B_INKA_L.INKA_DATE                                 " & vbNewLine _
        & "    , B_INKA_L.HOKAN_STR_DATE                            " & vbNewLine _
        & "    , M_CUST.HOKAN_NIYAKU_CALCULATION                    " & vbNewLine _
        & "    , '1' AS INKA_M_EXISTS                               " & vbNewLine _
        & "FROM                                                     " & vbNewLine _
        & "    $LM_TRN$..B_INKA_L                                   " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "    $LM_TRN$..B_INKA_M                                   " & vbNewLine _
        & "        ON  B_INKA_M.NRS_BR_CD = B_INKA_L.NRS_BR_CD      " & vbNewLine _
        & "        AND B_INKA_M.INKA_NO_L = B_INKA_L.INKA_NO_L      " & vbNewLine _
        & "        AND B_INKA_M.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "    $LM_MST$..M_GOODS                                    " & vbNewLine _
        & "        ON  M_GOODS.NRS_BR_CD = B_INKA_M.NRS_BR_CD       " & vbNewLine _
        & "        AND M_GOODS.GOODS_CD_NRS = B_INKA_M.GOODS_CD_NRS " & vbNewLine _
        & "LEFT JOIN                                                " & vbNewLine _
        & "    $LM_MST$..M_CUST                                     " & vbNewLine _
        & "        ON  M_CUST.NRS_BR_CD = M_GOODS.NRS_BR_CD         " & vbNewLine _
        & "        AND M_CUST.CUST_CD_L = M_GOODS.CUST_CD_L         " & vbNewLine _
        & "        AND M_CUST.CUST_CD_M = M_GOODS.CUST_CD_M         " & vbNewLine _
        & "        AND M_CUST.CUST_CD_S = M_GOODS.CUST_CD_S         " & vbNewLine _
        & "        AND M_CUST.CUST_CD_SS = M_GOODS.CUST_CD_SS       " & vbNewLine _
        & "WHERE                                                    " & vbNewLine _
        & "    B_INKA_L.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
        & "AND B_INKA_L.INKA_NO_L = @INKA_NO_L                      " & vbNewLine _
        & "AND B_INKA_M.NRS_BR_CD IS NOT NULL                       " & vbNewLine _
        & ""

#End Region ' "保管・荷役料最終計算日 検索処理 SQL"

#End Region

#Region "入力チェック"

    Private Const SQL_HAITA_SELECT_INKA_L As String = "SELECT COUNT(INKA_NO_L) AS REC_CNT" & vbNewLine _
                                                    & "FROM $LM_TRN$..B_INKA_L           " & vbNewLine _
                                                    & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                    & "  AND INKA_NO_L    = @INKA_NO_L   " & vbNewLine _
                                                    & "  AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                    & "  AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine


    Private Const SQL_HAITA_SELECT_OUTKA_L As String = "SELECT COUNT(OUTKA_NO_L) AS REC_CNT       " & vbNewLine _
                                                     & "FROM $LM_TRN$..C_OUTKA_L                  " & vbNewLine _
                                                     & "WHERE NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
                                                     & "--要望番号:1097 yamanaka 2012.07.09 Start " & vbNewLine _
                                                     & "  AND FURI_NO      = @FURI_NO             " & vbNewLine _
                                                     & "--AND OUTKA_NO_L   = @FURI_NO             " & vbNewLine _
                                                     & "--要望番号:1097 yamanaka 2012.07.09 End   " & vbNewLine _
                                                     & "  AND SYS_UPD_DATE = @GUI_UPD_DATE        " & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_UPD_TIME        " & vbNewLine



    Private Const SQL_HAITA_SELECT_UNSO_L As String = "SELECT COUNT(UNSO_NO_L) AS REC_CNT" & vbNewLine _
                                                    & "FROM $LM_TRN$..F_UNSO_L           " & vbNewLine _
                                                    & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                    & "  AND UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                    & "  AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                    & "  AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine


    Private Const SQL_SELECT_SAGYO_CHK As String = "SELECT COUNT(SAGYO_REC_NO) AS REC_CNT" & vbNewLine _
                                                 & "FROM $LM_TRN$..E_SAGYO               " & vbNewLine _
                                                 & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
                                                 & "  AND INOUTKA_NO_LM= @INOUTKA_NO_LM  " & vbNewLine _
                                                 & "  AND SAGYO_CD     = @SAGYO_CD       " & vbNewLine

    Private Const SQL_GOODS_LOT_CHK As String = "SELECT                                         " & vbNewLine _
                                                 & "COUNT(*) AS ZAI_CNT                         " & vbNewLine _
                                                 & "FROM                                        " & vbNewLine _
                                                 & "$LM_TRN$..D_ZAI_TRS AS ZAI                  " & vbNewLine _
                                                 & "LEFT OUTER JOIN                             " & vbNewLine _
                                                 & "$LM_MST$..M_GOODS AS MG                     " & vbNewLine _
                                                 & "ON MG.NRS_BR_CD = ZAI.NRS_BR_CD             " & vbNewLine _
                                                 & "AND MG.GOODS_CD_NRS = ZAI.GOODS_CD_NRS      " & vbNewLine _
                                                 & "WHERE                                       " & vbNewLine

    Private Const SQL_GOODS_LOT_CHK_CUST_CD_L As String = "ZAI.NRS_BR_CD     = @NRS_BR_CD       " & vbNewLine _
                                                        & "AND ZAI.CUST_CD_L = @CUST_CD_L       " & vbNewLine

    Private Const SQL_GOODS_LOT_CHK_CUST_CD_L_DETAIL_PLUS As String = "ZAI.NRS_BR_CD     =    @DETAILS_NRS_BR_CD              " & vbNewLine _
                                                                    & "AND ZAI.CUST_CD_L IN ( @CUST_CD_L, @DETAILS_CUST_CD_L) " & vbNewLine

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private Const SQL_SELECT_TOU_SITU_EXP As String = "SELECT [NRS_BR_CD] AS NRS_BR_CD                          " & vbNewLine _
                                                 & "    ,[WH_CD] AS WH_CD                                       " & vbNewLine _
                                                 & "    ,[TOU_NO] AS TOU_NO                                     " & vbNewLine _
                                                 & "    ,[SITU_NO] AS SITU_NO                                   " & vbNewLine _
                                                 & "    ,[SERIAL_NO] AS SERIAL_NO                               " & vbNewLine _
                                                 & "    ,[NO_APL_GOODS_STR_RULE_APL_DATE_FROM] AS APL_DATE_FROM " & vbNewLine _
                                                 & "    ,[NO_APL_GOODS_STR_RULE_APL_DATE_TO] AS APL_DATE_TO     " & vbNewLine _
                                                 & "    ,[CUST_CD_L] AS CUST_CD_L                               " & vbNewLine _
                                                 & "FROM $LM_MST$..[M_TOU_SITU_EXP]                             " & vbNewLine _
                                                 & "WHERE [NRS_BR_CD] = @NRS_BR_CD                              " & vbNewLine _
                                                 & " AND [WH_CD] = @WH_CD                                       " & vbNewLine _
                                                 & " AND [CUST_CD_L] = @CUST_CD_L                               " & vbNewLine _
                                                 & " AND [NO_APL_GOODS_STR_RULE_APL_DATE_FROM] <= @INKA_DATE    " & vbNewLine _
                                                 & " AND [NO_APL_GOODS_STR_RULE_APL_DATE_TO] >= @INKA_DATE      " & vbNewLine _
                                                 & " AND [SYS_DEL_FLG] = '0'                                    " & vbNewLine

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    '要望番号:1511 KIM 2012/10/12 START
    '要望番号:1393 terakawa 2012.09.03 Start
    'Private Const SQL_GOODS_LOT_CHK_AFTER As String = "AND ZAI.PORA_ZAI_NB > 0                  " & vbNewLine _
    '                                             & "AND ZAI.TOU_NO = @TOU_NO                    " & vbNewLine _
    '                                             & "AND ZAI.SITU_NO = @SITU_NO                  " & vbNewLine _
    '                                             & "AND ZAI.ZONE_CD = @ZONE_CD                  " & vbNewLine _
    '                                             & "AND ZAI.LOCA = @LOCA                        " & vbNewLine _
    '                                             & "AND ((MG.GOODS_CD_CUST = @GOODS_CD_CUST AND LOT_NO <> @LOT_NO)  " & vbNewLine _
    '                                             & "OR (MG.GOODS_CD_CUST <> @GOODS_CD_CUST AND LOT_NO = @LOT_NO))   " & vbNewLine
    Private Const SQL_GOODS_LOT_CHK_AFTER As String = "AND ZAI.PORA_ZAI_NB > 0                                        " & vbNewLine _
                                                    & "AND ZAI.TOU_NO = @TOU_NO                                       " & vbNewLine _
                                                    & "AND ZAI.SITU_NO = @SITU_NO                                     " & vbNewLine _
                                                    & "AND ZAI.ZONE_CD = @ZONE_CD                                     " & vbNewLine _
                                                    & "AND ZAI.LOCA = @LOCA                                           " & vbNewLine _
                                                    & "AND (MG.GOODS_CD_CUST = @GOODS_CD_CUST AND LOT_NO <> @LOT_NO)  " & vbNewLine

    Private Const SQL_GOODS_LOT_CHK_SYS_DEL_FLG As String = "AND ZAI.SYS_DEL_FLG = '0'                               " & vbNewLine
    '要望番号:1393 terakawa 2012.09.03 End
    '要望番号:1511 KIM 2012/10/12 END

    'KASAMA 2013.10.29 WIT対応 Start
    Private Const SQL_HANDY_CUST_CHK As String = "SELECT COUNT(*) AS REC_CNT         " & vbNewLine _
                                               & "FROM $LM_MST$..M_CUST_HANDY        " & vbNewLine _
                                               & "WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
                                               & "  AND WH_CD     = @WH_CD           " & vbNewLine _
                                               & "  AND CUST_CD_L = @CUST_CD_L       " & vbNewLine _
                                               & "  -- 入荷検品WK確定更新可否フラグ  " & vbNewLine _
                                               & "  AND FLG_02 = '1'                 " & vbNewLine _
                                               & "  AND SYS_DEL_FLG = '0'            " & vbNewLine
    'KASAMA 2013.10.29 WIT対応 End

#End Region

#End Region

#Region "設定処理 SQL"

#Region "Insert"

#Region "INKA_L"

    Private Const SQL_INSERT_INKA_L As String = "INSERT INTO $LM_TRN$..B_INKA_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",FURI_NO                      " & vbNewLine _
                                              & ",INKA_TP                      " & vbNewLine _
                                              & ",INKA_KB                      " & vbNewLine _
                                              & ",INKA_STATE_KB                " & vbNewLine _
                                              & ",INKA_DATE                    " & vbNewLine _
                                              & ",STORAGE_DUE_DATE  --ADD 20170710 " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",INKA_PLAN_QT                 " & vbNewLine _
                                              & ",INKA_PLAN_QT_UT              " & vbNewLine _
                                              & ",INKA_TTL_NB                  " & vbNewLine _
                                              & ",BUYER_ORD_NO_L               " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_L          " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN               " & vbNewLine _
                                              & ",HOKAN_YN                     " & vbNewLine _
                                              & ",HOKAN_FREE_KIKAN             " & vbNewLine _
                                              & ",HOKAN_STR_DATE               " & vbNewLine _
                                              & ",NIYAKU_YN                    " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",CHECKLIST_PRT_DATE           " & vbNewLine _
                                              & ",CHECKLIST_PRT_USER           " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_DATE        " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_USER        " & vbNewLine _
                                              & ",UKETSUKE_DATE                " & vbNewLine _
                                              & ",UKETSUKE_USER                " & vbNewLine _
                                              & ",KEN_DATE                     " & vbNewLine _
                                              & ",KEN_USER                     " & vbNewLine _
                                              & ",INKO_DATE                    " & vbNewLine _
                                              & ",INKO_USER                    " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_DATE           " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_USER           " & vbNewLine _
                                              & ",UNCHIN_TP                    " & vbNewLine _
                                              & ",UNCHIN_KB                    " & vbNewLine _
                                              & ",WH_KENPIN_WK_STATUS          " & vbNewLine _
                                              & ",WH_TAB_STATUS                " & vbNewLine _
                                              & ",WH_TAB_YN                    " & vbNewLine _
                                              & ",WH_TAB_IMP_YN                " & vbNewLine _
                                              & "--DEL 2019/10/10 要望管理007373  ,STOP_ALLOC         --ADD 2019/08/01 要望管理005237" & vbNewLine _
                                              & ",WH_TAB_NO_SIJI_FLG           " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@FURI_NO                     " & vbNewLine _
                                              & ",@INKA_TP                     " & vbNewLine _
                                              & ",@INKA_KB                     " & vbNewLine _
                                              & ",@INKA_STATE_KB               " & vbNewLine _
                                              & ",@INKA_DATE                   " & vbNewLine _
                                              & ",@STORAGE_DUE_DATE  --ADD 20170710 " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@INKA_PLAN_QT                " & vbNewLine _
                                              & ",@INKA_PLAN_QT_UT             " & vbNewLine _
                                              & ",@INKA_TTL_NB                 " & vbNewLine _
                                              & ",@BUYER_ORD_NO_L              " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_L         " & vbNewLine _
                                              & ",@TOUKI_HOKAN_YN              " & vbNewLine _
                                              & ",@HOKAN_YN                    " & vbNewLine _
                                              & ",@HOKAN_FREE_KIKAN            " & vbNewLine _
                                              & ",@HOKAN_STR_DATE              " & vbNewLine _
                                              & ",@NIYAKU_YN                   " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",@CHECKLIST_PRT_DATE          " & vbNewLine _
                                              & ",@CHECKLIST_PRT_USER          " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",@UNCHIN_TP                   " & vbNewLine _
                                              & ",@UNCHIN_KB                   " & vbNewLine _
                                              & ",@WH_KENPIN_WK_STATUS         " & vbNewLine _
                                              & ",@WH_TAB_STATUS               " & vbNewLine _
                                              & ",@WH_TAB_YN                   " & vbNewLine _
                                              & ",@WH_TAB_IMP_YN               " & vbNewLine _
                                              & "--DEL 2019/10/10 要望管理007373  ,@STOP_ALLOC        --ADD 2019/08/01 要望管理005237" & vbNewLine _
                                              & ",@WH_TAB_NO_SIJI_FLG          " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "INKA_M"

    Private Const SQL_INSERT_INKA_M As String = "INSERT INTO $LM_TRN$..B_INKA_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",INKA_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_M          " & vbNewLine _
                                              & ",BUYER_ORD_NO_M               " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",PRINT_SORT                   " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@INKA_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_M         " & vbNewLine _
                                              & ",@BUYER_ORD_NO_M              " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@PRINT_SORT                  " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "INKA_S"

    Private Const SQL_INSERT_INKA_S As String = "INSERT INTO $LM_TRN$..B_INKA_S" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",INKA_NO_M                    " & vbNewLine _
                                              & ",INKA_NO_S                    " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",LOCA                         " & vbNewLine _
                                              & ",TOU_NO                       " & vbNewLine _
                                              & ",SITU_NO                      " & vbNewLine _
                                              & ",ZONE_CD                      " & vbNewLine _
                                              & ",KONSU                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SERIAL_NO                    " & vbNewLine _
                                              & ",GOODS_COND_KB_1              " & vbNewLine _
                                              & ",GOODS_COND_KB_2              " & vbNewLine _
                                              & ",GOODS_COND_KB_3              " & vbNewLine _
                                              & ",GOODS_CRT_DATE               " & vbNewLine _
                                              & ",LT_DATE                      " & vbNewLine _
                                              & ",SPD_KB                       " & vbNewLine _
                                              & ",OFB_KB                       " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",ALLOC_PRIORITY               " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",BUG_YN                       " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@INKA_NO_M                   " & vbNewLine _
                                              & ",@INKA_NO_S                   " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@LOCA                        " & vbNewLine _
                                              & ",@TOU_NO                      " & vbNewLine _
                                              & ",@SITU_NO                     " & vbNewLine _
                                              & ",@ZONE_CD                     " & vbNewLine _
                                              & ",@KONSU                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SERIAL_NO                   " & vbNewLine _
                                              & ",@GOODS_COND_KB_1             " & vbNewLine _
                                              & ",@GOODS_COND_KB_2             " & vbNewLine _
                                              & ",@GOODS_COND_KB_3             " & vbNewLine _
                                              & ",@GOODS_CRT_DATE              " & vbNewLine _
                                              & ",@LT_DATE                     " & vbNewLine _
                                              & ",@SPD_KB                      " & vbNewLine _
                                              & ",@OFB_KB                      " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@ALLOC_PRIORITY              " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",@BUG_YN                      " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "UNSO_L"

    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $LM_TRN$..F_UNSO_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",YUSO_BR_CD                   " & vbNewLine _
                                              & ",INOUTKA_NO_L                 " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",JIYU_KB                      " & vbNewLine _
                                              & ",DENP_NO                      " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",ARR_ACT_TIME                 " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_REF_NO                  " & vbNewLine _
                                              & ",SHIP_CD                      " & vbNewLine _
                                              & ",ORIG_CD                      " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB             " & vbNewLine _
                                              & ",VCLE_KB                      " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD               " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD              " & vbNewLine _
                                              & ",AD_3                         " & vbNewLine _
                                              & ",UNSO_TEHAI_KB                " & vbNewLine _
                                              & ",BUY_CHU_NO                   " & vbNewLine _
                                              & ",AREA_CD                      " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG             " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD              " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD              " & vbNewLine _
                                              & ",TRIP_NO_SYUKA                " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI               " & vbNewLine _
                                              & ",TRIP_NO_HAIKA                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start" & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD           " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD          " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@YUSO_BR_CD                  " & vbNewLine _
                                              & ",@INOUTKA_NO_L                " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@JIYU_KB                     " & vbNewLine _
                                              & ",@DENP_NO                     " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@OUTKA_PLAN_TIME             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@ARR_ACT_TIME                " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_REF_NO                 " & vbNewLine _
                                              & ",@SHIP_CD                     " & vbNewLine _
                                              & ",@ORIG_CD                     " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",@VCLE_KB                     " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD              " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD             " & vbNewLine _
                                              & ",@AD_3                        " & vbNewLine _
                                              & ",@UNSO_TEHAI_KB               " & vbNewLine _
                                              & ",@BUY_CHU_NO                  " & vbNewLine _
                                              & ",@AREA_CD                     " & vbNewLine _
                                              & ",@TYUKEI_HAISO_FLG            " & vbNewLine _
                                              & ",@SYUKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@HAIKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@TRIP_NO_SYUKA               " & vbNewLine _
                                              & ",@TRIP_NO_TYUKEI              " & vbNewLine _
                                              & ",@TRIP_NO_HAIKA               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start" & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD          " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD         " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End  " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "UNSO_M"

    Private Const SQL_INSERT_UNSO_M As String = "INSERT INTO $LM_TRN$..F_UNSO_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",UNSO_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM                     " & vbNewLine _
                                              & ",UNSO_TTL_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_TTL_QT                  " & vbNewLine _
                                              & ",QT_UT                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",IRIME_UT                     " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SIZE_KB                      " & vbNewLine _
                                              & ",ZBUKA_CD                     " & vbNewLine _
                                              & ",ABUKA_CD                     " & vbNewLine _
                                              & ",PKG_NB                       " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@UNSO_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM                    " & vbNewLine _
                                              & ",@UNSO_TTL_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_TTL_QT                 " & vbNewLine _
                                              & ",@QT_UT                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@IRIME_UT                    " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SIZE_KB                     " & vbNewLine _
                                              & ",@ZBUKA_CD                    " & vbNewLine _
                                              & ",@ABUKA_CD                    " & vbNewLine _
                                              & ",@PKG_NB                      " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_INSERT_UNCHIN As String = "INSERT INTO $LM_TRN$..F_UNCHIN_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SEIQ_GROUP_NO                    " & vbNewLine _
                                              & ",SEIQ_GROUP_NO_M                  " & vbNewLine _
                                              & ",SEIQTO_CD                        " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SEIQ_SYARYO_KB                   " & vbNewLine _
                                              & ",SEIQ_PKG_UT                      " & vbNewLine _
                                              & ",SEIQ_NG_NB                       " & vbNewLine _
                                              & ",SEIQ_DANGER_KB                   " & vbNewLine _
                                              & ",SEIQ_TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD                   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD                  " & vbNewLine _
                                              & ",SEIQ_KYORI                       " & vbNewLine _
                                              & ",SEIQ_WT                          " & vbNewLine _
                                              & ",SEIQ_UNCHIN                      " & vbNewLine _
                                              & ",SEIQ_CITY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_WINT_EXTC                   " & vbNewLine _
                                              & ",SEIQ_RELY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_TOLL                        " & vbNewLine _
                                              & ",SEIQ_INSU                        " & vbNewLine _
                                              & ",SEIQ_FIXED_FLAG                  " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO                   " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO_M                 " & vbNewLine _
                                              & ",@SEIQTO_CD                       " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SEIQ_SYARYO_KB                  " & vbNewLine _
                                              & ",@SEIQ_PKG_UT                     " & vbNewLine _
                                              & ",@SEIQ_NG_NB                      " & vbNewLine _
                                              & ",@SEIQ_DANGER_KB                  " & vbNewLine _
                                              & ",@SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD                  " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD                 " & vbNewLine _
                                              & ",@SEIQ_KYORI                      " & vbNewLine _
                                              & ",@SEIQ_WT                         " & vbNewLine _
                                              & ",@SEIQ_UNCHIN                     " & vbNewLine _
                                              & ",@SEIQ_CITY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_WINT_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_RELY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_TOLL                       " & vbNewLine _
                                              & ",@SEIQ_INSU                       " & vbNewLine _
                                              & ",@SEIQ_FIXED_FLAG                 " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' SHIHARAI INSERT用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SHIHARAI_INSERT As String = "INSERT INTO $LM_TRN$..F_SHIHARAI_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO                " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO_M              " & vbNewLine _
                                              & ",SHIHARAITO_CD                    " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SHIHARAI_SYARYO_KB               " & vbNewLine _
                                              & ",SHIHARAI_PKG_UT                  " & vbNewLine _
                                              & ",SHIHARAI_NG_NB                   " & vbNewLine _
                                              & ",SHIHARAI_DANGER_KB               " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_BUNRUI_KB        " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD               " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD              " & vbNewLine _
                                              & ",SHIHARAI_KYORI                   " & vbNewLine _
                                              & ",SHIHARAI_WT                      " & vbNewLine _
                                              & ",SHIHARAI_UNCHIN                  " & vbNewLine _
                                              & ",SHIHARAI_CITY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_WINT_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_RELY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_TOLL                    " & vbNewLine _
                                              & ",SHIHARAI_INSU                    " & vbNewLine _
                                              & ",SHIHARAI_FIXED_FLAG              " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO               " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO_M             " & vbNewLine _
                                              & ",@SHIHARAITO_CD                   " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SHIHARAI_SYARYO_KB              " & vbNewLine _
                                              & ",@SHIHARAI_PKG_UT                 " & vbNewLine _
                                              & ",@SHIHARAI_NG_NB                  " & vbNewLine _
                                              & ",@SHIHARAI_DANGER_KB              " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_BUNRUI_KB       " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD              " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD             " & vbNewLine _
                                              & ",@SHIHARAI_KYORI                  " & vbNewLine _
                                              & ",@SHIHARAI_WT                     " & vbNewLine _
                                              & ",@SHIHARAI_UNCHIN                 " & vbNewLine _
                                              & ",@SHIHARAI_CITY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_WINT_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_RELY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_TOLL                   " & vbNewLine _
                                              & ",@SHIHARAI_INSU                   " & vbNewLine _
                                              & ",@SHIHARAI_FIXED_FLAG             " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


#End Region

#Region "SAGYO"

    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO $LM_TRN$..E_SAGYO  " & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",SAGYO_REC_NO                 " & vbNewLine _
                                              & ",SAGYO_COMP                   " & vbNewLine _
                                              & ",SKYU_CHK                     " & vbNewLine _
                                              & ",SAGYO_SIJI_NO                " & vbNewLine _
                                              & ",INOUTKA_NO_LM                " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",IOZS_KB                      " & vbNewLine _
                                              & ",SAGYO_CD                     " & vbNewLine _
                                              & ",SAGYO_NM                     " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",DEST_NM                      " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM_NRS                 " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",INV_TANI                     " & vbNewLine _
                                              & ",SAGYO_NB                     " & vbNewLine _
                                              & ",SAGYO_UP                     " & vbNewLine _
                                              & ",SAGYO_GK                     " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",SEIQTO_CD                    " & vbNewLine _
                                              & ",REMARK_ZAI                   " & vbNewLine _
                                              & ",REMARK_SKYU                  " & vbNewLine _
                                              & ",REMARK_SIJI                  " & vbNewLine _
                                              & ",SAGYO_COMP_DATE              " & vbNewLine _
                                              & ",DEST_SAGYO_FLG               " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@SAGYO_REC_NO                " & vbNewLine _
                                              & ",@SAGYO_COMP                  " & vbNewLine _
                                              & ",@SKYU_CHK                    " & vbNewLine _
                                              & ",@SAGYO_SIJI_NO               " & vbNewLine _
                                              & ",@INOUTKA_NO_LM               " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@IOZS_KB                     " & vbNewLine _
                                              & ",@SAGYO_CD                    " & vbNewLine _
                                              & ",@SAGYO_NM                    " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@DEST_NM                     " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM_NRS                " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@INV_TANI                    " & vbNewLine _
                                              & ",@SAGYO_NB                    " & vbNewLine _
                                              & ",@SAGYO_UP                    " & vbNewLine _
                                              & ",@SAGYO_GK                    " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@SEIQTO_CD                   " & vbNewLine _
                                              & ",@REMARK_ZAI                  " & vbNewLine _
                                              & ",@REMARK_SKYU                 " & vbNewLine _
                                              & ",@REMARK_SIJI                 " & vbNewLine _
                                              & ",@SAGYO_COMP_DATE             " & vbNewLine _
                                              & ",@DEST_SAGYO_FLG              " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "ZAI_TRS"

    Private Const SQL_INSERT_ZAI_TRS As String = "INSERT INTO $LM_TRN$..D_ZAI_TRS" & vbNewLine _
                                               & "(                              " & vbNewLine _
                                               & " NRS_BR_CD                     " & vbNewLine _
                                               & ",ZAI_REC_NO                    " & vbNewLine _
                                               & ",WH_CD                         " & vbNewLine _
                                               & ",TOU_NO                        " & vbNewLine _
                                               & ",SITU_NO                       " & vbNewLine _
                                               & ",ZONE_CD                       " & vbNewLine _
                                               & ",LOCA                          " & vbNewLine _
                                               & ",LOT_NO                        " & vbNewLine _
                                               & ",CUST_CD_L                     " & vbNewLine _
                                               & ",CUST_CD_M                     " & vbNewLine _
                                               & ",GOODS_CD_NRS                  " & vbNewLine _
                                               & ",GOODS_KANRI_NO                " & vbNewLine _
                                               & ",INKA_NO_L                     " & vbNewLine _
                                               & ",INKA_NO_M                     " & vbNewLine _
                                               & ",INKA_NO_S                     " & vbNewLine _
                                               & ",ALLOC_PRIORITY                " & vbNewLine _
                                               & ",RSV_NO                        " & vbNewLine _
                                               & ",SERIAL_NO                     " & vbNewLine _
                                               & ",HOKAN_YN                      " & vbNewLine _
                                               & ",TAX_KB                        " & vbNewLine _
                                               & ",GOODS_COND_KB_1               " & vbNewLine _
                                               & ",GOODS_COND_KB_2               " & vbNewLine _
                                               & ",GOODS_COND_KB_3               " & vbNewLine _
                                               & ",OFB_KB                        " & vbNewLine _
                                               & ",SPD_KB                        " & vbNewLine _
                                               & ",REMARK_OUT                    " & vbNewLine _
                                               & ",PORA_ZAI_NB                   " & vbNewLine _
                                               & ",ALCTD_NB                      " & vbNewLine _
                                               & ",ALLOC_CAN_NB                  " & vbNewLine _
                                               & ",IRIME                         " & vbNewLine _
                                               & ",PORA_ZAI_QT                   " & vbNewLine _
                                               & ",ALCTD_QT                      " & vbNewLine _
                                               & ",ALLOC_CAN_QT                  " & vbNewLine _
                                               & ",INKO_DATE                     " & vbNewLine _
                                               & ",INKO_PLAN_DATE                " & vbNewLine _
                                               & ",ZERO_FLAG                     " & vbNewLine _
                                               & ",LT_DATE                       " & vbNewLine _
                                               & ",GOODS_CRT_DATE                " & vbNewLine _
                                               & ",DEST_CD_P                     " & vbNewLine _
                                               & ",REMARK                        " & vbNewLine _
                                               & ",SMPL_FLAG                     " & vbNewLine _
                                               & ",SYS_ENT_DATE                  " & vbNewLine _
                                               & ",SYS_ENT_TIME                  " & vbNewLine _
                                               & ",SYS_ENT_PGID                  " & vbNewLine _
                                               & ",SYS_ENT_USER                  " & vbNewLine _
                                               & ",SYS_UPD_DATE                  " & vbNewLine _
                                               & ",SYS_UPD_TIME                  " & vbNewLine _
                                               & ",SYS_UPD_PGID                  " & vbNewLine _
                                               & ",SYS_UPD_USER                  " & vbNewLine _
                                               & ",SYS_DEL_FLG                   " & vbNewLine _
                                               & " )VALUES(                      " & vbNewLine _
                                               & " @NRS_BR_CD                    " & vbNewLine _
                                               & ",@ZAI_REC_NO                   " & vbNewLine _
                                               & ",@WH_CD                        " & vbNewLine _
                                               & ",@TOU_NO                       " & vbNewLine _
                                               & ",@SITU_NO                      " & vbNewLine _
                                               & ",@ZONE_CD                      " & vbNewLine _
                                               & ",@LOCA                         " & vbNewLine _
                                               & ",@LOT_NO                       " & vbNewLine _
                                               & ",@CUST_CD_L                    " & vbNewLine _
                                               & ",@CUST_CD_M                    " & vbNewLine _
                                               & ",@GOODS_CD_NRS                 " & vbNewLine _
                                               & ",@GOODS_KANRI_NO               " & vbNewLine _
                                               & ",@INKA_NO_L                    " & vbNewLine _
                                               & ",@INKA_NO_M                    " & vbNewLine _
                                               & ",@INKA_NO_S                    " & vbNewLine _
                                               & ",@ALLOC_PRIORITY               " & vbNewLine _
                                               & ",@RSV_NO                       " & vbNewLine _
                                               & ",@SERIAL_NO                    " & vbNewLine _
                                               & ",@HOKAN_YN                     " & vbNewLine _
                                               & ",@TAX_KB                       " & vbNewLine _
                                               & ",@GOODS_COND_KB_1              " & vbNewLine _
                                               & ",@GOODS_COND_KB_2              " & vbNewLine _
                                               & ",@GOODS_COND_KB_3              " & vbNewLine _
                                               & ",@OFB_KB                       " & vbNewLine _
                                               & ",@SPD_KB                       " & vbNewLine _
                                               & ",@REMARK_OUT                   " & vbNewLine _
                                               & ",@PORA_ZAI_NB                  " & vbNewLine _
                                               & ",@ALCTD_NB                     " & vbNewLine _
                                               & ",@ALLOC_CAN_NB                 " & vbNewLine _
                                               & ",@IRIME                        " & vbNewLine _
                                               & ",@PORA_ZAI_QT                  " & vbNewLine _
                                               & ",@ALCTD_QT                     " & vbNewLine _
                                               & ",@ALLOC_CAN_QT                 " & vbNewLine _
                                               & ",@INKO_DATE                    " & vbNewLine _
                                               & ",@INKO_PLAN_DATE               " & vbNewLine _
                                               & ",@ZERO_FLAG                    " & vbNewLine _
                                               & ",@LT_DATE                      " & vbNewLine _
                                               & ",@GOODS_CRT_DATE               " & vbNewLine _
                                               & ",@DEST_CD_P                    " & vbNewLine _
                                               & ",@REMARK                       " & vbNewLine _
                                               & ",@SMPL_FLAG                    " & vbNewLine _
                                               & ",@SYS_ENT_DATE                 " & vbNewLine _
                                               & ",@SYS_ENT_TIME                 " & vbNewLine _
                                               & ",@SYS_ENT_PGID                 " & vbNewLine _
                                               & ",@SYS_ENT_USER                 " & vbNewLine _
                                               & ",@SYS_UPD_DATE                 " & vbNewLine _
                                               & ",@SYS_UPD_TIME                 " & vbNewLine _
                                               & ",@SYS_UPD_PGID                 " & vbNewLine _
                                               & ",@SYS_UPD_USER                 " & vbNewLine _
                                               & ",@SYS_DEL_FLG                  " & vbNewLine _
                                               & ")                              " & vbNewLine

#End Region

#Region "M_FILE"

    Private Const SQL_INSERT_M_FILE As String = _
      " INSERT INTO LM_MST..M_FILE(                                         " & vbNewLine _
    & "  KEY_TYPE_KBN                                                       " & vbNewLine _
    & " ,KEY_NO                                                             " & vbNewLine _
    & " ,KEY_NO_SUB                                                         " & vbNewLine _
    & " ,KEY_NO_SEQ                                                         " & vbNewLine _
    & " ,FILE_TYPE_KBN                                                      " & vbNewLine _
    & " ,CONT_TYPE_KBN                                                      " & vbNewLine _
    & " ,CONT_NO                                                            " & vbNewLine _
    & " ,REMARK                                                             " & vbNewLine _
    & " ,FILE_PATH                                                          " & vbNewLine _
    & " ,FILE_NM                                                            " & vbNewLine _
    & " ,ENT_SYSID_KBN                                                      " & vbNewLine _
    & " ,SYS_ENT_DATE                                                       " & vbNewLine _
    & " ,SYS_ENT_TIME                                                       " & vbNewLine _
    & " ,SYS_ENT_PGID                                                       " & vbNewLine _
    & " ,SYS_ENT_USER                                                       " & vbNewLine _
    & " ,SYS_UPD_DATE                                                       " & vbNewLine _
    & " ,SYS_UPD_TIME                                                       " & vbNewLine _
    & " ,SYS_UPD_PGID                                                       " & vbNewLine _
    & " ,SYS_UPD_USER                                                       " & vbNewLine _
    & " ,SYS_DEL_FLG                                                        " & vbNewLine _
    & " )                                                                   " & vbNewLine _
    & " SELECT                                                              " & vbNewLine _
    & "  KEY_TYPE                                      AS KEY_TYPE_KBN      " & vbNewLine _
    & " ,CONTROL_NO_L + CONTROL_NO_M + CONTROL_NO_S    AS KEY_NO            " & vbNewLine _
    & " ,''                                            AS KEY_NO_SUB        " & vbNewLine _
    & " ,0                                             AS KEY_NO_SEQ        " & vbNewLine _
    & " ,FILE_TYPE                                     AS FILE_TYPE_KBN     " & vbNewLine _
    & " ,''                                            AS CONT_TYPE_KBN     " & vbNewLine _
    & " ,''                                            AS CONT_NO           " & vbNewLine _
    & " ,REMARK                                        AS REMARK            " & vbNewLine _
    & " ,FILE_PATH                                     AS FILE_PATH         " & vbNewLine _
    & " ,FILE_NM                                       AS FILE_NM           " & vbNewLine _
    & " ,'06'                                          AS ENT_SYSID_KBN     " & vbNewLine _
    & " ,@SYS_ENT_DATE                                 AS SYS_ENT_DATE      " & vbNewLine _
    & " ,@SYS_ENT_TIME                                 AS SYS_ENT_TIME      " & vbNewLine _
    & " ,@SYS_ENT_PGID                                 AS SYS_ENT_PGID      " & vbNewLine _
    & " ,@SYS_ENT_USER                                 AS SYS_ENT_USER      " & vbNewLine _
    & " ,@SYS_UPD_DATE                                 AS SYS_UPD_DATE      " & vbNewLine _
    & " ,@SYS_UPD_TIME                                 AS SYS_UPD_TIME      " & vbNewLine _
    & " ,@SYS_UPD_PGID                                 AS SYS_UPD_PGID      " & vbNewLine _
    & " ,@SYS_UPD_USER                                 AS SYS_UPD_USER      " & vbNewLine _
    & " ,@SYS_DEL_FLG                                  AS SYS_DEL_FLG       " & vbNewLine _
    & " FROM  $LM_TRN$..TZ_FILE                                             " & vbNewLine _
    & " WHERE NRS_BR_CD    = @NRS_BR_CD                                     " & vbNewLine _
    & " AND   FILE_TYPE    = @FILE_TYPE                                     " & vbNewLine _
    & " AND   KEY_TYPE     = @KEY_TYPE                                      " & vbNewLine _
    & " AND   CONTROL_NO_L = @CONTROL_NO_L                                  " & vbNewLine _
    & " AND   CONTROL_SEQ IN (SELECT                                        " & vbNewLine _
    & "                       MAX(CONTROL_SEQ) AS CONTROL_SEQ               " & vbNewLine _
    & "                       FROM $LM_TRN$..TZ_FILE                        " & vbNewLine _
    & "                       WHERE NRS_BR_CD    = @NRS_BR_CD               " & vbNewLine _
    & "                       AND   FILE_TYPE    = @FILE_TYPE               " & vbNewLine _
    & "                       AND   KEY_TYPE     = @KEY_TYPE                " & vbNewLine _
    & "                       AND   CONTROL_NO_L = @CONTROL_NO_L            " & vbNewLine _
    & "                       GROUP BY                                      " & vbNewLine _
    & "                       FILE_TYPE,KEY_TYPE,NRS_BR_CD,CONTROL_NO_L)    " & vbNewLine _
    & " AND   SYS_DEL_FLG = '0'                                             " & vbNewLine

#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "INKA_PHOTO"

    Private Const SQL_INSERT_INKA_PHOTO As String =
          "INSERT INTO $LM_TRN$..B_INKA_PHOTO" & vbNewLine _
        & "(                                 " & vbNewLine _
        & " NRS_BR_CD                        " & vbNewLine _
        & ",INKA_NO_L                        " & vbNewLine _
        & ",INKA_NO_M                        " & vbNewLine _
        & ",INKA_NO_S                        " & vbNewLine _
        & ",NO                               " & vbNewLine _
        & ",FILE_PATH                        " & vbNewLine _
        & ",SYS_ENT_DATE                     " & vbNewLine _
        & ",SYS_ENT_TIME                     " & vbNewLine _
        & ",SYS_ENT_PGID                     " & vbNewLine _
        & ",SYS_ENT_USER                     " & vbNewLine _
        & ",SYS_UPD_DATE                     " & vbNewLine _
        & ",SYS_UPD_TIME                     " & vbNewLine _
        & ",SYS_UPD_PGID                     " & vbNewLine _
        & ",SYS_UPD_USER                     " & vbNewLine _
        & ",SYS_DEL_FLG                      " & vbNewLine _
        & " )VALUES(                         " & vbNewLine _
        & " @NRS_BR_CD                       " & vbNewLine _
        & ",@INKA_NO_L                       " & vbNewLine _
        & ",@INKA_NO_M                       " & vbNewLine _
        & ",@INKA_NO_S                       " & vbNewLine _
        & ",@NO                              " & vbNewLine _
        & ",@FILE_PATH                       " & vbNewLine _
        & ",@SYS_ENT_DATE                    " & vbNewLine _
        & ",@SYS_ENT_TIME                    " & vbNewLine _
        & ",@SYS_ENT_PGID                    " & vbNewLine _
        & ",@SYS_ENT_USER                    " & vbNewLine _
        & ",@SYS_UPD_DATE                    " & vbNewLine _
        & ",@SYS_UPD_TIME                    " & vbNewLine _
        & ",@SYS_UPD_PGID                    " & vbNewLine _
        & ",@SYS_UPD_USER                    " & vbNewLine _
        & ",@SYS_DEL_FLG                     " & vbNewLine _
        & ")                                 " & vbNewLine

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#End Region

#Region "Update"

#Region "INKA_L"

    Private Const SQL_UPDATE_INKA_L As String = "UPDATE $LM_TRN$..B_INKA_L SET                " & vbNewLine _
                                              & " FURI_NO               = @FURI_NO            " & vbNewLine _
                                              & ",INKA_TP               = @INKA_TP            " & vbNewLine _
                                              & ",INKA_KB               = @INKA_KB            " & vbNewLine _
                                              & ",INKA_STATE_KB         = @INKA_STATE_KB      " & vbNewLine _
                                              & ",INKA_DATE             = @INKA_DATE          " & vbNewLine _
                                              & ",STORAGE_DUE_DATE      = @STORAGE_DUE_DATE  --ADD 20170710 " & vbNewLine _
                                              & ",WH_CD                 = @WH_CD              " & vbNewLine _
                                              & ",CUST_CD_L             = @CUST_CD_L          " & vbNewLine _
                                              & ",CUST_CD_M             = @CUST_CD_M          " & vbNewLine _
                                              & ",INKA_PLAN_QT          = @INKA_PLAN_QT       " & vbNewLine _
                                              & ",INKA_PLAN_QT_UT       = @INKA_PLAN_QT_UT    " & vbNewLine _
                                              & ",INKA_TTL_NB           = @INKA_TTL_NB        " & vbNewLine _
                                              & ",BUYER_ORD_NO_L        = @BUYER_ORD_NO_L     " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_L   = @OUTKA_FROM_ORD_NO_L" & vbNewLine _
                                              & ",TOUKI_HOKAN_YN        = @TOUKI_HOKAN_YN     " & vbNewLine _
                                              & ",HOKAN_YN              = @HOKAN_YN           " & vbNewLine _
                                              & ",HOKAN_FREE_KIKAN      = @HOKAN_FREE_KIKAN   " & vbNewLine _
                                              & ",HOKAN_STR_DATE        = @HOKAN_STR_DATE     " & vbNewLine _
                                              & ",NIYAKU_YN             = @NIYAKU_YN          " & vbNewLine _
                                              & ",TAX_KB                = @TAX_KB             " & vbNewLine _
                                              & ",REMARK                = @REMARK             " & vbNewLine _
                                              & ",REMARK_OUT            = @REMARK_OUT         " & vbNewLine _
                                              & ",UNCHIN_TP             = @UNCHIN_TP          " & vbNewLine _
                                              & ",UNCHIN_KB             = @UNCHIN_KB          " & vbNewLine _
                                              & ",WH_KENPIN_WK_STATUS   = @WH_KENPIN_WK_STATUS" & vbNewLine _
                                              & ",WH_TAB_STATUS         = @WH_TAB_STATUS      " & vbNewLine _
                                              & ",WH_TAB_YN             = @WH_TAB_YN          " & vbNewLine _
                                              & ",WH_TAB_IMP_YN         = @WH_TAB_IMP_YN      " & vbNewLine _
                                              & "--DEL 2019/10/10 要望管理007373  ,STOP_ALLOC            = @STOP_ALLOC         --ADD 2019/08/01 要望管理005237" & vbNewLine _
                                              & ",WH_TAB_NO_SIJI_FLG    = @WH_TAB_NO_SIJI_FLG " & vbNewLine _
                                              & ",SYS_UPD_DATE          = @SYS_UPD_DATE       " & vbNewLine _
                                              & ",SYS_UPD_TIME          = @SYS_UPD_TIME       " & vbNewLine _
                                              & ",SYS_UPD_PGID          = @SYS_UPD_PGID       " & vbNewLine _
                                              & ",SYS_UPD_USER          = @SYS_UPD_USER       " & vbNewLine _
                                              & "WHERE   NRS_BR_CD      = @NRS_BR_CD          " & vbNewLine _
                                              & "  AND   INKA_NO_L      = @INKA_NO_L          " & vbNewLine


    Private Const SQL_UPDATE_INKA_L_PRINT As String = "UPDATE $LM_TRN$..B_INKA_L SET           " & vbNewLine _
                                                    & " $UPDATE1$             = @SYS_UPD_DATE  " & vbNewLine _
                                                    & ",$UPDATE2$             = @SYS_UPD_USER  " & vbNewLine _
                                                    & ",SYS_UPD_DATE          = @SYS_UPD_DATE  " & vbNewLine _
                                                    & ",SYS_UPD_TIME          = @SYS_UPD_TIME  " & vbNewLine _
                                                    & ",SYS_UPD_PGID          = @SYS_UPD_PGID  " & vbNewLine _
                                                    & ",SYS_UPD_USER          = @SYS_UPD_USER  " & vbNewLine _
                                                    & ",INKA_STATE_KB         = @INKA_STATE_KB " & vbNewLine _
                                                    & "WHERE   NRS_BR_CD      = @NRS_BR_CD     " & vbNewLine _
                                                    & "  AND   INKA_NO_L      = @INKA_NO_L     " & vbNewLine


    Private Const SQL_UPDATE_KISANDATE As String = "UPDATE $LM_TRN$..B_INKA_L SET      " & vbNewLine _
                                                 & " HOKAN_STR_DATE   = @HOKAN_STR_DATE" & vbNewLine _
                                                 & ",SYS_UPD_DATE     = @SYS_UPD_DATE  " & vbNewLine _
                                                 & ",SYS_UPD_TIME     = @SYS_UPD_TIME  " & vbNewLine _
                                                 & ",SYS_UPD_PGID     = @SYS_UPD_PGID  " & vbNewLine _
                                                 & ",SYS_UPD_USER     = @SYS_UPD_USER  " & vbNewLine _
                                                 & "WHERE   NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
                                                 & "  AND   INKA_NO_L = @INKA_NO_L     " & vbNewLine

#End Region

#Region "INKA_M"

    Private Const SQL_UPDATE_INKA_M As String = "UPDATE $LM_TRN$..B_INKA_M SET                " & vbNewLine _
                                              & " GOODS_CD_NRS          = @GOODS_CD_NRS       " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_M   = @OUTKA_FROM_ORD_NO_M" & vbNewLine _
                                              & ",BUYER_ORD_NO_M        = @BUYER_ORD_NO_M     " & vbNewLine _
                                              & ",REMARK                = @REMARK             " & vbNewLine _
                                              & ",PRINT_SORT            = @PRINT_SORT         " & vbNewLine _
                                              & ",SYS_UPD_DATE          = @SYS_UPD_DATE       " & vbNewLine _
                                              & ",SYS_UPD_TIME          = @SYS_UPD_TIME       " & vbNewLine _
                                              & ",SYS_UPD_PGID          = @SYS_UPD_PGID       " & vbNewLine _
                                              & ",SYS_UPD_USER          = @SYS_UPD_USER       " & vbNewLine _
                                              & "WHERE   NRS_BR_CD      = @NRS_BR_CD          " & vbNewLine _
                                              & "  AND   INKA_NO_L      = @INKA_NO_L          " & vbNewLine _
                                              & "  AND   INKA_NO_M      = @INKA_NO_M          " & vbNewLine

#End Region

#Region "INKA_S"

    Private Const SQL_UPDATE_INKA_S As String = "UPDATE $LM_TRN$..B_INKA_S SET         " & vbNewLine _
                                              & " LOT_NO             = @LOT_NO         " & vbNewLine _
                                              & ",LOCA               = @LOCA           " & vbNewLine _
                                              & ",TOU_NO             = @TOU_NO         " & vbNewLine _
                                              & ",SITU_NO            = @SITU_NO        " & vbNewLine _
                                              & ",ZONE_CD            = @ZONE_CD        " & vbNewLine _
                                              & ",KONSU              = @KONSU          " & vbNewLine _
                                              & ",HASU               = @HASU           " & vbNewLine _
                                              & ",IRIME              = @IRIME          " & vbNewLine _
                                              & ",BETU_WT            = @BETU_WT        " & vbNewLine _
                                              & ",SERIAL_NO          = @SERIAL_NO      " & vbNewLine _
                                              & ",GOODS_COND_KB_1    = @GOODS_COND_KB_1" & vbNewLine _
                                              & ",GOODS_COND_KB_2    = @GOODS_COND_KB_2" & vbNewLine _
                                              & ",GOODS_COND_KB_3    = @GOODS_COND_KB_3" & vbNewLine _
                                              & ",GOODS_CRT_DATE     = @GOODS_CRT_DATE " & vbNewLine _
                                              & ",LT_DATE            = @LT_DATE        " & vbNewLine _
                                              & ",SPD_KB             = @SPD_KB         " & vbNewLine _
                                              & ",OFB_KB             = @OFB_KB         " & vbNewLine _
                                              & ",DEST_CD            = @DEST_CD        " & vbNewLine _
                                              & ",REMARK             = @REMARK         " & vbNewLine _
                                              & ",ALLOC_PRIORITY     = @ALLOC_PRIORITY " & vbNewLine _
                                              & ",REMARK_OUT         = @REMARK_OUT     " & vbNewLine _
                                              & ",ZAI_REC_NO         = @ZAI_REC_NO     " & vbNewLine _
                                              & ",BUG_YN             = @BUG_YN         " & vbNewLine _
                                              & ",SYS_UPD_DATE       = @SYS_UPD_DATE   " & vbNewLine _
                                              & ",SYS_UPD_TIME       = @SYS_UPD_TIME   " & vbNewLine _
                                              & ",SYS_UPD_PGID       = @SYS_UPD_PGID   " & vbNewLine _
                                              & ",SYS_UPD_USER       = @SYS_UPD_USER   " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD      " & vbNewLine _
                                              & "  AND   INKA_NO_L   = @INKA_NO_L      " & vbNewLine _
                                              & "  AND   INKA_NO_M   = @INKA_NO_M      " & vbNewLine _
                                              & "  AND   INKA_NO_S   = @INKA_NO_S      " & vbNewLine

#End Region

#Region "INKA_WK"
    'START ADD 2013/09/10 KURIHARA WIT対応

    Private Const SQL_UPDATE_INKA_WK As String = "UPDATE $LM_TRN$..B_INKA_WK SET                " & vbNewLine _
                                               & " KENPIN_KAKUTEI_FLG    = @KENPIN_KAKUTEI_FLG  " & vbNewLine _
                                               & ",SYS_UPD_DATE          = @SYS_UPD_DATE        " & vbNewLine _
                                               & ",SYS_UPD_TIME          = @SYS_UPD_TIME        " & vbNewLine _
                                               & ",SYS_UPD_PGID          = @SYS_UPD_PGID        " & vbNewLine _
                                               & ",SYS_UPD_USER          = @SYS_UPD_USER        " & vbNewLine _
                                               & "WHERE   NRS_BR_CD      = @NRS_BR_CD           " & vbNewLine _
                                               & "  AND   INKA_NO_L      = @INKA_NO_L           " & vbNewLine

    'END   ADD 2013/09/10 KURIHARA WIT対応
#End Region

#Region "UNSO_L"

#Region "通常更新"

    Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET           " & vbNewLine _
                                              & " NRS_BR_CD          = @NRS_BR_CD        " & vbNewLine _
                                              & ",UNSO_NO_L          = @UNSO_NO_L        " & vbNewLine _
                                              & ",YUSO_BR_CD         = @YUSO_BR_CD       " & vbNewLine _
                                              & ",INOUTKA_NO_L       = @INOUTKA_NO_L     " & vbNewLine _
                                              & ",TRIP_NO            = @TRIP_NO          " & vbNewLine _
                                              & ",UNSO_CD            = @UNSO_CD          " & vbNewLine _
                                              & ",UNSO_BR_CD         = @UNSO_BR_CD       " & vbNewLine _
                                              & ",BIN_KB             = @BIN_KB           " & vbNewLine _
                                              & ",JIYU_KB            = @JIYU_KB          " & vbNewLine _
                                              & ",DENP_NO            = @DENP_NO          " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE    = @OUTKA_PLAN_DATE  " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME    = @OUTKA_PLAN_TIME  " & vbNewLine _
                                              & ",ARR_PLAN_DATE      = @ARR_PLAN_DATE    " & vbNewLine _
                                              & ",ARR_PLAN_TIME      = @ARR_PLAN_TIME    " & vbNewLine _
                                              & ",ARR_ACT_TIME       = @ARR_ACT_TIME     " & vbNewLine _
                                              & ",CUST_CD_L          = @CUST_CD_L        " & vbNewLine _
                                              & ",CUST_CD_M          = @CUST_CD_M        " & vbNewLine _
                                              & ",CUST_REF_NO        = @CUST_REF_NO      " & vbNewLine _
                                              & ",SHIP_CD            = @SHIP_CD          " & vbNewLine _
                                              & ",ORIG_CD            = @ORIG_CD          " & vbNewLine _
                                              & ",DEST_CD            = @DEST_CD          " & vbNewLine _
                                              & ",UNSO_PKG_NB        = @UNSO_PKG_NB      " & vbNewLine _
                                              & ",NB_UT              = @NB_UT            " & vbNewLine _
                                              & ",UNSO_WT            = @UNSO_WT          " & vbNewLine _
                                              & ",UNSO_ONDO_KB       = @UNSO_ONDO_KB     " & vbNewLine _
                                              & ",PC_KB              = @PC_KB            " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB   = @TARIFF_BUNRUI_KB " & vbNewLine _
                                              & ",VCLE_KB            = @VCLE_KB          " & vbNewLine _
                                              & ",MOTO_DATA_KB       = @MOTO_DATA_KB     " & vbNewLine _
                                              & ",TAX_KB             = @TAX_KB           " & vbNewLine _
                                              & ",REMARK             = @REMARK           " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD     = @SEIQ_TARIFF_CD   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD    = @SEIQ_ETARIFF_CD  " & vbNewLine _
                                              & ",AD_3               = @AD_3             " & vbNewLine _
                                              & ",UNSO_TEHAI_KB      = @UNSO_TEHAI_KB    " & vbNewLine _
                                              & ",BUY_CHU_NO         = @BUY_CHU_NO       " & vbNewLine _
                                              & ",AREA_CD            = @AREA_CD          " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG   = @TYUKEI_HAISO_FLG " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD    = @SYUKA_TYUKEI_CD  " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD    = @HAIKA_TYUKEI_CD  " & vbNewLine _
                                              & ",TRIP_NO_SYUKA      = @TRIP_NO_SYUKA    " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI     = @TRIP_NO_TYUKEI   " & vbNewLine _
                                              & ",TRIP_NO_HAIKA      = @TRIP_NO_HAIKA    " & vbNewLine _
                                              & ",SYS_UPD_DATE       = @SYS_UPD_DATE     " & vbNewLine _
                                              & ",SYS_UPD_TIME       = @SYS_UPD_TIME     " & vbNewLine _
                                              & ",SYS_UPD_PGID       = @SYS_UPD_PGID     " & vbNewLine _
                                              & ",SYS_UPD_USER       = @SYS_UPD_USER     " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start" & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD = @SHIHARAI_TARIFF_CD  " & vbNewLine _
                                              & "--'要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End  " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD        " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L        " & vbNewLine

#End Region

#Region "システム項目更新"

    Private Const SQL_UPDATE_UNSO_L_SYS_DATETIME As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                           & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine

#End Region

#End Region



#Region "INKA_SEQ_QR"


    Private Const SQL_UPDATE_INKA_QR As String _
        = " UPDATE $LM_TRN$..B_INKA_SEQ_QR               " & vbNewLine _
        & "    SET LOAD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
        & "      , LOAD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
        & "      , SYS_UPD_DATE   = @SYS_UPD_DATE        " & vbNewLine _
        & "      , SYS_UPD_TIME   = @SYS_UPD_TIME        " & vbNewLine _
        & "      , SYS_UPD_PGID   = @SYS_UPD_PGID        " & vbNewLine _
        & "      , SYS_UPD_USER   = @SYS_UPD_USER        " & vbNewLine _
        & "  WHERE NRS_BR_CD      = @NRS_BR_CD           " & vbNewLine _
        & "    AND INKA_NO_L      = @INKA_NO_L           " & vbNewLine _
        & "    AND INKA_NO_M      = @INKA_NO_M           " & vbNewLine _
        & "    AND INKA_NO_S      = @INKA_NO_S           " & vbNewLine _
        & "    AND SEQ            = @SEQ                 " & vbNewLine _
        & "    AND SYS_DEL_FLG    = '0'                  " & vbNewLine

#End Region



#Region "SAGYO"

    Private Const SQL_UPDATE_SAGYO As String = "UPDATE $LM_TRN$..E_SAGYO SET             " & vbNewLine _
                                             & "       LOT_NO          = @LOT_NO         " & vbNewLine _
                                             & "      ,SAGYO_COMP_DATE = @SAGYO_COMP_DATE" & vbNewLine _
                                             & "      ,SAGYO_NB        = @SAGYO_NB       " & vbNewLine _
                                             & "      ,SAGYO_GK        = @SAGYO_GK       " & vbNewLine _
                                             & "      ,SEIQTO_CD       = @SEIQTO_CD      " & vbNewLine _
                                             & "      ,REMARK_SIJI     = @REMARK_SIJI    " & vbNewLine _
                                             & "      ,SYS_UPD_DATE    = @SYS_UPD_DATE   " & vbNewLine _
                                             & "      ,SYS_UPD_TIME    = @SYS_UPD_TIME   " & vbNewLine _
                                             & "      ,SYS_UPD_PGID    = @SYS_UPD_PGID   " & vbNewLine _
                                             & "      ,SYS_UPD_USER    = @SYS_UPD_USER   " & vbNewLine _
                                             & "WHERE  NRS_BR_CD       = @NRS_BR_CD      " & vbNewLine _
                                             & "  AND  SAGYO_REC_NO    = @SAGYO_REC_NO   " & vbNewLine


#End Region

#Region "ZAI_TRS"

    Private Const SQL_UPDATE_ZAI_TRS As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET       " & vbNewLine _
                                               & " NRS_BR_CD         = @NRS_BR_CD      " & vbNewLine _
                                               & ",ZAI_REC_NO        = @ZAI_REC_NO     " & vbNewLine _
                                               & ",WH_CD             = @WH_CD          " & vbNewLine _
                                               & ",TOU_NO            = @TOU_NO         " & vbNewLine _
                                               & ",SITU_NO           = @SITU_NO        " & vbNewLine _
                                               & ",ZONE_CD           = @ZONE_CD        " & vbNewLine _
                                               & ",LOCA              = @LOCA           " & vbNewLine _
                                               & ",LOT_NO            = @LOT_NO         " & vbNewLine _
                                               & ",CUST_CD_L         = @CUST_CD_L      " & vbNewLine _
                                               & ",CUST_CD_M         = @CUST_CD_M      " & vbNewLine _
                                               & ",GOODS_CD_NRS      = @GOODS_CD_NRS   " & vbNewLine _
                                               & ",INKA_NO_L         = @INKA_NO_L      " & vbNewLine _
                                               & ",INKA_NO_M         = @INKA_NO_M      " & vbNewLine _
                                               & ",INKA_NO_S         = @INKA_NO_S      " & vbNewLine _
                                               & ",ALLOC_PRIORITY    = @ALLOC_PRIORITY " & vbNewLine _
                                               & ",RSV_NO            = @RSV_NO         " & vbNewLine _
                                               & ",SERIAL_NO         = @SERIAL_NO      " & vbNewLine _
                                               & ",HOKAN_YN          = @HOKAN_YN       " & vbNewLine _
                                               & ",TAX_KB            = @TAX_KB         " & vbNewLine _
                                               & ",GOODS_COND_KB_1   = @GOODS_COND_KB_1" & vbNewLine _
                                               & ",GOODS_COND_KB_2   = @GOODS_COND_KB_2" & vbNewLine _
                                               & ",GOODS_COND_KB_3   = @GOODS_COND_KB_3" & vbNewLine _
                                               & ",OFB_KB            = @OFB_KB         " & vbNewLine _
                                               & ",SPD_KB            = @SPD_KB         " & vbNewLine _
                                               & ",REMARK_OUT          = @REMARK_OUT   " & vbNewLine _
                                               & ",PORA_ZAI_NB       = @PORA_ZAI_NB    " & vbNewLine _
                                               & ",ALLOC_CAN_NB      = @ALLOC_CAN_NB   " & vbNewLine _
                                               & ",IRIME             = @IRIME          " & vbNewLine _
                                               & ",PORA_ZAI_QT       = @PORA_ZAI_QT    " & vbNewLine _
                                               & ",ALLOC_CAN_QT      = @ALLOC_CAN_QT   " & vbNewLine _
                                               & ",INKO_DATE         = @INKO_DATE      " & vbNewLine _
                                               & ",INKO_PLAN_DATE    = @INKO_PLAN_DATE " & vbNewLine _
                                               & ",LT_DATE           = @LT_DATE        " & vbNewLine _
                                               & ",GOODS_CRT_DATE    = @GOODS_CRT_DATE " & vbNewLine _
                                               & ",DEST_CD_P         = @DEST_CD_P      " & vbNewLine _
                                               & ",REMARK            = @REMARK         " & vbNewLine _
                                               & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
                                               & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
                                               & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
                                               & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
                                               & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
                                               & "  AND ZAI_REC_NO   = @ZAI_REC_NO     " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

#End Region

    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
#Region "INKA_KENPIN_WK"
    Private Const SQL_UPDATE_INKA_KENPIN_WK As String =
                " UPDATE                               " & vbNewLine _
              & "    $LM_TRN$..B_INKA_KENPIN_WK  -- 013118 LM_TRN_10固定修正  " & vbNewLine _
              & " SET                                  " & vbNewLine _
              & "     INKA_NO_L    = @INKA_NO_L    --@ " & vbNewLine _
              & "    ,INKA_NO_M    = @INKA_NO_M    --@ " & vbNewLine _
              & "    ,INKA_NO_S    = @INKA_NO_S    --@ " & vbNewLine _
              & "    ,SYS_UPD_DATE = @SYS_UPD_DATE --@ " & vbNewLine _
              & "    ,SYS_UPD_TIME = @SYS_UPD_TIME --@ " & vbNewLine _
              & "    ,SYS_UPD_USER = @SYS_UPD_USER --@ " & vbNewLine _
              & "    ,SYS_UPD_PGID = @SYS_UPD_PGID --@ " & vbNewLine _
              & " WHERE                                " & vbNewLine _
              & "     NRS_BR_CD    = @NRS_BR_CD    --@ " & vbNewLine _
              & " AND WH_CD        = @WH_CD        --@ " & vbNewLine _
              & " AND CUST_CD_L    = @CUST_CD_L    --@ " & vbNewLine _
              & " AND INPUT_DATE   = @INPUT_DATE   --@ " & vbNewLine _
              & " AND SEQ          = @SEQ          --@ " & vbNewLine

    'ADD S 2019/12/02 006350
    Private Const SQL_UPDATE_INKA_KENPIN_WK_TORI_FLG As String = _
                "UPDATE                                " & vbNewLine _
              & "    $LM_TRN$..B_INKA_KENPIN_WK        " & vbNewLine _
              & "SET                                   " & vbNewLine _
              & "    INKA_TORI_FLG = @INKA_TORI_FLG    " & vbNewLine _
              & "   ,SYS_UPD_DATE = @SYS_UPD_DATE      " & vbNewLine _
              & "   ,SYS_UPD_TIME = @SYS_UPD_TIME      " & vbNewLine _
              & "   ,SYS_UPD_USER = @SYS_UPD_USER      " & vbNewLine _
              & "   ,SYS_UPD_PGID = @SYS_UPD_PGID      " & vbNewLine _
              & "WHERE                                 " & vbNewLine _
              & "    NRS_BR_CD    = @NRS_BR_CD         " & vbNewLine _
              & "AND CUST_CD_L    = @CUST_CD_L         " & vbNewLine _
              & "AND INKA_NO_L    = @INKA_NO_L         " & vbNewLine
    'ADD E 2019/12/02 006350

#End Region
    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

#End Region

#Region "Delete"

#Region "UPDATE_DEL_FLG"

    Private Const SQL_COM_UPDATE_DEL_FLG As String = " SYS_UPD_DATE    = @SYS_UPD_DATE" & vbNewLine _
                                                   & ",SYS_UPD_TIME    = @SYS_UPD_TIME" & vbNewLine _
                                                   & ",SYS_UPD_PGID    = @SYS_UPD_PGID" & vbNewLine _
                                                   & ",SYS_UPD_USER    = @SYS_UPD_USER" & vbNewLine _
                                                   & ",SYS_DEL_FLG     = @SYS_DEL_FLG " & vbNewLine _
                                                   & " WHERE NRS_BR_CD = @NRS_BR_CD   " & vbNewLine

#End Region

#Region "UNSO_L"

    Private Const SQL_DELETE_UNSO_L As String = "DELETE FROM $LM_TRN$..F_UNSO_L    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L  " & vbNewLine

#End Region

#Region "UNSO_M"

    Private Const SQL_DELETE_UNSO_M As String = "DELETE FROM $LM_TRN$..F_UNSO_M    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L  " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_DELETE_UNCHIN As String = "DELETE FROM $LM_TRN$..F_UNCHIN_TRS " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L   " & vbNewLine

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    Private Const SQL_DELETE_SHIHARAI As String = "DELETE FROM $LM_TRN$..F_SHIHARAI_TRS " & vbNewLine _
                                                & "WHERE   NRS_BR_CD   = @NRS_BR_CD     " & vbNewLine _
                                                & "  AND   UNSO_NO_L   = @UNSO_NO_L     " & vbNewLine
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

#End Region

#Region "SAGYO"

    Private Const SQL_DELETE_SAGYO As String = "DELETE FROM $LM_TRN$..E_SAGYO         " & vbNewLine _
                                             & "WHERE   NRS_BR_CD      = @NRS_BR_CD   " & vbNewLine _
                                             & "  AND   SAGYO_REC_NO   = @SAGYO_REC_NO" & vbNewLine

#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "INKA_PHOTO"

    Private Const SQL_DELETE_INKA_PHOTO As String =
          "DELETE FROM $LM_TRN$..B_INKA_PHOTO" & vbNewLine _
        & "WHERE   NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
        & "  AND   INKA_NO_L   = @INKA_NO_L  " & vbNewLine _
        & "  AND   INKA_NO_M   = @INKA_NO_M  " & vbNewLine _
        & "  AND   INKA_NO_S   = @INKA_NO_S  " & vbNewLine

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END


#End Region

#End Region

    '2014.04.24 CALT対応 追加 --ST--
#Region "CALT対応SQL"
#Region "Select"
#Region "キャンセルデータ抽出"
    ''' <summary>
    ''' キャンセルデータ抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEND_CANCEL As String = _
                  " SELECT                                                              " & vbNewLine _
                & "      BIP.NRS_BR_CD                AS NRS_BR_CD                      " & vbNewLine _
                & "     ,BIP.INKA_NO_L                AS INKA_NO_L                      " & vbNewLine _
                & "     ,BIP.INKA_NO_M                AS INKA_NO_M                      " & vbNewLine _
                & "     ,BIP.INKA_NO_S                AS INKA_NO_S                      " & vbNewLine _
                & "     ,BIP.SEND_SEQ + 1             AS SEND_SEQ                       " & vbNewLine _
                & "     ,'2'                          AS DATA_KBN                       " & vbNewLine _
                & "     ,BIP.WH_CD                    AS WH_CD                          " & vbNewLine _
                & "     ,BIP.CUST_CD_L                AS CUST_CD_L                      " & vbNewLine _
                & "     ,BIP.CUST_CD_M                AS CUST_CD_M                      " & vbNewLine _
                & "     ,BIP.CUST_NM_L                AS CUST_NM_L                      " & vbNewLine _
                & "     ,BIP.INKA_DATE                AS INKA_DATE                      " & vbNewLine _
                & "     ,BIP.BUYER_ORD_NO_L           AS BUYER_ORD_NO_L                 " & vbNewLine _
                & "     ,BIP.OUTKA_FROM_ORD_NO_L      AS OUTKA_FROM_ORD_NO_L            " & vbNewLine _
                & "     ,BIP.REMARK_L                 AS REMARK_L                       " & vbNewLine _
                & "     ,BIP.REMARK_OUT_L             AS REMARK_OUT_L                   " & vbNewLine _
                & "     ,BIP.GOODS_CD_NRS             AS GOODS_CD_NRS                   " & vbNewLine _
                & "     ,BIP.GOODS_CD_CUST            AS GOODS_CD_CUST                  " & vbNewLine _
                & "     ,BIP.GOODS_NM_1               AS GOODS_NM_1                     " & vbNewLine _
                & "     ,BIP.OUTKA_FROM_ORD_NO_M      AS OUTKA_FROM_ORD_NO_M            " & vbNewLine _
                & "     ,BIP.BUYER_ORD_NO_M           AS BUYER_ORD_NO_M                 " & vbNewLine _
                & "     ,BIP.LOT_NO                   AS LOT_NO                         " & vbNewLine _
                & "     ,BIP.REMARK_M                 AS REMARK_M                       " & vbNewLine _
                & "     ,BIP.INKA_NB                  AS INKA_NB                        " & vbNewLine _
                & "     ,BIP.INKA_WT                  AS INKA_WT                        " & vbNewLine _
                & "     ,BIP.KONSU                    AS KONSU                          " & vbNewLine _
                & "     ,BIP.HASU                     AS HASU                           " & vbNewLine _
                & "     ,BIP.IRIME                    AS IRIME                          " & vbNewLine _
                & "     ,BIP.BETU_WT                  AS BETU_WT                        " & vbNewLine _
                & "     ,BIP.PKG_NB                   AS PKG_NB                         " & vbNewLine _
                & "     ,BIP.PKG_UT                   AS PKG_UT                         " & vbNewLine _
                & "     ,BIP.JAN_CD                   AS JAN_CD                         " & vbNewLine _
                & "     ,BIP.SERIAL_NO                AS SERIAL_NO                      " & vbNewLine _
                & "     ,BIP.ONDO_KB                  AS ONDO_KB                        " & vbNewLine _
                & "     ,BIP.ONDO_STR_DATE            AS ONDO_STR_DATE                  " & vbNewLine _
                & "     ,BIP.ONDO_END_DATE            AS ONDO_END_DATE                  " & vbNewLine _
                & "     ,BIP.ONDO_MX                  AS ONDO_MX                        " & vbNewLine _
                & "     ,BIP.ONDO_MM                  AS ONDO_MM                        " & vbNewLine _
                & "     ,BIP.GOODS_COND_KB_1          AS GOODS_COND_KB_1                " & vbNewLine _
                & "     ,BIP.GOODS_COND_KB_2          AS GOODS_COND_KB_2                " & vbNewLine _
                & "     ,BIP.GOODS_COND_KB_3          AS GOODS_COND_KB_3                " & vbNewLine _
                & "     ,BIP.GOODS_CRT_DATE           AS GOODS_CRT_DATE                 " & vbNewLine _
                & "     ,BIP.LT_DATE                  AS LT_DATE                        " & vbNewLine _
                & "     ,BIP.SPD_KB                   AS SPD_KB                         " & vbNewLine _
                & "     ,BIP.OFB_KB                   AS OFB_KB                         " & vbNewLine _
                & "     ,BIP.DEST_CD                  AS DEST_CD                        " & vbNewLine _
                & "     ,BIP.REMARK_S                 AS REMARK_S                       " & vbNewLine _
                & "     ,BIP.ALLOC_PRIORITY           AS ALLOC_PRIORITY                 " & vbNewLine _
                & "     ,BIP.REMARK_OUT_S             AS REMARK_OUT_S                   " & vbNewLine _
                & "     ,'2'                          AS SEND_SHORI_FLG                 " & vbNewLine _
                & "     ,BIP.SEND_USER                AS SEND_USER                      " & vbNewLine _
                & "     ,BIP.SEND_TIME                AS SEND_TIME                      " & vbNewLine _
                & "     ,BIP.SYS_ENT_DATE             AS SYS_ENT_DATE                   " & vbNewLine _
                & "     ,BIP.SYS_ENT_TIME             AS SYS_ENT_TIME                   " & vbNewLine _
                & "     ,BIP.SYS_ENT_PGID             AS SYS_ENT_PGID                   " & vbNewLine _
                & "     ,BIP.SYS_ENT_USER             AS SYS_ENT_USER                   " & vbNewLine _
                & "     ,BIP.SYS_UPD_DATE             AS SYS_UPD_DATE                   " & vbNewLine _
                & "     ,BIP.SYS_UPD_TIME             AS SYS_UPD_TIME                   " & vbNewLine _
                & "     ,BIP.SYS_UPD_PGID             AS SYS_UPD_PGID                   " & vbNewLine _
                & "     ,BIP.SYS_UPD_USER             AS SYS_UPD_USER                   " & vbNewLine _
                & "     ,'1'                          AS SYS_DEL_FLG                    " & vbNewLine _
                & " FROM $LM_TRN$..B_INKA_PLAN_SEND  AS BIP                            	" & vbNewLine _
                & " LEFT JOIN                                                           " & vbNewLine _
                & "     (                                                               " & vbNewLine _
                & "       SELECT                                                        " & vbNewLine _
                & "             BIP_IN2.NRS_BR_CD                  AS NRS_BR_CD         " & vbNewLine _
                & "            ,BIP_IN2.INKA_NO_L                  AS INKA_NO_L         " & vbNewLine _
                & "            ,BIP_IN2.INKA_NO_M                  AS INKA_NO_M         " & vbNewLine _
                & "            ,BIP_IN2.INKA_NO_S                  AS INKA_NO_S         " & vbNewLine _
                & "            ,MAX(GET_SEQ.SEND_SEQ)              AS SEND_SEQ          " & vbNewLine _
                & "       FROM $LM_TRN$..B_INKA_PLAN_SEND         AS BIP_IN2           	" & vbNewLine _
                & "       LEFT JOIN                                                     " & vbNewLine _
                & "           (                                                         " & vbNewLine _
                & "             SELECT                                                  " & vbNewLine _
                & "                   BIP_IN1.NRS_BR_CD            AS NRS_BR_CD         " & vbNewLine _
                & "                  ,BIP_IN1.INKA_NO_L            AS INKA_NO_L         " & vbNewLine _
                & "                  ,BIP_IN1.INKA_NO_M            AS INKA_NO_M         " & vbNewLine _
                & "                  ,BIP_IN1.INKA_NO_S            AS INKA_NO_S         " & vbNewLine _
                & "                  ,BIP_IN1.SEND_SEQ             AS SEND_SEQ          " & vbNewLine _
                & "             FROM $LM_TRN$..B_INKA_PLAN_SEND  AS BIP_IN1            	" & vbNewLine _
                & "             WHERE                                                   " & vbNewLine _
                & "                   BIP_IN1.SYS_DEL_FLG           = '0'               " & vbNewLine _
                & "               AND BIP_IN1.NRS_BR_CD             = @NRS_BR_CD  --@   " & vbNewLine _
                & "               AND BIP_IN1.INKA_NO_L             = @INKA_NO_L  --@   " & vbNewLine _
                & "               AND BIP_IN1.DATA_KBN             <> '2'               " & vbNewLine _
                & "             GROUP BY                                                " & vbNewLine _
                & "                   BIP_IN1.NRS_BR_CD                                 " & vbNewLine _
                & "                  ,BIP_IN1.INKA_NO_L                                 " & vbNewLine _
                & "                  ,BIP_IN1.INKA_NO_M                                 " & vbNewLine _
                & "                  ,BIP_IN1.INKA_NO_S                                 " & vbNewLine _
                & "                  ,BIP_IN1.SEND_SEQ                                  " & vbNewLine _
                & "            ) AS GET_SEQ                                             " & vbNewLine _
                & "          ON BIP_IN2.NRS_BR_CD             = GET_SEQ.NRS_BR_CD       " & vbNewLine _
                & "         AND BIP_IN2.INKA_NO_L             = GET_SEQ.INKA_NO_L       " & vbNewLine _
                & "         AND BIP_IN2.INKA_NO_M             = GET_SEQ.INKA_NO_M       " & vbNewLine _
                & "         AND BIP_IN2.INKA_NO_S             = GET_SEQ.INKA_NO_S       " & vbNewLine _
                & "       WHERE                                                         " & vbNewLine _
                & "             BIP_IN2.SYS_DEL_FLG           = '0'                     " & vbNewLine _
                & "         AND BIP_IN2.NRS_BR_CD             = @NRS_BR_CD    --@       " & vbNewLine _
                & "         AND BIP_IN2.INKA_NO_L             = @INKA_NO_L    --@       " & vbNewLine _
                & "         AND BIP_IN2.DATA_KBN             <> '2'                     " & vbNewLine _
                & "       GROUP BY                                                      " & vbNewLine _
                & "             BIP_IN2.NRS_BR_CD                                       " & vbNewLine _
                & "            ,BIP_IN2.INKA_NO_L                                       " & vbNewLine _
                & "            ,BIP_IN2.INKA_NO_M                                       " & vbNewLine _
                & "            ,BIP_IN2.INKA_NO_S                                       " & vbNewLine _
                & "     ) AS MAX_SEQ                                                    " & vbNewLine _
                & "   ON BIP.NRS_BR_CD    = MAX_SEQ.NRS_BR_CD                           " & vbNewLine _
                & "  AND BIP.INKA_NO_L    = MAX_SEQ.INKA_NO_L                           " & vbNewLine _
                & "  AND BIP.INKA_NO_M    = MAX_SEQ.INKA_NO_M                           " & vbNewLine _
                & "  AND BIP.INKA_NO_S    = MAX_SEQ.INKA_NO_S                           " & vbNewLine _
                & "  AND BIP.SEND_SEQ     = MAX_SEQ.SEND_SEQ                            " & vbNewLine _
                & " LEFT JOIN                                                           " & vbNewLine _
                & "     (                                                               " & vbNewLine _
                & "       SELECT                                                        " & vbNewLine _
                & "             BIP_IN3.NRS_BR_CD            AS NRS_BR_CD               " & vbNewLine _
                & "            ,BIP_IN3.INKA_NO_L            AS INKA_NO_L               " & vbNewLine _
                & "            ,BIP_IN3.INKA_NO_M            AS INKA_NO_M               " & vbNewLine _
                & "            ,BIP_IN3.INKA_NO_S            AS INKA_NO_S               " & vbNewLine _
                & "            ,CONVERT(INTEGER,MAX(BIP_IN3.SEND_SEQ)) + 1  AS SEND_SEQ " & vbNewLine _
                & "       FROM $LM_TRN$..B_INKA_PLAN_SEND   AS BIP_IN3                  " & vbNewLine _
                & "       WHERE                                                         " & vbNewLine _
                & "             BIP_IN3.NRS_BR_CD             = @NRS_BR_CD     --@      " & vbNewLine _
                & "         AND BIP_IN3.INKA_NO_L             = @INKA_NO_L     --@      " & vbNewLine _
                & "       GROUP BY                                                      " & vbNewLine _
                & "             BIP_IN3.NRS_BR_CD                                       " & vbNewLine _
                & "            ,BIP_IN3.INKA_NO_L                                       " & vbNewLine _
                & "            ,BIP_IN3.INKA_NO_M                                       " & vbNewLine _
                & "            ,BIP_IN3.INKA_NO_S                                       " & vbNewLine _
                & "     )                         AS GET_MAX_SEQ                        " & vbNewLine _
                & "   ON BIP.NRS_BR_CD             = GET_MAX_SEQ.NRS_BR_CD              " & vbNewLine _
                & "  AND BIP.INKA_NO_L             = GET_MAX_SEQ.INKA_NO_L              " & vbNewLine _
                & "  AND BIP.INKA_NO_M             = GET_MAX_SEQ.INKA_NO_M              " & vbNewLine _
                & "  AND BIP.INKA_NO_S             = GET_MAX_SEQ.INKA_NO_S              " & vbNewLine _
                & "  AND (BIP.SEND_SEQ +1)         = GET_MAX_SEQ.SEND_SEQ               " & vbNewLine _
                & " WHERE                                                               " & vbNewLine _
                & "      BIP.SYS_DEL_FLG  = '0'                                         " & vbNewLine _
                & "  AND BIP.NRS_BR_CD    = @NRS_BR_CD     --@                          " & vbNewLine _
                & "  AND BIP.INKA_NO_L    = @INKA_NO_L     --@                          " & vbNewLine _
                & "  AND GET_MAX_SEQ.SEND_SEQ IS NOT NULL                               " & vbNewLine _
                & "  AND MAX_SEQ.INKA_NO_L IS NOT NULL                                  " & vbNewLine

    ''' <summary>
    ''' キャンセルデータ用ORDERBY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SEND_CANCEL_ORDER_BY As String = _
     " ORDER BY                " & vbNewLine _
   & "      BIP.NRS_BR_CD ASC  " & vbNewLine _
   & "     ,BIP.INKA_NO_L ASC  " & vbNewLine _
   & "     ,BIP.INKA_NO_M ASC  " & vbNewLine _
   & "     ,BIP.INKA_NO_S ASC  " & vbNewLine _
   & "     ,BIP.SEND_SEQ  ASC  " & vbNewLine
#End Region

#Region "倉庫チェック"
    Private Const SQL_SELECT_WH_CD_EXIST As String = _
                " SELECT                                  " & vbNewLine _
              & "       COUNT(1)         AS REC_CNT       " & vbNewLine _
              & " FROM $LM_MST$..Z_KBN   AS S102          " & vbNewLine _
              & " WHERE                                   " & vbNewLine _
              & "      S102.KBN_GROUP_CD = 'S102'         " & vbNewLine _
              & "  AND S102.KBN_NM1      = @NRS_BR_CD --@ " & vbNewLine _
              & "  AND S102.KBN_NM2      = @WH_CD     --@ " & vbNewLine

#End Region

#End Region

#Region "Insert"
    ''' <summary>
    ''' 入荷予定データ作成SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEND_INKA_DATA As String =
                 " INSERT INTO $LM_TRN$..B_INKA_PLAN_SEND -- 013118 LM_TRN_10固定修正" & vbNewLine _
               & "       (                                 " & vbNewLine _
               & "         NRS_BR_CD                       " & vbNewLine _
               & "        ,INKA_NO_L                       " & vbNewLine _
               & "        ,INKA_NO_M                       " & vbNewLine _
               & "        ,INKA_NO_S                       " & vbNewLine _
               & "        ,SEND_SEQ                        " & vbNewLine _
               & "        ,DATA_KBN                        " & vbNewLine _
               & "        ,WH_CD                           " & vbNewLine _
               & "        ,CUST_CD_L                       " & vbNewLine _
               & "        ,CUST_CD_M                       " & vbNewLine _
               & "        ,CUST_NM_L                       " & vbNewLine _
               & "        ,INKA_DATE                       " & vbNewLine _
               & "        ,BUYER_ORD_NO_L                  " & vbNewLine _
               & "        ,OUTKA_FROM_ORD_NO_L             " & vbNewLine _
               & "        ,REMARK_L                        " & vbNewLine _
               & "        ,REMARK_OUT_L                    " & vbNewLine _
               & "        ,GOODS_CD_NRS                    " & vbNewLine _
               & "        ,GOODS_CD_CUST                   " & vbNewLine _
               & "        ,GOODS_NM_1                      " & vbNewLine _
               & "        ,OUTKA_FROM_ORD_NO_M             " & vbNewLine _
               & "        ,BUYER_ORD_NO_M                  " & vbNewLine _
               & "        ,LOT_NO                          " & vbNewLine _
               & "        ,REMARK_M                        " & vbNewLine _
               & "        ,INKA_NB                         " & vbNewLine _
               & "        ,INKA_WT                         " & vbNewLine _
               & "        ,KONSU                           " & vbNewLine _
               & "        ,HASU                            " & vbNewLine _
               & "        ,IRIME                           " & vbNewLine _
               & "        ,BETU_WT                         " & vbNewLine _
               & "        ,PKG_NB                          " & vbNewLine _
               & "        ,PKG_UT                          " & vbNewLine _
               & "        ,JAN_CD                          " & vbNewLine _
               & "        ,SERIAL_NO                       " & vbNewLine _
               & "        ,ONDO_KB                         " & vbNewLine _
               & "        ,ONDO_STR_DATE                   " & vbNewLine _
               & "        ,ONDO_END_DATE                   " & vbNewLine _
               & "        ,ONDO_MX                         " & vbNewLine _
               & "        ,ONDO_MM                         " & vbNewLine _
               & "        ,GOODS_COND_KB_1                 " & vbNewLine _
               & "        ,GOODS_COND_KB_2                 " & vbNewLine _
               & "        ,GOODS_COND_KB_3                 " & vbNewLine _
               & "        ,GOODS_CRT_DATE                  " & vbNewLine _
               & "        ,LT_DATE                         " & vbNewLine _
               & "        ,SPD_KB                          " & vbNewLine _
               & "        ,OFB_KB                          " & vbNewLine _
               & "        ,DEST_CD                         " & vbNewLine _
               & "        ,REMARK_S                        " & vbNewLine _
               & "        ,ALLOC_PRIORITY                  " & vbNewLine _
               & "        ,REMARK_OUT_S                    " & vbNewLine _
               & "        ,SEND_SHORI_FLG                  " & vbNewLine _
               & "        ,SYS_ENT_DATE                    " & vbNewLine _
               & "        ,SYS_ENT_TIME                    " & vbNewLine _
               & "        ,SYS_ENT_PGID                    " & vbNewLine _
               & "        ,SYS_ENT_USER                    " & vbNewLine _
               & "        ,SYS_UPD_DATE                    " & vbNewLine _
               & "        ,SYS_UPD_TIME                    " & vbNewLine _
               & "        ,SYS_UPD_PGID                    " & vbNewLine _
               & "        ,SYS_UPD_USER                    " & vbNewLine _
               & "        ,SYS_DEL_FLG                     " & vbNewLine _
               & "       )                                 " & vbNewLine _
               & " VALUES(                                 " & vbNewLine _
               & "          @NRS_BR_CD                     " & vbNewLine _
               & "        , @INKA_NO_L                     " & vbNewLine _
               & "        , @INKA_NO_M                     " & vbNewLine _
               & "        , @INKA_NO_S                     " & vbNewLine _
               & "        , @SEND_SEQ                      " & vbNewLine _
               & "        , @DATA_KBN                      " & vbNewLine _
               & "        , @WH_CD                         " & vbNewLine _
               & "        , @CUST_CD_L                     " & vbNewLine _
               & "        , @CUST_CD_M                     " & vbNewLine _
               & "        , @CUST_NM_L                     " & vbNewLine _
               & "        , @INKA_DATE                     " & vbNewLine _
               & "        , @BUYER_ORD_NO_L                " & vbNewLine _
               & "        , @OUTKA_FROM_ORD_NO_L           " & vbNewLine _
               & "        , @REMARK_L                      " & vbNewLine _
               & "        , @REMARK_OUT_L                  " & vbNewLine _
               & "        , @GOODS_CD_NRS                  " & vbNewLine _
               & "        , @GOODS_CD_CUST                 " & vbNewLine _
               & "        , @GOODS_NM_1                    " & vbNewLine _
               & "        , @OUTKA_FROM_ORD_NO_M           " & vbNewLine _
               & "        , @BUYER_ORD_NO_M                " & vbNewLine _
               & "        , @LOT_NO                        " & vbNewLine _
               & "        , @REMARK_M                      " & vbNewLine _
               & "        , @INKA_NB                       " & vbNewLine _
               & "        , @INKA_WT                       " & vbNewLine _
               & "        , @KONSU                         " & vbNewLine _
               & "        , @HASU                          " & vbNewLine _
               & "        , @IRIME                         " & vbNewLine _
               & "        , @BETU_WT                       " & vbNewLine _
               & "        , @PKG_NB                        " & vbNewLine _
               & "        , @PKG_UT                        " & vbNewLine _
               & "        , @JAN_CD                        " & vbNewLine _
               & "        , @SERIAL_NO                     " & vbNewLine _
               & "        , @ONDO_KB                       " & vbNewLine _
               & "        , @ONDO_STR_DATE                 " & vbNewLine _
               & "        , @ONDO_END_DATE                 " & vbNewLine _
               & "        , @ONDO_MX                       " & vbNewLine _
               & "        , @ONDO_MM                       " & vbNewLine _
               & "        , @GOODS_COND_KB_1               " & vbNewLine _
               & "        , @GOODS_COND_KB_2               " & vbNewLine _
               & "        , @GOODS_COND_KB_3               " & vbNewLine _
               & "        , @GOODS_CRT_DATE                " & vbNewLine _
               & "        , @LT_DATE                       " & vbNewLine _
               & "        , @SPD_KB                        " & vbNewLine _
               & "        , @OFB_KB                        " & vbNewLine _
               & "        , @DEST_CD                       " & vbNewLine _
               & "        , @REMARK_S                      " & vbNewLine _
               & "        , @ALLOC_PRIORITY                " & vbNewLine _
               & "        , @REMARK_OUT_S                  " & vbNewLine _
               & "        , @SEND_SHORI_FLG                " & vbNewLine _
               & "        , @SYS_DATE                      " & vbNewLine _
               & "        , @SYS_TIME                      " & vbNewLine _
               & "        , @SYS_PGID                      " & vbNewLine _
               & "        , @SYS_USER                      " & vbNewLine _
               & "        , @SYS_DATE                      " & vbNewLine _
               & "        , @SYS_TIME                      " & vbNewLine _
               & "        , @SYS_PGID                      " & vbNewLine _
               & "        , @SYS_USER                      " & vbNewLine _
               & "        , @SYS_DEL_FLG                   " & vbNewLine _
               & "      )                                  " & vbNewLine

#End Region

#End Region
    '2014.04.24 CALT対応 追加 --ED--

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
    Private _StrSql As StringBuilder = New StringBuilder()

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = New ArrayList()

#End Region

#Region "Method"

#Region "検索処理"

#Region "データ抽出"

#Region "INKA_L"

    ''' <summary>
    ''' 入荷(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectInkaLData(ByVal ds As DataSet) As DataSet
#If True Then 'インターコンチ　総保入期限切れ防止対応
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "INKA_NO_L" _
                                            , "FURI_NO" _
                                            , "INKA_TP" _
                                            , "INKA_KB" _
                                            , "INKA_STATE_KB" _
                                            , "INKA_DATE" _
                                            , "STORAGE_DUE_DATE" _
                                            , "WH_CD" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "INKA_PLAN_QT" _
                                            , "INKA_PLAN_QT_UT" _
                                            , "INKA_TTL_NB" _
                                            , "BUYER_ORD_NO_L" _
                                            , "OUTKA_FROM_ORD_NO_L" _
                                            , "TOUKI_HOKAN_YN" _
                                            , "HOKAN_YN" _
                                            , "HOKAN_FREE_KIKAN" _
                                            , "HOKAN_STR_DATE" _
                                            , "NIYAKU_YN" _
                                            , "TAX_KB" _
                                            , "REMARK" _
                                            , "REMARK_OUT" _
                                            , "CHECKLIST_PRT_DATE" _
                                            , "CHECKLIST_PRT_USER" _
                                            , "UNCHIN_TP" _
                                            , "UNCHIN_KB" _
                                            , "CUST_NM" _
                                            , "INKA_PLAN_QT_UT_NM" _
                                            , "JIS_CD" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "SOKO_NM" _
                                            , "NRS_BR_NM" _
                                            , "TANTO_USER" _
                                            , "SYS_ENT_USER" _
                                            , "SYS_UPD_USER" _
                                            , "INKA_STATE_KB_NM" _
                                            , "INKA_KB_NM" _
                                            , "OUTKA_NO_L" _
                                            , "PRINT_FLG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "OUTKA_SYS_UPD_DATE" _
                                            , "OUTKA_SYS_UPD_TIME" _
                                            , "WH_KENPIN_WK_STATUS" _
                                            , "WH_TAB_SAGYO_SIJI_STATUS" _
                                            , "WH_TAB_YN" _
                                            , "WH_TAB_IMP_YN" _
                                            , "WH_TAB_SAGYO_STATUS" _
                                            , "WH_TAB_NO_SIJI_FLG" _
                                            }
#Else
        Dim str As String() = New String() {"NRS_BR_CD" _
                                     , "INKA_NO_L" _
                                     , "FURI_NO" _
                                     , "INKA_TP" _
                                     , "INKA_KB" _
                                     , "INKA_STATE_KB" _
                                     , "INKA_DATE" _
                                     , "WH_CD" _
                                     , "CUST_CD_L" _
                                     , "CUST_CD_M" _
                                     , "INKA_PLAN_QT" _
                                     , "INKA_PLAN_QT_UT" _
                                     , "INKA_TTL_NB" _
                                     , "BUYER_ORD_NO_L" _
                                     , "OUTKA_FROM_ORD_NO_L" _
                                     , "TOUKI_HOKAN_YN" _
                                     , "HOKAN_YN" _
                                     , "HOKAN_FREE_KIKAN" _
                                     , "HOKAN_STR_DATE" _
                                     , "NIYAKU_YN" _
                                     , "TAX_KB" _
                                     , "REMARK" _
                                     , "REMARK_OUT" _
                                     , "CHECKLIST_PRT_DATE" _
                                     , "CHECKLIST_PRT_USER" _
                                     , "UNCHIN_TP" _
                                     , "UNCHIN_KB" _
                                     , "CUST_NM" _
                                     , "INKA_PLAN_QT_UT_NM" _
                                     , "JIS_CD" _
                                     , "CUST_NM_L" _
                                     , "CUST_NM_M" _
                                     , "SOKO_NM" _
                                     , "NRS_BR_NM" _
                                     , "TANTO_USER" _
                                     , "SYS_ENT_USER" _
                                     , "SYS_UPD_USER" _
                                     , "INKA_STATE_KB_NM" _
                                     , "INKA_KB_NM" _
                                     , "OUTKA_NO_L" _
                                     , "PRINT_FLG" _
                                     , "SYS_UPD_DATE" _
                                     , "SYS_UPD_TIME" _
                                     , "OUTKA_SYS_UPD_DATE" _
                                     , "OUTKA_SYS_UPD_TIME" _
                                     }
#End If

        Return Me.SelectListData(ds, "LMB020_INKA_L", LMB020DAC.SQL_SELECT_INKA_L, LMB020DAC.SelectCondition.PTN1, str)

    End Function

#If True Then   'ADD 2020/06/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
    '商品明細マスタより置場情報を取得(サブ区分="02")セット内容)
#Region "SELECT_M_GOODS_DETAILS"

    Private Const SQL_SELECT_M_GOODS_DETAILS As String = " SELECT                                     " & vbNewLine _
                               & " M_GOODS_DETAILS.SET_NAIYO                   AS SET_NAIYO          " & vbNewLine _
                               & " FROM                                                              " & vbNewLine _
                               & " $LM_MST$..M_GOODS_DETAILS     M_GOODS_DETAILS                     " & vbNewLine _
                               & " WHERE                                                             " & vbNewLine _
                               & " M_GOODS_DETAILS.GOODS_CD_NRS = @GOODS_CD_NRS                      " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_GOODS_DETAILS.SUB_KB = @SUB_KB                                  " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_GOODS_DETAILS.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_GOODS_DETAILS.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine


#End If
#End Region
#End Region

#Region "INKA_M"

    ''' <summary>
    ''' 入荷(中)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(中)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectInkaMData(ByVal ds As DataSet) As DataSet

        Dim sql As New StringBuilder

        '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start

        sql.Append(SQL_SELECT_INKA_M_PARTS1)
        If IsEDIMiddleMultiple(ds) = True Then
            sql.Append(LMB020DAC.SQL_SELECT_INKA_M_PARTS3)
        Else
            sql.Append(LMB020DAC.SQL_SELECT_INKA_M_PARTS2)
        End If
        sql.Append(LMB020DAC.SQL_SELECT_INKA_M_PARTS4)

        '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end

        'START YANAI メモ②No.20
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "INKA_NO_L" _
        '                                    , "INKA_NO_M" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "GOODS_CD_CUST" _
        '                                    , "OUTKA_FROM_ORD_NO_M" _
        '                                    , "BUYER_ORD_NO_M" _
        '                                    , "REMARK" _
        '                                    , "PRINT_SORT" _
        '                                    , "GOODS_NM" _
        '                                    , "ONDO_KB" _
        '                                    , "SUM_KOSU" _
        '                                    , "NB_UT" _
        '                                    , "ONDO_STR_DATE" _
        '                                    , "ONDO_END_DATE" _
        '                                    , "PKG_NB" _
        '                                    , "PKG_NB_UT1" _
        '                                    , "PKG_NB_UT2" _
        '                                    , "STD_IRIME_NB_M" _
        '                                    , "STD_IRIME_UT" _
        '                                    , "SUM_SURYO_M" _
        '                                    , "SUM_JURYO_M" _
        '                                    , "SHOBO_CD" _
        '                                    , "HIKIATE" _
        '                                    , "SAGYO_UMU" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "EDI_KOSU" _
        '                                    , "EDI_SURYO" _
        '                                    , "STD_IRIME_NB" _
        '                                    , "STD_WT_KGS" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "CUST_CD_S" _
        '                                    , "CUST_CD_SS" _
        '                                    , "TARE_YN" _
        '                                    , "LOT_CTL_KB" _
        '                                    , "LT_DATE_CTL_KB" _
        '                                    , "CRT_DATE_CTL_KB" _
        '                                    , "UP_KBN" _
        '                                    }
        'START YANAI 要望番号573
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "INKA_NO_L" _
        '                                    , "INKA_NO_M" _
        '                                    , "GOODS_CD_NRS" _
        '                                    , "GOODS_CD_CUST" _
        '                                    , "OUTKA_FROM_ORD_NO_M" _
        '                                    , "BUYER_ORD_NO_M" _
        '                                    , "REMARK" _
        '                                    , "PRINT_SORT" _
        '                                    , "GOODS_NM" _
        '                                    , "ONDO_KB" _
        '                                    , "SUM_KOSU" _
        '                                    , "NB_UT" _
        '                                    , "ONDO_STR_DATE" _
        '                                    , "ONDO_END_DATE" _
        '                                    , "PKG_NB" _
        '                                    , "PKG_NB_UT1" _
        '                                    , "PKG_NB_UT2" _
        '                                    , "STD_IRIME_NB_M" _
        '                                    , "STD_IRIME_UT" _
        '                                    , "SUM_SURYO_M" _
        '                                    , "SUM_JURYO_M" _
        '                                    , "SHOBO_CD" _
        '                                    , "HIKIATE" _
        '                                    , "SAGYO_UMU" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "EDI_KOSU" _
        '                                    , "EDI_SURYO" _
        '                                    , "STD_IRIME_NB" _
        '                                    , "STD_WT_KGS" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "CUST_CD_S" _
        '                                    , "CUST_CD_SS" _
        '                                    , "TARE_YN" _
        '                                    , "LOT_CTL_KB" _
        '                                    , "LT_DATE_CTL_KB" _
        '                                    , "CRT_DATE_CTL_KB" _
        '                                    , "EDI_FLG" _
        '                                    , "UP_KBN" _
        '                                    }
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "GOODS_CD_NRS" _
                                            , "GOODS_CD_CUST" _
                                            , "OUTKA_FROM_ORD_NO_M" _
                                            , "BUYER_ORD_NO_M" _
                                            , "REMARK" _
                                            , "PRINT_SORT" _
                                            , "GOODS_NM" _
                                            , "ONDO_KB" _
                                            , "SUM_KOSU" _
                                            , "NB_UT" _
                                            , "ONDO_STR_DATE" _
                                            , "ONDO_END_DATE" _
                                            , "PKG_NB" _
                                            , "PKG_NB_UT1" _
                                            , "PKG_NB_UT2" _
                                            , "STD_IRIME_NB_M" _
                                            , "STD_IRIME_UT" _
                                            , "SUM_SURYO_M" _
                                            , "SUM_JURYO_M" _
                                            , "SHOBO_CD" _
                                            , "HIKIATE" _
                                            , "SAGYO_UMU" _
                                            , "SYS_DEL_FLG" _
                                            , "EDI_KOSU" _
                                            , "EDI_SURYO" _
                                            , "STD_IRIME_NB" _
                                            , "STD_WT_KGS" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_CD_S" _
                                            , "CUST_CD_SS" _
                                            , "TARE_YN" _
                                            , "LOT_CTL_KB" _
                                            , "LT_DATE_CTL_KB" _
                                            , "CRT_DATE_CTL_KB" _
                                            , "EDI_FLG" _
                                            , "UP_KBN" _
                                            , "JISSEKI_FLAG" _
                                            , "EDI_NB_UT"
                                            }
        'END YANAI 要望番号573
        'END YANAI メモ②No.20

        '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 upd start
        Return Me.SelectListData(ds, "LMB020_INKA_M", sql.ToString, LMB020DAC.SelectCondition.PTN1, str)
        'Return Me.SelectListData(ds, "LMB020_INKA_M", LMB020DAC.SQL_SELECT_INKA_M, LMB020DAC.SelectCondition.PTN1, str)
        '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 upd end

    End Function

    '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
    ''' <summary>
    ''' 複数EDI（中）設定対応
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsEDIMiddleMultiple(ByRef ds As DataSet) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020IN")
        Dim result As Boolean = False

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, inTbl.Rows(0))

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SELECT_CUST_DTL, Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB020DAC", "IsEDIMiddleMultiple", cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                result = reader.HasRows

            End Using

        End Using

        Return result

    End Function
    '2018/02/14 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end

#End Region

#Region "INKA_S"

    ''' <summary>
    ''' 入荷(小)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(小)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectInkaSData(ByVal ds As DataSet) As DataSet

        'ADD 2022/11/07 倉庫写真アプリ対応 → PHOTO_YN を追加
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "INKA_NO_S" _
                                            , "ZAI_REC_NO" _
                                            , "LOT_NO" _
                                            , "LOCA" _
                                            , "TOU_NO" _
                                            , "SITU_NO" _
                                            , "ZONE_CD" _
                                            , "KONSU" _
                                            , "HASU" _
                                            , "IRIME" _
                                            , "BETU_WT" _
                                            , "SERIAL_NO" _
                                            , "GOODS_COND_KB_1" _
                                            , "GOODS_COND_KB_2" _
                                            , "GOODS_COND_KB_3" _
                                            , "GOODS_CRT_DATE" _
                                            , "LT_DATE" _
                                            , "SPD_KB" _
                                            , "OFB_KB" _
                                            , "DEST_CD" _
                                            , "REMARK" _
                                            , "ALLOC_PRIORITY" _
                                            , "REMARK_OUT" _
                                            , "SYS_DEL_FLG" _
                                            , "STD_IRIME_UT" _
                                            , "STD_IRIME_NM" _
                                            , "KOSU_S" _
                                            , "SURYO_S" _
                                            , "JURYO_S" _
                                            , "STD_WT_KGS" _
                                            , "DEST_NM" _
                                            , "SUM_KONSU_S" _
                                            , "LOT_CTL_KB" _
                                            , "LT_DATE_CTL_KB" _
                                            , "CRT_DATE_CTL_KB" _
                                            , "ZAI_REC_CNT" _
                                            , "UP_KBN" _
                                            , "EXISTS_REMARK" _
                                            , "IMG_YN" _
                                            , "BUG_YN" _
                                            , "PHOTO_YN"
                                            }

        Return Me.SelectListData(ds, "LMB020_INKA_S", LMB020DAC.SQL_SELECT_INKA_S, LMB020DAC.SelectCondition.PTN1, str)

    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet) As DataSet


        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "UNSO_NO_L" _
        '                                    , "YUSO_BR_CD" _
        '                                    , "INOUTKA_NO_L" _
        '                                    , "TRIP_NO" _
        '                                    , "UNSO_CD" _
        '                                    , "UNSO_BR_CD" _
        '                                    , "TARE_YN" _
        '                                    , "BIN_KB" _
        '                                    , "JIYU_KB" _
        '                                    , "DENP_NO" _
        '                                    , "OUTKA_PLAN_DATE" _
        '                                    , "OUTKA_PLAN_TIME" _
        '                                    , "ARR_PLAN_DATE" _
        '                                    , "ARR_PLAN_TIME" _
        '                                    , "ARR_ACT_TIME" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "CUST_REF_NO" _
        '                                    , "SHIP_CD" _
        '                                    , "ORIG_CD" _
        '                                    , "DEST_CD" _
        '                                    , "UNSO_PKG_NB" _
        '                                    , "NB_UT" _
        '                                    , "UNSO_WT" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "PC_KB" _
        '                                    , "TARIFF_BUNRUI_KB" _
        '                                    , "TAX_KB" _
        '                                    , "VCLE_KB" _
        '                                    , "MOTO_DATA_KB" _
        '                                    , "REMARK" _
        '                                    , "SEIQ_TARIFF_CD" _
        '                                    , "SEIQ_ETARIFF_CD" _
        '                                    , "AD_3" _
        '                                    , "UNSO_TEHAI_KB" _
        '                                    , "BUY_CHU_NO" _
        '                                    , "AREA_CD" _
        '                                    , "TYUKEI_HAISO_FLG" _
        '                                    , "SYUKA_TYUKEI_CD" _
        '                                    , "HAIKA_TYUKEI_CD" _
        '                                    , "TRIP_NO_SYUKA" _
        '                                    , "TRIP_NO_TYUKEI" _
        '                                    , "TRIP_NO_HAIKA" _
        '                                    , "BETU_KYORI_CD" _
        '                                    , "UNCHIN_TARIFF_CD" _
        '                                    , "EXTC_TARIFF_CD" _
        '                                    , "JIS_CD" _
        '                                    , "KYORI" _
        '                                    , "UNSOCO_NM" _
        '                                    , "UNSOCO_BR_NM" _
        '                                    , "ORIG_CD_NM" _
        '                                    , "UNCHIN_TARIFF_REM" _
        '                                    , "YOKO_REM" _
        '                                    , "UMU_FLG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "SYS_DEL_FLG" _
        '                                    , "SEIQ_UNCHIN" _
        '                                    , "UP_KBN" _
        '                                    , "FIXED_CHK" _
        '                                    , "GROUP_CHK" _
        '                                    }

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "YUSO_BR_CD" _
                                            , "INOUTKA_NO_L" _
                                            , "TRIP_NO" _
                                            , "UNSO_CD" _
                                            , "UNSO_BR_CD" _
                                            , "TARE_YN" _
                                            , "BIN_KB" _
                                            , "JIYU_KB" _
                                            , "DENP_NO" _
                                            , "OUTKA_PLAN_DATE" _
                                            , "OUTKA_PLAN_TIME" _
                                            , "ARR_PLAN_DATE" _
                                            , "ARR_PLAN_TIME" _
                                            , "ARR_ACT_TIME" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_REF_NO" _
                                            , "SHIP_CD" _
                                            , "ORIG_CD" _
                                            , "DEST_CD" _
                                            , "UNSO_PKG_NB" _
                                            , "NB_UT" _
                                            , "UNSO_WT" _
                                            , "UNSO_ONDO_KB" _
                                            , "PC_KB" _
                                            , "TARIFF_BUNRUI_KB" _
                                            , "TAX_KB" _
                                            , "VCLE_KB" _
                                            , "MOTO_DATA_KB" _
                                            , "REMARK" _
                                            , "SEIQ_TARIFF_CD" _
                                            , "SEIQ_ETARIFF_CD" _
                                            , "AD_3" _
                                            , "UNSO_TEHAI_KB" _
                                            , "BUY_CHU_NO" _
                                            , "AREA_CD" _
                                            , "TYUKEI_HAISO_FLG" _
                                            , "SYUKA_TYUKEI_CD" _
                                            , "HAIKA_TYUKEI_CD" _
                                            , "TRIP_NO_SYUKA" _
                                            , "TRIP_NO_TYUKEI" _
                                            , "TRIP_NO_HAIKA" _
                                            , "BETU_KYORI_CD" _
                                            , "UNCHIN_TARIFF_CD" _
                                            , "EXTC_TARIFF_CD" _
                                            , "JIS_CD" _
                                            , "KYORI" _
                                            , "UNSOCO_NM" _
                                            , "UNSOCO_BR_NM" _
                                            , "ORIG_CD_NM" _
                                            , "UNCHIN_TARIFF_REM" _
                                            , "YOKO_REM" _
                                            , "UMU_FLG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SYS_DEL_FLG" _
                                            , "SEIQ_UNCHIN" _
                                            , "UP_KBN" _
                                            , "FIXED_CHK" _
                                            , "GROUP_CHK" _
                                            , "SHIHARAI_TARIFF_CD" _
                                            , "SHIHARAI_ETARIFF_CD" _
                                            , "SHIHARAI_FIXED_CHK" _
                                            , "SHIHARAI_GROUP_CHK" _
                                            , "SHIHARAI_TARIFF_REM" _
                                            , "SHIHARAI_YOKO_REM" _
                                            }
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        Return Me.SelectListData(ds, "LMB020_UNSO_L", LMB020DAC.SQL_SELECT_UNSO_L, LMB020DAC.SelectCondition.PTN1, str)

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運賃のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SELECT_UNCHIN, br))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectUnchinData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQ_GROUP_NO_M", "SEIQ_GROUP_NO_M")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SEIQ_SYARYO_KB", "SEIQ_SYARYO_KB")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        map.Add("SEIQ_TARIFF_BUNRUI_KB", "SEIQ_TARIFF_BUNRUI_KB")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("SEIQ_UNCHIN", "SEIQ_UNCHIN")
        map.Add("SEIQ_CITY_EXTC", "SEIQ_CITY_EXTC")
        map.Add("SEIQ_WINT_EXTC", "SEIQ_WINT_EXTC")
        map.Add("SEIQ_RELY_EXTC", "SEIQ_RELY_EXTC")
        map.Add("SEIQ_TOLL", "SEIQ_TOLL")
        map.Add("SEIQ_INSU", "SEIQ_INSU")
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("KANRI_UNCHIN", "KANRI_UNCHIN")
        map.Add("KANRI_CITY_EXTC", "KANRI_CITY_EXTC")
        map.Add("KANRI_WINT_EXTC", "KANRI_WINT_EXTC")
        map.Add("KANRI_RELY_EXTC", "KANRI_RELY_EXTC")
        map.Add("KANRI_TOLL", "KANRI_TOLL")
        map.Add("KANRI_INSU", "KANRI_INSU")
        map.Add("REMARK", "REMARK")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SAGYO_KANRI", "SAGYO_KANRI")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "F_UNCHIN_TRS")

    End Function

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' 作業のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の作業のデータ取得SQLの構築・発行</remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"SAGYO_COMP" _
                                            , "SAGYO_COMP_NM" _
                                            , "SKYU_CHK" _
                                            , "SAGYO_REC_NO" _
                                            , "SAGYO_SIJI_NO" _
                                            , "INOUTKA_NO_LM" _
                                            , "NRS_BR_CD" _
                                            , "IOZS_KB" _
                                            , "SAGYO_CD" _
                                            , "SAGYO_NM" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "DEST_SAGYO_FLG" _
                                            , "DEST_CD" _
                                            , "DEST_NM" _
                                            , "LOT_NO" _
                                            , "INV_TANI" _
                                            , "SAGYO_NB" _
                                            , "SAGYO_UP" _
                                            , "SAGYO_GK" _
                                            , "TAX_KB" _
                                            , "REMARK_ZAI" _
                                            , "SAGYO_RYAK" _
                                            , "SYS_DEL_FLG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "UP_KBN" _
                                            , "REMARK_SIJI" _
                                            }

        Return Me.SelectListData(ds, "LMB020_SAGYO", LMB020DAC.SQL_SELECT_SAGYO, LMB020DAC.SelectCondition.PTN2, str)

    End Function

#End Region

#Region "ZAIKO"

    ''' <summary>
    ''' 在庫のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の在庫のデータ取得SQLの構築・発行</remarks>
    Private Function SelectZaikoData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"ZAI_REC_NO" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "INKA_NO_S" _
                                            , "RSV_NO" _
                                            , "ALCTD_NB" _
                                            , "ALLOC_CAN_NB" _
                                            , "PORA_ZAI_QT" _
                                            , "ALCTD_QT" _
                                            , "ALLOC_CAN_QT" _
                                            , "INKO_DATE" _
                                            , "INKO_PLAN_DATE" _
                                            , "ZERO_FLAG" _
                                            , "SMPL_FLAG" _
                                            , "SYS_DEL_FLG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "UP_KBN" _
                                            }

        Return Me.SelectListData(ds, "LMB020_ZAI", LMB020DAC.SQL_SELECT_ZAIKO, LMB020DAC.SelectCondition.PTN1, str)

    End Function

#End Region

#Region "MAX_SEQ"

    ''' <summary>
    ''' Maxシーケンス取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時のMaxシーケンス取得SQLの構築・発行</remarks>
    Private Function SelectMaxNo(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"INKA_NO_M" _
                                            , "MAX_INKA_NO_S" _
                                            }

        Return Me.SelectListData(ds, "LMB020_MAX_NO", LMB020DAC.SQL_SELECT_MAX_NO, LMB020DAC.SelectCondition.PTN1, str)

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
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Return Me.ComSelectCustData(ds)

    End Function

    ''' <summary>
    ''' 荷主取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主取得SQLの構築・発行</remarks>
    Private Function SelectSubCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_M")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Return Me.ComSelectCustData(ds, Me._Row("CUST_CD_S").ToString(), Me._Row("CUST_CD_SS").ToString())

    End Function

    ''' <summary>
    ''' 荷主取得メイン処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sCd">荷主(小)コード　初期値："00"</param>
    ''' <param name="ssCd">荷主(極小)コード　初期値："00"</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ComSelectCustData(ByVal ds As DataSet _
                                           , Optional ByVal sCd As String = "00" _
                                           , Optional ByVal ssCd As String = "00" _
                                           ) As DataSet

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", sCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", ssCd, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SELECT_CUST, br))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectCustData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("HOKAN_SEIQTO_CD", "HOKAN_SEIQTO_CD")
        map.Add("NIYAKU_SEIQTO_CD", "NIYAKU_SEIQTO_CD")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")
        map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, "CUST")

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
        Return Me.SelectGheaderData(ds, "CUST", LMG000DAC.SQL_SELECT_HOKAN_CHK_DATE, "HOKAN_SEIQTO_CD")
    End Function

    ''' <summary>
    ''' 請求ヘッダ取得(荷役料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderDataNiyaku(ByVal ds As DataSet) As DataSet
        Return Me.SelectGheaderData(ds, "CUST", LMG000DAC.SQL_SELECT_NIYAKU_CHK_DATE, "NIYAKU_SEIQTO_CD")
    End Function

    ''' <summary>
    ''' 請求ヘッダ取得(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderDataSagyo(ByVal ds As DataSet) As DataSet
        Return Me.SelectGheaderData(ds, "LMB020_SAGYO", LMG000DAC.SQL_SELECT_SAGYO_CHK_DATE, "SEIQTO_CD")
    End Function

    ''' <summary>
    ''' 請求ヘッダ取得(運賃)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderDataUnchin(ByVal ds As DataSet) As DataSet
        Return Me.SelectGheaderData(ds, "F_UNCHIN_TRS", LMG000DAC.SQL_SELECT_KEIRI_CHK_DATE, "SEIQTO_CD", "SEIQ_TARIFF_BUNRUI_KB")
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
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item(colNm1).ToString(), DBDataType.CHAR))
        If String.IsNullOrEmpty(colNm2) = False Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me._Row.Item(colNm2).ToString(), DBDataType.CHAR))
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectGheaderData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "G_HED"))

    End Function

#End Region

#Region "M_SOKO"

    ''' <summary>
    ''' 倉庫情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>倉庫情報取得SQLの構築・発行</remarks>
    Private Function SelectSokoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SELECT_SOKO, br))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectSokoData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INKA_UKE_PRT_YN", "INKA_UKE_PRT_YN")
        map.Add("INKA_KENPIN_YN", "INKA_KENPIN_YN")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "SOKO"))

    End Function

#End Region

    '要望番号:1350 terakawa 2012.08.24 Start
#Region "M_CUST_DETAILS"
    ''' <summary>
    ''' 荷主明細取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function GetCustDetail(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inTbl.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SELECT_CUST_DETAILS, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "GetCustDetail", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "CUST_DETAILS"))

    End Function

#End Region
    '要望番号:1350 terakawa 2012.08.24 End

#Region "INKA_QR"

    Private Function SelectInkaQrData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() _
                              {"NRS_BR_CD" _
                             , "INKA_NO_L" _
                             , "INKA_NO_M" _
                             , "INKA_NO_S" _
                             , "SEQ" _
                             , "GOODS_CD_NRS" _
                             , "GOODS_CD_CUST" _
                             , "GOODS_NM_1" _
                             , "GOODS_NM_2" _
                             , "GOODS_NM_3" _
                             , "PKG_NB" _
                             , "IRIME" _
                             , "STD_IRIME_NB" _
                             , "LOT_NO" _
                             , "SERIAL_NO" _
                             , "GOODS_CRT_DATE" _
                             , "LT_DATE" _
                             , "KENPIN_NB" _
                             , "EXISTS_REMARK" _
                             , "WH_CD" _
                             , "TOU_NO" _
                             , "SITU_NO" _
                             , "ZONE_CD" _
                             , "LOCA" _
                             , "LOAD_DATE" _
                             , "LOAD_TIME" _
                             , "IS_LOADING" _
                             , "NRS_SEQ_QR_NO" _
                             , "INKA_SYS_UPD_DATE" _
                             , "INKA_SYS_UPD_TIME" _
                             , "KEPIN_DATE" _
                             , "KENPIN_TIME" _
                             , "SYS_UPD_DATE" _
                             , "SYS_UPD_TIME" _
                                }

        Return Me.SelectListData(ds, "LMB020_INKA_SEQ_QR", LMB020DAC.SQL_SELECT_INKA_QR, LMB020DAC.SelectCondition.PTN1, str)

    End Function


#End Region

#Region "TB_KINPIN_HEAD"

    Private Function SelectTabHeadData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() _
                              {"NRS_BR_CD" _
                              , "INKA_NO_L" _
                              , "IN_KENPIN_LOC_SEQ" _
                              , "IN_KENPIN_LOC_STATE_KB" _
                              , "WORK_STATE_KB" _
                              , "CANCEL_FLG" _
                              , "WH_CD" _
                              , "CUST_CD_L" _
                              , "CUST_CD_M" _
                              , "CUST_NM_L" _
                              , "CUST_NM_M" _
                              , "BUYER_ORD_NO_L" _
                              , "OUTKA_FROM_ORD_NO_L" _
                              , "UNSO_CD" _
                              , "UNSO_NM" _
                              , "UNSO_BR_CD" _
                              , "UNSO_BR_NM" _
                              , "INKA_DATE" _
                              , "REMARK" _
                              , "REMARK_OUT" _
                              , "REMARK_KENPIN_CHK_FLG" _
                              , "REMARK_LOCA_CHK_FLG" _
                              , "SYS_ENT_DATE" _
                              , "SYS_ENT_TIME" _
                              , "SYS_ENT_PGID" _
                              , "SYS_ENT_USER" _
                              , "SYS_UPD_DATE" _
                              , "SYS_UPD_TIME" _
                              , "SYS_UPD_PGID" _
                              , "SYS_UPD_USER" _
                              , "SYS_DEL_FLG" _
                                }
        Return Me.SelectListData(ds, "LMB020_TAB_HEAD", LMB020DAC.SQL_SELECT_TB_KENPIN_HEAD, LMB020DAC.SelectCondition.PTN1, str)

    End Function


#End Region

#Region "TB_KINPIN_DTL"

    Private Function SelectTabDtlData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() _
                              {"NRS_BR_CD" _
                              , "INKA_NO_L" _
                              , "IN_KENPIN_LOC_SEQ" _
                              , "INKA_NO_M" _
                              , "INKA_NO_S" _
                              , "KENPIN_STATE_KB" _
                              , "LOCATION_STATE_KB" _
                              , "GOODS_CD_NRS" _
                              , "GOODS_CD_CUST" _
                              , "GOODS_NM_NRS" _
                              , "STD_IRIME_NB" _
                              , "PKG_NB" _
                              , "STD_WT_KGS" _
                              , "SHOBO_CD" _
                              , "RUI" _
                              , "HINMEI" _
                              , "NB_UT" _
                              , "PKG_UT" _
                              , "TOU_NO" _
                              , "SITU_NO" _
                              , "ZONE_CD" _
                              , "LOCA" _
                              , "LOT_NO" _
                              , "KONSU" _
                              , "HASU" _
                              , "BETU_WT" _
                              , "IRIME" _
                              , "IRIME_UT" _
                              , "SERIAL_NO" _
                              , "GOODS_COND_KB_1" _
                              , "GOODS_COND_KB_2" _
                              , "GOODS_COND_KB_3" _
                              , "LT_DATE" _
                              , "GOODS_CRT_DATE" _
                              , "SPD_KB" _
                              , "OFB_KB" _
                              , "DEST_CD" _
                              , "DEST_NM" _
                              , "ALLOC_PRIORITY" _
                              , "BUG_FLG" _
                              , "REMARK" _
                              , "REMARK_OUT" _
                              , "SYS_UPD_DATE" _
                              , "SYS_UPD_TIME" _
                              , "SYS_DEL_FLG" _
                              , "IS_LOADING" _
                              , "STD_IRIME_NM" _
                              , "IMG_YN" _
                                }

        Return Me.SelectListData(ds, "LMB020_TAB_DTL", LMB020DAC.SQL_SELECT_TB_KENPIN_DTL, LMB020DAC.SelectCondition.PTN1, str)

    End Function


#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "INKA_PHOTO"

    ''' <summary>
    ''' 入荷写真データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷写真データ取得SQLの構築・発行</remarks>
    Private Function SelectInkaPhotoData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "INKA_NO_L" _
                                            , "INKA_NO_M" _
                                            , "INKA_NO_S" _
                                            , "NO" _
                                            , "SHOHIN_NM" _
                                            , "SATSUEI_DATE" _
                                            , "USER_LNM" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "FILE_PATH"
                                            }

        Return Me.SelectListData(ds, "LMB020_INKA_PHOTO", LMB020DAC.SQL_SELECT_INKA_PHOTO, LMB020DAC.SelectCondition.PTN1, str)

    End Function

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#Region "保管・荷役料最終計算日 検索処理"

    ''' <summary>
    ''' 保管・荷役料最終計算日 検索処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectHokanNiyakuCalculation(ByVal ds As DataSet) As DataSet

        Dim inTbl As DataTable = ds.Tables("LMB020IN")
        Dim inTblLRow As DataRow = inTbl.Rows(0)

        Dim nrsBrCd As String = inTblLRow.Item("NRS_BR_CD").ToString
        Dim inkaNoL As String = inTblLRow.Item("INKA_NO_L").ToString

        ' SQL格納変数の初期化
        Me._StrSql.Length = 0

        ' SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        Me._StrSql.Append(LMB020DAC.SQL_SELECT_HOKAN_NIYAKU_CALCULATION)

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd))

            ' パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", nrsBrCd, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", inkaNoL, DBDataType.CHAR))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("INKA_DATE", "INKA_DATE")
                map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
                map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")
                map.Add("INKA_M_EXISTS", "INKA_M_EXISTS")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB020_HOKAN_NIYAKU_CALCULATION")

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function


#End Region

#End Region

#Region "チェック"

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectInkaLSysDateTime(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, LMB020DAC.SelectCondition.PTN1)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_HAITA_SELECT_INKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectInkaLSysDateTime", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectOutkaLSysDateTime(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me._Row.Item("FURI_NO").ToString(), DBDataType.CHAR))
        Call Me.SetOutKaSysDateTime(Me._Row, Me._SqlPrmList)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_HAITA_SELECT_OUTKA_L, br))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectOutkaLSysDateTime", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLSysDateTime(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_HAITA_SELECT_UNSO_L, br))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectUnsoLSysDateTime", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    'KASAMA 2013.10.29 WIT対応 Start
    ''' <summary>
    ''' ハンディ対象荷主チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHandyCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL作成
        Me._StrSql.Append(LMB020DAC.SQL_HANDY_CUST_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row.Item("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "ChkHandyCust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function
    'KASAMA 2013.10.29 WIT対応 End

    '要望番号:1350 terakawa 2012.08.24 Start
    ''' <summary>
    ''' 同一置場での商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_S")
        Dim inTblM As DataTable = ds.Tables("LMB020_INKA_M")
        Dim inTblL As DataTable = ds.Tables("LMB020_INKA_L")
        Dim nrsBrCdDetails As String = String.Empty
        Dim custCdLDetails As String = String.Empty

        '荷主明細から同一置き場・商品チェック特殊荷主情報を取得
        If ds.Tables("CUST_DETAILS").Rows.Count > 0 Then
            nrsBrCdDetails = ds.Tables("CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()
            custCdLDetails = ds.Tables("CUST_DETAILS").Rows(0).Item("SET_NAIYO_2").ToString()
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL作成
        Me._StrSql.Append(LMB020DAC.SQL_GOODS_LOT_CHK)
        If String.IsNullOrEmpty(custCdLDetails) Then
            Me._StrSql.Append(SQL_GOODS_LOT_CHK_CUST_CD_L)
        Else
            Me._StrSql.Append(SQL_GOODS_LOT_CHK_CUST_CD_L_DETAIL_PLUS)
        End If
        Me._StrSql.Append(LMB020DAC.SQL_GOODS_LOT_CHK_AFTER)

        '要望番号:1393 terakawa 2012.09.03 Start
        '在庫レコード番号の長さが11以上の場合（削除データが存在し、連結されていた場合)
        If Me._Row.Item("ZAI_REC_NO").ToString().Length >= 11 Then
            Me._StrSql.Append("AND ZAI.ZAI_REC_NO NOT IN('" & Me._Row.Item("ZAI_REC_NO").ToString() & "')" & vbNewLine)
        Else
            Me._StrSql.Append("AND ZAI.ZAI_REC_NO <> '" & Me._Row.Item("ZAI_REC_NO").ToString() & "'" & vbNewLine)
        End If
        Me._StrSql.Append(LMB020DAC.SQL_GOODS_LOT_CHK_SYS_DEL_FLG)
        '要望番号:1393 terakawa 2012.09.03 End

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_NRS_BR_CD", nrsBrCdDetails, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inTblL.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DETAILS_CUST_CD_L", custCdLDetails, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row.Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row.Item("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row.Item("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", inTblM.Rows(0).Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "ChkGoodsLot", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("ZAI_CNT")))
        reader.Close()
        Return ds

    End Function
    '要望番号:1350 terakawa 2012.08.24 End

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Dim inTblL As DataTable = ds.Tables("LMB020_INKA_L")
        Dim inTblLRow As DataRow = inTblL.Rows(0)
        '営業所コード取得
        Dim nrsBrCd As String = inTblLRow.Item("NRS_BR_CD").ToString
        '倉庫コード取得
        Dim whCd As String = inTblLRow.Item("WH_CD").ToString
        '荷主コード取得
        Dim custCdL As String = inTblLRow.Item("CUST_CD_L").ToString
        '入荷コード取得
        Dim inkaDate As String = inTblLRow.Item("INKA_DATE").ToString

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        Me._StrSql.Append(SQL_SELECT_TOU_SITU_EXP)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", nrsBrCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", custCdL, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", inkaDate, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "getTouSituExp", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("APL_DATE_FROM", "APL_DATE_FROM")
        map.Add("APL_DATE_TO", "APL_DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB020_TOU_SITU_EXP")

        Return ds

    End Function
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region

#End Region

#Region "設定処理"

#Region "Insert"

#Region "INKA_L"

    ''' <summary>
    ''' 入荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertInkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_INKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetInkaLComParameter(Me._Row, Me._SqlPrmList)

        '入荷(小)がある場合、チェックリスト更新
        Dim chkDate As String = String.Empty
        Dim chkUser As String = String.Empty
        If 0 < ds.Tables("LMB020_INKA_S").Select("SYS_DEL_FLG = '0'").Length Then
            chkDate = sysDateTime(0)
            chkUser = MyBase.GetUserID()
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_DATE", chkDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_USER", chkUser, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertInkaLData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        '更新日付を設定
        Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
        Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

        Return ds

    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_UNSO_L _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertUnsoLData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        '更新日付を設定
        Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
        Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
        Me._Row.Item("UP_KBN") = 1

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送（中）テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送（中）テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_UNSO_M _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnsoMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertUnsoMData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_UNCHIN_TRS").Rows.Count = 0 Then
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_UNCHIN_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_UNCHIN _
                                                                       , inTbl.Rows(0).Item("NRS_BR_CD").ToString()))
        '画面表示用
        Dim unchin As Decimal = 0
        Dim kyori As Decimal = 0

        Dim max As Integer = inTbl.Rows.Count - 1

        '距離(請求距離は全て同じ)
        kyori = Convert.ToDecimal(Me.FormatNumValue(inTbl.Rows(0).Item("SEIQ_KYORI").ToString()))

        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()

            'パラメータの初期化
            cmd.Parameters.Clear()

            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            '運賃合計
            unchin += Convert.ToDecimal(Me.FormatNumValue(Me._Row.Item("SEIQ_UNCHIN").ToString()))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        '画面用の値を反映
        Dim unsoL As DataRow = ds.Tables("LMB020_UNSO_L").Rows(0)
        unsoL.Item("SEIQ_UNCHIN") = unchin
        unsoL.Item("KYORI") = kyori

        Return ds

    End Function

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払運賃データ作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>支払運賃データ新規登録SQLの構築・発行</remarks>
    Private Function InsertShiharaiData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_SHIHARAI_TRS").Rows.Count = 0 Then
            'F_SHIHARAI_TRSが0件ということは本来無いが、一応念のために0件の時はINSERT処理が行われないようにする
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_SHIHARAI_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SHIHARAI_INSERT _
                                                                       , ds.Tables("F_SHIHARAI_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetShiharaiComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertShiharaiData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' 作業テーブル登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_SAGYO")
        Dim max As Integer = inTbl.Rows.Count - 1

        'SQL文のコンパイル
        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim brCd As String = inkaLDr.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_SAGYO, brCd))

        If -1 < max Then

            For i As Integer = 0 To max

                'INTableの条件rowの格納
                Me._Row = inTbl.Rows(i)

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

                '新規登録
                Call Me.InsertSagyoData(cmd, inkaLDr)

                'フラグを更新用に変更
                Me._Row.Item("UP_KBN") = 1

            Next

        End If

        Return ds

    End Function

#End Region

#Region "LM_MST..M_FILE"
    ''' <summary>
    ''' M_FILEにINSERT
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertMFileData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_FILE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_M_FILE, Me._Row.Item("NRS_BR_CD").ToString))

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetInsertMFileParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("InsertMFileData", "InsertMFileData", cmd)

        'SQLの発行
        Dim rtn As Integer = Me.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "Update"

#Region "INKA_L"

    ''' <summary>
    ''' 入荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_INKA_L, LMB020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetInkaLComParameter(Me._Row, Me._SqlPrmList)
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaLData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)テーブル更新(起算日修正)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSaveDateAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim br As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_KISANDATE, LMB020DAC.SQL_COM_UPDATE_CONDITION), br))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", Me._Row.Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateSaveDateAction", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)テーブル更新(印刷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaLPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()

        'sql作成
        Dim sql As String = Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_INKA_L_PRINT, LMB020DAC.SQL_COM_UPDATE_CONDITION), brCd)

        '印刷種別によりSQL修正
        sql = Me.SetUpNm(ds, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", Me._Row.Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaLData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
        End If

        Return ds

    End Function

#End Region

#Region "INKA_M"

    ''' <summary>
    ''' 入荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_M")
        Dim max As Integer = inTbl.Rows.Count - 1

        If -1 < max Then

            'SQL格納変数の初期化
            Me._StrSql.Length = 0

            'SQL構築
            Me._StrSql.Append(" UPDATE $LM_TRN$..B_INKA_M      " & vbNewLine)
            Me._StrSql.Append(" SET                            " & vbNewLine)
            Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
            Me._StrSql.Append("   AND INKA_NO_L = @INKA_NO_L   " & vbNewLine)
            Me._StrSql.Append("   AND INKA_NO_M = @INKA_NO_M   " & vbNewLine)

            'SQL文のコンパイル
            Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()
            Dim cmd1 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_INKA_M, brCd))
            Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_UPDATE_INKA_M, brCd))
            Dim cmd3 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), brCd))

            Dim upKbn As String = String.Empty

            For i As Integer = max To 0 Step -1

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd1.Parameters.Clear()
                cmd2.Parameters.Clear()
                cmd3.Parameters.Clear()

                '条件rowの格納
                Me._Row = inTbl.Rows(i)
                upKbn = Me._Row.Item("UP_KBN").ToString()
                Me._Row.Item("UP_KBN") = 1

                '正常レコードの場合
                If "0".Equals(Me._Row.Item("SYS_DEL_FLG").ToString()) = True Then

                    '新規登録
                    If "0".Equals(upKbn) = True Then

                        Call Me.InsertInkaMData(cmd1)

                    Else

                        '更新処理
                        Call Me.UpdateInkaMData(cmd2)

                    End If

                Else

                    '削除処理
                    Call Me.DeleteInkaMData(cmd3, brCd)
                    Me._Row.Delete()

                End If


            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaMData(ByVal cmd As SqlCommand) As Boolean

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetInkaMComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertInkaMData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return True

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaMData(ByVal cmd As SqlCommand) As Boolean

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetInkaMComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaMData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaMData(ByVal cmd As SqlCommand, ByVal brCd As String) As Boolean

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me._Row.Item("INKA_NO_M").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaMDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)
        Return True

    End Function

#End Region

#Region "INKA_S"

    ''' <summary>
    ''' 入荷(小)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(小)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaSData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_S")
        Dim max As Integer = inTbl.Rows.Count - 1

        If -1 < max Then

            Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()

            'SQL格納変数の初期化
            Me._StrSql.Length = 0

            'SQL構築
            Me._StrSql.Append(" UPDATE $LM_TRN$..B_INKA_S      " & vbNewLine)
            Me._StrSql.Append(" SET                            " & vbNewLine)
            Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
            Me._StrSql.Append("   AND INKA_NO_L = @INKA_NO_L   " & vbNewLine)
            Me._StrSql.Append("   AND INKA_NO_M = @INKA_NO_M   " & vbNewLine)
            Me._StrSql.Append("   AND INKA_NO_S = @INKA_NO_S   " & vbNewLine)

            'SQL文のコンパイル
            Dim cmd1 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_INKA_S, brCd))
            Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_UPDATE_INKA_S, brCd))
            Dim cmd3 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), brCd))

            Dim upKbn As String = String.Empty

            For i As Integer = max To 0 Step -1

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd1.Parameters.Clear()
                cmd2.Parameters.Clear()
                cmd3.Parameters.Clear()

                'rowの格納
                Me._Row = inTbl.Rows(i)

                upKbn = Me._Row.Item("UP_KBN").ToString()
                Me._Row.Item("UP_KBN") = 1

                '正常レコードの場合
                If "0".Equals(Me._Row.Item("SYS_DEL_FLG").ToString()) = True Then

                    '新規登録
                    If "0".Equals(upKbn) = True Then

                        Call Me.InsertInkaSData(cmd1, brCd)

                    Else

                        '更新処理
                        Call Me.UpdateInkaSData(cmd2, brCd)

                    End If

                Else

                    '削除処理
                    Call Me.DeleteInkaSData(cmd3, brCd)
                    Me._Row.Delete()

                End If

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaSData(ByVal cmd As SqlCommand, ByVal brCd As String) As Boolean

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetInkaSComParameter(Me._Row, Me._SqlPrmList, brCd)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertInkaSData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return True

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaSData(ByVal cmd As SqlCommand, ByVal brCd As String) As Boolean

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetInkaSComParameter(Me._Row, Me._SqlPrmList, brCd)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaSData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaSData(ByVal cmd As SqlCommand, ByVal brCd As String) As Boolean

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me._Row.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", Me._Row.Item("INKA_NO_S").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaSDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

#End Region

#Region "INKA_WK"

    'START ADD 2013/09/10 KURIHARA WIT対応
    ''' <summary>
    ''' 入荷WKテーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷WKテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaWkData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_WK")
        Dim max As Integer = inTbl.Rows.Count - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_UPDATE_INKA_WK, brCd))

            Dim upKbn As String = String.Empty

            For i As Integer = max To 0 Step -1

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

                '条件rowの格納
                Me._Row = inTbl.Rows(i)

                '更新処理
                Call Me.UpdateInkaWkData(cmd)

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaWkData(ByVal cmd As SqlCommand) As Boolean

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetInkaWkComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaWKData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

    'END   ADD 2013/09/10 KURIHARA WIT対応
#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_UNSO_L, LMB020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateUnsoLData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
            Me._Row.Item("UP_KBN") = 1
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)テーブル更新(システム項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLSysData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_UNSO_L_SYS_DATETIME, LMB020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateUnsoLSysData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
            Me._Row.Item("UP_KBN") = 1
        End If

        Return ds

    End Function

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSagyoSysData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_SAGYO")
        Dim max As Integer = inTbl.Rows.Count - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
            Dim brCd As String = inkaLDr.Item("NRS_BR_CD").ToString()
            Dim cmd1 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_SAGYO, brCd))
            Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_SAGYO, LMB020DAC.SQL_COM_UPDATE_CONDITION), brCd))
            Dim cmd3 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_DELETE_SAGYO, brCd))
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_SELECT_SAGYO_CHK, brCd))
            Dim upKbn As String = String.Empty

            For i As Integer = max To 0 Step -1

                'INTableの条件rowの格納
                Me._Row = inTbl.Rows(i)
                upKbn = Me._Row.Item("UP_KBN").ToString()

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                Me._Row.Item("UP_KBN") = 1

                If "1".Equals(Me._Row.Item("SYS_DEL_FLG").ToString()) = True OrElse "2".Equals(upKbn) = True Then

                    '削除処理
                    cmd3.Parameters.Clear()
                    Me.DeleteSagyoData(cmd3, inkaLDr)
                    Me._Row.Delete()

                Else

                    Select Case upKbn

                        Case "0"

                            '新規登録

                            '既に登録されている場合、スルー
                            cmd.Parameters.Clear()
                            If 0 < Me.SelectSagyoChk(cmd) Then
                                Continue For
                            End If

                            '新規登録
                            Me._SqlPrmList.Clear()
                            cmd1.Parameters.Clear()
                            Call Me.InsertSagyoData(cmd1, inkaLDr)

                        Case "1"

                            '更新登録
                            cmd2.Parameters.Clear()
                            If Me.UpdateDagyoData(cmd2, inkaLDr) = False Then
                                Return ds
                            End If

                    End Select

                End If

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="inkaLdr">入荷(大)DataRow</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoData(ByVal cmd As SqlCommand, ByVal inkaLdr As DataRow) As Boolean

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetSagyoParameter(Me._Row, Me._SqlPrmList, inkaLdr)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertSagyoData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
        Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

        Return True

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="inkaLdr">入荷(大)DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateDagyoData(ByVal cmd As SqlCommand, ByVal inkaLdr As DataRow) As Boolean

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSagyoParameter(Me._Row, Me._SqlPrmList, inkaLdr)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateDagyoData", cmd)

        'SQLの発行
        Dim rtnResult As Boolean = Me.UpdateResultChk(cmd)
        If rtnResult = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
        End If
        Return rtnResult

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="inkaLdr">入荷(大)DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoData(ByVal cmd As SqlCommand, ByVal inkaLdr As DataRow) As Boolean

        'パラメータ設定
        Call Me.SetSagyoPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteSagyoSysData", cmd)

        'SQLの発行
        Return Me.UpdateResultChk(cmd)

    End Function

    ''' <summary>
    ''' 登録済みかを確認
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>取得件数</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoChk(ByVal cmd As SqlCommand) As Integer

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", Me._Row.Item("INOUTKA_NO_LM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", Me._Row.Item("SAGYO_CD").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectSagyoChk", cmd)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        SelectSagyoChk = Convert.ToInt32(reader("REC_CNT"))
        reader.Close()

        Return SelectSagyoChk

    End Function

#End Region

#Region "ZAI_TRS"

    ''' <summary>
    ''' 在庫テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateZaiTrsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_ZAI")
        Dim max As Integer = inTbl.Rows.Count - 1

        '対象レコードが存在する場合
        If -1 < max Then

            Dim sDt As DataTable = ds.Tables("LMB020_INKA_S")

            '他テーブル情報を設定
            Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
            Dim inkaMDr As DataRow = Nothing
            Dim inkaNoM As String = String.Empty
            Dim chkInkaNoM As String = String.Empty

            'SQL格納変数の初期化
            Me._StrSql.Length = 0

            'SQL構築
            Me._StrSql.Append(" UPDATE $LM_TRN$..D_ZAI_TRS     " & vbNewLine)
            Me._StrSql.Append(" SET                            " & vbNewLine)
            Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
            Me._StrSql.Append("  AND ZAI_REC_NO = @ZAI_REC_NO  " & vbNewLine)
            Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_CONDITION)

            'SQL文のコンパイル
            Dim brCd As String = inkaLDr.Item("NRS_BR_CD").ToString()
            Dim cmd1 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_ZAI_TRS, brCd))
            Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMB020DAC.SQL_UPDATE_ZAI_TRS, LMB020DAC.SQL_COM_UPDATE_CONDITION), brCd))
            Dim cmd3 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), brCd))

            Dim upKbn As String = String.Empty

            For i As Integer = max To 0 Step -1

                '条件rowの格納
                Me._Row = inTbl.Rows(i)

                upKbn = Me._Row.Item("UP_KBN").ToString()
                Me._Row.Item("UP_KBN") = 1

                '入荷中番が変わった場合
                chkInkaNoM = Me._Row.Item("INKA_NO_M").ToString()
                If inkaNoM.Equals(chkInkaNoM) = False Then

                    '入荷(中)の情報を取得
                    inkaMDr = Me.GetInkaMDataRow(ds, Me._Row)

                    '判定用変数を更新
                    inkaNoM = chkInkaNoM

                End If

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd1.Parameters.Clear()
                cmd2.Parameters.Clear()
                cmd3.Parameters.Clear()

                '正常レコードの場合
                If "0".Equals(Me._Row.Item("SYS_DEL_FLG").ToString()) = True Then

                    '新規登録
                    If "0".Equals(upKbn) = True Then

                        Call Me.InsertZaiTrsData(cmd1, inkaLDr, inkaMDr, sDt)

                    Else

                        '更新処理、排他エラーの場合、終了
                        If Me.UpdateZaiTrsData(cmd2, inkaLDr, inkaMDr, sDt) = False Then
                            Return ds
                        End If

                    End If

                Else

                    '削除処理、排他エラーの場合、終了
                    If Me.DeleteZaiTrsData(cmd3, inkaLDr, inkaMDr, sDt, brCd) = False Then
                        Return ds
                    End If

                    Me._Row.Delete()

                End If

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="inkaLdr">入荷(大)DataRow</param>
    ''' <param name="inkaMdr">入荷(中)DataRow</param>
    ''' <param name="sDt">入荷(小)DataTable</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertZaiTrsData(ByVal cmd As SqlCommand, ByVal inkaLDr As DataRow, ByVal inkaMDr As DataRow, ByVal sDt As DataTable) As Boolean

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetZaiTrsComParameter(Me._Row, inkaLDr, inkaMDr, Me.GetInkaSDataRow(sDt, Me._Row), Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertZaiTrsData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
        Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

        Return True

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="inkaLdr">入荷(大)DataRow</param>
    ''' <param name="inkaMdr">入荷(中)DataRow</param>
    ''' <param name="sDt">入荷(小)DataTable</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTrsData(ByVal cmd As SqlCommand, ByVal inkaLDr As DataRow, ByVal inkaMDr As DataRow, ByVal sDt As DataTable) As Boolean

        '引当(済)の場合、スルー
        If LMB020DAC.HIKIATE_ZUMI.Equals(inkaMDr.Item("HIKIATE").ToString()) = True Then
            Return True
        End If

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetZaiTrsComParameter(Me._Row, inkaLDr, inkaMDr, Me.GetInkaSDataRow(sDt, Me._Row), Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateZaiTrsData", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then

            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="inkaLdr">入荷(大)DataRow</param>
    ''' <param name="inkaMdr">入荷(中)DataRow</param>
    ''' <param name="sDt">入荷(小)DataTable</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiTrsData(ByVal cmd As SqlCommand _
                                      , ByVal inkaLDr As DataRow _
                                      , ByVal inkaMDr As DataRow _
                                      , ByVal sDt As DataTable _
                                      , ByVal brCd As String _
                                      ) As Boolean

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateZaiTrsDelFlg", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then

            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

            Return True

        End If

        Return False

    End Function

#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "INKA_PHOTO"

    ''' <summary>
    ''' 入荷写真データテーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷写真データテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaPhotoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_PHOTO")
        Dim max As Integer = inTbl.Rows.Count - 1
        Dim inTblinkaS As DataTable = ds.Tables("LMB020_INKA_S")

        '保存処理時点の入荷写真データ取得
        Dim dsOrg As DataSet = ds.Clone
        Dim drIn As DataRow = dsOrg.Tables("LMB020IN").NewRow
        drIn.Item("NRS_BR_CD") = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD")
        drIn.Item("INKA_NO_L") = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L")
        dsOrg.Tables("LMB020IN").Rows.Add(drIn)
        dsOrg = SelectInkaPhotoData(dsOrg)

        Dim inTblOrg As DataTable = dsOrg.Tables("LMB020_INKA_PHOTO")
        Dim maxOrg As Integer = inTblOrg.Rows.Count - 1

        If (-1 < max) Or (-1 < maxOrg) Then

            Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()

            'SQL格納変数の初期化
            Me._StrSql.Length = 0

            'SQL構築
            Me._StrSql.Append(" UPDATE $LM_TRN$..B_INKA_PHOTO  " & vbNewLine)
            Me._StrSql.Append(" SET                            " & vbNewLine)
            Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
            Me._StrSql.Append("   AND INKA_NO_L = @INKA_NO_L   " & vbNewLine)
            Me._StrSql.Append("   AND INKA_NO_M = @INKA_NO_M   " & vbNewLine)
            Me._StrSql.Append("   AND INKA_NO_S = @INKA_NO_S   " & vbNewLine)

            'SQL文のコンパイル
            Dim cmd1 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_INSERT_INKA_PHOTO, brCd))
            Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_DELETE_INKA_PHOTO, brCd))
            Dim cmd3 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), brCd))

            '入荷写真データを削除
            Dim workDtOrg As DataTable = inTblOrg.Copy
            Dim viewOrg As DataView = New DataView(workDtOrg)
            workDtOrg = viewOrg.ToTable(True, {"INKA_NO_L", "INKA_NO_M", "INKA_NO_S"})  '指定項目でグループ化
            For Each row As DataRow In workDtOrg.Rows

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd2.Parameters.Clear()
                cmd3.Parameters.Clear()

                Dim inkaSCount As Integer = inTblinkaS.Select(
                        String.Concat("INKA_NO_M = '", row.Item("INKA_NO_M").ToString(),
                                      "' AND INKA_NO_S = '", row.Item("INKA_NO_S").ToString(), "'")).Count

                If inkaSCount = 0 Then
                    'INKA_Sに存在しない場合、行削除されたので論理削除

                    '論理削除処理
                    Call Me.UpdateInkaPhotoData(cmd3, brCd, row)

                Else
                    'INKA_Sに存在する場合、改めて画像選択データで更新するため物理削除

                    '物理削除処理
                    Call Me.DeleteInkaPhotoData(cmd2, brCd, row)

                End If

            Next

            '入荷写真データ追加
            Dim workDt As DataTable = inTbl.Copy
            Dim view As DataView = New DataView(workDt)
            workDt = view.ToTable(True, {"INKA_NO_L", "INKA_NO_M", "INKA_NO_S"})    '指定項目でグループ化
            For Each row As DataRow In workDt.Rows

                'INKA_Sに存在しない場合、更新スキップ
                Dim inkaSCount As Integer = inTblinkaS.Select(
                        String.Concat("INKA_NO_M = '", row.Item("INKA_NO_M").ToString(),
                                      "' AND INKA_NO_S = '", row.Item("INKA_NO_S").ToString(), "'")).Count
                If inkaSCount = 0 Then
                    Continue For
                End If

                '入荷写真データを追加
                Dim inkaPDrs As DataRow() = inTbl.Select(
                        String.Concat("INKA_NO_M = '", row.Item("INKA_NO_M").ToString(),
                                      "' AND INKA_NO_S = '", row.Item("INKA_NO_S").ToString(), "'"),
                        "NO ASC , FILE_PATH ASC")

                For Each rowPhoto As DataRow In inkaPDrs

                    'SQLパラメータ初期化
                    Me._SqlPrmList.Clear()
                    cmd1.Parameters.Clear()

                    '登録処理
                    Call Me.InsertInkaPhotoData(cmd1, brCd, rowPhoto)

                Next

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="inkaPRow">DataRow</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaPhotoData(ByVal cmd As SqlCommand, ByVal brCd As String, ByVal inkaPRow As DataRow) As Boolean

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", inkaPRow.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", inkaPRow.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", inkaPRow.Item("INKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NO", inkaPRow.Item("NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_PATH", inkaPRow.Item("FILE_PATH").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertInkaPhotoData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return True

    End Function

    ''' <summary>
    ''' 更新登録（論理削除）
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="inkaPRow">DataRow</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaPhotoData(ByVal cmd As SqlCommand, ByVal brCd As String, ByVal inkaPRow As DataRow) As Boolean

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", inkaPRow.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", inkaPRow.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", inkaPRow.Item("INKA_NO_S").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaPhotoDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

    ''' <summary>
    ''' 削除登録（物理削除）
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="inkaPRow">DataRow</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaPhotoData(ByVal cmd As SqlCommand, ByVal brCd As String, ByVal inkaPRow As DataRow) As Boolean

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", inkaPRow.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", inkaPRow.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", inkaPRow.Item("INKA_NO_S").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteInkaPhotoData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END


#Region "B_INKA_SEQ_QR"


    ''' <summary>
    ''' 日陸連番QR(入荷)テーブル更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaQr(ByVal ds As DataSet) As DataSet

        Dim inkaL As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim nrsBrCd As String = inkaL.Item("NRS_BR_CD").ToString()

        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_UPDATE_INKA_QR, nrsBrCd))

            For Each row As DataRow In ds.Tables("LMB020_INKA_SEQ_QR").Rows

                If (LMConst.FLG.ON.Equals(row.Item("IS_LOADING")) = False) Then
                    Continue For
                End If

                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

                Me.SetInkaQrParameter(row, Me._SqlPrmList)
                Me.SetSysdataParameter(Me._SqlPrmList)

                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                        , System.Reflection.MethodBase.GetCurrentMethod.Name _
                                        , cmd)

                If (Me.UpdateResultChk(cmd) = False) Then
                    Exit For
                End If
            Next

        End Using

        Return ds

    End Function

#End Region

#End Region

#Region "Delete"

#Region "INKA_L"

    ''' <summary>
    ''' 入荷(大)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLDelFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..B_INKA_L      " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND INKA_NO_L = @INKA_NO_L   " & vbNewLine)
        Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_CONDITION)

        'SQL文のコンパイル
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , brCd))

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaLDelFlg", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "INKA_M"

    ''' <summary>
    ''' 入荷(中)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(中)論理削除SQLの構築・発行</remarks>
    Private Function UpdateInkaMSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "B_INKA_M")
    End Function

#End Region

#Region "INKA_S"

    ''' <summary>
    ''' 入荷(小)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(小)論理削除SQLの構築・発行</remarks>
    Private Function UpdateInkaSSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "B_INKA_S")
    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim sql As String = LMB020DAC.SQL_DELETE_UNSO_L
        sql = String.Concat(sql, LMB020DAC.SQL_COM_UPDATE_CONDITION)
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteUnsoLData", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)
        If cnt < 1 AndAlso String.IsNullOrEmpty(Me._Row.Item("UNSO_NO_L").ToString()) = False Then
            MyBase.SetMessage("E011")
        Else
            Me._Row.Delete()
        End If

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送(中)テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_DELETE_UNSO_M _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteUnsoMData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_DELETE_UNCHIN _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteUnchinData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>支払運賃テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_DELETE_SHIHARAI _
                                                                       , ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteShiharaiData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


#End Region

#Region "OUTKA"

    ''' <summary>
    ''' 出荷(大)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutKaL(ByVal ds As DataSet) As DataSet
        Return Me.DeleteOutKa(ds, LMB020DAC.SelectCondition.PTN1)
    End Function

    ''' <summary>
    ''' 出荷(中)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutKaM(ByVal ds As DataSet) As DataSet
        Return Me.DeleteOutKa(ds, LMB020DAC.SelectCondition.PTN1)
    End Function

    ''' <summary>
    ''' 出荷(小)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutKaS(ByVal ds As DataSet) As DataSet
        Return Me.DeleteOutKa(ds, LMB020DAC.SelectCondition.PTN1)
    End Function

    ''' <summary>
    ''' 出荷の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutKa(ByVal ds As DataSet, ByVal ptn As LMB020DAC.SelectCondition) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQL文のコンパイル
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), brCd))

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..C_OUTKA_L     " & vbNewLine)
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append("   AND OUTKA_NO_L = @FURI_NO    " & vbNewLine)

        Select Case ptn
            Case LMB020DAC.SelectCondition.PTN1
                Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_CONDITION)
                Call Me.SetOutKaSysDateTime(Me._Row, Me._SqlPrmList)
        End Select

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me._Row.Item("FURI_NO").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteOutKa", cmd)

        'SQLの発行
        Select Case ptn
            Case LMB020DAC.SelectCondition.PTN1
                Me.UpdateResultChk(cmd)
            Case Else
                MyBase.GetUpdateResult(cmd)
        End Select

        Return ds

    End Function

#End Region

    'ADD 2022/11/07 倉庫写真アプリ対応 START
#Region "INKA_PHOTO"

    ''' <summary>
    '''入荷写真データの論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷写真データ論理削除SQLの構築・発行</remarks>
    Private Function UpdateInkaPhotoSysDelFlg(ByVal ds As DataSet) As DataSet
        Return Me.UpdateComDelFlg(ds, "B_INKA_PHOTO")
    End Function

#End Region
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#Region "共通"

    ''' <summary>
    ''' 配下データの論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dbTblNm">DBのTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>論理削除SQLの構築・発行</remarks>
    Private Function UpdateComDelFlg(ByVal ds As DataSet, ByVal dbTblNm As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQL構築
        Me._StrSql.Append(String.Concat(" UPDATE $LM_TRN$..", dbTblNm, "      ", vbNewLine))
        Me._StrSql.Append(" SET                            " & vbNewLine)
        Me._StrSql.Append(LMB020DAC.SQL_COM_UPDATE_DEL_FLG)
        Me._StrSql.Append(String.Concat("   AND INKA_NO_L = @INKA_NO_L   ", vbNewLine))

        'SQL文のコンパイル
        Me._Row = inTbl.Rows(0)
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), brCd))

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList, brCd)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateComDelFlg", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "LM_MST..M_FILE"
    ''' <summary>
    ''' M_FILEを物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteMFileData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_FILE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL構築
        Me._StrSql.Clear()
        Me._StrSql.AppendLine(" DELETE FROM LM_MST..M_FILE                      ")
        Me._StrSql.AppendLine(" WHERE                                           ")
        Me._StrSql.AppendLine("     KEY_TYPE_KBN  = @KEY_TYPE                   ")
        Me._StrSql.AppendLine(" AND FILE_TYPE_KBN = @FILE_TYPE                  ")
        Me._StrSql.AppendLine(" AND KEY_NO     LIKE @CONTROL_NO_L               ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString)

        'パラメータ設定
        Call Me.SetDeleteMFileParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "DeleteTabletImageData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">SQK</param>
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
    ''' 更新項目設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetUpNm(ByVal ds As DataSet, ByVal sql As String) As String

        Dim printType As String = ds.Tables("LMB020_PRINT_TYPE").Rows(0)("PRINT_TYPE").ToString()
        Dim update1 As String = String.Empty
        Dim update2 As String = String.Empty

        '入荷作業進捗区分設定
        Select Case printType
            'UPD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
            'Case PRINT_HOUKOKUSHO, PRINT_HOUKOKU_CHECKLIST, PRINT_DECI_MONITER '2012/12/06入荷報告チェックリスト(PRINT_HOUKOKU_CHECKLIST)追加:2012/10/10入荷確定入力モニター表追加:
            Case PRINT_HOUKOKUSHO, PRINT_HOUKOKU_CHECKLIST, PRINT_DECI_MONITER, PRINT_HOUKOKUSHO_KAKUIN   '1 依頼番号 : 002747  入荷報告書(角印)　追加
                update1 = "HOUKOKUSYO_PR_DATE"
                update2 = "HOUKOKUSYO_PR_USER"

            Case PRINT_CHECKLIST
                update1 = "CHECKLIST_PRT_DATE"
                update2 = "CHECKLIST_PRT_USER"

            Case PRINT_UKETSUKEHYOU
                update1 = "UKETSUKELIST_PRT_DATE"
                update2 = "UKETSUKELIST_PRT_USER"

        End Select

        sql = sql.Replace("$UPDATE1$", update1)
        sql = sql.Replace("$UPDATE2$", update2)

        Return sql

    End Function

#If True Then   'ADD 2020/08/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
#Region "M_GOODS_DETAILS"

    ''' <summary>
    ''' データ取得処理(商品明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoodsMeisaiOkiba(ByVal ds As DataSet) As DataSet

        Dim inTbl As DataTable = ds.Tables("LMB020_GOODS_DETAILS_GET")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB020DAC.SQL_SELECT_M_GOODS_DETAILS)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetGoodsMeisaiParameter(Me._SqlPrmList, Me._Row)


        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectDataGoodsMeisaiOkiba", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()
        Dim goodsCnt As Integer = 0

        If reader.HasRows() = True Then

            map.Add("SET_NAIYO", "SET_NAIYO")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB020_GOODS_DETAILS_SET")

            '処理件数の設定
            goodsCnt = ds.Tables("LMB020_GOODS_DETAILS_SET").Rows.Count

        End If
        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#End Region
#End If
    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMB020DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, ptn)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        '2021/04/15 入荷データ編集画面タイムアウト暫定対策
        cmd.CommandTimeout = 600

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectListData", cmd)

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

        'SQLの発行
        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cnt">件数</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        'SQLの発行
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)のレコードを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function GetInkaMDataRow(ByVal ds As DataSet, ByVal dr As DataRow) As DataRow

        Dim dt As DataTable = ds.Tables("LMB020_INKA_M")

        Return dt.Select(String.Concat("INKA_NO_L = '", dr.Item("INKA_NO_L").ToString(), "' " _
                                       , "AND INKA_NO_M = '", dr.Item("INKA_NO_M").ToString(), "' " _
                                       ))(0)

    End Function

    ''' <summary>
    ''' 入荷(小)のレコードを取得
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Private Function GetInkaSDataRow(ByVal dt As DataTable, ByVal dr As DataRow) As DataRow

        Return dt.Select(String.Concat("INKA_NO_L = '", dr.Item("INKA_NO_L").ToString(), "' " _
                                       , "AND INKA_NO_M = '", dr.Item("INKA_NO_M").ToString(), "' " _
                                       , "AND INKA_NO_S = '", dr.Item("INKA_NO_S").ToString(), "' " _
                                       ))(0)

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal ptn As LMB020DAC.SelectCondition)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            Select Case ptn

                Case LMB020DAC.SelectCondition.PTN1

                    prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))

                Case LMB020DAC.SelectCondition.PTN2

                    prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", String.Concat(.Item("INKA_NO_L").ToString(), "%"), DBDataType.CHAR))

            End Select


        End With

    End Sub

#If True Then   'ADD 2020/08/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
    ''' <summary>
    ''' M_GOODS_DETAILS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsMeisaiParameter(ByVal prmList As ArrayList, ByRef targetRow As DataRow)

        With targetRow

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SUB_KB", .Item("SUB_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub
#End If

    ''' <summary>
    ''' パラメータ設定モジュール(荷主明細存在チェック)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByRef targetRow As DataRow)

        With targetRow

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetDataInsertParameter(ByVal prmList As ArrayList) As String()

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        SetDataInsertParameter = Me.SetSysdataParameter(prmList)
        Return SetDataInsertParameter

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataParameter(ByVal prmList As ArrayList) As String()

        'システム項目
        SetSysdataParameter = Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Return SetSysdataParameter

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataTimeParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", sysDateTime(1), DBDataType.CHAR))

        Return sysDateTime

    End Function

    ''' <summary>
    ''' INKA_Lの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TP", .Item("INKA_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_KB", .Item("INKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STORAGE_DUE_DATE", .Item("STORAGE_DUE_DATE").ToString(), DBDataType.CHAR))     'ADD 20170710
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", .Item("INKA_PLAN_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", .Item("INKA_PLAN_QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", .Item("INKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Item("BUYER_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", .Item("UNCHIN_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KENPIN_WK_STATUS", .Item("WH_KENPIN_WK_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", .Item("WH_TAB_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_IMP_YN", .Item("WH_TAB_IMP_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_SAGYO_SIJI_STATUS").ToString(), DBDataType.CHAR))
            'DEL 2019/10/10 要望管理007373  prmList.Add(MyBase.GetSqlParameter("@STOP_ALLOC", .Item("STOP_ALLOC").ToString(), DBDataType.CHAR)) 'ADD 2019/08/01 要望管理005237
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_NO_SIJI_FLG", .Item("WH_TAB_NO_SIJI_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' INKA_Mの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", .Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", .Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' INKA_Sの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaSComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal brCd As String)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KONSU", Me.FormatNumValue(.Item("KONSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUG_YN", .Item("BUG_YN").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START ADD 2013/09/10 KURIHARA WIT対応
    ''' <summary>
    ''' INKA_WKの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaWkComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KENPIN_KAKUTEI_FLG", .Item("KENPIN_KAKUTEI_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END   ADD 2013/09/10 KURIHARA WIT対応


    Private Sub SetInkaQrParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)
        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEQ", .Item("SEQ").ToString(), DBDataType.CHAR))

        End With
    End Sub


    ''' <summary>
    ''' ZAI_TRSの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="inkaLDr">入荷(大)レコード</param>
    ''' <param name="inkaMDr">入荷(中)レコード</param>
    ''' <param name="inkaSDr">入荷(小)レコード</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComParameter(ByVal conditionRow As DataRow _
                                      , ByVal inkaLDr As DataRow _
                                      , ByVal inkaMDr As DataRow _
                                      , ByVal inkaSDr As DataRow _
                                      , ByVal prmList As ArrayList _
                                      )

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inkaLDr.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", inkaLDr.Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOU_NO", inkaSDr.Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SITU_NO", inkaSDr.Item("SITU_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZONE_CD", inkaSDr.Item("ZONE_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCA", inkaSDr.Item("LOCA").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", inkaSDr.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", inkaLDr.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", inkaLDr.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", inkaMDr.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", inkaSDr.Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", inkaSDr.Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", inkaLDr.Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", inkaLDr.Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", inkaSDr.Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", inkaSDr.Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", inkaSDr.Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", inkaSDr.Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", inkaSDr.Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", inkaSDr.Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(inkaSDr.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", inkaSDr.Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", inkaSDr.Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", inkaSDr.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", inkaSDr.Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))

            'WIT対応 2013.12.10 Start
            prmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            'WIT対応 2013.12.10 End

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Call Me.SetUnsoLPkParameter(conditionRow, prmList)
            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", .Item("UNSO_PKG_NB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)PK
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLPkParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", .Item("UNSO_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", .Item("UNSO_TTL_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASU", .Item("HASU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", .Item("SEIQ_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_UNCHIN", .Item("SEIQ_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CITY_EXTC", .Item("SEIQ_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WINT_EXTC", .Item("SEIQ_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_RELY_EXTC", .Item("SEIQ_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TOLL", .Item("SEIQ_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_INSU", .Item("SEIQ_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", .Item("DECI_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", .Item("DECI_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", .Item("DECI_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", .Item("KANRI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", .Item("KANRI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", .Item("KANRI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", .Item("KANRI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", .Item("KANRI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", .Item("KANRI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetShiharaiComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", .Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", .Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_SYARYO_KB", .Item("SHIHARAI_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_PKG_UT", .Item("SHIHARAI_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_NG_NB", Me.FormatNumValue(.Item("SHIHARAI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_DANGER_KB", .Item("SHIHARAI_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_BUNRUI_KB", .Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_KYORI", Me.FormatNumValue(.Item("SHIHARAI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WT", Me.FormatNumValue(.Item("SHIHARAI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNCHIN", Me.FormatNumValue(.Item("SHIHARAI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_CITY_EXTC", Me.FormatNumValue(.Item("SHIHARAI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WINT_EXTC", Me.FormatNumValue(.Item("SHIHARAI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_RELY_EXTC", Me.FormatNumValue(.Item("SHIHARAI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TOLL", Me.FormatNumValue(.Item("SHIHARAI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_INSU", Me.FormatNumValue(.Item("SHIHARAI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", .Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", Me.FormatNumValue(.Item("DECI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", Me.FormatNumValue(.Item("DECI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", Me.FormatNumValue(.Item("DECI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", Me.FormatNumValue(.Item("DECI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", Me.FormatNumValue(.Item("DECI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", Me.FormatNumValue(.Item("DECI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", Me.FormatNumValue(.Item("DECI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", Me.FormatNumValue(.Item("DECI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", Me.FormatNumValue(.Item("DECI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", Me.FormatNumValue(.Item("KANRI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", Me.FormatNumValue(.Item("KANRI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", Me.FormatNumValue(.Item("KANRI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", Me.FormatNumValue(.Item("KANRI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", Me.FormatNumValue(.Item("KANRI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", Me.FormatNumValue(.Item("KANRI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

    ''' <summary>
    ''' SAGYOの更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="inkaLDr">入荷(大)のDataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal inkaLDr As DataRow)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", inkaLDr.Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", Me.FormatNumValue(.Item("SAGYO_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", Me.FormatNumValue(.Item("SAGYO_UP").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", Me.FormatNumValue(.Item("SAGYO_GK").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", inkaLDr.Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", inkaLDr.Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SIJI", .Item("REMARK_SIJI").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' SAGYOのPKパラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoPkParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 論理削除時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Private Function SetUpdateDelFlgParameter(ByVal prmList As ArrayList, ByVal brCd As String) As String()

        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.ON, DBDataType.CHAR))
        SetUpdateDelFlgParameter = Me.SetSysdataParameter(prmList)
        Return SetUpdateDelFlgParameter

    End Function

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutKaSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", dr.Item("OUTKA_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", dr.Item("OUTKA_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' M_FILE削除
    ''' </summary>
    ''' <param name="dr">dararow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDeleteMFileParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@FILE_TYPE", dr.Item("FILE_TYPE").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@KEY_TYPE", dr.Item("KEY_TYPE").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@CONTROL_NO_L", String.Concat(dr.Item("CONTROL_NO_L").ToString, "%"), DBDataType.CHAR))

    End Sub
    ''' <summary>
    ''' M_FILE登録
    ''' </summary>
    ''' <param name="dr">dararow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertMFileParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@FILE_TYPE", dr.Item("FILE_TYPE").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@KEY_TYPE", dr.Item("KEY_TYPE").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@CONTROL_NO_L", dr.Item("CONTROL_NO_L").ToString, DBDataType.CHAR))

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

    '2014.04.24 CALT対応 追加 --ST--
#Region "CALT対応SQL"
#Region "Select"
#Region "キャンセル"
    ''' <summary>
    ''' キャンセルデータ抜出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSendCancel(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB020_INKA_L").Rows(0)

        'SQL構築
        Me._StrSql.AppendLine(LMB020DAC.SQL_SELECT_SEND_CANCEL)
        Me._StrSql.AppendLine(LMB020DAC.SQL_SEND_CANCEL_ORDER_BY)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetInkaParameterOnCalt(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "SelectSendCancel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("SEND_SEQ", "SEND_SEQ")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_OUT_L", "REMARK_OUT_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OUTKA_FROM_ORD_NO_M", "OUTKA_FROM_ORD_NO_M")
        map.Add("BUYER_ORD_NO_M", "BUYER_ORD_NO_M")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("INKA_WT", "INKA_WT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("IRIME", "IRIME")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("REMARK_OUT_S", "REMARK_OUT_S")
        map.Add("SEND_SHORI_FLG", "SEND_SHORI_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB020_INKA_DEL_OUT")

        MyBase.SetResultCount(ds.Tables("LMB020_INKA_DEL_OUT").Rows.Count)
        reader.Close()

        Return ds

    End Function

#End Region

#Region "倉庫チェック"
    ''' <summary>
    ''' 倉庫チェック
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkWhCd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB020_INKA_L").Rows(0)

        'SQL構築
        Me._StrSql.AppendLine(LMB020DAC.SQL_SELECT_WH_CD_EXIST)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetchkWhCdParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "ChkWhCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#End Region

#Region "Insert"
    ''' <summary>
    ''' 入荷報告データ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendInkaData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB020_INKA_DEL_OUT").Rows(0)

        'SQL構築
        Me._StrSql.Append(LMB020DAC.SQL_INSERT_SEND_INKA_DATA)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetInkaInsertParameterOnCalt(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystems()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "InsertSendInka", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "パラメータ設定"
    ''' <summary>
    ''' キャンセルパラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaParameterOnCalt(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 倉庫チェックパラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetchkWhCdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 入荷予定データ作成
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaInsertParameterOnCalt(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_SEQ", .Item("SEND_SEQ").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_KBN", .Item("DATA_KBN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", .Item("CUST_NM_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Item("BUYER_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_L", .Item("REMARK_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT_L", .Item("REMARK_OUT_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", .Item("GOODS_NM_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", .Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", .Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_M", .Item("REMARK_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NB", .Item("INKA_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_WT", .Item("INKA_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KONSU", .Item("KONSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HASU", .Item("HASU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JAN_CD", .Item("JAN_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_STR_DATE", .Item("ONDO_STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_END_DATE", .Item("ONDO_END_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_MX", .Item("ONDO_MX").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_MM", .Item("ONDO_MM").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_S", .Item("REMARK_S").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT_S", .Item("REMARK_OUT_S").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_SHORI_FLG", .Item("SEND_SHORI_FLG").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystems()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

#End Region

#End Region
    '2014.04.24 CALT対応 追加 --ED--

    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
#Region "ｱｸﾞﾘﾏｰﾄ対応"
#Region "UPDATE"
    ''' <summary>
    ''' 入荷WKテーブル更新処理(管理番号設定)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷WKテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaKenpinWkData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_KENPIN_WK_DATA")
        Dim max As Integer = inTbl.Rows.Count - 1

        If -1 < max Then

            'SQL文のコンパイル
            Dim brCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB020DAC.SQL_UPDATE_INKA_KENPIN_WK, brCd))

            For i As Integer = max To 0 Step -1

                'SQLパラメータ初期化
                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

                '条件rowの格納
                Me._Row = inTbl.Rows(i)

                '更新処理
                Call Me.UpdateInkaKenpinWkData(cmd)

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷WKテーブル更新処理(管理番号設定)(パラメータ設定)
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaKenpinWkData(ByVal cmd As SqlCommand) As Boolean

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetInkaKenpinWkComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaKenpinWkData", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return True

    End Function

    'ADD S 2019/12/02 006350
    ''' <summary>
    ''' 入荷WKテーブル更新処理(INKA_TORI_FLGリセット。入荷M/S削除用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷WKテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaKenpinWkToriFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_KENPIN_WK_TORI_RESET")

        For Each inRow As DataRow In inTbl.Rows

            _StrSql.Clear()
            _StrSql.Append(LMB020DAC.SQL_UPDATE_INKA_KENPIN_WK_TORI_FLG)
            _StrSql.Append("AND INKA_NO_M    = @INKA_NO_M         " & vbNewLine)
            If inRow("INKA_NO_S").ToString <> "" Then
                _StrSql.Append("AND INKA_NO_S    = @INKA_NO_S         " & vbNewLine)
            End If

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(_StrSql.ToString, inRow("NRS_BR_CD").ToString))

            'パラメータ設定
            Me._SqlPrmList.Clear()
            Call Me.SetUpdateInkaKenpinWkToriFlgParameter(inRow, Me._SqlPrmList)
            Call Me.SetSysdataParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.Clear()
            cmd.Parameters.AddRange(_SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaKenpinWkToriFlg", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷WKテーブル更新処理(INKA_TORI_FLGリセット。入荷L削除用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷WKテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaKenpinWkToriFlgInkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB020_INKA_L")

        For Each inRow As DataRow In inTbl.Rows

            _StrSql.Clear()
            _StrSql.Append(LMB020DAC.SQL_UPDATE_INKA_KENPIN_WK_TORI_FLG)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(_StrSql.ToString, inRow("NRS_BR_CD").ToString))

            'パラメータ設定
            Me._SqlPrmList.Clear()
            Call Me.SetUpdateInkaKenpinWkToriFlgInkaLParameter(inRow, Me._SqlPrmList)
            Call Me.SetSysdataParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.Clear()
            cmd.Parameters.AddRange(_SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog("LMB020DAC", "UpdateInkaKenpinWkToriFlgInkaL", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

        Next

        Return ds

    End Function
    'ADD E 2019/12/02 006350

#End Region

#Region "パラメータ設定"
    ''' <summary>
    ''' INKA_KENPIN_WKの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaKenpinWkComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INPUT_DATE", .Item("INPUT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEQ", .Item("SEQ").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))


        End With

    End Sub

    'ADD S 2019/12/02 006350
    ''' <summary>
    ''' INKA_KENPIN_WKの更新パラメータ設定(INKA_TORI_FLGリセット。入荷M/S削除用)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateInkaKenpinWkToriFlgParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@INKA_TORI_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            If .Item("INKA_NO_S").ToString() <> "" Then
                prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' INKA_KENPIN_WKの更新パラメータ設定(INKA_TORI_FLGリセット。入荷L削除用)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateInkaKenpinWkToriFlgInkaLParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@INKA_TORI_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'ADD E 2019/12/02 006350

#End Region

#End Region
    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

#End Region

End Class
