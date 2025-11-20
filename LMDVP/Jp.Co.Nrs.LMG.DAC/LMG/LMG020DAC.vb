' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG020DAC : 保管料・荷役料計算
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "Count用"

    ''' <summary>
    ''' SQLCountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_START As String = "SELECT COUNT(*) AS SQL_CNT FROM ("
    Private Const SQL_START As String = "SELECT *  FROM ("
    ' Private Const SQL_COUNT_START As String = "SELECT COUNT(CNT.CUST_CD_L) AS SQL_CNT FROM ("

    ''' <summary>
    ''' SQLCountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_END As String = ")CNT"

    Private Const SQL_HEADER As String = "SET ARITHABORT ON " & vbNewLine _
                                              & "SET ARITHIGNORE ON " & vbNewLine

#End Region

#Region "SELECT文"

    ''' <summary>
    ''' SELECT文分割１
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT1 As String = " SELECT DISTINCT                                                       " & vbNewLine _
                                           & " MAIN.CUST_CD_L + '-' + MAIN.CUST_CD_M + '-' + MAIN.CUST_CD_S + '-' + MAIN.CUST_CD_SS    AS CUST_CD         " & vbNewLine _
                                           & " ,MAIN.CUST_NM_L + ' ' + MAIN.CUST_NM_M + ' ' + MAIN.CUST_NM_S + ' ' + MAIN.CUST_NM_SS   AS CUST_NM         " & vbNewLine _
                                           & " ,MAIN.CUST_CD_L                                                                         AS CUST_CD_L       " & vbNewLine _
                                           & " ,MAIN.CUST_CD_M                                                                         AS CUST_CD_M       " & vbNewLine _
                                           & " ,MAIN.CUST_CD_S                                                                         AS CUST_CD_S       " & vbNewLine _
                                           & " ,MAIN.CUST_CD_SS                                                                        AS CUST_CD_SS      " & vbNewLine _
                                           & " ,MAIN.JOB_NO                                                                            AS JOB_NO          " & vbNewLine _
                                           & " ,MAIN.INV_DATE_TO                                                                       AS INV_DATE_TO     " & vbNewLine _
                                           & " ,MAIN.SYS_ENT_USER_NM                                                                          AS SYS_ENT_USER_NM " & vbNewLine _
                                           & " ,MAIN.SYS_ENT_DATE                                                                      AS SYS_ENT_DATE    " & vbNewLine _
                                           & " ,MAIN.OYA_SEIQTO_CD                                                                     AS OYA_SEIQTO_CD   " & vbNewLine _
                                           & " ,MAIN.CUST_NM_L                                                                         AS CUST_NM_L       " & vbNewLine _
                                           & " ,MAIN.CUST_NM_M                                                                         AS CUST_NM_M       " & vbNewLine _
                                           & " ,MAIN.SYS_ENT_PGID                                                                      AS SYS_ENT_PGID    " & vbNewLine


    ''' <summary>
    ''' SELECT文分割２
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT2 As String = "FROM $LM_MST$..M_SEIQTO AS SEIQTO,  "
       
    ''' <summary>
    ''' SELECT文分割３
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT3 As String = " ( SELECT DISTINCT                                               " & vbNewLine _
                                         & " TBL.NRS_BR_CD                             AS NRS_BR_CD         " & vbNewLine _
                                         & " ,TBL.CUST_CD_L                            AS CUST_CD_L         " & vbNewLine _
                                         & " ,TBL.CUST_CD_M                            AS CUST_CD_M         " & vbNewLine _
                                         & " ,TBL.CUST_CD_S                            AS CUST_CD_S         " & vbNewLine _
                                         & " ,TBL.CUST_CD_SS                           AS CUST_CD_SS        " & vbNewLine _
                                         & " ,TBL.JOB_NO                               AS JOB_NO            " & vbNewLine _
                                         & " ,TBL.INV_DATE_TO                          AS INV_DATE_TO       " & vbNewLine _
                                         & " ,SUSER.USER_NM                         AS SYS_ENT_USER_NM      " & vbNewLine _
                                         & " ,TBL.SYS_ENT_DATE                         AS SYS_ENT_DATE      " & vbNewLine _
                                         & " ,CUST.OYA_SEIQTO_CD                       AS OYA_SEIQTO_CD     " & vbNewLine _
                                         & " ,CUST.HOKAN_SEIQTO_CD                     AS HOKAN_SEIQTO_CD   " & vbNewLine _
                                         & " ,CUST.NIYAKU_SEIQTO_CD                     AS NIYAKU_SEIQTO_CD   " & vbNewLine _
                                         & " ,CUST.CUST_NM_L                                                " & vbNewLine _
                                         & " ,CUST.CUST_NM_M                                                " & vbNewLine _
                                         & " ,CUST.CUST_NM_S                                                " & vbNewLine _
                                         & " ,CUST.CUST_NM_SS                                               " & vbNewLine _
                                         & " ,TBL.SYS_ENT_PGID                         AS SYS_ENT_PGID      " & vbNewLine _
                                         & " FROM (                                                         " & vbNewLine

    ''' <summary>
    ''' SELECT文分割４
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT4 As String = " SELECT                                                        " & vbNewLine _
                                         & " SEKY_PRT.NRS_BR_CD                              AS NRS_BR_CD     " & vbNewLine _
                                         & " ,GOODS.CUST_CD_L                            AS CUST_CD_L     " & vbNewLine _
                                         & " ,GOODS.CUST_CD_M                            AS CUST_CD_M     " & vbNewLine _
                                         & " ,GOODS.CUST_CD_S                            AS CUST_CD_S     " & vbNewLine _
                                         & " ,GOODS.CUST_CD_SS                           AS CUST_CD_SS    " & vbNewLine _
                                         & " ,SEKY_PRT.JOB_NO                                AS JOB_NO        " & vbNewLine _
                                         & " ,SEKY_PRT.INV_DATE_TO                           AS INV_DATE_TO   " & vbNewLine _
                                         & " ,SEKY_PRT.SYS_ENT_USER                          AS SYS_ENT_USER  " & vbNewLine _
                                         & " ,SEKY_PRT.SYS_ENT_PGID                          AS SYS_ENT_PGID  " & vbNewLine _
                                         & " ,MAX(SEKY_PRT.SYS_ENT_DATE)                     AS SYS_ENT_DATE  " & vbNewLine _
                                         & "  FROM                                                        " & vbNewLine _
                                         & " $LM_TRN$..G_SEKY_MEISAI_PRT AS SEKY_PRT                                   " & vbNewLine _
                                         & " LEFT JOIN LM_MST..M_GOODS AS GOODS ON                                   " & vbNewLine _
                                         & " SEKY_PRT.NRS_BR_CD = GOODS.NRS_BR_CD                                   " & vbNewLine _
                                         & " AND SEKY_PRT.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                  " & vbNewLine

    ''' <summary>
    ''' SELECT文分割５
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT5 As String = " WHERE SEKY_PRT.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                        ' & " AND SEKY.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                           " & vbNewLine
                                        
    ''' <summary>
    ''' SELECT文分割６
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT6 As String = " GROUP BY                                                                                                                               " & vbNewLine _
                                         & " SEKY_PRT.NRS_BR_CD                                                                                                                        " & vbNewLine _
                                         & " ,GOODS.CUST_CD_L                                                                                                                            " & vbNewLine _
                                         & " ,GOODS.CUST_CD_M                                                                                                                            " & vbNewLine _
                                         & " ,GOODS.CUST_CD_S                                                                                                                            " & vbNewLine _
                                         & " ,GOODS.CUST_CD_SS                                                                                                                           " & vbNewLine _
                                         & " ,SEKY_PRT.JOB_NO                                                                                                                               " & vbNewLine _
                                         & " ,SEKY_PRT.INV_DATE_TO                                                                                                                     " & vbNewLine _
                                         & " ,SEKY_PRT.SYS_ENT_USER                                                                                                                    " & vbNewLine _
                                         & " ,SEKY_PRT.SYS_ENT_PGID                                                                                                                    " & vbNewLine _
                                         & " ) AS TBL                                                                                                                              " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_CUST AS CUST                                                                                                               " & vbNewLine _
                                         & " ON  TBL.NRS_BR_CD = CUST.NRS_BR_CD                                                                                                  " & vbNewLine _
                                         & " AND TBL.CUST_CD_L = CUST.CUST_CD_L                                                                                                    " & vbNewLine _
                                         & " AND TBL.CUST_CD_M = CUST.CUST_CD_M                                                                                                    " & vbNewLine _
                                         & " AND TBL.CUST_CD_S = CUST.CUST_CD_S                                                                                                    " & vbNewLine _
                                         & " AND TBL.CUST_CD_SS = CUST.CUST_CD_SS                                                                                                  " & vbNewLine

    ' ''' <summary>
    ' ''' SELECT文分割7
    ' ''' 
    ' ''' </summary>
    ' ''' <remarks></remarks>
    ' Private Const SQL_SELECT7 As String = " ) AS MAIN                                                                                               " & vbNewLine
                                      
    ''' <summary>
    ''' SELECT文分割８
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT8 As String = " LEFT OUTER JOIN $LM_MST$..S_USER AS SUSER                        " & vbNewLine _
                                         & " ON TBL.SYS_ENT_USER = SUSER.USER_CD                         " & vbNewLine _
                                         & " AND SUSER.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                         & " ) AS MAIN                                                     " & vbNewLine _
                                         & " WHERE MAIN.NRS_BR_CD = SEIQTO.NRS_BR_CD                       " & vbNewLine _
                                         & " AND (MAIN.NIYAKU_SEIQTO_CD = SEIQTO.SEIQTO_CD                   " & vbNewLine _
                                         & " OR MAIN.HOKAN_SEIQTO_CD = SEIQTO.SEIQTO_CD )                  " & vbNewLine _
                                         & " AND SEIQTO.SYS_DEL_FLG = '0'                                  " & vbNewLine



    ''' <summary>
    ''' SELECT文分割９
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT9 As String = "   UNION                                                                                                  " & vbNewLine
                                         
    ''' <summary>
    ''' SELECT文分割10
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT10 As String = "   )AS MAIN2                                                                                                  " & vbNewLine

    ''' <summary>
    ''' SELECT文分割11
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT11 As String = " SELECT                                                        " & vbNewLine _
                                           & " MAIN.CUST_CD_L + '-' + MAIN.CUST_CD_M + '-' + MAIN.CUST_CD_S + '-' + MAIN.CUST_CD_SS    AS CUST_CD         " & vbNewLine _
                                           & " ,MAIN.CUST_NM_L + ' ' + MAIN.CUST_NM_M + ' ' + MAIN.CUST_NM_S + ' ' + MAIN.CUST_NM_SS   AS CUST_NM         " & vbNewLine _
                                           & " ,MAIN.CUST_CD_L                                                                         AS CUST_CD_L       " & vbNewLine _
                                           & " ,MAIN.CUST_CD_M                                                                         AS CUST_CD_M       " & vbNewLine _
                                           & " ,MAIN.CUST_CD_S                                                                         AS CUST_CD_S       " & vbNewLine _
                                           & " ,MAIN.CUST_CD_SS                                                                        AS CUST_CD_SS      " & vbNewLine _
                                           & " ,MAIN.JOB_NO                                                                            AS JOB_NO          " & vbNewLine _
                                           & " ,MAIN.INV_DATE_TO                                                                       AS INV_DATE_TO     " & vbNewLine _
                                           & " ,SUSER.USER_NM                                                                          AS SYS_ENT_USER_NM    " & vbNewLine _
                                           & " ,MAIN.SYS_ENT_DATE                                                                      AS SYS_ENT_DATE    " & vbNewLine _
                                           & " ,MAIN.OYA_SEIQTO_CD                                                                     AS OYA_SEIQTO_CD   " & vbNewLine _
                                           & " ,MAIN.CUST_NM_L                                                                         AS CUST_NM_L       " & vbNewLine _
                                           & " ,MAIN.CUST_NM_M                                                                         AS CUST_NM_M       " & vbNewLine _
                                           & " ,MAIN.SYS_ENT_PGID                                                                      AS SYS_ENT_PGID    " & vbNewLine

    ''' <summary>
    ''' SELECT文分割12
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT12 As String = " ( SELECT DISTINCT                                                     " & vbNewLine _
                                         & " TBL.NRS_BR_CD                             AS NRS_BR_CD                " & vbNewLine _
                                         & " ,TBL.CUST_CD_L                            AS CUST_CD_L                " & vbNewLine _
                                         & " ,TBL.CUST_CD_M                            AS CUST_CD_M                " & vbNewLine _
                                         & " ,TBL.CUST_CD_S                            AS CUST_CD_S                " & vbNewLine _
                                         & " ,TBL.CUST_CD_SS                           AS CUST_CD_SS               " & vbNewLine _
                                         & " ,TBL.JOB_NO                               AS JOB_NO                   " & vbNewLine _
                                         & " ,TBL.INV_DATE_TO                          AS INV_DATE_TO              " & vbNewLine _
                                         & " ,TBL.SYS_ENT_USER                         AS SYS_ENT_USER             " & vbNewLine _
                                         & " ,TBL.SYS_ENT_DATE                         AS SYS_ENT_DATE             " & vbNewLine _
                                         & " ,CUST.OYA_SEIQTO_CD                       AS OYA_SEIQTO_CD            " & vbNewLine _
                                         & " ,CUST.NIYAKU_SEIQTO_CD                    AS NIYAKU_SEIQTO_CD         " & vbNewLine _
                                         & " ,CUST.CUST_NM_L                                                       " & vbNewLine _
                                         & " ,CUST.CUST_NM_M                                                       " & vbNewLine _
                                         & " ,CUST.CUST_NM_S                                                       " & vbNewLine _
                                         & " ,CUST.CUST_NM_SS                                                      " & vbNewLine _
                                         & " ,TBL.SYS_ENT_PGID                         AS SYS_ENT_PGID             " & vbNewLine _
                                         & " FROM (                                                                " & vbNewLine

    ' ''' <summary>
    ' ''' SELECT文分割13
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Const SQL_SELECT13 As String = " LEFT OUTER JOIN $LM_MST$..S_USER AS SUSER                        " & vbNewLine _
    '                                     & " ON MAIN.NRS_BR_CD = SUSER.NRS_BR_CD                           " & vbNewLine _
    '                                     & " AND MAIN.SYS_ENT_USER = SUSER.USER_CD                         " & vbNewLine _
    '                                     & " AND SUSER.SYS_DEL_FLG = '0'                                   " & vbNewLine _
    '                                     & " WHERE MAIN.NRS_BR_CD = SEIQTO.NRS_BR_CD                       " & vbNewLine _
    '                                     & " AND (MAIN.NIYAKU_SEIQTO_CD = SEIQTO.SEIQTO_CD                   " & vbNewLine _
    '                                     & " OR MAIN.HOKAN_SEIQTO_CD = SEIQTO.SEIQTO_CD )                  " & vbNewLine _
    '                                     & " AND SEIQTO.SYS_DEL_FLG = '0'                                  " & vbNewLine




    ''' <summary>
    ''' 請求テーブル存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SEKY_CHECK As String = "SELECT COUNT(SEKY.JOB_NO) AS SQL_CNT" & vbNewLine _
                                       & "FROM $LM_TRN$..G_SEKY_TBL AS SEKY" & vbNewLine _
                                       & "WHERE SEKY.JOB_NO = @JOB_NO" & vbNewLine

#End Region

#Region "ORDERBY"

    ''' <summary>
    ''' ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY INV_DATE_TO DESC,CUST_CD_L,CUST_CD_M,CUST_CD_S,CUST_CD_SS                                        "

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
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG020DAC.SQL_HEADER)
        Me._StrSql.Append(LMG020DAC.SQL_COUNT_START)     'SQL構築(カウント用SelectCountStart句)
        Me.SETSQL()
        Me._StrSql.Append(LMG020DAC.SQL_COUNT_END)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SQL_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQL作成

        'SQL構築(データ抽出用Select句)

        'SQL作成
        'Me._StrSql.Append(LMG020DAC.SQL_START)     'SQL構築(用Select句)
        Me._StrSql.Append(LMG020DAC.SQL_HEADER)
        Me.SETSQL()
        Me._StrSql.Append(LMG020DAC.SQL_ORDER_BY)               'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        cmd.CommandTimeout = 6000
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG020DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        'map.Add("CLOSE_NM", "CLOSE_NM")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("JOB_NO", "JOB_NO")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        '2011/08/18 菱刈  締日区分をコメント化　スタート
        ' map.Add("CLOSE_KB", "CLOSE_KB")
        '2011/08/18 菱刈  締日区分をコメント化　エンド
        map.Add("OYA_SEIQTO_CD", "OYA_SEIQTO_CD")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG020OUT")

        Return ds

    End Function

#End Region

#Region "存在チェック"

    Private Function SelectSeiq(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG020DAC.SEKY_CHECK.Replace("@JOB_NO" _
                                                       , String.Concat( _
                                                       "'", Me._Row.Item("JOB_NO").ToString(), "'")))    'SQL構築(カウント用SelectCountStart句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        cmd.CommandTimeout = 6000

        MyBase.Logger.WriteSQLLog("LMG020DAC", "SelectSeiq", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SQL_CNT")))
        reader.Close()
        Return ds
    End Function


#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL5(ByVal count As Integer)


        '実行ユーザコード判定用
        Dim TANTO_USER_FLG As String = Me._Row.Item("TANTO_USER_FLG").ToString

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row


            '請求フラグ
            whereStr = .Item("SEKY_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKY_PRT.SEKY_FLG = @SEKY_FLG")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '請求期間ＴＯ
            whereStr = .Item("INV_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKY_PRT.INV_DATE_TO = @INV_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", whereStr, DBDataType.CHAR))
                End If
            End If

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKY_PRT.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
                End If
            End If

            'JOB番号
            whereStr = .Item("JOB_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKY_PRT.JOB_NO = @JOB_NO")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", String.Concat(whereStr), DBDataType.CHAR))
                End If

            End If

            'SBS高道）担当者作成分チェック時は、請求元在庫データ作成者を参照に変更対応
            '実行ユーザコード
            whereStr = .Item("TANTO_USER_FLG").ToString
            If String.IsNullOrEmpty(whereStr) = False Then
                ''私の作成分チェック時の場合
                If "1".Equals(whereStr) = True Then
                    whereStr = .Item("USER_CD").ToString()
                    If String.IsNullOrEmpty(whereStr) = False Then
                        Me._StrSql.Append(" AND SEKY_PRT.SYS_ENT_USER = @USER_CD")
                        Me._StrSql.Append(vbNewLine)
                        If count = 0 Then
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))

                        End If
                    End If
                End If
            End If

        End With
    End Sub
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL6(ByVal count As Integer)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row


            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '荷主コード（極小）
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '荷主コード（小）
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

        End With
    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL8(ByVal count As Integer)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '締め日区分
            whereStr = .Item("CLOSE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQTO.CLOSE_KB = @CLOSE_KB")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", whereStr, DBDataType.CHAR))
                End If
            End If

        End With
    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL9(ByVal count As Integer)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row


            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '荷主コード（極小）
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_SS = @CUST_CD_SS")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If

            '荷主コード（小）
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.CUST_CD_S = @CUST_CD_S")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr), DBDataType.CHAR))
                End If
            End If


        End With
    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL10(ByVal count As Integer)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_NM_L+CUST.CUST_NM_M+CUST.CUST_NM_S+CUST.CUST_NM_SS LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                End If
            End If
            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.OYA_SEIQTO_CD LIKE @SEIQTO_CD")
                Me._StrSql.Append(vbNewLine)
                If count = 0 Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                End If
            End If

        End With
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

    ''' <summary>
    ''' SQL設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SETSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim count5 As Integer = 0
        Dim count6 As Integer = 0
        Dim countT As Integer = 0
        Dim count8 As Integer = 0
        Dim count10 As Integer = 0

        Me._StrSql.Append(LMG020DAC.SQL_SELECT1)
        Me._StrSql.Append(LMG020DAC.SQL_SELECT2)
        Me._StrSql.Append(LMG020DAC.SQL_SELECT3)
        Me._StrSql.Append(LMG020DAC.SQL_SELECT4)
        Me._StrSql.Append(LMG020DAC.SQL_SELECT5)
        Call SetConditionMasterSQL5(count5)
        Call SetConditionMasterSQL9(count6)
        count5 = +1
        count6 = +1
        Me._StrSql.Append(LMG020DAC.SQL_SELECT6)
        Call SetConditionMasterSQL6(count6)
        Call SetConditionMasterSQL10(count10)
        count10 = +1
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT7)
        'SBS高道）作成分の取得先の変更に伴う修正
        'If "1".Equals(TANTO_FLG) = True Then
        '    Me._StrSql.Append(LMG020DAC.CUST_USER)
        '    Me._StrSql.Append(LMG020DAC.CUST_WHERE)
        '    Call SetConditionMasterSQLT(countT)
        '    countT = +1
        'End If
        Me._StrSql.Append(LMG020DAC.SQL_SELECT8)
        Call SetConditionMasterSQL8(count8)
        count8 = +1
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT9)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT11)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT2)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT12)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT4)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT5)
        'Call SetConditionMasterSQL5(count5)
        'Call SetConditionMasterSQL9(count6)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT6)
        'Call SetConditionMasterSQL6(count6)
        'Call SetConditionMasterSQL10(count10)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT7)
        'SBS高道）作成分の取得先の変更に伴う修正
        'If "1".Equals(TANTO_FLG) = True Then
        '    Me._StrSql.Append(LMG020DAC.CUST_USER)
        '    Me._StrSql.Append(LMG020DAC.CUST_WHERE)
        '    Call SetConditionMasterSQLT(countT)
        'End If
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT13)
        'Call SetConditionMasterSQL8(count8)
        'Me._StrSql.Append(LMG020DAC.SQL_SELECT10)

    End Sub


#End Region

#End Region

End Class
