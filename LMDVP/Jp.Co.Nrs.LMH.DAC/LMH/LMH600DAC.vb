' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH600    : EDI送状(BP日通)
'  作  成  者       :  inoue
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMH600DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH600DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String _
        = " SELECT DISTINCT                                         " & vbNewLine _
        & "        HED.NRS_BR_CD                      AS NRS_BR_CD  " & vbNewLine _
        & "      , 'BY'                               AS PTN_ID     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR2.PTN_CD                             " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR1.PTN_CD                             " & vbNewLine _
        & "             ELSE MR3.PTN_CD                             " & vbNewLine _
        & "        END                                AS PTN_CD     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR2.RPT_ID                             " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL                 " & vbNewLine _
        & "             THEN MR1.RPT_ID                             " & vbNewLine _
        & "             ELSE MR3.RPT_ID                             " & vbNewLine _
        & "        END                                AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String _
        = "  SELECT                                                 " & vbNewLine _
        & "         CASE WHEN MR2.PTN_CD IS NOT NULL                " & vbNewLine _
        & "              THEN MR2.RPT_ID                            " & vbNewLine _
        & "              WHEN MR1.PTN_CD IS NOT NULL                " & vbNewLine _
        & "              THEN MR1.RPT_ID                            " & vbNewLine _
        & "              ELSE MR3.RPT_ID                            " & vbNewLine _
        & "         END                    AS RPT_ID                " & vbNewLine _
        & "       , HED.NRS_BR_CD          AS NRS_BR_CD             " & vbNewLine _
        & "       , HED.CUST_CD_L          AS CUST_CD_L             " & vbNewLine _
        & "       , HED.CUST_CD_M          AS CUST_CD_M             " & vbNewLine _
        & "       , HED.EDI_CTL_NO         AS EDI_CTL_NO            " & vbNewLine _
        & "       , DTL.EDI_CTL_NO_CHU     AS EDI_CTL_NO_CHU        " & vbNewLine _
        & "       , HED.TOKUI_CD           AS TOKUI_CD              " & vbNewLine _
        & "       , HED.TOKUI_NM           AS TOKUI_NM              " & vbNewLine _
        & "       , HED.DEST_ZIP           AS DEST_ZIP              " & vbNewLine _
        & "       , HED.DEST_TEL           AS DEST_TEL              " & vbNewLine _
        & "       , HED.DEST_AD1           AS DEST_AD1              " & vbNewLine _
        & "       , HED.DEST_AD2           AS DEST_AD2              " & vbNewLine _
        & "       , HED.DEST_NM            AS DEST_NM               " & vbNewLine _
        & "       , EL.OUTKA_PLAN_DATE     AS OUTKA_PLAN_DATE       " & vbNewLine _
        & "       , DEST.JIS               AS JIS                   " & vbNewLine _
        & "       , HED.OUTKA_SOKO_CD      AS OUTKA_SOKO_CD         " & vbNewLine _
        & "       , HED.DEST_CD            AS DEST_CD               " & vbNewLine _
        & "       , HED.ORDER_TYPE         AS ORDER_TYPE            " & vbNewLine _
        & "       , HED.HACHU_NO           AS HACHU_NO              " & vbNewLine _
        & "       , HED.CUST_ORD_NO        AS CUST_ORD_NO           " & vbNewLine _
        & "       , HED.BUMON_CD           AS BUMON_CD              " & vbNewLine _
        & "       , DTL.GOODS_CD           AS GOODS_CD              " & vbNewLine _
        & "       , DTL.BUYER_GOODS_CD     AS BUYER_GOODS_CD        " & vbNewLine _
        & "       , DTL.GOODS_NM           AS GOODS_NM              " & vbNewLine _
        & "       , DTL.LOT_NO             AS LOT_NO                " & vbNewLine _
        & "       , DTL.PKG_NB             AS PKG_NB                " & vbNewLine _
        & "       , DTL.OUTKA_PKG_NB       AS OUTKA_PKG_NB          " & vbNewLine _
        & "       , DTL.OUTKA_NB           AS OUTKA_NB              " & vbNewLine _
        & "       , HED.BUYER_ORD_NO       AS BUYER_ORD_NO          " & vbNewLine _
        & "       , HED.HOL_KB             AS HOL_KB                " & vbNewLine _
        & "       , HED.KR_TOKUI_CD        AS KR_TOKUI_CD           " & vbNewLine _
        & "       , DTL.ROW_TYPE           AS ROW_TYPE              " & vbNewLine _
        & "       , HED.BIKO_HED1          AS BIKO_HED1             " & vbNewLine _
        & "       , HED.BIKO_HED2          AS BIKO_HED2             " & vbNewLine _
        & "       , G.PKG_UT               AS PKG_UT                " & vbNewLine _
        & "       , DTL.LOT_FLAG           AS LOT_FLAG              " & vbNewLine _
        & "       , HED.SIIRESAKI_CD       AS SIIRESAKI_CD          " & vbNewLine _
        & "       , HED.DENPYO_NO          AS DENPYO_NO_HED         " & vbNewLine _
        & "       , DTL.DENPYO_NO          AS DENPYO_NO_DTL         " & vbNewLine _
        & "       , UNSO.UNSOCO_CD         AS UNSOCO_CD             " & vbNewLine _
        & "       , UNSO.UNSOCO_BR_CD      AS UNSOCO_BR_CD          " & vbNewLine _
        & "       , UNSO.UNSOCO_NM         AS UNSOCO_NM             " & vbNewLine _
        & "       , UNSO.UNSOCO_BR_NM      AS UNSOCO_BR_NM          " & vbNewLine _
        & "       , HED.MOSIOKURI_KB       AS MOSIOKURI_KB          " & vbNewLine _
        & "       , SM.PKG_NB              AS PKG_NB_SUM            " & vbNewLine _
        & "       , SM.OUTKA_PKG_NB        AS OUTKA_PKG_NB_SUM      " & vbNewLine _
        & "       , SM.OUTKA_NB            AS OUTKA_NB_SUM          " & vbNewLine _
        & "       , SM.TOTAL_QT            AS TOTAL_QT_SUM          " & vbNewLine _
        & "       , SM.TOTAL_WT            AS TOTAL_WT_SUM          " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String _
        = "    FROM                                                   " & vbNewLine _
        & "         $LM_TRN$..H_OUTKAEDI_HED_BP AS HED                " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_TRN$..H_OUTKAEDI_DTL_BP AS DTL                " & vbNewLine _
        & "      ON HED.SYS_DEL_FLG = '0'                             " & vbNewLine _
        & "     AND DTL.SYS_DEL_FLG = '0'                             " & vbNewLine _
        & "     AND HED.DEL_KB      = '0'                             " & vbNewLine _
        & "     AND DTL.DEL_KB      = '0'                             " & vbNewLine _
        & "     AND HED.CRT_DATE    = DTL.CRT_DATE                    " & vbNewLine _
        & "     AND HED.[FILE_NAME] = DTL.[FILE_NAME]                 " & vbNewLine _
        & "     AND HED.REC_NO      = DTL.REC_NO                      " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_MST$..M_DEST AS DEST                          " & vbNewLine _
        & "      ON DEST.NRS_BR_CD = HED.NRS_BR_CD                    " & vbNewLine _
        & "     AND DEST.CUST_CD_L = HED.CUST_CD_L                    " & vbNewLine _
        & "     AND DEST.DEST_CD   = HED.DEST_CD                      " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_TRN$..H_OUTKAEDI_L AS EL                      " & vbNewLine _
        & "      ON EL.EDI_CTL_NO     = HED.EDI_CTL_NO                " & vbNewLine _
        & "     AND EL.NRS_BR_CD      = HED.NRS_BR_CD                 " & vbNewLine _
        & "     AND EL.SYS_DEL_FLG    = '0'                           " & vbNewLine _
        & "     AND HED.SYS_DEL_FLG   = '0'                           " & vbNewLine _
        & "     AND HED.DEL_KB        = '0'                           " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_TRN$..H_OUTKAEDI_M AS EM                      " & vbNewLine _
        & "      ON EM.EDI_CTL_NO     = DTL.EDI_CTL_NO                " & vbNewLine _
        & "     AND EM.EDI_CTL_NO_CHU = DTL.EDI_CTL_NO_CHU            " & vbNewLine _
        & "     AND EM.NRS_BR_CD      = DTL.NRS_BR_CD                 " & vbNewLine _
        & "     AND EM.SYS_DEL_FLG    = '0'                           " & vbNewLine _
        & "     AND DTL.SYS_DEL_FLG   = '0'                           " & vbNewLine _
        & "     AND DTL.DEL_KB        = '0'                           " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_MST$..M_UNSOCO AS UNSO                        " & vbNewLine _
        & "      ON EL.UNSO_CD    = UNSO.UNSOCO_CD                    " & vbNewLine _
        & "     AND EL.UNSO_BR_CD = UNSO.UNSOCO_BR_CD                 " & vbNewLine _
        & "     AND EL.NRS_BR_CD  = UNSO.NRS_BR_CD                    " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_MST$..M_GOODS AS G                            " & vbNewLine _
        & "      ON G.NRS_BR_CD = EM.NRS_BR_CD                        " & vbNewLine _
        & "     AND G.GOODS_CD_NRS = EM.NRS_GOODS_CD                  " & vbNewLine _
        & "     AND EM.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "        (                                                  " & vbNewLine _
        & "          SELECT                                           " & vbNewLine _
        & "                 DTL.CRT_DATE                              " & vbNewLine _
        & "               , DTL.[FILE_NAME]                           " & vbNewLine _
        & "               , DTL.REC_NO                                " & vbNewLine _
        & "               , SUM(DTL.PKG_NB)       AS PKG_NB           " & vbNewLine _
        & "               , SUM(DTL.OUTKA_PKG_NB) AS OUTKA_PKG_NB    " & vbNewLine _
        & "               , SUM(DTL.OUTKA_NB) AS OUTKA_NB             " & vbNewLine _
        & "               , SUM(DTL.TOTAL_QT) AS TOTAL_QT             " & vbNewLine _
        & "               , SUM(DTL.TOTAL_WT) AS TOTAL_WT             " & vbNewLine _
        & "            FROM                                           " & vbNewLine _
        & "                 $LM_TRN$..H_OUTKAEDI_DTL_BP AS DTL        " & vbNewLine _
        & "           WHERE                                           " & vbNewLine _
        & "                 DTL.EDI_CTL_NO  = @EDI_CTL_NO             " & vbNewLine _
        & "             AND DTL.NRS_BR_CD   = @NRS_BR_CD              " & vbNewLine _
        & "             AND DTL.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & "             AND DTL.DEL_KB      = '0'                     " & vbNewLine _
        & "           GROUP BY                                        " & vbNewLine _
        & "                 DTL.CRT_DATE                              " & vbNewLine _
        & "               , DTL.[FILE_NAME]                           " & vbNewLine _
        & "               , DTL.REC_NO                                " & vbNewLine _
        & "         ) AS SM                                           " & vbNewLine _
        & "      ON HED.SYS_DEL_FLG = '0'                             " & vbNewLine _
        & "     AND HED.DEL_KB      = '0'                             " & vbNewLine _
        & "     AND HED.CRT_DATE    = SM.CRT_DATE                     " & vbNewLine _
        & "     AND HED.[FILE_NAME] = SM.[FILE_NAME]                  " & vbNewLine _
        & "     AND HED.REC_NO      = SM.REC_NO                       " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_MST$..M_CUST_RPT AS CUST_RPT_1                " & vbNewLine _
        & "      ON CUST_RPT_1.PTN_ID      = 'BY'                     " & vbNewLine _
        & "     AND CUST_RPT_1.NRS_BR_CD   = HED.NRS_BR_CD            " & vbNewLine _
        & "     AND CUST_RPT_1.CUST_CD_L   = HED.CUST_CD_L            " & vbNewLine _
        & "     AND CUST_RPT_1.CUST_CD_M   = HED.CUST_CD_M            " & vbNewLine _
        & "     AND CUST_RPT_1.CUST_CD_S   = '00'                     " & vbNewLine _
        & "     AND CUST_RPT_1.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "       $LM_MST$..M_RPT AS MR1                              " & vbNewLine _
        & "      ON MR1.NRS_BR_CD          = CUST_RPT_1.NRS_BR_CD     " & vbNewLine _
        & "     AND MR1.PTN_ID             = CUST_RPT_1.PTN_ID        " & vbNewLine _
        & "     AND MR1.PTN_CD             = CUST_RPT_1.PTN_CD        " & vbNewLine _
        & "     AND MR1.SYS_DEL_FLG        = '0'                      " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "         $LM_MST$..M_CUST_RPT AS CUST_RPT_2                " & vbNewLine _
        & "      ON CUST_RPT_2.PTN_ID      = 'BY'                     " & vbNewLine _
        & "     AND CUST_RPT_2.NRS_BR_CD   = G.NRS_BR_CD              " & vbNewLine _
        & "     AND CUST_RPT_2.CUST_CD_L   = G.CUST_CD_L              " & vbNewLine _
        & "     AND CUST_RPT_2.CUST_CD_M   = G.CUST_CD_M              " & vbNewLine _
        & "     AND CUST_RPT_2.CUST_CD_S   = '00'                     " & vbNewLine _
        & "     AND CUST_RPT_2.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "       $LM_MST$..M_RPT AS MR2                              " & vbNewLine _
        & "      ON MR2.NRS_BR_CD          = CUST_RPT_2.NRS_BR_CD     " & vbNewLine _
        & "     AND MR2.PTN_ID             = CUST_RPT_2.PTN_ID        " & vbNewLine _
        & "     AND MR2.PTN_CD             = CUST_RPT_2.PTN_CD        " & vbNewLine _
        & "     AND MR2.SYS_DEL_FLG        = '0'                      " & vbNewLine _
        & "    LEFT JOIN                                              " & vbNewLine _
        & "       $LM_MST$..M_RPT AS MR3                              " & vbNewLine _
        & "      ON MR3.NRS_BR_CD          = HED.NRS_BR_CD            " & vbNewLine _
        & "     AND MR3.PTN_ID             = 'BY'                     " & vbNewLine _
        & "     AND MR3.STANDARD_FLAG      = '01'                     " & vbNewLine _
        & "     AND MR3.SYS_DEL_FLG        = '0'                      " & vbNewLine

    ''' <summary>
    ''' SQL_WHERE
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE As String _
        = "   WHERE                                        " & vbNewLine _
        & "         HED.SYS_DEL_FLG = '0'                  " & vbNewLine _
        & "     AND DTL.SYS_DEL_FLG = '0'                  " & vbNewLine _
        & "     AND HED.DEL_KB      = '0'                  " & vbNewLine _
        & "     AND DTL.SYS_DEL_FLG = '0'                  " & vbNewLine _
        & "     AND EM.SYS_DEL_FLG  = '0'                  " & vbNewLine _
        & "     AND HED.EDI_CTL_NO  = @EDI_CTL_NO          " & vbNewLine _
        & "     AND HED.NRS_BR_CD   = @NRS_BR_CD           " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String _
        = " ORDER BY                   " & vbNewLine _
        & "       UNSO.UNSOCO_CD       " & vbNewLine _
        & "     , UNSO.UNSOCO_BR_CD    " & vbNewLine _
        & "     , HED.EDI_CTL_NO       " & vbNewLine _
        & "     , DTL.EDI_CTL_NO_CHU   " & vbNewLine


#End Region

#Region "データセット名"

    ''' <summary>
    ''' テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TableNames

        ''' <summary>
        ''' 帳票テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RPT As String = "M_RPT"

        ''' <summary>
        ''' INテーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_TABLE As String = "LMH600IN"


        ''' <summary>
        ''' OUTテーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_TABLE As String = "LMH600OUT"


        ''' <summary>
        ''' EDI印刷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI_PRINT As String = "H_EDI_PRINT"

    End Class


    ''' <summary>
    ''' メソッド名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Functions

        ''' <summary>
        ''' 帳票パターン
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SELECT_MPRT As String = "SelectMPrt"

        ''' <summary>
        ''' 印刷データ取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SELECT_PRT_DATA As String = "SelectPrintData"

    End Class
#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow = Nothing

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder = Nothing

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = Nothing

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
        Dim inTbl As DataTable = ds.Tables(TableNames.IN_TABLE)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH600DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMH600DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMH600DAC.SQL_WHERE)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetSelectParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TableNames.RPT)

        Return ds

    End Function


    ''' <summary>
    ''' 納品送状対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品送状出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TableNames.IN_TABLE)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH600DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH600DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMH600DAC.SQL_WHERE)            'SQL構築(データ抽出用WHERE句)(新規)
        Me._StrSql.Append(LMH600DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetSelectParameter()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("TOKUI_NM", "TOKUI_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_AD1", "DEST_AD1")
        map.Add("DEST_AD2", "DEST_AD2")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("JIS", "JIS")
        map.Add("OUTKA_SOKO_CD", "OUTKA_SOKO_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("ORDER_TYPE", "ORDER_TYPE")
        map.Add("HACHU_NO", "HACHU_NO")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUMON_CD", "BUMON_CD")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("BUYER_GOODS_CD", "BUYER_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("HOL_KB", "HOL_KB")
        map.Add("KR_TOKUI_CD", "KR_TOKUI_CD")
        map.Add("ROW_TYPE", "ROW_TYPE")
        map.Add("BIKO_HED1", "BIKO_HED1")
        map.Add("BIKO_HED2", "BIKO_HED2")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("LOT_FLAG", "LOT_FLAG")
        map.Add("SIIRESAKI_CD", "SIIRESAKI_CD")
        map.Add("DENPYO_NO_HED", "DENPYO_NO_HED")
        map.Add("DENPYO_NO_DTL", "DENPYO_NO_DTL")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("MOSIOKURI_KB", "MOSIOKURI_KB")
        map.Add("PKG_NB_SUM", "PKG_NB_SUM")
        map.Add("OUTKA_PKG_NB_SUM", "OUTKA_PKG_NB_SUM")
        map.Add("OUTKA_NB_SUM", "OUTKA_NB_SUM")
        map.Add("TOTAL_QT_SUM", "TOTAL_QT_SUM")
        map.Add("TOTAL_WT_SUM", "TOTAL_WT_SUM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TableNames.OUT_TABLE)

        Return ds

    End Function


#End Region

#Region "設定処理"

#Region "パラメータ設定"


    ''' <summary>
    ''' 検索用パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectParameter()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

    End Sub



#End Region

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
