' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷編集
'  プログラムID     :  LMC792DAC : シッピングマーク(大阪・汎用)
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC792DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC792DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                          " & vbNewLine _
                                            & "	 MAIN.NRS_BR_CD                           AS NRS_BR_CD  " & vbNewLine _
                                            & " ,'BT'                                     AS PTN_ID     " & vbNewLine _
                                            & "	,MAIN.PTN_CD                              AS PTN_CD     " & vbNewLine _
                                            & "	,MAIN.RPT_ID                              AS RPT_ID     " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                         " & vbNewLine _
                                             & "  MAIN.NRS_BR_CD         AS NRS_BR_CD                                         " & vbNewLine _
                                             & " ,MAIN.OUTKA_NO_L        AS OUTKA_NO_L                                        " & vbNewLine _
                                             & " ,MAIN.RPT_ID            AS RPT_ID                                            " & vbNewLine _
                                             & " ,MAIN.CASE_NO_FROM      AS CASE_NO_FROM                                      " & vbNewLine _
                                             & " ,MAIN.CASE_NO_TO        AS CASE_NO_TO                                        " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_1       AS MARK_INFO_1                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_2       AS MARK_INFO_2                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_3       AS MARK_INFO_3                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_4       AS MARK_INFO_4                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_5       AS MARK_INFO_5                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_6       AS MARK_INFO_6                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_7       AS MARK_INFO_7                                       " & vbNewLine _
                                             & " ,MAIN.MARK_INFO_8       AS MARK_INFO_8                                       " & vbNewLine _
                                             & " ,MAIN.MARK_NB           AS MARK_NB                                           " & vbNewLine
#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                                            " & vbNewLine _
                                            & "	(                                                                              " & vbNewLine _
                                             & "	 SELECT                                                                    " & vbNewLine _
                                             & "           CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                    " & vbNewLine _
                                             & "	            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                    " & vbNewLine _
                                             & "	            ELSE MR3.PTN_CD END                            AS PTN_CD       " & vbNewLine _
                                             & "         , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                    " & vbNewLine _
                                             & "		        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                             & "		        ELSE MR3.RPT_ID END                           AS RPT_ID       " & vbNewLine _
                                             & "		 ,CMH.NRS_BR_CD                                       AS NRS_BR_CD    " & vbNewLine _
                                             & "		 ,CMH.OUTKA_NO_L                                      AS OUTKA_NO_L   " & vbNewLine _
                                             & "         ,CMH.CASE_NO_FROM                                    AS CASE_NO_FROM " & vbNewLine _
                                             & "         ,CMH.CASE_NO_TO                                      AS CASE_NO_TO   " & vbNewLine _
                                             & "		 ,CMD_001.REMARK_INFO                                 AS MARK_INFO_1  " & vbNewLine _
                                             & "		 --,CMD_002.REMARK_INFO                                 AS MARK_INFO_2  " & vbNewLine _
                                             & "		 ,CASE WHEN CHARINDEX('@CUST_ORD_NO@' , CMD_002.REMARK_INFO) > 0 THEN REPLACE(CMD_002.REMARK_INFO,'@CUST_ORD_NO@',CL.CUST_ORD_NO) ELSE CMD_002.REMARK_INFO END AS MARK_INFO_2                      " & vbNewLine _
                                             & "		 --,CMD_003.REMARK_INFO                                 AS MARK_INFO_3  " & vbNewLine _
                                             & "		 ,CASE WHEN CHARINDEX('@GOODS_NM@' , CMD_003.REMARK_INFO) > 0 THEN REPLACE(CMD_003.REMARK_INFO,'@GOODS_NM@',ISNULL(MGD_055.SET_NAIYO,'')) ELSE CMD_003.REMARK_INFO END AS MARK_INFO_3                                     " & vbNewLine _
                                             & "		 ,CASE WHEN CHARINDEX('@LOT_NO@' , CMD_004.REMARK_INFO) > 0 THEN REPLACE(CMD_004.REMARK_INFO,'@LOT_NO@',CM.LOT_NO) ELSE CMD_004.REMARK_INFO END AS MARK_INFO_4                                     " & vbNewLine _
                                             & "		 --,CMD_004.REMARK_INFO                                 AS MARK_INFO_4  " & vbNewLine _
                                             & "		 ,CMD_005.REMARK_INFO                                 AS MARK_INFO_5  " & vbNewLine _
                                             & "		 ,CASE WHEN CHARINDEX('@OUTKA_TTL_QT@' , CMD_006.REMARK_INFO) > 0 THEN REPLACE(CMD_006.REMARK_INFO,'@OUTKA_TTL_QT@',CONVERT(integer,CM.OUTKA_TTL_QT)) ELSE CMD_006.REMARK_INFO END AS MARK_INFO_6  " & vbNewLine _
                                             & "		 --,CMD_006.REMARK_INFO                                 AS MARK_INFO_6  " & vbNewLine _
                                             & "		 ,CMD_007.REMARK_INFO                                 AS MARK_INFO_7  " & vbNewLine _
                                             & "		 ,CMD_008.REMARK_INFO                                 AS MARK_INFO_8  " & vbNewLine _
                                             & "		 ,''                                                  AS MARK_NB      " & vbNewLine _
                                             & "		FROM $LM_TRN$..C_MARK_HED CMH                                          " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_001                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_001.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_001.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_001.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_001.MARK_EDA    = '001'                             " & vbNewLine _
                                             & "         AND CMD_001.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_002                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_002.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_002.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_002.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_002.MARK_EDA    = '002'                             " & vbNewLine _
                                             & "         AND CMD_002.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_003                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_003.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_003.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_003.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_003.MARK_EDA    = '003'                             " & vbNewLine _
                                             & "         AND CMD_003.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_004                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_004.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_004.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_004.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_004.MARK_EDA    = '004'                             " & vbNewLine _
                                             & "         AND CMD_004.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_005                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_005.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_005.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_005.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_005.MARK_EDA    = '005'                             " & vbNewLine _
                                             & "         AND CMD_005.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_006                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_006.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_006.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_006.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_006.MARK_EDA    = '006'                             " & vbNewLine _
                                             & "         AND CMD_006.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_007                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_007.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_007.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_007.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_007.MARK_EDA    = '007'                             " & vbNewLine _
                                             & "         AND CMD_007.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "        LEFT JOIN                                                    " & vbNewLine _
                                             & "             $LM_TRN$..C_MARK_DTL CMD_008                            " & vbNewLine _
                                             & "          ON CMH.NRS_BR_CD       = CMD_008.NRS_BR_CD                 " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_L      = CMD_008.OUTKA_NO_L                " & vbNewLine _
                                             & "         AND CMH.OUTKA_NO_M      = CMD_008.OUTKA_NO_M                " & vbNewLine _
                                             & "         AND CMD_008.MARK_EDA    = '008'                             " & vbNewLine _
                                             & "         AND CMD_008.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                             & "		LEFT JOIN                                                              " & vbNewLine _
                                             & "		$LM_TRN$..C_OUTKA_L CL                                                 " & vbNewLine _
                                             & "		ON                                                                     " & vbNewLine _
                                             & "		CMH.NRS_BR_CD = CL.NRS_BR_CD                                           " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CMH.OUTKA_NO_L = CL.OUTKA_NO_L                                         " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CL.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                             & "		LEFT JOIN                                                              " & vbNewLine _
                                             & "		$LM_TRN$..C_OUTKA_M CM                                                 " & vbNewLine _
                                             & "		ON                                                                     " & vbNewLine _
                                             & "		CMH.NRS_BR_CD = CM.NRS_BR_CD                                           " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CMH.OUTKA_NO_L = CM.OUTKA_NO_L                                         " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CMH.OUTKA_NO_M = CM.OUTKA_NO_M                                         " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CM.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                             & "		LEFT JOIN                                                              " & vbNewLine _
                                             & "		$LM_MST$..M_GOODS MG                                                   " & vbNewLine _
                                             & "		ON                                                                     " & vbNewLine _
                                             & "		CM.NRS_BR_CD = MG.NRS_BR_CD                                            " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CM.GOODS_CD_NRS = MG.GOODS_CD_NRS                                      " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		MG.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                             & "		LEFT JOIN                                                              " & vbNewLine _
                                             & "		$LM_MST$..M_GOODS_DETAILS MGD_055                                      " & vbNewLine _
                                             & "		ON                                                                     " & vbNewLine _
                                             & "		CM.NRS_BR_CD = MGD_055.NRS_BR_CD                                       " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		CM.GOODS_CD_NRS = MGD_055.GOODS_CD_NRS                                 " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		MGD_055.SUB_KB = '55'                                                  " & vbNewLine _
                                             & "		AND                                                                    " & vbNewLine _
                                             & "		MGD_055.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                             & "	  LEFT JOIN                                                            " & vbNewLine _
                                             & "		  $LM_MST$..M_CUST_RPT MCR1                                        " & vbNewLine _
                                             & "	  ON CL.NRS_BR_CD = MCR1.NRS_BR_CD                                     " & vbNewLine _
                                             & "	  AND CL.CUST_CD_L = MCR1.CUST_CD_L                                    " & vbNewLine _
                                             & "	  AND CL.CUST_CD_M = MCR1.CUST_CD_M                                    " & vbNewLine _
                                             & "	  AND MCR1.PTN_ID = '00'                                               " & vbNewLine _
                                             & "	--帳票パターン取得                                                     " & vbNewLine _
                                             & "	  LEFT JOIN                                                            " & vbNewLine _
                                             & "		  $LM_MST$..M_RPT MR1                                              " & vbNewLine _
                                             & "	  ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                    " & vbNewLine _
                                             & "	  AND MR1.PTN_ID = MCR1.PTN_ID                                         " & vbNewLine _
                                             & "	  AND MR1.PTN_CD = MCR1.PTN_CD                                         " & vbNewLine _
                                             & "	  AND MR1.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                             & "	--商品Mの荷主での荷主帳票パターン取得                                  " & vbNewLine _
                                             & "	  LEFT JOIN                                                            " & vbNewLine _
                                             & "		  $LM_MST$..M_CUST_RPT MCR2                                        " & vbNewLine _
                                             & "	  ON MG.NRS_BR_CD = MCR2.NRS_BR_CD                                     " & vbNewLine _
                                             & "	  AND MG.CUST_CD_L = MCR2.CUST_CD_L                                    " & vbNewLine _
                                             & "	  AND MG.CUST_CD_M = MCR2.CUST_CD_M                                    " & vbNewLine _
                                             & "	  AND MG.CUST_CD_S = MCR2.CUST_CD_S                                    " & vbNewLine _
                                             & "	  AND MCR2.PTN_ID = 'BT'                                               " & vbNewLine _
                                             & "	--帳票パターン取得                                                     " & vbNewLine _
                                             & "	  LEFT JOIN                                                            " & vbNewLine _
                                             & "		 $LM_MST$..M_RPT MR2                                               " & vbNewLine _
                                             & "	  ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                    " & vbNewLine _
                                             & "	  AND MR2.PTN_ID = MCR2.PTN_ID                                         " & vbNewLine _
                                             & "	  AND MR2.PTN_CD = MCR2.PTN_CD                                         " & vbNewLine _
                                             & "	  AND MR2.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                             & "	--存在しない場合の帳票パターン取得                                     " & vbNewLine _
                                             & "	  LEFT LOOP JOIN                                                       " & vbNewLine _
                                             & "		 $LM_MST$..M_RPT MR3                                               " & vbNewLine _
                                             & "	  ON MR3.NRS_BR_CD = CL.NRS_BR_CD                                      " & vbNewLine _
                                             & "	  AND MR3.PTN_ID = 'BT'                                                " & vbNewLine _
                                             & "	  AND MR3.STANDARD_FLAG = '01'                                         " & vbNewLine _
                                             & "	  AND MR3.SYS_DEL_FLG = '0'                                            " & vbNewLine _
                                             & "	WHERE                                                                  " & vbNewLine _
                                             & "	 CMH.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                             & "	 AND                                                                   " & vbNewLine _
                                             & "	 CMH.OUTKA_NO_L = @OUTKA_NO_L                                          " & vbNewLine _
                                             & "	 AND                                                                   " & vbNewLine _
                                             & "	 CMH.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                             & "	 )MAIN                                                                 " & vbNewLine


#End Region

#Region "GROUP BY句"

    ''' <summary>
    ''' 印刷データ抽出用 GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY              " & vbNewLine _
                                         & "  MAIN.NRS_BR_CD      " & vbNewLine _
                                         & " ,MAIN.OUTKA_NO_L     " & vbNewLine _
                                         & " ,MAIN.RPT_ID         " & vbNewLine _
                                         & " ,MAIN.CASE_NO_FROM   " & vbNewLine _
                                         & " ,MAIN.CASE_NO_TO     " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_1    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_2    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_3    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_4    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_5    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_6    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_7    " & vbNewLine _
                                         & " ,MAIN.MARK_INFO_8    " & vbNewLine _
                                         & " ,MAIN.MARK_NB        " & vbNewLine

#End Region

#Region "ORDER BY句"

    ' ''' <summary>
    ' ''' 印刷データ抽出用 ORDER BY句
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Const SQL_ORDER_BY As String = " ORDER BY          " & vbNewLine _
    '                                     & " SUB_CS.LOT_NO      " & vbNewLine


#End Region

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

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC792IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC792DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC792DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC792DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        '処理件数の設定
        If ds.Tables("M_RPT").Rows.Count < 1 Then
            MyBase.SetMessage("G021")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC792IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMC792DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC792DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC792DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        'Me._StrSql.Append(LMC792DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)
        Call Me.setIndataParameter(Me._Row)               '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC792DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("MARK_INFO_1", "MARK_INFO_1")
        map.Add("MARK_INFO_2", "MARK_INFO_2")
        map.Add("MARK_INFO_3", "MARK_INFO_3")
        map.Add("MARK_INFO_4", "MARK_INFO_4")
        map.Add("MARK_INFO_5", "MARK_INFO_5")
        map.Add("MARK_INFO_6", "MARK_INFO_6")
        map.Add("MARK_INFO_7", "MARK_INFO_7")
        map.Add("MARK_INFO_8", "MARK_INFO_8")
        map.Add("CASE_NO_FROM", "CASE_NO_FROM")
        map.Add("CASE_NO_TO", "CASE_NO_TO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC792OUT")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setIndataParameter(ByVal dr As DataRow)

        With dr

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

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

End Class
