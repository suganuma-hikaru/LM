' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : 出荷管理
'  プログラムID     :  LMH580    : 納品書(埼玉BP・納品送状)
'  作  成  者       :  渡部　剛
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH580DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH580DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                       " & vbNewLine _
                                            & "        BPHED.NRS_BR_CD                                AS NRS_BR_CD    " & vbNewLine _
                                            & "      , 'B1'                                             AS PTN_ID     " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD               " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD                                                " & vbNewLine _
                                            & "        END                                              AS PTN_CD     " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID                                                " & vbNewLine _
                                            & "        END                                              AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                          " & vbNewLine _
                                            & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID         " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID         " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID                                          " & vbNewLine _
                                            & "        END                         AS RPT_ID                    " & vbNewLine _
                                            & "      , BPHED.DEL_KB                AS HED_DEL_KB                " & vbNewLine _
                                            & "      , BPHED.CRT_DATE              AS CRT_DATE                  " & vbNewLine _
                                            & "      , BPHED.FILE_NAME             AS FILE_NAME                 " & vbNewLine _
                                            & "      , BPHED.REC_NO                AS REC_NO                    " & vbNewLine _
                                            & "      , BPDTL.GYO                   AS GYO                       " & vbNewLine _
                                            & "      , BPHED.NRS_BR_CD             AS NRS_BR_CD                 " & vbNewLine _
                                            & "      , M_NRS_BR.NRS_BR_NM          AS NRS_BR_NM                 " & vbNewLine _
                                            & "      , M_NRS_BR.AD_1               AS NRS_BR_AD1                " & vbNewLine _
                                            & "      , M_NRS_BR.AD_2               AS NRS_BR_AD2                " & vbNewLine _
                                            & "      , M_NRS_BR.AD_3               AS NRS_BR_AD3                " & vbNewLine _
                                            & "      , M_NRS_BR.TEL                AS NRS_BR_TEL                " & vbNewLine _
                                            & "      , M_NRS_BR.FAX                AS NRS_BR_FAX                " & vbNewLine _
                                            & "      , BPHED.EDI_CTL_NO            AS EDI_CTL_NO                " & vbNewLine _
                                            & "      , BPDTL.EDI_CTL_NO_CHU        AS EDI_CTL_NO_CHU            " & vbNewLine _
                                            & "      , BPHED.OUTKA_CTL_NO          AS OUTKA_CTL_NO              " & vbNewLine _
                                            & "      , BPDTL.OUTKA_CTL_NO_CHU      AS OUTKA_CTL_NO_CHU          " & vbNewLine _
                                            & "      , BPHED.CUST_CD_L             AS CUST_CD_L                 " & vbNewLine _
                                            & "      , BPHED.CUST_CD_M             AS CUST_CD_M                 " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_L           AS CUST_NM_L                  " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_M           AS CUST_NM_M                  " & vbNewLine _
                                            & "      , M_CUST.AD_1                AS CUST_AD_1                  " & vbNewLine _
                                            & "      , M_CUST.AD_2                AS CUST_AD_2                  " & vbNewLine _
                                            & "      , M_CUST.AD_3                AS CUST_AD_3                  " & vbNewLine _
                                            & "      , M_CUST.TEL                 AS CUST_TEL                   " & vbNewLine _
                                            & "      , M_CUST.FAX                 AS CUST_FAX                   " & vbNewLine _
                                            & "      , M_CUST.PIC                 AS CUST_PIC                   " & vbNewLine _
                                            & "      , BPHED.PRTFLG               AS PRTFLG                     " & vbNewLine _
                                            & "      , BPHED.CANCEL_FLG           AS CANCEL_FLG                 " & vbNewLine _
                                            & "      , BPHED.DATA_KB              AS HED_DATA_KB                " & vbNewLine _
                                            & "      , BPHED.KITAKU_CD            AS HED_KITAKU_CD              " & vbNewLine _
                                            & "      , BPHED.OUTKA_SOKO_CD        AS HED_OUTKA_SOKO_CD          " & vbNewLine _
                                            & "      , BPHED.ORDER_TYPE           AS HED_ORDER_TYPE             " & vbNewLine _
                                            & "--(2013.02.18)要望番号1867 出荷日の取得TBL変更 -- START --       " & vbNewLine _
                                            & "      --, BPHED.OUTKA_PLAN_DATE    AS HED_OUTKA_PLAN_DATE        " & vbNewLine _
                                            & "      , OUTEDIL.OUTKA_PLAN_DATE    AS HED_OUTKA_PLAN_DATE        " & vbNewLine _
                                            & "--(2013.02.18)要望番号1867 出荷日の取得TBL変更 --  END  --       " & vbNewLine _
                                            & "      , BPHED.CUST_ORD_NO          AS HED_CUST_ORD_NO            " & vbNewLine _
                                            & "      , BPHED.DEST_CD              AS DEST_CD                    " & vbNewLine _
                                            & "      , BPHED.DEST_JIS_CD          AS DEST_JIS_CD                " & vbNewLine _
                                            & "      , BPHED.DEST_NM1             AS DEST_NM1                   " & vbNewLine _
                                            & "      , BPHED.DEST_NM2             AS DEST_NM2                   " & vbNewLine _
                                            & "      , BPHED.DEST_AD1             AS DEST_AD1                   " & vbNewLine _
                                            & "      , BPHED.DEST_AD2             AS DEST_AD2                   " & vbNewLine _
                                            & "      , BPHED.DEST_TEL             AS DEST_TEL                   " & vbNewLine _
                                            & "      , BPHED.DEST_ZIP             AS DEST_ZIP                   " & vbNewLine _
                                            & "      , BPHED.ARR_PLAN_DATE        AS ARR_PLAN_DATE              " & vbNewLine _
                                            & "      , BPHED.ARR_PLAN_TIME        AS ARR_PLAN_TIME              " & vbNewLine _
                                            & "      , BPHED.HT_DATE              AS HT_DATE                    " & vbNewLine _
                                            & "      , BPHED.HT_TIME              AS HT_TIME                    " & vbNewLine _
                                            & "      , BPHED.HT_CAR_NO            AS HT_CAR_NO                  " & vbNewLine _
                                            & "      , BPHED.HT_DRIVER            AS HT_DRIVER                  " & vbNewLine _
                                            & "      , BPHED.HT_UNSO_CO           AS HT_UNSO_CO                 " & vbNewLine _
                                            & "      , BPHED.MOSIOKURI_KB         AS MOSIOKURI_KB               " & vbNewLine _
                                            & "      , BPHED.BUMON_CD             AS BUMON_CD                   " & vbNewLine _
                                            & "      , BPHED.JIGYOBU_CD           AS JIGYOBU_CD                 " & vbNewLine _
                                            & "      , BPHED.TOKUI_CD             AS TOKUI_CD                   " & vbNewLine _
                                            & "      , BPHED.TOKUI_NM             AS TOKUI_NM                   " & vbNewLine _
                                            & "      , BPHED.BUYER_ORD_NO         AS BUYER_ORD_NO               " & vbNewLine _
                                            & "      , BPHED.HACHU_NO             AS HACHU_NO                   " & vbNewLine _
                                            & "      , BPHED.DENPYO_NO            AS DENPYO_NO                  " & vbNewLine _
                                            & "      , BPHED.TENPO_CD             AS TENPO_CD                   " & vbNewLine _
                                            & "      , BPHED.CHOKUSO_KB           AS CHOKUSO_KB                 " & vbNewLine _
                                            & "      , BPHED.SEIKYU_KB            AS SEIKYU_KB                  " & vbNewLine _
                                            & "      , BPHED.HACHU_DATE           AS HACHU_DATE                 " & vbNewLine _
                                            & "      --(2013.01.29)文字化け値があるので取得しない -- START --   " & vbNewLine _
                                            & "      --, BPHED.CHUMON_NM          AS CHUMON_NM                  " & vbNewLine _
                                            & "      , ''                         AS CHUMON_NM                  " & vbNewLine _
                                            & "      --(2013.01.29)文字化け値があるので取得しない --  END  --   " & vbNewLine _
                                            & "      , BPHED.HOL_KB               AS HOL_KB                     " & vbNewLine _
                                            & "      , BPHED.BIKO_HED1            AS HED_BIKO_HED1              " & vbNewLine _
                                            & "      , BPHED.BIKO_HED2            AS HED_BIKO_HED2              " & vbNewLine _
                                            & "      , BPHED.HAKO_NM              AS HAKO_NM                    " & vbNewLine _
                                            & "      , BPHED.SIIRESAKI_CD         AS SIIRESAKI_CD               " & vbNewLine _
                                            & "      , BPHED.KR_TOKUI_CD          AS KR_TOKUI_CD                " & vbNewLine _
                                            & "      , BPHED.KEIHI_KB             AS KEIHI_KB                   " & vbNewLine _
                                            & "      , BPHED.JUCHU_KB             AS JUCHU_KB                   " & vbNewLine _
                                            & "      , BPHED.DEST_NM              AS DEST_NM                    " & vbNewLine _
                                            & "      , BPHED.KIGYO_NM             AS KIGYO_NM                   " & vbNewLine _
                                            & "      , BPHED.FILLER_1             AS HED_FILLER_1               " & vbNewLine _
                                            & "      , BPHED.RECORD_STATUS        AS HED_REC_STS                " & vbNewLine _
                                            & "      , BPDTL.DEL_KB               AS DTL_DEL_KB                 " & vbNewLine _
                                            & "      , BPDTL.DATA_KB              AS DTL_DATA_KB                " & vbNewLine _
                                            & "      , BPDTL.KITAKU_CD            AS DTL_KITAKU_CD              " & vbNewLine _
                                            & "      , BPDTL.OUTKA_SOKO_CD        AS DTL_OUTKA_SOKO_CD          " & vbNewLine _
                                            & "      , BPDTL.ORDER_TYPE           AS DTL_ORDER_TYPE             " & vbNewLine _
                                            & "      , BPDTL.OUTKA_PLAN_DATE      AS DTL_OUTKA_PLAN_DATE        " & vbNewLine _
                                            & "      , BPDTL.CUST_ORD_NO          AS DTL_CUST_ORD_NO            " & vbNewLine _
                                            & "      , BPDTL.ROW_NO               AS ROW_NO                     " & vbNewLine _
                                            & "      , BPDTL.ROW_TYPE             AS ROW_TYPE                   " & vbNewLine _
                                            & "      , BPDTL.GOODS_CD             AS GOODS_CD                   " & vbNewLine _
                                            & "      , BPDTL.GOODS_NM             AS GOODS_NM                   " & vbNewLine _
                                            & "      , BPDTL.PKG_NB               AS PKG_NB                     " & vbNewLine _
                                            & "      , BPDTL.LOT_NO               AS LOT_NO                     " & vbNewLine _
                                            & "      , BPDTL.OUTKA_PKG_NB         AS OUTKA_PKG_NB               " & vbNewLine _
                                            & "      , BPDTL.OUTKA_NB             AS OUTKA_NB                   " & vbNewLine _
                                            & "      , BPDTL.TOTAL_WT             AS TOTAL_WT                   " & vbNewLine _
                                            & "      , BPDTL.TOTAL_QT             AS TOTAL_QT                   " & vbNewLine _
                                            & "      , BPDTL.LOT_FLAG             AS LOT_FLAG                   " & vbNewLine _
                                            & "      , BPDTL.BIKO_HED1            AS DTL_BIKO_HED1              " & vbNewLine _
                                            & "      , BPDTL.BIKO_HED2            AS DTL_BIKO_HED2              " & vbNewLine _
                                            & "      , BPDTL.BIKO_DTL             AS BIKO_DTL                   " & vbNewLine _
                                            & "      , BPDTL.BUYER_GOODS_CD       AS BUYER_GOODS_CD             " & vbNewLine _
                                            & "      , BPDTL.TENPO_TANKA          AS TENPO_TANKA                " & vbNewLine _
                                            & "      , BPDTL.TENPO_KINGAKU        AS TENPO_KINGAKU              " & vbNewLine _
                                            & "      , BPDTL.JAN_CD               AS JAN_CD                     " & vbNewLine _
                                            & "      , BPDTL.TENPO_BAIKA          AS TENPO_BAIKA                " & vbNewLine _
                                            & "      , BPDTL.FILLER_1             AS DTL_FILLER_1               " & vbNewLine _
                                            & "      , BPDTL.RECORD_STATUS        AS DTL_REC_STS                " & vbNewLine _
                                            & "      , BPDTL.JISSEKI_SHORI_FLG    AS JISSEKI_SHORI_FLG          " & vbNewLine _
                                            & "      , S_USER.USER_NM             AS USER_NM                    " & vbNewLine _
                                            & "--      , CAST(CEILING(BPDTL.ROW_NO / Z_KBN.VALUE1) AS INT) AS OUTPUT_CNT       " & vbNewLine _
                                            & "      , CASE WHEN SUB.MAXCNT = BPDTL.ROW_NO                      " & vbNewLine _
                                            & "        THEN CAST(CEILING(BPDTL.ROW_NO / Z_KBN.VALUE1) AS INT)   " & vbNewLine _
                                            & "        ELSE 0                                                   " & vbNewLine _
                                            & "        END OUTPUT_CNT                                           " & vbNewLine _
                                            & "-- 外装名追加 要望番号2471対応                                   " & vbNewLine _
                                            & "      , OUTPKG.KBN_NM2             AS OUTER_PKG_NM               " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = " --出荷(大)                                                                                         " & vbNewLine _
                                     & " FROM $LM_TRN$..H_OUTKAEDI_HED_BP BPHED                                                             " & vbNewLine _
                                     & "       --BPｶｽﾄﾛｰﾙ出荷EDI(明細)                                                                      " & vbNewLine _
                                     & "       LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BP BPDTL                                                  " & vbNewLine _
                                     & "              ON BPDTL.CRT_DATE  = BPHED.CRT_DATE                                                   " & vbNewLine _
                                     & "             AND BPDTL.FILE_NAME = BPHED.FILE_NAME                                                  " & vbNewLine _
                                     & "             AND BPDTL.REC_NO    = BPHED.REC_NO                                                     " & vbNewLine _
                                     & "             AND BPDTL.DEL_KB    = '0'                                                              " & vbNewLine _
                                     & "--(2013.06.17) 追加START                                                                            " & vbNewLine _
                                     & "       LEFT JOIN                                                                                    " & vbNewLine _
                                     & "       (SELECT MAX(SUBDTL.ROW_NO) AS MAXCNT                                                         " & vbNewLine _
                                     & "              ,SUBHED.OUTKA_PLAN_DATE                                                               " & vbNewLine _
                                     & "              ,SUBHED.CUST_ORD_NO                                                                   " & vbNewLine _
                                     & "          FROM $LM_TRN$..H_OUTKAEDI_HED_BP SUBHED                                                   " & vbNewLine _
                                     & "       --BP(明細)                                                                                   " & vbNewLine _
                                     & "       LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BP SUBDTL                                                 " & vbNewLine _
                                     & "              ON SUBDTL.CRT_DATE  = SUBHED.CRT_DATE                                                 " & vbNewLine _
                                     & "             AND SUBDTL.FILE_NAME = SUBHED.FILE_NAME                                                " & vbNewLine _
                                     & "             AND SUBDTL.REC_NO    = SUBHED.REC_NO                                                   " & vbNewLine _
                                     & "             AND SUBDTL.DEL_KB    = '0'                                                             " & vbNewLine _
                                     & "       WHERE                                                                                        " & vbNewLine _
                                     & "             SUBHED.DEL_KB    = '0'                                                                 " & vbNewLine _
                                     & "       GROUP BY                                                                                     " & vbNewLine _
                                     & "        SUBHED.OUTKA_PLAN_DATE                                                                      " & vbNewLine _
                                     & "       ,SUBHED.CUST_ORD_NO                                                                          " & vbNewLine _
                                     & "       ) SUB                                                                                        " & vbNewLine _
                                     & "       ON BPHED.CUST_ORD_NO = SUB.CUST_ORD_NO                                                       " & vbNewLine _
                                     & "       AND BPHED.OUTKA_PLAN_DATE = SUB.OUTKA_PLAN_DATE                                              " & vbNewLine _
                                     & "--(2013.06.17) 追加END                                                                              " & vbNewLine _
                                     & "       --EDI出荷(大)                                                                                " & vbNewLine _
                                     & "       LEFT JOIN $LM_TRN$..H_OUTKAEDI_L OUTEDIL                                                     " & vbNewLine _
                                     & "              ON OUTEDIL.NRS_BR_CD  = BPHED.NRS_BR_CD                                               " & vbNewLine _
                                     & "             AND OUTEDIL.EDI_CTL_NO = BPHED.EDI_CTL_NO                                              " & vbNewLine _
                                     & "             AND OUTEDIL.DEL_KB     = '0'                                                           " & vbNewLine _
                                     & "       --営業所Ｍ                                                                                   " & vbNewLine _
                                     & "       LEFT JOIN $LM_MST$..M_NRS_BR M_NRS_BR                                                        " & vbNewLine _
                                     & "              ON M_NRS_BR.NRS_BR_CD  = BPHED.NRS_BR_CD                                              " & vbNewLine _
                                     & "       --荷主Ｍ                                                                                     " & vbNewLine _
                                     & "       LEFT JOIN $LM_MST$..M_CUST M_CUST                                                            " & vbNewLine _
                                     & "              ON M_CUST.NRS_BR_CD  = BPHED.NRS_BR_CD                                                " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_L  = BPHED.CUST_CD_L                                                " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_M  = BPHED.CUST_CD_M                                                " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_S  = '00'                                                           " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_SS = '00'                                                           " & vbNewLine _
                                     & "--(2013.06.17) 追加START                                                                            " & vbNewLine _
                                     & "       --区分Ｍ                                                                                     " & vbNewLine _
                                     & "       LEFT JOIN $LM_MST$..Z_KBN Z_KBN                                                              " & vbNewLine _
                                     & "              ON Z_KBN.KBN_GROUP_CD  = 'B015'                                                       " & vbNewLine _
                                     & "             AND Z_KBN.KBN_CD        = '01'                                                         " & vbNewLine _
                                     & "--(2013.06.17) 追加END                                                                            " & vbNewLine _
                                     & "       --商品Ｍ                                                                                     " & vbNewLine _
                                     & "--(2015.12.18 変更 要望番号2471対応 START)                                                            " & vbNewLine _
                                     & "--       LEFT JOIN $LM_MST$..M_GOODS M_GOODS                                                          " & vbNewLine _
                                     & "--              ON M_GOODS.NRS_BR_CD    = BPDTL.NRS_BR_CD                                             " & vbNewLine _
                                     & "--           AND M_GOODS.CUST_CD_L    = BPDTL.CUST_CD_L                                             " & vbNewLine _
                                     & "--           AND M_GOODS.CUST_CD_M    = BPDTL.CUST_CD_M                                             " & vbNewLine _
                                     & "--           AND M_GOODS.CUST_CD_S    = '00'                                                        " & vbNewLine _
                                     & "--           AND M_GOODS.CUST_CD_SS   = '00'                                                        " & vbNewLine _
                                     & "--           AND M_GOODS.GOODS_CD_NRS = BPDTL.GOODS_CD                                              " & vbNewLine _
                                     & "       --商品Ｍ                                                                                     " & vbNewLine _
                                     & "       LEFT JOIN                                                                                    " & vbNewLine _
                                     & "        (SELECT MG1.* FROM $LM_MST$..M_GOODS MG1                                                    " & vbNewLine _
                                     & "          LEFT JOIN                                                                                 " & vbNewLine _
                                     & "           (SELECT NRS_BR_CD,                                                                       " & vbNewLine _
                                     & "                   CUST_CD_L,                                                                       " & vbNewLine _
                                     & "                   CUST_CD_M,                                                                       " & vbNewLine _
                                     & "                   GOODS_CD_CUST,                                                                   " & vbNewLine _
                                     & "                   MIN(GOODS_CD_NRS) AS GOODS_CD_NRS                                                " & vbNewLine _
                                     & "            FROM $LM_MST$..M_GOODS                                                                  " & vbNewLine _
                                     & "            WHERE                                                                                   " & vbNewLine _
                                     & "                  NRS_BR_CD = @NRS_BR_CD                                                            " & vbNewLine _
                                     & "              AND CUST_CD_L = @CUST_CD_L                                                            " & vbNewLine _
                                     & "              AND CUST_CD_M = @CUST_CD_M                                                            " & vbNewLine _
                                     & "            GROUP BY                                                                                " & vbNewLine _
                                     & "               NRS_BR_CD                                                                            " & vbNewLine _
                                     & "             , CUST_CD_L                                                                            " & vbNewLine _
                                     & "             , CUST_CD_M                                                                            " & vbNewLine _
                                     & "             , GOODS_CD_CUST                                                                        " & vbNewLine _
                                     & "            HAVING Count(*) = 1) AS MG2                                                             " & vbNewLine _
                                     & "           ON MG1.NRS_BR_CD = MG2.NRS_BR_CD                                                      " & vbNewLine _
                                     & "           AND MG1.GOODS_CD_NRS = MG2.GOODS_CD_NRS                                                  " & vbNewLine _
                                     & "         WHERE MG1.NRS_BR_CD = @NRS_BR_CD                                                           " & vbNewLine _
                                     & "          AND MG1.CUST_CD_L = @CUST_CD_L                                                            " & vbNewLine _
                                     & "          AND MG1.CUST_CD_M = @CUST_CD_M                                                            " & vbNewLine _
                                     & "          AND MG2.GOODS_CD_NRS IS NOT NULL                                                          " & vbNewLine _
                                     & "        ) AS M_GOODS                                                                                " & vbNewLine _
                                     & "       ON M_GOODS.NRS_BR_CD    = BPDTL.NRS_BR_CD                                                    " & vbNewLine _
                                     & "      AND M_GOODS.CUST_CD_L    = BPDTL.CUST_CD_L                                                    " & vbNewLine _
                                     & "      AND M_GOODS.CUST_CD_M    = BPDTL.CUST_CD_M                                                    " & vbNewLine _
                                     & "      AND M_GOODS.GOODS_CD_CUST = BPDTL.GOODS_CD                                                    " & vbNewLine _
                                     & "--(2015.12.18 変更 要望番号2471対応 END)                                                            " & vbNewLine _
                                     & "       -- EDI印刷種別テーブル                                                                       " & vbNewLine _
                                     & "       LEFT JOIN $LM_TRN$..H_EDI_PRINT EDI_PRT                                                      " & vbNewLine _
                                     & "              ON EDI_PRT.NRS_BR_CD = BPDTL.NRS_BR_CD                                                " & vbNewLine _
                                     & "             AND EDI_PRT.EDI_CTL_NO = BPDTL.EDI_CTL_NO                                              " & vbNewLine _
                                     & "             AND EDI_PRT.INOUT_KB = '0'                                                             " & vbNewLine _
                                     & "--(2013.02.27) BP新システム切替対応START                                                            " & vbNewLine _
                                     & "--             AND EDI_PRT.DENPYO_NO = BPHED.DENPYO_NO                                                " & vbNewLine _
                                     & "             AND EDI_PRT.DENPYO_NO  = BPHED.CUST_ORD_NO                                             " & vbNewLine _
                                     & "--(2013.02.27) BP新システム切替対応END                                                              " & vbNewLine _
                                     & "             AND EDI_PRT.PRINT_TP  = '06'                                                           " & vbNewLine _
                                     & "       LEFT JOIN (                                                                                  " & vbNewLine _
                                     & "                   SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                                          " & vbNewLine _
                                     & "                        , H_EDI_PRINT.NRS_BR_CD                                                     " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                                    " & vbNewLine _
                                     & "                        , H_EDI_PRINT.DENPYO_NO                                                     " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                                         " & vbNewLine _
                                     & "                    WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                                      " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                                      " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                                      " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.PRINT_TP    = '06'                                            " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                                       " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                     & "                    GROUP BY                                                                        " & vbNewLine _
                                     & "                          H_EDI_PRINT.NRS_BR_CD                                                     " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                                    " & vbNewLine _
                                     & "                        , H_EDI_PRINT.DENPYO_NO                                                     " & vbNewLine _
                                     & "                 ) HEDIPRINT                                                                        " & vbNewLine _
                                     & "              ON HEDIPRINT.NRS_BR_CD  = BPHED.NRS_BR_CD                                             " & vbNewLine _
                                     & "             AND HEDIPRINT.EDI_CTL_NO = BPHED.EDI_CTL_NO                                            " & vbNewLine _
                                     & "--(2013.02.27) BP新システム切替対応START                                                            " & vbNewLine _
                                     & "--             AND HEDIPRINT.DENPYO_NO  = BPHED.DENPYO_NO                                             " & vbNewLine _
                                     & "             AND HEDIPRINT.DENPYO_NO  = BPHED.CUST_ORD_NO                                           " & vbNewLine _
                                     & "--(2013.02.27) BP新システム切替対応END                                                              " & vbNewLine _
                                     & "       --ユーザＭ                                                                                   " & vbNewLine _
                                     & "       LEFT JOIN $LM_MST$..S_USER S_USER                                                            " & vbNewLine _
                                     & "              ON S_USER.USER_CD = @LOGIN_USER                                                       " & vbNewLine _
                                     & "        -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)                                 " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                                             " & vbNewLine _
                                     & "                     ON M_CUSTRPT1.NRS_BR_CD   = BPHED.NRS_BR_CD                                    " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L         --@                             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M         --@                             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_S   = '00'                                               " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.PTN_ID      = 'B1'                                               " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                                                        " & vbNewLine _
                                     & "                     ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD                               " & vbNewLine _
                                     & "                    AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID                                  " & vbNewLine _
                                     & "                    AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD                                  " & vbNewLine _
                                     & "                    AND MR1.SYS_DEL_FLG        = '0'                                                " & vbNewLine _
                                     & "        -- 帳票パターンマスタ②(商品マスタより)                                                     " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                                             " & vbNewLine _
                                     & "                     ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_S   = '00'                                               " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.PTN_ID      = 'B1'                                               " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                                                        " & vbNewLine _
                                     & "                     ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD                               " & vbNewLine _
                                     & "                    AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID                                  " & vbNewLine _
                                     & "                    AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD                                  " & vbNewLine _
                                     & "                    AND MR2.SYS_DEL_FLG        = '0'                                                " & vbNewLine _
                                     & "        -- 帳票パターンマスタ③<存在しない場合の帳票パターン取得>                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT MR3                                                         " & vbNewLine _
                                     & "                     ON MR3.NRS_BR_CD          = BPHED.NRS_BR_CD                                    " & vbNewLine _
                                     & "                    AND MR3.PTN_ID             = 'B1'                                               " & vbNewLine _
                                     & "                    AND MR3.STANDARD_FLAG      = '01'                                               " & vbNewLine _
                                     & "                    AND MR3.SYS_DEL_FLG        = '0'                                                " & vbNewLine _
                                     & "        -- 外装名 ' 要望番号2471対応 added 2015.12.18                                                                                     " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_DETAILS MCDTL                                              " & vbNewLine _
                                     & "                     ON MCDTL.NRS_BR_CD        = M_GOODS.NRS_BR_CD                                  " & vbNewLine _
                                     & "                    AND  MCDTL.CUST_CD         = (CASE WHEN MCDTL.CUST_CLASS='00' THEN  M_GOODS.CUST_CD_L                                                                " & vbNewLine _
                                     & "                                                       WHEN MCDTL.CUST_CLASS='01' THEN (M_GOODS.CUST_CD_L + M_GOODS.CUST_CD_M)                                           " & vbNewLine _
                                     & "                                                       WHEN MCDTL.CUST_CLASS='02' THEN (M_GOODS.CUST_CD_L + M_GOODS.CUST_CD_M + M_GOODS.CUST_CD_S)                       " & vbNewLine _
                                     & "                                                       WHEN MCDTL.CUST_CLASS='03' THEN (M_GOODS.CUST_CD_L + M_GOODS.CUST_CD_M + M_GOODS.CUST_CD_S + M_GOODS.CUST_CD_SS)  " & vbNewLine _
                                     & "                                                  END)                                              " & vbNewLine _
                                     & "                    AND MCDTL.SET_NAIYO        = '01'                                               " & vbNewLine _
                                     & "                    AND MCDTL.SUB_KB           = '0F'                                               " & vbNewLine _
                                     & "                    AND MCDTL.SYS_DEL_FLG      = '0'                                                " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..Z_KBN OUTPKG                                                      " & vbNewLine _
                                     & "                     ON OUTPKG.KBN_GROUP_CD = MCDTL.SET_NAIYO_3                                     " & vbNewLine _
                                     & "                    AND OUTPKG.KBN_NM1      = M_GOODS.OUTER_PKG                                     " & vbNewLine


    '''' <summary>
    '''' データ抽出用WHERE句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_WHERE As String = "     WHERE                                                        " & vbNewLine _
    '                                       & "     BPHED.CUST_CD_L  = @CUST_CD_L                           " & vbNewLine _
    '                                       & " AND BPHED.CUST_CD_M  = @CUST_CD_M                           " & vbNewLine _
    '                                       & " AND BPHED.NRS_BR_CD  = @NRS_BR_CD                           " & vbNewLine _
    '                                       & " AND BPDTL.DEL_KB  = '0'                                     " & vbNewLine _
    '                                       & " --AND BPHED.EDI_CTL_NO = 'S90798333'  --                    " & vbNewLine _
    '                                       & " AND OUTEDIL.FREE_N10   = '10'         --固定(ある条件により)" & vbNewLine

    '(2013.02.27) BP新システム切替対応START
    ''' <summary>
    ''' SQL_WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String = "	 WHERE                                " & vbNewLine _
                                         & "	 BPHED.NRS_BR_CD     = @NRS_BR_CD " & vbNewLine _
                                         & "	 AND BPHED.CUST_CD_L = @CUST_CD_L " & vbNewLine _
                                         & "	 AND BPHED.CUST_CD_M = @CUST_CD_M " & vbNewLine




    ''' <summary>
    ''' SQL_EXISTS
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXISTS As String = "	 AND EXISTS (                                                                                        " & vbNewLine _
                                       & "SELECT * FROM                                                                                          " & vbNewLine _
                                       & "   (SELECT OUTEDIL.NRS_BR_CD,OUTEDIL.CUST_CD_L,OUTEDIL.CUST_CD_M,BPHED.CUST_ORD_NO ,BPHED.DENPYO_NO    " & vbNewLine _
                                       & "    --出荷(大)                                                                                         " & vbNewLine _
                                       & "    FROM $LM_TRN$..H_OUTKAEDI_HED_BP BPHED                                                             " & vbNewLine _
                                       & "          --BPｶｽﾄﾛｰﾙ出荷EDI(明細)                                                                      " & vbNewLine _
                                       & "          LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_BP BPDTL                                                  " & vbNewLine _
                                       & "                 ON BPDTL.CRT_DATE  = BPHED.CRT_DATE                                                   " & vbNewLine _
                                       & "                AND BPDTL.FILE_NAME = BPHED.FILE_NAME                                                  " & vbNewLine _
                                       & "                AND BPDTL.REC_NO    = BPHED.REC_NO                                                     " & vbNewLine _
                                       & "                AND BPDTL.DEL_KB    = '0'                                                            " & vbNewLine _
                                       & "          --EDI出荷(大)                                                                                " & vbNewLine _
                                       & "          LEFT JOIN $LM_TRN$..H_OUTKAEDI_L OUTEDIL                                                     " & vbNewLine _
                                       & "                 ON OUTEDIL.NRS_BR_CD  = BPHED.NRS_BR_CD                                               " & vbNewLine _
                                       & "                AND OUTEDIL.EDI_CTL_NO = BPHED.EDI_CTL_NO                                              " & vbNewLine _
                                       & "                AND OUTEDIL.DEL_KB     = '0'                                                         " & vbNewLine _
                                       & "          LEFT JOIN (                                                                                  " & vbNewLine _
                                       & "                      SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                                          " & vbNewLine _
                                       & "                           , H_EDI_PRINT.NRS_BR_CD                                                     " & vbNewLine _
                                       & "                           , H_EDI_PRINT.EDI_CTL_NO                                                    " & vbNewLine _
                                       & "                           , H_EDI_PRINT.DENPYO_NO                                                     " & vbNewLine _
                                       & "                        FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                                         " & vbNewLine _
                                       & "                       WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                                      " & vbNewLine _
                                       & "                         AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                                      " & vbNewLine _
                                       & "                         AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                                      " & vbNewLine _
                                       & "                         AND H_EDI_PRINT.PRINT_TP    = '06'                                          " & vbNewLine _
                                       & "                         AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                                       " & vbNewLine _
                                       & "                         AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                       & "                       GROUP BY                                                                        " & vbNewLine _
                                       & "                             H_EDI_PRINT.NRS_BR_CD                                                     " & vbNewLine _
                                       & "                           , H_EDI_PRINT.EDI_CTL_NO                                                    " & vbNewLine _
                                       & "                           , H_EDI_PRINT.DENPYO_NO                                                     " & vbNewLine _
                                       & "                    ) HEDIPRINT                                                                        " & vbNewLine _
                                       & "                 ON HEDIPRINT.NRS_BR_CD  = BPHED.NRS_BR_CD                                             " & vbNewLine _
                                       & "                AND HEDIPRINT.EDI_CTL_NO = BPHED.EDI_CTL_NO                                            " & vbNewLine _
                                       & "--                AND HEDIPRINT.DENPYO_NO  = BPHED.DENPYO_NO                                             " & vbNewLine _
                                       & "                AND HEDIPRINT.DENPYO_NO  = BPHED.CUST_ORD_NO                                           " & vbNewLine


    ''' <summary>
    ''' SQL_EXISTS_WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXISTS_WHERE As String = "	   ) BASE WHERE                   " & vbNewLine _
                                         & "     BASE.CUST_ORD_NO = BPHED.CUST_ORD_NO " & vbNewLine _
                                         & "--AND  LEFT(BASE.DENPYO_NO,7) = LEFT(BPHED.DENPYO_NO,7)      " & vbNewLine _
                                         & "AND  BASE.NRS_BR_CD   = BPHED.NRS_BR_CD   " & vbNewLine _
                                         & "AND  BASE.CUST_CD_L   = BPHED.CUST_CD_L   " & vbNewLine _
                                         & "AND  BASE.CUST_CD_M   = BPHED.CUST_CD_M   " & vbNewLine _
                                         & "AND  BPDTL.ROW_TYPE  <> 'U'               " & vbNewLine _
                                         & ")                                         " & vbNewLine

    '(2013.02.27) BP新システム切替対応END

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                     " & vbNewLine _
                                         & "      BPHED.CUST_ORD_NO      " & vbNewLine _
                                         & "    , BPHED.DENPYO_NO        " & vbNewLine _
                                         & "    , BPHED.EDI_CTL_NO       " & vbNewLine _
                                         & "    , BPDTL.GYO              " & vbNewLine _
                                         & "    , BPDTL.EDI_CTL_NO_CHU   " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMH580IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH580DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMH580DAC.SQL_FROM)             'SQL構築(データ抽出用From句)

        '(2013.02.05)要望番号1822 -- START --
        'If Me._Row.Item("PRTFLG").ToString = "1" Then
        '    '出力済の場合
        '    Call Me.SetConditionMasterSQL_OUT()
        'Else
        '    '未出力の場合
        '    Call Me.SetConditionMasterSQL()
        'End If

        '未出力・出力済どちらも出力済のSQLで対応する。
        Call Me.SetConditionMasterSQL_OUT()
        '(2013.02.05)要望番号1822 --  ENE  --

        Call Me.SetConditionPrintPatternMSQL()          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH580DAC", "SelectMPrt", cmd)

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
    ''' 納品送状対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品送状出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH580IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH580DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH580DAC.SQL_FROM)             'SQL構築(データ抽出用From句)

        ''(2013.02.27) BP新システム切替対応START
        Me._StrSql.Append(LMH580DAC.SQL_WHERE)            'SQL構築(データ抽出用WHERE句)(新規)
        Me._StrSql.Append(LMH580DAC.SQL_EXISTS)           'SQL構築(データ抽出用SELECT句(AND EXISTS))(新規)
        ''(2013.02.27) BP新システム切替対応END

        '(2013.02.05)要望番号1822 -- START --
        'If Me._Row.Item("PRTFLG").ToString = "1" Then
        '    '出力済の場合
        '    'Me._StrSql.Append(LMH580DAC.SQL_WHERE)      'SQL構築(データ抽出用Where句)
        '    Call Me.SetConditionMasterSQL_OUT()
        'Else
        '    '未出力の場合
        '    Call Me.SetConditionMasterSQL()
        'End If

        '未出力・出力済どちらも出力済のSQLで対応する。
        Call Me.SetConditionMasterSQL_OUT()
        '(2013.02.05)要望番号1822 --  ENE  --

        ''(2013.02.27) BP新システム切替対応START
        Me._StrSql.Append(LMH580DAC.SQL_EXISTS_WHERE) 'SQL構築(データ抽出用WHERE句(AND EXISTS))(新規)
        ''(2013.02.27) BP新システム切替対応END

        Me._StrSql.Append(LMH580DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        Call Me.SetConditionPrintPatternMSQL()            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH580DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("HED_DEL_KB", "HED_DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("NRS_BR_AD1", "NRS_BR_AD1")
        map.Add("NRS_BR_AD2", "NRS_BR_AD2")
        map.Add("NRS_BR_AD3", "NRS_BR_AD3")
        map.Add("NRS_BR_TEL", "NRS_BR_TEL")
        map.Add("NRS_BR_FAX", "NRS_BR_FAX")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_AD_1", "CUST_AD_1")
        map.Add("CUST_AD_2", "CUST_AD_2")
        map.Add("CUST_AD_3", "CUST_AD_3")
        map.Add("CUST_TEL", "CUST_TEL")
        map.Add("CUST_FAX", "CUST_FAX")
        map.Add("CUST_PIC", "CUST_PIC")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("HED_DATA_KB", "HED_DATA_KB")
        map.Add("HED_KITAKU_CD", "HED_KITAKU_CD")
        map.Add("HED_OUTKA_SOKO_CD", "HED_OUTKA_SOKO_CD")
        map.Add("HED_ORDER_TYPE", "HED_ORDER_TYPE")
        map.Add("HED_OUTKA_PLAN_DATE", "HED_OUTKA_PLAN_DATE")
        map.Add("HED_CUST_ORD_NO", "HED_CUST_ORD_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("DEST_NM1", "DEST_NM1")
        map.Add("DEST_NM2", "DEST_NM2")
        map.Add("DEST_AD1", "DEST_AD1")
        map.Add("DEST_AD2", "DEST_AD2")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HT_DATE", "HT_DATE")
        map.Add("HT_TIME", "HT_TIME")
        map.Add("HT_CAR_NO", "HT_CAR_NO")
        map.Add("HT_DRIVER", "HT_DRIVER")
        map.Add("HT_UNSO_CO", "HT_UNSO_CO")
        map.Add("MOSIOKURI_KB", "MOSIOKURI_KB")
        map.Add("BUMON_CD", "BUMON_CD")
        map.Add("JIGYOBU_CD", "JIGYOBU_CD")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("TOKUI_NM", "TOKUI_NM")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("HACHU_NO", "HACHU_NO")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("TENPO_CD", "TENPO_CD")
        map.Add("CHOKUSO_KB", "CHOKUSO_KB")
        map.Add("SEIKYU_KB", "SEIKYU_KB")
        map.Add("HACHU_DATE", "HACHU_DATE")
        map.Add("CHUMON_NM", "CHUMON_NM")
        map.Add("HOL_KB", "HOL_KB")
        map.Add("HED_BIKO_HED1", "HED_BIKO_HED1")
        map.Add("HED_BIKO_HED2", "HED_BIKO_HED2")
        map.Add("HAKO_NM", "HAKO_NM")
        map.Add("SIIRESAKI_CD", "SIIRESAKI_CD")
        map.Add("KR_TOKUI_CD", "KR_TOKUI_CD")
        map.Add("KEIHI_KB", "KEIHI_KB")
        map.Add("JUCHU_KB", "JUCHU_KB")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("KIGYO_NM", "KIGYO_NM")
        map.Add("HED_FILLER_1", "HED_FILLER_1")
        map.Add("HED_REC_STS", "HED_REC_STS")
        map.Add("DTL_DEL_KB", "DTL_DEL_KB")
        map.Add("DTL_DATA_KB", "DTL_DATA_KB")
        map.Add("DTL_KITAKU_CD", "DTL_KITAKU_CD")
        map.Add("DTL_OUTKA_SOKO_CD", "DTL_OUTKA_SOKO_CD")
        map.Add("DTL_ORDER_TYPE", "DTL_ORDER_TYPE")
        map.Add("DTL_OUTKA_PLAN_DATE", "DTL_OUTKA_PLAN_DATE")
        map.Add("DTL_CUST_ORD_NO", "DTL_CUST_ORD_NO")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("ROW_TYPE", "ROW_TYPE")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("TOTAL_WT", "TOTAL_WT")
        map.Add("TOTAL_QT", "TOTAL_QT")
        map.Add("LOT_FLAG", "LOT_FLAG")
        map.Add("DTL_BIKO_HED1", "DTL_BIKO_HED1")
        map.Add("DTL_BIKO_HED2", "DTL_BIKO_HED2")
        map.Add("BIKO_DTL", "BIKO_DTL")
        map.Add("BUYER_GOODS_CD", "BUYER_GOODS_CD")
        map.Add("TENPO_TANKA", "TENPO_TANKA")
        map.Add("TENPO_KINGAKU", "TENPO_KINGAKU")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("TENPO_BAIKA", "TENPO_BAIKA")
        map.Add("DTL_FILLER_1", "DTL_FILLER_1")
        map.Add("DTL_REC_STS", "DTL_REC_STS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("USER_NM", "USER_NM")
        '2013.06.17 追加START
        map.Add("OUTPUT_CNT", "OUTPUT_CNT")
        '2013.06.17 追加END

#If True Then ' 要望番号2471対応 added 2015.12.18 inoue
        map.Add("OUTER_PKG_NM", "OUTER_PKG_NM")
#End If

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH580OUT")

        Return ds

    End Function
    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        ''SQLパラメータ初期化(WHERE句で実施しているので、ここではコメント)
        'Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '荷主コード(大)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            '荷主コード(中)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOGIN_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))



        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" BPHED.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND BPHED.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND BPHED.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            ''EDI取込日(FROM)
            'whereStr = .Item("CRT_DATE_FROM").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND BPHED.CRT_DATE >= @CRT_DATE_FROM ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            'End If

            ''EDI取込日(TO)
            'whereStr = .Item("CRT_DATE_TO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND BPHED.CRT_DATE <= @CRT_DATE_TO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            'End If

            'EDI出荷予定日(FROM)
            whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND BPHED.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM ")
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI出荷予定日(TO)
            whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND BPHED.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO ")
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '(2013.02.05)要望番号1822 未出力/出力済の判断を行わない -- START --
            'プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
            'whereStr = .Item("PRTFLG").ToString()
            'Select Case whereStr
            '    Case "0"
            '        '未出力
            '        Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
            '    Case "1"
            '        '出力済
            '        Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            'End Select
            'Me._StrSql.Append(vbNewLine)
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '(2013.02.05)要望番号1822 未出力/出力済の判断を行わない --  END  --

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済み'Notes1061)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_OUT()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" BPHED.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND BPHED.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND BPHED.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            ''(2013.02.27) BP新システム切替対応で復活START
            'EDI出荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND BPHED.EDI_CTL_NO = @EDI_CTL_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If
            ''(2013.02.27) BP新システム切替対応で復活END

            ' ''(2013.02.27) BP新システム切替対応START
            ' ''伝票№(オーダー№)
            ''whereStr = .Item("DENPYO_NO").ToString()
            ''If String.IsNullOrEmpty(whereStr) = False Then
            ''    Me._StrSql.Append(" AND BPHED.DENPYO_NO = @DENPYO_NO ")
            ''    Me._StrSql.Append(vbNewLine)
            ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            ''End If

            ''伝票№(オーダー№)
            'whereStr = .Item("DENPYO_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND BPHED.CUST_ORD_NO = @DENPYO_NO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            'End If
            ' ''(2013.02.27) BP新システム切替対応END

            '(2013.02.05)要望番号1822 未出力/出力済の判断を行わない -- START --
            'プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
            'whereStr = .Item("PRTFLG").ToString()
            'Select Case whereStr
            '    Case "0"
            '        '未出力
            '        Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
            '    Case "1"
            '        '出力済
            '        Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            'End Select

            'Me._StrSql.Append(vbNewLine)
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '(2013.02.05)要望番号1822 未出力/出力済の判断を行わない --  END  --

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
