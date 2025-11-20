' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG030DAC : 保管料荷役料明細編集
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMG030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索"

#Region "COUNT"

    ''' <summary>
    ''' 件数判定用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SELECT_COUNT As String = "SELECT COUNT(SEKYP.NRS_BR_CD) AS SQL_CNT                         " & vbNewLine


#End Region

    ''' <summary>
    ''' 変動保管料情報検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SELECT_SQL_VAR_STRAGE As String = "" _
            & " SELECT                                     " & vbNewLine _
            & "   SEI.VAR_STRAGE_FLG                       " & vbNewLine _
            & " FROM                                       " & vbNewLine _
            & "   $LM_MST$..M_CUST AS CST                  " & vbNewLine _
            & " LEFT JOIN                                  " & vbNewLine _
            & "   $LM_MST$..M_SEIQTO AS SEI                " & vbNewLine _
            & "   ON  SEI.NRS_BR_CD = CST.NRS_BR_CD        " & vbNewLine _
            & "   AND SEI.SEIQTO_CD = CST.HOKAN_SEIQTO_CD  " & vbNewLine _
            & " WHERE                                      " & vbNewLine _
            & "       CST.NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
            & "   AND CST.CUST_CD_L = @CUST_CD_L           " & vbNewLine _
            & "   AND CST.CUST_CD_M = @CUST_CD_M           " & vbNewLine _
            & "   AND CST.CUST_CD_S = @CUST_CD_S           " & vbNewLine _
            & "   AND CST.CUST_CD_SS = @CUST_CD_SS         " & vbNewLine

    ''' <summary>
    ''' 検索時SQLセレクト
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIRST_SELECT_SQL As String = "SELECT                                                       " & vbNewLine _
                                             & " TBL.GOODS_CD_CUST AS GOODS_CD_CUST,                         " & vbNewLine _
                                             & " TBL.GOODS_NM_1 AS GOODS_NM_1,                               " & vbNewLine _
                                             & " SEKYP.LOT_NO AS LOT_NO,                                     " & vbNewLine _
                                             & " SEKYP.SERIAL_NO AS SERIAL_NO,                               " & vbNewLine _
                                             & " TBL.NB_UT_NM AS NB_UT_NM,                                   " & vbNewLine _
                                             & " SEKYP.IRIME AS IRIME,                                       " & vbNewLine _
                                             & " TBL.IRIME_UT_NM AS IRIME_UT_NM,                             " & vbNewLine _
                                             & " KBN3.#KBN# AS TAX_KB_NM,                                    " & vbNewLine _
                                             & " SEKYP.SEKI_ARI_NB1 AS SEKI_ARI_NB1,                         " & vbNewLine _
                                             & " SEKYP.SEKI_ARI_NB2 AS SEKI_ARI_NB2,                         " & vbNewLine _
                                             & " SEKYP.SEKI_ARI_NB3 AS SEKI_ARI_NB3,                         " & vbNewLine _
                                             & " SEKYP.INKO_NB_TTL1 AS INKO_NB_TTL1,                         " & vbNewLine _
                                             & " SEKYP.OUTKO_NB_TTL1 AS OUTKO_NB_TTL1,                       " & vbNewLine _
                                             & " SEKYP.STORAGE1 AS STORAGE1,                                 " & vbNewLine _
                                             & " SEKYP.STORAGE2 AS STORAGE2,                                 " & vbNewLine _
                                             & " SEKYP.STORAGE3 AS STORAGE3,                                 " & vbNewLine _
                                             & " SEKYP.STORAGE_AMO_TTL AS STORAGE_AMO_TTL,                   " & vbNewLine _
                                             & " SEKYP.HANDLING_IN1 AS HANDLING_IN1,                         " & vbNewLine _
                                             & " SEKYP.HANDLING_IN2 AS HANDLING_IN2,                         " & vbNewLine _
                                             & " SEKYP.HANDLING_IN3 AS HANDLING_IN3,                         " & vbNewLine _
                                             & " SEKYP.HANDLING_OUT1 AS HANDLING_OUT1,                       " & vbNewLine _
                                             & " SEKYP.HANDLING_OUT2 AS HANDLING_OUT2,                       " & vbNewLine _
                                             & " SEKYP.HANDLING_OUT3 AS HANDLING_OUT3,                       " & vbNewLine _
                                             & " SEKYP.HANDLING_AMO_TTL AS HANDLING_AMO_TTL,                 " & vbNewLine _
                                             & " SEKYP.INKA_NO_L AS INKA_NO_L,                               " & vbNewLine _
                                             & " SEKYP.GOODS_CD_NRS AS GOODS_CD_NRS,                         " & vbNewLine _
                                             & " TBL.CUST_NM_S_SS AS CUST_NM_S_SS,                           " & vbNewLine _
                                             & " TBL.CUST_CD_L AS CUST_CD_L,                                 " & vbNewLine _
                                             & " TBL.CUST_CD_M AS CUST_CD_M,                                 " & vbNewLine _
                                             & " TBL.CUST_CD_S AS CUST_CD_S,                                 " & vbNewLine _
                                             & " TBL.CUST_CD_SS AS CUST_CD_SS,                               " & vbNewLine _
                                             & " SEKYP.CTL_NO AS CTL_NO,                                     " & vbNewLine _
                                             & " TBL.STD_IRIME_UT AS IRIME_UT,                               " & vbNewLine _
                                             & " SEKYP.TAX_KB AS TAX_KB,                                     " & vbNewLine _
                                             & " TBL.NB_UT AS NB_UT,                                         " & vbNewLine _
                                             & " SEKYP.SYS_UPD_DATE AS SYS_UPD_DATE,                         " & vbNewLine _
                                             & " SEKYP.SYS_UPD_TIME AS SYS_UPD_TIME,                         " & vbNewLine _
                                             & " SEKYP.STRAGE_HENDO_NASHI_AMO_TTL,                           " & vbNewLine _
                                             & " CASE WHEN SEKYP.INKA_DATE = ''                              " & vbNewLine _
                                             & "     THEN ''                                                 " & vbNewLine _
                                             & "     ELSE FORMAT(CONVERT(DATE,SEKYP.INKA_DATE),'yyyy/MM/dd') " & vbNewLine _
                                             & "     END AS INKA_DATE,                                       " & vbNewLine _
                                             & " SEKYP.VAR_RATE                                              " & vbNewLine

    ''' <summary>
    ''' 検索時SQLフロム
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIRST_FROM_SQL As String = "FROM $LM_TRN$..G_SEKY_MEISAI_PRT SEKYP                         " & vbNewLine _
                                             & "INNER JOIN                                                   " & vbNewLine _
                                             & "  (SELECT                                                    " & vbNewLine _
                                             & "    GOODS.NRS_BR_CD,                                         " & vbNewLine _
                                             & "    GOODS.GOODS_CD_NRS,                                      " & vbNewLine _
                                             & "    GOODS.GOODS_CD_CUST,                                     " & vbNewLine _
                                             & "    GOODS.GOODS_NM_1,                                        " & vbNewLine _
                                             & "    GOODS.STD_IRIME_UT,                                      " & vbNewLine _
                                             & "    GOODS.NB_UT,                                             " & vbNewLine _
                                             & "    GOODS.CUST_CD_L,                                         " & vbNewLine _
                                             & "    GOODS.CUST_CD_M,                                         " & vbNewLine _
                                             & "    GOODS.CUST_CD_S,                                         " & vbNewLine _
                                             & "    GOODS.CUST_CD_SS,                                        " & vbNewLine _
                                             & "    CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS AS CUST_NM_S_SS,  " & vbNewLine _
                                             & "    KBN1.#KBN# AS NB_UT_NM,                                  " & vbNewLine _
                                             & "    KBN2.#KBN# AS IRIME_UT_NM                                " & vbNewLine _
                                             & "   FROM $LM_MST$..M_GOODS AS GOODS                           " & vbNewLine _
                                             & "    INNER JOIN $LM_MST$..M_CUST AS CUST                      " & vbNewLine _
                                             & "      ON  GOODS.NRS_BR_CD = CUST.NRS_BR_CD                   " & vbNewLine _
                                             & "      AND GOODS.CUST_CD_L = CUST.CUST_CD_L                   " & vbNewLine _
                                             & "      AND GOODS.CUST_CD_M = CUST.CUST_CD_M                   " & vbNewLine _
                                             & "      AND GOODS.CUST_CD_S = CUST.CUST_CD_S                   " & vbNewLine _
                                             & "      AND GOODS.CUST_CD_SS = CUST.CUST_CD_SS                 " & vbNewLine _
                                             & "    LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN1                  " & vbNewLine _
                                             & "      ON  KBN1.KBN_GROUP_CD ='K002'                          " & vbNewLine _
                                             & "      AND KBN1.KBN_CD = GOODS.NB_UT                          " & vbNewLine _
                                             & "      AND KBN1.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                             & "    LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN2                  " & vbNewLine _
                                             & "      ON  KBN2.KBN_GROUP_CD ='I001'                          " & vbNewLine _
                                             & "      AND KBN2.KBN_CD = GOODS.STD_IRIME_UT                   " & vbNewLine _
                                             & "      AND KBN2.SYS_DEL_FLG = '0'                             " & vbNewLine _
                                             & "    WHERE GOODS.SYS_DEL_FLG = '0')TBL                        " & vbNewLine _
                                             & "   ON  SEKYP.NRS_BR_CD = TBL.NRS_BR_CD                       " & vbNewLine _
                                             & "   AND SEKYP.GOODS_CD_NRS = TBL.GOODS_CD_NRS                 " & vbNewLine _
                                             & " LEFT OUTER JOIN $LM_MST$..Z_KBN AS KBN3                     " & vbNewLine _
                                             & "   ON  KBN3.KBN_GROUP_CD ='Z001'                             " & vbNewLine _
                                             & "   AND KBN3.KBN_CD = SEKYP.TAX_KB                            " & vbNewLine _
                                             & "   AND KBN3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                             & "WHERE SEKYP.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                             & "  AND SEKYP.JOB_NO = @JOB_NO                                 " & vbNewLine

#Region "ORDERBY"

    ''' <summary>
    '''  ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY TBL.GOODS_NM_1 ,TBL.GOODS_CD_CUST, SEKYP.LOT_NO, SEKYP.INKA_DATE, SEKYP.INKA_NO_L"

#End Region

#End Region

#Region "UPDATE"

    ''' <summary>
    ''' 保管荷役明細印刷テーブル更新用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UPDATE_SQL As String = "UPDATE                                                            " & vbNewLine _
                                        & "  $LM_TRN$..G_SEKY_MEISAI_PRT                                     " & vbNewLine _
                                        & "SET                                                               " & vbNewLine _
                                        & "  HANDLING_IN1 = @HANDLING_IN1,                                   " & vbNewLine _
                                        & "  HANDLING_OUT1 = @HANDLING_OUT1,                                 " & vbNewLine _
                                        & "  STORAGE1 = @STORAGE1,                                           " & vbNewLine _
                                        & "  SEKI_ARI_NB1 = @SEKI_ARI_NB1,                                   " & vbNewLine _
                                        & "  HANDLING_IN2 = @HANDLING_IN2,                                   " & vbNewLine _
                                        & "  HANDLING_OUT2 = @HANDLING_OUT2,                                 " & vbNewLine _
                                        & "  STORAGE2 = @STORAGE2,                                           " & vbNewLine _
                                        & "  SEKI_ARI_NB2 = @SEKI_ARI_NB2,                                   " & vbNewLine _
                                        & "  HANDLING_IN3 = @HANDLING_IN3,                                   " & vbNewLine _
                                        & "  HANDLING_OUT3 = @HANDLING_OUT3,                                 " & vbNewLine _
                                        & "  STORAGE3 = @STORAGE3,                                           " & vbNewLine _
                                        & "  SEKI_ARI_NB3 = @SEKI_ARI_NB3,                                   " & vbNewLine _
                                        & "  INKO_NB_TTL1 = @INKO_NB_TTL1,                                   " & vbNewLine _
                                        & "  OUTKO_NB_TTL1 = @OUTKO_NB_TTL1,                                 " & vbNewLine _
                                        & "  STORAGE_AMO_TTL = @STORAGE_AMO_TTL,                             " & vbNewLine _
                                        & "  HANDLING_AMO_TTL = @HANDLING_AMO_TTL,                           " & vbNewLine _
                                        & "  STRAGE_HENDO_NASHI_AMO_TTL = @STRAGE_HENDO_NASHI_AMO_TTL,       " & vbNewLine _
                                        & "  SYS_UPD_DATE = @SYS_UPD_DATE,                                   " & vbNewLine _
                                        & "  SYS_UPD_TIME = @SYS_UPD_TIME,                                   " & vbNewLine _
                                        & "  SYS_UPD_PGID = @SYS_UPD_PGID,                                   " & vbNewLine _
                                        & "  SYS_UPD_USER = @SYS_UPD_USER                                    " & vbNewLine _
                                        & "WHERE NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                        & "  AND JOB_NO = @JOB_NO                                            " & vbNewLine _
                                        & "  AND CTL_NO = @CTL_NO                                            " & vbNewLine _
                                        & "  AND SYS_UPD_DATE = @SYS_UPD_DATE_PRT                            " & vbNewLine _
                                        & "  AND SYS_UPD_TIME = @SYS_UPD_TIME_PRT                            " & vbNewLine

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 保管荷役明細印刷テーブル一括変更用SQL SET句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const IKKATU_UPDATE_SQL As String = "UPDATE                                                      " & vbNewLine _
                                        & "  $LM_TRN$..G_SEKY_MEISAI_PRT                                     " & vbNewLine _
                                        & "SET                                                               " & vbNewLine _
                                        & "  SYS_UPD_DATE = @SYS_UPD_DATE,                                   " & vbNewLine _
                                        & "  SYS_UPD_TIME = @SYS_UPD_TIME,                                   " & vbNewLine _
                                        & "  SYS_UPD_PGID = @SYS_UPD_PGID,                                   " & vbNewLine _
                                        & "  SYS_UPD_USER = @SYS_UPD_USER,                                   " & vbNewLine

    ''' <summary>
    ''' 保管荷役明細印刷テーブル一括変更用SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const IKKATU_UPDATE_SQL_WHERE As String = "WHERE NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                        & "  AND JOB_NO = @JOB_NO                                            " & vbNewLine _
                                        & "  AND CTL_NO = @CTL_NO                                            " & vbNewLine _
                                        & "  AND SYS_UPD_DATE = @SYS_UPD_DATE_PRT                            " & vbNewLine _
                                        & "  AND SYS_UPD_TIME = @SYS_UPD_TIME_PRT                            " & vbNewLine

    'END YANAI 20111014 一括変更追加

#End Region

#Region "排他"

    ''' <summary>
    ''' 排他チェック用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HAITA_SQL As String = "SELECT COUNT(SEKYP.JOB_NO) AS SQL_CNT         " & vbNewLine _
                                      & "FROM $LM_TRN$..G_SEKY_MEISAI_PRT SEKYP        " & vbNewLine _
                                      & "WHERE SEKYP.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                                      & "  AND SEKYP.JOB_NO = @JOB_NO                  " & vbNewLine _
                                      & "  AND SEKYP.CTL_NO = @CTL_NO                  " & vbNewLine _
                                      & "  AND SEKYP.SYS_UPD_DATE = @SYS_UPD_DATE_PRT  " & vbNewLine _
                                      & "  AND SEKYP.SYS_UPD_TIME = @SYS_UPD_TIME_PRT  " & vbNewLine

    ''' <summary>
    ''' 取込用排他
    ''' </summary>
    ''' <remarks></remarks>
    Private Const BACTH_HAITA_SQL As String = "SELECT COUNT(SEKY.SEKY_FLG) AS SQL_CNT  " & vbNewLine _
                                      & "FROM $LM_TRN$..G_SEKY_TBL AS SEKY             " & vbNewLine _
                                      & "WHERE SEKY.NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
                                      & "  AND SEKY.JOB_NO = @JOB_NO                   " & vbNewLine _
                                      & "  AND SEKY.INV_DATE_TO = @INV_DATE_TO         " & vbNewLine


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

#Region "検索処理"

    ''' <summary>
    ''' 検索処理(変動保管料情報)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG030DAC.SELECT_SQL_VAR_STRAGE)

        '検索用パラメータ設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
        End With

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG030DAC", "SelectVarStrage", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("VAR_STRAGE_FLG", "VAR_STRAGE_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG030OUT_VAR_STRAGE")

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        With Me._Row
            Me._StrSql.Append(LMG030DAC.SELECT_COUNT)
            Dim sql As String = FIRST_FROM_SQL.Replace("@NRS_BR_CD", String.Concat("'", .Item("NRS_BR_CD").ToString(), "'"))
            Me._StrSql.Append(sql.Replace("@JOB_NO", String.Concat("'", .Item("JOB_NO").ToString(), "'")))
        End With
        Call Me.SetConditionMasterSQL()                         '検索用パラメータ設定
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()), "KBN_NM1"))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG030DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SQL_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '20210628 ベトナム対応Add
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20210628 ベトナム対応Add

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQL作成
        Me._StrSql.Append(LMG030DAC.FIRST_SELECT_SQL)
        With Me._Row
            Dim sql As String = LMG030DAC.FIRST_FROM_SQL.Replace("@NRS_BR_CD", String.Concat("'", .Item("NRS_BR_CD").ToString(), "'"))
            Me._StrSql.Append(sql.Replace("@JOB_NO", String.Concat("'", .Item("JOB_NO").ToString(), "'")))
        End With
        Call Me.SetConditionMasterSQL()                         '検索用パラメータ設定
        Me._StrSql.Append(LMG030DAC.SQL_ORDER_BY)               'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG030DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT_NM", "IRIME_UT_NM")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("SEKI_ARI_NB1", "SEKI_ARI_NB1")
        map.Add("SEKI_ARI_NB2", "SEKI_ARI_NB2")
        map.Add("SEKI_ARI_NB3", "SEKI_ARI_NB3")
        map.Add("INKO_NB_TTL1", "INKO_NB_TTL1")
        map.Add("OUTKO_NB_TTL1", "OUTKO_NB_TTL1")
        map.Add("STORAGE1", "STORAGE1")
        map.Add("STORAGE2", "STORAGE2")
        map.Add("STORAGE3", "STORAGE3")
        map.Add("STORAGE_AMO_TTL", "STORAGE_AMO_TTL")
        map.Add("HANDLING_IN1", "HANDLING_IN1")
        map.Add("HANDLING_IN2", "HANDLING_IN2")
        map.Add("HANDLING_IN3", "HANDLING_IN3")
        map.Add("HANDLING_OUT1", "HANDLING_OUT1")
        map.Add("HANDLING_OUT2", "HANDLING_OUT2")
        map.Add("HANDLING_OUT3", "HANDLING_OUT3")
        map.Add("HANDLING_AMO_TTL", "HANDLING_AMO_TTL")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("CUST_NM_S_SS", "CUST_NM_S_SS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CTL_NO", "CTL_NO")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("NB_UT", "NB_UT")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("STRAGE_HENDO_NASHI_AMO_TTL", "STRAGE_HENDO_NASHI_AMO_TTL")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("VAR_RATE", "VAR_RATE")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG030OUT")

        Return ds

    End Function

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 保管荷役明細印刷テーブル更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpDateTable(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN_UPDATE")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG030DAC.UPDATE_SQL)     'SQL構築(Update用)
        Call Me.SetUpdatedataSQL()                  '更新用パラメータ設定
        Call Me.SetParamCommonSystemUpd()           'システム共通更新項目

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG030DAC", "UpDateTable", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        If reader < 1 = True Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 保管荷役明細印刷テーブル一括変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IkkatuUpDateTable(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN_IKKATU_UPDATE")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMG030DAC.IKKATU_UPDATE_SQL)           'SQL構築(Update用) SET句
        Call Me.SetIkkatuSQL()                                   'SQL構築(Update用) SET句(可変)
        Me._StrSql.Append(LMG030DAC.IKKATU_UPDATE_SQL_WHERE)     'SQL構築(Update用) WHERE句
        Call Me.SetIkkatuUpdatedataSQL()                         '更新用パラメータ設定
        Call Me.SetParamCommonSystemUpd()                        'システム共通更新項目

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG030DAC", "IkkatuUpDateTable", cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        Dim guidancdKbn As String = "00"
        If reader < 1 = True Then
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore(guidancdKbn, _
                                   "E011", _
                                   , _
                                   Me._Row.Item("ROW_NO").ToString, _
                                   "商品コード＿商品名＿ロット№＿シリアル№＿入荷管理番号L", _
                                   String.Concat(Me._Row.Item("GOODS_CD_CUST").ToString(), "＿", Me._Row.Item("GOODS_NM").ToString(), "＿", Me._Row.Item("LOT_NO").ToString(), "＿", Me._Row.Item("SERIAL_NO").ToString(), "＿", Me._Row.Item("INKA_NO_L").ToString(), "＿"))
        Else
            MyBase.SetMessage(Nothing)
        End If

        Return ds

    End Function
    'END YANAI 20111014 一括変更追加

#End Region

#Region "排他処理"

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN_UPDATE")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        With Me._Row
            Dim sql As String = LMG030DAC.HAITA_SQL.Replace("@NRS_BR_CD", String.Concat("'", .Item("NRS_BR_CD").ToString(), "'"))
            sql = sql.Replace("@JOB_NO", String.Concat("'", .Item("JOB_NO").ToString(), "'"))
            sql = sql.Replace("@CTL_NO", String.Concat("'", .Item("CTL_NO").ToString(), "'"))
            sql = sql.Replace("@SYS_UPD_DATE_PRT", String.Concat("'", .Item("SYS_UPD_DATE_PRT").ToString(), "'"))
            Me._StrSql.Append(sql.Replace("@SYS_UPD_TIME_PRT", String.Concat("'", .Item("SYS_UPD_TIME_PRT").ToString(), "'")))
        End With

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        MyBase.Logger.WriteSQLLog("LMG030DAC", "HaitaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        If Convert.ToInt32(reader.Item("SQL_CNT")) < 1 = True Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function


#End Region

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Acquisithion(ByVal ds As DataSet) As DataSet

        Dim ErrCode As String = "9"
        Dim BatchNo As String = String.Concat(MyBase.GetSystemDate(), MyBase.GetSystemTime())

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN_ACQUISITHION")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim NrsBrCd As String = Me._Row.Item("IN_NRS_BR_CD").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append("$LM_TRN$..UP_CREATE_G_SEKY_MEISAI_PRT")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                        , NrsBrCd))
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 12000            '20分

        'パラメータ設定（請求フラグ）
        Dim SEKY_FLG As SqlParameter = cmd.Parameters.AddWithValue("@IN_SEKY_FLG", DBDataType.CHAR)
        SEKY_FLG.Direction = ParameterDirection.Input
        SEKY_FLG.Value = Me._Row.Item("IN_SEKY_FLG")

        'パラメータ設定（バッチ番号）
        Dim BATCH_NO As SqlParameter = cmd.Parameters.AddWithValue("@IN_BATCH_NO", DBDataType.CHAR)
        BATCH_NO.Direction = ParameterDirection.Input
        BATCH_NO.Value = BatchNo

        'パラメータ設定（営業所コード）
        Dim NRS_BR_CD As SqlParameter = cmd.Parameters.AddWithValue("@IN_NRS_BR_CD", DBDataType.CHAR)
        NRS_BR_CD.Direction = ParameterDirection.Input
        NRS_BR_CD.Value = NrsBrCd

        'パラメータ設定（実行ユーザーコード）
        Dim USER_ID As SqlParameter = cmd.Parameters.AddWithValue("@IN_OPE_USER_CD", DBDataType.CHAR)
        USER_ID.Direction = ParameterDirection.Input
        USER_ID.Value = Me._Row.Item("IN_OPE_USER_CD")

        '荷主コード（大）
        Dim CUST_CD_L As SqlParameter = cmd.Parameters.AddWithValue("@IN_CUST_CD_L", DBDataType.CHAR)
        CUST_CD_L.Direction = ParameterDirection.Input

        '荷主コード（中）
        Dim CUST_CD_M As SqlParameter = cmd.Parameters.AddWithValue("@IN_CUST_CD_M", DBDataType.CHAR)
        CUST_CD_M.Direction = ParameterDirection.Input

        '荷主コード（小）
        Dim CUST_CD_S As SqlParameter = cmd.Parameters.AddWithValue("@IN_CUST_CD_S", DBDataType.CHAR)
        CUST_CD_S.Direction = ParameterDirection.Input

        '荷主コード（極小）
        Dim CUST_CD_SS As SqlParameter = cmd.Parameters.AddWithValue("@IN_CUST_CD_SS", DBDataType.CHAR)
        CUST_CD_SS.Direction = ParameterDirection.Input

        'ジョブ番号
        Dim JOB_NO As SqlParameter = cmd.Parameters.AddWithValue("@IN_JOB_NO", DBDataType.CHAR)
        JOB_NO.Direction = ParameterDirection.Input
        JOB_NO.Value = Me._Row.Item("IN_JOB_NO")

        '起動元区分
        Dim MOTO_KBN As SqlParameter = cmd.Parameters.AddWithValue("@IN_MOTO_KBN", DBDataType.CHAR)
        MOTO_KBN.Direction = ParameterDirection.Input
        MOTO_KBN.Value = "00"

        'パラメータ設定（出力（処理結果））
        Dim OT_FLG_SEKY_ZERO As SqlParameter = cmd.Parameters.AddWithValue("@OT_FLG_SEKY_ZERO", DBDataType.NVARCHAR)
        OT_FLG_SEKY_ZERO.Direction = ParameterDirection.Output
        OT_FLG_SEKY_ZERO.Value = String.Empty

        'パラメータ設定（出力（処理結果））
        Dim OT_RTN_CD As SqlParameter = cmd.Parameters.AddWithValue("@OT_RTN_CD", DBDataType.NVARCHAR)
        OT_RTN_CD.Direction = ParameterDirection.Output
        OT_RTN_CD.Value = String.Empty

        'ストアドプロシージャーの呼び出し
        Dim rtnCd As SqlDataReader = Me.GetSelectResult(cmd)

        '処理結果判定
        If ErrCode.Equals(GetOutPutParam(cmd, "@OT_RTN_CD")) = True Then
            'エラーの場合、メッセージを設定
            MyBase.SetMessage("S001", New String() {"取込"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 取込排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AcquisithionHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN_BATCH_HAITA")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        With Me._Row
            Dim sql As String = BACTH_HAITA_SQL.Replace("@NRS_BR_CD", String.Concat("'", .Item("NRS_BR_CD").ToString(), "'"))
            sql = sql.Replace("@JOB_NO", String.Concat("'", .Item("JOB_NO").ToString(), "'"))
            Me._StrSql.Append(sql.Replace("@INV_DATE_TO", String.Concat("'", .Item("INV_DATE_TO").ToString(), "'")))
        End With

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMG030DAC", "AcquisithionHaita", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SQL_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '商品コード
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TBL.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                Me._StrSql.Append(vbNewLine)
                'START YANAI 要望番号886
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            '商品名
            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TBL.GOODS_NM_1 LIKE @GOODS_NM_1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主名称S_SS
            whereStr = .Item("CUST_NM_S_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TBL.CUST_NM_S_SS LIKE @CUST_NM_S_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_S_SS", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'ロット№
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKYP.LOT_NO LIKE @LOT_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKYP.SERIAL_NO LIKE @SERIAL_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '商品KEY
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKYP.GOODS_CD_NRS LIKE @GOODS_CD_NRS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If



        End With
    End Sub

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIkkatuSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '一期保管料単価
            whereStr = .Item("STORAGE1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.STORAGE1 = @STORAGE1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE1", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '一期保管料あり積数
            whereStr = .Item("SEKI_ARI_NB1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.SEKI_ARI_NB1 = @SEKI_ARI_NB1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKI_ARI_NB1", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '二期保管料単価
            whereStr = .Item("STORAGE2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.STORAGE2 = @STORAGE2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE2", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '二期保管料あり積数
            whereStr = .Item("SEKI_ARI_NB2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.SEKI_ARI_NB2 = @SEKI_ARI_NB2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKI_ARI_NB2", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '三期保管料単価
            whereStr = .Item("STORAGE3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.STORAGE3 = @STORAGE3")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE3", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '三期保管料あり積数
            whereStr = .Item("SEKI_ARI_NB3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.SEKI_ARI_NB3= @SEKI_ARI_NB3")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKI_ARI_NB3", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

            '保管料合計金額
            whereStr = .Item("STORAGE_AMO_TTL").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" G_SEKY_MEISAI_PRT.STRAGE_HENDO_NASHI_AMO_TTL= @STORAGE_AMO_TTL")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_AMO_TTL", String.Concat(whereStr), DBDataType.NUMERIC))
            End If

        End With
    End Sub

    ''' <summary>
    ''' 更新用SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIkkatuUpdatedataSQL()

        '検索条件部に入力された条件とパラメータ設定
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", .Item("JOB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CTL_NO", .Item("CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_PRT", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_PRT", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub
    'END YANAI 20111014 一括変更追加

    ''' <summary>
    ''' 更新用SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdatedataSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", .Item("JOB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CTL_NO", .Item("CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_IN1", .Item("HANDLING_IN1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_OUT1", .Item("HANDLING_OUT1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE1", .Item("STORAGE1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKI_ARI_NB1", .Item("SEKI_ARI_NB1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_IN2", .Item("HANDLING_IN2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_OUT2", .Item("HANDLING_OUT2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE2", .Item("STORAGE2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKI_ARI_NB2", .Item("SEKI_ARI_NB2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_IN3", .Item("HANDLING_IN3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_OUT3", .Item("HANDLING_OUT3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE3", .Item("STORAGE3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKI_ARI_NB3", .Item("SEKI_ARI_NB3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_NB_TTL1", .Item("INKO_NB_TTL1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_NB_TTL1", .Item("OUTKO_NB_TTL1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_AMO_TTL", .Item("STORAGE_AMO_TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_AMO_TTL", .Item("HANDLING_AMO_TTL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_PRT", .Item("SYS_UPD_DATE_PRT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_PRT", .Item("SYS_UPD_TIME_PRT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STRAGE_HENDO_NASHI_AMO_TTL", .Item("STRAGE_HENDO_NASHI_AMO_TTL").ToString(), DBDataType.NUMERIC))

        End With

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

#Region "言語取得"
    '20210628 ベトナム対応 add start
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable
        inTbl = ds.Tables("LMG030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG030DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function

    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '20210628 ベトナム対応 add End

#End Region

End Class
