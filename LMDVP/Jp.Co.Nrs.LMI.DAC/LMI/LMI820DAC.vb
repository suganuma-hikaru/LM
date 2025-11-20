' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI820  : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI820DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI820DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "TXT出力データの検索"

#Region "TXT出力データの検索 SQL SELECT句"

    ''' <summary>
    ''' ダウケミ請求明細テーブル検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEIQPRT As String = " SELECT                                                                  " & vbNewLine _
                                               & " SEIQPRT.ID                                 AS ID                        " & vbNewLine _
                                               & ",SEIQPRT.SHORI_YM                           AS SHORI_YM                  " & vbNewLine _
                                               & ",SEIQPRT.IN_KAISHA                          AS IN_KAISHA                 " & vbNewLine _
                                               & ",SEIQPRT.KAISHA_CD                          AS KAISHA_CD                 " & vbNewLine _
                                               & ",SEIQPRT.DV_NO                              AS DV_NO                     " & vbNewLine _
                                               & ",SEIQPRT.GMID                               AS GMID                      " & vbNewLine _
                                               & ",SEIQPRT.COST                               AS COST                      " & vbNewLine _
                                               & ",SEIQPRT.HIYO                               AS HIYO                      " & vbNewLine _
                                               & ",SEIQPRT.TUKA                               AS TUKA                      " & vbNewLine _
                                               & ",SEIQPRT.GAKU                               AS GAKU                      " & vbNewLine _
                                               & ",SEIQPRT.FUGO                               AS FUGO                      " & vbNewLine _
                                               & ",SEIQPRT.HASSEI_YM                          AS HASSEI_YM                 " & vbNewLine _
                                               & ",SEIQPRT.SHIP_NO                            AS SHIP_NO                   " & vbNewLine _
                                               & ",SEIQPRT.WT                                 AS WT                        " & vbNewLine _
                                               & ",SEIQPRT.KYORI                              AS KYORI                     " & vbNewLine _

#End Region

#Region "TXT出力データの検索 SQL FROM句"

    ''' <summary>
    ''' TXT出力データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SEIQPRT As String = "FROM                                                             " & vbNewLine _
                                                    & "$LM_TRN$..I_DOW_SEIQ_PRT SEIQPRT                                 " & vbNewLine

#End Region

#Region "TXT出力データの検索 SQL ORDER句"

    ''' <summary>
    ''' TXT出力データの検索 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SEIQPRT As String = "ORDER BY                                                        " & vbNewLine _
                                                    & " SEIQPRT.REC_TYPE                                                " & vbNewLine _
                                                    & ",SEIQPRT.COST                                                    " & vbNewLine _
                                                    & ",SEIQPRT.GMID                                                    " & vbNewLine _
                                                    & ",SEIQPRT.DV_NO                                                   " & vbNewLine _
                                                    & ",SEIQPRT.SHIP_NO                                                 " & vbNewLine _
                                                    & ",SEIQPRT.GAKU                                                    " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

    '''' <summary>
    '''' 検索条件設定用
    '''' </summary>
    '''' <remarks></remarks>
    'Private _Row As Data.DataRow

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

#Region "SQLメイン処理"

#Region "TXT出力データの検索"

    ''' <summary>
    ''' TXT出力データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTXT(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI820IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI820DAC.SQL_SELECT_SEIQPRT)       'SQL構築(Select句 出荷)
        Me._StrSql.Append(LMI820DAC.SQL_SELECT_FROM_SEIQPRT)  'SQL構築(From句 出荷)
        Call SetSQLWhereSEIQPRT(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMI820DAC.SQL_SELECT_ORDER_SEIQPRT)  'SQL構築(Order句 出荷)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI820DAC", "SelectTXT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ID", "ID")
        map.Add("SHORI_YM", "SHORI_YM")
        map.Add("IN_KAISHA", "IN_KAISHA")
        map.Add("KAISHA_CD", "KAISHA_CD")
        map.Add("DV_NO", "DV_NO")
        map.Add("GMID", "GMID")
        map.Add("COST", "COST")
        map.Add("HIYO", "HIYO")
        map.Add("TUKA", "TUKA")
        map.Add("GAKU", "GAKU")
        map.Add("FUGO", "FUGO")
        map.Add("HASSEI_YM", "HASSEI_YM")
        map.Add("SHIP_NO", "SHIP_NO")
        map.Add("WT", "WT")
        map.Add("KYORI", "KYORI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI820OUT_TXT")

        reader.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 TXT出力データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSEIQPRT(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" SEIQPRT.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '年月From
            whereStr = Mid(.Item("DATE_FROM").ToString(), 1, 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQPRT.SEIQ_YM = @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'レコード種類1
            strSqlAppend = String.Empty
            whereStr = .Item("JIKKO_KB").ToString()
            If ("04").Equals(.Item("JIKKO_KB").ToString()) = True OrElse _
                ("05").Equals(.Item("JIKKO_KB").ToString()) = True Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " SEIQPRT.REC_TYPE = '1'")
            End If

            If ("04").Equals(.Item("JIKKO_KB").ToString()) = True OrElse _
                ("06").Equals(.Item("JIKKO_KB").ToString()) = True Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " SEIQPRT.REC_TYPE = '2'")
            End If

            If ("04").Equals(.Item("JIKKO_KB").ToString()) = True OrElse _
                ("07").Equals(.Item("JIKKO_KB").ToString()) = True Then
                If String.IsNullOrEmpty(strSqlAppend) = True Then
                    'DEPARTの条件を設定していない場合
                    strSqlAppend = " AND ("
                Else
                    'DEPARTの条件を既に設定している場合
                    strSqlAppend = String.Concat(strSqlAppend, " OR ")
                End If
                strSqlAppend = String.Concat(strSqlAppend, " SEIQPRT.REC_TYPE = '3'")
            End If

            '請求項目1、2、3の設定
            If String.IsNullOrEmpty(strSqlAppend) = False Then
                strSqlAppend = String.Concat(strSqlAppend, " )")
                Me._StrSql.Append(strSqlAppend)
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

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

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
