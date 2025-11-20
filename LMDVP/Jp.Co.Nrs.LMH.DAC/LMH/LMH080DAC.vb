' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH080    : 
'  作  成  者       :  s.kobayashi
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH080DAC
''' </summary>
''' <remarks></remarks>
Public Class LMH080DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH080IN"
    Public Const TABLE_NM_OUT As String = "LMH080OUT"
    Public Const TABLE_NM_INDEL As String = "LMH080IN_DEL"

#Region "検索カウント"

    ''' <summary>
    ''' 検索カウント
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_DATA As String = " SELECT                                               " & vbNewLine _
                                           & " COUNT(H4_DELIVERY_NO)            AS SELECT_CNT " & vbNewLine

#End Region '検索カウント

#Region "検索SELECT"

    ''' <summary>
    ''' 検索SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                     " & vbNewLine _
                                            & "     H4_DELIVERY_NO                            AS H4_DELIVERY_NO           " & vbNewLine _
                                            & "    ,MIN(L2_ITEM_CODE)                         AS L2_ITEM_CODE             " & vbNewLine _
                                            & "    ,MIN(L2_NAME_INTERNAL)                     AS L2_NAME_INTERNAL         " & vbNewLine _
                                            & "    ,SUM(L2_QUANTITY)                          AS L2_QUANTITY              " & vbNewLine _
                                            & "    ,SUM(L2_GROSS)                             AS L2_GROSS                 " & vbNewLine _
                                            & "    ,MIN(L2_UOM)                               AS L2_UOM                   " & vbNewLine _
                                            & "    ,MIN(L2_BATCH_NO)                          AS L2_BATCH_NO              " & vbNewLine _
                                            & "    ,H3_NAME_ALL                               AS H3_NAME_ALL              " & vbNewLine _
                                            & "    ,OUTER_PKG                                 AS OUTER_PKG                " & vbNewLine _
                                            & "    ,MIN(L2_QUANTITY_UOM)                      AS L2_QUANTITY_UOM          " & vbNewLine _
                                            & "    ,INKA_DATE                                 AS INKA_DATE                " & vbNewLine _
                                            & "    ,INKA_NB                                   AS INKA_NB                  " & vbNewLine _
                                            & "    ,MISOUCYAKU_DATE                           AS MISOUCYAKU_DATE          " & vbNewLine _
                                            & "    ,CRT_DATE                                  AS CRT_DATE                 " & vbNewLine _
                                            & "    ,INKA_CTL_NO_L                             AS INKA_CTL_NO_L            " & vbNewLine _
                                            & "    ,FILE_NAME                                 AS FILE_NAME                " & vbNewLine _
                                            & "    ,MIN(REC_NO)                               AS REC_NO                   " & vbNewLine _
                                            & "    ,CUST_CD_L                                 AS CUST_CD_L                " & vbNewLine _
                                            & "    ,CUST_CD_M                                 AS CUST_CD_M                " & vbNewLine _
                                            & "    ,NRS_BR_CD                                 AS NRS_BR_CD                " & vbNewLine _
                                            & "    ,DEL_KB                                    AS DEL_KB                   " & vbNewLine _
                                            & "    ,SYS_UPD_DATE                              AS SYS_UPD_DATE             " & vbNewLine _
                                            & "    ,SYS_UPD_TIME                              AS SYS_UPD_TIME             " & vbNewLine

    ''' <summary>
    ''' 検索SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_BASE As String = "FROM (                                                                          " & vbNewLine _
                                                    & "SELECT DISTINCT                                                                 " & vbNewLine _
                                                    & "     HED.H4_DELIVERY_NO                        AS H4_DELIVERY_NO                " & vbNewLine _
                                                    & "    ,DTL.L2_ITEM_CODE                          AS L2_ITEM_CODE                  " & vbNewLine _
                                                    & "    ,DTL.L2_NAME_INTERNAL                      AS L2_NAME_INTERNAL              " & vbNewLine _
                                                    & "    ,DTL.L2_QUANTITY                           AS L2_QUANTITY                   " & vbNewLine _
                                                    & "    ,DTL.L2_GROSS                              AS L2_GROSS                      " & vbNewLine _
                                                    & "    ,DTL.L2_UOM                                AS L2_UOM                        " & vbNewLine _
                                                    & "    ,DTL.L2_BATCH_NO                           AS L2_BATCH_NO                   " & vbNewLine _
                                                    & "    ,HED.H3_NAME1 + HED.H3_NAME2  + HED.H3_NAME3   AS H3_NAME_ALL               " & vbNewLine _
                                                    & "    ,0                                             AS OUTER_PKG                 " & vbNewLine _
                                                    & "    ,DTL.L2_QUANTITY_UOM                           AS L2_QUANTITY_UOM           " & vbNewLine _
                                                    & "    ,INKA.INKA_DATE                                 AS INKA_DATE                 " & vbNewLine _
                                                    & "    ,INKA.INKA_TTL_NB                               AS INKA_NB                   " & vbNewLine _
                                                    & "    ,HED.MISOUCYAKU_DATE                           AS MISOUCYAKU_DATE           " & vbNewLine _
                                                    & "    ,HED.CRT_DATE                                  AS CRT_DATE                  " & vbNewLine _
                                                    & "    ,INKA.INKA_NO_L                             AS INKA_CTL_NO_L                 " & vbNewLine _
                                                    & "    ,HED.FILE_NAME                                 AS FILE_NAME                 " & vbNewLine _
                                                    & "    ,HED.REC_NO                                    AS REC_NO                    " & vbNewLine _
                                                    & "    ,HED.CUST_CD_L                                 AS CUST_CD_L                 " & vbNewLine _
                                                    & "    ,HED.CUST_CD_M                                 AS CUST_CD_M                 " & vbNewLine _
                                                    & "    ,HED.NRS_BR_CD                                 AS NRS_BR_CD                 " & vbNewLine _
                                                    & "    ,HED.DEL_KB                                    AS DEL_KB                    " & vbNewLine _
                                                    & "    ,HED.SYS_UPD_DATE                              AS SYS_UPD_DATE              " & vbNewLine _
                                                    & "    ,HED.SYS_UPD_TIME                              AS SYS_UPD_TIME              " & vbNewLine _
                                                    & "    ,DTL.GYO                                       AS GYO                       " & vbNewLine
#End Region '検索SELECT

#Region "検索FROM句"

    ''' <summary>
    ''' 検索FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                    " & vbNewLine _
                                            & "    $LM_TRN$..H_INKAEDI_HED_UTI HED                         " & vbNewLine _
                                            & "    LEFT JOIN $LM_TRN$..H_INKAEDI_DTL_UTI DTL               " & vbNewLine _
                                            & "    ON HED.CRT_DATE = DTL.CRT_DATE                          " & vbNewLine _
                                            & "    AND HED.FILE_NAME = DTL.FILE_NAME                       " & vbNewLine _
                                            & "    AND HED.REC_NO = DTL.REC_NO                             " & vbNewLine _
                                            & "    LEFT JOIN (SELECT INL.NRS_BR_CD,INL.INKA_NO_L,INS.SERIAL_NO,INL.INKA_DATE,INL.INKA_TTL_NB FROM " & vbNewLine _
                                            & "    $LM_TRN$..B_INKA_S INS                                  " & vbNewLine _
                                            & "    LEFT JOIN $LM_TRN$..B_INKA_M INM                       " & vbNewLine _
                                            & "    ON INS.NRS_BR_CD = INM.NRS_BR_CD                        " & vbNewLine _
                                            & "    AND INS.INKA_NO_L = INM.INKA_NO_L                       " & vbNewLine _
                                            & "    AND INS.INKA_NO_M = INM.INKA_NO_M                       " & vbNewLine _
                                            & "    LEFT JOIN $LM_TRN$..B_INKA_L INL                       " & vbNewLine _
                                            & "    ON INM.NRS_BR_CD = INL.NRS_BR_CD                        " & vbNewLine _
                                            & "    AND INM.INKA_NO_L = INL.INKA_NO_L                       " & vbNewLine _
                                            & "    WHERE INL.NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
                                            & "    AND INL.CUST_CD_L = @CUST_CD_L                         " & vbNewLine _
                                            & "    AND INL.CUST_CD_M = @CUST_CD_M                        " & vbNewLine _
                                            & "    AND INL.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                            & "    AND INM.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                            & "    AND INS.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                            & "    ) INKA ON HED.NRS_BR_CD = INKA.NRS_BR_CD                        " & vbNewLine _
                                            & "    AND HED.H4_DELIVERY_NO = INKA.SERIAL_NO                  " & vbNewLine

#End Region '検索FROM句


    Private Const SQL_SELECT_FROMEND As String = ") BASE                        " & vbNewLine

#Region "検索GROUPBY句"

    ''' <summary>
    ''' 検索GROUPBY句(INKA,HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUPBY As String = "GROUP BY                                               " & vbNewLine _
                                                    & "     HED.H4_DELIVERY_NO                           " & vbNewLine _
                                                    & "    ,DTL.L2_ITEM_CODE                             " & vbNewLine _
                                                    & "    ,DTL.L2_NAME_INTERNAL                         " & vbNewLine _
                                                    & "    ,DTL.L2_QUANTITY                              " & vbNewLine _
                                                    & "    ,DTL.L2_GROSS                                 " & vbNewLine _
                                                    & "    ,DTL.L2_UOM                                   " & vbNewLine _
                                                    & "    ,DTL.L2_BATCH_NO                              " & vbNewLine _
                                                    & "    ,HED.H3_NAME1 + HED.H3_NAME2  + HED.H3_NAME3  " & vbNewLine _
                                                    & "    ,DTL.L2_QUANTITY_UOM                          " & vbNewLine _
                                                    & "    ,INKA.INKA_DATE                               " & vbNewLine _
                                                    & "    ,INKA.INKA_TTL_NB                             " & vbNewLine _
                                                    & "    ,HED.MISOUCYAKU_DATE                          " & vbNewLine _
                                                    & "    ,HED.CRT_DATE                                 " & vbNewLine _
                                                    & "    ,INKA.INKA_NO_L                               " & vbNewLine _
                                                    & "    ,HED.FILE_NAME                                " & vbNewLine _
                                                    & "    ,HED.REC_NO                                   " & vbNewLine _
                                                    & "    ,HED.CUST_CD_L                                " & vbNewLine _
                                                    & "    ,HED.CUST_CD_M                                " & vbNewLine _
                                                    & "    ,HED.NRS_BR_CD                                " & vbNewLine _
                                                    & "    ,HED.DEL_KB                                   " & vbNewLine _
                                                    & "    ,HED.SYS_UPD_DATE                             " & vbNewLine _
                                                    & "    ,HED.SYS_UPD_TIME                             " & vbNewLine _
                                                    & "    ,DTL.GYO                                      " & vbNewLine


    ''' <summary>
    ''' 検索GROUPBY句(BASE)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GROUPBY_BASE As String = "GROUP BY                      " & vbNewLine _
                                                & "     H4_DELIVERY_NO           " & vbNewLine _
                                                & "    ,H3_NAME_ALL              " & vbNewLine _
                                                & "    ,OUTER_PKG                " & vbNewLine _
                                                & "    ,INKA_DATE                " & vbNewLine _
                                                & "    ,INKA_NB                  " & vbNewLine _
                                                & "    ,MISOUCYAKU_DATE          " & vbNewLine _
                                                & "    ,CRT_DATE                 " & vbNewLine _
                                                & "    ,INKA_CTL_NO_L            " & vbNewLine _
                                                & "    ,FILE_NAME                " & vbNewLine _
                                                & "    ,CUST_CD_L                " & vbNewLine _
                                                & "    ,CUST_CD_M                " & vbNewLine _
                                                & "    ,NRS_BR_CD                " & vbNewLine _
                                                & "    ,DEL_KB                   " & vbNewLine _
                                                & "    ,SYS_UPD_DATE             " & vbNewLine _
                                                & "    ,SYS_UPD_TIME             " & vbNewLine
#End Region '検索ORDERBY句

#Region "検索ORDERBY句"

    ''' <summary>
    ''' 検索ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDERBY As String = " ORDER BY H4_DELIVERY_NO "

#End Region '検索ORDERBY句

#Region "削除処理"
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_HED_UTI As String = " UPDATE " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_HED_UTI " & vbNewLine _
                                        & " SET " & vbNewLine _
                                        & " DEL_KB = '1' " & vbNewLine _
                                        & ", SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
                                        & ", SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
                                        & ", SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
                                        & ", SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
                                        & " ,SYS_DEL_FLG = '1' " & vbNewLine _
                                        & " WHERE " & vbNewLine _
                                        & " CRT_DATE = @CRT_DATE " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " FILE_NAME = @FILE_NAME " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " REC_NO = @REC_NO " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA " & vbNewLine

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DTL_UTI As String = " UPDATE " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_DTL_UTI " & vbNewLine _
                                        & " SET " & vbNewLine _
                                        & " DEL_KB = '1' " & vbNewLine _
                                        & ", SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
                                        & ", SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
                                        & ", SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
                                        & ", SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
                                        & " ,SYS_DEL_FLG = '1' " & vbNewLine _
                                        & " WHERE " & vbNewLine _
                                        & " CRT_DATE = @CRT_DATE " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " FILE_NAME = @FILE_NAME " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " REC_NO = @REC_NO " & vbNewLine


#End Region '一括変更

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 条件設定用
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
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region 'Field

#Region "Method"

#Region "検索件数取得処理"

    ''' <summary>
    ''' 検索件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH080DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH080DAC.SQL_COUNT_DATA)
        'SQL作成
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_DATA_BASE)
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_FROMEND)
   
        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH080DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMH080DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_DATA)
        'SQL作成
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_DATA_BASE)
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_GROUPBY)
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_FROMEND)
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_GROUPBY_BASE)
        Me._StrSql.Append(LMH080DAC.SQL_SELECT_ORDERBY)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH080DAC", "SelectListData", cmd)

        'Debug.Print(cmd.CommandText)
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("H4_DELIVERY_NO", "H4_DELIVERY_NO")
        map.Add("L2_ITEM_CODE", "L2_ITEM_CODE")
        map.Add("L2_NAME_INTERNAL", "L2_NAME_INTERNAL")
        map.Add("L2_QUANTITY", "L2_QUANTITY")
        map.Add("L2_GROSS", "L2_GROSS")
        map.Add("L2_UOM", "L2_UOM")
        map.Add("L2_BATCH_NO", "L2_BATCH_NO")
        map.Add("H3_NAME_ALL", "H3_NAME_ALL")
        map.Add("OUTER_PKG", "OUTER_PKG")
        map.Add("L2_QUANTITY_UOM", "L2_QUANTITY_UOM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("MISOUCYAKU_DATE", "MISOUCYAKU_DATE")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DEL_KB", "DEL_KB")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMH080DAC.TABLE_NM_OUT)

        Return ds

    End Function

#End Region

#Region "削除"

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>削除</remarks>
    Private Function DeleteHedUTI(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0
        Dim strSql As String = String.Empty

        'DataSetのIN情報を取得
        Dim dtIndel As DataTable = ds.Tables(LMH080DAC.TABLE_NM_INDEL)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        strSql = Me.SetSchemaNm(LMH080DAC.SQL_UPD_HED_UTI, dtIndel.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL作成
        Me._StrSql.Append(strSql)

        'SQLパラメータ設定
        Call Me.SetUpdDelPrm(dtIndel)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH080DAC", "UpdateHedDel", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>削除</remarks>
    Private Function DeleteDtlUTI(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0
        Dim strSql As String = String.Empty

        'DataSetのIN情報を取得
        Dim dtIndel As DataTable = ds.Tables(LMH080DAC.TABLE_NM_INDEL)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        strSql = Me.SetSchemaNm(LMH080DAC.SQL_UPD_DTL_UTI, dtIndel.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL作成
        Me._StrSql.Append(strSql)

        'SQLパラメータ設定
        Call Me.SetUpdDelPrm(dtIndel)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH080DAC", "UpdateDtlDel", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#End Region 'Method

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="brCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        '必須条件
        Me._StrSql.Append("    HED.NRS_BR_CD = @NRS_BR_CD                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND HED.CUST_CD_L = @CUST_CD_L                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND HED.CUST_CD_M = @CUST_CD_M                               ")
        Me._StrSql.Append(vbNewLine)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M"), DBDataType.CHAR))

        With Me._Row

            '荷主コード(大)
            whereStr = .Item("CRT_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND @CRT_DATE_FROM <=  HED.SYS_ENT_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CRT_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.SYS_ENT_DATE <= @CRT_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("DELIVERY_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.H4_DELIVERY_NO LIKE @DELIVERY_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.L2_ITEM_CODE = @GOODS_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", String.Concat(whereStr), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.L2_NAME_INTERNAL LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("QT_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.L2_QUANTITY LIKE @QT_UT")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@QT_UT", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.L2_BATCH_NO LIKE @LOT_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.H3_NAME1 + HED.H3_NAME2  + HED.H3_NAME3 LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("PKG_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND DTL.L2_QUANTITY_UOM LIKE @PKG_UT")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("INKA_CTL_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INKA.INKA_CTL_NO_L LIKE @INKA_CTL_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("FILE_NAME").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.FILE_NAME LIKE @FILE_NAME")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND HED.REC_NO = @REC_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("VIEW_KB1").ToString()
            If "1".Equals(whereStr) = True Then
                Me._StrSql.Append(" AND HED.DEL_KB = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND INKA.INKA_NO_L IS null ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND HED.MISOUCYAKU_DATE = '' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND HED.MISOUCYAKU_SHORI_FLG = '0' ")
                Me._StrSql.Append(vbNewLine)
                '2012.12.08 umano 入荷対象外データも外す 修正START
                Me._StrSql.Append(" AND HED.INKA_TAG_FLG = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND DTL.INKA_TAG_FLG = '0' ")
                '2012.12.08 umano 入荷対象外データも外す 修正END

            End If
            whereStr = .Item("VIEW_KB2").ToString()
            If "1".Equals(whereStr) = True Then
                Me._StrSql.Append(" AND HED.DEL_KB = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND (NOT INKA.INKA_NO_L IS null AND INKA.INKA_NO_L <> '' )")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND HED.MISOUCYAKU_DATE = '' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND HED.MISOUCYAKU_SHORI_FLG = '0' ")
                Me._StrSql.Append(vbNewLine)
                '2012.12.08 umano 入荷対象外データも外す 修正START
                Me._StrSql.Append(" AND HED.INKA_TAG_FLG = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND DTL.INKA_TAG_FLG = '0' ")
                '2012.12.08 umano 入荷対象外データも外す 修正END
            End If

            whereStr = .Item("VIEW_KB3").ToString()
            If "1".Equals(whereStr) = True Then
                Me._StrSql.Append(" AND HED.DEL_KB = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND HED.MISOUCYAKU_DATE <> '' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND HED.MISOUCYAKU_SHORI_FLG = '1' ")
                Me._StrSql.Append(vbNewLine)
                '2012.12.08 umano 入荷対象外データも外す 修正START
                Me._StrSql.Append(" AND HED.INKA_TAG_FLG = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND DTL.INKA_TAG_FLG = '0' ")
                '2012.12.08 umano 入荷対象外データも外す 修正END
            End If

            whereStr = .Item("VIEW_KB4").ToString()
            If "1".Equals(whereStr) = True Then
                Me._StrSql.Append(" AND HED.DEL_KB = '1' ")
                Me._StrSql.Append(vbNewLine)
                '2012.12.08 umano 入荷対象外データも外す 修正START
                Me._StrSql.Append(" AND HED.INKA_TAG_FLG = '0' ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND DTL.INKA_TAG_FLG = '0' ")
                '2012.12.08 umano 入荷対象外データも外す 修正END
            End If

            whereStr = .Item("VIEW_KB5").ToString()
            If "1".Equals(whereStr) = True Then
                Me._StrSql.Append(" AND HED.DEL_KB = '0' ")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdDelPrm(ByVal dtIndel As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", dtIndel.Rows(0).Item("CRT_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", dtIndel.Rows(0).Item("FILE_NAME"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", dtIndel.Rows(0).Item("REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", dtIndel.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", dtIndel.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

#End Region 'SQL

End Class
