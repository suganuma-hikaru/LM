' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF581DAC : 配車表(群馬専用)
'  作  成  者       :  kurihara
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF581DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF581DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"
    ''' <summary>
    ''' 日付絞込(納入日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_NONYU As String = "01"

    ''' <summary>
    ''' 日付絞込(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_SHUKKA As String = "02"

    ''' <summary>
    ''' 日付絞込(運行日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_LL As String = "03"

    ''' <summary>
    ''' 日付絞込(作成日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_ENT As String = "04"

    ''' <summary>
    ''' 中継配送有
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TYUKEI_HAISO_FLG_ON As String = "01"

#End Region


#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "        F02_01.NRS_BR_CD                                 AS NRS_BR_CD " & vbNewLine _
                                            & "	     , 'BM'                                             AS PTN_ID    " & vbNewLine _
                                            & "	     , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "	       ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "	     , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

#End Region

#Region "SELECT句"
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                    " & vbNewLine _
                                            & "	 CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                         " & vbNewLine _
                                            & "  ELSE MR3.RPT_ID END                                       AS RPT_ID      " & vbNewLine _
                                            & " ,F02_01.NRS_BR_CD                                          AS NRS_BR_CD   " & vbNewLine _
                                            & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                                   " & vbNewLine _
                                            & "                               THEN F01_01.TRIP_DATE                       " & vbNewLine _
                                            & "                               ELSE F01_04.TRIP_DATE                       " & vbNewLine _
                                            & "  END                                                       AS TRIP_DATE   " & vbNewLine _
                                            & " ,F02_01.TRIP_NO                                            AS TRIP_NO     " & vbNewLine _
                                            & " ,F01_01.UNSOCO_CD                                          AS UNSOCO_CD   " & vbNewLine _
                                            & " ,F01_01.JSHA_KB                                            AS JSHA_KB     " & vbNewLine _
                                            & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                                   " & vbNewLine _
                                            & "                               THEN M39_01.CAR_NO                          " & vbNewLine _
                                            & "                               ELSE M39_02.CAR_NO                          " & vbNewLine _
                                            & "  END                                                       AS CAR_NO      " & vbNewLine _
                                            & " ,CASE WHEN F02_01.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                            & "                   THEN ISNULL(M10_01.DEST_NM,M10_03.DEST_NM)              " & vbNewLine _
                                            & "       WHEN F02_01.MOTO_DATA_KB = '20'                                     " & vbNewLine _
                                            & "                   THEN ISNULL(M10_02.DEST_NM,M10_04.DEST_NM)              " & vbNewLine _
                                            & "  ELSE ISNULL(M10_02.DEST_NM,M10_04.DEST_NM)  END           AS DEST_NM     " & vbNewLine _
                                            & " ,SUM(F02_01.UNSO_WT)                                       AS UNSO_WT     " & vbNewLine _
                                            & " ,M38_02.UNSOCO_NM                                          AS UNSOCO_NM   " & vbNewLine _
                                            & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                                   " & vbNewLine _
                                            & "                               THEN M37_01.DRIVER_NM                       " & vbNewLine _
                                            & "                               ELSE M37_02.DRIVER_NM                       " & vbNewLine _
                                            & "  END                                                       AS DRIVER_NM   " & vbNewLine _
                                            & " ,ISNULL(M39_01.VCLE_KB,M39_02.VCLE_KB)                     AS VCLE_KB     " & vbNewLine _
                                            & " ,M01_01.NRS_BR_NM                                          AS NRS_BR_NM   " & vbNewLine _
                                            & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                                   " & vbNewLine _
                                            & "                               THEN KBN_07.KBN_NM1                         " & vbNewLine _
                                            & "                               ELSE KBN_08.KBN_NM1                         " & vbNewLine _
                                            & "  END                                                       AS SYARYO_NM   " & vbNewLine _
                                            & " ,CASE WHEN F02_01.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                            & "                   THEN ISNULL(M10_01.AD_1,M10_03.AD_1)                    " & vbNewLine _
                                            & "       WHEN F02_01.MOTO_DATA_KB = '20'                                     " & vbNewLine _
                                            & "                   THEN ISNULL(M10_02.AD_1,M10_04.AD_1)                    " & vbNewLine _
                                            & "  ELSE ISNULL(M10_02.AD_1,M10_04.AD_1)  END                 AS DEST_AD_1   " & vbNewLine _
                                            & " ,CASE WHEN F02_01.MOTO_DATA_KB = '10'                                     " & vbNewLine _
                                            & "                   THEN ISNULL(M10_01.AD_2,M10_03.AD_2)                    " & vbNewLine _
                                            & "       WHEN F02_01.MOTO_DATA_KB = '20'                                     " & vbNewLine _
                                            & "                   THEN ISNULL(M10_02.AD_2,M10_04.AD_2)                    " & vbNewLine _
                                            & "  ELSE ISNULL(M10_02.AD_2,M10_04.AD_2)  END                 AS DEST_AD_2   " & vbNewLine _
                                            & " ,CASE WHEN F01_01.JSHA_KB = '01'                                          " & vbNewLine _
                                            & "                   THEN '自社'                                             " & vbNewLine _
                                            & "       WHEN F01_01.JSHA_KB = '02'                                          " & vbNewLine _
                                            & "                   THEN  CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'            " & vbNewLine _
                                            & "                                     THEN KBN_07.KBN_NM1                   " & vbNewLine _
                                            & "                                     ELSE KBN_08.KBN_NM1                   " & vbNewLine _
                                            & "                         END                                               " & vbNewLine _
                                            & "  ELSE '？？？'  END                                        AS PAGE_KBN_NM " & vbNewLine _
                                            & " ,M07_01.CUST_NM_L                                          AS CUST_NM_L   " & vbNewLine







#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "FROM       $LM_TRN$..F_UNSO_L           F02_01                       " & vbNewLine _
                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = F01_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.TRIP_NO                  = F01_01.TRIP_NO                   " & vbNewLine _
                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_02                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = F01_02.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.TRIP_NO_SYUKA            = F01_02.TRIP_NO                   " & vbNewLine _
                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_03                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = F01_03.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.TRIP_NO_TYUKEI           = F01_03.TRIP_NO                   " & vbNewLine _
                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_04                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = F01_04.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.TRIP_NO_HAIKA            = F01_04.TRIP_NO                   " & vbNewLine _
                                 & "LEFT  JOIN (                                                             " & vbNewLine _
                                 & "                  SELECT F03_01.NRS_BR_CD                                " & vbNewLine _
                                 & "                        ,F03_01.UNSO_NO_L                                " & vbNewLine _
                                 & "                        ,SUM(ISNULL(M08_01.STD_WT_KGS,'0')) AS STD_WT_KGS" & vbNewLine _
                                 & "                    FROM $LM_TRN$..F_UNSO_M        F03_01                " & vbNewLine _
                                 & "               LEFT JOIN $LM_MST$..M_GOODS         M08_01                " & vbNewLine _
                                 & "                      ON F03_01.NRS_BR_CD        = M08_01.NRS_BR_CD      " & vbNewLine _
                                 & "                     AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS   " & vbNewLine _
                                 & "                     AND M08_01.SYS_DEL_FLG      = '0'                   " & vbNewLine _
                                 & "                   WHERE F03_01.SYS_DEL_FLG      = '0'                   " & vbNewLine _
                                 & "                GROUP BY F03_01.NRS_BR_CD                                " & vbNewLine _
                                 & "                        ,F03_01.UNSO_NO_L                                " & vbNewLine _
                                 & "            )                           F03_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = F03_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.UNSO_NO_L                = F03_01.UNSO_NO_L                 " & vbNewLine _
                                 & "LEFT  JOIN (                                                             " & vbNewLine _
                                 & "                  SELECT NRS_BR_CD                                       " & vbNewLine _
                                 & "                        ,UNSO_NO_L                                       " & vbNewLine _
                                 & "                        ,MIN(F04_01.SEIQ_GROUP_NO)        AS GROUP_NO    " & vbNewLine _
                                 & "                        ,SUM(   F04_01.DECI_UNCHIN                       " & vbNewLine _
                                 & "                              + F04_01.DECI_CITY_EXTC                    " & vbNewLine _
                                 & "                              + F04_01.DECI_WINT_EXTC                    " & vbNewLine _
                                 & "                              + F04_01.DECI_RELY_EXTC                    " & vbNewLine _
                                 & "                              + F04_01.DECI_TOLL                         " & vbNewLine _
                                 & "                              + F04_01.DECI_INSU                         " & vbNewLine _
                                 & "                             )                            AS UNCHIN      " & vbNewLine _
                                 & "                        ,MAX(F04_01.SEIQ_KYORI)           AS KYORI       " & vbNewLine _
                                 & "                    FROM $LM_TRN$..F_UNCHIN_TRS       F04_01             " & vbNewLine _
                                 & "                   WHERE SYS_DEL_FLG = '0'                               " & vbNewLine _
                                 & "                GROUP BY NRS_BR_CD                                       " & vbNewLine _
                                 & "                        ,UNSO_NO_L                                       " & vbNewLine _
                                 & "            )                           F04_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = F04_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.UNSO_NO_L                = F04_01.UNSO_NO_L                 " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_NRS_BR           M01_01                           " & vbNewLine _
                                 & "   ON F02_01.NRS_BR_CD                = M01_01.NRS_BR_CD                 " & vbNewLine _
                                 & "  AND M01_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_CUST             M07_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M07_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.CUST_CD_L                = M07_01.CUST_CD_L                 " & vbNewLine _
                                 & " AND  F02_01.CUST_CD_M                = M07_01.CUST_CD_M                 " & vbNewLine _
                                 & " AND  M07_01.CUST_CD_S                = '00'                             " & vbNewLine _
                                 & " AND  M07_01.CUST_CD_SS               = '00'                             " & vbNewLine _
                                 & " AND  M07_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M38_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.UNSO_CD                  = M38_01.UNSOCO_CD                 " & vbNewLine _
                                 & " AND  F02_01.UNSO_BR_CD               = M38_01.UNSOCO_BR_CD              " & vbNewLine _
                                 & " AND  M38_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_02                           " & vbNewLine _
                                 & "  ON  F01_01.NRS_BR_CD                = M38_02.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F01_01.UNSOCO_CD                = M38_02.UNSOCO_CD                 " & vbNewLine _
                                 & " AND  F01_01.UNSOCO_BR_CD             = M38_02.UNSOCO_BR_CD              " & vbNewLine _
                                 & " AND  M38_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_03                           " & vbNewLine _
                                 & "  ON  F01_02.NRS_BR_CD                = M38_03.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F01_02.UNSOCO_CD                = M38_03.UNSOCO_CD                 " & vbNewLine _
                                 & " AND  F01_02.UNSOCO_BR_CD             = M38_03.UNSOCO_BR_CD              " & vbNewLine _
                                 & " AND  M38_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_04                           " & vbNewLine _
                                 & "  ON  F01_03.NRS_BR_CD                = M38_04.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F01_03.UNSOCO_CD                = M38_04.UNSOCO_CD                 " & vbNewLine _
                                 & " AND  F01_03.UNSOCO_BR_CD             = M38_04.UNSOCO_BR_CD              " & vbNewLine _
                                 & " AND  M38_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_05                           " & vbNewLine _
                                 & "  ON  F01_04.NRS_BR_CD                = M38_05.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F01_04.UNSOCO_CD                = M38_05.UNSOCO_CD                 " & vbNewLine _
                                 & " AND  F01_04.UNSOCO_BR_CD             = M38_05.UNSOCO_BR_CD              " & vbNewLine _
                                 & " AND  M38_05.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M10_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.CUST_CD_L                = M10_01.CUST_CD_L                 " & vbNewLine _
                                 & " AND  F02_01.ORIG_CD                  = M10_01.DEST_CD                   " & vbNewLine _
                                 & " AND  M10_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_02                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M10_02.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.CUST_CD_L                = M10_02.CUST_CD_L                 " & vbNewLine _
                                 & " AND  F02_01.DEST_CD                  = M10_02.DEST_CD                   " & vbNewLine _
                                 & " AND  M10_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_03                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M10_03.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  'ZZZZZ'                         = M10_03.CUST_CD_L                 " & vbNewLine _
                                 & " AND  F02_01.ORIG_CD                  = M10_03.DEST_CD                   " & vbNewLine _
                                 & " AND  M10_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_04                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M10_04.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  'ZZZZZ'                         = M10_04.CUST_CD_L                 " & vbNewLine _
                                 & " AND  F02_01.DEST_CD                  = M10_04.DEST_CD                   " & vbNewLine _
                                 & " AND  M10_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_01                           " & vbNewLine _
                                 & "  ON  F02_01.SYUKA_TYUKEI_CD          = M12_01.JIS_CD                    " & vbNewLine _
                                 & " AND  M12_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_02                           " & vbNewLine _
                                 & "  ON  F02_01.HAIKA_TYUKEI_CD          = M12_02.JIS_CD                    " & vbNewLine _
                                 & " AND  M12_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_AREA             M36_01                           " & vbNewLine _
                                 & "  ON  F02_01.NRS_BR_CD                = M36_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F02_01.AREA_CD                  = M36_01.AREA_CD                   " & vbNewLine _
                                 & " AND  F02_01.BIN_KB                   = M36_01.BIN_KB                    " & vbNewLine _
                                 & " AND  M10_02.JIS                      = M36_01.JIS_CD                    " & vbNewLine _
                                 & " AND  M36_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_01                           " & vbNewLine _
                                 & "  ON  F01_01.NRS_BR_CD                = M39_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F01_01.CAR_KEY                  = M39_01.CAR_KEY                   " & vbNewLine _
                                 & " AND  M39_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_02                           " & vbNewLine _
                                 & "  ON  F01_04.NRS_BR_CD                = M39_02.NRS_BR_CD                 " & vbNewLine _
                                 & " AND  F01_04.CAR_KEY                  = M39_02.CAR_KEY                   " & vbNewLine _
                                 & " AND  M39_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_01                           " & vbNewLine _
                                 & "  ON  F01_01.DRIVER_CD                = M37_01.DRIVER_CD                 " & vbNewLine _
                                 & " AND  M37_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_02                           " & vbNewLine _
                                 & "  ON  F01_04.DRIVER_CD                = M37_02.DRIVER_CD                 " & vbNewLine _
                                 & " AND  M37_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_01                           " & vbNewLine _
                                 & "  ON  F02_01.BIN_KB                   = KBN_01.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_01.KBN_GROUP_CD             = 'U001'                           " & vbNewLine _
                                 & " AND  KBN_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_02                           " & vbNewLine _
                                 & "  ON  F02_01.TARIFF_BUNRUI_KB         = KBN_02.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_02.KBN_GROUP_CD             = 'T015'                           " & vbNewLine _
                                 & " AND  KBN_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_03                           " & vbNewLine _
                                 & "  ON  F02_01.UNSO_ONDO_KB             = KBN_03.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_03.KBN_GROUP_CD             = 'U006'                           " & vbNewLine _
                                 & " AND  KBN_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_04                           " & vbNewLine _
                                 & "  ON  F02_01.MOTO_DATA_KB             = KBN_04.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_04.KBN_GROUP_CD             = 'M004'                           " & vbNewLine _
                                 & " AND  KBN_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_05                           " & vbNewLine _
                                 & "  ON  M39_01.CAR_TP_KB                = KBN_05.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_05.KBN_GROUP_CD             = 'S023'                           " & vbNewLine _
                                 & " AND  KBN_05.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_06                           " & vbNewLine _
                                 & "  ON  M39_02.CAR_TP_KB                = KBN_06.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_06.KBN_GROUP_CD             = 'S023'                           " & vbNewLine _
                                 & " AND  KBN_06.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_07                           " & vbNewLine _
                                 & "  ON  M39_01.VCLE_KB                  = KBN_07.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_07.KBN_GROUP_CD             = 'S012'                           " & vbNewLine _
                                 & " AND  KBN_07.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_08                           " & vbNewLine _
                                 & "  ON  M39_02.VCLE_KB                  = KBN_08.KBN_CD                    " & vbNewLine _
                                 & " AND  KBN_08.KBN_GROUP_CD             = 'S012'                           " & vbNewLine _
                                 & " AND  KBN_08.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "LEFT  JOIN $LM_MST$..S_USER             S01_01                           " & vbNewLine _
                                 & "  ON  F02_01.SYS_ENT_USER             = S01_01.USER_CD                   " & vbNewLine _
                                 & " AND  S01_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & "--運送Lでの荷主帳票パターン取得                                          " & vbNewLine _
                                 & "LEFT JOIN $LM_MST$..M_CUST_RPT          MCR1                             " & vbNewLine _
                                 & "  ON MCR1.NRS_BR_CD                   = F02_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND MCR1.CUST_CD_L                   = F02_01.CUST_CD_L                 " & vbNewLine _
                                 & " AND MCR1.CUST_CD_M                   = F02_01.CUST_CD_M                 " & vbNewLine _
                                 & " AND MCR1.CUST_CD_S                   = '00'                             " & vbNewLine _
                                 & " AND MCR1.PTN_ID                      = 'BM'                             " & vbNewLine _
                                 & " AND MCR1.SYS_DEL_FLG                 = '0'                              " & vbNewLine _
                                 & "--帳票パターン取得                                                       " & vbNewLine _
                                 & "LEFT JOIN $LM_MST$..M_RPT               MR1                              " & vbNewLine _
                                 & "  ON MR1.NRS_BR_CD                    = MCR1.NRS_BR_CD                   " & vbNewLine _
                                 & " AND MR1.PTN_ID                       = MCR1.PTN_ID                      " & vbNewLine _
                                 & " AND MR1.PTN_CD                       = MCR1.PTN_CD                      " & vbNewLine _
                                 & " AND MR1.SYS_DEL_FLG                  = '0'                              " & vbNewLine _
                                 & "--存在しない場合の帳票パターン取得                                       " & vbNewLine _
                                 & "LEFT JOIN $LM_MST$..M_RPT               MR3                              " & vbNewLine _
                                 & "  ON MR3.NRS_BR_CD                    = F02_01.NRS_BR_CD                 " & vbNewLine _
                                 & " AND MR3.PTN_ID                       = 'BM'                             " & vbNewLine _
                                 & " AND MR3.STANDARD_FLAG                = '01'                             " & vbNewLine _
                                 & " AND MR3.SYS_DEL_FLG                  = '0'                              " & vbNewLine _
                                 & "WHERE F02_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                 & " AND F02_01.UNSO_WT   IS NOT NULL                                        " & vbNewLine _
                                 & " AND F02_01.UNSO_WT   <> 0                                               " & vbNewLine



#End Region

#Region "GROUP BY"
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                                                     " & vbNewLine _
                                     & "  CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                     & "  ELSE MR3.RPT_ID END                                             " & vbNewLine _
                                     & " , F02_01.NRS_BR_CD                                               " & vbNewLine _
                                     & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                          " & vbNewLine _
                                     & "                               THEN F01_01.TRIP_DATE              " & vbNewLine _
                                     & "                               ELSE F01_04.TRIP_DATE              " & vbNewLine _
                                     & "  END                                                             " & vbNewLine _
                                     & " ,F02_01.TRIP_NO                                                  " & vbNewLine _
                                     & " ,F01_01.UNSOCO_CD                                                " & vbNewLine _
                                     & " ,F01_01.JSHA_KB                                                  " & vbNewLine _
                                     & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                          " & vbNewLine _
                                     & "                               THEN M39_01.CAR_NO                 " & vbNewLine _
                                     & "                               ELSE M39_02.CAR_NO                 " & vbNewLine _
                                     & "  END                                                             " & vbNewLine _
                                     & " ,CASE WHEN F02_01.MOTO_DATA_KB = '10'                            " & vbNewLine _
                                     & "                   THEN ISNULL(M10_01.DEST_NM,M10_03.DEST_NM)     " & vbNewLine _
                                     & "       WHEN F02_01.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                     & "                   THEN ISNULL(M10_02.DEST_NM,M10_04.DEST_NM)     " & vbNewLine _
                                     & "  ELSE ISNULL(M10_02.DEST_NM,M10_04.DEST_NM)  END                 " & vbNewLine _
                                     & " ,M38_02.UNSOCO_NM                                                " & vbNewLine _
                                     & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                          " & vbNewLine _
                                     & "                               THEN M37_01.DRIVER_NM              " & vbNewLine _
                                     & "                               ELSE M37_02.DRIVER_NM              " & vbNewLine _
                                     & "  END                                                             " & vbNewLine _
                                     & " ,ISNULL(M39_01.VCLE_KB,M39_02.VCLE_KB)                           " & vbNewLine _
                                     & " ,M01_01.NRS_BR_NM                                                " & vbNewLine _
                                     & " ,CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                          " & vbNewLine _
                                     & "                               THEN KBN_07.KBN_NM1                " & vbNewLine _
                                     & "                               ELSE KBN_08.KBN_NM1                " & vbNewLine _
                                     & "  END                                                             " & vbNewLine _
                                     & " ,CASE WHEN F02_01.MOTO_DATA_KB = '10'                            " & vbNewLine _
                                     & "                   THEN ISNULL(M10_01.AD_1,M10_03.AD_1)           " & vbNewLine _
                                     & "       WHEN F02_01.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                     & "                   THEN ISNULL(M10_02.AD_1,M10_04.AD_1)           " & vbNewLine _
                                     & "  ELSE ISNULL(M10_02.AD_1,M10_04.AD_1)  END                       " & vbNewLine _
                                     & " ,CASE WHEN F02_01.MOTO_DATA_KB = '10'                            " & vbNewLine _
                                     & "                   THEN ISNULL(M10_01.AD_2,M10_03.AD_2)           " & vbNewLine _
                                     & "       WHEN F02_01.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                     & "                   THEN ISNULL(M10_02.AD_2,M10_04.AD_2)           " & vbNewLine _
                                     & "  ELSE ISNULL(M10_02.AD_2,M10_04.AD_2)  END                       " & vbNewLine _
                                     & " ,M07_01.CUST_NM_L                                                " & vbNewLine

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks> 
    Private Const SQL_ORDER_BY As String = "ORDER BY                                        " & vbNewLine _
                                         & "      TRIP_DATE                             ASC " & vbNewLine _
                                         & "    , F01_01.JSHA_KB                        ASC " & vbNewLine _
                                         & "    , ISNULL(M39_01.VCLE_KB,M39_02.VCLE_KB) ASC " & vbNewLine _
                                         & "    , F01_01.UNSOCO_CD                      ASC " & vbNewLine _
                                         & "    , CAR_NO                                ASC " & vbNewLine _
                                         & "    , F02_01.TRIP_NO                        ASC " & vbNewLine _
                                         & "    , DEST_NM                               ASC " & vbNewLine

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF581IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF581DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF581DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF581DAC", "SelectMPrt", cmd)

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

    ''' <summary>
    ''' 運賃テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF581IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF581DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用SELECT句)
        Me._StrSql.Append(LMF581DAC.SQL_FROM)             'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMF581DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP_BY句)
        Me._StrSql.Append(LMF581DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER_BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF581DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("TRIP_DATE", "TRIP_DATE")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("JSHA_KB", "JSHA_KB")
        map.Add("CAR_NO", "CAR_NO")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("DRIVER_NM", "DRIVER_NM")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SYARYO_NM", "SYARYO_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("PAGE_KBN_NM", "PAGE_KBN_NM")
        '(2013.04.11) 要望番号2015 追加START
        map.Add("CUST_NM_L", "CUST_NM_L")
        '(2013.04.11) 要望番号2015 追加END

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF581OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'Me._StrSql.Append(" WHERE ")
        'Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        'With Me._Row


        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("  AND F02_01.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '輸送部営業所
            whereStr = .Item("YUSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.YUSO_BR_CD = @YUSO_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運送会社(1次)コード
            whereStr = .Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.UNSO_CD LIKE @UNSO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", whereStr, DBDataType.NVARCHAR))
            End If

            '運送会社支店(1次)コード
            whereStr = .Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.UNSO_BR_CD LIKE @UNSO_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", whereStr, DBDataType.NVARCHAR))
            End If

            '運送会社(1次)名
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M38_01.UNSOCO_NM + '　' + M38_01.UNSOCO_BR_NM LIKE @UNSO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NM", whereStr, DBDataType.NVARCHAR))
            End If

            '荷主(大)コード
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主(中)コード
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M07_01.CUST_NM_L + '　' + M07_01.CUST_NM_M LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", whereStr, DBDataType.NVARCHAR))
            End If

            '日付絞込条件
            Call Me.SetConditionDateSQL(Me._SqlPrmList, Me._Row, Me._StrSql)

            '作成者コード
            whereStr = .Item("SYS_ENT_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.SYS_ENT_USER LIKE @SYS_ENT_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", whereStr, DBDataType.CHAR))
            End If

            '作成者名
            whereStr = .Item("SYS_ENT_USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND S01_01.USER_NM LIKE @SYS_ENT_USER_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER_NM", whereStr, DBDataType.NVARCHAR))
            End If

            '運行紐付け
            whereStr = .Item("UNCO_ARI_NASHI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                If LMF581DAC.TYUKEI_HAISO_FLG_ON.Equals(whereStr) = True Then

                    '運行番号のどれかに値がある場合、紐付け済み
                    Me._StrSql.Append(" AND  ( '' <> F02_01.TRIP_NO OR '' <> F02_01.TRIP_NO_SYUKA OR '' <> F02_01.TRIP_NO_TYUKEI OR '' <> F02_01.TRIP_NO_HAIKA ) ")
                    Me._StrSql.Append(vbNewLine)

                Else

                    '運行番号の全てに値がない場合、紐付け未
                    Me._StrSql.Append(" AND F02_01.TRIP_NO = '' ")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND F02_01.TRIP_NO_SYUKA = '' ")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND F02_01.TRIP_NO_TYUKEI = '' ")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND F02_01.TRIP_NO_HAIKA = '' ")
                    Me._StrSql.Append(vbNewLine)

                End If

            End If

            '中継配送フラグ
            whereStr = .Item("TYUKEI_HAISO_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.TYUKEI_HAISO_FLG = @TYUKEI_HAISO_FLG")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", whereStr, DBDataType.CHAR))
            End If

            '運送番号
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.UNSO_NO_L LIKE @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
            End If

            '便区分
            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.BIN_KB = @BIN_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If

            'タリフ分類
            whereStr = .Item("TARIFF_BUNRUI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", whereStr, DBDataType.CHAR))
            End If

            '荷主参照番号
            whereStr = .Item("CUST_REF_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.CUST_REF_NO LIKE @CUST_REF_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", whereStr, DBDataType.NVARCHAR))
            End If

            '発地名
            whereStr = .Item("ORIG_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M10_01.DEST_NM LIKE @ORIG_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_NM", whereStr, DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M10_02.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", whereStr, DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_ADD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M10_02.AD_1 LIKE @DEST_ADD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ADD", whereStr, DBDataType.NVARCHAR))
            End If

            'エリア名
            whereStr = .Item("AREA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M36_01.AREA_NM LIKE @AREA_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_NM", whereStr, DBDataType.NVARCHAR))
            End If

            '管理番号
            whereStr = .Item("KANRI_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.INOUTKA_NO_L LIKE @KANRI_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))
            End If

            '備考
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.REMARK LIKE @REMARK")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", whereStr, DBDataType.NVARCHAR))
            End If

            'まとめ番号
            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F04_01.GROUP_NO LIKE @SEIQ_GROUP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", whereStr, DBDataType.CHAR))
            End If

            '運送温度区分
            whereStr = .Item("UNSO_ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.UNSO_ONDO_KB = @UNSO_ONDO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", whereStr, DBDataType.CHAR))
            End If

            '元データ区分
            whereStr = .Item("MOTO_DATA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F02_01.MOTO_DATA_KB = @MOTO_DATA_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", whereStr, DBDataType.CHAR))
            End If

            '中継配送フラグごとのSQL構築
            Call Me.SetHaishoWhereData(Me._SqlPrmList, Me._Row, Me._StrSql)

        End With



    End Sub


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(日付絞込)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetConditionDateSQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        With dr

            '日付絞込がない場合、スルー
            Dim dateKbn As String = .Item("DATE_KBN").ToString()
            If String.IsNullOrEmpty(dateKbn) = True Then
                Exit Sub
            End If

            Dim fromDate As String = .Item("DATE_FROM").ToString()
            Dim toDate As String = .Item("DATE_TO").ToString()

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(fromDate) = True _
                AndAlso String.IsNullOrEmpty(toDate) = True _
                Then
                Exit Sub
            End If

            Dim fromCondition As String = String.Empty
            Dim toCondition As String = String.Empty

            Select Case dateKbn

                Case LMF581DAC.DATE_KBN_NONYU

                    fromCondition = " AND F02_01.ARR_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.ARR_PLAN_DATE <= @TO_DATE "

                Case LMF581DAC.DATE_KBN_SHUKKA

                    fromCondition = " AND F02_01.OUTKA_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.OUTKA_PLAN_DATE <= @TO_DATE "

                Case LMF581DAC.DATE_KBN_LL

                    fromCondition = " AND ( F01_01.TRIP_DATE >= @FROM_DATE OR F01_04.TRIP_DATE >= @FROM_DATE ) "
                    toCondition = " AND ( F01_01.TRIP_DATE <= @TO_DATE OR F01_04.TRIP_DATE <= @TO_DATE ) "

                Case LMF581DAC.DATE_KBN_ENT

                    fromCondition = " AND F02_01.SYS_ENT_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.SYS_ENT_DATE <= @TO_DATE "

            End Select

            If String.IsNullOrEmpty(fromDate) = False Then
                sql.Append(fromCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@FROM_DATE", fromDate, DBDataType.CHAR))
            End If

            If String.IsNullOrEmpty(toDate) = False Then
                sql.Append(toCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TO_DATE", toDate, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(中継配送有 Or 無)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetHaishoWhereData(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        Dim whereStr As String = String.Empty

        With dr

            '運送会社コード
            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    F01_01.UNSOCO_CD LIKE @UNSOCO_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_02.UNSOCO_CD LIKE @UNSOCO_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_03.UNSOCO_CD LIKE @UNSOCO_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_04.UNSOCO_CD LIKE @UNSOCO_CD ) ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", whereStr, DBDataType.NVARCHAR))

            End If

            '運送会社支店コード
            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    F01_01.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_02.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_03.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_04.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", whereStr, DBDataType.NVARCHAR))

            End If

            '運送会社名
            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_02.UNSOCO_NM + '　' + M38_02.UNSOCO_BR_NM LIKE @UNSOCO_NM ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", whereStr, DBDataType.NVARCHAR))

            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO LIKE @TRIP_NO ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", whereStr, DBDataType.CHAR))

            End If

            '乗務員名
            whereStr = .Item("DRIVER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M37_01.DRIVER_NM LIKE @DRIVER_NM ")
                sql.Append(vbNewLine)
                sql.Append("       OR M37_02.DRIVER_NM LIKE @DRIVER_NM ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", whereStr, DBDataType.NVARCHAR))

            End If

            '車種
            whereStr = .Item("CAR_TP_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M39_01.CAR_TP_KB LIKE @CAR_TP_KB ")
                sql.Append(vbNewLine)
                sql.Append("       OR M39_02.CAR_TP_KB LIKE @CAR_TP_KB ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@CAR_TP_KB", whereStr, DBDataType.CHAR))

            End If

            '車番
            whereStr = .Item("CAR_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M39_01.CAR_NO LIKE @CAR_NO ")
                sql.Append(vbNewLine)
                sql.Append("       OR M39_02.CAR_NO LIKE @CAR_NO ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@CAR_NO", whereStr, DBDataType.NVARCHAR))

            End If

            '自傭区分
            whereStr = .Item("JSHA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M39_01.JSHA_KB LIKE @JSHA_KB ")
                sql.Append(vbNewLine)
                sql.Append("       OR M39_02.JSHA_KB LIKE @JSHA_KB ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@JSHA_KB", whereStr, DBDataType.CHAR))

            End If

            '配荷中継地
            whereStr = .Item("HAIKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M12_01.KEN + M12_01.SHI LIKE @HAIKA_TYUKEI_NM ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_NM", whereStr, DBDataType.NVARCHAR))

            End If

            '集荷中継地
            whereStr = .Item("SYUKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M12_02.KEN + M12_02.SHI LIKE @SYUKA_TYUKEI_NM ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_NM", whereStr, DBDataType.NVARCHAR))

            End If

            '運行番号(集荷)
            whereStr = .Item("TRIP_NO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO_SYUKA LIKE @TRIP_NO_SYUKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", whereStr, DBDataType.CHAR))

            End If

            '運行番号(中継)
            whereStr = .Item("TRIP_NO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO_TYUKEI LIKE @TRIP_NO_TYUKEI ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", whereStr, DBDataType.CHAR))

            End If

            '運行番号(配荷)
            whereStr = .Item("TRIP_NO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO_HAIKA LIKE @TRIP_NO_HAIKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", whereStr, DBDataType.CHAR))

            End If

            '運送会社(集荷)
            whereStr = .Item("UNSOCO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_03.UNSOCO_NM + '　' + M38_03.UNSOCO_BR_NM LIKE @UNSOCO_SYUKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_SYUKA", whereStr, DBDataType.NVARCHAR))

            End If

            '運送会社(中継)
            whereStr = .Item("UNSOCO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_04.UNSOCO_NM + '　' + M38_04.UNSOCO_BR_NM LIKE @UNSOCO_TYUKEI ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_TYUKEI", whereStr, DBDataType.NVARCHAR))

            End If

            '運送会社(配荷)
            whereStr = .Item("UNSOCO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_05.UNSOCO_NM + '　' + M38_05.UNSOCO_BR_NM LIKE @UNSOCO_HAIKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_HAIKA", whereStr, DBDataType.NVARCHAR))

            End If

        End With

    End Sub



#End Region

#Region "設定処理"

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

#End Region

End Class
