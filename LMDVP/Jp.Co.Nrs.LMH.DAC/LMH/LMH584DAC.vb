' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : 出荷管理
'  プログラムID     :  LMH584    : 納品書(千葉_ロンザ納品送り状)
'  作  成  者       :  渡部 剛
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH584DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH584DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                       " & vbNewLine _
                                            & "        OUTEDIL.NRS_BR_CD                                AS NRS_BR_CD  " & vbNewLine _
                                            & "      , 'B7'                                             AS PTN_ID     " & vbNewLine _
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
    Private Const SQL_SELECT_DATA As String = " SELECT                                                         " & vbNewLine _
                                            & "       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID         " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID         " & vbNewLine _
                                            & "       ELSE MR3.RPT_ID                                          " & vbNewLine _
                                            & "       END                       AS RPT_ID                      " & vbNewLine _
                                            & "     , OUTEDIL.CRT_DATE          AS CRT_DATE                    " & vbNewLine _
                                            & "     , OUTEDIL.NRS_BR_CD         AS NRS_BR_CD                   " & vbNewLine _
                                            & "     , M_NRS_BR.NRS_BR_NM        AS NRS_BR_NM                   " & vbNewLine _
                                            & "     , M_NRS_BR.TEL              AS NRS_BR_TEL                  " & vbNewLine _
                                            & "     , M_NRS_BR.FAX              AS NRS_BR_FAX                  " & vbNewLine _
                                            & "     , M_CUST.CUST_NM_L          AS CUST_NM_L                   " & vbNewLine _
                                            & "     , M_CUST.AD_1               AS CUST_AD_1                   " & vbNewLine _
                                            & "     , M_CUST.TEL                AS CUST_TEL                    " & vbNewLine _
                                            & "     , OUTEDIL.CUST_CD_L         AS CUST_CD_L                   " & vbNewLine _
                                            & "     , OUTEDIL.CUST_CD_M         AS CUST_CD_M                   " & vbNewLine _
                                            & "     , OUTEDIL.EDI_CTL_NO        AS L_EDI_CTL_NO                " & vbNewLine _
                                            & "     , OUTEDIL.ARR_PLAN_DATE     AS ARR_PLAN_DATE               " & vbNewLine _
                                            & "     , OUTEDIL.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE             " & vbNewLine _
                                            & "     , OUTEDIL.CUST_ORD_NO       AS CUST_ORD_NO                 " & vbNewLine _
                                            & "     , OUTEDIL.OUTKA_CTL_NO      AS OUTKA_CTL_NO                " & vbNewLine _
                                            & "     , M_DEST.DEST_NM            AS DEST_NM                     " & vbNewLine _
                                            & "     , M_DEST.AD_1               AS DEST_AD_1                   " & vbNewLine _
                                            & "     , M_DEST.AD_2               AS DEST_AD_2                   " & vbNewLine _
                                            & "     , M_DEST.TEL                AS DEST_TEL                    " & vbNewLine _
                                            & "     , OUTEDIM.EDI_CTL_NO        AS M_EDI_CTL_NO                " & vbNewLine _
                                            & "     , OUTEDIM.EDI_CTL_NO_CHU    AS EDI_CTL_NO_CHU              " & vbNewLine _
                                            & "     , OUTEDIM.BUYER_ORD_NO_DTL  AS BUYER_ORD_NO_DTL            " & vbNewLine _
                                            & "     , OUTEDIM.CUST_GOODS_CD     AS CUST_GOODS_CD               " & vbNewLine _
                                            & "     , OUTEDIM.GOODS_NM          AS GOODS_NM                    " & vbNewLine _
                                            & "     , OUTEDIM.LOT_NO            AS LOT_NO                      " & vbNewLine _
                                            & "     , OUTEDIM.OUTKA_TTL_NB      AS OUTKA_TTL_NB                " & vbNewLine _
                                            & "     , OUTEDIM.SET_KB            AS SET_KB                      " & vbNewLine _
                                            & "     , M_GOODS.ONDO_KB           AS ONDO_KB                     " & vbNewLine _
                                            & "     , M_GOODS.ONDO_MX           AS ONDO_MX                     " & vbNewLine _
                                            & "     , KBN.KBN_CD                AS KBN_CD                      " & vbNewLine _
                                            & "     , KBN.KBN_NM1               AS KBN_NM_1                    " & vbNewLine _
                                            & "     , OUTEDIL.DEST_CD           AS DEST_CD                     " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C01          AS L_FREE_C01                  " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C02          AS L_FREE_C02                  " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C03          AS L_FREE_C03                  " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C04          AS L_FREE_C04                  " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C20          AS L_FREE_C20                  " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C21          AS L_FREE_C21                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C01          AS M_FREE_C01                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C02          AS M_FREE_C02                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C03          AS M_FREE_C03                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C04          AS M_FREE_C04                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C05          AS M_FREE_C05                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C06          AS M_FREE_C06                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C07          AS M_FREE_C07                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C08          AS M_FREE_C08                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C09          AS M_FREE_C09                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C10          AS M_FREE_C10                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C11          AS M_FREE_C11                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C12          AS M_FREE_C12                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C13          AS M_FREE_C13                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C21          AS M_FREE_C21                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C22          AS M_FREE_C22                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C25          AS M_FREE_C25                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C26          AS M_FREE_C26                  " & vbNewLine _
                                            & "     , OUTEDIM.FREE_C27          AS M_FREE_C27                  " & vbNewLine _
                                            & "     , OUTEDIM.NRS_GOODS_CD      AS NRS_GOODS_CD                " & vbNewLine _
                                            & "     , DTL_LNZ.DELIVERY_NO       AS DELIVERY_NO                 " & vbNewLine _
                                            & "     , DTL_LNZ.GOODS_DOKU_KB     AS GOODS_DOKU_KB               " & vbNewLine _
                                            & "     , DTL_LNZ.GOODS_ONDO_KB     AS GOODS_ONDO_KB               " & vbNewLine _
                                            & "     , DTL_LNZ.GOODS_ONDO_MX     AS GOODS_ONDO_MX               " & vbNewLine _
                                            & "     , OUTEDIL.FREE_C06          AS L_FREE_C06 --2013.02.06追加 " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = " --EDI出荷(大)                                                                         " & vbNewLine _
                                     & " FROM $LM_TRN$..H_OUTKAEDI_L_PRT_LNZ OUTEDIL             --2013.02.05修正             " & vbNewLine _
                                     & "       --EDI出荷M                                                                      " & vbNewLine _
                                     & "      INNER JOIN $LM_TRN$..H_OUTKAEDI_M_PRT_LNZ OUTEDIM  --2013.02.05修正              " & vbNewLine _
                                     & "              ON  OUTEDIM.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                     & "             AND OUTEDIM.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                 " & vbNewLine _
                                     & "             AND OUTEDIM.EDI_CTL_NO = OUTEDIL.EDI_CTL_NO                               " & vbNewLine _
                                     & "       --荷主M                                                                         " & vbNewLine _
                                     & "      INNER JOIN $LM_MST$..M_CUST M_CUST                                               " & vbNewLine _
                                     & "              ON M_CUST.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                  " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_L = OUTEDIL.CUST_CD_L                                  " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_M = OUTEDIL.CUST_CD_M                                  " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_S = '00'                                               " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_SS = '00'                                              " & vbNewLine _
                                     & "       --    AND M_CUST.OYA_SEIQTO_CD = OUTEDIL.FREE_C20                               " & vbNewLine _
                                     & "       --営業所Ｍ                                                                      " & vbNewLine _
                                     & "      INNER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                                           " & vbNewLine _
                                     & "              ON M_NRS_BR.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                " & vbNewLine _
                                     & "             AND M_NRS_BR.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                     & "     --届先M                                                                           " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                                          " & vbNewLine _
                                     & "              ON M_DEST.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                     & "             AND M_DEST.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                  " & vbNewLine _
                                     & "             AND M_DEST.CUST_CD_L = OUTEDIL.CUST_CD_L                                  " & vbNewLine _
                                     & "             AND M_DEST.DEST_CD = OUTEDIL.DEST_CD                                      " & vbNewLine _
                                     & " --▽▽2013.20.07修正▽▽                                                              " & vbNewLine _
                                     & " --商品Ｍ                                                                              " & vbNewLine _
                                     & " LEFT JOIN (                                                                           " & vbNewLine _
                                     & "             SELECT MIN(NRS_BR_CD)     AS NRS_BR_CD                                    " & vbNewLine _
                                     & "                   ,MIN(GOODS_CD_NRS)  AS GOODS_CD_NRS                                 " & vbNewLine _
                                     & "                   ,MIN(CUST_CD_L)     AS CUST_CD_L                                    " & vbNewLine _
                                     & "                   ,MIN(CUST_CD_M)     AS CUST_CD_M                                    " & vbNewLine _
                                     & "                   ,MIN(ONDO_KB)       AS ONDO_KB                                      " & vbNewLine _
                                     & "                   ,MIN(ONDO_MX)       AS ONDO_MX                                      " & vbNewLine _
                                     & "                   ,MIN(DOKU_KB)       AS DOKU_KB                                      " & vbNewLine _
                                     & "                   ,GOODS_CD_CUST      --商品コード毎の最小値のみ取得                  " & vbNewLine _
                                     & "             FROM $LM_MST$..M_GOODS                                                    " & vbNewLine _
                                     & " 			 WHERE NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                                     & "               AND CUST_CD_L = @CUST_CD_L    --OUTEDIL.CUST_CD_L                       " & vbNewLine _
                                     & "               AND CUST_CD_M = @CUST_CD_M    --OUTEDIL.CUST_CD_M                       " & vbNewLine _
                                     & "               AND SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "             GROUP BY                                                                  " & vbNewLine _
                                     & "                   GOODS_CD_CUST                                                       " & vbNewLine _
                                     & "            ) AS M_GOODS ON                                                            " & vbNewLine _
                                     & "                 M_GOODS.GOODS_CD_CUST = OUTEDIM.CUST_GOODS_CD                         " & vbNewLine _
                                     & " --▲▲2013.02.07修正▲▲                                                              " & vbNewLine _
                                     & "       --区分MT                                                                        " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..Z_KBN KBN                                             " & vbNewLine _
                                     & "             ON KBN.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "            AND KBN.KBN_GROUP_CD = 'G001'                                              " & vbNewLine _
                                     & "            AND KBN.KBN_CD = M_GOODS.DOKU_KB                                           " & vbNewLine _
                                     & "       --ロンザ明細                                                                    " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_LNZ AS DTL_LNZ                         " & vbNewLine _
                                     & "             ON DTL_LNZ.CRT_DATE =  OUTEDIL.CRT_DATE                                   " & vbNewLine _
                                     & "            AND DTL_LNZ.FILE_NAME = OUTEDIL.FREE_C01                                   " & vbNewLine _
                                     & "            AND DTL_LNZ.REC_NO = OUTEDIM.FREE_C13                                      " & vbNewLine _
                                     & "       -- EDI印刷種別テーブル                                                          " & vbNewLine _
                                     & "       LEFT JOIN (                                                                     " & vbNewLine _
                                     & "                   SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                             " & vbNewLine _
                                     & "                        , H_EDI_PRINT.NRS_BR_CD                                        " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                       " & vbNewLine _
                                     & "                      --, H_EDI_PRINT.DENPYO_NO            --★2013.0207条件より除外   " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                            " & vbNewLine _
                                     & "                    WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.PRINT_TP    = '10'                               " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                          " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "                    GROUP BY                                                           " & vbNewLine _
                                     & "                          H_EDI_PRINT.NRS_BR_CD                                        " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                       " & vbNewLine _
                                     & "                     -- , H_EDI_PRINT.DENPYO_NO             --★2013.0207条件より除外  " & vbNewLine _
                                     & "                 ) HEDIPRINT                                                           " & vbNewLine _
                                     & "              ON HEDIPRINT.NRS_BR_CD  = OUTEDIL.NRS_BR_CD                              " & vbNewLine _
                                     & "             AND HEDIPRINT.EDI_CTL_NO = OUTEDIL.FREE_C06                               " & vbNewLine _
                                     & "           --AND HEDIPRINT.DENPYO_NO  = OUTEDIL.CUST_ORD_NO --★2013.0207条件より除外  " & vbNewLine _
                                     & "        -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)                    " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                                " & vbNewLine _
                                     & "                     ON M_CUSTRPT1.NRS_BR_CD   = OUTEDIL.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_L   = OUTEDIL.CUST_CD_L  --修正             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_M   = OUTEDIL.CUST_CD_M  --修正             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_S   = '00'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.PTN_ID      = 'B7'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                                           " & vbNewLine _
                                     & "                     ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD                  " & vbNewLine _
                                     & "                    AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID                     " & vbNewLine _
                                     & "                    AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD                     " & vbNewLine _
                                     & "                    AND MR1.SYS_DEL_FLG        = '0'                                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ②(商品マスタより)                                        " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                                " & vbNewLine _
                                     & "                     ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_S   = '00'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.PTN_ID      = 'B7'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                                           " & vbNewLine _
                                     & "                     ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD                  " & vbNewLine _
                                     & "                    AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID                     " & vbNewLine _
                                     & "                    AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD                     " & vbNewLine _
                                     & "                    AND MR2.SYS_DEL_FLG        = '0'                                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ③<存在しない場合の帳票パターン取得>                      " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT MR3                                            " & vbNewLine _
                                     & "                     ON MR3.NRS_BR_CD          = OUTEDIL.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND MR3.PTN_ID             = 'B7'                                  " & vbNewLine _
                                     & "                    AND MR3.STANDARD_FLAG      = '01'                                  " & vbNewLine _
                                     & "                    AND MR3.SYS_DEL_FLG        = '0'                                   " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM1 As String = " --EDI出荷(大)                                                                        " & vbNewLine _
                                     & " FROM $LM_TRN$..H_OUTKAEDI_L_PRT_LNZ OUTEDIL             --2013.02.05修正              " & vbNewLine _
                                     & "       --EDI出荷M                                                                      " & vbNewLine _
                                     & "      INNER JOIN $LM_TRN$..H_OUTKAEDI_M_PRT_LNZ OUTEDIM  --2013.02.05修正              " & vbNewLine _
                                     & "              ON  OUTEDIM.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                     & "             AND OUTEDIM.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                 " & vbNewLine _
                                     & "             AND OUTEDIM.EDI_CTL_NO = OUTEDIL.EDI_CTL_NO                               " & vbNewLine _
                                     & "       --荷主M                                                                         " & vbNewLine _
                                     & "      INNER JOIN $LM_MST$..M_CUST M_CUST                                               " & vbNewLine _
                                     & "              ON M_CUST.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                  " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_L = OUTEDIL.CUST_CD_L                                  " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_M = OUTEDIL.CUST_CD_M                                  " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_S = '00'                                               " & vbNewLine _
                                     & "             AND M_CUST.CUST_CD_SS = '00'                                              " & vbNewLine _
                                     & "       --    AND M_CUST.OYA_SEIQTO_CD = OUTEDIL.FREE_C20                               " & vbNewLine _
                                     & "       --営業所Ｍ                                                                      " & vbNewLine _
                                     & "      INNER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                                           " & vbNewLine _
                                     & "              ON M_NRS_BR.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                " & vbNewLine _
                                     & "             AND M_NRS_BR.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                     & "     --届先M                                                                           " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                                          " & vbNewLine _
                                     & "              ON M_DEST.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                     & "             AND M_DEST.NRS_BR_CD = OUTEDIL.NRS_BR_CD                                  " & vbNewLine _
                                     & "             AND M_DEST.CUST_CD_L = OUTEDIL.CUST_CD_L                                  " & vbNewLine _
                                     & "             AND M_DEST.DEST_CD = OUTEDIL.DEST_CD                                      " & vbNewLine _
                                     & " --▽▽2013.20.07修正▽▽                                                              " & vbNewLine _
                                     & " --商品Ｍ                                                                              " & vbNewLine _
                                     & " LEFT JOIN (                                                                           " & vbNewLine _
                                     & "             SELECT MIN(NRS_BR_CD)     AS NRS_BR_CD                                    " & vbNewLine _
                                     & "                   ,MIN(GOODS_CD_NRS)  AS GOODS_CD_NRS                                 " & vbNewLine _
                                     & "                   ,MIN(CUST_CD_L)     AS CUST_CD_L                                    " & vbNewLine _
                                     & "                   ,MIN(CUST_CD_M)     AS CUST_CD_M                                    " & vbNewLine _
                                     & "                   ,MIN(ONDO_KB)       AS ONDO_KB                                      " & vbNewLine _
                                     & "                   ,MIN(ONDO_MX)       AS ONDO_MX                                      " & vbNewLine _
                                     & "                   ,MIN(DOKU_KB)       AS DOKU_KB                                      " & vbNewLine _
                                     & "                   ,GOODS_CD_CUST      --商品コード毎の最小値のみ取得                  " & vbNewLine _
                                     & "             FROM $LM_MST$..M_GOODS                                                    " & vbNewLine _
                                     & " 			 WHERE NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
                                     & "               AND CUST_CD_L = @CUST_CD_L --OUTEDIL.CUST_CD_L                          " & vbNewLine _
                                     & "               AND CUST_CD_M = @CUST_CD_M --OUTEDIL.CUST_CD_M                          " & vbNewLine _
                                     & "               AND SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                     & "             GROUP BY                                                                  " & vbNewLine _
                                     & "                   GOODS_CD_CUST                                                       " & vbNewLine _
                                     & "            ) AS M_GOODS ON                                                            " & vbNewLine _
                                     & "                 M_GOODS.GOODS_CD_CUST = OUTEDIM.CUST_GOODS_CD                         " & vbNewLine _
                                     & " --▲▲2013.02.07修正▲▲                                                              " & vbNewLine _
                                     & "       --区分MT                                                                        " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..Z_KBN KBN                                             " & vbNewLine _
                                     & "             ON KBN.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                     & "            AND KBN.KBN_GROUP_CD = 'G001'                                              " & vbNewLine _
                                     & "            AND KBN.KBN_CD = M_GOODS.DOKU_KB                                           " & vbNewLine _
                                     & "       --ロンザ明細                                                                    " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_LNZ AS DTL_LNZ                         " & vbNewLine _
                                     & "             ON DTL_LNZ.CRT_DATE =  OUTEDIL.CRT_DATE                                   " & vbNewLine _
                                     & "            AND DTL_LNZ.FILE_NAME = OUTEDIL.FREE_C01                                   " & vbNewLine _
                                     & "            AND DTL_LNZ.REC_NO = OUTEDIM.FREE_C13                                      " & vbNewLine _
                                     & "       -- EDI印刷種別テーブル                                                          " & vbNewLine _
                                     & "       LEFT JOIN (                                                                     " & vbNewLine _
                                     & "                   SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                             " & vbNewLine _
                                     & "                        , H_EDI_PRINT.NRS_BR_CD                                        " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                       " & vbNewLine _
                                     & "                      --, H_EDI_PRINT.DENPYO_NO              --★2013.02.07条件より除外" & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                            " & vbNewLine _
                                     & "                    WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.PRINT_TP    = '10'                               " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                          " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "                    GROUP BY                                                           " & vbNewLine _
                                     & "                          H_EDI_PRINT.NRS_BR_CD                                        " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO             --★2013.02.07条件より除外" & vbNewLine _
                                     & "                      --, H_EDI_PRINT.DENPYO_NO                                        " & vbNewLine _
                                     & "                 ) HEDIPRINT                                                           " & vbNewLine _
                                     & "              ON HEDIPRINT.NRS_BR_CD  = OUTEDIL.NRS_BR_CD                              " & vbNewLine _
                                     & "             AND HEDIPRINT.EDI_CTL_NO = OUTEDIL.EDI_CTL_NO                             " & vbNewLine _
                                     & "           --AND HEDIPRINT.DENPYO_NO  = OUTEDIL.CUST_ORD_NO  --★2013.02.07条件より除外" & vbNewLine _
                                     & "        -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)                    " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                                " & vbNewLine _
                                     & "                     ON M_CUSTRPT1.NRS_BR_CD   = OUTEDIL.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_L   = OUTEDIL.CUST_CD_L  --修正             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_M   = OUTEDIL.CUST_CD_M  --修正             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_S   = '00'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.PTN_ID      = 'B7'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                                           " & vbNewLine _
                                     & "                     ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD                  " & vbNewLine _
                                     & "                    AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID                     " & vbNewLine _
                                     & "                    AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD                     " & vbNewLine _
                                     & "                    AND MR1.SYS_DEL_FLG        = '0'                                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ②(商品マスタより)                                        " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                                " & vbNewLine _
                                     & "                     ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_S   = '00'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.PTN_ID      = 'B7'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                                           " & vbNewLine _
                                     & "                     ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD                  " & vbNewLine _
                                     & "                    AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID                     " & vbNewLine _
                                     & "                    AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD                     " & vbNewLine _
                                     & "                    AND MR2.SYS_DEL_FLG        = '0'                                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ③<存在しない場合の帳票パターン取得>                      " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT MR3                                            " & vbNewLine _
                                     & "                     ON MR3.NRS_BR_CD          = OUTEDIL.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND MR3.PTN_ID             = 'B7'                                  " & vbNewLine _
                                     & "                    AND MR3.STANDARD_FLAG      = '01'                                  " & vbNewLine _
                                     & "                    AND MR3.SYS_DEL_FLG        = '0'                                   " & vbNewLine



    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                          " & vbNewLine _
                                         & "      OUTEDIL.OUTKA_PLAN_DATE     " & vbNewLine _
                                         & "    , OUTEDIM.FREE_C12            " & vbNewLine _
                                         & "    , DTL_LNZ.DELIVERY_NO         " & vbNewLine _
                                         & "    , OUTEDIL.DEST_CD             " & vbNewLine _
                                         & "    , OUTEDIM.FREE_C13            " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMH584IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH584DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        'Me._StrSql.Append(LMH584DAC.SQL_FROM)             'SQL構築(データ抽出用From句)--分岐前処理

        Select Case Me._Row.Item("PRTFLG").ToString
            Case "0"
                '未出力の場合
                Me._StrSql.Append(LMH584DAC.SQL_FROM)    'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()
            Case "1"
                '出力済の場合
                'Me._StrSql.Append(LMH584DAC.SQL_WHERE)   'SQL構築(データ抽出用Where句)
                Me._StrSql.Append(LMH584DAC.SQL_FROM)     'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL_OUT()
            Case "2"
                'ロンザ専用画面の場合
                Me._StrSql.Append(LMH584DAC.SQL_FROM1)     'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL_OUT()
        End Select

        'If Me._Row.Item("PRTFLG").ToString = "0" Then
        '    '未出力の場合
        '    Call Me.SetConditionMasterSQL()
        'Else
        '    '出力済の場合
        '    Call Me.SetConditionMasterSQL_OUT()
        'End If
        Call Me.SetConditionPrintPatternMSQL()          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH584DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMH584IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH584DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        'Me._StrSql.Append(LMH584DAC.SQL_FROM)             'SQL構築(データ抽出用From句)--分岐前処理

        Select Case Me._Row.Item("PRTFLG").ToString
            Case "0"
                '未出力の場合
                Me._StrSql.Append(LMH584DAC.SQL_FROM)    'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()
            Case "1"
                '出力済の場合
                'Me._StrSql.Append(LMH584DAC.SQL_WHERE)   'SQL構築(データ抽出用Where句)
                Me._StrSql.Append(LMH584DAC.SQL_FROM)     'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL_OUT()
            Case "2"
                'ロンザ専用画面の場合
                Me._StrSql.Append(LMH584DAC.SQL_FROM1)     'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL_OUT()
        End Select

        Me._StrSql.Append(LMH584DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        Call Me.SetConditionPrintPatternMSQL()            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH584DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("NRS_BR_TEL", "NRS_BR_TEL")
        map.Add("NRS_BR_FAX", "NRS_BR_FAX")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_AD_1", "CUST_AD_1")
        map.Add("CUST_TEL", "CUST_TEL")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("L_EDI_CTL_NO", "L_EDI_CTL_NO")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("M_EDI_CTL_NO", "M_EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("SET_KB", "SET_KB")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_NM_1", "KBN_NM_1")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("L_FREE_C01", "L_FREE_C01")
        map.Add("L_FREE_C02", "L_FREE_C02")
        map.Add("L_FREE_C03", "L_FREE_C03")
        map.Add("L_FREE_C04", "L_FREE_C04")
        map.Add("L_FREE_C20", "L_FREE_C20")
        map.Add("L_FREE_C21", "L_FREE_C21")
        map.Add("M_FREE_C01", "M_FREE_C01")
        map.Add("M_FREE_C02", "M_FREE_C02")
        map.Add("M_FREE_C03", "M_FREE_C03")
        map.Add("M_FREE_C04", "M_FREE_C04")
        map.Add("M_FREE_C05", "M_FREE_C05")
        map.Add("M_FREE_C06", "M_FREE_C06")
        map.Add("M_FREE_C07", "M_FREE_C07")
        map.Add("M_FREE_C08", "M_FREE_C08")
        map.Add("M_FREE_C09", "M_FREE_C09")
        map.Add("M_FREE_C10", "M_FREE_C10")
        map.Add("M_FREE_C11", "M_FREE_C11")
        map.Add("M_FREE_C12", "M_FREE_C12")
        map.Add("M_FREE_C13", "M_FREE_C13")
        map.Add("M_FREE_C21", "M_FREE_C21")
        map.Add("M_FREE_C22", "M_FREE_C22")
        map.Add("M_FREE_C25", "M_FREE_C25")
        map.Add("M_FREE_C26", "M_FREE_C26")
        map.Add("M_FREE_C27", "M_FREE_C27")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("DELIVERY_NO", "DELIVERY_NO")
        map.Add("GOODS_DOKU_KB", "GOODS_DOKU_KB")
        map.Add("GOODS_ONDO_KB", "GOODS_ONDO_KB")
        map.Add("GOODS_ONDO_MX", "GOODS_ONDO_MX")
        map.Add("L_FREE_C06", "L_FREE_C06")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH584OUT")

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
                Me._StrSql.Append(" OUTEDIL.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            ''EDI取込日(FROM)
            'whereStr = .Item("CRT_DATE_FROM").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND OUTEDIL.CRT_DATE >= @CRT_DATE_FROM ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            'End If

            ''EDI取込日(TO)
            'whereStr = .Item("CRT_DATE_TO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND OUTEDIL.CRT_DATE <= @CRT_DATE_TO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            'End If

            'EDI出荷予定日(FROM)
            whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI出荷予定日(TO)
            whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If
            'プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
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

        'プリントフラグ用空変数
        Dim Printflg As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" OUTEDIL.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.CUST_CD_L = @CUST_CD_L")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.CUST_CD_M = @CUST_CD_M")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            Printflg = .Item("PRTFLG").ToString()

            'EDI出荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Select Case Printflg
                    Case "1"
                        Me._StrSql.Append(" AND OUTEDIL.FREE_C06 = @EDI_CTL_NO ")

                    Case "2"
                        Me._StrSql.Append(" AND OUTEDIL.EDI_CTL_NO = @EDI_CTL_NO ")
                End Select
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            '伝票№(オーダー№)
            'whereStr = .Item("DENPYO_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND OUTEDIL.DENPYO_NO = @DENPYO_NO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            'End If

            'EDI出荷予定日(FROM)
            whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI出荷予定日(TO)
            whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTEDIL.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
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
