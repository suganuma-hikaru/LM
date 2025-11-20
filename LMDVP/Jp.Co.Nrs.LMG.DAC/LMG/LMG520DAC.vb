' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG520DAC : 請求鑑 (値引表示有)
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

Public Class LMG520DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 出力対象帳票パターン取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT                                                     " & vbNewLine _
                                            & "HED.NRS_BR_CD                     AS  NRS_BR_CD            " & vbNewLine _
                                            & ",CASE WHEN @KAGAMI_PTN = '01'THEN '53'                     " & vbNewLine _
                                            & "      WHEN @KAGAMI_PTN = '00'THEN '77'                     " & vbNewLine _
                                            & "      END                                AS PTN_ID         " & vbNewLine _
                                            & ",CASE WHEN @KAGAMI_PTN = '01'THEN MR01.PTN_CD              " & vbNewLine _
                                            & "      WHEN @KAGAMI_PTN = '00'THEN MR02.PTN_CD              " & vbNewLine _
                                            & "      END                                AS PTN_CD         " & vbNewLine _
                                            & "--CASE WHEN @KAGAMI_PTN = '01'THEN MR01.RPT_ID              " & vbNewLine _
                                            & "--     WHEN @KAGAMI_PTN = '00'THEN MR02.RPT_ID              " & vbNewLine _
                                            & "--      END                                AS RPT_ID         " & vbNewLine _
                                            & " ,CASE WHEN SUBSTRING(HED.SKYU_DATE,1,6) < @RPT_CHG_START_YM                 " & vbNewLine _
                                            & "      THEN                                                                   " & vbNewLine _
                                            & "         CASE WHEN @KAGAMI_PTN = '01'THEN MR01.RPT_ID  + '_OLD'              " & vbNewLine _
                                            & "              WHEN @KAGAMI_PTN = '00'THEN MR02.RPT_ID  + '_OLD'              " & vbNewLine _
                                            & "         END                                                                 " & vbNewLine _
                                            & "      ELSE                                                                   " & vbNewLine _
                                            & "         CASE WHEN @KAGAMI_PTN = '01'THEN MR01.RPT_ID                        " & vbNewLine _
                                            & "              WHEN @KAGAMI_PTN = '00'THEN MR02.RPT_ID                        " & vbNewLine _
                                            & "         END                                                                 " & vbNewLine _
                                            & "      END                    AS RPT_ID                                       " & vbNewLine _
                                            & "FROM                                                       " & vbNewLine _
                                            & " $LM_TRN$..G_KAGAMI_HED AS HED                               " & vbNewLine _
                                            & " LEFT OUTER JOIN    $LM_MST$..M_SEIQTO  AS SEQT              " & vbNewLine _
                                            & "   ON  HED.NRS_BR_CD = SEQT.NRS_BR_CD                      " & vbNewLine _
                                            & "   AND HED.SEIQTO_CD = SEQT.SEIQTO_CD                      " & vbNewLine _
                                            & " LEFT OUTER JOIN    $LM_MST$..M_RPT     AS MR01              " & vbNewLine _
                                            & "   ON  MR01.NRS_BR_CD  = SEQT.NRS_BR_CD                    " & vbNewLine _
                                            & "   AND MR01.PTN_ID     = '53'                              " & vbNewLine _
                                            & "   AND MR01.PTN_CD     = SEQT.DOC_PTN                      " & vbNewLine _
                                            & "   AND MR01.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                            & " LEFT OUTER JOIN    $LM_MST$..M_RPT     AS MR02              " & vbNewLine _
                                            & "   ON  MR02.NRS_BR_CD  = SEQT.NRS_BR_CD                    " & vbNewLine _
                                            & "   AND MR02.PTN_ID     = '77'                              " & vbNewLine _
                                            & "   AND MR02.PTN_CD     = SEQT.DOC_PTN2                     " & vbNewLine _
                                            & "   AND MR02.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                            & "WHERE HED.SKYU_NO = @SKYU_NO                               " & vbNewLine

    ''' <summary>
    ''' 請求書鑑出力対象データ検索用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                       " & vbNewLine _
                                            & " CASE WHEN SUBSTRING(HED.SKYU_DATE,1,6) < @RPT_CHG_START_YM                  " & vbNewLine _
                                            & "      THEN                                                                   " & vbNewLine _
                                            & "         CASE WHEN @KAGAMI_PTN = '01'THEN MR.RPT_ID  + '_OLD'                " & vbNewLine _
                                            & "              WHEN @KAGAMI_PTN = '00'THEN MR2.RPT_ID  + '_OLD'               " & vbNewLine _
                                            & "         END                                                                 " & vbNewLine _
                                            & "      ELSE                                                                   " & vbNewLine _
                                            & "         CASE WHEN @KAGAMI_PTN = '01'THEN MR.RPT_ID                          " & vbNewLine _
                                            & "              WHEN @KAGAMI_PTN = '00'THEN MR2.RPT_ID                         " & vbNewLine _
                                            & "         END                                                                 " & vbNewLine _
                                            & "      END                    AS RPT_ID,                                      " & vbNewLine _
                                            & "--CASE WHEN @KAGAMI_PTN = '01'THEN MR.RPT_ID                                   " & vbNewLine _
                                            & "--      WHEN @KAGAMI_PTN = '00'THEN MR2.RPT_ID                                 " & vbNewLine _
                                            & "--      END                    AS RPT_ID,                                      " & vbNewLine _
                                            & "   HED.SEIQTO_NM             AS SEIQTO_NM,                                   " & vbNewLine _
                                            & "   HED.SEIQTO_PIC            AS SEIQTO_PIC,                                  " & vbNewLine _
                                            & "   HED.SKYU_NO               AS SKYU_NO,                                     " & vbNewLine _
                                            & "   HED.SKYU_DATE             AS SKYU_DATE,                                   " & vbNewLine _
                                            & "   SEQT.MEIGI_KB             AS MEIGI_KB,                                    " & vbNewLine _
                                            & "   SEQT.KOUZA_KB             AS KOUZA_KB,                                    " & vbNewLine _
                                            & "   CASE WHEN SUBSTRING(HED.SKYU_DATE, 1, 6) < @RPT_CHG_START_YM              " & vbNewLine _
                                            & "       THEN ISNULL(OLD_HONSYA_NM.KBN_NM1, SEQT.KBN_NM2)                      " & vbNewLine _
                                            & "       ELSE SEQT.KBN_NM2                                                     " & vbNewLine _
                                            & "       END                   AS HONSYA_NM,                                   " & vbNewLine _
                                            & "   SEQT.KBN_NM3              AS HONSYA_ADD1,                                 " & vbNewLine _
                                            & "   ''                        AS HONSYA_ADD2,                                 " & vbNewLine _
                                            & "   SEQT.KBN_NM4              AS HONSYA_TEL,                                  " & vbNewLine _
                                            & "   CASE WHEN SUBSTRING(HED.SKYU_DATE, 1, 6) < @RPT_CHG_START_YM              " & vbNewLine _
                                            & "       THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRSBR.NRS_BR_NM)                   " & vbNewLine _
                                            & "       ELSE NRSBR.NRS_BR_NM                                                  " & vbNewLine _
                                            & "       END                   AS CENTER_NM,                                   " & vbNewLine _
                                            & "   KBN01.KBN_NM5             AS CENTER_ADD1,                                 " & vbNewLine _
                                            & "   KBN01.KBN_NM6             AS CENTER_ADD2,                                 " & vbNewLine _
                                            & "   KBN01.KBN_NM7             AS CENTER_TEL,                                  " & vbNewLine _
                                            & "   CASE WHEN SUBSTRING(HED.SKYU_DATE, 1, 6) < @RPT_CHG_START_YM              " & vbNewLine _
                                            & "       THEN                                                                  " & vbNewLine _
                                            & "           CASE WHEN SEQT.MEIGI_KB = '00' THEN ISNULL(OLD_HONSYA_NM.KBN_NM1, SEQT.KBN_NM2) " & vbNewLine _
                                            & "           WHEN SEQT.MEIGI_KB = '01' THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRSBR.NRS_BR_NM)   " & vbNewLine _
                                            & "           ELSE '' END                                                       " & vbNewLine _
                                            & "       ELSE                                                                  " & vbNewLine _
                                            & "           CASE WHEN SEQT.MEIGI_KB = '00' THEN SEQT.KBN_NM2                  " & vbNewLine _
                                            & "           WHEN SEQT.MEIGI_KB = '01' THEN NRSBR.NRS_BR_NM                    " & vbNewLine _
                                            & "           ELSE '' END                                                       " & vbNewLine _
                                            & "       END                   AS HAKKO_MOTO_NM,                               " & vbNewLine _
                                            & "   CASE WHEN SEQT.MEIGI_KB = '00' THEN SEQT.KBN_NM3                          " & vbNewLine _
                                            & "   WHEN SEQT.MEIGI_KB = '01' THEN KBN01.KBN_NM5                              " & vbNewLine _
                                            & "   ELSE '' END               AS HAKKO_MOTO_ADD1,                             " & vbNewLine _
                                            & "   CASE WHEN SEQT.MEIGI_KB = '00' THEN ''                                    " & vbNewLine _
                                            & "   WHEN SEQT.MEIGI_KB = '01' THEN KBN01.KBN_NM6                              " & vbNewLine _
                                            & "   ELSE '' END               AS HAKKO_MOTO_ADD2,                             " & vbNewLine _
                                            & "   CASE WHEN SEQT.MEIGI_KB = '00' THEN SEQT.KBN_NM4                          " & vbNewLine _
                                            & "   WHEN SEQT.MEIGI_KB = '01' THEN KBN01.KBN_NM7                              " & vbNewLine _
                                            & "   ELSE '' END               AS HAKKO_MOTO_TEL,                              " & vbNewLine _
                                            & "   '' AS SKYU_SYOSYU,                                                        " & vbNewLine _
                                            & "   SUBSTRING(HED.SKYU_DATE,5,2) AS SKYU_MONTH,                               " & vbNewLine _
                                            & "   HED.REMARK                AS REMARK,                                      " & vbNewLine _
                                            & "   TAX.TAX_RATE              AS TAX_RATE,                                    " & vbNewLine _
                                            & "   HED.NEBIKI_RT1            AS NEBIKI_RT1,                                  " & vbNewLine _
                                            & "   HED.NEBIKI_GK1            AS NEBIKI_GK1,                                  " & vbNewLine _
                                            & "   HED.TAX_GK1               AS TAX_GK1,                                     " & vbNewLine _
                                            & "   HED.TAX_HASU_GK1          AS TAX_HASU_GK1,                                " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "   CASE WHEN DTL.ITEM_CURR_CD = HED.EX_SAKI_CURR_CD AND HED.INV_CURR_CD = HED.EX_MOTO_CURR_CD THEN  HED.TAX_GK1 / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        WHEN DTL.ITEM_CURR_CD = HED.EX_MOTO_CURR_CD AND HED.INV_CURR_CD = HED.EX_SAKI_CURR_CD THEN  HED.TAX_GK1 * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        ELSE HED.TAX_GK1                                                 " & vbNewLine _
                                            & "        END TAX_GK1_SEIQ,                                                " & vbNewLine _
                                            & "   CASE WHEN DTL.ITEM_CURR_CD = HED.EX_SAKI_CURR_CD AND HED.INV_CURR_CD = HED.EX_MOTO_CURR_CD THEN  HED.TAX_HASU_GK1 / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        WHEN DTL.ITEM_CURR_CD = HED.EX_MOTO_CURR_CD AND HED.INV_CURR_CD = HED.EX_SAKI_CURR_CD THEN  HED.TAX_HASU_GK1 * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        ELSE HED.TAX_HASU_GK1                                                 " & vbNewLine _
                                            & "        END TAX_HASU_GK1_SEIQ,                                                " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   HED.CRT_KB                AS CRT_KB,                                       " & vbNewLine _
                                            & "   HED.NEBIKI_RT2            AS NEBIKI_RT2,                                  " & vbNewLine _
                                            & "   HED.NEBIKI_GK2            AS NEBIKI_GK2,                                  " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "   CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END EX_RATE,       " & vbNewLine _
                                            & "--   CASE WHEN HED.EX_MOTO_CURR_CD = '' THEN 'JPY' ELSE HED.EX_MOTO_CURR_CD END EX_MOTO_CURR_CD,  " & vbNewLine _
                                            & "--   CASE WHEN HED.EX_SAKI_CURR_CD = '' THEN 'JPY' ELSE HED.EX_SAKI_CURR_CD END EX_SAKI_CURR_CD,  " & vbNewLine _
                                            & "   KBNC025_MOTO.KBN_NM1      AS EX_MOTO_CURR_CD,                             " & vbNewLine _
                                            & "   KBNC025_SAKI.KBN_NM1      AS EX_SAKI_CURR_CD,                             " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   ''                        AS DETAIL_MIDASHI,                              " & vbNewLine _
                                            & "   DTL.GROUP_KB              AS GROUP_KB,                                    " & vbNewLine _
                                            & "   DTL.SEIQKMK_CD            AS SEIQKMK_CD,                                  " & vbNewLine _
                                            & "   DTL.SEIQKMK_NM            AS SEIQKMK_NM,                                  " & vbNewLine _
                                            & "   DTL.TAX_KB                AS TAX_KB,                                      " & vbNewLine _
                                            & "   DTL.KBN_NM6               AS BUSYO_CD,                                    " & vbNewLine _
                                            & "   DTL.KBN_NM4               AS SUMMARY_CODE,                                " & vbNewLine _
                                            & "   DTL.KEISAN_TLGK           AS KEISAN_TLGK,                                 " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "   CASE WHEN DTL.ITEM_CURR_CD = HED.EX_SAKI_CURR_CD AND HED.INV_CURR_CD = HED.EX_MOTO_CURR_CD THEN  DTL.KEISAN_TLGK / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        WHEN DTL.ITEM_CURR_CD = HED.EX_MOTO_CURR_CD AND HED.INV_CURR_CD = HED.EX_SAKI_CURR_CD THEN  DTL.KEISAN_TLGK * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        ELSE DTL.KEISAN_TLGK                                                 " & vbNewLine _
                                            & "        END KEISAN_TLGK_SEIQ,                                                " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   DTL.NEBIKI_RT             AS NEBIKI_RT,                                   " & vbNewLine _
                                            & "   DTL.NEBIKI_GK             AS KOTEI_NEBIKI_GK,                             " & vbNewLine _
                                            & "   DTL.NEBIKI_RTGK           AS RT_NEBIKI_GK,                                " & vbNewLine _
                                            & "   DTL.NEBIKI_RTGK + DTL.NEBIKI_GK AS NEBIKI_GK,                             " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "   CASE WHEN DTL.ITEM_CURR_CD = HED.EX_SAKI_CURR_CD AND HED.INV_CURR_CD = HED.EX_MOTO_CURR_CD THEN  DTL.NEBIKI_RTGK / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) + DTL.NEBIKI_GK / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        WHEN DTL.ITEM_CURR_CD = HED.EX_MOTO_CURR_CD AND HED.INV_CURR_CD = HED.EX_SAKI_CURR_CD THEN  DTL.NEBIKI_RTGK * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) + DTL.NEBIKI_GK * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) " & vbNewLine _
                                            & "        ELSE DTL.NEBIKI_RTGK + DTL.NEBIKI_GK                                 " & vbNewLine _
                                            & "        END NEBIKI_GK_SEIQ,                                                  " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   DTL.KEISAN_TLGK - (DTL.NEBIKI_RTGK + DTL.NEBIKI_GK) AS KINGAKU,           " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "   CASE WHEN DTL.ITEM_CURR_CD = HED.EX_SAKI_CURR_CD AND HED.INV_CURR_CD = HED.EX_MOTO_CURR_CD THEN  (DTL.KEISAN_TLGK / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END)) - (DTL.NEBIKI_RTGK / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) + DTL.NEBIKI_GK / (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END)) " & vbNewLine _
                                            & "        WHEN DTL.ITEM_CURR_CD = HED.EX_MOTO_CURR_CD AND HED.INV_CURR_CD = HED.EX_SAKI_CURR_CD THEN  (DTL.KEISAN_TLGK * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END)) - (DTL.NEBIKI_RTGK * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END) + DTL.NEBIKI_GK * (CASE WHEN HED.EX_RATE = 0.000 THEN 1.000 ELSE HED.EX_RATE END)) " & vbNewLine _
                                            & "        ELSE DTL.KEISAN_TLGK - (DTL.NEBIKI_RTGK + DTL.NEBIKI_GK)             " & vbNewLine _
                                            & "        END KINGAKU_SEIQ,                                                    " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "--   CASE WHEN DTL.ITEM_CURR_CD = '' THEN 'JPY' ELSE DTL.ITEM_CURR_CD END ITEM_CURR_CD,  " & vbNewLine _
                                            & "   KBNC025_ITEM.KBN_NM1           AS ITEM_CURR_CD,                           " & vbNewLine _
                                            & "   KBNC025_SEIQ.KBN_NM1           AS SEIQ_CURR_CD,                           " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   DTL.KBN_NM1               AS TAX_KB_NM,                                   " & vbNewLine _
                                            & "   DTL.TEKIYO                AS TEKIYO,                                      " & vbNewLine _
                                            & "   DTL.PRINT_SORT            AS PRINT_SORT,                                  " & vbNewLine _
                                            & "   DTL.SKYU_SUB_NO           AS SKYU_SUB_NO,                                 " & vbNewLine _
                                            & "   DTL.MAKE_SYU_KB           AS MAKE_SYU_KB,                                 " & vbNewLine _
                                            & "   SEQT.BANK_NM              AS BANK_NM,                                     " & vbNewLine _
                                            & "   SEQT.YOKIN_SYU            AS YOKIN_SYU,                                   " & vbNewLine _
                                            & "   SEQT.KOZA_NO              AS KOZA_NO,                                     " & vbNewLine _
                                            & "   CASE WHEN SUBSTRING(HED.SKYU_DATE, 1, 6) < @RPT_CHG_START_YM              " & vbNewLine _
                                            & "       THEN ISNULL(OLD_BANK_MEIGI_NM.KBN_NM1, SEQT.MEIGI_NM)                 " & vbNewLine _
                                            & "       ELSE SEQT.MEIGI_NM                                                    " & vbNewLine _
                                            & "       END                   AS MEIGI_NM,                                    " & vbNewLine _
                                            & "   SEQT.YOKIN_SYU + ' ' + SEQT.KOZA_NO AS YOKIN_INFO,                        " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "   ISNULL(CURR_ITEM.ROUND_POS,0)            AS ITEM_ROUND_POS,               " & vbNewLine _
                                            & "   ISNULL(CURR_SEIQ.ROUND_POS,0)            AS SEIQ_ROUND_POS                " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "   --20160705                " & vbNewLine _
                                            & "   ,ISNULL(DTL.KANJO,'')  AS KANJO                                           " & vbNewLine _
                                            & "   ,ISNULL(ARC.RC_NM,'')  AS KANJO_NM                                        " & vbNewLine _
                                            & "   --20161116                                 " & vbNewLine _
                                            & "   ,DTL.TEMPLATE_IMP_FLG           AS TEMPLATE_IMP_FLG                       " & vbNewLine _
                                            & "   ,S_USER.USER_NM                 AS KAGAMI_ENT_USER_NM                     " & vbNewLine _
                                            & "   ,SEQT.NRS_KEIRI_CD2        AS JDE_CD            ---- ADD 20170530 QRｺｰﾄﾞ対応                     " & vbNewLine _
                                            & "   ,KBNQR.KBN_NM1             AS QR_SYSTEM_ID      ---- ADD 20170530 QRｺｰﾄﾞ対応                     " & vbNewLine _
                                            & "   ,KBNQR.KBN_NM2             AS QR_SYSTEM_ID_NM   ---- ADD 20170530 QRｺｰﾄﾞ対応                     " & vbNewLine _
                                            & "   ,KBNQR.KBN_NM3             AS QR_REC_TYP        ---- ADD 20170530 QRｺｰﾄﾞ対応                     " & vbNewLine _
                                            & "   ,KBNQR.KBN_NM4             AS QR_REC_TYP_NM     ---- ADD 20170530 QRｺｰﾄﾞ対応                     " & vbNewLine _
                                            & "   ,KBNQR.KBN_NM1 + '_' + KBNQR.KBN_NM2 +  '_' +  KBNQR.KBN_NM3 +  '_' +  KBNQR.KBN_NM4  AS QR_FOLDER  " & vbNewLine _
                                            & "   ,ISNULL(KBNG014.KBN_CD,'') AS KBNG014_FLG   --ADD 2020/09/15 振込手数料を含む記載                " & vbNewLine _
                                            & "   ,SEQT.DOC_DEST_YN          AS DOC_DEST_YN   --ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,ISNULL(KBNG202.KBN_NM1,'') AS ATENA_RPR_ID --ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,SEQT.ZIP                  AS SEIQTO_ZIP    --ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,SEQT.AD_1                 AS SEIQTO_AD1   --ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,SEQT.AD_2                 AS SEIQTO_AD2   --ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,SEQT.AD_3                 AS SEIQTO_AD3   --ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,SEQT.SEIQTO_BUSYO_NM      AS SEIQTO_BUSYO_NM                             " & vbNewLine _
                                            & "   ,SEQT.OYA_PIC              AS SEIQTO_OYA_PIC                              " & vbNewLine _
                                            & "   ,CASE WHEN SUBSTRING(HED.SKYU_DATE, 1, 6) < @RPT_CHG_START_YM             " & vbNewLine _
                                            & "       THEN ISNULL(OLD_NRS_BR_NM.KBN_NM1, NRSBR.NRS_BR_NM)                   " & vbNewLine _
                                            & "       ELSE NRSBR.NRS_BR_NM                                                  " & vbNewLine _
                                            & "       END                    AS NRS_BR_NM                                   " & vbNewLine _
                                            & "-- ,NRSBR.NRS_BR_NM           AS NRS_BR_NM     --ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,NRSBR.ZIP                 AS NRS_BR_ZIP    --ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,NRSBR.AD_1                AS NRS_BR_AD1    --ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,NRSBR.AD_2                AS NRS_BR_AD2    --ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,NRSBR.AD_3                AS NRS_BR_AD3    --ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入 " & vbNewLine _
                                            & "   ,SEQT.NRS_BR_CD            AS NRS_BR_CD     --ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼    " & vbNewLine _
                                            & "   ,ISNULL(KBNB1S.KBN_NM1, KBNB1.KBN_NM1)  AS BANK_NM1     " & vbNewLine _
                                            & "   ,ISNULL(KBNB1S.KBN_NM2, KBNB1.KBN_NM2)  AS BANK_BR1     " & vbNewLine _
                                            & "   ,ISNULL(KBNB1S.KBN_NM3, KBNB1.KBN_NM3)  AS BANK_ACC1    " & vbNewLine _
                                            & "   ,ISNULL(KBNB2S.KBN_NM1, KBNB2.KBN_NM1)  AS BANK_NM2     " & vbNewLine _
                                            & "   ,ISNULL(KBNB2S.KBN_NM2, KBNB2.KBN_NM2)  AS BANK_BR2     " & vbNewLine _
                                            & "   ,ISNULL(KBNB2S.KBN_NM3, KBNB2.KBN_NM3)  AS BANK_ACC2    " & vbNewLine _
                                            & "   ,ISNULL(KBNB3S.KBN_NM1, KBNB3.KBN_NM1)  AS BANK_NM3     " & vbNewLine _
                                            & "   ,ISNULL(KBNB3S.KBN_NM2, KBNB3.KBN_NM2)  AS BANK_BR3     " & vbNewLine _
                                            & "   ,ISNULL(KBNB3S.KBN_NM3, KBNB3.KBN_NM3)  AS BANK_ACC3    " & vbNewLine _
                                            & "   ,ISNULL(KBNT.KBN_NM1,'')   AS T_NO         " & vbNewLine _
                                            & "   ,ISNULL(KBNT.KBN_NM2,'')   AS BANK__NM0    " & vbNewLine _
                                            & "   ,ISNULL(KBNT.KBN_NM3,'')   AS FURIKOMI_MEMO        --ADD 2022/06/29 029595 " & vbNewLine _
                                            & "FROM                                                                         " & vbNewLine _
                                            & "   --(2014.09.10) 追加START 多通貨対応                                       " & vbNewLine _
                                            & " (SELECT                                                                     " & vbNewLine _
                                            & "         MAX(START_DATE) AS START_DATE                                       " & vbNewLine _
                                            & "        ,MTAX.TAX_CD                                                         " & vbNewLine _
                                            & "        ,MTAX.TAX_RATE                                                       " & vbNewLine _
                                            & "		,KBN_Z001.KBN_CD                                                        " & vbNewLine _
                                            & "		FROM                                                                    " & vbNewLine _
                                            & "		(                                                                       " & vbNewLine _
                                            & "         SELECT                                                              " & vbNewLine _
                                            & "		        MBR.COUNTRY_CD AS COUNTRY_CD                                    " & vbNewLine _
                                            & "			   ,KBN.TAX_CD    AS TAX_CD                                         " & vbNewLine _
                                            & "			   ,KBN.KBN_CD    AS KBN_CD                                         " & vbNewLine _
                                            & "		       FROM $LM_MST$..M_NRS_BR MBR                                      " & vbNewLine _
                                            & "		       LEFT OUTER JOIN                                                  " & vbNewLine _
                                            & "			   (SELECT                                                          " & vbNewLine _
                                            & "				KBN_GROUP_CD                                                    " & vbNewLine _
                                            & "				,KBN_CD                                                         " & vbNewLine _
                                            & "				,MIN(KBN_NM3) AS TAX_CD                                         " & vbNewLine _
                                            & "				,KBN_NM7                                                        " & vbNewLine _
                                            & "				,KBN_NM8                                                        " & vbNewLine _
                                            & "			    FROM $LM_MST$..Z_KBN                                            " & vbNewLine _
                                            & "			    WHERE KBN_GROUP_CD = 'Z001'                                     " & vbNewLine _
                                            & "			      AND KBN_NM8      = '01'                                       " & vbNewLine _
                                            & "			      AND SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & "		   	    GROUP BY                                                        " & vbNewLine _
                                            & "				 KBN_GROUP_CD                                                   " & vbNewLine _
                                            & "				,KBN_CD                                                         " & vbNewLine _
                                            & "				,KBN_NM7                                                        " & vbNewLine _
                                            & "				,KBN_NM8                                                        " & vbNewLine _
                                            & "			    ) AS KBN                                                        " & vbNewLine _
                                            & "		        ON MBR.COUNTRY_CD = KBN.KBN_NM7                                 " & vbNewLine _
                                            & "		        WHERE MBR.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                            & "		          AND MBR.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                            & "				GROUP BY                                                        " & vbNewLine _
                                            & "				MBR.COUNTRY_CD                                                  " & vbNewLine _
                                            & "			   ,KBN.TAX_CD                                                      " & vbNewLine _
                                            & "			   ,KBN.KBN_CD                                                      " & vbNewLine _
                                            & "		) KBN_Z001                                                              " & vbNewLine _
                                            & "		        LEFT OUTER JOIN                                                 " & vbNewLine _
                                            & "             $LM_MST$..M_TAX MTAX                                            " & vbNewLine _
                                            & "             ON                                                              " & vbNewLine _
                                            & "				MTAX.TAX_CD = KBN_Z001.TAX_CD                                   " & vbNewLine _
                                            & "       WHERE                                                                 " & vbNewLine _
                                            & "	     MTAX.START_DATE <= @SKYU_DATE                                          " & vbNewLine _
                                            & "      GROUP BY                                                               " & vbNewLine _
                                            & "	     MTAX.TAX_CD                                                            " & vbNewLine _
                                            & "	    ,MTAX.TAX_RATE                                                          " & vbNewLine _
                                            & "		,KBN_Z001.KBN_CD                                                        " & vbNewLine _
                                            & "		HAVING                                                                  " & vbNewLine _
                                            & "		EXISTS                                                                  " & vbNewLine _
                                            & "		(SELECT                                                                 " & vbNewLine _
                                            & "            MAX(EXT_TAX.START_DATE) AS START_DATE                            " & vbNewLine _
                                            & "           ,EXT_TAX.TAX_CD                                                   " & vbNewLine _
                                            & "       FROM $LM_MST$..M_TAX EXT_TAX                                          " & vbNewLine _
                                            & "       WHERE EXT_TAX.START_DATE <= @SKYU_DATE                                " & vbNewLine _
                                            & "         AND EXT_TAX.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "       GROUP BY EXT_TAX.TAX_CD                                               " & vbNewLine _
                                            & "	   HAVING                                                                   " & vbNewLine _
                                            & "	   MTAX.TAX_CD = EXT_TAX.TAX_CD                                             " & vbNewLine _
                                            & "	   AND                                                                      " & vbNewLine _
                                            & "	   MAX(MTAX.START_DATE) = MAX(EXT_TAX.START_DATE)                           " & vbNewLine _
                                            & "	   )) TAX,                                                                  " & vbNewLine _
                                            & "   --(2014.09.10) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "-- (SELECT                                                                     " & vbNewLine _
                                            & "--    TAX.TAX_RATE                                                             " & vbNewLine _
                                            & "--  FROM $LM_MST$..M_TAX AS TAX                                                " & vbNewLine _
                                            & "--   INNER JOIN                                                                " & vbNewLine _
                                            & "--    (SELECT                                                                  " & vbNewLine _
                                            & "--      KBN1.KBN_GROUP_CD,                                                     " & vbNewLine _
                                            & "--      KBN1.KBN_CD,                                                           " & vbNewLine _
                                            & "--      KBN1.KBN_NM3,                                                          " & vbNewLine _
                                            & "--      TAX.START_DATE,                                                        " & vbNewLine _
                                            & "--      TAX.TAX_CD                                                             " & vbNewLine _
                                            & "--     FROM                                                                    " & vbNewLine _
                                            & "--      (SELECT                                                                " & vbNewLine _
                                            & "--        MAX(START_DATE) AS START_DATE,                                       " & vbNewLine _
                                            & "--        TAX.TAX_CD                                                           " & vbNewLine _
                                            & "--       FROM $LM_MST$..M_TAX AS TAX                                           " & vbNewLine _
                                            & "--       WHERE TAX.START_DATE <= @SKYU_DATE                                    " & vbNewLine _
                                            & "--       GROUP BY TAX.TAX_CD)TAX                                               " & vbNewLine _
                                            & "--        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1                              " & vbNewLine _
                                            & "--         ON  KBN1.KBN_GROUP_CD = 'Z001'                                      " & vbNewLine _
                                            & "--         AND KBN1.KBN_CD       = '01'                                        " & vbNewLine _
                                            & "--       WHERE KBN1.KBN_NM3 = TAX.TAX_CD) TAX1                                 " & vbNewLine _
                                            & "--   ON  TAX1.START_DATE = TAX.START_DATE                                      " & vbNewLine _
                                            & "--   AND TAX1.KBN_NM3    = TAX.TAX_CD)TAX,                                     " & vbNewLine _
                                            & " $LM_TRN$..G_KAGAMI_HED AS HED                                               " & vbNewLine _
                                            & "  LEFT OUTER JOIN                                                            " & vbNewLine _
                                            & "   (SELECT                                                                   " & vbNewLine _
                                            & "      SEQT.NRS_BR_CD,                                                        " & vbNewLine _
                                            & "      SEQT.SEIQTO_CD,                                                        " & vbNewLine _
                                            & "      SEQT.MEIGI_KB,                                                         " & vbNewLine _
                                            & "      SEQT.KOUZA_KB,                                                         " & vbNewLine _
                                            & "      SEQT.DOC_PTN,                                                          " & vbNewLine _
                                            & "      SEQT.DOC_PTN2,                                                         " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "      SEQT.SEIQ_CURR_CD,                                                     " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "      SEQT.NRS_KEIRI_CD2,       ---- ADD 20170530 QRｺｰﾄﾞ対応                 " & vbNewLine _
                                            & "      KBN1.KBN_NM2,                                                          " & vbNewLine _
                                            & "      KBN1.KBN_NM3,                                                          " & vbNewLine _
                                            & "      KBN1.KBN_NM4,                                                          " & vbNewLine _
                                            & "      BANK.BANK_NM,                                                          " & vbNewLine _
                                            & "      BANK.YOKIN_SYU,                                                        " & vbNewLine _
                                            & "      BANK.KOZA_NO,                                                          " & vbNewLine _
                                            & "      BANK.MEIGI_NM                                                          " & vbNewLine _
                                            & "     ,SEQT.ZIP    --ADD 2020/09/29  014230                           " & vbNewLine _
                                            & "     ,SEQT.DOC_DEST_YN    --ADD 2020/09/29  014230                           " & vbNewLine _
                                            & "     ,SEQT.AD_1    --ADD 2020/09/29  014230                           " & vbNewLine _
                                            & "     ,SEQT.AD_2    --ADD 2020/09/29  014230                           " & vbNewLine _
                                            & "     ,SEQT.AD_3    --ADD 2020/09/29  014230                           " & vbNewLine _
                                            & "     ,SEQT.SEIQTO_BUSYO_NM                                                   " & vbNewLine _
                                            & "     ,SEQT.OYA_PIC                                                           " & vbNewLine _
                                            & "    FROM $LM_MST$..M_SEIQTO AS SEQT                                          " & vbNewLine _
                                            & "     LEFT OUTER JOIN                                                         " & vbNewLine _
                                            & "      (SELECT                                                                " & vbNewLine _
                                            & "        KBN1.KBN_CD,                                                         " & vbNewLine _
                                            & "        KBN1.KBN_NM2,                                                        " & vbNewLine _
                                            & "        KBN1.KBN_NM3,                                                        " & vbNewLine _
                                            & "        KBN1.KBN_NM4                                                         " & vbNewLine _
                                            & "       FROM $LM_MST$..Z_KBN AS KBN1                                          " & vbNewLine _
                                            & "       WHERE KBN1.KBN_GROUP_CD = 'K011')KBN1                                 " & vbNewLine _
                                            & "      ON KBN1.KBN_CD  = '00'                                                 " & vbNewLine _
                                            & "     LEFT OUTER JOIN $LM_MST$..M_NRSBANK BANK                                " & vbNewLine _
                                            & "      ON BANK.MEIGI_CD = SEQT.KOUZA_KB)SEQT                                  " & vbNewLine _
                                            & "   ON  HED.NRS_BR_CD = SEQT.NRS_BR_CD                                        " & vbNewLine _
                                            & "   AND HED.SEIQTO_CD = SEQT.SEIQTO_CD                                        " & vbNewLine _
                                            & "  LEFT OUTER JOIN $LM_MST$..M_NRS_BR AS NRSBR                                " & vbNewLine _
                                            & "   ON  HED.NRS_BR_CD = NRSBR.NRS_BR_CD                                       " & vbNewLine _
                                            & "  INNER JOIN                                                                 " & vbNewLine _
                                            & "   (SELECT                                                                   " & vbNewLine _
                                            & "      DTL.SKYU_SUB_NO,                                                       " & vbNewLine _
                                            & "      DTL.MAKE_SYU_KB,                                                       " & vbNewLine _
                                            & "      DTL.SKYU_NO,                                                           " & vbNewLine _
                                            & "      DTL.SEIQKMK_CD,                                                        " & vbNewLine _
                                            & "      DTL.BUSYO_CD,                                                          " & vbNewLine _
                                            & "      DTL.KEISAN_TLGK,                                                       " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & "      DTL.ITEM_CURR_CD,                                                      " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "      DTL.NEBIKI_RT,                                                         " & vbNewLine _
                                            & "      DTL.NEBIKI_RTGK,                                                       " & vbNewLine _
                                            & "      DTL.NEBIKI_GK,                                                         " & vbNewLine _
                                            & "      DTL.TEKIYO,                                                            " & vbNewLine _
                                            & "      DTL.PRINT_SORT,                                                        " & vbNewLine _
                                            & "      SEIQ.GROUP_KB,                                                         " & vbNewLine _
                                            & "      SEIQ.SEIQKMK_NM,                                                       " & vbNewLine _
                                            & "      SEIQ.TAX_KB,                                                           " & vbNewLine _
                                            & "      SEIQ.KBN_NM1,                                                          " & vbNewLine _
                                            & "      KBN.KBN_NM4,                                                           " & vbNewLine _
                                            & "      KBN1.KBN_NM6                                                           " & vbNewLine _
                                            & "      --20160705                                                           " & vbNewLine _
                                            & "      --20161116                                                           " & vbNewLine _
                                            & "      ,DTL. TEMPLATE_IMP_FLG                                                           " & vbNewLine _
                                            & "--upd 2016/09/07 再保管対応  , KBN1.KBN_NM3   AS KANJO                                                           " & vbNewLine _
                                            & "      , CASE WHEN DTL.JISYATASYA_KB = '02' THEN CASE WHEN KBN1.KBN_NM7 = ''  " & vbNewLine _
                                            & "                                                     THEN KBN1.KBN_NM3       " & vbNewLine _
                                            & "                                                     ELSE KBN1.KBN_NM7       " & vbNewLine _
                                            & "                                                END                          " & vbNewLine _
                                            & "                                           ELSE KBN1.KBN_NM3  END  AS KANJO  " & vbNewLine _
                                            & "    FROM $LM_TRN$..G_KAGAMI_DTL AS DTL                                       " & vbNewLine _
                                            & "     LEFT OUTER JOIN                                                         " & vbNewLine _
                                            & "      (SELECT                                                                " & vbNewLine _
                                            & "        SEIQ.GROUP_KB,                                                       " & vbNewLine _
                                            & "        SEIQ.SEIQKMK_CD,                                                     " & vbNewLine _
                                            & "        SEIQ.SEIQKMK_CD_S,                                                   " & vbNewLine _
                                            & "        SEIQ.SEIQKMK_NM,                                                     " & vbNewLine _
                                            & "        SEIQ.TAX_KB,                                                         " & vbNewLine _
                                            & "        KBN.KBN_NM1,                                                         " & vbNewLine _
                                            & "        SEIQ.KEIRI_KB                                                        " & vbNewLine _
                                            & "       FROM $LM_MST$..M_SEIQKMK AS SEIQ                                      " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN                               " & vbNewLine _
                                            & "         ON  KBN.KBN_GROUP_CD = 'Z001'                                       " & vbNewLine _
                                            & "         AND KBN.KBN_CD       = SEIQ.TAX_KB)SEIQ                             " & vbNewLine _
                                            & "      ON  DTL.GROUP_KB      = SEIQ.GROUP_KB                                  " & vbNewLine _
                                            & "      AND DTL.SEIQKMK_CD    = SEIQ.SEIQKMK_CD                                " & vbNewLine _
                                            & "      AND DTL.SEIQKMK_CD_S  = SEIQ.SEIQKMK_CD_S                              " & vbNewLine _
                                            & "     LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN                                  " & vbNewLine _
                                            & "      ON  KBN.KBN_GROUP_CD  = 'B007'                                         " & vbNewLine _
                                            & "      AND KBN.KBN_CD        = DTL.BUSYO_CD                                   " & vbNewLine _
                                            & "     LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1                                 " & vbNewLine _
                                            & "      ON  KBN1.KBN_GROUP_CD = 'B006'                                         " & vbNewLine _
                                            & "      AND KBN1.KBN_NM4      = DTL.BUSYO_CD                                   " & vbNewLine _
                                            & "      AND KBN1.KBN_NM1      = SEIQ.KEIRI_KB                                  " & vbNewLine _
                                            & "    WHERE DTL.SYS_DEL_FLG   = '0' ) DTL                                      " & vbNewLine _
                                            & "  ON  HED.SKYU_NO      = DTL.SKYU_NO                                         " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT AS MR                                             " & vbNewLine _
                                            & "  ON  MR.NRS_BR_CD = SEQT.NRS_BR_CD                                          " & vbNewLine _
                                            & "  AND MR.PTN_ID    = '53'                                                    " & vbNewLine _
                                            & "  AND MR.PTN_CD    = SEQT.DOC_PTN                                            " & vbNewLine _
                                            & "  AND MR.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_RPT AS MR2                                            " & vbNewLine _
                                            & "  ON  MR2.NRS_BR_CD = SEQT.NRS_BR_CD                                         " & vbNewLine _
                                            & "  AND MR2.PTN_ID    = '77'                                                   " & vbNewLine _
                                            & "  AND MR2.PTN_CD    = SEQT.DOC_PTN2                                          " & vbNewLine _
                                            & "  AND MR2.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..S_USER AS S_USER                                        " & vbNewLine _
                                            & "  ON  S_USER.USER_CD = HED.SYS_ENT_USER                                      " & vbNewLine _
                                            & "  AND S_USER.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN AS KBN01                                          " & vbNewLine _
                                            & "  ON  KBN01.KBN_GROUP_CD = 'B007'                                            " & vbNewLine _
                                            & "  AND KBN01.KBN_CD       = S_USER.BUSYO_CD                                   " & vbNewLine _
                                            & "  AND KBN01.SYS_DEL_FLG  = '0'                                               " & vbNewLine _
                                            & "   --(2014.08.21) 追加START 多通貨対応                                       " & vbNewLine _
                                            & " LEFT JOIN COM_DB..M_CURR AS CURR_ITEM                                       " & vbNewLine _
                                            & "  ON  CURR_ITEM.BASE_CURR_CD = HED.INV_CURR_CD                               " & vbNewLine _
                                            & "  AND CURR_ITEM.CURR_CD      = DTL.ITEM_CURR_CD                              " & vbNewLine _
                                            & "  AND CURR_ITEM.SYS_DEL_FLG  = '0'                                           " & vbNewLine _
                                            & "  AND CURR_ITEM.UP_FLG       = '00000'                                       " & vbNewLine _
                                            & " LEFT JOIN COM_DB..M_CURR AS CURR_SEIQ                                       " & vbNewLine _
                                            & "  ON  CURR_SEIQ.BASE_CURR_CD = ISNULL((SELECT BASE_CURR_CD FROM $LM_MST$..M_NRS_BR WHERE NRS_BR_CD = (SELECT NRS_BR_CD FROM $LM_TRN$..G_KAGAMI_HED WHERE SKYU_NO = @SKYU_NO)) ,'') " & vbNewLine _
                                            & "  AND CURR_SEIQ.CURR_CD      = HED.INV_CURR_CD                               " & vbNewLine _
                                            & "  AND CURR_SEIQ.SYS_DEL_FLG  = '0'                                           " & vbNewLine _
                                            & "  AND CURR_SEIQ.UP_FLG       = '00000'                                       " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_ITEM                                   " & vbNewLine _
                                            & "  ON  KBNC025_ITEM.KBN_GROUP_CD = 'C025'                                     " & vbNewLine _
                                            & "  AND KBNC025_ITEM.KBN_NM6      =  (CASE WHEN DTL.ITEM_CURR_CD = '' THEN 'JPY' ELSE DTL.ITEM_CURR_CD END)          " & vbNewLine _
                                            & "  AND KBNC025_ITEM.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_SEIQ                                   " & vbNewLine _
                                            & "  ON  KBNC025_SEIQ.KBN_GROUP_CD = 'C025'                                     " & vbNewLine _
                                            & "  AND KBNC025_SEIQ.KBN_NM6      =  (CASE WHEN HED.INV_CURR_CD = '' THEN 'JPY' ELSE HED.INV_CURR_CD END)        " & vbNewLine _
                                            & "  AND KBNC025_SEIQ.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_MOTO                                   " & vbNewLine _
                                            & "  ON  KBNC025_MOTO.KBN_GROUP_CD = 'C025'                                     " & vbNewLine _
                                            & "  AND KBNC025_MOTO.KBN_NM6      =  (CASE WHEN HED.EX_MOTO_CURR_CD = '' THEN 'JPY' ELSE HED.EX_MOTO_CURR_CD END)    " & vbNewLine _
                                            & "  AND KBNC025_MOTO.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN AS KBNC025_SAKI                                   " & vbNewLine _
                                            & "  ON  KBNC025_SAKI.KBN_GROUP_CD = 'C025'                                     " & vbNewLine _
                                            & "  AND KBNC025_SAKI.KBN_NM6      =  (CASE WHEN HED.EX_SAKI_CURR_CD = '' THEN 'JPY' ELSE HED.EX_SAKI_CURR_CD END)    " & vbNewLine _
                                            & "  AND KBNC025_SAKI.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & "   --(2014.08.21) 追加END 多通貨対応                                         " & vbNewLine _
                                            & "  ---- ADD 20170530 QRｺｰﾄﾞ対応                                               " & vbNewLine _
                                            & " LEFT JOIN LM_MST..Z_KBN AS KBNQR                                            " & vbNewLine _
                                            & "   ON  KBNQR.KBN_GROUP_CD = 'Q001'                                           " & vbNewLine _
                                            & "  AND KBNQR.KBN_CD        = '01'                                             " & vbNewLine _
                                            & "  AND KBNQR.SYS_DEL_FLG   = '0'                                              " & vbNewLine _
                                            & " --ADD 2020/09/15 振込手数料を含む記載                                       " & vbNewLine _
                                            & " LEFT JOIN LM_MST..Z_KBN AS KBNG014                                          " & vbNewLine _
                                            & " ON  KBNG014.KBN_GROUP_CD = 'G014'                                           " & vbNewLine _
                                            & " AND KBNG014.KBN_NM1     =  HED.NRS_BR_CD                                    " & vbNewLine _
                                            & " AND KBNG014.KBN_NM2      = HED.SEIQTO_CD                                    " & vbNewLine _
                                            & " AND KBNG014.SYS_DEL_FLG   = '0'                                             " & vbNewLine _
                                            & " --ADD 2020/09/29 014230                                                     " & vbNewLine _
                                            & " LEFT JOIN LM_MST..Z_KBN AS KBNG202                                          " & vbNewLine _
                                            & " ON  KBNG202.KBN_GROUP_CD = 'G202'                                           " & vbNewLine _
                                            & " AND KBNG202.KBN_CD       = '00'                                             " & vbNewLine _
                                            & " AND KBNG202.SYS_DEL_FLG   = '0'                                             " & vbNewLine _
                                            & " LEFT JOIN ABM_DB..M_REVCOST AS ARC                                          " & vbNewLine _
                                            & " ON  ARC.ACC_CD = DTL.KANJO                                                  " & vbNewLine _
                                            & " AND ARC.SYS_DEL_FLG   = '0'                                                 " & vbNewLine _
                                            & "        --標準の振込先情報↓                                                  " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNB1                              " & vbNewLine _
                                            & "         ON  KBNB1.KBN_GROUP_CD = 'B041'                                      " & vbNewLine _
                                            & "         AND KBNB1.KBN_CD       =  '01'                                       " & vbNewLine _
                                            & "         AND KBNB1.SYS_DEL_FLG =  '0'                                        " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNB2                              " & vbNewLine _
                                            & "         ON  KBNB2.KBN_GROUP_CD = 'B041'                                      " & vbNewLine _
                                            & "         AND KBNB2.KBN_CD       =  '02'                                       " & vbNewLine _
                                            & "         AND KBNB2.SYS_DEL_FLG =  '0'                                        " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNB3                              " & vbNewLine _
                                            & "         ON  KBNB3.KBN_GROUP_CD = 'B041'                                      " & vbNewLine _
                                            & "         AND KBNB3.KBN_CD       =  '03'                                       " & vbNewLine _
                                            & "         AND KBNB3.SYS_DEL_FLG =  '0'                                        " & vbNewLine _
                                            & "        --標準の振込先情報↑                                                  " & vbNewLine _
                                            & "        --請求先毎の振込先情報(標準より優先)↓                                " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNB1S                             " & vbNewLine _
                                            & "         ON  KBNB1S.KBN_GROUP_CD = 'B041'                                     " & vbNewLine _
                                            & "         AND KBNB1S.KBN_NM4      = HED.NRS_BR_CD                              " & vbNewLine _
                                            & "         AND KBNB1S.KBN_NM5      = HED.SEIQTO_CD                              " & vbNewLine _
                                            & "         AND KBNB1S.KBN_NM6      = '01'                                       " & vbNewLine _
                                            & "         AND KBNB1S.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNB2S                             " & vbNewLine _
                                            & "         ON  KBNB2S.KBN_GROUP_CD = 'B041'                                     " & vbNewLine _
                                            & "         AND KBNB2S.KBN_NM4      = HED.NRS_BR_CD                              " & vbNewLine _
                                            & "         AND KBNB2S.KBN_NM5      = HED.SEIQTO_CD                              " & vbNewLine _
                                            & "         AND KBNB2S.KBN_NM6      = '02'                                       " & vbNewLine _
                                            & "         AND KBNB2S.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNB3S                             " & vbNewLine _
                                            & "         ON  KBNB3S.KBN_GROUP_CD = 'B041'                                     " & vbNewLine _
                                            & "         AND KBNB3S.KBN_NM4      = HED.NRS_BR_CD                              " & vbNewLine _
                                            & "         AND KBNB3S.KBN_NM5      = HED.SEIQTO_CD                              " & vbNewLine _
                                            & "         AND KBNB3S.KBN_NM6      = '03'                                       " & vbNewLine _
                                            & "         AND KBNB3S.SYS_DEL_FLG  = '0'                                        " & vbNewLine _
                                            & "        --請求先毎の振込先情報(標準より優先)↑                                " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBNT                               " & vbNewLine _
                                            & "         ON  KBNT.KBN_GROUP_CD = 'B042'                                       " & vbNewLine _
                                            & "         AND KBNT.KBN_CD       =  '01'                                        " & vbNewLine _
                                            & "         AND KBNT.SYS_DEL_FLG =  '0'                                         " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS OLD_HONSYA_NM                     " & vbNewLine _
                                            & "         ON  OLD_HONSYA_NM.KBN_GROUP_CD = 'B044'                             " & vbNewLine _
                                            & "         AND OLD_HONSYA_NM.KBN_CD       =  '00'                              " & vbNewLine _
                                            & "         AND OLD_HONSYA_NM.SYS_DEL_FLG =  '0'                                " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS OLD_NRS_BR_NM                     " & vbNewLine _
                                            & "         ON  OLD_NRS_BR_NM.KBN_GROUP_CD = 'B044'                             " & vbNewLine _
                                            & "         AND OLD_NRS_BR_NM.KBN_CD       =  HED.NRS_BR_CD                     " & vbNewLine _
                                            & "         AND OLD_NRS_BR_NM.SYS_DEL_FLG =  '0'                                " & vbNewLine _
                                            & "        LEFT OUTER JOIN $LM_MST$..Z_KBN AS OLD_BANK_MEIGI_NM                 " & vbNewLine _
                                            & "         ON  OLD_BANK_MEIGI_NM.KBN_GROUP_CD = 'B045'                         " & vbNewLine _
                                            & "         AND OLD_BANK_MEIGI_NM.KBN_CD       =  SEQT.KOUZA_KB                 " & vbNewLine _
                                            & "         AND OLD_BANK_MEIGI_NM.SYS_DEL_FLG =  '0'                            " & vbNewLine _
                                            & "WHERE HED.SKYU_NO = @SKYU_NO                                                 " & vbNewLine _
                                            & "ORDER BY                                                                     " & vbNewLine _
                                            & "      TAX_KB,                                                                " & vbNewLine _
                                            & "      GROUP_KB,                                                              " & vbNewLine _
                                            & "      SEIQKMK_CD,                                                            " & vbNewLine _
                                            & "      PRINT_SORT,                                                            " & vbNewLine _
                                            & "      NEBIKI_RT,                                                             " & vbNewLine _
                                            & "      SKYU_SUB_NO                                                            " & vbNewLine

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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim Ssql As String = String.Empty
        Me._StrSql.Append(LMG520DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select)
        Call Me.SetConditionPrtMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG520DAC", "SelectMPrt", cmd)

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
    ''' 請求書鑑出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求書鑑出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG520DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Call Me.SetConditionPrtDataSQL()                  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG520DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SEIQTO_PIC", "SEIQTO_PIC")
        map.Add("SKYU_NO", "SKYU_NO")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("MEIGI_KB", "MEIGI_KB")
        map.Add("KOUZA_KB", "KOUZA_KB")
        map.Add("HONSYA_NM", "HONSYA_NM")
        map.Add("HONSYA_ADD1", "HONSYA_ADD1")
        map.Add("HONSYA_ADD2", "HONSYA_ADD2")
        map.Add("HONSYA_TEL", "HONSYA_TEL")
        map.Add("CENTER_NM", "CENTER_NM")
        map.Add("CENTER_ADD1", "CENTER_ADD1")
        map.Add("CENTER_ADD2", "CENTER_ADD2")
        map.Add("CENTER_TEL", "CENTER_TEL")
        map.Add("HAKKO_MOTO_NM", "HAKKO_MOTO_NM")
        map.Add("HAKKO_MOTO_ADD1", "HAKKO_MOTO_ADD1")
        map.Add("HAKKO_MOTO_ADD2", "HAKKO_MOTO_ADD2")
        map.Add("HAKKO_MOTO_TEL", "HAKKO_MOTO_TEL")
        map.Add("SKYU_SYOSYU", "SKYU_SYOSYU")
        map.Add("SKYU_MONTH", "SKYU_MONTH")
        map.Add("REMARK", "REMARK")
        map.Add("TAX_RATE", "TAX_RATE")
        map.Add("NEBIKI_RT1", "NEBIKI_RT1")
        map.Add("NEBIKI_GK1", "NEBIKI_GK1")
        map.Add("TAX_GK1", "TAX_GK1")
        map.Add("TAX_HASU_GK1", "TAX_HASU_GK1")
        '2014.08.21 追加START 多通貨対応
        map.Add("TAX_GK1_SEIQ", "TAX_GK1_SEIQ")
        map.Add("TAX_HASU_GK1_SEIQ", "TAX_HASU_GK1_SEIQ")
        '2014.08.21 追加END 多通貨対応
        map.Add("NEBIKI_RT2", "NEBIKI_RT2")
        map.Add("NEBIKI_GK2", "NEBIKI_GK2")
        map.Add("DETAIL_MIDASHI", "DETAIL_MIDASHI")
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("SUMMARY_CODE", "SUMMARY_CODE")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        '2014.08.21 追加START 多通貨対応
        map.Add("KEISAN_TLGK_SEIQ", "KEISAN_TLGK_SEIQ")
        '2014.08.21 追加END 多通貨対応
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("KOTEI_NEBIKI_GK", "KOTEI_NEBIKI_GK")
        map.Add("RT_NEBIKI_GK", "RT_NEBIKI_GK")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        '2014.08.21 追加START 多通貨対応
        map.Add("NEBIKI_GK_SEIQ", "NEBIKI_GK_SEIQ")
        '2014.08.21 追加END 多通貨対応
        map.Add("KINGAKU", "KINGAKU")
        '2014.08.21 追加START 多通貨対応
        map.Add("KINGAKU_SEIQ", "KINGAKU_SEIQ")
        '2014.08.21 追加END 多通貨対応
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("BANK_NM", "BANK_NM")
        map.Add("YOKIN_SYU", "YOKIN_SYU")
        map.Add("KOZA_NO", "KOZA_NO")
        map.Add("MEIGI_NM", "MEIGI_NM")
        map.Add("YOKIN_INFO", "YOKIN_INFO")
        map.Add("SKYU_SUB_NO", "SKYU_SUB_NO")
        map.Add("MAKE_SYU_KB", "MAKE_SYU_KB")
        '2014.08.21 追加START 多通貨対応
        map.Add("EX_RATE", "EX_RATE")
        map.Add("EX_MOTO_CURR_CD", "EX_MOTO_CURR_CD")
        map.Add("EX_SAKI_CURR_CD", "EX_SAKI_CURR_CD")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("SEIQ_CURR_CD", "SEIQ_CURR_CD")
        map.Add("ITEM_ROUND_POS", "ITEM_ROUND_POS")
        map.Add("SEIQ_ROUND_POS", "SEIQ_ROUND_POS")
        '2014.08.21 追加END 多通貨対応
        '20161116 請求テンプレート対応
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")

        map.Add("KANJO", "KANJO")
        map.Add("KANJO_NM", "KANJO_NM")

#If True Then  ' 鑑作成区分名表示 20161025 added inoue
        map.Add("CRT_KB", "CRT_KB")
#End If

#If True Then ' 作成者名表示対応 200170420 added by inoue
        map.Add("KAGAMI_ENT_USER_NM", "KAGAMI_ENT_USER_NM")
#End If

        'ADD 2017/05/30  QRｺｰﾄﾞ対応  
        map.Add("JDE_CD", "JDE_CD")
        map.Add("QR_SYSTEM_ID", "QR_SYSTEM_ID")
        map.Add("QR_SYSTEM_ID_NM", "QR_SYSTEM_ID_NM")
        map.Add("QR_REC_TYP", "QR_REC_TYP")
        map.Add("QR_REC_TYP_NM", "QR_REC_TYP_NM")
        map.Add("QR_FOLDER", "QR_FOLDER")

        map.Add("KBNG014_FLG", "KBNG014_FLG")  'ADD 2020/09/15 振込手数料を含む記載 対象はKBNG014_FLGに区分CD設定
        map.Add("DOC_DEST_YN", "DOC_DEST_YN")  'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("ATENA_RPR_ID", "ATENA_RPR_ID")  'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("SEIQTO_ZIP", "SEIQTO_ZIP")  'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("SEIQTO_AD1", "SEIQTO_AD1")  'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("SEIQTO_AD2", "SEIQTO_AD2")  'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("SEIQTO_AD3", "SEIQTO_AD3")  'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("SEIQTO_BUSYO_NM", "SEIQTO_BUSYO_NM")
        map.Add("SEIQTO_OYA_PIC", "SEIQTO_OYA_PIC")

        map.Add("NRS_BR_NM", "NRS_BR_NM")  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("NRS_BR_ZIP", "NRS_BR_ZIP")  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("NRS_BR_AD1", "NRS_BR_AD1")  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("NRS_BR_AD2", "NRS_BR_AD2")  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
        map.Add("NRS_BR_AD3", "NRS_BR_AD3")  'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
#If True Then   'ADD 2021/09/03 023615 【LMS_RT】請求書に部署名の追加依頼(群馬大隅)
        map.Add("NRS_BR_CD", "NRS_BR_CD")
#End If

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
        map.Add("BANK_NM1", "BANK_NM1")
        map.Add("BANK_BR1", "BANK_BR1")
        map.Add("BANK_ACC1", "BANK_ACC1")
        map.Add("BANK_NM2", "BANK_NM2")
        map.Add("BANK_BR2", "BANK_BR2")
        map.Add("BANK_ACC2", "BANK_ACC2")
        map.Add("BANK_NM3", "BANK_NM3")
        map.Add("BANK_BR3", "BANK_BR3")
        map.Add("BANK_ACC3", "BANK_ACC3")
        map.Add("T_NO", "T_NO")
        map.Add("BANK__NM0", "BANK__NM0")
        map.Add("FURIKOMI_MEMO", "FURIKOMI_MEMO")
#End If
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG520SET")


        Return ds

    End Function

#End Region

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

#Region "パラメタ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（出力対象帳票パターン取得処理用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrtMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", Me._Row.Item("SKYU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAGAMI_PTN", Me._Row.Item("KAGAMI_PTN").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_CHG_START_YM", Me._Row.Item("RPT_CHG_START_YM").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（請求書鑑出力対象データ検索用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrtDataSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", Me._Row.Item("SKYU_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", Me._Row.Item("SKYU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAGAMI_PTN", Me._Row.Item("KAGAMI_PTN").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", Me.GetUserID(), DBDataType.NVARCHAR))
        '2014.09.10 多通貨対応　追加START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        '2014.09.10 多通貨対応　追加END
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_CHG_START_YM", Me._Row.Item("RPT_CHG_START_YM").ToString(), DBDataType.CHAR))

    End Sub

#End Region

End Class
