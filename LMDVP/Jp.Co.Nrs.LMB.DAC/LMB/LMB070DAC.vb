' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB070DAC : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB070DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB070DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String =
          " SELECT                                      " & vbNewLine _
        & "     COUNT(1)           AS SELECT_CNT        " & vbNewLine

    ''' <summary>
    ''' データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String =
          " SELECT                  " & vbNewLine _
        & "     PHOTO.NO            " & vbNewLine _
        & "   , PHOTO.SHOHIN_NM     " & vbNewLine _
        & "   , PHOTO.SATSUEI_DATE  " & vbNewLine _
        & "   , USERHDR.USER_LNM    " & vbNewLine _
        & "   , PHOTO.SYS_UPD_DATE  " & vbNewLine _
        & "   , PHOTO.SYS_UPD_TIME  " & vbNewLine _
        & "   , PPATH.FILE_PATH     " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_BASE As String =
          "FROM                                         " & vbNewLine _
        & "    AB_DB..NP_PHOTO PHOTO                    " & vbNewLine _
        & "    LEFT JOIN                                " & vbNewLine _
        & "        AB_DB..NP_PHOTO_PATH PPATH           " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "        PPATH.NO = PHOTO.NO                  " & vbNewLine _
        & "    AND PPATH.SYS_DEL_FLG = '0'              " & vbNewLine _
        & "    LEFT JOIN                                " & vbNewLine _
        & "        ABM_DB..M_USER_HDR USERHDR           " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "        USERHDR.USER_CD = PHOTO.SYS_ENT_USER " & vbNewLine _
        & "    AND USERHDR.SYS_DEL_FLG = '0'            " & vbNewLine _
        & "WHERE                                        " & vbNewLine _
        & "    PHOTO.SYS_DEL_FLG = '0'                  " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String =
          "    ORDER BY             " & vbNewLine _
        & "     PHOTO.NO            " & vbNewLine _
        & "   , PPATH.FILE_PATH     " & vbNewLine

#End Region


#End Region

#Region "INSERT SQL"

    ''' <summary>
    ''' 入荷写真データ追加用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_B_INKA_PHOTO As String =
             "INSERT INTO $LM_TRN$..B_INKA_PHOTO" & vbNewLine _
            & "(              " & vbNewLine _
            & " NRS_BR_CD     " & vbNewLine _
            & ",INKA_NO_L     " & vbNewLine _
            & ",INKA_NO_M     " & vbNewLine _
            & ",INKA_NO_S     " & vbNewLine _
            & ",NO            " & vbNewLine _
            & ",FILE_PATH     " & vbNewLine _
            & ",SYS_ENT_DATE  " & vbNewLine _
            & ",SYS_ENT_TIME  " & vbNewLine _
            & ",SYS_ENT_PGID  " & vbNewLine _
            & ",SYS_ENT_USER  " & vbNewLine _
            & ",SYS_UPD_DATE  " & vbNewLine _
            & ",SYS_UPD_TIME  " & vbNewLine _
            & ",SYS_UPD_PGID  " & vbNewLine _
            & ",SYS_UPD_USER  " & vbNewLine _
            & ",SYS_DEL_FLG   " & vbNewLine _
            & " )VALUES(      " & vbNewLine _
            & " @NRS_BR_CD    " & vbNewLine _
            & ",@INKA_NO_L    " & vbNewLine _
            & ",@INKA_NO_M    " & vbNewLine _
            & ",@INKA_NO_S    " & vbNewLine _
            & ",@NO           " & vbNewLine _
            & ",@FILE_PATH    " & vbNewLine _
            & ",@SYS_ENT_DATE " & vbNewLine _
            & ",@SYS_ENT_TIME " & vbNewLine _
            & ",@SYS_ENT_PGID " & vbNewLine _
            & ",@SYS_ENT_USER " & vbNewLine _
            & ",@SYS_UPD_DATE " & vbNewLine _
            & ",@SYS_UPD_TIME " & vbNewLine _
            & ",@SYS_UPD_PGID " & vbNewLine _
            & ",@SYS_UPD_USER " & vbNewLine _
            & ",@SYS_DEL_FLG  " & vbNewLine _
            & ")              " & vbNewLine

#End Region

#Region "INSERT SQL"

    ''' <summary>
    ''' 写真データ追加用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_NP_PHOTO As String = "UPDATE AB_DB..NP_PHOTO SET    " & vbNewLine _
                                                & " SHOHIN_NM    = @SHOHIN_NM    " & vbNewLine _
                                                & ",SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
                                                & ",SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
                                                & ",SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
                                                & ",SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
                                                & "WHERE                         " & vbNewLine _
                                                & "    NO           = @NO           " & vbNewLine _
                                                & "AND SYS_UPD_DATE = @GUI_UPD_DATE " & vbNewLine _
                                                & "AND SYS_UPD_TIME = @GUI_UPD_TIME " & vbNewLine

#End Region

#Region "DELETE SQL"

    ''' <summary>
    ''' 入荷写真データ削除用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_B_INKA_PHOTO As String =
              "DELETE FROM                                  " & vbNewLine _
            & "     $LM_TRN$..B_INKA_PHOTO                  " & vbNewLine _
            & "WHERE                                        " & vbNewLine _
            & "        B_INKA_PHOTO.NRS_BR_CD  = @NRS_BR_CD " & vbNewLine _
            & "    AND B_INKA_PHOTO.INKA_NO_L  = @INKA_NO_L " & vbNewLine _
            & "    AND B_INKA_PHOTO.INKA_NO_M  = @INKA_NO_M " & vbNewLine _
            & "    AND B_INKA_PHOTO.INKA_NO_S  = @INKA_NO_S " & vbNewLine

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
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql_where As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 写真選択対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>写真選択対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB070DAC.SQL_SELECT_COUNT)   'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMB070DAC.SQL_FROM_BASE)      'FROM句作成

        Call Me.SetConditionMasterSQL()                 '条件設定部分構築
        Me._StrSql.Append(Me._StrSql_where)             'Baseへの条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB070DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 写真選択検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>写真選択検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB070DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMB070DAC.SQL_FROM_BASE)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(Me._StrSql_where)
        Me._StrSql.Append(LMB070DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB070DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NO", "NO")
        map.Add("SHOHIN_NM", "SHOHIN_NM")
        map.Add("SATSUEI_DATE", "SATSUEI_DATE")
        map.Add("USER_LNM", "USER_LNM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILE_PATH", "FILE_PATH")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB070OUT")

        Return ds

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
        With Me._Row

            'SATSUEI_DATE(FROM)
            whereStr = .Item("SATSUEI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND PHOTO.SATSUEI_DATE >= @SATSUEI_DATE_FROM")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SATSUEI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'SATSUEI_DATE(TO)
            whereStr = .Item("SATSUEI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND PHOTO.SATSUEI_DATE <= @SATSUEI_DATE_TO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SATSUEI_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'KEYWORD
            whereStr = .Item("KEYWORD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND (PHOTO.SHOHIN_NM LIKE @KEYWORD OR USERHDR.USER_LNM LIKE @KEYWORD)")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEYWORD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 入荷写真データ追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function InsertInkaPhoto(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB070OUT_INKA_PHOTO")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB070DAC.SQL_INSERT_B_INKA_PHOTO, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Dim sqlParamList As List(Of SqlParameter) = Me.CreateInsertInkaPhotoParameterList(inRow)
        cmd.Parameters.AddRange(sqlParamList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷写真データ追加用パラメータのリストを作成します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <returns>パラメータのリスト</returns>
    ''' <remarks></remarks>
    Private Function CreateInsertInkaPhotoParameterList(ByVal inRow As DataRow) As List(Of SqlParameter)
        Dim parameters As New List(Of SqlParameter)

        With parameters
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_M", inRow("INKA_NO_M").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_S", inRow("INKA_NO_S").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@NO", inRow("NO").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@FILE_PATH", inRow("FILE_PATH").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        End With

        ' 共通
        Me.SetParamCommonSystemIns(parameters)
        Me.SetParamCommonSystemUpd(parameters)

        Return parameters
    End Function

    ''' <summary>
    ''' 写真データの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function UpdateNpPhoto(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB070SAVE")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(LMB070DAC.SQL_UPDATE_NP_PHOTO)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSaveParameter(Me._Row, Me._SqlPrmList)
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then
            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 写真データの更新パラメータ設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSaveParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NO", .Item("NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHOHIN_NM", .Item("SHOHIN_NM").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataParameter(ByVal prmList As ArrayList) As String()

        'システム項目
        SetSysdataParameter = Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Return SetSysdataParameter

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataTimeParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", sysDateTime(1), DBDataType.CHAR))

        Return sysDateTime

    End Function

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cnt">件数</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        'SQLの発行
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 入荷写真データ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function DeleteInkaPhoto(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB070IN")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB070DAC.SQL_DELETE_B_INKA_PHOTO, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Dim sqlParamList As List(Of SqlParameter) = Me.CreateDeleteInkaPhotoParameterList(inRow)
        cmd.Parameters.AddRange(sqlParamList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷写真データ削除用パラメータのリストを作成します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <returns>パラメータのリスト</returns>
    ''' <remarks></remarks>
    Private Function CreateDeleteInkaPhotoParameterList(ByVal inRow As DataRow) As List(Of SqlParameter)
        Dim parameters As New List(Of SqlParameter)

        With parameters
            ' WHERE
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_L", inRow("INKA_NO_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_M", inRow("INKA_NO_M").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INKA_NO_S", inRow("INKA_NO_S").ToString(), DBDataType.CHAR))
        End With

        Return parameters
    End Function

#End Region

#Region "共通"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(追加時))
    ''' </summary>
    ''' <param name="parameters">パラメータリスト</param>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns(ByVal parameters As List(Of SqlParameter))

        'パラメータ設定
        parameters.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <param name="parameters">パラメータリスト</param>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd(ByVal parameters As List(Of SqlParameter))

        'パラメータ設定
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

#End Region

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

End Class
