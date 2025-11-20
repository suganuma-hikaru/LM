' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF850    : 岡山貨物CSVマスタ(運送データ用)
'  作  成  者       :  [UMANO]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF850DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF850DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 岡山貨物CSV作成データ検索用SQL(運送データ) SELECT部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_Okaken_CSV_UNSO As String = " SELECT                                                                                  " & vbNewLine _
                                                       & "  UNSOL.NRS_BR_CD AS NRS_BR_CD                                                           " & vbNewLine _
                                                       & " ,UNSOL.CUST_REF_NO AS CUSTORDNO                                                         " & vbNewLine _
                                                       & " ,UNSOL.OUTKA_PLAN_DATE AS SYUKKABI                                                      " & vbNewLine _
                                                       & " ,'' AS NIOKURININ_CD                                                                    " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C01 AS NIOKURININ_MEI1                                                 " & vbNewLine _
                                                       & " ,NRSBR.NRS_BR_NM AS NIOKURININ_MEI2                                                     " & vbNewLine _
                                                       & " ,'' AS KEN                                                                              " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.AD_1                                                                " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_1                                                                   " & vbNewLine _
                                                       & "  END AS NIOKURININ_ADD1                                                                 " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.AD_2                                                                " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_2                                                                   " & vbNewLine _
                                                       & "  END AS NIOKURININ_ADD2                                                                 " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.AD_3                                                                " & vbNewLine _
                                                       & "       ELSE NRSBR.AD_3                                                                   " & vbNewLine _
                                                       & "  END AS NIOKURININ_ADD3                                                                 " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.TEL                                                                 " & vbNewLine _
                                                       & "       ELSE NRSBR.TEL                                                                    " & vbNewLine _
                                                       & "  END AS NIOKURININ_TEL                                                                  " & vbNewLine _
                                                       & " ,CASE WHEN DESTORIG.DEST_CD IS NOT NULL                                                 " & vbNewLine _
                                                       & "       THEN DESTORIG.ZIP                                                                 " & vbNewLine _
                                                       & "       ELSE NRSBR.ZIP                                                                    " & vbNewLine _
                                                       & "  END AS NIOKURININ_ZIP                                                                  " & vbNewLine _
                                                       & " ,'' AS NIOKURININ_KANA                                                                  " & vbNewLine _
                                                       & " ,'' AS EDA_CD                                                                           " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_CD                                                                      " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.DEST_NM                                                                " & vbNewLine _
                                                       & "       ELSE DEST.DEST_NM                                                                 " & vbNewLine _
                                                       & "  END AS NIUKENIN_NM1                                                                    " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_NM2                                                                     " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_KEN                                                                     " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.AD_1 + ' ' +                                                           " & vbNewLine _
                                                       & "            DEST2.AD_2 + ' ' +                                                           " & vbNewLine _
                                                       & "            UNSOL.AD_3                                                                   " & vbNewLine _
                                                       & "       ELSE DEST.AD_1 + ' ' +                                                            " & vbNewLine _
                                                       & "            DEST.AD_2 + ' ' +                                                            " & vbNewLine _
                                                       & "            UNSOL.AD_3                                                                   " & vbNewLine _
                                                       & "  END AS NIUKENIN_ADD1                                                                   " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_ADD2                                                                    " & vbNewLine _
                                                       & " ,'' AS NIUKENIN_ADD3                                                                    " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.TEL                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.TEL                                                                     " & vbNewLine _
                                                       & "  END AS NIUKENIN_TEL                                                                    " & vbNewLine _
                                                       & " ,CASE WHEN 0 < LEN(DEST.CUST_DEST_CD)                                                   " & vbNewLine _
                                                       & "       THEN DEST2.ZIP                                                                    " & vbNewLine _
                                                       & "       ELSE DEST.ZIP                                                                     " & vbNewLine _
                                                       & "  END AS NIUKENIN_ZIP                                                                    " & vbNewLine _
                                                       & " ,'' AS DEST_NM_KANA                                                                     " & vbNewLine _
                                                       & " ,UNSOL.UNSO_PKG_NB AS KOSU                                                              " & vbNewLine _
                                                       & " ,UNSOL.UNSO_WT AS JYURYO                                                                " & vbNewLine _
                                                       & " ,'0' AS JYURYO_SAI                                                                      " & vbNewLine _
                                                       & " ,DEST3.DEST_NM AS SHIP_NM_L                                                             " & vbNewLine _
                                                       & " ,CUST.DENPYO_NM AS DENPYO_NM                                                            " & vbNewLine _
                                                       & " ,'' AS KIJI_1                                                                           " & vbNewLine _
                                                       & " ,UNSOL.CUST_REF_NO AS KIJI_2                                                            " & vbNewLine _
                                                       & " ,UNSOL.BUY_CHU_NO AS KIJI_3                                                             " & vbNewLine _
                                                       & " ,UNSOL.REMARK AS KIJI_4                                                                 " & vbNewLine _
                                                       & " ,'' AS KIJI_5                                                                           " & vbNewLine _
                                                       & " ,'' AS KIJI_6                                                                           " & vbNewLine _
                                                       & " ,SUBSTRING(UNSOL.ARR_PLAN_DATE,5,2) AS HAISOBI_MM                                       " & vbNewLine _
                                                       & " ,SUBSTRING(UNSOL.ARR_PLAN_DATE,7,2) AS HAISOBI_DD                                       " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C04 AS REMARK_NOUKI                                                    " & vbNewLine _
                                                       & " ,'' AS OKURI_NO                                                                         " & vbNewLine _
                                                       & " ,'' AS KAMOTSU_NO                                                                       " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C05 AS MOTOTYAKU_KB                                                    " & vbNewLine _
                                                       & " ,OKURIJOCSV.FREE_C06 AS KASHIKIRI_KB                                                    " & vbNewLine _
                                                       & " ,@ROW_NO AS ROW_NO                                                                      " & vbNewLine _
                                                       & " ,UNSOL.UNSO_NO_L AS UNSO_NO_L                                                           " & vbNewLine _
                                                       & " ,UNSOM.UNSO_NO_M AS UNSO_NO_M                                                           " & vbNewLine _
                                                       & " ,@FILEPATH AS FILEPATH                                                                  " & vbNewLine _
                                                       & " ,@FILENAME AS FILENAME                                                                  " & vbNewLine _
                                                       & " ,@SYS_DATE AS SYS_DATE                                                                  " & vbNewLine _
                                                       & " ,@SYS_TIME AS SYS_TIME                                                                  " & vbNewLine _
                                                       & " ,CUST.CUST_NM_L AS CUST_NM_L                                                            " & vbNewLine

    ''' <summary>
    ''' 岡山貨物CSV作成データ検索用SQL(運送データ) FROM・WHERE部
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_Okaken_CSV_UNSO_FROM As String = " FROM $LM_TRN$..F_UNSO_L UNSOL                                                    " & vbNewLine _
                                                       & " LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM ON                                                 " & vbNewLine _
                                                       & " UNSOL.NRS_BR_CD = UNSOM.NRS_BR_CD AND                                                 " & vbNewLine _
                                                       & " UNSOL.UNSO_NO_L = UNSOM.UNSO_NO_L AND                                                 " & vbNewLine _
                                                       & " UNSOM.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_NRS_BR NRSBR ON                                                 " & vbNewLine _
                                                       & " NRSBR.NRS_BR_CD = UNSOL.NRS_BR_CD                                                     " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                    " & vbNewLine _
                                                       & " CUST.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " CUST.CUST_CD_L = UNSOL.CUST_CD_L AND                                                  " & vbNewLine _
                                                       & " CUST.CUST_CD_M = UNSOL.CUST_CD_M AND                                                  " & vbNewLine _
                                                       & " CUST.CUST_CD_S = '00' AND                                                             " & vbNewLine _
                                                       & " CUST.CUST_CD_SS = '00'                                                                " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                    " & vbNewLine _
                                                       & " DEST.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " DEST.CUST_CD_L = UNSOL.CUST_CD_L AND                                                  " & vbNewLine _
                                                       & " DEST.DEST_CD = UNSOL.DEST_CD                                                          " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST2 ON                                                   " & vbNewLine _
                                                       & " DEST2.NRS_BR_CD = DEST.NRS_BR_CD AND                                                  " & vbNewLine _
                                                       & " DEST2.CUST_CD_L = DEST.CUST_CD_L AND                                                  " & vbNewLine _
                                                       & " DEST2.DEST_CD = DEST.CUST_DEST_CD                                                     " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DEST3 ON                                                   " & vbNewLine _
                                                       & " DEST3.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                                 " & vbNewLine _
                                                       & " DEST3.CUST_CD_L = UNSOL.CUST_CD_L AND                                                 " & vbNewLine _
                                                       & " DEST3.DEST_CD = UNSOL.SHIP_CD                                                         " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_DEST DESTORIG ON                                                " & vbNewLine _
                                                       & " DESTORIG.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                              " & vbNewLine _
                                                       & " DESTORIG.CUST_CD_L = UNSOL.CUST_CD_L AND                                              " & vbNewLine _
                                                       & " DESTORIG.DEST_CD = UNSOL.ORIG_CD                                                      " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV ON                                       " & vbNewLine _
                                                       & " OKURIJOCSV.NRS_BR_CD = UNSOL.NRS_BR_CD AND                                            " & vbNewLine _
                                                       & " OKURIJOCSV.UNSOCO_CD = UNSOL.UNSO_CD AND                                              " & vbNewLine _
                                                       & " OKURIJOCSV.CUST_CD_L = UNSOL.CUST_CD_L AND                                            " & vbNewLine _
                                                       & " OKURIJOCSV.OKURIJO_TP = '02'                                                          " & vbNewLine _
                                                       & " LEFT JOIN $LM_MST$..Z_KBN Z1 ON                                                       " & vbNewLine _
                                                       & " UNSOL.ARR_PLAN_TIME = Z1.KBN_CD AND                                                   " & vbNewLine _
                                                       & " Z1.KBN_GROUP_CD = 'N010'                                                              " & vbNewLine _
                                                       & " WHERE UNSOL.NRS_BR_CD = @NRS_BR_CD                                                    " & vbNewLine _
                                                       & " AND UNSOL.UNSO_NO_L = @UNSO_NO_L                                                      " & vbNewLine _
                                                       & " AND UNSOL.SYS_DEL_FLG = '0'                                                           " & vbNewLine

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
    ''' 岡山貨物CSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>岡山貨物CSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectOkakenCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF850IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF850DAC.SQL_SELECT_Okaken_CSV_UNSO)
        Me._StrSql.Append(LMF850DAC.SQL_SELECT_Okaken_CSV_UNSO_FROM)
        Call setSQLSelect()                   '条件設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF850DAC", "SelectOkakenCsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUSTORDNO", "CUSTORDNO")
        map.Add("SYUKKABI", "SYUKKABI")
        map.Add("NIOKURININ_CD", "NIOKURININ_CD")
        map.Add("NIOKURININ_MEI1", "NIOKURININ_MEI1")
        map.Add("NIOKURININ_MEI2", "NIOKURININ_MEI2")
        map.Add("KEN", "KEN")
        map.Add("NIOKURININ_ADD1", "NIOKURININ_ADD1")
        map.Add("NIOKURININ_ADD2", "NIOKURININ_ADD2")
        map.Add("NIOKURININ_ADD3", "NIOKURININ_ADD3")
        map.Add("NIOKURININ_TEL", "NIOKURININ_TEL")
        map.Add("NIOKURININ_ZIP", "NIOKURININ_ZIP")
        map.Add("NIOKURININ_KANA", "NIOKURININ_KANA")
        map.Add("EDA_CD", "EDA_CD")
        map.Add("NIUKENIN_CD", "NIUKENIN_CD")
        map.Add("NIUKENIN_NM1", "NIUKENIN_NM1")
        map.Add("NIUKENIN_NM2", "NIUKENIN_NM2")
        map.Add("NIUKENIN_KEN", "NIUKENIN_KEN")
        map.Add("NIUKENIN_ADD1", "NIUKENIN_ADD1")
        map.Add("NIUKENIN_ADD2", "NIUKENIN_ADD2")
        map.Add("NIUKENIN_ADD3", "NIUKENIN_ADD3")
        map.Add("NIUKENIN_TEL", "NIUKENIN_TEL")
        map.Add("NIUKENIN_ZIP", "NIUKENIN_ZIP")
        map.Add("DEST_NM_KANA", "DEST_NM_KANA")
        map.Add("KOSU", "KOSU")
        map.Add("JYURYO", "JYURYO")
        map.Add("JYURYO_SAI", "JYURYO_SAI")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("DENPYO_NM", "DENPYO_NM")
        map.Add("KIJI_1", "KIJI_1")
        map.Add("KIJI_2", "KIJI_2")
        map.Add("KIJI_3", "KIJI_3")
        map.Add("KIJI_4", "KIJI_4")
        map.Add("KIJI_5", "KIJI_5")
        map.Add("KIJI_6", "KIJI_6")
        map.Add("HAISOBI_MM", "HAISOBI_MM")
        map.Add("HAISOBI_DD", "HAISOBI_DD")
        map.Add("REMARK_NOUKI", "REMARK_NOUKI")
        map.Add("OKURI_NO", "OKURI_NO")
        map.Add("KAMOTSU_NO", "KAMOTSU_NO")
        map.Add("MOTOTYAKU_KB", "MOTOTYAKU_KB")
        map.Add("KASHIKIRI_KB", "KASHIKIRI_KB")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")
        map.Add("CUST_NM_L", "CUST_NM_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF850OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMF850OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

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

    ''' <summary>
    '''  パラメータ設定モジュール（出荷検索）
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row("UNSO_NO_L"), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
