' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH500    : EDI入荷チェックリスト
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       EINL.NRS_BR_CD                                   AS NRS_BR_CD " & vbNewLine _
                                            & "      , '59'                                             AS PTN_ID    " & vbNewLine _
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
    ''' EDI入荷(大) - EDI入荷(中),荷(大),入荷(中),荷主Ｍ,商品Ｍ,ユーザーＭ,区分Ｍ
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = " FROM                                                               " & vbNewLine _
                                          & " -- EDI入荷(大)                                                     " & vbNewLine _
                                          & "      $LM_TRN$..H_INKAEDI_L EINL                                    " & vbNewLine _
                                          & "      -- EDI入荷(中)                                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..H_INKAEDI_M EINM                    " & vbNewLine _
                                          & "                   ON EINM.NRS_BR_CD         = EINL.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND EINM.EDI_CTL_NO        = EINL.EDI_CTL_NO      " & vbNewLine _
                                          & "      -- 荷主マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                          & "                   ON M_CUST.NRS_BR_CD       = EINL.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_L       = EINL.CUST_CD_L       " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_M       = EINL.CUST_CD_M       " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                          & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                          & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = EINL.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = EINM.NRS_GOODS_CD    " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(EDI入荷の荷主より取得)                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = EINL.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = EINL.CUST_CD_L       " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = EINL.CUST_CD_M       " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = '59'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
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
                                          & "                  AND M_CUSTRPT2.PTN_ID      = '59'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD         =  EINL.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = '59'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine


    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                  " & vbNewLine _
                                           & "	     EINL.NRS_BR_CD     = @NRS_BR_CD  " & vbNewLine _
                                           & "	 AND EINL.CUST_CD_L     = @CUST_CD_L  " & vbNewLine _
                                           & "	 AND EINL.CUST_CD_M     = @CUST_CD_M  " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                       " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID      " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID      " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END            AS RPT_ID              " & vbNewLine _
                                       & "      , EINL.NRS_BR_CD                 AS NRS_BR_CD           " & vbNewLine _
                                       & "      , EINL.NRS_WH_CD                 AS NRS_WH_CD           " & vbNewLine _
                                       & "      , EINL.CUST_CD_L                 AS CUST_CD_L           " & vbNewLine _
                                       & "      , EINL.CUST_CD_M                 AS CUST_CD_M           " & vbNewLine _
                                       & "      , EINL.CUST_NM_L                 AS CUST_NM_L           " & vbNewLine _
                                       & "      , EINL.CUST_NM_M                 AS CUST_NM_M           " & vbNewLine _
                                       & "      , M_CUST.CUST_NM_L               AS M_CUST_NM_L         " & vbNewLine _
                                       & "      , M_CUST.CUST_NM_M               AS M_CUST_NM_M         " & vbNewLine _
                                       & "      , EINL.DEL_KB                    AS EDI_DEL_KB          " & vbNewLine _
                                       & "      , EINL.OUT_FLAG                  AS OUT_FLAG            " & vbNewLine _
                                       & "      , EINL.JISSEKI_FLAG              AS JISSEKI_FLAG        " & vbNewLine _
                                       & "      , EINL.INKA_CTL_NO_L             AS INKA_NO_L           " & vbNewLine _
                                       & "      , EINL.EDI_CTL_NO                AS EDI_INKA_NO_L       " & vbNewLine _
                                       & "      , EINL.INKA_DATE                 AS EDI_INKA_DATE       " & vbNewLine _
                                       & "      , EINL.BUYER_ORD_NO              AS BUYER_ORD_NO_L      " & vbNewLine _
                                       & "      , EINL.OUTKA_FROM_ORD_NO         AS OUTKA_FROM_ORD_NO_L " & vbNewLine _
                                       & "      , ISNULL(INKAM.INKA_NO_M,'')     AS INKA_NO_M           " & vbNewLine _
                                       & "      , EINM.EDI_CTL_NO_CHU            AS EDI_INKA_NO_M       " & vbNewLine _
                                       & "      , EINM.CUST_GOODS_CD             AS CUST_GOODS_CD       " & vbNewLine _
                                       & "      , EINM.GOODS_NM                  AS GOODS_NM            " & vbNewLine _
                                       & "      , EINM.STD_IRIME                 AS STD_IRIME           " & vbNewLine _
                                       & "      , EINM.STD_IRIME_UT              AS STD_IRIME_UT        " & vbNewLine _
                                       & "      , EINM.IRIME                     AS IRIME               " & vbNewLine _
                                       & "      , EINM.IRIME_UT                  AS IRIME_UT            " & vbNewLine _
                                       & "      , EINM.LOT_NO                    AS LOT_NO              " & vbNewLine _
                                       & "      , EINM.SERIAL_NO                 AS SERIAL_NO           " & vbNewLine _
                                       & "      , EINM.NB                        AS NB                  " & vbNewLine _
                                       & "      , EINM.NB_UT                     AS NB_UT               " & vbNewLine _
                                       & "      , EINM.BUYER_ORD_NO              AS BUYER_ORD_NO_M      " & vbNewLine _
                                       & "      , EINM.OUTKA_FROM_ORD_NO         AS OUTKA_FROM_ORD_NO_M " & vbNewLine _
                                       & "      , ISNULL(INKAL.SYS_DEL_FLG,'')   AS SYS_DEL_FLG         " & vbNewLine _
                                       & "      , ISNULL(INKAL.INKA_DATE,'')     AS INKA_DATE           " & vbNewLine _
                                       & "      , ISNULL(INKAL.INKA_STATE_KB,'') AS INKA_STATE_KB       " & vbNewLine _
                                       & "      , ISNULL(KBN1.KBN_NM1,'')        AS INKA_STATE_KB_NM    " & vbNewLine _
                                       & "      , CASE WHEN INKAL.SYS_DEL_FLG IS NULL THEN 'EDI'        " & vbNewLine _
                                       & "             WHEN INKAL.SYS_DEL_FLG ='1'    THEN '入荷取消'   " & vbNewLine _
                                       & "        ELSE ISNULL(KBN1.KBN_NM1,'')                          " & vbNewLine _
                                       & "        END                            AS PROGRESS            " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' EDI入荷(大) - EDI入荷(中),荷(大),入荷(中),荷主Ｍ,商品Ｍ,ユーザーＭ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = " FROM                                                               " & vbNewLine _
                                     & " -- EDI入荷(大)                                                     " & vbNewLine _
                                     & "      $LM_TRN$..H_INKAEDI_L EINL                                    " & vbNewLine _
                                     & "      -- EDI入荷(中)                                                " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..H_INKAEDI_M EINM                    " & vbNewLine _
                                     & "                   ON EINM.NRS_BR_CD         = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND EINM.EDI_CTL_NO        = EINL.EDI_CTL_NO      " & vbNewLine _
                                     & "      -- 入荷(大)                                                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..B_INKA_L INKAL                      " & vbNewLine _
                                     & "                   ON INKAL.NRS_BR_CD        = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND INKAL.INKA_NO_L        = EINL.INKA_CTL_NO_L   " & vbNewLine _
                                     & "      -- 入荷(中)                                                   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_TRN$..B_INKA_M INKAM                      " & vbNewLine _
                                     & "                   ON INKAM.NRS_BR_CD        = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND INKAM.INKA_NO_L        = EINL.INKA_CTL_NO_L   " & vbNewLine _
                                     & "                  AND INKAM.INKA_NO_M        = EINM.EDI_CTL_NO_CHU  " & vbNewLine _
                                     & "      -- 荷主マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = EINL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = EINL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                     & "      -- 商品マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = EINM.NRS_GOODS_CD    " & vbNewLine _
                                     & "      -- 運送会社マスタ                                             " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO M_UNSOCO                   " & vbNewLine _
                                     & "                   ON M_UNSOCO.NRS_BR_CD     = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_UNSOCO.UNSOCO_CD     = EINL.UNSO_CD         " & vbNewLine _
                                     & "                  AND M_UNSOCO.UNSOCO_BR_CD  = EINL.UNSO_BR_CD      " & vbNewLine _
                                     & "      -- 担当者別荷主マスタ (ユーザーで集約)                        " & vbNewLine _
                                     & "      LEFT OUTER JOIN (SELECT M_TCUST.CUST_CD_L AS CUST_CD_L        " & vbNewLine _
                                     & "                            , MIN(USER_CD)      AS USER_CD          " & vbNewLine _
                                     & "                         FROM $LM_MST$..M_TCUST M_TCUST             " & vbNewLine _
                                     & "                        GROUP BY M_TCUST.CUST_CD_L                  " & vbNewLine _
                                     & "                       ) M_TCUST                                    " & vbNewLine _
                                     & "                   ON EINL.CUST_CD_L         = M_TCUST.CUST_CD_L    " & vbNewLine _
                                     & "      -- ユーザーマスタ 入力者                                      " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER ENT_USER                     " & vbNewLine _
                                     & "                   ON ENT_USER.USER_CD       = EINL.SYS_ENT_USER    " & vbNewLine _
                                     & "      -- ユーザーマスタ 更新者                                      " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER UPD_USER                     " & vbNewLine _
                                     & "                   ON UPD_USER.USER_CD       = EINL.SYS_UPD_USER    " & vbNewLine _
                                     & "      -- ユーザーマスタ 担当者                                      " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..S_USER TANTO_USER                   " & vbNewLine _
                                     & "      -- 要望番号1756 yamanaka 2013.03.01 START                     " & vbNewLine _
                                     & "                   ON TANTO_USER.USER_CD     = M_CUST.TANTO_CD      " & vbNewLine _
                                     & "      --             ON TANTO_USER.USER_CD     = M_TCUST.USER_CD    " & vbNewLine _
                                     & "      -- 要望番号1756 yamanaka 2013.03.01 END                       " & vbNewLine _
                                     & "      -- EDI荷主マスタ                                              " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_EDI_CUST M_EDI_CUST               " & vbNewLine _
                                     & "                   ON M_EDI_CUST.NRS_BR_CD   = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.CUST_CD_L   = EINL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.CUST_CD_M   = EINL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.WH_CD       = EINL.NRS_WH_CD       " & vbNewLine _
                                     & "                  AND M_EDI_CUST.INOUT_KB    = '1'                  " & vbNewLine _
                                     & "                  AND M_EDI_CUST.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      -- 区分マスタ <N004>                                          " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..Z_KBN KBN1                          " & vbNewLine _
                                     & "                   ON KBN1.KBN_GROUP_CD      = 'N004'               " & vbNewLine _
                                     & "                  AND KBN1.KBN_CD            = INKAL.INKA_STATE_KB  " & vbNewLine _
                                     & "                  AND KBN1.SYS_DEL_FLG       = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(EDI入荷の荷主より取得)                " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = EINL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = EINL.CUST_CD_L       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = EINL.CUST_CD_M       " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '59'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
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
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '59'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  EINL.NRS_BR_CD      " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '59'                 " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine



    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                     " & vbNewLine _
                                         & "       EINL.CUST_CD_L         " & vbNewLine _
                                         & "     , EINL.CUST_CD_M         " & vbNewLine _
                                         & "     , EINL.INKA_DATE         " & vbNewLine _
                                         & "     , INKAL.INKA_DATE        " & vbNewLine _
                                         & "     , EINL.INKA_CTL_NO_L     " & vbNewLine _
                                         & "     , INKAM.INKA_NO_M        " & vbNewLine _
                                         & "     , EINM.EDI_CTL_NO        " & vbNewLine _
                                         & "     , EINM.EDI_CTL_NO_CHU    " & vbNewLine


    ''' <summary>                             
    ''' LMH501用印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY_LMH501 As String = " ORDER BY                     " & vbNewLine _
                                                & "       EINL.EDI_CTL_NO        " & vbNewLine _
                                                & "     , EINM.EDI_CTL_NO_CHU    " & vbNewLine _
                                                & "     , EINL.CUST_CD_L         " & vbNewLine _
                                                & "     , EINL.CUST_CD_M         " & vbNewLine _
                                                & "     , INKAL.INKA_DATE        " & vbNewLine _
                                                & "     , EINL.INKA_DATE         " & vbNewLine _
                                                & "     , INKAL.INKA_NO_L        " & vbNewLine _
                                                & "     , INKAM.INKA_NO_M        " & vbNewLine



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
        Dim inTbl As DataTable = ds.Tables("LMH500IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH500DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH500DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        Me._StrSql.Append(LMH500DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)
        Call Me.SetConditionPrintPatternMSQL()          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH500DAC", "SelectMPrt", cmd)

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
    ''' EDI入荷(大)・EDI入荷(中)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI入荷(大)・EDI入荷(中)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH500IN")
        'DataSetのM_RPT情報を取得'NOTES 883
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH500DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH500DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        Call Me.SetConditionMasterSQL()              'SQL構築(印刷データ抽出条件設定)
        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMH501" Then 'Notes 883 2012/05/16　開始
            Me._StrSql.Append(LMH500DAC.SQL_ORDER_BY_LMH501)    'SQL構築(印刷データ抽出用 ORDER BY句) 'LMH501の場合 'Notes 883 2012/05/16　開始
        Else
            Me._StrSql.Append(LMH500DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)
        End If


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH500DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("M_CUST_NM_L", "M_CUST_NM_L")
        map.Add("M_CUST_NM_M", "M_CUST_NM_M")
        map.Add("EDI_DEL_KB", "EDI_DEL_KB")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("EDI_INKA_NO_L", "EDI_INKA_NO_L")
        map.Add("EDI_INKA_DATE", "EDI_INKA_DATE")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("EDI_INKA_NO_M", "EDI_INKA_NO_M")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("STD_IRIME", "STD_IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("NB", "NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("BUYER_ORD_NO_M", "BUYER_ORD_NO_M")
        map.Add("OUTKA_FROM_ORD_NO_M", "OUTKA_FROM_ORD_NO_M")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        map.Add("INKA_STATE_KB_NM", "INKA_STATE_KB_NM")
        map.Add("PROGRESS", "PROGRESS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH500OUT")

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
    Private Sub SetConditionMasterSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStr2 As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim connectFlg As Boolean = False
        Dim checkFlg As Boolean = False

        With Me._Row

            Me._StrSql.Append(" ( ")

            '未登録にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB1").ToString()) = False Then
                Me._StrSql.Append(" ((EINL.DEL_KB IN ('0','3','2') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" EINL.OUT_FLAG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" EINL.JISSEKI_FLAG IN ('0','9')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_08 = '1' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" EINL.OUT_FLAG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" EINL.JISSEKI_FLAG IN ('0','9'))) ")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '入荷登録済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB2").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" ((M_EDI_CUST.FLAG_01 IN ('1','2','4') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.SYS_DEL_FLG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.INKA_STATE_KB < '50') ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 IN ('1','4') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.SYS_DEL_FLG = '1') ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 IN ('0','3','9') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.INKA_STATE_KB IS NOT NULL)) ")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '実績未にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB3").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (((M_EDI_CUST.FLAG_01 IN ('1','2','3') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" EINL.SYS_DEL_FLG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.SYS_DEL_FLG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.INKA_STATE_KB >= '50') ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '2' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (EINL.SYS_DEL_FLG = '1' OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.SYS_DEL_FLG = '1')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '3' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (EINL.SYS_DEL_FLG = '0' OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" EINL.OUT_FLAG = '2')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '4' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (EINL.SYS_DEL_FLG = '0' OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" INKAL.SYS_DEL_FLG = '0'))) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND ")
                Me._StrSql.Append(" (EINL.JISSEKI_FLAG = '0')) ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績作成済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB4").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EINL.JISSEKI_FLAG = '1') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If

            '実績送信済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB5").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EINL.JISSEKI_FLAG = '2') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If

            '赤データにチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB6").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EINL.DEL_KB = '2') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If

            '取消のみにチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB8").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (EINL.DEL_KB = '1') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If


            '進捗区分チェックなしは全件検索
            If checkFlg = False Then

                Me._StrSql.Append(" ((EINL.DEL_KB IN ('0','3','2')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (EINL.DEL_KB = '1' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '2' OR")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" M_EDI_CUST.FLAG_08 = '1'))) ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True

            End If


            Me._StrSql.Append(" ) ")

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.NRS_BR_CD = @NRS_BR_CD ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.NRS_WH_CD = @WH_CD ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.CUST_CD_L = @CUST_CD_L ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.CUST_CD_M = @CUST_CD_M ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_CD LIKE @TANTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EDI取込日(FROM)
            whereStr = .Item("TORIKOMI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.CRT_DATE >= @TORIKOMI_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("TORIKOMI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.CRT_DATE <= @TORIKOMI_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '入荷日(FROM)
            whereStr = .Item("INKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ISNULL(INKAL.INKA_DATE,EINL.INKA_DATE) >= @INKA_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日(TO)
            whereStr = .Item("INKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ISNULL(INKAL.INKA_DATE,EINL.INKA_DATE) <= @INKA_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '状態
            whereStr = .Item("JYOTAI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.SYS_DEL_FLG = @JYOTAI_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYOTAI_KB", whereStr, DBDataType.CHAR))
            End If

            '保留
            whereStr = .Item("HORYU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals("3") Then
                    Me._StrSql.Append(" AND EINL.DEL_KB = '3' ")
                Else
                    Me._StrSql.Append(" AND EINL.DEL_KB <> '3' ")
                End If
            End If

            '入荷種別
            whereStr = .Item("INKA_TP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.INKA_TP = @INKA_TP ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", whereStr, DBDataType.CHAR))
            End If

            '入荷区分
            whereStr = .Item("INKA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.INKA_KB = @INKA_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", whereStr, DBDataType.CHAR))
            End If

            'オーダー番号
            whereStr = .Item("OUTKA_FROM_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.OUTKA_FROM_ORD_NO LIKE @OUTKA_FROM_ORD_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M) LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_GOODS.GOODS_NM_1 LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送元区分名
            whereStr = .Item("UNSO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.UNCHIN_KB = @UNSO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_KB", whereStr, DBDataType.CHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_UNSOCO.UNSOCO_NM + '　' + M_UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EDI入荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.EDI_CTL_NO LIKE @EDI_CTL_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '入荷管理番号(大)
            whereStr = .Item("INKA_CTL_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.INKA_CTL_NO_L LIKE @INKA_CTL_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '注文番号
            whereStr = .Item("BUYER_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EINL.BUYER_ORD_NO = @BUYER_ORD_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", whereStr, DBDataType.NVARCHAR))
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
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))  'NVARCHAR⇒VARCHAR
            End If

            '最終更新者
            whereStr = .Item("SYS_UPD_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UPD_USER.USER_NM LIKE @SYS_UPD_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))  'NVARCHAR⇒VARCHAR
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
