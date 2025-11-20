' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME520DAC : 作業指示書
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME520DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME520DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                      " & vbNewLine _
                                            & "SIJI.NRS_BR_CD                                      AS NRS_BR_CD     " & vbNewLine _
                                            & "	,'40'                                                AS PTN_ID      " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                   " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                   " & vbNewLine _
                                            & "	      ELSE MR3.PTN_CD END                            AS PTN_CD      " & vbNewLine _
                                            & "	,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                   " & vbNewLine _
                                            & "	      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                   " & vbNewLine _
                                            & "       ELSE MR3.RPT_ID END                            AS RPT_ID      " & vbNewLine


#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用(仮)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                              " & vbNewLine _
                                            & "	CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                    " & vbNewLine _
                                            & "	     WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "      ELSE MR3.RPT_ID END  AS RPT_ID                                 " & vbNewLine _
                                            & ",@SAIHAKKO_FLG             AS SAIHAKKO_FLG	                        " & vbNewLine _
                                            & ",SIJI.NRS_BR_CD            AS NRS_BR_CD                              " & vbNewLine _
                                            & ",SIJI.SAGYO_SIJI_NO		  AS SAGYO_SIJI_NO                          " & vbNewLine _
                                            & ",M_CUST_L.CUST_NM_L		  AS CUST_NM_L                              " & vbNewLine _
                                            & ",M_CUST_L.CUST_NM_M		  AS CUST_NM_M                              " & vbNewLine _
                                            & ",USR.USER_NM		          AS USER_NM                                " & vbNewLine _
                                            & ",SIJI.REMARK_1		      AS REMARK_1                               " & vbNewLine _
                                            & ",SIJI.REMARK_2		      AS REMARK_2                               " & vbNewLine _
                                            & ",SIJI.REMARK_3		      AS REMARK_3                               " & vbNewLine _
                                            & ",SAGYO.GOODS_CD_NRS		  AS GOODS_CD_NRS                           " & vbNewLine _
                                            & ",SAGYO.GOODS_NM_NRS		  AS GOODS_NM_NRS                           " & vbNewLine _
                                            & ",SAGYO.SAGYO_COMP_DATE	  AS SAGYO_COMP_DATE                        " & vbNewLine _
                                            & ",SAGYO.LOT_NO		      AS LOT_NO                                 " & vbNewLine _
                                            & ",TRS.IRIME		          AS IRIME                                  " & vbNewLine _
                                            & ",KBN01.KBN_NM1		      AS IRIME_UT                               " & vbNewLine _
                                            & ",SAGYO.SAGYO_NB		      AS SAGYO_NB                               " & vbNewLine _
                                            & ",SAGYO.PORA_ZAI_NB		  AS PORA_ZAI_NB                            " & vbNewLine _
                                            & ",TRS.TOU_NO		          AS TOU_NO                                 " & vbNewLine _
                                            & ",TRS.SITU_NO	       	      AS SITU_NO                                " & vbNewLine _
                                            & ",TRS.ZONE_CD			      AS ZONE_CD                                " & vbNewLine _
                                            & ",TRS.LOCA				  AS LOCA                                   " & vbNewLine _
                                            & ",MS.SAGYO_RYAK			  AS SAGYO_RYAK                             " & vbNewLine _
                                            & ",SAGYO.SAGYO_NM			  AS SAGYO_NM                               " & vbNewLine _
                                            & ",MS.SAGYO_REMARK	          AS SAGYO_REMARK                           " & vbNewLine _
                                            & ",GOODS.GOODS_CD_CUST       AS GOODS_CD_CUST                          " & vbNewLine _
                                            & ",GOODS.PKG_NB              AS PKG_NB                                 " & vbNewLine _
                                            & ",ROUND(SAGYO.SAGYO_NB / GOODS.PKG_NB,0) AS KONSU                     " & vbNewLine _
                                            & ",SAGYO.SAGYO_NB % GOODS.PKG_NB          AS HASU                      " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = "FROM                                                     " & vbNewLine _
                                     & "	  $LM_TRN$..E_SAGYO_SIJI   SIJI                      " & vbNewLine _
                                     & "--  作業                                                 " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "	  $LM_TRN$..E_SAGYO        SAGYO                     " & vbNewLine _
                                     & "   ON SAGYO.NRS_BR_CD     = SIJI.NRS_BR_CD               " & vbNewLine _
                                     & "  AND SAGYO.SAGYO_SIJI_NO = SIJI.SAGYO_SIJI_NO           " & vbNewLine _
                                     & "  AND SAGYO.SYS_DEL_FLG   = '0'                          " & vbNewLine _
                                     & "  --商品M                                                " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..M_GOODS GOODS                              " & vbNewLine _
                                     & " ON GOODS.NRS_BR_CD       = SAGYO.NRS_BR_CD              " & vbNewLine _
                                     & "AND	GOODS.CUST_CD_L       = SAGYO.CUST_CD_L              " & vbNewLine _
                                     & "AND	GOODS.CUST_CD_M       = SAGYO.CUST_CD_M              " & vbNewLine _
                                     & "AND GOODS.GOODS_CD_NRS    = SAGYO.GOODS_CD_NRS           " & vbNewLine _
                                     & "--在庫データ                                             " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "     $LM_TRN$..D_ZAI_TRS TRS                             " & vbNewLine _
                                     & "  ON TRS.NRS_BR_CD        = SAGYO.NRS_BR_CD              " & vbNewLine _
                                     & " AND TRS.ZAI_REC_NO       = SAGYO.ZAI_REC_NO             " & vbNewLine _
                                     & " AND TRS.SYS_DEL_FLG      = '0'	                         " & vbNewLine _
                                     & "  --荷主M(Lレベル)                                       " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "     $LM_MST$..M_CUST M_CUST_L                           " & vbNewLine _
                                     & "  ON M_CUST_L.NRS_BR_CD   = SAGYO.NRS_BR_CD              " & vbNewLine _
                                     & " AND M_CUST_L.CUST_CD_L   = SAGYO.CUST_CD_L              " & vbNewLine _
                                     & " AND M_CUST_L.CUST_CD_M   = SAGYO.CUST_CD_M              " & vbNewLine _
                                     & " AND M_CUST_L.CUST_CD_S   = '00'                         " & vbNewLine _
                                     & " AND M_CUST_L.CUST_CD_SS  = '00'                         " & vbNewLine _
                                     & " AND M_CUST_L.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                     & "--作業マスタ                                             " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "     $LM_MST$..M_SAGYO MS                                " & vbNewLine _
                                     & "  ON MS.NRS_BR_CD         = SAGYO.NRS_BR_CD              " & vbNewLine _
                                     & " AND MS.SAGYO_CD          = SAGYO.SAGYO_CD               " & vbNewLine _
                                     & " AND MS.SYS_DEL_FLG       = '0'                          " & vbNewLine _
                                     & "--ユーザマスタ                                           " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "     $LM_MST$..S_USER USR                                " & vbNewLine _
                                     & "  ON USR.USER_CD          = SIJI.SYS_ENT_USER            " & vbNewLine _
                                     & " AND USR.SYS_DEL_FLG      = '0'                          " & vbNewLine _
                                     & " --区分マスタ                                            " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..Z_KBN   KBN01                              " & vbNewLine _
                                     & " ON KBN01.KBN_GROUP_CD      = 'I001'                     " & vbNewLine _
                                     & "AND KBN01.KBN_CD            = GOODS.STD_IRIME_UT         " & vbNewLine _
                                     & "  --作業マスタでの荷主帳票パターン取得                   " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..M_CUST_RPT MCR1                            " & vbNewLine _
                                     & " ON MCR1.NRS_BR_CD = SAGYO.NRS_BR_CD                     " & vbNewLine _
                                     & "AND MCR1.CUST_CD_L = SAGYO.CUST_CD_L                     " & vbNewLine _
                                     & "AND MCR1.CUST_CD_M = SAGYO.CUST_CD_M                     " & vbNewLine _
                                     & "AND MCR1.CUST_CD_S = '00'                                " & vbNewLine _
                                     & "AND MCR1.PTN_ID    = '40'                                " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                     & "  --帳票パターン取得                                     " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..M_RPT MR1                                  " & vbNewLine _
                                     & " ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                     & "AND MR1.PTN_ID    = MCR1.PTN_ID                          " & vbNewLine _
                                     & "AND MR1.PTN_CD    = MCR1.PTN_CD                          " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "  --商品コードの荷主帳票パターン取得                     " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..M_CUST_RPT MCR2                            " & vbNewLine _
                                     & " ON MCR2.NRS_BR_CD = GOODS.NRS_BR_CD                     " & vbNewLine _
                                     & "AND MCR2.CUST_CD_L = GOODS.CUST_CD_L                     " & vbNewLine _
                                     & "AND MCR2.CUST_CD_M = GOODS.CUST_CD_M                     " & vbNewLine _
                                     & "AND MCR2.CUST_CD_S = '00'                                " & vbNewLine _
                                     & "AND MCR2.PTN_ID    = '40'                                " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                     & "  --帳票パターン取得                                     " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..M_RPT MR2                                  " & vbNewLine _
                                     & " ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                     & "AND MR2.PTN_ID    = MCR2.PTN_ID                          " & vbNewLine _
                                     & "AND MR2.PTN_CD    = MCR2.PTN_CD                          " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "  --存在しない場合の帳票パターン取得                     " & vbNewLine _
                                     & "  LEFT JOIN                                              " & vbNewLine _
                                     & "    $LM_MST$..M_RPT MR3                                  " & vbNewLine _
                                     & " ON MR3.NRS_BR_CD     = SIJI.NRS_BR_CD                   " & vbNewLine _
                                     & "AND MR3.PTN_ID        = '40'                             " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG   = '0'                              " & vbNewLine _
                                     & "WHERE                                                    " & vbNewLine _
                                     & "    SIJI.NRS_BR_CD     = @NRS_BR_CD                      " & vbNewLine _
                                     & "AND SIJI.SAGYO_SIJI_NO = @SAGYO_SIJI_NO                  " & vbNewLine
#End Region


#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                " & vbNewLine _
                                & "  SIJI.SAGYO_SIJI_NO ASC                         " & vbNewLine _
                                & "	,SAGYO.GOODS_CD_NRS ASC                         " & vbNewLine _
                                & "	,SAGYO.ZAI_REC_NO ASC                           " & vbNewLine


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
    '''出力対象帳票パターン取得処理(仮)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LME520DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LME520DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME520DAC", "SelectMPrt", cmd)

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
    ''' 運賃テーブル対象データ(仮)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME520IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'RPT_IDのチェック用
        Dim rptId As String = rptTbl.Rows(0).Item("RPT_ID").ToString()

        'SQL作成
        Me._StrSql.Append(LME520DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LME520DAC.SQL_FROM)      'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LME520DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用OrderBy句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME520DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("SAIHAKKO_FLG", "SAIHAKKO_FLG")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("USER_NM", "USER_NM")
        map.Add("REMARK_1", "REMARK_1")
        map.Add("REMARK_2", "REMARK_2")
        map.Add("REMARK_3", "REMARK_3")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("SAGYO_RYAK", "SAGYO_RYAK")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("SAGYO_REMARK", "SAGYO_REMARK")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LME520OUT")

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
            '再発行フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", Me._Row.Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))
            '作業指示書番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me._Row.Item("SAGYO_SIJI_NO").ToString(), DBDataType.CHAR))

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
