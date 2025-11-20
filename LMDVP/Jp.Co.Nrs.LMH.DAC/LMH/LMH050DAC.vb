' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMH050DAC : 
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Field"

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 郵便番号マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>郵便番号マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function GetSMessage(ByVal ds As DataSet) As DataSet

        Dim sql As String = "     SELECT MESSAGE_ID               " & vbNewLine _
                                        & "           ,MESSAGE_STRING               " & vbNewLine _
                                        & "           ,MESSAGE_DISP_FLG               " & vbNewLine _
                                        & "           ,BUTTON_CNT               " & vbNewLine _
                                        & "           ,SYS_ENT_DATE               " & vbNewLine _
                                        & "           ,SYS_ENT_TIME               " & vbNewLine _
                                        & "           ,SYS_ENT_PGID               " & vbNewLine _
                                        & "           ,SYS_ENT_USER               " & vbNewLine _
                                        & "           ,SYS_UPD_DATE               " & vbNewLine _
                                        & "           ,SYS_UPD_TIME               " & vbNewLine _
                                        & "           ,SYS_UPD_PGID               " & vbNewLine _
                                        & "           ,SYS_UPD_USER               " & vbNewLine _
                                        & "           ,SYS_DEL_FLG               " & vbNewLine _
                                        & "       FROM $LM_MST$..S_MESSAGE               " & vbNewLine

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQL作成
        Me._StrSql.Append(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), ""))

        MyBase.Logger.WriteSQLLog("LMH050DAC", "GetSMessage", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("MESSAGE_ID", "MESSAGE_ID")
        map.Add("MESSAGE_STRING", "MESSAGE_STRING")
        map.Add("MESSAGE_DISP_FLG", "MESSAGE_DISP_FLG")
        map.Add("BUTTON_CNT", "BUTTON_CNT")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "S_MESSAGE")

        Return ds

    End Function

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
