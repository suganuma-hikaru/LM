' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB570    : コンテナ番号ラベル印刷
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports System.Reflection

''' <summary>
''' LMB570DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB570DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    Private Const SQL_GET_TRN_TBL_EXISTS As String = "" _
        & "SELECT                                                       " & vbNewLine _
        & "    CASE WHEN OBJECT_ID('$LM_TRN$..' + @TBL_NM, 'U') IS NULL " & vbNewLine _
        & "        THEN '0'                                             " & vbNewLine _
        & "        ELSE '1'                                             " & vbNewLine _
        & "    END AS TBL_EXISTS                                        " & vbNewLine _
        & ""

    ''' <summary>
    ''' 帳票種別取得用 兼 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "" _
        & "SELECT DISTINCT                                               " & vbNewLine _
        & "      H_INKAEDI_DTL_TSMC.NRS_BR_CD                            " & vbNewLine _
        & "    , 'DH'                                          AS PTN_ID " & vbNewLine _
        & "    , CASE                                                    " & vbNewLine _
        & "        WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD           " & vbNewLine _
        & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD           " & vbNewLine _
        & "        ELSE MR3.PTN_CD                                       " & vbNewLine _
        & "      END                                           AS PTN_CD " & vbNewLine _
        & "    , CASE                                                    " & vbNewLine _
        & "        WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID           " & vbNewLine _
        & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
        & "        ELSE MR3.RPT_ID                                       " & vbNewLine _
        & "      END                                           AS RPT_ID " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.TRACKING_NO                          " & vbNewLine _
        & "    , H_INKAEDI_DTL_TSMC.INLA_CTL_NO_M                        " & vbNewLine _
        & ""


    ''' <summary>
    ''' 帳票種別取得用 兼 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "" _
        & "FROM                                                              " & vbNewLine _
        & "    -- TSMC_入荷EDI受信データ                                     " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_DTL_TSMC                                  " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- 入荷M                                                      " & vbNewLine _
        & "    $LM_TRN$..B_INKA_M                                            " & vbNewLine _
        & "        ON  B_INKA_M.NRS_BR_CD = H_INKAEDI_DTL_TSMC.NRS_BR_CD     " & vbNewLine _
        & "        AND B_INKA_M.INKA_NO_L = H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L " & vbNewLine _
        & "        AND B_INKA_M.SYS_DEL_FLG  = '0'                           " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- 商品マスタ                                                 " & vbNewLine _
        & "    $LM_MST$..M_GOODS                                             " & vbNewLine _
        & "        ON  M_GOODS.NRS_BR_CD = B_INKA_M.NRS_BR_CD                " & vbNewLine _
        & "        AND M_GOODS.GOODS_CD_NRS = B_INKA_M.GOODS_CD_NRS          " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- TSMC_入荷EDI受信データの荷主での荷主帳票パターン……①     " & vbNewLine _
        & "    $LM_MST$..M_CUST_RPT MCR1                                     " & vbNewLine _
        & "        ON  MCR1.NRS_BR_CD = H_INKAEDI_DTL_TSMC.NRS_BR_CD         " & vbNewLine _
        & "        AND MCR1.CUST_CD_L = H_INKAEDI_DTL_TSMC.CUST_CD_L         " & vbNewLine _
        & "        AND MCR1.CUST_CD_M = H_INKAEDI_DTL_TSMC.CUST_CD_M         " & vbNewLine _
        & "        AND MCR1.CUST_CD_S = '00'                                 " & vbNewLine _
        & "        AND MCR1.PTN_ID = 'DH'                                    " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- 荷主帳票パターン……① の帳票パターン                      " & vbNewLine _
        & "    $LM_MST$..M_RPT MR1                                           " & vbNewLine _
        & "        ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
        & "        AND MR1.PTN_ID = MCR1.PTN_ID                              " & vbNewLine _
        & "        AND MR1.PTN_CD = MCR1.PTN_CD                              " & vbNewLine _
        & "        AND MR1.SYS_DEL_FLG = '0'                                 " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- 商品マスタの荷主での荷主帳票パターン……②                 " & vbNewLine _
        & "    $LM_MST$..M_CUST_RPT MCR2                                     " & vbNewLine _
        & "        ON  MCR2.NRS_BR_CD = M_GOODS.NRS_BR_CD                    " & vbNewLine _
        & "        AND MCR2.CUST_CD_L = M_GOODS.CUST_CD_L                    " & vbNewLine _
        & "        AND MCR2.CUST_CD_M = M_GOODS.CUST_CD_M                    " & vbNewLine _
        & "        AND MCR2.CUST_CD_S = M_GOODS.CUST_CD_S                    " & vbNewLine _
        & "        AND MCR2.PTN_ID = 'DH'                                    " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- 荷主帳票パターン……② の帳票パターン                      " & vbNewLine _
        & "    $LM_MST$..M_RPT MR2                                           " & vbNewLine _
        & "        ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                        " & vbNewLine _
        & "        AND MR2.PTN_ID = MCR2.PTN_ID                              " & vbNewLine _
        & "        AND MR2.PTN_CD = MCR2.PTN_CD                              " & vbNewLine _
        & "        AND MR2.SYS_DEL_FLG = '0'                                 " & vbNewLine _
        & "LEFT JOIN                                                         " & vbNewLine _
        & "    -- 荷主帳票パターン……①② の帳票パターンが                  " & vbNewLine _
        & "    -- 存在しない場合の帳票パターン                               " & vbNewLine _
        & "    $LM_MST$..M_RPT MR3                                           " & vbNewLine _
        & "        ON  MR3.NRS_BR_CD = H_INKAEDI_DTL_TSMC.NRS_BR_CD          " & vbNewLine _
        & "        AND MR3.PTN_ID = 'DH'                                     " & vbNewLine _
        & "        AND MR3.STANDARD_FLAG = '01'                              " & vbNewLine _
        & "        AND MR3.SYS_DEL_FLG = '0'                                 " & vbNewLine _
        & ""


    ''' <summary>
    ''' 帳票種別取得用 兼 印刷データ抽出用 WHERE句 1/2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_1 As String = "" _
        & "WHERE                                               " & vbNewLine _
        & "    H_INKAEDI_DTL_TSMC.NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.INKA_CTL_NO_L = @INKA_NO_L   " & vbNewLine _
        & ""


    ''' <summary>
    ''' 帳票種別取得用 兼 印刷データ抽出用 WHERE句 2/2
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_2 As String = "" _
        & "AND H_INKAEDI_DTL_TSMC.SYS_DEL_FLG = '0'            " & vbNewLine _
        & "AND H_INKAEDI_DTL_TSMC.LVL1_UT IN ('CTR', 'TNK')    " & vbNewLine _
        & "AND CASE                                            " & vbNewLine _
        & "        WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD " & vbNewLine _
        & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD " & vbNewLine _
        & "        ELSE MR3.PTN_CD                             " & vbNewLine _
        & "    END IS NOT NULL                                 " & vbNewLine _
        & ""


    ''' <summary>
    ''' 帳票種別取得用 兼 印刷データ抽出用 ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "" _
        & "ORDER BY                             " & vbNewLine _
        & "    H_INKAEDI_DTL_TSMC.INLA_CTL_NO_M " & vbNewLine _
        & ""

#End Region ' "検索処理 SQL"

#End Region ' "Const"

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

#End Region ' "Field"

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB570_TBL_EXISTS")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMB570DAC.SQL_GET_TRN_TBL_EXISTS)

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TBL_NM", Me._Row.Item("TBL_NM").ToString(), DBDataType.NVARCHAR))

        ' スキーマ名設定
        Dim strSql As String
        strSql = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        Me._Row.Item("TBL_EXISTS") = "0"

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(strSql)

            ' パラメータの反映
            For Each obj As Object In _SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.Read() Then
                    Me._Row.Item("TBL_EXISTS") = Convert.ToString(reader("TBL_EXISTS"))
                End If

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB570IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMB570DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMB570DAC.SQL_FROM)
        Me._StrSql.Append(LMB570DAC.SQL_WHERE_1)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMB570DAC.SQL_WHERE_2)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

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
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB570IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMB570DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMB570DAC.SQL_FROM)
        Me._StrSql.Append(LMB570DAC.SQL_WHERE_1)
        Call Me.SetConditionMasterSQL()
        Me._StrSql.Append(LMB570DAC.SQL_WHERE_2)
        Me._StrSql.Append(LMB570DAC.SQL_ORDER_BY)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        ' SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        ' DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        ' 取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("TRACKING_NO", "TRACKING_NO")
        map.Add("QRCODE", "TRACKING_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB570OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' 検索条件部に入力された条件とパラメータの設定
        Dim whereStr As String = String.Empty
        With Me._Row

            ' 営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            ' 入荷管理番号L
            whereStr = .Item("INKA_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))

            ' 入荷管理番号M
            whereStr = .Item("INKA_NO_M").ToString()
            If whereStr.Trim.Length() > 0 Then
                Me._StrSql.Append("AND H_INKAEDI_DTL_TSMC.INLA_CTL_NO_M = @INKA_NO_M   " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub


#End Region ' "検索処理"

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

#End Region ' "SQL"

#End Region ' "設定処理"


#End Region ' "Method"

End Class
