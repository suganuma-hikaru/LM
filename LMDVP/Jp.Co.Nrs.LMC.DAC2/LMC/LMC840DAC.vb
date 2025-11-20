' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC840    : シグマ 出荷Text作成
'  作  成  者       :  中村
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC840DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC840DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' シグマ 出荷Text作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SIGMA_CSV As String = " SELECT                                                                                      " & vbNewLine _
                                                       & "  OUTKAL.NRS_BR_CD AS NRS_BR_CD                                                          " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST.CUST_DEST_CD                                                            " & vbNewLine _
                                                       & "       ELSE OUTKAL.DEST_CD                                                               " & vbNewLine _
                                                       & "  END AS DEST_CD                                                                         " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_NO_L AS OUTKA_NO_L                                                        " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE                                              " & vbNewLine _
                                                       & " ,OUTKAL.ARR_PLAN_DATE AS ARR_PLAN_DATE                                                  " & vbNewLine _
                                                       & " ,OUTKAL.OUTKA_PKG_NB AS OUTKA_PKG_NB                                                    " & vbNewLine _
                                                       & " ,DEST.EDI_CD AS EDI_CD                                                                           " & vbNewLine _
                                                       & " ,CUST.CUST_CD_L AS CUST_CD_L                                                            " & vbNewLine _
                                                       & " ,OUTKAL.CUST_ORD_NO AS CUST_ORD_NO                                                            " & vbNewLine _
                                                       & " ,'1' AS PRT_CNT                                                                         " & vbNewLine _
                                                       & " ,@ROW_NO AS ROW_NO                                                                      " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_DATE AS SYS_UPD_DATE                                                    " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_TIME AS SYS_UPD_TIME                                                    " & vbNewLine _
                                                       & " ,@FILEPATH AS FILEPATH                                                                  " & vbNewLine _
                                                       & " ,@FILENAME AS FILENAME                                                                  " & vbNewLine _
                                                       & " ,@SYS_DATE AS SYS_DATE                                                                  " & vbNewLine _
                                                       & " ,@SYS_TIME AS SYS_TIME                                                                  " & vbNewLine
    ''' <summary>
    ''' シグマ 出荷Text作成データ検索用SQL FROM・WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SIGMA_CSV_FROM As String = " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                        " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                      " & vbNewLine _
                                                       & " DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                   " & vbNewLine _
                                                       & " DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                                   " & vbNewLine _
                                                       & " DEST.DEST_CD = OUTKAL.DEST_CD                                                           " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST2 ON                                                     " & vbNewLine _
                                                       & " DEST2.NRS_BR_CD = DEST.NRS_BR_CD AND                                                    " & vbNewLine _
                                                       & " DEST2.CUST_CD_L = DEST.CUST_CD_L AND                                                    " & vbNewLine _
                                                       & " DEST2.DEST_CD = DEST.CUST_DEST_CD                                                       " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                      " & vbNewLine _
                                                       & " CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                   " & vbNewLine _
                                                       & " CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                                   " & vbNewLine _
                                                       & " CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                                   " & vbNewLine _
                                                       & " CUST.CUST_CD_S = '00' AND                                                               " & vbNewLine _
                                                       & " CUST.CUST_CD_SS = '00'                                                                  " & vbNewLine _
                                                       & " WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
                                                       & " AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                                     " & vbNewLine _
                                                       & " AND OUTKAL.CUST_CD_L = @CUST_CD_L                                                       " & vbNewLine _
                                                       & " AND OUTKAL.SYS_DEL_FLG = '0'                                                            " & vbNewLine
    '                                                   & " AND (OUTKAL.CUST_CD_L = '00359') OR (OUTKAL.CUST_CD_L = '00631')                        " & vbNewLine _

    '& " LEFT JOIN $LM_MST$..Z_KBN Z1 ON                                                         " & vbNewLine _
    '& " Z1.KBN_GROUP_CD = 'N010' AND                                                            " & vbNewLine _
    '& " OUTKAL.ARR_PLAN_TIME = Z1.KBN_CD                                                        " & vbNewLine _
    '& " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL ON                                                   " & vbNewLine _
    '& " UNSOL.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                                  " & vbNewLine _
    '& " UNSOL.INOUTKA_NO_L = OUTKAL.OUTKA_NO_L AND                                              " & vbNewLine _
    '& " UNSOL.MOTO_DATA_KB = '20' AND                                                           " & vbNewLine _
    '& " UNSOL.SYS_DEL_FLG = '0'                                                                 " & vbNewLine _
    '& " WHERE OUTKAL.NRS_BR_CD = @NRS_BR_CD                                                     " & vbNewLine _
    '& " AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                                     " & vbNewLine _
    '& " AND OUTKAL.SYS_DEL_FLG = '0'                                                            " & vbNewLine

    ''' <summary>
    ''' シグマ 出荷Text作成データ検索用SQL SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SIGMA_CSV_GROUP As String = " ORDER BY                                                                                " & vbNewLine _
                                                       & "  OUTKAL.OUTKA_NO_L                                                                      " & vbNewLine _
                                                       & " ,OUTKAL.CUST_CD_L                                                                       " & vbNewLine _
                                                       & " ,OUTKAL.SYS_UPD_TIME                                                                    " & vbNewLine

    'Private Const SQL_SELECT_SIGMA_CSV_GROUP As String = " GROUP BY                                                                              " & vbNewLine _
    '                                                   & "  OUTKAL.NRS_BR_CD                                                                       " & vbNewLine _
    '                                                   & " ,DEST.CUST_DEST_CD                                                                      " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_CD                                                                         " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_KB                                                                         " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_AD_1                                                                       " & vbNewLine _
    '                                                   & " ,EDIL.DEST_AD_1                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.AD_1                                                                             " & vbNewLine _
    '                                                   & " ,DEST.AD_1                                                                              " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_AD_2                                                                       " & vbNewLine _
    '                                                   & " ,EDIL.DEST_AD_2                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.AD_2                                                                             " & vbNewLine _
    '                                                   & " ,DEST.AD_2                                                                              " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_AD_3                                                                       " & vbNewLine _
    '                                                   & " ,EDIL.DEST_AD_3                                                                         " & vbNewLine _
    '                                                   & " ,DEST2.AD_3                                                                             " & vbNewLine _
    '                                                   & " ,DEST.AD_3                                                                              " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_NM                                                                         " & vbNewLine _
    '                                                   & " ,EDIL.DEST_NM                                                                           " & vbNewLine _
    '                                                   & " ,DEST2.DEST_NM                                                                          " & vbNewLine _
    '                                                   & " ,DEST.DEST_NM                                                                           " & vbNewLine _
    '                                                   & " ,OUTKAL.DEST_TEL                                                                        " & vbNewLine _
    '                                                   & " ,EDIL.DEST_TEL                                                                          " & vbNewLine _
    '                                                   & " ,DEST2.TEL                                                                              " & vbNewLine _
    '                                                   & " ,DEST.TEL                                                                               " & vbNewLine _
    '                                                   & " ,CUST.CUST_CD_L                                                                         " & vbNewLine _
    '                                                   & " ,CUST.AD_1                                                                              " & vbNewLine _
    '                                                   & " ,CUST.AD_2                                                                              " & vbNewLine _
    '                                                   & " ,CUST.AD_3                                                                              " & vbNewLine _
    '                                                   & " ,CUST.CUST_NM_L                                                                         " & vbNewLine _
    '                                                   & " ,CUST.CUST_NM_M                                                                         " & vbNewLine _
    '                                                   & " ,CUST.TEL                                                                               " & vbNewLine _
    '                                                   & " ,OUTKAL.ARR_PLAN_DATE                                                                   " & vbNewLine _
    '                                                   & " ,GOODS.GOODS_NM_1                                                                       " & vbNewLine _
    '                                                   & " ,OUTKAL.BUYER_ORD_NO                                                                    " & vbNewLine _
    '                                                   & " ,Z1.KBN_NM1                                                                             " & vbNewLine _
    '                                                   & " ,OUTKAL.OUTKA_NO_L                                                                      " & vbNewLine _
    '                                                   & " ,OUTKAL.SYS_UPD_DATE                                                                    " & vbNewLine _
    '                                                   & " ,OUTKAL.SYS_UPD_TIME                                                                    " & vbNewLine _
    '                                                   & " ,OUTKAL.OUTKA_NO_L                                                                      " & vbNewLine _
    '                                                   & " ,OUTKAL.OUTKA_PLAN_DATE                                                                 " & vbNewLine _
    '                                                   & " ,UNSOL.UNSO_WT                                                                          " & vbNewLine
#End Region

#Region "更新 SQL"

#Region "シグマ 出荷Text作成"

    Private Const SQL_UPDATE_SIGMA_CSV As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
                                             & " DENP_FLAG         = '01'                         " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                             & "  AND CUST_CD_L    = @CUST_CD_L                   " & vbNewLine _
                                             & "  AND OUTKA_NO_L   = @OUTKA_NO_L                  " & vbNewLine

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
    ''' シグマ 出荷Text作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>シグマ 出荷Text作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSigmaCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC840IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC840DAC.SQL_SELECT_SIGMA_CSV)
        Me._StrSql.Append(LMC840DAC.SQL_SELECT_SIGMA_CSV_FROM)
        Call setSQLSelect()                   '条件設定
        '
        Me._StrSql.Append(LMC840DAC.SQL_SELECT_SIGMA_CSV_GROUP)
        '
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

        MyBase.Logger.WriteSQLLog("LMC840DAC", "SQL_SELECT_SIGMA_CSV", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("ROW_NO", "ROW_NO")
        '20121119
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC840OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC840OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（シグマ出荷CSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSigmaCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC840OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC840DAC.SQL_UPDATE_SIGMA_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC840DAC", "UpdateMeitetuCsv", cmd)

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

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L"), DBDataType.CHAR))


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
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
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
