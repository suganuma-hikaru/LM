' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI450  : 
'  作  成  者       :  [hojo]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMI450DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI450DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NAME

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = "LMI450IN"

        ''' <summary>
        ''' 都道府県テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ADDR As String = "LMI450ADDR"

        ''' <summary>
        ''' 商品テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS As String = "LMI450GOODS"

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EXCEL As String = "LMI450EXCEL"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI As String = "LMI450EDI"

    End Class

#Region "カラム名定義"
    Public Class COL_NAME
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const DEST_ADDR As String = "DEST_ADDR"
        Public Const DEST_ADDR_KEN As String = "DEST_ADDR_KEN"
        Public Const GOODS_CD_CUST As String = "GOODS_CD_CUST"
        Public Const PKG_NB As String = "PKG_NB"
    End Class

    Public Class INPUT_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const DEST_ADDR As String = COL_NAME.DEST_ADDR
        Public Const GOODS_CD_CUST As String = COL_NAME.GOODS_CD_CUST
    End Class

    Public Class ADDR_COLUMN_NM
        Public Const DEST_ADDR_KEN As String = COL_NAME.DEST_ADDR_KEN
    End Class

    Public Class GOODS_COLUMN_NM
        Public Const PKG_NB As String = COL_NAME.PKG_NB
    End Class

#End Region

#Region "関数名定義"
    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME
        ''' <summary>
        ''' 県データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectKenData As String = "SelectKenData"
        ''' <summary>
        ''' 県データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectGoodsData As String = "SelectGoodsData"
    End Class
#End Region

#Region "検索"

    ''' <summary>
    ''' 県データ検索SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KEN_DATA As String _
        = " SELECT KEN_N AS DEST_ADDR_KEN                   " & vbNewLine _
        & " FROM $LM_MST$..M_ZIP                            " & vbNewLine _
        & " WHERE                                           " & vbNewLine _
        & "  KEN_N + CITY_N + TOWN_N LIKE @DEST_ADDR        " & vbNewLine _
        & "  GROUP BY KEN_N                                 " & vbNewLine

    ''' <summary>
    ''' 商品データ検索SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODS_DATA As String _
        = " SELECT PKG_NB AS PKG_NB                         " & vbNewLine _
        & " FROM $LM_MST$..M_GOODS                          " & vbNewLine _
        & " WHERE                                           " & vbNewLine _
        & "      NRS_BR_CD      = @NRS_BR_CD                " & vbNewLine _
        & "  AND CUST_CD_L      = '00041'                   " & vbNewLine _
        & "  AND CUST_CD_M      = '00'                      " & vbNewLine _
        & "  AND (PKG_UT        = 'BX'                      " & vbNewLine _
        & "   OR  PKG_UT        = 'CS'                      " & vbNewLine _
        & "   OR  PKG_UT        = 'CT1')                    " & vbNewLine _
        & "  AND GOODS_CD_CUST  = @GOODS_CD_CUST            " & vbNewLine _
        & "  AND SYS_DEL_FLG    = '0'                       " & vbNewLine

#End Region


#End Region

#Region "Field"
    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow
    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder = Nothing

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = Nothing

#End Region

#Region "Method"

    '#Region "SQLメイン処理"

#Region "都道府県取得"
    ''' <summary>
    ''' 都道府県取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SelectKenData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder(LMI450DAC.SQL_SELECT_KEN_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString())

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQLパラメータ設定
        Me.SetSQLSelectAddrDataParameter(inRow)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If (reader.HasRows) Then

                    'DataReader→DataTableへの転記
                    Dim map As Hashtable = New Hashtable()

                    For Each column As String In {
                                                 ADDR_COLUMN_NM.DEST_ADDR_KEN
                                                }
                        map.Add(column, column)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.ADDR)

                    MyBase.SetResultCount(ds.Tables(TABLE_NAME.ADDR).Rows.Count)

                End If

            End Using

        End Using

        Return ds

    End Function

#Region "SQLパラメータ設定(都道府県取得)"

    ''' <summary>
    ''' パラメータ設定(都道府県取得)
    ''' </summary>
    ''' <param name="inRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectAddrDataParameter(ByVal inRow As DataRow)

        If (inRow IsNot Nothing) Then
            With inRow
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ADDR", String.Concat("%", .Item(INPUT_COLUMN_NM.DEST_ADDR).ToString(), "%"), DBDataType.NVARCHAR))
            End With
        End If

    End Sub

#End Region


#End Region

#Region "商品情報取得"
    ''' <summary>
    ''' 商品情報取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SelectGoodsData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder(LMI450DAC.SQL_SELECT_GOODS_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString())

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQLパラメータ設定
        Me.SetSQLSelectGoodsDataParameter(inRow)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If (reader.HasRows) Then

                    'DataReader→DataTableへの転記
                    Dim map As Hashtable = New Hashtable()

                    For Each column As String In {
                                                 GOODS_COLUMN_NM.PKG_NB
                                                }
                        map.Add(column, column)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.GOODS)

                    MyBase.SetResultCount(ds.Tables(TABLE_NAME.GOODS).Rows.Count)

                End If

            End Using

        End Using

        Return ds

    End Function

#Region "SQLパラメータ設定(商品情報取得)"
    ''' <summary>
    ''' パラメータ設定(商品情報取得)
    ''' </summary>
    ''' <param name="inRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectGoodsDataParameter(ByVal inRow As DataRow)

        If (inRow IsNot Nothing) Then
            With inRow
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item(INPUT_COLUMN_NM.GOODS_CD_CUST).ToString(), DBDataType.NVARCHAR))
            End With
        End If

    End Sub

#End Region


#End Region

#Region "スキーマ名称設定"
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


