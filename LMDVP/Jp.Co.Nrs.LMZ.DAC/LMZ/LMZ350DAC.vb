' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ350DAC : 真荷主照会
'  作  成  者       :  hori
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMZ350DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ350DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    Private Const SQL_SELECT_COUNT_COUNTRY As String _
                = " SELECT                                  " & vbNewLine _
                & "   COUNT(MBP.BP_CD) AS SELECT_CNT        " & vbNewLine

    Private Const SQL_SELECT_DATA_COUNTRY As String _
                = " SELECT                                  " & vbNewLine _
                & "   MBP.BP_CD,                            " & vbNewLine _
                & "   MBP.BP_NM1,                           " & vbNewLine _
                & "   MBP.COUNTRY_CD,                       " & vbNewLine _
                & "   MCR.COUNTRY_ENM                       " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA_COUNTRY As String _
                = " FROM                                    " & vbNewLine _
                & "   ABM_DB..M_BP AS MBP                   " & vbNewLine _
                & " LEFT JOIN                               " & vbNewLine _
                & "   ABM_DB..M_COUNTRY AS MCR              " & vbNewLine _
                & "   ON                                    " & vbNewLine _
                & "     MCR.COUNTRY_CD = MBP.COUNTRY_CD     " & vbNewLine _
                & " WHERE                                   " & vbNewLine _
                & "   MBP.SYS_DEL_FLG = '0'                 " & vbNewLine

#End Region

#Region "ORDER BY"

    Private Const SQL_ORDER_BY_COUNTRY As String _
                = " ORDER BY                    " & vbNewLine _
                & "     MBP.BP_CD               " & vbNewLine

#End Region

#End Region

#Region "ComboBox SQL"

    ''' <summary>
    ''' 国取得用
    ''' </summary>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Const SQL_SELECT_COMBO_COUNTRY As String _
            = " SELECT              " & vbNewLine _
            & "    COUNTRY_CD       " & vbNewLine _
            & "   ,COUNTRY_ENM      " & vbNewLine _
            & " FROM                " & vbNewLine _
            & "   ABM_DB..M_COUNTRY " & vbNewLine _
            & " ORDER BY            " & vbNewLine _
            & "     COUNTRY_CD      " & vbNewLine

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
    ''' 検索件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ350IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ350DAC.SQL_SELECT_COUNT_COUNTRY)
        Me._StrSql.Append(LMZ350DAC.SQL_FROM_DATA_COUNTRY)
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ350DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ350IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Dim sql As String = String.Empty

        'SQL作成
        Me._StrSql.Append(LMZ350DAC.SQL_SELECT_DATA_COUNTRY)
        Me._StrSql.Append(LMZ350DAC.SQL_FROM_DATA_COUNTRY)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMZ350DAC.SQL_ORDER_BY_COUNTRY)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ350DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("BP_CD", "BP_CD")
        map.Add("BP_NM1", "BP_NM1")
        map.Add("COUNTRY_CD", "COUNTRY_CD")
        map.Add("COUNTRY_ENM", "COUNTRY_ENM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ350OUT")

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

            whereStr = .Item("BP_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MBP.BP_CD LIKE @BP_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BP_CD", String.Concat(whereStr, "%"), DBDataType.NCHAR))
            End If

            whereStr = .Item("BP_NM1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MBP.BP_NM1 LIKE @BP_NM1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BP_NM1", String.Concat("%", whereStr, "%"), DBDataType.NCHAR))
            End If

            whereStr = .Item("COUNTRY_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND MBP.COUNTRY_CD = @COUNTRY_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COUNTRY_CD", whereStr, DBDataType.NCHAR))
            End If

        End With

    End Sub

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 国取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboCountry(ByVal ds As DataSet) As DataSet

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMZ350DAC.SQL_SELECT_COMBO_COUNTRY)

        MyBase.Logger.WriteSQLLog("LMZ350DAC", "SelectComboCountry", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        'レコードをクリア
        ds.Tables("LMZ350COMBO_COUNTRY").Rows.Clear()

        '取得データの格納先をマッピング
        map.Add("COUNTRY_CD", "COUNTRY_CD")
        map.Add("COUNTRY_ENM", "COUNTRY_ENM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ350COMBO_COUNTRY")

        Return ds

    End Function

#End Region

#End Region

End Class
