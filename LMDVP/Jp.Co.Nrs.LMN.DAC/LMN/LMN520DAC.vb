' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : SCM
'  プログラムID     :  LMN520    : 商品別過去実績推移
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN520DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN520DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    'マスタスキーマ名
    Private Const MST_SCHEMA As String = "LM_MST"

    'トランザクションスキーマ名
    Private Const TRN_SCHEMA As String = "LM_TRN"

    'ステータス「未設定」区分コード
    Private Const KbnCdMisettei As String = "00"
    'ステータス「設定済」区分コード
    Private Const KbnCdSetteiZumi As String = "01"

    ''' <summary>
    ''' 印刷データ抽出用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private SQL_SELECT_DATA As String

    ''' <summary>
    ''' 印刷データ抽出用SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlSelectData()

        Dim SQL As String = "SELECT                                                             " & vbNewLine _
                          & " 'LMN520'                     AS RPT_ID                            " & vbNewLine _
                          & ",MAIN.NRS_BR_CD               AS NRS_BR_CD                         " & vbNewLine _
                          & ",MAIN.WH_CD                   AS WH_CD                             " & vbNewLine _
                          & ",MS.WH_NM                     AS WH_NM                             " & vbNewLine _
                          & ",@SCM_CUST_CD                 AS SCM_CUST_CD                       " & vbNewLine _
                          & ",MC.CUST_NM_L                 AS CUST_NM                           " & vbNewLine _
                          & ",MAIN.INOUTKA_DATE            AS INOUTKA_DATE                      " & vbNewLine _
                          & ",@GOODS_CD_CUST               AS GOODS_CD_CUST                     " & vbNewLine _
                          & ",MG.GOODS_NM                  AS GOODS_NM                          " & vbNewLine _
                          & ",MG.NB_UT                     AS NB_UT                             " & vbNewLine _
                          & ",0                            AS ZAI_NB                            " & vbNewLine _
                          & ",SUM(MAIN.INKA_NB)            AS INKA_NB                           " & vbNewLine _
                          & ",SUM(MAIN.OUTKA_NB)           AS OUTKA_NB                          " & vbNewLine _
                          & ",''                           AS HANREI_TAITLE1                    " & vbNewLine _
                          & ",''                           AS HANREI_TAITLE2                    " & vbNewLine _
                          & "FROM                                                               " & vbNewLine _
                          & "(                                                                  " & vbNewLine _
                          & "--入荷履歴                                                         " & vbNewLine _
                          & "  SELECT                                                           " & vbNewLine _
                          & "   INL.NRS_BR_CD                        AS NRS_BR_CD               " & vbNewLine _
                          & "  ,INL.WH_CD                            AS WH_CD                   " & vbNewLine _
                          & "  ,INL.CUST_CD_L                        AS CUST_CD_L               " & vbNewLine _
                          & "  ,MG.GOODS_CD_CUST                     AS GOODS_CD_CUST           " & vbNewLine _
                          & "  ,INL.INKA_DATE                        AS INOUTKA_DATE            " & vbNewLine _
                          & "  ,0                                    AS ZAI_NB                  " & vbNewLine _
                          & "  ,(INS.KONSU * MG.PKG_NB) + INS.HASU   AS INKA_NB                 " & vbNewLine _
                          & "  ,0                                    AS OUTKA_NB                " & vbNewLine _
                          & "  FROM                                                             " & vbNewLine _
                          & "  --入荷L                                                          " & vbNewLine _
                          & "  " & Me._TrnNm & "B_INKA_L INL                                    " & vbNewLine _
                          & "  --入荷M                                                          " & vbNewLine _
                          & "  LEFT JOIN " & Me._TrnNm & "B_INKA_M INM                          " & vbNewLine _
                          & "  ON  INM.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                          & "  AND INM.NRS_BR_CD = INL.NRS_BR_CD                                " & vbNewLine _
                          & "  AND INM.INKA_NO_L = INL.INKA_NO_L                                " & vbNewLine _
                          & "  --入荷S                                                          " & vbNewLine _
                          & "  LEFT JOIN " & Me._TrnNm & "B_INKA_S INS                          " & vbNewLine _
                          & "  ON  INS.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                          & "  AND INS.NRS_BR_CD = INM.NRS_BR_CD                                " & vbNewLine _
                          & "  AND INS.INKA_NO_L = INM.INKA_NO_L                                " & vbNewLine _
                          & "  AND INS.INKA_NO_M = INM.INKA_NO_M                                " & vbNewLine _
                          & "  --商品M                                                          " & vbNewLine _
                          & "  LEFT JOIN " & Me._MstNm & "M_GOODS MG                            " & vbNewLine _
                          & "  ON  MG.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                          & "  AND MG.NRS_BR_CD = INM.NRS_BR_CD                                 " & vbNewLine _
                          & "  AND MG.GOODS_CD_NRS = INM.GOODS_CD_NRS                           " & vbNewLine _
                          & "  WHERE                                                            " & vbNewLine _
                          & "      INL.NRS_BR_CD = @NRS_BR_CD                                   " & vbNewLine _
                          & "  AND INL.WH_CD = @WH_CD                                           " & vbNewLine _
                          & "  AND INL.CUST_CD_L = (SELECT                                      " & vbNewLine _
                          & "                        KBN_NM5                                    " & vbNewLine _
                          & "                       FROM                                        " & vbNewLine _
                          & "                        " & Me._MstNm & "Z_KBN                     " & vbNewLine _
                          & "                       WHERE                                       " & vbNewLine _
                          & "                           SYS_DEL_FLG = '0'                       " & vbNewLine _
                          & "                       AND KBN_GROUP_CD = 'S033'                   " & vbNewLine _
                          & "                       AND KBN_NM3 = @SCM_CUST_CD                  " & vbNewLine _
                          & "                       AND KBN_NM4 = @NRS_BR_CD)                   " & vbNewLine _
                          & "  AND MG.GOODS_CD_CUST = @GOODS_CD_CUST                            " & vbNewLine _
                          & "  AND INL.INKA_STATE_KB > '40'                                     " & vbNewLine _
                          & "--出荷履歴                                                         " & vbNewLine _
                          & "  UNION ALL                                                        " & vbNewLine _
                          & "  SELECT                                                           " & vbNewLine _
                          & "   OUTL.NRS_BR_CD                       AS NRS_BR_CD               " & vbNewLine _
                          & "  ,OUTL.WH_CD                           AS WH_CD                   " & vbNewLine _
                          & "  ,OUTL.CUST_CD_L                       AS CUST_CD_L               " & vbNewLine _
                          & "  ,MG.GOODS_CD_CUST                     AS GOODS_CD_CUST           " & vbNewLine _
                          & "  ,OUTL.OUTKA_PLAN_DATE                 AS INOUTKA_DATE            " & vbNewLine _
                          & "  ,0                                    AS ZAI_NB                  " & vbNewLine _
                          & "  ,0                                    AS INKA_NB                 " & vbNewLine _
                          & "  ,OUTM.OUTKA_TTL_NB                    AS OUTKA_NB                " & vbNewLine _
                          & "  FROM                                                             " & vbNewLine _
                          & "  --出荷L                                                          " & vbNewLine _
                          & "  " & Me._TrnNm & "C_OUTKA_L OUTL                                  " & vbNewLine _
                          & "  --出荷M                                                          " & vbNewLine _
                          & "  LEFT JOIN " & Me._TrnNm & "C_OUTKA_M OUTM                        " & vbNewLine _
                          & "  ON  OUTM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                          & "  AND OUTM.NRS_BR_CD = OUTL.NRS_BR_CD                              " & vbNewLine _
                          & "  AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L                            " & vbNewLine _
                          & "  --商品M                                                          " & vbNewLine _
                          & "  LEFT JOIN " & Me._MstNm & "M_GOODS MG                            " & vbNewLine _
                          & "  ON  MG.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                          & "  AND MG.NRS_BR_CD = OUTM.NRS_BR_CD                                " & vbNewLine _
                          & "  AND MG.GOODS_CD_NRS = OUTM.GOODS_CD_NRS                          " & vbNewLine _
                          & "  WHERE                                                            " & vbNewLine _
                          & "      OUTL.NRS_BR_CD = @NRS_BR_CD                                  " & vbNewLine _
                          & "  AND OUTL.WH_CD = @WH_CD                                          " & vbNewLine _
                          & "  AND OUTL.CUST_CD_L = (SELECT                                     " & vbNewLine _
                          & "                         KBN_NM5                                   " & vbNewLine _
                          & "                        FROM                                       " & vbNewLine _
                          & "                         " & Me._MstNm & "Z_KBN                    " & vbNewLine _
                          & "                        WHERE                                      " & vbNewLine _
                          & "                            SYS_DEL_FLG = '0'                      " & vbNewLine _
                          & "                        AND KBN_GROUP_CD = 'S033'                  " & vbNewLine _
                          & "                        AND KBN_NM3 = @SCM_CUST_CD                 " & vbNewLine _
                          & "                        AND KBN_NM4 = @NRS_BR_CD)                  " & vbNewLine _
                          & "  AND MG.GOODS_CD_CUST = @GOODS_CD_CUST                            " & vbNewLine _
                          & "  AND OUTL.OUTKA_STATE_KB > '50'                                   " & vbNewLine _
                          & ") MAIN                                                             " & vbNewLine _
                          & "LEFT JOIN                                                          " & vbNewLine _
                          & "(                                                                  " & vbNewLine _
                          & "  SELECT                                                           " & vbNewLine _
                          & "   NRS_BR_CD        AS NRS_BR_CD                                   " & vbNewLine _
                          & "  ,CUST_CD_L        AS CUST_CD_L                                   " & vbNewLine _
                          & "  ,MIN(CUST_NM_L)   AS CUST_NM_L                                   " & vbNewLine _
                          & "  FROM                                                             " & vbNewLine _
                          & "  " & Me._MstNm & "M_CUST                                          " & vbNewLine _
                          & "  WHERE                                                            " & vbNewLine _
                          & "  SYS_DEL_FLG = '0'                                                " & vbNewLine _
                          & "  GROUP BY                                                         " & vbNewLine _
                          & "   NRS_BR_CD                                                       " & vbNewLine _
                          & "  ,CUST_CD_L                                                       " & vbNewLine _
                          & ") MC                                                               " & vbNewLine _
                          & "ON  MC.NRS_BR_CD = MAIN.NRS_BR_CD                                  " & vbNewLine _
                          & "AND MC.CUST_CD_L = MAIN.CUST_CD_L                                  " & vbNewLine _
                          & "LEFT JOIN                                                          " & vbNewLine _
                          & "(                                                                  " & vbNewLine _
                          & "  SELECT                                                           " & vbNewLine _
                          & "   NRS_BR_CD              AS NRS_BR_CD                             " & vbNewLine _
                          & "  ,CUST_CD_L              AS CUST_CD_L                             " & vbNewLine _
                          & "  ,GOODS_CD_CUST          AS GOODS_CD_CUST                         " & vbNewLine _
                          & "  ,MIN(GOODS_NM_1)        AS GOODS_NM                              " & vbNewLine _
                          & "  ,MIN(NB_UT)             AS NB_UT                                 " & vbNewLine _
                          & "  FROM                                                             " & vbNewLine _
                          & "  " & Me._MstNm & "M_GOODS                                         " & vbNewLine _
                          & "  WHERE                                                            " & vbNewLine _
                          & "  SYS_DEL_FLG = '0'                                                " & vbNewLine _
                          & "  GROUP BY                                                         " & vbNewLine _
                          & "   NRS_BR_CD                                                       " & vbNewLine _
                          & "  ,CUST_CD_L                                                       " & vbNewLine _
                          & "  ,GOODS_CD_CUST                                                   " & vbNewLine _
                          & ") MG                                                               " & vbNewLine _
                          & "ON  MG.NRS_BR_CD = MAIN.NRS_BR_CD                                  " & vbNewLine _
                          & "AND MG.CUST_CD_L = MAIN.CUST_CD_L                                  " & vbNewLine _
                          & "AND MG.GOODS_CD_CUST = @GOODS_CD_CUST                              " & vbNewLine _
                          & "LEFT JOIN " & Me._MstNm & "M_SOKO MS                               " & vbNewLine _
                          & "ON  MS.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                          & "AND MS.WH_CD = MAIN.WH_CD                                          " & vbNewLine _
                          & "GROUP BY                                                           " & vbNewLine _
                          & " MAIN.NRS_BR_CD                                                    " & vbNewLine _
                          & ",MAIN.WH_CD                                                        " & vbNewLine _
                          & ",MS.WH_NM                                                          " & vbNewLine _
                          & ",MC.CUST_NM_L                                                      " & vbNewLine _
                          & ",MAIN.INOUTKA_DATE                                                 " & vbNewLine _
                          & ",MG.GOODS_NM                                                       " & vbNewLine _
                          & ",MG.NB_UT                                                          " & vbNewLine _
                          & "ORDER BY                                                           " & vbNewLine _
                          & " INOUTKA_DATE                                                      " & vbNewLine

        SQL_SELECT_DATA = SQL

    End Sub



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
    ''' 設定処理用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' データ取得用トランザクションスキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnNm As String

    ''' <summary>
    ''' データ取得用マスタスキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstNm As String

    ''' <summary>
    ''' LMSのコネクション
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMS1 As SqlConnection = New SqlConnection

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品別過去実績データ検索(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrintDataLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("LMN520IN").Rows(0).Item("NRS_BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.CreateSqlSelectData()
                Me._StrSql.Append(SQL_SELECT_DATA)
                Call Me.SetConditionMasterSQL()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._StrSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._Row = inTbl.Rows(0)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN520DAC", "SelectPrintDataLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("RPT_ID", "RPT_ID")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("SCM_CUST_CD", "SCM_CUST_CD")
                map.Add("CUST_NM", "CUST_NM")
                map.Add("INOUTKA_DATE", "INOUTKA_DATE")
                map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
                map.Add("GOODS_NM", "GOODS_NM")
                map.Add("NB_UT", "NB_UT")
                map.Add("ZAI_NB", "ZAI_NB")
                map.Add("INKA_NB", "INKA_NB")
                map.Add("OUTKA_NB", "OUTKA_NB")
                map.Add("HANREI_TAITLE1", "HANREI_TAITLE1")
                map.Add("HANREI_TAITLE2", "HANREI_TAITLE2")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN520OUT")

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
    ''' 商品別過去実績データ検索(LMSVer2)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintDataLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlSelectData()                     'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(SQL_SELECT_DATA)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN520DAC", "SelectPrintDataLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("NB_UT", "NB_UT")
        map.Add("ZAI_NB", "ZAI_NB")
        map.Add("INKA_NB", "INKA_NB")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("HANREI_TAITLE1", "HANREI_TAITLE1")
        map.Add("HANREI_TAITLE2", "HANREI_TAITLE2")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN520OUT")

        Return ds

    End Function


    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))

            'SCM荷主コード
            whereStr = .Item("SCM_CUST_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", whereStr, DBDataType.CHAR))

            '荷主商品コード
            whereStr = .Item("GOODS_CD_CUST").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", whereStr, DBDataType.CHAR))

        End With

    End Sub


#End Region

#Region "設定処理"

#Region "スキーマ設定"

    ''' <summary>
    ''' マスタ用スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSchemaNm()

        Me._MstSchemaNm = String.Concat(MST_SCHEMA, "..")

    End Sub

#End Region

#Region "接続名取得"

    Private Sub GetLMSConnectName(ByVal ds As DataSet)

        'IN情報を取得
        Dim inTable As DataTable = ds.Tables("LMN520IN")

        '営業所コード取得
        Dim brCd As String = inTable.Rows(0).Item("NRS_BR_CD").ToString()

        Me._TrnNm = String.Concat(Me.GetSchemaEDI(brCd), "..")
        Me._MstNm = String.Concat(Me.GetSchemaEDIMst(brCd), "..")

    End Sub

#End Region

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

        'TODO後で戻す
        'Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString
        Dim serverAcFlg As String = "01"

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

        'TODO後で戻す
        'Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString
        Dim serverAcFlg As String = "01"

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

        Call Me.SetSchemaNm()

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

#End Region

#End Region

End Class
