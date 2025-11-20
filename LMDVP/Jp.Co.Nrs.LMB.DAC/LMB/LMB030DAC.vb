' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 入荷管理
'  プログラムID     :  LMB030DAC : 入荷印刷指示
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "Update"

    ''' <summary>
    ''' 更新SQL(保存)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_TRN$..B_INKA_L SET                              " & vbNewLine _
                                       & "        HOUKOKUSYO_PR_DATE   = @HOUKOKUSYO_PR_DATE         " & vbNewLine _
                                       & "       ,HOUKOKUSYO_PR_USER   = @HOUKOKUSYO_PR_USER         " & vbNewLine _
                                       & "       ,SYS_UPD_DATE         = @SYS_UPD_DATE               " & vbNewLine _
                                       & "       ,SYS_UPD_TIME         = @SYS_UPD_TIME               " & vbNewLine _
                                       & "       ,SYS_UPD_PGID         = @SYS_UPD_PGID               " & vbNewLine _
                                       & "       ,SYS_UPD_USER         = @SYS_UPD_USER               " & vbNewLine _
                                       & "WHERE                                                      " & vbNewLine _
                                       & "   NRS_BR_CD   = @NRS_BR_CD                                " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   @INKA_DATE_FROM <= INKA_DATE                            " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   INKA_DATE  <= @INKA_DATE_TO                             " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   SYS_DEL_FLG = '0'                                       " & vbNewLine


    ''' <summary>
    ''' 更新SQL(入荷進捗区分更新)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_STATE As String = "UPDATE $LM_TRN$..B_INKA_L SET                   " & vbNewLine _
                                       & "        INKA_STATE_KB        = '90'  --報告済み　          " & vbNewLine _
                                       & "WHERE                                                      " & vbNewLine _
                                       & "   NRS_BR_CD   = @NRS_BR_CD                                " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   @INKA_DATE_FROM <= INKA_DATE                            " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   INKA_DATE  <= @INKA_DATE_TO                             " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                       & " AND                                                       " & vbNewLine _
                                       & "   INKA_STATE_KB >= '50'                                   " & vbNewLine

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

#Region "更新"

    ''' <summary>
    ''' 入荷Lテーブル更新(印刷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷Lテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaLPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB030DAC.SQL_UPDATE)      'SQL構築(データ抽出用Update句)
        Me._StrSql.Append(Me.SetWhereSQL())          '条件設定

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB030DAC", "UpdateInkaLPrintData", cmd)

        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("G021")
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 入荷Lテーブル更新(印刷 入荷進捗区分)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷Lテーブル更新SQLの構築・発行</remarks>
    Private Function UpdateInkaState(ByVal ds As DataSet) As DataSet

        'DataSetのOUT情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB030DAC.SQL_UPDATE_INKA_STATE)      'SQL構築(データ抽出用Update句)
        Me._StrSql.Append(Me.SetWhereSQL_InkaState())          '条件設定

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB030DAC", "UpdateInkaState", cmd)

        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("G021")
            Return ds
        End If

        Return ds

    End Function



#End Region '更新

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

    ''' <summary>
    ''' 条件文設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetWhereSQL() As String

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append(" AND CUST_CD_L LIKE @CUST_CD_L ")
                andstr.Append(vbNewLine)
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append(" AND CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
            End If

            whereStr = .Item("INKA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = True Then
                andstr.Append(" AND INKA_KB <> '50' ")
                andstr.Append(vbNewLine)
            End If

            whereStr = .Item("SYS_ENT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append(" AND SYS_ENT_DATE = @SYS_ENT_DATE ")
                andstr.Append(vbNewLine)
            End If

        End With

        Return andstr.ToString()

    End Function


    ''' <summary>
    ''' 条件文設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetWhereSQL_InkaState() As String

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append(" AND CUST_CD_L LIKE @CUST_CD_L ")
                andstr.Append(vbNewLine)
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append(" AND CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
            End If

            whereStr = .Item("SYS_ENT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                andstr.Append(" AND SYS_ENT_DATE = @SYS_ENT_DATE ")
                andstr.Append(vbNewLine)
            End If

        End With

        Return andstr.ToString()

    End Function

#End Region 'SQL

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール()
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", .Item("INKA_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", .Item("INKA_DATE_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_USER", MyBase.GetUserID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat("%", .Item("CUST_CD_L").ToString(), "%"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat("%", .Item("CUST_CD_M").ToString(), "%"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", .Item("SYS_ENT_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region 'パラメータ設定

End Class
