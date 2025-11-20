' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : 請求
'  プログラムID     :  LMF100DAC : 請求印刷指示
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF100DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF100DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' 都道府県名データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KEN As String = " SELECT                                                               " & vbNewLine _
                                           & "	          KEN_NM                  AS KEN_NM                         " & vbNewLine _
                                           & "	         ,KEN_CD                  AS KEN_CD                         " & vbNewLine _
                                           & " 	     FROM $LM_MST$..M_KEN                                           " & vbNewLine _
                                           & "		 WHERE SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                           & " ORDER BY KEN_CD                     " & vbNewLine


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
    ''' 都道府県名データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>都道府県名データ検索取得SQLの構築・発行</remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF100KEN_IN")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF100DAC.SQL_SELECT_KEN)      'SQL構築(都道府県名抽出用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMF100DAC", "SelectKenData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング

        map.Add("KEN_NM", "KEN_NM")
        map.Add("KEN_CD", "KEN_CD")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF100KEN")

        Return ds

    End Function

#End Region

#Region "設定処理"



    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行()
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

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

#End Region

#End Region

End Class
