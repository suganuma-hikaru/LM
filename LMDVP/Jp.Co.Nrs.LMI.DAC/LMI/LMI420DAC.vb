' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI420  : JX 請求運賃比較
'  作  成  者       :  daikoku
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI420DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI420DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理"

#Region "検索処理 SELECT句"

    ''' <summary>
    ''' 検索処理 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT SASHIZU_NO                                         " & vbNewLine _
                                                & "     ,SUM(JX_UNCHIN)        AS JX_UNCHIN                 " & vbNewLine _
                                                & "     ,MAX(NRS_UNCHIN)       AS NRS_UNCHIN                " & vbNewLine _
                                                & "     ,MAX(NRS_UNCHIN) - SUM(JX_UNCHIN)   AS SAGAKU       " & vbNewLine _
                                                & "     ,MAX(UNSO_NO_L)        AS UNSO_NO_L                 " & vbNewLine _
                                                & "     ,MAX(OUTKA_PLAN_DATE)  AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                & "FROM  $LM_TRN$..WK_JX_UNCHIN                             " & vbNewLine _
                                                & "WHERE USER_ID   = @USER_ID                               " & vbNewLine _
                                                & "	 AND GET_FLG   = '1'                                    " & vbNewLine _
                                                & "GROUP BY  SASHIZU_NO                                     " & vbNewLine


    'Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                     " & vbNewLine _
    '                                            & "  MAIN.ORDER_NO             AS ORDER_NO                  " & vbNewLine _
    '                                            & " ,MAIN.NRS_UNCHIN           AS NRS_UNCHIN                " & vbNewLine _
    '                                            & " ,@JX_UNCHIN               AS JX_UNCHIN                  " & vbNewLine _
    '                                            & " ,(ISNULL(MAIN.NRS_UNCHIN,0) - @JX_UNCHIN)  AS SAGAKU    " & vbNewLine _
    '                                            & " ,MAIN.UNSO_NO_L           AS UNSO_NO_L                  " & vbNewLine _
    '                                            & " ,MAIN.OUTKA_PLAN_DATE     AS OUTKA_PLAN_DATE            " & vbNewLine _
    '                                            & "FROM                                                     " & vbNewLine _
    '                                            & "   (SELECT LEFT(UNSO.CUST_REF_NO,8)    AS ORDER_NO       " & vbNewLine _
    '                                            & "       ,SUM(UNCHIN.DECI_UNCHIN)        AS NRS_UNCHIN     " & vbNewLine _
    '                                            & "       ,MAX(UNSO.UNSO_NO_L)            AS UNSO_NO_L      " & vbNewLine _
    '                                            & "       ,MAX(UNSO.OUTKA_PLAN_DATE)      AS OUTKA_PLAN_DATE" & vbNewLine _
    '                                            & "      FROM $LM_TRN$..F_UNSO_L UNSO                       " & vbNewLine _
    '                                            & "      INNER JOIN $LM_TRN$..F_UNCHIN_TRS  UNCHIN          " & vbNewLine _
    '                                            & "        ON UNSO.UNSO_NO_L    = UNCHIN.UNSO_NO_L          " & vbNewLine _
    '                                            & "       AND UNCHIN.SYS_DEL_FLG ='0'                       " & vbNewLine _
    '                                            & "      WHERE UNSO.CUST_CD_L   = @CUST_CD_L                " & vbNewLine _
    '                                            & "	       AND UNSO.NRS_BR_CD   = @NRS_BR_CD                " & vbNewLine _
    '                                            & "        AND UNSO.SYS_DEL_FLG = '0'                       " & vbNewLine _
    '                                            & "		   AND LEFT(UNSO.CUST_REF_NO,8) = @ORDER_NO         " & vbNewLine _
    '                                            & "       --AND UNCHIN.DECI_UNCHIN <>'0'                    " & vbNewLine _
    '                                            & "     --AND LEFT(F_UNSO_L.OUTKA_PLAN_DATE,6)= '201609'    " & vbNewLine _
    '                                            & "      GROUP BY LEFT(UNSO.CUST_REF_NO,8)) AS MAIN         " & vbNewLine

#End Region

#End Region

#Region "ワーク突合せ結果更新処理"

#Region "更新句"

    ''' <summary>
    ''' 更新処理 UPDATE句 塩浜版
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TSUKIAWASE As String = "UPDATE LM_TRN..WK_JX_UNCHIN                                           " & vbNewLine _
                                                    & " SET NRS_UNCHIN      = MAIN.NRS_UNCHIN                              " & vbNewLine _
                                                    & "    ,SAGAKU          = (ISNULL(MAIN.NRS_UNCHIN,0) - WKJX.JX_UNCHIN) " & vbNewLine _
                                                    & "    ,UNSO_NO_L       = MAIN.UNSO_NO_L                               " & vbNewLine _
                                                    & "    ,OUTKA_PLAN_DATE = MAIN.OUTKA_PLAN_DATE                         " & vbNewLine _
                                                    & "    ,GET_FLG    = '1'                                               " & vbNewLine _
                                                    & "FROM                                                                " & vbNewLine _
                                                    & "   $LM_TRN$..WK_JX_UNCHIN WKJX                                      " & vbNewLine _
                                                    & "   LEFT JOIN                                                        " & vbNewLine _
                                                    & "    (SELECT LEFT(UNSO.CUST_REF_NO,8)   AS ORDER_NO                  " & vbNewLine _
                                                    & "       ,SUM(UNCHIN.DECI_UNCHIN)        AS NRS_UNCHIN                " & vbNewLine _
                                                    & "       ,MAX(UNSO.UNSO_NO_L)            AS UNSO_NO_L                 " & vbNewLine _
                                                    & "       ,MAX(UNSO.OUTKA_PLAN_DATE)      AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                    & "      FROM  $LM_TRN$..F_UNSO_L UNSO                                 " & vbNewLine _
                                                    & "      LEFT JOIN  $LM_TRN$..F_UNCHIN_TRS  UNCHIN                     " & vbNewLine _
                                                    & "        ON UNSO.UNSO_NO_L     = UNCHIN.UNSO_NO_L                    " & vbNewLine _
                                                    & "       AND UNCHIN.SYS_DEL_FLG ='0'                                  " & vbNewLine _
                                                    & "      INNER JOIN  $LM_MST$..Z_KBN KBN                               " & vbNewLine _
                                                    & "        ON KBN.KBN_GROUP_CD = 'C032'                                " & vbNewLine _
                                                    & "        AND KBN.KBN_NM1      = @NRS_BR_CD                           " & vbNewLine _
                                                    & "        AND KBN.KBN_NM2      = UNSO.CUST_CD_L                       " & vbNewLine _
                                                    & "        AND KBN.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                                    & "      WHERE UNSO.NRS_BR_CD   = @NRS_BR_CD                           " & vbNewLine _
                                                    & "        AND UNSO.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                    & "      GROUP BY LEFT(UNSO.CUST_REF_NO,8))   AS MAIN                  " & vbNewLine _
                                                    & "    ON MAIN.ORDER_NO  =  WKJX.SASHIZU_NO                            " & vbNewLine _
                                                    & "WHERE  WKJX.USER_ID   = @USER_ID                                    " & vbNewLine _
                                                    & "--  AND MAIN.ORDER_NO = LEFT(WKJXL.SASHIZU_NO,8)                    " & vbNewLine _
                                                    & "  AND MAIN.ORDER_NO IS NOT NULL                                     " & vbNewLine

    ''' <summary>
    ''' 更新処理 UPDATE句 大阪版
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TSUKIAWASE_OSAKA As String = "UPDATE LM_TRN..WK_JX_UNCHIN                                           " & vbNewLine _
                                                    & " SET NRS_UNCHIN      = MAIN.NRS_UNCHIN                              " & vbNewLine _
                                                    & "    ,SAGAKU          = (ISNULL(MAIN.NRS_UNCHIN,0) - WKJX.JX_UNCHIN) " & vbNewLine _
                                                    & "    ,UNSO_NO_L       = MAIN.UNSO_NO_L                               " & vbNewLine _
                                                    & "    ,OUTKA_PLAN_DATE = MAIN.OUTKA_PLAN_DATE                         " & vbNewLine _
                                                    & "    ,GET_FLG    = '1'                                               " & vbNewLine _
                                                    & "FROM                                                                " & vbNewLine _
                                                    & "   $LM_TRN$..WK_JX_UNCHIN WKJX                                      " & vbNewLine _
                                                    & "   LEFT JOIN                                                        " & vbNewLine _
                                                    & "    (SELECT LEFT(UNSO.CUST_REF_NO,8)   AS ORDER_NO                          " & vbNewLine _
                                                    & "       ,SUM(UNCHIN.DECI_UNCHIN)        AS NRS_UNCHIN                " & vbNewLine _
                                                    & "       ,MAX(UNSO.UNSO_NO_L)            AS UNSO_NO_L                 " & vbNewLine _
                                                    & "       ,MAX(UNSO.OUTKA_PLAN_DATE)      AS OUTKA_PLAN_DATE           " & vbNewLine _
                                                    & "      FROM  $LM_TRN$..F_UNSO_L UNSO                                 " & vbNewLine _
                                                    & "      LEFT JOIN  $LM_TRN$..F_UNCHIN_TRS  UNCHIN                     " & vbNewLine _
                                                    & "        ON UNSO.UNSO_NO_L     = UNCHIN.UNSO_NO_L                    " & vbNewLine _
                                                    & "       AND UNCHIN.SYS_DEL_FLG ='0'                                  " & vbNewLine _
                                                    & "      INNER JOIN  $LM_MST$..Z_KBN KBN                               " & vbNewLine _
                                                    & "        ON KBN.KBN_GROUP_CD = 'C032'                                " & vbNewLine _
                                                    & "        AND KBN.KBN_NM1      = @NRS_BR_CD                           " & vbNewLine _
                                                    & "        AND KBN.KBN_NM2      = UNSO.CUST_CD_L                       " & vbNewLine _
                                                    & "        AND KBN.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
                                                    & "      WHERE UNSO.NRS_BR_CD   = @NRS_BR_CD                           " & vbNewLine _
                                                    & "        AND UNSO.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                                    & "      GROUP BY UNSO.CUST_REF_NO)   AS MAIN                          " & vbNewLine _
                                                    & "    ON MAIN.ORDER_NO  =  WKJX.SASHIZU_NO                            " & vbNewLine _
                                                    & "WHERE  WKJX.USER_ID   = @USER_ID                                    " & vbNewLine _
                                                    & "--  AND MAIN.ORDER_NO = WKJXL.SASHIZU_NO                           " & vbNewLine _
                                                    & "  AND MAIN.ORDER_NO IS NOT NULL                                     " & vbNewLine



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

#Region "SQLメイン処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI420IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        _SqlPrmList.Add(GetSqlParameter("@USER_ID", Me._Row("USER_ID"), DBDataType.CHAR))

        'SQL作成
        Me._StrSql.Append(LMI420DAC.SQL_SELECT_KENSAKU)        '

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI420DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ORDER_NO", "SASHIZU_NO")
        map.Add("JX_UNCHIN", "JX_UNCHIN")
        map.Add("NRS_UNCHIN", "NRS_UNCHIN")
        map.Add("SAGAKU", "SAGAKU")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("UNSO_NO_L", "UNSO_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI420OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "JX ワーク明細テーブルDELETE"
    ''' <summary>
    ''' JX ワーク明細テーブルDelete（DAC）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>WA-KU 明細テーブル新規登録SQLの構築・発行</remarks>
    Private Function DeleteExcelRec(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI420IN")
        Dim row As DataRow = inTbl.Rows(0)

        If row IsNot Nothing Then


            'SQLパラメータ初期化
            _SqlPrmList = New ArrayList
            'SQL格納変数の初期化
            _StrSql = New StringBuilder

            'SQL構築
            _StrSql.Append("DELETE  $LM_TRN$..WK_JX_UNCHIN          " & vbNewLine)
            _StrSql.Append(" WHERE USER_ID = @USER_ID               " & vbNewLine)

            'パラメータ設定
            _SqlPrmList.Add(GetSqlParameter("@USER_ID", row("USER_ID"), DBDataType.CHAR))

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            Dim cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            Logger.WriteSQLLog("LMI420DAC", "DeleteExcelRec", cmd)

            'SQLの発行
            Dim rtn As Integer = Me.GetDeleteResult(cmd)

        End If

        Return ds

    End Function
#End Region

#Region "JX ワーク明細テーブルINSERT"
    ''' <summary>
    ''' JX ワークテーブルINSERT（DAC）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ワーク明細テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertExcelRec(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI420IN")
        Dim row As DataRow

        If inTbl IsNot Nothing Then

            For i As Integer = 0 To inTbl.Rows.Count - 1

                row = inTbl.Rows(i)

                'SQLパラメータ初期化
                _SqlPrmList = New ArrayList
                'SQL格納変数の初期化
                _StrSql = New StringBuilder

                'SQL構築
                _StrSql.Append("INSERT INTO  $LM_TRN$..WK_JX_UNCHIN  (          " & vbNewLine)
                _StrSql.Append(" USER_ID                             " & vbNewLine)
                _StrSql.Append(",ROW_NO                              " & vbNewLine)
                _StrSql.Append(",SASHIZU_NO                          " & vbNewLine)
                _StrSql.Append(",JX_UNCHIN                           " & vbNewLine)
                _StrSql.Append(",SYS_ENT_DATE                        " & vbNewLine)
                _StrSql.Append(",SYS_ENT_TIME                        " & vbNewLine)
                _StrSql.Append(",SYS_ENT_PGID                        " & vbNewLine)
                _StrSql.Append(",SYS_ENT_USER                        " & vbNewLine)
                _StrSql.Append(",SYS_UPD_DATE                        " & vbNewLine)
                _StrSql.Append(",SYS_UPD_TIME                        " & vbNewLine)
                _StrSql.Append(",SYS_UPD_PGID                        " & vbNewLine)
                _StrSql.Append(",SYS_UPD_USER                        " & vbNewLine)
                _StrSql.Append(",SYS_DEL_FLG                         " & vbNewLine)
                _StrSql.Append(") VALUES (                           " & vbNewLine)
                _StrSql.Append(" @USER_ID                            " & vbNewLine)
                _StrSql.Append(",@ROW_NO                             " & vbNewLine)
                _StrSql.Append(",@SASHIZU_NO                        " & vbNewLine)
                _StrSql.Append(",@JX_UNCHIN                          " & vbNewLine)
                _StrSql.Append(",@SYS_ENT_DATE                       " & vbNewLine)
                _StrSql.Append(",@SYS_ENT_TIME                       " & vbNewLine)
                _StrSql.Append(",@SYS_ENT_PGID                       " & vbNewLine)
                _StrSql.Append(",@SYS_ENT_USER                       " & vbNewLine)
                _StrSql.Append(",@SYS_UPD_DATE                       " & vbNewLine)
                _StrSql.Append(",@SYS_UPD_TIME                       " & vbNewLine)
                _StrSql.Append(",@SYS_UPD_PGID                       " & vbNewLine)
                _StrSql.Append(",@SYS_UPD_USER                       " & vbNewLine)
                _StrSql.Append(",@SYS_DEL_FLG                        " & vbNewLine)
                _StrSql.Append(")                                    " & vbNewLine)

                'パラメータ設定
                _SqlPrmList.Add(GetSqlParameter("@USER_ID", row("USER_ID"), DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@ROW_NO", row("ROW_NO"), DBDataType.NUMERIC))
                _SqlPrmList.Add(GetSqlParameter("@SASHIZU_NO", row("ORDER_NO"), DBDataType.VARCHAR))
                _SqlPrmList.Add(GetSqlParameter("@JX_UNCHIN", row("JX_UNCHIN"), DBDataType.NUMERIC))
                _SqlPrmList.Add(GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID() & "", DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_ENT_USER", Me.GetUserID() & "", DBDataType.VARCHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate() & "", DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime() & "", DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID() & "", DBDataType.CHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_UPD_USER", Me.GetUserID() & "", DBDataType.VARCHAR))
                _SqlPrmList.Add(GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

                'スキーマ名設定
                Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

                'SQL文のコンパイル
                Dim cmd As SqlCommand = Me.CreateSqlCommand(sql)

                'パラメータの反映
                For Each obj As Object In _SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                Logger.WriteSQLLog("LMI420DAC", "InsertExcelRec", cmd)

                'SQLの発行
                Dim rtn As Integer = Me.GetInsertResult(cmd)
            Next

        End If

        Return ds

    End Function
#End Region

#Region "JX ワーク明細テーブルUpdate"
    ''' <summary>
    ''' JX ワーク明細テーブルUpdate DAC）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ワーク明細テーブル更新処理SQLの構築・発行</remarks>
    Private Function UpdateExcelRec(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI420IN")
        Dim row As DataRow = inTbl.Rows(0)

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList
        'SQL格納変数の初期化
        _StrSql = New StringBuilder

       'パラメータ設定
        Dim br As String = row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", br, DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        _SqlPrmList.Add(GetSqlParameter("@USER_ID", row("USER_ID"), DBDataType.CHAR))

        'SQL作成
        'If row.Item("NRS_BR_CD").ToString.Trim.Equals("20") Then
        '    '大阪版
        Me._StrSql.Append(LMI420DAC.SQL_UPDATE_TSUKIAWASE_OSAKA)
        'Else
        ''塩浜版
        'Me._StrSql.Append(LMI420DAC.SQL_UPDATE_TSUKIAWASE)
        'End If


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        Logger.WriteSQLLog("LMI420DAC", "UpdateExcelRec", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

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
