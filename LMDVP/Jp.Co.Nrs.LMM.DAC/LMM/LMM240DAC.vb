' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM240DAC : 帳票パターンマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM240DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM240DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    Private Const STANDARD_FLG As String = "01"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(RPT.PTN_ID)      AS SELECT_CNT   " & vbNewLine



    ''' <summary>
    ''' 帳票パターンマスタデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                  " & vbNewLine _
                                            & "       RPT.NRS_BR_CD                AS NRS_BR_CD         " & vbNewLine _
                                            & "      ,NRSBR.NRS_BR_NM              AS NRS_BR_NM         " & vbNewLine _
                                            & "      ,RPT.PTN_ID                   AS PTN_ID            " & vbNewLine _
                                            & "      ,KBN1.KBN_NM1                 AS PTN_ID_NM         " & vbNewLine _
                                            & "      ,RPT.PTN_CD                   AS PTN_CD            " & vbNewLine _
                                            & "      ,RPT.PTN_NM                   AS PTN_NM            " & vbNewLine _
                                            & "      ,RPT.RPT_ID                   AS RPT_ID            " & vbNewLine _
                                            & "      ,KBN2.KBN_CD                  AS RPT_NM            " & vbNewLine _
                                            & "      ,RPT.STANDARD_FLAG            AS STANDARD_FLAG     " & vbNewLine _
                                            & "      ,KBN3.KBN_NM2                 AS STANDARD_FLAG_NM  " & vbNewLine _
                                            & "      ,RPT.COPIES_NB1               AS COPIES_NB1        " & vbNewLine _
                                            & "      ,RPT.PTN_CD2                  AS PTN_CD2           " & vbNewLine _
                                            & "      ,RPT2.PTN_NM                  AS PTN_NM2           " & vbNewLine _
                                            & "      ,RPT.COPIES_NB2               AS COPIES_NB2        " & vbNewLine _
                                            & "      ,RPT.RPTOUT_KB                AS RPTOUT_KB         " & vbNewLine _
                                            & "      ,RPT.OUTPUT_KB                AS OUTPUT_KB         " & vbNewLine _
                                            & "      ,KBN4.KBN_NM1                 AS RPTOUT_KB_NM      " & vbNewLine _
                                            & "      ,RPT.PRINTER_NM               AS PRINTER_NM        " & vbNewLine _
                                            & "      ,RPT.JOB_ID                   AS JOB_ID            " & vbNewLine _
                                            & "      ,RPT.HISTORY_FLAG             AS HISTORY_FLAG      " & vbNewLine _
                                            & "      ,RPT.REMARK                   AS REMARK            " & vbNewLine _
                                            & "      ,RPT.SYS_ENT_DATE             AS SYS_ENT_DATE      " & vbNewLine _
                                            & "      ,USER1.USER_NM                AS SYS_ENT_USER_NM   " & vbNewLine _
                                            & "      ,RPT.SYS_UPD_DATE             AS SYS_UPD_DATE      " & vbNewLine _
                                            & "      ,RPT.SYS_UPD_TIME             AS SYS_UPD_TIME      " & vbNewLine _
                                            & "      ,USER2.USER_NM                AS SYS_UPD_USER_NM   " & vbNewLine _
                                            & "      ,RPT.SYS_DEL_FLG              AS SYS_DEL_FLG       " & vbNewLine _
                                            & "      ,KBN5.KBN_NM1                 AS SYS_DEL_NM        " & vbNewLine
#End Region

#Region "FROM句"

    ''' <summary>
    ''' From句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_DATA As String = "FROM                                                           " & vbNewLine _
                                              & "                      $LM_MST$..M_RPT     AS RPT           " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR         " & vbNewLine _
                                              & "        ON RPT.NRS_BR_CD        = NRSBR.NRS_BR_CD          " & vbNewLine _
                                              & "       AND NRSBR.SYS_DEL_FLG    = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1          " & vbNewLine _
                                              & "        ON RPT.PTN_ID           = KBN1.KBN_CD              " & vbNewLine _
                                              & "       AND KBN1.KBN_GROUP_CD    = 'T007'                   " & vbNewLine _
                                              & "       AND KBN1.VALUE1          = '1.000'                  " & vbNewLine _
                                              & "       AND KBN1.SYS_DEL_FLG     = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2          " & vbNewLine _
                                              & "        ON RPT.RPT_ID           = KBN2.KBN_NM1             " & vbNewLine _
                                              & "       AND KBN2.KBN_GROUP_CD    = 'R010'                   " & vbNewLine _
                                              & "       AND KBN2.SYS_DEL_FLG     = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3          " & vbNewLine _
                                              & "        ON RPT.STANDARD_FLAG    = KBN3.KBN_CD              " & vbNewLine _
                                              & "       AND KBN3.KBN_GROUP_CD    = 'U009'                   " & vbNewLine _
                                              & "       AND KBN3.SYS_DEL_FLG     = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN  $LM_MST$..M_RPT    AS RPT2          " & vbNewLine _
                                              & "        ON RPT.NRS_BR_CD        = RPT2.NRS_BR_CD           " & vbNewLine _
                                              & "       AND RPT.PTN_ID           = RPT2.PTN_ID              " & vbNewLine _
                                              & "       AND RPT.PTN_CD2          = RPT2.PTN_CD              " & vbNewLine _
                                              & "       AND RPT2.SYS_DEL_FLG     = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN4          " & vbNewLine _
                                              & "        ON RPT.RPTOUT_KB        = KBN4.KBN_CD              " & vbNewLine _
                                              & "       AND KBN4.KBN_GROUP_CD    = 'T008'                   " & vbNewLine _
                                              & "       AND KBN4.SYS_DEL_FLG     = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1         " & vbNewLine _
                                              & "        ON RPT.SYS_ENT_USER     = USER1.USER_CD            " & vbNewLine _
                                              & "       AND USER1.SYS_DEL_FLG    = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2         " & vbNewLine _
                                              & "       ON   RPT.SYS_UPD_USER    = USER2.USER_CD            " & vbNewLine _
                                              & "       AND USER2.SYS_DEL_FLG    = '0'                      " & vbNewLine _
                                              & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN5          " & vbNewLine _
                                              & "        ON RPT.SYS_DEL_FLG      = KBN5.KBN_CD              " & vbNewLine _
                                              & "       AND KBN5.KBN_GROUP_CD    = 'S051'                   " & vbNewLine _
                                              & "       AND KBN5.SYS_DEL_FLG     = '0'                      " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                       " & vbNewLine _
                                         & "      RPT.NRS_BR_CD            " & vbNewLine _
                                         & "     ,RPT.PTN_ID               " & vbNewLine _
                                         & "     ,RPT.PTN_CD               " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_RPT As String = "SELECT                                     " & vbNewLine _
                                            & "      COUNT(RPT.PTN_ID)   AS REC_CNT    " & vbNewLine _
                                            & " FROM $LM_MST$..M_RPT     AS RPT        " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD         " & vbNewLine _
                                            & "  AND PTN_ID       = @PTN_ID            " & vbNewLine _
                                            & "  AND PTN_CD       = @PTN_CD            " & vbNewLine


#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_RPT     " & vbNewLine _
                                       & "(                               " & vbNewLine _
                                       & "       NRS_BR_CD                " & vbNewLine _
                                       & "      ,PTN_ID                   " & vbNewLine _
                                       & "      ,PTN_CD                   " & vbNewLine _
                                       & "      ,PTN_NM                   " & vbNewLine _
                                       & "      ,RPT_ID                   " & vbNewLine _
                                       & "      ,STANDARD_FLAG            " & vbNewLine _
                                       & "      ,COPIES_NB1               " & vbNewLine _
                                       & "      ,PTN_CD2                  " & vbNewLine _
                                       & "      ,COPIES_NB2               " & vbNewLine _
                                       & "      ,RPTOUT_KB                " & vbNewLine _
                                       & "      ,OUTPUT_KB                " & vbNewLine _
                                       & "      ,PRINTER_NM               " & vbNewLine _
                                       & "      ,JOB_ID                   " & vbNewLine _
                                       & "      ,HISTORY_FLAG             " & vbNewLine _
                                       & "      ,REMARK                   " & vbNewLine _
                                       & "      ,SYS_ENT_DATE             " & vbNewLine _
                                       & "      ,SYS_ENT_TIME             " & vbNewLine _
                                       & "      ,SYS_ENT_PGID             " & vbNewLine _
                                       & "      ,SYS_ENT_USER             " & vbNewLine _
                                       & "      ,SYS_UPD_DATE             " & vbNewLine _
                                       & "      ,SYS_UPD_TIME             " & vbNewLine _
                                       & "      ,SYS_UPD_PGID             " & vbNewLine _
                                       & "      ,SYS_UPD_USER             " & vbNewLine _
                                       & "      ,SYS_DEL_FLG              " & vbNewLine _
                                       & "      ) VALUES (                " & vbNewLine _
                                       & "       @NRS_BR_CD               " & vbNewLine _
                                       & "      ,@PTN_ID                  " & vbNewLine _
                                       & "      ,@PTN_CD                  " & vbNewLine _
                                       & "      ,@PTN_NM                  " & vbNewLine _
                                       & "      ,@RPT_ID                  " & vbNewLine _
                                       & "      ,@STANDARD_FLAG           " & vbNewLine _
                                       & "      ,@COPIES_NB1              " & vbNewLine _
                                       & "      ,@PTN_CD2                 " & vbNewLine _
                                       & "      ,@COPIES_NB2              " & vbNewLine _
                                       & "      ,@RPTOUT_KB               " & vbNewLine _
                                       & "      ,@OUTPUT_KB               " & vbNewLine _
                                       & "      ,@PRINTER_NM              " & vbNewLine _
                                       & "      ,@JOB_ID                  " & vbNewLine _
                                       & "      ,@HISTORY_FLAG            " & vbNewLine _
                                       & "      ,@REMARK                  " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE            " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME            " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID            " & vbNewLine _
                                       & "      ,@SYS_ENT_USER            " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE            " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME            " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID            " & vbNewLine _
                                       & "      ,@SYS_UPD_USER            " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG             " & vbNewLine _
                                       & ")                               " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_RPT SET                       " & vbNewLine _
                                       & "        PTN_NM             = @PTN_NM             " & vbNewLine _
                                       & "       ,RPT_ID             = @RPT_ID             " & vbNewLine _
                                       & "       ,STANDARD_FLAG      = @STANDARD_FLAG      " & vbNewLine _
                                       & "       ,COPIES_NB1         = @COPIES_NB1         " & vbNewLine _
                                       & "       ,PTN_CD2            = @PTN_CD2            " & vbNewLine _
                                       & "       ,COPIES_NB2         = @COPIES_NB2         " & vbNewLine _
                                       & "       ,RPTOUT_KB          = @RPTOUT_KB          " & vbNewLine _
                                       & "       ,OUTPUT_KB          = @OUTPUT_KB          " & vbNewLine _
                                       & "       ,PRINTER_NM         = @PRINTER_NM         " & vbNewLine _
                                       & "       ,JOB_ID             = @JOB_ID             " & vbNewLine _
                                       & "       ,HISTORY_FLAG       = @HISTORY_FLAG       " & vbNewLine _
                                       & "       ,REMARK             = @REMARK             " & vbNewLine _
                                       & "       ,SYS_UPD_DATE       = @SYS_UPD_DATE       " & vbNewLine _
                                       & "       ,SYS_UPD_TIME       = @SYS_UPD_TIME       " & vbNewLine _
                                       & "       ,SYS_UPD_PGID       = @SYS_UPD_PGID       " & vbNewLine _
                                       & "       ,SYS_UPD_USER       = @SYS_UPD_USER       " & vbNewLine _
                                       & " WHERE                                           " & vbNewLine _
                                       & "         NRS_BR_CD         = @NRS_BR_CD          " & vbNewLine _
                                       & " AND     PTN_ID            = @PTN_ID             " & vbNewLine _
                                       & " AND     PTN_CD            = @PTN_CD             " & vbNewLine
    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_RPT SET                  " & vbNewLine _
                                         & "        SYS_UPD_DATE    = @SYS_UPD_DATE     " & vbNewLine _
                                         & "       ,SYS_UPD_TIME    = @SYS_UPD_TIME     " & vbNewLine _
                                         & "       ,SYS_UPD_PGID    = @SYS_UPD_PGID     " & vbNewLine _
                                         & "       ,SYS_UPD_USER    = @SYS_UPD_USER     " & vbNewLine _
                                         & "       ,SYS_DEL_FLG     = @SYS_DEL_FLG      " & vbNewLine _
                                         & " WHERE                                      " & vbNewLine _
                                         & "         NRS_BR_CD      = @NRS_BR_CD        " & vbNewLine _
                                         & " AND     PTN_ID         = @PTN_ID           " & vbNewLine _
                                         & " AND     PTN_CD         = @PTN_CD           " & vbNewLine
    ''' <summary>
    ''' 標準帳票フラグ更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_STANDARD_FLG As String = "UPDATE $LM_MST$..M_RPT SET          " & vbNewLine _
                                       & "        STANDARD_FLAG      = '00'                " & vbNewLine _
                                       & " WHERE                                           " & vbNewLine _
                                       & "         NRS_BR_CD         = @NRS_BR_CD          " & vbNewLine _
                                       & " AND     PTN_ID            = @PTN_ID             " & vbNewLine

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
    ''' 帳票パターンマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM240DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM240DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM240DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM240DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM240DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_ID_NM", "PTN_ID_NM")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("PTN_NM", "PTN_NM")
        map.Add("RPT_ID", "RPT_ID")
        map.Add("RPT_NM", "RPT_NM")
        map.Add("STANDARD_FLAG", "STANDARD_FLAG")
        map.Add("STANDARD_FLAG_NM", "STANDARD_FLAG_NM")
        map.Add("COPIES_NB1", "COPIES_NB1")
        map.Add("PTN_CD2", "PTN_CD2")
        map.Add("PTN_NM2", "PTN_NM2")
        map.Add("COPIES_NB2", "COPIES_NB2")
        map.Add("RPTOUT_KB", "RPTOUT_KB")
        map.Add("RPTOUT_KB_NM", "RPTOUT_KB_NM")
        map.Add("OUTPUT_KB", "OUTPUT_KB")
        map.Add("PRINTER_NM", "PRINTER_NM")
        map.Add("JOB_ID", "JOB_ID")
        map.Add("HISTORY_FLAG", "HISTORY_FLAG")
        map.Add("REMARK", "REMARK")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM240OUT")

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
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (RPT.SYS_DEL_FLG = @SYS_DEL_FLG  OR RPT.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (RPT.NRS_BR_CD = @NRS_BR_CD OR RPT.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("PTN_ID").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" RPT.PTN_ID = @PTN_ID")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("STANDARD_FLAG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" RPT.STANDARD_FLAG = @STANDARD_FLAG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STANDARD_FLAG", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("PTN_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" RPT.PTN_CD LIKE @PTN_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("PTN_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" RPT.PTN_NM LIKE @PTN_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("RPT_ID").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" RPT.RPT_ID = @RPT_ID")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_ID", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("RPTOUT_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" RPT.RPTOUT_KB = @RPTOUT_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPTOUT_KB", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 帳票パターンマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM240DAC.SQL_EXIT_RPT)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "SelectRptM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM240DAC.SQL_EXIT_RPT, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "CheckExistRptM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '検証結果(メモ)№74対応(2011.09.08)
        '【更新区分の判定】INTableの標準帳票フラグが'01'(標準帳票)ならば以下の処理を行う
        If Me._Row.Item("STANDARD_FLAG").ToString = STANDARD_FLG Then
            '更新対象データと同一営業所・帳票種類IDに該当する標準帳票フラグを、全て'00'(標準帳票以外)に更新する
            Call Me.UpdateStandardFlgOff(ds)
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM240DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "InsertRptM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '検証結果(メモ)№74対応(2011.09.08)
        '【更新区分の判定】INTableの標準帳票フラグが'01'(標準帳票)ならば以下の処理を行う
        If Me._Row.Item("STANDARD_FLAG").ToString = STANDARD_FLG Then
            '更新対象データと同一営業所・帳票種類IDに該当する標準帳票フラグを、全て'00'(標準帳票以外)に更新する
            Call Me.UpdateStandardFlgOff(ds)
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM240DAC.SQL_UPDATE _
                                                                                     , LMM240DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "UpdateRptM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteRptM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM240DAC.SQL_DELETE _
                                                                                     , LMM240DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "DeleteRptM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    '検証結果(メモ)№74対応(2011.09.08)
    ''' <summary>
    ''' 標準帳票フラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>対象データと同一の営業所・帳票種類IDに紐づく標準帳票を全て0(標準帳票以外)に更新SQLの構築・発行</remarks>
    Private Function UpdateStandardFlgOff(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM240IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM240DAC.SQL_UPDATE_STANDARD_FLG _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM240DAC", "UpdateStandardFlgOff", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(帳票パターンマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", .Item("PTN_ID").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", .Item("PTN_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", .Item("PTN_ID").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", .Item("PTN_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_NM", .Item("PTN_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_ID", .Item("RPT_ID").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STANDARD_FLAG", .Item("STANDARD_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COPIES_NB1", .Item("COPIES_NB1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD2", .Item("PTN_CD2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COPIES_NB2", .Item("COPIES_NB2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPTOUT_KB", .Item("RPTOUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTPUT_KB", .Item("OUTPUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINTER_NM", .Item("PRINTER_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_ID", .Item("JOB_ID").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HISTORY_FLAG", .Item("HISTORY_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", Me._Row.Item("PTN_ID").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", Me._Row.Item("PTN_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
