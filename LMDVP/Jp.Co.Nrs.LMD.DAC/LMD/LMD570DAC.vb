' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD570    : 在庫証明書印刷
'  作  成  者       :  [hagimoto]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD570DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD570DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "検索処理 SQL(SELECT句)"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	ZAITRS.NRS_BR_CD                                         AS NRS_BR_CD " & vbNewLine _
                                            & ",'32'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                                                                             " & vbNewLine _
                                                & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                                                   " & vbNewLine _
                                                & "WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                                                        " & vbNewLine _
                                                & "ELSE MR3.RPT_ID END AS RPT_ID                                                                                                                      " & vbNewLine _
                                                & "                                                                                                                                                   " & vbNewLine _
                                                & ",ZAITRS.NRS_BR_CD                AS NRS_BR_CD                                                                                                      " & vbNewLine _
                                                & ",SEIQ.SEIQTO_CD AS SEIQTO_CD                                                                                                                       " & vbNewLine _
                                                & ",CASE WHEN MCD.SET_NAIYO <> '' AND MCD.SET_NAIYO IS NOT NULL                                                                                       " & vbNewLine _
                                                & "      THEN MCD.SET_NAIYO                                                                                                                           " & vbNewLine _
                                                & "      ELSE SEIQ.SEIQTO_NM                                                                                                                          " & vbNewLine _
                                                & "      END AS SEIQTO_NM                                                                                                                             " & vbNewLine _
                                                & ",SEIQ.SEIQTO_BUSYO_NM AS SEIQTO_BUSYO_NM                                                                                                           " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_L                AS CUST_CD_L                                                                                                      " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_M                AS CUST_CD_M                                                                                                      " & vbNewLine _
                                                & ",CUST.CUST_NM_L                AS CUST_NM_L                                                                                                        " & vbNewLine _
                                                & ",CUST.CUST_NM_M                AS CUST_NM_M                                                                                                        " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start                                                             " & vbNewLine _
                                                & ",CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_1                                                                                                        " & vbNewLine _
                                                & "      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_1                                                                                                 " & vbNewLine _
                                                & "      ELSE M_SOKO_USER.AD_1 END               AS NRS_AD_1                                                                                          " & vbNewLine _
                                                & ",CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_2                                                                                                        " & vbNewLine _
                                                & "      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_2                                                                                                 " & vbNewLine _
                                                & "      ELSE M_SOKO_USER.AD_2 END               AS NRS_AD_2                                                                                          " & vbNewLine _
                                                & "--,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_1                                                                                                        " & vbNewLine _
                                                & "--      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_1                                                                                                 " & vbNewLine _
                                                & "--      ELSE NRS.AD_1 END               AS NRS_AD_1                                                                                                  " & vbNewLine _
                                                & "--,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_2                                                                                                        " & vbNewLine _
                                                & "--      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_2                                                                                                 " & vbNewLine _
                                                & "--      ELSE NRS.AD_2 END               AS NRS_AD_2                                                                                                  " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end                                                             " & vbNewLine _
                                                & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 <> '' OR ZAITRS.GOODS_COND_KB_2 <> '') THEN                                                                     " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '01' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND COND.INFERIOR_GOODS_KB = '00' THEN                                            " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '00' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND COND.INFERIOR_GOODS_KB = '01' THEN                                            " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '01' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND ZAITRS.GOODS_COND_KB_3 = '' THEN                                              " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '00' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "ELSE ''                                                                                                                                            " & vbNewLine _
                                                & "END AS GOODS_COND                                                                                                                                  " & vbNewLine _
                                                & "                                                                                                                                                   " & vbNewLine _
                                                & ",KBN2.KBN_NM1 AS TAX_KB                                                                                                                            " & vbNewLine _
                                                & ",KBN5.KBN_NM1 AS OFB_KB_NM                                                                                                                   " & vbNewLine _
                                                & ",SOKO.WH_CD AS WH_CD                                                                                                                               " & vbNewLine _
                                                & ",SOKO.WH_NM AS WH_NM                                                                                                                               " & vbNewLine _
                                                & ",ISNULL(KBN7.KBN_NM1, '')  AS SOKO_CD                                                                                                              " & vbNewLine _
                                                & ",MG.GOODS_CD_CUST AS GOODS_CD_CUST                                                                                                                 " & vbNewLine _
                                                & ",MG.GOODS_NM_1 AS GOODS_NM                                                                                                                         " & vbNewLine _
                                                & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 --- START ---                                                                                    " & vbNewLine _
                                                & ",MG.GOODS_NM_2   AS GOODS_NM_2                                                                                                                     " & vbNewLine _
                                                & ",MG.SEARCH_KEY_1 AS SEARCH_KEY_1                                                                                                                   " & vbNewLine _
                                                & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 ---  END  ---                                                                                    " & vbNewLine _
                                                & ",BASE10.LOT_NO AS LOT_NO                                                                                                                           " & vbNewLine _
                                                & ",MG.NB_UT AS NB_UT                                                                                                                                 " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_NB) / MG.PKG_NB AS ZAI_NB                                                                                                          " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB AS HASU                                                                                                            " & vbNewLine _
                                                & ",MG.PKG_NB AS PKG_NB                                                                                                                               " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_NB) AS KOSU                                                                                                                        " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_QT) AS ZAI_QT                                                                                                                      " & vbNewLine _
                                                & ",MG.STD_IRIME_UT AS STD_IRIME_UT                                                                                                                   " & vbNewLine _
                                                & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END AS INKO_DATE                                          " & vbNewLine _
                                                & ",ZAITRS.REMARK_OUT AS REMARK_OUT                                                                                                                   " & vbNewLine _
                                                & ",KBN3.KBN_NM1 AS GOODS_COND_KB_1                                                                                                                   " & vbNewLine _
                                                & ",KBN4.KBN_NM1 AS GOODS_COND_KB_2                                                                                                                   " & vbNewLine _
                                                & ",ZAITRS.GOODS_COND_KB_3 AS GOODS_COND_KB_3                                                                                                         " & vbNewLine _
                                                & ",COND.JOTAI_NM AS GOODS_COND_NM_3                                                                                                                  " & vbNewLine _
                                                & ",CASE WHEN CUST_D.SET_NAIYO <> '01' THEN BASE10.SERIAL_NO END AS SERIAL_NO                                                                         " & vbNewLine _
                                                & ",ZAITRS.IRIME AS IRIME_NB                                                                                                                          " & vbNewLine _
                                                & ",MG.STD_IRIME_UT AS IRIME_UT                                                                                                                       " & vbNewLine _
                                                & ",MG.CUST_CD_S AS CUST_CD_S                                                                                                                         " & vbNewLine _
                                                & ",MG.CUST_CD_SS AS CUST_CD_SS                                                                                                                       " & vbNewLine _
                                                & ",CUST.CUST_NM_S                AS CUST_NM_S                                                                                                        " & vbNewLine _
                                                & ",CUST.CUST_NM_SS               AS CUST_NM_SS                                                                                                       " & vbNewLine _
                                                & ",@PRINT_FROM                   AS PRINT_FROM                                                                                                       " & vbNewLine _
                                                & ",CASE WHEN @KAKUIN_FLG = '1' THEN KBN_NRS.KBN_NM1 ELSE NRS.NRS_BR_NM END AS NRS_BR_NM                                                                                                        " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start                                                             " & vbNewLine _
                                                & ",CASE WHEN SOKO.WH_KB = '01' THEN SOKO.TEL                                                                                                         " & vbNewLine _
                                                & "      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.TEL                                                                                                  " & vbNewLine _
                                                & "      ELSE M_SOKO_USER.TEL END               AS TEL                                                                                                " & vbNewLine _
                                                & "--,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.TEL                                                                                                         " & vbNewLine _
                                                & "--      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.TEL                                                                                                  " & vbNewLine _
                                                & "--      ELSE NRS.TEL END               AS TEL                                                                                                        " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end                                                              " & vbNewLine _
                                                & ",CUST_D.SET_NAIYO              AS SERIAL_NO_GROUP_KB                                                                                               " & vbNewLine _
                                                & ",MCD2.SET_NAIYO                AS ORDER_BY_KB                                                                                               " & vbNewLine _
                                                & " -- HASU_ROW START" & vbNewLine _
                                                & " ,CASE WHEN (SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB ) = '0' THEN   0" & vbNewLine _
                                                & "    	  WHEN (SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB ) > '0' THEN + 1" & vbNewLine _
                                                & "       		END 					 AS HASU_ROW                " & vbNewLine _
                                                & " -- HASU_ROW END                                                  " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用(LMF586用)---START---
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_586 As String = "SELECT                                                                                                                                             " & vbNewLine _
                                                & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                                                   " & vbNewLine _
                                                & "WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                                                        " & vbNewLine _
                                                & "ELSE MR3.RPT_ID END AS RPT_ID                                                                                                                      " & vbNewLine _
                                                & "                                                                                                                                                   " & vbNewLine _
                                                & ",ZAITRS.NRS_BR_CD                AS NRS_BR_CD                                                                                                      " & vbNewLine _
                                                & ",SEIQ.SEIQTO_CD AS SEIQTO_CD                                                                                                                       " & vbNewLine _
                                                & ",CASE WHEN MCD.SET_NAIYO <> '' AND MCD.SET_NAIYO IS NOT NULL                                                                                       " & vbNewLine _
                                                & "      THEN MCD.SET_NAIYO                                                                                                                           " & vbNewLine _
                                                & "      ELSE SEIQ.SEIQTO_NM                                                                                                                          " & vbNewLine _
                                                & "      END AS SEIQTO_NM                                                                                                                             " & vbNewLine _
                                                & ",SEIQ.SEIQTO_BUSYO_NM AS SEIQTO_BUSYO_NM                                                                                                           " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_L                AS CUST_CD_L                                                                                                      " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_M                AS CUST_CD_M                                                                                                      " & vbNewLine _
                                                & ",CUST.CUST_NM_L                AS CUST_NM_L                                                                                                        " & vbNewLine _
                                                & ",CUST.CUST_NM_M                AS CUST_NM_M                                                                                                        " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start                                                             " & vbNewLine _
                                                & ",CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_1                                                                                                        " & vbNewLine _
                                                & "      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_1                                                                                                 " & vbNewLine _
                                                & "      ELSE M_SOKO_USER.AD_1 END               AS NRS_AD_1                                                                                          " & vbNewLine _
                                                & ",CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_2                                                                                                        " & vbNewLine _
                                                & "      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_2                                                                                                 " & vbNewLine _
                                                & "      ELSE M_SOKO_USER.AD_2 END               AS NRS_AD_2                                                                                          " & vbNewLine _
                                                & "--,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_1                                                                                                        " & vbNewLine _
                                                & "--      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_1                                                                                                 " & vbNewLine _
                                                & "--      ELSE NRS.AD_1 END               AS NRS_AD_1                                                                                                  " & vbNewLine _
                                                & "--,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_2                                                                                                        " & vbNewLine _
                                                & "--      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_2                                                                                                 " & vbNewLine _
                                                & "--      ELSE NRS.AD_2 END               AS NRS_AD_2                                                                                                  " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end                                                             " & vbNewLine _
                                                & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 <> '' OR ZAITRS.GOODS_COND_KB_2 <> '') THEN                                                                     " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '01' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND COND.INFERIOR_GOODS_KB = '00' THEN                                            " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '00' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND COND.INFERIOR_GOODS_KB = '01' THEN                                            " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '01' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND ZAITRS.GOODS_COND_KB_3 = '' THEN                                              " & vbNewLine _
                                                & "(SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '00' AND SYS_DEL_FLG = '0')                                            " & vbNewLine _
                                                & "ELSE ''                                                                                                                                            " & vbNewLine _
                                                & "END AS GOODS_COND                                                                                                                                  " & vbNewLine _
                                                & "                                                                                                                                                   " & vbNewLine _
                                                & ",KBN2.KBN_NM1 AS TAX_KB                                                                                                                            " & vbNewLine _
                                                & ",KBN5.KBN_NM1 AS OFB_KB_NM                                                                                                                   " & vbNewLine _
                                                & ",SOKO.WH_CD AS WH_CD                                                                                                                               " & vbNewLine _
                                                & ",SOKO.WH_NM AS WH_NM                                                                                                                               " & vbNewLine _
                                                & ",MG.GOODS_CD_CUST AS GOODS_CD_CUST                                                                                                                 " & vbNewLine _
                                                & ",MG.GOODS_NM_1 AS GOODS_NM                                                                                                                         " & vbNewLine _
                                                & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 --- START ---                                                                                    " & vbNewLine _
                                                & ",MG.GOODS_NM_2   AS GOODS_NM_2                                                                                                                     " & vbNewLine _
                                                & ",MG.SEARCH_KEY_1 AS SEARCH_KEY_1                                                                                                                   " & vbNewLine _
                                                & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 ---  END  ---                                                                                    " & vbNewLine _
                                                & ",BASE10.LOT_NO AS LOT_NO                                                                                                                           " & vbNewLine _
                                                & ",MG.NB_UT AS NB_UT                                                                                                                                 " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_NB) / MG.PKG_NB AS ZAI_NB                                                                                                          " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB AS HASU                                                                                                            " & vbNewLine _
                                                & ",MG.PKG_NB AS PKG_NB                                                                                                                               " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_NB) AS KOSU                                                                                                                        " & vbNewLine _
                                                & ",SUM(BASE10.PORA_ZAI_QT) AS ZAI_QT                                                                                                                      " & vbNewLine _
                                                & ",MG.STD_IRIME_UT AS STD_IRIME_UT                                                                                                                   " & vbNewLine _
                                                & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END AS INKO_DATE                                          " & vbNewLine _
                                                & ",ZAITRS.REMARK_OUT AS REMARK_OUT                                                                                                                   " & vbNewLine _
                                                & ",KBN3.KBN_NM1 AS GOODS_COND_KB_1                                                                                                                   " & vbNewLine _
                                                & ",KBN4.KBN_NM1 AS GOODS_COND_KB_2                                                                                                                   " & vbNewLine _
                                                & "--変更(2012.12.03)                                                                                                                             " & vbNewLine _
                                                & ",ZAITRS.REMARK AS GOODS_COND_KB_3                                                                                                         " & vbNewLine _
                                                & "--変更(2012.12.03)                                                                                                                             " & vbNewLine _
                                                & ",COND.JOTAI_NM AS GOODS_COND_NM_3                                                                                                                  " & vbNewLine _
                                                & ",CASE WHEN CUST_D.SET_NAIYO <> '01' THEN BASE10.SERIAL_NO END AS SERIAL_NO                                                                         " & vbNewLine _
                                                & ",ZAITRS.IRIME AS IRIME_NB                                                                                                                          " & vbNewLine _
                                                & ",MG.STD_IRIME_UT AS IRIME_UT                                                                                                                       " & vbNewLine _
                                                & ",MG.CUST_CD_S AS CUST_CD_S                                                                                                                         " & vbNewLine _
                                                & ",MG.CUST_CD_SS AS CUST_CD_SS                                                                                                                       " & vbNewLine _
                                                & ",CUST.CUST_NM_S                AS CUST_NM_S                                                                                                        " & vbNewLine _
                                                & ",CUST.CUST_NM_SS               AS CUST_NM_SS                                                                                                       " & vbNewLine _
                                                & ",@PRINT_FROM                   AS PRINT_FROM                                                                                                       " & vbNewLine _
                                                & ",CASE WHEN @KAKUIN_FLG = '1' THEN KBN_NRS.KBN_NM1 ELSE NRS.NRS_BR_NM END AS NRS_BR_NM                                                                                                        " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start                                                             " & vbNewLine _
                                                & ",CASE WHEN SOKO.WH_KB = '01' THEN SOKO.TEL                                                                                                         " & vbNewLine _
                                                & "      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.TEL                                                                                                  " & vbNewLine _
                                                & "      ELSE M_SOKO_USER.TEL END               AS TEL                                                                                                " & vbNewLine _
                                                & "--,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.TEL                                                                                                         " & vbNewLine _
                                                & "--      WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.TEL                                                                                                  " & vbNewLine _
                                                & "--      ELSE NRS.TEL END               AS TEL                                                                                                        " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end                                                              " & vbNewLine _
                                                & ",CUST_D.SET_NAIYO              AS SERIAL_NO_GROUP_KB                                                                                               " & vbNewLine _
                                                & ",MCD2.SET_NAIYO                AS ORDER_BY_KB                                                                                               " & vbNewLine _
                                                & " -- HASU_ROW START" & vbNewLine _
                                                & " ,CASE WHEN (SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB ) = '0' THEN   0" & vbNewLine _
                                                & "    	  WHEN (SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB ) > '0' THEN + 1" & vbNewLine _
                                                & "       		END 					 AS HASU_ROW                " & vbNewLine _
                                                & " -- HASU_ROW END                                                  " & vbNewLine _
                                                & ",ZAITRS.REMARK AS REMARK                                          " & vbNewLine
    ' 印刷データ抽出用(LMF586用) --- END ---

    '(2012.12.13)要望番号1657 LMD587専用 -- START --
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_587 As String = " SELECT                                                                                                                        " & vbNewLine _
                                                & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                       " & vbNewLine _
                                                & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                       " & vbNewLine _
                                                & "        ELSE MR3.RPT_ID END                               AS RPT_ID                                                            " & vbNewLine _
                                                & "                                                                                                                               " & vbNewLine _
                                                & "      , ZAITRS.NRS_BR_CD                                  AS NRS_BR_CD                                                         " & vbNewLine _
                                                & "      , SEIQ.SEIQTO_CD                                    AS SEIQTO_CD                                                         " & vbNewLine _
                                                & "      , CASE WHEN MCD.SET_NAIYO <> '' AND MCD.SET_NAIYO IS NOT NULL                                                            " & vbNewLine _
                                                & "                  THEN MCD.SET_NAIYO                                                                                           " & vbNewLine _
                                                & "        ELSE SEIQ.SEIQTO_NM                                                                                                    " & vbNewLine _
                                                & "        END AS SEIQTO_NM                                                                                                       " & vbNewLine _
                                                & "      , SEIQ.SEIQTO_BUSYO_NM AS SEIQTO_BUSYO_NM                                                                                " & vbNewLine _
                                                & "      , ZAITRS.CUST_CD_L                                  AS CUST_CD_L                                                         " & vbNewLine _
                                                & "      , ZAITRS.CUST_CD_M                                  AS CUST_CD_M                                                         " & vbNewLine _
                                                & "      , CUST.CUST_NM_L                                    AS CUST_NM_L                                                         " & vbNewLine _
                                                & "      , CUST.CUST_NM_M                                    AS CUST_NM_M                                                         " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start                                                             " & vbNewLine _
                                                & "       ,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_1                                                                                                        " & vbNewLine _
                                                & "             WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_1                                                                                                 " & vbNewLine _
                                                & "             ELSE M_SOKO_USER.AD_1 END               AS NRS_AD_1                                                                                          " & vbNewLine _
                                                & "       ,CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_2                                                                                                        " & vbNewLine _
                                                & "             WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_2                                                                                                 " & vbNewLine _
                                                & "             ELSE M_SOKO_USER.AD_2 END               AS NRS_AD_2                                                                                          " & vbNewLine _
                                                & "--      , CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_1                                                                             " & vbNewLine _
                                                & "--             WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_1                                                                      " & vbNewLine _
                                                & "--        ELSE NRS.AD_1 END                                 AS NRS_AD_1                                                          " & vbNewLine _
                                                & "--      , CASE WHEN SOKO.WH_KB = '01' THEN SOKO.AD_2                                                                             " & vbNewLine _
                                                & "--             WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.AD_2                                                                      " & vbNewLine _
                                                & "--        ELSE NRS.AD_2 END                                 AS NRS_AD_2                                                          " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end                                                             " & vbNewLine _
                                                & "      , CASE WHEN (ZAITRS.GOODS_COND_KB_1 <> '' OR ZAITRS.GOODS_COND_KB_2 <> '') THEN                                          " & vbNewLine _
                                                & "                  (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '01' AND SYS_DEL_FLG = '0')    " & vbNewLine _
                                                & "             WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND COND.INFERIOR_GOODS_KB = '00' THEN           " & vbNewLine _
                                                & "                  (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '00' AND SYS_DEL_FLG = '0')    " & vbNewLine _
                                                & "             WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND COND.INFERIOR_GOODS_KB = '01' THEN           " & vbNewLine _
                                                & "                  (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '01' AND SYS_DEL_FLG = '0')    " & vbNewLine _
                                                & "             WHEN ZAITRS.GOODS_COND_KB_1 = '' AND ZAITRS.GOODS_COND_KB_2 = '' AND ZAITRS.GOODS_COND_KB_3 = '' THEN             " & vbNewLine _
                                                & "                  (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H017' AND KBN_CD = '00' AND SYS_DEL_FLG = '0')    " & vbNewLine _
                                                & "       ELSE ''                                                                                                                 " & vbNewLine _
                                                & "       END                                                AS GOODS_COND                                                        " & vbNewLine _
                                                & "      , KBN2.KBN_NM1                                      AS TAX_KB                                                            " & vbNewLine _
                                                & "      , KBN5.KBN_NM1                                      AS OFB_KB_NM                                                         " & vbNewLine _
                                                & "      --(2012.12.13)要望番号1657 LMD587専用 -- START --                                                                        " & vbNewLine _
                                                & "      --, SOKO.WH_CD                                      AS WH_CD                                                             " & vbNewLine _
                                                & "      --, SOKO.WH_NM                                      AS WH_NM                                                             " & vbNewLine _
                                                & "      --TEST用, ZAITRS.TOU_NO                             AS TOU_NO                                                            " & vbNewLine _
                                                & "      , CASE WHEN KBN6.KBN_NM1 <> '' THEN KBN6.KBN_NM2                                                                         " & vbNewLine _
                                                & "        ELSE SOKO.WH_CD                                                                                                        " & vbNewLine _
                                                & "        END                                               AS WH_CD                                                             " & vbNewLine _
                                                & "      , CASE WHEN KBN6.KBN_NM1 <> '' THEN KBN6.KBN_NM1                                                                         " & vbNewLine _
                                                & "        ELSE 'NRS 株式会社 ' + SOKO.WH_NM                                                                                     " & vbNewLine _
                                                & "        END                                               AS WH_NM                                                             " & vbNewLine _
                                                & "      --(2012.12.13)要望番号1657 LMD587専用 --  END  --                                                                        " & vbNewLine _
                                                & "      , MG.GOODS_CD_CUST                                  AS GOODS_CD_CUST                                                     " & vbNewLine _
                                                & "      , MG.GOODS_NM_1                                     AS GOODS_NM                                                          " & vbNewLine _
                                                & "      , MG.GOODS_NM_2                                     AS GOODS_NM_2                                                        " & vbNewLine _
                                                & "      , MG.SEARCH_KEY_1                                   AS SEARCH_KEY_1                                                      " & vbNewLine _
                                                & "      , BASE10.LOT_NO                                     AS LOT_NO                                                            " & vbNewLine _
                                                & "      , MG.NB_UT                                          AS NB_UT                                                             " & vbNewLine _
                                                & "      , SUM(BASE10.PORA_ZAI_NB) / MG.PKG_NB               AS ZAI_NB                                                            " & vbNewLine _
                                                & "      , SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB               AS HASU                                                              " & vbNewLine _
                                                & "      , MG.PKG_NB                                         AS PKG_NB                                                            " & vbNewLine _
                                                & "      , SUM(BASE10.PORA_ZAI_NB)                           AS KOSU                                                              " & vbNewLine _
                                                & "      , SUM(BASE10.PORA_ZAI_QT)                           AS ZAI_QT                                                            " & vbNewLine _
                                                & "      , MG.STD_IRIME_UT                                   AS STD_IRIME_UT                                                      " & vbNewLine _
                                                & "      , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE                                                      " & vbNewLine _
                                                & "        ELSE ZAITRS.INKO_DATE                                                                                                  " & vbNewLine _
                                                & "        END                                               AS INKO_DATE                                                         " & vbNewLine _
                                                & "      , ZAITRS.REMARK_OUT                                 AS REMARK_OUT                                                        " & vbNewLine _
                                                & "      , KBN3.KBN_NM1                                      AS GOODS_COND_KB_1                                                   " & vbNewLine _
                                                & "      , KBN4.KBN_NM1                                      AS GOODS_COND_KB_2                                                   " & vbNewLine _
                                                & "      , ZAITRS.GOODS_COND_KB_3                            AS GOODS_COND_KB_3                                                   " & vbNewLine _
                                                & "      , COND.JOTAI_NM                                     AS GOODS_COND_NM_3                                                   " & vbNewLine _
                                                & "      , CASE WHEN CUST_D.SET_NAIYO <> '01' THEN BASE10.SERIAL_NO                                                               " & vbNewLine _
                                                & "        END                                               AS SERIAL_NO                                                         " & vbNewLine _
                                                & "      , ZAITRS.IRIME                                      AS IRIME_NB                                                          " & vbNewLine _
                                                & "      , MG.STD_IRIME_UT                                   AS IRIME_UT                                                          " & vbNewLine _
                                                & "      , MG.CUST_CD_S                                      AS CUST_CD_S                                                         " & vbNewLine _
                                                & "      , MG.CUST_CD_SS                                     AS CUST_CD_SS                                                        " & vbNewLine _
                                                & "      , CUST.CUST_NM_S                                    AS CUST_NM_S                                                         " & vbNewLine _
                                                & "      , CUST.CUST_NM_SS                                   AS CUST_NM_SS                                                        " & vbNewLine _
                                                & "      , @PRINT_FROM                                       AS PRINT_FROM                                                        " & vbNewLine _
                                                & "      , CASE WHEN @KAKUIN_FLG = '1' THEN KBN_NRS.KBN_NM1 ELSE NRS.NRS_BR_NM END AS NRS_BR_NM                                                         " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd start                                         " & vbNewLine _
                                                & "      , CASE WHEN SOKO.WH_KB = '01' THEN SOKO.TEL                                                                              " & vbNewLine _
                                                & "             WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.TEL                                                                       " & vbNewLine _
                                                & "            ELSE M_SOKO_USER.TEL END               AS TEL                                                                      " & vbNewLine _
                                                & "--      , CASE WHEN SOKO.WH_KB = '01' THEN SOKO.TEL                                                                              " & vbNewLine _
                                                & "--             WHEN SOKO.ZAIKO_WH_CD <> '' THEN SOKO_2.TEL                                                                       " & vbNewLine _
                                                & "--            ELSE NRS.TEL END                              AS TEL                                                               " & vbNewLine _
                                                & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen upd end                                           " & vbNewLine _
                                                & "      , CUST_D.SET_NAIYO                                  AS SERIAL_NO_GROUP_KB                                                " & vbNewLine _
                                                & "      , MCD2.SET_NAIYO                                    AS ORDER_BY_KB                                                       " & vbNewLine _
                                                & "      , CASE WHEN (SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB ) = '0' THEN   0                                                        " & vbNewLine _
                                                & "             WHEN (SUM(BASE10.PORA_ZAI_NB) % MG.PKG_NB ) > '0' THEN + 1                                                        " & vbNewLine _
                                                & "        END                                               AS HASU_ROW                                                          " & vbNewLine
    '(2012.12.13)要望番号1657 LMD587専用 --  END  --

#End Region

#Region "検索処理 SQL(FROM句)"

    ''' <summary>
    ''' データ抽出用FROM句(帳票種別取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                                                                                                  " & vbNewLine _
                                      & "$LM_TRN$..D_ZAI_TRS ZAITRS                                                                                           " & vbNewLine _
                                      & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
                                      & "$LM_MST$..M_GOODS MG ON                                                                                           " & vbNewLine _
                                      & "MG.NRS_BR_CD = @NRS_BR_CD                                                                                         " & vbNewLine _
                                      & "AND MG.GOODS_CD_NRS = ZAITRS.GOODS_CD_NRS                                                                         " & vbNewLine _
                                      & "--在庫の荷主での荷主帳票パターン取得                                                                                 " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                  " & vbNewLine _
                                      & "ON  MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                      " & vbNewLine _
                                      & "AND MCR1.CUST_CD_L = @CUST_CD_L                                                                                      " & vbNewLine _
                                      & "AND MCR1.CUST_CD_M = @CUST_CD_M                                                                                      " & vbNewLine _
                                      & "AND MCR1.PTN_ID = '32'                                                                                               " & vbNewLine _
                                      & "AND MCR1.CUST_CD_S = '00'                                                                                            " & vbNewLine _
                                      & "--帳票パターン取得                                                                                                   " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                        " & vbNewLine _
                                      & "ON  MR1.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                      & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                         " & vbNewLine _
                                      & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                         " & vbNewLine _
                                      & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "--商品Mの荷主での荷主帳票パターン取得                                                                                " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                  " & vbNewLine _
                                      & "ON  MCR2.NRS_BR_CD = @NRS_BR_CD                                                                                      " & vbNewLine _
                                      & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                 " & vbNewLine _
                                      & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                 " & vbNewLine _
                                      & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                 " & vbNewLine _
                                      & "AND MCR2.PTN_ID = '32'                                                                                               " & vbNewLine _
                                      & "--帳票パターン取得                                                                                                   " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                        " & vbNewLine _
                                      & "ON  MR2.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                      & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                         " & vbNewLine _
                                      & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                         " & vbNewLine _
                                      & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "--存在しない場合の帳票パターン取得                                                                                   " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                        " & vbNewLine _
                                      & "ON  MR3.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                      & "AND MR3.PTN_ID = '32'                                                                                                " & vbNewLine _
                                      & "AND MR3.STANDARD_FLAG = '01'                                                                                         " & vbNewLine _
                                      & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine _
                                      & "AND ZAITRS.CUST_CD_L = @CUST_CD_L                                                                                    " & vbNewLine _
                                      & "AND ZAITRS.CUST_CD_M = @CUST_CD_M                                                                                    " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                                                                                                               " & vbNewLine _
                                    & "(                                                                                                                                                  " & vbNewLine _
                                    & "SELECT                                                                                                                                             " & vbNewLine _
                                    & "*                                                                                                                                                  " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "(SELECT                                                                                                                                            " & vbNewLine _
                                    & "CUST_CD_L                AS CUST_CD_L                                                                                                              " & vbNewLine _
                                    & ",CUST_CD_M                AS CUST_CD_M                                                                                                             " & vbNewLine _
                                    & ",CUST_NM_L                AS CUST_NM_L                                                                                                             " & vbNewLine _
                                    & ",ZAI_REC_NO               AS ZAI_REC_NO                                                                                                            " & vbNewLine _
                                    & ",GOODS_CD_NRS             AS GOODS_CD_NRS                                                                                                          " & vbNewLine _
                                    & ",GOODS_CD_CUST          AS GOODS_CD_CUST                                                                                                           " & vbNewLine _
                                    & ",LOT_NO                   AS LOT_NO                                                                                                                " & vbNewLine _
                                    & ",SERIAL_NO                AS SERIAL_NO                                                                                                             " & vbNewLine _
                                    & ",0                        AS HIKAKU_ZAI_NB                                                                                                         " & vbNewLine _
                                    & ",SUM(PORA_ZAI_NB)         AS PORA_ZAI_NB                                                                                                           " & vbNewLine _
                                    & ",0                        AS EXTRA_ZAI_NB                                                                                                          " & vbNewLine _
                                    & ",0                        AS HIKAKU_ZAI_QT                                                                                                         " & vbNewLine _
                                    & ",SUM(PORA_ZAI_QT)         AS PORA_ZAI_QT                                                                                                           " & vbNewLine _
                                    & ",0                        AS EXTRA_ZAI_QT                                                                                                          " & vbNewLine _
                                    & ",NRS_BR_CD                AS NRS_BR_CD                                                                                                             " & vbNewLine _
                                    & "--月末在庫履歴(D_ZAI_ZAN_JITSU)                                                                                                                          " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "(SELECT                                                                                                                                            " & vbNewLine _
                                    & "''                                AS OUTKA_NO_L                                                                                                    " & vbNewLine _
                                    & ",''                                AS OUTKA_NO_M                                                                                                   " & vbNewLine _
                                    & ",''                                AS OUTKA_NO_S                                                                                                   " & vbNewLine _
                                    & ",ZAI2.CUST_CD_L                    AS CUST_CD_L                                                                                                    " & vbNewLine _
                                    & ",ZAI2.CUST_CD_M                    AS CUST_CD_M                                                                                                    " & vbNewLine _
                                    & ",''                                AS CUST_NM_L                                                                                                    " & vbNewLine _
                                    & ",ZAN.ZAI_REC_NO                    AS ZAI_REC_NO                                                                                                   " & vbNewLine _
                                    & ",ZAI2.GOODS_CD_NRS                 AS GOODS_CD_NRS                                                                                                 " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST                  AS GOODS_CD_CUST                                                                                                " & vbNewLine _
                                    & ",ISNULL(ZAI2.LOT_NO, '')           AS LOT_NO                                                                                                       " & vbNewLine _
                                    & ",ISNULL(ZAI2.SERIAL_NO, '')        AS SERIAL_NO                                                                                                    " & vbNewLine _
                                    & ",ZAN.PORA_ZAI_NB                   AS PORA_ZAI_NB                                                                                                  " & vbNewLine _
                                    & ",ZAN.PORA_ZAI_QT                   AS PORA_ZAI_QT                                                                                                  " & vbNewLine _
                                    & ",ZAN.NRS_BR_CD                     AS NRS_BR_CD                                                                                                    " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "$LM_TRN$..D_ZAI_ZAN_JITSU ZAN                                                                                                                        " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI2                                                                                                                   " & vbNewLine _
                                    & "ON  ZAI2.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "AND ZAI2.NRS_BR_CD = ZAN.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                    & "AND ZAI2.ZAI_REC_NO = ZAN.ZAI_REC_NO                                                                                                               " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..B_INKA_L INL2                                                                                                                    " & vbNewLine _
                                    & "ON  INL2.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "AND INL2.NRS_BR_CD = ZAI2.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND INL2.INKA_NO_L = ZAI2.INKA_NO_L                                                                                                                " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                       " & vbNewLine _
                                    & "ON  ZAI2.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                  " & vbNewLine _
                                    & "AND ZAI2.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                            " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "ZAN.SYS_DEL_FLG = '0'                                                                                                                              " & vbNewLine _
                                    & "AND ZAN.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND ZAI2.CUST_CD_L = @CUST_CD_L                                                                                                                    " & vbNewLine _
                                    & "AND ZAI2.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                    & "AND ZAN.RIREKI_DATE <= @PRINT_FROM                                                                                                                 " & vbNewLine _
                                    & "AND ZAN.RIREKI_DATE >= CASE WHEN @GETU_FLG = '1' THEN @ZAI_GETU ELSE ZAN.RIREKI_DATE END                                                           " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND INL2.INKA_STATE_KB !< '50'                                                                                                                     " & vbNewLine _
                                    & "AND (ZAN.PORA_ZAI_NB <> 0 OR ZAN.PORA_ZAI_QT <> 0)                                                                                                 " & vbNewLine _
                                    & "--入荷データ(B_INKA_S)                                                                                                                                  " & vbNewLine _
                                    & "UNION ALL                                                                                                                                          " & vbNewLine _
                                    & "SELECT                                                                                                                                             " & vbNewLine _
                                    & "''                                                        AS OUTKA_NO_L                                                                            " & vbNewLine _
                                    & ",''                                                        AS OUTKA_NO_M                                                                           " & vbNewLine _
                                    & ",''                                                        AS OUTKA_NO_S                                                                           " & vbNewLine _
                                    & ",INL1.CUST_CD_L                                            AS CUST_CD_L                                                                            " & vbNewLine _
                                    & ",INL1.CUST_CD_M                                            AS CUST_CD_M                                                                            " & vbNewLine _
                                    & ",''                                                        AS CUST_NM_L                                                                            " & vbNewLine _
                                    & ",INS1.ZAI_REC_NO                                           AS ZAI_REC_NO                                                                           " & vbNewLine _
                                    & ",INM1.GOODS_CD_NRS                                         AS GOODS_CD_NRS                                                                         " & vbNewLine _
                                    & ",MG1.GOODS_CD_CUST                     AS GOODS_CD_CUST                                                                                            " & vbNewLine _
                                    & ",ISNULL(INS1.LOT_NO, '')                                   AS LOT_NO                                                                               " & vbNewLine _
                                    & ",ISNULL(INS1.SERIAL_NO, '')                                AS SERIAL_NO                                                                            " & vbNewLine _
                                    & ",(INS1.KONSU * MG1.PKG_NB) + INS1.HASU                     AS PORA_ZAI_NB                                                                          " & vbNewLine _
                                    & ",((INS1.KONSU * MG1.PKG_NB) + INS1.HASU) * INS1.IRIME      AS PORA_ZAI_QT                                                                          " & vbNewLine _
                                    & ",INS1.NRS_BR_CD                     AS NRS_BR_CD                                                                                                   " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "$LM_TRN$..B_INKA_L INL1                                                                                                                              " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..B_INKA_M INM1                                                                                                                    " & vbNewLine _
                                    & "ON  INM1.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "AND INM1.NRS_BR_CD = INL1.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND INM1.INKA_NO_L = INL1.INKA_NO_L                                                                                                                " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..B_INKA_S INS1                                                                                                                    " & vbNewLine _
                                    & "ON  INS1.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "AND INS1.NRS_BR_CD = INL1.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND INS1.INKA_NO_L = INL1.INKA_NO_L                                                                                                                " & vbNewLine _
                                    & "AND INS1.INKA_NO_M = INM1.INKA_NO_M                                                                                                                " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_GOODS MG1                                                                                                                      " & vbNewLine _
                                    & "ON  MG1.NRS_BR_CD = INL1.NRS_BR_CD                                                                                                                 " & vbNewLine _
                                    & "AND MG1.GOODS_CD_NRS = INM1.GOODS_CD_NRS                                                                                                           " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "INL1.SYS_DEL_FLG = '0'                                                                                                                             " & vbNewLine _
                                    & "AND INL1.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "AND (INL1.INKA_STATE_KB > '10' OR RTRIM(INS1.ZAI_REC_NO) <> '')                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND INL1.CUST_CD_L = @CUST_CD_L                                                                                                                    " & vbNewLine _
                                    & "AND INL1.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND INL1.INKA_DATE <= @PRINT_FROM                                                                                                                  " & vbNewLine _
                                    & "AND INL1.INKA_DATE >= CASE WHEN @GETU_FLG = '1' THEN @ZAI_GETU ELSE INL1.INKA_DATE END                                                             " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND (INL1.INKA_DATE > '00000000' OR INL1.INKA_STATE_KB < '50')                                                                                     " & vbNewLine _
                                    & "--在庫移動分を加減算(D_IDO_TRS)                                                                                                                             " & vbNewLine _
                                    & "--移動後                                                                                                                                              " & vbNewLine _
                                    & "UNION ALL                                                                                                                                          " & vbNewLine _
                                    & "SELECT                                                                                                                                             " & vbNewLine _
                                    & "''                                          AS OUTKA_NO_L                                                                                          " & vbNewLine _
                                    & ",''                                          AS OUTKA_NO_M                                                                                         " & vbNewLine _
                                    & ",''                                          AS OUTKA_NO_S                                                                                         " & vbNewLine _
                                    & ",ZAI3.CUST_CD_L                              AS CUST_CD_L                                                                                          " & vbNewLine _
                                    & ",ZAI3.CUST_CD_M                              AS CUST_CD_M                                                                                          " & vbNewLine _
                                    & ",''                                          AS CUST_NM_L                                                                                          " & vbNewLine _
                                    & ",IDO1.N_ZAI_REC_NO                           AS ZAI_REC_NO                                                                                         " & vbNewLine _
                                    & ",ZAI3.GOODS_CD_NRS                           AS GOODS_CD_NRS                                                                                       " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST AS GOODS_CD_CUST                                                                                                                 " & vbNewLine _
                                    & ",ISNULL(ZAI3.LOT_NO, '')                     AS LOT_NO                                                                                             " & vbNewLine _
                                    & ",ISNULL(ZAI3.SERIAL_NO, '')                  AS SERIAL_NO                                                                                          " & vbNewLine _
                                    & ",IDO1.N_PORA_ZAI_NB                          AS PORA_ZAI_NB                                                                                        " & vbNewLine _
                                    & ",IDO1.N_PORA_ZAI_NB * IDO1.ZAIK_IRIME           AS PORA_ZAI_QT                                                                                        " & vbNewLine _
                                    & ",IDO1.NRS_BR_CD                     AS NRS_BR_CD                                                                                                   " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "$LM_TRN$..D_IDO_TRS IDO1                                                                                                                             " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI3                                                                                                                   " & vbNewLine _
                                    & "ON  ZAI3.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "AND ZAI3.NRS_BR_CD = IDO1.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND ZAI3.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                                                                                                            " & vbNewLine _
                                    & "--LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI4                                                                                                                   " & vbNewLine _
                                    & "--ON  ZAI4.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "--AND ZAI4.NRS_BR_CD = IDO1.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "--AND ZAI4.ZAI_REC_NO = IDO1.N_ZAI_REC_NO                                                                                                            " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                       " & vbNewLine _
                                    & "ON  ZAI3.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                  " & vbNewLine _
                                    & "AND ZAI3.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                            " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "IDO1.SYS_DEL_FLG = '0'                                                                                                                             " & vbNewLine _
                                    & "AND IDO1.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND ZAI3.CUST_CD_L = @CUST_CD_L                                                                                                                    " & vbNewLine _
                                    & "AND ZAI3.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND IDO1.IDO_DATE <= @PRINT_FROM                                                                                                                   " & vbNewLine _
                                    & "AND IDO1.IDO_DATE >= CASE WHEN @GETU_FLG = '1' THEN @ZAI_GETU ELSE IDO1.IDO_DATE END                                                               " & vbNewLine _
                                    & "--移動前                                                                                                                                              " & vbNewLine _
                                    & "UNION ALL                                                                                                                                          " & vbNewLine _
                                    & "SELECT                                                                                                                                             " & vbNewLine _
                                    & "''                                          AS OUTKA_NO_L                                                                                          " & vbNewLine _
                                    & ",''                                          AS OUTKA_NO_M                                                                                         " & vbNewLine _
                                    & ",''                                          AS OUTKA_NO_S                                                                                         " & vbNewLine _
                                    & ",ZAI5.CUST_CD_L                              AS CUST_CD_L                                                                                          " & vbNewLine _
                                    & ",ZAI5.CUST_CD_M                              AS CUST_CD_M                                                                                          " & vbNewLine _
                                    & ",''                                          AS CUST_NM_L                                                                                          " & vbNewLine _
                                    & ",IDO2.O_ZAI_REC_NO                           AS ZAI_REC_NO                                                                                         " & vbNewLine _
                                    & ",ZAI5.GOODS_CD_NRS                           AS GOODS_CD_NRS                                                                                       " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST                 AS GOODS_CD_CUST                                                                                                 " & vbNewLine _
                                    & ",ISNULL(ZAI5.LOT_NO, '')                     AS LOT_NO                                                                                             " & vbNewLine _
                                    & ",ISNULL(ZAI5.SERIAL_NO, '')                  AS SERIAL_NO                                                                                          " & vbNewLine _
                                    & ",IDO2.N_PORA_ZAI_NB * -1                     AS PORA_ZAI_NB                                                                                        " & vbNewLine _
                                    & ",IDO2.N_PORA_ZAI_NB * IDO2.ZAIK_IRIME * -1      AS PORA_ZAI_QT                                                                                        " & vbNewLine _
                                    & ",IDO2.NRS_BR_CD                     AS NRS_BR_CD                                                                                                   " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "$LM_TRN$..D_IDO_TRS IDO2                                                                                                                             " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAI5                                                                                                                   " & vbNewLine _
                                    & "ON  ZAI5.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine _
                                    & "AND ZAI5.NRS_BR_CD = IDO2.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND ZAI5.ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                                                                                            " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                       " & vbNewLine _
                                    & "ON  ZAI5.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                  " & vbNewLine _
                                    & "AND ZAI5.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                            " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "IDO2.SYS_DEL_FLG = '0'                                                                                                                             " & vbNewLine _
                                    & "AND IDO2.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND ZAI5.CUST_CD_L = @CUST_CD_L                                                                                                                    " & vbNewLine _
                                    & "AND ZAI5.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND IDO2.IDO_DATE <= @PRINT_FROM                                                                                                                   " & vbNewLine _
                                    & "AND IDO2.IDO_DATE >= CASE WHEN @GETU_FLG = '1' THEN @ZAI_GETU ELSE IDO2.IDO_DATE END                                                               " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "--出荷データ(C_OUTKA_S)                                                                                                                                 " & vbNewLine _
                                    & "UNION ALL                                                                                                                                          " & vbNewLine _
                                    & "SELECT                                                                                                                                    " & vbNewLine _
                                    & "OUTKA_NO_L                                                                                                                                         " & vbNewLine _
                                    & ",OUTKA_NO_M                                                                                                                                        " & vbNewLine _
                                    & ",OUTKA_NO_S                                                                                                                                        " & vbNewLine _
                                    & ",CUST_CD_L                                                                                                                                         " & vbNewLine _
                                    & ",CUST_CD_M                                                                                                                                         " & vbNewLine _
                                    & ",CUST_NM_L                                                                                                                                         " & vbNewLine _
                                    & ",ZAI_REC_NO                                                                                                                                        " & vbNewLine _
                                    & ",GOODS_CD_NRS                                                                                                                                      " & vbNewLine _
                                    & ",GOODS_CD_CUST                                                                                                                                     " & vbNewLine _
                                    & ",LOT_NO                                                                                                                                            " & vbNewLine _
                                    & ",SERIAL_NO                                                                                                                                         " & vbNewLine _
                                    & ",PORA_ZAI_NB                                                                                                                                       " & vbNewLine _
                                    & ",PORA_ZAI_QT                                                                                                                                       " & vbNewLine _
                                    & ",NRS_BR_CD                                                                                                                                         " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "(SELECT                                                                                                                                            " & vbNewLine _
                                    & "OUTS.OUTKA_NO_L                   AS OUTKA_NO_L                                                                                                    " & vbNewLine _
                                    & ",OUTS.OUTKA_NO_M                   AS OUTKA_NO_M                                                                                                   " & vbNewLine _
                                    & ",OUTS.OUTKA_NO_S                   AS OUTKA_NO_S                                                                                                   " & vbNewLine _
                                    & ",OUTL.CUST_CD_L                    AS CUST_CD_L                                                                                                    " & vbNewLine _
                                    & ",OUTL.CUST_CD_M                    AS CUST_CD_M                                                                                                    " & vbNewLine _
                                    & ",''                                AS CUST_NM_L                                                                                                    " & vbNewLine _
                                    & ",OUTS.ZAI_REC_NO                   AS ZAI_REC_NO                                                                                                   " & vbNewLine _
                                    & ",OUTM.GOODS_CD_NRS                 AS GOODS_CD_NRS                                                                                                 " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST               AS GOODS_CD_CUST                                                                                                   " & vbNewLine _
                                    & ",ISNULL(OUTS.LOT_NO, '')           AS LOT_NO                                                                                                       " & vbNewLine _
                                    & ",ISNULL(OUTS.SERIAL_NO, '')        AS SERIAL_NO                                                                                                    " & vbNewLine _
                                    & ",OUTS.ALCTD_NB * -1                AS PORA_ZAI_NB                                                                                                  " & vbNewLine _
                                    & ",OUTS.ALCTD_QT * -1                AS PORA_ZAI_QT                                                                                                  " & vbNewLine _
                                    & ",OUTS.NRS_BR_CD                     AS NRS_BR_CD                                                                                                   " & vbNewLine _
                                    & "FROM                                                                                                                                               " & vbNewLine _
                                    & "$LM_TRN$..C_OUTKA_L OUTL                                                                                                                             " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..C_OUTKA_M OUTM                                                                                                                   " & vbNewLine _
                                    & "ON  OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                                              " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..C_OUTKA_S OUTS                                                                                                                   " & vbNewLine _
                                    & "ON  OUTS.NRS_BR_CD = OUTL.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND OUTS.OUTKA_NO_L = OUTL.OUTKA_NO_L                                                                                                              " & vbNewLine _
                                    & "AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M                                                                                                              " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                       " & vbNewLine _
                                    & "ON  OUTM.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                  " & vbNewLine _
                                    & "AND OUTM.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                            " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "OUTL.SYS_DEL_FLG = '0' AND OUTM.SYS_DEL_FLG = '0' AND OUTS.SYS_DEL_FLG = '0'                                                                                                                           " & vbNewLine _
                                    & "AND OUTM.ALCTD_KB <> '04'                                                                                                                          " & vbNewLine _
                                    & "AND OUTL.OUTKA_STATE_KB !< '60'                                                                                                                    " & vbNewLine _
                                    & "AND OUTL.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "AND OUTL.CUST_CD_L = @CUST_CD_L                                                                                                                    " & vbNewLine _
                                    & "AND OUTL.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                    & "AND OUTL.OUTKA_PLAN_DATE <= @PRINT_FROM                                                                                                            " & vbNewLine _
                                    & "AND OUTL.OUTKA_PLAN_DATE >= CASE WHEN @GETU_FLG = '1' THEN @ZAI_GETU ELSE OUTL.OUTKA_PLAN_DATE END                                                 " & vbNewLine _
                                    & ") BASE3                                                                                                                                            " & vbNewLine _
                                    & ") BASE4                                                                                                                                            " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "CUST_CD_L = @CUST_CD_L                                                                                                                             " & vbNewLine _
                                    & "AND CUST_CD_M = @CUST_CD_M                                                                                                                         " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "GROUP BY                                                                                                                                           " & vbNewLine _
                                    & "CUST_CD_L                                                                                                                                          " & vbNewLine _
                                    & ",CUST_CD_M                                                                                                                                         " & vbNewLine _
                                    & ",CUST_NM_L                                                                                                                                         " & vbNewLine _
                                    & ",ZAI_REC_NO                                                                                                                                        " & vbNewLine _
                                    & ",GOODS_CD_NRS                                                                                                                                      " & vbNewLine _
                                    & ",GOODS_CD_CUST                                                                                                                                     " & vbNewLine _
                                    & ",LOT_NO                                                                                                                                            " & vbNewLine _
                                    & ",SERIAL_NO                                                                                                                                         " & vbNewLine _
                                    & ",NRS_BR_CD                                                                                                                                         " & vbNewLine _
                                    & ") BASE                                                                                                                                             " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "WHERE                                                                                                                                              " & vbNewLine _
                                    & "BASE.PORA_ZAI_NB <> 0                                                                                                                              " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & ")                                                                                                                                                  " & vbNewLine _
                                    & "BASE10                                                                                                                                             " & vbNewLine _
                                    & "LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                                                                                                 " & vbNewLine _
                                    & "ON ZAITRS.NRS_BR_CD = BASE10.NRS_BR_CD                                                                                                             " & vbNewLine _
                                    & "AND ZAITRS.ZAI_REC_NO = BASE10.ZAI_REC_NO                                                                                                          " & vbNewLine _
                                    & "AND ZAITRS.SYS_DEL_FLG = '0'                                                                                                                       " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_GOODS MG                                                                                                                       " & vbNewLine _
                                    & "ON  ZAITRS.NRS_BR_CD = MG.NRS_BR_CD                                                                                                                " & vbNewLine _
                                    & "AND ZAITRS.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                          " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_CUST AS CUST                                                                                                                   " & vbNewLine _
                                    & "ON MG.NRS_BR_CD = CUST.NRS_BR_CD                                                                                                                   " & vbNewLine _
                                    & "AND MG.CUST_CD_L = CUST.CUST_CD_L                                                                                                                  " & vbNewLine _
                                    & "AND MG.CUST_CD_M = CUST.CUST_CD_M                                                                                                                  " & vbNewLine _
                                    & "AND MG.CUST_CD_S = CUST.CUST_CD_S                                                                                                                  " & vbNewLine _
                                    & "AND MG.CUST_CD_SS = CUST.CUST_CD_SS                                                                                                                " & vbNewLine _
                                    & "                                                                                                                                                   " & vbNewLine _
                                    & "LEFT JOIN                                                                                                                                          " & vbNewLine _
                                    & "$LM_MST$..M_SEIQTO SEIQ ON                                                                                                                           " & vbNewLine _
                                    & "SEIQ.SEIQTO_CD = CUST.HOKAN_SEIQTO_CD                                                                                                              " & vbNewLine _
                                    & "AND SEIQ.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "LEFT JOIN                                                                                                                                          " & vbNewLine _
                                    & "$LM_MST$..M_NRS_BR NRS ON                                                                                                                            " & vbNewLine _
                                    & "NRS.NRS_BR_CD = @NRS_BR_CD                                                                                                                         " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN_NRS ON                                                                                                                              " & vbNewLine _
                                    & "KBN_NRS.KBN_GROUP_CD = 'B044'                                                                                                                         " & vbNewLine _
                                    & "AND KBN_NRS.KBN_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..M_CUSTCOND COND ON                                                                                                                         " & vbNewLine _
                                    & "COND.NRS_BR_CD = @NRS_BR_CD                                                                                                                        " & vbNewLine _
                                    & "AND COND.CUST_CD_L = BASE10.CUST_CD_L                                                                                                              " & vbNewLine _
                                    & "AND COND.JOTAI_CD = ZAITRS.GOODS_COND_KB_3                                                                                                         " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN2 ON                                                                                                                              " & vbNewLine _
                                    & "KBN2.KBN_GROUP_CD = 'Z001'                                                                                                                         " & vbNewLine _
                                    & "AND KBN2.KBN_CD = ZAITRS.TAX_KB                                                                                                                    " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..M_SOKO SOKO ON                                                                                                                             " & vbNewLine _
                                    & "SOKO.NRS_BR_CD = @NRS_BR_CD                                                                                                                        " & vbNewLine _
                                    & "AND SOKO.WH_CD = ZAITRS.WH_CD                                                                                                                      " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..M_SOKO SOKO_2 ON                                                                                                                         " & vbNewLine _
                                    & "SOKO_2.NRS_BR_CD = @NRS_BR_CD                                                                                                                      " & vbNewLine _
                                    & "AND SOKO_2.WH_CD = SOKO.ZAIKO_WH_CD                                                                                                                " & vbNewLine _
                                    & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start   " & vbNewLine _
                                    & "LEFT JOIN                                                                                " & vbNewLine _
                                    & " (SELECT M_SOKO.*                                                                        " & vbNewLine _
                                    & "  FROM $LM_MST$..S_USER                                                                  " & vbNewLine _
                                    & "  LEFT JOIN LM_MST..M_SOKO                                                               " & vbNewLine _
                                    & "	ON S_USER.WH_CD = M_SOKO.WH_CD                                                          " & vbNewLine _
                                    & "	WHERE S_USER.USER_CD = @USER_ID)M_SOKO_USER                                             " & vbNewLine _
                                    & "ON M_SOKO_USER.NRS_BR_CD = ZAITRS.NRS_BR_CD                                              " & vbNewLine _
                                    & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add end     " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..M_CUST_DETAILS CUST_D ON                                                                                                                 " & vbNewLine _
                                    & "CUST_D.NRS_BR_CD   = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "AND CUST_D.CUST_CD = BASE10.CUST_CD_L + BASE10.CUST_CD_M                                                                                           " & vbNewLine _
                                    & "AND CUST_D.SUB_KB  = '09'                                                                                                                          " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..M_CUST_DETAILS MCD ON                                                                                                                    " & vbNewLine _
                                    & "MCD.NRS_BR_CD   = @NRS_BR_CD                                                                                                                       " & vbNewLine _
                                    & "AND MCD.CUST_CD = BASE10.CUST_CD_L + BASE10.CUST_CD_M + MG.CUST_CD_S                                                                               " & vbNewLine _
                                    & "AND MCD.SUB_KB  = '15'                                                                                                                             " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..M_CUST_DETAILS MCD2 ON                                                                                                                    " & vbNewLine _
                                    & "MCD2.NRS_BR_CD   = @NRS_BR_CD                                                                                                                       " & vbNewLine _
                                    & "AND MCD2.CUST_CD = BASE10.CUST_CD_L                                                                                                                 " & vbNewLine _
                                    & "AND MCD2.SUB_KB  = '18'                                                                                                                             " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN3 ON                                                                                                                              " & vbNewLine _
                                    & "KBN3.KBN_GROUP_CD = 'S005'                                                                                                                         " & vbNewLine _
                                    & "AND KBN3.KBN_CD = ZAITRS.GOODS_COND_KB_1                                                                                                           " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN4 ON                                                                                                                              " & vbNewLine _
                                    & "KBN4.KBN_GROUP_CD = 'S006'                                                                                                                         " & vbNewLine _
                                    & "AND KBN4.KBN_CD = ZAITRS.GOODS_COND_KB_2                                                                                                           " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN5 ON                                                                                                                              " & vbNewLine _
                                    & "KBN5.KBN_GROUP_CD = 'B002'                                                                                                                         " & vbNewLine _
                                    & "AND KBN5.KBN_CD = ZAITRS.OFB_KB                                                                                                           " & vbNewLine _
                                    & "--(2012.12.13)要望番号1657対応 -- START --                                                                                                " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN6 ON                                                                                                                            " & vbNewLine _
                                    & "KBN6.KBN_GROUP_CD = 'T021'                                                                                                                         " & vbNewLine _
                                    & "AND KBN6.KBN_CD = ZAITRS.TOU_NO                                                                                                                    " & vbNewLine _
                                    & "--(2012.12.13)要望番号1657対応 --  END  --                                                                                                         " & vbNewLine _
                                    & "--在庫の荷主での荷主帳票パターン取得                                                                                                                 " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                                                  " & vbNewLine _
                                    & "ON  MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "AND MCR1.CUST_CD_L = @CUST_CD_L                                                                                                                    " & vbNewLine _
                                    & "AND MCR1.CUST_CD_M = @CUST_CD_M                                                                                                                    " & vbNewLine _
                                    & "AND MCR1.PTN_ID = '32'                                                                                                                             " & vbNewLine _
                                    & "AND MCR1.CUST_CD_S = '00'                                                                                                                          " & vbNewLine _
                                    & "--帳票パターン取得                                                                                                                                         " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                                                        " & vbNewLine _
                                    & "ON  MR1.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                    & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                                                       " & vbNewLine _
                                    & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                                                       " & vbNewLine _
                                    & "AND MR1.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                    & "--商品Mの荷主での荷主帳票パターン取得                                                                                                                               " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                                                  " & vbNewLine _
                                    & "ON  MCR2.NRS_BR_CD = @NRS_BR_CD                                                                                                                    " & vbNewLine _
                                    & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                                                                                  " & vbNewLine _
                                    & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                                                                                  " & vbNewLine _
                                    & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                                                                                  " & vbNewLine _
                                    & "AND MCR2.PTN_ID = '32'                                                                                                                             " & vbNewLine _
                                    & "--帳票パターン取得                                                                                                                                         " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                                                        " & vbNewLine _
                                    & "ON  MR2.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                    & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                                                       " & vbNewLine _
                                    & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                                                       " & vbNewLine _
                                    & "AND MR2.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                    & "--存在しない場合の帳票パターン取得                                                                                                                                 " & vbNewLine _
                                    & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                                                        " & vbNewLine _
                                    & "ON  MR3.NRS_BR_CD = @NRS_BR_CD                                                                                                                     " & vbNewLine _
                                    & "AND MR3.PTN_ID = '32'                                                                                                                              " & vbNewLine _
                                    & "AND MR3.STANDARD_FLAG = '01'                                                                                                                       " & vbNewLine _
                                    & "AND MR3.SYS_DEL_FLG = '0'                                                                                                                          " & vbNewLine _
                                    & "LEFT OUTER JOIN                                                                                                                                    " & vbNewLine _
                                    & "$LM_MST$..Z_KBN KBN7 ON                                                                                                                            " & vbNewLine _
                                    & "KBN7.KBN_GROUP_CD = 'T043'                                                                                                                         " & vbNewLine _
                                    & "AND KBN7.KBN_NM2 = BASE10.CUST_CD_L                                                                                                                " & vbNewLine _
                                    & "AND KBN7.KBN_NM3 = BASE10.CUST_CD_M                                                                                                                " & vbNewLine _
                                    & "AND KBN7.SYS_DEL_FLG = '0'                                                                                                                         " & vbNewLine

#End Region

#Region "検索処理 SQL(GROUP BY句)"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                                                                                             " & vbNewLine _
                                         & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
                                         & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                    " & vbNewLine _
                                         & "      ELSE MR3.RPT_ID                                                                                                " & vbNewLine _
                                         & " END                                                                                                                 " & vbNewLine _
                                         & ",ZAITRS.NRS_BR_CD                                                                                                    " & vbNewLine _
                                         & ",SEIQ.SEIQTO_CD                                                                                                      " & vbNewLine _
                                         & ",CASE WHEN MCD.SET_NAIYO <> '' AND MCD.SET_NAIYO IS NOT NULL                                                         " & vbNewLine _
                                         & "      THEN MCD.SET_NAIYO                                                                                             " & vbNewLine _
                                         & "      ELSE SEIQ.SEIQTO_NM                                                                                            " & vbNewLine _
                                         & "      END                                                                                                            " & vbNewLine _
                                         & ",SEIQ.SEIQTO_BUSYO_NM                                                                                                " & vbNewLine _
                                         & ",ZAITRS.CUST_CD_L                                                                                                    " & vbNewLine _
                                         & ",ZAITRS.CUST_CD_M                                                                                                    " & vbNewLine _
                                         & ",CUST.CUST_NM_L                                                                                                      " & vbNewLine _
                                         & ",CUST.CUST_NM_M                                                                                                      " & vbNewLine _
                                         & ",NRS.AD_1                                                                                                            " & vbNewLine _
                                         & ",NRS.AD_2                                                                                                            " & vbNewLine _
                                         & ",ZAITRS.GOODS_COND_KB_1                                                                                              " & vbNewLine _
                                         & ",ZAITRS.GOODS_COND_KB_2                                                                                              " & vbNewLine _
                                         & ",ZAITRS.GOODS_COND_KB_3                                                                                              " & vbNewLine _
                                         & ",COND.INFERIOR_GOODS_KB                                                                                              " & vbNewLine _
                                         & ",ZAITRS.TAX_KB                                                                                                       " & vbNewLine _
                                         & ",KBN2.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",ZAITRS.OFB_KB                                                                                                       " & vbNewLine _
                                         & ",KBN5.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",SOKO.WH_CD                                                                                                          " & vbNewLine _
                                         & ",SOKO.WH_NM                                                                                                          " & vbNewLine _
                                         & ",KBN7.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",SOKO.WH_KB                                                                                                          " & vbNewLine _
                                         & ",SOKO.AD_1                                                                                                           " & vbNewLine _
                                         & ",SOKO.AD_2                                                                                                           " & vbNewLine _
                                         & ",SOKO.TEL                                                                                                            " & vbNewLine _
                                         & ",SOKO.ZAIKO_WH_CD                                                                                                    " & vbNewLine _
                                         & ",SOKO_2.AD_1                                                                                                         " & vbNewLine _
                                         & ",SOKO_2.AD_2                                                                                                         " & vbNewLine _
                                         & ",SOKO_2.TEL                                                                                                          " & vbNewLine _
                                         & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start                               " & vbNewLine _
                                         & ",M_SOKO_USER.AD_1                                                                                                    " & vbNewLine _
                                         & ",M_SOKO_USER.AD_2                                                                                                    " & vbNewLine _
                                         & ",M_SOKO_USER.TEL                                                                                                     " & vbNewLine _
                                         & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start                               " & vbNewLine _
                                         & ",MG.GOODS_CD_CUST                                                                                                    " & vbNewLine _
                                         & ",MG.GOODS_NM_1                                                                                                       " & vbNewLine _
                                         & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 --- START ---                                                      " & vbNewLine _
                                         & ",MG.GOODS_NM_2                                                                                                       " & vbNewLine _
                                         & ",MG.SEARCH_KEY_1                                                                                                     " & vbNewLine _
                                         & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 ---  END  ---                                                      " & vbNewLine _
                                         & ",BASE10.LOT_NO                                                                                                       " & vbNewLine _
                                         & ",MG.NB_UT                                                                                                            " & vbNewLine _
                                         & ",MG.PKG_NB                                                                                                           " & vbNewLine _
                                         & ",MG.STD_IRIME_UT                                                                                                     " & vbNewLine _
                                         & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                         " & vbNewLine _
                                         & ",ZAITRS.REMARK_OUT                                                                                                   " & vbNewLine _
                                         & ",KBN3.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",KBN4.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",COND.JOTAI_NM                                                                                                       " & vbNewLine _
                                         & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                         " & vbNewLine _
                                         & ",CASE WHEN CUST_D.SET_NAIYO <>'01' THEN BASE10.SERIAL_NO END                                                         " & vbNewLine _
                                         & ",ZAITRS.IRIME                                                                                                        " & vbNewLine _
                                         & ",MG.CUST_CD_S                                                                                                        " & vbNewLine _
                                         & ",MG.CUST_CD_SS                                                                                                       " & vbNewLine _
                                         & ",CUST.CUST_NM_S                                                                                                      " & vbNewLine _
                                         & ",CUST.CUST_NM_SS                                                                                                     " & vbNewLine _
                                         & ",NRS.NRS_BR_NM                                                                                                       " & vbNewLine _
                                         & ",KBN_NRS.KBN_NM1                                                                                                       " & vbNewLine _
                                         & ",NRS.TEL                                                                                                             " & vbNewLine _
                                         & ",CUST_D.SET_NAIYO                                                                                                             " & vbNewLine _
                                         & ",MCD2.SET_NAIYO                                                                                                             " & vbNewLine


    ''' <summary>
    ''' GROUP BY(LMD586用)---START---
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_586 As String = "GROUP BY                                                                                                             " & vbNewLine _
                                         & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
                                         & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                    " & vbNewLine _
                                         & "      ELSE MR3.RPT_ID                                                                                                " & vbNewLine _
                                         & " END                                                                                                                 " & vbNewLine _
                                         & ",ZAITRS.NRS_BR_CD                                                                                                    " & vbNewLine _
                                         & ",SEIQ.SEIQTO_CD                                                                                                      " & vbNewLine _
                                         & ",CASE WHEN MCD.SET_NAIYO <> '' AND MCD.SET_NAIYO IS NOT NULL                                                         " & vbNewLine _
                                         & "      THEN MCD.SET_NAIYO                                                                                             " & vbNewLine _
                                         & "      ELSE SEIQ.SEIQTO_NM                                                                                            " & vbNewLine _
                                         & "      END                                                                                                            " & vbNewLine _
                                         & ",SEIQ.SEIQTO_BUSYO_NM                                                                                                " & vbNewLine _
                                         & ",ZAITRS.CUST_CD_L                                                                                                    " & vbNewLine _
                                         & ",ZAITRS.CUST_CD_M                                                                                                    " & vbNewLine _
                                         & ",CUST.CUST_NM_L                                                                                                      " & vbNewLine _
                                         & ",CUST.CUST_NM_M                                                                                                      " & vbNewLine _
                                         & ",NRS.AD_1                                                                                                            " & vbNewLine _
                                         & ",NRS.AD_2                                                                                                            " & vbNewLine _
                                         & ",ZAITRS.GOODS_COND_KB_1                                                                                              " & vbNewLine _
                                         & ",ZAITRS.GOODS_COND_KB_2                                                                                              " & vbNewLine _
                                         & ",ZAITRS.GOODS_COND_KB_3                                                                                              " & vbNewLine _
                                         & ",COND.INFERIOR_GOODS_KB                                                                                              " & vbNewLine _
                                         & ",ZAITRS.TAX_KB                                                                                                       " & vbNewLine _
                                         & ",KBN2.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",ZAITRS.OFB_KB                                                                                                       " & vbNewLine _
                                         & ",KBN5.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",SOKO.WH_CD                                                                                                          " & vbNewLine _
                                         & ",SOKO.WH_NM                                                                                                          " & vbNewLine _
                                         & ",SOKO.WH_KB                                                                                                          " & vbNewLine _
                                         & ",SOKO.AD_1                                                                                                           " & vbNewLine _
                                         & ",SOKO.AD_2                                                                                                           " & vbNewLine _
                                         & ",SOKO.TEL                                                                                                            " & vbNewLine _
                                         & ",SOKO.ZAIKO_WH_CD                                                                                                    " & vbNewLine _
                                         & ",SOKO_2.AD_1                                                                                                         " & vbNewLine _
                                         & ",SOKO_2.AD_2                                                                                                         " & vbNewLine _
                                         & ",SOKO_2.TEL                                                                                                          " & vbNewLine _
                                         & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start                               " & vbNewLine _
                                         & ",M_SOKO_USER.AD_1                                                                                                    " & vbNewLine _
                                         & ",M_SOKO_USER.AD_2                                                                                                    " & vbNewLine _
                                         & ",M_SOKO_USER.TEL                                                                                                     " & vbNewLine _
                                         & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start                               " & vbNewLine _
                                         & ",MG.GOODS_CD_CUST                                                                                                    " & vbNewLine _
                                         & ",MG.GOODS_NM_1                                                                                                       " & vbNewLine _
                                         & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 --- START ---                                                      " & vbNewLine _
                                         & ",MG.GOODS_NM_2                                                                                                       " & vbNewLine _
                                         & ",MG.SEARCH_KEY_1                                                                                                     " & vbNewLine _
                                         & "--(2012.08.08)群馬対応 商品名2・荷主カテゴリ1追加 ---  END  ---                                                      " & vbNewLine _
                                         & ",BASE10.LOT_NO                                                                                                       " & vbNewLine _
                                         & "--(2012.12.03)追加　　　　　　　　　　　　　　　　　　　　　　　                                                     " & vbNewLine _
                                         & ",BASE10.ZAI_REC_NO                                                                                                   " & vbNewLine _
                                         & "--(2012.12.03)追加　　　　　　　　　　　　　　　　　　　　　　　                                                     " & vbNewLine _
                                         & ",MG.NB_UT                                                                                                            " & vbNewLine _
                                         & ",MG.PKG_NB                                                                                                           " & vbNewLine _
                                         & ",MG.STD_IRIME_UT                                                                                                     " & vbNewLine _
                                         & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                         " & vbNewLine _
                                         & ",ZAITRS.REMARK_OUT                                                                                                   " & vbNewLine _
                                         & ",KBN3.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",KBN4.KBN_NM1                                                                                                        " & vbNewLine _
                                         & ",COND.JOTAI_NM                                                                                                       " & vbNewLine _
                                         & "--(2012.12.03)追加　　　　　　　　　　　　　　　　　　　　　　　                                                     " & vbNewLine _
                                         & ",ZAITRS.REMARK                                                                                                       " & vbNewLine _
                                         & "--(2012.12.03)追加　　　　　　　　　　　　　　　　　　　　　　　                                                     " & vbNewLine _
                                         & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                         " & vbNewLine _
                                         & ",CASE WHEN CUST_D.SET_NAIYO <>'01' THEN BASE10.SERIAL_NO END                                                         " & vbNewLine _
                                         & ",ZAITRS.IRIME                                                                                                        " & vbNewLine _
                                         & ",MG.CUST_CD_S                                                                                                        " & vbNewLine _
                                         & ",MG.CUST_CD_SS                                                                                                       " & vbNewLine _
                                         & ",CUST.CUST_NM_S                                                                                                      " & vbNewLine _
                                         & ",CUST.CUST_NM_SS                                                                                                     " & vbNewLine _
                                         & ",NRS.NRS_BR_NM                                                                                                       " & vbNewLine _
                                         & ",KBN_NRS.KBN_NM1                                                                                                       " & vbNewLine _
                                         & ",NRS.TEL                                                                                                             " & vbNewLine _
                                         & ",CUST_D.SET_NAIYO                                                                                                             " & vbNewLine _
                                         & ",MCD2.SET_NAIYO                                                                                                             " & vbNewLine

    '(LMD586用)---  END---

    '(2012.12.13)要望番号1657 LMD587専用 -- START --
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_587 As String = " GROUP BY                                                                   " & vbNewLine _
                                             & "       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                             & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                             & "       ELSE MR3.RPT_ID                                                      " & vbNewLine _
                                             & "       END                                                                  " & vbNewLine _
                                             & "     , ZAITRS.NRS_BR_CD                                                     " & vbNewLine _
                                             & "     , SEIQ.SEIQTO_CD                                                       " & vbNewLine _
                                             & "     , CASE WHEN MCD.SET_NAIYO <> '' AND MCD.SET_NAIYO IS NOT NULL          " & vbNewLine _
                                             & "                 THEN MCD.SET_NAIYO                                         " & vbNewLine _
                                             & "       ELSE SEIQ.SEIQTO_NM                                                  " & vbNewLine _
                                             & "       END                                                                  " & vbNewLine _
                                             & "     , SEIQ.SEIQTO_BUSYO_NM                                                 " & vbNewLine _
                                             & "     , ZAITRS.CUST_CD_L                                                     " & vbNewLine _
                                             & "     , ZAITRS.CUST_CD_M                                                     " & vbNewLine _
                                             & "     , CUST.CUST_NM_L                                                       " & vbNewLine _
                                             & "     , CUST.CUST_NM_M                                                       " & vbNewLine _
                                             & "     , NRS.AD_1                                                             " & vbNewLine _
                                             & "     , NRS.AD_2                                                             " & vbNewLine _
                                             & "     , ZAITRS.GOODS_COND_KB_1                                               " & vbNewLine _
                                             & "     , ZAITRS.GOODS_COND_KB_2                                               " & vbNewLine _
                                             & "     , ZAITRS.GOODS_COND_KB_3                                               " & vbNewLine _
                                             & "     , COND.INFERIOR_GOODS_KB                                               " & vbNewLine _
                                             & "     , ZAITRS.TAX_KB                                                        " & vbNewLine _
                                             & "     , KBN2.KBN_NM1                                                         " & vbNewLine _
                                             & "     , ZAITRS.OFB_KB                                                        " & vbNewLine _
                                             & "     , KBN5.KBN_NM1                                                         " & vbNewLine _
                                             & "     , SOKO.WH_CD                                                           " & vbNewLine _
                                             & "     , SOKO.WH_NM                                                           " & vbNewLine _
                                             & "     , SOKO.WH_KB                                                           " & vbNewLine _
                                             & "     , SOKO.AD_1                                                            " & vbNewLine _
                                             & "     , SOKO.AD_2                                                            " & vbNewLine _
                                             & "     , SOKO.TEL                                                             " & vbNewLine _
                                             & "     , SOKO.ZAIKO_WH_CD                                                     " & vbNewLine _
                                             & "     , SOKO_2.AD_1                                                          " & vbNewLine _
                                             & "     , SOKO_2.AD_2                                                          " & vbNewLine _
                                             & "     , SOKO_2.TEL                                                           " & vbNewLine _
                                             & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start   " & vbNewLine _
                                             & "     , M_SOKO_USER.AD_1                                                     " & vbNewLine _
                                             & "     , M_SOKO_USER.AD_2                                                     " & vbNewLine _
                                             & "     , M_SOKO_USER.TEL                                                      " & vbNewLine _
                                             & "--2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start   " & vbNewLine _
                                             & "     , MG.GOODS_CD_CUST                                                     " & vbNewLine _
                                             & "     , MG.GOODS_NM_1                                                        " & vbNewLine _
                                             & "     , MG.GOODS_NM_2                                                        " & vbNewLine _
                                             & "     , MG.SEARCH_KEY_1                                                      " & vbNewLine _
                                             & "     , BASE10.LOT_NO                                                        " & vbNewLine _
                                             & "     , MG.NB_UT                                                             " & vbNewLine _
                                             & "     , MG.PKG_NB                                                            " & vbNewLine _
                                             & "     , MG.STD_IRIME_UT                                                      " & vbNewLine _
                                             & "     , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE    " & vbNewLine _
                                             & "       ELSE ZAITRS.INKO_DATE                                                " & vbNewLine _
                                             & "       END                                                                  " & vbNewLine _
                                             & "     , ZAITRS.REMARK_OUT                                                    " & vbNewLine _
                                             & "     , KBN3.KBN_NM1                                                         " & vbNewLine _
                                             & "     , KBN4.KBN_NM1                                                         " & vbNewLine _
                                             & "     , COND.JOTAI_NM                                                        " & vbNewLine _
                                             & "     , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE    " & vbNewLine _
                                             & "       ELSE ZAITRS.INKO_DATE                                                " & vbNewLine _
                                             & "       END                                                                  " & vbNewLine _
                                             & "     , CASE WHEN CUST_D.SET_NAIYO <>'01' THEN BASE10.SERIAL_NO              " & vbNewLine _
                                             & "       END                                                                  " & vbNewLine _
                                             & "     , ZAITRS.IRIME                                                         " & vbNewLine _
                                             & "     , MG.CUST_CD_S                                                         " & vbNewLine _
                                             & "     , MG.CUST_CD_SS                                                        " & vbNewLine _
                                             & "     , CUST.CUST_NM_S                                                       " & vbNewLine _
                                             & "     , CUST.CUST_NM_SS                                                      " & vbNewLine _
                                             & "     , NRS.NRS_BR_NM                                                        " & vbNewLine _
                                             & "     , KBN_NRS.KBN_NM1                                                        " & vbNewLine _
                                             & "     , NRS.TEL                                                              " & vbNewLine _
                                             & "     , CUST_D.SET_NAIYO                                                     " & vbNewLine _
                                             & "     , MCD2.SET_NAIYO                                                       " & vbNewLine _
                                             & "--(2012.12.13)要望番号1657対応 -- START --                                  " & vbNewLine _
                                             & "     , KBN6.KBN_NM1                                                         " & vbNewLine _
                                             & "     , KBN6.KBN_NM2                                                         " & vbNewLine _
                                             & "     --TEST用, ZAITRS.TOU_NO                                                " & vbNewLine _
                                             & "--(2012.12.13)要望番号1657対応 --  END  --                                  " & vbNewLine

    '(2012.12.13)要望番号1657 LMD587専用 --  END  --

#End Region

#Region "検索処理 SQL(ORDER BY句)"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                                                                             " & vbNewLine _
                                    & " SEIQ.SEIQTO_CD                                                                                                           " & vbNewLine _
                                    & ",ZAITRS.CUST_CD_L                                                                                                         " & vbNewLine _
                                    & ",ZAITRS.CUST_CD_M                                                                                                         " & vbNewLine _
                                    & ",MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                    & ",MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.NRS_BR_CD                                                                                                         " & vbNewLine _
                                    & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --  START  --                                                        " & vbNewLine _
                                    & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                    & "		OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                    & "	  ELSE '01'                                                                                                              " & vbNewLine _
                                    & "	  END                                                                                                                    " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_1                                                                                                 " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_2                                                                                                 " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_3                                                                                                 " & vbNewLine _
                                    & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --   END   --                                                        " & vbNewLine _
                                    & ",ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                    & ",SOKO.WH_CD                                                                                                               " & vbNewLine _
                                    & "--要望番号:1057 対応Start 2012/05/23                                                                                      " & vbNewLine _
                                    & ",MG.GOODS_NM_1                                                                                                            " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                    & "--, CASE WHEN MCD2.SET_NAIYO = '01' THEN MG.GOODS_CD_CUST                                                                 " & vbNewLine _
                                    & "--   ELSE MG.GOODS_NM_1                                                                                                   " & vbNewLine _
                                    & "--   END                                                                                                                  " & vbNewLine _
                                    & "--要望番号:1057 対応End   2012/05/23                                                                                      " & vbNewLine _
                                    & ",BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.IRIME                                                                                                             " & vbNewLine _
                                    & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                              " & vbNewLine

    '---->要望番号:1057 対応Start 2012/05/23
    Private Const SQL_ORDER_BY_02 As String = "ORDER BY                                                                                                          " & vbNewLine _
                                    & " SEIQ.SEIQTO_CD                                                                                                           " & vbNewLine _
                                    & ",ZAITRS.CUST_CD_L                                                                                                         " & vbNewLine _
                                    & ",ZAITRS.CUST_CD_M                                                                                                         " & vbNewLine _
                                    & ",MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                    & ",MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.NRS_BR_CD                                                                                                         " & vbNewLine _
                                    & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --  START  --                                                        " & vbNewLine _
                                    & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                    & "		OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                    & "	  ELSE '01'                                                                                                              " & vbNewLine _
                                    & "	  END                                                                                                                    " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_1                                                                                                 " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_2                                                                                                 " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_3                                                                                                 " & vbNewLine _
                                    & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --   END   --                                                        " & vbNewLine _
                                    & ",ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                    & ",SOKO.WH_CD                                                                                                               " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST                                                                                                         " & vbNewLine _
                                    & ",BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.IRIME                                                                                                             " & vbNewLine _
                                    & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                              " & vbNewLine

    Private Const SQL_ORDER_BY_03 As String = "ORDER BY                                                                                                          " & vbNewLine _
                                    & " SEIQ.SEIQTO_CD                                                                                                           " & vbNewLine _
                                    & ",ZAITRS.CUST_CD_L                                                                                                         " & vbNewLine _
                                    & ",ZAITRS.CUST_CD_M                                                                                                         " & vbNewLine _
                                    & ",MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                    & ",MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.NRS_BR_CD                                                                                                         " & vbNewLine _
                                    & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --  START  --                                                        " & vbNewLine _
                                    & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                    & "		OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                    & "	  ELSE '01'                                                                                                              " & vbNewLine _
                                    & "	  END                                                                                                                    " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_1                                                                                                 " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_2                                                                                                 " & vbNewLine _
                                    & "--,ZAITRS.GOODS_COND_KB_3                                                                                                 " & vbNewLine _
                                    & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --   END   --                                                        " & vbNewLine _
                                    & ",ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                    & ",SOKO.WH_CD                                                                                                               " & vbNewLine _
                                    & ",MG.GOODS_NM_1                                                                                                                   " & vbNewLine _
                                    & ",MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                    & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                              " & vbNewLine _
                                    & ",BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                    & ",ZAITRS.IRIME                                                                                                             " & vbNewLine
    '<----要望番号:1057 対応End   2012/05/23

    '(2012/07/17) 要望番号:1175対応 --- START ---
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_573_01 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_NM_1                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_573_02 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_573_03 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_NM_1                                                                            " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine
    '(2012/07/17) 要望番号:1175対応 ---  END  ---

    '(2012/08/08) 群馬対応 --- START ---
    ''' <summary>
    ''' ORDER BY 
    ''' </summary>
    ''' <remarks>LMD579用：商品名2表示版</remarks>
    Private Const SQL_ORDER_BY_579_01 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_NM_2                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_579_02 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_579_03 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_NM_2                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine


    ''' <summary>
    ''' ORDER BY 
    ''' </summary>
    ''' <remarks>LMD581用：荷主カテゴリ1改頁版</remarks>
    Private Const SQL_ORDER_BY_581_01 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.SEARCH_KEY_1                                                                             " & vbNewLine _
                                                & "    , MG.GOODS_NM_1                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_581_02 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.SEARCH_KEY_1                                                                             " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_581_03 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.SEARCH_KEY_1                                                                             " & vbNewLine _
                                                & "    , MG.GOODS_NM_1                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine

    '(2012/08/08) 群馬対応 ---  END  ---

    '(2012/08/14) 埼玉日立物流対応 ---  START  ---
    ''' <summary>
    ''' ORDER BY 
    ''' </summary>
    ''' <remarks>LMD582用：荷主カテゴリ1改頁・表示版</remarks>
    Private Const SQL_ORDER_BY_582_01 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                                & "	    	OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                                & "	      ELSE '01'                                                                                                              " & vbNewLine _
                                                & "	      END                                                                                                                    " & vbNewLine _
                                                & "    , ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                                & "    , ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                                & "    , MG.SEARCH_KEY_1                                                                             " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_NM_1                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_582_02 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                                & "	    	OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                                & "	      ELSE '01'                                                                                                              " & vbNewLine _
                                                & "	      END                                                                                                                    " & vbNewLine _
                                                & "    , ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                                & "    , ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                                & "    , MG.SEARCH_KEY_1                                                                             " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine

    Private Const SQL_ORDER_BY_582_03 As String = "ORDER BY                                                                                          " & vbNewLine _
                                                & "      SEIQ.SEIQTO_CD                                                                              " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_L                                                                            " & vbNewLine _
                                                & "    , ZAITRS.CUST_CD_M                                                                            " & vbNewLine _
                                                & "    , CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                                & "	    	OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                                & "	      ELSE '01'                                                                                                              " & vbNewLine _
                                                & "	      END                                                                                                                    " & vbNewLine _
                                                & "    , ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                                & "    , ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                                & "    , MG.SEARCH_KEY_1                                                                             " & vbNewLine _
                                                & "    , MG.CUST_CD_S                                                                                " & vbNewLine _
                                                & "    , MG.CUST_CD_SS                                                                               " & vbNewLine _
                                                & "    , SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "    , MG.GOODS_NM_1                                                                               " & vbNewLine _
                                                & "    , MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & "    , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END " & vbNewLine _
                                                & "    , BASE10.LOT_NO                                                                               " & vbNewLine _
                                                & "    , ZAITRS.IRIME                                                                                " & vbNewLine

    '(2012/08/14) 埼玉日立物流対応 ---  END  ---

    '(2012/12/03) LMD586用 ---  START  ---
    ''' <summary>
    ''' ORDER BY 
    ''' </summary>
    ''' <remarks>LMD586用</remarks>
    Private Const SQL_ORDER_BY_586_01 As String = "ORDER BY                                                                                                             " & vbNewLine _
                                                  & " SEIQ.SEIQTO_CD                                                                                                           " & vbNewLine _
                                                  & ",ZAITRS.CUST_CD_L                                                                                                         " & vbNewLine _
                                                  & ",ZAITRS.CUST_CD_M                                                                                                         " & vbNewLine _
                                                  & ",MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                                  & ",MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                                  & ",ZAITRS.NRS_BR_CD                                                                                                         " & vbNewLine _
                                                  & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --  START  --                                                        " & vbNewLine _
                                                  & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                                  & "		OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                                  & "	  ELSE '01'                                                                                                              " & vbNewLine _
                                                  & "	  END                                                                                                                    " & vbNewLine _
                                                  & "--,ZAITRS.GOODS_COND_KB_1                                                                                                 " & vbNewLine _
                                                  & "--,ZAITRS.GOODS_COND_KB_2                                                                                                 " & vbNewLine _
                                                  & "--,ZAITRS.GOODS_COND_KB_3                                                                                                 " & vbNewLine _
                                                  & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --   END   --                                                        " & vbNewLine _
                                                  & ",ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                                  & ",ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                                  & ",SOKO.WH_CD                                                                                                               " & vbNewLine _
                                                  & "--要望番号:1057 対応Start 2012/05/23                                                                                      " & vbNewLine _
                                                  & ",MG.GOODS_NM_1                                                                                                            " & vbNewLine _
                                                  & ",MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                  & "--, CASE WHEN MCD2.SET_NAIYO = '01' THEN MG.GOODS_CD_CUST                                                                 " & vbNewLine _
                                                  & "--   ELSE MG.GOODS_NM_1                                                                                                   " & vbNewLine _
                                                  & "--   END                                                                                                                  " & vbNewLine _
                                                  & "--要望番号:1057 対応End   2012/05/23                                                                                      " & vbNewLine _
                                                  & ",BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                                  & ",ZAITRS.IRIME                                                                                                             " & vbNewLine _
                                                  & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                              " & vbNewLine _
                                                  & ",ZAITRS.REMARK                                                                                                          " & vbNewLine



    Private Const SQL_ORDER_BY_586_02 As String = "ORDER BY                                                                                                          " & vbNewLine _
                                                & " SEIQ.SEIQTO_CD                                                                                                           " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_L                                                                                                         " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_M                                                                                                         " & vbNewLine _
                                                & ",MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                                & ",MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                                & ",ZAITRS.NRS_BR_CD                                                                                                         " & vbNewLine _
                                                & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --  START  --                                                        " & vbNewLine _
                                                & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                                & "		OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                                & "	  ELSE '01'                                                                                                              " & vbNewLine _
                                                & "	  END                                                                                                                    " & vbNewLine _
                                                & "--,ZAITRS.GOODS_COND_KB_1                                                                                                 " & vbNewLine _
                                                & "--,ZAITRS.GOODS_COND_KB_2                                                                                                 " & vbNewLine _
                                                & "--,ZAITRS.GOODS_COND_KB_3                                                                                                 " & vbNewLine _
                                                & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --   END   --                                                        " & vbNewLine _
                                                & ",ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                                & ",ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                                & ",SOKO.WH_CD                                                                                                               " & vbNewLine _
                                                & ",MG.GOODS_CD_CUST                                                                                                         " & vbNewLine _
                                                & ",BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                                & ",ZAITRS.IRIME                                                                                                             " & vbNewLine _
                                                & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                              " & vbNewLine _
                                                & ",ZAITRS.REMARK                                                                                                         " & vbNewLine


    Private Const SQL_ORDER_BY_586_03 As String = "ORDER BY                                                                                                          " & vbNewLine _
                                                & " SEIQ.SEIQTO_CD                                                                                                           " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_L                                                                                                         " & vbNewLine _
                                                & ",ZAITRS.CUST_CD_M                                                                                                         " & vbNewLine _
                                                & ",MG.CUST_CD_S                                                                                                             " & vbNewLine _
                                                & ",MG.CUST_CD_SS                                                                                                            " & vbNewLine _
                                                & ",ZAITRS.NRS_BR_CD                                                                                                         " & vbNewLine _
                                                & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --  START  --                                                        " & vbNewLine _
                                                & ",CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                                              " & vbNewLine _
                                                & "		OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'                                    " & vbNewLine _
                                                & "	  ELSE '01'                                                                                                              " & vbNewLine _
                                                & "	  END                                                                                                                    " & vbNewLine _
                                                & "--,ZAITRS.GOODS_COND_KB_1                                                                                                 " & vbNewLine _
                                                & "--,ZAITRS.GOODS_COND_KB_2                                                                                                 " & vbNewLine _
                                                & "--,ZAITRS.GOODS_COND_KB_3                                                                                                 " & vbNewLine _
                                                & "--(2012.04.17) Notes990 商品状態区分ソート改善　篠原 --   END   --                                                        " & vbNewLine _
                                                & ",ZAITRS.TAX_KB                                                                                                            " & vbNewLine _
                                                & ",ZAITRS.OFB_KB                                                                                                            " & vbNewLine _
                                                & ",SOKO.WH_CD                                                                                                               " & vbNewLine _
                                                & ",MG.GOODS_NM_1                                                                                                                   " & vbNewLine _
                                                & ",MG.GOODS_CD_CUST                                                                            " & vbNewLine _
                                                & ",CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END                              " & vbNewLine _
                                                & ",BASE10.LOT_NO                                                                                                            " & vbNewLine _
                                                & ",ZAITRS.IRIME                                                                                                             " & vbNewLine _
                                                & ",ZAITRS.REMARK                                                                                                          " & vbNewLine
    '(2012/12/03) LMD586用 ---  END    ---

    '(2012/07/17) 要望番号:1175対応 --- START ---
    ''' <summary>
    ''' ORDER BY LMD587用 倉庫コード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_587_01 As String = " ORDER BY                                                                                            " & vbNewLine _
                                                & "       SEIQ.SEIQTO_CD                                                                                " & vbNewLine _
                                                & "     , ZAITRS.CUST_CD_L                                                                              " & vbNewLine _
                                                & "     , ZAITRS.CUST_CD_M                                                                              " & vbNewLine _
                                                & "     , MG.CUST_CD_S                                                                                  " & vbNewLine _
                                                & "     , MG.CUST_CD_SS                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.NRS_BR_CD                                                                              " & vbNewLine _
                                                & "     , CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                   " & vbNewLine _
                                                & "              OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'      " & vbNewLine _
                                                & "       ELSE '01'                                                                                     " & vbNewLine _
                                                & "       END                                                                                           " & vbNewLine _
                                                & "     , ZAITRS.TAX_KB                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.OFB_KB                                                                                 " & vbNewLine _
                                                & "--(2012.12.13)要望番号1657対応 -- START --                                                           " & vbNewLine _
                                                & "     --, SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "     , CASE WHEN KBN6.KBN_NM1 <> '' THEN KBN6.KBN_NM2                                                " & vbNewLine _
                                                & "       ELSE SOKO.WH_CD                                                                               " & vbNewLine _
                                                & "       END                                                                                           " & vbNewLine _
                                                & "--(2012.12.13)要望番号1657対応 --  END  --                                                           " & vbNewLine _
                                                & "     , MG.GOODS_NM_1                                                                                 " & vbNewLine _
                                                & "     , MG.GOODS_CD_CUST                                                                              " & vbNewLine _
                                                & "     , BASE10.LOT_NO                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.IRIME                                                                                  " & vbNewLine _
                                                & "     , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END   " & vbNewLine

    Private Const SQL_ORDER_BY_587_02 As String = " ORDER BY                                                                                            " & vbNewLine _
                                                & "       SEIQ.SEIQTO_CD                                                                                " & vbNewLine _
                                                & "     , ZAITRS.CUST_CD_L                                                                              " & vbNewLine _
                                                & "     , ZAITRS.CUST_CD_M                                                                              " & vbNewLine _
                                                & "     , MG.CUST_CD_S                                                                                  " & vbNewLine _
                                                & "     , MG.CUST_CD_SS                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.NRS_BR_CD                                                                              " & vbNewLine _
                                                & "     , CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                   " & vbNewLine _
                                                & "              OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'      " & vbNewLine _
                                                & "       ELSE '01'                                                                                     " & vbNewLine _
                                                & "       END                                                                                           " & vbNewLine _
                                                & "     , ZAITRS.TAX_KB                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.OFB_KB                                                                                 " & vbNewLine _
                                                & "--(2012.12.13)要望番号1657対応 -- START --                                                           " & vbNewLine _
                                                & "     --, SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "     , CASE WHEN KBN6.KBN_NM1 <> '' THEN KBN6.KBN_NM2                                                " & vbNewLine _
                                                & "       ELSE SOKO.WH_CD                                                                               " & vbNewLine _
                                                & "       END                                                                                           " & vbNewLine _
                                                & "--(2012.12.13)要望番号1657対応 --  END  --                                                           " & vbNewLine _
                                                & "     , MG.GOODS_CD_CUST                                                                              " & vbNewLine _
                                                & "     , BASE10.LOT_NO                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.IRIME                                                                                  " & vbNewLine _
                                                & "     , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END   " & vbNewLine

    Private Const SQL_ORDER_BY_587_03 As String = " ORDER BY                                                                                            " & vbNewLine _
                                                & "       SEIQ.SEIQTO_CD                                                                                " & vbNewLine _
                                                & "     , ZAITRS.CUST_CD_L                                                                              " & vbNewLine _
                                                & "     , ZAITRS.CUST_CD_M                                                                              " & vbNewLine _
                                                & "     , MG.CUST_CD_S                                                                                  " & vbNewLine _
                                                & "     , MG.CUST_CD_SS                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.NRS_BR_CD                                                                              " & vbNewLine _
                                                & "     , CASE WHEN (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 = '')                   " & vbNewLine _
                                                & "              OR (ZAITRS.GOODS_COND_KB_1 + GOODS_COND_KB_2 + GOODS_COND_KB_3 IS NULL) THEN '00'      " & vbNewLine _
                                                & "       ELSE '01'                                                                                     " & vbNewLine _
                                                & "       END                                                                                           " & vbNewLine _
                                                & "     , ZAITRS.TAX_KB                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.OFB_KB                                                                                 " & vbNewLine _
                                                & "--(2012.12.13)要望番号1657対応 -- START --                                                           " & vbNewLine _
                                                & "     --, SOKO.WH_CD                                                                                  " & vbNewLine _
                                                & "     , CASE WHEN KBN6.KBN_NM1 <> '' THEN KBN6.KBN_NM2                                                " & vbNewLine _
                                                & "       ELSE SOKO.WH_CD                                                                               " & vbNewLine _
                                                & "       END                                                                                           " & vbNewLine _
                                                & "--(2012.12.13)要望番号1657対応 --  END  --                                                           " & vbNewLine _
                                                & "     , MG.GOODS_NM_1                                                                                 " & vbNewLine _
                                                & "     , MG.GOODS_CD_CUST                                                                              " & vbNewLine _
                                                & "     , CASE WHEN RTRIM(ZAITRS.INKO_DATE) = '' THEN ZAITRS.INKO_PLAN_DATE ELSE ZAITRS.INKO_DATE END   " & vbNewLine _
                                                & "     , BASE10.LOT_NO                                                                                 " & vbNewLine _
                                                & "     , ZAITRS.IRIME                                                                                  " & vbNewLine

    '(2012/07/17) 要望番号:1175対応 ---  END  ---

#End Region

#End Region

#Region "その他特定処理 SQL"

    '(2012.04.05) 在庫証明書2枚出力対応 -- START --
    ''' <summary>
    ''' 設定値(荷主明細マスタ)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCUST_DETAILS As String = " SELECT SET_NAIYO    AS SET_NAIYO     " & vbNewLine _
                                                     & "      , SET_NAIYO_2  AS SET_NAIYO_2   " & vbNewLine _
                                                     & "      , SET_NAIYO_3  AS SET_NAIYO_3   " & vbNewLine _
                                                     & "   FROM $LM_MST$..M_CUST_DETAILS MCD  " & vbNewLine _
                                                     & "  WHERE MCD.NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                                     & "    AND MCD.CUST_CD     = @CUST_CD_L  " & vbNewLine _
                                                     & "    AND MCD.SUB_KB      = '24'        " & vbNewLine _
                                                     & "    AND MCD.SYS_DEL_FLG = '0'         " & vbNewLine _
                                                     & "  ORDER BY MCD.CUST_CD_EDA            " & vbNewLine

    '(2012.04.05) 在庫証明書2枚出力対応 --  END  --

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
        Dim inTbl As DataTable = ds.Tables("LMD570IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD570DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMD570DAC.SQL_FROM_MPrt)        'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
        '荷主コード(小)があれば、 荷主コード(小)･(極小)を条件とする
        If inTbl.Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = False Then
            Call Me.SetConditionWhereSQL_587("0")
        End If
        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 --  END  --

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD570DAC", "SelectMPrt", cmd)

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
    ''' 在庫テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD570IN")

        '(2012/07/17) 要望番号1175対応 --- START ---
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")
        '(2012/07/17) 要望番号1175対応 ---  END  ---

        '20120222STR
        'SORT順取得用
        'Dim SORT As String = ds.Tables("LMD570OUT").Rows(0)("ORDER_BY_KB").ToString()
        '20120222END

        '(2012.04.05) 在庫証明書2枚出力対応 -- START --
        '荷主明細マスタの設定値取得
        Me.SelectMCustDetailsData(ds)
        '(2012.04.05) 在庫証明書2枚出力対応 --  END  --

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'SQL振り分け(2012.12.03)
        'SQL構築(データ抽出用Select句)
        Select Case rptTbl.Rows(0).Item("RPT_ID").ToString()
            Case "LMD586", "LMD588"
                Me._StrSql.Append(LMD570DAC.SQL_SELECT_DATA_586)
            Case "LMD587"
                Me._StrSql.Append(LMD570DAC.SQL_SELECT_DATA_587)
            Case Else
                Me._StrSql.Append(LMD570DAC.SQL_SELECT_DATA)
        End Select

        'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMD570DAC.SQL_FROM)

        'SQL構築(条件設定)
        Call Me.SetConditionMasterSQL()
        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
        '荷主コード(小)があれば、 荷主コード(小)･(極小)を条件とする
        If inTbl.Rows(0).Item("CUST_CD_S").ToString.Trim.Equals("") = False Then
            Call Me.SetConditionWhereSQL_587("1")
        End If
        '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 --  END  --

        'SQL振り分け(2012.12.03)
        'SQL構築(データ抽出用GROUP BY句)
        Select Case rptTbl.Rows(0).Item("RPT_ID").ToString()
            Case "LMD586", "LMD588"
                Me._StrSql.Append(LMD570DAC.SQL_GROUP_BY_586)
            Case "LMD587"
                Me._StrSql.Append(LMD570DAC.SQL_GROUP_BY_587)
            Case Else
                Me._StrSql.Append(LMD570DAC.SQL_GROUP_BY)
        End Select

        'SQL構築(データ抽出用ORDER BY句)
        '(2012/08/08) 群馬対応 --- START ---
        Select Case rptTbl.Rows(0).Item("RPT_ID").ToString()
            Case "LMD573"
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_573_01)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_573_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_573_03)
                End Select

            Case "LMD579"
                '群馬対応 LMD580：商品名2表示
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_579_01)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_579_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_579_03)
                End Select

            Case "LMD581"
                '群馬対応 LMD581：荷主カテゴリにて改頁
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_581_01)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_581_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_581_03)
                End Select

            Case "LMD582"
                '埼玉日立物流FN対応 LMD582：荷主カテゴリにて改頁・表示
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_582_01)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_582_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_582_03)
                End Select

            Case "LMD586", "LMD588"
                'LMD586
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_586_01)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_586_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_586_03)
                End Select

                '(2012.12.13)要望番号1657対応 -- START -- 
            Case "LMD587"
                'LMD587
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_587_01)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_587_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_587_03)
                End Select
                '(2012.12.13)要望番号1657対応 --  END  -- 

            Case Else
                Select Case Me._Row.Item("SORT_KBN").ToString
                    Case "01"
                        '商品名順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY)
                    Case "02"
                        '商品コード順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_02)
                    Case "03"
                        '商品名・入庫日順
                        Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_03)
                End Select
        End Select

        ''(2012/07/17) 要望番号1175対応 --- START ---
        ''SQL構築(データ抽出用ORDER BY句)
        'If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMD573" Then
        '    Select Case Me._Row.Item("SORT_KBN").ToString
        '        Case "01"
        '            '商品名順
        '            Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_573_01)
        '        Case "02"
        '            '商品コード順
        '            Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_573_02)
        '        Case "03"
        '            '商品名・入庫日順
        '            Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_573_03)
        '    End Select

        'Else
        '    Select Case Me._Row.Item("SORT_KBN").ToString
        '        Case "01"
        '            '商品名順
        '            Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY)
        '        Case "02"
        '            '商品コード順
        '            Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_02)
        '        Case "03"
        '            '商品名・入庫日順
        '            Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_03)
        '    End Select
        'End If


        ''---->要望番号:1057 対応Start 2012/05/23
        ''Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY)             'SQL構築(データ抽出用ORDER BY句)

        'If Me._Row.Item("SORT_KBN").ToString = "01" Then
        '    '商品名順
        '    Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY)          'SQL構築(データ抽出用ORDER BY句)

        'ElseIf Me._Row.Item("SORT_KBN").ToString = "02" Then
        '    '商品コード順
        '    Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_02)       'SQL構築(データ抽出用ORDER BY句)

        'ElseIf Me._Row.Item("SORT_KBN").ToString = "03" Then
        '    '商品名・入庫日順
        '    Me._StrSql.Append(LMD570DAC.SQL_ORDER_BY_03)       'SQL構築(データ抽出用ORDER BY句)
        'End If
        '<----要望番号:1057 対応End   2012/05/23

        '(2012/07/17)要望番号 1175対応 ---  END  ---

        '(2012/08/08) 群馬対応 --- END1 ---

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 1200

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD570DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SEIQTO_BUSYO_NM", "SEIQTO_BUSYO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("NRS_AD_1", "NRS_AD_1")
        map.Add("NRS_AD_2", "NRS_AD_2")
        map.Add("GOODS_COND", "GOODS_COND")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("OFB_KB_NM", "OFB_KB_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NB_UT", "NB_UT")
        map.Add("IRIME_NB", "IRIME_NB")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("ZAI_NB", "ZAI_NB")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("KOSU", "KOSU")
        map.Add("ZAI_QT", "ZAI_QT")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("PRINT_FROM", "PRINT_FROM")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("TEL", "TEL")
        map.Add("SERIAL_NO_GROUP_KB", "SERIAL_NO_GROUP_KB")
        map.Add("ORDER_BY_KB", "ORDER_BY_KB")
        map.Add("HASU_ROW", "HASU_ROW")
        '(2012.08.08) 群馬対応 --- START ---
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SERIAL_NO", "SERIAL_NO")
        '(2012.08.08) 群馬対応 ---  END  ---

        '(2013.01.20) 埼玉BP対応 LMC588追加 -- START --
        '(2012.12.03) 千葉追加---START---
        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMD586" _
        Or rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMD588" Then
            map.Add("REMARK", "REMARK")
        End If
        '(2012.12.03) 千葉追加---  END---
        '(2013.01.20) 埼玉BP対応 LMC588追加 --  END  --

        '使用SQLの分岐にて例外の画面IDの場合マッピングから除外
        If Not (rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMD586" _
        OrElse rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMD587" _
        OrElse rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMD588") Then
            map.Add("SOKO_CD", "SOKO_CD")
        End If

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD570OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '' ''検索条件部に入力された条件とパラメータ設定
        '' ''Dim whereStr As String = String.Empty

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_FROM", Me._Row.Item("PRINT_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_GETU", Me._Row.Item("ZAI_GETU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GETU_FLG", Me._Row.Item("GETU_FLG").ToString(), DBDataType.CHAR))
            '2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_ID", MyBase.GetUserID(), DBDataType.NVARCHAR))
            '2018/03/08 001033 20180131【LMS】横浜BC帳票住所がYCCになっている対応 Annen add end
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KAKUIN_FLG", Me._Row.Item("KAKUIN_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 -- START --
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks>LMD587 在庫証明書(小･極小毎)向け</remarks>
    Private Sub SetConditionWhereSQL_587(ByVal SyoriPTN As String)

        'SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '荷主コード(小)
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If SyoriPTN = "0" Then
                    andstr.Append(" AND ")
                Else
                    andstr.Append(" WHERE ")
                End If
                andstr.Append(" MG.CUST_CD_S = @CUST_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(極小)
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append(" AND ")
                End If

                andstr.Append(" MG.CUST_CD_SS = @CUST_CD_SS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub
    '(2012.12.13)要望番号1671 在庫証明書(小･極小)対応 --  END  --

    ''''''' <summary>
    '''''''  LMC520INパラメータ設定
    ''''''' </summary>
    ''''''' <remarks>荷主明細マスタ存在抽出用SQLの構築</remarks>
    '' ''Private Sub setIndataParameter(ByVal _Row As DataRow)

    '' ''    'SQLパラメータ初期化
    '' ''    Me._SqlPrmList = New ArrayList()

    '' ''    'パラメータ設定
    '' ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", _Row("NRS_BR_CD"), DBDataType.CHAR))
    '' ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", _Row("OUTKA_NO_L"), DBDataType.CHAR))

    '' ''End Sub


    '(2012.04.05) 在庫証明書2枚出力対応 -- START --
    ''' <summary>
    '''荷主明細マスタ(設定値)取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>荷主明細マスタ取得SQLの構築・発行</remarks>
    Private Function SelectMCustDetailsData(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMD570IN").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD570DAC.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
        Call Me.SetConditionMasterSQL()                            '条件設定
        'Call Me.SetConditionMasterSQL(Me._Row)                        '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD570DAC", "SelectMCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO")

        reader.Close()

        Return ds

    End Function

    '(2012.04.05) 在庫証明書2枚出力対応 --  END  --

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
