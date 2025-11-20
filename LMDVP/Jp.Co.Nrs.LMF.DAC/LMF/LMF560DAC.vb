' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF560DAC : 送り状一覧明細
'  作  成  者       :  YAMANAKA
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF560DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF560DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                   " & vbNewLine _
                                            & "	  UNSO_L.NRS_BR_CD                                  AS NRS_BR_CD  " & vbNewLine _
                                            & " , 'A5'                                              AS PTN_ID     " & vbNewLine _
                                            & " , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                " & vbNewLine _
                                            & "	 	   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                " & vbNewLine _
                                            & "	   	   ELSE MR3.PTN_CD END                          AS PTN_CD     " & vbNewLine _
                                            & " , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                            & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                " & vbNewLine _
                                            & "		   ELSE MR3.RPT_ID END                          AS RPT_ID     " & vbNewLine


#End Region

#Region "SELECT句"

    Private Const SQL_SELECT_DATA As String = " SELECT                                               " & vbNewLine _
                                            & "	   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID  " & vbNewLine _
                                            & "		    WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
                                            & "         ELSE MR3.RPT_ID END AS RPT_ID                " & vbNewLine _
                                            & "  , UNSO_L.UNSO_CD           AS UNSO_CD               " & vbNewLine _
                                            & "  , UNSO_L.UNSO_BR_CD        AS UNSO_BR_CD            " & vbNewLine _
                                            & "  , UNSO_L.ARR_PLAN_DATE     AS ARR_PLAN_DATE         " & vbNewLine _
                                            & "  , UNSO_L.CUST_CD_L         AS CUST_CD_L             " & vbNewLine _
                                            & "  , UNSO_L.CUST_CD_M         AS CUST_CD_M             " & vbNewLine _
                                            & "  , M_CUST.CUST_NM_L         AS CUST_NM_L             " & vbNewLine _
                                            & "  , M_CUST.CUST_NM_M         AS CUST_NM_M             " & vbNewLine _
                                            & "  , M_DEST.DEST_NM           AS DEST_NM               " & vbNewLine _
                                            & "  , UNSO_L.REMARK            AS REMARK_L              " & vbNewLine _
                                            & "  , M_DEST.ZIP               AS ZIP                   " & vbNewLine _
                                            & "  , M_DEST.AD_1              AS AD_1                  " & vbNewLine _
                                            & "  , M_DEST.AD_2              AS AD_2                  " & vbNewLine _
                                            & "  , UNSO_M.GOODS_CD_NRS      AS GOODS_CD_NRS          " & vbNewLine _
                                            & "  , UNSO_M.GOODS_NM          AS GOODS_NM              " & vbNewLine _
                                            & "  , UNSO_M.UNSO_TTL_NB       AS UNSO_TTL_NB           " & vbNewLine _
                                            & "  , UNSO_M.IRIME             AS IRIME                 " & vbNewLine _
                                            & "  , UNSO_M.IRIME_UT          AS IRIME_UT              " & vbNewLine _
                                            & "  , UNSO_M.BETU_WT           AS BETU_WT               " & vbNewLine _
                                            & "  , UNSO_M.REMARK            AS REMARK_M              " & vbNewLine _
                                            & "  , M_UNSOCO.UNSOCO_NM       AS UNSOCO_NM             " & vbNewLine _
                                            & "  , M_UNSOCO.UNSOCO_BR_NM    AS UNSOCO_BR_NM          " & vbNewLine _
                                            & "  , Z_KBN.KBN_NM1            AS TANI                  " & vbNewLine _
                                            & "--  , M_UNSOCO.UNSOCO_CD       AS UNSOCO_CD             " & vbNewLine _
                                            & "--  , M_UNSOCO.UNSOCO_BR_CD    AS UNSOCO_BR_CD          " & vbNewLine

#End Region

#Region "FROM句"
    Private Const SQL_FROM_DATA As String = " FROM                                                   " & vbNewLine _
                                          & "  $LM_TRN$..F_UNSO_L UNSO_L                             " & vbNewLine _
                                          & "  --運送Ｍ                                              " & vbNewLine _
                                          & "  LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M                   " & vbNewLine _
                                          & "         ON UNSO_M.NRS_BR_CD = UNSO_L.NRS_BR_CD         " & vbNewLine _
                                          & "        AND UNSO_M.UNSO_NO_L = UNSO_L.UNSO_NO_L         " & vbNewLine _
                                          & "        AND UNSO_M.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                          & "  --荷主マスタ                                          " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST                            " & vbNewLine _
                                          & "         ON M_CUST.NRS_BR_CD = UNSO_L.NRS_BR_CD         " & vbNewLine _
                                          & "        AND M_CUST.CUST_CD_L = UNSO_L.CUST_CD_L         " & vbNewLine _
                                          & "        AND M_CUST.CUST_CD_M = UNSO_L.CUST_CD_M         " & vbNewLine _
                                          & "        AND M_CUST.CUST_CD_S = '00'                     " & vbNewLine _
                                          & "        AND M_CUST.CUST_CD_SS = '00'                    " & vbNewLine _
                                          & "  --届先マスタ                                          " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_DEST                            " & vbNewLine _
                                          & "         ON M_DEST.NRS_BR_CD = UNSO_L.NRS_BR_CD         " & vbNewLine _
                                          & "        AND M_DEST.DEST_CD   = UNSO_L.DEST_CD           " & vbNewLine _
                                          & "        AND M_DEST.CUST_CD_L = UNSO_L.CUST_CD_L         " & vbNewLine _
                                          & "  --運送会社マスタ                                      " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_UNSOCO                          " & vbNewLine _
                                          & "         ON M_UNSOCO.NRS_BR_CD = UNSO_L.NRS_BR_CD       " & vbNewLine _
                                          & "        AND M_UNSOCO.UNSOCO_CD = UNSO_L.UNSO_CD         " & vbNewLine _
                                          & "        AND M_UNSOCO.UNSOCO_BR_CD = UNSO_L.UNSO_BR_CD   " & vbNewLine _
                                          & "  --商品マスタ                                          " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_GOODS                           " & vbNewLine _
                                          & "         ON M_GOODS.NRS_BR_CD = UNSO_M.NRS_BR_CD        " & vbNewLine _
                                          & "        AND M_GOODS.GOODS_CD_NRS = UNSO_M.GOODS_CD_NRS  " & vbNewLine _
                                          & "  --区分マスタ                                          " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..Z_KBN                             " & vbNewLine _
                                          & "         ON Z_KBN.KBN_CD = UNSO_L.NB_UT                 " & vbNewLine _
                                          & "        AND Z_KBN.KBN_GROUP_CD = 'K002'                 " & vbNewLine _
                                          & "  --運送Lでの荷主帳票パターン取得                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                   " & vbNewLine _
                                          & "         ON MCR1.NRS_BR_CD = UNSO_L.NRS_BR_CD           " & vbNewLine _
                                          & "	     AND MCR1.CUST_CD_L = UNSO_L.CUST_CD_L           " & vbNewLine _
                                          & "	     AND MCR1.CUST_CD_M = UNSO_L.CUST_CD_M           " & vbNewLine _
                                          & "	     AND MCR1.CUST_CD_S = '00'                       " & vbNewLine _
                                          & "	     AND MCR1.PTN_ID   = 'A5'                        " & vbNewLine _
                                          & "  --帳票パターン取得                                    " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR1                         " & vbNewLine _
                                          & "	      ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD              " & vbNewLine _
                                          & "	     AND MR1.PTN_ID    = MCR1.PTN_ID                 " & vbNewLine _
                                          & "	     AND MR1.PTN_CD    = MCR1.PTN_CD                 " & vbNewLine _
                                          & "        AND MR1.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                          & "  --商品Mの荷主での荷主帳票パターン取得                 " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                   " & vbNewLine _
                                          & "	      ON MCR2.NRS_BR_CD = M_GOODS.NRS_BR_CD          " & vbNewLine _
                                          & "	     AND MCR2.CUST_CD_L = M_GOODS.CUST_CD_L          " & vbNewLine _
                                          & "	     AND MCR2.CUST_CD_M = M_GOODS.CUST_CD_M          " & vbNewLine _
                                          & "	     AND MCR2.CUST_CD_S = M_GOODS.CUST_CD_S          " & vbNewLine _
                                          & "	     AND MCR2.PTN_ID  = 'A5'                         " & vbNewLine _
                                          & "  --帳票パターン取得                                    " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR2                         " & vbNewLine _
                                          & "	      ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD              " & vbNewLine _
                                          & "	     AND MR2.PTN_ID    = MCR2.PTN_ID                 " & vbNewLine _
                                          & "	     AND MR2.PTN_CD    = MCR2.PTN_CD                 " & vbNewLine _
                                          & "        AND MR2.SYS_DEL_FLG = '0'                       " & vbNewLine _
                                          & "  --存在しない場合の帳票パターン取得                    " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_RPT MR3                         " & vbNewLine _
                                          & "	      ON MR3.NRS_BR_CD     = UNSO_L.NRS_BR_CD        " & vbNewLine _
                                          & "	     AND MR3.PTN_ID        = 'A5'                    " & vbNewLine _
                                          & "	     AND MR3.STANDARD_FLAG = '01'                    " & vbNewLine _
                                          & "        AND MR3.SYS_DEL_FLG = '0'                       " & vbNewLine


#End Region

#Region "ORDER BY句"

    ''' <summary>
    ''' 検索対象データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_DATA As String = "  ORDER BY                                         " & vbNewLine _
                                           & "    UNSO_L.CUST_CD_L                               " & vbNewLine _
                                           & "  , UNSO_L.CUST_CD_M                               " & vbNewLine _
                                           & "  , UNSO_L.ARR_PLAN_DATE                           " & vbNewLine _
                                           & "  , UNSO_L.UNSO_CD                                 " & vbNewLine _
                                           & "  , UNSO_L.UNSO_BR_CD                              " & vbNewLine _
                                           & "  , M_DEST.DEST_NM                                 " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMF560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF560DAC.SQL_SELECT_MPrt)      'SQL構築(Select句)
        Me._StrSql.Append(LMF560DAC.SQL_FROM_DATA)        'SQL構築(From句)
        Call SetSQLWhereDATA(inTbl.Rows(0))               '条件設定(Where句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF560DAC", "SelectMPrt", cmd)

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
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF560DAC.SQL_SELECT_DATA)          'SQL構築 SELECT句
        Me._StrSql.Append(LMF560DAC.SQL_FROM_DATA)            'SQL構築 FROM句
        Call SetSQLWhereDATA(inTbl.Rows(0))                   '条件設定
        Me._StrSql.Append(LMF560DAC.SQL_ORDER_DATA)           'SQL構築 ORDER BY句

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF560DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("TANI", "TANI")
        'map.Add("UNSOCO_CD", "UNSOCO_CD")
        'map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF560OUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereDATA(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("UNSO_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            End If

            '運行番号
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO_L.UNSO_NO_L = @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

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
