' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ250DAC : 運送会社マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ250DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ250DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(UNSOCO.UNSOCO_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' UNSOCO_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
                                        & "      UNSOCO.NRS_BR_CD                               AS NRS_BR_CD                   " & vbNewLine _
                                        & "     ,UNSOCO.UNSOCO_NM                               AS UNSOCO_NM                   " & vbNewLine _
                                        & "     ,UNSOCO.UNSOCO_BR_NM                            AS UNSOCO_BR_NM                " & vbNewLine _
                                        & "     ,UNSOCO.UNSOCO_CD                               AS UNSOCO_CD                   " & vbNewLine _
                                        & "     ,UNSOCO.UNSOCO_BR_CD                            AS UNSOCO_BR_CD                " & vbNewLine _
                                        & "     ,KBN.KBN_NM1                                    AS MOTOUKE_KB_NM               " & vbNewLine _
                                        & "     ,UNSOCO.MOTOUKE_KB                              AS MOTOUKE_KB                  " & vbNewLine _
                                        & "     ,UNSOCO.NIHUDA_YN                               AS NIHUDA_YN                   " & vbNewLine _
                                        & "     ,UNSOCO.TARE_YN                                 AS TARE_YN                     " & vbNewLine


#End Region

#Region "FROM句"


    '要望対応:1248 terakawa 2013.03.21 Start
    'Private Const SQL_FROM_DATA As String = "FROM                                                       " & vbNewLine _
    '                                      & "                      $LM_MST$..M_UNSOCO    AS UNSOCO      " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN       AS KBN         " & vbNewLine _
    '                                      & "        ON UNSOCO.MOTOUKE_KB  = KBN.KBN_CD                 " & vbNewLine _
    '                                      & "       AND KBN.KBN_GROUP_CD   = 'M006'                     " & vbNewLine _
    '                                      & "       AND KBN.SYS_DEL_FLG    = '0'                        " & vbNewLine _
    '                                      & "WHERE   UNSOCO.SYS_DEL_FLG    = '0'                        " & vbNewLine

    Private Const SQL_FROM_DATA As String = "FROM                                                       " & vbNewLine _
                                          & "                      $LM_MST$..M_UNSOCO    AS UNSOCO      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN       AS KBN         " & vbNewLine _
                                          & "        ON UNSOCO.MOTOUKE_KB  = KBN.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN.KBN_GROUP_CD   = 'M006'                     " & vbNewLine _
                                          & "       AND KBN.SYS_DEL_FLG    = '0'                        " & vbNewLine

    Private Const SQL_FROM_TUNSOCO As String = "      INNER JOIN $LM_MST$..M_TUNSOCO   AS TUNSOCO " & vbNewLine _
                                      & "        ON TUNSOCO.SYS_DEL_FLG  = '0'                        " & vbNewLine _
                                      & "       AND TUNSOCO.UNSOCO_CD    =  UNSOCO.UNSOCO_CD        " & vbNewLine _
                                      & "       AND TUNSOCO.UNSOCO_BR_CD =  UNSOCO.UNSOCO_BR_CD     " & vbNewLine


    Private Const SQL_WHERE As String = "WHERE   UNSOCO.SYS_DEL_FLG    = '0'                        " & vbNewLine

    '要望対応:1248 terakawa 2013.03.21 End



#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                       " & vbNewLine _
                                         & "     UNSOCO.UNSOCO_CD,UNSOCO.UNSOCO_BR_CD      " & vbNewLine

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
    ''' 運送会社マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ250IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ250DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMZ250DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)

        '要望対応:1248 terakawa 2013.03.21 Start
        'マイ運送会社区分が"01"の場合、条件追加
        If _Row.Item("MY_UNSOCO_YN").ToString() = "01" Then
            Me._StrSql.Append(LMZ250DAC.SQL_FROM_TUNSOCO)    'SQL構築(カウント用追加from句)
        End If
        Me._StrSql.Append(LMZ250DAC.SQL_WHERE)            'SQL構築(カウント用WHERE句)
        '要望対応:1248 terakawa 2013.03.21 End

        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ250DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運送会社マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ250IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ250DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ250DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)

        '要望対応:1248 terakawa 2013.03.21 Start
        'マイ運送会社区分が"01"の場合、条件追加
        If _Row.Item("MY_UNSOCO_YN").ToString() = "01" Then
            Me._StrSql.Append(LMZ250DAC.SQL_FROM_TUNSOCO)    'SQL構築(データ抽出用追加from句)
        End If
        Me._StrSql.Append(LMZ250DAC.SQL_WHERE)            'SQL構築(データ抽出用WHERE句)
        '要望対応:1248 terakawa 2013.03.21 End

        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMZ250DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ250DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("MOTOUKE_KB_NM", "MOTOUKE_KB_NM")
        map.Add("MOTOUKE_KB", "MOTOUKE_KB")
        map.Add("NIHUDA_YN", "NIHUDA_YN")
        map.Add("TARE_YN", "TARE_YN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ250OUT")

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

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.UNSOCO_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("UNSOCO_BR_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_BR_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("MOTOUKE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.MOTOUKE_KB = @MOTOUKE_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTOUKE_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.UNSOCO_CD LIKE @UNSOCO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND UNSOCO.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '要望対応:1248 terakawa 2013.03.21 Start
            'マイ運送会社区分が"01"の場合、ユーザーIDを条件に設定
            whereStr = .Item("MY_UNSOCO_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False AndAlso _
               whereStr = "01" Then
                Me._StrSql.Append("AND TUNSOCO.USER_CD = @USER_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", GetUserID(), DBDataType.CHAR))
            End If
            '要望対応:1248 terakawa 2013.03.21 End

        End With

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
