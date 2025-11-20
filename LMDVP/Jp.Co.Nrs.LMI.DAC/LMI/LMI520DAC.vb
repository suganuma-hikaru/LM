' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主
'  プログラムID     :  LMI520DAC : コストデータ一覧(ダウ・ケミカル用)
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI520DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI520DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用(1-1)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                       " & vbNewLine _
                                            & "	IDSP.NRS_BR_CD                                        AS NRS_BR_CD   " & vbNewLine _
                                            & ",'82'                                                  AS PTN_ID      " & vbNewLine _
                                            & ",CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	 ELSE MR2.PTN_CD END                              AS PTN_CD      " & vbNewLine _
                                            & ",CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                     " & vbNewLine _
                                            & "		 ELSE MR2.RPT_ID END                              AS RPT_ID      " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用(2-1)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                 " & vbNewLine _
                                        & "(SELECT  DISTINCT                                           " & vbNewLine _
                                        & "        CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID    " & vbNewLine _
                                        & "        ELSE MR2.RPT_ID END       AS RPT_ID                 " & vbNewLine _
                                        & "  FROM                                                      " & vbNewLine _
                                        & "       $LM_TRN$..I_DOW_SEIQ_PRT IDSP                        " & vbNewLine _
                                        & "                                                            " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                         " & vbNewLine _
                                        & "  ON IDSP.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                        & " AND IDSP.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                        & " AND IDSP.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                        & " AND MCR1.PTN_ID    = '82'                                  " & vbNewLine _
                                        & "--帳票パターン取得                                          " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR1                               " & vbNewLine _
                                        & "  ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                       " & vbNewLine _
                                        & " AND MR1.PTN_ID      = MCR1.PTN_ID                          " & vbNewLine _
                                        & " AND MR1.PTN_CD      = MCR1.PTN_CD                          " & vbNewLine _
                                        & " AND MR1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                        & "--存在しない場合の帳票パターン取得                          " & vbNewLine _
                                        & "LEFT JOIN $LM_MST$..M_RPT MR2                               " & vbNewLine _
                                        & "ON  MR2.NRS_BR_CD         = IDSP.NRS_BR_CD                  " & vbNewLine _
                                        & "AND MR2.PTN_ID            = '82'                            " & vbNewLine _
                                        & "AND MR2.STANDARD_FLAG     = '01'                            " & vbNewLine _
                                        & "AND MR2.SYS_DEL_FLG       = '0'                             " & vbNewLine _
                                        & "                                                            " & vbNewLine _
                                        & "  WHERE IDSP.NRS_BR_CD     = @NRS_BR_CD --条件1＠           " & vbNewLine _
                                        & "    AND IDSP.SYS_DEL_FLG   = '0'        --条件2(固定)       " & vbNewLine _
                                        & ") RPT_ID                                                    " & vbNewLine


#End Region

#Region "FROM句"

    ''' <summary>
    ''' 帳票種別取得用(SQL_FROM_MPrt:1-2 終 SQL_FROM:2-2 終)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                                        " & vbNewLine _
                                          & " $LM_TRN$..I_DOW_SEIQ_PRT IDSP                              " & vbNewLine _
                                          & "--主テーブルからの荷主帳票パターン取得                      " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                         " & vbNewLine _
                                          & "  ON IDSP.NRS_BR_CD = MCR1.NRS_BR_CD                        " & vbNewLine _
                                          & " AND IDSP.CUST_CD_L = MCR1.CUST_CD_L                        " & vbNewLine _
                                          & " AND IDSP.CUST_CD_M = MCR1.CUST_CD_M                        " & vbNewLine _
                                          & " AND MCR1.PTN_ID    = '82'                                  " & vbNewLine _
                                          & "--帳票パターン取得                                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR1                               " & vbNewLine _
                                          & "  ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & " AND MR1.PTN_ID      = MCR1.PTN_ID                          " & vbNewLine _
                                          & " AND MR1.PTN_CD      = MCR1.PTN_CD                          " & vbNewLine _
                                          & " AND MR1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                          & "--存在しない場合の帳票パターン取得                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR2                               " & vbNewLine _
                                          & " ON MR2.NRS_BR_CD         = IDSP.NRS_BR_CD                  " & vbNewLine _
                                          & "AND MR2.PTN_ID            = '82'                            " & vbNewLine _
                                          & "AND MR2.STANDARD_FLAG     = '01'                            " & vbNewLine _
                                          & "AND MR2.SYS_DEL_FLG       = '0'                             " & vbNewLine _
                                          & "  WHERE IDSP.NRS_BR_CD     = @NRS_BR_CD --条件1＠           " & vbNewLine _
                                          & "    AND IDSP.SYS_DEL_FLG   = '0'        --条件2(固定)       " & vbNewLine


    Private Const SQL_FROM As String = "   ,IDSP.NRS_BR_CD		AS	NRS_BR_CD	 --	営業所コード	 " & vbNewLine _
                                        & ",MNB.NRS_BR_NM		AS	NRS_BR_NM	 --	営業所名	 	 " & vbNewLine _
                                        & ",IDSP.SEIQ_YM		AS	SEIQ_YM	     --	請求月	 		 " & vbNewLine _
                                        & ",IDSP.CUST_CD_L		AS	CUST_CD_L	 --	荷主コード(大)	 " & vbNewLine _
                                        & ",IDSP.CUST_CD_M		AS	CUST_CD_M	 --	荷主コード(中)	 " & vbNewLine _
                                        & ",CUST.CUST_NM_L		AS	CUST_NM_L	 --	荷主名称　(大)	 " & vbNewLine _
                                        & ",CUST.CUST_NM_M		AS	CUST_NM_M	 --	荷主名称　(中)	 " & vbNewLine _
                                        & ",IDSP.REC_TYPE		AS	REC_TYPE	 --	請求種別	 	 " & vbNewLine _
                                        & ",IDSP.ID	        	AS	ID	         --	ID	 			 " & vbNewLine _
                                        & ",IDSP.SHORI_YM		AS	SHORI_YM	 --	処理月	 		 " & vbNewLine _
                                        & ",IDSP.IN_KAISHA		AS	IN_KAISHA	 --	入力BP	 		 " & vbNewLine _
                                        & ",IDSP.KAISHA_CD		AS	KAISHA_CD	 --	会社コード	 	 " & vbNewLine _
                                        & ",IDSP.DV_NO	    	AS	DV_NO     	 --	デリバリー№	 " & vbNewLine _
                                        & ",IDSP.GMID	        AS	GMID	     --	GMID	 		 " & vbNewLine _
                                        & ",IDSP.GOODS_NM       AS	GOODS_NM	 --	商品名	 		 " & vbNewLine _
                                        & ",IDSP.COST			AS	COST		 --	コスト	 		 " & vbNewLine _
                                        & ",IDSP.HIYO			AS	HIYO	     --	費用BP	 		 " & vbNewLine _
                                        & ",IDSP.TUKA			AS	TUKA	     --	通貨	         " & vbNewLine _
                                        & ",IDSP.GAKU			AS	GAKU		 --	金額	 		 " & vbNewLine _
                                        & ",IDSP.FUGO			AS	FUGO		 --	符号	 		 " & vbNewLine _
                                        & ",IDSP.HASSEI_YM		AS	HASSEI_YM	 --	発生日	 		 " & vbNewLine _
                                        & ",IDSP.SHIP_NO		AS	SHIP_NO	     --	シップメント№	 " & vbNewLine _
                                        & ",IDSP.WT				AS	WT			 --	重量	 		 " & vbNewLine _
                                        & ",IDSP.KYORI			AS	KYORI		 --	距離	 		 " & vbNewLine _
                                        & "  FROM                                                    " & vbNewLine _
                                        & "       $LM_TRN$..I_DOW_SEIQ_PRT IDSP                      " & vbNewLine _
                                        & "       LEFT JOIN $LM_MST$..M_NRS_BR MNB                   " & vbNewLine _
                                        & "               ON IDSP.NRS_BR_CD       = MNB.NRS_BR_CD    " & vbNewLine _
                                        & "              AND MNB.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                        & "       LEFT JOIN $LM_MST$..M_CUST CUST                    " & vbNewLine _
                                        & "               ON IDSP.NRS_BR_CD       = CUST.NRS_BR_CD   " & vbNewLine _
                                        & "              AND IDSP.CUST_CD_L  = CUST.CUST_CD_L        " & vbNewLine _
                                        & "              AND IDSP.CUST_CD_M  = CUST.CUST_CD_M        " & vbNewLine _
                                        & "              AND CUST.CUST_CD_S  = '00'                  " & vbNewLine _
                                        & "              AND CUST.CUST_CD_SS = '00'                  " & vbNewLine _
                                        & "              AND CUST.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                        & "  WHERE IDSP.NRS_BR_CD     = @NRS_BR_CD --条件1＠         " & vbNewLine
#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                 " & vbNewLine _
                                         & " IDSP.NRS_BR_CD ASC      " & vbNewLine _
                                         & ",IDSP.KAISHA_CD ASC      " & vbNewLine _
                                         & ",IDSP.REC_TYPE  ASC      " & vbNewLine _
                                         & ",IDSP.COST      ASC      " & vbNewLine _
                                         & ",IDSP.GMID      ASC      " & vbNewLine _
                                         & ",IDSP.DV_NO     ASC      " & vbNewLine _
                                         & ",IDSP.SHIP_NO   ASC      " & vbNewLine _
                                         & ",IDSP.GAKU      ASC      " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMI520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI520DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMI520DAC.SQL_FROM_MPrt)        'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI520DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI520DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMI520DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionReportSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMI520DAC.SQL_ORDER_BY)             'SQL構築(データ抽出用ORDER BY)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI520DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SEIQ_YM", "SEIQ_YM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("REC_TYPE", "REC_TYPE")
        map.Add("ID", "ID")
        map.Add("SHORI_YM", "SHORI_YM")
        map.Add("IN_KAISHA", "IN_KAISHA")
        map.Add("KAISHA_CD", "KAISHA_CD")
        map.Add("DV_NO", "DV_NO")
        map.Add("GMID", "GMID")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("COST", "COST")
        map.Add("HIYO", "HIYO")
        map.Add("TUKA", "TUKA")
        map.Add("GAKU", "GAKU")
        map.Add("FUGO", "FUGO")
        map.Add("HASSEI_YM", "HASSEI_YM")
        map.Add("SHIP_NO", "SHIP_NO")
        map.Add("WT", "WT")
        map.Add("KYORI", "KYORI")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI520OUT")

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

        End With

    End Sub


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionReportSQL()

        'Me._StrSql.Append(" WHERE ")
        'Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            ''営業所コード
            'whereStr = .Item("NRS_BR_CD").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" IDSP.NRS_BR_CD = @NRS_BR_CD")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            'End If

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))


            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IDSP.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IDSP.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '日付From
            whereStr = Left(.Item("F_DATE").ToString(), 6)
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IDSP.SEIQ_YM = @F_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@F_DATE", whereStr, DBDataType.CHAR))
            End If

            '帳票種別
            whereStr = .Item("SYORI_KB").ToString()
            Select Case whereStr
                Case "01"
                    Me._StrSql.Append(" AND ((IDSP.REC_TYPE = '1') OR (IDSP.REC_TYPE = '2') OR (IDSP.REC_TYPE = '3') )")
                Case "02"
                    Me._StrSql.Append(" AND IDSP.REC_TYPE = '1'")
                Case "03"
                    Me._StrSql.Append(" AND IDSP.REC_TYPE = '2'")
                Case "04"
                    Me._StrSql.Append(" AND IDSP.REC_TYPE = '3'")
            End Select

            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYORI_KB", whereStr, DBDataType.CHAR))

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
