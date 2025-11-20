' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB501    : 入荷受付表
'  作  成  者       :  [kikuchi]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB501DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB501DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = _
                                              "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	INL.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'01'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE  WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                     " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_Mprt As String = _
                                       "FROM                                                            " & vbNewLine _
                                     & "--入荷L                                                         " & vbNewLine _
                                     & "$LM_TRN$..B_INKA_L INL                                          " & vbNewLine _
                                     & "LEFT join $LM_TRN$..B_INKA_M INM                     " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = INM.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INL.INKA_NO_L = INM.INKA_NO_L                     " & vbNewLine _
                                     & "--入荷M                     " & vbNewLine _
                                     & "LEFT join $LM_TRN$..B_INKA_S INS                     " & vbNewLine _
                                     & "ON  INM.NRS_BR_CD = INS.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INM.INKA_NO_L = INS.INKA_NO_L                     " & vbNewLine _
                                     & "AND INM.INKA_NO_M = INS.INKA_NO_M                     " & vbNewLine _
                                     & "AND INM.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                     & "--入荷S                     " & vbNewLine _
                                     & "LEFT join $LM_MST$..M_GOODS MG                     " & vbNewLine _
                                     & "ON  INM.NRS_BR_CD = MG.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                     " & vbNewLine _
                                     & "AND INS.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                     & "--運送L                     " & vbNewLine _
                                     & "LEFT join $LM_TRN$..F_UNSO_L UL                     " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = UL.NRS_BR_CD                     " & vbNewLine _
                                     & "AND  INL.INKA_NO_L = UL.INOUTKA_NO_L                     " & vbNewLine _
                                     & "AND  UL.MOTO_DATA_KB = '10'                     " & vbNewLine _
                                     & "AND UL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                     & "--入荷Lでの荷主帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                     " & vbNewLine _
                                     & "ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                     " & vbNewLine _
                                     & "AND INL.CUST_CD_L = MCR1.CUST_CD_L                     " & vbNewLine _
                                     & "AND INL.CUST_CD_M = MCR1.CUST_CD_M                     " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                     " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '01'                     " & vbNewLine _
                                     & "--帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                     " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                      " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                     " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                     " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                     " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                     " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                     " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                     " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                     " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '01'                     " & vbNewLine _
                                     & "--帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                     " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                      " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                     " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                     " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                     " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                     " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = INL.NRS_BR_CD                      " & vbNewLine _
                                     & "AND MR3.PTN_ID = '01'                      " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                     " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "	  WHERE INL.NRS_BR_CD  = @NRS_BR_CD                  	" & vbNewLine _
                                     & "	   AND INL.INKA_NO_L   = @INKA_NO_L                  	" & vbNewLine _
                                     & "	   AND INL.SYS_DEL_FLG = '0'                         	" & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINT_DATA As String = _
              "     SELECT                                                                               " & vbNewLine _
            & "          CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                " & vbNewLine _
            & "  		      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                " & vbNewLine _
            & "		     ELSE MR3.RPT_ID END                               AS RPT_ID                     " & vbNewLine _
            & "     	,CONCAT(BIL.INKA_NO_L,BIM.INKA_NO_M,BIS.INKA_NO_S) AS INKA_NO                    " & vbNewLine _
            & "     	,MGS.GOODS_CD_CUST                                 AS GOODS_CD_CUST              " & vbNewLine _
            & "     	,MGS.GOODS_NM_1                                    AS GOODS_NM                   " & vbNewLine _
            & "     	,BIS.LOT_NO                                        AS LOT_NO                     " & vbNewLine _
            & "     	,BIS.IRIME                                         AS IRIME                      " & vbNewLine _
            & "     	,MGS.STD_IRIME_UT                                  AS STD_IRIME_UT               " & vbNewLine _
            & "     	,BIS.KONSU                                         AS KONSU                      " & vbNewLine _
            & "     	,MGS.PKG_NB                                        AS PKG_NB                     " & vbNewLine _
            & "     	,(BIS.KONSU * MGS.PKG_NB) + BIS.HASU               AS KOSU                       " & vbNewLine _
            & "     	,MGS.NB_UT                                         AS NB_UT                      " & vbNewLine _
            & "     	,BIL.NRS_BR_CD                                     AS NRS_BR_CD                  " & vbNewLine _
            & "     	,BIL.INKA_DATE                                     AS INKA_DATE                  " & vbNewLine _
            & "     	,BIL.CUST_CD_L                                     AS CUST_CD_L                  " & vbNewLine _
            & "     	,BIL.CUST_CD_M                                     AS CUST_CD_M                  " & vbNewLine _
            & "     	,SUSE.USER_NM                                      AS USER_NM                    " & vbNewLine _
            & "     	,ISNULL(KBN_N001.KBN_NM1,'')                       AS NB_UT_NM                   " & vbNewLine _
            & "     FROM                                                                                 " & vbNewLine _
            & "     	$LM_TRN$..B_INKA_L BIL                                                           " & vbNewLine _
            & "     LEFT JOIN                                                                            " & vbNewLine _
            & "     	$LM_TRN$..B_INKA_M BIM                                                           " & vbNewLine _
            & "     	ON                                                                               " & vbNewLine _
            & "     		BIL.NRS_BR_CD   = BIM.NRS_BR_CD                                              " & vbNewLine _
            & "     	AND	BIL.INKA_NO_L   = BIM.INKA_NO_L                                              " & vbNewLine _
            & "     	AND	BIM.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
            & "     LEFT JOIN                                                                            " & vbNewLine _
            & "     	$LM_TRN$..B_INKA_S BIS                                                           " & vbNewLine _
            & "     	ON                                                                               " & vbNewLine _
            & "     		BIM.NRS_BR_CD   = BIS.NRS_BR_CD                                              " & vbNewLine _
            & "     	AND	BIM.INKA_NO_L   = BIS.INKA_NO_L                                              " & vbNewLine _
            & "     	AND	BIM.INKA_NO_M   = BIS.INKA_NO_M                                              " & vbNewLine _
            & "     	AND	BIS.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
            & "     LEFT JOIN                                                                            " & vbNewLine _
            & "     	$LM_MST$..M_GOODS MGS                                                            " & vbNewLine _
            & "     	ON                                                                               " & vbNewLine _
            & "     		MGS.NRS_BR_CD    = BIM.NRS_BR_CD                                             " & vbNewLine _
            & "     	AND MGS.GOODS_CD_NRS = BIM.GOODS_CD_NRS                                          " & vbNewLine _
            & "     LEFT JOIN                                                                            " & vbNewLine _
            & "     	$LM_MST$..Z_KBN KBN_N001                                                         " & vbNewLine _
            & "     	ON                                                                               " & vbNewLine _
            & "     		KBN_N001.KBN_GROUP_CD = 'N001'                                               " & vbNewLine _
            & "     	AND KBN_N001.KBN_CD = MGS.NB_UT                                                  " & vbNewLine _
            & "	  --入荷Lでの荷主帳票パターン取得                                                     	 " & vbNewLine _
            & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                 	 " & vbNewLine _
            & "	  ON  BIL.NRS_BR_CD = MCR1.NRS_BR_CD                                                	 " & vbNewLine _
            & "	  AND BIL.CUST_CD_L = MCR1.CUST_CD_L                                                	 " & vbNewLine _
            & "	  AND BIL.CUST_CD_M = MCR1.CUST_CD_M                                                	 " & vbNewLine _
            & "	  AND '00'          = MCR1.CUST_CD_S                                                	 " & vbNewLine _
            & "	  AND MCR1.PTN_ID   = '01'                                                          	 " & vbNewLine _
            & "	  --帳票パターン取得                                                                	 " & vbNewLine _
            & "	  LEFT JOIN $LM_MST$..M_RPT MR1                                                       	 " & vbNewLine _
            & "	  ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                                	 " & vbNewLine _
            & "	  AND MR1.PTN_ID    = MCR1.PTN_ID                                                   	 " & vbNewLine _
            & "	  AND MR1.PTN_CD    = MCR1.PTN_CD                                                   	 " & vbNewLine _
            & "   AND MR1.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
            & "	  --商品Mの荷主での荷主帳票パターン取得                                   	             " & vbNewLine _
            & "	  LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                 	 " & vbNewLine _
            & "	  ON  MGS.NRS_BR_CD = MCR2.NRS_BR_CD                                                 	 " & vbNewLine _
            & "	  AND MGS.CUST_CD_L = MCR2.CUST_CD_L                                                 	 " & vbNewLine _
            & "	  AND MGS.CUST_CD_M = MCR2.CUST_CD_M                                                 	 " & vbNewLine _
            & "	  AND MGS.CUST_CD_S = MCR2.CUST_CD_S                                                 	 " & vbNewLine _
            & "	  AND MCR2.PTN_ID  = '01'                                                           	" & vbNewLine _
            & "	  --帳票パターン取得                                                                	" & vbNewLine _
            & "	  LEFT JOIN $LM_MST$..M_RPT MR2                                                       	" & vbNewLine _
            & "	  ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                                	" & vbNewLine _
            & "	  AND MR2.PTN_ID    = MCR2.PTN_ID                                                   	" & vbNewLine _
            & "	  AND MR2.PTN_CD    = MCR2.PTN_CD                                                   	" & vbNewLine _
            & "   AND MR2.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
            & "	  --存在しない場合の帳票パターン取得                                                	" & vbNewLine _
            & "	  LEFT JOIN $LM_MST$..M_RPT MR3                                                       	" & vbNewLine _
            & "	  ON  MR3.NRS_BR_CD     = BIL.NRS_BR_CD                                             	" & vbNewLine _
            & "	  AND MR3.PTN_ID        = '01'                                                      	" & vbNewLine _
            & "	  AND MR3.STANDARD_FLAG = '01'                                                      	" & vbNewLine _
            & "   AND MR3.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
            & "	  --入荷受付表印刷者名取得                                                          	" & vbNewLine _
            & "	  LEFT JOIN $LM_MST$..S_USER SUSE                                                       	" & vbNewLine _
            & "	  ON  BIL.UKETSUKELIST_PRT_USER = SUSE.USER_CD                                           " & vbNewLine _
            & "	  AND SUSE.SYS_DEL_FLG = '0'                                                             " & vbNewLine _
            & "     WHERE                                                                                " & vbNewLine _
            & "     		BIL.INKA_NO_L   = @INKA_NO_L                                                 " & vbNewLine _
            & "     	AND BIL.NRS_BR_CD   = @NRS_BR_CD                                                 " & vbNewLine _
            & "     	AND BIL.SYS_DEL_FLG = '0'                                                        " & vbNewLine _
            & "     ORDER BY                                                                             " & vbNewLine _
            & "     		BIL.NRS_BR_CD                                                                " & vbNewLine _
            & "     	   ,BIL.INKA_NO_L                                                                " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMB501IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB501DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMB501DAC.SQL_FROM_Mprt)        'SQL構築(帳票種別用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB501DAC", "SelectMPrt", cmd)

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
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB501IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB501DAC.SQL_SELECT_PRINT_DATA)      'SQL構築(データ抽出用Select句)
        Call Me.SetConditionMasterSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB501DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("INKA_NO", "INKA_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("KOSU", "KOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("USER_NM", "USER_NM")
        map.Add("NB_UT_NM", "NB_UT_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB501OUT")

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

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            'Me._StrSql.Append(" AND INL.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("INKA_NO_L").ToString()
            'Me._StrSql.Append(" AND INL.INKA_NO_L = @INKA_NO_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))

            '印刷ユーザー
            whereStr = .Item("USER_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))

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
