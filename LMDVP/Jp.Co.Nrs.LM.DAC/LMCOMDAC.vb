' ==========================================================================
'  システム名     : LM
'  サブシステム名 : LMCOM    : システム共通
'  プログラムID   : LMCOMDOC : システム共通データ処理
'  作  成  者     : 大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMCOMDACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMCOMDAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "M_GOODS_DETAILS"

    ''' <summary>
    ''' 商品明細マスタデータ取得SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GOODS_DETAILS As String = " SELECT NRS_BR_CD             AS NRS_BR_CD        " & vbNewLine _
                                              & "      , GOODS_CD_NRS          AS GOODS_CD_NRS     " & vbNewLine _
                                              & "      , MIN(GOODS_CD_NRS_EDA) AS GOODS_CD_NRS_EDA " & vbNewLine _
                                              & "      , SUB_KB                AS SUB_KB           " & vbNewLine _
                                              & "      , SET_NAIYO             AS SET_NAIYO        " & vbNewLine _
                                              & "      , REMARK                AS REMARK           " & vbNewLine _
                                              & "   FROM LM_MST..M_GOODS_DETAILS                   " & vbNewLine _
                                              & "  WHERE NRS_BR_CD    = @NRS_BR_CD                 " & vbNewLine _
                                              & "    AND GOODS_CD_NRS = @GOODS_CD_NRS              " & vbNewLine _
                                              & "    AND SUB_KB       = @SUB_KB                    " & vbNewLine _
                                              & "  GROUP BY                                        " & vbNewLine _
                                              & "        NRS_BR_CD                                 " & vbNewLine _
                                              & "      , GOODS_CD_NRS                              " & vbNewLine _
                                              & "      , SUB_KB                                    " & vbNewLine _
                                              & "      , SET_NAIYO                                 " & vbNewLine _
                                              & "      , REMARK                                    " & vbNewLine



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
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row2 As Data.DataRow

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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品明細マスタデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品明細マスタデータ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectGoodsDetailsData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("GOODS_DETAILS_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMCOMDAC.SQL_GOODS_DETAILS)      'SQL構築(データ抽出用Select句)
        Call Me.SetConditionMGoodsDetailsSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMCOMDAC", "SelectGoodsDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_NRS_EDA", "GOODS_CD_NRS_EDA")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("REMARK", "REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GOODS_DETAILS_OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMGoodsDetailsSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_KB", Me._Row.Item("SUB_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

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

End Class
