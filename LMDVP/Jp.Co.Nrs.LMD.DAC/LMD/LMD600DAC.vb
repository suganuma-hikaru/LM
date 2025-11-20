' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD600    : 振替伝票
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD600DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD600DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum SelectCondition As Integer
        PTN1  '出力対象帳票パターン取得
        PTN2  'データ検索
        PTN3  '振替日検索            'ADD 2018/12/25 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい
    End Enum

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMD600DAC"

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMD600IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMD600OUT"

    ''' <summary>
    ''' 帳票ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PTN_ID As String = "38"

#End Region '制御用

#Region "SQL"

#Region "帳票ID"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	 @NRS_BR_CD                                          AS NRS_BR_CD    " & vbNewLine _
                                            & "	,@PTN_ID                                             AS PTN_ID       " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                    " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                    " & vbNewLine _
                                            & "	      ELSE MR3.PTN_CD END                            AS PTN_CD       " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                    " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "	      ELSE MR3.RPT_ID END                            AS RPT_ID       " & vbNewLine _
                                            & "	FROM                                                                 " & vbNewLine _
                                            & "	 $LM_TRN$..C_OUTKA_L C01                                             " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_TRN$..C_OUTKA_M C02                                             " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 C02.NRS_BR_CD   = @NRS_BR_CD                                        " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C01.OUTKA_NO_L  = C02.OUTKA_NO_L                                    " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C02.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                            & "	--商品マスタ（出荷）                                                 " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_MST$..M_GOODS OUT_M_GOODS                                       " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 OUT_M_GOODS.NRS_BR_CD   = @NRS_BR_CD                                " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C02.GOODS_CD_NRS        = OUT_M_GOODS.GOODS_CD_NRS                  " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 OUT_M_GOODS.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                            & "	--出荷Lでの荷主帳票パターン取得                                      " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_MST$..M_CUST_RPT MCR1                                           " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 MCR1.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C01.CUST_CD_L  = MCR1.CUST_CD_L                                     " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C01.CUST_CD_M  = MCR1.CUST_CD_M                                     " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MCR1.CUST_CD_S = '00'                                               " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MCR1.PTN_ID    = @PTN_ID                                            " & vbNewLine _
                                            & "	--帳票パターン取得                                                   " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_MST$..M_RPT MR1                                                 " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 MR1.NRS_BR_CD = @NRS_BR_CD                                          " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MR1.PTN_ID    = MCR1.PTN_ID                                         " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MR1.PTN_CD    = MCR1.PTN_CD                                         " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & "	--商品Mの荷主での荷主帳票パターン取得                                " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_MST$..M_CUST_RPT MCR2                                           " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 MCR2.NRS_BR_CD  = @NRS_BR_CD                                        " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 OUT_M_GOODS.CUST_CD_L = MCR2.CUST_CD_L                              " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 OUT_M_GOODS.CUST_CD_M = MCR2.CUST_CD_M                              " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 OUT_M_GOODS.CUST_CD_S = MCR2.CUST_CD_S                              " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MCR2.PTN_ID           = @PTN_ID                                     " & vbNewLine _
                                            & "	--帳票パターン取得                                                   " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_MST$..M_RPT MR2                                                 " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 MR2.NRS_BR_CD = @NRS_BR_CD                                          " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MR2.PTN_ID    = MCR2.PTN_ID                                         " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MR2.PTN_CD    = MCR2.PTN_CD                                         " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & "	--存在しない場合の帳票パターン取得                                   " & vbNewLine _
                                            & "	LEFT JOIN                                                            " & vbNewLine _
                                            & "	 $LM_MST$..M_RPT MR3                                                 " & vbNewLine _
                                            & "	ON                                                                   " & vbNewLine _
                                            & "	 MR3.NRS_BR_CD     = @NRS_BR_CD                                      " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MR3.PTN_ID        = @PTN_ID                                         " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 MR3.STANDARD_FLAG = '01'                                            " & vbNewLine _
                                            & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                            & "	WHERE                                                                " & vbNewLine _
                                            & "	 C01.NRS_BR_CD   = @NRS_BR_CD                                        " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C01.FURI_NO     = @FURI_NO                                          " & vbNewLine _
                                            & "	AND                                                                  " & vbNewLine _
                                            & "	 C01.SYS_DEL_FLG = '0'                                               " & vbNewLine

#End Region

    ''' <summary>
    ''' 印刷データ取得SQL（ヘッダ）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FIRST As String = " SELECT                                                                            " & vbNewLine _
                                              & "   MAX(MAIN.RPT_ID)                     AS RPT_ID                                 " & vbNewLine _
                                              & " , MAX(MAIN.NRS_BR_CD)                  AS NRS_BR_CD                              " & vbNewLine _
                                              & " , MAX(MAIN.FURI_NO)                    AS FURI_NO                                " & vbNewLine _
                                              & " , MAX(MAIN.NRS_BR_NM)                  AS NRS_BR_NM                              " & vbNewLine _
                                              & " , MAX(MAIN.FURIKAE_KBN)                AS FURIKAE_KBN                            " & vbNewLine _
                                              & " , MAX(MAIN.INKA_DATE)                  AS INKA_DATE                              " & vbNewLine _
                                              & " , MAX(MAIN.YOUKI_HENKO)                AS YOUKI_HENKO                            " & vbNewLine _
                                              & " , MAX(MAIN.TOUKI_HOKAN_YN)             AS TOUKI_HOKAN_YN                         " & vbNewLine _
                                              & " , MAX(MAIN.WH_NM)                      AS WH_NM                                  " & vbNewLine _
                                              & " , MAIN.OUTKA_NO_L                      AS OUTKA_NO_L                             " & vbNewLine _
                                              & " , MAX(MAIN.OUT_CUST_CD_L)              AS OUT_CUST_CD_L                          " & vbNewLine _
                                              & " , MAX(MAIN.OUT_CUST_CD_M)              AS OUT_CUST_CD_M                          " & vbNewLine _
                                              & " , MAX(MAIN.OUT_CUST_NM_L)              AS OUT_CUST_NM_L                          " & vbNewLine _
                                              & " , MAX(MAIN.OUT_CUST_NM_M)              AS OUT_CUST_NM_M                          " & vbNewLine _
                                              & " , MAX(MAIN.OUT_GOODS_NM_1)             AS OUT_GOODS_NM_1                         " & vbNewLine _
                                              & " --(2012.11.08)要望番号1546 -- START --                                           " & vbNewLine _
                                              & " , MAX(MAIN.OUT_GOODS_CD_CUST)          AS OUT_GOODS_CD_CUST                      " & vbNewLine _
                                              & " --(2012.11.08)要望番号1546 --  END  --                                           " & vbNewLine _
                                              & " , MAX(MAIN.LOT_NO)                     AS LOT_NO                                 " & vbNewLine _
                                              & " , MAX(MAIN.SERIAL_NO)                  AS SERIAL_NO                              " & vbNewLine _
                                              & " , MAX(MAIN.OUTKA_FROM_ORD_NO_L)        AS OUTKA_FROM_ORD_NO_L                    " & vbNewLine _
                                              & " , MAX(MAIN.OUT_NIYAKU_YN)              AS OUT_NIYAKU_YN                          " & vbNewLine _
                                              & " , MAX(MAIN.OUT_TAX_KB)                 AS OUT_TAX_KB                             " & vbNewLine _
                                              & " , MAX(MAIN.OUT_IRIME)                  AS OUT_IRIME                              " & vbNewLine _
                                              & " , MAX(MAIN.IRIME_UT)                   AS IRIME_UT                               " & vbNewLine _
                                              & " , MAX(MAIN.OUTKA_M_PKG_NB)             AS OUTKA_M_PKG_NB                         " & vbNewLine _
                                              & " , MAX(MAIN.OUTKA_HASU)                 AS OUTKA_HASU                             " & vbNewLine _
                                              & " , MAX(MAIN.PKG_NB)                     AS PKG_NB                                 " & vbNewLine _
                                              & " , MAX(MAIN.OUTKA_TTL_NB)               AS OUTKA_TTL_NB                           " & vbNewLine _
                                              & " , MAX(MAIN.ALCTD_NB)                   AS ALCTD_NB                               " & vbNewLine _
                                              & " , MAX(MAIN.BACKLOG_NB)                 AS BACKLOG_NB                             " & vbNewLine _
                                              & " , MAX(MAIN.OUTKA_ATT)                  AS OUTKA_ATT                              " & vbNewLine _
                                              & " --, MAX(MAIN.OUT_SAGYO_NM_1)             AS OUT_SAGYO_NM_1                         " & vbNewLine _
                                              & " --, MAX(MAIN.OUT_SAGYO_NM_2)             AS OUT_SAGYO_NM_2                         " & vbNewLine _
                                              & " --, MAX(MAIN.OUT_SAGYO_NM_3)             AS OUT_SAGYO_NM_3                         " & vbNewLine _
                                              & "-- --20160708 Timeout対応                                                                           " & vbNewLine _
                                              & " , ISNULL(                                                                               " & vbNewLine _
                                              & " (                                                                                       " & vbNewLine _
                                              & " SELECT BASE.SAGYO_NM                                                                    " & vbNewLine _
                                              & "    FROM                                                                                 " & vbNewLine _
                                              & "        (                                                                                " & vbNewLine _
                                              & "		SELECT      TOP 3                                                        " & vbNewLine _
                                              & "       SAGYO_NM AS SAGYO_NM                                                              " & vbNewLine _
                                              & "	   ,ROW_NUMBER() OVER                                                            " & vbNewLine _
                                              & "          (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                              & "          AS  NUM                                                                        " & vbNewLine _
                                              & "         FROM $LM_TRN$..E_SAGYO E                                                       " & vbNewLine _
                                              & "         WHERE E.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                              & "           AND E.INOUTKA_NO_LM =(MAIN.OUTKA_NO_L + MAIN.OUTKA_NO_M)                      " & vbNewLine _
                                              & "           AND E.IOZS_KB='21'                                                            " & vbNewLine _
                                              & "		   AND E.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                   " & vbNewLine _
                                              & "        ) AS BASE                                                                        " & vbNewLine _
                                              & "    WHERE BASE.NUM = 1                                                                   " & vbNewLine _
                                              & "	)                                                                                " & vbNewLine _
                                              & "	,'')                                                                             " & vbNewLine _
                                              & "	                AS OUT_SAGYO_NM_1                                                " & vbNewLine _
                                              & " , ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                              & "    FROM                                                                                 " & vbNewLine _
                                              & "        (SELECT              TOP 3                                                       " & vbNewLine _
                                              & "       SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                              & "          (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                              & "          AS  NUM                                                                        " & vbNewLine _
                                              & "         FROM $LM_TRN$..E_SAGYO E                                                       " & vbNewLine _
                                              & "         WHERE E.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                              & "           AND E.INOUTKA_NO_LM = (MAIN.OUTKA_NO_L + MAIN.OUTKA_NO_M)                     " & vbNewLine _
                                              & "           AND E.IOZS_KB='21'                                                            " & vbNewLine _
                                              & "		   AND E.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                   " & vbNewLine _
                                              & "        ) AS BASE                                                                        " & vbNewLine _
                                              & "    WHERE BASE.NUM = 2),'')                                      AS OUT_SAGYO_NM_2       " & vbNewLine _
                                              & " , ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                              & "    FROM                                                                                 " & vbNewLine _
                                              & "        (SELECT            TOP 3                                                         " & vbNewLine _
                                              & "       SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                              & "          (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                              & "          AS  NUM                                                                        " & vbNewLine _
                                              & "         FROM $LM_TRN$..E_SAGYO E                                                       " & vbNewLine _
                                              & "         WHERE E.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                              & "           AND E.INOUTKA_NO_LM = (MAIN.OUTKA_NO_L + MAIN.OUTKA_NO_M)                     " & vbNewLine _
                                              & "           AND E.IOZS_KB='21'                                                            " & vbNewLine _
                                              & "		   AND E.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                   " & vbNewLine _
                                              & "        ) AS BASE                                                                        " & vbNewLine _
                                              & "    WHERE BASE.NUM = 3),'')                                      AS OUT_SAGYO_NM_3       " & vbNewLine _
                                              & "----20160708 Timeout対応                                                                             " & vbNewLine _
                                              & " , MAIN.INKA_NO_L                       AS INKA_NO_L                              " & vbNewLine _
                                              & " , MAX(MAIN.IN_CUST_CD_L)               AS IN_CUST_CD_L                           " & vbNewLine _
                                              & " , MAX(MAIN.IN_CUST_CD_M)               AS IN_CUST_CD_M                           " & vbNewLine _
                                              & " , MAX(MAIN.IN_CUST_NM_L)               AS IN_CUST_NM_L                           " & vbNewLine _
                                              & " , MAX(MAIN.IN_CUST_NM_M)               AS IN_CUST_NM_M                           " & vbNewLine _
                                              & " , MAX(MAIN.IN_GOODS_NM_1)              AS IN_GOODS_NM_1                          " & vbNewLine _
                                              & " --(2012.11.08)要望番号1546 -- START --                                           " & vbNewLine _
                                              & " , MAX(MAIN.IN_GOODS_CD_CUST)          AS IN_GOODS_CD_CUST                        " & vbNewLine _
                                              & " --(2012.11.08)要望番号1546 --  END  --                                           " & vbNewLine _
                                              & " , MAX(MAIN.DENP_NO)                    AS DENP_NO                                " & vbNewLine _
                                              & " , MAX(MAIN.IN_NIYAKU_YN)               AS IN_NIYAKU_YN                           " & vbNewLine _
                                              & " , MAX(MAIN.IN_TAX_KB)                  AS IN_TAX_KB                              " & vbNewLine _
                                              & " , MAX(MAIN.IN_IRIME)                   AS IN_IRIME                               " & vbNewLine _
                                              & " , MAX(MAIN.STD_IRIME_UT_01)            AS STD_IRIME_UT_01                        " & vbNewLine _
                                              & " , MAX(MAIN.INKA_ATT)                   AS INKA_ATT                               " & vbNewLine _
                                              & " --, MAX(MAIN.IN_SAGYO_NM_1)              AS IN_SAGYO_NM_1                          " & vbNewLine _
                                              & " --, MAX(MAIN.IN_SAGYO_NM_2)              AS IN_SAGYO_NM_2                          " & vbNewLine _
                                              & " --, MAX(MAIN.IN_SAGYO_NM_3)              AS IN_SAGYO_NM_3                          " & vbNewLine _
                                              & " ----20160708 TimeOut対応                                                                " & vbNewLine _
                                              & "  , ISNULL((SELECT BASE.SAGYO_NM                                                         " & vbNewLine _
                                              & "    FROM                                                                                 " & vbNewLine _
                                              & "        (SELECT     TOP 3                                                                " & vbNewLine _
                                              & "       SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                              & "          (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                              & "          AS  NUM                                                                        " & vbNewLine _
                                              & "         FROM $LM_TRN$..E_SAGYO E                                                       " & vbNewLine _
                                              & "         WHERE E.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                              & "           AND E.INOUTKA_NO_LM = (MAIN.INKA_NO_L + MAIN.INKA_NO_M)                       " & vbNewLine _
                                              & "           AND E.IOZS_KB='11'                                                            " & vbNewLine _
                                              & "		   AND E.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                   " & vbNewLine _
                                              & "        ) AS BASE                                                                        " & vbNewLine _
                                              & "    WHERE BASE.NUM = 1),'')                                      AS IN_SAGYO_NM_1        " & vbNewLine _
                                              & " , ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                              & "    FROM                                                                                 " & vbNewLine _
                                              & "        (SELECT       TOP 3                                                              " & vbNewLine _
                                              & "       SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                              & "          (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                              & "          AS  NUM                                                                        " & vbNewLine _
                                              & "         FROM $LM_TRN$..E_SAGYO E                                                       " & vbNewLine _
                                              & "         WHERE E.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                              & "           AND E.INOUTKA_NO_LM = (MAIN.INKA_NO_L + MAIN.INKA_NO_M)                       " & vbNewLine _
                                              & "           AND E.IOZS_KB='11'                                                            " & vbNewLine _
                                              & "		   AND E.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                   " & vbNewLine _
                                              & "        ) AS BASE                                                                        " & vbNewLine _
                                              & "    WHERE BASE.NUM = 2),'')                                      AS IN_SAGYO_NM_2        " & vbNewLine _
                                              & " , ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                              & "    FROM                                                                                 " & vbNewLine _
                                              & "        (SELECT       TOP 3                                                              " & vbNewLine _
                                              & "       SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                              & "          (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                              & "          AS  NUM                                                                        " & vbNewLine _
                                              & "         FROM $LM_TRN$..E_SAGYO E                                                       " & vbNewLine _
                                              & "         WHERE E.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                              & "           AND E.INOUTKA_NO_LM = (MAIN.INKA_NO_L + MAIN.INKA_NO_M)                       " & vbNewLine _
                                              & "           AND E.IOZS_KB='11'                                                            " & vbNewLine _
                                              & "		   AND E.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                   " & vbNewLine _
                                              & "        ) AS BASE                                                                        " & vbNewLine _
                                              & "    WHERE BASE.NUM = 3),'')                                      AS IN_SAGYO_NM_3        " & vbNewLine _
                                              & " ----20160708 TimeOut対応                                  AS IN_SAGYO_NM_3        " & vbNewLine _
                                              & " , MAIN.OUT_MEISAI_LOT_NO               AS OUT_MEISAI_LOT_NO                      " & vbNewLine _
                                              & " , MAIN.OUTKA_TTL_NB_S                  AS OUTKA_TTL_NB_S                         " & vbNewLine _
                                              & " , MAIN.OUTKA_TTL_QT_S                  AS OUTKA_TTL_QT_S                         " & vbNewLine _
                                              & " , MAIN.OUT_MEISAI_IRIME                AS OUT_MEISAI_IRIME                       " & vbNewLine _
                                              & " , MAIN.OUT_MEISAI_IRIME_UT             AS OUT_MEISAI_IRIME_UT                    " & vbNewLine _
                                              & " , MAIN.IN_MEISAI_LOT_NO                AS IN_MEISAI_LOT_NO                       " & vbNewLine _
                                              & " , MAIN.KOSU                            AS KOSU                                   " & vbNewLine _
                                              & " , MAIN.SURYO                           AS SURYO                                  " & vbNewLine _
                                              & " , MAIN.IN_MEISAI_IRIME                 AS IN_MEISAI_IRIME                        " & vbNewLine _
                                              & " , MAIN.STD_IRIME_UT_02                 AS STD_IRIME_UT_02                        " & vbNewLine _
                                              & " , MAIN.OUTKA_NO_S                      AS OUTKA_NO_S                             " & vbNewLine _
                                              & " , MAIN.INKA_NO_S                       AS INKA_NO_S                              " & vbNewLine _
                                              & "--Notes No.952 2012/04/27 START                                                   " & vbNewLine _
                                              & " , MAIN.INKA_NO_S                       AS INKA_NO_S                              " & vbNewLine _
                                              & " , MAIN.TOU_NO                          AS TOU_NO                                 " & vbNewLine _
                                              & " , MAIN.SITU_NO                         AS SITU_NO                                " & vbNewLine _
                                              & " , MAIN.ZONE_CD                         AS ZONE_CD                                " & vbNewLine _
                                              & " , MAIN.LOCA                            AS LOCA                                   " & vbNewLine _
                                              & "--Notes No.952 2012/04/27  END                                                    " & vbNewLine _
                                              & " --(2012.12.05)要望番号1623 -- START --                                           " & vbNewLine _
                                              & " , MAX(MAIN.ALCTD_CAN_NB)               AS ALCTD_CAN_NB                           " & vbNewLine _
                                              & " --(2012.12.05)要望番号1623 --  END  --                                           " & vbNewLine _
                                              & " -- フィルメニッヒ入荷EDI対応                                                     " & vbNewLine _
                                              & " , MAX(ISNULL(MAIN.GOODS_COND_KB_3, ''))     AS GOODS_COND_KB_3                   " & vbNewLine _
                                              & " , MAX(ISNULL(MAIN.JOTAI_NM, ''))            AS JOTAI_NM                          " & vbNewLine _
                                              & " --ADD 2018/09/14 Start 依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更 " & vbNewLine _
                                              & " , MAX(MAIN.OUT_REMARK)                  AS OUT_REMARK                            " & vbNewLine _
                                              & " , MAX(MAIN.OUT_SERIAL_NO)               AS OUT_SERIAL_NO                         " & vbNewLine _
                                              & " , MAX(MAIN.OUT_REMARK_OUT)              AS OUT_REMARK_OUT                        " & vbNewLine _
                                              & " , MAX(MAIN.OUT_GOODS_COND_KB_1)         AS OUT_GOODS_COND_KB_1                   " & vbNewLine _
                                              & " , MAX(MAIN.OUT_GOODS_COND_KB_2)         AS OUT_GOODS_COND_KB_2                   " & vbNewLine _
                                              & " , MAX(MAIN.OUT_OFB_KB)                  AS OUT_OFB_KB                            " & vbNewLine _
                                              & " , MAX(MAIN.OUT_SPD_KB)                  AS OUT_SPD_KB                            " & vbNewLine _
                                              & " , MAX(MAIN.OUT_ZAI_REC_NO)              AS OUT_ZAI_REC_NO                        " & vbNewLine _
                                              & " , MAX(MAIN.IN_REMARK)                   AS IN_REMARK                             " & vbNewLine _
                                              & " , MAX(MAIN.IN_SERIAL_NO)                AS IN_SERIAL_NO                          " & vbNewLine _
                                              & " , MAX(MAIN.IN_REMARK_OUT)               AS IN_REMARK_OUT                         " & vbNewLine _
                                              & " , MAX(CASE WHEN MAIN.DATA_KBN = '1' THEN MAIN.IN_GOODS_COND_KB_1 ELSE ISNULL(MAIN.IN_GOODS_COND_KB_1, '') END) AS IN_GOODS_COND_KB_1 " & vbNewLine _
                                              & " , MAX(CASE WHEN MAIN.DATA_KBN = '1' THEN MAIN.IN_GOODS_COND_KB_2 ELSE ISNULL(MAIN.IN_GOODS_COND_KB_2, '') END) AS IN_GOODS_COND_KB_2 " & vbNewLine _
                                              & " , MAX(CASE WHEN MAIN.DATA_KBN = '1' THEN MAIN.IN_OFB_KB ELSE ISNULL(MAIN.IN_OFB_KB, '') END)                   AS IN_OFB_KB          " & vbNewLine _
                                              & " , MAX(CASE WHEN MAIN.DATA_KBN = '1' THEN MAIN.IN_SPD_KB ELSE ISNULL(MAIN.IN_SPD_KB, '') END)                   AS IN_SPD_KB          " & vbNewLine _
                                              & " , MAX(MAIN.DATA_KBN)                    AS DATA_KBN                              " & vbNewLine _
                                              & " --ADD 2018/09/14 End   依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更 " & vbNewLine _
                                              & " , MAX(MAIN.OUT_INKO_DATE)               AS OUT_INKO_DATE                         " & vbNewLine _
                                              & " FROM                                                                             " & vbNewLine _
                                              & "  (                                                                               " & vbNewLine

    ''' <summary>
    ''' 印刷データ取得SQL（共通メイン部）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAIN As String = " SELECT                                                                                  " & vbNewLine _
                                            & "   @RPT_ID                                                       AS RPT_ID               " & vbNewLine _
                                            & " , @NRS_BR_CD                                                    AS NRS_BR_CD            " & vbNewLine _
                                            & " , C01.FURI_NO                                                   AS FURI_NO              " & vbNewLine _
                                            & " , M01.NRS_BR_NM                                                 AS NRS_BR_NM            " & vbNewLine _
                                            & " , Z01_FURIKAE.KBN_NM1                                           AS FURIKAE_KBN          " & vbNewLine _
                                            & " , B01.INKA_DATE                                                 AS INKA_DATE            " & vbNewLine _
                                            & " , CASE WHEN @YOUKI_HENKO = '20' THEN '有'                                               " & vbNewLine _
                                            & "        WHEN @YOUKI_HENKO = '30' THEN '無'                                               " & vbNewLine _
                                            & "        ELSE '' END                                              AS YOUKI_HENKO          " & vbNewLine _
                                            & " , Z01_HOKAN.KBN_NM1                                             AS TOUKI_HOKAN_YN       " & vbNewLine _
                                            & " , M03.WH_NM                                                     AS WH_NM                " & vbNewLine _
                                            & " , C01.OUTKA_NO_L                                                AS OUTKA_NO_L           " & vbNewLine _
                                            & " , C02.OUTKA_NO_M                                                AS OUTKA_NO_M           " & vbNewLine _
                                            & " , OUT_M_CUST.CUST_CD_L                                          AS OUT_CUST_CD_L        " & vbNewLine _
                                            & " , OUT_M_CUST.CUST_CD_M                                          AS OUT_CUST_CD_M        " & vbNewLine _
                                            & " , OUT_M_CUST.CUST_NM_L                                          AS OUT_CUST_NM_L        " & vbNewLine _
                                            & " , OUT_M_CUST.CUST_NM_M                                          AS OUT_CUST_NM_M        " & vbNewLine _
                                            & " , OUT_M_GOODS.GOODS_NM_1                                        AS OUT_GOODS_NM_1       " & vbNewLine _
                                            & " --(2012.11.08)要望番号1546 -- START --                                                  " & vbNewLine _
                                            & " , OUT_M_GOODS.GOODS_CD_CUST                                     AS OUT_GOODS_CD_CUST    " & vbNewLine _
                                            & " --(2012.11.08)要望番号1546 --  END  --                                                  " & vbNewLine _
                                            & " , C02.LOT_NO                                                    AS LOT_NO               " & vbNewLine _
                                            & " , C02.SERIAL_NO                                                 AS SERIAL_NO            " & vbNewLine _
                                            & " , C01.CUST_ORD_NO                                               AS OUTKA_FROM_ORD_NO_L  " & vbNewLine _
                                            & " , Z01_OUT_NIYAKU.KBN_NM1                                        AS OUT_NIYAKU_YN        " & vbNewLine _
                                            & " , Z01_OUT_TAX.KBN_NM1                                           AS OUT_TAX_KB           " & vbNewLine _
                                            & " , C02.IRIME                                                     AS OUT_IRIME            " & vbNewLine _
                                            & " , C02.IRIME_UT                                                  AS IRIME_UT             " & vbNewLine _
                                            & " , C02.OUTKA_PKG_NB                                              AS OUTKA_M_PKG_NB       " & vbNewLine _
                                            & " , C02.OUTKA_HASU                                                AS OUTKA_HASU           " & vbNewLine _
                                            & " , OUT_M_GOODS.PKG_NB                                            AS PKG_NB               " & vbNewLine _
                                            & " , C02.OUTKA_TTL_NB                                              AS OUTKA_TTL_NB         " & vbNewLine _
                                            & " , C02.ALCTD_NB                                                  AS ALCTD_NB             " & vbNewLine _
                                            & " , C02.BACKLOG_NB                                                AS BACKLOG_NB           " & vbNewLine _
                                            & " , REPLACE(C01.REMARK,char(13) + char(10),'')                    AS OUTKA_ATT            " & vbNewLine _
                                            & " --, ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                            & " --   FROM                                                                                 " & vbNewLine _
                                            & " --       (SELECT                                                                          " & vbNewLine _
                                            & " --      SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                            & " --         (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                            & " --         AS  NUM                                                                        " & vbNewLine _
                                            & " --        FROM $LM_TRN$..E_SAGYO E                                                        " & vbNewLine _
                                            & " --        WHERE E.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                            & " --          AND E.INOUTKA_NO_LM = (C02.OUTKA_NO_L + C02.OUTKA_NO_M)                       " & vbNewLine _
                                            & "  --         AND E.IOZS_KB='21'                                                            " & vbNewLine _
                                            & "  --      ) AS BASE                                                                        " & vbNewLine _
                                            & " --   WHERE BASE.NUM = 1),'')                                      AS OUT_SAGYO_NM_1       " & vbNewLine _
                                            & " --, ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                            & " --   FROM                                                                                 " & vbNewLine _
                                            & " --       (SELECT                                                                          " & vbNewLine _
                                            & " --      SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                            & " --         (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                            & " --         AS  NUM                                                                        " & vbNewLine _
                                            & "  --       FROM $LM_TRN$..E_SAGYO E                                                        " & vbNewLine _
                                            & "  --       WHERE E.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                            & "  --         AND E.INOUTKA_NO_LM = (C02.OUTKA_NO_L + C02.OUTKA_NO_M)                       " & vbNewLine _
                                            & "  --         AND E.IOZS_KB='21'                                                            " & vbNewLine _
                                            & "  --      ) AS BASE                                                                        " & vbNewLine _
                                            & "  --  WHERE BASE.NUM = 2),'')                                      AS OUT_SAGYO_NM_2       " & vbNewLine _
                                            & " --, ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                            & "  --  FROM                                                                                 " & vbNewLine _
                                            & "  --      (SELECT                                                                          " & vbNewLine _
                                            & "  --     SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                            & "  --        (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                            & "  --        AS  NUM                                                                        " & vbNewLine _
                                            & "  --       FROM $LM_TRN$..E_SAGYO E                                                        " & vbNewLine _
                                            & "  --       WHERE E.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                            & "  --         AND E.INOUTKA_NO_LM = (C02.OUTKA_NO_L + C02.OUTKA_NO_M)                       " & vbNewLine _
                                            & "  --         AND E.IOZS_KB='21'                                                            " & vbNewLine _
                                            & "  --      ) AS BASE                                                                        " & vbNewLine _
                                            & "  --  WHERE BASE.NUM = 3),'')                                      AS OUT_SAGYO_NM_3       " & vbNewLine _
                                            & " , B01.INKA_NO_L                                                 AS INKA_NO_L            " & vbNewLine _
                                            & " , B02.INKA_NO_M                                                 AS INKA_NO_M            " & vbNewLine _
                                            & " , IN_M_CUST.CUST_CD_L                                           AS IN_CUST_CD_L         " & vbNewLine _
                                            & " , IN_M_CUST.CUST_CD_M                                           AS IN_CUST_CD_M         " & vbNewLine _
                                            & " , IN_M_CUST.CUST_NM_L                                           AS IN_CUST_NM_L         " & vbNewLine _
                                            & " , IN_M_CUST.CUST_NM_M                                           AS IN_CUST_NM_M         " & vbNewLine _
                                            & " , IN_M_GOODS.GOODS_NM_1                                         AS IN_GOODS_NM_1        " & vbNewLine _
                                            & " --(2012.11.08)要望番号1546 -- START --                                                  " & vbNewLine _
                                            & " , IN_M_GOODS.GOODS_CD_CUST                                      AS IN_GOODS_CD_CUST     " & vbNewLine _
                                            & " --(2012.11.08)要望番号1546 --  END  --                                                  " & vbNewLine _
                                            & " , B01.OUTKA_FROM_ORD_NO_L                                       AS DENP_NO              " & vbNewLine _
                                            & " , Z01_IN_NIYAKU.KBN_NM1                                         AS IN_NIYAKU_YN         " & vbNewLine _
                                            & " , Z01_IN_TAX.KBN_NM1                                            AS IN_TAX_KB            " & vbNewLine _
                                            & " , IN_M_GOODS.STD_IRIME_NB                                       AS IN_IRIME             " & vbNewLine _
                                            & " , IN_M_GOODS.STD_IRIME_UT                                       AS STD_IRIME_UT_01      " & vbNewLine _
                                            & " , REPLACE(B01.REMARK,char(13) + char(10),'')                    AS INKA_ATT             " & vbNewLine _
                                            & " --, ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                            & "  --  FROM                                                                                 " & vbNewLine _
                                            & " --       (SELECT                                                                          " & vbNewLine _
                                            & " --      SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                            & " --         (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                            & " --         AS  NUM                                                                        " & vbNewLine _
                                            & " --        FROM $LM_TRN$..E_SAGYO E                                                        " & vbNewLine _
                                            & " --        WHERE E.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                            & " --          AND E.INOUTKA_NO_LM = (B02.INKA_NO_L + B02.INKA_NO_M)                         " & vbNewLine _
                                            & " --          AND E.IOZS_KB='11'                                                            " & vbNewLine _
                                            & " --       ) AS BASE                                                                        " & vbNewLine _
                                            & " --   WHERE BASE.NUM = 1),'')                                      AS IN_SAGYO_NM_1        " & vbNewLine _
                                            & " --, ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                            & " --   FROM                                                                                 " & vbNewLine _
                                            & " --       (SELECT                                                                          " & vbNewLine _
                                            & " --      SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                            & " --         (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                            & " --         AS  NUM                                                                        " & vbNewLine _
                                            & " --        FROM $LM_TRN$..E_SAGYO E                                                        " & vbNewLine _
                                            & " --        WHERE E.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                            & " --          AND E.INOUTKA_NO_LM = (B02.INKA_NO_L + B02.INKA_NO_M)                         " & vbNewLine _
                                            & " --          AND E.IOZS_KB='11'                                                            " & vbNewLine _
                                            & " --       ) AS BASE                                                                        " & vbNewLine _
                                            & " --   WHERE BASE.NUM = 2),'')                                      AS IN_SAGYO_NM_2        " & vbNewLine _
                                            & " --, ISNULL((SELECT BASE.SAGYO_NM                                                          " & vbNewLine _
                                            & " --   FROM                                                                                 " & vbNewLine _
                                            & "  --      (SELECT                                                                          " & vbNewLine _
                                            & " --      SAGYO_NM AS SAGYO_NM,ROW_NUMBER() OVER                                            " & vbNewLine _
                                            & " --         (PARTITION BY INOUTKA_NO_LM ORDER BY INOUTKA_NO_LM)                            " & vbNewLine _
                                            & " --         AS  NUM                                                                        " & vbNewLine _
                                            & " --        FROM $LM_TRN$..E_SAGYO E                                                        " & vbNewLine _
                                            & " --        WHERE E.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                            & "  --         AND E.INOUTKA_NO_LM = (B02.INKA_NO_L + B02.INKA_NO_M)                         " & vbNewLine _
                                            & " --          AND E.IOZS_KB='11'                                                            " & vbNewLine _
                                            & " --       ) AS BASE                                                                        " & vbNewLine _
                                            & "  --  WHERE BASE.NUM = 3),'')                                      AS IN_SAGYO_NM_3        " & vbNewLine _
                                            & " -- フィルメニッヒ入荷EDI対応                                                            " & vbNewLine _
                                            & " , B03.GOODS_COND_KB_3 AS GOODS_COND_KB_3                                                " & vbNewLine _
                                            & " , CUSTCOND.JOTAI_NM AS JOTAI_NM                                                         " & vbNewLine


    ''' <summary>
    ''' 印刷データ取得SQL（出荷S情報）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SUB_OUT As String = " , C03.LOT_NO                                                    AS OUT_MEISAI_LOT_NO    " & vbNewLine _
                                               & " , C03.ALCTD_NB                                                  AS OUTKA_TTL_NB_S       " & vbNewLine _
                                               & " , C03.ALCTD_QT                                                  AS OUTKA_TTL_QT_S       " & vbNewLine _
                                               & " , C03.IRIME                                                     AS OUT_MEISAI_IRIME     " & vbNewLine _
                                               & " , C02.IRIME_UT                                                  AS OUT_MEISAI_IRIME_UT  " & vbNewLine _
                                               & " , null                                                          AS IN_MEISAI_LOT_NO     " & vbNewLine _
                                               & " , null                                                          AS KOSU                 " & vbNewLine _
                                               & " , null                                                          AS SURYO                " & vbNewLine _
                                               & " , null                                                          AS IN_MEISAI_IRIME      " & vbNewLine _
                                               & " , null                                                          AS STD_IRIME_UT_02      " & vbNewLine _
                                               & " , C03.OUTKA_NO_S                                                AS OUTKA_NO_S           " & vbNewLine _
                                               & " , null                                                          AS INKA_NO_S            " & vbNewLine _
                                               & " --Notes No.952 2012/04/27 START                                                         " & vbNewLine _
                                               & " , C03.TOU_NO                                                    AS TOU_NO               " & vbNewLine _
                                               & " , C03.SITU_NO                                                   AS SITU_NO              " & vbNewLine _
                                               & " , C03.ZONE_CD                                                   AS ZONE_CD              " & vbNewLine _
                                               & " , C03.LOCA                                                      AS LOCA                 " & vbNewLine _
                                               & " --Notes No.952 2012/04/27  END                                                          " & vbNewLine _
                                               & " --(2012.12.05)要望番号1623 -- START --                                                  " & vbNewLine _
                                               & " , C03.ALCTD_CAN_NB                                              AS ALCTD_CAN_NB         " & vbNewLine _
                                               & " --(2012.12.05)要望番号1623 --  END  --                                                  " & vbNewLine _
                                               & "--ADD 2018/09/14 Start 依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更         " & vbNewLine _
                                               & " ,C03.REMARK                                      AS OUT_REMARK           --備考小（社内）           " & vbNewLine _
                                               & " ,C03.SERIAL_NO                                   AS OUT_SERIAL_NO        --シリアルNo.              " & vbNewLine _
                                               & " ,isnull(D01.REMARK_OUT,'')                       AS OUT_REMARK_OUT       --備考小（社外）           " & vbNewLine _
                                               & " ,isnull(OKBNS005.KBN_NM1,'')                     AS OUT_GOODS_COND_KB_1  --商品状態区分1（中身）    " & vbNewLine _
                                               & " ,isnull(OKBNS006.KBN_NM1,'')                     AS OUT_GOODS_COND_KB_2  --商品状態区分2（外観）    " & vbNewLine _
                                               & " ,isnull(OKBNB002.KBN_NM1,'')                     AS OUT_OFB_KB           --簿外品区分               " & vbNewLine _
                                               & " ,isnull(OKBNH003.KBN_NM1,'')                     AS OUT_SPD_KB           --保留品区分               " & vbNewLine _
                                               & " ,C03.ZAI_REC_NO                                  AS OUT_ZAI_REC_NO       --在庫レコード番号         " & vbNewLine _
                                               & " ,null                                            AS IN_REMARK                           " & vbNewLine _
                                               & " ,null                                            AS IN_SERIAL_NO                        " & vbNewLine _
                                               & " ,null                                            AS IN_REMARK_OUT                       " & vbNewLine _
                                               & " ,null                                            AS IN_GOODS_COND_KB_1                  " & vbNewLine _
                                               & " ,null                                            AS IN_GOODS_COND_KB_2                  " & vbNewLine _
                                               & " ,null                                            AS IN_OFB_KB                           " & vbNewLine _
                                               & " ,null                                            AS IN_SPD_KB                           " & vbNewLine _
                                               & " ,'1'                                             AS DATA_KBN                            " & vbNewLine _
                                               & " ,isnull(D01.INKO_DATE,'')                        AS OUT_INKO_DATE                       " & vbNewLine _
                                               & "--ADD 2018/09/14 End   依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更         " & vbNewLine



    ''' <summary>
    ''' 印刷データ取得SQL（共通FROM）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = " FROM                                                                                    " & vbNewLine _
                                            & " --出荷L                                                                                 " & vbNewLine _
                                            & "  $LM_TRN$..C_OUTKA_L C01                                                                " & vbNewLine _
                                            & " --出荷M                                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & " (                                                                                       " & vbNewLine _
                                            & "     SELECT    TOP 1                                                                          " & vbNewLine _
                                            & "       OM.OUTKA_NO_L            AS OUTKA_NO_L                                            " & vbNewLine _
                                            & "     , MAX(OUTKA_NO_M)       AS OUTKA_NO_M                                               " & vbNewLine _
                                            & "     , MAX(SERIAL_NO)        AS SERIAL_NO                                                " & vbNewLine _
                                            & "     , MAX(IRIME)            AS IRIME                                                    " & vbNewLine _
                                            & "     , MAX(IRIME_UT)         AS IRIME_UT                                                 " & vbNewLine _
                                            & "     , MAX(OM.OUTKA_PKG_NB)     AS OUTKA_PKG_NB                                          " & vbNewLine _
                                            & "     , MAX(OUTKA_HASU)       AS OUTKA_HASU                                               " & vbNewLine _
                                            & "     , MAX(OUTKA_TTL_NB)     AS OUTKA_TTL_NB                                             " & vbNewLine _
                                            & "     , MAX(ALCTD_NB)         AS ALCTD_NB                                                 " & vbNewLine _
                                            & "     , MAX(BACKLOG_NB)       AS BACKLOG_NB                                               " & vbNewLine _
                                            & "     , MAX(GOODS_CD_NRS)     AS GOODS_CD_NRS                                    " & vbNewLine _
                                            & "     , MAX(LOT_NO)           AS LOT_NO                                          " & vbNewLine _
                                            & "	 --,OM.NRS_BR_CD                                                        " & vbNewLine _
                                            & "     FROM                                                                       " & vbNewLine _
                                            & "      $LM_TRN$..C_OUTKA_M  OM                                                   " & vbNewLine _
                                            & "	  LEFT JOIN $LM_TRN$..C_OUTKA_L   OL                                    " & vbNewLine _
                                            & "	  ON   OM.NRS_BR_CD = OL.NRS_BR_CD                                      " & vbNewLine _
                                            & "	  AND OM.OUTKA_NO_L = OL.OUTKA_NO_L                                     " & vbNewLine _
                                            & "	  AND OM.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                            & "     WHERE                                                                      " & vbNewLine _
                                            & "      OL.NRS_BR_CD   = @NRS_BR_CD                                               " & vbNewLine _
                                            & "     AND                                                                        " & vbNewLine _
                                            & "      OL.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                                            & "	  --当日のみに限定                                                      " & vbNewLine _
                                            & "	  AND OL.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                  " & vbNewLine _
                                            & "	  --振替No                                                              " & vbNewLine _
                                            & "	  AND OL.FURI_NO = @FURI_NO                                             " & vbNewLine _
                                            & "     GROUP BY                                                                   " & vbNewLine _
                                            & "      OM.OUTKA_NO_L                                                             " & vbNewLine _
                                            & "	   ,OM.NRS_BR_CD                                                        " & vbNewLine _
                                            & "	                                                                        " & vbNewLine _
                                            & " ) C02                                                                          " & vbNewLine _
                                            & " ON                                                                             " & vbNewLine _
                                            & "  C01.OUTKA_NO_L  = C02.OUTKA_NO_L                                              " & vbNewLine _
                                            & " -- AND C01.NRS_BR_CD  = C02.NRS_BR_CD                                          " & vbNewLine _
                                            & " --20160708  TIMEOUT対応                                                        " & vbNewLine _
                                            & " --出荷S                                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_TRN$..C_OUTKA_S C03                                                                " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  C03.NRS_BR_CD   = @NRS_BR_CD                                                           " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C02.OUTKA_NO_L  = C03.OUTKA_NO_L                                                       " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C02.OUTKA_NO_M  = C03.OUTKA_NO_M                                                       " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C03.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                            & " --入荷L                                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_TRN$..B_INKA_L B01                                                                 " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  B01.NRS_BR_CD   = @NRS_BR_CD                                                           " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  B01.FURI_NO     = @FURI_NO                                                             " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  B01.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                            & " --入荷M                                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & " (                                                                                       " & vbNewLine _
                                            & "     SELECT        TOP 1                                                                 " & vbNewLine _
                                            & "       IM.INKA_NO_L           AS INKA_NO_L                                               " & vbNewLine _
                                            & "     , MAX(INKA_NO_M)      AS INKA_NO_M                                                  " & vbNewLine _
                                            & "     , MAX(GOODS_CD_NRS)   AS GOODS_CD_NRS                                               " & vbNewLine _
                                            & "     FROM                                                                                " & vbNewLine _
                                            & "      $LM_TRN$..B_INKA_M   IM                                                            " & vbNewLine _
                                            & "	  LEFT JOIN $LM_TRN$..B_INKA_L IL                                                " & vbNewLine _
                                            & "	 ON IM.NRS_BR_CD = IL.NRS_BR_CD                                                  " & vbNewLine _
                                            & "	 AND IM.INKA_NO_L = IL.INKA_NO_L                                                 " & vbNewLine _
                                            & "	 AND IM.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
                                            & "     WHERE                                                                               " & vbNewLine _
                                            & "      IM.NRS_BR_CD   = @NRS_BR_CD                                                        " & vbNewLine _
                                            & "     AND                                                                                 " & vbNewLine _
                                            & "      IM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                            & "	  --20160708                                                                     " & vbNewLine _
                                            & "	  --当日のみに限定                                                               " & vbNewLine _
                                            & "	  AND IM.SYS_ENT_DATE = @FURIKAEBI  --UPD 20181221 003904  CONVERT(VARCHAR,GETDATE(),112)                           " & vbNewLine _
                                            & "	  --振替No                                                                       " & vbNewLine _
                                            & "	  AND IL.FURI_NO = @FURI_NO                                                      " & vbNewLine _
                                            & "     GROUP BY                                                                            " & vbNewLine _
                                            & "      IM.INKA_NO_L                                                                       " & vbNewLine _
                                            & " ) B02                                                                                   " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  B01.INKA_NO_L   = B02.INKA_NO_L                                                        " & vbNewLine _
                                            & " --入荷S                                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_TRN$..B_INKA_S B03                                                                 " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  B03.NRS_BR_CD   = @NRS_BR_CD                                                           " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  B02.INKA_NO_L   = B03.INKA_NO_L                                                        " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  B02.INKA_NO_M   = B03.INKA_NO_M                                                        " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  B03.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                            & " --営業所M                                                                               " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..M_NRS_BR M01                                                                 " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  M01.NRS_BR_CD   = @NRS_BR_CD                                                           " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  M01.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                            & " --倉庫M                                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..M_SOKO M03                                                                   " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  M03.NRS_BR_CD   = @NRS_BR_CD                                                           " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C01.WH_CD       = M03.WH_CD                                                            " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  M03.SYS_DEL_FLG = '0'                                                                  " & vbNewLine _
                                            & " --区分M（保管料有無）                                                                   " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN Z01_HOKAN                                                              " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  Z01_HOKAN.KBN_GROUP_CD = 'H009'                                                        " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_HOKAN.KBN_CD       =                                                               " & vbNewLine _
                                            & "  CASE WHEN (B01.TOUKI_HOKAN_YN ='00') THEN '10'                                   " & vbNewLine _
                                            & "  WHEN (B01.TOUKI_HOKAN_YN ='01' AND C01.TOUKI_HOKAN_YN ='00')              " & vbNewLine _
                                            & "  THEN '20'                                                                              " & vbNewLine _
                                            & "  WHEN (B01.TOUKI_HOKAN_YN ='01' AND C01.TOUKI_HOKAN_YN ='01')              " & vbNewLine _
                                            & "  THEN '30'                                                                              " & vbNewLine _
                                            & "  END                                                                                    " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_HOKAN.SYS_DEL_FLG  = '0'                                                           " & vbNewLine _
                                            & " --区分M（振替区分）                                                                     " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN Z01_FURIKAE                                                            " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  Z01_FURIKAE.KBN_GROUP_CD = 'H004'                                                      " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_FURIKAE.KBN_CD       = @FURIKAE_KBN                                                " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_FURIKAE.SYS_DEL_FLG  = '0'                                                         " & vbNewLine _
                                            & " --区分M（荷役料有無＿出荷）                                                             " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN Z01_OUT_NIYAKU                                                         " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  Z01_OUT_NIYAKU.KBN_GROUP_CD = 'U009'                                                   " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_OUT_NIYAKU.KBN_CD       = C01.NIYAKU_YN                                            " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_OUT_NIYAKU.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " --区分M（税区分＿出荷）                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN Z01_OUT_TAX                                                            " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  Z01_OUT_TAX.KBN_GROUP_CD = 'Z001'                                                      " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_OUT_TAX.KBN_CD       = @OUT_TAX_KB                                                 " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_OUT_TAX.SYS_DEL_FLG  = '0'                                                         " & vbNewLine _
                                            & " --区分M（荷役料有無＿入荷                                                               " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN Z01_IN_NIYAKU                                                          " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  Z01_IN_NIYAKU.KBN_GROUP_CD = 'U009'                                                    " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_IN_NIYAKU.KBN_CD       = B01.NIYAKU_YN                                             " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_IN_NIYAKU.SYS_DEL_FLG  = '0'                                                       " & vbNewLine _
                                            & " --区分M（税区分＿入荷）                                                                 " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN Z01_IN_TAX                                                             " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  Z01_IN_TAX.KBN_GROUP_CD = 'Z001'                                                       " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_IN_TAX.KBN_CD       = B01.TAX_KB                                                   " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  Z01_IN_TAX.SYS_DEL_FLG  = '0'                                                          " & vbNewLine _
                                            & " --商品M（出荷）                                                                         " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..M_GOODS OUT_M_GOODS                                                          " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  OUT_M_GOODS.NRS_BR_CD   = @NRS_BR_CD                                                   " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C02.GOODS_CD_NRS        = OUT_M_GOODS.GOODS_CD_NRS                                     " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  OUT_M_GOODS.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
                                            & " --商品M（入荷）                                                                         " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..M_GOODS IN_M_GOODS                                                           " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  IN_M_GOODS.NRS_BR_CD   = @NRS_BR_CD                                                    " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  B02.GOODS_CD_NRS       = IN_M_GOODS.GOODS_CD_NRS                                       " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  IN_M_GOODS.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
                                            & " --荷主M（出荷）                                                                         " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..M_CUST OUT_M_CUST                                                            " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  OUT_M_CUST.NRS_BR_CD          = @NRS_BR_CD                                             " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  OUT_M_GOODS.CUST_CD_L         = OUT_M_CUST.CUST_CD_L                                   " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  OUT_M_GOODS.CUST_CD_M         = OUT_M_CUST.CUST_CD_M                                   " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  OUT_M_GOODS.CUST_CD_S         = OUT_M_CUST.CUST_CD_S                                   " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  OUT_M_GOODS.CUST_CD_SS        = OUT_M_CUST.CUST_CD_SS                                  " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  OUT_M_CUST.SYS_DEL_FLG        = '0'                                                    " & vbNewLine _
                                            & " --荷主M（入荷）                                                                         " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..M_CUST IN_M_CUST                                                             " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "  IN_M_CUST.NRS_BR_CD         = @NRS_BR_CD                                               " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  IN_M_GOODS.CUST_CD_L        = IN_M_CUST.CUST_CD_L                                      " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  IN_M_GOODS.CUST_CD_M        = IN_M_CUST.CUST_CD_M                                      " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  IN_M_GOODS.CUST_CD_S        = IN_M_CUST.CUST_CD_S                                      " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  IN_M_GOODS.CUST_CD_SS       = IN_M_CUST.CUST_CD_SS                                     " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  IN_M_CUST.SYS_DEL_FLG       = '0'                                                      " & vbNewLine _
                                            & " -- フィルメニッヒ入荷EDI対応                                                            " & vbNewLine _
                                            & "  LEFT JOIN                                                                              " & vbNewLine _
                                            & "     $LM_MST$..M_CUSTCOND AS CUSTCOND                                                      " & vbNewLine _
                                            & " ON                                                                                      " & vbNewLine _
                                            & "      CUSTCOND.NRS_BR_CD = IN_M_CUST.NRS_BR_CD                                           " & vbNewLine _
                                            & "  AND CUSTCOND.CUST_CD_L = IN_M_CUST.CUST_CD_L                                           " & vbNewLine _
                                            & "  AND CUSTCOND.JOTAI_CD = B03.GOODS_COND_KB_3                                            " & vbNewLine _
                                            & "  AND CUSTCOND.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                            & " --20160708  TIMEOUT対応                                                        " & vbNewLine _
                                            & "--ADD 2018/09/14 Start    依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更     " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBNS005        --振替先　状態(中身)                                            " & vbNewLine _
                                            & "    ON   KBNS005.KBN_GROUP_CD = 'S005'                                                   " & vbNewLine _
                                            & "   AND   KBNS005.KBN_CD       = B03.GOODS_COND_KB_1                                      " & vbNewLine _
                                            & "   AND   KBNS005.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBNS006        --振替先　状態(外観)                                    " & vbNewLine _
                                            & "    ON   KBNS006.KBN_GROUP_CD = 'S006'                                                   " & vbNewLine _
                                            & "   AND   KBNS006.KBN_CD       = B03.GOODS_COND_KB_2                                      " & vbNewLine _
                                            & "   AND   KBNS006.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBNB002        --振替先　簿外品       　                               " & vbNewLine _
                                            & "    ON   KBNB002.KBN_GROUP_CD = 'B002'                                                   " & vbNewLine _
                                            & "   AND   KBNB002.KBN_CD       = B03.OFB_KB                                               " & vbNewLine _
                                            & "   AND   KBNB002.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN KBNH003        --振替先　保留品                                        " & vbNewLine _
                                            & "    ON   KBNH003.KBN_GROUP_CD = 'H003'                                                   " & vbNewLine _
                                            & "   AND   KBNH003.KBN_CD       = B03.SPD_KB                                               " & vbNewLine _
                                            & "   AND   KBNH003.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " --在庫データ(出荷元用)                                                                  " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_TRN$..D_ZAI_TRS D01                                                                " & vbNewLine _
                                            & "    ON D01.NRS_BR_CD   = @NRS_BR_CD                                                      " & vbNewLine _
                                            & "   AND D01.ZAI_REC_NO  = C03.ZAI_REC_NO                                                  " & vbNewLine _
                                            & "   AND D01.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN OKBNS005        --振替元　状態(中身)                                    " & vbNewLine _
                                            & "    ON   OKBNS005.KBN_GROUP_CD = 'S005'                                                   " & vbNewLine _
                                            & "   AND   OKBNS005.KBN_CD       = D01.GOODS_COND_KB_1                                      " & vbNewLine _
                                            & "   AND   OKBNS005.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN OKBNS006        --振替元　状態(外観)                                    " & vbNewLine _
                                            & "    ON   OKBNS006.KBN_GROUP_CD = 'S006'                                                   " & vbNewLine _
                                            & "   AND   OKBNS006.KBN_CD       = D01.GOODS_COND_KB_2                                      " & vbNewLine _
                                            & "   AND   OKBNS006.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN OKBNB002        --振替元　簿外品       　                               " & vbNewLine _
                                            & "    ON   OKBNB002.KBN_GROUP_CD = 'B002'                                                   " & vbNewLine _
                                            & "   AND   OKBNB002.KBN_CD       = D01.OFB_KB                                               " & vbNewLine _
                                            & "   AND   OKBNB002.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_MST$..Z_KBN OKBNH003        --振替元　保留品                                        " & vbNewLine _
                                            & "    ON   OKBNH003.KBN_GROUP_CD = 'H003'                                                   " & vbNewLine _
                                            & "   AND   OKBNH003.KBN_CD       = D01.SPD_KB                                               " & vbNewLine _
                                            & "   AND   OKBNH003.SYS_DEL_FLG  = '0'                                                      " & vbNewLine _
                                            & "--ADD 2018/09/14 End       依頼番号 : 001996  【LMS】在庫振替画面_振替伝票_項目変更      " & vbNewLine _
                                            & " --在庫データ(入荷先用)                                                                  " & vbNewLine _
                                            & " LEFT JOIN                                                                               " & vbNewLine _
                                            & "  $LM_TRN$..D_ZAI_TRS D02                                                                " & vbNewLine _
                                            & "    ON D02.NRS_BR_CD   = @NRS_BR_CD                                                      " & vbNewLine _
                                            & "   AND D02.ZAI_REC_NO  = B03.ZAI_REC_NO                                                  " & vbNewLine _
                                            & "   AND D02.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
                                            & " WHERE                                                                                   " & vbNewLine _
                                            & "  C01.NRS_BR_CD   = @NRS_BR_CD                                                           " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C01.FURI_NO     = @FURI_NO                                                             " & vbNewLine _
                                            & " AND                                                                                     " & vbNewLine _
                                            & "  C01.SYS_DEL_FLG = '0'                                                                  " & vbNewLine



    ''' <summary>
    ''' 印刷データ取得SQL（UNION）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNION As String = "  UNION   " & vbNewLine

#If False Then   'UPD 2018/09/17 依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更  
       ''' <summary>
    ''' 印刷データ取得SQL（入荷S情報）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SUB_IN As String = " , null                                                          AS OUT_MEISAI_LOT_NO    " & vbNewLine _
                                              & " , null                                                          AS OUTKA_TTL_NB_S       " & vbNewLine _
                                              & " , null                                                          AS OUTKA_TTL_QT_S       " & vbNewLine _
                                              & " , null                                                          AS OUT_MEISAI_IRIME     " & vbNewLine _
                                              & " , null                                                          AS OUT_MEISAI_IRIME_UT  " & vbNewLine _
                                              & " , B03.LOT_NO                                                    AS IN_MEISAI_LOT_NO     " & vbNewLine _
                                              & " , B03.KONSU * IN_M_GOODS.PKG_NB + B03.HASU                      AS KOSU                 " & vbNewLine _
                                              & " , ( B03.KONSU * IN_M_GOODS.PKG_NB + B03.HASU ) * B03.IRIME      AS SURYO                " & vbNewLine _
                                              & " , B03.IRIME                                                     AS IN_MEISAI_IRIME      " & vbNewLine _
                                              & " , IN_M_GOODS.STD_IRIME_UT                                       AS STD_IRIME_UT_02      " & vbNewLine _
                                              & " , null                                                          AS OUTKA_NO_S           " & vbNewLine _
                                              & " , B03.INKA_NO_S                                                 AS INKA_NO_S            " & vbNewLine _
                                              & " --Notes No.952 2012/04/27 START                                                         " & vbNewLine _
                                              & " , null                                                          AS TOU_NO               " & vbNewLine _
                                              & " , null                                                          AS SITU_NO              " & vbNewLine _
                                              & " , null                                                          AS ZONE_CD              " & vbNewLine _
                                              & " , null                                                          AS LOCA                 " & vbNewLine _
                                              & " --Notes No.952 2012/04/27  END                                                          " & vbNewLine _
                                              & " --(2012.12.05)要望番号1623 -- START --                                                  " & vbNewLine _
                                              & " , null                                                          AS ALCTD_CAN_NB         " & vbNewLine _
                                              & " --(2012.12.05)要望番号1623 --  END  --                                                  " & vbNewLine _

#Else
    ''' <summary>
    ''' 印刷データ取得SQL（入荷S情報）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SUB_IN As String = " , null                                                          AS OUT_MEISAI_LOT_NO    " & vbNewLine _
                                              & " , null                                                          AS OUTKA_TTL_NB_S       " & vbNewLine _
                                              & " , null                                                          AS OUTKA_TTL_QT_S       " & vbNewLine _
                                              & " , null                                                          AS OUT_MEISAI_IRIME     " & vbNewLine _
                                              & " , null                                                          AS OUT_MEISAI_IRIME_UT  " & vbNewLine _
                                              & " , B03.LOT_NO                                                    AS IN_MEISAI_LOT_NO     " & vbNewLine _
                                              & " , B03.KONSU * IN_M_GOODS.PKG_NB + B03.HASU                      AS KOSU                 " & vbNewLine _
                                              & " , ( B03.KONSU * IN_M_GOODS.PKG_NB + B03.HASU ) * B03.IRIME      AS SURYO                " & vbNewLine _
                                              & " , B03.IRIME                                                     AS IN_MEISAI_IRIME      " & vbNewLine _
                                              & " , IN_M_GOODS.STD_IRIME_UT                                       AS STD_IRIME_UT_02      " & vbNewLine _
                                              & " , null                                                          AS OUTKA_NO_S           " & vbNewLine _
                                              & " , B03.INKA_NO_S                                                 AS INKA_NO_S            " & vbNewLine _
                                              & " --Notes No.952 2012/04/27 START                                                         " & vbNewLine _
                                              & " , B03.TOU_NO                                                    AS TOU_NO               " & vbNewLine _
                                              & " , B03.SITU_NO                                                   AS SITU_NO              " & vbNewLine _
                                              & " , B03.ZONE_CD                                                   AS ZONE_CD              " & vbNewLine _
                                              & " , B03.LOCA                                                      AS LOCA                 " & vbNewLine _
                                              & " --Notes No.952 2012/04/27  END                                                          " & vbNewLine _
                                              & " --(2012.12.05)要望番号1623 -- START --                                                  " & vbNewLine _
                                              & " , null                                                          AS ALCTD_CAN_NB         " & vbNewLine _
                                              & " --(2012.12.05)要望番号1623 --  END  --                                                  " & vbNewLine _
                                              & "--ADD 2018/09/14 Start 依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更         " & vbNewLine _
                                              & " ,null                                            AS OUT_REMARK                          " & vbNewLine _
                                              & " ,null                                            AS OUT_SERIAL_NO                       " & vbNewLine _
                                              & " ,null                                            AS OUTREMARK_OUT                       " & vbNewLine _
                                              & " ,null                                            AS OUT_GOODS_COND_KB_1                 " & vbNewLine _
                                              & " ,null                                            AS OUT_GOODS_COND_KB_2                 " & vbNewLine _
                                              & " ,null                                            AS OUT_OFB_KB                          " & vbNewLine _
                                              & " ,null                                            AS OUT_SPD_KB                          " & vbNewLine _
                                              & " ,NULL                                            AS OUT_ZAI_REC_NO                      " & vbNewLine _
                                              & " ,B03.REMARK                                      AS IN_REMARK             --備考小（社内）          " & vbNewLine _
                                              & " ,B03.SERIAL_NO                                   AS IN_SERIAL_NO          --シリアルNo.             " & vbNewLine _
                                              & " ,B03.REMARK_OUT                                  AS IN_REMARK_OUT         --備考小（社外）          " & vbNewLine _
                                              & " ,KBNS005.KBN_NM1                                 AS IN_GOODS_COND_KB_1    --商品状態区分1（中身）   " & vbNewLine _
                                              & " ,KBNS006.KBN_NM1                                 AS IN_GOODS_COND_KB_2    --商品状態区分2（外観）   " & vbNewLine _
                                              & " ,KBNB002.KBN_NM1                                 AS IN_OFB_KB             --簿外品区分              " & vbNewLine _
                                              & " ,KBNH003.KBN_NM1                                 AS IN_SPD_KB             --保留品区分              " & vbNewLine _
                                              & " ,'2'                                             AS DATA_KBN                            " & vbNewLine _
                                              & " ,isnull(D02.INKO_DATE,'')                        AS OUT_INKO_DATE                       " & vbNewLine _
                                              & "--ADD 2018/09/14 End   依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更         " & vbNewLine

#End If

    ''' <summary>
    ''' 印刷データ取得SQL（ラスト）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LAST As String = " ) MAIN                                                                  " & vbNewLine _
                                                & " GROUP BY                                                            " & vbNewLine _
                                                & "   MAIN.OUTKA_NO_L                                                   " & vbNewLine _
                                                & " , MAIN.INKA_NO_L                                                    " & vbNewLine _
                                                & " , MAIN.OUT_MEISAI_LOT_NO                                            " & vbNewLine _
                                                & " , MAIN.OUTKA_TTL_NB_S                                               " & vbNewLine _
                                                & " , MAIN.OUTKA_TTL_QT_S                                               " & vbNewLine _
                                                & " , MAIN.OUT_MEISAI_IRIME                                             " & vbNewLine _
                                                & " , MAIN.OUT_MEISAI_IRIME_UT                                          " & vbNewLine _
                                                & " , MAIN.IN_MEISAI_LOT_NO                                             " & vbNewLine _
                                                & " , MAIN.KOSU                                                         " & vbNewLine _
                                                & " , MAIN.SURYO                                                        " & vbNewLine _
                                                & " , MAIN.IN_MEISAI_IRIME                                              " & vbNewLine _
                                                & " , MAIN.STD_IRIME_UT_02                                              " & vbNewLine _
                                                & " , MAIN.OUTKA_NO_S                                                   " & vbNewLine _
                                                & " , MAIN.INKA_NO_S                                                    " & vbNewLine _
                                                & " --Notes No.952 2012/04/27 START                                     " & vbNewLine _
                                                & " , MAIN.TOU_NO                                                       " & vbNewLine _
                                                & " , MAIN.SITU_NO                                                      " & vbNewLine _
                                                & " , MAIN.ZONE_CD                                                      " & vbNewLine _
                                                & " , MAIN.LOCA                                                         " & vbNewLine _
                                                & " --Notes No.952 2012/04/27 E N D                                     " & vbNewLine _
                                                & " --(2012.12.05)要望番号1623 -- START --                              " & vbNewLine _
                                                & " , MAIN.ALCTD_CAN_NB                                                 " & vbNewLine _
                                                & " --(2012.12.05)要望番号1623 --  END  --                              " & vbNewLine _
                                                & " -- フィルメニッヒ入荷EDI対応                                        " & vbNewLine _
                                                & " --, MAIN.GOODS_COND_KB_3                                              " & vbNewLine _
                                                & " --, MAIN.JOTAI_NM                                                     " & vbNewLine _
                                                & "  --20160708                                                          " & vbNewLine _
                                                & " ,MAIN.INKA_NO_M                                                     " & vbNewLine _
                                                & " ,MAIN.OUTKA_NO_M                                                     " & vbNewLine _
                                                & "  --20160708                                                         " & vbNewLine _
                                                & " ORDER BY                                                            " & vbNewLine _
                                                & "   MAIN.OUTKA_NO_L                                                   " & vbNewLine _
                                                & " , CASE WHEN MAIN.OUT_MEISAI_LOT_NO is null then 0  else 1 end desc  " & vbNewLine _
                                                & " , MAIN.OUT_MEISAI_LOT_NO                                            " & vbNewLine _
                                                & " , MAIN.INKA_NO_L                                                    " & vbNewLine _
                                                & " , MAIN.IN_MEISAI_LOT_NO                                             " & vbNewLine _
                                                & " , MAIN.OUTKA_NO_S                                                   " & vbNewLine _
                                                & " , MAIN.INKA_NO_S                                                    " & vbNewLine _
                                                & " --Notes No.952 2012/04/27 START                                     " & vbNewLine _
                                                & " , MAIN.TOU_NO                                                       " & vbNewLine _
                                                & " , MAIN.SITU_NO                                                      " & vbNewLine _
                                                & " , MAIN.ZONE_CD                                                      " & vbNewLine _
                                                & " , MAIN.LOCA                                                         " & vbNewLine _
                                                & " --Notes No.952 2012/04/27  END                                      " & vbNewLine


#If True Then   'ADD 2018/12/25 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい
    Private Const SQL_SELECT_FURIKAEBI As String = "SELECT                                                                 " & vbNewLine _
                                            & "  SYS_ENT_DATE       AS FURIKAEBI                                           " & vbNewLine _
                                            & "FROM $LM_TRN$..B_INKA_L                                                     " & vbNewLine _
                                            & "WHERE FURI_NO = @FURI_NO                                                    " & vbNewLine

#End If


#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "PTN_ID" _
                                            , "PTN_CD" _
                                            , "RPT_ID"
                                            }

        Return Me.SelectListData(ds, LMD600DAC.TABLE_NM_M_RPT, LMD600DAC.SQL_SELECT_MPrt, LMD600DAC.SelectCondition.PTN1, str)

    End Function

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrtData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"RPT_ID" _
                                            , "NRS_BR_CD" _
                                            , "FURI_NO" _
                                            , "NRS_BR_NM" _
                                            , "FURIKAE_KBN" _
                                            , "INKA_DATE" _
                                            , "YOUKI_HENKO" _
                                            , "TOUKI_HOKAN_YN" _
                                            , "WH_NM" _
                                            , "OUTKA_NO_L" _
                                            , "OUT_CUST_NM_L" _
                                            , "OUT_CUST_NM_M" _
                                            , "OUT_GOODS_NM_1" _
                                            , "OUT_GOODS_CD_CUST" _
                                            , "LOT_NO" _
                                            , "SERIAL_NO" _
                                            , "OUTKA_FROM_ORD_NO_L" _
                                            , "OUT_NIYAKU_YN" _
                                            , "OUT_TAX_KB" _
                                            , "OUT_IRIME" _
                                            , "IRIME_UT" _
                                            , "OUTKA_M_PKG_NB" _
                                            , "OUTKA_HASU" _
                                            , "PKG_NB" _
                                            , "OUTKA_TTL_NB" _
                                            , "ALCTD_NB" _
                                            , "BACKLOG_NB" _
                                            , "OUTKA_ATT" _
                                            , "OUT_SAGYO_NM_1" _
                                            , "OUT_SAGYO_NM_2" _
                                            , "OUT_SAGYO_NM_3" _
                                            , "INKA_NO_L" _
                                            , "IN_CUST_NM_L" _
                                            , "IN_CUST_NM_M" _
                                            , "IN_GOODS_NM_1" _
                                            , "IN_GOODS_CD_CUST" _
                                            , "DENP_NO" _
                                            , "IN_NIYAKU_YN" _
                                            , "IN_TAX_KB" _
                                            , "IN_IRIME" _
                                            , "STD_IRIME_UT_01" _
                                            , "INKA_ATT" _
                                            , "IN_SAGYO_NM_1" _
                                            , "IN_SAGYO_NM_2" _
                                            , "IN_SAGYO_NM_3" _
                                            , "OUT_MEISAI_LOT_NO" _
                                            , "OUTKA_TTL_NB_S" _
                                            , "OUTKA_TTL_QT_S" _
                                            , "OUT_MEISAI_IRIME" _
                                            , "OUT_MEISAI_IRIME_UT" _
                                            , "IN_MEISAI_LOT_NO" _
                                            , "KOSU" _
                                            , "SURYO" _
                                            , "IN_MEISAI_IRIME" _
                                            , "STD_IRIME_UT_02" _
                                            , "OUTKA_NO_S" _
                                            , "INKA_NO_S" _
                                            , "TOU_NO" _
                                            , "SITU_NO" _
                                            , "ZONE_CD" _
                                            , "LOCA" _
                                            , "ALCTD_CAN_NB" _
                                            , "OUT_CUST_CD_L" _
                                            , "OUT_CUST_CD_M" _
                                            , "IN_CUST_CD_L" _
                                            , "IN_CUST_CD_M" _
                                            , "GOODS_COND_KB_3" _
                                            , "JOTAI_NM" _
                                            , "OUT_REMARK" _
                                            , "OUT_SERIAL_NO" _
                                            , "OUT_REMARK_OUT" _
                                            , "OUT_GOODS_COND_KB_1" _
                                            , "OUT_GOODS_COND_KB_2" _
                                            , "OUT_OFB_KB" _
                                            , "OUT_SPD_KB" _
                                            , "OUT_ZAI_REC_NO" _
                                            , "IN_REMARK" _
                                            , "IN_SERIAL_NO" _
                                            , "IN_REMARK_OUT" _
                                            , "IN_GOODS_COND_KB_1" _
                                            , "IN_GOODS_COND_KB_2" _
                                            , "IN_OFB_KB" _
                                            , "IN_SPD_KB" _
                                            , "DATA_KBN" _
                                            , "OUT_INKO_DATE"
}

        '印刷データ検索SQL作成
        Dim sql As StringBuilder = New StringBuilder()
        sql.Append(LMD600DAC.SQL_SELECT_FIRST)
        sql.Append(LMD600DAC.SQL_SELECT_MAIN)
        sql.Append(LMD600DAC.SQL_SELECT_SUB_OUT)
        sql.Append(LMD600DAC.SQL_SELECT_FROM)
        sql.Append(LMD600DAC.SQL_SELECT_UNION)
        sql.Append(LMD600DAC.SQL_SELECT_MAIN)
        sql.Append(LMD600DAC.SQL_SELECT_SUB_IN)
        sql.Append(LMD600DAC.SQL_SELECT_FROM)
        sql.Append(LMD600DAC.SQL_SELECT_LAST)

        ds = Me.SelectListData(ds, LMD600DAC.TABLE_NM_OUT, sql.ToString(), LMD600DAC.SelectCondition.PTN2, str)

#If False Then  'UPD 2018/09/20 依頼番号 : 001996   【LMS】在庫振替画面_振替伝票_項目変更
        Return Me.SetDataSet(ds)
#Else
        Return ds
#End If



    End Function

#If True Then   'ADD 2018/12/25 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい

    ''' <summary>
    ''' 振替No.より振替日取得    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替No.より振替日取得SQLの構築・発行</remarks>
    Private Function SelecFurikaebiGet(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD600IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim sql As String

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        sql = LMD600DAC.SQL_SELECT_FURIKAEBI      'SQL構築(データ抽出用Select句)

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        _SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me._Row.Item("FURI_NO").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next


        MyBase.Logger.WriteSQLLog("LMD600DAC", "SelecFurikaebiGet", cmd)
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("FURIKAEBI", "FURIKAEBI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD600_FURIKAE")

        Return ds

    End Function
#End If

#End Region

#Region "設定処理"

#Region "ユーティリティ"

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
    ''' データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMD600DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMD600DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        'Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        '印刷データ取得時、パラメータにRPT_IDセット
        If LMD600DAC.SelectCondition.PTN2.Equals(ptn) Then
            Dim rptId As String = ds.Tables(LMD600DAC.TABLE_NM_M_RPT).Rows(0).Item("RPT_ID").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_ID", rptId, DBDataType.CHAR))
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        cmd.CommandTimeout = 120

        MyBase.Logger.WriteSQLLog(LMD600DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            map.Add(str(i), str(i))
        Next

        Return MyBase.SetSelectResultToDataSet(map, ds, reader, tblNm)

    End Function

    ''' <summary>
    ''' 印刷情報データセット編集
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>LMD600帳票デザインにデータセットのデータを合わせる</remarks>
    Private Function SetDataSet(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMD600DAC.TABLE_NM_OUT)
        Dim max As Integer = ds.Tables(LMD600DAC.TABLE_NM_OUT).Rows.Count - 1
        Dim startRow As Integer = max
        Dim endRow As Integer = max

        If max < 0 Then
            Return ds
        End If

        '入荷S情報の出力スタート行番号取得
        For i As Integer = 0 To max

            If String.IsNullOrEmpty(dt.Rows(i).Item("OUT_MEISAI_LOT_NO").ToString) = True Then
                startRow = i
                Exit For
            End If

        Next

        endRow = max - startRow

        '入荷S情報をデータセットに格納
        For i As Integer = i To endRow

            dt.Rows(i).Item("IN_MEISAI_LOT_NO") = dt.Rows(startRow).Item("IN_MEISAI_LOT_NO")
            dt.Rows(i).Item("KOSU") = dt.Rows(startRow).Item("KOSU")
            dt.Rows(i).Item("SURYO") = dt.Rows(startRow).Item("SURYO")
            dt.Rows(i).Item("IN_MEISAI_IRIME") = dt.Rows(startRow).Item("IN_MEISAI_IRIME")
            dt.Rows(i).Item("STD_IRIME_UT_02") = dt.Rows(startRow).Item("STD_IRIME_UT_02")
            startRow = startRow + 1

        Next

        '必要なくなった行を削除
        For i As Integer = max To endRow + 1 Step -1

            If String.IsNullOrEmpty(dt.Rows(i).Item("OUT_MEISAI_LOT_NO").ToString) = False Then
                Exit For
            End If

            Dim dr As DataRow = dt.Rows(i)
            dt.Rows.Remove(dr)

        Next

        Return ds

    End Function

#End Region 'ユーティリティ

    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMD600DAC.SelectCondition)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))

            Select Case ptn

                Case LMD600DAC.SelectCondition.PTN1 '帳票パターン取得

                    prmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMD600DAC.PTN_ID, DBDataType.CHAR))


                Case LMD600DAC.SelectCondition.PTN2 '印刷データ取得

                    prmList.Add(MyBase.GetSqlParameter("@FURIKAE_KBN", .Item("FURIKAE_KBN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@YOUKI_HENKO", .Item("YOUKI_HENKO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUT_TAX_KB", .Item("OUT_TAX_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@FURIKAEBI", .Item("FURIKAEBI").ToString(), DBDataType.NVARCHAR))       'ADD 2018/12/26 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧した

#If True Then   'ADD 2018/12/25 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい
                Case LMD600DAC.SelectCondition.PTN3 '振替日取得

                    prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.NVARCHAR))
#End If


            End Select


        End With

    End Sub

#End Region


#End Region

End Class
