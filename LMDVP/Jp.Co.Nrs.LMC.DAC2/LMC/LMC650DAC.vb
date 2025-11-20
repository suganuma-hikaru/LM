' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC650    : 詰め合わせ明細書
'  作  成  者       :  [SHINOHARA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC650DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC650DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	OUTL.NRS_BR_CD                                           AS NRS_BR_CD " & vbNewLine _
                                            & ",@PTN_ID                                                  AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                      " & vbNewLine _
                                            & "	 	 ELSE MR3.PTN_CD END                                 AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  	 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                            & "		 ELSE MR3.RPT_ID END                                 AS RPT_ID    " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "--出荷L                                                                      " & vbNewLine _
                                     & "FROM                                                                         " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                                     " & vbNewLine _
                                     & "--マスタテーブル                                                             " & vbNewLine _
                                     & "--商品M                                                                      " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                               " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = OUTL.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = @GOODS_CD_NRS                                          " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                          " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                          " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                          " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                                    " & vbNewLine _
                                     & "AND MCR1.PTN_ID = @PTN_ID                                                    " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                                " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                                        " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                          " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                            " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                            " & vbNewLine _
                                     & "AND MCR2.PTN_ID = @PTN_ID                                                    " & vbNewLine _
                                     & "--帳票パターン取得                                                           " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                                " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                                 " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                                 " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                           " & vbNewLine _
                                     & "LEFT LOOP JOIN $LM_MST$..M_RPT MR3                                           " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                           " & vbNewLine _
                                     & "AND MR3.PTN_ID = @PTN_ID                                                     " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                                 " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                     & "WHERE                                                                        " & vbNewLine _
                                     & "OUTL.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine _
                                     & "AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                            " & vbNewLine _
                                     & "AND OUTL.SYS_DEL_FLG = '0'                                                   " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMC650INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMC650DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC650DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC650DAC", "SelectMPrt", cmd)

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
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'Me._Rowには必ず値が設定されているため、string.isnullorEmptyの判定をしない
            'Me._Rowに値がない場合は、Row.count=0のため、BLCで処理が終わってしまい、ここまでこない

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '出荷管理番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            '商品キー
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            'パターンID
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", "96", DBDataType.CHAR))

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
