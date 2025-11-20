' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC500    : 納品書印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class LMC501DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_737 As String = " --SQL_SELECT_MAIN1                                               " & vbNewLine _
                                            & " SELECT                                                            " & vbNewLine _
                                            & " RPT_ID                                                            " & vbNewLine _
                                            & ",NRS_BR_CD                                                         " & vbNewLine _
                                            & ",PRINT_SORT                                                        " & vbNewLine _
                                            & ",DEST_CD                                                           " & vbNewLine _
                                            & ",DEST_NM                                                           " & vbNewLine _
                                            & ",DEST_AD_1                                                         " & vbNewLine _
                                            & ",DEST_AD_2                                                         " & vbNewLine _
                                            & ",DEST_AD_3                                                         " & vbNewLine _
                                            & ",DEST_TEL                                                          " & vbNewLine _
                                            & ",OUTKA_PLAN_DATE                                                   " & vbNewLine _
                                            & ",CUST_ORD_NO                                                       " & vbNewLine _
                                            & ",BUYER_ORD_NO                                                      " & vbNewLine _
                                            & ",UNSOCO_NM                                                         " & vbNewLine _
                                            & ",ARR_PLAN_DATE                                                     " & vbNewLine _
                                            & ",ARR_PLAN_TIME                                                     " & vbNewLine _
                                            & ",OUTKA_NO_L                                                        " & vbNewLine _
                                            & ",URIG_NM                                                           " & vbNewLine _
                                            & ",CUST_NM_L                                                         " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 -- START --                            " & vbNewLine _
                                            & ",CUST_NM_M                                                         " & vbNewLine _
                                            & ",DENPYO_NM                                                         " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 --  END  --                            " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_1      " & vbNewLine _
                                            & "      ELSE CUST_AD_1 END CUST_AD_1                                 " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_2      " & vbNewLine _
                                            & "      ELSE CUST_AD_2 END CUST_AD_2                                 " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_TEL       " & vbNewLine _
                                            & "      ELSE CUST_TEL END CUST_TEL                                   " & vbNewLine _
                                            & ",NRS_BR_NM                                                         " & vbNewLine _
                                            & ",NRS_BR_AD1                                                        " & vbNewLine _
                                            & ",NRS_BR_AD2                                                        " & vbNewLine _
                                            & ",NRS_BR_TEL                                                        " & vbNewLine _
                                            & ",NRS_BR_FAX                                                        " & vbNewLine _
                                            & ",PIC                                                               " & vbNewLine _
                                            & ",OUTKA_NO_M                                                        " & vbNewLine _
                                            & ",MIN(OUTKA_NO_S)          AS OUTKA_NO_S                            " & vbNewLine _
                                            & ",INKA_DATE                                                         " & vbNewLine _
                                            & ",REMARK_OUT                                                        " & vbNewLine _
                                            & ",GOODS_NM_1                                                        " & vbNewLine _
                                            & ",LOT_NO                                                            " & vbNewLine _
                                            & ",SERIAL_NO                                                         " & vbNewLine _
                                            & ",IRIME                                                             " & vbNewLine _
                                            & ",PKG_NB                                                            " & vbNewLine _
                                            & ",SUM(ALCTD_NB) / PKG_NB   AS KONSU                                 " & vbNewLine _
                                            & ",SUM(ALCTD_NB) % PKG_NB   AS HASU                                  " & vbNewLine _
                                            & ",SUM(ALCTD_NB)            AS ALCTD_NB                              " & vbNewLine _
                                            & ",PKG_UT                                                            " & vbNewLine _
                                            & ",PKG_UT_NM                                                         " & vbNewLine _
                                            & ",SUM(ALCTD_QT)            AS ALCTD_QT                              " & vbNewLine _
                                            & ",IRIME_UT                                                          " & vbNewLine _
                                            & ",WT                       AS WT                                    " & vbNewLine _
                                            & ",UNSO_WT                                                           " & vbNewLine _
                                            & ",TOU_NO                                                            " & vbNewLine _
                                            & ",SITU_NO                                                           " & vbNewLine _
                                            & ",ZONE_CD                                                           " & vbNewLine _
                                            & ",LOCA                                                              " & vbNewLine _
                                            & ",MIN(ZAN_KONSU)           AS ZAN_KONSU                             " & vbNewLine _
                                            & "--(2012.09.06)要望番号1411 残端数不具合対応 -- START --            " & vbNewLine _
                                            & "--,MIN(ZAN_HASU)          AS ZAN_HASU                              " & vbNewLine _
                                            & ",MIN(ALCTD_CAN_NB) % PKG_NB AS ZAN_HASU                            " & vbNewLine _
                                            & "--(2012.09.06)要望番号1411 残端数不具合対応 --  END  --            " & vbNewLine _
                                            & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                            & ",REMARK_M                                                          " & vbNewLine _
                                            & ",CUST_NM_S                                                         " & vbNewLine _
                                            & ",CUST_NM_S_H                                                       " & vbNewLine _
                                            & ",CUST_ORD_NO_DTL                                                   " & vbNewLine _
                                            & ",OUTKA_REMARK                                                      " & vbNewLine _
                                            & ",HAISOU_REMARK                                                     " & vbNewLine _
                                            & ",NHS_REMARK                                                        " & vbNewLine _
                                            & ",KYORI                                                             " & vbNewLine _
                                            & ",OUTKA_PKG_NB_L                                                    " & vbNewLine _
                                            & ",SOKO_NM                                                           " & vbNewLine _
                                            & ",BUSHO_NM                                                          " & vbNewLine _
                                            & ",NRS_BR_NM_NICHI                                                   " & vbNewLine _
                                            & ",MIN(ALCTD_CAN_QT)        AS ALCTD_CAN_QT                          " & vbNewLine _
                                            & ",ALCTD_KB                                                          " & vbNewLine _
                                            & ",PTN_FLAG                                                          " & vbNewLine _
                                            & ",CUST_CD_L                                                         " & vbNewLine _
                                            & ",ORDER_TYPE                       AS ORDER_TYPE                    " & vbNewLine _
                                            & ",CASE WHEN URIG_NM <> ''                                           " & vbNewLine _
                                            & "           AND URIG_NM IS NOT NULL THEN URIG_NM                    " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                           " & vbNewLine _
                                            & "      WHEN DEST_SALES_NM   <> ''                                   " & vbNewLine _
                                            & "           AND DEST_SALES_NM IS NOT NULL THEN DEST_SALES_NM        " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                           " & vbNewLine _
                                            & "      WHEN DENPYO_NM   <> ''                                       " & vbNewLine _
                                            & "           AND DENPYO_NM IS NOT NULL   THEN DENPYO_NM              " & vbNewLine _
                                            & "      ELSE CUST_NM_L                                               " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                      " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　START                 " & vbNewLine _
                                            & ",PC_KB_NM                                                          " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　 END                  " & vbNewLine _
                                            & "--【要望番号1017】 2012/04/24　注文番号明細  START                 " & vbNewLine _
                                            & ",BUYER_ORD_NO_DTL                                                  " & vbNewLine _
                                            & "--【要望番号1017】　2012/04/17 注文番号明細　 END                  " & vbNewLine _
                                            & "--日医工対策開始                                                   " & vbNewLine _
                                            & ",TORI_KB   --20120802 群馬篠崎運送でも利用。(FREE_C01)             " & vbNewLine _
                                            & "--★追加開始2012/05/11                                             " & vbNewLine _
                                            & ",SHIP_CD_L                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                             " & vbNewLine _
                                            & "--★削除開始2012/05/11                                             " & vbNewLine _
                                            & "--,CHOKUSO_NM                                                      " & vbNewLine _
                                            & "--,CHOKUSO_CD                                                      " & vbNewLine _
                                            & "--,HAKKO_DATE                                                      " & vbNewLine _
                                            & "--★削除終了2012/05/11                                             " & vbNewLine _
                                            & ",SET_NAIYO_4                                                       " & vbNewLine _
                                            & ",SET_NAIYO_5                                                       " & vbNewLine _
                                            & ",SET_NAIYO_6                                                       " & vbNewLine _
                                            & ",GOODS_NM_2                                                        " & vbNewLine _
                                            & ",KICHO_KB                                                          " & vbNewLine _
                                            & ",SEI_YURAI_KB                                                      " & vbNewLine _
                                            & ",REMARK_UPPER                                                      " & vbNewLine _
                                            & ",REMARK_LOWER                                                      " & vbNewLine _
                                            & ",GOODS_SYUBETU                                                     " & vbNewLine _
                                            & ",YUKOU_KIGEN                                                       " & vbNewLine _
                                            & ",GOODS_NM_3    --2012/05/23                                        " & vbNewLine _
                                            & ",EDISHIP_CD    --2012/05/23                                        " & vbNewLine _
                                            & ",EDIDEST_CD    --2012/05/23                                        " & vbNewLine _
                                            & "--日医工対策終了                                                   " & vbNewLine _
                                            & "--(2012.06.01)日本ファイン対応 --- START ---                       " & vbNewLine _
                                            & ",SET_NAIYO_FROM1                                                   " & vbNewLine _
                                            & ",SET_NAIYO_FROM2                                                   " & vbNewLine _
                                            & ",SET_NAIYO_FROM3                                                   " & vbNewLine _
                                            & "--(2012.06.01)日本ファイン対応 ---  END  ---                       " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                           " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_3      " & vbNewLine _
                                            & "      ELSE CUST_AD_3 END CUST_AD_3                                 " & vbNewLine _
                                            & ",DEST_REMARK                                                       " & vbNewLine _
                                            & ",DEST_SALES_CD                                                     " & vbNewLine _
                                            & ",DEST_SALES_NM                                                     " & vbNewLine _
                                            & ",DEST_SALES_AD_1                                                   " & vbNewLine _
                                            & ",DEST_SALES_AD_2                                                   " & vbNewLine _
                                            & ",DEST_SALES_AD_3                                                   " & vbNewLine _
                                            & ",DEST_SALES_TEL                                                    " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                           " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 開始20120731            " & vbNewLine _
                                            & ",SAGYO_RYAK_1                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_2                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_3                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_4                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_5                                                      " & vbNewLine _
                                            & ",DEST_SAGYO_RYAK_1                                                 " & vbNewLine _
                                            & ",DEST_SAGYO_RYAK_2                                                 " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　開始　                             " & vbNewLine _
                                            & ",SZ01_YUSO                                                         " & vbNewLine _
                                            & ",SZ01_UNSO                                                         " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　終了　                             " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731            " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                 " & vbNewLine _
                                            & ",HAISOU_REMARK_MST                                                 " & vbNewLine _
                                            & ",GOODS_NM_CUST                                                     " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                 " & vbNewLine _
                                            & "--2016/1/20 LMC815 LMC815 add start                                " & vbNewLine _
                                            & ",GODO_GOODS_CODE                                                   " & vbNewLine _
                                            & "--2016/1/20 LMC815 LMC815 add end                                  " & vbNewLine _
                                            & "FROM                                                               " & vbNewLine _
                                            & "(                                                                  " & vbNewLine _
                                            & "--★★★SQL_SELECT_OUTKA★★★  出荷テーブル                                                                                    " & vbNewLine _
                                             & "SELECT                                                                                                                         " & vbNewLine _
                                             & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                              " & vbNewLine _
                                             & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                              " & vbNewLine _
                                             & "      ELSE MR3.RPT_ID                                                                                                          " & vbNewLine _
                                             & " END              AS RPT_ID                                                                                                    " & vbNewLine _
                                             & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                                                 " & vbNewLine _
                                             & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                                                " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                                                               " & vbNewLine _
                                             & "      ELSE OUTL.DEST_CD                                                                                                        " & vbNewLine _
                                             & " END              AS DEST_CD                                                                                                   " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                               " & vbNewLine _
                                             & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
                                             & " END              AS DEST_NM                                                                                                   " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
                                             & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
                                             & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
                                             & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
                                             & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
                                             & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                                 " & vbNewLine _
                                             & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                                  " & vbNewLine _
                                             & ",OUTL.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE                                                                                     " & vbNewLine _
                                             & ",OUTL.CUST_ORD_NO       AS CUST_ORD_NO                                                                                         " & vbNewLine _
                                             & ",OUTL.BUYER_ORD_NO      AS BUYER_ORD_NO                                                                                        " & vbNewLine _
                                             & ",MUCO.UNSOCO_NM         AS UNSOCO_NM                                                                                           " & vbNewLine _
                                             & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                                       " & vbNewLine _
                                             & ",KBN1.KBN_NM1           AS ARR_PLAN_TIME                                                                                       " & vbNewLine _
                                             & ",OUTL.OUTKA_NO_L        AS OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & ",MDOUTU.DEST_NM         AS URIG_NM                                                                                             " & vbNewLine _
                                             & ",MC.CUST_NM_L           AS CUST_NM_L                                                                                           " & vbNewLine _
                                             & "--(2012.06.15) 要望番号1155 -- START --                                                                                        " & vbNewLine _
                                             & ",MC.CUST_NM_M           AS CUST_NM_M                                                                                           " & vbNewLine _
                                             & "--(2012.06.15) 要望番号1155 --  END  --                                                                                        " & vbNewLine _
                                             & ",MC.AD_1                AS CUST_AD_1                                                                                           " & vbNewLine _
                                             & ",MC.AD_2                AS CUST_AD_2                                                                                           " & vbNewLine _
                                             & ",MC.TEL                 AS CUST_TEL                                                                                            " & vbNewLine _
                                             & ",MC.DENPYO_NM           AS DENPYO_NM                                                                                            " & vbNewLine _
                                             & ",MNRS.NRS_BR_NM         AS NRS_BR_NM                                                                                           " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.AD_1                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.AD_1                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_AD1                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.AD_2                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.AD_2                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_AD2                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.TEL                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.TEL                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_TEL                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.FAX                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.FAX                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_FAX                                                                                                 " & vbNewLine _
                                             & ",MUSER.USER_NM          AS PIC                                                                                                 " & vbNewLine _
                                             & ",OUTM.OUTKA_NO_M        AS OUTKA_NO_M                                                                                          " & vbNewLine _
                                             & ",OUTS.OUTKA_NO_S        AS OUTKA_NO_S                                                                                          " & vbNewLine _
                                             & ",CASE WHEN MGD.SET_NAIYO = '1' THEN ''                                                                                         " & vbNewLine _
                                             & "      WHEN INL.INKA_STATE_KB < '50' THEN INL.INKA_DATE                                                                         " & vbNewLine _
                                             & "      WHEN INL.INKA_STATE_KB >= '50' THEN ZAI.INKO_DATE                                                                        " & vbNewLine _
                                             & "      ELSE ''                                                                                                                  " & vbNewLine _
                                             & " END                    AS INKA_DATE                                                                                           " & vbNewLine _
                                             & ",ZAI.REMARK_OUT         AS REMARK_OUT                                                                                          " & vbNewLine _
                                             & ",CASE WHEN (MDGE.GOODS_NM IS NOT NULL AND MDGE.GOODS_NM <> '' AND EDIL.OUTKA_CTL_NO IS NOT NULL) THEN MDGE.GOODS_NM            " & vbNewLine _
                                             & "      WHEN (MDGL.GOODS_NM IS NOT NULL AND MDGL.GOODS_NM <> '' AND EDIL.OUTKA_CTL_NO IS NULL) THEN MDGL.GOODS_NM                " & vbNewLine _
                                             & "      ELSE MG.GOODS_NM_1                                                                                                       " & vbNewLine _
                                             & " END                    AS GOODS_NM_1                                                                                          " & vbNewLine _
                                             & ",OUTS.LOT_NO            AS LOT_NO                                                                                              " & vbNewLine _
                                             & ",OUTS.SERIAL_NO         AS SERIAL_NO                                                                                           " & vbNewLine _
                                             & ",OUTS.IRIME             AS IRIME                                                                                               " & vbNewLine _
                                             & ",MG.PKG_NB              AS PKG_NB                                                                                              " & vbNewLine _
                                             & ",OUTS.ALCTD_NB / MG.PKG_NB     AS KONSU                                                                                        " & vbNewLine _
                                             & ",OUTS.ALCTD_NB % MG.PKG_NB     AS HASU                                                                                         " & vbNewLine _
                                             & ",OUTS.ALCTD_NB          AS ALCTD_NB                                                                                            " & vbNewLine _
                                             & ",MG.PKG_UT              AS PKG_UT                                                                                              " & vbNewLine _
                                             & ",KBN5.KBN_NM1           AS PKG_UT_NM                                                                                           " & vbNewLine _
                                             & ",OUTS.ALCTD_QT          AS ALCTD_QT                                                                                            " & vbNewLine _
                                             & ",MG.STD_IRIME_UT        AS IRIME_UT                                                                                            " & vbNewLine _
                                             & "--(2012.10.01)要望番号1445 -- START --                                                                                         " & vbNewLine _
                                             & ",UL.UNSO_WT             AS WT                                                                                                  " & vbNewLine _
                                             & "--,OUTS.BETU_WT * OUTS.ALCTD_NB  AS WT                                                                                         " & vbNewLine _
                                             & "--(2012.10.01)要望番号1445 --  END  --                                                                                         " & vbNewLine _
                                             & ",0                      AS UNSO_WT                                                                                             " & vbNewLine _
                                             & ",OUTS.TOU_NO            AS TOU_NO                                                                                              " & vbNewLine _
                                             & ",OUTS.SITU_NO           AS SITU_NO                                                                                             " & vbNewLine _
                                             & ",RTRIM(OUTS.ZONE_CD)     AS ZONE_CD                                                                                             " & vbNewLine _
                                             & ",OUTS.LOCA              AS LOCA                                                                                                " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB / MG.PKG_NB   AS ZAN_KONSU                                                                                  " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB % MG.PKG_NB   AS ZAN_HASU                                                                                   " & vbNewLine _
                                             & ",MG.GOODS_CD_CUST       AS GOODS_CD_CUST                                                                                       " & vbNewLine _
                                             & ",OUTM.REMARK            AS REMARK_M                                                                                            " & vbNewLine _
                                             & "--★★★変更開始--------------------------------                                                                                     " & vbNewLine _
                                             & ",''   AS CUST_NM_S                                                                                           " & vbNewLine _
                                             & ",''   AS CUST_NM_S_H                                                                                           " & vbNewLine _
                                             & "--★★★変更終了--------------------------------                                                                                     " & vbNewLine _
                                             & ",OUTM.CUST_ORD_NO_DTL   AS CUST_ORD_NO_DTL                                                                                     " & vbNewLine _
                                             & ",OUTL.REMARK            AS OUTKA_REMARK                                                                                        " & vbNewLine _
                                             & ",UL.REMARK              AS HAISOU_REMARK                                                                                       " & vbNewLine _
                                             & ",OUTL.NHS_REMARK        AS NHS_REMARK                                                                                          " & vbNewLine _
                                             & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  --- START ---                                             " & vbNewLine _
                                             & "--,CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                                        " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD < MSO.JIS_CD) THEN MKY3.KYORI                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD < EDIL.DEST_JIS_CD) THEN MKY4.KYORI                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS < MSO.JIS_CD) THEN MKY1.KYORI                             " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD < MDOUT.JIS) THEN MKY2.KYORI                             " & vbNewLine _
                                             & "--      ELSE 0                                                                                                                 " & vbNewLine _
                                             & "-- END                  AS KYORI                                                                                               " & vbNewLine _
                                             & ",CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                                          " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD <= MSO.JIS_CD) THEN MKY3.KYORI                        " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD <= EDIL.DEST_JIS_CD) THEN MKY4.KYORI                        " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                                         " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS <= MSO.JIS_CD) THEN MKY1.KYORI                              " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD <= MDOUT.JIS) THEN MKY2.KYORI                              " & vbNewLine _
                                             & "      ELSE 0                                                                                                                   " & vbNewLine _
                                             & " END                    AS KYORI                                                                                               " & vbNewLine _
                                             & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  ---  END  ---                                             " & vbNewLine _
                                             & ",OUTL.OUTKA_PKG_NB      AS OUTKA_PKG_NB_L                                                                                      " & vbNewLine _
                                             & ",KBN2.KBN_NM1           AS SOKO_NM                                                                                             " & vbNewLine _
                                             & ",KBN3.KBN_NM1           AS BUSHO_NM                                                                                            " & vbNewLine _
                                             & ",KBN4.KBN_NM1           AS NRS_BR_NM_NICHI                                                                                     " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_QT      AS ALCTD_CAN_QT                                                                                        " & vbNewLine _
                                             & ",OUTM.ALCTD_KB          AS ALCTD_KB                                                                                            " & vbNewLine _
                                             & ",'0'                    AS PTN_FLAG                                                                                            " & vbNewLine _
                                             & ",OUTL.CUST_CD_L         AS CUST_CD_L                                                                                           " & vbNewLine _
                                             & ",OUTL.ORDER_TYPE        AS ORDER_TYPE                                                                                          " & vbNewLine _
                                             & ",ZAI.LT_DATE                                                                                                                   " & vbNewLine _
                                             & ",OUTM.BUYER_ORD_NO_DTL AS BUYER_ORD_NO_DTL                                                                                     " & vbNewLine _
                                             & ",ZAI.GOODS_COND_KB_2 AS GOODS_COND_CD                                                                                          " & vbNewLine _
                                             & ",ZAI.REMARK AS REMARK_ZAI                                                                                                      " & vbNewLine _
                                             & ",KBN6.KBN_NM2 AS TITLE_SMPL                                                                                                    " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB AS ALCTD_CAN_NB                                                                                             " & vbNewLine _
                                             & ",OUTM.BACKLOG_QT                                                                                                               " & vbNewLine _
                                             & ",KBN7.KBN_NM2 AS DOKU_NM                                                                                                       " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                             " & vbNewLine _
                                             & ",KBN9.KBN_NM2 AS PC_KB_NM                                                                                                      " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                             & "--日医工対策開始                                                                                                               " & vbNewLine _
                                             & ",EDIL.FREE_C01           AS TORI_KB                                                                                            " & vbNewLine _
                                             & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                             & ",OUTL.SHIP_CD_L          AS SHIP_CD_L                                                                                          " & vbNewLine _
                                             & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                             & "--★削除開始2012/05/11                                                                                                         " & vbNewLine _
                                             & "--,EDIL.SHIP_NM_L          AS CHOKUSO_NM1                                                                                      " & vbNewLine _
                                             & "--,EDIL.FREE_C11           AS CHOKUSO_CD                                                                                       " & vbNewLine _
                                             & "--,EDIL.FREE_C08           AS HAKKO_DATE                                                                                       " & vbNewLine _
                                             & "--★削除終了2012/05/11                                                                                                         " & vbNewLine _
                                             & ",MCD_N.SET_NAIYO         AS SET_NAIYO_4                                                                                        " & vbNewLine _
                                             & ",MCD_N.SET_NAIYO_2       AS SET_NAIYO_5                                                                                        " & vbNewLine _
                                             & ",MCD_N.SET_NAIYO_3       AS SET_NAIYO_6                                                                                        " & vbNewLine _
                                             & ",MG.GOODS_NM_2           AS GOODS_NM_2                                                                                         " & vbNewLine _
                                             & ",EDIM.FREE_C02           AS KICHO_KB                                                                                           " & vbNewLine _
                                             & ",EDIM.FREE_C03           AS SEI_YURAI_KB                                                                                       " & vbNewLine _
                                             & ",EDIM.FREE_C04           AS REMARK_UPPER      --群馬篠崎運送利用                                                               " & vbNewLine _
                                             & ",EDIM.FREE_C05           AS REMARK_LOWER                                                                                       " & vbNewLine _
                                             & ",EDIM.FREE_C06           AS GOODS_SYUBETU     --群馬篠崎運送利用                                                               " & vbNewLine _
                                             & ",EDIM.FREE_C01           AS YUKOU_KIGEN                                                                                        " & vbNewLine _
                                             & ",MG.GOODS_NM_3           AS GOODS_NM_3         --2012/05/23                                                                    " & vbNewLine _
                                             & ",MDOUTU.EDI_CD           AS EDISHIP_CD         --2012/05/23                                                                    " & vbNewLine _
                                             & ",MDOUT.EDI_CD            AS EDIDEST_CD         --2012/05/23                                                                    " & vbNewLine _
                                             & "--日医工対策終了                                                                                                               " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ---                                                                                     " & vbNewLine _
                                             & ",MCD_F.SET_NAIYO         AS SET_NAIYO_FROM1                                                                                    " & vbNewLine _
                                             & ",MCD_F.SET_NAIYO_2       AS SET_NAIYO_FROM2                                                                                    " & vbNewLine _
                                             & ",MCD_F.SET_NAIYO_3       AS SET_NAIYO_FROM3                                                                                    " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ---                                                                                     " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                             & ",MC.AD_3                 AS CUST_AD_3                                                                                          " & vbNewLine _
                                             & ",MDOUT.REMARK            AS DEST_REMARK                                                                                        " & vbNewLine _
                                             & ",MDOUT.SALES_CD          AS DEST_SALES_CD                                                                                      " & vbNewLine _
                                             & ",MC_SALES.CUST_NM_L      AS DEST_SALES_NM                                                                                      " & vbNewLine _
                                             & ",MC_SALES.AD_1           AS DEST_SALES_AD_1                                                                                    " & vbNewLine _
                                             & ",MC_SALES.AD_2           AS DEST_SALES_AD_2                                                                                    " & vbNewLine _
                                             & ",MC_SALES.AD_3           AS DEST_SALES_AD_3                                                                                    " & vbNewLine _
                                             & ",MC_SALES.TEL            AS DEST_SALES_TEL                                                                                     " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                             & "--群馬(合算)端数行分割版向け対応(作業略称) 開始20120731                                                                        " & vbNewLine _
                                             & "--1ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 1),'') AS SAGYO_RYAK_1                                                                                        " & vbNewLine _
                                             & "--1ここまで                                                                                                                    " & vbNewLine _
                                             & "--2ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 2),'') AS SAGYO_RYAK_2                                                                                        " & vbNewLine _
                                             & "--2ここまで                                                                                                                    " & vbNewLine _
                                             & "--3ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 3),'') AS SAGYO_RYAK_3                                                                                        " & vbNewLine _
                                             & "--3ここまで                                                                                                                    " & vbNewLine _
                                             & "--4ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 4),'') AS SAGYO_RYAK_4                                                                                        " & vbNewLine _
                                             & "--4ここまで                                                                                                                    " & vbNewLine _
                                             & "--5ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 5),'') AS SAGYO_RYAK_5                                                                                        " & vbNewLine _
                                             & "--5ここまで                                                                                                                    " & vbNewLine _
                                             & "--届け先1ここから                                                                                                              " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 6),'') AS DEST_SAGYO_RYAK_1                                                                                   " & vbNewLine _
                                             & "--届け先1ここまで                                                                                                              " & vbNewLine _
                                             & "--届け先2ここから                                                                                                              " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 7),'') AS DEST_SAGYO_RYAK_2                                                                                   " & vbNewLine _
                                             & "--届け先ここまで                                                                                                               " & vbNewLine _
                                             & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731                                                                        " & vbNewLine _
                                             & "--群馬篠崎運送向け自由項目対応　開始　                                                                                         " & vbNewLine _
                                             & ",EDIM.FREE_C07           AS SZ01_YUSO --篠崎                                                                                   " & vbNewLine _
                                             & ",EDIM.FREE_C09           AS SZ01_UNSO --篠崎                                                                                   " & vbNewLine _
                                             & " --2015/1/14                                                                                                                   " & vbNewLine _
                                             & "  , MDOUT.DELI_ATT       AS  HAISOU_REMARK_MST                                                                                 " & vbNewLine _
                                             & "  , MGD43.REMARK         AS  GOODS_NM_CUST                                                                                     " & vbNewLine _
                                             & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                             & "--群馬篠崎運送向け自由項目対応　終了                                                                                         　" & vbNewLine _
                                             & "--20160120 ゴードー add start                                                                                         　       " & vbNewLine _
                                             & "  , MGDETAILS.SET_NAIYO  AS GODO_GOODS_CODE                                                                                    " & vbNewLine _
                                             & "--20160120 ゴードー add end                                                                                         　         " & vbNewLine _
                                             & "--出荷L                                                                                                                        " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "$LM_TRN$..C_OUTKA_L OUTL                                                                                                       " & vbNewLine _
                                             & "--トランザクションテーブル                                                                                                     " & vbNewLine _
                                             & "--出荷M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                                             " & vbNewLine _
                                             & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND OUTM.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                             & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                             & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                             & "       (SELECT                                                                                                                 " & vbNewLine _
                                             & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                             & "           ,OUTKA_NO_L                                                                                                         " & vbNewLine _
                                             & "           ,MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                                                                     " & vbNewLine _
                                             & "       FROM $LM_TRN$..C_OUTKA_M WHERE SYS_DEL_FLG ='0'                                                                         " & vbNewLine _
                                             & "        AND NRS_BR_CD = @NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "        AND OUTKA_NO_L = @KANRI_NO_L                                                                                           " & vbNewLine _
                                             & "       GROUP BY NRS_BR_CD,OUTKA_NO_L) OUTM_MIN                                                                                 " & vbNewLine _
                                             & "       ON OUTM_MIN.NRS_BR_CD        = OUTL.NRS_BR_CD                                                                           " & vbNewLine _
                                             & "       AND OUTM_MIN.OUTKA_NO_L      = OUTL.OUTKA_NO_L                                                                          " & vbNewLine _
                                             & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM2                                                                                            " & vbNewLine _
                                             & "ON  OUTM2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND OUTM2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                         " & vbNewLine _
                                             & "AND OUTM2.OUTKA_NO_M = OUTM_MIN.OUTKA_NO_M                                                                                     " & vbNewLine _
                                             & "AND OUTM2.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                             & "--出荷S                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                                             " & vbNewLine _
                                             & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                                          " & vbNewLine _
                                             & "AND OUTS.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--出荷EDIL                                                                                                                     " & vbNewLine _
                                             & "--(2012.09.06)要望番号1412対応  --- START ---                                                                                  " & vbNewLine _
                                             & "--LEFT JOIN                                                                                                                    " & vbNewLine _
                                             & "--(                                                                                                                            " & vbNewLine _
                                             & "--SELECT                                                                                                                       " & vbNewLine _
                                             & "-- --20120730 MIN化　開始(20120803取りやめ)                                                                                    " & vbNewLine _
                                             & "-- --MIN(NRS_BR_CD)    AS NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "-- --,MIN(OUTKA_CTL_NO) AS OUTKA_CTL_NO                                                                                        " & vbNewLine _
                                             & "-- --,MIN(CUST_CD_L)    AS CUST_CD_L                                                                                           " & vbNewLine _
                                             & "-- NRS_BR_CD                                                                                                                   " & vbNewLine _
                                             & "--,OUTKA_CTL_NO                                                                                                                " & vbNewLine _
                                             & "--,CUST_CD_L                                                                                                                   " & vbNewLine _
                                             & "-- --20120730 MIN化　終了                                                                                                      " & vbNewLine _
                                             & "----★削除開始2012/05/11                                                                                                       " & vbNewLine _
                                             & "----,SHIP_NM_L                                                                                                                 " & vbNewLine _
                                             & "----★削除終了2012/05/11                                                                                                       " & vbNewLine _
                                             & "--,MIN(DEST_CD)     AS DEST_CD                                                                                                 " & vbNewLine _
                                             & "--,MIN(DEST_NM)     AS DEST_NM                                                                                                 " & vbNewLine _
                                             & "--,MIN(DEST_AD_1)   AS DEST_AD_1                                                                                               " & vbNewLine _
                                             & "--,MIN(DEST_AD_2)   AS DEST_AD_2                                                                                               " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出 -- START --                                                             " & vbNewLine _
                                             & "----,DEST_JIS_CD                                                                                                               " & vbNewLine _
                                             & "--,MIN(DEST_JIS_CD) AS DEST_JIS_CD                                                                                             " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出 --  END  --                                                             " & vbNewLine _
                                             & "-- --20120730 MIN化　開始                                                                                                      " & vbNewLine _
                                             & "--,MIN(FREE_C01)   AS FREE_C01                                                                                                 " & vbNewLine _
                                             & "--,MIN(FREE_C08)   AS FREE_C08                                                                                                 " & vbNewLine _
                                             & "--,MIN(FREE_C11)   AS FREE_C11                                                                                                 " & vbNewLine _
                                             & "-- --20120730 MIN化　終了                                                                                                      " & vbNewLine _
                                             & "--,SYS_DEL_FLG                                                                                                                 " & vbNewLine _
                                             & "--FROM                                                                                                                         " & vbNewLine _
                                             & "--$LM_TRN$..H_OUTKAEDI_L                                                                                                       " & vbNewLine _
                                             & "--GROUP BY                                                                                                                     " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　開始 (20120803取りやめ)                                                               " & vbNewLine _
                                             & "-- NRS_BR_CD                                                                                                                   " & vbNewLine _
                                             & "--,OUTKA_CTL_NO                                                                                                                " & vbNewLine _
                                             & "--,CUST_CD_L                                                                                                                   " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　終了                                                                                  " & vbNewLine _
                                             & "----★削除開始2012/05/11                                                                                                       " & vbNewLine _
                                             & "----,SHIP_NM_L                                                                                                                 " & vbNewLine _
                                             & "----★削除開始2012/05/11                                                                                                       " & vbNewLine _
                                             & "----(2012.04.11) Notes№923  DEST_JIS_CDはMINで抽出のためコメント -- START --                                                  " & vbNewLine _
                                             & "----,DEST_JIS_CD                                                                                                               " & vbNewLine _
                                             & "----(2012.04.11) Notes№923  DEST_JIS_CDはMINで抽出のためコメント --  END  --                                                  " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　開始                                                                                  " & vbNewLine _
                                             & "----,FREE_C01                                                                                                                  " & vbNewLine _
                                             & "----,FREE_C08                                                                                                                  " & vbNewLine _
                                             & "----,FREE_C11                                                                                                                  " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　終了 ↓SYS_DEL_FLGのカンマ除去↓                                                      " & vbNewLine _
                                             & "--,SYS_DEL_FLG                                                                                                                 " & vbNewLine _
                                             & "--) EDIL                                                                                                                       " & vbNewLine _
                                             & "--ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                          " & vbNewLine _
                                             & "--下記の内容に変更                                                                                                             " & vbNewLine _
                                             & " LEFT JOIN (                                                                                                                   " & vbNewLine _
                                             & "            SELECT                                                                                                             " & vbNewLine _
                                             & "                   NRS_BR_CD                                                                                                   " & vbNewLine _
                                             & "                 , EDI_CTL_NO                                                                                                  " & vbNewLine _
                                             & "                 , OUTKA_CTL_NO                                                                                                " & vbNewLine _
                                             & "             FROM (                                                                                                            " & vbNewLine _
                                             & "                    SELECT                                                                                                     " & vbNewLine _
                                             & "                           EDIOUTL.NRS_BR_CD                                                                                   " & vbNewLine _
                                             & "                         , EDIOUTL.EDI_CTL_NO                                                                                  " & vbNewLine _
                                             & "                         , EDIOUTL.OUTKA_CTL_NO                                                                                " & vbNewLine _
                                             & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                                                          " & vbNewLine _
                                             & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                                                              , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
                                             & "                                                       ORDER BY EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                                                              , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
                                             & "                                                  )                                                                            " & vbNewLine _
                                             & "                           END AS IDX                                                                                          " & vbNewLine _
                                             & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                                                       " & vbNewLine _
                                             & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                                                           " & vbNewLine _
                                             & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                                                                    " & vbNewLine _
                                             & "                      AND EDIOUTL.OUTKA_CTL_NO = @KANRI_NO_L                                                                   " & vbNewLine _
                                             & "                  ) EBASE                                                                                                      " & vbNewLine _
                                             & "            WHERE EBASE.IDX = 1                                                                                                " & vbNewLine _
                                             & "            ) TOPEDI                                                                                                           " & vbNewLine _
                                             & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                                                                " & vbNewLine _
                                             & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                                                               " & vbNewLine _
                                             & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                                                         " & vbNewLine _
                                             & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                                                                  " & vbNewLine _
                                             & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                                                                 " & vbNewLine _
                                             & "--(2012.09.06)要望番号1412対応  ---  END  ---                                                                                  " & vbNewLine _
                                             & "--入荷L                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                                                                               " & vbNewLine _
                                             & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                                                                             " & vbNewLine _
                                             & "AND INL.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--入荷S                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                                               " & vbNewLine _
                                             & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                                                                             " & vbNewLine _
                                             & "AND INS.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--運送L                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                                " & vbNewLine _
                                             & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND UL.MOTO_DATA_KB = '20'                                                                                                     " & vbNewLine _
                                             & "AND UL.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                             & "--在庫テーブル                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                                              " & vbNewLine _
                                             & "ON  ZAI.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                                                           " & vbNewLine _
                                             & "AND ZAI.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--マスタテーブル                                                                                                               " & vbNewLine _
                                             & "--営業所M                                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_NRS_BR MNRS                                                                                              " & vbNewLine _
                                             & "ON MNRS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "--商品M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                 " & vbNewLine _
                                             & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--商品M(MIN)                                                                                                                   " & vbNewLine _
                                             & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                                  " & vbNewLine _
                                             & "ON M_GOODS_MIN.NRS_BR_CD      = OUTL.NRS_BR_CD                                                                                 " & vbNewLine _
                                             & "AND M_GOODS_MIN.GOODS_CD_NRS   = OUTM2.GOODS_CD_NRS                                                                            " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                             & "--荷主M(商品M経由)                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                                  " & vbNewLine _
                                             & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                                              " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                             & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                             & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                             & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                             & "--届先M(届先取得)(出荷L参照)                                                                                                   " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                                                                               " & vbNewLine _
                                             & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                                                                               " & vbNewLine _
                                             & "--届先M(売上先取得)(出荷L参照)                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                                                                              " & vbNewLine _
                                             & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                          " & vbNewLine _
                                             & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                                                                          " & vbNewLine _
                                             & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                                                                            " & vbNewLine _
                                             & "--届先M(届先取得)(出荷EDIL参照)                                                                                                " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDEDI                                                                                               " & vbNewLine _
                                             & "ON  MDEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MDEDI.CUST_CD_L = EDIL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MDEDI.DEST_CD = EDIL.DEST_CD                                                                                               " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                             & "--届先M(納品書荷主名義名取得)(届先M参照)                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC_SALES                                                                                            " & vbNewLine _
                                             & "       ON MC_SALES.NRS_BR_CD  = MDOUT.NRS_BR_CD                                                                                " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_L  = MDOUT.SALES_CD                                                                                 " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_M  = '00'                                                                                           " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_S  = '00'                                                                                           " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_SS = '00'                                                                                           " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                             & "--運送会社M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                                                                              " & vbNewLine _
                                             & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                                                                                " & vbNewLine _
                                             & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                                                                          " & vbNewLine _
                                             & "--倉庫M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                                                                 " & vbNewLine _
                                             & "ON  MSO.NRS_BR_CD = OUTL.NRS_BR_CD  --追加                                                                                     " & vbNewLine _
                                             & "AND MSO.WH_CD     = OUTL.WH_CD                                                                                                 " & vbNewLine _
                                             & "--距離程M(M_DEST.JIS < M_SOKO.JIS_CD)(出荷L参照)                                                                               " & vbNewLine _
                                             & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                                                                                " & vbNewLine _
                                             & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                                                                        " & vbNewLine _
                                             & "AND UL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                                                                          " & vbNewLine _
                                             & "--2014/5/16End s.kobayashi NotesNo.2183                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY1                                                                                               " & vbNewLine _
                                             & "ON  MKY1.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY1.KYORI_CD = MC.BETU_KYORI_CD                                                                                         " & vbNewLine _
                                             & "AND MKY1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY1.ORIG_JIS_CD = MDOUT.JIS                                                                                               " & vbNewLine _
                                             & "AND MKY1.DEST_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "--距離程M(M_SOKO.JIS_CD < M_DEST.JIS)(出荷L参照)                                                                               " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY2                                                                                               " & vbNewLine _
                                             & "ON  MKY2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY2.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY2.ORIG_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "AND MKY2.DEST_JIS_CD = MDOUT.JIS                                                                                               " & vbNewLine _
                                             & "--距離程M(H_OUTKAEDI_L.DEST_JIS_CD < M_SOKO.JIS_CD)(出荷EDIL参照)                                                              " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY3                                                                                               " & vbNewLine _
                                             & "ON  MKY3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY3.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY3.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY3.ORIG_JIS_CD = EDIL.DEST_JIS_CD                                                                                        " & vbNewLine _
                                             & "AND MKY3.DEST_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "--距離程M(M_SOKO.JIS_CD < H_OUTKAEDI_L.DEST_JIS_CD)(出荷EDIL参照)                                                              " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY4                                                                                               " & vbNewLine _
                                             & "ON  MKY4.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY4.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY4.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY4.ORIG_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "AND MKY4.DEST_JIS_CD = EDIL.DEST_JIS_CD                                                                                        " & vbNewLine _
                                             & "--届先商品M(出荷L参照)                                                                                                         " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DESTGOODS MDGL                                                                                           " & vbNewLine _
                                             & "ON  MDGL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND MDGL.CUST_CD_L = OUTL.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND MDGL.CUST_CD_M = OUTL.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND MDGL.CD = OUTL.DEST_CD                                                                                                     " & vbNewLine _
                                             & "AND MDGL.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "AND MDGL.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--届先商品M(出荷EDI参照)                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DESTGOODS MDGE                                                                                           " & vbNewLine _
                                             & "ON  MDGE.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND MDGE.CUST_CD_L = OUTL.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND MDGE.CUST_CD_M = OUTL.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND MDGE.CD = EDIL.DEST_CD                                                                                                     " & vbNewLine _
                                             & "AND MDGE.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "AND MDGE.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(納入予定区分)                                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                                 " & vbNewLine _
                                             & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                                                 " & vbNewLine _
                                             & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                                                                           " & vbNewLine _
                                             & "AND KBN1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用倉庫名)                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                                                 " & vbNewLine _
                                             & "ON  KBN2.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN2.KBN_CD = '00'                                                                                                         " & vbNewLine _
                                             & "AND KBN2.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用部署名)                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                                                 " & vbNewLine _
                                             & "ON  KBN3.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN3.KBN_CD = '01'                                                                                                         " & vbNewLine _
                                             & "AND KBN3.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用営業署名)                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                                                                                 " & vbNewLine _
                                             & "ON  KBN4.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN4.KBN_CD = '02'                                                                                                         " & vbNewLine _
                                             & "AND KBN4.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(荷姿)                                                                                                                  " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN5                                                                                                 " & vbNewLine _
                                             & "ON  KBN5.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                             & "AND KBN5.KBN_CD = MG.PKG_UT                                                                                                    " & vbNewLine _
                                             & "AND KBN5.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--明細タイトル（小分け）                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN6                                                                                                 " & vbNewLine _
                                             & "ON  KBN6.KBN_GROUP_CD = 'S041'                                                                                                 " & vbNewLine _
                                             & "AND KBN6.KBN_CD = OUTM.ALCTD_KB                                                                                                " & vbNewLine _
                                             & "AND KBN6.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(毒劇区分名称)                                                                                                           " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN7                                                                                                  " & vbNewLine _
                                             & "ON  KBN7.KBN_GROUP_CD = 'G001'                                                                                                  " & vbNewLine _
                                             & "AND KBN7.KBN_CD = MG.DOKU_KB                                                                                                    " & vbNewLine _
                                             & "AND KBN7.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                             " & vbNewLine _
                                             & "--区分M(元着払区分名称)                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN9                                                                                                 " & vbNewLine _
                                             & "ON  KBN9.KBN_GROUP_CD = 'M001'                                                                                                 " & vbNewLine _
                                             & "AND KBN9.KBN_CD = OUTL.PC_KB                                                                                                   " & vbNewLine _
                                             & "AND KBN9.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                             & "--日医工対策開始                                                                                                               " & vbNewLine _
                                             & "--★出荷EDI M(日医工)                                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                                                                            " & vbNewLine _
                                             & "ON  EDIM.NRS_BR_CD        = OUTM.NRS_BR_CD                                                                                     " & vbNewLine _
                                             & "AND EDIM.OUTKA_CTL_NO     = OUTM.OUTKA_NO_L                                                                                    " & vbNewLine _
                                             & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                                                                    " & vbNewLine _
                                             & "AND EDIM.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--日医工_出荷指示EDIデータ                                                                                                     " & vbNewLine _
                                             & "--LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_NIK EDIN                                                                                    " & vbNewLine _
                                             & "--ON   OUTL.NRS_BR_CD      = EDIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                             & "--AND  EDIL.EDI_CTL_NO     = EDIN.EDI_CTL_NO                                                                                     " & vbNewLine _
                                             & "--AND  EDIM.EDI_CTL_NO_CHU = EDIN.EDI_CTL_NO_CHU                                                                                 " & vbNewLine _
                                             & "--AND  EDIN.SYS_DEL_FLG    = '0'                                                                                                 " & vbNewLine _
                                             & "                                                                                                                               " & vbNewLine _
                                             & "--荷主明細M ★                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_N                                                                                       " & vbNewLine _
                                             & "ON  MCD_N.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MCD_N.CUST_CD   = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MCD_N.SUB_KB = '26'                                                                                                        " & vbNewLine _
                                             & "--日医工対策終了                                                                                                               " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ----                                                                                    " & vbNewLine _
                                             & "--荷主明細M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_F                                                                                       " & vbNewLine _
                                             & "ON  MCD_F.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MCD_F.CUST_CD   = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MCD_F.SUB_KB = '29'                                                                                                        " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  ---  END  ----                                                                                    " & vbNewLine _
                                             & "--出荷Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                             & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                             & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                             & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                             & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                             & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                             & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                             & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                             & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                             & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                             & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                             & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                             & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                             & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                             & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                             & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--ユーザM                                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..S_USER MUSER                                                                                               " & vbNewLine _
                                             & "ON MUSER.USER_CD = OUTL.SYS_ENT_USER                                                                                           " & vbNewLine _
                                             & "AND MUSER.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                             & "--商品明細M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                                        " & vbNewLine _
                                             & "ON  MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                                               " & vbNewLine _
                                             & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                         " & vbNewLine _
                                             & "AND MGD.SUB_KB = '09'                                                                                                          " & vbNewLine _
                                             & " --商品M LMC757  2015/1/15                                                              " & vbNewLine _
                                             & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD43                                                    " & vbNewLine _
                                             & "	ON  OUTM.NRS_BR_CD = MGD43.NRS_BR_CD                                              " & vbNewLine _
                                             & "	AND OUTM.GOODS_CD_NRS = MGD43.GOODS_CD_NRS                                              " & vbNewLine _
                                             & "	AND OUTL.SHIP_CD_L = MGD43.SET_NAIYO                                              " & vbNewLine _ 
                                             & " AND MGD43.SUB_KB ='43' AND MGD43.SYS_DEL_FLG ='0'                                         " & vbNewLine _
                                             & " --商品M LMC757 2015/1/15                                                                " & vbNewLine _ 
                                             & " --商品詳細マスタ LMC815 20160120 start                                                                " & vbNewLine _ 
                                             & " LEFT OUTER JOIN LM_MST..M_GOODS_DETAILS MGDETAILS                                                                " & vbNewLine _ 
                                             & " ON  OUTL.NRS_BR_CD = MGDETAILS.NRS_BR_CD                                                                " & vbNewLine _ 
                                             & " AND MG.GOODS_CD_NRS = MGDETAILS.GOODS_CD_NRS                                                                " & vbNewLine _ 
                                             & " AND MGDETAILS.SUB_KB = '56'                                                           " & vbNewLine _ 
                                             & " AND MGDETAILS.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _ 
                                             & " --商品詳細マスタ LMC815 20160120 end                                                                " & vbNewLine _ 
                                             & "WHERE                                                                                                                          " & vbNewLine _
                                             & "    OUTL.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                                                                " & vbNewLine _
                                             & "AND OUTL.OUTKA_NO_L = @KANRI_NO_L                                                                                              " & vbNewLine _
                                             & "--★★★SQL_SELECT_UNSO★★★ 運送テーブル                                                                                    " & vbNewLine _
                                            & "UNION ALL                                                                                                                      " & vbNewLine _
                                            & "SELECT                                                                                                                         " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                              " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                              " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                                                                          " & vbNewLine _
                                            & " END                                                                             AS RPT_ID                                     " & vbNewLine _
                                            & ",UNSOL.NRS_BR_CD                                                                 AS NRS_BR_CD                                  " & vbNewLine _
                                            & ",0                                                                               AS PRINT_SORT                                 " & vbNewLine _
                                            & ",UNSOL.DEST_CD                                                                   AS DEST_CD                                    " & vbNewLine _
                                            & ",MD.DEST_NM                                                                      AS DEST_NM                                    " & vbNewLine _
                                            & ",MD.AD_1                                                                         AS DEST_AD_1                                  " & vbNewLine _
                                            & ",MD.AD_2                                                                         AS DEST_AD_2                                  " & vbNewLine _
                                            & "--(2014.07.02)修正START                                                                                                        " & vbNewLine _
                                            & ",UNSOL.AD_3                                                                      AS DEST_AD_3                                  " & vbNewLine _
                                            & "--,MD.AD_3                                                                         AS DEST_AD_3                                  " & vbNewLine _
                                            & "--(2014.07.02)修正END                                                                                                          " & vbNewLine _
                                            & ",MD.TEL                                                                          AS DEST_TEL                                   " & vbNewLine _
                                            & ",UNSOL.OUTKA_PLAN_DATE                                                           AS OUTKA_PLAN_DATE                            " & vbNewLine _
                                            & ",UNSOL.CUST_REF_NO                                                               AS CUST_ORD_NO                                " & vbNewLine _
                                            & ",UNSOL.BUY_CHU_NO                                                                AS BUYER_ORD_NO                               " & vbNewLine _
                                            & ",UNSOCO.UNSOCO_NM                                                                AS UNSOCO_NM                                  " & vbNewLine _
                                            & ",UNSOL.ARR_PLAN_DATE                                                             AS ARR_PLAN_DATE                              " & vbNewLine _
                                            & ",KBN1.KBN_NM1                                                                    AS ARR_PLAN_TIME                              " & vbNewLine _
                                            & ",CASE WHEN RTRIM(UNSOL.INOUTKA_NO_L) <> '' THEN UNSOL.INOUTKA_NO_L                                                             " & vbNewLine _
                                            & "      ELSE UNSOL.UNSO_NO_L                                                                                                     " & vbNewLine _
                                            & " END                                                                             AS OUTKA_NO_L                                 " & vbNewLine _
                                            & ",MDU.DEST_NM                                                                     AS URIG_NM                                    " & vbNewLine _
                                            & ",MC.CUST_NM_L                                                                    AS CUST_NM_L                                  " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 -- START --                                                                                        " & vbNewLine _
                                            & ",MC.CUST_NM_M                                                                    AS CUST_NM_M                                  " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 --  END  --                                                                                        " & vbNewLine _
                                            & ",MC.AD_1                                                                         AS CUST_AD_1                                  " & vbNewLine _
                                            & ",MC.AD_2                                                                         AS CUST_AD_2                                  " & vbNewLine _
                                            & ",MC.TEL                                                                          AS CUST_TEL                                   " & vbNewLine _
                                            & ",MC.DENPYO_NM                                                                    AS DENPYO_NM                                  " & vbNewLine _
                                            & "--(2014.07.08)修正箇所START                                                                                                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.NRS_BR_NM ELSE MS_UNSO.WH_NM  END NRS_BR_NM                     " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.AD_1      ELSE MS_UNSO.AD_1   END NRS_BR_AD1                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.AD_2      ELSE MS_UNSO.AD_2   END NRS_BR_AD2                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.TEL       ELSE MS_UNSO.TEL    END NRS_BR_TEL                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.FAX       ELSE MS_UNSO.FAX    END NRS_BR_FAX                    " & vbNewLine _
                                            & "--,MB.NRS_BR_NM                                                                    AS NRS_BR_NM                                  " & vbNewLine _
                                            & "--,MB.AD_1                                                                         AS NRS_BR_AD1                                 " & vbNewLine _
                                            & "--,MB.AD_2                                                                         AS NRS_BR_AD2                                 " & vbNewLine _
                                            & "--,MB.TEL                                                                          AS NRS_BR_TEL                                 " & vbNewLine _
                                            & "--,MB.FAX                                                                          AS NRS_BR_FAX                                 " & vbNewLine _
                                            & "--(2014.07.08)修正箇所END                                                                                                    " & vbNewLine _
                                            & ",USER_E.USER_NM                                                                  AS PIC                                        " & vbNewLine _
                                            & ",UNSOM.UNSO_NO_M                                                                 AS OUTKA_NO_M                                 " & vbNewLine _
                                            & ",''                                                                              AS OUTKA_NO_S                                 " & vbNewLine _
                                            & ",''                                                                              AS INKA_DATE                                  " & vbNewLine _
                                            & ",''                                                                              AS REMARK_OUT                                 " & vbNewLine _
                                            & "--NOTES 1032 START①                                                                                                           " & vbNewLine _
                                            & "--,CASE WHEN (MDG.GOODS_NM IS NOT NULL AND MDG.GOODS_NM <> '') THEN MDG.GOODS_NM                                               " & vbNewLine _
                                            & "--      ELSE MG.GOODS_NM_1                                                                                                     " & vbNewLine _
                                            & "--END                                                                             AS GOODS_NM_1                                " & vbNewLine _
                                            & ",CASE WHEN (MDG.GOODS_NM IS NOT NULL AND MDG.GOODS_NM <> '') THEN MDG.GOODS_NM   --Case1                                       " & vbNewLine _
                                            & "	  WHEN (MG.GOODS_NM_1 IS NOT NULL AND MG.GOODS_NM_1 <> '') THEN MG.GOODS_NM_1   --Case2                                       " & vbNewLine _
                                            & "      ELSE UNSOM.GOODS_NM                                                        --Case3                                       " & vbNewLine _
                                            & " END                                                                             AS GOODS_NM_1                                 " & vbNewLine _
                                            & "--NOTES 1032 END①                                                                                                             " & vbNewLine _
                                            & ",UNSOM.LOT_NO                                                                    AS LOT_NO                                     " & vbNewLine _
                                            & ",''                                                                              AS SERIAL_NO                                  " & vbNewLine _
                                            & ",UNSOM.IRIME                                                                     AS IRIME                                      " & vbNewLine _
                                            & "--NOTES 1032 START②                                                                                                           " & vbNewLine _
                                            & "--,MG.PKG_NB                                                                     AS PKG_NB                                     " & vbNewLine _
                                            & ",CASE WHEN (MG.PKG_NB IS NOT NULL AND MG.PKG_NB <> 0 ) THEN MG.PKG_NB                                                          " & vbNewLine _
                                            & "      ELSE UNSOM.PKG_NB                                                                                                        " & vbNewLine _
                                            & " END                                                                             AS PKG_NB                                     " & vbNewLine _
                                            & "--NOTES 1032 END②                                                                                                             " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB / MG.PKG_NB                                                   AS KONSU                                      " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB % MG.PKG_NB                                                   AS HASU                                       " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB                                                               AS ALCTD_NB                                   " & vbNewLine _
                                            & "--NOTES 1032 START③                                                                                                           " & vbNewLine _
                                            & "--,MG.PKG_UT                                                                     AS PKG_UT                                     " & vbNewLine _
                                            & ",CASE WHEN (MG.PKG_UT IS NOT NULL AND MG.PKG_UT <> '') THEN MG.PKG_UT                                                          " & vbNewLine _
                                            & "      ELSE UNSOM.QT_UT                                                                                                         " & vbNewLine _
                                            & " END                                                                             AS PKG_UT                                     " & vbNewLine _
                                            & "--NOTES 1032 END③                                                                                                             " & vbNewLine _
                                            & "--NOTES 1032 START④                                                                                                           " & vbNewLine _
                                            & "--2012/05/24 START                                                                                                             " & vbNewLine _
                                            & "--,KBN2.KBN_NM1                                                                    AS PKG_UT_NM                                " & vbNewLine _
                                            & "--,CASE WHEN (MG.PKG_UT IS NOT NULL AND MG.PKG_UT <> '') THEN KBN2.KBN_NM1                                                     " & vbNewLine _
                                            & "--      ELSE KBN2_1.KBN_NM1                                                                                                    " & vbNewLine _
                                            & "-- END                                                                             AS PKG_UT_NM                                " & vbNewLine _
                                            & ",CASE WHEN (KBN2.KBN_NM1 IS NOT NULL AND KBN2.KBN_NM1 <> '') THEN KBN2.KBN_NM1                                                 " & vbNewLine _
                                            & "      ELSE KBN2_1.KBN_NM1                                                                                                      " & vbNewLine _
                                            & " END                                                                             AS PKG_UT_NM                                  " & vbNewLine _
                                            & "--2012/05/24 END                                                                                                               " & vbNewLine _
                                            & "--NOTES 1032 END④                                                                                                             " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_QT                                                               AS ALCTD_QT                                   " & vbNewLine _
                                            & "--NOTES 1032 START⑤                                                                                                           " & vbNewLine _
                                            & "--,MG.STD_IRIME_UT                                                                 AS IRIME_UT                                 " & vbNewLine _
                                            & ",CASE WHEN (MG.STD_IRIME_UT IS NOT NULL AND MG.STD_IRIME_UT <> '') THEN MG.STD_IRIME_UT                                        " & vbNewLine _
                                            & "      ELSE UNSOM.IRIME_UT                                                                                                      " & vbNewLine _
                                            & " END                                                                             AS IRIME_UT                                   " & vbNewLine _
                                            & "--NOTES 1032 END⑤                                                                                                             " & vbNewLine _
                                            & ",0                                                                               AS WT                                         " & vbNewLine _
                                            & ",UNSOL.UNSO_WT                                                                   AS UNSO_WT                                    " & vbNewLine _
                                            & ",''                                                                              AS TOU_NO                                     " & vbNewLine _
                                            & ",''                                                                              AS SITU_NO                                    " & vbNewLine _
                                            & ",''                                                                              AS ZONE_CD                                    " & vbNewLine _
                                            & ",''                                                                              AS LOCA                                       " & vbNewLine _
                                            & ",0                                                                               AS ZAN_KONSU                                  " & vbNewLine _
                                            & ",0                                                                               AS ZAN_HASU                                   " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST                                                                AS GOODS_CD_CUST                              " & vbNewLine _
                                            & ",''                                                                              AS REMARK_M                                   " & vbNewLine _
                                            & "--★★★変更開始--------------------------------                                                                               " & vbNewLine _
                                            & ",''                                                            AS CUST_NM_S                                                    " & vbNewLine _
                                            & ",''                                                            AS CUST_NM_S_H                                                  " & vbNewLine _
                                            & "--★★★変更--------------------------------                                                                                   " & vbNewLine _
                                            & ",UNSOL.CUST_REF_NO                                                               AS CUST_ORD_NO_DTL                            " & vbNewLine _
                                            & ",''                                                                              AS OUTKA_REMARK                               " & vbNewLine _
                                            & ",UNSOL.REMARK                                                                    AS HAISOU_REMARK                              " & vbNewLine _
                                            & ",''                                                                              AS NHS_REMARK                                 " & vbNewLine _
                                            & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  --- START ---                                             " & vbNewLine _
                                            & "--,CASE WHEN MD.KYORI > 0 THEN MD.KYORI                                                                                        " & vbNewLine _
                                            & "--      WHEN (MD.KYORI = 0 AND MDO.JIS < MD.JIS) THEN MK1.KYORI                                                                " & vbNewLine _
                                            & "--      WHEN (MD.KYORI = 0 AND MD.JIS < MDO.JIS) THEN MK2.KYORI                                                                " & vbNewLine _
                                            & "--      ELSE 0                                                                                                                 " & vbNewLine _
                                            & "-- END                                                                           AS KYORI                                      " & vbNewLine _
                                            & ",CASE WHEN MD.KYORI > 0 THEN MD.KYORI                                                                                          " & vbNewLine _
                                            & "      WHEN (MD.KYORI = 0 AND MDO.JIS <= MD.JIS) THEN MK1.KYORI                                                                 " & vbNewLine _
                                            & "      WHEN (MD.KYORI = 0 AND MD.JIS <= MDO.JIS) THEN MK2.KYORI                                                                 " & vbNewLine _
                                            & "      ELSE 0                                                                                                                   " & vbNewLine _
                                            & " END                                                                             AS KYORI                                      " & vbNewLine _
                                            & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  ---  END  ---                                             " & vbNewLine _
                                            & ",UNSOL.UNSO_PKG_NB                                                               AS OUTKA_PKG_NB_L                             " & vbNewLine _
                                            & ",''                                                                              AS SOKO_NM                                    " & vbNewLine _
                                            & ",''                                                                              AS BUSHO_NM                                   " & vbNewLine _
                                            & ",''                                                                              AS NRS_BR_NM_NICHI                            " & vbNewLine _
                                            & ",0                                                                               AS ALCTD_CAN_QT                               " & vbNewLine _
                                            & ",''                                                                              AS ALCTD_KB                                   " & vbNewLine _
                                            & ",'1'                                                                             AS PTN_FLAG                                   " & vbNewLine _
                                            & ",UNSOL.CUST_CD_L                                                                 AS CUST_CD_L                                  " & vbNewLine _
                                            & ",''                                                                              AS ORDER_TYPE                                 " & vbNewLine _
                                            & ",''                                                                              AS LT_DATE                                     " & vbNewLine _
                                            & ",''                                                                              AS BUYER_ORD_NO_DTL                            " & vbNewLine _
                                            & ",''                                                                              AS GOODS_COND_CD                               " & vbNewLine _
                                            & ",''                                                                              AS REMARK_ZAI                                  " & vbNewLine _
                                            & ",''                                                                              AS TITLE_SMPL                                  " & vbNewLine _
                                            & ",0                                                                               AS ALCTD_CAN_NB                                " & vbNewLine _
                                            & ",0                                                                               AS BACKLOG_QT                                  " & vbNewLine _
                                            & ",KBN3.KBN_NM2                                                                    AS DOKU_NM                                     " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                              " & vbNewLine _
                                            & ",KBN9.KBN_NM2                                                                    AS PC_KB_NM                                   " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                            & "--日医工対策開始                                                                                                               " & vbNewLine _
                                            & ",''                       AS TORI_KB                                                                                           " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & ",UNSOL.SHIP_CD            AS SHIP_CD_L                                                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--★削除開始2012/05/11                                                                                                         " & vbNewLine _
                                            & "--,''                       AS CHOKUSO_NM1                                                                                     " & vbNewLine _
                                            & "--,''                       AS CHOKUSO_CD                                                                                      " & vbNewLine _
                                            & "--,''                       AS HAKKO_DATE                                                                                      " & vbNewLine _
                                            & "--,''                       AS SET_NAIYO_4                                                                                     " & vbNewLine _
                                            & "--,''                       AS SET_NAIYO_5                                                                                     " & vbNewLine _
                                            & "--,''                       AS SET_NAIYO_6                                                                                     " & vbNewLine _
                                            & "--,''                       AS GOODS_NM_2                                                                                      " & vbNewLine _
                                            & "--★削除終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & ",MCD_N.SET_NAIYO         AS SET_NAIYO_4                                                                                        " & vbNewLine _
                                            & ",MCD_N.SET_NAIYO_2       AS SET_NAIYO_5                                                                                        " & vbNewLine _
                                            & ",MCD_N.SET_NAIYO_3       AS SET_NAIYO_6                                                                                        " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & ",''                       AS KICHO_KB                                                                                          " & vbNewLine _
                                            & ",''                       AS SEI_YURAI_KB                                                                                      " & vbNewLine _
                                            & ",''                       AS REMARK_UPPER                                                                                      " & vbNewLine _
                                            & ",''                       AS REMARK_LOWER                                                                                      " & vbNewLine _
                                            & ",''                       AS GOODS_SYUBETU                                                                                     " & vbNewLine _
                                            & ",''                       AS YUKOU_KIGEN                                                                                       " & vbNewLine _
                                            & ",''                       AS GOODS_NM_3         --2012/05/23                                                                   " & vbNewLine _
                                            & ",MDU.EDI_CD               AS EDISHIP_CD         --2012/05/23                                                                   " & vbNewLine _
                                            & ",MD.EDI_CD                AS EDIDEST_CD         --2012/05/23                                                                   " & vbNewLine _
                                            & "--日医工対策終了                                                                                                               " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応 --- START ---                                                                                      " & vbNewLine _
                                            & ",MCD_F.SET_NAIYO         AS SET_NAIYO_FROM1                                                                                    " & vbNewLine _
                                            & ",MCD_F.SET_NAIYO_2       AS SET_NAIYO_FROM2                                                                                    " & vbNewLine _
                                            & ",MCD_F.SET_NAIYO_3       AS SET_NAIYO_FROM3                                                                                    " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応 ---  END  ---                                                                                      " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                            & ",MC.AD_3                 AS CUST_AD_3                                                                                          " & vbNewLine _
                                            & ",MD.REMARK            AS DEST_REMARK                                                                                           " & vbNewLine _
                                            & ",MD.SALES_CD          AS DEST_SALES_CD                                                                                         " & vbNewLine _
                                            & ",MC_SALES.CUST_NM_L      AS DEST_SALES_NM                                                                                      " & vbNewLine _
                                            & ",MC_SALES.AD_1           AS DEST_SALES_AD_1                                                                                    " & vbNewLine _
                                            & ",MC_SALES.AD_2           AS DEST_SALES_AD_2                                                                                    " & vbNewLine _
                                            & ",MC_SALES.AD_3           AS DEST_SALES_AD_3                                                                                    " & vbNewLine _
                                            & ",MC_SALES.TEL            AS DEST_SALES_TEL                                                                                     " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 開始20120731                                                                        " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_1                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_2                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_3                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_4                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_5                                                                                       " & vbNewLine _
                                            & ",''                      AS DEST_SAGYO_RYAK_1                                                                                  " & vbNewLine _
                                            & ",''                      AS DEST_SAGYO_RYAK_2                                                                                  " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731                                                                        " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　開始　                                                                                         " & vbNewLine _
                                            & ",''                       AS SZ01_YUSO          --篠崎                                                                         " & vbNewLine _
                                            & ",''                       AS SZ01_UNSO          --篠崎                                                                         " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                            & "  , MD.DELI_ATT           AS  HAISOU_REMARK_MST                                                                                " & vbNewLine _
                                            & "  , MGD43.REMARK          AS GOODS_NM_CUST                                                                                     " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　終了                                                                                         　" & vbNewLine _
                                            & "--20160120 ゴードー add start                                                                                         　       " & vbNewLine _
                                            & "  , MGDETAILS.SET_NAIYO   AS GODO_GOODS_CODE                                                                                   " & vbNewLine _
                                            & "--20160120 ゴードー add end                                                                                         　         " & vbNewLine _
                                            & "FROM                                                                                                                           " & vbNewLine _
                                            & "--運送L                                                                                                                        " & vbNewLine _
                                            & "$LM_TRN$..F_UNSO_L UNSOL                                                                                                       " & vbNewLine _
                                            & "--運送M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM                                                                                             " & vbNewLine _
                                            & "ON  UNSOM.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                            & "AND UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                                                                          " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                            & "--運送M(中MIN)                                                                                                                 " & vbNewLine _
                                            & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                            & "       (SELECT                                                                                                                 " & vbNewLine _
                                            & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                            & "           ,UNSO_NO_L                                                                                                          " & vbNewLine _
                                            & "           ,MIN(UNSO_NO_M) AS  UNSO_NO_M                                                                                       " & vbNewLine _
                                            & "       FROM $LM_TRN$..F_UNSO_M WHERE SYS_DEL_FLG ='0'                                                                          " & vbNewLine _
                                            & "        AND NRS_BR_CD = @NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "        AND UNSO_NO_L = @KANRI_NO_L                                                                                            " & vbNewLine _
                                            & "       GROUP BY NRS_BR_CD,UNSO_NO_L) UNSOM_MIN                                                                                 " & vbNewLine _
                                            & "       ON UNSOM_MIN.NRS_BR_CD       = UNSOL.NRS_BR_CD                                                                          " & vbNewLine _
                                            & "       AND UNSOM_MIN.UNSO_NO_L      = UNSOL.UNSO_NO_L                                                                          " & vbNewLine _
                                            & "--運送M(中MIN)                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM2                                                                                            " & vbNewLine _
                                            & "ON  UNSOM2.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                         " & vbNewLine _
                                            & "AND UNSOM2.UNSO_NO_L = UNSOL.UNSO_NO_L                                                                                         " & vbNewLine _
                                            & "AND UNSOM2.UNSO_NO_M = UNSOM_MIN.UNSO_NO_M                                                                                     " & vbNewLine _
                                            & "AND UNSOM2.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "--★★追加終了--------------------------------                                                                                 " & vbNewLine _
                                            & "--商品M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                 " & vbNewLine _
                                            & "ON  MG.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MG.NRS_BR_CD = UNSOM.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MG.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                                                                       " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                             & "--商品M(MIN)                                                                                                                  " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                                  " & vbNewLine _
                                            & "ON M_GOODS_MIN.NRS_BR_CD      = UNSOL.NRS_BR_CD                                                                                " & vbNewLine _
                                            & "AND M_GOODS_MIN.GOODS_CD_NRS   = UNSOM2.GOODS_CD_NRS                                                                           " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                            & "--運送会社M                                                                                                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                                                                            " & vbNewLine _
                                            & "ON  UNSOCO.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "AND UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                         " & vbNewLine _
                                            & "AND UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                                                                           " & vbNewLine _
                                            & "AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                                                                     " & vbNewLine _
                                            & "--届先M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MD                                                                                                  " & vbNewLine _
                                            & "ON  MD.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MD.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MD.CUST_CD_L = UNSOL.CUST_CD_L                                                                                             " & vbNewLine _
                                            & "AND MD.DEST_CD = UNSOL.DEST_CD                                                                                                 " & vbNewLine _
                                            & "--届先M(売上先取得)                                                                                                            " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MDU                                                                                                 " & vbNewLine _
                                            & "ON  MDU.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDU.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDU.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDU.DEST_CD = UNSOL.SHIP_CD                                                                                                " & vbNewLine _
                                            & "--届先M(距離取得)                                                                                                              " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MDO                                                                                                 " & vbNewLine _
                                            & "ON  MDO.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDO.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDO.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDO.DEST_CD = UNSOL.ORIG_CD                                                                                                " & vbNewLine _
                                            & "--荷主M(商品M経由)                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                                  " & vbNewLine _
                                            & "ON  MC.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MC.NRS_BR_CD = MG.NRS_BR_CD                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                                              " & vbNewLine _
                                            & "--(2014.07.08)追加箇所START                                                                                                    " & vbNewLine _
                                            & "--倉庫M(荷主M経由)                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SOKO MS_UNSO                                                                                             " & vbNewLine _
                                            & "ON  MS_UNSO.SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
                                            & "AND MC.NRS_BR_CD = MS_UNSO.NRS_BR_CD                                                                                           " & vbNewLine _
                                            & "AND MC.DEFAULT_SOKO_CD = MS_UNSO.WH_CD                                                                                         " & vbNewLine _
                                            & "--(2014.07.08)追加箇所END                                                                                                      " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                            & "--届先M(納品書荷主名義名取得)(届先M参照)                                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC_SALES                                                                                            " & vbNewLine _
                                            & "       ON MC_SALES.NRS_BR_CD  = MD.NRS_BR_CD                                                                                   " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_L  = MD.SALES_CD                                                                                    " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_M  = '00'                                                                                           " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_S  = '00'                                                                                           " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_SS = '00'                                                                                           " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                            & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                                                                                " & vbNewLine _
                                            & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                                                                        " & vbNewLine _
                                            & "AND UNSOL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                                                                       " & vbNewLine _
                                            & "--2014/5/16End s.kobayashi NotesNo.2183                                                                                       " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)                                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_KYORI MK1                                                                                                " & vbNewLine _
                                            & "ON  MK1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MK1.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "--AND MK1.KYORI_CD  = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                            & "AND MK1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                        " & vbNewLine _
                                            & "AND MK1.ORIG_JIS_CD = MDO.JIS                                                                                                  " & vbNewLine _
                                            & "AND MK1.DEST_JIS_CD = MD.JIS                                                                                                   " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)                                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_KYORI MK2                                                                                                " & vbNewLine _
                                            & "ON  MK2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MK2.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "--AND MK2.KYORI_CD  = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                            & "AND MK2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                        " & vbNewLine _
                                            & "AND MK2.ORIG_JIS_CD = MD.JIS                                                                                                   " & vbNewLine _
                                            & "AND MK2.DEST_JIS_CD = MDO.JIS                                                                                                  " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                            & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                            & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                            & "--営業所M                                                                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_NRS_BR MB                                                                                                " & vbNewLine _
                                            & "ON  MB.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MB.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "--届先商品M(運送L参照)                                                                                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DESTGOODS MDG                                                                                            " & vbNewLine _
                                            & "ON  MDG.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDG.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDG.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDG.CUST_CD_M = UNSOL.CUST_CD_M                                                                                            " & vbNewLine _
                                            & "AND MDG.CD = UNSOL.DEST_CD                                                                                                     " & vbNewLine _
                                            & "AND MDG.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                                                                      " & vbNewLine _
                                            & "--区分M(納入予定区分)                                                                                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                                 " & vbNewLine _
                                            & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                                                 " & vbNewLine _
                                            & "AND KBN1.KBN_CD = UNSOL.ARR_PLAN_TIME                                                                                          " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--区分M(荷姿)                                                                                                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                                                 " & vbNewLine _
                                            & "ON  KBN2.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                            & "AND KBN2.KBN_CD = MG.PKG_UT                                                                                                    " & vbNewLine _
                                            & "AND KBN2.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--区分M(荷姿)F運送M版(Notes1032)                                                                                               " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN2_1                                                                                                 " & vbNewLine _
                                            & "ON  KBN2_1.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                            & "AND KBN2_1.KBN_CD = UNSOM.QT_UT                                                                                                    " & vbNewLine _
                                            & "AND KBN2_1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--SYS_ENT_USER名称取得                                                                                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER USER_E                                                                                              " & vbNewLine _
                                            & "ON  USER_E.USER_CD     = UNSOL.SYS_ENT_USER                                                                                    " & vbNewLine _
                                            & "AND USER_E.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "--区分M(毒劇区分名称)                                                                                                           " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                                                  " & vbNewLine _
                                            & "ON  KBN3.KBN_GROUP_CD = 'G001'                                                                                                  " & vbNewLine _
                                            & "AND KBN3.KBN_CD = MG.DOKU_KB                                                                                                    " & vbNewLine _
                                            & "AND KBN3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                             " & vbNewLine _
                                            & "--区分M(元着払区分名称)                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN9                                                                                                 " & vbNewLine _
                                            & "ON  KBN9.KBN_GROUP_CD = 'M001'                                                                                                 " & vbNewLine _
                                            & "AND KBN9.KBN_CD = UNSOL.PC_KB                                                                                                  " & vbNewLine _
                                            & "AND KBN9.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & "--荷主明細M ★                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_N                                                                                       " & vbNewLine _
                                            & "ON  MCD_N.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND MCD_N.CUST_CD   = UNSOL.CUST_CD_L                                                                                          " & vbNewLine _
                                            & "AND MCD_N.SUB_KB = '26'                                                                                                        " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ---                                                                                     " & vbNewLine _
                                            & "--荷主明細M ★                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_F                                                                                       " & vbNewLine _
                                            & "ON  MCD_F.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND MCD_F.CUST_CD   = UNSOL.CUST_CD_L                                                                                          " & vbNewLine _
                                            & "AND MCD_F.SUB_KB = '29'                                                                                                        " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応  ---  END  ---                                                                                     " & vbNewLine _
                                            & "                                                                                                                               " & vbNewLine _
                                            & "--運送Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                            & "ON  UNSOL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                           " & vbNewLine _
                                            & "AND UNSOL.CUST_CD_L = MCR1.CUST_CD_L                                                                                           " & vbNewLine _
                                            & "AND UNSOL.CUST_CD_M = MCR1.CUST_CD_M                                                                                           " & vbNewLine _
                                            & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                            & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                            & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                            & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                            & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                            & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                            & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                            & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                            & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                            & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                            & "ON  MR3.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                            & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                            & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--商品明細M                                                                                                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                                        " & vbNewLine _
                                            & "ON  MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                                               " & vbNewLine _
                                            & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                         " & vbNewLine _
                                            & "AND MGD.SUB_KB = '09'                                                                                                          " & vbNewLine _
                                            & " --商品M LMC757   2015/1/14                                                             " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD43                                                    " & vbNewLine _
                                            & "	ON  UNSOM.NRS_BR_CD = MGD43.NRS_BR_CD                                              " & vbNewLine _
                                            & "	AND UNSOM.GOODS_CD_NRS = MGD43.GOODS_CD_NRS                                              " & vbNewLine _
                                            & "	AND UNSOL.SHIP_CD = MGD43.SET_NAIYO                                              " & vbNewLine _
                                            & " AND MGD43.SUB_KB ='43' AND MGD43.SYS_DEL_FLG ='0'                                         " & vbNewLine _
                                            & " --商品M LMC757   2015/1/14                                                             " & vbNewLine _
                                            & " --20160120 tsunehira add                                                             " & vbNewLine _
                                            & " LEFT OUTER JOIN LM_MST..M_GOODS_DETAILS MGDETAILS                                                             " & vbNewLine _
                                            & " ON  MG.NRS_BR_CD = MGDETAILS.NRS_BR_CD                                                             " & vbNewLine _
                                            & " AND MG.GOODS_CD_NRS = MGDETAILS.GOODS_CD_NRS                                                             " & vbNewLine _
                                            & " AND MGDETAILS.SUB_KB = '56'                                                             " & vbNewLine _
                                            & " AND MGDETAILS.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                            & "WHERE                                                                                                                          " & vbNewLine _
                                            & "    UNSOL.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                            & "AND UNSOL.NRS_BR_CD = @NRS_BR_CD                                                                                               " & vbNewLine _
                                            & "AND UNSOL.UNSO_NO_L = @KANRI_NO_L                                                                                              " & vbNewLine _
                                            & "--★★★SQL_WHERE_MAIN★★★                   " & vbNewLine _
                                            & "  ) MAIN                                       " & vbNewLine _
                                            & "WHERE                                          " & vbNewLine _
                                            & " PTN_FLAG = @PTN_FLAG                          " & vbNewLine _
                                            &"  --SQL_GROUP_BY1                               " & vbNewLine _
                                         & "GROUP BY                                       " & vbNewLine _
                                         & " RPT_ID                                        " & vbNewLine _
                                         & ",NRS_BR_CD                                     " & vbNewLine _
                                         & ",PRINT_SORT                                    " & vbNewLine _
                                         & ",DEST_CD                                       " & vbNewLine _
                                         & ",DEST_NM                                       " & vbNewLine _
                                         & ",DEST_AD_1                                     " & vbNewLine _
                                         & ",DEST_AD_2                                     " & vbNewLine _
                                         & ",DEST_AD_3                                     " & vbNewLine _
                                         & ",DEST_TEL                                      " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                               " & vbNewLine _
                                         & ",CUST_ORD_NO                                   " & vbNewLine _
                                         & ",BUYER_ORD_NO                                  " & vbNewLine _
                                         & ",UNSOCO_NM                                     " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                 " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                 " & vbNewLine _
                                         & ",OUTKA_NO_L                                    " & vbNewLine _
                                         & ",URIG_NM                                       " & vbNewLine _
                                         & ",CUST_NM_L                                     " & vbNewLine _
                                         & "--(2012.06.15) 要望番号1155  --- START ---     " & vbNewLine _
                                         & ",CUST_NM_M                                     " & vbNewLine _
                                         & "--(2012.06.15) 要望番号1155  ---  END  ---     " & vbNewLine _
                                         & ",CUST_AD_1                                     " & vbNewLine _
                                         & ",CUST_AD_2                                     " & vbNewLine _
                                         & ",CUST_TEL                                      " & vbNewLine _
                                         & ",DENPYO_NM                                     " & vbNewLine _
                                         & ",NRS_BR_NM                                     " & vbNewLine _
                                         & ",NRS_BR_AD1                                    " & vbNewLine _
                                         & ",NRS_BR_AD2                                    " & vbNewLine _
                                         & ",NRS_BR_TEL                                    " & vbNewLine _
                                         & ",NRS_BR_FAX                                    " & vbNewLine _
                                         & ",PIC                                           " & vbNewLine _
                                         & ",OUTKA_NO_M                                    " & vbNewLine _
                                         & ",INKA_DATE                                     " & vbNewLine _
                                         & ",REMARK_OUT                                    " & vbNewLine _
                                         & ",GOODS_NM_1                                    " & vbNewLine _
                                         & ",LOT_NO                                        " & vbNewLine _
                                         & ",SERIAL_NO                                     " & vbNewLine _
                                         & ",IRIME                                         " & vbNewLine _
                                         & ",PKG_NB                                        " & vbNewLine _
                                         & ",PKG_UT                                        " & vbNewLine _
                                         & ",PKG_UT_NM                                     " & vbNewLine _
                                         & ",IRIME_UT                                      " & vbNewLine _
                                         & ",WT                                            " & vbNewLine _
                                         & ",UNSO_WT                                       " & vbNewLine _
                                         & ",TOU_NO                                        " & vbNewLine _
                                         & ",SITU_NO                                       " & vbNewLine _
                                         & ",ZONE_CD                                       " & vbNewLine _
                                         & ",LOCA                                          " & vbNewLine _
                                         & ",GOODS_CD_CUST                                 " & vbNewLine _
                                         & ",REMARK_M                                      " & vbNewLine _
                                         & ",CUST_NM_S                                     " & vbNewLine _
                                         & ",CUST_NM_S_H                                   " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                               " & vbNewLine _
                                         & ",OUTKA_REMARK                                  " & vbNewLine _
                                         & ",HAISOU_REMARK                                 " & vbNewLine _
                                         & ",NHS_REMARK                                    " & vbNewLine _
                                         & ",KYORI                                         " & vbNewLine _
                                         & ",OUTKA_PKG_NB_L                                " & vbNewLine _
                                         & ",SOKO_NM                                       " & vbNewLine _
                                         & ",BUSHO_NM                                      " & vbNewLine _
                                         & ",NRS_BR_NM_NICHI                               " & vbNewLine _
                                         & ",ALCTD_KB                                      " & vbNewLine _
                                         & ",PTN_FLAG                                      " & vbNewLine _
                                         & ",CUST_CD_L                                     " & vbNewLine _
                                         & ",ORDER_TYPE                                    " & vbNewLine _
                                         & "--【要望番号995】　2012/04/17　元着払い区分　START  " & vbNewLine _
                                         & ",PC_KB_NM                                      " & vbNewLine _
                                         & "--【要望番号995】　2012/04/17　元着払い区分　 END   " & vbNewLine _
                                         & "--【要望番号1017】 2012/04/24　注文番号明細  START   " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                    " & vbNewLine _
                                         & "--【要望番号1017】　2012/04/24 注文番号明細　 END    " & vbNewLine _
                                         & "--日医工対策開始                                    " & vbNewLine _
                                         & ",TORI_KB                                         " & vbNewLine _
                                         & "--★追加開始2012/05/11                           " & vbNewLine _
                                         & ",SHIP_CD_L                                       " & vbNewLine _
                                         & "--★追加終了2012/05/11                           " & vbNewLine _
                                         & "--★削除開始2012/05/11                           " & vbNewLine _
                                         & "--,CHOKUSO_NM1                                   " & vbNewLine _
                                         & "--,CHOKUSO_CD                                    " & vbNewLine _
                                         & "--,HAKKO_DATE                                    " & vbNewLine _
                                         & "--★削除終了2012/05/11                           " & vbNewLine _
                                         & ",SET_NAIYO_4                                     " & vbNewLine _
                                         & ",SET_NAIYO_5                                     " & vbNewLine _
                                         & ",SET_NAIYO_6                                     " & vbNewLine _
                                         & ",GOODS_NM_2                                      " & vbNewLine _
                                         & ",KICHO_KB                                        " & vbNewLine _
                                         & ",SEI_YURAI_KB                                    " & vbNewLine _
                                         & ",REMARK_UPPER                                    " & vbNewLine _
                                         & ",REMARK_LOWER                                    " & vbNewLine _
                                         & ",GOODS_SYUBETU                                   " & vbNewLine _
                                         & ",YUKOU_KIGEN                                     " & vbNewLine _
                                         & ",GOODS_NM_3 --2012/05/23                         " & vbNewLine _
                                         & ",EDISHIP_CD --2012/05/23                         " & vbNewLine _
                                         & ",EDIDEST_CD --2012/05/23                         " & vbNewLine _
                                         & "--日医工対策終了                                 " & vbNewLine _
                                         & "--(2012.06.01)日本ﾌｧｲﾝ対応 -- START --           " & vbNewLine _
                                         & ",SET_NAIYO_FROM1                                 " & vbNewLine _
                                         & ",SET_NAIYO_FROM2                                 " & vbNewLine _
                                         & ",SET_NAIYO_FROM3                                 " & vbNewLine _
                                         & "--(2012.06.01)日本ﾌｧｲﾝ対応 --  END  --           " & vbNewLine _
                                         & "--【要望番号1122】埼玉対応 --- START ---         " & vbNewLine _
                                         & ",CUST_AD_3                                       " & vbNewLine _
                                         & ",DEST_REMARK                                     " & vbNewLine _
                                         & ",DEST_SALES_CD                                   " & vbNewLine _
                                         & ",DEST_SALES_NM                                   " & vbNewLine _
                                         & ",DEST_SALES_AD_1                                 " & vbNewLine _
                                         & ",DEST_SALES_AD_2                                 " & vbNewLine _
                                         & ",DEST_SALES_AD_3                                 " & vbNewLine _
                                         & ",DEST_SALES_TEL                                  " & vbNewLine _
                                         & "--【要望番号1122】埼玉対応 ---  END  ---         " & vbNewLine _
                                         & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731" & vbNewLine _
                                         & ",SAGYO_RYAK_1                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_2                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_3                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_4                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_5                                    " & vbNewLine _
                                         & ",DEST_SAGYO_RYAK_1                               " & vbNewLine _
                                         & ",DEST_SAGYO_RYAK_2                               " & vbNewLine _
                                         & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731" & vbNewLine _
                                         & "--群馬篠崎運送向け自由項目対応　開始　           " & vbNewLine _
                                         & ",SZ01_YUSO  --篠崎                               " & vbNewLine _
                                         & ",SZ01_UNSO  --篠崎                               " & vbNewLine _
                                         & "--群馬篠崎運送向け自由項目対応　終了　           " & vbNewLine _
                                         & "--2015/1/14 LMC757                               " & vbNewLine _
                                         & ",HAISOU_REMARK_MST                               " & vbNewLine _
                                         & ",GOODS_NM_CUST                                   " & vbNewLine _
                                         & "--2015/1/14 LMC757                               " & vbNewLine _
                                         & "--2016/1/20 LMC815 LMC815 add start              " & vbNewLine _
                                         & ",GODO_GOODS_CODE                                 " & vbNewLine _
                                         & "--2016/1/20 LMC815 LMC815 add end                " & vbNewLine _
                                         & "--  ★★★SQL_ORDER_BY★★★                     " & vbNewLine _
                                         & "     ORDER BY                                    " & vbNewLine _
                                         & "     NRS_BR_CD                                   " & vbNewLine _
                                         & "    ,OUTKA_NO_L                                  " & vbNewLine _
                                         & "    ,PRINT_SORT                                  " & vbNewLine _
                                         & "    ,OUTKA_NO_M                                  " & vbNewLine _
                                         & "    ,OUTKA_NO_S                                  " & vbNewLine



    ''' <summary>
    '''  印刷データ抽出用2(大阪物流センター用：作業項目有り)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_737_OSK As String = "SELECT                                                            " & vbNewLine _
                                            & " RPT_ID                                                            " & vbNewLine _
                                            & ",NRS_BR_CD                                                         " & vbNewLine _
                                            & ",PRINT_SORT                                                        " & vbNewLine _
                                            & ",DEST_CD                                                           " & vbNewLine _
                                            & ",DEST_NM                                                           " & vbNewLine _
                                            & ",DEST_AD_1                                                         " & vbNewLine _
                                            & ",DEST_AD_2                                                         " & vbNewLine _
                                            & ",DEST_AD_3                                                         " & vbNewLine _
                                            & ",DEST_TEL                                                          " & vbNewLine _
                                            & ",OUTKA_PLAN_DATE                                                   " & vbNewLine _
                                            & ",CUST_ORD_NO                                                       " & vbNewLine _
                                            & ",BUYER_ORD_NO                                                      " & vbNewLine _
                                            & ",UNSOCO_NM                                                         " & vbNewLine _
                                            & ",ARR_PLAN_DATE                                                     " & vbNewLine _
                                            & ",ARR_PLAN_TIME                                                     " & vbNewLine _
                                            & ",OUTKA_NO_L                                                        " & vbNewLine _
                                            & ",URIG_NM                                                           " & vbNewLine _
                                            & ",CUST_NM_L                                                         " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 -- START --                            " & vbNewLine _
                                            & ",CUST_NM_M                                                         " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 --  END  --                            " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_1      " & vbNewLine _
                                            & "      ELSE CUST_AD_1 END CUST_AD_1                                 " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_2      " & vbNewLine _
                                            & "      ELSE CUST_AD_2 END CUST_AD_2                                 " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_TEL       " & vbNewLine _
                                            & "      ELSE CUST_TEL END CUST_TEL                                 " & vbNewLine _
                                            & ",DENPYO_NM                                                         " & vbNewLine _
                                            & ",NRS_BR_NM                                                         " & vbNewLine _
                                            & ",NRS_BR_AD1                                                        " & vbNewLine _
                                            & ",NRS_BR_AD2                                                        " & vbNewLine _
                                            & ",NRS_BR_TEL                                                        " & vbNewLine _
                                            & ",NRS_BR_FAX                                                        " & vbNewLine _
                                            & ",PIC                                                               " & vbNewLine _
                                            & ",OUTKA_NO_M                                                        " & vbNewLine _
                                            & ",MIN(OUTKA_NO_S)          AS OUTKA_NO_S                            " & vbNewLine _
                                            & ",INKA_DATE                                                         " & vbNewLine _
                                            & ",REMARK_OUT                                                        " & vbNewLine _
                                            & ",GOODS_NM_1                                                        " & vbNewLine _
                                            & ",LOT_NO                                                            " & vbNewLine _
                                            & ",SERIAL_NO                                                         " & vbNewLine _
                                            & ",IRIME                                                             " & vbNewLine _
                                            & ",PKG_NB                                                            " & vbNewLine _
                                            & ",SUM(ALCTD_NB) / PKG_NB   AS KONSU                                 " & vbNewLine _
                                            & ",SUM(ALCTD_NB) % PKG_NB   AS HASU                                  " & vbNewLine _
                                            & ",SUM(ALCTD_NB)            AS ALCTD_NB                              " & vbNewLine _
                                            & ",PKG_UT                                                            " & vbNewLine _
                                            & ",PKG_UT_NM                                                         " & vbNewLine _
                                            & ",SUM(ALCTD_QT)            AS ALCTD_QT                              " & vbNewLine _
                                            & ",IRIME_UT                                                          " & vbNewLine _
                                            & ",WT                       AS WT                                    " & vbNewLine _
                                            & ",UNSO_WT                                                           " & vbNewLine _
                                            & ",TOU_NO                                                            " & vbNewLine _
                                            & ",SITU_NO                                                           " & vbNewLine _
                                            & ",ZONE_CD                                                           " & vbNewLine _
                                            & ",LOCA                                                              " & vbNewLine _
                                            & ",MIN(ZAN_KONSU)           AS ZAN_KONSU                             " & vbNewLine _
                                            & "--(2012.09.06)要望番号1411 残端数不具合対応 -- START --            " & vbNewLine _
                                            & "--,MIN(ZAN_HASU)          AS ZAN_HASU                              " & vbNewLine _
                                            & ",MIN(ALCTD_CAN_NB) % PKG_NB AS ZAN_HASU                            " & vbNewLine _
                                            & "--(2012.09.06)要望番号1411 残端数不具合対応 --  END  --            " & vbNewLine _
                                            & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                            & ",REMARK_M                                                          " & vbNewLine _
                                            & ",CUST_NM_S                                                         " & vbNewLine _
                                            & ",CUST_NM_S_H                                                       " & vbNewLine _
                                            & ",CUST_ORD_NO_DTL                                                   " & vbNewLine _
                                            & ",OUTKA_REMARK                                                      " & vbNewLine _
                                            & ",HAISOU_REMARK                                                     " & vbNewLine _
                                            & ",NHS_REMARK                                                        " & vbNewLine _
                                            & ",KYORI                                                             " & vbNewLine _
                                            & ",OUTKA_PKG_NB_L                                                    " & vbNewLine _
                                            & ",SOKO_NM                                                           " & vbNewLine _
                                            & ",BUSHO_NM                                                          " & vbNewLine _
                                            & ",NRS_BR_NM_NICHI                                                   " & vbNewLine _
                                            & ",MIN(ALCTD_CAN_QT)        AS ALCTD_CAN_QT                          " & vbNewLine _
                                            & ",ALCTD_KB                                                          " & vbNewLine _
                                            & ",PTN_FLAG                                                          " & vbNewLine _
                                            & ",CUST_CD_L                                                         " & vbNewLine _
                                            & ",ORDER_TYPE                AS ORDER_TYPE                           " & vbNewLine _
                                            & ",LT_DATE                                                           " & vbNewLine _
                                            & ",BUYER_ORD_NO_DTL                                                  " & vbNewLine _
                                            & ",GOODS_COND_CD                                                     " & vbNewLine _
                                            & ",GOODS_COND_NM --☆                                                " & vbNewLine _
                                            & ",REMARK_ZAI                                                        " & vbNewLine _
                                            & ",TITLE_SMPL                                                        " & vbNewLine _
                                            & ",MIN(ALCTD_CAN_NB)        AS ALCTD_CAN_NB                          " & vbNewLine _
                                            & ",BACKLOG_QT                                                        " & vbNewLine _
                                            & ",DOKU_NM                                                           " & vbNewLine _
                                            & ",CASE WHEN URIG_NM <> ''                                           " & vbNewLine _
                                            & "           AND URIG_NM IS NOT NULL THEN URIG_NM                    " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                           " & vbNewLine _
                                            & "      WHEN DEST_SALES_NM   <> ''                                   " & vbNewLine _
                                            & "           AND DEST_SALES_NM IS NOT NULL THEN DEST_SALES_NM        " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                           " & vbNewLine _
                                            & "      WHEN DENPYO_NM   <> ''                                       " & vbNewLine _
                                            & "           AND DENPYO_NM IS NOT NULL   THEN DENPYO_NM              " & vbNewLine _
                                            & "      ELSE CUST_NM_L                                               " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                      " & vbNewLine _
                                            & "--【Notes】№728 作業項目1～5を表示(大阪のみ) START                " & vbNewLine _
                                            & ",SAGYO_MEI_REC_NO_1                                                " & vbNewLine _
                                            & ",SAGYO_MEI_CD_1                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_NM_1                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_REC_NO_2                                                " & vbNewLine _
                                            & ",SAGYO_MEI_CD_2                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_NM_2                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_REC_NO_3                                                " & vbNewLine _
                                            & ",SAGYO_MEI_CD_3                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_NM_3                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_REC_NO_4                                                " & vbNewLine _
                                            & ",SAGYO_MEI_CD_4                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_NM_4                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_REC_NO_5                                                " & vbNewLine _
                                            & ",SAGYO_MEI_CD_5                                                    " & vbNewLine _
                                            & ",SAGYO_MEI_NM_5                                                    " & vbNewLine _
                                            & "--【Notes】№728 作業項目1～5を表示(大阪のみ) END                  " & vbNewLine _
                                            & "--2012/03/12対応開始                                               " & vbNewLine _
                                            & ",TOU_SITU_NM                                                       " & vbNewLine _
                                            & ",JISYATASYA_KB                                                     " & vbNewLine _
                                            & "--2012/03/12対応終了                                               " & vbNewLine _
                                            & "--【要望番号995】　2012/04/20　元着払い区分　START                 " & vbNewLine _
                                            & ",PC_KB_NM                                                          " & vbNewLine _
                                            & "--【要望番号995】　2012/04/20　元着払い区分　 END                  " & vbNewLine _
                                            & "--日医工対策開始                                                   " & vbNewLine _
                                            & ",TORI_KB                                                           " & vbNewLine _
                                            & "--★追加開始2012/05/11                                             " & vbNewLine _
                                            & ",SHIP_CD_L                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                             " & vbNewLine _
                                            & "--★削除開始2012/05/11                                             " & vbNewLine _
                                            & "--,CHOKUSO_NM1                                                     " & vbNewLine _
                                            & "--,CHOKUSO_CD                                                      " & vbNewLine _
                                            & "--,HAKKO_DATE                                                      " & vbNewLine _
                                            & "--★削除終了2012/05/11                                             " & vbNewLine _
                                            & ",SET_NAIYO_4                                                       " & vbNewLine _
                                            & ",SET_NAIYO_5                                                       " & vbNewLine _
                                            & ",SET_NAIYO_6                                                       " & vbNewLine _
                                            & ",GOODS_NM_2                                                        " & vbNewLine _
                                            & ",KICHO_KB                                                          " & vbNewLine _
                                            & ",SEI_YURAI_KB                                                      " & vbNewLine _
                                            & ",REMARK_UPPER                                                      " & vbNewLine _
                                            & ",REMARK_LOWER                                                      " & vbNewLine _
                                            & ",GOODS_SYUBETU                                                     " & vbNewLine _
                                            & ",YUKOU_KIGEN                                                       " & vbNewLine _
                                            & ",GOODS_NM_3 --2012/05/23                                           " & vbNewLine _
                                            & ",EDISHIP_CD --2012/05/23                                           " & vbNewLine _
                                            & ",EDIDEST_CD --2012/05/23                                           " & vbNewLine _
                                            & "--日医工対策終了                                                   " & vbNewLine _
                                            & "--(2012.06.01)日本ファイン対応 --- START ---                       " & vbNewLine _
                                            & ",SET_NAIYO_FROM1                                                   " & vbNewLine _
                                            & ",SET_NAIYO_FROM2                                                   " & vbNewLine _
                                            & ",SET_NAIYO_FROM3                                                   " & vbNewLine _
                                            & "--(2012.06.01)日本ファイン対応 ---  END  ---                       " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                           " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_3      " & vbNewLine _
                                            & "      ELSE CUST_AD_3 END CUST_AD_3                                 " & vbNewLine _
                                            & ",DEST_REMARK                                                       " & vbNewLine _
                                            & ",DEST_SALES_CD                                                     " & vbNewLine _
                                            & ",DEST_SALES_NM                                                     " & vbNewLine _
                                            & ",DEST_SALES_AD_1                                                   " & vbNewLine _
                                            & ",DEST_SALES_AD_2                                                   " & vbNewLine _
                                            & ",DEST_SALES_AD_3                                                   " & vbNewLine _
                                            & ",DEST_SALES_TEL                                                    " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                           " & vbNewLine _
                                            & "--Notes1331  群馬対応  ---START---                                 " & vbNewLine _
                                            & ",SAGYO_RYAK_1                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_2                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_3                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_4                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_5                                                      " & vbNewLine _
                                            & ",DEST_SAGYO_RYAK_1                                                 " & vbNewLine _
                                            & ",DEST_SAGYO_RYAK_2                                                 " & vbNewLine _
                                            & ",SZ01_YUSO                                                         " & vbNewLine _
                                            & ",SZ01_UNSO                                                         " & vbNewLine _
                                            & "--Notes1331  群馬対応  ---END---                                   " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                 " & vbNewLine _
                                            & ",HAISOU_REMARK_MST                                                     " & vbNewLine _
                                            & ",GOODS_NM_CUST                                                     " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                 " & vbNewLine _
                                            & "FROM                                                               " & vbNewLine _
                                            & "(                                                                  " & vbNewLine _
                                             & "--出荷テーブル                                                                                                           " & vbNewLine _
                                             & "SELECT                                                                                                                         " & vbNewLine _
                                             & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                              " & vbNewLine _
                                             & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                              " & vbNewLine _
                                             & "      ELSE MR3.RPT_ID                                                                                                          " & vbNewLine _
                                             & " END              AS RPT_ID                                                                                                    " & vbNewLine _
                                             & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                                                 " & vbNewLine _
                                             & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                                                " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                                                     " & vbNewLine _
                                             & "      ELSE OUTL.DEST_CD                                                                                                        " & vbNewLine _
                                             & " END              AS DEST_CD                                                                                                   " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                     " & vbNewLine _
                                             & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
                                             & " END              AS DEST_NM                                                                                                   " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
                                             & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
                                             & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
                                             & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
                                             & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
                                             & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                                 " & vbNewLine _
                                             & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                                  " & vbNewLine _
                                             & ",OUTL.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE                                                                                     " & vbNewLine _
                                             & ",OUTL.CUST_ORD_NO       AS CUST_ORD_NO                                                                                         " & vbNewLine _
                                             & ",OUTL.BUYER_ORD_NO      AS BUYER_ORD_NO                                                                                        " & vbNewLine _
                                             & ",MUCO.UNSOCO_NM         AS UNSOCO_NM                                                                                           " & vbNewLine _
                                             & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                                       " & vbNewLine _
                                             & ",KBN1.KBN_NM1           AS ARR_PLAN_TIME                                                                                       " & vbNewLine _
                                             & ",OUTL.OUTKA_NO_L        AS OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & ",MDOUTU.DEST_NM         AS URIG_NM                                                                                             " & vbNewLine _
                                             & ",MC.CUST_NM_L           AS CUST_NM_L                                                                                           " & vbNewLine _
                                             & "--(2012.06.15) 要望番号1155 -- START --                                                                                        " & vbNewLine _
                                             & ",MC.CUST_NM_M           AS CUST_NM_M                                                                                           " & vbNewLine _
                                             & "--(2012.06.15) 要望番号1155 --  END  --                                                                                        " & vbNewLine _
                                             & ",MC.AD_1                AS CUST_AD_1                                                                                           " & vbNewLine _
                                             & ",MC.AD_2                AS CUST_AD_2                                                                                           " & vbNewLine _
                                             & ",MC.TEL                 AS CUST_TEL                                                                                            " & vbNewLine _
                                             & ",MC.DENPYO_NM           AS DENPYO_NM                                                                                            " & vbNewLine _
                                             & ",MNRS.NRS_BR_NM         AS NRS_BR_NM                                                                                           " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.AD_1                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.AD_1                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_AD1                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.AD_2                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.AD_2                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_AD2                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.TEL                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.TEL                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_TEL                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.FAX                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.FAX                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_FAX                                                                                                 " & vbNewLine _
                                             & ",MUSER.USER_NM          AS PIC                                                                                                 " & vbNewLine _
                                             & ",OUTM.OUTKA_NO_M        AS OUTKA_NO_M                                                                                          " & vbNewLine _
                                             & ",OUTS.OUTKA_NO_S        AS OUTKA_NO_S                                                                                          " & vbNewLine _
                                             & ",CASE WHEN MGD.SET_NAIYO = '1' THEN ''                                                                                         " & vbNewLine _
                                             & "      WHEN INL.INKA_STATE_KB < '50' THEN INL.INKA_DATE                                                                         " & vbNewLine _
                                             & "      WHEN INL.INKA_STATE_KB >= '50' THEN ZAI.INKO_DATE                                                                        " & vbNewLine _
                                             & "      ELSE ''                                                                                                                  " & vbNewLine _
                                             & " END                    AS INKA_DATE                                                                                           " & vbNewLine _
                                             & ",ZAI.REMARK_OUT         AS REMARK_OUT                                                                                          " & vbNewLine _
                                             & ",CASE WHEN (MDGE.GOODS_NM IS NOT NULL AND MDGE.GOODS_NM <> '' AND EDIL.OUTKA_CTL_NO IS NOT NULL) THEN MDGE.GOODS_NM            " & vbNewLine _
                                             & "      WHEN (MDGL.GOODS_NM IS NOT NULL AND MDGL.GOODS_NM <> '' AND EDIL.OUTKA_CTL_NO IS NULL) THEN MDGL.GOODS_NM                " & vbNewLine _
                                             & "      ELSE MG.GOODS_NM_1                                                                                                       " & vbNewLine _
                                             & " END                    AS GOODS_NM_1                                                                                          " & vbNewLine _
                                             & ",OUTS.LOT_NO            AS LOT_NO                                                                                              " & vbNewLine _
                                             & ",OUTS.SERIAL_NO         AS SERIAL_NO                                                                                           " & vbNewLine _
                                             & ",OUTS.IRIME             AS IRIME                                                                                               " & vbNewLine _
                                             & ",MG.PKG_NB              AS PKG_NB                                                                                              " & vbNewLine _
                                             & ",OUTS.ALCTD_NB / MG.PKG_NB     AS KONSU                                                                                        " & vbNewLine _
                                             & ",OUTS.ALCTD_NB % MG.PKG_NB     AS HASU                                                                                         " & vbNewLine _
                                             & ",OUTS.ALCTD_NB          AS ALCTD_NB                                                                                            " & vbNewLine _
                                             & ",MG.PKG_UT              AS PKG_UT                                                                                              " & vbNewLine _
                                             & ",KBN5.KBN_NM1           AS PKG_UT_NM                                                                                           " & vbNewLine _
                                             & ",OUTS.ALCTD_QT          AS ALCTD_QT                                                                                            " & vbNewLine _
                                             & ",MG.STD_IRIME_UT        AS IRIME_UT                                                                                            " & vbNewLine _
                                             & "--(2012.10.01)要望番号1445 -- START --                                                                                         " & vbNewLine _
                                             & ",UL.UNSO_WT             AS WT                                                                                                  " & vbNewLine _
                                             & "--,OUTS.BETU_WT * OUTS.ALCTD_NB  AS WT                                                                                         " & vbNewLine _
                                             & "--(2012.10.01)要望番号1445 --  END  --                                                                                         " & vbNewLine _
                                             & ",0                      AS UNSO_WT                                                                                             " & vbNewLine _
                                             & ",OUTS.TOU_NO            AS TOU_NO                                                                                              " & vbNewLine _
                                             & ",OUTS.SITU_NO           AS SITU_NO                                                                                             " & vbNewLine _
                                             & ",RTRIM(OUTS.ZONE_CD)     AS ZONE_CD                                                                                             " & vbNewLine _
                                             & ",OUTS.LOCA              AS LOCA                                                                                                " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB / MG.PKG_NB   AS ZAN_KONSU                                                                                  " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB % MG.PKG_NB   AS ZAN_HASU                                                                                   " & vbNewLine _
                                             & ",MG.GOODS_CD_CUST       AS GOODS_CD_CUST                                                                                       " & vbNewLine _
                                             & ",OUTM.REMARK            AS REMARK_M                                                                                            " & vbNewLine _
                                             & "--★★★変更開始--------------------------------                                                                                     " & vbNewLine _
                                             & ",''   AS CUST_NM_S                                                                                           " & vbNewLine _
                                             & ",''   AS CUST_NM_S_H                                                                                           " & vbNewLine _
                                             & "--★★★変更終了--------------------------------                                                                                     " & vbNewLine _
                                             & ",OUTM.CUST_ORD_NO_DTL   AS CUST_ORD_NO_DTL                                                                                     " & vbNewLine _
                                             & ",OUTL.REMARK            AS OUTKA_REMARK                                                                                        " & vbNewLine _
                                             & ",UL.REMARK              AS HAISOU_REMARK                                                                                       " & vbNewLine _
                                             & ",OUTL.NHS_REMARK        AS NHS_REMARK                                                                                          " & vbNewLine _
                                             & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  --- START ---                                             " & vbNewLine _
                                             & "--,CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                                        " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD < MSO.JIS_CD) THEN MKY3.KYORI                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD < EDIL.DEST_JIS_CD) THEN MKY4.KYORI                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS < MSO.JIS_CD) THEN MKY1.KYORI                             " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD < MDOUT.JIS) THEN MKY2.KYORI                             " & vbNewLine _
                                             & "--      ELSE 0                                                                                                                 " & vbNewLine _
                                             & "-- END                  AS KYORI                                                                                               " & vbNewLine _
                                             & ",CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                                          " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD <= MSO.JIS_CD) THEN MKY3.KYORI                        " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD <= EDIL.DEST_JIS_CD) THEN MKY4.KYORI                        " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                                         " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS <= MSO.JIS_CD) THEN MKY1.KYORI                              " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD <= MDOUT.JIS) THEN MKY2.KYORI                              " & vbNewLine _
                                             & "      ELSE 0                                                                                                                   " & vbNewLine _
                                             & " END                    AS KYORI                                                                                               " & vbNewLine _
                                             & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  ---  END  ---                                             " & vbNewLine _
                                             & ",OUTL.OUTKA_PKG_NB      AS OUTKA_PKG_NB_L                                                                                      " & vbNewLine _
                                             & ",KBN2.KBN_NM1           AS SOKO_NM                                                                                             " & vbNewLine _
                                             & ",KBN3.KBN_NM1           AS BUSHO_NM                                                                                            " & vbNewLine _
                                             & ",KBN4.KBN_NM1           AS NRS_BR_NM_NICHI                                                                                     " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_QT      AS ALCTD_CAN_QT                                                                                        " & vbNewLine _
                                             & ",OUTM.ALCTD_KB          AS ALCTD_KB                                                                                            " & vbNewLine _
                                             & ",'0'                    AS PTN_FLAG                                                                                            " & vbNewLine _
                                             & ",OUTL.CUST_CD_L         AS CUST_CD_L                                                                                           " & vbNewLine _
                                             & ",OUTL.ORDER_TYPE        AS ORDER_TYPE                                                                                          " & vbNewLine _
                                             & ",ZAI.LT_DATE                                                                                                                   " & vbNewLine _
                                             & ",OUTM.BUYER_ORD_NO_DTL AS BUYER_ORD_NO_DTL                                                                                     " & vbNewLine _
                                             & ",ZAI.GOODS_COND_KB_2 AS GOODS_COND_CD                                                                                          " & vbNewLine _
                                             & ",ISNULL(KBN8.KBN_NM1,'') AS GOODS_COND_NM  --☆                                                                                " & vbNewLine _
                                             & ",ZAI.REMARK AS REMARK_ZAI                                                                                                      " & vbNewLine _
                                             & ",KBN6.KBN_NM2 AS TITLE_SMPL                                                                                                    " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB AS ALCTD_CAN_NB                                                                                             " & vbNewLine _
                                             & ",OUTM.BACKLOG_QT                                                                                                               " & vbNewLine _
                                             & ",KBN7.KBN_NM2 AS DOKU_NM                                                                                                       " & vbNewLine _
                                             & "--【Notes】№728 作業項目1～5を表示(大阪のみ) START                                                                            " & vbNewLine _
                                             & ",CASE WHEN MDOUTU.DEST_NM <> ''                                     --★VB追加                                                 " & vbNewLine _
                                             & "           AND MDOUTU.DEST_NM IS NOT NULL THEN MDOUTU.DEST_NM                                                                  " & vbNewLine _
                                             & "      WHEN MC.DENPYO_NM   <> ''                                                                                                " & vbNewLine _
                                             & "           AND MC.DENPYO_NM IS NOT NULL   THEN MC.DENPYO_NM                                                                    " & vbNewLine _
                                             & "      ELSE MC.CUST_NM_L                                                                                                        " & vbNewLine _
                                             & " END                         AS ATSUKAISYA_NM                                                                                  " & vbNewLine _
                                             & "--822改変区間開始(空白挿入化)★Notes822                                                                                        " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_REC_NO_1                                                                                                      " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_CD_1                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_NM_1                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_REC_NO_2                                                                                                      " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_CD_2                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_NM_2                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_REC_NO_3                                                                                                      " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_CD_3                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_NM_3                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_REC_NO_4                                                                                                      " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_CD_4                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_NM_4                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_REC_NO_5                                                                                                      " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_CD_5                                                                                                          " & vbNewLine _
                                             & ",'' AS SAGYO_MEI_NM_5                                                                                                          " & vbNewLine _
                                             & "--【Notes】№728 作業項目1～5を表示(大阪のみ) END                                                                              " & vbNewLine _
                                             & "--822改変区間終了(空白挿入化)★Notes822                                                                                        " & vbNewLine _
                                             & "--2012/03/12対応開始                                                                                                           " & vbNewLine _
                                             & ",MTS.TOU_SITU_NM           AS TOU_SITU_NM                                                                                      " & vbNewLine _
                                             & ",MTS.JISYATASYA_KB         AS JISYATASYA_KB                                                                                    " & vbNewLine _
                                             & "--2012/03/12対応終了                                                                                                           " & vbNewLine _
                                             & "--出荷L                                                                                                                        " & vbNewLine _
                                             & "--【要望番号995】　2012/04/20　元着払い区分　START                                                                             " & vbNewLine _
                                             & ",KBN9.KBN_NM2 AS PC_KB_NM                                                                                                      " & vbNewLine _
                                             & "--【要望番号995】　2012/04/20　元着払い区分　 END                                                                              " & vbNewLine _
                                             & "--日医工対策開始★                                                                                                             " & vbNewLine _
                                             & ",''                       AS TORI_KB                                                                                           " & vbNewLine _
                                             & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                             & ",''                       AS SHIP_CD_L                                                                                         " & vbNewLine _
                                             & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                             & "--★削除開始2012/05/11                                                                                                         " & vbNewLine _
                                             & "--,''                       AS CHOKUSO_NM1                                                                                     " & vbNewLine _
                                             & "--,''                       AS CHOKUSO_CD                                                                                      " & vbNewLine _
                                             & "--,''                       AS HAKKO_DATE                                                                                      " & vbNewLine _
                                             & "--★削除終了2012/05/11                                                                                                         " & vbNewLine _
                                             & ",''                       AS SET_NAIYO_4                                                                                       " & vbNewLine _
                                             & ",''                       AS SET_NAIYO_5                                                                                       " & vbNewLine _
                                             & ",''                       AS SET_NAIYO_6                                                                                       " & vbNewLine _
                                             & ",''                       AS GOODS_NM_2                                                                                        " & vbNewLine _
                                             & ",''                       AS KICHO_KB                                                                                          " & vbNewLine _
                                             & ",''                       AS SEI_YURAI_KB                                                                                      " & vbNewLine _
                                             & ",''                       AS REMARK_UPPER                                                                                      " & vbNewLine _
                                             & ",''                       AS REMARK_LOWER                                                                                      " & vbNewLine _
                                             & ",''                       AS GOODS_SYUBETU                                                                                     " & vbNewLine _
                                             & ",''                       AS YUKOU_KIGEN                                                                                       " & vbNewLine _
                                             & ",''                       AS GOODS_NM_3         --2012/05/23                                                                   " & vbNewLine _
                                             & ",''                       AS EDISHIP_CD         --2012/05/23                                                                   " & vbNewLine _
                                             & ",''                       AS EDIDEST_CD         --2012/05/23                                                                   " & vbNewLine _
                                             & "--日医工対策終了★                                                                                                             " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応 --- START ---　                                                                                    " & vbNewLine _
                                             & ",''                       AS SET_NAIYO_FROM1                                                                                   " & vbNewLine _
                                             & ",''                       AS SET_NAIYO_FROM2                                                                                   " & vbNewLine _
                                             & ",''                       AS SET_NAIYO_FROM3                                                                                   " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応 ---  END  ---　                                                                                    " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                             & ",''                       AS CUST_AD_3                                                                                         " & vbNewLine _
                                             & ",''                       AS DEST_REMARK                                                                                       " & vbNewLine _
                                             & ",''                       AS DEST_SALES_CD                                                                                     " & vbNewLine _
                                             & ",''                       AS DEST_SALES_NM                                                                                     " & vbNewLine _
                                             & ",''                       AS DEST_SALES_AD_1                                                                                   " & vbNewLine _
                                             & ",''                       AS DEST_SALES_AD_2                                                                                   " & vbNewLine _
                                             & ",''                       AS DEST_SALES_AD_3                                                                                   " & vbNewLine _
                                             & ",''                       AS DEST_SALES_TEL                                                                                    " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                             & "--Notes1331  群馬対応  ---START---                                                                                             " & vbNewLine _
                                             & ",''                       AS SAGYO_RYAK_1                                                                                      " & vbNewLine _
                                             & ",''                       AS SAGYO_RYAK_2                                                                                      " & vbNewLine _
                                             & ",''                       AS SAGYO_RYAK_3                                                                                      " & vbNewLine _
                                             & ",''                       AS SAGYO_RYAK_4                                                                                      " & vbNewLine _
                                             & ",''                       AS SAGYO_RYAK_5                                                                                      " & vbNewLine _
                                             & ",''                       AS DEST_SAGYO_RYAK_1                                                                                 " & vbNewLine _
                                             & ",''                       AS DEST_SAGYO_RYAK_2                                                                                 " & vbNewLine _
                                             & ",''                       AS SZ01_YUSO                                                                                         " & vbNewLine _
                                             & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                             & "  , MDOUT.DELI_ATT         AS  HAISOU_REMARK_MST                                                                                    " & vbNewLine _
                                             & "  , MGD43.REMARK           AS  GOODS_NM_CUST                                                                                    " & vbNewLine _
                                             & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                             & ",''                       AS SZ01_UNSO                                                                                         " & vbNewLine _
                                             & "--Notes1331  群馬対応  --- END ---                                                                                             " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "$LM_TRN$..C_OUTKA_L OUTL                                                                                                       " & vbNewLine _
                                             & "--トランザクションテーブル                                                                                                     " & vbNewLine _
                                             & "--出荷M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                                             " & vbNewLine _
                                             & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND OUTM.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                             & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                             & "       (SELECT                                                                                                                 " & vbNewLine _
                                             & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                             & "           ,OUTKA_NO_L                                                                                                         " & vbNewLine _
                                             & "           ,MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                                                                     " & vbNewLine _
                                             & "       FROM $LM_TRN$..C_OUTKA_M WHERE SYS_DEL_FLG ='0'                                                                         " & vbNewLine _
                                             & "       GROUP BY NRS_BR_CD,OUTKA_NO_L) OUTM_MIN                                                                                 " & vbNewLine _
                                             & "       ON OUTM_MIN.NRS_BR_CD        = OUTL.NRS_BR_CD                                                                           " & vbNewLine _
                                             & "       AND OUTM_MIN.OUTKA_NO_L      = OUTL.OUTKA_NO_L                                                                          " & vbNewLine _
                                             & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM2                                                                                            " & vbNewLine _
                                             & "ON  OUTM2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND OUTM2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                         " & vbNewLine _
                                             & "AND OUTM2.OUTKA_NO_M = OUTM_MIN.OUTKA_NO_M                                                                                     " & vbNewLine _
                                             & "AND OUTM2.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                             & "--出荷S                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                                             " & vbNewLine _
                                             & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                                          " & vbNewLine _
                                             & "AND OUTS.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--出荷EDIL                                                                                                                     " & vbNewLine _
                                             & "--(2012.09.06)要望番号1412対応  --- START ---                                                                                  " & vbNewLine _
                                             & "--LEFT JOIN                                                                                                                    " & vbNewLine _
                                             & "--(                                                                                                                            " & vbNewLine _
                                             & "--SELECT                                                                                                                       " & vbNewLine _
                                             & "--NRS_BR_CD                                                                                                                    " & vbNewLine _
                                             & "--,OUTKA_CTL_NO                                                                                                                " & vbNewLine _
                                             & "--,CUST_CD_L                                                                                                                   " & vbNewLine _
                                             & "--,SHIP_NM_L                                                                                                                   " & vbNewLine _
                                             & "--,MIN(DEST_CD)     AS DEST_CD                                                                                                 " & vbNewLine _
                                             & "--,MIN(DEST_NM)     AS DEST_NM                                                                                                 " & vbNewLine _
                                             & "--,MIN(DEST_AD_1)   AS DEST_AD_1                                                                                               " & vbNewLine _
                                             & "--,MIN(DEST_AD_2)   AS DEST_AD_2                                                                                               " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出 -- START --                                                             " & vbNewLine _
                                             & "----,DEST_JIS_CD                                                                                                               " & vbNewLine _
                                             & "--,MIN(DEST_JIS_CD) AS DEST_JIS_CD                                                                                             " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出 --  END  --                                                             " & vbNewLine _
                                             & "--,SYS_DEL_FLG                                                                                                                 " & vbNewLine _
                                             & "--FROM                                                                                                                         " & vbNewLine _
                                             & "--$LM_TRN$..H_OUTKAEDI_L                                                                                                       " & vbNewLine _
                                             & "--GROUP BY                                                                                                                     " & vbNewLine _
                                             & "--NRS_BR_CD                                                                                                                    " & vbNewLine _
                                             & "--,OUTKA_CTL_NO                                                                                                                " & vbNewLine _
                                             & "--,CUST_CD_L                                                                                                                   " & vbNewLine _
                                             & "--,SHIP_NM_L                                                                                                                   " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出のためコメント -- START --                                               " & vbNewLine _
                                             & "----,DEST_JIS_CD                                                                                                               " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出のためコメント --  END  --                                               " & vbNewLine _
                                             & "--,SYS_DEL_FLG                                                                                                                 " & vbNewLine _
                                             & "--) EDIL                                                                                                                       " & vbNewLine _
                                             & "--ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                          " & vbNewLine _
                                             & "--AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                                                                      " & vbNewLine _
                                             & "--AND EDIL.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                             & "--下記の内容に変更                                                                                                             " & vbNewLine _
                                             & " LEFT JOIN (                                                                                                                   " & vbNewLine _
                                             & "            SELECT                                                                                                             " & vbNewLine _
                                             & "                   NRS_BR_CD                                                                                                   " & vbNewLine _
                                             & "                 , EDI_CTL_NO                                                                                                  " & vbNewLine _
                                             & "                 , OUTKA_CTL_NO                                                                                                " & vbNewLine _
                                             & "             FROM (                                                                                                            " & vbNewLine _
                                             & "                    SELECT                                                                                                     " & vbNewLine _
                                             & "                           EDIOUTL.NRS_BR_CD                                                                                   " & vbNewLine _
                                             & "                         , EDIOUTL.EDI_CTL_NO                                                                                  " & vbNewLine _
                                             & "                         , EDIOUTL.OUTKA_CTL_NO                                                                                " & vbNewLine _
                                             & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                                                          " & vbNewLine _
                                             & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                                                              , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
                                             & "                                                       ORDER BY EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                                                              , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
                                             & "                                                  )                                                                            " & vbNewLine _
                                             & "                           END AS IDX                                                                                          " & vbNewLine _
                                             & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                                                       " & vbNewLine _
                                             & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                                                           " & vbNewLine _
                                             & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                                                                    " & vbNewLine _
                                             & "                      AND EDIOUTL.OUTKA_CTL_NO = @KANRI_NO_L                                                                   " & vbNewLine _
                                             & "                  ) EBASE                                                                                                      " & vbNewLine _
                                             & "            WHERE EBASE.IDX = 1                                                                                                " & vbNewLine _
                                             & "            ) TOPEDI                                                                                                           " & vbNewLine _
                                             & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                                                                " & vbNewLine _
                                             & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                                                               " & vbNewLine _
                                             & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                                                         " & vbNewLine _
                                             & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                                                                  " & vbNewLine _
                                             & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                                                                 " & vbNewLine _
                                             & "--(2012.09.06)要望番号1412対応  ---  END  ---                                                                                  " & vbNewLine _
                                             & "--入荷L                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                                                                               " & vbNewLine _
                                             & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                                                                             " & vbNewLine _
                                             & "AND INL.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--入荷S                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                                               " & vbNewLine _
                                             & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                                                                             " & vbNewLine _
                                             & "AND INS.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--運送L                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                                " & vbNewLine _
                                             & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND UL.MOTO_DATA_KB = '20'                                                                                                     " & vbNewLine _
                                             & "AND UL.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                             & "--在庫テーブル                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                                              " & vbNewLine _
                                             & "ON  ZAI.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                                                           " & vbNewLine _
                                             & "AND ZAI.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--マスタテーブル                                                                                                               " & vbNewLine _
                                             & "--営業所M                                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_NRS_BR MNRS                                                                                              " & vbNewLine _
                                             & "ON MNRS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "--商品M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                 " & vbNewLine _
                                             & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--商品M(MIN)                                                                                                                   " & vbNewLine _
                                             & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                                  " & vbNewLine _
                                             & "ON M_GOODS_MIN.NRS_BR_CD      = OUTL.NRS_BR_CD                                                                                 " & vbNewLine _
                                             & "AND M_GOODS_MIN.GOODS_CD_NRS   = OUTM2.GOODS_CD_NRS                                                                            " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                             & "--荷主M(商品M経由)                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                                  " & vbNewLine _
                                             & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                                              " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                             & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                             & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                             & "--届先M(届先取得)(出荷L参照)                                                                                                   " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                                                                               " & vbNewLine _
                                             & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                                                                               " & vbNewLine _
                                             & "--届先M(売上先取得)(出荷L参照)                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                                                                              " & vbNewLine _
                                             & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                          " & vbNewLine _
                                             & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                                                                          " & vbNewLine _
                                             & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                                                                            " & vbNewLine _
                                             & "--届先M(届先取得)(出荷EDIL参照)                                                                                                " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDEDI                                                                                               " & vbNewLine _
                                             & "ON  MDEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MDEDI.CUST_CD_L = EDIL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MDEDI.DEST_CD = EDIL.DEST_CD                                                                                               " & vbNewLine _
                                             & "--運送会社M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                                                                              " & vbNewLine _
                                             & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                                                                                " & vbNewLine _
                                             & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                                                                          " & vbNewLine _
                                             & "--倉庫M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                                                                 " & vbNewLine _
                                             & "ON  MSO.WH_CD = OUTL.WH_CD                                                                                                     " & vbNewLine _
                                             & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                                                                                " & vbNewLine _
                                             & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                                                                        " & vbNewLine _
                                             & "AND UL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                                                                          " & vbNewLine _
                                             & "--2014/5/16End s.kobayashi NotesNo.2183                                                                                       " & vbNewLine _
                                             & "--距離程M(M_DEST.JIS < M_SOKO.JIS_CD)(出荷L参照)                                                                               " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY1                                                                                               " & vbNewLine _
                                             & "ON  MKY1.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY1.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY1.ORIG_JIS_CD = MDOUT.JIS                                                                                               " & vbNewLine _
                                             & "AND MKY1.DEST_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "--距離程M(M_SOKO.JIS_CD < M_DEST.JIS)(出荷L参照)                                                                               " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY2                                                                                               " & vbNewLine _
                                             & "ON  MKY2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY2.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY2.ORIG_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "AND MKY2.DEST_JIS_CD = MDOUT.JIS                                                                                               " & vbNewLine _
                                             & "--距離程M(H_OUTKAEDI_L.DEST_JIS_CD < M_SOKO.JIS_CD)(出荷EDIL参照)                                                              " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY3                                                                                               " & vbNewLine _
                                             & "ON  MKY3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY3.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY3.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY3.ORIG_JIS_CD = EDIL.DEST_JIS_CD                                                                                        " & vbNewLine _
                                             & "AND MKY3.DEST_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "--距離程M(M_SOKO.JIS_CD < H_OUTKAEDI_L.DEST_JIS_CD)(出荷EDIL参照)                                                              " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY4                                                                                               " & vbNewLine _
                                             & "ON  MKY4.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY4.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY4.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY4.ORIG_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "AND MKY4.DEST_JIS_CD = EDIL.DEST_JIS_CD                                                                                        " & vbNewLine _
                                             & "--届先商品M(出荷L参照)                                                                                                         " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DESTGOODS MDGL                                                                                           " & vbNewLine _
                                             & "ON  MDGL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND MDGL.CUST_CD_L = OUTL.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND MDGL.CUST_CD_M = OUTL.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND MDGL.CD = OUTL.DEST_CD                                                                                                     " & vbNewLine _
                                             & "AND MDGL.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "AND MDGL.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--届先商品M(出荷EDI参照)                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DESTGOODS MDGE                                                                                           " & vbNewLine _
                                             & "ON  MDGE.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND MDGE.CUST_CD_L = OUTL.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND MDGE.CUST_CD_M = OUTL.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND MDGE.CD = EDIL.DEST_CD                                                                                                     " & vbNewLine _
                                             & "AND MDGE.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "AND MDGE.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(納入予定区分)                                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                                 " & vbNewLine _
                                             & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                                                 " & vbNewLine _
                                             & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                                                                           " & vbNewLine _
                                             & "AND KBN1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用倉庫名)                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                                                 " & vbNewLine _
                                             & "ON  KBN2.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN2.KBN_CD = '00'                                                                                                         " & vbNewLine _
                                             & "AND KBN2.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用部署名)                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                                                 " & vbNewLine _
                                             & "ON  KBN3.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN3.KBN_CD = '01'                                                                                                         " & vbNewLine _
                                             & "AND KBN3.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用営業署名)                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                                                                                 " & vbNewLine _
                                             & "ON  KBN4.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN4.KBN_CD = '02'                                                                                                         " & vbNewLine _
                                             & "AND KBN4.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(荷姿)                                                                                                                  " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN5                                                                                                 " & vbNewLine _
                                             & "ON  KBN5.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                             & "AND KBN5.KBN_CD = MG.PKG_UT                                                                                                    " & vbNewLine _
                                             & "AND KBN5.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--明細タイトル（小分け）                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN6                                                                                                 " & vbNewLine _
                                             & "ON  KBN6.KBN_GROUP_CD = 'S041'                                                                                                 " & vbNewLine _
                                             & "AND KBN6.KBN_CD = OUTM.ALCTD_KB                                                                                                " & vbNewLine _
                                             & "AND KBN6.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(毒劇区分名称)                                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN7                                                                                                 " & vbNewLine _
                                             & "ON  KBN7.KBN_GROUP_CD = 'G001'                                                                                                 " & vbNewLine _
                                             & "AND KBN7.KBN_CD = MG.DOKU_KB                                                                                                   " & vbNewLine _
                                             & "AND KBN7.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--【要望番号995】　2012/04/20　元着払い区分　START                                                                             " & vbNewLine _
                                             & "--区分M(元着払区分名称)                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN9                                                                                                 " & vbNewLine _
                                             & "ON  KBN9.KBN_GROUP_CD = 'M001'                                                                                                 " & vbNewLine _
                                             & "AND KBN9.KBN_CD = OUTL.PC_KB                                                                                                   " & vbNewLine _
                                             & "AND KBN9.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--【要望番号995】　2012/04/20　元着払い区分　 END                                                                              " & vbNewLine _
                                             & "--出荷Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                             & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                             & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                             & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                             & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                             & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                             & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                             & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                             & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                             & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                             & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                             & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                             & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                             & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                             & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                             & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--ユーザM                                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..S_USER MUSER                                                                                               " & vbNewLine _
                                             & "ON MUSER.USER_CD = OUTL.SYS_ENT_USER                                                                                           " & vbNewLine _
                                             & "AND MUSER.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                             & "--商品明細M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                                      " & vbNewLine _
                                             & "ON  MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                                               " & vbNewLine _
                                             & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                         " & vbNewLine _
                                             & "AND MGD.SUB_KB = '09'                                                                                                          " & vbNewLine _
                                             & "--棟室M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_TOU_SITU MTS                                                                                             " & vbNewLine _
                                             & "ON  OUTL.WH_CD = MTS.WH_CD                                                                                                     " & vbNewLine _
                                             & "AND OUTS.TOU_NO = MTS.TOU_NO                                                                                                   " & vbNewLine _
                                             & "AND OUTS.SITU_NO = MTS.SITU_NO                                                                                                 " & vbNewLine _
                                             & "AND MTS.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--商品状態区分名称表示☆                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN8                                                                                               " & vbNewLine _
                                             & "ON  KBN8.KBN_GROUP_CD = 'S006'                                                                                               " & vbNewLine _
                                             & "AND KBN8.KBN_CD       = ZAI.GOODS_COND_KB_2                                                                                  " & vbNewLine _
                                             & "AND KBN8.SYS_DEL_FLG  = '0'                                                                                                  " & vbNewLine _
                                             & " --商品M LMC757  2015/1/14                                                              " & vbNewLine _
                                             & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD43                                                    " & vbNewLine _
                                             & "	ON  OUTM.NRS_BR_CD = MGD43.NRS_BR_CD                                              " & vbNewLine _
                                             & "	AND OUTM.GOODS_CD_NRS = MGD43.GOODS_CD_NRS                                              " & vbNewLine _
                                             & "	AND OUTL.SHIP_CD_L = MGD43.SET_NAIYO                                              " & vbNewLine _ 
                                             & " AND MGD43.SUB_KB ='43' AND MGD43.SYS_DEL_FLG ='0'                                         " & vbNewLine _ 
                                             & " --商品M LMC757  2015/1/14                                                              " & vbNewLine _
                                             & "WHERE                                                                                                                          " & vbNewLine _
                                             & "    OUTL.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                                                                " & vbNewLine _
                                             & "AND OUTL.OUTKA_NO_L = @KANRI_NO_L                                                                                              " & vbNewLine _
                                             & "--運送テーブル                                                                                                           " & vbNewLine _
                                            & "UNION ALL                                                                                                                      " & vbNewLine _
                                            & "SELECT                                                                                                                         " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                              " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                              " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                                                                          " & vbNewLine _
                                            & " END                                                                             AS RPT_ID                                     " & vbNewLine _
                                            & ",UNSOL.NRS_BR_CD                                                                 AS NRS_BR_CD                                  " & vbNewLine _
                                            & ",0                                                                               AS PRINT_SORT                                 " & vbNewLine _
                                            & ",UNSOL.DEST_CD                                                                   AS DEST_CD                                    " & vbNewLine _
                                            & ",MD.DEST_NM                                                                      AS DEST_NM                                    " & vbNewLine _
                                            & ",MD.AD_1                                                                         AS DEST_AD_1                                  " & vbNewLine _
                                            & ",MD.AD_2                                                                         AS DEST_AD_2                                  " & vbNewLine _
                                            & "--(2014.07.02)修正START                                                                                                        " & vbNewLine _
                                            & ",UNSOL.AD_3                                                                      AS DEST_AD_3                                  " & vbNewLine _
                                            & "--,MD.AD_3                                                                         AS DEST_AD_3                                  " & vbNewLine _
                                            & "--(2014.07.02)修正END                                                                                                          " & vbNewLine _
                                            & ",MD.TEL                                                                          AS DEST_TEL                                   " & vbNewLine _
                                            & ",UNSOL.OUTKA_PLAN_DATE                                                           AS OUTKA_PLAN_DATE                            " & vbNewLine _
                                            & ",UNSOL.CUST_REF_NO                                                               AS CUST_ORD_NO                                " & vbNewLine _
                                            & ",UNSOL.BUY_CHU_NO                                                                AS BUYER_ORD_NO                               " & vbNewLine _
                                            & ",UNSOCO.UNSOCO_NM                                                                AS UNSOCO_NM                                  " & vbNewLine _
                                            & ",UNSOL.ARR_PLAN_DATE                                                             AS ARR_PLAN_DATE                              " & vbNewLine _
                                            & ",KBN1.KBN_NM1                                                                    AS ARR_PLAN_TIME                              " & vbNewLine _
                                            & ",CASE WHEN RTRIM(UNSOL.INOUTKA_NO_L) <> '' THEN UNSOL.INOUTKA_NO_L                                                             " & vbNewLine _
                                            & "      ELSE UNSOL.UNSO_NO_L                                                                                                     " & vbNewLine _
                                            & " END                                                                             AS OUTKA_NO_L                                 " & vbNewLine _
                                            & ",MDU.DEST_NM                                                                     AS URIG_NM                                    " & vbNewLine _
                                            & ",MC.CUST_NM_L                                                                    AS CUST_NM_L                                  " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 -- START --                                                                                        " & vbNewLine _
                                            & ",MC.CUST_NM_M                                                                    AS CUST_NM_M                                  " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 --  END  --                                                                                        " & vbNewLine _
                                            & ",MC.AD_1                                                                         AS CUST_AD_1                                  " & vbNewLine _
                                            & ",MC.AD_2                                                                         AS CUST_AD_2                                  " & vbNewLine _
                                            & ",MC.TEL                                                                          AS CUST_TEL                                   " & vbNewLine _
                                            & ",MC.DENPYO_NM                                                                    AS DENPYO_NM                                   " & vbNewLine _
                                            & ",MB.NRS_BR_NM                                                                    AS NRS_BR_NM                                  " & vbNewLine _
                                            & ",MB.AD_1                                                                         AS NRS_BR_AD1                                 " & vbNewLine _
                                            & ",MB.AD_2                                                                         AS NRS_BR_AD2                                 " & vbNewLine _
                                            & ",MB.TEL                                                                          AS NRS_BR_TEL                                 " & vbNewLine _
                                            & ",MB.FAX                                                                          AS NRS_BR_FAX                                 " & vbNewLine _
                                            & ",USER_E.USER_NM                                                                  AS PIC                                        " & vbNewLine _
                                            & ",UNSOM.UNSO_NO_M                                                                 AS OUTKA_NO_M                                 " & vbNewLine _
                                            & ",''                                                                              AS OUTKA_NO_S                                 " & vbNewLine _
                                            & ",''                                                                              AS INKA_DATE                                  " & vbNewLine _
                                            & ",''                                                                              AS REMARK_OUT                                 " & vbNewLine _
                                            & "--NOTES 1032 START①                                                                                                           " & vbNewLine _
                                            & "--,CASE WHEN (MDG.GOODS_NM IS NOT NULL AND MDG.GOODS_NM <> '') THEN MDG.GOODS_NM                                                 " & vbNewLine _
                                            & "--      ELSE MG.GOODS_NM_1                                                                                                       " & vbNewLine _
                                            & "-- END                                                                             AS GOODS_NM_1                                 " & vbNewLine _
                                            & ",CASE WHEN (MDG.GOODS_NM IS NOT NULL AND MDG.GOODS_NM <> '') THEN MDG.GOODS_NM   --Case1                                       " & vbNewLine _
                                            & "	  WHEN (MG.GOODS_NM_1 IS NOT NULL AND MG.GOODS_NM_1 <> '') THEN MG.GOODS_NM_1   --Case2                                       " & vbNewLine _
                                            & "      ELSE UNSOM.GOODS_NM                                                        --Case3                                       " & vbNewLine _
                                            & " END                                                                             AS GOODS_NM_1                                 " & vbNewLine _
                                            & "--NOTES 1032 END①                                                                                                             " & vbNewLine _
                                            & ",UNSOM.LOT_NO                                                                    AS LOT_NO                                     " & vbNewLine _
                                            & ",''                                                                              AS SERIAL_NO                                  " & vbNewLine _
                                            & ",UNSOM.IRIME                                                                     AS IRIME                                      " & vbNewLine _
                                            & "--NOTES 1032 START②                                                                                                           " & vbNewLine _
                                            & "--,MG.PKG_NB                                                                       AS PKG_NB                                     " & vbNewLine _
                                            & ",CASE WHEN (MG.PKG_NB IS NOT NULL AND MG.PKG_NB <> 0 ) THEN MG.PKG_NB                                                          " & vbNewLine _
                                            & "      ELSE UNSOM.PKG_NB                                                                                                        " & vbNewLine _
                                            & " END                                                                             AS PKG_NB                                     " & vbNewLine _
                                            & "--NOTES 1032 END②                                                                                                             " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB / MG.PKG_NB                                                   AS KONSU                                      " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB % MG.PKG_NB                                                   AS HASU                                       " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB                                                               AS ALCTD_NB                                   " & vbNewLine _
                                            & "--NOTES 1032 START③                                                                                                           " & vbNewLine _
                                            & "--,MG.PKG_UT                                                                       AS PKG_UT                                   " & vbNewLine _
                                            & ",CASE WHEN (MG.PKG_UT IS NOT NULL AND MG.PKG_UT <> '') THEN MG.PKG_UT                                                          " & vbNewLine _
                                            & "      ELSE UNSOM.QT_UT                                                                                                         " & vbNewLine _
                                            & " END                                                                             AS PKG_UT                                     " & vbNewLine _
                                            & "--NOTES 1032 END③                                                                                                             " & vbNewLine _
                                            & "--NOTES 1032 START④                                                                                                           " & vbNewLine _
                                            & "--,KBN2.KBN_NM1                                                                    AS PKG_UT_NM                                " & vbNewLine _
                                            & "--2012/05/24 START                                                                                                             " & vbNewLine _
                                            & "--,CASE WHEN (MG.PKG_UT IS NOT NULL AND MG.PKG_UT <> '') THEN KBN2.KBN_NM1                                                     " & vbNewLine _
                                            & "--      ELSE KBN2_1.KBN_NM1                                                                                                    " & vbNewLine _
                                            & "-- END                                                                             AS PKG_UT_NM                                " & vbNewLine _
                                            & ",CASE WHEN (KBN2.KBN_NM1 IS NOT NULL AND KBN2.KBN_NM1 <> '') THEN KBN2.KBN_NM1                                                 " & vbNewLine _
                                            & "      ELSE KBN2_1.KBN_NM1                                                                                                      " & vbNewLine _
                                            & " END                                                                             AS PKG_UT_NM                                  " & vbNewLine _
                                            & "--2012/05/24 END                                                                                                               " & vbNewLine _
                                            & "--NOTES 1032 END④                                                                                                             " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_QT                                                               AS ALCTD_QT                                   " & vbNewLine _
                                            & "--NOTES 1032 START⑤                                                                                                           " & vbNewLine _
                                            & "--,MG.STD_IRIME_UT                                                                 AS IRIME_UT                                 " & vbNewLine _
                                            & ",CASE WHEN (MG.STD_IRIME_UT IS NOT NULL AND MG.STD_IRIME_UT <> '') THEN MG.STD_IRIME_UT                                        " & vbNewLine _
                                            & "      ELSE UNSOM.IRIME_UT                                                                                                      " & vbNewLine _
                                            & " END                                                                             AS IRIME_UT                                   " & vbNewLine _
                                            & "--NOTES 1032 END⑤                                                                                                             " & vbNewLine _
                                            & ",0                                                                               AS WT                                         " & vbNewLine _
                                            & ",UNSOL.UNSO_WT                                                                   AS UNSO_WT                                    " & vbNewLine _
                                            & ",''                                                                              AS TOU_NO                                     " & vbNewLine _
                                            & ",''                                                                              AS SITU_NO                                    " & vbNewLine _
                                            & ",''                                                                              AS ZONE_CD                                    " & vbNewLine _
                                            & ",''                                                                              AS LOCA                                       " & vbNewLine _
                                            & ",0                                                                               AS ZAN_KONSU                                  " & vbNewLine _
                                            & ",0                                                                               AS ZAN_HASU                                   " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST                                                                AS GOODS_CD_CUST                              " & vbNewLine _
                                            & ",''                                                                              AS REMARK_M                                   " & vbNewLine _
                                            & "--★★★変更開始--------------------------------                                                                               " & vbNewLine _
                                            & ",''                                                            AS CUST_NM_S                                " & vbNewLine _
                                            & ",''                                                             AS CUST_NM_S_H                                " & vbNewLine _
                                            & "--★★★変更終了--------------------------------                                                                               " & vbNewLine _
                                            & ",UNSOL.CUST_REF_NO                                                               AS CUST_ORD_NO_DTL                            " & vbNewLine _
                                            & ",''                                                                              AS OUTKA_REMARK                               " & vbNewLine _
                                            & ",UNSOL.REMARK                                                                    AS HAISOU_REMARK                              " & vbNewLine _
                                            & ",''                                                                              AS NHS_REMARK                                 " & vbNewLine _
                                            & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  --- START ---                                             " & vbNewLine _
                                            & "--,CASE WHEN MD.KYORI > 0 THEN MD.KYORI                                                                                        " & vbNewLine _
                                            & "--      WHEN (MD.KYORI = 0 AND MDO.JIS < MD.JIS) THEN MK1.KYORI                                                                " & vbNewLine _
                                            & "--      WHEN (MD.KYORI = 0 AND MD.JIS < MDO.JIS) THEN MK2.KYORI                                                                " & vbNewLine _
                                            & "--      ELSE 0                                                                                                                 " & vbNewLine _
                                            & "-- END                                                                           AS KYORI                                      " & vbNewLine _
                                            & ",CASE WHEN MD.KYORI > 0 THEN MD.KYORI                                                                                          " & vbNewLine _
                                            & "      WHEN (MD.KYORI = 0 AND MDO.JIS <= MD.JIS) THEN MK1.KYORI                                                                 " & vbNewLine _
                                            & "      WHEN (MD.KYORI = 0 AND MD.JIS <= MDO.JIS) THEN MK2.KYORI                                                                 " & vbNewLine _
                                            & "      ELSE 0                                                                                                                   " & vbNewLine _
                                            & " END                                                                             AS KYORI                                      " & vbNewLine _
                                            & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  ---  END  ---                                             " & vbNewLine _
                                            & ",UNSOL.UNSO_PKG_NB                                                               AS OUTKA_PKG_NB_L                             " & vbNewLine _
                                            & ",''                                                                              AS SOKO_NM                                    " & vbNewLine _
                                            & ",''                                                                              AS BUSHO_NM                                   " & vbNewLine _
                                            & ",''                                                                              AS NRS_BR_NM_NICHI                            " & vbNewLine _
                                            & ",0                                                                               AS ALCTD_CAN_QT                               " & vbNewLine _
                                            & ",''                                                                              AS ALCTD_KB                                   " & vbNewLine _
                                            & ",'1'                                                                             AS PTN_FLAG                                   " & vbNewLine _
                                            & ",UNSOL.CUST_CD_L                                                                 AS CUST_CD_L                                  " & vbNewLine _
                                            & ",''                                                                              AS ORDER_TYPE                                 " & vbNewLine _
                                            & ",''                                                                              AS LT_DATE                                    " & vbNewLine _
                                            & ",''                                                                              AS BUYER_ORD_NO_DTL                           " & vbNewLine _
                                            & ",''                                                                              AS GOODS_COND_CD                              " & vbNewLine _
                                            & ",''                                                                              AS GOODS_COND_NM--☆                            " & vbNewLine _
                                            & ",''                                                                              AS REMARK_ZAI                                 " & vbNewLine _
                                            & ",''                                                                              AS TITLE_SMPL                                 " & vbNewLine _
                                            & ",0                                                                               AS ALCTD_CAN_NB                               " & vbNewLine _
                                            & ",0                                                                               AS BACKLOG_QT                                 " & vbNewLine _
                                            & ",KBN3.KBN_NM2                                                                    AS DOKU_NM                                    " & vbNewLine _
                                            & ",CASE WHEN MDU.DEST_NM <> ''                                     --★VB追加               " & vbNewLine _
                                            & "           AND MDU.DEST_NM IS NOT NULL THEN MDU.DEST_NM                              " & vbNewLine _
                                            & "      WHEN MC.DENPYO_NM   <> ''                                                 " & vbNewLine _
                                            & "           AND MC.DENPYO_NM IS NOT NULL   THEN MC.DENPYO_NM                        " & vbNewLine _
                                            & "      ELSE MC.CUST_NM_L                                                         " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM    " & vbNewLine _
                                            & " --【Notes】№728 作業項目1～5を表示(大阪のみ) START                                                                           " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_REC_NO_1                         " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_CD_1                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_NM_1                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_REC_NO_2                         " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_CD_2                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_NM_2                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_REC_NO_3                         " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_CD_3                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_NM_3                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_REC_NO_4                         " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_CD_4                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_NM_4                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_REC_NO_5                         " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_CD_5                             " & vbNewLine _
                                            & ",''                                                                              AS SAGYO_MEI_NM_5                             " & vbNewLine _
                                            & " --【Notes】№728 作業項目1～5を表示(大阪のみ) END                                                                             " & vbNewLine _
                                            & "--2012/03/12対応開始                                                                                                           " & vbNewLine _
                                            & ",''                                                                              AS TOU_SITU_NM                                " & vbNewLine _
                                            & ",''                                                                              AS JISYATASYA_KB                              " & vbNewLine _
                                            & "--2012/03/12対応終了                                                                                                           " & vbNewLine _
                                            & "--【要望番号995】　2012/04/20　元着払い区分　START                                                                             " & vbNewLine _
                                            & ",KBN9.KBN_NM2                                                                    AS PC_KB_NM                                   " & vbNewLine _
                                            & "--【要望番号995】　2012/04/20　元着払い区分　 END                                                                              " & vbNewLine _
                                            & "--日医工対策開始★                                                                                                             " & vbNewLine _
                                            & ",''                       AS TORI_KB                                                                                           " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & ",''                       AS SHIP_CD_L                                                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--★削除開始2012/05/11                                                                                                         " & vbNewLine _
                                            & "--,''                       AS CHOKUSO_NM1                                                                                     " & vbNewLine _
                                            & "--,''                       AS CHOKUSO_CD                                                                                      " & vbNewLine _
                                            & "--,''                       AS HAKKO_DATE                                                                                      " & vbNewLine _
                                            & "--★削除終了2012/05/11                                                                                                         " & vbNewLine _
                                            & ",''                       AS SET_NAIYO_4                                                                                       " & vbNewLine _
                                            & ",''                       AS SET_NAIYO_5                                                                                       " & vbNewLine _
                                            & ",''                       AS SET_NAIYO_6                                                                                       " & vbNewLine _
                                            & ",''                       AS GOODS_NM_2                                                                                        " & vbNewLine _
                                            & ",''                       AS KICHO_KB                                                                                          " & vbNewLine _
                                            & ",''                       AS SEI_YURAI_KB                                                                                      " & vbNewLine _
                                            & ",''                       AS REMARK_UPPER                                                                                      " & vbNewLine _
                                            & ",''                       AS REMARK_LOWER                                                                                      " & vbNewLine _
                                            & ",''                       AS GOODS_SYUBETU                                                                                     " & vbNewLine _
                                            & ",''                       AS YUKOU_KIGEN                                                                                       " & vbNewLine _
                                            & ",''                       AS GOODS_NM_3         --2012/05/23                                                                   " & vbNewLine _
                                            & ",''                       AS EDISHIP_CD    --2012/05/23                                                                        " & vbNewLine _
                                            & ",''                       AS EDIDEST_CD    --2012/05/23                                                                        " & vbNewLine _
                                            & "--日医工対策終了★                                                                                                             " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応 --- START ---                                                                                      " & vbNewLine _
                                            & ",''                       AS SET_NAIYO_FROM1                                                                                   " & vbNewLine _
                                            & ",''                       AS SET_NAIYO_FROM2                                                                                   " & vbNewLine _
                                            & ",''                       AS SET_NAIYO_FROM3                                                                                   " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応 ---  END ---                                                                                       " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                            & ",''                       AS CUST_AD_3                                                                                         " & vbNewLine _
                                            & ",''                       AS DEST_REMARK                                                                                       " & vbNewLine _
                                            & ",''                       AS DEST_SALES_CD                                                                                     " & vbNewLine _
                                            & ",''                       AS DEST_SALES_NM                                                                                     " & vbNewLine _
                                            & ",''                       AS DEST_SALES_AD_1                                                                                      " & vbNewLine _
                                            & ",''                       AS DEST_SALES_AD_2                                                                                      " & vbNewLine _
                                            & ",''                       AS DEST_SALES_AD_3                                                                                      " & vbNewLine _
                                            & ",''                       AS DEST_SALES_TEL                                                                                      " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                            & "--Notes1331  群馬対応  ---START---                                                                                             " & vbNewLine _
                                            & ",''                       AS SAGYO_RYAK_1                                                                                      " & vbNewLine _
                                            & ",''                       AS SAGYO_RYAK_2                                                                                      " & vbNewLine _
                                            & ",''                       AS SAGYO_RYAK_3                                                                                      " & vbNewLine _
                                            & ",''                       AS SAGYO_RYAK_4                                                                                      " & vbNewLine _
                                            & ",''                       AS SAGYO_RYAK_5                                                                                      " & vbNewLine _
                                            & ",''                       AS DEST_SAGYO_RYAK_1                                                                                 " & vbNewLine _
                                            & ",''                       AS DEST_SAGYO_RYAK_2                                                                                 " & vbNewLine _
                                            & ",''                       AS SZ01_YUSO                                                                                         " & vbNewLine _
                                            & ",''                       AS SZ01_UNSO                                                                                         " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                            & "  , MD.DELI_ATT           AS  HAISOU_REMARK_MST                                                                                    " & vbNewLine _
                                            & "  , MGD43.REMARK          AS  GOODS_NM_CUST                                                                                    " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                            & "--Notes1331  群馬対応  --- END ---                                                                                             " & vbNewLine _
                                            & "FROM                                                                                                                           " & vbNewLine _
                                            & "--運送L                                                                                                                        " & vbNewLine _
                                            & "$LM_TRN$..F_UNSO_L UNSOL                                                                                                       " & vbNewLine _
                                            & "--運送M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM                                                                                             " & vbNewLine _
                                            & "ON  UNSOM.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                            & "AND UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                                                                          " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                            & "--運送M(中MIN)                                                                                                                 " & vbNewLine _
                                            & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                            & "       (SELECT                                                                                                                 " & vbNewLine _
                                            & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                            & "           ,UNSO_NO_L                                                                                                          " & vbNewLine _
                                            & "           ,MIN(UNSO_NO_M) AS  UNSO_NO_M                                                                                       " & vbNewLine _
                                            & "       FROM $LM_TRN$..F_UNSO_M WHERE SYS_DEL_FLG ='0'                                                                          " & vbNewLine _
                                            & "       GROUP BY NRS_BR_CD,UNSO_NO_L) UNSOM_MIN                                                                                 " & vbNewLine _
                                            & "       ON UNSOM_MIN.NRS_BR_CD       = UNSOL.NRS_BR_CD                                                                          " & vbNewLine _
                                            & "       AND UNSOM_MIN.UNSO_NO_L      = UNSOL.UNSO_NO_L                                                                          " & vbNewLine _
                                            & "--運送M(中MIN)                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM2                                                                                            " & vbNewLine _
                                            & "ON  UNSOM2.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                         " & vbNewLine _
                                            & "AND UNSOM2.UNSO_NO_L = UNSOL.UNSO_NO_L                                                                                         " & vbNewLine _
                                            & "AND UNSOM2.UNSO_NO_M = UNSOM_MIN.UNSO_NO_M                                                                                     " & vbNewLine _
                                            & "AND UNSOM2.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                            & "--商品M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                 " & vbNewLine _
                                            & "ON  MG.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MG.NRS_BR_CD = UNSOM.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MG.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                                                                       " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                            & "--商品M(MIN)                                                                                                                   " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                                  " & vbNewLine _
                                            & "ON M_GOODS_MIN.NRS_BR_CD      = UNSOL.NRS_BR_CD                                                                                " & vbNewLine _
                                            & "AND M_GOODS_MIN.GOODS_CD_NRS   = UNSOM2.GOODS_CD_NRS                                                                           " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                            & "--運送会社M                                                                                                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                                                                            " & vbNewLine _
                                            & "ON  UNSOCO.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "AND UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                         " & vbNewLine _
                                            & "AND UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                                                                           " & vbNewLine _
                                            & "AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                                                                     " & vbNewLine _
                                            & "--届先M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MD                                                                                                  " & vbNewLine _
                                            & "ON  MD.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MD.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MD.CUST_CD_L = UNSOL.CUST_CD_L                                                                                             " & vbNewLine _
                                            & "AND MD.DEST_CD = UNSOL.DEST_CD                                                                                                 " & vbNewLine _
                                            & "--届先M(売上先取得)                                                                                                            " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MDU                                                                                                 " & vbNewLine _
                                            & "ON  MDU.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDU.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDU.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDU.DEST_CD = UNSOL.SHIP_CD                                                                                                " & vbNewLine _
                                            & "--届先M(距離取得)                                                                                                              " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MDO                                                                                                 " & vbNewLine _
                                            & "ON  MDO.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDO.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDO.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDO.DEST_CD = UNSOL.ORIG_CD                                                                                                " & vbNewLine _
                                            & "--荷主M(商品M経由)                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                                  " & vbNewLine _
                                            & "ON  MC.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MC.NRS_BR_CD = MG.NRS_BR_CD                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                                              " & vbNewLine _
                                            & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                                                                                " & vbNewLine _
                                            & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                                                                        " & vbNewLine _
                                            & "AND UNSOL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                                                                       " & vbNewLine _
                                            & "--2014/5/16End s.kobayashi NotesNo.2183                                                                                       " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)                                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_KYORI MK1                                                                                                " & vbNewLine _
                                            & "ON  MK1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MK1.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "--AND MK1.KYORI_CD  = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                            & "AND MK1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                        " & vbNewLine _
                                            & "AND MK1.ORIG_JIS_CD = MDO.JIS                                                                                                  " & vbNewLine _
                                            & "AND MK1.DEST_JIS_CD = MD.JIS                                                                                                   " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)                                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_KYORI MK2                                                                                                " & vbNewLine _
                                            & "ON  MK2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MK2.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "--AND MK2.KYORI_CD  = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                            & "AND MK2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                        " & vbNewLine _
                                            & "AND MK2.ORIG_JIS_CD = MD.JIS                                                                                                   " & vbNewLine _
                                            & "AND MK2.DEST_JIS_CD = MDO.JIS                                                                                                  " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                            & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                            & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                            & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                            & "--営業所M                                                                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_NRS_BR MB                                                                                                " & vbNewLine _
                                            & "ON  MB.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MB.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "--届先商品M(運送L参照)                                                                                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DESTGOODS MDG                                                                                            " & vbNewLine _
                                            & "ON  MDG.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDG.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDG.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDG.CUST_CD_M = UNSOL.CUST_CD_M                                                                                            " & vbNewLine _
                                            & "AND MDG.CD = UNSOL.DEST_CD                                                                                                     " & vbNewLine _
                                            & "AND MDG.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                                                                      " & vbNewLine _
                                            & "--区分M(納入予定区分)                                                                                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                                 " & vbNewLine _
                                            & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                                                 " & vbNewLine _
                                            & "AND KBN1.KBN_CD = UNSOL.ARR_PLAN_TIME                                                                                          " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--区分M(荷姿)                                                                                                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                                                 " & vbNewLine _
                                            & "ON  KBN2.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                            & "AND KBN2.KBN_CD = MG.PKG_UT                                                                                                    " & vbNewLine _
                                            & "AND KBN2.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--SYS_ENT_USER名称取得                                                                                                         " & vbNewLine _
                                            & "--区分M(荷姿)F運送M版(Notes1032)                                                                                               " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN2_1                                                                                                 " & vbNewLine _
                                            & "ON  KBN2_1.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                            & "AND KBN2_1.KBN_CD = UNSOM.QT_UT                                                                                                    " & vbNewLine _
                                            & "AND KBN2_1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--SYS_ENT_USER名称取得                                                                                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER USER_E                                                                                              " & vbNewLine _
                                            & "ON  USER_E.USER_CD     = UNSOL.SYS_ENT_USER                                                                                    " & vbNewLine _
                                            & "AND USER_E.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "--区分M(毒劇区分名称)                                                                                                           " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                                                  " & vbNewLine _
                                            & "ON  KBN3.KBN_GROUP_CD = 'G001'                                                                                                  " & vbNewLine _
                                            & "AND KBN3.KBN_CD = MG.DOKU_KB                                                                                                    " & vbNewLine _
                                            & "AND KBN3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--【要望番号995】　2012/04/20　元着払い区分　START                                                                             " & vbNewLine _
                                            & "--区分M(元着払区分名称)                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN9                                                                                                 " & vbNewLine _
                                            & "ON  KBN9.KBN_GROUP_CD = 'M001'                                                                                                 " & vbNewLine _
                                            & "AND KBN9.KBN_CD = UNSOL.PC_KB                                                                                                   " & vbNewLine _
                                            & "AND KBN9.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--【要望番号995】　2012/04/20　元着払い区分　 END                                                                              " & vbNewLine _
                                            & "--運送Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                            & "ON  UNSOL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                           " & vbNewLine _
                                            & "AND UNSOL.CUST_CD_L = MCR1.CUST_CD_L                                                                                           " & vbNewLine _
                                            & "AND UNSOL.CUST_CD_M = MCR1.CUST_CD_M                                                                                           " & vbNewLine _
                                            & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                            & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                            & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                            & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                            & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                            & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                            & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                            & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                            & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                            & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                            & "ON  MR3.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                            & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                            & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--商品明細M                                                                                                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                                        " & vbNewLine _
                                            & "ON  MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                                               " & vbNewLine _
                                            & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                         " & vbNewLine _
                                            & "AND MGD.SUB_KB = '09'                                                                                                          " & vbNewLine _
                                            & " --商品M LMC757 2015/1/14                                                                " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD43                                                    " & vbNewLine _
                                            & "	ON  UNSOM.NRS_BR_CD = MGD43.NRS_BR_CD                                              " & vbNewLine _
                                            & "	AND UNSOM.GOODS_CD_NRS = MGD43.GOODS_CD_NRS                                              " & vbNewLine _
                                            & "	AND UNSOL.SHIP_CD = MGD43.SET_NAIYO                                              " & vbNewLine _
                                            & " AND MGD43.SUB_KB ='43' AND MGD43.SYS_DEL_FLG ='0'                                         " & vbNewLine _
                                            & " --商品M LMC757 2015/1/14                                                                " & vbNewLine _
                                            & "WHERE                                                                                                                          " & vbNewLine _
                                            & "    UNSOL.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                            & "AND UNSOL.NRS_BR_CD = @NRS_BR_CD                                                                                               " & vbNewLine _
                                            & "AND UNSOL.UNSO_NO_L = @KANRI_NO_L                                                                                              " & vbNewLine _
                                            & "--★★★SQL_WHERE_MAIN★★★                   " & vbNewLine _
                                           & "  ) MAIN                                       " & vbNewLine _
                                           & "WHERE                                          " & vbNewLine _
                                           & " PTN_FLAG = @PTN_FLAG                          " & vbNewLine _
                                           & "GROUP BY                                      " & vbNewLine _
                                         & " RPT_ID                                        " & vbNewLine _
                                         & ",NRS_BR_CD                                     " & vbNewLine _
                                         & ",PRINT_SORT                                    " & vbNewLine _
                                         & ",DEST_CD                                       " & vbNewLine _
                                         & ",DEST_NM                                       " & vbNewLine _
                                         & ",DEST_AD_1                                     " & vbNewLine _
                                         & ",DEST_AD_2                                     " & vbNewLine _
                                         & ",DEST_AD_3                                     " & vbNewLine _
                                         & ",DEST_TEL                                      " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                               " & vbNewLine _
                                         & ",CUST_ORD_NO                                   " & vbNewLine _
                                         & ",BUYER_ORD_NO                                  " & vbNewLine _
                                         & ",UNSOCO_NM                                     " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                 " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                 " & vbNewLine _
                                         & ",OUTKA_NO_L                                    " & vbNewLine _
                                         & ",URIG_NM                                       " & vbNewLine _
                                         & ",CUST_NM_L                                     " & vbNewLine _
                                         & "--(2012.06.15) 要望番号1155  --- START ---     " & vbNewLine _
                                         & ",CUST_NM_M                                     " & vbNewLine _
                                         & "--(2012.06.15) 要望番号1155  ---  END  ---     " & vbNewLine _
                                         & ",CUST_AD_1                                     " & vbNewLine _
                                         & ",CUST_AD_2                                     " & vbNewLine _
                                         & ",CUST_TEL                                      " & vbNewLine _
                                         & ",DENPYO_NM                                     " & vbNewLine _
                                         & ",NRS_BR_NM                                     " & vbNewLine _
                                         & ",NRS_BR_AD1                                    " & vbNewLine _
                                         & ",NRS_BR_AD2                                    " & vbNewLine _
                                         & ",NRS_BR_TEL                                    " & vbNewLine _
                                         & ",NRS_BR_FAX                                    " & vbNewLine _
                                         & ",PIC                                           " & vbNewLine _
                                         & ",OUTKA_NO_M                                    " & vbNewLine _
                                         & ",INKA_DATE                                     " & vbNewLine _
                                         & ",REMARK_OUT                                    " & vbNewLine _
                                         & ",GOODS_NM_1                                    " & vbNewLine _
                                         & ",LOT_NO                                        " & vbNewLine _
                                         & ",SERIAL_NO                                     " & vbNewLine _
                                         & ",IRIME                                         " & vbNewLine _
                                         & ",PKG_NB                                        " & vbNewLine _
                                         & ",PKG_UT                                        " & vbNewLine _
                                         & ",PKG_UT_NM                                     " & vbNewLine _
                                         & ",IRIME_UT                                      " & vbNewLine _
                                         & ",WT                                            " & vbNewLine _
                                         & ",UNSO_WT                                       " & vbNewLine _
                                         & ",TOU_NO                                        " & vbNewLine _
                                         & ",SITU_NO                                       " & vbNewLine _
                                         & ",ZONE_CD                                       " & vbNewLine _
                                         & ",LOCA                                          " & vbNewLine _
                                         & ",GOODS_CD_CUST                                 " & vbNewLine _
                                         & ",REMARK_M                                      " & vbNewLine _
                                         & ",CUST_NM_S                                     " & vbNewLine _
                                         & ",CUST_NM_S_H                                     " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                               " & vbNewLine _
                                         & ",OUTKA_REMARK                                  " & vbNewLine _
                                         & ",HAISOU_REMARK                                 " & vbNewLine _
                                         & ",NHS_REMARK                                    " & vbNewLine _
                                         & ",KYORI                                         " & vbNewLine _
                                         & ",OUTKA_PKG_NB_L                                " & vbNewLine _
                                         & ",SOKO_NM                                       " & vbNewLine _
                                         & ",BUSHO_NM                                      " & vbNewLine _
                                         & ",NRS_BR_NM_NICHI                               " & vbNewLine _
                                         & ",ALCTD_KB                                      " & vbNewLine _
                                         & ",PTN_FLAG                                      " & vbNewLine _
                                         & ",CUST_CD_L                                     " & vbNewLine _
                                         & ",ORDER_TYPE                                    " & vbNewLine _
                                         & ",LT_DATE                                        " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                               " & vbNewLine _
                                         & ",GOODS_COND_CD                                  " & vbNewLine _
                                         & ",GOODS_COND_NM                                     " & vbNewLine _
                                         & ",REMARK_ZAI                                     " & vbNewLine _
                                         & ",TITLE_SMPL                                     " & vbNewLine _
                                         & ",BACKLOG_QT                                     " & vbNewLine _
                                         & ",DOKU_NM                                        " & vbNewLine _
                                         & ",ATSUKAISYA_NM --★VB" & vbNewLine _
                                         & "-- Notes№728 作業項目1～5を表示(大阪のみ) START " & vbNewLine _
                                         & ",SAGYO_MEI_REC_NO_1                              " & vbNewLine _
                                         & ",SAGYO_MEI_CD_1                                  " & vbNewLine _
                                         & ",SAGYO_MEI_NM_1                                  " & vbNewLine _
                                         & ",SAGYO_MEI_REC_NO_2                              " & vbNewLine _
                                         & ",SAGYO_MEI_CD_2                                  " & vbNewLine _
                                         & ",SAGYO_MEI_NM_2                                  " & vbNewLine _
                                         & ",SAGYO_MEI_REC_NO_3                              " & vbNewLine _
                                         & ",SAGYO_MEI_CD_3                                  " & vbNewLine _
                                         & ",SAGYO_MEI_NM_3                                  " & vbNewLine _
                                         & ",SAGYO_MEI_REC_NO_4                              " & vbNewLine _
                                         & ",SAGYO_MEI_CD_4                                  " & vbNewLine _
                                         & ",SAGYO_MEI_NM_4                                  " & vbNewLine _
                                         & ",SAGYO_MEI_REC_NO_5                              " & vbNewLine _
                                         & ",SAGYO_MEI_CD_5                                  " & vbNewLine _
                                         & ",SAGYO_MEI_NM_5                                  " & vbNewLine _
                                         & "-- Notes№728 作業項目1～5を表示(大阪のみ) END   " & vbNewLine _
                                         & "--2012/03/12対応開始                             " & vbNewLine _
                                         & ",TOU_SITU_NM                                     " & vbNewLine _
                                         & ",JISYATASYA_KB                                   " & vbNewLine _
                                         & "--2012/03/12対応終了                             " & vbNewLine _
                                         & "--【要望番号995】　2012/04/17　元着払い区分　START " & vbNewLine _
                                         & ",PC_KB_NM                                          " & vbNewLine _
                                         & "--【要望番号995】　2012/04/17　元着払い区分　 END  " & vbNewLine _
                                         & "--【要望番号1017】　 2012/04/24　注文番号明細  START " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                   " & vbNewLine _
                                         & "--【要望番号1017】　2012/04/24 注文番号明細　 END   " & vbNewLine _
                                         & "--日医工対策開始                                    " & vbNewLine _
                                         & ",TORI_KB                                            " & vbNewLine _
                                         & "--★追加開始2012/05/11                              " & vbNewLine _
                                         & ",SHIP_CD_L                                          " & vbNewLine _
                                         & "--★追加終了2012/05/11                              " & vbNewLine _
                                         & "--★削除開始2012/05/11                              " & vbNewLine _
                                         & "--,CHOKUSO_NM1                                      " & vbNewLine _
                                         & "--,CHOKUSO_CD                                       " & vbNewLine _
                                         & "--,HAKKO_DATE                                       " & vbNewLine _
                                         & "--★削除終了2012/05/11                              " & vbNewLine _
                                         & ",SET_NAIYO_4                                        " & vbNewLine _
                                         & ",SET_NAIYO_5                                        " & vbNewLine _
                                         & ",SET_NAIYO_6                                        " & vbNewLine _
                                         & ",GOODS_NM_2                                         " & vbNewLine _
                                         & ",KICHO_KB                                           " & vbNewLine _
                                         & ",SEI_YURAI_KB                                       " & vbNewLine _
                                         & ",REMARK_UPPER                                       " & vbNewLine _
                                         & ",REMARK_LOWER                                       " & vbNewLine _
                                         & ",GOODS_SYUBETU                                      " & vbNewLine _
                                         & ",YUKOU_KIGEN                                        " & vbNewLine _
                                         & ",GOODS_NM_3 --2012/05/23                            " & vbNewLine _
                                         & ",EDISHIP_CD --2012/05/23                            " & vbNewLine _
                                         & ",EDIDEST_CD --2012/05/23                            " & vbNewLine _
                                         & "--日医工対策終了                                    " & vbNewLine _
                                         & "--(2012.06.01)日本ﾌｧｲﾝ対応 -- START --              " & vbNewLine _
                                         & ",SET_NAIYO_FROM1                                    " & vbNewLine _
                                         & ",SET_NAIYO_FROM2                                    " & vbNewLine _
                                         & ",SET_NAIYO_FROM3                                    " & vbNewLine _
                                         & "--(2012.06.01)日本ﾌｧｲﾝ対応 --  END  --              " & vbNewLine _
                                         & "--【要望番号1122】埼玉対応 --- START ---            " & vbNewLine _
                                         & ",CUST_AD_3                                          " & vbNewLine _
                                         & ",DEST_REMARK                                        " & vbNewLine _
                                         & ",DEST_SALES_CD                                      " & vbNewLine _
                                         & ",DEST_SALES_NM                                      " & vbNewLine _
                                         & ",DEST_SALES_AD_1                                    " & vbNewLine _
                                         & ",DEST_SALES_AD_2                                    " & vbNewLine _
                                         & ",DEST_SALES_AD_3                                    " & vbNewLine _
                                         & ",DEST_SALES_TEL                                     " & vbNewLine _
                                         & "--【要望番号1122】埼玉対応 ---  END  ---            " & vbNewLine _
                                         & "--群馬対応20120803開始                              " & vbNewLine _
                                         & ",SAGYO_RYAK_1                                       " & vbNewLine _
                                         & ",SAGYO_RYAK_2                                       " & vbNewLine _
                                         & ",SAGYO_RYAK_3                                       " & vbNewLine _
                                         & ",SAGYO_RYAK_4                                       " & vbNewLine _
                                         & ",SAGYO_RYAK_5                                       " & vbNewLine _
                                         & ",DEST_SAGYO_RYAK_1                                  " & vbNewLine _
                                         & ",DEST_SAGYO_RYAK_2                                  " & vbNewLine _
                                         & ",SZ01_YUSO                                          " & vbNewLine _
                                         & ",SZ01_UNSO                                          " & vbNewLine _
                                         & "--群馬対応20120803終了                              " & vbNewLine _
                                         & "--2015/1/14 LMC757                                  " & vbNewLine _
                                         & ",HAISOU_REMARK_MST                                           " & vbNewLine _
                                         & ",GOODS_NM_CUST                                      " & vbNewLine _
                                         & "--2015/1/14 LMC757                                  " & vbNewLine _
                                         & "--  ★★★SQL_ORDER_BY★★★                " & vbNewLine _
                                         & "     ORDER BY                               " & vbNewLine _
                                         & "     NRS_BR_CD                              " & vbNewLine _
                                         & "    ,OUTKA_NO_L                             " & vbNewLine _
                                         & "    ,PRINT_SORT                             " & vbNewLine _
                                         & "    ,OUTKA_NO_M                             " & vbNewLine _
                                         & "    ,OUTKA_NO_S                             " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用(埼玉ゴードー用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_815 As String = " --SQL_SELECT_SAITAMA_GODO                                          " & vbNewLine _
                                            & " SELECT                                                            " & vbNewLine _
                                            & " RPT_ID                                                            " & vbNewLine _
                                            & ",NRS_BR_CD                                                         " & vbNewLine _
                                            & ",PRINT_SORT                                                        " & vbNewLine _
                                            & ",DEST_CD                                                           " & vbNewLine _
                                            & ",DEST_NM                                                           " & vbNewLine _
                                            & ",DEST_AD_1                                                         " & vbNewLine _
                                            & ",DEST_AD_2                                                         " & vbNewLine _
                                            & ",DEST_AD_3                                                         " & vbNewLine _
                                            & ",DEST_TEL                                                          " & vbNewLine _
                                            & ",OUTKA_PLAN_DATE                                                   " & vbNewLine _
                                            & ",CUST_ORD_NO                                                       " & vbNewLine _
                                            & ",BUYER_ORD_NO                                                      " & vbNewLine _
                                            & ",UNSOCO_NM                                                         " & vbNewLine _
                                            & ",ARR_PLAN_DATE                                                     " & vbNewLine _
                                            & ",ARR_PLAN_TIME                                                     " & vbNewLine _
                                            & ",OUTKA_NO_L                                                        " & vbNewLine _
                                            & ",URIG_NM                                                           " & vbNewLine _
                                            & ",CUST_NM_L                                                         " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 -- START --                            " & vbNewLine _
                                            & ",CUST_NM_M                                                         " & vbNewLine _
                                            & ",DENPYO_NM                                                         " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 --  END  --                            " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_1      " & vbNewLine _
                                            & "      ELSE CUST_AD_1 END CUST_AD_1                                 " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_2      " & vbNewLine _
                                            & "      ELSE CUST_AD_2 END CUST_AD_2                                 " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_TEL       " & vbNewLine _
                                            & "      ELSE CUST_TEL END CUST_TEL                                   " & vbNewLine _
                                            & ",NRS_BR_NM                                                         " & vbNewLine _
                                            & ",NRS_BR_AD1                                                        " & vbNewLine _
                                            & ",NRS_BR_AD2                                                        " & vbNewLine _
                                            & ",NRS_BR_TEL                                                        " & vbNewLine _
                                            & ",NRS_BR_FAX                                                        " & vbNewLine _
                                            & ",PIC                                                               " & vbNewLine _
                                            & ",'' AS OUTKA_NO_M                                                  " & vbNewLine _
                                            & ",'' AS OUTKA_NO_S                                                  " & vbNewLine _
                                            & ",'' AS INKA_DATE                                                   " & vbNewLine _
                                            & ",REMARK_OUT                                                        " & vbNewLine _
                                            & ",GOODS_NM_1                                                        " & vbNewLine _
                                            & ",LOT_NO                                                            " & vbNewLine _
                                            & ",SERIAL_NO                                                         " & vbNewLine _
                                            & ",IRIME                                                             " & vbNewLine _
                                            & ",PKG_NB                                                            " & vbNewLine _
                                            & ",SUM(ALCTD_NB) / PKG_NB   AS KONSU                                 " & vbNewLine _
                                            & ",SUM(ALCTD_NB) % PKG_NB   AS HASU                                  " & vbNewLine _
                                            & ",SUM(ALCTD_NB)            AS ALCTD_NB                              " & vbNewLine _
                                            & ",PKG_UT                                                            " & vbNewLine _
                                            & ",PKG_UT_NM                                                         " & vbNewLine _
                                            & ",SUM(ALCTD_QT)            AS ALCTD_QT                              " & vbNewLine _
                                            & ",IRIME_UT                                                          " & vbNewLine _
                                            & ",WT                       AS WT                                    " & vbNewLine _
                                            & ",UNSO_WT                                                           " & vbNewLine _
                                            & ",'' AS TOU_NO                                                      " & vbNewLine _
                                            & ",'' AS SITU_NO                                                     " & vbNewLine _
                                            & ",'' AS ZONE_CD                                                     " & vbNewLine _
                                            & ",'' AS LOCA                                                        " & vbNewLine _
                                            & ",'' AS ZAN_KONSU                                                   " & vbNewLine _
                                            & ",'' AS ZAN_HASU                                                    " & vbNewLine _
                                            & ",'' AS ZAN_HASU                                                    " & vbNewLine _
                                            & ",GOODS_CD_CUST                                                     " & vbNewLine _
                                            & ",REMARK_M                                                          " & vbNewLine _
                                            & ",CUST_NM_S                                                         " & vbNewLine _
                                            & ",CUST_NM_S_H                                                       " & vbNewLine _
                                            & ",CUST_ORD_NO_DTL                                                   " & vbNewLine _
                                            & ",OUTKA_REMARK                                                      " & vbNewLine _
                                            & ",HAISOU_REMARK                                                     " & vbNewLine _
                                            & ",NHS_REMARK                                                        " & vbNewLine _
                                            & ",KYORI                                                             " & vbNewLine _
                                            & ",OUTKA_PKG_NB_L                                                    " & vbNewLine _
                                            & ",SOKO_NM                                                           " & vbNewLine _
                                            & ",BUSHO_NM                                                          " & vbNewLine _
                                            & ",NRS_BR_NM_NICHI                                                   " & vbNewLine _
                                            & ",'' AS ALCTD_CAN_QT                                                " & vbNewLine _
                                            & ",ALCTD_KB                                                          " & vbNewLine _
                                            & ",PTN_FLAG                                                          " & vbNewLine _
                                            & ",CUST_CD_L                                                         " & vbNewLine _
                                            & ",ORDER_TYPE                       AS ORDER_TYPE                    " & vbNewLine _
                                            & ",CASE WHEN URIG_NM <> ''                                           " & vbNewLine _
                                            & "           AND URIG_NM IS NOT NULL THEN URIG_NM                    " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                           " & vbNewLine _
                                            & "      WHEN DEST_SALES_NM   <> ''                                   " & vbNewLine _
                                            & "           AND DEST_SALES_NM IS NOT NULL THEN DEST_SALES_NM        " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                           " & vbNewLine _
                                            & "      WHEN DENPYO_NM   <> ''                                       " & vbNewLine _
                                            & "           AND DENPYO_NM IS NOT NULL   THEN DENPYO_NM              " & vbNewLine _
                                            & "      ELSE CUST_NM_L                                               " & vbNewLine _
                                            & " END                         AS ATSUKAISYA_NM                      " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　START                 " & vbNewLine _
                                            & ",PC_KB_NM                                                          " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　 END                  " & vbNewLine _
                                            & "--【要望番号1017】 2012/04/24　注文番号明細  START                 " & vbNewLine _
                                            & ",BUYER_ORD_NO_DTL                                                  " & vbNewLine _
                                            & "--【要望番号1017】　2012/04/17 注文番号明細　 END                  " & vbNewLine _
                                            & "--日医工対策開始                                                   " & vbNewLine _
                                            & ",TORI_KB   --20120802 群馬篠崎運送でも利用。(FREE_C01)             " & vbNewLine _
                                            & "--★追加開始2012/05/11                                             " & vbNewLine _
                                            & ",SHIP_CD_L                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                             " & vbNewLine _
                                            & "--★削除開始2012/05/11                                             " & vbNewLine _
                                            & "--,CHOKUSO_NM                                                      " & vbNewLine _
                                            & "--,CHOKUSO_CD                                                      " & vbNewLine _
                                            & "--,HAKKO_DATE                                                      " & vbNewLine _
                                            & "--★削除終了2012/05/11                                             " & vbNewLine _
                                            & ",SET_NAIYO_4                                                       " & vbNewLine _
                                            & ",SET_NAIYO_5                                                       " & vbNewLine _
                                            & ",SET_NAIYO_6                                                       " & vbNewLine _
                                            & ",GOODS_NM_2                                                        " & vbNewLine _
                                            & ",KICHO_KB                                                          " & vbNewLine _
                                            & ",SEI_YURAI_KB                                                      " & vbNewLine _
                                            & ",REMARK_UPPER                                                      " & vbNewLine _
                                            & ",REMARK_LOWER                                                      " & vbNewLine _
                                            & ",GOODS_SYUBETU                                                     " & vbNewLine _
                                            & ",YUKOU_KIGEN                                                       " & vbNewLine _
                                            & ",GOODS_NM_3    --2012/05/23                                        " & vbNewLine _
                                            & ",EDISHIP_CD    --2012/05/23                                        " & vbNewLine _
                                            & ",EDIDEST_CD    --2012/05/23                                        " & vbNewLine _
                                            & "--日医工対策終了                                                   " & vbNewLine _
                                            & "--(2012.06.01)日本ファイン対応 --- START ---                       " & vbNewLine _
                                            & ",SET_NAIYO_FROM1                                                   " & vbNewLine _
                                            & ",SET_NAIYO_FROM2                                                   " & vbNewLine _
                                            & ",SET_NAIYO_FROM3                                                   " & vbNewLine _
                                            & "--(2012.06.01)日本ファイン対応 ---  END  ---                       " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                           " & vbNewLine _
                                            & ",CASE WHEN DEST_SALES_CD <> ''                                     " & vbNewLine _
                                            & "           AND DEST_SALES_CD IS NOT NULL THEN DEST_SALES_AD_3      " & vbNewLine _
                                            & "      ELSE CUST_AD_3 END CUST_AD_3                                 " & vbNewLine _
                                            & ",DEST_REMARK                                                       " & vbNewLine _
                                            & ",DEST_SALES_CD                                                     " & vbNewLine _
                                            & ",DEST_SALES_NM                                                     " & vbNewLine _
                                            & ",DEST_SALES_AD_1                                                   " & vbNewLine _
                                            & ",DEST_SALES_AD_2                                                   " & vbNewLine _
                                            & ",DEST_SALES_AD_3                                                   " & vbNewLine _
                                            & ",DEST_SALES_TEL                                                    " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                           " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 開始20120731            " & vbNewLine _
                                            & ",SAGYO_RYAK_1                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_2                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_3                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_4                                                      " & vbNewLine _
                                            & ",SAGYO_RYAK_5                                                      " & vbNewLine _
                                            & ",DEST_SAGYO_RYAK_1                                                 " & vbNewLine _
                                            & ",DEST_SAGYO_RYAK_2                                                 " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　開始　                             " & vbNewLine _
                                            & ",SZ01_YUSO                                                         " & vbNewLine _
                                            & ",SZ01_UNSO                                                         " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　終了　                             " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731            " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                 " & vbNewLine _
                                            & ",HAISOU_REMARK_MST                                                 " & vbNewLine _
                                            & ",GOODS_NM_CUST                                                     " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                 " & vbNewLine _
                                            & "--2016/1/20 LMC815 LMC815 add start                                " & vbNewLine _
                                            & ",GODO_GOODS_CODE                                                   " & vbNewLine _
                                            & "--2016/1/20 LMC815 LMC815 add end                                  " & vbNewLine _
                                            & "FROM                                                               " & vbNewLine _
                                            & "(                                                                  " & vbNewLine _
                                            & "--★★★SQL_SELECT_OUTKA★★★  出荷テーブル                                                                                    " & vbNewLine _
                                             & "SELECT                                                                                                                         " & vbNewLine _
                                             & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                              " & vbNewLine _
                                             & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                              " & vbNewLine _
                                             & "      ELSE MR3.RPT_ID                                                                                                          " & vbNewLine _
                                             & " END              AS RPT_ID                                                                                                    " & vbNewLine _
                                             & ",OUTL.NRS_BR_CD   AS NRS_BR_CD                                                                                                 " & vbNewLine _
                                             & ",OUTM.PRINT_SORT  AS PRINT_SORT                                                                                                " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_CD                                                                               " & vbNewLine _
                                             & "      ELSE OUTL.DEST_CD                                                                                                        " & vbNewLine _
                                             & " END              AS DEST_CD                                                                                                   " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_NM                                                                               " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_NM                                                                               " & vbNewLine _
                                             & "      ELSE MDOUT.DEST_NM                                                                                                       " & vbNewLine _
                                             & " END              AS DEST_NM                                                                                                   " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_1                                                                             " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_1                                                                   " & vbNewLine _
                                             & "      ELSE MDOUT.AD_1                                                                                                          " & vbNewLine _
                                             & " END              AS DEST_AD_1                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN OUTL.DEST_KB = '01' THEN OUTL.DEST_AD_2                                                                             " & vbNewLine _
                                             & "      WHEN OUTL.DEST_KB = '02' THEN EDIL.DEST_AD_2                                                                   " & vbNewLine _
                                             & "      ELSE MDOUT.AD_2                                                                                                          " & vbNewLine _
                                             & " END              AS DEST_AD_2                                                                                                 " & vbNewLine _
                                             & ",OUTL.DEST_AD_3   AS DEST_AD_3                                                                                                 " & vbNewLine _
                                             & ",OUTL.DEST_TEL    AS DEST_TEL                                                                                                  " & vbNewLine _
                                             & ",OUTL.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE                                                                                     " & vbNewLine _
                                             & ",OUTL.CUST_ORD_NO       AS CUST_ORD_NO                                                                                         " & vbNewLine _
                                             & ",OUTL.BUYER_ORD_NO      AS BUYER_ORD_NO                                                                                        " & vbNewLine _
                                             & ",MUCO.UNSOCO_NM         AS UNSOCO_NM                                                                                           " & vbNewLine _
                                             & ",OUTL.ARR_PLAN_DATE     AS ARR_PLAN_DATE                                                                                       " & vbNewLine _
                                             & ",KBN1.KBN_NM1           AS ARR_PLAN_TIME                                                                                       " & vbNewLine _
                                             & ",OUTL.OUTKA_NO_L        AS OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & ",MDOUTU.DEST_NM         AS URIG_NM                                                                                             " & vbNewLine _
                                             & ",MC.CUST_NM_L           AS CUST_NM_L                                                                                           " & vbNewLine _
                                             & "--(2012.06.15) 要望番号1155 -- START --                                                                                        " & vbNewLine _
                                             & ",MC.CUST_NM_M           AS CUST_NM_M                                                                                           " & vbNewLine _
                                             & "--(2012.06.15) 要望番号1155 --  END  --                                                                                        " & vbNewLine _
                                             & ",MC.AD_1                AS CUST_AD_1                                                                                           " & vbNewLine _
                                             & ",MC.AD_2                AS CUST_AD_2                                                                                           " & vbNewLine _
                                             & ",MC.TEL                 AS CUST_TEL                                                                                            " & vbNewLine _
                                             & ",MC.DENPYO_NM           AS DENPYO_NM                                                                                            " & vbNewLine _
                                             & ",MNRS.NRS_BR_NM         AS NRS_BR_NM                                                                                           " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.AD_1                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.AD_1                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_AD1                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.AD_2                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.AD_2                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_AD2                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.TEL                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.TEL                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_TEL                                                                                                 " & vbNewLine _
                                             & ",CASE WHEN MSO.WH_KB = '01' THEN MSO.FAX                                                                             " & vbNewLine _
                                             & "      ELSE MNRS.FAX                                                                                                          " & vbNewLine _
                                             & " END              AS NRS_BR_FAX                                                                                                 " & vbNewLine _
                                             & ",MUSER.USER_NM          AS PIC                                                                                                 " & vbNewLine _
                                             & ",OUTM.OUTKA_NO_M        AS OUTKA_NO_M                                                                                          " & vbNewLine _
                                             & ",OUTS.OUTKA_NO_S        AS OUTKA_NO_S                                                                                          " & vbNewLine _
                                             & ",CASE WHEN MGD.SET_NAIYO = '1' THEN ''                                                                                         " & vbNewLine _
                                             & "      WHEN INL.INKA_STATE_KB < '50' THEN INL.INKA_DATE                                                                         " & vbNewLine _
                                             & "      WHEN INL.INKA_STATE_KB >= '50' THEN ZAI.INKO_DATE                                                                        " & vbNewLine _
                                             & "      ELSE ''                                                                                                                  " & vbNewLine _
                                             & " END                    AS INKA_DATE                                                                                           " & vbNewLine _
                                             & ",ZAI.REMARK_OUT         AS REMARK_OUT                                                                                          " & vbNewLine _
                                             & ",CASE WHEN (MDGE.GOODS_NM IS NOT NULL AND MDGE.GOODS_NM <> '' AND EDIL.OUTKA_CTL_NO IS NOT NULL) THEN MDGE.GOODS_NM            " & vbNewLine _
                                             & "      WHEN (MDGL.GOODS_NM IS NOT NULL AND MDGL.GOODS_NM <> '' AND EDIL.OUTKA_CTL_NO IS NULL) THEN MDGL.GOODS_NM                " & vbNewLine _
                                             & "      ELSE MG.GOODS_NM_1                                                                                                       " & vbNewLine _
                                             & " END                    AS GOODS_NM_1                                                                                          " & vbNewLine _
                                             & ",OUTS.LOT_NO            AS LOT_NO                                                                                              " & vbNewLine _
                                             & ",OUTS.SERIAL_NO         AS SERIAL_NO                                                                                           " & vbNewLine _
                                             & ",OUTS.IRIME             AS IRIME                                                                                               " & vbNewLine _
                                             & ",MG.PKG_NB              AS PKG_NB                                                                                              " & vbNewLine _
                                             & ",OUTS.ALCTD_NB / MG.PKG_NB     AS KONSU                                                                                        " & vbNewLine _
                                             & ",OUTS.ALCTD_NB % MG.PKG_NB     AS HASU                                                                                         " & vbNewLine _
                                             & ",OUTS.ALCTD_NB          AS ALCTD_NB                                                                                            " & vbNewLine _
                                             & ",MG.PKG_UT              AS PKG_UT                                                                                              " & vbNewLine _
                                             & ",KBN5.KBN_NM1           AS PKG_UT_NM                                                                                           " & vbNewLine _
                                             & ",OUTS.ALCTD_QT          AS ALCTD_QT                                                                                            " & vbNewLine _
                                             & ",MG.STD_IRIME_UT        AS IRIME_UT                                                                                            " & vbNewLine _
                                             & "--(2012.10.01)要望番号1445 -- START --                                                                                         " & vbNewLine _
                                             & ",UL.UNSO_WT             AS WT                                                                                                  " & vbNewLine _
                                             & "--,OUTS.BETU_WT * OUTS.ALCTD_NB  AS WT                                                                                         " & vbNewLine _
                                             & "--(2012.10.01)要望番号1445 --  END  --                                                                                         " & vbNewLine _
                                             & ",0                      AS UNSO_WT                                                                                             " & vbNewLine _
                                             & ",OUTS.TOU_NO            AS TOU_NO                                                                                              " & vbNewLine _
                                             & ",OUTS.SITU_NO           AS SITU_NO                                                                                             " & vbNewLine _
                                             & ",RTRIM(OUTS.ZONE_CD)     AS ZONE_CD                                                                                             " & vbNewLine _
                                             & ",OUTS.LOCA              AS LOCA                                                                                                " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB / MG.PKG_NB   AS ZAN_KONSU                                                                                  " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB % MG.PKG_NB   AS ZAN_HASU                                                                                   " & vbNewLine _
                                             & ",MG.GOODS_CD_CUST       AS GOODS_CD_CUST                                                                                       " & vbNewLine _
                                             & ",OUTM.REMARK            AS REMARK_M                                                                                            " & vbNewLine _
                                             & "--★★★変更開始--------------------------------                                                                                     " & vbNewLine _
                                             & ",''   AS CUST_NM_S                                                                                           " & vbNewLine _
                                             & ",''   AS CUST_NM_S_H                                                                                           " & vbNewLine _
                                             & "--★★★変更終了--------------------------------                                                                                     " & vbNewLine _
                                             & ",OUTM.CUST_ORD_NO_DTL   AS CUST_ORD_NO_DTL                                                                                     " & vbNewLine _
                                             & ",OUTL.REMARK            AS OUTKA_REMARK                                                                                        " & vbNewLine _
                                             & ",UL.REMARK              AS HAISOU_REMARK                                                                                       " & vbNewLine _
                                             & ",OUTL.NHS_REMARK        AS NHS_REMARK                                                                                          " & vbNewLine _
                                             & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  --- START ---                                             " & vbNewLine _
                                             & "--,CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                                        " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD < MSO.JIS_CD) THEN MKY3.KYORI                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD < EDIL.DEST_JIS_CD) THEN MKY4.KYORI                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                                       " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS < MSO.JIS_CD) THEN MKY1.KYORI                             " & vbNewLine _
                                             & "--      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD < MDOUT.JIS) THEN MKY2.KYORI                             " & vbNewLine _
                                             & "--      ELSE 0                                                                                                                 " & vbNewLine _
                                             & "-- END                  AS KYORI                                                                                               " & vbNewLine _
                                             & ",CASE WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI > 0) THEN MDEDI.KYORI                                                          " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND EDIL.DEST_JIS_CD <= MSO.JIS_CD) THEN MKY3.KYORI                        " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB = '02' AND MDEDI.KYORI = 0 AND MSO.JIS_CD <= EDIL.DEST_JIS_CD) THEN MKY4.KYORI                        " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI > 0) THEN MDOUT.KYORI                                                         " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MDOUT.JIS <= MSO.JIS_CD) THEN MKY1.KYORI                              " & vbNewLine _
                                             & "      WHEN (OUTL.DEST_KB <> '02' AND MDOUT.KYORI = 0 AND MSO.JIS_CD <= MDOUT.JIS) THEN MKY2.KYORI                              " & vbNewLine _
                                             & "      ELSE 0                                                                                                                   " & vbNewLine _
                                             & " END                    AS KYORI                                                                                               " & vbNewLine _
                                             & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  ---  END  ---                                             " & vbNewLine _
                                             & ",OUTL.OUTKA_PKG_NB      AS OUTKA_PKG_NB_L                                                                                      " & vbNewLine _
                                             & ",KBN2.KBN_NM1           AS SOKO_NM                                                                                             " & vbNewLine _
                                             & ",KBN3.KBN_NM1           AS BUSHO_NM                                                                                            " & vbNewLine _
                                             & ",KBN4.KBN_NM1           AS NRS_BR_NM_NICHI                                                                                     " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_QT      AS ALCTD_CAN_QT                                                                                        " & vbNewLine _
                                             & ",OUTM.ALCTD_KB          AS ALCTD_KB                                                                                            " & vbNewLine _
                                             & ",'0'                    AS PTN_FLAG                                                                                            " & vbNewLine _
                                             & ",OUTL.CUST_CD_L         AS CUST_CD_L                                                                                           " & vbNewLine _
                                             & ",OUTL.ORDER_TYPE        AS ORDER_TYPE                                                                                          " & vbNewLine _
                                             & ",ZAI.LT_DATE                                                                                                                   " & vbNewLine _
                                             & ",OUTM.BUYER_ORD_NO_DTL AS BUYER_ORD_NO_DTL                                                                                     " & vbNewLine _
                                             & ",ZAI.GOODS_COND_KB_2 AS GOODS_COND_CD                                                                                          " & vbNewLine _
                                             & ",ZAI.REMARK AS REMARK_ZAI                                                                                                      " & vbNewLine _
                                             & ",KBN6.KBN_NM2 AS TITLE_SMPL                                                                                                    " & vbNewLine _
                                             & ",OUTS.ALCTD_CAN_NB AS ALCTD_CAN_NB                                                                                             " & vbNewLine _
                                             & ",OUTM.BACKLOG_QT                                                                                                               " & vbNewLine _
                                             & ",KBN7.KBN_NM2 AS DOKU_NM                                                                                                       " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                             " & vbNewLine _
                                             & ",KBN9.KBN_NM2 AS PC_KB_NM                                                                                                      " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                             & "--日医工対策開始                                                                                                               " & vbNewLine _
                                             & ",EDIL.FREE_C01           AS TORI_KB                                                                                            " & vbNewLine _
                                             & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                             & ",OUTL.SHIP_CD_L          AS SHIP_CD_L                                                                                          " & vbNewLine _
                                             & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                             & "--★削除開始2012/05/11                                                                                                         " & vbNewLine _
                                             & "--,EDIL.SHIP_NM_L          AS CHOKUSO_NM1                                                                                      " & vbNewLine _
                                             & "--,EDIL.FREE_C11           AS CHOKUSO_CD                                                                                       " & vbNewLine _
                                             & "--,EDIL.FREE_C08           AS HAKKO_DATE                                                                                       " & vbNewLine _
                                             & "--★削除終了2012/05/11                                                                                                         " & vbNewLine _
                                             & ",MCD_N.SET_NAIYO         AS SET_NAIYO_4                                                                                        " & vbNewLine _
                                             & ",MCD_N.SET_NAIYO_2       AS SET_NAIYO_5                                                                                        " & vbNewLine _
                                             & ",MCD_N.SET_NAIYO_3       AS SET_NAIYO_6                                                                                        " & vbNewLine _
                                             & ",MG.GOODS_NM_2           AS GOODS_NM_2                                                                                         " & vbNewLine _
                                             & ",EDIM.FREE_C02           AS KICHO_KB                                                                                           " & vbNewLine _
                                             & ",EDIM.FREE_C03           AS SEI_YURAI_KB                                                                                       " & vbNewLine _
                                             & ",EDIM.FREE_C04           AS REMARK_UPPER      --群馬篠崎運送利用                                                               " & vbNewLine _
                                             & ",EDIM.FREE_C05           AS REMARK_LOWER                                                                                       " & vbNewLine _
                                             & ",EDIM.FREE_C06           AS GOODS_SYUBETU     --群馬篠崎運送利用                                                               " & vbNewLine _
                                             & ",EDIM.FREE_C01           AS YUKOU_KIGEN                                                                                        " & vbNewLine _
                                             & ",MG.GOODS_NM_3           AS GOODS_NM_3         --2012/05/23                                                                    " & vbNewLine _
                                             & ",MDOUTU.EDI_CD           AS EDISHIP_CD         --2012/05/23                                                                    " & vbNewLine _
                                             & ",MDOUT.EDI_CD            AS EDIDEST_CD         --2012/05/23                                                                    " & vbNewLine _
                                             & "--日医工対策終了                                                                                                               " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ---                                                                                     " & vbNewLine _
                                             & ",MCD_F.SET_NAIYO         AS SET_NAIYO_FROM1                                                                                    " & vbNewLine _
                                             & ",MCD_F.SET_NAIYO_2       AS SET_NAIYO_FROM2                                                                                    " & vbNewLine _
                                             & ",MCD_F.SET_NAIYO_3       AS SET_NAIYO_FROM3                                                                                    " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ---                                                                                     " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                             & ",MC.AD_3                 AS CUST_AD_3                                                                                          " & vbNewLine _
                                             & ",MDOUT.REMARK            AS DEST_REMARK                                                                                        " & vbNewLine _
                                             & ",MDOUT.SALES_CD          AS DEST_SALES_CD                                                                                      " & vbNewLine _
                                             & ",MC_SALES.CUST_NM_L      AS DEST_SALES_NM                                                                                      " & vbNewLine _
                                             & ",MC_SALES.AD_1           AS DEST_SALES_AD_1                                                                                    " & vbNewLine _
                                             & ",MC_SALES.AD_2           AS DEST_SALES_AD_2                                                                                    " & vbNewLine _
                                             & ",MC_SALES.AD_3           AS DEST_SALES_AD_3                                                                                    " & vbNewLine _
                                             & ",MC_SALES.TEL            AS DEST_SALES_TEL                                                                                     " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                             & "--群馬(合算)端数行分割版向け対応(作業略称) 開始20120731                                                                        " & vbNewLine _
                                             & "--1ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 1),'') AS SAGYO_RYAK_1                                                                                        " & vbNewLine _
                                             & "--1ここまで                                                                                                                    " & vbNewLine _
                                             & "--2ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 2),'') AS SAGYO_RYAK_2                                                                                        " & vbNewLine _
                                             & "--2ここまで                                                                                                                    " & vbNewLine _
                                             & "--3ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 3),'') AS SAGYO_RYAK_3                                                                                        " & vbNewLine _
                                             & "--3ここまで                                                                                                                    " & vbNewLine _
                                             & "--4ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 4),'') AS SAGYO_RYAK_4                                                                                        " & vbNewLine _
                                             & "--4ここまで                                                                                                                    " & vbNewLine _
                                             & "--5ここから                                                                                                                    " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 5),'') AS SAGYO_RYAK_5                                                                                        " & vbNewLine _
                                             & "--5ここまで                                                                                                                    " & vbNewLine _
                                             & "--届け先1ここから                                                                                                              " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 6),'') AS DEST_SAGYO_RYAK_1                                                                                   " & vbNewLine _
                                             & "--届け先1ここまで                                                                                                              " & vbNewLine _
                                             & "--届け先2ここから                                                                                                              " & vbNewLine _
                                             & ",ISNULL((SELECT BASE.SAGYO_RYAK                                                                                                " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "(SELECT                                                                                                                        " & vbNewLine _
                                             & "M_SAGYO.SAGYO_RYAK AS SAGYO_RYAK                                                                                               " & vbNewLine _
                                             & ",ROW_NUMBER() OVER (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM) AS NUM                                                  " & vbNewLine _
                                             & "FROM $LM_TRN$..E_SAGYO E                                                                                                       " & vbNewLine _
                                             & "       JOIN $LM_MST$..M_SAGYO M_SAGYO                                                                                          " & vbNewLine _
                                             & "         ON M_SAGYO.NRS_BR_CD = E.NRS_BR_CD                                                                                    " & vbNewLine _
                                             & "        AND M_SAGYO.SAGYO_CD  = E.SAGYO_CD                                                                                     " & vbNewLine _
                                             & "WHERE E.NRS_BR_CD = OUTL.NRS_BR_CD AND E.INOUTKA_NO_LM = (OUTM.OUTKA_NO_L + OUTM.OUTKA_NO_M) AND E.IOZS_KB = '21'              " & vbNewLine _
                                             & ") AS BASE                                                                                                                      " & vbNewLine _
                                             & "WHERE BASE.NUM = 7),'') AS DEST_SAGYO_RYAK_2                                                                                   " & vbNewLine _
                                             & "--届け先ここまで                                                                                                               " & vbNewLine _
                                             & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731                                                                        " & vbNewLine _
                                             & "--群馬篠崎運送向け自由項目対応　開始　                                                                                         " & vbNewLine _
                                             & ",EDIM.FREE_C07           AS SZ01_YUSO --篠崎                                                                                   " & vbNewLine _
                                             & ",EDIM.FREE_C09           AS SZ01_UNSO --篠崎                                                                                   " & vbNewLine _
                                             & " --2015/1/14                                                                                                                   " & vbNewLine _
                                             & "  , MDOUT.DELI_ATT       AS  HAISOU_REMARK_MST                                                                                 " & vbNewLine _
                                             & "  , MGD43.REMARK         AS  GOODS_NM_CUST                                                                                     " & vbNewLine _
                                             & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                             & "--群馬篠崎運送向け自由項目対応　終了                                                                                         　" & vbNewLine _
                                             & "--20160120 ゴードー add start                                                                                         　       " & vbNewLine _
                                             & "  , MGDETAILS.SET_NAIYO  AS GODO_GOODS_CODE                                                                                    " & vbNewLine _
                                             & "--20160120 ゴードー add end                                                                                         　         " & vbNewLine _
                                             & "--出荷L                                                                                                                        " & vbNewLine _
                                             & "FROM                                                                                                                           " & vbNewLine _
                                             & "$LM_TRN$..C_OUTKA_L OUTL                                                                                                       " & vbNewLine _
                                             & "--トランザクションテーブル                                                                                                     " & vbNewLine _
                                             & "--出荷M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                                             " & vbNewLine _
                                             & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND OUTM.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                             & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                             & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                             & "       (SELECT                                                                                                                 " & vbNewLine _
                                             & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                             & "           ,OUTKA_NO_L                                                                                                         " & vbNewLine _
                                             & "           ,MIN(OUTKA_NO_M) AS  OUTKA_NO_M                                                                                     " & vbNewLine _
                                             & "       FROM $LM_TRN$..C_OUTKA_M WHERE SYS_DEL_FLG ='0'                                                                         " & vbNewLine _
                                             & "        AND NRS_BR_CD = @NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "        AND OUTKA_NO_L = @KANRI_NO_L                                                                                           " & vbNewLine _
                                             & "       GROUP BY NRS_BR_CD,OUTKA_NO_L) OUTM_MIN                                                                                 " & vbNewLine _
                                             & "       ON OUTM_MIN.NRS_BR_CD        = OUTL.NRS_BR_CD                                                                           " & vbNewLine _
                                             & "       AND OUTM_MIN.OUTKA_NO_L      = OUTL.OUTKA_NO_L                                                                          " & vbNewLine _
                                             & "--出荷M(中MIN)                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM2                                                                                            " & vbNewLine _
                                             & "ON  OUTM2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND OUTM2.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                         " & vbNewLine _
                                             & "AND OUTM2.OUTKA_NO_M = OUTM_MIN.OUTKA_NO_M                                                                                     " & vbNewLine _
                                             & "AND OUTM2.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                             & "--出荷S                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                                             " & vbNewLine _
                                             & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                                          " & vbNewLine _
                                             & "AND OUTS.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--出荷EDIL                                                                                                                     " & vbNewLine _
                                             & "--(2012.09.06)要望番号1412対応  --- START ---                                                                                  " & vbNewLine _
                                             & "--LEFT JOIN                                                                                                                    " & vbNewLine _
                                             & "--(                                                                                                                            " & vbNewLine _
                                             & "--SELECT                                                                                                                       " & vbNewLine _
                                             & "-- --20120730 MIN化　開始(20120803取りやめ)                                                                                    " & vbNewLine _
                                             & "-- --MIN(NRS_BR_CD)    AS NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "-- --,MIN(OUTKA_CTL_NO) AS OUTKA_CTL_NO                                                                                        " & vbNewLine _
                                             & "-- --,MIN(CUST_CD_L)    AS CUST_CD_L                                                                                           " & vbNewLine _
                                             & "-- NRS_BR_CD                                                                                                                   " & vbNewLine _
                                             & "--,OUTKA_CTL_NO                                                                                                                " & vbNewLine _
                                             & "--,CUST_CD_L                                                                                                                   " & vbNewLine _
                                             & "-- --20120730 MIN化　終了                                                                                                      " & vbNewLine _
                                             & "----★削除開始2012/05/11                                                                                                       " & vbNewLine _
                                             & "----,SHIP_NM_L                                                                                                                 " & vbNewLine _
                                             & "----★削除終了2012/05/11                                                                                                       " & vbNewLine _
                                             & "--,MIN(DEST_CD)     AS DEST_CD                                                                                                 " & vbNewLine _
                                             & "--,MIN(DEST_NM)     AS DEST_NM                                                                                                 " & vbNewLine _
                                             & "--,MIN(DEST_AD_1)   AS DEST_AD_1                                                                                               " & vbNewLine _
                                             & "--,MIN(DEST_AD_2)   AS DEST_AD_2                                                                                               " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出 -- START --                                                             " & vbNewLine _
                                             & "----,DEST_JIS_CD                                                                                                               " & vbNewLine _
                                             & "--,MIN(DEST_JIS_CD) AS DEST_JIS_CD                                                                                             " & vbNewLine _
                                             & "----(2012.04.11)【Notes】№923  DEST_JIS_CDはMINで抽出 --  END  --                                                             " & vbNewLine _
                                             & "-- --20120730 MIN化　開始                                                                                                      " & vbNewLine _
                                             & "--,MIN(FREE_C01)   AS FREE_C01                                                                                                 " & vbNewLine _
                                             & "--,MIN(FREE_C08)   AS FREE_C08                                                                                                 " & vbNewLine _
                                             & "--,MIN(FREE_C11)   AS FREE_C11                                                                                                 " & vbNewLine _
                                             & "-- --20120730 MIN化　終了                                                                                                      " & vbNewLine _
                                             & "--,SYS_DEL_FLG                                                                                                                 " & vbNewLine _
                                             & "--FROM                                                                                                                         " & vbNewLine _
                                             & "--$LM_TRN$..H_OUTKAEDI_L                                                                                                       " & vbNewLine _
                                             & "--GROUP BY                                                                                                                     " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　開始 (20120803取りやめ)                                                               " & vbNewLine _
                                             & "-- NRS_BR_CD                                                                                                                   " & vbNewLine _
                                             & "--,OUTKA_CTL_NO                                                                                                                " & vbNewLine _
                                             & "--,CUST_CD_L                                                                                                                   " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　終了                                                                                  " & vbNewLine _
                                             & "----★削除開始2012/05/11                                                                                                       " & vbNewLine _
                                             & "----,SHIP_NM_L                                                                                                                 " & vbNewLine _
                                             & "----★削除開始2012/05/11                                                                                                       " & vbNewLine _
                                             & "----(2012.04.11) Notes№923  DEST_JIS_CDはMINで抽出のためコメント -- START --                                                  " & vbNewLine _
                                             & "----,DEST_JIS_CD                                                                                                               " & vbNewLine _
                                             & "----(2012.04.11) Notes№923  DEST_JIS_CDはMINで抽出のためコメント --  END  --                                                  " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　開始                                                                                  " & vbNewLine _
                                             & "----,FREE_C01                                                                                                                  " & vbNewLine _
                                             & "----,FREE_C08                                                                                                                  " & vbNewLine _
                                             & "----,FREE_C11                                                                                                                  " & vbNewLine _
                                             & "-- --20120730 MIN化によるコメントアウト　終了 ↓SYS_DEL_FLGのカンマ除去↓                                                      " & vbNewLine _
                                             & "--,SYS_DEL_FLG                                                                                                                 " & vbNewLine _
                                             & "--) EDIL                                                                                                                       " & vbNewLine _
                                             & "--ON  EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                          " & vbNewLine _
                                             & "--下記の内容に変更                                                                                                             " & vbNewLine _
                                             & " LEFT JOIN (                                                                                                                   " & vbNewLine _
                                             & "            SELECT                                                                                                             " & vbNewLine _
                                             & "                   NRS_BR_CD                                                                                                   " & vbNewLine _
                                             & "                 , EDI_CTL_NO                                                                                                  " & vbNewLine _
                                             & "                 , OUTKA_CTL_NO                                                                                                " & vbNewLine _
                                             & "             FROM (                                                                                                            " & vbNewLine _
                                             & "                    SELECT                                                                                                     " & vbNewLine _
                                             & "                           EDIOUTL.NRS_BR_CD                                                                                   " & vbNewLine _
                                             & "                         , EDIOUTL.EDI_CTL_NO                                                                                  " & vbNewLine _
                                             & "                         , EDIOUTL.OUTKA_CTL_NO                                                                                " & vbNewLine _
                                             & "                         , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                                                          " & vbNewLine _
                                             & "                           ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                                                              , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
                                             & "                                                       ORDER BY EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
                                             & "                                                              , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
                                             & "                                                  )                                                                            " & vbNewLine _
                                             & "                           END AS IDX                                                                                          " & vbNewLine _
                                             & "                     FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                                                       " & vbNewLine _
                                             & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                                                           " & vbNewLine _
                                             & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                                                                    " & vbNewLine _
                                             & "                      AND EDIOUTL.OUTKA_CTL_NO = @KANRI_NO_L                                                                   " & vbNewLine _
                                             & "                  ) EBASE                                                                                                      " & vbNewLine _
                                             & "            WHERE EBASE.IDX = 1                                                                                                " & vbNewLine _
                                             & "            ) TOPEDI                                                                                                           " & vbNewLine _
                                             & "        ON TOPEDI.NRS_BR_CD    = OUTL.NRS_BR_CD                                                                                " & vbNewLine _
                                             & "       AND TOPEDI.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                                                                               " & vbNewLine _
                                             & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                                                         " & vbNewLine _
                                             & "        ON EDIL.NRS_BR_CD  = TOPEDI.NRS_BR_CD                                                                                  " & vbNewLine _
                                             & "       AND EDIL.EDI_CTL_NO = TOPEDI.EDI_CTL_NO                                                                                 " & vbNewLine _
                                             & "--(2012.09.06)要望番号1412対応  ---  END  ---                                                                                  " & vbNewLine _
                                             & "--入荷L                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..B_INKA_L INL                                                                                               " & vbNewLine _
                                             & "ON  INL.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND INL.INKA_NO_L = OUTS.INKA_NO_L                                                                                             " & vbNewLine _
                                             & "AND INL.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--入荷S                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..B_INKA_S INS                                                                                               " & vbNewLine _
                                             & "ON  INS.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_L = OUTS.INKA_NO_L                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_M = OUTS.INKA_NO_M                                                                                             " & vbNewLine _
                                             & "AND INS.INKA_NO_S = OUTS.INKA_NO_S                                                                                             " & vbNewLine _
                                             & "AND INS.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--運送L                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..F_UNSO_L UL                                                                                                " & vbNewLine _
                                             & "ON  UL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                          " & vbNewLine _
                                             & "AND UL.MOTO_DATA_KB = '20'                                                                                                     " & vbNewLine _
                                             & "AND UL.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                             & "--在庫テーブル                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI                                                                                              " & vbNewLine _
                                             & "ON  ZAI.NRS_BR_CD = OUTS.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND ZAI.ZAI_REC_NO = OUTS.ZAI_REC_NO                                                                                           " & vbNewLine _
                                             & "AND ZAI.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--マスタテーブル                                                                                                               " & vbNewLine _
                                             & "--営業所M                                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_NRS_BR MNRS                                                                                              " & vbNewLine _
                                             & "ON MNRS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "--商品M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                 " & vbNewLine _
                                             & "ON  MG.NRS_BR_CD = OUTM.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--商品M(MIN)                                                                                                                   " & vbNewLine _
                                             & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                                  " & vbNewLine _
                                             & "ON M_GOODS_MIN.NRS_BR_CD      = OUTL.NRS_BR_CD                                                                                 " & vbNewLine _
                                             & "AND M_GOODS_MIN.GOODS_CD_NRS   = OUTM2.GOODS_CD_NRS                                                                            " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                             & "--荷主M(商品M経由)                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                                  " & vbNewLine _
                                             & "ON  MC.NRS_BR_CD = MG.NRS_BR_CD                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                                                " & vbNewLine _
                                             & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                                              " & vbNewLine _
                                             & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                             & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                             & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                             & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                             & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                             & "--届先M(届先取得)(出荷L参照)                                                                                                   " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDOUT                                                                                               " & vbNewLine _
                                             & "ON  MDOUT.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MDOUT.CUST_CD_L = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MDOUT.DEST_CD = OUTL.DEST_CD                                                                                               " & vbNewLine _
                                             & "--届先M(売上先取得)(出荷L参照)                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDOUTU                                                                                              " & vbNewLine _
                                             & "ON  MDOUTU.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                          " & vbNewLine _
                                             & "AND MDOUTU.CUST_CD_L = OUTL.CUST_CD_L                                                                                          " & vbNewLine _
                                             & "AND MDOUTU.DEST_CD = OUTL.SHIP_CD_L                                                                                            " & vbNewLine _
                                             & "--届先M(届先取得)(出荷EDIL参照)                                                                                                " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DEST MDEDI                                                                                               " & vbNewLine _
                                             & "ON  MDEDI.NRS_BR_CD = EDIL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MDEDI.CUST_CD_L = EDIL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MDEDI.DEST_CD = EDIL.DEST_CD                                                                                               " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                             & "--届先M(納品書荷主名義名取得)(届先M参照)                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST MC_SALES                                                                                            " & vbNewLine _
                                             & "       ON MC_SALES.NRS_BR_CD  = MDOUT.NRS_BR_CD                                                                                " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_L  = MDOUT.SALES_CD                                                                                 " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_M  = '00'                                                                                           " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_S  = '00'                                                                                           " & vbNewLine _
                                             & "      AND MC_SALES.CUST_CD_SS = '00'                                                                                           " & vbNewLine _
                                             & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                             & "--運送会社M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_UNSOCO MUCO                                                                                              " & vbNewLine _
                                             & "ON  MUCO.NRS_BR_CD = UL.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MUCO.UNSOCO_CD = UL.UNSO_CD                                                                                                " & vbNewLine _
                                             & "AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                                                                                          " & vbNewLine _
                                             & "--倉庫M                                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                                                                 " & vbNewLine _
                                             & "ON  MSO.NRS_BR_CD = OUTL.NRS_BR_CD  --追加                                                                                     " & vbNewLine _
                                             & "AND MSO.WH_CD     = OUTL.WH_CD                                                                                                 " & vbNewLine _
                                             & "--距離程M(M_DEST.JIS < M_SOKO.JIS_CD)(出荷L参照)                                                                               " & vbNewLine _
                                             & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                                                                                " & vbNewLine _
                                             & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                                                                        " & vbNewLine _
                                             & "AND UL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                                                                          " & vbNewLine _
                                             & "--2014/5/16End s.kobayashi NotesNo.2183                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY1                                                                                               " & vbNewLine _
                                             & "ON  MKY1.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY1.KYORI_CD = MC.BETU_KYORI_CD                                                                                         " & vbNewLine _
                                             & "AND MKY1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY1.ORIG_JIS_CD = MDOUT.JIS                                                                                               " & vbNewLine _
                                             & "AND MKY1.DEST_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "--距離程M(M_SOKO.JIS_CD < M_DEST.JIS)(出荷L参照)                                                                               " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY2                                                                                               " & vbNewLine _
                                             & "ON  MKY2.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY2.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY2.ORIG_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "AND MKY2.DEST_JIS_CD = MDOUT.JIS                                                                                               " & vbNewLine _
                                             & "--距離程M(H_OUTKAEDI_L.DEST_JIS_CD < M_SOKO.JIS_CD)(出荷EDIL参照)                                                              " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY3                                                                                               " & vbNewLine _
                                             & "ON  MKY3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY3.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY3.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY3.ORIG_JIS_CD = EDIL.DEST_JIS_CD                                                                                        " & vbNewLine _
                                             & "AND MKY3.DEST_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "--距離程M(M_SOKO.JIS_CD < H_OUTKAEDI_L.DEST_JIS_CD)(出荷EDIL参照)                                                              " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_KYORI MKY4                                                                                               " & vbNewLine _
                                             & "ON  MKY4.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "--AND MKY4.KYORI_CD = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                             & "AND MKY4.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                       " & vbNewLine _
                                             & "AND MKY4.ORIG_JIS_CD = MSO.JIS_CD                                                                                              " & vbNewLine _
                                             & "AND MKY4.DEST_JIS_CD = EDIL.DEST_JIS_CD                                                                                        " & vbNewLine _
                                             & "--届先商品M(出荷L参照)                                                                                                         " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DESTGOODS MDGL                                                                                           " & vbNewLine _
                                             & "ON  MDGL.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND MDGL.CUST_CD_L = OUTL.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND MDGL.CUST_CD_M = OUTL.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND MDGL.CD = OUTL.DEST_CD                                                                                                     " & vbNewLine _
                                             & "AND MDGL.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "AND MDGL.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--届先商品M(出荷EDI参照)                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_DESTGOODS MDGE                                                                                           " & vbNewLine _
                                             & "ON  MDGE.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND MDGE.CUST_CD_L = OUTL.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND MDGE.CUST_CD_M = OUTL.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND MDGE.CD = EDIL.DEST_CD                                                                                                     " & vbNewLine _
                                             & "AND MDGE.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                        " & vbNewLine _
                                             & "AND MDGE.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(納入予定区分)                                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                                 " & vbNewLine _
                                             & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                                                 " & vbNewLine _
                                             & "AND KBN1.KBN_CD = OUTL.ARR_PLAN_TIME                                                                                           " & vbNewLine _
                                             & "AND KBN1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用倉庫名)                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                                                 " & vbNewLine _
                                             & "ON  KBN2.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN2.KBN_CD = '00'                                                                                                         " & vbNewLine _
                                             & "AND KBN2.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用部署名)                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                                                 " & vbNewLine _
                                             & "ON  KBN3.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN3.KBN_CD = '01'                                                                                                         " & vbNewLine _
                                             & "AND KBN3.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(日本合成用営業署名)                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN4                                                                                                 " & vbNewLine _
                                             & "ON  KBN4.KBN_GROUP_CD = 'P005'                                                                                                 " & vbNewLine _
                                             & "AND KBN4.KBN_CD = '02'                                                                                                         " & vbNewLine _
                                             & "AND KBN4.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(荷姿)                                                                                                                  " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN5                                                                                                 " & vbNewLine _
                                             & "ON  KBN5.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                             & "AND KBN5.KBN_CD = MG.PKG_UT                                                                                                    " & vbNewLine _
                                             & "AND KBN5.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--明細タイトル（小分け）                                                                                                       " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN6                                                                                                 " & vbNewLine _
                                             & "ON  KBN6.KBN_GROUP_CD = 'S041'                                                                                                 " & vbNewLine _
                                             & "AND KBN6.KBN_CD = OUTM.ALCTD_KB                                                                                                " & vbNewLine _
                                             & "AND KBN6.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--区分M(毒劇区分名称)                                                                                                           " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN7                                                                                                  " & vbNewLine _
                                             & "ON  KBN7.KBN_GROUP_CD = 'G001'                                                                                                  " & vbNewLine _
                                             & "AND KBN7.KBN_CD = MG.DOKU_KB                                                                                                    " & vbNewLine _
                                             & "AND KBN7.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                             " & vbNewLine _
                                             & "--区分M(元着払区分名称)                                                                                                        " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..Z_KBN KBN9                                                                                                 " & vbNewLine _
                                             & "ON  KBN9.KBN_GROUP_CD = 'M001'                                                                                                 " & vbNewLine _
                                             & "AND KBN9.KBN_CD = OUTL.PC_KB                                                                                                   " & vbNewLine _
                                             & "AND KBN9.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                             & "--日医工対策開始                                                                                                               " & vbNewLine _
                                             & "--★出荷EDI M(日医工)                                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                                                                                            " & vbNewLine _
                                             & "ON  EDIM.NRS_BR_CD        = OUTM.NRS_BR_CD                                                                                     " & vbNewLine _
                                             & "AND EDIM.OUTKA_CTL_NO     = OUTM.OUTKA_NO_L                                                                                    " & vbNewLine _
                                             & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                                                                                    " & vbNewLine _
                                             & "AND EDIM.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "--日医工_出荷指示EDIデータ                                                                                                     " & vbNewLine _
                                             & "--LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_NIK EDIN                                                                                    " & vbNewLine _
                                             & "--ON   OUTL.NRS_BR_CD      = EDIN.NRS_BR_CD                        ==                                                              " & vbNewLine _
                                             & "--AND  EDIL.EDI_CTL_NO     = EDIN.EDI_CTL_NO                                                                                     " & vbNewLine _
                                             & "--AND  EDIM.EDI_CTL_NO_CHU = EDIN.EDI_CTL_NO_CHU                                                                                 " & vbNewLine _
                                             & "--AND  EDIN.SYS_DEL_FLG    = '0'                                                                                                 " & vbNewLine _
                                             & "                                                                                                                               " & vbNewLine _
                                             & "--荷主明細M ★                                                                                                                 " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_N                                                                                       " & vbNewLine _
                                             & "ON  MCD_N.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MCD_N.CUST_CD   = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MCD_N.SUB_KB = '26'                                                                                                        " & vbNewLine _
                                             & "--日医工対策終了                                                                                                               " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ----                                                                                    " & vbNewLine _
                                             & "--荷主明細M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_F                                                                                       " & vbNewLine _
                                             & "ON  MCD_F.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                           " & vbNewLine _
                                             & "AND MCD_F.CUST_CD   = OUTL.CUST_CD_L                                                                                           " & vbNewLine _
                                             & "AND MCD_F.SUB_KB = '29'                                                                                                        " & vbNewLine _
                                             & "--(2012.06.01) 日本ﾌｧｲﾝ対応  ---  END  ----                                                                                    " & vbNewLine _
                                             & "--出荷Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                             & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                            " & vbNewLine _
                                             & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                                                            " & vbNewLine _
                                             & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                                                            " & vbNewLine _
                                             & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                             & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                             & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                             & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                             & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                             & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                             & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                             & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                             & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                             & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                             & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                             & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                             & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                             & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                             & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                             & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                             & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                             & "--ユーザM                                                                                                                      " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..S_USER MUSER                                                                                               " & vbNewLine _
                                             & "ON MUSER.USER_CD = OUTL.SYS_ENT_USER                                                                                           " & vbNewLine _
                                             & "AND MUSER.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                             & "--商品明細M                                                                                                                    " & vbNewLine _
                                             & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                                        " & vbNewLine _
                                             & "ON  MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                                               " & vbNewLine _
                                             & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                         " & vbNewLine _
                                             & "AND MGD.SUB_KB = '09'                                                                                                          " & vbNewLine _
                                             & " --商品M LMC757  2015/1/15                                                              " & vbNewLine _
                                             & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD43                                                    " & vbNewLine _
                                             & "	ON  OUTM.NRS_BR_CD = MGD43.NRS_BR_CD                                              " & vbNewLine _
                                             & "	AND OUTM.GOODS_CD_NRS = MGD43.GOODS_CD_NRS                                              " & vbNewLine _
                                             & "	AND OUTL.SHIP_CD_L = MGD43.SET_NAIYO                                              " & vbNewLine _ 
                                             & " AND MGD43.SUB_KB ='43' AND MGD43.SYS_DEL_FLG ='0'                                         " & vbNewLine _
                                             & " --商品M LMC757 2015/1/15                                                                " & vbNewLine _ 
                                             & " --商品詳細マスタ LMC815 20160120 start                                                                " & vbNewLine _ 
                                             & " LEFT OUTER JOIN LM_MST..M_GOODS_DETAILS MGDETAILS                                                                " & vbNewLine _ 
                                             & " ON  OUTL.NRS_BR_CD = MGDETAILS.NRS_BR_CD                                                                " & vbNewLine _ 
                                             & " AND MG.GOODS_CD_NRS = MGDETAILS.GOODS_CD_NRS                                                                " & vbNewLine _ 
                                             & " AND MGDETAILS.SUB_KB = '56'                                                           " & vbNewLine _ 
                                             & " AND MGDETAILS.SYS_DEL_FLG = '0'                                                                              " & vbNewLine _ 
                                             & " --商品詳細マスタ LMC815 20160120 end                                                                " & vbNewLine _ 
                                             & "WHERE                                                                                                                          " & vbNewLine _
                                             & "    OUTL.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                             & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                                                                " & vbNewLine _
                                             & "AND OUTL.OUTKA_NO_L = @KANRI_NO_L                                                                                              " & vbNewLine _
                                             & "--★★★SQL_SELECT_UNSO★★★ 運送テーブル                                                                                    " & vbNewLine _
                                            & "UNION ALL                                                                                                                      " & vbNewLine _
                                            & "SELECT                                                                                                                         " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                              " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                              " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID                                                                                                          " & vbNewLine _
                                            & " END                                                                             AS RPT_ID                                     " & vbNewLine _
                                            & ",UNSOL.NRS_BR_CD                                                                 AS NRS_BR_CD                                  " & vbNewLine _
                                            & ",0                                                                               AS PRINT_SORT                                 " & vbNewLine _
                                            & ",UNSOL.DEST_CD                                                                   AS DEST_CD                                    " & vbNewLine _
                                            & ",MD.DEST_NM                                                                      AS DEST_NM                                    " & vbNewLine _
                                            & ",MD.AD_1                                                                         AS DEST_AD_1                                  " & vbNewLine _
                                            & ",MD.AD_2                                                                         AS DEST_AD_2                                  " & vbNewLine _
                                            & "--(2014.07.02)修正START                                                                                                        " & vbNewLine _
                                            & ",UNSOL.AD_3                                                                      AS DEST_AD_3                                  " & vbNewLine _
                                            & "--,MD.AD_3                                                                         AS DEST_AD_3                                  " & vbNewLine _
                                            & "--(2014.07.02)修正END                                                                                                          " & vbNewLine _
                                            & ",MD.TEL                                                                          AS DEST_TEL                                   " & vbNewLine _
                                            & ",UNSOL.OUTKA_PLAN_DATE                                                           AS OUTKA_PLAN_DATE                            " & vbNewLine _
                                            & ",UNSOL.CUST_REF_NO                                                               AS CUST_ORD_NO                                " & vbNewLine _
                                            & ",UNSOL.BUY_CHU_NO                                                                AS BUYER_ORD_NO                               " & vbNewLine _
                                            & ",UNSOCO.UNSOCO_NM                                                                AS UNSOCO_NM                                  " & vbNewLine _
                                            & ",UNSOL.ARR_PLAN_DATE                                                             AS ARR_PLAN_DATE                              " & vbNewLine _
                                            & ",KBN1.KBN_NM1                                                                    AS ARR_PLAN_TIME                              " & vbNewLine _
                                            & ",CASE WHEN RTRIM(UNSOL.INOUTKA_NO_L) <> '' THEN UNSOL.INOUTKA_NO_L                                                             " & vbNewLine _
                                            & "      ELSE UNSOL.UNSO_NO_L                                                                                                     " & vbNewLine _
                                            & " END                                                                             AS OUTKA_NO_L                                 " & vbNewLine _
                                            & ",MDU.DEST_NM                                                                     AS URIG_NM                                    " & vbNewLine _
                                            & ",MC.CUST_NM_L                                                                    AS CUST_NM_L                                  " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 -- START --                                                                                        " & vbNewLine _
                                            & ",MC.CUST_NM_M                                                                    AS CUST_NM_M                                  " & vbNewLine _
                                            & "--(2012.06.15) 要望番号1155 --  END  --                                                                                        " & vbNewLine _
                                            & ",MC.AD_1                                                                         AS CUST_AD_1                                  " & vbNewLine _
                                            & ",MC.AD_2                                                                         AS CUST_AD_2                                  " & vbNewLine _
                                            & ",MC.TEL                                                                          AS CUST_TEL                                   " & vbNewLine _
                                            & ",MC.DENPYO_NM                                                                    AS DENPYO_NM                                  " & vbNewLine _
                                            & "--(2014.07.08)修正箇所START                                                                                                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.NRS_BR_NM ELSE MS_UNSO.WH_NM  END NRS_BR_NM                     " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.AD_1      ELSE MS_UNSO.AD_1   END NRS_BR_AD1                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.AD_2      ELSE MS_UNSO.AD_2   END NRS_BR_AD2                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.TEL       ELSE MS_UNSO.TEL    END NRS_BR_TEL                    " & vbNewLine _
                                            & ",CASE WHEN MS_UNSO.WH_NM IS NULL OR MS_UNSO.WH_NM = '' THEN MB.FAX       ELSE MS_UNSO.FAX    END NRS_BR_FAX                    " & vbNewLine _
                                            & "--,MB.NRS_BR_NM                                                                    AS NRS_BR_NM                                  " & vbNewLine _
                                            & "--,MB.AD_1                                                                         AS NRS_BR_AD1                                 " & vbNewLine _
                                            & "--,MB.AD_2                                                                         AS NRS_BR_AD2                                 " & vbNewLine _
                                            & "--,MB.TEL                                                                          AS NRS_BR_TEL                                 " & vbNewLine _
                                            & "--,MB.FAX                                                                          AS NRS_BR_FAX                                 " & vbNewLine _
                                            & "--(2014.07.08)修正箇所END                                                                                                    " & vbNewLine _
                                            & ",USER_E.USER_NM                                                                  AS PIC                                        " & vbNewLine _
                                            & ",UNSOM.UNSO_NO_M                                                                 AS OUTKA_NO_M                                 " & vbNewLine _
                                            & ",''                                                                              AS OUTKA_NO_S                                 " & vbNewLine _
                                            & ",''                                                                              AS INKA_DATE                                  " & vbNewLine _
                                            & ",''                                                                              AS REMARK_OUT                                 " & vbNewLine _
                                            & "--NOTES 1032 START①                                                                                                           " & vbNewLine _
                                            & "--,CASE WHEN (MDG.GOODS_NM IS NOT NULL AND MDG.GOODS_NM <> '') THEN MDG.GOODS_NM                                               " & vbNewLine _
                                            & "--      ELSE MG.GOODS_NM_1                                                                                                     " & vbNewLine _
                                            & "--END                                                                             AS GOODS_NM_1                                " & vbNewLine _
                                            & ",CASE WHEN (MDG.GOODS_NM IS NOT NULL AND MDG.GOODS_NM <> '') THEN MDG.GOODS_NM   --Case1                                       " & vbNewLine _
                                            & "	  WHEN (MG.GOODS_NM_1 IS NOT NULL AND MG.GOODS_NM_1 <> '') THEN MG.GOODS_NM_1   --Case2                                       " & vbNewLine _
                                            & "      ELSE UNSOM.GOODS_NM                                                        --Case3                                       " & vbNewLine _
                                            & " END                                                                             AS GOODS_NM_1                                 " & vbNewLine _
                                            & "--NOTES 1032 END①                                                                                                             " & vbNewLine _
                                            & ",UNSOM.LOT_NO                                                                    AS LOT_NO                                     " & vbNewLine _
                                            & ",''                                                                              AS SERIAL_NO                                  " & vbNewLine _
                                            & ",UNSOM.IRIME                                                                     AS IRIME                                      " & vbNewLine _
                                            & "--NOTES 1032 START②                                                                                                           " & vbNewLine _
                                            & "--,MG.PKG_NB                                                                     AS PKG_NB                                     " & vbNewLine _
                                            & ",CASE WHEN (MG.PKG_NB IS NOT NULL AND MG.PKG_NB <> 0 ) THEN MG.PKG_NB                                                          " & vbNewLine _
                                            & "      ELSE UNSOM.PKG_NB                                                                                                        " & vbNewLine _
                                            & " END                                                                             AS PKG_NB                                     " & vbNewLine _
                                            & "--NOTES 1032 END②                                                                                                             " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB / MG.PKG_NB                                                   AS KONSU                                      " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB % MG.PKG_NB                                                   AS HASU                                       " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_NB                                                               AS ALCTD_NB                                   " & vbNewLine _
                                            & "--NOTES 1032 START③                                                                                                           " & vbNewLine _
                                            & "--,MG.PKG_UT                                                                     AS PKG_UT                                     " & vbNewLine _
                                            & ",CASE WHEN (MG.PKG_UT IS NOT NULL AND MG.PKG_UT <> '') THEN MG.PKG_UT                                                          " & vbNewLine _
                                            & "      ELSE UNSOM.QT_UT                                                                                                         " & vbNewLine _
                                            & " END                                                                             AS PKG_UT                                     " & vbNewLine _
                                            & "--NOTES 1032 END③                                                                                                             " & vbNewLine _
                                            & "--NOTES 1032 START④                                                                                                           " & vbNewLine _
                                            & "--2012/05/24 START                                                                                                             " & vbNewLine _
                                            & "--,KBN2.KBN_NM1                                                                    AS PKG_UT_NM                                " & vbNewLine _
                                            & "--,CASE WHEN (MG.PKG_UT IS NOT NULL AND MG.PKG_UT <> '') THEN KBN2.KBN_NM1                                                     " & vbNewLine _
                                            & "--      ELSE KBN2_1.KBN_NM1                                                                                                    " & vbNewLine _
                                            & "-- END                                                                             AS PKG_UT_NM                                " & vbNewLine _
                                            & ",CASE WHEN (KBN2.KBN_NM1 IS NOT NULL AND KBN2.KBN_NM1 <> '') THEN KBN2.KBN_NM1                                                 " & vbNewLine _
                                            & "      ELSE KBN2_1.KBN_NM1                                                                                                      " & vbNewLine _
                                            & " END                                                                             AS PKG_UT_NM                                  " & vbNewLine _
                                            & "--2012/05/24 END                                                                                                               " & vbNewLine _
                                            & "--NOTES 1032 END④                                                                                                             " & vbNewLine _
                                            & ",UNSOM.UNSO_TTL_QT                                                               AS ALCTD_QT                                   " & vbNewLine _
                                            & "--NOTES 1032 START⑤                                                                                                           " & vbNewLine _
                                            & "--,MG.STD_IRIME_UT                                                                 AS IRIME_UT                                 " & vbNewLine _
                                            & ",CASE WHEN (MG.STD_IRIME_UT IS NOT NULL AND MG.STD_IRIME_UT <> '') THEN MG.STD_IRIME_UT                                        " & vbNewLine _
                                            & "      ELSE UNSOM.IRIME_UT                                                                                                      " & vbNewLine _
                                            & " END                                                                             AS IRIME_UT                                   " & vbNewLine _
                                            & "--NOTES 1032 END⑤                                                                                                             " & vbNewLine _
                                            & ",0                                                                               AS WT                                         " & vbNewLine _
                                            & ",UNSOL.UNSO_WT                                                                   AS UNSO_WT                                    " & vbNewLine _
                                            & ",''                                                                              AS TOU_NO                                     " & vbNewLine _
                                            & ",''                                                                              AS SITU_NO                                    " & vbNewLine _
                                            & ",''                                                                              AS ZONE_CD                                    " & vbNewLine _
                                            & ",''                                                                              AS LOCA                                       " & vbNewLine _
                                            & ",0                                                                               AS ZAN_KONSU                                  " & vbNewLine _
                                            & ",0                                                                               AS ZAN_HASU                                   " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST                                                                AS GOODS_CD_CUST                              " & vbNewLine _
                                            & ",''                                                                              AS REMARK_M                                   " & vbNewLine _
                                            & "--★★★変更開始--------------------------------                                                                               " & vbNewLine _
                                            & ",''                                                            AS CUST_NM_S                                                    " & vbNewLine _
                                            & ",''                                                            AS CUST_NM_S_H                                                  " & vbNewLine _
                                            & "--★★★変更--------------------------------                                                                                   " & vbNewLine _
                                            & ",UNSOL.CUST_REF_NO                                                               AS CUST_ORD_NO_DTL                            " & vbNewLine _
                                            & ",''                                                                              AS OUTKA_REMARK                               " & vbNewLine _
                                            & ",UNSOL.REMARK                                                                    AS HAISOU_REMARK                              " & vbNewLine _
                                            & ",''                                                                              AS NHS_REMARK                                 " & vbNewLine _
                                            & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  --- START ---                                             " & vbNewLine _
                                            & "--,CASE WHEN MD.KYORI > 0 THEN MD.KYORI                                                                                        " & vbNewLine _
                                            & "--      WHEN (MD.KYORI = 0 AND MDO.JIS < MD.JIS) THEN MK1.KYORI                                                                " & vbNewLine _
                                            & "--      WHEN (MD.KYORI = 0 AND MD.JIS < MDO.JIS) THEN MK2.KYORI                                                                " & vbNewLine _
                                            & "--      ELSE 0                                                                                                                 " & vbNewLine _
                                            & "-- END                                                                           AS KYORI                                      " & vbNewLine _
                                            & ",CASE WHEN MD.KYORI > 0 THEN MD.KYORI                                                                                          " & vbNewLine _
                                            & "      WHEN (MD.KYORI = 0 AND MDO.JIS <= MD.JIS) THEN MK1.KYORI                                                                 " & vbNewLine _
                                            & "      WHEN (MD.KYORI = 0 AND MD.JIS <= MDO.JIS) THEN MK2.KYORI                                                                 " & vbNewLine _
                                            & "      ELSE 0                                                                                                                   " & vbNewLine _
                                            & " END                                                                             AS KYORI                                      " & vbNewLine _
                                            & "--(2012.08.06) 発JIS、着JISが共に同じJISでも、必ず0㎞になるのを修正  ---  END  ---                                             " & vbNewLine _
                                            & ",UNSOL.UNSO_PKG_NB                                                               AS OUTKA_PKG_NB_L                             " & vbNewLine _
                                            & ",''                                                                              AS SOKO_NM                                    " & vbNewLine _
                                            & ",''                                                                              AS BUSHO_NM                                   " & vbNewLine _
                                            & ",''                                                                              AS NRS_BR_NM_NICHI                            " & vbNewLine _
                                            & ",0                                                                               AS ALCTD_CAN_QT                               " & vbNewLine _
                                            & ",''                                                                              AS ALCTD_KB                                   " & vbNewLine _
                                            & ",'1'                                                                             AS PTN_FLAG                                   " & vbNewLine _
                                            & ",UNSOL.CUST_CD_L                                                                 AS CUST_CD_L                                  " & vbNewLine _
                                            & ",''                                                                              AS ORDER_TYPE                                 " & vbNewLine _
                                            & ",''                                                                              AS LT_DATE                                     " & vbNewLine _
                                            & ",''                                                                              AS BUYER_ORD_NO_DTL                            " & vbNewLine _
                                            & ",''                                                                              AS GOODS_COND_CD                               " & vbNewLine _
                                            & ",''                                                                              AS REMARK_ZAI                                  " & vbNewLine _
                                            & ",''                                                                              AS TITLE_SMPL                                  " & vbNewLine _
                                            & ",0                                                                               AS ALCTD_CAN_NB                                " & vbNewLine _
                                            & ",0                                                                               AS BACKLOG_QT                                  " & vbNewLine _
                                            & ",KBN3.KBN_NM2                                                                    AS DOKU_NM                                     " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                              " & vbNewLine _
                                            & ",KBN9.KBN_NM2                                                                    AS PC_KB_NM                                   " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                            & "--日医工対策開始                                                                                                               " & vbNewLine _
                                            & ",''                       AS TORI_KB                                                                                           " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & ",UNSOL.SHIP_CD            AS SHIP_CD_L                                                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--★削除開始2012/05/11                                                                                                         " & vbNewLine _
                                            & "--,''                       AS CHOKUSO_NM1                                                                                     " & vbNewLine _
                                            & "--,''                       AS CHOKUSO_CD                                                                                      " & vbNewLine _
                                            & "--,''                       AS HAKKO_DATE                                                                                      " & vbNewLine _
                                            & "--,''                       AS SET_NAIYO_4                                                                                     " & vbNewLine _
                                            & "--,''                       AS SET_NAIYO_5                                                                                     " & vbNewLine _
                                            & "--,''                       AS SET_NAIYO_6                                                                                     " & vbNewLine _
                                            & "--,''                       AS GOODS_NM_2                                                                                      " & vbNewLine _
                                            & "--★削除終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & ",MCD_N.SET_NAIYO         AS SET_NAIYO_4                                                                                        " & vbNewLine _
                                            & ",MCD_N.SET_NAIYO_2       AS SET_NAIYO_5                                                                                        " & vbNewLine _
                                            & ",MCD_N.SET_NAIYO_3       AS SET_NAIYO_6                                                                                        " & vbNewLine _
                                            & ",MG.GOODS_NM_2           AS GOODS_NM_2                                                                                         " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & ",''                       AS KICHO_KB                                                                                          " & vbNewLine _
                                            & ",''                       AS SEI_YURAI_KB                                                                                      " & vbNewLine _
                                            & ",''                       AS REMARK_UPPER                                                                                      " & vbNewLine _
                                            & ",''                       AS REMARK_LOWER                                                                                      " & vbNewLine _
                                            & ",''                       AS GOODS_SYUBETU                                                                                     " & vbNewLine _
                                            & ",''                       AS YUKOU_KIGEN                                                                                       " & vbNewLine _
                                            & ",''                       AS GOODS_NM_3         --2012/05/23                                                                   " & vbNewLine _
                                            & ",MDU.EDI_CD               AS EDISHIP_CD         --2012/05/23                                                                   " & vbNewLine _
                                            & ",MD.EDI_CD                AS EDIDEST_CD         --2012/05/23                                                                   " & vbNewLine _
                                            & "--日医工対策終了                                                                                                               " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応 --- START ---                                                                                      " & vbNewLine _
                                            & ",MCD_F.SET_NAIYO         AS SET_NAIYO_FROM1                                                                                    " & vbNewLine _
                                            & ",MCD_F.SET_NAIYO_2       AS SET_NAIYO_FROM2                                                                                    " & vbNewLine _
                                            & ",MCD_F.SET_NAIYO_3       AS SET_NAIYO_FROM3                                                                                    " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応 ---  END  ---                                                                                      " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                            & ",MC.AD_3                 AS CUST_AD_3                                                                                          " & vbNewLine _
                                            & ",MD.REMARK            AS DEST_REMARK                                                                                           " & vbNewLine _
                                            & ",MD.SALES_CD          AS DEST_SALES_CD                                                                                         " & vbNewLine _
                                            & ",MC_SALES.CUST_NM_L      AS DEST_SALES_NM                                                                                      " & vbNewLine _
                                            & ",MC_SALES.AD_1           AS DEST_SALES_AD_1                                                                                    " & vbNewLine _
                                            & ",MC_SALES.AD_2           AS DEST_SALES_AD_2                                                                                    " & vbNewLine _
                                            & ",MC_SALES.AD_3           AS DEST_SALES_AD_3                                                                                    " & vbNewLine _
                                            & ",MC_SALES.TEL            AS DEST_SALES_TEL                                                                                     " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 開始20120731                                                                        " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_1                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_2                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_3                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_4                                                                                       " & vbNewLine _
                                            & ",''                      AS SAGYO_RYAK_5                                                                                       " & vbNewLine _
                                            & ",''                      AS DEST_SAGYO_RYAK_1                                                                                  " & vbNewLine _
                                            & ",''                      AS DEST_SAGYO_RYAK_2                                                                                  " & vbNewLine _
                                            & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731                                                                        " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　開始　                                                                                         " & vbNewLine _
                                            & ",''                       AS SZ01_YUSO          --篠崎                                                                         " & vbNewLine _
                                            & ",''                       AS SZ01_UNSO          --篠崎                                                                         " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                            & "  , MD.DELI_ATT           AS  HAISOU_REMARK_MST                                                                                " & vbNewLine _
                                            & "  , MGD43.REMARK          AS GOODS_NM_CUST                                                                                     " & vbNewLine _
                                            & "--2015/1/14 LMC757                                                                                                             " & vbNewLine _
                                            & "--群馬篠崎運送向け自由項目対応　終了                                                                                         　" & vbNewLine _
                                            & "--20160120 ゴードー add start                                                                                         　       " & vbNewLine _
                                            & "  , MGDETAILS.SET_NAIYO   AS GODO_GOODS_CODE                                                                                   " & vbNewLine _
                                            & "--20160120 ゴードー add end                                                                                         　         " & vbNewLine _
                                            & "FROM                                                                                                                           " & vbNewLine _
                                            & "--運送L                                                                                                                        " & vbNewLine _
                                            & "$LM_TRN$..F_UNSO_L UNSOL                                                                                                       " & vbNewLine _
                                            & "--運送M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM                                                                                             " & vbNewLine _
                                            & "ON  UNSOM.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                            & "AND UNSOM.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND UNSOM.UNSO_NO_L = UNSOL.UNSO_NO_L                                                                                          " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                            & "--運送M(中MIN)                                                                                                                 " & vbNewLine _
                                            & "LEFT OUTER JOIN                                                                                                                " & vbNewLine _
                                            & "       (SELECT                                                                                                                 " & vbNewLine _
                                            & "           NRS_BR_CD                                                                                                           " & vbNewLine _
                                            & "           ,UNSO_NO_L                                                                                                          " & vbNewLine _
                                            & "           ,MIN(UNSO_NO_M) AS  UNSO_NO_M                                                                                       " & vbNewLine _
                                            & "       FROM $LM_TRN$..F_UNSO_M WHERE SYS_DEL_FLG ='0'                                                                          " & vbNewLine _
                                            & "        AND NRS_BR_CD = @NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "        AND UNSO_NO_L = @KANRI_NO_L                                                                                            " & vbNewLine _
                                            & "       GROUP BY NRS_BR_CD,UNSO_NO_L) UNSOM_MIN                                                                                 " & vbNewLine _
                                            & "       ON UNSOM_MIN.NRS_BR_CD       = UNSOL.NRS_BR_CD                                                                          " & vbNewLine _
                                            & "       AND UNSOM_MIN.UNSO_NO_L      = UNSOL.UNSO_NO_L                                                                          " & vbNewLine _
                                            & "--運送M(中MIN)                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM2                                                                                            " & vbNewLine _
                                            & "ON  UNSOM2.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                         " & vbNewLine _
                                            & "AND UNSOM2.UNSO_NO_L = UNSOL.UNSO_NO_L                                                                                         " & vbNewLine _
                                            & "AND UNSOM2.UNSO_NO_M = UNSOM_MIN.UNSO_NO_M                                                                                     " & vbNewLine _
                                            & "AND UNSOM2.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "--★★追加終了--------------------------------                                                                                 " & vbNewLine _
                                            & "--商品M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                 " & vbNewLine _
                                            & "ON  MG.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MG.NRS_BR_CD = UNSOM.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MG.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                                                                       " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                               " & vbNewLine _
                                             & "--商品M(MIN)                                                                                                                  " & vbNewLine _
                                            & "LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                                                                                  " & vbNewLine _
                                            & "ON M_GOODS_MIN.NRS_BR_CD      = UNSOL.NRS_BR_CD                                                                                " & vbNewLine _
                                            & "AND M_GOODS_MIN.GOODS_CD_NRS   = UNSOM2.GOODS_CD_NRS                                                                           " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                               " & vbNewLine _
                                            & "--運送会社M                                                                                                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                                                                                            " & vbNewLine _
                                            & "ON  UNSOCO.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "AND UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                         " & vbNewLine _
                                            & "AND UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD                                                                                           " & vbNewLine _
                                            & "AND UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD                                                                                     " & vbNewLine _
                                            & "--届先M                                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MD                                                                                                  " & vbNewLine _
                                            & "ON  MD.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MD.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MD.CUST_CD_L = UNSOL.CUST_CD_L                                                                                             " & vbNewLine _
                                            & "AND MD.DEST_CD = UNSOL.DEST_CD                                                                                                 " & vbNewLine _
                                            & "--届先M(売上先取得)                                                                                                            " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MDU                                                                                                 " & vbNewLine _
                                            & "ON  MDU.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDU.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDU.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDU.DEST_CD = UNSOL.SHIP_CD                                                                                                " & vbNewLine _
                                            & "--届先M(距離取得)                                                                                                              " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DEST MDO                                                                                                 " & vbNewLine _
                                            & "ON  MDO.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDO.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDO.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDO.DEST_CD = UNSOL.ORIG_CD                                                                                                " & vbNewLine _
                                            & "--荷主M(商品M経由)                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC                                                                                                  " & vbNewLine _
                                            & "ON  MC.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MC.NRS_BR_CD = MG.NRS_BR_CD                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_L = MG.CUST_CD_L                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_M = MG.CUST_CD_M                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_S = MG.CUST_CD_S                                                                                                " & vbNewLine _
                                            & "AND MC.CUST_CD_SS = MG.CUST_CD_SS                                                                                              " & vbNewLine _
                                            & "--(2014.07.08)追加箇所START                                                                                                    " & vbNewLine _
                                            & "--倉庫M(荷主M経由)                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SOKO MS_UNSO                                                                                             " & vbNewLine _
                                            & "ON  MS_UNSO.SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
                                            & "AND MC.NRS_BR_CD = MS_UNSO.NRS_BR_CD                                                                                           " & vbNewLine _
                                            & "AND MC.DEFAULT_SOKO_CD = MS_UNSO.WH_CD                                                                                         " & vbNewLine _
                                            & "--(2014.07.08)追加箇所END                                                                                                      " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 --- START ---                                                                                       " & vbNewLine _
                                            & "--届先M(納品書荷主名義名取得)(届先M参照)                                                                                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC_SALES                                                                                            " & vbNewLine _
                                            & "       ON MC_SALES.NRS_BR_CD  = MD.NRS_BR_CD                                                                                   " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_L  = MD.SALES_CD                                                                                    " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_M  = '00'                                                                                           " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_S  = '00'                                                                                           " & vbNewLine _
                                            & "      AND MC_SALES.CUST_CD_SS = '00'                                                                                           " & vbNewLine _
                                            & "--【要望番号1122】埼玉対応 ---  END  ---                                                                                       " & vbNewLine _
                                            & "--2014/5/16Start s.kobayashi NotesNo.2183                                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN11                                                                                                " & vbNewLine _
                                            & "ON  KBN11.KBN_GROUP_CD = 'U028'  --追加                                                                                        " & vbNewLine _
                                            & "AND UNSOL.SEIQ_TARIFF_CD = KBN11.KBN_NM1                                                                                       " & vbNewLine _
                                            & "--2014/5/16End s.kobayashi NotesNo.2183                                                                                       " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)                                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_KYORI MK1                                                                                                " & vbNewLine _
                                            & "ON  MK1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MK1.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "--AND MK1.KYORI_CD  = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                            & "AND MK1.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                        " & vbNewLine _
                                            & "AND MK1.ORIG_JIS_CD = MDO.JIS                                                                                                  " & vbNewLine _
                                            & "AND MK1.DEST_JIS_CD = MD.JIS                                                                                                   " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)                                                                                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_KYORI MK2                                                                                                " & vbNewLine _
                                            & "ON  MK2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MK2.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "--AND MK2.KYORI_CD  = MC.BETU_KYORI_CD                                                                                           " & vbNewLine _
                                            & "AND MK2.KYORI_CD = (CASE ISNULL(KBN11.KBN_NM2,'') WHEN '' THEN MC.BETU_KYORI_CD ELSE KBN11.KBN_NM2 END)                        " & vbNewLine _
                                            & "AND MK2.ORIG_JIS_CD = MD.JIS                                                                                                   " & vbNewLine _
                                            & "AND MK2.DEST_JIS_CD = MDO.JIS                                                                                                  " & vbNewLine _
                                            & "--★★★追加開始--------------------------------                                                                                     " & vbNewLine _
                                             & "--荷主M(商品M経由) 最小の出荷(中)で抽出                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST MC2                                                                                                 " & vbNewLine _
                                            & "ON  MC2.NRS_BR_CD = M_GOODS_MIN.NRS_BR_CD                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_L = M_GOODS_MIN.CUST_CD_L                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_M = M_GOODS_MIN.CUST_CD_M                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_S = M_GOODS_MIN.CUST_CD_S                                                                                      " & vbNewLine _
                                            & "AND MC2.CUST_CD_SS = M_GOODS_MIN.CUST_CD_SS                                                                                    " & vbNewLine _
                                            & "AND MC2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--★★★追加終了--------------------------------                                                                                     " & vbNewLine _
                                            & "--営業所M                                                                                                                      " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_NRS_BR MB                                                                                                " & vbNewLine _
                                            & "ON  MB.SYS_DEL_FLG = '0'                                                                                                       " & vbNewLine _
                                            & "AND MB.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "--届先商品M(運送L参照)                                                                                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_DESTGOODS MDG                                                                                            " & vbNewLine _
                                            & "ON  MDG.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "AND MDG.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MDG.CUST_CD_L = UNSOL.CUST_CD_L                                                                                            " & vbNewLine _
                                            & "AND MDG.CUST_CD_M = UNSOL.CUST_CD_M                                                                                            " & vbNewLine _
                                            & "AND MDG.CD = UNSOL.DEST_CD                                                                                                     " & vbNewLine _
                                            & "AND MDG.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS                                                                                      " & vbNewLine _
                                            & "--区分M(納入予定区分)                                                                                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                                 " & vbNewLine _
                                            & "ON  KBN1.KBN_GROUP_CD = 'N010'                                                                                                 " & vbNewLine _
                                            & "AND KBN1.KBN_CD = UNSOL.ARR_PLAN_TIME                                                                                          " & vbNewLine _
                                            & "AND KBN1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--区分M(荷姿)                                                                                                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN2                                                                                                 " & vbNewLine _
                                            & "ON  KBN2.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                            & "AND KBN2.KBN_CD = MG.PKG_UT                                                                                                    " & vbNewLine _
                                            & "AND KBN2.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--区分M(荷姿)F運送M版(Notes1032)                                                                                               " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN2_1                                                                                                 " & vbNewLine _
                                            & "ON  KBN2_1.KBN_GROUP_CD = 'N001'                                                                                                 " & vbNewLine _
                                            & "AND KBN2_1.KBN_CD = UNSOM.QT_UT                                                                                                    " & vbNewLine _
                                            & "AND KBN2_1.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--SYS_ENT_USER名称取得                                                                                                         " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..S_USER USER_E                                                                                              " & vbNewLine _
                                            & "ON  USER_E.USER_CD     = UNSOL.SYS_ENT_USER                                                                                    " & vbNewLine _
                                            & "AND USER_E.SYS_DEL_FLG = '0'                                                                                                   " & vbNewLine _
                                            & "--区分M(毒劇区分名称)                                                                                                           " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN3                                                                                                  " & vbNewLine _
                                            & "ON  KBN3.KBN_GROUP_CD = 'G001'                                                                                                  " & vbNewLine _
                                            & "AND KBN3.KBN_CD = MG.DOKU_KB                                                                                                    " & vbNewLine _
                                            & "AND KBN3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　START                                                                             " & vbNewLine _
                                            & "--区分M(元着払区分名称)                                                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN9                                                                                                 " & vbNewLine _
                                            & "ON  KBN9.KBN_GROUP_CD = 'M001'                                                                                                 " & vbNewLine _
                                            & "AND KBN9.KBN_CD = UNSOL.PC_KB                                                                                                  " & vbNewLine _
                                            & "AND KBN9.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
                                            & "--【要望番号995】　2012/04/17　元着払い区分　 END                                                                              " & vbNewLine _
                                            & "--★追加開始2012/05/11                                                                                                         " & vbNewLine _
                                            & "--荷主明細M ★                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_N                                                                                       " & vbNewLine _
                                            & "ON  MCD_N.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND MCD_N.CUST_CD   = UNSOL.CUST_CD_L                                                                                          " & vbNewLine _
                                            & "AND MCD_N.SUB_KB = '26'                                                                                                        " & vbNewLine _
                                            & "--★追加終了2012/05/11                                                                                                         " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応  --- START ---                                                                                     " & vbNewLine _
                                            & "--荷主明細M ★                                                                                                                 " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD_F                                                                                       " & vbNewLine _
                                            & "ON  MCD_F.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                          " & vbNewLine _
                                            & "AND MCD_F.CUST_CD   = UNSOL.CUST_CD_L                                                                                          " & vbNewLine _
                                            & "AND MCD_F.SUB_KB = '29'                                                                                                        " & vbNewLine _
                                            & "--(2012.06.01) 日本ﾌｧｲﾝ対応  ---  END  ---                                                                                     " & vbNewLine _
                                            & "                                                                                                                               " & vbNewLine _
                                            & "--運送Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                            & "ON  UNSOL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                           " & vbNewLine _
                                            & "AND UNSOL.CUST_CD_L = MCR1.CUST_CD_L                                                                                           " & vbNewLine _
                                            & "AND UNSOL.CUST_CD_M = MCR1.CUST_CD_M                                                                                           " & vbNewLine _
                                            & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                            & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                            & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                            & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                            & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                            & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                            & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                            & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                            & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                            & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                            & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                            & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                            & "ON  MR3.NRS_BR_CD = UNSOL.NRS_BR_CD                                                                                            " & vbNewLine _
                                            & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                            & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                            & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                            & "--商品明細M                                                                                                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                                                                                        " & vbNewLine _
                                            & "ON  MGD.NRS_BR_CD = MG.NRS_BR_CD                                                                                               " & vbNewLine _
                                            & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                         " & vbNewLine _
                                            & "AND MGD.SUB_KB = '09'                                                                                                          " & vbNewLine _
                                            & " --商品M LMC757   2015/1/14                                                             " & vbNewLine _
                                            & "	LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD43                                                    " & vbNewLine _
                                            & "	ON  UNSOM.NRS_BR_CD = MGD43.NRS_BR_CD                                              " & vbNewLine _
                                            & "	AND UNSOM.GOODS_CD_NRS = MGD43.GOODS_CD_NRS                                              " & vbNewLine _
                                            & "	AND UNSOL.SHIP_CD = MGD43.SET_NAIYO                                              " & vbNewLine _
                                            & " AND MGD43.SUB_KB ='43' AND MGD43.SYS_DEL_FLG ='0'                                         " & vbNewLine _
                                            & " --商品M LMC757   2015/1/14                                                             " & vbNewLine _
                                            & " --20160120 tsunehira add                                                             " & vbNewLine _
                                            & " LEFT OUTER JOIN LM_MST..M_GOODS_DETAILS MGDETAILS                                                             " & vbNewLine _
                                            & " ON  MG.NRS_BR_CD = MGDETAILS.NRS_BR_CD                                                             " & vbNewLine _
                                            & " AND MG.GOODS_CD_NRS = MGDETAILS.GOODS_CD_NRS                                                             " & vbNewLine _
                                            & " AND MGDETAILS.SUB_KB = '56'                                                             " & vbNewLine _
                                            & " AND MGDETAILS.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                            & "WHERE                                                                                                                          " & vbNewLine _
                                            & "    UNSOL.SYS_DEL_FLG = '0'                                                                                                    " & vbNewLine _
                                            & "AND UNSOL.NRS_BR_CD = @NRS_BR_CD                                                                                               " & vbNewLine _
                                            & "AND UNSOL.UNSO_NO_L = @KANRI_NO_L                                                                                              " & vbNewLine _
                                            & "--★★★SQL_WHERE_MAIN★★★                   " & vbNewLine _
                                            & "  ) MAIN                                       " & vbNewLine _
                                            & "WHERE                                          " & vbNewLine _
                                            & " PTN_FLAG = @PTN_FLAG                          " & vbNewLine _
                                            &"  --SQL_GROUP_BY1                               " & vbNewLine _
                                         & "GROUP BY                                       " & vbNewLine _
                                         & " RPT_ID                                        " & vbNewLine _
                                         & ",NRS_BR_CD                                     " & vbNewLine _
                                         & ",PRINT_SORT                                    " & vbNewLine _
                                         & ",DEST_CD                                       " & vbNewLine _
                                         & ",DEST_NM                                       " & vbNewLine _
                                         & ",DEST_AD_1                                     " & vbNewLine _
                                         & ",DEST_AD_2                                     " & vbNewLine _
                                         & ",DEST_AD_3                                     " & vbNewLine _
                                         & ",DEST_TEL                                      " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                               " & vbNewLine _
                                         & ",CUST_ORD_NO                                   " & vbNewLine _
                                         & ",BUYER_ORD_NO                                  " & vbNewLine _
                                         & ",UNSOCO_NM                                     " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                 " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                 " & vbNewLine _
                                         & ",OUTKA_NO_L                                    " & vbNewLine _
                                         & ",URIG_NM                                       " & vbNewLine _
                                         & ",CUST_NM_L                                     " & vbNewLine _
                                         & "--(2012.06.15) 要望番号1155  --- START ---     " & vbNewLine _
                                         & ",CUST_NM_M                                     " & vbNewLine _
                                         & "--(2012.06.15) 要望番号1155  ---  END  ---     " & vbNewLine _
                                         & ",CUST_AD_1                                     " & vbNewLine _
                                         & ",CUST_AD_2                                     " & vbNewLine _
                                         & ",CUST_TEL                                      " & vbNewLine _
                                         & ",DENPYO_NM                                     " & vbNewLine _
                                         & ",NRS_BR_NM                                     " & vbNewLine _
                                         & ",NRS_BR_AD1                                    " & vbNewLine _
                                         & ",NRS_BR_AD2                                    " & vbNewLine _
                                         & ",NRS_BR_TEL                                    " & vbNewLine _
                                         & ",NRS_BR_FAX                                    " & vbNewLine _
                                         & ",PIC                                           " & vbNewLine _
                                         & "--,OUTKA_NO_M                                    " & vbNewLine _
                                         & "--,INKA_DATE                                     " & vbNewLine _
                                         & ",REMARK_OUT                                    " & vbNewLine _
                                         & ",GOODS_NM_1                                    " & vbNewLine _
                                         & ",LOT_NO                                        " & vbNewLine _
                                         & ",SERIAL_NO                                     " & vbNewLine _
                                         & ",IRIME                                         " & vbNewLine _
                                         & ",PKG_NB                                        " & vbNewLine _
                                         & ",PKG_UT                                        " & vbNewLine _
                                         & ",PKG_UT_NM                                     " & vbNewLine _
                                         & ",IRIME_UT                                      " & vbNewLine _
                                         & ",WT                                            " & vbNewLine _
                                         & ",UNSO_WT                                       " & vbNewLine _
                                         & "--,TOU_NO                                        " & vbNewLine _
                                         & "--,SITU_NO                                       " & vbNewLine _
                                         & "--,ZONE_CD                                       " & vbNewLine _
                                         & "--,LOCA                                          " & vbNewLine _
                                         & ",GOODS_CD_CUST                                 " & vbNewLine _
                                         & ",REMARK_M                                      " & vbNewLine _
                                         & ",CUST_NM_S                                     " & vbNewLine _
                                         & ",CUST_NM_S_H                                   " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                               " & vbNewLine _
                                         & ",OUTKA_REMARK                                  " & vbNewLine _
                                         & ",HAISOU_REMARK                                 " & vbNewLine _
                                         & ",NHS_REMARK                                    " & vbNewLine _
                                         & ",KYORI                                         " & vbNewLine _
                                         & ",OUTKA_PKG_NB_L                                " & vbNewLine _
                                         & ",SOKO_NM                                       " & vbNewLine _
                                         & ",BUSHO_NM                                      " & vbNewLine _
                                         & ",NRS_BR_NM_NICHI                               " & vbNewLine _
                                         & ",ALCTD_KB                                      " & vbNewLine _
                                         & ",PTN_FLAG                                      " & vbNewLine _
                                         & ",CUST_CD_L                                     " & vbNewLine _
                                         & ",ORDER_TYPE                                    " & vbNewLine _
                                         & "--【要望番号995】　2012/04/17　元着払い区分　START  " & vbNewLine _
                                         & ",PC_KB_NM                                      " & vbNewLine _
                                         & "--【要望番号995】　2012/04/17　元着払い区分　 END   " & vbNewLine _
                                         & "--【要望番号1017】 2012/04/24　注文番号明細  START   " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                    " & vbNewLine _
                                         & "--【要望番号1017】　2012/04/24 注文番号明細　 END    " & vbNewLine _
                                         & "--日医工対策開始                                    " & vbNewLine _
                                         & ",TORI_KB                                         " & vbNewLine _
                                         & "--★追加開始2012/05/11                           " & vbNewLine _
                                         & ",SHIP_CD_L                                       " & vbNewLine _
                                         & "--★追加終了2012/05/11                           " & vbNewLine _
                                         & "--★削除開始2012/05/11                           " & vbNewLine _
                                         & "--,CHOKUSO_NM1                                   " & vbNewLine _
                                         & "--,CHOKUSO_CD                                    " & vbNewLine _
                                         & "--,HAKKO_DATE                                    " & vbNewLine _
                                         & "--★削除終了2012/05/11                           " & vbNewLine _
                                         & ",SET_NAIYO_4                                     " & vbNewLine _
                                         & ",SET_NAIYO_5                                     " & vbNewLine _
                                         & ",SET_NAIYO_6                                     " & vbNewLine _
                                         & ",GOODS_NM_2                                      " & vbNewLine _
                                         & ",KICHO_KB                                        " & vbNewLine _
                                         & ",SEI_YURAI_KB                                    " & vbNewLine _
                                         & ",REMARK_UPPER                                    " & vbNewLine _
                                         & ",REMARK_LOWER                                    " & vbNewLine _
                                         & ",GOODS_SYUBETU                                   " & vbNewLine _
                                         & ",YUKOU_KIGEN                                     " & vbNewLine _
                                         & ",GOODS_NM_3 --2012/05/23                         " & vbNewLine _
                                         & ",EDISHIP_CD --2012/05/23                         " & vbNewLine _
                                         & ",EDIDEST_CD --2012/05/23                         " & vbNewLine _
                                         & "--日医工対策終了                                 " & vbNewLine _
                                         & "--(2012.06.01)日本ﾌｧｲﾝ対応 -- START --           " & vbNewLine _
                                         & ",SET_NAIYO_FROM1                                 " & vbNewLine _
                                         & ",SET_NAIYO_FROM2                                 " & vbNewLine _
                                         & ",SET_NAIYO_FROM3                                 " & vbNewLine _
                                         & "--(2012.06.01)日本ﾌｧｲﾝ対応 --  END  --           " & vbNewLine _
                                         & "--【要望番号1122】埼玉対応 --- START ---         " & vbNewLine _
                                         & ",CUST_AD_3                                       " & vbNewLine _
                                         & ",DEST_REMARK                                     " & vbNewLine _
                                         & ",DEST_SALES_CD                                   " & vbNewLine _
                                         & ",DEST_SALES_NM                                   " & vbNewLine _
                                         & ",DEST_SALES_AD_1                                 " & vbNewLine _
                                         & ",DEST_SALES_AD_2                                 " & vbNewLine _
                                         & ",DEST_SALES_AD_3                                 " & vbNewLine _
                                         & ",DEST_SALES_TEL                                  " & vbNewLine _
                                         & "--【要望番号1122】埼玉対応 ---  END  ---         " & vbNewLine _
                                         & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731" & vbNewLine _
                                         & ",SAGYO_RYAK_1                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_2                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_3                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_4                                    " & vbNewLine _
                                         & ",SAGYO_RYAK_5                                    " & vbNewLine _
                                         & ",DEST_SAGYO_RYAK_1                               " & vbNewLine _
                                         & ",DEST_SAGYO_RYAK_2                               " & vbNewLine _
                                         & "--群馬(合算)端数行分割版向け対応(作業略称) 終了20120731" & vbNewLine _
                                         & "--群馬篠崎運送向け自由項目対応　開始　           " & vbNewLine _
                                         & ",SZ01_YUSO  --篠崎                               " & vbNewLine _
                                         & ",SZ01_UNSO  --篠崎                               " & vbNewLine _
                                         & "--群馬篠崎運送向け自由項目対応　終了　           " & vbNewLine _
                                         & "--2015/1/14 LMC757                               " & vbNewLine _
                                         & ",HAISOU_REMARK_MST                               " & vbNewLine _
                                         & ",GOODS_NM_CUST                                   " & vbNewLine _
                                         & "--2015/1/14 LMC757                               " & vbNewLine _
                                         & "--2016/1/20 LMC815 LMC815 add start              " & vbNewLine _
                                         & ",GODO_GOODS_CODE                                 " & vbNewLine _
                                         & "--2016/1/20 LMC815 LMC815 add end                " & vbNewLine _
                                         & "--  ★★★SQL_ORDER_BY★★★                     " & vbNewLine _
                                         & "     ORDER BY                                    " & vbNewLine _
                                         & "     NRS_BR_CD                                   " & vbNewLine _
                                         & "    ,OUTKA_NO_L                                  " & vbNewLine _
                                         & "    ,PRINT_SORT                                  " & vbNewLine _
                                         & "    ,OUTKA_NO_M                                  " & vbNewLine _
                                         & "    ,OUTKA_NO_S                                  " & vbNewLine


#If True Then   'ADD 2020/04/24 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
    Private Const SQL_SELECT_MAIN_JJ15 As String = "-- ★★★SQL_SELECT_ジョンソンエンドジョンソンJ&J_土気自動倉庫★★★                       " & vbNewLine _
                                        & "SELECT                                                                       " & vbNewLine _
                                        & "	 RPT_ID                                                                 " & vbNewLine _
                                        & "	,NRS_BR_CD                                                              " & vbNewLine _
                                        & "	,NOUHINBI                                                               " & vbNewLine _
                                        & "	,NENGAPPI                                                               " & vbNewLine _
                                        & "	,CUST_CD_L                                                              " & vbNewLine _
                                        & "	,CUST_CD_M                                                              " & vbNewLine _
                                        & "	,CUST_NM_L                                                              " & vbNewLine _
                                        & "	,CUST_NM_M                                                              " & vbNewLine _
                                        & "	,CUST_NM_S                                                              " & vbNewLine _
                                        & "	,CUST_NM_SS                                                             " & vbNewLine _
                                        & "	,CUST_AD_1                                                              " & vbNewLine _
                                        & "	,CUST_AD_2                                                              " & vbNewLine _
                                        & "	,CUST_AD_3                                                              " & vbNewLine _
                                        & "	,TOKUISAKI_CD                                                           " & vbNewLine _
                                        & "	,TOKUISAKI_NM                                                           " & vbNewLine _
                                        & "	,CUST_ORD_NO                                                            " & vbNewLine _
                                        & "	,BUYER_ORD_NO                                                           " & vbNewLine _
                                        & "	,OUTKA_NO_L                                                             " & vbNewLine _
                                        & "	,DEST_CD                                                                " & vbNewLine _
                                        & "	,DEST_NM                                                                " & vbNewLine _
                                        & "	,DEST_AD_1                                                              " & vbNewLine _
                                        & "	,DEST_AD_2                                                              " & vbNewLine _
                                        & "	,DEST_AD_3                                                              " & vbNewLine _
                                        & "	,GOODS_CD_CUST                                                          " & vbNewLine _
                                        & "	,GOODS_NM                                                               " & vbNewLine _
                                        & "	,JAN_CD                                                                 " & vbNewLine _
                                        & "	,IRISU                                                                  " & vbNewLine _
                                        & "	,UNSOCO_CD                                                              " & vbNewLine _
                                        & "	,REMARK                                                                 " & vbNewLine _
                                        & "	,DENP_NO                                                                " & vbNewLine _
                                        & "	,ROW_NUMBER() OVER (PARTITION BY OUTKA_NO_L  ORDER BY FREE_C01) AS R_NO " & vbNewLine _
                                        & "	,FREE_C01                                                               " & vbNewLine _
                                        & "	,MIN(OUTKA_M_PKG_NB_TOTAL) AS OUTKA_M_PKG_NB_TOTAL                      " & vbNewLine _
                                        & "	,MIN(OUTKA_TTL_QT_TOTAL)   AS OUTKA_TTL_QT_TOTAL                        " & vbNewLine _
                                        & "	,MIN(LOT_NOG1)             AS LOT_NOG1                                  " & vbNewLine _
                                        & " ,MIN(OUTKA_M_PKG_NBG1)     AS OUTKA_M_PKG_NBG1                          " & vbNewLine _
                                        & "	,MIN(LOT_NOG2)             AS LOT_NOG2                                  " & vbNewLine _
                                        & " ,MIN(OUTKA_M_PKG_NBG2)     AS OUTKA_M_PKG_NBG2                          " & vbNewLine _
                                        & " ,MIN(LOT_NOG3)             AS LOT_NOG3                                  " & vbNewLine _
                                        & " ,MIN(OUTKA_M_PKG_NBG3)     AS OUTKA_M_PKG_NBG3                          " & vbNewLine _
                                        & " ,MAIN.CUST_ORD_NO_DTL      AS ORDER_NO        --ADD 2021/01/19 017853   " & vbNewLine _
                                        & "FROM                                                                     " & vbNewLine _
                                        & " (                                                                       " & vbNewLine


    Private Const SQL_SELECT_MAIN_JJ15_SELECT As String = "-- ★★★SQL_SELECT_MAIN_JJ15_SELECT ★★★                       " & vbNewLine _
                                        & "   SELECT                                                         " & vbNewLine _
                                        & "      CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID            " & vbNewLine _
                                        & "         WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                        & "         ELSE MR3.RPT_ID                                          " & vbNewLine _
                                        & "      END                               AS RPT_ID                 " & vbNewLine _
                                        & "     ,OUTL.NRS_BR_CD                    AS NRS_BR_CD              " & vbNewLine _
                                        & "     ,OUTL.ARR_PLAN_DATE                AS NOUHINBI     --納品日  " & vbNewLine _
                                        & "     ,FORMAT(GETDATE(), 'yyyyMMdd')     AS NENGAPPI     --年月日  " & vbNewLine _
                                        & "     ,CUST.CUST_CD_L                    AS CUST_CD_L              " & vbNewLine _
                                        & "     ,CUST.CUST_CD_M                    AS CUST_CD_M              " & vbNewLine _
                                        & "     ,CUST.CUST_NM_L                    AS CUST_NM_L              " & vbNewLine _
                                        & "     ,CUST.CUST_NM_M                    AS CUST_NM_M              " & vbNewLine _
                                        & "     ,CUST.CUST_NM_S                    AS CUST_NM_S              " & vbNewLine _
                                        & "     ,CUST.CUST_NM_SS                   AS CUST_NM_SS             " & vbNewLine _
                                        & "     ,CUST.AD_1                         AS CUST_AD_1              " & vbNewLine _
                                        & "     ,CUST.AD_2                         AS CUST_AD_2              " & vbNewLine _
                                        & "     ,CUST.AD_3                         AS CUST_AD_3              " & vbNewLine _
                                        & "--    	,ISNULL(OUTL.SHIP_CD_L,'')         AS TOKUISAKI_CD         --お得意先コード    " & vbNewLine _
                                        & "--     ,ISNULL(DEST2.DEST_NM,'')          AS TOKUISAKI_NM         --お得意先名称      " & vbNewLine _
                                        & "--    	,ISNULL(EDIL.FREE_C24,'')         AS TOKUISAKI_CD         --お得意先コード    " & vbNewLine _
                                        & "--     ,ISNULL(EDIL.FREE_C25,'')          AS TOKUISAKI_NM         --お得意先名称      " & vbNewLine _
                                        & "    	,CASE WHEN EDIM2.FREE_C01 is null THEN OUTL.DEST_CD                            " & vbNewLine _
                                        & "    	                                   ELSE ISNULL(EDIL.FREE_C24,'')               " & vbNewLine _
                                        & "    	 END                               AS TOKUISAKI_CD         --お得意先コード    " & vbNewLine _
                                        & "     ,CASE WHEN EDIM2.FREE_C01 is null THEN MD.DEST_NM                              " & vbNewLine _
                                        & "                                       ELSE ISNULL(EDIL.FREE_C25,'')                " & vbNewLine _
                                        & "      END                               AS TOKUISAKI_NM         --お得意先名称      " & vbNewLine _
                                        & "     ,OUTL.CUST_ORD_NO                  AS CUST_ORD_NO          --荷主注文番号      " & vbNewLine _
                                        & "--     ,OUTL.BUYER_ORD_NO                 AS BUYER_ORD_NO         --発注番号 UPD 2021/02/01 " & vbNewLine _
                                        & "    	,CASE WHEN EDIM2.FREE_C01 is null THEN OUTL.BUYER_ORD_NO                       " & vbNewLine _
                                        & "    	                                  ELSE ISNULL(EDIM2.BUYER_ORD_NO_DTL,'')      " & vbNewLine _
                                        & "    	 END                               AS BUYER_ORD_NO         --発注番号          " & vbNewLine _
                                        & "     ,OUTL.DEST_CD                      AS DEST_CD              --お届け先コード    " & vbNewLine _
                                        & "     ,MD.DEST_NM                        AS DEST_NM              --お届け先名称      " & vbNewLine _
                                        & "     ,OUTL.DEST_AD_1                    AS DEST_AD_1            --お届け先住所１    " & vbNewLine _
                                        & "     ,OUTL.DEST_AD_2                    AS DEST_AD_2            --お届け先住所２    " & vbNewLine _
                                        & "     ,OUTL.DEST_AD_3                    AS DEST_AD_3            --お届け先住所３    " & vbNewLine _
                                        & "     ,MG.GOODS_CD_CUST                  AS GOODS_CD_CUST        --商品コード        " & vbNewLine _
                                        & "     ,MG.GOODS_NM_1                     AS GOODS_NM             --商品名            " & vbNewLine _
                                        & "     ,MG.JAN_CD                         AS JAN_CD               --JANコード         " & vbNewLine _
                                        & "--   ,MG.PKG_NB                         AS IRISU                --入数              " & vbNewLine _
                                        & "--   ,MG.STD_IRIME_NB                   AS IRISU                --入数  ケース対応で修正 " & vbNewLine _
                                        & "     ,MGD.SET_NAIYO                     AS IRISU                --入数  ケース対応で修正 " & vbNewLine _
                                        & "     ,OUTM.OUTKA_TTL_NB                 AS OUTKA_M_PKG_NB       --ケース            " & vbNewLine _
                                        & "--UUPD2020/10/05 ,OUTM.OUTKA_TTL_QT     AS OUTKA_TTL_QT         --数量（PCS）       " & vbNewLine _
                                        & "     ,OUTM.OUTKA_TTL_NB * MGD.SET_NAIYO AS OUTKA_TTL_QT         --数量（PCS)        " & vbNewLine _
                                        & "     ,OUTM.LOT_NO                       AS LOT_NO               --ロット№ & ケース " & vbNewLine _
                                        & "--   ,OM.OUTKA_M_PKG_NB                 AS OUTKA_M_PKG_NB_TOTAL --合計ケース        " & vbNewLine _
                                        & "--   ,OM.OUTKA_TTL_QT                   AS OUTKA_TTL_QT_TOTAL   --合計数量（PCS）   " & vbNewLine _
                                        & "     ,case WHEN EDIM2.FREE_C01 is null THEN OUTM.OUTKA_M_PKG_NB                     " & vbNewLine _
                                        & "                                       ELSE OM.OUTKA_M_PKG_NB                       " & vbNewLine _
                                        & "     END AS OUTKA_M_PKG_NB_TOTAL                                --合計ケース        " & vbNewLine _
                                        & "--   ,case WHEN EDIM2.FREE_C01 is null THEN OUTM.OUTKA_TTL_QT                       " & vbNewLine _
                                        & "--                                       ELSE OM.OUTKA_TTL_QT                       " & vbNewLine _
                                        & "--   ,OUTM.OUTKA_TTL_NB                 AS OUTKA_TTL_QT_TOTAL   --合計数量（PCS）   " & vbNewLine _
                                        & "     ,case WHEN EDIM2.FREE_C01 is null THEN OUTM.OUTKA_M_PKG_NB * MGD.SET_NAIYO     " & vbNewLine _
                                        & "                                       ELSE OM.OUTKA_M_PKG_NB   * MGD.SET_NAIYO     " & vbNewLine _
                                        & "     END AS OUTKA_TTL_QT_TOTAL                                  --合計数量（PCS）   " & vbNewLine _
                                        & "     ,MUCO.UNSOCO_CD + '--' + MUCO.UNSOCO_BR_CD AS UNSOCO_CD    --配送業者コード    " & vbNewLine _
                                        & "     ,OUTL.DENP_NO                      AS DENP_NO              --送り状整理№      " & vbNewLine _
                                        & "     ,OUTL.REMARK                       AS REMARK               --出荷時注意事項    " & vbNewLine _
                                        & "     ,OUTL.OUTKA_NO_L                   AS OUTKA_NO_L           --出荷管理番号      " & vbNewLine _
                                        & "--     ,ISNULL(EDIM2.FREE_C01,'')          AS FREE_C01                                " & vbNewLine _
                                        & "     ,ISNULL(EDIM2.FREE_C01,'X' + outm.OUTKA_NO_M)          AS FREE_C01             " & vbNewLine _
                                        & "     ------------------------------                                                 " & vbNewLine _
                                        & "--     ,OMG1.LOT_NO                       AS LOT_NOG1                                 " & vbNewLine _
                                        & "--     ,OMG1.OUTKA_M_PKG_NB               AS OUTKA_M_PKG_NBG1                         " & vbNewLine _
                                    & ",case WHEN EDIM2.FREE_C01 is null THEN OUTM.LOT_NO                                  " & vbNewLine _
                                    & "                                  ELSE OMG1.LOT_NO                                  " & vbNewLine _
                                    & " END AS LOT_NOG1                                                                    " & vbNewLine _
                                    & ",case WHEN EDIM2.FREE_C01 is null THEN OUTM.OUTKA_M_PKG_NB                            " & vbNewLine _
                                    & "                                  ELSE OMG1.OUTKA_M_PKG_NB                          " & vbNewLine _
                                    & " END AS OUTKA_M_PKG_NBG1                                                            " & vbNewLine _
                                        & "     ,ISNULL(OMG2.LOT_NO,'')            AS LOT_NOG2                                 " & vbNewLine _
                                        & "     ,ISNULL(OMG2.OUTKA_M_PKG_NB,0)     AS OUTKA_M_PKG_NBG2                         " & vbNewLine _
                                        & "     ,ISNULL(OMG3.LOT_NO,'')            AS LOT_NOG3                                 " & vbNewLine _
                                        & "     ,ISNULL(OMG3.OUTKA_M_PKG_NB,0)     AS OUTKA_M_PKG_NBG3                         " & vbNewLine _
                                        & "     ,OUTM.CUST_ORD_NO_DTL               --オーダー番号 --ADD 2021/01/19 017853     " & vbNewLine


    Private Const SQL_SELECT_MAIN_JJ15_FROM As String = "-- ★★★SQLSQL_SELECT_MAIN_JJ15_FROM  ★★★                       " & vbNewLine _
                                        & "   -- C_OUTKA_L                                             " & vbNewLine _
                                        & "   FROM $LM_TRN$..C_OUTKA_L  AS OUTL                        " & vbNewLine _
                                        & "   --C_OUTKA_M                                              " & vbNewLine _
                                        & "   LEFT JOIN $LM_TRN$..C_OUTKA_M  AS OUTM                   " & vbNewLine _
                                        & "     ON OUTM.NRS_BR_CD  = OUTL.NRS_BR_CD                    " & vbNewLine _
                                        & "    AND OUTM.OUTKA_NO_L  = OUTL.OUTKA_NO_L                  " & vbNewLine _
                                        & "    AND OUTM.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                        & "    --まとめ出荷時対応                                      " & vbNewLine _
                                        & "    AND OUTM.CUST_ORD_NO_DTL = CASE WHEN OUTM.CUST_ORD_NO_DTL = ''  " & vbNewLine _
                                        & "                                    THEN ''                         " & vbNewLine _
                                        & "                                    ELSE @ORDER_NO                  " & vbNewLine _
                                        & "                               END                                  " & vbNewLine _
                                        & "   LEFT JOIN $LM_MST$..M_CUST CUST                          " & vbNewLine _
                                        & "     ON    OUTL.NRS_BR_CD = CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "    AND    OUTL.CUST_CD_L  = CUST.CUST_CD_L                 " & vbNewLine _
                                        & "    AND    OUTL.CUST_CD_M  = CUST.CUST_CD_M                 " & vbNewLine _
                                        & "    AND    CUST.CUST_CD_S  = '00'                           " & vbNewLine _
                                        & "    AND    CUST.CUST_CD_SS = '00'                           " & vbNewLine _
                                        & "   --運送L  F_UNSO_L                                        " & vbNewLine _
                                        & "   LEFT JOIN $LM_TRN$..F_UNSO_L UL                          " & vbNewLine _
                                        & "     ON UL.NRS_BR_CD = OUTL.NRS_BR_CD                       " & vbNewLine _
                                        & "    AND UL.INOUTKA_NO_L = OUTL.OUTKA_NO_L                   " & vbNewLine _
                                        & "    AND UL.MOTO_DATA_KB = '20'                              " & vbNewLine _
                                        & "    AND UL.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                        & "   --運送会社M M_UNSOCO                                     " & vbNewLine _
                                        & "   LEFT JOIN $LM_MST$..M_UNSOCO MUCO                        " & vbNewLine _
                                        & "     ON MUCO.NRS_BR_CD     = UL.NRS_BR_CD                   " & vbNewLine _
                                        & "    AND MUCO.UNSOCO_CD    = UL.UNSO_CD                      " & vbNewLine _
                                        & "    AND MUCO.UNSOCO_BR_CD = UL.UNSO_BR_CD                   " & vbNewLine _
                                        & "    AND MUCO.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                        & "   --商品M  M_GOODS                                         " & vbNewLine _
                                        & "   LEFT JOIN $LM_MST$..M_GOODS MG                           " & vbNewLine _
                                        & "    ON MG.NRS_BR_CD = OUTM.NRS_BR_CD                        " & vbNewLine _
                                        & "   AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                  " & vbNewLine _
                                        & "   ----商品詳細　ADD 20201126                               " & vbNewLine _
                                        & "   LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                  " & vbNewLine _
                                        & "    ON MGD.NRS_BR_CD    = OUTM.NRS_BR_CD                    " & vbNewLine _
                                        & "   AND MGD.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                 " & vbNewLine _
                                        & "   AND MGD.SUB_KB       = '68'  --1梱包当たりの最大値 (納品書の入数で使用) " & vbNewLine _
                                        & "   AND MGD.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                        & "   --届先M(届先取得)(出荷L参照)                             " & vbNewLine _
                                        & "   LEFT JOIN $LM_MST$..M_DEST MD                            " & vbNewLine _
                                        & "    ON MD.NRS_BR_CD   = OUTL.NRS_BR_CD                      " & vbNewLine _
                                        & "   AND MD.CUST_CD_L   = OUTL.CUST_CD_L                      " & vbNewLine _
                                        & "   AND MD.DEST_CD     = OUTL.DEST_CD                        " & vbNewLine _
                                        & "   AND MD.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "   --届先M(売上先取得)(出荷L参照)                           " & vbNewLine _
                                        & "   LEFT JOIN $LM_MST$..M_DEST    DEST2                      " & vbNewLine _
                                        & "   ON     OUTL.NRS_BR_CD = DEST2.NRS_BR_CD                  " & vbNewLine _
                                        & "   AND    OUTL.CUST_CD_L = DEST2.CUST_CD_L                  " & vbNewLine _
                                        & "   AND    OUTL.SHIP_CD_L = DEST2.DEST_CD                    " & vbNewLine _
                                        & "     ----出荷EDIL                                           " & vbNewLine _
                                        & "   LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                    " & vbNewLine _
                                        & "    ON EDIL.NRS_BR_CD        = OUTL.NRS_BR_CD              " & vbNewLine _
                                        & "   AND EDIL.OUTKA_CTL_NO     = OUTL.OUTKA_NO_L             " & vbNewLine _
                                        & "   AND EDIL.SYS_DEL_FLG      = '0'                         " & vbNewLine _
                                        & "     ----出荷EDIM                                           " & vbNewLine _
                                        & "   LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM2                   " & vbNewLine _
                                        & "    ON EDIM2.NRS_BR_CD        = OUTM.NRS_BR_CD              " & vbNewLine _
                                        & "   AND EDIM2.OUTKA_CTL_NO     = OUTM.OUTKA_NO_L             " & vbNewLine _
                                        & "   AND EDIM2.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M             " & vbNewLine _
                                        & "   AND EDIM2.SYS_DEL_FLG      = '0'                         " & vbNewLine _
                                        & "   -- C_OUTKA_M OUTKA_NO_L・EDIM.FREE_C01で集計             " & vbNewLine _
                                        & "   LEFT JOIN (                                              " & vbNewLine _
                                        & "     SELECT                                                 " & vbNewLine _
                                        & "       C_OUTKA_M.OUTKA_NO_L AS OUTKA_NO_L                   " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_M_PKG_NB) AS OUTKA_M_PKG_NB      " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_TTL_QT)   AS OUTKA_TTL_QT        " & vbNewLine _
                                        & "      ,EDIM.FREE_C01                 AS FREE_C01            " & vbNewLine _
                                        & "     FROM  $LM_TRN$..C_OUTKA_M                              " & vbNewLine _
                                        & "       ----出荷EDIM                                         " & vbNewLine _
                                        & "   	LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                  " & vbNewLine _
                                        & "   	 ON EDIM.NRS_BR_CD        = C_OUTKA_M.NRS_BR_CD        " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO     = C_OUTKA_M.OUTKA_NO_L       " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO_CHU = C_OUTKA_M.OUTKA_NO_M       " & vbNewLine _
                                        & "   	AND EDIM.SYS_DEL_FLG      = '0'                        " & vbNewLine _
                                        & "     WHERE C_OUTKA_M.NRS_BR_CD   = @NRS_BR_CD               " & vbNewLine _
                                        & "       AND C_OUTKA_M.OUTKA_NO_L  = @KANRI_NO_L  --          " & vbNewLine _
                                        & "       AND C_OUTKA_M.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                        & "       AND EDIM.CUST_ORD_NO_DTL = @ORDER_NO   --ADD 2021/01/19 017853 " & vbNewLine _
                                        & "     GROUP BY C_OUTKA_M.OUTKA_NO_L,EDIM.CUST_ORD_NO_DTL,EDIM.FREE_C01) AS OM     " & vbNewLine _
                                        & "     ON  OM.OUTKA_NO_L = @KANRI_NO_L                        " & vbNewLine _
                                        & "     AND OM.FREE_C01   = EDIM2.FREE_C01                     " & vbNewLine _
                                        & "   -- C_OUTKA_M 出荷伝票明細番号(EDI FREE_C01) でロット番号ごと集計1 " & vbNewLine _
                                        & "   LEFT JOIN (                                              " & vbNewLine _
                                        & "     SELECT                                                " & vbNewLine _
                                        & "       ROW_NUMBER() OVER (PARTITION BY C_OUTKA_M.OUTKA_NO_L , EDIM.FREE_C01  ORDER BY EDIM.FREE_C01) AS FREE_C01の連番, " & vbNewLine _
                                        & "       C_OUTKA_M.OUTKA_NO_L AS OUTKA_NO_L                  " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_M_PKG_NB) AS OUTKA_M_PKG_NB     " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_TTL_QT)   AS OUTKA_TTL_QT       " & vbNewLine _
                                        & "      ,C_OUTKA_M.LOT_NO                                    " & vbNewLine _
                                        & "      ,EDIM.FREE_C01                 AS FREE_C01           " & vbNewLine _
                                        & "     FROM  $LM_TRN$..C_OUTKA_M                             " & vbNewLine _
                                        & "     ----出荷EDIM                                          " & vbNewLine _
                                        & "   	LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                 " & vbNewLine _
                                        & "   	 ON EDIM.NRS_BR_CD        = C_OUTKA_M.NRS_BR_CD       " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO     = C_OUTKA_M.OUTKA_NO_L      " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO_CHU = C_OUTKA_M.OUTKA_NO_M      " & vbNewLine _
                                        & "   	AND EDIM.SYS_DEL_FLG      = '0'                       " & vbNewLine _
                                        & "     WHERE C_OUTKA_M.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
                                        & "       AND C_OUTKA_M.OUTKA_NO_L  = @KANRI_NO_L             " & vbNewLine _
                                        & "       AND C_OUTKA_M.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                        & "       AND EDIM.CUST_ORD_NO_DTL = @ORDER_NO   --ADD 2021/01/19 017853 " & vbNewLine _
                                        & "     GROUP BY C_OUTKA_M.OUTKA_NO_L                         " & vbNewLine _
                                        & "             ,EDIM.CUST_ORD_NO_DTL   --ADD 2021/01/19 017853   " & vbNewLine _
                                        & "             ,EDIM.FREE_C01                                " & vbNewLine _
                                        & "             ,C_OUTKA_M.LOT_NO)  AS OMG1                   " & vbNewLine _
                                        & "       ON  OMG1.OUTKA_NO_L = @KANRI_NO_L                   " & vbNewLine _
                                        & "   	AND OMG1.FREE_C01   = EDIM2.FREE_C01                  " & vbNewLine _
                                        & "   	AND OMG1.FREE_C01の連番 = 1                           " & vbNewLine _
                                        & "   	-- C_OUTKA_M 出荷伝票明細番号(EDI FREE_C01) でロット番号ごと集計2 " & vbNewLine _
                                        & "   LEFT JOIN (                                            " & vbNewLine _
                                        & "     SELECT                                               " & vbNewLine _
                                        & "       ROW_NUMBER() OVER (PARTITION BY C_OUTKA_M.OUTKA_NO_L , EDIM.FREE_C01  ORDER BY EDIM.FREE_C01) AS FREE_C01の連番, " & vbNewLine _
                                        & "       C_OUTKA_M.OUTKA_NO_L AS OUTKA_NO_L                 " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_M_PKG_NB) AS OUTKA_M_PKG_NB    " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_TTL_QT)   AS OUTKA_TTL_QT      " & vbNewLine _
                                        & "      ,C_OUTKA_M.LOT_NO                                   " & vbNewLine _
                                        & "      ,EDIM.FREE_C01                 AS FREE_C01          " & vbNewLine _
                                        & "     FROM  $LM_TRN$..C_OUTKA_M                            " & vbNewLine _
                                        & "     ----出荷EDIM                                         " & vbNewLine _
                                        & "   	LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM                " & vbNewLine _
                                        & "   	 ON EDIM.NRS_BR_CD        = C_OUTKA_M.NRS_BR_CD      " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO     = C_OUTKA_M.OUTKA_NO_L     " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO_CHU = C_OUTKA_M.OUTKA_NO_M     " & vbNewLine _
                                        & "   	AND EDIM.SYS_DEL_FLG      = '0'                      " & vbNewLine _
                                        & "     WHERE C_OUTKA_M.NRS_BR_CD   = @NRS_BR_CD             " & vbNewLine _
                                        & "       AND C_OUTKA_M.OUTKA_NO_L  = @KANRI_NO_L            " & vbNewLine _
                                        & "       AND C_OUTKA_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                        & "       AND EDIM.CUST_ORD_NO_DTL = @ORDER_NO   --ADD 2021/01/19 017853 " & vbNewLine _
                                        & "     GROUP BY C_OUTKA_M.OUTKA_NO_L                       " & vbNewLine _
                                        & "             ,EDIM.CUST_ORD_NO_DTL   --ADD 2021/01/19 017853   " & vbNewLine _
                                        & "             ,EDIM.FREE_C01                              " & vbNewLine _
                                        & "             ,C_OUTKA_M.LOT_NO)  AS OMG2                 " & vbNewLine _
                                        & "       ON  OMG2.OUTKA_NO_L = @KANRI_NO_L                 " & vbNewLine _
                                        & "   	AND OMG2.FREE_C01   = EDIM2.FREE_C01                " & vbNewLine _
                                        & "   	AND OMG2.FREE_C01の連番 = 2                         " & vbNewLine _
                                        & "   		   -- C_OUTKA_M 出荷伝票明細番号(EDI FREE_C01) でロット番号ごと集計3 " & vbNewLine _
                                        & "   LEFT JOIN ( " & vbNewLine _
                                        & "     SELECT  " & vbNewLine _
                                        & "       ROW_NUMBER() OVER (PARTITION BY C_OUTKA_M.OUTKA_NO_L , EDIM.FREE_C01  ORDER BY EDIM.FREE_C01) AS FREE_C01の連番, " & vbNewLine _
                                        & "       C_OUTKA_M.OUTKA_NO_L AS OUTKA_NO_L " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_M_PKG_NB) AS OUTKA_M_PKG_NB " & vbNewLine _
                                        & "      ,SUM(C_OUTKA_M.OUTKA_TTL_QT)   AS OUTKA_TTL_QT " & vbNewLine _
                                        & "      ,C_OUTKA_M.LOT_NO " & vbNewLine _
                                        & "      ,EDIM.FREE_C01                 AS FREE_C01 " & vbNewLine _
                                        & "     FROM  $LM_TRN$..C_OUTKA_M " & vbNewLine _
                                        & "     ----出荷EDIM                            " & vbNewLine _
                                        & "   	LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EDIM    " & vbNewLine _
                                        & "   	 ON EDIM.NRS_BR_CD        = C_OUTKA_M.NRS_BR_CD   " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO     = C_OUTKA_M.OUTKA_NO_L  " & vbNewLine _
                                        & "   	AND EDIM.OUTKA_CTL_NO_CHU = C_OUTKA_M.OUTKA_NO_M   " & vbNewLine _
                                        & "   	AND EDIM.SYS_DEL_FLG      = '0'   " & vbNewLine _
                                        & "     AND EDIM.CUST_ORD_NO_DTL = @ORDER_NO   --ADD 2021/01/19 017853 " & vbNewLine _
                                        & "     WHERE C_OUTKA_M.NRS_BR_CD   = @NRS_BR_CD          " & vbNewLine _
                                        & "       AND C_OUTKA_M.OUTKA_NO_L  = @KANRI_NO_L   " & vbNewLine _
                                        & "       AND C_OUTKA_M.SYS_DEL_FLG = '0' " & vbNewLine _
                                        & "       AND EDIM.CUST_ORD_NO_DTL = @ORDER_NO   --ADD 2021/01/19 017853 " & vbNewLine _
                                        & "     GROUP BY C_OUTKA_M.OUTKA_NO_L " & vbNewLine _
                                        & "             ,EDIM.CUST_ORD_NO_DTL   --ADD 2021/01/19 017853   " & vbNewLine _
                                        & "             ,EDIM.FREE_C01 " & vbNewLine _
                                        & "             ,C_OUTKA_M.LOT_NO)  AS OMG3 " & vbNewLine _
                                        & "       ON  OMG3.OUTKA_NO_L = @KANRI_NO_L " & vbNewLine _
                                        & "   	AND OMG3.FREE_C01   = EDIM2.FREE_C01 " & vbNewLine _
                                        & "   	AND OMG3.FREE_C01の連番 = 3               " & vbNewLine _
                                        & "--出荷Lでの荷主帳票パターン取得                                                                                                " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                            " & vbNewLine _
                                        & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                            " & vbNewLine _
                                        & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                                                                            " & vbNewLine _
                                        & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                                                                            " & vbNewLine _
                                        & "AND '00' = MCR1.CUST_CD_S                                                                                                      " & vbNewLine _
                                        & "AND MCR1.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                        & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                  " & vbNewLine _
                                        & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                                                             " & vbNewLine _
                                        & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                   " & vbNewLine _
                                        & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                   " & vbNewLine _
                                        & "AND MR1.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                        & "--商品Mの荷主での荷主帳票パターン取得                                                                                          " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                            " & vbNewLine _
                                        & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                              " & vbNewLine _
                                        & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                              " & vbNewLine _
                                        & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                              " & vbNewLine _
                                        & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                              " & vbNewLine _
                                        & "AND MCR2.PTN_ID = '04'                                                                                                         " & vbNewLine _
                                        & "--帳票パターン取得                                                                                                             " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                  " & vbNewLine _
                                        & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                                                             " & vbNewLine _
                                        & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                   " & vbNewLine _
                                        & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                   " & vbNewLine _
                                        & "AND MR2.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                        & "--存在しない場合の帳票パターン取得                                                                                             " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                  " & vbNewLine _
                                        & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                             " & vbNewLine _
                                        & "AND MR3.PTN_ID = '04'                                                                                                          " & vbNewLine _
                                        & "AND MR3.STANDARD_FLAG = '01'                                                                                                   " & vbNewLine _
                                        & "AND MR3.SYS_DEL_FLG = '0'                                                                                                      " & vbNewLine _
                                        & "   WHERE OUTL.NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
                                        & "     AND OUTL.OUTKA_NO_L = @KANRI_NO_L " & vbNewLine _
                                        & "     AND OUTL.SYS_DEL_FLG = '0' " & vbNewLine _
                                        & ") MAIN" & vbNewLine


    Private Const SQL_SELECT_MAIN_JJ15_GROUP As String = "-- ★★★SQLSQL_SELECT_MAIN_JJ15_GROUP   ★★★                       " & vbNewLine _
                                        & "GROUP BY            " & vbNewLine _
                                        & "  RPT_ID            " & vbNewLine _
                                        & " ,NRS_BR_CD         " & vbNewLine _
                                        & " ,CUST_ORD_NO_DTL  --ADD 2021/01/19 017853 " & vbNewLine _
                                        & " ,FREE_C01          " & vbNewLine _
                                        & " ,NOUHINBI          " & vbNewLine _
                                        & " ,NENGAPPI          " & vbNewLine _
                                        & " ,TOKUISAKI_CD      " & vbNewLine _
                                        & " ,TOKUISAKI_NM      " & vbNewLine _
                                        & " ,CUST_ORD_NO       " & vbNewLine _
                                        & " ,BUYER_ORD_NO      " & vbNewLine _
                                        & " ,OUTKA_NO_L        " & vbNewLine _
                                        & " ,REMARK            " & vbNewLine _
                                        & " ,DEST_CD           " & vbNewLine _
                                        & " ,DEST_NM           " & vbNewLine _
                                        & " ,DEST_AD_1         " & vbNewLine _
                                        & " ,DEST_AD_2         " & vbNewLine _
                                        & " ,DEST_AD_3         " & vbNewLine _
                                        & " ,GOODS_CD_CUST     " & vbNewLine _
                                        & " ,GOODS_NM          " & vbNewLine _
                                        & " ,JAN_CD            " & vbNewLine _
                                        & " ,IRISU             " & vbNewLine _
                                        & " ,UNSOCO_CD         " & vbNewLine _
                                        & " ,CUST_CD_L         " & vbNewLine _
                                        & " ,CUST_CD_M         " & vbNewLine _
                                        & " ,CUST_NM_L         " & vbNewLine _
                                        & " ,CUST_NM_M         " & vbNewLine _
                                        & " ,CUST_NM_M         " & vbNewLine _
                                        & " ,CUST_NM_S         " & vbNewLine _
                                        & " ,CUST_NM_SS        " & vbNewLine _
                                        & " ,CUST_AD_1         " & vbNewLine _
                                        & " ,CUST_AD_2         " & vbNewLine _
                                        & " ,CUST_AD_3         " & vbNewLine _
                                        & " ,DENP_NO           " & vbNewLine _
                                        & "ORDER BY            " & vbNewLine _
                                        & "  OUTKA_NO_L        " & vbNewLine _
                                        & " ,CUST_ORD_NO_DTL  --ADD 2021/01/19 017853  " & vbNewLine _
                                        & " ,FREE_C01          " & vbNewLine




#End If
#End Region

#Region "SELECT句(荷主明細)"
    '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
    ''' <summary>
    ''' 設定値(荷主明細マスタ)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCUST_DETAILS As String = "SELECT                        " & vbNewLine _
                                                & " SET_NAIYO    AS SET_NAIYO              " & vbNewLine _
                                                & "FROM                                    " & vbNewLine _
                                                & "$LM_MST$..M_CUST_DETAILS MCD            " & vbNewLine _
                                                & "RIGHT JOIN                              " & vbNewLine _
                                                & "(SELECT                                 " & vbNewLine _
                                                & " CUST_CD_L                              " & vbNewLine _
                                                & " FROM $LM_TRN$..C_OUTKA_L               " & vbNewLine _
                                                & " WHERE                                  " & vbNewLine _
                                                & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
                                                & " AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L " & vbNewLine _
                                                & " ) CL                                   " & vbNewLine _
                                                & "ON MCD.CUST_CD = CL.CUST_CD_L           " & vbNewLine _
                                                & "WHERE                                   " & vbNewLine _
                                                & "MCD.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                                & "AND MCD.SUB_KB = '25'                   " & vbNewLine
    '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --
#End Region

#Region "SELECT句(出荷管理番号のオーダー番号取得)"
    'ADD2021/01/1018 LMS】JJ_追加要望_まとめ出荷を行った場合も納品書はオーダー毎に印刷して欲しい -- START --
    ''' <summary>
    ''' 設定値(出荷管理番号のオーダー番号)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUT_ORD_NO As String = "SELECT                        " & vbNewLine _
                & "	 NRS_BR_CD	                                                   " & vbNewLine _
                & "	,OUTKA_NO_L	                                                   " & vbNewLine _
                & "	,CHKCUST_ORD_NO	                                               " & vbNewLine _
                & "	FROM  (	                                                       " & vbNewLine _
                & "	SELECT 	                                                       " & vbNewLine _
                & "	  COM.NRS_BR_CD         AS NRS_BR_CD 	                       " & vbNewLine _
                & "	 ,COM.OUTKA_NO_L        AS OUTKA_NO_L	                       " & vbNewLine _
                & "	 ,COM.CUST_ORD_NO_DTL   AS COMCUST_ORD_NO_DTL	               " & vbNewLine _
                & "	 ,HOM.CUST_ORD_NO_DTL   AS HOMCUST_ORD_NO_DTL	               " & vbNewLine _
                & "	 ,CASE WHEN COM.CUST_ORD_NO_DTL <>  '' THEN COM.CUST_ORD_NO_DTL	 " & vbNewLine _
                & "	      WHEN COM.CUST_ORD_NO_DTL = '' AND ( HOM.CUST_ORD_NO_DTL IS NOT NULL)	 " & vbNewLine _
                & "	                         THEN HOM.CUST_ORD_NO_DTL	             " & vbNewLine _
                & "	                        ELSE ''	                                 " & vbNewLine _
                & "	 END  AS CHKCUST_ORD_NO	                                         " & vbNewLine _
                & "	FROM LM_TRN_15..C_OUTKA_M  COM	                                 " & vbNewLine _
                & "	LEFT JOIN LM_TRN_15..H_OUTKAEDI_M HOM	                         " & vbNewLine _
                & "	  ON HOM.NRS_BR_CD = COM.NRS_BR_CD	                             " & vbNewLine _
                & "	 AND HOM.OUTKA_CTL_NO = COM.OUTKA_NO_L	                         " & vbNewLine _
                & "	 AND HOM.OUTKA_CTL_NO_CHU = COM.OUTKA_NO_M	                     " & vbNewLine _
                & "	 AND HOM.SYS_DEL_FLG = '0'	                                     " & vbNewLine _
                & "	WHERE COM.NRS_BR_CD= @NRS_BR_CD                                  " & vbNewLine _
                & "	  AND COM.OUTKA_NO_L = @OUTKA_NO_L	                             " & vbNewLine _
                & "	  AND COM.SYS_DEL_FLG = '0'	                                     " & vbNewLine _
                & "	  ) MAIN	                                                     " & vbNewLine _
                & "	GROUP BY NRS_BR_CD,OUTKA_NO_L,CHKCUST_ORD_NO                     " & vbNewLine
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

    ''' <summary>
    ''' 納品書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC500IN")
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")


        '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
        Dim FreeC03_Umu As String = String.Empty
        Me.SelectMCustDetailsData(ds)
        If ds.Tables("SET_NAIYO").Rows.Count > 0 Then
            FreeC03_Umu = ds.Tables("SET_NAIYO").Rows(0)("SET_NAIYO").ToString()
        End If
        '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '種別フラグを取得('0':出荷、'1':運送)
        Dim ptnFlag As String = Me._Row.Item("PTN_FLAG").ToString()

        ''20120124 INSERT START OIKAWA OOSAKA対応
        '営業所CDを取得
        Dim nrs_br_cd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString()
        ''20120124 INSERT END OIKAWA OOSAKA対応)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '20160122 埼玉ゴードー tsunehira add start
        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC815" Then
            '通常
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_815)
            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
            '20160122 埼玉ゴードー tsunehira add end
#If True Then  'ADD 2020/04/30 007999　
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC545" Then
            '通常
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_MAIN_JJ15)      'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_MAIN_JJ15_SELECT)            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_MAIN_JJ15_FROM)
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_MAIN_JJ15_GROUP)
            Call Me.SetConditionMasterSQL()                          'SQL            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
#End If
        ElseIf nrs_br_cd.Equals("20") = False Then
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_737)         'SQL構築(データ抽出用Select句)'20120731 群馬
            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
        Else
            '大阪物流センタ－のみ
            Me._StrSql.Append(LMC501DAC.SQL_SELECT_737_OSK)       'SQL構築(データ抽出用Select句)

            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
        End If


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        '20120613----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC501DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC545" Then

            map.Add("RPT_ID", "RPT_ID")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("NOUHINBI", "NOUHINBI")
            map.Add("NENGAPPI", "NENGAPPI")
            map.Add("CUST_NM_L", "CUST_NM_L")
            map.Add("CUST_NM_M", "CUST_NM_M")
            map.Add("CUST_NM_S", "CUST_NM_S")
            map.Add("CUST_NM_SS", "CUST_NM_SS")
            map.Add("CUST_AD_1", "CUST_AD_1")
            map.Add("CUST_AD_2", "CUST_AD_2")
            map.Add("CUST_AD_3", "CUST_AD_3")
            map.Add("TOKUISAKI_CD", "TOKUISAKI_CD")
            map.Add("TOKUISAKI_NM", "TOKUISAKI_NM")
            map.Add("CUST_ORD_NO", "CUST_ORD_NO")
            map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
            map.Add("OUTKA_NO_L", "OUTKA_NO_L")
            map.Add("DEST_CD", "DEST_CD")
            map.Add("DEST_NM", "DEST_NM")
            map.Add("DEST_AD_1", "DEST_AD_1")
            map.Add("DEST_AD_2", "DEST_AD_2")
            map.Add("DEST_AD_3", "DEST_AD_3")
            map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
            map.Add("GOODS_NM", "GOODS_NM")
            map.Add("JAN_CD", "JAN_CD")
            map.Add("IRISU", "IRISU")
            map.Add("UNSOCO_CD", "UNSOCO_CD")
            map.Add("REMARK", "REMARK")
            map.Add("DENP_NO", "DENP_NO")
            map.Add("R_NO", "R_NO")
            map.Add("FREE_C01", "FREE_C01")
            map.Add("OUTKA_M_PKG_NB_TOTAL", "OUTKA_M_PKG_NB_TOTAL")
            map.Add("OUTKA_TTL_QT_TOTAL", "OUTKA_TTL_QT_TOTAL")
            map.Add("LOT_NOG1", "LOT_NOG1")
            map.Add("OUTKA_M_PKG_NBG1", "OUTKA_M_PKG_NBG1")
            map.Add("LOT_NOG2", "LOT_NOG2")
            map.Add("OUTKA_M_PKG_NBG2", "OUTKA_M_PKG_NBG2")
            map.Add("LOT_NOG3", "LOT_NOG3")
            map.Add("OUTKA_M_PKG_NBG3", "OUTKA_M_PKG_NBG3")
            map.Add("ORDER_NO", "ORDER_NO")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC545OUT")

        Else

            '通常 納品書
            map.Add("RPT_ID", "RPT_ID")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("PRINT_SORT", "PRINT_SORT")
            map.Add("DEST_CD", "DEST_CD")
            map.Add("DEST_NM", "DEST_NM")
            map.Add("DEST_AD_1", "DEST_AD_1")
            map.Add("DEST_AD_2", "DEST_AD_2")
            map.Add("DEST_AD_3", "DEST_AD_3")
            map.Add("DEST_TEL", "DEST_TEL")
            map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
            map.Add("CUST_ORD_NO", "CUST_ORD_NO")
            map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
            map.Add("UNSOCO_NM", "UNSOCO_NM")
            map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
            map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
            map.Add("OUTKA_NO_L", "OUTKA_NO_L")
            map.Add("URIG_NM", "URIG_NM")
            map.Add("CUST_NM_L", "CUST_NM_L")
            map.Add("CUST_AD_1", "CUST_AD_1")
            map.Add("CUST_AD_2", "CUST_AD_2")
            map.Add("CUST_TEL", "CUST_TEL")
            map.Add("NRS_BR_NM", "NRS_BR_NM")
            map.Add("NRS_BR_AD1", "NRS_BR_AD1")
            map.Add("NRS_BR_AD2", "NRS_BR_AD2")
            map.Add("NRS_BR_TEL", "NRS_BR_TEL")
            map.Add("NRS_BR_FAX", "NRS_BR_FAX")
            map.Add("PIC", "PIC")
            map.Add("OUTKA_NO_M", "OUTKA_NO_M")
            map.Add("OUTKA_NO_S", "OUTKA_NO_S")
            map.Add("INKA_DATE", "INKA_DATE")
            map.Add("REMARK_OUT", "REMARK_OUT")
            map.Add("GOODS_NM_1", "GOODS_NM_1")
            map.Add("LOT_NO", "LOT_NO")
            map.Add("SERIAL_NO", "SERIAL_NO")
            map.Add("IRIME", "IRIME")
            map.Add("PKG_NB", "PKG_NB")
            map.Add("KONSU", "KONSU")
            map.Add("HASU", "HASU")
            map.Add("ALCTD_NB", "ALCTD_NB")
            map.Add("PKG_UT", "PKG_UT")
            map.Add("PKG_UT_NM", "PKG_UT_NM")
            map.Add("ALCTD_QT", "ALCTD_QT")
            map.Add("IRIME_UT", "IRIME_UT")
            map.Add("WT", "WT")
            map.Add("UNSO_WT", "UNSO_WT")
            map.Add("TOU_NO", "TOU_NO")
            map.Add("SITU_NO", "SITU_NO")
            map.Add("ZONE_CD", "ZONE_CD")
            map.Add("LOCA", "LOCA")
            map.Add("ZAN_KONSU", "ZAN_KONSU")
            map.Add("ZAN_HASU", "ZAN_HASU")
            map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
            map.Add("REMARK_M", "REMARK_M")
            map.Add("CUST_NM_S", "CUST_NM_S")
            map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
            map.Add("OUTKA_REMARK", "OUTKA_REMARK")
            map.Add("HAISOU_REMARK", "HAISOU_REMARK")
            map.Add("NHS_REMARK", "NHS_REMARK")
            map.Add("KYORI", "KYORI")
            map.Add("OUTKA_PKG_NB_L", "OUTKA_PKG_NB_L")
            map.Add("SOKO_NM", "SOKO_NM")
            map.Add("BUSHO_NM", "BUSHO_NM")
            map.Add("NRS_BR_NM_NICHI", "NRS_BR_NM_NICHI")
            map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
            map.Add("ALCTD_KB", "ALCTD_KB")
            map.Add("PTN_FLAG", "PTN_FLAG")
            map.Add("CUST_CD_L", "CUST_CD_L")
            map.Add("ORDER_TYPE", "ORDER_TYPE")
            map.Add("ATSUKAISYA_NM", "ATSUKAISYA_NM")
            map.Add("CUST_NM_S_H", "CUST_NM_S_H")

            ''20120124 INSERT START OIKAWA OOSAKA対応)
            If nrs_br_cd.Equals("20") = True Then
                map.Add("TITLE_SMPL", "TITLE_SMPL")
                map.Add("LT_DATE", "LT_DATE")
                map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
                map.Add("GOODS_COND_CD", "GOODS_COND_CD")
                map.Add("GOODS_COND_NM", "GOODS_COND_NM")
                map.Add("REMARK_ZAI", "REMARK_ZAI")
                map.Add("BACKLOG_QT", "BACKLOG_QT")
                map.Add("DOKU_NM", "DOKU_NM")
                '【Notes】№728 作業項目出力対応 START ---------------------
                map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
                map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
                map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
                map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
                map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
                map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
                map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
                map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
                map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
                map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
                map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
                map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
                map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
                map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
                map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
                '【Notes】№728 作業項目出力対応 END ---------------------
                map.Add("TOU_SITU_NM", "TOU_SITU_NM")
                map.Add("JISYATASYA_KB", "JISYATASYA_KB")
            End If
            ''20120124 INSERT END OIKAWA OOSAKA対応)

            map.Add("PC_KB_NM", "PC_KB_NM")
            map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
            map.Add("TORI_KB", "TORI_KB")
            map.Add("SHIP_CD_L", "SHIP_CD_L")
            map.Add("SET_NAIYO_4", "SET_NAIYO_4")
            map.Add("SET_NAIYO_5", "SET_NAIYO_5")
            map.Add("SET_NAIYO_6", "SET_NAIYO_6")
            map.Add("GOODS_NM_2", "GOODS_NM_2")
            map.Add("KICHO_KB", "KICHO_KB")
            map.Add("SEI_YURAI_KB", "SEI_YURAI_KB")
            map.Add("REMARK_UPPER", "REMARK_UPPER")
            map.Add("REMARK_LOWER", "REMARK_LOWER")
            map.Add("GOODS_SYUBETU", "GOODS_SYUBETU")
            map.Add("YUKOU_KIGEN", "YUKOU_KIGEN")
            map.Add("GOODS_NM_3", "GOODS_NM_3")
            map.Add("EDISHIP_CD", "EDISHIP_CD")
            map.Add("EDIDEST_CD", "EDIDEST_CD")
            map.Add("SET_NAIYO_FROM1", "SET_NAIYO_FROM1")
            map.Add("SET_NAIYO_FROM2", "SET_NAIYO_FROM2")
            map.Add("SET_NAIYO_FROM3", "SET_NAIYO_FROM3")
            map.Add("CUST_AD_3", "CUST_AD_3")
            map.Add("DEST_REMARK", "DEST_REMARK")
            map.Add("DEST_SALES_CD", "DEST_SALES_CD")
            map.Add("DEST_SALES_NM", "DEST_SALES_NM")
            map.Add("DEST_SALES_AD_1", "DEST_SALES_AD_1")
            map.Add("DEST_SALES_AD_2", "DEST_SALES_AD_2")
            map.Add("DEST_SALES_AD_3", "DEST_SALES_AD_3")
            map.Add("DEST_SALES_TEL", "DEST_SALES_TEL")
            map.Add("CUST_NM_M", "CUST_NM_M")
            map.Add("DENPYO_NM", "DENPYO_NM")
            map.Add("HAISOU_REMARK_MST", "HAISOU_REMARK_MST")
            map.Add("GOODS_NM_CUST", "GOODS_NM_CUST")
            '20160121 ゴードー対応 tsunehira add start      
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC815" Then
                map.Add("GODO_GOODS_CODE", "GODO_GOODS_CODE")
            End If
            '20160121 ゴードー対応 tsunehira add start

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC500OUT")

        End If

        Return ds

    End Function




    '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
    ''' <summary>
    '''荷主明細マスタ(設定値)取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>荷主明細マスタ取得SQLの構築・発行</remarks>
    Private Function SelectMCustDetailsData(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMC500IN").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC501DAC.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
        Call Me.setIndataParameter(Me._Row)                        '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        '20120613----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC501DAC", "SelectMCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO")

        reader.Close()

        Return ds

    End Function

#If True Then   'add 2021/01/18 017853 【LMS】JJ_追加要望_まとめ出荷を行った場合も納品書はオーダー毎に印刷して欲しい
    ''' <summary>
    '''出荷管理番号のオーダー番号取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>出荷管理番号のオーダー番号取得SQLの構築・発行</remarks>
    Private Function SELECT_OUT_ORD_NO(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMC500IN").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC501DAC.SQL_SELECT_OUT_ORD_NO)      'SQL構築(荷主明細マスタ設定値Select句)
        'Call Me.setIndataParameter(Me._Row)                        '条件設定
        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC501DAC", "SELECT_OUT_ORD_NO", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("CHKCUST_ORD_NO", "CHKCUST_ORD_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "OUT_ORDER")

        reader.Close()

        Return ds

    End Function

#End If

    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '管理番号
            whereStr = .Item("KANRI_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))

            'パターンフラグ('0':出荷、'1':運送)
            whereStr = .Item("PTN_FLAG").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_FLAG", whereStr, DBDataType.CHAR))

            '(2012.03.03) 再発行フラグ追加 LMC513対応 -- START --
            '再発行フラグ
            'whereStr = .Item("SAIHAKKO_FLG").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))
            '(2012.03.03) 再発行フラグ追加 LMC513対応 --  END  --

#If True Then   'ADD 2021/01/19 017853 【LMS】JJ_追加要望_まとめ出荷を行った場合も納品書はオーダー毎に印刷して欲しい
            '管理番号
            whereStr = .Item("ORDER_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NO", whereStr, DBDataType.CHAR))
#End If
        End With

    End Sub

    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_SAIHAKKO()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '再発行フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    '''  荷主明細マスタ用(LMC514)
    ''' </summary>
    ''' <remarks>荷主明細マスタ存在抽出用SQLの構築</remarks>
    Private Sub setIndataParameter(ByVal _Row As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
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
