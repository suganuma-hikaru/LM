' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH552    : EDI出荷伝票(ゴードー溶剤用)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH552DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH552DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       HED.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'A1'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                    " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID   " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END  AS RPT_ID                     " & vbNewLine _
                                       & "      , HED.DEL_KB           AS DEL_KB                     " & vbNewLine _
                                       & "      , HED.CRT_DATE         AS CRT_DATE                   " & vbNewLine _
                                       & "      , HED.FILE_NAME        AS FILE_NAME                  " & vbNewLine _
                                       & "      , HED.REC_NO           AS REC_NO                     " & vbNewLine _
                                       & "      , HED.NRS_BR_CD        AS NRS_BR_CD                  " & vbNewLine _
                                       & "      , HED.EDI_CTL_NO       AS EDI_CTL_NO                 " & vbNewLine _
                                       & "      , HED.OUTKA_CTL_NO     AS OUTKA_CTL_NO               " & vbNewLine _
                                       & "      , HED.PRTFLG           AS PRTFLG                     " & vbNewLine _
                                       & "      , HED.CANCEL_FLG       AS CANCEL_FLG                 " & vbNewLine _
                                       & "      , HED.NRS_BR_FLG       AS NRS_BR_FLG                 " & vbNewLine _
                                       & "      , HED.FORWARD_FLG      AS FORWARD_FLG                " & vbNewLine _
                                       & "      , HED.DENP_NO          AS DENP_NO                    " & vbNewLine _
                                       & "      , HED.RB_KB            AS RB_KB                      " & vbNewLine _
                                       & "      , HED.DENP_KB          AS DENP_KB                    " & vbNewLine _
                                       & "      , HED.INPUT_DATE       AS INPUT_DATE                 " & vbNewLine _
                                       & "      , HED.TOKUI_CD         AS TOKUI_CD                   " & vbNewLine _
                                       & "      , HED.TOKUI_NM         AS TOKUI_NM                   " & vbNewLine _
                                       & "      , HED.DEST_CD          AS DEST_CD                    " & vbNewLine _
                                       & "      , HED.DEST_NM          AS DEST_NM                    " & vbNewLine _
                                       & "      , HED.DEST_ZIP         AS DEST_ZIP                   " & vbNewLine _
                                       & "      , HED.DEST_AD_1        AS DEST_AD_1                  " & vbNewLine _
                                       & "      , HED.DEST_AD_2        AS DEST_AD_2                  " & vbNewLine _
                                       & "      , HED.DEST_TEL         AS DEST_TEL                   " & vbNewLine _
                                       & "      , HED.CUST_NM          AS CUST_NM                    " & vbNewLine _
                                       & "      , HED.CUST_ZIP         AS CUST_ZIP                   " & vbNewLine _
                                       & "      , HED.CUST_AD_1        AS CUST_AD_1                  " & vbNewLine _
                                       & "      , HED.CUST_AD_2        AS CUST_AD_2                  " & vbNewLine _
                                       & "      , HED.CUST_TEL         AS CUST_TEL                   " & vbNewLine _
                                       & "      , HED.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE            " & vbNewLine _
                                       & "      , HED.ARR_PLAN_DATE    AS ARR_PLAN_DATE              " & vbNewLine _
                                       & "      , HED.HAISO_NM         AS HAISO_NM                   " & vbNewLine _
                                       & "      , HED.UNCHIN_NM        AS UNCHIN_NM                  " & vbNewLine _
                                       & "      , HED.BIN_NM           AS BIN_NM                     " & vbNewLine _
                                       & "      , HED.DEST_TANTO_NM    AS DEST_TANTO_NM              " & vbNewLine _
                                       & "      , HED.ATSUKAI_TEN_NM   AS ATSUKAI_TEN_NM             " & vbNewLine _
                                       & "      , HED.RECORD_STATUS    AS RECORD_STATUS              " & vbNewLine _
                                       & "      , HED.EDI_USER         AS EDI_USER                   " & vbNewLine _
                                       & "      , HED.EDI_DATE         AS EDI_DATE                   " & vbNewLine _
                                       & "      , HED.EDI_TIME         AS EDI_TIME                   " & vbNewLine _
                                       & "      , DTL.GYO              AS GYO                        " & vbNewLine _
                                       & "      , DTL.EDI_CTL_NO_CHU   AS EDI_CTL_NO_CHU             " & vbNewLine _
                                       & "      , DTL.OUTKA_CTL_NO_CHU AS OUTKA_CTL_NO_CHU           " & vbNewLine _
                                       & "      , DTL.NRS_GYO_NO       AS NRS_GYO_NO                 " & vbNewLine _
                                       & "      , DTL.GYO_NO           AS GYO_NO                     " & vbNewLine _
                                       & "      , DTL.GOODS_CD         AS GOODS_CD                   " & vbNewLine _
                                       & "      , DTL.GOODS_EDABAN     AS GOODS_EDABAN               " & vbNewLine _
                                       & "      , DTL.GOODS_NM         AS GOODS_NM                   " & vbNewLine _
                                       & "      , DTL.IRIME            AS IRIME                      " & vbNewLine _
                                       & "      , DTL.NISUGATA         AS NISUGATA                   " & vbNewLine _
                                       & "      , DTL.KOSU             AS KOSU                       " & vbNewLine _
                                       & "      , DTL.SURYO            AS SURYO                      " & vbNewLine _
                                       & "      , DTL.REMARK           AS REMARK                     " & vbNewLine _
                                       & "      , DTL.JYUCHU_NO        AS JYUCHU_NO                  " & vbNewLine _
                                       & "      , DTL.GEKIDOKU_KB      AS GEKIDOKU_KB                " & vbNewLine _
                                       & "      , DTL.COA_YN           AS COA_YN                     " & vbNewLine _
                                       & "      , DTL.GASKURO_YN       AS GASKURO_YN                 " & vbNewLine _
                                       & "      , DTL.SAMPLE_YN        AS SAMPLE_YN                  " & vbNewLine _
                                       & "      , DTL.PKG_NB           AS PKG_NB                     " & vbNewLine _
                                       & "      , DTL.EQ_LOT_KB        AS EQ_LOT_KB                  " & vbNewLine _
                                       & "      , DTL.LOT_NO           AS LOT_NO                     " & vbNewLine _
                                       & "      , DTL.ORDER_NO         AS ORDER_NO                   " & vbNewLine _
                                       & "      , DTL.SHITEI_LABEL     AS SHITEI_LABEL               " & vbNewLine _
                                       & "      , DTL.SHITEI_CAN       AS SHITEI_CAN                 " & vbNewLine _
                                       & "      , ''                   AS SAI_PRINT_UMU              " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 大阪 ゴードー溶剤出荷EDI受信データHEAD - ゴードー溶剤出荷EDI受信データDETAIL
    ''' </remarks>
    Private Const SQL_FROM As String = "   FROM                                                                " & vbNewLine _
                                     & "        $LM_TRN$..H_OUTKAEDI_HED_GODO HED                              " & vbNewLine _
                                     & "        --受信テーブル明細                                             " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_DTL_GODO DTL              " & vbNewLine _
                                     & "                     ON HED.CRT_DATE  = DTL.CRT_DATE                   " & vbNewLine _
                                     & "                    AND HED.FILE_NAME = DTL.FILE_NAME                  " & vbNewLine _
                                     & "                    AND HED.REC_NO    = DTL.REC_NO                     " & vbNewLine _
                                     & "        -- EDI印刷種別テーブル                                         " & vbNewLine _
                                     & "        LEFT JOIN (                                                    " & vbNewLine _
                                     & "                    SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT            " & vbNewLine _
                                     & "                         , H_EDI_PRINT.NRS_BR_CD                       " & vbNewLine _
                                     & "                         , H_EDI_PRINT.EDI_CTL_NO                      " & vbNewLine _
                                     & "                         , H_EDI_PRINT.DENPYO_NO                       " & vbNewLine _
                                     & "                      FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT           " & vbNewLine _
                                     & "                     WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD        " & vbNewLine _
                                     & "                       AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L        " & vbNewLine _
                                     & "                       AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M        " & vbNewLine _
                                     & "                       AND H_EDI_PRINT.PRINT_TP    = '01'              " & vbNewLine _
                                     & "                       AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB         " & vbNewLine _
                                     & "                       AND H_EDI_PRINT.SYS_DEL_FLG = '0'               " & vbNewLine _
                                     & "                     GROUP BY                                          " & vbNewLine _
                                     & "                           H_EDI_PRINT.NRS_BR_CD                       " & vbNewLine _
                                     & "                         , H_EDI_PRINT.EDI_CTL_NO                      " & vbNewLine _
                                     & "                         , H_EDI_PRINT.DENPYO_NO                       " & vbNewLine _
                                     & "                  ) HEDIPRINT                                          " & vbNewLine _
                                     & "               ON HEDIPRINT.NRS_BR_CD  = HED.NRS_BR_CD                 " & vbNewLine _
                                     & "              AND HEDIPRINT.EDI_CTL_NO = HED.EDI_CTL_NO                " & vbNewLine _
                                     & "              AND HEDIPRINT.DENPYO_NO  = HED.DENP_NO                   " & vbNewLine _
                                     & "        -- 荷主マスタ                                                  " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                        " & vbNewLine _
                                     & "                     ON M_CUST.NRS_BR_CD       = HED.NRS_BR_CD         " & vbNewLine _
                                     & "                    AND M_CUST.CUST_CD_L       = @CUST_CD_L            " & vbNewLine _
                                     & "                    AND M_CUST.CUST_CD_M       = @CUST_CD_M            " & vbNewLine _
                                     & "                    AND M_CUST.CUST_CD_S       = '00'                  " & vbNewLine _
                                     & "                    AND M_CUST.CUST_CD_SS      = '00'                  " & vbNewLine _
                                     & "                    AND M_CUST.SYS_DEL_FLG     = '0'                   " & vbNewLine _
                                     & "        -- 商品マスタ                                                  " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                     & "                     ON M_GOODS.NRS_BR_CD      = HED.NRS_BR_CD         " & vbNewLine _
                                     & "                    AND M_GOODS.GOODS_CD_NRS   = DTL.GOODS_CD          " & vbNewLine _
                                     & "        -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)    " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                " & vbNewLine _
                                     & "                     ON M_CUSTRPT1.NRS_BR_CD   = HED.NRS_BR_CD         " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L            " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M            " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.PTN_ID      = 'A1'                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                           " & vbNewLine _
                                     & "                     ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD  " & vbNewLine _
                                     & "                    AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID     " & vbNewLine _
                                     & "                    AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD     " & vbNewLine _
                                     & "                    AND MR1.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ②(商品マスタより)                        " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                " & vbNewLine _
                                     & "                     ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.PTN_ID      = 'A1'                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                           " & vbNewLine _
                                     & "                     ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD  " & vbNewLine _
                                     & "                    AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID     " & vbNewLine _
                                     & "                    AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD     " & vbNewLine _
                                     & "                    AND MR2.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >    " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                     & "                     ON MR3.NRS_BR_CD          = HED.NRS_BR_CD         " & vbNewLine _
                                     & "                    AND MR3.PTN_ID             = 'A1'                  " & vbNewLine _
                                     & "                    AND MR3.STANDARD_FLAG      = '01'                  " & vbNewLine _
                                     & "                    AND MR3.SYS_DEL_FLG        = '0'                   " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY             " & vbNewLine _
                                         & "       HED.CRT_DATE   " & vbNewLine _
                                         & "     , HED.FILE_NAME  " & vbNewLine _
                                         & "     , HED.REC_NO     " & vbNewLine _
                                         & "     , DTL.NRS_GYO_NO " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMH552IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成 -----------------------------------------------------------
		'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH552DAC.SQL_MPrt_SELECT)

        'SQL構築(帳票種別用FROM句)
        Me._StrSql.Append(LMH552DAC.SQL_FROM)

        'SQL構築(帳票種別用WHERE句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then
            Call Me.SetConditionMasterSQL_OUT() '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()     '未出力・両方(出力済、未出力併せて)
        End If 

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

        MyBase.Logger.WriteSQLLog("LMH552DAC", "SelectMPrt", cmd)

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
    ''' ゴードー溶剤EDI受信データ(HEAD)・ゴードー溶剤EDI受信データ(DETAIL)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ゴードー溶剤EDI受信データ(HEAD)・(DETAIL)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH552IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成 -----------------------------------------------------------
        'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH552DAC.SQL_SELECT)

        'SQL構築(印刷データ抽出用 FROM句)
        Me._StrSql.Append(LMH552DAC.SQL_FROM)

        'SQL構築(印刷データ抽出用 WHERE句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then
            Call Me.SetConditionMasterSQL_OUT() '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()     '未出力・両方(出力済、未出力併せて)
        End If 
        
        'SQL構築(印刷データ抽出用 ORDER BY句)
        Me._StrSql.Append(LMH552DAC.SQL_ORDER_BY)    

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

        MyBase.Logger.WriteSQLLog("LMH552DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID","RPT_ID")
        map.Add("DEL_KB","DEL_KB")
        map.Add("CRT_DATE","CRT_DATE")
        map.Add("FILE_NAME","FILE_NAME")
        map.Add("REC_NO","REC_NO")
        map.Add("NRS_BR_CD","NRS_BR_CD")
        map.Add("EDI_CTL_NO","EDI_CTL_NO")
        map.Add("OUTKA_CTL_NO","OUTKA_CTL_NO")
        map.Add("PRTFLG","PRTFLG")
        map.Add("CANCEL_FLG","CANCEL_FLG")
        map.Add("NRS_BR_FLG","NRS_BR_FLG")
        map.Add("FORWARD_FLG","FORWARD_FLG")
        map.Add("DENP_NO","DENP_NO")
        map.Add("RB_KB","RB_KB")
        map.Add("DENP_KB","DENP_KB")
        map.Add("INPUT_DATE","INPUT_DATE")
        map.Add("TOKUI_CD","TOKUI_CD")
        map.Add("TOKUI_NM","TOKUI_NM")
        map.Add("DEST_CD","DEST_CD")
        map.Add("DEST_NM","DEST_NM")
        map.Add("DEST_ZIP","DEST_ZIP")
        map.Add("DEST_AD_1","DEST_AD_1")
        map.Add("DEST_AD_2","DEST_AD_2")
        map.Add("DEST_TEL","DEST_TEL")
        map.Add("CUST_NM","CUST_NM")
        map.Add("CUST_ZIP","CUST_ZIP")
        map.Add("CUST_AD_1","CUST_AD_1")
        map.Add("CUST_AD_2","CUST_AD_2")
        map.Add("CUST_TEL","CUST_TEL")
        map.Add("OUTKA_PLAN_DATE","OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE","ARR_PLAN_DATE")
        map.Add("HAISO_NM","HAISO_NM")
        map.Add("UNCHIN_NM","UNCHIN_NM")
        map.Add("BIN_NM","BIN_NM")
        map.Add("DEST_TANTO_NM","DEST_TANTO_NM")
        map.Add("ATSUKAI_TEN_NM","ATSUKAI_TEN_NM")
        map.Add("RECORD_STATUS","RECORD_STATUS")
        map.Add("EDI_USER", "EDI_USER")
        map.Add("EDI_DATE","EDI_DATE")
        map.Add("EDI_TIME","EDI_TIME")
        map.Add("GYO","GYO")
        map.Add("EDI_CTL_NO_CHU","EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO_CHU","OUTKA_CTL_NO_CHU")
        map.Add("NRS_GYO_NO","NRS_GYO_NO")
        map.Add("GYO_NO","GYO_NO")
        map.Add("GOODS_CD","GOODS_CD")
        map.Add("GOODS_EDABAN","GOODS_EDABAN")
        map.Add("GOODS_NM","GOODS_NM")
        map.Add("IRIME","IRIME")
        map.Add("NISUGATA","NISUGATA")
        map.Add("KOSU","KOSU")
        map.Add("SURYO","SURYO")
        map.Add("REMARK","REMARK")
        map.Add("JYUCHU_NO","JYUCHU_NO")
        map.Add("GEKIDOKU_KB","GEKIDOKU_KB")
        map.Add("COA_YN","COA_YN")
        map.Add("GASKURO_YN","GASKURO_YN")
        map.Add("SAMPLE_YN","SAMPLE_YN")
        map.Add("PKG_NB","PKG_NB")
        map.Add("EQ_LOT_KB","EQ_LOT_KB")
        map.Add("LOT_NO","LOT_NO")
        map.Add("ORDER_NO","ORDER_NO")
        map.Add("SHITEI_LABEL","SHITEI_LABEL")
        map.Add("SHITEI_CAN","SHITEI_CAN")
        map.Add("SAI_PRINT_UMU","SAI_PRINT_UMU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH552OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL(ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me._Row.Item("INOUT_KB").ToString(), DBDataType.CHAR))

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
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '要望番号1077 2012.05.29 抽出条件からEDI出荷管理番号削除、伝票№追加 --- START ---
            ''EDI出荷管理番号
            'whereStr = .Item("EDI_CTL_NO").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.EDI_CTL_NO = @EDI_CTL_NO ")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            'End If

            '伝票№(オーダー№)
            whereStr = .Item("DENPYO_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.DENP_NO = @DENPYO_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            End If
            '要望番号1077 2012.05.29 抽出条件からEDI出荷管理番号削除、伝票№追加 ---  END  ---

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
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
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

            ''入出荷区分
            'whereStr = .Item("INOUT_KB").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND HED.INOUT_KB = @INOUT_KB")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))
            'End If

            '(2012.05.09) Notes№1007/1008 未出力/出力済の判断をHEDIPRINTのレコード有無で行う --- START ---
            ''プリントフラグ
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
