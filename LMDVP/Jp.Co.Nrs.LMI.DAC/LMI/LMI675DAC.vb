' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI675DAC : 鑑＋売上仕入伝票(DIC専用)
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI675DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI675DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "帳票種別取得用"
    Private Const SQL_SELECT_MPrt As String = _
                      "	SELECT DISTINCT                                         " & vbNewLine _
                    & "	DSM.NRS_BR_CD                           AS NRS_BR_CD    " & vbNewLine _
                    & ",'BL'                                    AS PTN_ID       " & vbNewLine _
                    & ",MR3.PTN_CD                              AS PTN_CD       " & vbNewLine _
                    & ",MR3.RPT_ID                              AS RPT_ID       " & vbNewLine

    Private Const SQL_FROM_MPrt As String = _
                      " FROM                                                    " & vbNewLine _
                    & " $$LM_TRN$$..I_DIC_SEKY_MEISAI AS DSM                      " & vbNewLine _
                    & " --営業所マスタ                                          " & vbNewLine _
                    & " LEFT JOIN $$LM_MST$$..M_NRS_BR AS NRS                     " & vbNewLine _
                    & "   ON DSM.NRS_BR_CD      = NRS.NRS_BR_CD                 " & vbNewLine _
                    & "--存在しない場合の帳票パターン取得                       " & vbNewLine _
                    & " LEFT JOIN $$LM_MST$$..M_RPT MR3                           " & vbNewLine _
                    & "   ON MR3.NRS_BR_CD     = DSM.NRS_BR_CD                  " & vbNewLine _
                    & "  AND MR3.PTN_ID        = 'BL'                           " & vbNewLine _
                    & "  AND MR3.STANDARD_FLAG = '01'                           " & vbNewLine _
                    & "  AND MR3.SYS_DEL_FLG   = '0'                            " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "   WHERE DSM.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                    & "   AND   DSM.SYS_DEL_FLG = '0'                           " & vbNewLine

#End Region

#Region "印刷データ抽出用"

    Private Const SQL_SELECT As String = _
                      "  SELECT                                                " & vbNewLine _
                    & "   MR1.RPT_ID             AS RPT_ID                     " & vbNewLine _
                    & "	, DSM.NRS_BR_CD          AS NRS_BR_CD                  " & vbNewLine _
                    & " , DSM.SKYU_DATE          AS SKYU_DATE                  " & vbNewLine _
                    & "	, DSM.TOKU_CD            AS TOKUI_CD                   " & vbNewLine _
                    & " , NRS.NRS_BR_NM          AS NRS_BR_NM                  " & vbNewLine _
                    & " , DSM.KAGAMI_KB          AS KAGAMI_KB                  " & vbNewLine _
                    & " , DSM.SEIQTO_CD          AS SEIQTO_CD                  " & vbNewLine _
                    & " , NRS.AD_1               AS AD_1                       " & vbNewLine _
                    & "	, NRS.AD_2               AS AD_2                       " & vbNewLine _
                    & "	, NRS.TEL                AS TEL                        " & vbNewLine _
                    & " , SEIQ.CUST_KAGAMI_TYPE4 AS KIGYO_CD                   " & vbNewLine _
                    & " , USR.USER_NM            AS USER_NM                    " & vbNewLine _
                    & " , SUM(DSM.HN_TTL)        AS HN_TTL                     " & vbNewLine _
                    & " , SUM(DSM.NIZUKURI)      AS NIZUKURI                   " & vbNewLine _
                    & " , SUM(DSM.YOKOMOCHI)     AS YOKOMOCHI                  " & vbNewLine _
                    & " , SUM(DSM.TTL)           AS TTL                        " & vbNewLine _
                    & " , SUM(DSM.TAX_TTL)       AS TAX_TTL                    " & vbNewLine _
                    & " , SUM(DSM.G_TTL)         AS G_TTL                      " & vbNewLine _
                    & " , DSM.TAX                AS TAX                        " & vbNewLine


    Private Const SQL_FROM As String = _
                      "  FROM $$LM_TRN$$..I_DIC_SEKY_MEISAI DSM                   " & vbNewLine _
                    & "  --営業所マスタ                                           " & vbNewLine _
                    & "  LEFT JOIN $$LM_MST$$..M_NRS_BR NRS                       " & vbNewLine _
                    & "  ON  DSM.NRS_BR_CD     = NRS.NRS_BR_CD                    " & vbNewLine _
                    & "--帳票マスタから取得                                       " & vbNewLine _
                    & "LEFT JOIN $$LM_MST$$..M_RPT MR1                            " & vbNewLine _
                    & "  ON MR1.NRS_BR_CD      = DSM.NRS_BR_CD                    " & vbNewLine _
                    & " AND MR1.PTN_ID         = 'BL'                             " & vbNewLine _
                    & " AND MR1.STANDARD_FLAG  = '01'                             " & vbNewLine _
                    & " AND MR1.SYS_DEL_FLG    = '0'                              " & vbNewLine _
                    & "  --請求先マスタ                                           " & vbNewLine _
                    & "  LEFT JOIN $$LM_MST$$..M_SEIQTO SEIQ                      " & vbNewLine _
                    & "  ON   DSM.NRS_BR_CD      = SEIQ.NRS_BR_CD                 " & vbNewLine _
                    & "	 AND  DSM.SEIQTO_CD      = SEIQ.SEIQTO_CD                 " & vbNewLine _
                    & "--荷主マスタ                                               " & vbNewLine _
                    & "  LEFT JOIN $$LM_MST$$..M_CUST CST                         " & vbNewLine _
                    & "  ON  DSM.NRS_BR_CD      = CST.NRS_BR_CD                   " & vbNewLine _
                    & "  AND CST.OYA_SEIQTO_CD  = @SEIQTO_CD    --条件＠          " & vbNewLine _
                    & "  AND CST.CUST_CD_M      =                                 " & vbNewLine _
                    & "  ( SELECT                                                 " & vbNewLine _
                    & "    MIN(CST.CUST_CD_M) AS CUST_CD_M                        " & vbNewLine _
                    & "    FROM $$LM_MST$$..M_CUST CST                            " & vbNewLine _
                    & "    WHERE                                                  " & vbNewLine _
                    & "           CST.NRS_BR_CD         = @NRS_BR_CD    --条件＠  " & vbNewLine _
                    & "       AND CST.OYA_SEIQTO_CD     = @SEIQTO_CD  ) --条件＠  " & vbNewLine _
                    & "  AND CST.CUST_CD_S      =                                 " & vbNewLine _
                    & "  ( SELECT                                                 " & vbNewLine _
                    & "    MIN(CST.CUST_CD_S) AS CUST_CD_S                        " & vbNewLine _
                    & "    FROM $$LM_MST$$..M_CUST CST                            " & vbNewLine _
                    & "    WHERE                                                  " & vbNewLine _
                    & "           CST.NRS_BR_CD         = @NRS_BR_CD    --条件＠  " & vbNewLine _
                    & "       AND CST.OYA_SEIQTO_CD     = @SEIQTO_CD  ) --条件＠  " & vbNewLine _
                    & "  AND CST.CUST_CD_SS      =                                " & vbNewLine _
                    & "  ( SELECT                                                 " & vbNewLine _
                    & "    MIN(CST.CUST_CD_SS) AS CUST_CD_SS                      " & vbNewLine _
                    & "    FROM $$LM_MST$$..M_CUST CST                            " & vbNewLine _
                    & "    WHERE                                                  " & vbNewLine _
                    & "       CST.NRS_BR_CD         = @NRS_BR_CD    --条件＠      " & vbNewLine _
                    & "   AND CST.OYA_SEIQTO_CD     = @SEIQTO_CD  ) --条件＠      " & vbNewLine _
                    & "--ユーザマスタ                                             " & vbNewLine _
                    & "  LEFT JOIN $$LM_MST$$..S_USER USR                         " & vbNewLine _
                    & "  ON    DSM.NRS_BR_CD    = USR.NRS_BR_CD                   " & vbNewLine _
                    & "  AND   USR.USER_CD = @LOGINID                             " & vbNewLine _
                    & "  WHERE DSM.SYS_DEL_FLG  = '0'           --条件(固定)      " & vbNewLine _
                    & "   AND DSM.NRS_BR_CD     = @NRS_BR_CD    --条件＠          " & vbNewLine _
                    & "   AND DSM.SKYU_DATE     = @SKYU_DATE    --条件＠          " & vbNewLine _
                    & "   AND DSM.SEIQTO_CD     = @SEIQTO_CD    --条件＠          " & vbNewLine




    Private Const SQL_GROUP_BY As String = _
                     "   GROUP BY                                              " & vbNewLine _
                   & "   MR1.RPT_ID                                            " & vbNewLine _
                   & " , DSM.NRS_BR_CD                                         " & vbNewLine _
                   & " , DSM.SKYU_DATE                                         " & vbNewLine _
                   & " , DSM.SEIQTO_CD                                         " & vbNewLine _
                   & " , DSM.KAGAMI_KB                                         " & vbNewLine _
                   & " , NRS.NRS_BR_NM                                         " & vbNewLine _
                   & " , NRS.AD_1                                              " & vbNewLine _
                   & " , NRS.AD_2                                              " & vbNewLine _
                   & " , NRS.TEL                                               " & vbNewLine _
                   & " , DSM.TOKU_CD                                           " & vbNewLine _
                   & " , SEIQ.CUST_KAGAMI_TYPE4                                " & vbNewLine _
                   & " , USR.USER_NM                                           " & vbNewLine _
                   & " , DSM.TAX                                               " & vbNewLine
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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI675IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI675DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI675DAC.SQL_FROM_MPrt)        'SQL構築(帳票種別用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI675DAC", "SelectMPrt", cmd)

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
    ''' 印刷対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>売上仕入伝票(DIC鑑種別：A+鑑種別B（横持料）)の構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI675IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI675DAC.SQL_SELECT)         'SQL構築(データ抽出用Select句)
        'Me._StrSql.Append(LMI675DAC.SQL_SELECT_MAIN)    'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI675DAC.SQL_FROM)           'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI675DAC.SQL_GROUP_BY)       'SQL構築(データ抽出用GroupBy句)
        Call Me.SetConditionMasterSQL()                 'SQL構築(条件設定)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOGINID", MyBase.GetUserID(), DBDataType.NVARCHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI675DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("KAGAMI_KB", "KAGAMI_KB")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("TEL", "TEL")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("KIGYO_CD", "KIGYO_CD")
        map.Add("USER_NM", "USER_NM")
        map.Add("HN_TTL", "HN_TTL")
        map.Add("NIZUKURI", "NIZUKURI")
        map.Add("YOKOMOCHI", "YOKOMOCHI")
        map.Add("TTL", "TTL")
        map.Add("TAX_TTL", "TAX_TTL")
        map.Add("G_TTL", "G_TTL")
        map.Add("TAX", "TAX")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI675OUT")

        Return ds

    End Function

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

            '営業所コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '請求日
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", Me._Row.Item("SKYU_DATE").ToString(), DBDataType.CHAR))

            '請求先コード
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub


#End Region

#Region "設定処理"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$$LM_TRN$$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$$LM_MST$$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#End Region

End Class
