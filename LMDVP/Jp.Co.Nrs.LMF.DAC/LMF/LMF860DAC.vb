' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF860    : 名鉄CSV
'  作  成  者       :  [UMANO]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF860DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF860DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_CSV As String = " SELECT                                                                                      " & vbNewLine _
                                                       & "  UNSOL.NRS_BR_CD AS NRS_BR_CD                                                           " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L AS DENPYO_NO                                                           " & vbNewLine _
                                                       & " ,SUBSTRING(UNSOL.OUTKA_PLAN_DATE,1,4) +                                                 " & vbNewLine _
                                                       & "  '/' +                                                                                  " & vbNewLine _
                                                       & "  SUBSTRING(UNSOL.OUTKA_PLAN_DATE,5,2) +                                                 " & vbNewLine _
                                                       & "  '/' +                                                                                  " & vbNewLine _
                                                       & "  SUBSTRING(UNSOL.OUTKA_PLAN_DATE,7,2) AS SYUKKABI                                       " & vbNewLine _
                                                       & " --,OKURIJOCSV.FREE_C01 AS NIOKURININ_CD --旧                                            " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定開始              " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C01                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C01                                                         " & vbNewLine _
                                                       & "  END                                   AS NIOKURININ_CD                                 " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定終了              " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM AS NIOKURININ_MEI1                                                     " & vbNewLine _
                                                       & " ,'' AS NIOKURININ_MEI2                                                                  " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.ZIP                                                                 " & vbNewLine _
                                                       & "       ELSE NRSBR.ZIP                                                                    " & vbNewLine _
                                                       & "  END AS NIOKURININ_ZIP                                                                  " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.AD_1                                                                " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_1                                                                   " & vbNewLine _
                                                       & "  END AS NIOKURININ_ADD1                                                                 " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.AD_2                                                                " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_2                                                                   " & vbNewLine _
                                                       & "  END AS NIOKURININ_ADD2                                                                 " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.AD_3                                                                " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_3                                                                   " & vbNewLine _
                                                       & "  END AS NIOKURININ_ADD3                                                                 " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.TEL                                                                 " & vbNewLine _
                                                       & "       ELSE NRSBR.TEL                                                                    " & vbNewLine _
                                                       & "  END AS NIOKURININ_TEL                                                                  " & vbNewLine _
                                                       & " --,OKURIJOCSV.FREE_C02 AS SHIHARAININ_CD --旧                                           " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定開始              " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                       & "   END                             AS SHIHARAININ_CD                                     " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定終了              " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                       & "       ELSE UNSOL.DEST_CD                                                                " & vbNewLine _
                                                       & "  END AS NIUKENIN_CD                                                                     " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.DEST_NM                                                                " & vbNewLine _
                                                       & "       ELSE DEST.DEST_NM                                                                 " & vbNewLine _
                                                       & "  END AS NIUKENIN_NM1                                                                    " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_NM2                                                                     " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.ZIP                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.ZIP                                                                     " & vbNewLine _
                                                       & "  END AS NIUKENIN_ZIP                                                                    " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.AD_1 + ' ' +                                                           " & vbNewLine _
                                                       & "            DEST2.AD_2 + ' ' +                                                           " & vbNewLine _
                                                       & "            UNSOL.AD_3                                                                   " & vbNewLine _
                                                       & "       ELSE DEST.AD_1 + ' ' +                                                            " & vbNewLine _
                                                       & "            DEST.AD_2 + ' ' +                                                            " & vbNewLine _
                                                       & "            UNSOL.AD_3                                                                   " & vbNewLine _
                                                       & "  END AS NIUKENIN_ADD1                                                                   " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_ADD2                                                                    " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_ADD3                                                                    " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.TEL                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.TEL                                                                     " & vbNewLine _
                                                       & "  END AS NIUKENIN_TEL                                                                    " & vbNewLine _
                                                       & " ,SUBSTRING(UNSOL.ARR_PLAN_DATE,5,2) +                                                   " & vbNewLine _
                                                       & "  '月' +                                                                                 " & vbNewLine _
                                                       & "  SUBSTRING(UNSOL.ARR_PLAN_DATE,7,2) +                                                   " & vbNewLine _
                                                       & "  '日'  AS HAISOBI                                                                       " & vbNewLine _
                                                       & " ,DEST3.DEST_NM AS SHIP_NM_L                                                             " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM AS DENPYO_NM                                                            " & vbNewLine _
                                                       & " --,OKURIJOCSV.FREE_C03 AS KIJI_1 --旧                                                   " & vbNewLine _
                                                       & " --,OKURIJOCSV.FREE_C04 AS KIJI_2 --旧                                                   " & vbNewLine _
                                                       & " --,OKURIJOCSV.FREE_C05 AS KIJI_3 --旧                                                   " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定開始              " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C03                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C03                                                         " & vbNewLine _
                                                       & "  END                      AS KIJI_1                                                     " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C04                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C04                                                         " & vbNewLine _
                                                       & "  END                      AS KIJI_2                                                     " & vbNewLine _
                                                       & " ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
                                                       & "       THEN OKURIJOCSV.FREE_C05                                                          " & vbNewLine _
                                                       & "       ELSE OKURIJOCSVX.FREE_C05                                                         " & vbNewLine _
                                                       & "  END                      AS KIJI_3                                                     " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定終了              " & vbNewLine _
                                                       & " ,'' AS KIJI_4                                                                           " & vbNewLine _
                                                       & " ,UNSOL.REMARK AS KIJI_5                                                                 " & vbNewLine _
                                                       & " ,'' AS KIJI_6                                                                           " & vbNewLine _
                                                       & " ,UNSOL.CUST_REF_NO AS KIJI_7                                                            " & vbNewLine _
                                                       & " --(2013.03.11)要望番号1930 記事8に買主注文番号を設定 -- START --                        " & vbNewLine _
                                                       & " --,'' AS KIJI_8                                                                         " & vbNewLine _
                                                       & " ,UNSOL.BUY_CHU_NO AS KIJI_8                                                             " & vbNewLine _
                                                       & " --(2013.03.11)要望番号1930 記事8に買主注文番号を設定 --  END  --                        " & vbNewLine _
                                                       & " ,UNSOL.UNSO_PKG_NB AS KOSU                                                              " & vbNewLine _
                                                       & " ,'0' AS PARETTOSU                                                                       " & vbNewLine _
                                                       & " ,FLOOR(UNSOL.UNSO_WT) AS JYURYO                                                         " & vbNewLine _
                                                       & " ,'0' AS YOSEKI                                                                          " & vbNewLine _
                                                       & " ,'0' AS HOKENRYOU                                                                       " & vbNewLine _
                                                       & " ,UNSOL.CUST_CD_L AS CUST_CD_L                                                           " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.JIS                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.JIS                                                                     " & vbNewLine _
                                                       & "  END AS JIS                                                                             " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.UNCHIN_SEIQTO_CD                                                       " & vbNewLine _
                                                       & "       ELSE DEST.UNCHIN_SEIQTO_CD                                                        " & vbNewLine _
                                                       & "  END AS UNCHIN_SEIQTO_CD                                                                " & vbNewLine _
                                                       & " ,@ROW_NO AS ROW_NO                                                                      " & vbNewLine _
                                                       & " ,UNSOL.SYS_UPD_DATE AS SYS_UPD_DATE                                                     " & vbNewLine _
                                                       & " ,UNSOL.SYS_UPD_TIME AS SYS_UPD_TIME                                                     " & vbNewLine _
                                                       & " ,@FILEPATH AS FILEPATH                                                                  " & vbNewLine _
                                                       & " ,@FILENAME AS FILENAME                                                                  " & vbNewLine _
                                                       & " ,@SYS_DATE AS SYS_DATE                                                                  " & vbNewLine _
                                                       & " ,@SYS_TIME AS SYS_TIME                                                                  " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L AS CUST_NM_L                                                            " & vbNewLine

    ''' <summary>
    ''' 名鉄CSV作成データ検索用SQL FROM・WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MEITETU_CSV_FROM As String = " FROM $LM_TRN$..F_UNSO_L UNSOL                                                        " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM ON                                                 " & vbNewLine _
                                                       & " UNSOL.NRS_BR_CD = UNSOM.NRS_BR_CD AND                                                 " & vbNewLine _
                                                       & " UNSOL.UNSO_NO_L = UNSOM.UNSO_NO_L AND                                                 " & vbNewLine _
                                                       & " UNSOM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_NRS_BR NRSBR ON                                                 " & vbNewLine _
                                                       & " NRSBR.NRS_BR_CD = UNSOL.NRS_BR_CD                                                     " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                    " & vbNewLine _
                                                       & " DEST.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " DEST.CUST_CD_L = UNSOL.CUST_CD_L AND                                                  " & vbNewLine _
                                                       & " DEST.DEST_CD = UNSOL.DEST_CD                                                          " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST2 ON                                                   " & vbNewLine _
                                                       & " DEST2.NRS_BR_CD = DEST.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " DEST2.CUST_CD_L = DEST.CUST_CD_L AND                                                  " & vbNewLine _
                                                       & " DEST2.DEST_CD = DEST.CUST_DEST_CD                                                     " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST3 ON                                                   " & vbNewLine _
                                                       & " DEST3.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                                 " & vbNewLine _
                                                       & " DEST3.CUST_CD_L = UNSOL.CUST_CD_L AND                                                 " & vbNewLine _
                                                       & " DEST3.DEST_CD = UNSOL.SHIP_CD                                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DESTORIG ON                                                " & vbNewLine _
                                                       & " DESTORIG.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                              " & vbNewLine _
                                                       & " DESTORIG.CUST_CD_L = UNSOL.CUST_CD_L AND                                              " & vbNewLine _
                                                       & " DESTORIG.DEST_CD = UNSOL.ORIG_CD                                                      " & vbNewLine _
                                                       & " -- LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV ON  --旧                              " & vbNewLine _
                                                       & " -- OKURIJOCSV.NRS_BR_CD  = UNSOL.NRS_BR_CD AND                                        " & vbNewLine _
                                                       & " -- OKURIJOCSV.UNSOCO_CD  = UNSOL.UNSO_CD   AND                                        " & vbNewLine _
                                                       & " -- OKURIJOCSV.CUST_CD_L  = UNSOL.CUST_CD_L AND                                        " & vbNewLine _
                                                       & " -- OKURIJOCSV.OKURIJO_TP = '01'                                                       " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定開始            " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV ON   --既存マスタJOIN                    " & vbNewLine _
                                                       & " OKURIJOCSV.NRS_BR_CD   = UNSOL.NRS_BR_CD AND                                          " & vbNewLine _
                                                       & " OKURIJOCSV.UNSOCO_CD   = UNSOL.UNSO_CD   AND                                          " & vbNewLine _
                                                       & " OKURIJOCSV.CUST_CD_L   = UNSOL.CUST_CD_L AND                                          " & vbNewLine _
                                                       & " OKURIJOCSV.OKURIJO_TP  = '01'                                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX ON  --追加マスタJOIN                    " & vbNewLine _
                                                       & " OKURIJOCSVX.NRS_BR_CD  = UNSOL.NRS_BR_CD AND                                          " & vbNewLine _
                                                       & " OKURIJOCSVX.UNSOCO_CD  = UNSOL.UNSO_CD   AND                                          " & vbNewLine _
                                                       & " OKURIJOCSVX.CUST_CD_L  = 'XXXXX'         AND                                          " & vbNewLine _
                                                       & " OKURIJOCSVX.OKURIJO_TP = '01'                                                         " & vbNewLine _
                                                       & " --★2013.02.26 / Notes1896 荷主コードが見当たらない場合強制'XXXXX'指定終了            " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                    " & vbNewLine _
                                                       & " CUST.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " CUST.CUST_CD_L = UNSOL.CUST_CD_L AND                                                  " & vbNewLine _
                                                       & " CUST.CUST_CD_M = UNSOL.CUST_CD_M AND                                                  " & vbNewLine _
                                                       & " CUST.CUST_CD_S = '00' AND                                                             " & vbNewLine _
                                                       & " CUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
                                                       & " WHERE UNSOL.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                                       & " AND UNSOL.UNSO_NO_L = @UNSO_NO_L                                                      " & vbNewLine _
                                                       & " AND UNSOL.SYS_DEL_FLG = '0'                                                           " & vbNewLine

    ' ''' <summary>
    ' ''' 名鉄CSV作成データ検索用SQL GROUP BY部
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Const SQL_SELECT_MEITETU_CSV_GROUPBY As String = " GROUP BY                                                                            " & vbNewLine _
    '                                                   & "  UNSOL.NRS_BR_CD                                                                       " & vbNewLine _
    '                                                   & " ,UNSOL.UNSO_NO_L                                                                      " & vbNewLine _
    '                                                   & " ,UNSOL.OUTKA_PLAN_DATE                                                                 " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.FREE_C01                                                                    " & vbNewLine _
    '                                                   & " ,NRSBR.NRS_BR_NM                                                                        " & vbNewLine _
    '                                                   & " ,NRSBR.ZIP                                                                              " & vbNewLine _
    '                                                   & " ,NRSBR.AD_1                                                                             " & vbNewLine _
    '                                                   & " ,NRSBR.AD_2                                                                             " & vbNewLine _
    '                                                   & " ,NRSBR.AD_3                                                                             " & vbNewLine _
    '                                                   & " ,NRSBR.TEL                                                                              " & vbNewLine _
    '                                                   & " ,SOKO.ZIP                                                                               " & vbNewLine _
    '                                                   & " ,SOKO.AD_1                                                                              " & vbNewLine _
    '                                                   & " ,SOKO.AD_2                                                                              " & vbNewLine _
    '                                                   & " ,SOKO.AD_3                                                                              " & vbNewLine _
    '                                                   & " ,SOKO.TEL                                                                               " & vbNewLine _
    '                                                   & " ,SOKO.WH_KB                                                                             " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.FREE_C02                                                                    " & vbNewLine _
    '                                                   & " ,DEST.CUST_DEST_CD                                                                      " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_CD                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.DEST_NM                                                                          " & vbNewLine _
    '                                                   & " ,DEST.DEST_NM                                                                           " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_NM                                                                         " & vbNewLine _
    '                                                   & " ,EDIL.DEST_NM                                                                           " & vbNewLine _
    '                                                   & " ,DEST2.ZIP                                                                              " & vbNewLine _
    '                                                   & " ,DEST.ZIP                                                                               " & vbNewLine _
    '                                                   & " ,EDIL.DEST_ZIP                                                                          " & vbNewLine _
    '                                                   & " ,DEST2.AD_1                                                                             " & vbNewLine _
    '                                                   & " ,DEST.AD_1                                                                              " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_AD_1                                                                       " & vbNewLine _
    '                                                   & " ,EDIL.DEST_AD_1                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.AD_2                                                                             " & vbNewLine _
    '                                                   & " ,DEST.AD_2                                                                              " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_AD_2                                                                       " & vbNewLine _
    '                                                   & " ,EDIL.DEST_AD_2                                                                         " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_KB                                                                         " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_AD_3                                                                       " & vbNewLine _
    '                                                   & " ,EDIL.DEST_AD_3                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.TEL                                                                              " & vbNewLine _
    '                                                   & " ,DEST.TEL                                                                               " & vbNewLine _
    '                                                   & " ,UNSOL.DEST_TEL                                                                        " & vbNewLine _
    '                                                   & " ,EDIL.DEST_TEL                                                                          " & vbNewLine _
    '                                                   & " ,UNSOL.ARR_PLAN_DATE                                                                   " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.FREE_C03                                                                    " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.FREE_C04                                                                    " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.FREE_C05                                                                    " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.FREE_C06                                                                    " & vbNewLine _
    '                                                   & " ,UNSOL.REMARK                                                                           " & vbNewLine _
    '                                                   & " ,UNSOL.CUST_ORD_NO                                                                     " & vbNewLine _
    '                                                   & " ,UNSOL.UNSO_WT                                                                          " & vbNewLine _
    '                                                   & " ,UNSOL.CUST_CD_L                                                                       " & vbNewLine _
    '                                                   & " ,DEST2.JIS                                                                              " & vbNewLine _
    '                                                   & " ,DEST.JIS                                                                               " & vbNewLine _
    '                                                   & " ,DEST3.DEST_NM                                                                          " & vbNewLine _
    '                                                   & " ,CUST.DENPYO_NM                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.UNCHIN_SEIQTO_CD                                                                 " & vbNewLine _
    '                                                   & " ,DEST.UNCHIN_SEIQTO_CD                                                                  " & vbNewLine _
    '                                                   & " ,UNSOL.SYS_UPD_DATE                                                                    " & vbNewLine _
    '                                                   & " ,UNSOL.SYS_UPD_TIME                                                                    " & vbNewLine _
    '                                                   & " ,UNSOL.OUTKA_PKG_NB                                                                    " & vbNewLine _
    '                                                   & " ,CUST.CUST_NM_L                                                                         " & vbNewLine _
    '                                                   & " --★2013.02.26 / Notes1896 追加開始                                                     " & vbNewLine _
    '                                                   & " ,OKURIJOCSV.NRS_BR_CD                                                                   " & vbNewLine _
    '                                                   & " ,OKURIJOCSVX.FREE_C01                                                                   " & vbNewLine _
    '                                                   & " ,OKURIJOCSVX.FREE_C02                                                                   " & vbNewLine _
    '                                                   & " ,OKURIJOCSVX.FREE_C03                                                                   " & vbNewLine _
    '                                                   & " ,OKURIJOCSVX.FREE_C04                                                                   " & vbNewLine _
    '                                                   & " ,OKURIJOCSVX.FREE_C05                                                                   " & vbNewLine _
    '                                                   & " --★2013.02.26 / Notes1896 追加終了                                                     " & vbNewLine _
    '                                                   & " --(2013.03.11)要望番号1930 記事8に買主注文番号を設定 -- START --                        " & vbNewLine _
    '                                                   & " ,UNSOL.BUYER_ORD_NO                                                                    " & vbNewLine _
    '                                                   & " --(2013.03.11)要望番号1930 記事8に買主注文番号を設定 --  END  --                        " & vbNewLine

    Private Const SQL_SELECT_MEITETU_CSV_GROUPBY As String = " GROUP BY                                                                          " & vbNewLine _
                                                   & "  UNSOL.NRS_BR_CD                                                              " & vbNewLine _
                                                   & " ,UNSOL.UNSO_NO_L                                                              " & vbNewLine _
                                                   & " ,UNSOL.OUTKA_PLAN_DATE                                                        " & vbNewLine _
                                                   & " ,UNSOL.OUTKA_PLAN_DATE                                                        " & vbNewLine _
                                                   & " ,OKURIJOCSV.FREE_C01                                                          " & vbNewLine _
                                                   & " ,OKURIJOCSVX.FREE_C01                                                         " & vbNewLine _
                                                   & " ,NRSBR.NRS_BR_NM                                                              " & vbNewLine _
                                                   & " ,DESTORIG.ZIP                                                                 " & vbNewLine _
                                                   & " ,NRSBR.ZIP                                                                    " & vbNewLine _
                                                   & " ,DESTORIG.AD_1                                                                " & vbNewLine _
                                                   & " ,NRSBR.AD_1                                                                   " & vbNewLine _
                                                   & " ,DESTORIG.AD_2                                                                " & vbNewLine _
                                                   & " ,NRSBR.AD_2                                                                   " & vbNewLine _
                                                   & " ,DESTORIG.AD_3                                                                " & vbNewLine _
                                                   & " ,NRSBR.AD_3                                                                   " & vbNewLine _
                                                   & " ,DESTORIG.TEL                                                                 " & vbNewLine _
                                                   & " ,NRSBR.TEL                                                                    " & vbNewLine _
                                                   & " ,OKURIJOCSV.FREE_C02                                                          " & vbNewLine _
                                                   & " ,OKURIJOCSVX.FREE_C02                                                         " & vbNewLine _
                                                   & " ,DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                   & " ,UNSOL.DEST_CD                                                                " & vbNewLine _
                                                   & " ,DEST2.DEST_NM                                                                " & vbNewLine _
                                                   & " ,DEST.DEST_NM                                                                 " & vbNewLine _
                                                   & " ,DEST2.ZIP                                                                    " & vbNewLine _
                                                   & " ,DEST.ZIP                                                                     " & vbNewLine _
                                                   & " ,DEST2.AD_1                                                                   " & vbNewLine _
                                                   & " ,DEST2.AD_2                                                                   " & vbNewLine _
                                                   & " ,UNSOL.AD_3                                                                   " & vbNewLine _
                                                   & " ,DEST.AD_1                                                                    " & vbNewLine _
                                                   & " ,DEST.AD_2                                                                    " & vbNewLine _
                                                   & " ,DEST2.TEL                                                                    " & vbNewLine _
                                                   & " ,DEST.TEL                                                                     " & vbNewLine _
                                                   & " ,UNSOL.ARR_PLAN_DATE                                                          " & vbNewLine _
                                                   & " ,DEST3.DEST_NM                                                                " & vbNewLine _
                                                   & " ,CUST.DENPYO_NM                                                               " & vbNewLine _
                                                   & " ,OKURIJOCSV.NRS_BR_CD                                                         " & vbNewLine _
                                                   & " ,OKURIJOCSV.FREE_C03                                                          " & vbNewLine _
                                                   & " ,OKURIJOCSVX.FREE_C03                                                         " & vbNewLine _
                                                   & " ,OKURIJOCSV.FREE_C04                                                          " & vbNewLine _
                                                   & " ,OKURIJOCSVX.FREE_C04                                                         " & vbNewLine _
                                                   & " ,OKURIJOCSV.FREE_C05                                                          " & vbNewLine _
                                                   & " ,OKURIJOCSVX.FREE_C05                                                         " & vbNewLine _
                                                   & " ,UNSOL.REMARK                                                                 " & vbNewLine _
                                                   & " ,UNSOL.CUST_REF_NO                                                            " & vbNewLine _
                                                   & " ,UNSOL.BUY_CHU_NO                                                             " & vbNewLine _
                                                   & " ,UNSOL.UNSO_PKG_NB                                                            " & vbNewLine _
                                                   & " ,FLOOR(UNSOL.UNSO_WT)                                                         " & vbNewLine _
                                                   & " ,UNSOL.CUST_CD_L                                                              " & vbNewLine _
                                                   & " ,DEST2.JIS                                                                    " & vbNewLine _
                                                   & " ,DEST.JIS                                                                     " & vbNewLine _
                                                   & "-- ,DEST.DEST_CD                                                                 " & vbNewLine _
                                                   & " ,DESTORIG.DEST_CD                                                             " & vbNewLine _
                                                   & " ,DEST2.UNCHIN_SEIQTO_CD                                                       " & vbNewLine _
                                                   & " ,DEST.UNCHIN_SEIQTO_CD                                                        " & vbNewLine _
                                                   & " ,UNSOL.SYS_UPD_DATE                                                           " & vbNewLine _
                                                   & " ,UNSOL.SYS_UPD_TIME                                                           " & vbNewLine _
                                                   & " ,CUST.CUST_NM_L                                                               " & vbNewLine


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
    ''' 名鉄CSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>名鉄CSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectMeitetuCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF860IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF860DAC.SQL_SELECT_MEITETU_CSV)
        Me._StrSql.Append(LMF860DAC.SQL_SELECT_MEITETU_CSV_FROM)
        Me._StrSql.Append(LMF860DAC.SQL_SELECT_MEITETU_CSV_GROUPBY)
        Call setSQLSelect()                   '条件設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF860DAC", "SelectMeitetuCsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("SYUKKABI", "SYUKKABI")
        map.Add("NIOKURININ_CD", "NIOKURININ_CD")
        map.Add("NIOKURININ_MEI1", "NIOKURININ_MEI1")
        map.Add("NIOKURININ_MEI2", "NIOKURININ_MEI2")
        map.Add("NIOKURININ_ZIP", "NIOKURININ_ZIP")
        map.Add("NIOKURININ_ADD1", "NIOKURININ_ADD1")
        map.Add("NIOKURININ_ADD2", "NIOKURININ_ADD2")
        map.Add("NIOKURININ_ADD3", "NIOKURININ_ADD3")
        map.Add("NIOKURININ_TEL", "NIOKURININ_TEL")
        map.Add("SHIHARAININ_CD", "SHIHARAININ_CD")
        map.Add("NIUKENIN_CD", "NIUKENIN_CD")
        map.Add("NIUKENIN_NM1", "NIUKENIN_NM1")
        map.Add("NIUKENIN_NM2", "NIUKENIN_NM2")
        map.Add("NIUKENIN_ZIP", "NIUKENIN_ZIP")
        map.Add("NIUKENIN_ADD1", "NIUKENIN_ADD1")
        map.Add("NIUKENIN_ADD2", "NIUKENIN_ADD2")
        map.Add("NIUKENIN_ADD3", "NIUKENIN_ADD3")
        map.Add("NIUKENIN_TEL", "NIUKENIN_TEL")
        map.Add("HAISOBI", "HAISOBI")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("DENPYO_NM", "DENPYO_NM")
        map.Add("KIJI_1", "KIJI_1")
        map.Add("KIJI_2", "KIJI_2")
        map.Add("KIJI_3", "KIJI_3")
        map.Add("KIJI_4", "KIJI_4")
        map.Add("KIJI_5", "KIJI_5")
        map.Add("KIJI_6", "KIJI_6")
        map.Add("KIJI_7", "KIJI_7")
        map.Add("KIJI_8", "KIJI_8")
        map.Add("KOSU", "KOSU")
        map.Add("PARETTOSU", "PARETTOSU")
        map.Add("JYURYO", "JYURYO")
        map.Add("YOSEKI", "YOSEKI")
        map.Add("HOKENRYOU", "HOKENRYOU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("JIS", "JIS")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")
        map.Add("CUST_NM_L", "CUST_NM_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF860OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMF860OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row("UNSO_NO_L"), DBDataType.CHAR))

    End Sub

#End Region

#End Region


#End Region

End Class
