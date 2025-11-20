' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI991DAC : サーテック　運賃明細作成
'  作  成  者       :  sia-minagawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI991DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI991DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = _
          "SELECT DISTINCT                                         " & vbNewLine _
        & "     ISNULL(MR2.NRS_BR_CD, MR1.NRS_BR_CD)  AS NRS_BR_CD " & vbNewLine _
        & "    ,ISNULL(MR2.PTN_ID, MR1.PTN_ID)  AS PTN_ID          " & vbNewLine _
        & "    ,ISNULL(MR2.PTN_CD, MR1.PTN_CD)  AS PTN_CD          " & vbNewLine _
        & "    ,ISNULL(MR2.RPT_ID, MR1.RPT_ID)  AS RPT_ID          " & vbNewLine _
        & "  FROM $LM_MST$..M_RPT  MR1                             " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_CUST_RPT  MCR1                  " & vbNewLine _
        & "    ON MCR1.NRS_BR_CD = MR1.NRS_BR_CD                   " & vbNewLine _
        & "   AND MCR1.CUST_CD_L = '00152'                         " & vbNewLine _
        & "   AND MCR1.CUST_CD_M = '00'                            " & vbNewLine _
        & "   AND MCR1.CUST_CD_S = '00'                            " & vbNewLine _
        & "   AND MCR1.PTN_ID = MR1.PTN_ID                         " & vbNewLine _
        & "   AND MCR1.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_RPT  MR2                        " & vbNewLine _
        & "    ON MR2.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
        & "   AND MR2.PTN_ID = MCR1.PTN_ID                         " & vbNewLine _
        & "   AND MR2.PTN_CD = MCR1.PTN_CD                         " & vbNewLine _
        & "   AND MR2.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & " WHERE MR1.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
        & "   AND MR1.PTN_ID = 'D0'                                " & vbNewLine _
        & "   AND MR1.STANDARD_FLAG = '01'                         " & vbNewLine _
        & "   AND MR1.SYS_DEL_FLG = '0'                            " & vbNewLine

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

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI991IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI991DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Call Me.SetParamSelectMPrt()                      'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI991DAC", "SelectMPRT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectMPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))

    End Sub

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
