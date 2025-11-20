' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC030    : 送付状番号入力
'  作  成  者       :  nishikawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC030DAC
''' </summary>
''' <remarks></remarks>
Public Class LMC030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "名称取得"

    ''' <summary>
    ''' 名称取得    
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                   " & vbNewLine _
                                            & " M_UNSOCO.UNSOCO_NM  + M_UNSOCO.UNSOCO_BR_NM AS UNSOCO_NM " & vbNewLine _
                                            & ",CASE WHEN C_OUTKA_L.DEST_KB = '00' THEN M_DEST.DEST_NM   " & vbNewLine _
                                            & "      ELSE C_OUTKA_L.DEST_NM                              " & vbNewLine _
                                            & " END AS DEST_NM                                           " & vbNewLine _
                                            & " FROM       $LM_TRN$..C_OUTKA_L C_OUTKA_L                 " & vbNewLine _
                                            & " LEFT JOIN  $LM_TRN$..F_UNSO_L F_UNSO_L                   " & vbNewLine _
                                            & " ON  C_OUTKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD             " & vbNewLine _
                                            & " AND C_OUTKA_L.OUTKA_NO_L = F_UNSO_L.INOUTKA_NO_L         " & vbNewLine _
                                            & " AND F_UNSO_L.MOTO_DATA_KB = '20'                         " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_UNSOCO M_UNSOCO                    " & vbNewLine _
                                            & " ON  F_UNSO_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD              " & vbNewLine _
                                            & " AND F_UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                " & vbNewLine _
                                            & " AND F_UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD          " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_DEST M_DEST                        " & vbNewLine _
                                            & " ON  C_OUTKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD               " & vbNewLine _
                                            & " AND C_OUTKA_L.DEST_CD = M_DEST.DEST_CD                   " & vbNewLine



#End Region '名称取得

#Region "保存"

    Private Const SQL_SELECT_UPD_DATA As String = " SELECT COUNT(C_OUTKA_L.NRS_BR_CD)  AS SELECT_CNT                       " & vbNewLine _
                                             & " FROM       $LM_TRN$..C_OUTKA_L C_OUTKA_L                " & vbNewLine _
                                             & " LEFT JOIN  $LM_TRN$..F_UNSO_L F_UNSO_L                  " & vbNewLine _
                                             & " ON  C_OUTKA_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD            " & vbNewLine _
                                             & " AND C_OUTKA_L.OUTKA_NO_L = F_UNSO_L.INOUTKA_NO_L        " & vbNewLine _
                                             & " AND F_UNSO_L.MOTO_DATA_KB = '20'                        " & vbNewLine


    Private Const SQL_UPDATE_OUTKAL As String = " UPDATE                                                 " & vbNewLine _
                                              & " $LM_TRN$..C_OUTKA_L                                    " & vbNewLine _
                                              & " SET                                                    " & vbNewLine _
                                              & " DENP_NO = @DENP_NO                                     " & vbNewLine _
                                              & ", SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                              & ", SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                              & ", SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                              & ", SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                              & " WHERE                                                  " & vbNewLine _
                                              & " NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                              & " AND                                                    " & vbNewLine _
                                              & " OUTKA_NO_L = @OUTKA_NO_L                               " & vbNewLine



    Private Const SQL_UPDATE_UNSO As String = " UPDATE                                                   " & vbNewLine _
                                            & " $LM_TRN$..F_UNSO_L                                       " & vbNewLine _
                                            & " SET                                                      " & vbNewLine _
                                            & " DENP_NO = @DENP_NO                                       " & vbNewLine _
                                            & ", SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
                                            & ", SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
                                            & ", SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
                                            & ", SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
                                            & " WHERE                                                    " & vbNewLine _
                                            & " NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                                            & " AND                                                      " & vbNewLine _
                                            & " INOUTKA_NO_L = @OUTKA_NO_L                               " & vbNewLine _
                                            & " AND                                                      " & vbNewLine _
                                            & " F_UNSO_L.MOTO_DATA_KB = '20'                             " & vbNewLine




#End Region '更新

#End Region 'Const

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

#Region "名称取得(更新)処理"

    ''' <summary>
    ''' 名称取得対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LMC030DAC.SQL_SELECT_DATA, Me._Row.Item("NRS_BR_CD").ToString()))

        '条件設定
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC030DAC", "SelectListData", cmd)

        'Debug.Print(cmd.CommandText)
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("DEST_NM", "DEST_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC030OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC030OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#Region "保存処理"


    ''' <summary>
    ''' 保存対象データ件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUpdData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LMC030DAC.SQL_SELECT_UPD_DATA, Me._Row.Item("NRS_BR_CD").ToString()))

        '条件設定
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC030DAC", "SelectUpdData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function



    ''' <summary>
    ''' 出荷Lテーブル更新（保存時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateDataOutkaL(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMC030IN")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LMC030DAC.SQL_UPDATE_OUTKAL, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.SetUpdatePrm(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        '処理件数登録
        updCnt = MyBase.GetUpdateResult(cmd)
        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 運送Lテーブル更新（保存時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateDataUnsoL(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMC030IN")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LMC030DAC.SQL_UPDATE_UNSO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.SetUpdatePrm(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        '処理件数登録
        updCnt = MyBase.GetUpdateResult(cmd)
        MyBase.SetResultCount(updCnt)

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

        Me._StrSql.Append("WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append("C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（保存）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdatePrm(ByVal ds As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_NO", ds.Rows(0).Item("DENP_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", ds.Rows(0).Item("OUTKA_NO_L"), DBDataType.CHAR))

    End Sub

#End Region 'SQL

End Class
