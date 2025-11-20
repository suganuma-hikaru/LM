' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH572    : 入荷EDI受信一覧表(大阪_浮間合成)
'  作  成  者       :  甲斐孝貴
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH572DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH572DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       DTL.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , '85'                                             AS PTN_ID    " & vbNewLine _
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
    ''' 浮間EDI受信データHEAD - 浮間EDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>  
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_INKAEDI_DTL_UKM  DTL                             " & vbNewLine _
                                          & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                          & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                          & "      LEFT JOIN (                                                   " & vbNewLine _
                                          & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                          & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                          & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                          & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                          & "                   GROUP BY                                         " & vbNewLine _
                                          & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                          & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                          & "                ) HEDIPRINT                                         " & vbNewLine _
                                          & "             ON HEDIPRINT.NRS_BR_CD  = DTL.NRS_BR_CD                " & vbNewLine _
                                          & "            AND HEDIPRINT.EDI_CTL_NO = DTL.EDI_CTL_NO               " & vbNewLine _
                                          & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                          & "      -- 商品マスタ                                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                          & "                   ON M_GOODS.NRS_BR_CD      = DTL.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_GOODS.GOODS_CD_NRS   = DTL.SAKUIN_CD        " & vbNewLine _
                                          & "      -- 帳票パターンマスタ①(H_INKAEDI_DTL_UKMの荷主より取得)      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                          & "                   ON M_CUSTRPT1.NRS_BR_CD   = DTL.NRS_BR_CD        " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT1.PTN_ID      = '85'                 " & vbNewLine _
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
                                          & "                  AND M_CUSTRPT2.PTN_ID      = '85'                 " & vbNewLine _
                                          & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                          & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                          & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                          & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                          & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                          & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                          & "                   ON MR3.NRS_BR_CD          =  DTL.NRS_BR_CD       " & vbNewLine _
                                          & "                  AND MR3.PTN_ID             = '85'                 " & vbNewLine _
                                          & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                          & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    
    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                           " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID          " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID          " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END        AS RPT_ID                     " & vbNewLine _
                                       & "      , DTL.DEL_KB                  AS DEL_KB                     " & vbNewLine _
                                       & "      , DTL.CRT_DATE                AS CRT_DATE                   " & vbNewLine _
                                       & "      , DTL.FILE_NAME               AS FILE_NAME                  " & vbNewLine _
                                       & "      , DTL.REC_NO                  AS REC_NO                     " & vbNewLine _
                                       & "      , DTL.NRS_BR_CD               AS NRS_BR_CD                  " & vbNewLine _
                                       & "      , DTL.EDI_CTL_NO              AS INKAEDI_NO_L               " & vbNewLine _
                                       & "      , DTL.EDI_CTL_NO_CHU          AS INKAEDI_NO_M               " & vbNewLine _
                                       & "      , DTL.INKA_CTL_NO_L           AS INKA_NO_L                  " & vbNewLine _
                                       & "      , DTL.INKA_CTL_NO_M           AS INKA_NO_M                  " & vbNewLine _
                                       & "      , DTL.PRTFLG                  AS PRTFLG                     " & vbNewLine _
                                       & "      , DTL.CANCEL_FLG              AS CANCEL_FLG                 " & vbNewLine _
                                       & "      , DTL.TOKUI_CD                AS TOKUI_CD                   " & vbNewLine _
                                       & "      , DTL.TOKUI_NM                AS TOKUI_NM                   " & vbNewLine _
                                       & "      , DTL.CHUMON_NO_1             AS CHUMON_NO_1                " & vbNewLine _
                                       & "      , DTL.DENPYO_NO               AS DENPYO_NO                  " & vbNewLine _
                                       & "      , DTL.INKA_BI                 AS INKA_BI                    " & vbNewLine _
                                       & "      , DTL.HINMEI                  AS GOODS_NM                   " & vbNewLine _
                                       & "      , DTL.LOT_NO                  AS LOT_NO                     " & vbNewLine _
                                       & "      , DTL.YOURYO                  AS YOURYO                     " & vbNewLine _
                                       & "      , DTL.KOSU                    AS KOSU                       " & vbNewLine _
                                       & "      , DTL.BASHO                   AS BASHO                      " & vbNewLine _
                                       & "      , DTL.SAKUIN_CD               AS SAKUIN_CD                  " & vbNewLine _
                                       & "      , DTL.TANTO_CD                AS TANTO_CD                   " & vbNewLine _
                                       & "      , DTL.GENKA_BUMON             AS GENKA_BUMON                " & vbNewLine _
                                       & "      , DTL.BIN_NM                  AS BIN_NM                     " & vbNewLine _
                                       & "      , DTL.TEL_NO                  AS TEL_NO                     " & vbNewLine _
                                       & "      , DTL.JUSHO                   AS JUSHO                      " & vbNewLine _
                                       & "      , DTL.SHIKEN_HYO              AS SHIKEN_HYO                 " & vbNewLine _
                                       & "      , DTL.SITEI_DENPYO            AS SITEI_DENPYO               " & vbNewLine _
                                       & "      , DTL.DAIGAEHIN_CD            AS DAIGAEHIN_CD               " & vbNewLine _
                                       & "      , DTL.DAIGAEHIN_NM            AS DAIGAEHIN_NM               " & vbNewLine _
                                       & "      , DTL.HAISO_KB                AS HAISO_KB                   " & vbNewLine _
                                       & "      , DTL.DOKUGEKI                AS DOKUGEKI                   " & vbNewLine _
                                       & "      , DTL.Z_CD                    AS Z_CD                       " & vbNewLine _
                                       & "      , DTL.RUIBETSU                AS RUIBETSU                   " & vbNewLine _
                                       & "      , DTL.CUST_CD                 AS CUST_CD                    " & vbNewLine _
                                       & "      , DTL.YELLOW_CARD_NO          AS YELLOW_CARD_NO             " & vbNewLine _
                                       & "      , DTL.YUBIN_NO                AS YUBIN_NO                   " & vbNewLine _
                                       & "      , DTL.CHUMON_NO_2             AS CHUMON_NO_2                " & vbNewLine _
                                       & "      , DTL.IRAI_SAKI               AS IRAI_SAKI                  " & vbNewLine _
                                       & "      , DTL.RECORD_STATUS           AS RECORD_STATUS              " & vbNewLine _
                                       & "      , ISNULL(MIN(M_GOODS.CUST_CD_S),'')     AS CUST_CD_S        " & vbNewLine _
                                       & "      , ISNULL(M_GOODS_DETAILS.SET_NAIYO,'')  AS FREE_C04         " & vbNewLine _
                                       & "      , SUBSTRING(DTL.BASHO,1,1)    AS SORT_BASHO                 " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 浮間EDI受信データHEAD - 浮間EDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_INKAEDI_DTL_UKM  DTL                             " & vbNewLine _
                                     & " --【Notes】№1007/1008対応 --- START ---                           " & vbNewLine _
                                     & "      -- EDI印刷種別テーブル                                        " & vbNewLine _
                                     & "      LEFT JOIN (                                                   " & vbNewLine _
                                     & "                  SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT           " & vbNewLine _
                                     & "                       , H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                    FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT          " & vbNewLine _
                                     & "                   WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M       " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.PRINT_TP    = '03'             " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                     & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                     & "                   GROUP BY                                         " & vbNewLine _
                                     & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                     & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                     & "                ) HEDIPRINT                                         " & vbNewLine _
                                     & "             ON HEDIPRINT.NRS_BR_CD  = DTL.NRS_BR_CD                " & vbNewLine _
                                     & "            AND HEDIPRINT.EDI_CTL_NO = DTL.EDI_CTL_NO               " & vbNewLine _
                                     & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                     & "      -- 荷主マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = DTL.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = @CUST_CD_L           " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = @CUST_CD_M           " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                     & "      -- 商品マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                   ON DTL.NRS_BR_CD         = M_GOODS.NRS_BR_CD     " & vbNewLine _
                                     & "                  AND left(DTL.SAKUIN_CD,5) = M_GOODS.GOODS_CD_CUST " & vbNewLine _
                                     & "                  AND DTL.YOURYO            = M_GOODS.STD_IRIME_NB  " & vbNewLine _
                                     & "                  AND M_GOODS.SYS_DEL_FLG   = '0'                   " & vbNewLine _
                                     & "      -- 商品明細マスタ                                             " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS_DETAILS M_GOODS_DETAILS            " & vbNewLine _
                                     & "                   ON M_GOODS_DETAILS.NRS_BR_CD    = M_GOODS.NRS_BR_CD	   " & vbNewLine _
                                     & "                  AND M_GOODS_DETAILS.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS  " & vbNewLine _
                                     & "                  AND M_GOODS_DETAILS.SUB_KB       = '02'                  " & vbNewLine _
                                     & "                  AND M_GOODS_DETAILS.SYS_DEL_FLG  = '0'                   " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(H_INKAEDI_DTL_UKMの荷主より取得)      " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = DTL.NRS_BR_CD        " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L           " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M           " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = '85'                 " & vbNewLine _
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
                                     & "                  AND M_CUSTRPT2.PTN_ID      = '85'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  DTL.NRS_BR_CD       " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = '85'                 " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine


    ''' <summary>                             
    ''' 印刷データ抽出用 GROUP BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                           " & vbNewLine _
                                         & "          MR2.PTN_CD                " & vbNewLine _
                                         & "        , MR2.RPT_ID                " & vbNewLine _
                                         & "        , MR1.PTN_CD                " & vbNewLine _
                                         & "        , MR1.RPT_ID                " & vbNewLine _
                                         & "        , MR3.RPT_ID                " & vbNewLine _
                                         & "        , DTL.DEL_KB                " & vbNewLine _
                                         & "        , DTL.CRT_DATE              " & vbNewLine _
                                         & "        , DTL.FILE_NAME             " & vbNewLine _
                                         & "        , DTL.REC_NO                " & vbNewLine _
                                         & "        , DTL.NRS_BR_CD             " & vbNewLine _
                                         & "        , DTL.EDI_CTL_NO            " & vbNewLine _
                                         & "        , DTL.EDI_CTL_NO_CHU        " & vbNewLine _
                                         & "        , DTL.INKA_CTL_NO_L         " & vbNewLine _
                                         & "        , DTL.INKA_CTL_NO_M         " & vbNewLine _
                                         & "        , DTL.PRTFLG                " & vbNewLine _
                                         & "        , DTL.CANCEL_FLG            " & vbNewLine _
                                         & "        , DTL.TOKUI_CD              " & vbNewLine _
                                         & "        , DTL.TOKUI_NM              " & vbNewLine _
                                         & "        , DTL.CHUMON_NO_1           " & vbNewLine _
                                         & "        , DTL.DENPYO_NO             " & vbNewLine _
                                         & "        , DTL.INKA_BI               " & vbNewLine _
                                         & "        , DTL.HINMEI                " & vbNewLine _
                                         & "        , DTL.LOT_NO                " & vbNewLine _
                                         & "        , DTL.YOURYO                " & vbNewLine _
                                         & "        , DTL.KOSU                  " & vbNewLine _
                                         & "        , DTL.BASHO                 " & vbNewLine _
                                         & "        , DTL.SAKUIN_CD             " & vbNewLine _
                                         & "        , DTL.TANTO_CD              " & vbNewLine _
                                         & "        , DTL.GENKA_BUMON           " & vbNewLine _
                                         & "        , DTL.BIN_NM                " & vbNewLine _
                                         & "        , DTL.TEL_NO                " & vbNewLine _
                                         & "        , DTL.JUSHO                 " & vbNewLine _
                                         & "        , DTL.SHIKEN_HYO            " & vbNewLine _
                                         & "        , DTL.SITEI_DENPYO          " & vbNewLine _
                                         & "        , DTL.DAIGAEHIN_CD          " & vbNewLine _
                                         & "        , DTL.DAIGAEHIN_NM          " & vbNewLine _
                                         & "        , DTL.HAISO_KB              " & vbNewLine _
                                         & "        , DTL.DOKUGEKI              " & vbNewLine _
                                         & "        , DTL.Z_CD                  " & vbNewLine _
                                         & "        , DTL.RUIBETSU              " & vbNewLine _
                                         & "        , DTL.CUST_CD               " & vbNewLine _
                                         & "        , DTL.YELLOW_CARD_NO        " & vbNewLine _
                                         & "        , DTL.YUBIN_NO              " & vbNewLine _
                                         & "        , DTL.CHUMON_NO_2           " & vbNewLine _
                                         & "        , DTL.IRAI_SAKI             " & vbNewLine _
                                         & "        , DTL.RECORD_STATUS         " & vbNewLine _
                                         & "        , M_GOODS_DETAILS.SET_NAIYO " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                       " & vbNewLine _
                                         & "       DTL.CRT_DATE             " & vbNewLine _
                                         & "     , DTL.FILE_NAME            " & vbNewLine _
                                         & "     , SORT_BASHO               " & vbNewLine _
                                         & "     , DTL.REC_NO               " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMH572IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH572DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH572DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then    'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定)
        End If                                          'Notes 1061 2012/05/15　終了

        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH572DAC", "SelectMPrt", cmd)

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
    ''' 浮間EDI受信データ(HEAD)・浮間EDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>浮間EDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH572IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH572DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH572DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then 'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合     '未出力・両方(出力済、未出力併せて)
        Else
            Call Me.SetConditionMasterSQL()              'SQL構築(印刷データ抽出条件設定)'未出力・両方(出力済、未出力併せて)
        End If                                       'Notes 1061 2012/05/15　終了
        Me._StrSql.Append(LMH572DAC.SQL_GROUP_BY)    'SQL構築(印刷データ抽出用 GROUP BY句)
        Me._StrSql.Append(LMH572DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH572DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKAEDI_NO_L", "INKAEDI_NO_L")
        map.Add("INKAEDI_NO_M", "INKAEDI_NO_M")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("TOKUI_NM", "TOKUI_NM")
        map.Add("CHUMON_NO_1", "CHUMON_NO_1")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("INKA_BI", "INKA_BI")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("YOURYO", "YOURYO")
        map.Add("KOSU", "KOSU")
        map.Add("BASHO", "BASHO")
        map.Add("SAKUIN_CD", "SAKUIN_CD")
        map.Add("TANTO_CD", "TANTO_CD")
        map.Add("GENKA_BUMON", "GENKA_BUMON")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("TEL_NO", "TEL_NO")
        map.Add("JUSHO", "JUSHO")
        map.Add("SHIKEN_HYO", "SHIKEN_HYO")
        map.Add("SITEI_DENPYO", "SITEI_DENPYO")
        map.Add("DAIGAEHIN_CD", "DAIGAEHIN_CD")
        map.Add("DAIGAEHIN_NM", "DAIGAEHIN_NM")
        map.Add("HAISO_KB", "HAISO_KB")
        map.Add("DOKUGEKI", "DOKUGEKI")
        map.Add("Z_CD", "Z_CD")
        map.Add("RUIBETSU", "RUIBETSU")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("YELLOW_CARD_NO", "YELLOW_CARD_NO")
        map.Add("YUBIN_NO", "YUBIN_NO")
        map.Add("CHUMON_NO_2", "CHUMON_NO_2")
        map.Add("IRAI_SAKI", "IRAI_SAKI")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("SORT_BASHO", "SORT_BASHO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH572OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL(ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me._Row.Item("INOUT_KB").ToString(), DBDataType.CHAR)) 'Notes1007 20120509

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
                Me._StrSql.Append(" DTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            ''倉庫コード
            'whereStr = .Item("WH_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND DTL.NRS_WH_CD = @WH_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            'End If

            ''荷主コード(大)
            'whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND DTL.CUST_CD_L = @CUST_CD_L")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            ''荷主コード(中)
            'whereStr = .Item("CUST_CD_M").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND DTL.CUST_CD_M = @CUST_CD_M")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            'End If

            'EDI取込日(FROM)
            whereStr = .Item("CRT_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.CRT_DATE >= @CRT_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("CRT_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.CRT_DATE <= @CRT_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            ''入出荷区分
            'whereStr = .Item("INOUT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND DTL.INOUT_KB = @INOUT_KB")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))
            'End If

            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            ''プリントフラグ
            'whereStr = .Item("PRTFLG").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND DTL.PRTFLG = @PRTFLG")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            'End If

            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う ---  END  ---

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
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
                Me._StrSql.Append(" DTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.EDI_CTL_NO = @EDI_CTL_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            ''プリントフラグ
            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---

            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))
            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
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