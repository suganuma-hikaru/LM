' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : 商品
'  プログラムID     :  LMM500    : 商品マスタ・請求関係印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                           " & vbNewLine _
                                            & "	MG.NRS_BR_CD                                           AS NRS_BR_CD    " & vbNewLine _
                                            & ",'66'                                                     AS PTN_ID       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                         " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                        " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID       " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                 " & vbNewLine _
                                            & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                        " & vbNewLine _
                                            & "WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                              " & vbNewLine _
                                            & "ELSE MR3.RPT_ID                                                                          " & vbNewLine _
                                            & "END              AS RPT_ID                                                               " & vbNewLine _
                                            & ",MG.SYS_DEL_FLG              AS SYS_DEL_FLG                                              " & vbNewLine _
                                            & ",KBN1.KBN_NM1                AS SYS_DEL_FLG_NM                                           " & vbNewLine _
                                            & ",MG.CUST_CD_L                AS CUST_CD_L                                                " & vbNewLine _
                                            & ",MG.CUST_CD_M                AS CUST_CD_M                                                " & vbNewLine _
                                            & ",MG.CUST_CD_S                AS CUST_CD_S                                                " & vbNewLine _
                                            & ",MG.CUST_CD_SS               AS CUST_CD_SS                                               " & vbNewLine _
                                            & ",MG.CUST_CD_L + '-' + MG.CUST_CD_M + '-' + MG.CUST_CD_S + '-' + MG.CUST_CD_SS AS  CUST_CD    " & vbNewLine _
                                            & ",MC.CUST_NM_L                AS CUST_NM_L                                                " & vbNewLine _
                                            & ",MC.CUST_NM_M                AS CUST_NM_M                                                " & vbNewLine _
                                            & ",MC.CUST_NM_S                AS CUST_NM_S                                                " & vbNewLine _
                                            & ",MC.CUST_NM_SS               AS CUST_NM_SS                                               " & vbNewLine _
                                            & ",MG.NRS_BR_CD                AS NRS_BR_CD                                                " & vbNewLine _
                                            & ",NRSBR.NRS_BR_NM             AS NRS_BR_NM                                                " & vbNewLine _
                                            & ",NRSBR.AD_1                  AS AD_1                                                     " & vbNewLine _
                                            & ",NRSBR.AD_2                  AS AD_2                                                     " & vbNewLine _
                                            & ",NRSBR.TEL                   AS TEL                                                      " & vbNewLine _
                                            & ",MG.GOODS_CD_CUST            AS GOODS_CD_CUST                                            " & vbNewLine _
                                            & ",MG.GOODS_NM_1               AS GOODS_NM_1                                               " & vbNewLine _
                                            & ",MG.NB_UT                    AS NB_UT                                                    " & vbNewLine _
                                            & ",KBN2.KBN_NM1                AS NB_UT_NM                                                 " & vbNewLine _
                                            & ",MG.STD_IRIME_NB             AS STD_IRIME_NB                                             " & vbNewLine _
                                            & ",MG.STD_IRIME_UT             AS STD_IRIME_UT                                             " & vbNewLine _
                                            & ",KBN3.KBN_NM1                 AS STD_IRIME_CD                                             " & vbNewLine _
                                            & ",MG.ONDO_KB                  AS ONDO_KB                                                  " & vbNewLine _
                                            & ",KBN4.KBN_NM1                AS ONDO_KB_NM                                               " & vbNewLine _
                                            & ",MG.ONDO_MX                  AS ONDO_MX                                                  " & vbNewLine _
                                            & ",MG.ONDO_MM                  AS ONDO_MM                                                  " & vbNewLine _
                                            & ",MG.PKG_NB                   AS PKG_NB                                                   " & vbNewLine _
                                            & ",MG.SHOBO_CD                 AS SHOBO_CD                                                 " & vbNewLine _
                                            & ",MSHOBO.RUI                  AS RUI                                                      " & vbNewLine _
                                            & ",KBN5.KBN_NM1                AS RUI_NM                                                   " & vbNewLine _
                                            & ",MSHOBO.HINMEI               AS HINMEI                                                   " & vbNewLine _
                                            & ",MSHOBO.SEISITSU             AS SEISITSU                                                 " & vbNewLine _
                                            & ",MSHOBO.SYU                  AS SYU                                                      " & vbNewLine _
                                            & ",KBN6.KBN_NM1                AS SYU_NM                                                   " & vbNewLine _
                                            & ",MG.ONDO_STR_DATE            AS ONDO_STR_DATE                                            " & vbNewLine _
                                            & ",MG.ONDO_END_DATE            AS ONDO_END_DATE                                            " & vbNewLine _
                                            & ",MG.UP_GP_CD_1               AS UP_GP_CD_1                                               " & vbNewLine _
                                            & ",BASE.STR_DATE               AS STR_DATE                                                 " & vbNewLine _
                                            & ",MTANKA.STORAGE_1            AS STORAGE_1                                                " & vbNewLine _
                                            & ",MTANKA.STORAGE_KB1          AS STORAGE_KB1                                              " & vbNewLine _
                                            & ",KBN7.KBN_NM1                AS STORAGE_KB1_NM                                           " & vbNewLine _
                                            & ",MTANKA.STORAGE_2            AS STORAGE_2                                                " & vbNewLine _
                                            & ",MTANKA.STORAGE_KB2          AS STORAGE_KB2                                              " & vbNewLine _
                                            & ",KBN8.KBN_NM1                AS STORAGE_KB2_NM                                           " & vbNewLine _
                                            & ",MTANKA.HANDLING_IN          AS HANDLING_IN                                              " & vbNewLine _
                                            & ",MTANKA.HANDLING_IN_KB       AS HANDLING_IN_KB                                           " & vbNewLine _
                                            & ",KBN9.KBN_NM1                AS HANDLING_IN_KB_NM                                        " & vbNewLine _
                                            & ",MTANKA.HANDLING_OUT         AS HANDLING_OUT                                             " & vbNewLine _
                                            & ",MTANKA.HANDLING_OUT_KB      AS HANDLING_OUT_KB                                          " & vbNewLine _
                                            & ",KBN10.KBN_NM1               AS HANDLING_OUT_KB_NM                                       " & vbNewLine _
                                            & ",MTANKA.MINI_TEKI_IN_AMO     AS MINI_TEKI_IN_AMO                                         " & vbNewLine _
                                            & ",MTANKA.MINI_TEKI_OUT_AMO    AS MINI_TEKI_OUT_AMO                                        " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                                                     " & vbNewLine _
                                          & "--商品M                                                                                  " & vbNewLine _
                                          & "          $LM_MST$..M_GOODS AS MG                                                          " & vbNewLine _
                                          & "--削除フラグ名                                                                           " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN1                                                          " & vbNewLine _
                                          & "ON  MG.SYS_DEL_FLG = KBN1.KBN_CD                                                         " & vbNewLine _
                                          & "AND KBN1.KBN_GROUP_CD = 'S051'                                                           " & vbNewLine _
                                          & "AND KBN1.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--荷主名                                                                                 " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST AS MC                                                           " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = MC.NRS_BR_CD                                                          " & vbNewLine _
                                          & "AND MG.CUST_CD_L = MC.CUST_CD_L                                                          " & vbNewLine _
                                          & "AND MG.CUST_CD_M = MC.CUST_CD_M                                                          " & vbNewLine _
                                          & "AND MG.CUST_CD_S = MC.CUST_CD_S                                                          " & vbNewLine _
                                          & "AND MG.CUST_CD_SS = MC.CUST_CD_SS                                                        " & vbNewLine _
                                          & "AND MC.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                          & "--営業所名                                                                               " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_NRS_BR AS NRSBR                                                      " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = NRSBR.NRS_BR_CD                                                       " & vbNewLine _
                                          & "--個数単位名                                                                             " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN2                                                          " & vbNewLine _
                                          & "ON MG.NB_UT = KBN2.KBN_CD                                                                " & vbNewLine _
                                          & "AND KBN2.KBN_GROUP_CD = 'K002'                                                           " & vbNewLine _
                                          & "AND KBN2.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--入目単位コード名                                                                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN3                                                          " & vbNewLine _
                                          & "ON  MG.STD_IRIME_UT = KBN3.KBN_CD                                                        " & vbNewLine _
                                          & "AND KBN3.KBN_GROUP_CD = 'I001'                                                           " & vbNewLine _
                                          & "AND KBN3.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--温度管理区分名                                                                         " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN4                                                          " & vbNewLine _
                                          & "ON  MG.ONDO_KB = KBN4.KBN_CD                                                             " & vbNewLine _
                                          & "AND KBN4.KBN_GROUP_CD = 'O002'                                                           " & vbNewLine _
                                          & "AND KBN4.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--類別区分                                                                               " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_SHOBO AS MSHOBO                                                      " & vbNewLine _
                                          & "ON  MG.SHOBO_CD = MSHOBO.SHOBO_CD                                                        " & vbNewLine _
                                          & "AND MSHOBO.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                          & "--類別区分名                                                                             " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN5                                                          " & vbNewLine _
                                          & "ON  MSHOBO.RUI = KBN5.KBN_CD                                                             " & vbNewLine _
                                          & "AND KBN5.KBN_GROUP_CD = 'S004'                                                           " & vbNewLine _
                                          & "AND KBN5.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--危険種別名                                                                             " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN6                                                          " & vbNewLine _
                                          & "ON  MSHOBO.SYU = KBN6.KBN_CD                                                             " & vbNewLine _
                                          & "AND KBN6.KBN_GROUP_CD = 'S022'                                                           " & vbNewLine _
                                          & "AND KBN6.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--適応開始日                                                                             " & vbNewLine _
                                          & "LEFT JOIN                                                                                " & vbNewLine _
                                          & "(SELECT * FROM                                                                           " & vbNewLine _
                                          & "   (SELECT                                                                                 " & vbNewLine _
                                          & "   DAYTBL.NRS_BR_CD                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.CUST_CD_L                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.CUST_CD_M                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.UP_GP_CD_1 AS UP_GP_CD_1                                                         " & vbNewLine _
                                          & "   ,DAYTBL.STR_DATE AS STR_DATE                                                             " & vbNewLine _
                                          & "   ,DAYTBL.SYS_DEL_FLG                                                                      " & vbNewLine _
                                          & "   ,ROW_NUMBER() OVER (PARTITION BY NRS_BR_CD,CUST_CD_L,CUST_CD_M,UP_GP_CD_1                " & vbNewLine _
                                          & "    ORDER BY STR_DATE DESC) AS NUM                                                          " & vbNewLine _
                                          & "   FROM $LM_MST$..M_TANKA AS DAYTBL                                                         " & vbNewLine _
                                          & "   WHERE                                                                                    " & vbNewLine _
                                          & "       DAYTBL.STR_DATE <= @SYS_DATE                                                             " & vbNewLine _
                                          & "   AND DAYTBL.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
                                          & "   GROUP BY                                                                                 " & vbNewLine _
                                          & "   DAYTBL.NRS_BR_CD                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.CUST_CD_L                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.CUST_CD_M                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.UP_GP_CD_1                                                                        " & vbNewLine _
                                          & "   ,DAYTBL.STR_DATE                                                                         " & vbNewLine _
                                          & "   ,DAYTBL.SYS_DEL_FLG                                                                      " & vbNewLine _
                                          & "   )AS DT                                                                                  " & vbNewLine _
                                          & " WHERE DT.NUM = 1                                                                        " & vbNewLine _
                                          & " )AS BASE                                                                        " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = BASE.NRS_BR_CD                                                          " & vbNewLine _
                                          & "AND MG.CUST_CD_L = BASE.CUST_CD_L                                                          " & vbNewLine _
                                          & "AND MG.CUST_CD_M = BASE.CUST_CD_M                                                          " & vbNewLine _
                                          & "AND MG.UP_GP_CD_1 = BASE.UP_GP_CD_1                                                        " & vbNewLine _
                                          & "--保管料(通常)                                                                           " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_TANKA AS MTANKA                                                      " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = MTANKA.NRS_BR_CD                                                      " & vbNewLine _
                                          & "AND MG.CUST_CD_L = MTANKA.CUST_CD_L                                                      " & vbNewLine _
                                          & "AND MG.CUST_CD_M = MTANKA.CUST_CD_M                                                      " & vbNewLine _
                                          & "AND MG.UP_GP_CD_1 = MTANKA.UP_GP_CD_1                                                    " & vbNewLine _
                                          & "AND MTANKA.STR_DATE = BASE.STR_DATE                                                        " & vbNewLine _
                                          & "--保管料建区分名(温度管理なし)                                                           " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN7                                                          " & vbNewLine _
                                          & "ON  MTANKA.STORAGE_KB1 = KBN7.KBN_CD                                                     " & vbNewLine _
                                          & "AND KBN7.KBN_GROUP_CD = 'T005'                                                           " & vbNewLine _
                                          & "AND KBN7.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--保管料建区分名(温度管理あり)                                                           " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN8                                                          " & vbNewLine _
                                          & "ON  MTANKA.STORAGE_KB2 = KBN8.KBN_CD                                                     " & vbNewLine _
                                          & "AND KBN8.KBN_GROUP_CD = 'T005'                                                           " & vbNewLine _
                                          & "AND KBN8.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--荷役料建(入庫)区分名                                                                   " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN9                                                          " & vbNewLine _
                                          & "ON  MTANKA.HANDLING_IN_KB = KBN9.KBN_CD                                                  " & vbNewLine _
                                          & "AND KBN9.KBN_GROUP_CD = 'T005'                                                           " & vbNewLine _
                                          & "AND KBN9.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                          & "--荷役料建(出庫)区分名                                                                   " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..Z_KBN AS KBN10                                                         " & vbNewLine _
                                          & "ON  MTANKA.HANDLING_OUT_KB = KBN10.KBN_CD                                                " & vbNewLine _
                                          & "AND KBN10.KBN_GROUP_CD = 'T005'                                                          " & vbNewLine _
                                          & "AND KBN10.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                          & "--出荷Lでの荷主帳票パターン取得                                                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                        " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = MCR1.NRS_BR_CD                                                        " & vbNewLine _
                                          & "AND MG.CUST_CD_L = MCR1.CUST_CD_L                                                        " & vbNewLine _
                                          & "AND MG.CUST_CD_M = MCR1.CUST_CD_M                                                        " & vbNewLine _
                                          & "AND '00' = MCR1.CUST_CD_S                                                                " & vbNewLine _
                                          & "AND MCR1.PTN_ID = '66'                                                                   " & vbNewLine _
                                          & "--帳票パターン取得                                                                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR1                                                              " & vbNewLine _
                                          & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                       " & vbNewLine _
                                          & "AND MR1.PTN_ID = MCR1.PTN_ID                                                             " & vbNewLine _
                                          & "AND MR1.PTN_CD = MCR1.PTN_CD                                                             " & vbNewLine _
                                          & "--商品Mの荷主での荷主帳票パターン取得                                                    " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                        " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                                        " & vbNewLine _
                                          & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                                        " & vbNewLine _
                                          & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                                        " & vbNewLine _
                                          & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                                        " & vbNewLine _
                                          & "AND MCR2.PTN_ID = '66'                                                                   " & vbNewLine _
                                          & "--帳票パターン取得                                                                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR2                                                              " & vbNewLine _
                                          & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                       " & vbNewLine _
                                          & "AND MR2.PTN_ID = MCR2.PTN_ID                                                             " & vbNewLine _
                                          & "AND MR2.PTN_CD = MCR2.PTN_CD                                                             " & vbNewLine _
                                          & "--存在しない場合の帳票パターン取得                                                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR3                                                              " & vbNewLine _
                                          & "ON  MR3.NRS_BR_CD = MG.NRS_BR_CD                                                         " & vbNewLine _
                                          & "AND MR3.PTN_ID = '66'                                                                    " & vbNewLine _
                                          & "AND MR3.STANDARD_FLAG = '01'                                                             " & vbNewLine _
                                          & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine




    ''' <summary>
    ''' GROUP BY（商品コード）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                             " & vbNewLine _
                                          & "CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
                                          & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
                                          & "      ELSE MR3.RPT_ID                              " & vbNewLine _
                                          & " END                                               " & vbNewLine _
                                          & ",MG.SYS_DEL_FLG                                    " & vbNewLine _
                                          & ",KBN1.KBN_NM1                                      " & vbNewLine _
                                          & ",MG.CUST_CD_L                                      " & vbNewLine _
                                          & ",MG.CUST_CD_M                                      " & vbNewLine _
                                          & ",MG.CUST_CD_S                                      " & vbNewLine _
                                          & ",MG.CUST_CD_SS                                     " & vbNewLine _
                                          & ",MC.CUST_NM_L                                      " & vbNewLine _
                                          & ",MC.CUST_NM_M                                      " & vbNewLine _
                                          & ",MC.CUST_NM_S                                      " & vbNewLine _
                                          & ",MC.CUST_NM_SS                                     " & vbNewLine _
                                          & ",MG.NRS_BR_CD                                      " & vbNewLine _
                                          & ",NRSBR.NRS_BR_NM                                   " & vbNewLine _
                                          & ",NRSBR.AD_1                                        " & vbNewLine _
                                          & ",NRSBR.AD_2                                        " & vbNewLine _
                                          & ",NRSBR.TEL                                         " & vbNewLine _
                                          & ",MG.GOODS_CD_CUST                                  " & vbNewLine _
                                          & ",MG.GOODS_NM_1                                     " & vbNewLine _
                                          & ",MG.NB_UT                                          " & vbNewLine _
                                          & ",KBN2.KBN_NM1                                      " & vbNewLine _
                                          & ",MG.STD_IRIME_NB                                   " & vbNewLine _
                                          & ",MG.STD_IRIME_UT                                   " & vbNewLine _
                                          & ",KBN3.KBN_NM1                                       " & vbNewLine _
                                          & ",MG.ONDO_KB                                        " & vbNewLine _
                                          & ",KBN4.KBN_NM1                                      " & vbNewLine _
                                          & ",MG.ONDO_MX                                        " & vbNewLine _
                                          & ",MG.ONDO_MM                                        " & vbNewLine _
                                          & ",MG.PKG_NB                                         " & vbNewLine _
                                          & ",MG.SHOBO_CD                                       " & vbNewLine _
                                          & ",MSHOBO.RUI                                        " & vbNewLine _
                                          & ",KBN5.KBN_NM1                                      " & vbNewLine _
                                          & ",MSHOBO.HINMEI                                     " & vbNewLine _
                                          & ",MSHOBO.SEISITSU                                   " & vbNewLine _
                                          & ",MSHOBO.SYU                                        " & vbNewLine _
                                          & ",KBN6.KBN_NM1                                      " & vbNewLine _
                                          & ",MG.ONDO_STR_DATE                                  " & vbNewLine _
                                          & ",MG.ONDO_END_DATE                                  " & vbNewLine _
                                          & ",MG.UP_GP_CD_1                                     " & vbNewLine _
                                          & ",MTANKA.STORAGE_1                                  " & vbNewLine _
                                          & ",MTANKA.STORAGE_KB1                                " & vbNewLine _
                                          & ",KBN7.KBN_NM1                                      " & vbNewLine _
                                          & ",MTANKA.STORAGE_2                                  " & vbNewLine _
                                          & ",MTANKA.STORAGE_KB2                                " & vbNewLine _
                                          & ",KBN8.KBN_NM1                                      " & vbNewLine _
                                          & ",MTANKA.HANDLING_IN                                " & vbNewLine _
                                          & ",MTANKA.HANDLING_IN_KB                             " & vbNewLine _
                                          & ",KBN9.KBN_NM1                                      " & vbNewLine _
                                          & ",MTANKA.HANDLING_OUT                               " & vbNewLine _
                                          & ",MTANKA.HANDLING_OUT_KB                            " & vbNewLine _
                                          & ",KBN10.KBN_NM1                                     " & vbNewLine _
                                          & ",MTANKA.MINI_TEKI_IN_AMO                           " & vbNewLine _
                                          & ",MTANKA.MINI_TEKI_OUT_AMO                          " & vbNewLine _
                                          & ",BASE.STR_DATE                                     " & vbNewLine

    ''' <summary>
    ''' ORDER BY（荷主商品コード）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                    " & vbNewLine _
                                         & " MG.CUST_CD_L                         " & vbNewLine _
                                         & ",MG.CUST_CD_M                         " & vbNewLine _
                                         & ",MG.CUST_CD_S                         " & vbNewLine _
                                         & ",MG.CUST_CD_SS                        " & vbNewLine _
                                         & ",MG.SYS_DEL_FLG                       " & vbNewLine _
                                         & ",MG.GOODS_CD_CUST                     " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMM500IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM500DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMM500DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSearch()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM500DAC", "SelectMPrt", cmd)

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
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM500IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM500DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM500DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMM500DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用Group By句)
        Me._StrSql.Append(LMM500DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSearch()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM500DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_FLG_NM", "SYS_DEL_FLG_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("TEL", "TEL")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("NB_UT", "NB_UT")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_IRIME_CD", "STD_IRIME_CD")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_KB_NM", "ONDO_KB_NM")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("RUI", "RUI")
        map.Add("RUI_NM", "RUI_NM")
        map.Add("HINMEI", "HINMEI")
        map.Add("SEISITSU", "SEISITSU")
        map.Add("SYU", "SYU")
        map.Add("SYU_NM", "SYU_NM")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("STORAGE_1", "STORAGE_1")
        map.Add("STORAGE_KB1", "STORAGE_KB1")
        map.Add("STORAGE_KB1_NM", "STORAGE_KB1_NM")
        map.Add("STORAGE_2", "STORAGE_2")
        map.Add("STORAGE_KB2", "STORAGE_KB2")
        map.Add("STORAGE_KB2_NM", "STORAGE_KB2_NM")
        map.Add("HANDLING_IN", "HANDLING_IN")
        map.Add("HANDLING_IN_KB", "HANDLING_IN_KB")
        map.Add("HANDLING_IN_KB_NM", "HANDLING_IN_KB_NM")
        map.Add("HANDLING_OUT", "HANDLING_OUT")
        map.Add("HANDLING_OUT_KB", "HANDLING_OUT_KB")
        map.Add("HANDLING_OUT_KB_NM", "HANDLING_OUT_KB_NM")
        map.Add("MINI_TEKI_IN_AMO", "MINI_TEKI_IN_AMO")
        map.Add("MINI_TEKI_OUT_AMO", "MINI_TEKI_OUT_AMO")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM500OUT")

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
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MG.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If
           

            '【荷主コード(大)：LIKE 値%】
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (MG.CUST_CD_L LIKE @CUST_CD_L)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(中)：LIKE 値%】
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  MG.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(小)：LIKE 値%】
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  MG.CUST_CD_S LIKE @CUST_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主コード(極小)：LIKE 値%】
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  MG.CUST_CD_SS LIKE @CUST_CD_SS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If



            '荷主商品コード
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MG.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                andstr.Append(vbNewLine)
                '(2012.04.06) Notes№886 GOODS_CD_CUSTのDBDataTypeをCHAR→NVARCHARに変更 -- STRAT --
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                '(2012.04.06) Notes№886 GOODS_CD_CUSTのDBDataTypeをCHAR→NVARCHARに変更 --  END  --
            End If

            '商品名
            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" MG.GOODS_NM_1 LIKE @GOODS_NM_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '温度管理区分
            whereStr = .Item("ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (MG.ONDO_KB = @ONDO_KB)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", whereStr, DBDataType.CHAR))
            End If

            '削除フラグ
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (MG.SYS_DEL_FLG = @SYS_DEL_FLG)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
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

#Region "パラメータの設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSearch()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

    End Sub

#End Region
#End Region

End Class
