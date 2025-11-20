' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI591    : 運賃請求明細書（DIC在庫部課、負担課別）
'  作  成  者       :  kurihara
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI591DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI591DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       UNCHIN.NRS_BR_CD                                 AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'A9'                                             AS PTN_ID    " & vbNewLine _
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
    ''' </remarks>
    Private Const SQL_MPrt_FROM As String = "  FROM $LM_TRN$..F_UNCHIN_TRS UNCHIN                                " & vbNewLine _
                                          & "       --運送L                                                      " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_TRN$..F_UNSO_L UNSO                      " & vbNewLine _
                                          & "            ON UNCHIN.UNSO_NO_L = UNSO.UNSO_NO_L                    " & vbNewLine _
                                          & "           AND UNCHIN.CUST_CD_L = UNSO.CUST_CD_L                    " & vbNewLine _
                                          & "           AND UNCHIN.CUST_CD_M = UNSO.CUST_CD_M                    " & vbNewLine _
                                          & "           AND UNCHIN.NRS_BR_CD = UNSO.NRS_BR_CD                    " & vbNewLine _
                                          & "           AND UNSO.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                          & "       --運送M                                                      " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_TRN$..F_UNSO_M UNSOM                     " & vbNewLine _
                                          & "            ON UNCHIN.UNSO_NO_L = UNSOM.UNSO_NO_L                   " & vbNewLine _
                                          & "           AND UNCHIN.UNSO_NO_M = UNSOM.UNSO_NO_M                   " & vbNewLine _
                                          & "           AND UNCHIN.NRS_BR_CD = UNSOM.NRS_BR_CD                   " & vbNewLine _
                                          & "           AND UNSOM.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                          & "       --商品マスタ                                                 " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_MST$..M_GOODS GOODS                      " & vbNewLine _
                                          & "            ON UNCHIN.NRS_BR_CD = GOODS.NRS_BR_CD                   " & vbNewLine _
                                          & "           AND UNSOM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                          & "       --運賃での荷主帳票パターン取得                               " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT MCR1                    " & vbNewLine _
                                          & "           ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                    " & vbNewLine _
                                          & "           AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                    " & vbNewLine _
                                          & "           AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                    " & vbNewLine _
                                          & "           AND '00' = MCR1.CUST_CD_S                                " & vbNewLine _
                                          & "           AND MCR1.PTN_ID = 'A9'                                   " & vbNewLine _
                                          & "       --帳票パターン取得                                           " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR1                          " & vbNewLine _
                                          & "           ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & "           AND MR1.PTN_ID = MCR1.PTN_ID                             " & vbNewLine _
                                          & "           AND MR1.PTN_CD = MCR1.PTN_CD                             " & vbNewLine _
                                          & "           AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "       --商品Mの荷主での荷主帳票パターン取得                        " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT MCR2                    " & vbNewLine _
                                          & "           ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                     " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                     " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                     " & vbNewLine _
                                          & "           AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                     " & vbNewLine _
                                          & "           AND MCR2.PTN_ID = 'A9'                                   " & vbNewLine _
                                          & "       --帳票パターン取得                                           " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR2                          " & vbNewLine _
                                          & "           ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                          & "           AND MR2.PTN_ID = MCR2.PTN_ID                             " & vbNewLine _
                                          & "           AND MR2.PTN_CD = MCR2.PTN_CD                             " & vbNewLine _
                                          & "           AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "       --存在しない場合の帳票パターン取得                           " & vbNewLine _
                                          & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR3                          " & vbNewLine _
                                          & "           ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                     " & vbNewLine _
                                          & "           AND MR3.PTN_ID = 'A9'                                    " & vbNewLine _
                                          & "           AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                          & "           AND MR3.SYS_DEL_FLG = '0'                                " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用 WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_WHERE As String = " WHERE                                        " & vbNewLine _
                                           & "       UNCHIN.NRS_BR_CD     =  @NRS_BR_CD     " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_L     =  @CUST_CD_L     " & vbNewLine _
                                           & "   AND UNCHIN.CUST_CD_M     =  @CUST_CD_M     " & vbNewLine

    '''' <summary>
    '''' 印刷データ抽出用 SELECT句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT As String = _
    '                                " SELECT                                                                " & vbNewLine _
    '                                & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID             " & vbNewLine _
    '                                & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID             " & vbNewLine _
    '                                & "        ELSE MR3.RPT_ID END                  AS RPT_ID               " & vbNewLine _
    '                                & "      , UNCHIN.NRS_BR_CD                     AS NRS_BR_CD            " & vbNewLine _
    '                                & "      , NRS_BR.NRS_BR_NM                     AS NRS_BR_NM            " & vbNewLine _
    '                                & "      , UNCHIN.CUST_CD_L                     AS CUST_CD_L            " & vbNewLine _
    '                                & "      , CUST  .CUST_NM_L                     AS CUST_NM_L            " & vbNewLine _
    '                                & "      , UNSOM .ZBUKA_CD                      AS ZBUKA_CD             " & vbNewLine _
    '                                & "      , SUM(UNCHIN.SEIQ_WT)                  AS SEIQ_WT              " & vbNewLine _
    '                                & "      , SUM(UNCHIN.DECI_UNCHIN)              AS DECI_UNCHIN          " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = _
                                    " SELECT                                                                " & vbNewLine _
                                    & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID             " & vbNewLine _
                                    & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID             " & vbNewLine _
                                    & "        ELSE MR3.RPT_ID END                  AS RPT_ID               " & vbNewLine _
                                    & "      , UNCHIN.NRS_BR_CD                     AS NRS_BR_CD            " & vbNewLine _
                                    & "      , NRS_BR.NRS_BR_NM                     AS NRS_BR_NM            " & vbNewLine _
                                    & "      , UNCHIN.CUST_CD_L                     AS CUST_CD_L            " & vbNewLine _
                                    & "      , CUST.CUST_NM_L                       AS CUST_NM_L            " & vbNewLine _
                                    & "      , UNSOM.ZBUKA_CD                       AS ZBUKA_CD             " & vbNewLine _
                                    & "      , SUBSTRING(UNSOM.ABUKA_CD,1,5)        AS ABUKA_CD             " & vbNewLine _
                                    & "      , SUM(UNCHIN.DECI_WT)                  AS SEIQ_WT              " & vbNewLine _
                                    & "      , SUM(UNCHIN.DECI_UNCHIN)              AS DECI_UNCHIN          " & vbNewLine _
                                    & "      , @F_DATE                              AS F_DATE               " & vbNewLine _
                                    & "      , @T_DATE                              AS T_DATE               " & vbNewLine




    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..F_UNCHIN_TRS UNCHIN                                " & vbNewLine _
                                     & "       --運送L                                                      " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_TRN$..F_UNSO_L UNSO                      " & vbNewLine _
                                     & "            ON UNCHIN.UNSO_NO_L = UNSO.UNSO_NO_L                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_L = UNSO.CUST_CD_L                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_M = UNSO.CUST_CD_M                    " & vbNewLine _
                                     & "           AND UNCHIN.NRS_BR_CD = UNSO.NRS_BR_CD                    " & vbNewLine _
                                     & "           AND UNSO.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                     & "       --運送M                                                      " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_TRN$..F_UNSO_M UNSOM                     " & vbNewLine _
                                     & "            ON UNCHIN.UNSO_NO_L = UNSOM.UNSO_NO_L                   " & vbNewLine _
                                     & "           AND UNCHIN.UNSO_NO_M = UNSOM.UNSO_NO_M                   " & vbNewLine _
                                     & "           AND UNCHIN.NRS_BR_CD = UNSOM.NRS_BR_CD                   " & vbNewLine _
                                     & "           AND UNSOM.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                     & "       --営業所マスタ                                               " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_NRS_BR NRS_BR                    " & vbNewLine _
                                     & "            ON UNCHIN.NRS_BR_CD = NRS_BR.NRS_BR_CD                  " & vbNewLine _
                                     & "       --荷主マスタ                                                 " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST CUST                        " & vbNewLine _
                                     & "            ON UNCHIN.NRS_BR_CD = CUST.NRS_BR_CD                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_L = CUST.CUST_CD_L                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_M = CUST.CUST_CD_M                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_S = CUST.CUST_CD_S                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_SS = CUST.CUST_CD_SS                  " & vbNewLine _
                                     & "       --商品マスタ                                                 " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_GOODS GOODS                      " & vbNewLine _
                                     & "            ON UNCHIN.NRS_BR_CD = GOODS.NRS_BR_CD                   " & vbNewLine _
                                     & "           AND UNSOM.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              " & vbNewLine _
                                     & "       --運賃での荷主帳票パターン取得                               " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT MCR1                    " & vbNewLine _
                                     & "           ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                    " & vbNewLine _
                                     & "           AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                    " & vbNewLine _
                                     & "           AND '00' = MCR1.CUST_CD_S                                " & vbNewLine _
                                     & "           AND MCR1.PTN_ID = 'A9'                                   " & vbNewLine _
                                     & "       --帳票パターン取得                                           " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR1                          " & vbNewLine _
                                     & "           ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                     & "           AND MR1.PTN_ID = MCR1.PTN_ID                             " & vbNewLine _
                                     & "           AND MR1.PTN_CD = MCR1.PTN_CD                             " & vbNewLine _
                                     & "           AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "       --商品Mの荷主での荷主帳票パターン取得                        " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT MCR2                    " & vbNewLine _
                                     & "           ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                     " & vbNewLine _
                                     & "           AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                     " & vbNewLine _
                                     & "           AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                     " & vbNewLine _
                                     & "           AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                     " & vbNewLine _
                                     & "           AND MCR2.PTN_ID = 'A9'                                   " & vbNewLine _
                                     & "       --帳票パターン取得                                           " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR2                          " & vbNewLine _
                                     & "           ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                     & "           AND MR2.PTN_ID = MCR2.PTN_ID                             " & vbNewLine _
                                     & "           AND MR2.PTN_CD = MCR2.PTN_CD                             " & vbNewLine _
                                     & "           AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "       --存在しない場合の帳票パターン取得                           " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR3                          " & vbNewLine _
                                     & "           ON  MR3.NRS_BR_CD = UNCHIN.NRS_BR_CD                     " & vbNewLine _
                                     & "           AND MR3.PTN_ID = 'A9'                                    " & vbNewLine _
                                     & "           AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                     & "           AND MR3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "       --届け先M ADD 2019/05/28  005720                             " & vbNewLine _
                                     & "       LEFT OUTER JOIN LM_MST..M_DEST DEST                          " & vbNewLine _
                                     & "           ON  DEST.NRS_BR_CD = UNSO.NRS_BR_CD                      " & vbNewLine _
                                     & "           AND DEST.CUST_CD_L = UNSO.CUST_CD_L                      " & vbNewLine _
                                     & "           AND DEST.DEST_CD   = UNSO.DEST_CD                        " & vbNewLine




    ''' <summary>                             
    ''' 印刷データ抽出用 GROUP BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                                                       " & vbNewLine _
                                    & "        UNCHIN.NRS_BR_CD                                             " & vbNewLine _
                                    & "      , NRS_BR.NRS_BR_NM                                             " & vbNewLine _
                                    & "      , UNCHIN.CUST_CD_L                                             " & vbNewLine _
                                    & "      , CUST.CUST_NM_L                                               " & vbNewLine _
                                    & "      , UNSOM.ZBUKA_CD                                               " & vbNewLine _
                                    & "      , SUBSTRING(UNSOM.ABUKA_CD,1,5)                                " & vbNewLine _
                                    & "      , MR2.PTN_CD                                                   " & vbNewLine _
                                    & "      , MR2.RPT_ID                                                   " & vbNewLine _
                                    & "      , MR1.PTN_CD                                                   " & vbNewLine _
                                    & "      , MR1.RPT_ID                                                   " & vbNewLine _
                                    & "      , MR3.RPT_ID                                                   " & vbNewLine


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                                               " & vbNewLine _
                                         & "   UNSOM.ZBUKA_CD                                       " & vbNewLine _
                                         & " , SUBSTRING(UNSOM.ABUKA_CD,1,5)                        " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI591IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI591DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMI591DAC.SQL_MPrt_FROM)      'SQL構築(帳票種別用FROM句)
        Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI591DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル(F_UNCHIN_TRS)対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル(F_UNCHIN_TRS)対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI591IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI591DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMI591DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        Call Me.SetConditionMasterSQL2()              'SQL構築(印刷データ抽出条件設定)
        Me._StrSql.Append(LMI591DAC.SQL_GROUP_BY)    'SQL構築(印刷データ抽出用 GROUP BY句)
        Me._StrSql.Append(LMI591DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI591DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ZBUKA_CD", "ZBUKA_CD")
        map.Add("ABUKA_CD", "ABUKA_CD")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("F_DATE", "F_DATE")
        map.Add("T_DATE", "T_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI591OUT")

        Return ds

    End Function

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
                Me._StrSql.Append(" UNCHIN.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            'whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            whereStr = .Item("CUST_CD_L").ToString()

            '(2012.07.17) 埼玉物流センター専用機能に修正 --- START -- 
            If String.IsNullOrEmpty(whereStr) = False Then
                '(2013.06.06)No.2057 Start
                Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
                ''営業所が50(埼玉)で荷主コードが'10001'の場合、'10002'も含める
                'If .Item("NRS_BR_CD").ToString() = "50" And .Item("CUST_CD_L").ToString() = "10001" Then
                '    Me._StrSql.Append(" AND (UNCHIN.CUST_CD_L = @CUST_CD_L OR UNCHIN.CUST_CD_L = '10002') ")
                'Else
                '    Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
                'End If
                '(2013.06.06)No.2057 End
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            'If String.IsNullOrEmpty(whereStr) = False Then
            '    '春日部(= "55")
            '    If .Item("NRS_BR_CD").ToString() = "55" Then
            '        Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
            '        Me._StrSql.Append(vbNewLine)
            '    Else
            '        '荷主コードが'10001'の場合、'10002'も含める
            '        If .Item("CUST_CD_L").ToString() = "10001" Then
            '            Me._StrSql.Append(" AND (UNCHIN.CUST_CD_L = @CUST_CD_L or UNCHIN.CUST_CD_L = '10002')")
            '            Me._StrSql.Append(vbNewLine)
            '        Else
            '            Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
            '            Me._StrSql.Append(vbNewLine)
            '        End If
            '    End If
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If
            '(2012.07.17) 埼玉物流センター専用機能に修正 ---  END  --

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNCHIN.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日(FROM)
            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO.ARR_PLAN_DATE >= @F_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '出荷日(TO)
            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO.ARR_PLAN_DATE <= @T_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
            End If

            '請求種別(進捗：未確定、確定)
            whereStr = .Item("SEIQ_SYUBETU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                Select Case whereStr

                    Case "00"       '未確定
                        Me._StrSql.Append(" AND UNCHIN.SEIQ_FIXED_FLAG = '00'")
                        Me._StrSql.Append(vbNewLine)

                    Case "01"       '確定
                        Me._StrSql.Append(" AND UNCHIN.SEIQ_FIXED_FLAG = '01'")
                        Me._StrSql.Append(vbNewLine)

                    Case Else       '全て

                End Select

            End If

            Me._StrSql.Append(" AND UNCHIN.SYS_DEL_FLG = '0'")
            Me._StrSql.Append(vbNewLine)

            Me._StrSql.Append(" AND UNCHIN.DECI_UNCHIN > 0")
            Me._StrSql.Append(vbNewLine)

            '春日部以外は対象
            If .Item("NRS_BR_CD").ToString() <> "55" Then
                Me._StrSql.Append(" AND (UNSO.MOTO_DATA_KB <> '10'" & _
                                    " OR UNCHIN.CUST_CD_L <> '10001'" & _
                                    " OR UNCHIN.CUST_CD_M <> '00'" & _
                                    " OR UNCHIN.CUST_CD_S <> '14'" & _
                                    " OR UNCHIN.CUST_CD_SS <> '00')")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

#If True Then   'ADD 2019/05/29 005720【LMS】特定荷主機能_古河HBFN請求(4/10,22,25日分別してデータ反映)の修正依頼(古河佐藤所長)→自動化 
    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL2()

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
                Me._StrSql.Append(" UNCHIN.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            'whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            whereStr = .Item("CUST_CD_L").ToString()

            '(2012.07.17) 埼玉物流センター専用機能に修正 --- START -- 
            If String.IsNullOrEmpty(whereStr) = False Then
                '(2013.06.06)No.2057 Start
                Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
                ''営業所が50(埼玉)で荷主コードが'10001'の場合、'10002'も含める
                'If .Item("NRS_BR_CD").ToString() = "50" And .Item("CUST_CD_L").ToString() = "10001" Then
                '    Me._StrSql.Append(" AND (UNCHIN.CUST_CD_L = @CUST_CD_L OR UNCHIN.CUST_CD_L = '10002') ")
                'Else
                '    Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
                'End If
                '(2013.06.06)No.2057 End
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            'If String.IsNullOrEmpty(whereStr) = False Then
            '    '春日部(= "55")
            '    If .Item("NRS_BR_CD").ToString() = "55" Then
            '        Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
            '        Me._StrSql.Append(vbNewLine)
            '    Else
            '        '荷主コードが'10001'の場合、'10002'も含める
            '        If .Item("CUST_CD_L").ToString() = "10001" Then
            '            Me._StrSql.Append(" AND (UNCHIN.CUST_CD_L = @CUST_CD_L or UNCHIN.CUST_CD_L = '10002')")
            '            Me._StrSql.Append(vbNewLine)
            '        Else
            '            Me._StrSql.Append(" AND UNCHIN.CUST_CD_L = @CUST_CD_L")
            '            Me._StrSql.Append(vbNewLine)
            '        End If
            '    End If
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If
            '(2012.07.17) 埼玉物流センター専用機能に修正 ---  END  --

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNCHIN.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日(FROM)
            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO.ARR_PLAN_DATE >= @F_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '出荷日(TO)
            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO.ARR_PLAN_DATE <= @T_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
            End If

            '請求種別(進捗：未確定、確定)
            whereStr = .Item("SEIQ_SYUBETU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                Select Case whereStr

                    Case "00"       '未確定
                        Me._StrSql.Append(" AND UNCHIN.SEIQ_FIXED_FLAG = '00'")
                        Me._StrSql.Append(vbNewLine)

                    Case "01"       '確定
                        Me._StrSql.Append(" AND UNCHIN.SEIQ_FIXED_FLAG = '01'")
                        Me._StrSql.Append(vbNewLine)

                    Case Else       '全て

                End Select

            End If

            Me._StrSql.Append(" AND UNCHIN.SYS_DEL_FLG = '0'")
            Me._StrSql.Append(vbNewLine)

            Me._StrSql.Append(" AND UNCHIN.DECI_UNCHIN > 0")
            Me._StrSql.Append(vbNewLine)

            '春日部以外は対象
            If .Item("NRS_BR_CD").ToString() <> "55" Then
                Me._StrSql.Append(" AND (UNSO.MOTO_DATA_KB <> '10'" & _
                                    " OR UNCHIN.CUST_CD_L <> '10001'" & _
                                    " OR UNCHIN.CUST_CD_M <> '00'" & _
                                    " OR UNCHIN.CUST_CD_S <> '14'" & _
                                    " OR UNCHIN.CUST_CD_SS <> '00')")
                Me._StrSql.Append(vbNewLine)
            End If
            'DEL Start 2019/11/20 C06974【LMS】特定荷主機能_運賃請求明細書(在庫部課別)が正しい金額で表示されるが(在庫・扱い部課別)と差異があるので同じ金額になるよう修正依頼
            'Me._StrSql.Append(" AND ( DEST.AD_1 is null")
            'Me._StrSql.Append(vbNewLine)
            'Me._StrSql.Append("  OR (DEST.AD_1 not like N'神奈川県横浜市%'")
            'Me._StrSql.Append(vbNewLine)
            'Me._StrSql.Append(" AND DEST.AD_1  not LIKE N'神奈川県川崎市%'))")
            'Me._StrSql.Append(vbNewLine)
            'DEL End   2019/11/20 C06974【LMS】特定荷主機能_運賃請求明細書(在庫部課別)が正しい金額で表示されるが(在庫・扱い部課別)と差異があるので同じ金額になるよう修正依頼

        End With

    End Sub
#End If

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
