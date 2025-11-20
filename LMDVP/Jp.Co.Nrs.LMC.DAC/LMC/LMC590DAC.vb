' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC590DAC : 出荷実績チェックリスト
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC590DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC590DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	OUTKAL.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'14'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                             " & vbNewLine _
                                         & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                         & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                  " & vbNewLine _
                                         & "		  ELSE MR3.RPT_ID END                                AS RPT_ID " & vbNewLine _
                                         & " ,OUTKAL.NRS_BR_CD  AS NRS_BR_CD                                       " & vbNewLine _
                                         & " ,OUTKAL.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                           " & vbNewLine _
                                         & " ,CUST.CUST_NM_L         AS CUST_NM_L                                  " & vbNewLine _
                                         & " ,CUST.CUST_CD_L         AS CUST_CD_L                                  " & vbNewLine _
                                         & " ,CUST.CUST_CD_M         AS CUST_CD_M                                  " & vbNewLine _
                                         & " ,OUTKAL.OUTKA_NO_L      AS OUTKA_NO_L                                 " & vbNewLine _
                                         & " ,OUTKAM.OUTKA_NO_M      AS OUTKA_NO_M                                 " & vbNewLine _
                                         & " ,OUTKAL.DEST_CD         AS DEST_CD                                    " & vbNewLine _
                                         & " ,GOODS.GOODS_CD_CUST    AS GOODS_CD_CUST                              " & vbNewLine _
                                         & " ,OUTKAM.CUST_ORD_NO_DTL AS CUST_ORD_NO_DTL                            " & vbNewLine _
                                         & " ,GOODS.GOODS_NM_1       AS GOODS_NM_1                                 " & vbNewLine _
                                         & " ,OUTKAM.ALCTD_NB        AS ALCTD_NB                                   " & vbNewLine _
                                         & " ,GOODS.PKG_NB           AS PKG_NB                                     " & vbNewLine _
                                         & " ,GOODS.PKG_UT           AS PKG_UT                                     " & vbNewLine _
                                         & " ,GOODS.NB_UT            AS NB_UT                                      " & vbNewLine _
                                         & " ,OUTKAM.ALCTD_QT        AS ALCTD_QT                                   " & vbNewLine _
                                         & " ,OUTKAM.IRIME_UT        AS IRIME_UT                                   " & vbNewLine _
                                         & " ,OUTKAL.WH_CD           AS WH_CD                                      " & vbNewLine _
                                         & " ,OUTKAL.ARR_PLAN_DATE   AS ARR_PLAN_DATE                              " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                    " & vbNewLine _
                                         & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIOUT.DEST_NM                    " & vbNewLine _
                                         & "       ELSE DEST.DEST_NM                                               " & vbNewLine _
                                         & "  END                    AS DEST_NM                                    " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_1                  " & vbNewLine _
                                         & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIOUT.DEST_AD_1                  " & vbNewLine _
                                         & "       ELSE DEST.AD_1                                                  " & vbNewLine _
                                         & "  END                    AS DEST_AD_1                                  " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_2                  " & vbNewLine _
                                         & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIOUT.DEST_AD_2                  " & vbNewLine _
                                         & "       ELSE DEST.AD_2                                                  " & vbNewLine _
                                         & "  END                    AS DEST_AD_2                                  " & vbNewLine _
                                         & " ,OUTKAS.BETU_WT                          AS BETU_WT                   " & vbNewLine _
                                         & " ,OUTKAM.LOT_NO                           AS LOT_NO                    " & vbNewLine _
                                         & " ,OUTKAL.DENP_NO                          AS DENP_NO                   " & vbNewLine _
                                         & " ,OUTKAL.SYUBETU_KB                       AS SYUBETU_KB                " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "FROM                                                             " & vbNewLine _
                                          & "	 $LM_TRN$..C_OUTKA_L AS OUTKAL				             " & vbNewLine _
                                          & "	--出荷データM				                             " & vbNewLine _
                                          & "	LEFT JOIN $LM_TRN$..C_OUTKA_M AS OUTKAM				     " & vbNewLine _
                                          & "	ON OUTKAL.NRS_BR_CD=OUTKAM.NRS_BR_CD				     " & vbNewLine _
                                          & "	AND OUTKAL.OUTKA_NO_L=OUTKAM.OUTKA_NO_L				     " & vbNewLine _
                                          & "	AND OUTKAM.SYS_DEL_FLG='0'				                 " & vbNewLine _
                                          & "	--出荷データS				                             " & vbNewLine _
                                          & "	LEFT JOIN $LM_TRN$..C_OUTKA_S AS OUTKAS				     " & vbNewLine _
                                          & "	ON OUTKAL.NRS_BR_CD=OUTKAS.NRS_BR_CD				     " & vbNewLine _
                                          & "	AND OUTKAL.OUTKA_NO_L=OUTKAS.OUTKA_NO_L				     " & vbNewLine _
                                          & "	AND OUTKAM.OUTKA_NO_M=OUTKAS.OUTKA_NO_M				     " & vbNewLine _
                                          & "	AND OUTKAS.SYS_DEL_FLG='0'				                 " & vbNewLine _
                                          & "	--商品マスタ				                             " & vbNewLine _
                                          & "	LEFT JOIN $LM_MST$..M_GOODS AS GOODS				     " & vbNewLine _
                                          & "	ON OUTKAL.NRS_BR_CD=GOODS.NRS_BR_CD				         " & vbNewLine _
                                          & "	AND OUTKAM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS				 " & vbNewLine _
                                          & "	--荷主マスタ				                             " & vbNewLine _
                                          & "	INNER JOIN $LM_MST$..M_CUST AS CUST				         " & vbNewLine _
                                          & "	ON GOODS.NRS_BR_CD=CUST.NRS_BR_CD				         " & vbNewLine _
                                          & "	AND GOODS.CUST_CD_L=CUST.CUST_CD_L				         " & vbNewLine _
                                          & "	AND  GOODS.CUST_CD_M=CUST.CUST_CD_M				         " & vbNewLine _
                                          & "	AND GOODS.CUST_CD_S=CUST.CUST_CD_S				         " & vbNewLine _
                                          & "	AND GOODS.CUST_CD_SS=CUST.CUST_CD_SS				     " & vbNewLine _
                                          & "	--届先マスタ				                             " & vbNewLine _
                                          & "	LEFT JOIN $LM_MST$..M_DEST AS DEST				         " & vbNewLine _
                                          & "	ON DEST.NRS_BR_CD=OUTKAL. NRS_BR_CD 				     " & vbNewLine _
                                          & "	AND DEST.CUST_CD_L=OUTKAL.CUST_CD_L 				     " & vbNewLine _
                                          & "	AND DEST.DEST_CD=OUTKAL.DEST_CD				             " & vbNewLine _
                                          & "	--EDI出荷データL        				                 " & vbNewLine _
                                          & "	LEFT JOIN 				                                 " & vbNewLine _
                                          & "	(SELECT NRS_BR_CD 				                         " & vbNewLine _
                                          & "	,OUTKA_CTL_NO 				                             " & vbNewLine _
                                          & "	,MIN(DEST_NM)     AS DEST_NM 				                                 " & vbNewLine _
                                          & "	,MIN(DEST_AD_1)   AS DEST_AD_1				                                 " & vbNewLine _
                                          & "	,MIN(DEST_AD_2)   AS DEST_AD_2      				                         " & vbNewLine _
                                          & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                          & "	FROM $LM_TRN$..H_OUTKAEDI_L 				             " & vbNewLine _
                                          & "	--AS EDI				                                 " & vbNewLine _
                                          & "	GROUP BY  				                                 " & vbNewLine _
                                          & "	 OUTKA_CTL_NO				                             " & vbNewLine _
                                          & "	,SYS_DEL_FLG				                             " & vbNewLine _
                                          & "	,NRS_BR_CD ) EDIOUT     				                 " & vbNewLine _
                                          & "	ON  EDIOUT.NRS_BR_CD = OUTKAL.NRS_BR_CD				     " & vbNewLine _
                                          & "	AND EDIOUT.OUTKA_CTL_NO= OUTKAL.OUTKA_NO_L				 " & vbNewLine _
                                          & "	AND EDIOUT.SYS_DEL_FLG='0'				                 " & vbNewLine _
                                          & "--出荷Lでの荷主帳票パターン取得                             " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                         " & vbNewLine _
                                          & "ON  OUTKAL.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & "AND OUTKAL.CUST_CD_L = MCR1.CUST_CD_L                       " & vbNewLine _
                                          & "AND OUTKAL.CUST_CD_M = MCR1.CUST_CD_M                       " & vbNewLine _
                                          & "AND '00' = MCR1.CUST_CD_S                                   " & vbNewLine _
                                          & "AND MCR1.PTN_ID = '14'                                      " & vbNewLine _
                                          & "--帳票パターン取得                                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR1                               " & vbNewLine _
                                          & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                          " & vbNewLine _
                                          & "AND MR1.PTN_ID = MCR1.PTN_ID                                " & vbNewLine _
                                          & "AND MR1.PTN_CD = MCR1.PTN_CD                                " & vbNewLine _
                                          & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                          & "--商品Mの荷主での荷主帳票パターン取得                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                         " & vbNewLine _
                                          & "ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                        " & vbNewLine _
                                          & "AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                        " & vbNewLine _
                                          & "AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                        " & vbNewLine _
                                          & "AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                        " & vbNewLine _
                                          & "AND MCR2.PTN_ID = '14'                                      " & vbNewLine _
                                          & "--帳票パターン取得                                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR2                               " & vbNewLine _
                                          & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                          " & vbNewLine _
                                          & "AND MR2.PTN_ID = MCR2.PTN_ID                                " & vbNewLine _
                                          & "AND MR2.PTN_CD = MCR2.PTN_CD                                " & vbNewLine _
                                          & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                          & "--存在しない場合の帳票パターン取得                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR3                               " & vbNewLine _
                                          & "ON  MR3.NRS_BR_CD = OUTKAL.NRS_BR_CD                        " & vbNewLine _
                                          & "AND MR3.PTN_ID = '14'                                       " & vbNewLine _
                                          & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                          & "AND MR3.STANDARD_FLAG = '01'                                " & vbNewLine


#End Region


#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = "GROUP BY                                                                                                             " & vbNewLine _
                                          & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                                                    " & vbNewLine _
                                          & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                                                                    " & vbNewLine _
                                          & "    ELSE MR3.RPT_ID END                                                                                              " & vbNewLine _
                                          & " ,OUTKAL.NRS_BR_CD                                                                                                   " & vbNewLine _
                                          & " ,OUTKAL.OUTKA_PLAN_DATE                                                                                             " & vbNewLine _
                                          & " ,CUST.CUST_NM_L                                                                                                     " & vbNewLine _
                                          & " ,CUST.CUST_CD_L                                                                                                     " & vbNewLine _
                                          & " ,CUST.CUST_CD_M                                                                                                     " & vbNewLine _
                                          & " ,OUTKAL.OUTKA_NO_L                                                                                                  " & vbNewLine _
                                          & " ,OUTKAM.OUTKA_NO_M                                                                                                  " & vbNewLine _
                                          & " ,OUTKAL.DEST_CD                                                                                                     " & vbNewLine _
                                          & " ,GOODS.GOODS_CD_CUST                                                                                                " & vbNewLine _
                                          & " ,OUTKAM.CUST_ORD_NO_DTL                                                                                             " & vbNewLine _
                                          & " ,GOODS.GOODS_NM_1                                                                                                   " & vbNewLine _
                                          & " ,OUTKAM.ALCTD_NB                                                                                                    " & vbNewLine _
                                          & " ,GOODS.PKG_NB                                                                                                       " & vbNewLine _
                                          & " ,GOODS.PKG_UT                                                                                                       " & vbNewLine _
                                          & " ,GOODS.NB_UT                                                                                                        " & vbNewLine _
                                          & " ,OUTKAM.ALCTD_QT                                                                                                    " & vbNewLine _
                                          & " ,OUTKAM.IRIME_UT                                                                                                    " & vbNewLine _
                                          & " ,OUTKAL.WH_CD                                                                                                       " & vbNewLine _
                                          & " ,OUTKAL.ARR_PLAN_DATE                                                                                               " & vbNewLine _
                                          & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                    " & vbNewLine _
                                          & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIOUT.DEST_NM                    " & vbNewLine _
                                          & "       ELSE DEST.DEST_NM                                                 " & vbNewLine _
                                          & "  END                                                                    " & vbNewLine _
                                          & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_1                  " & vbNewLine _
                                          & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIOUT.DEST_AD_1                  " & vbNewLine _
                                          & "       ELSE DEST.AD_1                                                    " & vbNewLine _
                                          & "  END                                                                    " & vbNewLine _
                                          & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_AD_2                  " & vbNewLine _
                                          & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIOUT.DEST_AD_2                  " & vbNewLine _
                                          & "       ELSE DEST.AD_2                                                    " & vbNewLine _
                                          & "  END                                                                    " & vbNewLine _
                                          & " ,OUTKAS.BETU_WT                                                                                                     " & vbNewLine _
                                          & " ,OUTKAM.LOT_NO                                                                                                      " & vbNewLine _
                                          & " ,OUTKAL.DENP_NO                                                                                                     " & vbNewLine _
                                          & " ,OUTKAL.SYUBETU_KB                                                                                                  " & vbNewLine


#End Region


#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                       " & vbNewLine _
                                         & "    OUTKAL.OUTKA_PLAN_DATE                     " & vbNewLine _
                                         & "    ,OUTKAL.NRS_BR_CD                          " & vbNewLine _
                                         & "    ,CUST.CUST_CD_L                            " & vbNewLine _
                                         & "    ,OUTKAL.OUTKA_NO_L                         " & vbNewLine _
                                         & "    ,OUTKAM.OUTKA_NO_M                         " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMC590IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC590DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC590DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionWhereSQL()                    '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC590DAC", "SelectMPrt", cmd)

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
    ''' 出荷テーブル対象データ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC590IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC590DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC590DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionWhereSQL()                    'SQL構築(条件設定)
        Me._StrSql.Append(LMC590DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMC590DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC590DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("WH_CD", "WH_CD")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("SYUBETU_KB", "SYUBETU_KB")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC590OUT")

        Return ds

    End Function
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionWhereSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" OUTKAL.NRS_BR_CD = @NRS_BR_CD  ")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))



            whereStr = .Item("CUST_CD_L").ToString()
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" OUTKAL.CUST_CD_L= @CUST_CD_L ")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            whereStr = .Item("CUST_CD_M").ToString()
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" OUTKAL.CUST_CD_M = @CUST_CD_M")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" OUTKAL.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE ")
            andstr.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", whereStr, DBDataType.CHAR))

            whereStr = .Item("SYUBETU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = True Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OUTKAL.SYUBETU_KB <> '50' ")
                andstr.Append(vbNewLine)
            End If

            whereStr = .Item("SYS_ENT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" OUTKAL.SYS_ENT_DATE = @SYS_ENT_DATE ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" OUTKAL.SYS_DEL_FLG = '0' ")
            andstr.Append(vbNewLine)

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


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
