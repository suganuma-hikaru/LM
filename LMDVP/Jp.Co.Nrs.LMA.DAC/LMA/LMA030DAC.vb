' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMA       : メニュー
'  プログラムID     :  LMA030DAC : 荷主選択
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMA030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMA030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "SELECT文"

    ''' <summary>
    ''' SELECT文（荷主）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUST As String = "SELECT DISTINCT            " & vbNewLine _
                                         & " KBN_NM3 AS KBN_CD,           " & vbNewLine _
                                         & " KBN_NM1 AS KBN_NM            " & vbNewLine _
                                         & "FROM $LM_MST$..Z_KBN          " & vbNewLine _
                                         & "WHERE KBN_GROUP_CD ='N018'    " & vbNewLine _
                                         & "ORDER BY KBN_CD"


    ''' <summary>
    ''' SELECT文（遷移先）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DISP As String = "SELECT                     " & vbNewLine _
                                         & " KBN_CD AS KBN_CD,            " & vbNewLine _
                                         & " KBN_NM2 AS KBN_NM            " & vbNewLine _
                                         & "FROM $LM_MST$..Z_KBN          " & vbNewLine _
                                         & "WHERE KBN_GROUP_CD ='N018'    " & vbNewLine _
                                         & "  AND KBN_NM1 = '@KBN_NM'     " & vbNewLine _
                                         & "ORDER BY KBN_CD"

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
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectComboData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMA030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If Me._Row.Item("HANTEI").ToString().Equals("0") = True Then
            Me._StrSql.Append(LMA030DAC.SQL_SELECT_CUST)     'SQL構築(用Select句)
        Else
            Me._StrSql.Append(LMA030DAC.SQL_SELECT_DISP.Replace( _
                              "@KBN_NM", Me._Row.Item("KBN_NM").ToString()))     'SQL構築(用Select句)
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMA030DAC", "SelectComboData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_NM", "KBN_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMA030OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

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
