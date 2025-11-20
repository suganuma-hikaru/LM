' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI670DAC : 売上仕入伝票(DIC鑑種別：A+鑑種別B（横持料))
'  作  成  者       :  寺川
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI670DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI670DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "帳票種別取得用"
    Private Const SQL_SELECT_MPrt As String = _
                      "	SELECT DISTINCT                                         " & vbNewLine _
                    & "	DSM.NRS_BR_CD                           AS NRS_BR_CD    " & vbNewLine _
                    & ",'AV'                                    AS PTN_ID       " & vbNewLine _
                    & ",MR3.PTN_CD                              AS PTN_CD       " & vbNewLine _
                    & ",MR3.RPT_ID                              AS RPT_ID       " & vbNewLine

    Private Const SQL_FROM_MPrt As String = _
                      " FROM                                                    " & vbNewLine _
                    & " $LM_TRN$..I_DIC_SEKY_MEISAI AS DSM                      " & vbNewLine _
                    & " --営業所マスタ                                          " & vbNewLine _
                    & " LEFT JOIN $LM_MST$..M_NRS_BR AS NRS                     " & vbNewLine _
                    & "   ON DSM.NRS_BR_CD      = NRS.NRS_BR_CD                 " & vbNewLine _
                    & "--存在しない場合の帳票パターン取得                       " & vbNewLine _
                    & " LEFT JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                    & "   ON MR3.NRS_BR_CD     = DSM.NRS_BR_CD                  " & vbNewLine _
                    & "  AND MR3.PTN_ID        = 'AV'                           " & vbNewLine _
                    & "  AND MR3.STANDARD_FLAG = '01'                           " & vbNewLine _
                    & "  AND MR3.SYS_DEL_FLG   = '0'                            " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "   WHERE DSM.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                    & "   AND   DSM.SYS_DEL_FLG = '0'                           " & vbNewLine

#End Region

#Region "印刷データ抽出用"

    Private Const SQL_SELECT As String = _
                      " SELECT                                                  " & vbNewLine _
                    & "  MAIN.RPT_ID                                            " & vbNewLine _
                    & " , MAIN.NRS_BR_NM         AS NRS_BR_NM                   " & vbNewLine _
                    & " , MAIN.SKYU_DATE         AS SKYU_DATE                   " & vbNewLine _
                    & " , sum(MAIN.HN_TTL )      AS HN_TTL                      " & vbNewLine _
                    & " , sum(MAIN.NIZUKURI)     AS NIZUKURI                    " & vbNewLine _
                    & " , sum(MAIN.YOKOMOCHI)    AS YOKOMOCHI                   " & vbNewLine _
                    & " FROM (                                                  " & vbNewLine

    Private Const SQL_SELECT_MAIN As String = _
                      " SELECT                                                  " & vbNewLine _
                    & "(SELECT  DISTINCT                                        " & vbNewLine _
                    & " MR3.RPT_ID            AS RPT_ID                         " & vbNewLine _
                    & " FROM                                                    " & vbNewLine _
                    & " $LM_TRN$..I_DIC_SEKY_MEISAI AS DSM                      " & vbNewLine _
                    & " --営業所マスタ                                          " & vbNewLine _
                    & " LEFT JOIN $LM_MST$..M_NRS_BR AS NRS                     " & vbNewLine _
                    & "   ON DSM.NRS_BR_CD      = NRS.NRS_BR_CD                 " & vbNewLine _
                    & "--存在しない場合の帳票パターン取得                       " & vbNewLine _
                    & " LEFT JOIN $LM_MST$..M_RPT MR3                           " & vbNewLine _
                    & "   ON  MR3.NRS_BR_CD    = NRS.NRS_BR_CD                  " & vbNewLine _
                    & "  AND MR3.PTN_ID        = 'AV'                           " & vbNewLine _
                    & "  AND MR3.STANDARD_FLAG = '01'                           " & vbNewLine _
                    & "  AND MR3.SYS_DEL_FLG   = '0'                            " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "   WHERE DSM.NRS_BR_CD   = @NRS_BR_CD                    " & vbNewLine _
                    & "   AND   DSM.SYS_DEL_FLG = '0'                           " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & ") RPT_ID                                                 " & vbNewLine _
                    & ", NRS.NRS_BR_NM    AS NRS_BR_NM                          " & vbNewLine _
                    & ", DSM.SKYU_DATE    AS SKYU_DATE                          " & vbNewLine _
                    & ", DSM.HN_TTL       AS HN_TTL                             " & vbNewLine _
                    & ", DSM.NIZUKURI     AS NIZUKURI                           " & vbNewLine _
                    & ", DSM.YOKOMOCHI    AS YOKOMOCHI                          " & vbNewLine

    Private Const SQL_FROM As String = _
                      "FROM $LM_TRN$..I_DIC_SEKY_MEISAI DSM                     " & vbNewLine _
                    & " --営業所マスタ                                          " & vbNewLine _
                    & "INNER JOIN $LM_MST$..M_NRS_BR NRS                        " & vbNewLine _
                    & "  ON  DSM.NRS_BR_CD      = NRS.NRS_BR_CD                 " & vbNewLine _
                    & "WHERE DSM.SYS_DEL_FLG  = '0'           --条件1(固定)     " & vbNewLine _
                    & "  AND DSM.NRS_BR_CD    = @NRS_BR_CD    --条件2＠         " & vbNewLine _
                    & "  AND DSM.SKYU_DATE    = @SKYU_DATE    --条件3＠         " & vbNewLine _
                    & "  AND DSM.SEIQTO_CD    = @SEIQTO_CD    --条件4＠         " & vbNewLine _
                    & "  AND DSM.KAGAMI_KB    = 'A'           --条件5(固定)     " & vbNewLine

    Private Const SQL_GROUP_BY As String = _
                    " ) AS MAIN                                                 " & vbNewLine _
                    & " GROUP BY                                                " & vbNewLine _
                    & "   MAIN.RPT_ID                                           " & vbNewLine _
                    & " , MAIN.NRS_BR_NM                                        " & vbNewLine _
                    & " , MAIN.SKYU_DATE                                        " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI670IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI670DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI670DAC.SQL_FROM_MPrt)        'SQL構築(帳票種別用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI670DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMI670IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI670DAC.SQL_SELECT)         'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI670DAC.SQL_SELECT_MAIN)    'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI670DAC.SQL_FROM)           'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMI670DAC.SQL_GROUP_BY)       'SQL構築(データ抽出用GroupBy句)
        Call Me.SetConditionMasterSQL()                 'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI670DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("HN_TTL", "HN_TTL")
        map.Add("NIZUKURI", "NIZUKURI")
        map.Add("YOKOMOCHI", "YOKOMOCHI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI670OUT")

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
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#End Region

End Class
