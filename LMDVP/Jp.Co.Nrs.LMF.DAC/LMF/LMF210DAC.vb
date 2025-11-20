' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF210DAC : 運行情報一覧表示
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF210DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF210DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"


    ''' <summary>
    '''カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = "SELECT COUNT(WT.NRS_BR_CD) AS SELECT_CNT  FROM( " & vbNewLine

    ''' <summary>
    '''カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = "SELECT * FROM( " & vbNewLine


    ''' <summary>
    ''' F_UNCHIN_TRSデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                " & vbNewLine _
                                          & " UNSOLL.NRS_BR_CD                          AS NRS_BR_CD                                                                                                " & vbNewLine _
                                         & " ,UNSOLL.TRIP_NO                           AS TRIP_NO                                                                                                  " & vbNewLine _
                                         & " ,UNSOLL.TRIP_DATE                         AS TRIP_DATE                                                                                                " & vbNewLine _
                                         & " ,UNSOCO.UNSOCO_NM                         AS UNSOCO_NM                                                                                                " & vbNewLine _
                                         & " ,UNSOCO.UNSOCO_BR_NM                      AS UNSOCO_BR_NM                                                                                                " & vbNewLine _
                                         & " ,VCLE.CAR_NO                              AS CAR_NO                                                                                                   " & vbNewLine _
                                         & " ,KBN1.KBN_NM1                              AS JSHA_NM                                                                                                  " & vbNewLine _
                                         & " ,DRIVER.DRIVER_NM                          AS DRIVER_NM                                                                                                " & vbNewLine _
                                         & " ,ISNULL(SUM(UNSO.UNSO_WT),0)+ISNULL(SUM(UNSO1.UNSO_WT),0)+ISNULL(SUM(UNSO2.UNSO_WT),0)+ISNULL(SUM(UNSO3.UNSO_WT),0)  AS UNSO_WT                       " & vbNewLine _
                                         & " ,ISNULL(SUM(UNSO.UNSO_PKG_NB),0)+ISNULL(SUM(UNSO1.UNSO_PKG_NB),0)+ISNULL(SUM(UNSO2.UNSO_PKG_NB),0)+ISNULL(SUM(UNSO3.UNSO_PKG_NB),0)     AS UNSO_PKG_NB" & vbNewLine _
                                         & " ,VCLE.LOAD_WT                             AS LOAD_WT                                                                                                  " & vbNewLine _
                                         & " ,UNSOLL.UNSO_ONDO                         AS UNSO_ONDO                                                                                                " & vbNewLine _
                                         & " ,UNSOLL.PAY_UNCHIN                        AS PAY_UNCHIN                                                                                               " & vbNewLine _
                                         & " ,KBN2.KBN_NM1                             AS BIN                                                                                                      " & vbNewLine _
                                         & " ,UNSOLL.REMARK                            AS REMARK                                                                                                   " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                               " & vbNewLine _
                                          & "      $LM_TRN$..F_UNSO_LL AS UNSOLL                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L  AS UNSO                  " & vbNewLine _
                                          & "        ON UNSOLL.TRIP_NO=UNSO.TRIP_NO                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L  AS UNSO1                 " & vbNewLine _
                                          & "        ON UNSOLL.TRIP_NO=UNSO1.TRIP_NO_SYUKA                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L  AS UNSO2                 " & vbNewLine _
                                          & "        ON UNSOLL.TRIP_NO=UNSO2.TRIP_NO_TYUKEI                     " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_TRN$..F_UNSO_L  AS UNSO3                 " & vbNewLine _
                                          & "        ON UNSOLL.TRIP_NO=UNSO3.TRIP_NO_HAIKA                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_UNSOCO  AS UNSOCO                " & vbNewLine _
                                          & "        ON UNSOLL.NRS_BR_CD=UNSOCO.NRS_BR_CD                       " & vbNewLine _
                                          & "       AND UNSOLL.UNSOCO_CD=UNSOCO.UNSOCO_CD                       " & vbNewLine _
                                          & "       AND UNSOLL.UNSOCO_BR_CD=UNSOCO.UNSOCO_BR_CD                 " & vbNewLine _
                                          & "       AND UNSOCO.SYS_DEL_FLG='0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_VCLE    AS VCLE                  " & vbNewLine _
                                          & "        ON UNSOLL.CAR_KEY=VCLE.CAR_KEY                             " & vbNewLine _
                                          & "       AND VCLE.SYS_DEL_FLG= '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DRIVER  AS DRIVER                " & vbNewLine _
                                          & "        ON UNSOLL.DRIVER_CD=DRIVER.DRIVER_CD                       " & vbNewLine _
                                          & "       AND DRIVER.SYS_DEL_FLG='0'                                  " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                  " & vbNewLine _
                                          & "        ON UNSOLL.JSHA_KB=KBN1.KBN_CD                              " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD='J002'                                " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG='0'                                    " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                  " & vbNewLine _
                                          & "        ON UNSOLL.BIN_KB=KBN2.KBN_CD                               " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD='U001'                                " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG='0'                                    " & vbNewLine _
                                          & "" & vbNewLine






#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY_B As String = " GROUP BY                                                                                                                   " & vbNewLine _
                                         & "  VCLE.LOAD_WT  ) WT    " & vbNewLine

#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                                                                                                                   " & vbNewLine _
                                         & "  UNSOLL.NRS_BR_CD,UNSOLL.TRIP_NO,UNSOLL.TRIP_DATE,UNSOCO.UNSOCO_NM,VCLE.CAR_NO  " & vbNewLine _
                                           & " ,KBN1.KBN_NM1,DRIVER.DRIVER_NM,VCLE.LOAD_WT,UNSOLL.UNSO_ONDO,UNSOLL.PAY_UNCHIN,KBN2.KBN_NM1,UNSOLL.REMARK,VCLE.TRAILER_NO,UNSOCO.UNSOCO_BR_NM ) WT " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                               " & vbNewLine _
                                         & "    TRIP_NO,TRIP_DATE,UNSOCO_NM ,UNSOCO_BR_NM                  " & vbNewLine

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
    ''' 運賃マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF210DAC.SQL_SELECT_COUNT)      'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMF210DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF210DAC.SQL_FROM_DATA)         'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                    '条件設定
        Me._StrSql.Append(LMF210DAC.SQL_GROUP_BY)        'SQL構築(カウント用from句)
        Call Me.SetConditionWhereSQL()                     '条件設定


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF210DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF210DAC.SQL_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF210DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMF210DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMF210DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Call Me.SetConditionWhereSQL()                    '条件設定
        Me._StrSql.Append(LMF210DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF210DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("TRIP_DATE", "TRIP_DATE")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("CAR_NO", "CAR_NO")
        map.Add("JSHA_NM", "JSHA_NM")
        map.Add("DRIVER_NM", "DRIVER_NM")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("LOAD_WT", "LOAD_WT")
        map.Add("UNSO_ONDO", "UNSO_ONDO")
        map.Add("PAY_UNCHIN", "PAY_UNCHIN")
        map.Add("BIN", "BIN")
        map.Add("REMARK", "REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF210OUT")

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

            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.NRS_BR_CD = @NRS_BR_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("TRIP_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.TRIP_DATE >= @TRIP_DATE_FROM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TRIP_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.TRIP_DATE <= @TRIP_DATE_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("LOAD_WT_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOAD_WT_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("LOAD_WT_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOAD_WT_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("UNSO_ONDO_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.UNSO_ONDO >= @UNSO_ONDO_FROM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_FROM", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("UNSO_ONDO_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.UNSO_ONDO <= @UNSO_ONDO_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.TRIP_NO LIKE @TRIP_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If


            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOCO.UNSOCO_NM + '　' + UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_NM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If


            whereStr = .Item("CAR_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" VCLE.CAR_NO LIKE @CAR_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("JSHA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" VCLE.JSHA_KB  = @JSHA_KB  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JSHA_KB", whereStr, DBDataType.CHAR))
            End If



            whereStr = .Item("DRIVER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DRIVER.DRIVER_NM LIKE @DRIVER_NM  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.BIN_KB  = @BIN_KB  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" UNSOLL.REMARK LIKE @REMARK  ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    Private Sub SetConditionWhereSQL()


        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("LOAD_WT_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" WT.LOAD_WT - WT.UNSO_WT >= @LOAD_WT_FROM ")
                andstr.Append(vbNewLine)


            End If

            whereStr = .Item("LOAD_WT_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" WT.LOAD_WT- WT.UNSO_WT <= @LOAD_WT_TO ")
                andstr.Append(vbNewLine)


            End If


            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(請求先マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_DATE_FROM", .Item("TRIP_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_DATE_To", .Item("TRIP_DATE_To").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOAD_WT_FROM", .Item("LOAD_WT_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOAD_WT_TO", .Item("LOAD_WT_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_FROM", .Item("UNSO_ONDO_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_TO", .Item("UNSO_ONDO_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", .Item("UNSOCO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO", .Item("CAR_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JSHA_KB", .Item("JSHA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", .Item("DRIVER_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))



        End With

    End Sub

#End Region

#End Region

#End Region

End Class
