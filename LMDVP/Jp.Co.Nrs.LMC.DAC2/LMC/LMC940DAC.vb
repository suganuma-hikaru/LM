' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC940    : ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV(大黒)出力
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC940DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC940DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成データ検索用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KANGAROO_CSV As String _
            = " SELECT                                                                              " & vbNewLine _
            & "     COL.NRS_BR_CD,                                                                  " & vbNewLine _
            & "     COL.OUTKA_NO_L,                                                                 " & vbNewLine _
            & "     COL.OUTKA_PLAN_DATE AS OUTKA_DATE,                                              " & vbNewLine _
            & "     COL.OUTKA_PKG_NB AS KOSU,                                                       " & vbNewLine _
            & "     CONVERT(int,UNSOL.UNSO_WT) AS JURYO,                                            " & vbNewLine _
            & "     COL.DEST_TEL,                                                                   " & vbNewLine _
            & "     CASE WHEN COL.DEST_KB = '00'                                                    " & vbNewLine _
            & "         THEN DEST.DEST_NM                                                           " & vbNewLine _
            & "         ELSE COL.DEST_NM                                                            " & vbNewLine _
            & "     END AS DEST_NM1,                                                                " & vbNewLine _
            & "     '' AS DEST_NM2,                                                                 " & vbNewLine _
            & "     CASE WHEN ISNULL(EDIL.DEST_ZIP,'') = ''                                         " & vbNewLine _
            & "          THEN ISNULL(DEST.ZIP,'')                                                   " & vbNewLine _
            & "          ELSE EDIL.DEST_ZIP END AS DEST_ZIP,                                        " & vbNewLine _
            & "     LTRIM(COL.DEST_AD_1) + LTRIM(COL.DEST_AD_2) + LTRIM(COL.DEST_AD_3) AS DEST_AD1, " & vbNewLine _
            & "     '' AS DEST_AD2,                                                                 " & vbNewLine _
            & "     '' AS DEST_AD3,                                                                 " & vbNewLine _
            & "     COL.OUTKA_NO_L AS KIJI1,                                                        " & vbNewLine _
            & "     MGS.GOODS_NM_1 AS KIJI2,                                                        " & vbNewLine _
            & "     UNSOL.REMARK AS KIJI3,                                                          " & vbNewLine _
            & "     COL.CUST_ORD_NO AS KIJI4,                                                       " & vbNewLine _
            & "     COL.BUYER_ORD_NO AS KIJI5,                                                      " & vbNewLine _
            & "     COL.ARR_PLAN_DATE AS COMMENT1,                                                  " & vbNewLine _
            & "     '1' AS COMMENT2,                                                                " & vbNewLine _
            & "     CASE WHEN COL.DEST_KB = '00'                                                    " & vbNewLine _
            & "         THEN COL.CUST_CD_L + COL.DEST_CD                                            " & vbNewLine _
            & "         ELSE COL.CUST_CD_L + COL.DEST_NM                                            " & vbNewLine _
            & "     END AS SUMMARY_KEY,                                                             " & vbNewLine _
            & "     @ROW_NO AS ROW_NO,                                                              " & vbNewLine _
            & "     COL.SYS_UPD_DATE,                                                               " & vbNewLine _
            & "     COL.SYS_UPD_TIME,                                                               " & vbNewLine _
            & "     @FILEPATH AS FILEPATH,                                                          " & vbNewLine _
            & "     @FILENAME AS FILENAME,                                                          " & vbNewLine _
            & "     @SYS_DATE AS SYS_DATE,                                                          " & vbNewLine _
            & "     @SYS_TIME AS SYS_TIME                                                           " & vbNewLine _
            & " FROM                                                                                " & vbNewLine _
            & "     $LM_TRN$..C_OUTKA_L COL                                                         " & vbNewLine _
            & "     LEFT JOIN (                                                                     " & vbNewLine _
            & "         SELECT                                                                      " & vbNewLine _
            & "             NRS_BR_CD,                                                              " & vbNewLine _
            & "             OUTKA_NO_L,                                                             " & vbNewLine _
            & "             MIN(OUTKA_NO_M) AS OUTKA_NO_M                                           " & vbNewLine _
            & "         FROM                                                                        " & vbNewLine _
            & "             $LM_TRN$..C_OUTKA_M                                                     " & vbNewLine _
            & "         WHERE                                                                       " & vbNewLine _
            & "             SYS_DEL_FLG = '0'                                                       " & vbNewLine _
            & "         GROUP BY                                                                    " & vbNewLine _
            & "             NRS_BR_CD,                                                              " & vbNewLine _
            & "             OUTKA_NO_L                                                              " & vbNewLine _
            & "         ) COM_MIN                                                                   " & vbNewLine _
            & "         ON                                                                          " & vbNewLine _
            & "             COM_MIN.NRS_BR_CD = COL.NRS_BR_CD                                       " & vbNewLine _
            & "             AND COM_MIN.OUTKA_NO_L = COL.OUTKA_NO_L                                 " & vbNewLine _
            & "     LEFT JOIN                                                                       " & vbNewLine _
            & "         $LM_TRN$..C_OUTKA_M COM                                                     " & vbNewLine _
            & "         ON                                                                          " & vbNewLine _
            & "             COM.NRS_BR_CD = COM_MIN.NRS_BR_CD                                       " & vbNewLine _
            & "             AND COM.OUTKA_NO_L = COM_MIN.OUTKA_NO_L                                 " & vbNewLine _
            & "             AND COM.OUTKA_NO_M = COM_MIN.OUTKA_NO_M                                 " & vbNewLine _
            & "     LEFT JOIN                                                                       " & vbNewLine _
            & "         $LM_MST$..M_GOODS MGS                                                       " & vbNewLine _
            & "         ON                                                                          " & vbNewLine _
            & "             MGS.NRS_BR_CD = COM.NRS_BR_CD                                           " & vbNewLine _
            & "             AND MGS.GOODS_CD_NRS = COM.GOODS_CD_NRS                                 " & vbNewLine _
            & "             AND MGS.SYS_DEL_FLG = '0'                                               " & vbNewLine _
            & "     LEFT JOIN                                                                       " & vbNewLine _
            & "         $LM_TRN$..F_UNSO_L UNSOL                                                    " & vbNewLine _
            & "         ON                                                                          " & vbNewLine _
            & "             UNSOL.NRS_BR_CD = COL.NRS_BR_CD                                         " & vbNewLine _
            & "             AND UNSOL.INOUTKA_NO_L = COL.OUTKA_NO_L                                 " & vbNewLine _
            & "             AND UNSOL.MOTO_DATA_KB = '20'                                           " & vbNewLine _
            & "             AND UNSOL.SYS_DEL_FLG = '0'                                             " & vbNewLine _
            & "     LEFT JOIN                                                                       " & vbNewLine _
            & "         $LM_MST$..M_DEST DEST                                                       " & vbNewLine _
            & "         ON                                                                          " & vbNewLine _
            & "             DEST.NRS_BR_CD = COL.NRS_BR_CD                                          " & vbNewLine _
            & "             AND DEST.CUST_CD_L = COL.CUST_CD_L                                      " & vbNewLine _
            & "             AND DEST.DEST_CD = COL.DEST_CD                                          " & vbNewLine _
            & "             AND DEST.SYS_DEL_FLG = '0'                                              " & vbNewLine _
            & "     LEFT JOIN (                                                                     " & vbNewLine _
            & "         SELECT                                                                      " & vbNewLine _
            & "             NRS_BR_CD,                                                              " & vbNewLine _
            & "             OUTKA_CTL_NO,                                                           " & vbNewLine _
            & "             DEST_ZIP                                                                " & vbNewLine _
            & "         FROM                                                                        " & vbNewLine _
            & "             $LM_TRN$..H_OUTKAEDI_L                                                  " & vbNewLine _
            & "         WHERE                                                                       " & vbNewLine _
            & "             SYS_DEL_FLG = '0'                                                       " & vbNewLine _
            & "         GROUP BY                                                                    " & vbNewLine _
            & "             NRS_BR_CD,                                                              " & vbNewLine _
            & "             OUTKA_CTL_NO,                                                           " & vbNewLine _
            & "             DEST_ZIP                                                                " & vbNewLine _
            & "         ) EDIL                                                                      " & vbNewLine _
            & "         ON                                                                          " & vbNewLine _
            & "             EDIL.NRS_BR_CD = COL.NRS_BR_CD                                          " & vbNewLine _
            & "             AND EDIL.OUTKA_CTL_NO = COL.OUTKA_NO_L                                  " & vbNewLine _
            & " WHERE                                                                               " & vbNewLine _
            & "     COL.NRS_BR_CD = @NRS_BR_CD                                                      " & vbNewLine _
            & "     AND COL.OUTKA_NO_L = @OUTKA_NO_L                                                " & vbNewLine _
            & "     AND COL.SYS_DEL_FLG = '0'                                                       " & vbNewLine

#End Region

#Region "更新 SQL"

    Private Const SQL_UPDATE_KANGAROO_CSV As String _
            = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
            & " DENP_FLAG         = '01'            " & vbNewLine _
            & ",SYS_UPD_DATE      = @SYS_UPD_DATE   " & vbNewLine _
            & ",SYS_UPD_TIME      = @SYS_UPD_TIME   " & vbNewLine _
            & ",SYS_UPD_PGID      = @SYS_UPD_PGID   " & vbNewLine _
            & ",SYS_UPD_USER      = @SYS_UPD_USER   " & vbNewLine _
            & "WHERE NRS_BR_CD    = @NRS_BR_CD      " & vbNewLine _
            & "  AND OUTKA_NO_L   = @OUTKA_NO_L     " & vbNewLine

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
    ''' ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKangarooMagicCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC940IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC940DAC.SQL_SELECT_KANGAROO_CSV)

        '条件設定
        Call setSQLSelect()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC940DAC", "SelectKangarooMagicGeneralCsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("KOSU", "KOSU")
        map.Add("JURYO", "JURYO")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_NM1", "DEST_NM1")
        map.Add("DEST_NM2", "DEST_NM2")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD1", "DEST_AD1")
        map.Add("DEST_AD2", "DEST_AD2")
        map.Add("DEST_AD3", "DEST_AD3")
        map.Add("KIJI1", "KIJI1")
        map.Add("KIJI2", "KIJI2")
        map.Add("KIJI3", "KIJI3")
        map.Add("KIJI4", "KIJI4")
        map.Add("KIJI5", "KIJI5")
        map.Add("COMMENT1", "COMMENT1")
        map.Add("COMMENT2", "COMMENT2")
        map.Add("SUMMARY_KEY", "SUMMARY_KEY")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC940OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC940OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKangarooCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC940OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC940DAC.SQL_UPDATE_KANGAROO_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC940DAC", "UpdateKangarooCsv", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region

#Region "設定処理"

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

    ''' <summary>
    '''  パラメータ設定モジュール（出荷検索）
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

End Class
