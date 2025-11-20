' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC890    : ヤマトB2 CSV出力
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC890DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC890DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#If False Then ' H_OUTKAEDI_Lカラム参照追加 20160830 changed inoue
#Else
    ''' <summary>
    ''' ヤマトCSV作成データ検索用SQL SELECT部 FFEM用
    ''' </summary>
    ''' Update 2016.08.12 
    ''' SQL_SELECT_YAMATOB2_CSV
    ''' DEST_NMがブランクの場合”.”をセットする。
    '''
    ''' <remarks></remarks>
    Private Const SQL_SELECT_YAMATOB2_CSV As String _
        = " SELECT                                                                                 " & vbNewLine _
        & "   NRS_BR_CD                                                                            " & vbNewLine _
        & "  ,DENPYO_NO                                                                            " & vbNewLine _
        & "  ,OKYAKU_KANRI_NO                                                                      " & vbNewLine _
        & "  ,OKURIJYO_PTN                                                                         " & vbNewLine _
        & "  ,COOL_KBN                                                                             " & vbNewLine _
        & "  ,DENPYO_BANGO                                                                         " & vbNewLine _
        & "  ,OUTKA_PLAN_DATE                                                                      " & vbNewLine _
        & "  ,ARR_PLAN_DATE                                                                        " & vbNewLine _
        & "  ,HAITATSU_TIME                                                                        " & vbNewLine _
        & "  ,DEST_CD                                                                              " & vbNewLine _
        & "  ,DEST_TEL                                                                             " & vbNewLine _
        & "  ,DEST_TEL_EDA                                                                         " & vbNewLine _
        & "  ,DEST_ZIP                                                                             " & vbNewLine _
        & "  ,DEST_ADD_1                                                                           " & vbNewLine _
        & "  ,DEST_TATEMONO_NM                                                                     " & vbNewLine _
        & " , DEST_BUMON1                                   AS DEST_BUMON1                         " & vbNewLine _
        & "  ,CASE WHEN DEST_COL1 <> 0 AND DEST_COL2 <> 0                                          " & vbNewLine _
        & "        THEN SUBSTRING(ZFVYDTEKIYO, 1, DEST_COL1 - 1)                                   " & vbNewLine _
        & "        ELSE ''                                                                         " & vbNewLine _
        & "   END                                           AS DEST_BUMON2                         " & vbNewLine _
        & "  ,CASE WHEN DEST_COL1 <> 0 AND DEST_COL2 <> 0                                          " & vbNewLine _
        & "        THEN CASE                                                                       " & vbNewLine _
        & "             WHEN SUBSTRING(ZFVYDTEKIYO, DEST_COL1 + 1, DEST_COL2 - 1) = '' THEN '.'    " & vbNewLine _
        & "                  ELSE SUBSTRING(ZFVYDTEKIYO, DEST_COL1 + 1, DEST_COL2 - 1)             " & vbNewLine _
        & "             END                                                                        " & vbNewLine _
        & "        ELSE '.'                                                                        " & vbNewLine _
        & "   END                                           AS  DEST_NM                            " & vbNewLine _
        & "  ,DET_NM_KANA                                                                          " & vbNewLine _
        & "  ,KEISYO                                                                               " & vbNewLine _
        & "  ,IRAINUSHI_CD                                                                         " & vbNewLine _
        & "  ,IRAINUSHI_TEL                                                                        " & vbNewLine _
        & "  ,IRAINUSHI_TEL_EDA                                                                    " & vbNewLine _
        & "  ,IRAINUSHI_ZIP                                                                        " & vbNewLine _
        & "  ,IRAINUSHI_ADD                                                                        " & vbNewLine _
        & "  ,IRAINUSHI_TATEMONO_NM                                                                " & vbNewLine _
        & "  ,IRAINUSHI_NM                                                                         " & vbNewLine _
        & "  ,IRAINUSHI_NM_KANA                                                                    " & vbNewLine _
        & "  ,GOODS_CD1                                                                            " & vbNewLine _
        & "  ,GOODS_NM1                                                                            " & vbNewLine _
        & "  ,GOODS_CD2                                                                            " & vbNewLine _
        & "  ,GOODS_NM2                                                                            " & vbNewLine _
        & "  ,NIATSUKAI1                                                                           " & vbNewLine _
        & "  ,NIATSUKAI2                                                                           " & vbNewLine _
        & "  ,KIJI                                                                                 " & vbNewLine _
        & "  ,COLLECT_AMT                                                                          " & vbNewLine _
        & "  ,COLLECT_TAX                                                                          " & vbNewLine _
        & "  ,TOMEOKI                                                                              " & vbNewLine _
        & "  ,BR_CD                                                                                " & vbNewLine _
        & "  ,PRINT_CNT                                                                            " & vbNewLine _
        & "  ,KOSU_DISP_FLG                                                                        " & vbNewLine _
        & "  ,SEIKYUSAKI_CD                                                                        " & vbNewLine _
        & "  ,SEIKYUSAKI_BUNRUI_CD                                                                 " & vbNewLine _
        & "  ,UNCHIN_KANRI_NO                                                                      " & vbNewLine _
        & "  ,CARD_HARAI                                                                           " & vbNewLine _
        & "  ,CARD_KAMEI_NO                                                                        " & vbNewLine _
        & "  ,CARD_NO1                                                                             " & vbNewLine _
        & "  ,CARD_NO2                                                                             " & vbNewLine _
        & "  ,CARD_NO3                                                                             " & vbNewLine _
        & "  ,EMAIL_KBN                                                                            " & vbNewLine _
        & "  ,EMAIL_ADDRESS                                                                        " & vbNewLine _
        & "  ,EMAIL_IN_TYPE                                                                        " & vbNewLine _
        & "  ,EMAIL_MSG                                                                            " & vbNewLine _
        & "  ,KANRYO_EMAIL_KBN                                                                     " & vbNewLine _
        & "  ,KANRYO_EMAIL_ADDRESS                                                                 " & vbNewLine _
        & "  ,KANRYO_EMAIL_MSG                                                                     " & vbNewLine _
        & "  ,SYUNOU_KBN                                                                           " & vbNewLine _
        & "  ,YOBI                                                                                 " & vbNewLine _
        & "  ,SYUNOU_SKY_AMT                                                                       " & vbNewLine _
        & "  ,SYUNOU_SKY_TAX                                                                       " & vbNewLine _
        & "  ,SYUNOU_SKY_ZIP                                                                       " & vbNewLine _
        & "  ,SYUNOU_SKY_ADD                                                                       " & vbNewLine _
        & "  ,SYUNOU_SKY_TATEMONO                                                                  " & vbNewLine _
        & "  ,SYUNOU_SKY_BUMON1                                                                    " & vbNewLine _
        & "  ,SYUNOU_SKY_BUMON2                                                                    " & vbNewLine _
        & "  ,SYUNOU_SKY_NM_KNJI                                                                   " & vbNewLine _
        & "  ,SYUNOU_SKY_NM_KANA                                                                   " & vbNewLine _
        & "  ,SYUNOU_TOI_NM_KANJI                                                                  " & vbNewLine _
        & "  ,SYUNOU_TOI_NM_ZIP                                                                    " & vbNewLine _
        & "  ,SYUNOU_TOI_ADD                                                                       " & vbNewLine _
        & "  ,SYUNOU_TOI_TATEMONO                                                                  " & vbNewLine _
        & "  ,SYUNOU_TOI_TEL                                                                       " & vbNewLine _
        & "  ,SYUNOU_DAI_KANRI_NO                                                                  " & vbNewLine _
        & "  ,SYUNOU_DAI_HINMEI                                                                    " & vbNewLine _
        & "  ,SYUNOU_DAI_BIKOU                                                                     " & vbNewLine _
        & "  ,ROW_NO                                                                               " & vbNewLine _
        & "  ,SYS_UPD_DATE                                                                         " & vbNewLine _
        & "  ,SYS_UPD_TIME                                                                         " & vbNewLine _
        & "  ,FILEPATH                                                                             " & vbNewLine _
        & "  ,FILENAME                                                                             " & vbNewLine _
        & "  ,SYS_DATE                                                                             " & vbNewLine _
        & "  ,SYS_TIME                                                                             " & vbNewLine _
        & "  ,CUST_NM_L                                                                            " & vbNewLine _
        & " FROM (                                                                                 " & vbNewLine _
        & " SELECT   TOP 1                                                                         " & vbNewLine _
        & "  OUTKAL.NRS_BR_CD	          AS NRS_BR_CD                                             " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L            AS DENPYO_NO                                             " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L            AS OKYAKU_KANRI_NO                                       " & vbNewLine _
        & " -- 元着区分                                                                            " & vbNewLine _
        & " ,CASE OUTKAL.PC_KB WHEN '01' THEN '0'                                                  " & vbNewLine _
        & "                    WHEN '02' THEN '5'                                                  " & vbNewLine _
        & "                    ELSE '0'  END       AS OKURIJYO_PTN                                 " & vbNewLine _
        & " ,CASE HIMOKU.ONDO_JOKEN WHEN 'ZA' THEN '1'                                             " & vbNewLine _
        & "                        WHEN 'ZB' THEN '2'                                              " & vbNewLine _
        & " 						ELSE '0'          END   AS COOL_KBN                            " & vbNewLine _
        & " ,''                           AS DENPYO_BANGO                                          " & vbNewLine _
        & " ,OUTKAL.OUTKA_PLAN_DATE       AS OUTKA_PLAN_DATE                                       " & vbNewLine _
        & " ,OUTKAL.ARR_PLAN_DATE         AS ARR_PLAN_DATE                                         " & vbNewLine _
        & " ,'0812'                       AS HAITATSU_TIME                                         " & vbNewLine _
        & " ,''                           AS DEST_CD                                               " & vbNewLine _
        & " ,OUTKAL.DEST_TEL              AS DEST_TEL                                              " & vbNewLine _
        & " ,''                           AS DEST_TEL_EDA                                          " & vbNewLine _
        & "  , CASE WHEN OUTKAL.DEST_KB = '02'                                                     " & vbNewLine _
        & "         THEN EDIL.DEST_ZIP                                                             " & vbNewLine _
        & "         ELSE DEST.ZIP                                                                  " & vbNewLine _
        & "         END                                                   AS DEST_ZIP              " & vbNewLine _
        & "  , CASE OUTKAL.DEST_KB WHEN '01'                                                       " & vbNewLine _
        & "                        THEN LTRIM(RTRIM(OUTKAL.DEST_AD_1))                             " & vbNewLine _
        & "                           + LTRIM(RTRIM(OUTKAL.DEST_AD_2))                             " & vbNewLine _
        & "                        WHEN '02'                                                       " & vbNewLine _
        & "                        THEN LTRIM(RTRIM(EDIL.DEST_AD_1))                               " & vbNewLine _
        & "                           + LTRIM(RTRIM(EDIL.DEST_AD_2))                               " & vbNewLine _
        & "                        ELSE LTRIM(RTRIM(DEST.AD_1))                                    " & vbNewLine _
        & "                           + LTRIM(RTRIM(DEST.AD_2))                                    " & vbNewLine _
        & "                        END                                    AS DEST_ADD_1            " & vbNewLine _
        & "  , OUTKAL.DEST_AD_3                                           AS DEST_TATEMONO_NM      " & vbNewLine _
        & "  , CASE OUTKAL.DEST_KB WHEN '01' THEN OUTKAL.DEST_NM                                   " & vbNewLine _
        & "                        WHEN '02' THEN EDIL.DEST_NM                                     " & vbNewLine _
        & "                        ELSE  DEST.DEST_NM END                 AS DEST_BUMON1           " & vbNewLine _
        & "  , CASE WHEN LEN(FJF.ZFVYDTEKIYO) = 0 THEN 0                                           " & vbNewLine _
        & "         ELSE CHARINDEX('|', FJF.ZFVYDTEKIYO)                                           " & vbNewLine _
        & "    END                                                        AS DEST_COL1             " & vbNewLine _
        & "  , CASE WHEN CHARINDEX('|', FJF.ZFVYDTEKIYO) = 0 THEN 0                                " & vbNewLine _
        & "         ELSE CHARINDEX('|', SUBSTRING(FJF.ZFVYDTEKIYO                                  " & vbNewLine _
        & "                                     , CHARINDEX('|', FJF.ZFVYDTEKIYO) + 1              " & vbNewLine _
        & "                                     , 100))                                            " & vbNewLine _
        & "    END                                                        AS DEST_COL2             " & vbNewLine _
        & "  ,OUTKAL.REMARK                AS REMARK   --出荷時注意事項                            " & vbNewLine _
        & "  , FJF.ZFVYDTEKIYO                AS ZFVYDTEKIYO                                       " & vbNewLine _
        & " ,''                           AS DET_NM_KANA                                           " & vbNewLine _
        & " ,''                           AS KEISYO                                                " & vbNewLine _
        & " ,''                           AS IRAINUSHI_CD                                          " & vbNewLine _
        & " ,NRSBR.TEL                    AS IRAINUSHI_TEL                                         " & vbNewLine _
        & " ,''                           AS IRAINUSHI_TEL_EDA                                     " & vbNewLine _
        & " ,NRSBR.ZIP                    AS IRAINUSHI_ZIP                                         " & vbNewLine _
        & " ,LTRIM(RTRIM(NRSBR.AD_1))                                                              " & vbNewLine _
        & "    + LTRIM(RTRIM(NRSBR.AD_2)) AS IRAINUSHI_ADD                                         " & vbNewLine _
        & " , CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                           " & vbNewLine _
        & "        THEN OKURIJOCSV.FREE_C03                                                        " & vbNewLine _
        & "        WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                          " & vbNewLine _
        & "        THEN OKURIJOCSVX.FREE_C03                                                       " & vbNewLine _
        & "        ELSE ''                                                                         " & vbNewLine _
        & "   END                                                    AS IRAINUSHI_TATEMONO_NM      " & vbNewLine _
        & " ,CUST.CUST_NM_L               AS IRAINUSHI_NM                                          " & vbNewLine _
        & " ,''                           AS IRAINUSHI_NM_KANA                                     " & vbNewLine _
        & " ,''                           AS GOODS_CD1                                             " & vbNewLine _
        & " ,OUTKAL.CUST_ORD_NO           AS GOODS_NM1  --UPD 2016/07/14                           " & vbNewLine _
        & " ,''                           AS GOODS_CD2                                             " & vbNewLine _
        & " ,''                           AS GOODS_NM2                                             " & vbNewLine _
        & " ,''                           AS NIATSUKAI1                                            " & vbNewLine _
        & " ,''                           AS NIATSUKAI2                                            " & vbNewLine _
        & " ,''                           AS KIJI                                                  " & vbNewLine _
        & " ,''                           AS COLLECT_AMT                                           " & vbNewLine _
        & " ,''                           AS COLLECT_TAX                                           " & vbNewLine _
        & " ,''                           AS TOMEOKI                                               " & vbNewLine _
        & " ,''                           AS BR_CD                                                 " & vbNewLine _
        & " ,OUTKAL.OUTKA_PKG_NB          AS PRINT_CNT                                             " & vbNewLine _
        & " ,''                           AS KOSU_DISP_FLG                                         " & vbNewLine _
        & " , CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                           " & vbNewLine _
        & "        THEN OKURIJOCSV.FREE_C02                                                        " & vbNewLine _
        & "        WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                          " & vbNewLine _
        & "        THEN OKURIJOCSVX.FREE_C02                                                       " & vbNewLine _
        & "        ELSE ''                                                                         " & vbNewLine _
        & "   END                                                    AS SEIKYUSAKI_CD              " & vbNewLine _
        & " ,''                           AS SEIKYUSAKI_BUNRUI_CD                                  " & vbNewLine _
        & " ,'01'                         AS UNCHIN_KANRI_NO                                       " & vbNewLine _
        & " ,''                           AS CARD_HARAI                                            " & vbNewLine _
        & " ,''                           AS CARD_KAMEI_NO                                         " & vbNewLine _
        & " ,''                           AS CARD_NO1                                              " & vbNewLine _
        & " ,''                           AS CARD_NO2                                              " & vbNewLine _
        & " ,''                           AS CARD_NO3                                              " & vbNewLine _
        & " ,''                           AS EMAIL_KBN                                             " & vbNewLine _
        & " ,''                           AS EMAIL_ADDRESS                                         " & vbNewLine _
        & " ,''                           AS EMAIL_IN_TYPE                                         " & vbNewLine _
        & " ,''                           AS EMAIL_MSG                                             " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_KBN                                      " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_ADDRESS                                  " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_MSG                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_KBN                                            " & vbNewLine _
        & " ,''                           AS YOBI                                                  " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_AMT                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_TAX                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_ZIP                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_ADD                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_TATEMONO                                   " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_BUMON1                                     " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_BUMON2                                     " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_NM_KNJI                                    " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_NM_KANA                                    " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_NM_KANJI                                   " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_NM_ZIP                                     " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_ADD                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_TATEMONO                                   " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_TEL                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_KANRI_NO                                   " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_HINMEI                                     " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_BIKOU                                      " & vbNewLine _
        & " ,@ROW_NO                      AS ROW_NO                                                " & vbNewLine _
        & " ,OUTKAL.SYS_UPD_DATE          AS SYS_UPD_DATE                                          " & vbNewLine _
        & " ,OUTKAL.SYS_UPD_TIME          AS SYS_UPD_TIME                                          " & vbNewLine _
        & " ,@FILEPATH                    AS FILEPATH                                              " & vbNewLine _
        & " ,@FILENAME                    AS FILENAME                                              " & vbNewLine _
        & " ,@SYS_DATE                    AS SYS_DATE                                              " & vbNewLine _
        & " ,@SYS_TIME                    AS SYS_TIME                                              " & vbNewLine _
        & " ,CUST.CUST_NM_L              AS CUST_NM_L                                              " & vbNewLine


    ''' <summary>
    ''' ヤマトCSV作成データ検索用SQL FROM・WHERE部 FFEM用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_YAMATOB2_CSV_FROM As String _
        = " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                      " & vbNewLine _
        & "      DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      DEST.DEST_CD = OUTKAL.DEST_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                      " & vbNewLine _
        & "      CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_S = '00' AND                                                          " & vbNewLine _
        & "      CUST.CUST_CD_SS = '00'                                                             " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_NRS_BR NRSBR ON                                                   " & vbNewLine _
        & "      NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                 " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
        & "           OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
        & "       AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
        & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                    " & vbNewLine _
        & "           GOODS.NRS_BR_CD    = OUTKAM.NRS_BR_CD                                         " & vbNewLine _
        & "       AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                      " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..M_HINMOKU_FJF HIMOKU ON                                             " & vbNewLine _
        & "           HIMOKU.NRS_BR_CD  = GOODS.NRS_BR_CD                                           " & vbNewLine _
        & "       AND HIMOKU.HINMOKU_CD = GOODS.GOODS_CD_CUST                                       " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST_DETAILS CUST_DET ON                                          " & vbNewLine _
        & "           CUST_DET.NRS_BR_CD  = OUTKAL.NRS_BR_CD                                        " & vbNewLine _
        & "       AND CUST_DET.CUST_CD    = OUTKAL.CUST_CD_L                                        " & vbNewLine _
        & "       AND CUST_DET.SUB_KB      = '0M'   --  ヤマトCSV出力FFEM専用出力                   " & vbNewLine _
        & "       AND CUST_DET.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "             SELECT                                                                      " & vbNewLine _
        & "                   NRS_BR_CD                                                             " & vbNewLine _
        & "                 , EDI_CTL_NO                                                            " & vbNewLine _
        & "                 , OUTKA_CTL_NO                                                          " & vbNewLine _
        & "             FROM (                                                                      " & vbNewLine _
        & "                     SELECT                                                              " & vbNewLine _
        & "                           EDIOUTL.NRS_BR_CD                                             " & vbNewLine _
        & "                         , EDIOUTL.EDI_CTL_NO                                            " & vbNewLine _
        & "                         , EDIOUTL.OUTKA_CTL_NO                                          " & vbNewLine _
        & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                    " & vbNewLine _
        & "                                ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD   " & vbNewLine _
        & "                                                                   , EDIOUTL.OUTKA_CTL_NO" & vbNewLine _
        & "                                                        ORDER BY EDIOUTL.NRS_BR_CD       " & vbNewLine _
        & "                                                               , EDIOUTL.EDI_CTL_NO      " & vbNewLine _
        & "                                                        )                                " & vbNewLine _
        & "                           END AS IDX                                                    " & vbNewLine _
        & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                 " & vbNewLine _
        & "                     WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                    " & vbNewLine _
        & "                       AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                             " & vbNewLine _
        & "                       AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                            " & vbNewLine _
        & "                   ) EBASE                                                               " & vbNewLine _
        & "             WHERE EBASE.IDX = 1                                                         " & vbNewLine _
        & "             ) TOPEDI                                                                    " & vbNewLine _
        & "         ON TOPEDI.NRS_BR_CD               = OUTKAL.NRS_BR_CD                            " & vbNewLine _
        & "        AND TOPEDI.OUTKA_CTL_NO            = OUTKAL.OUTKA_NO_L                           " & vbNewLine _
        & "                                                                                         " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                   " & vbNewLine _
        & "         ON EDIL.NRS_BR_CD                 = TOPEDI.NRS_BR_CD                            " & vbNewLine _
        & "        AND EDIL.EDI_CTL_NO                = TOPEDI.EDI_CTL_NO                           " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "             SELECT                                                                      " & vbNewLine _
        & "                    NRS_BR_CD         AS NRS_BR_CD                                       " & vbNewLine _
        & "                  , OUTKA_CTL_NO      AS OUTKA_CTL_NO                                    " & vbNewLine _
        & "                  , MAX(ZFVYDTEKIYO)  AS ZFVYDTEKIYO                                     " & vbNewLine _
        & "               FROM $LM_TRN$..H_INOUTKAEDI_HED_FJF                                       " & vbNewLine _
        & "              WHERE SYS_DEL_FLG     = '0'                                                " & vbNewLine _
        & "                AND DEL_KB          = '0'                                                " & vbNewLine _
        & "                AND INOUT_KB        = '0'                                                " & vbNewLine _
        & "                AND NRS_BR_CD       = @NRS_BR_CD                                         " & vbNewLine _
        & "                AND OUTKA_CTL_NO    = @OUTKA_NO_L                                        " & vbNewLine _
        & "              GROUP BY NRS_BR_CD                                                         " & vbNewLine _
        & "                     , OUTKA_CTL_NO                                                      " & vbNewLine _
        & "                                                                                         " & vbNewLine _
        & "            ) AS FJF                                                                     " & vbNewLine _
        & "   ON FJF.NRS_BR_CD    = TOPEDI.NRS_BR_CD                                                " & vbNewLine _
        & "  AND FJF.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                                               " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                      = OUTKAL.NRS_BR_CD                            " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                   = OUTKAL.OUTKA_NO_L                           " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                   = '20'                                        " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                    = '0'                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                             " & vbNewLine _
        & "   AND OKURIJOCSV.OKURIJO_TP               = '04' --ヤマト                               " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                     " & vbNewLine _
        & "   AND OKURIJOCSVX.OKURIJO_TP              = '04' --ヤマト                               " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST SHIP_CD_DEST                                                 " & vbNewLine _
        & "   ON SHIP_CD_DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.DEST_CD   = OUTKAL.SHIP_CD_L                                          " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD  = @NRS_BR_CD                                                    " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                                   " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "   AND CUST_DET.SET_NAIYO  = '1'   -- FFEM専用                                           " & vbNewLine _
        & " ) AS A                                                                                  " & vbNewLine

    '-----------------------------------------------------------------------
#End If



#Region "標準"

#If False Then ' H_OUTKAEDI_Lカラム参照追加 20160830 changed inoue
#Else

    ''' <summary>
    ''' ヤマトCSV作成データ検索用SQL SELECT部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_YAMATOB2_CSV2 As String _
        = " SELECT TOP(1)                                                                             " & vbNewLine _
        & "  OUTKAL.NRS_BR_CD	          AS NRS_BR_CD                                                " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L            AS DENPYO_NO                                                " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L            AS OKYAKU_KANRI_NO                                          " & vbNewLine _
        & " -- 元着区分                                                                               " & vbNewLine _
        & " --UPD 2016/09/21 丸和(横浜)対応,''                                                        " & vbNewLine _
        & " ,CASE  WHEN KBNZ024.KBN_NM2 IS NOT NULL AND KBNZ024.KBN_NM2 <> '' THEN '4' --ﾀｲﾑ便20161122 " & vbNewLine _
        & "  WHEN MCD.SUB_KB = '0S' AND EDIL.FREE_C26 = '○' THEN '2'                            " & vbNewLine _
        & "            ELSE CASE OUTKAL.PC_KB WHEN '01' THEN '0'                                      " & vbNewLine _
        & "                                   WHEN '02' THEN '5'                                      " & vbNewLine _
        & "                                   ELSE '0'  END                                           " & vbNewLine _
        & "  END       AS OKURIJYO_PTN                                                                " & vbNewLine _
        & " , CASE WHEN KBNZ024.KBN_NM1 IS NOT NULL AND KBNZ024.KBN_NM1 <> '' THEN KBNZ024.KBN_NM1    " & vbNewLine _
        & "   ELSE '0'   END AS COOL_KBN                                                              " & vbNewLine _
        & " ,''                           AS DENPYO_BANGO                                             " & vbNewLine _
        & " ,OUTKAL.OUTKA_PLAN_DATE       AS OUTKA_PLAN_DATE                                          " & vbNewLine _
        & " ,OUTKAL.ARR_PLAN_DATE         AS ARR_PLAN_DATE                                            " & vbNewLine _
        & " , CASE WHEN KBNZ024.KBN_NM2 IS NOT NULL AND KBNZ024.KBN_NM2 <> ''  THEN KBNZ024.KBN_NM2    " & vbNewLine _
        & "   ELSE '0812'  END AS HAITATSU_TIME                                                            " & vbNewLine _
        & " ,''                           AS DEST_CD                                                  " & vbNewLine _
        & " ,OUTKAL.DEST_TEL              AS DEST_TEL                                                 " & vbNewLine _
        & " ,''                           AS DEST_TEL_EDA                                             " & vbNewLine _
        & " , CASE WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
        & "        THEN EDIL.DEST_ZIP                                                                 " & vbNewLine _
        & "        ELSE DEST.ZIP                                                                      " & vbNewLine _
        & "        END                                                      AS DEST_ZIP               " & vbNewLine _
        & " , CASE OUTKAL.DEST_KB WHEN '01'                                                           " & vbNewLine _
        & "                       THEN LTRIM(RTRIM(OUTKAL.DEST_AD_1))                                 " & vbNewLine _
        & "                          + LTRIM(RTRIM(OUTKAL.DEST_AD_2))                                 " & vbNewLine _
        & "                       WHEN '02'                                                           " & vbNewLine _
        & "                       THEN LTRIM(RTRIM(EDIL.DEST_AD_1))                                   " & vbNewLine _
        & "                          + LTRIM(RTRIM(EDIL.DEST_AD_2))                                   " & vbNewLine _
        & "                       ELSE LTRIM(RTRIM(DEST.AD_1))                                        " & vbNewLine _
        & "                          + LTRIM(RTRIM(DEST.AD_2))                                        " & vbNewLine _
        & "                       END                                       AS DEST_ADD_1             " & vbNewLine _
        & " ,''                           AS DEST_TATEMONO_NM                                         " & vbNewLine _
        & " , OUTKAL.DEST_AD_3            AS DEST_BUMON1                                              " & vbNewLine _
        & " ,''                                                             AS DEST_BUMON2            " & vbNewLine _
        & " , CASE OUTKAL.DEST_KB WHEN '01' THEN OUTKAL.DEST_NM                                       " & vbNewLine _
        & "                       WHEN '02' THEN EDIL.DEST_NM                                         " & vbNewLine _
        & "                       ELSE  DEST.DEST_NM END                    AS DEST_NM                " & vbNewLine _
        & " ,''                           AS DET_NM_KANA                                              " & vbNewLine _
        & " ,''                           AS KEISYO                                                   " & vbNewLine _
        & " ,''                           AS IRAINUSHI_CD                                             " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'  -- 自社倉庫                                                " & vbNewLine _
        & "        THEN SOKO.TEL                                                                      " & vbNewLine _
        & "        ELSE NRSBR.TEL                                                                     " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_TEL          " & vbNewLine _
        & " , CUST.TEL                                                      AS IRAINUSHI_TEL_SAKURA   " & vbNewLine _
        & " ,''                           AS IRAINUSHI_TEL_EDA                                        " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN SOKO.ZIP                                                                      " & vbNewLine _
        & "        ELSE NRSBR.ZIP                                                                     " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_ZIP          " & vbNewLine _
        & " , CUST.ZIP                                                      AS IRAINUSHI_ZIP_SAKURA   " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN LTRIM(RTRIM(SOKO.AD_1))                                                       " & vbNewLine _
        & "        ELSE LTRIM(RTRIM(NRSBR.AD_1))                                                      " & vbNewLine _
        & "   END                                                                                     " & vbNewLine _
        & " + CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN LTRIM(RTRIM(SOKO.AD_2))                                                       " & vbNewLine _
        & "        ELSE LTRIM(RTRIM(NRSBR.AD_2))                                                      " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_ADD          " & vbNewLine _
        & " , LTRIM(RTRIM(CUST.AD_1)) + LTRIM(RTRIM(CUST.AD_2))                                       " & vbNewLine _
        & "                           + LTRIM(RTRIM(CUST.AD_3))             AS IRAINUSHI_ADD_SAKURA   " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN LTRIM(RTRIM(SOKO.AD_3))                                                       " & vbNewLine _
        & "        ELSE LTRIM(RTRIM(NRSBR.AD_3))                                                      " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_TATEMONO_NM  " & vbNewLine _
        & " , ''                                                            AS IRAINUSHI_TATEMONO_NM_SAKURA " & vbNewLine _
        & " , NRSBR.NRS_BR_NM                                               AS IRAINUSHI_NM           " & vbNewLine _
        & " , CUST.CUST_NM_L                                                AS IRAINUSHI_NM_SAKURA    " & vbNewLine _
        & " ,''                           AS IRAINUSHI_NM_KANA                                        " & vbNewLine _
        & " ,''                           AS GOODS_CD1                                                " & vbNewLine _
        & " ,OUTKAL.CUST_ORD_NO       AS GOODS_NM1  --UPD 2016/07/14                                  " & vbNewLine _
        & " ,GOODS.GOODS_NM_1         AS GOODS_NM1_SAKURA                                             " & vbNewLine _
        & " ,''                           AS GOODS_CD2                                                " & vbNewLine _
        & " ,''                           AS GOODS_NM2                                                " & vbNewLine _
        & " ,''                           AS NIATSUKAI1                                               " & vbNewLine _
        & " ,''                           AS NIATSUKAI2                                               " & vbNewLine _
        & " -- 記事 = (売上先or荷主別名or荷主名（大)) + '様扱い'                                      " & vbNewLine _
        & " , CASE WHEN SHIP_CD_DEST.NRS_BR_CD IS NOT NULL                                            " & vbNewLine _
        & "        THEN SHIP_CD_DEST.DEST_NM                                                          " & vbNewLine _
        & "        WHEN LEN(CUST.DENPYO_NM) > 0                                                       " & vbNewLine _
        & "        THEN CUST.DENPYO_NM                                                                " & vbNewLine _
        & "        ELSE CUST.CUST_NM_L END                                                            " & vbNewLine _
        & " + CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
        & "        THEN OKURIJOCSV.FREE_C04                                                           " & vbNewLine _
        & "        WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
        & "        THEN OKURIJOCSVX.FREE_C04                                                          " & vbNewLine _
        & "        ELSE ''                                                                            " & vbNewLine _
        & "   END                                                           AS KIJI                   " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L                                              AS KIJI_SAKURA            " & vbNewLine _
        & " --UPD 2016/09/21 丸和(横浜)対応,''                           AS COLLECT_AMT                                              " & vbNewLine _
        & " ,CASE WHEN MCD.SUB_KB = '0S'                                                                  " & vbNewLine _
        & "            THEN CASE WHEN EDIL.FREE_C26 = '○'                                                 " & vbNewLine _
        & "                           THEN  CONVERT(varchar,CONVERT(INT,EDIL.FREE_N01))                    " & vbNewLine _
        & "                           ELSE ''                                                              " & vbNewLine _
        & "                 END                                                                            " & vbNewLine _
        & "            ELSE ''                                                                             " & vbNewLine _
        & "  END  AS COLLECT_AMT                                                                           " & vbNewLine _
        & " ,CASE WHEN MCD.SUB_KB = '0S'                                                                   " & vbNewLine _
        & "            THEN CASE WHEN EDIL.FREE_C26 = '○'                                                 " & vbNewLine _
        & "                           THEN  CONVERT(int,CONVERT(int,CONVERT(varchar,CONVERT(INT,EDIL.FREE_N01)))            " & vbNewLine _
        & "                              - (CONVERT(int,CONVERT(varchar,CONVERT(INT,EDIL.FREE_N01))) / (1 + TAX.TAX_RATE))) " & vbNewLine _
        & "                           ELSE ''                                                              " & vbNewLine _
        & "                 END                                                                            " & vbNewLine _
        & "            ELSE ''                                                                             " & vbNewLine _
        & "  END  AS COLLECT_TAX                                                                           " & vbNewLine _
        & " ,''                           AS TOMEOKI                                                  " & vbNewLine _
        & " ,''                           AS BR_CD                                                    " & vbNewLine _
        & " ,OUTKAL.OUTKA_PKG_NB          AS PRINT_CNT                                                " & vbNewLine _
        & " ,''                           AS KOSU_DISP_FLG                                            " & vbNewLine _
        & " , CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
        & "        THEN OKURIJOCSV.FREE_C02                                                           " & vbNewLine _
        & "        WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
        & "        THEN OKURIJOCSVX.FREE_C02                                                          " & vbNewLine _
        & "        ELSE ''                                                                            " & vbNewLine _
        & "   END                                                           AS SEIKYUSAKI_CD          " & vbNewLine _
        & " ,''                           AS SEIKYUSAKI_BUNRUI_CD                                     " & vbNewLine _
        & " ,'01'                         AS UNCHIN_KANRI_NO                                          " & vbNewLine _
        & " ,''                           AS CARD_HARAI                                               " & vbNewLine _
        & " ,''                           AS CARD_KAMEI_NO                                            " & vbNewLine _
        & " ,''                           AS CARD_NO1                                                 " & vbNewLine _
        & " ,''                           AS CARD_NO2                                                 " & vbNewLine _
        & " ,''                           AS CARD_NO3                                                 " & vbNewLine _
        & " ,''                           AS EMAIL_KBN                                                " & vbNewLine _
        & " ,''                           AS EMAIL_ADDRESS                                            " & vbNewLine _
        & " ,''                           AS EMAIL_IN_TYPE                                            " & vbNewLine _
        & " ,''                           AS EMAIL_MSG                                                " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_KBN                                         " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_ADDRESS                                     " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_MSG                                         " & vbNewLine _
        & " ,''                           AS SYUNOU_KBN                                               " & vbNewLine _
        & " ,''                           AS YOBI                                                     " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_AMT                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_TAX                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_ZIP                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_ADD                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_TATEMONO                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_BUMON1                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_BUMON2                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_NM_KNJI                                       " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_NM_KANA                                       " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_NM_KANJI                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_NM_ZIP                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_ADD                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_TATEMONO                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_TEL                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_KANRI_NO                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_HINMEI                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_BIKOU                                         " & vbNewLine _
        & " ,@ROW_NO                      AS ROW_NO                                                   " & vbNewLine _
        & " ,OUTKAL.SYS_UPD_DATE          AS SYS_UPD_DATE                                             " & vbNewLine _
        & " ,OUTKAL.SYS_UPD_TIME          AS SYS_UPD_TIME                                             " & vbNewLine _
        & " ,@FILEPATH                    AS FILEPATH                                                 " & vbNewLine _
        & " ,@FILENAME                    AS FILENAME                                                 " & vbNewLine _
        & " ,@SYS_DATE                    AS SYS_DATE                                                 " & vbNewLine _
        & " ,@SYS_TIME                    AS SYS_TIME                                                 " & vbNewLine _
        & " ,CUST.CUST_NM_L              AS CUST_NM_L                   " & vbNewLine






    ''' <summary>
    ''' ヤマトCSV作成データ検索用SQL FROM・WHERE部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_YAMATOB2_CSV_FROM2 As String _
        = " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                      " & vbNewLine _
        & "      DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      DEST.DEST_CD = OUTKAL.DEST_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                      " & vbNewLine _
        & "      CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_S = '00' AND                                                          " & vbNewLine _
        & "      CUST.CUST_CD_SS = '00'                                                             " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_SOKO SOKO  ON                                                     " & vbNewLine _
        & "      SOKO.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      SOKO.WH_CD     = OUTKAL.WH_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
        & "           OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
        & "       AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
        & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                    " & vbNewLine _
        & "           GOODS.NRS_BR_CD    = OUTKAM.NRS_BR_CD                                         " & vbNewLine _
        & "       AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                      " & vbNewLine _
        & " LEFT JOIN LM_MST..M_NRS_BR AS NRSBR                                                     " & vbNewLine _
        & "   ON NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                 " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "            SELECT                                                                       " & vbNewLine _
        & "                  NRS_BR_CD                                                              " & vbNewLine _
        & "                , EDI_CTL_NO                                                             " & vbNewLine _
        & "                , OUTKA_CTL_NO                                                           " & vbNewLine _
        & "            FROM (                                                                       " & vbNewLine _
        & "                    SELECT                                                               " & vbNewLine _
        & "                          EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
        & "                        , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
        & "                        , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
        & "                        , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                     " & vbNewLine _
        & "                          ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
        & "                                                             , EDIOUTL.OUTKA_CTL_NO      " & vbNewLine _
        & "                                                      ORDER BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
        & "                                                             , EDIOUTL.EDI_CTL_NO        " & vbNewLine _
        & "                                                  )                                      " & vbNewLine _
        & "                          END AS IDX                                                     " & vbNewLine _
        & "                    FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                 " & vbNewLine _
        & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
        & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                              " & vbNewLine _
        & "                      AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                             " & vbNewLine _
        & "                  ) EBASE                                                                " & vbNewLine _
        & "            WHERE EBASE.IDX = 1                                                          " & vbNewLine _
        & "            ) TOPEDI                                                                     " & vbNewLine _
        & "        ON TOPEDI.NRS_BR_CD               = OUTKAL.NRS_BR_CD                             " & vbNewLine _
        & "       AND TOPEDI.OUTKA_CTL_NO            = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                   " & vbNewLine _
        & "        ON EDIL.NRS_BR_CD                 = TOPEDI.NRS_BR_CD                             " & vbNewLine _
        & "       AND EDIL.EDI_CTL_NO                = TOPEDI.EDI_CTL_NO                            " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                     = OUTKAL.NRS_BR_CD                             " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                  = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                  = '20'                                         " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                   = '0'                                          " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "   ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                              " & vbNewLine _
        & "  AND OKURIJOCSV.OKURIJO_TP               = '04' --ヤマト                                " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "   ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                      " & vbNewLine _
        & "  AND OKURIJOCSVX.OKURIJO_TP              = '04' --ヤマト                                " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJO_KBN    --追加マスタJOIN(区分用)  20161122    " & vbNewLine _
        & "   ON OKURIJO_KBN.NRS_BR_CD               = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJO_KBN.UNSOCO_CD               = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJO_KBN.CUST_CD_L               = 'XXXXX'                                      " & vbNewLine _
        & "  AND OKURIJO_KBN.OKURIJO_TP              = '04' --ヤマト                                " & vbNewLine _
        & "  AND OKURIJO_KBN.FREE_C20 = UNSOl.UNSO_BR_CD                                            " & vbNewLine _
        & "  AND OKURIJO_KBN.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN KBNZ024 --                                                   " & vbNewLine _
        & "  ON KBNZ024.KBN_GROUP_CD = 'Z024'                                                       " & vbNewLine _
        & "  AND KBNZ024.KBN_CD = OKURIJO_KBN.FREE_C19                                              " & vbNewLine _
        & "  AND KBNZ024.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST SHIP_CD_DEST                                                 " & vbNewLine _
        & "   ON SHIP_CD_DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.DEST_CD   = OUTKAL.SHIP_CD_L                                          " & vbNewLine _
        & "--荷主明細M  丸和(横浜)対応                                                              " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                  " & vbNewLine _
        & "   ON MCD.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                   " & vbNewLine _
        & "  AND MCD.CUST_CD   = OUTKAL.CUST_CD_L + OUTKAL.CUST_CD_M                                " & vbNewLine _
        & "  AND MCD.SUB_KB    = '0S'                                                               " & vbNewLine _
        & "LEFT JOIN (                                     " & vbNewLine _
        & "    SELECT                                      " & vbNewLine _
        & "        TAX.TAX_RATE   AS TAX_RATE              " & vbNewLine _
        & "      , TAX.START_DATE AS START_DATE            " & vbNewLine _
        & "    FROM                                        " & vbNewLine _
        & "       $LM_MST$..M_TAX TAX                      " & vbNewLine _
        & "    INNER JOIN (                                " & vbNewLine _
        & "        SELECT                                  " & vbNewLine _
        & "            KBN1.KBN_GROUP_CD                   " & vbNewLine _
        & "          , KBN1.KBN_CD                         " & vbNewLine _
        & "          , KBN1.KBN_NM3                        " & vbNewLine _
        & "          , TAX2.START_DATE                     " & vbNewLine _
        & "          , TAX2.TAX_CD                         " & vbNewLine _
        & "        FROM (                                  " & vbNewLine _
        & "            SELECT                              " & vbNewLine _
        & "                MAX(START_DATE) AS START_DATE   " & vbNewLine _
        & "              , TAX3.TAX_CD     AS TAX_CD       " & vbNewLine _
        & "            FROM                                " & vbNewLine _
        & "               $LM_MST$..M_TAX TAX3             " & vbNewLine _
        & "            WHERE                               " & vbNewLine _
        & "                TAX3.START_DATE <= @OUTKA_PLAN_DATE   " & vbNewLine _
        & "            GROUP BY                            " & vbNewLine _
        & "                TAX3.TAX_CD) TAX2               " & vbNewLine _
        & "        INNER JOIN                              " & vbNewLine _
        & "           $LM_MST$..Z_KBN KBN1                 " & vbNewLine _
        & "         ON KBN1.KBN_GROUP_CD = 'Z001'          " & vbNewLine _
        & "        AND KBN1.KBN_CD = '01'                  " & vbNewLine _
        & "        AND KBN1.KBN_NM3 = TAX2.TAX_CD) TAX1    " & vbNewLine _
        & "     ON TAX1.START_DATE = TAX.START_DATE        " & vbNewLine _
        & "    AND TAX1.KBN_NM3 = TAX.TAX_CD)TAX           " & vbNewLine _
        & "  ON TAX.START_DATE <= @OUTKA_PLAN_DATE         " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD      = @NRS_BR_CD                                                " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L     = @OUTKA_NO_L                                               " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG    = '0'                                                       " & vbNewLine _
        & "   AND NOT EXISTS (SELECT * FROM LM_MST..M_CUST_DETAILS                                  " & vbNewLine _
        & "                            WHERE M_CUST_DETAILS.NRS_BR_CD   = OUTKAL.NRS_BR_CD          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.CUST_CD     = OUTKAL.CUST_CD_L          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SUB_KB      = '0M'                      " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SYS_DEL_FLG = '0'                       " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SET_NAIYO   = '1')                      " & vbNewLine


#End If

#End Region

#Region "丸和物産(横浜)"

    ''' <summary>
    ''' ヤマトCSV作成データ検索用SQL SELECT部 丸和物産(横浜)用
    ''' </summary>
    ''' <remarks>標準版を流用して専用テーブルの結合を付加</remarks>
    Private Const SQL_SELECT_YAMATOB2_CSV3 As String _
        = " SELECT TOP(1)                                                                             " & vbNewLine _
        & "  OUTKAL.NRS_BR_CD	          AS NRS_BR_CD                                                " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L            AS DENPYO_NO                                                " & vbNewLine _
        & " ,CASE ISNULL(MRC.B2_CUSTOMER_NO, '')                                                      " & vbNewLine _
        & "    WHEN '' THEN OUTKAL.OUTKA_NO_L                                                         " & vbNewLine _
        & "    ELSE MRC.B2_CUSTOMER_NO                                                                " & vbNewLine _
        & "    END                        AS OKYAKU_KANRI_NO                                          " & vbNewLine _
        & " -- 元着区分                                                                               " & vbNewLine _
        & " --UPD 2016/09/21 丸和(横浜)対応,''                                                        " & vbNewLine _
        & " --UPD START 2023/07/31 038225【LMS】丸和物産ExcelセミEDI支払方法が反映されない障害対応    " & vbNewLine _
        & " --,CASE  WHEN KBNZ024.KBN_NM2 IS NOT NULL AND KBNZ024.KBN_NM2 <> '' THEN '4' --ﾀｲﾑ便20161122 " & vbNewLine _
        & " -- WHEN MCD.SUB_KB = '0S' AND EDIL.FREE_C26 = '○' THEN '2'                            " & vbNewLine _
        & " --           ELSE CASE OUTKAL.PC_KB WHEN '01' THEN '0'                                      " & vbNewLine _
        & " --                                  WHEN '02' THEN '5'                                      " & vbNewLine _
        & " --                                  ELSE '0'  END                                           " & vbNewLine _
        & " -- END       AS OKURIJYO_PTN                                                                " & vbNewLine _
        & " ,CASE WHEN MCD.SUB_KB = '0S' AND EDIL.FREE_C26 = '○' THEN '2'  --Excel取込 代引き        " & vbNewLine _
        & "       WHEN MCD.SUB_KB = '0S' AND EDIL.FREE_C05 <> '' THEN EDIL.FREE_C05  --Csv取込 お支払方法の先頭1文字 " & vbNewLine _
        & "       ELSE '0'  --Excel取込 代引き以外                                                    " & vbNewLine _
        & "  END       AS OKURIJYO_PTN                                                                " & vbNewLine _
        & " --UPD END 2023/07/31 038225【LMS】丸和物産ExcelセミEDI支払方法が反映されない障害対応      " & vbNewLine _
        & " , CASE WHEN KBNZ024.KBN_NM1 IS NOT NULL AND KBNZ024.KBN_NM1 <> '' THEN KBNZ024.KBN_NM1    " & vbNewLine _
        & "   ELSE '0'   END AS COOL_KBN                                                              " & vbNewLine _
        & " ,''                           AS DENPYO_BANGO                                             " & vbNewLine _
        & " ,OUTKAL.OUTKA_PLAN_DATE       AS OUTKA_PLAN_DATE                                          " & vbNewLine _
        & " ,OUTKAL.ARR_PLAN_DATE         AS ARR_PLAN_DATE                                            " & vbNewLine _
        & " , CASE WHEN KBNZ024.KBN_NM2 IS NOT NULL AND KBNZ024.KBN_NM2 <> ''  THEN KBNZ024.KBN_NM2    " & vbNewLine _
        & "   ELSE '0812'  END AS HAITATSU_TIME                                                            " & vbNewLine _
        & " ,''                           AS DEST_CD                                                  " & vbNewLine _
        & " ,OUTKAL.DEST_TEL              AS DEST_TEL                                                 " & vbNewLine _
        & " ,''                           AS DEST_TEL_EDA                                             " & vbNewLine _
        & " , CASE WHEN OUTKAL.DEST_KB = '02'                                                         " & vbNewLine _
        & "        THEN EDIL.DEST_ZIP                                                                 " & vbNewLine _
        & "        ELSE DEST.ZIP                                                                      " & vbNewLine _
        & "        END                                                      AS DEST_ZIP               " & vbNewLine _
        & " , CASE OUTKAL.DEST_KB WHEN '01'                                                           " & vbNewLine _
        & "                       THEN LTRIM(RTRIM(OUTKAL.DEST_AD_1))                                 " & vbNewLine _
        & "                          + LTRIM(RTRIM(OUTKAL.DEST_AD_2))                                 " & vbNewLine _
        & "                       WHEN '02'                                                           " & vbNewLine _
        & "                       THEN LTRIM(RTRIM(EDIL.DEST_AD_1))                                   " & vbNewLine _
        & "                          + LTRIM(RTRIM(EDIL.DEST_AD_2))                                   " & vbNewLine _
        & "                       ELSE LTRIM(RTRIM(DEST.AD_1))                                        " & vbNewLine _
        & "                          + LTRIM(RTRIM(DEST.AD_2))                                        " & vbNewLine _
        & "                       END                                       AS DEST_ADD_1             " & vbNewLine _
        & " ,''                           AS DEST_TATEMONO_NM                                         " & vbNewLine _
        & " , OUTKAL.DEST_AD_3            AS DEST_BUMON1                                              " & vbNewLine _
        & " ,''                                                             AS DEST_BUMON2            " & vbNewLine _
        & " , CASE OUTKAL.DEST_KB WHEN '01' THEN OUTKAL.DEST_NM                                       " & vbNewLine _
        & "                       WHEN '02' THEN EDIL.DEST_NM                                         " & vbNewLine _
        & "                       ELSE  DEST.DEST_NM END                    AS DEST_NM                " & vbNewLine _
        & " ,''                           AS DET_NM_KANA                                              " & vbNewLine _
        & " ,''                           AS KEISYO                                                   " & vbNewLine _
        & " ,''                           AS IRAINUSHI_CD                                             " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'  -- 自社倉庫                                                " & vbNewLine _
        & "        THEN SOKO.TEL                                                                      " & vbNewLine _
        & "        ELSE NRSBR.TEL                                                                     " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_TEL          " & vbNewLine _
        & " , CUST.TEL                                                      AS IRAINUSHI_TEL_SAKURA   " & vbNewLine _
        & " ,''                           AS IRAINUSHI_TEL_EDA                                        " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN SOKO.ZIP                                                                      " & vbNewLine _
        & "        ELSE NRSBR.ZIP                                                                     " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_ZIP          " & vbNewLine _
        & " , CUST.ZIP                                                      AS IRAINUSHI_ZIP_SAKURA   " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN LTRIM(RTRIM(SOKO.AD_1))                                                       " & vbNewLine _
        & "        ELSE LTRIM(RTRIM(NRSBR.AD_1))                                                      " & vbNewLine _
        & "   END                                                                                     " & vbNewLine _
        & " + CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN LTRIM(RTRIM(SOKO.AD_2))                                                       " & vbNewLine _
        & "        ELSE LTRIM(RTRIM(NRSBR.AD_2))                                                      " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_ADD          " & vbNewLine _
        & " , LTRIM(RTRIM(CUST.AD_1)) + LTRIM(RTRIM(CUST.AD_2))                                       " & vbNewLine _
        & "                           + LTRIM(RTRIM(CUST.AD_3))             AS IRAINUSHI_ADD_SAKURA   " & vbNewLine _
        & " , CASE WHEN SOKO.WH_KB = '01'                                                             " & vbNewLine _
        & "        THEN LTRIM(RTRIM(SOKO.AD_3))                                                       " & vbNewLine _
        & "        ELSE LTRIM(RTRIM(NRSBR.AD_3))                                                      " & vbNewLine _
        & "   END                                                           AS IRAINUSHI_TATEMONO_NM  " & vbNewLine _
        & " , ''                                                            AS IRAINUSHI_TATEMONO_NM_SAKURA " & vbNewLine _
        & " , NRSBR.NRS_BR_NM                                               AS IRAINUSHI_NM           " & vbNewLine _
        & " , CUST.CUST_NM_L                                                AS IRAINUSHI_NM_SAKURA    " & vbNewLine _
        & " ,''                           AS IRAINUSHI_NM_KANA                                        " & vbNewLine _
        & " ,''                           AS GOODS_CD1                                                " & vbNewLine _
        & " , CASE WHEN OUTKAL.REMARK <> '' THEN OUTKAL.REMARK                                        " & vbNewLine _
        & "        ELSE '-'                                                                           " & vbNewLine _
        & "   END                                                           AS GOODS_NM1              " & vbNewLine _
        & " ,GOODS.GOODS_NM_1         AS GOODS_NM1_SAKURA                                             " & vbNewLine _
        & " ,''                           AS GOODS_CD2                                                " & vbNewLine _
        & " ,''                           AS GOODS_NM2                                                " & vbNewLine _
        & " ,''                           AS NIATSUKAI1                                               " & vbNewLine _
        & " ,''                           AS NIATSUKAI2                                               " & vbNewLine _
        & " -- 記事 = (売上先or荷主別名or荷主名（大)) + '様扱い'                                      " & vbNewLine _
        & " , CASE WHEN SHIP_CD_DEST.NRS_BR_CD IS NOT NULL                                            " & vbNewLine _
        & "        THEN SHIP_CD_DEST.DEST_NM                                                          " & vbNewLine _
        & "        WHEN LEN(CUST.DENPYO_NM) > 0                                                       " & vbNewLine _
        & "        THEN CUST.DENPYO_NM                                                                " & vbNewLine _
        & "        ELSE CUST.CUST_NM_L END                                                            " & vbNewLine _
        & " + CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
        & "        THEN OKURIJOCSV.FREE_C04                                                           " & vbNewLine _
        & "        WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
        & "        THEN OKURIJOCSVX.FREE_C04                                                          " & vbNewLine _
        & "        ELSE ''                                                                            " & vbNewLine _
        & "   END                                                           AS KIJI                   " & vbNewLine _
        & " ,OUTKAL.OUTKA_NO_L                                              AS KIJI_SAKURA            " & vbNewLine _
        & " --UPD 2016/09/21 丸和(横浜)対応,''                           AS COLLECT_AMT                                              " & vbNewLine _
        & " ,CASE WHEN MCD.SUB_KB = '0S'                                                                  " & vbNewLine _
        & "            THEN CASE WHEN EDIL.FREE_C26 = '○'                                                 " & vbNewLine _
        & "                           THEN  CONVERT(varchar,CONVERT(INT,EDIL.FREE_N01))                    " & vbNewLine _
        & " --UPD START 2023/07/31 038225【LMS】丸和物産ExcelセミEDI支払方法が反映されない障害対応         " & vbNewLine _
        & " --                          ELSE ''                                                              " & vbNewLine _
        & "                           ELSE EDIL.FREE_C06                                                   " & vbNewLine _
        & " --UPD END 2023/07/31 038225【LMS】丸和物産ExcelセミEDI支払方法が反映されない障害対応           " & vbNewLine _
        & "                 END                                                                            " & vbNewLine _
        & "            ELSE ''                                                                             " & vbNewLine _
        & "  END  AS COLLECT_AMT                                                                           " & vbNewLine _
        & " ,CASE WHEN MCD.SUB_KB = '0S'                                                                   " & vbNewLine _
        & "            THEN CASE WHEN EDIL.FREE_C26 = '○'                                                 " & vbNewLine _
        & "                           THEN  CONVERT(int,CONVERT(int,CONVERT(varchar,CONVERT(INT,EDIL.FREE_N01)))            " & vbNewLine _
        & "                              - (CONVERT(int,CONVERT(varchar,CONVERT(INT,EDIL.FREE_N01))) / (1 + TAX.TAX_RATE))) " & vbNewLine _
        & " --UPD START 2023/07/31 038225【LMS】丸和物産ExcelセミEDI支払方法が反映されない障害対応         " & vbNewLine _
        & " --                          ELSE ''                                                              " & vbNewLine _
        & "                           ELSE EDIL.FREE_C07                                                   " & vbNewLine _
        & " --UPD END 2023/07/31 038225【LMS】丸和物産ExcelセミEDI支払方法が反映されない障害対応           " & vbNewLine _
        & "                 END                                                                            " & vbNewLine _
        & "            ELSE ''                                                                             " & vbNewLine _
        & "  END  AS COLLECT_TAX                                                                           " & vbNewLine _
        & " ,''                           AS TOMEOKI                                                  " & vbNewLine _
        & " ,''                           AS BR_CD                                                    " & vbNewLine _
        & " ,OUTKAL.OUTKA_PKG_NB          AS PRINT_CNT                                                " & vbNewLine _
        & " ,''                           AS KOSU_DISP_FLG                                            " & vbNewLine _
        & " , CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                              " & vbNewLine _
        & "        THEN OKURIJOCSV.FREE_C02                                                           " & vbNewLine _
        & "        WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                             " & vbNewLine _
        & "        THEN OKURIJOCSVX.FREE_C02                                                          " & vbNewLine _
        & "        ELSE ''                                                                            " & vbNewLine _
        & "   END                                                           AS SEIKYUSAKI_CD          " & vbNewLine _
        & " ,''                           AS SEIKYUSAKI_BUNRUI_CD                                     " & vbNewLine _
        & " ,'01'                         AS UNCHIN_KANRI_NO                                          " & vbNewLine _
        & " ,''                           AS CARD_HARAI                                               " & vbNewLine _
        & " ,''                           AS CARD_KAMEI_NO                                            " & vbNewLine _
        & " ,''                           AS CARD_NO1                                                 " & vbNewLine _
        & " ,''                           AS CARD_NO2                                                 " & vbNewLine _
        & " ,''                           AS CARD_NO3                                                 " & vbNewLine _
        & " ,''                           AS EMAIL_KBN                                                " & vbNewLine _
        & " ,''                           AS EMAIL_ADDRESS                                            " & vbNewLine _
        & " ,''                           AS EMAIL_IN_TYPE                                            " & vbNewLine _
        & " ,''                           AS EMAIL_MSG                                                " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_KBN                                         " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_ADDRESS                                     " & vbNewLine _
        & " ,''                           AS KANRYO_EMAIL_MSG                                         " & vbNewLine _
        & " ,''                           AS SYUNOU_KBN                                               " & vbNewLine _
        & " ,''                           AS YOBI                                                     " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_AMT                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_TAX                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_ZIP                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_ADD                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_TATEMONO                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_BUMON1                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_BUMON2                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_NM_KNJI                                       " & vbNewLine _
        & " ,''                           AS SYUNOU_SKY_NM_KANA                                       " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_NM_KANJI                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_NM_ZIP                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_ADD                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_TATEMONO                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_TOI_TEL                                           " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_KANRI_NO                                      " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_HINMEI                                        " & vbNewLine _
        & " ,''                           AS SYUNOU_DAI_BIKOU                                         " & vbNewLine _
        & " ,@ROW_NO                      AS ROW_NO                                                   " & vbNewLine _
        & " ,OUTKAL.SYS_UPD_DATE          AS SYS_UPD_DATE                                             " & vbNewLine _
        & " ,OUTKAL.SYS_UPD_TIME          AS SYS_UPD_TIME                                             " & vbNewLine _
        & " ,@FILEPATH                    AS FILEPATH                                                 " & vbNewLine _
        & " ,@FILENAME                    AS FILENAME                                                 " & vbNewLine _
        & " ,@SYS_DATE                    AS SYS_DATE                                                 " & vbNewLine _
        & " ,@SYS_TIME                    AS SYS_TIME                                                 " & vbNewLine _
        & " ,CUST.CUST_NM_L              AS CUST_NM_L                   " & vbNewLine

    ''' <summary>
    ''' ヤマトCSV作成データ検索用SQL FROM・WHERE部 丸和物産(横浜)用
    ''' </summary>
    ''' <remarks>標準版を流用して専用テーブルの結合を付加</remarks>
    Private Const SQL_SELECT_YAMATOB2_CSV_FROM3 As String _
        = " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                      " & vbNewLine _
        & "      DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      DEST.DEST_CD = OUTKAL.DEST_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                      " & vbNewLine _
        & "      CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_S = '00' AND                                                          " & vbNewLine _
        & "      CUST.CUST_CD_SS = '00'                                                             " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_SOKO SOKO  ON                                                     " & vbNewLine _
        & "      SOKO.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      SOKO.WH_CD     = OUTKAL.WH_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
        & "           OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
        & "       AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
        & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                    " & vbNewLine _
        & "           GOODS.NRS_BR_CD    = OUTKAM.NRS_BR_CD                                         " & vbNewLine _
        & "       AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                      " & vbNewLine _
        & " LEFT JOIN LM_MST..M_NRS_BR AS NRSBR                                                     " & vbNewLine _
        & "   ON NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                 " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "            SELECT                                                                       " & vbNewLine _
        & "                  NRS_BR_CD                                                              " & vbNewLine _
        & "                , EDI_CTL_NO                                                             " & vbNewLine _
        & "                , OUTKA_CTL_NO                                                           " & vbNewLine _
        & "            FROM (                                                                       " & vbNewLine _
        & "                    SELECT                                                               " & vbNewLine _
        & "                          EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
        & "                        , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
        & "                        , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
        & "                        , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                     " & vbNewLine _
        & "                          ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
        & "                                                             , EDIOUTL.OUTKA_CTL_NO      " & vbNewLine _
        & "                                                      ORDER BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
        & "                                                             , EDIOUTL.EDI_CTL_NO        " & vbNewLine _
        & "                                                  )                                      " & vbNewLine _
        & "                          END AS IDX                                                     " & vbNewLine _
        & "                    FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                 " & vbNewLine _
        & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
        & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                              " & vbNewLine _
        & "                      AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                             " & vbNewLine _
        & "                  ) EBASE                                                                " & vbNewLine _
        & "            WHERE EBASE.IDX = 1                                                          " & vbNewLine _
        & "            ) TOPEDI                                                                     " & vbNewLine _
        & "        ON TOPEDI.NRS_BR_CD               = OUTKAL.NRS_BR_CD                             " & vbNewLine _
        & "       AND TOPEDI.OUTKA_CTL_NO            = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                   " & vbNewLine _
        & "        ON EDIL.NRS_BR_CD                 = TOPEDI.NRS_BR_CD                             " & vbNewLine _
        & "       AND EDIL.EDI_CTL_NO                = TOPEDI.EDI_CTL_NO                            " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                     = OUTKAL.NRS_BR_CD                             " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                  = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                  = '20'                                         " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                   = '0'                                          " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "   ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                              " & vbNewLine _
        & "  AND OKURIJOCSV.OKURIJO_TP               = '04' --ヤマト                                " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "   ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                      " & vbNewLine _
        & "  AND OKURIJOCSVX.OKURIJO_TP              = '04' --ヤマト                                " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJO_KBN    --追加マスタJOIN(区分用)  20161122    " & vbNewLine _
        & "   ON OKURIJO_KBN.NRS_BR_CD               = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJO_KBN.UNSOCO_CD               = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJO_KBN.CUST_CD_L               = 'XXXXX'                                      " & vbNewLine _
        & "  AND OKURIJO_KBN.OKURIJO_TP              = '04' --ヤマト                                " & vbNewLine _
        & "  AND OKURIJO_KBN.FREE_C20 = UNSOl.UNSO_BR_CD                                            " & vbNewLine _
        & "  AND OKURIJO_KBN.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN KBNZ024 --                                                   " & vbNewLine _
        & "  ON KBNZ024.KBN_GROUP_CD = 'Z024'                                                       " & vbNewLine _
        & "  AND KBNZ024.KBN_CD = OKURIJO_KBN.FREE_C19                                              " & vbNewLine _
        & "  AND KBNZ024.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST SHIP_CD_DEST                                                 " & vbNewLine _
        & "   ON SHIP_CD_DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.DEST_CD   = OUTKAL.SHIP_CD_L                                          " & vbNewLine _
        & "--荷主明細M  丸和(横浜)対応                                                              " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                  " & vbNewLine _
        & "   ON MCD.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                   " & vbNewLine _
        & "  AND MCD.CUST_CD   = OUTKAL.CUST_CD_L + OUTKAL.CUST_CD_M                                " & vbNewLine _
        & "  AND MCD.SUB_KB    = '0S'                                                               " & vbNewLine _
        & "--丸和物産(CSV)出荷EDI受信データ                                                         " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "   SELECT                                                                                " & vbNewLine _
        & "      NRS_BR_CD                                                                          " & vbNewLine _
        & "     ,EDI_CTL_NO                                                                         " & vbNewLine _
        & "     ,MIN(B2_CUSTOMER_NO) AS B2_CUSTOMER_NO                                              " & vbNewLine _
        & "   FROM                                                                                  " & vbNewLine _
        & "     $LM_TRN$..H_OUTKAEDI_DTL_MRCCSV                                                     " & vbNewLine _
        & "   GROUP BY                                                                              " & vbNewLine _
        & "      NRS_BR_CD                                                                          " & vbNewLine _
        & "     ,EDI_CTL_NO                                                                         " & vbNewLine _
        & " ) AS MRC                                                                                " & vbNewLine _
        & "   ON MRC.NRS_BR_CD    = EDIL.NRS_BR_CD                                                  " & vbNewLine _
        & "  AND MRC.EDI_CTL_NO   = EDIL.EDI_CTL_NO                                                 " & vbNewLine _
        & "LEFT JOIN (                                     " & vbNewLine _
        & "    SELECT                                      " & vbNewLine _
        & "        TAX.TAX_RATE   AS TAX_RATE              " & vbNewLine _
        & "      , TAX.START_DATE AS START_DATE            " & vbNewLine _
        & "    FROM                                        " & vbNewLine _
        & "       $LM_MST$..M_TAX TAX                      " & vbNewLine _
        & "    INNER JOIN (                                " & vbNewLine _
        & "        SELECT                                  " & vbNewLine _
        & "            KBN1.KBN_GROUP_CD                   " & vbNewLine _
        & "          , KBN1.KBN_CD                         " & vbNewLine _
        & "          , KBN1.KBN_NM3                        " & vbNewLine _
        & "          , TAX2.START_DATE                     " & vbNewLine _
        & "          , TAX2.TAX_CD                         " & vbNewLine _
        & "        FROM (                                  " & vbNewLine _
        & "            SELECT                              " & vbNewLine _
        & "                MAX(START_DATE) AS START_DATE   " & vbNewLine _
        & "              , TAX3.TAX_CD     AS TAX_CD       " & vbNewLine _
        & "            FROM                                " & vbNewLine _
        & "               $LM_MST$..M_TAX TAX3             " & vbNewLine _
        & "            WHERE                               " & vbNewLine _
        & "                TAX3.START_DATE <= @OUTKA_PLAN_DATE   " & vbNewLine _
        & "            GROUP BY                            " & vbNewLine _
        & "                TAX3.TAX_CD) TAX2               " & vbNewLine _
        & "        INNER JOIN                              " & vbNewLine _
        & "           $LM_MST$..Z_KBN KBN1                 " & vbNewLine _
        & "         ON KBN1.KBN_GROUP_CD = 'Z001'          " & vbNewLine _
        & "        AND KBN1.KBN_CD = '01'                  " & vbNewLine _
        & "        AND KBN1.KBN_NM3 = TAX2.TAX_CD) TAX1    " & vbNewLine _
        & "     ON TAX1.START_DATE = TAX.START_DATE        " & vbNewLine _
        & "    AND TAX1.KBN_NM3 = TAX.TAX_CD)TAX           " & vbNewLine _
        & "  ON TAX.START_DATE <= @OUTKA_PLAN_DATE         " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD      = @NRS_BR_CD                                                " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L     = @OUTKA_NO_L                                               " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG    = '0'                                                       " & vbNewLine _
        & "   AND NOT EXISTS (SELECT * FROM LM_MST..M_CUST_DETAILS                                  " & vbNewLine _
        & "                            WHERE M_CUST_DETAILS.NRS_BR_CD   = OUTKAL.NRS_BR_CD          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.CUST_CD     = OUTKAL.CUST_CD_L          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SUB_KB      = '0M'                      " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SYS_DEL_FLG = '0'                       " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SET_NAIYO   = '1')                      " & vbNewLine

#End Region


#End Region

#Region "更新 SQL"

#Region "ヤマトCSV作成"

    Private Const SQL_UPDATE_YAMATOB2_CSV As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
                                             & " DENP_FLAG         = '01'                         " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                             & "  AND OUTKA_NO_L   = @DENPYO_NO                   " & vbNewLine

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
    ''' ヤマトCSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ヤマトCSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectYamatoB2Csv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC890IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If _Row.Item("CSV_OUTFLG").ToString.Trim = "0" Then
            '標準
            Me._StrSql.Append(LMC890DAC.SQL_SELECT_YAMATOB2_CSV2)
            Me._StrSql.Append(LMC890DAC.SQL_SELECT_YAMATOB2_CSV_FROM2)

        ElseIf _Row.Item("CSV_OUTFLG").ToString.Trim = "1" Then
            'FFEM
            Me._StrSql.Append(LMC890DAC.SQL_SELECT_YAMATOB2_CSV)
            Me._StrSql.Append(LMC890DAC.SQL_SELECT_YAMATOB2_CSV_FROM)

        ElseIf _Row.Item("CSV_OUTFLG").ToString.Trim = "2" Then
            '丸和物産(横浜)
            Me._StrSql.Append(LMC890DAC.SQL_SELECT_YAMATOB2_CSV3)
            Me._StrSql.Append(LMC890DAC.SQL_SELECT_YAMATOB2_CSV_FROM3)
        End If

        Call setSQLSelect()                   '条件設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        If _Row.Item("CSV_OUTFLG").ToString.Trim = "0" Then
            '標準
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me._Row("OUTKA_PLAN_DATE"), DBDataType.CHAR))

        ElseIf _Row.Item("CSV_OUTFLG").ToString.Trim = "2" Then
            '丸和物産(横浜)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me._Row("OUTKA_PLAN_DATE"), DBDataType.CHAR))
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC890DAC", "SelectYamatoB2Csv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("OKYAKU_KANRI_NO", "OKYAKU_KANRI_NO")
        map.Add("OKURIJYO_PTN", "OKURIJYO_PTN")
        map.Add("COOL_KBN", "COOL_KBN")
        map.Add("DENPYO_BANGO", "DENPYO_BANGO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("HAITATSU_TIME", "HAITATSU_TIME")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_TEL_EDA", "DEST_TEL_EDA")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_ADD_1", "DEST_ADD_1")
        map.Add("DEST_TATEMONO_NM", "DEST_TATEMONO_NM")
        map.Add("DEST_BUMON1", "DEST_BUMON1")
        map.Add("DEST_BUMON2", "DEST_BUMON2")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DET_NM_KANA", "DET_NM_KANA")
        map.Add("KEISYO", "KEISYO")
        map.Add("IRAINUSHI_CD", "IRAINUSHI_CD")
        If _Row.Item("CSV_OUTFLG2").ToString.Trim = "0" Then
            map.Add("IRAINUSHI_TEL", "IRAINUSHI_TEL")
        ElseIf _Row.Item("CSV_OUTFLG2").ToString.Trim = "1" Then
            map.Add("IRAINUSHI_TEL", "IRAINUSHI_TEL_SAKURA")
        End If
        map.Add("IRAINUSHI_TEL_EDA", "IRAINUSHI_TEL_EDA")
        If _Row.Item("CSV_OUTFLG2").ToString.Trim = "0" Then
            map.Add("IRAINUSHI_ZIP", "IRAINUSHI_ZIP")
            map.Add("IRAINUSHI_ADD", "IRAINUSHI_ADD")
            map.Add("IRAINUSHI_TATEMONO_NM", "IRAINUSHI_TATEMONO_NM")
            map.Add("IRAINUSHI_NM", "IRAINUSHI_NM")
        ElseIf _Row.Item("CSV_OUTFLG2").ToString.Trim = "1" Then
            map.Add("IRAINUSHI_ZIP", "IRAINUSHI_ZIP_SAKURA")
            map.Add("IRAINUSHI_ADD", "IRAINUSHI_ADD_SAKURA")
            map.Add("IRAINUSHI_TATEMONO_NM", "IRAINUSHI_TATEMONO_NM_SAKURA")
            map.Add("IRAINUSHI_NM", "IRAINUSHI_NM_SAKURA")
        End If
        map.Add("IRAINUSHI_NM_KANA", "IRAINUSHI_NM_KANA")
        map.Add("GOODS_CD1", "GOODS_CD1")
        If _Row.Item("CSV_OUTFLG2").ToString.Trim = "0" Then
            map.Add("GOODS_NM1", "GOODS_NM1")
        ElseIf _Row.Item("CSV_OUTFLG2").ToString.Trim = "1" Then
            map.Add("GOODS_NM1", "GOODS_NM1_SAKURA")
        End If
        map.Add("GOODS_CD2", "GOODS_CD2")
        map.Add("GOODS_NM2", "GOODS_NM2")
        map.Add("NIATSUKAI1", "NIATSUKAI1")
        map.Add("NIATSUKAI2", "NIATSUKAI2")
        If _Row.Item("CSV_OUTFLG2").ToString.Trim = "0" Then
            map.Add("KIJI", "KIJI")
        ElseIf _Row.Item("CSV_OUTFLG2").ToString.Trim = "1" Then
            map.Add("KIJI", "KIJI_SAKURA")
        End If
        map.Add("COLLECT_AMT", "COLLECT_AMT")
        map.Add("COLLECT_TAX", "COLLECT_TAX")
        map.Add("TOMEOKI", "TOMEOKI")
        map.Add("BR_CD", "BR_CD")
        map.Add("PRINT_CNT", "PRINT_CNT")
        map.Add("KOSU_DISP_FLG", "KOSU_DISP_FLG")
        map.Add("SEIKYUSAKI_CD", "SEIKYUSAKI_CD")
        map.Add("SEIKYUSAKI_BUNRUI_CD", "SEIKYUSAKI_BUNRUI_CD")
        map.Add("UNCHIN_KANRI_NO", "UNCHIN_KANRI_NO")
        map.Add("CARD_HARAI", "CARD_HARAI")
        map.Add("CARD_KAMEI_NO", "CARD_KAMEI_NO")
        map.Add("CARD_NO1", "CARD_NO1")
        map.Add("CARD_NO2", "CARD_NO2")
        map.Add("CARD_NO3", "CARD_NO3")
        map.Add("EMAIL_KBN", "EMAIL_KBN")
        map.Add("EMAIL_ADDRESS", "EMAIL_ADDRESS")
        map.Add("EMAIL_IN_TYPE", "EMAIL_IN_TYPE")
        map.Add("EMAIL_MSG", "EMAIL_MSG")
        map.Add("KANRYO_EMAIL_KBN", "KANRYO_EMAIL_KBN")
        map.Add("KANRYO_EMAIL_ADDRESS", "KANRYO_EMAIL_ADDRESS")
        map.Add("KANRYO_EMAIL_MSG", "KANRYO_EMAIL_MSG")
        map.Add("SYUNOU_KBN", "SYUNOU_KBN")
        map.Add("YOBI", "YOBI")
        map.Add("SYUNOU_SKY_AMT", "SYUNOU_SKY_AMT")
        map.Add("SYUNOU_SKY_TAX", "SYUNOU_SKY_TAX")
        map.Add("SYUNOU_SKY_ZIP", "SYUNOU_SKY_ZIP")
        map.Add("SYUNOU_SKY_ADD", "SYUNOU_SKY_ADD")
        map.Add("SYUNOU_SKY_TATEMONO", "SYUNOU_SKY_TATEMONO")
        map.Add("SYUNOU_SKY_BUMON1", "SYUNOU_SKY_BUMON1")
        map.Add("SYUNOU_SKY_BUMON2", "SYUNOU_SKY_BUMON2")
        map.Add("SYUNOU_SKY_NM_KNJI", "SYUNOU_SKY_NM_KNJI")
        map.Add("SYUNOU_SKY_NM_KANA", "SYUNOU_SKY_NM_KANA")
        map.Add("SYUNOU_TOI_NM_KANJI", "SYUNOU_TOI_NM_KANJI")
        map.Add("SYUNOU_TOI_NM_ZIP", "SYUNOU_TOI_NM_ZIP")
        map.Add("SYUNOU_TOI_ADD", "SYUNOU_TOI_ADD")
        map.Add("SYUNOU_TOI_TATEMONO", "SYUNOU_TOI_TATEMONO")
        map.Add("SYUNOU_TOI_TEL", "SYUNOU_TOI_TEL")
        map.Add("SYUNOU_DAI_KANRI_NO", "SYUNOU_DAI_KANRI_NO")
        map.Add("SYUNOU_DAI_HINMEI", "SYUNOU_DAI_HINMEI")
        map.Add("SYUNOU_DAI_BIKOU", "SYUNOU_DAI_BIKOU")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC890OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC890OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（ヤマトCSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateYamatoB2Csv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC890OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", Me._Row("DENPYO_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC890DAC.SQL_UPDATE_YAMATOB2_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC890DAC", "UpdateYamatoB2Csv", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))

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
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
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


#End Region

End Class
