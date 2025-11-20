' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ370DAC : 温度管理アラートチェック
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMZ370DACクラス
''' </summary>
Public Class LMZ370DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' 温度管理アラートチェック対象レコード取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODS_AND_DETAILS As String = "" _
            & "SELECT                                                                                                  " & vbNewLine _
            & "      M_GOODS.GOODS_CD_NRS                                                                              " & vbNewLine _
            & "    , M_GOODS.GOODS_CD_CUST                                                                             " & vbNewLine _
            & "    , M_GOODS.GOODS_NM_1 AS GOODS_NM                                                                    " & vbNewLine _
            & "FROM                                                                                                    " & vbNewLine _
            & "    $LM_MST$..M_GOODS                                                                                   " & vbNewLine _
            & "LEFT JOIN                                                                                               " & vbNewLine _
            & "    $LM_MST$..M_GOODS_DETAILS                                                                           " & vbNewLine _
            & "        ON  M_GOODS_DETAILS.NRS_BR_CD = M_GOODS.NRS_BR_CD                                               " & vbNewLine _
            & "        AND M_GOODS_DETAILS.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                                         " & vbNewLine _
            & "        AND M_GOODS_DETAILS.SUB_KB = '81'                                                               " & vbNewLine _
            & "        AND M_GOODS_DETAILS.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
            & "WHERE                                                                                                   " & vbNewLine _
            & "    M_GOODS.NRS_BR_CD = @NRS_BR_CD                                                                      " & vbNewLine _
            & "AND M_GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                                                                " & vbNewLine _
            & "AND M_GOODS.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "AND ISNULL(M_GOODS_DETAILS.SET_NAIYO, '0') = '1'    -- 温度管理アラートチェックあり                     " & vbNewLine _
            & "AND M_GOODS.ONDO_KB = '02'                          -- 定温                                             " & vbNewLine _
            & "-- 保管温度管理開始月日, 保管温度管理終了月日 双方 数値                                                 " & vbNewLine _
            & "AND ISNUMERIC(M_GOODS.ONDO_STR_DATE) = '1' AND ISNUMERIC(M_GOODS.ONDO_END_DATE) = '1'                   " & vbNewLine _
            & "AND(    -- 保管温度管理開始月日 <= 保管温度管理終了月日                                                 " & vbNewLine _
            & "       (    M_GOODS.ONDO_STR_DATE <= M_GOODS.ONDO_END_DATE                                              " & vbNewLine _
            & "            -- 保管温度管理開始月日 <= 入出荷月日 <= 保管温度管理終了月日                               " & vbNewLine _
            & "        AND SUBSTRING(@INOUTKA_DATE, 5, 4) BETWEEN M_GOODS.ONDO_STR_DATE AND M_GOODS.ONDO_END_DATE)     " & vbNewLine _
            & "    OR (-- 保管温度管理開始月日 >  保管温度管理終了月日                                                 " & vbNewLine _
            & "            M_GOODS.ONDO_STR_DATE >  M_GOODS.ONDO_END_DATE                                              " & vbNewLine _
            & "            -- 保管温度管理終了月日 <= 入出荷月日 <= 12月31日             または                        " & vbNewLine _
            & "            -- 01月01日             <= 入出荷月日 <= 保管温度管理開始月日                               " & vbNewLine _
            & "        AND(    SUBSTRING(@INOUTKA_DATE, 5, 4) BETWEEN M_GOODS.ONDO_STR_DATE AND '1231'                 " & vbNewLine _
            & "            OR  SUBSTRING(@INOUTKA_DATE, 5, 4) BETWEEN '0101'                AND M_GOODS.ONDO_END_DATE) " & vbNewLine _
            & "        )                                                                                               " & vbNewLine _
            & "    )                                                                                                   " & vbNewLine _
            & ""

#End Region ' "SQL"

#End Region ' "Const"

#Region "Method"

#Region "共通"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">変換元SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>変換後SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region '共通

#Region "検索"

    ''' <summary>
    ''' 温度管理アラートチェック対象レコード取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectGoodsAndDetails(ByVal ds As DataSet) As DataSet

        'テーブル名
        Const IN_TBL_NM As String = "LMZ370IN"
        Const OUT_TBL_NM As String = "LMZ370OUT"

        ' DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(IN_TBL_NM).Rows(0)

        ' SQLの編集
        Dim sql As String = Me.SetSchemaNm(LMZ370DAC.SQL_SELECT_GOODS_AND_DETAILS, inRow("NRS_BR_CD").ToString)

        ' SelectCommandの作成
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'SQLパラメータを設定
            Dim sqlParamList As List(Of SqlParameter) = New List(Of SqlParameter)
            With sqlParamList
                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", inRow("GOODS_CD_NRS").ToString, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@INOUTKA_DATE", inRow("INOUTKA_DATE").ToString, DBDataType.CHAR))
            End With
            cmd.Parameters.AddRange(sqlParamList.ToArray)

            'ログ出力
            MyBase.Logger.WriteSQLLog("LMZ370DAC", "SelectGoodsAndDetails", cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    ' 取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(OUT_TBL_NM).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    ' DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, OUT_TBL_NM)
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region ' "検索"

#End Region ' "Method"

End Class
