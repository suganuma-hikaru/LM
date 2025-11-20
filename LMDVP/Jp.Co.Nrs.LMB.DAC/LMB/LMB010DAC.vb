' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB010    : 入荷データ検索
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMB010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(B_INKA_L.INKA_NO_L)		            AS SELECT_CNT       " & vbNewLine

    'START YANAI メモ②No.28
    '''' <summary>
    '''' B_INKA_Lデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                     " & vbNewLine _
    '                                        & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
    '                                        & ",Z1.KBN_NM1                                          AS INKA_STATE_KB_NM    " & vbNewLine _
    '                                        & ",B_INKA_L.INKA_DATE                                  AS INKA_DATE	       " & vbNewLine _
    '                                        & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
    '                                        & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM	           " & vbNewLine _
    '                                        & ",B_INKA_M.INKA_TTL_NB                                AS INKA_TTL_NB	       " & vbNewLine _
    '                                        & ",B_INKA_M.WT                                         AS WT                  " & vbNewLine _
    '                                        & ",B_INKA_L.REMARK                                     AS REMARK	           " & vbNewLine _
    '                                        & ",B_INKA_M.REC_CNT                                    AS REC_CNT	           " & vbNewLine _
    '                                        & ",Z2.KBN_NM1                                          AS UNCHIN_NM           " & vbNewLine _
    '                                        & ",DEST_NM                                             AS DEST_NM  	       " & vbNewLine _
    '                                        & ",B_INKA_L.INKA_NO_L                                  AS INKA_NO_L	       " & vbNewLine _
    '                                        & ",M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM  AS UNSOCO_NM	       " & vbNewLine _
    '                                        & ",B_INKA_L.BUYER_ORD_NO_L                             AS BUYER_ORD_NO_L      " & vbNewLine _
    '                                        & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
    '                                        & ",Z4.KBN_NM1                                          AS INKA_KB_NM	       " & vbNewLine _
    '                                        & ",M_SOKO.WH_NM                                        AS WH_NM	           " & vbNewLine _
    '                                        & ",B_INKA_L.NRS_BR_CD                                  AS NRS_BR_CD	       " & vbNewLine _
    '                                        & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	       " & vbNewLine _
    '                                        & ",TANTO_USER.USER_NM                                  AS TANTO_USER	       " & vbNewLine _
    '                                        & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER        " & vbNewLine _
    '                                        & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
    '                                        & ",B_INKA_L.SYS_UPD_DATE                               AS SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",B_INKA_L.SYS_UPD_TIME                               AS SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
    '                                        & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
    '                                        & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine 
    'START YANAI 要望番号748
    '''' <summary>
    '''' B_INKA_Lデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                     " & vbNewLine _
    '                                            & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS INKA_STATE_KB_NM    " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_DATE                                  AS INKA_DATE	       " & vbNewLine _
    '                                            & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
    '                                            & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM	           " & vbNewLine _
    '                                            & ",B_INKA_M.INKA_TTL_NB                                AS INKA_TTL_NB	       " & vbNewLine _
    '                                            & ",B_INKA_M.WT                                         AS WT                  " & vbNewLine _
    '                                            & ",B_INKA_L.REMARK                                     AS REMARK	           " & vbNewLine _
    '                                            & ",B_INKA_M.REC_CNT                                    AS REC_CNT	           " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS UNCHIN_NM           " & vbNewLine _
    '                                            & ",DEST_NM                                             AS DEST_NM  	       " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_NO_L                                  AS INKA_NO_L	       " & vbNewLine _
    '                                            & ",M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM  AS UNSOCO_NM	       " & vbNewLine _
    '                                            & ",B_INKA_L.BUYER_ORD_NO_L                             AS BUYER_ORD_NO_L      " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
    '                                            & ",Z4.KBN_NM1                                          AS INKA_KB_NM	       " & vbNewLine _
    '                                            & ",M_SOKO.WH_NM                                        AS WH_NM	           " & vbNewLine _
    '                                            & ",B_INKA_L.NRS_BR_CD                                  AS NRS_BR_CD	       " & vbNewLine _
    '                                            & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	       " & vbNewLine _
    '                                            & ",TANTO_USER.USER_NM                                  AS TANTO_USER	       " & vbNewLine _
    '                                            & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER        " & vbNewLine _
    '                                            & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_DATE                               AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_TIME                               AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine _
    '                                            & ",B_INKA_L.WH_CD                                      AS WH_CD               " & vbNewLine _
    '                                            & ",B_INKA_M.OUTKA_FROM_ORD_NO_M                        AS OUTKA_FROM_ORD_NO_M " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_TP                                    AS INKA_TP_CD          " & vbNewLine _
    'START YANAI 要望番号882
    '''' <summary>
    '''' B_INKA_Lデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                     " & vbNewLine _
    '                                            & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS INKA_STATE_KB_NM    " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_DATE                                  AS INKA_DATE	       " & vbNewLine _
    '                                            & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
    '                                            & ",M_GOODS.CUST_CD_S                                   AS CUST_CD_S           " & vbNewLine _
    '                                            & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM	           " & vbNewLine _
    '                                            & ",B_INKA_M.INKA_TTL_NB                                AS INKA_TTL_NB	       " & vbNewLine _
    '                                            & ",B_INKA_M.WT                                         AS WT                  " & vbNewLine _
    '                                            & ",B_INKA_L.REMARK                                     AS REMARK	           " & vbNewLine _
    '                                            & ",B_INKA_M.REC_CNT                                    AS REC_CNT	           " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS UNCHIN_NM           " & vbNewLine _
    '                                            & ",DEST_NM                                             AS DEST_NM  	       " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_NO_L                                  AS INKA_NO_L	       " & vbNewLine _
    '                                            & ",M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM  AS UNSOCO_NM	       " & vbNewLine _
    '                                            & ",B_INKA_L.BUYER_ORD_NO_L                             AS BUYER_ORD_NO_L      " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
    '                                            & ",Z4.KBN_NM1                                          AS INKA_KB_NM	       " & vbNewLine _
    '                                            & ",M_SOKO.WH_NM                                        AS WH_NM	           " & vbNewLine _
    '                                            & ",B_INKA_L.NRS_BR_CD                                  AS NRS_BR_CD	       " & vbNewLine _
    '                                            & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	       " & vbNewLine _
    '                                            & ",TANTO_USER.USER_NM                                  AS TANTO_USER	       " & vbNewLine _
    '                                            & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER        " & vbNewLine _
    '                                            & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_DATE                               AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_TIME                               AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine _
    '                                            & ",B_INKA_L.WH_CD                                      AS WH_CD               " & vbNewLine _
    '                                            & ",B_INKA_M.OUTKA_FROM_ORD_NO_M                        AS OUTKA_FROM_ORD_NO_M " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_TP                                    AS INKA_TP_CD          " & vbNewLine _
    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    '''' <summary>
    '''' B_INKA_Lデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
    '                                            & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS INKA_STATE_KB_NM    " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_DATE                                  AS INKA_DATE	       " & vbNewLine _
    '                                            & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
    '                                            & ",M_GOODS.CUST_CD_S                                   AS CUST_CD_S           " & vbNewLine _
    '                                            & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM	           " & vbNewLine _
    '                                            & ",INKAL2.INKA_TTL_NB                                  AS INKA_TTL_NB	       " & vbNewLine _
    '                                            & ",INKAL2.WT                                           AS WT                  " & vbNewLine _
    '                                            & ",B_INKA_L.REMARK                                     AS REMARK	           " & vbNewLine _
    '                                            & ",B_INKA_M.REC_CNT                                    AS REC_CNT	           " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS UNCHIN_NM           " & vbNewLine _
    '                                            & ",DEST_NM                                             AS DEST_NM  	       " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_NO_L                                  AS INKA_NO_L	       " & vbNewLine _
    '                                            & ",M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM  AS UNSOCO_NM	       " & vbNewLine _
    '                                            & ",B_INKA_L.BUYER_ORD_NO_L                             AS BUYER_ORD_NO_L      " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
    '                                            & ",Z4.KBN_NM1                                          AS INKA_KB_NM	       " & vbNewLine _
    '                                            & ",M_SOKO.WH_NM                                        AS WH_NM	           " & vbNewLine _
    '                                            & ",B_INKA_L.NRS_BR_CD                                  AS NRS_BR_CD	       " & vbNewLine _
    '                                            & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	       " & vbNewLine _
    '                                            & ",TANTO_USER.USER_NM                                  AS TANTO_USER	       " & vbNewLine _
    '                                            & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER        " & vbNewLine _
    '                                            & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_DATE                               AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_TIME                               AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine _
    '                                            & ",B_INKA_L.WH_CD                                      AS WH_CD               " & vbNewLine _
    '                                            & ",B_INKA_M.OUTKA_FROM_ORD_NO_M                        AS OUTKA_FROM_ORD_NO_M " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_TP                                    AS INKA_TP_CD          " & vbNewLine _
    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
    '''' <summary>
    '''' B_INKA_Lデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
    '                                            & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
    '                                            & ",Z1.KBN_NM1                                          AS INKA_STATE_KB_NM    " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_DATE                                  AS INKA_DATE	       " & vbNewLine _
    '                                            & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
    '                                            & ",M_GOODS.CUST_CD_S                                   AS CUST_CD_S           " & vbNewLine _
    '                                            & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM	           " & vbNewLine _
    '                                            & ",INKAL2.INKA_TTL_NB                                  AS INKA_TTL_NB	       " & vbNewLine _
    '                                            & ",INKAL2.WT                                           AS WT                  " & vbNewLine _
    '                                            & ",B_INKA_L.REMARK                                     AS REMARK	           " & vbNewLine _
    '                                            & ",B_INKA_M.REC_CNT                                    AS REC_CNT	           " & vbNewLine _
    '                                            & ",Z2.KBN_NM1                                          AS UNCHIN_NM           " & vbNewLine _
    '                                            & ",DEST_NM                                             AS DEST_NM  	       " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_NO_L                                  AS INKA_NO_L	       " & vbNewLine _
    '                                            & ",M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM  AS UNSOCO_NM	       " & vbNewLine _
    '                                            & ",B_INKA_L.BUYER_ORD_NO_L                             AS BUYER_ORD_NO_L      " & vbNewLine _
    '                                            & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
    '                                            & ",Z4.KBN_NM1                                          AS INKA_KB_NM	       " & vbNewLine _
    '                                            & ",M_SOKO.WH_NM                                        AS WH_NM	           " & vbNewLine _
    '                                            & ",B_INKA_L.NRS_BR_CD                                  AS NRS_BR_CD	       " & vbNewLine _
    '                                            & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	       " & vbNewLine _
    '                                            & ",TANTO_USER.USER_NM                                  AS TANTO_USER	       " & vbNewLine _
    '                                            & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER        " & vbNewLine _
    '                                            & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_DATE                               AS SYS_UPD_DATE        " & vbNewLine _
    '                                            & ",B_INKA_L.SYS_UPD_TIME                               AS SYS_UPD_TIME        " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
    '                                            & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine _
    '                                            & ",B_INKA_L.WH_CD                                      AS WH_CD               " & vbNewLine _
    '                                            & ",B_INKA_M.OUTKA_FROM_ORD_NO_M                        AS OUTKA_FROM_ORD_NO_M " & vbNewLine _
    '                                            & ",B_INKA_L.INKA_TP                                    AS INKA_TP_CD          " & vbNewLine _
    '                                            & ",ISNULL(SCNT.REC_CNT,0)                              AS REC_CNT_S           " & vbNewLine _
    '                                            & ",M_CUST.PIC                                          AS PIC                 " & vbNewLine _
    ''' <summary>
    ''' B_INKA_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
                                                & "--* UPD 2018/10/02 依頼番号 : 002498   【LMS】【既存バグ】入荷まとめ機能バグ対応     --2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本/ Annen upd start " & vbNewLine _
                                                & "--* CASE WHEN H_INKA_EDL_L.INKA_CTL_NO_L <> '' THEN                            " & vbNewLine _
                                                & "--*     CASE WHEN H_INKA_EDL_L.EDI_COUNT >= 2                                  " & vbNewLine _
                                                & "--*	           AND B_INKA_L.SYS_ENT_DATE = B_INKA_L.SYS_UPD_DATE               " & vbNewLine _
                                                & "--*			   AND B_INKA_L.SYS_ENT_TIME = B_INKA_L.SYS_UPD_TIME THEN          " & vbNewLine _
                                                & "--*    ''                                                                      " & vbNewLine _
                                                & "--*	 ELSE                                                                      " & vbNewLine _
                                                & "--*	     B_INKA_L.OUTKA_FROM_ORD_NO_L                                          " & vbNewLine _
                                                & "--*	  END                                                                      " & vbNewLine _
                                                & "--* ELSE                                                                       " & vbNewLine _
                                                & "--*     B_INKA_L.OUTKA_FROM_ORD_NO_L                                           " & vbNewLine _
                                                & "--* END                                                 AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
                                                & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
                                                & "--2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen upd end " & vbNewLine _
                                                & ",Z1.KBN_NM1                                          AS INKA_STATE_KB_NM    " & vbNewLine _
                                                & ",B_INKA_L.INKA_DATE                                  AS INKA_DATE	       " & vbNewLine _
                                                & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
                                                & ",M_GOODS.CUST_CD_S                                   AS CUST_CD_S           " & vbNewLine _
                                                & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM	           " & vbNewLine _
                                                & ",INKAL2.INKA_TTL_NB                                  AS INKA_TTL_NB	       " & vbNewLine _
                                                & ",INKAL2.WT                                           AS WT                  " & vbNewLine _
                                                & ",B_INKA_L.REMARK                                     AS REMARK	           " & vbNewLine _
                                                & ",B_INKA_M.REC_CNT                                    AS REC_CNT	           " & vbNewLine _
                                                & ",Z2.KBN_NM1                                          AS UNCHIN_NM           " & vbNewLine _
                                                & ",DEST_NM                                             AS DEST_NM  	       " & vbNewLine _
                                                & ",B_INKA_L.INKA_NO_L                                  AS INKA_NO_L	       " & vbNewLine _
                                                & ",M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM  AS UNSOCO_NM	       " & vbNewLine _
                                                & ",B_INKA_L.BUYER_ORD_NO_L                             AS BUYER_ORD_NO_L      " & vbNewLine _
                                                & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
                                                & ",Z4.KBN_NM1                                          AS INKA_KB_NM	       " & vbNewLine _
                                                & ",M_SOKO.WH_NM                                        AS WH_NM	           " & vbNewLine _
                                                & ",B_INKA_L.NRS_BR_CD                                  AS NRS_BR_CD	       " & vbNewLine _
                                                & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	       " & vbNewLine _
                                                & ",TANTO_USER.USER_NM                                  AS TANTO_USER	       " & vbNewLine _
                                                & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER        " & vbNewLine _
                                                & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
                                                & ",B_INKA_L.SYS_UPD_DATE                               AS SYS_UPD_DATE        " & vbNewLine _
                                                & ",B_INKA_L.SYS_UPD_TIME                               AS SYS_UPD_TIME        " & vbNewLine _
                                                & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
                                                & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
                                                & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine _
                                                & ",B_INKA_L.WH_CD                                      AS WH_CD               " & vbNewLine _
                                                & ",B_INKA_M.OUTKA_FROM_ORD_NO_M                        AS OUTKA_FROM_ORD_NO_M " & vbNewLine _
                                                & ",B_INKA_L.INKA_TP                                    AS INKA_TP_CD          " & vbNewLine _
                                                & ",ISNULL(SCNT.REC_CNT,0)                              AS REC_CNT_S           " & vbNewLine _
                                                & ",M_CUST.PIC                                          AS PIC                 " & vbNewLine _
                                                & ",INKAS2.LOT_NO                                       AS LOT_NO              " & vbNewLine _
                                                & ",INKAS2.SERIAL_NO                                    AS SERIAL_NO           " & vbNewLine _
                                                & ",ISNULL(WH_STATUS.KBN_NM1, '')                       AS WH_WORK_STATUS_NM   " & vbNewLine _
                                                & ",ISNULL(WEB_INKA_L.WEB_INKA_NO_L, '')                AS WEB_INKA_NO_L       " & vbNewLine _
                                                & ",ISNULL(WH_TAB_STATUS.KBN_NM1, '')                   AS WH_TAB_STATUS_NM    " & vbNewLine _
                                                & ",ISNULL(KBNT.KBN_NM1, '')                            AS WH_TAB_WORK_STATUS_NM " & vbNewLine _
                                                & ",ISNULL(TAB_KBN.WH_TAB_WORK_STATUS, '')              AS WH_TAB_WORK_STATUS_KB " & vbNewLine

    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    'END YANAI 要望番号882
    'END YANAI 要望番号748
    'END YANAI メモ②No.28


    'START YANAI メモ②No.28
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM As String = "FROM                                                                                  " & vbNewLine _
    '                                 & "$LM_TRN$..B_INKA_L                                                                    " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..Z_KBN                        Z1                                             " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                                                    " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "Z1.KBN_GROUP_CD = 'N004'                                                              " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..M_CUST                       M_CUST                                         " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "M_CUST.CUST_CD_S = '00'                                                               " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "M_CUST.CUST_CD_SS = '00'                                                              " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "(                                                                                     " & vbNewLine _
    '                                 & "         SELECT B_INKA_M.NRS_BR_CD                                                    " & vbNewLine _
    '                                 & "               ,B_INKA_M.INKA_NO_L                                                    " & vbNewLine _
    '                                 & "               ,MCNT.REC_CNT                                                          " & vbNewLine _
    '                                 & "               ,B_INKA_M.GOODS_CD_NRS                                                 " & vbNewLine _
    '                                 & "               ,SUM(B_INKA_S.KONSU * M_GOODS.PKG_NB + B_INKA_S.HASU)  AS INKA_TTL_NB  " & vbNewLine _
    '                                 & "               ,SUM(FLOOR((B_INKA_S.KONSU * M_GOODS.PKG_NB + B_INKA_S.HASU)           " & vbNewLine _
    '                                 & "                  * B_INKA_S.IRIME                                                    " & vbNewLine _
    '                                 & "                  * M_GOODS.STD_WT_KGS                                                " & vbNewLine _
    '                                 & "                  / M_GOODS.STD_IRIME_NB * 1000) / 1000)                 AS WT        " & vbNewLine _
    '                                 & "         FROM $LM_TRN$..B_INKA_M     B_INKA_M                                         " & vbNewLine _
    '                                 & "         INNER JOIN (                                                                 " & vbNewLine _
    '                                 & "                        SELECT                                                        " & vbNewLine _
    '                                 & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                                 & "                        ,MIN(B_INKA_M.INKA_NO_M)   AS INKA_NO_M                       " & vbNewLine _
    '                                 & "                        ,COUNT(B_INKA_M.INKA_NO_L) AS REC_CNT                         " & vbNewLine _
    '                                 & "                        FROM                                                          " & vbNewLine _
    '                                 & "                         $LM_TRN$..B_INKA_M     B_INKA_M                              " & vbNewLine _
    '                                 & "                        WHERE B_INKA_M.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                                 & "                        GROUP BY                                                      " & vbNewLine _
    '                                 & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                                 & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                                 & "                      ) MCNT                                                          " & vbNewLine _
    '                                 & "          ON      B_INKA_M.NRS_BR_CD = MCNT.NRS_BR_CD                                 " & vbNewLine _
    '                                 & "           AND    B_INKA_M.INKA_NO_L = MCNT.INKA_NO_L                                 " & vbNewLine _
    '                                 & "           AND    B_INKA_M.INKA_NO_M = MCNT.INKA_NO_M                                 " & vbNewLine _
    '                                 & "          LEFT JOIN                                                                   " & vbNewLine _
    '                                 & "          $LM_TRN$..B_INKA_S     B_INKA_S                                             " & vbNewLine _
    '                                 & "          ON      MCNT.NRS_BR_CD = B_INKA_S.NRS_BR_CD                                 " & vbNewLine _
    '                                 & "           AND    MCNT.INKA_NO_L = B_INKA_S.INKA_NO_L                                 " & vbNewLine _
    '                                 & "           AND    MCNT.INKA_NO_M = B_INKA_S.INKA_NO_M                                 " & vbNewLine _
    '                                 & "           AND    B_INKA_S.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                                 & "          LEFT JOIN                                                                   " & vbNewLine _
    '                                 & "           $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                                 & "          ON                                                                          " & vbNewLine _
    '                                 & "           B_INKA_M.NRS_BR_CD =M_GOODS.NRS_BR_CD                                      " & vbNewLine _
    '                                 & "          AND                                                                         " & vbNewLine _
    '                                 & "           B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                                 & "          GROUP BY                                                                    " & vbNewLine _
    '                                 & "            B_INKA_M.NRS_BR_CD                                                        " & vbNewLine _
    '                                 & "           ,B_INKA_M.INKA_NO_L                                                        " & vbNewLine _
    '                                 & "           ,MCNT.REC_CNT                                                              " & vbNewLine _
    '                                 & "           ,B_INKA_M.GOODS_CD_NRS                                                     " & vbNewLine _
    '                                 & "           ,M_GOODS.GOODS_NM_1                                                        " & vbNewLine _
    '                                 & "           ,M_GOODS.PKG_NB                                                            " & vbNewLine _
    '                                 & "           ,M_GOODS.STD_WT_KGS                                                        " & vbNewLine _
    '                                 & ")           B_INKA_M                                                                  " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.NRS_BR_CD =B_INKA_M.NRS_BR_CD                                                " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "B_INKA_L.INKA_NO_L =B_INKA_M.INKA_NO_L                                                " & vbNewLine _
    '                                 & "LEFT JOIN                                                                   " & vbNewLine _
    '                                 & " $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                                 & "ON                                                                          " & vbNewLine _
    '                                 & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                     " & vbNewLine _
    '                                 & "AND                                                                         " & vbNewLine _
    '                                 & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                                       " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                                               " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "B_INKA_L.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                                            " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "F_UNSO_L.MOTO_DATA_KB = '10'                                                          " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..M_DEST                       M_DEST                                         " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "B_INKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "F_UNSO_L.ORIG_CD = M_DEST.DEST_CD                                                     " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..Z_KBN                Z2                                                     " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.UNCHIN_KB = Z2.KBN_CD                                                        " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "Z2.KBN_GROUP_CD = 'T015'                                                              " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..M_UNSOCO                 M_UNSOCO                                           " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                               " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                           " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..Z_KBN        Z3                                                             " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.INKA_TP = Z3.KBN_CD                                                          " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "Z3.KBN_GROUP_CD = 'N007'                                                              " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..Z_KBN        Z4                                                             " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.INKA_KB = Z4.KBN_CD                                                          " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "Z4.KBN_GROUP_CD = 'N006'                                                              " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..M_SOKO               M_SOKO                                                 " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "M_SOKO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                 " & vbNewLine _
    '                                 & "AND                                                                                   " & vbNewLine _
    '                                 & "M_SOKO.WH_CD = B_INKA_L.WH_CD                                                         " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..M_NRS_BR                                                                    " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                                               " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "(                                                                                     " & vbNewLine _
    '                                 & "SELECT                                                                                " & vbNewLine _
    '                                 & "M_TCUST.CUST_CD_L AS CUST_CD_L                                                        " & vbNewLine _
    '                                 & ",min(USER_CD) AS USER_CD                                                              " & vbNewLine _
    '                                 & "FROM                                                                                  " & vbNewLine _
    '                                 & "$LM_MST$..M_TCUST              M_TCUST                                                " & vbNewLine _
    '                                 & "GROUP BY                                                                              " & vbNewLine _
    '                                 & "M_TCUST.CUST_CD_L                                                                     " & vbNewLine _
    '                                 & ") M_TCUST                                                                             " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.CUST_CD_L = M_TCUST.CUST_CD_L                                                " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..S_USER               TANTO_USER                                             " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "M_TCUST.USER_CD = TANTO_USER.USER_CD                                                  " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..S_USER               ENT_USER                                               " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.SYS_ENT_USER = ENT_USER.USER_CD                                              " & vbNewLine _
    '                                 & "LEFT JOIN                                                                             " & vbNewLine _
    '                                 & "$LM_MST$..S_USER               UPD_USER                                               " & vbNewLine _
    '                                 & "ON                                                                                    " & vbNewLine _
    '                                 & "B_INKA_L.SYS_UPD_USER = UPD_USER.USER_CD                                              " & vbNewLine _
    '                                 & "WHERE                                                                                 " & vbNewLine _
    '                                 & "B_INKA_L.SYS_DEL_FLG  = '0'                                                           " & vbNewLine
    'START YANAI 要望番号882
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM As String = "FROM                                                                                  " & vbNewLine _
    '                             & "$LM_TRN$..B_INKA_L                                                                    " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                        Z1                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                                                    " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z1.KBN_GROUP_CD = 'N004'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_CUST                       M_CUST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_S = '00'                                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_SS = '00'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "         SELECT B_INKA_M.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "               ,B_INKA_M.INKA_NO_L                                                    " & vbNewLine _
    '                             & "               ,MCNT.REC_CNT                                                          " & vbNewLine _
    '                             & "               ,B_INKA_M.GOODS_CD_NRS                                                 " & vbNewLine _
    '                             & "               ,SUM(B_INKA_S.KONSU * M_GOODS.PKG_NB + B_INKA_S.HASU)  AS INKA_TTL_NB  " & vbNewLine _
    '                             & "               ,SUM(FLOOR((B_INKA_S.KONSU * M_GOODS.PKG_NB + B_INKA_S.HASU)           " & vbNewLine _
    '                             & "                  * B_INKA_S.IRIME                                                    " & vbNewLine _
    '                             & "                  * M_GOODS.STD_WT_KGS                                                " & vbNewLine _
    '                             & "                  / M_GOODS.STD_IRIME_NB * 1000) / 1000)                 AS WT        " & vbNewLine _
    '                             & "               ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                          " & vbNewLine _
    '                             & "         FROM $LM_TRN$..B_INKA_M     B_INKA_M                                         " & vbNewLine _
    '                             & "         INNER JOIN (                                                                 " & vbNewLine _
    '                             & "                        SELECT                                                        " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                        ,MIN(B_INKA_M.INKA_NO_M)   AS INKA_NO_M                       " & vbNewLine _
    '                             & "                        ,COUNT(B_INKA_M.INKA_NO_L) AS REC_CNT                         " & vbNewLine _
    '                             & "                        FROM                                                          " & vbNewLine _
    '                             & "                         $LM_TRN$..B_INKA_M     B_INKA_M                              " & vbNewLine _
    '                             & "                        WHERE B_INKA_M.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                             & "                        GROUP BY                                                      " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                      ) MCNT                                                          " & vbNewLine _
    '                             & "          ON      B_INKA_M.NRS_BR_CD = MCNT.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_L = MCNT.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_M = MCNT.INKA_NO_M                                 " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "          $LM_TRN$..B_INKA_S     B_INKA_S                                             " & vbNewLine _
    '                             & "          ON      MCNT.NRS_BR_CD = B_INKA_S.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_L = B_INKA_S.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_M = B_INKA_S.INKA_NO_M                                 " & vbNewLine _
    '                             & "           AND    B_INKA_S.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "           $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                             & "          ON                                                                          " & vbNewLine _
    '                             & "           B_INKA_M.NRS_BR_CD =M_GOODS.NRS_BR_CD                                      " & vbNewLine _
    '                             & "          AND                                                                         " & vbNewLine _
    '                             & "           B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                             & "          GROUP BY                                                                    " & vbNewLine _
    '                             & "            B_INKA_M.NRS_BR_CD                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.INKA_NO_L                                                        " & vbNewLine _
    '                             & "           ,MCNT.REC_CNT                                                              " & vbNewLine _
    '                             & "           ,B_INKA_M.GOODS_CD_NRS                                                     " & vbNewLine _
    '                             & "           ,M_GOODS.GOODS_NM_1                                                        " & vbNewLine _
    '                             & "           ,M_GOODS.PKG_NB                                                            " & vbNewLine _
    '                             & "           ,M_GOODS.STD_WT_KGS                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                              " & vbNewLine _
    '                             & ")           B_INKA_M                                                                  " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD =B_INKA_M.NRS_BR_CD                                                " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L =B_INKA_M.INKA_NO_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                   " & vbNewLine _
    '                             & " $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                             & "ON                                                                          " & vbNewLine _
    '                             & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                     " & vbNewLine _
    '                             & "AND                                                                         " & vbNewLine _
    '                             & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                                       " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                                            " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.MOTO_DATA_KB = '10'                                                          " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_DEST                       M_DEST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.ORIG_CD = M_DEST.DEST_CD                                                     " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                Z2                                                     " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.UNCHIN_KB = Z2.KBN_CD                                                        " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z2.KBN_GROUP_CD = 'T015'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_UNSOCO                 M_UNSOCO                                           " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                           " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z3                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_TP = Z3.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z3.KBN_GROUP_CD = 'N007'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z4                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_KB = Z4.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z4.KBN_GROUP_CD = 'N006'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_SOKO               M_SOKO                                                 " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_SOKO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_SOKO.WH_CD = B_INKA_L.WH_CD                                                         " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_NRS_BR                                                                    " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                                               " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "SELECT                                                                                " & vbNewLine _
    '                             & "M_TCUST.CUST_CD_L AS CUST_CD_L                                                        " & vbNewLine _
    '                             & ",min(USER_CD) AS USER_CD                                                              " & vbNewLine _
    '                             & "FROM                                                                                  " & vbNewLine _
    '                             & "$LM_MST$..M_TCUST              M_TCUST                                                " & vbNewLine _
    '                             & "GROUP BY                                                                              " & vbNewLine _
    '                             & "M_TCUST.CUST_CD_L                                                                     " & vbNewLine _
    '                             & ") M_TCUST                                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_TCUST.CUST_CD_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               TANTO_USER                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_TCUST.USER_CD = TANTO_USER.USER_CD                                                  " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               ENT_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_ENT_USER = ENT_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               UPD_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_UPD_USER = UPD_USER.USER_CD                                              " & vbNewLine _
    '                             & "WHERE                                                                                 " & vbNewLine _
    '                             & "B_INKA_L.SYS_DEL_FLG  = '0'                                                           " & vbNewLine
    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM As String = "FROM                                                                                  " & vbNewLine _
    '                             & "$LM_TRN$..B_INKA_L                                                                    " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                        Z1                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                                                    " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z1.KBN_GROUP_CD = 'N004'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_CUST                       M_CUST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_S = '00'                                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_SS = '00'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "         SELECT B_INKA_M.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "               ,B_INKA_M.INKA_NO_L                                                    " & vbNewLine _
    '                             & "               ,MCNT.REC_CNT                                                          " & vbNewLine _
    '                             & "               ,B_INKA_M.GOODS_CD_NRS                                                 " & vbNewLine _
    '                             & "               ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                          " & vbNewLine _
    '                             & "         FROM $LM_TRN$..B_INKA_M     B_INKA_M                                         " & vbNewLine _
    '                             & "         INNER JOIN (                                                                 " & vbNewLine _
    '                             & "                        SELECT                                                        " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                        ,MIN(B_INKA_M.INKA_NO_M)   AS INKA_NO_M                       " & vbNewLine _
    '                             & "                        ,COUNT(B_INKA_M.INKA_NO_L) AS REC_CNT                         " & vbNewLine _
    '                             & "                        FROM                                                          " & vbNewLine _
    '                             & "                         $LM_TRN$..B_INKA_M     B_INKA_M                              " & vbNewLine _
    '                             & "                        WHERE B_INKA_M.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                             & "                        GROUP BY                                                      " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                      ) MCNT                                                          " & vbNewLine _
    '                             & "          ON      B_INKA_M.NRS_BR_CD = MCNT.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_L = MCNT.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_M = MCNT.INKA_NO_M                                 " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "          $LM_TRN$..B_INKA_S     B_INKA_S                                             " & vbNewLine _
    '                             & "          ON      MCNT.NRS_BR_CD = B_INKA_S.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_L = B_INKA_S.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_M = B_INKA_S.INKA_NO_M                                 " & vbNewLine _
    '                             & "           AND    B_INKA_S.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "           $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                             & "          ON                                                                          " & vbNewLine _
    '                             & "           B_INKA_M.NRS_BR_CD =M_GOODS.NRS_BR_CD                                      " & vbNewLine _
    '                             & "          AND                                                                         " & vbNewLine _
    '                             & "           B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                             & "          GROUP BY                                                                    " & vbNewLine _
    '                             & "            B_INKA_M.NRS_BR_CD                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.INKA_NO_L                                                        " & vbNewLine _
    '                             & "           ,MCNT.REC_CNT                                                              " & vbNewLine _
    '                             & "           ,B_INKA_M.GOODS_CD_NRS                                                     " & vbNewLine _
    '                             & "           ,M_GOODS.GOODS_NM_1                                                        " & vbNewLine _
    '                             & "           ,M_GOODS.PKG_NB                                                            " & vbNewLine _
    '                             & "           ,M_GOODS.STD_WT_KGS                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                              " & vbNewLine _
    '                             & ")           B_INKA_M                                                                  " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD =B_INKA_M.NRS_BR_CD                                                " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L =B_INKA_M.INKA_NO_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                   " & vbNewLine _
    '                             & " $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                             & "ON                                                                          " & vbNewLine _
    '                             & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                     " & vbNewLine _
    '                             & "AND                                                                         " & vbNewLine _
    '                             & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                                       " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                                            " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.MOTO_DATA_KB = '10'                                                          " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_DEST                       M_DEST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.ORIG_CD = M_DEST.DEST_CD                                                     " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                Z2                                                     " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.UNCHIN_KB = Z2.KBN_CD                                                        " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z2.KBN_GROUP_CD = 'T015'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_UNSOCO                 M_UNSOCO                                           " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                           " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z3                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_TP = Z3.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z3.KBN_GROUP_CD = 'N007'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z4                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_KB = Z4.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z4.KBN_GROUP_CD = 'N006'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_SOKO               M_SOKO                                                 " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_SOKO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_SOKO.WH_CD = B_INKA_L.WH_CD                                                         " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_NRS_BR                                                                    " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                                               " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "SELECT                                                                                " & vbNewLine _
    '                             & "M_TCUST.CUST_CD_L AS CUST_CD_L                                                        " & vbNewLine _
    '                             & ",min(USER_CD) AS USER_CD                                                              " & vbNewLine _
    '                             & "FROM                                                                                  " & vbNewLine _
    '                             & "$LM_MST$..M_TCUST              M_TCUST                                                " & vbNewLine _
    '                             & "GROUP BY                                                                              " & vbNewLine _
    '                             & "M_TCUST.CUST_CD_L                                                                     " & vbNewLine _
    '                             & ") M_TCUST                                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_TCUST.CUST_CD_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               TANTO_USER                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_TCUST.USER_CD = TANTO_USER.USER_CD                                                  " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               ENT_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_ENT_USER = ENT_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               UPD_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_UPD_USER = UPD_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(SELECT                                                                               " & vbNewLine _
    '                             & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
    '                             & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
    '                             & " ,SUM(INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) AS INKA_TTL_NB                         " & vbNewLine _
    '                             & " ,SUM(FLOOR((INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU)                                 " & vbNewLine _
    '                             & "      * INKAS.IRIME                                                                   " & vbNewLine _
    '                             & "      * GOODS.STD_WT_KGS                                                              " & vbNewLine _
    '                             & "      / GOODS.STD_IRIME_NB * 1000) / 1000)                 AS WT                      " & vbNewLine _
    '                             & " FROM $LM_TRN$..B_INKA_L INKAL                                                        " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                   " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAM.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                   " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                    " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                              " & vbNewLine _
    '                             & " WHERE                                                                                " & vbNewLine _
    '                             & " INKAL.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " GROUP BY                                                                             " & vbNewLine _
    '                             & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
    '                             & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
    '                             & " ) INKAL2                                                                             " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAL2.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAL2.INKA_NO_L = B_INKA_L.INKA_NO_L                                                " & vbNewLine _
    '                             & "WHERE                                                                                 " & vbNewLine _
    '                             & "B_INKA_L.SYS_DEL_FLG  = '0'                                                           " & vbNewLine
    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM As String = "FROM                                                                              " & vbNewLine _
    '                             & "$LM_TRN$..B_INKA_L                                                                    " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                        Z1                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                                                    " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z1.KBN_GROUP_CD = 'N004'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_CUST                       M_CUST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_S = '00'                                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_SS = '00'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "         SELECT B_INKA_M.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "               ,B_INKA_M.INKA_NO_L                                                    " & vbNewLine _
    '                             & "               ,MCNT.REC_CNT                                                          " & vbNewLine _
    '                             & "               ,B_INKA_M.GOODS_CD_NRS                                                 " & vbNewLine _
    '                             & "               ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                          " & vbNewLine _
    '                             & "         FROM $LM_TRN$..B_INKA_M     B_INKA_M                                         " & vbNewLine _
    '                             & "         INNER JOIN (                                                                 " & vbNewLine _
    '                             & "                        SELECT                                                        " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                        ,MIN(B_INKA_M.INKA_NO_M)   AS INKA_NO_M                       " & vbNewLine _
    '                             & "                        ,COUNT(B_INKA_M.INKA_NO_L) AS REC_CNT                         " & vbNewLine _
    '                             & "                        FROM                                                          " & vbNewLine _
    '                             & "                         $LM_TRN$..B_INKA_M     B_INKA_M                              " & vbNewLine _
    '                             & "                        WHERE B_INKA_M.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                             & "                        GROUP BY                                                      " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                      ) MCNT                                                          " & vbNewLine _
    '                             & "          ON      B_INKA_M.NRS_BR_CD = MCNT.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_L = MCNT.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_M = MCNT.INKA_NO_M                                 " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "          $LM_TRN$..B_INKA_S     B_INKA_S                                             " & vbNewLine _
    '                             & "          ON      MCNT.NRS_BR_CD = B_INKA_S.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_L = B_INKA_S.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_M = B_INKA_S.INKA_NO_M                                 " & vbNewLine _
    '                             & "           AND    B_INKA_S.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "           $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                             & "          ON                                                                          " & vbNewLine _
    '                             & "           B_INKA_M.NRS_BR_CD =M_GOODS.NRS_BR_CD                                      " & vbNewLine _
    '                             & "          AND                                                                         " & vbNewLine _
    '                             & "           B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                             & "          GROUP BY                                                                    " & vbNewLine _
    '                             & "            B_INKA_M.NRS_BR_CD                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.INKA_NO_L                                                        " & vbNewLine _
    '                             & "           ,MCNT.REC_CNT                                                              " & vbNewLine _
    '                             & "           ,B_INKA_M.GOODS_CD_NRS                                                     " & vbNewLine _
    '                             & "           ,M_GOODS.GOODS_NM_1                                                        " & vbNewLine _
    '                             & "           ,M_GOODS.PKG_NB                                                            " & vbNewLine _
    '                             & "           ,M_GOODS.STD_WT_KGS                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                              " & vbNewLine _
    '                             & ")           B_INKA_M                                                                  " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD =B_INKA_M.NRS_BR_CD                                                " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L =B_INKA_M.INKA_NO_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "         SELECT B_INKA_S.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "               ,B_INKA_S.INKA_NO_L                                                    " & vbNewLine _
    '                             & "               ,COUNT(B_INKA_S.INKA_NO_L) AS REC_CNT                                  " & vbNewLine _
    '                             & "         FROM $LM_TRN$..B_INKA_S     B_INKA_S                                         " & vbNewLine _
    '                             & "         WHERE B_INKA_S.SYS_DEL_FLG = '0'                                             " & vbNewLine _
    '                             & "          GROUP BY                                                                    " & vbNewLine _
    '                             & "            B_INKA_S.NRS_BR_CD                                                        " & vbNewLine _
    '                             & "           ,B_INKA_S.INKA_NO_L                                                        " & vbNewLine _
    '                             & ")           SCNT                                                                      " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "SCNT.NRS_BR_CD =B_INKA_L.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "SCNT.INKA_NO_L =B_INKA_L.INKA_NO_L                                                    " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & " $LM_MST$..M_GOODS              M_GOODS                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                                         " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                                       " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                                            " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.MOTO_DATA_KB = '10'                                                          " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_DEST                       M_DEST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.ORIG_CD = M_DEST.DEST_CD                                                     " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                Z2                                                     " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.UNCHIN_KB = Z2.KBN_CD                                                        " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z2.KBN_GROUP_CD = 'T015'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_UNSOCO                 M_UNSOCO                                           " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                           " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z3                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_TP = Z3.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z3.KBN_GROUP_CD = 'N007'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z4                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_KB = Z4.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z4.KBN_GROUP_CD = 'N006'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_SOKO               M_SOKO                                                 " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_SOKO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_SOKO.WH_CD = B_INKA_L.WH_CD                                                         " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_NRS_BR                                                                    " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                                               " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "SELECT                                                                                " & vbNewLine _
    '                             & "M_TCUST.CUST_CD_L AS CUST_CD_L                                                        " & vbNewLine _
    '                             & ",min(USER_CD) AS USER_CD                                                              " & vbNewLine _
    '                             & "FROM                                                                                  " & vbNewLine _
    '                             & "$LM_MST$..M_TCUST              M_TCUST                                                " & vbNewLine _
    '                             & "GROUP BY                                                                              " & vbNewLine _
    '                             & "M_TCUST.CUST_CD_L                                                                     " & vbNewLine _
    '                             & ") M_TCUST                                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_TCUST.CUST_CD_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               TANTO_USER                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_TCUST.USER_CD = TANTO_USER.USER_CD                                                  " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               ENT_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_ENT_USER = ENT_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               UPD_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_UPD_USER = UPD_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(SELECT                                                                               " & vbNewLine _
    '                             & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
    '                             & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
    '                             & " ,SUM(INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) AS INKA_TTL_NB                         " & vbNewLine _
    '                             & " ,SUM(FLOOR((INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU)                                 " & vbNewLine _
    '                             & "      * INKAS.IRIME                                                                   " & vbNewLine _
    '                             & "      * GOODS.STD_WT_KGS                                                              " & vbNewLine _
    '                             & "      / GOODS.STD_IRIME_NB * 1000) / 1000)                 AS WT                      " & vbNewLine _
    '                             & " FROM $LM_TRN$..B_INKA_L INKAL                                                        " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                   " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAM.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                   " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                    " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                              " & vbNewLine _
    '                             & " WHERE                                                                                " & vbNewLine _
    '                             & " INKAL.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " GROUP BY                                                                             " & vbNewLine _
    '                             & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
    '                             & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
    '                             & " ) INKAL2                                                                             " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAL2.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAL2.INKA_NO_L = B_INKA_L.INKA_NO_L                                                " & vbNewLine _
    '                             & "WHERE                                                                                 " & vbNewLine _
    '                             & "B_INKA_L.SYS_DEL_FLG  = '0'                                                           " & vbNewLine
    ' ''' <summary>
    ' ''' データ抽出用FROM句
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Const SQL_FROM As String = "FROM                                                                              " & vbNewLine _
    '                             & "$LM_TRN$..B_INKA_L                                                                    " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                        Z1                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                                                    " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z1.KBN_GROUP_CD = 'N004'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_CUST                       M_CUST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_S = '00'                                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_CUST.CUST_CD_SS = '00'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "         SELECT B_INKA_M.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "               ,B_INKA_M.INKA_NO_L                                                    " & vbNewLine _
    '                             & "               ,MCNT.REC_CNT                                                          " & vbNewLine _
    '                             & "               ,B_INKA_M.GOODS_CD_NRS                                                 " & vbNewLine _
    '                             & "               ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                          " & vbNewLine _
    '                             & "         FROM $LM_TRN$..B_INKA_M     B_INKA_M                                         " & vbNewLine _
    '                             & "         INNER JOIN (                                                                 " & vbNewLine _
    '                             & "                        SELECT                                                        " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                        ,MIN(B_INKA_M.INKA_NO_M)   AS INKA_NO_M                       " & vbNewLine _
    '                             & "                        ,COUNT(B_INKA_M.INKA_NO_L) AS REC_CNT                         " & vbNewLine _
    '                             & "                        FROM                                                          " & vbNewLine _
    '                             & "                         $LM_TRN$..B_INKA_M     B_INKA_M                              " & vbNewLine _
    '                             & "                        WHERE B_INKA_M.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                             & "                        GROUP BY                                                      " & vbNewLine _
    '                             & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
    '                             & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
    '                             & "                      ) MCNT                                                          " & vbNewLine _
    '                             & "          ON      B_INKA_M.NRS_BR_CD = MCNT.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_L = MCNT.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    B_INKA_M.INKA_NO_M = MCNT.INKA_NO_M                                 " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "          $LM_TRN$..B_INKA_S     B_INKA_S                                             " & vbNewLine _
    '                             & "          ON      MCNT.NRS_BR_CD = B_INKA_S.NRS_BR_CD                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_L = B_INKA_S.INKA_NO_L                                 " & vbNewLine _
    '                             & "           AND    MCNT.INKA_NO_M = B_INKA_S.INKA_NO_M                                 " & vbNewLine _
    '                             & "           AND    B_INKA_S.SYS_DEL_FLG = '0'                                          " & vbNewLine _
    '                             & "          LEFT JOIN                                                                   " & vbNewLine _
    '                             & "           $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
    '                             & "          ON                                                                          " & vbNewLine _
    '                             & "           B_INKA_M.NRS_BR_CD =M_GOODS.NRS_BR_CD                                      " & vbNewLine _
    '                             & "          AND                                                                         " & vbNewLine _
    '                             & "           B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
    '                             & "          GROUP BY                                                                    " & vbNewLine _
    '                             & "            B_INKA_M.NRS_BR_CD                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.INKA_NO_L                                                        " & vbNewLine _
    '                             & "           ,MCNT.REC_CNT                                                              " & vbNewLine _
    '                             & "           ,B_INKA_M.GOODS_CD_NRS                                                     " & vbNewLine _
    '                             & "           ,M_GOODS.GOODS_NM_1                                                        " & vbNewLine _
    '                             & "           ,M_GOODS.PKG_NB                                                            " & vbNewLine _
    '                             & "           ,M_GOODS.STD_WT_KGS                                                        " & vbNewLine _
    '                             & "           ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                              " & vbNewLine _
    '                             & ")           B_INKA_M                                                                  " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD =B_INKA_M.NRS_BR_CD                                                " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L =B_INKA_M.INKA_NO_L                                                " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(                                                                                     " & vbNewLine _
    '                             & "         SELECT B_INKA_S.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "               ,B_INKA_S.INKA_NO_L                                                    " & vbNewLine _
    '                             & "               ,COUNT(B_INKA_S.INKA_NO_L) AS REC_CNT                                  " & vbNewLine _
    '                             & "         FROM $LM_TRN$..B_INKA_S     B_INKA_S                                         " & vbNewLine _
    '                             & "         WHERE B_INKA_S.SYS_DEL_FLG = '0'                                             " & vbNewLine _
    '                             & "          GROUP BY                                                                    " & vbNewLine _
    '                             & "            B_INKA_S.NRS_BR_CD                                                        " & vbNewLine _
    '                             & "           ,B_INKA_S.INKA_NO_L                                                        " & vbNewLine _
    '                             & ")           SCNT                                                                      " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "SCNT.NRS_BR_CD =B_INKA_L.NRS_BR_CD                                                    " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "SCNT.INKA_NO_L =B_INKA_L.INKA_NO_L                                                    " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & " $LM_MST$..M_GOODS              M_GOODS                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                                         " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                                       " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                                            " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.MOTO_DATA_KB = '10'                                                          " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_DEST                       M_DEST                                         " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "B_INKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.ORIG_CD = M_DEST.DEST_CD                                                     " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN                Z2                                                     " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.UNCHIN_KB = Z2.KBN_CD                                                        " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z2.KBN_GROUP_CD = 'T015'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_UNSOCO                 M_UNSOCO                                           " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                               " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                           " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z3                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_TP = Z3.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z3.KBN_GROUP_CD = 'N007'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..Z_KBN        Z4                                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.INKA_KB = Z4.KBN_CD                                                          " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "Z4.KBN_GROUP_CD = 'N006'                                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_SOKO               M_SOKO                                                 " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_SOKO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                 " & vbNewLine _
    '                             & "AND                                                                                   " & vbNewLine _
    '                             & "M_SOKO.WH_CD = B_INKA_L.WH_CD                                                         " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..M_NRS_BR                                                                    " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                                               " & vbNewLine _
    '                             & " -- 要望番号1756 yamanaka 2013.03.01 START                                            " & vbNewLine _
    '                             & " -- (2012.09.03) 要望番号1359 修正 --- START ---                                      " & vbNewLine _
    '                             & " --LEFT JOIN                                                                          " & vbNewLine _
    '                             & " --(                                                                                  " & vbNewLine _
    '                             & " --SELECT                                                                             " & vbNewLine _
    '                             & " --M_TCUST.CUST_CD_L AS CUST_CD_L                                                     " & vbNewLine _
    '                             & " --,min(USER_CD) AS USER_CD                                                           " & vbNewLine _
    '                             & " --FROM                                                                               " & vbNewLine _
    '                             & " --$LM_MST$..M_TCUST              M_TCUST                                             " & vbNewLine _
    '                             & " --GROUP BY                                                                           " & vbNewLine _
    '                             & " --M_TCUST.CUST_CD_L                                                                  " & vbNewLine _
    '                             & " --) M_TCUST                                                                          " & vbNewLine _
    '                             & " --ON                                                                                 " & vbNewLine _
    '                             & " --B_INKA_L.CUST_CD_L = M_TCUST.CUST_CD_L                                             " & vbNewLine _
    '                             & " -- 下記のJOINに変更                                                                  " & vbNewLine _
    '                             & " --LEFT JOIN (SELECT                                                                  " & vbNewLine _
    '                             & " --                  M_TCUST.CUST_CD_L    AS CUST_CD_L                                " & vbNewLine _
    '                             & " --                , MIN(M_TCUST.USER_CD) AS USER_CD                                  " & vbNewLine _
    '                             & " --                , S_USER.NRS_BR_CD     AS NRS_BR_CD                                " & vbNewLine _
    '                             & " --             FROM $LM_MST$..M_TCUST M_TCUST                                        " & vbNewLine _
    '                             & " --                LEFT JOIN $LM_MST$..S_USER S_USER                                  " & vbNewLine _
    '                             & " --                         ON S_USER.USER_CD = M_TCUST.USER_CD                       " & vbNewLine _
    '                             & " --            GROUP BY                                                               " & vbNewLine _
    '                             & " --                  M_TCUST.CUST_CD_L                                                " & vbNewLine _
    '                             & " --                , S_USER.NRS_BR_CD                                                 " & vbNewLine _
    '                             & " --           ) M_TCUST                                                               " & vbNewLine _
    '                             & " --       ON M_TCUST.CUST_CD_L = B_INKA_L.CUST_CD_L                                   " & vbNewLine _
    '                             & " --      AND M_TCUST.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                   " & vbNewLine _
    '                             & " -- (2012.09.03) 要望番号1359 修正 ---  END  ---                                      " & vbNewLine _
    '                             & " --LEFT JOIN                                                                          " & vbNewLine _
    '                             & " --$LM_MST$..S_USER               TANTO_USER                                          " & vbNewLine _
    '                             & " --ON                                                                                 " & vbNewLine _
    '                             & " --M_TCUST.USER_CD = TANTO_USER.USER_CD                                               " & vbNewLine _
    '                             & " -- (2012.09.03) 要望番号1359 追加 --- START ---                                      " & vbNewLine _
    '                             & " --AND M_TCUST.NRS_BR_CD = TANTO_USER.NRS_BR_CD                                       " & vbNewLine _
    '                             & " -- (2012.09.03) 要望番号1359 追加 ---  END  ---                                      " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               TANTO_USER                                             " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                                                  " & vbNewLine _
    '                             & " -- 要望番号1756 yamanaka 2013.03.01 END                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               ENT_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_ENT_USER = ENT_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "$LM_MST$..S_USER               UPD_USER                                               " & vbNewLine _
    '                             & "ON                                                                                    " & vbNewLine _
    '                             & "B_INKA_L.SYS_UPD_USER = UPD_USER.USER_CD                                              " & vbNewLine _
    '                             & "LEFT JOIN                                                                             " & vbNewLine _
    '                             & "(SELECT                                                                               " & vbNewLine _
    '                             & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
    '                             & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
    '                             & " ,SUM(INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) AS INKA_TTL_NB                         " & vbNewLine _
    '                             & " ,SUM(FLOOR((INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU)                                 " & vbNewLine _
    '                             & "      * INKAS.IRIME                                                                   " & vbNewLine _
    '                             & "      * GOODS.STD_WT_KGS                                                              " & vbNewLine _
    '                             & "      / GOODS.STD_IRIME_NB * 1000) / 1000)                 AS WT                      " & vbNewLine _
    '                             & " FROM $LM_TRN$..B_INKA_L INKAL                                                        " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                   " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAM.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                   " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                    " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                              " & vbNewLine _
    '                             & " WHERE                                                                                " & vbNewLine _
    '                             & " INKAL.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
    '                             & " GROUP BY                                                                             " & vbNewLine _
    '                             & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
    '                             & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
    '                             & " ) INKAL2                                                                             " & vbNewLine _
    '                             & " ON                                                                                   " & vbNewLine _
    '                             & " INKAL2.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                " & vbNewLine _
    '                             & " AND                                                                                  " & vbNewLine _
    '                             & " INKAL2.INKA_NO_L = B_INKA_L.INKA_NO_L                                                " & vbNewLine
    '20160614 tsunehira add start
    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                                              " & vbNewLine _
                                 & "$LM_TRN$..B_INKA_L                                                                    " & vbNewLine _
                                 & " LEFT OUTER JOIN                                                                      " & vbNewLine _
                                 & "    (SELECT                                                                           " & vbNewLine _
                                 & "        KBN1.KBN_CD,                                                                  " & vbNewLine _
                                 & "        " & " #KBN# " & "     AS KBN_NM1                                              " & vbNewLine _
                                 & "     FROM $LM_MST$..Z_KBN AS KBN1                                                     " & vbNewLine _
                                 & "     WHERE                                                                            " & vbNewLine _
                                 & "        KBN1.KBN_GROUP_CD = 'N004')Z1                                                 " & vbNewLine _
                                 & "  ON B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                                               " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..M_CUST                       M_CUST                                         " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "M_CUST.CUST_CD_S = '00'                                                               " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "M_CUST.CUST_CD_SS = '00'                                                              " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "(                                                                                     " & vbNewLine _
                                 & "         SELECT B_INKA_M.NRS_BR_CD                                                    " & vbNewLine _
                                 & "               ,B_INKA_M.INKA_NO_L                                                    " & vbNewLine _
                                 & "               ,MCNT.REC_CNT                                                          " & vbNewLine _
                                 & "               ,B_INKA_M.GOODS_CD_NRS                                                 " & vbNewLine _
                                 & "               ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                          " & vbNewLine _
                                 & "         FROM $LM_TRN$..B_INKA_M     B_INKA_M                                         " & vbNewLine _
                                 & "         INNER JOIN (                                                                 " & vbNewLine _
                                 & "                        SELECT                                                        " & vbNewLine _
                                 & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
                                 & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
                                 & "                        ,MIN(B_INKA_M.INKA_NO_M)   AS INKA_NO_M                       " & vbNewLine _
                                 & "                        ,COUNT(B_INKA_M.INKA_NO_L) AS REC_CNT                         " & vbNewLine _
                                 & "                        FROM                                                          " & vbNewLine _
                                 & "                         $LM_TRN$..B_INKA_M     B_INKA_M                              " & vbNewLine _
                                 & "                        WHERE B_INKA_M.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                 & "                        GROUP BY                                                      " & vbNewLine _
                                 & "                         B_INKA_M.NRS_BR_CD                                           " & vbNewLine _
                                 & "                        ,B_INKA_M.INKA_NO_L                                           " & vbNewLine _
                                 & "                      ) MCNT                                                          " & vbNewLine _
                                 & "          ON      B_INKA_M.NRS_BR_CD = MCNT.NRS_BR_CD                                 " & vbNewLine _
                                 & "           AND    B_INKA_M.INKA_NO_L = MCNT.INKA_NO_L                                 " & vbNewLine _
                                 & "           AND    B_INKA_M.INKA_NO_M = MCNT.INKA_NO_M                                 " & vbNewLine _
                                 & "          LEFT JOIN                                                                   " & vbNewLine _
                                 & "          $LM_TRN$..B_INKA_S     B_INKA_S                                             " & vbNewLine _
                                 & "          ON      MCNT.NRS_BR_CD = B_INKA_S.NRS_BR_CD                                 " & vbNewLine _
                                 & "           AND    MCNT.INKA_NO_L = B_INKA_S.INKA_NO_L                                 " & vbNewLine _
                                 & "           AND    MCNT.INKA_NO_M = B_INKA_S.INKA_NO_M                                 " & vbNewLine _
                                 & "           AND    B_INKA_S.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                 & "          LEFT JOIN                                                                   " & vbNewLine _
                                 & "           $LM_MST$..M_GOODS              M_GOODS                                     " & vbNewLine _
                                 & "          ON                                                                          " & vbNewLine _
                                 & "           B_INKA_M.NRS_BR_CD =M_GOODS.NRS_BR_CD                                      " & vbNewLine _
                                 & "          AND                                                                         " & vbNewLine _
                                 & "           B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
                                 & "          GROUP BY                                                                    " & vbNewLine _
                                 & "            B_INKA_M.NRS_BR_CD                                                        " & vbNewLine _
                                 & "           ,B_INKA_M.INKA_NO_L                                                        " & vbNewLine _
                                 & "           ,MCNT.REC_CNT                                                              " & vbNewLine _
                                 & "           ,B_INKA_M.GOODS_CD_NRS                                                     " & vbNewLine _
                                 & "           ,M_GOODS.GOODS_NM_1                                                        " & vbNewLine _
                                 & "           ,M_GOODS.PKG_NB                                                            " & vbNewLine _
                                 & "           ,M_GOODS.STD_WT_KGS                                                        " & vbNewLine _
                                 & "           ,B_INKA_M.OUTKA_FROM_ORD_NO_M                                              " & vbNewLine _
                                 & ")           B_INKA_M                                                                  " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.NRS_BR_CD =B_INKA_M.NRS_BR_CD                                                " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "B_INKA_L.INKA_NO_L =B_INKA_M.INKA_NO_L                                                " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "(                                                                                     " & vbNewLine _
                                 & "         SELECT B_INKA_S.NRS_BR_CD                                                    " & vbNewLine _
                                 & "               ,B_INKA_S.INKA_NO_L                                                    " & vbNewLine _
                                 & "               ,COUNT(B_INKA_S.INKA_NO_L) AS REC_CNT                                  " & vbNewLine _
                                 & "         FROM $LM_TRN$..B_INKA_S     B_INKA_S                                         " & vbNewLine _
                                 & "         WHERE B_INKA_S.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                 & "          GROUP BY                                                                    " & vbNewLine _
                                 & "            B_INKA_S.NRS_BR_CD                                                        " & vbNewLine _
                                 & "           ,B_INKA_S.INKA_NO_L                                                        " & vbNewLine _
                                 & ")           SCNT                                                                      " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "SCNT.NRS_BR_CD =B_INKA_L.NRS_BR_CD                                                    " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "SCNT.INKA_NO_L =B_INKA_L.INKA_NO_L                                                    " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & " $LM_MST$..M_GOODS              M_GOODS                                               " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                               " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                                         " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                                       " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                                               " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "B_INKA_L.INKA_NO_L = F_UNSO_L.INOUTKA_NO_L                                            " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "F_UNSO_L.MOTO_DATA_KB = '10'                                                          " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..M_DEST                       M_DEST                                         " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "B_INKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "F_UNSO_L.ORIG_CD = M_DEST.DEST_CD                                                     " & vbNewLine _
                                 & " LEFT OUTER JOIN                                                                      " & vbNewLine _
                                 & "    (SELECT                                                                           " & vbNewLine _
                                 & "        KBN1.KBN_CD,                                                                  " & vbNewLine _
                                 & "        " & " #KBN# " & "     AS KBN_NM1                                              " & vbNewLine _
                                 & "     FROM $LM_MST$..Z_KBN AS KBN1                                                     " & vbNewLine _
                                 & "     WHERE                                                                            " & vbNewLine _
                                 & "        KBN1.KBN_GROUP_CD = 'T015')Z2                                                 " & vbNewLine _
                                 & "  ON B_INKA_L.UNCHIN_KB = Z2.KBN_CD                                                   " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..M_UNSOCO                 M_UNSOCO                                           " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                               " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                           " & vbNewLine _
                                 & " LEFT OUTER JOIN                                                                      " & vbNewLine _
                                 & "    (SELECT                                                                           " & vbNewLine _
                                 & "        KBN1.KBN_CD,                                                                  " & vbNewLine _
                                 & "        " & " #KBN# " & "     AS KBN_NM1                                              " & vbNewLine _
                                 & "     FROM $LM_MST$..Z_KBN AS KBN1                                                     " & vbNewLine _
                                 & "     WHERE                                                                            " & vbNewLine _
                                 & "        KBN1.KBN_GROUP_CD = 'N007')Z3                                                 " & vbNewLine _
                                 & "  ON B_INKA_L.INKA_TP = Z3.KBN_CD                                                     " & vbNewLine _
                                 & " LEFT OUTER JOIN                                                                      " & vbNewLine _
                                 & "    (SELECT                                                                           " & vbNewLine _
                                 & "        KBN1.KBN_CD,                                                                  " & vbNewLine _
                                 & "        " & " #KBN# " & "     AS KBN_NM1                                              " & vbNewLine _
                                 & "     FROM $LM_MST$..Z_KBN AS KBN1                                                     " & vbNewLine _
                                 & "     WHERE                                                                            " & vbNewLine _
                                 & "        KBN1.KBN_GROUP_CD = 'N006')Z4                                                 " & vbNewLine _
                                 & "  ON B_INKA_L.INKA_KB = Z4.KBN_CD                                                     " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..M_SOKO               M_SOKO                                                 " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "M_SOKO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                 " & vbNewLine _
                                 & "AND                                                                                   " & vbNewLine _
                                 & "M_SOKO.WH_CD = B_INKA_L.WH_CD                                                         " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..M_NRS_BR                                                                    " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                                               " & vbNewLine _
                                 & " -- 要望番号1756 yamanaka 2013.03.01 START                                            " & vbNewLine _
                                 & " -- (2012.09.03) 要望番号1359 修正 --- START ---                                      " & vbNewLine _
                                 & " --LEFT JOIN                                                                          " & vbNewLine _
                                 & " --(                                                                                  " & vbNewLine _
                                 & " --SELECT                                                                             " & vbNewLine _
                                 & " --M_TCUST.CUST_CD_L AS CUST_CD_L                                                     " & vbNewLine _
                                 & " --,min(USER_CD) AS USER_CD                                                           " & vbNewLine _
                                 & " --FROM                                                                               " & vbNewLine _
                                 & " --$LM_MST$..M_TCUST              M_TCUST                                             " & vbNewLine _
                                 & " --GROUP BY                                                                           " & vbNewLine _
                                 & " --M_TCUST.CUST_CD_L                                                                  " & vbNewLine _
                                 & " --) M_TCUST                                                                          " & vbNewLine _
                                 & " --ON                                                                                 " & vbNewLine _
                                 & " --B_INKA_L.CUST_CD_L = M_TCUST.CUST_CD_L                                             " & vbNewLine _
                                 & " -- 下記のJOINに変更                                                                  " & vbNewLine _
                                 & " --LEFT JOIN (SELECT                                                                  " & vbNewLine _
                                 & " --                  M_TCUST.CUST_CD_L    AS CUST_CD_L                                " & vbNewLine _
                                 & " --                , MIN(M_TCUST.USER_CD) AS USER_CD                                  " & vbNewLine _
                                 & " --                , S_USER.NRS_BR_CD     AS NRS_BR_CD                                " & vbNewLine _
                                 & " --             FROM $LM_MST$..M_TCUST M_TCUST                                        " & vbNewLine _
                                 & " --                LEFT JOIN $LM_MST$..S_USER S_USER                                  " & vbNewLine _
                                 & " --                         ON S_USER.USER_CD = M_TCUST.USER_CD                       " & vbNewLine _
                                 & " --            GROUP BY                                                               " & vbNewLine _
                                 & " --                  M_TCUST.CUST_CD_L                                                " & vbNewLine _
                                 & " --                , S_USER.NRS_BR_CD                                                 " & vbNewLine _
                                 & " --           ) M_TCUST                                                               " & vbNewLine _
                                 & " --       ON M_TCUST.CUST_CD_L = B_INKA_L.CUST_CD_L                                   " & vbNewLine _
                                 & " --      AND M_TCUST.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                   " & vbNewLine _
                                 & " -- (2012.09.03) 要望番号1359 修正 ---  END  ---                                      " & vbNewLine _
                                 & " --LEFT JOIN                                                                          " & vbNewLine _
                                 & " --$LM_MST$..S_USER               TANTO_USER                                          " & vbNewLine _
                                 & " --ON                                                                                 " & vbNewLine _
                                 & " --M_TCUST.USER_CD = TANTO_USER.USER_CD                                               " & vbNewLine _
                                 & " -- (2012.09.03) 要望番号1359 追加 --- START ---                                      " & vbNewLine _
                                 & " --AND M_TCUST.NRS_BR_CD = TANTO_USER.NRS_BR_CD                                       " & vbNewLine _
                                 & " -- (2012.09.03) 要望番号1359 追加 ---  END  ---                                      " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..S_USER               TANTO_USER                                             " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                                                  " & vbNewLine _
                                 & " -- 要望番号1756 yamanaka 2013.03.01 END                                              " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..S_USER               ENT_USER                                               " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.SYS_ENT_USER = ENT_USER.USER_CD                                              " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "$LM_MST$..S_USER               UPD_USER                                               " & vbNewLine _
                                 & "ON                                                                                    " & vbNewLine _
                                 & "B_INKA_L.SYS_UPD_USER = UPD_USER.USER_CD                                              " & vbNewLine _
                                 & "LEFT JOIN                                                                             " & vbNewLine _
                                 & "(SELECT                                                                               " & vbNewLine _
                                 & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
                                 & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
                                 & " ,SUM(INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU) AS INKA_TTL_NB                         " & vbNewLine _
                                 & " ,SUM(FLOOR((INKAS.KONSU * GOODS.PKG_NB + INKAS.HASU)                                 " & vbNewLine _
                                 & "      * INKAS.IRIME                                                                   " & vbNewLine _
                                 & "      * GOODS.STD_WT_KGS                                                              " & vbNewLine _
                                 & "      / GOODS.STD_IRIME_NB * 1000) / 1000)                 AS WT                      " & vbNewLine _
                                 & " FROM $LM_TRN$..B_INKA_L INKAL                                                        " & vbNewLine _
                                 & " LEFT JOIN $LM_TRN$..B_INKA_M INKAM                                                   " & vbNewLine _
                                 & " ON                                                                                   " & vbNewLine _
                                 & " INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                    " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                    " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " INKAM.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                 & " LEFT JOIN $LM_TRN$..B_INKA_S INKAS                                                   " & vbNewLine _
                                 & " ON                                                                                   " & vbNewLine _
                                 & " INKAS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                    " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                    " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " INKAS.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                    " & vbNewLine _
                                 & " ON                                                                                   " & vbNewLine _
                                 & " GOODS.NRS_BR_CD = INKAM.NRS_BR_CD                                                    " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                              " & vbNewLine _
                                 & " WHERE                                                                                " & vbNewLine _
                                 & " INKAL.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                 & " GROUP BY                                                                             " & vbNewLine _
                                 & "  INKAL.NRS_BR_CD                                                                     " & vbNewLine _
                                 & " ,INKAL.INKA_NO_L                                                                     " & vbNewLine _
                                 & " ) INKAL2                                                                             " & vbNewLine _
                                 & " ON                                                                                   " & vbNewLine _
                                 & " INKAL2.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                                " & vbNewLine _
                                 & " AND                                                                                  " & vbNewLine _
                                 & " INKAL2.INKA_NO_L = B_INKA_L.INKA_NO_L                                                " & vbNewLine _
                                 & " LEFT JOIN                                                                            " & vbNewLine _
                                 & "      (SELECT                                                                         " & vbNewLine _
                                 & "               KBN1.KBN_CD           AS KBN_CD                                        " & vbNewLine _
                                 & "            ,  " & " #KBN# " & "     AS KBN_NM1                                       " & vbNewLine _
                                 & "         FROM                                                                         " & vbNewLine _
                                 & "              $LM_MST$..Z_KBN AS KBN1                                                 " & vbNewLine _
                                 & "        WHERE                                                                         " & vbNewLine _
                                 & "              KBN1.KBN_GROUP_CD = 'W008'                                              " & vbNewLine _
                                 & "      ) AS WH_STATUS                                                                  " & vbNewLine _
                                 & "   ON WH_STATUS.KBN_CD       = B_INKA_L.WH_KENPIN_WK_STATUS                           " & vbNewLine _
                                 & "--2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen add start " & vbNewLine _
                                 & "LEFT JOIN (                                                                           " & vbNewLine _
                                 & "    SELECT                                                                            " & vbNewLine _
                                 & "		 NRS_BR_CD                                                                    " & vbNewLine _
                                 & "	    ,INKA_CTL_NO_L             AS INKA_CTL_NO_L                                   " & vbNewLine _
                                 & "		,COUNT(INKA_CTL_NO_L)      AS EDI_COUNT                                       " & vbNewLine _
                                 & "	FROM                                                                              " & vbNewLine _
                                 & "	    $LM_TRN$..H_INKAEDI_L                                                        " & vbNewLine _
                                 & "	WHERE                                                                             " & vbNewLine _
                                 & "	      SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                 & "	  AND NRS_BR_CD = @NRS_BR_CD   --UPD 2020/06/04 '10' 固定修正 013118                                                            " & vbNewLine _
                                 & "	  AND CUST_CD_L = '00010'                                                         " & vbNewLine _
                                 & "	GROUP BY NRS_BR_CD,INKA_CTL_NO_L                                                  " & vbNewLine _
                                 & "	) H_INKA_EDL_L                                                                    " & vbNewLine _
                                 & "ON H_INKA_EDL_L.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                        " & vbNewLine _
                                 & "AND H_INKA_EDL_L.INKA_CTL_NO_L = B_INKA_L.INKA_NO_L                                   " & vbNewLine _
                                 & "--2018/04/18 001528 【LMS】日立FN_EDI入荷登録時、入荷大でまとめる(千葉BC物管受注１松本) Annen add end " & vbNewLine _
                                 & "LEFT  JOIN GL_DB..VB_INKA_L AS WEB_INKA_L                                             " & vbNewLine _
                                 & "ON    WEB_INKA_L.NRS_BR_CD   = B_INKA_L.NRS_BR_CD                                     " & vbNewLine _
                                 & "AND   WEB_INKA_L.INKA_NO_L   = B_INKA_L.INKA_NO_L                                     " & vbNewLine _
                                 & "AND   WEB_INKA_L.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                 & " LEFT JOIN                                                                            " & vbNewLine _
                                 & "      (SELECT                                                                         " & vbNewLine _
                                 & "               KBN1.KBN_CD           AS KBN_CD                                        " & vbNewLine _
                                 & "            ,  " & " #KBN# " & "     AS KBN_NM1                                       " & vbNewLine _
                                 & "         FROM                                                                         " & vbNewLine _
                                 & "              $LM_MST$..Z_KBN AS KBN1                                                 " & vbNewLine _
                                 & "        WHERE                                                                         " & vbNewLine _
                                 & "              KBN1.KBN_GROUP_CD = 'S118'                                              " & vbNewLine _
                                 & "      ) AS WH_TAB_STATUS                                                              " & vbNewLine _
                                 & "   ON WH_TAB_STATUS.KBN_CD       = B_INKA_L.WH_TAB_STATUS                             " & vbNewLine _
                                 & " LEFT JOIN (                                                                          " & vbNewLine _
                                 & "     SELECT                                                                           " & vbNewLine _
                                 & "      INKL.NRS_BR_CD AS NRS_BR_CD                                                     " & vbNewLine _
                                 & "     ,INKL.INKA_NO_L AS INKA_NO_L                                                     " & vbNewLine _
                                 & "     ,CASE WHEN TABH.IN_KENPIN_LOC_STATE_KB IN ('00','01','02','03','04')             " & vbNewLine _
                                 & "	       AND  TABH.CANCEL_FLG = '01'              THEN '99'　                       " & vbNewLine _
                                 & "           WHEN TABH.IN_KENPIN_LOC_STATE_KB = '00'  THEN '00'                         " & vbNewLine _
                                 & "           WHEN TABH.IN_KENPIN_LOC_STATE_KB = '01'  THEN '01'                         " & vbNewLine _
                                 & "           WHEN TABH.IN_KENPIN_LOC_STATE_KB = '02'  THEN '02'                         " & vbNewLine _
                                 & "           WHEN TABH.IN_KENPIN_LOC_STATE_KB = '03'  THEN '03'                         " & vbNewLine _
                                 & "           WHEN TABH.IN_KENPIN_LOC_STATE_KB = '04'                                    " & vbNewLine _
                                 & "	        AND INKL.WH_TAB_IMP_YN = '00'           THEN '04'                         " & vbNewLine _
                                 & "	       WHEN TABH.IN_KENPIN_LOC_STATE_KB = '04'                                    " & vbNewLine _
                                 & "	        AND INKL.WH_TAB_IMP_YN = '01'           THEN '05'                         " & vbNewLine _
                                 & "	       ELSE ''                                                                    " & vbNewLine _
                                 & "      END                AS WH_TAB_WORK_STATUS                                        " & vbNewLine _
                                 & "     FROM $LM_TRN$..B_INKA_L INKL                                                     " & vbNewLine _
                                 & "     LEFT JOIN                                                                        " & vbNewLine _
                                 & "         (                                                                            " & vbNewLine _
                                 & "          SELECT                                                                      " & vbNewLine _
                                 & "            HED.NRS_BR_CD                                                             " & vbNewLine _
                                 & "           ,HED.INKA_NO_L                                                             " & vbNewLine _
                                 & "           ,HED.IN_KENPIN_LOC_STATE_KB                                                " & vbNewLine _
                                 & "           ,HED.CANCEL_FLG                                                            " & vbNewLine _
                                 & "	      FROM $LM_TRN$..TB_KENPIN_HEAD HED                                           " & vbNewLine _
                                 & " 	      INNER JOIN (                                                                " & vbNewLine _
                                 & "   	          SELECT NRS_BR_CD,INKA_NO_L,MAX(IN_KENPIN_LOC_SEQ)AS SEQ                 " & vbNewLine _
                                 & "		      FROM $LM_TRN$..TB_KENPIN_HEAD                                           " & vbNewLine _
                                 & "		      WHERE SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                 & "		      GROUP BY NRS_BR_CD,INKA_NO_L                                            " & vbNewLine _
                                 & "	          )MAX                                                                    " & vbNewLine _
                                 & "	      ON  HED.NRS_BR_CD = MAX.NRS_BR_CD                                           " & vbNewLine _
                                 & "	      AND HED.INKA_NO_L = MAX.INKA_NO_L                                           " & vbNewLine _
                                 & "	      AND HED.IN_KENPIN_LOC_SEQ = MAX.SEQ                                         " & vbNewLine _
                                 & "	     )TABH                                                                        " & vbNewLine _
                                 & "     ON  TABH.NRS_BR_CD = INKL.NRS_BR_CD                                              " & vbNewLine _
                                 & "     AND TABH.INKA_NO_L = INKL.INKA_NO_L                                              " & vbNewLine _
                                 & " )TAB_KBN                                                                             " & vbNewLine _
                                 & " ON  TAB_KBN.NRS_BR_CD = B_INKA_L.NRS_BR_CD                                           " & vbNewLine _
                                 & " AND TAB_KBN.INKA_NO_L = B_INKA_L.INKA_NO_L                                           " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..Z_KBN KBNT                                                       " & vbNewLine _
                                 & " ON  KBNT.KBN_GROUP_CD = 'S124'                                                       " & vbNewLine _
                                 & " AND KBNT.KBN_CD = TAB_KBN.WH_TAB_WORK_STATUS                                         " & vbNewLine

    '20160614 tsunehira add end


    ''' <summary>
    ''' データ抽出用WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String = "WHERE                                                                            " & vbNewLine _
                                      & "B_INKA_L.SYS_DEL_FLG  = '0'                                                      " & vbNewLine
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    'END YANAI 要望番号882
    'END YANAI メモ②No.28


    ''' <summary>
    ''' ORDER BY（①入荷日、②管理番号）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     B_INKA_L.INKA_DATE                                " & vbNewLine _
                                         & "    ,B_INKA_L.INKA_NO_L                                " & vbNewLine

    'START YANAI メモ②No.28
    ''' <summary>
    ''' H_INKAEDI_Lの入荷管理番号抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EDIL_DATA1 As String = " SELECT                                                                " & vbNewLine _
                                            & " EDI_L.NRS_BR_CD                                     AS NRS_BR_CD           " & vbNewLine _
                                            & ",EDI_L.INKA_CTL_NO_L                                 AS INKA_NO_L           " & vbNewLine

    ''' <summary>
    ''' H_INKAEDI_Lのオーダー番号抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EDIL_DATA2 As String = " SELECT                                                                " & vbNewLine _
                                            & " EDI_L.NRS_BR_CD                                     AS NRS_BR_CD            " & vbNewLine _
                                            & ",EDI_L.OUTKA_FROM_ORD_NO                             AS OUTKA_FROM_ORD_NO_L  " & vbNewLine

    ''' <summary>
    ''' H_INKAEDI_Lの抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_EDIL As String = "FROM                                                                         " & vbNewLine _
                                 & "$LM_TRN$..H_INKAEDI_L EDI_L                                                           " & vbNewLine _
                                 & "WHERE                                                                                 " & vbNewLine _
                                 & "EDI_L.SYS_DEL_FLG  = '0'                                                              " & vbNewLine

    ''' <summary>
    ''' B_INKA_S抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKAS_DATA As String = " SELECT                                                               " & vbNewLine _
                                            & " B_INKA_S.NRS_BR_CD                                  AS NRS_BR_CD           " & vbNewLine _
                                            & ",B_INKA_S.INKA_NO_L                                  AS INKA_NO_L           " & vbNewLine _
                                            & ",B_INKA_S.OFB_KB                                     AS OFB_KB              " & vbNewLine

    ''' <summary>
    ''' B_INKA_S抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_INKAS As String = "FROM                                                                         " & vbNewLine _
                                 & "$LM_TRN$..B_INKA_S                                                                     " & vbNewLine _
                                 & "WHERE                                                                                  " & vbNewLine _
                                 & "B_INKA_S.SYS_DEL_FLG  = '0'                                                            " & vbNewLine
    'END YANAI メモ②No.28

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 荷主明細マスタの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUSTDETAILS As String = " SELECT                                                                   " & vbNewLine _
                                               & " CUSTDETAILS.NRS_BR_CD                            AS NRS_BR_CD                " & vbNewLine _
                                               & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS                                    " & vbNewLine _
                                               & " WHERE                                                                        " & vbNewLine _
                                               & " CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " CUSTDETAILS.CUST_CD LIKE @CUST_CD_L                                          " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " CUSTDETAILS.SUB_KB = '12'                                                    " & vbNewLine

    'START YANAI 要望番号1056 作業一括作成時の不具合
    '''' <summary>
    '''' 作業レコードの検索 SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_SAGYODATA As String = " SELECT                                                                     " & vbNewLine _
    '                                           & " SAGYO.NRS_BR_CD                            AS NRS_BR_CD                      " & vbNewLine _
    '                                           & " FROM $LM_TRN$..E_SAGYO SAGYO                                                 " & vbNewLine _
    '                                           & " WHERE                                                                        " & vbNewLine _
    '                                           & " SAGYO.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
    '                                           & " AND                                                                          " & vbNewLine _
    '                                           & " SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9) = @INKA_NO_L                              " & vbNewLine _
    '                                           & " AND                                                                          " & vbNewLine _
    '                                           & " SAGYO.INOUTKA_NO_LM <> @INKA_NO_L + '000'                                    " & vbNewLine _
    '                                           & " AND                                                                          " & vbNewLine _
    '                                           & " SAGYO.SYS_DEL_FLG = '0'                                                      " & vbNewLine
    ''' <summary>
    ''' 作業レコードの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYODATA As String = " SELECT                                                                     " & vbNewLine _
                                               & " SAGYO.NRS_BR_CD                            AS NRS_BR_CD                      " & vbNewLine _
                                               & " FROM $LM_TRN$..E_SAGYO SAGYO                                                 " & vbNewLine _
                                               & " WHERE                                                                        " & vbNewLine _
                                               & " SAGYO.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " SUBSTRING(SAGYO.INOUTKA_NO_LM,1,9) = @INKA_NO_L                              " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " SAGYO.INOUTKA_NO_LM <> @INKA_NO_L + '000'                                    " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " SAGYO.IOZS_KB = '11'                                                         " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " SAGYO.SYS_DEL_FLG = '0'                                                      " & vbNewLine
    'END YANAI 要望番号1056 作業一括作成時の不具合

    ''' <summary>
    ''' 作成用、作業レコードの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO As String = " SELECT                                                                         " & vbNewLine _
                                               & " INKA_L.NRS_BR_CD                           AS NRS_BR_CD                      " & vbNewLine _
                                               & ",''                                         AS SAGYO_REC_NO                   " & vbNewLine _
                                               & ",CASE WHEN '50' = INKA_L.INKA_STATE_KB                                        " & vbNewLine _
                                               & "      THEN '01'                                                               " & vbNewLine _
                                               & "      WHEN '90' = INKA_L.INKA_STATE_KB                                        " & vbNewLine _
                                               & "      THEN '01'                                                               " & vbNewLine _
                                               & "      ELSE '00'                                                               " & vbNewLine _
                                               & " END                                        AS SAGYO_COMP                     " & vbNewLine _
                                               & ",'00'                                       AS SKYU_CHK                       " & vbNewLine _
                                               & ",''                                         AS SAGYO_SIJI_NO                  " & vbNewLine _
                                               & ",INKA_L.INKA_NO_L + INKA_M.INKA_NO_M        AS INOUTKA_NO_LM                  " & vbNewLine _
                                               & ",INKA_L.WH_CD                               AS WH_CD                          " & vbNewLine _
                                               & ",'11'                                       AS IOZS_KB                        " & vbNewLine _
                                               & ",GOODS.INKA_KAKO_SAGYO_KB_1                 AS SAGYO_CD1                      " & vbNewLine _
                                               & ",GOODS.INKA_KAKO_SAGYO_KB_2                 AS SAGYO_CD2                      " & vbNewLine _
                                               & ",GOODS.INKA_KAKO_SAGYO_KB_3                 AS SAGYO_CD3                      " & vbNewLine _
                                               & ",GOODS.INKA_KAKO_SAGYO_KB_4                 AS SAGYO_CD4                      " & vbNewLine _
                                               & ",GOODS.INKA_KAKO_SAGYO_KB_5                 AS SAGYO_CD5                      " & vbNewLine _
                                               & ",''                                         AS SAGYO_NM                       " & vbNewLine _
                                               & ",INKA_L.CUST_CD_L                           AS CUST_CD_L                      " & vbNewLine _
                                               & ",INKA_L.CUST_CD_M                           AS CUST_CD_M                      " & vbNewLine _
                                               & ",''                                         AS DEST_CD                        " & vbNewLine _
                                               & ",''                                         AS DEST_NM                        " & vbNewLine _
                                               & ",INKA_M.GOODS_CD_NRS                        AS GOODS_CD_NRS                   " & vbNewLine _
                                               & ",GOODS.GOODS_NM_1                           AS GOODS_NM_NRS                   " & vbNewLine _
                                               & ",ISNULL(INKA_S.LOT_NO,'')                   AS LOT_NO                         " & vbNewLine _
                                               & ",''                                         AS INV_TANI                       " & vbNewLine _
                                               & ",ISNULL(INKA_S.KONSU,'0') * GOODS.PKG_NB + ISNULL(INKA_S.HASU,'0') AS SAGYO_NB " & vbNewLine _
                                               & ",''                                         AS SAGYO_UP                       " & vbNewLine _
                                               & ",''                                         AS SAGYO_GK                       " & vbNewLine _
                                               & ",''                                         AS TAX_KB                         " & vbNewLine _
                                               & ",CUST.SAGYO_SEIQTO_CD                       AS SEIQTO_CD                      " & vbNewLine _
                                               & ",''                                         AS REMARK_ZAI                     " & vbNewLine _
                                               & ",INKA_L.OUTKA_FROM_ORD_NO_L                 AS REMARK_SKYU                    " & vbNewLine _
                                               & ",''                                         AS SAGYO_COMP_CD                  " & vbNewLine _
                                               & ",INKA_L.INKA_DATE                           AS SAGYO_COMP_DATE                " & vbNewLine _
                                               & ",'00'                                       AS DEST_SAGYO_FLG                 " & vbNewLine _
                                               & ",INKA_L.INKA_NO_L                           AS INKA_NO_L                      " & vbNewLine _
                                               & ",INKA_M.INKA_NO_M                           AS INKA_NO_M                      " & vbNewLine _
                                               & ",ISNULL(INKA_S.INKA_NO_S,'')                AS INKA_NO_S                      " & vbNewLine

    ''' <summary>
    ''' 作成用、作業レコードの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO As String = " FROM                                                                      " & vbNewLine _
                                               & "$LM_TRN$..B_INKA_L INKA_L                                                     " & vbNewLine _
                                               & "LEFT JOIN                                                                     " & vbNewLine _
                                               & "$LM_TRN$..B_INKA_M INKA_M                                                     " & vbNewLine _
                                               & "ON                                                                            " & vbNewLine _
                                               & "INKA_M.NRS_BR_CD = INKA_L.NRS_BR_CD                                           " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_M.INKA_NO_L = INKA_L.INKA_NO_L                                           " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_M.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                               & "LEFT JOIN                                                                     " & vbNewLine _
                                               & "$LM_TRN$..B_INKA_S INKA_S                                                     " & vbNewLine _
                                               & "ON                                                                            " & vbNewLine _
                                               & "INKA_S.NRS_BR_CD = INKA_M.NRS_BR_CD                                           " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_S.INKA_NO_L = INKA_M.INKA_NO_L                                           " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_S.INKA_NO_M = INKA_M.INKA_NO_M                                           " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_S.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                               & "LEFT JOIN                                                                     " & vbNewLine _
                                               & "$LM_MST$..M_GOODS GOODS                                                       " & vbNewLine _
                                               & "ON                                                                            " & vbNewLine _
                                               & "GOODS.NRS_BR_CD = INKA_M.NRS_BR_CD                                            " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "GOODS.GOODS_CD_NRS = INKA_M.GOODS_CD_NRS                                      " & vbNewLine _
                                               & "LEFT JOIN                                                                     " & vbNewLine _
                                               & "$LM_MST$..M_CUST CUST                                                         " & vbNewLine _
                                               & "ON                                                                            " & vbNewLine _
                                               & "CUST.NRS_BR_CD = GOODS.NRS_BR_CD                                              " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "CUST.CUST_CD_L = GOODS.CUST_CD_L                                              " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "CUST.CUST_CD_M = GOODS.CUST_CD_M                                              " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "CUST.CUST_CD_S = GOODS.CUST_CD_S                                              " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                            " & vbNewLine

    ''' <summary>
    ''' 作成用、作業レコードの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SAGYO As String = " WHERE                                                                    " & vbNewLine _
                                               & "INKA_L.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_L.INKA_NO_L = @INKA_NO_L                                                 " & vbNewLine _
                                               & "AND                                                                           " & vbNewLine _
                                               & "INKA_L.SYS_DEL_FLG = '0'                                                      " & vbNewLine

    ''' <summary>
    ''' 作成用、作業レコードの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SAGYO As String = " ORDER BY                                                                 " & vbNewLine _
                                               & " INKA_S.NRS_BR_CD                                                             " & vbNewLine _
                                               & ",INKA_S.INKA_NO_L                                                             " & vbNewLine _
                                               & ",INKA_S.INKA_NO_M                                                             " & vbNewLine _
                                               & ",INKA_S.INKA_NO_S                                                             " & vbNewLine

    ''' <summary>
    ''' 作業マスタの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYOMST As String = " SELECT                                                                      " & vbNewLine _
                                               & " SAGYO.NRS_BR_CD                           AS NRS_BR_CD                       " & vbNewLine _
                                               & ",SAGYO.SAGYO_NM                            AS SAGYO_NM                        " & vbNewLine _
                                               & ",SAGYO.INV_TANI                            AS INV_TANI                        " & vbNewLine _
                                               & ",SAGYO.KOSU_BAI                            AS KOSU_BAI                        " & vbNewLine _
                                               & ",SAGYO.SAGYO_UP                            AS SAGYO_UP                        " & vbNewLine _
                                               & ",SAGYO.ZEI_KBN                             AS TAX_KB                          " & vbNewLine _
                                               & " FROM $LM_MST$..M_SAGYO SAGYO                                                 " & vbNewLine _
                                               & " WHERE                                                                        " & vbNewLine _
                                               & " SAGYO.NRS_BR_CD = @NRS_BR_CD                                                 " & vbNewLine _
                                               & " AND                                                                          " & vbNewLine _
                                               & " SAGYO.SAGYO_CD = @SAGYO_CD                                                   " & vbNewLine

    'END YANAI 20120121 作業一括処理対応

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ作成用データの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAMAKEDATA_COUNT As String = " SELECT COUNT(ZAI.NRS_BR_CD)		            AS SELECT_CNT            " & vbNewLine

    ''' <summary>
    ''' 出荷データ作成用データの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAMAKEDATA As String = " SELECT                                                                      " & vbNewLine _
                                                     & " ZAI.NRS_BR_CD                             AS NRS_BR_CD                      " & vbNewLine _
                                                     & ",ZAI.WH_CD                                 AS WH_CD                          " & vbNewLine _
                                                     & ",ZAI.CUST_CD_L                             AS CUST_CD_L                      " & vbNewLine _
                                                     & ",ZAI.CUST_CD_M                             AS CUST_CD_M                      " & vbNewLine _
                                                     & ",ZAI.GOODS_CD_NRS                          AS GOODS_CD_NRS                   " & vbNewLine _
                                                     & ",ZAI.RSV_NO                                AS RSV_NO                         " & vbNewLine _
                                                     & ",ZAI.LOT_NO                                AS LOT_NO                         " & vbNewLine _
                                                     & ",ZAI.SERIAL_NO                             AS SERIAL_NO                      " & vbNewLine _
                                                     & ",ZAI.IRIME                                 AS IRIME                          " & vbNewLine _
                                                     & ",ZAI.ALLOC_CAN_NB                          AS ALLOC_CAN_NB                   " & vbNewLine _
                                                     & ",ZAI.ALLOC_CAN_QT                          AS ALLOC_CAN_QT                   " & vbNewLine _
                                                     & ",GOODS.GOODS_NM_1                          AS GOODS_NM                       " & vbNewLine _
                                                     & ",GOODS.PKG_NB                              AS PKG_NB                         " & vbNewLine _
                                                     & ",GOODS.STD_IRIME_UT                        AS IRIME_UT                       " & vbNewLine _
                                                     & ",GOODS.NB_UT                               AS NB_UT                          " & vbNewLine _
                                                     & ",GOODS.STD_WT_KGS                          AS STD_WT_KGS                     " & vbNewLine _
                                                     & ",GOODS.SEARCH_KEY_1                        AS SEARCH_KEY_1                   " & vbNewLine _
                                                     & ",Z1.KBN_CD                                 AS ZBUKA_CD                       " & vbNewLine _
                                                     & ",@OUTKA_DATE_INIT                          AS OUTKA_DATE_INIT                " & vbNewLine _
                                                     & ",@ROW_NO                                   AS ROW_NO                         " & vbNewLine

    ''' <summary>
    ''' 出荷データ作成用データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_OUTKAMAKEDATA As String = " FROM                                                                   " & vbNewLine _
                                                          & "$LM_TRN$..B_INKA_S INKAS                                                " & vbNewLine _
                                                          & "INNER JOIN                                                              " & vbNewLine _
                                                          & "$LM_TRN$..D_ZAI_TRS ZAI                                                 " & vbNewLine _
                                                          & "ON                                                                      " & vbNewLine _
                                                          & "ZAI.NRS_BR_CD = INKAS.NRS_BR_CD                                         " & vbNewLine _
                                                          & "AND                                                                     " & vbNewLine _
                                                          & "ZAI.ZAI_REC_NO = INKAS.ZAI_REC_NO                                       " & vbNewLine _
                                                          & "AND                                                                     " & vbNewLine _
                                                          & "ZAI.ALLOC_CAN_NB > 0                                                    " & vbNewLine _
                                                          & "AND                                                                     " & vbNewLine _
                                                          & "ZAI.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                          & "LEFT JOIN                                                               " & vbNewLine _
                                                          & "$LM_MST$..M_GOODS GOODS                                                 " & vbNewLine _
                                                          & "ON                                                                      " & vbNewLine _
                                                          & "GOODS.NRS_BR_CD = ZAI.NRS_BR_CD                                         " & vbNewLine _
                                                          & "AND                                                                     " & vbNewLine _
                                                          & "GOODS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                   " & vbNewLine _
                                                          & "LEFT JOIN                                                               " & vbNewLine _
                                                          & "$LM_MST$..Z_KBN Z1                                                      " & vbNewLine _
                                                          & "ON                                                                      " & vbNewLine _
                                                          & "Z1.KBN_NM1 = GOODS.NRS_BR_CD                                            " & vbNewLine _
                                                          & "AND                                                                     " & vbNewLine _
                                                          & "Z1.KBN_NM2 = GOODS.CUST_CD_L                                            " & vbNewLine _
                                                          & "AND                                                                     " & vbNewLine _
                                                          & "Z1.KBN_GROUP_CD = 'Z019'                                                " & vbNewLine


    ''' <summary>
    ''' 出荷データ作成用データの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_OUTKAMAKEDATA As String = " WHERE                                                                 " & vbNewLine _
                                                           & "INKAS.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine _
                                                           & "AND                                                                    " & vbNewLine _
                                                           & "INKAS.INKA_NO_L = @INKA_NO_L                                           " & vbNewLine _
                                                           & "AND                                                                    " & vbNewLine _
                                                           & "INKAS.SYS_DEL_FLG = '0'                                                " & vbNewLine
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'UTI追加修正 yamanaka 2012.12.21 Start
    ''' <summary>
    ''' UTI出荷データ作成用データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_UTI_OUTKAMAKEDATA_CNT As String = " FROM                                                                   " & vbNewLine _
                                                                  & "$LM_TRN$..B_INKA_L INKAL                                                " & vbNewLine _
                                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                                  & "$LM_TRN$..B_INKA_S INKAS                                                " & vbNewLine _
                                                                  & "ON                                                                      " & vbNewLine _
                                                                  & "INKAL.NRS_BR_CD = INKAS.NRS_BR_CD                                         " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "INKAL.INKA_NO_L = INKAS.INKA_NO_L                                       " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "INKAS.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                                  & "INNER JOIN                                                              " & vbNewLine _
                                                                  & "$LM_TRN$..D_ZAI_TRS ZAI                                                 " & vbNewLine _
                                                                  & "ON                                                                      " & vbNewLine _
                                                                  & "ZAI.NRS_BR_CD = INKAS.NRS_BR_CD                                         " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "ZAI.ZAI_REC_NO = INKAS.ZAI_REC_NO                                       " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "ZAI.ALLOC_CAN_NB > 0                                                    " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "ZAI.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                                  & "$LM_MST$..M_GOODS GOODS                                                 " & vbNewLine _
                                                                  & "ON                                                                      " & vbNewLine _
                                                                  & "GOODS.NRS_BR_CD = ZAI.NRS_BR_CD                                         " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "GOODS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                                   " & vbNewLine _
                                                                  & "LEFT JOIN                                                               " & vbNewLine _
                                                                  & "$LM_MST$..Z_KBN Z1                                                      " & vbNewLine _
                                                                  & "ON                                                                      " & vbNewLine _
                                                                  & "Z1.KBN_NM1 = GOODS.NRS_BR_CD                                            " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "Z1.KBN_NM2 = GOODS.CUST_CD_L                                            " & vbNewLine _
                                                                  & "AND                                                                     " & vbNewLine _
                                                                  & "Z1.KBN_GROUP_CD = 'Z019'                                                " & vbNewLine

    ''' <summary>
    ''' UTI出荷データ作成用データの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_UTI_OUTKAMAKEDATA As String = " WHERE                                                                 " & vbNewLine _
                                                               & "INKAL.NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine _
                                                               & "AND                                                                    " & vbNewLine _
                                                               & "INKAL.INKA_NO_L = @INKA_NO_L                                           " & vbNewLine _
                                                               & "AND                                                                    " & vbNewLine _
                                                               & "INKAL.INKA_STATE_KB = '50'                                             " & vbNewLine _
                                                               & "AND                                                                    " & vbNewLine _
                                                               & "INKAL.SYS_DEL_FLG = '0'                                                " & vbNewLine
    'UTI追加修正 yamanaka 2012.12.21 End

    ''' <summary>
    ''' UTI出荷データ作成用データの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAMAKEDATA2 As String = ",UNSOL.ORIG_CD                            AS ORIG_CD                        " & vbNewLine _
                                                      & ",CUST.SP_UNSO_CD                          AS SP_UNSO_CD                     " & vbNewLine _
                                                      & ",CUST.SP_UNSO_BR_CD                       AS SP_UNSO_BR_CD                  " & vbNewLine _
                                                      & ",DEST.DEST_CD                             AS DEST_CD                        " & vbNewLine _
                                                      & ",DEST.DEST_NM                             AS DEST_NM                        " & vbNewLine _
                                                      & ",DEST.AD_1                                AS AD_1                           " & vbNewLine _
                                                      & ",DEST.AD_2                                AS AD_2                           " & vbNewLine _
                                                      & ",DEST.AD_3                                AS AD_3                           " & vbNewLine _
                                                      & ",GOODS.PKG_UT                             AS PKG_UT                         " & vbNewLine


    ''' <summary>
    ''' UTI出荷データ作成用データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_UTI_OUTKAMAKEDATA As String = " FROM                                                               " & vbNewLine _
                                                              & "$LM_TRN$..B_INKA_S INKAS                                            " & vbNewLine _
                                                              & "INNER JOIN                                                          " & vbNewLine _
                                                              & "$LM_TRN$..D_ZAI_TRS ZAI                                             " & vbNewLine _
                                                              & "ON                                                                  " & vbNewLine _
                                                              & "ZAI.NRS_BR_CD = INKAS.NRS_BR_CD                                     " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "ZAI.ZAI_REC_NO = INKAS.ZAI_REC_NO                                   " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "ZAI.ALLOC_CAN_NB > 0                                                " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "ZAI.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                                              & "LEFT JOIN                                                           " & vbNewLine _
                                                              & "$LM_TRN$..F_UNSO_L UNSOL                                            " & vbNewLine _
                                                              & "ON                                                                  " & vbNewLine _
                                                              & "INKAS.NRS_BR_CD = UNSOL.NRS_BR_CD                                   " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "INKAS.INKA_NO_L = UNSOL.INOUTKA_NO_L                                " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "UNSOL.SYS_DEL_FLG = 0                                               " & vbNewLine _
                                                              & "   AND                                                              " & vbNewLine _
                                                              & " UNSOL.MOTO_DATA_KB = '10'                                          " & vbNewLine _
                                                              & "LEFT JOIN                                                           " & vbNewLine _
                                                              & "$LM_MST$..M_GOODS GOODS                                             " & vbNewLine _
                                                              & "ON                                                                  " & vbNewLine _
                                                              & "GOODS.NRS_BR_CD = ZAI.NRS_BR_CD                                     " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "GOODS.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                               " & vbNewLine _
                                                              & "LEFT JOIN                                                           " & vbNewLine _
                                                              & "$LM_MST$..M_CUST CUST                                               " & vbNewLine _
                                                              & "ON                                                                  " & vbNewLine _
                                                              & "GOODS.NRS_BR_CD = CUST.NRS_BR_CD                                    " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "GOODS.CUST_CD_L = CUST.CUST_CD_L                                    " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "GOODS.CUST_CD_M = CUST.CUST_CD_M                                    " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "GOODS.CUST_CD_S = CUST.CUST_CD_S                                    " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "GOODS.CUST_CD_SS = CUST.CUST_CD_SS                                  " & vbNewLine _
                                                              & "LEFT JOIN                                                           " & vbNewLine _
                                                              & "$LM_MST$..M_DEST DEST                                               " & vbNewLine _
                                                              & "ON                                                                  " & vbNewLine _
                                                              & "CUST.NRS_BR_CD = DEST.NRS_BR_CD                                     " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "CUST.CUST_CD_L = DEST.CUST_CD_L                                     " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "DEST.DEST_CD = 'ﾊﾞﾝﾆﾝｸﾞ'                                            " & vbNewLine _
                                                              & "LEFT JOIN                                                           " & vbNewLine _
                                                              & "$LM_MST$..Z_KBN Z1                                                  " & vbNewLine _
                                                              & "ON                                                                  " & vbNewLine _
                                                              & "Z1.KBN_NM1 = GOODS.NRS_BR_CD                                        " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "Z1.KBN_NM2 = GOODS.CUST_CD_L                                        " & vbNewLine _
                                                              & "AND                                                                 " & vbNewLine _
                                                              & "Z1.KBN_GROUP_CD = 'Z019'                                            " & vbNewLine

    ''' <summary>
    ''' UTI入荷報告取り消しのカウント SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UTI_OUTKA_DATA_COUNT_FOR_CANCEL As String = "SELECT COUNT(OL.NRS_BR_CD) AS SELECT_CNT                                                                          " & vbNewLine _
                                                                        & "FROM $LM_TRN$..C_OUTKA_L OL                                                               " & vbNewLine _
                                                                        & "left join  $LM_TRN$..C_OUTKA_M OM                                                         " & vbNewLine _
                                                                        & "  on OL.NRS_BR_CD = OM.NRS_BR_CD                                                          " & vbNewLine _
                                                                        & "  AND OL.OUTKA_NO_L = OM.OUTKA_NO_L                                                       " & vbNewLine _
                                                                        & "left join                                                                                 " & vbNewLine _
                                                                        & "  (SELECT INL.NRS_BR_CD,INL.INKA_NO_L,INL.CUST_CD_L,INL.CUST_CD_M,INS.SERIAL_NO           " & vbNewLine _
                                                                        & "  FROM                                                                                    " & vbNewLine _
                                                                        & "   $LM_TRN$..B_INKA_L INL                                                                 " & vbNewLine _
                                                                        & "  left join $LM_TRN$..B_INKA_M INM                                                        " & vbNewLine _
                                                                        & "  on INL.NRS_BR_CD = INM.NRS_BR_CD                                                        " & vbNewLine _
                                                                        & "  AND INL.INKA_NO_L = INM.INKA_NO_L                                                       " & vbNewLine _
                                                                        & "  left join $LM_TRN$..B_INKA_S INS                                                        " & vbNewLine _
                                                                        & "  on INM.NRS_BR_CD = INS.NRS_BR_CD                                                        " & vbNewLine _
                                                                        & "  AND INM.INKA_NO_L = INS.INKA_NO_L                                                       " & vbNewLine _
                                                                        & "  AND INM.INKA_NO_M = INS.INKA_NO_M                                                       " & vbNewLine _
                                                                        & "  WHERE                                                                                   " & vbNewLine _
                                                                        & "  INL.NRS_BR_CD = @NRS_BR_CD                                                              " & vbNewLine _
                                                                        & "  AND INL.INKA_NO_L  = @INKA_NO_L                                                         " & vbNewLine _
                                                                        & "  AND INL.SYS_DEL_FLG = '0' AND INM.SYS_DEL_FLG = '0' AND INS.SYS_DEL_FLG = '0'           " & vbNewLine _
                                                                        & "  ) INKA                                                                                  " & vbNewLine _
                                                                        & "  on OL.NRS_BR_CD = INKA.NRS_BR_CD                                                        " & vbNewLine _
                                                                        & "  AND OL.CUST_CD_L = INKA.CUST_CD_L                                                       " & vbNewLine _
                                                                        & "  AND OL.CUST_CD_M = INKA.CUST_CD_M                                                       " & vbNewLine _
                                                                        & "  AND OM.SERIAL_NO = INKA.SERIAL_NO                                                       " & vbNewLine _
                                                                        & "where                                                                                     " & vbNewLine _
                                                                        & "  INKA.NRS_BR_CD = @NRS_BR_CD                                                             " & vbNewLine _
                                                                        & "  AND INKA.INKA_NO_L  = @INKA_NO_L                                                        " & vbNewLine _
                                                                        & "  AND OL.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
                                                                        & "  AND OM.SYS_DEL_FLG = '0'                                                                " & vbNewLine


    'WIT対応 入荷データ一括取込対応 kasama Start
#Region "入荷データ一括取込対応"

    ''' <summary>
    ''' 荷主明細マスタの検索 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUST_DETAIL As String = _
          " SELECT                                                  " & vbNewLine _
        & "     CUSTDETAILS.SET_NAIYO                               " & vbNewLine _
        & "     ,CUSTDETAILS.SET_NAIYO_2                            " & vbNewLine _
        & "     ,CUSTDETAILS.SET_NAIYO_3                            " & vbNewLine _
        & " FROM $LM_MST$..M_CUST_DETAILS CUSTDETAILS               " & vbNewLine _
        & " WHERE                                                   " & vbNewLine _
        & " CUSTDETAILS.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
        & " AND                                                     " & vbNewLine _
        & " CUSTDETAILS.CUST_CD LIKE @CUST_CD                       " & vbNewLine _
        & " AND                                                     " & vbNewLine _
        & " CUSTDETAILS.SUB_KB = @SUB_KB                            " & vbNewLine


    ''' <summary>
    ''' 取込用元データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TORIKOMI_SRC As String = _
                  "SELECT                                           " & vbNewLine _
                & "      WK.NRS_BR_CD                               " & vbNewLine _
                & "    , WK.INKA_NO_L                               " & vbNewLine _
                & "    , WK.INKA_NO_M                               " & vbNewLine _
                & "    , WK.SEQ                                     " & vbNewLine _
                & "    , WK.WH_CD                                   " & vbNewLine _
                & "    , ISNULL(KBN_LOCA.KBN_NM3, '') AS TOU_NO     " & vbNewLine _
                & "    , ISNULL(KBN_LOCA.KBN_NM4, '') AS SITU_NO    " & vbNewLine _
                & "    , ISNULL(KBN_LOCA.KBN_NM5, '') AS ZONE_CD    " & vbNewLine _
                & "    , ISNULL(KBN_LOCA.KBN_NM6, '') AS LOCA       " & vbNewLine _
                & "    , L.CUST_CD_L                                " & vbNewLine _
                & "    , L.CUST_CD_M                                " & vbNewLine _
                & "    , WK.GOODS_KANRI_NO                          " & vbNewLine _
                & "    , GOODS.GOODS_CD_NRS                         " & vbNewLine _
                & "    , GOODS.STD_IRIME_NB                         " & vbNewLine _
                & "    , GOODS.PKG_NB                               " & vbNewLine _
                & "    , GOODS.STD_WT_KGS                           " & vbNewLine _
                & "    , L.HOKAN_YN                                 " & vbNewLine _
                & "    , L.TAX_KB                                   " & vbNewLine _
                & "    , WK.INKA_NB                                 " & vbNewLine _
                & "    , WK.NYUKOZUMI_NB                            " & vbNewLine _
                & "    , ''                           AS LOT_NO     " & vbNewLine _
                & "    , ''                           AS SET_NAIYO  " & vbNewLine _
                & "    , WK.INKA_NB                   AS EDI_NB     " & vbNewLine _
                & "    , WK.INKA_NB                   AS SUM_INKA_NB " & vbNewLine _
                & "FROM                                             " & vbNewLine _
                & "    $LM_TRN$..B_INKA_WK WK                       " & vbNewLine _
                & "LEFT OUTER JOIN                                  " & vbNewLine _
                & "    $LM_TRN$..B_INKA_L L                         " & vbNewLine _
                & "ON                                               " & vbNewLine _
                & "        WK.NRS_BR_CD = L.NRS_BR_CD               " & vbNewLine _
                & "    AND WK.INKA_NO_L = L.INKA_NO_L               " & vbNewLine _
                & "    AND L.SYS_DEL_FLG = '0'                      " & vbNewLine _
                & "LEFT OUTER JOIN                                  " & vbNewLine _
                & "    $LM_TRN$..B_INKA_M M                         " & vbNewLine _
                & "ON                                               " & vbNewLine _
                & "        WK.NRS_BR_CD = M.NRS_BR_CD               " & vbNewLine _
                & "    AND WK.INKA_NO_L = M.INKA_NO_L               " & vbNewLine _
                & "    AND WK.INKA_NO_M = M.INKA_NO_M               " & vbNewLine _
                & "    AND M.SYS_DEL_FLG = '0'                      " & vbNewLine _
                & "LEFT OUTER JOIN                                  " & vbNewLine _
                & "    $LM_MST$..M_GOODS GOODS                      " & vbNewLine _
                & "ON                                               " & vbNewLine _
                & "        M.NRS_BR_CD = GOODS.NRS_BR_CD            " & vbNewLine _
                & "    AND M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS      " & vbNewLine _
                & "    AND M.SYS_DEL_FLG = '0'                      " & vbNewLine _
                & "LEFT OUTER JOIN                                  " & vbNewLine _
                & "    $LM_MST$..Z_KBN KBN_LOCA                     " & vbNewLine _
                & "ON                                               " & vbNewLine _
                & "        KBN_LOCA.KBN_GROUP_CD = 'L005'           " & vbNewLine _
                & "    AND KBN_LOCA.KBN_CD = L.NRS_BR_CD            " & vbNewLine _
                & "    AND KBN_LOCA.KBN_NM1 = L.CUST_CD_L           " & vbNewLine _
                & "    AND KBN_LOCA.KBN_NM2 = L.CUST_CD_M           " & vbNewLine _
                & "    AND KBN_LOCA.SYS_DEL_FLG = '0'               " & vbNewLine _
                & "WHERE                                            " & vbNewLine _
                & "        WK.SYS_DEL_FLG = '0'                     " & vbNewLine _
                & "    AND WK.NYUKO_KAKUTEI_FLG = '00'              " & vbNewLine _
                & "    AND WK.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
                & "    AND WK.INKA_NO_L = @INKA_NO_L                " & vbNewLine _
                & "                                                 " & vbNewLine


    '2014.06.06 FFEM対応 追加START
    ''' <summary>
    ''' FFEM取込用元データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TORIKOMI_FFEM_SRC As String = _
                                "SELECT                                              " & vbNewLine _
                              & "      WK.NRS_BR_CD                                  " & vbNewLine _
                              & "    , WK.INKA_NO_L                                  " & vbNewLine _
                              & "    , WK.INKA_NO_M                                  " & vbNewLine _
                              & "    , ''             AS SEQ                         " & vbNewLine _
                              & "    , WK.WH_CD                                      " & vbNewLine _
                              & "    , WK.TOU_NO                                     " & vbNewLine _
                              & "    , WK.SITU_NO                                    " & vbNewLine _
                              & "    , WK.ZONE_CD                                    " & vbNewLine _
                              & "    , WK.LOCA                                       " & vbNewLine _
                              & "    , L.CUST_CD_L                                   " & vbNewLine _
                              & "    , L.CUST_CD_M                                   " & vbNewLine _
                              & "    --, WK.GOODS_KANRI_NO 041007在庫まとめのため削除" & vbNewLine _
                              & "    , ''             AS GOODS_KANRI_NO              " & vbNewLine _
                              & "    , GOODS.GOODS_CD_NRS                            " & vbNewLine _
                              & "    , GOODS.STD_IRIME_NB                            " & vbNewLine _
                              & "    , GOODS.PKG_NB                                  " & vbNewLine _
                              & "    , GOODS.STD_WT_KGS                              " & vbNewLine _
                              & "    , L.HOKAN_YN                                    " & vbNewLine _
                              & "    , L.TAX_KB                                      " & vbNewLine _
                              & "    , SUM(WK.INKA_NB)      AS INKA_NB               " & vbNewLine _
                              & "    , SUM(WK.NYUKOZUMI_NB) AS NYUKOZUMI_NB          " & vbNewLine _
                              & "    , WK.LOT_NO                                     " & vbNewLine _
                              & "    , ISNULL(MCD.SET_NAIYO,'00') AS SET_NAIYO       " & vbNewLine _
                              & "    , M.NB                 AS EDI_NB                " & vbNewLine _
                              & " 	,ISNULL((SELECT                                  " & vbNewLine _
                              & "                      SUM(INKA_NB)                  " & vbNewLine _
                              & "					  FROM $LM_TRN$..B_INKA_WK       " & vbNewLine _
                              & "				   WHERE                             " & vbNewLine _
                              & "						NRS_BR_CD  = @NRS_BR_CD      " & vbNewLine _
                              & "                    AND INKA_NO_L = @INKA_NO_L      " & vbNewLine _
                              & "                    AND NRS_BR_CD = M.NRS_BR_CD     " & vbNewLine _
                              & "                    AND INKA_NO_L = M.INKA_CTL_NO_L " & vbNewLine _
                              & "                   AND  INKA_NO_M = M.INKA_CTL_NO_M " & vbNewLine _
                              & "                   AND NYUKO_KAKUTEI_FLG = '00'     " & vbNewLine _
                              & "                   AND SYS_DEL_FLG = '0'            " & vbNewLine _
                              & "				   GROUP BY                          " & vbNewLine _
                              & "					  NRS_BR_CD                      " & vbNewLine _
                              & "                     , INKA_NO_L                    " & vbNewLine _
                              & "                     , INKA_NO_M                    " & vbNewLine _
                              & "					 FOR XML PATH('')                " & vbNewLine _
                              & "                   ),0) AS SUM_INKA_NB              " & vbNewLine _
                              & "FROM                                                " & vbNewLine _
                              & "    $LM_TRN$..H_INKAEDI_M M                         " & vbNewLine _
                              & "LEFT OUTER JOIN                                     " & vbNewLine _
                              & "    $LM_TRN$..B_INKA_L L                            " & vbNewLine _
                              & "ON                                                  " & vbNewLine _
                              & "        M.NRS_BR_CD = L.NRS_BR_CD                   " & vbNewLine _
                              & "    AND M.INKA_CTL_NO_L = L.INKA_NO_L               " & vbNewLine _
                              & "    AND L.SYS_DEL_FLG = '0'                         " & vbNewLine _
                              & "LEFT OUTER JOIN                                     " & vbNewLine _
                              & "    $LM_TRN$..B_INKA_WK WK                          " & vbNewLine _
                              & "ON                                                  " & vbNewLine _
                              & "        M.NRS_BR_CD  = WK.NRS_BR_CD                 " & vbNewLine _
                              & "    AND M.INKA_CTL_NO_L = WK.INKA_NO_L              " & vbNewLine _
                              & "    AND M.INKA_CTL_NO_M = WK.INKA_NO_M              " & vbNewLine _
                              & "    AND WK.NYUKO_KAKUTEI_FLG = '00'                 " & vbNewLine _
                              & "    AND WK.SYS_DEL_FLG = '0'                        " & vbNewLine _
                              & "LEFT OUTER JOIN                                     " & vbNewLine _
                              & "    $LM_MST$..M_GOODS GOODS                         " & vbNewLine _
                              & "ON                                                  " & vbNewLine _
                              & "        M.NRS_BR_CD = GOODS.NRS_BR_CD               " & vbNewLine _
                              & "    AND M.NRS_GOODS_CD = GOODS.GOODS_CD_NRS         " & vbNewLine _
                              & "    AND M.SYS_DEL_FLG = '0'                         " & vbNewLine _
                              & "LEFT OUTER JOIN                                     " & vbNewLine _
                              & "    LM_MST..M_CUST_DETAILS MCD                      " & vbNewLine _
                              & "ON                                                  " & vbNewLine _
                              & "        L.NRS_BR_CD = MCD.NRS_BR_CD                 " & vbNewLine _
                              & "    AND L.CUST_CD_L + L.CUST_CD_M = MCD.CUST_CD     " & vbNewLine _
                              & "    AND MCD.SUB_KB = '72'                           " & vbNewLine _
                              & "    AND MCD.SYS_DEL_FLG = '0'                       " & vbNewLine _
                              & "WHERE                                               " & vbNewLine _
                              & "        M.SYS_DEL_FLG = '0'                         " & vbNewLine _
                              & "    AND M.NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                              & "    AND M.INKA_CTL_NO_L = @INKA_NO_L                " & vbNewLine _
                              & "GROUP BY                                            " & vbNewLine _
                              & "      WK.NRS_BR_CD                                  " & vbNewLine _
                              & "    , WK.INKA_NO_L                                  " & vbNewLine _
                              & "    , WK.INKA_NO_M                                  " & vbNewLine _
                              & "    , WK.WH_CD                                      " & vbNewLine _
                              & "    , WK.TOU_NO                                     " & vbNewLine _
                              & "    , WK.SITU_NO                                    " & vbNewLine _
                              & "    , WK.ZONE_CD                                    " & vbNewLine _
                              & "    , WK.LOCA                                       " & vbNewLine _
                              & "    , L.CUST_CD_L                                   " & vbNewLine _
                              & "    , L.CUST_CD_M                                   " & vbNewLine _
                              & "    --, WK.GOODS_KANRI_NO 2014.10.06 削除           " & vbNewLine _
                              & "    , GOODS.GOODS_CD_NRS                            " & vbNewLine _
                              & "    , GOODS.STD_IRIME_NB                            " & vbNewLine _
                              & "    , GOODS.PKG_NB                                  " & vbNewLine _
                              & "    , GOODS.STD_WT_KGS                              " & vbNewLine _
                              & "    , L.HOKAN_YN                                    " & vbNewLine _
                              & "    , L.TAX_KB                                      " & vbNewLine _
                              & "    , WK.LOT_NO                                     " & vbNewLine _
                              & "    , MCD.SET_NAIYO                                 " & vbNewLine _
                              & "    , M.NB                                          " & vbNewLine _
                              & "    , M.NRS_BR_CD                                   " & vbNewLine _
                              & "    , M.INKA_CTL_NO_L                               " & vbNewLine _
                              & "    , M.INKA_CTL_NO_M                               " & vbNewLine
    'Private Const SQL_SELECT_TORIKOMI_FFEM_SRC As String = _
    '  "SELECT                                           " & vbNewLine _
    '& "      WK.NRS_BR_CD                               " & vbNewLine _
    '& "    , WK.INKA_NO_L                               " & vbNewLine _
    '& "    , WK.INKA_NO_M                               " & vbNewLine _
    '& "    , ''             AS SEQ                      " & vbNewLine _
    '& "    , WK.WH_CD                                   " & vbNewLine _
    '& "    , WK.TOU_NO                                  " & vbNewLine _
    '& "    , WK.SITU_NO                                 " & vbNewLine _
    '& "    , WK.ZONE_CD                                 " & vbNewLine _
    '& "    , WK.LOCA                                    " & vbNewLine _
    '& "    , L.CUST_CD_L                                " & vbNewLine _
    '& "    , L.CUST_CD_M                                " & vbNewLine _
    '& "    , WK.GOODS_KANRI_NO                          " & vbNewLine _
    '& "    , GOODS.GOODS_CD_NRS                         " & vbNewLine _
    '& "    , GOODS.STD_IRIME_NB                         " & vbNewLine _
    '& "    , GOODS.PKG_NB                               " & vbNewLine _
    '& "    , GOODS.STD_WT_KGS                           " & vbNewLine _
    '& "    , L.HOKAN_YN                                 " & vbNewLine _
    '& "    , L.TAX_KB                                   " & vbNewLine _
    '& "    , SUM(WK.INKA_NB)      AS INKA_NB            " & vbNewLine _
    '& "    , SUM(WK.NYUKOZUMI_NB) AS NYUKOZUMI_NB       " & vbNewLine _
    '& "    , WK.LOT_NO                                  " & vbNewLine _
    '& "    , ISNULL(MCD.SET_NAIYO,'00') AS SET_NAIYO    " & vbNewLine _
    '& "FROM                                             " & vbNewLine _
    '& "    $LM_TRN$..B_INKA_WK WK                       " & vbNewLine _
    '& "LEFT OUTER JOIN                                  " & vbNewLine _
    '& "    $LM_TRN$..B_INKA_L L                         " & vbNewLine _
    '& "ON                                               " & vbNewLine _
    '& "        WK.NRS_BR_CD = L.NRS_BR_CD               " & vbNewLine _
    '& "    AND WK.INKA_NO_L = L.INKA_NO_L               " & vbNewLine _
    '& "    AND L.SYS_DEL_FLG = '0'                      " & vbNewLine _
    '& "LEFT OUTER JOIN                                  " & vbNewLine _
    '& "    $LM_TRN$..B_INKA_M M                         " & vbNewLine _
    '& "ON                                               " & vbNewLine _
    '& "        WK.NRS_BR_CD = M.NRS_BR_CD               " & vbNewLine _
    '& "    AND WK.INKA_NO_L = M.INKA_NO_L               " & vbNewLine _
    '& "    AND WK.INKA_NO_M = M.INKA_NO_M               " & vbNewLine _
    '& "    AND M.SYS_DEL_FLG = '0'                      " & vbNewLine _
    '& "LEFT OUTER JOIN                                  " & vbNewLine _
    '& "    $LM_MST$..M_GOODS GOODS                      " & vbNewLine _
    '& "ON                                               " & vbNewLine _
    '& "        M.NRS_BR_CD = GOODS.NRS_BR_CD            " & vbNewLine _
    '& "    AND M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS      " & vbNewLine _
    '& "    AND M.SYS_DEL_FLG = '0'                      " & vbNewLine _
    '& "LEFT OUTER JOIN                                  " & vbNewLine _
    '& "    $LM_MST$..M_CUST_DETAILS MCD                 " & vbNewLine _
    '& "ON                                               " & vbNewLine _
    '& "        L.NRS_BR_CD = MCD.NRS_BR_CD              " & vbNewLine _
    '& "    AND L.CUST_CD_L + L.CUST_CD_M = MCD.CUST_CD  " & vbNewLine _
    '& "    AND MCD.SUB_KB = '72'                        " & vbNewLine _
    '& "    AND MCD.SYS_DEL_FLG = '0'                    " & vbNewLine _
    '& "WHERE                                            " & vbNewLine _
    '& "        WK.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '& "    AND WK.NYUKO_KAKUTEI_FLG = '00'              " & vbNewLine _
    '& "    AND WK.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
    '& "    AND WK.INKA_NO_L = @INKA_NO_L                " & vbNewLine _
    '& "GROUP BY                                         " & vbNewLine _
    '& "      WK.NRS_BR_CD                               " & vbNewLine _
    '& "    , WK.INKA_NO_L                               " & vbNewLine _
    '& "    , WK.INKA_NO_M                               " & vbNewLine _
    '& "    , WK.WH_CD                                   " & vbNewLine _
    '& "    , WK.TOU_NO                                  " & vbNewLine _
    '& "    , WK.SITU_NO                                 " & vbNewLine _
    '& "    , WK.ZONE_CD                                 " & vbNewLine _
    '& "    , WK.LOCA                                    " & vbNewLine _
    '& "    , L.CUST_CD_L                                " & vbNewLine _
    '& "    , L.CUST_CD_M                                " & vbNewLine _
    '& "    , WK.GOODS_KANRI_NO                          " & vbNewLine _
    '& "    , GOODS.GOODS_CD_NRS                         " & vbNewLine _
    '& "    , GOODS.STD_IRIME_NB                         " & vbNewLine _
    '& "    , GOODS.PKG_NB                               " & vbNewLine _
    '& "    , GOODS.STD_WT_KGS                           " & vbNewLine _
    '& "    , L.HOKAN_YN                                 " & vbNewLine _
    '& "    , L.TAX_KB                                   " & vbNewLine _
    '& "    , WK.LOT_NO                                  " & vbNewLine _
    '& "    , MCD.SET_NAIYO                              " & vbNewLine
    '2014.06.06 FFEM対応 追加END


    ''' <summary>
    ''' B_INKA_WK 検品過小件数取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_B_INKA_WK As String = _
                                "SELECT                                         " & vbNewLine _
                              & "     COUNT(MAIN.INKA_NO_L) AS REC_CNT          " & vbNewLine _
                              & "FROM                                           " & vbNewLine _
                              & "(                                              " & vbNewLine _
                              & "SELECT                                         " & vbNewLine _
                              & "	 BIW.NRS_BR_CD                              " & vbNewLine _
                              & "	,BIW.INKA_NO_L                              " & vbNewLine _
                              & "	,BIW.INKA_NO_M                              " & vbNewLine _
                              & "	,HIM.NB                                     " & vbNewLine _
                              & "    ,SUM(ISNULL(BIW.INKA_NB,0)) AS KENPIN_NB   " & vbNewLine _
                              & "FROM                                           " & vbNewLine _
                              & "    $LM_TRN$..H_INKAEDI_M HIM                  " & vbNewLine _
                              & "	LEFT JOIN                                   " & vbNewLine _
                              & "	$LM_TRN$..B_INKA_M BM                       " & vbNewLine _
                              & "	ON                                          " & vbNewLine _
                              & "	HIM.NRS_BR_CD = BM.NRS_BR_CD                " & vbNewLine _
                              & "	AND                                         " & vbNewLine _
                              & "	HIM.INKA_CTL_NO_L = BM.INKA_NO_L            " & vbNewLine _
                              & "	AND                                         " & vbNewLine _
                              & "	HIM.INKA_CTL_NO_M = BM.INKA_NO_M            " & vbNewLine _
                              & "	LEFT JOIN                                   " & vbNewLine _
                              & "	$LM_TRN$..B_INKA_WK BIW                     " & vbNewLine _
                              & "	ON                                          " & vbNewLine _
                              & "	BM.NRS_BR_CD = BIW.NRS_BR_CD                " & vbNewLine _
                              & "	AND                                         " & vbNewLine _
                              & "	BM.INKA_NO_L = BIW.INKA_NO_L                " & vbNewLine _
                              & "	AND                                         " & vbNewLine _
                              & "	BM.INKA_NO_M = BIW.INKA_NO_M                " & vbNewLine _
                              & "                                               " & vbNewLine _
                              & "WHERE                                          " & vbNewLine _
                              & "        BIW.NRS_BR_CD         = @NRS_BR_CD     " & vbNewLine _
                              & "    AND BIW.INKA_NO_L         = @INKA_NO_L     " & vbNewLine _
                              & "    AND BIW.NYUKO_KAKUTEI_FLG = '00'           " & vbNewLine _
                              & "    AND BIW.SYS_DEL_FLG       = '0'            " & vbNewLine _
                              & "    AND HIM.SYS_DEL_FLG       = '0'            " & vbNewLine _
                              & "    AND BM.SYS_DEL_FLG        = '0'            " & vbNewLine _
                              & "	GROUP BY                                    " & vbNewLine _
                              & "	 BIW.NRS_BR_CD                              " & vbNewLine _
                              & "	,BIW.INKA_NO_L                              " & vbNewLine _
                              & "	,BIW.INKA_NO_M                              " & vbNewLine _
                              & "	,HIM.NB                                     " & vbNewLine _
                              & "	HAVING                                      " & vbNewLine _
                              & "	HIM.NB <> SUM(BIW.INKA_NB)                  " & vbNewLine _
                              & ") MAIN                                         " & vbNewLine



    ''' <summary>
    ''' INKA_L件数取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_INKA_L As String = _
          "SELECT                                          " & vbNewLine _
        & "    COUNT(INKA_NO_L) AS REC_CNT                 " & vbNewLine _
        & "FROM                                            " & vbNewLine _
        & "    $LM_TRN$..B_INKA_L                          " & vbNewLine _
        & "WHERE                                           " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
        & "    AND INKA_NO_L = @INKA_NO_L                  " & vbNewLine _
        & "    AND SYS_UPD_DATE = @SYS_UPD_DATE            " & vbNewLine _
        & "    AND SYS_UPD_TIME = @SYS_UPD_TIME            " & vbNewLine

    ''' <summary>
    ''' INKA_NO_S最大値取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAX_INKA_NO_S As String = _
          "SELECT                                          " & vbNewLine _
        & "    ISNULL(MAX(INKA_NO_S), 0) AS MAX_INKA_NO_S  " & vbNewLine _
        & "FROM                                            " & vbNewLine _
        & "    $LM_TRN$..B_INKA_S                          " & vbNewLine _
        & "WHERE                                           " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
        & "    AND INKA_NO_L = @INKA_NO_L                  " & vbNewLine _
        & "    AND INKA_NO_M = @INKA_NO_M                  " & vbNewLine

#End Region
    'WIT対応 入荷データ一括取込対応 kasama End

#End Region

#Region "INSERT"

#Region "H_INKAEDI_HED_DPN"

    'START YANAI 要望番号898
    '''' <summary>
    '''' INSERT（H_INKAEDI_HED_DPN）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT_HED_DPN As String = "INSERT INTO $LM_TRN$..H_INKAEDI_HED_DPN                                                              " & vbNewLine _
    '                                            & "SELECT                                                                                              " & vbNewLine _
    '                                            & "INKA_L.SYS_DEL_FLG                                                                                  " & vbNewLine _
    '                                            & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
    '                                            & ",@FILE_NAME                                                                                         " & vbNewLine _
    '                                            & ",@REC_NO                                                                                            " & vbNewLine _
    '                                            & ",INKA_L.NRS_BR_CD                                                                                   " & vbNewLine _
    '                                            & ",@EDI_CTL_NO                                                                                        " & vbNewLine _
    '                                            & ",INKA_L.INKA_NO_L                                                                                   " & vbNewLine _
    '                                            & ",INKA_L.CUST_CD_L                                                                                   " & vbNewLine _
    '                                            & ",INKA_L.CUST_CD_M                                                                                   " & vbNewLine _
    '                                            & ",'0'                                                                                                " & vbNewLine _
    '                                            & ",'0'                                                                                                " & vbNewLine _
    '                                            & ",@PLANT_CD + 'MANUAL' + INKA_L.INKA_NO_L                                                            " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",SUBSTRING(INKA_L.OUTKA_FROM_ORD_NO_L, 1, 10)                                                       " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",INKA_L.INKA_DATE                                                                                   " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",@PLANT_CD                                                                                          " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",@RECORD_STATUS                                                                                     " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",''                                                                                                 " & vbNewLine _
    '                                            & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
    '                                            & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2)            " & vbNewLine _
    '                                            & ",INKA_L.SYS_ENT_USER                                                                                " & vbNewLine _
    '                                            & ",INKA_L.SYS_ENT_DATE                                                                                " & vbNewLine _
    '                                            & ",SUBSTRING(INKA_L.SYS_ENT_TIME,1,2) + ':' + SUBSTRING(INKA_L.SYS_ENT_TIME,3,2) + ':' + SUBSTRING(INKA_L.SYS_ENT_TIME,5,2)" & vbNewLine _
    '                                            & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
    '                                            & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2)            " & vbNewLine _
    '                                            & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_TIME                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_PGID                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_TIME                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_PGID                                                                                      " & vbNewLine _
    '                                            & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
    '                                            & ",INKA_L.SYS_DEL_FLG                                                                                 " & vbNewLine _
    '                                            & "FROM                                                                                                " & vbNewLine _
    '                                            & "$LM_TRN$..B_INKA_L INKA_L                                                                           " & vbNewLine _
    '                                            & "WHERE                                                                                               " & vbNewLine _
    '                                            & "INKA_L.INKA_NO_L = @INKA_NO_L                                                                       " & vbNewLine
    ''' <summary>
    ''' INSERT（H_INKAEDI_HED_DPN）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_HED_DPN As String = "INSERT INTO $LM_TRN$..H_INKAEDI_HED_DPN                                                              " & vbNewLine _
                                                & "SELECT                                                                                              " & vbNewLine _
                                                & "INKA_L.SYS_DEL_FLG                                                                                  " & vbNewLine _
                                                & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
                                                & ",@FILE_NAME                                                                                         " & vbNewLine _
                                                & ",@REC_NO                                                                                            " & vbNewLine _
                                                & ",INKA_L.NRS_BR_CD                                                                                   " & vbNewLine _
                                                & ",@EDI_CTL_NO                                                                                        " & vbNewLine _
                                                & ",INKA_L.INKA_NO_L                                                                                   " & vbNewLine _
                                                & ",INKA_L.CUST_CD_L                                                                                   " & vbNewLine _
                                                & ",INKA_L.CUST_CD_M                                                                                   " & vbNewLine _
                                                & ",'0'                                                                                                " & vbNewLine _
                                                & ",'0'                                                                                                " & vbNewLine _
                                                & ",@PLANT_CD + 'MANUAL' + INKA_L.INKA_NO_L                                                            " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",SUBSTRING(CAST(INKA_L.OUTKA_FROM_ORD_NO_L AS TEXT), 1, 10)                                         " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",INKA_L.INKA_DATE                                                                                   " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",@PLANT_CD                                                                                          " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",@RECORD_STATUS                                                                                     " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",''                                                                                                 " & vbNewLine _
                                                & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
                                                & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2)            " & vbNewLine _
                                                & ",INKA_L.SYS_ENT_USER                                                                                " & vbNewLine _
                                                & ",INKA_L.SYS_ENT_DATE                                                                                " & vbNewLine _
                                                & ",SUBSTRING(INKA_L.SYS_ENT_TIME,1,2) + ':' + SUBSTRING(INKA_L.SYS_ENT_TIME,3,2) + ':' + SUBSTRING(INKA_L.SYS_ENT_TIME,5,2)" & vbNewLine _
                                                & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
                                                & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2)            " & vbNewLine _
                                                & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_TIME                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_PGID                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_DATE                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_TIME                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_PGID                                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_USER                                                                                      " & vbNewLine _
                                                & ",INKA_L.SYS_DEL_FLG                                                                                 " & vbNewLine _
                                                & "FROM                                                                                                " & vbNewLine _
                                                & "$LM_TRN$..B_INKA_L INKA_L                                                                           " & vbNewLine _
                                                & "WHERE                                                                                               " & vbNewLine _
                                                & "INKA_L.INKA_NO_L = @INKA_NO_L                                                                       " & vbNewLine
    'END YANAI 要望番号898

#End Region

#Region "H_INKAEDI_DTL_DPN"

    ''' <summary>
    ''' INSERT（H_INKAEDI_DTL_DPN）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DTL_DPN As String = "INSERT INTO $LM_TRN$..H_INKAEDI_DTL_DPN                                                              " & vbNewLine _
                                                & "SELECT                                                                                              " & vbNewLine _
                                                & "INKA_M.SYS_DEL_FLG    as    DEL_KB                                                                  " & vbNewLine _
                                                & ",@SYS_ENT_DATE    as    CRT_DATE                                                                    " & vbNewLine _
                                                & ",@FILE_NAME    as    FILE_NAME                                                                      " & vbNewLine _
                                                & ",@REC_NO    as    REC_NO                                                                            " & vbNewLine _
                                                & ",RIGHT('000' + CONVERT(varchar,(ROW_NUMBER() OVER (PARTITION BY INKA_M.NRS_BR_CD,INKA_M.INKA_NO_L ORDER BY INKA_M.NRS_BR_CD,INKA_M.INKA_NO_L,INKA_M.INKA_NO_M))),3)    as    GYO" & vbNewLine _
                                                & ",INKA_L.NRS_BR_CD    as    NRS_BR_CD                                                                " & vbNewLine _
                                                & ",@EDI_CTL_NO    as    EDI_CTL_NO                                                                    " & vbNewLine _
                                                & ",INKA_M.INKA_NO_M    as    EDI_CTL_NO_CHU                                                           " & vbNewLine _
                                                & ",INKA_L.INKA_NO_L    as    INKA_CTL_NO_L                                                            " & vbNewLine _
                                                & ",INKA_M.INKA_NO_M    as    INKA_CTL_NO_M                                                            " & vbNewLine _
                                                & ",INKA_L.CUST_CD_L    as    CUST_CD_L                                                                " & vbNewLine _
                                                & ",INKA_L.CUST_CD_M    as    CUST_CD_M                                                                " & vbNewLine _
                                                & ",M_GOODS.GOODS_CD_CUST    as    R5_GOODS_CD                                                         " & vbNewLine _
                                                & ",M_GOODS.STD_IRIME_UT    as    R5_NB_UT                                                             " & vbNewLine _
                                                & ",0    as    R5_SHOKIRYO                                                                             " & vbNewLine _
                                                & ",0    as    R5_KYOKYURYO                                                                            " & vbNewLine _
                                                & ",''    as    R5_MANRYO_DATE                                                                         " & vbNewLine _
                                                & ",''    as    R5_COMMENT_1                                                                           " & vbNewLine _
                                                & ",''    as    R5_COMMENT_2                                                                           " & vbNewLine _
                                                & ",''    as    R5_GOODS_NM_E                                                                          " & vbNewLine _
                                                & ",''    as    R5_GOODS_NM_J                                                                          " & vbNewLine _
                                                & ",''    as    R5_KYOKYU_GOODS_CD                                                                     " & vbNewLine _
                                                & ",''    as    R5_KYOKYU_GOODS_CMT                                                                    " & vbNewLine _
                                                & ",SUBSTRING(INKA_M.OUTKA_FROM_ORD_NO_M, 1, 6)    as    R5_ORDER_NO_DTL                               " & vbNewLine _
                                                & ",SUM((INKA_S.KONSU * M_GOODS.PKG_NB + INKA_S.HASU) * INKA_S.IRIME)     as    R5_QT                  " & vbNewLine _
                                                & ",''    as    R6_LOT_NO                                                                              " & vbNewLine _
                                                & ",0    as    R6_SHOKIRYO                                                                             " & vbNewLine _
                                                & ",SUM((INKA_S.KONSU * M_GOODS.PKG_NB + INKA_S.HASU) * INKA_S.IRIME)    as    R6_QT                   " & vbNewLine _
                                                & ",''    as    R6_ORDER_NO_LOT                                                                        " & vbNewLine _
                                                & ",''    as    R6_LOT_MAKE_DATE                                                                       " & vbNewLine _
                                                & ",''    as    R6_LAST_INKO_DATE                                                                      " & vbNewLine _
                                                & ",@RECORD_STATUS    as    RECORD_STATUS                                                              " & vbNewLine _
                                                & ",'1'    as    JISSEKI_SHORI_FLG                                                                     " & vbNewLine _
                                                & ",''    as    JISSEKI_USER                                                                           " & vbNewLine _
                                                & ",''    as    JISSEKI_DATE                                                                           " & vbNewLine _
                                                & ",''    as    JISSEKI_TIME                                                                           " & vbNewLine _
                                                & ",''    as    SEND_USER                                                                              " & vbNewLine _
                                                & ",''    as    SEND_DATE                                                                              " & vbNewLine _
                                                & ",''    as    SEND_TIME                                                                              " & vbNewLine _
                                                & ",''    as    DELETE_USER                                                                            " & vbNewLine _
                                                & ",''    as    DELETE_DATE                                                                            " & vbNewLine _
                                                & ",''    as    DELETE_TIME                                                                            " & vbNewLine _
                                                & ",''    as    DELETE_EDI_NO                                                                          " & vbNewLine _
                                                & ",''    as    DELETE_EDI_NO_CHU                                                                      " & vbNewLine _
                                                & ",@SYS_ENT_USER AS UPD_USER                                                                          " & vbNewLine _
                                                & ",@SYS_ENT_DATE AS UPD_DATE                                                                          " & vbNewLine _
                                                & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2) AS UPD_TIME " & vbNewLine _
                                                & ",@SYS_ENT_DATE    as    SYS_ENT_DATE                                                                " & vbNewLine _
                                                & ",@SYS_ENT_TIME            as    SYS_ENT_TIME                                                        " & vbNewLine _
                                                & ",@SYS_ENT_PGID      as    SYS_ENT_PGID                                                              " & vbNewLine _
                                                & ",@SYS_ENT_USER    as    SYS_ENT_USER                                                                " & vbNewLine _
                                                & ",@SYS_ENT_DATE    as    SYS_UPD_DATE                                                                " & vbNewLine _
                                                & ",@SYS_ENT_TIME            as    SYS_UPD_TIME                                                        " & vbNewLine _
                                                & ",@SYS_ENT_PGID    as    SYS_UPD_PGID                                                                " & vbNewLine _
                                                & ",@SYS_ENT_USER    as    SYS_UPD_USER                                                                " & vbNewLine _
                                                & ",INKA_M.SYS_DEL_FLG    as    SYS_DEL_FLG                                                            " & vbNewLine _
                                                & "FROM                                                                                                " & vbNewLine _
                                                & "$LM_TRN$..B_INKA_L INKA_L                                                                           " & vbNewLine _
                                                & "LEFT JOIN $LM_TRN$..B_INKA_M INKA_M                                                                 " & vbNewLine _
                                                & "ON  INKA_L.NRS_BR_CD = INKA_M.NRS_BR_CD                                                             " & vbNewLine _
                                                & "AND INKA_L.INKA_NO_L = INKA_M.INKA_NO_L                                                             " & vbNewLine _
                                                & "LEFT JOIN $LM_TRN$..B_INKA_S INKA_S                                                                 " & vbNewLine _
                                                & "ON INKA_M.NRS_BR_CD = INKA_S.NRS_BR_CD                                                              " & vbNewLine _
                                                & "AND INKA_M.INKA_NO_L = INKA_S.INKA_NO_L                                                             " & vbNewLine _
                                                & "AND INKA_M.INKA_NO_M = INKA_S.INKA_NO_M                                                             " & vbNewLine _
                                                & "--商品Ｍ                                                                                            " & vbNewLine _
                                                & "LEFT JOIN $LM_MST$..M_GOODS M_GOODS                                                                 " & vbNewLine _
                                                & "ON  INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                                            " & vbNewLine _
                                                & "AND INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                                                      " & vbNewLine _
                                                & "WHERE                                                                                               " & vbNewLine _
                                                & "INKA_M.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                                                & "AND INKA_L.INKA_NO_L = @INKA_NO_L                                                                   " & vbNewLine _
                                                & "GROUP BY                                                                                            " & vbNewLine _
                                                & "INKA_M.SYS_DEL_FLG                                                                                  " & vbNewLine _
                                                & ",INKA_L.NRS_BR_CD                                                                                   " & vbNewLine _
                                                & ",INKA_M.NRS_BR_CD                                                                                   " & vbNewLine _
                                                & ",INKA_M.INKA_NO_M                                                                                   " & vbNewLine _
                                                & ",INKA_L.INKA_NO_L                                                                                   " & vbNewLine _
                                                & ",INKA_M.INKA_NO_L                                                                                   " & vbNewLine _
                                                & ",INKA_L.CUST_CD_L                                                                                   " & vbNewLine _
                                                & ",INKA_L.CUST_CD_M                                                                                   " & vbNewLine _
                                                & ",M_GOODS.GOODS_CD_CUST                                                                              " & vbNewLine _
                                                & ",M_GOODS.STD_IRIME_UT                                                                               " & vbNewLine _
                                                & ",INKA_M.OUTKA_FROM_ORD_NO_M                                                                         " & vbNewLine _
                                                & ",M_GOODS.PKG_NB                                                                                     " & vbNewLine

#End Region

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' INSERT（H_INKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKA_EDI_L As String = "INSERT INTO $LM_TRN$..H_INKAEDI_L                                                                     " & vbNewLine _
                                                    & "SELECT                                                                                              " & vbNewLine _
                                                    & "INKA_L.SYS_DEL_FLG                                                                                  " & vbNewLine _
                                                    & ",INKA_L.NRS_BR_CD                                                                                   " & vbNewLine _
                                                    & ",@EDI_CTL_NO AS EDI_CTL_NO                                                                          " & vbNewLine _
                                                    & ",INKA_L.INKA_NO_L                                                                                   " & vbNewLine _
                                                    & ",INKA_L.INKA_TP                                                                                     " & vbNewLine _
                                                    & ",INKA_L.INKA_KB                                                                                     " & vbNewLine _
                                                    & ",INKA_L.INKA_STATE_KB                                                                               " & vbNewLine _
                                                    & ",INKA_L.INKA_DATE                                                                                   " & vbNewLine _
                                                    & ",'' as INKA_TIME                                                                                    " & vbNewLine _
                                                    & ",INKA_L.WH_CD as NRS_WH_CD                                                                          " & vbNewLine _
                                                    & ",INKA_L.CUST_CD_L                                                                                   " & vbNewLine _
                                                    & ",INKA_L.CUST_CD_M                                                                                   " & vbNewLine _
                                                    & ",CUST.CUST_NM_L                                                                                     " & vbNewLine _
                                                    & ",CUST.CUST_NM_M                                                                                     " & vbNewLine _
                                                    & ",INKA_PLAN_QT                                                                                       " & vbNewLine _
                                                    & ",INKA_PLAN_QT_UT                                                                                    " & vbNewLine _
                                                    & ",INKA_TTL_NB                                                                                        " & vbNewLine _
                                                    & ",'' as NAIGAI_KB                                                                                    " & vbNewLine _
                                                    & ",INKA_L.BUYER_ORD_NO_L AS BUYER_ORD_NO                                                              " & vbNewLine _
                                                    & ",INKA_L.OUTKA_FROM_ORD_NO_L as OUTKA_FROM_ORD_NO                                                    " & vbNewLine _
                                                    & ",INKA_L.SEIQTO_CD                                                                                   " & vbNewLine _
                                                    & ",RIGHT(INKA_L.TOUKI_HOKAN_YN,1) as TOUKI_HOKAN_YN                                                   " & vbNewLine _
                                                    & ",RIGHT(INKA_L.HOKAN_YN,1) as TOUKI_HOKAN_YN                                                         " & vbNewLine _
                                                    & ",INKA_L.HOKAN_FREE_KIKAN                                                                            " & vbNewLine _
                                                    & ",INKA_L.HOKAN_STR_DATE                                                                              " & vbNewLine _
                                                    & ",RIGHT(INKA_L.NIYAKU_YN,1) as TOUKI_HOKAN_YN                                                        " & vbNewLine _
                                                    & ",INKA_L.TAX_KB                                                                                      " & vbNewLine _
                                                    & ",INKA_L.REMARK                                                                                      " & vbNewLine _
                                                    & ",INKA_L.REMARK_OUT as NYUBAN_L                                                                      " & vbNewLine _
                                                    & ",INKA_L.UNCHIN_TP as UNCHIN_TP                                                                      " & vbNewLine _
                                                    & ",INKA_L.UNCHIN_KB as UNCHIN_KB                                                                      " & vbNewLine _
                                                    & ",ISNULL(UNSO_L.ORIG_CD,'') as OUTKA_MOTO                                                            " & vbNewLine _
                                                    & ",ISNULL(UNSO_L.VCLE_KB,'') as SYARYO_KB                                                             " & vbNewLine _
                                                    & ",ISNULL(UNSO_L.UNSO_ONDO_KB,'') as UNSO_ONDO_KB                                                     " & vbNewLine _
                                                    & ",ISNULL(UNSO_L.UNSO_CD,'') as UNSO_ONDO_KB                                                          " & vbNewLine _
                                                    & ",ISNULL(UNSO_L.UNSO_BR_CD,'') as UNSO_ONDO_KB                                                       " & vbNewLine _
                                                    & ",ISNULL(UNCHIN.DECI_UNCHIN,0) as UNCHIN                                                             " & vbNewLine _
                                                    & ",ISNULL(UNSO_L.SEIQ_TARIFF_CD,'') as YOKO_TARIFF_CD                                                 " & vbNewLine _
                                                    & ",'1' AS OUT_FLAG                                                                                    " & vbNewLine _
                                                    & ",'0' AS AKAKURO_KB                                                                                  " & vbNewLine _
                                                    & ",'0' AS JISSEKI_FLAG                                                                                " & vbNewLine _
                                                    & ",'' AS JISSEKI_USER                                                                                 " & vbNewLine _
                                                    & ",'' AS JISSEKI_DATE                                                                                 " & vbNewLine _
                                                    & ",'' AS JISSEKI_TIME                                                                                 " & vbNewLine _
                                                    & ",0 AS FREE_N01                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N02                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N03                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N04                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N05                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N06                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N07                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N08                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N09                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N10                                                                                      " & vbNewLine _
                                                    & ",'' AS FREE_C01                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C02                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C03                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C04                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C05                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C06                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C07                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C08                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C09                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C10                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C11                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C12                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C13                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C14                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C15                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C16                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C17                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C18                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C19                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C20                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C21                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C22                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C23                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C24                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C25                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C26                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C27                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C28                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C29                                                                                     " & vbNewLine _
                                                    & ",'01-' + INKA_NO_L AS FREE_C30                                                                      " & vbNewLine _
                                                    & ",@SYS_ENT_USER AS CRT_USER                                                                          " & vbNewLine _
                                                    & ",@SYS_ENT_DATE AS CRT_DATE                                                                          " & vbNewLine _
                                                    & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2) AS CRT_TIME " & vbNewLine _
                                                    & ",@SYS_ENT_USER AS UPD_USER                                                                          " & vbNewLine _
                                                    & ",@SYS_ENT_DATE AS UPD_DATE                                                                          " & vbNewLine _
                                                    & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2) AS UPD_TIME " & vbNewLine _
                                                    & ",'' AS EDIT_FLAG                                                                                    " & vbNewLine _
                                                    & ",'' AS MATCHING_FLAG                                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_DATE    as    SYS_ENT_DATE                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_TIME            as    SYS_ENT_TIME                                                        " & vbNewLine _
                                                    & ",@SYS_ENT_PGID      as    SYS_ENT_PGID                                                              " & vbNewLine _
                                                    & ",@SYS_ENT_USER    as    SYS_ENT_USER                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_DATE    as    SYS_UPD_DATE                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_TIME            as    SYS_UPD_TIME                                                        " & vbNewLine _
                                                    & ",@SYS_ENT_PGID    as    SYS_UPD_PGID                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_USER    as    SYS_UPD_USER                                                                " & vbNewLine _
                                                    & ",INKA_L.SYS_DEL_FLG    as    SYS_DEL_FLG                                                            " & vbNewLine _
                                                    & "FROM  $LM_TRN$..B_INKA_L INKA_L                                                                     " & vbNewLine _
                                                    & "--運送Ｌ                                                                                            " & vbNewLine _
                                                    & "LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                                                                 " & vbNewLine _
                                                    & "ON INKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                                                              " & vbNewLine _
                                                    & "AND INKA_L.INKA_NO_L = UNSO_L.INOUTKA_NO_L                                                          " & vbNewLine _
                                                    & "AND UNSO_L.MOTO_DATA_KB = '10'                                                                      " & vbNewLine _
                                                    & "--運賃                                                                                              " & vbNewLine _
                                                    & "LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UNCHIN                                                             " & vbNewLine _
                                                    & "ON UNSO_L.UNSO_NO_L = UNCHIN.UNSO_NO_L                                                              " & vbNewLine _
                                                    & "AND UNCHIN.UNSO_NO_M = '000'                                                                        " & vbNewLine _
                                                    & "--荷主マスタ                                                                                        " & vbNewLine _
                                                    & "LEFT JOIN $LM_MST$..M_CUST CUST                                                                     " & vbNewLine _
                                                    & "ON INKA_L.NRS_BR_CD = CUST.NRS_BR_CD                                                                " & vbNewLine _
                                                    & "AND INKA_L.CUST_CD_L = CUST.CUST_CD_L                                                               " & vbNewLine _
                                                    & "AND INKA_L.CUST_CD_M = CUST.CUST_CD_M                                                               " & vbNewLine _
                                                    & "AND CUST.CUST_CD_S = '00'                                                                           " & vbNewLine _
                                                    & "AND CUST.CUST_CD_SS = '00'                                                                          " & vbNewLine _
                                                    & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                                                                       " & vbNewLine _
                                                    & "AND  INKA_L.INKA_NO_L = @INKA_NO_L                                                                  " & vbNewLine
#End Region

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' INSERT（H_INKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKA_EDI_M As String = "INSERT INTO $LM_TRN$..H_INKAEDI_M                                                                     " & vbNewLine _
                                                    & "SELECT                                                                                              " & vbNewLine _
                                                    & "INKA_M.SYS_DEL_FLG                                                                                  " & vbNewLine _
                                                    & ",INKA_M.NRS_BR_CD                                                                                   " & vbNewLine _
                                                    & ",@EDI_CTL_NO AS EDI_CTL_NO                                                                          " & vbNewLine _
                                                    & ",INKA_M.INKA_NO_M AS EDI_CTL_NO_CHU                                                                 " & vbNewLine _
                                                    & ",INKA_M.INKA_NO_L                                                                                   " & vbNewLine _
                                                    & ",INKA_M.INKA_NO_M                                                                                   " & vbNewLine _
                                                    & ",INKA_M.GOODS_CD_NRS AS NRS_GOODS_CD                                                                " & vbNewLine _
                                                    & ",GOODS.GOODS_CD_CUST AS CUST_GOODS_CD                                                               " & vbNewLine _
                                                    & ",GOODS.GOODS_NM_1 AS GOODS_NM                                                                       " & vbNewLine _
                                                    & ",(SUM(INKA_S.KONSU) * GOODS.PKG_NB) + SUM(INKA_S.HASU) AS NB                                        " & vbNewLine _
                                                    & ",GOODS.NB_UT AS NB_UT                                                                               " & vbNewLine _
                                                    & ",GOODS.PKG_NB AS PKG_NB                                                                             " & vbNewLine _
                                                    & ",GOODS.PKG_UT AS PKG_UT                                                                             " & vbNewLine _
                                                    & ",SUM(INKA_S.KONSU) AS INKA_PKG_NB                                                                   " & vbNewLine _
                                                    & ",SUM(INKA_S.HASU) AS HASU                                                                           " & vbNewLine _
                                                    & ",GOODS.STD_IRIME_NB AS STD_IRIME                                                                    " & vbNewLine _
                                                    & ",GOODS.STD_IRIME_UT AS STD_IRIME_UT                                                                 " & vbNewLine _
                                                    & ",GOODS.STD_WT_KGS AS BETU_WT                                                                        " & vbNewLine _
                                                    & ",GOODS.STD_CBM AS CBM                                                                               " & vbNewLine _
                                                    & ",GOODS.ONDO_KB AS ONDO_KB                                                                           " & vbNewLine _
                                                    & ",OUTKA_FROM_ORD_NO_M AS OUTKA_FROM_ORD_NO                                                           " & vbNewLine _
                                                    & ",BUYER_ORD_NO_M AS BUYER_ORD_NO                                                                     " & vbNewLine _
                                                    & ",INKA_M.REMARK AS REMARK                                                                            " & vbNewLine _
                                                    & ",'' AS LOT_NO                                                                                       " & vbNewLine _
                                                    & ",'' AS SERIAL_NO                                                                                    " & vbNewLine _
                                                    & ",INKA_S.IRIME AS IRIME                                                                              " & vbNewLine _
                                                    & ",GOODS.STD_IRIME_UT AS IRIME_UT                                                                     " & vbNewLine _
                                                    & ",'0' AS OUT_KB                                                                                      " & vbNewLine _
                                                    & ",'0' AS AKAKURO_KB                                                                                  " & vbNewLine _
                                                    & ",'0' AS JISSEKI_FLAG                                                                                " & vbNewLine _
                                                    & ",'' AS JISSEKI_USER                                                                                 " & vbNewLine _
                                                    & ",'' AS JISSEKI_DATE                                                                                 " & vbNewLine _
                                                    & ",'' AS JISSEKI_TIME                                                                                 " & vbNewLine _
                                                    & ",0 AS FREE_N01                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N02                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N03                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N04                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N05                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N06                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N07                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N08                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N09                                                                                      " & vbNewLine _
                                                    & ",0 AS FREE_N10                                                                                      " & vbNewLine _
                                                    & ",'' AS FREE_C01                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C02                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C03                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C04                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C05                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C06                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C07                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C08                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C09                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C10                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C11                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C12                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C13                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C14                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C15                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C16                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C17                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C18                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C19                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C20                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C21                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C22                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C23                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C24                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C25                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C26                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C27                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C28                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C29                                                                                     " & vbNewLine _
                                                    & ",'' AS FREE_C30                                                                                     " & vbNewLine _
                                                    & ",@SYS_ENT_USER AS CRT_USER                                                                          " & vbNewLine _
                                                    & ",@SYS_ENT_DATE AS CRT_DATE                                                                          " & vbNewLine _
                                                    & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2) AS CRT_TIME " & vbNewLine _
                                                    & ",@SYS_ENT_USER AS UPD_USER                                                                          " & vbNewLine _
                                                    & ",@SYS_ENT_DATE AS UPD_DATE                                                                          " & vbNewLine _
                                                    & ",SUBSTRING(@SYS_ENT_TIME,1,2) + ':' + SUBSTRING(@SYS_ENT_TIME,3,2) + ':' + SUBSTRING(@SYS_ENT_TIME,5,2) AS UPD_TIME " & vbNewLine _
                                                    & ",@SYS_ENT_DATE    as    SYS_ENT_DATE                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_TIME            as    SYS_ENT_TIME                                                        " & vbNewLine _
                                                    & ",@SYS_ENT_PGID      as    SYS_ENT_PGID                                                              " & vbNewLine _
                                                    & ",@SYS_ENT_USER    as    SYS_ENT_USER                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_DATE    as    SYS_UPD_DATE                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_TIME            as    SYS_UPD_TIME                                                        " & vbNewLine _
                                                    & ",@SYS_ENT_PGID    as    SYS_UPD_PGID                                                                " & vbNewLine _
                                                    & ",@SYS_ENT_USER    as    SYS_UPD_USER                                                                " & vbNewLine _
                                                    & ",INKA_M.SYS_DEL_FLG    as    SYS_DEL_FLG                                                            " & vbNewLine _
                                                    & "FROM  $LM_TRN$..B_INKA_L INKA_L                                                                     " & vbNewLine _
                                                    & "--入荷Ｍ                                                                                            " & vbNewLine _
                                                    & "LEFT JOIN $LM_TRN$..B_INKA_M INKA_M                                                                 " & vbNewLine _
                                                    & "ON INKA_L.NRS_BR_CD = INKA_M.NRS_BR_CD                                                              " & vbNewLine _
                                                    & "AND INKA_L.INKA_NO_L = INKA_M.INKA_NO_L                                                             " & vbNewLine _
                                                    & "LEFT JOIN $LM_TRN$..B_INKA_S INKA_S                                                                 " & vbNewLine _
                                                    & "ON INKA_M.NRS_BR_CD = INKA_S.NRS_BR_CD                                                              " & vbNewLine _
                                                    & "AND INKA_M.INKA_NO_L = INKA_S.INKA_NO_L                                                             " & vbNewLine _
                                                    & "AND INKA_M.INKA_NO_M = INKA_S.INKA_NO_M                                                             " & vbNewLine _
                                                    & "--商品                                                                                              " & vbNewLine _
                                                    & "LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                   " & vbNewLine _
                                                    & "ON INKA_M.NRS_BR_CD = GOODS.NRS_BR_CD                                                               " & vbNewLine _
                                                    & "AND INKA_M.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                        " & vbNewLine _
                                                    & "WHERE INKA_L.NRS_BR_CD = @NRS_BR_CD                                                                       " & vbNewLine _
                                                    & "AND INKA_L.INKA_NO_L = @INKA_NO_L                                                                   " & vbNewLine _
                                                    & "AND INKA_L.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                                    & "AND INKA_M.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                                    & "AND INKA_S.SYS_DEL_FLG = '0'                                                                        " & vbNewLine _
                                                    & "GROUP BY                                                                                            " & vbNewLine _
                                                    & "INKA_M.SYS_DEL_FLG                                                                                  " & vbNewLine _
                                                    & ",INKA_M.NRS_BR_CD                                                                                   " & vbNewLine _
                                                    & ",INKA_M.INKA_NO_M                                                                                   " & vbNewLine _
                                                    & ",INKA_M.INKA_NO_L                                                                                   " & vbNewLine _
                                                    & ",INKA_M.INKA_NO_M                                                                                   " & vbNewLine _
                                                    & ",INKA_M.GOODS_CD_NRS                                                                                " & vbNewLine _
                                                    & ",GOODS.GOODS_CD_CUST                                                                                " & vbNewLine _
                                                    & ",GOODS.GOODS_NM_1                                                                                   " & vbNewLine _
                                                    & ",GOODS.NB_UT                                                                                        " & vbNewLine _
                                                    & ",GOODS.PKG_NB                                                                                       " & vbNewLine _
                                                    & ",GOODS.PKG_UT                                                                                       " & vbNewLine _
                                                    & ",GOODS.STD_IRIME_NB                                                                                 " & vbNewLine _
                                                    & ",GOODS.STD_IRIME_UT                                                                                 " & vbNewLine _
                                                    & ",GOODS.STD_WT_KGS                                                                                   " & vbNewLine _
                                                    & ",GOODS.STD_CBM                                                                                      " & vbNewLine _
                                                    & ",GOODS.ONDO_KB                                                                                      " & vbNewLine _
                                                    & ",OUTKA_FROM_ORD_NO_M                                                                                " & vbNewLine _
                                                    & ",BUYER_ORD_NO_M                                                                                     " & vbNewLine _
                                                    & ",INKA_M.REMARK                                                                                      " & vbNewLine _
                                                    & ",INKA_S.IRIME                                                                                       " & vbNewLine _
                                                    & ",GOODS.STD_IRIME_UT                                                                                 " & vbNewLine _
                                                    & ",INKA_M.SYS_DEL_FLG                                                                                 " & vbNewLine

#End Region

#Region "作業レコード新規追加 SQL INSERT句"

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 作業レコード新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO $LM_TRN$..E_SAGYO                          " & vbNewLine _
                                              & " ( 		                                           " & vbNewLine _
                                              & " NRS_BR_CD,                                           " & vbNewLine _
                                              & " SAGYO_REC_NO,                                        " & vbNewLine _
                                              & " SAGYO_COMP,                                          " & vbNewLine _
                                              & " SKYU_CHK,                                            " & vbNewLine _
                                              & " SAGYO_SIJI_NO,                                       " & vbNewLine _
                                              & " INOUTKA_NO_LM,                                       " & vbNewLine _
                                              & " WH_CD,                                               " & vbNewLine _
                                              & " IOZS_KB,                                             " & vbNewLine _
                                              & " SAGYO_CD,                                            " & vbNewLine _
                                              & " SAGYO_NM,                                            " & vbNewLine _
                                              & " CUST_CD_L,                                           " & vbNewLine _
                                              & " CUST_CD_M,                                           " & vbNewLine _
                                              & " DEST_CD,                                             " & vbNewLine _
                                              & " DEST_NM,                                             " & vbNewLine _
                                              & " GOODS_CD_NRS,                                        " & vbNewLine _
                                              & " GOODS_NM_NRS,                                        " & vbNewLine _
                                              & " LOT_NO,                                              " & vbNewLine _
                                              & " INV_TANI,                                            " & vbNewLine _
                                              & " SAGYO_NB,                                            " & vbNewLine _
                                              & " SAGYO_UP,                                            " & vbNewLine _
                                              & " SAGYO_GK,                                            " & vbNewLine _
                                              & " TAX_KB,                                              " & vbNewLine _
                                              & " SEIQTO_CD,                                           " & vbNewLine _
                                              & " REMARK_ZAI,                                          " & vbNewLine _
                                              & " REMARK_SKYU,                                         " & vbNewLine _
                                              & " SAGYO_COMP_CD,                                       " & vbNewLine _
                                              & " SAGYO_COMP_DATE,                                     " & vbNewLine _
                                              & " DEST_SAGYO_FLG,                                      " & vbNewLine _
                                              & " SYS_ENT_DATE,                                        " & vbNewLine _
                                              & " SYS_ENT_TIME,                                        " & vbNewLine _
                                              & " SYS_ENT_PGID,                                        " & vbNewLine _
                                              & " SYS_ENT_USER,                                        " & vbNewLine _
                                              & " SYS_UPD_DATE,                                        " & vbNewLine _
                                              & " SYS_UPD_TIME,                                        " & vbNewLine _
                                              & " SYS_UPD_PGID,                                        " & vbNewLine _
                                              & " SYS_UPD_USER,                                        " & vbNewLine _
                                              & " SYS_DEL_FLG                                          " & vbNewLine _
                                              & " ) VALUES (                                           " & vbNewLine _
                                              & " @NRS_BR_CD,                                          " & vbNewLine _
                                              & " @SAGYO_REC_NO,                                       " & vbNewLine _
                                              & " @SAGYO_COMP,                                         " & vbNewLine _
                                              & " @SKYU_CHK,                                           " & vbNewLine _
                                              & " @SAGYO_SIJI_NO,                                      " & vbNewLine _
                                              & " @INOUTKA_NO_LM,                                      " & vbNewLine _
                                              & " @WH_CD,                                              " & vbNewLine _
                                              & " @IOZS_KB,                                            " & vbNewLine _
                                              & " @SAGYO_CD,                                           " & vbNewLine _
                                              & " @SAGYO_NM,                                           " & vbNewLine _
                                              & " @CUST_CD_L,                                          " & vbNewLine _
                                              & " @CUST_CD_M,                                          " & vbNewLine _
                                              & " @DEST_CD,                                            " & vbNewLine _
                                              & " @DEST_NM,                                            " & vbNewLine _
                                              & " @GOODS_CD_NRS,                                       " & vbNewLine _
                                              & " @GOODS_NM_NRS,                                       " & vbNewLine _
                                              & " @LOT_NO,                                             " & vbNewLine _
                                              & " @INV_TANI,                                           " & vbNewLine _
                                              & " @SAGYO_NB,                                           " & vbNewLine _
                                              & " @SAGYO_UP,                                           " & vbNewLine _
                                              & " @SAGYO_GK,                                           " & vbNewLine _
                                              & " @TAX_KB,                                             " & vbNewLine _
                                              & " @SEIQTO_CD,                                          " & vbNewLine _
                                              & " @REMARK_ZAI,                                         " & vbNewLine _
                                              & " @REMARK_SKYU,                                        " & vbNewLine _
                                              & " @SAGYO_COMP_CD,                                      " & vbNewLine _
                                              & " @SAGYO_COMP_DATE,                                    " & vbNewLine _
                                              & " @DEST_SAGYO_FLG,                                     " & vbNewLine _
                                              & " @SYS_ENT_DATE,                                       " & vbNewLine _
                                              & " @SYS_ENT_TIME,                                       " & vbNewLine _
                                              & " @SYS_ENT_PGID,                                       " & vbNewLine _
                                              & " @SYS_ENT_USER,                                       " & vbNewLine _
                                              & " @SYS_UPD_DATE,                                       " & vbNewLine _
                                              & " @SYS_UPD_TIME,                                       " & vbNewLine _
                                              & " @SYS_UPD_PGID,                                       " & vbNewLine _
                                              & " @SYS_UPD_USER,                                       " & vbNewLine _
                                              & " @SYS_DEL_FLG                                         " & vbNewLine _
                                              & " )                                                    " & vbNewLine
    'END YANAI 20120121 作業一括処理対応
#End Region

#Region "出荷データ(大)新規追加 SQL INSERT句"

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ(大)新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKAL As String = "INSERT INTO $LM_TRN$..C_OUTKA_L                       " & vbNewLine _
                                              & " ( 		                                           " & vbNewLine _
                                              & " NRS_BR_CD,                                           " & vbNewLine _
                                              & " OUTKA_NO_L,                                          " & vbNewLine _
                                              & " FURI_NO,                                             " & vbNewLine _
                                              & " OUTKA_KB,                                            " & vbNewLine _
                                              & " SYUBETU_KB,                                          " & vbNewLine _
                                              & " OUTKA_STATE_KB,                                      " & vbNewLine _
                                              & " OUTKAHOKOKU_YN,                                      " & vbNewLine _
                                              & " PICK_KB,                                             " & vbNewLine _
                                              & " DENP_NO,                                             " & vbNewLine _
                                              & " ARR_KANRYO_INFO,                                     " & vbNewLine _
                                              & " WH_CD,                                               " & vbNewLine _
                                              & " OUTKA_PLAN_DATE,                                     " & vbNewLine _
                                              & " OUTKO_DATE,                                          " & vbNewLine _
                                              & " ARR_PLAN_DATE,                                       " & vbNewLine _
                                              & " ARR_PLAN_TIME,                                       " & vbNewLine _
                                              & " HOKOKU_DATE,                                         " & vbNewLine _
                                              & " TOUKI_HOKAN_YN,                                      " & vbNewLine _
                                              & " END_DATE,                                            " & vbNewLine _
                                              & " CUST_CD_L,                                           " & vbNewLine _
                                              & " CUST_CD_M,                                           " & vbNewLine _
                                              & " SHIP_CD_L,                                           " & vbNewLine _
                                              & " SHIP_CD_M,                                           " & vbNewLine _
                                              & " DEST_CD,                                             " & vbNewLine _
                                              & " DEST_AD_3,                                           " & vbNewLine _
                                              & " DEST_TEL,                                            " & vbNewLine _
                                              & " NHS_REMARK,                                          " & vbNewLine _
                                              & " SP_NHS_KB,                                           " & vbNewLine _
                                              & " COA_YN,                                              " & vbNewLine _
                                              & " CUST_ORD_NO,                                         " & vbNewLine _
                                              & " BUYER_ORD_NO,                                        " & vbNewLine _
                                              & " REMARK,                                              " & vbNewLine _
                                              & " OUTKA_PKG_NB,                                        " & vbNewLine _
                                              & " DENP_YN,                                             " & vbNewLine _
                                              & " PC_KB,                                               " & vbNewLine _
                                              & " NIYAKU_YN,                                           " & vbNewLine _
                                              & " DEST_KB,                                             " & vbNewLine _
                                              & " DEST_NM,                                             " & vbNewLine _
                                              & " DEST_AD_1,                                           " & vbNewLine _
                                              & " DEST_AD_2,                                           " & vbNewLine _
                                              & " ALL_PRINT_FLAG,                                      " & vbNewLine _
                                              & " NIHUDA_FLAG,                                         " & vbNewLine _
                                              & " NHS_FLAG,                                            " & vbNewLine _
                                              & " DENP_FLAG,                                           " & vbNewLine _
                                              & " COA_FLAG,                                            " & vbNewLine _
                                              & " HOKOKU_FLAG,                                         " & vbNewLine _
                                              & " MATOME_PICK_FLAG,                                    " & vbNewLine _
                                              & " LAST_PRINT_DATE,                                     " & vbNewLine _
                                              & " LAST_PRINT_TIME,                                     " & vbNewLine _
                                              & " SASZ_USER,                                           " & vbNewLine _
                                              & " OUTKO_USER,                                          " & vbNewLine _
                                              & " KEN_USER,                                            " & vbNewLine _
                                              & " OUTKA_USER,                                          " & vbNewLine _
                                              & " HOU_USER,                                            " & vbNewLine _
                                              & " ORDER_TYPE,                                          " & vbNewLine _
                                              & " SYS_ENT_DATE,                                        " & vbNewLine _
                                              & " SYS_ENT_TIME,                                        " & vbNewLine _
                                              & " SYS_ENT_PGID,                                        " & vbNewLine _
                                              & " SYS_ENT_USER,                                        " & vbNewLine _
                                              & " SYS_UPD_DATE,                                        " & vbNewLine _
                                              & " SYS_UPD_TIME,                                        " & vbNewLine _
                                              & " SYS_UPD_PGID,                                        " & vbNewLine _
                                              & " SYS_UPD_USER,                                        " & vbNewLine _
                                              & " SYS_DEL_FLG                                          " & vbNewLine _
                                              & " ) VALUES (                                           " & vbNewLine _
                                              & " @NRS_BR_CD,                                          " & vbNewLine _
                                              & " @OUTKA_NO_L,                                         " & vbNewLine _
                                              & " @FURI_NO,                                            " & vbNewLine _
                                              & " @OUTKA_KB,                                           " & vbNewLine _
                                              & " @SYUBETU_KB,                                         " & vbNewLine _
                                              & " @OUTKA_STATE_KB,                                     " & vbNewLine _
                                              & " @OUTKAHOKOKU_YN,                                     " & vbNewLine _
                                              & " @PICK_KB,                                            " & vbNewLine _
                                              & " @DENP_NO,                                            " & vbNewLine _
                                              & " @ARR_KANRYO_INFO,                                    " & vbNewLine _
                                              & " @WH_CD,                                              " & vbNewLine _
                                              & " @OUTKA_PLAN_DATE,                                    " & vbNewLine _
                                              & " @OUTKO_DATE,                                         " & vbNewLine _
                                              & " @ARR_PLAN_DATE,                                      " & vbNewLine _
                                              & " @ARR_PLAN_TIME,                                      " & vbNewLine _
                                              & " @HOKOKU_DATE,                                        " & vbNewLine _
                                              & " @TOUKI_HOKAN_YN,                                     " & vbNewLine _
                                              & " @END_DATE,                                           " & vbNewLine _
                                              & " @CUST_CD_L,                                          " & vbNewLine _
                                              & " @CUST_CD_M,                                          " & vbNewLine _
                                              & " @SHIP_CD_L,                                          " & vbNewLine _
                                              & " @SHIP_CD_M,                                          " & vbNewLine _
                                              & " @DEST_CD,                                            " & vbNewLine _
                                              & " @DEST_AD_3,                                          " & vbNewLine _
                                              & " @DEST_TEL,                                           " & vbNewLine _
                                              & " @NHS_REMARK,                                         " & vbNewLine _
                                              & " @SP_NHS_KB,                                          " & vbNewLine _
                                              & " @COA_YN,                                             " & vbNewLine _
                                              & " @CUST_ORD_NO,                                        " & vbNewLine _
                                              & " @BUYER_ORD_NO,                                       " & vbNewLine _
                                              & " @REMARK,                                             " & vbNewLine _
                                              & " @OUTKA_PKG_NB,                                       " & vbNewLine _
                                              & " @DENP_YN,                                            " & vbNewLine _
                                              & " @PC_KB,                                              " & vbNewLine _
                                              & " @NIYAKU_YN,                                          " & vbNewLine _
                                              & " @DEST_KB,                                            " & vbNewLine _
                                              & " @DEST_NM,                                            " & vbNewLine _
                                              & " @DEST_AD_1,                                          " & vbNewLine _
                                              & " @DEST_AD_2,                                          " & vbNewLine _
                                              & " @ALL_PRINT_FLAG,                                     " & vbNewLine _
                                              & " @NIHUDA_FLAG,                                        " & vbNewLine _
                                              & " @NHS_FLAG,                                           " & vbNewLine _
                                              & " @DENP_FLAG,                                          " & vbNewLine _
                                              & " @COA_FLAG,                                           " & vbNewLine _
                                              & " @HOKOKU_FLAG,                                        " & vbNewLine _
                                              & " @MATOME_PICK_FLAG,                                   " & vbNewLine _
                                              & " @LAST_PRINT_DATE,                                    " & vbNewLine _
                                              & " @LAST_PRINT_TIME,                                    " & vbNewLine _
                                              & " @SASZ_USER,                                          " & vbNewLine _
                                              & " @OUTKO_USER,                                         " & vbNewLine _
                                              & " @KEN_USER,                                           " & vbNewLine _
                                              & " @OUTKA_USER,                                         " & vbNewLine _
                                              & " @HOU_USER,                                           " & vbNewLine _
                                              & " @ORDER_TYPE,                                         " & vbNewLine _
                                              & " @SYS_ENT_DATE,                                       " & vbNewLine _
                                              & " @SYS_ENT_TIME,                                       " & vbNewLine _
                                              & " @SYS_ENT_PGID,                                       " & vbNewLine _
                                              & " @SYS_ENT_USER,                                       " & vbNewLine _
                                              & " @SYS_UPD_DATE,                                       " & vbNewLine _
                                              & " @SYS_UPD_TIME,                                       " & vbNewLine _
                                              & " @SYS_UPD_PGID,                                       " & vbNewLine _
                                              & " @SYS_UPD_USER,                                       " & vbNewLine _
                                              & " @SYS_DEL_FLG                                         " & vbNewLine _
                                              & " )                                                    " & vbNewLine
#End Region

#Region "出荷データ(中)新規追加 SQL INSERT句"
    ''' <summary>
    ''' 出荷データ(中)新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKAM As String = "INSERT INTO $LM_TRN$..C_OUTKA_M                       " & vbNewLine _
                                              & " ( 		                                           " & vbNewLine _
                                              & " NRS_BR_CD,                                           " & vbNewLine _
                                              & " OUTKA_NO_L,                                          " & vbNewLine _
                                              & " OUTKA_NO_M,                                          " & vbNewLine _
                                              & " EDI_SET_NO,                                          " & vbNewLine _
                                              & " COA_YN,                                              " & vbNewLine _
                                              & " CUST_ORD_NO_DTL,                                     " & vbNewLine _
                                              & " BUYER_ORD_NO_DTL,                                    " & vbNewLine _
                                              & " GOODS_CD_NRS,                                        " & vbNewLine _
                                              & " RSV_NO,                                              " & vbNewLine _
                                              & " LOT_NO,                                              " & vbNewLine _
                                              & " SERIAL_NO,                                           " & vbNewLine _
                                              & " ALCTD_KB,                                            " & vbNewLine _
                                              & " OUTKA_PKG_NB,                                        " & vbNewLine _
                                              & " OUTKA_HASU,                                          " & vbNewLine _
                                              & " OUTKA_QT,                                            " & vbNewLine _
                                              & " OUTKA_TTL_NB,                                        " & vbNewLine _
                                              & " OUTKA_TTL_QT,                                        " & vbNewLine _
                                              & " ALCTD_NB,                                            " & vbNewLine _
                                              & " ALCTD_QT,                                            " & vbNewLine _
                                              & " BACKLOG_NB,                                          " & vbNewLine _
                                              & " BACKLOG_QT,                                          " & vbNewLine _
                                              & " UNSO_ONDO_KB,                                        " & vbNewLine _
                                              & " IRIME,                                               " & vbNewLine _
                                              & " IRIME_UT,                                            " & vbNewLine _
                                              & " OUTKA_M_PKG_NB,                                      " & vbNewLine _
                                              & " REMARK,                                              " & vbNewLine _
                                              & " SIZE_KB,                                             " & vbNewLine _
                                              & " ZAIKO_KB,                                            " & vbNewLine _
                                              & " SOURCE_CD,                                           " & vbNewLine _
                                              & " YELLOW_CARD,                                         " & vbNewLine _
                                              & " GOODS_CD_NRS_FROM,                                   " & vbNewLine _
                                              & " PRINT_SORT,                                          " & vbNewLine _
                                              & " SYS_ENT_DATE,                                        " & vbNewLine _
                                              & " SYS_ENT_TIME,                                        " & vbNewLine _
                                              & " SYS_ENT_PGID,                                        " & vbNewLine _
                                              & " SYS_ENT_USER,                                        " & vbNewLine _
                                              & " SYS_UPD_DATE,                                        " & vbNewLine _
                                              & " SYS_UPD_TIME,                                        " & vbNewLine _
                                              & " SYS_UPD_PGID,                                        " & vbNewLine _
                                              & " SYS_UPD_USER,                                        " & vbNewLine _
                                              & " SYS_DEL_FLG                                          " & vbNewLine _
                                              & " ) VALUES (                                           " & vbNewLine _
                                              & " @NRS_BR_CD,                                          " & vbNewLine _
                                              & " @OUTKA_NO_L,                                         " & vbNewLine _
                                              & " @OUTKA_NO_M,                                         " & vbNewLine _
                                              & " @EDI_SET_NO,                                         " & vbNewLine _
                                              & " @COA_YN,                                             " & vbNewLine _
                                              & " @CUST_ORD_NO_DTL,                                    " & vbNewLine _
                                              & " @BUYER_ORD_NO_DTL,                                   " & vbNewLine _
                                              & " @GOODS_CD_NRS,                                       " & vbNewLine _
                                              & " @RSV_NO,                                             " & vbNewLine _
                                              & " @LOT_NO,                                             " & vbNewLine _
                                              & " @SERIAL_NO,                                          " & vbNewLine _
                                              & " @ALCTD_KB,                                           " & vbNewLine _
                                              & " @OUTKA_PKG_NB,                                       " & vbNewLine _
                                              & " @OUTKA_HASU,                                         " & vbNewLine _
                                              & " @OUTKA_QT,                                           " & vbNewLine _
                                              & " @OUTKA_TTL_NB,                                       " & vbNewLine _
                                              & " @OUTKA_TTL_QT,                                       " & vbNewLine _
                                              & " @ALCTD_NB,                                           " & vbNewLine _
                                              & " @ALCTD_QT,                                           " & vbNewLine _
                                              & " @BACKLOG_NB,                                         " & vbNewLine _
                                              & " @BACKLOG_QT,                                         " & vbNewLine _
                                              & " @UNSO_ONDO_KB,                                       " & vbNewLine _
                                              & " @IRIME,                                              " & vbNewLine _
                                              & " @IRIME_UT,                                           " & vbNewLine _
                                              & " @OUTKA_M_PKG_NB,                                     " & vbNewLine _
                                              & " @REMARK,                                             " & vbNewLine _
                                              & " @SIZE_KB,                                            " & vbNewLine _
                                              & " @ZAIKO_KB,                                           " & vbNewLine _
                                              & " @SOURCE_CD,                                          " & vbNewLine _
                                              & " @YELLOW_CARD,                                        " & vbNewLine _
                                              & " @GOODS_CD_NRS_FROM,                                  " & vbNewLine _
                                              & " @PRINT_SORT,                                         " & vbNewLine _
                                              & " @SYS_ENT_DATE,                                       " & vbNewLine _
                                              & " @SYS_ENT_TIME,                                       " & vbNewLine _
                                              & " @SYS_ENT_PGID,                                       " & vbNewLine _
                                              & " @SYS_ENT_USER,                                       " & vbNewLine _
                                              & " @SYS_UPD_DATE,                                       " & vbNewLine _
                                              & " @SYS_UPD_TIME,                                       " & vbNewLine _
                                              & " @SYS_UPD_PGID,                                       " & vbNewLine _
                                              & " @SYS_UPD_USER,                                       " & vbNewLine _
                                              & " @SYS_DEL_FLG                                         " & vbNewLine _
                                              & " )                                                    " & vbNewLine
#End Region

#Region "運送データ(大)新規追加 SQL INSERT句"
    ''' <summary>
    ''' 運送データ(大)新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_UNSOL As String = "INSERT INTO $LM_TRN$..F_UNSO_L                         " & vbNewLine _
                                             & " ( 		                                               " & vbNewLine _
                                             & " NRS_BR_CD,                                            " & vbNewLine _
                                             & " UNSO_NO_L,                                            " & vbNewLine _
                                             & " YUSO_BR_CD,                                           " & vbNewLine _
                                             & " INOUTKA_NO_L,                                         " & vbNewLine _
                                             & " TRIP_NO,                                              " & vbNewLine _
                                             & " UNSO_CD,                                              " & vbNewLine _
                                             & " UNSO_BR_CD,                                           " & vbNewLine _
                                             & " BIN_KB,                                               " & vbNewLine _
                                             & " JIYU_KB,                                              " & vbNewLine _
                                             & " DENP_NO,                                              " & vbNewLine _
                                             & " OUTKA_PLAN_DATE,                                      " & vbNewLine _
                                             & " OUTKA_PLAN_TIME,                                      " & vbNewLine _
                                             & " ARR_PLAN_DATE,                                        " & vbNewLine _
                                             & " ARR_PLAN_TIME,                                        " & vbNewLine _
                                             & " ARR_ACT_TIME,                                         " & vbNewLine _
                                             & " CUST_CD_L,                                            " & vbNewLine _
                                             & " CUST_CD_M,                                            " & vbNewLine _
                                             & " CUST_REF_NO,                                          " & vbNewLine _
                                             & " SHIP_CD,                                              " & vbNewLine _
                                             & " ORIG_CD,                                              " & vbNewLine _
                                             & " DEST_CD,                                              " & vbNewLine _
                                             & " UNSO_PKG_NB,                                          " & vbNewLine _
                                             & " NB_UT,                                                " & vbNewLine _
                                             & " UNSO_WT,                                              " & vbNewLine _
                                             & " UNSO_ONDO_KB,                                         " & vbNewLine _
                                             & " PC_KB,                                                " & vbNewLine _
                                             & " TARIFF_BUNRUI_KB,                                     " & vbNewLine _
                                             & " VCLE_KB,                                              " & vbNewLine _
                                             & " MOTO_DATA_KB,                                         " & vbNewLine _
                                             & " TAX_KB,                                               " & vbNewLine _
                                             & " REMARK,                                               " & vbNewLine _
                                             & " SEIQ_TARIFF_CD,                                       " & vbNewLine _
                                             & " SEIQ_ETARIFF_CD,                                      " & vbNewLine _
                                             & " AD_3,                                                 " & vbNewLine _
                                             & " UNSO_TEHAI_KB,                                        " & vbNewLine _
                                             & " BUY_CHU_NO,                                           " & vbNewLine _
                                             & " AREA_CD,                                              " & vbNewLine _
                                             & " TYUKEI_HAISO_FLG,                                     " & vbNewLine _
                                             & " SYUKA_TYUKEI_CD,                                      " & vbNewLine _
                                             & " HAIKA_TYUKEI_CD,                                      " & vbNewLine _
                                             & " TRIP_NO_SYUKA,                                        " & vbNewLine _
                                             & " TRIP_NO_TYUKEI,                                       " & vbNewLine _
                                             & " TRIP_NO_HAIKA,                                        " & vbNewLine _
                                             & " SYS_ENT_DATE,                                         " & vbNewLine _
                                             & " SYS_ENT_TIME,                                         " & vbNewLine _
                                             & " SYS_ENT_PGID,                                         " & vbNewLine _
                                             & " SYS_ENT_USER,                                         " & vbNewLine _
                                             & " SYS_UPD_DATE,                                         " & vbNewLine _
                                             & " SYS_UPD_TIME,                                         " & vbNewLine _
                                             & " SYS_UPD_PGID,                                         " & vbNewLine _
                                             & " SYS_UPD_USER,                                         " & vbNewLine _
                                             & " SYS_DEL_FLG                                           " & vbNewLine _
                                             & " ) VALUES (                                            " & vbNewLine _
                                             & " @NRS_BR_CD,                                           " & vbNewLine _
                                             & " @UNSO_NO_L,                                           " & vbNewLine _
                                             & " @YUSO_BR_CD,                                          " & vbNewLine _
                                             & " @INOUTKA_NO_L,                                        " & vbNewLine _
                                             & " @TRIP_NO,                                             " & vbNewLine _
                                             & " @UNSO_CD,                                             " & vbNewLine _
                                             & " @UNSO_BR_CD,                                          " & vbNewLine _
                                             & " @BIN_KB,                                              " & vbNewLine _
                                             & " @JIYU_KB,                                             " & vbNewLine _
                                             & " @DENP_NO,                                             " & vbNewLine _
                                             & " @OUTKA_PLAN_DATE,                                     " & vbNewLine _
                                             & " @OUTKA_PLAN_TIME,                                     " & vbNewLine _
                                             & " @ARR_PLAN_DATE,                                       " & vbNewLine _
                                             & " @ARR_PLAN_TIME,                                       " & vbNewLine _
                                             & " @ARR_ACT_TIME,                                        " & vbNewLine _
                                             & " @CUST_CD_L,                                           " & vbNewLine _
                                             & " @CUST_CD_M,                                           " & vbNewLine _
                                             & " @CUST_REF_NO,                                         " & vbNewLine _
                                             & " @SHIP_CD,                                             " & vbNewLine _
                                             & " @ORIG_CD,                                             " & vbNewLine _
                                             & " @DEST_CD,                                             " & vbNewLine _
                                             & " @UNSO_PKG_NB,                                         " & vbNewLine _
                                             & " @NB_UT,                                               " & vbNewLine _
                                             & " @UNSO_WT,                                             " & vbNewLine _
                                             & " @UNSO_ONDO_KB,                                        " & vbNewLine _
                                             & " @PC_KB,                                               " & vbNewLine _
                                             & " @TARIFF_BUNRUI_KB,                                    " & vbNewLine _
                                             & " @VCLE_KB,                                             " & vbNewLine _
                                             & " @MOTO_DATA_KB,                                        " & vbNewLine _
                                             & " @TAX_KB,                                              " & vbNewLine _
                                             & " @REMARK,                                              " & vbNewLine _
                                             & " @SEIQ_TARIFF_CD,                                      " & vbNewLine _
                                             & " @SEIQ_ETARIFF_CD,                                     " & vbNewLine _
                                             & " @AD_3,                                                " & vbNewLine _
                                             & " @UNSO_TEHAI_KB,                                       " & vbNewLine _
                                             & " @BUY_CHU_NO,                                          " & vbNewLine _
                                             & " @AREA_CD,                                             " & vbNewLine _
                                             & " @TYUKEI_HAISO_FLG,                                    " & vbNewLine _
                                             & " @SYUKA_TYUKEI_CD,                                     " & vbNewLine _
                                             & " @HAIKA_TYUKEI_CD,                                     " & vbNewLine _
                                             & " @TRIP_NO_SYUKA,                                       " & vbNewLine _
                                             & " @TRIP_NO_TYUKEI,                                      " & vbNewLine _
                                             & " @TRIP_NO_HAIKA,                                       " & vbNewLine _
                                             & " @SYS_ENT_DATE,                                        " & vbNewLine _
                                             & " @SYS_ENT_TIME,                                        " & vbNewLine _
                                             & " @SYS_ENT_PGID,                                        " & vbNewLine _
                                             & " @SYS_ENT_USER,                                        " & vbNewLine _
                                             & " @SYS_UPD_DATE,                                        " & vbNewLine _
                                             & " @SYS_UPD_TIME,                                        " & vbNewLine _
                                             & " @SYS_UPD_PGID,                                        " & vbNewLine _
                                             & " @SYS_UPD_USER,                                        " & vbNewLine _
                                             & " @SYS_DEL_FLG                                          " & vbNewLine _
                                             & " )                                                     " & vbNewLine
#End Region

#Region "運送データ(中)新規追加 SQL INSERT句"
    ''' <summary>
    ''' 運送データ(中)新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_UNSOM As String = "INSERT INTO $LM_TRN$..F_UNSO_M                         " & vbNewLine _
                                             & " ( 		                                               " & vbNewLine _
                                             & " NRS_BR_CD,                                            " & vbNewLine _
                                             & " UNSO_NO_L,                                            " & vbNewLine _
                                             & " UNSO_NO_M,                                            " & vbNewLine _
                                             & " GOODS_CD_NRS,                                         " & vbNewLine _
                                             & " GOODS_NM,                                             " & vbNewLine _
                                             & " UNSO_TTL_NB,                                          " & vbNewLine _
                                             & " NB_UT,                                                " & vbNewLine _
                                             & " UNSO_TTL_QT,                                          " & vbNewLine _
                                             & " QT_UT,                                                " & vbNewLine _
                                             & " HASU,                                                 " & vbNewLine _
                                             & " ZAI_REC_NO,                                           " & vbNewLine _
                                             & " UNSO_ONDO_KB,                                         " & vbNewLine _
                                             & " IRIME,                                                " & vbNewLine _
                                             & " IRIME_UT,                                             " & vbNewLine _
                                             & " BETU_WT,                                              " & vbNewLine _
                                             & " SIZE_KB,                                              " & vbNewLine _
                                             & " ZBUKA_CD,                                             " & vbNewLine _
                                             & " ABUKA_CD,                                             " & vbNewLine _
                                             & " PKG_NB,                                               " & vbNewLine _
                                             & " LOT_NO,                                               " & vbNewLine _
                                             & " REMARK,                                               " & vbNewLine _
                                             & " SYS_ENT_DATE,                                         " & vbNewLine _
                                             & " SYS_ENT_TIME,                                         " & vbNewLine _
                                             & " SYS_ENT_PGID,                                         " & vbNewLine _
                                             & " SYS_ENT_USER,                                         " & vbNewLine _
                                             & " SYS_UPD_DATE,                                         " & vbNewLine _
                                             & " SYS_UPD_TIME,                                         " & vbNewLine _
                                             & " SYS_UPD_PGID,                                         " & vbNewLine _
                                             & " SYS_UPD_USER,                                         " & vbNewLine _
                                             & " SYS_DEL_FLG                                           " & vbNewLine _
                                             & " ) VALUES (                                            " & vbNewLine _
                                             & " @NRS_BR_CD,                                           " & vbNewLine _
                                             & " @UNSO_NO_L,                                           " & vbNewLine _
                                             & " @UNSO_NO_M,                                           " & vbNewLine _
                                             & " @GOODS_CD_NRS,                                        " & vbNewLine _
                                             & " @GOODS_NM,                                            " & vbNewLine _
                                             & " @UNSO_TTL_NB,                                         " & vbNewLine _
                                             & " @NB_UT,                                               " & vbNewLine _
                                             & " @UNSO_TTL_QT,                                         " & vbNewLine _
                                             & " @QT_UT,                                               " & vbNewLine _
                                             & " @HASU,                                                " & vbNewLine _
                                             & " @ZAI_REC_NO,                                          " & vbNewLine _
                                             & " @UNSO_ONDO_KB,                                        " & vbNewLine _
                                             & " @IRIME,                                               " & vbNewLine _
                                             & " @IRIME_UT,                                            " & vbNewLine _
                                             & " @BETU_WT,                                             " & vbNewLine _
                                             & " @SIZE_KB,                                             " & vbNewLine _
                                             & " @ZBUKA_CD,                                            " & vbNewLine _
                                             & " @ABUKA_CD,                                            " & vbNewLine _
                                             & " @PKG_NB,                                              " & vbNewLine _
                                             & " @LOT_NO,                                              " & vbNewLine _
                                             & " @REMARK,                                              " & vbNewLine _
                                             & " @SYS_ENT_DATE,                                        " & vbNewLine _
                                             & " @SYS_ENT_TIME,                                        " & vbNewLine _
                                             & " @SYS_ENT_PGID,                                        " & vbNewLine _
                                             & " @SYS_ENT_USER,                                        " & vbNewLine _
                                             & " @SYS_UPD_DATE,                                        " & vbNewLine _
                                             & " @SYS_UPD_TIME,                                        " & vbNewLine _
                                             & " @SYS_UPD_PGID,                                        " & vbNewLine _
                                             & " @SYS_UPD_USER,                                        " & vbNewLine _
                                             & " @SYS_DEL_FLG                                          " & vbNewLine _
                                             & " )                                                     " & vbNewLine
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

#End Region

    'WIT対応 入荷データ一括取込対応 kasama Start
#Region "入荷データ一括取込対応"

#Region "B_INKA_S"

    Private Const SQL_INSERT_B_INKA_S As String = _
                " INSERT INTO                            " & vbNewLine _
                & "     $LM_TRN$..B_INKA_S                 " & vbNewLine _
                & " (                                      " & vbNewLine _
                & "     NRS_BR_CD                          " & vbNewLine _
                & "     , INKA_NO_L                        " & vbNewLine _
                & "     , INKA_NO_M                        " & vbNewLine _
                & "     , INKA_NO_S                        " & vbNewLine _
                & "     , ZAI_REC_NO                       " & vbNewLine _
                & "     , LOT_NO                           " & vbNewLine _
                & "     , LOCA                             " & vbNewLine _
                & "     , TOU_NO                           " & vbNewLine _
                & "     , SITU_NO                          " & vbNewLine _
                & "     , ZONE_CD                          " & vbNewLine _
                & "     , KONSU                            " & vbNewLine _
                & "     , HASU                             " & vbNewLine _
                & "     , IRIME                            " & vbNewLine _
                & "     , BETU_WT                          " & vbNewLine _
                & "     , SERIAL_NO                        " & vbNewLine _
                & "     , GOODS_COND_KB_1                  " & vbNewLine _
                & "     , GOODS_COND_KB_2                  " & vbNewLine _
                & "     , GOODS_COND_KB_3                  " & vbNewLine _
                & "     , GOODS_CRT_DATE                   " & vbNewLine _
                & "     , LT_DATE                          " & vbNewLine _
                & "     , SPD_KB                           " & vbNewLine _
                & "     , OFB_KB                           " & vbNewLine _
                & "     , DEST_CD                          " & vbNewLine _
                & "     , REMARK                           " & vbNewLine _
                & "     , ALLOC_PRIORITY                   " & vbNewLine _
                & "     , REMARK_OUT                       " & vbNewLine _
                & "     , SYS_ENT_DATE                     " & vbNewLine _
                & "     , SYS_ENT_TIME                     " & vbNewLine _
                & "     , SYS_ENT_PGID                     " & vbNewLine _
                & "     , SYS_ENT_USER                     " & vbNewLine _
                & "     , SYS_UPD_DATE                     " & vbNewLine _
                & "     , SYS_UPD_TIME                     " & vbNewLine _
                & "     , SYS_UPD_PGID                     " & vbNewLine _
                & "     , SYS_UPD_USER                     " & vbNewLine _
                & "     , SYS_DEL_FLG                      " & vbNewLine _
                & " )                                      " & vbNewLine _
                & " VALUES                                 " & vbNewLine _
                & " (                                      " & vbNewLine _
                & "     @NRS_BR_CD                         " & vbNewLine _
                & "     , @INKA_NO_L                       " & vbNewLine _
                & "     , @INKA_NO_M                       " & vbNewLine _
                & "     , @INKA_NO_S                       " & vbNewLine _
                & "     , @ZAI_REC_NO                      " & vbNewLine _
                & "     , @LOT_NO                          " & vbNewLine _
                & "     , @LOCA                            " & vbNewLine _
                & "     , @TOU_NO                          " & vbNewLine _
                & "     , @SITU_NO                         " & vbNewLine _
                & "     , @ZONE_CD                         " & vbNewLine _
                & "     , @KONSU                           " & vbNewLine _
                & "     , @HASU                            " & vbNewLine _
                & "     , @IRIME                           " & vbNewLine _
                & "     , @BETU_WT                         " & vbNewLine _
                & "     , @SERIAL_NO                       " & vbNewLine _
                & "     , @GOODS_COND_KB_1                 " & vbNewLine _
                & "     , @GOODS_COND_KB_2                 " & vbNewLine _
                & "     , @GOODS_COND_KB_3                 " & vbNewLine _
                & "     , @GOODS_CRT_DATE                  " & vbNewLine _
                & "     , @LT_DATE                         " & vbNewLine _
                & "     , @SPD_KB                          " & vbNewLine _
                & "     , @OFB_KB                          " & vbNewLine _
                & "     , @DEST_CD                         " & vbNewLine _
                & "     , @REMARK                          " & vbNewLine _
                & "     , @ALLOC_PRIORITY                  " & vbNewLine _
                & "     , @REMARK_OUT                      " & vbNewLine _
                & "     , @SYS_ENT_DATE                    " & vbNewLine _
                & "     , @SYS_ENT_TIME                    " & vbNewLine _
                & "     , @SYS_ENT_PGID                    " & vbNewLine _
                & "     , @SYS_ENT_USER                    " & vbNewLine _
                & "     , @SYS_UPD_DATE                    " & vbNewLine _
                & "     , @SYS_UPD_TIME                    " & vbNewLine _
                & "     , @SYS_UPD_PGID                    " & vbNewLine _
                & "     , @SYS_UPD_USER                    " & vbNewLine _
                & "     , '0'                              " & vbNewLine _
                & " )"

#End Region

#Region "D_ZAI_TRS"

    Private Const SQL_INSERT_D_ZAI_TRS As String = _
                " INSERT INTO                           " & vbNewLine _
                & "     $LM_TRN$..D_ZAI_TRS             " & vbNewLine _
                & " (                                   " & vbNewLine _
                & "     NRS_BR_CD                       " & vbNewLine _
                & "     , ZAI_REC_NO                    " & vbNewLine _
                & "     , WH_CD                         " & vbNewLine _
                & "     , TOU_NO                        " & vbNewLine _
                & "     , SITU_NO                       " & vbNewLine _
                & "     , ZONE_CD                       " & vbNewLine _
                & "     , LOCA                          " & vbNewLine _
                & "     , LOT_NO                        " & vbNewLine _
                & "     , CUST_CD_L                     " & vbNewLine _
                & "     , CUST_CD_M                     " & vbNewLine _
                & "     , GOODS_CD_NRS                  " & vbNewLine _
                & "     , GOODS_KANRI_NO                " & vbNewLine _
                & "     , INKA_NO_L                     " & vbNewLine _
                & "     , INKA_NO_M                     " & vbNewLine _
                & "     , INKA_NO_S                     " & vbNewLine _
                & "     , ALLOC_PRIORITY                " & vbNewLine _
                & "     , RSV_NO                        " & vbNewLine _
                & "     , SERIAL_NO                     " & vbNewLine _
                & "     , HOKAN_YN                      " & vbNewLine _
                & "     , TAX_KB                        " & vbNewLine _
                & "     , GOODS_COND_KB_1               " & vbNewLine _
                & "     , GOODS_COND_KB_2               " & vbNewLine _
                & "     , GOODS_COND_KB_3               " & vbNewLine _
                & "     , OFB_KB                        " & vbNewLine _
                & "     , SPD_KB                        " & vbNewLine _
                & "     , REMARK_OUT                    " & vbNewLine _
                & "     , PORA_ZAI_NB                   " & vbNewLine _
                & "     , ALCTD_NB                      " & vbNewLine _
                & "     , ALLOC_CAN_NB                  " & vbNewLine _
                & "     , IRIME                         " & vbNewLine _
                & "     , PORA_ZAI_QT                   " & vbNewLine _
                & "     , ALCTD_QT                      " & vbNewLine _
                & "     , ALLOC_CAN_QT                  " & vbNewLine _
                & "     , INKO_DATE                     " & vbNewLine _
                & "     , INKO_PLAN_DATE                " & vbNewLine _
                & "     , ZERO_FLAG                     " & vbNewLine _
                & "     , LT_DATE                       " & vbNewLine _
                & "     , GOODS_CRT_DATE                " & vbNewLine _
                & "     , DEST_CD_P                     " & vbNewLine _
                & "     , REMARK                        " & vbNewLine _
                & "     , SMPL_FLAG                     " & vbNewLine _
                & "     , SYS_ENT_DATE                  " & vbNewLine _
                & "     , SYS_ENT_TIME                  " & vbNewLine _
                & "     , SYS_ENT_PGID                  " & vbNewLine _
                & "     , SYS_ENT_USER                  " & vbNewLine _
                & "     , SYS_UPD_DATE                  " & vbNewLine _
                & "     , SYS_UPD_TIME                  " & vbNewLine _
                & "     , SYS_UPD_PGID                  " & vbNewLine _
                & "     , SYS_UPD_USER                  " & vbNewLine _
                & "     , SYS_DEL_FLG                   " & vbNewLine _
                & " )                                   " & vbNewLine _
                & " VALUES                              " & vbNewLine _
                & " (                                   " & vbNewLine _
                & "     @NRS_BR_CD                      " & vbNewLine _
                & "     , @ZAI_REC_NO                   " & vbNewLine _
                & "     , @WH_CD                        " & vbNewLine _
                & "     , @TOU_NO                       " & vbNewLine _
                & "     , @SITU_NO                      " & vbNewLine _
                & "     , @ZONE_CD                      " & vbNewLine _
                & "     , @LOCA                         " & vbNewLine _
                & "     , @LOT_NO                       " & vbNewLine _
                & "     , @CUST_CD_L                    " & vbNewLine _
                & "     , @CUST_CD_M                    " & vbNewLine _
                & "     , @GOODS_CD_NRS                 " & vbNewLine _
                & "     , @GOODS_KANRI_NO               " & vbNewLine _
                & "     , @INKA_NO_L                    " & vbNewLine _
                & "     , @INKA_NO_M                    " & vbNewLine _
                & "     , @INKA_NO_S                    " & vbNewLine _
                & "     , @ALLOC_PRIORITY               " & vbNewLine _
                & "     , @RSV_NO                       " & vbNewLine _
                & "     , @SERIAL_NO                    " & vbNewLine _
                & "     , @HOKAN_YN                     " & vbNewLine _
                & "     , @TAX_KB                       " & vbNewLine _
                & "     , @GOODS_COND_KB_1              " & vbNewLine _
                & "     , @GOODS_COND_KB_2              " & vbNewLine _
                & "     , @GOODS_COND_KB_3              " & vbNewLine _
                & "     , @OFB_KB                       " & vbNewLine _
                & "     , @SPD_KB                       " & vbNewLine _
                & "     , @REMARK_OUT                   " & vbNewLine _
                & "     , @PORA_ZAI_NB                  " & vbNewLine _
                & "     , @ALCTD_NB                     " & vbNewLine _
                & "     , @ALLOC_CAN_NB                 " & vbNewLine _
                & "     , @IRIME                        " & vbNewLine _
                & "     , @PORA_ZAI_QT                  " & vbNewLine _
                & "     , @ALCTD_QT                     " & vbNewLine _
                & "     , @ALLOC_CAN_QT                 " & vbNewLine _
                & "     , @INKO_DATE                    " & vbNewLine _
                & "     , @INKO_PLAN_DATE               " & vbNewLine _
                & "     , @ZERO_FLAG                    " & vbNewLine _
                & "     , @LT_DATE                      " & vbNewLine _
                & "     , @GOODS_CRT_DATE               " & vbNewLine _
                & "     , @DEST_CD_P                    " & vbNewLine _
                & "     , @REMARK                       " & vbNewLine _
                & "     , @SMPL_FLAG                    " & vbNewLine _
                & "     , @SYS_ENT_DATE                 " & vbNewLine _
                & "     , @SYS_ENT_TIME                 " & vbNewLine _
                & "     , @SYS_ENT_PGID                 " & vbNewLine _
                & "     , @SYS_ENT_USER                 " & vbNewLine _
                & "     , @SYS_UPD_DATE                 " & vbNewLine _
                & "     , @SYS_UPD_TIME                 " & vbNewLine _
                & "     , @SYS_UPD_PGID                 " & vbNewLine _
                & "     , @SYS_UPD_USER                 " & vbNewLine _
                & "     , '0'                           " & vbNewLine _
                & " )"

#End Region

#End Region
    'WIT対応 入荷データ一括取込対応 kasama End

    '2014.05.16 追加START
#Region "出荷ピッキングWK情報登録"

    Private Const SQL_INSERT_C_OUTKA_PICK_WK As String = _
              "INSERT                                                                                             " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_PICK_WK                                                                      " & vbNewLine _
            & "(                                                                                                  " & vbNewLine _
            & "      NRS_BR_CD                                                                                    " & vbNewLine _
            & "    , OUTKA_NO_L                                                                                   " & vbNewLine _
            & "    , OUTKA_NO_M                                                                                   " & vbNewLine _
            & "    , SERIAL_NO                                                                                    " & vbNewLine _
            & "    , HIKI_STATE_KBN                                                                               " & vbNewLine _
            & "    , SYS_ENT_DATE                                                                                 " & vbNewLine _
            & "    , SYS_ENT_TIME                                                                                 " & vbNewLine _
            & "    , SYS_ENT_PGID                                                                                 " & vbNewLine _
            & "    , SYS_ENT_USER                                                                                 " & vbNewLine _
            & "    , SYS_UPD_DATE                                                                                 " & vbNewLine _
            & "    , SYS_UPD_TIME                                                                                 " & vbNewLine _
            & "    , SYS_UPD_PGID                                                                                 " & vbNewLine _
            & "    , SYS_UPD_USER                                                                                 " & vbNewLine _
            & "    , SYS_DEL_FLG                                                                                  " & vbNewLine _
            & ")VALUES(                                                                                           " & vbNewLine _
            & "      @NRS_BR_CD                                                                                   " & vbNewLine _
            & "    , @OUTKA_NO_L                                                                                  " & vbNewLine _
            & "    , @OUTKA_NO_M                                                                                  " & vbNewLine _
            & "    , @SERIAL_NO                                                                                   " & vbNewLine _
            & "    , '00'                                                                                         " & vbNewLine _
            & "    , @SYS_ENT_DATE                                                                                " & vbNewLine _
            & "    , @SYS_ENT_TIME                                                                                " & vbNewLine _
            & "    , @SYS_ENT_PGID                                                                                " & vbNewLine _
            & "    , @SYS_ENT_USER                                                                                " & vbNewLine _
            & "    , @SYS_UPD_DATE                                                                                " & vbNewLine _
            & "    , @SYS_UPD_TIME                                                                                " & vbNewLine _
            & "    , @SYS_UPD_PGID                                                                                " & vbNewLine _
            & "    , @SYS_UPD_USER                                                                                " & vbNewLine _
            & "    , '0'                                                                                          " & vbNewLine _
            & ")                                                                                                   " & vbNewLine
    '2014.05.16 追加END

#End Region

#End Region

#Region "DELETE"

#Region "作業レコードの削除"

#Region "作業レコードの削除 SQL DELETE句"

    ''' <summary>
    ''' 作業レコードの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SAGYO As String = "DELETE FROM $LM_TRN$..E_SAGYO " & vbNewLine

#End Region

#Region "作業レコードの削除 SQL WHERE句"

    'START YANAI 要望番号849
    '''' <summary>
    '''' 作業レコードの削除 SQL WHERE句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_DELETE_WHERE_SAGYO As String = "WHERE                                                                      " & vbNewLine _
    '                                               & " NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
    '                                               & " AND                                                                       " & vbNewLine _
    '                                               & " SUBSTRING(E_SAGYO.INOUTKA_NO_LM,1,9) = @INKA_NO_L                         " & vbNewLine _
    '                                               & " AND                                                                       " & vbNewLine _
    '                                               & " E_SAGYO.INOUTKA_NO_LM <> @INKA_NO_L + '000'                               " & vbNewLine
    ''' <summary>
    ''' 作業レコードの削除 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WHERE_SAGYO As String = "WHERE                                                                      " & vbNewLine _
                                                   & " NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                                   & " AND                                                                       " & vbNewLine _
                                                   & " SUBSTRING(E_SAGYO.INOUTKA_NO_LM,1,9) = @INKA_NO_L                         " & vbNewLine _
                                                   & " AND                                                                       " & vbNewLine _
                                                   & " E_SAGYO.INOUTKA_NO_LM <> @INKA_NO_L + '000'                               " & vbNewLine _
                                                   & " AND                                                                       " & vbNewLine _
                                                   & " E_SAGYO.IOZS_KB = '11'                                                    " & vbNewLine
    'END YANAI 要望番号849

#End Region

#End Region

#End Region

#Region "UPDATE"

#Region "印刷"

    ''' <summary>
    ''' 入荷(大)更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_PRINT As String = "UPDATE $LM_TRN$..B_INKA_L SET                     " & vbNewLine _
                                         & " INKA_STATE_KB     = @INKA_STATE_KB                   " & vbNewLine _
                                         & ",$UPDATE1$         = @SYS_UPD_DATE                    " & vbNewLine _
                                         & ",$UPDATE2$         = @SYS_UPD_USER                    " & vbNewLine _
                                         & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                         & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                         & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                         & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                         & "WHERE NRS_BR_CD    = @NRS_BR_CD                       " & vbNewLine _
                                         & "  AND INKA_NO_L    = @INKA_NO_L                       " & vbNewLine _
                                         & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE                " & vbNewLine _
                                         & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME                " & vbNewLine

#End Region

#Region "出荷データ作成"

    ''' <summary>
    ''' 入荷(大)更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MAKE As String = "UPDATE $LM_TRN$..B_INKA_L SET                         " & vbNewLine _
                                            & " INKA_STATE_KB     = @INKA_STATE_KB                   " & vbNewLine _
                                            & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                            & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                            & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                            & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD                       " & vbNewLine _
                                            & "  AND INKA_NO_L    = @INKA_NO_L                       " & vbNewLine _
                                            & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE                " & vbNewLine _
                                            & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME                " & vbNewLine

#End Region

#Region "入荷報告取消"

    ''' <summary>
    ''' 入荷(大)更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_HOUKOKU_CANCEL As String = "UPDATE $LM_TRN$..B_INKA_L SET                     " & vbNewLine _
                                                             & " INKA_STATE_KB     = @INKA_STATE_KB                   " & vbNewLine _
                                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                       " & vbNewLine _
                                                             & "  AND INKA_NO_L    = @INKA_NO_L                       " & vbNewLine _
                                                             & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE                " & vbNewLine _
                                                             & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME                " & vbNewLine

#End Region

    'WIT対応 入荷データ一括取込対応 kasama Start
#Region "入荷データ一括取込対応"

#Region "B_INKA_L"

    ''' <summary>
    ''' 入荷(大)更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_IKKATU_TORIKOMI As String = "UPDATE $LM_TRN$..B_INKA_L SET                     " & vbNewLine _
                                                             & " INKA_STATE_KB     = @INKA_STATE_KB                   " & vbNewLine _
                                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                       " & vbNewLine _
                                                             & "  AND INKA_NO_L    = @INKA_NO_L                       " & vbNewLine _
                                                             & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE                " & vbNewLine _
                                                             & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME                " & vbNewLine

#End Region

#Region "B_INKA_WK"

    Private Const SQL_UPDATE_B_INKA_WK As String = _
                  " UPDATE                                          " & vbNewLine _
                & "     $LM_TRN$..B_INKA_WK                         " & vbNewLine _
                & " SET                                             " & vbNewLine _
                & "       INKA_NO_S = @INKA_NO_S                    " & vbNewLine _
                & "     , TOU_NO = @TOU_NO                          " & vbNewLine _
                & "     , SITU_NO = @SITU_NO                        " & vbNewLine _
                & "     , ZONE_CD = @ZONE_CD                        " & vbNewLine _
                & "     , LOCA = @LOCA                              " & vbNewLine _
                & "     , NYUKO_KAKUTEI_FLG = '01'                  " & vbNewLine _
                & "     , NYUKO_TANTO = @NYUKO_TANTO                " & vbNewLine _
                & "     , NYUKO_DATE = @NYUKO_DATE                  " & vbNewLine _
                & "     , NYUKO_TIME = @NYUKO_TIME                  " & vbNewLine _
                & "     , SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
                & "     , SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
                & "     , SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
                & "     , SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
                & " WHERE                                           " & vbNewLine _
                & "     NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                & "     AND INKA_NO_L = @INKA_NO_L                  " & vbNewLine _
                & "     AND INKA_NO_M = @INKA_NO_M                  " & vbNewLine _
                & "     AND SEQ = @SEQ                              " & vbNewLine

    Private Const SQL_UPDATE_B_INKA_WK_FFEM As String = _
              " UPDATE                                          " & vbNewLine _
            & "     $LM_TRN$..B_INKA_WK                         " & vbNewLine _
            & " SET                                             " & vbNewLine _
            & "       INKA_NO_S = @INKA_NO_S                    " & vbNewLine _
            & "     , TOU_NO = @TOU_NO                          " & vbNewLine _
            & "     , SITU_NO = @SITU_NO                        " & vbNewLine _
            & "     , ZONE_CD = @ZONE_CD                        " & vbNewLine _
            & "     , LOCA = @LOCA                              " & vbNewLine _
            & "     , NYUKO_KAKUTEI_FLG = '01'                  " & vbNewLine _
            & "     , NYUKO_TANTO = @NYUKO_TANTO                " & vbNewLine _
            & "     , NYUKO_DATE = @NYUKO_DATE                  " & vbNewLine _
            & "     , NYUKO_TIME = @NYUKO_TIME                  " & vbNewLine _
            & "     , SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
            & "     , SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
            & "     , SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
            & "     , SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
            & " WHERE                                           " & vbNewLine _
            & "     NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
            & "     AND INKA_NO_L = @INKA_NO_L                  " & vbNewLine _
            & "     AND INKA_NO_M = @INKA_NO_M                  " & vbNewLine

#End Region

#End Region
    'WIT対応 入荷データ一括取込対応 kasama End

#End Region

    '2014.04.17 CALT連携対応 Ri --ST--
#Region "入荷予定データ作成対応"
#Region "SELECT"
    ''' <summary>
    ''' INSERT用データ抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKA_LMS As String = _
                    " SELECT                                                        " & vbNewLine _
                  & "      BIL.NRS_BR_CD                   AS NRS_BR_CD             " & vbNewLine _
                  & "     ,BIL.INKA_NO_L                   AS INKA_NO_L             " & vbNewLine _
                  & "     ,BIM.INKA_NO_M                   AS INKA_NO_M             " & vbNewLine _
                  & "     ,BIS.INKA_NO_S                   AS INKA_NO_S             " & vbNewLine _
                  & " --  ,IsNull(BIP.SEND_SEQ,0) +1       AS SEND_SEQ              " & vbNewLine _
                  & "     ,CASE                                                     " & vbNewLine _
                  & "       WHEN IsNull(BIP.SEND_SEQ,0)    = 0                      " & vbNewLine _
                  & "        THEN 1                                                 " & vbNewLine _
                  & "       ELSE BIP.SEND_SEQ + 2                                   " & vbNewLine _
                  & "      END                             AS SEND_SEQ              " & vbNewLine _
                  & "     ,CASE                                                     " & vbNewLine _
                  & "       WHEN IsNull(BIP.INKA_NO_L,'')   = ''                    " & vbNewLine _
                  & "        THEN '0'                                               " & vbNewLine _
                  & "       ELSE  '1'                                               " & vbNewLine _
                  & "      END                             AS DATA_KBN              " & vbNewLine _
                  & "     ,BIL.WH_CD                       AS WH_CD                 " & vbNewLine _
                  & "     ,BIL.CUST_CD_L                   AS CUST_CD_L             " & vbNewLine _
                  & "     ,BIL.CUST_CD_M                   AS CUST_CD_M             " & vbNewLine _
                  & "     ,MCUS.CUST_NM_L                  AS CUST_NM_L             " & vbNewLine _
                  & "     ,BIL.INKA_DATE                   AS INKA_DATE             " & vbNewLine _
                  & "     ,BIL.BUYER_ORD_NO_L              AS BUYER_ORD_NO_L        " & vbNewLine _
                  & "     ,BIL.OUTKA_FROM_ORD_NO_L         AS OUTKA_FROM_ORD_NO_L   " & vbNewLine _
                  & "     ,BIL.REMARK                      AS REMARK_L              " & vbNewLine _
                  & "     ,BIL.REMARK_OUT                  AS REMARK_OUT_L          " & vbNewLine _
                  & "     ,BIM.GOODS_CD_NRS                AS GOODS_CD_NRS          " & vbNewLine _
                  & "     ,MGDS.GOODS_CD_CUST              AS GOODS_CD_CUST         " & vbNewLine _
                  & "     ,MGDS.GOODS_NM_1                 AS GOODS_NM_1            " & vbNewLine _
                  & "     ,BIM.OUTKA_FROM_ORD_NO_M         AS OUTKA_FROM_ORD_NO_M   " & vbNewLine _
                  & "     ,BIM.BUYER_ORD_NO_M              AS BUYER_ORD_NO_M        " & vbNewLine _
                  & "     ,BIS.LOT_NO                      AS LOT_NO                " & vbNewLine _
                  & "     ,BIM.REMARK                      AS REMARK_M              " & vbNewLine _
                  & "     ,(( BIS.KONSU * MGDS.PKG_NB )                             " & vbNewLine _
                  & "       + BIS.HASU)                    AS INKA_NB               " & vbNewLine _
                  & "     ,((( BIS.KONSU * MGDS.PKG_NB )                            " & vbNewLine _
                  & "        + BIS.HASU)                                            " & vbNewLine _
                  & "       * BIS.IRIME                                             " & vbNewLine _
                  & "       * BIS.BETU_WT)                 AS INKA_WT               " & vbNewLine _
                  & "     ,BIS.KONSU                       AS KONSU                 " & vbNewLine _
                  & "     ,BIS.HASU                        AS HASU                  " & vbNewLine _
                  & "     ,BIS.IRIME                       AS IRIME                 " & vbNewLine _
                  & "     ,BIS.BETU_WT                     AS BETU_WT               " & vbNewLine _
                  & "     ,MGDS.PKG_NB                     AS PKG_NB                " & vbNewLine _
                  & "     ,MGDS.PKG_UT                     AS PKG_UT                " & vbNewLine _
                  & "     ,MGDS.JAN_CD                     AS JAN_CD                " & vbNewLine _
                  & "     ,BIS.SERIAL_NO                   AS SERIAL_NO             " & vbNewLine _
                  & "     ,MGDS.ONDO_KB                    AS ONDO_KB               " & vbNewLine _
                  & "     ,MGDS.ONDO_STR_DATE              AS ONDO_STR_DATE         " & vbNewLine _
                  & "     ,MGDS.ONDO_END_DATE              AS ONDO_END_DATE         " & vbNewLine _
                  & "     ,MGDS.ONDO_MX                    AS ONDO_MX               " & vbNewLine _
                  & "     ,MGDS.ONDO_MM                    AS ONDO_MM               " & vbNewLine _
                  & "     ,BIS.GOODS_COND_KB_1             AS GOODS_COND_KB_1       " & vbNewLine _
                  & "     ,BIS.GOODS_COND_KB_2             AS GOODS_COND_KB_2       " & vbNewLine _
                  & "     ,BIS.GOODS_COND_KB_3             AS GOODS_COND_KB_3       " & vbNewLine _
                  & "     ,BIS.GOODS_CRT_DATE              AS GOODS_CRT_DATE        " & vbNewLine _
                  & "     ,BIS.LT_DATE                     AS LT_DATE               " & vbNewLine _
                  & "     ,BIS.SPD_KB                      AS SPD_KB                " & vbNewLine _
                  & "     ,BIS.OFB_KB                      AS OFB_KB                " & vbNewLine _
                  & "     ,BIS.DEST_CD                     AS DEST_CD               " & vbNewLine _
                  & "     ,BIS.REMARK                      AS REMARK_S              " & vbNewLine _
                  & "     ,BIS.ALLOC_PRIORITY              AS ALLOC_PRIORITY        " & vbNewLine _
                  & "     ,BIS.REMARK_OUT                  AS REMARK_OUT_S          " & vbNewLine _
                  & "     ,'2'                             AS SEND_SHORI_FLG        " & vbNewLine _
                  & "     ,'0'                             AS SYS_DEL_FLG           " & vbNewLine _
                  & " FROM                                                          " & vbNewLine _
                  & "      $LM_TRN$..B_INKA_L             AS BIL                    " & vbNewLine _
                  & " LEFT JOIN                                                     " & vbNewLine _
                  & "      $LM_TRN$..B_INKA_M             AS BIM                    " & vbNewLine _
                  & "   ON BIM.SYS_DEL_FLG                  = '0'                   " & vbNewLine _
                  & "  AND BIL.NRS_BR_CD                    = BIM.NRS_BR_CD         " & vbNewLine _
                  & "  AND BIL.INKA_NO_L                    = BIM.INKA_NO_L         " & vbNewLine _
                  & " LEFT JOIN                                                     " & vbNewLine _
                  & "      $LM_TRN$..B_INKA_S             AS BIS                    " & vbNewLine _
                  & "   ON BIS.SYS_DEL_FLG                  = '0'                   " & vbNewLine _
                  & "  AND BIL.NRS_BR_CD                    = BIS.NRS_BR_CD         " & vbNewLine _
                  & "  AND BIL.INKA_NO_L                    = BIS.INKA_NO_L         " & vbNewLine _
                  & "  AND BIM.INKA_NO_M                    = BIS.INKA_NO_M         " & vbNewLine _
                  & " LEFT JOIN                                                     " & vbNewLine _
                  & "     (                                                         " & vbNewLine _
                  & "       SELECT                                                      " & vbNewLine _
                  & "             BIP_IN2.NRS_BR_CD                  AS NRS_BR_CD       " & vbNewLine _
                  & "            ,BIP_IN2.INKA_NO_L                  AS INKA_NO_L       " & vbNewLine _
                  & "            ,BIP_IN2.INKA_NO_M                  AS INKA_NO_M       " & vbNewLine _
                  & "            ,BIP_IN2.INKA_NO_S                  AS INKA_NO_S       " & vbNewLine _
                  & "            ,MAX(GET_SEQ.SEND_SEQ)              AS SEND_SEQ        " & vbNewLine _
                  & "       FROM $LM_TRN$..B_INKA_PLAN_SEND          AS BIP_IN2         " & vbNewLine _
                  & "       LEFT JOIN                                                   " & vbNewLine _
                  & "           (                                                       " & vbNewLine _
                  & "             SELECT                                                " & vbNewLine _
                  & "                   BIP_IN1.NRS_BR_CD           AS NRS_BR_CD        " & vbNewLine _
                  & "                  ,BIP_IN1.INKA_NO_L           AS INKA_NO_L        " & vbNewLine _
                  & "                  ,BIP_IN1.INKA_NO_M           AS INKA_NO_M        " & vbNewLine _
                  & "                  ,BIP_IN1.INKA_NO_S           AS INKA_NO_S        " & vbNewLine _
                  & "                  ,BIP_IN1.SEND_SEQ            AS SEND_SEQ         " & vbNewLine _
                  & "             FROM $LM_TRN$..B_INKA_PLAN_SEND   AS BIP_IN1          " & vbNewLine _
                  & "             WHERE                                                 " & vbNewLine _
                  & "                   BIP_IN1.SYS_DEL_FLG          = '0'              " & vbNewLine _
                  & "               AND BIP_IN1.NRS_BR_CD            = @NRS_BR_CD  --@  " & vbNewLine _
                  & "               AND BIP_IN1.INKA_NO_L            = @INKA_NO_L  --@  " & vbNewLine _
                  & "               AND BIP_IN1.DATA_KBN            <> '2'              " & vbNewLine _
                  & "             GROUP BY                                              " & vbNewLine _
                  & "                   BIP_IN1.NRS_BR_CD                               " & vbNewLine _
                  & "                  ,BIP_IN1.INKA_NO_L                               " & vbNewLine _
                  & "                  ,BIP_IN1.INKA_NO_M                               " & vbNewLine _
                  & "                  ,BIP_IN1.INKA_NO_S                               " & vbNewLine _
                  & "                  ,BIP_IN1.SEND_SEQ                                " & vbNewLine _
                  & "            ) AS GET_SEQ                                           " & vbNewLine _
                  & "          ON BIP_IN2.NRS_BR_CD             = GET_SEQ.NRS_BR_CD     " & vbNewLine _
                  & "         AND BIP_IN2.INKA_NO_L             = GET_SEQ.INKA_NO_L     " & vbNewLine _
                  & "         AND BIP_IN2.INKA_NO_M             = GET_SEQ.INKA_NO_M     " & vbNewLine _
                  & "         AND BIP_IN2.INKA_NO_S             = GET_SEQ.INKA_NO_S     " & vbNewLine _
                  & "       WHERE                                                       " & vbNewLine _
                  & "             BIP_IN2.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                  & "         AND BIP_IN2.NRS_BR_CD             = @NRS_BR_CD    --@     " & vbNewLine _
                  & "         AND BIP_IN2.INKA_NO_L             = @INKA_NO_L    --@     " & vbNewLine _
                  & "         AND BIP_IN2.DATA_KBN             <> '2'                   " & vbNewLine _
                  & "       GROUP BY                                                    " & vbNewLine _
                  & "             BIP_IN2.NRS_BR_CD                                     " & vbNewLine _
                  & "            ,BIP_IN2.INKA_NO_L                                     " & vbNewLine _
                  & "            ,BIP_IN2.INKA_NO_M                                     " & vbNewLine _
                  & "            ,BIP_IN2.INKA_NO_S                                     " & vbNewLine _
                  & "     ) AS BIP                                                  " & vbNewLine _
                  & "   ON BIL.NRS_BR_CD                    = BIP.NRS_BR_CD         " & vbNewLine _
                  & "  AND BIL.INKA_NO_L                    = BIP.INKA_NO_L         " & vbNewLine _
                  & "  AND BIM.INKA_NO_M                    = BIP.INKA_NO_M         " & vbNewLine _
                  & "  AND BIS.INKA_NO_S                    = BIP.INKA_NO_S         " & vbNewLine _
                  & " LEFT JOIN                                                     " & vbNewLine _
                  & "      $LM_MST$..M_CUST                  AS MCUS                " & vbNewLine _
                  & "   ON MCUS.SYS_DEL_FLG                 = '0'                   " & vbNewLine _
                  & "  AND MCUS.CUST_CD_S                   = '00'                  " & vbNewLine _
                  & "  AND MCUS.CUST_CD_SS                  = '00'                  " & vbNewLine _
                  & "  AND BIL.NRS_BR_CD                    = MCUS.NRS_BR_CD        " & vbNewLine _
                  & "  AND BIL.CUST_CD_L                    = MCUS.CUST_CD_L        " & vbNewLine _
                  & "  AND BIL.CUST_CD_M                    = MCUS.CUST_CD_M        " & vbNewLine _
                  & " LEFT JOIN                                                     " & vbNewLine _
                  & "      $LM_MST$..M_GOODS                 AS MGDS                " & vbNewLine _
                  & "   ON MGDS.SYS_DEL_FLG                 = '0'                   " & vbNewLine _
                  & "  AND BIL.NRS_BR_CD                    = MGDS.NRS_BR_CD        " & vbNewLine _
                  & "  AND BIM.GOODS_CD_NRS                 = MGDS.GOODS_CD_NRS     " & vbNewLine _
                  & " WHERE                                                         " & vbNewLine _
                  & "      BIL.SYS_DEL_FLG                  = '0'                   " & vbNewLine _
                  & "--AND BIM.SYS_DEL_FLG                  = '0'                   " & vbNewLine _
                  & "--AND BIS.SYS_DEL_FLG                  = '0'                   " & vbNewLine _
                  & "--AND MCUS.SYS_DEL_FLG                 = '0'                   " & vbNewLine _
                  & "  AND BIL.INKA_STATE_KB               <> '10'                  " & vbNewLine _
                  & "  AND BIL.NRS_BR_CD                    = @NRS_BR_CD        --@ " & vbNewLine _
                  & "  AND BIL.INKA_NO_L                    = @INKA_NO_L        --@ " & vbNewLine _
                  & "  AND BIL.SYS_UPD_DATE                 = @GUI_SYS_UPD_DATE --@ " & vbNewLine _
                  & "  AND BIL.SYS_UPD_TIME                 = @GUI_SYS_UPD_TIME --@ " & vbNewLine

    ''' <summary>
    ''' キャンセル報告データ抜出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEND_CANCEL As String = _
                         " SELECT                                                              " & vbNewLine _
                       & "      BIP.NRS_BR_CD                AS NRS_BR_CD                      " & vbNewLine _
                       & "     ,BIP.INKA_NO_L                AS INKA_NO_L                      " & vbNewLine _
                       & "     ,BIP.INKA_NO_M                AS INKA_NO_M                      " & vbNewLine _
                       & "     ,BIP.INKA_NO_S                AS INKA_NO_S                      " & vbNewLine _
                       & "     ,BIP.SEND_SEQ + 1             AS SEND_SEQ                       " & vbNewLine _
                       & "     ,'2'                          AS DATA_KBN                       " & vbNewLine _
                       & "     ,BIP.WH_CD                    AS WH_CD                          " & vbNewLine _
                       & "     ,BIP.CUST_CD_L                AS CUST_CD_L                      " & vbNewLine _
                       & "     ,BIP.CUST_CD_M                AS CUST_CD_M                      " & vbNewLine _
                       & "     ,BIP.CUST_NM_L                AS CUST_NM_L                      " & vbNewLine _
                       & "     ,BIP.INKA_DATE                AS INKA_DATE                      " & vbNewLine _
                       & "     ,BIP.BUYER_ORD_NO_L           AS BUYER_ORD_NO_L                 " & vbNewLine _
                       & "     ,BIP.OUTKA_FROM_ORD_NO_L      AS OUTKA_FROM_ORD_NO_L            " & vbNewLine _
                       & "     ,BIP.REMARK_L                 AS REMARK_L                       " & vbNewLine _
                       & "     ,BIP.REMARK_OUT_L             AS REMARK_OUT_L                   " & vbNewLine _
                       & "     ,BIP.GOODS_CD_NRS             AS GOODS_CD_NRS                   " & vbNewLine _
                       & "     ,BIP.GOODS_CD_CUST            AS GOODS_CD_CUST                  " & vbNewLine _
                       & "     ,BIP.GOODS_NM_1               AS GOODS_NM_1                     " & vbNewLine _
                       & "     ,BIP.OUTKA_FROM_ORD_NO_M      AS OUTKA_FROM_ORD_NO_M            " & vbNewLine _
                       & "     ,BIP.BUYER_ORD_NO_M           AS BUYER_ORD_NO_M                 " & vbNewLine _
                       & "     ,BIP.LOT_NO                   AS LOT_NO                         " & vbNewLine _
                       & "     ,BIP.REMARK_M                 AS REMARK_M                       " & vbNewLine _
                       & "     ,BIP.INKA_NB                  AS INKA_NB                        " & vbNewLine _
                       & "     ,BIP.INKA_WT                  AS INKA_WT                        " & vbNewLine _
                       & "     ,BIP.KONSU                    AS KONSU                          " & vbNewLine _
                       & "     ,BIP.HASU                     AS HASU                           " & vbNewLine _
                       & "     ,BIP.IRIME                    AS IRIME                          " & vbNewLine _
                       & "     ,BIP.BETU_WT                  AS BETU_WT                        " & vbNewLine _
                       & "     ,BIP.PKG_NB                   AS PKG_NB                         " & vbNewLine _
                       & "     ,BIP.PKG_UT                   AS PKG_UT                         " & vbNewLine _
                       & "     ,BIP.JAN_CD                   AS JAN_CD                         " & vbNewLine _
                       & "     ,BIP.SERIAL_NO                AS SERIAL_NO                      " & vbNewLine _
                       & "     ,BIP.ONDO_KB                  AS ONDO_KB                        " & vbNewLine _
                       & "     ,BIP.ONDO_STR_DATE            AS ONDO_STR_DATE                  " & vbNewLine _
                       & "     ,BIP.ONDO_END_DATE            AS ONDO_END_DATE                  " & vbNewLine _
                       & "     ,BIP.ONDO_MX                  AS ONDO_MX                        " & vbNewLine _
                       & "     ,BIP.ONDO_MM                  AS ONDO_MM                        " & vbNewLine _
                       & "     ,BIP.GOODS_COND_KB_1          AS GOODS_COND_KB_1                " & vbNewLine _
                       & "     ,BIP.GOODS_COND_KB_2          AS GOODS_COND_KB_2                " & vbNewLine _
                       & "     ,BIP.GOODS_COND_KB_3          AS GOODS_COND_KB_3                " & vbNewLine _
                       & "     ,BIP.GOODS_CRT_DATE           AS GOODS_CRT_DATE                 " & vbNewLine _
                       & "     ,BIP.LT_DATE                  AS LT_DATE                        " & vbNewLine _
                       & "     ,BIP.SPD_KB                   AS SPD_KB                         " & vbNewLine _
                       & "     ,BIP.OFB_KB                   AS OFB_KB                         " & vbNewLine _
                       & "     ,BIP.DEST_CD                  AS DEST_CD                        " & vbNewLine _
                       & "     ,BIP.REMARK_S                 AS REMARK_S                       " & vbNewLine _
                       & "     ,BIP.ALLOC_PRIORITY           AS ALLOC_PRIORITY                 " & vbNewLine _
                       & "     ,BIP.REMARK_OUT_S             AS REMARK_OUT_S                   " & vbNewLine _
                       & "     ,'2'                          AS SEND_SHORI_FLG                 " & vbNewLine _
                       & "     ,BIP.SEND_USER                AS SEND_USER                      " & vbNewLine _
                       & "     ,BIP.SEND_TIME                AS SEND_TIME                      " & vbNewLine _
                       & "     ,BIP.SYS_ENT_DATE             AS SYS_ENT_DATE                   " & vbNewLine _
                       & "     ,BIP.SYS_ENT_TIME             AS SYS_ENT_TIME                   " & vbNewLine _
                       & "     ,BIP.SYS_ENT_PGID             AS SYS_ENT_PGID                   " & vbNewLine _
                       & "     ,BIP.SYS_ENT_USER             AS SYS_ENT_USER                   " & vbNewLine _
                       & "     ,BIP.SYS_UPD_DATE             AS SYS_UPD_DATE                   " & vbNewLine _
                       & "     ,BIP.SYS_UPD_TIME             AS SYS_UPD_TIME                   " & vbNewLine _
                       & "     ,BIP.SYS_UPD_PGID             AS SYS_UPD_PGID                   " & vbNewLine _
                       & "     ,BIP.SYS_UPD_USER             AS SYS_UPD_USER                   " & vbNewLine _
                       & "     ,'1'                          AS SYS_DEL_FLG                    " & vbNewLine _
                       & " FROM $LM_TRN$..B_INKA_PLAN_SEND   AS BIP                            " & vbNewLine _
                       & " LEFT JOIN                                                           " & vbNewLine _
                       & "     (                                                               " & vbNewLine _
                       & "       SELECT                                                        " & vbNewLine _
                       & "             BIP_IN2.NRS_BR_CD                  AS NRS_BR_CD         " & vbNewLine _
                       & "            ,BIP_IN2.INKA_NO_L                  AS INKA_NO_L         " & vbNewLine _
                       & "            ,BIP_IN2.INKA_NO_M                  AS INKA_NO_M         " & vbNewLine _
                       & "            ,BIP_IN2.INKA_NO_S                  AS INKA_NO_S         " & vbNewLine _
                       & "            ,MAX(GET_SEQ.SEND_SEQ)              AS SEND_SEQ          " & vbNewLine _
                       & "       FROM $LM_TRN$..B_INKA_PLAN_SEND          AS BIP_IN2           " & vbNewLine _
                       & "       LEFT JOIN                                                     " & vbNewLine _
                       & "           (                                                         " & vbNewLine _
                       & "             SELECT                                                  " & vbNewLine _
                       & "                   BIP_IN1.NRS_BR_CD            AS NRS_BR_CD         " & vbNewLine _
                       & "                  ,BIP_IN1.INKA_NO_L            AS INKA_NO_L         " & vbNewLine _
                       & "                  ,BIP_IN1.INKA_NO_M            AS INKA_NO_M         " & vbNewLine _
                       & "                  ,BIP_IN1.INKA_NO_S            AS INKA_NO_S         " & vbNewLine _
                       & "                  ,BIP_IN1.SEND_SEQ             AS SEND_SEQ          " & vbNewLine _
                       & "             FROM $LM_TRN$..B_INKA_PLAN_SEND   AS BIP_IN1            " & vbNewLine _
                       & "             WHERE                                                   " & vbNewLine _
                       & "                   BIP_IN1.SYS_DEL_FLG           = '0'               " & vbNewLine _
                       & "               AND BIP_IN1.NRS_BR_CD             = @NRS_BR_CD  --@   " & vbNewLine _
                       & "               AND BIP_IN1.INKA_NO_L             = @INKA_NO_L  --@   " & vbNewLine _
                       & "               AND BIP_IN1.DATA_KBN             <> '2'               " & vbNewLine _
                       & "             GROUP BY                                                " & vbNewLine _
                       & "                   BIP_IN1.NRS_BR_CD                                 " & vbNewLine _
                       & "                  ,BIP_IN1.INKA_NO_L                                 " & vbNewLine _
                       & "                  ,BIP_IN1.INKA_NO_M                                 " & vbNewLine _
                       & "                  ,BIP_IN1.INKA_NO_S                                 " & vbNewLine _
                       & "                  ,BIP_IN1.SEND_SEQ                                  " & vbNewLine _
                       & "            ) AS GET_SEQ                                             " & vbNewLine _
                       & "          ON BIP_IN2.NRS_BR_CD             = GET_SEQ.NRS_BR_CD       " & vbNewLine _
                       & "         AND BIP_IN2.INKA_NO_L             = GET_SEQ.INKA_NO_L       " & vbNewLine _
                       & "         AND BIP_IN2.INKA_NO_M             = GET_SEQ.INKA_NO_M       " & vbNewLine _
                       & "         AND BIP_IN2.INKA_NO_S             = GET_SEQ.INKA_NO_S       " & vbNewLine _
                       & "       WHERE                                                         " & vbNewLine _
                       & "             BIP_IN2.SYS_DEL_FLG           = '0'                     " & vbNewLine _
                       & "         AND BIP_IN2.NRS_BR_CD             = @NRS_BR_CD    --@       " & vbNewLine _
                       & "         AND BIP_IN2.INKA_NO_L             = @INKA_NO_L    --@       " & vbNewLine _
                       & "         AND BIP_IN2.DATA_KBN             <> '2'                     " & vbNewLine _
                       & "       GROUP BY                                                      " & vbNewLine _
                       & "             BIP_IN2.NRS_BR_CD                                       " & vbNewLine _
                       & "            ,BIP_IN2.INKA_NO_L                                       " & vbNewLine _
                       & "            ,BIP_IN2.INKA_NO_M                                       " & vbNewLine _
                       & "            ,BIP_IN2.INKA_NO_S                                       " & vbNewLine _
                       & "     ) AS MAX_SEQ                                                    " & vbNewLine _
                       & "   ON BIP.NRS_BR_CD    = MAX_SEQ.NRS_BR_CD                           " & vbNewLine _
                       & "  AND BIP.INKA_NO_L    = MAX_SEQ.INKA_NO_L                           " & vbNewLine _
                       & "  AND BIP.INKA_NO_M    = MAX_SEQ.INKA_NO_M                           " & vbNewLine _
                       & "  AND BIP.INKA_NO_S    = MAX_SEQ.INKA_NO_S                           " & vbNewLine _
                       & "  AND BIP.SEND_SEQ     = MAX_SEQ.SEND_SEQ                            " & vbNewLine _
                       & " LEFT JOIN                                                           " & vbNewLine _
                       & "     (                                                               " & vbNewLine _
                       & "       SELECT                                                        " & vbNewLine _
                       & "             BIP_IN3.NRS_BR_CD            AS NRS_BR_CD               " & vbNewLine _
                       & "            ,BIP_IN3.INKA_NO_L            AS INKA_NO_L               " & vbNewLine _
                       & "            ,BIP_IN3.INKA_NO_M            AS INKA_NO_M               " & vbNewLine _
                       & "            ,BIP_IN3.INKA_NO_S            AS INKA_NO_S               " & vbNewLine _
                       & "            ,CONVERT(INTEGER,MAX(BIP_IN3.SEND_SEQ)) + 1  AS SEND_SEQ " & vbNewLine _
                       & "       FROM $LM_TRN$..B_INKA_PLAN_SEND   AS BIP_IN3                  " & vbNewLine _
                       & "       WHERE                                                         " & vbNewLine _
                       & "             BIP_IN3.NRS_BR_CD             = @NRS_BR_CD     --@      " & vbNewLine _
                       & "         AND BIP_IN3.INKA_NO_L             = @INKA_NO_L     --@      " & vbNewLine _
                       & "       GROUP BY                                                      " & vbNewLine _
                       & "             BIP_IN3.NRS_BR_CD                                       " & vbNewLine _
                       & "            ,BIP_IN3.INKA_NO_L                                       " & vbNewLine _
                       & "            ,BIP_IN3.INKA_NO_M                                       " & vbNewLine _
                       & "            ,BIP_IN3.INKA_NO_S                                       " & vbNewLine _
                       & "     )                         AS GET_MAX_SEQ                        " & vbNewLine _
                       & "   ON BIP.NRS_BR_CD             = GET_MAX_SEQ.NRS_BR_CD              " & vbNewLine _
                       & "  AND BIP.INKA_NO_L             = GET_MAX_SEQ.INKA_NO_L              " & vbNewLine _
                       & "  AND BIP.INKA_NO_M             = GET_MAX_SEQ.INKA_NO_M              " & vbNewLine _
                       & "  AND BIP.INKA_NO_S             = GET_MAX_SEQ.INKA_NO_S              " & vbNewLine _
                       & "  AND (BIP.SEND_SEQ +1)         = GET_MAX_SEQ.SEND_SEQ               " & vbNewLine _
                       & " WHERE                                                               " & vbNewLine _
                       & "      BIP.SYS_DEL_FLG  = '0'                                         " & vbNewLine _
                       & "  AND BIP.NRS_BR_CD    = @NRS_BR_CD     --@                          " & vbNewLine _
                       & "  AND BIP.INKA_NO_L    = @INKA_NO_L     --@                          " & vbNewLine _
                       & "  AND GET_MAX_SEQ.SEND_SEQ IS NOT NULL                               " & vbNewLine _
                       & "  AND MAX_SEQ.INKA_NO_L IS NOT NULL                                  " & vbNewLine


    ''' <summary>
    ''' オーダーバイ句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SEND_CANCEL_ORDER_BY As String = _
         " ORDER BY                " & vbNewLine _
       & "      BIP.NRS_BR_CD ASC  " & vbNewLine _
       & "     ,BIP.INKA_NO_L ASC  " & vbNewLine _
       & "     ,BIP.INKA_NO_M ASC  " & vbNewLine _
       & "     ,BIP.INKA_NO_S ASC  " & vbNewLine _
       & "     ,BIP.SEND_SEQ  ASC  " & vbNewLine

#Region "特定の荷主固有のテーブルが存在するか否かの判定"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    Private Const SQL_GET_TRN_TBL_EXISTS As String = "" _
        & "SELECT                                                       " & vbNewLine _
        & "    CASE WHEN OBJECT_ID('$LM_TRN$..' + @TBL_NM, 'U') IS NULL " & vbNewLine _
        & "        THEN '0'                                             " & vbNewLine _
        & "        ELSE '1'                                             " & vbNewLine _
        & "    END AS TBL_EXISTS                                        " & vbNewLine _
        & ""

#End Region ' ""特定の荷主固有のテーブルが存在するか否かの判定""

#Region "RFIDラベルデータ取得処理"

    ''' <summary>
    ''' RFIDラベルデータ取得処理 SQL
    ''' </summary>
    Private Const SQL_SELECT_RFID_LAVEL_DATA As String = "" _
        & "-- ドラム                                                                                                                                                                            " & vbNewLine _
        & "SELECT                                                                                                                                                                               " & vbNewLine _
        & "      CASE H_INKAEDI_DTL_TSMC.LVL1_UT WHEN 'DRU' THEN 1 WHEN 'BOX' THEN 2 WHEN 'CY' THEN 3 WHEN 'PAL' THEN 3 ELSE 9 END AS LVL1_UT_ORDER                                             " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LVL1_UT                       AS LVL1_UT                                                                                                                    " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L                 AS INKA_NO_L                                                                                                                  " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INLA_CTL_NO_M                 AS INKA_NO_M                                                                                                                  " & vbNewLine _
        & "    , CASE WHEN ISNUMERIC(H_INKAEDI_DTL_TSMC.TSMC_QTY) = 1 THEN CAST(H_INKAEDI_DTL_TSMC.TSMC_QTY AS NUMERIC(10)) ELSE 0 END AS TSMC_QTY                                              " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.T_GOODS_CD                    AS MATERIAL_NO                                                                                                                " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LT_DATE                       AS EXPIRATION_DATE                                                                                                            " & vbNewLine _
        & "    , 'TS'                                             AS COMPANY_ID                                                                                                                 " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LOT_NO                        AS RAW_LOT_ID                                                                                                                 " & vbNewLine _
        & "    , '00000'                                          AS SEQUENCE_ID                                                                                                                " & vbNewLine _
        & "    , ''                                               AS CYLINDER_NO                                                                                                                " & vbNewLine _
        & "    , ''                                               AS LOT_ID                                                                                                                     " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL1_CHECK, 12, LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) - 11) ELSE '' END AS VENDOR_MATERIAL_NO " & vbNewLine _
        & "    , CASE WHEN ISNULL(M_GOODS.SHOBOKIKEN_KB, '03') IN ('01', '02') THEN 'Y' ELSE 'N' END AS HAZARDOUS                                                                               " & vbNewLine _
        & "    , ''                                               AS SERIAL_NO                                                                                                                  " & vbNewLine _
        & "    , ''                                               AS SECOND_LAYER_PPN                                                                                                           " & vbNewLine _
        & "    , ''                                               AS BOX_SERIAL_NO                                                                                                              " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.PLANT + H_INKAEDI_DTL_TSMC.WH AS SHIP_TO                                                                                                                    " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL2_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL2_CHECK, 12, LEN(H_INKAEDI_DTL_TSMC.LVL2_CHECK) - 11) ELSE '' END THIRD_LAYER_PPN       " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.TRACKING_NO                   AS PACKAGING_ID                                                                                                               " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.SUPPLY_CD                     AS VENDER_CODE                                                                                                                " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL2_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL2_CHECK, 3, 9) ELSE '' END AS DUNS_NO                                                   " & vbNewLine _
        & "    , 'N'                                              AS SAMPLE                                                                                                                     " & vbNewLine _
        & "FROM                                                                                                                                                                                 " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_DTL_TSMC                                                                                                                                                     " & vbNewLine _
        & "LEFT JOIN                                                                                                                                                                            " & vbNewLine _
        & "    $LM_MST$..Z_KBN                                                                                                                                                                  " & vbNewLine _
        & "        ON  Z_KBN.KBN_GROUP_CD = 'K038'                                                                                                                                              " & vbNewLine _
        & "        AND Z_KBN.KBN_NM1 = H_INKAEDI_DTL_TSMC.SUPPLY_CD                                                                                                                             " & vbNewLine _
        & "        AND Z_KBN.SYS_DEL_FLG = '0'                                                                                                                                                  " & vbNewLine _
        & "LEFT JOIN                                                                                                                                                                            " & vbNewLine _
        & "    $LM_MST$..M_GOODS                                                                                                                                                                " & vbNewLine _
        & "        ON  M_GOODS.NRS_BR_CD = H_INKAEDI_DTL_TSMC.NRS_BR_CD                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.GOODS_CD_CUST = H_INKAEDI_DTL_TSMC.T_GOODS_CD                                                                                                                    " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_L = H_INKAEDI_DTL_TSMC.CUST_CD_L                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_M = H_INKAEDI_DTL_TSMC.CUST_CD_M                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_S = Z_KBN.KBN_NM3                                                                                                                                        " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_SS = Z_KBN.KBN_NM4                                                                                                                                       " & vbNewLine _
        & "        AND M_GOODS.SYS_DEL_FLG = '0'                                                                                                                                                " & vbNewLine _
        & "WHERE                                                                                                                                                                                " & vbNewLine _
        & "    H_INKAEDI_DTL_TSMC.NRS_BR_CD = @NRS_BR_CD                                                                                                                                        " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L = @INKA_NO_L                                                                                                                                    " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.LVL1_UT = 'DRU'                                                                                                                                               " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.SYS_DEL_FLG = '0'                                                                                                                                             " & vbNewLine _
        & "-- ボックス                                                                                                                                                                          " & vbNewLine _
        & "UNION ALL                                                                                                                                                                            " & vbNewLine _
        & "SELECT                                                                                                                                                                               " & vbNewLine _
        & "      CASE H_INKAEDI_DTL_TSMC.LVL1_UT WHEN 'DRU' THEN 1 WHEN 'BOX' THEN 2 WHEN 'CY' THEN 3 WHEN 'PAL' THEN 3 ELSE 9 END AS LVL1_UT_ORDER                                             " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LVL1_UT                       AS LVL1_UT                                                                                                                    " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L                 AS INKA_NO_L                                                                                                                  " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INLA_CTL_NO_M                 AS INKA_NO_M                                                                                                                  " & vbNewLine _
        & "    , CASE WHEN ISNUMERIC(H_INKAEDI_DTL_TSMC.TSMC_QTY) = 1 THEN CAST(H_INKAEDI_DTL_TSMC.TSMC_QTY AS NUMERIC(10)) ELSE 0 END AS TSMC_QTY                                              " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.T_GOODS_CD                    AS MATERIAL_NO                                                                                                                " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LT_DATE                       AS EXPIRATION_DATE                                                                                                            " & vbNewLine _
        & "    , 'TS'                                             AS COMPANY_ID                                                                                                                 " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LOT_NO                        AS RAW_LOT_ID                                                                                                                 " & vbNewLine _
        & "    , ''                                               AS SEQUENCE_ID                                                                                                                " & vbNewLine _
        & "    , ''                                               AS CYLINDER_NO                                                                                                                " & vbNewLine _
        & "    , ''                                               AS LOT_ID                                                                                                                     " & vbNewLine _
        & "    , ''                                               AS VENDOR_MATERIAL_NO                                                                                                         " & vbNewLine _
        & "    , CASE WHEN ISNULL(M_GOODS.SHOBOKIKEN_KB, '03') IN ('01', '02') THEN 'Y' ELSE 'N' END AS HAZARDOUS                                                                               " & vbNewLine _
        & "    , '00000'                                          AS SERIAL_NO                                                                                                                  " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL1_CHECK, 12, LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) - 11) ELSE '' END AS SECOND_LAYER_PPN   " & vbNewLine _
        & "    , '00000'                                          AS BOX_SERIAL_NO                                                                                                              " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.PLANT + H_INKAEDI_DTL_TSMC.WH AS SHIP_TO                                                                                                                    " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL2_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL2_CHECK, 12, LEN(H_INKAEDI_DTL_TSMC.LVL2_CHECK) - 11) ELSE '' END THIRD_LAYER_PPN       " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.TRACKING_NO                   AS PACKAGING_ID                                                                                                               " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.SUPPLY_CD                     AS VENDER_CODE                                                                                                                " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL2_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL2_CHECK, 3, 9) ELSE '' END AS DUNS_NO                                                   " & vbNewLine _
        & "    , 'N'                                              AS SAMPLE                                                                                                                     " & vbNewLine _
        & "FROM                                                                                                                                                                                 " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_DTL_TSMC                                                                                                                                                     " & vbNewLine _
        & "LEFT JOIN                                                                                                                                                                            " & vbNewLine _
        & "    $LM_MST$..Z_KBN                                                                                                                                                                  " & vbNewLine _
        & "        ON  Z_KBN.KBN_GROUP_CD = 'K038'                                                                                                                                              " & vbNewLine _
        & "        AND Z_KBN.KBN_NM1 = H_INKAEDI_DTL_TSMC.SUPPLY_CD                                                                                                                             " & vbNewLine _
        & "        AND Z_KBN.SYS_DEL_FLG = '0'                                                                                                                                                  " & vbNewLine _
        & "LEFT JOIN                                                                                                                                                                            " & vbNewLine _
        & "    $LM_MST$..M_GOODS                                                                                                                                                                " & vbNewLine _
        & "        ON  M_GOODS.NRS_BR_CD = H_INKAEDI_DTL_TSMC.NRS_BR_CD                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.GOODS_CD_CUST = H_INKAEDI_DTL_TSMC.T_GOODS_CD                                                                                                                    " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_L = H_INKAEDI_DTL_TSMC.CUST_CD_L                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_M = H_INKAEDI_DTL_TSMC.CUST_CD_M                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_S = Z_KBN.KBN_NM3                                                                                                                                        " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_SS = Z_KBN.KBN_NM4                                                                                                                                       " & vbNewLine _
        & "        AND M_GOODS.SYS_DEL_FLG = '0'                                                                                                                                                " & vbNewLine _
        & "WHERE                                                                                                                                                                                " & vbNewLine _
        & "    H_INKAEDI_DTL_TSMC.NRS_BR_CD = @NRS_BR_CD                                                                                                                                        " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L = @INKA_NO_L                                                                                                                                    " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.LVL1_UT = 'BOX'                                                                                                                                               " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.SYS_DEL_FLG = '0'                                                                                                                                             " & vbNewLine _
        & "-- シリンダー                                                                                                                                                                        " & vbNewLine _
        & "UNION ALL                                                                                                                                                                            " & vbNewLine _
        & "SELECT                                                                                                                                                                               " & vbNewLine _
        & "      CASE H_INKAEDI_DTL_TSMC.LVL1_UT WHEN 'DRU' THEN 1 WHEN 'BOX' THEN 2 WHEN 'CY' THEN 3 WHEN 'PAL' THEN 3 ELSE 9 END AS LVL1_UT_ORDER                                             " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LVL1_UT                       AS LVL1_UT                                                                                                                    " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L                 AS INKA_NO_L                                                                                                                  " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INLA_CTL_NO_M                 AS INKA_NO_M                                                                                                                  " & vbNewLine _
        & "    , CASE WHEN ISNUMERIC(H_INKAEDI_DTL_TSMC.TSMC_QTY) = 1 THEN CAST(H_INKAEDI_DTL_TSMC.TSMC_QTY AS NUMERIC(10)) ELSE 0 END AS TSMC_QTY                                              " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.T_GOODS_CD                    AS MATERIAL_NO                                                                                                                " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LT_DATE                       AS EXPIRATION_DATE                                                                                                            " & vbNewLine _
        & "    , 'TS'                                             AS COMPANY_ID                                                                                                                 " & vbNewLine _
        & "    , ''                                               AS RAW_LOT_ID                                                                                                                 " & vbNewLine _
        & "    , ''                                               AS SEQUENCE_ID                                                                                                                " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.TRACKING_NO                   AS CYLINDER_NO                                                                                                                " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.LOT_NO                        AS LOT_ID                                                                                                                     " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL1_CHECK, 12, LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) - 11) ELSE '' END AS VENDOR_MATERIAL_NO " & vbNewLine _
        & "    , CASE WHEN ISNULL(M_GOODS.SHOBOKIKEN_KB, '03') IN ('01', '02') THEN 'Y' ELSE 'N' END AS HAZARDOUS                                                                               " & vbNewLine _
        & "    , ''                                               AS SERIAL_NO                                                                                                                  " & vbNewLine _
        & "    , ''                                               AS SECOND_LAYER_PPN                                                                                                           " & vbNewLine _
        & "    , ''                                               AS BOX_SERIAL_NO                                                                                                              " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.PLANT + H_INKAEDI_DTL_TSMC.WH AS SHIP_TO                                                                                                                    " & vbNewLine _
        & "    , ''                                               AS THIRD_LAYER_PPN                                                                                                            " & vbNewLine _
        & "    , @SYS_DATE_YYMMDD                                 AS PACKAGING_ID                                                                                                               " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.SUPPLY_CD                     AS VENDER_CODE                                                                                                                " & vbNewLine _
        & "    , CASE WHEN LEN(H_INKAEDI_DTL_TSMC.LVL1_CHECK) > 11 THEN SUBSTRING(H_INKAEDI_DTL_TSMC.LVL1_CHECK, 3, 9) ELSE '' END AS DUNS_NO                                                   " & vbNewLine _
        & "    , 'N'                                              AS SAMPLE                                                                                                                     " & vbNewLine _
        & "FROM                                                                                                                                                                                 " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_DTL_TSMC                                                                                                                                                     " & vbNewLine _
        & "LEFT JOIN                                                                                                                                                                            " & vbNewLine _
        & "    $LM_MST$..Z_KBN                                                                                                                                                                  " & vbNewLine _
        & "        ON  Z_KBN.KBN_GROUP_CD = 'K038'                                                                                                                                              " & vbNewLine _
        & "        AND Z_KBN.KBN_NM1 = H_INKAEDI_DTL_TSMC.SUPPLY_CD                                                                                                                             " & vbNewLine _
        & "        AND Z_KBN.SYS_DEL_FLG = '0'                                                                                                                                                  " & vbNewLine _
        & "LEFT JOIN                                                                                                                                                                            " & vbNewLine _
        & "    $LM_MST$..M_GOODS                                                                                                                                                                " & vbNewLine _
        & "        ON  M_GOODS.NRS_BR_CD = H_INKAEDI_DTL_TSMC.NRS_BR_CD                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.GOODS_CD_CUST = H_INKAEDI_DTL_TSMC.T_GOODS_CD                                                                                                                    " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_L = H_INKAEDI_DTL_TSMC.CUST_CD_L                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_M = H_INKAEDI_DTL_TSMC.CUST_CD_M                                                                                                                         " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_S = Z_KBN.KBN_NM3                                                                                                                                        " & vbNewLine _
        & "        AND M_GOODS.CUST_CD_SS = Z_KBN.KBN_NM4                                                                                                                                       " & vbNewLine _
        & "        AND M_GOODS.SYS_DEL_FLG = '0'                                                                                                                                                " & vbNewLine _
        & "WHERE                                                                                                                                                                                " & vbNewLine _
        & "    H_INKAEDI_DTL_TSMC.NRS_BR_CD = @NRS_BR_CD                                                                                                                                        " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L = @INKA_NO_L                                                                                                                                    " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.LVL1_UT IN ('CY', 'PAL')                                                                                                                                      " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.SYS_DEL_FLG = '0'                                                                                                                                             " & vbNewLine _
        & "ORDER BY                                                                                                                                                                             " & vbNewLine _
        & "      LVL1_UT_ORDER                                                                                                                                                                  " & vbNewLine _
        & "    , INKA_NO_L                                                                                                                                                                      " & vbNewLine _
        & "    , INKA_NO_M                                                                                                                                                                      " & vbNewLine _
        & ""

#End Region ' "RFIDラベルデータ取得処理"

#End Region

#Region "UPDATE"
    '無し
#End Region

#Region "INSERT"

    ''' <summary>
    ''' 入荷予定データ作成SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEND_INKA_DATA As String = _
                 " INSERT INTO $LM_TRN$..B_INKA_PLAN_SEND " & vbNewLine _
               & "       (                                 " & vbNewLine _
               & "         NRS_BR_CD                       " & vbNewLine _
               & "        ,INKA_NO_L                       " & vbNewLine _
               & "        ,INKA_NO_M                       " & vbNewLine _
               & "        ,INKA_NO_S                       " & vbNewLine _
               & "        ,SEND_SEQ                        " & vbNewLine _
               & "        ,DATA_KBN                        " & vbNewLine _
               & "        ,WH_CD                           " & vbNewLine _
               & "        ,CUST_CD_L                       " & vbNewLine _
               & "        ,CUST_CD_M                       " & vbNewLine _
               & "        ,CUST_NM_L                       " & vbNewLine _
               & "        ,INKA_DATE                       " & vbNewLine _
               & "        ,BUYER_ORD_NO_L                  " & vbNewLine _
               & "        ,OUTKA_FROM_ORD_NO_L             " & vbNewLine _
               & "        ,REMARK_L                        " & vbNewLine _
               & "        ,REMARK_OUT_L                    " & vbNewLine _
               & "        ,GOODS_CD_NRS                    " & vbNewLine _
               & "        ,GOODS_CD_CUST                   " & vbNewLine _
               & "        ,GOODS_NM_1                      " & vbNewLine _
               & "        ,OUTKA_FROM_ORD_NO_M             " & vbNewLine _
               & "        ,BUYER_ORD_NO_M                  " & vbNewLine _
               & "        ,LOT_NO                          " & vbNewLine _
               & "        ,REMARK_M                        " & vbNewLine _
               & "        ,INKA_NB                         " & vbNewLine _
               & "        ,INKA_WT                         " & vbNewLine _
               & "        ,KONSU                           " & vbNewLine _
               & "        ,HASU                            " & vbNewLine _
               & "        ,IRIME                           " & vbNewLine _
               & "        ,BETU_WT                         " & vbNewLine _
               & "        ,PKG_NB                          " & vbNewLine _
               & "        ,PKG_UT                          " & vbNewLine _
               & "        ,JAN_CD                          " & vbNewLine _
               & "        ,SERIAL_NO                       " & vbNewLine _
               & "        ,ONDO_KB                         " & vbNewLine _
               & "        ,ONDO_STR_DATE                   " & vbNewLine _
               & "        ,ONDO_END_DATE                   " & vbNewLine _
               & "        ,ONDO_MX                         " & vbNewLine _
               & "        ,ONDO_MM                         " & vbNewLine _
               & "        ,GOODS_COND_KB_1                 " & vbNewLine _
               & "        ,GOODS_COND_KB_2                 " & vbNewLine _
               & "        ,GOODS_COND_KB_3                 " & vbNewLine _
               & "        ,GOODS_CRT_DATE                  " & vbNewLine _
               & "        ,LT_DATE                         " & vbNewLine _
               & "        ,SPD_KB                          " & vbNewLine _
               & "        ,OFB_KB                          " & vbNewLine _
               & "        ,DEST_CD                         " & vbNewLine _
               & "        ,REMARK_S                        " & vbNewLine _
               & "        ,ALLOC_PRIORITY                  " & vbNewLine _
               & "        ,REMARK_OUT_S                    " & vbNewLine _
               & "        ,SEND_SHORI_FLG                  " & vbNewLine _
               & "        ,SYS_ENT_DATE                    " & vbNewLine _
               & "        ,SYS_ENT_TIME                    " & vbNewLine _
               & "        ,SYS_ENT_PGID                    " & vbNewLine _
               & "        ,SYS_ENT_USER                    " & vbNewLine _
               & "        ,SYS_UPD_DATE                    " & vbNewLine _
               & "        ,SYS_UPD_TIME                    " & vbNewLine _
               & "        ,SYS_UPD_PGID                    " & vbNewLine _
               & "        ,SYS_UPD_USER                    " & vbNewLine _
               & "        ,SYS_DEL_FLG                     " & vbNewLine _
               & "       )                                 " & vbNewLine _
               & " VALUES(                                 " & vbNewLine _
               & "          @NRS_BR_CD                     " & vbNewLine _
               & "        , @INKA_NO_L                     " & vbNewLine _
               & "        , @INKA_NO_M                     " & vbNewLine _
               & "        , @INKA_NO_S                     " & vbNewLine _
               & "        , @SEND_SEQ                      " & vbNewLine _
               & "        , @DATA_KBN                      " & vbNewLine _
               & "        , @WH_CD                         " & vbNewLine _
               & "        , @CUST_CD_L                     " & vbNewLine _
               & "        , @CUST_CD_M                     " & vbNewLine _
               & "        , @CUST_NM_L                     " & vbNewLine _
               & "        , @INKA_DATE                     " & vbNewLine _
               & "        , @BUYER_ORD_NO_L                " & vbNewLine _
               & "        , @OUTKA_FROM_ORD_NO_L           " & vbNewLine _
               & "        , @REMARK_L                      " & vbNewLine _
               & "        , @REMARK_OUT_L                  " & vbNewLine _
               & "        , @GOODS_CD_NRS                  " & vbNewLine _
               & "        , @GOODS_CD_CUST                 " & vbNewLine _
               & "        , @GOODS_NM_1                    " & vbNewLine _
               & "        , @OUTKA_FROM_ORD_NO_M           " & vbNewLine _
               & "        , @BUYER_ORD_NO_M                " & vbNewLine _
               & "        , @LOT_NO                        " & vbNewLine _
               & "        , @REMARK_M                      " & vbNewLine _
               & "        , @INKA_NB                       " & vbNewLine _
               & "        , @INKA_WT                       " & vbNewLine _
               & "        , @KONSU                         " & vbNewLine _
               & "        , @HASU                          " & vbNewLine _
               & "        , @IRIME                         " & vbNewLine _
               & "        , @BETU_WT                       " & vbNewLine _
               & "        , @PKG_NB                        " & vbNewLine _
               & "        , @PKG_UT                        " & vbNewLine _
               & "        , @JAN_CD                        " & vbNewLine _
               & "        , @SERIAL_NO                     " & vbNewLine _
               & "        , @ONDO_KB                       " & vbNewLine _
               & "        , @ONDO_STR_DATE                 " & vbNewLine _
               & "        , @ONDO_END_DATE                 " & vbNewLine _
               & "        , @ONDO_MX                       " & vbNewLine _
               & "        , @ONDO_MM                       " & vbNewLine _
               & "        , @GOODS_COND_KB_1               " & vbNewLine _
               & "        , @GOODS_COND_KB_2               " & vbNewLine _
               & "        , @GOODS_COND_KB_3               " & vbNewLine _
               & "        , @GOODS_CRT_DATE                " & vbNewLine _
               & "        , @LT_DATE                       " & vbNewLine _
               & "        , @SPD_KB                        " & vbNewLine _
               & "        , @OFB_KB                        " & vbNewLine _
               & "        , @DEST_CD                       " & vbNewLine _
               & "        , @REMARK_S                      " & vbNewLine _
               & "        , @ALLOC_PRIORITY                " & vbNewLine _
               & "        , @REMARK_OUT_S                  " & vbNewLine _
               & "        , @SEND_SHORI_FLG                " & vbNewLine _
               & "        , @SYS_DATE                      " & vbNewLine _
               & "        , @SYS_TIME                      " & vbNewLine _
               & "        , @SYS_PGID                      " & vbNewLine _
               & "        , @SYS_USER                      " & vbNewLine _
               & "        , @SYS_DATE                      " & vbNewLine _
               & "        , @SYS_TIME                      " & vbNewLine _
               & "        , @SYS_PGID                      " & vbNewLine _
               & "        , @SYS_USER                      " & vbNewLine _
               & "        , @SYS_DEL_FLG                   " & vbNewLine _
               & "      )                                  " & vbNewLine

#End Region

#End Region
    '2014.04.17 CALT連携対応 Ri --ED--

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

#Region "検索・更新処理"

    ''' <summary>
    ''' 入荷データL検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '20160614 tsunehira add start
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20160614 tsunehira add end

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMB010DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        Call Me.SetFromInkaS()                            '条件設定
        Me._StrSql.Append(LMB010DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        Call Me.SetConditionMasterSQL()                   '条件設定

        '20160614 tsunehira comment out
        'スキーマ名設定
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        '20160614 tsunehira add start
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))
        '20160614 tsunehira add end

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectData", cmd)

        'SQLの発行
        'TODO:テストのためログを入れる。
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMB010DAC", "SelectCountData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMB010DAC", "SelectCountData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMB010DAC", "SelectCountData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '20160614 tsunehira add start
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20160614 tsunehira add end

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB010DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        Call Me.SetFromInkaS()                            '条件設定
        Me._StrSql.Append(LMB010DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMB010DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        'Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        '20160614 tsunehira add
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectListData", cmd)

        'SQLの発行
        'TODO:テストのためログを入れる。
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMB010DAC", "SelectListData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMB010DAC", "SelectListData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMB010DAC", "SelectListData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("INKA_STATE_KB_NM", "INKA_STATE_KB_NM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_NM", "CUST_NM")
        'START YANAI 要望番号748
        map.Add("CUST_CD_S", "CUST_CD_S")
        'END YANAI 要望番号748
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("INKA_TTL_NB", "INKA_TTL_NB")
        map.Add("WT", "WT")
        map.Add("REMARK", "REMARK")
        map.Add("REC_CNT", "REC_CNT")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("UNCHIN_NM", "UNCHIN_NM")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("INKA_TP_NM", "INKA_TP_NM")
        map.Add("INKA_KB_NM", "INKA_KB_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        'START YANAI メモ②No.28
        map.Add("WH_CD", "WH_CD")
        map.Add("OUTKA_FROM_ORD_NO_M", "OUTKA_FROM_ORD_NO_M")
        map.Add("INKA_TP_CD", "INKA_TP_CD")
        'END YANAI メモ②No.28
        'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
        map.Add("REC_CNT_S", "REC_CNT_S")
        map.Add("PIC", "PIC")
        'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）

        map.Add("WH_WORK_STATUS_NM", "WH_WORK_STATUS_NM")

        map.Add("WEB_INKA_NO_L", "WEB_INKA_NO_L")

        map.Add("WH_TAB_STATUS_NM", "WH_TAB_STATUS_NM")
        map.Add("WH_TAB_WORK_STATUS_NM", "WH_TAB_WORK_STATUS_NM")
        map.Add("WH_TAB_WORK_STATUS_KB", "WH_TAB_WORK_STATUS_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT")

        Return ds

    End Function

    'START YANAI メモ②No.28
    ''' <summary>
    ''' EDIチェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDIチェック対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListEdiData1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成(入荷管理番号取得用)
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_EDIL_DATA1)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB010DAC.SQL_FROM_EDIL)             'SQL構築(データ抽出用From句)
        Call Me.SetWhererEdi1(inTbl)                   'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectListEdiData1", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_EDI_L1")

        Return ds

    End Function

    ''' <summary>
    ''' EDIチェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDIチェック対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListEdiData2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成(入荷管理番号取得用)
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_EDIL_DATA2)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB010DAC.SQL_FROM_EDIL)             'SQL構築(データ抽出用From句)
        Call Me.SetWhererEdi2(inTbl)                   'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectListEdiData2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_EDI_L2")

        Return ds

    End Function

    ''' <summary>
    ''' EDIチェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDIチェック対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListInkaSData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_CHK")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成(入荷管理番号取得用)
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_INKAS_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB010DAC.SQL_FROM_INKAS)             'SQL構築(データ抽出用From句)
        Call Me.SetWhererInkaS(inTbl)                   'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectListInkaSData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("OFB_KB", "OFB_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_INKA_S")

        Return ds

    End Function

    'END YANAI メモ②No.28

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '入荷管理番号（ダイレクト検索）
            whereStr = .Item("INKA_NO_L_DIRECT").ToString()
            If String.IsNullOrEmpty(.Item("INKA_NO_L_DIRECT").ToString()) = False Then
                Me._StrSql.Append(" AND B_INKA_L.INKA_NO_L LIKE @INKA_NO_L_DIRECT")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L_DIRECT", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L_DIRECT", String.Concat("%", whereStr), DBDataType.NVARCHAR)) '検証結果_導入時要望 №63対応(2011.09.12)
                Exit Sub
            End If

            '進捗区分
            Dim arr As ArrayList = New ArrayList()

            '予定入力済にチェックあり
            If String.IsNullOrEmpty(.Item("INKA_STATE_KB1").ToString()) = False Then

                arr.Add("'10'")

            End If

            '受付表印刷にチェックあり
            If String.IsNullOrEmpty(.Item("INKA_STATE_KB2").ToString()) = False Then

                arr.Add("'20'")

            End If

            '受付済にチェックあり
            If String.IsNullOrEmpty(.Item("INKA_STATE_KB3").ToString()) = False Then

                arr.Add("'30'")

            End If

            '検品済にチェックあり
            If String.IsNullOrEmpty(.Item("INKA_STATE_KB4").ToString()) = False Then

                arr.Add("'40'")

            End If

            '入荷済にチェックあり
            If String.IsNullOrEmpty(.Item("INKA_STATE_KB5").ToString()) = False Then

                arr.Add("'50'")

            End If

            '報告済にチェックあり
            If String.IsNullOrEmpty(.Item("INKA_STATE_KB6").ToString()) = False Then

                arr.Add("'90'")

            End If

            '入荷データの進捗区分を設定
            Call Me.SetCheckBoxData(arr, "INKA_STATE_KB")

            '荷主コード
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '運送元区分
            arr = New ArrayList()

            '運送元区分（全部ならスキップ）
            If String.IsNullOrEmpty(.Item("UNCHIN_TP3").ToString()) = True Then

                '運送元区分（運送）
                If String.IsNullOrEmpty(.Item("UNCHIN_TP1").ToString()) = False Then
                    arr.Add("'01'")
                End If

                '運送元区分（横持ち）
                If String.IsNullOrEmpty(.Item("UNCHIN_TP2").ToString()) = False Then
                    arr.Add("'02'")
                End If

                '運送データの運送元区分を設定
                Call Me.SetCheckBoxData(arr, "UNCHIN_TP")

            End If

            '倉庫
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.WH_CD = @WH_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '入荷日（FROM)
            whereStr = .Item("INKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.INKA_DATE >= @INKA_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日（TO)
            whereStr = .Item("INKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.INKA_DATE <= @INKA_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'オーダー番号
            whereStr = .Item("OUTKA_FROM_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.OUTKA_FROM_ORD_NO_L LIKE @OUTKA_FROM_ORD_NO_L")
                Me._StrSql.Append(vbNewLine)
#If False Then  'UPD 2019/01/23 依頼番号 : 003868   【LMS】オーダー番号の検索方法「前方一致⇒部分一致」(
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
#Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))

#End If
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (M_CUST.CUST_NM_L + '　' +  M_CUST.CUST_NM_M) LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'START YANAI 要望番号748
            '荷主コード小
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_GOODS.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", MaeCoverData(whereStr, "0", 2), DBDataType.NVARCHAR))
            End If
            'END YANAI 要望番号748

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_GOODS.GOODS_NM_1 LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'コメント
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.REMARK LIKE @REMARK")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷元名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_DEST.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運賃区分
            whereStr = .Item("UNCHIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.UNCHIN_KB LIKE @UNCHIN_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_UNSOCO.UNSOCO_NM + '　' +  M_UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '顧客入荷管理番号
            whereStr = .Item("WEB_INKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND WEB_INKA_L.WEB_INKA_NO_L LIKE @WEB_INKA_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WEB_INKA_NO_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '入荷管理番号(大)
            whereStr = .Item("INKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.INKA_NO_L LIKE @INKA_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '注文番号
            whereStr = .Item("BUYER_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.BUYER_ORD_NO_L LIKE @BUYER_ORD_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '入荷種別
            whereStr = .Item("INKA_TP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.INKA_TP = @INKA_TP")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", whereStr, DBDataType.CHAR))
            End If

            '入荷区分
            whereStr = .Item("INKA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.INKA_KB = @INKA_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", whereStr, DBDataType.CHAR))
            End If

            '担当者
            whereStr = .Item("TANTO_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_NM LIKE @TANTO_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作成者
            whereStr = .Item("SYS_ENT_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ENT_USER.USER_NM LIKE @SYS_ENT_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '最終更新者
            whereStr = .Item("SYS_UPD_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UPD_USER.USER_NM LIKE @SYS_UPD_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 庫内作業ステータス
            whereStr = .Item("WH_WORK_STATUS").ToString()
            If String.IsNullOrWhiteSpace(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.WH_KENPIN_WK_STATUS = @WH_WORK_STATUS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_WORK_STATUS", whereStr, DBDataType.CHAR))
            End If

            '20160615 tsunehira add start
            '選択言語フラグ
            Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))
            '20160615 tsunehira add end

            '私の担当分
            If LMConst.FLG.ON.Equals(.Item("TANTO_USER_FLG").ToString()) Then
                whereStr = .Item("USER_ID").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND B_INKA_L.SYS_ENT_USER = @SYS_ENT_USER")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", whereStr, DBDataType.NVARCHAR))
                End If
            End If

            ' 現場作業指示ステータス
            whereStr = .Item("WH_TAB_STATUS").ToString()
            If String.IsNullOrWhiteSpace(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_L.WH_TAB_STATUS = @WH_TAB_STATUS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", whereStr, DBDataType.CHAR))
            End If

            ' 現場進捗区分
            whereStr = .Item("WH_TAB_WORK_STATUS").ToString()
            If String.IsNullOrWhiteSpace(whereStr) = False Then
                Me._StrSql.Append(" AND TAB_KBN.WH_TAB_WORK_STATUS = @WH_TAB_WORK_STATUS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_WORK_STATUS", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    'START YANAI メモ②No.28
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetWhererEdi1(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim max As Integer = dt.Rows.Count - 1

        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EDI_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

        End With

        '入荷管理番号番号
        If 0 < max Then
            Me._StrSql.Append(" AND (")
            For i As Integer = 0 To max
                If i <> 0 Then
                    Me._StrSql.Append(" OR ")
                End If
                Me._StrSql.Append(String.Concat("EDI_L.INKA_CTL_NO_L = '", dt.Rows(i).Item("INKA_NO_L").ToString(), "'"))
            Next
            Me._StrSql.Append(") ")
            Me._StrSql.Append(vbNewLine)
        End If

        '20160615 tsunehira add start 
        '20160801 tsunehira close
        '選択言語フラグ
        'Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))
        '20160615 tsunehira add end


    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetWhererEdi2(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim max As Integer = dt.Rows.Count - 1

        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EDI_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

        End With

        'オーダー番号
        If 0 < max Then
            Me._StrSql.Append(" AND (")
            For i As Integer = 0 To max
                If i <> 0 Then
                    Me._StrSql.Append(" OR ")
                End If
                Me._StrSql.Append(String.Concat("EDI_L.OUTKA_FROM_ORD_NO = '", dt.Rows(i).Item("OUTKA_FROM_ORD_NO_L").ToString(), "'"))
            Next
            Me._StrSql.Append(") ")
            Me._StrSql.Append(vbNewLine)
        End If

        '20160615 tsunehira add start
        '20160801 tsunehira close
        '選択言語フラグ
        'Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))
        '20160615 tsunehira add end



    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetWhererInkaS(ByVal dt As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim max As Integer = dt.Rows.Count - 1

        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_S.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

        End With

        '入荷管理番号番号
        If 0 < max Then
            Me._StrSql.Append(" AND (")
            For i As Integer = 0 To max
                If i <> 0 Then
                    Me._StrSql.Append(" OR ")
                End If
                Me._StrSql.Append(String.Concat("B_INKA_S.INKA_NO_L = '", dt.Rows(i).Item("INKA_NO_L").ToString(), "'"))
            Next
            Me._StrSql.Append(") ")
            Me._StrSql.Append(vbNewLine)
        End If

        '20160615 tsunehira add start
        '20160801 tsunehira close
        '選択言語フラグ
        'Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))
        '20160615 tsunehira add end

    End Sub

    'END YANAI メモ②No.28

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 荷主明細マスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustDetails(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_CUSTDETAILS)      'SQL構築

        'パラメータ設定
        Call Me.SetCustDetailsSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectCustDetails", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_CHECK")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコードの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_SAGYODATA)      'SQL構築

        'パラメータ設定
        Call Me.SetSagyoSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectSagyoData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_CHECK")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作成用、作業レコードの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMakeSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_SAGYO)       'SQL構築(SELECT句)
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_FROM_SAGYO)  'SQL構築(FROM句)
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_WHERE_SAGYO) 'SQL構築(WHERE句)
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_ORDER_SAGYO) 'SQL構築(ORDER句)

        'パラメータ設定
        Call Me.SetSagyoSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectMakeSagyo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("SAGYO_COMP", "SAGYO_COMP")
        map.Add("SKYU_CHK", "SKYU_CHK")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("INOUTKA_NO_LM", "INOUTKA_NO_LM")
        map.Add("WH_CD", "WH_CD")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("SAGYO_CD1", "SAGYO_CD1")
        map.Add("SAGYO_CD2", "SAGYO_CD2")
        map.Add("SAGYO_CD3", "SAGYO_CD3")
        map.Add("SAGYO_CD4", "SAGYO_CD4")
        map.Add("SAGYO_CD5", "SAGYO_CD5")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("SAGYO_GK", "SAGYO_GK")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("REMARK_ZAI", "REMARK_ZAI")
        map.Add("REMARK_SKYU", "REMARK_SKYU")
        map.Add("SAGYO_COMP_CD", "SAGYO_COMP_CD")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("DEST_SAGYO_FLG", "DEST_SAGYO_FLG")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_SAGYO")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業マスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoMst(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_SAGYOMST")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_SAGYOMST)      'SQL構築

        'パラメータ設定
        Call Me.SetSagyoMstSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectSagyoMst", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("KOSU_BAI", "KOSU_BAI")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("TAX_KB", "TAX_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_SAGYOMST")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコードの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRec(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010OUT_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_INSERT_SAGYO)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSagyoInsParameter(ds)

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertSagyoRec", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコードの物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_DELETE_SAGYO)       'SQL構築(Delete句)
        Me._StrSql.Append(LMB010DAC.SQL_DELETE_WHERE_SAGYO) 'SQL構築(Where句)
        Call Me.SetSagyoSelectParameter(ds)                 '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "DeleteSagyo", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function
    'END YANAI 20120121 作業一括処理対応

    ''' <summary>
    ''' チェックボックスの条件を設定
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="colNm">列名</param>
    ''' <remarks></remarks>
    Private Sub SetCheckBoxData(ByVal arr As ArrayList, ByVal colNm As String)

        Dim max As Integer = arr.Count - 1

        If -1 < max Then

            Me._StrSql.Append(String.Concat(" AND B_INKA_L.", colNm, " IN ("))

            For i As Integer = 0 To max

                If i = max Then

                    Me._StrSql.Append(String.Concat(arr(i).ToString(), " ) "))

                Else

                    Me._StrSql.Append(String.Concat(arr(i).ToString(), " , "))

                End If

            Next

            Me._StrSql.Append(vbNewLine)

        End If

    End Sub

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 入荷(大)テーブル更新（印刷時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLPrint(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_INKA_L").Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_UPDATE_PRINT, Me._Row.Item("NRS_BR_CD").ToString())

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetUpdPrintParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystemUp()

        '印刷種別によりSQL修正
        sql = Me.SetUpNm(Me._Row, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "UpdateInkaLPrint", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFromInkaS()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("INKA_NO_L_DIRECT").ToString()
            If String.IsNullOrEmpty(.Item("INKA_NO_L_DIRECT").ToString()) = True Then
                '入荷管理番号（ダイレクト検索）が設定されていない場合のみ
                If String.IsNullOrEmpty(.Item("LOT_NO").ToString()) = True AndAlso _
                    String.IsNullOrEmpty(.Item("SERIAL_NO").ToString()) = True Then
                    'ロット№、シリアル№の両方が空の場合はLEFT JOIN
                    Me._StrSql.Append(" LEFT JOIN ")
                Else
                    'ロット№、シリアル№のどちらかが設定されている場合はINNER JOIN
                    Me._StrSql.Append(" INNER JOIN ")
                End If
            Else
                '入荷管理番号（ダイレクト検索）時はLEFT JOIN
                Me._StrSql.Append(" LEFT JOIN ")
            End If
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" ( ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("          SELECT B_INKA_S.NRS_BR_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("                ,B_INKA_S.INKA_NO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("                ,MIN(B_INKA_S.LOT_NO) AS LOT_NO ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("                ,MIN(B_INKA_S.SERIAL_NO) AS SERIAL_NO ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("          FROM $LM_TRN$..B_INKA_S     B_INKA_S ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("          WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" B_INKA_S.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND B_INKA_S.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            whereStr = .Item("INKA_NO_L_DIRECT").ToString()
            If String.IsNullOrEmpty(.Item("INKA_NO_L_DIRECT").ToString()) = True Then
                '入荷管理番号（ダイレクト検索）が設定されていない場合のみ

                'ロット№
                whereStr = .Item("LOT_NO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND B_INKA_S.LOT_NO LIKE @LOT_NO")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND B_INKA_S.LOT_NO <> ''")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                End If

                'シリアル№
                whereStr = .Item("SERIAL_NO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND B_INKA_S.SERIAL_NO LIKE @SERIAL_NO")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND B_INKA_S.SERIAL_NO <> ''")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                End If
            End If

            Me._StrSql.Append("          GROUP BY ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("             B_INKA_S.NRS_BR_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("            ,B_INKA_S.INKA_NO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" )           INKAS2 ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" ON ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" INKAS2.NRS_BR_CD = B_INKA_L.NRS_BR_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND INKAS2.INKA_NO_L = B_INKA_L.INKA_NO_L ")
            Me._StrSql.Append(vbNewLine)

        End With

    End Sub
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ作成用データの検索(カウント)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaDataCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKA")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_OUTKAMAKEDATA_COUNT) 'SQL構築 SELECT COUNT句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_FROM_OUTKAMAKEDATA)  'SQL構築 FROM句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_WHERE_OUTKAMAKEDATA) 'SQL構築 WHERE句

        'パラメータ設定
        Call Me.SetOutkaDataSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectOutkaDataCount", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ作成用データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKA")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_OUTKAMAKEDATA)       'SQL構築 SELECT句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_FROM_OUTKAMAKEDATA)  'SQL構築 FROM句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_WHERE_OUTKAMAKEDATA) 'SQL構築 WHERE句

        'パラメータ設定
        Call Me.SetOutkaDataSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectOutkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("ZBUKA_CD", "ZBUKA_CD")
        map.Add("OUTKA_DATE_INIT", "OUTKA_DATE_INIT")
        map.Add("ROW_NO", "ROW_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_OUTKA")

        reader.Close()

        Return ds

    End Function
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'UTI修正 yamanaka 2012.12.07 Start
    ''' <summary>
    ''' UTI出荷データ作成用データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUtiOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKA")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_OUTKAMAKEDATA)           'SQL構築 SELECT句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_OUTKAMAKEDATA2)          'SQL構築 SELECT句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_FROM_UTI_OUTKAMAKEDATA)  'SQL構築 FROM句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_WHERE_OUTKAMAKEDATA)     'SQL構築 WHERE句

        'パラメータ設定
        Call Me.SetOutkaDataSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectUtiOutkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("ZBUKA_CD", "ZBUKA_CD")
        map.Add("OUTKA_DATE_INIT", "OUTKA_DATE_INIT")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("ORIG_CD", "ORIG_CD")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        '要望番号:1704 yamanaka 2012.12.19 Start
        map.Add("PKG_UT", "PKG_UT")
        '要望番号:1704 yamanaka 2012.12.19 End

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_OUTKA")

        reader.Close()

        Return ds

    End Function
    'UTI修正 yamanaka 2012.12.07 End

    'UTI追加修正 yamanaka 2012.12.21 Start
    ''' <summary>
    ''' UTI出荷データ作成用データの検索(カウント)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUtiOutkaDataCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKA")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_OUTKAMAKEDATA_COUNT)         'SQL構築 SELECT COUNT句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_FROM_UTI_OUTKAMAKEDATA_CNT)  'SQL構築 FROM句
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_WHERE_UTI_OUTKAMAKEDATA)     'SQL構築 WHERE句

        'パラメータ設定
        Call Me.SetOutkaDataSelectParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectOutkaDataCount", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)テーブル更新（出荷データ作成）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpMakeOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_OUTKA").Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_UPDATE_MAKE, Me._Row.Item("NRS_BR_CD").ToString())

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetUpdPrintParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystemUp()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "UpMakeOutkaData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function
    'UTI追加修正 yamanaka 2012.12.21 End

    'UTI追加修正 s.kobayashi 2013.1.30 Start
    ''' <summary>
    ''' 入荷報告取り消し用の件数取得(カウント)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUtiOutkaDataCountForCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_INKA_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_UTI_OUTKA_DATA_COUNT_FOR_CANCEL)         'SQL構築 SELECT COUNT句

        'パラメータ設定
        Call Me.SetSelectInkaCancelDataParameter(ds)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectUtiOutkaDataCountForCancel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)テーブル更新（入荷報告取り消し）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaHokokuCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_INKA_L").Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_UPDATE_INKA_HOUKOKU_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetUpdInkaHoukokuCancelParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystemUp()

        '印刷種別によりSQL修正
        sql = Me.SetUpNm(Me._Row, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "UpdInkaHoukokuCancel", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

    'WIT対応 入荷データ一括取込対応 kasama Start

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectInkaLCountBySysDateTime(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_INKA_L")
        Dim inRow As DataRow = inTbl.Rows(0)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_SELECT_COUNT_INKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Me.SetSelectInkaLCountBySysDateTimeParameterList(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    '2014.06.06 FFEM対応 追加START
    ''' <summary>
    ''' 入荷WK検品数　チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectBInkaWkKenpinChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_INKA_L")
        Dim inRow As DataRow = inTbl.Rows(0)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_SELECT_COUNT_B_INKA_WK, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Me.SetSelectTorikomiSrcDataParameterList(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        'Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function
    '2014.06.06 FFEM対応 追加END

    ''' <summary>
    ''' 荷主明細マスタの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustDetail(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_CUST_DETAIL")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_CUST_DETAIL)      'SQL構築

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Call Me.SetSelectCustDetailParameter(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("SET_NAIYO_2", "SET_NAIYO_2")
        map.Add("SET_NAIYO_3", "SET_NAIYO_3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_CUST_DETAIL")

        reader.Close()

        Return ds

    End Function

    ''' ==========================================================================
    ''' <summary>取込用元データ抽出メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function SelectTorikomiSrcData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_INKA_L")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sqlBuilder As New StringBuilder(LMB010DAC.SQL_SELECT_TORIKOMI_SRC)

        '2014.06.06 FFEM対応 追加START
        If (inRow.Item("SET_NAIYO").ToString()).Equals("01") = True Then
            sqlBuilder = New StringBuilder(LMB010DAC.SQL_SELECT_TORIKOMI_FFEM_SRC)
        End If
        '2014.06.06 FFEM対応 追加END

        Dim sql As String = Me.SetSchemaNm(sqlBuilder.ToString, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Me.SetSelectTorikomiSrcDataParameterList(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As New Hashtable
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("SEQ", "SEQ")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("NYUKOZUMI_NB", "NYUKOZUMI_NB")
        '2014.06.06 FFEM対応 追加START
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("EDI_NB", "EDI_NB")
        map.Add("SUM_INKA_NB", "SUM_INKA_NB")
        '2014.06.06 FFEM対応 追加END

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_TORIKOMI_SRC")

        Return ds

    End Function

    ''' ==========================================================================
    ''' <summary>INKA_NO_S最大値取得メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function SelectMaxInkaNoS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_INKA_S")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_SELECT_MAX_INKA_NO_S, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Dim sqlParamList As New List(Of SqlParameter)

        With sqlParamList
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_M", inRow("INKA_NO_M").ToString(), DBDataType.CHAR))
        End With

        cmd.Parameters.AddRange(sqlParamList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As New Hashtable
        map.Add("MAX_INKA_NO_S", "MAX_INKA_NO_S")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_INKA_NO_S")

        Return ds

    End Function

    'WIT対応 入荷データ一括取込対応 kasama End

    '2014.04.17 CALT連携対応 Ri --ST-- 

    ''' <summary>
    ''' 入荷(L/M/S))取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInkaLMS(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_INKA_PLAN_SEND").Rows(0)

        'SQL構築
        Me._StrSql.Append(LMB010DAC.SQL_SELECT_INKA_LMS)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetInkaParameterOnCalt(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectInkaLMS", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("SEND_SEQ", "SEND_SEQ")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_OUT_L", "REMARK_OUT_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OUTKA_FROM_ORD_NO_M", "OUTKA_FROM_ORD_NO_M")
        map.Add("BUYER_ORD_NO_M", "BUYER_ORD_NO_M")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("INKA_WT", "INKA_WT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("IRIME", "IRIME")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("REMARK_OUT_S", "REMARK_OUT_S")
        map.Add("SEND_SHORI_FLG", "SEND_SHORI_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_INKA_PLAN_SEND")

        MyBase.SetResultCount(ds.Tables("LMB010OUT_INKA_PLAN_SEND").Rows.Count)
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' キャンセルデータ抜出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSendCancel(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_INKA_PLAN_SEND").Rows(0)

        'SQL構築
        Me._StrSql.AppendLine(LMB010DAC.SQL_SELECT_SEND_CANCEL)
        Me._StrSql.AppendLine(LMB010DAC.SQL_SEND_CANCEL_ORDER_BY)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetInkaParameterOnCalt(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectSendCancel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("SEND_SEQ", "SEND_SEQ")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_OUT_L", "REMARK_OUT_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("OUTKA_FROM_ORD_NO_M", "OUTKA_FROM_ORD_NO_M")
        map.Add("BUYER_ORD_NO_M", "BUYER_ORD_NO_M")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("INKA_WT", "INKA_WT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("IRIME", "IRIME")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("GOODS_COND_KB_3", "GOODS_COND_KB_3")
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("SPD_KB", "SPD_KB")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("ALLOC_PRIORITY", "ALLOC_PRIORITY")
        map.Add("REMARK_OUT_S", "REMARK_OUT_S")
        map.Add("SEND_SHORI_FLG", "SEND_SHORI_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_INKA_PLAN_SEND")

        MyBase.SetResultCount(ds.Tables("LMB010OUT_INKA_PLAN_SEND").Rows.Count)
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 入荷報告データ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendInkaData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010OUT_INKA_PLAN_SEND").Rows(0)

        'SQL構築
        Me._StrSql.Append(LMB010DAC.SQL_INSERT_SEND_INKA_DATA)

        'DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetInkaInsertParameterOnCalt(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystems()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertSendInka", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    '2014.04.17 CALT連携対応 Ri --ED--

    '2014.05.16 追加START
    ''' ==========================================================================
    ''' <summary>出荷ピッキングWK情報登録メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function InsertCOutkaPickWk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKA_PICK_WK")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_C_OUTKA_PICK_WK, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = ds.Tables("LMB010IN_OUTKA_PICK_WK").Rows.Count - 1

        For i As Integer = 0 To max
            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetOutkaPickWkInsertParameter(ds, i)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertCOutkaPickWk", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)
        Next

        Return ds

    End Function

    '2014.05.16 追加END

#Region "特定の荷主固有のテーブルが存在するか否かの判定"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010_TBL_EXISTS")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMB010DAC.SQL_GET_TRN_TBL_EXISTS)

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TBL_NM", Me._Row.Item("TBL_NM").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Me._Row.Item("TBL_EXISTS") = "0"

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

            ' パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    Me._Row.Item("TBL_EXISTS") = Convert.ToString(reader("TBL_EXISTS"))
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region ' "特定の荷主固有のテーブルが存在するか否かの判定"

#Region "RFIDラベルデータ取得処理"

    ''' <summary>
    ''' RFIDラベルデータ取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectRfidLavelData(ByVal ds As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_PRINT_RFID").Rows(0)

        ' SQL構築
        Me._StrSql.AppendLine(LMB010DAC.SQL_SELECT_RFID_LAVEL_DATA)

        ' DBスキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Dim cnt As Integer = 0

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータ設定
            Me._SqlPrmList = New ArrayList()
            Call Me.SetSelectRfidLavelDataParameter(Me._Row, Me._SqlPrmList)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("LVL1_UT", "LVL1_UT")
                map.Add("INKA_NO_L", "INKA_NO_L")
                map.Add("INKA_NO_M", "INKA_NO_M")
                map.Add("TSMC_QTY", "TSMC_QTY")
                map.Add("MATERIAL_NO", "MATERIAL_NO")
                map.Add("EXPIRATION_DATE", "EXPIRATION_DATE")
                map.Add("COMPANY_ID", "COMPANY_ID")
                map.Add("RAW_LOT_ID", "RAW_LOT_ID")
                map.Add("SEQUENCE_ID", "SEQUENCE_ID")
                map.Add("CYLINDER_NO", "CYLINDER_NO")
                map.Add("LOT_ID", "LOT_ID")
                map.Add("VENDOR_MATERIAL_NO", "VENDOR_MATERIAL_NO")
                map.Add("HAZARDOUS", "HAZARDOUS")
                map.Add("SERIAL_NO", "SERIAL_NO")
                map.Add("SECOND_LAYER_PPN", "SECOND_LAYER_PPN")
                map.Add("BOX_SERIAL_NO", "BOX_SERIAL_NO")
                map.Add("SHIP_TO", "SHIP_TO")
                map.Add("THIRD_LAYER_PPN", "THIRD_LAYER_PPN")
                map.Add("PACKAGING_ID", "PACKAGING_ID")
                map.Add("VENDER_CODE", "VENDER_CODE")
                map.Add("DUNS_NO", "DUNS_NO")
                map.Add("SAMPLE", "SAMPLE")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB010OUT_PRINT_RFID")
                cnt = ds.Tables("LMB010OUT_PRINT_RFID").Rows.Count()

            End Using

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "RFIDラベルデータ取得処理"

#End Region

#Region "設定処理"

#Region "Insert"

#Region "H_INKAEDI_HED_DPN"

    ''' <summary>
    ''' H_INKAEDI_HED_DPN新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>H_INKAEDI_HED_DPN新規登録SQLの構築・発行</remarks>
    Private Function InsertHInkaEdiHedDpnData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_EDI")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_HED_DPN, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetEdiComParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertHInkaEdiHedDpnData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_DTL_DPN"

    ''' <summary>
    ''' H_INKAEDI_DTL_DPN新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>H_INKAEDI_DTL_DPN新規登録SQLの構築・発行</remarks>
    Private Function InsertHInkaEdiDtlDpnData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_EDI")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_DTL_DPN, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetEdiComParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertHInkaEdiDtlDpnData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' H_INKAEDI_L新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>H_INKAEDI_L新規登録SQLの構築・発行</remarks>
    Private Function InsertHInkaEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_EDI")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_INKA_EDI_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetEdiComParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertHInkaEdiLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' H_INKAEDI_M新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>H_INKAEDI_M新規登録SQLの構築・発行</remarks>
    Private Function InsertHInkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_EDI")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_INKA_EDI_M, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetEdiComParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertHInkaEdiMData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
#Region "C_OUTKA_L"

    ''' <summary>
    ''' C_OUTKA_L新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>C_OUTKA_L新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKAL")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_OUTKAL, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = ds.Tables("LMB010IN_OUTKAL").Rows.Count - 1

        For i As Integer = 0 To max
            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetOutkaLInsertParameter(ds, i)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertOutkaL", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)
        Next

        Return ds

    End Function

#End Region

#Region "C_OUTKA_M"

    ''' <summary>
    ''' C_OUTKA_M新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>C_OUTKA_M新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_OUTKAM")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_OUTKAM, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = ds.Tables("LMB010IN_OUTKAM").Rows.Count - 1

        For i As Integer = 0 To max
            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetOutkaMInsertParameter(ds, i)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertOutkaM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)
        Next

        Return ds

    End Function

#End Region

#Region "F_UNSO_L"

    ''' <summary>
    ''' F_UNSO_L新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>F_UNSO_L新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_UNSOL")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_UNSOL, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = ds.Tables("LMB010IN_UNSOL").Rows.Count - 1

        For i As Integer = 0 To max
            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetUnsoLInsertParameter(ds, i)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertUnsoL", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)
        Next

        Return ds

    End Function

#End Region

#Region "F_UNSO_M"

    ''' <summary>
    ''' F_UNSO_M新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>F_UNSO_M新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_UNSOM")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB010DAC.SQL_INSERT_UNSOM, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim max As Integer = ds.Tables("LMB010IN_UNSOM").Rows.Count - 1

        For i As Integer = 0 To max
            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetParamCommonSystemIns()
            Call Me.SetUnsoMInsertParameter(ds, i)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMB010DAC", "InsertUnsoM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)
        Next

        Return ds

    End Function

#End Region
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'WIT対応 入荷データ一括取込対応 kasama Start
#Region "B_INKA_S"

    ''' ==========================================================================
    ''' <summary>入荷S情報登録メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function InsertInkaS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_INKA_S")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_INSERT_B_INKA_S, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Me.SetInsertBInkaSParameterList(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "D_ZAI_TRS"

    ''' ==========================================================================
    ''' <summary>在庫情報登録メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function InsertZaiTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010IN_ZAI_TRS")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_INSERT_D_ZAI_TRS, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Me.SetInsertDZaiTrsParameterList(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region
    'WIT対応 入荷データ一括取込対応 kasama End

#End Region

#Region "Update"

    'WIT対応 入荷データ一括取込対応 kasama Start

#Region "B_INKA_L"

    ''' <summary>
    ''' 入荷(大)テーブル更新（入荷一括取込）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaIkkatuTorikomi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMB010IN_INKA_L").Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMB010DAC.SQL_UPDATE_INKA_IKKATU_TORIKOMI, Me._Row.Item("NRS_BR_CD").ToString())

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetUpdInkaIkkatuTorikomiParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetParamCommonSystemUp()

        '印刷種別によりSQL修正
        sql = Me.SetUpNm(Me._Row, sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "UpdateInkaIkkatuTorikomi", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "B_INKA_WK"

    ''' ==========================================================================
    ''' <summary>入荷WK情報更新メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function UpdateInkaWk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB010OUT_TORIKOMI_SRC")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        '2014.06.06 FFEM対応 追加START
        Dim sql As String = String.Empty
        If String.Concat(inRow.Item("NRS_BR_CD").ToString(), inRow.Item("CUST_CD_L").ToString(), inRow.Item("CUST_CD_M").ToString()).Equals("100013500") = True OrElse
            String.Concat(inRow.Item("NRS_BR_CD").ToString(), inRow.Item("CUST_CD_L").ToString(), inRow.Item("CUST_CD_M").ToString()).Equals("400055500") = True OrElse
            String.Concat(inRow.Item("NRS_BR_CD").ToString(), inRow.Item("CUST_CD_L").ToString(), inRow.Item("CUST_CD_M").ToString()).Equals("600013500") = True Then
            sql = Me.SetSchemaNm(LMB010DAC.SQL_UPDATE_B_INKA_WK_FFEM, inRow("NRS_BR_CD").ToString)
        Else
            sql = Me.SetSchemaNm(LMB010DAC.SQL_UPDATE_B_INKA_WK, inRow("NRS_BR_CD").ToString)
        End If
        '2014.06.06 FFEM対応 追加END
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Me._SqlPrmList = New ArrayList
        Me.SetUpdateBInkaWKParameterList(inRow)
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region
    'WIT対応 入荷データ一括取込対応 kasama End

#Region "言語取得"
    '20160614 tsunehira add start
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable
        inTbl = ds.Tables("LMB010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB010DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function
    '20160614 tsunehira add End
#End Region




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
    
        '20160614 tsunehira add start
    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '20160614 tsunehira add end


    'START YANAI メモ②No.28
    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(EDI)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiComParameter(ByVal prmList As ArrayList)

        With Me._Row

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PLANT_CD", .Item("PLANT_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", .Item("RECORD_STATUS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", .Item("SYS_ENT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", .Item("SYS_ENT_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END YANAI メモ②No.28

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 荷主明細マスタの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetCustDetailsSelectParameter(ByVal ds As DataSet)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_SAGYO").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(.Tables("LMB010IN_SAGYO").Rows(0).Item("CUST_CD_L").ToString(), "%"), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 作業レコードの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoSelectParameter(ByVal ds As DataSet)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_SAGYO").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Tables("LMB010IN_SAGYO").Rows(0).Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 作業マスタの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoMstSelectParameter(ByVal ds As DataSet)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_SAGYOMST").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Tables("LMB010IN_SAGYOMST").Rows(0).Item("SAGYO_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 作業レコードの追加パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoInsParameter(ByVal ds As DataSet)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010OUT_SAGYO").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Tables("LMB010OUT_SAGYO").Rows(0).Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Tables("LMB010OUT_SAGYO").Rows(0).Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Tables("LMB010OUT_SAGYO").Rows(0).Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_CD1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Tables("LMB010OUT_SAGYO").Rows(0).Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Tables("LMB010OUT_SAGYO").Rows(0).Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Tables("LMB010OUT_SAGYO").Rows(0).Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Tables("LMB010OUT_SAGYO").Rows(0).Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Tables("LMB010OUT_SAGYO").Rows(0).Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Tables("LMB010OUT_SAGYO").Rows(0).Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Tables("LMB010OUT_SAGYO").Rows(0).Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Tables("LMB010OUT_SAGYO").Rows(0).Item("INV_TANI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", Me.FormatNumValue(.Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", Me.FormatNumValue(.Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_UP").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", Me.FormatNumValue(.Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_GK").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Tables("LMB010OUT_SAGYO").Rows(0).Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))  '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Tables("LMB010OUT_SAGYO").Rows(0).Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Tables("LMB010OUT_SAGYO").Rows(0).Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Tables("LMB010OUT_SAGYO").Rows(0).Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Tables("LMB010OUT_SAGYO").Rows(0).Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ作成用データの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaDataSelectParameter(ByVal ds As DataSet)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_OUTKA").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Tables("LMB010IN_OUTKA").Rows(0).Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_INIT", .Tables("LMB010IN_OUTKA").Rows(0).Item("OUTKA_DATE_INIT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", .Tables("LMB010IN_OUTKA").Rows(0).Item("ROW_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷データ(大)の追加パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLInsertParameter(ByVal ds As DataSet, ByVal rowNo As Integer)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("FURI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("PICK_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("ARR_KANRYO_INFO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@END_DATE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("END_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("SHIP_CD_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DENP_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("PC_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_KB", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DEST_AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("ALL_PRINT_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("NIHUDA_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("NHS_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("DENP_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("COA_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("HOKOKU_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("MATOME_PICK_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("LAST_PRINT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("LAST_PRINT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SASZ_USER", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("SASZ_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKO_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_USER", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("KEN_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("OUTKA_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOU_USER", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("HOU_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", .Tables("LMB010IN_OUTKAL").Rows(rowNo).Item("ORDER_TYPE").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷データ(中)の追加パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMInsertParameter(ByVal ds As DataSet, ByVal rowNo As Integer)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_HASU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_TTL_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("ALCTD_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("ALCTD_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("BACKLOG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("BACKLOG_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("IRIME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("OUTKA_M_PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("SIZE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Tables("LMB010IN_OUTKAM").Rows(rowNo).Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' 出荷ピックWKの追加パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaPickWkInsertParameter(ByVal ds As DataSet, ByVal rowNo As Integer)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_OUTKA_PICK_WK").Rows(rowNo).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Tables("LMB010IN_OUTKA_PICK_WK").Rows(rowNo).Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Tables("LMB010IN_OUTKA_PICK_WK").Rows(rowNo).Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Tables("LMB010IN_OUTKA_PICK_WK").Rows(rowNo).Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKI_STATE_KBN", .Tables("LMB010IN_OUTKA_PICK_WK").Rows(rowNo).Item("HIKI_STATE_KBN").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送データ(大)の追加パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLInsertParameter(ByVal ds As DataSet, ByVal rowNo As Integer)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TRIP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("BIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("JIYU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("SHIP_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("NB_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("PC_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("VCLE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Tables("LMB010IN_UNSOL").Rows(rowNo).Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送データ(中)の追加パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMInsertParameter(ByVal ds As DataSet, ByVal rowNo As Integer)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("UNSO_TTL_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("NB_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("UNSO_TTL_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@QT_UT", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("QT_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("HASU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("IRIME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("IRIME_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("SIZE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Tables("LMB010IN_UNSOM").Rows(rowNo).Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'START s.kobayashi 要望番号1784 
    ''' <summary>
    ''' 出荷データ作成用データの検索パラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetSelectInkaCancelDataParameter(ByVal ds As DataSet)

        With ds

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB010IN_INKA_L").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Tables("LMB010IN_INKA_L").Rows(0).Item("INKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'WIT対応 入荷データ一括取込対応 kasama Start

    ''' <summary>
    ''' 荷主明細マスタの検索パラメータ設定
    ''' </summary>
    ''' <param name="inRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSelectCustDetailParameter(ByVal inRow As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(inRow("CUST_CD").ToString(), "%"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_KB", inRow("SUB_KB").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 商品情報取得用パラメータを設定します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <remarks></remarks>
    Private Sub SetSelectTorikomiSrcDataParameterList(ByVal inRow As DataRow)

        With Me._SqlPrmList
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 荷主明細(一括取込)情報取得用パラメータを設定します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <remarks></remarks>
    Private Sub SetSelectInkaLCountBySysDateTimeParameterList(ByVal inRow As DataRow)

        With Me._SqlPrmList
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", inRow("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", inRow("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 入荷S情報登録用パラメータを設定します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <remarks></remarks>
    Private Sub SetInsertBInkaSParameterList(ByVal inRow As DataRow)

        With Me._SqlPrmList

            ' SET
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_M", inRow("INKA_NO_M").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_S", inRow("INKA_NO_S").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ZAI_REC_NO", inRow("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@LOT_NO", inRow("LOT_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@LOCA", inRow("LOCA").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@TOU_NO", inRow("TOU_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@SITU_NO", inRow("SITU_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ZONE_CD", inRow("ZONE_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@KONSU", Me.FormatNumValue(inRow("KONSU").ToString), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(inRow("HASU").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(inRow("IRIME").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(inRow("BETU_WT").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@SERIAL_NO", inRow("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", inRow("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", inRow("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", inRow("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", inRow("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@LT_DATE", inRow("LT_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SPD_KB", inRow("SPD_KB").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@OFB_KB", inRow("OFB_KB").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@DEST_CD", inRow("DEST_CD").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@REMARK", inRow("REMARK").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", inRow("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@REMARK_OUT", inRow("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
        End With

        ' 共通
        Me.SetParamCommonSystemIns()
    End Sub

    ''' <summary>
    ''' 在庫情報登録用パラメータを設定します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <remarks></remarks>
    Private Sub SetInsertDZaiTrsParameterList(ByVal inRow As DataRow)

        With Me._SqlPrmList

            ' SET
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ZAI_REC_NO", inRow("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@WH_CD", inRow("WH_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@TOU_NO", inRow("TOU_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@SITU_NO", inRow("SITU_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ZONE_CD", inRow("ZONE_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@LOCA", inRow("LOCA").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@LOT_NO", inRow("LOT_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@CUST_CD_L", inRow("CUST_CD_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@CUST_CD_M", inRow("CUST_CD_M").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", inRow("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", inRow("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_M", inRow("INKA_NO_M").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_S", inRow("INKA_NO_S").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", inRow("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@RSV_NO", inRow("RSV_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@SERIAL_NO", inRow("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@HOKAN_YN", inRow("HOKAN_YN").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@TAX_KB", inRow("TAX_KB").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", inRow("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", inRow("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", inRow("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@OFB_KB", inRow("OFB_KB").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SPD_KB", inRow("SPD_KB").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@REMARK_OUT", inRow("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(inRow("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(inRow("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(inRow("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(inRow("IRIME").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(inRow("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(inRow("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(inRow("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@INKO_DATE", inRow("INKO_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", inRow("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ZERO_FLAG", inRow("ZERO_FLAG").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@LT_DATE", inRow("LT_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", inRow("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@DEST_CD_P", inRow("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@REMARK", inRow("REMARK").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@SMPL_FLAG", inRow("SMPL_FLAG").ToString(), DBDataType.CHAR))
        End With

        ' 共通
        Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' 入荷WK情報更新用パラメータを設定します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateBInkaWKParameterList(ByVal inRow As DataRow)

        With Me._SqlPrmList

            ' SET
            .Add(MyBase.GetSqlParameter("@INKA_NO_S", inRow("INKA_NO_S").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@TOU_NO", inRow("TOU_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SITU_NO", inRow("SITU_NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@ZONE_CD", inRow("ZONE_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@LOCA", inRow("LOCA").ToString(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@NYUKO_TANTO", MyBase.GetUserID(), DBDataType.NVARCHAR))
            .Add(MyBase.GetSqlParameter("@NYUKO_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@NYUKO_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

            ' WHERE
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_M", inRow("INKA_NO_M").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SEQ", inRow("SEQ").ToString(), DBDataType.CHAR))
        End With

        ' 共通
        Me.SetParamCommonSystemUp()

    End Sub

    ''' <summary>
    ''' 入荷(大)の更新(入荷一括取込時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdInkaIkkatuTorikomiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'WIT対応 入荷データ一括取込対応 kasama End

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

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

    '2014.04/17 CALT連携対応 ri --ST--

    ''' <summary>
    ''' 入荷(大)ステータス更新
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaParameterOnCalt(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 入荷予定データ作成
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaInsertParameterOnCalt(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_SEQ", .Item("SEND_SEQ").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_KBN", .Item("DATA_KBN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", .Item("CUST_NM_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Item("BUYER_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_L", .Item("REMARK_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT_L", .Item("REMARK_OUT_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", .Item("GOODS_NM_1").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", .Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", .Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_M", .Item("REMARK_M").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NB", .Item("INKA_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_WT", .Item("INKA_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KONSU", .Item("KONSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HASU", .Item("HASU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JAN_CD", .Item("JAN_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_STR_DATE", .Item("ONDO_STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_END_DATE", .Item("ONDO_END_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_MX", .Item("ONDO_MX").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_MM", .Item("ONDO_MM").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_S", .Item("REMARK_S").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT_S", .Item("REMARK_OUT_S").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_SHORI_FLG", .Item("SEND_SHORI_FLG").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystems()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    '2014.04/17 CALT連携対応 ri --ED--

#Region "SQL条件設定 作業指示書の更新"

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 入荷(大)の更新(印刷時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrintParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 更新項目設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetUpNm(ByVal conditionRow As DataRow, ByVal sql As String) As String

        Dim printKb As String = conditionRow.Item("PRINT_KB").ToString()
        Dim update1 As String = String.Empty
        Dim update2 As String = String.Empty

        '入荷更新項目の設定
        Select Case printKb
            'UPD2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
            'Case "01", "04", "05" '入荷報告
            Case "01", "04", "05", "07" '入荷報告 ADD 2018/11/01 入荷報告(角印)追加
                update1 = "HOUKOKUSYO_PR_DATE"
                update2 = "HOUKOKUSYO_PR_USER"
            Case "02" 'チェックリスト
                update1 = "CHECKLIST_PRT_DATE"
                update2 = "CHECKLIST_PRT_USER"
            Case "03" '入荷受付表
                update1 = "UKETSUKELIST_PRT_DATE"
                update2 = "UKETSUKELIST_PRT_USER"
        End Select

        sql = sql.Replace("$UPDATE1$", update1)
        sql = sql.Replace("$UPDATE2$", update2)

        Return sql

    End Function
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

#End Region

#Region "入荷報告取消"
    ''' <summary>
    ''' 入荷(大)の更新(印刷時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdInkaHoukokuCancelParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub
#End Region

#Region "RFIDラベルデータ取得処理"

    ''' <summary>
    ''' RFIDラベルデータ取得処理 パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    Private Sub SetSelectRfidLavelDataParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DATE_YYMMDD", MyBase.GetSystemDate().Substring(2, 6), DBDataType.VARCHAR))

        End With

    End Sub

#End Region ' "RFIDラベルデータ取得処理"

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#Region "ユーティリティ"

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    '''' <summary>
    '''' Update文の発行
    '''' </summary>
    '''' <param name="cmd">SQLコマンド</param>
    '''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    '''' <remarks></remarks>
    'Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

    '    'SQLの発行
    '    If MyBase.GetUpdateResult(cmd) < 1 Then
    '        MyBase.SetMessage("E011")
    '        Return False
    '    End If

    '    Return True

    'End Function
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
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

#End Region
    'END YANAI 20120121 作業一括処理対応


#End Region

#End Region

#End Region

End Class
