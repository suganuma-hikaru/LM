' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330  : 納品データ選択&編集
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI330DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI330DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "編集処理"

#Region "排他処理"

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHK As String = "SELECT                                       " & vbNewLine _
                                          & " COUNT(*)       AS SELECT_CNT                " & vbNewLine _
                                          & "FROM                                         " & vbNewLine _
                                          & " $LM_TRN$..H_OUTKAEDI_L_PRT_LNZ LONZA_L      " & vbNewLine _
                                          & "WHERE                                        " & vbNewLine _
                                          & "    LONZA_L.NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
                                          & "AND LONZA_L.EDI_CTL_NO = @EDI_CTL_NO         " & vbNewLine _
                                          & "AND LONZA_L.SYS_UPD_DATE = @SYS_UPD_DATE     " & vbNewLine _
                                          & "AND LONZA_L.SYS_UPD_TIME = @SYS_UPD_TIME     " & vbNewLine

#End Region

#End Region

#Region "検索処理"

#Region "検索処理 件数取得"

    ''' <summary>
    ''' 検索処理 件数取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = "SELECT                                       " & vbNewLine _
                                             & " COUNT(*)       AS SELECT_CNT                " & vbNewLine _
                                             & "FROM                                         " & vbNewLine _
                                             & " $LM_TRN$..H_OUTKAEDI_L_PRT_LNZ LONZA_L      " & vbNewLine


#End Region

#Region "検索処理 SELECT句"

    ''' <summary>
    ''' 検索処理 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                 " & vbNewLine _
                                               & "  LONZA_L.NRS_BR_CD        AS NRS_BR_CD                " & vbNewLine _
                                               & ", LONZA_L.CUST_CD_L        AS CUST_CD_L                " & vbNewLine _
                                               & ", LONZA_L.CUST_NM_L        AS CUST_NM_L                " & vbNewLine _
                                               & ", LONZA_L.CUST_ORD_NO      AS DELIVERY_NO              " & vbNewLine _
                                               & ", LONZA_L.DEST_CD          AS DEST_CD                  " & vbNewLine _
                                               & ", DEST.DEST_NM             AS DEST_NM                  " & vbNewLine _
                                               & ", LONZA_L.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE          " & vbNewLine _
                                               & ", LONZA_L.ARR_PLAN_DATE    AS ARR_PLAN_DATE            " & vbNewLine _
                                               & ", LONZA_L.EDI_CTL_NO       AS EDI_CTL_NO               " & vbNewLine _
                                               & ", LONZA_L.FREE_C06         AS FREE_C06                 " & vbNewLine _
                                               & ", LONZA_M.CUST_GOODS_CD    AS GOODS_CD_CUST            " & vbNewLine _
                                               & ", LONZA_M.LOT_NO           AS LOT_NO                   " & vbNewLine _
                                               & ", LONZA_M.GOODS_NM         AS GOODS_NM                 " & vbNewLine _
                                               & ", LONZA_M.OUTKA_TTL_NB     AS OUTKA_TTL_NB             " & vbNewLine _
                                               & ", LONZA_M.BUYER_ORD_NO_DTL AS BUYER_ORD_NO             " & vbNewLine _
                                               & ", LONZA_M.EDI_CTL_NO_CHU   AS EDI_CTL_NO_CHU           " & vbNewLine _
                                               & ", LONZA_L.CRT_USER         AS SYS_ENT_USER             " & vbNewLine _
                                               & ", LONZA_L.CRT_DATE         AS SYS_ENT_DATE             " & vbNewLine _
                                               & ", LONZA_L.CRT_TIME         AS SYS_ENT_TIME             " & vbNewLine _
                                               & ", LONZA_L.SYS_UPD_USER     AS SYS_UPD_USER             " & vbNewLine _
                                               & ", LONZA_L.SYS_UPD_DATE     AS SYS_UPD_DATE             " & vbNewLine _
                                               & ", LONZA_L.SYS_UPD_TIME     AS SYS_UPD_TIME             " & vbNewLine _
                                               & ", LONZA_L.SYS_DEL_FLG      AS SYS_DEL_FLG              " & vbNewLine

#End Region

#Region "検索処理 FROM句"

    ''' <summary>
    ''' 検索処理 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                              " & vbNewLine _
                                                    & "    $LM_TRN$..H_OUTKAEDI_L_PRT_LNZ LONZA_L        " & vbNewLine _
                                                    & "LEFT JOIN                                         " & vbNewLine _
                                                    & "    $LM_TRN$..H_OUTKAEDI_M_PRT_LNZ LONZA_M        " & vbNewLine _
                                                    & " ON LONZA_L.NRS_BR_CD = LONZA_M.NRS_BR_CD         " & vbNewLine _
                                                    & "AND LONZA_L.EDI_CTL_NO = LONZA_M.EDI_CTL_NO       " & vbNewLine _
                                                    & "AND LONZA_M.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                                    & "LEFT JOIN                                         " & vbNewLine _
                                                    & "    $LM_MST$..M_DEST DEST                         " & vbNewLine _
                                                    & " ON LONZA_L.NRS_BR_CD = DEST.NRS_BR_CD            " & vbNewLine _
                                                    & "AND LONZA_L.CUST_CD_L = DEST.CUST_CD_L            " & vbNewLine _
                                                    & "AND LONZA_L.DEST_CD = DEST.DEST_CD                " & vbNewLine _
                                                    & "AND DEST.SYS_DEL_FLG = '0'                        " & vbNewLine

#End Region

#Region "検索処理 ORDER BY句"

    ''' <summary>
    ''' 検索処理 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENSAKU As String = "ORDER BY                                         " & vbNewLine _
                                                     & "   LONZA_L.EDI_CTL_NO                            " & vbNewLine

#End Region

#End Region

#Region "保存処理"

#Region "存在チェック"

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHK As String = "SELECT                                        " & vbNewLine _
                                           & "   MAX(MAIN.SELECT_CNT) AS SELECT_CNT         " & vbNewLine _
                                           & "FROM(                                         " & vbNewLine _
                                           & "     SELECT                                   " & vbNewLine _
                                           & "       COUNT(*) AS SELECT_CNT                 " & vbNewLine _
                                           & "     FROM                                     " & vbNewLine _
                                           & "         $LM_MST$..M_GOODS GOODS              " & vbNewLine _
                                           & "     WHERE                                    " & vbNewLine _
                                           & "         GOODS.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                           & "     AND GOODS.CUST_CD_L = @CUST_CD_L         " & vbNewLine _
                                           & "     AND GOODS.CUST_CD_M = '00'               " & vbNewLine _
                                           & "     AND GOODS.GOODS_CD_CUST = @CUST_GOODS_CD " & vbNewLine _
                                           & "    UNION ALL                                 " & vbNewLine _
                                           & "     SELECT                                   " & vbNewLine _
                                           & "       COUNT(*) AS SELECT_CNT                 " & vbNewLine _
                                           & "     FROM                                     " & vbNewLine _
                                           & "         $LM_TRN$..M_SET_GOODS_LNZ GOODS_SET  " & vbNewLine _
                                           & "     WHERE                                    " & vbNewLine _
                                           & "         GOODS_SET.NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
                                           & "     AND GOODS_SET.OYA_CD = @CUST_GOODS_CD    " & vbNewLine _
                                           & "     )MAIN                                    " & vbNewLine

#End Region

#Region "編集登録"

    ''' <summary>
    ''' 保存処理(LONZA_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DATA_L As String = "UPDATE                                " & vbNewLine _
                                              & "  $LM_TRN$..H_OUTKAEDI_L_PRT_LNZ      " & vbNewLine _
                                              & "SET                                   " & vbNewLine _
                                              & "  ARR_PLAN_DATE = @ARR_PLAN_DATE      " & vbNewLine _
                                              & ", DEST_CD  = @DEST_CD                 " & vbNewLine _
                                              & ", DEST_NM = @DEST_NM                  " & vbNewLine _
                                              & ", CUST_ORD_NO = @DELIVERY_NO          " & vbNewLine _
                                              & ", SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
                                              & ", SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine _
                                              & ", SYS_UPD_PGID = @SYS_UPD_PGID        " & vbNewLine _
                                              & ", SYS_UPD_USER = @SYS_UPD_USER        " & vbNewLine _
                                              & ", SYS_DEL_FLG = @SYS_DEL_FLG          " & vbNewLine _
                                              & "WHERE                                 " & vbNewLine _
                                              & "    NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                              & "AND CUST_CD_L = @CUST_CD_L            " & vbNewLine _
                                              & "AND EDI_CTL_NO = @EDI_CTL_NO          " & vbNewLine

    ''' <summary>
    ''' 保存処理(LONZA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DATA_M As String = "UPDATE                                " & vbNewLine _
                                              & "  $LM_TRN$..H_OUTKAEDI_M_PRT_LNZ      " & vbNewLine _
                                              & "SET                                   " & vbNewLine _
                                              & "  BUYER_ORD_NO_DTL = @BUYER_ORD_NO    " & vbNewLine _
                                              & ", CUST_GOODS_CD  = @CUST_GOODS_CD     " & vbNewLine _
                                              & ", GOODS_NM = @GOODS_NM                " & vbNewLine _
                                              & ", LOT_NO = @LOT_NO                    " & vbNewLine _
                                              & ", OUTKA_TTL_NB = @OUTKA_TTL_NB        " & vbNewLine _
                                              & ", SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
                                              & ", SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine _
                                              & ", SYS_UPD_PGID = @SYS_UPD_PGID        " & vbNewLine _
                                              & ", SYS_UPD_USER = @SYS_UPD_USER        " & vbNewLine _
                                              & ", SYS_DEL_FLG = @SYS_DEL_FLG          " & vbNewLine _
                                              & "WHERE                                 " & vbNewLine _
                                              & "    NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                              & "AND EDI_CTL_NO = @EDI_CTL_NO          " & vbNewLine _
                                              & "AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU  " & vbNewLine
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

#Region "SQLメイン処理"

#Region "編集処理"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Sub HaitaChk(ByVal ds As DataSet)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI330DAC.SQL_HAITA_CHK)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI330DAC", "HaitaChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 閾値件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI330DAC.SQL_SELECT_COUNT)          'SQL構築(件数取得)
        Call Me.SetSQLSelectWhereKensaku()                     '条件設定(WHERE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI330DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        reader.Read()

        '処理件数の設定
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI330DAC.SQL_SELECT_KENSAKU)        'SQL構築(SELECT句)
        Me._StrSql.Append(LMI330DAC.SQL_SELECT_FROM_KENSAKU)   'SQL構築(FROM句)
        Call Me.SetSQLSelectWhereKensaku()                     '条件設定(WHERE句)
        Me._StrSql.Append(LMI330DAC.SQL_SELECT_ORDER_KENSAKU)  'SQL構築(ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI330DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("DELIVERY_NO", "DELIVERY_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI330OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExistData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI330DAC.SQL_SELECT_CHK)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetExistChkParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI330DAC", "SelectExistData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        reader.Read()

        '処理件数の設定
        If Convert.ToInt32(reader("SELECT_CNT")) = 0 Then
            MyBase.SetMessage("E493", New String() {"対象データ", "商品マスタまたは商品セットマスタ", ""})
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 編集登録(LONZA_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>編集登録SQLの構築・発行</remarks>
    Private Function UpdateDataL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI330DAC.SQL_UPDATE_DATA_L)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamSaveL()
        Call Me.SetParamCommonSystemUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI330DAC", "UpdateData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 編集登録(LONZA_M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>編集登録SQLの構築・発行</remarks>
    Private Function UpdateDataM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI330IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI330DAC.SQL_UPDATE_DATA_M)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamSaveM()
        Call Me.SetParamCommonSystemUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI330DAC", "UpdateData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "パラメータ設定"

#Region "編集処理"

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "削除・復活処理"

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpDel()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKI_DATE", .Item("HIKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEISAI_NO", .Item("MEISAI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIN_CD", .Item("HIN_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKITORI_CD", .Item("HIKITORI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectWhereKensaku()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.WH_CD = @WH_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.CUST_CD_L = @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '出荷予定日 FROM
            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.OUTKA_PLAN_DATE >= @F_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '出荷予定日 TO
            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.OUTKA_PLAN_DATE <= @T_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
            End If

            'デリバリー№
            whereStr = .Item("DELIVERY_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.CUST_ORD_NO LIKE @DELIVERY_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.DEST_CD LIKE @DEST_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名称
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" LONZA_L.DEST_NM LIKE @DEST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(andstr)
                Me._StrSql.Append("AND LONZA_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)

            End If

        End With

    End Sub

#End Region

#Region "保存処理"

    ''' <summary>
    ''' パラメータ設定モジュール(存在チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetExistChkParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(LONZA_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSaveL()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_NO", .Item("DELIVERY_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(LONZA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSaveM()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "パラメータ設定 共通"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

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
