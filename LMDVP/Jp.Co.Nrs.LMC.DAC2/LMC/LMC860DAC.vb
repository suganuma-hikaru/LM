' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC860DAC : 出荷報告作成
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC860DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC860DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    '2013.07.30 追加START
#Region "実績作成処理 データ抽出用SQL"

#Region "H_SENDOUTEDI_BYKAGT SELECT句"

    Private Const SQL_SELECT_BYKAGT_SEND_DATA As String = "SELECT                                                                                                                                     " & vbNewLine _
                                      & " BASE.DEL_KB													                                                 AS DEL_KB					                  " & vbNewLine _
                                      & ",BASE.CRT_DATE                                                                                                  AS CRT_DATE                                  " & vbNewLine _
                                      & ",BASE.FILE_NAME                                                                                                 AS FILE_NAME                                 " & vbNewLine _
                                      & ",BASE.REC_NO                                                                                                    AS REC_NO                                    " & vbNewLine _
                                      & ",BASE.GYO                                                                                                       AS GYO                                       " & vbNewLine _
                                      & ",BASE.NRS_BR_CD                                                                                                 AS NRS_BR_CD                                 " & vbNewLine _
                                      & ",BASE.EDI_CTL_NO                                                                                                AS EDI_CTL_NO                                " & vbNewLine _
                                      & ",BASE.EDI_CTL_NO_CHU                                                                                            AS EDI_CTL_NO_CHU                            " & vbNewLine _
                                      & ",BASE.OUTKA_CTL_NO                                                                                              AS OUTKA_CTL_NO                              " & vbNewLine _
                                      & ",BASE.OUTKA_CTL_NO_CHU			                                                                                 AS OUTKA_CTL_NO_CHU                          " & vbNewLine _
                                      & ",BASE.CUST_CD_L                                                                                                 AS CUST_CD_L                                 " & vbNewLine _
                                      & ",BASE.CUST_CD_M                                                                                                 AS CUST_CD_M                                 " & vbNewLine _
                                      & ",BASE.E1EDK01_ACTION                                                                                            AS E1EDK01_ACTION                            " & vbNewLine _
                                      & ",BASE.E1EDK01_CURCY                                                                                             AS E1EDK01_CURCY                             " & vbNewLine _
                                      & ",BASE.E1EDK01_LIFSK                                                                                             AS E1EDK01_LIFSK                             " & vbNewLine _
                                      & ",BASE.E1EDK02_QUALF_BELNR_002                                                                                   AS E1EDK02_QUALF_BELNR_002                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_012                                                                                   AS E1EDK14_QUALF_ORGID_012                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_008                                                                                   AS E1EDK14_QUALF_ORGID_008                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_007                                                                                   AS E1EDK14_QUALF_ORGID_007                   " & vbNewLine _
                                      & ",BASE.E1EDK14_QUALF_ORGID_006                                                                                   AS E1EDK14_QUALF_ORGID_006                   " & vbNewLine _
                                      & ",BASE.E1EDKA1_PARVW_LIFNR_AG                                                                                    AS E1EDKA1_PARVW_LIFNR_AG                    " & vbNewLine _
                                      & ",CASE WHEN BASE.CUST_CD_L = '00266' THEN                                                                                                                     " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250556' THEN                                                                                                " & vbNewLine _
                                      & "            CASE WHEN (BASE.E1EDKA1_PARVW_LIFNR_WE = '' AND BASE.E1EDKA1_PARVW_PARTN_ZZ = '') THEN                                                           " & vbNewLine _
                                      & "                '99999999999'                                                                                                                                " & vbNewLine _
                                      & "            ELSE   BASE.E1EDKA1_PARVW_PARTN_ZZ + BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                 " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250570' THEN                                                                                                     " & vbNewLine _
                                      & "            CASE WHEN (BASE.E1EDKA1_PARVW_LIFNR_WE = '' OR LEN(BASE.E1EDKA1_PARVW_LIFNR_WE) <= 11) THEN                                                      " & vbNewLine _
                                      & "                '9999999999'                                                                                                                                 " & vbNewLine _
                                      & "            ELSE LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,12)                                                                                                        " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        ELSE                                                                                                                                                 " & vbNewLine _
                                      & "            CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_WE = '' THEN '9999999999'                                                                                     " & vbNewLine _
                                      & "            ELSE BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                                                 " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE '100000'                                                                                                                                            " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "WHEN BASE.CUST_CD_L = '00759' THEN                                                                                                                           " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN (BASE.E1EDKA1_PARVW_LIFNR_WE = '' AND BASE.E1EDKA1_PARVW_PARTN_ZZ = '') THEN                                                               " & vbNewLine _
                                      & "            '99999999999'                                                                                                                                    " & vbNewLine _
                                      & "        ELSE   BASE.E1EDKA1_PARVW_PARTN_ZZ + BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                     " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE '100000'                                                                                                                                            " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "ELSE                                                                                                                                                         " & vbNewLine _
                                      & "    BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                                                              " & vbNewLine _
                                      & "END 														AS E1EDKA1_PARVW_LIFNR_WE                                                                         " & vbNewLine _
                                      & "--,BASE.E1EDKA1_PARVW_LIFNR_WE                                                                                                                               " & vbNewLine _
                                      & ",CASE WHEN BASE.CUST_CD_L = '00266' THEN                                                                                                                     " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN BASE.E1EDKA1_PARVW_PARTN_ZZ = '' THEN                                                                                                      " & vbNewLine _
                                      & "            CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250556' THEN                                                                                            " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,6)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250563' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,5)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250564' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,5)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '250570' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,6)                                                                                                         " & vbNewLine _
                                      & "            WHEN BASE.E1EDKA1_PARVW_LIFNR_AG = '252370' THEN                                                                                                 " & vbNewLine _
                                      & "                 LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,5)                                                                                                         " & vbNewLine _
                                      & "            ELSE '99999'                                                                                                                                     " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        ELSE BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                     " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE ''                                                                                                                                                  " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "WHEN BASE.CUST_CD_L = '00759' THEN                                                                                                                           " & vbNewLine _
                                      & "    CASE WHEN BASE.SAMPLE_HOUKOKU_FLG = '0' THEN                                                                                                             " & vbNewLine _
                                      & "        CASE WHEN BASE.E1EDKA1_PARVW_PARTN_ZZ = '' THEN                                                                                                      " & vbNewLine _
                                      & "            CASE WHEN BASE.E1EDKA1_PARVW_LIFNR_WE = '' THEN                                                                                                  " & vbNewLine _
                                      & "                '999999'                                                                                                                                     " & vbNewLine _
                                      & "            ELSE LEFT(BASE.E1EDKA1_PARVW_LIFNR_WE,6)                                                                                                         " & vbNewLine _
                                      & "            END                                                                                                                                              " & vbNewLine _
                                      & "        ELSE BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                     " & vbNewLine _
                                      & "        END                                                                                                                                                  " & vbNewLine _
                                      & "    ELSE ''                                                                                                                                                  " & vbNewLine _
                                      & "    END                                                                                                                                                      " & vbNewLine _
                                      & "ELSE                                                                                                                                                         " & vbNewLine _
                                      & "    BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                              " & vbNewLine _
                                      & "END 														AS E1EDKA1_PARVW_PARTN_ZZ                                                                         " & vbNewLine _
                                      & "--,BASE.E1EDKA1_PARVW_PARTN_ZZ                                                                                                                               " & vbNewLine _
                                      & ",BASE.E1EDK03_IDDAT_DATUM_001											                                         AS E1EDK03_IDDAT_DATUM_001                   " & vbNewLine _
                                      & ",BASE.E1EDKT1_TDID                                                                                              AS E1EDKT1_TDID                              " & vbNewLine _
                                      & ",BASE.E1EDKT1_TSSPRAS                                                                                           AS E1EDKT1_TSSPRAS                           " & vbNewLine _
                                      & ",BASE.E1EDKT1_TDOBJECT                                                                                          AS E1EDKT1_TDOBJECT                          " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5         " & vbNewLine _
                                      & ",BASE.E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6                                                                         AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6         " & vbNewLine _
                                      & ",BASE.E1EDK02_QUALF_BELNR_001                                                                                   AS E1EDK02_QUALF_BELNR_001                   " & vbNewLine _
                                      & ",BASE.E1EDK02_QUALF_BELNR_043                                                                                   AS E1EDK02_QUALF_BELNR_043                   " & vbNewLine _
                                      & ",BASE.E1EDP01_POSEX                                                                                             AS E1EDP01_POSEX                             " & vbNewLine _
                                      & ",BASE.E1EDP01_MATNR                                                                                             AS E1EDP01_MATNR                             " & vbNewLine _
                                      & ",BASE.E1EDP01_MENGE                                                                                             AS E1EDP01_MENGE                             " & vbNewLine _
                                      & ",BASE.E1EDP01_MENEE                                                                                             AS E1EDP01_MENEE                             " & vbNewLine _
                                      & ",BASE.E1EDP01_WERKS                                                                                             AS E1EDP01_WERKS                             " & vbNewLine _
                                      & ",BASE.E1EDP01_VSTEL                                                                                             AS E1EDP01_VSTEL                             " & vbNewLine _
                                      & ",BASE.E1EDP01_LGORT                                                                                             AS E1EDP01_LGORT                             " & vbNewLine _
                                      & ",BASE.E1EDP02_QUALF_BELNR_043                                                                                   AS E1EDP02_QUALF_BELNR_043                   " & vbNewLine _
                                      & ",BASE.E1EDP02_QUALF_ZEILE_043                                                                                   AS E1EDP02_QUALF_ZEILE_043                   " & vbNewLine _
                                      & ",BASE.E1EDP02_QUALF_BELNR_044                                                                                   AS E1EDP02_QUALF_BELNR_044                   " & vbNewLine _
                                      & ",BASE.E1EDP03_QUALF_DATE_010                                                                                    AS E1EDP03_QUALF_DATE_010                    " & vbNewLine _
                                      & ",BASE.E1EDP03_QUALF_DATE_024                                                                                    AS E1EDP03_QUALF_DATE_024                    " & vbNewLine _
                                      & ",BASE.E1EDP20_WMENG_EDATU                                                                                       AS E1EDP20_WMENG_EDATU                       " & vbNewLine _
                                      & ",BASE.E1EDP19_QUALF_IDTNR_002                                                                                   AS E1EDP19_QUALF_IDTNR_002                   " & vbNewLine _
                                      & ",BASE.E1EDP19_QUALF_IDTNR_010                                                                                   AS E1EDP19_QUALF_IDTNR_010                   " & vbNewLine _
                                      & ",BASE.E1EDKA1_PARVW_PARTN_DUMMY                                                                                 AS E1EDKA1_PARVW_PARTN_DUMMY                 " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME1                                                                                             AS E1EDKA1_NAME1                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME2                                                                                             AS E1EDKA1_NAME2                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME3                                                                                             AS E1EDKA1_NAME3                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_NAME4                                                                                             AS E1EDKA1_NAME4                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_STRAS                                                                                             AS E1EDKA1_STRAS                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_STRS2                                                                                             AS E1EDKA1_STRS2                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_ORT01                                                                                             AS E1EDKA1_ORT01                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_PSTLZ                                                                                             AS E1EDKA1_PSTLZ                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_LAND1                                                                                             AS E1EDKA1_LAND1                             " & vbNewLine _
                                      & ",BASE.E1EDKA1_TELF1                                                                                             AS E1EDKA1_TELF1                             " & vbNewLine _
                                      & ",BASE.SAMPLE_HOUKOKU_FLG                                                                                        AS SAMPLE_HOUKOKU_FLG                        " & vbNewLine _
                                      & ",BASE.RECORD_STATUS                                                                                             AS RECORD_STATUS                             " & vbNewLine _
                                      & ",BASE.JISSEKI_SHORI_FLG                                                                                         AS JISSEKI_SHORI_FLG                         " & vbNewLine _
                                      & ",BASE.SAMPLEHOUKOKU_SHORI_FLG                                                                                   AS SAMPLEHOUKOKU_SHORI_FLG                   " & vbNewLine _
                                      & "FROM (                                                                                                                                                       " & vbNewLine _
                                      & " SELECT	                               							" & vbNewLine _
                                      & " '0'                                          AS DEL_KB	                               " & vbNewLine _
                                      & ",@SYS_UPD_DATE                                AS CRT_DATE                                 " & vbNewLine _
                                      & ",OUT_L.OUTKA_NO_L + '_' + OUT_M.OUTKA_NO_M + '_' + OUT_S.OUTKA_NO_S + '_' + @SYS_UPD_DATE + '_' + @SYS_UPD_TIME   AS FILE_NAME                                " & vbNewLine _
                                      & ",'00001'                                      AS REC_NO                                   " & vbNewLine _
                                      & ",'001'                                        AS GYO                                      " & vbNewLine _
                                      & ",OUT_L.NRS_BR_CD                              AS NRS_BR_CD                                " & vbNewLine _
                                      & ",''                                           AS EDI_CTL_NO                               " & vbNewLine _
                                      & ",''                                           AS EDI_CTL_NO_CHU                           " & vbNewLine _
                                      & ",OUT_M.OUTKA_NO_L                             AS OUTKA_CTL_NO                             " & vbNewLine _
                                      & ",OUT_M.OUTKA_NO_M                             AS OUTKA_CTL_NO_CHU                         " & vbNewLine _
                                      & ",OUT_L.CUST_CD_L                              AS CUST_CD_L                                " & vbNewLine _
                                      & ",OUT_L.CUST_CD_M                              AS CUST_CD_M                                " & vbNewLine _
                                      & ",'004'                                        AS E1EDK01_ACTION                           " & vbNewLine _
                                      & ",'JPY'                                        AS E1EDK01_CURCY                            " & vbNewLine _
                                      & ",'ZJ'                                         AS E1EDK01_LIFSK                            " & vbNewLine _
                                      & ",''                                           AS E1EDK02_QUALF_BELNR_002                  " & vbNewLine _
                                      & ",'ZSO'                                        AS E1EDK14_QUALF_ORGID_012                  " & vbNewLine _
                                      & ",'3420'                                       AS E1EDK14_QUALF_ORGID_008                  " & vbNewLine _
                                      & ",'01'                                         AS E1EDK14_QUALF_ORGID_007                  " & vbNewLine _
                                      & ",'01'                                         AS E1EDK14_QUALF_ORGID_006                  " & vbNewLine _
                                      & "  ,CASE WHEN MIN_GOODS.CUST_CD_S <> '01' THEN --修正開始 2014.12.22                                                                                      " & vbNewLine _
                                      & "   CASE WHEN OUT_L.CUST_CD_L='00266' THEN                                                                                                                " & vbNewLine _
                                      & "    CASE WHEN   OUT_L.CUST_CD_M = '02' THEN                                                                                                              " & vbNewLine _
                                      & "  		CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2) = 'NE' THEN Z_B018_02.KBN_NM5                                                                          " & vbNewLine _
                                      & "  	    　　 WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2) = 'NC' THEN Z_B018_06.KBN_NM1                                                                          " & vbNewLine _
                                      & "      ELSE Z_B018_02.KBN_NM1 END                                                                                                                         " & vbNewLine _
                                      & "      WHEN  OUT_L.CUST_CD_M = '03' THEN Z_B018_03.KBN_NM1                                                                                                " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') THEN Z_B018_01.KBN_NM1                                                                     " & vbNewLine _
                                      & "      ELSE '' END                                                                                                                                        " & vbNewLine _
                                      & "    ELSE                                                                                                                                                 " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='TT' OR  SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='T-' THEN Z_B018_01.KBN_NM1 ELSE                                " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='DN' THEN Z_B018_02.KBN_NM1 ELSE                                                                          " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='NE' THEN Z_B018_02.KBN_NM5 ELSE                                                                          " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='C-' THEN Z_B018_03.KBN_NM1 ELSE                                                                          " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2)='NC' THEN Z_B018_06.KBN_NM1 ELSE '' END END END END END END                                               " & vbNewLine _
                                      & "   WHEN MIN_GOODS.CUST_CD_S =  '01' THEN                                                                                                                 " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='T' THEN Z_B018_01.KBN_NM1 ELSE                                                                           " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='B' THEN Z_B018_05.KBN_NM1 ELSE                                                                           " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='N' THEN Z_B018_02.KBN_NM1 ELSE                                                                           " & vbNewLine _
                                      & "    CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,1)='C' THEN Z_B018_03.KBN_NM1 ELSE '' END END END END END AS E1EDKA1_PARVW_LIFNR_AG  --修正開始 2014.12.22   " & vbNewLine _
                                      & ",CASE WHEN OUT_L.CUST_CD_M = '02' THEN OUT_L.DENP_NO                                      " & vbNewLine _
                                      & "      WHEN OUT_L.CUST_CD_M = '03' THEN OUT_L.DENP_NO                                      " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' AND M_GOODS.CUST_CD_S <> '01' THEN OUT_L.DENP_NO  " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN CONVERT(nvarchar(5),DTL_BYKTST.NONYU_CD)    " & vbNewLine _
                                      & "      ELSE OUT_L.DEST_CD END E1EDKA1_PARVW_LIFNR_WE                                       " & vbNewLine _
                                      & ",CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN OUT_L.SHIP_CD_L  " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN CONVERT(nvarchar(6),DTL_BYKTST.TOKUI_CD)    " & vbNewLine _
                                      & "      ELSE OUT_L.SHIP_CD_L END E1EDKA1_PARVW_PARTN_ZZ                                     " & vbNewLine _
                                      & ",OUT_L.ARR_PLAN_DATE                          AS E1EDK03_IDDAT_DATUM_001                  " & vbNewLine _
                                      & ",'Z015'                                       AS E1EDKT1_TDID                             " & vbNewLine _
                                      & ",'JA'                                         AS E1EDKT1_TSSPRAS                          " & vbNewLine _
                                      & ",'VBBK'                                       AS E1EDKT1_TDOBJECT                         " & vbNewLine _
                                      & ",CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN M_DEST.DEST_NM   " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN DTL_BYKTST.NONYU_NM1    " & vbNewLine _
                                      & "      ELSE M_DEST.DEST_NM END E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1                           " & vbNewLine _
                                      & "--,CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN ''               " & vbNewLine _
                                      & "--      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN ISNULL(RTRIM(DTL_BYKTST.NONYU_NM),'') " & vbNewLine _
                                      & "--      ELSE ''             END E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2                           " & vbNewLine _
                                      & ",''                                           AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2        " & vbNewLine _
                                      & "--,M_DEST.AD_1                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3        " & vbNewLine _
                                      & "--,M_DEST.AD_2                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4        " & vbNewLine _
                                      & "--,M_DEST.AD_3                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5        " & vbNewLine _
                                      & ",OUT_L.DEST_AD_1                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3        " & vbNewLine _
                                      & ",OUT_L.DEST_AD_2                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4        " & vbNewLine _
                                      & ",OUT_L.DEST_AD_3                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5        " & vbNewLine _
                                      & ",M_DEST.TEL                                   AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6        " & vbNewLine _
                                      & ",CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN OUT_L.CUST_ORD_NO        " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN OUT_M.BUYER_ORD_NO_DTL  " & vbNewLine _
                                      & "      ELSE OUT_L.CUST_ORD_NO END E1EDK02_QUALF_BELNR_001                                  " & vbNewLine _
                                      & "--,OUT_M.RSV_NO                                 AS E1EDK02_QUALF_BELNR_043                  " & vbNewLine _
                                      & ",REPLACE(LEFT(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,''))),'-','')  AS E1EDK02_QUALF_BELNR_043                  " & vbNewLine _
                                      & "--,SUBSTRING(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,'')) +1,40) AS E1EDP01_POSEX            " & vbNewLine _
                                      & ",''                                           AS E1EDP01_POSEX                            " & vbNewLine _
                                      & "--要望管理番号2387                                                                        " & vbNewLine _
                                      & "--,M_GOODS.GOODS_CD_CUST                        AS E1EDP01_MATNR                            " & vbNewLine _
                                      & "--,Replace(M_GOODS.GOODS_CD_CUST,'-NP','')      AS E1EDP01_MATNR                            " & vbNewLine _
                                      & ",Replace(M_GOODS.GOODS_CD_CUST,M_GOODS.CLASS_3,'')      AS E1EDP01_MATNR                  " & vbNewLine _
                                      & ",ISNULL(OUT_S.ALCTD_NB,0)                     AS E1EDP01_MENGE                            " & vbNewLine _
                                      & ",'PCE'                                        AS E1EDP01_MENEE                            " & vbNewLine _
                                      & ",'3420'                                       AS E1EDP01_WERKS                            " & vbNewLine _
                                      & ",''                                           AS E1EDP01_VSTEL                            " & vbNewLine _
                                      & "--,ISNULL(M_CUSTC.REMARK,'1000')                AS E1EDP01_LGORT                            " & vbNewLine _
                                      & ",CASE WHEN M_CUSTC.REMARK IS NULL THEN '1000'                                             " & vbNewLine _
                                      & "      ELSE REVERSE(CONVERT(VARCHAR(4), REVERSE(M_CUSTC.REMARK))) END  E1EDP01_LGORT       " & vbNewLine _
                                      & ",REPLACE(LEFT(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,''))),'-','')  AS E1EDP02_QUALF_BELNR_043                  " & vbNewLine _
                                      & "--,OUT_M.RSV_NO                                 AS E1EDP02_QUALF_BELNR_043                  " & vbNewLine _
                                      & "--,ISNULL(OUT_S.REMARK,'')                      AS E1EDP02_QUALF_ZEILE_043                  " & vbNewLine _
                                      & ",SUBSTRING(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,'')) +1,40) AS E1EDP02_QUALF_ZEILE_043            " & vbNewLine _
                                      & ",CASE WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID = 'LMC020' THEN OUT_L.BUYER_ORD_NO       " & vbNewLine _
                                      & "      WHEN (OUT_L.CUST_CD_M = '00' OR OUT_L.CUST_CD_M = '01') AND OUT_L.SYS_ENT_PGID <> 'LMC020' THEN OUT_M.CUST_ORD_NO_DTL   " & vbNewLine _
                                      & "      ELSE OUT_L.BUYER_ORD_NO END E1EDP02_QUALF_BELNR_044                  " & vbNewLine _
                                      & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP03_QUALF_DATE_010                   " & vbNewLine _
                                      & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP03_QUALF_DATE_024                   " & vbNewLine _
                                      & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP20_WMENG_EDATU                      " & vbNewLine _
                                      & "--要望管理番号2387                                                                        " & vbNewLine _
                                      & "--,M_GOODS.GOODS_CD_CUST                      AS E1EDP19_QUALF_IDTNR_002                  " & vbNewLine _
                                      & "--,Replace(M_GOODS.GOODS_CD_CUST,'-NP','')      AS E1EDP19_QUALF_IDTNR_002                  " & vbNewLine _
                                      & ",Replace(M_GOODS.GOODS_CD_CUST,M_GOODS.CLASS_3,'')      AS E1EDP19_QUALF_IDTNR_002        " & vbNewLine _
                                      & ",ISNULL(OUT_S.LOT_NO,'')                      AS E1EDP19_QUALF_IDTNR_010                  " & vbNewLine _
                                      & "--要望番号2091(2013.10.24) 追加START                                                      " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '100000'                                         " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_PARVW_PARTN_DUMMY               " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN OUT_L.DEST_NM                                    " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_NAME1                           " & vbNewLine _
                                      & ",''                                           AS E1EDKA1_NAME2                            " & vbNewLine _
                                      & ",''                                           AS E1EDKA1_NAME3                            " & vbNewLine _
                                      & ",''                                           AS E1EDKA1_NAME4                            " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_1                                 " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_STRAS                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_2                                 " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_STRS2                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_3                                 " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_ORT01                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  M_DEST.ZIP                                      " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_PSTLZ                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  'JP'                                            " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_LAND1                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  M_DEST.TEL                                      " & vbNewLine _
                                      & "      ELSE ''                                 END E1EDKA1_TELF1                           " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '1'                                              " & vbNewLine _
                                      & "      ELSE '0'                                END SAMPLE_HOUKOKU_FLG                      " & vbNewLine _
                                      & "--要望番号2091(2013.10.24) 追加END                                                        " & vbNewLine _
                                      & ",''                                           AS RECORD_STATUS                            " & vbNewLine _
                                      & ",'2'                                          AS JISSEKI_SHORI_FLG                        " & vbNewLine _
                                      & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '2'                                              " & vbNewLine _
                                      & "      ELSE '0'                                END SAMPLEHOUKOKU_SHORI_FLG                 " & vbNewLine

#End Region

#Region "H_SENDOUTEDI_BYKAGT FROM句"

    Private Const SQL_FROM_BYKAGT_SEND_DATA As String = "FROM                                                       " & vbNewLine _
                                          & "$LM_TRN$..C_OUTKA_M OUT_M                                              " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..C_OUTKA_L OUT_L                                   " & vbNewLine _
                                          & "  ON  OUT_L.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                          & "  AND OUT_L.NRS_BR_CD = OUT_M.NRS_BR_CD                                " & vbNewLine _
                                          & "  AND OUT_L.OUTKA_NO_L = OUT_M.OUTKA_NO_L                              " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDI_L                                " & vbNewLine _
                                          & "  ON  EDI_L.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                          & "  AND EDI_L.NRS_BR_CD = OUT_L.NRS_BR_CD                                " & vbNewLine _
                                          & "  AND EDI_L.OUTKA_CTL_NO = OUT_L.OUTKA_NO_L                            " & vbNewLine _
                                          & "LEFT JOIN                                                              " & vbNewLine _
                                          & "(SELECT                                                                " & vbNewLine _
                                          & "      TST.DEL_KB                                                       " & vbNewLine _
                                          & "     ,TST.NRS_BR_CD                                                    " & vbNewLine _
                                          & "--   ,TST.CRT_DATE                                                     " & vbNewLine _
                                          & "--   ,TST.FILE_NAME                                                    " & vbNewLine _
                                          & "--   ,TST.REC_NO                                                       " & vbNewLine _
                                          & "     ,TST.EDI_CTL_NO                                                   " & vbNewLine _
                                          & "     ,TST.NONYU_CD                                                     " & vbNewLine _
                                          & "     ,TST.TOKUI_CD                                                     " & vbNewLine _
                                          & "     ,TST.NONYU_NM1                                                    " & vbNewLine _
                                          & "--   ,MIN(TST.NONYU_NM2 + TST.NONYU_NM3) AS NONYU_NM                   " & vbNewLine _
                                          & "--   ,TST.NONYU_NM2                                                    " & vbNewLine _
                                          & "--   ,TST.NONYU_NM3                                                    " & vbNewLine _
                                          & "--   ,TST.JUTYU_NO                                                     " & vbNewLine _
                                          & "--   ,TST.ORDER_NO                                                     " & vbNewLine _
                                          & "     FROM                                                              " & vbNewLine _
                                          & "     $LM_TRN$..H_OUTKAEDI_DTL_BYKTST TST                               " & vbNewLine _
                                          & "     WHERE                                                             " & vbNewLine _
                                          & "         TST.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                          & "     AND TST.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                          & "     AND TST.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                          & "     GROUP BY                                                          " & vbNewLine _
                                          & "      TST.DEL_KB                                                       " & vbNewLine _
                                          & "     ,TST.NRS_BR_CD                                                    " & vbNewLine _
                                          & "--   ,TST.CRT_DATE                                                     " & vbNewLine _
                                          & "--   ,TST.FILE_NAME                                                    " & vbNewLine _
                                          & "--   ,TST.REC_NO                                                       " & vbNewLine _
                                          & "     ,TST.EDI_CTL_NO                                                   " & vbNewLine _
                                          & "     ,TST.NONYU_CD                                                     " & vbNewLine _
                                          & "     ,TST.TOKUI_CD                                                     " & vbNewLine _
                                          & "     ,TST.NONYU_NM1                                                    " & vbNewLine _
                                          & "--   ,TST.NONYU_NM2                                                    " & vbNewLine _
                                          & "--   ,TST.NONYU_NM3                                                    " & vbNewLine _
                                          & "--   ,TST.JUTYU_NO                                                     " & vbNewLine _
                                          & "--   ,TST.ORDER_NO                                                     " & vbNewLine _
                                          & ") DTL_BYKTST                                                           " & vbNewLine _
                                          & "  ON  DTL_BYKTST.DEL_KB <> '1'                                         " & vbNewLine _
                                          & "  AND DTL_BYKTST.NRS_BR_CD = EDI_L.NRS_BR_CD                           " & vbNewLine _
                                          & "  AND DTL_BYKTST.EDI_CTL_NO = EDI_L.EDI_CTL_NO                         " & vbNewLine _
                                          & "-- AND DTL_BYKTST.EDI_CTL_NO_CHU = EDI_M.EDI_CTL_NO_CHU                " & vbNewLine _
                                          & "-- AND DTL_BYKTST.JISSEKI_SHORI_FLG = '1'                              " & vbNewLine _
                                          & " LEFT JOIN                                                             " & vbNewLine _
                                          & " (SELECT                                                               " & vbNewLine _
                                          & "      S2.NRS_BR_CD                                                     " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_L                                                    " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_M                                                    " & vbNewLine _
                                          & "     ,MAX(S2.OUTKA_NO_S)  AS OUTKA_NO_S                                " & vbNewLine _
                                          & "     ,S2.LOT_NO                                                        " & vbNewLine _
                                          & "     ,S2.ZAI_REC_NO                                                    " & vbNewLine _
                                          & "     ,S2.SERIAL_NO                                                        " & vbNewLine _
                                          & "     ,SUM(S2.ALCTD_NB) AS ALCTD_NB                                     " & vbNewLine _
                                          & "     FROM                                                              " & vbNewLine _
                                          & "     $LM_TRN$..C_OUTKA_S S2                                            " & vbNewLine _
                                          & "     WHERE                                                             " & vbNewLine _
                                          & "     S2.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                          & "--     AND                                                               " & vbNewLine _
                                          & "--     S2.OUTKA_NO_L = @OUTKA_CTL_NO                                     " & vbNewLine _
                                          & "     AND                                                               " & vbNewLine _
                                          & "     S2.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                          & "     GROUP BY                                                          " & vbNewLine _
                                          & "      S2.NRS_BR_CD                                                     " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_L                                                    " & vbNewLine _
                                          & "     ,S2.OUTKA_NO_M                                                    " & vbNewLine _
                                          & "     ,S2.LOT_NO                                                        " & vbNewLine _
                                          & "     ,S2.ZAI_REC_NO                                                    " & vbNewLine _
                                          & "     ,S2.SERIAL_NO                                                        " & vbNewLine _
                                          & " ) OUT_S                                                               " & vbNewLine _
                                          & "  ON                                                                   " & vbNewLine _
                                          & "      OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                                " & vbNewLine _
                                          & "  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                              " & vbNewLine _
                                          & "  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                              " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI_TRS                                 " & vbNewLine _
                                          & "  ON  ZAI_TRS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  AND ZAI_TRS.NRS_BR_CD = OUT_S.NRS_BR_CD                              " & vbNewLine _
                                          & "  AND ZAI_TRS.ZAI_REC_NO = OUT_S.ZAI_REC_NO                            " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_SENDOUTEDI_BYKAGT SND_BYKAGT                    " & vbNewLine _
                                          & " ON  SND_BYKAGT.DEL_KB <> '1'                                          " & vbNewLine _
                                          & " AND SND_BYKAGT.NRS_BR_CD = OUT_M.NRS_BR_CD                            " & vbNewLine _
                                          & " AND SND_BYKAGT.OUTKA_CTL_NO = OUT_M.OUTKA_NO_L                        " & vbNewLine _
                                          & " AND SND_BYKAGT.OUTKA_CTL_NO_CHU = OUT_M.OUTKA_NO_M                    " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_GOODS  M_GOODS                                  " & vbNewLine _
                                          & "  ON  M_GOODS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  AND M_GOODS.NRS_BR_CD = OUT_L.NRS_BR_CD                              " & vbNewLine _
                                          & "  AND M_GOODS.CUST_CD_L = OUT_L.CUST_CD_L                              " & vbNewLine _
                                          & "  AND M_GOODS.CUST_CD_M = OUT_L.CUST_CD_M                              " & vbNewLine _
                                          & "  AND M_GOODS.GOODS_CD_NRS = OUT_M.GOODS_CD_NRS                        " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_DEST  M_DEST                                    " & vbNewLine _
                                          & "  ON  M_DEST.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                          & "  AND M_DEST.NRS_BR_CD = OUT_L.NRS_BR_CD                               " & vbNewLine _
                                          & "  AND M_DEST.CUST_CD_L = OUT_L.CUST_CD_L                               " & vbNewLine _
                                          & "  AND M_DEST.DEST_CD = OUT_L.DEST_CD                                   " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_CUSTCOND  M_CUSTC                               " & vbNewLine _
                                          & "  ON  M_CUSTC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  AND M_CUSTC.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                            " & vbNewLine _
                                          & "  AND M_CUSTC.CUST_CD_L = ZAI_TRS.CUST_CD_L                            " & vbNewLine _
                                          & "  AND M_CUSTC.JOTAI_CD = ZAI_TRS.GOODS_COND_KB_3                       " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_01                                  " & vbNewLine _
                                          & "  ON  Z_B018_01.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_01.KBN_CD = '01'                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_02                                  " & vbNewLine _
                                          & "  ON  Z_B018_02.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_02.KBN_CD = '02'                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_03                                  " & vbNewLine _
                                          & "  ON  Z_B018_03.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_03.KBN_CD = '03'                                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_05          --追加開始 2014.12.22   " & vbNewLine _
                                          & "  ON  Z_B018_05.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_05.KBN_CD = '05'                                          " & vbNewLine _
                                          & " LEFT JOIN LM_MST..Z_KBN  Z_B018_06                                    " & vbNewLine _
                                          & "  ON  Z_B018_06.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
                                          & "  AND Z_B018_06.KBN_CD = '06'                                          " & vbNewLine _
                                          & " LEFT JOIN                                                             " & vbNewLine _
                                          & " (SELECT                                                               " & vbNewLine _
                                          & "	 SUBCOM.NRS_BR_CD       AS NRS_BR_CD                                " & vbNewLine _
                                          & "	,SUBCOM.OUTKA_NO_L      AS OUTKA_NO_L                               " & vbNewLine _
                                          & "	,MIN(SUBCOM.OUTKA_NO_M) AS OUTKA_NO_M                               " & vbNewLine _
                                          & "  FROM                                                                 " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_M SUBCOM                                          " & vbNewLine _
                                          & "  LEFT JOIN                                                            " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_L SUBCOL                                          " & vbNewLine _
                                          & "	ON  SUBCOL.NRS_BR_CD  = SUBCOM.NRS_BR_CD                            " & vbNewLine _
                                          & "	AND SUBCOL.OUTKA_NO_L = SUBCOM.OUTKA_NO_L                           " & vbNewLine _
                                          & "  WHERE                                                                " & vbNewLine _
                                          & "	    SUBCOM.NRS_BR_CD   = @NRS_BR_CD                                 " & vbNewLine _
                                          & "	AND SUBCOL.CUST_CD_L   = @CUST_CD_L                                 " & vbNewLine _
                                          & "	AND SUBCOL.CUST_CD_M   = @CUST_CD_M                                 " & vbNewLine _
                                          & "	AND SUBCOM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "	AND SUBCOL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                          & "  GROUP BY                                                             " & vbNewLine _
                                          & "	 SUBCOM.NRS_BR_CD                                                   " & vbNewLine _
                                          & "	,SUBCOM.OUTKA_NO_L                                                  " & vbNewLine _
                                          & ") MIN_COM                                                              " & vbNewLine _
                                          & "  ON  MIN_COM.NRS_BR_CD   = OUT_L.NRS_BR_CD                            " & vbNewLine _
                                          & "  AND MIN_COM.OUTKA_NO_L  = OUT_L.OUTKA_NO_L                           " & vbNewLine _
                                          & " LEFT JOIN                                                             " & vbNewLine _
                                          & " (SELECT                                                               " & vbNewLine _
                                          & "	 SUBCOM2.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                          & "	,SUBCOM2.OUTKA_NO_L   AS OUTKA_NO_L                                 " & vbNewLine _
                                          & "	,SUBCOM2.OUTKA_NO_M   AS OUTKA_NO_M                                 " & vbNewLine _
                                          & "	,SUBCOM2.GOODS_CD_NRS AS GOODS_CD_NRS                               " & vbNewLine _
                                          & "  FROM                                                                 " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_M SUBCOM2                                         " & vbNewLine _
                                          & "  LEFT JOIN                                                            " & vbNewLine _
                                          & "	$LM_TRN$..C_OUTKA_L SUBCOL2                                         " & vbNewLine _
                                          & "	ON  SUBCOL2.NRS_BR_CD  = SUBCOM2.NRS_BR_CD                          " & vbNewLine _
                                          & "	AND SUBCOL2.OUTKA_NO_L = SUBCOM2.OUTKA_NO_L                         " & vbNewLine _
                                          & "  WHERE                                                                " & vbNewLine _
                                          & "	    SUBCOM2.NRS_BR_CD   = @NRS_BR_CD                                " & vbNewLine _
                                          & "	AND SUBCOL2.CUST_CD_L   = @CUST_CD_L                                " & vbNewLine _
                                          & "	AND SUBCOL2.CUST_CD_M   = @CUST_CD_M                                " & vbNewLine _
                                          & "	AND SUBCOM2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                          & "	AND SUBCOL2.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                          & ") MIN_CD                                                               " & vbNewLine _
                                          & "  ON  MIN_CD.NRS_BR_CD   = MIN_COM.NRS_BR_CD                           " & vbNewLine _
                                          & "  AND MIN_CD.OUTKA_NO_L  = MIN_COM.OUTKA_NO_L                          " & vbNewLine _
                                          & "  AND MIN_CD.OUTKA_NO_M  = MIN_COM.OUTKA_NO_M                          " & vbNewLine _
                                          & " LEFT JOIN $LM_MST$..M_GOODS  MIN_GOODS                              " & vbNewLine _
                                          & "  ON  MIN_GOODS.NRS_BR_CD    = MIN_CD.NRS_BR_CD                      " & vbNewLine _
                                          & "  AND MIN_GOODS.GOODS_CD_NRS = MIN_CD.GOODS_CD_NRS                   " & vbNewLine _
                                          & "  AND MIN_GOODS.SYS_DEL_FLG  = '0'             --追加終了 2014.12.22 " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BYKEKT AS BYKEKT                   " & vbNewLine _
                                          & "  ON  BYKEKT.NRS_BR_CD       = OUT_L.NRS_BR_CD                         " & vbNewLine _
                                          & "  AND BYKEKT.OUTKA_CTL_NO    = OUT_L.OUTKA_NO_L                        " & vbNewLine _
                                          & "  AND BYKEKT.SYS_DEL_FLG = '0'                 --追加終了 2017.07.20   " & vbNewLine _
                                          & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BYKDIR AS BYKDIR                   " & vbNewLine _
                                          & "  ON  BYKDIR.NRS_BR_CD       = OUT_L.NRS_BR_CD                         " & vbNewLine _
                                          & "  AND BYKDIR.OUTKA_CTL_NO    = OUT_L.OUTKA_NO_L                        " & vbNewLine _
                                          & "  AND BYKDIR.SYS_DEL_FLG = '0'                 --追加終了 2017.08.01   " & vbNewLine _
                                          & " WHERE                                                                 " & vbNewLine _
                                          & "       OUT_M.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                          & "  AND  OUT_M.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                          & "  AND  OUT_L.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                          & "  AND  OUT_L.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                          & "  AND  OUT_L.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE                        " & vbNewLine _
                                          & "  AND  OUT_L.OUTKA_STATE_KB >= '60'                                    " & vbNewLine _
                                          & "  AND  OUT_L.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                          & "--  AND  OUT_L.SYS_ENT_PGID = 'LMC020'                                 " & vbNewLine _
                                          & "  AND  OUT_L.SYUBETU_KB <> '50'                                        " & vbNewLine _
                                          & "--  (2013.11.25) 要望番号2130 追加START                                " & vbNewLine _
                                          & "  AND  OUT_L.OUTKAHOKOKU_YN <> '00'                                    " & vbNewLine _
                                          & "--  (2013.11.25) 要望番号2130 追加END                                  " & vbNewLine _
                                          & "  AND  BYKEKT.OUTKA_CTL_NO IS NULL                                     " & vbNewLine _
                                          & "  AND  BYKDIR.OUTKA_CTL_NO IS NULL                                     " & vbNewLine _
                                          & "  AND  SND_BYKAGT.SYS_DEL_FLG IS NULL                                  " & vbNewLine _
                                          & "  ) BASE                                  				                " & vbNewLine


    '    Private Const SQL_SELECT_BYKAGT_SEND_DATA As String = "SELECT                                                                      " & vbNewLine _
    '                                          & " '0'                                          AS DEL_KB	                               " & vbNewLine _
    '                                          & ",@SYS_UPD_DATE                                AS CRT_DATE                                 " & vbNewLine _
    '                                          & ",OUT_L.OUTKA_NO_L + '_' + OUT_M.OUTKA_NO_M + '_' + OUT_S.OUTKA_NO_S + '_' + @SYS_UPD_DATE + '_' + @SYS_UPD_TIME   AS FILE_NAME                                " & vbNewLine _
    '                                          & ",'00001'                                      AS REC_NO                                   " & vbNewLine _
    '                                          & ",'001'                                        AS GYO                                      " & vbNewLine _
    '                                          & ",OUT_L.NRS_BR_CD                              AS NRS_BR_CD                                " & vbNewLine _
    '                                          & ",''                                           AS EDI_CTL_NO                               " & vbNewLine _
    '                                          & ",''                                           AS EDI_CTL_NO_CHU                           " & vbNewLine _
    '                                          & ",OUT_M.OUTKA_NO_L                             AS OUTKA_CTL_NO                             " & vbNewLine _
    '                                          & ",OUT_M.OUTKA_NO_M                             AS OUTKA_CTL_NO_CHU                         " & vbNewLine _
    '                                          & ",OUT_L.CUST_CD_L                              AS CUST_CD_L                                " & vbNewLine _
    '                                          & ",OUT_L.CUST_CD_M                              AS CUST_CD_M                                " & vbNewLine _
    '                                          & ",'004'                                        AS E1EDK01_ACTION                           " & vbNewLine _
    '                                          & ",'JPY'                                        AS E1EDK01_CURCY                            " & vbNewLine _
    '                                          & ",'ZJ'                                         AS E1EDK01_LIFSK                            " & vbNewLine _
    '                                          & ",''                                           AS E1EDK02_QUALF_BELNR_002                  " & vbNewLine _
    '                                          & ",'ZSO'                                        AS E1EDK14_QUALF_ORGID_012                  " & vbNewLine _
    '                                          & ",'3420'                                       AS E1EDK14_QUALF_ORGID_008                  " & vbNewLine _
    '                                          & ",'01'                                         AS E1EDK14_QUALF_ORGID_007                  " & vbNewLine _
    '                                          & ",'01'                                         AS E1EDK14_QUALF_ORGID_006                  " & vbNewLine _
    '                                          & ",CASE WHEN OUT_L.CUST_CD_M = '02' THEN CASE WHEN SUBSTRING(OUT_L.CUST_ORD_NO,1,2) = 'NE' THEN Z_B018_02.KBN_NM5 ELSE Z_B018_02.KBN_NM1 END  " & vbNewLine _
    '                                          & "      WHEN OUT_L.CUST_CD_M = '03' THEN Z_B018_03.KBN_NM1                                  " & vbNewLine _
    '                                          & "      WHEN OUT_L.CUST_CD_M = '01' THEN Z_B018_01.KBN_NM1                                  " & vbNewLine _
    '                                          & "      ELSE ''                                                                             " & vbNewLine _
    '                                          & "      END  E1EDKA1_PARVW_LIFNR_AG                                                         " & vbNewLine _
    '                                          & ",CASE WHEN OUT_L.CUST_CD_M = '02' THEN OUT_L.DENP_NO                                      " & vbNewLine _
    '                                          & "      WHEN OUT_L.CUST_CD_M = '03' THEN OUT_L.DENP_NO                                      " & vbNewLine _
    '                                          & "      WHEN OUT_L.CUST_CD_M = '01' AND OUT_L.SYS_ENT_PGID = 'LMC020' AND M_GOODS.CUST_CD_S <> '01' THEN OUT_L.DENP_NO  " & vbNewLine _
    '                                          & "      ELSE OUT_L.DEST_CD END E1EDKA1_PARVW_LIFNR_WE                                       " & vbNewLine _
    '                                          & ",OUT_L.SHIP_CD_L                              AS E1EDKA1_PARVW_PARTN_ZZ                   " & vbNewLine _
    '                                          & ",OUT_L.ARR_PLAN_DATE                          AS E1EDK03_IDDAT_DATUM_001                  " & vbNewLine _
    '                                          & ",'Z015'                                       AS E1EDKT1_TDID                             " & vbNewLine _
    '                                          & ",'JA'                                         AS E1EDKT1_TSSPRAS                          " & vbNewLine _
    '                                          & ",'VBBK'                                       AS E1EDKT1_TDOBJECT                         " & vbNewLine _
    '                                          & ",M_DEST.DEST_NM                               AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1        " & vbNewLine _
    '                                          & ",''                                           AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2        " & vbNewLine _
    '                                          & "--,M_DEST.AD_1                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3        " & vbNewLine _
    '                                          & "--,M_DEST.AD_2                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4        " & vbNewLine _
    '                                          & "--,M_DEST.AD_3                                  AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5        " & vbNewLine _
    '                                          & ",OUT_L.DEST_AD_1                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3        " & vbNewLine _
    '                                          & ",OUT_L.DEST_AD_2                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4        " & vbNewLine _
    '                                          & ",OUT_L.DEST_AD_3                              AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5        " & vbNewLine _
    '                                          & ",M_DEST.TEL                                   AS E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6        " & vbNewLine _
    '                                          & ",OUT_L.CUST_ORD_NO                            AS E1EDK02_QUALF_BELNR_001                  " & vbNewLine _
    '                                          & "--,OUT_M.RSV_NO                                 AS E1EDK02_QUALF_BELNR_043                  " & vbNewLine _
    '                                          & ",REPLACE(LEFT(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,''))),'-','')  AS E1EDK02_QUALF_BELNR_043                  " & vbNewLine _
    '                                          & "--,SUBSTRING(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,'')) +1,40) AS E1EDP01_POSEX            " & vbNewLine _
    '                                          & ",''                                           AS E1EDP01_POSEX                            " & vbNewLine _
    '                                          & ",M_GOODS.GOODS_CD_CUST                        AS E1EDP01_MATNR                            " & vbNewLine _
    '                                          & ",ISNULL(OUT_S.ALCTD_NB,0)                     AS E1EDP01_MENGE                            " & vbNewLine _
    '                                          & ",'PCE'                                        AS E1EDP01_MENEE                            " & vbNewLine _
    '                                          & ",'3420'                                       AS E1EDP01_WERKS                            " & vbNewLine _
    '                                          & ",''                                           AS E1EDP01_VSTEL                            " & vbNewLine _
    '                                          & "--,ISNULL(M_CUSTC.REMARK,'1000')                AS E1EDP01_LGORT                            " & vbNewLine _
    '                                          & ",CASE WHEN M_CUSTC.REMARK IS NULL THEN '1000'                                             " & vbNewLine _
    '                                          & "      ELSE REVERSE(CONVERT(VARCHAR(4), REVERSE(M_CUSTC.REMARK))) END  E1EDP01_LGORT       " & vbNewLine _
    '                                          & ",REPLACE(LEFT(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,''))),'-','')  AS E1EDP02_QUALF_BELNR_043                  " & vbNewLine _
    '                                          & "--,OUT_M.RSV_NO                                 AS E1EDP02_QUALF_BELNR_043                  " & vbNewLine _
    '                                          & "--,ISNULL(OUT_S.REMARK,'')                      AS E1EDP02_QUALF_ZEILE_043                  " & vbNewLine _
    '                                          & ",SUBSTRING(ISNULL(OUT_S.SERIAL_NO,''),CHARINDEX('-',ISNULL(OUT_S.SERIAL_NO,'')) +1,40) AS E1EDP02_QUALF_ZEILE_043            " & vbNewLine _
    '                                          & ",OUT_L.BUYER_ORD_NO                           AS E1EDP02_QUALF_BELNR_044                  " & vbNewLine _
    '                                          & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP03_QUALF_DATE_010                   " & vbNewLine _
    '                                          & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP03_QUALF_DATE_024                   " & vbNewLine _
    '                                          & ",OUT_L.OUTKA_PLAN_DATE                        AS E1EDP20_WMENG_EDATU                      " & vbNewLine _
    '                                          & ",M_GOODS.GOODS_CD_CUST                        AS E1EDP19_QUALF_IDTNR_002                  " & vbNewLine _
    '                                          & ",ISNULL(OUT_S.LOT_NO,'')                      AS E1EDP19_QUALF_IDTNR_010                  " & vbNewLine _
    '                                          & "--要望番号2091(2013.10.24) 追加START                                                      " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '100000'                                         " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_PARVW_PARTN_DUMMY               " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN OUT_L.DEST_NM                                    " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_NAME1                           " & vbNewLine _
    '                                          & ",''                                           AS E1EDKA1_NAME2                            " & vbNewLine _
    '                                          & ",''                                           AS E1EDKA1_NAME3                            " & vbNewLine _
    '                                          & ",''                                           AS E1EDKA1_NAME4                            " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_1                                 " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_STRAS                           " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_2                                 " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_STRS2                           " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  OUT_L.DEST_AD_3                                 " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_ORT01                           " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  M_DEST.ZIP                                      " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_PSTLZ                           " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  'JP'                                            " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_LAND1                           " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN  M_DEST.TEL                                      " & vbNewLine _
    '                                          & "      ELSE ''                                 END E1EDKA1_TELF1                           " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '1'                                              " & vbNewLine _
    '                                          & "      ELSE '0'                                END SAMPLE_HOUKOKU_FLG                      " & vbNewLine _
    '                                          & "--要望番号2091(2013.10.24) 追加END                                                        " & vbNewLine _
    '                                          & ",''                                           AS RECORD_STATUS                            " & vbNewLine _
    '                                          & ",'2'                                          AS JISSEKI_SHORI_FLG                        " & vbNewLine _
    '                                          & ",CASE WHEN M_GOODS.CUST_CD_S = '01' THEN '2'                                              " & vbNewLine _
    '                                          & "      ELSE '0'                                END SAMPLEHOUKOKU_SHORI_FLG                 " & vbNewLine

    '#End Region

    '#Region "H_SENDOUTEDI_BYKAGT FROM句"

    '    Private Const SQL_FROM_BYKAGT_SEND_DATA As String = "FROM                                                       " & vbNewLine _
    '                                          & "$LM_TRN$..C_OUTKA_M OUT_M                                              " & vbNewLine _
    '                                          & " LEFT JOIN $LM_TRN$..C_OUTKA_L OUT_L                                   " & vbNewLine _
    '                                          & "  ON  OUT_L.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                                          & "  AND OUT_L.NRS_BR_CD = OUT_M.NRS_BR_CD                                " & vbNewLine _
    '                                          & "  AND OUT_L.OUTKA_NO_L = OUT_M.OUTKA_NO_L                              " & vbNewLine _
    '                                          & " LEFT JOIN                                                             " & vbNewLine _
    '                                          & " (SELECT                                                               " & vbNewLine _
    '                                          & "      S2.NRS_BR_CD                                                     " & vbNewLine _
    '                                          & "     ,S2.OUTKA_NO_L                                                    " & vbNewLine _
    '                                          & "     ,S2.OUTKA_NO_M                                                    " & vbNewLine _
    '                                          & "     ,MAX(S2.OUTKA_NO_S)  AS OUTKA_NO_S                                " & vbNewLine _
    '                                          & "     ,S2.LOT_NO                                                        " & vbNewLine _
    '                                          & "     ,S2.ZAI_REC_NO                                                    " & vbNewLine _
    '                                          & "     ,S2.SERIAL_NO                                                        " & vbNewLine _
    '                                          & "     ,SUM(S2.ALCTD_NB) AS ALCTD_NB                                     " & vbNewLine _
    '                                          & "     FROM                                                              " & vbNewLine _
    '                                          & "     $LM_TRN$..C_OUTKA_S S2                                            " & vbNewLine _
    '                                          & "     WHERE                                                             " & vbNewLine _
    '                                          & "     S2.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
    '                                          & "--     AND                                                               " & vbNewLine _
    '                                          & "--     S2.OUTKA_NO_L = @OUTKA_CTL_NO                                     " & vbNewLine _
    '                                          & "     AND                                                               " & vbNewLine _
    '                                          & "     S2.SYS_DEL_FLG = '0'                                              " & vbNewLine _
    '                                          & "     GROUP BY                                                          " & vbNewLine _
    '                                          & "      S2.NRS_BR_CD                                                     " & vbNewLine _
    '                                          & "     ,S2.OUTKA_NO_L                                                    " & vbNewLine _
    '                                          & "     ,S2.OUTKA_NO_M                                                    " & vbNewLine _
    '                                          & "     ,S2.LOT_NO                                                        " & vbNewLine _
    '                                          & "     ,S2.ZAI_REC_NO                                                    " & vbNewLine _
    '                                          & "     ,S2.SERIAL_NO                                                        " & vbNewLine _
    '                                          & " ) OUT_S                                                               " & vbNewLine _
    '                                          & "  ON                                                                   " & vbNewLine _
    '                                          & "      OUT_S.NRS_BR_CD = OUT_M.NRS_BR_CD                                " & vbNewLine _
    '                                          & "  AND OUT_S.OUTKA_NO_L = OUT_M.OUTKA_NO_L                              " & vbNewLine _
    '                                          & "  AND OUT_S.OUTKA_NO_M = OUT_M.OUTKA_NO_M                              " & vbNewLine _
    '                                          & " LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI_TRS                                 " & vbNewLine _
    '                                          & "  ON  ZAI_TRS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                          & "  AND ZAI_TRS.NRS_BR_CD = OUT_S.NRS_BR_CD                              " & vbNewLine _
    '                                          & "  AND ZAI_TRS.ZAI_REC_NO = OUT_S.ZAI_REC_NO                            " & vbNewLine _
    '                                          & " LEFT JOIN $LM_TRN$..H_SENDOUTEDI_BYKAGT SND_BYKAGT                    " & vbNewLine _
    '                                          & " ON  SND_BYKAGT.DEL_KB <> '1'                                          " & vbNewLine _
    '                                          & " AND SND_BYKAGT.NRS_BR_CD = OUT_M.NRS_BR_CD                            " & vbNewLine _
    '                                          & " AND SND_BYKAGT.OUTKA_CTL_NO = OUT_M.OUTKA_NO_L                        " & vbNewLine _
    '                                          & " AND SND_BYKAGT.OUTKA_CTL_NO_CHU = OUT_M.OUTKA_NO_M                    " & vbNewLine _
    '                                          & " LEFT JOIN $LM_MST$..M_GOODS  M_GOODS                                  " & vbNewLine _
    '                                          & "  ON  M_GOODS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                          & "  AND M_GOODS.NRS_BR_CD = OUT_L.NRS_BR_CD                              " & vbNewLine _
    '                                          & "  AND M_GOODS.CUST_CD_L = OUT_L.CUST_CD_L                              " & vbNewLine _
    '                                          & "  AND M_GOODS.CUST_CD_M = OUT_L.CUST_CD_M                              " & vbNewLine _
    '                                          & "  AND M_GOODS.GOODS_CD_NRS = OUT_M.GOODS_CD_NRS                        " & vbNewLine _
    '                                          & " LEFT JOIN $LM_MST$..M_DEST  M_DEST                                    " & vbNewLine _
    '                                          & "  ON  M_DEST.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                          & "  AND M_DEST.NRS_BR_CD = OUT_L.NRS_BR_CD                               " & vbNewLine _
    '                                          & "  AND M_DEST.CUST_CD_L = OUT_L.CUST_CD_L                               " & vbNewLine _
    '                                          & "  AND M_DEST.DEST_CD = OUT_L.DEST_CD                                   " & vbNewLine _
    '                                          & " LEFT JOIN $LM_MST$..M_CUSTCOND  M_CUSTC                               " & vbNewLine _
    '                                          & "  ON  M_CUSTC.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                          & "  AND M_CUSTC.NRS_BR_CD = ZAI_TRS.NRS_BR_CD                            " & vbNewLine _
    '                                          & "  AND M_CUSTC.CUST_CD_L = ZAI_TRS.CUST_CD_L                            " & vbNewLine _
    '                                          & "  AND M_CUSTC.JOTAI_CD = ZAI_TRS.GOODS_COND_KB_3                       " & vbNewLine _
    '                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_01                                  " & vbNewLine _
    '                                          & "  ON  Z_B018_01.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
    '                                          & "  AND Z_B018_01.KBN_CD = '01'                                          " & vbNewLine _
    '                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_02                                  " & vbNewLine _
    '                                          & "  ON  Z_B018_02.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
    '                                          & "  AND Z_B018_02.KBN_CD = '02'                                          " & vbNewLine _
    '                                          & " LEFT JOIN $LM_MST$..Z_KBN  Z_B018_03                                  " & vbNewLine _
    '                                          & "  ON  Z_B018_03.KBN_GROUP_CD = 'B018'                                  " & vbNewLine _
    '                                          & "  AND Z_B018_03.KBN_CD = '03'                                          " & vbNewLine _
    '                                          & " WHERE                                                                 " & vbNewLine _
    '                                          & "       OUT_M.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
    '                                          & "  AND  OUT_M.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                          & "  AND  OUT_L.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
    '                                          & "  AND  OUT_L.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
    '                                          & "  AND  OUT_L.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE                        " & vbNewLine _
    '                                          & "  AND  OUT_L.OUTKA_STATE_KB >= '60'                                    " & vbNewLine _
    '                                          & "  AND  OUT_L.SYS_DEL_FLG = '0'                                         " & vbNewLine _
    '                                          & "  AND  OUT_L.SYS_ENT_PGID = 'LMC020'                                   " & vbNewLine _
    '                                          & "  AND  OUT_L.SYUBETU_KB <> '50'                                        " & vbNewLine _
    '                                          & "  AND  SND_BYKAGT.SYS_DEL_FLG IS NULL                                  " & vbNewLine

#End Region

#End Region
    '2013.07.30 追加END

    '2013.07.30 追加START
#Region "実績作成処理 更新用SQL"

#Region "H_SENDOUTEDI_BYKDIR"

    ''' <summary>
    ''' 実績TBLのINSERT（H_SENDOUTEDI_BYKAGT）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_H_SENDOUTEDI_BYKAGT As String = "INSERT INTO       " & vbNewLine _
                                     & "$LM_TRN$..H_SENDOUTEDI_BYKAGT       " & vbNewLine _
                                     & "(                                " & vbNewLine _
                                     & " DEL_KB                          " & vbNewLine _
                                     & ",CRT_DATE                        " & vbNewLine _
                                     & ",FILE_NAME                       " & vbNewLine _
                                     & ",REC_NO                          " & vbNewLine _
                                     & ",GYO                             " & vbNewLine _
                                     & ",NRS_BR_CD                       " & vbNewLine _
                                     & ",EDI_CTL_NO                      " & vbNewLine _
                                     & ",EDI_CTL_NO_CHU                  " & vbNewLine _
                                     & ",OUTKA_CTL_NO                    " & vbNewLine _
                                     & ",OUTKA_CTL_NO_CHU                " & vbNewLine _
                                     & ",CUST_CD_L                       " & vbNewLine _
                                     & ",CUST_CD_M                       " & vbNewLine _
                                     & ",E1EDK01_ACTION                       " & vbNewLine _
                                     & ",E1EDK01_CURCY                        " & vbNewLine _
                                     & ",E1EDK01_LIFSK                        " & vbNewLine _
                                     & ",E1EDK02_QUALF_BELNR_002              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_012              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_008              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_007              " & vbNewLine _
                                     & ",E1EDK14_QUALF_ORGID_006              " & vbNewLine _
                                     & ",E1EDKA1_PARVW_LIFNR_AG               " & vbNewLine _
                                     & ",E1EDKA1_PARVW_LIFNR_WE               " & vbNewLine _
                                     & ",E1EDKA1_PARVW_PARTN_ZZ               " & vbNewLine _
                                     & ",E1EDK03_IDDAT_DATUM_001              " & vbNewLine _
                                     & ",E1EDKT1_TDID                         " & vbNewLine _
                                     & ",E1EDKT1_TSSPRAS                      " & vbNewLine _
                                     & ",E1EDKT1_TDOBJECT                     " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5    " & vbNewLine _
                                     & ",E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6    " & vbNewLine _
                                     & ",E1EDK02_QUALF_BELNR_001              " & vbNewLine _
                                     & ",E1EDK02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",E1EDP01_POSEX                        " & vbNewLine _
                                     & ",E1EDP01_MATNR                        " & vbNewLine _
                                     & ",E1EDP01_MENGE                        " & vbNewLine _
                                     & ",E1EDP01_MENEE                        " & vbNewLine _
                                     & ",E1EDP01_WERKS                        " & vbNewLine _
                                     & ",E1EDP01_VSTEL                        " & vbNewLine _
                                     & ",E1EDP01_LGORT                        " & vbNewLine _
                                     & ",E1EDP02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",E1EDP02_QUALF_ZEILE_043              " & vbNewLine _
                                     & ",E1EDP02_QUALF_BELNR_044              " & vbNewLine _
                                     & ",E1EDP03_QUALF_DATE_010               " & vbNewLine _
                                     & ",E1EDP03_QUALF_DATE_024               " & vbNewLine _
                                     & ",E1EDP20_WMENG_EDATU                  " & vbNewLine _
                                     & ",E1EDP19_QUALF_IDTNR_002              " & vbNewLine _
                                     & ",E1EDP19_QUALF_IDTNR_010              " & vbNewLine _
                                     & ",E1EDKA1_PARVW_PARTN_DUMMY            " & vbNewLine _
                                     & ",E1EDKA1_NAME1                        " & vbNewLine _
                                     & ",E1EDKA1_NAME2                        " & vbNewLine _
                                     & ",E1EDKA1_NAME3                        " & vbNewLine _
                                     & ",E1EDKA1_NAME4                        " & vbNewLine _
                                     & ",E1EDKA1_STRAS                        " & vbNewLine _
                                     & ",E1EDKA1_STRS2                        " & vbNewLine _
                                     & ",E1EDKA1_ORT01                        " & vbNewLine _
                                     & ",E1EDKA1_PSTLZ                        " & vbNewLine _
                                     & ",E1EDKA1_LAND1                        " & vbNewLine _
                                     & ",E1EDKA1_TELF1                        " & vbNewLine _
                                     & ",SAMPLE_HOUKOKU_FLG                   " & vbNewLine _
                                     & ",RECORD_STATUS                   " & vbNewLine _
                                     & ",JISSEKI_SHORI_FLG               " & vbNewLine _
                                     & ",JISSEKI_USER                    " & vbNewLine _
                                     & ",JISSEKI_DATE                    " & vbNewLine _
                                     & ",JISSEKI_TIME                    " & vbNewLine _
                                     & ",SEND_USER                       " & vbNewLine _
                                     & ",SEND_DATE                       " & vbNewLine _
                                     & ",SEND_TIME                       " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_SHORI_FLG         " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_USER              " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_DATE              " & vbNewLine _
                                     & ",SAMPLEHOUKOKU_TIME              " & vbNewLine _
                                     & ",DELETE_USER                     " & vbNewLine _
                                     & ",DELETE_DATE                     " & vbNewLine _
                                     & ",DELETE_TIME                     " & vbNewLine _
                                     & ",DELETE_EDI_NO                   " & vbNewLine _
                                     & ",DELETE_EDI_NO_CHU               " & vbNewLine _
                                     & ",UPD_USER                        " & vbNewLine _
                                     & ",UPD_DATE                        " & vbNewLine _
                                     & ",UPD_TIME                        " & vbNewLine _
                                     & ",SYS_ENT_DATE                    " & vbNewLine _
                                     & ",SYS_ENT_TIME                    " & vbNewLine _
                                     & ",SYS_ENT_PGID                    " & vbNewLine _
                                     & ",SYS_ENT_USER                    " & vbNewLine _
                                     & ",SYS_UPD_DATE                    " & vbNewLine _
                                     & ",SYS_UPD_TIME                    " & vbNewLine _
                                     & ",SYS_UPD_PGID                    " & vbNewLine _
                                     & ",SYS_UPD_USER                    " & vbNewLine _
                                     & ",SYS_DEL_FLG                     " & vbNewLine _
                                     & ")VALUES(                         " & vbNewLine _
                                     & " @DEL_KB                         " & vbNewLine _
                                     & ",@CRT_DATE                       " & vbNewLine _
                                     & ",@FILE_NAME                      " & vbNewLine _
                                     & ",@REC_NO                         " & vbNewLine _
                                     & ",@GYO                            " & vbNewLine _
                                     & ",@NRS_BR_CD                      " & vbNewLine _
                                     & ",@EDI_CTL_NO                     " & vbNewLine _
                                     & ",@EDI_CTL_NO_CHU                 " & vbNewLine _
                                     & ",@OUTKA_CTL_NO                   " & vbNewLine _
                                     & ",@OUTKA_CTL_NO_CHU               " & vbNewLine _
                                     & ",@CUST_CD_L                      " & vbNewLine _
                                     & ",@CUST_CD_M                      " & vbNewLine _
                                     & ",@E1EDK01_ACTION                       " & vbNewLine _
                                     & ",@E1EDK01_CURCY                        " & vbNewLine _
                                     & ",@E1EDK01_LIFSK                        " & vbNewLine _
                                     & ",@E1EDK02_QUALF_BELNR_002              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_012              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_008              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_007              " & vbNewLine _
                                     & ",@E1EDK14_QUALF_ORGID_006              " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_LIFNR_AG               " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_LIFNR_WE               " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_PARTN_ZZ               " & vbNewLine _
                                     & ",@E1EDK03_IDDAT_DATUM_001              " & vbNewLine _
                                     & ",@E1EDKT1_TDID                         " & vbNewLine _
                                     & ",@E1EDKT1_TSSPRAS                      " & vbNewLine _
                                     & ",@E1EDKT1_TDOBJECT                     " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5    " & vbNewLine _
                                     & ",@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6    " & vbNewLine _
                                     & ",@E1EDK02_QUALF_BELNR_001              " & vbNewLine _
                                     & ",@E1EDK02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",@E1EDP01_POSEX                        " & vbNewLine _
                                     & ",@E1EDP01_MATNR                        " & vbNewLine _
                                     & ",@E1EDP01_MENGE                        " & vbNewLine _
                                     & ",@E1EDP01_MENEE                        " & vbNewLine _
                                     & ",@E1EDP01_WERKS                        " & vbNewLine _
                                     & ",@E1EDP01_VSTEL                        " & vbNewLine _
                                     & ",@E1EDP01_LGORT                        " & vbNewLine _
                                     & ",@E1EDP02_QUALF_BELNR_043              " & vbNewLine _
                                     & ",@E1EDP02_QUALF_ZEILE_043              " & vbNewLine _
                                     & ",@E1EDP02_QUALF_BELNR_044              " & vbNewLine _
                                     & ",@E1EDP03_QUALF_DATE_010               " & vbNewLine _
                                     & ",@E1EDP03_QUALF_DATE_024               " & vbNewLine _
                                     & ",@E1EDP20_WMENG_EDATU                  " & vbNewLine _
                                     & ",@E1EDP19_QUALF_IDTNR_002              " & vbNewLine _
                                     & ",@E1EDP19_QUALF_IDTNR_010              " & vbNewLine _
                                     & ",@E1EDKA1_PARVW_PARTN_DUMMY            " & vbNewLine _
                                     & ",@E1EDKA1_NAME1                        " & vbNewLine _
                                     & ",@E1EDKA1_NAME2                        " & vbNewLine _
                                     & ",@E1EDKA1_NAME3                        " & vbNewLine _
                                     & ",@E1EDKA1_NAME4                        " & vbNewLine _
                                     & ",@E1EDKA1_STRAS                        " & vbNewLine _
                                     & ",@E1EDKA1_STRS2                        " & vbNewLine _
                                     & ",@E1EDKA1_ORT01                        " & vbNewLine _
                                     & ",@E1EDKA1_PSTLZ                        " & vbNewLine _
                                     & ",@E1EDKA1_LAND1                        " & vbNewLine _
                                     & ",@E1EDKA1_TELF1                        " & vbNewLine _
                                     & ",@SAMPLE_HOUKOKU_FLG                   " & vbNewLine _
                                     & ",@RECORD_STATUS                  " & vbNewLine _
                                     & ",@JISSEKI_SHORI_FLG              " & vbNewLine _
                                     & ",@JISSEKI_USER                   " & vbNewLine _
                                     & ",@JISSEKI_DATE                   " & vbNewLine _
                                     & ",@JISSEKI_TIME                   " & vbNewLine _
                                     & ",@SEND_USER                      " & vbNewLine _
                                     & ",@SEND_DATE                      " & vbNewLine _
                                     & ",@SEND_TIME                      " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_SHORI_FLG         " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_USER              " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_DATE              " & vbNewLine _
                                     & ",@SAMPLEHOUKOKU_TIME              " & vbNewLine _
                                     & ",@DELETE_USER                    " & vbNewLine _
                                     & ",@DELETE_DATE                    " & vbNewLine _
                                     & ",@DELETE_TIME                    " & vbNewLine _
                                     & ",@DELETE_EDI_NO                  " & vbNewLine _
                                     & ",@DELETE_EDI_NO_CHU              " & vbNewLine _
                                     & ",@UPD_USER                       " & vbNewLine _
                                     & ",@UPD_DATE                       " & vbNewLine _
                                     & ",@UPD_TIME                       " & vbNewLine _
                                     & ",@SYS_ENT_DATE                   " & vbNewLine _
                                     & ",@SYS_ENT_TIME                   " & vbNewLine _
                                     & ",@SYS_ENT_PGID                   " & vbNewLine _
                                     & ",@SYS_ENT_USER                   " & vbNewLine _
                                     & ",@SYS_UPD_DATE                   " & vbNewLine _
                                     & ",@SYS_UPD_TIME                   " & vbNewLine _
                                     & ",@SYS_UPD_PGID                   " & vbNewLine _
                                     & ",@SYS_UPD_USER                   " & vbNewLine _
                                     & ",@SYS_DEL_FLG                    " & vbNewLine _
                                     & ")                                " & vbNewLine



#End Region

#Region "C_OUTKA_L"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
                                              & " OUTKA_STATE_KB    = '90'                            " & vbNewLine _
                                              & ",HOKOKU_DATE       = @SYS_UPD_DATE                   " & vbNewLine _
                                              & ",HOU_USER          = @SYS_UPD_USER                   " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                   " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                   " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                   " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                   " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                      " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_CTL_NO                   " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                           " & vbNewLine
#End Region

#End Region
    '2013.07.30 追加END

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
    ''' 出荷報告作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷報告作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectOutkaHokoku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC860IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC860DAC.SQL_SELECT_BYKAGT_SEND_DATA)
        Me._StrSql.Append(LMC860DAC.SQL_FROM_BYKAGT_SEND_DATA)
        Call setSQLSelect()                   '条件設定
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC860DAC", "SelectOutkaHokoku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("E1EDK01_ACTION", "E1EDK01_ACTION")
        map.Add("E1EDK01_CURCY", "E1EDK01_CURCY")
        map.Add("E1EDK01_LIFSK", "E1EDK01_LIFSK")
        map.Add("E1EDK02_QUALF_BELNR_002", "E1EDK02_QUALF_BELNR_002")
        map.Add("E1EDK14_QUALF_ORGID_012", "E1EDK14_QUALF_ORGID_012")
        map.Add("E1EDK14_QUALF_ORGID_008", "E1EDK14_QUALF_ORGID_008")
        map.Add("E1EDK14_QUALF_ORGID_007", "E1EDK14_QUALF_ORGID_007")
        map.Add("E1EDK14_QUALF_ORGID_006", "E1EDK14_QUALF_ORGID_006")
        map.Add("E1EDKA1_PARVW_LIFNR_AG", "E1EDKA1_PARVW_LIFNR_AG")
        map.Add("E1EDKA1_PARVW_LIFNR_WE", "E1EDKA1_PARVW_LIFNR_WE")
        map.Add("E1EDKA1_PARVW_PARTN_ZZ", "E1EDKA1_PARVW_PARTN_ZZ")
        map.Add("E1EDK03_IDDAT_DATUM_001", "E1EDK03_IDDAT_DATUM_001")
        map.Add("E1EDKT1_TDID", "E1EDKT1_TDID")
        map.Add("E1EDKT1_TSSPRAS", "E1EDKT1_TSSPRAS")
        map.Add("E1EDKT1_TDOBJECT", "E1EDKT1_TDOBJECT")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5")
        map.Add("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6", "E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6")
        map.Add("E1EDK02_QUALF_BELNR_001", "E1EDK02_QUALF_BELNR_001")
        map.Add("E1EDK02_QUALF_BELNR_043", "E1EDK02_QUALF_BELNR_043")
        map.Add("E1EDP01_POSEX", "E1EDP01_POSEX")
        map.Add("E1EDP01_MATNR", "E1EDP01_MATNR")
        map.Add("E1EDP01_MENGE", "E1EDP01_MENGE")
        map.Add("E1EDP01_MENEE", "E1EDP01_MENEE")
        map.Add("E1EDP01_WERKS", "E1EDP01_WERKS")
        map.Add("E1EDP01_VSTEL", "E1EDP01_VSTEL")
        map.Add("E1EDP01_LGORT", "E1EDP01_LGORT")
        map.Add("E1EDP02_QUALF_BELNR_043", "E1EDP02_QUALF_BELNR_043")
        map.Add("E1EDP02_QUALF_ZEILE_043", "E1EDP02_QUALF_ZEILE_043")
        map.Add("E1EDP02_QUALF_BELNR_044", "E1EDP02_QUALF_BELNR_044")
        map.Add("E1EDP03_QUALF_DATE_010", "E1EDP03_QUALF_DATE_010")
        map.Add("E1EDP03_QUALF_DATE_024", "E1EDP03_QUALF_DATE_024")
        map.Add("E1EDP20_WMENG_EDATU", "E1EDP20_WMENG_EDATU")
        map.Add("E1EDP19_QUALF_IDTNR_002", "E1EDP19_QUALF_IDTNR_002")
        map.Add("E1EDP19_QUALF_IDTNR_010", "E1EDP19_QUALF_IDTNR_010")
        '要望番号2091 追加START 2013.10.24
        map.Add("E1EDKA1_PARVW_PARTN_DUMMY", "E1EDKA1_PARVW_PARTN_DUMMY")
        map.Add("E1EDKA1_NAME1", "E1EDKA1_NAME1")
        map.Add("E1EDKA1_NAME2", "E1EDKA1_NAME2")
        map.Add("E1EDKA1_NAME3", "E1EDKA1_NAME3")
        map.Add("E1EDKA1_NAME4", "E1EDKA1_NAME4")
        map.Add("E1EDKA1_STRAS", "E1EDKA1_STRAS")
        map.Add("E1EDKA1_STRS2", "E1EDKA1_STRS2")
        map.Add("E1EDKA1_ORT01", "E1EDKA1_ORT01")
        map.Add("E1EDKA1_PSTLZ", "E1EDKA1_PSTLZ")
        map.Add("E1EDKA1_LAND1", "E1EDKA1_LAND1")
        map.Add("E1EDKA1_TELF1", "E1EDKA1_TELF1")
        map.Add("SAMPLE_HOUKOKU_FLG", "SAMPLE_HOUKOKU_FLG")
        '要望番号2091 追加END 2013.10.24
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")

        map.Add("SAMPLEHOUKOKU_SHORI_FLG", "SAMPLEHOUKOKU_SHORI_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC860OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC860OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（出荷報告作成時ステージUP）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC860OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me._Row("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC860DAC.SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC860DAC", "UpdateOutkaData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "H_SENDOUTEDI_BYKAGT"

    ''' <summary>
    ''' BYK(代理店用)EDI実績テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>BYK(代理店用)EDI実績テーブル更新SQLの構築・発行</remarks>
    Private Function InsertSendOutBykAgtData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dowSendTbl As DataTable = ds.Tables("LMC860OUT")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMC860DAC.SQL_INSERT_H_SENDOUTEDI_BYKAGT _
                                                                       , ds.Tables("LMC860OUT").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = dowSendTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = dowSendTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter()
            Call Me.SetEdiSendAgtCreateParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC860DAC", "InsertSendOutBykAgtData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#Region "EDI送信(TBL)新規登録パラメータ設定"

    ''' <summary>
    ''' EDI送信(TBL)の新規登録パラメータ設定(BYK(テツタニ(代理店)))
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendAgtCreateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Dim updTimeNormal As String = MyBase.GetSystemTime().Substring(0, 6)

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(.Item("GYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", Me.NullConvertString(.Item("OUTKA_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK01_ACTION", Me.NullConvertString(.Item("E1EDK01_ACTION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK01_CURCY", Me.NullConvertString(.Item("E1EDK01_CURCY")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK01_LIFSK", Me.NullConvertString(.Item("E1EDK01_LIFSK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK02_QUALF_BELNR_002", Me.NullConvertString(.Item("E1EDK02_QUALF_BELNR_002")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_012", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_012")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_008", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_008")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_007", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_007")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK14_QUALF_ORGID_006", Me.NullConvertString(.Item("E1EDK14_QUALF_ORGID_006")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_LIFNR_AG", Me.NullConvertString(.Item("E1EDKA1_PARVW_LIFNR_AG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_LIFNR_WE", Me.NullConvertString(.Item("E1EDKA1_PARVW_LIFNR_WE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_PARTN_ZZ", Me.NullConvertString(.Item("E1EDKA1_PARVW_PARTN_ZZ")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK03_IDDAT_DATUM_001", Me.NullConvertString(.Item("E1EDK03_IDDAT_DATUM_001")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT1_TDID", Me.NullConvertString(.Item("E1EDKT1_TDID")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT1_TSSPRAS", Me.NullConvertString(.Item("E1EDKT1_TSSPRAS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT1_TDOBJECT", Me.NullConvertString(.Item("E1EDKT1_TDOBJECT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6", Me.NullConvertString(.Item("E1EDKT2_TDLINE_TDFORMAT_TEXTLINE6")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK02_QUALF_BELNR_001", Me.NullConvertString(.Item("E1EDK02_QUALF_BELNR_001")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDK02_QUALF_BELNR_043", Me.NullConvertString(.Item("E1EDK02_QUALF_BELNR_043")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_POSEX", Me.NullConvertString(.Item("E1EDP01_POSEX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_MATNR", Me.NullConvertString(.Item("E1EDP01_MATNR")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_MENGE", Me.NullConvertString(.Item("E1EDP01_MENGE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_MENEE", Me.NullConvertString(.Item("E1EDP01_MENEE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_WERKS", Me.NullConvertString(.Item("E1EDP01_WERKS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_VSTEL", Me.NullConvertString(.Item("E1EDP01_VSTEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP01_LGORT", Me.NullConvertString(.Item("E1EDP01_LGORT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP02_QUALF_BELNR_043", Me.NullConvertString(.Item("E1EDP02_QUALF_BELNR_043")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP02_QUALF_ZEILE_043", Me.NullConvertString(.Item("E1EDP02_QUALF_ZEILE_043")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP02_QUALF_BELNR_044", Me.NullConvertString(.Item("E1EDP02_QUALF_BELNR_044")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP03_QUALF_DATE_010", Me.NullConvertString(.Item("E1EDP03_QUALF_DATE_010")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP03_QUALF_DATE_024", Me.NullConvertString(.Item("E1EDP03_QUALF_DATE_024")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP20_WMENG_EDATU", Me.NullConvertString(.Item("E1EDP20_WMENG_EDATU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP19_QUALF_IDTNR_002", Me.NullConvertString(.Item("E1EDP19_QUALF_IDTNR_002")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDP19_QUALF_IDTNR_010", Me.NullConvertString(.Item("E1EDP19_QUALF_IDTNR_010")), DBDataType.NVARCHAR))
            '要望番号2091 追加START 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PARVW_PARTN_DUMMY", Me.NullConvertString(.Item("E1EDKA1_PARVW_PARTN_DUMMY")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME1", Me.NullConvertString(.Item("E1EDKA1_NAME1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME2", Me.NullConvertString(.Item("E1EDKA1_NAME2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME3", Me.NullConvertString(.Item("E1EDKA1_NAME3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_NAME4", Me.NullConvertString(.Item("E1EDKA1_NAME4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_STRAS", Me.NullConvertString(.Item("E1EDKA1_STRAS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_STRS2", Me.NullConvertString(.Item("E1EDKA1_STRS2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_ORT01", Me.NullConvertString(.Item("E1EDKA1_ORT01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_PSTLZ", Me.NullConvertString(.Item("E1EDKA1_PSTLZ")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_LAND1", Me.NullConvertString(.Item("E1EDKA1_LAND1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@E1EDKA1_TELF1", Me.NullConvertString(.Item("E1EDKA1_TELF1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLE_HOUKOKU_FLG", Me.NullConvertString(.Item("SAMPLE_HOUKOKU_FLG")), DBDataType.NVARCHAR))
            '要望番号2091 追加END 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(.Item("RECORD_STATUS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.NullConvertString(.Item("SEND_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.NullConvertString(.Item("SEND_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", Me.NullConvertString(.Item("SEND_TIME")), DBDataType.NVARCHAR))
            '要望番号2091 追加START 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_SHORI_FLG", Me.NullConvertString(.Item("SAMPLEHOUKOKU_SHORI_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAMPLEHOUKOKU_TIME", updTime, DBDataType.NVARCHAR))
            '要望番号2091 追加END 2013.10.24
            prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", Me.NullConvertString(.Item("DELETE_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", Me.NullConvertString(.Item("DELETE_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", Me.NullConvertString(.Item("DELETE_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", Me.NullConvertString(.Item("DELETE_EDI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", Me.NullConvertString(.Item("DELETE_EDI_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

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

    ''' <summary>
    '''  パラメータ設定モジュール（出荷検索）
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me._Row("OUTKA_PLAN_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter()

        'システム項目
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUp()

    End Sub

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function

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

#Region "時間コロン編集"
    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function
#End Region


#End Region

End Class
