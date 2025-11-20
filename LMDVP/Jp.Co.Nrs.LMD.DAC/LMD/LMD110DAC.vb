' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD110    : 振替検索
'  作  成  者       :  DAIKOKU  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD110DAC
''' </summary>
''' <remarks></remarks>
Public Class LMD110DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD110IN"
    Public Const TABLE_NM_OUT As String = "LMD110OUT"


#Region "検索カウント"

    ''' <summary>
    ''' 検索カウント
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_DATA As String = " SELECT                                               " & vbNewLine _
                                           & " COUNT(FURI.FURI_NO)            AS SELECT_CNT         " & vbNewLine

#End Region '検索カウント

#Region "検索SELECT"

    ''' <summary>
    ''' 検索SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                     " & vbNewLine _
                                            & "  FURI.NRS_BR_CD            AS NRS_BR_CD                                 " & vbNewLine _
                                            & " ,FURI.FURI_DATE            AS FURI_DATE                                 " & vbNewLine _
                                            & " ,FURI.MOTO_ORD_NO          AS ORDER_NO                                  " & vbNewLine _
                                            & " ,FURI.FURI_NO              AS FURI_NO                                   " & vbNewLine _
                                            & " ,ISNULL(KBN1.KBN_NM1,'')   AS FURI_NM                                   " & vbNewLine _
                                            & " ,FURI.FURI_KBN			   AS FURI_KBN                                  " & vbNewLine _
                                            & " ,FURI.TAX_KBN			   AS TAX_KBN                                   " & vbNewLine _
                                            & " ,ISNULL(KBN2.KBN_NM1,'')   AS YOUKI_NM                                  " & vbNewLine _
                                            & " ,FURI.YOUKI_HENKO_KBN	   AS YOUKI_HENKO_KBN                           " & vbNewLine _
                                            & " ,MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M          AS MOTO_CUST_NM       " & vbNewLine _
                                            & " ,SCUST.CUST_NM_L + '　' + SCUST.CUST_NM_M          AS SAKI_CUST_NM      " & vbNewLine _
                                            & " ,SGOODS.GOODS_NM_1                                 AS SAKI_GOODS_NM     " & vbNewLine _
                                            & " ,U.USER_NM                                         AS UPD_USER_NM       " & vbNewLine _
                                            & " ,FURI.SYS_ENT_DATE                                 AS FURI_SYS_ENT_DATE " & vbNewLine _
                                            & " ,ISNULL(MU.USER_NM,'')                             AS MOTO_USER_NM      " & vbNewLine _
                                            & " ,ISNULL(SU.USER_NM,'')                             AS SAKI_USER_NM      " & vbNewLine _
                                            & " ,OUTL.OUTKA_NO_L                                   AS OUTKA_NO_L        " & vbNewLine _
                                            & " ,OUTL.SYS_UPD_DATE + OUTL.SYS_UPD_TIME             AS OUT_UP_DT         " & vbNewLine _
                                            & " ,INL.INKA_NO_L                                     AS INKA_NO_L         " & vbNewLine _
                                            & " ,INL.SYS_UPD_DATE + INL.SYS_UPD_TIME               AS IN_UP_DT          " & vbNewLine _
                                            & " ,FURI.SYS_UPD_DATE                                 AS UPD_DATE          " & vbNewLine _
                                            & " ,FURI.SYS_UPD_DATE + FURI.SYS_UPD_TIME             AS UP_DT             " & vbNewLine

#End Region '検索SELECT

#Region "検索FROM句"

    ''' <summary>
    ''' 検索FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                        " & vbNewLine _
                                            & " --振替データ                                             " & vbNewLine _
                                            & " $LM_TRN$..D_FURIKAE_TRS FURI                             " & vbNewLine _
                                            & " --荷主 振替先                                            " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST MCUST                         " & vbNewLine _
                                            & "   ON MCUST.NRS_BR_CD   = FURI.NRS_BR_CD                  " & vbNewLine _
                                            & "  AND MCUST.CUST_CD_L   = FURI.MOTO_CUST_CD_L             " & vbNewLine _
                                            & "  AND MCUST.CUST_CD_M   = FURI.MOTO_CUST_CD_M             " & vbNewLine _
                                            & "  AND MCUST.CUST_CD_S   = '00'                            " & vbNewLine _
                                            & "  AND MCUST.CUST_CD_SS  = '00'                            " & vbNewLine _
                                            & " --荷主 振替先                                            " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST SCUST                         " & vbNewLine _
                                            & "   ON SCUST.NRS_BR_CD   = FURI.NRS_BR_CD                  " & vbNewLine _
                                            & "  AND SCUST.CUST_CD_L   = FURI.SAKI_CUST_CD_L             " & vbNewLine _
                                            & "  AND SCUST.CUST_CD_M   = FURI.SAKI_CUST_CD_M             " & vbNewLine _
                                            & "  AND SCUST.CUST_CD_S   = '00'                            " & vbNewLine _
                                            & "  AND SCUST.CUST_CD_SS  = '00'                            " & vbNewLine _
                                            & " --区分　振替区分                                         " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN KBN1                           " & vbNewLine _
                                            & "   ON KBN1.KBN_GROUP_CD  = 'H004'                         " & vbNewLine _
                                            & "  AND KBN1.KBN_CD        = FURI.FURI_KBN                  " & vbNewLine _
                                            & " --区分　容器変更                                         " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN KBN2                           " & vbNewLine _
                                            & "   ON KBN2.KBN_GROUP_CD  = 'U009'                         " & vbNewLine _
                                            & "  AND KBN2.KBN_CD        = FURI.YOUKI_HENKO_KBN           " & vbNewLine _
                                            & " --商品　振替先                                           " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_GOODS  SGOODS                      " & vbNewLine _
                                            & "   ON SGOODS.NRS_BR_CD     = FURI.NRS_BR_CD               " & vbNewLine _
                                            & "  AND SGOODS.GOODS_CD_CUST = FURI.SAKI_GOODS_CD_NRS       " & vbNewLine _
                                            & "  --ユーザー                                              " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..S_USER  U                            " & vbNewLine _
                                            & "   ON U.USER_CD     = FURI.SYS_UPD_USER                   " & vbNewLine _
                                            & " --出荷                                                   " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..C_OUTKA_L OUTL   -- 013118 LM_TRN_10固定修正" & vbNewLine _
                                            & "   ON OUTL.NRS_BR_CD   = FURI.NRS_BR_CD                   " & vbNewLine _
                                            & "  AND OUTL.FURI_NO     = FURI.FURI_NO                     " & vbNewLine _
                                            & "  AND OUTL.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                            & " --入荷                                                   " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..B_INKA_L  INL     -- 013118 LM_TRN_10固定修正" & vbNewLine _
                                            & "   ON INL.NRS_BR_CD   = FURI.NRS_BR_CD                    " & vbNewLine _
                                            & "  AND INL.FURI_NO     = FURI.FURI_NO                      " & vbNewLine _
                                            & "  AND INL.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                            & " --振替元担当ユーザー                                     " & vbNewLine _
                                            & " LEFT JOIN LM_MST..S_USER  MU                             " & vbNewLine _
                                            & "   ON MU.USER_CD     = MCUST.TANTO_CD                     " & vbNewLine _
                                            & " --振替先担当ユーザー                                     " & vbNewLine _
                                            & " LEFT JOIN LM_MST..S_USER  SU                             " & vbNewLine _
                                            & "   ON SU.USER_CD     = SCUST.TANTO_CD                     " & vbNewLine


#End Region '検索FROM句

#Region "検索ORDERBY句"

    ''' <summary>
    ''' 検索ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDERBY As String = " ORDER BY FURI.FURI_NO DESC "

#End Region '検索ORDERBY句



#Region "印刷チェック"

    Private Const SQL_SELECT_PRINT_CHECK As String = " SELECT                                               " & vbNewLine _
                                        & " M_CUST.CUST_NM_L            AS CUST_NM_L                        " & vbNewLine _
                                        & ",M_CUST.CUST_NM_M            AS CUST_NM_M                        " & vbNewLine _
                                        & ",Z_KBN.KBN_CD                AS CLOSE_DATE                       " & vbNewLine _
                                        & " FROM  $LM_MST$..M_CUST         M_CUST                           " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..M_SEIQTO  M_SEIQTO                         " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " M_CUST.SAGYO_SEIQTO_CD = M_SEIQTO.SEIQTO_CD                     " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.NRS_BR_CD       = M_SEIQTO.NRS_BR_CD                     " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN                            " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " Z_KBN.KBN_GROUP_CD      = 'S008'                                " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_SEIQTO.CLOSE_KB       = Z_KBN.KBN_CD                          " & vbNewLine _
                                        & " WHERE                                                           " & vbNewLine _
                                        & " M_CUST.NRS_BR_CD       = @NRS_BR_CD                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_L       = @CUST_CD_L                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_M       = @CUST_CD_M                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_S       = '00'                                   " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_SS      = '00'                                   " & vbNewLine


    Private Const SQL_SELECT_PRINT_CHECK_SEIQ As String = " SELECT                                          " & vbNewLine _
                                        & " M_CUST.CUST_NM_L            AS CUST_NM_L                        " & vbNewLine _
                                        & ",M_CUST.CUST_NM_M            AS CUST_NM_M                        " & vbNewLine _
                                        & ",Z_KBN.KBN_CD                AS CLOSE_DATE                       " & vbNewLine _
                                        & " FROM  $LM_MST$..M_CUST         M_CUST                           " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..M_SEIQTO  M_SEIQTO                         " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " M_SEIQTO.NRS_BR_CD     = M_CUST.NRS_BR_CD                       " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_SEIQTO.SEIQTO_CD     = @SEIQTO_CD                             " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN                            " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " Z_KBN.KBN_GROUP_CD      = 'S008'                                " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_SEIQTO.CLOSE_KB       = Z_KBN.KBN_CD                          " & vbNewLine _
                                        & " WHERE                                                           " & vbNewLine _
                                        & " M_CUST.NRS_BR_CD       = @NRS_BR_CD                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_L       = @CUST_CD_L                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_M       = @CUST_CD_M                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_S       = '00'                                   " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_SS      = '00'                                   " & vbNewLine



#End Region

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 条件設定用
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

#End Region 'Field

#Region "Method"

#Region "検索件数取得処理"

    ''' <summary>
    ''' 検索件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMD110DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD110DAC.SQL_COUNT_DATA)
        Me._StrSql.Append(LMD110DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSQL()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD110DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMD110DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD110DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMD110DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSQL()

        Me._StrSql.Append(LMD110DAC.SQL_SELECT_ORDERBY)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD110DAC", "SelectListData", cmd)

        'Debug.Print(cmd.CommandText)
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("FURI_DATE", "FURI_DATE")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("FURI_NO", "FURI_NO")
        map.Add("FURI_NM", "FURI_NM")
        map.Add("FURI_KBN", "FURI_KBN")
        map.Add("TAX_KBN", "TAX_KBN")
        map.Add("YOUKI_NM", "YOUKI_NM")
        map.Add("YOUKI_HENKO_KBN", "YOUKI_HENKO_KBN")
        map.Add("MOTO_CUST_NM", "MOTO_CUST_NM")
        map.Add("SAKI_CUST_NM", "SAKI_CUST_NM")
        map.Add("SAKI_GOODS_NM", "SAKI_GOODS_NM")
        map.Add("UPD_USER_NM", "UPD_USER_NM")
        map.Add("FURI_SYS_ENT_DATE", "FURI_SYS_ENT_DATE")
        map.Add("MOTO_USER_NM", "MOTO_USER_NM")
        map.Add("SAKI_USER_NM", "SAKI_USER_NM")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUT_UP_DT", "OUT_UP_DT")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("IN_UP_DT", "IN_UP_DT")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UP_DT", "UP_DT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMD110DAC.TABLE_NM_OUT)

        Return ds

    End Function

#End Region

#Region "印刷チェック"

    'Private Function SelectPrintCheck(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables(LMD110DAC.TABLE_NM_IN)

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL格納変数の初期化
    '    Me._StrSql = New StringBuilder()

    '    'SQL作成
    '    If String.IsNullOrEmpty(Me._Row.Item("SEIQTO_CD").ToString()) = True Then
    '        Me._StrSql.Append(LMD110DAC.SQL_SELECT_PRINT_CHECK)
    '    Else
    '        Me._StrSql.Append(LMD110DAC.SQL_SELECT_PRINT_CHECK_SEIQ)
    '    End If
    '    Call Me.SetPrm()

    '    'スキーマ設定
    '    Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMD110DAC", "SelectPrintCheck", cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    'DataReader→DataTableへの転記
    '    Dim map As Hashtable = New Hashtable()

    '    '取得データの格納先をマッピング
    '    map.Add("CUST_NM_L", "CUST_NM_L")
    '    map.Add("CUST_NM_M", "CUST_NM_M")
    '    map.Add("CLOSE_DATE", "CLOSE_DATE")

    '    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMD110DAC.TABLE_NM_PRINT)

    '    Return ds
    'End Function

    'Private Sub SetPrm()

    '    Me._SqlPrmList = New ArrayList()

    '    'パラメータ設定(共通）
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L").ToString(), DBDataType.CHAR))
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M").ToString(), DBDataType.CHAR))
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

    'End Sub

#End Region

#End Region 'Method

#Region "SQL"


    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="brCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' カラム名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="dtVal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetColNm(ByVal sql As String, ByVal dtVal As DataTable) As String

        Dim colNm As String = dtVal.Rows(0).Item("EDIT_ITEM_NM").ToString
        Dim temp As String = String.Empty

        If colNm = "SAGYO_NB" Then
            temp = String.Concat(colNm, " = @EDIT_ITEM_VALUE ,", " SAGYO_GK = ROUND(@EDIT_ITEM_VALUE * SAGYO_UP,0) ")
        ElseIf colNm = "SAGYO_UP" Then
            temp = String.Concat(colNm, " = @EDIT_ITEM_VALUE ,", " SAGYO_GK = ROUND(SAGYO_NB * @EDIT_ITEM_VALUE,0) ")
        Else
            temp = String.Concat(colNm, " = @EDIT_ITEM_VALUE ")

        End If

        sql = sql.Replace("$CHANGE_ITEM$", temp)
        Return sql

    End Function


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append(" FURI.SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND FURI.NRS_BR_CD  = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND FURI.WH_CD      = @WH_CD  ")
        Me._StrSql.Append(vbNewLine)

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))

        With Me._Row

            '振替日(FROM)
            whereStr = .Item("FIRIKAE_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.FURI_DATE >= @FIRIKAE_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FIRIKAE_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '振替日(TO)
            whereStr = .Item("FURIKAE_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.FURI_DATE <= @FURIKAE_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURIKAE_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '振替元荷主コード(大)
            whereStr = .Item("MOTO_CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.MOTO_CUST_CD_L LIKE @MOTO_CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '振替元荷主コード(中)
            whereStr = .Item("MOTO_CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.MOTO_CUST_CD_L LIKE @MOTO_CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '振替先荷主コード(大)
            whereStr = .Item("SAKI_CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.SAKI_CUST_CD_L LIKE @SAKI_CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '振替先荷主コード(中)
            whereStr = .Item("SAKI_CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.SAKI_CUST_CD_L LIKE @SAKI_CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '振替区分
            whereStr = .Item("FURI_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.FURI_KBN = @FURI_KBN")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_KBN", whereStr, DBDataType.CHAR))
            End If

            '容器変更
            whereStr = .Item("YOUKI_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.YOUKI_HENKO_KBN = @YOUKI_KBN")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOUKI_KBN", whereStr, DBDataType.CHAR))
            End If

            '私の作成分
            whereStr = .Item("MY_SELECT").ToString()
            If ("1").Equals(whereStr) = True Then
                whereStr = .Item("USER_ID").ToString()

                Me._StrSql.Append(" AND FURI.SYS_ENT_USER = @USER_ID")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_ID", whereStr, DBDataType.NVARCHAR))
            End If

            'オーダー番号
            whereStr = .Item("ORDER_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.MOTO_ORD_NO LIKE @ORDER_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '振替管理番号
            whereStr = .Item("FUERI_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FURI.FURI_NO LIKE  @FUERI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FUERI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '振替元名称
            whereStr = .Item("MOTO_CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (MCUST.CUST_NM_L + '　' + MCUST.CUST_NM_M) LIKE @MOTO_CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '振替先元名称
            whereStr = .Item("SAKI_CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (SCUST.CUST_NM_L + '　' + SCUST.CUST_NM_M) LIKE @SAKI_CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '振替先商品名
            whereStr = .Item("SAKI_GOODS_NM_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND　SGOODS.GOODS_NM_1 LIKE @SAKI_GOODS_NM_NRS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_GOODS_NM_NRS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub
#End Region

End Class

