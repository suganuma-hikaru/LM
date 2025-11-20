' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020DAC : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Base

''' <summary>
''' LMN020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

    ''' <summary>
    ''' 排他チェック用SQL(共通部分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_COMMON As String = " AND SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
                                             & " AND SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine

    ''' <summary>
    ''' 論理削除SQL(共通部分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_COMMON As String = "              SYS_DEL_FLG  = @SYS_DEL_FLG    " & vbNewLine _
                                               & "             ,SYS_UPD_DATE = @SYS_UPD_DATE   " & vbNewLine _
                                               & "             ,SYS_UPD_TIME = @SYS_UPD_TIME   " & vbNewLine _
                                               & "             ,SYS_UPD_PGID = @SYS_UPD_PGID   " & vbNewLine _
                                               & "             ,SYS_UPD_USER = @SYS_UPD_USER   " & vbNewLine

    ''' <summary>
    ''' 初期化SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_UPD_INIT As String = String.Empty

    Private Sub SQLUpdInit()
        'スキーマ名設定
        Call Me.SetSchemaNm()

        Dim SQL As String = "              STATUS_KBN   = '00'                                 " & vbNewLine _
                          & "             ,BR_CD        = (SELECT                              " & vbNewLine _
                          & "                                   NRS_BR_CD                      " & vbNewLine _
                          & "                              FROM " & Me._MstSchemaNm & "M_SOKO  " & vbNewLine _
                          & "                              WHERE                               " & vbNewLine _
                          & "                                   WH_CD = @WH_CD                 " & vbNewLine _
                          & "                              AND  SYS_DEL_FLG = '0')             " & vbNewLine _
                          & "             ,SOKO_CD      = @WH_CD                               " & vbNewLine _
                          & "             ,OUTKA_DATE   = ''                                   " & vbNewLine _
                          & "             ,ARR_DATE     = ''                                   " & vbNewLine _
                          & "             ,SYS_UPD_DATE = @SYS_UPD_DATE                        " & vbNewLine _
                          & "             ,SYS_UPD_TIME = @SYS_UPD_TIME                        " & vbNewLine _
                          & "             ,SYS_UPD_PGID = @SYS_UPD_PGID                        " & vbNewLine _
                          & "             ,SYS_UPD_USER = @SYS_UPD_USER                        " & vbNewLine

        SQL_UPD_INIT = SQL

    End Sub

    ''' <summary>
    ''' 更新SQL Where句(N_OUTKASCM_L/N_OUTKASCM_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_WHERE_L As String = "WHERE   SCM_CTL_NO_L  = @SCM_CTL_NO_L        " & vbNewLine


    ''' <summary>
    ''' 更新SQL Where句(N_OUTKASCM_HED_BP/N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_WHERE_HED As String = " WHERE CRT_DATE   = @CRT_DATE              " & vbNewLine _
                                                 & " AND   FILE_NAME  = @FILE_NAME             " & vbNewLine _
                                                 & " AND   REC_NO     = @REC_NO                " & vbNewLine

#End Region

    'マスタスキーマ名
    Private Const MST_SCHEMA As String = "LM_MST"

    'トランザクションスキーマ名
    Private Const TRN_SCHEMA As String = "LM_TRN"


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

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' ヘッダ部検索(N_OUTKASCM_L / N_OUTKASCM_HED_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ヘッダ表示項目の検索結果取得SQLの構築・発行</remarks>
    Private Function SelectHeaderData(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.SQLDataHdr()                                  'SQL構築
        Call Me.SetParamSelectHdr()                           'パラメータ設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectHeaderData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("STATUS_KBN", "STATUS_KBN")
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")
        map.Add("CUST_ORD_NO_L", "CUST_ORD_NO_L")
        map.Add("MOUSHIOKURI_KBN", "MOUSHIOKURI_KBN")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("ARR_DATE", "ARR_DATE")
        map.Add("EDI_DATE", "EDI_DATE")
        map.Add("EDI_TIME", "EDI_TIME")
        map.Add("EDI_DATETIME", "EDI_DATETIME")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("REMARK", "REMARK")
        map.Add("INSERT_FLG", "INSERT_FLG")
        map.Add("HED_BP_SYS_UPD_DATE", "HED_BP_SYS_UPD_DATE")
        map.Add("HED_BP_SYS_UPD_TIME", "HED_BP_SYS_UPD_TIME")
        map.Add("L_SYS_UPD_DATE", "L_SYS_UPD_DATE")
        map.Add("L_SYS_UPD_TIME", "L_SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN020OUT_L")

        Return ds

    End Function

    ''' <summary>
    ''' 明細部検索(N_OUTKASCM_M / N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>明細表示項目の検索結果取得SQLの構築・発行</remarks>
    Private Function SelectDetailData(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.SQLDataDtl()                                  'SQL構築
        Call Me.SetParamSelectDtl()                           'パラメータ設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectDetailData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("BIKO_DTL", "BIKO_DTL")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN020OUT_M")

        Return ds

    End Function

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
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

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

                MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectZaikoDataSver1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN020OUT_ZAIKO")

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
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

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

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectZaikoDataSver2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN020OUT_ZAIKO")

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
    ''' パラメータ設定モジュール(ヘッダ表示項目検索時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectHdr()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INSERT_FLG", .Item("INSERT_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(明細表示項目検索時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectDtl()

        With Me._Row

            'パラメータ設定
            Call Me.SetParamSelectHdr()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(在庫情報検索時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectZaiko()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "削除処理"

#Region "チェック"

    ''' <summary>
    ''' N_OUTKASCM_Lの排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_L検索結果取得SQLの構築・発行</remarks>
    Private Function SelectOutkaScmL(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLHaitaScmL()                           'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L)  'Where句
        Me._StrSql.Append(LMN020DAC.SQL_HAITA_COMMON)    '排他共通

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaita_LChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectOutkaScmL", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_HED_BPの排他チェック(N_OUTKASCM_Lと紐づくデータをチェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_HED_BP検索結果取得SQLの構築・発行</remarks>
    Private Function SelectOutkaScmHedBp_L(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLHaitaScmHed()                           'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L)    'Where句
        Me._StrSql.Append(LMN020DAC.SQL_HAITA_COMMON)      '排他共通

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaita_Hed_LChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectOutkaScmHed_L", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_HED_BPの排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_HED_BP検索結果取得SQLの構築・発行</remarks>
    Private Function SelectOutkaScmHedBp(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLHaitaScmHed()                           'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_HED)  'Where句
        Me._StrSql.Append(LMN020DAC.SQL_HAITA_COMMON)      '排他共通

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaita_HedChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectOutkaScmHed", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫側DBの対象データの削除状態チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>H_OUTKAEDI_L検索結果取得SQLの構築・発行</remarks>
    Private Function SelectHOutkaEdiL(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLDelChkEdiL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteChkEdiLChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "SelectHOutkaEdiL", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "論理削除"

    ''' <summary>
    ''' N_OUTKASCM_L論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_L論理削除SQLの構築・発行</remarks>
    Private Function DeleteOutkaScmL(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateL()        'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPD_DEL_COMMON)  '論理削除共通
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L)  'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel_L()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmL", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_M論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_M論理削除SQLの構築・発行</remarks>
    Private Function DeleteOutkaScmM(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateM()                             'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPD_DEL_COMMON)  '論理削除共通
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L)  'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel_L()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmM", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_HED_BP論理削除(N_OUTKASCM_Lと紐づくデータを削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_HED_BP論理削除SQLの構築・発行</remarks>
    Private Function DeleteOutkaScmHedBp_L(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateHed()                             'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPD_DEL_COMMON)    '論理削除共通
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L)    'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel_L()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmHedBp_L", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_DTL_BP論理削除(N_OUTKASCM_Lと紐づくデータを削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_DTL_BP論理削除SQLの構築・発行</remarks>
    Private Function DeleteOutkaScmDtlBp_L(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateDtl()                             'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPD_DEL_COMMON)    '論理削除共通
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L)    'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel_L()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmDtlBp_L", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_HED_BP論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_HED_BP論理削除SQLの構築・発行</remarks>
    Private Function DeleteOutkaScmHedBp(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateHed()                             'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPD_DEL_COMMON)    '論理削除共通
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_HED)  'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel_Hed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmHedBp", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' N_OUTKASCM_DTL_BP論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_DTL_BP論理削除SQLの構築・発行</remarks>
    Private Function DeleteOutkaScmDtlBp(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateDtl()                             'Select句
        Me._StrSql.Append(LMN020DAC.SQL_UPD_DEL_COMMON)    '論理削除共通
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_HED)  'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamDel_Hed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmDtlBp", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(排他処理(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita_LChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("L_SYS_UPD_DATE").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("L_SYS_UPD_TIME").ToString, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他処理(N_OUTKASCM_HED_BP)　N_OUTKASCM_Lと紐づくデータの排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita_Hed_LChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_BP_SYS_UPD_DATE").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_BP_SYS_UPD_TIME").ToString, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他処理(N_OUTKASCM_HED_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita_HedChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_BP_SYS_UPD_DATE").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_BP_SYS_UPD_TIME").ToString, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(倉庫側DBの対象データの削除状態チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteChkEdiLChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(論理削除処理(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDel_L()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.ON, DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(論理削除処理(N_OUTKASCM_HED_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDel_Hed()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.ON, DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目)
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

#Region "SQL"

    ''' <summary>
    '''  初期表示(ヘッダ)データ抽出用
    ''' </summary>
    ''' <remarks>初期表示(ヘッダ)データ抽出用SQLの構築</remarks>
    Private Sub SQLDataHdr()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("Select                                                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("     SOKO_CD                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,WH_CD                                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,STATUS_KBN                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,SCM_CUST_CD                                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,CUST_ORD_NO_L                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,MOUSHIOKURI_KBN                                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,OUTKA_DATE                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,ARR_DATE                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,EDI_DATE                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,EDI_TIME                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,EDI_DATETIME                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,DEST_NM                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,DEST_ZIP                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,DEST_AD                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,DEST_TEL                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,REMARK                                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,INSERT_FLG                                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,HED_BP_SYS_UPD_DATE                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,HED_BP_SYS_UPD_TIME                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,L_SYS_UPD_DATE                                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    ,L_SYS_UPD_TIME                                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        (                                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        SELECT                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             SCML.SOKO_CD                     AS SOKO_CD              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,DSK.WH_CD                        AS WH_CD                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.STATUS_KBN                  AS STATUS_KBN           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.SCM_CUST_CD                 AS SCM_CUST_CD          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.CUST_ORD_NO_L               AS CUST_ORD_NO_L        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.MOUSHIOKURI_KBN             AS MOUSHIOKURI_KBN      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.OUTKA_DATE                  AS OUTKA_DATE           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.ARR_DATE                    AS ARR_DATE             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.EDI_DATE                    AS EDI_DATE             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SUBSTRING(SCML.EDI_TIME,1,2) + ':'                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            + SUBSTRING(SCML.EDI_TIME,3,2) + ':'                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            + SUBSTRING(SCML.EDI_TIME,5,2)    AS EDI_TIME             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,''                               AS EDI_DATETIME         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.DEST_NM                     AS DEST_NM              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.DEST_ZIP                    AS DEST_ZIP             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.DEST_AD                     AS DEST_AD              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.DEST_TEL                    AS DEST_TEL             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.REMARK1 + SCML.REMARK2      AS REMARK               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,'0'                              AS INSERT_FLG           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.SYS_UPD_DATE                AS HED_BP_SYS_UPD_DATE  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.SYS_UPD_TIME                AS HED_BP_SYS_UPD_TIME  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.SYS_UPD_DATE                AS L_SYS_UPD_DATE       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCML.SYS_UPD_TIME                AS L_SYS_UPD_TIME       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        FROM                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L SCML")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        LEFT JOIN                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_HED_BP SCMH")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ON  SCMH.SCM_CTL_NO_L = SCML.SCM_CTL_NO_L                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND SCMH.SYS_DEL_FLG = '0'                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        LEFT JOIN                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_DEFAULT_SOKO DSK")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ON   DSK.SCM_CUST_CD  = SCML.SCM_CUST_CD                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  DSK.JIS_CD       = (SELECT                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                     MAX(JIS_CD)                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                 FROM                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("                                     M_ZIP DSK")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                 WHERE ZIP_NO = SCML.DEST_ZIP         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                 AND   SYS_DEL_FLG = '0')             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  DSK.SYS_DEL_FLG  = '0'                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        WHERE                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             SCML.SYS_DEL_FLG  = '0'                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  SCML.SCM_CTL_NO_L = @SCM_CTL_NO_L                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("UNION ALL                                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        SELECT                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             DSK.WH_CD                        AS SOKO_CD              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,''                               AS WH_CD                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,'00'                             AS STATUS_KBN           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,@SCM_CUST_CD                     AS SCM_CUST_CD          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.DENPYO_NO                   AS CUST_ORD_NO_L        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,ISNULL((SELECT                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                   KBN_CD                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("              FROM                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("                   Z_KBN                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("              WHERE                                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                   KBN_GROUP_CD = 'M008'                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("              AND                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                   KBN_NM1 = SCMH.MOSIOKURI_KB                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("              AND                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                   SYS_DEL_FLG = '0'),'')     AS MOUSHIOKURI_KBN      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.OUTKA_PLAN_DATE             AS OUTKA_DATE           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.ARR_PLAN_DATE               AS ARR_DATE             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.EDI_DATE                    AS EDI_DATE             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.EDI_TIME                    AS EDI_TIME             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,''                               AS EDI_DATETIME         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.DEST_NM1 + SCMH.DEST_NM2    AS DEST_NM              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.DEST_ZIP                    AS DEST_ZIP             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.DEST_AD1 + SCMH.DEST_AD2    AS DEST_AD              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.DEST_TEL                    AS DEST_TEL             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.BIKO_HED1 + SCMH.BIKO_HED2  AS REMARK               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,'1'                              AS INSERT_FLG           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.SYS_UPD_DATE                AS HED_BP_SYS_UPD_DATE  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,SCMH.SYS_UPD_TIME                AS HED_BP_SYS_UPD_TIME  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,''                               AS L_SYS_UPD_DATE       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("            ,''                               AS L_SYS_UPD_TIME       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        FROM                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_HED_BP SCMH                                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        LEFT JOIN                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN KBN1                                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ON   KBN1.KBN_GROUP_CD = 'S033'                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  KBN1.KBN_NM5      = SCMH.CUST_CD_L                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  KBN1.SYS_DEL_FLG  = '0'                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        LEFT JOIN                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_DEFAULT_SOKO DSK                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ON   DSK.SCM_CUST_CD  = @SCM_CUST_CD                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  DSK.JIS_CD       = (SELECT                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                     MAX(JIS_CD)                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                 FROM                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("                                     M_ZIP                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                 WHERE ZIP_NO = SCMH.DEST_ZIP         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("                                 AND   SYS_DEL_FLG = '0')             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  DSK.SYS_DEL_FLG  = '0'                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        WHERE                                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             SCMH.SYS_DEL_FLG  = '0'                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  SCMH.CRT_DATE     = @CRT_DATE                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  SCMH.FILE_NAME    = @FILE_NAME                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        AND  SCMH.REC_NO       = @REC_NO                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ) MAIN                                                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE INSERT_FLG = @INSERT_FLG                                        ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  初期表示(明細)データ抽出用
    ''' </summary>
    ''' <remarks>初期表示(明細)データ抽出用SQLの構築</remarks>
    Private Sub SQLDataDtl()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("          CUST_GOODS_CD                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         ,GOODS_NM                                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         ,OUTKA_TTL_NB                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         ,BIKO_DTL                                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         ,GDS.GOODS_CD_NRS                                         ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("     FROM                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("     (                                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         SELECT                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("              SCMM.CUST_GOODS_CD                 AS CUST_GOODS_CD  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,SCMM.GOODS_NM                      AS GOODS_NM       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,SCMM.OUTKA_TTL_NB                  AS OUTKA_TTL_NB   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,SCMM.REMARK                        AS BIKO_DTL       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,'0'                                AS INSERT_FLG     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         FROM                                                      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_M SCMM")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         WHERE                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             SCMM.SYS_DEL_FLG   = '0'                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         AND SCMM.SCM_CTL_NO_L  = @SCM_CTL_NO_L                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         UNION ALL                                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         SELECT                                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("              SCMD.GOODS_CD                      AS CUST_GOODS_CD  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,SCMD.GOODS_NM                      AS GOODS_NM       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,SCMD.PKG_NB * SCMD.OUTKA_PKG_NB    AS OUTKA_TTL_NB   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("             ,SCMD.BIKO_DTL                      AS BIKO_DTL       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        ,'1'                                     AS INSERT_FLG     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    FROM                                                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_DTL_BP SCMD")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    WHERE                                                          ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("        SCMD.SYS_DEL_FLG   = '0'                                   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND SCMD.CRT_DATE      = @CRT_DATE                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND SCMD.FILE_NAME     = @FILE_NAME                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    AND SCMD.REC_NO        = @REC_NO                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(") MAIN                                                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("Z_KBN KBN1")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON   KBN1.KBN_GROUP_CD = 'S033'                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  KBN1.SYS_DEL_FLG  = '0'                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                                     ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._MstSchemaNm)
        Me._StrSql.Append("M_GOODS GDS")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON   GDS.GOODS_CD_CUST = MAIN.CUST_GOODS_CD                        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  GDS.NRS_BR_CD     = KBN1.KBN_NM4                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  GDS.CUST_CD_L     = KBN1.KBN_NM5                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND  GDS.SYS_DEL_FLG   = '0'                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE                                                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    MAIN.INSERT_FLG    = @INSERT_FLG                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND KBN1.KBN_NM4       = @BR_CD                                    ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND KBN1.KBN_NM3       = @SCM_CUST_CD                              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ORDER BY  CUST_GOODS_CD                                            ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("         ,GOODS_NM                                                 ")
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
        Me._StrSql.Append("     SUM(ISNULL(ZAI.PORA_ZAI_NB,0))   AS PORA_ZAI_NB  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnEDINm)
        Me._StrSql.Append("D_ZAI_TRS   ZAI                                       ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("LEFT JOIN                                             ")
        Me._StrSql.Append(Me._MstEDINm)
        Me._StrSql.Append("M_GOODS   GOODS                                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("ON ZAI.GOODS_CD_NRS = GOODS.GOODS_CD_NRS              ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE    ZAI.NRS_BR_CD      = @BR_CD                  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND GOODS.GOODS_CD_CUST        = @CUST_GOODS_CD           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND ZAI.WH_CD               = @SOKO_CD                ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND ZAI.SYS_DEL_FLG         = '0'                     ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  排他チェック用SQL(N_OUTKASCM_L)
    ''' </summary>
    ''' <remarks>排他チェック用SQL(N_OUTKASCM_L)の構築</remarks>
    Private Sub SQLHaitaScmL()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("SELECT                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    COUNT(SCM_CTL_NO_L)  AS REC_CNT  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                                 ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L                         ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  排他チェック用SQL(N_OUTKASCM_HED_BP)
    ''' </summary>
    ''' <remarks>排他チェック用SQL(N_OUTKASCM_HED_BP)の構築</remarks>
    Private Sub SQLHaitaScmHed()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._StrSql.Append("SELECT                           ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    COUNT(CRT_DATE)  AS REC_CNT  ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_HED_BP")
        Me._StrSql.Append(vbNewLine)


    End Sub

    ''' <summary>
    '''  倉庫側DBの対象データの削除状態チェック(H_OUTKAEDI_L)
    ''' </summary>
    ''' <remarks>倉庫側DBの対象データの削除状態チェック(H_OUTKAEDI_L)SQLの構築</remarks>
    Private Sub SQLDelChkEdiL()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._StrSql.Append("SELECT                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("    COUNT(NRS_BR_CD)  AS REC_CNT   ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("FROM                               ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._TrnEDINm)
        Me._StrSql.Append("H_OUTKAEDI_L")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("WHERE NRS_BR_CD    = @BR_CD        ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND   WH_CD        = @SOKO_CD      ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND   SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND   SYS_DEL_FLG  = '0'           ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 更新SQL(N_OUTKASCM_L)
    ''' </summary>
    ''' <remarks>更新SQL(N_OUTKASCM_L)の構築</remarks>
    Private Sub SQLUpdateL()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("UPDATE                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_L SET ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 更新SQL(N_OUTKASCM_M)
    ''' </summary>
    ''' <remarks>更新SQL(N_OUTKASCM_M)の構築</remarks>
    Private Sub SQLUpdateM()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("UPDATE                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_M SET ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 更新SQL(N_OUTKASCM_HED_BP)
    ''' </summary>
    ''' <remarks>更新SQL(N_OUTKASCM_HED_BP)の構築</remarks>
    Private Sub SQLUpdateHed()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("UPDATE                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_HED_BP SET ")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 更新SQL(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <remarks>更新SQL(N_OUTKASCM_HED_BP)の構築</remarks>
    Private Sub SQLUpdateDtl()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._StrSql.Append("UPDATE                             ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me._LMNTrnSchemaNm)
        Me._StrSql.Append("N_OUTKASCM_DTL_BP SET ")
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

#Region "初期化処理"

    ''' <summary>
    ''' N_OUTKASCM_L初期化処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>N_OUTKASCM_L更新SQLの構築・発行</remarks>
    Private Function InitShukkaData(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Call Me.SQLUpdateL() 'Select句
        Call Me.SQLUpdInit() '更新句作成
        Me._StrSql.Append(SQL_UPD_INIT)                 '更新内容
        Me._StrSql.Append(LMN020DAC.SQL_UPDATE_WHERE_L) 'Where句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamInit_L()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN020DAC", "DeleteOutkaScmL", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(初期化処理(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInit_L()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

#End Region

#Region "LMNControl"

#Region "Feild"

    ''' <summary>
    ''' 区分マスタ保持用
    ''' </summary>
    ''' <remarks></remarks>
    Private _kbnDs As DataSet

#End Region

#Region "Const"

    Private Const COL_BR_CD As String = "COL_BR_CD"

    Private Const COL_IKO_FLG As String = "COL_IKO_FLG"

    Private Const COL_LMS_SV_NM As String = "COL_LMS_SV_NM"

    Private Const COL_LMS_SCHEMA_NM As String = "COL_LMS_SCHEMA_NM"

    Private Const COL_LMS2_SV_NM As String = "COL_LMS2_SV_NM"

    Private Const COL_LMS2_SCHEMA_NM As String = "COL_LMS2_SCHEMA_NM"

#End Region

    ''' <summary>
    ''' 区分マスタ設定初期処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LMNControl()

        If _kbnDs Is Nothing = True Then

            Me.CreateKbnDataSet()

            Me.SetConnectDataSet(_kbnDs)

        End If

    End Sub

    ''' <summary>
    ''' 区分マスタ取得
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
    ''' 区分マスタ設定
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
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetConnection()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

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

        Me._strSql.Append("SELECT       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  KBN_GROUP_CD	AS	KBN_GROUP_CD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_CD		    AS	KBN_CD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_KEYWORD	AS	KBN_KEYWORD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM1		AS	KBN_NM1")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM2		AS	KBN_NM2")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM3		AS	KBN_NM3")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM4		AS	KBN_NM4")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM5		AS	KBN_NM5")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM6		AS	KBN_NM6")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM7		AS	KBN_NM7")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM8		AS	KBN_NM8")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM9		AS	KBN_NM9")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM10		AS	KBN_NM10")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE1		    AS	VALUE1")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE2	       	AS	VALUE2")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE3	    	AS	VALUE3")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SORT	    	AS	SORT")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,REM	    	AS	REM")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._MstSchemaNm)
        Me._strSql.Append("Z_KBN KBN       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" KBN.SYS_DEL_FLG = '0'       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" AND KBN.KBN_GROUP_CD ='L001'       ")


    End Sub


#End Region


#End Region

End Class
