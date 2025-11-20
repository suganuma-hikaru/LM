' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM290DAC : 振替対象商品マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM290DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM290DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(FURI.CD_NRS)      AS SELECT_CNT   " & vbNewLine



    ''' <summary>
    ''' 振替対象商品マスタデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                          " & vbNewLine _
                              & "             FURI.NRS_BR_CD                                                        AS NRS_BR_CD                " & vbNewLine _
                              & "            ,NRSBR.NRS_BR_NM                                                       AS NRS_BR_NM                " & vbNewLine _
                              & "            ,CUST.CUST_CD_L                                                        AS CUST_CD_L                " & vbNewLine _
                              & "            ,CUST.CUST_NM_L                                                        AS CUST_NM_L                " & vbNewLine _
                              & "            ,CUST.CUST_CD_M                                                        AS CUST_CD_M                " & vbNewLine _
                              & "            ,CUST.CUST_NM_M                                                        AS CUST_NM_M                " & vbNewLine _
                              & "            ,CUST.CUST_CD_S                                                        AS CUST_CD_S                " & vbNewLine _
                              & "            ,CUST.CUST_NM_S                                                        AS CUST_NM_S                " & vbNewLine _
                              & "            ,CUST.CUST_CD_SS                                                       AS CUST_CD_SS               " & vbNewLine _
                              & "            ,CUST.CUST_NM_SS                                                       AS CUST_NM_SS               " & vbNewLine _
                              & "            ,GOODS.GOODS_CD_CUST                                                   AS GOODS_CD_CUST            " & vbNewLine _
                              & "            ,FURI.CD_NRS                                                           AS CD_NRS                   " & vbNewLine _
                              & "            ,GOODS.GOODS_NM_1                                                      AS GOODS_NM_1               " & vbNewLine _
                              & "            ,GOODS.PKG_UT                                                          AS PKG_UT                   " & vbNewLine _
                              & "            ,KBN1.KBN_NM1                                                          AS PKG_UT_NM                " & vbNewLine _
                              & "            ,GOODS.STD_IRIME_NB                                                    AS STD_IRIME_NB             " & vbNewLine _
                              & "            ,GOODS.STD_IRIME_UT                                                    AS STD_IRIME_UT             " & vbNewLine _
                              & "            ,KBN2.KBN_NM1                                                          AS STD_IRIME_UT_NM          " & vbNewLine _
                              & "            ,CONVERT(VARCHAR,GOODS.STD_IRIME_NB) + ' ' + KBN2.KBN_NM1              AS STD_IRIME                " & vbNewLine _
                              & "            ,C2.CUST_CD_L                                                          AS CUST_CD_L_TO             " & vbNewLine _
                              & "            ,C2.CUST_NM_L                                                          AS CUST_NM_L_TO             " & vbNewLine _
                              & "            ,C2.CUST_CD_M                                                          AS CUST_CD_M_TO             " & vbNewLine _
                              & "            ,C2.CUST_NM_M                                                          AS CUST_NM_M_TO             " & vbNewLine _
                              & "            ,C2.CUST_CD_S                                                          AS CUST_CD_S_TO             " & vbNewLine _
                              & "            ,C2.CUST_NM_S                                                          AS CUST_NM_S_TO             " & vbNewLine _
                              & "            ,C2.CUST_CD_SS                                                         AS CUST_CD_SS_TO            " & vbNewLine _
                              & "            ,C2.CUST_NM_SS                                                         AS CUST_NM_SS_TO            " & vbNewLine _
                              & "            ,G2.GOODS_CD_CUST                                                      AS GOODS_CD_CUST_TO         " & vbNewLine _
                              & "            ,FURI.CD_NRS_TO                                                        AS CD_NRS_TO                " & vbNewLine _
                              & "            ,G2.GOODS_NM_1                                                         AS GOODS_NM_1_TO            " & vbNewLine _
                              & "            ,G2.PKG_UT                                                             AS PKG_UT_TO                " & vbNewLine _
                              & "            ,KBN3.KBN_NM1                                                          AS PKG_UT_NM_TO             " & vbNewLine _
                              & "            ,G2.STD_IRIME_NB                                                       AS STD_IRIME_NB_TO          " & vbNewLine _
                              & "            ,G2.STD_IRIME_UT                                                       AS STD_IRIME_UT_TO          " & vbNewLine _
                              & "            ,KBN4.KBN_NM1                                                          AS STD_IRIME_UT_NM_TO       " & vbNewLine _
                              & "            ,CONVERT(VARCHAR,G2.STD_IRIME_NB) + ' ' + KBN4.KBN_NM1                 AS STD_IRIME_TO             " & vbNewLine _
                              & "            ,FURI.SYS_ENT_DATE                                                     AS SYS_ENT_DATE             " & vbNewLine _
                              & "            ,USER1.USER_NM                                                         AS SYS_ENT_USER_NM          " & vbNewLine _
                              & "            ,FURI.SYS_UPD_DATE                                                     AS SYS_UPD_DATE             " & vbNewLine _
                              & "            ,FURI.SYS_UPD_TIME                                                     AS SYS_UPD_TIME             " & vbNewLine _
                              & "            ,USER2.USER_NM                                                         AS SYS_UPD_USER_NM          " & vbNewLine _
                              & "            ,FURI.SYS_DEL_FLG                                                      AS SYS_DEL_FLG              " & vbNewLine _
                              & "            ,KBN5.KBN_NM1                                                          AS SYS_DEL_NM               " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                 " & vbNewLine _
                                          & "                      $LM_MST$..M_FURI_GOODS AS FURI                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR     AS NRSBR               " & vbNewLine _
                                          & "        ON FURI.NRS_BR_CD      = NRSBR.NRS_BR_CD                    " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS      AS GOODS               " & vbNewLine _
                                          & "        ON FURI.NRS_BR_CD      = GOODS.NRS_BR_CD                    " & vbNewLine _
                                          & "       AND FURI.CD_NRS         = GOODS.GOODS_CD_NRS                 " & vbNewLine _
                                          & "       AND GOODS.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST       AS CUST                " & vbNewLine _
                                          & "        ON GOODS.NRS_BR_CD     = CUST.NRS_BR_CD                     " & vbNewLine _
                                          & "       AND GOODS.CUST_CD_L     = CUST.CUST_CD_L                     " & vbNewLine _
                                          & "       AND GOODS.CUST_CD_M     = CUST.CUST_CD_M                     " & vbNewLine _
                                          & "       AND GOODS.CUST_CD_S     = CUST.CUST_CD_S                     " & vbNewLine _
                                          & "       AND GOODS.CUST_CD_SS    = CUST.CUST_CD_SS                    " & vbNewLine _
                                          & "       AND CUST.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN1                " & vbNewLine _
                                          & "        ON GOODS.PKG_UT        = KBN1.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD   = 'N001'                             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN2                " & vbNewLine _
                                          & "        ON GOODS.STD_IRIME_UT  = KBN2.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD   = 'I001'                             " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS      AS G2                  " & vbNewLine _
                                          & "        ON FURI.NRS_BR_CD      = G2.NRS_BR_CD                       " & vbNewLine _
                                          & "       AND FURI.CD_NRS_TO      = G2.GOODS_CD_NRS                    " & vbNewLine _
                                          & "       AND G2.SYS_DEL_FLG      = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_CUST       AS C2                  " & vbNewLine _
                                          & "        ON G2.NRS_BR_CD        = C2.NRS_BR_CD                       " & vbNewLine _
                                          & "       AND G2.CUST_CD_L        = C2.CUST_CD_L                       " & vbNewLine _
                                          & "       AND G2.CUST_CD_M        = C2.CUST_CD_M                       " & vbNewLine _
                                          & "       AND G2.CUST_CD_S        = C2.CUST_CD_S                       " & vbNewLine _
                                          & "       AND G2.CUST_CD_SS       = C2.CUST_CD_SS                      " & vbNewLine _
                                          & "       AND C2.SYS_DEL_FLG      = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN3                " & vbNewLine _
                                          & "        ON G2.PKG_UT           = KBN3.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD   = 'N001'                             " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN4                " & vbNewLine _
                                          & "        ON G2.STD_IRIME_UT     = KBN4.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD   = 'I001'                             " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER       AS USER1               " & vbNewLine _
                                          & "        ON FURI.SYS_ENT_USER   = USER1.USER_CD                      " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER       AS USER2               " & vbNewLine _
                                          & "        ON FURI.SYS_UPD_USER   = USER2.USER_CD                      " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG   = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN        AS KBN5                " & vbNewLine _
                                          & "        ON FURI.SYS_DEL_FLG    = KBN5.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN5.KBN_GROUP_CD   = 'S051'                             " & vbNewLine _
                                          & "       AND KBN5.SYS_DEL_FLG    = '0'                                " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                              " & vbNewLine _
                                         & "      CUST.CUST_CD_L                                  " & vbNewLine _
                                         & "     ,CUST.CUST_CD_M                                  " & vbNewLine _
                                         & "     ,CUST.CUST_CD_S                                  " & vbNewLine _
                                         & "     ,CUST.CUST_CD_SS                                 " & vbNewLine _
                                         & "     ,GOODS.GOODS_CD_CUST                             " & vbNewLine _
                                         & "     ,C2.CUST_CD_L                                    " & vbNewLine _
                                         & "     ,C2.CUST_CD_M                                    " & vbNewLine _
                                         & "     ,C2.CUST_CD_S                                    " & vbNewLine _
                                         & "     ,C2.CUST_CD_SS                                   " & vbNewLine _
                                         & "     ,G2.GOODS_CD_CUST                                " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_FURI As String = "SELECT                                    " & vbNewLine _
                                            & "      COUNT(FURI.CD_NRS)  AS REC_CNT    " & vbNewLine _
                                            & " FROM $LM_MST$..M_FURI_GOODS AS FURI    " & vbNewLine _
                                            & "WHERE CD_NRS      = @CD_NRS             " & vbNewLine _
                                            & "  AND CD_NRS_TO   = @CD_NRS_TO          " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_FURI_GOODS           " & vbNewLine _
                                       & "(                                            " & vbNewLine _
                                       & "       NRS_BR_CD                             " & vbNewLine _
                                       & "      ,CD_NRS                                " & vbNewLine _
                                       & "      ,CD_NRS_TO                             " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                          " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                          " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                          " & vbNewLine _
                                       & "      ,SYS_ENT_USER                          " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                          " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                          " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                          " & vbNewLine _
                                       & "      ,SYS_UPD_USER                          " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                           " & vbNewLine _
                                       & "      ) VALUES (                             " & vbNewLine _
                                       & "       @NRS_BR_CD                            " & vbNewLine _
                                       & "      ,@CD_NRS                               " & vbNewLine _
                                       & "      ,@CD_NRS_TO                            " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                         " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                         " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                         " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                         " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                         " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                         " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                         " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                         " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                          " & vbNewLine _
                                       & ")                                            " & vbNewLine


    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_FURI_GOODS SET                     " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         CD_NRS               = @CD_NRS               " & vbNewLine _
                                       & " AND     CD_NRS_TO            = @CD_NRS_TO            " & vbNewLine

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
    ''' 振替対象商品マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替対象商品マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM290IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM290DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM290DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM290DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 振替対象商品マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替対象商品マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM290IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM290DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM290DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM290DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM290DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("CD_NRS", "CD_NRS")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PKG_UT_NM", "PKG_UT_NM")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_IRIME_UT_NM", "STD_IRIME_UT_NM")
        map.Add("STD_IRIME", "STD_IRIME")
        map.Add("CUST_CD_L_TO", "CUST_CD_L_TO")
        map.Add("CUST_NM_L_TO", "CUST_NM_L_TO")
        map.Add("CUST_CD_M_TO", "CUST_CD_M_TO")
        map.Add("CUST_NM_M_TO", "CUST_NM_M_TO")
        map.Add("CUST_CD_S_TO", "CUST_CD_S_TO")
        map.Add("CUST_NM_S_TO", "CUST_NM_S_TO")
        map.Add("CUST_CD_SS_TO", "CUST_CD_SS_TO")
        map.Add("CUST_NM_SS_TO", "CUST_NM_SS_TO")
        map.Add("GOODS_CD_CUST_TO", "GOODS_CD_CUST_TO")
        map.Add("CD_NRS_TO", "CD_NRS_TO")
        map.Add("GOODS_NM_1_TO", "GOODS_NM_1_TO")
        map.Add("PKG_UT_TO", "PKG_UT_TO")
        map.Add("PKG_UT_NM_TO", "PKG_UT_NM_TO")
        map.Add("STD_IRIME_NB_TO", "STD_IRIME_NB_TO")
        map.Add("STD_IRIME_UT_TO", "STD_IRIME_UT_TO")
        map.Add("STD_IRIME_UT_NM_TO", "STD_IRIME_UT_NM_TO")
        map.Add("STD_IRIME_TO", "STD_IRIME_TO")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM290OUT")

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
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (FURI.SYS_DEL_FLG = @SYS_DEL_FLG  OR FURI.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (FURI.NRS_BR_CD = @NRS_BR_CD OR FURI.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号886
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_NM_1 LIKE @GOODS_NM_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_CD_L_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" C2.CUST_CD_L LIKE @CUST_CD_L_TO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L_TO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" C2.CUST_NM_L LIKE @CUST_NM_L_TO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L_TO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_CD_CUST_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" G2.GOODS_CD_CUST LIKE @GOODS_CD_CUST_TO")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号886
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST_TO", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST_TO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            whereStr = .Item("GOODS_NM_1_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" G2.GOODS_NM_1 LIKE @GOODS_NM_1_TO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1_TO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 振替対象商品マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替対象商品マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectFuriGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM290IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM290DAC.SQL_EXIT_FURI)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM290DAC", "SelectFuriGoodsM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 振替対象商品マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替対象商品マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistFuriGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM290IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM290DAC.SQL_EXIT_FURI, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM290DAC", "CheckExistFuriGoodsM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' 振替対象商品マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替対象商品マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertFuriGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM290IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM290DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM290DAC", "InsertFuriGoodsM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 振替対象商品マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>振替対象商品マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteFuriGoodsM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM290IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM290DAC.SQL_DELETE _
                                                                                     , LMM290DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM290DAC", "DeleteFuriGoodsM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function


    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function



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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(振替対象商品マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS", .Item("CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS_TO", .Item("CD_NRS_TO").ToString(), DBDataType.CHAR))

        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS", .Item("CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS_TO", .Item("CD_NRS_TO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS", .Item("CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_NRS_TO", .Item("CD_NRS_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        With Me._Row

            '画面パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With
        
    End Sub

#End Region

#End Region

#End Region

End Class
