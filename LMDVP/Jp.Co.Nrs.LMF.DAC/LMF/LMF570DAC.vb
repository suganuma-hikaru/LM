' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF570DAC : 配送指図書
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF570DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF570DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                      " & vbNewLine _
                                            & "UNSO_L.NRS_BR_CD                                      AS NRS_BR_CD   " & vbNewLine _
                                            & "	,'A6'                                                AS PTN_ID      " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                   " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                   " & vbNewLine _
                                            & "	      ELSE MR3.PTN_CD END                            AS PTN_CD      " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                   " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                   " & vbNewLine _
                                            & "       ELSE MR3.RPT_ID END                            AS RPT_ID      " & vbNewLine


#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                       " & vbNewLine _
                                     & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                   " & vbNewLine _
                                     & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                   " & vbNewLine _
                                     & "  ELSE MR3.RPT_ID END      AS RPT_ID                                 " & vbNewLine _
                                     & ", UNSO_L.UNSO_NO_L        AS UNSO_NO_L                               " & vbNewLine _
                                     & ", UNSO_L.UNSO_CD          AS UNSO_CD                                 " & vbNewLine _
                                     & ", UNSO_L.UNSO_BR_CD       AS UNSO_BR_CD                              " & vbNewLine _
                                     & "--2018/04/19 001391【LMS】配送一覧表_納品予定日追加-既存クエリ追加 Annen add start " & vbNewLine _
                                     & ", UNSO_L.ARR_PLAN_DATE  AS ARR_PLAN_DATE                             " & vbNewLine _
                                     & "--2018/04/19 001391【LMS】配送一覧表_納品予定日追加-既存クエリ追加 Annen add end " & vbNewLine _
                                     & ", UNSO_L.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                         " & vbNewLine _
                                     & ", UNSO_L.CUST_CD_L        AS CUST_CD_L                               " & vbNewLine _
                                     & ", UNSO_L.CUST_CD_M        AS CUST_CD_M                               " & vbNewLine _
                                     & ", UNSO_L.REMARK           AS REMARK                                  " & vbNewLine _
                                     & ", UNSO_L.UNSO_WT          AS UNSO_WT                                 " & vbNewLine _
                                     & ", SUM(UNSO_M.UNSO_TTL_NB) AS UNSO_TTL_NB                             " & vbNewLine _
                                     & ", M_CUST.CUST_NM_L        AS CUST_NM_L                               " & vbNewLine _
                                     & ", M_CUST.CUST_NM_M        AS CUST_NM_M                               " & vbNewLine _
                                     & ", M_DEST.DEST_NM          AS DEST_NM                                 " & vbNewLine _
                                     & ", M_DEST.TEL              AS TEL                                     " & vbNewLine _
                                     & ", M_DEST.AD_1+M_DEST.AD_2 AS DEST_AD                                 " & vbNewLine _
                                     & ", M_UNSOCO.UNSOCO_NM      AS UNSOCO_NM                               " & vbNewLine _
                                     & ", M_UNSOCO.UNSOCO_BR_NM   AS UNSOCO_BR_NM                            " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "FROM                                                                " & vbNewLine _
                                     & "$LM_TRN$..F_UNSO_L UNSO_L                                           " & vbNewLine _
                                     & "      -- 運送(中)                                                   " & vbNewLine _
                                     & "         LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M ON                     " & vbNewLine _
                                     & "                    UNSO_L.NRS_BR_CD = UNSO_M.NRS_BR_CD AND         " & vbNewLine _
                                     & "                    UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L AND         " & vbNewLine _
                                     & "                    UNSO_M.SYS_DEL_FLG = '0'                        " & vbNewLine _
                                     & "      -- 届先マスタ                                                 " & vbNewLine _
                                     & "         LEFT JOIN $LM_MST$..M_DEST M_DEST ON                       " & vbNewLine _
                                     & "                     UNSO_L.NRS_BR_CD = M_DEST.NRS_BR_CD AND        " & vbNewLine _
                                     & "                     UNSO_L.CUST_CD_L = M_DEST.CUST_CD_L AND        " & vbNewLine _
                                     & "                     UNSO_L.DEST_CD = M_DEST.DEST_CD AND            " & vbNewLine _
                                     & "                     M_DEST.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                     & "      -- 運送会社マスタ                                             " & vbNewLine _
                                     & "         LEFT JOIN $LM_MST$..M_UNSOCO M_UNSOCO ON                   " & vbNewLine _
                                     & "                     UNSO_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD AND      " & vbNewLine _
                                     & "                     UNSO_L.UNSO_CD = M_UNSOCO.UNSOCO_CD AND        " & vbNewLine _
                                     & "                     UNSO_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD AND  " & vbNewLine _
                                     & "                     M_UNSOCO.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "      -- 荷主マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST M_CUST                       " & vbNewLine _
                                     & "                   ON M_CUST.NRS_BR_CD       = UNSO_L.NRS_BR_CD     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_L       = UNSO_L.CUST_CD_L     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_M       = UNSO_L.CUST_CD_M     " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_S       = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.CUST_CD_SS      = '00'                 " & vbNewLine _
                                     & "                  AND M_CUST.SYS_DEL_FLG     = '0'                  " & vbNewLine _
                                     & "      -- 商品マスタ                                                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                     " & vbNewLine _
                                     & "                   ON M_GOODS.NRS_BR_CD      = UNSO_M.NRS_BR_CD     " & vbNewLine _
                                     & "                  AND M_GOODS.GOODS_CD_NRS   = UNSO_M.GOODS_CD_NRS  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(UNSO_Lの荷主より取得)                 " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1               " & vbNewLine _
                                     & "                   ON M_CUSTRPT1.NRS_BR_CD   = UNSO_L.NRS_BR_CD     " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_L   = UNSO_L.CUST_CD_L     " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_M   = UNSO_L.CUST_CD_M     " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.PTN_ID      = 'A6'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT1.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                          " & vbNewLine _
                                     & "                   ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID    " & vbNewLine _
                                     & "                  AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD    " & vbNewLine _
                                     & "                  AND MR1.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ②(商品マスタより)                       " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2               " & vbNewLine _
                                     & "                   ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD    " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L    " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M    " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.CUST_CD_S   = '00'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.PTN_ID      = 'A6'                 " & vbNewLine _
                                     & "                  AND M_CUSTRPT2.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                          " & vbNewLine _
                                     & "                   ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD " & vbNewLine _
                                     & "                  AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID    " & vbNewLine _
                                     & "                  AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD    " & vbNewLine _
                                     & "                  AND MR2.SYS_DEL_FLG        = '0'                  " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >   " & vbNewLine _
                                     & "      LEFT OUTER JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                                     & "                   ON MR3.NRS_BR_CD          =  UNSO_L.NRS_BR_CD    " & vbNewLine _
                                     & "                  AND MR3.PTN_ID             = 'A6'                 " & vbNewLine _
                                     & "                  AND MR3.STANDARD_FLAG      = '01'                 " & vbNewLine _
                                     & "                  AND MR3.SYS_DEL_FLG        = '0'                  " & vbNewLine

#End Region

#Region "GROUP BY"
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_GROUP_BY As String = " GROUP BY                                                    " & vbNewLine _
                                 & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                   " & vbNewLine _
                                 & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                   " & vbNewLine _
                                 & "  ELSE MR3.RPT_ID END                                                " & vbNewLine _
                                 & ", UNSO_L.UNSO_NO_L                                                   " & vbNewLine _
                                 & ", UNSO_L.UNSO_CD                                                     " & vbNewLine _
                                 & ", UNSO_L.UNSO_BR_CD                                                  " & vbNewLine _
                                 & "--2018/04/19 001391【LMS】配送一覧表_納品予定日追加-既存クエリ追加 Annen upd start " & vbNewLine _
                                 & ", UNSO_L.ARR_PLAN_DATE                                               " & vbNewLine _
                                 & "--2018/04/19 001391【LMS】配送一覧表_納品予定日追加-既存クエリ追加 Annen upd end " & vbNewLine _
                                 & ", UNSO_L.OUTKA_PLAN_DATE                                             " & vbNewLine _
                                 & ", UNSO_L.CUST_CD_L                                                   " & vbNewLine _
                                 & ", UNSO_L.CUST_CD_M                                                   " & vbNewLine _
                                 & ", UNSO_L.REMARK                                                      " & vbNewLine _
                                 & ", UNSO_L.UNSO_WT                                                     " & vbNewLine _
                                 & ", M_CUST.CUST_NM_L                                                   " & vbNewLine _
                                 & ", M_CUST.CUST_NM_M                                                   " & vbNewLine _
                                 & ", M_DEST.DEST_NM                                                     " & vbNewLine _
                                 & ", M_DEST.TEL                                                         " & vbNewLine _
                                 & ", M_DEST.AD_1+M_DEST.AD_2                                            " & vbNewLine _
                                 & ", M_UNSOCO.UNSOCO_NM                                                 " & vbNewLine _
                                 & ", M_UNSOCO.UNSOCO_BR_NM                                              " & vbNewLine

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                " & vbNewLine _
                                & "  UNSO_L.UNSO_NO_L ASC                           " & vbNewLine


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
    '''出力対象帳票パターン取得処理(仮)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF570IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF570DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF570DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF570DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds


    End Function

    ''' <summary>
    ''' 運送テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF570IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMF570DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF570DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMF570DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GroupBy句)
        Me._StrSql.Append(LMF570DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF570DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        '2018/04/19 001391【LMS】配送一覧表_納品予定日追加-既存クエリ追加 Annen upd start
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        '2018/04/19 001391【LMS】配送一覧表_納品予定日追加-既存クエリ追加 Annen upd end
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("TEL", "TEL")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF570OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" UNSO_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運送番号(大)
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO_L.UNSO_NO_L = @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

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

#End Region

#End Region

#End Region

End Class
