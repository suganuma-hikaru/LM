' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI480  : 古河請求(ディック)
'  作  成  者       :  kido
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI480DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI480DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理"

#Region "検索処理 SELECT句"

    ''' <summary>
    ''' 抽出区分01:DICG関係請求、シート:神奈川配送分横持
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_0101 As String =
                   "SELECT                                                                                                             " & vbNewLine _
                 & "-- F02_01.OUTKA_PLAN_DATE                  AS OUTKA_PLAN_DATE                                                        " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                    AS ARR_PLAN_DATE                                                          " & vbNewLine _
                 & ",KBN_01.KBN_NM1                          AS BIN                                                                    " & vbNewLine _
                 & ",SUM(F02_01.UNSO_WT    )                 AS SUM_WT                                                                 " & vbNewLine _
                 & ", 'kg'                                   AS WT_TANI                                                                " & vbNewLine _
                 & ", 13600                                  AS TANKA                                                                  " & vbNewLine _
                 & "--1便                                                                                                              " & vbNewLine _
                 & ",CASE WHEN SUM(F02_01.UNSO_WT    ) >= 3000 AND F02_01.BIN_KB = '02'                                                " & vbNewLine _
                 & " THEN FLOOR( ( SUM(F02_01.UNSO_WT) - 3000) * 4.7)                                                                  " & vbNewLine _
                 & "      WHEN SUM(F02_01.UNSO_WT    ) >= 2000 AND F02_01.BIN_KB = '03'                                                " & vbNewLine _
                 & " THEN FLOOR(( SUM(F02_01.UNSO_WT) - 2000) * 5.2)                                                                   " & vbNewLine _
                 & " ELSE 0                                                                                                            " & vbNewLine _
                 & " END                                     AS CHOKA                                                                  " & vbNewLine _
                 & ",CASE WHEN F02_01.BIN_KB = '02' THEN 5710                                                                          " & vbNewLine _
                 & "      WHEN F02_01.BIN_KB = '03' THEN 4530                                                                          " & vbNewLine _
                 & " END                                     AS KOSOKU_TSUKORYO                                                        " & vbNewLine _
                 & "FROM       $LM_TRN$..F_UNSO_L           F02_01                                                                     " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_01 ON  F02_01.TRIP_NO = F01_01.TRIP_NO                                   " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_02 ON  F02_01.TRIP_NO_SYUKA = F01_02.TRIP_NO                             " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_03 ON  F02_01.TRIP_NO_TYUKEI = F01_03.TRIP_NO                            " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_04 ON  F02_01.TRIP_NO_HAIKA = F01_04.TRIP_NO                             " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT F03_01.NRS_BR_CD                                                                          " & vbNewLine _
                 & "                        ,F03_01.UNSO_NO_L                                                                          " & vbNewLine _
                 & "                        ,SUM(ISNULL(M08_01.STD_WT_KGS,'0')) AS STD_WT_KG                                           " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_UNSO_M        F03_01                                                          " & vbNewLine _
                 & "               LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                          " & vbNewLine _
                 & "                      ON F03_01.NRS_BR_CD        = M08_01.NRS_BR_CD                                                " & vbNewLine _
                 & "                     AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS                                             " & vbNewLine _
                 & "                     AND M08_01.SYS_DEL_FLG      = '0'                                                             " & vbNewLine _
                 & "                   WHERE F03_01.SYS_DEL_FLG      = '0'                                                             " & vbNewLine _
                 & "                GROUP BY F03_01.NRS_BR_CD                                                                          " & vbNewLine _
                 & "                        ,F03_01.UNSO_NO_L                                                                          " & vbNewLine _
                 & "            )                           F03_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F03_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F03_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "                        ,MIN(F04_01.SEIQ_GROUP_NO)        AS GROUP_NO                                              " & vbNewLine _
                 & "                        ,SUM(   F04_01.DECI_UNCHIN                                                                 " & vbNewLine _
                 & "                              + F04_01.DECI_CITY_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_WINT_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_RELY_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_TOLL                                                                   " & vbNewLine _
                 & "                              + F04_01.DECI_INSU                                                                   " & vbNewLine _
                 & "                             )                            AS UNCHIN                                                " & vbNewLine _
                 & "                        ,MAX(F04_01.SEIQ_KYORI)           AS KYORI                                                 " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_UNCHIN_TRS       F04_01                                                       " & vbNewLine _
                 & "                   WHERE SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                 & "                GROUP BY NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "            )                           F04_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F04_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F04_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "--START UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)                                           " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "                        ,MIN(F05_01.SHIHARAI_GROUP_NO)    AS GROUP_NO                                              " & vbNewLine _
                 & "                        ,SUM(   F05_01.DECI_UNCHIN                                                                 " & vbNewLine _
                 & "                              + F05_01.DECI_CITY_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_WINT_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_RELY_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_TOLL                                                                   " & vbNewLine _
                 & "                              + F05_01.DECI_INSU                                                                   " & vbNewLine _
                 & "                             )                   AS SHIHARAI_UNCHIN                                                " & vbNewLine _
                 & "                        ,MAX(F05_01.SHIHARAI_KYORI)           AS KYORI                                             " & vbNewLine _
                 & "                        ,MAX(F05_01.SHIHARAI_FIXED_FLAG)  AS SHIHARAI_FI                                           " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_SHIHARAI_TRS       F05_01                                                     " & vbNewLine _
                 & "                   WHERE SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                 & "                GROUP BY NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "            )                           F05_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F05_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F05_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "--END UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_CUST             M07_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M07_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M07_01.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_M                = M07_01.CUST_CD_M                                                           " & vbNewLine _
                 & " AND  M07_01.CUST_CD_S                = '00'                                                                       " & vbNewLine _
                 & " AND  M07_01.CUST_CD_SS               = '00'                                                                       " & vbNewLine _
                 & " AND  M07_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M38_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_CD                  = M38_01.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_BR_CD               = M38_01.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_02                                                                     " & vbNewLine _
                 & "  ON  F01_01.NRS_BR_CD                = M38_02.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_01.UNSOCO_CD                = M38_02.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_01.UNSOCO_BR_CD             = M38_02.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_03                                                                     " & vbNewLine _
                 & "  ON  F01_02.NRS_BR_CD                = M38_03.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_02.UNSOCO_CD                = M38_03.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_02.UNSOCO_BR_CD             = M38_03.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_04                                                                     " & vbNewLine _
                 & "  ON  F01_03.NRS_BR_CD                = M38_04.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_03.UNSOCO_CD                = M38_04.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_03.UNSOCO_BR_CD             = M38_04.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_05                                                                     " & vbNewLine _
                 & "  ON  F01_04.NRS_BR_CD                = M38_05.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_04.UNSOCO_CD                = M38_05.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_04.UNSOCO_BR_CD             = M38_05.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_05.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M10_01.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.ORIG_CD                  = M10_01.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_02.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M10_02.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.DEST_CD                  = M10_02.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M10_02_JIS                                                                 " & vbNewLine _
                 & "  ON  M10_02.JIS                      = M10_02_JIS.JIS_CD                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_03                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_03.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  'ZZZZZ'                         = M10_03.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.ORIG_CD                  = M10_03.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_04                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_04.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  'ZZZZZ'                         = M10_04.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.DEST_CD                  = M10_04.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M10_04_JIS                                                                 " & vbNewLine _
                 & "  ON  M10_04.JIS                      = M10_04_JIS.JIS_CD                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.SYUKA_TYUKEI_CD          = M12_01.JIS_CD                                                              " & vbNewLine _
                 & " AND  M12_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.HAIKA_TYUKEI_CD          = M12_02.JIS_CD                                                              " & vbNewLine _
                 & " AND  M12_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_AREA             M36_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M36_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.AREA_CD                  = M36_01.AREA_CD                                                             " & vbNewLine _
                 & " --要望番号1202 追加START(2012.07.02)                                                                              " & vbNewLine _
                 & " AND  F02_01.BIN_KB                   = M36_01.BIN_KB                                                              " & vbNewLine _
                 & " --要望番号1202 追加END  (2012.07.02)                                                                              " & vbNewLine _
                 & " AND  M10_02.JIS                      = M36_01.JIS_CD                                                              " & vbNewLine _
                 & " AND  M36_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_01                                                                     " & vbNewLine _
                 & "  ON  F01_01.CAR_KEY                  = M39_01.CAR_KEY                                                             " & vbNewLine _
                 & " AND  M39_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_02                                                                     " & vbNewLine _
                 & "  ON  F01_04.CAR_KEY                  = M39_02.CAR_KEY                                                             " & vbNewLine _
                 & " AND  M39_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_01                                                                     " & vbNewLine _
                 & "  ON  F01_01.DRIVER_CD                = M37_01.DRIVER_CD                                                           " & vbNewLine _
                 & " AND  M37_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_02                                                                     " & vbNewLine _
                 & "  ON  F01_04.DRIVER_CD                = M37_02.DRIVER_CD                                                           " & vbNewLine _
                 & " AND  M37_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.BIN_KB                   = KBN_01.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_01.KBN_GROUP_CD             = 'U001'                                                                     " & vbNewLine _
                 & " AND  KBN_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.TARIFF_BUNRUI_KB         = KBN_02.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_02.KBN_GROUP_CD             = 'T015'                                                                     " & vbNewLine _
                 & " AND  KBN_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_03                                                                     " & vbNewLine _
                 & "  ON  F02_01.UNSO_ONDO_KB             = KBN_03.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_03.KBN_GROUP_CD             = 'U006'                                                                     " & vbNewLine _
                 & " AND  KBN_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_04                                                                     " & vbNewLine _
                 & "  ON  F02_01.MOTO_DATA_KB             = KBN_04.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_04.KBN_GROUP_CD             = 'M004'                                                                     " & vbNewLine _
                 & " AND  KBN_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_05                                                                     " & vbNewLine _
                 & "  ON  M39_01.CAR_TP_KB                = KBN_05.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_05.KBN_GROUP_CD             = 'S023'                                                                     " & vbNewLine _
                 & " AND  KBN_05.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_06                                                                     " & vbNewLine _
                 & "  ON  M39_02.CAR_TP_KB                = KBN_06.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_06.KBN_GROUP_CD             = 'S023'                                                                     " & vbNewLine _
                 & " AND  KBN_06.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..S_USER             S01_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.SYS_ENT_USER             = S01_01.USER_CD                                                             " & vbNewLine _
                 & " AND  S01_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & " --要望番号2140 (2013.12.25) 追加START                                                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_TRN$..C_OUTKA_L        C01_01                                                                       " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = C01_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.INOUTKA_NO_L             = C01_01.OUTKA_NO_L                                                          " & vbNewLine _
                 & " AND  F02_01.MOTO_DATA_KB             = '20'                                                                       " & vbNewLine _
                 & " AND  C01_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                                                            " & vbNewLine _
                 & " --要望番号2063 (2015.05.27) 追加START                                                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_TRN$..H_TEHAIINFO_TBL    H_TEHAI                                                                    " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = H_TEHAI.NRS_BR_CD                                                          " & vbNewLine _
                 & " AND  F02_01.TRIP_NO                  = H_TEHAI.TRIP_NO                                                            " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = H_TEHAI.UNSO_NO_L                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_27                                                                     " & vbNewLine _
                 & "  ON  H_TEHAI.TEHAI_SYUBETSU          = KBN_27.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_27.KBN_GROUP_CD             = 'U027'                                                                     " & vbNewLine _
                 & " AND  KBN_27.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & " --要望番号2140 (2013.12.25) 追加END                                                                               " & vbNewLine _
                 & "WHERE F02_01.NRS_BR_CD                = @NRS_BR_CD                                                                 " & vbNewLine _
                 & "  AND F02_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 -- START --                                                            " & vbNewLine _
                 & "  AND F02_01.UNSO_TEHAI_KB            = '10'                                                                       " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                                                            " & vbNewLine _
                 & " AND F02_01.YUSO_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                 & " AND (M38_01.UNSOCO_CD = 'seiko' OR M38_01.UNSOCO_CD = 'kobay')                                                    " & vbNewLine _
                 & " AND F02_01.CUST_CD_L =  '30001'                                                                                   " & vbNewLine _
                 & " AND F02_01.CUST_CD_M <> '01'                                                                                      " & vbNewLine _
                 & " AND F02_01.ARR_PLAN_DATE >= @KIKAN_F_DATE                                                                         " & vbNewLine _
                 & " AND F02_01.ARR_PLAN_DATE <= @KIKAN_T_DATE                                                                         " & vbNewLine _
                 & " AND F02_01.TYUKEI_HAISO_FLG = '00'                                                                                " & vbNewLine _
                 & " AND F02_01.BIN_KB in ('02','03')                                                                                  " & vbNewLine _
                 & "GROUP BY                                                                                                           " & vbNewLine _
                 & "-- F02_01.OUTKA_PLAN_DATE                                                                                            " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                                                                                              " & vbNewLine _
                 & ",KBN_01.KBN_NM1                                                                                                    " & vbNewLine _
                 & ",F02_01.BIN_KB                                                                                                     " & vbNewLine _
                 & "ORDER BY                                                                                                           " & vbNewLine _
                 & "-- F02_01.OUTKA_PLAN_DATE                                                                                            " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                                                                                              " & vbNewLine _
                 & ",KBN_01.KBN_NM1                                                                                                    " & vbNewLine

    ''' <summary>
    ''' 抽出区分01:DICG関係請求、シート:神奈川配送分横持(聖亘提出用)M07_01
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_0102 As String =
                  "SELECT                                                                                                             " & vbNewLine _
                 & " ''                                      AS RPT_ID                                                                 " & vbNewLine _
                 & ",MNRS.NRS_BR_NM                          AS NRS_BR_NM                                                              " & vbNewLine _
                 & ",M07_01.CUST_NM_L                        AS CUST_NM_L                                                              " & vbNewLine _
                 & ",@KIKAN_F_DATE                           AS KIKAN_F_DATE                                                           " & vbNewLine _
                 & ",@KIKAN_T_DATE                           AS KIKAN_T_DATE                                                           " & vbNewLine _
                 & "--,F02_01.OUTKA_PLAN_DATE                  AS OUTKA_PLAN_DATE                                                        " & vbNewLine _
                 & ",F02_01.ARR_PLAN_DATE                    AS ARR_PLAN_DATE                                                          " & vbNewLine _
                 & ",KBN_01.KBN_NM1                          AS BIN                                                                    " & vbNewLine _
                 & ",SUM(F02_01.UNSO_WT    )                 AS SUM_WT                                                                 " & vbNewLine _
                 & ", 'kg'                                   AS WT_TANI                                                                " & vbNewLine _
                 & ",KBN_08.VALUE1                           AS T3MADE                                                                 " & vbNewLine _
                 & ",0                                       AS EXCESS_CHARGE                                                          " & vbNewLine _
                 & ",0                                       AS TOTAL_FARE                                                             " & vbNewLine _
                 & ",KBN_07.VALUE1                           AS HIGH_SPEED                                                             " & vbNewLine _
                 & ",KBN_08.VALUE2                           AS T3_OVER_CHARGE                                                         " & vbNewLine _
                 & "FROM       $LM_TRN$..F_UNSO_L           F02_01                                                                     " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_01 ON  F02_01.TRIP_NO = F01_01.TRIP_NO                                   " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_02 ON  F02_01.TRIP_NO_SYUKA = F01_02.TRIP_NO                             " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_03 ON  F02_01.TRIP_NO_TYUKEI = F01_03.TRIP_NO                            " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_04 ON  F02_01.TRIP_NO_HAIKA = F01_04.TRIP_NO                             " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT F03_01.NRS_BR_CD                                                                          " & vbNewLine _
                 & "                        ,F03_01.UNSO_NO_L                                                                          " & vbNewLine _
                 & "                        ,SUM(ISNULL(M08_01.STD_WT_KGS,'0')) AS STD_WT_KGS                                          " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_UNSO_M        F03_01                                                          " & vbNewLine _
                 & "               LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                          " & vbNewLine _
                 & "                      ON F03_01.NRS_BR_CD        = M08_01.NRS_BR_CD                                                " & vbNewLine _
                 & "                     AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS                                             " & vbNewLine _
                 & "                     AND M08_01.SYS_DEL_FLG      = '0'                                                             " & vbNewLine _
                 & "                   WHERE F03_01.SYS_DEL_FLG      = '0'                                                             " & vbNewLine _
                 & "                GROUP BY F03_01.NRS_BR_CD                                                                          " & vbNewLine _
                 & "                        ,F03_01.UNSO_NO_L                                                                          " & vbNewLine _
                 & "            )                           F03_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F03_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F03_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "                        ,MIN(F04_01.SEIQ_GROUP_NO)        AS GROUP_NO                                              " & vbNewLine _
                 & "                        ,SUM(   F04_01.DECI_UNCHIN                                                                 " & vbNewLine _
                 & "                              + F04_01.DECI_CITY_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_WINT_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_RELY_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_TOLL                                                                   " & vbNewLine _
                 & "                              + F04_01.DECI_INSU                                                                   " & vbNewLine _
                 & "                             )                            AS UNCHIN                                                " & vbNewLine _
                 & "                        ,MAX(F04_01.SEIQ_KYORI)           AS KYORI                                                 " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_UNCHIN_TRS       F04_01                                                       " & vbNewLine _
                 & "                   WHERE SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                 & "                GROUP BY NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "            )                           F04_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F04_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F04_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "--START UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)                                           " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "                        ,MIN(F05_01.SHIHARAI_GROUP_NO)    AS GROUP_NO                                              " & vbNewLine _
                 & "                        ,SUM(   F05_01.DECI_UNCHIN                                                                 " & vbNewLine _
                 & "                              + F05_01.DECI_CITY_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_WINT_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_RELY_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_TOLL                                                                   " & vbNewLine _
                 & "                              + F05_01.DECI_INSU                                                                   " & vbNewLine _
                 & "                             )                   AS SHIHARAI_UNCHIN                                                " & vbNewLine _
                 & "                        ,MAX(F05_01.SHIHARAI_KYORI)           AS KYORI                                             " & vbNewLine _
                 & "                        ,MAX(F05_01.SHIHARAI_FIXED_FLAG)  AS SHIHARAI_FIXED_FLAG                                   " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_SHIHARAI_TRS       F05_01                                                     " & vbNewLine _
                 & "                   WHERE SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                 & "                GROUP BY NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "            )                           F05_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F05_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F05_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "--END UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_CUST             M07_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M07_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M07_01.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_M                = M07_01.CUST_CD_M                                                           " & vbNewLine _
                 & " AND  M07_01.CUST_CD_S                = '00'                                                                       " & vbNewLine _
                 & " AND  M07_01.CUST_CD_SS               = '00'                                                                       " & vbNewLine _
                 & " AND  M07_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M38_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_CD                  = M38_01.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_BR_CD               = M38_01.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_02                                                                     " & vbNewLine _
                 & "  ON  F01_01.NRS_BR_CD                = M38_02.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_01.UNSOCO_CD                = M38_02.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_01.UNSOCO_BR_CD             = M38_02.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_03                                                                     " & vbNewLine _
                 & "  ON  F01_02.NRS_BR_CD                = M38_03.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_02.UNSOCO_CD                = M38_03.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_02.UNSOCO_BR_CD             = M38_03.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_04                                                                     " & vbNewLine _
                 & "  ON  F01_03.NRS_BR_CD                = M38_04.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_03.UNSOCO_CD                = M38_04.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_03.UNSOCO_BR_CD             = M38_04.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_05                                                                     " & vbNewLine _
                 & "  ON  F01_04.NRS_BR_CD                = M38_05.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_04.UNSOCO_CD                = M38_05.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_04.UNSOCO_BR_CD             = M38_05.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_05.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M10_01.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.ORIG_CD                  = M10_01.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_02.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M10_02.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.DEST_CD                  = M10_02.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M10_02_JIS                                                                 " & vbNewLine _
                 & "  ON  M10_02.JIS                      = M10_02_JIS.JIS_CD                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_03                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_03.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  'ZZZZZ'                         = M10_03.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.ORIG_CD                  = M10_03.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_04                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_04.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  'ZZZZZ'                         = M10_04.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.DEST_CD                  = M10_04.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M10_04_JIS                                                                 " & vbNewLine _
                 & "  ON  M10_04.JIS                      = M10_04_JIS.JIS_CD                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.SYUKA_TYUKEI_CD          = M12_01.JIS_CD                                                              " & vbNewLine _
                 & " AND  M12_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.HAIKA_TYUKEI_CD          = M12_02.JIS_CD                                                              " & vbNewLine _
                 & " AND  M12_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_AREA             M36_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M36_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.AREA_CD                  = M36_01.AREA_CD                                                             " & vbNewLine _
                 & " --要望番号1202 追加START(2012.07.02)                                                                              " & vbNewLine _
                 & " AND  F02_01.BIN_KB                   = M36_01.BIN_KB                                                              " & vbNewLine _
                 & " --要望番号1202 追加END  (2012.07.02)                                                                              " & vbNewLine _
                 & " AND  M10_02.JIS                      = M36_01.JIS_CD                                                              " & vbNewLine _
                 & " AND  M36_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_01                                                                     " & vbNewLine _
                 & "  ON  F01_01.CAR_KEY                  = M39_01.CAR_KEY                                                             " & vbNewLine _
                 & " AND  M39_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_02                                                                     " & vbNewLine _
                 & "  ON  F01_04.CAR_KEY                  = M39_02.CAR_KEY                                                             " & vbNewLine _
                 & " AND  M39_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_01                                                                     " & vbNewLine _
                 & "  ON  F01_01.DRIVER_CD                = M37_01.DRIVER_CD                                                           " & vbNewLine _
                 & " AND  M37_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_02                                                                     " & vbNewLine _
                 & "  ON  F01_04.DRIVER_CD                = M37_02.DRIVER_CD                                                           " & vbNewLine _
                 & " AND  M37_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.BIN_KB                   = KBN_01.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_01.KBN_GROUP_CD             = 'U001'                                                                     " & vbNewLine _
                 & " AND  KBN_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.TARIFF_BUNRUI_KB         = KBN_02.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_02.KBN_GROUP_CD             = 'T015'                                                                     " & vbNewLine _
                 & " AND  KBN_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_03                                                                     " & vbNewLine _
                 & "  ON  F02_01.UNSO_ONDO_KB             = KBN_03.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_03.KBN_GROUP_CD             = 'U006'                                                                     " & vbNewLine _
                 & " AND  KBN_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_04                                                                     " & vbNewLine _
                 & "  ON  F02_01.MOTO_DATA_KB             = KBN_04.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_04.KBN_GROUP_CD             = 'M004'                                                                     " & vbNewLine _
                 & " AND  KBN_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_05                                                                     " & vbNewLine _
                 & "  ON  M39_01.CAR_TP_KB                = KBN_05.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_05.KBN_GROUP_CD             = 'S023'                                                                     " & vbNewLine _
                 & " AND  KBN_05.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_06                                                                     " & vbNewLine _
                 & "  ON  M39_02.CAR_TP_KB                = KBN_06.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_06.KBN_GROUP_CD             = 'S023'                                                                     " & vbNewLine _
                 & " AND  KBN_06.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..S_USER             S01_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.SYS_ENT_USER             = S01_01.USER_CD                                                             " & vbNewLine _
                 & " AND  S01_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & " --要望番号2140 (2013.12.25) 追加START                                                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_TRN$..C_OUTKA_L        C01_01                                                                       " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = C01_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.INOUTKA_NO_L             = C01_01.OUTKA_NO_L                                                          " & vbNewLine _
                 & " AND  F02_01.MOTO_DATA_KB             = '20'                                                                       " & vbNewLine _
                 & " AND  C01_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                                                            " & vbNewLine _
                 & " --要望番号2063 (2015.05.27) 追加START                                                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_TRN$..H_TEHAIINFO_TBL    H_TEHAI                                                                    " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = H_TEHAI.NRS_BR_CD                                                          " & vbNewLine _
                 & " AND  F02_01.TRIP_NO                  = H_TEHAI.TRIP_NO                                                            " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = H_TEHAI.UNSO_NO_L                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_27                                                                     " & vbNewLine _
                 & "  ON  H_TEHAI.TEHAI_SYUBETSU          = KBN_27.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_27.KBN_GROUP_CD             = 'U027'                                                                     " & vbNewLine _
                 & " AND  KBN_27.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & " --要望番号2140 (2013.12.25) 追加END                                                                               " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_07                                                                     " & vbNewLine _
                 & "  ON  F02_01.BIN_KB                   = KBN_07.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_07.KBN_GROUP_CD             = 'U038'                                                                     " & vbNewLine _
                 & " AND  KBN_07.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_08                                                                     " & vbNewLine _
                 & "  ON  KBN_08.KBN_CD                   = F02_01.BIN_KB                                                               " & vbNewLine _
                 & " AND  KBN_08.KBN_GROUP_CD             = 'U037'                                                                     " & vbNewLine _
                 & " AND  KBN_08.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_NRS_BR  MNRS                                                                                " & vbNewLine _
                 & "ON    F02_01.NRS_BR_CD         = MNRS.NRS_BR_CD                                                                    " & vbNewLine _
                 & "WHERE F02_01.NRS_BR_CD                = @NRS_BR_CD                                                                 " & vbNewLine _
                 & "  AND F02_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 -- START --                                                            " & vbNewLine _
                 & "  AND F02_01.UNSO_TEHAI_KB            = '10'                                                                       " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                                                            " & vbNewLine _
                 & " AND F02_01.YUSO_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                 & " AND (M38_01.UNSOCO_CD = 'seiko' OR M38_01.UNSOCO_CD = 'kobay')                                                    " & vbNewLine _
                 & " AND F02_01.CUST_CD_L =  '30001'                                                                                   " & vbNewLine _
                 & " AND F02_01.CUST_CD_M <> '01'                                                                                      " & vbNewLine _
                 & " AND F02_01.ARR_PLAN_DATE >= @KIKAN_F_DATE                                                                         " & vbNewLine _
                 & " AND F02_01.ARR_PLAN_DATE <= @KIKAN_T_DATE                                                                         " & vbNewLine _
                 & " AND F02_01.TYUKEI_HAISO_FLG = '00'                                                                                " & vbNewLine _
                 & " AND F02_01.BIN_KB in ('02','03')                                                                                  " & vbNewLine _
                 & "GROUP BY                                                                                                           " & vbNewLine _
                 & "-- F02_01.OUTKA_PLAN_DATE                                                                                            " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                                                                                              " & vbNewLine _
                 & ",KBN_01.KBN_NM1                                                                                                    " & vbNewLine _
                 & ",MNRS.NRS_BR_NM                                                                                                   " & vbNewLine _
                 & ",M07_01.CUST_NM_L                                                                                                 " & vbNewLine _
                 & ",KBN_08.VALUE1                                                                                                    " & vbNewLine _
                 & ",KBN_07.VALUE1                                                                                                    " & vbNewLine _
                 & ",KBN_08.VALUE2                                                                                                    " & vbNewLine _
                 & "ORDER BY                                                                                                           " & vbNewLine _
                 & "-- F02_01.OUTKA_PLAN_DATE                                                                                            " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                                                                                              " & vbNewLine _
                 & ",KBN_01.KBN_NM1                                                                                                    " & vbNewLine

    ''' <summary>
    ''' 抽出区分01:DICG関係請求、シート:神奈川地区固定車
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_0103 As String =
                   "SELECT                                                                                                             " & vbNewLine _
                 & " ''                               AS RPT_ID                                                        " & vbNewLine _
                 & ",MNRS.NRS_BR_NM                          AS NRS_BR_NM                                                              " & vbNewLine _
                 & ",M07_01.CUST_NM_L                        AS CUST_NM_L                                                              " & vbNewLine _
                 & ",@KIKAN_F_DATE                           AS KIKAN_F_DATE                                                           " & vbNewLine _
                 & ",@KIKAN_T_DATE                           AS KIKAN_T_DATE                                                           " & vbNewLine _
                 & "--,F02_01.OUTKA_PLAN_DATE                  AS OUTKA_PLAN_DATE                                                        " & vbNewLine _
                 & ",F02_01.ARR_PLAN_DATE                    AS ARR_PLAN_DATE                                                          " & vbNewLine _
                 & ",KBN_01.KBN_NM1                          AS BIN                                                                    " & vbNewLine _
                 & ",3                                       as NUM                                                                    " & vbNewLine _
                 & ",'台'                                    as TANI                                                                   " & vbNewLine _
                 & ",17000                                   as TANKA                                                                  " & vbNewLine _
                 & ",51000                                   as TOTAL                                                                  " & vbNewLine _
                 & "FROM       $LM_TRN$..F_UNSO_L           F02_01                                                                     " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_01 ON  F02_01.TRIP_NO = F01_01.TRIP_NO                                   " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_02 ON  F02_01.TRIP_NO_SYUKA = F01_02.TRIP_NO                             " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_03 ON  F02_01.TRIP_NO_TYUKEI = F01_03.TRIP_NO                            " & vbNewLine _
                 & "LEFT  JOIN                                                                                                         " & vbNewLine _
                 & "(SELECT * FROM $LM_TRN$..F_UNSO_LL  UNION                                                                          " & vbNewLine _
                 & "-- SELECT * FROM LM_TRN_50..F_UNSO_LL  UNION                                                                         " & vbNewLine _
                 & " SELECT * FROM LM_TRN_30..F_UNSO_LL ) F01_04 ON  F02_01.TRIP_NO_HAIKA = F01_04.TRIP_NO                             " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT F03_01.NRS_BR_CD                                                                          " & vbNewLine _
                 & "                        ,F03_01.UNSO_NO_L                                                                          " & vbNewLine _
                 & "                        ,SUM(ISNULL(M08_01.STD_WT_KGS,'0')) AS STD_WT_KGS                                          " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_UNSO_M        F03_01                                                          " & vbNewLine _
                 & "               LEFT JOIN $LM_MST$..M_GOODS         M08_01                                                          " & vbNewLine _
                 & "                      ON F03_01.NRS_BR_CD        = M08_01.NRS_BR_CD                                                " & vbNewLine _
                 & "                     AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS                                             " & vbNewLine _
                 & "                     AND M08_01.SYS_DEL_FLG      = '0'                                                             " & vbNewLine _
                 & "                   WHERE F03_01.SYS_DEL_FLG      = '0'                                                             " & vbNewLine _
                 & "                GROUP BY F03_01.NRS_BR_CD                                                                          " & vbNewLine _
                 & "                        ,F03_01.UNSO_NO_L                                                                          " & vbNewLine _
                 & "            )                           F03_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F03_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F03_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "                        ,MIN(F04_01.SEIQ_GROUP_NO)        AS GROUP_NO                                              " & vbNewLine _
                 & "                        ,SUM(   F04_01.DECI_UNCHIN                                                                 " & vbNewLine _
                 & "                              + F04_01.DECI_CITY_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_WINT_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_RELY_EXTC                                                              " & vbNewLine _
                 & "                              + F04_01.DECI_TOLL                                                                   " & vbNewLine _
                 & "                              + F04_01.DECI_INSU                                                                   " & vbNewLine _
                 & "                             )                            AS UNCHIN                                                " & vbNewLine _
                 & "                        ,MAX(F04_01.SEIQ_KYORI)           AS KYORI                                                 " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_UNCHIN_TRS       F04_01                                                       " & vbNewLine _
                 & "                   WHERE SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                 & "                GROUP BY NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "            )                           F04_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F04_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F04_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "--START UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)                                           " & vbNewLine _
                 & "LEFT  JOIN (                                                                                                       " & vbNewLine _
                 & "                  SELECT NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "                        ,MIN(F05_01.SHIHARAI_GROUP_NO)    AS GROUP_NO                                              " & vbNewLine _
                 & "                        ,SUM(   F05_01.DECI_UNCHIN                                                                 " & vbNewLine _
                 & "                              + F05_01.DECI_CITY_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_WINT_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_RELY_EXTC                                                              " & vbNewLine _
                 & "                              + F05_01.DECI_TOLL                                                                   " & vbNewLine _
                 & "                              + F05_01.DECI_INSU                                                                   " & vbNewLine _
                 & "                             )                   AS SHIHARAI_UNCHIN                                                " & vbNewLine _
                 & "                        ,MAX(F05_01.SHIHARAI_KYORI)           AS KYORI                                             " & vbNewLine _
                 & "                        ,MAX(F05_01.SHIHARAI_FIXED_FLAG)  AS SHIHARAI_FIXED_FLAG                                   " & vbNewLine _
                 & "                    FROM $LM_TRN$..F_SHIHARAI_TRS       F05_01                                                     " & vbNewLine _
                 & "                   WHERE SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
                 & "                GROUP BY NRS_BR_CD                                                                                 " & vbNewLine _
                 & "                        ,UNSO_NO_L                                                                                 " & vbNewLine _
                 & "            )                           F05_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = F05_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = F05_01.UNSO_NO_L                                                           " & vbNewLine _
                 & "--END UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_CUST             M07_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M07_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M07_01.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_M                = M07_01.CUST_CD_M                                                           " & vbNewLine _
                 & " AND  M07_01.CUST_CD_S                = '00'                                                                       " & vbNewLine _
                 & " AND  M07_01.CUST_CD_SS               = '00'                                                                       " & vbNewLine _
                 & " AND  M07_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M38_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_CD                  = M38_01.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F02_01.UNSO_BR_CD               = M38_01.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_02                                                                     " & vbNewLine _
                 & "  ON  F01_01.NRS_BR_CD                = M38_02.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_01.UNSOCO_CD                = M38_02.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_01.UNSOCO_BR_CD             = M38_02.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_03                                                                     " & vbNewLine _
                 & "  ON  F01_02.NRS_BR_CD                = M38_03.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_02.UNSOCO_CD                = M38_03.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_02.UNSOCO_BR_CD             = M38_03.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_04                                                                     " & vbNewLine _
                 & "  ON  F01_03.NRS_BR_CD                = M38_04.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_03.UNSOCO_CD                = M38_04.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_03.UNSOCO_BR_CD             = M38_04.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_05                                                                     " & vbNewLine _
                 & "  ON  F01_04.NRS_BR_CD                = M38_05.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F01_04.UNSOCO_CD                = M38_05.UNSOCO_CD                                                           " & vbNewLine _
                 & " AND  F01_04.UNSOCO_BR_CD             = M38_05.UNSOCO_BR_CD                                                        " & vbNewLine _
                 & " AND  M38_05.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M10_01.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.ORIG_CD                  = M10_01.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_02.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.CUST_CD_L                = M10_02.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.DEST_CD                  = M10_02.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M10_02_JIS                                                                 " & vbNewLine _
                 & "  ON  M10_02.JIS                      = M10_02_JIS.JIS_CD                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_03                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_03.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  'ZZZZZ'                         = M10_03.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.ORIG_CD                  = M10_03.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_04                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M10_04.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  'ZZZZZ'                         = M10_04.CUST_CD_L                                                           " & vbNewLine _
                 & " AND  F02_01.DEST_CD                  = M10_04.DEST_CD                                                             " & vbNewLine _
                 & " AND  M10_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M10_04_JIS                                                                 " & vbNewLine _
                 & "  ON  M10_04.JIS                      = M10_04_JIS.JIS_CD                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.SYUKA_TYUKEI_CD          = M12_01.JIS_CD                                                              " & vbNewLine _
                 & " AND  M12_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.HAIKA_TYUKEI_CD          = M12_02.JIS_CD                                                              " & vbNewLine _
                 & " AND  M12_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_AREA             M36_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = M36_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.AREA_CD                  = M36_01.AREA_CD                                                             " & vbNewLine _
                 & " --要望番号1202 追加START(2012.07.02)                                                                              " & vbNewLine _
                 & " AND  F02_01.BIN_KB                   = M36_01.BIN_KB                                                              " & vbNewLine _
                 & " --要望番号1202 追加END  (2012.07.02)                                                                              " & vbNewLine _
                 & " AND  M10_02.JIS                      = M36_01.JIS_CD                                                              " & vbNewLine _
                 & " AND  M36_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_01                                                                     " & vbNewLine _
                 & "  ON  F01_01.CAR_KEY                  = M39_01.CAR_KEY                                                             " & vbNewLine _
                 & " AND  M39_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_02                                                                     " & vbNewLine _
                 & "  ON  F01_04.CAR_KEY                  = M39_02.CAR_KEY                                                             " & vbNewLine _
                 & " AND  M39_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_01                                                                     " & vbNewLine _
                 & "  ON  F01_01.DRIVER_CD                = M37_01.DRIVER_CD                                                           " & vbNewLine _
                 & " AND  M37_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_02                                                                     " & vbNewLine _
                 & "  ON  F01_04.DRIVER_CD                = M37_02.DRIVER_CD                                                           " & vbNewLine _
                 & " AND  M37_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.BIN_KB                   = KBN_01.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_01.KBN_GROUP_CD             = 'U001'                                                                     " & vbNewLine _
                 & " AND  KBN_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_02                                                                     " & vbNewLine _
                 & "  ON  F02_01.TARIFF_BUNRUI_KB         = KBN_02.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_02.KBN_GROUP_CD             = 'T015'                                                                     " & vbNewLine _
                 & " AND  KBN_02.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_03                                                                     " & vbNewLine _
                 & "  ON  F02_01.UNSO_ONDO_KB             = KBN_03.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_03.KBN_GROUP_CD             = 'U006'                                                                     " & vbNewLine _
                 & " AND  KBN_03.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_04                                                                     " & vbNewLine _
                 & "  ON  F02_01.MOTO_DATA_KB             = KBN_04.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_04.KBN_GROUP_CD             = 'M004'                                                                     " & vbNewLine _
                 & " AND  KBN_04.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_05                                                                     " & vbNewLine _
                 & "  ON  M39_01.CAR_TP_KB                = KBN_05.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_05.KBN_GROUP_CD             = 'S023'                                                                     " & vbNewLine _
                 & " AND  KBN_05.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_06                                                                     " & vbNewLine _
                 & "  ON  M39_02.CAR_TP_KB                = KBN_06.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_06.KBN_GROUP_CD             = 'S023'                                                                     " & vbNewLine _
                 & " AND  KBN_06.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..S_USER             S01_01                                                                     " & vbNewLine _
                 & "  ON  F02_01.SYS_ENT_USER             = S01_01.USER_CD                                                             " & vbNewLine _
                 & " AND  S01_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & " --要望番号2140 (2013.12.25) 追加START                                                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_TRN$..C_OUTKA_L        C01_01                                                                       " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = C01_01.NRS_BR_CD                                                           " & vbNewLine _
                 & " AND  F02_01.INOUTKA_NO_L             = C01_01.OUTKA_NO_L                                                          " & vbNewLine _
                 & " AND  F02_01.MOTO_DATA_KB             = '20'                                                                       " & vbNewLine _
                 & " AND  C01_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                                                            " & vbNewLine _
                 & " --要望番号2063 (2015.05.27) 追加START                                                                             " & vbNewLine _
                 & "LEFT  JOIN $LM_TRN$..H_TEHAIINFO_TBL    H_TEHAI                                                                    " & vbNewLine _
                 & "  ON  F02_01.NRS_BR_CD                = H_TEHAI.NRS_BR_CD                                                          " & vbNewLine _
                 & " AND  F02_01.TRIP_NO                  = H_TEHAI.TRIP_NO                                                            " & vbNewLine _
                 & " AND  F02_01.UNSO_NO_L                = H_TEHAI.UNSO_NO_L                                                          " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_27                                                                     " & vbNewLine _
                 & "  ON  H_TEHAI.TEHAI_SYUBETSU          = KBN_27.KBN_CD                                                              " & vbNewLine _
                 & " AND  KBN_27.KBN_GROUP_CD             = 'U027'                                                                     " & vbNewLine _
                 & " AND  KBN_27.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & " --要望番号2140 (2013.12.25) 追加END                                                                               " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_NRS_BR  MNRS                                                                                " & vbNewLine _
                 & "ON    F02_01.NRS_BR_CD         = MNRS.NRS_BR_CD                                                                    " & vbNewLine _
                 & "WHERE F02_01.NRS_BR_CD                = @NRS_BR_CD                                                                 " & vbNewLine _
                 & "  AND F02_01.SYS_DEL_FLG              = '0'                                                                        " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 -- START --                                                            " & vbNewLine _
                 & "  AND F02_01.UNSO_TEHAI_KB            = '10'                                                                       " & vbNewLine _
                 & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                                                            " & vbNewLine _
                 & " AND F02_01.YUSO_BR_CD = @NRS_BR_CD                                                                                " & vbNewLine _
                 & " AND (M38_01.UNSOCO_CD = 'seiko' OR M38_01.UNSOCO_CD = 'kobay')                                                    " & vbNewLine _
                 & " AND F02_01.CUST_CD_L =  '30001'                                                                                   " & vbNewLine _
                 & " AND F02_01.CUST_CD_M <> '01'                                                                                      " & vbNewLine _
                 & " AND F02_01.ARR_PLAN_DATE >= @KIKAN_F_DATE                                                                         " & vbNewLine _
                 & " AND F02_01.ARR_PLAN_DATE <= @KIKAN_T_DATE                                                                         " & vbNewLine _
                 & " AND F02_01.TYUKEI_HAISO_FLG = '00'                                                                                " & vbNewLine _
                 & " AND F02_01.BIN_KB = '02'                                                                                          " & vbNewLine _
                 & "GROUP BY                                                                                                           " & vbNewLine _
                 & " --F02_01.OUTKA_PLAN_DATE                                                                                            " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                                                                                              " & vbNewLine _
                 & ",KBN_01.KBN_NM1                                                                                                    " & vbNewLine _
                 & ",F02_01.BIN_KB                                                                                                     " & vbNewLine _
                 & ",MNRS.NRS_BR_NM                                                                                                   " & vbNewLine _
                 & ",M07_01.CUST_NM_L                                                                                                  " & vbNewLine _
                 & "ORDER BY                                                                                                           " & vbNewLine _
                 & "-- F02_01.OUTKA_PLAN_DATE                                                                                            " & vbNewLine _
                 & " F02_01.ARR_PLAN_DATE                                                                                              " & vbNewLine _
                 & ",KBN_01.KBN_NM1                                                                                                    " & vbNewLine

    ''' <summary>
    ''' 抽出区分01:DICG関係請求、シート:栃木地区最低保証
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_0104 As String = _
                   " SELECT                                                                                                            " & vbNewLine _
                 & " ''                               AS RPT_ID                                                        " & vbNewLine _
                 & ",MNRS.NRS_BR_NM                          AS NRS_BR_NM                                                              " & vbNewLine _
                 & ",CST.CUST_NM_L                           AS CUST_NM_L                                                              " & vbNewLine _
                 & ",@KIKAN_F_DATE                           AS KIKAN_F_DATE                                                           " & vbNewLine _
                 & ",@KIKAN_T_DATE                           AS KIKAN_T_DATE                                                           " & vbNewLine _
                 & "      ,USL.OUTKA_PLAN_DATE                AS OUTKA_PLAN_DATE  --1  出荷予定日                                      " & vbNewLine _
                 & "      ,USL.ARR_PLAN_DATE                  AS ARR_PLAN_DATE    --2  納入予定日                                      " & vbNewLine _
                 & "      ,CASE WHEN USL.MOTO_DATA_KB = '10' THEN DST_ORIG.DEST_NM                                                     " & vbNewLine _
                 & "            WHEN USL.MOTO_DATA_KB = '20' AND ISNULL(EDIL2.DEST_NM,'') <> '' THEN EDIL2.DEST_NM                     " & vbNewLine _
                 & "            WHEN USL.MOTO_DATA_KB = '40' AND ISNULL(EDIL.DEST_NM,'') <> '' THEN EDIL.DEST_NM                       " & vbNewLine _
                 & "            ELSE DST.DEST_NM  END               AS DEST_NM    --3  届先名                                          " & vbNewLine _
                 & "      ,CASE WHEN USL.MOTO_DATA_KB = '10' THEN DST_ORIG.AD_1                                                        " & vbNewLine _
                 & "            WHEN USL.MOTO_DATA_KB = '20' AND ISNULL(EDIL2.DEST_NM,'') <> '' THEN EDIL2.DEST_AD_1 + EDIL2.DEST_AD_2 " & vbNewLine _
                 & "            WHEN USL.MOTO_DATA_KB = '40' AND ISNULL(EDIL.DEST_NM,'') <> '' THEN EDIL.DEST_AD_1 + EDIL.DEST_AD_2    " & vbNewLine _
                 & "            ELSE DST.AD_1 END AS DEST_AD_1                    --4  住所1                                           " & vbNewLine _
                 & "      ,ISNULL(CASE WHEN TRS.SEIQ_GROUP_NO = '' THEN USL.UNSO_PKG_NB ELSE                                           " & vbNewLine _
                 & "            MATOME.UNSO_PKG_NB END,0)     AS UNSO_PKG_NB      --5  運送梱包個数                                    " & vbNewLine _
                 & "      ,TRS.DECI_WT                        AS SEIQ_WT          --6  請求適用重量                                    " & vbNewLine _
                 & "      ,TRS.DECI_KYORI                     AS SEIQ_KYORI       --7  請求適用距離                                    " & vbNewLine _
                 & "      ,3500 AS MINIMUM                                        --8  最低保障                                        " & vbNewLine _
                 & "      ,(TRS.DECI_UNCHIN +    --確定請求運賃                                                                        " & vbNewLine _
                 & "       TRS.DECI_CITY_EXTC + --確定請求都市割増                                                                     " & vbNewLine _
                 & "       TRS.DECI_WINT_EXTC + --確定請求冬期割増                                                                     " & vbNewLine _
                 & "       TRS.DECI_RELY_EXTC + --確定請求中継料                                                                       " & vbNewLine _
                 & "       TRS.DECI_TOLL +      --確定請求通行料                                                                       " & vbNewLine _
                 & "       TRS.DECI_INSU)       --確定請求保険料                                                                       " & vbNewLine _
                 & "                                         AS DECI_UNCHIN      --9  請求運賃                                         " & vbNewLine _
                 & "      ,3500 -                                                                                                      " & vbNewLine _
                 & "      (TRS.DECI_UNCHIN +    --確定請求運賃                                                                         " & vbNewLine _
                 & "       TRS.DECI_CITY_EXTC + --確定請求都市割増                                                                     " & vbNewLine _
                 & "       TRS.DECI_WINT_EXTC + --確定請求冬期割増                                                                     " & vbNewLine _
                 & "       TRS.DECI_RELY_EXTC + --確定請求中継料                                                                       " & vbNewLine _
                 & "       TRS.DECI_TOLL +      --確定請求通行料                                                                       " & vbNewLine _
                 & "       TRS.DECI_INSU) AS HOTENGAKU                           --10  補填額                                          " & vbNewLine _
                 & "      ,CST.CUST_NM_M AS CUST_NM_M                            --11                                                  " & vbNewLine _
                 & "      ,USC.UNSOCO_NM                      AS UNSOCO_NM                                                             " & vbNewLine _
                 & "      ,USL.CUST_CD_L                                                                                               " & vbNewLine _
                 & "      ,USL.CUST_CD_M                                                                                               " & vbNewLine _
                 & " FROM $LM_TRN$..F_UNCHIN_TRS TRS         --運賃マスタ                                                              " & vbNewLine _
                 & " LEFT JOIN --まとめ重量・個数取得                                                                                  " & vbNewLine _
                 & "   (SELECT                                                                                                         " & vbNewLine _
                 & "      SEIQ_GROUP_NO                                                                                                " & vbNewLine _
                 & "     ,SUM(DECI_WT) AS DECI_WT                                                                                      " & vbNewLine _
                 & "     ,SUM(USL.UNSO_PKG_NB ) AS UNSO_PKG_NB                                                                         " & vbNewLine _
                 & "    FROM $LM_TRN$..F_UNCHIN_TRS TRS                                                                                " & vbNewLine _
                 & "    LEFT JOIN $LM_TRN$..F_UNSO_L USL                                                                               " & vbNewLine _
                 & "        ON  USL.UNSO_NO_L = TRS.UNSO_NO_L                                                                          " & vbNewLine _
                 & "       AND USL.SYS_DEL_FLG    = '0'                                                                                " & vbNewLine _
                 & "    WHERE                                                                                                          " & vbNewLine _
                 & "          TRS.NRS_BR_CD = @NRS_BR_CD                                                                               " & vbNewLine _
                 & "      AND @KIKAN_F_DATE     <= USL.ARR_PLAN_DATE                                                                   " & vbNewLine _
                 & "      AND USL.ARR_PLAN_DATE <= @KIKAN_T_DATE                                                                       " & vbNewLine _
                 & "      AND TRS.SYS_DEL_FLG    = '0'                                                                                 " & vbNewLine _
                 & "      AND TRS.SEIQ_GROUP_NO <> ''                                                                                  " & vbNewLine _
                 & "    GROUP BY                                                                                                       " & vbNewLine _
                 & "      SEIQ_GROUP_NO                                                                                                " & vbNewLine _
                 & "   ) AS MATOME                                                                                                     " & vbNewLine _
                 & " ON MATOME.SEIQ_GROUP_NO = TRS.UNSO_NO_L                                                                           " & vbNewLine _
                 & " LEFT JOIN $LM_TRN$..F_UNSO_L USL        --運送L                                                                   " & vbNewLine _
                 & "      ON  USL.UNSO_NO_L = TRS.UNSO_NO_L                                                                            " & vbNewLine _
                 & "      AND USL.SYS_DEL_FLG    = '0'                                                                                 " & vbNewLine _
                 & " --運送LLとJOIN                                                                                                    " & vbNewLine _
                 & " LEFT OUTER JOIN                                                                                                   " & vbNewLine _
                 & "      $LM_TRN$..F_UNSO_LL AS UNSO_LL1                                                                              " & vbNewLine _
                 & "   ON USL.TRIP_NO = UNSO_LL1.TRIP_NO                                                                               " & vbNewLine _
                 & " LEFT OUTER JOIN                                                                                                   " & vbNewLine _
                 & "      $LM_MST$..M_DRIVER AS DRI1                                                                                   " & vbNewLine _
                 & "   ON UNSO_LL1.DRIVER_CD = DRI1.DRIVER_CD                                                                          " & vbNewLine _
                 & "-- LEFT OUTER JOIN                                                                                                   " & vbNewLine _
                 & "--      LM_TRN_50..F_UNSO_LL AS UNSO_LL2                                                                             " & vbNewLine _
                 & "--   ON USL.TRIP_NO = UNSO_LL2.TRIP_NO                                                                               " & vbNewLine _
                 & "-- LEFT OUTER JOIN                                                                                                   " & vbNewLine _
                 & "--      $LM_MST$..M_DRIVER AS DRI2                                                                                   " & vbNewLine _
                 & "--   ON UNSO_LL2.DRIVER_CD = DRI2.DRIVER_CD                                                                          " & vbNewLine _
                 & " LEFT OUTER JOIN                                                                                                   " & vbNewLine _
                 & "      $LM_TRN$..F_UNSO_LL AS UNSO_LL3                                                                              " & vbNewLine _
                 & "   ON USL.TRIP_NO = UNSO_LL3.TRIP_NO                                                                               " & vbNewLine _
                 & " LEFT OUTER JOIN                                                                                                   " & vbNewLine _
                 & "      $LM_MST$..M_DRIVER AS DRI3                                                                                   " & vbNewLine _
                 & "   ON UNSO_LL3.DRIVER_CD = DRI3.DRIVER_CD                                                                          " & vbNewLine _
                 & " LEFT JOIN                                                                                                         " & vbNewLine _
                 & "    (SELECT NRS_BR_CD,CUST_CD_L,SUBSTRING(FREE_C30,1,2) as UNSO_KBN,SUBSTRING(FREE_C30,4,9) as UNSO_NO_L           " & vbNewLine _
                 & "     ,DEST_NM,DEST_AD_1,DEST_AD_2                                                                                  " & vbNewLine _
                 & "     FROM $LM_TRN$..H_OUTKAEDI_L                                                                                   " & vbNewLine _
                 & "     WHERE                                                                                                         " & vbNewLine _
                 & "          NRS_BR_CD = @NRS_BR_CD                                                                                   " & vbNewLine _
                 & "       AND SUBSTRING(FREE_C30,4,9) <>'' AND SUBSTRING(FREE_C30,5,8) <>'00000000'                                   " & vbNewLine _
                 & "       AND  SUBSTRING(FREE_C30,1,2) <>'' AND SYS_DEL_FLG  = '0'                                                    " & vbNewLine _
                 & "     )                                                                                                             " & vbNewLine _
                 & "       EDIL  --EDI出荷L(運送データ)                                                                                " & vbNewLine _
                 & "   ON  EDIL.UNSO_NO_L  = USL.UNSO_NO_L                                                                             " & vbNewLine _
                 & "  AND USL.MOTO_DATA_KB = '40'                                                                                      " & vbNewLine _
                 & " LEFT JOIN                                                                                                         " & vbNewLine _
                 & "    (SELECT NRS_BR_CD,CUST_CD_L,OUTKA_CTL_NO                                                                       " & vbNewLine _
                 & "     ,DEST_NM,DEST_AD_1,DEST_AD_2 FROM $LM_TRN$..H_OUTKAEDI_L                                                      " & vbNewLine _
                 & "     WHERE                                                                                                         " & vbNewLine _
                 & "          NRS_BR_CD = @NRS_BR_CD                                                                                   " & vbNewLine _
                 & "      AND OUTKA_CTL_NO <> ''                                                                                       " & vbNewLine _
                 & "      AND SYS_DEL_FLG  = '0'                                                                                       " & vbNewLine _
                 & "    )                                                                                                              " & vbNewLine _
                 & "      EDIL2  --EDI出荷L(出荷データ)                                                                                " & vbNewLine _
                 & "   ON EDIL2.NRS_BR_CD = USL.NRS_BR_CD AND EDIL2.OUTKA_CTL_NO  = USL.INOUTKA_NO_L                                   " & vbNewLine _
                 & "  AND USL.MOTO_DATA_KB = '20'                                                                                      " & vbNewLine _
                 & " LEFT JOIN $LM_MST$..M_UNSOCO USC        --運送会社マスタ                                                          " & vbNewLine _
                 & "      ON  USC.NRS_BR_CD    = TRS.NRS_BR_CD                                                                         " & vbNewLine _
                 & "      AND USC.UNSOCO_CD    = USL.UNSO_CD                                                                           " & vbNewLine _
                 & "      AND USC.UNSOCO_BR_CD = USL.UNSO_BR_CD                                                                        " & vbNewLine _
                 & " LEFT JOIN $LM_MST$..M_CUST CST          --荷主マスタ                                                              " & vbNewLine _
                 & "      ON  CST.NRS_BR_CD   = TRS.NRS_BR_CD                                                                          " & vbNewLine _
                 & "      AND CST.CUST_CD_L   = TRS.CUST_CD_L                                                                          " & vbNewLine _
                 & "      AND CST.CUST_CD_M   = TRS.CUST_CD_M                                                                          " & vbNewLine _
                 & "      AND CST.CUST_CD_S   = TRS.CUST_CD_S                                                                          " & vbNewLine _
                 & "      AND CST.CUST_CD_SS  = TRS.CUST_CD_SS                                                                         " & vbNewLine _
                 & " LEFT JOIN $LM_MST$..M_DEST  DST         --届先マスタ                                                              " & vbNewLine _
                 & "      ON  DST.NRS_BR_CD = TRS.NRS_BR_CD                                                                            " & vbNewLine _
                 & "      AND DST.CUST_CD_L = TRS.CUST_CD_L                                                                            " & vbNewLine _
                 & "      AND DST.DEST_CD   = USL.DEST_CD                                                                              " & vbNewLine _
                 & " LEFT JOIN   $LM_MST$..M_DEST  DST_ORIG   --届先マスタ(出発地)                                                     " & vbNewLine _
                 & "      ON  DST_ORIG.NRS_BR_CD = TRS.NRS_BR_CD                                                                       " & vbNewLine _
                 & "      AND DST_ORIG.CUST_CD_L = TRS.CUST_CD_L                                                                       " & vbNewLine _
                 & "      AND DST_ORIG.DEST_CD   = USL.ORIG_CD                                                                         " & vbNewLine _
                 & "LEFT  JOIN $LM_MST$..M_NRS_BR  MNRS                                                                                " & vbNewLine _
                 & "ON    TRS.NRS_BR_CD         = MNRS.NRS_BR_CD                                                                    " & vbNewLine _
                 & " WHERE                                                                                                             " & vbNewLine _
                 & "     TRS.NRS_BR_CD = @NRS_BR_CD                                                                                    " & vbNewLine _
                 & " AND ( @KIKAN_F_DATE     <= USL.ARR_PLAN_DATE                                                                      " & vbNewLine _
                 & " AND   USL.ARR_PLAN_DATE <= @KIKAN_T_DATE )                                                                        " & vbNewLine _
                 & " AND TRS.SEIQ_FIXED_FLAG = '01'                                                                                    " & vbNewLine _
                 & " AND TRS.SYS_DEL_FLG    = '0'                                                                                      " & vbNewLine _
                 & " AND ((TRS.DECI_UNCHIN = 0 AND TRS.SEIQ_GROUP_NO='')                                                               " & vbNewLine _
                 & "      OR TRS.DECI_UNCHIN <> 0)--2014.01.20 まとめ０円が表示するように修正                                          " & vbNewLine _
                 & " AND ((TRS.DECI_UNCHIN = 0 AND TRS.SEIQ_GROUP_NO='')                                                               " & vbNewLine _
                 & "      OR (TRS.DECI_UNCHIN = 0 AND MATOME.UNSO_PKG_NB <> 0)                                                         " & vbNewLine _
                 & "      OR TRS.DECI_UNCHIN <> 0)                                                                                     " & vbNewLine _
                 & " AND (TRS.DECI_UNCHIN +    --確定請求運賃                                                                          " & vbNewLine _
                 & "      TRS.DECI_CITY_EXTC + --確定請求都市割増                                                                      " & vbNewLine _
                 & "      TRS.DECI_WINT_EXTC + --確定請求冬期割増                                                                      " & vbNewLine _
                 & "      TRS.DECI_RELY_EXTC + --確定請求中継料                                                                        " & vbNewLine _
                 & "      TRS.DECI_TOLL +      --確定請求通行料                                                                        " & vbNewLine _
                 & "      TRS.DECI_INSU) < 3500                                                                                        " & vbNewLine _
                 & " AND CASE WHEN USL.MOTO_DATA_KB = '10' THEN DST_ORIG.AD_1                                                          " & vbNewLine _
                 & "          WHEN USL.MOTO_DATA_KB = '20' AND ISNULL(EDIL2.DEST_NM,'') <> '' THEN EDIL2.DEST_AD_1 + EDIL2.DEST_AD_2   " & vbNewLine _
                 & "          WHEN USL.MOTO_DATA_KB = '40' AND ISNULL(EDIL.DEST_NM,'') <> '' THEN EDIL.DEST_AD_1 + EDIL.DEST_AD_2      " & vbNewLine _
                 & "          ELSE DST.AD_1 END                --4  住所1                                                              " & vbNewLine _
                 & "        LIKE '%栃木%'                                                                                              " & vbNewLine _
                 & " AND (USL.CUST_CD_L IN( '30001','30021','30002')                                                                   " & vbNewLine _
                 & "      or USL.CUST_CD_L+USL.CUST_CD_M IN('3001000','3001003')                                                       " & vbNewLine _
                 & "      )                                                                                                            " & vbNewLine _
                 & "--DEL 20019/096/28005720  AND USC.UNSOCO_CD = 'EX'                                                                                          " & vbNewLine _
                 & " ORDER BY                                                                                                          " & vbNewLine _
                 & " USL.CUST_CD_L,USL.OUTKA_PLAN_DATE                                                                                               " & vbNewLine


    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	      MR3.NRS_BR_CD                            AS NRS_BR_CD          " & vbNewLine _
                                            & "      ,MR3.PTN_ID                               AS PTN_ID             " & vbNewLine _
                                            & "      ,MR3.PTN_CD                               AS PTN_CD             " & vbNewLine _
                                            & "      ,MR3.RPT_ID                               AS RPT_ID             " & vbNewLine
    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 浮間横持運賃TBL(I_YOKO_UNCHIN_TRS)
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_MST$..M_RPT MR3                            " & vbNewLine 

    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                     " & vbNewLine _
                                           & "     MR3.NRS_BR_CD       = @NRS_BR_CD     " & vbNewLine _
                                           & " AND MR3.PTN_ID          = @PTN_ID         " & vbNewLine _
                                           & " AND MR3.STANDARD_FLAG   = '01'            " & vbNewLine _
                                           & " AND MR3.SYS_DEL_FLG      = '0'            " & vbNewLine
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

#Region "SQLメイン処理"

    ''' <summary>
    ''' 検索処理(抽出区分01:DICG関係請求、シート:神奈川配送分横持)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData0101(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI480IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI480DAC.SQL_SELECT_DATA_0101)      'SQL構築

        'パラメータ設定
        Call Me.SetSelectData0101Parameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI480DAC", "SelectData0101", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("BIN", "BIN")
        map.Add("SUM_WT", "SUM_WT")
        map.Add("WT_TANI", "WT_TANI")
        map.Add("TANKA", "TANKA")
        map.Add("CHOKA", "CHOKA")
        map.Add("KOSOKU_TSUKORYO", "KOSOKU_TSUKORYO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI480OUT_0101")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(抽出区分01:DICG関係請求、シート:神奈川配送分横持(聖亘提出用))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData0102(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI480IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI480DAC.SQL_SELECT_DATA_0102)      'SQL構築

        'パラメータ設定
        Call Me.SetSelectData0102Parameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI480DAC", "SelectData0102", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング KIKAN_F_DATE CUST_NM_L
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("KIKAN_F_DATE", "KIKAN_F_DATE")
        map.Add("KIKAN_T_DATE", "KIKAN_T_DATE")
        'map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("BIN", "BIN")
        map.Add("SUM_WT", "SUM_WT")
        map.Add("WT_TANI", "WT_TANI")
        map.Add("T3MADE", "T3MADE")
        map.Add("EXCESS_CHARGE", "EXCESS_CHARGE")
        map.Add("TOTAL_FARE", "TOTAL_FARE")
        map.Add("HIGH_SPEED", "HIGH_SPEED")
        map.Add("T3_OVER_CHARGE", "T3_OVER_CHARGE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI480OUT_0102")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(抽出区分01:DICG関係請求、シート:神奈川地区固定車)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData0103(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI480IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI480DAC.SQL_SELECT_DATA_0103)      'SQL構築

        'パラメータ設定
        Call Me.SetSelectData0103Parameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI480DAC", "SelectData0103", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("KIKAN_F_DATE", "KIKAN_F_DATE")
        map.Add("KIKAN_T_DATE", "KIKAN_T_DATE")
        'map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("BIN", "BIN")
        map.Add("NUM", "NUM")
        map.Add("TANI", "TANI")
        map.Add("TANKA", "TANKA")
        map.Add("TOTAL", "TOTAL")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI480OUT_0103")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(抽出区分01:DICG関係請求、シート:栃木地区最低保証)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData0104(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI480IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI480DAC.SQL_SELECT_DATA_0104)      'SQL構築

        'パラメータ設定
        Call Me.SetSelectData0104Parameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI480DAC", "SelectData0104", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("KIKAN_F_DATE", "KIKAN_F_DATE")
        map.Add("KIKAN_T_DATE", "KIKAN_T_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("MINIMUM", "MINIMUM")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("HOTENGAKU", "HOTENGAKU")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI480OUT_0104")

        reader.Close()

        Return ds

    End Function


    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI480IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI480DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMI480DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        Me._StrSql.Append(LMI480DAC.SQL_MPrt_WHERE)
        Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI480DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function
#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(抽出区分01:DICG関係請求、シート:神奈川配送分横持)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectData0101Parameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_F_DATE", .Item("KIKAN_F_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_T_DATE", .Item("KIKAN_T_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出区分01:DICG関係請求、シート:神奈川配送分横持(聖亘提出用))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectData0102Parameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_F_DATE", .Item("KIKAN_F_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_T_DATE", .Item("KIKAN_T_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出区分01:DICG関係請求、シート:神奈川地区固定車)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectData0103Parameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_F_DATE", .Item("KIKAN_F_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_T_DATE", .Item("KIKAN_T_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出区分01:DICG関係請求、シート:栃木地区最低保証)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectData0104Parameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_F_DATE", .Item("KIKAN_F_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIKAN_T_DATE", .Item("KIKAN_T_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

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
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'Me._StrSql.Append(" WHERE ")
        'Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" MR3.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            '印刷ID
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", Me._Row.Item("PTN_ID").ToString(), DBDataType.CHAR))
            'whereStr = .Item("PTN_ID").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append("AND  MR3.PTN_ID = @PTN_ID")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", whereStr, DBDataType.CHAR))
            'End If


        End With

    End Sub
#End Region

#End Region

End Class
