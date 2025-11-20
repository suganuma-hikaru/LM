' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC900    : 佐川e飛伝 CSV出力
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC900DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC900DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"
#If False Then ' H_OUTKAEDI_Lカラム参照追加 20160830 changed inoue
#Else
    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL SELECT部 FFEM用
    ''' 2016.08.12 update 
    ''' 1/4箇所
    ''' 旧）ADD1_LEN + LEN(SUBSTRING(REMARK,DEST_COL1 + 1,DEST_COL2 - 1)) > 48
    ''' ↓↓↓(担当者名の後ろに様を付けるため1文字減らした) 
    ''' 新）ADD1_LEN + LEN(SUBSTRING(REMARK,DEST_COL1 + 1,DEST_COL2 - 1)) > 47
    ''' 2/4箇所
    ''' 旧）ELSE CASE WHEN LEN(SUBSTRING(REMARK,DEST_COL1 + 1,DEST_COL2 - 1))  >  16
    ''' ↓↓↓(担当者名の後ろに様を付けるため1文字減らした)
    ''' 新）ELSE CASE WHEN LEN(SUBSTRING(REMARK,DEST_COL1 + 1,DEST_COL2 - 1))  >  15
    ''' 3/4箇所
    ''' 旧）ELSE SUBSTRING(REMARK,DEST_COL1 + 1,DEST_COL2 - 1)
    ''' ↓↓↓(担当者名の後ろに様を付ける)
    ''' 新）ELSE SUBSTRING(REMARK,DEST_COL1 + 1,DEST_COL2 - 1) + '様'
    ''' 4/4箇所
    ''' ,CASE WHEN LEN(OUTKAL.DEST_AD_1 + OUTKAL.DEST_AD_2 + OUTKAL.DEST_AD_3 ) > 48 
    '''       THEN ''        
    '''      ELSE CASE WHEN LEN(OUTKAL.DEST_AD_3) > 16 THEN ''     
    '''               ELSE OUTKAL.DEST_AD_3 
    '''           END
    ''' END  AS DEST_AD_3 
    ''' ↓↓↓(参照先をヤマトB2と統一するため、C_OUTKA_LのAD_3からは参照しない)
    ''' ,'' AS DEST_AD_3
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV As String _
        = " SELECT                                                                                                  " & vbNewLine _
        & "    NRS_BR_CD                                                                                            " & vbNewLine _
        & "   ,DENPYO_NO                                                                                            " & vbNewLine _
        & "   ,JYUSYOROKU_CD                                                                                        " & vbNewLine _
        & "   ,DEST_TEL                                                                                             " & vbNewLine _
        & "   ,DEST_ZIP                                                                                             " & vbNewLine _
        & "   ,DEST_AD_1                                                                                            " & vbNewLine _
        & "   ,DEST_AD_2                                                                                            " & vbNewLine _
        & "   ,CASE WHEN DEST_COL1 <> 0                                                                             " & vbNewLine _
        & "          AND DEST_COL2 <> 0                                                                             " & vbNewLine _
        & "         THEN CASE WHEN ADD1_LEN                                                                         " & vbNewLine _
        & "                      + LEN(SUBSTRING(ZFVYDTEKIYO, 1, DEST_COL1 - 1))                                    " & vbNewLine _
        & "                      + LEN(SUBSTRING(ZFVYDTEKIYO, DEST_COL1 + 1, DEST_COL2 - 1)) < 47                   " & vbNewLine _
        & "                   THEN SUBSTRING(ZFVYDTEKIYO                                                            " & vbNewLine _
        & "                                , 1                                                                      " & vbNewLine _
        & "                                , DEST_COL1 - 1)                                                         " & vbNewLine _
        & "                      + ' '                                                                              " & vbNewLine _
        & "                      + SUBSTRING(ZFVYDTEKIYO                                                            " & vbNewLine _
        & "                                , DEST_COL1 + 1                                                          " & vbNewLine _
        & "                                , DEST_COL2 - 1)                                                         " & vbNewLine _
        & "                      + OKURIJOCSV_FREE_C04                                                              " & vbNewLine _
        & "                   ELSE CASE WHEN ADD1_LEN                                                               " & vbNewLine _
        & "                                + LEN(SUBSTRING(ZFVYDTEKIYO                                              " & vbNewLine _
        & "                                              , DEST_COL1 + 1                                            " & vbNewLine _
        & "                                              , DEST_COL2 - 1)) > 48                                     " & vbNewLine _
        & "                             THEN ''                                                                     " & vbNewLine _
        & "                             ELSE CASE WHEN LEN(SUBSTRING(ZFVYDTEKIYO                                    " & vbNewLine _
        & "                                                        , DEST_COL1 + 1                                  " & vbNewLine _
        & "                                                        , DEST_COL2 - 1)) > 16                           " & vbNewLine _
        & "                                       THEN ''                                                           " & vbNewLine _
        & "                                       ELSE SUBSTRING(ZFVYDTEKIYO                                        " & vbNewLine _
        & "                                                    , DEST_COL1 + 1                                      " & vbNewLine _
        & "                                                    , DEST_COL2 - 1)                                     " & vbNewLine _
        & "                                          + OKURIJOCSV_FREE_C04                                          " & vbNewLine _
        & "                                  END                                                                    " & vbNewLine _
        & "                        END                                                                              " & vbNewLine _
        & "              END                                                                                        " & vbNewLine _
        & "         ELSE ''                                                                                         " & vbNewLine _
        & "    END AS DEST_AD_3                                                                                     " & vbNewLine _
        & "   ,DEST_NM_1                                                                                            " & vbNewLine _
        & "   ,DEST_NM_2                                                                                            " & vbNewLine _
        & "   ,OKYAKU_KANRI_NO                                                                                      " & vbNewLine _
        & "   ,OKYAKU_CD                                                                                            " & vbNewLine _
        & "   ,BUSYO_TANTOUSYA                                                                                      " & vbNewLine _
        & "   ,NIOKURININ_TEL                                                                                       " & vbNewLine _
        & "   ,GOIRAINUSHI_TEL                                                                                      " & vbNewLine _
        & "   ,GOIRAINUSHI_ZIP                                                                                      " & vbNewLine _
        & "   ,GOIRAINUSHI_AD1                                                                                      " & vbNewLine _
        & "   ,GOIRAINUSHI_AD2                                                                                      " & vbNewLine _
        & "   ,GOIRAINUSH_NM1                                                                                       " & vbNewLine _
        & "   ,GOIRAINUSH_NM2                                                                                       " & vbNewLine _
        & "   ,NISUGATA_CD                                                                                          " & vbNewLine _
        & "   ,HINMEI1                                                                                              " & vbNewLine _
        & "   ,HINMEI2                                                                                              " & vbNewLine _
        & "   ,HINMEI3                                                                                              " & vbNewLine _
        & "   ,HINMEI4                                                                                              " & vbNewLine _
        & "   ,HINMEI5                                                                                              " & vbNewLine _
        & "   ,SYUKKA_KOSU                                                                                          " & vbNewLine _
        & "   ,BINSYU_SPEED                                                                                         " & vbNewLine _
        & "   ,BINSYU_HINMEI                                                                                        " & vbNewLine _
        & "   ,HAITATSU_BI                                                                                          " & vbNewLine _
        & "   ,HAITATSU_JIKANTAI                                                                                    " & vbNewLine _
        & "   ,HAITATSU_JIKAN                                                                                       " & vbNewLine _
        & "   ,DAIBIKI_KINGAKU                                                                                      " & vbNewLine _
        & "   ,TAX                                                                                                  " & vbNewLine _
        & "   ,KESSAI_SYUBETSU                                                                                      " & vbNewLine _
        & "   ,HOKEN_KINGAKU                                                                                        " & vbNewLine _
        & "   ,HOKEN_KINGAKU_INJI                                                                                   " & vbNewLine _
        & "   ,SEAL1                                                                                                " & vbNewLine _
        & "   ,SEAL2                                                                                                " & vbNewLine _
        & "   ,SEAL3                                                                                                " & vbNewLine _
        & "   ,EIGYOTENDOME                                                                                         " & vbNewLine _
        & "   ,SRC_KBN                                                                                              " & vbNewLine _
        & "   ,EIGYOTEN_CD                                                                                          " & vbNewLine _
        & "   ,MOTOCHAKU_KBN                                                                                        " & vbNewLine _
        & "   ,ROW_NO                                                                                               " & vbNewLine _
        & "   ,SYS_UPD_DATE                                                                                         " & vbNewLine _
        & "   ,SYS_UPD_TIME                                                                                         " & vbNewLine _
        & "   ,FILEPATH                                                                                             " & vbNewLine _
        & "   ,FILENAME                                                                                             " & vbNewLine _
        & "   ,SYS_DATE                                                                                             " & vbNewLine _
        & "   ,SYS_TIME                                                                                             " & vbNewLine _
        & "  FROM (                                                                                                 " & vbNewLine _
        & "   SELECT TOP(1)                                                                                         " & vbNewLine _
        & "    OUTKAL.NRS_BR_CD	            AS NRS_BR_CD                                                            " & vbNewLine _
        & "   ,OUTKAL.OUTKA_NO_L            AS DENPYO_NO                                                            " & vbNewLine _
        & "   ,''                           AS JYUSYOROKU_CD                                                        " & vbNewLine _
        & "   ,OUTKAL.DEST_TEL              AS DEST_TEL                                                             " & vbNewLine _
        & "  , CASE WHEN OUTKAL.DEST_KB = '02'                                                                      " & vbNewLine _
        & "         THEN                                                                                            " & vbNewLine _
        & "              CASE WHEN LEN(EDIL.DEST_ZIP) =  7                                                          " & vbNewLine _
        & "                   THEN SUBSTRING(EDIL.DEST_ZIP, 1, 3)                                                   " & vbNewLine _
        & "                      + '-'                                                                              " & vbNewLine _
        & "                      + SUBSTRING(EDIL.DEST_ZIP, 4, 4)                                                   " & vbNewLine _
        & "                   ELSE EDIL.DEST_ZIP                                                                    " & vbNewLine _
        & "              END                                                                                        " & vbNewLine _
        & "         ELSE                                                                                            " & vbNewLine _
        & "              CASE WHEN LEN(DEST.ZIP) =  7                                                               " & vbNewLine _
        & "                   THEN SUBSTRING(DEST.ZIP, 1, 3)                                                        " & vbNewLine _
        & "                      + '-'                                                                              " & vbNewLine _
        & "                      + SUBSTRING(DEST.ZIP, 4, 4)                                                        " & vbNewLine _
        & "                   ELSE DEST.ZIP                                                                         " & vbNewLine _
        & "              END                                                                                        " & vbNewLine _
        & "    END                                                                    AS DEST_ZIP                   " & vbNewLine _
        & " , CASE OUTKAL.DEST_KB WHEN '01'                                                                         " & vbNewLine _
        & "                       THEN UPPER(OUTKAL.DEST_AD_1)                                                      " & vbNewLine _
        & "                          + UPPER(OUTKAL.DEST_AD_2)                                                      " & vbNewLine _
        & "                       WHEN '02'                                                                         " & vbNewLine _
        & "                       THEN UPPER(EDIL.DEST_AD_1)                                                        " & vbNewLine _
        & "                          + UPPER(EDIL.DEST_AD_2)                                                        " & vbNewLine _
        & "                       ELSE UPPER(DEST.AD_1)                                                             " & vbNewLine _
        & "                          + UPPER(DEST.AD_2)                                                             " & vbNewLine _
        & "   END                                                                                                   " & vbNewLine _
        & " + UPPER(OUTKAL.DEST_AD_3)                                                 AS DEST_AD_1                  " & vbNewLine _
        & " , ''                                                                      AS DEST_AD_2                  " & vbNewLine _
        & " , ''                                                                      AS DEST_AD_3                  " & vbNewLine _
        & "  , CASE OUTKAL.DEST_KB WHEN '01'                                                                        " & vbNewLine _
        & "                        THEN LEN(UPPER(OUTKAL.DEST_AD_1)                                                 " & vbNewLine _
        & "                               + UPPER(OUTKAL.DEST_AD_2)                                                 " & vbNewLine _
        & "                               + UPPER(OUTKAL.DEST_AD_3))                                                " & vbNewLine _
        & "                        WHEN '02'                                                                        " & vbNewLine _
        & "                        THEN LEN(UPPER(EDIL.DEST_AD_1)                                                   " & vbNewLine _
        & "                               + UPPER(EDIL.DEST_AD_2)                                                   " & vbNewLine _
        & "                               + UPPER(OUTKAL.DEST_AD_3))                                                " & vbNewLine _
        & "                        ELSE LEN(UPPER(DEST.AD_1)                                                        " & vbNewLine _
        & "                               + UPPER(DEST.AD_2)                                                        " & vbNewLine _
        & "                               + UPPER(OUTKAL.DEST_AD_3))                                                " & vbNewLine _
        & "                        END                                                AS ADD1_LEN                   " & vbNewLine _
        & "  , OUTKAL.REMARK                                                          AS REMARK   --出荷時注意事項  " & vbNewLine _
        & "  , FJF.ZFVYDTEKIYO                                                        AS ZFVYDTEKIYO                " & vbNewLine _
        & "  , CASE WHEN LEN(FJF.ZFVYDTEKIYO) = 0 THEN 0                                                            " & vbNewLine _
        & "         ELSE CHARINDEX('|', FJF.ZFVYDTEKIYO)                                                            " & vbNewLine _
        & "    END                                                                    AS DEST_COL1                  " & vbNewLine _
        & "  , CASE WHEN CHARINDEX('|', FJF.ZFVYDTEKIYO) = 0 THEN 0                                                 " & vbNewLine _
        & "         ELSE CHARINDEX('|', SUBSTRING(FJF.ZFVYDTEKIYO                                                   " & vbNewLine _
        & "                                     , CHARINDEX('|', FJF.ZFVYDTEKIYO) + 1                               " & vbNewLine _
        & "                                     , 100))                                                             " & vbNewLine _
        & "    END                                                                    AS DEST_COL2                  " & vbNewLine _
        & "  , CASE OUTKAL.DEST_KB WHEN '01' THEN OUTKAL.DEST_NM                                                    " & vbNewLine _
        & "                        WHEN '02' THEN EDIL.DEST_NM                                                      " & vbNewLine _
        & "                        ELSE  DEST.DEST_NM END                             AS DEST_NM_1                  " & vbNewLine _
        & "   ,''                           AS DEST_NM_2                                                            " & vbNewLine _
        & "   ,OUTKAL.OUTKA_NO_L            AS OKYAKU_KANRI_NO                                                      " & vbNewLine _
        & "   ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                                           " & vbNewLine _
        & "         THEN OKURIJOCSV.FREE_C02                                                                        " & vbNewLine _
        & "         WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                                          " & vbNewLine _
        & "         THEN OKURIJOCSVX.FREE_C02                                                                       " & vbNewLine _
        & "         ELSE ''                                                                                         " & vbNewLine _
        & "    END                                                                    AS OKYAKU_CD                  " & vbNewLine _
        & "   ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                                           " & vbNewLine _
        & "         THEN OKURIJOCSV.FREE_C04                                                                        " & vbNewLine _
        & "         WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                                                          " & vbNewLine _
        & "         THEN OKURIJOCSVX.FREE_C04                                                                       " & vbNewLine _
        & "         ELSE ''                                                                                         " & vbNewLine _
        & "    END                                                                    AS OKURIJOCSV_FREE_C04        " & vbNewLine _
        & "   ,''                           AS BUSYO_TANTOUSYA                                                      " & vbNewLine _
        & "   ,''                           AS NIOKURININ_TEL                                                       " & vbNewLine _
        & "   ,CUST.TEL                     AS GOIRAINUSHI_TEL                                                      " & vbNewLine _
        & "   ,CASE WHEN LEN(CUST.ZIP) > 7                                                                          " & vbNewLine _
        & "              THEN CUST.ZIP                                                                              " & vbNewLine _
        & " 			 ELSE CASE WHEN LEN(CUST.ZIP) > 3                                                           " & vbNewLine _
        & " 			                THEN SUBSTRING(CUST.ZIP,1,3) + '-' + SUBSTRING(CUST.ZIP,4,4)                " & vbNewLine _
        & " 							ELSE CUST.ZIP                                                               " & vbNewLine _
        & " 				  END                                                                                   " & vbNewLine _
        & "    END    AS GOIRAINUSHI_ZIP                                                                            " & vbNewLine _
        & "   ,CUST.AD_1                    AS GOIRAINUSHI_AD1                                                      " & vbNewLine _
        & "   ,CUST.AD_2                    AS GOIRAINUSHI_AD2                                                      " & vbNewLine _
        & "   ,CUST.CUST_NM_L               AS GOIRAINUSH_NM1                                                       " & vbNewLine _
        & "   ,''                           AS GOIRAINUSH_NM2                                                       " & vbNewLine _
        & "   ,'008'                        AS NISUGATA_CD                                                          " & vbNewLine _
        & "   ,OUTKAL.CUST_ORD_NO           AS HINMEI1                                                              " & vbNewLine _
        & "   ,''                           AS HINMEI2                                                              " & vbNewLine _
        & "   ,''                           AS HINMEI3                                                              " & vbNewLine _
        & "   ,''                           AS HINMEI4                                                              " & vbNewLine _
        & "   ,''                           AS HINMEI5                                                              " & vbNewLine _
        & "   ,OUTKAL.OUTKA_PKG_NB          AS SYUKKA_KOSU                                                          " & vbNewLine _
        & "   ,''                           AS BINSYU_SPEED                                                         " & vbNewLine _
        & "   ,CASE HIMOKU.ONDO_JOKEN WHEN 'ZA' THEN '003'                                                          " & vbNewLine _
        & "                          WHEN 'ZB' THEN '002'                                                           " & vbNewLine _
        & " 						  ELSE '001'          END   AS BINSYU_HINMEI                                    " & vbNewLine _
        & "   ,OUTKAL.ARR_PLAN_DATE         AS HAITATSU_BI                                                          " & vbNewLine _
        & "   ,'01'                         AS HAITATSU_JIKANTAI                                                    " & vbNewLine _
        & "   ,''                           AS HAITATSU_JIKAN                                                       " & vbNewLine _
        & "   ,''                           AS DAIBIKI_KINGAKU                                                      " & vbNewLine _
        & "   ,''                           AS TAX                                                                  " & vbNewLine _
        & "   ,''                           AS KESSAI_SYUBETSU                                                      " & vbNewLine _
        & "   ,''                           AS HOKEN_KINGAKU                                                        " & vbNewLine _
        & "   ,''                           AS HOKEN_KINGAKU_INJI                                                   " & vbNewLine _
        & "   , '007'                                                     AS SEAL1                                  " & vbNewLine _
        & "   , CASE HIMOKU.ONDO_JOKEN WHEN 'ZA' THEN '002'                                                         " & vbNewLine _
        & "                            WHEN 'ZB' THEN '001'                                                         " & vbNewLine _
        & "                            ELSE ''                                                                      " & vbNewLine _
        & "     END                                                       AS SEAL2                                  " & vbNewLine _
        & "   ,''                           AS SEAL3                                                                " & vbNewLine _
        & "  ,''                            AS EIGYOTENDOME                                                         " & vbNewLine _
        & "  ,''                            AS SRC_KBN                                                              " & vbNewLine _
        & "  ,''                            AS EIGYOTEN_CD                                                          " & vbNewLine _
        & "  ,'1'                           AS MOTOCHAKU_KBN                                                        " & vbNewLine _
        & "  ,@ROW_NO AS ROW_NO                                                                                     " & vbNewLine _
        & "  ,OUTKAL.SYS_UPD_DATE           AS SYS_UPD_DATE                                                         " & vbNewLine _
        & "  ,OUTKAL.SYS_UPD_TIME           AS SYS_UPD_TIME                                                         " & vbNewLine _
        & "  ,@FILEPATH                     AS FILEPATH                                                             " & vbNewLine _
        & "  ,@FILENAME                     AS FILENAME                                                             " & vbNewLine _
        & "  ,@SYS_DATE                     AS SYS_DATE                                                             " & vbNewLine _
        & "  ,@SYS_TIME                     AS SYS_TIME                                                             " & vbNewLine


    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL FROM・WHERE部 FFEM用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV_FROM As String _
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
        & "       AND CUST_DET.SUB_KB      = '0M'   --  CSV出力FFEM専用出力                         " & vbNewLine _
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
        & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD        " & vbNewLine _
        & "                                                              , EDIOUTL.OUTKA_CTL_NO     " & vbNewLine _
        & "                                                       ORDER BY EDIOUTL.NRS_BR_CD        " & vbNewLine _
        & "                                                              , EDIOUTL.EDI_CTL_NO       " & vbNewLine _
        & "                                                   )                                     " & vbNewLine _
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
        & "   ON FJF.NRS_BR_CD                 = TOPEDI.NRS_BR_CD                                   " & vbNewLine _
        & "  AND FJF.OUTKA_CTL_NO              = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                      = OUTKAL.NRS_BR_CD                            " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                   = OUTKAL.OUTKA_NO_L                           " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                   = '20'                                        " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                    = '0'                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                             " & vbNewLine _
        & "   AND OKURIJOCSV.OKURIJO_TP               = '05' --佐川                                 " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                     " & vbNewLine _
        & "   AND OKURIJOCSVX.OKURIJO_TP              = '05' --佐川                                 " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST SHIP_CD_DEST                                                 " & vbNewLine _
        & "   ON SHIP_CD_DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.DEST_CD   = OUTKAL.SHIP_CD_L                                          " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                                   " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "   AND CUST_DET.SET_NAIYO  = '1'   -- FFEM専用                                           " & vbNewLine _
        & " ) AS A                                                                                  " & vbNewLine

#End If

#Region "標準用"


#If False Then ' H_OUTKAEDI_Lカラム参照追加 20160830 changed inoue
#Else

    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL SELECT部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV2 As String _
           = " SELECT TOP(1)                                                                " & vbNewLine _
           & "    OUTKAL.NRS_BR_CD	            AS NRS_BR_CD                                " & vbNewLine _
           & "   ,OUTKAL.OUTKA_NO_L             AS DENPYO_NO                                " & vbNewLine _
           & "   ,''                            AS JYUSYOROKU_CD                            " & vbNewLine _
           & "   ,OUTKAL.DEST_TEL               AS DEST_TEL                                 " & vbNewLine _
           & "   , CASE WHEN OUTKAL.DEST_KB = '02'                                          " & vbNewLine _
           & "          THEN EDIL.DEST_ZIP                                                  " & vbNewLine _
           & "          ELSE DEST.ZIP                                                       " & vbNewLine _
           & "     END                                                      AS DEST_ZIP     " & vbNewLine _
           & "   , CASE OUTKAL.DEST_KB WHEN '01'                                            " & vbNewLine _
           & "                         THEN UPPER(OUTKAL.DEST_AD_1)                         " & vbNewLine _
           & "                            + UPPER(OUTKAL.DEST_AD_2)                         " & vbNewLine _
           & "                         WHEN '02'                                            " & vbNewLine _
           & "                         THEN UPPER(EDIL.DEST_AD_1)                           " & vbNewLine _
           & "                            + UPPER(EDIL.DEST_AD_2)                           " & vbNewLine _
           & "                         ELSE UPPER(DEST.AD_1)                                " & vbNewLine _
           & "                            + UPPER(DEST.AD_2)                                " & vbNewLine _
           & "                         END                                  AS DEST_AD_1    " & vbNewLine _
           & "   ,''                                                        AS DEST_AD_2    " & vbNewLine _
           & "   , CASE OUTKAL.DEST_KB WHEN '00' THEN OUTKAL.DEST_AD_3                      " & vbNewLine _
           & "                         WHEN '01' THEN OUTKAL.DEST_AD_3                      " & vbNewLine _
           & "                         WHEN '02' THEN EDIL.DEST_AD_3                        " & vbNewLine _
           & "                         ELSE  DEST.AD_3 END                  AS DEST_AD_3    " & vbNewLine _
           & "   , CASE OUTKAL.DEST_KB WHEN '01' THEN OUTKAL.DEST_NM                        " & vbNewLine _
           & "                         WHEN '02' THEN EDIL.DEST_NM                          " & vbNewLine _
           & "                         ELSE  DEST.DEST_NM END               AS DEST_NM_1    " & vbNewLine _
           & "   ,''                                                        AS DEST_NM_2    " & vbNewLine _
           & "   ,OUTKAL.OUTKA_NO_L                                   AS OKYAKU_KANRI_NO    " & vbNewLine _
           & "   ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                " & vbNewLine _
           & "         THEN OKURIJOCSV.FREE_C02                                             " & vbNewLine _
           & "         WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                               " & vbNewLine _
           & "         THEN OKURIJOCSVX.FREE_C02                                            " & vbNewLine _
           & "         ELSE ''                                                              " & vbNewLine _
           & "    END                                                 AS OKYAKU_CD          " & vbNewLine _
           & "   ,''                                                  AS BUSYO_TANTOUSYA    " & vbNewLine _
           & "   ,''                                                  AS NIOKURININ_TEL     " & vbNewLine _
           & "   , CASE WHEN SOKO.WH_KB = '01'  -- 自社倉庫                                 " & vbNewLine _
           & "          THEN SOKO.TEL                                                       " & vbNewLine _
           & "          ELSE NRSBR.TEL                                                      " & vbNewLine _
           & "     END                                                AS GOIRAINUSHI_TEL    " & vbNewLine _
           & "   , CASE WHEN SOKO.WH_KB = '01'                                              " & vbNewLine _
           & "            THEN                                                              " & vbNewLine _
           & "                 CASE WHEN LEN(SOKO.ZIP) = 7                                  " & vbNewLine _
           & "                      THEN SUBSTRING(SOKO.ZIP, 1, 3)                          " & vbNewLine _
           & "                         + '-'                                                " & vbNewLine _
           & "                         + SUBSTRING(SOKO.ZIP, 4, 4)                          " & vbNewLine _
           & "                      ELSE SOKO.ZIP                                           " & vbNewLine _
           & "                 END                                                          " & vbNewLine _
           & "            ELSE                                                              " & vbNewLine _
           & "                 CASE WHEN LEN(NRSBR.ZIP) = 7                                 " & vbNewLine _
           & "                      THEN SUBSTRING(NRSBR.ZIP, 1, 3)                         " & vbNewLine _
           & "                         + '-'                                                " & vbNewLine _
           & "                         + SUBSTRING(NRSBR.ZIP, 4, 4)                         " & vbNewLine _
           & "                      ELSE NRSBR.ZIP                                          " & vbNewLine _
           & "                 END                                                          " & vbNewLine _
           & "            END                                         AS GOIRAINUSHI_ZIP    " & vbNewLine _
           & "    , CASE WHEN SOKO.WH_KB = '01'                                             " & vbNewLine _
           & "           THEN LTRIM(RTRIM(SOKO.AD_1))                                       " & vbNewLine _
           & "           ELSE LTRIM(RTRIM(NRSBR.AD_1))                                      " & vbNewLine _
           & "      END                                               AS GOIRAINUSHI_AD1    " & vbNewLine _
           & "    , CASE WHEN SOKO.WH_KB = '01'                                             " & vbNewLine _
           & "           THEN LTRIM(RTRIM(SOKO.AD_2))                                       " & vbNewLine _
           & "           ELSE LTRIM(RTRIM(NRSBR.AD_2))                                      " & vbNewLine _
           & "      END                                               AS GOIRAINUSHI_AD2    " & vbNewLine _
           & "    , NRSBR.NRS_BR_NM                                   AS GOIRAINUSH_NM1     " & vbNewLine _
           & "   ,''                           AS GOIRAINUSH_NM2                            " & vbNewLine _
           & "   ,'008'                        AS NISUGATA_CD                               " & vbNewLine _
           & "   ,OUTKAL.CUST_ORD_NO           AS HINMEI1                                   " & vbNewLine _
           & "   ,''                           AS HINMEI2                                   " & vbNewLine _
           & "   ,''                           AS HINMEI3                                   " & vbNewLine _
           & "   ,''                           AS HINMEI4                                   " & vbNewLine _
           & "   ,''                           AS HINMEI5                                   " & vbNewLine _
           & "   ,OUTKAL.OUTKA_PKG_NB          AS SYUKKA_KOSU                               " & vbNewLine _
           & "   ,''                           AS BINSYU_SPEED                              " & vbNewLine _
           & " 	 ,ISNULL(OKURIJOCSV.FREE_C19,'001')     AS BINSYU_HINMEI       -- 指定なし           " & vbNewLine _
           & "   ,OUTKAL.ARR_PLAN_DATE         AS HAITATSU_BI                               " & vbNewLine _
           & "   ,'00'                         AS HAITATSU_JIKANTAI   -- 指定なし           " & vbNewLine _
           & "   ,''                           AS HAITATSU_JIKAN                            " & vbNewLine _
           & "   ,''                           AS DAIBIKI_KINGAKU                           " & vbNewLine _
           & "   ,''                           AS TAX                                       " & vbNewLine _
           & "   ,''                           AS KESSAI_SYUBETSU                           " & vbNewLine _
           & "   ,''                           AS HOKEN_KINGAKU                             " & vbNewLine _
           & "   ,''                           AS HOKEN_KINGAKU_INJI                        " & vbNewLine _
           & "   ,ISNULL(OKURIJOCSV.FREE_C20,'')                           AS SEAL1                                     " & vbNewLine _
           & "  ,''                                                       AS SEAL2                                     " & vbNewLine _
           & "  ,''                                                       AS SEAL3                                     " & vbNewLine _
           & "  ,''                            AS EIGYOTENDOME                              " & vbNewLine _
           & "  ,''                            AS SRC_KBN                                   " & vbNewLine _
           & "  ,''                            AS EIGYOTEN_CD                               " & vbNewLine _
           & "  ,'1'                           AS MOTOCHAKU_KBN                             " & vbNewLine _
           & "  ,@ROW_NO AS ROW_NO                                                          " & vbNewLine _
           & "  ,OUTKAL.SYS_UPD_DATE           AS SYS_UPD_DATE                              " & vbNewLine _
           & "  ,OUTKAL.SYS_UPD_TIME           AS SYS_UPD_TIME                              " & vbNewLine _
           & "  ,@FILEPATH                     AS FILEPATH                                  " & vbNewLine _
           & "  ,@FILENAME                     AS FILENAME                                  " & vbNewLine _
           & "  ,@SYS_DATE                     AS SYS_DATE                                  " & vbNewLine _
           & "  ,@SYS_TIME                     AS SYS_TIME                                  " & vbNewLine

    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL FROM・WHERE部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV_FROM2 As String _
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
        & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
        & "           OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
        & "       AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
        & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                       " & vbNewLine _
        & "        ON GOODS.NRS_BR_CD    = OUTKAM.NRS_BR_CD                                         " & vbNewLine _
        & "       AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_SOKO SOKO                                                         " & vbNewLine _
        & "   ON SOKO.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                  " & vbNewLine _
        & "  AND SOKO.WH_CD     = OUTKAL.WH_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_NRS_BR AS NRSBR                                                   " & vbNewLine _
        & "   ON NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                 " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "               SELECT                                                                    " & vbNewLine _
        & "                     NRS_BR_CD                                                           " & vbNewLine _
        & "                   , EDI_CTL_NO                                                          " & vbNewLine _
        & "                   , OUTKA_CTL_NO                                                        " & vbNewLine _
        & "               FROM (                                                                    " & vbNewLine _
        & "                       SELECT                                                            " & vbNewLine _
        & "                             EDIOUTL.NRS_BR_CD                                           " & vbNewLine _
        & "                           , EDIOUTL.EDI_CTL_NO                                          " & vbNewLine _
        & "                           , EDIOUTL.OUTKA_CTL_NO                                        " & vbNewLine _
        & "                           , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                  " & vbNewLine _
        & "                             ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD      " & vbNewLine _
        & "                                                                , EDIOUTL.OUTKA_CTL_NO   " & vbNewLine _
        & "                                                         ORDER BY EDIOUTL.NRS_BR_CD      " & vbNewLine _
        & "                                                                , EDIOUTL.EDI_CTL_NO     " & vbNewLine _
        & "                                                     )                                   " & vbNewLine _
        & "                             END AS IDX                                                  " & vbNewLine _
        & "                       FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                               " & vbNewLine _
        & "                       WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
        & "                         AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                           " & vbNewLine _
        & "                         AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                          " & vbNewLine _
        & "                     ) EBASE                                                             " & vbNewLine _
        & "               WHERE EBASE.IDX = 1                                                       " & vbNewLine _
        & "               ) TOPEDI                                                                  " & vbNewLine _
        & "           ON TOPEDI.NRS_BR_CD               = OUTKAL.NRS_BR_CD                          " & vbNewLine _
        & "          AND TOPEDI.OUTKA_CTL_NO            = OUTKAL.OUTKA_NO_L                         " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                   " & vbNewLine _
        & "        ON EDIL.NRS_BR_CD                    = TOPEDI.NRS_BR_CD                          " & vbNewLine _
        & "       AND EDIL.EDI_CTL_NO                   = TOPEDI.EDI_CTL_NO                         " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                      = OUTKAL.NRS_BR_CD                            " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                   = OUTKAL.OUTKA_NO_L                           " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                   = '20'                                        " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                    = '0'                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                             " & vbNewLine _
        & "   --20170324  FREEC18と支店コードJOIN条件追加                                           " & vbNewLine _
        & "   AND OKURIJOCSV.FREE_C18                 = UNSOL.UNSO_BR_CD                           " & vbNewLine _
        & "   AND OKURIJOCSV.OKURIJO_TP               = '05' --佐川                                 " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                     " & vbNewLine _
        & "   AND OKURIJOCSVX.OKURIJO_TP              = '05' --佐川                                 " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD   = @NRS_BR_CD                                                   " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L  = @OUTKA_NO_L                                                  " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "   AND NOT EXISTS (SELECT * FROM LM_MST..M_CUST_DETAILS                                  " & vbNewLine _
        & "                            WHERE M_CUST_DETAILS.NRS_BR_CD   = OUTKAL.NRS_BR_CD          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.CUST_CD     = OUTKAL.CUST_CD_L          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SUB_KB      = '0M'                      " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SYS_DEL_FLG = '0'                       " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SET_NAIYO   = '1')                      " & vbNewLine
#End If
#End Region

    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL GROUP BY部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV_GROUPBY As String = " GROUP BY                                                                            " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD                                                                       " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L                                                                      " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE                                                                 " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C01                                                                    " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM                                                                        " & vbNewLine _
                                                       & " ,NRSBR.ZIP                                                                              " & vbNewLine _
                                                       & " ,NRSBR.AD_1                                                                             " & vbNewLine _
                                                       & " ,NRSBR.AD_2                                                                             " & vbNewLine _
                                                       & " ,NRSBR.AD_3                                                                             " & vbNewLine _
                                                       & " ,NRSBR.TEL                                                                              " & vbNewLine _
                                                       & " ,SOKO.ZIP                                                                               " & vbNewLine _
                                                       & " ,SOKO.AD_1                                                                              " & vbNewLine _
                                                       & " ,SOKO.AD_2                                                                              " & vbNewLine _
                                                       & " ,SOKO.AD_3                                                                              " & vbNewLine _
                                                       & " ,SOKO.TEL                                                                               " & vbNewLine _
                                                       & " ,SOKO.WH_KB                                                                             " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C02                                                                    " & vbNewLine _
                                                       & " ,DEST.CUST_DEST_CD                                                                      " & vbNewLine _
                                                       & " ,OUTKAL.DEST_CD                                                                         " & vbNewLine _
                                                       & " ,DEST2.DEST_NM                                                                          " & vbNewLine _
                                                       & " ,DEST.DEST_NM                                                                           " & vbNewLine _
                                                       & " ,OUTKAL.DEST_NM                                                                         " & vbNewLine _
                                                       & " ,EDIL.DEST_NM                                                                           " & vbNewLine _
                                                       & " ,DEST2.ZIP                                                                              " & vbNewLine _
                                                       & " ,DEST.ZIP                                                                               " & vbNewLine _
                                                       & " ,EDIL.DEST_ZIP                                                                          " & vbNewLine _
                                                       & " ,DEST2.AD_1                                                                             " & vbNewLine _
                                                       & " ,DEST.AD_1                                                                              " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_1                                                                       " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_1                                                                         " & vbNewLine _
                                                       & " ,DEST2.AD_2                                                                             " & vbNewLine _
                                                       & " ,DEST.AD_2                                                                              " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_2                                                                       " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_2                                                                         " & vbNewLine _
                                                       & " ,OUTKAL.DEST_KB                                                                         " & vbNewLine _
                                                       & " ,OUTKAL.DEST_AD_3                                                                       " & vbNewLine _
                                                       & " ,EDIL.DEST_AD_3                                                                         " & vbNewLine _
                                                       & " ,DEST2.TEL                                                                              " & vbNewLine _
                                                       & " ,DEST.TEL                                                                               " & vbNewLine _
                                                       & " ,OUTKAL.DEST_TEL                                                                        " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE                                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C03                                                                    " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C04                                                                    " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C05                                                                    " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C06                                                                    " & vbNewLine _
                                                       & " ,UNSOL.REMARK                                                                           " & vbNewLine _
                                                       & " ,OUTKAL.CUST_ORD_NO                                                                     " & vbNewLine _
                                                       & " ,UNSOL.UNSO_WT                                                                          " & vbNewLine _
                                                       & " ,OUTKAL.CUST_CD_L                                                                       " & vbNewLine _
                                                       & " ,DEST2.JIS                                                                              " & vbNewLine _
                                                       & " ,DEST.JIS                                                                               " & vbNewLine _
                                                       & " ,DEST3.DEST_NM                                                                          " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM                                                                         " & vbNewLine _
                                                       & " ,DEST2.UNCHIN_SEIQTO_CD                                                                 " & vbNewLine _
                                                       & " ,DEST.UNCHIN_SEIQTO_CD                                                                  " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_DATE                                                                    " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_TIME                                                                    " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PKG_NB                                                                    " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L                                                                         " & vbNewLine _
                                                       & " ,OKURIJOCSV.NRS_BR_CD                                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C01                                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C02                                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C03                                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C04                                                                   " & vbNewLine _
                                                       & " ,OKURIJOCSVX.FREE_C05                                                                   " & vbNewLine _
                                                       & " ,OUTKAL.BUYER_ORD_NO                                                                    " & vbNewLine


#End Region

#Region "更新 SQL"

#Region "佐川CSV作成"

    Private Const SQL_UPDATE_SAGAWAEHIDEN_CSV As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
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
    ''' 佐川CSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>佐川CSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSagawaEHidenCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If _Row.Item("CSV_OUTFLG").ToString.Trim = "0" Then
            '標準
            Me._StrSql.Append(LMC900DAC.SQL_SELECT_SAGAWAEHIDEN_CSV2)
            Me._StrSql.Append(LMC900DAC.SQL_SELECT_SAGAWAEHIDEN_CSV_FROM2)

        ElseIf _Row.Item("CSV_OUTFLG").ToString.Trim = "1" Then
            'FFEM
            Me._StrSql.Append(LMC900DAC.SQL_SELECT_SAGAWAEHIDEN_CSV)
            Me._StrSql.Append(LMC900DAC.SQL_SELECT_SAGAWAEHIDEN_CSV_FROM)

        End If

        ''Me._StrSql.Append(LMC900DAC.SQL_SELECT_SAGAWAEHIDEN_CSV_GROUPBY)

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

        MyBase.Logger.WriteSQLLog("LMC900DAC", "SelectSagawaEHidenCsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("JYUSYOROKU_CD", "JYUSYOROKU_CD")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_NM_1", "DEST_NM_1")
        map.Add("DEST_NM_2", "DEST_NM_2")
        map.Add("OKYAKU_KANRI_NO", "OKYAKU_KANRI_NO")
        map.Add("OKYAKU_CD", "OKYAKU_CD")
        map.Add("BUSYO_TANTOUSYA", "BUSYO_TANTOUSYA")
        map.Add("NIOKURININ_TEL", "NIOKURININ_TEL")
        map.Add("GOIRAINUSHI_TEL", "GOIRAINUSHI_TEL")
        map.Add("GOIRAINUSHI_ZIP", "GOIRAINUSHI_ZIP")
        map.Add("GOIRAINUSHI_AD1", "GOIRAINUSHI_AD1")
        map.Add("GOIRAINUSHI_AD2", "GOIRAINUSHI_AD2")
        map.Add("GOIRAINUSH_NM1", "GOIRAINUSH_NM1")
        map.Add("GOIRAINUSH_NM2", "GOIRAINUSH_NM2")
        map.Add("NISUGATA_CD", "NISUGATA_CD")
        map.Add("HINMEI1", "HINMEI1")
        map.Add("HINMEI2", "HINMEI2")
        map.Add("HINMEI3", "HINMEI3")
        map.Add("HINMEI4", "HINMEI4")
        map.Add("HINMEI5", "HINMEI5")
        map.Add("SYUKKA_KOSU", "SYUKKA_KOSU")
        map.Add("BINSYU_SPEED", "BINSYU_SPEED")
        map.Add("BINSYU_HINMEI", "BINSYU_HINMEI")
        map.Add("HAITATSU_BI", "HAITATSU_BI")
        map.Add("HAITATSU_JIKANTAI", "HAITATSU_JIKANTAI")
        map.Add("HAITATSU_JIKAN", "HAITATSU_JIKAN")
        map.Add("DAIBIKI_KINGAKU", "DAIBIKI_KINGAKU")
        map.Add("TAX", "TAX")
        map.Add("KESSAI_SYUBETSU", "KESSAI_SYUBETSU")
        map.Add("HOKEN_KINGAKU", "HOKEN_KINGAKU")
        map.Add("HOKEN_KINGAKU_INJI", "HOKEN_KINGAKU_INJI")
        map.Add("SEAL1", "SEAL1")
        map.Add("SEAL2", "SEAL2")
        map.Add("SEAL3", "SEAL3")
        map.Add("EIGYOTENDOME", "EIGYOTENDOME")
        map.Add("SRC_KBN", "SRC_KBN")
        map.Add("EIGYOTEN_CD", "EIGYOTEN_CD")
        map.Add("MOTOCHAKU_KBN", "MOTOCHAKU_KBN")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC900OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC900OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（佐川CSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSagawaEHidenCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC900OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", Me._Row("DENPYO_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC900DAC.SQL_UPDATE_SAGAWAEHIDEN_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC900DAC", "UpdateSagawaEHidenCsv", cmd)

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
