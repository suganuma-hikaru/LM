' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMH800DAC : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH800DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH800DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

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
    ''' トランスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "Method"

#Region "荷主コードチェック"
    ''' <summary>
    ''' 荷主コードチェック（DAC）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>

    Private Function SelectSCMCustCd(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH800CUST_INFO")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'スキーマ名設定
        Me._MstSchemaNm = MyBase.GetDatabaseName("", DBKbn.MST)
        'SQL作成
        'SQL構築
        Me._StrSql.Append("Select KBN_NM3 AS SCM_CUST_CD,KBN_CD AS KBN_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    KBN_GROUP_CD    = 'S032'                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND KBN_NM3       = @SCM_CUST_CD                                    ")
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
        End With

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH800DAC", "SelectSCMCustCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")
        map.Add("KBN_CD", "KBN_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH800CUST_LIST")

        Return ds


    End Function

#End Region

#Region "ファイルパス取得"
    ''' <summary>
    ''' ファイルパス取得（DAC）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>

    Private Function SelectFilePath(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH800CUST_LIST")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'スキーマ名設定
        Me._MstSchemaNm = MyBase.GetDatabaseName("", DBKbn.MST)
        'SQL作成
        'SQL構築
        Me._StrSql.Append("Select KBN_NM3 AS FILE_PATH,KBN_NM4 AS FILE_NAME,KBN_NM5 AS FILE_EXTENSION ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    KBN_GROUP_CD    = @KBN_GROUP_CD                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND KBN_CD       = @BR_CD                                    ")
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", Me.GetGroupCd(.Item("KBN_CD").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
        End With

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH800DAC", "SelectFilePath", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("FILE_PATH", "FILE_PATH")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("FILE_EXTENSION", "FILE_EXTENSION")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH800FILEPATH_OUT")

        Return ds


    End Function

#End Region

    Private Function GetGroupCd(ByVal scmCustKbn As String) As String
        Dim rtn As String = String.Empty
        Select Case scmCustKbn
            Case "00"
                'BPカストロール
                rtn = "S036"
        End Select

        Return rtn

    End Function


#End Region 'Method

End Class
