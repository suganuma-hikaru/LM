' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF620DAC : 運行指示書
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF620DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF620DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	      UNSO_LL.NRS_BR_CD                                AS NRS_BR_CD " & vbNewLine _
                                            & "     , '47'                                             AS PTN_ID    " & vbNewLine _
                                            & "     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "		       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD                                               " & vbNewLine _
                                            & "	 	  END                                              AS PTN_CD    " & vbNewLine _
                                            & "     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "  	 	   WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID                                               " & vbNewLine _
                                            & "		  END                                              AS RPT_ID    " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                           " & vbNewLine _
                                          & "          CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                          " & vbNewLine _
                                          & "               WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                          " & vbNewLine _
                                          & "          ELSE MR3.RPT_ID END                      AS RPT_ID           -- 帳票ID    " & vbNewLine _
                                          & "        , UNSO_LL.TRIP_NO                          AS TRIP_NO          -- 運行番号  " & vbNewLine _
                                          & "        , ISNULL(M_VCLE.CAR_NO,'')                 AS CAR_NO           -- 車番      " & vbNewLine _
                                          & "        , UNSO_LL.TRIP_DATE                        AS TRIP_DATE        -- 運行日    " & vbNewLine _
                                          & "        , MAX(UNSO_L.UNSO_NO_L)                    AS UNSO_NO_L        -- 運送番号L " & vbNewLine _
                                          & "        , Z_KBN.KBN_NM1                            AS BIN_NM           -- 便区分    " & vbNewLine _
                                          & "        , M_CUST.CUST_NM_L                         AS CUST_NM_L        -- 荷主(大)  " & vbNewLine _
                                          & "        , CASE WHEN UNSO_L.MOTO_DATA_KB = '10' THEN M_DEST_N.DEST_NM                " & vbNewLine _
                                          & "               WHEN UNSO_L.MOTO_DATA_KB = '20' THEN M_DEST_S.DEST_NM                " & vbNewLine _
                                          & "          ELSE M_DEST_S.DEST_NM                                                     " & vbNewLine _
                                          & "          END                                      AS DEST_NM          -- 届先名    " & vbNewLine _
                                          & "        , SUM(UNSO_M.BETU_WT * UNSO_M.UNSO_TTL_NB) AS UNSO_WT          -- 運送重量  " & vbNewLine _
                                          & "        , UNSO_L.REMARK                            AS UNSO_REMARK      -- 運送備考  " & vbNewLine _
                                          & "        , M_DRIVER.DRIVER_NM                       AS DRIVER_NM        -- 乗務員名  " & vbNewLine _
                                          & "        , CASE WHEN UNSO_L.MOTO_DATA_KB = '10' THEN MIN(M_DEST_N.AD_1)              " & vbNewLine _
                                          & "               WHEN UNSO_L.MOTO_DATA_KB = '20' THEN MIN(M_DEST_S.AD_1)              " & vbNewLine _
                                          & "          ELSE MIN(M_DEST_S.AD_1)                                                   " & vbNewLine _
                                          & "          END                                      AS DEST_AD_1        -- 届先住所1 " & vbNewLine _
                                          & "        , CASE WHEN UNSO_L.MOTO_DATA_KB = '10' THEN MIN(M_DEST_N.AD_2)              " & vbNewLine _
                                          & "               WHEN UNSO_L.MOTO_DATA_KB = '20' THEN MIN(M_DEST_S.AD_2)              " & vbNewLine _
                                          & "          ELSE MIN(M_DEST_S.AD_2)                                                   " & vbNewLine _
                                          & "          END                                      AS DEST_AD_2        -- 届先住所2 " & vbNewLine _
                                          & "        , M_NRS_BR.NRS_BR_NM                       AS NRS_BR_NM        -- 営業所名  " & vbNewLine
#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = " FROM $LM_TRN$..F_UNSO_LL  UNSO_LL                                " & vbNewLine _
                                     & "      --運送L                                                     " & vbNewLine _
                                     & "      LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                         " & vbNewLine _
                                     & "             ON UNSO_L.NRS_BR_CD = UNSO_LL.NRS_BR_CD              " & vbNewLine _
                                     & "            AND UNSO_L.TRIP_NO   = UNSO_LL.TRIP_NO                " & vbNewLine _
                                     & "            AND UNSO_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                     & "      --運送M                                                     " & vbNewLine _
                                     & "      LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M                         " & vbNewLine _
                                     & "             ON UNSO_M.NRS_BR_CD = UNSO_L.NRS_BR_CD               " & vbNewLine _
                                     & "            AND UNSO_M.UNSO_NO_L = UNSO_L.UNSO_NO_L               " & vbNewLine _
                                     & "            AND UNSO_M.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                     & "      --営業所Ｍ                                                  " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_NRS_BR M_NRS_BR                       " & vbNewLine _
                                     & "             ON M_NRS_BR.NRS_BR_CD = UNSO_LL.NRS_BR_CD            " & vbNewLine _
                                     & "      --乗務員Ｍ                                                  " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_DRIVER M_DRIVER                       " & vbNewLine _
                                     & "             ON M_DRIVER.DRIVER_CD = UNSO_LL.DRIVER_CD            " & vbNewLine _
                                     & "      --車輛Ｍ                                                    " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_VCLE M_VCLE                           " & vbNewLine _
                                     & "             ON M_VCLE.NRS_BR_CD = UNSO_LL.NRS_BR_CD              " & vbNewLine _
                                     & "            AND M_VCLE.CAR_KEY   = UNSO_LL.CAR_KEY                " & vbNewLine _
                                     & "      --荷主Ｍ                                                    " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_CUST M_CUST                           " & vbNewLine _
                                     & "             ON M_CUST.NRS_BR_CD  = UNSO_L.NRS_BR_CD              " & vbNewLine _
                                     & "            AND M_CUST.CUST_CD_L  = UNSO_L.CUST_CD_L              " & vbNewLine _
                                     & "            AND M_CUST.CUST_CD_M  = UNSO_L.CUST_CD_M              " & vbNewLine _
                                     & "            AND M_CUST.CUST_CD_S  = '00'                          " & vbNewLine _
                                     & "            AND M_CUST.CUST_CD_SS = '00'                          " & vbNewLine _
                                     & "      --届先Ｍ (出荷用)                                           " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_DEST M_DEST_S                         " & vbNewLine _
                                     & "             ON M_DEST_S.NRS_BR_CD = UNSO_L.NRS_BR_CD             " & vbNewLine _
                                     & "            AND M_DEST_S.CUST_CD_L = UNSO_L.CUST_CD_L             " & vbNewLine _
                                     & "            AND M_DEST_S.DEST_CD   = UNSO_L.DEST_CD               " & vbNewLine _
                                     & "      --届先Ｍ (入荷用)                                           " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_DEST M_DEST_N                         " & vbNewLine _
                                     & "             ON M_DEST_N.NRS_BR_CD = UNSO_L.NRS_BR_CD             " & vbNewLine _
                                     & "            AND M_DEST_N.CUST_CD_L = UNSO_L.CUST_CD_L             " & vbNewLine _
                                     & "            AND M_DEST_N.DEST_CD   = UNSO_L.ORIG_CD               " & vbNewLine _
                                     & "      --商品Ｍ                                                    " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_GOODS M_GOODS                         " & vbNewLine _
                                     & "             ON M_GOODS.NRS_BR_CD    = UNSO_M.NRS_BR_CD           " & vbNewLine _
                                     & "            AND M_GOODS.GOODS_CD_NRS = UNSO_M.GOODS_CD_NRS        " & vbNewLine _
                                     & "      --区分Ｍ(運送便区分)                                        " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..Z_KBN Z_KBN                             " & vbNewLine _
                                     & "             ON Z_KBN.KBN_GROUP_CD = 'U001'                       " & vbNewLine _
                                     & "            AND Z_KBN.KBN_CD = UNSO_L.BIN_KB                      " & vbNewLine _
                                     & "      -- 帳票パターンマスタ①(UNSO_Lの荷主より取得)               " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                   " & vbNewLine _
                                     & "             ON M_CUSTRPT1.NRS_BR_CD   = UNSO_L.NRS_BR_CD         " & vbNewLine _
                                     & "            AND M_CUSTRPT1.CUST_CD_L   = UNSO_L.CUST_CD_L         " & vbNewLine _
                                     & "            AND M_CUSTRPT1.CUST_CD_M   = UNSO_L.CUST_CD_M         " & vbNewLine _
                                     & "            AND M_CUSTRPT1.CUST_CD_S   = '00'                     " & vbNewLine _
                                     & "            AND M_CUSTRPT1.PTN_ID      = '47'                     " & vbNewLine _
                                     & "            AND M_CUSTRPT1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_RPT  MR1                              " & vbNewLine _
                                     & "             ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD     " & vbNewLine _
                                     & "            AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID        " & vbNewLine _
                                     & "            AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD        " & vbNewLine _
                                     & "            AND MR1.SYS_DEL_FLG        = '0'                      " & vbNewLine _
                                     & "      -- 帳票パターンマスタ②(商品マスタより)                     " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                   " & vbNewLine _
                                     & "             ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD        " & vbNewLine _
                                     & "            AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L        " & vbNewLine _
                                     & "            AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M        " & vbNewLine _
                                     & "            AND M_CUSTRPT2.CUST_CD_S   = '00'                     " & vbNewLine _
                                     & "            AND M_CUSTRPT2.PTN_ID      = '47'                     " & vbNewLine _
                                     & "            AND M_CUSTRPT2.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_RPT  MR2                              " & vbNewLine _
                                     & "             ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD     " & vbNewLine _
                                     & "            AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID        " & vbNewLine _
                                     & "            AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD        " & vbNewLine _
                                     & "            AND MR2.SYS_DEL_FLG        = '0'                      " & vbNewLine _
                                     & "      -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 > " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_RPT MR3                               " & vbNewLine _
                                     & "             ON MR3.NRS_BR_CD          =  UNSO_LL.NRS_BR_CD       " & vbNewLine _
                                     & "            AND MR3.PTN_ID             = '47'                     " & vbNewLine _
                                     & "            AND MR3.STANDARD_FLAG      = '01'                     " & vbNewLine


#End Region

#Region "GROUP BY句"
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                  " & vbNewLine _
                                         & "       MR1.PTN_CD          " & vbNewLine _
                                         & "     , MR2.PTN_CD          " & vbNewLine _
                                         & "     , MR1.RPT_ID          " & vbNewLine _
                                         & "     , MR2.RPT_ID          " & vbNewLine _
                                         & "     , MR3.RPT_ID          " & vbNewLine _
                                         & "     , UNSO_LL.TRIP_NO     " & vbNewLine _
                                         & "     , M_VCLE.CAR_NO       " & vbNewLine _
                                         & "     , UNSO_LL.TRIP_DATE   " & vbNewLine _
                                         & "     , UNSO_L.BIN_KB       " & vbNewLine _
                                         & "     , M_CUST.CUST_NM_L    " & vbNewLine _
                                         & "     , UNSO_L.REMARK       " & vbNewLine _
                                         & "     , M_DRIVER.DRIVER_NM  " & vbNewLine _
                                         & "     , Z_KBN.KBN_NM1       " & vbNewLine _
                                         & "     , M_NRS_BR.NRS_BR_NM  " & vbNewLine _
                                         & "     , UNSO_L.MOTO_DATA_KB " & vbNewLine _
                                         & "     , M_DEST_N.DEST_NM    " & vbNewLine _
                                         & "     , M_DEST_S.DEST_NM    " & vbNewLine



#End Region

#Region "ORDER BY句"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                                        " & vbNewLine _
                                         & "       UNSO_LL.TRIP_DATE      --①運行日         " & vbNewLine _
                                         & "     , UNSO_LL.TRIP_NO        --②運行番号       " & vbNewLine _
                                         & "     , M_VCLE.CAR_NO          --③車番           " & vbNewLine _
                                         & "     , M_DRIVER.DRIVER_NM     --④乗務員名       " & vbNewLine _
                                         & "     , M_DEST_S.DEST_NM       --⑤届先名(出荷用) " & vbNewLine _
                                         & "     , M_DEST_N.DEST_NM       --⑥届先名(入荷用) " & vbNewLine _
                                         & "     , MAX(UNSO_L.UNSO_NO_L)  --⑦MAX(運送番号L) " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMF620IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF620DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF620DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF620DAC", "SelectMPrt", cmd)

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
    ''' 運行情報データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運行データ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF620IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMF620DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用SELECT句)
        Me._StrSql.Append(LMF620DAC.SQL_FROM)             'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMF620DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMF620DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER By句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF620DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("CAR_NO", "CAR_NO")
        map.Add("TRIP_DATE", "TRIP_DATE")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("UNSO_REMARK", "UNSO_REMARK")
        map.Add("DRIVER_NM", "DRIVER_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("NRS_BR_NM", "NRS_BR_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF620OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" UNSO_LL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSO_LL.TRIP_NO = @TRIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", whereStr, DBDataType.CHAR))
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
