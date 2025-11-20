' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM360DAC : 請求先テンプレートマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM360DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM360DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(SEIQTEM.SEIQTO_CD)      AS SELECT_CNT   " & vbNewLine



    ''' <summary>
    ''' 請求テンプレートマスタデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                " & vbNewLine _
                                            & "       SEIQTEM.NRS_BR_CD                       AS NRS_BR_CD            " & vbNewLine _
                                            & "      ,NRSBR.NRS_BR_NM                         AS NRS_BR_NM            " & vbNewLine _
                                            & "      ,SEIQTEM.SEIQTO_CD                       AS SEIQTO_CD            " & vbNewLine _
                                            & "      ,SEIQTO.SEIQTO_NM                        AS SEIQTO_NM            " & vbNewLine _
                                            & "      ,SEIQTEM.PTN_CD                          AS PTN_CD               " & vbNewLine _
                                            & "      ,SEIQTEM.GROUP_KB                        AS GROUP_KB             " & vbNewLine _
                                            & "      ,KBN1.KBN_NM1                            AS GROUP_KB_NM          " & vbNewLine _
                                            & "      ,SEIQTEM.SEIQKMK_CD                      AS SEIQKMK_CD           " & vbNewLine _
                                            & "      ,SEIQTEM.SEIQKMK_CD_S                    AS SEIQKMK_CD_S         " & vbNewLine _
                                            & "      ,SEIQKMK.SEIQKMK_NM                      AS SEIQKMK_NM           " & vbNewLine _
                                            & "      ,SEIQTEM.KEISAN_TLGK                     AS KEISAN_TLGK          " & vbNewLine _
                                            & "      ,SEIQTEM.NEBIKI_RT                       AS NEBIKI_RT            " & vbNewLine _
                                            & "      ,SEIQTEM.NEBIKI_GK                       AS NEBIKI_GK            " & vbNewLine _
                                            & "      ,SEIQTEM.TEKIYO                          AS TEKIYO               " & vbNewLine _
                                            & "      ,SEIQTEM.TCUST_BPCD                      AS TCUST_BPCD           " & vbNewLine _
                                            & "      ,MBP.BP_NM1                              AS TCUST_BPNM           " & vbNewLine _
                                            & "      ,SEIQTEM.SYS_ENT_DATE                    AS SYS_ENT_DATE         " & vbNewLine _
                                            & "      ,USER1.USER_NM                           AS SYS_ENT_USER_NM      " & vbNewLine _
                                            & "      ,SEIQTEM.SYS_UPD_DATE                    AS SYS_UPD_DATE         " & vbNewLine _
                                            & "      ,SEIQTEM.SYS_UPD_TIME                    AS SYS_UPD_TIME         " & vbNewLine _
                                            & "      ,USER2.USER_NM                           AS SYS_UPD_USER_NM      " & vbNewLine _
                                            & "      ,SEIQTEM.SYS_DEL_FLG                     AS SYS_DEL_FLG          " & vbNewLine _
                                            & "      ,KBN2.KBN_NM1                            AS SYS_DEL_NM           " & vbNewLine


#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                 " & vbNewLine _
                                          & "                      $LM_MST$..M_SEIQ_TEMPLATE AS SEIQTEM           " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                   " & vbNewLine _
                                          & "        ON SEIQTEM.NRS_BR_CD    = NRSBR.NRS_BR_CD                    " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                " & vbNewLine _
                                          & "      (SELECT                                                        " & vbNewLine _
                                          & "        NRS_BR_CD                                                    " & vbNewLine _
                                          & "       ,SEIQTO_CD                                                    " & vbNewLine _
                                          & "       ,SEIQTO_NM + '　' + SEIQTO_BUSYO_NM    AS SEIQTO_NM           " & vbNewLine _
                                          & "       FROM                                                          " & vbNewLine _
                                          & "        $LM_MST$..M_SEIQTO                                           " & vbNewLine _
                                          & "       WHERE                                                         " & vbNewLine _
                                          & "        SYS_DEL_FLG    = '0'                                         " & vbNewLine _
                                          & "                                   )      AS SEIQTO                  " & vbNewLine _
                                          & "        ON SEIQTEM.NRS_BR_CD    = SEIQTO.NRS_BR_CD                   " & vbNewLine _
                                          & "       AND SEIQTEM.SEIQTO_CD    = SEIQTO.SEIQTO_CD                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                " & vbNewLine _
                                          & "      (SELECT                                                        " & vbNewLine _
                                          & "        GROUP_KB                                                     " & vbNewLine _
                                          & "       ,SEIQKMK_CD                                                   " & vbNewLine _
                                          & "       ,SEIQKMK_CD_S                                                 " & vbNewLine _
                                          & "       ,SEIQKMK_NM                                                   " & vbNewLine _
                                          & "       FROM                                                          " & vbNewLine _
                                          & "        $LM_MST$..M_SEIQKMK                                          " & vbNewLine _
                                          & "       WHERE                                                         " & vbNewLine _
                                          & "        SYS_DEL_FLG    = '0'                                         " & vbNewLine _
                                          & "                                   )      AS SEIQKMK                 " & vbNewLine _
                                          & "        ON SEIQTEM.GROUP_KB     = SEIQKMK.GROUP_KB                   " & vbNewLine _
                                          & "       AND SEIQTEM.SEIQKMK_CD   = SEIQKMK.SEIQKMK_CD                 " & vbNewLine _
                                          & "       AND SEIQTEM.SEIQKMK_CD_S = SEIQKMK.SEIQKMK_CD_S               " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER1                   " & vbNewLine _
                                          & "        ON SEIQTEM.SYS_ENT_USER = USER1.USER_CD                      " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER    AS USER2                   " & vbNewLine _
                                          & "       ON SEIQTEM.SYS_UPD_USER  = USER2.USER_CD                      " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG    = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1                    " & vbNewLine _
                                          & "        ON SEIQTEM.GROUP_KB     = KBN1.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD    = 'S024'                             " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG     = '0'                                " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2                    " & vbNewLine _
                                          & "        ON SEIQTEM.SYS_DEL_FLG  = KBN2.KBN_CD                        " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD    = 'S051'                             " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG     = '0'                                " & vbNewLine _
                                          & "      LEFT JOIN ABM_DB..M_BP              AS MBP                     " & vbNewLine _
                                          & "        ON MBP.BP_CD            = SEIQTEM.TCUST_BPCD                 " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                            " & vbNewLine _
                                         & "      SEIQTEM.SEIQTO_CD                             " & vbNewLine _
                                         & "     ,SEIQTEM.PTN_CD                                " & vbNewLine _
                                         & "     ,SEIQTEM.GROUP_KB                              " & vbNewLine _
                                         & "     ,SEIQTEM.SEIQKMK_CD                            " & vbNewLine _
                                         & "     ,SEIQTEM.SEIQKMK_CD_S                          " & vbNewLine

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
    Private Const SQL_EXIT_SEIQTEM As String = "SELECT                                       " & vbNewLine _
                                            & "      COUNT(SEIQTEM.SEIQTO_CD)  AS REC_CNT    " & vbNewLine _
                                            & " FROM $LM_MST$..M_SEIQ_TEMPLATE AS SEIQTEM    " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD               " & vbNewLine _
                                            & "  AND SEIQTO_CD    = @SEIQTO_CD               " & vbNewLine _
                                            & "  AND PTN_CD       = @PTN_CD                  " & vbNewLine

#If True Then   'ADD 2021/11/11 025485 【LMS】ABP_指摘・要望-69_荷主未登録請求先で製品セグメントが空
    Private Const SQL_CHKCUST_HOKAN As String = "SELECT                                " & vbNewLine _
                                            & "   COUNT(CUST.NRS_BR_CD)  AS REC_CNT    " & vbNewLine _
                                            & "FROM  $LM_MST$..M_CUST  CUST            " & vbNewLine _
                                            & "WHERE                                   " & vbNewLine _
                                            & "     CUST.NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                            & " AND CUST.SYS_DEL_FLG = '0'             " & vbNewLine _
                                            & " --保管料請求先マスタコード             " & vbNewLine _
                                            & " AND CUST.HOKAN_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine

    Private Const SQL_CHKCUST_NIYAKU As String = "SELECT                                " & vbNewLine _
                                            & "   COUNT(CUST.NRS_BR_CD)  AS REC_CNT    " & vbNewLine _
                                            & "FROM  $LM_MST$..M_CUST  CUST            " & vbNewLine _
                                            & "WHERE                                   " & vbNewLine _
                                            & "     CUST.NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                            & " AND CUST.SYS_DEL_FLG = '0'             " & vbNewLine _
                                            & " --荷役料請求先マスタコード             " & vbNewLine _
                                            & " AND CUST.NIYAKU_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine

    Private Const SQL_CHKCUST_UMCHIN As String = "SELECT                                " & vbNewLine _
                                            & "   COUNT(CUST.NRS_BR_CD)  AS REC_CNT    " & vbNewLine _
                                            & "FROM  $LM_MST$..M_CUST  CUST            " & vbNewLine _
                                            & "WHERE                                   " & vbNewLine _
                                            & "     CUST.NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                            & " AND CUST.SYS_DEL_FLG = '0'             " & vbNewLine _
                                            & " --運賃料請求先マスタコード             " & vbNewLine _
                                            & " AND CUST.UNCHIN_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine

    Private Const SQL_CHKCUST_SAGYO As String = "SELECT                                " & vbNewLine _
                                            & "   COUNT(CUST.NRS_BR_CD)  AS REC_CNT    " & vbNewLine _
                                            & "FROM  $LM_MST$..M_CUST  CUST            " & vbNewLine _
                                            & "WHERE                                   " & vbNewLine _
                                            & "     CUST.NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                            & " AND CUST.SYS_DEL_FLG = '0'             " & vbNewLine _
                                            & " --作業料請求先マスタコード             " & vbNewLine _
                                            & " AND CUST.SAGYO_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine

    Private Const SQL_CHKCUST_OYA As String = "SELECT                                " & vbNewLine _
                                            & "   COUNT(CUST.NRS_BR_CD)  AS REC_CNT    " & vbNewLine _
                                            & "FROM  $LM_MST$..M_CUST  CUST            " & vbNewLine _
                                            & "WHERE                                   " & vbNewLine _
                                            & "     CUST.NRS_BR_CD  = @NRS_BR_CD       " & vbNewLine _
                                            & " AND CUST.SYS_DEL_FLG = '0'             " & vbNewLine _
                                            & " --主請求先マスタコード           　　  " & vbNewLine _
                                            & " AND CUST.OYA_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine

#End If

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SEIQ_TEMPLATE        " & vbNewLine _
                                       & "(                                            " & vbNewLine _
                                       & "       NRS_BR_CD                             " & vbNewLine _
                                       & "      ,SEIQTO_CD                             " & vbNewLine _
                                       & "      ,PTN_CD                                " & vbNewLine _
                                       & "      ,GROUP_KB                              " & vbNewLine _
                                       & "      ,SEIQKMK_CD                            " & vbNewLine _
                                       & "      ,SEIQKMK_CD_S                          " & vbNewLine _
                                       & "      ,KEISAN_TLGK                           " & vbNewLine _
                                       & "      ,NEBIKI_RT                             " & vbNewLine _
                                       & "      ,NEBIKI_GK                             " & vbNewLine _
                                       & "      ,TEKIYO                                " & vbNewLine _
                                       & "      ,TCUST_BPCD                            " & vbNewLine _
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
                                       & "      ,@SEIQTO_CD                            " & vbNewLine _
                                       & "      ,@PTN_CD                               " & vbNewLine _
                                       & "      ,@GROUP_KB                             " & vbNewLine _
                                       & "      ,@SEIQKMK_CD                           " & vbNewLine _
                                       & "      ,@SEIQKMK_CD_S                         " & vbNewLine _
                                       & "      ,@KEISAN_TLGK                          " & vbNewLine _
                                       & "      ,@NEBIKI_RT                            " & vbNewLine _
                                       & "      ,@NEBIKI_GK                            " & vbNewLine _
                                       & "      ,@TEKIYO                               " & vbNewLine _
                                       & "      ,@TCUST_BPCD                           " & vbNewLine _
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
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SEIQ_TEMPLATE SET                  " & vbNewLine _
                                       & "        GROUP_KB              = @GROUP_KB             " & vbNewLine _
                                       & "       ,SEIQKMK_CD            = @SEIQKMK_CD           " & vbNewLine _
                                       & "       ,SEIQKMK_CD_S          = @SEIQKMK_CD_S         " & vbNewLine _
                                       & "       ,KEISAN_TLGK           = @KEISAN_TLGK          " & vbNewLine _
                                       & "       ,NEBIKI_RT             = @NEBIKI_RT            " & vbNewLine _
                                       & "       ,NEBIKI_GK             = @NEBIKI_GK            " & vbNewLine _
                                       & "       ,TEKIYO                = @TEKIYO               " & vbNewLine _
                                       & "       ,TCUST_BPCD            = @TCUST_BPCD           " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     SEIQTO_CD            = @SEIQTO_CD            " & vbNewLine _
                                       & " AND     PTN_CD               = @PTN_CD               " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_SEIQ_TEMPLATE SET                  " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     SEIQTO_CD            = @SEIQTO_CD            " & vbNewLine _
                                       & " AND     PTN_CD               = @PTN_CD               " & vbNewLine

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
    ''' 請求先テンプレートマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM360DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM360DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM360DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM360DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM360DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("GROUP_KB_NM", "GROUP_KB_NM")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_CD_S", "SEIQKMK_CD_S")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM360OUT")

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
                andstr.Append(" (SEIQTEM.SYS_DEL_FLG = @SYS_DEL_FLG  OR SEIQTEM.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SEIQTEM.NRS_BR_CD = @NRS_BR_CD OR SEIQTEM.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTEM.SEIQTO_CD LIKE @SEIQTO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTO.SEIQTO_NM LIKE @SEIQTO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("PTN_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTEM.PTN_CD LIKE @PTN_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("GROUP_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTEM.GROUP_KB = @GROUP_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQKMK_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTEM.SEIQKMK_CD LIKE @SEIQKMK_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQKMK_CD_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTEM.SEIQKMK_CD_S = @SEIQKMK_CD_S")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("TEKIYO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTEM.TEKIYO LIKE @TEKIYO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEKIYO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
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
    ''' 請求先テンプレートマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM360DAC.SQL_EXIT_SEIQTEM)
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

        MyBase.Logger.WriteSQLLog("LMM360DAC", "SelectSeiqTemplateM", cmd)

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
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_EXIT_SEIQTEM, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "CheckExistSeiqTemplateM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

#If True Then   'ADD 2022/11/11 025485 【LMS】ABP_指摘・要望-69_荷主未登録請求先で製品セグメントが空

    ''' <summary>
    ''' 荷主マスタ各請求先存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckCust_HOKAN(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_CHKCUST_HOKAN, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamChackCUST()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "CheckCust_HOKAN", cmd)

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
    ''' 荷主マスタ各請求先存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckCust_NIYAKU(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_CHKCUST_NIYAKU, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamChackCUST()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "CheckCust_NIYAKU", cmd)

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
    ''' 荷主マスタ各請求先存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckCust_UNCHIN(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_CHKCUST_UMCHIN, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamChackCUST()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "CheckCust_UNCHIN", cmd)

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
    ''' 荷主マスタ各請求先存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckCust_SAGYO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_CHKCUST_SAGYO, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamChackCUST()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "CheckCust_SAGYO", cmd)

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
    ''' 荷主マスタ各請求先存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckCust_OYA(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_CHKCUST_OYA, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamChackCUST()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "CheckCust_OYA", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

#End If

    ''' <summary>
    ''' 請求先テンプレートマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM360DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "InsertSeiqTemplateM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM360DAC.SQL_UPDATE _
                                                                                     , LMM360DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "UpdateSeiqTemplateM", cmd)

        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先テンプレートマスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM360IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM360DAC.SQL_DELETE _
                                                                                     , LMM360DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM360DAC", "DeleteSeiqTemplateM", cmd)

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
    ''' パラメータ設定モジュール(請求先テンプレートマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))		'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", .Item("PTN_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#If True Then   'ADD 2022/11/11 025485 【LMS】ABP_指摘・要望-69_荷主未登録請求先で製品セグメントが空
    ''' <summary>
    ''' パラメータ設定モジュール(請求先テンプレートマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamChackCUST()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))        '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

        End With

    End Sub
#End If

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
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))		'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", .Item("PTN_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", .Item("GROUP_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD", .Item("SEIQKMK_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQKMK_CD_S", .Item("SEIQKMK_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEISAN_TLGK", .Item("KEISAN_TLGK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_RT", .Item("NEBIKI_RT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEBIKI_GK", .Item("NEBIKI_GK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEKIYO", .Item("TEKIYO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TCUST_BPCD", .Item("TCUST_BPCD").ToString(), DBDataType.NVARCHAR))

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

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))		'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_CD", Me._Row.Item("PTN_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
