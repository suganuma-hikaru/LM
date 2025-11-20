' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ380    : 物産アニマルヘルス在庫選択
'  作  成  者       :  HORI
' ==========================================================================

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ380DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ380DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = "" _
        & "SELECT                                   " & vbNewLine _
        & "   COUNT(ZAI.GOODS_CD_NRS) AS SELECT_CNT " & vbNewLine _
        & ""

    ''' <summary>
    ''' CUST_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "" _
        & "SELECT                               " & vbNewLine _
        & "   ZAI.NRS_BR_CD                     " & vbNewLine _
        & "  ,ZAI.ZAI_REC_NO                    " & vbNewLine _
        & "  ,GOD.GOODS_CD_CUST AS GOODS_CD     " & vbNewLine _
        & "  ,ZAI.GOODS_CD_NRS                  " & vbNewLine _
        & "  ,GOD.GOODS_NM_1 AS GOODS_NM        " & vbNewLine _
        & "  ,ZAI.LOT_NO                        " & vbNewLine _
        & "  ,ZAI.GOODS_COND_KB_3 AS GOODS_RANK " & vbNewLine _
        & "  ,CND.JOTAI_NM AS GOODS_RANK_NM     " & vbNewLine _
        & "  ,ZAI.ALLOC_CAN_NB AS NB            " & vbNewLine _
        & "  ,ZAI.LT_DATE                       " & vbNewLine _
        & ""

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "" _
        & "FROM                                             " & vbNewLine _
        & "  $LM_TRN$..D_ZAI_TRS AS ZAI                     " & vbNewLine _
        & "  LEFT JOIN                                      " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB1                       " & vbNewLine _
        & "    ON                                           " & vbNewLine _
        & "          KB1.KBN_GROUP_CD = 'B047'              " & vbNewLine _
        & "      AND KB1.KBN_NM1 = @WH_TYPE                 " & vbNewLine _
        & "  LEFT JOIN                                      " & vbNewLine _
        & "    $LM_MST$..M_GOODS AS GOD                     " & vbNewLine _
        & "    ON                                           " & vbNewLine _
        & "          GOD.NRS_BR_CD = ZAI.NRS_BR_CD          " & vbNewLine _
        & "      AND GOD.GOODS_CD_NRS = ZAI.GOODS_CD_NRS    " & vbNewLine _
        & "  LEFT JOIN                                      " & vbNewLine _
        & "    $LM_MST$..M_CUSTCOND AS CND                  " & vbNewLine _
        & "    ON                                           " & vbNewLine _
        & "          CND.NRS_BR_CD = ZAI.NRS_BR_CD          " & vbNewLine _
        & "      AND CND.CUST_CD_L = KB1.KBN_NM2            " & vbNewLine _
        & "      AND CND.JOTAI_CD = ZAI.GOODS_COND_KB_3     " & vbNewLine _
        & "WHERE                                            " & vbNewLine _
        & "      ZAI.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
        & "  AND ZAI.CUST_CD_L = KB1.KBN_NM2                " & vbNewLine _
        & "  AND ZAI.CUST_CD_M = KB1.KBN_NM3                " & vbNewLine _
        & "  AND ZAI.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & ""

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "" _
        & "ORDER BY                 " & vbNewLine _
        & "   GOD.GOODS_CD_CUST     " & vbNewLine _
        & "  ,ZAI.LOT_NO            " & vbNewLine _
        & "  ,ZAI.LT_DATE           " & vbNewLine _
        & "  ,ZAI.GOODS_COND_KB_3   " & vbNewLine _
        & ""

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
    ''' 件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ380IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ380DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMZ380DAC.SQL_FROM_DATA)
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ380DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ380IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ380DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMZ380DAC.SQL_FROM_DATA)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMZ380DAC.SQL_ORDER_BY)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ380DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("GOODS_RANK", "GOODS_RANK")
        map.Add("GOODS_RANK_NM", "GOODS_RANK_NM")
        map.Add("NB", "NB")
        map.Add("LT_DATE", "LT_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ380OUT")

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

            '固定条件
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TYPE", .Item("WH_TYPE").ToString(), DBDataType.CHAR))

            '商品CD
            whereStr = .Item("GOODS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND GOD.GOODS_CD_CUST LIKE @GOODS_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND GOD.GOODS_NM_1 LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'LOT
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZAI.LOT_NO LIKE @LOT_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品ランク
            whereStr = .Item("GOODS_RANK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND ZAI.GOODS_COND_KB_3 = @GOODS_RANK")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_RANK", whereStr, DBDataType.NVARCHAR))
            End If

        End With

    End Sub

#End Region

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

End Class
