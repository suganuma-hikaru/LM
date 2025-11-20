' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI610DAC : シリンダ番号エラーリスト
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI610DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI610DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	 ZAI_TRS.NRS_BR_CD                                      AS NRS_BR_CD  " & vbNewLine _
                                            & ", 'AJ'                                                   AS PTN_ID     " & vbNewLine _
                                            & ", CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                     " & vbNewLine _
                                            & "	 	  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	  	  ELSE MR3.PTN_CD END                               AS PTN_CD     " & vbNewLine _
                                            & ", CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                            & "  	  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                            & "	 	  ELSE MR3.RPT_ID END                               AS RPT_ID     " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                " & vbNewLine _
                                            & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                     " & vbNewLine _
                                            & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                            & "       ELSE MR3.RPT_ID END               AS RPT_ID                     " & vbNewLine _
                                            & ", ZAI_TRS.NRS_BR_CD                      AS NRS_BR_CD                  " & vbNewLine _
                                            & ", ZAI_TRS.CUST_CD_L                      AS CUST_CD_L                  " & vbNewLine _
                                            & ", CUST.CUST_NM_L                         AS CUST_NM_L                  " & vbNewLine _
                                            & ", ZAI_TRS.GOODS_CD_NRS                   AS GOODS_CD_NRS               " & vbNewLine _
                                            & ", GOODS.GOODS_CD_CUST                    AS GOODS_CD_CUST              " & vbNewLine _
                                            & ", GOODS.GOODS_NM_1                       AS GOODS_NM_1                 " & vbNewLine _
                                            & ", ZAI_TRS.INKO_PLAN_DATE                 AS INKO_PLAN_DATE             " & vbNewLine _
                                            & ", ZAI_TRS.INKA_NO_L                      AS INKA_NO_L                  " & vbNewLine _
                                            & ", ZAI_TRS.INKA_NO_M                      AS INKA_NO_M                  " & vbNewLine _
                                            & ", ZAI_TRS.INKA_NO_S                      AS INKA_NO_S                  " & vbNewLine _
                                            & ", ZAI_TRS.SERIAL_NO                      AS SERIAL_NO                  " & vbNewLine _
                                            & ", ''                                     AS ERR_MSG                    " & vbNewLine

#End Region

#Region "FROM句"

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM $LM_TRN$..D_ZAI_TRS ZAI_TRS                       " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_GOODS GOODS                      " & vbNewLine _
                                            & "       ON GOODS.NRS_BR_CD = ZAI_TRS.NRS_BR_CD          " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_L = ZAI_TRS.CUST_CD_L          " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_M = ZAI_TRS.CUST_CD_M          " & vbNewLine _
                                            & "      AND GOODS.GOODS_CD_NRS = ZAI_TRS.GOODS_CD_NRS    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST CUST                        " & vbNewLine _
                                            & "       ON GOODS.NRS_BR_CD = CUST.NRS_BR_CD             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_L = CUST.CUST_CD_L             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_M = CUST.CUST_CD_M             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_S = CUST.CUST_CD_S             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS           " & vbNewLine _
                                            & "INNER JOIN $LM_MST$..M_CUST_DETAILS CDTL                        " & vbNewLine _
                                            & "       ON CUST.NRS_BR_CD = CDTL.NRS_BR_CD             " & vbNewLine _
                                            & "      AND CUST.CUST_CD_L = CDTL.CUST_CD             " & vbNewLine _
                                            & "      AND CDTL.SUB_KB = '46'             " & vbNewLine _
                                            & "INNER JOIN $LM_MST$..M_GOODS_DETAILS DETAILS           " & vbNewLine _
                                            & "       ON ZAI_TRS.NRS_BR_CD = DETAILS.NRS_BR_CD        " & vbNewLine _
                                            & "      AND ZAI_TRS.GOODS_CD_NRS = DETAILS.GOODS_CD_NRS  " & vbNewLine _
                                            & "      AND DETAILS.SUB_KB = '12'                        " & vbNewLine _
                                            & "--荷主帳票パターン取得                  　             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                    " & vbNewLine _
                                            & "       ON ZAI_TRS.NRS_BR_CD = MCR1.NRS_BR_CD           " & vbNewLine _
                                            & "      AND ZAI_TRS.CUST_CD_L = MCR1.CUST_CD_L           " & vbNewLine _
                                            & "      AND ZAI_TRS.CUST_CD_M = MCR1.CUST_CD_M           " & vbNewLine _
                                            & "      AND MCR1.CUST_CD_S = '00'                        " & vbNewLine _
                                            & "      AND MCR1.PTN_ID    = 'AJ'                        " & vbNewLine _
                                            & "  --帳票パターン取得                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR1                          " & vbNewLine _
                                            & "       ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD               " & vbNewLine _
                                            & "      AND MR1.PTN_ID = MCR1.PTN_ID                     " & vbNewLine _
                                            & "      AND MR1.PTN_CD = MCR1.PTN_CD                     " & vbNewLine _
                                            & "      AND MR1.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                            & "  --商品Ｍの荷主での荷主帳票パターン取得               " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                    " & vbNewLine _
                                            & "       ON GOODS.NRS_BR_CD = MCR2.NRS_BR_CD             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_L = MCR2.CUST_CD_L             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_M = MCR2.CUST_CD_M             " & vbNewLine _
                                            & "      AND GOODS.CUST_CD_S = MCR2.CUST_CD_S             " & vbNewLine _
                                            & "      AND MCR2.PTN_ID  = 'AJ'                          " & vbNewLine _
                                            & "  --帳票パターン取得                                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2                          " & vbNewLine _
                                            & "       ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD               " & vbNewLine _
                                            & "      AND MR2.PTN_ID = MCR2.PTN_ID                     " & vbNewLine _
                                            & "      AND MR2.PTN_CD = MCR2.PTN_CD                     " & vbNewLine _
                                            & "      AND MR2.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                            & "  --存在しない場合の帳票パターン取得                   " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR3                          " & vbNewLine _
                                            & "       ON MR3.NRS_BR_CD = ZAI_TRS.NRS_BR_CD            " & vbNewLine _
                                            & "      AND MR3.PTN_ID = 'AJ'                            " & vbNewLine _
                                            & "      AND MR3.STANDARD_FLAG = '01'                     " & vbNewLine _
                                            & "      AND MR3.SYS_DEL_FLG = '0'                        " & vbNewLine

#End Region

#Region "WHERE句"

    ''' <summary>
    ''' 印刷データ抽出用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE As String = "WHERE                                 " & vbNewLine _
                                             & "ZAI_TRS.NRS_BR_CD = @NRS_BR_CD        " & vbNewLine _
                                             & "AND ZAI_TRS.INKO_PLAN_DATE >= @F_DATE " & vbNewLine _
                                             & "AND ZAI_TRS.INKO_PLAN_DATE <= @T_DATE " & vbNewLine _
                                             & "AND ZAI_TRS.SYS_DEL_FLG = '0'         " & vbNewLine

#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 印刷データ抽出用 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                  " & vbNewLine _
                                         & "  ZAI_TRS.INKO_PLAN_DATE  " & vbNewLine _
                                         & ", ZAI_TRS.CUST_CD_L       " & vbNewLine _
                                         & ", ZAI_TRS.GOODS_CD_NRS    " & vbNewLine _
                                         & ", ZAI_TRS.INKA_NO_L       " & vbNewLine _
                                         & ", ZAI_TRS.INKA_NO_M       " & vbNewLine _
                                         & ", ZAI_TRS.INKA_NO_S       " & vbNewLine

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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI610IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI610DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI610DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI610DAC.SQL_SELECT_WHERE)     'SQL構築(データ抽出用Where句)

        'パラメータの設定
        Call SetParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI610DAC", "SelectMPRT", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMI610IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMI610DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI610DAC.SQL_SELECT_FROM)      'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI610DAC.SQL_SELECT_WHERE)     'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMI610DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        'パラメータの設定
        Call SetParameter()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI610DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("INKO_PLAN_DATE", "INKO_PLAN_DATE")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ERR_MSG", "ERR_MSG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI610WK")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", .Item("F_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", .Item("T_DATE").ToString(), DBDataType.CHAR))

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
