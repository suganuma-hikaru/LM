' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400  : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI400DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI400DAC
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
                                          & " $LM_TRN$..M_SET_GOODS_LNZ GOODS_SET         " & vbNewLine _
                                          & "WHERE                                        " & vbNewLine _
                                          & "    GOODS_SET.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                          & "AND GOODS_SET.OYA_CD = @OYA_CD               " & vbNewLine _
                                          & "AND GOODS_SET.SYS_UPD_DATE = @SYS_UPD_DATE   " & vbNewLine _
                                          & "AND GOODS_SET.SYS_UPD_TIME = @SYS_UPD_TIME   " & vbNewLine

#End Region

#End Region

#Region "削除・復活処理"

#Region "データ抽出"

    ''' <summary>
    ''' データ抽出
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_SELECT As String = "SELECT                         " & vbNewLine _
                                           & "  NRS_BR_CD AS NRS_BR_CD       " & vbNewLine _
                                           & ", OYA_CD    AS OYA_CD          " & vbNewLine _
                                           & ", KO_CD     AS KO_CD           " & vbNewLine _
                                           & "FROM                           " & vbNewLine _
                                           & "    $LM_TRN$..M_SET_GOODS_LNZ  " & vbNewLine _
                                           & "WHERE                          " & vbNewLine _
                                           & "    NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
                                           & "AND OYA_CD = @OYA_CD           " & vbNewLine _
                                           & "ORDER BY                       " & vbNewLine _
                                           & "    KO_CD                      " & vbNewLine


#End Region

#Region "更新処理"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UP_DEL As String = "UPDATE                                   " & vbNewLine _
                                       & "  $LM_TRN$..M_SET_GOODS_LNZ              " & vbNewLine _
                                       & "SET                                      " & vbNewLine _
                                       & "  SYS_UPD_DATE = @SYS_UPD_DATE           " & vbNewLine _
                                       & ", SYS_UPD_TIME = @SYS_UPD_TIME           " & vbNewLine _
                                       & ", SYS_UPD_PGID = @SYS_UPD_PGID           " & vbNewLine _
                                       & ", SYS_UPD_USER = @SYS_UPD_USER           " & vbNewLine _
                                       & ", SYS_DEL_FLG  = @SYS_DEL_FLG            " & vbNewLine _
                                       & "WHERE                                    " & vbNewLine _
                                       & "    NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                       & "AND OYA_CD = @OYA_CD                     " & vbNewLine _
                                       & "AND KO_CD = @KO_CD                       " & vbNewLine

#End Region

#End Region

#Region "検索処理"

#Region "検索処理 SELECT句"

    ''' <summary>
    ''' 検索処理 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT                                   " & vbNewLine _
                                               & "  GOODS_SET.NRS_BR_CD    AS NRS_BR_CD    " & vbNewLine _
                                               & ", NRS_BR.NRS_BR_NM       AS NRS_BR_NM    " & vbNewLine _
                                               & ", GOODS.CUST_CD_L        AS CUST_CD_L    " & vbNewLine _
                                               & ", CUST.CUST_NM_L         AS CUST_NM_L    " & vbNewLine _
                                               & ", GOODS_SET.OYA_CD       AS OYA_CD       " & vbNewLine _
                                               & ", GOODS_SET.OYA_NM       AS OYA_NM       " & vbNewLine _
                                               & ", GOODS_SET.KO_CD        AS KO_CD        " & vbNewLine _
                                               & ", GOODS.GOODS_NM_1       AS GOODS_NM_1   " & vbNewLine _
                                               & ", GOODS_SET.SET_KOSU     AS SET_KOSU     " & vbNewLine _
                                               & ", GOODS_SET.SYS_ENT_USER AS SYS_ENT_USER " & vbNewLine _
                                               & ", GOODS_SET.SYS_ENT_DATE AS SYS_ENT_DATE " & vbNewLine _
                                               & ", GOODS_SET.SYS_ENT_TIME AS SYS_ENT_TIME " & vbNewLine _
                                               & ", GOODS_SET.SYS_UPD_USER AS SYS_UPD_USER " & vbNewLine _
                                               & ", GOODS_SET.SYS_UPD_DATE AS SYS_UPD_DATE " & vbNewLine _
                                               & ", GOODS_SET.SYS_UPD_TIME AS SYS_UPD_TIME " & vbNewLine _
                                               & ", GOODS_SET.SYS_DEL_FLG  AS SYS_DEL_FLG  " & vbNewLine _
                                               & ", KBN.KBN_NM1            AS SYS_DEL_NM   " & vbNewLine


#End Region

#Region "検索処理 FROM句"

    ''' <summary>
    ''' 検索処理 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                       " & vbNewLine _
                                                    & "    $LM_TRN$..M_SET_GOODS_LNZ GOODS_SET    " & vbNewLine _
                                                    & "LEFT JOIN                                  " & vbNewLine _
                                                    & "    $LM_MST$..M_NRS_BR NRS_BR              " & vbNewLine _
                                                    & " ON GOODS_SET.NRS_BR_CD = NRS_BR.NRS_BR_CD " & vbNewLine _
                                                    & "LEFT JOIN                                  " & vbNewLine _
                                                    & "    $LM_MST$..M_GOODS GOODS                " & vbNewLine _
                                                    & " ON GOODS_SET.NRS_BR_CD = GOODS.NRS_BR_CD  " & vbNewLine _
                                                    & "AND GOODS.CUST_CD_L = '00182'              " & vbNewLine _
                                                    & "AND GOODS.CUST_CD_M = '00'                 " & vbNewLine _
                                                    & "AND GOODS_SET.KO_CD = GOODS.GOODS_CD_CUST  " & vbNewLine _
                                                    & "AND GOODS.SYS_DEL_FLG = '0'                " & vbNewLine _
                                                    & "LEFT JOIN                                  " & vbNewLine _
                                                    & "    $LM_MST$..M_CUST CUST                  " & vbNewLine _
                                                    & " ON GOODS.NRS_BR_CD = CUST.NRS_BR_CD       " & vbNewLine _
                                                    & "AND GOODS.CUST_CD_L = CUST.CUST_CD_L       " & vbNewLine _
                                                    & "AND GOODS.CUST_CD_M = CUST.CUST_CD_M       " & vbNewLine _
                                                    & "AND GOODS.CUST_CD_S = CUST.CUST_CD_S       " & vbNewLine _
                                                    & "AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS     " & vbNewLine _
                                                    & "AND CUST.SYS_DEL_FLG = '0'                 " & vbNewLine _
                                                    & "LEFT JOIN                                  " & vbNewLine _
                                                    & "    $LM_MST$..Z_KBN KBN                    " & vbNewLine _
                                                    & " ON KBN.KBN_GROUP_CD = 'S051'              " & vbNewLine _
                                                    & "AND GOODS_SET.SYS_DEL_FLG = KBN.KBN_CD     " & vbNewLine _
                                                    & "AND KBN.SYS_DEL_FLG = '0'                  " & vbNewLine


#End Region

#Region "検索処理 ORDER BY句"

    ''' <summary>
    ''' 検索処理 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENSAKU As String = "ORDER BY                                 " & vbNewLine _
                                                     & "  GOODS_SET.OYA_CD                       " & vbNewLine _
                                                     & ", GOODS_SET.KO_CD                        " & vbNewLine


#End Region

#End Region

#Region "保存処理"

#Region "存在チェック"

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CHK As String = "SELECT                                       " & vbNewLine _
                                           & " COUNT(*)       AS SELECT_CNT                " & vbNewLine _
                                           & "FROM                                         " & vbNewLine _
                                           & " $LM_TRN$..M_SET_GOODS_LNZ GOODS_SET         " & vbNewLine _
                                           & "WHERE                                        " & vbNewLine _
                                           & "    GOODS_SET.NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                           & "AND GOODS_SET.OYA_CD = @OYA_CD               " & vbNewLine _
                                           & "AND GOODS_SET.SYS_DEL_FLG = '0'              " & vbNewLine

    ''' <summary>
    ''' 存在チェック(商品マスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODS_CHK As String = "SELECT                                       " & vbNewLine _
                                                 & " COUNT(*)       AS SELECT_CNT                " & vbNewLine _
                                                 & "FROM                                         " & vbNewLine _
                                                 & " $LM_MST$..M_GOODS GOODS                     " & vbNewLine _
                                                 & "WHERE                                        " & vbNewLine _
                                                 & "    GOODS.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                                 & "AND GOODS.CUST_CD_L = '00182'                " & vbNewLine _
                                                 & "AND GOODS.CUST_CD_M = '00'                   " & vbNewLine _
                                                 & "AND GOODS.GOODS_CD_CUST = @KO_CD             " & vbNewLine _
                                                 & "AND GOODS.SYS_DEL_FLG = '0'                  " & vbNewLine

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DATA As String = "INSERT INTO                                              " & vbNewLine _
                                            & " $LM_TRN$..M_SET_GOODS_LNZ                               " & vbNewLine _
                                            & "(                                                        " & vbNewLine _
                                            & "  NRS_BR_CD                                              " & vbNewLine _
                                            & ", OYA_CD                                                 " & vbNewLine _
                                            & ", OYA_NM                                                 " & vbNewLine _
                                            & ", KO_CD                                                  " & vbNewLine _
                                            & ", SAP_CODE                                               " & vbNewLine _
                                            & ", SET_KOSU                                               " & vbNewLine _
                                            & ", SYS_ENT_DATE                                           " & vbNewLine _
                                            & ", SYS_ENT_TIME                                           " & vbNewLine _
                                            & ", SYS_ENT_PGID                                           " & vbNewLine _
                                            & ", SYS_ENT_USER                                           " & vbNewLine _
                                            & ", SYS_UPD_DATE                                           " & vbNewLine _
                                            & ", SYS_UPD_TIME                                           " & vbNewLine _
                                            & ", SYS_UPD_PGID                                           " & vbNewLine _
                                            & ", SYS_UPD_USER                                           " & vbNewLine _
                                            & ", SYS_DEL_FLG                                            " & vbNewLine _
                                            & ")VALUES(                                                 " & vbNewLine _
                                            & "  @NRS_BR_CD                                             " & vbNewLine _
                                            & ", @OYA_CD                                                " & vbNewLine _
                                            & ", @OYA_NM                                                " & vbNewLine _
                                            & ", @KO_CD                                                 " & vbNewLine _
                                            & ", ''                                                     " & vbNewLine _
                                            & ", @SET_KOSU                                              " & vbNewLine _
                                            & ", @SYS_ENT_DATE                                          " & vbNewLine _
                                            & ", @SYS_ENT_TIME                                          " & vbNewLine _
                                            & ", @SYS_ENT_PGID                                          " & vbNewLine _
                                            & ", @SYS_ENT_USER                                          " & vbNewLine _
                                            & ", @SYS_UPD_DATE                                          " & vbNewLine _
                                            & ", @SYS_UPD_TIME                                          " & vbNewLine _
                                            & ", @SYS_UPD_PGID                                          " & vbNewLine _
                                            & ", @SYS_UPD_USER                                          " & vbNewLine _
                                            & ", @SYS_DEL_FLG                                           " & vbNewLine _
                                            & ")                                                        " & vbNewLine

#End Region

#Region "編集登録"

    ''' <summary>
    ''' 編集登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_DATA As String = "DELETE                                " & vbNewLine _
                                         & "FROM                                  " & vbNewLine _
                                         & "    $LM_TRN$..M_SET_GOODS_LNZ         " & vbNewLine _
                                         & "WHERE                                 " & vbNewLine _
                                         & "    NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                         & "AND OYA_CD = @OYA_CD                  " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_HAITA_CHK)

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

        MyBase.Logger.WriteSQLLog("LMI400DAC", "HaitaChk", cmd)

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

#Region "削除・復活処理"

    ''' <summary>
    ''' 削除・復活用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_CHK_SELECT)        'SQL構築
        Call Me.SetParamUpDel()                            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI400DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OYA_CD", "OYA_CD")
        map.Add("KO_CD", "KO_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI400OUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI400OUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_UP_DEL)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ設定
        Call Me.SetParamUpDel()
        Call Me.SetParamCommonSystemUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI400DAC", "DeleteData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
        End If

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
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_SELECT_KENSAKU)        'SQL構築(SELECT句)
        Me._StrSql.Append(LMI400DAC.SQL_SELECT_FROM_KENSAKU)   'SQL構築(FROM句)
        Call Me.SetSQLSelectWhereKensaku()                     '条件設定(WHERE句)
        Me._StrSql.Append(LMI400DAC.SQL_SELECT_ORDER_KENSAKU)  'SQL構築(ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI400DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("OYA_CD", "OYA_CD")
        map.Add("OYA_NM", "OYA_NM")
        map.Add("KO_CD", "KO_CD")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("SET_KOSU", "SET_KOSU")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI400OUT")

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
    Private Function SelectInsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_SELECT_CHK)

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

        MyBase.Logger.WriteSQLLog("LMI400DAC", "SelectInsertData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        reader.Read()

        '処理件数の設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 存在チェック(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_SELECT_GOODS_CHK)

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

        MyBase.Logger.WriteSQLLog("LMI400DAC", "ExistChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        reader.Read()

        '処理件数の設定
        If Convert.ToInt32(reader("SELECT_CNT")) = 0 Then
            MyBase.SetMessage("E010")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>新規登録SQLの構築・発行</remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_INSERT_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamSave()
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI400DAC", "InsertData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>物理削除SQLの構築・発行</remarks>
    Private Function DelData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI400IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI400DAC.SQL_DEL_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamSave()
        Call Me.SetParamCommonSystemUpd()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI400DAC", "DelData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_CD", .Item("OYA_CD").ToString(), DBDataType.NVARCHAR))
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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_CD", .Item("OYA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KO_CD", .Item("KO_CD").ToString(), DBDataType.NVARCHAR))
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
                andstr.Append(" GOODS_SET.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '親コード
            whereStr = .Item("OYA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS_SET.OYA_CD LIKE @OYA_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '親名称
            whereStr = .Item("OYA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS_SET.OYA_NM LIKE @OYA_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '削除フラグ
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS_SET.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            Me._StrSql.Append("WHERE")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(andstr)
            Me._StrSql.Append(vbNewLine)

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_CD", .Item("OYA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KO_CD", .Item("KO_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(保存用（共通）)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSave()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_CD", .Item("OYA_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_NM", .Item("OYA_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KO_CD", .Item("KO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KOSU", .Item("SET_KOSU").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "パラメータ設定 共通"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(新規時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

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
