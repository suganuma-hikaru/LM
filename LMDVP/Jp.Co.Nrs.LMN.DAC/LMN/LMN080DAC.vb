' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN080DAC : 欠品警告
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Base

''' <summary>
''' LMN080DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN080DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    'マスタスキーマ名
    Private Const MST_SCHEMA As String = "LM_MST.."

    'トランザクションスキーマ名
    Private Const TRN_SCHEMA As String = "LM_TRN.."

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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region

#Region "Method"

#Region "SQL"

    ''' <summary>
    '''  対象倉庫コード、今回引当出荷オーダー数取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetSOKO_CD_LIST()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  SCML.BR_CD                        AS BR_CD        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  ,SCML.SOKO_CD                     AS SOKO_CD      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,(SELECT                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        WH_NM                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("   FROM                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("        M_SOKO SOKO                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("   WHERE                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        SOKO.WH_CD = SOKO_CD                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("   AND  SOKO.SYS_DEL_FLG = '0')   AS SOKO_NM        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,COUNT(SCML.SCM_CTL_NO_L)        AS HIKIATE_ORD_NB ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L SCML                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SCM_CUST_CD = @SCM_CUST_CD                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.STATUS_KBN = '01'                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SYS_DEL_FLG = '0'                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("GROUP BY                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("BR_CD                                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",SOKO_CD                                            ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  出荷予定品目数取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetPLAN_HINMOKU_NB()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT DISTINCT                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  SCML.SOKO_CD         AS SOKO_CD        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCMM.CUST_GOODS_CD   AS GOODS_CD_CUST  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L SCML                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_M SCMM                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCMM.SCM_CTL_NO_L = SCML.SCM_CTL_NO_L    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCMM.SYS_DEL_FLG = '0'                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SCM_CUST_CD = @SCM_CUST_CD          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.STATUS_KBN = '01'                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SYS_DEL_FLG = '0'                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SOKO_CD = @SOKO_CD                  ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  明細データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetLMN080OUT_M()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  SCML.SOKO_CD          AS SOKO_CD               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN.KBN_NM1           AS CUST_NM               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCMM.CUST_GOODS_CD    AS GOODS_CD_CUST         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCMM.GOODS_NM         AS GOODS_NM              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCML.OUTKA_DATE       AS OUTKA_DATE            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,''                    AS KEPPIN_NB             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,''                    AS PLAN_ZAIKO_NB         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,''                    AS HIKIATE_NB            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCMM.OUTKA_TTL_NB     AS DETAIL_NB             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCML.DEST_NM          AS DEST_NM               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SCML.CUST_ORD_NO_L    AS CUST_ORD_NO_L         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_M SCMM                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("RIGHT JOIN                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L SCML                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON                                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SCM_CTL_NO_L = SCMM.SCM_CTL_NO_L            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SCM_CUST_CD = @SCM_CUST_CD                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SOKO_CD = @SOKO_CD                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.STATUS_KBN = '01'                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCML.SYS_DEL_FLG = '0'                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN KBN                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON                                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("KBN.KBN_GROUP_CD = 'S032'                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("KBN.KBN_NM3 = @SCM_CUST_CD                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("KBN.SYS_DEL_FLG = '0'                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCMM.SYS_DEL_FLG = '0'                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ORDER BY                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" SOKO_CD       ASC                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",CUST_NM       ASC                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",GOODS_CD_CUST ASC                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",OUTKA_DATE    ASC                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",CUST_ORD_NO_L ASC                               ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  欠品危惧チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLCheckPreKeppin()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  CUST_GOODS_CD                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnSchemaNm)
        Me._StrSql.Append("N_ZAIKO_NISSU                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SOKO_CD = @SOKO_CD               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCM_CUST_CD = @SCM_CUST_CD       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("CUST_GOODS_CD = @GOODS_CD_CUST   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ZAIKO_NISSU <= 14                ")
        Me._StrSql.Append(vbNewLine)

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 対象倉庫コード、今回引当出荷オーダー数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetSOKO_CD_LIST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN080IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLGetSOKO_CD_LIST()

        'パラメータの設定
        Call Me.SetParamGetSOKO_CD_LIST()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN080DAC", "GetSOKO_CD_LIST", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("BR_CD", "BR_CD")
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("SOKO_NM", "SOKO_NM")
        map.Add("HIKIATE_ORD_NB", "HIKIATE_ORD_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN080OUT_L")

        Return ds

    End Function

    ''' <summary>
    ''' 出荷予定品目数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetPLAN_HINMOKU_NB(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN080OUT_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLGetPLAN_HINMOKU_NB()

        'パラメータの設定
        Call Me.SetParamGetPLAN_HINMOKU_NB()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN080DAC", "GetPLAN_HINMOKU_NB", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN080OUT_M")

        Return ds

    End Function

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetLMN080OUT_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN080OUT_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLGetLMN080OUT_M()

        'パラメータの設定
        Call Me.SetParamGetLMN080OUT_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN080DAC", "GetLMN080OUT_M", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("KEPPIN_NB", "KEPPIN_NB")
        map.Add("PLAN_ZAIKO_NB", "PLAN_ZAIKO_NB")
        map.Add("HIKIATE_NB", "HIKIATE_NB")
        map.Add("DETAIL_NB", "DETAIL_NB")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("CUST_ORD_NO_L", "CUST_ORD_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN080OUT_M")

        Return ds

    End Function

    ''' <summary>
    ''' 欠品危惧チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckPreKeppin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN080OUT_M")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLCheckPreKeppin()

        'パラメータの設定、Where文構築
        Call Me.SetParamCheckPreKeppin()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN080DAC", "CheckPreKeppin", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN080OUT_L")

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' 対象倉庫コード、今回引当出荷オーダー数取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetSOKO_CD_LIST()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 出荷予定品目数取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetPLAN_HINMOKU_NB()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 明細データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetLMN080OUT_M()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 欠品危惧チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCheckPreKeppin()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.CHAR))


        End With

    End Sub

#End Region

#End Region

#End Region

#Region "スキーマ設定"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSchemaNm()

        Me._MstSchemaNm = MST_SCHEMA

        Me._TrnSchemaNm = TRN_SCHEMA

    End Sub

#End Region

End Class
