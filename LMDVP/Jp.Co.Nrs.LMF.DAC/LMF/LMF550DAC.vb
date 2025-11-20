' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF550    : 物品引取書
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF550DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF550DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       F_UNSO_L.NRS_BR_CD                               AS NRS_BR_CD " & vbNewLine _
                                            & "      , '99'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                  " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID END      AS RPT_ID               " & vbNewLine _
                                       & "      , '01'                     AS PRT_KBN              " & vbNewLine _
                                       & "      , F_UNSO_L.NRS_BR_CD       AS NRS_BR_CD            " & vbNewLine _
                                       & "      , F_UNSO_L.UNSO_NO_L       AS UNSO_NO_L            " & vbNewLine _
                                       & "      , F_UNSO_M.UNSO_NO_M       AS UNSO_NO_M            " & vbNewLine _
                                       & "      , F_UNSO_L.UNSO_CD         AS UNSO_CD              " & vbNewLine _
                                       & "      , F_UNSO_L.UNSO_BR_CD      AS UNSO_BR_CD           " & vbNewLine _
                                       & "      , F_UNSO_L.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE      " & vbNewLine _
                                       & "      , F_UNSO_L.OUTKA_PLAN_TIME AS OUTKA_PLAN_TIME	   " & vbNewLine _
                                       & "      , F_UNSO_L.ARR_PLAN_DATE   AS ARR_PLAN_DATE        " & vbNewLine _
                                       & "      , F_UNSO_L.ARR_PLAN_TIME   AS ARR_PLAN_TIME        " & vbNewLine _
                                       & "      , F_UNSO_L.REMARK          AS REMARK_L             " & vbNewLine _
                                       & "      , F_UNSO_L.CUST_CD_L       AS CUST_CD_L            " & vbNewLine _
                                       & "      , F_UNSO_L.CUST_CD_M       AS CUST_CD_M            " & vbNewLine _
                                       & "      , F_UNSO_L.UNSO_PKG_NB     AS UNSO_PKG_NB          " & vbNewLine _
                                       & "      , F_UNSO_M.GOODS_CD_NRS    AS GOODS_CD_NRS         " & vbNewLine _
                                       & "      , F_UNSO_M.GOODS_NM        AS GOODS_NM             " & vbNewLine _
                                       & "      , F_UNSO_M.UNSO_TTL_NB     AS UNSO_TTL_NB          " & vbNewLine _
                                       & "      , F_UNSO_M.NB_UT           AS NB_UT                " & vbNewLine _
                                       & "      , Z_KBN.KBN_NM1            AS NB_UT_NM             " & vbNewLine _
                                       & "      , F_UNSO_M.UNSO_TTL_QT     AS UNSO_TTL_QT          " & vbNewLine _
                                       & "      , F_UNSO_M.QT_UT           AS QT_UT                " & vbNewLine _
                                       & "      , F_UNSO_M.HASU            AS HASU                 " & vbNewLine _
                                       & "      , F_UNSO_M.ZAI_REC_NO      AS ZAI_REC_NO           " & vbNewLine _
                                       & "      , F_UNSO_M.IRIME           AS IRIME                " & vbNewLine _
                                       & "      , F_UNSO_M.IRIME_UT        AS IRIME_UT             " & vbNewLine _
                                       & "      , F_UNSO_M.LOT_NO          AS LOT_NO               " & vbNewLine _
                                       & "      , F_UNSO_M.BETU_WT         AS BETU_WT              " & vbNewLine _
                                       & "      , F_UNSO_M.PKG_NB          AS PKG_NB               " & vbNewLine _
                                       & "      , F_UNSO_M.REMARK          AS REMARK_M             " & vbNewLine _
                                       & "      , M_DEST.DEST_NM           AS DEST_NM              " & vbNewLine _
                                       & "      , M_DEST.AD_1              AS DEST_AD_1            " & vbNewLine _
                                       & "      , M_DEST.AD_2              AS DEST_AD_2            " & vbNewLine _
                                       & "      , M_DEST.TEL               AS DEST_TEL             " & vbNewLine _
                                       & "      , M_NRS_BR.NRS_BR_NM       AS NRS_BR_NM            " & vbNewLine _
                                       & "      , M_NRS_BR.AD_1            AS NRS_BR_AD_1          " & vbNewLine _
                                       & "      , M_NRS_BR.AD_2            AS NRS_BR_AD_2          " & vbNewLine _
                                       & "      , M_NRS_BR.TEL             AS NRS_BR_TEL           " & vbNewLine 
 
    ''' <summary>
    ''' 帳票種別取得用 FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = " FROM                                                                        " & vbNewLine _
                                     & "      --運送Ｌ                                                               " & vbNewLine _
                                     & "      $LM_TRN$..F_UNSO_L F_UNSO_L                                            " & vbNewLine _
                                     & "              --運送Ｍ                                                       " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_TRN$..F_UNSO_M F_UNSO_M                    " & vbNewLine _
                                     & "                           ON F_UNSO_L.NRS_BR_CD = F_UNSO_M.NRS_BR_CD        " & vbNewLine _
                                     & "                          AND F_UNSO_L.UNSO_NO_L = F_UNSO_M.UNSO_NO_L        " & vbNewLine _
                                     & "              --営業所マスタ                                                 " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_NRS_BR M_NRS_BR                    " & vbNewLine _
                                     & "                     ON F_UNSO_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD              " & vbNewLine _
                                     & "              --届先マスタ(発地情報)                                         " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_DEST M_DEST                        " & vbNewLine _
                                     & "                           ON F_UNSO_L.NRS_BR_CD = M_DEST.NRS_BR_CD          " & vbNewLine _
                                     & "                          AND F_UNSO_L.CUST_CD_L = M_DEST.CUST_CD_L          " & vbNewLine _
                                     & "                          AND F_UNSO_L.ORIG_CD   = M_DEST.DEST_CD            " & vbNewLine _
                                     & "              --区分マスタ                                                   " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..Z_KBN Z_KBN                          " & vbNewLine _
                                     & "                           ON Z_KBN.KBN_GROUP_CD = 'N001'                    " & vbNewLine _
                                     & "                          AND Z_KBN.KBN_CD       = F_UNSO_M.NB_UT            " & vbNewLine _
                                     & "                          AND Z_KBN.SYS_DEL_FLG  = '0'                       " & vbNewLine _
                                     & "              -- 商品マスタ                                                  " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                     & "                           ON M_GOODS.NRS_BR_CD      = F_UNSO_M.NRS_BR_CD    " & vbNewLine _
                                     & "                          AND M_GOODS.GOODS_CD_NRS   = F_UNSO_M.GOODS_CD_NRS " & vbNewLine _
                                     & "              -- 帳票パターンマスタ①(F_UNSO_Lの荷主より取得)                " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                " & vbNewLine _
                                     & "                           ON M_CUSTRPT1.NRS_BR_CD   = F_UNSO_L.NRS_BR_CD    " & vbNewLine _
                                     & "                          AND M_CUSTRPT1.CUST_CD_L   = F_UNSO_L.CUST_CD_L    " & vbNewLine _
                                     & "                          AND M_CUSTRPT1.CUST_CD_M   = F_UNSO_L.CUST_CD_M    " & vbNewLine _
                                     & "                          AND M_CUSTRPT1.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                          AND M_CUSTRPT1.PTN_ID      = '99'                  " & vbNewLine _
                                     & "                          AND M_CUSTRPT1.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                           " & vbNewLine _
                                     & "                           ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD  " & vbNewLine _
                                     & "                          AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID     " & vbNewLine _
                                     & "                          AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD     " & vbNewLine _
                                     & "                          AND MR1.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "              -- 帳票パターンマスタ②(商品マスタより)                        " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                " & vbNewLine _
                                     & "                           ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD     " & vbNewLine _
                                     & "                          AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L     " & vbNewLine _
                                     & "                          AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M     " & vbNewLine _
                                     & "                          AND M_CUSTRPT2.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                          AND M_CUSTRPT2.PTN_ID      = '99'                  " & vbNewLine _
                                     & "                          AND M_CUSTRPT2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                           " & vbNewLine _
                                     & "                           ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD  " & vbNewLine _
                                     & "                          AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID     " & vbNewLine _
                                     & "                          AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD     " & vbNewLine _
                                     & "                          AND MR2.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "              -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >    " & vbNewLine _
                                     & "              LEFT OUTER JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                     & "                           ON MR3.NRS_BR_CD          = F_UNSO_L.NRS_BR_CD    " & vbNewLine _
                                     & "                          AND MR3.PTN_ID             = '99'                  " & vbNewLine _
                                     & "                          AND MR3.STANDARD_FLAG      = '01'                  " & vbNewLine 


    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                     " & vbNewLine _
                                         & "       F_UNSO_M.GOODS_NM  ASC " & vbNewLine _
                                         & "     , F_UNSO_M.UNSO_NO_M ASC " & vbNewLine _

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

    ''' <summary>
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF550DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMF550DAC.SQL_FROM)           'SQL構築(帳票種別用FROM句)
        Call Me.SetConditionMasterSQL()                 'SQL構築(印刷データ抽出用条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF550DAC", "SelectMPrt", cmd)

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
    ''' 物品引取書対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>物品引取書対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF550DAC.SQL_SELECT)      'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMF550DAC.SQL_FROM)        'SQL構築(印刷データ抽出用 FROM句)
        Call Me.SetConditionMasterSQL()
        'SQL構築(印刷データ抽出用条件設定)
        Me._StrSql.Append(LMF550DAC.SQL_ORDER_BY)    'SQL構築(印刷データ抽出用 ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF550DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("PRT_KBN", "PRT_KBN")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKA_PLAN_TIME", "OUTKA_PLAN_TIME")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("UNSO_TTL_QT", "UNSO_TTL_QT")
        map.Add("QT_UT", "QT_UT")
        map.Add("HASU", "HASU")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("NRS_BR_AD_1", "NRS_BR_AD_1")
        map.Add("NRS_BR_AD_2", "NRS_BR_AD_2")
        map.Add("NRS_BR_TEL", "NRS_BR_TEL")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF550OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
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

            '(2012.06.21) 要望番号1191 --- START ---
            '運送事由区分(02：引取)
            'Me._StrSql.Append(" F_UNSO_L.JIYU_KB = '02' ")

            '削除フラグ
            Me._StrSql.Append(" F_UNSO_L.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(" AND F_UNSO_M.SYS_DEL_FLG = '0' ")

            '(2012.06.21) 要望番号1191 ---  END  ---

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F_UNSO_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '運送管理番号(大)
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F_UNSO_L.UNSO_NO_L = @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", whereStr, DBDataType.CHAR))
            End If

            '(2012.06.21) 要望番号1191 --- START ---
            ''削除フラグ
            'Me._StrSql.Append(" AND F_UNSO_L.SYS_DEL_FLG = '0' ")
            'Me._StrSql.Append(" AND F_UNSO_M.SYS_DEL_FLG = '0' ")
            '(2012.06.21) 要望番号1191 ---  END  ---

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
