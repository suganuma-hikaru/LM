' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI260  : 引取運賃明細入力
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI260DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI260DAC
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
                                          & " $LM_TRN$..I_HIKITORI_UNCHIN_MEISAI HIKI     " & vbNewLine _
                                          & "WHERE                                        " & vbNewLine _
                                          & "    HIKI.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                          & "AND HIKI.CUST_CD_L = @CUST_CD_L              " & vbNewLine _
                                          & "AND HIKI.CUST_CD_M = @CUST_CD_M              " & vbNewLine _
                                          & "AND HIKI.HIKI_DATE = @HIKI_DATE              " & vbNewLine _
                                          & "AND HIKI.MEISAI_NO = @MEISAI_NO              " & vbNewLine _
                                          & "AND HIKI.HIN_CD = @HIN_CD                    " & vbNewLine _
                                          & "AND HIKI.DEST_CD = @HIKITORI_CD              " & vbNewLine _
                                          & "AND HIKI.SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
                                          & "AND HIKI.SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine

#End Region

#End Region

#Region "削除・復活処理"

#Region "更新処理"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UP_DEL As String = "UPDATE                                   " & vbNewLine _
                                       & "  $LM_TRN$..I_HIKITORI_UNCHIN_MEISAI     " & vbNewLine _
                                       & "SET                                      " & vbNewLine _
                                       & "  SYS_UPD_DATE = @SYS_UPD_DATE           " & vbNewLine _
                                       & ", SYS_UPD_TIME = @SYS_UPD_TIME           " & vbNewLine _
                                       & ", SYS_UPD_PGID = @SYS_UPD_PGID           " & vbNewLine _
                                       & ", SYS_UPD_USER = @SYS_UPD_USER           " & vbNewLine _
                                       & ", SYS_DEL_FLG  = @SYS_DEL_FLG            " & vbNewLine _
                                       & "WHERE                                    " & vbNewLine _
                                       & "    NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                       & "AND CUST_CD_L = @CUST_CD_L               " & vbNewLine _
                                       & "AND CUST_CD_M = @CUST_CD_M               " & vbNewLine _
                                       & "AND HIKI_DATE = @HIKI_DATE               " & vbNewLine _
                                       & "AND MEISAI_NO = @MEISAI_NO               " & vbNewLine _
                                       & "AND HIN_CD = @HIN_CD                     " & vbNewLine _
                                       & "AND DEST_CD = @HIKITORI_CD               " & vbNewLine

#End Region

#End Region

#Region "検索処理"

#Region "検索処理 SELECT句"

    ''' <summary>
    ''' 検索処理 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KENSAKU As String = "SELECT                                                                       " & vbNewLine _
                                               & "  HIKI.NRS_BR_CD                              AS NRS_BR_CD                   " & vbNewLine _
                                               & ", HIKI.CUST_CD_L                              AS CUST_CD_L                   " & vbNewLine _
                                               & ", HIKI.CUST_CD_M                              AS CUST_CD_M                   " & vbNewLine _
                                               & ", CUST.CUST_NM_L                              AS CUST_NM_L                   " & vbNewLine _
                                               & ", CUST.CUST_NM_M                              AS CUST_NM_M                   " & vbNewLine _
                                               & ", HIKI.HIKI_DATE                              AS HIKI_DATE                   " & vbNewLine _
                                               & ", HIKI.MEISAI_NO                              AS MEISAI_NO                   " & vbNewLine _
                                               & ", HIKI.HIN_CD                                 AS HIN_CD                      " & vbNewLine _
                                               & ", KBN1.KBN_NM1                                AS HIN_NM                      " & vbNewLine _
                                               & ", HIKI.DEST_CD                                AS HIKITORI_CD                 " & vbNewLine _
                                               & ", DEST.DEST_NM                                AS HIKITORI_NM                 " & vbNewLine _
                                               & ", HIKI.FC_NB                                  AS FC_NB                       " & vbNewLine _
                                               & ", HIKI.FC_TANKA                               AS FC_TANKA                    " & vbNewLine _
                                               & ", HIKI.FC_TOTAL                               AS FC_TOTAL                    " & vbNewLine _
                                               & ", HIKI.DM_NB                                  AS DM_NB                       " & vbNewLine _
                                               & ", HIKI.DM_TANKA                               AS DM_TANKA                    " & vbNewLine _
                                               & ", HIKI.DM_TOTAL                               AS DM_TOTAL                    " & vbNewLine _
                                               & ", HIKI.CNTN_KISU                              AS KISU                        " & vbNewLine _
                                               & ", HIKI.SEIHIN_GAKU                            AS SEIHIN                      " & vbNewLine _
                                               & ", HIKI.SUKURAP_GAKU                           AS SUKURAP                     " & vbNewLine _
                                               & ", HIKI.WARIMASI_GAKU                          AS WARIMASI                    " & vbNewLine _
                                               & ", HIKI.SEIKEI_GAKU                            AS SEIKEI                      " & vbNewLine _
                                               & ", HIKI.ROSEN_GAKU                             AS ROSEN                       " & vbNewLine _
                                               & ", HIKI.KOUSOKU_GAKU                           AS KOUSOKU                     " & vbNewLine _
                                               & ", HIKI.SONOTA_GAKU                            AS SONOTA                      " & vbNewLine _
                                               & ", HIKI.ALL_TOTAL                              AS ALL_TOTAL                   " & vbNewLine _
                                               & ", HIKI.REMARK                                 AS REMARK                      " & vbNewLine _
                                               & ", USER1.USER_NM                               AS SYS_ENT_USER                " & vbNewLine _
                                               & ", HIKI.SYS_ENT_DATE                           AS SYS_ENT_DATE                " & vbNewLine _
                                               & ", HIKI.SYS_ENT_TIME                           AS SYS_ENT_TIME                " & vbNewLine _
                                               & ", USER2.USER_NM                               AS SYS_UPD_USER                " & vbNewLine _
                                               & ", HIKI.SYS_UPD_DATE                           AS SYS_UPD_DATE                " & vbNewLine _
                                               & ", HIKI.SYS_UPD_TIME                           AS SYS_UPD_TIME                " & vbNewLine _
                                               & ", HIKI.SYS_DEL_FLG                            AS SYS_DEL_FLG                 " & vbNewLine _
                                               & ", KBN2.KBN_NM1                                AS SYS_DEL_NM                  " & vbNewLine
#End Region

#Region "検索処理 FROM句"

    '''' <summary>
    '''' 検索処理 FROM句
    '''' </summary>
    '''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_KENSAKU As String = "FROM                                            " & vbNewLine _
                                                    & "$LM_TRN$..I_HIKITORI_UNCHIN_MEISAI HIKI         " & vbNewLine _
                                                    & "LEFT JOIN                                       " & vbNewLine _
                                                    & "$LM_MST$..M_CUST CUST                           " & vbNewLine _
                                                    & " ON HIKI.NRS_BR_CD = CUST.NRS_BR_CD             " & vbNewLine _
                                                    & "AND HIKI.CUST_CD_L = CUST.CUST_CD_L             " & vbNewLine _
                                                    & "AND HIKI.CUST_CD_M = CUST.CUST_CD_M             " & vbNewLine _
                                                    & "AND CUST.CUST_CD_S = '00'                       " & vbNewLine _
                                                    & "AND CUST.CUST_CD_SS = '00'                      " & vbNewLine _
                                                    & "AND CUST.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                                    & "LEFT JOIN                                       " & vbNewLine _
                                                    & "$LM_MST$..M_DEST DEST                           " & vbNewLine _
                                                    & " ON HIKI.NRS_BR_CD = DEST.NRS_BR_CD             " & vbNewLine _
                                                    & "AND HIKI.CUST_CD_L = DEST.CUST_CD_L             " & vbNewLine _
                                                    & "AND HIKI.DEST_CD = DEST.DEST_CD                 " & vbNewLine _
                                                    & "AND DEST.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                                    & "LEFT JOIN                                       " & vbNewLine _
                                                    & "$LM_MST$..S_USER USER1                          " & vbNewLine _
                                                    & " ON HIKI.SYS_ENT_USER = USER1.USER_CD           " & vbNewLine _
                                                    & "AND USER1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                                    & "LEFT JOIN                                       " & vbNewLine _
                                                    & "$LM_MST$..S_USER USER2                          " & vbNewLine _
                                                    & " ON HIKI.SYS_UPD_USER = USER2.USER_CD           " & vbNewLine _
                                                    & "AND USER2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                                    & "LEFT JOIN                                       " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN KBN1                            " & vbNewLine _
                                                    & " ON KBN1.KBN_GROUP_CD = 'H019'                  " & vbNewLine _
                                                    & "AND HIKI.HIN_CD = KBN1.KBN_CD                   " & vbNewLine _
                                                    & "AND KBN1.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                                    & "LEFT JOIN                                       " & vbNewLine _
                                                    & "$LM_MST$..Z_KBN KBN2                            " & vbNewLine _
                                                    & " ON KBN2.KBN_GROUP_CD = 'S051'                  " & vbNewLine _
                                                    & "AND HIKI.SYS_DEL_FLG = KBN2.KBN_CD              " & vbNewLine

#End Region

#Region "検索処理 ORDER BY句"

    ''' <summary>
    ''' 検索処理 ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_KENSAKU As String = "ORDER BY                                       " & vbNewLine _
                                                     & "  HIKI.HIKI_DATE                               " & vbNewLine _
                                                     & ", HIKI.HIN_CD                                  " & vbNewLine _
                                                     & ", HIKI.MEISAI_NO                               " & vbNewLine

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
                                           & " $LM_TRN$..I_HIKITORI_UNCHIN_MEISAI HIKI     " & vbNewLine _
                                           & "WHERE                                        " & vbNewLine _
                                           & "    HIKI.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                           & "AND HIKI.CUST_CD_L = @CUST_CD_L              " & vbNewLine _
                                           & "AND HIKI.CUST_CD_M = @CUST_CD_M              " & vbNewLine _
                                           & "AND HIKI.HIKI_DATE = @HIKI_DATE              " & vbNewLine _
                                           & "AND HIKI.MEISAI_NO = @MEISAI_NO              " & vbNewLine _
                                           & "AND HIKI.HIN_CD = @HIN_CD                    " & vbNewLine _
                                           & "AND HIKI.DEST_CD = @HIKITORI_CD              " & vbNewLine

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DATA As String = "INSERT INTO                                              " & vbNewLine _
                                            & " $LM_TRN$..I_HIKITORI_UNCHIN_MEISAI                      " & vbNewLine _
                                            & "(                                                        " & vbNewLine _
                                            & "  NRS_BR_CD                                              " & vbNewLine _
                                            & ", CUST_CD_L                                              " & vbNewLine _
                                            & ", CUST_CD_M                                              " & vbNewLine _
                                            & ", HIKI_DATE                                              " & vbNewLine _
                                            & ", MEISAI_NO                                              " & vbNewLine _
                                            & ", HIN_CD                                                 " & vbNewLine _
                                            & ", DEST_CD                                                " & vbNewLine _
                                            & ", FC_NB                                                  " & vbNewLine _
                                            & ", FC_TANKA                                               " & vbNewLine _
                                            & ", FC_TOTAL                                               " & vbNewLine _
                                            & ", DM_NB                                                  " & vbNewLine _
                                            & ", DM_TANKA                                               " & vbNewLine _
                                            & ", DM_TOTAL                                               " & vbNewLine _
                                            & ", CNTN_KISU                                              " & vbNewLine _
                                            & ", SEIHIN_GAKU                                            " & vbNewLine _
                                            & ", SUKURAP_GAKU                                           " & vbNewLine _
                                            & ", WARIMASI_GAKU                                          " & vbNewLine _
                                            & ", SEIKEI_GAKU                                            " & vbNewLine _
                                            & ", ROSEN_GAKU                                             " & vbNewLine _
                                            & ", KOUSOKU_GAKU                                           " & vbNewLine _
                                            & ", SONOTA_GAKU                                            " & vbNewLine _
                                            & ", ALL_TOTAL                                              " & vbNewLine _
                                            & ", REMARK                                                 " & vbNewLine _
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
                                            & ", @CUST_CD_L                                             " & vbNewLine _
                                            & ", @CUST_CD_M                                             " & vbNewLine _
                                            & ", @HIKI_DATE                                             " & vbNewLine _
                                            & ", @MEISAI_NO                                             " & vbNewLine _
                                            & ", @HIN_CD                                                " & vbNewLine _
                                            & ", @HIKITORI_CD                                           " & vbNewLine _
                                            & ", @FC_NB                                                 " & vbNewLine _
                                            & ", @FC_TANKA                                              " & vbNewLine _
                                            & ", @FC_TOTAL                                              " & vbNewLine _
                                            & ", @DM_NB                                                 " & vbNewLine _
                                            & ", @DM_TANKA                                              " & vbNewLine _
                                            & ", @DM_TOTAL                                              " & vbNewLine _
                                            & ", @KISU                                                  " & vbNewLine _
                                            & ", @SEIHIN                                                " & vbNewLine _
                                            & ", @SUKURAP                                               " & vbNewLine _
                                            & ", @WARIMASI                                              " & vbNewLine _
                                            & ", @SEIKEI                                                " & vbNewLine _
                                            & ", @ROSEN                                                 " & vbNewLine _
                                            & ", @KOUSOKU                                               " & vbNewLine _
                                            & ", @SONOTA                                                " & vbNewLine _
                                            & ", @ALL_TOTAL                                             " & vbNewLine _
                                            & ", @REMARK                                                " & vbNewLine _
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
    Private Const SQL_UPDATE_DATA As String = "UPDATE                                " & vbNewLine _
                                            & "  $LM_TRN$..I_HIKITORI_UNCHIN_MEISAI  " & vbNewLine _
                                            & "SET                                   " & vbNewLine _
                                            & "  FC_NB = @FC_NB                      " & vbNewLine _
                                            & ", FC_TANKA  = @FC_TANKA               " & vbNewLine _
                                            & ", FC_TOTAL = @FC_TOTAL                " & vbNewLine _
                                            & ", DM_NB = @DM_NB                      " & vbNewLine _
                                            & ", DM_TANKA = @DM_TANKA                " & vbNewLine _
                                            & ", DM_TOTAL = @DM_TOTAL                " & vbNewLine _
                                            & ", CNTN_KISU  = @KISU                  " & vbNewLine _
                                            & ", SEIHIN_GAKU = @SEIHIN               " & vbNewLine _
                                            & ", SUKURAP_GAKU = @SUKURAP             " & vbNewLine _
                                            & ", WARIMASI_GAKU = @WARIMASI           " & vbNewLine _
                                            & ", SEIKEI_GAKU = @SEIKEI               " & vbNewLine _
                                            & ", ROSEN_GAKU  = @ROSEN                " & vbNewLine _
                                            & ", KOUSOKU_GAKU = @KOUSOKU             " & vbNewLine _
                                            & ", SONOTA_GAKU = @SONOTA               " & vbNewLine _
                                            & ", ALL_TOTAL = @ALL_TOTAL              " & vbNewLine _
                                            & ", REMARK = @REMARK                    " & vbNewLine _
                                            & ", SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
                                            & ", SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine _
                                            & ", SYS_UPD_PGID = @SYS_UPD_PGID        " & vbNewLine _
                                            & ", SYS_UPD_USER = @SYS_UPD_USER        " & vbNewLine _
                                            & "WHERE                                 " & vbNewLine _
                                            & "    NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                            & "AND CUST_CD_L = @CUST_CD_L            " & vbNewLine _
                                            & "AND CUST_CD_M = @CUST_CD_M            " & vbNewLine _
                                            & "AND HIKI_DATE = @HIKI_DATE            " & vbNewLine _
                                            & "AND MEISAI_NO = @MEISAI_NO            " & vbNewLine _
                                            & "AND HIN_CD = @HIN_CD                  " & vbNewLine _
                                            & "AND DEST_CD = @HIKITORI_CD            " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI260DAC.SQL_HAITA_CHK)

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

        MyBase.Logger.WriteSQLLog("LMI260DAC", "HaitaChk", cmd)

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
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI260DAC.SQL_UP_DEL)

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

        MyBase.Logger.WriteSQLLog("LMI260DAC", "DeleteData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMI260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI260DAC.SQL_SELECT_KENSAKU)        'SQL構築(SELECT句)
        Me._StrSql.Append(LMI260DAC.SQL_SELECT_FROM_KENSAKU)   'SQL構築(FROM句)
        Call Me.SetSQLSelectWhereKensaku()                     '条件設定(WHERE句)
        Me._StrSql.Append(LMI260DAC.SQL_SELECT_ORDER_KENSAKU)  'SQL構築(ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI260DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("HIKI_DATE", "HIKI_DATE")
        map.Add("MEISAI_NO", "MEISAI_NO")
        map.Add("HIN_CD", "HIN_CD")
        map.Add("HIN_NM", "HIN_NM")
        map.Add("HIKITORI_CD", "HIKITORI_CD")
        map.Add("HIKITORI_NM", "HIKITORI_NM")
        map.Add("FC_NB", "FC_NB")
        map.Add("FC_TANKA", "FC_TANKA")
        map.Add("FC_TOTAL", "FC_TOTAL")
        map.Add("DM_NB", "DM_NB")
        map.Add("DM_TANKA", "DM_TANKA")
        map.Add("DM_TOTAL", "DM_TOTAL")
        map.Add("KISU", "KISU")
        map.Add("SEIHIN", "SEIHIN")
        map.Add("SUKURAP", "SUKURAP")
        map.Add("WARIMASI", "WARIMASI")
        map.Add("SEIKEI", "SEIKEI")
        map.Add("ROSEN", "ROSEN")
        map.Add("KOUSOKU", "KOUSOKU")
        map.Add("SONOTA", "SONOTA")
        map.Add("ALL_TOTAL", "ALL_TOTAL")
        map.Add("REMARK", "REMARK")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI260OUT")

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
        Dim inTbl As DataTable = ds.Tables("LMI260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI260DAC.SQL_SELECT_CHK)

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

        MyBase.Logger.WriteSQLLog("LMI260DAC", "SelectInsertData", cmd)

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
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>新規登録SQLの構築・発行</remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI260DAC.SQL_INSERT_DATA)

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

        MyBase.Logger.WriteSQLLog("LMI260DAC", "InsertData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 編集登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>編集登録SQLの構築・発行</remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI260IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI260DAC.SQL_UPDATE_DATA)

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

        MyBase.Logger.WriteSQLLog("LMI260DAC", "UpdateData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKI_DATE", .Item("HIKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEISAI_NO", .Item("MEISAI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIN_CD", .Item("HIN_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKITORI_CD", .Item("HIKITORI_CD").ToString(), DBDataType.CHAR))
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

            '引取日 FROM
            whereStr = .Item("F_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.HIKI_DATE >= @F_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '引取日 TO
            whereStr = .Item("T_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.HIKI_DATE <= @T_DATE")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@T_DATE", whereStr, DBDataType.CHAR))
            End If

            '品名コード
            whereStr = .Item("HIN_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.HIN_CD = @HIN_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIN_CD", whereStr, DBDataType.CHAR))
            End If

            '引取先コード
            whereStr = .Item("HIKITORI_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.DEST_CD = @HIKITORI_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKITORI_CD", whereStr, DBDataType.CHAR))
            End If

            '引取先名称
            whereStr = .Item("HIKITORI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @HIKITORI_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKITORI_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '削除フラグ
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" HIKI.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(andstr)
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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKI_DATE", .Item("HIKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEISAI_NO", .Item("MEISAI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIN_CD", .Item("HIN_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKITORI_CD", .Item("HIKITORI_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(保存用（共通）)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSave()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKI_DATE", .Item("HIKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEISAI_NO", .Item("MEISAI_NO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIN_CD", .Item("HIN_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIKITORI_CD", .Item("HIKITORI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FC_NB", .Item("FC_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FC_TANKA", .Item("FC_TANKA").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FC_TOTAL", .Item("FC_TOTAL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DM_NB", .Item("DM_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DM_TANKA", .Item("DM_TANKA").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DM_TOTAL", .Item("DM_TOTAL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KISU", .Item("KISU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIHIN", .Item("SEIHIN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUKURAP", .Item("SUKURAP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WARIMASI", .Item("WARIMASI").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIKEI", .Item("SEIKEI").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROSEN", .Item("ROSEN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOUSOKU", .Item("KOUSOKU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SONOTA", .Item("SONOTA").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALL_TOTAL", .Item("ALL_TOTAL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))

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
