' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF580DAC : 配車表
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF580DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF580DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "        UNSOL.NRS_BR_CD                                  AS NRS_BR_CD " & vbNewLine _
                                            & "	     , '49'                                             AS PTN_ID    " & vbNewLine _
                                            & "	     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "	 	       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
                                            & "	       ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "	     , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "	            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

#End Region

#Region "SELECT句"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                   " & vbNewLine _
                                            & "	       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                  " & vbNewLine _
                                            & "	            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                  " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                     AS RPT_ID                 " & vbNewLine _
                                            & "      , UNSOL.NRS_BR_CD                         AS NRS_BR_CD              " & vbNewLine _
                                            & "      , UNSOL.UNSO_NO_L                         AS UNSO_NO_L              " & vbNewLine _
                                            & "--(2012.07.02)運送Ｌ単位で出力。ＭはGROUP BY -- START --                  " & vbNewLine _
                                            & "--      , MIN(UNSOM.UNSO_NO_M)                    AS UNSO_NO_M              " & vbNewLine _
                                            & "--(2012.07.02)運送Ｌ単位で出力。ＭはGROUP BY --  END  --                  " & vbNewLine _
                                            & "--(2013.08.12)運送Ｍ単位で出力。-- START --                               " & vbNewLine _
                                            & "      , UNSOM.UNSO_NO_M                         AS UNSO_NO_M              " & vbNewLine _
                                            & "--(2013.08.12)運送Ｍ単位で出力。--  END  --                               " & vbNewLine _
                                            & "      , M_UNSOCO.UNSOCO_NM                      AS UNSOCO_NM              " & vbNewLine _
                                            & "      , M_UNSOCO.UNSOCO_BR_NM                   AS UNSOCO_BR_NM           " & vbNewLine _
                                            & "      , UNSOL.ARR_PLAN_DATE                     AS ARR_PLAN_DATE          " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_L                        AS CUST_NM_L              " & vbNewLine _
                                            & "      , M_CUST.CUST_NM_M                        AS CUST_NM_M              " & vbNewLine _
                                            & "      , UNSOL.BUY_CHU_NO                        AS BUY_CHU_NO             " & vbNewLine _
                                            & "      , M_DEST.DEST_NM                          AS DEST_NM                " & vbNewLine _
                                            & "      , M_DEST.AD_1                             AS AD_1                   " & vbNewLine _
                                            & "      , M_DEST.TEL                              AS TEL                    " & vbNewLine _
                                            & "--(2013.08.12)運送Ｍ単位で出力。-- START --                               " & vbNewLine _
                                            & "--      , MIN(UNSOM.GOODS_NM)                     AS GOODS_NM --2012/07/02  " & vbNewLine _
                                            & "--      , SUM(UNSOM.UNSO_TTL_NB)                  AS UNSO_TTL_NB            " & vbNewLine _
                                            & "--要望番号:1228 yamanaka 2012.7.3 Start                                   " & vbNewLine _
                                            & "      , UNSOM.BETU_WT * UNSOM.UNSO_TTL_NB       AS WT                     " & vbNewLine _
                                            & "--    , SUM(UNSOM.BETU_WT)                      AS BETU_WT                " & vbNewLine _
                                            & "--要望番号:1228 yamanaka 2012.7.3 End                                     " & vbNewLine _
                                            & "      , UNSOM.GOODS_NM                          AS GOODS_NM               " & vbNewLine _
                                            & "--    , UNSOM_GN.GOODS_NM                       AS GOODS_NM --2012/06/28  " & vbNewLine _
                                            & "      , UNSOM.UNSO_TTL_NB                       AS UNSO_TTL_NB            " & vbNewLine _
                                            & "--    , UNSOM.BETU_WT                           AS BETU_WT                " & vbNewLine _
                                            & "--(2012.07.02)運送Ｌ単位で出力。ＭはGROUP BY --  END  --                  " & vbNewLine _
                                            & "      , UNSOL.REMARK                            AS REMARK                 " & vbNewLine _
                                            & "      , M_DEST.AD_2                             AS AD_2                   " & vbNewLine _
                                            & "      , M_DEST.AD_3                             AS AD_3                   " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM As String = " FROM $LM_TRN$..F_UNSO_L UNSOL                            " & vbNewLine _
                                     & "      --運送Ｍ                                            " & vbNewLine _
                                     & "      LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM                  " & vbNewLine _
                                     & "             ON UNSOM.NRS_BR_CD    = UNSOL.NRS_BR_CD      " & vbNewLine _
                                     & "            AND UNSOM.UNSO_NO_L    = UNSOL.UNSO_NO_L      " & vbNewLine _
                                     & "      --運送会社マスタ                                    " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_UNSOCO M_UNSOCO               " & vbNewLine _
                                     & "             ON M_UNSOCO.NRS_BR_CD = UNSOL.NRS_BR_CD      " & vbNewLine _
                                     & "            AND M_UNSOCO.UNSOCO_CD = UNSOL.UNSO_CD        " & vbNewLine _
                                     & "--(2012.07.02) JOIN条件が足りなかったので追加 -- START -- " & vbNewLine _
                                     & "            AND M_UNSOCO.UNSOCO_BR_CD = UNSOL.UNSO_BR_CD  " & vbNewLine _
                                     & "--(2012.07.02) JOIN条件が足りなかったので追加 --  END  -- " & vbNewLine _
                                     & "      --荷主マスタ                                        " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_CUST   M_CUST                 " & vbNewLine _
                                     & "             ON M_CUST.NRS_BR_CD   = UNSOL.NRS_BR_CD      " & vbNewLine _
                                     & "            AND M_CUST.CUST_CD_L   = UNSOL.CUST_CD_L      " & vbNewLine _
                                     & "            AND M_CUST.CUST_CD_M   = UNSOL.CUST_CD_M      " & vbNewLine _
                                     & "      --届先マスタ                                        " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_DEST   M_DEST                 " & vbNewLine _
                                     & "             ON M_DEST.NRS_BR_CD = UNSOL.NRS_BR_CD        " & vbNewLine _
                                     & "            AND M_DEST.CUST_CD_L = UNSOL.CUST_CD_L        " & vbNewLine _
                                     & "            AND M_DEST.DEST_CD   = UNSOL.DEST_CD          " & vbNewLine _
                                     & "      --商品マスタ                                        " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_GOODS M_GOODS                 " & vbNewLine _
                                     & "             ON M_GOODS.NRS_BR_CD    = UNSOM.NRS_BR_CD    " & vbNewLine _
                                     & "            AND M_GOODS.GOODS_CD_NRS = UNSOM.GOODS_CD_NRS " & vbNewLine _
                                     & "--(2012.07.02) 運送Ｍは既にJOIN済。よって不要 -- START -- " & vbNewLine _
                                     & "--    --運送Ｍ(商品名取得用)2012/06/28                    " & vbNewLine _
                                     & "--    LEFT JOIN $LM_TRN$..F_UNSO_M UNSOM_GN               " & vbNewLine _
                                     & "--           ON UNSOM_GN.NRS_BR_CD = UNSOL.NRS_BR_CD      " & vbNewLine _
                                     & "--          AND UNSOM_GN.UNSO_NO_L = UNSOL.UNSO_NO_L      " & vbNewLine _
                                     & "--          AND UNSOM_GN.UNSO_NO_M = '001'                " & vbNewLine _
                                     & "--(2012.07.02) 運送Ｍは既にJOIN済。よって不要 --  END  -- " & vbNewLine _
                                     & "      --運送Ｍでの荷主帳票パターン取得                    " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                 " & vbNewLine _
                                     & "             ON MCR1.NRS_BR_CD   = UNSOL.NRS_BR_CD        " & vbNewLine _
                                     & "            AND MCR1.CUST_CD_L   = UNSOL.CUST_CD_L        " & vbNewLine _
                                     & "            AND MCR1.CUST_CD_M   = UNSOL.CUST_CD_M        " & vbNewLine _
                                     & "            AND MCR1.CUST_CD_S   = '00'                   " & vbNewLine _
                                     & "            AND MCR1.PTN_ID      = '49'                   " & vbNewLine _
                                     & "            AND MCR1.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                     & "      --帳票パターン取得                                  " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_RPT MR1                       " & vbNewLine _
                                     & "             ON MR1.NRS_BR_CD = MCR1.NRS_BR_CD            " & vbNewLine _
                                     & "            AND MR1.PTN_ID    = MCR1.PTN_ID               " & vbNewLine _
                                     & "            AND MR1.PTN_CD    = MCR1.PTN_CD               " & vbNewLine _
                                     & "            AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "      --商品コードの荷主帳票パターン取得                  " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                 " & vbNewLine _
                                     & "             ON MCR2.NRS_BR_CD = M_GOODS.NRS_BR_CD        " & vbNewLine _
                                     & "            AND MCR2.CUST_CD_L = M_GOODS.CUST_CD_L        " & vbNewLine _
                                     & "            AND MCR2.CUST_CD_M = M_GOODS.CUST_CD_M        " & vbNewLine _
                                     & "            AND MCR2.CUST_CD_S = '00'                     " & vbNewLine _
                                     & "            AND MCR2.PTN_ID    = '49'                     " & vbNewLine _
                                     & "            AND MCR2.SYS_DEL_FLG = '0'                    " & vbNewLine _
                                     & "      --帳票パターン取得                                  " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_RPT MR2                       " & vbNewLine _
                                     & "             ON MR2.NRS_BR_CD = MCR2.NRS_BR_CD            " & vbNewLine _
                                     & "            AND MR2.PTN_ID    = MCR2.PTN_ID               " & vbNewLine _
                                     & "            AND MR2.PTN_CD    = MCR2.PTN_CD               " & vbNewLine _
                                     & "            AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                     & "      --存在しない場合の帳票パターン取得                  " & vbNewLine _
                                     & "      LEFT JOIN $LM_MST$..M_RPT MR3                       " & vbNewLine _
                                     & "             ON MR3.NRS_BR_CD     = UNSOL.NRS_BR_CD       " & vbNewLine _
                                     & "            AND MR3.PTN_ID        = '49'                  " & vbNewLine _
                                     & "            AND MR3.STANDARD_FLAG = '01'                  " & vbNewLine _
                                     & "            AND MR3.SYS_DEL_FLG   = '0'                   " & vbNewLine

#End Region

#Region "GROUP BY"
    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                    " & vbNewLine _
                                         & "       MR1.PTN_CD            " & vbNewLine _
                                         & "     , MR2.PTN_CD            " & vbNewLine _
                                         & "     , MR1.RPT_ID            " & vbNewLine _
                                         & "     , MR2.RPT_ID            " & vbNewLine _
                                         & "     , MR3.RPT_ID            " & vbNewLine _
                                         & "     , UNSOL.NRS_BR_CD       " & vbNewLine _
                                         & "     , UNSOL.UNSO_NO_L       " & vbNewLine _
                                         & "--(2013.08.12)運送Ｍ単位で出力。-- START -- " & vbNewLine _
                                         & "     , UNSOM.UNSO_NO_M       " & vbNewLine _
                                         & "--(2013.08.12)運送Ｍ単位で出力。-- END -- " & vbNewLine _
                                         & "     , M_UNSOCO.UNSOCO_NM    " & vbNewLine _
                                         & "     , M_UNSOCO.UNSOCO_BR_NM " & vbNewLine _
                                         & "     , UNSOL.ARR_PLAN_DATE   " & vbNewLine _
                                         & "     , M_CUST.CUST_NM_L      " & vbNewLine _
                                         & "     , M_CUST.CUST_NM_M      " & vbNewLine _
                                         & "     , UNSOL.BUY_CHU_NO      " & vbNewLine _
                                         & "     , M_DEST.DEST_NM        " & vbNewLine _
                                         & "     , M_DEST.AD_1           " & vbNewLine _
                                         & "     , M_DEST.TEL            " & vbNewLine _
                                         & "--(2013.08.12)運送Ｍ単位で出力。-- START -- " & vbNewLine _
                                         & "     , UNSOM.GOODS_NM        " & vbNewLine _
                                         & "     , UNSOM.UNSO_TTL_NB     " & vbNewLine _
                                         & "     , UNSOM.BETU_WT * UNSOM.UNSO_TTL_NB  " & vbNewLine _
                                         & "--(2013.08.12)運送Ｍ単位で出力。-- END -- " & vbNewLine _
                                         & "     , UNSOL.REMARK          " & vbNewLine _
                                         & "     , M_DEST.AD_2           " & vbNewLine _
                                         & "     , M_DEST.AD_3           " & vbNewLine

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                   " & vbNewLine _
                                         & "      UNSOL.UNSO_NO_L  ASC " & vbNewLine _
                                         & "	, UNSOM.UNSO_NO_M  ASC " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMF580IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF580DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF580DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF580DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMF580IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF580DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用SELECT句)
        Me._StrSql.Append(LMF580DAC.SQL_FROM)             'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMF580DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP_BY句)
        Me._StrSql.Append(LMF580DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER_BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF580DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("BUY_CHU_NO", "BUY_CHU_NO")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("AD_1", "AD_1")
        map.Add("TEL", "TEL")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        '要望番号:1228 yamanaka 2012.7.3 Start
        map.Add("WT", "WT")
        '要望番号:1228 yamanaka 2012.7.3 End
        map.Add("REMARK", "REMARK")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF580OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" UNSOL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UNSOL.UNSO_NO_L = @UNSO_NO_L ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
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
