' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060DAC : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Base

''' <summary>
''' LMN060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    '在庫日数取得用（過去出荷重量取得用期間）
    Private Const PRE_DATE As Integer = 30

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
    ''' マスタ用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクション用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMNTrnSchemaNm As String

    ''' <summary>
    ''' EDI用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnEDINm As String

    ''' <summary>
    ''' EDIマスタ用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstEDINm As String

    ''' <summary>
    ''' LMSVer1のコネクション
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMS1 As SqlConnection = New SqlConnection

#End Region

#Region "Const"

    'マスタスキーマ名
    Private Const MST_SCHEMA As String = "LM_MST"

    'トランザクションスキーマ名
    Private Const TRN_SCHEMA As String = "LM_TRN"

#Region "区分マスタ"

    'ステータス「未設定」区分コード
    Private Const KbnCdMisettei As String = "00"
    'ステータス「設定済」区分コード
    Private Const KbnCdSetteiZumi As String = "01"

#End Region

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 列ヘッダデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>列ヘッダ設定内容の検索結果取得SQLの構築・発行</remarks>
    Private Function SelectColumnHdr(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.SQLColumnHdr1()                           'SQL構築
        Call Me.CreateSqlColHeader()                      'SQL構築
        Call Me.SQLColumnHdr2()                           'SQL構築

        Call Me.SetParamSelectHdr()                      'パラメータ設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN060DAC", "SelectColumnHdr", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("SOKO_NM", "SOKO_NM")
        map.Add("BR_CD", "BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN060OUT_HDR")

        Return ds

    End Function

    ''' <summary>
    ''' Detail表示用データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks> Detail表示用データの検索結果取得SQLの構築・発行</remarks>
    Private Function SelectDetail(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLDetail()

        'パラメータの設定、Where文構築
        Call Me.SetConditionMasterSQL()

        'GROUP BY句
        Call Me.SQLDetailGroupBy()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN060DAC", "SelectDetail", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("ZAIKO_NISSU", "ZAIKO_NISSU")
        map.Add("NISSU_DATE", "NISSU_DATE")
        map.Add("NISSU_TIME", "NISSU_TIME")
        map.Add("PLAN_OUTKA_NB", "PLAN_OUTKA_NB")
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("BR_CD", "BR_CD")
        map.Add("IKO_FLG", "IKO_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN060OUT")

        Return ds

    End Function

    ''' <summary>
    ''' オーダー・在庫のない倉庫は表示なしチェックボックスの値によるSQL構築
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlColHeader()

        With Me._Row

            If .Item("STOCK_UNDISP_FLG").ToString().Equals(LMConst.FLG.ON) Then
                Me._StrSql.Append("AND  SCML.STATUS_KBN IN ('01','02')")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 在庫データの検索(D_ZAI_TRS(LMSVer1))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データの検索結果取得SQLの構築・発行</remarks>
    Private Function SelectZaikoDataSver1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '******************************** LMSコネクション開始 *****************************
        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(Me._Row.Item("BR_CD").ToString())

                'SQL作成
                Call Me.SQLSelectZaiko(True)

                'パラメータ設定
                Call Me.SetParamSelectZaiko()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._StrSql.ToString(), Me._LMS1)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN060DAC", "SelectZaikoDataSver1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("ZAIKO_NB", "ZAIKO_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN060OUT_ZAIKO")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' 在庫データの検索(D_ZAI_TRS(LMSVer2))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データの検索結果取得SQLの構築・発行</remarks>
    Private Function SelectZaikoDataSver2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Call Me.SQLSelectZaiko()

        'パラメータ設定
        Call Me.SetParamSelectZaiko()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN060DAC", "SelectZaikoDataSver2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ZAIKO_NB", "ZAIKO_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN060OUT_ZAIKO")

        Return ds

    End Function

    ''' <summary>
    ''' 月間出荷重量取得(C_OUTKA_M(LMSVer1))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データの検索結果取得SQLの構築・発行</remarks>
    Private Function GetMonthOutkaNbSver1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '******************************** LMSコネクション開始 *****************************
        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(Me._Row.Item("BR_CD").ToString())

                'SQL作成
                Call Me.SQLGetMonthOutkaNb(True)

                'パラメータ設定
                Call Me.SetParamGetMonthOutkaNb()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._StrSql.ToString(), Me._LMS1)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN060DAC", "GetMonthOutkaNbSver1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("MONTH_OUTKA_NB", "MONTH_OUTKA_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN060OUT")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' 月間出荷重量取得(C_OUTKA_M(LMSVer2))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データの検索結果取得SQLの構築・発行</remarks>
    Private Function GetMonthOutkaNbSver2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Call Me.SQLGetMonthOutkaNb()

        'パラメータ設定
        Call Me.SetParamGetMonthOutkaNb()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN060DAC", "GetMonthOutkaNbSver2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("MONTH_OUTKA_NB", "MONTH_OUTKA_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN060OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 在庫日数更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateN_ZAIKO_NISSU(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN060OUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Call Me.SQLUpdateN_ZAIKO_NISSU()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdateN_ZAIKO_NISSU()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN060DAC", "UpdateN_ZAIKO_NISSU", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#Region "LMS DB OPen/Close"

    ''' <summary>
    ''' LMSVer1のOPEN
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenConnectionLMS1(ByVal brCd As String)

        Me._LMS1.ConnectionString = Me.GetConnectionLMS1(brCd)
        Me._LMS1.Open()


    End Sub

    ''' <summary>
    '''  LMSVer1のCLOSE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseConnectionLMS1()

        Me._LMS1.Close()
        Me._LMS1.Dispose()

    End Sub

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(列ヘッダ検索時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectHdr()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STOCK_UNDISP_FLG", .Item("STOCK_UNDISP_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))

            '検索条件部に入力された条件とパラメータ設定
            Dim whereStr As String = String.Empty


            whereStr = .Item("CUST_GOODS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GDS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GDS.GOODS_NM_1 LIKE @GOODS_NM_1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(月間出荷重量取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetMonthOutkaNb()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '日付設定
        Dim nowDate As String = Me.GetSystemDate()
        '30日前の日付を取得
        Dim preDate As String = Convert.ToDateTime(Date.Parse(Format(Convert.ToInt32(nowDate), "0000/00/00"))).AddDays(-PRE_DATE).ToString("yyyyMMdd")

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", nowDate, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRE_DATE", preDate, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫データ取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectZaiko()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫日数更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateN_ZAIKO_NISSU()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIKO_NISSU", .Item("ZAIKO_NISSU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

#Region "システム共通"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID() & "", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))

    End Sub

#End Region

#End Region

#Region "SQL"

    ''' <summary>
    ''' 列ヘッダデータ抽出用(前半部)
    ''' </summary>
    ''' <remarks>列ヘッダデータ抽出用(前半部)SQLの構築</remarks>
    Private Sub SQLColumnHdr1()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("     MAIN.SOKO_CD           AS SOKO_CD             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,MAIN.BR_CD             AS BR_CD               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,SOKO.WH_NM             AS SOKO_NM             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    (SELECT                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        SOKO1.WH_CD             AS SOKO_CD         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("       ,KBN.KBN_NM4               AS BR_CD         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("       ,'1'                   AS STOCK_UNDISP_FLG  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    FROM                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN KBN")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    LEFT JOIN                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_SOKO SOKO1")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ON     SOKO1.NRS_BR_CD = KBN.KBN_NM4           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND SOKO1.SYS_DEL_FLG = '0'                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE  KBN.KBN_GROUP_CD = 'S033'                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND    KBN.KBN_NM3 = @SCM_CUST_CD              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND    KBN.SYS_DEL_FLG = '0'                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    UNION ALL                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    SELECT                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        SCML.SOKO_CD          AS SOKO_CD           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("       ,SCML.BR_CD            AS BR_CD             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("       ,'0'                   AS STOCK_UNDISP_FLG  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    FROM                                           ")
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L  SCML")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    WHERE                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("           SCML.SCM_CUST_CD  = @SCM_CUST_CD        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND    SCML.SYS_DEL_FLG  = '0'                 ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 列ヘッダデータ抽出用(後半部)
    ''' </summary>
    ''' <remarks>列ヘッダデータ抽出用(後半部)SQLの構築</remarks>
    Private Sub SQLColumnHdr2()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append(") MAIN                                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    LEFT JOIN                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_SOKO SOKO                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON   SOKO.WH_CD            = MAIN.SOKO_CD               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  SOKO.SYS_DEL_FLG      = '0'                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE  (MAIN.STOCK_UNDISP_FLG = '0'                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        OR MAIN.STOCK_UNDISP_FLG = @STOCK_UNDISP_FLG)   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("GROUP BY MAIN.SOKO_CD,SOKO.WH_NM,MAIN.BR_CD")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ORDER BY MAIN.SOKO_CD                                   ")

    End Sub

    ''' <summary>
    ''' Detail表示用データ抽出用
    ''' </summary>
    ''' <remarks>Detail表示用データ抽出用SQLの構築</remarks>
    Private Sub SQLDetail()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("     GDS.GOODS_CD_CUST            AS CUST_GOODS_CD   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,GDS.GOODS_CD_NRS             AS NRS_GOODS_CD    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,GDS.GOODS_NM_1               AS GOODS_NM        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,ISNULL(NIS.ZAIKO_NISSU,0)    AS ZAIKO_NISSU     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,NIS.SYS_UPD_DATE             AS NISSU_DATE      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,NIS.SYS_UPD_TIME             AS NISSU_TIME      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,ISNULL(SUM(SCMM.OUTKA_TTL_NB),0) AS PLAN_OUTKA_NB   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,@SOKO_CD                     AS SOKO_CD         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,@BR_CD                       AS BR_CD           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,KBN2.KBN_NM4                 AS IKO_FLG         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_GOODS GDS")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN KBN1")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  KBN1.KBN_GROUP_CD = 'S033'                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND KBN1.SYS_DEL_FLG  = '0'                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND    KBN1.KBN_NM4      = GDS.NRS_BR_CD             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND    KBN1.KBN_NM5      = GDS.CUST_CD_L             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN  KBN2                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON   KBN2.KBN_GROUP_CD       = 'L001'                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  KBN2.KBN_NM3            = GDS.NRS_BR_CD         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  KBN2.SYS_DEL_FLG        = '0'                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_ZAIKO_NISSU NIS")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  NIS.SCM_CUST_CD        = KBN1.KBN_NM3            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND NIS.CUST_GOODS_CD      = GDS.GOODS_CD_CUST       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND NIS.SYS_DEL_FLG        = '0'                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L SCML")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  SCML.SCM_CUST_CD       = KBN1.KBN_NM3            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SCML.STATUS_KBN        = '01'                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SCML.SYS_DEL_FLG       = '0'                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SCML.SOKO_CD           = NIS.SOKO_CD             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_M SCMM")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON  SCMM.SCM_CTL_NO_L      = SCML.SCM_CTL_NO_L       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SCMM.CUST_GOODS_CD     = GDS.GOODS_CD_CUST       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SCMM.SYS_DEL_FLG       = '0'                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE GDS.SYS_DEL_FLG      = '0'                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND    KBN1.KBN_NM3        = @SCM_CUST_CD            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND    NIS.SOKO_CD         = @SOKO_CD                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND    GDS.NRS_BR_CD        = @BR_CD                 ")
        Me._StrSql.Append(vbNewLine)

    End Sub


    ''' <summary>
    ''' Detail表示用データ抽出用GROUPBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLDetailGroupBy()

        Me._StrSql.Append("GROUP BY                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         GDS.GOODS_CD_CUST                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,GDS.GOODS_CD_NRS                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,GDS.GOODS_NM_1                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,NIS.ZAIKO_NISSU                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,NIS.SYS_UPD_DATE                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,NIS.SYS_UPD_TIME                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,KBN2.KBN_NM4                                ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  初期表示(明細(在庫テーブル))データ抽出用
    ''' </summary>
    ''' <param name="Sver1Flg">True:LMSVer1参照、False:LMSVer2(通常)参照、</param>
    ''' <remarks>初期表示(明細(在庫テーブル))データ抽出の構築</remarks>
    Private Sub SQLSelectZaiko(Optional ByVal Sver1Flg As Boolean = False)

        'スキーマ名設定
        Call Me.SetSchemaNm(Sver1Flg)

        'SQL構築
        Me._StrSql.Append("SELECT                                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("     SUM(ISNULL(ZAI.PORA_ZAI_NB,0))   AS ZAIKO_NB     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnEDINm)
        Me._StrSql.Append("D_ZAI_TRS   ZAI                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE    ZAI.NRS_BR_CD      = @BR_CD                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND ZAI.GOODS_CD_NRS        = @NRS_GOODS_CD           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND ZAI.WH_CD               = @SOKO_CD                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND ZAI.SYS_DEL_FLG         = '0'                     ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  月間出荷重量取得
    ''' </summary>
    ''' <param name="Sver1Flg">True:LMSVer1参照、False:LMSVer2(通常)参照、</param>
    ''' <remarks></remarks>
    Private Sub SQLGetMonthOutkaNb(Optional ByVal Sver1Flg As Boolean = False)

        'スキーマ名設定
        Call Me.SetSchemaNm(Sver1Flg)

        'SQL構築
        Me._StrSql.Append("SELECT                                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" SUM(OUTM.OUTKA_TTL_NB) AS MONTH_OUTKA_NB                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnEDINm)
        Me._StrSql.Append("C_OUTKA_M OUTM                                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("RIGHT JOIN                                                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnEDINm)
        Me._StrSql.Append("C_OUTKA_L OUTL                                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON                                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTL.OUTKA_NO_L = OUTM.OUTKA_NO_L                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTL.NRS_BR_CD = @BR_CD                                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTL.WH_CD = @SOKO_CD                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("(OUTL.OUTKA_PLAN_DATE >= @PRE_DATE AND OUTL.OUTKA_PLAN_DATE <= @NOW_DATE) ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTL.CUST_CD_L = @LMS_CUST_CD                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTL.SYS_DEL_FLG = '0'                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTM.NRS_BR_CD = @BR_CD                                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTM.GOODS_CD_NRS = @NRS_GOODS_CD                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("OUTM.SYS_DEL_FLG = '0'                                                    ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  月間出荷重量取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLUpdateN_ZAIKO_NISSU()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("UPDATE                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_ZAIKO_NISSU                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SET                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ZAIKO_NISSU = @ZAIKO_NISSU      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SCM_CUST_CD = @SCM_CUST_CD       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("CUST_GOODS_CD = @CUST_GOODS_CD   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SOKO_CD = @SOKO_CD               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("SYS_DEL_FLG = '0'                ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="Sver1Flg">True:LMSVer1参照、False:LMSVer2(通常)参照、</param>
    ''' <remarks></remarks>
    Private Sub SetSchemaNm(Optional ByVal Sver1Flg As Boolean = False)

        Dim brCd As String = Me._Row.Item("BR_CD").ToString()

        Me._MstSchemaNm = String.Concat(MST_SCHEMA, "..")
        Me._LMNTrnSchemaNm = String.Concat(TRN_SCHEMA, "..")

        Me._TrnEDINm = String.Concat(Me.GetSchemaEDI(brCd), "..")
        Me._MstEDINm = String.Concat(Me.GetSchemaEDIMst(brCd), "..")

    End Sub

#End Region

#End Region

#End Region

#Region "LMNControl"

    Private Sub LMNControl()

        If _kbnDs Is Nothing = True Then

            Me.CreateKbnDataSet()

            Me.SetConnectDataSet(_kbnDs)

        End If

    End Sub


    ''' <summary>
    ''' 区分マスタ保持用
    ''' </summary>
    ''' <remarks></remarks>
    Private _kbnDs As DataSet

#Region "Const"

    Private Const COL_BR_CD As String = "COL_BR_CD"

    Private Const COL_IKO_FLG As String = "COL_IKO_FLG"

    Private Const COL_LMS_SV_NM As String = "COL_LMS_SV_NM"

    Private Const COL_LMS_SCHEMA_NM As String = "COL_LMS_SCHEMA_NM"

    Private Const COL_LMS2_SV_NM As String = "COL_LMS2_SV_NM"

    Private Const COL_LMS2_SCHEMA_NM As String = "COL_LMS2_SCHEMA_NM"

#End Region

    ''' <summary>
    ''' 区分マスタデータセット作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateKbnDataSet()

        '区分マスタ取得
        _kbnDs = New DataSet
        Dim dt As DataTable = New DataTable
        _kbnDs.Tables.Add(dt)
        _kbnDs.Tables(0).TableName = "Z_KBN"

        For i As Integer = 0 To 17
            _kbnDs.Tables("Z_KBN").Columns.Add(SetCol(i))
        Next

    End Sub

    ''' <summary>
    ''' カラム作成
    ''' </summary>
    ''' <param name="colno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCol(ByVal colno As Integer) As DataColumn
        Dim col As DataColumn = New DataColumn
        Dim colname As String = String.Empty
        col = New DataColumn
        Select Case colno
            Case 0
                colname = "KBN_GROUP_CD"
            Case 1
                colname = "KBN_CD"
            Case 2
                colname = "KBN_KEYWORD"
            Case 3 'KBN_NM1
                colname = "KBN_NM1"
            Case 4 'KBN_NM2
                colname = "KBN_NM2"
            Case 5 'KBN_NM3
                colname = "KBN_NM3"
            Case 6 'KBN_NM4
                colname = "KBN_NM4"
            Case 7 'KBN_NM5
                colname = "KBN_NM5"
            Case 8 'KBN_NM6
                colname = "KBN_NM6"
            Case 9 'KBN_NM7
                colname = "KBN_NM7"
            Case 10 'KBN_NM8
                colname = "KBN_NM8"
            Case 11 'KBN_NM9
                colname = "KBN_NM9"
            Case 12 'KBN_NM10
                colname = "KBN_NM10"
            Case 13
                colname = "VALUE1"
            Case 14
                colname = "VALUE2"
            Case 15
                colname = "VALUE3"
            Case 16
                colname = "SORT"
            Case 17
                colname = "REM"
        End Select

        col.ColumnName = colname
        col.Caption = colname

        Return col
    End Function


    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Friend Function GetSchemaEDI(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = dataRows(0).Item("KBN_NM6").ToString

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Friend Function GetSchemaEDIMst(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = Me._MstSchemaNm.Replace("..", "")

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' LMSVer1の接続文字列取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetConnectionLMS1(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")

        Dim DBName As String = String.Empty
        Dim loginSchemaNM As String = String.Empty
        Dim userId As String = "sa"
        Dim pass As String = "as"

        DBName = dataRows(0).Item("KBN_NM7").ToString
        loginSchemaNM = dataRows(0).Item("KBN_NM8").ToString

        Return String.Concat("Data Source=", DBName, ";Initial Catalog=", loginSchemaNM, ";Persist Security Info=True;User ID=", userId, ";Password=", pass)

    End Function


    ''' <summary>
    ''' 区分マスタの接続情報を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetConnectDataSet(ByVal ds As DataSet)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetConnection()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        MyBase.Logger.WriteSQLLog("LMNControlDAC", "SQLGetConnection", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_KEYWORD", "KBN_KEYWORD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")
        map.Add("KBN_NM5", "KBN_NM5")
        map.Add("KBN_NM6", "KBN_NM6")
        map.Add("KBN_NM7", "KBN_NM7")
        map.Add("KBN_NM8", "KBN_NM8")
        map.Add("KBN_NM9", "KBN_NM9")
        map.Add("KBN_NM10", "KBN_NM10")
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "Z_KBN")


    End Sub

    ''' <summary>
    '''区分マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetConnection()

        Me._StrSql.Append("SELECT       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  KBN_GROUP_CD	AS	KBN_GROUP_CD")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_CD		    AS	KBN_CD")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_KEYWORD	AS	KBN_KEYWORD")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM1		AS	KBN_NM1")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM2		AS	KBN_NM2")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM3		AS	KBN_NM3")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM4		AS	KBN_NM4")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM5		AS	KBN_NM5")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM6		AS	KBN_NM6")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM7		AS	KBN_NM7")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM8		AS	KBN_NM8")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM9		AS	KBN_NM9")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,KBN_NM10		AS	KBN_NM10")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,VALUE1		    AS	VALUE1")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,VALUE2	       	AS	VALUE2")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,VALUE3	    	AS	VALUE3")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,SORT	    	AS	SORT")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ,REM	    	AS	REM")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN KBN       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" KBN.SYS_DEL_FLG = '0'       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND KBN.KBN_GROUP_CD ='L001'       ")


    End Sub


#End Region

End Class
