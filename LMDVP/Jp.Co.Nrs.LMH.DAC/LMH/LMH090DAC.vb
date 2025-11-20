' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブシステム
'  プログラムID     :  LMH090DAC : 現品票印刷
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH090DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH090DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SELECT"

    Private Const SQL_SELECT_COUNT_LIST_DATA As String = _
          "SELECT                                                              " & vbNewLine _
        & "     COUNT(*)            AS SELECT_CNT                              " & vbNewLine

    Private Const SQL_SELECT_LIST_DATA As String = _
          "SELECT                                                              " & vbNewLine _
        & "     HED.ZFVYDENNO       AS OUTKA_FROM_ORD_NO                       " & vbNewLine _
        & "    ,HED.ZFVYDENYMD      AS INKA_DATE                               " & vbNewLine _
        & "    ,DTL.MATNR           AS CUST_GOODS_CD                           " & vbNewLine _
        & "    ,DTL.ZFVYMAKTX3      AS GOODS_NM                                " & vbNewLine _
        & "    ,DTL.CHARG           AS LOT_NO                                  " & vbNewLine _
        & "    ,MG.STD_IRIME_NB     AS STD_IRIME                               " & vbNewLine _
        & "    ,DTL.ZFVYSURYO                                                  " & vbNewLine _
        & "    ,DTL.ZFVYBRGEW                                                  " & vbNewLine _
        & "    ,HED.CRT_DATE                                                   " & vbNewLine _
        & "    ,HED.FILE_NAME                                                  " & vbNewLine _
        & "    ,HED.REC_NO                                                     " & vbNewLine _
        & "    ,HED.SYS_UPD_DATE    AS HED_UPD_DATE                            " & vbNewLine _
        & "    ,HED.SYS_UPD_TIME    AS HED_UPD_TIME                            " & vbNewLine _
        & "    ,DTL.GYO                                                        " & vbNewLine _
        & "    ,DTL.SYS_UPD_DATE    AS DTL_UPD_DATE                            " & vbNewLine _
        & "    ,DTL.SYS_UPD_TIME    AS DTL_UPD_TIME                            " & vbNewLine _
        & "--ADD 2019/12/17 009991 Start                                       " & vbNewLine _
        & "	,CASE WHEN (HIM.NB * HIM.IRIME) <> HIM.FREE_N01                    " & vbNewLine _
        & "	      THEN 'NG'                                                    " & vbNewLine _
        & "	      ELSE 'OK'                                                    " & vbNewLine _
        & "	 END          AS GENPINHYO_CHKFLG                                  " & vbNewLine _
        & "--ADD 2019/12/17 009991 END                                         " & vbNewLine

    Private Const SQL_SELECT_LIST_DATA_FROM As String = _
          "  FROM $LM_TRN$..H_INOUTKAEDI_HED_FJF  HED                          " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_INOUTKAEDI_DTL_FJF  DTL                     " & vbNewLine _
        & "    ON DTL.CRT_DATE = HED.CRT_DATE                                  " & vbNewLine _
        & "   AND DTL.FILE_NAME = HED.FILE_NAME                                " & vbNewLine _
        & "   AND DTL.REC_NO = HED.REC_NO                                      " & vbNewLine _
        & "  -- 商品マスタ                                                     " & vbNewLine _
        & "  LEFT JOIN                                                         " & vbNewLine _
        & "       (SELECT *                                                    " & vbNewLine _
        & "          FROM $LM_MST$..M_GOODS  MG1                               " & vbNewLine _
        & "         WHERE MG1.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
        & "           AND MG1.CUST_CD_L = @CUST_CD_L                           " & vbNewLine _
        & "           AND MG1.CUST_CD_M = @CUST_CD_M                           " & vbNewLine _
        & "           AND MG1.SYS_DEL_FLG = '0'                                " & vbNewLine _
        & "           AND NOT EXISTS                                           " & vbNewLine _
        & "               (SELECT COUNT(*)                                     " & vbNewLine _
        & "                  FROM $LM_MST$..M_GOODS  MG2                       " & vbNewLine _
        & "                 WHERE MG2.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
        & "                   AND MG2.CUST_CD_L = @CUST_CD_L                   " & vbNewLine _
        & "                   AND MG2.CUST_CD_M = @CUST_CD_M                   " & vbNewLine _
        & "                   AND MG2.GOODS_CD_CUST = MG1.GOODS_CD_CUST        " & vbNewLine _
        & "                   AND MG2.SYS_DEL_FLG = '0'                        " & vbNewLine _
        & "                HAVING COUNT(*) > 1                                 " & vbNewLine _
        & "               )                                                    " & vbNewLine _
        & "       )  MG                                                        " & vbNewLine _
        & "    ON MG.GOODS_CD_CUST = DTL.MATNR                                 " & vbNewLine _
        & "--ADD 2019/12/17 009991 Start                                       " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_INKAEDI_M HIM                                " & vbNewLine _
        & "   ON HIM.SYS_DEL_FLG    = '0'                                      " & vbNewLine _
        & "  AND HIM.EDI_CTL_NO     = HED.EDI_CTL_NO                           " & vbNewLine _
        & "  AND HIM.EDI_CTL_NO_CHU = DTL.GYO                                  " & vbNewLine _
        & "--ADD 2019/12/17 009991 END                                         " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "   AND HED.DEL_KB = '0'                                             " & vbNewLine _
        & "   AND HED.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
        & "   AND HED.INOUT_KB = '1'                                           " & vbNewLine _
        & "   AND DTL.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "   AND DTL.DEL_KB = '0'                                             " & vbNewLine

    Private Const SQL_SELECT_LIST_DATA_ORDER As String = _
          " ORDER BY HED.ZFVYDENNO                                             " & vbNewLine _
        & "         ,HED.ZFVYDENYMD                                            " & vbNewLine _
        & "         ,DTL.MATNR                                                 " & vbNewLine _
        & "         ,DTL.CHARG                                                 " & vbNewLine

#End Region 'Const

#End Region 'SELECT

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

#End Region 'Field

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 印刷対象データ件数取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("SELECT_LIST_DATA_IN").Rows(0)

        'SELECT文生成
        Me._StrSql = New StringBuilder(SQL_SELECT_COUNT_LIST_DATA)
        Me._StrSql.Append(SQL_SELECT_LIST_DATA_FROM)

        'オーダー番号
        If Me._Row.Item("OUTKA_FROM_ORD_NO").ToString <> "" Then
            Me._StrSql.Append("   AND HED.ZFVYDENNO = @OUTKA_FROM_ORD_NO                           " & vbNewLine)
        End If
        '入荷日
        If Me._Row.Item("INKA_DATE").ToString <> "" Then
            Me._StrSql.Append("   AND HED.ZFVYDENYMD = @INKA_DATE                                  " & vbNewLine)
        End If
        '商品コード
        If Me._Row.Item("CUST_GOODS_CD").ToString <> "" Then
            Me._StrSql.Append("   AND DTL.MATNR = @CUST_GOODS_CD                                   " & vbNewLine)
        End If
        'ロットNo.
        If Me._Row.Item("LOT_NO").ToString <> "" Then
            Me._StrSql.Append("   AND DTL.CHARG = @LOT_NO                                          " & vbNewLine)
        End If
        '標準入目
        If Me._Row.Item("STD_IRIME").ToString <> "" Then
            Me._StrSql.Append("   AND MG.STD_IRIME_NB = @STD_IRIME                                 " & vbNewLine)
        End If

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータリストの生成
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectListDataParam()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH090DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("SELECT_LIST_DATA_IN").Rows(0)

        'SELECT文生成
        Me._StrSql = New StringBuilder(SQL_SELECT_LIST_DATA)
        Me._StrSql.Append(SQL_SELECT_LIST_DATA_FROM)

        'オーダー番号
        If Me._Row.Item("OUTKA_FROM_ORD_NO").ToString <> "" Then
            Me._StrSql.Append("   AND HED.ZFVYDENNO = @OUTKA_FROM_ORD_NO                           " & vbNewLine)
        End If
        '入荷日
        If Me._Row.Item("INKA_DATE").ToString <> "" Then
            Me._StrSql.Append("   AND HED.ZFVYDENYMD = @INKA_DATE                                  " & vbNewLine)
        End If
        '商品コード
        If Me._Row.Item("CUST_GOODS_CD").ToString <> "" Then
            Me._StrSql.Append("   AND DTL.MATNR = @CUST_GOODS_CD                                   " & vbNewLine)
        End If
        'ロットNo.
        If Me._Row.Item("LOT_NO").ToString <> "" Then
            Me._StrSql.Append("   AND DTL.CHARG = @LOT_NO                                          " & vbNewLine)
        End If
        '標準入目
        If Me._Row.Item("STD_IRIME").ToString <> "" Then
            Me._StrSql.Append("   AND MG.STD_IRIME_NB = @STD_IRIME                                 " & vbNewLine)
        End If

        'ソート順
        Me._StrSql.Append(SQL_SELECT_LIST_DATA_ORDER)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータリストの生成
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectListDataParam()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH090DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("STD_IRIME", "STD_IRIME")
        map.Add("ZFVYSURYO", "ZFVYSURYO")
        map.Add("ZFVYBRGEW", "ZFVYBRGEW")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("HED_UPD_DATE", "HED_UPD_DATE")
        map.Add("HED_UPD_TIME", "HED_UPD_TIME")
        map.Add("GYO", "GYO")
        map.Add("DTL_UPD_DATE", "DTL_UPD_DATE")
        map.Add("DTL_UPD_TIME", "DTL_UPD_TIME")
        map.Add("GENPINHYO_CHKFLG", "GENPINHYO_CHKFLG")   'ADD 2019/12/18 09991

        '取得結果をデータセットへ設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SELECT_LIST_DATA_OUT")

        Return ds

    End Function

#End Region '検索処理

#Region "パラメータ設定"

    ''' <summary>
    ''' SelectListData用パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectListDataParam()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD"), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L"), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M"), DBDataType.VARCHAR))

        'オーダー番号
        If Me._Row("OUTKA_FROM_ORD_NO").ToString <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me._Row.Item("OUTKA_FROM_ORD_NO"), DBDataType.VARCHAR))
        End If
        '入荷日
        If Me._Row("INKA_DATE").ToString <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me._Row.Item("INKA_DATE"), DBDataType.CHAR))
        End If
        '商品コード
        If Me._Row("CUST_GOODS_CD").ToString <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me._Row.Item("CUST_GOODS_CD"), DBDataType.VARCHAR))
        End If
        'ロットNo.
        If Me._Row("LOT_NO").ToString <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO"), DBDataType.VARCHAR))
        End If
        '標準入目
        If Me._Row("STD_IRIME").ToString <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME", Me._Row.Item("STD_IRIME"), DBDataType.NUMERIC))
        End If

    End Sub

#End Region 'パラメータ設定

#Region "ユーティリティ"

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

#End Region 'ユーティリティ

#End Region 'Method

End Class
