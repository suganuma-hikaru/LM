' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD610    : 予定棚卸し一覧表印刷
'  作  成  者       :  [shinohara]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD610DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD610DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	ZAITRS.NRS_BR_CD                                        AS NRS_BR_CD  " & vbNewLine _
                                            & ",'AT'                                                    AS PTN_ID     " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                      " & vbNewLine _
                                            & "	 	 ELSE MR3.PTN_CD END                                AS PTN_CD     " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  	 WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      " & vbNewLine _
                                            & "		 ELSE MR3.RPT_ID END                                AS RPT_ID     " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                                             " & vbNewLine _
                                            & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                               " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                          " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID             " & vbNewLine _
                                            & " ,ZAITRS.NRS_BR_CD                                        AS NRS_BR_CD          " & vbNewLine _
                                            & " ,ZAITRS.WH_CD                                            AS WH_CD              " & vbNewLine _
                                            & " ,ZAITRS.CUST_CD_L                                        AS CUST_CD_L          " & vbNewLine _
                                            & " ,ZAITRS.CUST_CD_M                                        AS CUST_CD_M          " & vbNewLine _
                                            & " ,GOODS.CUST_CD_S                                         AS CUST_CD_S          " & vbNewLine _
                                            & " ,CUST.CUST_NM_L                                          AS CUST_NM_L          " & vbNewLine _
                                            & " ,ZAITRS.TOU_NO                                           AS TOU_NO             " & vbNewLine _
                                            & " ,ZAITRS.SITU_NO                                          AS SITU_NO            " & vbNewLine _
                                            & " ,RTRIM(ZAITRS.ZONE_CD)                                   AS ZONE_CD            " & vbNewLine _
                                            & " ,ZAITRS.LOCA                                             AS LOCA               " & vbNewLine _
                                            & " ,GOODS.GOODS_NM_1                                        AS GOODS_NM           " & vbNewLine _
                                            & " ,ZAITRS.LOT_NO                                           AS LOT_NO             " & vbNewLine _
                                            & " ,ZAITRS.REMARK                                           AS REMARK             " & vbNewLine _
                                            & " ,ZAITRS.REMARK_OUT                                       AS REMARK_OUT         " & vbNewLine _
                                            & " ,ZAITRS.INKO_DATE                                        AS INKO_DATE          " & vbNewLine _
                                            & " ,ZAITRS.OFB_KB                                           AS OFB_KB             " & vbNewLine _
                                            & " ,ZAITRS.PORA_ZAI_NB                                      AS PORA_ZAI_NB        " & vbNewLine _
                                            & " ,ZAITRS.PORA_ZAI_QT                                      AS PORA_ZAI_QT        " & vbNewLine _
                                            & " ,ZAITRS.ALCTD_NB                                         AS ALCTD_NB           " & vbNewLine _
                                            & " ,ZAITRS.SMPL_FLAG                                        AS SMPL_FLAG          " & vbNewLine _
                                            & " ,ZAITRS.IRIME                                            AS IRIME              " & vbNewLine _
                                            & " ,GOODS.PKG_NB                                            AS PKG_NB             " & vbNewLine _
                                            & " ,ZAITRS.PORA_ZAI_NB / GOODS.PKG_NB                       AS KONSU              " & vbNewLine _
                                            & " ,ZAITRS.ALCTD_NB / GOODS.PKG_NB                          AS ALCTD_KONSU        " & vbNewLine _
                                            & " ,ZAITRS.PORA_ZAI_NB % GOODS.PKG_NB                       AS HASU               " & vbNewLine _
                                            & " ,ZAITRS.ALCTD_NB % GOODS.PKG_NB                          AS ALCTD_HASU         " & vbNewLine _
                                            & " ,GOODS.ONDO_KB                                           AS ONDO_KB            " & vbNewLine _
                                            & " ,ISNULL(KBN2.KBN_NM1,'')                                 AS GOODS_COND_KB_1    " & vbNewLine _
                                            & " ,ISNULL(KBN3.KBN_NM1,'')                                 AS GOODS_COND_KB_2    " & vbNewLine _
                                            & " ,GOODS.SHOBO_CD                                          AS SHOBO_CD           " & vbNewLine _
                                            & " ,KBN5.KBN_NM1                                            AS PKG_UT             " & vbNewLine _
                                            & " ,KBN6.KBN_NM1                                            AS SHOBO_KB           " & vbNewLine _
                                            & " ,GOODS.GOODS_CD_CUST                                     AS GOODS_CD_CUST      " & vbNewLine _
                                            & " ,SHOBO.HINMEI                                            AS HINMEI             " & vbNewLine _
                                            & " ,GOODS.STD_IRIME_NB                                      AS STD_IRIME_NB       " & vbNewLine _
                                            & " ,ZAITRS.ALLOC_CAN_NB                                     AS ALLOC_CAN_NB       " & vbNewLine _
                                            & " ,ZAITRS.ALLOC_CAN_NB / GOODS.PKG_NB                      AS ALLOC_CAN_KONSU    " & vbNewLine _
                                            & " ,ZAITRS.ALLOC_CAN_NB % GOODS.PKG_NB                      AS ALLOC_CAN_HASU     " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                         " & vbNewLine _
                                      & "$LM_TRN$..D_ZAI_TRS ZAITRS                                  " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "$LM_MST$..M_GOODS GOODS ON                                  " & vbNewLine _
                                      & "GOODS.NRS_BR_CD = ZAITRS.NRS_BR_CD                          " & vbNewLine _
                                      & "AND GOODS.GOODS_CD_NRS = ZAITRS.GOODS_CD_NRS                " & vbNewLine _
                                      & "AND GOODS.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "$LM_MST$..M_CUST CUST ON                                    " & vbNewLine _
                                      & "CUST.NRS_BR_CD = ZAITRS.NRS_BR_CD                           " & vbNewLine _
                                      & "AND CUST.CUST_CD_L = GOODS.CUST_CD_L                        " & vbNewLine _
                                      & "AND CUST.CUST_CD_M = GOODS.CUST_CD_M                        " & vbNewLine _
                                      & "AND CUST.CUST_CD_S = GOODS.CUST_CD_S                        " & vbNewLine _
                                      & "AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                      " & vbNewLine _
                                      & "AND CUST.SYS_DEL_FLG = '0'                                  " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "$LM_MST$..M_SHOBO SHOBO ON                                  " & vbNewLine _
                                      & "SHOBO.SHOBO_CD = GOODS.SHOBO_CD                             " & vbNewLine _
                                      & "AND SHOBO.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                      & "--LEFT OUTER JOIN $LM_MST$..Z_KBN KBN2 ON                   " & vbNewLine _
                                      & "--ZAITRS.GOODS_COND_KB_1 = KBN2.KBN_CD                      " & vbNewLine _
                                      & "--AND KBN2.KBN_GROUP_CD='S005'                              " & vbNewLine _
                                      & "--AND KBN2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                      & "--LEFT OUTER JOIN $LM_MST$..Z_KBN KBN3 ON                   " & vbNewLine _
                                      & "--ZAITRS.GOODS_COND_KB_2 = KBN3.KBN_CD                      " & vbNewLine _
                                      & "--AND KBN3.KBN_GROUP_CD='S006'                              " & vbNewLine _
                                      & "--AND KBN3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                      & "--LEFT OUTER JOIN $LM_MST$..Z_KBN KBN5 ON                   " & vbNewLine _
                                      & "--GOODS.NB_UT = KBN5.KBN_CD                                 " & vbNewLine _
                                      & "--AND KBN5.KBN_GROUP_CD='K002'                              " & vbNewLine _
                                      & "--AND KBN5.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                      & "--LEFT OUTER JOIN $LM_MST$..Z_KBN KBN6 ON                   " & vbNewLine _
                                      & "--SHOBO.RUI = KBN6.KBN_CD                                   " & vbNewLine _
                                      & "--AND KBN6.KBN_GROUP_CD='S004'                              " & vbNewLine _
                                      & "--AND KBN6.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "   (SELECT                                                  " & vbNewLine _
                                      & "      " & " #KBN# " & "     AS KBN_NM1                      " & vbNewLine _
                                      & "      ,KBN1.KBN_CD                                          " & vbNewLine _
                                      & "   FROM $LM_MST$..Z_KBN AS KBN1                             " & vbNewLine _
                                      & "   WHERE KBN1.KBN_GROUP_CD = 'S005'                         " & vbNewLine _
                                      & "     AND KBN1.SYS_DEL_FLG = '0') KBN2                       " & vbNewLine _
                                      & "  ON ZAITRS.GOODS_COND_KB_1 = KBN2.KBN_CD                   " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "   (SELECT                                                  " & vbNewLine _
                                      & "      " & " #KBN# " & "     AS KBN_NM1                      " & vbNewLine _
                                      & "      ,KBN1.KBN_CD                                          " & vbNewLine _
                                      & "   FROM $LM_MST$..Z_KBN AS KBN1                             " & vbNewLine _
                                      & "   WHERE KBN1.KBN_GROUP_CD = 'S006'                         " & vbNewLine _
                                      & "     AND KBN1.SYS_DEL_FLG = '0') KBN3                       " & vbNewLine _
                                      & "  ON ZAITRS.GOODS_COND_KB_2 = KBN3.KBN_CD                   " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "   (SELECT                                                  " & vbNewLine _
                                      & "      " & " #KBN# " & "     AS KBN_NM1                      " & vbNewLine _
                                      & "      ,KBN1.KBN_CD                                          " & vbNewLine _
                                      & "   FROM $LM_MST$..Z_KBN AS KBN1                             " & vbNewLine _
                                      & "   WHERE KBN1.KBN_GROUP_CD = 'K002'                         " & vbNewLine _
                                      & "     AND KBN1.SYS_DEL_FLG = '0') KBN5                       " & vbNewLine _
                                      & "  ON GOODS.NB_UT = KBN5.KBN_CD                              " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "   (SELECT                                                  " & vbNewLine _
                                      & "      " & " #KBN# " & "     AS KBN_NM1                      " & vbNewLine _
                                      & "      ,KBN1.KBN_CD                                          " & vbNewLine _
                                      & "   FROM $LM_MST$..Z_KBN AS KBN1                             " & vbNewLine _
                                      & "   WHERE KBN1.KBN_GROUP_CD = 'S004'                         " & vbNewLine _
                                      & "     AND KBN1.SYS_DEL_FLG = '0') KBN6                       " & vbNewLine _
                                      & "  ON SHOBO.RUI = KBN6.KBN_CD                                " & vbNewLine _
                                      & "LEFT OUTER JOIN                                             " & vbNewLine _
                                      & "     (SELECT                                                " & vbNewLine _
                                      & "         INKAL.NRS_BR_CD,                                   " & vbNewLine _
                                      & "         INKAL.INKA_NO_L,                                   " & vbNewLine _
                                      & "         INKAL.INKA_STATE_KB,                               " & vbNewLine _
                                      & "         INKAL.INKA_DATE                                    " & vbNewLine _
                                      & "     FROM                                                   " & vbNewLine _
                                      & "         $LM_TRN$..B_INKA_L  INKAL    -- 013118 LM_TRN_10固定修正" & vbNewLine _
                                      & "     WHERE INKAL.SYS_DEL_FLG = '0') INKAL                   " & vbNewLine _
                                      & "     ON  ZAITRS.NRS_BR_CD = INKAL.NRS_BR_CD                 " & vbNewLine _
                                      & "     AND ZAITRS.INKA_NO_L = INKAL.INKA_NO_L                 " & vbNewLine _
                                      & "--在庫の荷主での荷主帳票パターン取得                        " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                         " & vbNewLine _
                                      & "ON  ZAITRS.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                      & "AND ZAITRS.CUST_CD_L = MCR1.CUST_CD_L                       " & vbNewLine _
                                      & "AND ZAITRS.CUST_CD_M = MCR1.CUST_CD_M                       " & vbNewLine _
                                      & "AND MCR1.PTN_ID = 'AT'                                      " & vbNewLine _
                                      & "AND '00' = MCR1.CUST_CD_S                                   " & vbNewLine _
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
                                      & "AND MCR2.PTN_ID = 'AT'                                      " & vbNewLine _
                                      & "--帳票パターン取得                                          " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR2                               " & vbNewLine _
                                      & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                          " & vbNewLine _
                                      & "AND MR2.PTN_ID = MCR2.PTN_ID                                " & vbNewLine _
                                      & "AND MR2.PTN_CD = MCR2.PTN_CD                                " & vbNewLine _
                                      & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "--存在しない場合の帳票パターン取得                          " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR3                               " & vbNewLine _
                                      & "ON  MR3.NRS_BR_CD = ZAITRS.NRS_BR_CD                        " & vbNewLine _
                                      & "AND MR3.PTN_ID = 'AT'                                       " & vbNewLine _
                                      & "AND MR3.STANDARD_FLAG = '01'                                " & vbNewLine _
                                      & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine

    ''' <summary>
    ''' ORDER BY Nomal
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                         " & vbNewLine _
                                         & "     ZAITRS.CUST_CD_L                            " & vbNewLine _
                                         & "    ,ZAITRS.WH_CD                                " & vbNewLine _
                                         & "    ,ZAITRS.TOU_NO                               " & vbNewLine _
                                         & "    ,ZAITRS.SITU_NO                              " & vbNewLine _
                                         & "    ,ZAITRS.ZONE_CD                              " & vbNewLine _
                                         & "    ,ZAITRS.LOCA                                 " & vbNewLine _
                                         & "    ,GOODS.GOODS_NM_1                            " & vbNewLine _
                                         & "    ,ZAITRS.LOT_NO                               " & vbNewLine

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _rptRow As Data.DataRow

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
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


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

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD610IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD610DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMD610DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        '2016.02.05 修正START
        'スキーマ名設定
        Dim sql As String = Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm)
        '2016.02.05 修正END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD610DAC", "SelectMPrt", cmd)

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
    ''' 在庫テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        '2016.02.05 追加START
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '2016.02.05 追加END

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD610IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'M_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'RPTTableの条件rowの格納
        Me._rptRow = rptTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD610DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMD610DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMD610DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        '2016.02.05 修正START
        'スキーマ名設定
        Dim sql As String = Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm)
        '2016.02.05 修正END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD610DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("SMPL_FLAG", "SMPL_FLAG")
        map.Add("IRIME", "IRIME")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("KONSU", "KONSU")
        map.Add("ALCTD_KONSU", "ALCTD_KONSU")
        map.Add("HASU", "HASU")
        map.Add("ALCTD_HASU", "ALCTD_HASU")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("GOODS_COND_KB_1", "GOODS_COND_KB_1")
        map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("SHOBO_KB", "SHOBO_KB")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("HINMEI", "HINMEI")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("ALLOC_CAN_KONSU", "ALLOC_CAN_KONSU")
        map.Add("ALLOC_CAN_HASU", "ALLOC_CAN_HASU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD610OUT")

        Return ds

    End Function

    '2016.02.05 追加START
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD610IN")

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

        MyBase.Logger.WriteSQLLog("LMD515DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function
    '2016.02.05 追加END

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        'INKA_STATE_KB判定用の文字
        Dim INKA_STATE_KBN_YO As String = "01"
        Dim INKA_STATE_KBN_JITSU As String = "02"
        Dim INKA_STATE_KBN_ZEN As String = "03"

        With Me._Row
            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.NRS_BR_CD = @NRS_BR_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.WH_CD = @WH_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード（小）
            whereStr = .Item("CUST_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_CD_S LIKE @CUST_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード（極小）
            whereStr = .Item("CUST_CD_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_CD_SS LIKE @CUST_CD_U")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_U", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '入荷日From
            whereStr = .Item("INKO_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" INKAL.INKA_DATE >= @INKO_PLAN_DATE_FROM ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日To
            whereStr = .Item("INKO_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" INKAL.INKA_DATE <= @INKO_PLAN_DATE_TO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'ゼロフラグ
            whereStr = .Item("ZERO_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'ゼロフラグ=1の場合
                If whereStr.Equals(ZERO_FLG) Then
                    If andstr.Length <> 0 Then
                        andstr.Append("AND")
                    End If
                    andstr.Append(" ZAITRS.PORA_ZAI_NB <> 0 ")
                    andstr.Append(vbNewLine)
                End If
            End If

            'データ種別()
            whereStr = .Item("INKA_STATE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals(INKA_STATE_KBN_ZEN) = False Then
                    If whereStr.Equals(INKA_STATE_KBN_ZEN) = False Then
                        If andstr.Length <> 0 Then
                            andstr.Append("AND")
                        End If
                        If whereStr.Equals(INKA_STATE_KBN_YO) Then
                            '"予"が選択されている時
                            andstr.Append(" INKAL.INKA_STATE_KB < '50' ")
                            andstr.Append(vbNewLine)
                        ElseIf whereStr.Equals(INKA_STATE_KBN_JITSU) Then
                            '"実"が選択されている時
                            andstr.Append(" INKAL.INKA_STATE_KB = '50' ")
                            andstr.Append(vbNewLine)
                        End If
                    End If
                End If
            End If
            '荷主商品コード
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_NM_1 LIKE @GOODS_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '荷主カテゴリ1
            whereStr = .Item("SEARCH_KEY_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SEARCH_KEY_1 LIKE @SEARCH_KEY_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_1", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '毒劇区分
            whereStr = .Item("DOKU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.DOKU_KB = @DOKU_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKU_KB", whereStr, DBDataType.CHAR))
            End If

            '荷主勘定科目コード1
            whereStr = .Item("CUST_COST_CD1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_COST_CD1 LIKE @CUST_COST_CD1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD1", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主勘定科目コード2
            whereStr = .Item("CUST_COST_CD2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.CUST_COST_CD2 LIKE @CUST_COST_CD2")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_COST_CD2", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '消防コード
            whereStr = .Item("SHOBO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SHOBO_CD LIKE @SHOBO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '消防情報
            whereStr = .Item("SHOBO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (KBN6.KBN_NM1 + ' ' + ISNULL(SHOBO.HINMEI,'')) LIKE @SHOBO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '置場
            whereStr = .Item("OKIBA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                '条件に"-"有り無し関係なく抽出させる --- START ---
                andstr.Append(" ZAITRS.TOU_NO + ZAITRS.SITU_NO + ZAITRS.ZONE_CD + ZAITRS.LOCA LIKE  @OKIBA")
                ' 条件に"-"有り無し関係なく抽出させる ---  END  ---
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '★2013.02.25 / Notes1890対応開始
            '印刷時参照置場(棟・室・ZONE・LOCATION別個指定)
            '棟
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.TOU_NO = @TOU_NO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", whereStr, DBDataType.CHAR))
            End If

            '室
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SITU_NO = @SITU_NO ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", whereStr, DBDataType.CHAR))
            End If

            'ZONE
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZONE_CD = @ZONE_CD ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", whereStr, DBDataType.CHAR))
            End If

            'ロケーション
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.LOCA LIKE @LOCA")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '★2013.02.25 / Notes1890対応終了

            'ロット
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.LOT_NO LIKE @LOT_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考小（社内）
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.REMARK LIKE @REMARK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'シリアル№
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SERIAL_NO LIKE @SERIAL_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '状態 中身
            whereStr = .Item("GOODS_COND_KB_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_COND_KB_1 = @GOODS_COND_KB_1 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", whereStr, DBDataType.CHAR))
            End If

            '状態 外装
            whereStr = .Item("GOODS_COND_KB_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_COND_KB_2 = @GOODS_COND_KB_2 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", whereStr, DBDataType.CHAR))
            End If

            '状態 荷主
            whereStr = .Item("GOODS_COND_KB_3").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_COND_KB_3 = @GOODS_COND_KB_3 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", whereStr, DBDataType.CHAR))
            End If

            '簿外品
            whereStr = .Item("OFB_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.OFB_KB = @OFB_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", whereStr, DBDataType.CHAR))
            End If

            '保留品
            whereStr = .Item("SPD_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.SPD_KB = @SPD_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", whereStr, DBDataType.CHAR))
            End If

            '荷主カテゴリ２
            whereStr = .Item("SEARCH_KEY_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.SEARCH_KEY_2 LIKE @SEARCH_KEY_2 ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '入荷管理番号
            whereStr = .Item("INKA_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                '条件に"-"有り無し関係なく抽出させる --- START ---
                andstr.Append(" ZAITRS.INKA_NO_L + ZAITRS.INKA_NO_M + ZAITRS.INKA_NO_S LIKE @INKA_NO")
                '条件に"-"有り無し関係なく抽出させる ---  END  ---
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '商品KEY
            whereStr = .Item("GOODS_CD_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.GOODS_CD_NRS LIKE @GOODS_CD_NRS")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '在庫レコード番号
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZAI_REC_NO LIKE @ZAI_REC_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '割当優先
            whereStr = .Item("ALLOC_PRIORITY").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ALLOC_PRIORITY = @ALLOC_PRIORITY ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", whereStr, DBDataType.CHAR))
            End If

            '予約届先コード
            whereStr = .Item("DEST_CD_P").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.DEST_CD_P LIKE @DEST_CD_P")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L + '-' + CUST.CUST_NM_M LIKE @CUST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.TAX_KB = @TAX_KB ")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", whereStr, DBDataType.CHAR))
            End If

            '温度
            whereStr = .Item("ONDO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.ONDO_KB = @ONDO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_NM", whereStr, DBDataType.CHAR))
            End If

            '入目単位
            whereStr = .Item("IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.STD_IRIME_UT = @IRIME_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            '残数単位
            whereStr = .Item("NB_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.NB_UT = @NB_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", whereStr, DBDataType.CHAR))
            End If

            '実数量単位
            whereStr = .Item("STD_IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.STD_IRIME_UT = @STD_IRIME_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            '入数単位
            whereStr = .Item("PKG_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.PKG_UT = @PKG_UT")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If
            andstr.Append(" ZAITRS.SYS_DEL_FLG = '0' ")
            andstr.Append(vbNewLine)
            andstr.Append("AND")
            andstr.Append(vbNewLine)
            andstr.Append(" ZAITRS.ALLOC_CAN_NB > 0 ")
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

    '2016.02.05 追加START
    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '2016.02.05 追加END

#End Region

#End Region


#End Region

End Class
