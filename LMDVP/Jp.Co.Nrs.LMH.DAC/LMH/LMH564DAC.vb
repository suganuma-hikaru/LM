' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH564    : EDI入出荷受信帳票(旭化成)
'  作  成  者       :  黎
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH564DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH564DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'AU'                                             AS PTN_ID    " & vbNewLine _
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
    ''' ダウケミEDI受信データHEAD - ダウケミEDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_ASH  HED                              " & vbNewLine _
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
                                            & "                     AND H_EDI_PRINT.PRINT_TP    = '02'             " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                            & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                            & "                   GROUP BY                                         " & vbNewLine _
                                            & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                            & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                            & "                ) HEDIPRINT                                         " & vbNewLine _
                                            & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                            & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                            & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                            & "      -- 旭化成EDI受信データ                                      " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_ASH  DTL             " & vbNewLine _
                                            & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                            & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                            & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                            & "      -- 商品マスタ                                                 " & vbNewLine _
                                            & "     LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                            & "     ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD                      " & vbNewLine _
                                            & "     AND M_GOODS.GOODS_CD_CUST = DTL.HINMEI						   " & vbNewLine _
                                            & "      -- 帳票パターンマスタ①(H_OUTKAEDI_HED_ASHの荷主より取得)     " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                            & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT1.PTN_ID      = 'AU'                 " & vbNewLine _
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
                                            & "                  AND M_CUSTRPT2.PTN_ID      = 'AU'                 " & vbNewLine _
                                            & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                            & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                            & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                            & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                            & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                            & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                            & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                            & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                            & "                  AND MR3.PTN_ID             = 'AU'                 " & vbNewLine _
                                            & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                            & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                   " & vbNewLine _
                                            & "       HED.NRS_BR_CD     =  @NRS_BR_CD  " & vbNewLine _
                                            & "   AND  '00070'  =  @CUST_CD_L          " & vbNewLine _
                                            & "   AND  '00'     =  @CUST_CD_M          " & vbNewLine
    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                                              " & vbNewLine _
                                          & "      CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   					       " & vbNewLine _
                                          & "        	WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   					       " & vbNewLine _
                                          & " 	   ELSE MR3.PTN_CD END                           	   AS RPT_ID			   " & vbNewLine _
                                          & "      , HED.EDI_CTL_NO          						   AS EDI_CTL_NO           " & vbNewLine _
                                          & "      , HED.TORIHIKI_KB         						   AS TORIHIKI_KB          " & vbNewLine _
                                          & "      , HED.TORIHIKI_KB_KANJI   						   AS TORIHIKI_KB_KANJI    " & vbNewLine _
                                          & "      , HED.CHOHYO_NO           						   AS CHOHYO_NO            " & vbNewLine _
                                          & "      , HED.FORMAT_NO           						   AS FORMAT_NO            " & vbNewLine _
                                          & "      , HED.SASHIZU_NO          						   AS SASHIZU_NO           " & vbNewLine _
                                          & "      , HED.NIUKE_NIN 			  					       AS NIUKE_NIN            " & vbNewLine _
                                          & "      , HED.SHUKKA_DATE         						   AS SHUKKA_DATE          " & vbNewLine _
                                          & "      , HED.NIUKE_JUSHO_KANJI   						   AS NIUKE_JUSHO_KANJI    " & vbNewLine _
                                          & "      , HED.CHUMON_NO           						   AS CHUMON_NO            " & vbNewLine _
                                          & "      , HED.NIUKE_NM_KANJI      						   AS NIUKE_NM_KANJI       " & vbNewLine _
                                          & "      , HED.USER_NOKI           						   AS USER_NOKI            " & vbNewLine _
                                          & "      , HED.NIUKE_TEL           						   AS NIUKE_TEL            " & vbNewLine _
                                          & "      , HED.SHIMUKE_CHI         						   AS SHIMUKE_CHI          " & vbNewLine _
                                          & "      , HED.YUSO_GYOSHA         						   AS YUSO_GYOSHA          " & vbNewLine _
                                          & "      , HED.KIJI                						   AS KIJI                 " & vbNewLine _
                                          & "      , HED.UNCHIN_CD           						   AS UNCHIN_CD            " & vbNewLine _
                                          & "      , DTL.GYO                 						   AS GYO                  " & vbNewLine _
                                          & "      , DTL.HINMEI              						   AS HINMEI               " & vbNewLine _
                                          & "      , DTL.HINMEI_YAKU         						   AS HINMEI_YAKU          " & vbNewLine _
                                          & "      , DTL.LOT_NO              						   AS LOT_NO               " & vbNewLine _
                                          & "      , DTL.NISUGATA_SURYO      						   AS NISUGATA_SURYO       " & vbNewLine _
                                          & "      , DTL.KANRI_SURYO         						   AS KANRI_SURYO          " & vbNewLine _
                                          & "      , DTL.TAN_I               						   AS TAN_I                " & vbNewLine _
                                          & "      , DTL.KANRI_KB            						   AS KANRI_KB             " & vbNewLine _
                                          & "      , DTL.YUSHUTSU_KB         						   AS YUSHUTSU_KB          " & vbNewLine _
                                          & "      , DTL.END_KB              						   AS END_KB               " & vbNewLine _
                                          & "      , M_DEST.SYS_DEL_FLG      						   AS SYS_DEL_FLG          " & vbNewLine _
                                          & "      , HED.ORDER_DATE        						       AS ORDER_DATE           " & vbNewLine _
                                          & "      , HED.SHORI_DATE          						   AS SHORI_DATE           " & vbNewLine _
                                          & "      , HED.SHORI_TIME          						   AS SHORI_TIME           " & vbNewLine _
                                          & "      , HED.DEL_KB              						   AS H_DEL_KB             " & vbNewLine _
                                          & "      , HED.NRS_BR_CD           						   AS NRS_BR_CD            " & vbNewLine _
                                          & "      , HED.CRT_DATE            						   AS CRT_DATE             " & vbNewLine _
                                          & "      , HED.FILE_NAME           						   AS FILE_NAME            " & vbNewLine _
                                          & "      , HED.REC_NO              						   AS REC_NO               " & vbNewLine _
                                          & "      , DTL.EDI_CTL_NO_CHU      						   AS EDI_CTL_NO_CHU       " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 旭化成EDI受信データHEAD - 旭化成EDI受信データDETAIL,商品Ｍ,区分Ｍ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_HED_ASH  HED                           " & vbNewLine _
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
                                    & "                     AND H_EDI_PRINT.PRINT_TP    = '02'             " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB        " & vbNewLine _
                                    & "                     AND H_EDI_PRINT.SYS_DEL_FLG = '0'              " & vbNewLine _
                                    & "                   GROUP BY                                         " & vbNewLine _
                                    & "                         H_EDI_PRINT.NRS_BR_CD                      " & vbNewLine _
                                    & "                       , H_EDI_PRINT.EDI_CTL_NO                     " & vbNewLine _
                                    & "                ) HEDIPRINT                                         " & vbNewLine _
                                    & "             ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                " & vbNewLine _
                                    & "            AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO               " & vbNewLine _
                                    & " --【Notes】№1007/1008対応 ---  END  ---                           " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_ASH  DTL             " & vbNewLine _
                                    & "                   ON DTL.CRT_DATE  = HED.CRT_DATE                  " & vbNewLine _
                                    & "                  AND DTL.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
                                    & "                  AND DTL.REC_NO    = HED.REC_NO                    " & vbNewLine _
                                    & "      -- 荷主マスタ                                                 " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                    & "                   ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD        " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_L       = HED.CUST_CD_L        " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_M       = HED.CUST_CD_M        " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                    & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                    & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                    & "		 -- 届け先マスタ                                               " & vbNewLine _
                                    & "         LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                    " & vbNewLine _
                                    & "					  ON M_DEST.NRS_BR_CD       = HED.NRS_BR_CD		   " & vbNewLine _
                                    & "					 AND M_DEST.CUST_CD_L		 = HED.CUST_CD_L	   " & vbNewLine _
                                    & "					 AND M_DEST.DEST_CD         = HED.NIUKE_NIN		   " & vbNewLine _
                                    & "      -- 商品マスタ                                                 " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                    & "                   ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD        " & vbNewLine _
                                    & "                  AND M_GOODS.GOODS_CD_NRS   = DTL.HINMEI           " & vbNewLine _
                                    & "      -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_NSNの荷主より取得)   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                    & "                   ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD        " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_L   = HED.CUST_CD_L        " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_M   = HED.CUST_CD_M        " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                    & "                  AND M_CUSTRPT1.PTN_ID      = 'AU'                 " & vbNewLine _
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
                                    & "                  AND M_CUSTRPT2.PTN_ID      = 'AU'                 " & vbNewLine _
                                    & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                    & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                    & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                    & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                    & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                    & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                    & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                    & "                   ON MR3.NRS_BR_CD          =  HED.NRS_BR_CD       " & vbNewLine _
                                    & "                  AND MR3.PTN_ID             = 'AU'                 " & vbNewLine _
                                    & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                    & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY            " & vbNewLine _
                                         & "       HED.CRT_DATE  " & vbNewLine _
                                         & "     , HED.FILE_NAME " & vbNewLine _
                                         & "     , HED.REC_NO    " & vbNewLine _
                                         & "     , DTL.GYO       " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMH564IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH564DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH564DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        '(2012.03.19) WHERE条件を帳票取得時と同じにする -- START --
        'Me._StrSql.Append(LMH564DAC.SQL_MPrt_WHERE)     'SQL構築(帳票種別用WHERE句)
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        If Me._Row.Item("PRTFLG").ToString = "1" Then    'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()          '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定) '未出力・両方(出力済、未出力併せて)
        End If                                          'Notes 1061 2012/05/15　終了
        'Call Me.SetConditionPrintPatternMSQL()          '条件設定 Notes1061
        '(2012.03.19) WHERE条件を帳票取得時と同じにする --  END  --

        ''追加(Notes_1007 2012/05/09)
        'Call Me.SetConditionPrintPatternMSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH564DAC", "SelectMPrt", cmd)

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
    ''' 旭化成EDI受信データ(HEAD)・旭化成EDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>旭化成EDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH564IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH564DAC.SQL_SELECT)           'SQL構築(印刷データ抽出用 SELECT句)

        Me._StrSql.Append(LMH564DAC.SQL_FROM)             'SQL構築(印刷データ抽出用 FROM句)

        If Me._Row.Item("PRTFLG").ToString = "1" Then     'Notes 1061 2012/05/15　開始
            Call Me.SetConditionMasterSQL_OUT()              '出力済の場合
            'Call Me.SetConditionPrintPatternMSQL()          '条件設定
        Else
            Call Me.SetConditionMasterSQL()                  'SQL構築(印刷データ抽出条件設定) '未出力・両方(出力済、未出力併せて)
        End If                                            'Notes 1061 2012/05/15　終了
        Me._StrSql.Append(LMH564DAC.SQL_ORDER_BY)         'SQL構築(印刷データ抽出用 ORDER BY句)

        'Call Me.SetConditionPrintPatternMSQL()           '条件設定 Notes1061

        ''追加(Notes_1007 2012/05/09)
        'Call Me.SetConditionPrintPatternMSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH564DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("TORIHIKI_KB", "TORIHIKI_KB")
        map.Add("TORIHIKI_KB_KANJI", "TORIHIKI_KB_KANJI")
        map.Add("CHOHYO_NO", "CHOHYO_NO")
        map.Add("FORMAT_NO", "FORMAT_NO")
        map.Add("SASHIZU_NO", "SASHIZU_NO")
        map.Add("NIUKE_NIN", "NIUKE_NIN")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NIUKE_JUSHO_KANJI", "NIUKE_JUSHO_KANJI")
        map.Add("CHUMON_NO", "CHUMON_NO")
        map.Add("NIUKE_NM_KANJI", "NIUKE_NM_KANJI")
        map.Add("USER_NOKI", "USER_NOKI")
        map.Add("NIUKE_TEL", "NIUKE_TEL")
        map.Add("SHIMUKE_CHI", "SHIMUKE_CHI")
        map.Add("YUSO_GYOSHA", "YUSO_GYOSHA")
        map.Add("KIJI", "KIJI")
        map.Add("UNCHIN_CD", "UNCHIN_CD")
        map.Add("GYO", "GYO")
        map.Add("HINMEI", "HINMEI")
        map.Add("HINMEI_YAKU", "HINMEI_YAKU")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NISUGATA_SURYO", "NISUGATA_SURYO")
        map.Add("KANRI_SURYO", "KANRI_SURYO")
        map.Add("TAN_I", "TAN_I")
        map.Add("KANRI_KB", "KANRI_KB")
        map.Add("YUSHUTSU_KB", "YUSHUTSU_KB")
        map.Add("END_KB", "END_KB")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("ORDER_DATE", "ORDER_DATE")
        map.Add("SHORI_DATE", "SHORI_DATE")
        map.Add("SHORI_TIME", "SHORI_TIME")
        map.Add("H_DEL_KB", "H_DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        ' map.Add("PRTFLG", "PRTFLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH564OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        'SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList() 'notes1061

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            '営業所コード
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

            '入出荷区分(Notes1007 2012/05/09)
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

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
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '取り敢えずないので調査後に条件復活させる2012/11/02
            '倉庫コード
            'whereStr = .Item("WH_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.NRS_WH_CD = @WH_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            'End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine) 'Notes 1061
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR)) 'Notes 1061
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine) 'Notes 1061
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR)) 'Notes 1061
            End If

            'EDI取込日(FROM)
            whereStr = .Item("CRT_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_DATE >= @CRT_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("CRT_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.CRT_DATE <= @CRT_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR)) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)'Notes 1061
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
            '    Me._StrSql.Append(vbNewLine) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)'Notes 1061
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR)) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)'Notes 1061
            'End If

            '2012/12/17検索条件の邪魔になるので除外
            ''START-20121109追加：取り消し区分表示制御開始▼▼▼
            'Me._StrSql.Append(" AND HED.EDI_CTL_NO != 'C00000000'")
            'Me._StrSql.Append(vbNewLine)
            ''▲▲▲20121109追加：取り消し区分表示制御終了-TOEND

            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            'プリントフラグ
            'whereStr = .Item("PRTFLG").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.PRTFLG = @PRTFLG")
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
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済 Notes 1061 2012/05/15 新設)
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
                Me._StrSql.Append(" HED.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            'End If

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
            Me._StrSql.Append(vbNewLine) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR)) 'Notes1007 2012/05/09(スカラ変数の定義がぶつかるためコメントアウト)
            'End If

            'EDI出荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.EDI_CTL_NO = @EDI_CTL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            '2012/12/17検索条件の邪魔になるので除外
            ''START-20121109追加：取り消し区分表示制御開始▼▼▼
            'Me._StrSql.Append(" AND HED.EDI_CTL_NO != 'C00000000'")
            'Me._StrSql.Append(vbNewLine)
            ''▲▲▲20121109追加：取り消し区分表示制御終了-TOEND

            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            'プリントフラグ
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
            '未出力/出力済の判断をHEDIPRINTのレコード有無で行う ---  END  ---

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
