' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB500    : 入荷受付表
'  作  成  者       :  [shinohara]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	INL.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'01'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE  WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                     " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                 " & vbNewLine _
                                            & " BASE2.RPT_ID                           AS RPT_ID                       " & vbNewLine _
                                            & ",BASE2.NRS_BR_CD                        AS NRS_BR_CD                    " & vbNewLine _
                                            & ",BASE2.INKA_NO_L                        AS INKA_NO_L                    " & vbNewLine _
                                            & ",BASE2.PRINT_SORT                       AS PRINT_SORT                   " & vbNewLine _
                                            & ",BASE2.INKA_NO_M                        AS INKA_NO_M                    " & vbNewLine _
                                            & ",BASE2.INKA_NO_S                        AS INKA_NO_S                    " & vbNewLine _
                                            & ",BASE2.USER_NM                          AS USER_NM                      " & vbNewLine _
                                            & ",BASE2.NRS_BR_NM                        AS NRS_BR_NM                    " & vbNewLine _
                                            & ",BASE2.WH_NM                            AS WH_NM                        " & vbNewLine _
                                            & ",BASE2.INKA_DATE                        AS INKA_DATE                    " & vbNewLine _
                                            & ",BASE2.CUST_CD_L                        AS CUST_CD_L                    " & vbNewLine _
                                            & ",BASE2.CUST_CD_M                        AS CUST_CD_M                    " & vbNewLine _
                                            & ",BASE2.INKA_TTL_NB                      AS INKA_TTL_NB                  " & vbNewLine _
                                            & ",BASE2.CUST_NM_L                        AS CUST_NM_L                    " & vbNewLine _
                                            & ",BASE2.INKA_PLAN_QT                     AS INKA_PLAN_QT                 " & vbNewLine _
                                            & ",BASE2.INKA_PLAN_QT_UT                  AS INKA_PLAN_QT_UT              " & vbNewLine _
                                            & ",BASE2.CUST_NM_M                        AS CUST_NM_M                    " & vbNewLine _
                                            & ",BASE2.UKETSUKE_USER                    AS UKETSUKE_USER                " & vbNewLine _
                                            & ",BASE2.OUTKA_FROM_ORD_NO_L              AS OUTKA_FROM_ORD_NO_L          " & vbNewLine _
                                            & ",BASE2.BUYER_ORD_NO_L                   AS BUYER_ORD_NO_L               " & vbNewLine _
                                            & ",BASE2.DEST_NM_L                        AS DEST_NM_L                    " & vbNewLine _
                                            & ",BASE2.REMARK                           AS REMARK                       " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_L01                     AS SAGYO_CD_L01                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_L02                     AS SAGYO_CD_L02                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_L03                     AS SAGYO_CD_L03                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_L04                     AS SAGYO_CD_L04                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_L05                     AS SAGYO_CD_L05                 " & vbNewLine _
                                            & ",BASE2.TOU_NO                           AS TOU_NO                       " & vbNewLine _
                                            & ",BASE2.SITU_NO                          AS SITU_NO                      " & vbNewLine _
                                            & ",BASE2.ZONE_CD                          AS ZONE_CD                      " & vbNewLine _
                                            & ",BASE2.LOCA                             AS LOCA                         " & vbNewLine _
                                            & ",BASE2.GOODS_CD_CUST                    AS GOODS_CD_CUST                " & vbNewLine _
                                            & ",BASE2.GOODS_NM_1                       AS GOODS_NM_1                   " & vbNewLine _
                                            & ",BASE2.LOT_NO                           AS LOT_NO                       " & vbNewLine _
                                            & ",BASE2.IRIME                            AS IRIME                        " & vbNewLine _
                                            & ",BASE2.STD_IRIME_UT                     AS STD_IRIME_UT                 " & vbNewLine _
                                            & ",BASE2.KONSU                            AS KONSU                        " & vbNewLine _
                                            & ",BASE2.HASU                             AS HASU                         " & vbNewLine _
                                            & ",BASE2.PKG_NB                           AS PKG_NB                       " & vbNewLine _
                                            & ",BASE2.KOSU                             AS KOSU                         " & vbNewLine _
                                            & ",BASE2.SURYO                            AS SURYO                        " & vbNewLine _
                                            & ",BASE2.NB_UT                            AS NB_UT                        " & vbNewLine _
                                            & ",BASE2.PKG_UT                           AS PKG_UT                       " & vbNewLine _
                                            & ",BASE2.GOODS_CRT_DATE                   AS GOODS_CRT_DATE               " & vbNewLine _
                                            & ",BASE2.LT_DATE                          AS LT_DATE                      " & vbNewLine _
                                            & ",BASE2.SERIAL_NO                        AS SERIAL_NO                    " & vbNewLine _
                                            & ",BASE2.ONDO_KB                          AS ONDO_KB                      " & vbNewLine _
                                            & ",BASE2.ONDO_MX                          AS ONDO_MX                      " & vbNewLine _
                                            & ",BASE2.ONDO_MM                          AS ONDO_MM                      " & vbNewLine _
                                            & ",BASE2.GOODS_COND_KB_1                  AS GOODS_COND_KB_1              " & vbNewLine _
                                            & ",BASE2.GOODS_COND_KB_2                  AS GOODS_COND_KB_2              " & vbNewLine _
                                            & ",BASE2.GOODS_COND_KB_3                  AS GOODS_COND_KB_3              " & vbNewLine _
                                            & ",BASE2.SPD_KB                           AS SPD_KB                       " & vbNewLine _
                                            & ",BASE2.OFB_KB                           AS OFB_KB                       " & vbNewLine _
                                            & ",BASE2.REMARK_OUT                       AS REMARK_OUT                   " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_M01                     AS SAGYO_CD_M01                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_M02                     AS SAGYO_CD_M02                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_M03                     AS SAGYO_CD_M03                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_M04                     AS SAGYO_CD_M04                 " & vbNewLine _
                                            & ",BASE2.SAGYO_CD_M05                     AS SAGYO_CD_M05                 " & vbNewLine _
                                            & ",ML01.SAGYO_RYAK                        AS SAGYO_NM_L01                 " & vbNewLine _
                                            & ",ML02.SAGYO_RYAK                        AS SAGYO_NM_L02                 " & vbNewLine _
                                            & ",ML03.SAGYO_RYAK                        AS SAGYO_NM_L03                 " & vbNewLine _
                                            & ",ML04.SAGYO_RYAK                        AS SAGYO_NM_L04                 " & vbNewLine _
                                            & ",ML05.SAGYO_RYAK                        AS SAGYO_NM_L05                 " & vbNewLine _
                                            & ",MM01.SAGYO_RYAK                        AS SAGYO_NM_M01                 " & vbNewLine _
                                            & ",MM02.SAGYO_RYAK                        AS SAGYO_NM_M02                 " & vbNewLine _
                                            & ",MM03.SAGYO_RYAK                        AS SAGYO_NM_M03                 " & vbNewLine _
                                            & ",MM04.SAGYO_RYAK                        AS SAGYO_NM_M04                 " & vbNewLine _
                                            & ",MM05.SAGYO_RYAK                        AS SAGYO_NM_M05                 " & vbNewLine _
                                            & ",BASE2.SHOBO_CD                         AS SHOBO_CD  --ADD 2019/03/29 依頼番号 : 005131【LMS】群馬入庫予定表_危険品区分を表示する " & vbNewLine _
                                            & ",BASE2.RUI                              AS RUI       --ADD 2019/03/29 依頼番号 : 005131【LMS】群馬入庫予定表_危険品区分を表示する " & vbNewLine _
                                            & ",BASE2.HINMEI                           AS HINMEI    --ADD 2019/03/29 依頼番号 : 005131【LMS】群馬入庫予定表_危険品区分を表示する " & vbNewLine _
                                            & "FROM                                                                    " & vbNewLine _
                                            & "(                                                                       " & vbNewLine _
                                            & "SELECT                                                                  " & vbNewLine _
                                            & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                        " & vbNewLine _
                                            & "WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                             " & vbNewLine _
                                            & "ELSE MR3.RPT_ID END                      AS RPT_ID                      " & vbNewLine _
                                            & ",INL.NRS_BR_CD                        AS NRS_BR_CD                      " & vbNewLine _
                                            & ",INL.INKA_NO_L                        AS INKA_NO_L                      " & vbNewLine _
                                            & ",INM.PRINT_SORT                       AS PRINT_SORT                     " & vbNewLine _
                                            & ",INM.INKA_NO_M                        AS INKA_NO_M                      " & vbNewLine _
                                            & ",INS.INKA_NO_S                        AS INKA_NO_S                      " & vbNewLine _
                                            & ",USR_PRT.USER_NM                      AS USER_NM                        " & vbNewLine _
                                            & ",MNB.NRS_BR_NM                        AS NRS_BR_NM                      " & vbNewLine _
                                            & ",SOKO.WH_NM                           AS WH_NM                          " & vbNewLine _
                                            & ",INL.INKA_DATE                        AS INKA_DATE                      " & vbNewLine _
                                            & ",INL.CUST_CD_L                        AS CUST_CD_L                      " & vbNewLine _
                                            & ",INL.CUST_CD_M                        AS CUST_CD_M                      " & vbNewLine _
                                            & ",INL.INKA_TTL_NB                      AS INKA_TTL_NB                    " & vbNewLine _
                                            & ",CUST.CUST_NM_L                       AS CUST_NM_L                      " & vbNewLine _
                                            & ",INL.INKA_PLAN_QT                     AS INKA_PLAN_QT                   " & vbNewLine _
                                            & ",INL.INKA_PLAN_QT_UT                  AS INKA_PLAN_QT_UT                " & vbNewLine _
                                            & ",CUST.CUST_NM_M                       AS CUST_NM_M                      " & vbNewLine _
                                            & ",USR.USER_NM                          AS UKETSUKE_USER                  " & vbNewLine _
                                            & ",INL.OUTKA_FROM_ORD_NO_L              AS OUTKA_FROM_ORD_NO_L            " & vbNewLine _
                                            & ",INL.BUYER_ORD_NO_L                   AS BUYER_ORD_NO_L                 " & vbNewLine _
                                            & ",CASE WHEN EDIL.INKA_CTL_NO_L IS NOT NULL THEN DESTEDI.DEST_NM          " & vbNewLine _
                                            & "ELSE DESTL.DEST_NM                                                      " & vbNewLine _
                                            & "END                                  AS DEST_NM_L                       " & vbNewLine _
                                            & ",INL.REMARK                           AS REMARK                         " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                               " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + '000') and                           " & vbNewLine _
                                            & "E.iozs_kb='10'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 1),'') as SAGYO_CD_L01                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                               " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + '000') and                           " & vbNewLine _
                                            & "E.iozs_kb='10'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 2),'') as SAGYO_CD_L02                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                               " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + '000') and                           " & vbNewLine _
                                            & "E.iozs_kb='10'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 3),'') as SAGYO_CD_L03                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                                " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + '000') and                           " & vbNewLine _
                                            & "E.iozs_kb='10'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 4),'') as SAGYO_CD_L04                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                                " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + '000') and                           " & vbNewLine _
                                            & "E.iozs_kb='10'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 5),'') as SAGYO_CD_L05                                 " & vbNewLine _
                                            & ",INS.TOU_NO                           AS TOU_NO                         " & vbNewLine _
                                            & ",INS.SITU_NO                          AS SITU_NO                        " & vbNewLine _
                                            & ",INS.ZONE_CD                          AS ZONE_CD                        " & vbNewLine _
                                            & ",INS.LOCA                             AS LOCA                           " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST                     AS GOODS_CD_CUST                  " & vbNewLine _
                                            & ",CASE WHEN INS.INKA_NO_S IS NOT NULL THEN MG.GOODS_NM_1                 " & vbNewLine _
                                            & "ELSE EDIM.GOODS_NM                                                      " & vbNewLine _
                                            & "END AS GOODS_NM_1                                                       " & vbNewLine _
                                            & ",ISNULL(INS.LOT_NO                                                      " & vbNewLine _
                                            & ",ISNULL(EDIM.LOT_NO , ''))           AS LOT_NO                          " & vbNewLine _
                                            & ",ISNULL(INS.IRIME                                                       " & vbNewLine _
                                            & ",ISNULL(EDIM.IRIME , '0'))           AS IRIME                           " & vbNewLine _
                                            & ",MG.STD_IRIME_UT                      AS STD_IRIME_UT                   " & vbNewLine _
                                            & ",ISNULL(INS.KONSU                                                       " & vbNewLine _
                                            & ",ISNULL(EDIM.NB , '0'))           AS KONSU                              " & vbNewLine _
                                            & ",ISNULL(INS.HASU                                                        " & vbNewLine _
                                            & ",ISNULL(EDIM.HASU , '0'))           AS HASU                             " & vbNewLine _
                                            & ",MG.PKG_NB                            AS PKG_NB                         " & vbNewLine _
                                            & ",CASE WHEN INS.KONSU IS NULL THEN                                       " & vbNewLine _
                                            & "ISNULL(EDIM.NB , 0)                                                     " & vbNewLine _
                                            & "* ISNULL(MG.PKG_NB   , 0)                                               " & vbNewLine _
                                            & "+ ISNULL(EDIM.HASU    , 0)                                              " & vbNewLine _
                                            & "ELSE                                                                    " & vbNewLine _
                                            & "ISNULL(INS.KONSU , 0)                                                   " & vbNewLine _
                                            & "* ISNULL(MG.PKG_NB   , 0)                                               " & vbNewLine _
                                            & "+ ISNULL(INS.HASU    , 0)                                               " & vbNewLine _
                                            & "END                                  AS KOSU                            " & vbNewLine _
                                            & ",CASE WHEN INS.KONSU IS NULL THEN                                       " & vbNewLine _
                                            & "(ISNULL(EDIM.NB , 0)                                                    " & vbNewLine _
                                            & "* ISNULL(MG.PKG_NB   , 0)                                               " & vbNewLine _
                                            & "+ ISNULL(EDIM.HASU    , 0))                                             " & vbNewLine _
                                            & "* ISNULL(EDIM.IRIME , 0)                                                " & vbNewLine _
                                            & "ELSE                                                                    " & vbNewLine _
                                            & "(ISNULL(INS.KONSU , 0)                                                  " & vbNewLine _
                                            & "* ISNULL(MG.PKG_NB   , 0)                                               " & vbNewLine _
                                            & "+ ISNULL(INS.HASU    , 0))                                              " & vbNewLine _
                                            & "* ISNULL(INS.IRIME , 0)                                                 " & vbNewLine _
                                            & "END                                   AS SURYO                          " & vbNewLine _
                                            & ",MG.NB_UT                             AS NB_UT                          " & vbNewLine _
                                            & ",MG.PKG_UT                            AS PKG_UT                         " & vbNewLine _
                                            & ",INS.GOODS_CRT_DATE                   AS GOODS_CRT_DATE                 " & vbNewLine _
                                            & ",INS.LT_DATE                          AS LT_DATE                        " & vbNewLine _
                                            & ",ISNULL(INS.SERIAL_NO                                                   " & vbNewLine _
                                            & ",ISNULL(EDIM.SERIAL_NO , ''))         AS SERIAL_NO                      " & vbNewLine _
                                            & ",ONDO_MX                              AS ONDO_MX                        " & vbNewLine _
                                            & ",ONDO_MM                              AS ONDO_MM                        " & vbNewLine _
                                            & ",KBN_01.KBN_NM1                       AS ONDO_KB                        " & vbNewLine _
                                            & ",KBN_02.KBN_NM1                       AS GOODS_COND_KB_1                " & vbNewLine _
                                            & ",KBN_03.KBN_NM1                       AS GOODS_COND_KB_2                " & vbNewLine _
                                            & ",MCC.JOTAI_NM                         AS GOODS_COND_KB_3                " & vbNewLine _
                                            & ",KBN_04.KBN_NM1                       AS SPD_KB                         " & vbNewLine _
                                            & ",KBN_05.KBN_NM1                       AS OFB_KB                         " & vbNewLine _
                                            & ",INS.REMARK                           AS REMARK_OUT                     " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                                " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + INM.inka_no_m) and                   " & vbNewLine _
                                            & "E.iozs_kb='11'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 1),'') as SAGYO_CD_M01                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                                " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + INM.inka_no_m) and                   " & vbNewLine _
                                            & "E.iozs_kb='11'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 2),'') as SAGYO_CD_M02                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                               " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + INM.inka_no_m) and                   " & vbNewLine _
                                            & "E.iozs_kb='11'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 3),'') as SAGYO_CD_M03                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                               " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + INM.inka_no_m) and                   " & vbNewLine _
                                            & "E.iozs_kb='11'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 4),'') as SAGYO_CD_M04                                 " & vbNewLine _
                                            & ",ISNULL((select base.sagyo_cd                                           " & vbNewLine _
                                            & "from                                                                    " & vbNewLine _
                                            & "(select                                                                 " & vbNewLine _
                                            & "sagyo_cd as sagyo_cd,ROW_NUMBER() OVER                                  " & vbNewLine _
                                            & "(PARTITION BY inoutka_no_lM ORDER BY inoutka_no_lM)                     " & vbNewLine _
                                            & "as  num                                                                 " & vbNewLine _
                                            & "from $LM_TRN$..E_SAGYO E                                               " & vbNewLine _
                                            & "where E.NRS_BR_CD = INL.NRS_BR_CD and                                   " & vbNewLine _
                                            & "E.inoutka_no_LM = (INM.inka_no_l + INM.inka_no_m) and                   " & vbNewLine _
                                            & "E.iozs_kb='11'                                                          " & vbNewLine _
                                            & ") as base                                                               " & vbNewLine _
                                            & "where base.num = 5),'') as SAGYO_CD_M05                                 " & vbNewLine _
                                            & ",ISNULL(MG.SHOBO_CD,'')                 AS SHOBO_CD      --ADD 2019/03/29 依頼番号 : 005131    " & vbNewLine _
                                            & ",ISNULL(MS.RUI,'')                      AS RUI           --ADD 2019/03/29 依頼番号 : 005131    " & vbNewLine _
                                            & ",ISNULL(MS.HINMEI,'')                   AS HINMEI        --ADD 2019/03/29 依頼番号 : 005131    " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                            " & vbNewLine _
                                            & "--入荷L                                                  " & vbNewLine _
                                            & "$LM_TRN$..B_INKA_L INL                                   " & vbNewLine _
                                            & "--入荷M                                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..B_INKA_M INM                         " & vbNewLine _
                                            & "ON   INL.NRS_BR_CD = INM.NRS_BR_CD                       " & vbNewLine _
                                            & "AND  INL.INKA_NO_L = INM.INKA_NO_L                       " & vbNewLine _
                                            & "AND  INM.SYS_DEL_FLG  = '0'                              " & vbNewLine _
                                            & "--入荷M EDI                                              " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..H_INKAEDI_M EDIM                     " & vbNewLine _
                                            & "ON   INM.NRS_BR_CD = EDIM.NRS_BR_CD                      " & vbNewLine _
                                            & "AND  INM.INKA_NO_L = EDIM.INKA_CTL_NO_L                  " & vbNewLine _
                                            & "AND  INM.INKA_NO_M = EDIM.INKA_CTL_NO_M                  " & vbNewLine _
                                            & "AND  EDIM.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                            & "--入荷S                                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..B_INKA_S INS                         " & vbNewLine _
                                            & "ON  INM.NRS_BR_CD    = INS.NRS_BR_CD                     " & vbNewLine _
                                            & "AND INM.INKA_NO_L    = INS.INKA_NO_L                     " & vbNewLine _
                                            & "AND INM.INKA_NO_M    = INS.INKA_NO_M                     " & vbNewLine _
                                            & "AND INS.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                            & "--商品M                                                  " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS MG                           " & vbNewLine _
                                            & "ON  INM.NRS_BR_CD    = MG.NRS_BR_CD                      " & vbNewLine _
                                            & "AND INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                   " & vbNewLine _
                                            & "AND MG.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                            & "	  --入荷EDIL                                           	" & vbNewLine _
                                            & "	  LEFT JOIN                                            	" & vbNewLine _
                                            & "	  (                                            	        " & vbNewLine _
                                            & "	    SELECT                                          	" & vbNewLine _
                                            & "	     NRS_BR_CD                                         	" & vbNewLine _
                                            & "	    ,INKA_CTL_NO_L                                      " & vbNewLine _
                                            & "	    ,CUST_CD_L                                          " & vbNewLine _
                                            & "	    ,OUTKA_MOTO                                         " & vbNewLine _
                                            & "	    ,SYS_DEL_FLG                                        " & vbNewLine _
                                            & "	    FROM                                          	    " & vbNewLine _
                                            & "	    $LM_TRN$..H_INKAEDI_L                               " & vbNewLine _
                                            & "	    GROUP BY                                          	" & vbNewLine _
                                            & "	     NRS_BR_CD                                         	" & vbNewLine _
                                            & "	    ,INKA_CTL_NO_L                                      " & vbNewLine _
                                            & "	    ,CUST_CD_L                                          " & vbNewLine _
                                            & "	    ,OUTKA_MOTO                                         " & vbNewLine _
                                            & "	    ,SYS_DEL_FLG                                        " & vbNewLine _
                                            & "	  ) EDIL                                            	" & vbNewLine _
                                            & "	  ON  EDIL.NRS_BR_CD = INL.NRS_BR_CD                    " & vbNewLine _
                                            & "	  AND EDIL.INKA_CTL_NO_L = INL.INKA_NO_L                " & vbNewLine _
                                            & "	  AND EDIL.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "	  --運送L                                            	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_TRN$..F_UNSO_L UL                      	" & vbNewLine _
                                            & "	  ON  INL.NRS_BR_CD    = UL.NRS_BR_CD                	" & vbNewLine _
                                            & "	  AND INL.INKA_NO_L    = UL.INOUTKA_NO_L             	" & vbNewLine _
                                            & "	  AND UL.MOTO_DATA_KB  = '10'                        	" & vbNewLine _
                                            & "	  AND UL.SYS_DEL_FLG   = '0'                         	" & vbNewLine _
                                            & "	  --入荷Lでの荷主帳票パターン取得                    	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                  	" & vbNewLine _
                                            & "	  ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                 	" & vbNewLine _
                                            & "	  AND INL.CUST_CD_L = MCR1.CUST_CD_L                 	" & vbNewLine _
                                            & "	  AND INL.CUST_CD_M = MCR1.CUST_CD_M                 	" & vbNewLine _
                                            & "	  AND '00'          = MCR1.CUST_CD_S                 	" & vbNewLine _
                                            & "	  AND MCR1.PTN_ID   = '01'                           	" & vbNewLine _
                                            & "	  --帳票パターン取得                                 	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_RPT MR1                        	" & vbNewLine _
                                            & "	  ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                 	" & vbNewLine _
                                            & "	  AND MR1.PTN_ID    = MCR1.PTN_ID                    	" & vbNewLine _
                                            & "	  AND MR1.PTN_CD    = MCR1.PTN_CD                    	" & vbNewLine _
                                            & "   AND MR1.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                            & "	  --商品Mの荷主での荷主帳票パターン取得              	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                  	" & vbNewLine _
                                            & "	  ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                  	" & vbNewLine _
                                            & "	  AND MG.CUST_CD_L = MCR2.CUST_CD_L                  	" & vbNewLine _
                                            & "	  AND MG.CUST_CD_M = MCR2.CUST_CD_M                  	" & vbNewLine _
                                            & "	  AND MG.CUST_CD_S = MCR2.CUST_CD_S                  	" & vbNewLine _
                                            & "	  AND MCR2.PTN_ID  = '01'                            	" & vbNewLine _
                                            & "	  --帳票パターン取得                                 	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_RPT MR2                        	" & vbNewLine _
                                            & "	  ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                 	" & vbNewLine _
                                            & "	  AND MR2.PTN_ID    = MCR2.PTN_ID                    	" & vbNewLine _
                                            & "	  AND MR2.PTN_CD    = MCR2.PTN_CD                    	" & vbNewLine _
                                            & "   AND MR2.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                            & "	  --存在しない場合の帳票パターン取得                 	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_RPT MR3                        	" & vbNewLine _
                                            & "	  ON  MR3.NRS_BR_CD     = INL.NRS_BR_CD              	" & vbNewLine _
                                            & "	  AND MR3.PTN_ID        = '01'                       	" & vbNewLine _
                                            & "	  AND MR3.STANDARD_FLAG = '01'                       	" & vbNewLine _
                                            & "   AND MR3.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                            & "	  --日陸営業所マスタ                                 	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_NRS_BR MNB                     	" & vbNewLine _
                                            & "	  ON  MNB.NRS_BR_CD     = INL.NRS_BR_CD              	" & vbNewLine _
                                            & "	  --倉庫マスタ                                       	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_SOKO SOKO                      	" & vbNewLine _
                                            & "	  ON  SOKO.NRS_BR_CD    = INL.NRS_BR_CD              	" & vbNewLine _
                                            & "	  AND SOKO.WH_CD        = INL.WH_CD                  	" & vbNewLine _
                                            & "	  --荷主マスタ                                       	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_CUST CUST                      	" & vbNewLine _
                                            & "	  ON  CUST.NRS_BR_CD    = MG.NRS_BR_CD              	" & vbNewLine _
                                            & "	  AND CUST.CUST_CD_L    = MG.CUST_CD_L              	" & vbNewLine _
                                            & "	  AND CUST.CUST_CD_M    = MG.CUST_CD_M              	" & vbNewLine _
                                            & "	  AND CUST.CUST_CD_S    = MG.CUST_CD_S               	" & vbNewLine _
                                            & "	  AND CUST.CUST_CD_SS   = MG.CUST_CD_SS                 " & vbNewLine _
                                            & "   --届先M（出荷元取得入荷L参照）                        " & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_DEST DESTL                     	" & vbNewLine _
                                            & "	  ON  DESTL.NRS_BR_CD    = INL.NRS_BR_CD              	" & vbNewLine _
                                            & "	  AND DESTL.CUST_CD_L    = INL.CUST_CD_L              	" & vbNewLine _
                                            & "	  AND DESTL.DEST_CD      = UL.ORIG_CD               	" & vbNewLine _
                                            & "   --届先M（出荷元取得EDIL参照）                         " & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_DEST DESTEDI                 	" & vbNewLine _
                                            & "	  ON  DESTEDI.NRS_BR_CD    = EDIL.NRS_BR_CD          	" & vbNewLine _
                                            & "	  AND DESTEDI.CUST_CD_L    = EDIL.CUST_CD_L          	" & vbNewLine _
                                            & "	  AND DESTEDI.DEST_CD      = EDIL.OUTKA_MOTO            " & vbNewLine _
                                            & "	  --荷主別商品状態区分マスタ                         	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..M_CUSTCOND MCC                   	" & vbNewLine _
                                            & "	  ON  MCC.NRS_BR_CD    = INL.NRS_BR_CD               	" & vbNewLine _
                                            & "	  AND MCC.CUST_CD_L    = INL.CUST_CD_L               	" & vbNewLine _
                                            & "	  AND MCC.JOTAI_CD     = INS.GOODS_COND_KB_3        	" & vbNewLine _
                                            & "	  --ユーザーマスタ(印刷)                               	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..S_USER USR_PRT                    " & vbNewLine _
                                            & "	  ON  USR_PRT.USER_CD      = INL.UKETSUKELIST_PRT_USER  " & vbNewLine _
                                            & "	  --ユーザーマスタ                                   	" & vbNewLine _
                                            & "	  LEFT JOIN $LM_MST$..S_USER USR                       	" & vbNewLine _
                                            & "	  ON  USR.USER_CD      = INL.UKETSUKELIST_PRT_USER      " & vbNewLine _
                                            & "	  --温度(区分01)                                     	" & vbNewLine _
                                            & "	  LEFT JOIN  $LM_MST$..Z_KBN KBN_01                    	" & vbNewLine _
                                            & "	  ON                                                 	" & vbNewLine _
                                            & "	   KBN_01.KBN_GROUP_CD = 'O002'                      	" & vbNewLine _
                                            & "	  AND                                                	" & vbNewLine _
                                            & "	   MG.ONDO_KB      = KBN_01.KBN_CD                   	" & vbNewLine _
                                            & "	  --商品状態区分1(中身)(区分02)                      	" & vbNewLine _
                                            & "	  LEFT JOIN  $LM_MST$..Z_KBN KBN_02                    	" & vbNewLine _
                                            & "	  ON                                                 	" & vbNewLine _
                                            & "	   KBN_02.KBN_GROUP_CD = 'S005'                      	" & vbNewLine _
                                            & "	  AND                                                	" & vbNewLine _
                                            & "	   INS.GOODS_COND_KB_1= KBN_02.KBN_CD                	" & vbNewLine _
                                            & "	  --商品状態区分2(外観)(区分03)                      	" & vbNewLine _
                                            & "	  LEFT JOIN  $LM_MST$..Z_KBN KBN_03                    	" & vbNewLine _
                                            & "	  ON                                                 	" & vbNewLine _
                                            & "	   KBN_03.KBN_GROUP_CD = 'S006'                      	" & vbNewLine _
                                            & "	  AND                                                	" & vbNewLine _
                                            & "	   INS.GOODS_COND_KB_2 = KBN_03.KBN_CD               	" & vbNewLine _
                                            & "	  --保留品区分(区分04)                               	" & vbNewLine _
                                            & "	  LEFT JOIN  $LM_MST$..Z_KBN KBN_04                    	" & vbNewLine _
                                            & "	  ON                                                 	" & vbNewLine _
                                            & "	   KBN_04.KBN_GROUP_CD = 'H003'                      	" & vbNewLine _
                                            & "	  AND                                                	" & vbNewLine _
                                            & "	   INS.SPD_KB      = KBN_04.KBN_CD                   	" & vbNewLine _
                                            & "	  --簿外品区分(区分05)                               	" & vbNewLine _
                                            & "	  LEFT JOIN  $LM_MST$..Z_KBN KBN_05                    	" & vbNewLine _
                                            & "	  ON                                                 	" & vbNewLine _
                                            & "	   KBN_05.KBN_GROUP_CD = 'B002'                      	" & vbNewLine _
                                            & "	  AND                                                	" & vbNewLine _
                                            & "	   INS.OFB_KB      = KBN_05.KBN_CD                   	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	  --消防マスタ   ADD 2019/04/11 依頼番号 : 005131       " & vbNewLine _
                                            & "	  LEFT JOIN  $LM_MST$..M_SHOBO  MS                      " & vbNewLine _
                                            & "	  ON                                                 	" & vbNewLine _
                                            & "	      MS.SHOBO_CD    =  MG.SHOBO_CD                     " & vbNewLine _
                                            & "	  AND MS.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                            & "	  WHERE INL.NRS_BR_CD  = @NRS_BR_CD                  	" & vbNewLine _
                                            & "	   AND INL.INKA_NO_L   = @INKA_NO_L                  	" & vbNewLine _
                                            & "	   AND INL.SYS_DEL_FLG = '0'                         	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "GROUP BY MR1.PTN_CD                  " & vbNewLine _
                                            & ",MR2.PTN_CD                          " & vbNewLine _
                                            & ",MR1.RPT_ID                          " & vbNewLine _
                                            & ",MR2.RPT_ID                          " & vbNewLine _
                                            & ",MR3.RPT_ID                          " & vbNewLine _
                                            & ",INL.NRS_BR_CD                       " & vbNewLine _
                                            & ",INL.INKA_NO_L                       " & vbNewLine _
                                            & ",INM.PRINT_SORT                      " & vbNewLine _
                                            & ",INM.INKA_NO_M                       " & vbNewLine _
                                            & ",INS.INKA_NO_S                       " & vbNewLine _
                                            & ",USR_PRT.USER_NM                     " & vbNewLine _
                                            & ",MNB.NRS_BR_NM                       " & vbNewLine _
                                            & ",SOKO.WH_NM                          " & vbNewLine _
                                            & ",INL.INKA_DATE                       " & vbNewLine _
                                            & ",INL.CUST_CD_L                       " & vbNewLine _
                                            & ",INL.CUST_CD_M                       " & vbNewLine _
                                            & ",INL.INKA_TTL_NB                     " & vbNewLine _
                                            & ",CUST.CUST_NM_L                      " & vbNewLine _
                                            & ",INL.INKA_PLAN_QT                    " & vbNewLine _
                                            & ",INL.INKA_PLAN_QT_UT                 " & vbNewLine _
                                            & ",USR.USER_NM                         " & vbNewLine _
                                            & ",INL.OUTKA_FROM_ORD_NO_L             " & vbNewLine _
                                            & ",CUST.CUST_NM_M                      " & vbNewLine _
                                            & ",INL.BUYER_ORD_NO_L                  " & vbNewLine _
                                            & ",EDIL.INKA_CTL_NO_L                  " & vbNewLine _
                                            & ",DESTL.DEST_NM                       " & vbNewLine _
                                            & ",DESTEDI.DEST_NM                     " & vbNewLine _
                                            & ",INL.REMARK                          " & vbNewLine _
                                            & ",INM.INKA_NO_L                       " & vbNewLine _
                                            & ",INS.TOU_NO                          " & vbNewLine _
                                            & ",INS.SITU_NO                         " & vbNewLine _
                                            & ",INS.ZONE_CD                         " & vbNewLine _
                                            & ",INS.LOCA                            " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST                    " & vbNewLine _
                                            & ",MG.GOODS_NM_1                       " & vbNewLine _
                                            & ",EDIM.GOODS_NM                       " & vbNewLine _
                                            & ",INS.LOT_NO                          " & vbNewLine _
                                            & ",EDIM.LOT_NO                         " & vbNewLine _
                                            & ",INS.IRIME                           " & vbNewLine _
                                            & ",EDIM.IRIME                          " & vbNewLine _
                                            & ",MG.STD_IRIME_UT                     " & vbNewLine _
                                            & ",INS.KONSU                           " & vbNewLine _
                                            & ",EDIM.NB                             " & vbNewLine _
                                            & ",INS.HASU                            " & vbNewLine _
                                            & ",EDIM.HASU                           " & vbNewLine _
                                            & ",MG.PKG_NB                           " & vbNewLine _
                                            & ",MG.NB_UT                            " & vbNewLine _
                                            & ",MG.PKG_UT                           " & vbNewLine _
                                            & ",INS.GOODS_CRT_DATE                  " & vbNewLine _
                                            & ",INS.LT_DATE                         " & vbNewLine _
                                            & ",INS.SERIAL_NO                       " & vbNewLine _
                                            & ",EDIM.SERIAL_NO                      " & vbNewLine _
                                            & ",EDIM.SERIAL_NO                      " & vbNewLine _
                                            & ",MG.ONDO_MX                          " & vbNewLine _
                                            & ",MG.ONDO_MM                          " & vbNewLine _
                                            & ",KBN_01.KBN_NM1                      " & vbNewLine _
                                            & ",KBN_02.KBN_NM1                      " & vbNewLine _
                                            & ",KBN_03.KBN_NM1                      " & vbNewLine _
                                            & ",MCC.JOTAI_NM                        " & vbNewLine _
                                            & ",KBN_04.KBN_NM1                      " & vbNewLine _
                                            & ",KBN_05.KBN_NM1                      " & vbNewLine _
                                            & ",INS.REMARK                          " & vbNewLine _
                                            & ",MG.SHOBO_CD  ----ADD 2019/03/29 依頼番号 : 005131 " & vbNewLine _
                                            & ",MS.RUI       ----ADD 2019/03/29 依頼番号 : 005131 " & vbNewLine _
                                            & ",MS.HINMEI    ----ADD 2019/03/29 依頼番号 : 005131 " & vbNewLine _
                                            & "                                     " & vbNewLine _
                                            & "	 ) AS BASE2                                          	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO ML01                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_L01 = ML01.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO ML02                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_L02 = ML02.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO ML03                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_L03 = ML03.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO ML04                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_L04 = ML04.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO ML05                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_L05 = ML05.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO MM01                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_M01 = MM01.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO MM02                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_M02 = MM02.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO MM03                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_M03 = MM03.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO MM04                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_M04 = MM04.SAGYO_CD              	" & vbNewLine _
                                            & "	                                                     	" & vbNewLine _
                                            & "	 LEFT JOIN                                           	" & vbNewLine _
                                            & "	 $LM_MST$..M_SAGYO MM05                                	" & vbNewLine _
                                            & "	 ON  BASE2.SAGYO_CD_M05 = MM05.SAGYO_CD              	" & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_Mprt As String = "FROM                                                            " & vbNewLine _
                                     & "--入荷L                                    " & vbNewLine _
                                     & "$LM_TRN$..B_INKA_L INL                     " & vbNewLine _
                                     & "LEFT join $LM_TRN$..B_INKA_M INM                     " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = INM.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INL.INKA_NO_L = INM.INKA_NO_L                     " & vbNewLine _
                                     & "--入荷M                                   " & vbNewLine _
                                     & "LEFT join $LM_TRN$..B_INKA_S INS                     " & vbNewLine _
                                     & "ON  INM.NRS_BR_CD = INS.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INM.INKA_NO_L = INS.INKA_NO_L                     " & vbNewLine _
                                     & "AND INM.INKA_NO_M = INS.INKA_NO_M                     " & vbNewLine _
                                     & "AND INM.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                     & "--入荷S                     " & vbNewLine _
                                     & "LEFT join $LM_MST$..M_GOODS MG                     " & vbNewLine _
                                     & "ON  INM.NRS_BR_CD = MG.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                     " & vbNewLine _
                                     & "AND INS.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                     & "--運送L                     " & vbNewLine _
                                     & "LEFT join $LM_TRN$..F_UNSO_L UL                     " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = UL.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INL.INKA_NO_L = UL.INOUTKA_NO_L                     " & vbNewLine _
                                     & "AND  UL.MOTO_DATA_KB = '10'                     " & vbNewLine _
                                     & "AND UL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                     & "--入荷Lでの荷主帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                     " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                     " & vbNewLine _
                                     & "AND INL.CUST_CD_L = MCR1.CUST_CD_L                     " & vbNewLine _
                                     & "AND INL.CUST_CD_M = MCR1.CUST_CD_M                     " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                     " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '01'                     " & vbNewLine _
                                     & "--帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                     " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                      " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                     " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                     " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                     " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                     " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                     " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                     " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                     " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '01'                     " & vbNewLine _
                                     & "--帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                     " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                      " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                     " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                     " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                     " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = INL.NRS_BR_CD                      " & vbNewLine _
                                     & "AND MR3.PTN_ID = '01'                      " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                     " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "	  WHERE INL.NRS_BR_CD  = @NRS_BR_CD                  	" & vbNewLine _
                                     & "	   AND INL.INKA_NO_L   = @INKA_NO_L                  	" & vbNewLine _
                                     & "	   AND INL.SYS_DEL_FLG = '0'                         	" & vbNewLine


    ''' <summary>
    ''' ORDER BY（①入荷日、②管理番号）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                    " & vbNewLine _
                                            & "	     BASE2.NRS_BR_CD                                	" & vbNewLine _
                                            & "	    ,BASE2.INKA_NO_L                                	" & vbNewLine _
                                            & "	    ,BASE2.PRINT_SORT                               	" & vbNewLine _
                                            & "	    ,BASE2.INKA_NO_M                                	" & vbNewLine _
                                            & "	    ,BASE2.INKA_NO_S	                                " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMB500IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB500DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMB500DAC.SQL_FROM_Mprt)        'SQL構築(帳票種別用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB500DAC", "SelectMPrt", cmd)

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
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB500IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB500DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB500DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMB500DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ''パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB500DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        'map.Add("INKA_NO_SS", "INKA_NO_SS")
        map.Add("USER_NM", "USER_NM")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("INKA_TTL_NB", "INKA_TTL_NB")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("INKA_PLAN_QT", "INKA_PLAN_QT")
        map.Add("INKA_PLAN_QT_UT", "INKA_PLAN_QT_UT")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UKETSUKE_USER", "UKETSUKE_USER")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("DEST_NM_L", "DEST_NM_L")
        map.Add("REMARK", "REMARK")
        map.Add("SAGYO_CD_L01", "SAGYO_CD_L01")
        map.Add("SAGYO_NM_L01", "SAGYO_NM_L01")
        map.Add("SAGYO_CD_L02", "SAGYO_CD_L02")
        map.Add("SAGYO_NM_L02", "SAGYO_NM_L02")
        map.Add("SAGYO_CD_L03", "SAGYO_CD_L03")
        map.Add("SAGYO_NM_L03", "SAGYO_NM_L03")
        map.Add("SAGYO_CD_L04", "SAGYO_CD_L04")
        map.Add("SAGYO_NM_L04", "SAGYO_NM_L04")
        map.Add("SAGYO_CD_L05", "SAGYO_CD_L05")
        map.Add("SAGYO_NM_L05", "SAGYO_NM_L05")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("KOSU", "KOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("SURYO", "SURYO")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("SAGYO_CD_M01", "SAGYO_CD_M01")
        map.Add("SAGYO_NM_M01", "SAGYO_NM_M01")
        map.Add("SAGYO_CD_M02", "SAGYO_CD_M02")
        map.Add("SAGYO_NM_M02", "SAGYO_NM_M02")
        map.Add("SAGYO_CD_M03", "SAGYO_CD_M03")
        map.Add("SAGYO_NM_M03", "SAGYO_NM_M03")
        map.Add("SAGYO_CD_M04", "SAGYO_CD_M04")
        map.Add("SAGYO_NM_M04", "SAGYO_NM_M04")
        map.Add("SAGYO_CD_M05", "SAGYO_CD_M05")
        map.Add("SAGYO_NM_M05", "SAGYO_NM_M05")
        map.Add("SHOBO_CD", "SHOBO_CD")             'ADD 2019/04/02 依頼番号 : 005131   【LMS】群馬入庫予定表_危険品区分を表示する
        map.Add("RUI", "RUI")                       'ADD 2019/04/02 依頼番号 : 005131   【LMS】群馬入庫予定表_危険品区分を表示する
        map.Add("HINMEI", "HINMEI")                 'ADD 2019/04/02 依頼番号 : 005131   【LMS】群馬入庫予定表_危険品区分を表示する


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB500OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
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
            'Me._StrSql.Append(" AND INL.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("INKA_NO_L").ToString()
            'Me._StrSql.Append(" AND INL.INKA_NO_L = @INKA_NO_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))

            '印刷ユーザー
            whereStr = .Item("USER_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))

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
