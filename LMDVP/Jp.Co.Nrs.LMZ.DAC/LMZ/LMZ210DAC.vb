' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ210DAC : 届先マスタ照会
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ210DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ210DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(M_DEST.DEST_CD) AS SELECT_CNT   " & vbNewLine

      ''' <summary>
    ''' CUST_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                     " & vbNewLine _
                                            & "        M_DEST.NRS_BR_CD          AS NRS_BR_CD              " & vbNewLine _
                                            & "      , M_DEST.CUST_CD_L          AS CUST_CD_L              " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_L          AS CUST_NM_L              " & vbNewLine _
                                            & "      , M_DEST.DEST_NM            AS DEST_NM                " & vbNewLine _
                                            & "      , M_DEST.AD_1               AS AD_1                   " & vbNewLine _
                                            & "      , M_DEST.DEST_CD            AS DEST_CD                " & vbNewLine _
                                            & "      , M_DEST.ZIP                AS ZIP                    " & vbNewLine _
                                            & "      , M_DEST.AD_2               AS AD_2                   " & vbNewLine _
                                            & "      , M_DEST.AD_3               AS AD_3                   " & vbNewLine _
                                            & "      , M_DEST.TEL                AS TEL                    " & vbNewLine _
                                            & "      , M_DEST.FAX                AS FAX                    " & vbNewLine _
                                            & "      , M_DEST.UNCHIN_SEIQTO_CD   AS UNCHIN_SEIQTO_CD       " & vbNewLine _
                                            & "      , M_DEST.JIS                AS JIS                    " & vbNewLine _
                                            & "      , M_DEST.SP_NHS_KB          AS SP_NHS_KB              " & vbNewLine _
                                            & "      , M_DEST.COA_YN             AS COA_YN                 " & vbNewLine _
                                            & "      , M_DEST.EDI_CD             AS EDI_CD                 " & vbNewLine _
                                            & "      , M_DEST.CUST_DEST_CD       AS CUST_DEST_CD           " & vbNewLine _
                                            & "      , M_DEST.SALES_CD           AS SALES_CD               " & vbNewLine _
                                            & "      , M_DEST.SP_UNSO_CD         AS SP_UNSO_CD             " & vbNewLine _
                                            & "      , M_DEST.SP_UNSO_BR_CD      AS SP_UNSO_BR_CD          " & vbNewLine _
                                            & "      , M_DEST.DELI_ATT           AS DELI_ATT               " & vbNewLine _
                                            & "      , M_DEST.CARGO_TIME_LIMIT   AS CARGO_TIME_LIMIT       " & vbNewLine _
                                            & "      , M_DEST.LARGE_CAR_YN       AS LARGE_CAR_YN           " & vbNewLine _
                                            & "      , M_DEST.KYORI              AS KYORI                  " & vbNewLine _
                                            & "      , M_DEST.PICK_KB            AS PICK_KB                " & vbNewLine _
                                            & "      , M_DEST.BIN_KB             AS BIN_KB                 " & vbNewLine _
                                            & "      , M_DEST.MOTO_CHAKU_KB      AS MOTO_CHAKU_KB          " & vbNewLine _
                                            & "      , M_DEST.URIAGE_CD          AS URIAGE_CD              " & vbNewLine _
                                            & "      , M_DEST.REMARK             AS REMARK                 " & vbNewLine

#End Region

#Region "FROM句"
    Private Const SQL_FROM_DATA As String = " FROM                                                    " & vbNewLine _
                                          & "      $LM_MST$..M_DEST M_DEST                            " & vbNewLine _
                                          & "      --荷主マスタ                                       " & vbNewLine _
                                          & "      LEFT JOIN $LM_MST$..M_CUST M_CUST                  " & vbNewLine _
                                          & "             ON M_CUST.NRS_BR_CD  = M_DEST.NRS_BR_CD     " & vbNewLine _
                                          & "            AND M_CUST.CUST_CD_L  = M_DEST.CUST_CD_L     " & vbNewLine _
                                          & "            AND M_CUST.CUST_CD_M  = '00'                 " & vbNewLine _
                                          & "            AND M_CUST.CUST_CD_S  = '00'                 " & vbNewLine _
                                          & "            AND M_CUST.CUST_CD_SS = '00'                 " & vbNewLine
#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY           " & vbNewLine _
                                         & "       DEST_CD      " & vbNewLine

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
    ''' 届先マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ210DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMZ210DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ210DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ210IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ210DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMZ210DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMZ210DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ210DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("AD_1", "AD_1")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("ZIP", "ZIP")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("JIS", "JIS")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("CUST_DEST_CD", "CUST_DEST_CD")
        map.Add("SALES_CD", "SALES_CD")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("DELI_ATT", "DELI_ATT")
        map.Add("CARGO_TIME_LIMIT", "CARGO_TIME_LIMIT")
        map.Add("LARGE_CAR_YN", "LARGE_CAR_YN")
        map.Add("KYORI", "KYORI")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("MOTO_CHAKU_KB", "MOTO_CHAKU_KB")
        map.Add("URIAGE_CD", "URIAGE_CD")
        map.Add("REMARK", "REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ210OUT")

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
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append("WHERE M_DEST.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '要望番号1604 2012/11/16 本明追加 START
            Me._StrSql.Append("AND M_DEST.SYS_DEL_FLG = '0'")
            Me._StrSql.Append(vbNewLine)
            '要望番号1604 2012/11/16 本明追加 START

            '要望番号1949 2013/04/09 修正START
            '関連表示有無フラグのチェックの有無(無:荷主コードのみ検索,有:荷主コード+'ZZZZZ'荷主で検索)
            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            Me._StrSql.Append("AND (M_DEST.CUST_CD_L = @CUST_CD_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            'End If

            If .Item("RELATION_SHOW_FLG").ToString().Equals("1") = True Then

                '荷主コード(大)
                whereStr = .Item("CUST_CD_L").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append("OR EXISTS (SELECT S_KBN.KBN_NM1 ")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("FROM $LM_MST$..Z_KBN S_KBN")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("WHERE S_KBN.KBN_GROUP_CD = 'T017'")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND S_KBN.KBN_NM1 = M_DEST.CUST_CD_L)")
                End If

            End If
            Me._StrSql.Append(")")
            '要望番号1949 2013/04/09 修正END

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND M_DEST.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("AD_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND M_DEST.AD_1 LIKE @AD_1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND M_DEST.REMARK LIKE @REMARK")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND M_DEST.DEST_CD LIKE @DEST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue

            ' 配送時注意事項
            whereStr = .Item("DELI_ATT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND M_DEST.DELI_ATT LIKE @DELI_ATT")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELI_ATT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
#End If




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
