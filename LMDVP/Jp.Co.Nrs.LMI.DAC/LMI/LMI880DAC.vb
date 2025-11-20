' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI880  : 篠崎運送月末在庫実績データ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI880DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI880DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "CSV出力データの検索"

#Region "篠崎運送専用の月末在庫テーブルからデータ取得"

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL"

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL SELECT句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MONTHLYSNZ As String = " SELECT                                                                     " & vbNewLine _
                                                  & " MONTHLYSNZ.DEL_KB                            AS DEL_KB                     " & vbNewLine _
                                                  & ",MONTHLYSNZ.NRS_BR_CD                         AS NRS_BR_CD                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.JISSEKI_DATE                      AS JISSEKI_DATE               " & vbNewLine _
                                                  & ",MONTHLYSNZ.ZAI_REC_NO                        AS ZAI_REC_NO                 " & vbNewLine _
                                                  & ",MONTHLYSNZ.GOODS_CD_CUST                     AS GOODS_CD_CUST              " & vbNewLine _
                                                  & ",MONTHLYSNZ.GOODS_NM                          AS GOODS_NM                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.LOT_NO                            AS LOT_NO                     " & vbNewLine _
                                                  & ",MONTHLYSNZ.QT                                AS QT                         " & vbNewLine _
                                                  & ",MONTHLYSNZ.IRIME                             AS IRIME                      " & vbNewLine _
                                                  & ",MONTHLYSNZ.LOCA                              AS LOCA                       " & vbNewLine _
                                                  & ",MONTHLYSNZ.ORDER_NO                          AS ORDER_NO                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.TANAOROSHI_BI                     AS TANAOROSHI_BI              " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_FLG                          AS SEND_FLG                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_USER                         AS SEND_USER                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_DATE                         AS SEND_DATE                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.SEND_TIME                         AS SEND_TIME                  " & vbNewLine _
                                                  & ",MONTHLYSNZ.CRT_USER                          AS CRT_USER                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.CRT_DATE                          AS CRT_DATE                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.CRT_TIME                          AS CRT_TIME                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.UPD_USER                          AS UPD_USER                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.UPD_DATE                          AS UPD_DATE                   " & vbNewLine _
                                                  & ",MONTHLYSNZ.UPD_TIME                          AS UPD_TIME                   " & vbNewLine

#End Region

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL FROM句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MONTHLYSNZ As String = "FROM                                                                    " & vbNewLine _
                                                       & "$LM_TRN$..H_SENDMONTHLY_SNZ MONTHLYSNZ                                  " & vbNewLine

#End Region

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL WHERE句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_MONTHLYSNZ As String = "WHERE MONTHLYSNZ.NRS_BR_CD = @NRS_BR_CD                                     " & vbNewLine _
                                                        & "  AND MONTHLYSNZ.JISSEKI_DATE = @JISSEKI_DATE                               " & vbNewLine _
                                                        & "  AND MONTHLYSNZ.SYS_DEL_FLG = '0'                                          " & vbNewLine

#End Region

#Region "篠崎運送専用の月末在庫テーブルからデータ取得 SQL ORDER句"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_MONTHLYSNZ As String = "ORDER BY                                                                    " & vbNewLine _
                                                        & "  MONTHLYSNZ.GOODS_CD_CUST                                                  " & vbNewLine _
                                                        & " ,MONTHLYSNZ.LOT_NO                                                         " & vbNewLine _
                                                        & " ,MONTHLYSNZ.IRIME                                                          " & vbNewLine _
                                                        & " ,MONTHLYSNZ.LOCA                                                           " & vbNewLine

#End Region

#End Region

#End Region

#End Region

#Region "CSV出力データの更新"

#Region "篠崎運送専用の月末在庫テーブルの更新"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルの更新 SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MONTHLYSNZ As String = "UPDATE $LM_TRN$..H_SENDMONTHLY_SNZ SET                     " & vbNewLine _
                                              & "                  SEND_FLG             = '1'                   " & vbNewLine _
                                              & "                 ,SEND_USER            = @SEND_USER            " & vbNewLine _
                                              & "                 ,SEND_DATE            = @SEND_DATE            " & vbNewLine _
                                              & "                 ,SEND_TIME            = @SEND_TIME            " & vbNewLine _
                                              & "                 ,UPD_USER             = @UPD_USER             " & vbNewLine _
                                              & "                 ,UPD_DATE             = @UPD_DATE             " & vbNewLine _
                                              & "                 ,UPD_TIME             = @UPD_TIME             " & vbNewLine _
                                              & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                              & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                              & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                              & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                              & "                 WHERE                                         " & vbNewLine _
                                              & "                       NRS_BR_CD       = @NRS_BR_CD            " & vbNewLine _
                                              & "                   AND JISSEKI_DATE    = @JISSEKI_DATE         " & vbNewLine _
                                              & "                   AND SYS_DEL_FLG     = '0'                   " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

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

#Region "篠崎運送専用の月末在庫テーブルからデータ取得"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI880IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI880DAC.SQL_SELECT_MONTHLYSNZ)       'SQL構築(Select句)
        Me._StrSql.Append(LMI880DAC.SQL_SELECT_FROM_MONTHLYSNZ)  'SQL構築(From句)
        Me._StrSql.Append(LMI880DAC.SQL_SELECT_WHERE_MONTHLYSNZ) 'SQL構築(Where句)
        Me._StrSql.Append(LMI880DAC.SQL_SELECT_ORDER_MONTHLYSNZ) 'SQL構築(Order句)

        'パラメータの設定
        Call SetSQLMonthlySnzParameter(inTbl.Rows(0), Me._SqlPrmList)           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI880DAC", "SelectCSV", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("QT", "QT")
        map.Add("IRIME", "IRIME")
        map.Add("LOCA", "LOCA")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("TANAOROSHI_BI", "TANAOROSHI_BI")
        map.Add("SEND_FLG", "SEND_FLG")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI880INOUT_CSV")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "篠崎運送専用の月末在庫テーブルの更新"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateCSV(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI880IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI880DAC.SQL_UPDATE_MONTHLYSNZ)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSQLMonthlySnzUpdParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI880DAC", "UpdateCSV", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 篠崎運送専用の月末在庫テーブル(検索時)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLMonthlySnzParameter(ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 篠崎運送専用の月末在庫テーブル(更新時)"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLMonthlySnzUpdParameter(ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Concat(Mid(MyBase.GetSystemTime(), 1, 2), ":", Mid(MyBase.GetSystemTime(), 3, 2), ":", Mid(MyBase.GetSystemTime(), 5, 2)), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", String.Concat(Mid(MyBase.GetSystemTime(), 1, 2), ":", Mid(MyBase.GetSystemTime(), 3, 2), ":", Mid(MyBase.GetSystemTime(), 5, 2)), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

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
