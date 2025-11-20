' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH510    : EDI出荷チェックリスト
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH510DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH510DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = "-- SET ARITHABORT ON " & vbNewLine _
                                            & "-- SET ARITHIGNORE ON " & vbNewLine _
                                            & " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       EOUTL.NRS_BR_CD                                  AS NRS_BR_CD " & vbNewLine _
                                            & "      , '60'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' EDI出荷(大) - EDI出荷(中),荷(大),出荷(中),荷主Ｍ,商品Ｍ,ユーザーＭ,区分Ｍ
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = " FROM                                                               " & vbNewLine _
                                          & " -- EDI出荷(大)                                                     " & vbNewLine _
                                          & "      $LM_TRN$..H_OUTKAEDI_L EOUTL                                  " & vbNewLine _
                                          & "      -- EDI出荷(中)                                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_M EOUTM                  " & vbNewLine _
                                          & "                   ON EOUTM.NRS_BR_CD        = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND EOUTM.EDI_CTL_NO       = EOUTL.EDI_CTL_NO     " & vbNewLine _
                                          & "      -- 荷主マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                          & "                   ON M_CUST.NRS_BR_CD       = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_L       = EOUTL.CUST_CD_L      " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_M       = EOUTL.CUST_CD_M      " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                          & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = EOUTM.NRS_GOODS_CD   " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(EDI出荷の荷主より取得)                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = EOUTL.CUST_CD_L      " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = EOUTL.CUST_CD_M      " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = '60'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR1                           " & vbNewLine _
                                          & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
                                          & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
                                          & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
                                          & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.PTN_ID      = '60'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD         =  EOUTL.NRS_BR_CD      " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = '60'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>                         
    ''' 帳票種別取得用 SELECT句           
    ''' </summary>                        
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                  " & vbNewLine _
                                           & "       EOUTL.NRS_BR_CD    = @NRS_BR_CD  " & vbNewLine _
                                           & "   AND EOUTL.CUST_CD_L    = @CUST_CD_L  " & vbNewLine _
                                           & "   AND EOUTL.CUST_CD_M    = @CUST_CD_M  " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                       " & vbNewLine _
                                       & "       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID       " & vbNewLine _
                                       & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID       " & vbNewLine _
                                       & "       ELSE MR3.RPT_ID END            AS RPT_ID               " & vbNewLine _
                                       & "     , EOUTL.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
                                       & "     , EOUTL.NRS_BR_NM                AS NRS_BR_NM            " & vbNewLine _
                                       & "     , EOUTL.WH_CD                    AS NRS_WH_CD            " & vbNewLine _
                                       & "     , EOUTL.WH_NM                    AS NRS_WH_NM            " & vbNewLine _
                                       & "     , EOUTL.EDI_CTL_NO               AS EDI_OUTKA_NO_L       " & vbNewLine _
                                       & "     , EOUTL.OUTKA_CTL_NO             AS OUTKA_NO_L           " & vbNewLine _
                                       & "     , EOUTL.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
                                       & "     , EOUTL.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
                                       & "     , M_CUST.CUST_CD_S               AS CUST_CD_S            " & vbNewLine _
                                       & "     , M_CUST.CUST_CD_SS              AS CUST_CD_SS           " & vbNewLine _
                                       & "     , EOUTL.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
                                       & "     , EOUTL.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_L               AS M_CUST_NM_L          " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_M               AS M_CUST_NM_M          " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_S               AS M_CUST_NM_S          " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_SS              AS M_CUST_NM_SS         " & vbNewLine _
                                       & "     , OUTKAL.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE      " & vbNewLine _
                                       & "     , EOUTL.OUTKA_PLAN_DATE          AS EDI_OUTKA_PLAN_DATE  " & vbNewLine _
                                       & "     , OUTKAL.ARR_PLAN_DATE           AS ARR_PLAN_DATE        " & vbNewLine _
                                       & "     , ISNULL(KBN5.KBN_NM1,'')        AS ARR_PLAN_TIME        " & vbNewLine _
                                       & "     , EOUTL.ARR_PLAN_DATE            AS EDI_ARR_PLAN_DATE    " & vbNewLine _
                                       & "     , ISNULL(KBN2.KBN_NM1,'')        AS EDI_ARR_PLAN_TIME    " & vbNewLine _
                                       & "     , ISNULL(KBN3.KBN_NM1,'')        AS PICK_KB              " & vbNewLine _
                                       & "     , EOUTL.CUST_ORD_NO              AS CUST_ORD_NO_L        " & vbNewLine _
                                       & "     , EOUTL.BUYER_ORD_NO             AS BUYER_ORD_NO_L       " & vbNewLine _
                                       & "     , EOUTL.DEST_CD                  AS DEST_CD              " & vbNewLine _
                                       & "     , EOUTL.DEST_NM                  AS DEST_NM              " & vbNewLine _
                                       & "     , EOUTL.DEST_AD_1                AS DEST_AD_1            " & vbNewLine _
                                       & "     , EOUTL.DEST_AD_2                AS DEST_AD_2            " & vbNewLine _
                                       & "     , EOUTL.DEST_AD_3                AS DEST_AD_3            " & vbNewLine _
                                       & "     , OUTKAL.DEST_TEL                AS DEST_TEL             " & vbNewLine _
                                       & "     , EOUTL.DEST_TEL                 AS EDI_DEST_TEL         " & vbNewLine _
                                       & "     , EOUTL.DEST_ZIP                 AS DEST_ZIP             " & vbNewLine _
                                       & "     , EOUTL.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
                                       & "   -- (2012.03.13) Notes874 運送会社名 取得先変更 -- START -- " & vbNewLine _
                                       & "   --, EOUTL.UNSO_NM                  AS UNSO_NM              " & vbNewLine _
                                       & "     , M_UNSOCO.UNSOCO_NM             AS UNSO_NM              " & vbNewLine _
                                       & "   -- (2012.03.13) Notes874 運送会社名 取得先変更 --  END  -- " & vbNewLine _
                                       & "     , ISNULL(KBN4.KBN_NM1,'')        AS BIN_KB               " & vbNewLine _
                                       & "     , OUTKAL.REMARK                  AS REMARK_L             " & vbNewLine _
                                       & "     , EOUTL.REMARK                   AS EDI_REMARK_L         " & vbNewLine _
                                       & "     , F_UNSO_L.REMARK                AS UNSO_ATT             " & vbNewLine _
                                       & "     , EOUTL.UNSO_ATT                 AS EDI_UNSO_ATT         " & vbNewLine _
                                       & "     , EOUTL.CRT_DATE                 AS CRT_DATE             " & vbNewLine _
                                       & "     , EOUTL.CRT_TIME                 AS CRT_TIME             " & vbNewLine _
                                       & "     , EOUTL.DEL_KB                   AS EDI_DEL_KB           " & vbNewLine _
                                       & "     , EOUTL.OUT_FLAG                 AS OUT_FLAG             " & vbNewLine _
                                       & "     , EOUTL.JISSEKI_FLAG             AS JISSEKI_FLAG         " & vbNewLine _
                                       & "     , EOUTL.FREE_C01                 AS L_FREE_C01           " & vbNewLine _
                                       & "     , EOUTL.FREE_C02                 AS L_FREE_C02           " & vbNewLine _
                                       & "     , EOUTL.FREE_C04                 AS L_FREE_C04           " & vbNewLine _
                                       & "     , EOUTL.FREE_C05                 AS L_FREE_C05           " & vbNewLine _
                                       & "     , EOUTL.FREE_C06                 AS L_FREE_C06           " & vbNewLine _
                                       & "     , EOUTL.FREE_C07                 AS L_FREE_C07           " & vbNewLine _
                                       & "     , EOUTM.EDI_CTL_NO_CHU           AS EDI_OUTKA_NO_M       " & vbNewLine _
                                       & "     , OUTKAM.OUTKA_NO_M              AS OUTKA_NO_M           " & vbNewLine _
                                       & "     , EOUTM.NRS_GOODS_CD             AS NRS_GOODS_CD         " & vbNewLine _
                                       & "     , EOUTM.CUST_GOODS_CD            AS CUST_GOODS_CD        " & vbNewLine _
                                       & "     , EOUTM.GOODS_NM                 AS GOODS_NM             " & vbNewLine _
                                       & "     , EOUTM.IRIME                    AS IRIME                " & vbNewLine _
                                       & "     , EOUTM.IRIME_UT                 AS IRIME_UT             " & vbNewLine _
                                       & "     , EOUTM.LOT_NO                   AS LOT_NO               " & vbNewLine _
                                       & "     , EOUTM.SERIAL_NO                AS SERIAL_NO            " & vbNewLine _
                                       & "     , EOUTM.OUTKA_HASU               AS OUTKA_HASU           " & vbNewLine _
                                       & "     , EOUTM.OUTKA_TTL_QT             AS OUTKA_TTL_QT         " & vbNewLine _
                                       & "     , EOUTM.KB_UT                    AS KB_UT                " & vbNewLine _
                                       & "     , EOUTM.QT_UT                    AS QT_UT                " & vbNewLine _
                                       & "     , EOUTM.REMARK                   AS REMARK_M             " & vbNewLine _
                                       & "     , EOUTM.CUST_ORD_NO_DTL          AS CUST_ORD_NO_M        " & vbNewLine _
                                       & "     , EOUTM.BUYER_ORD_NO_DTL         AS BUYER_ORD_NO_M       " & vbNewLine _
                                       & "     , EOUTM.FREE_C02                 AS M_FREE_C02           " & vbNewLine _
                                       & "     , EOUTM.FREE_C06                 AS M_FREE_C06           " & vbNewLine _
                                       & "     , EOUTM.FREE_C07                 AS M_FREE_C07           " & vbNewLine _
                                       & "     , CASE WHEN OUTKAL.SYS_DEL_FLG IS NULL THEN 'EDI'        " & vbNewLine _
                                       & "            WHEN OUTKAL.SYS_DEL_FLG ='1'    THEN '出荷取消'   " & vbNewLine _
                                       & "       ELSE ISNULL(KBN1.KBN_NM1,'')                           " & vbNewLine _
                                       & "       END                            AS PROGRESS             " & vbNewLine _
                                       & "     , EOUTL.SYS_UPD_USER             AS SYS_ENT_USER_CD      " & vbNewLine _
                                       & "     , ENT_USER.USER_NM               AS SYS_ENT_USER_NM      " & vbNewLine _
                                       & "     , EOUTL.SYS_UPD_USER             AS SYS_UPD_USER_CD      " & vbNewLine _
                                       & "     , UPD_USER.USER_NM               AS SYS_UPD_USER_NM      " & vbNewLine _
                                       & "     , @PRINT_USER_CD                 AS PRINT_USER_CD        " & vbNewLine _
                                       & "     , @PRINT_USER_NM                 AS PRINT_USER_NM        " & vbNewLine _
                                       & "     , EOUTL.DEL_KB                   AS DEL_KB               " & vbNewLine _
                                       & "     , EOUTM.OUTKA_PKG_NB             AS OUTKA_PKG_NB         " & vbNewLine _
                                       & "   -- (2012.03.14) 項目追加 -- START --                       " & vbNewLine _
                                       & "     , ISNULL(OUTKAL.OUTKA_STATE_KB,'') AS OUTKA_STATE_KB     " & vbNewLine _
                                       & "     , M_EDI_CUST.FLAG_01               AS FLAG_01            " & vbNewLine _
                                       & "     , EOUTM.ALCTD_KB                   AS ALCTD_KB           " & vbNewLine _
                                       & "     , M_GOODS.PKG_NB                   AS M_PKG_NB           " & vbNewLine _
                                       & "     , EOUTM.PKG_NB                     AS EDI_PKG_NB         " & vbNewLine _
                                       & "   -- (2012.03.14) 項目追加 --  END  --                       " & vbNewLine _
                                       & "   -- (2015.05.28) 項目追加 -- START --                       " & vbNewLine _
                                       & "     , EOUTL.FREE_C03                 AS L_FREE_C03           " & vbNewLine _
                                       & "     , EOUTL.FREE_C24                 AS L_FREE_C24           " & vbNewLine _
                                       & "     , EOUTL.FREE_C25                 AS L_FREE_C25           " & vbNewLine _
                                       & "   -- (2015.05.28) 項目追加 --  END  --                       " & vbNewLine _
                                       & "     , EOUTL.FREE_C25                 AS L_FREE_C25           " & vbNewLine _
                                       & "   -- 要望番号：002325　キャンセルデータの受信時刻出力対応    " & vbNewLine _
                                       & "     , HED_DIC_NEW.EDI_CTL_NO         AS DELETE_EDI_NO        " & vbNewLine _
                                       & "     , HED_DIC_NEW.SYS_ENT_DATE       AS INPUT_DATE           " & vbNewLine _
                                       & "	 , HED_DIC_NEW.SYS_ENT_TIME         AS INPUT_TIME           " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 SELECT句　LMH512用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LMH512 As String = " SELECT                                                       " & vbNewLine _
                                       & "       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID             " & vbNewLine _
                                       & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID             " & vbNewLine _
                                       & "       ELSE MR3.RPT_ID END            AS RPT_ID                     " & vbNewLine _
                                       & "     , EOUTL.NRS_BR_CD                AS NRS_BR_CD                  " & vbNewLine _
                                       & "     , EOUTL.NRS_BR_NM                AS NRS_BR_NM                  " & vbNewLine _
                                       & "     , EOUTL.WH_CD                    AS NRS_WH_CD                  " & vbNewLine _
                                       & "     , EOUTL.WH_NM                    AS NRS_WH_NM                  " & vbNewLine _
                                       & "     , EOUTL.EDI_CTL_NO               AS EDI_OUTKA_NO_L             " & vbNewLine _
                                       & "     , EOUTL.OUTKA_CTL_NO             AS OUTKA_NO_L                 " & vbNewLine _
                                       & "     , EOUTL.CUST_CD_L                AS CUST_CD_L                  " & vbNewLine _
                                       & "     , EOUTL.CUST_CD_M                AS CUST_CD_M                  " & vbNewLine _
                                       & "     , CASE WHEN M_CUST.CUST_CD_S IS NOT NULL THEN M_CUST.CUST_CD_S " & vbNewLine _
                                       & "       ELSE '00' END                  AS CUST_CD_S                 " & vbNewLine _
                                       & "     , CASE WHEN M_CUST.CUST_CD_SS IS NOT NULL THEN M_CUST.CUST_CD_SS " & vbNewLine _
                                       & "       ELSE '00' END                  AS CUST_CD_SS                 " & vbNewLine _
                                       & "     , EOUTL.CUST_NM_L                AS CUST_NM_L                  " & vbNewLine _
                                       & "     , EOUTL.CUST_NM_M                AS CUST_NM_M                  " & vbNewLine _
                                       & "     , CASE WHEN  M_CUST.CUST_NM_L IS NOT NULL THEN  M_CUST.CUST_NM_L  " & vbNewLine _
                                       & "       ELSE M_CUST1.CUST_NM_L END     AS M_CUST_NM_L                 " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_M               AS M_CUST_NM_M                " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_S               AS M_CUST_NM_S                " & vbNewLine _
                                       & "     , M_CUST.CUST_NM_SS              AS M_CUST_NM_SS               " & vbNewLine _
                                       & "     , OUTKAL.OUTKA_PLAN_DATE         AS OUTKA_PLAN_DATE            " & vbNewLine _
                                       & "     , EOUTL.OUTKA_PLAN_DATE          AS EDI_OUTKA_PLAN_DATE        " & vbNewLine _
                                       & "     , OUTKAL.ARR_PLAN_DATE           AS ARR_PLAN_DATE              " & vbNewLine _
                                       & "     , ISNULL(KBN5.KBN_NM1,'')        AS ARR_PLAN_TIME              " & vbNewLine _
                                       & "     , EOUTL.ARR_PLAN_DATE            AS EDI_ARR_PLAN_DATE          " & vbNewLine _
                                       & "     , ISNULL(KBN2.KBN_NM1,'')        AS EDI_ARR_PLAN_TIME          " & vbNewLine _
                                       & "     , ISNULL(KBN3.KBN_NM1,'')        AS PICK_KB                    " & vbNewLine _
                                       & "     , EOUTL.CUST_ORD_NO              AS CUST_ORD_NO_L              " & vbNewLine _
                                       & "     , EOUTL.BUYER_ORD_NO             AS BUYER_ORD_NO_L             " & vbNewLine _
                                       & "     , EOUTL.DEST_CD                  AS DEST_CD                    " & vbNewLine _
                                       & "     , EOUTL.DEST_NM                  AS DEST_NM                    " & vbNewLine _
                                       & "     , EOUTL.DEST_AD_1                AS DEST_AD_1                  " & vbNewLine _
                                       & "     , EOUTL.DEST_AD_2                AS DEST_AD_2                  " & vbNewLine _
                                       & "     , EOUTL.DEST_AD_3                AS DEST_AD_3                  " & vbNewLine _
                                       & "     , OUTKAL.DEST_TEL                AS DEST_TEL                   " & vbNewLine _
                                       & "     , EOUTL.DEST_TEL                 AS EDI_DEST_TEL               " & vbNewLine _
                                       & "     , EOUTL.DEST_ZIP                 AS DEST_ZIP                   " & vbNewLine _
                                       & "     , EOUTL.UNSO_CD                  AS UNSO_CD                    " & vbNewLine _
                                       & "   -- (2012.03.13) Notes874 運送会社名 取得先変更 -- START --       " & vbNewLine _
                                       & "   --, EOUTL.UNSO_NM                  AS UNSO_NM                    " & vbNewLine _
                                       & "     , M_UNSOCO.UNSOCO_NM             AS UNSO_NM                    " & vbNewLine _
                                       & "   -- (2012.03.13) Notes874 運送会社名 取得先変更 --  END  --       " & vbNewLine _
                                       & "     , ISNULL(KBN4.KBN_NM1,'')        AS BIN_KB                     " & vbNewLine _
                                       & "     , OUTKAL.REMARK                  AS REMARK_L                   " & vbNewLine _
                                       & "     , EOUTL.REMARK                   AS EDI_REMARK_L               " & vbNewLine _
                                       & "     , F_UNSO_L.REMARK                AS UNSO_ATT                   " & vbNewLine _
                                       & "     , EOUTL.UNSO_ATT                 AS EDI_UNSO_ATT               " & vbNewLine _
                                       & "     , EOUTL.CRT_DATE                 AS CRT_DATE                   " & vbNewLine _
                                       & "     , EOUTL.CRT_TIME                 AS CRT_TIME                   " & vbNewLine _
                                       & "     , EOUTL.DEL_KB                   AS EDI_DEL_KB                 " & vbNewLine _
                                       & "     , EOUTL.OUT_FLAG                 AS OUT_FLAG                   " & vbNewLine _
                                       & "     , EOUTL.JISSEKI_FLAG             AS JISSEKI_FLAG               " & vbNewLine _
                                       & "     , EOUTL.FREE_C01                 AS L_FREE_C01                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C02                 AS L_FREE_C02                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C04                 AS L_FREE_C04                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C05                 AS L_FREE_C05                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C06                 AS L_FREE_C06                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C07                 AS L_FREE_C07                 " & vbNewLine _
                                       & "     , EOUTM.EDI_CTL_NO_CHU           AS EDI_OUTKA_NO_M             " & vbNewLine _
                                       & "     , OUTKAM.OUTKA_NO_M              AS OUTKA_NO_M                 " & vbNewLine _
                                       & "     , EOUTM.NRS_GOODS_CD             AS NRS_GOODS_CD               " & vbNewLine _
                                       & "     , EOUTM.CUST_GOODS_CD            AS CUST_GOODS_CD              " & vbNewLine _
                                       & "     , EOUTM.GOODS_NM                 AS GOODS_NM                   " & vbNewLine _
                                       & "     , EOUTM.IRIME                    AS IRIME                      " & vbNewLine _
                                       & "     , EOUTM.IRIME_UT                 AS IRIME_UT                   " & vbNewLine _
                                       & "     , EOUTM.LOT_NO                   AS LOT_NO                     " & vbNewLine _
                                       & "     , EOUTM.SERIAL_NO                AS SERIAL_NO                  " & vbNewLine _
                                       & "     , EOUTM.OUTKA_HASU               AS OUTKA_HASU                 " & vbNewLine _
                                       & "     , EOUTM.OUTKA_TTL_QT             AS OUTKA_TTL_QT               " & vbNewLine _
                                       & "     , EOUTM.KB_UT                    AS KB_UT                      " & vbNewLine _
                                       & "     , EOUTM.QT_UT                    AS QT_UT                      " & vbNewLine _
                                       & "     , EOUTM.REMARK                   AS REMARK_M                   " & vbNewLine _
                                       & "     , EOUTM.CUST_ORD_NO_DTL          AS CUST_ORD_NO_M              " & vbNewLine _
                                       & "     , EOUTM.BUYER_ORD_NO_DTL         AS BUYER_ORD_NO_M             " & vbNewLine _
                                       & "     , EOUTM.FREE_C02                 AS M_FREE_C02                 " & vbNewLine _
                                       & "     , EOUTM.FREE_C06                 AS M_FREE_C06                 " & vbNewLine _
                                       & "     , EOUTM.FREE_C07                 AS M_FREE_C07                 " & vbNewLine _
                                       & "     , CASE WHEN OUTKAL.SYS_DEL_FLG IS NULL THEN 'EDI'              " & vbNewLine _
                                       & "            WHEN OUTKAL.SYS_DEL_FLG ='1'    THEN '出荷取消'         " & vbNewLine _
                                       & "       ELSE ISNULL(KBN1.KBN_NM1,'')                                 " & vbNewLine _
                                       & "       END                            AS PROGRESS                   " & vbNewLine _
                                       & "     , EOUTL.SYS_UPD_USER             AS SYS_ENT_USER_CD            " & vbNewLine _
                                       & "     , ENT_USER.USER_NM               AS SYS_ENT_USER_NM            " & vbNewLine _
                                       & "     , EOUTL.SYS_UPD_USER             AS SYS_UPD_USER_CD            " & vbNewLine _
                                       & "     , UPD_USER.USER_NM               AS SYS_UPD_USER_NM            " & vbNewLine _
                                       & "     , @PRINT_USER_CD                 AS PRINT_USER_CD              " & vbNewLine _
                                       & "     , @PRINT_USER_NM                 AS PRINT_USER_NM              " & vbNewLine _
                                       & "     , EOUTL.DEL_KB                   AS DEL_KB                     " & vbNewLine _
                                       & "     , EOUTM.OUTKA_PKG_NB             AS OUTKA_PKG_NB               " & vbNewLine _
                                       & "   -- (2012.03.14) 項目追加 -- START --                             " & vbNewLine _
                                       & "     , ISNULL(OUTKAL.OUTKA_STATE_KB,'') AS OUTKA_STATE_KB           " & vbNewLine _
                                       & "     , M_EDI_CUST.FLAG_01               AS FLAG_01                  " & vbNewLine _
                                       & "     , EOUTM.ALCTD_KB                   AS ALCTD_KB                 " & vbNewLine _
                                       & "     , M_GOODS.PKG_NB                   AS M_PKG_NB                 " & vbNewLine _
                                       & "     , EOUTM.PKG_NB                     AS EDI_PKG_NB               " & vbNewLine _
                                       & "   -- (2012.03.14) 項目追加 --  END  --                             " & vbNewLine _
                                       & "   -- (2015.05.28) 項目追加 -- START --                             " & vbNewLine _
                                       & "     , EOUTL.FREE_C03                 AS L_FREE_C03                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C24                 AS L_FREE_C24                 " & vbNewLine _
                                       & "     , EOUTL.FREE_C25                 AS L_FREE_C25                 " & vbNewLine _
                                       & "   -- (2015.05.28) 項目追加 --  END  --                             " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' EDI出荷(大) - EDI出荷(中),出荷(大),出荷(中),運送(大),荷主Ｍ,商品Ｍ,運送会社Ｍ,届先Ｍ,ユーザーＭ,担当者Ｍ,EDI荷主Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = " FROM                                                                " & vbNewLine _
                                     & " -- EDI出荷(大)                                                      " & vbNewLine _
                                     & "      $LM_TRN$..H_OUTKAEDI_L EOUTL                                   " & vbNewLine _
                                     & "      -- EDI出荷(中)                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_M EOUTM                   " & vbNewLine _
                                     & "                   ON EOUTM.NRS_BR_CD        = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND EOUTM.EDI_CTL_NO       = EOUTL.EDI_CTL_NO      " & vbNewLine _
                                     & " --EDI出荷(中MIN)                                                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN                                                " & vbNewLine _
                                     & "        (SELECT                                                      " & vbNewLine _
                                     & "            NRS_BR_CD                                                " & vbNewLine _
                                     & "            ,EDI_CTL_NO                                              " & vbNewLine _
                                     & "            ,MIN(EDI_CTL_NO_CHU) AS  EDI_CTL_NO_CHU                  " & vbNewLine _
                                     & "            ,MIN(NRS_GOODS_CD) AS NRS_GOODS_CD                       " & vbNewLine _
                                     & "        FROM $LM_TRN$..H_OUTKAEDI_M WHERE SYS_DEL_FLG ='0'             " & vbNewLine _
                                     & "        GROUP BY NRS_BR_CD,EDI_CTL_NO) EOUTM_MIN                     " & vbNewLine _
                                     & "        ON EOUTM_MIN.NRS_BR_CD        = EOUTL.NRS_BR_CD              " & vbNewLine _
                                     & "        AND EOUTM_MIN.EDI_CTL_NO       = EOUTL.EDI_CTL_NO            " & vbNewLine _
                                     & "      LEFT JOIN (                                                    " & vbNewLine _
                                     & "        SELECT                                                       " & vbNewLine _
                                     & "            H_OUTKAEDI_M.NRS_BR_CD                                   " & vbNewLine _
                                     & "            ,H_OUTKAEDI_M.EDI_CTL_NO                                 " & vbNewLine _
                                     & "            ,H_OUTKAEDI_M.CUST_ORD_NO_DTL                            " & vbNewLine _
                                     & "            ,H_OUTKAEDI_M.GOODS_NM                                   " & vbNewLine _
                                     & "        FROM                                                         " & vbNewLine _
                                     & "            $LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                  " & vbNewLine _
                                     & "        WHERE                                                        " & vbNewLine _
                                     & "            H_OUTKAEDI_M.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                     & "            AND H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                  " & vbNewLine _
                                     & "            AND H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                     & "      ) AS H_OUTKAEDI_M_FST                                          " & vbNewLine _
                                     & "      ON EOUTL.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD                 " & vbNewLine _
                                     & "      AND EOUTL.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO              " & vbNewLine _
                                     & "      -- 出荷(大)                                                    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L OUTKAL                     " & vbNewLine _
                                     & "                   ON OUTKAL.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND OUTKAL.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO    " & vbNewLine _
                                     & "      -- 出荷(中)                                                    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                     " & vbNewLine _
                                     & "                   ON OUTKAM.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND OUTKAM.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO    " & vbNewLine _
                                     & "                  AND OUTKAM.OUTKA_NO_M      = EOUTM.EDI_CTL_NO_CHU  " & vbNewLine _
                                     & "      -- 運送(大)                                                    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L F_UNSO_L                    " & vbNewLine _
                                     & "                   ON F_UNSO_L.NRS_BR_CD     = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND F_UNSO_L.INOUTKA_NO_L  = EOUTL.OUTKA_CTL_NO    " & vbNewLine _
                                     & "                  AND EOUTL.OUTKA_CTL_NO     <> ''                   " & vbNewLine _
                                     & "                  AND (  F_UNSO_L.MOTO_DATA_KB = '20'                " & vbNewLine _
                                     & "                      OR F_UNSO_L.MOTO_DATA_KB = '40')               " & vbNewLine _
                                     & "      -- 商品マスタMIN                                               " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                   " & vbNewLine _
                                     & "                   ON M_GOODS_MIN.NRS_BR_CD      = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_GOODS_MIN.GOODS_CD_NRS   = EOUTM_MIN.NRS_GOODS_CD    " & vbNewLine _
                                     & "      -- 荷主マスタ                                                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                        " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = M_GOODS_MIN.CUST_CD_L     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = M_GOODS_MIN.CUST_CD_M     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = M_GOODS_MIN.CUST_CD_S     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = M_GOODS_MIN.CUST_CD_SS    " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                   " & vbNewLine _
                                     & "      -- 商品マスタ                                                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = EOUTM.NRS_GOODS_CD    " & vbNewLine _
                                     & "      -- 運送会社マスタ                                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO M_UNSOCO                    " & vbNewLine _
                                     & "                   ON M_UNSOCO.NRS_BR_CD     = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_UNSOCO.UNSOCO_CD     = EOUTL.UNSO_CD         " & vbNewLine _
                                     & "                  AND M_UNSOCO.UNSOCO_BR_CD  = EOUTL.UNSO_BR_CD      " & vbNewLine _
                                     & "      -- 届先マスタ                                                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                        " & vbNewLine _
                                     & "                   ON M_DEST.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_DEST.CUST_CD_L       = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_DEST.DEST_CD         = EOUTL.DEST_CD         " & vbNewLine _
                                     & "      -- 担当者別荷主マスタ (ユーザーで集約)                         " & vbNewLine _
                                     & "      LEFT OUTER JOIN (SELECT M_TCUST.CUST_CD_L AS CUST_CD_L         " & vbNewLine _
                                     & "                            , MIN(USER_CD)      AS USER_CD           " & vbNewLine _
                                     & "                         FROM $LM_MST$..M_TCUST M_TCUST              " & vbNewLine _
                                     & "                        GROUP BY M_TCUST.CUST_CD_L                   " & vbNewLine _
                                     & "                       ) M_TCUST                                     " & vbNewLine _
                                     & "                   ON EOUTL.CUST_CD_L        = M_TCUST.CUST_CD_L     " & vbNewLine _
                                     & "      -- ユーザーマスタ 入力者                                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER ENT_USER                      " & vbNewLine _
                                     & "                   ON ENT_USER.USER_CD       = EOUTL.SYS_ENT_USER    " & vbNewLine _
                                     & "      -- ユーザーマスタ 更新者                                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER UPD_USER                      " & vbNewLine _
                                     & "                   ON UPD_USER.USER_CD       = EOUTL.SYS_UPD_USER    " & vbNewLine _
                                     & "      -- ユーザーマスタ 担当者                                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER TANTO_USER                    " & vbNewLine _
                                     & "      -- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                     & "                   ON TANTO_USER.USER_CD     = M_CUST.TANTO_CD       " & vbNewLine _
                                     & "      --             ON TANTO_USER.USER_CD     = M_TCUST.USER_CD     " & vbNewLine _
                                     & "      -- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                     & "      -- EDI荷主マスタ                                               " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_EDI_CUST M_EDI_CUST                " & vbNewLine _
                                     & "                   ON M_EDI_CUST.NRS_BR_CD   = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.CUST_CD_L   = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.CUST_CD_M   = EOUTL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.WH_CD       = EOUTL.WH_CD           " & vbNewLine _
                                     & "                  AND M_EDI_CUST.INOUT_KB    = '0'                   " & vbNewLine _
                                     & "                  AND M_EDI_CUST.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <S010> 出荷状態区分                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN1                           " & vbNewLine _
                                     & "                   ON KBN1.KBN_GROUP_CD      = 'S010'                " & vbNewLine _
                                     & "                  AND KBN1.KBN_CD            = OUTKAL.OUTKA_STATE_KB " & vbNewLine _
                                     & "                  AND KBN1.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <N010> 納入予定時刻                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN2                           " & vbNewLine _
                                     & "                   ON KBN2.KBN_GROUP_CD      = 'N010'                " & vbNewLine _
                                     & "                  AND KBN2.KBN_CD            = EOUTL.ARR_PLAN_TIME   " & vbNewLine _
                                     & "                  AND KBN2.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <N010> 納入予定時刻                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN5                           " & vbNewLine _
                                     & "                   ON KBN5.KBN_GROUP_CD      = 'N010'                " & vbNewLine _
                                     & "                  AND KBN5.KBN_CD            = OUTKAL.ARR_PLAN_TIME  " & vbNewLine _
                                     & "                  AND KBN5.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <P001> まとめピック区分                          " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN3                           " & vbNewLine _
                                     & "                   ON KBN3.KBN_GROUP_CD      = 'P001'                " & vbNewLine _
                                     & "                  AND KBN3.KBN_CD            = EOUTL.PICK_KB         " & vbNewLine _
                                     & "                  AND KBN3.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <U001> 運送便区分                                " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN4                           " & vbNewLine _
                                     & "                   ON KBN4.KBN_GROUP_CD      = 'U001'                " & vbNewLine _
                                     & "                  AND KBN4.KBN_CD            = EOUTL.BIN_KB          " & vbNewLine _
                                     & "                  AND KBN4.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(EDI出荷の荷主より取得)                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = EOUTL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '60'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                           " & vbNewLine _
                                     & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD  " & vbNewLine _
                                     & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID     " & vbNewLine _
                                     & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD     " & vbNewLine _
                                     & "                  AND MR1.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ②(商品マスタより)                        " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                " & vbNewLine _
                                     & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD     " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L     " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M     " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '60'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                           " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD  " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID     " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD     " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  EOUTL.NRS_BR_CD      " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '60'                  " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                  " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "	 -- DIC取込データ＜削除更新が発生したデータの取得＞              " & vbNewLine _
                                     & "	 LEFT OUTER JOIN LM_TRN..H_INOUTKAEDI_HED_DIC_NEW HED_DIC_NEW    " & vbNewLine _
                                     & "	              ON HED_DIC_NEW.NRS_BR_CD       = EOUTL.NRS_BR_CD   " & vbNewLine _
                                     & "                 AND HED_DIC_NEW.CUST_CD_L       = EOUTL.CUST_CD_L   " & vbNewLine _
                                     & "                 AND HED_DIC_NEW.CUST_CD_M       = EOUTL.CUST_CD_M   " & vbNewLine _
                                     & "                 AND HED_DIC_NEW.INOUT_KB        = '0'               " & vbNewLine _
                                     & "                 AND HED_DIC_NEW.DEL_KB          = '1'               " & vbNewLine _
                                     & "	             AND HED_DIC_NEW.AKAKURO_KBN     = '0'               " & vbNewLine _
                                     & "                 AND RIGHT(HED_DIC_NEW.OUTKA_CTL_NO,8) = '00000000'  " & vbNewLine _
                                     & "                 AND HED_DIC_NEW.DENPYO_NO IN ( EOUTL.CUST_ORD_NO )  " & vbNewLine



    ''' <summary>
    ''' 印刷データ抽出用 FROM句 LMH512用
    ''' </summary>
    ''' <remarks>
    ''' EDI出荷(大) - EDI出荷(中),出荷(大),出荷(中),運送(大),荷主Ｍ,商品Ｍ,運送会社Ｍ,届先Ｍ,ユーザーＭ,担当者Ｍ,EDI荷主Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM_LMH512 As String = " FROM                                                                " & vbNewLine _
                                     & " -- EDI出荷(大)                                                      " & vbNewLine _
                                     & "      $LM_TRN$..H_OUTKAEDI_L EOUTL                                   " & vbNewLine _
                                     & "      -- EDI出荷(中)                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_M EOUTM                   " & vbNewLine _
                                     & "                   ON EOUTM.NRS_BR_CD        = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND EOUTM.EDI_CTL_NO       = EOUTL.EDI_CTL_NO      " & vbNewLine _
                                     & " --EDI出荷(中MIN)                                                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN                                                " & vbNewLine _
                                     & "        (SELECT                                                      " & vbNewLine _
                                     & "            NRS_BR_CD                                                " & vbNewLine _
                                     & "            ,EDI_CTL_NO                                              " & vbNewLine _
                                     & "            ,MIN(EDI_CTL_NO_CHU) AS  EDI_CTL_NO_CHU                  " & vbNewLine _
                                     & "            ,MIN(NRS_GOODS_CD) AS NRS_GOODS_CD                       " & vbNewLine _
                                     & "        FROM $LM_TRN$..H_OUTKAEDI_M WHERE SYS_DEL_FLG ='0'             " & vbNewLine _
                                     & "        GROUP BY NRS_BR_CD,EDI_CTL_NO) EOUTM_MIN                     " & vbNewLine _
                                     & "        ON EOUTM_MIN.NRS_BR_CD        = EOUTL.NRS_BR_CD              " & vbNewLine _
                                     & "        AND EOUTM_MIN.EDI_CTL_NO       = EOUTL.EDI_CTL_NO            " & vbNewLine _
                                     & "      LEFT JOIN (                                                    " & vbNewLine _
                                     & "        SELECT                                                       " & vbNewLine _
                                     & "            H_OUTKAEDI_M.NRS_BR_CD                                   " & vbNewLine _
                                     & "            ,H_OUTKAEDI_M.EDI_CTL_NO                                 " & vbNewLine _
                                     & "            ,H_OUTKAEDI_M.CUST_ORD_NO_DTL                            " & vbNewLine _
                                     & "            ,H_OUTKAEDI_M.GOODS_NM                                   " & vbNewLine _
                                     & "        FROM                                                         " & vbNewLine _
                                     & "            $LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                  " & vbNewLine _
                                     & "        WHERE                                                        " & vbNewLine _
                                     & "            H_OUTKAEDI_M.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                     & "            AND H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                  " & vbNewLine _
                                     & "            AND H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                     & "      ) AS H_OUTKAEDI_M_FST                                          " & vbNewLine _
                                     & "      ON EOUTL.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD                 " & vbNewLine _
                                     & "      AND EOUTL.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO              " & vbNewLine _
                                     & "      -- 出荷(大)                                                    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..C_OUTKA_L OUTKAL                     " & vbNewLine _
                                     & "                   ON OUTKAL.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND OUTKAL.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO    " & vbNewLine _
                                     & "      -- 出荷(中)                                                    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                     " & vbNewLine _
                                     & "                   ON OUTKAM.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND OUTKAM.OUTKA_NO_L      = EOUTL.OUTKA_CTL_NO    " & vbNewLine _
                                     & "                  AND OUTKAM.OUTKA_NO_M      = EOUTM.EDI_CTL_NO_CHU  " & vbNewLine _
                                     & "      -- 運送(大)                                                    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L F_UNSO_L                    " & vbNewLine _
                                     & "                   ON F_UNSO_L.NRS_BR_CD     = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND F_UNSO_L.INOUTKA_NO_L  = EOUTL.OUTKA_CTL_NO    " & vbNewLine _
                                     & "                  AND EOUTL.OUTKA_CTL_NO     <> ''                   " & vbNewLine _
                                     & "                  AND (  F_UNSO_L.MOTO_DATA_KB = '20'                " & vbNewLine _
                                     & "                      OR F_UNSO_L.MOTO_DATA_KB = '40')               " & vbNewLine _
                                     & "      -- 商品マスタMIN                                               " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS_MIN                   " & vbNewLine _
                                     & "                   ON M_GOODS_MIN.NRS_BR_CD      = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_GOODS_MIN.GOODS_CD_NRS   = EOUTM_MIN.NRS_GOODS_CD    " & vbNewLine _
                                     & "      -- 荷主マスタ                                                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                        " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = M_GOODS_MIN.CUST_CD_L     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = M_GOODS_MIN.CUST_CD_M     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = M_GOODS_MIN.CUST_CD_S     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = M_GOODS_MIN.CUST_CD_SS    " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST1                        " & vbNewLine _
                                     & "                   ON M_CUST1.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUST1.CUST_CD_L       = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_CUST1.CUST_CD_M       = EOUTL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_CUST1.CUST_CD_S       = '00'                  " & vbNewLine _
                                     & "                  AND M_CUST1.CUST_CD_SS      = '00'                  " & vbNewLine _
                                     & "                  AND M_CUST1.SYS_DEL_FLG     = '0'                   " & vbNewLine _
                                     & "      -- 商品マスタ                                                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = EOUTM.NRS_GOODS_CD    " & vbNewLine _
                                     & "      -- 運送会社マスタ                                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO M_UNSOCO                    " & vbNewLine _
                                     & "                   ON M_UNSOCO.NRS_BR_CD     = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_UNSOCO.UNSOCO_CD     = EOUTL.UNSO_CD         " & vbNewLine _
                                     & "                  AND M_UNSOCO.UNSOCO_BR_CD  = EOUTL.UNSO_BR_CD      " & vbNewLine _
                                     & "      -- 届先マスタ                                                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                        " & vbNewLine _
                                     & "                   ON M_DEST.NRS_BR_CD       = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_DEST.CUST_CD_L       = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_DEST.DEST_CD         = EOUTL.DEST_CD         " & vbNewLine _
                                     & "      -- 担当者別荷主マスタ (ユーザーで集約)                         " & vbNewLine _
                                     & "      LEFT OUTER JOIN (SELECT M_TCUST.CUST_CD_L AS CUST_CD_L         " & vbNewLine _
                                     & "                            , MIN(USER_CD)      AS USER_CD           " & vbNewLine _
                                     & "                         FROM $LM_MST$..M_TCUST M_TCUST              " & vbNewLine _
                                     & "                        GROUP BY M_TCUST.CUST_CD_L                   " & vbNewLine _
                                     & "                       ) M_TCUST                                     " & vbNewLine _
                                     & "                   ON EOUTL.CUST_CD_L        = M_TCUST.CUST_CD_L     " & vbNewLine _
                                     & "      -- ユーザーマスタ 入力者                                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER ENT_USER                      " & vbNewLine _
                                     & "                   ON ENT_USER.USER_CD       = EOUTL.SYS_ENT_USER    " & vbNewLine _
                                     & "      -- ユーザーマスタ 更新者                                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER UPD_USER                      " & vbNewLine _
                                     & "                   ON UPD_USER.USER_CD       = EOUTL.SYS_UPD_USER    " & vbNewLine _
                                     & "      -- ユーザーマスタ 担当者                                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER TANTO_USER                    " & vbNewLine _
                                     & "      -- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                     & "                   ON TANTO_USER.USER_CD     = M_CUST.TANTO_CD       " & vbNewLine _
                                     & "      --             ON TANTO_USER.USER_CD     = M_TCUST.USER_CD     " & vbNewLine _
                                     & "      -- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                     & "      -- EDI荷主マスタ                                               " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_EDI_CUST M_EDI_CUST                " & vbNewLine _
                                     & "                   ON M_EDI_CUST.NRS_BR_CD   = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.CUST_CD_L   = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.CUST_CD_M   = EOUTL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.WH_CD       = EOUTL.WH_CD           " & vbNewLine _
                                     & "                  AND M_EDI_CUST.INOUT_KB    = '0'                   " & vbNewLine _
                                     & "                  AND M_EDI_CUST.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <S010> 出荷状態区分                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN1                           " & vbNewLine _
                                     & "                   ON KBN1.KBN_GROUP_CD      = 'S010'                " & vbNewLine _
                                     & "                  AND KBN1.KBN_CD            = OUTKAL.OUTKA_STATE_KB " & vbNewLine _
                                     & "                  AND KBN1.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <N010> 納入予定時刻                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN2                           " & vbNewLine _
                                     & "                   ON KBN2.KBN_GROUP_CD      = 'N010'                " & vbNewLine _
                                     & "                  AND KBN2.KBN_CD            = EOUTL.ARR_PLAN_TIME   " & vbNewLine _
                                     & "                  AND KBN2.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <N010> 納入予定時刻                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN5                           " & vbNewLine _
                                     & "                   ON KBN5.KBN_GROUP_CD      = 'N010'                " & vbNewLine _
                                     & "                  AND KBN5.KBN_CD            = OUTKAL.ARR_PLAN_TIME  " & vbNewLine _
                                     & "                  AND KBN5.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <P001> まとめピック区分                          " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN3                           " & vbNewLine _
                                     & "                   ON KBN3.KBN_GROUP_CD      = 'P001'                " & vbNewLine _
                                     & "                  AND KBN3.KBN_CD            = EOUTL.PICK_KB         " & vbNewLine _
                                     & "                  AND KBN3.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 区分マスタ <U001> 運送便区分                                " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN4                           " & vbNewLine _
                                     & "                   ON KBN4.KBN_GROUP_CD      = 'U001'                " & vbNewLine _
                                     & "                  AND KBN4.KBN_CD            = EOUTL.BIN_KB          " & vbNewLine _
                                     & "                  AND KBN4.SYS_DEL_FLG       = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(EDI出荷の荷主より取得)                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = EOUTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = EOUTL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = EOUTL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '60'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                           " & vbNewLine _
                                     & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD  " & vbNewLine _
                                     & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID     " & vbNewLine _
                                     & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD     " & vbNewLine _
                                     & "                  AND MR1.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ②(商品マスタより)                        " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                " & vbNewLine _
                                     & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD     " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L     " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M     " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '60'                  " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                           " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD  " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID     " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD     " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >    " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  EOUTL.NRS_BR_CD      " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '60'                  " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                  " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                   " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' Notes993ソート順変更
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                     " & vbNewLine _
                                         & "      EOUTL.CUST_CD_L         " & vbNewLine _
                                         & "    , EOUTL.CUST_CD_M         " & vbNewLine _
                                         & "    , EOUTL.OUTKA_PLAN_DATE   " & vbNewLine _
                                         & "    , OUTKAL.OUTKA_PLAN_DATE  " & vbNewLine _
                                         & "    , EOUTL.OUTKA_CTL_NO      " & vbNewLine _
                                         & "    --Notes993開始            " & vbNewLine _
                                         & "    --, OUTKAM.OUTKA_NO_M     " & vbNewLine _
                                         & "    , ISNULL(OUTKAM.OUTKA_NO_M,'999')  " & vbNewLine _
                                         & "    --Notes993終了            " & vbNewLine _
                                         & "    , EOUTM.EDI_CTL_NO        " & vbNewLine _
                                         & "    , EOUTM.EDI_CTL_NO_CHU    " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' LMH512ソート順変更
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMH512 As String = " ORDER BY                     " & vbNewLine _
                                                & "      EOUTL.CUST_CD_L         " & vbNewLine _
                                                & "    , EOUTL.CUST_CD_M         " & vbNewLine _
                                                & "    , EOUTM.EDI_CTL_NO        " & vbNewLine _
                                                & "    , EOUTL.OUTKA_PLAN_DATE   " & vbNewLine _
                                                & "    , OUTKAL.OUTKA_PLAN_DATE  " & vbNewLine _
                                                & "    , EOUTL.OUTKA_CTL_NO      " & vbNewLine _
                                                & "    --Notes993開始            " & vbNewLine _
                                                & "    --, OUTKAM.OUTKA_NO_M     " & vbNewLine _
                                                & "    , ISNULL(OUTKAM.OUTKA_NO_M,'999')  " & vbNewLine _
                                                & "    --Notes993終了            " & vbNewLine


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

    ''' <summary>
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim nrsBrCd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString
        Dim custCdL As String = inTbl.Rows(0).Item("CUST_CD_L").ToString
        Dim custCdM As String = inTbl.Rows(0).Item("CUST_CD_M").ToString

        'SQL作成
        Me._StrSql.Append(LMH510DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH510DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        'Me._StrSql.Append(LMH510DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)


        ''速度緊急対応。。
        _StrSql.Append("WHERE                                               ")
        _StrSql.Append("      EOUTL.NRS_BR_CD    = " & "'" & nrsBrCd & "'   ")
        _StrSql.Append("  AND EOUTL.CUST_CD_L    = " & "'" & custCdL & "'   ")
        _StrSql.Append("  AND EOUTL.CUST_CD_M    = " & "'" & custCdM & "'   ")

  
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ''パラメータの反映
        'For Each obj As Object In Me._SqlPrmList
        '    cmd.Parameters.Add(obj)
        'Next

        MyBase.Logger.WriteSQLLog("LMH510DAC", "SelectMPrt", cmd)

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
    ''' EDI出荷(大)・EDI出荷(中)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)・EDI出荷(中)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH510IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL振り分け処理(2012.11.20)---START---
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Select Case rptId
            Case "LMH512"
                Me._StrSql.Append(LMH510DAC.SQL_SELECT_LMH512)      'SQL構築(印刷データ抽出用 SELECT句)
                Call Me.SetConditionParameterSQL()           'SQL構築(印刷データ抽出項目設定)
                Me._StrSql.Append(LMH510DAC.SQL_FROM_LMH512)        'SQL構築(印刷データ抽出用 FROM句)
                Call Me.SetConditionMasterSQL()              'SQL構築(印刷データ抽出条件設定)
                Me._StrSql.Append(LMH510DAC.SQL_ORDER_BY_LMH512) 'SQL構築(印刷データ抽出用 ORDER BY句 LMH512用)

            Case Else
                Me._StrSql.Append(LMH510DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
                Call Me.SetConditionParameterSQL()           'SQL構築(印刷データ抽出項目設定)
                Me._StrSql.Append(LMH510DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
                Call Me.SetConditionMasterSQL()              'SQL構築(印刷データ抽出条件設定)
                Me._StrSql.Append(LMH510DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)
        End Select
        'SQL振り分け処理(2012.11.20)---END---

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH510DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("NRS_WH_NM", "NRS_WH_NM")
        map.Add("EDI_OUTKA_NO_L", "EDI_OUTKA_NO_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("M_CUST_NM_L", "M_CUST_NM_L")
        map.Add("M_CUST_NM_M", "M_CUST_NM_M")
        map.Add("M_CUST_NM_S", "M_CUST_NM_S")
        map.Add("M_CUST_NM_SS", "M_CUST_NM_SS")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("EDI_OUTKA_PLAN_DATE", "EDI_OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("EDI_ARR_PLAN_DATE", "EDI_ARR_PLAN_DATE")
        map.Add("EDI_ARR_PLAN_TIME", "EDI_ARR_PLAN_TIME")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("CUST_ORD_NO_L", "CUST_ORD_NO_L")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("EDI_DEST_TEL", "EDI_DEST_TEL")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("EDI_REMARK_L", "EDI_REMARK_L")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("EDI_UNSO_ATT", "EDI_UNSO_ATT")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("EDI_DEL_KB", "EDI_DEL_KB")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("L_FREE_C01", "L_FREE_C01")
        map.Add("L_FREE_C02", "L_FREE_C02")
        map.Add("L_FREE_C04", "L_FREE_C04")
        map.Add("L_FREE_C05", "L_FREE_C05")
        map.Add("L_FREE_C06", "L_FREE_C06")
        map.Add("L_FREE_C07", "L_FREE_C07")
        map.Add("EDI_OUTKA_NO_M", "EDI_OUTKA_NO_M")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("OUTKA_HASU", "OUTKA_HASU")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("KB_UT", "KB_UT")
        map.Add("QT_UT", "QT_UT")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("CUST_ORD_NO_M", "CUST_ORD_NO_M")
        map.Add("BUYER_ORD_NO_M", "BUYER_ORD_NO_M")
        map.Add("M_FREE_C02", "M_FREE_C02")
        map.Add("M_FREE_C06", "M_FREE_C06")
        map.Add("M_FREE_C07", "M_FREE_C07")
        map.Add("PROGRESS", "PROGRESS")
        map.Add("SYS_ENT_USER_CD", "SYS_ENT_USER_CD")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_USER_CD", "SYS_UPD_USER_CD")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("PRINT_USER_CD", "PRINT_USER_CD")
        map.Add("PRINT_USER_NM", "PRINT_USER_NM")
        map.Add("DEL_KB", "DEL_KB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        '(2012.03.14) 項目追加 --- START ---
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("FLAG_01", "FLAG_01")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("M_PKG_NB", "M_PKG_NB")
        map.Add("EDI_PKG_NB", "EDI_PKG_NB")
        '(2012.03.14) 項目追加 ---  END  ---
        '(2015.05.28) 項目追加 --- START ---
        map.Add("L_FREE_C03", "L_FREE_C03")
        map.Add("L_FREE_C24", "L_FREE_C24")
        map.Add("L_FREE_C25", "L_FREE_C25")
        '(2012.05.28) 項目追加 ---  END  ---


        If (rptId <> "LMH512") Then
            map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
            map.Add("INPUT_DATE", "INPUT_DATE")
            map.Add("INPUT_TIME", "INPUT_TIME")
        End If


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH510OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionParameterSQL()

        'ユーザーコード(印刷者)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_USER_CD", MyBase.GetUserID(), DBDataType.CHAR))

        'ユーザー名(印刷者)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_USER_NM", MyBase.GetUserName(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '進捗区分
            Dim arr As ArrayList = New ArrayList()

            Dim connectFlg As Boolean = False
            Dim checkFlg As Boolean = False

            Me._StrSql.Append(" ( ")

            '未登録にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB1").ToString()) = False Then

                Me._StrSql.Append(" ((EOUTL.DEL_KB IN ('0','3','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.OUT_FLAG IN ('0','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.JISSEKI_FLAG IN ('0','9'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (EOUTL.DEL_KB = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND M_EDI_CUST.FLAG_08 = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.OUT_FLAG IN ('0','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.JISSEKI_FLAG IN ('0','9')))")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '出荷登録済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB2").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" ((M_EDI_CUST.FLAG_01 IN ('1','2','3','4')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.OUTKA_STATE_KB < '60')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('1','3','4')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.SYS_DEL_FLG = '1')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('0','9')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.OUTKA_STATE_KB IS NOT NULL)")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (LEFT(EOUTL.FREE_C30,2) = '01'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND SUBSTRING(EOUTL.FREE_C30,5,8) NOT IN ('','00000000')))")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績未にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB3").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (((M_EDI_CUST.FLAG_01 IN ('1','2','3')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.OUTKA_STATE_KB >= '60')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '2'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND (EOUTL.SYS_DEL_FLG = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR OUTKAL.SYS_DEL_FLG = '1'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '4'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.SYS_DEL_FLG = '0')")
                Me._StrSql.Append(vbNewLine)
                '2011.10.07 START デュポンEDIデータ即実績作成対応
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '3'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND EOUTL.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND OUTKAL.OUTKA_STATE_KB IS NULL))")
                Me._StrSql.Append(vbNewLine)
                '2011.10.07 END
                Me._StrSql.Append(" AND (EOUTL.JISSEKI_FLAG = '0'))")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績作成済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB4").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EOUTL.JISSEKI_FLAG = '1')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績送信済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB5").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EOUTL.JISSEKI_FLAG = '2')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '赤データにチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB6").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EOUTL.DEL_KB = '2')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '取消のみにチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB8").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EOUTL.DEL_KB = '1')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '進捗区分チェックなしは全件検索
            If checkFlg = False Then

                Me._StrSql.Append(" ((EOUTL.DEL_KB IN ('0','3','2'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '2'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR M_EDI_CUST.FLAG_08 = '1'))")
                Me._StrSql.Append(vbNewLine)

            End If

            Me._StrSql.Append(" ) ")

            '====== ヘッダ項目 ======'

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.WH_CD = @WH_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND EOUTL.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(" AND EOUTL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND EOUTL.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(" AND EOUTL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_CD LIKE @TANTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.DEST_CD LIKE @DEST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'EDI取込日(FROM)
            whereStr = .Item("EDI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.CRT_DATE >= @EDI_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("EDI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.CRT_DATE <= @EDI_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_TO", whereStr, DBDataType.CHAR))
            End If


            '区分格納変数
            Dim kbn As String

            '可変比較対象項目名格納変数
            Dim colNM As String = String.Empty

            'EDI検索日付区分
            kbn = .Item("SEARCH_DATE_KBN").ToString()

            'EDI検索日付区分によって以下分岐
            Select Case kbn

                'Case "01"
                '    colNM = "CRT_DATE"
                Case "01"
                    colNM = "OUTKA_PLAN_DATE"
                Case "02"
                    colNM = "ARR_PLAN_DATE"
                Case Else
                    colNM = String.Empty

            End Select

            If String.IsNullOrEmpty(colNM) = False Then

                'EDI検索日(FROM)
                whereStr = .Item("SEARCH_DATE_FROM").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    '2011.09.25 修正START
                    Select Case kbn
                        'Case "01"
                        '    Me._StrSql.Append(" AND @SEARCH_DATE = EOUTL.")
                        '    Me._StrSql.Append(colNM)
                        '    Me._StrSql.Append(vbNewLine)
                        '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE", whereStr, DBDataType.CHAR))
                        Case "01", "02"
                            Me._StrSql.Append(" AND @SEARCH_DATE_FROM <= ISNULL(OUTKAL.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" ,EOUTL.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" )")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.CHAR))
                        Case Else

                    End Select
                    '2011.09.25 修正END
                End If

                'EDI検索日(TO)
                whereStr = .Item("SEARCH_DATE_TO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then

                    Select Case kbn

                        Case "01", "02"
                            Me._StrSql.Append(" AND @SEARCH_DATE_TO >= ISNULL(OUTKAL.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" ,EOUTL.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" )")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.CHAR))
                        Case Else

                    End Select

                End If

            End If

            '====== スプレッド項目 ======'

            '★★★
            '状態
            whereStr = .Item("JYOTAI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.SYS_DEL_FLG = @JYOTAI_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYOTAI_KB", whereStr, DBDataType.CHAR))
            End If

            '保留
            whereStr = .Item("HORYU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals("3") Then
                    Me._StrSql.Append(" AND EOUTL.DEL_KB = '3' ")
                Else
                    Me._StrSql.Append(" AND EOUTL.DEL_KB <> '3' ")
                End If
            End If
            '★★★

            'オーダー番号
            whereStr = .Item("CUST_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.CUST_ORD_NO LIKE @CUST_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (EOUTL.CUST_NM_L + EOUTL.CUST_NM_M) LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (EOUTL.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(" OR M_DEST.DEST_NM LIKE @DEST_NM)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷時注意事項
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (EOUTL.REMARK LIKE @REMARK")
                Me._StrSql.Append(" OR OUTKAL.REMARK LIKE @REMARK)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '配送時注意事項
            whereStr = .Item("UNSO_ATT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (EOUTL.UNSO_ATT LIKE @UNSO_ATT")
                Me._StrSql.Append(" OR F_UNSO_L.REMARK LIKE @UNSO_ATT)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名（中1）
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_M_FST.GOODS_NM LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_AD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (M_DEST.AD_1 + M_DEST.AD_2 + M_DEST.AD_3 LIKE @DEST_AD")
                Me._StrSql.Append(" OR EOUTL.DEST_AD_1 + EOUTL.DEST_AD_2 + OUTKAL.DEST_AD_3 LIKE @DEST_AD)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_UNSOCO.UNSOCO_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '便区分
            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.BIN_KB = @BIN_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号(大)
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.EDI_CTL_NO LIKE @EDI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '出荷管理番号(大)
            whereStr = .Item("OUTKA_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.OUTKA_CTL_NO LIKE @KANRI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'まとめ番号(大)
            whereStr = .Item("MATOME_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(EOUTL.FREE_C30,4,9) LIKE @MATOME_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '注文番号
            whereStr = .Item("BUYER_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.BUYER_ORD_NO LIKE @BUYER_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷種別
            whereStr = .Item("SYUBETU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.SYUBETU_KB = @SYUBETU_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", whereStr, DBDataType.CHAR))
            End If

            'タリフ分類区分
            whereStr = .Item("UNSO_MOTO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EOUTL.UNSO_TEHAI_KB = @UNSO_MOTO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", whereStr, DBDataType.CHAR))
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
