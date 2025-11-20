' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC500    : 納品書印刷
'  作  成  者       :  [HORI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class LMC502DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "共通"

    ''' <summary>
    ''' 設定値(荷主明細マスタ)取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MCUST_DETAILS As String = "SELECT                              " & vbNewLine _
                                                & " SET_NAIYO    AS SET_NAIYO               " & vbNewLine _
                                                & "FROM                                     " & vbNewLine _
                                                & "$LM_MST$..M_CUST_DETAILS MCD             " & vbNewLine _
                                                & "RIGHT JOIN                               " & vbNewLine _
                                                & "(SELECT                                  " & vbNewLine _
                                                & " CUST_CD_L                               " & vbNewLine _
                                                & " FROM $LM_TRN$..C_OUTKA_L                " & vbNewLine _
                                                & " WHERE                                   " & vbNewLine _
                                                & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD        " & vbNewLine _
                                                & " AND C_OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine _
                                                & " ) CL                                    " & vbNewLine _
                                                & "ON MCD.CUST_CD = CL.CUST_CD_L            " & vbNewLine _
                                                & "WHERE                                    " & vbNewLine _
                                                & "MCD.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
                                                & "AND MCD.SUB_KB = '25'                    " & vbNewLine

#End Region '共通

#Region "LMC900"

    ''' <summary>
    ''' 印刷データ抽出
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_LMC900_SELECT_MAIN As String = "" _
        & "SELECT                                               " & vbNewLine _
        & "   CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID  " & vbNewLine _
        & "        WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID  " & vbNewLine _
        & "        ELSE MR3.RPT_ID                              " & vbNewLine _
        & "        END AS RPT_ID                                " & vbNewLine _
        & "  ,MAIN.NRS_BR_CD                                    " & vbNewLine _
        & "  ,MAIN.CUST_CD_L                                    " & vbNewLine _
        & "  ,MAIN.CUST_CD_M                                    " & vbNewLine _
        & "  ,MAIN.OUTKA_NO_L                                   " & vbNewLine _
        & "  ,MAIN.OUTKA_NO_M                                   " & vbNewLine _
        & "  ,MAIN.OUTKA_NO_S                                   " & vbNewLine _
        & "  ,'' AS DENP_NO                                     " & vbNewLine _
        & "  ,'' AS SEIRI_NO                                    " & vbNewLine _
        & "  ,'' AS GYO_NO                                      " & vbNewLine _
        & "  ,'1' AS DATA_KBN                                   " & vbNewLine _
        & "  ,'納品案内書' AS TITLE                             " & vbNewLine _
        & "  ,MAIN.A_DATE                                       " & vbNewLine _
        & "  ,MAIN.A_TORIHIKI_KBN                               " & vbNewLine _
        & "  ,MAIN.A_SEIQTO_CD                                  " & vbNewLine _
        & "  ,MAIN.A_SEIQTO_NM_1                                " & vbNewLine _
        & "  ,'' AS A_SEIQTO_NM_2                               " & vbNewLine _
        & "  ,MAIN.A_CHOKUSO_CD                                 " & vbNewLine _
        & "  ,MAIN.A_CHOKUSO_NM                                 " & vbNewLine _
        & "  ,MAIN.A_NONYU_CD                                   " & vbNewLine _
        & "  ,MAIN.A_NONYU_NM                                   " & vbNewLine _
        & "  ,MAIN.A_NONYU_ZIP                                  " & vbNewLine _
        & "  ,MAIN.A_NONYU_ADD_1                                " & vbNewLine _
        & "  ,MAIN.A_NONYU_ADD_2                                " & vbNewLine _
        & "  ,MAIN.A_JISHA_NM                                   " & vbNewLine _
        & "  ,MAIN.A_WH_NM                                      " & vbNewLine _
        & "  ,MAIN.A_MAKER_CD                                   " & vbNewLine _
        & "  ,MAIN.A_DEPO_CD                                    " & vbNewLine _
        & "  ,MAIN.A_GOODS_CD                                   " & vbNewLine _
        & "  ,MAIN.A_GOODS_NM                                   " & vbNewLine _
        & "  ,MAIN.A_DENP_REMARK                                " & vbNewLine _
        & "  ,MAIN.A_DTL_REMARK                                 " & vbNewLine _
        & "  ,MAIN.A_FIXED                                      " & vbNewLine _
        & "  ,MAIN.SURYO                                        " & vbNewLine _
        & "  ,MAIN.LOT_NO                                       " & vbNewLine _
        & "  ,FLOOR(MAIN.SURYO / GODS.PKG_NB) AS MOTOKON        " & vbNewLine _
        & "  ,MAIN.SURYO % GODS.PKG_NB AS HASU                  " & vbNewLine _
        & "  ,MAIN.LT_DATE                                      " & vbNewLine _
        & "  ,MAIN.ORD_NO                                       " & vbNewLine _
        & "FROM (                                               " & vbNewLine _
        & "  SELECT                                             " & vbNewLine _
        & "     MIN(OUTL.NRS_BR_CD) AS NRS_BR_CD                " & vbNewLine _
        & "    ,MIN(OUTL.CUST_CD_L) AS CUST_CD_L                " & vbNewLine _
        & "    ,MIN(OUTL.CUST_CD_M) AS CUST_CD_M                " & vbNewLine _
        & "    ,OUTS.OUTKA_NO_L                                 " & vbNewLine _
        & "    ,OUTS.OUTKA_NO_M                                 " & vbNewLine _
        & "    ,MIN(OUTS.OUTKA_NO_S) AS OUTKA_NO_S              " & vbNewLine _
        & "    ,MIN(EDTL.A_DATE) AS A_DATE                      " & vbNewLine _
        & "    ,EDTL.A_TORIHIKI_KBN                             " & vbNewLine _
        & "    ,MIN(EDTL.A_SEIQTO_CD) AS A_SEIQTO_CD            " & vbNewLine _
        & "    ,MIN(EDTL.A_SEIQTO_NM) AS A_SEIQTO_NM_1          " & vbNewLine _
        & "    ,MIN(EDTL.A_CHOKUSO_CD) AS A_CHOKUSO_CD          " & vbNewLine _
        & "    ,MIN(EDTL.A_CHOKUSO_NM) AS A_CHOKUSO_NM          " & vbNewLine _
        & "    ,MIN(EDTL.A_NONYU_CD) AS A_NONYU_CD              " & vbNewLine _
        & "    ,MIN(EDTL.A_NONYU_NM) AS A_NONYU_NM              " & vbNewLine _
        & "    ,MIN(EDTL.A_NONYU_ZIP) AS A_NONYU_ZIP            " & vbNewLine _
        & "    ,MIN(EDTL.A_NONYU_ADD_1) AS A_NONYU_ADD_1        " & vbNewLine _
        & "    ,MIN(EDTL.A_NONYU_ADD_2) AS A_NONYU_ADD_2        " & vbNewLine _
        & "    ,MIN(EDTL.A_JISHA_NM) AS A_JISHA_NM              " & vbNewLine _
        & "    ,MIN(EDTL.A_WH_NM) AS A_WH_NM                    " & vbNewLine _
        & "    ,MIN(EDTL.A_MAKER_CD) AS A_MAKER_CD              " & vbNewLine _
        & "    ,MIN(EDTL.A_DEPO_CD) AS A_DEPO_CD                " & vbNewLine _
        & "    ,MIN(EDTL.A_GOODS_CD) AS A_GOODS_CD              " & vbNewLine _
        & "    ,MIN(EDTL.A_GOODS_NM) AS A_GOODS_NM              " & vbNewLine _
        & "    ,MIN(EDTL.A_DENP_REMARK) AS A_DENP_REMARK        " & vbNewLine _
        & "    ,MIN(EDTL.A_DTL_REMARK) AS A_DTL_REMARK          " & vbNewLine _
        & "    ,MIN(EDTL.A_FIXED) AS A_FIXED                    " & vbNewLine _
        & "    ,SUM(OUTS.ALCTD_NB) AS SURYO                     " & vbNewLine _
        & "    ,OUTS.LOT_NO                                     " & vbNewLine _
        & "    ,MIN(ZAIK.LT_DATE) AS LT_DATE                    " & vbNewLine _
        & "    ,MIN(OUTM.GOODS_CD_NRS) AS GOODS_CD_NRS          " & vbNewLine _
        & "    ,MIN(OUTL.CUST_ORD_NO) AS ORD_NO                 " & vbNewLine _
        & "  FROM                                               " & vbNewLine _
        & "    --出荷データL                                    " & vbNewLine _
        & "    $LM_TRN$..C_OUTKA_L AS OUTL                      " & vbNewLine _
        & "    --出荷データM                                    " & vbNewLine _
        & "    LEFT JOIN                                        " & vbNewLine _
        & "      $LM_TRN$..C_OUTKA_M AS OUTM                    " & vbNewLine _
        & "      ON                                             " & vbNewLine _
        & "            OUTM.NRS_BR_CD = OUTL.NRS_BR_CD          " & vbNewLine _
        & "        AND OUTM.OUTKA_NO_L = OUTL.OUTKA_NO_L        " & vbNewLine _
        & "        AND OUTM.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "    --出荷データS                                    " & vbNewLine _
        & "    LEFT JOIN                                        " & vbNewLine _
        & "      $LM_TRN$..C_OUTKA_S AS OUTS                    " & vbNewLine _
        & "      ON                                             " & vbNewLine _
        & "            OUTS.NRS_BR_CD = OUTM.NRS_BR_CD          " & vbNewLine _
        & "        AND OUTS.OUTKA_NO_L = OUTM.OUTKA_NO_L        " & vbNewLine _
        & "        AND OUTS.OUTKA_NO_M = OUTM.OUTKA_NO_M        " & vbNewLine _
        & "        AND OUTS.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "    --在庫データ                                     " & vbNewLine _
        & "    LEFT JOIN                                        " & vbNewLine _
        & "      $LM_TRN$..D_ZAI_TRS AS ZAIK                    " & vbNewLine _
        & "      ON                                             " & vbNewLine _
        & "            ZAIK.NRS_BR_CD = OUTS.NRS_BR_CD          " & vbNewLine _
        & "        AND ZAIK.ZAI_REC_NO = OUTS.ZAI_REC_NO        " & vbNewLine _
        & "    --物産アニマルヘルス_出荷指示EDIデータ           " & vbNewLine _
        & "    LEFT JOIN                                        " & vbNewLine _
        & "      $LM_TRN$..H_OUTKAEDI_DTL_BAH AS EDTL           " & vbNewLine _
        & "      ON                                             " & vbNewLine _
        & "            EDTL.OUTKA_CTL_NO = OUTM.OUTKA_NO_L      " & vbNewLine _
        & "        AND EDTL.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M  " & vbNewLine _
        & "  WHERE                                              " & vbNewLine _
        & "        OUTL.NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
        & "    AND OUTL.OUTKA_NO_L = @KANRI_NO_L                " & vbNewLine _
        & "    AND OUTL.SYS_DEL_FLG = '0'                       " & vbNewLine _
        & "  GROUP BY                                           " & vbNewLine _
        & "     OUTS.OUTKA_NO_L                                 " & vbNewLine _
        & "    ,OUTS.OUTKA_NO_M                                 " & vbNewLine _
        & "    ,OUTS.LOT_NO                                     " & vbNewLine _
        & "    ,EDTL.A_TORIHIKI_KBN                             " & vbNewLine _
        & "  ) AS MAIN                                          " & vbNewLine _
        & "  --商品マスタ                                       " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_GOODS AS GODS                        " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          GODS.NRS_BR_CD = MAIN.NRS_BR_CD            " & vbNewLine _
        & "      AND GODS.GOODS_CD_NRS = MAIN.GOODS_CD_NRS      " & vbNewLine _
        & "  --商品マスタの荷主帳票パターン(優先度1)            " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_CUST_RPT AS MCR2                     " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          MCR2.NRS_BR_CD = GODS.NRS_BR_CD            " & vbNewLine _
        & "      AND MCR2.CUST_CD_L = GODS.CUST_CD_L            " & vbNewLine _
        & "      AND MCR2.CUST_CD_M = GODS.CUST_CD_M            " & vbNewLine _
        & "      AND MCR2.CUST_CD_S = GODS.CUST_CD_S            " & vbNewLine _
        & "      AND MCR2.PTN_ID = '04'                         " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_RPT AS MR2                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          MR2.NRS_BR_CD = MCR2.NRS_BR_CD             " & vbNewLine _
        & "      AND MR2.PTN_ID = MCR2.PTN_ID                   " & vbNewLine _
        & "      AND MR2.PTN_CD = MCR2.PTN_CD                   " & vbNewLine _
        & "      AND MR2.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "  --出荷データLの荷主帳票パターン(優先度2)           " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_CUST_RPT AS MCR1                     " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          MCR1.NRS_BR_CD = MAIN.NRS_BR_CD            " & vbNewLine _
        & "      AND MCR1.CUST_CD_L = MAIN.CUST_CD_L            " & vbNewLine _
        & "      AND MCR1.CUST_CD_M = MAIN.CUST_CD_M            " & vbNewLine _
        & "      AND MCR1.CUST_CD_S = '00'                      " & vbNewLine _
        & "      AND MCR1.PTN_ID = '04'                         " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_RPT AS MR1                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          MR1.NRS_BR_CD = MCR1.NRS_BR_CD             " & vbNewLine _
        & "      AND MR1.PTN_ID = MCR1.PTN_ID                   " & vbNewLine _
        & "      AND MR1.PTN_CD = MCR1.PTN_CD                   " & vbNewLine _
        & "      AND MR1.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "  --デフォルトの帳票パターン(優先度3)                " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_RPT AS MR3                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          MR3.NRS_BR_CD = MAIN.NRS_BR_CD             " & vbNewLine _
        & "      AND MR3.PTN_ID = '04'                          " & vbNewLine _
        & "      AND MR3.STANDARD_FLAG = '01'                   " & vbNewLine _
        & "      AND MR3.SYS_DEL_FLG = '0'                      " & vbNewLine _
        & "ORDER BY                                             " & vbNewLine _
        & "   MAIN.A_TORIHIKI_KBN                               " & vbNewLine _
        & "  ,MAIN.OUTKA_NO_M                                   " & vbNewLine _
        & "  ,MAIN.OUTKA_NO_S                                   " & vbNewLine _
        & ""

    ''' <summary>
    ''' 出荷データSの備考をクリアする
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_LMC900_UPDATE_OUTKA_S_CLEAR As String = "" _
        & " UPDATE                          " & vbNewLine _
        & "   $LM_TRN$..C_OUTKA_S           " & vbNewLine _
        & " SET                             " & vbNewLine _
        & "    REMARK = ''                  " & vbNewLine _
        & "   ,SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
        & "   ,SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
        & "   ,SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
        & "   ,SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
        & " WHERE                           " & vbNewLine _
        & "       NRS_BR_CD = @NRS_BR_CD    " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine _
        & ""

    ''' <summary>
    ''' 出荷実績作成にて出力内容が必要となるので出荷データSに書き込んでおく
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_LMC900_UPDATE_OUTKA_S As String = "" _
        & " UPDATE                          " & vbNewLine _
        & "   $LM_TRN$..C_OUTKA_S           " & vbNewLine _
        & " SET                             " & vbNewLine _
        & "    REMARK = @REMARK             " & vbNewLine _
        & "   ,SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
        & "   ,SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
        & "   ,SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
        & "   ,SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
        & " WHERE                           " & vbNewLine _
        & "       NRS_BR_CD = @NRS_BR_CD    " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L  " & vbNewLine _
        & "   AND OUTKA_NO_M = @OUTKA_NO_M  " & vbNewLine _
        & "   AND OUTKA_NO_S = @OUTKA_NO_S  " & vbNewLine _
        & ""

#End Region 'LMC900

#End Region 'Const

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

#End Region 'Field

#Region "Method"

#Region "共通"

    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '管理番号
            whereStr = .Item("KANRI_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))

            'パターンフラグ('0':出荷、'1':運送)
            whereStr = .Item("PTN_FLAG").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_FLAG", whereStr, DBDataType.CHAR))

            '再発行フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))

            '管理番号
            whereStr = .Item("ORDER_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NO", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    '''荷主明細マスタ(設定値)取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>荷主明細マスタ取得SQLの構築・発行</remarks>
    Private Function SelectMCustDetailsData(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMC500IN").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC502DAC.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
        Call Me.setIndataParameter(Me._Row)                        '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC502DAC", "SelectMCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    '''  荷主明細マスタ用
    ''' </summary>
    ''' <remarks>荷主明細マスタ存在抽出用SQLの構築</remarks>
    Private Sub setIndataParameter(ByVal _Row As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        End With

    End Sub

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

#End Region '共通

#Region "LMC900"

    ''' <summary>
    ''' 物産アニマルヘルス：納品案内書
    ''' 出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData_LMC900(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC500IN")

        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        Dim FreeC03_Umu As String = String.Empty
        Me.SelectMCustDetailsData(ds)
        If ds.Tables("SET_NAIYO").Rows.Count > 0 Then
            FreeC03_Umu = ds.Tables("SET_NAIYO").Rows(0)("SET_NAIYO").ToString()
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '種別フラグを取得('0':出荷、'1':運送)
        Dim ptnFlag As String = Me._Row.Item("PTN_FLAG").ToString()

        '営業所CDを取得
        Dim nrs_br_cd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC502DAC.SQL_LMC900_SELECT_MAIN)

        '条件設定
        Call Me.SetConditionMasterSQL()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウト対策
        cmd.CommandTimeout = 6000

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC502DAC", "SelectPrintData_LMC900", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("SEIRI_NO", "SEIRI_NO")
        map.Add("GYO_NO", "GYO_NO")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("TITLE", "TITLE")
        map.Add("A_DATE", "A_DATE")
        map.Add("A_TORIHIKI_KBN", "A_TORIHIKI_KBN")
        map.Add("A_SEIQTO_CD", "A_SEIQTO_CD")
        map.Add("A_SEIQTO_NM_1", "A_SEIQTO_NM_1")
        map.Add("A_SEIQTO_NM_2", "A_SEIQTO_NM_2")
        map.Add("A_CHOKUSO_CD", "A_CHOKUSO_CD")
        map.Add("A_CHOKUSO_NM", "A_CHOKUSO_NM")
        map.Add("A_NONYU_CD", "A_NONYU_CD")
        map.Add("A_NONYU_NM", "A_NONYU_NM")
        map.Add("A_NONYU_ZIP", "A_NONYU_ZIP")
        map.Add("A_NONYU_ADD_1", "A_NONYU_ADD_1")
        map.Add("A_NONYU_ADD_2", "A_NONYU_ADD_2")
        map.Add("A_JISHA_NM", "A_JISHA_NM")
        map.Add("A_WH_NM", "A_WH_NM")
        map.Add("A_MAKER_CD", "A_MAKER_CD")
        map.Add("A_DEPO_CD", "A_DEPO_CD")
        map.Add("A_GOODS_CD", "A_GOODS_CD")
        map.Add("A_GOODS_NM", "A_GOODS_NM")
        map.Add("A_DENP_REMARK", "A_DENP_REMARK")
        map.Add("A_DTL_REMARK", "A_DTL_REMARK")
        map.Add("A_FIXED", "A_FIXED")
        map.Add("SURYO", "SURYO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("MOTOKON", "MOTOKON")
        map.Add("HASU", "HASU")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("ORD_NO", "ORD_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC900OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 物産アニマルヘルス：納品案内書
    ''' 出荷データSの備考をクリア
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaS_Clear_LMC900(ByVal ds As DataSet) As DataSet

        Dim updCnt As Integer = 0

        '対象の出荷管理番号Lをすべて処理
        Dim inTbl As DataTable = ds.Tables("LMC900OUT").DefaultView.ToTable(True, "NRS_BR_CD", "OUTKA_NO_L")
        For i As Integer = 0 To inTbl.Rows.Count - 1
            'DataSetのIN情報を取得
            Dim inRow As DataRow = inTbl.Rows(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(Me.SetSchemaNm(LMC502DAC.SQL_LMC900_UPDATE_OUTKA_S_CLEAR, inRow("NRS_BR_CD").ToString()))

            'SQLパラメータ設定
            Me._SqlPrmList = New ArrayList()
            With Me._SqlPrmList
                .Add(MyBase.GetSqlParameter("@REMARK", String.Empty, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.CHAR))

                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_NO_L", inRow("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            End With

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC502DAC", "UpdateOutkaS_Clear_LMC900", cmd)

            'SQL発行
            updCnt += MyBase.GetUpdateResult(cmd)
        Next

        '更新総件数を処理結果件数として設定
        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 物産アニマルヘルス：納品案内書
    ''' 出荷実績作成にて出力内容が必要となるので出荷データSに書き込んでおく
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaS_LMC900(ByVal ds As DataSet) As DataSet

        Dim updCnt As Integer = 0

        '対象データをすべて処理
        Dim inTbl As DataTable = ds.Tables("LMC900OUT")
        For i As Integer = 0 To inTbl.Rows.Count - 1
            'DataSetのIN情報を取得
            Dim inRow As DataRow = inTbl.Rows(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(Me.SetSchemaNm(LMC502DAC.SQL_LMC900_UPDATE_OUTKA_S, inRow("NRS_BR_CD").ToString()))

            'SQLパラメータ設定
            Me._SqlPrmList = New ArrayList()
            With Me._SqlPrmList
                '今回の出力内容を結合文字列として加工
                Const SEP As String = ";"
                Dim remark As String = String.Concat(
                    "納品案内書",
                    SEP,
                    inRow("DENP_NO").ToString(),
                    SEP,
                    inRow("SEIRI_NO").ToString(),
                    SEP,
                    inRow("GYO_NO").ToString(),
                    SEP,
                    inRow("SURYO").ToString(),
                    SEP,
                    inRow("MOTOKON").ToString(),
                    SEP,
                    inRow("HASU").ToString(),
                    SEP,
                    inRow("LOT_NO").ToString(),
                    SEP,
                    inRow("LT_DATE").ToString()
                    )

                .Add(MyBase.GetSqlParameter("@REMARK", remark, DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.CHAR))

                .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_NO_L", inRow("OUTKA_NO_L").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_NO_M", inRow("OUTKA_NO_M").ToString(), DBDataType.CHAR))
                .Add(MyBase.GetSqlParameter("@OUTKA_NO_S", inRow("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            End With

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC502DAC", "UpdateOutkaS_LMC900", cmd)

            'SQL発行
            updCnt += MyBase.GetUpdateResult(cmd)
        Next

        '更新総件数を処理結果件数として設定
        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#End Region 'LMC900

#End Region 'Method

End Class
