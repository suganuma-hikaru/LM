' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM110DAC : 休日マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMM110DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM110DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(HOL.REMARK)     AS SELECT_CNT        " & vbNewLine


    ''' <summary>
    ''' HOL_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                            " & vbNewLine _
                                            & "       LEFT(HOL.HOL,4)                           AS SAKIYEAR                       " & vbNewLine _
                                            & "      ,HOL.HOL                                   AS POSTKEY                        " & vbNewLine _
                                            & "      ,HOL.REMARK                                AS REMARK                         " & vbNewLine _
                                            & "      ,HOL.SYS_ENT_DATE                          AS SYS_ENT_DATE                   " & vbNewLine _
                                            & "      ,USER1.USER_NM                             AS SYS_ENT_USER_NM                " & vbNewLine _
                                            & "      ,HOL.SYS_UPD_DATE                          AS SYS_UPD_DATE                   " & vbNewLine _
                                            & "      ,HOL.SYS_UPD_TIME                          AS SYS_UPD_TIME                   " & vbNewLine _
                                            & "      ,USER2.USER_NM                             AS SYS_UPD_USER_NM                " & vbNewLine _
                                            & "      ,HOL.SYS_DEL_FLG                           AS SYS_DEL_FLG                    " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                              AS SYS_DEL_NM                     " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                        " & vbNewLine _
                                          & "                      $LM_MST$..M_HOL                        AS HOL         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER                       AS USER1       " & vbNewLine _
                                          & "        ON HOL.SYS_ENT_USER    = USER1.USER_CD                              " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG   = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER                       AS USER2       " & vbNewLine _
                                          & "       ON HOL.SYS_UPD_USER     = USER2.USER_CD                              " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG   = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                        AS KBN1        " & vbNewLine _
                                          & "        ON HOL.SYS_DEL_FLG     = KBN1.KBN_CD                                " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD   = 'S051'                                     " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG    = '0'                                        " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                          " & vbNewLine _
                                         & "     HOL.HOL                                      " & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 休日存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_HOL As String = "SELECT                            " & vbNewLine _
                                            & "   COUNT(HOL)  AS REC_CNT      " & vbNewLine _
                                            & "FROM $LM_MST$..M_HOL           " & vbNewLine _
                                            & "WHERE HOL    = @HOL            " & vbNewLine


    ''' <summary>
    ''' 休日存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_HOL_EDIT As String = "SELECT                                    " & vbNewLine _
                                              & "   COUNT(HOL)  AS REC_CNT                 " & vbNewLine _
                                              & "FROM $LM_MST$..M_HOL                      " & vbNewLine _
                                              & "WHERE HOL           = @HOL                " & vbNewLine _
                                              & "  AND SYS_DEL_FLG   = @DA_FLG             " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_HOL             " & vbNewLine _
                                       & "(                                       " & vbNewLine _
                                       & "       HOL                              " & vbNewLine _
                                       & "      ,REMARK                           " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                     " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                     " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                     " & vbNewLine _
                                       & "      ,SYS_ENT_USER                     " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                     " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                     " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                     " & vbNewLine _
                                       & "      ,SYS_UPD_USER                     " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                      " & vbNewLine _
                                       & "      ) VALUES (                        " & vbNewLine _
                                       & "       @HOL                             " & vbNewLine _
                                       & "      ,@REMARK                          " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                    " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                    " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                    " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                    " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                    " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                    " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                    " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                     " & vbNewLine _
                                       & ")                                       " & vbNewLine

    ''' <summary>
    ''' 削除(DELETE/LIKE)SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_LIKEDEL As String = "DELETE                                    " & vbNewLine _
                                       & " FROM                                      " & vbNewLine _
                                       & " $LM_MST$..M_HOL                           " & vbNewLine _
                                       & " WHERE                                     " & vbNewLine _
                                       & " HOL   LIKE     @HOL                       " & vbNewLine

    ''' <summary>
    ''' 削除・復活(UPDATE)SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_HOL SET                            " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         HOL                  = @HOL                  " & vbNewLine


    ''' <summary>
    ''' 複写(INSERT/SELECT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERTSELECT As String = "INSERT INTO  $LM_MST$..M_HOL              " & vbNewLine _
                                       & "(                                               " & vbNewLine _
                                       & "       HOL                                      " & vbNewLine _
                                       & "      ,REMARK                                   " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                             " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                             " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                             " & vbNewLine _
                                       & "      ,SYS_ENT_USER                             " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                             " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                             " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                             " & vbNewLine _
                                       & "      ,SYS_UPD_USER                             " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                              " & vbNewLine _
                                       & " )                                              " & vbNewLine _
                                       & " SELECT                                         " & vbNewLine _
                                       & "       @SAKI + RIGHT(HOL,4)                     " & vbNewLine _
                                       & "      ,REMARK                                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                            " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                            " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                            " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                            " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                            " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                            " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                             " & vbNewLine _
                                       & " FROM         $LM_MST$..M_HOL         AS HOL    " & vbNewLine



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
    ''' 休日マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM110DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM110DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ更新処理時存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ更新処理時存在チェック件数取得SQLの構築・発行</remarks>
    Private Function SelectUpdData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '編集前と編集後が同じ日付のときはそのままDELETE/INSERT
        If inTbl.Rows(0).Item("PREKEY").ToString = inTbl.Rows(0).Item("POSTKEY").ToString Then
            Return ds
        End If

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM110DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM110DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionExistSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM110DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM110DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM110DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SAKIYEAR", "SAKIYEAR")
        map.Add("POSTKEY", "POSTKEY")
        map.Add("REMARK", "REMARK")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM110OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ複写対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ複写対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectCopyData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM110DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM110DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetParamDelInMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ存在チェック(0件ならばエラー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistHolM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_EXIT_HOL_EDIT _
                                                                        , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamCommonSystemDelExist()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "CheckExistHolM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ存在チェック新規登録時(0件でなければエラー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistHolMIn(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_EXIT_HOL _
                                                                        , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamCommonDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "CheckExistHolMInsert", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (HOL.SYS_DEL_FLG = @SYS_DEL_FLG  OR HOL.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SAKIYEAR").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LEFT(HOL.HOL,4) LIKE @SAKIYEAR")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKIYEAR", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HOL.REMARK LIKE @REMARK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(更新処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionExistSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row


            whereStr = .Item("POSTKEY").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HOL = @POSTKEY")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@POSTKEY", whereStr, DBDataType.CHAR))
            End If


            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 休日マスタ新規登録(INSERT)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertHolM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "InsertHolM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ更新(DELETE)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateHolM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_LIKEDEL, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        reader.Close()

        MyBase.Logger.WriteSQLLog("LMM110DAC", "UpdateHolM", cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ削除・復活(UPDATE)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteHolM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "DeleteHolM", cmd)

        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function


    ''' <summary>
    ''' 休日マスタ複写(DELETE)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>休日マスタ削除・復活SQLの構築・発行</remarks>
    Private Function CopyDelHolM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_LIKEDEL _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamCopyDel()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "CopyDelHolM", cmd)

        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    '''  休日マスタ複写(SELECT/INSERT)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyInHolM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM110DAC.SQL_INSERTSELECT)    'SQL構築
        Call Me.SetParamDelInMasterSQL()                 '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamCopyDelIn()
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM110DAC", "CopyInHolM", cmd)

        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 暦上日チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDateCheck(ByVal ds As DataSet) As DataSet

        'DataTable情報を取得
        Dim outTbl As DataTable = ds.Tables("LMM110OUT")
        Dim inTbl As DataTable = ds.Tables("LMM110IN")

        '複写した日付の中にうるう年がないか判定
        Dim drs As DataRow() = outTbl.Select(String.Concat("POSTKEY = ", inTbl.Rows(0).Item("SAKIYEAR").ToString(), "0229"))

        'うるう年がない場合スルー
        If drs.Length < 1 Then
            Return ds
        End If

        Me._Row = drs(0)

        'うるう年がある場合、暦上日チェック
        If IsDate(DateFormatUtility.EditSlash(Me._Row.Item("POSTKEY").ToString)) = False Then

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM110DAC.SQL_LIKEDEL, inTbl.Rows(0).Item("USER_BR_CD").ToString()))

            'SQLパラメータ初期化/設定
            Call Me.SetComParam()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM110DAC", "IsDateCheck", cmd)

            MyBase.GetUpdateResult(cmd)

            'OUTテーブルから該当データを削除
            Me._Row.Delete()

        End If

        Return ds

    End Function

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

#Region "パラメータ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamLikeDelMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row


            whereStr = .Item("SAKIYEAR").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HOL.HOL LIKE @HOL")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelInMasterSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row


            whereStr = .Item("MOTOYEAR").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HOL.HOL LIKE @HOL")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(休日マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", Me._Row.Item("SAKIYEAR").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDel()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonDelete()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", .Item("POSTKEY").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", Me._Row.Item("PREKEY").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub
    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活存在チェック時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDelExist()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", Me._Row.Item("PREKEY").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DA_FLG", Me._Row.Item("DA_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(日付複写時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCopyDelIn()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI", Me._Row.Item("SAKIYEAR").ToString(), DBDataType.CHAR))

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(日付複写時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonDelIn()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", Me._Row.Item("MOTOYEAR").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(日付複写時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCopyDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", String.Concat(Me._Row.Item("SAKIYEAR").ToString(), "%"), DBDataType.CHAR))


    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonDelete()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", Me._Row.Item("PREKEY").ToString(), DBDataType.CHAR))

    End Sub
    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
