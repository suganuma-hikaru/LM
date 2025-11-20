' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF680    : 梱包明細
'  作  成  者       :  hojo
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF680DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF680DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    Private Const PTN_ID As String = "C2"

    Class TABLE_NM
        Public Const INPUT As String = "LMF680IN"
        Public Const OUTPUT As String = "LMF680OUT"
        Public Const M_RPT As String = "M_RPT"

    End Class

    Class COLUM_NM
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const UNSO_NO_L As String = "UNSO_NO_L"
        Public Const PTN_ID As String = "PTN_ID"
        Public Const PTN_CD As String = "PTN_CD"
        Public Const RPT_ID As String = "RPT_ID"

        Public Const CUST_CD_L As String = "CUST_CD_L"
        Public Const CUST_NM_L As String = "CUST_NM_L"
        Public Const GOODS_CD_CUST As String = "GOODS_CD_CUST"
        Public Const GOODS_CD_NRS As String = "GOODS_CD_NRS"
        Public Const GOODS_NM As String = "GOODS_NM"
        Public Const IRIME As String = "IRIME"
        Public Const IRIME_UT As String = "IRIME_UT"
        Public Const UNSO_TTL_NB As String = "UNSO_TTL_NB"
        Public Const NB_UT As String = "NB_UT"
        Public Const UNSO_TTL_QT As String = "UNSO_TTL_QT"
        Public Const LOT_NO As String = "LOT_NO"

    End Class

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String _
        = " SELECT DISTINCT                                                       " & vbNewLine _
        & "        FUL.NRS_BR_CD                                    AS NRS_BR_CD  " & vbNewLine _
        & "      , @PTN_ID                                          AS PTN_ID     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD               " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
        & "        ELSE MR3.PTN_CD                                                " & vbNewLine _
        & "        END                                              AS PTN_CD     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
        & "        ELSE MR3.RPT_ID                                                " & vbNewLine _
        & "        END                                              AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String _
        = " SELECT                                                  " & vbNewLine _
        & "  CASE WHEN MR2.RPT_ID IS NOT NULL                       " & vbNewLine _
        & "      THEN MR2.RPT_ID                                    " & vbNewLine _
        & "      WHEN MR1.RPT_ID IS NOT NULL                        " & vbNewLine _
        & "      THEN MR1.RPT_ID                                    " & vbNewLine _
        & "      ELSE MR3.RPT_ID                                    " & vbNewLine _
        & "  END                        AS RPT_ID                   " & vbNewLine _
        & " ,FUL.NRS_BR_CD				AS NRS_BR_CD                " & vbNewLine _
        & " ,FUM.UNSO_NO_L				AS UNSO_NO_L                " & vbNewLine _
        & " ,FUL.CUST_CD_L				AS CUST_CD_L                " & vbNewLine _
        & " ,MC.CUST_NM_L				AS CUST_NM_L                " & vbNewLine _
        & " ,MG.GOODS_CD_CUST			AS GOODS_CD_CUST            " & vbNewLine _
        & " ,FUM.GOODS_CD_NRS			AS GOODS_CD_NRS             " & vbNewLine _
        & " ,FUM.GOODS_NM				AS GOODS_NM                 " & vbNewLine _
        & " ,FUM.IRIME					AS IRIME                    " & vbNewLine _
        & " ,FUM.IRIME_UT				AS IRIME_UT                 " & vbNewLine _
        & " ,FUM.UNSO_TTL_NB			AS UNSO_TTL_NB              " & vbNewLine _
        & " ,FUM.NB_UT					AS NB_UT                    " & vbNewLine _
        & " ,FUM.UNSO_TTL_QT			AS UNSO_TTL_QT              " & vbNewLine _
        & " ,FUM.LOT_NO     			AS LOT_NO                   " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String _
        = " FROM $LM_TRN$..F_UNSO_L FUL                             " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & " 	(                                                   " & vbNewLine _
        & " 		SELECT                                          " & vbNewLine _
        & " 			 NRS_BR_CD                                  " & vbNewLine _
        & " 			,UNSO_NO_L                                  " & vbNewLine _
        & " 			,UNSO_NO_M                                  " & vbNewLine _
        & " 			,GOODS_CD_NRS                               " & vbNewLine _
        & " 			,GOODS_NM                                   " & vbNewLine _
        & " 			,IRIME                                      " & vbNewLine _
        & " 			,IRIME_UT                                   " & vbNewLine _
        & " 			,NB_UT                                      " & vbNewLine _
        & " 			,LOT_NO                                     " & vbNewLine _
        & " 			,SUM(UNSO_TTL_NB) AS UNSO_TTL_NB            " & vbNewLine _
        & " 			,SUM(UNSO_TTL_QT) AS UNSO_TTL_QT            " & vbNewLine _
        & " 		FROM $LM_TRN$..F_UNSO_M                         " & vbNewLine _
        & " 		WHERE SYS_DEL_FLG = '0'                         " & vbNewLine _
        & " 		GROUP BY                                        " & vbNewLine _
        & " 			 NRS_BR_CD                                  " & vbNewLine _
        & " 			,UNSO_NO_L                                  " & vbNewLine _
        & " 			,UNSO_NO_M                                  " & vbNewLine _
        & " 			,GOODS_CD_NRS                               " & vbNewLine _
        & " 			,GOODS_NM                                   " & vbNewLine _
        & " 			,IRIME                                      " & vbNewLine _
        & " 			,IRIME_UT                                   " & vbNewLine _
        & " 			,NB_UT                                      " & vbNewLine _
        & " 			,LOT_NO		                                " & vbNewLine _
        & " 	)FUM                                                " & vbNewLine _
        & " ON  FUM.NRS_BR_CD = FUL.NRS_BR_CD                       " & vbNewLine _
        & " AND FUM.UNSO_NO_L = FUL.UNSO_NO_L                       " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_GOODS AS MG                            " & vbNewLine _
        & "   ON MG.GOODS_CD_NRS = FUM.GOODS_CD_NRS                 " & vbNewLine _
        & "  AND MG.NRS_BR_CD    = FUM.NRS_BR_CD                    " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_CUST  AS MC                            " & vbNewLine _
        & "   ON MC.NRS_BR_CD    = MG.NRS_BR_CD                     " & vbNewLine _
        & "  AND MC.CUST_CD_L    = MG.CUST_CD_L                     " & vbNewLine _
        & "  AND MC.CUST_CD_M    = MG.CUST_CD_M                     " & vbNewLine _
        & "  AND MC.CUST_CD_S    = MG.CUST_CD_S                     " & vbNewLine _
        & "  AND MC.CUST_CD_SS   = MG.CUST_CD_SS                    " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_CUST_RPT AS MCR1                       " & vbNewLine _
        & "   ON MCR1.NRS_BR_CD = FUL.NRS_BR_CD                     " & vbNewLine _
        & "  AND MCR1.CUST_CD_L = FUL.CUST_CD_L                     " & vbNewLine _
        & "  AND MCR1.CUST_CD_M = FUL.CUST_CD_M                     " & vbNewLine _
        & "  AND MCR1.CUST_CD_S = '00'                              " & vbNewLine _
        & "  AND MCR1.PTN_ID    = @PTN_ID                           " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_RPT AS MR1                             " & vbNewLine _
        & "   ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                   " & vbNewLine _
        & "  AND MR1.PTN_ID      = MCR1.PTN_ID                      " & vbNewLine _
        & "  AND MR1.PTN_CD      = MCR1.PTN_CD                      " & vbNewLine _
        & "  AND MR1.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_CUST_RPT AS MCR2                       " & vbNewLine _
        & "   ON MCR2.NRS_BR_CD = MG.NRS_BR_CD                      " & vbNewLine _
        & "  AND MCR2.CUST_CD_L = MG.CUST_CD_L                      " & vbNewLine _
        & "  AND MCR2.CUST_CD_M = MG.CUST_CD_M                      " & vbNewLine _
        & "  AND MCR2.CUST_CD_S = MG.CUST_CD_S                      " & vbNewLine _
        & "  AND MCR2.PTN_ID    = @PTN_ID                           " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_RPT AS MR2                             " & vbNewLine _
        & "   ON MR2.NRS_BR_CD   = MCR2.NRS_BR_CD                   " & vbNewLine _
        & "  AND MR2.PTN_ID      = MCR2.PTN_ID                      " & vbNewLine _
        & "  AND MR2.PTN_CD      = MCR2.PTN_CD                      " & vbNewLine _
        & "  AND MR2.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & " LEFT JOIN                                               " & vbNewLine _
        & "      $LM_MST$..M_RPT AS MR3                             " & vbNewLine _
        & "   ON MR3.NRS_BR_CD      = FUL.NRS_BR_CD                 " & vbNewLine _
        & "  AND MR3.PTN_ID         = @PTN_ID                       " & vbNewLine _
        & "  AND MR3.STANDARD_FLAG  = '01'                          " & vbNewLine _
        & "  AND MR3.SYS_DEL_FLG    = '0'                           " & vbNewLine


    Private Const SQL_WHERE As String _
        = " WHERE                                                   " & vbNewLine _
        & "     FUL.NRS_BR_CD   = @NRS_BR_CD                        " & vbNewLine _
        & " AND FUL.UNSO_NO_L   = @UNSO_NO_L                        " & vbNewLine _
        & " AND FUL.SYS_DEL_FLG = '0'                               " & vbNewLine


    Private Const SQL_GROUP_BY As String _
        = "  GROUP BY                                               " & vbNewLine _
        & "        MR1.RPT_ID                                       " & vbNewLine _
        & "      , MR2.RPT_ID                                       " & vbNewLine _
        & "      , MR3.RPT_ID                                       " & vbNewLine _
        & "      , FUL.NRS_BR_CD				                    " & vbNewLine _
        & "      , FUM.UNSO_NO_L				                    " & vbNewLine _
        & "      , FUL.CUST_CD_L				                    " & vbNewLine _
        & "      , MC.CUST_NM_L				                        " & vbNewLine _
        & " 	 , MG.GOODS_CD_CUST                                 " & vbNewLine _
        & "      , FUM.GOODS_CD_NRS			                        " & vbNewLine _
        & "      , FUM.GOODS_NM				                        " & vbNewLine _
        & "      , FUM.IRIME				                        " & vbNewLine _
        & "      , FUM.IRIME_UT				                        " & vbNewLine _
        & "      , FUM.UNSO_TTL_NB			                        " & vbNewLine _
        & "      , FUM.NB_UT				                        " & vbNewLine _
        & "      , FUM.UNSO_TTL_QT			                        " & vbNewLine _
        & "      , FUM.LOT_NO    			                        " & vbNewLine
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String _
        = "  ORDER BY                                               " & vbNewLine _
        & "        FUM.UNSO_NO_L                                        " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                    " & vbNewLine _
        & "      , FUM.LOT_NO                                           " & vbNewLine _
        & "      , FUM.IRIME DESC                                       " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INPUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF680DAC.SQL_SELECT_MPrt)
        Me._StrSql.Append(LMF680DAC.SQL_FROM)
        Me._StrSql.Append(LMF680DAC.SQL_WHERE)

        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item(COLUM_NM.UNSO_NO_L).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMF680DAC.PTN_ID, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                    , System.Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim selectCount As Integer = 0

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If (reader.HasRows) Then

                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In New String() _
                        {
                            COLUM_NM.NRS_BR_CD,
                            COLUM_NM.PTN_ID,
                            COLUM_NM.PTN_CD,
                            COLUM_NM.RPT_ID
                        }

                        map.Add(item, item)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.M_RPT)

                    selectCount = ds.Tables(TABLE_NM.M_RPT).Rows.Count

                End If

            End Using

            Me.SetResultCount(selectCount)

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INPUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF680DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMF680DAC.SQL_FROM)
        Me._StrSql.Append(LMF680DAC.SQL_WHERE)
        Me._StrSql.Append(LMF680DAC.SQL_GROUP_BY)
        Me._StrSql.Append(LMF680DAC.SQL_ORDER_BY)


        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item(COLUM_NM.UNSO_NO_L).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMF680DAC.PTN_ID, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                    , System.Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim selectCount As Integer = 0

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                For Each item As String In New String() _
                    {
                        COLUM_NM.RPT_ID,
                        COLUM_NM.NRS_BR_CD,
                        COLUM_NM.UNSO_NO_L,
                        COLUM_NM.CUST_CD_L,
                        COLUM_NM.CUST_NM_L,
                        COLUM_NM.GOODS_CD_CUST,
                        COLUM_NM.GOODS_CD_NRS,
                        COLUM_NM.GOODS_NM,
                        COLUM_NM.IRIME,
                        COLUM_NM.IRIME_UT,
                        COLUM_NM.UNSO_TTL_NB,
                        COLUM_NM.NB_UT,
                        COLUM_NM.UNSO_TTL_QT,
                        COLUM_NM.LOT_NO
                    }

                    map.Add(item, item)
                Next

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUTPUT)

            End Using

            Me.SetResultCount(selectCount)

        End Using


        Return ds

    End Function


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
