' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF650DAC : 都道府県別運送情報一覧
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF650DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF650DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	UNSO_L.NRS_BR_CD                                         AS NRS_BR_CD " & vbNewLine _
                                            & ",'AO'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "	     WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                      " & vbNewLine _
                                            & "	     ELSE MR3.PTN_CD END                                 AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                            & "	     ELSE MR3.RPT_ID END                                 AS RPT_ID    " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                           " & vbNewLine _
                                            & "          CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                        " & vbNewLine _
                                            & "               WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                        " & vbNewLine _
                                            & "          ELSE MR3.RPT_ID END                      AS RPT_ID -- 帳票ID            " & vbNewLine _
                                            & ",UNSO_L.NRS_BR_CD       AS NRS_BR_CD             --日陸営業所コード               " & vbNewLine _
                                            & ",@F_DATE                AS F_DATE                --出荷日FROM(画面項目)           " & vbNewLine _
                                            & ",@T_DATE                AS T_DATE                --出荷日TO(画面項目)             " & vbNewLine _
                                            & ",KEN.KEN_CD             AS KEN_CD                --都道府県コード                 " & vbNewLine _
                                            & ",KEN.KEN_NM             AS KEN_NM                --県名                           " & vbNewLine _
                                            & ",UNSO_L.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE       --出荷予定日                     " & vbNewLine _
                                            & ",UNSO_L.ARR_PLAN_DATE   AS ARR_PLAN_DATE         --納入予定日                     " & vbNewLine _
                                            & ",DEST.DEST_NM           AS DEST_NM               --納入先                         " & vbNewLine _
                                            & ",UNSO_L.UNSO_PKG_NB     AS UNSO_PKG_NB           --数量                           " & vbNewLine _
                                            & ",UNSO_L.NB_UT           AS NB_UT                 --荷姿                           " & vbNewLine _
                                            & ",UNCHIN.SEIQ_WT         AS SEIQ_WT               --重量                           " & vbNewLine _
                                            & ",UNSOCO.UNSOCO_NM       AS UNSOCO_NM             --運送会社名                     " & vbNewLine _
                                            & ",CUST.CUST_NM_L         AS CUST_NM_L             --荷主名(大)                     " & vbNewLine _
                                            & ",CUST.CUST_NM_M         AS CUST_NM_M             --荷主名(中)                     " & vbNewLine _
                                            & ",UNCHIN.SEIQ_UNCHIN     AS SEIQ_UNCHIN           --運賃                           " & vbNewLine _
                                            & ",UNSO_L.UNSO_NO_L       AS UNSO_NO_L             --運送番号L                      " & vbNewLine _
                                            & ",DEST.AD_1              AS DEST_AD_1             --納入先1                        " & vbNewLine _
                                            & ",DEST.AD_2              AS DEST_AD_2             --納入先2                        " & vbNewLine _
                                            & ",DEST.JIS               AS JIS                   --納入先JISコード(ソート用)       " & vbNewLine


#End Region

#Region "FROM句"
    Private Const SQL_FROM As String = "FROM                                                            " & vbNewLine _
                                     & "	--運送L				                                        " & vbNewLine _
                                     & "	$LM_TRN$..F_UNSO_L AS UNSO_L     			                " & vbNewLine _
                                     & "	--荷主マスタ				                                " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_CUST AS CUST				            " & vbNewLine _
                                     & "	ON  UNSO_L.NRS_BR_CD     = CUST.NRS_BR_CD				    " & vbNewLine _
                                     & "	AND UNSO_L.CUST_CD_L     = CUST.CUST_CD_L				    " & vbNewLine _
                                     & "	AND UNSO_L.CUST_CD_M     = CUST.CUST_CD_M				    " & vbNewLine _
                                     & "	AND CUST.CUST_CD_S       = '00'				                " & vbNewLine _
                                     & "	AND CUST.CUST_CD_SS      = '00'				                " & vbNewLine _
                                     & "    --運賃                                                      " & vbNewLine _
                                     & "    LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UNCHIN                     " & vbNewLine _
                                     & "    ON  UNSO_L.NRS_BR_CD     = UNCHIN.NRS_BR_CD                 " & vbNewLine _
                                     & "    AND UNSO_L.UNSO_NO_L     = UNCHIN.UNSO_NO_L                 " & vbNewLine _
                                     & "    --届先マスタ                                                " & vbNewLine _
                                     & "    LEFT JOIN $LM_MST$..M_DEST AS DEST                          " & vbNewLine _
                                     & "    ON  UNSO_L.NRS_BR_CD     = DEST.NRS_BR_CD                   " & vbNewLine _
                                     & "    AND UNSO_L.CUST_CD_L     = DEST.CUST_CD_L                   " & vbNewLine _
                                     & "    AND UNSO_L.DEST_CD       = DEST.DEST_CD                     " & vbNewLine _
                                     & "    --運送会社マスタ                                            " & vbNewLine _
                                     & "    LEFT JOIN $LM_MST$..M_UNSOCO UNSOCO                         " & vbNewLine _
                                     & "    ON  UNSO_L.NRS_BR_CD     = UNSOCO.NRS_BR_CD                 " & vbNewLine _
                                     & "    AND UNSO_L.UNSO_CD       = UNSOCO.UNSOCO_CD                 " & vbNewLine _
                                     & "    AND UNSO_L.UNSO_BR_CD    = UNSOCO.UNSOCO_BR_CD              " & vbNewLine _
                                     & "   --県マスタ                                                   " & vbNewLine _
                                     & "    LEFT JOIN $LM_MST$..M_KEN KEN                               " & vbNewLine _
                                     & "    ON  LEFT (DEST.JIS,2)    = KEN.KEN_CD                       " & vbNewLine _
                                     & "	--運送Lでの荷主帳票パターン取得                             " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                         " & vbNewLine _
                                     & "	ON  UNSO_L.NRS_BR_CD     = MCR1.NRS_BR_CD                   " & vbNewLine _
                                     & "	AND UNSO_L.CUST_CD_L     = MCR1.CUST_CD_L                   " & vbNewLine _
                                     & "	AND UNSO_L.CUST_CD_M     = MCR1.CUST_CD_M                   " & vbNewLine _
                                     & "	AND '00' = MCR1.CUST_CD_S                                   " & vbNewLine _
                                     & "	AND MCR1.PTN_ID = 'AO'                                      " & vbNewLine _
                                     & "	--帳票パターン取得                                          " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_RPT MR1                               " & vbNewLine _
                                     & "	ON  MR1.NRS_BR_CD        = MCR1.NRS_BR_CD                   " & vbNewLine _
                                     & "	AND MR1.PTN_ID           = MCR1.PTN_ID                      " & vbNewLine _
                                     & "	AND MR1.PTN_CD           = MCR1.PTN_CD                      " & vbNewLine _
                                     & "	AND MR1.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "	--運賃TRSの荷主での荷主帳票パターン取得                     " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                         " & vbNewLine _
                                     & "	ON  UNCHIN.NRS_BR_CD     = MCR2.NRS_BR_CD                   " & vbNewLine _
                                     & "	AND UNCHIN.CUST_CD_L     = MCR2.CUST_CD_L                   " & vbNewLine _
                                     & "	AND UNCHIN.CUST_CD_M     = MCR2.CUST_CD_M                   " & vbNewLine _
                                     & "	AND UNCHIN.CUST_CD_S     = MCR2.CUST_CD_S                   " & vbNewLine _
                                     & "	AND MCR2.PTN_ID = 'AO'                                      " & vbNewLine _
                                     & "	--帳票パターン取得                                          " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_RPT MR2                               " & vbNewLine _
                                     & "	ON  MR2.NRS_BR_CD        = MCR2.NRS_BR_CD                   " & vbNewLine _
                                     & "	AND MR2.PTN_ID           = MCR2.PTN_ID                      " & vbNewLine _
                                     & "	AND MR2.PTN_CD           = MCR2.PTN_CD                      " & vbNewLine _
                                     & "	AND MR2.SYS_DEL_FLG      = '0'                              " & vbNewLine _
                                     & "	--存在しない場合の帳票パターン取得                          " & vbNewLine _
                                     & "	LEFT JOIN $LM_MST$..M_RPT MR3                               " & vbNewLine _
                                     & "	ON  MR3.NRS_BR_CD        = UNSO_L.NRS_BR_CD                 " & vbNewLine _
                                     & "	AND MR3.PTN_ID           = 'AO'                             " & vbNewLine _
                                     & "	AND MR3.STANDARD_FLAG    = '01'                             " & vbNewLine _
                                     & "	AND MR3.SYS_DEL_FLG      = '0'                              " & vbNewLine

#End Region

#Region "WHERE"

    ''' <summary>
    ''' WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String = "    WHERE UNSO_L.NRS_BR_CD        = @NRS_BR_CD                   " & vbNewLine _
                                      & "    AND   UNSO_L.OUTKA_PLAN_DATE >= @F_DATE                      " & vbNewLine _
                                      & "    AND   UNSO_L.OUTKA_PLAN_DATE <= @T_DATE                      " & vbNewLine _
                                      & "    AND   UNSO_L.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                      & "    AND   KEN.KEN_CD IS NOT NULL                                 " & vbNewLine

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                " & vbNewLine _
                                         & "     KEN.KEN_CD ASC                     " & vbNewLine _
                                         & "    ,UNSO_L.OUTKA_PLAN_DATE ASC         " & vbNewLine _
                                         & "    ,DEST.JIS ASC                       " & vbNewLine _
                                         & "    ,UNSOCO.UNSOCO_NM ASC               " & vbNewLine _
                                         & "    ,UNSO_L.UNSO_NO_L ASC               " & vbNewLine

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
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF650IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF650DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF650DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMF650DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF650DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF650IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LMF650DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF650DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMF650DAC.SQL_WHERE)            'SQL構築(データ抽出用Where句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)

        Me._StrSql.Append(LMF650DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF650DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("F_DATE", "F_DATE")
        map.Add("T_DATE", "T_DATE")
        map.Add("KEN_CD", "KEN_CD")
        map.Add("KEN_NM", "KEN_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SEIQ_UNCHIN", "SEIQ_UNCHIN")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("JIS", "JIS")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF650OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '出荷日
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", Me._Row.Item("F_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", Me._Row.Item("T_DATE").ToString(), DBDataType.CHAR))

            '荷主コード（大）
            whereStr = Me._Row.Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND   UNSO_L.CUST_CD_L = @CUST_CD_L                     ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = Me._Row.Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	   AND   UNSO_L.CUST_CD_M = @CUST_CD_M                      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            End If

            'JISコード(都道府県コード)
            whereStr = Me._Row.Item("KEN_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("	    AND   KEN.KEN_CD              = @KEN_CD                	      ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_CD", Me._Row.Item("KEN_CD").ToString(), DBDataType.CHAR))
            End If


            'Me._StrSql.Append("           )                                           ")
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
